
using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class POImportChargesDefinition
    {
        [DBField("CHARGE_TYPE_ID")]
        public int ImportChargeDefId { get; set; }

        [DBField("NAME")]
        public string Name { get; set; }

        public int OrderId { get; set; }

        public string Currency { get; set; }

        public decimal ExchangeRate { get; set; }

        public string SLPA { get; set; }

        public string Clearing { get; set; }

        public string Others { get; set; }
    }
}
