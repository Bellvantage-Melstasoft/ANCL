using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    [Serializable]
    public class Warehouse
    {
        private int warehouseID, isActive, companyID, headOfWarehouseID;
        private string location, phoneNo, headOfWarehouseName, address;

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


        [DBField("PHONE_NO")]
        public string PhoneNo
        {
            get
            {
                return phoneNo;
            }

            set
            {
                phoneNo = value;
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

        [DBField("HEAD_OF_WAREHOUSE")]
        public int HeadOfWarehouseID
        {
            get
            {
                return headOfWarehouseID;
            }

            set
            {
                headOfWarehouseID = value;
            }
        }

        [DBField("USER_NAME")]
        public string HeadOfWarehouseName
        {
            get
            {
                return headOfWarehouseName;
            }

            set
            {
                headOfWarehouseName = value;
            }
        }

        [DBField("ADDRESS")]
        public string Address
        {
            get
            {
                return address;
            }

            set
            {
                address = value;
            }
        }
    }
}
