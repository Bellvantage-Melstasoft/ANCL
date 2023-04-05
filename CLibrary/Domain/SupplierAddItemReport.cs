using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Domain
{
    [Serializable]
    public class SupplierAddItemReport
    {
        [DBField("SUPPLIER_ID")]
        public int SupplierId { get; set; }


        [DBField("SUPPLIER_NAME")]
        public String SupplierName { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDate { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("GRN_CODE")]
        public string GrnCode { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }
    }
}
