using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace CLibrary.Domain
{
    public class HScode
    {
        [DBField("HS_ID")]
        public string HsId { get; set; }

        [DBField("HS_NAME")]
        public string HsName { get; set; }
    }
}
