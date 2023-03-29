using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class SupplierType {

        [DBField("TYPE_ID")]
        public int TypeId { get; set; }

        [DBField("SUPPLIER_TYPE")]
        public string SupplierTypeName { get; set; }
    }
}
