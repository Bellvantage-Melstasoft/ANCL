using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class GrnDetails
    {
        private int poId,grndID,issuedQty;
        private int quotationId;
        private decimal itemPrice;
        private decimal quantity, freeQty;
        private decimal totalAmount;
        private decimal vatAmount;
        private decimal nbtAmount;
        private int itemId;
        private int grnId;


        [DBField("GRN_ID")]
        public int GrnId
        {
            get { return grnId; }
            set { grnId = value; }
        }


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

        [DBField("QUOTATION_ID")]
        public int QuotationId
        {
            get { return quotationId; }
            set { quotationId = value; }
        }

        [DBField("PO_ID")]
        public int PoId
        {
            get { return poId; }
            set { poId = value; }
        }


       //--------2018-07-06
        [DBField("IS_GRN_RAISED")]
        public int IsGrnRaised { get; set; }

        [DBField("IS_GRN_APPROVED")]
        public int IsGrnApproved { get; set; }

        [DBField("GRN_APPROVED_BY")]
        public int GrnApprovedBy { get; set; }

        [DBField("GRN_APPROVED_DATE_TIME")]
        public DateTime GrnApprovedDateTime { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("ADD_TO_GRN_COUNT")]
        public int AddToGrnCount { get; set; }

        [DBField("GRN_CODE")]
        public string GrnCode { get; set; }

        public AddItem _AddItem { get; set; }

        [DBField("GRND_ID")]
        public int GrndID
        {
            get
            {
                return grndID;
            }

            set
            {
                grndID = value;
            }
        }

        [DBField("ISSUED_QTY")]
        public int IssuedQty
        {
            get
            {
                return issuedQty;
            }

            set
            {
                issuedQty = value;
            }
        }

        //modified for GRN NEW

        [DBField("FREE_QTY")]
        public decimal FreeQty
        {
            get
            {
                return freeQty;
            }

            set
            {
                freeQty = value;
            }
        }

        [DBField("EXPIRY_DATE")]
        public DateTime ExpiryDate { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("REFERENCE_NO")]
        public string ReferenceNo { get; set; }

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }

        public decimal WaitingQty { get; set; }
       // public int PodId { get; set; }

        [DBField("SUPPLIER_MENTIONED_ITEM_NAME")]
        public string SupplierMentionedItemName { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }

        [DBField("POD_ID")]
        public int PodId { get; set; }

        public int HasVat { get; set; }

        [DBField("AVAILABLE_MASTER_STOCK")]
        public decimal AvailableMasterStock { get; set; }

        [DBField("AVAILABLE_DETAIL_STOCK")]
        public decimal AvailableDetailStock { get; set; }

        public decimal VatAmountDisplay { get; set; }
        

    }
}
