
using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class POImportTransportModeDef
    {
        [DBField("IMPORT_TRANSPORT_MODE_ID")]
        public int Id { get; set; }

        [DBField("NAME")]
        public string Name { get; set; }
    }
}
