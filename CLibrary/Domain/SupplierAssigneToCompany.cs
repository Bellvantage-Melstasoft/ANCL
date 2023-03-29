using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class SupplierAssignedToCompany
    {
        private int supplierId;
        private int companyId;
        private DateTime requestedDate;
        private int isApproved;
        private int supplierFollowing;
        private int isAgreedTerms;
        private string supplierName;
        private string address1;
        private string address2;
        private string email;
        private string officeContactNo;
        private string phoneNo;
        private string businessRegistrationNumber;
        private string vatRegistrationNumber;
        private int companyType;
        private string businessCatecory;
        private string supplierLogo;
        private int isRequestFromSupplier;
        private int isCreatedByAdmin;
        private int isActive;

        [DBField("TERM_CONDITIONS_AGREED")]
        public int IsAgreedTerms
        {
            get { return isAgreedTerms; }
            set { isAgreedTerms = value; }
        }


        [DBField("IS_REQUESTFROM_SUPPLIER")]
        public int IsRequestFromSupplier
        {
            get { return isRequestFromSupplier; }
            set { isRequestFromSupplier = value; }
        }
        [DBField("SUPPLIER_LOGO")]
        public string SupplierLogo
        {
            get { return supplierLogo; }
            set { supplierLogo = value; }
        }

        [DBField("BUSINESS_CATEGORY")]
        public string BusinessCatecory
        {
            get { return businessCatecory; }
            set { businessCatecory = value; }
        }

        [DBField("COMPNY_TYPE")]
        public int CompanyType
        {
            get { return companyType; }
            set { companyType = value; }
        }

        [DBField("VAT_REG_NO")]
        public string VatRegistrationNumber
        {
            get { return vatRegistrationNumber; }
            set { vatRegistrationNumber = value; }
        }

        [DBField("BUSINESS_REGISTRATION_NO")]
        public string BusinessRegistrationNumber
        {
            get { return businessRegistrationNumber; }
            set { businessRegistrationNumber = value; }
        }

        [DBField("MOBILE_NO")]
        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }

        [DBField("OFFICE_CONTACT_NO")]
        public string OfficeContactNo
        {
            get { return officeContactNo; }
            set { officeContactNo = value; }
        }

        [DBField("EMAIL")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DBField("ADDRESS02")]
        public string Address2
        {
            get { return address2; }
            set { address2 = value; }
        }


        [DBField("ADDRESS01")]
        public string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }


        [DBField("SUPPLIER_ID")]

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        public List<SuplierImageUpload> _SuplierImageUploadList { get; set; }
        public List<SupplierCategory> _SupplierCategory { get; set; }
        public List<SuplierImageUpload> SupplierComplainDocument { get; set; }


        public string mainItemCategory { get; set; }

        public int isAgentSupplier { get; set; }

        [DBField("SUPPLIER_FOLLOW")]
        public int SupplierFollowing
        {
            get { return supplierFollowing; }
            set { supplierFollowing = value; }
        }

        [DBField("IS_APPROVE")]
        public int IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }

        [DBField("REQUSETED_DATE")]
        public DateTime RequestedDate
        {
            get { return requestedDate; }
            set { requestedDate = value; }
        }

        [DBField("COMPANY_ID")]
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }



        public Supplier _Supplier { get; set; }

        //Edit 2018-05-31 Yashoman
        [DBField("DEPARTMENT_NAME")]
        public string DepartmentName { get; set; }

        [DBField("DEPARTMENT_ID")]
        public int DepartmentID { get; set; }

        [DBField("DEPARTMENT_IMAGE_PATH")]
        public string ImagePath { get; set; }


    }
}
