using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class QuotationImage
    {
        [DBField("QUOTATION_IMAGE_ID")]
        public int QuotationImageId { get; set; }

        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

        [DBField("IMAGE_PATH")]
        public string ImagePath { get; set; }


        public SupplierQuotation Quotation { get; set; }

        /// <summary>
        /// to identify whether the record is a new one or deleted one when updating
        /// 1= new 2 = deleted
        /// </summary>
        public int RecordStatus { get; set; }

    }
}
