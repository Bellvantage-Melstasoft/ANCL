using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class MrndIssueNoteBatches
    {
        [DBField("MRND_IN_ID")]
        public int MrndInId { get; set; }

        [DBField("BATCH_ID")]
        public int BatchId { get; set; }

        [DBField("ISSUED_QTY")]
        public decimal IssuedQty { get; set; }

        [DBField("ISSUED_STOCK_VALUE")]
        public decimal IssuedStockValue { get; set; }

        [DBField("BATCH_CODE")]
        public int BatchCode { get; set; }

        [DBField("EXPIRY_DATE")]
        public DateTime BatchExpiryDate { get; set; }

        [DBField("MRND_ID")]
        public int MrndID { get; set; }

        [DBField("MRN_ID")]
        public int MrnID { get; set; }

        [DBField("ITEM_ID")]
        public int ItemID { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseID { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }

        [DBField("RETURN_QTY")]
        public decimal ReturnQty { get; set; }

        
    }
}
