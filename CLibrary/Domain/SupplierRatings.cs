using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    public class SupplierRatings
    {
        private int supplierId;
        private int rating;
        private string username;
        private string password;
        private string createdBy;
        private DateTime createdDate;
        private int companyid;
        private int isActive;
        private int isbalcklist;
        private DateTime updatedDate;
        private string updatedBy;
        private string remarks;

        [DBField("REMARKS")]
        public string Remarks
        {
            get { return remarks; }
            set { remarks = value; }
        }


        [DBField("UPDATED_BY")]
        public string UpdatedBy
        {
            get { return updatedBy; }
            set { updatedBy = value; }
        }

        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate
        {
            get { return updatedDate; }
            set { updatedDate = value; }
        }



        [DBField("IS_BLACKLIST")]
        public int Isbalcklist
        {
            get { return isbalcklist; }
            set { isbalcklist = value; }
        }

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DBField("COMPANY_ID")]
        public int Companyid
        {
            get { return companyid; }
            set { companyid = value; }
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

        [DBField("PASSWORD")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DBField("USERNAME")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DBField("RATING")]
        public int Rating
        {
            get { return rating; }
            set { rating = value; }
        }

        [DBField("SUPPLIER_ID")]
        public int SupplierId
        {
            get { return supplierId; }
            set { supplierId = value; }
        }

        public SupplierAssignedToCompany _SupplierAssigneToCompany { get; set; }
    }
}
