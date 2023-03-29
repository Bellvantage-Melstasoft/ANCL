using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Helper
{
    public class IssuedInventoryDetails
    {
        public IssuedInventoryDetails()
        {
            Batches = new List<IssuedInventoryBatches>();
        }

        public int ItemId { get; set; }
        public int StockMaintaingType { get; set; }
        public decimal TotalIssuedQty { get; set; }
        public decimal TotalIssuedStockValue { get; set; }

        public List<IssuedInventoryBatches> Batches { get; set; }
    }
    public class IssuedInventoryBatches
    {
        [DBField("BATCH_ID")]
        public int BatchId { get; set; }

        [DBField("BATCH_CODE")]
        public int BatchCode { get; set; }

        [DBField("ISSUED_QTY")]
        public decimal IssuedQty { get; set; }

        [DBField("ISSUED_STOCK_VALUE")]
        public decimal IssuedStockValue { get; set; }
    }
}
