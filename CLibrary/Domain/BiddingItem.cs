using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    [Serializable]
    public class BiddingItem
    {
        /// <summary>
        /// changes done by salman on 2019-01-17
        /// </summary>
        [DBField("BIDDING_ITEM_ID")]
        public int BiddingItemId { get; set; }

        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("PRD_ID")]
        public int PrdId { get; set; }

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

        [DBField("DISPLAY_IMAGE")]
        public string DisplayImage { get; set; }
        [DBField("LAST_SUPPLIER_NAME")]
        public string LastSupplierName { get; set; }

        [DBField("LAST_PURCHASED_PRICE")]
        public decimal LastPurchasedPrice { get; set; }
        [DBField("IS_QUOTATION_SELECTED")]
        public int IsQuotationSelected { get; set; }
        [DBField("IS_TERMINATED")]
        public int IsTerminated { get; set; }

        [DBField("FILE_SAMPLE_PROVIDED")]
        public int FileSampleProvided { get; set; }

        //Terminate button function-pasindu-2020/04/25
        [DBField("DESCRIPTION")]
        public string ItemDescription { get; set; }

        [DBField("REMARKS")]
        public string Purpose { get; set; }
        //End-terminate button function-pasindu-2020/04/25
        [DBField("LAST_PO_ID")]
        public int LastPoId { get; set; }

        [DBField("LAST_SUPPLIER_ID")]
        public int LSupplierId { get; set; }
        
        [DBField("LAST_PURCHASED_PRICE")]
        public decimal LPurchasedPrice { get; set; }

        [DBField("SELECTED_BY_NAME")]
        public string PerformedByName { get; set; }

        [DBField("SELECTED_ON")]
        public DateTime PerformedOn { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string UnitShortName { get; set; }

        [DBField("HS_ID")]
        public string HsId { get; set; }

        [DBField("SUPPLIER_MENTIONED_ITEM_NAME")]
        public string SupplierMentionedItemName { get; set; }

        

        public List<SupplierQuotationItem> SupplierQuotationItems { get; set; }

        public PrDetailsV2 PrDetail { get; set; }

        [DBField("QUOTATION_COUNT")]
        public int QuotationCount { get; set; }

        public int LastSupplierId { get; set; }
        public int QuotationItemId { get; set; }
        public int QuotationId { get; set; }
        public string Description { get; set; }

        public decimal QuotedPrice { get; set; }

        public decimal SubTotal { get; set; }
        public decimal Nbt { get; set; }
        public decimal vaT { get; set; }
        public decimal NetTotal { get; set; }
        public decimal TotalVaT { get; set; }
        public decimal TotalNbT { get; set; }
        public decimal RequestingQty { get; set; }
        public decimal RequestedTotalQty { get; set; }
        public decimal UnitPrice { get; set; }
        public int HasVat { get; set; }
        public int HasNbt { get; set; }
        public int NbtType { get; set; }
        public int TablationId { get; set; }
        public int SelectedSupplierID { get; set; }
        public string SupplierMentionedName { get; set; }
        public string IsSelectedTB  { get; set; }
        public string SpecComply { get; set; }

        //USed in TR Approval Sup Wise
        public decimal SubTotal_Sup { get; set; }
        public decimal Nbt_sup { get; set; }
        public decimal vat_sup { get; set; }
        public decimal NetTotal_sup { get; set; }

        [DBField("MILL")]
        public string Mill { get; set; }

        [DBField("IMP_CIF")]
        public decimal ImpCIF { get; set; }

        [DBField("CURRENCY_NAME")]
        public string CurrencyType { get; set; }

        [DBField("SUPPLIER_AGENT_NAME")]
        public string SupplierAgent { get; set; }

        [DBField("COUNTRY_NAME")]
        public string Country { get; set; }

        [DBField("IMP_BRAND")]
        public string ImpBrand { get; set; }

        [DBField("IMP_REMARK")]
        public string ImpRemark { get; set; }

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

        [DBField("TERM_NAME")]
        public string TermName { get; set; }

        [DBField("TERM")]
        public string Term { get; set; }

        [DBField("QUOTATION_REFERENCE_CODE")]
        public string ReferenceCode { get; set; }

        [DBField("SPARE_PART_NUMBER")]
        public string SparePartNumber { get; set; }

        [DBField("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }

        [DBField("NO_OF_DAYS_PAYMENT_MODE")]
        public int PaymentModeDays { get; set; }
        
        public decimal UnitPriceLkr { get; set; }

        public decimal UnitPriceForeign { get; set; }
    }
}
