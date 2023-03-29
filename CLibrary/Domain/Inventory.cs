using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Helper;

namespace CLibrary.Domain
{
    public class Inventory
    {
        private int warehouseID, itemID, holdedQty, lastUpdatedBy, isActive,  companyID;
        private string location, itemName;
        private decimal stockValue, issuedQty, availableQty;
        private DateTime lastUpdatedDate;

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
        public int HoldedQty
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

        [DBField("COMPANY_ID")]
        public int CompanyID
        {
            get
            {
                return companyID;
            }

            set
            {
                companyID = value;
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

        public decimal IssuedQty
        {
            get
            {
                return issuedQty;
            }

            set
            {
                issuedQty = value;
            }
        }

        public int FromWarehouseId { get; set; }

        [DBField("ISSUED_STOCK_VALUE")]
        public decimal IssuedStockValue { get; set; }

        [DBField("STOCK_MAINTAINING_TYPE")]
        public decimal StockMaintainingType { get; set; }

        public List<IssuedInventoryBatches> IssuedBatches { get; set; }
        public List<TrdIssueNoteBatches> TrdIssueNoteBatches { get; set; }

        [DBField("UNIT_PRICE")]
        public decimal UnitPrice { get; set; }

        [DBField("SHORT_CODE")]
        public string MeasurementShortName { get; set; }


    }
}
