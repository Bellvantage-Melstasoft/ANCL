using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;


namespace CLibrary.Domain
{
    public class BiddingPlan
    {
        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("PLAN_ID")]
        public int PlanId { get; set; }

        [DBField("START_DATE")]
        public DateTime StartDate { get; set; }

        [DBField("END_DATE")]
        public DateTime EndDate { get; set; }

        [DBField("ACTUAL_DATE")]
        public DateTime ActualDate { get; set; }

        [DBField("ENTERED_USER")]
        public string EnteredUser { get; set; }

        [DBField("ENTERED_DATE")]
        public DateTime EnteredDate { get; set; }

        [DBField("PLAN_NAME")]
        public string Planname { get; set; }

        [DBField("IS_COMPLETED")]
        public int Iscompleted { get; set; }

        [DBField("WITH_TIME")]
        public int WithTime { get; set; }

        public List<BiddingPlanFileUpload> biddingPlanFileUpload { get; set; }
    }


    public class BiddingPlanFileUpload
    {
        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("PLAN_ID")]
        public int PlanId { get; set; }

        [DBField("SEQ_ID")]
        public int sequenceId { get; set; }

        [DBField("FILE_NAME")]
        public string filename { get; set; }

        [DBField("FILE_PATH")]
        public string filepath { get; set; }

        [DBField("BID_CODE")]
        public int BidCode { get; set; }

        
    }
}
