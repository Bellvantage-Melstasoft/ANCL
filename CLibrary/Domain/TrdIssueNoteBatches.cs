using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class TrdIssueNoteBatches
    {
        [DBField("TRD_IN_ID")]
        public int TrdInId { get; set; }

        [DBField("BATCH_ID")]
        public int BatchId { get; set; }

        [DBField("ISSUED_QTY")]
        public decimal IssuedQty { get; set; }

        [DBField("ISSUED_STOCK_VALUE")]
        public decimal IssuedStockValue { get; set; }

        [DBField("BATCH_CODE")]
        public int BatchCode { get; set; }
    }
}
