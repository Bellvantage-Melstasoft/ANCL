
using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class POImportPaymentModeDef
    {
        [DBField("PAYMENT_MODE_ID")]
        public int PaymentModeId { get; set; }

        [DBField("NAME")]
        public string Name { get; set; }


    }
}
