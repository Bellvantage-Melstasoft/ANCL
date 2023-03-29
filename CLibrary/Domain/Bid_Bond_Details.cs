using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    [Serializable]
    public class Bid_Bond_Details
    {
        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("BOND_TYPE_ID")]
        public int BondtypeId { get; set; }

        [DBField("IS_REQUIRED")]
        public int IsRequired { get; set; }

        [DBField("AMOUNT")]
        public decimal Amount { get; set; }

        [DBField("PERCENTAGE")]
        public decimal Percentage { get; set; }

        [DBField("FROM_DATE")]
        public DateTime FromDate { get; set; }

        [DBField("TO_DATE")]
        public DateTime ToDate { get; set; }

        [DBField("ENTERED_USER")]
        public string EnteredUser { get; set; }

        [DBField("ENTERED_DATE")]
        public DateTime EnteredDate { get; set; }
    }
}
