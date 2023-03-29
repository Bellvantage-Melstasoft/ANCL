using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class StockOverrideLog
    {

        [DBField("OVERRIDE_LOG_ID")]
        public int LogId { get; set; }

        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("WAREHOUSE_ID")]
        public int WarehouseId { get; set; }

        [DBField("EXISTED_QTY")]
        public decimal ExistedQty { get; set; }

        [DBField("EXISTED_STOCK_VALUE")]
        public decimal ExistedStockValue { get; set; }

        [DBField("OVERRIDING_QTY")]
        public decimal OverridingQty { get; set; }

        [DBField("OVERRIDING_STOCK_VALUE")]
        public decimal OverridingStockValue { get; set; }

        [DBField("OVERRIDDEN_BY")]
        public int OverriddenBy { get; set; }

        [DBField("OVERRIDDEN_ON")]
        public DateTime OverriddenOn { get; set; }

        [DBField("OVERRIDING_TYPE")]
        public int OverriddingType { get; set; }

        [DBField("PSVD_ID")]
        public int PSVDId { get; set; }

        [DBField("REMARKS")]
        public string Remark { get; set; }

        [DBField("LOCATION")]
        public string Location { get; set; }

        [DBField("FIRST_NAME")]
        public string FirstName { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("UPDATED_USER")]
        public string UpdatedUser { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }
    }
}
