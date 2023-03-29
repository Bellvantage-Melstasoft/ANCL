using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    [Serializable]
    public class PR_Master
    {
        string subDepartmentName;

        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("PR_CODE")]
        public string PrCode { get; set; }

        [DBField("MRN_CODE")]
        public string MrnCode { get; set; }

        [DBField("DEPARTMENT_ID")]
        public int DepartmentId { get; set; }

        [DBField("DATE_OF_REQUEST")]
        public DateTime DateOfRequest { get; set; }

        [DBField("QUOTATION_FOR")]
        public string QuotationFor { get; set; }

        [DBField("CLONED_FROM_PR")]
        public int ClonedFromPR { get; set; }

        [DBField("OUR_REFERENCE")]
        public string OurReference { get; set; }

        [DBField("REQUESTED_BY")]
        public string RequestedBy { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("CREATED_BY_NAME")]
        public string CreatedByName { get; set; }

        [DBField("MRN_CREATED_BY_NAME")]
        public string MRNCreatedByName { get; set; }


        [DBField("APPROVED_BY_NAME")]
        public string ApprovedByName { get; set; }

        [DBField("UPDATED_DATETIME")]
        public DateTime UpdatedDateTime { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("PR_IS_APPROVED")]
        public int PrIsApproved { get; set; }

        [DBField("PR_IS_CONFIRMED_APPROVAL")]
        public int PrIsConfirmedApproval { get; set; }

        [DBField("PR_IS_APPROVED_OR_REJECT_BY")]
        public string PrIsApprovedOrRejectedBy { get; set; }

        [DBField("PR_IS_APPROVED_OR_REJECT_DATE")]
        public DateTime PrIsApprovedOeRejectDate { get; set; }

        [DBField("PR_IS_APPROVED_FOR_BID")]
        public int PrIsApprovedForBid { get; set; }

        [DBField("PR_IS_APPROVED_OR_REJECT_FOR_BID_BY")]
        public string PrIsApprovedOrRejectForBidBy { get; set; }

        [DBField("PR_IS_APPROVED_OR_REJECT_FOR_BID_DATE")]
        public DateTime PrIsApprovedOrRejectForBidDate { get; set; }

        [DBField("BID_TERMS_CONDITION")]
        public string BidTermsAndConditions { get; set; }

        [DBField("IS_PO_RAISED")]
        public int IsPoRaised { get; set; }

        [DBField("DEPARTMENT_NAME")]
        public string departmentName { get; set; }

        [DBField("BASE_PR_ID")]
        public int BasePrId { get; set; }

        [DBField("REJECTED_REASON")]
        public string RejectedReason { get; set; }

        [DBField("EXPENSE_TYPE")]
        public string expenseType { get; set; }

        [DBField("MounthName")]
        public string monthName { get; set; }

        [DBField("PR_Count")]
        public string prCount { get; set; }


        /// <summary>
        /// //2018-08-08
        /// </summary>
        [DBField("REF_01")]
        public string Ref01 { get; set; }

        [DBField("REF_02")]
        public string Ref02 { get; set; }

        [DBField("REF_03")]
        public string Ref03 { get; set; }

        [DBField("REF_04")]
        public string Ref04 { get; set; }

        [DBField("REF_05")]
        public string Ref05 { get; set; }

        [DBField("REF_06")]
        public string Ref06 { get; set; }

        [DBField("PR_TYPE_ID")]
        public int PrTypeid { get; set; }

        [DBField("PR_PROCEDURE")]
        public string PrProcedure { get; set; }

        [DBField("PURCHASE_PROCEDURE")]
        public int PurchaseProcedure { get; set; }


        [DBField("PURCHASE_TYPE")]
        public string PurchaseType { get; set; }

        [DBField("REQUIRED_DATE")]
        public DateTime RequiredDate { get; set; }

        [DBField("MRNREFERENCE_NO")]
        public string MRNRefNumber { get; set; }

        [DBField("CURRENT_STATUS")]
        public int CurrentStatus { get; set; }

        [DBField("ITEM_CATEGORY_ID")]
        public int ItemCategoryId { get; set; }
        [DBField("ITEM_ID")]
        public int ItemId { get; set; }
        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }
        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }
        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }
        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("REQUIRED_FOR")]
        public string RequiredFor { get; set; }

        [DBField("STORE_KEEPER_NAME")]
        public string storekeepername { get; set; }
        [DBField("ESTIMATED_AMOUNT")]
        public decimal EstimatedAmount { get; set; }
        [DBField("REQUESTED_QTY")]
        public decimal RequestedQty { get; set; }

        [DBField("PRD_ID")]
        public int PrdId { get; set; }

        // By Adee on 24.04.2020

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("LOCATION")]
        public string Location { get; set; }


        [DBField("PHONE_NO")]
        public string PhoneNo { get; set; }


        [DBField("ADDRESS")]
        public string Address { get; set; }

        /// <summary>
        /// changes done by salman on 2019-01-17
        /// </summary>
        public List<Bidding> Bids { get; set; }
        public List<PR_Details> PrDetails { get; set; }
        public PrDetailsV2 PrDetail { get; set; }
        public PrDetailsV2 Items { get; set; }


        [DBField("SUB_DEPARTMENT_NAME")]
        public string SubDepartmentName
        {
            get
            {
                return subDepartmentName;
            }

            set
            {
                subDepartmentName = value;
            }
        }

        public string Status { get; set; }
    
        public int btnEnablestatus { get; set; }
    }
    
     
}
