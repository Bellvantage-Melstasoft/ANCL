using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    public class SupplierQuotationItem
    {

        [DBField("QUOTATION_ITEM_ID")]
        public int QuotationItemId { get; set; }

        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

        [DBField("BIDDING_ITEM_ID")]
        public int BiddingItemId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("QTY")]
        public decimal Qty { get; set; }

        [DBField("ESTIMATED_PRICE")]
        public decimal EstimatedPrice { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

        [DBField("HAS_VAT")]
        public int HasVat { get; set; }

        [DBField("VAT_AMOUNT")]
        public decimal VatAmount { get; set; }

        [DBField("HAS_NBT")]
        public int HasNbt { get; set; }

        [DBField("NBT_CALCULATION_TYPE")]
        public int NbtCalculationType { get; set; }

        [DBField("NBT_AMOUNT")]
        public decimal NbtAmount { get; set; }

        [DBField("NET_TOTAL")]
        public decimal TotalAmount { get; set; }

        public SupplierQuotation Quotation { get; set; }
        public BiddingItem BidItem { get; set; }
        public AddItem Item { get; set; }


        [DBField("IS_SELECTED")]
        public int IsSelected { get; set; }
        public List<SupplierBOM> SupplierBOMs { get; set; }

        [DBField("ITEM_REFERENCE_CODE")]
        public string ItemReferenceCode { get; set; }

        [DBField("QUOTATION_REFERENCE_CODE")]
        public string ReferenceCode { get; set; }

        [DBField("CURRENCY_ID")]
        public int CurrencyId { get; set; }

        [DBField("EXCHANGE_RATE")]
        public decimal ExchangeRate { get; set; }

        [DBField("EXCHANGE_RATE_IMP")]
        public decimal ExchangeRateImp { get; set; }

        [DBField("AGENT_ID")]
        public int AgentId { get; set; }

        [DBField("MILL")]
        public string Mill { get; set; }

        [DBField("CURRENCY_NAME")]
        public string CurrencyType { get; set; }

        [DBField("CURRENCY_TYPE_ID")]
        public int CurrencyTypeId { get; set; }

        [DBField("SUPPLIER_AGENT_NAME")]
        public string SupplierAgent { get; set; }

        [DBField("SUPPLIER_AGENT")]
        public int SupplierAgentId { get; set; }

        [DBField("COUNTRY_NAME")]
        public string Country { get; set; }

        

        [DBField("CIF")]
        public decimal CIF { get; set; }

        [DBField("IMP_CIF")]
        public decimal ImpCIF { get; set; }

        [DBField("DUTYPAL")]
        public decimal Dutypal { get; set; }

        [DBField("DUTY_PAL")]
        public decimal DutypalImp { get; set; }

        [DBField("CLEARING_COST")]
        public decimal Clearingcost { get; set; }

        [DBField("TERMS")]
        public string Terms { get; set; }

        [DBField("TERM")]
        public string Term { get; set; }


        [DBField("TERM_NAME")]
        public string TermName { get; set; }

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

        [DBField("TABULATION_ID")]
        public int TabulationId { get; set; }

        [DBField("SUPPLIER_ID")]
        public int SupplierId { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("IS_QUOTATION_ITEM_APPROVAL_REMARK")]
        public string IsQuotationItemApprovalRemark { get; set; }

        [DBField("IS_QUOTATION_ITEM_APPROVED")]
        public int IsQuotationItemApproved { get; set; }

        [DBField("ACTUAL_PRICE")]
        public decimal ActualPrice { get; set; }
        
        [DBField("SHORT_CODE")]
        public String MeasurementShortName { get; set; }
       
        [DBField("SUPPLIER_MENTIONED_ITEM_NAME")]
        public string SupplierMentionedItemName { get; set; }

        [DBField("SELECTED_QUOTATION")]
        public int SelectedQuotation { get; set; }

        [DBField("QUOTATION_SELECTED_BY")]
        public int QuotationSelectedBy { get; set; }

        [DBField("SELECTED_BY_NAME")]
        public string SelectedByName { get; set; }

        [DBField("QUOTATION_SELECTED_ON")]
        public DateTime SelectedDate { get; set; }


        public string status { get; set; }

        public int QuotationCount { get; set; }

        public string Days { get; set; }

        public decimal SelectedQuantity { get; set; }
        public decimal SelectedUnitPrice { get; set; }
        public decimal SelectedSubTotal { get; set; }
        public decimal SelectedNbtAmount { get; set; }
        public decimal SelectedVatAmount { get; set; }
        public decimal SelecetedNetTotal { get; set; }
        public int IsSelectedTB { get; set; }
        public int No { get; set; }

        [DBField("T_QTY")]
        public decimal TQty { get; set; }

        [DBField("T_SUB_TOT")]
        public decimal TSubTot { get; set; }

        [DBField("T_VAT")]
        public decimal TVat { get; set; }

        [DBField("T_NET")]
        public decimal TNetTot { get; set; }

        [DBField("IMP_REMARK")]
        public string ImpRemark { get; set; }

        [DBField("BRAND")]
        public string Brand { get; set; }

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

        [DBField("IMP_HISTORY")]
        public string ImpHistory { get; set; }

        [DBField("IMP_HISTORY_ID")]
        public int ImpHistoryID { get; set; }

        

        [DBField("PROCEED_REMARK")]
        public string ProceedRemark { get; set; }

        

        [DBField("XID_RATE")]
        public decimal xid { get; set; }

        [DBField("CID_RATE")]
        public decimal cid { get; set; }

        [DBField("PAL_RATE")]
        public decimal pal { get; set; }

        [DBField("EIC_RATE")]
        public decimal eic { get; set; }

        [DBField("AIR_FREIGHT")]
        public decimal AirFreight { get; set; }

        [DBField("INSURENCE")]
        public decimal Insurance { get; set; }

        [DBField("VAT_RATE")]
        public decimal VatRate { get; set; }

        [DBField("XID_VALUE")]
        public decimal XIDValue { get; set; }

        [DBField("CID_VALUE")]
        public decimal CIDValue { get; set; }

        [DBField("PAL_VALUE")]
        public decimal PALValue { get; set; }

        [DBField("EIC_VALUE")]
        public decimal EICValue { get; set; }

        [DBField("VAT_VALUE")]
        public decimal VATValueIMP { get; set; }

        [DBField("HS_ID")]
        public string HsId { get; set; }

        [DBField("NEW_HS_ID")]
        public string NewHSId { get; set; }

        [DBField("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }

        [DBField("NO_OF_DAYS_PAYEMENT_MODE")]
        public int NoOfDaysPaymentMode { get; set; }

        [DBField("UNIT_PRICE_LKR")]
        public decimal UnitPriceLkr { get; set; }

        //TABULATION_DETAIL -> APPROVAL_REMARK -> SUPPLIER_APPROVAL_REMARK
        [DBField("SUPPLIER_APPROVAL_REMARK")]
        public string SupplierApprovalRemark { get; set; }

    }
}
