using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class TempBOM
    {
        [DBField("DEPARTMENT_ID")]
        public int DepartmentId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("SEQ_NO")]
        public int SeqNo { get; set; }

        [DBField("METERIAL")]
        public string Meterial { get; set; }

        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        [DBField("PR_ID")]
        public int PrId { get; set; }
    }
}
