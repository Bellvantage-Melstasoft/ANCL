using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class ProcurementCommittee
    {
        [DBField("COMMITTEE_ID")]
        public int CommitteeId { get; set; }

        [DBField("COMMITTEE_NAME")]
        public string CommitteeName { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("CREATED_USER")]
        public int CreatedUser { get; set; }
    }
}
