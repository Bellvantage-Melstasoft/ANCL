using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;


namespace CLibrary.Domain {
    public class GrnReturnDetails {

        [DBField("GRND_RETURN_ID")]
        public int GrndReturnId { get; set; }

        [DBField("GRND_ID")]
        public int GrndId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        [DBField("GRN_ID")]
        public int GrnId { get; set; }

        [DBField("RETURNED_QTY")]
        public decimal ReturnedQty { get; set; }

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

        [DBField("VAT_VALUE")]
        public decimal VatValue { get; set; }

        [DBField("NET_TOTAL")]
        public decimal NetTotal { get; set; }

        [DBField("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("SHORT_CODE")]
        public string ShortCode { get; set; }

        [DBField("SUPPLIER_MENTIONED_ITEM_NAME")]
        public string SupplierMentionedItemName { get; set; }
        
        public int StockMaintainingType { get; set; }
        public int IsApproved { get; set; }
        public int podId { get; set; }
    }
}
