using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class DutyRates {
        [DBField("HS_ID")]
        public string HsId { get; set; }

        [DBField("HS_ID_NAME")]
        public string HsIdName { get; set; }

        [DBField("Date")]
        public DateTime Date { get; set; }

        [DBField("XID")]
        public decimal XID { get; set; }

        [DBField("CID")]
        public decimal CID { get; set; }

        [DBField("PAL")]
        public decimal PAL { get; set; }

        [DBField("EIC")]
        public decimal EIC { get; set; }

    }
}
