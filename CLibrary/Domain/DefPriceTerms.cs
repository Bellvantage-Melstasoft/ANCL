using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class DefPriceTerms {

        [DBField("TERM_ID")]
        public int TermId { get; set; }

        [DBField("TERM_NAME")]
        public string TermName { get; set; }
    }
}
