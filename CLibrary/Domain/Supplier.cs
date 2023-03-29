using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class Supplier
    {
        private int supplierId;
        private string supplierName;
        private string address1;
        private string address2;
        private string email;
        private string officeContactNo;
        private string phoneNo;
        private string requestedDate;
        private string businessRegistrationNumber;
        private string vatRegistrationNumber;
        private int companyType;
        private int businessCatecory;
        private string supplierLogo;
        private int isRequestFromSupplier;
        private int isCreatedByAdmin;
        private int isActive;
        private string natureOfBusiness;
        private int supplierType;

        [DBField("SUPPLIER_ID")]

        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        [DBField("SUPPLIER_NAME")]
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        [DBField("EMAIL")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DBField("ADDRESS01")]
        public string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }

        [DBField("ADDRESS02")]
        public string Address2
        {
            get { return address2; }
            set { address2 = value; }
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

        [DBField("SUPPLIER_LOGO")]
        public string SupplierLogo
        {
            get { return supplierLogo; }
            set { supplierLogo = value; }
        }

        [DBField("BUSINESS_CATEGORY")]
        public int BusinessCatecory
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

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DBField("IS_CREATEDBY_ADMIN")]
        public int IsCreatedByAdmin
        {
            get { return isCreatedByAdmin; }
            set { isCreatedByAdmin = value; }
        }

        [DBField("IS_REQUESTFROM_SUPPLIER")]
        public int IsRequestFromSupplier
        {
            get { return isRequestFromSupplier; }
            set { isRequestFromSupplier = value; }
        }
        
        [DBField("REQUESTED_DATE")]
        public string RequestedDate
        {
            get { return requestedDate; }
            set { requestedDate = value; }
        }

        [DBField("SUPPLIER_TYPE")]
        public int SupplierType
        {
            get { return supplierType; }
            set { supplierType = value; }
        }

        [DBField("USR_NAME")]
        public string Username { get; set; }

        public SupplierLogin _SupplierLogin { get; set; }
        public List<SuplierImageUpload> _SuplierImageUploadList { get; set; }
        public List<SupplierCategory> _SupplierCategory { get; set; }

        public int isAgentSupplier { get; set; }

       //------Edit SMS Notification Supplier Token
        [DBField("SUPPLIER_DEVICE_TOCKEN")]
        public string SupplierDeviceTocken { get; set; }

        [DBField("BUSINESS_CATEGORY_NAME")]
        public string NatureOfBusiness
        {
            get
            {
                return natureOfBusiness;
            }

            set
            {
                natureOfBusiness = value;
            }
        }

        public int InvitationStatus { get; set; }

        [DBField("IS_REGISTERED_SUPPLIER")]
        public int IsRegisteredSupplier { get; set; }

        [DBField("SUPPLIER_REGISTRATION_N0")]
        public string SupplierRegistration { get; set; }

    }
}
