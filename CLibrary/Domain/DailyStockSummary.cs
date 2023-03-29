using CLibrary.Common;
using System;

namespace CLibrary.Domain {
    public class DailyStockSummary {

        [DBField("DATE")]
        public DateTime Date { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("AVAILABLE_QTY")]
        public decimal AvailableQty { get; set; }

        [DBField("HOLDED_QTY")]
        public decimal HoldedQty { get; set; }

        [DBField("STOCK_VALUE")]
        public decimal StockValue { get; set; }

        [DBField("LOCATION")]
        public string Location { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("REFERENCE_NO")]
        public string ReferenceNo { get; set; }

        [DBField("SYS_AVAILABLE_QTY")]
        public decimal SysAvailableQty { get; set; }

        [DBField("SYS_STOCK_VALUE")]
        public decimal SysStockValue { get; set; }
    }
}
