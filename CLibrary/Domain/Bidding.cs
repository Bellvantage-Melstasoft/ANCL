using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    [Serializable]
    public class Bidding
    {
        /// <summary>
        /// changes done by salman on 2019-01-17
        /// </summary>
        
        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("BID_CODE")]
        public int BidCode { get; set; }

        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreateDate { get; set; }

        [DBField("CREATED_USER")]
        public int CreatedUserId { get; set; }

        [DBField("CREATED_USER_NAME")]
        public string CreatedUserName { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("COMPANY_Name")]
        public string CompanyName { get; set; }

        [DBField("START_DATE")]
        public DateTime StartDate { get; set; }

        [DBField("END_DATE")]
        public DateTime EndDate { get; set; }

        [DBField("BID_TYPE")]
        public int BidType { get; set; }

        [DBField("BID_OPEN_TYPE")]
        public int BidOpenType { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("IS_BID_COMPLETED")]
        public int IsBidComplete { get; set; }

        [DBField("BID_OPENING_PERIOD")]
        public int BidOpeningPeriod { get; set; }

        [DBField("OPEN_BID_TO")]
        public int BidOpenTo { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }

        [DBField("APPROVAL_REMARKS")]
        public string ApprovalRemarks { get; set; }

        [DBField("BID_TERMS_AND_CONDITIONS")]
        public string TermsAndConditions { get; set; }

        [DBField("IS_QUOTATION_SELECTED")]
        public int IsQuotationSelected { get; set; }

        [DBField("QUOTATION_SELECTED_BY")]
        public int QuotationSelectedBy { get; set; }

        [DBField("QUOTATION_SELECTED_BY_NAME")]
        public string QuotationSelectedByName { get; set; }

        [DBField("QUOTATION_SELECTION_DATE")]
        public DateTime QuotationSelectionDate { get; set; }

        [DBField("IS_QUOTATION_APPROVED")]
        public int IsQuotationApproved { get; set; }

        [DBField("QUOTATION_APPROVED_BY")]
        public int QuotationApprovedBy { get; set; }

        [DBField("QUOTATION_APPROVED_BY_NAME")]
        public string QuotationApprovedByName { get; set; }

        [DBField("QUOTATION_APPROVAL_DATE")]
        public DateTime QuotationApprovalDate { get; set; }

        [DBField("QUOTATION_APPROVAL_REMARKS")]
        public string QuotationApprovalRemarks { get; set; }

        [DBField("IS_QUOTATION_CONFIRMED")]
        public int IsQuotationConfirmed { get; set; }

        [DBField("QUOTATION_CONFIRMED_BY")]
        public int QuotationConfirmedBy { get; set; }

        [DBField("QUOTATION_CONFIRMED_BY_Name")]
        public string QuotationConfirmedByName { get; set; }

        [DBField("QUOTATION_CONFIRMATION_DATE")]
        public DateTime QuotationConfirmedDate { get; set; }


        [DBField("QUOTATION_CONFIRMATION_REMARKS")]
        public string QuotationConfirmationRemarks { get; set; }

        [DBField("IS_QUOTATION_ADDED_TO_PO")]
        public int IsQuotationAddedToPo { get; set; }

        [DBField("PARTICIPATED")]
        public int ParticipatedCount { get; set; }

        [DBField("SUBMITTED_QUOTATIONS")]
        public int SubmittedQuotatiionsCount { get; set; }

        [DBField("PENDING_QUOTATIONS")]
        public int PendingQuotatiionsCount { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("PURCHASING_OFFICER")]
        public int PurchasingOfficer { get; set; }

        public int NoOfQuotations { get; set; }
        public int Visibility { get; set; }
        public int IsClnedPr { get; set; }
        public int NoOfRejectedQuotations { get; set; }

        public PR_Master PRMaster { get; set; }

        public List<BiddingItem> BiddingItems { get; set;}

        public List<SupplierQuotation> SupplierQuotations { get; set; }

        public SupplierQuotation SelectedQuotation { get; set; }

        public TabulationMaster SelectedTabulation { get; set; }

        public List<BiddingPlan> BiddingPlan { get; set; }

        public List<Bid_Bond_Details> BidBondDetails { get; set; }

        public List<TabulationMaster> Tabulations { get; set; }

        /*START - Used in PR Inquiry Form*/
        public List<POMaster> POsCreated { get; set; }
        public List<GrnMaster> GRNsCreated { get; set; }
        /*END - Used in PR Inquiry Form*/

        public string MRNRefNumber { get; set; }
        public string subDepartmentName { get; set; }

        [DBField("IS_TERMINATED")]
        public int IsTerminated { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("PR_CODE")]
        public string prCode { get; set; }

        [DBField("TABULATION_REVIEW_APPROVAL_REMARK")]
        public string TabulationreviewApprovalRemark { get; set; }

        [DBField("IS_TABULATION_APPROVAL")]
        public int IsTabulationReviewApproved { get; set; }

        public int btnEnablestatus { get; set; }

        [DBField("PARTICIPATED_SUPPLIERS")]
        public int ParticipatedSuppliers { get; set; }

        [DBField("EMAIL_STATUS")]
        public int EmailStatus { get; set; }

        [DBField("IS_CLONED")]
        public int IsCloned { get; set; }

        [DBField("BID_WAS_CLONED")]
        public int BidWasCloned { get; set; }

        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }

        [DBField("PROCEED_REMARK")]
        public string ProceedRemark { get; set; }

        //****Newly Added*****
        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }


    }
}
