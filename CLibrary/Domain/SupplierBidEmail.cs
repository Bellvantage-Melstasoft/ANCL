using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class SupplierBidEmail
    {
        private int prid;
        private int bidid;
        private int supplierId;
        private string supplierName;
        private string email;
       


        [DBField("PR_ID")]
        public int PRId
        {
            get { return prid; }
            set { prid = value; }
        }

        [DBField("Bid_Id")]
        public int BidId
        {
            get { return bidid; }
            set { bidid = value; }
        }

        [DBField("SUPPLIER_ID")]
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        [DBField("EMAIL")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
    }


}
