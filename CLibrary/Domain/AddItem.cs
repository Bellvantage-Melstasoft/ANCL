using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class AddItem
    {
        [DBField("ITEM_ID")]
        public int ItemId { get; set; }

        [DBField("CATEGORY_ID")]
        public int CategoryId { get; set; }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryId { get; set; }

        [DBField("ITEM_NAME")]
        public string ItemName { get; set; }

        [DBField("HS_NAME")]
        public string HsName { get; set; }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string MeasurementShortName { get; set; }

        [DBField("IS_ACTIVE")]
        public int IsActive { get; set; }

        [DBField("CREATED_DATETIME")]
        public DateTime CreatedDateTime { get; set; }

        [DBField("CREATED_BY")]
        public string CreatedBy { get; set; }

        [DBField("UPDATED_DATETIME")]
        public DateTime UpdatedDateTime { get; set; }

        [DBField("UPDATED_BY")]
        public string UpdatedBy { get; set; }

        [DBField("CATEGORY_NAME")]
        public string CategoryName { get; set; }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName { get; set; }

        [DBField("IMAGE_PATH")]
        public string ImagePath { get; set; }

        [DBField("REFERENCE_NO")]
        public string ReferenceNo { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }

        [DBField("ITEM_TYPE")]
        public int ItemType { get; set; }

        [DBField("REORDER_LEVEL")]
        public int ReorderLevel { get; set; }

        [DBField("HS_ID")]
        public string HsId { get; set; }

        [DBField("MEASUREMENT_ID")]
        public int MeasurementId { get; set; }

        [DBField("MODEL")]
        public string Model { get; set; }

        [DBField("PART_ID")]
        public string PartId { get; set; }

        [DBField("CURENT_COMPANY_ID")]
        public string currentCompanyId { get; set; }

        [DBField("ITEM_MASTER_IMAGE_PATH")]
        public string ItemImagePath { get; set; }

        [DBField("ORDER_CODE")]
        public string OrderCode { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }

        public ItemCategory _ItemCategory { get; set; }
        public ItemSubCategory _ItemSubCategory { get; set; }
    }
}
