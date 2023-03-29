using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class NaturseOfBusiness
    {
        [DBField("BUSINESS_CATEGORY_ID")]
        public int BusinessCategoryId { get; set; }

        [DBField("BUSINESS_CATEGORY_NAME")]
        public string BusinessCategoryName { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("CREATED_USER")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate { get; set; }

        [DBField("UPDATED_USER")]
        public string UpdatedBy { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }
    }
}
