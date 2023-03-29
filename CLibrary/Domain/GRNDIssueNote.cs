using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Domain
{
    public class GRNDIssueNote
    {
        private int grndInID, grndID, itemID, warehouseID, issuedQty, issuedBy;
        private decimal issuedStockValue;
        private DateTime issuedOn;

        [DBField("GRND_IN_ID")]
        public int GrndInID
        {
            get
            {
                return grndInID;
            }

            set
            {
                grndInID = value;
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

        [DBField("ISSUED_QTY")]
        public int IssuedQty
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

        [DBField("ISSUED_BY")]
        public int IssuedBy
        {
            get
            {
                return issuedBy;
            }

            set
            {
                issuedBy = value;
            }
        }

        [DBField("ISSUED_STOCK_VALUE")]
        public decimal IssuedStockValue
        {
            get
            {
                return issuedStockValue;
            }

            set
            {
                issuedStockValue = value;
            }
        }

        [DBField("ISSUED_ON")]
        public DateTime IssuedOn
        {
            get
            {
                return issuedOn;
            }

            set
            {
                issuedOn = value;
            }
        }
    }
}
