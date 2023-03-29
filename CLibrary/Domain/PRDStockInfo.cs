using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    public class PRDStockInfo
    {
        [DBField("PRD_ID")]
        public int ProId { get; set; }

        [DBField("STOCK_BALANCE")]
        public decimal StockBalance { get; set; }

        [DBField("LAST_PURCHASE_PRICE")]
        public decimal LastPurchasePrice { get; set; }

        [DBField("SUPPLIER_ID")]
        public int SupplierID { get; set; }

        [DBField("LAST_PURCHASE_DATE")]
        public DateTime LastPurchaseDate { get; set; }

        [DBField("AVG_CONSUMPTION")]
        public decimal AvgConsumption { get; set; }

        [DBField("REFERENCE_NO")]
        public string ReferenceNo { get; set; }
    }
}
