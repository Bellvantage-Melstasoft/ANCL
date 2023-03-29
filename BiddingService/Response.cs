using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Reflection;
using System.IO;

namespace BiddingService
{
    public class Response
    {
        public int ID { get; set; }
        public string Data { get; set; }
    }

    public class SupplierBids
    {
        private int quotationNo;
        private int itemId;
        private int prID;
        private int supplierId;
        private int perItemPrice;
        private int isSelected;
        private string remarks;
        private string itemName;
        private int total;
        private decimal itemQuantity;
        private decimal amount;
        private decimal expense;
        private decimal nbt;
        private decimal vat;
        private decimal totalPrice;
        private decimal subTotal;
        
        public int isVatInclude { get; set; }

        public string ItemDescription { get; set; }

        public int IsActive { get; set; }

        public string BidTermsAndConditions { get; set; }

        public decimal VatAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal NbtAmount { get; set; }

        public int IsStayedAsLaterBid { get; set; }

        public decimal CustomizeAmount { get; set; }

        public string BidOpeningId { get; set; }



        public decimal SubTotal
        {
            get { return subTotal; }
            set { subTotal = value; }
        }
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }
        public int IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }
        public int PerItemPrice
        {
            get { return perItemPrice; }
            set { perItemPrice = value; }
        }

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        public int PrID
        {
            get { return prID; }
            set { prID = value; }
        }

        public int ItemId
        {
            get { return itemId; }
            set { itemId = value; }
        }
        public int QuotationNo
        {
            get { return quotationNo; }
            set { quotationNo = value; }
        }
        public string ItemName
        {
            get { return itemName; }
            set { itemName = value; }
        }
        public int Total
        {
            get { return total; }
            set { total = value; }
        }
        public decimal ItemQuantity
        {
            get { return itemQuantity; }
            set { itemQuantity = value; }
        }

        public decimal Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public decimal Nbt
        {
            get { return nbt; }
            set { nbt = value; }
        }
        public decimal Vat
        {
            get { return vat; }
            set { vat = value; }
        }
        public decimal TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }


        public List<supplierBomSVC> _SupplierBidBomsSVC { get; set; }
        public List<SupplierBiddingFileUploadSVC> _SupplierBidFileAttachementsSVC { get; set; }
    }

    public class supplierBomSVC
    {

        public int SupplierId { get; set; }

        public int PrId { get; set; }

        public int ItemId { get; set; }

        public int SeqId { get; set; }

        public string Meterial { get; set; }

        public string Description { get; set; }

        public int IsActive { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public int Comply { get; set; }

        public string Remarks { get; set; }

    }

    public class SupplierBiddingFileUploadSVC
    {

        public int SupplierId { get; set; }

        public int QuotationId { get; set; }

        public int PrId { get; set; }

        public int ItemId { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

    }

    public class SupplierDetailsSVC
    {
        private int supplierId;
        private string supplierName;
        private string address1;
        private string address2;
        private string officeContactNo;
        private string phoneNo;
        private string businessRegistrationNumber;
        private string vatRegistrationNumber;
        private int companyType;
        private int businessCatecory;
        private string supplierLogo;
        
        public string SupplierLogo
        {
            get { return supplierLogo; }
            set { supplierLogo = value; }
        }

        public int BusinessCatecory
        {
            get { return businessCatecory; }
            set { businessCatecory = value; }
        }
        public int CompanyType
        {
            get { return companyType; }
            set { companyType = value; }
        }
        public string VatRegistrationNumber
        {
            get { return vatRegistrationNumber; }
            set { vatRegistrationNumber = value; }
        }
        public string BusinessRegistrationNumber
        {
            get { return businessRegistrationNumber; }
            set { businessRegistrationNumber = value; }
        }
      
        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }
        public string OfficeContactNo
        {
            get { return officeContactNo; }
            set { officeContactNo = value; }
        }
       
        public string Address2
        {
            get { return address2; }
            set { address2 = value; }
        }
        public string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }
        public string Username { get; set; }

        public List<SupplierCategorySVC> _SupplierCategory { get; set; }


        //------Edit SMS Notification Supplier Token
        [DBField("SUPPLIER_DEVICE_TOCKEN")]
        public string SupplierDeviceTocken { get; set; }

    }

    public class SupplierCategorySVC
    {
        private int supplierId;
        private int categoryId;
        private int isActive;

        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public int CategoryId
        {
            get { return categoryId; }
            set { categoryId = value; }
        }
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }  
    }

   
    public class SupplierAssignCompanySVC
    {
        public List<RequestcompanyList> _RequestcompanyList { get; set; }
        
    }
    public class RequestcompanyList
    {
        public int supplierId { get; set; }
        public int companyId { get; set; }
        public int isFollow { get; set; }
    }

    public class SaveSupplierLogo
    {
        public int supplierId { get; set; }
        public byte[] logo { get; set; }
        public string extension { get; set; }
    
    }

    public class SupplierDocuments
    {
        public int supplierId { get; set; }
        public List<SupplierDocument> docs { get; set; }
    }

    public class SupplierDocument
    {
        public byte[] doc { get; set; }
        public string type { get; set; }
    }

    public class SupplierBiddingDocuments
    {
        public int supplierId { get; set; }
        public int quotationId { get; set; }
        public int prId { get; set; }
        public int itemId { get; set; }
        public List<SupplierBiddingDocument> docs { get; set; }
    }

    public class SupplierBiddingDocument
    {
        public byte[] doc { get; set; }
        public string type { get; set; }
    }

    public class POItems
    {
        public POMaster PO { get; set; }

        public decimal TotalSubAmount { get; set; }
        public decimal TotalVat { get; set; }
        public decimal TotalNbt { get; set; }
        public decimal TotalAmount { get; set; }
    }
}



