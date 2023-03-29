using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {

    public class ImportsHistory {

        [DBField("HISTORY_ID")]
        public int HistoryId { get; set; }

        [DBField("HISTORY")]
        public string History { get; set; }
    }
}
