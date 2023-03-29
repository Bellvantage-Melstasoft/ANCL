using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    class StockRelease
    {
        private int subDepartmentID, itemID, releasedQty, releasedBy,releaseType;
        private Nullable<int> releasedSubDepartmentID;
        DateTime releasedDate;
        [DBField("SUB_DEPARTMENT_ID")]
        public int SubDepartmentID
        {
            get
            {
                return subDepartmentID;
            }

            set
            {
                subDepartmentID = value;
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
        public int ReleaseType
        {
            get
            {
                return releaseType;
            }

            set
            {
                releaseType = value;
            }
        }
        [DBField("RELEASED_SUB_DEPARTMENT_ID")]
        public int? ReleasedSubDepartmentID
        {
            get
            {
                return releasedSubDepartmentID;
            }

            set
            {
                releasedSubDepartmentID = value;
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
