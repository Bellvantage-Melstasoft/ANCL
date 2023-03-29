using CLibrary.Common;
using System;
using System.Collections.Generic;

namespace CLibrary.Domain {
    public class UserSubDepartment {

        [DBField("USER_ID")]
        public int UserId { get; set; }

        [DBField("FIRST_NAME")]
        public string FirstName { get; set; }

        [DBField("SUB_DEPARTMENT_ID")]
        public int DepartmentId { get; set; }

        [DBField("DEPARTMENT_NAME")]
        public string DepartmentName { get; set; }

        [DBField("IS_HEAD")]
        public int IsHead { get; set; }

        [DBField("FIRST_NAME")]
        public string UserName { get; set; }

        public int IsSelected { get; set; }
    }
}
