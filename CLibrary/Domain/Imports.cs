using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class Imports
    {
        [DBField("HS_CODE")]
        public string HsId { get; set; }

        [DBField("HS_NAME")]
        public string HsName { get; set; }

        [DBField("VAT")]
        public int VAT { get; set; }

        [DBField("PAL")]
        public int PAL { get; set; }

        [DBField("CESS")]
        public int CESS { get; set; }

        [DBField("CUSTOM_DUTY")]
        public int CUSTOM_DUTY { get; set; }

        [DBField("RATE")]
        public double RATE { get; set; }

        [DBField("EFFECTIVE_DATE")]
        public string EFFECTIVE_DATE { get; set; }

       


        public string hsId { get; set; }
        public string hsName { get; set; }
        public int vat { get; set; }
        public int cess { get; set; }
        public int customDuty { get; set; }
        public string effectiveDate { get; set; }
        public int pal { get; set; }
        public double rate { get; set; }


    }
}
