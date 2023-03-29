using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    public class MRN_Master
    {
        [DBField("MRN_ID")]
        public int MrnId { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("SUB_DEPARTMENT_ID")]
        public int SubDepartmentId { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("EXPECTED_DATE")]
        public DateTime ExpectedDate { get; set; }

        [DBField("CREATED_BY")]
        public int CreatedBy { get; set; }

        [DBField("STATUS")]
        public string Status { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("QUOTATION_FOR")]
        public string QuotationFor { get; set; }

        [DBField("PR_TYPE_ID")]
        public int PrTypeId { get; set; }

        [DBField("EXPENSE_TYPE")]
        public string ExpenseType { get; set; }

        [DBField("PR_PROCEDURE")]
        public string PrProcedure { get; set; }

        [DBField("PURCHASE_TYPE")]
        public string PurchaseType { get; set; }

        [DBField("WAREHOUSE_ID")]
        public string WarehouseId { get; set; }

        [DBField("STORE_KEEPER_ID")]
        public int storekeeperId { get; set; }

        [DBField("ITEM_CATEGORY_ID")]
        public int itemCatId { get; set; }

    }
}
