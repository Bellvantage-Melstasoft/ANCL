using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class CompanyType {

        [DBField("TYPE_ID")]
        public int TypeId { get; set; }

        [DBField("COMPANY_TYPE")]
        public string CompanyTypeName { get; set; }
    }
}
