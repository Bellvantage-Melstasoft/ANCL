
using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class CompanyDepartment
    {
        private int departmentID;
        private string departmentName;
        private DateTime createdDate;
        private string createdBy;
        private DateTime updatedDate;
        private string updatedBy;
        private int isActive;
        private string imagePath;
        private string address1;
        private string address2;
        private string city;
        private string country;
        private string phoneNO;
        private string mobileNo;
        private string faxNO;
        private string vatNo;


        [DBField("SUPPLIER_FOLLOW")]
        public int isSupplierFollow { get; set; }

        [DBField("IS_APPROVE")]
        public int isApproved { get; set; }

        [DBField("TERM_CONDITIONS_AGREED")]
        public int isTermsAgreed { get; set; }
        
        private string termConditionpath;

        [DBField("TERM_CONDITION_FILE_PATH")]
        public string TermConditionpath
        {
            get { return termConditionpath; }
            set { termConditionpath = value; }
        }

        [DBField("VAT_NO")]
        public string VatNo
        {
            get { return vatNo; }
            set { vatNo = value; }
        }

        [DBField("FAX_NO")]
        public string FaxNO
        {
            get { return faxNO; }
            set { faxNO = value; }
        }

        [DBField("MOBILE_NO")]
        public string MobileNo
        {
            get { return mobileNo; }
            set { mobileNo = value; }
        }

        [DBField("PHONE_NO")]
        public string PhoneNO
        {
            get { return phoneNO; }
            set { phoneNO = value; }
        }

        [DBField("COUNTRY")]
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        [DBField("CITY")]
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        [DBField("ADDRESS2")]
        public string Address2
        {
            get { return address2; }
            set { address2 = value; }
        }

        [DBField("ADDRESS1")]
        public string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }
        
        [DBField("DEPARTMENT_IMAGE_PATH")]
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; }
        }



        [DBField("UPDATED_BY")]
        public string UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }
        

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
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

        [DBField("DEPARTMENT_NAME")]
        public string DepartmentName
        {
            get { return departmentName; }
            set { departmentName = value; }
        }

        [DBField("DEPARTMENT_ID")]
        public int DepartmentID
        {
            get { return departmentID; }
            set { departmentID = value; }
        }

    }
}
