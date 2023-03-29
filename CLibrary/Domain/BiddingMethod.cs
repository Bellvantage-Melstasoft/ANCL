using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class BiddingMethod
    {
        [DBField("BIDDING_METHOD_ID")]
        public int BiddingMethodId { get; set; }

        [DBField("BIDDING_METHOD_NAME")]
        public string BiddingMethodName { get; set; }

        [DBField("OPENED_FOR_DAYS")]
        public int OpenedForDays { get; set; }
    }
}
