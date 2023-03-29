using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    [Serializable]
    public class TabulationMaster
    {
        [DBField("TABULATION_ID")]
        public int TabulationId { get; set; }

        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("BID_CODE")]
        public int BidCode { get; set; }

        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("CREATED_BY")]
        public int CreatedBy { get; set; }

        [DBField("CREATED_BY_NAME")]
        public string CreatedByName { get; set; }

        [DBField("CREATED_ON")]
        public DateTime CreatedOn { get; set; }

        [DBField("IS_CURRENT")]
        public int IsCurrent { get; set; }

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

        [DBField("VAT_AMOUNT")]
        public decimal VatAmount { get; set; }

        [DBField("NBT_AMOUNT")]
        public decimal NbtAmount { get; set; }

        [DBField("NET_TOTAL")]
        public decimal NetTotal { get; set; }
        
        [DBField("IS_SELECTED")]
        public int IsSelected { get; set; }

        [DBField("SELECTION_REMARKS")]
        public string SelectionRemarks { get; set; }

        [DBField("SELECTED_BY")]
        public int SelectedBy { get; set; }

        [DBField("SELECTED_BY_NAME")]
        public string SelectedByName { get; set; }

        [DBField("SELECTED_ON")]
        public DateTime SelectedOn { get; set; }

        [DBField("RECOMMENDATION_TYPE")]
        public int RecommendationType { get; set; }

        [DBField("REQUIRED_RECOMMENDATION_COUNT")]
        public int RequiredRecommendationCount { get; set; }

        [DBField("RECOMMENDED_COUNT")]
        public int RecommendedCount { get; set; }

        [DBField("RECOMMENDATION_OVERIDING_DESIGNATION")]
        public int RecommendationOveridingDesignation { get; set; }

        [DBField("RECOMMENDATION_WAS_OVERIDDEN")]
        public int RecommendationWasOveridden { get; set; }

        [DBField("RECOMMENDATION_OVERRIDDEN_BY")]
        public int RecommendationOveriddenBy { get; set; }

        [DBField("RECOMMENDATION_OVERRIDDEN_BY_NAME")]
        public string RecommendationOveriddenByName { get; set; }

        [DBField("RECOMMENDATION_OVERIDDEN_ON")]
        public DateTime RecommendationOveriddenOn { get; set; }

        [DBField("RECOMMENDATION_OVERRIDING_REMARKS")]
        public string RecommendationOverridingRemarks { get; set; }

        [DBField("IS_RECOMMENDATION_DOC_UPLOADED")]
        public int IsRecommendationDocUploaded { get; set; }

        [DBField("APPROVAL_OVERIDING_DESIGNATION_NAME")]
        public string ApprovalOverridingDesignationUserName { get; set; }

        [DBField("RECOMMENDATION_OVERIDING_DESIGNATION_NAME")]
        public string RecommendationOverridingDesignationUserName { get; set; }


        [DBField("IS_RECOMMENDED")]
        public int IsRecommended { get; set; }

        [DBField("APPROVAL_TYPE")]
        public int ApprovalType { get; set; }

        [DBField("REQUIRED_APPROVAL_COUNT")]
        public int RequiredApprovalCount { get; set; }

        [DBField("APPROVED_COUNT")]
        public int ApprovedCount { get; set; }

        [DBField("APPROVAL_OVERIDING_DESIGNATION")]
        public int ApprovalOveridingDesignation { get; set; }

        [DBField("APPROVAL_WAS_OVERIDDEN")]
        public int ApprovalWasOveridden { get; set; }

        [DBField("APPROVAL_OVERRIDDEN_BY")]
        public int ApprovalOverriddenBy { get; set; }

        [DBField("APPROVAL_OVERRIDDEN_BY_NAME")]
        public string ApprovalOverriddenByName { get; set; }

        [DBField("APPROVAL_OVERIDDEN_ON")]
        public DateTime ApprovalOverriddenOn { get; set; }

        [DBField("APPROVAL_OVERRIDING_REMARKS")]
        public string ApprovalOverridingRemarks { get; set; }

        [DBField("IS_APPROVAL_DOC_UPLOADED")]
        public int IsApprovalDocUploaded { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }




        public List<TabulationDetail> QuotationItems { get; set; }
        public List<TabulationDetail> TabulationDetails { get; set; }
        public List<TabulationRecommendation> TabulationRecommendations { get; set; }
        public List<TabulationApproval> TabulationApprovals { get; set; }

        public int CanLoggedInUserOverrideRecommendation { get; set; }

        public int CanLoggedInUserOverrideApproval { get; set; }
        
        public int IsPORaisedforPRDetail { get; set; }

        public int Visible { get; set; }
    }
}
