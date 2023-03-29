using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
    [Serializable]
    public class SupplierBidEmailContact
    {
        private int prid;
        private string contactName, contactNo;
        private string email;
        private int userId;
        private string title;

        [DBField("PR_ID")]
        public int PRId
        {
            get { return prid; }
            set { prid = value; }
        }

        [DBField("USER_ID")]
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [DBField("Contact_Name")]
        public string ContactOfficer
        {
            get { return contactName; }
            set { contactName = value; }
        }

        [DBField("Contact_No")]
        public string ContactNo
        {
            get { return contactNo; }
            set { contactNo = value; }
        }

        [DBField("EMAIL")]
        public string Email
        {
            get { return email; }
            set { email = value; }

        }

        [DBField("TITLE")]
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }
}
