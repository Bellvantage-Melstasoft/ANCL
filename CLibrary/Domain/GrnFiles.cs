using CLibrary.Common;
using System;

namespace CLibrary.Domain
{
    public class GrnFiles
    {

        [DBField("GRNF_ID")]
        public int GrnfId { get; set; }

        [DBField("GRN_ID")]
        public int GrnId { get; set; }

        [DBField("FILE_NAME")]
        public string FileName { get; set; }

        [DBField("LOCATION")]
        public string Location { get; set; }
    }
}