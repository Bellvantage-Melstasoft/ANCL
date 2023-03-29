using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class WarehouseInventoryBatches
    {
        private int batcId, batchCode, warehouseID, itemID, companyId, lastUpdatedBy, isActive;
        private DateTime expiryDate, lastUpdatedDate;
        private decimal availableQty, holdedQty, stockValue;

        [DBField("BATCH_ID")]
        public int BatchchId
        {
            get
            {
                return batcId;
            }

            set
            {
                batcId = value;
            }
        }

        [DBField("BATCH_CODE")]
        public int BatchCode
        {
            get
            {
                return batchCode;
            }

            set
            {
                batchCode = value;
            }
        }
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

        [DBField("COMPANY_ID")]
        public int CompanyId
        {
            get
            {
                return companyId;
            }

            set
            {
                companyId = value;
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

        [DBField("EXPIRY_DATE")]
        public DateTime ExpiryDate
        {
            get
            {
                return expiryDate;
            }

            set
            {
                expiryDate = value;
            }
        }

        [DBField("LAST_UPADATED_DATE")]
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

        [DBField("AVAILABLE_QTY")]
        public decimal AvailableStock
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

        public int addStatus { get; set; }
    }
}
