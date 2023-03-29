using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain {
    public class DepartmentReturnBatch {


        [DBField("DEPARTMENT_RETURN_BATCH_ID")]
        public int DepartmentReturnBatchId { get; set; }

        [DBField("DEPARTMENT_RETURN_ID")]
        public int DepartmetReturnId { get; set; }

        [DBField("BATCH_ID")]
        public int BatchId { get; set; }

        [DBField("RETURN_QTY")]
        public decimal ReturnQty { get; set; }

        [DBField("RETURN_STOCK")]
        public decimal ReturnStock { get; set; }

        [DBField("MRND_IN_ID")]
        public int MrndInId { get; set; }
    }
}
