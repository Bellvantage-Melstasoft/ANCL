using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    public class WarehouseInventory
    {
        private int warehouseID, itemID, categoryID, subCategoryID, lastUpdatedBy, isActive;
        private string location, itemName, categoryName, subCategoryName;
        private DateTime lastUpdatedDate;
        private decimal availableQty, reorderLevel, holdedQty, unitPrice, stockValue;

        [DBField("WAREHOUSE_ID")]
        public int WarehouseID
        {
            get
            {
                return warehouseID;
            }

            set
            {
                warehouseID = value;
            }
        }

        [DBField("ITEM_ID")]
        public int ItemID
        {
            get
            {
                return itemID;
            }

            set
            {
                itemID = value;
            }
        }

        [DBField("CATEGORY_ID")]
        public int CategoryID
        {
            get
            {
                return categoryID;
            }

            set
            {
                categoryID = value;
            }
        }

        [DBField("SUB_CATEGORY_ID")]
        public int SubCategoryID
        {
            get
            {
                return subCategoryID;
            }

            set
            {
                subCategoryID = value;
            }
        }

        [DBField("MEASUREMENT_SHORT_NAME")]
        public string measurementShortName { get; set; }

        [DBField("AVAILABLE_QTY")]
        public decimal AvailableQty
        {
            get
            {
                return availableQty;
            }

            set
            {
                availableQty = value;
            }
        }

        [DBField("HOLDED_QTY")]
        public decimal HoldedQty
        {
            get
            {
                return holdedQty;
            }

            set
            {
                holdedQty = value;
            }
        }

        [DBField("LAST_UPDATED_BY")]
        public int LastUpdatedBy
        {
            get
            {
                return lastUpdatedBy;
            }

            set
            {
                lastUpdatedBy = value;
            }
        }

        [DBField("IS_ACTIVE")]
        public int IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }
        }
        [DBField("REORDER_LEVEL")]
        public decimal ReorderLevel
        {
            get
            {
                return reorderLevel;
            }

            set
            {
                reorderLevel = value;
            }
        }


        [DBField("LOCATION")]
        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
            }
        }

        [DBField("ITEM_NAME")]
        public string ItemName
        {
            get
            {
                return itemName;
            }

            set
            {
                itemName = value;
            }
        }

        [DBField("CATEGORY_NAME")]
        public string CategoryName
        {
            get
            {
                return categoryName;
            }

            set
            {
                categoryName = value;
            }
        }

        [DBField("SUB_CATEGORY_NAME")]
        public string SubCategoryName
        {
            get
            {
                return subCategoryName;
            }

            set
            {
                subCategoryName = value;
            }
        }

        [DBField("LAST_UPDATED_DATE")]
        public DateTime LastUpdatedDate
        {
            get
            {
                return lastUpdatedDate;
            }

            set
            {
                lastUpdatedDate = value;
            }
        }

        [DBField("UNIT_PRICE")]
        public decimal UnitPrice
        {
            get
            {
                return unitPrice;
            }

            set
            {
                unitPrice = value;
            }
        }
        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainingType { get; set; }

        [DBField("STOCK_VALUE")]
        public decimal StockValue
        {
            get
            {
                return stockValue;
            }

            set
            {
                stockValue = value;
            }
        }
    }
}
