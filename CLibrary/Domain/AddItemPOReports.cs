using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Domain
{
    [Serializable]
    public class AddItemPOReports
    {
        [DBField("SUPPLIER_ID")]
        public int SupplierId { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }
        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }
        [DBField("TOTAL_AMOUNT")]
        public float TotalAMount { get; set; }

        [DBField("ITEM_ID")]
        public int ItemID { get; set; }
        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }
        [DBField("CATEGORY_ID")]
        public int SubCategoryId { get; set; }
        [DBField("SUB_CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDate { get; set; }
    }
}
