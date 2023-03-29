using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class RoleFunction
    {
        [DBField("ROLE_ID")]
        public int userRoleId { get; set; }

        [DBField("SYSTEM_DIVISION_ID")]
        public int systemDivisionId { get; set; }

        [DBField("FUNCTION_ID")]
        public int functionId { get; set; }

       [DBField("SYS_FUNCTION_ID")]
        public int fId { get; set; }
      
        [DBField("ROLE_NAME")]
        public string userRoleName { get; set; }

        [DBField("DIVISION_NAME")]
        public string systemDivisionName { get; set; }

        [DBField("FUNCTION_NAME")]
        public string functionName { get; set; }
        
        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("SYS_DIVISION_ID")]
        public int functionDevisionID { get; set; }

        [DBField("DIVISION_ID")]
        public int DevisionId { get; set; }

       [DBField("USER_ROLE_ID")]
        public int uroleid { get; set; }
    }
}
