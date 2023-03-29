using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class GeneralSettings
    {
        [DBField("DEPARTMENT_ID")]
        public int DepartmentId { get; set; }

        [DBField("BID_OPENING_PERIOD")]
        public decimal BidOpeningPeriod { get; set; }

        [DBField("CAN_OVERRIDE")]
        public int CanOverride { get; set; }

        [DBField("BID_ONLY_REGISTERED_SUPPLIER")]
        public int BidOnlyRegisteredSupplier { get; set; }

        [DBField("VIEW_BIDS_ONLINE_UPONPR_CREATION")]
        public int ViewBidsOnlineUponPrCreation { get; set; }

    }
}
