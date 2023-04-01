using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    [Serializable]

    public class GrnMaster
    {
        private int grnId;
        private int poId;
        private int supplierid;
        private DateTime goodReceivedDate;
        private decimal totalAmount;
        private string createdBy;
        private DateTime createdDate;
        private string grnNote;
        private int isApproved;
        private DateTime approvedDate;
        private string approvedBy;
        private int companyId;
        private decimal totalNbt;
        private decimal totalVat;
        private string quotationFor, approvalremaks;



        [DBField("GRN_IS_CONFIRMED_APPROVAL")]
        public int IsConfirmedApproval { get; set; }

        [DBField("DEPARTMENT_ID")]
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }


        [DBField("APPROVED_BY")]
        public string ApprovedBy
        {
            get { return approvedBy; }
            set { approvedBy = value; }
        }

        [DBField("APPROVED_DATE")]
        public DateTime ApprovedDate
        {
            get { return approvedDate; }
            set { approvedDate = value; }
        }

        [DBField("IS_APPROVED")]
        public int IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }



        [DBField("GRN_NOTE")]
        public string GrnNote
        {
            get { return grnNote; }
            set { grnNote = value; }
        }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate
        {
            get { return createdDate; }
            set { createdDate = value; }
        }

        [DBField("CREATED_BY")]
        public string CreatedBy
        {
            get { return createdBy; }
            set { createdBy = value; }
        }

        [DBField("TOTAL_AMOUNT")]
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        [DBField("GOOD_RECEIVED_DATE")]
        public DateTime GoodReceivedDate
        {
            get { return goodReceivedDate; }
            set { goodReceivedDate = value; }
        }

        [DBField("SUPPLIER_ID")]
        public int Supplierid
        {
            get { return supplierid; }
            set { supplierid = value; }
        }

        [DBField("PO_ID")]
        public int PoId
        {
            get { return poId; }
            set { poId = value; }
        }

        [DBField("GRN_ID")]
        public int GrnId
        {
            get { return grnId; }
            set { grnId = value; }
        }

        //Modified for GRN NEW
        [DBField("NBT_TOTAL")]
        public decimal TotalNbt
        {
            get { return totalNbt; }
            set { totalNbt = value; }
        }

        [DBField("QUOTATION_FOR")]
        public string QuotationFor
        {
            get { return quotationFor; }
            set { quotationFor = value; }
        }

        [DBField("VAT_TOTAL")]
        public decimal TotalVat
        {
            get { return totalVat; }
            set { totalVat = value; }
        }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("CREATED_USER_NAME")]
        public string CreatedByName { get; set; }

        //modified for GRN NEW
        [DBField("APPROVAL_REMARKS")]
        public string ApprovalRemaks
        {
            get { return approvalremaks; }
            set { approvalremaks = value; }
        }

        [DBField("APPROVED_USER_NAME")]
        public string ApprovedByName { get; set; }

        [DBField("APPROVED_SIGNATURE")]
        public string ApprovedSignature { get; set; }

        [DBField("CREATED_SIGNATURE")]
        public string CreatedSignature { get; set; }


        [DBField("GRN_CODE")]
        public string GrnCode { get; set; }


        [DBField("PO_CODE")]
        public string POCode { get; set; }

        [DBField("BASED_PR")]
        public int BasePr { get; set; }

        [DBField("PR_CODE")]
        public string PrCode { get; set; }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName { get; set; }

        [DBField("IS_GRN_RAISED")]
        public int IsGrnRaised { get; set; }

        [DBField("IS_GRN_APPROVED")]
        public int IsGrnApproved { get; set; }

        [DBField("REASON_FOR_REJECT")]
        public string ReasonForReject { get; set; }

        [DBField("INVOICE_NO")]
        public string InvoiceNo { get; set; }

        [DBField("DEPARTMENT_NAME")]
        public string subdepartment { get; set; }

        [DBField("QUOTATION_FOR")]
        public string Description { get; set; }

        [DBField("WAREHOUSE_NAME")]
        public string WarehouseName { get; set; }

        [DBField("CLONED_COVERING_PR")]
        public int ClonedCoveringPR { get; set; }

        public Supplier _Supplier { get; set; }
        public List<GrnDetails> _GrnDetailsList { get; set; }
        public CompanyDepartment _companyDepartment { get; set; }
        public POMaster _POMaster { get; set; }
        public PR_Master _PRMaster { get; set; }
        public List<int> PoIds { get; set; }
        public List<GrnDetails> GrnDetailsList { get; set; }
        public List<GrnFiles> UploadedFiles { get; set; }
        public Warehouse _Warehouse { get; set; }
    }
}
