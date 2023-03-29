using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Helper;

namespace CLibrary.Domain {
    public class TRDIssueNote {

        [DBField("TRD_IN_ID")]
        public int TRDInId { get; set; }

        [DBField("TRD_ID")]
        public int TRDId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("ISSUED_QTY")]
        public decimal IssuedQTY { get; set; }

        [DBField("ISSUED_BY")]
        public int IssuedBy { get; set; }

        [DBField("ISSUED_ON")]
        public DateTime IssuedOn { get; set; }

        [DBField("DELIVERED_BY")]
        public int DeliveredBy { get; set; }

        [DBField("STATUS")]
        public int Status { get; set; }

        [DBField("ISSUED_STOCK_VALUE")]
        public decimal IssuedStockValue { get; set; }

        [DBField("RECEIVED_ON")]
        public DateTime ReceivedOn { get; set; }

        [DBField("DELIVERED_ON")]
        public DateTime DeliveredOn { get; set; }

        [DBField("RECEIVED_BY")]
        public int ReceivedBy { get; set; }

        [DBField("TR_ID")]
        public int TRId { get; set; }

        [DBField("TR_CODE")]
        public string TrCode { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("LOCATION")]
        public string Location { get; set; }

        [DBField("SHORT_CODE")]
        public string measurementShortName { get; set; }

        [DBField("DELIVERED_USER")]
        public string DeliveredUser { get; set; }

        [DBField("RECEIVED_USER")]
        public string ReceivedUser { get; set; }

        [DBField("FROM_WAREHOUSE_ID")]
        public int FromWarehouseId { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }


        public int RequestedMeasurementId { get; set; }

        public List<IssuedInventoryBatches> IssuedBatches { get; set; }
        public List<TrdIssueNoteBatches> IssueNoteBatches { get; set; }

    }
}
