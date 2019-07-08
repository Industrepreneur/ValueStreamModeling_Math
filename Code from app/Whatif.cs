using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Whatif
/// </summary>
public class Whatif : ClassC0 {

    int whatifType;
    string database;

    struct WhatifType {
        public const int LABOR = 0;
        public const int EQUIPMENT = 1;
        public const int PRODUCTS = 2;
    }

    public Whatif(string localdir, string database, int whatifType)
        : base(localdir) {
        this.whatifType = whatifType;
        this.database = database;
    }

    public void startWhatif() {
        string connectionString = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source = " + localdir + "\\" + database;
        string commandString = "";
        string table = "";
        switch (whatifType) {
            case WhatifType.LABOR:
                table = "zztblLabor";
                DbUse.RunSql("DELETE * FROM " + table, connectionString);
                commandString = "INSERT INTO zztblLabor ( LaborID, LaborDesc, LaborDept, GrpSiz, Setup, Run, Varbility, LabComment, Abst, OT, L2, PriorityShare, L3, L1, L4 ) " +
               " SELECT tblLabor.LaborID, tblLabor.LaborDesc, tblLabor.LaborDept, tblLabor.GrpSiz, tblLabor.Setup, tblLabor.Run, tblLabor.Varbility, tblLabor.LabComment, tblLabor.Abst, tblLabor.OT, tblLabor.L2, tblLabor.PriorityShare, tblLabor.L3, tblLabor.L1, tblLabor.L4 " +
               " FROM tblLabor WHERE (((tblLabor.LaborDesc)<>'none' ));";
                break;
            case WhatifType.EQUIPMENT:
                table = "zztblEquip";
                DbUse.RunSql("DELETE * FROM " + table, connectionString);
                commandString = " INSERT INTO zztblEquip ( EquipID, EquipDesc, EquipDept, GrpSiz, MTF, MTR, Setup, Run, Varbility, EqComment, Labor, OT, E1, E2, E3, E4 ) " +
                " SELECT tblEquip.EquipID, tblEquip.EquipDesc, tblEquip.EquipDept, tblEquip.GrpSiz, tblEquip.MTF, tblEquip.MTR, tblEquip.Setup, tblEquip.Run, tblEquip.Varbility, tblEquip.EqComment, tblEquip.Labor, tblEquip.OT, tblEquip.E1, tblEquip.E2, tblEquip.E3, tblEquip.E4 " +
                " FROM tblEquip    WHERE (((tblEquip.EquipDesc)<>'none')); ";
                break;
            case WhatifType.PRODUCTS:
                DbUse.RunSql("DELETE * FROM " + table, connectionString);
                commandString = "INSERT INTO zztblProdFore ( ProdID, ProdDept, DemandFac, Variability, makestock, TBatchGather, EndDemd, Lotsiz, ProdComment, TransferBatch, [Value], optimizelotsize, optimizetbatch, P4, P3, LotSizeFac, P2, P1 ) " +
                " SELECT tblProdFore.ProdID, tblProdFore.ProdDept, tblProdFore.DemandFac, tblProdFore.Variability, tblProdFore.makestock, tblProdFore.TBatchGather, tblProdFore.EndDemd, tblProdFore.Lotsiz, tblProdFore.ProdComment, tblProdFore.TransferBatch, tblProdFore.Value, tblProdFore.optimizelotsize, tblProdFore.optimizetbatch, tblProdFore.P4, tblProdFore.P3, tblProdFore.LotSizeFac, tblProdFore.P2, tblProdFore.P1 " +
                " FROM tblProdFore; ";
                break;
            default:
                break;
        }
        DbUse.RunSql(commandString, connectionString);
    }

    public void endWhatif() {
        switch (whatifType) {
            case (WhatifType.LABOR):
                endWhatif_Labor();
                break;
            case (WhatifType.EQUIPMENT):
                endWhatif_Equipment();
                break;
            case (WhatifType.PRODUCTS):
                endWhatif_Products();
                break;
            default: break;
        }
    }

    public void InsertWhatifRecord(string selectedTableValues, ref ADODB.Recordset recdata) {

        DbUse.open_ado_rec(globaldb, ref recdata, selectedTableValues);

        while (!recdata.EOF) {
            globNVal = (string)recdata.Fields["v2"].Value;
            globTNameE = (string)recdata.Fields["Te"].Value;
            globTNameA = (string)recdata.Fields["Ta"].Value;
            globrecid = (int)recdata.Fields["recid"].Value;
            globOVal = (string)recdata.Fields["v1"].Value;
            globFNameE = (string)recdata.Fields["Fe"].Value;
            globDType = (int)recdata.Fields["type"].Value;
            globFNameA = (string)recdata.Fields["Fa"].Value;
            InsertAudit();
            recdata.MoveNext();
        }

    }

    public void endWhatif_Labor() {
        ADODB.Recordset recdata = null;
        string selectedTableValues;

        selectedTableValues = "SELECT zztblLabor.LaborDept AS v1, tblLabor.LaborDept AS v2, 'laborDept' AS Fa, 'Labor Dept' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '10' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((tblLabor.LaborDept)<>[zztbllabor].[labordept]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[LaborComment] AS v1, tblLabor.[LaborComment] AS v2, 'laborcomment' AS Fa, 'Labor Comment' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '10' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE ((([LaborÇomment])<>[zztbllabor].[laborcomment]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[L1] AS v1, tblLabor.[L1] AS v2, 'l1' AS Fa, 'Labor Parameter L1' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '10' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[L1])<>[zztbllabor].[l1]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[L2] AS v1, tblLabor.[L2] AS v2, 'L2' AS Fa, 'Labor Parameter L2' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '10' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[L2])<>[zztbllabor].[L2]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[L3] AS v1, tblLabor.[L3] AS v2, 'L3' AS Fa, 'Labor Parameter L3' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '10' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[L3])<>[zztbllabor].[L3]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[L4] AS v1, tblLabor.[L4] AS v2, 'L4' AS Fa, 'Labor Parameter L4' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '10' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[L4])<>[zztbllabor].[L4]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[setup] AS v1, tblLabor.[setup] AS v2, 'setup' AS Fa, 'Setup Time Multipler' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '6' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[setup])<>[zztbllabor].[setup]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[run] AS v1, tblLabor.[run] AS v2, 'run' AS Fa, 'run Time Multipler' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '6' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[run])<>[zztbllabor].[run]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[OT] AS v1, tblLabor.[OT] AS v2, 'OT' AS Fa, 'Overtime %' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '6' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[OT])<>[zztbllabor].[OT]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[Abst] AS v1, tblLabor.[Abst] AS v2, 'Abst' AS Fa, 'Absenteeism %' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '6' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[Abst])<>[zztbllabor].[Abst]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[GrpSiz] AS v1, tblLabor.[GrpSiz] AS v2, 'GrpSiz' AS Fa, 'No. in Group (Size)' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '6' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[GrpSiz])<>[zztbllabor].[GrpSiz]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[varbility] AS v1, tblLabor.[varbility] AS v2, 'varbility' AS Fa, 'Labor Only Variability Multipler' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '6' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[varbility])<>[zztbllabor].[varbility]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblLabor.[priorityshare] AS v1, tblLabor.[priorityshare] AS v2, 'priorityshare' AS Fa, 'Prioirtize Labor Use/Sharing' AS Fe, 'Labor' AS Te, 'tbllabor' AS Ta, tblLabor.laborid AS recid, '6' AS type FROM tblLabor INNER JOIN zztblLabor ON tblLabor.LaborID = zztblLabor.LaborID WHERE (((zztblLabor.[priorityshare])<>[zztbllabor].[priorityshare]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

    }

    public void endWhatif_Equipment() {
        ADODB.Recordset recdata = null;
        string selectedTableValues;

        selectedTableValues = " SELECT zztblEquip.EquipDept AS v1, tblEquip.EquipDept AS v2, 'EquipDept' AS Fa, 'Equip Dept' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '10' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((tblEquip.EquipDept)<>[zztblEquip].[Equipdept]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[EquipComment] AS v1, tblEquip.[EquipComment] AS v2, 'EqComment' AS Fa, 'Equipment Comment' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '10' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE ((([EquipÇomment])<>[zztblEquip].[Equipcomment]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[E1] AS v1, tblEquip.[E1] AS v2, 'E1' AS Fa, 'Equip Parameter E1' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '10' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[E1])<>[zztblEquip].[E1]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[E2] AS v1, tblEquip.[E2] AS v2, 'E2' AS Fa, 'Equip Parameter E2' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '10' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[E2])<>[zztblEquip].[E2]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[E3] AS v1, tblEquip.[E3] AS v2, 'E3' AS Fa, 'Equip Parameter E3' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '10' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[E3])<>[zztblEquip].[E3]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[E4] AS v1, tblEquip.[E4] AS v2, 'E4' AS Fa, 'Equip Parameter E4' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '10' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[E4])<>[zztblEquip].[E4]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[setup] AS v1, tblEquip.[setup] AS v2, 'setup' AS Fa, 'Setup Time Multipler' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '6' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[setup])<>[zztblEquip].[setup]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[run] AS v1, tblEquip.[run] AS v2, 'run' AS Fa, 'run Time Multipler' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '6' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[run])<>[zztblEquip].[run]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[OT] AS v1, tblEquip.[OT] AS v2, 'OT' AS Fa, 'Overtime %' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '6' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[OT])<>[zztblEquip].[OT]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[MTF] AS v1, tblEquip.[MTF] AS v2, 'MTF' AS Fa, 'Mean Time To (between) Failures' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '6' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[MTF])<>[zztblEquip].[MTF]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[MTR] AS v1, tblEquip.[MTR] AS v2, 'MTR' AS Fa, 'Mean Time To Repair' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '6' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[MTR])<>[zztblEquip].[MTR]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[GrpSiz] AS v1, tblEquip.[GrpSiz] AS v2, 'GrpSiz' AS Fa, 'No. in Group (Size)' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '6' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[GrpSiz])<>[zztblEquip].[GrpSiz]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[varbility] AS v1, tblEquip.[varbility] AS v2, 'varbility' AS Fa, 'Equip Only Variability Multipler' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '6' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[varbility])<>[zztblEquip].[varbility]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblEquip.[Labor] AS v1, tblEquip.[Labor] AS v2, 'Labor' AS Fa, 'Assigned Labor Group' AS Fe, 'Equipment' as Te, 'tblEquip' AS Ta, tblEquip.Equipid AS recid, '3' AS type FROM tblEquip INNER JOIN zztblEquip ON tblEquip.EquipID = zztblEquip.EquipID WHERE (((zztblEquip.[Labor])<>[zztblEquip].[Labor]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

    }

    public void endWhatif_Products() {

        ADODB.Recordset recdata = null;
        string selectedTableValues;

        selectedTableValues = " SELECT zztblProdFore.ProdDept AS v1, tblProdFore.ProdDept AS v2, 'ProdDept' AS Fa, 'Product Dept' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '10' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((tblProdFore.ProdForeDept)<>[zztblProdFore].[ProdForedept]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[ProdComment] AS v1, tblProdFore.[ProdComment] AS v2, 'ProdComment' AS Fa, 'Product Comment' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '10' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE ((([ProdForeÇomment])<>[zztblProdFore].[ProdForecomment]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[P1] AS v1, tblProdFore.[P1] AS v2, 'P1' AS Fa, 'Product Parameter P1' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '10' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[P1])<>[zztblProdFore].[P1]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[P2] AS v1, tblProdFore.[P2] AS v2, 'P2' AS Fa, 'Product Parameter P2' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '10' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[P2])<>[zztblProdFore].[P2]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[P3] AS v1, tblProdFore.[P3] AS v2, 'P3' AS Fa, 'Product Parameter P3' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '10' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[P3])<>[zztblProdFore].[P3]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[P4] AS v1, tblProdFore.[P4] AS v2, 'P4' AS Fa, 'Product Parameter P4' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '10' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[P4])<>[zztblProdFore].[P4]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[LotsizeFac] AS v1, tblProdFore.[LotsizeFac] AS v2, 'LotsizeFac' AS Fa, 'Lot size Multipler' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '6' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[LotsizeFac])<>[zztblProdFore].[LotsizeFac]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[DemandFac] AS v1, tblProdFore.[DemandFac] AS v2, 'DemandFac' AS Fa, 'Demand Multipler' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '6' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[DemandFac])<>[zztblProdFore].[DemandFac]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[EndDemd] AS v1, tblProdFore.[EndDemd] AS v2, 'EndDemd' AS Fa, 'End Use (shipped) Demand' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '6' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[EndDemd])<>[zztblProdFore].[EndDemd]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[Lotsiz] AS v1, tblProdFore.[Lotsiz] AS v2, 'Lotsiz' AS Fa, 'Starting Lot Size' as fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '6' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[Lotsiz])<>[zztblProdFore].[Lotsiz]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[MTR] AS v1, tblProdFore.[MTR] AS v2, 'MTR' AS Fa, 'Mean Time To Repair' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '6' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[MTR])<>[zztblProdFore].[MTR]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[GrpSiz] AS v1, tblProdFore.[GrpSiz] AS v2, 'GrpSiz' AS Fa, 'No. in Group (Size)' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '6' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[GrpSiz])<>[zztblProdFore].[GrpSiz]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[varbility] AS v1, tblProdFore.[varbility] AS v2, 'varbility' AS Fa, 'ProdFore Only Variability Multipler' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '6' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[varbility])<>[zztblProdFore].[varbility]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

        selectedTableValues = " SELECT zztblProdFore.[Labor] AS v1, tblProdFore.[Labor] AS v2, 'Labor' AS Fa, 'Assigned Labor Group' AS Fe, 'Product' as Te, 'tblProdFore' AS Ta, tblProdFore.prodid AS recid, '3' AS type FROM tblProdFore INNER JOIN zztblProdFore ON tblProdFore.prodid = zztblProdFore.prodid WHERE (((zztblProdFore.[Labor])<>[zztblProdFore].[Labor]));";
        InsertWhatifRecord(selectedTableValues, ref recdata);

    }
}