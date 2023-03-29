using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain {
    public class SubCategoryStoreKeeper {

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("USER_ID")]
        public int UserId { get; set; }

        [DBField("EFFECTIVE_DATE")]
        public DateTime EffectiveDate { get; set; }

        [DBField("USER_NAME")]
        public string UserName { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }


        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }


     
    }
}
