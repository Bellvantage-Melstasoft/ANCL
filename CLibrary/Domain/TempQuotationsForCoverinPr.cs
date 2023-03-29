using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class TempQuotationsForCoverinPr {

        [DBField("QUOTATIO_ID")]
        public int QuotationId { get; set; }

        [DBField("QUOTATIO_ITEM_ID")]
        public int QuotationItemId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("PARNT_PR_ID")]
        public int ParentPrId { get; set; }

        [DBField("SUPPLIER_ID")]
        public int SupplierId { get; set; }

        [DBField("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [DBField("QTY")]
        public decimal Qty { get; set; }

        [DBField("VAT")]
        public decimal Vat { get; set; }

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

        [DBField("NET_TOTAL")]
        public decimal NetTotal { get; set; }

        
    }
}
