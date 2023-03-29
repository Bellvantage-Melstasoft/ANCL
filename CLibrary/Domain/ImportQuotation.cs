using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class ImportQuotation {

        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

        [DBField("AGENT_ID")]
        public int AgentId { get; set; }

        [DBField("CURRENCY_TYPE_ID")]
        public int CurrencyTypeId { get; set; }

        [DBField("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }

        [DBField("TERM_ID")]
        public int TermId { get; set; }

        [DBField("TRANSPORT_MODE_ID")]
        public int TransportModeId { get; set; }

        [DBField("CONTAINER_SIZE_ID")]
        public int ContainerSizeId { get; set; }

        [DBField("SUPPLIER_AGENT")]
        public int SupplierAgentId { get; set; }

        [DBField("COUNTRY")]
        public int Country { get; set; }

        [DBField("NO_OF_DAYS_PAYEMENT_MODE")]
        public int PaymentModeDays { get; set; }

        public List<ImportQuotationItem> ImportQuotationItemList { get; set; }
    }
}
