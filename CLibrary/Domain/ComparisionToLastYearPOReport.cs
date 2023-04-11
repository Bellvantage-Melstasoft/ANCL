using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Domain
{
    public class ComparisionToLastYearPOReport
    {
        [DBField("SUPPLIER_ID")]
        public int SuppilerId { get; set; }

        [DBField("PO_CODE")]
        public string POCode { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }
        [DBField("ITEM_PRICE")]
        public float ItemPrice { get; set; }

        [DBField("QUANTITY")]
        public int Qunatity { get; set; }
        [DBField("VAT_AMOUNT")]

        public float VATAmount { get; set; }
        [DBField("NBT_AMOUNT")]
        public float NBTAmount { get; set; }
        [DBField("TOTAL_AMOUNT")]

        public float TotalAmount { get; set; }
        [DBField("APPROVED_DATE")]
        public DateTime ApprovedDate { get; set; }
        [DBField("PR_TYPE")]
        public int PRType { get; set; }
        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }
        [DBField("EXPENSE_TYPE")]
        public int ExpenseType { get; set; }
        [DBField("PR_CATEGORY_ID")]
        public int PRCategoryId { get; set; }
    }
}
