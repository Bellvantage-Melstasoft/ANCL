using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
    public class SupplierCategory
    {
        private int supplierId;
        private int categoryId;
        private int isActive;

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DBField("CATEGORY_ID")]
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }

        [DBField("SUPPLIER_ID")]
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }


        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

    }
}
