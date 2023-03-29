using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class BidHistory
    {
        private int quotationNo;
        private int bidderId;
        private string biddedBy;
        private DateTime submittedDate;
        private decimal unitPrice;
        private decimal vatAmount;
        private decimal nbtAmount;
        private decimal totalAmount;

        [DBField("TOTAL_AMOUNT")]
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }

        [DBField("NBT_AMOUNT")]
        public decimal NbtAmount
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

        [DBField("PER_ITEM_PRICE")]
        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }

        [DBField("BID_SUBMITED_DATE")]
        public DateTime SubmittedDate
        {
            get { return submittedDate; }
            set { submittedDate = value; }
        }

        [DBField("BIDDED_BY")]
        public string BiddedBy
        {
            get { return biddedBy; }
            set { biddedBy = value; }
        }

        [DBField("BIDDER_ID")]
        public int BidderId
        {
            get { return bidderId; }
            set { bidderId = value; }
        }

        [DBField("QUOTATION_NO")]
        public int QuotationNo
        {
            get { return quotationNo; }
            set { quotationNo = value; }
        }

    }
}
