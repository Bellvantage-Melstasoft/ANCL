using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain {
    public class InvoiceImages {

        [DBField("INVOICE_IMAGE_ID")]
        public int InvoiceImageId { get; set; }

        [DBField("INVOICE_ID")]
        public int InvoiceId { get; set; }

        [DBField("IMAGE_PATH")]
        public string ImagePath { get; set; }

    }
}
