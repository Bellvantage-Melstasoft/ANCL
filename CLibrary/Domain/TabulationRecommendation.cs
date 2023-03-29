using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class TabulationRecommendation
    {
        [DBField("RECOMMENDATION_ID")]
        public int RecommendationId { get; set; }

        [DBField("TABULATION_ID")]
        public int TabulationId { get; set; }

        [DBField("RECOMMEND_BY_TYPE")]
        public int RecommendedByType { get; set; }

        [DBField("DESIGNATION_ID")]
        public int DesignationId { get; set; }

        [DBField("DESIGNATION_NAME")]
        public string DesignationName { get; set; }

        [DBField("SEQUENCE")]
        public int Sequence { get; set; }

        [DBField("CAN_OVERIDE")]
        public int CanOverride { get; set; }

        [DBField("OVERIDING_DESIGNATION")]
        public int OverridingDesignation { get; set; }

        //[DBField("OVERIDING_DESIGNATION_NAME")]
        //public string OverridingDesignationName { get; set; }

        [DBField("OVERIDING_DESIGNATION_NAME")]
        public string OverridingDesignationUserName { get; set; }

        [DBField("IS_RECOMMENDED")]
        public int IsRecommended { get; set; }

        [DBField("RECOMMENDED_BY")]
        public int RecommendedBy { get; set; }

        [DBField("RECOMMENDED_BY_NAME")]
        public string RecommendedByName { get; set; }

        [DBField("RECOMMENDED_DATE")]
        public DateTime RecommendedDate { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        [DBField("WAS_OVERIDDEN")]
        public int WasOverriden { get; set; }

        [DBField("IS_ADDED_TO_PO")]
        public int IsApprovedPO { get; set; }

        public int CanLoggedInUserRecommend { get; set; }

        public int CanLoggedInUserOverride { get; set; }
        public int IsPORaisedforPRDetail { get; set; }
    }
}
