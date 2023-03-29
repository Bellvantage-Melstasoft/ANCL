using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;


namespace CLibrary.Domain {
    public class InvoiceDetails {
        [DBField("INVOICE_ID")]
        public int InvoiceId { get; set; }

        [DBField("PO_ID")]
        public int PoId { get; set; }

        [DBField("GRN_ID")]
        public int GrnId { get; set; }

        [DBField("PAYMENT_TYPE")]
        public int PaymentType { get; set; }

        [DBField("INVOICE_NO")]
        public string InvoiceNo { get; set; }

        [DBField("INVOICE_DATE")]
        public DateTime InvoiceDate { get; set; }

        [DBField("INVOICE_AMOUNT")]
        public decimal InvoiceAmount { get; set; }

        [DBField("VAT_NO")]
        public string VatNo { get; set; }

        [DBField("IS_PAYMENT_SETTLED")]
        public int IsPaymentSettled { get; set; }

        [DBField("PO_CODE")]
        public string POCode { get; set; }

        [DBField("GRN_CODE")]
        public string GRNCode { get; set; }

        [DBField("IS_CANCELLED")]
        public int IsCancelled { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("REMARKS")]
        public string Remark { get; set; }

        [DBField("REMARK_ON")]
        public DateTime RemarkOn { get; set; }

        [DBField("UPDATED_BY")]
        public int UpdatedBy { get; set; }

        [DBField("UPDATED_ON")]
        public DateTime UpdatedOn { get; set; }


        public int num { get; set; }
        public int status { get; set; }


    }
}
