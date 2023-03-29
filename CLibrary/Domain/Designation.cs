using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class Designation
    {
        [DBField("DESIGNATION_ID")]
        public int DesignationId { get; set; }

        [DBField("DESIGNATION_NAME")]
        public string DesignationName { get; set; }

        [DBField("ENTERED_USER")]
        public string EnteredUser { get; set; }

        [DBField("ENTERED_DATE")]
        public DateTime EnteredDate { get; set; }
    }
}
