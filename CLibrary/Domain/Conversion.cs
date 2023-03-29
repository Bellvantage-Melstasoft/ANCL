using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class Conversion
    {
        [DBField("FROM_ID")]
        public int FromId { get; set; }

        [DBField("TO_ID")]
        public int ToId { get; set; }

        [DBField("MULTIPLIER")]
        public decimal Multiplier { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("FROM_NAME")]
        public string FromName { get; set; }

        [DBField("TO_NAME")]
        public string ToName { get; set; }
    }
}
