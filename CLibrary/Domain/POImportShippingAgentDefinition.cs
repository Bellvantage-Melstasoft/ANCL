
using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Domain
{
   public class POImportShippingAgentDefinition
    {
        [DBField("IMPORT_REFDEFINITION_ID")]
        public int ImportRefDefId { get; set; }

        [DBField("NAME")]
        public string Name { get; set; }

        public int OrderId { get; set; }

    }
}
