using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

/// <summary>
/// Summary description for CalcClass
/// </summary>
public class CalcClass
{
    protected string connectionString;

    protected string APPL_NAME = "mpx-web";

    public const string RUN_FILE = "vsm.run";

    public const short CLEAR_FLAG = 0;
    public const short LAB_OVER_U = 1;
    public const short EQ_OVER_U = 2;
    public const short WARN_FLAG = 4;
    public const short ERR_FLAG = 8;
    public const short NO_OPT_FLAG = 16;
    public const short NO_EQUIP_FLAG = 32;
    public const short NO_PROD_FLAG = 64;

    public const short WHATIFSTOP_FLAG = 0;
    public const short WHATIFGO_FLAG = 3;

    public const short DOCK_STOCK_MISSING = 2; // flag value
    public const short NO_ROUTING = 4; // flag value

    public CalcClass(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public CalcClass()
    {

    }

    public static bool CalculationsCancelled(string cookieid)
    {
        bool cancelled = false;
        ADODB.Connection connMySql = new ADODB.Connection();
        ADODB.Recordset recMySql = new ADODB.Recordset();
        DbUse.OpenAdoMysql(connMySql);
        DbUse.OpenAdoRec(connMySql, recMySql, "SELECT usercalc.cancel FROM usercalc INNER JOIN userlist ON userlist.id = usercalc.id WHERE userlist.sessionid =  '" + HttpContext.Current.Session.SessionID + "';");
        try
        {
            cancelled = !recMySql.Fields["cancel"].Value.ToString().Equals("0");
        }
        catch (Exception) { }
        DbUse.CloseAdoRec(recMySql);
        DbUse.CloseAdo(connMySql);
        return cancelled;
    }

    public bool runsql(string commandString)
    {
        string connection = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + connectionString;
        return DbUse.RunSql(commandString, connection);
    }


    public static CalculationResult CalculateResults(ClassF classE1_1)
    {
        LogFiles logFiles = new LogFiles(classE1_1.username);
        if (DbUse.InRunProcess(classE1_1.varlocal, true))
        {
            logFiles.DuplicateRunEndLog();
            throw new Exception("Cannot start verification and calculations. The verification and calculations are still in process from the previous run. Please wait.");
        }
       

        DbUse.RunMysql("INSERT INTO usercalc (id) SELECT userlist.id FROM userlist WHERE userlist.sessionid = '" + HttpContext.Current.Session.SessionID + "';");
        DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET total = 1, calc = 1, lastCheck = " + DateTime.Now.Ticks + ", cancel = 0 WHERE userlist.sessionid = '" + HttpContext.Current.Session.SessionID + "';");
        DbUse.CreateRunFile(classE1_1.varlocal, classE1_1.username);
        classE1_1.Open();
        classE1_1.SetBasicModelInfo();  //  sets glngwid etc.

        classE1_1.calc_return = 0;     //0 - none, 1 labor, 2 eq over util, 4 warnings 8 errors 

        string errorsMessage = "";
        string resultsMessage = "";
        try
        {
            classE1_1.RunDLL();
            if (!classE1_1.errorMessageGlobal.Equals(""))
            {
                string errorMessageGlobal = classE1_1.errorMessageGlobal;
                classE1_1.errorMessageGlobal = "";
                throw new Exception("The calculations were unsuccessful. " + errorMessageGlobal);
            }

            if ((classE1_1.calc_return & CalcClass.ERR_FLAG) > 0)
            {
                resultsMessage = do_calc_msg(classE1_1.calc_return, 0);
            }
            else
            {
                resultsMessage = do_calc_msg(classE1_1.calc_return, 1);

            }
            errorsMessage = GetErrorMessage(classE1_1);
            save_errors_results(classE1_1, resultsMessage, errorsMessage);
            classE1_1.runsqlado("UPDATE zs0tblWhatif SET display = -1 WHERE WID = " + classE1_1.glngwid + ";");
            classE1_1.Close();
        }
        catch (Exception ex)
        {
            logFiles.ErrorLog(ex);
            classE1_1.Close();
            DbUse.DeleteRunFile(classE1_1.varlocal, classE1_1.username);
            logFiles.RunEndLog();
            DbUse.RunMysql("DELETE usercalc.* FROM usercalc INNER JOIN userlist ON usercalc.id = userlist.id WHERE userlist.sessionid = '" + HttpContext.Current.Session.SessionID + "';");
            throw new Exception("MPX internal error has occured in calculations. Cannot calculate results. " + classE1_1.errorMessageGlobal);
        }
        DbUse.DeleteRunFile(classE1_1.varlocal, classE1_1.username);
        DbUse.RunMysql("DELETE usercalc.* FROM usercalc INNER JOIN userlist ON usercalc.id = userlist.id WHERE userlist.sessionid = '" + HttpContext.Current.Session.SessionID + "';");

        logFiles.RunEndLog();
        CalculationResult calcResult = new CalculationResult(resultsMessage, errorsMessage);
        return calcResult;
    }

    public static string GetErrorMessage(ClassE classE1_1)
    {
        ADODB.Recordset reccust = null;
        string errorMsg;

        classE1_1.Open();
        DbUse.open_ado_rec(classE1_1.globaldb, ref reccust, "SELECT zstblErrors.* FROM zstblErrors ORDER BY zstblErrors.Level, zstblErrors.text,  zstblErrors.Table, "
        + " zstblErrors.equipid, zstblErrors.partid; ");  //  6-26-17  sort by ...

        errorMsg = "";
        while (!reccust.EOF)
        {
            errorMsg = errorMsg + (string)reccust.Fields["Text"].Value + "<br/>";
            reccust.MoveNext();

        }
        DbUse.CloseAdoRec(reccust);
        return errorMsg;
    }

    public static string do_calc_msg(int calc_r, int mtype)
    {
        string str1 = "";

        if ((calc_r & CalcClass.NO_OPT_FLAG) != 0)
        {
            str1 = "No products have been selected for lotsize or transfer batch size optimization";
            return str1;
        }

        //--------------------------------------------------------------

        if ((calc_r & CalcClass.NO_EQUIP_FLAG) != 0)
        {
            str1 = "No equipment groups have been defined (added) in this model?";
        }

        if ((calc_r & CalcClass.NO_PROD_FLAG) != 0)
        {
            if (str1.Length > 0)
                str1 += "<br/>No products have been defined (added) in this model?";
            else
                str1 = "No products have been defined (added) in this model?";
        }
        if (str1.Length > 0)
            return str1;

        //--------------------------------------------------------------

        str1 = "Verification completed<br/>";
        if (((calc_r & CalcClass.ERR_FLAG) == 0) & ((calc_r & CalcClass.WARN_FLAG) == 0)) { str1 += "No Errors or Warnings found<br/>"; }
        else
        {
            if ((calc_r & CalcClass.ERR_FLAG) > 0) { str1 += "Some Errors found"; } else { str1 += "No Errors found"; }
            if ((calc_r & CalcClass.WARN_FLAG) > 0) { str1 += "Some Warnings found<br/>"; } else { str1 += "No Warnings found<br/>"; }
        }

        if (mtype == 0) { return (str1); };
        if ((calc_r & CalcClass.ERR_FLAG) != 0) { return (str1); };

        // today today  verify needed ???   AFTER error correct wid ?    recalc all 3, 4, 45 whatif_err  equip ?? OnAbortTransaction second/ TimeoutException around ???

        //--------------------------------------------------------------
        //  & CALC
        str1 += "<br/>Calculations completed<br/><br/>";
        if (((calc_r & CalcClass.LAB_OVER_U) == 0) & ((calc_r & CalcClass.EQ_OVER_U) == 0)) { str1 += "<b>PRODUCTION IS SUSTAINABLE<br/>NO RESOURCES ARE OVER-UTILIZED</b>"; }
        else
        {
            //str1 += DbUse.toRedColor("PRODUCTION IS INFEASIBLE");
            if ((calc_r & CalcClass.LAB_OVER_U) > 0) { str1 += "<br/>" + DbUse.toRedColor("LABOR(S) ARE OVER-UTILIZED"); }
            if ((calc_r & CalcClass.EQ_OVER_U) > 0) { str1 += "<br/>" + DbUse.toRedColor("EQUIPMENT(S) ARE OVER-UTILIZED"); }
            //str1 += "<br/>" + DbUse.toRedColor("LEAD-TIMES, CRITICAL PATHS, AND WIP CANNOT BE CALCULATED");
        }
        return str1;
    }

    public static string getValidationMessage(int calc_r)
    {
        string str1 = "";

        if ((calc_r & CalcClass.NO_OPT_FLAG) != 0)
        {
            str1 = "No products have been selected for lotsize or transfer batch size optimization";
            return str1;
        }

        //--------------------------------------------------------------

        if ((calc_r & CalcClass.NO_EQUIP_FLAG) != 0)
        {
            str1 = "No equipment groups have been defined (added) in this model?";
        }

        if ((calc_r & CalcClass.NO_PROD_FLAG) != 0)
        {
            if (str1.Length > 0)
                str1 += " No products have been defined (added) in this model?";
            else
                str1 = "No products have been defined (added) in this model?";
        }
        if (!(str1.Length > 0))
        {
            str1 = null;
        }

        return str1;

    }

    public static string getErrWarnMessage(int calc_r)
    {
        string str1 = "";


        if ((calc_r & CalcClass.ERR_FLAG) > 0) { str1 += " Some Errors found."; } else {  }
        if ((calc_r & CalcClass.WARN_FLAG) > 0) { str1 += " Some Warnings found."; } else { }
        if (str1.Equals(String.Empty))
        {
            str1 = null;
        }

        return str1;
    }

    public static string getMessage(int calc_r)
    {
        string str1 = "";

        if (!String.IsNullOrEmpty(getValidationMessage(calc_r)))
        {
            str1 = null;
        }
        else
        {
            if (((calc_r & CalcClass.LAB_OVER_U) == 0) & ((calc_r & CalcClass.EQ_OVER_U) == 0))
            {
                //str1 += "NO RESOURCES ARE OVER-UTILIZED";
                str1 = null;
            }
            else
            {
                //str1 += DbUse.toRedColor("PRODUCTION IS INFEASIBLE");
                if ((calc_r & CalcClass.LAB_OVER_U) > 0) { str1 += "LABOR OVER-UTILIZATION"; }
                if ((calc_r & CalcClass.EQ_OVER_U) > 0) { str1 += "EQUIPMENT OVER-UTILIZATION"; }
                //str1 += "<br/>" + DbUse.toRedColor("LEAD-TIMES, CRITICAL PATHS, AND WIP CANNOT BE CALCULATED");
            }
        }

        return str1;
    }

    public static void save_errors_results(ClassE classE1_1, string msgResults, string msgErrors)
    {
        string commandString;
        string msgResultsRep = msgResults.Replace("<br/>", "\n");
        string msgErrorsRep = msgErrors.Replace("<br/>", "\n");
        msgResultsRep = DbUse.removeRedColor(msgResultsRep);
        commandString = "UPDATE zstblresults SET zstblresults.results = '" + msgResultsRep + "';";
        classE1_1.runsql(commandString);

        commandString = "UPDATE zstblresults SET zstblresults.errors = '" + msgErrorsRep + "';";
        classE1_1.runsql(commandString);
    }

    public class CalculationResult
    {
        public string resultMessage;
        public string errorMessage;

        public CalculationResult(string resultMessage, string errorMessage)
        {
            this.resultMessage = resultMessage;
            this.errorMessage = errorMessage;
        }
    }

}