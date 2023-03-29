using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    public class UserRole
    {
        [DBField("ROLE_ID")]
        public int userRoleId { get; set; }

        [DBField("ROLE_NAME")]
        public string userRoleName { get; set; }

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

        public List<RoleFunction> _roleFunctionList { get; set; }
    }
}
