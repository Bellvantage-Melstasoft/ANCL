using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class POHistory
    {
        [DBField("PO_ID")]
        public int PurchaseOrderId { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime PurchaseDate { get; set; }

        [DBField("SUPPLIER_ID")]
        public int SupplierId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("ITEM_PRICE")]
        public decimal ItemPrice { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }


    }
}
