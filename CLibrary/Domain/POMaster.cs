using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    public class POMaster
    {
        private int poID;
        private string poCode;
        private int departmentId;
        private int quotationId;
        private int supplierId;
        private DateTime createdDate;
        private string createdBy;
        private decimal vatAmount;
        private decimal nbtAmount;
        private string vatRegNo;
        private string sVatRegNo;
        private decimal totalAmount;
        private int isApproved;
        private string approvedBy;
        private int isReceived;
        private string receivedBy;
        private DateTime isApprovedDate;
        private string paymentMethod,quotationFor;


        [DBField("PO_IS_CONFIRMED_APPROVAL")]
        public int IsConfirmedApproval { get; set; }

        [DBField("PAYMENT_METHOD")]
        public string PaymentMethod
        {
            get { return paymentMethod; }
            set { paymentMethod = value; }
        }


        public DateTime IsApprovedDate
        {
            get { return isApprovedDate; }
            set { isApprovedDate = value; }
        }



        [DBField("IS_RECEIVED_DATE")]
        public string ReceivedBy
        {
            get { return receivedBy; }
            set { receivedBy = value; }
        }

        [DBField("IS_RECEIVED")]
        public int IsReceived
        {
            get { return isReceived; }
            set { isReceived = value; }
        }

        [DBField("APPROVED_BY")]
        public string ApprovedBy
        {
            get { return approvedBy; }
            set { approvedBy = value; }
        }

        [DBField("IS_APPROVED")]
        public int IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }

        [DBField("TOTAL_AMOUNT")]
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        [DBField("SVAT_REG_NO")]
        public string SVatRegNo
        {
            get { return sVatRegNo; }
            set { sVatRegNo = value; }
        }

        [DBField("VAT_REG_NO")]
        public string VatRegNo
        {
            get { return vatRegNo; }
            set { vatRegNo = value; }
        }

        [DBField("NBT_AMOUNT")]
        public decimal NBTAmount
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

        [DBField("CREATED_BY")]
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        [DBField("SUPPLIER_ID")]
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        [DBField("QUOTATION_ID")]
        public int QuotationId
        {
            get { return quotationId; }
            set { quotationId = value; }
        }

        [DBField("DEPARTMENT_ID")]
        public int DepartmentId
        {
            get { return departmentId; }
            set { departmentId = value; }
        }

        [DBField("PO_CODE")]
        public string POCode
        {
            get { return poCode; }
            set { poCode = value; }
        }

        [DBField("PO_ID")]
        public int PoID
        {
            get { return poID; }
            set { poID = value; }
        }

        //Edit 2018-06-24
        [DBField("BASED_PR")]
        public int BasePr { get; set; }

        [DBField("PR_CODE")]
        public string PrCode { get; set; }

        public List<PODetails> _PODetails { get; set; }
        public CompanyDepartment _companyDepartment { get; set; }
        public Supplier _Supplier { get; set; }
        public Warehouse _Warehouse { get; set; }

        [DBField("TOTAL_CUSTOMIZED_AMOUNT")]
        public decimal TotalCustomizedAmount { get; set; }

        [DBField("TOTAL_CUSTOMIZED_VAT")]
        public decimal TotalCustomizedVat { get; set; }

        [DBField("TOTAL_CUSTOMIZED_NBT")]
        public decimal TotalCustomizedNbt { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        [DBField("ITEMCOUNT")]
        public int ItemCount { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("DELIVER_TO_WAREHOUSE")]
        public int DeliverToWarehouse { get; set; }

        [DBField("QUOTATION_APPROVED_BY")]
        public int QuotationApprovedBy { get; set; }

        [DBField("QUOTATION_APPROVED_BY_NAME")]
        public string QuotationApprovedByName { get; set; }

        [DBField("QUOTATION_APPROVED_BY_SIGNATURE")]
        public string QuotationApprovedBySignature { get; set; }

        [DBField("QUOTATION_APPROVAL_DATE")]
        public DateTime QuotationApprovalDate { get; set; }


        [DBField("CLONED_FROM_PR")]
        public int ClonedFromPR { get; set; }

        [DBField("QUOTATION_CONFIRMED_BY")]
        public int QuotationConfirmedBy { get; set; }

        [DBField("QUOTATION_CONFIRMED_BY_SIGNATURE")]
        public string QuotationConfirmedBySignature { get; set; }

        [DBField("QUOTATION_CONFIRMED_BY_Name")]
        public string QuotationConfirmedByName { get; set; }

        [DBField("QUOTATION_CONFIRMATION_DATE")]
        public DateTime QuotationConfirmationDate { get; set; }

        [DBField("APPROVED_USER_NAME")]
        public string ApprovedByName { get; set; }

        [DBField("APPROVED_USER_SIGNATURE")]
        public string ApprovedBySignature { get; set; }

        [DBField("QUANTITY")]
        public decimal Quantity { get; set; }

        [DBField("APPROVED_DATE")]
        public DateTime ApprovedDate { get; set; }

        public List<PODetails> PoDetails { get; set; }

        [DBField("CREATED_USER_NAME")]
        public string CreatedByName { get; set; }

        [DBField("RECEIVED_QTY")]
        public int ReceivedQty { get; set; }

        [DBField("STORE_KEEPER_NAME")]
        public string StoreKeeperName { get; set; }

        [DBField("DEPARTMENT_NAME")]
        public string subdepartment { get; set; }

        [DBField("REQUIRED_FOR")]
        public string Description { get; set; }

        [DBField("WAREHOUSE_NAME")]
        public string WarehouseName { get; set; }

        [DBField("IS_CURRENT")]
        public int IsCurrent { get; set; }

        [DBField("WAS_DERIVED")]
        public int WasDerived { get; set; }

        [DBField("WAS_DERIVED_TYPE")]
        public int WasDerivedType { get; set; }

        [DBField("IS_DERIVED_TYPE")]
        public int IsDerivedType { get; set; }

        [DBField("PARENT_PO_CODE")]
        public string ParentPOCode { get; set; }

        [DBField("IS_DERIVED")]
        public int IsDerived { get; set; }

        [DBField("APPROVED_SIGNATURE")]
        public string ApprovedSignature { get; set; }

        [DBField("CREATED_SIGNATURE")]
        public string CreatedSignature { get; set; }

        [DBField("APPROVAL_REMARKS")]
        public string ApprovalRemarks { get; set; }

        [DBField("PARENT_APPROVED_USER")]
        public int ParentApprovedUser { get; set; }

        [DBField("PARENT_APPROVED_USER_NAME")]
        public string ParentApprovedByName { get; set; }

        [DBField("PARENT_APPROVED_USER_SIGNATURE")]
        public string ParentApprovedBySignature { get; set; }

        [DBField("IS_APPROVED_BY_PARENT_APPROVED_USER")]
        public int IsApprovedByParentApprovedUser { get; set; }

        [DBField("PARENT_APPROVED_USER_APPROVAL_DATE")]
        public DateTime ParentApprovedUserApprovalDate { get; set; }

        [DBField("PARENT_APPROVED_USER_APPROVAL_REMARKS")]
        public string ParentApprovedUserApprovalRemarks { get; set; }

        [DBField("IS_DERIVED_FROM_PO")]
        public int IsDerivedFromPo { get; set; }

        [DBField("DERIVING_REASON")]
        public string DerivingReason { get; set; }

        [DBField("REQUIRED_FOR")]
        public string QuotationFor { get; set; }

        [DBField("PRINT_COUNT")]
        public int PrintCount { get; set; }

        [DBField("TABULATION_ID")]
        public int TabulationId { get; set; }

        [DBField("PURCHASE_TYPE")]
        public int PurchaseType { get; set; }
        
        public List<POMaster> DerivedFromPOs { get; set; }
        public List<POMaster> DerivedPOs { get; set; }
        public List<GrnMaster> GeneratedGRNs { get; set; }
        

        [DBField("SUB_TOTAL")]
        public decimal SubTotal { get; set; }
        public Warehouse Warehouse { get; set; }

        [DBField("STORE_KEEPER")]
        public string StoreKeeper { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        [DBField("IS_CANCELLED")]
        public int IsCancelled { get; set; }

        [DBField("PO_EMAIL_STATUS")]
        public int PoEmailStatus { get; set; }

        [DBField("IMPORT_ITEM_TYPE")]
        public int ImportItemType { get; set; }
        

        [DBField("APPROVED_DESIGNATION_NAME")]
        public string ApprovedDesignationName { get; set; }

        [DBField("CREATED_DESIGNATION_NAME")]
        public string CreatedDesignationName { get; set; }

        [DBField("PARENT_APPROVED_DESIGNATION_NAME")]
        public string ParentApprovedDesignationName { get; set; }

        
        [DBField("PURCHASE_PROCEDURE")]
        public int PurchaseProcedure { get; set; }

        [DBField("ITEM_CATEGORY_ID")]
        public int ItemCategoryId { get; set; }

        [DBField("STORE_KEEPER_ID")]
        public string StoreKeepereId { get; set; }
    }


}
