using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class SupplierBidBondDetails
    {
        [DBField("BID_ID")]
        public int Bid_Id { get; set; }

        [DBField("SUPPLIER_ID")]
        public int Supplier_Id { get; set; }
        
        [DBField("BOND_NO")]
        public string Bond_No { get; set; }

        [DBField("BANK")]
        public string Bank { get; set; }

        [DBField("BOND_AMOUNT")]
        public decimal Bond_Amount { get; set; }

        [DBField("EXPIRE_DATE_OF_BOND")]
        public DateTime Expire_Date_Of_Bond { get; set; }

        [DBField("RECEIPT_NO")]
        public string Receipt_No { get; set; }
    }
}
