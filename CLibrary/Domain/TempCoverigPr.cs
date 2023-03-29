using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class TempCoverigPr {

        [DBField("PARENT_PR_ID")]
        public int ParentPrId { get; set; }

        [DBField("POD_ID")]
        public int PodId { get; set; }

        [DBField("PO_ID")]
        public int PoId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("PRD_ID")]
        public int PrdId { get; set; }

        [DBField("EXTRA_QTY")]
        public decimal ExtraQty { get; set; }

        [DBField("RECEIVED_QTY")]
        public decimal ReceivedQty { get; set; }
    }
}
