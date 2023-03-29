using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class PrType
    {
        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("PR_TYPE_ID")]
        public int PrTypeId { get; set; }

        [DBField("PR_TYPE_NAME")]
        public string PrTypeName { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("DEPARTMENT_NAME")]
        public string DepartmentName { get; set; }
    }
}
