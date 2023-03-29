using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class PreviousPurchase
    {

        private int supplierId;
        private string supplierName;
        private int rating;
        private int isbalcklist;
        private decimal itemPrice;
        private string ratingStar;
        public string RatingStar
        {
            get { return ratingStar; }
            set { ratingStar = value; }
        }
        [DBField("ITEM_PRICE")]
        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        [DBField("IS_BLACKLIST")]
        public int Isbalcklist
        {
            get { return isbalcklist; }
            set { isbalcklist = value; }
        }

        [DBField("RATING")]
        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }


        [DBField("SUPPLIER_ID")]

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

    }
}
