using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProdOperRoutingDelegate
/// </summary>
public class ProdOperRoutingDelegate: FeatureDelegate
{
	public ProdOperRoutingDelegate()
	{
		FIELDS = new string[] { "OpID", "OpNum", "OpNam", "EquipDesc", "PercentAssign", "EqSetupTime", "EqSetupTbatch", "eqSetupPiece", "eqRunLot", "eqRunTbatch", "EqRunTime", "LabSetupTime", "labSetupTbatch", "labSetupPiece", "labRunLot", "labRunTbatch", "LabRunTime" };
        ADVANCED_FIELDS = new bool[] { false, false, false, false, false, false, true, true, true, true, false, false, true, true, true, true, false };
        //HEADERS = new string[] { null, "Operation Name", "Operation Number", "Equipment Assigned", "% Assigned", "Equip. Setup Time (Lot)", "Equip. Setup Time (Tbatch)", "Equip. Setup Time (Piece)", "Equip. Run Time (Lot)", "Equip. Run Time (Tbatch)", "Equip. Run Time (Piece)", "Labor Setup Time (Lot)", "Labor Setup Time (Tbatch)", "Labor Setup Time (Piece)", "Labor Run Time (Lot)", "Labor Run Time (Tbatch)", "Labor Run Time (Piece)" };
        HEADERS = new string[] { null, "Number", "Description", "Equipment", "% Assigned", "Lot", "Tbatch", "Piece", "Lot", "Tbatch", "Piece", "Lot", "Tbatch", "Piece", "Lot", "Tbatch", "Piece" };
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        InitializeCombos();
        InitializeCheckboxes();
        COMBOS[3] = true;

        defaultSortString = " ORDER BY OpID";
        sortedTableName = "tbloper";

        wantSort2 = true;
        sortedTableName2 = "tbloperfrto";
        wantTwoHeaders = true;
        
	}

    public override void SetupTooltips() {
        base.SetupTooltips();

        HEADER_TOOLTIPS[1] = "Operation Name (must be unique for different operations on all products).";
        HEADER_TOOLTIPS[2] = "Operation Number (should be unique for a given product). Operation Numbers should correspond to the same Operation Name across all products.";
        HEADER_TOOLTIPS[3] = "Equipment assigned to the operation.";
        HEADER_TOOLTIPS[4] = "Percent of the products assigned to the operation.";

        HEADER_TOOLTIPS[5] = "Time to set up the equipment for the entire lot (in Operation Time Units).";
        HEADER_TOOLTIPS[6] = "Time required due to transferring transfer batches (in Operation Time Units).";
        HEADER_TOOLTIPS[7] = "Additional set-up time required by the equipment for each piece in the lot (in Operation Time Units).";

        HEADER_TOOLTIPS[8] = "Additional time required by the equipment to run an entire lot (in Operation Time Units).";
        HEADER_TOOLTIPS[9] = "Time required due to transferring transfer batches (in Operation Time Units).";
        HEADER_TOOLTIPS[10] = "Time the equipment needs to run a single piece (in Operation Time Units).";

        HEADER_TOOLTIPS[11] = "Time required for the labor to set up for the entire lot (in Operation Time Units).";
        HEADER_TOOLTIPS[12] = "Time required due to transferring transfer batches (in Operation Time Units).";
        HEADER_TOOLTIPS[13] = "Additional set-up time required by the labor for each piece in the lot (in Operation Time Units).";

        HEADER_TOOLTIPS[14] = "Additional time required by the labor to run an entire lot (in Operation Time Units).";
        HEADER_TOOLTIPS[15] = "Time required due to transferring transfer batches (in Operation Time Units).";
        HEADER_TOOLTIPS[16] = "Amount of time required for the labor to run each piece (in Operation Time Units).";

    }

    public override void SetupSecondHeaders() {
        base.SetupSecondHeaders();
        info.AddMergedColumns(new int[] { 5, 6, 7 }, "Equipment Setup Time");
        info.AddMergedColumns(new int[] { 8, 9, 10 }, "Equipment Run Time");
        info.AddMergedColumns(new int[] { 11, 12, 13 }, "Labor Setup Time");
        info.AddMergedColumns(new int[] { 14, 15, 16 }, "Labor Run Time");
    }

    public string GetCommandString(int commandType, string commandString, string selectedProduct) {
        if (commandType == Command.SELECT) {
            int orderIndex = commandString.ToLower().IndexOf("order by");
            string order = ";";
            if (orderIndex > -1) {
                order = commandString.Substring(orderIndex);
                commandString = commandString.Substring(0, commandString.ToLower().IndexOf("order by"));
            }
            commandString += " WHERE (OpNam <> 'DOCK' AND OpNam <> 'STOCK' AND Opnam <> 'SCRAP') AND tblProdfore.ProdID = " + selectedProduct + " " + order;
        } else if (commandType == Command.INSERT) {
            commandString = commandString.Substring(0, commandString.IndexOf("(") + 1) + "ProdFore, ProdDesc, " + commandString.Substring(commandString.IndexOf("(") + 1);
            commandString = commandString.Substring(0, commandString.IndexOf("?") + 1) + ",?,?" + commandString.Substring(commandString.IndexOf("?") + 1);
        }
        return commandString;
    }



}