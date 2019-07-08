using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for FeatureDelegate
/// </summary>
public class FeatureDelegate {

    public string TABLE_NAME;
    public string sortedTableName;
    public string sortedTableName2;
    public string defaultSortString;
    public string SORT_COMMAND;

    public string tableQueryString;

    public bool nonEdits = false;
    public bool[] fieldsNonEditable;

    public bool hasAdvanced = true;

    public string[] FIELDS;
    public bool[] ADVANCED_FIELDS;
    public string[] HEADERS;
    public string[] HEADER_TOOLTIPS;

    public bool[] COMBOS;
    public bool[] CHECKBOXES;

    public string TABLE_NAME_ROUTING;

    public bool wantSort = true;
    public bool wantSort2 = false;

    public bool wantTwoHeaders = false;

    public MergedColumnsInfo info;

    public static class Command {
        public const int SELECT = 0;
        public const int INSERT = 1;
        public const int UPDATE = 2;
        public const int DELETE = 3;
    }

	public FeatureDelegate()
	{
		
	}

    public virtual void SetupTooltips() {
        if (HEADER_TOOLTIPS != null) {
            for (int i = 0; i < HEADER_TOOLTIPS.Length; i++) {
                HEADER_TOOLTIPS[i] = "";
            }
        }
    }

    public virtual void SetupSecondHeaders() {
        info = new MergedColumnsInfo();
    }

    public virtual void SetTableQueryString() {

    }

    public virtual string GetCommandString(int commandType, string commandString) {
        return "";
    }

    protected void InitializeCombos() {
        COMBOS = new bool[FIELDS.Length];
        for (int i = 0; i < FIELDS.Length; i++) {
            COMBOS[i] = false;
        }
    }

    protected void InitializeCheckboxes() {
        CHECKBOXES = new bool[FIELDS.Length];
        for (int i = 0; i < FIELDS.Length; i++) {
            CHECKBOXES[i] = false;
        }
    }

    public virtual void SyncTables() {
        
    }
}