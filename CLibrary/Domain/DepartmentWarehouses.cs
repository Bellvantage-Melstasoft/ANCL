using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Domain
{
    public class DepartmentWarehouses
    {
        private int subDepartmentId, warehouseId, createdBy;
        private string createdDate, warehouseName, departmentName;

        [DBField("SUBDEPARTMENT_ID")]
        public int SubDepartmentId
        {
            get
            {
                return subDepartmentId;
            }

            set
            {
                subDepartmentId = value;
            }
        }



        [DBField("WAREHOUSE_ID")]
        public int WarehouseId
        {
            get
            {
                return warehouseId;
            }

            set
            {
                warehouseId = value;
            }
        }
        [DBField("CREATED_BY")]
        public int CreatedBy
        {
            get
            {
                return createdBy;
            }

            set
            {
                createdBy = value;
            }
        }
        [DBField("CREATED_DATE")]
        public string CreatedDate
        {
            get
            {
                return createdDate;
            }

            set
            {
                createdDate = value;
            }
        }

        [DBField("DEPARTMENT_NAME")]
        public string DepartmentName
        {
            get
            {
                return departmentName;
            }

            set
            {
                departmentName = value;
            }
        }

        [DBField("Location")]
        public string WarehouseName
        {
            get
            {
                return warehouseName;
            }

            set
            {
                warehouseName = value;
            }
        }
    }
}
