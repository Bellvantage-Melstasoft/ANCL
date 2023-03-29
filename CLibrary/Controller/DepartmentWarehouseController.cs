using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using CLibrary.Infrastructure;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface DepartmentWarehouseController
    {
        List<DepartmentWarehouses> GetWarehouseNameDepartmentId(int subDepartmentId);
        List<DepartmentWarehouses> GetDepartmentNameByWarehouseId(int WarehouseId);
    }
    public class DepartmentWarehouseControllerImpl : DepartmentWarehouseController
    {
        public List<DepartmentWarehouses> GetWarehouseNameDepartmentId(int subDepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DepartmentWarehouseDAO DepartmentWarehouseDAO = DAOFactory.createDepartmentWarehouseDAO();
                return DepartmentWarehouseDAO.GetWarehouseNameDepartmentId(subDepartmentId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<DepartmentWarehouses> GetDepartmentNameByWarehouseId(int WarehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                DepartmentWarehouseDAO DepartmentWarehouseDAO = DAOFactory.createDepartmentWarehouseDAO();
                return DepartmentWarehouseDAO.GetDepartmentNameByWarehouseId(WarehouseId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
    }
}
