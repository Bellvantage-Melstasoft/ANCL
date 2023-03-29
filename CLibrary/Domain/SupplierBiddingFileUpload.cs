using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class SupplierBiddingFileUpload
    {
        [DBField("QUOTATION_FILE_ID")]
        public int QuotationFileId { get; set; }

        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }
        
        [DBField("FILE_NAME")]
        public string FileName { get; set; }

        [DBField("FILE_PATH")]
        public string FilePath { get; set; }
        
        public SupplierQuotation Quotation { get; set; }

        /// <summary>
        /// to identify whether the record is a new one or deleted one when updating
        /// 1= new 2 = deleted
        /// </summary>
        public int RecordStatus { get; set; }
    }
}
