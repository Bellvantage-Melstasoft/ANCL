using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;


namespace CLibrary.Domain {
    public class DepartmetReturn {

        [DBField("DEPARTMENT_RETURN_ID")]
        public int DepartmentReturnId { get; set; }

        [DBField("MRND_IN_ID")]
        public int MrndInId { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("RETURN_QTY")]
        public decimal ReturnQty { get; set; }

        [DBField("RETURN_STOCK")]
        public decimal ReturnStock { get; set; }

        [DBField("RETURN_ON")]
        public DateTime ReturnOn { get; set; }

        [DBField("RETURN_BY")]
        public int ReturnBy { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public decimal StockMaintainingType { get; set; }

        [DBField("BATCH_ID")]
        public int BatchId { get; set; }

        [DBField("BATCH_CODE")]
        public int BatchCode { get; set; }

        [DBField("MRND_ID")]
        public int MrndId { get; set; }
    }
}
