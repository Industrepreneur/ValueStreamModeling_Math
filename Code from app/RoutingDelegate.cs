using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RoutingDelegate
/// </summary>
public class RoutingDelegate : FeatureDelegate
{
    public RoutingDelegate()
    {
        TABLE_NAME = "tblOperFrTo";
        sortedTableName = "tbloperfrto_1";

        FIELDS = new string[] { "RecID", "ProdDesc", "opnam1", "opnam2", "Per", "fromnum", "tonum" }; //
        ADVANCED_FIELDS = new bool[] { false, false, false, false, false, false, false };
        HEADERS = new string[] { null, "Product Name", "Operation From", "Operation To", "% Routed", "From Operation Number", "To Operation Number" };
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        nonEdits = true;
        fieldsNonEditable = new bool[FIELDS.Length];
        fieldsNonEditable[1] = false;
        fieldsNonEditable[2] = false;
        fieldsNonEditable[3] = false;
        fieldsNonEditable[4] = false;
        fieldsNonEditable[5] = true;
        fieldsNonEditable[6] = true;
        InitializeCombos();
        InitializeCheckboxes();
        COMBOS[1] = true;
        COMBOS[2] = true;
        COMBOS[3] = true;

        hasAdvanced = false; // no advanced mode
    }

    public override void SetupTooltips()
    {
        base.SetupTooltips();
        HEADER_TOOLTIPS[1] = "Product that is being routed between two operations.";
        HEADER_TOOLTIPS[2] = "Operation that the given product is coming from.";
        HEADER_TOOLTIPS[3] = "Operation that the product is arriving at.";
        HEADER_TOOLTIPS[4] = "Percent of the product that is being routed between the two operations.";
        HEADER_TOOLTIPS[5] = "Number of the operation that the product is coming from.";
        HEADER_TOOLTIPS[6] = "Number of the operation that the product is arriving at.";


    }

    public string GetCommandString(int commandType, string commandString, string selectedProduct)
    {
        if (commandType == Command.SELECT)
        {
            //int orderIndex = commandString.ToLower().IndexOf("order by");
            //string order = ";";
            //if (orderIndex > -1)
            //{
            //    order = commandString.Substring(orderIndex);
            //    commandString = commandString.Substring(0, commandString.ToLower().IndexOf("order by"));
            //}
            commandString += " WHERE tblProdfore.ProdID = " + selectedProduct;
        }
        return commandString;
    }
}