using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class ItemCategory
    {
        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("CRAETED_DATE")]
        public DateTime CategorDate { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("COMPANY_ID")]
        public int companyId { get; set; }

        public List<ItemCategoryOwners> ItemCategoryOwners { get; set; }
    }
}
