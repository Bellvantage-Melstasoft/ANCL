using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
    public class SupplierRating
    {
        private int supplierId;
        private decimal rating;
        private string username;
        private string password;
        private string createdBy;
        private DateTime createdDate;


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
        public decimal Rating
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

    }
}
