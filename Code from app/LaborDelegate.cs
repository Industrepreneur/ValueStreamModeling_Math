using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LaborDelegate
/// </summary>
public class LaborDelegate: FeatureDelegate
{
	public LaborDelegate()
	{
        FIELDS = new string[] { "LaborID", "LaborDesc", "LaborDept", "GrpSiz", "OT", "Abst", "PriorityShare", "Setup", "Run", "Varbility", "LabComment" };
        ADVANCED_FIELDS = new bool[] { false, false, false, false, false, false, true, true, true, true, false };
        HEADERS = new string[] { null, "Group Name", "Dept", "QTY", "Overtime %", "Inefficiency", "Prioritize Use", "Setup Time Multiplier", "Run Time Multiplier", "Variability Multiplier", "Comment" };
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        InitializeCombos();
        InitializeCheckboxes();
        CHECKBOXES[6] = true;


        TABLE_NAME = "tbllabor";
        sortedTableName = TABLE_NAME;
        defaultSortString = "ORDER BY tbllabor.LaborDesc";
        SORT_COMMAND = "SELECT zstblsort.* FROM zstblsort WHERE (((zstblsort.tableName)= '" + sortedTableName + "') AND (afieldname <> 'L1') AND (afieldname <> 'L2') AND (afieldname <> 'L3') AND (afieldname <> 'L4'));";

	}

    public override void SetupTooltips() {
        base.SetupTooltips();

        HEADER_TOOLTIPS[1] = "Name of the Labor Group, must be unique";
        HEADER_TOOLTIPS[2] = "Department of the Labor Group";
        HEADER_TOOLTIPS[3] = "The number of employess in the group, must be at least 1";
        HEADER_TOOLTIPS[4] = "What extra percent of the scheduled time this Labor Group can be worked.";
        HEADER_TOOLTIPS[5] = "The percentage of unplanned time that employees cannot perform tasks";
        HEADER_TOOLTIPS[6] = "Select to use labor for higher utilized equipment and let lower utilized equipment wait for labor";
        HEADER_TOOLTIPS[7] = "A multiplier for how long of the inputted set-up time the labor group takes. This is useful to quickly effect ALL set-up times if there is a set-up reduction plan implemented across the entire plant.";
        HEADER_TOOLTIPS[8] = "A multiplier for how long of the inputted run time the labor group takes. This is useful to quickly effect ALL run times that the labor group does.";
        HEADER_TOOLTIPS[9] = "A multiplier for how much of the Variability in Product Times affect this labor group.";
        //keep assign labor to equipment with higher utilization when the machines are idle and allow lower utilized machines to wait for labor to be availab
    }

    public override string GetCommandString(int commandType, string commandString) {
        if (commandType == Command.SELECT) {
            int index = commandString.ToLower().IndexOf("order by");
            string orderCommand = commandString.Substring(index);
            string selectCommand = commandString.Substring(0, index);
            commandString = selectCommand + " WHERE LaborDesc <> 'none' " + orderCommand;
        }
        return commandString;
    }
}