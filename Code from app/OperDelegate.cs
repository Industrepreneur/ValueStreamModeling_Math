using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OperDelegate
/// </summary>
public class OperDelegate: FeatureDelegate
{
	public OperDelegate()
	{
        TABLE_NAME = "tbloper";
        sortedTableName = "tbloper_all";
        defaultSortString = " ORDER BY ProdFore";

        FIELDS = new string[] { "OpID", "ProdDesc", "ProdFore", "OpNam", "OpNum", "EquipDesc", "PercentAssign", "EqSetupTime", "EqSetupTbatch", "eqSetupPiece", "eqRunLot", "eqRunTbatch", "EqRunTime", "LabSetupTime", "labSetupTbatch", "labSetupPiece", "labRunLot", "labRunTbatch", "LabRunTime" };
        ADVANCED_FIELDS = new bool[] { false, false, false, false, false, false, false, false, true, true, true, true, false, false, true, true, true, true, false };
        //HEADERS = new string[] { null, "Product Name", null, "Operation Name", "Operation Number", "Equipment Assigned", "% Assigned", "Equip. Setup Time (Lot)", "Equip. Setup Time (Tbatch)", "Equip. Setup Time (Piece)", "Equip. Run Time (Lot)", "Equip. Run Time (Tbatch)", "Equip. Run Time (Piece)", "Labor Setup Time (Lot)", "Labor Setup Time (Tbatch)", "Labor Setup Time (Piece)", "Labor Run Time (Lot)", "Labor Run Time (Tbatch)", "Labor Run Time (Piece)" };
        HEADERS = new string[] { null, "Product Name", null, "Operation Name", "Operation Number", "Equipment Assigned", "% Assigned", "Lot", "Tbatch", "Piece", "Lot", "Tbatch", "Piece", "Lot", "Tbatch", "Piece", "Lot", "Tbatch", "Piece" };
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        InitializeCombos();
        InitializeCheckboxes();
        COMBOS[3] = true;
        COMBOS[5] = true;
        COMBOS[1] = true;

        wantTwoHeaders = true;
	}

    public override void SetupTooltips() {
        base.SetupTooltips();
        HEADER_TOOLTIPS[1] = "Product Name associated with the operation.";
        HEADER_TOOLTIPS[3] = "Operation Name (must be unique for different operations on all products).";
        HEADER_TOOLTIPS[4] = "Operation Number (should be unique for a given product). Operation Numbers should correspond to the same Operation Name across all products.";
        HEADER_TOOLTIPS[5] = "Equipment assigned to the operation.";
        HEADER_TOOLTIPS[6] = "Percent of the products assigned to the operation.";

        HEADER_TOOLTIPS[7] = "Time to set up the equipment for the entire lot (in Operation Time Units).";
        HEADER_TOOLTIPS[8] = "Time required due to transferring transfer batches (in Operation Time Units).";
        HEADER_TOOLTIPS[9] = "Additional set-up time required by the equipment for each piece in the lot (in Operation Time Units).";

        HEADER_TOOLTIPS[10] = "Additional time required by the equipment to run an entire lot (in Operation Time Units).";
        HEADER_TOOLTIPS[11] = "Time required due to transferring transfer batches (in Operation Time Units).";
        HEADER_TOOLTIPS[12] = "Time the equipment needs to run a single piece (in Operation Time Units).";

        HEADER_TOOLTIPS[13] = "Time required for the labor to set up for the entire lot (in Operation Time Units).";
        HEADER_TOOLTIPS[14] = "Time required due to transferring transfer batches (in Operation Time Units).";
        HEADER_TOOLTIPS[15] = "Additional set-up time required by the labor for each piece in the lot (in Operation Time Units).";

        HEADER_TOOLTIPS[16] = "Additional time required by the labor to run an entire lot (in Operation Time Units).";
        HEADER_TOOLTIPS[17] = "Time required due to transferring transfer batches (in Operation Time Units).";
        HEADER_TOOLTIPS[18] = "Amount of time required for the labor to run each piece (in Operation Time Units).";

    }

    public override void SetupSecondHeaders() {
        base.SetupSecondHeaders();
        info.AddMergedColumns(new int[] { 7, 8, 9 }, "Equipment Setup Time");
        info.AddMergedColumns(new int[] { 10, 11, 12 }, "Equipment Run Time");
        info.AddMergedColumns(new int[] { 13, 14, 15 }, "Labor Setup Time");
        info.AddMergedColumns(new int[] { 16, 17, 18 }, "Labor Run Time");
    }

    public static bool SameOperNameNumExists(string opName, int prodId, int opNum, int opId, string connectionString, LogFiles logFiles) {
        bool exists = false;
        ADODB.Connection conn = new ADODB.Connection();
        ADODB.Recordset rec = new ADODB.Recordset();

        opName = opName.ToUpper();

        bool adoOpen = DbUse.OpenAdo(conn, connectionString);
        string queryString = "SELECT OpID, OpNam, OpNum, Prodfore FROM tblOper WHERE Prodfore = " + prodId + " AND OpNam = '" + opName + "' AND OpNum = " + opNum + " AND OpID <> " + opId;
        bool recOpen = DbUse.OpenAdoRec(conn, rec, queryString);

        try {
            if (adoOpen && recOpen) {
               if (opId != -1) {
                    // we actually know  the opid of the record
                    exists = !rec.EOF;
               } else {
                    // don't know opid - one record it is the one we previously entered, more records - more opers with same name and num
                    if (!rec.EOF) {
                        rec.MoveNext();
                        exists = !rec.EOF;
                    }
               }

            }
        } catch (Exception ex) {
            logFiles.ErrorLog(ex);
        } finally {
            DbUse.CloseAdoRec(rec);
            DbUse.CloseAdo(conn);
        }
        return exists;

    }

    protected bool SameOperNameExists(string opName, int prodId, int opNum, string connectionString, LogFiles logFiles) {
        return SameOperNameNumExists(opName, prodId, opNum, -1, connectionString, logFiles);
    }

    public static bool SameOperNameDiffNumExists(string opName, int opNum, int prodId, string connectionString, LogFiles logFiles) {
        return SameOperNameDiffNumExists(opName, opNum, prodId, -1, connectionString, logFiles);
    }

    public static bool SameOperNameDiffNumExists(string opName, int opNum, int prodId, int opId, string connectionString, LogFiles logFiles) {
        bool exists = false;
        ADODB.Connection conn = new ADODB.Connection();
        ADODB.Recordset rec = new ADODB.Recordset();

        opName = opName.ToUpper();

        bool adoOpen = DbUse.OpenAdo(conn, connectionString);
        string queryString = "SELECT OpID, OpNam, OpNum, Prodfore FROM tblOper WHERE Prodfore = " + prodId + " AND OpNam = '" + opName + "' AND OpNum <> " + opNum  + " AND OpID <> " + opId;
        bool recOpen = DbUse.OpenAdoRec(conn, rec, queryString);

        try {
            if (adoOpen && recOpen) {
                exists = !rec.EOF;

            }
        } catch (Exception ex) {
            logFiles.ErrorLog(ex);
        } finally {
            DbUse.CloseAdoRec(rec);
            DbUse.CloseAdo(conn);
        }
        return exists;

    }


    public static bool SameOperNumDiffNameExists(string opName, int opNum, int prodId, int opId, string connectionString, LogFiles logFiles) {
        bool exists = false;
        ADODB.Connection conn = new ADODB.Connection();
        ADODB.Recordset rec = new ADODB.Recordset();

        opName = opName.ToUpper();

        bool adoOpen = DbUse.OpenAdo(conn, connectionString);
        string queryString = "SELECT OpID, OpNam, OpNum, Prodfore FROM tblOper WHERE Prodfore = " + prodId + " AND OpNam <> '" + opName + "' AND OpNum = " + opNum + " AND OpID <> " + opId;
        bool recOpen = DbUse.OpenAdoRec(conn, rec, queryString);

        try {
            if (adoOpen && recOpen) {
                exists = !rec.EOF;

            }
        } catch (Exception ex) {
            logFiles.ErrorLog(ex);
        } finally {
            DbUse.CloseAdoRec(rec);
            DbUse.CloseAdo(conn);
        }
        return exists;

    }

    public static bool SameOperNumDiffNameExists(string opName, int opNum, int prodId, string connectionString, LogFiles logFiles) {
        return SameOperNumDiffNameExists(opName, opNum, prodId, connectionString, logFiles);

    }

    public static bool DuplicateOperNamesOrNumbers(string connectionString) {
        return DuplicateOperNamesOrNumbers(connectionString, -1);
    }

    public static bool DuplicateOperNamesOrNumbers(string connectionString, int prodId) {
        bool duplicate = false;
        ADODB.Connection conn = new ADODB.Connection();
        ADODB.Recordset rec = new ADODB.Recordset();
        string sqlQuery = "SELECT tblOper_1.ProdFore, tblOper.OpNum, tblOper.OpNam FROM tblOper INNER JOIN tblOper AS tblOper_1 ON (tblOper.OpNam = tblOper_1.OpNam) AND (tblOper.ProdFore = tblOper_1.ProdFore) WHERE ((tblOper.OpNum)<[tbloper_1].[opnum])";
        if (prodId > 0) {
            sqlQuery += " AND tblOper_1.ProdFore = " + prodId;
        }
        sqlQuery += ";";
        DbUse.OpenAdo(conn, connectionString);
        DbUse.OpenAdoRec(conn, rec, sqlQuery);
        try {
            duplicate = !rec.EOF;
        } catch (Exception exp) {
            LogFiles logFiles = new LogFiles();
            logFiles.ErrorLog(exp);
        } finally {
            DbUse.CloseAdoRec(rec);
        }

        sqlQuery = "SELECT tblOper_1.ProdFore, tblOper.OpNum, tblOper.OpNam FROM tblOper INNER JOIN tblOper AS tblOper_1 ON (tblOper.OpNum = tblOper_1.OpNum) AND (tblOper.ProdFore = tblOper_1.ProdFore) WHERE StrComp(tblOper.OpNam, tbloper_1.OpNam) = 1";
        if (prodId > 0) {
            sqlQuery += " AND tblOper_1.ProdFore = " + prodId;
        }
        sqlQuery += ";";
        DbUse.OpenAdoRec(conn, rec, sqlQuery);
        try {
            duplicate = duplicate || !rec.EOF;
        } catch (Exception exp) {
            LogFiles logFiles = new LogFiles();
            logFiles.ErrorLog(exp);
        } finally {
            DbUse.CloseAdoRec(rec);
        }
        DbUse.CloseAdo(conn);
        return duplicate;
    }

    
}