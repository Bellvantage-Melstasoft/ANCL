using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain {
    public class GrndReturnNote {

        [DBField("GRND_IN_ID")]
        public int GrndInId { get; set; }

        [DBField("GRND_ID")]
        public int GrnId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("RETURNED_QTY")]
        public decimal ReturnedQty { get; set; }

        [DBField("RETURNED_STOCK_VALUE")]
        public decimal ReturnedStockValue { get; set; }

        [DBField("RETURNED_BY")]
        public int ReturnedBy { get; set; }

        [DBField("RETURNED_ON")]
        public DateTime ReturnedOn { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int Measurementid { get; set; }

        [DBField("REMARK")]
        public string Remark { get; set; }
    }
}
