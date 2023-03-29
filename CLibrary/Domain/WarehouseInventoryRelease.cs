using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;


namespace CLibrary.Domain
{
    public class WarehouseInventoryRelease
    {
        private int releaseID, warehouseID, itemID, releasedQty, releasedBy, releasedType, releasedMrndInID, releasedWarehouseID;
        private string description;
        private DateTime releasedDate;

        [DBField("INVENTORY_RELEASE_ID")]
        public int ReleaseID
        {
            get
            {
                return releaseID;
            }

            set
            {
                releaseID = value;
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

        [DBField("RELEASED_QTY")]
        public int ReleasedQty
        {
            get
            {
                return releasedQty;
            }

            set
            {
                releasedQty = value;
            }
        }

        [DBField("RELEASED_BY")]
        public int ReleasedBy
        {
            get
            {
                return releasedBy;
            }

            set
            {
                releasedBy = value;
            }
        }

        [DBField("RELEASED_TYPE")]
        public int ReleasedType
        {
            get
            {
                return releasedType;
            }

            set
            {
                releasedType = value;
            }
        }

        [DBField("RELEASED_MRND_IN_ID")]
        public int ReleasedMrndInID
        {
            get
            {
                return releasedMrndInID;
            }

            set
            {
                releasedMrndInID = value;
            }
        }

        [DBField("RELEASED_WAREHOUSE_ID")]
        public int ReleasedWarehouseID
        {
            get
            {
                return releasedWarehouseID;
            }

            set
            {
                releasedWarehouseID = value;
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

        [DBField("RELEASED_DATE")]
        public DateTime ReleasedDate
        {
            get
            {
                return releasedDate;
            }

            set
            {
                releasedDate = value;
            }
        }
    }
}
