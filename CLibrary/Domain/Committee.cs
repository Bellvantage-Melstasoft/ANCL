using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class Committee
    {
        [DBField("COMMITTEE_ID")]
        public int CommitteeId { get; set; }

        [DBField("COMMITTEE_NAME")]
        public string CommitteeName { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("CREATED_USER")]
        public int CreatedUser { get; set; }

        [DBField("COMMITTEE_TYPE")]
        public int CommitteeType { get; set; }
    }

    public class TecCommitteeFileUpload
    {
        [DBField("BID_ID")]
        public int BidId { get; set; }

        [DBField("TABULATION_ID")]
        public int tabulationId { get; set; }

        [DBField("SEQ_ID")]
        public int sequenceId { get; set; }

        [DBField("FILE_NAME")]
        public string filename { get; set; }

        [DBField("FILE_PATH")]
        public string filepath { get; set; }

        [DBField("COMMITTEE_TYPE")]
        public string commiteetype { get; set; }
    }
}
