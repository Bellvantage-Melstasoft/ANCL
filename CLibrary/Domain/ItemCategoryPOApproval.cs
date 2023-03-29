using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class ItemCategoryPOApproval
    {
        [DBField("PO_ID")]
        public int Po_Id { get; set; }

        [DBField("PR_ID")]
        public int PRId { get; set; }

        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("DESIGNATION_ID")]
        public int DesignationId { get; set; }

        [DBField("SEQUENCE")]
        public int Sequence { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }

        [DBField("COUNT")]
        public int Count { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        [DBField("USER_ID")]
        public int UserId { get; set; }

        [DBField("ENTERED_USER")]
        public string  EnteredUser { get; set; }

        [DBField("ENTERED_DATE")]
        public DateTime EnteredDate { get; set; }

        [DBField("HAS_PENDING_APPROVAL")]
        public int HasPendingApproval { get; set; }

        [DBField("PA_DESIGNATIONID")]
        public int PADesignationId { get; set; }

        [DBField("NO_OF_APPROVAL")]
        public int NoOfApproval { get; set; }

    }


}
