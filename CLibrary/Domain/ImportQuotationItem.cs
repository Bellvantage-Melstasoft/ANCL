using System;
using System.Linq;
using System.Text;
using CLibrary.Common;


namespace CLibrary.Domain {
    public class ImportQuotationItem {

        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

        [DBField("QUOTATION_ITEM_ID")]
        public int QuptationItemId { get; set; }

        [DBField("BRAND")]
        public string Brand { get; set; }

        [DBField("EXCHANGE_RATE")]
        public decimal ExchangeRate { get; set; }

        [DBField("CIF")]
        public decimal CIF { get; set; }

        [DBField("DUTY_PAL")]
        public decimal DutyPal { get; set; }

        [DBField("CLEARING_COST")]
        public decimal ClearingCost { get; set; }

        [DBField("OTHER")]
        public decimal Other { get; set; }

        [DBField("HISTORY")]
        public int History { get; set; }

        [DBField("VALIDITY")]
        public DateTime Validity { get; set; }

        [DBField("EST_DELIVERY")]
        public string EstDelivery { get; set; }

        [DBField("TOTAL")]
        public decimal Total { get; set; }

        [DBField("REMARK")]
        public string Remark { get; set; }

        [DBField("TERM")]
        public string Term { get; set; }

        [DBField("HS_ID")]
        public string HsId { get; set; }

        [DBField("MILL")]
        public string Mill { get; set; }

        [DBField("TERM_NAME")]
        public string TermName { get; set; }

        [DBField("HISTORY_NAME")]
        public string HistoryName { get; set; }

        [DBField("EXCANGE_RATE_VALUE")]
        public decimal ExchangeRateValue { get; set; }

        [DBField("XID_RATE")]
        public decimal XIDRate { get; set; }

        [DBField("CID_RATE")]
        public decimal CIDRate { get; set; }

        [DBField("PAL_RATE")]
        public decimal PALRate { get; set; }

        [DBField("EIC_RATE")]
        public decimal EICRate { get; set; }

        [DBField("AIR_FREIGHT")]
        public decimal AirFreight { get; set; }

        [DBField("INSURENCE")]
        public decimal Insurance { get; set; }

        [DBField("XID_VALUE")]
        public decimal XIDValue { get; set; }

        [DBField("CID_VALUE")]
        public decimal CIDValue { get; set; }

        [DBField("PAL_VALUE")]
        public decimal PALValue { get; set; }

        [DBField("EIC_VALUE")]
        public decimal EICValue { get; set; }

        [DBField("VAT_RATE")]
        public decimal VATRate { get; set; }

        [DBField("VAT_VALUE")]
        public decimal VATValue { get; set; }

        public int ItemId { get; set; }

        public decimal Sup_SubTotal { get; set; }
        public decimal Sup_Vat { get; set; }
        public decimal Sup_Netotal { get; set; }
        public decimal Sup_UnitPrice { get; set; }



    }
}
