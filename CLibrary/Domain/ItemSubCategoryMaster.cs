using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class ItemSubCategoryMaster
    {
        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATETIME")]
        public DateTime UpdatedDate { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("CURENT_COMPANY_ID")]
        public string currentCompanyId { get; set; }

        [DBField("COMPANY_ID")]
        public int companyId { get; set; }


    }
}
