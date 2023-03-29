using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class ItemCategoryApprovalLimit
    {
        [DBField("APPROVAL_LIMIT_ID")]
        public int ApprovalLimitId { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("MINIMUM_AMOUNT")]
        public int MinimumAmount { get; set; }

        [DBField("MAXIMUM_AMOUNT")]
        public int MaximumAmount { get; set; }

        [DBField("ENTERED_USER")]
        public string CreatedBy { get; set; }

        [DBField("ENTERED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("LIMIT_FOR")]
        public int LimitFor { get; set; }

        [DBField("APPROVAL_TYPE")]
        public string ApprovalType { get; set; }

        [DBField("APPROVAL_COUNT")]
        public int ApprovalCount { get; set; }

        [DBField("CAN_OVERIDE")]
        public int CanOveride { get; set; }

        [DBField("OVERIDE_USER")]
        public int OverideUser { get; set; }

        public List<CommitteeMember> AssignedDesignation { get; set; }
    } 
    
    
}
