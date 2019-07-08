
using System.Data;
using System.Data.OleDb;
using ADODB;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Web;


//  to be done  save whatif sub 447 ??


public partial class ClassC1 : ClassC0
{






    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iClear_model(int collect, string dir1);
    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iRun_model(int iWID, int errcode,
                                           int lab_err, int eq_err,
                                           int part_err, int oper_err, int route_err, int ibom_err, int full, int mess);
    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iGeneral_time(float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var);

    // ??  int or int * for the items after the first one  count is the size of the arrays ...
    //  int = int ??

    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iLabor_data(int l_count, int[] lab_id, float[] lab_size, float[] lab_ot, float[] lab_abs, float[] lab_s_fact, float[] lab_r_fact, float[] lab_v_fact, int[] balance);


    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iEquip_data(int eq_count, int[] eq_id, int[] eq_size, float[] mttf, float[] mttr, float[] eq_ot,
                                             int[] lab_id, float[] eq_s_fact, float[] eq_r_fact, float[] eq_v_fact, int[] cellid);
    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iParts_data(int p_count, int[] part_id, float[] demand, float[] lotsize, float[] tbatch,
                                             float[] part_d_fact, float[] part_v_fact, int[] tgather);

    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iOper_data(int op_count, int[] op_name, int[] op_id, int[] part_id, int[] eq_id, float[] pct_assgn,
                                               float[] eq_setup1, float[] eq_run1, float[] lab_setup1, float[] lab_run1,
                                               float[] eq_setup2, float[] eq_run2, float[] lab_setup2, float[] lab_run2,
                                               float[] eq_setup3, float[] eq_run3, float[] lab_setup3, float[] lab_run3);

    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iRoute_data(int r_count, int[] part_id, int[] from_op_id, int[] to_op_id, float[] pct_route);
    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iIbom_data(int ib_count, int[] prt_part_id, int[] comp_part_id, float[] upa);

    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int pic2text(int rec_id);
    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int text2pic(int rec_id);




    //  not needed!!  [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)] public static extern int  solve_string(var str_v, int X, float qvalues);
    //  not needed!! [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)] public static extern int setup_string(var str_v);
    //  not needed!! [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)] public static extern int  isolve(var str_v);

    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iOptimization(int numberOfParts, int[] partid,
                                           float[] weight, int[] optimizeLotSize, int[] OptimizeTbatch, float[] lotsizeValue, float[] tbatchValue);

    // The path can be just the dll name because the website bin folder is added to the system environment path variables (the place where the system searches for dlls by default) in DbPage class!!!!!!!!!
    //protected const string MPX_DLL_PATH = "webmpx95i.dll";
    protected const string MPX_DLL_PATH = "dll2.dll";
    // New function add of IDO Pictures;

    //
    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int iopt_all(string dir1,
             //General_time (
             float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var,

              //Labor_data (
              int l_count, int[] x1, float[] x2, float[] x3, float[] x4,
              float[] x5, float[] x6, float[] x7, int[] x8,

              //  Equip_data (
              int eq_count, int[] e_x1, int[] e_x2, float[] e_x3, float[] e_x4, float[] e_x5,
              int[] e_x6, float[] e_x7, float[] e_x8, float[] e_x9, int[] e_x10,

                //Parts_data (
                int p_count, int[] p_x1, float[] p_x2, float[] p_x3, float[] p_x4,
                 float[] p_x5, float[] p_x6, int[] p_x7,

                  //Oper_data (
                  int op_count, int[] o_x1, int[] o_x2, int[] o_x3, int[] o_x4, float[] o_x5,
                  float[] o_x6, float[] o_x7, float[] o_x8, float[] o_x9,
                  float[] o_x10, float[] o_x11, float[] o_x12, float[] o_x13,
                  float[] o_x14, float[] o_x15, float[] o_x16, float[] o_x17,

                   // Route_data (
                   int r_count, int[] r_x1, int[] r_x2, int[] r_x3, float[] r_x4,

                   //Ibom_data (
                   int ib_count, int[] i_x1, int[] i_x2, float[] i_x3,


                //Run_model (
                int iWID, //, int *  ierrcode, int *  ilab_err, int *  ieq_err, int *
                          //ipart_err, int *  ioper_err, int *  iroute_err, int *  iibom_err, int full, int MESS) 

               //  opt stuff 
               int numberOfParts, int[] partid,
               float[] weight, int[] optimizeLotSize, int[] OptimizeTbatch, float[] lotsizeValue, float[] tbatchValue);

    [DllImport(MPX_DLL_PATH, CallingConvention = CallingConvention.Winapi)]
    public static extern int irun_it(string dir1,
             //General_time (
             float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var,

              //Labor_data (
              int l_count, int[] x1, float[] x2, float[] x3, float[] x4,
              float[] x5, float[] x6, float[] x7, int[] x8,

              //  Equip_data (
              int eq_count, int[] e_x1, int[] e_x2, float[] e_x3, float[] e_x4, float[] e_x5,
              int[] e_x6, float[] e_x7, float[] e_x8, float[] e_x9, int[] e_x10,

                //Parts_data (
                int p_count, int[] p_x1, float[] p_x2, float[] p_x3, float[] p_x4,
                 float[] p_x5, float[] p_x6, int[] p_x7,

                  //Oper_data (
                  int op_count, int[] o_x1, int[] o_x2, int[] o_x3, int[] o_x4, float[] o_x5,
                  float[] o_x6, float[] o_x7, float[] o_x8, float[] o_x9,
                  float[] o_x10, float[] o_x11, float[] o_x12, float[] o_x13,
                  float[] o_x14, float[] o_x15, float[] o_x16, float[] o_x17,

                   // Route_data (
                   int r_count, int[] r_x1, int[] r_x2, int[] r_x3, float[] r_x4,

                   //Ibom_data (
                   int ib_count, int[] i_x1, int[] i_x2, float[] i_x3,


                //Run_model (
                int iWID);



    int add_error_text(int atend)
    {

        //on error goto err_adderrortext;

        ADODB.Recordset reccust = null;

        int n1;
        int n2;
        string strmatch;
        string namestr;
        int return_val;
        int ret;



        return_val = atend;

        //DbUse.open_ado_rec(globaldb, ref reccust, "zstblerrors");

        //if (inOPT == 0) {
        //    reccust.Filter = "[mpxflag] = 1 and [level] = 1";
        //    if (!reccust.EOF) {
        //        /*  ??  TDB  ret = // msgbox("There are 1 or more warnings from MPX. Do you want to keep these?", MsgBoxStyle.YesNo, appl_name);
        //        if (ret  == // msgboxResult.No)   {
        //            runsql("delete zstblErrors.* From zstblErrors WHERE (((zstblErrors.mpxFlag)=1) AND ((zstblErrors.Level)=1));");
        //            };
        //          */

        //    };
        //};  // if in opt
        //DbUse.CloseAdoRec(reccust);

        //Add labor names, equip, parts , etc;
        runsqlado("UPDATE zstblErrors INNER JOIN tbllabor ON zstblErrors.laborid = tbllabor.LaborID SET zstblErrors.[Text] = [text] + ' Labor Name: ' + [labordesc] WHERE (((zstblErrors.mpxFlag)=1));");
        runsqlado("UPDATE zstblErrors INNER JOIN tblequip ON zstblErrors.equipid = tblequip.EquipID SET zstblErrors.[Text] = [text] + ' Equipment Name: ' + [equipdesc] WHERE (((zstblErrors.mpxFlag)=1));");
        runsqlado("UPDATE zstblErrors INNER JOIN tblprodfore ON zstblErrors.partid = tblprodfore.ProdID SET zstblErrors.[Text] = [text] + ' Product: ' + [proddesc] WHERE (((zstblErrors.mpxFlag)=1));");
        runsqlado("UPDATE zstblErrors INNER JOIN tbloper ON zstblErrors.operID = tbloper.OpID SET zstblErrors.[Text] = [text] + ' Operation: ' + [opnam] WHERE (((zstblErrors.mpxFlag)=1));");
        runsqlado("UPDATE zstblErrors INNER JOIN tbloper ON zstblErrors.routeID = tbloper.OpID SET zstblErrors.[Text] = [text] + ' To Operation: ' + [opnam] WHERE (((zstblErrors.mpxFlag)=1));");
        runsqlado("UPDATE zstblErrors INNER JOIN tblprodfore ON zstblErrors.ibomID = tblprodfore.ProdID SET zstblErrors.[Text] = [text] + ' Component: ' + [proddesc] WHERE (((zstblErrors.mpxFlag)=1));");

        // mark mpx errors done;
        runsqlado("UPDATE zstblErrors SET zstblErrors.mpxflag = 0 WHERE (((zstblErrors.mpxFlag)=1));");

        //exit_adderrortext:

        return return_val;
        //exit  Function;

        //err_adderrortext:
        /*   if (Err.Number  == 3163)   {
              // msgbox("Error/Warning message is too int for available space.  Message will be truncated", 0, appl_name);
              reccust.Fields["Text"].Value = Left(reccust.Fields["Text"].Value + namestr, 250);
               //RESUME NEXT
              };
         * 
                     * */
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end function;

    int check_oper_data(int intcount)
    {
        //on error goto err_check_oper_data;

        // check if eq time > == labor time (include speed factors ?);
        // check if eq time  == 0 and labor time > 0 (include speed factors);
        //  ? see check stuff from MPX;
        // if error add to zstblerrors table;

        ADODB.Recordset recerror = null;
        ADODB.Recordset recoper = null;
        int i;
        int fstatus;

        DbUse.open_ado_rec(globaldb, ref recerror, "zstblerrors");



        for (i = 1; i <= (intcount - 1); i++)
        {
            if (((eq_setup[i] == 0.0) && (Lab_setup[i] > 0.0)))
            {
                recerror.AddNew();
                recerror.Fields["Level"].Value = 1; // 'warning;
                calc_return |= WARN_FLAG;
                recerror.Fields["Table"].Value = "Operations";
                recerror.Fields["Text"].Value = " Equipment Setup Time is 0 and Labor Setup Time > 0 ";
                recerror.Fields["mpxFlag"].Value = -1;
                recerror.Fields["partid"].Value = Part_id2[i];
                recerror.Fields["operid"].Value = op_id[i];
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
            }
            else
            {
                recerror.AddNew();
                recerror.Fields["Level"].Value = 1; // 'warning;
                calc_return |= WARN_FLAG;
                recerror.Fields["Table"].Value = "Operations";
                recerror.Fields["Text"].Value = " Equipment Setup Time is < Labor Setup Time ";
                recerror.Fields["mpxFlag"].Value = -1;
                recerror.Fields["partid"].Value = Part_id2[i];
                recerror.Fields["operid"].Value = op_id[i];
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
            };

            if (((eq_run[i] == 0.0) && (Lab_run[i] > 0.0)))
            {
                recerror.AddNew();
                recerror.Fields["Level"].Value = 1; // 'warning;
                calc_return |= WARN_FLAG;
                recerror.Fields["Table"].Value = "Operations";
                recerror.Fields["Text"].Value = " Equipment Run Time is 0 and Labor Run Time > 0 ";
                recerror.Fields["mpxFlag"].Value = -1;
                recerror.Fields["partid"].Value = Part_id2[i];
                recerror.Fields["operid"].Value = op_id[i];
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
            }
            else
            {
                recerror.AddNew();
                recerror.Fields["Level"].Value = 1; // 'warning;
                calc_return |= WARN_FLAG;
                recerror.Fields["Table"].Value = "Operations";
                recerror.Fields["Text"].Value = " Equipment Run Time is < Labor Run Time ";
                recerror.Fields["mpxFlag"].Value = -1;
                recerror.Fields["partid"].Value = Part_id2[i];
                recerror.Fields["operid"].Value = op_id[i];
                recerror.Fields["Wid"].Value = glngwid;
                recerror.Update();
            };

        }; // end for

        fstatus = add_error_text(0);



        //exit_check_oper_data:

        if (null != recoper)
        {
            DbUse.CloseAdoRec(recoper);
            recoper = null;
        };

        if (null != recerror)
        {
            DbUse.CloseAdoRec(recerror);
            recerror = null;
        };

        return -1; //exit  Function;

        //err_check_oper_data:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_check_oper_data;

    } // end function;

    void clear_results()
    {
        //on error goto err_clear_results;

        string strSQLTree;
        string strSQLPole;
        runsqlado("delete tblBOMTree.* from tblBOMTree");
        runsqlado("delete tblnotpossible.* from tblnotpossible");
        runsqlado("delete tblpossiblecomp.* from tblpossiblecomp");


        strSQLTree = "DELETE tblRsIbomTree.* From tblRsIbomTree;";
        strSQLPole = "DELETE tblRsIbomPoles.* From tblRsIbomPoles;";

        //Run the Delete Queries to empty tables;
        runsqlado(strSQLTree);
        runsqlado(strSQLPole);
        //exit_clear_results:
        return; //exit  Sub;

        //err_clear_results:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_clear_results;

    } // end sub;

    void EmptyResultsTables()
    {
        //on error goto Err_EmptyResultsTables;

        //Define variables;
        string strSQLAsset;
        string strSQLSummary;
        string strSQLEquip;
        string strSQLProd;
        string strSQLLabor;
        string strSQLoper;

        //Set up SQL Statements to delete all records from results tables;
        //If in What-if Mode, then only delete What-if results;

        strSQLSummary = "DELETE tblRsSummary.* From tblRsSummary WHERE ((tblRsSummary.WID=" + glngwid + "));";
        strSQLEquip = "delete tblRsEquip.* From tblRsEquip WHERE ((tblRsEquip.WID=" + glngwid + "));";
        strSQLProd = "delete tblRsProd.* From tblRsProd WHERE ((tblRsProd.WID=" + glngwid + "));";
        strSQLLabor = "delete tblRsLabor.* From tblRsLabor WHERE ((tblRsLabor.WID=" + glngwid + "));";
        strSQLoper = "delete tblRSOper.* FROM tblRSOper WHERE ((tblRSOper.WID=" + glngwid + "));";

        //Run the Delete Queries to empty tables;
        //  runsql strSQLAsset;

        runsqlado(strSQLSummary);
        runsqlado(strSQLEquip);
        runsqlado(strSQLProd);
        runsqlado(strSQLLabor);
        runsqlado(strSQLoper);

        // return; //exit _EmptyResultsTables:
        return; //exit  Sub;

        //Err_EmptyResultsTables:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT
    } // end sub;

    int erroradd()
    {
        return erroradd(false);
    }

    int erroradd(bool pickDllErrors)
    {
        //on error goto err_error_add;

        // add file to current table;
        // convert indexes into names;
        // add to text;

        ADODB.Recordset reccust = null;
        int return_val = 2;
        string strsql;

        //return 2 if nothing;  0 if error;  1 if warning;

        if (pickDllErrors)
        {
            // add errors from dll
            transfertext("zstblErrors", varlocal + "\\Results.err", -1);
        }

        DbUse.open_ado_rec(globaldb, ref reccust, "zstblErrors");
        if (reccust.EOF)
        {
            return_val = 2;
            //no errors or warnings
        };
        DbUse.CloseAdoRec(reccust);

        DbUse.open_ado_rec(globaldb, ref reccust, "select zstblErrors.* from zstblerrors where [level] = 1");
        if ((!reccust.EOF))
        {
            // level 1 => warning
            calc_return |= WARN_FLAG;
            return_val = 1;
        };
        DbUse.CloseAdoRec(reccust);

        DbUse.open_ado_rec(globaldb, ref reccust, "select zstblErrors.* from zstblerrors where [level] = 0");
        if ((!reccust.EOF))
        {
            // level 0 => error
            calc_return |= ERR_FLAG;
            return_val = 0;
        }
        DbUse.CloseAdoRec(reccust);



        // add labor/equip/oper/product names to error records
        add_error_text(return_val);


        return return_val; //exit  Function;

        //err_error_add:
        /*   if ((Err.Number  == 3176))   {
             // no errors or warnings.;
         } else {
             // msgbox(ErrorToString(), 0, appl_name);
             };
         * 
         * */
        //RESUME NEXT

    } // end function;


    void Input(ref string str1, ref string[] a)
    {

        char ccomma = Convert.ToChar(44);

        a = str1.Split(ccomma);

        return;
    }

    public float Change_days()
    {

        float ratio;
        float rtu1c;
        string TUFor;

        ADODB.Recordset recUnits = new ADODB.Recordset();
        //Open();
        DbUse.OpenAdoRec(globaldb, recUnits, "SELECT TUFor, rtu1c FROM tblgeneral");
        try
        {

            TUFor = recUnits.Fields["TUFor"].Value.ToString();
            rtu1c = float.Parse(recUnits.Fields["rtu1c"].Value.ToString());
        }
        catch (Exception ex)
        {
            throw new Exception("Error in unit conversion data. Cannot do unit conversions.");
        }


        //   set   rtu1c  and TUFor  //lucie code here !!!

        if (TUFor.ToUpper().Equals("YEAR"))
        {
            ratio = 365 / rtu1c;
            return (ratio);

        }
        else if (TUFor.ToUpper().Equals("QUARTER"))
        {
            ratio = 365 / (rtu1c * 4);
            return (ratio);

        }
        else if (TUFor.ToUpper().Equals("MONTH"))
        {
            ratio = 365 / (rtu1c * 12);
            return (ratio);
        }
        else if (TUFor.ToUpper().Equals("WEEK"))
        {
            return (1.0f);  //  do nothing !!!!
        }

        return (1.0f);  //  never get here !!
    }



    void transfertext(string tblname, string Filename, int header)
    {
        //on error goto err_transfer;

        string[] a = null;

        ADODB.Recordset reccust = null;
        string str2;
        string str1;

        float ratio = Change_days();

        if (!FileOperations.WaitForFile(Filename))
        {
            string fileBasicName = Filename.Substring(varlocal.Length + 1);
            errorMessageGlobal += "File '" + fileBasicName + "' was not created. ";
            return;
        }
        try
        {
            if (tblname.Equals("zstblerrors"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {                  //simple file open 
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);                                         //  simple read all reads on same line ...


                        reccust.AddNew();
                        reccust.Fields["Level"].Value = (a.Length == 0) ? "0" : a[0];
                        reccust.Fields["Table"].Value = a[1];
                        reccust.Fields["Text"].Value = a[2];
                        reccust.Fields["mpxFlag"].Value = (a[3].Length == 0) ? "0" : a[3];
                        reccust.Fields["LaborID"].Value = (a[4].Length == 0) ? "0" : a[4];
                        reccust.Fields["equipid"].Value = (a[6].Length == 0) ? "0" : a[6];
                        reccust.Fields["operid"].Value = (a[7].Length == 0) ? "0" : a[7];
                        reccust.Fields["routeid"].Value = (a[8].Length == 0) ? "0" : a[8];
                        reccust.Fields["ibomid"].Value = (a[9].Length == 0) ? "0" : a[9];
                        reccust.Fields["Wid"].Value = glngwid;
                        reccust.Update();
                    };  //end while Loop;
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if 

            if (tblname.Equals("tblRsEquip"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {                   //simple file open 
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["wid"].Value = a[0];
                        reccust.Fields["equipid"].Value = a[1];
                        reccust.Fields["setuputil"].Value = dig_round(Convert.ToDouble(a[2]), 4);
                        reccust.Fields["RunUtil"].Value = dig_round(Convert.ToDouble(a[3]), 4);
                        reccust.Fields["reputil"].Value = dig_round(Convert.ToDouble(a[4]), 4);
                        reccust.Fields["labwaitutil"].Value = dig_round(Convert.ToDouble(a[5]), 4);
                        reccust.Fields["IDLE"].Value = dig_round(Convert.ToDouble(a[6]), 4);
                        reccust.Fields["qprocess"].Value = dig_round(Convert.ToDouble(a[7]), 4);
                        reccust.Fields["qwait"].Value = dig_round(Convert.ToDouble(a[8]), 4);
                        reccust.Fields["Qtotal"].Value = dig_round(Convert.ToDouble(a[9]), 4);
                        reccust.Update();
                    };  //end while
                    fin.Close();

                    //get_utlimit;

                    string t2;
                    string ratio_str;
                    ratio_str = Convert.ToString(ratio);

                    t2 = " 123.0 "; // get_gen_t2();  // get  field  time2 = ((float)recGeneral.Fields["RTU1c"].Value);
                    str1 = " UPDATE tblEquip INNER JOIN tblRsEquip ON tblEquip.EquipID = tblRsEquip.EquipID SET tblRsEquip.index1 = ([tblequip].[ot]/100+1)*" + ratio_str + "*" + t2 + " WHERE (((tblRsEquip.WID)=" + a[0] + "));";
                    runsql(str1);
                }
                goto exit_transfer;
            }  // end if 

            if (tblname.Equals("tblRsLabor"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };
                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["wid"].Value = a[0];
                        reccust.Fields["LaborID"].Value = a[1];
                        reccust.Fields["setuputil"].Value = dig_round(Convert.ToDouble(a[2]), 4);
                        reccust.Fields["RunUtil"].Value = dig_round(Convert.ToDouble(a[3]), 4);
                        reccust.Fields["absutil"].Value = dig_round(Convert.ToDouble(a[4]), 4);
                        reccust.Fields["IDLE"].Value = dig_round(Convert.ToDouble(a[5]), 4);
                        reccust.Fields["qprocess"].Value = dig_round(Convert.ToDouble(a[6]), 4);
                        reccust.Fields["qwait"].Value = dig_round(Convert.ToDouble(a[7]), 4);

                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if 

            if (tblname.Equals("tblRsSummary"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };
                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["wid"].Value = Convert.ToInt32(a[0]);
                        reccust.Fields["product family"].Value = "";
                        reccust.Fields["TotalProd"].Value = Convert.ToSingle(dig_round(Convert.ToDouble(a[1]), 4));
                        reccust.Fields["TotalShipped"].Value = Convert.ToSingle(dig_round(Convert.ToDouble(a[2]), 4));
                        reccust.Fields["TotalStarted"].Value = Convert.ToSingle(dig_round(Convert.ToDouble(a[3]), 4));
                        reccust.Fields["Scrap"].Value = Convert.ToSingle(dig_round(Convert.ToDouble(a[4]), 4));
                        reccust.Fields["wip"].Value = Convert.ToSingle(dig_round(Convert.ToDouble(a[5]), 4));
                        reccust.Fields["flowtime"].Value = Convert.ToSingle(dig_round(Convert.ToDouble(a[6]), 4));
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  // end if 

            if (tblname.Equals("tblRsProd"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };
                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["wid"].Value = a[0];
                        reccust.Fields["prodid"].Value = a[1];
                        reccust.Fields["TotalGoodProd"].Value = dig_round(Convert.ToDouble(a[2]), 4);
                        reccust.Fields["ShippedProd"].Value = dig_round(Convert.ToDouble(a[3]), 4);
                        reccust.Fields["GoodForAsmProd"].Value = dig_round(Convert.ToDouble(a[4]), 4);
                        reccust.Fields["ScrapInAsm"].Value = dig_round(Convert.ToDouble(a[5]), 4);
                        reccust.Fields["Scrap"].Value = dig_round(Convert.ToDouble(a[6]), 4);
                        reccust.Fields["flowtime"].Value = dig_round(Convert.ToDouble(a[7]) * ratio, 4);
                        reccust.Fields["wip"].Value = dig_round(Convert.ToDouble(a[8]), 4);
                        reccust.Fields["LTEquip"].Value = dig_round(Convert.ToDouble(a[9]) * ratio, 4);
                        reccust.Fields["LTLabor"].Value = dig_round(Convert.ToDouble(a[10]) * ratio, 4);
                        reccust.Fields["LTSetup"].Value = dig_round(Convert.ToDouble(a[11]) * ratio, 4);
                        reccust.Fields["LTRun"].Value = dig_round(Convert.ToDouble(a[12]) * ratio, 4);
                        reccust.Fields["LTWaitLot"].Value = dig_round(Convert.ToDouble(a[13]) * ratio, 4);
                        //'reccust.Fields["LTAsm"].Value = vala[14]),4);
                        //'reccust.Fields["LTgather"].Value = vala[15];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            } // end if

            if (tblname.Equals("tblRsOper"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };
                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["wid"].Value = a[0];
                        reccust.Fields["prodid"].Value = a[1];
                        reccust.Fields["opid"].Value = a[2];
                        reccust.Fields["eqsettime"].Value = dig_round(Convert.ToDouble(a[3]), 4);
                        reccust.Fields["EqRunTime"].Value = dig_round(Convert.ToDouble(a[4]), 4);
                        reccust.Fields["labsettime"].Value = dig_round(Convert.ToDouble(a[5]), 4);
                        reccust.Fields["LabRunTime"].Value = dig_round(Convert.ToDouble(a[6]), 4);
                        reccust.Fields["flowtime"].Value = dig_round(Convert.ToDouble(a[7]), 4);
                        reccust.Fields["wip"].Value = dig_round(Convert.ToDouble(a[8]), 4);
                        reccust.Fields["LTEquip"].Value = dig_round(Convert.ToDouble(a[9]), 4);
                        reccust.Fields["LTLabor"].Value = dig_round(Convert.ToDouble(a[10]), 4);
                        reccust.Fields["LTSetup"].Value = dig_round(Convert.ToDouble(a[11]), 4);
                        reccust.Fields["LTRun"].Value = dig_round(Convert.ToDouble(a[12]), 4);
                        reccust.Fields["LTWaitLot"].Value = dig_round(Convert.ToDouble(a[13]), 4);
                        reccust.Fields["VisitsPer100"].Value = dig_round(Convert.ToDouble(a[14]), 4);
                        reccust.Fields["VisitsPerGood"].Value = dig_round(Convert.ToDouble(a[15]), 4);
                        reccust.Fields["AverLotSize"].Value = dig_round(Convert.ToDouble(a[16]), 4);
                        reccust.Fields["NumSetups"].Value = dig_round(Convert.ToDouble(a[17]), 4);
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if

            if (tblname.Equals("zstblErrors"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };
                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        a[0] = a[0].Trim();
                        reccust.Fields["Level"].Value = (a[0].Length > 0 ? str2i(a[0]) : 0);
                        a[1] = a[1].Trim();
                        reccust.Fields["Table"].Value = (a[1].Length > 0 ? a[1] : "");
                        a[2] = a[2].Trim();
                        reccust.Fields["Text"].Value = (a[2].Length > 0 ? a[2] : "");
                        a[3] = a[3].Trim();
                        reccust.Fields["mpxFlag"].Value = (a[3].Length > 0 ? str2i(a[3]) : 0);
                        a[4] = a[4].Trim();
                        reccust.Fields["LaborID"].Value = (a[4].Length > 0 ? str2i(a[4]) : 0);
                        a[5] = a[5].Trim();
                        reccust.Fields["equipid"].Value = (a[5].Length > 0 ? str2i(a[5]) : 0);
                        a[6] = a[6].Trim();
                        reccust.Fields["partid"].Value = (a[6].Length > 0 ? str2i(a[6]) : 0);
                        a[7] = a[7].Trim();
                        reccust.Fields["operid"].Value = (a[7].Length > 0 ? str2i(a[7]) : 0);
                        str1 = a[8];
                        str1 = str1.Trim();
                        if (str1.Length == 0) { str1 = "0"; }
                        reccust.Fields["routeid"].Value = str1;
                        str1 = a[9];
                        str1 = str1.Trim();
                        if (str1.Length == 0) { str1 = "0"; }
                        reccust.Fields["ibomid"].Value = str1;
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //    end  if

            if (tblname.Equals("tblequip"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["equipdesc"].Value = a[0];
                        reccust.Fields["grpsiz"].Value = a[1];
                        reccust.Fields["MTF"].Value = a[2];
                        reccust.Fields["MTR"].Value = a[3];
                        reccust.Fields["OT"].Value = a[4];
                        reccust.Fields["SETUP"].Value = a[5];
                        reccust.Fields["RUN"].Value = a[6];
                        reccust.Fields["Varbility"].Value = a[7];
                        if (a[7].Trim().Length == 0)
                        {
                            a[7] = "none";
                        };
                        reccust.Fields["labordesc"].Value = a[8];

                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            } ///  end if 

            if (tblname.Equals("tblgeneral"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["TITLE"].Value = a[0];
                        reccust.Fields["TUProd"].Value = a[1];
                        reccust.Fields["TULT"].Value = a[2];
                        reccust.Fields["TUFor"].Value = a[3];
                        reccust.Fields["RTU1b"].Value = a[4];
                        reccust.Fields["RTU1c"].Value = a[5];
                        reccust.Fields["utlimit"].Value = a[6];
                        reccust.Fields["coef_v_Labor"].Value = a[7];
                        reccust.Fields["coef_v_Equip"].Value = a[8];
                        reccust.Fields["coef_v_Parts"].Value = a[9];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            } // end if 

            if (tblname.Equals("tblLABOR"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["labordesc"].Value = a[0];
                        reccust.Fields["grpsiz"].Value = a[1];
                        reccust.Fields["Abst"].Value = a[2];
                        reccust.Fields["OT"].Value = a[3];
                        reccust.Fields["SETUP"].Value = a[4];
                        reccust.Fields["RUN"].Value = a[5];
                        reccust.Fields["Varbility"].Value = a[6];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //  end if 

            if (tblname.Equals("tblProdfore"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["proddesc"].Value = a[0];
                        reccust.Fields["enddemd"].Value = a[1];
                        reccust.Fields["lotsiz"].Value = a[2];
                        reccust.Fields["transferbatch"].Value = a[3];
                        reccust.Fields["demandfac"].Value = a[4];
                        reccust.Fields["Variability"].Value = a[5];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            } //  end if 

            if (tblname.Equals("tbloper"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["opnam"].Value = a[0];
                        reccust.Fields["Percentassign"].Value = a[1];
                        reccust.Fields["EqSetupTime"].Value = a[2];
                        reccust.Fields["EqRunTime"].Value = a[3];
                        reccust.Fields["LabSetupTime"].Value = a[4];
                        reccust.Fields["LabRunTime"].Value = a[5];
                        reccust.Fields["proddesc"].Value = a[6];
                        if (a[7].Trim().Length == 0)
                        {
                            a[7] = "none";
                        };
                        reccust.Fields["equipdesc"].Value = a[7];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //  end if

            if (tblname.Equals("tblWHATIF"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["Name"].Value = a[0];
                        reccust.Fields["comm"].Value = a[1];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            } // end if


            if (Filename.Equals("lotandtbatch.res"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, "tblprodfore");
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();

                    };
                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.MoveFirst();
                        reccust.Find("prodid = " + a[0]);
                        if (!reccust.EOF)
                        {

                            if (Convert.ToSingle(a[1]) > 0)
                            {

                                globNVal = a[1];
                                globTNameE = "Product  ";
                                globTNameA = "tblprodfore";
                                globrecid = Convert.ToInt32(reccust.Fields["prodid"].Value);
                                globOVal = Convert.ToString(reccust.Fields["lotsiz"].Value);
                                globFNameE = reccust.Fields["proddesc"].Value + "  -  Lotsize";
                                globDType = SINGLE_TYPE;
                                globFNameA = "lotsiz";

                                InsertAudit();
                                reccust.Fields["lotsiz"].Value = dig_round(Math.Max(1.0, Convert.ToSingle(a[1])), 3);
                            }

                            if (Convert.ToSingle(a[2]) > 0)
                            {
                                globNVal = a[2];
                                globTNameE = "Product  ";
                                globTNameA = "tblprodfore";
                                globrecid = Convert.ToInt32(reccust.Fields["prodid"].Value);
                                globOVal = Convert.ToString(reccust.Fields["transferbatch"].Value);
                                globFNameE = reccust.Fields["proddesc"].Value + "  -  Transfer Batch Size";
                                globDType = SINGLE_TYPE;
                                globFNameA = "lotsiz";

                                InsertAudit();
                                reccust.Fields["transferbatch"].Value = dig_round(Convert.ToSingle(a[2]), 3);

                            }
                            reccust.Update();
                        }

                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if 





            if (tblname.Equals("tblOPFRTOiMPORT"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["proddesc"].Value = a[0];
                        reccust.Fields["fromoper"].Value = a[1];
                        reccust.Fields["tooper"].Value = a[2];
                        reccust.Fields["Percent"].Value = a[3];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if 

            if (tblname.Equals("tblIBOMIMPORT"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["ParentID"].Value = a[0];
                        reccust.Fields["compid"].Value = a[1];
                        reccust.Fields["UPA"].Value = a[2];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if 


            if (tblname.Equals("tblwhatifiMPORT"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["Whatif_name"].Value = a[0];
                        reccust.Fields["tablea"].Value = a[1];
                        reccust.Fields["TABLEE"].Value = a[2];
                        reccust.Fields["fielda"].Value = (null == a[3]) ? "per" : a[3];
                        reccust.Fields["FIELDe"].Value = (null == a[4]) ? "per" : a[4];
                        reccust.Fields["I_name"].Value = (null == a[5]) ? " " : a[5];

                        reccust.Fields["DataType"].Value = a[8];
                        if (((int)reccust.Fields["DataType"].Value == 1) || ((int)reccust.Fields["DataType"].Value == 8) || ((int)reccust.Fields["DataType"].Value == 10))
                        {
                            reccust.Fields["newval"].Value = a[6];
                            reccust.Fields["OLDval"].Value = a[7];
                        }
                        else
                        {
                            reccust.Fields["newval"].Value = a[6];
                            reccust.Fields["OLDval"].Value = a[7];
                        };
                        reccust.Fields["p_name"].Value = (null == a[9]) ? " " : a[9];
                        reccust.Fields["count"].Value = a[10];
                        reccust.Fields["mtype"].Value = a[11];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if 

            if (tblname.Equals("tblProdload2"))
            {
                runsqlado(" delete tblprodload2.* from tblprodload2");
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };
                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["proddesc"].Value = a[0];
                        reccust.Fields["lotsiz"].Value = a[1];
                        reccust.Fields["transferbatch"].Value = a[2];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if 

            if (tblname.Equals("tblrsPOLcA"))
            {
                DbUse.open_ado_rec(globaldb, ref reccust, tblname);
                using (StreamReader fin = new StreamReader(Filename))
                {
                    if (header != 0)
                    {
                        str2 = fin.ReadLine();
                    };

                    while (!fin.EndOfStream)
                    {
                        str2 = fin.ReadLine();
                        Input(ref str2, ref a);

                        reccust.AddNew();
                        reccust.Fields["wid"].Value = glngwid;
                        reccust.Fields["FROMcELL"].Value = get_cell_name(str2i(a[0]));
                        reccust.Fields["tocell"].Value = get_cell_name(str2i(a[1]));
                        reccust.Fields["wiplots"].Value = a[2];
                        reccust.Fields["wippieces"].Value = a[3];
                        reccust.Fields["wiptbatch"].Value = a[4];
                        reccust.Update();
                    };
                    fin.Close();
                }
                goto exit_transfer;
            }  //end if 

        }
        catch (Exception ex)
        {
            LogFiles logFiles = new LogFiles(this.username);
            logFiles.ErrorLog(ex);
        }
        finally
        {
            DbUse.CloseAdoRec(reccust);
        }


        exit_transfer:  //  label location ??

        return; //exit  Sub;
        //err_transfer:
        // msgbox("Did not load table " + tblname + char(13) + ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;

    void FillResultTables()
    {
        //on error goto Err_FillResultTables;

        //Import Text files into output tables;

        //'  full address here for file !;


        transfertext("tblRsSummary", varlocal + "\\Results.gen", -1);
        transfertext("tblRsEquip", varlocal + "\\Results.eq", -1);
        transfertext("tblRsLabor", varlocal + "\\Results.lab", -1);
        transfertext("tblRsProd", varlocal + "\\Results.prt", -1);
        transfertext("tblRsOper", varlocal + "\\Results.opr", -1);
        //transfertext("tblRsAssetUtil", varlocal + "\\Summary.RES", 0);
        // transfertext("tblRsPolca", varlocal +"\\Results.pol", -1);
        // transfertext("tblRsPolca", varlocal +"\\Results.pol1", -1);

        ADODB.Recordset recUnits = new ADODB.Recordset();
        DbUse.OpenAdoRec(globaldb, recUnits, "SELECT rtu1b FROM tblgeneral;");
        float rtu1b = float.Parse(recUnits.Fields["rtu1b"].Value.ToString());
        float factorChangeDays = Change_days();
        float ratio2 = rtu1b / factorChangeDays; //  need to get rtu1b   change hours to days then to cal.days
        float ratio = factorChangeDays;

        runsqlado("UPDATE tblequip SET tblequip.Flag = 0;");
        runsqlado("UPDATE (((tbloper INNER JOIN tblRsOper ON tbloper.OpID = tblRsOper.OpID) INNER JOIN tblequip ON tbloper.EqID = tblequip.EquipID) INNER JOIN tblgeneral ON tblequip.Flag = tblgeneral.dummylink) INNER JOIN tblRsProd ON (tblRsOper.ProdID = tblRsProd.ProdID) AND (tblRsOper.WID = tblRsProd.WID) SET tblRsProd.LTEquip = [tblrsprod].[ltequip]-[tblrsoper].[ltequip]/" + ratio2 + ", tblRsProd.LTLabor = [tblrsprod].[ltlabor]-[tblrsoper].[ltlabor]/" + ratio2 + ", tblRsProd.LTRun = [tblrsprod].[ltrun]-"
          + " [tblrsoper].[ltrun]/" + ratio2 + ", tblRsProd.LTSetup = [tblrsprod].[ltsetup]-[tblrsoper].[ltsetup]/" + ratio2 + ", tblRsProd.LTWaitLot = [tblrsprod].[ltwaitlot]-[tblrsoper].[ltwaitlot]/" + ratio2 + ", tblRsProd.LTWaitAsm = [tblRsProd].[LTWaitAsm] + [tblrsoper].[FlowTime]/" + ratio2 + " WHERE (((tblRsOper.WID)=" + glngwid + ") AND ((InStr([EquipDept],'out of area'))<>0));");

        runsqlado("DELETE  tblRsSummary.* From tblRsSummary WHERE (((tblRsSummary.WID)=" + glngwid + ")) AND  ( (Not (tblRsSummary.[Product family]) Is Null) and (len(trim(tblrssummary.[product family])) >0) );");
        runsqlado("INSERT INTO tblRsSummary ( [Product family], TotalProd, WID, TotalStarted, Scrap, FlowTime, WIP, TotalShipped ) SELECT tblprodfore.ProdDept, Sum(tblRsProd.TotalGoodProd) AS good, tblRsProd.WID, Sum([TotalGoodProd]+[scrap]) AS started, Sum(tblRsProd.Scrap) AS SumOfScrap, Sum(([FlowTime]*[totalgoodprod]))/[flowd] AS flow2, Sum(tblRsProd.WIP) AS SumOfWIP, Sum(tblRsProd.ShippedProd) AS SumOfShippedProd FROM (tblprodfore INNER JOIN tblRsProd ON tblprodfore.ProdID = tblRsProd.ProdID) INNER JOIN x1 ON "
            + " (tblprodfore.ProdDept = x1.ProdDept) AND (tblRsProd.WID = x1.WID) GROUP BY tblprodfore.ProdDept, tblRsProd.WID, x1.flowD  HAVING (((tblRsProd.WID)=" + glngwid + ") AND ((x1.flowD)>0) AND ((Len(Trim([tblprodfore].[ProdDept])))>0));");
        runsqlado("UPDATE tblRsSummary SET tblRsSummary.FlowTime = [flowtime]*" + ratio + " WHERE (((tblRsSummary.WID)= " + glngwid + "));");
        do_oper_hours();

        // return; //exit _FillResultTables:
        return; //exit  Sub;

        //Err_FillResultTables:

        /*
       if ((Err.Number  == 3297))   {
           // msgbox("Did not read all results corectly. Continuing to read other results", 0, appl_name);
            //RESUME NEXT
           };
       // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT
        */


    } // end sub;


    int find_Opername(string[] arr_name, int Count)
    {
        //on error goto Err_find_Opername;

        int i;
        string newname;
        int retval;

        retval = 0;

        newname = arr_name[Count];

        for (i = 0; i <= (Count - 1); i++)
        {
            if ((arr_name[i] == newname))
            {
                retval = i;
                return retval; //exit _find_Opername;
            };
        }; // end for

        // return; //exit _find_Opername:
        return retval; //exit  Function;

        //Err_find_Opername:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _find_Opername;


    } // end function;

    void PassEquipInput()
    {
        //on error goto Err_PassEquipInput;

        //Define variables;

        ADODB.Recordset recEquip = null;
        string strsql;
        int intcount;
        int i;
        int fstatus;
        int retval; //exit;
        string EqName;
        int no_none;

        //Set variables;
        retval = 0;
        strsql = "select tblequip.* from tblequip order by equipid;";
        DbUse.open_ado_rec(globaldb, ref recEquip, strsql);

        //Count the number of records in table;
        recEquip.MoveFirst();
        intcount = 0;
        while (!recEquip.EOF)
        {
            intcount = intcount + 1;
            recEquip.MoveNext();
        };

        recEquip.MoveFirst();

        //Redimension all arrays;

        Array.Resize(ref Eq_id, intcount);
        Array.Resize(ref eq_size, intcount);
        Array.Resize(ref mttf, intcount);
        Array.Resize(ref MTTR, intcount);
        Array.Resize(ref eq_ot, intcount);
        Array.Resize(ref Lab_id2, intcount);
        Array.Resize(ref eq_s_fact, intcount);
        Array.Resize(ref eq_r_fact, intcount);
        Array.Resize(ref eq_v_fact, intcount);
        Array.Resize(ref eq_cell, intcount);
        //  demension the arrays to passs data !!

        no_none = find_nameItem("none", 0, Labor, 0);

        Eq_count = intcount;

        //Fill all arrays;
        for (i = 0; i <= (intcount - 1); i++)
        {
            Eq_id[i] = ((int)recEquip.Fields["equipid"].Value);
            eq_size[i] = (short.Parse(recEquip.Fields["grpsiz"].Value.ToString()));
            mttf[i] = ((float)recEquip.Fields["MTF"].Value);
            MTTR[i] = ((float)recEquip.Fields["MTR"].Value);
            eq_ot[i] = ((float)recEquip.Fields["OT"].Value);
            Lab_id2[i] = (int)((Convert.IsDBNull(recEquip.Fields["Labor"].Value)) ? no_none : ((int)recEquip.Fields["Labor"].Value));
            eq_s_fact[i] = ((float)recEquip.Fields["SETUP"].Value);
            eq_r_fact[i] = ((float)recEquip.Fields["RUN"].Value);
            eq_v_fact[i] = ((float)recEquip.Fields["Varbility"].Value);
            eq_cell[i] = -1; //get_cell( (string)(recEquip.Fields["EquipDept"].Value));
            recEquip.MoveNext();
        }; // end for

        //Pass all values to DLL;
        //fstatus = iEquip_data(intcount, Eq_id, eq_size, mttf, MTTR, eq_ot, Lab_id, eq_s_fact, eq_r_fact, eq_v_fact, eq_cell);

        //if (fstatus != 0) {
        //    fDLLError = -1;
        //};




        //exit_PassEquipInput: ;

        fexit = -1;  //  global variable 
        DbUse.CloseAdoRec(recEquip);
        recEquip = null;
        return; //exit  Sub;

        //Err_PassEquipInput:
        /*  if ((Err.Number  == 6) || (Err.Number = -2147467261))   {
               //RESUME NEXT
          } else {
              if (recEquip != null)    {
                  EqName = find_item((int)(recEquip.Fields["EqID"].Value), 0, equip);
                  // msgbox("Missing or NULL value in record ? " + char(13) + char(13) + "EQUIPMENT: " + EqName, 0, appl_name);
              } else {

                  // msgbox("MISSING RECORD COMPELETLY  INTERAL ERROR !! (email error@networkdyn.com)", 0, appl_name);
                  };
              fDLLError = -1;
              // resume   return; //exit _PassEquipInput;
              };
          if (fexit   == 0)   {
              // msgbox(ErrorToString(), 0, appl_name);
              // resume   return; //exit _PassEquipInput;
          } else {
               //RESUME NEXT
              };
         * */
    } // end sub;


    void PassGeneralInput()
    {
        //on error goto Err_PassGeneralInput;


        ADODB.Recordset recGeneral = null;
        string strtable;
        int intcount;
        int fexit;
        int fstatus;
        int i;




        //Default variables;
        fexit = 0;
        fstatus = 0;
        intcount = 0;
        strtable = "select tblGeneral.* from tblgeneral";

        //Set variables and open Tools Table;
        DbUse.open_ado_rec(globaldb, ref recGeneral, strtable);

        recGeneral.MoveFirst();

        //Pass all values to DLL;
        time1 = ((float)recGeneral.Fields["RTU1b"].Value);
        time2 = ((float)recGeneral.Fields["RTU1c"].Value);
        utlimit = ((float)recGeneral.Fields["utlimit"].Value);  // (float)95.0; //  gwwd  8-11-17  worked with .95  here

        cv_l = ((float)recGeneral.Fields["Coef_v_Labor"].Value);
        cv_e = ((float)recGeneral.Fields["Coef_v_Equip"].Value);
        cv_p = ((float)recGeneral.Fields["Coef_v_Parts"].Value);



        //fstatus = iGeneral_time(time1, time2, utlimit, cv_l, cv_e, cv_p);
        //fstatus = read_stop_file();
        //if (fstatus != 0) {
        //    fDLLError = -1;
        //};


        // return; //exit _PassGeneralInput:
        if (null != recGeneral)
        {
            DbUse.CloseAdoRec(recGeneral);
        };
        return; //exit  Sub;

        //Err_PassGeneralInput:
        /*  if ((Err.Number  == 6))   {
               //RESUME NEXT
              };

          // msgbox(ErrorToString(), 0, appl_name);
          // resume   return; //exit _PassGeneralInput;
          */
    } // end sub;

    bool include(int partid)
    {

        ADODB.Recordset reccust = null;
        string str1;

        str1 = "SELECT tblprodfore.prodid FROM tblprodfore WHERE (((tblprodfore.[include])=True) AND ((tblprodfore.[prodid])= " + partid + ")); ";
        DbUse.open_ado_rec(globaldb, ref reccust, str1);
        if (!reccust.EOF)
        {
            return true;
        }
        else { return false; };


    }

    void PassIbomInput()
    {
        //on error goto Err_PassIbomInput;

        //Define variables;

        ADODB.Recordset recIbom = null;
        string strsql;
        int intcount;
        int i;
        int fstatus;
        int fexit;
        string prodname;

        //Set variables;
        fexit = 0;
        strsql = "SELECT tblibom.* FROM tblibom INNER JOIN tblprodfore ON tblibom.ParentID = tblprodfore.ProdID";

        DbUse.open_ado_rec(globaldb, ref recIbom, strsql);

        //Count the number of records in table;
        intcount = (int)recordcount(recIbom);
        Ibom_count = intcount;
        if ((intcount == 0))
        {
            return; //exit _PassIbomInput;
        };
        recIbom.MoveFirst();

        //Redimension all arrays;
        Array.Resize(ref Part_id4, intcount);
        Array.Resize(ref Comp_part_id, intcount);
        Array.Resize(ref UPA, intcount);

        //Fill all arrays;
        for (i = 0; i <= (intcount - 1); i++)
        {
            Part_id4[i] = ((int)recIbom.Fields["ParentID"].Value);
            Comp_part_id[i] = ((int)recIbom.Fields["compid"].Value);


            UPA[i] = ((float)recIbom.Fields["UPA"].Value);

            recIbom.MoveNext();
        }; // end for

        //Pass all values to DLL;
        //fstatus = iIbom_data(intcount, Part_id, Comp_part_id, UPA);
        //fstatus = read_stop_file();
        //if (fstatus != 0) {
        //    fDLLError = -1;
        //};




        // return; //exit _PassIbomInput:
        fexit = -1;
        DbUse.CloseAdoRec(recIbom);
        recIbom = null;
        return; //exit  Sub;

        //Err_PassIbomInput:
        /*  if ((Err.Number  == 6) || (Err.Number = -2147467261))   {
               //RESUME NEXT
          } else {
              if ( recIbom !=  null )   {
                  prodname = find_item(((int) recIbom.Fields["ParentID"].Value), 0, product);
                  // msgbox("Missing or NULL value in record ? " + char(13) + char(13) + "PART: " + prodname, 0, appl_name);
              } else {

                  // msgbox("MISSING RECORD COMPELETLY !! INTERAL ERROR (email error@networkdyn.com)", 0, appl_name);
                  };
              fDLLError = -1;
              // resume   return; //exit _PassIbomInput;
       
              };
         */
        /*  if (((fexit   == 0) && (Err.Number != 3021)))   {
              // msgbox(ErrorToString(), 0, appl_name);
               return; //exit _PassIbomInput;
          } else {
               return; //exit _PassIbomInput;
              };
         * */
    } // end sub;

    void PassLaborInput()
    {
        //on error goto Err_PassLaborInput;


        ADODB.Recordset recLabor = null;
        string strtable;
        int intcount;
        int fexit;
        int fstatus;
        int i;
        string labname;

        //Default variables;
        fexit = 0;
        fstatus = 0;
        intcount = 0;
        strtable = "select tblLabor.* from tbllabor order by laborid;";


        //Set variables and open Tools Table;
        DbUse.open_ado_rec(globaldb, ref recLabor, strtable);

        //Count the number of records in table;
        fexit = -10;
        recLabor.MoveFirst();
        intcount = 0;
        while (!recLabor.EOF)
        {
            intcount = intcount + 1;
            recLabor.MoveNext();
        }; // while loop 
        Lab_count = intcount;
        fexit = 0;

        //Redimension all arrays;

        Array.Resize(ref Lab_id, intcount);
        Array.Resize(ref Lab_size, intcount);
        Array.Resize(ref Lab_ot, intcount);
        Array.Resize(ref lab_abs, intcount);
        Array.Resize(ref Lab_s_fact, intcount);
        Array.Resize(ref Lab_r_fact, intcount);
        Array.Resize(ref Lab_v_fact, intcount);
        Array.Resize(ref Lab_share, intcount);


        recLabor.MoveFirst();
        //Fill all arrays;
        for (i = 0; i < (intcount); i++)
        {
            Lab_id[i] = (int)recLabor.Fields["LaborID"].Value;
            Lab_size[i] = ((float)recLabor.Fields["grpsiz"].Value);
            Lab_ot[i] = ((float)recLabor.Fields["OT"].Value);
            lab_abs[i] = ((float)recLabor.Fields["Abst"].Value);
            Lab_s_fact[i] = ((float)recLabor.Fields["SETUP"].Value);
            Lab_r_fact[i] = ((float)recLabor.Fields["RUN"].Value);
            Lab_v_fact[i] = ((float)recLabor.Fields["Varbility"].Value);
            Lab_share[i] = (((bool)recLabor.Fields["PriorityShare"].Value) == true) ? -1 : 0;
            recLabor.MoveNext();
        }; // end for

        //Pass all values to DLL;
        //' todasy   looks like we are writing the data to ascii file ???;
        //fstatus = iLabor_data(intcount, Lab_id, Lab_size, Lab_ot, lab_abs, Lab_s_fact, Lab_r_fact, Lab_v_fact, Lab_share);
        //fstatus = read_stop_file();
        //if (fstatus != 0) {
        //    fDLLError = -1;
        //};




        if (null != recLabor)
        {
            DbUse.CloseAdoRec(recLabor);
            recLabor = null;
        };
        return; //exit  Sub;

        Err_PassLaborInput:
        ;

        /*    if ((Err.Number  == 6))   {
                 //RESUME NEXT
            } else {
                if (null != recLabor)   {
                    labname = find_item(((int)recLabor.Fields["LaborID"].Value), 0, Labor);
                    // msgbox("Missing or NULL value in record ? " + char(13) + char(13) + "LABOR: " + labname, 0, appl_name);
                } else {
                    // msgbox("MISSING RECORD COMPELETLY !! INTERAL ERROR (email error@networkdyn.com)", 0, appl_name);
                };
                fDLLError = -1;
                // resume   return; //exit _PassLaborInput;

                };
            if ((Err.Number  == -2147467261))   {
                // msgbox(ErrorToString(), 0, appl_name);
                 //RESUME NEXT
                };
            if (fexit   == 0 || (Err.Number = -2147467261))   {
                // msgbox(ErrorToString(), 0, appl_name);
                 //RESUME NEXT
                };
             return;
         * */
    } // end sub;

    void do_frto_clean(int opid)
    {
        //on error goto err1;

        string str1;
        int opid1;
        ADODB.Recordset rec1 = null;

        str1 = "SELECT Min(tbloper_1.OpID) AS MinID, Min([tbloper_1].[prodfore]) AS MinprodID FROM tbloper INNER JOIN tbloper AS tbloper_1 ON (tbloper.ProdFore = tbloper_1.ProdFore) AND (tbloper.OpNam = tbloper_1.OpNam) WHERE (((tbloper.OpID)=" + opid + "));";

        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        opid1 = (int)rec1.Fields["minid"].Value;
        DbUse.CloseAdoRec(rec1);
        str1 = "UPDATE tbloperfrto INNER JOIN (tbloper INNER JOIN tbloper AS tbloper_1 ON (tbloper.ProdFore = tbloper_1.ProdFore) AND (tbloper.OpNam = tbloper_1.OpNam)) ON tbloperfrto.OpNumF = tbloper.OpID SET tbloperfrto.OpNumF = " + opid1 + " WHERE (((tbloper_1.OpID)=" + opid + "));";
        runsqlado(str1);

        str1 = "UPDATE tbloperfrto INNER JOIN (tbloper INNER JOIN tbloper AS tbloper_1 ON (tbloper.ProdFore = tbloper_1.ProdFore) AND (tbloper.OpNam = tbloper_1.OpNam)) ON tbloperfrto.OpNumT = tbloper.OpID SET tbloperfrto.OpNumT = " + opid1 + " WHERE (((tbloper_1.OpID)=" + opid + "));";
        runsqlado(str1);
        //exit1:

        return; //exit  Sub;
        //err1:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;

    void clean_all_ops()
    {
        //on error goto err_clean;unsql

        string str1;
        ADODB.Recordset rec1 = null;
        ADODB.Recordset rec2 = null;
        string strcrit;
        int operid;
        int prodid;
        int routeid;

        runsqlado("Update tblOper SET tblOper.Flag = 0;");
        runsqlado("UPDATE tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID SET tbloper.Flag = 1;");

        DbUse.open_ado_rec(globaldb, ref rec2, "tbloper");

        strcrit = "[flag] = 0";
        rec2.Filter = strcrit;
        while (!rec2.EOF)
        {
            operid = (int)rec2.Fields["opid"].Value;
            del_op_res((operid));
            rec2.MoveNext(); //'xx'(strcrit);
        };
        DbUse.CloseAdoRec(rec2);
        runsqlado("delete tbloper.* FROM tbloper WHERE ((tbloper.Flag=0));");

        runsqlado("Update tblOperFrTo SET tblOperFrTo.Flag = 0;");
        runsqlado("Update (tbloperfrto INNER JOIN tbloper ON tbloperfrto.OpNumF = tbloper.OpID) INNER JOIN tbloper AS tbloper_1 ON tbloperfrto.OpNumT = tbloper_1.OpID SET tbloperfrto.Flag = 1;");
        DbUse.open_ado_rec(globaldb, ref rec1, "tbloperfrto");
        strcrit = "[flag] = 0";
        rec1.Filter = strcrit;
        while (!rec1.EOF)
        {
            routeid = (int)rec1.Fields["recid"].Value;
            del_route_res((routeid));
            rec1.MoveNext(); // 'xx'(strcrit);
        }; // end while
        DbUse.CloseAdoRec(rec1);
        runsqlado("delete tbloperFRTO.* FROM tbloperFRTO WHERE ((tbloperFRTO.Flag=0));");

        //exit_clean:
        return; //exit  Sub;
        //err_clean:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_clean;
    } // end sub;





    void PassOper()
    {
        //on error goto Err_PassOper;

        //Define variables;

        ADODB.Recordset recoper = null;
        string strsql;
        int fexit;
        int fstatus;
        int intcount;
        int i;
        int j;
        int op;
        int size;

        string str_v;

        string[] strOpName = null;
        int[] intOPname = null;
        int oldPartid;
        int opcount;
        float ival;
        int slen;
        string prodname;
        int eqnone;

        STOPMPX = 0;


        //checkDOCKSTOCK();

        eqnone = find_nameItem("NONE", 0, equip, 0);
        runsqlado("UPDATE tbloper SET tbloper.EqID = " + eqnone + " WHERE ((tbloper.EqID)=0);");

        qstrArry[0] = "Junk";
        qstrArry[1] = "PercentAssign";
        qstrArry[2] = "EqSetupTime";
        qstrArry[3] = "EqRunTime";
        qstrArry[4] = "LabSetupTime";
        qstrArry[5] = "LabRunTime";
        qstrArry[6] = "Equip";
        qstrArry[7] = getname("O1");
        qstrArry[8] = getname("O2");
        qstrArry[9] = getname("O3");
        qstrArry[10] = getname("O4");
        qstrArry[11] = getname("G1");
        qstrArry[12] = getname("G2");
        qstrArry[13] = getname("G3");
        qstrArry[14] = getname("G4");
        qstrArry[15] = getname("L1");
        qstrArry[16] = getname("L2");
        qstrArry[17] = getname("L3");
        qstrArry[18] = getname("L4");
        qstrArry[19] = getname("E1");
        qstrArry[20] = getname("E2");
        qstrArry[21] = getname("E3");
        qstrArry[22] = getname("E4");
        qstrArry[23] = getname("P1");
        qstrArry[24] = getname("P2");
        qstrArry[25] = getname("P3");
        qstrArry[26] = getname("P4");
        qstrArry[27] = "demand";
        qstrArry[28] = "lotsize";
        qstrArry[29] = "transferbatch";
        qstrArry[30] = "variability";

        qstrArry[31] = "eqSetupPiece";
        qstrArry[32] = "eqSetupTbatch";
        qstrArry[33] = "eqRunLot";
        qstrArry[34] = "eqRunTbatch";
        qstrArry[35] = "labSetupPiece";
        qstrArry[36] = "labSetupTbatch";
        qstrArry[37] = "labRunLot";
        qstrArry[38] = "labRunTbatch";


        //Default variables;
        fexit = 0;
        intcount = 0;
        oldPartid = 0;
        opcount = 0;

        runsqlado("UPDATE tblOper SET tblOper.Flag = 4;");
        runsqlado("UPDATE tblOper SET tblOper.Flag = 0 WHERE (((tblOper.OpNam)='DOCK'));");
        runsqlado("UPDATE tblOper SET tblOper.Flag = 1 WHERE (((tblOper.OpNam)='STOCK'));");
        runsqlado("UPDATE tblOper SET tblOper.Flag = 2 WHERE (((tblOper.OpNam)='SCRAP'));");

        //Set variables;
        strsql = "SELECT DISTINCTROW qfltopercalc.include, qfltopercalc.Flag, qfltOperCalc.OpID, qfltOperCalc.OpNam, qfltOperCalc.ProdFore as partfore, qfltOperCalc.V1, qfltOperCalc.V2, qfltOperCalc.V3, qfltOperCalc.V4, qfltOperCalc.V5, qfltOperCalc.V6, qfltOperCalc.V7, qfltOperCalc.V8, qfltOperCalc.V9, qfltOperCalc.V10, qfltOperCalc.V11, qfltOperCalc.V12, qfltOperCalc.V13, qfltOperCalc.V14, qfltOperCalc.V15, qfltOperCalc.V16, qfltOperCalc.V17, qfltOperCalc.V18, qfltOperCalc.V19, qfltOperCalc.V20, qfltOperCalc.V21, "
            + "qfltOperCalc.v22, qfltOperCalc.V23, qfltOperCalc.V24, qfltOperCalc.V25, qfltOperCalc.V26, qfltOperCalc.V27, qfltOperCalc.V28, qfltOperCalc.V29, qfltOperCalc.V30, qfltOperCalc.V31, qfltOperCalc.V32, qfltOperCalc.V33, qfltOperCalc.V34, qfltOperCalc.V35, qfltOperCalc.V36, qfltOperCalc.V37, qfltOperCalc.V38 FROM qfltOperCalc order by prodfore, flag, opnam, opid;";


        DbUse.open_ado_rec(globaldb, ref recoper, strsql);

        //Get the number of records in table;
        recoper.MoveFirst();
        recoper.MoveLast();
        intcount = (int)recordcount(recoper);
        recoper.MoveFirst();


        //Redimension all arrays;

        Array.Resize(ref strOpName, intcount);
        Array.Resize(ref intOPname, (intcount));
        Array.Resize(ref OP_name, (intcount));
        Array.Resize(ref op_id, (intcount));
        Array.Resize(ref Part_id2, (intcount));
        Array.Resize(ref Eq_id2, (intcount));
        Array.Resize(ref Pct_Assgn, (intcount));
        Array.Resize(ref eq_setup, (intcount));
        Array.Resize(ref eq_run, (intcount));
        Array.Resize(ref Lab_setup, (intcount));
        Array.Resize(ref Lab_run, (intcount));

        Array.Resize(ref eq_SetupPiece, (intcount));
        Array.Resize(ref eq_SetupTbatch, (intcount));
        Array.Resize(ref eq_RunLot, (intcount));
        Array.Resize(ref eq_RunTbatch, (intcount));
        Array.Resize(ref lab_SetupPiece, (intcount));
        Array.Resize(ref lab_SetupTbatch, (intcount));
        Array.Resize(ref lab_RunLot, (intcount));
        Array.Resize(ref lab_RunTbatch, (intcount));

        Op_count = intcount;
        //Fill all arrays;
        for (i = 0; i <= (intcount - 1); i++)
        {

            if ((oldPartid != (int)recoper.Fields["partfore"].Value))
            {
                opcount = 0;
                oldPartid = (int)recoper.Fields["partfore"].Value;
            };


            strOpName[opcount] = (string)recoper.Fields["opnam"].Value;
            intOPname[opcount] = (int)recoper.Fields["opid"].Value;

            // gwwd  
            Part_err_id = (int)recoper.Fields["partfore"].Value;
            Oper_err_id = intOPname[opcount];
            Part_err_name = Part_err_name = find_item(Part_err_id, 0, product);
            Oper_err_name = (string)recoper.Fields["opnam"].Value;


            j = find_Opername(strOpName, opcount);
            if ((j != 0))
            {
                OP_name[i] = intOPname[j];
            }
            else
            {
                OP_name[i] = (int)recoper.Fields["opid"].Value;
            };
            opcount = opcount + 1;

            op_id[i] = (int)recoper.Fields["opid"].Value;
            Part_id2[i] = (int)recoper.Fields["partfore"].Value;
            Eq_id2[i] = (int)recoper.Fields["V6"].Value;


            str_v = (string)recoper.Fields["v1"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            Pct_Assgn[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["v2"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            eq_setup[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["v3"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            eq_run[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["v4"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            Lab_setup[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["v5"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            Lab_run[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["v31"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            eq_SetupPiece[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["V32"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            eq_SetupTbatch[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["V33"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            eq_RunLot[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["V34"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            eq_RunTbatch[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["V35"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            lab_SetupPiece[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["V36"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            lab_SetupTbatch[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["V37"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            lab_RunLot[i] = Convert.ToSingle(Math.Max(0.0, ival));

            str_v = (string)recoper.Fields["V38"].Value;
            ival = eval_all(str_v, recoper);
            //on ERROR 2482 ... call for translation .fields("!!").Value;
            lab_RunTbatch[i] = Convert.ToSingle(Math.Max(0.0, ival));



            recoper.MoveNext();
        }; // end for


        if (STOPMPX == -1)
        {
            fDLLError = -1;
            return; //exit  Sub;
        };

        //Pass all values to DLL;
        //fstatus = iOper_data(intcount, OP_name, op_id, Part_id2, Eq_id2, Pct_Assgn, eq_setup, eq_SetupTbatch, eq_SetupPiece, eq_RunLot, eq_RunTbatch, eq_run, Lab_setup, lab_SetupTbatch, lab_SetupPiece, lab_RunLot, lab_RunTbatch, Lab_run);
        //fstatus = read_stop_file();
        //if (fstatus != 0) {
        //    fDLLError = -1;
        //};


        //  return memory

        //Redimension all arrays;




        // return; //exit _PassOper:
        fexit = -1;
        if (null != recoper)
        {
            DbUse.CloseAdoRec(recoper);
            recoper = null;
        };
        return; //exit  Sub;

        //Err_PassOper:
        /* if ((Err.Number  == 6) || (Err.Number = -2147467261))   {
              //RESUME NEXT

         } else {
             if (null != recoper)   {
                 prodname = find_item((int)(recoper.Fields["partfore"].Value), 0, product);
                 // msgbox("Missing or NULL value in record ? " + char(13) + char(13) + "PRODUCT: " + prodname + "    OPERATION: " + recoper.Fields["opnam"].Value, 0, appl_name);
             } else {
                 // msgbox("MISSING RECORD COMPELETLY !! INTERAL ERROR (email error@networkdyn.com)", 0, appl_name);

                 };
             fDLLError = -1;
             // resume   return; //exit _PassOper;


             };
         if (fexit   == -1)   {
              //RESUME NEXT
         } else {
             // msgbox(ErrorToString(), 0, appl_name);
             // resume   return; //exit _PassOper;
             };
         * */
    } // end sub;

    float eval_all(string str1, ADODB.Recordset recC)
    {
        //on error goto err_evalall;

        float val1;
        string[] qvalues;
        string opnam;
        string prodname;
        string strmsg;
        ADODB.Recordset recerror = null;
        float retval;

        qvalues = new string[40];

        val1 = eval2(str1, -1);
        if ((val1 != -999 && val1 != -777))
        {
            return val1; //exit  Function;
        };

        qvalues[0] = "0";
        qvalues[1] = (string)CDbl_ndi((string)recC.Fields["v1"].Value);
        qvalues[2] = (string)CDbl_ndi((string)recC.Fields["v2"].Value);
        qvalues[3] = (string)CDbl_ndi((string)recC.Fields["v3"].Value);
        qvalues[4] = (string)CDbl_ndi((string)recC.Fields["v4"].Value);
        qvalues[5] = (string)CDbl_ndi((string)recC.Fields["v5"].Value);
        qvalues[6] = Convert.ToString(recC.Fields["V6"].Value);
        qvalues[7] = (string)CDbl_ndi((string)recC.Fields["V7"].Value);
        qvalues[8] = (string)CDbl_ndi((string)recC.Fields["V8"].Value);
        qvalues[9] = (string)CDbl_ndi((string)recC.Fields["V9"].Value);
        qvalues[10] = (string)CDbl_ndi((string)recC.Fields["V10"].Value);
        qvalues[11] = (string)CDbl_ndi((string)recC.Fields["V11"].Value);
        qvalues[12] = (string)CDbl_ndi((string)recC.Fields["V12"].Value);
        qvalues[13] = (string)CDbl_ndi((string)recC.Fields["V13"].Value);
        qvalues[14] = (string)CDbl_ndi((string)recC.Fields["V14"].Value);
        qvalues[15] = (string)CDbl_ndi((string)recC.Fields["V15"].Value);
        qvalues[16] = (string)CDbl_ndi((string)recC.Fields["V16"].Value);
        qvalues[17] = (string)CDbl_ndi((string)recC.Fields["V17"].Value);
        qvalues[18] = (string)CDbl_ndi((string)recC.Fields["V18"].Value);
        qvalues[19] = (string)CDbl_ndi((string)recC.Fields["V19"].Value);
        qvalues[20] = (string)CDbl_ndi((string)recC.Fields["V20"].Value);
        qvalues[21] = (string)CDbl_ndi((string)recC.Fields["V21"].Value);
        qvalues[22] = (string)CDbl_ndi((string)recC.Fields["V22"].Value);
        qvalues[23] = (string)CDbl_ndi((string)recC.Fields["V23"].Value);
        qvalues[24] = (string)CDbl_ndi((string)recC.Fields["V24"].Value);
        qvalues[25] = (string)CDbl_ndi((string)recC.Fields["V25"].Value);
        qvalues[26] = (string)CDbl_ndi((string)recC.Fields["V26"].Value);
        qvalues[28] = Convert.ToString((float)recC.Fields["V28"].Value);
        qvalues[30] = Convert.ToString((float)recC.Fields["v30"].Value);

        qvalues[31] = (string)CDbl_ndi((string)recC.Fields["v31"].Value);
        qvalues[32] = (string)CDbl_ndi((string)recC.Fields["V32"].Value);
        qvalues[33] = (string)CDbl_ndi((string)recC.Fields["V33"].Value);
        qvalues[34] = (string)CDbl_ndi((string)recC.Fields["V34"].Value);
        qvalues[35] = (string)CDbl_ndi((string)recC.Fields["V35"].Value);
        qvalues[36] = (string)CDbl_ndi((string)recC.Fields["V36"].Value);
        qvalues[37] = (string)CDbl_ndi((string)recC.Fields["V37"].Value);
        qvalues[38] = (string)CDbl_ndi((string)recC.Fields["V38"].Value);

        val1 = CalcNumber(str1, ref qstrArry, ref qvalues, 39);


        // greg hard coded numbers
        if ((val1 == -999) || (val1 == -777))
        {

            opnam = (string)recC.Fields["opnam"].Value;
            prodname = find_item(((int)recC.Fields["partfore"].Value), 0, product);
            // msgbox("Cannot Solve number for Product " + prodname + " and Operation " + opnam, 0, appl_name);
            //add error here;
            DbUse.open_ado_rec(globaldb, ref recerror, "zstblerrors");
            strmsg = "Error in solving for number -- ";
            strmsg = strmsg + "Product: " + prodname + " ";
            strmsg = strmsg + "Operation: " + opnam + " ";
            recerror.AddNew();
            recerror.Fields["Level"].Value = 0;
            calc_return |= ERR_FLAG;
            recerror.Fields["Table"].Value = "Operation Table";
            recerror.Fields["Text"].Value = strmsg;
            recerror.Fields["Wid"].Value = glngwid;
            recerror.Update();
            STOPMPX = -1;
        };

        //eval_all = val1;

        // return; //exit _evalall:
        if (null != recerror)
        {
            DbUse.CloseAdoRec(recerror);
            recerror = null;
        };
        return val1; //exit  Function;
        //err_evalall:
        /* if ((Err.Number  == 999))   {
             return -999;
             };
         */
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end function;

    void PassOperFrTo()
    {
        //on error goto Err_PassOperFrTo;

        //Define variables;
        string opnam;
        string prodname;

        ADODB.Recordset recOpFrTo = null;
        string[] qvalues;
        string strsql;
        int intcount;
        int fstatus;
        int fexit;
        int i;
        string str_v;
        int size;
        float ival;
        int slen;

        qvalues = new string[40];

        STOPMPX = 0;

        //Default variables;
        fstatus = 0;
        fexit = 0;
        i = 0;

        FixDuplicateOperNameRoutings();

        //Set variables;
        strsql = "SELECT tbloperfrto.RecID, tbloperfrto.PartFore, tbloperfrto.OpNumF, tbloperfrto.OpNumT, tbloperfrto.Per, qfltopercalc.opnam, qfltOperCalc.V1, qfltOperCalc.V2, qfltOperCalc.V3, qfltOperCalc.V4, qfltOperCalc.V5, qfltOperCalc.V6, qfltOperCalc.V7, qfltOperCalc.V8, qfltOperCalc.V9, qfltOperCalc.V10, qfltOperCalc.V11, qfltOperCalc.V12, qfltOperCalc.V13, qfltOperCalc.V14, qfltOperCalc.V15, qfltOperCalc.V16, qfltOperCalc.V17, qfltOperCalc.V18, qfltOperCalc.V19, qfltOperCalc.V20, qfltOperCalc.V21,"
      + "qfltOperCalc.V22, qfltOperCalc.V23, qfltOperCalc.V24, qfltOperCalc.V25, qfltOperCalc.V26, qfltOperCalc.V27, qfltOperCalc.V28, qfltOperCalc.v29,  qfltOperCalc.V30, qfltOperCalc.V31, qfltOperCalc.V32, qfltOperCalc.V33, qfltOperCalc.V34, qfltOperCalc.V35, qfltOperCalc.V36, qfltOperCalc.V37, qfltOperCalc.V38 FROM (qfltOperCalc INNER JOIN tbloperfrto ON qfltOperCalc.OpID = tbloperfrto.OpNumF) INNER JOIN tblprodfore ON tbloperfrto.PartFore = tblprodfore.ProdID order by partfore, opnumf;";
        DbUse.open_ado_rec(globaldb, ref recOpFrTo, strsql);

        //Count the number of records;
        intcount = (int)recordcount(recOpFrTo);
        recOpFrTo.MoveFirst();

        //Redimension all arrays;

        Array.Resize(ref Part_id3, (intcount));
        Array.Resize(ref from_op_id, (intcount));
        Array.Resize(ref to_op_id, (intcount));
        Array.Resize(ref Pct_route, (intcount));

        Route_count = intcount;

        //Fill all arrays;
        for (i = 0; i <= (intcount - 1); i++)
        {

            Part_id3[i] = (int)recOpFrTo.Fields["partfore"].Value;
            from_op_id[i] = (int)recOpFrTo.Fields["OpNumF"].Value;
            to_op_id[i] = (int)recOpFrTo.Fields["OpNumT"].Value;

            // gwwd  
            Part_err_id = (int)recOpFrTo.Fields["partfore"].Value;
            Oper_err_id = -1 * (int)recOpFrTo.Fields["OpNumF"].Value;
            Part_err_name = find_item(Part_err_id, 0, product);
            Oper_err_name = find_item((int)recOpFrTo.Fields["OpNumF"].Value, Part_err_id, oper);

            str_v = recOpFrTo.Fields["per"].Value + "                                                           ";
            ival = eval_all(str_v, recOpFrTo);
            Pct_route[i] = Convert.ToSingle(Math.Max(0.0, ival));

            if (STOPMPX == -1)
            {
                fDLLError = -1;
                return; //exit  Sub;
            };

            recOpFrTo.MoveNext();
        }; // end for

        //Pass all values to DLL;
        //fstatus = iRoute_data(intcount, Part_id, from_op_id, to_op_id, Pct_route);
        //fstatus = read_stop_file();
        //if (fstatus != 0) {
        //    fDLLError = -1;
        //};


        //  return memory 
        //Redimension all arrays;




        //exit_PassOperFrTo:
        fexit = -1;
        if (null != recOpFrTo)
        {
            DbUse.CloseAdoRec(recOpFrTo);
            recOpFrTo = null;
        };
        return; //exit  Sub;

        //Err_PassOperFrTo:
        /* if ((Err.Number  == -2147467261))   {
              //RESUME NEXT
             };
         if ((Err.Number  == 94))   {
             if (null != recOpFrTo)   {
                 opnam = find_item(((int)recOpFrTo.Fields["OpNumF"].Value), 0, oper);
                 prodname = find_item(((int)recOpFrTo.Fields["partfore"].Value), 0, product);
                 // msgbox("Missing or NULL value in record ? " + char(13) + char(13) + "PRODUCT: " + prodname + "    OPERATION: " + opnam, 0, appl_name);
             } else {

                 // msgbox("MISSING RECORD COMPELETLY !! INTERAL ERROR (email error@networkdyn.com)", 0, appl_name);
                 };
             fDLLError = -1;
             // resume   return; //exit _PassOperFrTo;

         } else {
             // msgbox(ErrorToString(), 0, appl_name);
             // resume   return; //exit _PassOperFrTo;
             };
         * */
    } // end sub;



    void PassProdInfo()
    {
        //on error goto Err_PassProdInfo;

        //Define variables;

        ADODB.Recordset recprod = null;
        int fexit;
        int fstatus;
        int i;
        int intcount;
        string strsql;
        string prodname;
        ADODB.Recordset rec1 = null;
        string str1;
        int ret;
        string save_str;



        str1 = "SELECT tblprodfore.TransferBatch, tbloper.eqRunLot, tbloper.labRunLot FROM tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID WHERE (((tblprodfore.TransferBatch)>0 And (tblprodfore.TransferBatch) <> [lotsiz]) AND ((tbloper.eqRunLot)<>'0')) OR (((tblprodfore.TransferBatch)>0 And (tblprodfore.TransferBatch)<>[lotsiz]) AND ((tbloper.labRunLot)<>'0'));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.EOF)
        {
            if (inOPT == -1)
            {
                //' displayMessage(("Transfer batches are not compatable with 'run time per lot'.  The run time per lot will be moved to the standard equipment and labor setup time."));
            }
            else
            {
                //'ret = // msgbox("Transfer batches are not compatable with 'run time per lot'.  The run time per lot will be moved to the standard equipment and labor setup time.   Do you want to do this and continue?", MsgBoxStyle.YesNo, appl_name)
                //'if (ret  == // msgboxResult.No)   {
                //'    fDLLError = -1;
                //'     return; //exit  Sub;
                //'    };
            };
            if (glngwid > 0)
            {
                str1 = "SELECT tblprodfore.TransferBatch, tblprodfore.proddesc, tbloper.opnam, tbloper.opid, tbloper.eqRunLot, tbloper.eqsetuptime FROM tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID WHERE (((tblprodfore.TransferBatch)>0 And (tblprodfore.TransferBatch)<>[lotsiz]) AND ((tbloper.eqRunLot)<>'0'));";
                DbUse.open_ado_rec(globaldb, ref rec1, str1);
                while (!rec1.EOF)
                {
                    // make 2 whatif recs;
                    // from cur to 0           eqrunlot;
                    save_str = (string)rec1.Fields["eqrunlot"].Value;
                    globNVal = "0";
                    globTNameE = "Operations ";
                    globTNameA = "tbloper";
                    globrecid = (int)rec1.Fields["opid"].Value;
                    globOVal = (string)rec1.Fields["eqrunlot"].Value;
                    globFNameE = rec1.Fields["proddesc"].Value + " - " + rec1.Fields["opnam"].Value + "  -  Equipment run time Per lot";
                    globDType = TEXT_TYPE;
                    globFNameA = "Eqrunlot";
                    fstatus = InsertAudit();


                    // from cur to cur + xxx   eqsetlot;
                    globNVal = rec1.Fields["EqSetupTime"].Value + " + " + save_str;
                    globTNameE = "Operations ";
                    globTNameA = "tbloper";
                    globrecid = (int)rec1.Fields["opid"].Value;
                    globOVal = (string)rec1.Fields["EqSetupTime"].Value;
                    globFNameE = rec1.Fields["proddesc"].Value + " - " + rec1.Fields["opnam"].Value + "  -  Equipment setup time Per lot";
                    globDType = TEXT_TYPE;
                    globFNameA = "Eqsetuptime";
                    fstatus = InsertAudit();
                    rec1.MoveNext();
                };  // end while

                str1 = "SELECT tblprodfore.TransferBatch, tblprodfore.proddesc, tbloper.opnam, tbloper.opid, tbloper.labsetuptime, tbloper.labRunLot FROM tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID WHERE (((tblprodfore.TransferBatch)>0 And (tblprodfore.TransferBatch)<>[lotsiz]) AND ((tbloper.labRunLot)<>'0'));";
                DbUse.open_ado_rec(globaldb, ref rec1, str1);
                while (!rec1.EOF)
                {
                    // make 2 whatif recs;
                    // from cur to 0   eqrunlot;
                    save_str = (string)rec1.Fields["labrunlot"].Value;
                    globNVal = "0";
                    globTNameE = "Operations ";
                    globTNameA = "tbloper";
                    globrecid = (int)rec1.Fields["opid"].Value;
                    globOVal = (string)rec1.Fields["labrunlot"].Value;
                    globFNameE = rec1.Fields["proddesc"].Value + " - " + rec1.Fields["opnam"].Value + "  -  Labor run time Per lot";
                    globDType = TEXT_TYPE;
                    globFNameA = "labrunlot";
                    fstatus = InsertAudit();


                    // from cur to cur + xxx   labsetlot;
                    globNVal = rec1.Fields["LabSetupTime"].Value + " + " + save_str;
                    globTNameE = "Operations ";
                    globTNameA = "tbloper";
                    globrecid = (int)rec1.Fields["opid"].Value;
                    globOVal = (string)rec1.Fields["LabSetupTime"].Value;
                    globFNameE = rec1.Fields["proddesc"].Value + " - " + rec1.Fields["opnam"].Value + "  -  Labor setup time Per lot";
                    globDType = TEXT_TYPE;
                    globFNameA = "labsetuptime";
                    fstatus = InsertAudit();
                    rec1.MoveNext();
                };  //  end while 
                DbUse.CloseAdoRec(rec1);
                runsqlado("UPDATE tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID SET tbloper.EqSetupTime = [tbloper].[eqsetuptime] + '+' + [tbloper].[eqrunlot], tbloper.eqRunLot = '0' WHERE (((tblprodfore.TransferBatch)>0;And (tblprodfore.TransferBatch)<>[lotsiz]) AND ((tbloper.eqRunLot)<>'0'));");
                runsqlado("UPDATE tbloper INNER JOIN tblprodfore ON tbloper.ProdFore = tblprodfore.ProdID SET tbloper.LabSetupTime = [tbloper].[labsetuptime] + ' + ' + [tbloper].[labrunlot], tbloper.labRunLot = '0'  WHERE (((tblprodfore.TransferBatch)>0 And (tblprodfore.TransferBatch)<>[lotsiz]) AND ((tbloper.labRunLot)<>'0'));");
            };
        };


        //Set variables;
        fexit = 0;
        strsql = "SELECT tblProdFore.* FROM tblprodfore";

        DbUse.open_ado_rec(globaldb, ref recprod, strsql);

        //Count the number of records;
        intcount = recordcount(recprod);
        recprod.MoveFirst();


        //Redimension all arrays;

        Array.Resize(ref Part_id, (intcount));
        Array.Resize(ref demand, (intcount));
        Array.Resize(ref lotsize, (intcount));
        Array.Resize(ref tbatch, (intcount));
        Array.Resize(ref part_d_fact, (intcount));
        Array.Resize(ref part_v_fact, (intcount));
        Array.Resize(ref tbg, (intcount));

        Prod_count = intcount;

        //Fill all arrays;
        for (i = 0; i <= (intcount - 1); i++)
        {
            Part_id[i] = ((int)recprod.Fields["prodid"].Value);
            demand[i] = ((float)recprod.Fields["enddemd"].Value);
            lotsize[i] = (((float)recprod.Fields["lotsiz"].Value) * ((float)recprod.Fields["lotsizefac"].Value));
            tbatch[i] = ((float)recprod.Fields["transferbatch"].Value);
            part_d_fact[i] = ((float)recprod.Fields["demandfac"].Value);
            part_v_fact[i] = ((float)recprod.Fields["Variability"].Value);
            tbg[i] = (((bool)recprod.Fields["TBatchGather"].Value) == true) ? 0 : -1;
            recprod.MoveNext();
        }; // end for

        //Pass all values to DLL;
        //fstatus = iParts_data(intcount, Part_id, demand, lotsize, tbatch, part_d_fact, part_v_fact, tbg);
        //fstatus = read_stop_file();
        //if (fstatus != 0) {
        //    fDLLError = -1;
        //};

        //  return memory
        //Redimension all arrays;




        // return; //exit _PassProdInfo:
        fexit = -1;
        if (null != recprod)
        {
            DbUse.CloseAdoRec(recprod);
            recprod = null;
        };
        if (null != rec1)
        {
            DbUse.CloseAdoRec(rec1);
            rec1 = null;
        };
        return; //exit  Sub;

        //Err_PassProdInfo:
        /*   if ((Err.Number  == 6) || (Err.Number = -2147467261))   {
                //RESUME NEXT
           } else {
               if (null != recprod)   {
                   prodname = find_item(((int)recprod.Fields["prodid"].Value), 0, product);
                   // msgbox("Missing or NULL value in record ? " + char(13) + char(13) + "PRODUCT: " + prodname, 0, appl_name);
               } else {

                   // msgbox("MISSING RECORD COMPELETLY !! INTERAL ERROR (email error@networkdyn.com)", 0, appl_name);

               };
               fDLLError = -1;
               // resume   return; //exit _PassProdInfo;

               };
          
           if (fexit   == 0)   {
               // msgbox(ErrorToString(), 0, appl_name);
                //RESUME NEXT
           } else {
                //RESUME NEXT
               };
         */
    } // end sub;

    void ProcessErrorCode(int fCALL, int lngError, int lngLaborID, int lngEquipID, int lngProdID, int lngOpID, int lngRouteId, int lngIbomID)
    {
        //on error goto Err_ProcessErrorCode;

        //Define variables;
        string strmsg;
        string strmsg2;
        string strlab;
        string streq;
        string strprod;
        string stroper;
        string strroute;
        string stribom1;
        string stribom2;

        strlab = find_item(lngLaborID, 0, Labor);
        streq = find_item(lngEquipID, 0, equip);
        strprod = find_item(lngProdID, 0, product);
        stroper = find_item(lngOpID, lngProdID, oper);
        strroute = find_item(lngOpID, lngProdID, route);
        stribom1 = find_item(lngIbomID, 0, ibom);

        strmsg2 = "\n" + "\r";
        if ((strlab.Length > 0))
        {
            strmsg2 = strmsg2 + "Labor group: " + strlab + "\n" + "\r";
        };
        if ((streq.Length > 0))
        {
            strmsg2 = strmsg2 + "Equipment group: " + streq + "\n" + "\r";
        };
        if ((strprod.Length > 0))
        {
            strmsg2 = strmsg2 + "Product name: " + strprod + "\n" + "\r";
        };
        if ((stroper.Length > 0))
        {
            strmsg2 = strmsg2 + "Operation: " + stroper + "\n" + "\r";
        };
        if ((strroute.Length > 0))
        {
            strmsg2 = strmsg2 + "Routing operation: " + strroute + "\n" + "\r";
        };
        if ((stribom1.Length > 0))
        {
            strmsg2 = strmsg2 + "IBOM Part: " + stribom1 + "\n" + "\r";
        };

        //Process error code;
        strmsg = "";

        switch (lngError)
        {
            case (3):
                {
                    strmsg = "'@' cannot be used in a name.  '@' is being replaced with '_'";
                }
                break;
            case (5):
                {
                    strmsg = "EMAIL error@networkdyn.com  .  OPERATING SYSTEM I/O UN-GET FAILS";
                }
                break;
            case (6):
                {
                    strmsg = "Number of operations for a product exceeds avilable limit.  The routing equations cannot be solved due to lack of memory.";
                }
                break;
            case (7):
                {
                    strmsg = "Number of types of equipment exceeds available limit.  Some internal routing equations cannot be solved due to lack of memory.";
                }
                break;
            case (15):
                {
                    strmsg = "Error Invoking DOS Command processor  Command.com.  Possible sources: Insufficient Memory, file Command.com not found.";
                }
                break;
            case (21):
                {
                    strmsg = "Routing cannot be solved for a Product";
                }
                break;
            case (26):
                {
                    strmsg = "EMAIL error@networkdyn.com .  CANNOT FIND 'TO' OPERATION listed as a 'FROM' OPERATION. ";
                }
                break;
            case (28):
                {
                    strmsg = "Error while reading.  Did Not Read Model.";
                }
                break;
            case (29):
                {
                    strmsg = "MPX Did Not Write Model";
                }
                break;
            case (32):
                {
                    strmsg = "A File cannot be read/created.  The name given is a BAD FILE NAME  (meaning it refers to non-existant directories or files).";
                }
                break;
            case (34):
                {
                    strmsg = "A File cannot be created.  The name given is a BAD FILE NAME  (meaning it refers to non-existant directories or files).";
                }
                break;
            case (35):
                {
                    strmsg = "Deleting Labor group will cause one or more Equipment groups to have no (i.e. NONE) labor assigned";
                }
                break;
            case (36):
                {
                    strmsg = "Deleting Equipment group will cause one or more operations to have no Equipment assigned to it.";
                }
                break;
            case (37):
                {
                    strmsg = "Deleting a Product will cause Assembly Product to lose a component and all associated subcomponents";
                }
                break;
            case (38):
                {
                    strmsg = "OPERATION Records can not be found and deleted. ";
                }
                break;
            case (39):
                {
                    strmsg = "ROUTING Records can not be found and deleted.";
                }
                break;
            case (40):
                {
                    strmsg = "Component Records can not be found and deleted.";
                }
                break;
            case (41):
                {
                    strmsg = "Mean Time To Failure for an equipment group is too small. Please change value.";
                }
                break;
            case (42):
                {
                    strmsg = "No Equipment assigned for an operation.";
                }
                break;
            case (43):
                {
                    strmsg = "Record cannot be found and deleted.";
                }
                break;
            case (44):
                {
                    strmsg = "Record Delete Code is Inconsistant";
                }
                break;
            case (47):
                {
                    strmsg = "EMAIL error@networkdyn.com";
                }
                break;
            case (48):
                {
                    strmsg = "Numerical Problems.  Equipment Variability Routing cannot be solved";
                }
                break;
            case (51):
                {
                    strmsg = "A Product is a component of an assembly which a component of itself (IBOM LOOP).";
                }
                break;
            case (52):
                {
                    strmsg = "CANNOT FIND Product.";
                }
                break;
            case (53):
                {
                    strmsg = "Product cannot be found ?";
                }
                break;
            case (54):
                {
                    strmsg = "Cannot Find Product to use as IBOM component in structure";
                }
                break;
            case (55):
                {
                    strmsg = "Could not Solve for Total Production.  Using End Use Demand as Production Level";
                }
                break;
            case (56):
                {
                    strmsg = "Transfer Batch Size > Lotsize.  Transfer Batch Size should be -1 or less than or equal to Lotsize.";
                }
                break;
            case (57):
                {
                    strmsg = "One or more Parts is a Component of itself.";
                }
                break;
            case (61):
                {
                    strmsg = "A routing from an operation  has no destination Operation.  Please Fix this.";
                }
                break;
            case (62):
                {
                    strmsg = "Routing Percentage for ROUTING is less than 0 or greater than 100.  Please fix this.";
                }
                break;
            case (63):
                {
                    strmsg = "Product cannot be found";
                }
                break;
            case (64):
                {
                    strmsg = "Cannot do a COPY task.";
                }
                break;
            case (65):
                {
                    strmsg = "Cannot find a Product or Operation";
                }
                break;
            case (66):
                {
                    strmsg = "EQUIPMENT for an Operation has not been defined.  Please Fix this";
                }
                break;
            case (67):
                {
                    strmsg = "Assignment Percentage error.  for an operation the percent assigned is than 0 or greater than 100.  Please Fix this.";
                }
                break;
            case (68):
                {
                    strmsg = "An Operation cannot be assigned.  Equipment group name is a RESERVED word.";
                }
                break;
            case (69):
                {
                    strmsg = "Routing cannot be solved for a product.  CHECK IF THERE IS A PATH FROM EVERY OPERATION TO 'STOCK'.";
                }
                break;
            case (70):
                {
                    strmsg = "The ratio of labor to equipment exceeds the available labor for the Labor group.";
                }
                break;
            case (71):
                {
                    strmsg = "EMAIL error@networkdyn.com  .  MCT time details are questionable -  Calculation will continue";
                }
                break;
            case (72):
                {
                    strmsg = "Should not do COPY with IBOM.";
                }
                break;
            case (73):
                {
                    strmsg = "Should not do RENAME with IBOM.";
                }
                break;
            case (74):
                {
                    strmsg = "Percentage of pieces Routed out of DOCK does not equal 100%";
                }
                break;
            case (75):
                {
                    strmsg = "Total Proportion Assigned to Operation is not equal to 100%";
                }
                break;
            case (76):
                {
                    strmsg = "Percentage of pieces routed out of Operation does not equal 100%";
                }
                break;
            case (77):
                {
                    strmsg = "All pieces go to SCRAP from an operation.";
                }
                break;
            case (80):
                {
                    strmsg = "for an operation the equipment time=0 and Labor time>0. (Use a dummy equipment?).  Please check operation time data.";
                }
                break;
            case (81):
                {
                    strmsg = "Operation not found???  Trying to place Operation on screen";
                }
                break;
            case (83):
                {
                    strmsg = "Equipment Time is less than Labor Time.";
                }
                break;
            case (84):
                {
                    strmsg = "An operation is a sink for pieces.  There are no 'FROM-TO' routings from this operation?";
                }
                break;
            case (85):
                {
                    strmsg = "Operation data cannot be found.  There is no 'OPERATION ASSIGNMENT' record for this operation?";
                }
                break;
            case (86):
                {
                    strmsg = "No match for report/graph type when setting buttons";
                }
                break;
            case (89):
                {
                    strmsg = "Labor Group is not Used";
                }
                break;
            case (92):
                {
                    strmsg = "Cannot reload.  Data cannot be found.";
                }
                break;
            case (99):
                {
                    strmsg = "Cannot find What-if in data structures.";
                }
                break;
            case (101):
                {
                    strmsg = "Could not redirect system output";
                }
                break;
            case (102):
                {
                    strmsg = "Could not redirect system error messages";
                }
                break;
            case (103):
                {
                    strmsg = "EMAIL error@networkdyn.com  Equipment wait time < 0";
                }
                break;
            case (110):
                {
                    strmsg = "Cannot find data in BASE CASE.  Record cannot be found, WHAT-IF VALUE IGNORED.";
                }
                break;
            case (111):
                {
                    strmsg = "EMAIL error@networkdyn.com    Reading Base    case Data, could not match labor field names";
                }
                break;
            case (114):
                {
                    strmsg = "Placement of Arrow or Box is at a non-existant position";
                }
                break;
            case (115):
                {
                    strmsg = "Cannot determine Arrow Format.";
                }
                break;
            case (116):
                {
                    strmsg = "Cannot Open Input File for Reading";
                }
                break;
            case (121):
                {
                    strmsg = "END OF FILE.  GENERAL DATA SECTION at Title Input Line.";
                }
                break;
            case (122):
                {
                    strmsg = "END OF FILE.  TIME DATA SECTION at Version Input Line";
                }
                break;
            case (123):
                {
                    strmsg = "END OF FILE.  TIME DATA SECTION at TIME UNIT Input Line";
                }
                break;
            case (124):
                {
                    strmsg = "END OF FILE.  TIME DATA SECTION at VARIABILITY Input Line";
                }
                break;
            case (125):
                {
                    strmsg = "END OF FILE.  TIME DATA SECTION at DATES Input Line";
                }
                break;
            case (126):
                {
                    strmsg = "END OF FILE.  TIME DATA SECTION, looking for DONE.";
                }
                break;
            case (131):
                {
                    strmsg = "END OF FILE detected while reading LABOR Group Data.";
                }
                break;
            case (134):
                {
                    strmsg = "END OF FILE detected while reading EQUIPMENT Group Data.";
                }
                break;
            case (138):
                {
                    strmsg = "END OF FILE detected while reading PRODUCT DATA. ";
                }
                break;
            case (139):
                {
                    strmsg = "END OF FILE.  IBOM DATA SECTION.";
                }
                break;
            case (140):
                {
                    strmsg = "COMPONENT PART IN IBOM HAS NO ASSEMBLY TO BE ADDED TOO.  THE REST OF THE INDENTED BILL WILL BE IGNORED";
                }
                break;
            case (142):
                {
                    strmsg = "END OF FILE.  OPERATION DATA SECTION.";
                }
                break;
            case (143):
                {
                    strmsg = "END OF FILE.  OPERATION DATA SECTION.";
                }
                break;
            case (145):
                {
                    strmsg = "END OF FILE.  ROUTING DATA SECTION. ";
                }
                break;
            case (147):
                {
                    strmsg = "END OF FILE.  WHATIF SECTION. ";
                }
                break;
            case (148):
                {
                    strmsg = "END OF FILE.  WHATIF SECTION. ";
                }
                break;
            case (149):
                {
                    strmsg = "END OF FILE.  WHATIF SECTION. ";
                }
                break;
            case (150):
                {
                    strmsg = "END OF FILE.  WHATIF SECTION. ";
                }
                break;
            case (152):
                {
                    strmsg = "Error reading binary file.  End of LABOR section marker is wrong";
                }
                break;
            case (153):
                {
                    strmsg = "Error reading binary file.  End of Equipment section marker is wrong.";
                }
                break;
            case (154):
                {
                    strmsg = "Error reading binary file.  End of PRODUCTS section marker is wrong.";
                }
                break;
            case (155):
                {
                    strmsg = "Error reading binary file.  Last end of section marker is wrong";
                }
                break;
            case (156):
                {
                    strmsg = "Error reading binary file.  End of section marker is wrong";
                }
                break;
            case (157):
                {
                    strmsg = "Error reading binary file.  End of section marker is wrong.";
                }
                break;
            case (158):
                {
                    strmsg = "Error reading binary file.  End of IBOM section marker is wrong";
                }
                break;
            case (159):
                {
                    strmsg = "Error reading binary file.  End of IBOM section Marker is wrong";
                }
                break;
            case (160):
                {
                    strmsg = "Cannot Open Input File for Reading";
                }
                break;
            case (161):
                {
                    strmsg = "END OF FILE.  GENERAL DATA SECTION at title line";
                }
                break;
            case (162):
                {
                    strmsg = "END OF FILE.  GENERAL DATA SECTION at Version Input Line";
                }
                break;
            case (163):
                {
                    strmsg = "END OF FILE.  GENERAL DATA SECTION at TIME UNIT Input Line";
                }
                break;
            case (164):
                {
                    strmsg = "END OF FILE.  GENERAL DATA SECTION at VARIABILITY Input Line";
                }
                break;
            case (165):
                {
                    strmsg = "END OF FILE.  EQUIPMENT DATA SECTION";
                }
                break;
            case (166):
                {
                    strmsg = "END OF FILE.  PRODUCT DATA SECTION.";
                }
                break;
            case (167):
                {
                    strmsg = "END OF FILE.  OPERATION DATA SECTION";
                }
                break;
            case (168):
                {
                    strmsg = "END OF FILE.  OPERATION DATA SECTION";
                }
                break;
            case (169):
                {
                    strmsg = "END OF FILE.  ROUTING DATA SECTION.";
                }
                break;
            case (170):
                {
                    strmsg = "ASCII INPUT LINE WAS NOT UNDERSTOOD.  Line is ignored, reading is continuing.";
                }
                break;
            case (171):
                {
                    strmsg = "Cannot Open file to save Model.";
                }
                break;
            case (172):
                {
                    strmsg = "Cannot Open File for Writing";
                }
                break;
            case (173):
                {
                    strmsg = "File is not Open to Write What-if Data.  It should be open.";
                }
                break;
            case (174):
                {
                    strmsg = "File is not open to Read What-if Data??.  It should be open.";
                }
                break;
            case (175):
                {
                    strmsg = "Error reading binary whatif files.  End of file marker is wrong.";
                }
                break;
            case (176):
                {
                    strmsg = "Error reading binary whatif records.  End of File Marker is wrong.";
                }
                break;
            case (177):
                {
                    strmsg = "Cannot Open file to Write What-if Data.";
                }
                break;
            case (178):
                {
                    strmsg = "Cannot Read What-if Data.  What-if may not have been saved.";
                }
                break;
            case (179):
                {
                    strmsg = "Cannot Open File to read What-if Data.  What-if may not have been saved.";
                }
                break;
            case (180):
                {
                    strmsg = "INPUT FILE NOT CLOSED ??.  checked at read_prn";
                }
                break;
            case (151):
                {
                    strmsg = "No Operations for Product";
                }
                break;
            case (215):
                {
                    strmsg = "Data File was created with a different Version.";
                }
                break;
            case (221):
                {
                    strmsg = "Title Input Line is Short of data.  MPX will use DEFAULT VALUES and continue reading file...";
                }
                break;
            case (222):
                {
                    strmsg = "Version Input Line is Short of data.   MPX will use DEFAULT VALUES and continue reading file...";
                }
                break;
            case (223):
                {
                    strmsg = "TIME UNIT Input Line is Short of data.  MPX will use DEFAULT VALUES and continue reading file...";
                }
                break;
            case (224):
                {
                    strmsg = "VARIBILITY Input Line is Short of data.  MPX will use DEFAULT VALUES and continue reading file...";
                }
                break;
            case (225):
                {
                    strmsg = "DATE Input Line is Short of data.  MPX will use DEFAULT VALUES and continue reading file...";
                }
                break;
            case (226):
                {
                    strmsg = "Did not Find DONE, Reading until 'DONE' is found.  MPX will continue reading file...";
                }
                break;
            case (234):
                {
                    strmsg = "Equipment Input data line is Short of data.  MPX will use DEFAULT VALUES and continue reading file..";
                }
                break;
            case (238):
                {
                    strmsg = "Product Input data line is short of data.  MPX will use DEFAULT VALUES and continue reading file ...";
                }
                break;
            case (239):
                {
                    strmsg = "IBOM Input data line is short of data.  MPX will use DEFAULT VALUES and continue reading file ...";
                }
                break;
            case (240):
                {
                    strmsg = "COMPONENT is a duplicate record.";
                }
                break;
            case (243):
                {
                    strmsg = "Operation input data line is short of data.  MPX will use DEFAULT VALUES and continue reading file ...";
                }
                break;
            case (244):
                {
                    strmsg = "Operation assignment data has two or more records.";
                }
                break;
            case (245):
                {
                    strmsg = "Routing input data line is short of data.  MPX will use DEFAULT VALUES and continue reading file ...";
                }
                break;
            case (246):
                {
                    strmsg = "ROUTING data line is a duplicate.";
                }
                break;
            case (247):
                {
                    strmsg = "WHAT-IF SECTION. WHATIF Input Line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (248):
                {
                    strmsg = "WHAT-IF SECTION. WHATIF Input Line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (249):
                {
                    strmsg = "WHAT-IF SECTION. Comment Line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (250):
                {
                    strmsg = "WHAT-IF SECTION. WHAT-IF Input Data Line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (261):
                {
                    strmsg = "GENERAL DATA Title Input Line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (262):
                {
                    strmsg = "GENERAL DATA Version Input Line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (263):
                {
                    strmsg = "Deleting the last branch of operation.  This will cause the operation and all operational data (e.g. equipment, setup and run times...) to be completely deleted for this part.";
                }
                break;
            case (267):
                {
                    strmsg = "GENERAL DATA TIME UNIT Input Line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (264):
                {
                    strmsg = "GENERAL DATA VARIBILITY Input Line is Short of data  .MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (265):
                {
                    strmsg = "EQUIPMENT Input data line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (266):
                {
                    strmsg = "Product input data line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (268):
                {
                    strmsg = "Operation input data line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (269):
                {
                    strmsg = "Routing input data line is Short of data.  MPX will use DEFAULT VALUES and continue to read file...";
                }
                break;
            case (401):
                {
                    strmsg = "MPX Data files were created with different formats.  Models cannot be transported directly.  You will need to create an ASCII file to move between the versions.";
                }
                break;

                /* default:  {                           
                     strmsg = "";
                  }
                 * */
        };  // end of switch;

        if ((strmsg.Length > 0))
        {
            // msgbox(strmsg + strmsg2, 0, appl_name);
        };

        // return; //exit _ProcessErrorCode:
        return; //exit  Sub;

        //Err_ProcessErrorCode:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume   return; //exit _ProcessErrorCode;

    } // end sub;

    public int RunDLL()
    {
        //on error goto Err_RunDLL;

        //Define variables;
        int fstatus;
        short fCALL = 0;
        short fexit = 0;
        short IntCall = 0;
        int util = -1;

        short fNoError = 0;
        string strmsg;
        string strmsg2;
        string strsql;

        int plngError = 0;
        int plngLaborID = 0;
        int plngEquipID = 0;
        int plngProdID = 0;
        int plngOpID = 0;
        int plngRouteID = 0;
        int plngIbomID = 0;
        int err_test = 0;
        int ret = 0;
        bool whatif_reload = false;
        int overu = 0;
        bool go1;

        string thispath;
        string pvarpath;

        int runcode;
        int retval;



        //Default variables;
        fexit = 0;
        fDLLError = 0;     //'Global DLL Error Flag;
        fstatus = 0;
        fNoError = 0;
        fCALL = 0;

        errorMessageGlobal = "";

        //-----------------------------------------------------------------------------------
        in_calculate = true;

        LogFiles logFiles = new LogFiles(username);


        try
        {
            File.Copy(varlocal + "\\b.xxx", varlocal + "\\a.xxx", true);
            //Copy_File(varlocal + "\\b.xxx", varlocal + "\\a.xxx"); 
        }
        catch (Exception ex)
        {
            logFiles.ErrorLog(ex);
        }

        // first remove all errors .;
        runsqlado("DELETE zstblErrors.* From zstblErrors;");
        //' ''Set pvarPath to correct syntax;
        pvarpath = varlocal + "\\a.xxx";
        DbUse.open_ado(ref globaldb_a, pvarpath);

        //' make sure about equiptypes ...;
        match_delay_equip(2);

        // verify ??;
        fNoError = ValidateData();

        if (fNoError == 0)
        {
            // error was found when validating data
            retval = 0;
            model_modified = -1;
            saveModel_modified();
            eliminate_nulls0_ado();
            calc_return |= ERR_FLAG | ERR_MSG_FLAG;
            return retval;

        }
        else
        {
            retval = 0;
            model_modified = -1;
            saveModel_modified();
            eliminate_nulls0_ado();

            //' displayMessage(("Calculating Parameters"));
            runsqlado("DELETE zstblErrors.* From zstblErrors where MPXFLAG = 1;");

            EmptyResultsTables();

            go1 = SetEquipResults();    //  if no equipment day = 0 
            if (go1 == false)
            {
                calc_return = NO_EQUIP_FLAG;
                in_calculate = false;
                return 0;
            }
            go1 = SetProductResults();  // no products returns 0 !!
            if (go1 == false)
            {
                calc_return = NO_PROD_FLAG;
                in_calculate = false;
                return 0;
            }

            clear_results();
            /*if (DbUse.IsDllRunning()) {
                if (!DbUse.WaitForDllFinish() || DbUse.IsDllRunning()) {
                    errorMessageGlobal = "The server is currently busy. Please try the calculations later again.";
                    return 0;
                }
            }
            try {
                DbUse.CreateDllInProcessFile();
            } catch (Exception excep) {
                errorMessageGlobal += "The calculations were aborted because of a possible danger of a data conflict. Please run the calculations again.";
                throw excep;
            }*/
            logFiles.RunLogi("0");
            //fstatus = iClear_model(0, varlocal);       //   doesn't matter ??? \\ at end   yes/no 

            PassGeneralInput();
            //logFiles.RunLogi("labor");
            PassLaborInput();
            //logFiles.RunLogi("equip");
            PassEquipInput();
            //logFiles.RunLogi("prod");
            PassProdInfo();
            //logFiles.RunLogi("oper");
            PassOper();
            //logFiles.RunLogi("route");
            PassOperFrTo();
            //logFiles.RunLogi("ibom");
            PassIbomInput();


            //logFiles.RunLogi(" run-s"); 
            //runcode = iRun_model(glngwid, plngError, plngLaborID, plngEquipID, plngProdID, plngOpID, plngRouteID, plngIbomID, util, inOPT);
            //logFiles.RunLogi(" run-d");
            //DbUse.DeleteCommonRunFile();


            try
            {
                runcode = irun_it(varlocal, time1, time2, utlimit, cv_l, cv_e, cv_p,
                                    Lab_count, Lab_id, Lab_size, Lab_ot, lab_abs, Lab_s_fact, Lab_r_fact, Lab_v_fact, Lab_share,
                                    Eq_count, Eq_id, eq_size, mttf, MTTR, eq_ot, Lab_id2, eq_s_fact, eq_r_fact, eq_v_fact, eq_cell,
                                    Prod_count, Part_id, demand, lotsize, tbatch, part_d_fact, part_v_fact, tbg,
                                    Op_count, OP_name, op_id, Part_id2, Eq_id2, Pct_Assgn, eq_setup, eq_SetupTbatch, eq_SetupPiece, eq_RunLot, eq_RunTbatch, eq_run,
                                    Lab_setup, lab_SetupTbatch, lab_SetupPiece, lab_RunLot, lab_RunTbatch, Lab_run,
                                    Route_count, Part_id3, from_op_id, to_op_id, Pct_route,
                                    Ibom_count, Part_id4, Comp_part_id, UPA,
                                    glngwid);

            }
            catch (Exception ex)
            {
                // logFiles.ErrorLog(ex);
            }


            fCALL = 0;

            err_test = erroradd(true);
            runcode = 0;
            //Test the return condition from the DLL - not used at all!!
            /*if (runcode != 0) {
                ProcessErrorCode(IntCall, plngError, plngLaborID, plngEquipID, plngProdID, plngOpID, plngRouteID, plngIbomID);
                fDLLError = -1;
                calc_return = ERR_FLAG | ERR_MSG_FLAG;
                in_calculate = false;
                return 0; //exit _RunDLL;
            };*/


            if ((calc_return & (ERR_FLAG | ERR_MSG_FLAG)) == 0)
            {
                model_modified = -1;
                verify_needed = 0;
                saveModel_modified();
                saveVerify_needed();
                saveRecalcNeeded(glngwid, 0);  //today  gwwd needed in input areas as well!!!  
            }



            MessageBoxOpen:
            retval = fDLLError;

            strmsg2 = "";



            FillResultTables();
            overu = 0;
            if (err_test != 0)
            {
                overu = do_over_util_open();
            }
            if (err_test == 0)
            {
                strmsg2 = "The calculations are incomplete. An error was found.";
                calc_return |= ERR_FLAG | ERR_MSG_FLAG;
                in_calculate = false;
                return 0;
            };
            if (err_test == 1)
            {
                strmsg2 = "The calculations are complete.  However some warnings were issued.";
                calc_return |= WARN_FLAG | ERR_MSG_FLAG;
                in_calculate = false;
                return 0;
            }
            else
            {
                //' not warnings or errors;
                strmsg2 = "The Calculations are Complete. ";
            }

            if (overu == -1)
            {
                strmsg2 += "Some resources are over-utilized.  Flow Times and WIP have NOT  been calculated.";
            }
            else
            {
                strmsg2 += " No resources are over-utilized.  Flow Times and WIP have been calculated.  Use Output pull-down menu on main screen to see results values.";
            };




            strmsg2 = strmsg2.Trim();
            if ((inOPT == 0) && (strmsg2.Length > 0))
            {
                // zz msgbox(strmsg2, 0, appl_name);
            }
            else if ((inOPT == -1) && (fCALL == 0) && (err_test != 1) && (err_test != 2))
            {
                //zz msgbox("MPX Calculation error in run ... ", 0, appl_name);
            };


            //gdif no error in validate - done  do msgbox etc.!!!
        } //  



        //exit _RunDLL:
        in_calculate = false;

        fexit = -1;



        retval = -1;
        return retval; //exit  Function;

    } //  end function;





    bool SetEquipResults()
    {
        //on error goto Err_SetEquipResults;

        //Define variables;

        int intcount;
        int fstatus;
        string strtable;
        int fexit;
        ADODB.Recordset rec1 = null;


        //Set variables;
        intcount = 0;
        strtable = "tblEquip";

        DbUse.open_ado_rec(globaldb, ref rec1, strtable);

        //Count the number of records in table;
        fexit = -10;
        intcount = recordcount(rec1);

        DbUse.CloseAdoRec(rec1);

        if (intcount == 0)
        {
            // msgbox("No Equipment is defined. No calculations will be done.", MsgBoxStyle.OkOnly, appl_name);
            return false; //exit  Sub;
        };
        fexit = 0;

        return true;

    } // end sub;

    bool SetProductResults()
    {
        //on error goto Err_SetProductResults;

        //Define variables;

        int intcount;
        int fstatus;
        string strtable;
        int fexit;
        ADODB.Recordset rec1 = null;

        //Set variables;
        intcount = 0;
        strtable = "tblProdFore";

        DbUse.open_ado_rec(globaldb, ref rec1, strtable);

        //Count the number of records in table;
        fexit = -10;
        intcount = recordcount(rec1);
        DbUse.CloseAdoRec(rec1);
        if (intcount == 0)
        {
            // msgbox("No Products are defined. No calculations will be done.", MsgBoxStyle.OkOnly, appl_name);
            return false; //exit  Sub;
        };
        fexit = 0;

        // return; //exit _SetProductResults:
        return true; //exit  Sub;


    } // end sub;

    protected void eliminate_nulls0_ado()
    {
        runsqlado("UPDATE tblGeneral SET tblGeneral.g1 = 0 WHERE (((IIf(IsNull([g1]),0,-1))=0));");
        runsqlado("UPDATE tblGeneral SET tblGeneral.g2 = 0 WHERE (((IIf(IsNull([g2]),0,-1))=0));");
        runsqlado("UPDATE tblGeneral SET tblGeneral.g3 = 0 WHERE (((IIf(IsNull([g3]),0,-1))=0));");
        runsqlado("UPDATE tblGeneral SET tblGeneral.g4 = 0 WHERE (((IIf(IsNull([g4]),0,-1))=0));");

        runsqlado("UPDATE tblLabor SET tblLabor.l1 = 0 WHERE (((IIf(IsNull([l1]),0,-1))=0));");
        runsqlado("UPDATE tblLabor SET tblLabor.l2 = 0 WHERE (((IIf(IsNull([l2]),0,-1))=0));");
        runsqlado("UPDATE tblLabor SET tblLabor.l3 = 0 WHERE (((IIf(IsNull([l3]),0,-1))=0));");
        runsqlado("UPDATE tblLabor SET tblLabor.l4 = 0 WHERE (((IIf(IsNull([l4]),0,-1))=0));");
        runsqlado("UPDATE tbllabor SET tbllabor.laborDept = ' ' WHERE (((IIf(IsNull([labordept]),0,-1))=0));");
        runsqlado("UPDATE tbllabor SET tbllabor.labcomment = ' ' WHERE (((IIf(IsNull([labcomment]),0,-1))=0));");

        runsqlado("UPDATE tblEquip SET tblEquip.e1 = 0 WHERE (((IIf(IsNull([e1]),0,-1))=0));");
        runsqlado("UPDATE tblEquip SET tblEquip.e2 = 0 WHERE (((IIf(IsNull([e2]),0,-1))=0));");
        runsqlado("UPDATE tblEquip SET tblEquip.e3 = 0 WHERE (((IIf(IsNull([e3]),0,-1))=0));");
        runsqlado("UPDATE tblEquip SET tblEquip.e4 = 0 WHERE (((IIf(IsNull([e4]),0,-1))=0));");
        runsqlado("UPDATE tblEquip SET tblEquip.equipDept = ' ' WHERE (((IIf(IsNull([equipdept]),0,-1))=0));");
        runsqlado("UPDATE tblEquip SET tblEquip.eqcomment = ' ' WHERE (((IIf(IsNull([eqcomment]),0,-1))=0));");


        runsqlado("UPDATE tblProdFore SET tblProdFore.p1 = 0 WHERE (((IIf(IsNull([p1]),0,-1))=0));");
        runsqlado("UPDATE tblProdFore SET tblProdFore.p2 = 0 WHERE (((IIf(IsNull([p2]),0,-1))=0));");
        runsqlado("UPDATE tblProdFore SET tblProdFore.p3 = 0 WHERE (((IIf(IsNull([p3]),0,-1))=0));");
        runsqlado("UPDATE tblProdFore SET tblProdFore.p4 = 0 WHERE (((IIf(IsNull([p4]),0,-1))=0));");
        runsqlado("UPDATE tblprodfore SET tblprodfore.prodDept = ' ' WHERE (((IIf(IsNull([proddept]),0,-1))=0));");
        runsqlado("UPDATE tblprodfore SET tblprodfore.prodcomment = ' ' WHERE (((IIf(IsNull([prodcomment]),0,-1))=0));");
        runsqlado("UPDATE tblprodfore SET tblprodfore.makestock = -1 WHERE (((IIf(IsNull([makestock]),0,-1))=0));");
        runsqlado("UPDATE tblprodfore SET tblprodfore.tbatchgather = -1 WHERE (((IIf(IsNull([tbatchgather]),0,-1))=0));");


        runsqlado("UPDATE tbloper SET tbloper.o1 = 0 WHERE (((IIf(IsNull([o1]),0,-1))=0));");
        runsqlado("UPDATE tbloper SET tbloper.o2 = 0 WHERE (((IIf(IsNull([o2]),0,-1))=0));");
        runsqlado("UPDATE tbloper SET tbloper.o3 = 0 WHERE (((IIf(IsNull([o3]),0,-1))=0));");
        runsqlado("UPDATE tbloper SET tbloper.o4 = 0 WHERE (((IIf(IsNull([o4]),0,-1))=0));");

        runsqlado("UPDATE tbloper SET tbloper.eqsetuptbatch = 0 WHERE (((IIf(IsNull([eqsetuptbatch]),0,-1))=0));");
        runsqlado("UPDATE tbloper SET tbloper.eqsetuppiece = 0 WHERE (((IIf(IsNull([eqsetuppiece]),0,-1))=0));");
        runsqlado("UPDATE tbloper SET tbloper.eqruntbatch = 0 WHERE (((IIf(IsNull([eqruntbatch]),0,-1))=0));");

        runsqlado("UPDATE tbloper SET tbloper.eqrunlot = 0 WHERE (((IIf(IsNull([eqrunlot]),0,-1))=0));");

        runsqlado("UPDATE tbloper SET tbloper.labsetuptbatch = 0 WHERE (((IIf(IsNull([labsetuptbatch]),0,-1))=0));");
        runsqlado("UPDATE tbloper SET tbloper.labsetuppiece = 0 WHERE (((IIf(IsNull([labsetuppiece]),0,-1))=0));");
        runsqlado("UPDATE tbloper SET tbloper.labruntbatch = 0 WHERE (((IIf(IsNull([labruntbatch]),0,-1))=0));");
        runsqlado("UPDATE tbloper SET tbloper.labrunlot = 0 WHERE (((IIf(IsNull([labrunlot]),0,-1))=0));");


    }


    void eliminate_nulls0()
    {
        //on error goto err_nulls0;


        string CONNECTION_STRING = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + varlocal + "\\mpxmdb.mdb";


        using (OleDbConnection cnnNW = new OleDbConnection(CONNECTION_STRING))
        {
            cnnNW.Open();


            OleDbCommand Dosqltext = new OleDbCommand("Update tbloper INNER JOIN tbloper AS tbloper_1 ON (tbloper.ProdFore = tbloper_1.ProdFore) AND (tbloper.OpNam = tbloper_1.OpNam) SET tbloper.OpNum = [tbloper_1].[opnum] WHERE ((tbloper.OpID<>[tbloper_1].[opid]));", cnnNW);
            Dosqltext.ExecuteNonQuery();

            Dosqltext.CommandText = "UPDATE tblGeneral SET tblGeneral.g1 = 0 WHERE (((IIf(IsNull([g1]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblGeneral SET tblGeneral.g2 = 0 WHERE (((IIf(IsNull([g2]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblGeneral SET tblGeneral.g3 = 0 WHERE (((IIf(IsNull([g3]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblGeneral SET tblGeneral.g4 = 0 WHERE (((IIf(IsNull([g4]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();

            Dosqltext.CommandText = "UPDATE tblLabor SET tblLabor.l1 = 0 WHERE (((IIf(IsNull([l1]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblLabor SET tblLabor.l2 = 0 WHERE (((IIf(IsNull([l2]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblLabor SET tblLabor.l3 = 0 WHERE (((IIf(IsNull([l3]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblLabor SET tblLabor.l4 = 0 WHERE (((IIf(IsNull([l4]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbllabor SET tbllabor.laborDept = ' ' WHERE (((IIf(IsNull([labordept]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbllabor SET tbllabor.labcomment = ' ' WHERE (((IIf(IsNull([labcomment]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();

            Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.e1 = 0 WHERE (((IIf(IsNull([e1]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.e2 = 0 WHERE (((IIf(IsNull([e2]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.e3 = 0 WHERE (((IIf(IsNull([e3]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.e4 = 0 WHERE (((IIf(IsNull([e4]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.equipDept = ' ' WHERE (((IIf(IsNull([equipdept]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblEquip SET tblEquip.eqcomment = ' ' WHERE (((IIf(IsNull([eqcomment]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();


            Dosqltext.CommandText = "UPDATE tblProdFore SET tblProdFore.p1 = 0 WHERE (((IIf(IsNull([p1]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblProdFore SET tblProdFore.p2 = 0 WHERE (((IIf(IsNull([p2]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblProdFore SET tblProdFore.p3 = 0 WHERE (((IIf(IsNull([p3]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblProdFore SET tblProdFore.p4 = 0 WHERE (((IIf(IsNull([p4]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblprodfore SET tblprodfore.prodDept = ' ' WHERE (((IIf(IsNull([proddept]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblprodfore SET tblprodfore.prodcomment = ' ' WHERE (((IIf(IsNull([prodcomment]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblprodfore SET tblprodfore.makestock = -1 WHERE (((IIf(IsNull([makestock]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tblprodfore SET tblprodfore.tbatchgather = -1 WHERE (((IIf(IsNull([tbatchgather]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();


            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.o1 = 0 WHERE (((IIf(IsNull([o1]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.o2 = 0 WHERE (((IIf(IsNull([o2]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.o3 = 0 WHERE (((IIf(IsNull([o3]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.o4 = 0 WHERE (((IIf(IsNull([o4]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();

            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.eqsetuptbatch = 0 WHERE (((IIf(IsNull([eqsetuptbatch]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.eqsetuppiece = 0 WHERE (((IIf(IsNull([eqsetuppiece]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.eqruntbatch = 0 WHERE (((IIf(IsNull([eqruntbatch]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();

            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.eqrunlot = 0 WHERE (((IIf(IsNull([eqrunlot]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();

            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.labsetuptbatch = 0 WHERE (((IIf(IsNull([labsetuptbatch]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.labsetuppiece = 0 WHERE (((IIf(IsNull([labsetuppiece]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.labruntbatch = 0 WHERE (((IIf(IsNull([labruntbatch]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();
            Dosqltext.CommandText = "UPDATE tbloper SET tbloper.labrunlot = 0 WHERE (((IIf(IsNull([labrunlot]),0,-1))=0));";
            Dosqltext.ExecuteNonQuery();



            cnnNW.Close();
        }
        Open(); // update connection

        //exit_nulls0:
        return; //exit  Sub;
        //err_nulls0:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;

    float eval1(string str1)
    {
        //on error goto err_eval1;
        float retval;


        if ((str1.Length == 0))
        {
            retval = 0;
            return retval; //exit  Function;
        };

        if (IsNumeric(str1))
        {
            retval = str2f(CDbl_ndi(str1));
            return retval; //exit  Function;
        };

        if ((str1 == ".") || (str1 == ","))
        {
            retval = 0.0F;
            return retval; //exit  Function;
        };

        retval = RunSql2(str1, 1);

        exit_eval1:
        ;

        return retval; //exit  Function;

        err_eval1:
        retval = -999;
        // resume   return; //exit _eval1;

    } // end function;

    float eval2(string str1, short first)
    {
        //on error goto err_retval;

        string str3;
        float retval;

        retval = -999;

        if ((str1.Length == 0))
        {
            retval = 0;
            return retval; //exit  Function;
        };

        if (IsNumeric(str1))
        {
            retval = str2f(CDbl_ndi(str1));
            return retval; //exit  Function;
        };

        if (str1 == "." || str1 == ",")
        {
            retval = 0.0F;
            return retval; //exit  Function;
        };

        retval = RunSql2(str1, 2);

        /*  if (YACC)   {
              if ((Length(str1) > 0))   {
                  retval = isolve(str1);
                  FileOpen(1, "str1.1", OpenMode.Input);
                  Input(1, str3);
                  FileClose(1);
                  retval = CSng(str3);
                  };
          
              if (first  == 0)   {
                  if (retval  == -999)   {
                      retval = RunSql2(str1, 2);
                      };
                  };

          } else {
              if (first  == 0)   {
                  retval = RunSql2(str1, 2);
                  };
              };
         */

        //exit_retval: ;
        return retval; //exit  Function;

        //err_retval:
        // msgbox(ErrorToString(), 0, appl_name);
        //retval = -999;
        // resume   return retval; //exit _retval;

    } // end function;



    public void runall_from_display()
    {

        int ret1;
        int oldwid;
        int wid;


        ADODB.Recordset rec1 = null;

        oldwid = glngwid;

        var mySession = HttpContext.Current.Session;
        var mySessionID = mySession.SessionID;

        if (CalculationsCancelled(mySessionID))
        {
            global_runalldone = true;
            return;
        }
        int currentCalc = 1;
        long currTime = DateTime.Now.Ticks;
        DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET calc = " + currentCalc + ", lastCheck = " + currTime + ", timePerCalc = -1 WHERE userlist.sessionid = '" + mySessionID + "';");


        if (glngwid != 0) { SaveWhatIfAudit(glngwid); LoadBaseCase(); }

        //  must be in basecase now !!!
        CreateRunFile();
        ret1 = RunDLL();
        if (ret1 != -1)
        {
            global_runalldone = true;
            LoadWhatIf(oldwid);
            return;
        }
        long lastTime = currTime;


        DbUse.open_ado_rec(globaldb, ref rec1, "SELECT tblWhatIf.*, tblWhatIf.recalc  FROM tblWhatIf WHERE FamilyID = 0 ORDER BY tblwhatif.WID;");

        while (rec1.EOF == false)
        {
            // stop calculations if needed
            if (CalculationsCancelled(mySessionID))
            {
                break;
            }
            currTime = DateTime.Now.Ticks;
            currentCalc++;

            long timePerCalc = currTime - lastTime;

            DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET calc = " + currentCalc + ", lastCheck = " + currTime + ", timePerCalc = " + timePerCalc + " WHERE userlist.sessionid = '" + mySessionID + "';");
            lastTime = currTime;
            /////////////////////////////////


            LoadBaseCase();
            wid = Convert.ToInt32(rec1.Fields["wid"].Value);
            LoadWhatIf(wid);

            glngwid = wid;
            saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
            //rec1.MoveNext();
            CreateRunFile();
            ret1 = RunDLL();

            if (ret1 != -1)
            {   //  if errors 
                LoadBaseCase();
                LoadWhatIf(oldwid);
                DbUse.CloseAdoRec(rec1);
                glngwid = oldwid;
                saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
                global_runalldone = true;
                return;
            }
            rec1.MoveNext();


        }

        //  reset to start condition...
        LoadBaseCase();
        LoadWhatIf(oldwid);
        DbUse.CloseAdoRec(rec1);
        glngwid = oldwid;
        saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
        global_runalldone = true;
        return;

    }

    public System.Collections.Generic.List<string[]> CalculateResults(int calcType, string sessionID)
    {

        int ret1;
        int oldwid;
        int wid;
        System.Collections.Generic.List<string[]> myResults = new System.Collections.Generic.List<string[]>();

        ADODB.Recordset rec1 = null;

        oldwid = glngwid;

        //string cookieid = MyUtilities.clean(System.Web.HttpContext.Current.Request.Cookies[DbUse.LOGIN_COOKIE].Value);
        //if (CalculationsCancelled(cookieid))
        //{
        //    global_runalldone = true;
        //    return;
        //}
        int currentCalc = 1;
        long currTime = DateTime.Now.Ticks;
        long timePerCalc = 100000000;
        long lastTime = currTime;

        if (glngwid != 0) { SaveWhatIfAudit(glngwid); LoadBaseCase(); }

        //  must be in basecase now !!!
        //only calculate basecase if it needs recalc

        ADODB.Recordset reccust = null;


        DbUse.open_ado_rec(globaldb, ref reccust, "SELECT * FROM zs0tblWhatIf WHERE zs0tblWhatIf.WID=0;");
        int basecaserecalc = Convert.ToInt32(reccust.Fields["recalc"].Value);

        if (basecaserecalc != 0 || calcType == 1)
        {
            currTime = DateTime.Now.Ticks;
            DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET calc = " + currentCalc + ", lastCheck = " + currTime + ", timePerCalc = " + timePerCalc + ", curName = 'Basecase' WHERE userlist.sessionid = '" + sessionID + "';");
            CreateRunFile();
            ret1 = RunDLL();
            string validationMessage = CalcClass.getValidationMessage(this.calc_return);
            string errWarnMessage = CalcClass.getErrWarnMessage(this.calc_return);
            string utilMessage = CalcClass.getMessage(this.calc_return);
            string resultColor;
            if (String.IsNullOrEmpty(validationMessage) && String.IsNullOrEmpty(errorMessageGlobal) && String.IsNullOrEmpty(utilMessage))
            {
                resultColor = "true";
                utilMessage = "NO OVER-UTILIZATION";

            }
            else
            {
                resultColor = "false";
            }
            myResults.Add(new string[] { "Basecase", validationMessage, errWarnMessage, utilMessage, resultColor });
            this.calc_return = 0;
            currentCalc++;
            lastTime = currTime;
            if (ret1 != -1)
            {
                global_runalldone = true;
                LoadWhatIf(oldwid);
                return myResults;
            }

        }

        string myConnectionString;
        if (calcType == 0)
        {
            myConnectionString = "SELECT * FROM tblWhatIf WHERE FamilyID = 0 AND recalc = true ORDER BY tblwhatif.WID;";
        }
        else
        {
            myConnectionString = "SELECT * FROM tblWhatIf WHERE FamilyID = 0 AND display = -1;";
        }

        DbUse.open_ado_rec(globaldb, ref rec1, myConnectionString);
        string modelName = "";
        while (rec1.EOF == false)
        {
            //// stop calculations if needed
            //if (CalculationsCancelled(cookieid))
            //{
            //    break;
            //}


            timePerCalc = (timePerCalc + currTime - lastTime) / currentCalc;

            LoadBaseCase();
            wid = Convert.ToInt32(rec1.Fields["wid"].Value);
            modelName = Convert.ToString(rec1.Fields["name"].Value);
            LoadWhatIf(wid);
            glngwid = wid;
            saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
            //rec1.MoveNext();
            currTime = DateTime.Now.Ticks;
            DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET calc = " + currentCalc + ", lastCheck = " + currTime + ", timePerCalc = " + timePerCalc + ", curName = '" + modelName + "' WHERE userlist.sessionid = '" + sessionID + "';");
            CreateRunFile();
            ret1 = RunDLL();

            string validationMessage = CalcClass.getValidationMessage(this.calc_return);
            string errWarnMessage = CalcClass.getErrWarnMessage(this.calc_return);
            string utilMessage = CalcClass.getMessage(this.calc_return);
            string resultColor;
            if (String.IsNullOrEmpty(validationMessage) && String.IsNullOrEmpty(errorMessageGlobal) && String.IsNullOrEmpty(utilMessage))
            {
                resultColor = "true";
                utilMessage = "NO OVER-UTILIZATION";

            }
            else
            {
                resultColor = "false";
            }
            myResults.Add(new string[] { modelName, validationMessage, errWarnMessage, utilMessage, resultColor });
            this.calc_return = 0;
            currentCalc++;
            lastTime = currTime;

            //var message = CalcClass.do_calc_msg(this.calc_return, 1);
            //CALCCLASS IS SET GLOBALLY; SO ONCE A ERROR IS CAUGHT ALL MODELS WILL REPORT ERROR; NEED TO SET ERROR, WRITE TO RESULT; CLEAR ERROR? clear calc_return

            if (ret1 != -1)
            {   //  if errors 
                LoadBaseCase();
                LoadWhatIf(oldwid);
                DbUse.CloseAdoRec(rec1);
                glngwid = oldwid;
                saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
                global_runalldone = true;
                return myResults;
            }
            rec1.MoveNext();


        }

        //  reset to start condition...
        LoadBaseCase();
        LoadWhatIf(oldwid);
        DbUse.CloseAdoRec(rec1);
        glngwid = oldwid;
        saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
        global_runalldone = true;
        return myResults;

    }

    public void Run_All_ReCalc(int calcType)
    {

        int ret1;
        int oldwid;
        int wid;
        System.Collections.Generic.List<string[]> myResults = new System.Collections.Generic.List<string[]>();

        ADODB.Recordset rec1 = null;

        oldwid = glngwid;

        var mySession = HttpContext.Current.Session;
        var mySessionID = mySession.SessionID;

        if (CalculationsCancelled(mySessionID))
        {
            global_runalldone = true;
            return;
        }
        int currentCalc = 1;
        long currTime = DateTime.Now.Ticks;
        long timePerCalc = 100000000;
        long lastTime = currTime;

        if (glngwid != 0) { SaveWhatIfAudit(glngwid); LoadBaseCase(); }

        //  must be in basecase now !!!
        //only calculate basecase if it needs recalc

        ADODB.Recordset reccust = null;


        DbUse.open_ado_rec(globaldb, ref reccust, "SELECT * FROM zs0tblWhatIf WHERE zs0tblWhatIf.WID=0;");
        int basecaserecalc = Convert.ToInt32(reccust.Fields["recalc"].Value);

        if (basecaserecalc != 0 || calcType == 1)
        {
            currTime = DateTime.Now.Ticks;
            DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET calc = " + currentCalc + ", lastCheck = " + currTime + ", timePerCalc = " + timePerCalc + ", curName = 'Basecase' WHERE userlist.sessionid = '" + mySessionID + "';");
            CreateRunFile();
            ret1 = RunDLL();
            myResults.Add(new string[] { "basecase", CalcClass.getMessage(this.calc_return)});
            this.calc_return = 0;
            currentCalc++;
            lastTime = currTime;
            if (ret1 != -1)
            {
                global_runalldone = true;
                LoadWhatIf(oldwid);
                return;
            }

        }

        string myConnectionString; 
        if (calcType == 0)
        {
            myConnectionString = "SELECT * FROM tblWhatIf WHERE FamilyID = 0 AND recalc = true ORDER BY tblwhatif.WID;";
        }
        else
        {
            myConnectionString = "SELECT * FROM tblWhatIf WHERE FamilyID = 0 AND display = -1;";
        }

        DbUse.open_ado_rec(globaldb, ref rec1, myConnectionString);
        string modelName = "";
        while (rec1.EOF == false)
        {
            // stop calculations if needed
            if (CalculationsCancelled(mySessionID))
            {
                break;
            }


            timePerCalc = (timePerCalc + currTime - lastTime) / currentCalc;

            LoadBaseCase();
            wid = Convert.ToInt32(rec1.Fields["wid"].Value);
            modelName = Convert.ToString(rec1.Fields["name"].Value);
            LoadWhatIf(wid);
            glngwid = wid;
            saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
            //rec1.MoveNext();
            currTime = DateTime.Now.Ticks;
            DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET calc = " + currentCalc + ", lastCheck = " + currTime + ", timePerCalc = " + timePerCalc + ", curName = '" + modelName + "' WHERE userlist.sessionid = '" + mySessionID + "';");
            CreateRunFile();
            ret1 = RunDLL();
            myResults.Add(new string[] { modelName, CalcClass.getMessage(this.calc_return) });
            currentCalc++;
            lastTime = currTime;

            //var message = CalcClass.do_calc_msg(this.calc_return, 1);
            //CALCCLASS IS SET GLOBALLY; SO ONCE A ERROR IS CAUGHT ALL MODELS WILL REPORT ERROR; NEED TO SET ERROR, WRITE TO RESULT; CLEAR ERROR? clear calc_return

            if (ret1 != -1)
            {   //  if errors 
                LoadBaseCase();
                LoadWhatIf(oldwid);
                DbUse.CloseAdoRec(rec1);
                glngwid = oldwid;
                saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
                global_runalldone = true;
                return;
            }
            rec1.MoveNext();


        }

        //  reset to start condition...
        LoadBaseCase();
        LoadWhatIf(oldwid);
        DbUse.CloseAdoRec(rec1);
        glngwid = oldwid;
        saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
        global_runalldone = true;
        return;

    }

    public void Run_All_ReCalc()
    {

        int ret1;
        int oldwid;
        int wid;


        ADODB.Recordset rec1 = null;

        oldwid = glngwid;

        var mySession = HttpContext.Current.Session;
        var mySessionID = mySession.SessionID;

        if (CalculationsCancelled(mySessionID))
        {
            global_runalldone = true;
            return;
        }
        int currentCalc = 1;
        long currTime = DateTime.Now.Ticks;
        long timePerCalc = 100000000;
        long lastTime = currTime;

        if (glngwid != 0) { SaveWhatIfAudit(glngwid); LoadBaseCase(); }

        //  must be in basecase now !!!
        //only calculate basecase if it needs recalc

        ADODB.Recordset reccust = null;


        DbUse.open_ado_rec(globaldb, ref reccust, "SELECT * FROM zs0tblWhatIf WHERE zs0tblWhatIf.WID=0;");
        int basecaserecalc = Convert.ToInt32(reccust.Fields["recalc"].Value);

        if (basecaserecalc != 0)
        {
            currTime = DateTime.Now.Ticks;
            DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET calc = " + currentCalc + ", lastCheck = " + currTime + ", timePerCalc = " + timePerCalc + ", curName = 'Basecase' WHERE userlist.sessionid = '" + mySessionID + "';");
            CreateRunFile();
            ret1 = RunDLL();
            currentCalc++;
            lastTime = currTime;
            if (ret1 != -1)
            {
                global_runalldone = true;
                LoadWhatIf(oldwid);
                return;
            }

    }



    DbUse.open_ado_rec(globaldb, ref rec1, "SELECT * FROM tblWhatIf WHERE FamilyID = 0 AND recalc = true ORDER BY tblwhatif.WID;");
        string modelName = "";
        while (rec1.EOF == false)
        {
            // stop calculations if needed
            if (CalculationsCancelled(mySessionID))
            {
                break;
            }


            timePerCalc = (timePerCalc + currTime - lastTime) / currentCalc;

            LoadBaseCase();
            wid = Convert.ToInt32(rec1.Fields["wid"].Value);
             modelName = Convert.ToString(rec1.Fields["name"].Value);
            LoadWhatIf(wid);
            glngwid = wid;
            saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
            //rec1.MoveNext();
            currTime = DateTime.Now.Ticks;
            DbUse.RunMysql("UPDATE usercalc INNER JOIN userlist ON usercalc.id = userlist.id SET calc = " + currentCalc + ", lastCheck = " + currTime + ", timePerCalc = " + timePerCalc + ", curName = '" + modelName + "' WHERE userlist.sessionid = '" + mySessionID + "';");
            CreateRunFile();
            ret1 = RunDLL();
            currentCalc++;
            
            lastTime = currTime;

            if (ret1 != -1)
            {   //  if errors 
                LoadBaseCase();
                LoadWhatIf(oldwid);
                DbUse.CloseAdoRec(rec1);
                glngwid = oldwid;
                saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
                global_runalldone = true;
                return;
            }
            rec1.MoveNext();


        }

        //  reset to start condition...
        LoadBaseCase();
        LoadWhatIf(oldwid);
        DbUse.CloseAdoRec(rec1);
        glngwid = oldwid;
        saveWid(); // saveglngwid telling us what whatif is actually loaded right now!
        global_runalldone = true;
        return;

    }


    int do_over_util_open()
    {
        //on error goto err1;

        ADODB.Recordset rec1 = null;
        string str1;
        int retval;

        retval = 0;

        if ((inOPT == -1))
        {
            return retval; //exit  Function;
        };

        str1 = "SELECT [SetupUtil]+[runutil]+[reputil]+[labwaitutil] AS Expr1, tblRsEquip.WID, tblRsEquip.EquipID FROM tblRsEquip INNER JOIN tblequip ON tblRsEquip.EquipID = tblequip.EquipID WHERE ((([SetupUtil]+[runutil]+[reputil]+[labwaitutil])>" + get_utlimit() + ") AND (tblRsEquip.WID=" + glngwid + ") AND ((tblequip.GrpSiz)>0));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.EOF)
        {
            //'  show equip util l!! 
            calc_return |= EQ_OVER_U;
            retval = -1;

        };

        str1 = "SELECT tblRsLabor.WID, [RunUtil]+[absutil]+[setuputil] AS Expr1, tbllabor.GrpSiz  FROM tblRsLabor INNER JOIN tbllabor ON tblRsLabor.LaborID = tbllabor.LaborID WHERE((tblRsLabor.WID=" + glngwid + ") AND (([RunUtil]+[absutil]+[setuputil])>" + get_utlimit() + ") AND ((tbllabor.GrpSiz)>0));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);
        if (!rec1.EOF)
        {
            //' show labor util results !! 
            calc_return |= LAB_OVER_U;
            retval = -1;

        };

        if (null != rec1)
        {
            DbUse.CloseAdoRec(rec1);
            rec1 = null;
        };

        return retval; //exit  Function;


    } // end function;

    // GREG gerg  - this will make your life easier - method to format numbers!!!!!!!!! (no do_comma needed)
    // Similarly we can format even thousand separator to be space or comma

    //   NONO   the problem is a bit more subtle (sp?) the user enters the numbers as strings (eg.  5+1.25)  and if international 5 +1,25 but tat gets read as 5+1,125. = 1230 not 6.25

    public string FormatDecimalNumber(double num)
    {
        NumberFormatInfo numFormat = new NumberFormatInfo();
        numFormat.NumberDecimalSeparator = "."; // us format
        numFormat.NumberDecimalSeparator = ","; // european format
        return num.ToString(numFormat);
    }

    string do_comma(string str1)
    {


        //on error goto err1;

        string strnew;
        short intplace;
        string retval;
        char c59;
        char c46;
        char c44;

        if (INTERNATIONAL == false)
        {
            retval = str1;
            return retval; //exit  Function;
        };

        strnew = str1.ToUpper();

        c59 = Convert.ToChar(59);   //  ;
        c46 = Convert.ToChar(46);   //   .
        c44 = Convert.ToChar(44);   //   ,

        strnew.Replace(c44, c59);  //  , becomes ;
        strnew.Replace(c46, c44);  //  . becomes ,
        strnew.Replace(c59, c46);  //  ; becomes .


        retval = strnew;

        //exit1:
        return retval; //exit  Function;
        //err1:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit1;
    } // end function;


    int numberOfParts;
    int[] partid;
    float[] weight;
    int[] optimizeLotSize;
    int[] OptimizeTbatch;
    float[] lotsizeValue;
    float[] tbatchValue;


    public void do_opt()
    {
        int ret;
        int i;
        bool go1;

        ///////////////////////////////////////////////////////////////////////////

        //on error goto Err_RunDLL;

        //Define variables;
        short fNoError = 0;
        string pvarpath;
        int fstatus;
        int fCALL;


        LogFiles logFiles = new LogFiles();



        //Default variables;
        fexit = 0;
        fDLLError = 0;     //'Global DLL Error Flag;
        fstatus = 0;
        fNoError = 0;
        fCALL = 0;

        errorMessageGlobal = "";

        //-----------------------------------------------------------------------------------
        in_calculate = true;




        try
        {
            File.Copy(varlocal + "\\b.xxx", varlocal + "\\a.xxx", true);
            //Copy_File(varlocal + "\\b.xxx", varlocal + "\\a.xxx"); 
        }
        catch (Exception ex)
        {
            //
            logFiles.ErrorLog(ex);
        }

        // first remove all errors .;
        runsqlado("DELETE zstblErrors.* From zstblErrors;");
        //' ''Set pvarPath to correct syntax;
        pvarpath = varlocal + "\\a.xxx";
        DbUse.open_ado(ref globaldb_a, pvarpath);


        match_delay_equip(2);


        fNoError = ValidateData();



        ret = 0;
        model_modified = -1;
        saveModel_modified();
        eliminate_nulls0();

        //' displayMessage(("Calculating Parameters"));
        runsqlado("DELETE zstblErrors.* From zstblErrors where MPXFLAG = YES;");

        EmptyResultsTables();

        go1 = SetEquipResults();    //  if no equipment day = 0 

        go1 = SetProductResults();  // no products returns 0 !!

        clear_results();


        PassGeneralInput();

        PassLaborInput();

        PassEquipInput();

        PassProdInfo();

        PassOper();

        PassOperFrTo();


        PassIbomInput();




        /////////////////////////////////////////////////////////////////////////////
        ADODB.Recordset recprod = null;

        DbUse.open_ado_rec(globaldb, ref recprod, "select tblProdFore.* FROM tblProdFore WHERE (((tblProdFore.optimizelotsize)<>0) or ((tblProdFore.optimizetbatch)<>0));");
        if (recprod.EOF == true)
        {
            calc_return = NO_OPT_FLAG;
            return;
        }

        go1 = SetEquipResults();    //   if no equipment day = 0 
        if (go1 == false)
        {
            calc_return = NO_EQUIP_FLAG;
            return;
        }
        go1 = SetProductResults();  //  no products returns 0 !!
        if (go1 == false)
        {
            calc_return = NO_PROD_FLAG;
            return;
        }
        //Get the number of records in table;
        DbUse.open_ado_rec(globaldb, ref recprod, "select tblProdFore.* FROM tblProdFore;");
        if (recprod.EOF == true)
            recprod.MoveFirst();
        recprod.MoveLast();
        numberOfParts = (int)recordcount(recprod);

        // Array.Resize(ref  eq_SetupPiece, (intcount));

        Array.Resize(ref partid, numberOfParts);
        Array.Resize(ref weight, (numberOfParts));
        Array.Resize(ref optimizeLotSize, (numberOfParts));
        Array.Resize(ref OptimizeTbatch, (numberOfParts));
        Array.Resize(ref lotsizeValue, (numberOfParts));
        Array.Resize(ref tbatchValue, (numberOfParts));


        recprod.MoveFirst();
        for (i = 0; i < numberOfParts; i++)
        {
            partid[i] = (int)recprod.Fields["prodid"].Value;
            weight[i] = Convert.ToSingle(recprod.Fields["value"].Value);
            optimizeLotSize[i] = Convert.ToInt32(recprod.Fields["optimizelotsize"].Value);
            OptimizeTbatch[i] = Convert.ToInt32(recprod.Fields["optimizetbatch"].Value);
            lotsizeValue[i] = Convert.ToSingle(recprod.Fields["Lotsiz"].Value);
            tbatchValue[i] = Convert.ToSingle(recprod.Fields["TransferBatch"].Value);
            recprod.MoveNext();
        }
        DbUse.CloseAdoRec(recprod);
        ret = iopt_all(varlocal,

         //General_time ( float time1, float time2, float u_limit, float lab_var, float eq_var, float part_var,
         time1, time2, utlimit, cv_l, cv_e, cv_p,


                  //Labor_data (
                  // int l_count, int *   x1 , float *   x2, float *   x3, float *   x4,
                  //float *   x5, float *   x6, float *   x7, int * x8, 
                  Lab_count, Lab_id, Lab_size, Lab_ot, lab_abs, Lab_s_fact, Lab_r_fact, Lab_v_fact, Lab_share,

                //  Equip_data (
                // int eq_count, int *   e_x1, int *   e_x2, float *   e_x3, float *   e_x4, float *  e_x5,
                // int *   e_x6, float *   e_x7, float *   e_x8, float *   e_x9, int * e_x10,
                Eq_count, Eq_id, eq_size, mttf, MTTR, eq_ot, Lab_id2, eq_s_fact, eq_r_fact, eq_v_fact, eq_cell,

                //Parts_data (
                // int p_count, int *   p_x1, float *   p_x2, float *   p_x3, float *   p_x4,
                //  float *   p_x5, float *   p_x6, int * p_x7,
                Prod_count, Part_id, demand, lotsize, tbatch, part_d_fact, part_v_fact, tbg,

                  //Oper_data (
                  // int op_count, int *  o_x1, int *   o_x2, int *   o_x3, int *   o_x4, float *   o_x5,
                  // float *   o_x6, float *   o_x7, float *   o_x8, float *   o_x9,
                  // float *   o_x10,float *   o_x11,float *   o_x12,float *   o_x13,
                  // float *   o_x14,float *   o_x15,float *   o_x16,float *   o_x17,
                  Op_count, OP_name, op_id, Part_id2, Eq_id2, Pct_Assgn, eq_setup, eq_SetupTbatch, eq_SetupPiece, eq_RunLot, eq_RunTbatch, eq_run, Lab_setup, lab_SetupTbatch, lab_SetupPiece, lab_RunLot, lab_RunTbatch, Lab_run,

                   // Route_data (
                   // int r_count, int *   r_x1, int *   r_x2, int *   r_x3, float *   r_x4,
                   Route_count, Part_id3, from_op_id, to_op_id, Pct_route,

                   //Ibom_data (
                   // int ib_count, int *   i_x1, int *   i_x2, float *   i_x3,
                   Ibom_count, Part_id4, Comp_part_id, UPA,


                //Run_model (
                glngwid,

            //  opt stuff           


            numberOfParts, partid, weight, optimizeLotSize, OptimizeTbatch, lotsizeValue, tbatchValue);


        runsqlado("DELETE zstblErrors.* From zstblErrors where MPXFLAG = YES;");
        EmptyResultsTables();


        clear_results();

        FillResultTables();


        transfertext("t", "lotandtbatch.res", -1);  //  do whatif too!!!

        //exit1:
        return; //exit  Sub;
        //err1:
        /*   if (Err.Number  == 6)   {
                //RESUME NEXT
               };
           // msgbox(ErrorToString(), 0, appl_name);
            //RESUME NEXT
      */
    } // end sub;

    public short read_stop_file()
    {
        //on error  //RESUME NEXT

        if (File.Exists(varlocal + "\\STOP.1"))
        {
            return -1;
        }
        else
        {
            return 0;
        };
    } // end function;

    public string Call_text2pict(int idnum)
    {
        text2pic(idnum);

        return "-1";

    } // end function;

    string Call_pict2text(int idnum)
    {
        pic2text(idnum);

        return "-1";
    } // end function;



    //  next 2 subroutines are not used of needed yet !!!!

    string get_cell_name(int id)
    {
        //on error goto err1;

        ADODB.Recordset rec1a = null;
        string retval;

        retval = "";

        DbUse.open_ado_rec(globaldb, ref rec1a, "zscelllist");
        rec1a.Filter = "[id] = " + id;
        if (!rec1a.EOF)
        {
            if (null != rec1a)
            {
                DbUse.CloseAdoRec(rec1a);
                rec1a = null;
            };
            return ((string)rec1a.Fields["Name"].Value);
        };


        //exit1:
        if (null != rec1a)
        {
            DbUse.CloseAdoRec(rec1a);
            rec1a = null;
        };

        return (""); //exit  Function;
        //err1:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit1;
    } // end function;

    int get_cell(string name1)
    {
        //on error goto err1;

        ADODB.Recordset rec1a = null;
        int retval;

        retval = 0;

        DbUse.open_ado_rec(globaldb, ref rec1a, "zscelllist");
        rec1a.Filter = "[name] = '" + name1 + "'";
        if (!rec1a.EOF)
        {
            retval = (int)rec1a.Fields["id"].Value;
        }
        else
        {
            retval = cell_count;
            rec1a.AddNew();
            rec1a.Fields["id"].Value = cell_count;
            cell_count = cell_count + 1;
            rec1a.Fields["Name"].Value = name1;
            rec1a.Update();
        };


        //exit1:
        if (null != rec1a)
        {
            DbUse.CloseAdoRec(rec1a);
            rec1a = null;
        };
        return retval; //exit  Function;
        //err1:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit1;
    } // end function;

    void clear_cell()
    {
        //on error goto err1;

        cell_count = 0;
        runsqlado("delete zscelllist.* from zscelllist;");
        //exit1:
        return; //exit  Sub;
        //err1:
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit1;
    } // end sub;

    public float CalcNumber(string pString, ref string[] pstrFld1, ref string[] psngFld2, short pLen)
    {
        //on error goto Err_CalcNumber;

        //Define variables;
        string strnew;
        int intplace;
        int i;
        string pStrExp;
        string pstrRep;
        int done;
        int min1;
        int start1;
        int restart;
        int restart_calc;
        float retval;

        if (IsNumeric(pString))
        {
            retval = str2f(pString);
            return retval; //exit  Function;
        };

        restart_calc = 0;

        redo:  //  used for restart loop   string contains another variable name ??

        calcerr = 0;
        restart = 0;

        restart_calc = restart_calc + 1;
        pStrExp = pString.ToUpper();
        for (i = 1; i <= (pLen - 1); i++)
        {
            //Build expression;
            pstrRep = pstrFld1[i].ToUpper();
            intplace = pStrExp.IndexOf(pstrRep);
            if (pstrRep.Length > 0)
            {
                while (intplace != -1)
                {
                    strnew = strleft(pStrExp, intplace);
                    strnew = strnew + psngFld2[i];
                    strnew = strnew + strright(pStrExp, pStrExp.Length - intplace - (pstrRep.Length - 1));
                    pStrExp = strnew;
                    intplace = pStrExp.IndexOf(pstrRep, intplace + 1);
                    restart = 1;
                };
            };  // end if
        };   // end for

        pString = pStrExp.ToUpper();
        if (restart != 0)
        {
            if (restart_calc < 30)
            {
                goto redo;
            }
            else
            {
                // msgbox(" ERROR: substitution is circular.  Can't resolve number using -777 as the value.", 0, appl_name);
                retval = -777;
                STOPMPX = -1;
                return retval; //exit  Function;
            };
        };

        pStrExp = do_comma(pStrExp);

        done = 0;
        start1 = 1;
        while (done == 0)
        {
            min1 = pStrExp.IndexOf("-", start1);

            min1 = 0;

            if (min1 == 0)
            {
                done = -1;
            }
            else
            {
                strnew = strleft(pStrExp, min1 - 1) + " - ";
                start1 = min1 + 3;
                strnew = strnew + pStrExp.Substring(min1 + 1, 3000);
                pStrExp = strnew;
            };

        };  //  end while

        retval = eval2(pStrExp, 0);

        // return; //exit _CalcNumber:
        return retval; //exit  Function;

        //Err_CalcNumber:
        // msgbox(ErrorToString(), 0, appl_name);
        /*   if ((Err.Number  == 2434))   {
               // msgbox(appl_name + " will use -999 as the calculated value.", 0, appl_name);
               retval = -999;
               calcerr = -1;
                //RESUME NEXT
               return retval; //exit  Function;
           } else {
               // msgbox(appl_name + " will use -888 as the calculated value.", 0, appl_name);
               retval = -888;
               calcerr = -1;
               return retval; //exit  Function;
                //RESUME NEXT
               };
           // resume   return; //exit _CalcNumber;
         */
    } // end function

    void do_oper_hours()
    {
        //on error goto err1;

        string str1;

        str1 = "UPDATE tblgeneral, (((tblProdFore INNER JOIN tblOper ON tblProdFore.ProdID = tblOper.ProdFore) INNER JOIN tblEquip ON tblOper.EqID = tblEquip.EquipID) INNER JOIN tblLabor ON tblEquip.Labor = tblLabor.LaborID) INNER JOIN tblRsOper ON (tblOper.OpID = tblRsOper.OpID) AND (tblProdFore.ProdID = tblRsOper.ProdID) SET tblRsOper.e_set_hours = [tblRsOper].[EqSetTime]*([tblequip].[ot]/100+1)*[tblgeneral].[rtu1b]*[tblgeneral].[rtu1c]/100, tblRsOper.l_run_hours = [tblRsOper].[LabRunTime]*([tbllabor].[ot]/100+1)*[tblgeneral].[rtu1b]*[tblgeneral].[rtu1c]/100, tblRsOper.l_set_hours = [tblRsOper].[LabSetTime]*([tbllabor].[ot]/100+1)*[tblgeneral].[rtu1b]*[tblgeneral].[rtu1c]/100, tblRsOper.e_run_hours = [tblRsOper].[EqRunTime]*([tblequip].[ot]/100+1)*[tblgeneral].[rtu1b]*[tblgeneral].[rtu1c]/100 " +
            " WHERE (((tblRsOper.WID)=" + glngwid + "));";

        runsqlado(str1);


        runsqlado("Update tblRsLabor INNER JOIN tblLabor ON tblRsLabor.LaborID = tblLabor.LaborID SET tblRsLabor.clocktime = 1 WHERE (((tblRsLabor.WID)=" + glngwid + ") AND ((tblLabor.GrpSiz)=-1));");

        str1 = " UPDATE tblgeneral, tblLabor INNER JOIN tblRSLabor ON " + " tblLabor.LaborID = tblRSLabor.LaborID SET " + " tblRSLabor.clocktime = [RTU1b]*[rtu1c]*([tbllabor].[ot]/100+1)*([tbllabor].[grpsiz]) " + " WHERE (((tblLabor.GrpSiz)>0) AND ((tblRSLabor.WID)=" + glngwid + "));";
        runsqlado(str1);


        runsqlado("Update  tblRsequip INNER JOIN tblequip ON tblRsequip.equipID = tblequip.equipID SET tblRsequip.clocktime = 1 WHERE (((tblRsequip.WID)=" + glngwid + ") AND ((tblequip.GrpSiz)=-1));");

        str1 = " UPDATE tblgeneral, tblequip INNER JOIN tblRSequip ON " + " tblequip.equipID = tblRSequip.equipID SET " + " tblRSequip.clocktime = [RTU1b]*[rtu1c]*([tblequip].[ot]/100+1)*([tblequip].[grpsiz]) " + " WHERE (((tblequip.GrpSiz)>0) AND ((tblRSequip.WID)=" + glngwid + "));";
        runsqlado(str1);
        //exit1:
        return; //exit  Sub;

        //err1:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end sub;

    int test_for_overutil()
    {
        //on error goto err1;

        ADODB.Recordset rec1 = null;
        string str1;
        int retval;

        str1 = "SELECT [SetupUtil]+[runutil]+[reputil]+[labwaitutil] AS Expr1, tblRsEquip.WID, tblRsEquip.EquipID FROM tblRsEquip INNER JOIN tblequip ON tblRsEquip.EquipID = tblequip.EquipID WHERE ((([SetupUtil]+[runutil]+[reputil]+[labwaitutil])>" + get_utlimit() + ") AND (tblRsEquip.WID=" + glngwid + ") AND ((tblequip.GrpSiz)>0));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        if (!rec1.EOF)
        {
            retval = -1;
            if (null != rec1)
            {
                DbUse.CloseAdoRec(rec1);
                rec1 = null;
            };
            return retval; //exit  Function;
        };

        str1 = "SELECT tblRsLabor.WID, [RunUtil]+[absutil]+[setuputil] AS Expr1, tbllabor.GrpSiz  FROM tblRsLabor INNER JOIN tbllabor ON tblRsLabor.LaborID = tbllabor.LaborID WHERE((tblRsLabor.WID=" + glngwid + ") AND (([RunUtil]+[absutil]+[setuputil])>" + get_utlimit() + ") AND ((tbllabor.GrpSiz)>0));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        if (!rec1.EOF)
        {
            retval = -1;

            if (null != rec1)
            {
                DbUse.CloseAdoRec(rec1);
                rec1 = null;
            };
            return retval; //exit  Function;
        };

        retval = 0;


        //exit1:
        if (null != rec1)
        {
            DbUse.CloseAdoRec(rec1);
            rec1 = null;
        };
        return retval; //exit  Function;
        //err1:
        // msgbox(ErrorToString(), 0, appl_name);
        //RESUME NEXT

    } // end function;

    short labor_over_util()
    {


        string str1;
        ADODB.Recordset rec1 = null;
        short retval;

        str1 = "SELECT tblRsLabor.WID, [RunUtil]+[absutil]+[setuputil] AS Expr1, tbllabor.GrpSiz  FROM tblRsLabor INNER JOIN tbllabor ON tblRsLabor.LaborID = tbllabor.LaborID WHERE((tblRsLabor.WID=" + glngwid + ") AND (([RunUtil]+[absutil]+[setuputil])>" + get_utlimit() + ") AND ((tbllabor.GrpSiz)>0));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        if (!rec1.EOF)
        {
            retval = -1;

            if (null != rec1)
            {
                DbUse.CloseAdoRec(rec1);
                rec1 = null;
            };
            return retval; //exit  Function;
        };

        retval = 0;


        if (null != rec1)
        {
            DbUse.CloseAdoRec(rec1);
            rec1 = null;
        };
        return retval; //exit  Function;
    } // end function;


    short equip_over_util()
    {


        string str1;
        ADODB.Recordset rec1 = null;
        short retval;

        str1 = "SELECT [SetupUtil]+[runutil]+[reputil]+[labwaitutil] AS Expr1, tblRsEquip.WID, tblRsEquip.EquipID FROM tblRsEquip INNER JOIN tblequip ON tblRsEquip.EquipID = tblequip.EquipID WHERE ((([SetupUtil]+[runutil]+[reputil]+[labwaitutil])>" + get_utlimit() + ") AND (tblRsEquip.WID=" + glngwid + ") AND ((tblequip.GrpSiz)>0));";
        DbUse.open_ado_rec(globaldb, ref rec1, str1);

        if (!rec1.EOF)
        {
            retval = -1;

            if (null != rec1)
            {
                DbUse.CloseAdoRec(rec1);
                rec1 = null;
            };
            return retval; //exit  Function;
        };

        retval = 0;

        if (null != rec1)
        {
            DbUse.CloseAdoRec(rec1);
            rec1 = null;
        };

        return retval; //exit  Function;
    } // end function;


    float max(float X, float Y)
    {

        return Math.Max(X, Y);
        /*if (X > Y)   {
           return X; //exit  Function;
        } else {
             return Y; //exit  Function;
            };*/
    } // end function;





    // '' void runsql_L(ByVal str1 As String) {

    // ''    ''  // dim  Dodelete As New OleDbCommand()  ' The command to execute.;
    // ''         string ThisPath       ' Current application path.;
    // ''         string usersub;


    // ''    ' Get the path.;
    // ''    ThisPath = path1;

    // ''    usersub = getsubdir(cookienum);


    // ''         string CONNECTION_STRING  = _
    // ''            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ThisPath + usersub + "a.xxx";

    // ''     // dim  cnnNW As New OleDbConnection(CONNECTION_STRING);

    // ''    ';
    // ''    ' // dim  daEquipment As New OleDbDataAdapter( _
    // ''    '    "SELECT * FROM qfltequip", cnnNW);
    // ''    ' Create the SQL command.;


    // ''    str1 = clean_L(str1);

    // ''     // dim  Dosqltext As New OleDbCommand(str1, cnnNW);

    // ''    cnnNW.Open();
    // ''    Dosqltext.ExecuteNonQuery();
    // ''    cnnNW.Close();


    // ''} // end sub;


    int recordcount(ADODB.Recordset reccust)
    {
        //on error goto err1;
        int c1;

        c1 = 0;
        reccust.Requery();
        if (reccust.EOF || reccust.BOF)
        {
            return 0; //exit  Function;
        };

        reccust.MoveFirst();
        while (!reccust.EOF)
        {
            c1 = c1 + 1;
            reccust.MoveNext();
        };  // end while


        return c1; //exit  Function;

        //err1:   // msgbox(ErrorToString(), MsgBoxStyle.OkOnly, appl_name);
        //return 0;
        //RESUME NEXT

    } // end function;




    string get_title()
    {
        //on error goto err_title;

        ADODB.Recordset reccust = null;
        string retval;

        //Set Database variable;
        DbUse.open_ado_rec(globaldb, ref reccust, "tblgeneral");

        reccust.MoveFirst();

        retval = (string)reccust.Fields["TITLE"].Value;

        //exit_title:
        DbUse.CloseAdoRec(reccust);
        reccust = null;
        return retval; //exit  Function;
        //err_title:
        /*   if ((Err.Number  == 3024) || (Err.Number = 3044) || (Err.Number = 3078))   {
                return retval; //exit  Function;
               };
         * */
        // msgbox(ErrorToString(), 0, appl_name);
        // resume  exit_title;

    } // end function;


    short openerrors()
    {
        return -1;

    } // end function;


    public void Run_include(string whatifname)
    {


        int wid;
        int wid2;
        string str1;
        string str2;
        ADODB.Recordset recprod = null;
        ADODB.Recordset recibom = null;
        int fstatus;
        int ret1;


        //  tomorrow checked out name_type !!!! 1)  blank => bsaecase    2) name-> wid => use old name  3) no old name/wid => new whatif. 
        //                                      LoadBaseCase NO BLANKS 


        if (whatifname.Length == 0)
        {
            whatifname = "Base Case";
        };

        wid2 = glngwid;
        wid2 = get_wid(whatifname);
        if ((glngwid != 0) & (wid2 != glngwid))
        {
            {
                //  check out User provided name_type ....  tomorrow   gd check 
                // check out if whatif name matches current whatif name ? 
                // error msg  ask user about current whatifname records 
                LoadBaseCase();
            }
        }  //  end of get to basecase loaded  or stay with current whatif


        if (wid2 == -1)
        {// tomorrow  start new whatif 
            str1 = "Whatif created by Run page - include some products";

            wid = addnewwhatif(whatifname, str1);
            glngwid = wid;

            model_modified = -1;
            saveModel_modified();
        }
        else if (wid2 == 0)
        {  //  nothing here  BASECASE
        }
        else if (wid2 != glngwid)
        { //load wid2  tomorrow  
            LoadWhatIf(wid2);
        }

        //  else wid2 >0 and = glngwid nothing...  current whatif 




        //------------------------------------------------------------------------------------------------------------


        if (glngwid != 0)
        {

            //  create / save changes 
            //  loop parts => insertaudit 
            str1 = "SELECT  tblProdFore.* FROM tblProdFore WHERE (((tblProdFore.include)=False));";

            DbUse.open_ado_rec(globaldb, ref recprod, str1);


            while ((recprod.EOF) == false)
            {
                //  tomorrow  check prod fore whatif Fields ...  tomorrow   gd check 



                globNVal = "0";
                globTNameE = "Products ";
                globTNameA = "tblprodfore";
                globDType = SINGLE_TYPE;
                globFNameA = "demandfac";
                globOVal = Convert.ToString((float)recprod.Fields["demandfac"].Value);
                globFNameE = recprod.Fields["proddesc"].Value + " -  Demand Muliplier";
                globrecid = (int)recprod.Fields["prodid"].Value;
                fstatus = InsertAudit();


                //  loop all ibom records as well!!! tblprodfore 
                str1 = "SELECT tblProdFore.ProdID, tblProdFore.include, tblProdFore.ProdDesc, tblIbom.UPA, tblProdFore_1.ProdDesc AS parent1, tblIbom.IbomID  FROM tblProdFore AS tblProdFore_1 INNER JOIN (tblProdFore INNER JOIN tblIbom ON tblProdFore.ProdID = tblIbom.CompID) ON tblProdFore_1.ProdID = tblIbom.ParentID " +
                                                   " WHERE ((tblProdFore_1.ProdID)=" + globrecid + ");";
                DbUse.open_ado_rec(globaldb, ref recibom, str1);

                //  set values for add whatif 

                globNVal = "0";
                globTNameE = "IBOM ";
                globTNameA = "tblibom";
                globDType = SINGLE_TYPE;
                globFNameA = "upa";


                while ((recibom.EOF) == false)
                {
                    globOVal = Convert.ToString((float)recibom.Fields["upa"].Value); //  tomorrow   check  fields[ for conversionss!!!!
                    globFNameE = "Parent: " + recibom.Fields["parent1"].Value + " - Component: " + recibom.Fields["proddesc"].Value + "  -  Units per asssembly ";
                    globrecid = (int)recibom.Fields["ibomid"].Value;
                    fstatus = InsertAudit();

                    recibom.MoveNext();
                }

                recprod.MoveNext();
            }   //  end loop on recprod
            DbUse.CloseAdoRec(recprod);

            str1 = "UPDATE tblProdFore SET tblProdFore.DemandFac = 0 WHERE (((tblProdFore.include)=False));";
            runsqlado(str1);
            str1 = "UPDATE tblProdFore INNER JOIN tblIbom ON tblProdFore.ProdID = tblIbom.CompID SET tblIbom.UPA = 0 WHERE (((tblProdFore.include)=False)); ";
            runsqlado(str1);
            //  end if whatif <> 0
        }
        else
        {   //  BaseCase ....
            //  not whatif 
            str1 = "UPDATE tblProdFore SET tblProdFore.DemandFac = 0 WHERE (((tblProdFore.include)=False));";
            runsqlado(str1);
            str1 = "UPDATE tblProdFore INNER JOIN tblIbom ON tblProdFore.ProdID = tblIbom.CompID SET tblIbom.UPA = 0 WHERE (((tblProdFore.include)=False)); ";
            runsqlado(str1);
        }


        ret1 = RunDLL();
        if (ret1 != -1)
        {
            return;
        }




    }




    /* long all lotsize and demand range 
    * 
    * start 
    *   setup whatif family for the whole ...
    * 
    * loop lot size 
    *   do range for 1 paert 
    *     do loop for next part in dataset .
    *     no more 
    *     
    * 
    * start
    * demand loop
    *   do range for 1 part 
    *       do loop for next part in recordset 
    *       no more ? 
    *       
    *   do whatif new (name + number)
    *     rundll
   */

    /* start with just 1 part demand and lot size 
     

     string whatif_family;
    int whatiffamily_no;
    ADODB.Recordset  recprod_d = null;
    ADODB.Recordset  recprod_l = null;
    string prod_name_d;
    string prod_name_l;
    void strart_big_whatif() { 

        string str1;
        string str2;
        str1= "delete zswhatif_prod _l";
        runsql(str1);
        str1 = "delete zswhatif_prod_d";
        runsql(str1);
        open_ado_rec(globaldb, ref recwhatif, "whatif - max family id"); 



    */



    public ClassC1(string localdir)
        : base(localdir)
    {

    }

    public void MakeActualoper_all()
    {
        //on error GoTo Err_MakeActualValues;

        //Define variables;

        ADODB.Recordset recProd = null;
        int prodid;

        DbUse.open_ado_rec(globaldb, ref recProd, "select tblprodfore.* from tblprodfore");

        runsqlado("DELETE tbloper_d.* from tbloper_d;");

        while (!recProd.EOF)
        {
            prodid = Convert.ToInt32(recProd.Fields["prodid"].Value);
            MakeActualoper_p(prodid);

            recProd.MoveNext();
        }

        DbUse.CloseAdoRec(recProd);
        recProd = null;
    }


    public void MakeActualroute_all()
    {
        //on error GoTo Err_MakeActualValues;

        //Define variables;

        ADODB.Recordset recProd = null;

        int prodid;
        runsqlado("DELETE tblOpFrTo_d.* from tblOpFrTo_d;");
        DbUse.open_ado_rec(globaldb, ref recProd, "select tblprodfore.* from tblprodfore");


        while (!recProd.EOF)
        {
            prodid = Convert.ToInt32(recProd.Fields["prodid"].Value);
            MakeActualroute_p(prodid);

            recProd.MoveNext();
        }
        recProd.Close();
        runsqlado("UPDATE tblOpFrTo_d INNER JOIN tblOperFrTo ON tblOpFrTo_d.RecID = tblOperFrTo.RecID SET tblOpFrTo_d.fromnum = tblOperFrTo.fromnum, tblOpFrTo_d.tonum = tblOperFrTo.tonum;");

    }

    public void MakeActualoper_p(long prodid)
    {
        //on error GoTo Err_MakeActualValues;

        //Define variables;

        ADODB.Recordset recOper_d = null;


        ADODB.Recordset recoper = null;

        long i;
        long Count;
        string strsql;
        string str_v;



        qstrArry[0] = "Junk";
        qstrArry[1] = "PercentAssign";
        qstrArry[2] = "EqSetupTime";
        qstrArry[3] = "EqRunTime";
        qstrArry[4] = "LabSetupTime";
        qstrArry[5] = "LabRunTime";
        qstrArry[6] = "Equip";
        qstrArry[7] = getname("O1");
        qstrArry[8] = getname("O2");
        qstrArry[9] = getname("O3");
        qstrArry[10] = getname("O4");
        qstrArry[11] = getname("G1");
        qstrArry[12] = getname("G2");
        qstrArry[13] = getname("G3");
        qstrArry[14] = getname("G4");
        qstrArry[15] = getname("L1");
        qstrArry[16] = getname("L2");
        qstrArry[17] = getname("L3");
        qstrArry[18] = getname("L4");
        qstrArry[19] = getname("E1");
        qstrArry[20] = getname("E2");
        qstrArry[21] = getname("E3");
        qstrArry[22] = getname("E4");
        qstrArry[23] = getname("P1");
        qstrArry[24] = getname("P2");
        qstrArry[25] = getname("P3");
        qstrArry[26] = getname("P4");
        qstrArry[27] = "demand";
        qstrArry[28] = "lotsize";
        qstrArry[29] = "transferbatch";
        qstrArry[30] = "variability";

        qstrArry[31] = "eqSetupPiece";
        qstrArry[32] = "eqSetupTbatch";
        qstrArry[33] = "eqRunLot";
        qstrArry[34] = "eqRunTbatch";
        qstrArry[35] = "labSetupPiece";
        qstrArry[36] = "labSetupTbatch";
        qstrArry[37] = "labRunLot";
        qstrArry[38] = "labRunTbatch";



        //'Set variables;
        strsql = "SELECT DISTINCTROW qfltOperCalc.OpID, qfltOperCalc.OpNam, qfltopercalc.opnum,  qfltOperCalc.ProdFore, qfltOperCalc.V1, qfltOperCalc.V2, qfltOperCalc.V3, qfltOperCalc.V4, qfltOperCalc.V5, qfltOperCalc.V6, qfltOperCalc.V7, qfltOperCalc.V8, qfltOperCalc.V9, qfltOperCalc.V10, qfltOperCalc.V11, qfltOperCalc.V12, qfltOperCalc.V13, qfltOperCalc.V14, qfltOperCalc.V15, qfltOperCalc.V16, qfltOperCalc.V17, qfltOperCalc.V18, qfltOperCalc.V19, qfltOperCalc.V20, qfltOperCalc.V21, qfltOperCalc.V22, qfltOperCalc.V23, qfltOperCalc.V24, qfltOperCalc.V25, qfltOperCalc.V26, qfltOperCalc.V27, qfltOperCalc.V28, qfltOperCalc.V29, qfltOperCalc.V30, qfltOperCalc.V31, qfltOperCalc.V32, qfltOperCalc.V33, qfltOperCalc.V34, qfltOperCalc.V35, qfltOperCalc.V36, qfltOperCalc.V37, qfltOperCalc.V38 FROM qfltOperCalc Where qfltOperCalc.ProdFore = " + prodid + " ;";

        runsqlado("UPDATE tblprodfore SET tblprodfore.include = 0;");
        runsqlado("UPDATE tblprodfore SET tblprodfore.include = -1 WHERE (((tblprodfore.ProdID)=" + prodid + "));");


        DbUse.open_ado_rec(globaldb, ref recoper, strsql);


        DbUse.open_ado_rec(globaldb, ref recOper_d, "tblOper_d");


        string[] qvalues = new string[40];


        //'Get the number of records in table;
        Count = get_count(strsql, -1);
        if (Count > 0)
        {
            recoper.MoveFirst();
        }

        //'Fill all arrays;
        for (i = 0; i < Count; i++)
        {
            if (i != 0)
            {
                recoper.MoveNext();
            }
            qvalues[0] = "0";
            qvalues[1] = (string)CDbl_ndi((string)recoper.Fields["v1"].Value);
            qvalues[2] = (string)CDbl_ndi((string)recoper.Fields["v2"].Value);
            qvalues[3] = (string)CDbl_ndi((string)recoper.Fields["v3"].Value);
            qvalues[4] = (string)CDbl_ndi((string)recoper.Fields["v4"].Value);
            qvalues[5] = (string)CDbl_ndi((string)recoper.Fields["v5"].Value);
            qvalues[6] = Convert.ToString(recoper.Fields["V6"].Value);
            qvalues[7] = (string)CDbl_ndi((string)recoper.Fields["V7"].Value);
            qvalues[8] = (string)CDbl_ndi((string)recoper.Fields["V8"].Value);
            qvalues[9] = (string)CDbl_ndi((string)recoper.Fields["V9"].Value);
            qvalues[10] = (string)CDbl_ndi((string)recoper.Fields["V10"].Value);
            qvalues[11] = (string)CDbl_ndi((string)recoper.Fields["V11"].Value);
            qvalues[12] = (string)CDbl_ndi((string)recoper.Fields["V12"].Value);
            qvalues[13] = (string)CDbl_ndi((string)recoper.Fields["V13"].Value);
            qvalues[14] = (string)CDbl_ndi((string)recoper.Fields["V14"].Value);
            qvalues[15] = (string)CDbl_ndi((string)recoper.Fields["V15"].Value);
            qvalues[16] = (string)CDbl_ndi((string)recoper.Fields["V16"].Value);
            qvalues[17] = (string)CDbl_ndi((string)recoper.Fields["V17"].Value);
            qvalues[18] = (string)CDbl_ndi((string)recoper.Fields["V18"].Value);
            qvalues[19] = (string)CDbl_ndi((string)recoper.Fields["V19"].Value);
            qvalues[20] = (string)CDbl_ndi((string)recoper.Fields["V20"].Value);
            qvalues[21] = (string)CDbl_ndi((string)recoper.Fields["V21"].Value);
            qvalues[22] = (string)CDbl_ndi((string)recoper.Fields["V22"].Value);
            qvalues[23] = (string)CDbl_ndi((string)recoper.Fields["V23"].Value);
            qvalues[24] = (string)CDbl_ndi((string)recoper.Fields["V24"].Value);
            qvalues[25] = (string)CDbl_ndi((string)recoper.Fields["V25"].Value);
            qvalues[26] = (string)CDbl_ndi((string)recoper.Fields["V26"].Value);
            qvalues[28] = Convert.ToString((float)recoper.Fields["V28"].Value);
            qvalues[30] = Convert.ToString((float)recoper.Fields["v30"].Value);

            qvalues[31] = (string)CDbl_ndi((string)recoper.Fields["v31"].Value);
            qvalues[32] = (string)CDbl_ndi((string)recoper.Fields["V32"].Value);
            qvalues[33] = (string)CDbl_ndi((string)recoper.Fields["V33"].Value);
            qvalues[34] = (string)CDbl_ndi((string)recoper.Fields["V34"].Value);
            qvalues[35] = (string)CDbl_ndi((string)recoper.Fields["V35"].Value);
            qvalues[36] = (string)CDbl_ndi((string)recoper.Fields["V36"].Value);
            qvalues[37] = (string)CDbl_ndi((string)recoper.Fields["V37"].Value);
            qvalues[38] = (string)CDbl_ndi((string)recoper.Fields["V38"].Value);


            Part_err_id = (int)recoper.Fields["ProdFore"].Value;
            Oper_err_id = (int)recoper.Fields["opid"].Value;
            Part_err_name = find_item(Part_err_id, 0, product);

            Oper_err_name = (string)recoper.Fields["opnam"].Value;


            recOper_d.AddNew();
            recOper_d.Fields["opid"].Value = recoper.Fields["opid"].Value;
            recOper_d.Fields["opnam"].Value = recoper.Fields["opnam"].Value;
            recOper_d.Fields["opnum"].Value = recoper.Fields["opnum"].Value;
            recOper_d.Fields["ProdFore"].Value = recoper.Fields["ProdFore"].Value;
            recOper_d.Fields["EqID"].Value = recoper.Fields["V6"].Value;
            str_v = Convert.ToString(recoper.Fields["v1"].Value);
            recOper_d.Fields["Percentassign"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["v2"].Value);
            recOper_d.Fields["EqSetupTime"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["v3"].Value);
            recOper_d.Fields["EqRunTime"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["v4"].Value);
            recOper_d.Fields["LabSetupTime"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["v5"].Value);
            recOper_d.Fields["LabRunTime"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);

            str_v = Convert.ToString(recoper.Fields["v31"].Value);
            recOper_d.Fields["eqSetupPiece"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["V32"].Value);
            recOper_d.Fields["eqSetupTbatch"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["V33"].Value);
            recOper_d.Fields["eqrunlot"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["V34"].Value);
            recOper_d.Fields["eqRunTbatch"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["V35"].Value);
            recOper_d.Fields["labSetupPiece"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["V36"].Value);
            recOper_d.Fields["labSetupTbatch"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["V37"].Value);
            recOper_d.Fields["labrunlot"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            str_v = Convert.ToString(recoper.Fields["V38"].Value);
            recOper_d.Fields["labRunTbatch"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);

            recOper_d.Update();


        }; // end for
        //--------------------------------------------------------------------------------------------------------
        DbUse.CloseAdoRec(recoper);
        recoper = null;
        DbUse.CloseAdoRec(recOper_d);
        recOper_d = null;


        // return; //exit _MakeActualValues:
        return; //exit  Sub;

        //Err_MakeActualValues:
        //zz msgbox Error$, 0, appl_name;
        //RESUME NEXT

    } // end sub;


    public void MakeActualroute_p(long prodid)
    {
        //on error GoTo Err_MakeActualValues;

        //Define variables;


        ADODB.Recordset recRoute_d = null;

        ADODB.Recordset recRoute = null;

        long i;
        long Count;
        string strsql;
        string StrSQL_R;
        string str_v;


        //public string[] qstrArry = new string[40];

        qstrArry[0] = "Junk";
        qstrArry[1] = "PercentAssign";
        qstrArry[2] = "EqSetupTime";
        qstrArry[3] = "EqRunTime";
        qstrArry[4] = "LabSetupTime";
        qstrArry[5] = "LabRunTime";
        qstrArry[6] = "Equip";
        qstrArry[7] = getname("O1");
        qstrArry[8] = getname("O2");
        qstrArry[9] = getname("O3");
        qstrArry[10] = getname("O4");
        qstrArry[11] = getname("G1");
        qstrArry[12] = getname("G2");
        qstrArry[13] = getname("G3");
        qstrArry[14] = getname("G4");
        qstrArry[15] = getname("L1");
        qstrArry[16] = getname("L2");
        qstrArry[17] = getname("L3");
        qstrArry[18] = getname("L4");
        qstrArry[19] = getname("E1");
        qstrArry[20] = getname("E2");
        qstrArry[21] = getname("E3");
        qstrArry[22] = getname("E4");
        qstrArry[23] = getname("P1");
        qstrArry[24] = getname("P2");
        qstrArry[25] = getname("P3");
        qstrArry[26] = getname("P4");
        qstrArry[27] = "demand";
        qstrArry[28] = "lotsize";
        qstrArry[29] = "transferbatch";
        qstrArry[30] = "variability";

        qstrArry[31] = "eqSetupPiece";
        qstrArry[32] = "eqSetupTbatch";
        qstrArry[33] = "eqRunLot";
        qstrArry[34] = "eqRunTbatch";
        qstrArry[35] = "labSetupPiece";
        qstrArry[36] = "labSetupTbatch";
        qstrArry[37] = "labRunLot";
        qstrArry[38] = "labRunTbatch";



        //'Set variables;
        StrSQL_R = "SELECT DISTINCTROW tblOperFrTo.RecID as recid, tblOperFrTo.OpNumF as opnumf, tblOperFrTo.OpNumT as opnumT, tblOperFrTo.Per as per, tblOperFrTo.PartFore as partfore, qfltOperCalc.V1, qfltOperCalc.V2, qfltOperCalc.V3, qfltOperCalc.V4, qfltOperCalc.V5, qfltOperCalc.V6, qfltOperCalc.V7, qfltOperCalc.V8, qfltOperCalc.V9, qfltOperCalc.V10, qfltOperCalc.V11, qfltOperCalc.V12, qfltOperCalc.V13, qfltOperCalc.V14, qfltOperCalc.V15, qfltOperCalc.V16, qfltOperCalc.V17, qfltOperCalc.V18, qfltOperCalc.V19, qfltOperCalc.V20, qfltOperCalc.V21, qfltOperCalc.V22, qfltOperCalc.V23, qfltOperCalc.V24, qfltOperCalc.V25, qfltOperCalc.V26, qfltOperCalc.V27, qfltOperCalc.V28, qfltOperCalc.V29, qfltOperCalc.V30 FROM tblOperFrTo INNER JOIN qfltOperCalc ON tblOperFrTo.OpNumF = qfltOperCalc.OpID WHERE ((tblOperFrTo.PartFore= " + prodid + " )); ";

        runsqlado("UPDATE tblprodfore SET tblprodfore.include = 0;");
        runsqlado("UPDATE tblprodfore SET tblprodfore.include = -1 WHERE (((tblprodfore.ProdID)=" + prodid + "));");


        DbUse.open_ado_rec(globaldb, ref recRoute, StrSQL_R);

        DbUse.open_ado_rec(globaldb, ref recRoute_d, "tblOpFrTo_d");


        string[] qvalues = new string[40];



        //--------------------------------------------------------------------------------------------------------

        //'Get the number of records in table;

        Count = get_count(StrSQL_R, -1);
        if (Count > 0)
        {
            recRoute.MoveFirst();
        }

        for (i = 0; i < Count; i++)
        {


            qvalues[0] = "0";
            qvalues[1] = (string)CDbl_ndi((string)recRoute.Fields["v1"].Value);
            qvalues[2] = (string)CDbl_ndi((string)recRoute.Fields["v2"].Value);
            qvalues[3] = (string)CDbl_ndi((string)recRoute.Fields["v3"].Value);
            qvalues[4] = (string)CDbl_ndi((string)recRoute.Fields["v4"].Value);
            qvalues[5] = (string)CDbl_ndi((string)recRoute.Fields["v5"].Value);
            qvalues[6] = Convert.ToString(recRoute.Fields["V6"].Value);
            qvalues[7] = (string)CDbl_ndi((string)recRoute.Fields["V7"].Value);
            qvalues[8] = (string)CDbl_ndi((string)recRoute.Fields["V8"].Value);
            qvalues[9] = (string)CDbl_ndi((string)recRoute.Fields["V9"].Value);
            qvalues[10] = (string)CDbl_ndi((string)recRoute.Fields["V10"].Value);
            qvalues[11] = (string)CDbl_ndi((string)recRoute.Fields["V11"].Value);
            qvalues[12] = (string)CDbl_ndi((string)recRoute.Fields["V12"].Value);
            qvalues[13] = (string)CDbl_ndi((string)recRoute.Fields["V13"].Value);
            qvalues[14] = (string)CDbl_ndi((string)recRoute.Fields["V14"].Value);
            qvalues[15] = (string)CDbl_ndi((string)recRoute.Fields["V15"].Value);
            qvalues[16] = (string)CDbl_ndi((string)recRoute.Fields["V16"].Value);
            qvalues[17] = (string)CDbl_ndi((string)recRoute.Fields["V17"].Value);
            qvalues[18] = (string)CDbl_ndi((string)recRoute.Fields["V18"].Value);
            qvalues[19] = (string)CDbl_ndi((string)recRoute.Fields["V19"].Value);
            qvalues[20] = (string)CDbl_ndi((string)recRoute.Fields["V20"].Value);
            qvalues[21] = (string)CDbl_ndi((string)recRoute.Fields["V21"].Value);
            qvalues[22] = (string)CDbl_ndi((string)recRoute.Fields["V22"].Value);
            qvalues[23] = (string)CDbl_ndi((string)recRoute.Fields["V23"].Value);
            qvalues[24] = (string)CDbl_ndi((string)recRoute.Fields["V24"].Value);
            qvalues[25] = (string)CDbl_ndi((string)recRoute.Fields["V25"].Value);
            qvalues[26] = (string)CDbl_ndi((string)recRoute.Fields["V26"].Value);
            qvalues[28] = Convert.ToString((float)recRoute.Fields["V28"].Value);
            qvalues[29] = Convert.ToString((float)recRoute.Fields["v29"].Value);
            qvalues[30] = Convert.ToString((float)recRoute.Fields["v30"].Value);

            recRoute_d.AddNew();
            recRoute_d.Fields["recid"].Value = recRoute.Fields["recid"].Value;
            recRoute_d.Fields["PartFore"].Value = recRoute.Fields["PartFore"].Value;
            recRoute_d.Fields["OpNumF"].Value = recRoute.Fields["OpNumF"].Value;
            recRoute_d.Fields["OpNumT"].Value = recRoute.Fields["OpNumT"].Value;


            // gwwd  
            Part_err_id = (int)recRoute.Fields["partfore"].Value;
            Oper_err_id = -1 * (int)recRoute.Fields["OpNumF"].Value;
            Part_err_name = Part_err_name = find_item(Part_err_id, 0, product);
            Oper_err_name = find_item((int)recRoute.Fields["OpNumF"].Value, Part_err_id, oper);

            str_v = Convert.ToString(recRoute.Fields["per"].Value);
            recRoute_d.Fields["per"].Value = CalcNumber(str_v, ref qstrArry, ref qvalues, 39);
            recRoute_d.Update();

            recRoute.MoveNext();
        }; // end for
        DbUse.CloseAdoRec(recRoute);
        recRoute = null;
        DbUse.CloseAdoRec(recRoute_d);
        recRoute_d = null;
        // return; //exit _MakeActualValues:
        return; //exit  Sub;

        //Err_MakeActualValues:
        //zz msgbox Error$, 0, appl_name;
        //RESUME NEXT

    } // end sub;

    public short ValidateData()
    {
        //on error goto Err_ValidateData;

        //Define variable;
        short fstatus;
        short fError;
        short fWarn;
        short intError = -1;
        short intWarn = -1;
        string strmsg;
        string strQuestion;
        string strsql;
        short intType;
        short fresponse;
        short fValid;

        short retval;

        short i;

        //Default variables;
        retval = 0;
        fValid = 0;
        fstatus = 0;
        fError = 0;
        fWarn = 0;



        //' ' displayMessage("Verifying Data Model");

        //Delete all records from the zstblErrors (Errors Table);
        //before the new verify begins;
        strsql = "delete zstblErrors.Level, zstblErrors.Table, zstblErrors.Text FROM zstblErrors;";

        runsqlado(strsql);

        //' ' displayMessage("Verifying Data Model - Labor Data ");
        //Validate the Labor Group Table;
        // { test the results;
        fstatus = ValidateLaborGroup(ref intError, ref intWarn);

        if (fstatus == 0)
        {
            if (fError == 0)
            {
                fError = intError;
            };

            if (fWarn == 0)
            {
                fWarn = intWarn;
            };
        };

        //' ' displayMessage("Verifying Data Model - Equipment Data ");
        //Validate the Equipment Table;
        // { test the results;
        fstatus = ValidateEquipGroup(ref intError, ref intWarn);


        if (fstatus == 0)
        {
            if (fError == 0)
            {
                fError = intError;
            };

            if (fWarn == 0)
            {
                fWarn = intWarn;
            };
        };

        //' ' displayMessage("Verifying Data Model - Products Data ");
        //Validate the Production Forecast Table;
        // { test the results;
        fstatus = ValidateProdFore(ref intError, ref intWarn);

        if (fstatus == 0)
        {
            if (fError == 0)
            {
                fError = intError;
            };

            if (fWarn == 0)
            {
                fWarn = intWarn;
            };
        };

        //' ' displayMessage("Verifying Data Model - Operations Data ");
        //Validate the Operations Table;
        // { test the results;


        fstatus = ValidateOperation(intError, intWarn);

        if (fstatus == 0)
        {
            if (fError == 0)
            {
                fError = intError;
            };

            if (fWarn == 0)
            {
                fWarn = intWarn;
            };
        };


        //' ' displayMessage("Verifying Data Model - Routing Data ");
        //Validate the To From Operation Table;
        // { test the results;
        FixDuplicateOperNameRoutings();
        fstatus = ValidateOperFrTo(intError, intWarn);

        if (fstatus == 0)
        {
            if (fError == 0)
            {
                fError = intError;
            };

            if (fWarn == 0)
            {
                fWarn = intWarn;
            };
        };


        //' ' displayMessage("Verifying Data Model - IBOM Data ");
        //Validate the Production Assignment Table;
        // { test the results;
        fstatus = ValidateIbom(intError, intWarn);

        if (fstatus == 0)
        {
            if (fError == 0)
            {
                fError = intError;
            };

            if (fWarn == 0)
            {
                fWarn = intWarn;
            };
        };

        MakeActualoper_all();
        MakeActualroute_all();
        //'  raise error tomorrow  ERRORMSG  to user over web ???;

        //    user flag !!!  & exit AutoPostBackControl to runsql choices page 

        if ((fError == -1) || (fWarn == -1) || ((calc_return & ERR_FLAG) > 0))
        {
            fValid = 0;
            strmsg = "The Data Did Not Verify" + "\r\n";

            if (fWarn == -1)
            {
                strmsg = strmsg + "There are Warnings in The Data Verification" + "\r\n";
                strQuestion = "Would You Like to Continue Without Reviewing Warnings?";
                intType = 36;
                verify_needed = -1;
                saveVerify_needed();
                model_modified = -1;
                saveModel_modified();

            };



            if (fError == -1)
            {
                strmsg = strmsg + "There are Errors in The Data Verification" + "\r\n";
                strQuestion = "You Cannot Calculate The Results Without Fixing The Errors";
                intType = 16;
                verify_needed = -1;
                saveVerify_needed();
                model_modified = -1;
                saveModel_modified();
            };
        }
        else
        {
            fValid = -1;
            strmsg = "There Are No Errors Or Warnings.";
            strQuestion = " ";
            intType = 64;
            verify_needed = 0;
            saveVerify_needed();

        };


        // ''strmsg = strmsg + strQuestion;



        //  return_code = Vaildate_err;




        retval = fValid;

        // return; //exit _ValidateData:
        return retval; //exit  Function;

        //err_ValidateData: ;
        //' msgbox(ErrorToString(), 0, appl_name);
        //' resume   return; //exit _ValidateData;
    } // end function;
}
//End Class;
