using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    [Serializable]
    public class PrMasterV2
    {
        [DBField("PR_ID")]
        public int PrId { get; set; }
        [DBField("PR_CODE")]
        public string PrCode { get; set; }
        [DBField("PR_CODE_TEXT")]
        public string PrCodeText { get; set; }
        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }
        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }
        [DBField("WAREHOUSE_NAME")]
        public string WarehouseName { get; set; }

        [DBField("LOCATION")]
        public string WareHouseLocation { get; set; }

        [DBField("PR_TYPE")]
        public int PrType { get; set; }
        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }
        [DBField("EXPECTED_DATE")]
        public DateTime ExpectedDate { get; set; }
        [DBField("PURCHASE_PROCEDURE")]
        public int PurchaseProcedure { get; set; }
        [DBField("REQUIRED_FOR")]
        public string RequiredFor { get; set; }
        [DBField("PR_CATEGORY_ID")]
        public int PrCategoryId { get; set; }
        [DBField("PR_CATEGORY_NAME")]
        public string PrCategoryName { get; set; }
        [DBField("PR_SUB_CATEGORY_ID")]
        public int PrSubCategoryId { get; set; }
        [DBField("PR_SUB_CATEGORY_NAME")]
        public string PrSubCategoryName { get; set; }
        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }
        [DBField("EXPENSE_TYPE")]
        public int ExpenseType { get; set; }
        [DBField("EXPENSE_REMARKS")]
        public string ExpenseRemarks { get; set; }
        [DBField("IS_BUDGET")]
        public int ISBudget { get; set; }
        [DBField("BUDGET_AMOUNT")]
        public decimal BudgetAmount { get; set; }
        [DBField("BUDGET_INFO")]
        public string BudgetInfo { get; set; }
        [DBField("CREATED_BY")]
        public int CreatedBy { get; set; }
        [DBField("CREATED_BY_NAME")]
        public string CreatedByName { get; set; }
        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("EXPENSE_APPROVED_BY_NAME")]
        public string ExpenseApprovedByName { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDate { get; set; }
        [DBField("IS_PR_APPROVED")]
        public int IsPrApproved { get; set; }
        [DBField("PR_APPROVAL_BY")]
        public int PrApprovalBy { get; set; }
        [DBField("PR_APPROVAL_BY_NAME")]
        public string PrApprovalByName { get; set; }
        [DBField("PR_APPROVAL_ON")]
        public DateTime PrApprovalOn { get; set; }
        [DBField("PR_APPROVAL_REMARKS")]
        public string PrApprvalRemarks { get; set; }
        [DBField("IS_EXPENSE_APPROVED")]
        public int IsExpenseApproved { get; set; }
        [DBField("EXPENSE_APPROVAL_BY")]
        public int ExpenseApprovalBy { get; set; }
        [DBField("EXPENSE_APPROVAL_BY_NAME")]
        public string ExpenseApprovalByName { get; set; }
        [DBField("EXPENSE_APPROVAL_ON")]
        public DateTime ExpenseApproalOn { get; set; }
        [DBField("EXPENSE_APPROVAL_REMARKS")]
        public string ExpenseApprovalRemarks { get; set; }
        [DBField("IS_TERMINATED")]
        public int IsTerminated { get; set; }
        [DBField("TERMINATED_BY")]
        public int TerminatedBy { get; set; }
        [DBField("TERMINATED_BY_NAME")]
        public string TerminatedByName { get; set; }
        [DBField("TERMINATED_ON")]
        public DateTime TerminatedOn { get; set; }
        [DBField("TERMINATION_REMARKS")]
        public string TerminationRemarks { get; set; }
        [DBField("IS_CLONE")]
        public int IsClone { get; set; }
        [DBField("APPROVED_BY_NAME")]
        public string ApprovedByName { get; set; }


        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }
        [DBField("STORE_KEEPER_ID")]
        public int StoreKeeperId { get; set; }
        [DBField("STORE_KEEPER_NAME")]
        public string StoreKeeperName { get; set; }
        [DBField("CURRENT_STATUS")]
        public int CurrentStatus { get; set; }
        [DBField("MRN_ID")]
        public int MrnId { get; set; }

        [DBField("PARENT_PR_ID")]
        public int ParentPrId { get; set; }

        #region Taken From MrnMasterTable
        [DBField("MRN_CODE")]
        public string MrnCode { get; set; }
        [DBField("SUB_DEPARTMENT_ID")]
        public int SubDepartmentId { get; set; }
        [DBField("SUB_DEPARTMENT_NAME")]
        public string SubDepartmentName { get; set; }
        #endregion

        [DBField("APPROVED_SIGNATURE")]
        public string ApprovedSignature { get; set; }

        [DBField("CREATED_SIGNATURE")]
        public string CreatedSignature { get; set; }

        [DBField("TERMINATED_SIGNATURE")]
        public string TerminatedSignature { get; set; }
        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }


        [DBField("UPDATED_DATETIME")]
        public DateTime UpdatedDateTime { get; set; }
        [DBField("UPDATED_BY")]
        public int UpdatedBy { get; set; }

        [DBField("EXPENSEAPPROVAL_SIGNATURE")]
        public string ExpenseApprovalSignature { get; set; }

        [DBField("IS_PR_APPROVED")]
        public int PrIsApproved { get; set; }
        [DBField("PR_APPROVAL_BY")]
        public int PrIsApprovedOrRejectedBy { get; set; }
        [DBField("PR_APPROVAL_ON")]
        public DateTime PrIsApprovedOeRejectDate { get; set; }
        [DBField("DATE_OF_REQUEST")]
        public DateTime DateOfRequest { get; set; }

        [DBField("MRN_EXPECTED_DATE")]
        public DateTime MRNExpectedDate { get; set; }

        //*********** Newly Added ***************
        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        public List<PrDetailsV2> PrDetails { get; set; }
        public PrUpdateLog PrUpdateLog { get; set; }
        public List<PrCapexDoc> PrCapexDocs { get; set; }
        public List<PrUpdateLog> PrUpdateLogs { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<Bidding> Bids { get; set; }

        public SubDepartment SubDepartment { get; set; }
        public MrnMasterV2 MrnMaster { get; set; }
        public PrDetailsV2 PrDetail { get; set; }
        public List<PrDetailsV2> Items { get; set; }
        public MrnDetailsV2 MRNDetails { get; set; }
        public List<MrnDetailsV2> MRNDetail { get; set; }
        public List<PRDetailsStatusLog> LogDetails { get; set; }
        public List<BiddingItem> bidItems { get; set; }
        public List<POMaster> POsCreated { get; set; }
        public List<GrnMaster> GRNsCreated { get; set; }
        [DBField("IS_TABULATION_REVIEW_APPROVED")]
        public int IsTabulationReviewApproved { get; set; }

        [DBField("IS_TABULATION_REVIEW_APPROVAL_REMARK")]
        public string IsTabulationReviewApprovalRemark { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MesurementId { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("STATUS_NAME")]
        public string StatusName { get; set; }

        [DBField("EMAIL_STATUS")]
        public int EmailStatus { get; set; }

        [DBField("SUBMITTED_QUOTATIONS")]
        public int SubmittedQuotatiionsCount { get; set; }

        [DBField("IS_PO_RAISED")]
        public int IsPORaised { get; set; }

        [DBField("IMPORT_ITEM_TYPE")]
        public int ImportItemType { get; set; }

        [DBField("SPARE_PART_NUMBER")]
        public string SparePartNumber { get; set; }

        [DBField("CLONED_FROM_PR")]
        public int ClonedFromPR { get; set; }

    }
}
