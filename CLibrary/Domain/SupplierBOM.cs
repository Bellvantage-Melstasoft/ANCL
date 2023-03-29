using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class SupplierBOM
    {
        [DBField("SUPPLIER_BOM_ID")]
        public int SupplierBomId { get; set; }

        [DBField("QUOTATION_ITEM_ID")]
        public int QuotationItemId { get; set; }

        [DBField("MATERIAL")]
        public string Material { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("COMPLY")]
        public int Comply { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        public SupplierQuotationItem QuotationItem { get; set; }

    }
}
