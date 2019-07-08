using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RoutingDelegate
/// </summary>
public class RoutingTableDelegate : FeatureDelegate
{
    public RoutingTableDelegate()
    {
        TABLE_NAME = "tblOperFrTo";
        sortedTableName = "tbloperfrto_1";

        FIELDS = new string[] { "RecID", "opnam1", "opnam2", "Per", "fromnum", "tonum" };
        ADVANCED_FIELDS = new bool[] { false, false, false, false, true, true };
        //ADVANCED_FIELDS = new bool[] { false, false, false, false, false, false };
        HEADERS = new string[] { null, "Operation From", "Operation To", "% Routed", "From Operation Number", "To Operation Number" };
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        nonEdits = true;
        fieldsNonEditable = new bool[FIELDS.Length];
        fieldsNonEditable[1] = false;
        fieldsNonEditable[2] = false;
        fieldsNonEditable[3] = false;

        fieldsNonEditable[4] = true;
        fieldsNonEditable[5] = true;

        InitializeCombos();
        InitializeCheckboxes();

        COMBOS[1] = true;
        COMBOS[2] = true;

        

        hasAdvanced = true; // show/hide operation numbers
    }

    public override void SetupTooltips()
    {
        base.SetupTooltips();

        HEADER_TOOLTIPS[1] = "Operation that the given product is coming from.";
        HEADER_TOOLTIPS[2] = "Operation that the product is arriving at.";
        HEADER_TOOLTIPS[3] = "The likelihood a lot will flow to the selected Operation on average; The 'Scrap' selector represents the % of a lot that will be removed from production";
        HEADER_TOOLTIPS[4] = "Number of the operation that the product is coming from.";
        HEADER_TOOLTIPS[5] = "Number of the operation that the product is arriving at.";


    }

    public string GetCommandString(int commandType, string commandString, string selectedProduct)
    {
        if (commandType == Command.SELECT)
        {
            commandString = "SELECT " + TABLE_NAME + ".RecID,tblProdFore.ProdDesc, tblOper.OpNam AS opnam1, tblOper_1.OpNam AS opnam2, Per, fromnum, tonum " +
                         " FROM " + TABLE_NAME + " INNER JOIN ((tblProdFore INNER JOIN tblOper ON tblProdFore.ProdID = tblOper.ProdFore) INNER JOIN tblOper AS tblOper_1 ON tblProdFore.ProdID = tblOper_1.ProdFore) ON (" + TABLE_NAME + ".OpNumT = tblOper_1.OpID) AND (" + TABLE_NAME + ".OpNumF = tblOper.OpID)";
            commandString += " WHERE tblProdfore.ProdID = " + selectedProduct;
        }
        return commandString;
    }
}