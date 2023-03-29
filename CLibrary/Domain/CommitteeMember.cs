using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{

    public class CommitteeMember
    {
        [DBField("ID")]
        public int Id { get; set; }

        [DBField("COMMITTEE_ID")]
        public int CommitteeId { get; set; }
        
        [DBField("DESIGNATION_ID")]
        public int DesignationId { get; set; }

        [DBField("SEQUENCE_OF_APPROVAL")]
        public int SequenceOfApproval { get; set; }

        [DBField("ALLOWED_APPROVAL_COUNT")]
        public int AllowedApprovalCount { get; set; }

        [DBField("CAN_OVERIDE")]
        public int CanOveride { get; set; }

        [DBField("OVERIDE_DESIGNATION_ID")]
        public int OverideDesignationId { get; set; }

        [DBField("EFFECTIVE_DATE")]
        public DateTime EffectiveDate { get; set; }

        [DBField("ENTERED_USER")]
        public int EnteredUser { get; set; }

        [DBField("ENTERED_DATE")]
        public DateTime EnteredDate { get; set; }
    }
}
