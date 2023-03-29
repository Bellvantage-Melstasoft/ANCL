using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class PrCompanyPrTypeMapping
    {
        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("REF_COLUMN")]
        public int RefColumn { get; set; }

        //[DBField("PR_TYPE")]
        //public int CompanyId { get; set; }
    }
}
