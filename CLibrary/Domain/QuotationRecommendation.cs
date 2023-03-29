using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class QuotationRecommendation
    {
        [DBField("RECOMMENDATION_ID")]
        public int RecommendationId { get; set; }

        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

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

        [DBField("OVERIDING_DESIGNATION_NAME")]
        public int OverridingDesignationName { get; set; }

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

        public int CanLoggedInUserRecommend { get; set; }

        public int CanLoggedInUserOverride { get; set; }

    }
}
