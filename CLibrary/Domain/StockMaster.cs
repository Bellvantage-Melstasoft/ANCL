using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Domain
{
    class StockMaster
    {
        private int subDepartmentID, itemID, stock;

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

        [DBField("STOCK")]
        public int Stock
        {
            get
            {
                return stock;
            }

            set
            {
                stock = value;
            }
        }
        
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
    }
}
