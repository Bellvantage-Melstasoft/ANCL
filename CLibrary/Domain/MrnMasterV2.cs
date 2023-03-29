using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class MrnMasterV2 {
        [DBField("MRN_ID")]
        public int MrnId { get; set; }
        [DBField("MRN_CODE")]
        public string MrnCode { get; set; }
        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }
        [DBField("SUB_DEPARTMENT_ID")]
        public int SubDepartmentId { get; set; }
        [DBField("SUB_DEPARTMENT_NAME")]
        public string SubDepartmentName { get; set; }
        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }
        [DBField("WAREHOUSE_NAME")]
        public string WarehouseName { get; set; }
        [DBField("MRN_TYPE")]
        public int MrnType { get; set; }
        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }
        [DBField("EXPECTED_DATE")]
        public DateTime ExpectedDate { get; set; }
        [DBField("PURCHASE_PROCEDURE")]
        public int PurchaseProcedure { get; set; }
        [DBField("REQUIRED_FOR")]
        public string RequiredFor { get; set; }
        [DBField("MRN_CATEGORY_ID")]
        public int MrnCategoryId { get; set; }
        [DBField("MRN_CATEGORY_NAME")]
        public string MrnCategoryName { get; set; }
        [DBField("MRN_SUB_CATEGORY_ID")]
        public int MrnSubCategoryId { get; set; }
        [DBField("MRN_SUB_CATEGORY_NAME")]
        public string MrnSubCategoryName { get; set; }
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

        [DBField("EXPENSE_APPROVED_BY_NAME")]
        public string ExpenseApprovedByName { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDate { get; set; }
        [DBField("IS_MRN_APPROVED")]
        public int IsMrnApproved { get; set; }
        [DBField("MRN_APPROVAL_BY")]
        public int MrnApprovalBy { get; set; }
        [DBField("MRN_APPROVAL_BY_NAME")]
        public string MrnApprovalByName { get; set; }
        [DBField("MRN_APPROVAL_ON")]
        public DateTime MrnApprovalOn { get; set; }
        [DBField("MRN_APPROVAL_REMARKS")]
        public string MrnApprvalRemarks { get; set; }
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
        [DBField("CLONED_FROM_MRN")]
        public int ClonedFromMrn { get; set; }
        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }
        [DBField("STORE_KEEPER_ID")]
        public int StoreKeeperId { get; set; }
        [DBField("STORE_KEEPER_NAME")]
        public string StoreKeeperName { get; set; }
        [DBField("STATUS")]
        public int Status { get; set; }

        [DBField("APPROVED_SIGNATURE")]
        public string ApprovedSignature { get; set; }
        
        [DBField("CREATED_SIGNATURE")]
        public string CreatedSignature { get; set; }

        [DBField("TERMINATED_SIGNATURE")]
        public string TerminatedSignature { get; set; }

        [DBField("STATUS_NAME")]
        public string StatusName { get; set; }

        [DBField("PR_ID")]
        public int PrId { get; set; }

        [DBField("PR_CODE")]
        public string PrCode { get; set; }

        [DBField("LOCATION")]
        public string Location { get; set; }

        [DBField("DEPARTMENT_NAME")]
        public string DepartmentName { get; set; }

        [DBField("IMPORT_ITEM_TYPE")]
        public int ImportItemType { get; set; }

        [DBField("EXPENSEAPPROVAL_SIGNATURE")]
        public string ExpenseApprovalSignature { get; set; }

        //****Newly Added****
        public string ItemName { get; set; }

        public List<MrnDetailsV2> MrnDetails { get; set; }
        public MrnUpdateLog MrnUpdateLog { get; set; }
        public List<MrnUpdateLog> MrnUpdateLogs { get; set; }
        public List<MrnCapexDoc> MrnCapexDocs { get; set; }
        public SubDepartment SubDepartment { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}
