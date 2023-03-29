using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
   public class SystemDivision
    {
        [DBField("DIVISION_ID")]
        public int systemDivisionId { get; set; }

        [DBField("DIVISION_NAME")]
        public string systemDivisionName { get; set; }

        [DBField("CREATED_DATE")]
        public DateTime CreatedDate { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATE")]
        public DateTime UpdatedDate { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }
    }
}
