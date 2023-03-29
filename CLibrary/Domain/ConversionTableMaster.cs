using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class ConversionTableMaster
    {
        [DBField("FROM_ID")]
        public int FromId { get; set; }

        [DBField("TO_ID")]
        public int ToId { get; set; }

        [DBField("MULTIPLIER")]
        public decimal Multiplier { get; set; }
    }
}
