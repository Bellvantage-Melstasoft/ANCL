using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class DefPaymentMode {
        [DBField("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }

        [DBField("PAYMENT_MODE")]
        public string PaymentMode { get; set; }

    }
}
