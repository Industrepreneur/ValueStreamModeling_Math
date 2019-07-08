using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IbomDelegate
/// </summary>
public class IbomDelegate: FeatureDelegate
{
	public IbomDelegate()
	{
        TABLE_NAME = "tblbomTree";
        sortedTableName = TABLE_NAME;
        defaultSortString = " ORDER BY Count";
        //tblIbom(compName, UPA, ParentName, CompID, flag, ParentID)
        //"SELECT " + TABLE_NAME + ".RecID,tblProdFore.ProdDesc, tblOper.OpNam AS opnam1, tblOper_1.OpNam AS opnam2, Per, fromnum, tonum " +
        //                 " FROM " + TABLE_NAME + " INNER JOIN ((tblProdFore INNER JOIN tblOper ON tblProdFore.ProdID = tblOper.ProdFore) INNER JOIN tblOper AS tblOper_1 ON tblProdFore.ProdID = tblOper_1.ProdFore) ON (" + TABLE_NAME + ".OpNumT = tblOper_1.OpID) AND (" + TABLE_NAME + ".OpNumF = tblOper.OpID)";
        //commandString += " WHERE tblProdfore.ProdID = " + selectedProduct;
        tableQueryString = "SELECT Count, showName, UPA, UPF, [Level], parent FROM " + TABLE_NAME;
        FIELDS = new string[] {  "Count", "showName", "UPA", "UPF", "level", "parent" };
        ADVANCED_FIELDS = new bool[] { false, false, false, true, true, false };
        HEADERS = new string[] { null, "Name", "Units for Assembly", "Units for Final Assembly", "level", "parent"};
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        nonEdits = true;
        fieldsNonEditable = new bool[FIELDS.Length];
        fieldsNonEditable[0] = true;
        fieldsNonEditable[1] = true;
        fieldsNonEditable[2] = false;
        fieldsNonEditable[3] = true;
        fieldsNonEditable[4] = true;
        fieldsNonEditable[5] = true;



        InitializeCombos();
        InitializeCheckboxes();

        COMBOS[1] = true;

        hasAdvanced = true; // show/hide 
        wantSort = false;
    }

    public override void SetupTooltips() {
        base.SetupTooltips();

        HEADER_TOOLTIPS[1] = "Name of the component, + in front means the component has subcomponents.";
        HEADER_TOOLTIPS[2] = "Number of units needed for the next level of assembly. The number of components needed for a single assembly the component that the piece directly goes into e.g. 2As are in 1B and 2Bs in 1C displays 2 for A and 2 for B.";
        HEADER_TOOLTIPS[3] = "Number of units needed in the final product selected in above drop-down list. The number of components needed to build an entire parent product e.g. 2As in 1B and 2Bs in 1C displays 4 for A and 2 for B.";

    }
}