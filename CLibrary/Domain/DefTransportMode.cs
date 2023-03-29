using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class DefTransportMode {

        [DBField("MODE_ID")]
        public int ModeId { get; set; }

        [DBField("NAME")]
        public string Name { get; set; }

    }
}
