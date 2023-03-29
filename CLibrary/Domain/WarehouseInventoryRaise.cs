using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    public class WarehouseInventoryRaise
    {
        private int raiseID, warehouseID, itemID, raisedType, grndID, raisedWarehouseID, raisedBy;
        private decimal raisedQty;
        private DateTime raisedDate;
        private string description;
        private decimal stockValue;
        
        [DBField("INVENTORY_RAISE_ID")]
        public int RaiseID
        {
            get
            {
                return raiseID;
            }

            set
            {
                raiseID = value;
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

        [DBField("RAISED_QTY")]
        public decimal RaisedQty
        {
            get
            {
                return raisedQty;
            }

            set
            {
                raisedQty = value;
            }
        }

        [DBField("RAISED_TYPE")]
        public int RaisedType
        {
            get
            {
                return raisedType;
            }

            set
            {
                raisedType = value;
            }
        }

        [DBField("GRND_ID")]
        public int GrndID
        {
            get
            {
                return grndID;
            }

            set
            {
                grndID = value;
            }
        }

        [DBField("RAISED_WAREHOUSE_ID")]
        public int RaisedWarehouseID
        {
            get
            {
                return raisedWarehouseID;
            }

            set
            {
                raisedWarehouseID = value;
            }
        }

        [DBField("RAISED_BY")]
        public int RaisedBy
        {
            get
            {
                return raisedBy;
            }

            set
            {
                raisedBy = value;
            }
        }

        [DBField("RAISED_DATE")]
        public DateTime RaisedDate
        {
            get
            {
                return raisedDate;
            }

            set
            {
                raisedDate = value;
            }
        }

        [DBField("DESCRIPTION")]
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

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

        [DBField("STOCK_MAINTAINING_TYPE")]
        public int StockMaintainigType { get; set; }

        [DBField("COMPANY_ID")]
        public int CompanyId { get; set; }
    }
}
