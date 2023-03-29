using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class CurrencyRate {

        [DBField("CURRENCY_TYPE_ID")]
        public int CurrencyTypeId { get; set; }

        [DBField("DATE")]
        public DateTime Date { get; set; }

        [DBField("BUYING_RATE")]
        public decimal BuyingRate { get; set; }

        [DBField("SELLING_RATE")]
        public decimal SellingRate { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        [DBField("CURRENCY_NAME")]
        public string CurrentcyName { get; set; }

        [DBField("CURRENCY_SHORT_NAME")]
        public string CurrentcyShortName { get; set; }

        [DBField("ID")]
        public int Id { get; set; }

        [DBField("NAME")]
        public string Name { get; set; }



    }
}
