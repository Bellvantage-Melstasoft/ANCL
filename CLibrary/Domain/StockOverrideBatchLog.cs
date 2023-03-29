using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class StockOverrideBatchLog {


        [DBField("STOCK_OVERRIDE_BATCH_ID")]
        public int StockOverrideBatchId { get; set; }

        [DBField("OVERRIDE_LOG_ID")]
        public int LogId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("BATCH_ID")]
        public int BatchId { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("EXISTED_QTY")]
        public decimal ExistedQty { get; set; }

        [DBField("EXISTED_STOCK_VALUE")]
        public decimal ExistedStockValue { get; set; }

        [DBField("OVERRIDING_QTY")]
        public decimal OverridingQty { get; set; }

        [DBField("OVERRIDING_STOCK_VALUE")]
        public decimal OverridingStockValue { get; set; }

        [DBField("UPDATED_BY")]
        public int UpdatedBy { get; set; }

        [DBField("UPDATED_ON")]
        public DateTime UpdatedOn { get; set; }

        [DBField("BATCH_CODE")]
        public int BatchCode { get; set; }

        [DBField("SHORT_CODE")]
        public string ShortCode { get; set; }

        [DBField("BATCH_EXPIRY_DATE")]
        public DateTime BatchExpiryDate { get; set; }
    }
}
