using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EquipmentDelegate
/// </summary>
public class EquipmentDelegate: FeatureDelegate
{
	public EquipmentDelegate()
	{
        FIELDS = new string[] { "EquipID", "EquipDesc", "EquipDept", "EquipTypeName", "GrpSiz", "OT", "MTF", "MTR", "LaborDesc", "Setup", "Run", "Varbility", "EqComment" };
        ADVANCED_FIELDS = new bool[] { false, false, false, false, false, false, false, false, false, true, true, true, false };
        HEADERS = new string[] { null, "Name", "Dept", "Type", "QTY", "Overtime %", "MTTF", "MTTR", "Labor", "Setup Time Multiplier", "Run Time Multiplier", "Variability Multiplier", "Comment" };
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        InitializeCombos();
        InitializeCheckboxes();
        COMBOS[8] = true;
        COMBOS[3] = true;

        TABLE_NAME = "tblequip";
        sortedTableName = TABLE_NAME;
        defaultSortString = " ORDER BY EquipID";
	}

    public override string GetCommandString(int commandType, string commandString) {
        if (commandType == Command.SELECT) {
            int index = commandString.ToLower().IndexOf("order by");
            string orderCommand = commandString.Substring(index);
            string selectCommand = commandString.Substring(0, index);
            commandString = selectCommand + " WHERE EquipDesc <> 'none' " + orderCommand;
        }
        return commandString;
    }

    public override void SetupTooltips() {
        base.SetupTooltips();

        HEADER_TOOLTIPS[1] = "Name of the equipment group (each row must be unique).";
        HEADER_TOOLTIPS[2] = "Department of the equipment group (for user only).";
        HEADER_TOOLTIPS[3] = "Standard has a finite number of machines and Delay has infinite number of machines and work is immediately processed";
        HEADER_TOOLTIPS[4] = "The number of equipment in the group (at least 1).";
        HEADER_TOOLTIPS[5] = "What extra percent of the scheduled time this equipment group can be worked.";
        HEADER_TOOLTIPS[6] = "The average amount of time that the equipment can run before breaking, measured in the Operations Time Units selected on the General Input Page.";
        HEADER_TOOLTIPS[7] = "The average amount of time that the equipment takes to be repaired, measured in the Operations Time Units selected on the General Input Page.";
        HEADER_TOOLTIPS[8] = "The labor group that operates the equipment.";
        HEADER_TOOLTIPS[9] = "A multiplier for how long of the input set-up time the machine group takes.";
        HEADER_TOOLTIPS[10] = "A multiplier for how long of the input run time the machine group takes. This is useful to quickly effect ALL run times that the machine group does.";
        HEADER_TOOLTIPS[11] = "A multiplier for how much of the Variability in Product Times affect this machine group.";

    }
}