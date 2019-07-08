using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IbomDelegate
/// </summary>
public class BomDelegate : FeatureDelegate
{
    public BomDelegate()
    {
        TABLE_NAME = "tblbomtree";
        sortedTableName = TABLE_NAME;
        defaultSortString = " ORDER BY Count";

        tableQueryString = "SELECT Count, showName, UPA, UPF FROM " + TABLE_NAME;
        FIELDS = new string[] { "Count", "showName", "UPA", "UPF" };
        ADVANCED_FIELDS = new bool[] { false, false, false, false };
        HEADERS = new string[] { null, "Component Name", "Units for Assembly", "Units for Final Assembly" };
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        InitializeCombos();
        InitializeCheckboxes();

        wantSort = false;
        hasAdvanced = false;
    }

    public override void SetupTooltips()
    {
        base.SetupTooltips();

        HEADER_TOOLTIPS[1] = "Name of the component, + in front means the component has subcomponents.";
        HEADER_TOOLTIPS[2] = "Number of units needed for the next level of assembly. The number of components needed for a single assembly the component that the piece directly goes into e.g. 2As are in 1B and 2Bs in 1C displays 2 for A and 2 for B.";
        HEADER_TOOLTIPS[3] = "Number of units needed in the final product selected in above drop-down list. The number of components needed to build an entire parent product e.g. 2As in 1B and 2Bs in 1C displays 4 for A and 2 for B.";

    }
}