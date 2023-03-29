using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    [Serializable]
    public class SupplierQuotation
    {
        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("SUPPLIER_ID")]
        public int SupplierId { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

        [DBField("VAT_AMOUNT")]
        public decimal VatAmount { get; set; }

        [DBField("NBT_AMOUNT")]
        public decimal NbtAmount { get; set; }

        [DBField("NET_TOTAL")]
        public decimal NetTotal { get; set; }

        [DBField("SUPPLIER_TERMS_CONDITIONS")]
        public string TermsAndCondition { get; set; }

        [DBField("IS_SELECTED")]
        public int IsSelected { get; set; }

        [DBField("SELECTION_REMARKS")]
        public string SelectionRemarks { get; set; }

        [DBField("SELECTED_BY")]
        public int SelectedBy { get; set; }

        [DBField("SELECTED_BY_NAME")]
        public string SelectedByName { get; set; }

        [DBField("SELECTED_ON")]
        public DateTime SelectedByOn { get; set; }

        [DBField("IS_STAYED_AS_LATER_BID")]
        public int IsStayedAsLaterBid { get; set; }

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

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }

        [DBField("IS_ADDED_TO_PO")]
        public int IsApprovedPO { get; set; }

        [DBField("IS_DOC_UPLOADED")]
        public int IsUploadeded { get; set; }

        [DBField("RECOMMENDATION_TYPE")]
        public int RecomondationType { get; set; }


        [DBField("CURRENCY_TYPE_ID")]
        public int CurrencyTypeId { get; set; }

        [DBField("TERM_ID")]
        public int TermId { get; set; }

        [DBField("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }

        [DBField("CURRENCY_SHORT_NAME")]
        public string CurrencyShortname { get; set; }

        [DBField("CURRENCY_NAME")]
        public string CurrencyName { get; set; }

        [DBField("SUPPLIER_AGENT")]
        public int SupplierAgentId { get; set; }

        [DBField("PAYMENT_MODE")]
        public string PaymentMode { get; set; }

        [DBField("TERM_NAME")]
        public string TermName { get; set; }

        [DBField("CLEARING_AGENT")]
        public int ClearingAgent { get; set; }

        [DBField("TRANSPORT_MODE_ID")]
        public int TransportModeId { get; set; }

        [DBField("AGENT_ID")]
        public int AgentId { get; set; }
        
        [DBField("COUNTRY")]
        public int Country { get; set; }
        
        [DBField("SUPPLIER_AGENT")]
        public int SupplierAgent { get; set; }

        [DBField("SELLING_RATE")]
        public decimal SellingRate { get; set; }

        [DBField("CONTAINER_SIZE_ID")]
        public int ContainersizeId { get; set; }

        public Bidding Bid { get; set; }

        public Supplier SupplierDetails { get; set; }

        public List<SupplierQuotationItem> QuotationItems { get; set; }
        public List<ImportQuotation> ImportQuotationList { get; set; }
        public List<ImportQuotationItem> ImportQuotationItemList { get; set; }
        public List<SupplierBiddingFileUpload> UploadedFiles { get; set; }
        public List<QuotationImage> QuotationImages { get; set; }
        public List<QuotationRecommendation> QuotationRecommendations { get; set; }
        public List<QuotationApproval> QuotationApprovals { get; set; }

        public CurrencyRate objCurrencyDetails { get; set; }
        public ImportCalucationDetails objImportCalucationDetails { get; set; }



        /*1 = modified*/
        public int recordStatus { get; set; }

        public int CanLoggedInUserSelect { get; set; }

        public int CanLoggedInUserOverrideRecommendation { get; set; }

        public int CanLoggedInUserOverrideApproval { get; set; }

        [DBField("QUOTATION_REFERENCE_CODE")]
        public string QuotationReferenceCode { get; set; }
        public SupplierRatings SupplierRating { get; set; }

    
        public int IsQuotationTabulationApproved { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("NO_OF_DAYS_PAYEMENT_MODE")]
        public int PaymentModeDays { get; set; }

        

    }
    
}
