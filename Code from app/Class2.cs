using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Class2
/// </summary>
public class Class2: ClassE
{
	public Class2(string localdir):base(localdir) {
        
    }

    public void dowhatif_all_start()
    {
        dowhatif_General_start();
        dowhatif_Labor_start();
        dowhatif_Equip_start();
        dowhatif_Prod_start();
        dowhatif_IBOM_start();
        dowhatif_Oper_start();
        dowhatif_Route_start();

        return;
    }


    public void dowhatif_all_end()
    {
        runsqlado("DELETE * FROM zstblwhatifaudit;");
        dowhatif_General_end();
        dowhatif_Labor_end();
        dowhatif_Equip_end();
        dowhatif_Prod_end();
        dowhatif_IBOM_end();
        dowhatif_Oper_end();
        dowhatif_Route_end();

        return;
    }


    public void dowhatif_General_start()
    {


        string str11;
        runsqlado("Delete * from zztblgeneral;");
        str11 = " INSERT INTO zztblgeneral ( GeneralID, Title, TUProd, TULT, TUFor, Coef_v_Labor, Coef_v_Parts, RTU1b, RTU1c, Utlimit, Coef_v_Equip ) " +
               " SELECT tblGeneral.generalid, tblGeneral.Title, tblGeneral.TUProd, tblGeneral.TULT, tblGeneral.TUFor, tblGeneral.Coef_v_Labor, tblGeneral.Coef_v_Parts, tblGeneral.RTU1b, tblGeneral.RTU1c, tblGeneral.Utlimit, tblGeneral.Coef_v_Equip " + 
               "FROM tblGeneral;  ";
        runsqlado(str11);
    }

    public void dowhatif_General_end()
    {


        ADODB.Recordset recdata = null;
        string strtbl;
        int fstatus;

        
        
        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.title AS v1, tblGeneral.Title AS v2, 'Title' AS Fa, 'Title' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   10 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.title)<>[zztblgeneral].[title]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.TUprod AS v1, tblGeneral.TUprod AS v2, 'TUprod' AS Fa, 'Oper. Time Unit' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   10 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.TUprod)<>[zztblgeneral].[TUprod]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.TULT AS v1, tblGeneral.TULT AS v2, 'TULT' AS Fa, 'MCT units' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   10 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.TULT)<>[zztblgeneral].[TULT]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.TUprod AS v1, tblGeneral.TUfor AS v2, 'TUfor' AS Fa, 'Forecast/Demand Period Time Units' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   10 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.TUfor)<>[zztblgeneral].[TUfor]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.rtu1b AS v1, tblGeneral.rtu1b AS v2, 'rtu1b' AS Fa, 'MCTs in 1 Forecast period' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   6 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.rtu1b)<>[zztblgeneral].[rtu1b]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = recdata.Fields["v1"].Value.ToString();
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.rtu1c AS v1, tblGeneral.rtu1c AS v2, 'rtu1c' AS Fa, 'Oper Time Units in 1 MCT' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   6 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.rtu1c)<>[zztblgeneral].[rtu1c]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = recdata.Fields["v1"].Value.ToString();
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.coef_v_labor AS v1, tblGeneral.coef_v_labor AS v2, 'coef_v_labor' AS Fa, 'Labor Variability' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   6 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.coef_v_labor)<>[zztblgeneral].[coef_v_labor]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.coef_v_equip AS v1, tblGeneral.coef_v_equip AS v2, 'coef_v_equip' AS Fa, 'Equipment Time Variability' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   6 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.coef_v_equip)<>[zztblgeneral].[coef_v_equip]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.coef_v_parts AS v1, tblGeneral.coef_v_parts AS v2, 'coef_v_parts' AS Fa, 'Product Start Variability' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   6 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.coef_v_parts)<>[zztblgeneral].[coef_v_parts]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = "SELECT zztblgeneral.generalid as recid, zztblGeneral.Utlimit AS v1, tblGeneral.Utlimit AS v2, 'Utlimit' AS Fa, 'Utilization Limit' AS Fe, 'General Data' AS Te, 'tblgeneral' AS Ta,   6 AS type FROM tblgeneral INNER JOIN zztblgeneral ON tblgeneral.generalID = zztblgeneral.generalID WHERE (((tblgeneral.Utlimit)<>[zztblgeneral].[Utlimit]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }


        if (recdata != null)
        {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }
    }





    public void dowhatif_Labor_start()
    {


        string str11;
        runsqlado("Delete * from zztbllabor;");
        str11 = "INSERT INTO zztblLabor ( LaborID, LaborDesc, LaborDept, GrpSiz, Setup, Run, Varbility, LabComment, Abst, OT, L2, PriorityShare, L3, L1, L4 ) " +
               " SELECT tblLabor.LaborID, tblLabor.LaborDesc, tblLabor.LaborDept, tblLabor.GrpSiz, tblLabor.Setup, tblLabor.Run, tblLabor.Varbility, tblLabor.LabComment, tblLabor.Abst, tblLabor.OT, tblLabor.L2, tblLabor.PriorityShare, tblLabor.L3, tblLabor.L1, tblLabor.L4 " +
               " FROM tblLabor WHERE (((tblLabor.LaborDesc)<>'none' ));";
        runsqlado(str11);
    }


    public void dowhatif_Labor_end() {


        ADODB.Recordset recdata = null;
        string strtbl;
        int fstatus;

   /// ???  gwwd     setGlobalVar();  //  sets glngwid etc. 


        strtbl = "SELECT zztblLabor.LaborDept AS v1, tblLabor.LaborDept AS v2, 'laborDept' AS Fa,  [zztbllabor].[LaborDesc] & ' Labor Dept' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid,  10 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.LaborDept)<>[zztbllabor].[labordept]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblLabor.[LabComment] AS v1, tblLabor.[LabComment] AS v2, 'labcomment' AS Fa,  [zztbllabor].[LaborDesc] & ' Labor Comment' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 10 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE ((([tbllabor].[LabComment])<>[zztbllabor].[labcomment]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblLabor.[L1] AS v1, tblLabor.[L1] AS v2, 'l1' AS Fa, ' [zztbllabor].[LaborDesc] &  Labor Parameter L1' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 10 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[L1])<>[zztbllabor].[l1]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblLabor.[L2] AS v1, tblLabor.[L2] AS v2, 'L2' AS Fa,  [zztbllabor].[LaborDesc] & ' Labor Parameter L2' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 10 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[L2])<>[zztbllabor].[L2]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblLabor.[L3] AS v1, tblLabor.[L3] AS v2, 'L3' AS Fa,  [zztbllabor].[LaborDesc] &  ' Labor Parameter L3' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 10 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[L3])<>[zztbllabor].[L3]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblLabor.[L4] AS v1, tblLabor.[L4] AS v2, 'L4' AS Fa,  [zztbllabor].[LaborDesc] & ' Labor Parameter L4' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 10 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[L4])<>[zztbllabor].[L4]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblLabor.[setup] AS v1, tblLabor.[setup] AS v2, 'setup' AS Fa,  [zztbllabor].[LaborDesc] & ' Setup Time Multipler' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 6 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[setup])<>[zztbllabor].[setup]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblLabor.[run] AS v1, tblLabor.[run] AS v2, 'run' AS Fa,  [zztbllabor].[LaborDesc] & ' Run Time Multipler' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 6 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[run])<>[zztbllabor].[run]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblLabor.[OT] AS v1, tblLabor.[OT] AS v2, 'OT' AS Fa,  [zztbllabor].[LaborDesc] & ' Overtime %' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 6 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[OT])<>[zztbllabor].[OT]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblLabor.[Abst] AS v1, tblLabor.[Abst] AS v2, 'Abst' AS Fa,  [zztbllabor].[LaborDesc] & ' Absenteeism %' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 6 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[Abst])<>[zztbllabor].[Abst]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }


        strtbl = " SELECT zztblLabor.[GrpSiz] AS v1, tblLabor.[GrpSiz] AS v2, 'GrpSiz' AS Fa,  [zztbllabor].[LaborDesc] & ' No. in Group (Size)' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 6 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[GrpSiz])<>[zztbllabor].[GrpSiz]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblLabor.[varbility] AS v1, tblLabor.[varbility] AS v2, 'varbility' AS Fa,  [zztbllabor].[LaborDesc] &  ' Labor Only Variability Multipler' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 6 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[varbility])<>[zztbllabor].[varbility]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblLabor.[priorityshare] AS v1, tblLabor.[priorityshare] AS v2, 'priorityshare' AS Fa,  [zztbllabor].[LaborDesc] & ' Prioirtize Labor Use/Sharing' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, 1 AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.[priorityshare])<>[zztbllabor].[priorityshare]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {

            globNVal = Convert.ToString((bool)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((bool)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        if (recdata != null) {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }

    }




    public void dowhatif_Equip_start()
    {


        string str11;
        
        runsqlado("Delete * from zztblEquip;");
        str11 = " INSERT INTO zztblEquip ( EquipID, EquipDesc, EquipDept, GrpSiz, MTF, MTR, Setup, Run, Varbility, EqComment, Labor, OT, E1, E2, E3, E4, equiptype ) " +
                 " SELECT tblEquip.EquipID, tblEquip.EquipDesc, tblEquip.EquipDept, tblEquip.GrpSiz, tblEquip.MTF, tblEquip.MTR, tblEquip.Setup, tblEquip.Run, tblEquip.Varbility, tblEquip.EqComment, tblEquip.Labor, tblEquip.OT, tblEquip.E1, tblEquip.E2, tblEquip.E3, tblEquip.E4, tblEquip.equiptype  " +
                 " FROM tblEquip    WHERE (((tblEquip.EquipDesc)<>'none')); ";
        runsqlado(str11);
    }


    public void dowhatif_Equip_end() {


        ADODB.Recordset recdata = null;
        string strtbl;
        int fstatus;



        strtbl = " SELECT zztblEquip.EquipDept AS v1, tblEquip.EquipDept AS v2, 'EquipDept' AS Fa,  [zztblequip].[EquipDesc] & ' Equip Dept' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 10 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.EquipDept)<>[zztblEquip].[Equipdept]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblEquip.[EqComment] AS v1, tblEquip.[EqComment] AS v2, 'EqComment' AS Fa, [zztblequip].[EquipDesc] & ' Equipment Comment' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 10 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblequip.[EqComment])<>[zztblEquip].[Eqcomment]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblEquip.[E1] AS v1, tblEquip.[E1] AS v2, 'E1' AS Fa, [zztblequip].[EquipDesc] & ' Equip Parameter E1' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 10 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[E1])<>[zztblEquip].[E1]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblEquip.[E2] AS v1, tblEquip.[E2] AS v2, 'E2' AS Fa, [zztblequip].[EquipDesc] & ' Equip Parameter E2' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 10 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[E2])<>[zztblEquip].[E2]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblEquip.[E3] AS v1, tblEquip.[E3] AS v2, 'E3' AS Fa, [zztblequip].[EquipDesc] & ' Equip Parameter E3' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 10 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[E3])<>[zztblEquip].[E3]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblEquip.[E4] AS v1, tblEquip.[E4] AS v2, 'E4' AS Fa, [zztblequip].[EquipDesc] & ' quip Parameter E4' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 10 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[E4])<>[zztblEquip].[E4]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblEquip.[setup] AS v1, tblEquip.[setup] AS v2, 'setup' AS Fa, [zztblequip].[EquipDesc] & ' Setup Time Multipler' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 6 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[setup])<>[zztblEquip].[setup]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblEquip.[run] AS v1, tblEquip.[run] AS v2, 'run' AS Fa, [zztblequip].[EquipDesc] & ' Run Time Multipler' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 6 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[run])<>[zztblEquip].[run]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblEquip.[OT] AS v1, tblEquip.[OT] AS v2, 'OT' AS Fa, [zztblequip].[EquipDesc] & ' Overtime %' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 6 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[OT])<>[zztblEquip].[OT]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblEquip.[MTF] AS v1, tblEquip.[MTF] AS v2, 'MTF' AS Fa, [zztblequip].[EquipDesc] & ' Mean Time To (between) Failures' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 6 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[MTF])<>[zztblEquip].[MTF]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblEquip.[MTR] AS v1, tblEquip.[MTR] AS v2, 'MTR' AS Fa, [zztblequip].[EquipDesc] & ' Mean Time To Repair' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 6 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[MTR])<>[zztblEquip].[MTR]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblEquip.[GrpSiz] AS v1, tblEquip.[GrpSiz] AS v2, 'GrpSiz' AS Fa, [zztblequip].[EquipDesc] & ' No. in Group (Size)' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 4 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[GrpSiz])<>[zztblEquip].[GrpSiz]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((short)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblEquip.[varbility] AS v1, tblEquip.[varbility] AS v2, 'varbility' AS Fa, [zztblequip].[EquipDesc] & ' Equip Only Variability Multipler' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 6 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[varbility])<>[zztblEquip].[varbility]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblEquip.[Labor] AS v1, tblEquip.[Labor] AS v2, 'labor' AS Fa, [zztblequip].[EquipDesc] & ' Assigned Labor Group' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 3 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[Labor])<>[zztblEquip].[Labor]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((int)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        // second time adds labor description instead of labor id
        strtbl = " SELECT zztblEquip.[Labor] AS v1, tblEquip.[Labor] AS v2, 'labor' AS Fa, [zztblequip].[EquipDesc] & ' Assigned Labor Group' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 3 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.[Labor])<>[zztblEquip].[Labor]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((int)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        /*strtbl = " SELECT zztblEquip.EquipType AS v1, tblEquip.EquipType AS v2, 'EquipType' AS Fa,  [zztblequip].[EquipDesc] & ' Equip Dept' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, 10 AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.EquipType)<>[zztblEquip].[EquipType]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }*/

        if (recdata != null) {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }

    }

        


    public void dowhatif_Prod_start()
    {


        string str11;
        
        runsqlado("Delete * from zztblprodfore;");
        str11 = "INSERT INTO zztblProdFore ( ProdID, ProdDept, DemandFac, Variability, makestock, TBatchGather, EndDemd, Lotsiz, ProdComment, TransferBatch, [Value], optimizelotsize, optimizetbatch, P4, P3, LotSizeFac, P2, P1 ) "+
                " SELECT tblProdFore.ProdID, tblProdFore.ProdDept, tblProdFore.DemandFac, tblProdFore.Variability, tblProdFore.makestock, tblProdFore.TBatchGather, tblProdFore.EndDemd, tblProdFore.Lotsiz, tblProdFore.ProdComment, tblProdFore.TransferBatch, tblProdFore.Value, tblProdFore.optimizelotsize, tblProdFore.optimizetbatch, tblProdFore.P4, tblProdFore.P3, tblProdFore.LotSizeFac, tblProdFore.P2, tblProdFore.P1 " +
                " FROM tblProdFore; ";
        runsqlado(str11);
    }


    public void dowhatif_Prod_end() {


        ADODB.Recordset recdata = null;
        string strtbl;
        int fstatus;



        strtbl = " SELECT zztblProdFore.ProdDept AS v1, tblProdFore.ProdDept AS v2, 'ProdDept' AS Fa, [tblprodfore].[ProdDesc] & '  Product Dept' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 10 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.ProdDept)<>[zztblProdFore].[Proddept]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[ProdComment] AS v1, tblProdFore.[ProdComment] AS v2, 'ProdComment' AS Fa,  [tblprodfore].[ProdDesc] & ' Product Comment' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 10 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[ProdComment])<>[zztblProdFore].[ProdComment]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[TBatchGather] AS v1, tblProdFore.[TBatchGather] AS v2, 'TBatchGather' AS Fa, [tblprodfore].[ProdDesc] & ' Gather Transfer Batches' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 1 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[TBatchGather])<>[zztblProdFore].[TBatchGather]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((bool)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((bool)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[TransferBatch] AS v1, tblProdFore.[TransferBatch] AS v2, 'TransferBatch' AS Fa, [tblprodfore].[ProdDesc] & ' Transfer Batch Size' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 6 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[TransferBatch])<>[zztblProdFore].[TransferBatch]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[Value] AS v1, tblProdFore.[Value] AS v2, 'Value' AS Fa, [tblprodfore].[ProdDesc] & ' Product Value for Lot size opt.' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 6 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[Value])<>[zztblProdFore].[Value]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((double)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((double)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }


        strtbl = " SELECT zztblProdFore.[optimizelotsize] AS v1, tblProdFore.[optimizelotsize] AS v2, 'optimizelotsize' AS Fa, [tblprodfore].[ProdDesc] & ' Opt.  Lot size' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 1 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[optimizelotsize])<>[zztblProdFore].[optimizelotsize]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((bool)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((bool)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }



        strtbl = " SELECT zztblProdFore.[optimizetbatch] AS v1, tblProdFore.[optimizetbatch] AS v2, 'optimizetbatch' AS Fa, [tblprodfore].[ProdDesc] & ' Opt. Transfer Batch size' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 1 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[optimizetbatch])<>[zztblProdFore].[optimizetbatch]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((bool)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((bool)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }


        strtbl = " SELECT zztblProdFore.[makestock] AS v1, tblProdFore.[makestock] AS v2, 'makestock' AS Fa, [tblprodfore].[ProdDesc] & ' Make To Stock' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 1 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[makestock])<>[zztblProdFore].[makestock]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((bool)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((bool)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[P1] AS v1, tblProdFore.[P1] AS v2, 'P1' AS Fa, [tblprodfore].[ProdDesc] & ' Product Parameter P1' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 10 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[P1])<>[zztblProdFore].[P1]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblProdFore.[P2] AS v1, tblProdFore.[P2] AS v2, 'P2' AS Fa, [tblprodfore].[ProdDesc] & ' Product Parameter P2' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 10 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[P2])<>[zztblProdFore].[P2]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblProdFore.[P3] AS v1, tblProdFore.[P3] AS v2, 'P3' AS Fa, [tblprodfore].[ProdDesc] & ' Product Parameter P3' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 10 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[P3])<>[zztblProdFore].[P3]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblProdFore.[P4] AS v1, tblProdFore.[P4] AS v2, 'P4' AS Fa, [tblprodfore].[ProdDesc] & ' Product Parameter P4' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 10 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[P4])<>[zztblProdFore].[P4]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[LotsizeFac] AS v1, tblProdFore.[LotsizeFac] AS v2, 'LotsizeFac' AS Fa, [tblprodfore].[ProdDesc] & ' Lot size Multipler' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 6 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[LotsizeFac])<>[zztblProdFore].[LotsizeFac]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[DemandFac] AS v1, tblProdFore.[DemandFac] AS v2, 'DemandFac' AS Fa, [tblprodfore].[ProdDesc] & ' Demand Multipler' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 6 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[DemandFac])<>[zztblProdFore].[DemandFac]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[EndDemd] AS v1, tblProdFore.[EndDemd] AS v2, 'EndDemd' AS Fa, [tblprodfore].[ProdDesc] & ' End Use (shipped) Demand' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 6 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[EndDemd])<>[zztblProdFore].[EndDemd]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblProdFore.[Lotsiz] AS v1, tblProdFore.[Lotsiz] AS v2, 'Lotsiz' AS Fa, [tblprodfore].[ProdDesc] & ' Starting Lot Size' as fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 6 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[Lotsiz])<>[zztblProdFore].[Lotsiz]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }




        strtbl = " SELECT zztblProdFore.[variability] AS v1, tblProdFore.[variability] AS v2, 'variability' AS Fa, [tblprodfore].[ProdDesc] & ' Product Start Variability Multipler' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, 6 AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.[variability])<>[zztblProdFore].[variability]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        if (recdata != null) {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }




    }
    
    
    public void dowhatif_Oper_start()
    {


        string str11;
        
        runsqlado("Delete * from zztbloper;");
        str11 = "INSERT INTO zztblOper ( OpID, ProdFore, EqID, OpNum, OpNam, PercentAssign, EqSetupTime, EqRunTime, LabSetupTime, LabRunTime, labRunLot, labSetupTbatch, labSetupPiece, labRunTbatch, eqSetupTbatch, eqSetupPiece, eqRunTbatch, eqRunLot, O1, O2, O3, O4 ) " + 
                " SELECT tblOper.OpID, tblOper.ProdFore, tblOper.EqID, tblOper.OpNum, tblOper.OpNam, tblOper.PercentAssign, tblOper.EqSetupTime, tblOper.EqRunTime, tblOper.LabSetupTime, tblOper.LabRunTime, tblOper.labRunLot, tblOper.labSetupTbatch, tblOper.labSetupPiece, tblOper.labRunTbatch, tblOper.eqSetupTbatch, tblOper.eqSetupPiece, tblOper.eqRunTbatch, tblOper.eqRunLot, tblOper.O1, tblOper.O2, tblOper.O3, tblOper.O4 " +
                " FROM tblOper;  ";
        runsqlado(str11);
    }


    public void dowhatif_Oper_end() {


        ADODB.Recordset recdata = null;
        string strtbl;
        int fstatus;



        strtbl = " SELECT zztbloper.[Eqid] AS v1, tbloper.[Eqid] AS v2, 'eqid' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Assigned Equip Group' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tbloper.opid AS recid, 3 AS type FROM tbloper INNER JOIN zztbloper ON tbloper.opID = zztbloper.opID WHERE (((tbloper.[Eqid])<>[zztbloper].[Eqid]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = Convert.ToString((int)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((int)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.PercentAssign AS v1, tblOper.PercentAssign AS v2, 'PercentAssign' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Percent Assigned to Equipment' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tblOper.PercentAssign)<>[zztbloper].[PercentAssign]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }


        strtbl = " SELECT zztblOper.opnum AS v1, tblOper.opnum AS v2, 'opnum' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Operation Number' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tblOper.opnum)<>[zztbloper].[opnum]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = recdata.Fields["v2"].Value.ToString();
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = recdata.Fields["v1"].Value.ToString();
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[EqrunTime] AS v1, tblOper.[EqrunTime] AS v2, 'EqrunTime' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Equip Run Time' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[EqRunTime])<>[zztbloper].[EqRunTime]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[EqsetupTime] AS v1, tblOper.[EqsetupTime] AS v2, 'EqsetupTime' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Equip Setup Time' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[EqsetupTime])<>[zztbloper].[EqsetupTime]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[EqrunTbatch] AS v1, tblOper.[EqrunTbatch] AS v2, 'EqrunTbatch' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Equip Run Tbatch' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[EqRunTbatch])<>[zztbloper].[EqRunTbatch]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[Eqsetuptbatch] AS v1, tblOper.[Eqsetuptbatch] AS v2, 'Eqsetuptbatch' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Equip Setup Tbatch' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[Eqsetuptbatch])<>[zztbloper].[Eqsetuptbatch]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[Eqrunlot] AS v1, tblOper.[Eqrunlot] AS v2, 'Eqrunlot' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Equip Run Time per lot' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[EqRunlot])<>[zztbloper].[EqRunlot]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[Eqsetuppiece] AS v1, tblOper.[Eqsetuppiece] AS v2, 'Eqsetuppiece' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Equip Setup per piece' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[Eqsetuppiece])<>[zztbloper].[Eqsetuppiece]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }


        strtbl = " SELECT zztblOper.[LabrunTime] AS v1, tblOper.[LabrunTime] AS v2, 'LabrunTime' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Labor Run Time' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[LabRunTime])<>[zztbloper].[LabRunTime]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[LabsetupTime] AS v1, tblOper.[LabsetupTime] AS v2, 'LabsetupTime' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Labor Setup Time' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[LabsetupTime])<>[zztbloper].[LabsetupTime]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[LabrunTbatch] AS v1, tblOper.[LabrunTbatch] AS v2, 'LabrunTbatch' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Labor Run Tbatch' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[LabRunTbatch])<>[zztbloper].[LabRunTbatch]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[Labsetuptbatch] AS v1, tblOper.[Labsetuptbatch] AS v2, 'Labsetuptbatch' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Labor Setup Tbatch' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[Labsetuptbatch])<>[zztbloper].[Labsetuptbatch]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[Labrunlot] AS v1, tblOper.[Labrunlot] AS v2, 'Labrunlot' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Labor Run time per lot' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[LabRunlot])<>[zztbloper].[LabRunlot]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[Labsetuppiece] AS v1, tblOper.[Labsetuppiece] AS v2, 'Labsetuppiece' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Labor Setup per piece' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tbloper.[Labsetuppiece])<>[zztbloper].[Labsetuppiece]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        strtbl = " SELECT zztblOper.[O1] AS v1, tblOper.[O1] AS v2, 'O1' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Operations Parameter O1' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tblOper.[O1])<>[zztbloper].[O1]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblOper.[O2] AS v1, tblOper.[O2] AS v2, 'O2' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Operations Parameter O2' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tblOper.[O2])<>[zztbloper].[O2]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblOper.[O3] AS v1, tblOper.[O3] AS v2, 'O3' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Operations Parameter O3' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tblOper.[O3])<>[zztbloper].[O3]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        strtbl = " SELECT zztblOper.[O4] AS v1, tblOper.[O4] AS v2, 'O4' AS Fa, [tbloper].[ProdDesc] & ' Oper: '& [tbloper].[opnam] & ' Operations Parameter O4' AS Fe, 'Operations' as Te, 'tbloper' AS Ta, tblOper.opid AS recid, 10 AS type FROM tbloper INNER JOIN zztbloper ON tblOper.opid = zztblOper.opid WHERE (((tblOper.[O4])<>[zztbloper].[O4]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        if (recdata != null) {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }
    }
  
 

    

    public void dowhatif_Route_start()
    {


        string str11;
        string str21;
        str21 = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + varlocal + "\\mpxmdb.mdb";     //lucie
        runsqlado("Delete * from zztblOperFrTo;");
        str11 = "INSERT INTO zztblOperFrTo ( RecID, Per ) SELECT tblOperFrTo.RecID, tblOperFrTo.Per FROM tblOperFrTo; ";
        runsqlado(str11);
    }


    public void dowhatif_Route_end() {


        ADODB.Recordset recdata = null;
        string strtbl;
        int fstatus;



        strtbl = " SELECT zztbloperfrto.per AS v1, tbloperfrto.per AS v2, 'per' AS Fa, [tbloperfrto].[ProdDesc] & ' From Oper: '& [tbloperfrto].[fromopname] & ' To Oper: '& [tbloperfrto].[toopname] & ' % routed' AS Fe, 'Routing Data' as Te, 'tbloperfrto' AS Ta, tbloperfrto.recid AS recid, 10 AS type FROM tbloperfrto INNER JOIN zztbloperfrto ON tbloperfrto.recid = zztbloperfrto.recid WHERE (((tbloperfrto.per)<>[zztbloperfrto].[per]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }

        if (recdata != null) {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }


    }

   

    public void dowhatif_IBOM_start()
    {


        string str11;
        
        runsqlado("Delete * from zztblIbom;");
        str11 = "INSERT INTO zztblIbom ( IbomID, UPA ) SELECT tblIbom.IbomID, tblIbom.UPA FROM tblIbom; ";
        runsqlado(str11);
    }


    public void dowhatif_IBOM_end()
    {


        ADODB.Recordset recdata = null;
        string strtbl;
        int fstatus;



        strtbl = " SELECT zztblibom.UPA AS v1, tblibom.UPA AS v2, 'UPA' AS Fa, 'Parent: ' + tblibom.parentname + ' - Component: ' + tblibom.compname + ' Units Per Assembly'  AS Fe, 'IBOM' as Te, 'tblIBOM' AS Ta, tblibom.ibomid AS recid, 6 AS type FROM tblibom INNER JOIN zztblibom ON tblibom.ibomid = zztblibom.ibomid WHERE (((tblibom.upa)<>[zztblibom].[upa]));";

        DbUse.open_ado_rec(globaldb, ref recdata, strtbl);

        while (!recdata.EOF)
        {
            globNVal = Convert.ToString((float)recdata.Fields["v2"].Value);
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = Convert.ToString((float)recdata.Fields["v1"].Value);
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            fstatus = InsertAudit();
            recdata.MoveNext();
        }
        if (recdata != null)
        {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }
     }

    
//-----------------------------------------------------------------------------------------------------

    public int read_id_name_flag ( string flag) {

        int val1;
        string str1;

        ADODB.Recordset recdata = null;
        
        
        str1 = "SELECT zstblname_id_flags.Fvalue, zstblname_id_flags.Flag FROM zstblname_id_flags WHERE (((zstblname_id_flags.Flag)=" + flag + ")); ";
        DbUse.open_ado_rec(globaldb, ref recdata, str1);

        val1 = (int) recdata.Fields["fvalue"].Value;
        return val1;
    }


    public void write_id_name_flag(string flag, int val1)
    {
        string str1;

          str1 = "UPDATE zstblname_id_flags SET zstblname_id_flags.Fvalue = " + val1 +" WHERE (((zstblname_id_flags.Flag)=" + flag + "));";

        runsqlado(str1);
        return;
    }

    public void clear_id_name_flags() { 
          string str1;

          str1 = "UPDATE zstblname_id_flags SET zstblname_id_flags.Fvalue = -1;";
                  
        runsqlado(str1);
        return;
    }

    //  clear_id_name_flags();    called at model load
    //  do_all_id_name_flags();     do at all page loads ? do before save, do before whatif , do before run   do at all input page load steps 

    public void do_all_id_name_flags()
    {
        int val1;
        string flag;
        string str1;

        ADODB.Recordset recdata = null;


        str1 = "SELECT zstblname_id_flags.Fvalue, zstblname_id_flags.Flag FROM zstblname_id_flags order by id; ";
        DbUse.open_ado_rec(globaldb, ref recdata, str1);

        val1 = (int)recdata.Fields["fvalue"].Value;
        flag = (string)recdata.Fields["flag"].Value;
        while (recdata.EOF != true)
        {
            switch (flag)
            {
                case ("Lflag"):
                    {
                        if (val1 == 0)
                        {  //  using lab names in equipment  
                            str1 = "UPDATE tblEquip INNER JOIN tblLabor ON tblEquip.LaborDesc = tblLabor.LaborDesc SET tblEquip.Labor = [tbllabor].[laborid];";
                            runsqlado(str1);
                        }
                        else if (val1 == 1)
                        {  //  using laborids in equipment 
                            str1 = "UPDATE tblEquip INNER JOIN tblLabor ON tblEquip.Labor = tblLabor.LaborID SET tblEquip.LaborDesc = [tblLabor].[LaborDesc];";
                            runsqlado(str1);
                        }
                    } break;
                case ("Eflag"):
                    {
                        if (val1 == 0)
                        {  //  using eq names in opers  
                            str1 = "UPDATE tblEquip INNER JOIN tblOper ON tblEquip.EquipDesc = tblOper.EquipDesc SET tblOper.EqID = [tblEquip].[equipid];";
                            runsqlado(str1);
                        }
                        else if (val1 == 1)
                        {  //  using eqids in opers 
                            str1 = "UPDATE tblEquip INNER JOIN tblOper ON tblEquip.Equipid = tblOper.EqID SET tblOper.Equipdesc = [tblEquip].[equipdesc]; ";
                            runsqlado(str1);
                        }
                    } break;
                case ("P1flag"):
                    {
                        if (val1 == 0)
                        {  //  using prod names in opers  
                            str1 = "UPDATE tblOper INNER JOIN tblProdFore ON tblOper.ProdDesc = tblProdFore.ProdDesc SET tblOper.ProdFore = [tblProdFore].[prodid];";
                            runsqlado(str1);
                        }
                        else if (val1 == 1)
                        {  //  using prodids in opers 
                            str1 = "UPDATE tblOper INNER JOIN tblProdFore ON tblOper.ProdFore = tblProdFore.ProdID SET tblOper.ProdDesc = [tblProdFore].[proddesc];";
                            runsqlado(str1);
                        }
                    } break;
                case ("P2flag"):
                    {
                        if (val1 == 0)
                        {  //  using prod names in routing  
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblProdFore ON tblOperFrTo.ProdDesc = tblProdFore.ProdDesc SET tblOperFrTo.PartFore = [tblProdFore].[prodid];";
                            runsqlado(str1);
                        }
                        else if (val1 == 1)
                        {  //  using prodids in routing 
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblProdFore ON tblOperFrTo.PartFore = tblProdFore.ProdID SET tblOperFrTo.ProdDesc = [tblProdFore].[proddesc];";
                            runsqlado(str1);
                        }
                    } break;
                case ("P3flag"):
                    {
                        if (val1 == 0)
                        {  //  using prod names in ibom  
                            str1 = "UPDATE tblProdFore INNER JOIN tblIbom ON tblProdFore.ProdDesc = tblIbom.ParentName SET tblIbom.ParentID = [tblProdFore].[prodid];";
                            runsqlado(str1);             
                            str1 = "UPDATE tblIbom INNER JOIN tblProdFore ON tblIbom.compName = tblProdFore.ProdDesc SET tblIbom.CompID = [tblProdFore].[prodid];";
                            runsqlado(str1);

                        }
                        else if (val1 == 1)
                        {  //  using prodids in ibom 
                            str1 = "UPDATE tblProdFore INNER JOIN tblIbom ON tblProdFore.ProdID = tblIbom.ParentID SET tblIbom.ParentName = [tblProdFore].[proddesc];";
                            runsqlado(str1);
                            str1 = "UPDATE tblIbom INNER JOIN tblProdFore ON tblIbom.CompID = tblProdFore.ProdID SET tblIbom.compName = [tblProdFore].[proddesc];";
                            runsqlado(str1);
                        }
                    } break;
                case ("Oflag"):
                    {
                        if (val1 == 0)
                        {  //  using oper names in routing  
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblOper ON (tblOperFrTo.PartFore = tblOper.ProdFore) AND (tblOperFrTo.fromopname = tblOper.OpNam) SET tblOperFrTo.OpNumF = [tbloper].[OpID];";
                            runsqlado(str1);
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblOper ON (tblOperFrTo.ToOpName = tblOper.OpNam) AND (tblOperFrTo.PartFore = tblOper.ProdFore) SET tblOperFrTo.OpNumT = [tbloper].[OpID];";
                            runsqlado(str1);
                        }
                        else if (val1 == 1)
                        {  //  using operids in routing 
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblOper ON tblOperFrTo.OpNumF = tblOper.OpID SET tblOperFrTo.fromopname = [tbloper].[opnam];";
                            runsqlado(str1);
                            str1 = "UPDATE tblOperFrTo INNER JOIN tblOper ON tblOperFrTo.OpNumT = tblOper.OpID SET tblOperFrTo.ToOpName = [tbloper].[opnam];";
                            runsqlado(str1);
                        }
                    } break;   

            }

            recdata.MoveNext();
        }

        if (recdata != null)
        {
            DbUse.CloseAdoRec(recdata);
            recdata = null;
        }

        clear_id_name_flags();
    }

    /*\   where to insert calls 
     * 
     1.     from new_choices     Clear_errors_results()    model:   openmodel , newmodel, clearmodel ,   
     *                                               
     *                                                    see whatif_c.aspx.cs  look for xxxc1    
     *                                                          Whatif load/unload 
     *                                                          whatif page 1   Load
     *                                                          whatif page 2 return to base case 
     *                                                      
     * 
     * 
       
   
    2. dowhatif_all_start()  at whatif page view1 start , whatif page view2 start 
    3. dowhatif_all_end()    at whatif page view2 start, view4 start,    Models start
     * 
    4. do_all_id_name_flags() at start of all pages !!
    5. write_id_name_flag(string flag, int val1)  at start of approp input pages 
     * 
     *  at labor   (after 4.  
     *      write(id_name_fag("L_Flag", 1);
     *      
     * At equip 
     *      write(id_name_fag("L_Flag", 0);
     *      write(id_name_fag("E_Flag", 1);
     *      
     * At product names  
     *      write(id_name_fag("P1_Flag", 1);
     *      write(id_name_fag("P2_Flag", 1);
     *      write(id_name_fag("P3_Flag", 1);
     *      
     * at ibom page 
     *      write(id_name_fag("P3_Flag", 0);
     *     
     *  At opers 
     *      write(id_name_fag("E_Flag", 0);
     *      write(id_name_fag("P1_Flag", 0);
     *      write(id_name_fag("O_Flag", 1);
     *      
     *  At route 
     *      write(id_name_fag("O_Flag", 0);
     *      write(id_name_fag("P2_Flag", 0);

     *        
     * */
}