using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class DefCurrencyType {

        [DBField("CURRENCY_TYPE_ID")]
        public int CurrencyTypeId { get; set; }

        [DBField("CURRENCY_NAME")]
        public string CurrencyName { get; set; }

        [DBField("CURRENCY_SHORT_NAME")]
        public string CurrencyShortName { get; set; }
    }
}
