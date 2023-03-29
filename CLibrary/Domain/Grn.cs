using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class Grn
    {
        private int grnId;
        private int poId;
        private int supplierid;
        private DateTime goodReceivedDate;
        private decimal  totalAmount;
        private string createdBy;
        private DateTime createdDate;
        private string grnNote;



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

    }
}
