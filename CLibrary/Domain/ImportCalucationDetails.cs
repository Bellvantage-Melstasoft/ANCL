using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class ImportCalucationDetails
    {
        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

        public string SNumber { get; set; }

        public string  TNumber { get; set; }

        public string  ReferenceNumber { get; set; }

        [DBField("BRAND")]
        public string Brand { get; set; }
        [DBField("SUPPLIER_NAME")]
        public string Supplier  { get; set; }
        [DBField("MILL")]
        public string  Mill { get; set; }
        [DBField("NAME")]
        public string Country { get; set; }
        [DBField("AGENT")]
        public string  Agent { get; set; }

       
        public decimal Gsm { get; set; }
        [DBField("TERM")]
        public string Term { get; set; }
        [DBField("CIF")]
        public decimal OrginalCIFAmount { get; set; }

        public decimal CIFAmountLKR { get; set; }
        [DBField("DUTY_PAL")]
        public decimal DutyPAL { get; set; }
        [DBField("OTHER")]
        public decimal Other { get; set; }

        public decimal DuctyPALOther { get; set; }

        public decimal CostOfChemicals { get; set; }

        public decimal LandedCostLKR { get; set; }

        [DBField("CLEARING_COST")]
        public decimal ClearingCost { get; set; }

        public decimal ClearingCostLKR { get; set; }

        [DBField("VALIDITY")]
        public string Validity { get; set; }
        [DBField("EST_DELIVERY")]
        public string EstDelivery { get; set; }
        [DBField("HISTORY")]
        public string ImportHistory { get; set; }
        [DBField("PAYMENT_MODE")]
        public string PaymentMode { get; set; }

        [DBField("REMARK")]
        public string Remarks { get; set; }

        [DBField("EXCANGE_RATE_VALUE")]
        public decimal ExchangeRateValueOld { get; set; }

        public decimal LandedCost { get; set; }

        public decimal DutyPALAmount { get; set; }
        public decimal ExchnageRateNew { get; set; }

        public string Currency { get; set; }

        [DBField("CURRENCY_SHORT_NAME")]
        public string CurrencyShortName { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal NBT { get; set; }
        public decimal VAT { get; set; }
        public decimal NetTotal { get; set; }
        public decimal EstCost { get; set; }

        [DBField("EXCHANGE_RATE")]
        public decimal ExchangeRate { get; set; }
    }
}
