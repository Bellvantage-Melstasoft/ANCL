using CLibrary.Common;
using System;


namespace CLibrary.Domain {
    public class physicalStockVerificationDetails {

        [DBField("PSVD_ID")]
        public int PSVDId { get; set; }

        [DBField("PSVM_ID")]
        public int PSVMId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("SYS_AVAILABLE_QTY")]
        public decimal SysAvailableQty { get; set; }

        [DBField("SYS_STOCK_VALUE")]
        public decimal SysStockValue { get; set; }

        [DBField("PHYSICAL_AVAILABLE_QTY")]
        public decimal PhysicalAvailableQty { get; set; }

        [DBField("PHYSICAL_STOCK_VALUE")]
        public decimal PhysicalstockValue { get; set; }

        [DBField("IS_MODIFIED")]
        public int IsModified { get; set; }

        [DBField("REMARKS")]
        public string Remarks { get; set; }

        [DBField("REFERENCE_NO")]
        public string ReferenceNo { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

    }
}
