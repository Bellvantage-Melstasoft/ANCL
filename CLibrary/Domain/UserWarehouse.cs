using CLibrary.Common;
using System;
using System.Collections.Generic;

namespace CLibrary.Domain {
    public class UserWarehouse {

        [DBField("USER_ID")]
        public int UserId { get; set; }

        [DBField("FIRST_NAME")]
        public string UserName { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WrehouseId { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("LOCATION")]
        public string Location { get; set; }


        [DBField("IS_HEAD")]
        public int IsHead { get; set; }

        [DBField("USER_TYPE")]
        public int UserType { get; set; }

        public int IsSelected { get; set; }
    }
}
