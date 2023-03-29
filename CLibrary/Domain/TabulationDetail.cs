using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class TabulationDetail
    {
        [DBField("TABULATION_DETAIL_ID")]
        public int TabulationDetailId { get; set; }

        [DBField("TABULATION_ID")]
        public int TabulationId { get; set; }

        [DBField("SUPPLIER_ID")]
        public int SupplierId { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

        [DBField("QUOTATION_ITEM_ID")]
        public int QuotationItemId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("QTY")]
        public decimal Qty { get; set; }

        [DBField("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [DBField("ESTIMATED_PRICE")]
        public decimal EstimatedPrice { get; set; }

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

        [DBField("HAS_VAT")]
        public int HasVat { get; set; }

        [DBField("VAT_AMOUNT")]
        public decimal VAtAmount { get; set; }

        [DBField("HAS_NBT")]
        public int HasNbt { get; set; }

        [DBField("NBT_CALCULATION_TYPE")]
        public int NbtCalculationType { get; set; }

        [DBField("NBT_AMOUNT")]
        public decimal NbtAmount { get; set; }

        [DBField("NET_TOTAL")]
        public decimal NetTotal { get; set; }

        [DBField("IS_SELECTED")]
        public int IsSelected { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("AGENT_NAME")]
        public string AgentName { get; set; }

        [DBField("CURRENCY_NAME")]
        public string CurrencyName { get; set; }

        [DBField("COUNTRY")]
        public string CountryName { get; set; }

        [DBField("EXCHANGE_RATE")]
        public decimal ExchangeRate { get; set; }

        [DBField("COUNTRY")]
        public string Country { get; set; }

        [DBField("BRAND")]
        public string Brand { get; set; }

        [DBField("CIF")]
        public decimal CIF { get; set; }

        [DBField("DUTYPAL")]
        public decimal Dutypal { get; set; }

        [DBField("CLEARING_COST")]
        public decimal Clearingcost { get; set; }

        [DBField("TERMS")]
        public string Terms { get; set; }

        [DBField("OTHER_COST")]
        public decimal Other { get; set; }

        [DBField("HISTORY")]
        public string History { get; set; }

        [DBField("VALIDITY")]
        public DateTime Validity { get; set; }

        [DBField("REFNO")]
        public string Refno { get; set; }

        [DBField("ESTDELIVERY")]
        public string Estdelivery { get; set; }

        [DBField("REMARK")]
        public string Remark { get; set; }

        [DBField("APPROVAL_REMARK")]
        public string ApprovalRemark { get; set; }

        [DBField("QTY")]
        public decimal TotQty { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("IS_TERMINATED")]
        public int IsTerminated { get; set; }

        [DBField("TERMINATED_BY")]
        public int TerminatedBy { get; set; }

        [DBField("TERMINATION_REMARKS")]
        public string TerminationRemarks { get; set; }

        [DBField("TERMINATED_DATE")]
        public DateTime TerminatedDate { get; set; }

        [DBField("SUPPLIER_MENTIONED_ITEM_NAME")]
        public string SupplierMentionedItemName { get; set; }

        [DBField("QUOTATION_REFERENCE_CODE")]
        public string ReferenceCode { get; set; }

        [DBField("BIDDING_ITEM_ID")]
        public int BiddingItemId { get; set; }

        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("PRD_ID")]
        public int PrdId { get; set; }

        [DBField("IMP_CIF")]
        public decimal ImpCIF { get; set; }

        [DBField("IMP_TERM")]
        public int ImpTerm { get; set; }



        public int Number { get; set; }


        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }

        public int SelectedValue { get; set; }

        [DBField("SUPPLIER_AGENT_NAME")]
        public string SupplierAgentName { get; set; }

        [DBField("IMPORT_ITEM_TYPE")]
        public int ImportItemType { get; set; }

        [DBField("SPARE_PART_NUMBER")]
        public string SparePartNumber { get; set; }


        [DBField("IMP_HISTORY")]
        public string ImpHistory { get; set; }

        [DBField("IMP_HISTORY_ID")]
        public int ImpHistoryID { get; set; }

        [DBField("TERM")]
        public string Term { get; set; }

        [DBField("TERM_NAME")]
        public string TermName { get; set; }

        [DBField("IMP_CURRENCY_NAME")]
        public string ImpCurrencyName { get; set; }

        [DBField("COUNTRY_NAME")]
        public string CountryNameImp { get; set; }

        [DBField("SUPPLIER_AGENT_NAME")]
        public string SupplierAgent { get; set; }

        [DBField("IMP_BRAND")]
        public string ImpBrand { get; set; }

        [DBField("IMP_OTHER")]
        public decimal ImpOther { get; set; }

        [DBField("IMP_CLEARING")]
        public decimal ImpClearing { get; set; }

        [DBField("IMP_VALIDITY")]
        public DateTime ImpValidity { get; set; }

        [DBField("IMP_ESTDELIVERY")]
        public string ImpEstDelivery { get; set; }

        [DBField("IMP_REMARK")]
        public string ImpRemark { get; set; }

        [DBField("MILL")]
        public string Mill { get; set; }

        [DBField("UNIT_PRICE_LOCAL")]
        public decimal UnitPriceLocal { get; set; }

        [DBField("UNIT_PRICE_FOREIGN")]
        public decimal UnitPriceForeign { get; set; }

        [DBField("DAY_NO")]
        public int DayNo { get; set; }

        [DBField("PAYMENT_TYPE")]
        public string PaymentType { get; set; }

    }
}
