using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {

    public class DefContainerSize {

        [DBField("ID")]
        public int Id { get; set; }

        [DBField("SIZE")]
        public string Size { get; set; }
    }
}
