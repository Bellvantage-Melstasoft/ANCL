using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class PrUpdateLog {
        [DBField("PR_ID")]
        public int PrId { get; set; }
        [DBField("UPDATED_BY")]
        public int UpdatedBy { get; set; }
        [DBField("UPDATED_BY_NAME")]
        public string UpdatedByName { get; set; }
        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate { get; set; }
        [DBField("UPDATE_REMARKS")]
        public string UpdateRemarks { get; set; }
    }
}
