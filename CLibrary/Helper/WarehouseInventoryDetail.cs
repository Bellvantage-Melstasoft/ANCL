using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Helper
{
    public class WarehouseInventoryDetail
    {
        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("REFERENCE_NO")]
        public string ItemCode { get; set; }

        [DBField("LOCATION")]
        public string WarehouseName { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int StockMaintainingUom { get; set; }

        [DBField("AVAILABLE_QTY")]
        public decimal AvailableQty { get; set; }

        [DBField("HOLDED_QTY")]
        public decimal HoldedQty { get; set; }

        [DBField("STOCK_VALUE")]
        public decimal StockValue { get; set; }

        public decimal RequestedQty { get; set; }
        public decimal IssuedQty { get; set; }


        public List<WarehouseInventoryBatches> Batches { get; set; }
    }
}
