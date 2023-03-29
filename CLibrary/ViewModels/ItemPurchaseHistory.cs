using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.ViewModels {
    public class ItemPurchaseHistory {

        [DBField("PO_ID")]
        public int PoId { get; set; }

        [DBField("PO_CODE")]
        public string PoCode { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("ITEM_PRICE")]
        public decimal ItemPrice { get; set; }

        [DBField("QUANTITY")]
        public decimal Quantity { get; set; }

        [DBField("SHORT_CODE")]
        public string ShortCode { get; set; }
    }
}
