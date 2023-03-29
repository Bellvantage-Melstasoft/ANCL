using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface DepartmentWarehouseDAO
    {
        List<DepartmentWarehouses> GetWarehouseNameDepartmentId(int DepartmentId, DBConnection dbConnection);
        List<DepartmentWarehouses> GetDepartmentNameByWarehouseId(int WarehouseId, DBConnection dbConnection);

    }
    public class DepartmentWarehouseDAOSQLImpl : DepartmentWarehouseDAO
    {
        public List<DepartmentWarehouses> GetWarehouseNameDepartmentId(int DepartmentId, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT W.WAREHOUSE_ID, WH.LOCATION FROM DEPARTMENT_WAREHOUSES AS W " +
                                            "INNER JOIN WAREHOUSE AS WH ON WH.WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                            "WHERE W.SUBDEPARTMENT_ID = " + DepartmentId + " AND IS_ACTIVE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DepartmentWarehouses>(dbConnection.dr);
            }
        }


        public List<DepartmentWarehouses> GetDepartmentNameByWarehouseId(int WarehouseId, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM DEPARTMENT_WAREHOUSES AS DW " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SD ON SD.SUB_DEPARTMENT_ID = DW.SUBDEPARTMENT_ID " +
                                            "WHERE WAREHOUSE_ID = " + WarehouseId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DepartmentWarehouses>(dbConnection.dr);
            }
        }


    }
}
