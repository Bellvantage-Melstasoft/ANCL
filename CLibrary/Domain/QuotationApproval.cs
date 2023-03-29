using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class QuotationApproval
    {
        [DBField("APPROVAL_ID")]
        public int ApprovalId { get; set; }

        [DBField("QUOTATION_ID")]
        public int QuotationId { get; set; }

        [DBField("APPROVAL_BY_TYPE")]
        public int ApprovalByType { get; set; }

        [DBField("DESIGNATION_ID")]
        public int DesignationId { get; set; }

        [DBField("DESIGNATION_NAME")]
        public string DesignationName { get; set; }

        [DBField("SEQUENCE")]
        public int Sequence { get; set; }

        [DBField("CAN_OVERIDE")]
        public int CanOverride { get; set; }

        [DBField("OVERIDING_DESIGNATION")]
        public int OverridingDesignation { get; set; }

        [DBField("OVERIDING_DESIGNATION_NAME")]
        public int OverridingDesignationName { get; set; }

        [DBField("IS_APPROVED")]
        public int IsApproved { get; set; }

        [DBField("APPROVED_BY")]
        public int ApprovedBy { get; set; }

        [DBField("APPROVED_BY_NAME")]
        public string ApprovedByName { get; set; }

        [DBField("APPROVED_DATE")]
        public DateTime ApprovedDate { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        [DBField("WAS_OVERIDDEN")]
        public int WasOverriden { get; set; }

        public int CanLoggedInUserApprove { get; set; }

        public int CanLoggedInUserOverride { get; set; }


    }
}
