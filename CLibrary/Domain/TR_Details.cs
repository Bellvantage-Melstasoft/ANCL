using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
   public class TR_Details {

        [DBField("TRD_ID")]
        public int TRDId { get; set; }

        [DBField("TR_ID")]
        public int TRId { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("REQUESTED_QTY")]
        public decimal RequestedQTY { get; set; }

        [DBField("ISSUED_QTY")]
        public decimal IssuedQTY { get; set; }

        [DBField("RECEIVED_QTY")]
        public decimal ReceivedQTY { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("STATUS")]
        public int Status { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        [DBField("IS_TERMINATED")]
        public int IsTerminated { get; set; }

        [DBField("TERMINATED_BY")]
        public int TerminatedBy { get; set; }

        [DBField("TERMINATED_DATE")]
        public DateTime TerminatedDate { get; set; }

        [DBField("TERMINATED_REASON")]
        public string TerminatedReason { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryID { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryID { get; set; }

        [DBField("ITEM_ID")]
        public int ItemID { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string ShortName { get; set; }

        [DBField("TERMINATED_BY_NAME")]
        public string TerminatedByName { get; set; }


        [DBField("TERMINATED_BY_SIGNATURE")]
        public string TerminatedBySignature { get; set; }

        [DBField("A_QTY")]
        public decimal AvailableQty { get; set; }

        [DBField("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }
    }
}
