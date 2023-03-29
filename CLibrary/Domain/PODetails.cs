using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class PODetails
    {
        private int poId;
        private int quotationItemId;
        private decimal itemPrice;
        private decimal quantity;
        private decimal totalAmount;
        private decimal vatAmount;
        private decimal nbtAmount;
        private int itemId;
        private string itemName, measurementShortName;

        [DBField("ITEM_ID")]
        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }

        [DBField("NBT_AMOUNT")]
        public decimal NbtAmount
        {
            get { return nbtAmount; }
            set { nbtAmount = value; }
        }

        [DBField("VAT_AMOUNT")]
        public decimal VatAmount
        {
            get { return vatAmount; }
            set { vatAmount = value; }
        }
        [DBField("STATUS")]
        public int Status { get; set; }

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

        [DBField("TOTAL_AMOUNT")]
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        [DBField("QUANTITY")]
        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        [DBField("ITEM_PRICE")]
        public decimal ItemPrice
        {
            get { return itemPrice; }
            set { itemPrice = value; }
        }

        [DBField("QUOTATION_ITEM_ID")]
        public int QuotationItemId
        {
            get { return quotationItemId; }
            set { quotationItemId = value; }
        }

        [DBField("PO_ID")]
        public int PoId
        {
            get { return poId; }
            set { poId = value; }
        }
        public AddItem _AddItem { get; set; }
        [DBField("ITEM_NAME")]
        public string ItemName 
        {
            get { return itemName; }
            set { itemName = value; }
        }

        [DBField("PO_DETAILS_ID")]
        public int PodId { get; set; }

        [DBField("TERMINATION_REMARKS")]
        public string TerminationRemarks { get; set; }

        [DBField("TERMINATED_BY")]
        public int TerminatedBy { get; set; }

        [DBField("TERMINATED_BY_NAME")]
        public String TerminatedByName { get; set; }


        [DBField("TERMINATED_ON")]
        public DateTime TerminatedOn { get; set; }


        [DBField("IS_PO_RAISED")]
        public int IsPoRaised { get; set; }

        [DBField("IS_PO_APPROVED")]
        public int IsPoApproved { get; set; }

       [DBField("CUSTOMIZED_AMOUNT")]
       public decimal CustomizedAmount{ get; set; }

       [DBField("CUSTOMIZED_VAT")]
       public decimal CustomizedVat{get; set;}

       [DBField("CUSTOMIZED_NBT")]
       public decimal CustomizedNbt { get; set; }

       [DBField("CUSTOMIZED_TOTAL_AMOUNT")]
       public decimal CustomizedTotalAmount { get; set; }

       [DBField("CUSVAT_AMOUNT")]
       public decimal CusVatAmount { get; set; }

       [DBField("CUSNBT_AMOUNT")]
       public decimal CusNbtAmount { get; set; }

       [DBField("CUSTOTAL_AMOUNT")]
       public decimal CusTotalAmount { get; set; }

       [DBField("CUSTPERITEM_AMOUNT")]
       public decimal CustPerItemAmount { get; set; }

       [DBField("IS_CUSTOMIZED_AMOUNT")]
       public int IsCustomizedAmount { get; set; }

       [DBField("sumvatAmount")]
       public decimal sumvatAmount { get; set; }

       [DBField("sumnbtmount")]
       public decimal sumnbtmount { get; set; }

       [DBField("sumtotalAmount")]
       public decimal sumtotalAmount { get; set; }

       [DBField("BASED_PO")]
       public int BasedPo { get; set; }

       [DBField("CATEGORY_NAME")]
       public string CategoryName { get; set; }

       [DBField("SUB_CATEGORY_NAME")]
       public string SubCategoryName { get; set; }

       //-------Edit
       [DBField("RECEIVED_QTY")]
       public decimal ReceivedQty { get; set; }

       [DBField("DEPARTMENT_ID")]
       public int DepartmentId { get; set; }

       [DBField("ADD_TO_GRN_COUNT")]
       public int AddToGrnCount { get; set; }

       //----Edit
       [DBField("IS_PO_EDIT_MODE")]
       public int IsPOEditMode { get; set; }

        [DBField("TABULATION_ID")]
        public int TabulationId { get; set; }

        [DBField("TABULATION_DETAIL_ID")]
        public int TabulationDetailId { get; set; }

        [DBField("HAS_VAT")]
        public int HasVat { get; set; }


        [DBField("HAS_NBT")]
        public int HasNbt { get; set; }

        [DBField("NBT_CALCULATION_TYPE")]
        public int NbtCalculationType { get; set; }

        [DBField("WAITING_QTY")]
        public decimal WaitingQty { get; set; }

        
        [DBField("PENDING_QTY")]
        public decimal PendingQty { get; set; }

        [DBField("BASED_PR")]
        public int BasePr { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("MEASUREMENT_NAME")]
        public string MeasurementName { get; set; }
        

[DBField("SUPPLIER_MENTIONED_ITEM_NAME")]
        public string SupplierMentionedItemName { get; set; }

        // Used in View PO Approve
        public SupplierQuotationItem supplierQuotationItem { get; set; }

        //used in Generate GRN
        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }

        [DBField("DEPARTMENT_NAME")]
        public string Department_Name { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }


        [DBField("PO_CODE")]
        public string PoCode { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("TERM_NAME")]
        public string TermName { get; set; }

        [DBField("PO_PURCHASE_TYPE")]
        public int PoPurchaseType { get; set; }

        [DBField("SUPPLIER_AGENT_NAME")]
        public string SupplierAgentName { get; set; }

        [DBField("IMPORT_ITEM_TYPE")]
        public int ImportItemType { get; set; }

        [DBField("SPARE_PART_NUMBER")]
        public string SparePartNumber { get; set; }

        [DBField("UNIT_PRICE_LOCAL")]
        public decimal UnitPriceLocal { get; set; }

        [DBField("UNIT_PRICE_FOREIGN")]
        public decimal UnitPriceForeign { get; set; }

        public decimal VatAmountDisplay { get; set; }
    }
}
