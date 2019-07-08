using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductTableDelegate
/// </summary>
public class ProductTableDelegate: FeatureDelegate
{
	public ProductTableDelegate()
	{
        TABLE_NAME = "tblprodfore";
        sortedTableName = TABLE_NAME;
        defaultSortString = " ORDER BY ProdID";
        FIELDS = new string[] { "ProdID", "ProdDesc", "ProdDept", "EndDemd", "Lotsiz", "TransferBatch", "TBatchGather", "makestock", "Value", "Variability", "LotSizeFac", "DemandFac", "ProdComment" };
        ADVANCED_FIELDS = new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, false };
        HEADERS = new string[] { null, "Name", "Product Family", "Demand", "Lot Size", "Batch Size", "Gather Batches?", "Make To Stock", "Priority", "Variability Multiplier", "Lot Size Multiplier", "Demand Multiplier", "Comment" };
        //FIELDS = new string[] { "ProdID", "ProdDesc", "ProdDept", "EndDemd", "Lotsiz", "Variability", "Value", "TransferBatch", "TBatchGather", "makestock", "LotSizeFac", "DemandFac", "ProdComment" };
        //ADVANCED_FIELDS = new bool[] { false, false, false, false, false, true, true, true, true, true, true, true, false };
        //HEADERS = new string[] { null, "Name", "Product Family", "Demand", "Lot Size", "Variability Multiplier", "Priority", "Batch Size", "Gather Batches?", "Make To Stock", "Lot Size Multiplier", "Demand Multiplier", "Comment" };
        HEADER_TOOLTIPS = new string[HEADERS.Length];
        SetupTooltips();

        InitializeCombos();
        InitializeCheckboxes();
        CHECKBOXES[6] = true;
        CHECKBOXES[7] = true;
	}

    public override void SetupTooltips() {
        base.SetupTooltips();


        HEADER_TOOLTIPS[1] = "Name of the product, must be unique";
        HEADER_TOOLTIPS[2] = "Name to group products by";
        HEADER_TOOLTIPS[3] = "The total customer demand for the product over the forecast period";
        HEADER_TOOLTIPS[4] = "Average lot size used during production of the product";
  HEADER_TOOLTIPS[5] = "Number of pieces completed and pushed on to next operation before whole lot is completed. A value of -1 means that pieces wait until the entire batch is finished before moving.";
        HEADER_TOOLTIPS[6] = "Gather all of the pieces transfer batches back into a full lot before moving them to a new product or move the transfer batches to the other product as batches are finished.";
        HEADER_TOOLTIPS[7] = "Sets lead time for these parts to 0 in IBOM output page.";
 HEADER_TOOLTIPS[8] = "Weighting the relative importance of different products with larger numbers meaning the product is more valuable.";
        HEADER_TOOLTIPS[9] = "A multiplier for how much of the Variability in Product Times affect this machine group.";
        HEADER_TOOLTIPS[10] = "A multiplier to affect the Batch Size for the product.";
        HEADER_TOOLTIPS[11] = "A multiplier to affect the Demand for the product.";

        //HEADER_TOOLTIPS[1] = "Name of the product, must be unique";
        //HEADER_TOOLTIPS[2] = "Name to group products by";
        //HEADER_TOOLTIPS[3] = "The total customer demand for the product over the forecast period";
        //HEADER_TOOLTIPS[4] = "Average lot size used during production of the product";
        //HEADER_TOOLTIPS[5] = "A multiplier for how much of the Variability in Product Times affect this machine group.";
        //HEADER_TOOLTIPS[6] = "Weighting the relative importance of different products with larger numbers meaning the product is more valuable.";
        //HEADER_TOOLTIPS[7] = "Number of pieces completed and pushed on to next operation before whole lot is completed. A value of -1 means that pieces wait until the entire batch is finished before moving.";
        //HEADER_TOOLTIPS[8] = "Gather all of the pieces transfer batches back into a full lot before moving them to a new product or move the transfer batches to the other product as batches are finished.";
        //HEADER_TOOLTIPS[9] = "Sets lead time for these parts to 0 in IBOM output page.";
        //HEADER_TOOLTIPS[10] = "A multiplier to affect the Batch Size for the product.";
        //HEADER_TOOLTIPS[11] = "A multiplier to affect the Demand for the product.";

    }

    /*









*/
}