using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class QuotationImages
    {
        private int quotationNo;
        private int itemId;
        private int prID;
        private int supplierId;
        private string imagePath;

        [DBField("IMAGE_PATH")]
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }
        
        [DBField("SUPPLIER_ID")]
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        [DBField("PR_ID")]
        public int PrID
        {
            get { return prID; }
            set { prID = value; }
        }

        [DBField("ITEM_ID")]
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        [DBField("QUOTATION_NO")]
        public int QuotationNo
        {
            get { return quotationNo; }
            set { quotationNo = value; }
        }

    }
}
