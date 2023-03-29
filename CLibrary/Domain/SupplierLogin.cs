using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class SupplierLogin
    {
        private int supplierid;
        private string username;
        private string password;
        private int isActive;
        private string email;
        private int isApproved;
        private string supplierName;


        [DBField("SUPPLIER_NAME")]
        public string SupplierName
        {
            get { return supplierName; }
            set { supplierName = value; }
        }

        [DBField("IS_APPROVED")]
        public int IsApproved
        {
            get { return isApproved; }
            set { isApproved = value; }
        }

        [DBField("EMAIL_ADDRESS")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        [DBField("PASSWORD")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [DBField("USR_NAME")]
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        [DBField("SUPPLIER_ID")]
        public int Supplierid
        {
            get { return supplierid; }
            set { supplierid = value; }
        }
       
        public Supplier _Supplier { get; set; }
    }
}
