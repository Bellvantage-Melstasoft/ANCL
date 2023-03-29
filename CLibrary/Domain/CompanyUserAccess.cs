using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
   public class CompanyUserAccess
    {
        [DBField("USER_ID")]
        public int userId { get; set; }

        [DBField("DEPARTMENT_ID")]
        public int departmentId { get; set; }

        [DBField("SYSTEM_DIVISION_ID")]
        public int sysDivisionId { get; set; }

        [DBField("USER_ROLE_ID")]
        public int userRoleID { get; set; }

        [DBField("ACTION_ID")]
        public int actionId { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }





        [DBField("DIVISION_NAME")]
        public string sysDivisionName { get; set; }

        [DBField("ROLE_NAME")]
        public string userRoleName { get; set; }

        [DBField("FUNCTION_NAME")]
        public string functionActionName { get; set; }

        [DBField("FIRST_NAME")]
        public string firstName { get; set; }
        public List<RoleFunction> _roleFunctionList { get; set; }
        public List<CompanyUserAccess> _CompanyUserAccessList { get; set; }


    }
}
