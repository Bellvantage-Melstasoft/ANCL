using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class ItemSubCategory
    {
        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName{ get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }

        [DBField("EFFECTIVE_DATE")]
        public DateTime effectiveDate { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATETIME")]
        public DateTime UpdatedDate { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("COMPANY_ID")]
        public int companyId { get; set; }

        [DBField("USER_NAME")]
        public string UserName { get; set; }

    }
}
