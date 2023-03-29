
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class AddItemMaster
    {
        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATETIME")]
        public DateTime UpdatedDateTime { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("IMAGE_PATH")]
        public string ImagePath { get; set; }

        [DBField("REFERENCE_NO")]
        public string ReferenceNo { get; set; }


        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("CURENT_COMPANY_ID")]
        public string currentCompanyId { get; set; }

    }
}
