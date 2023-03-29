using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class MrnBomV2 {
        [DBField("BOM_ID")]
        public int BomId { get; set; }
        [DBField("MRND_ID")]
        public int MrndId { get; set; }
        [DBField("MATERIAL")]
        public string Material { get; set; }
        [DBField("DESCRIPTION")]
        public string Description { get; set; }

        /// <summary>
        /// 0 = Do Nothing,
        /// 1 = Insert,
        /// 2 = Update,
        /// 3 = Delete
        /// </summary>
        public int Todo { get; set; }
    }
}
