using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    [Serializable]
    public class VAT_NBT
    {
        [DBField("EFFECTIVE_DATE")]
        public DateTime EffectiveDate { get; set; }

        [DBField("VAT_VALUE")]
        public decimal VatRate { get; set; }

        [DBField("NBT_VALUE_1")]
        public decimal NBTRate1 { get; set; }

        [DBField("NBT_VALUE_2")]
        public decimal NBTRate2 { get; set; }
    }
}
