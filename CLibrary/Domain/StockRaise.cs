using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    class StockRaise
    {
        private int subDepartmentID, itemID, raisedQty,raisedBy;
        DateTime raisedDate;
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
        [DBField("RAISED_QTY")]
        public int RaisedQty
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
    }
}
