using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface UserWarehouseDAO {
        List<UserWarehouse> getUserWarehousedetails(int UserId, DBConnection dbConnection);
        List<UserWarehouse> GetWarehouseHeadsByWarehouseId(int WarehouseId, DBConnection dbConnection);
        List<UserWarehouse> GetWarehouseKeeperForMRN(int WarehouseId, int subCategoryId, DBConnection dbConnection);
        List<UserWarehouse> GetWarehousesByUserId(int UserId, DBConnection dbConnection);
    }

    public class UserWarehouseDAOSQLImpl : UserWarehouseDAO {

        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<UserWarehouse> getUserWarehousedetails(int UserId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM "+ dbLibrary + ".USER_WAREHOUSE WHERE USER_ID =" + UserId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserWarehouse>(dbConnection.dr);
            }
        }

        public List<UserWarehouse> GetWarehouseHeadsByWarehouseId(int WarehouseId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT W.USER_ID, CL.FIRST_NAME FROM " + dbLibrary + ".USER_WAREHOUSE AS W " +
                                            "INNER JOIN " + dbLibrary + ".COMPANY_LOGIN AS CL ON CL.USER_ID = W.USER_ID " +
                                            "WHERE W.USER_TYPE = 1 AND W.WAREHOUSE_ID=" + WarehouseId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserWarehouse>(dbConnection.dr);
            }
        }

        public List<UserWarehouse> GetWarehouseKeeperForMRN(int WarehouseId, int subCategoryId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT CL.USER_ID,CL.FIRST_NAME,SK.SUB_CATEGORY_ID  FROM USER_WAREHOUSE AS UW " +
                                            " LEFT JOIN (SELECT TOP 1 * FROM SUB_CATEGORY_STORE_KEEPER WHERE SUB_CATEGORY_ID="+ subCategoryId + " AND EFFECTIVE_DATE <=GETDATE()) AS SK ON UW.USER_ID=SK.USER_ID " +
                                            " INNER JOIN COMPANY_LOGIN AS CL ON UW.USER_ID = CL.USER_ID WHERE UW.WAREHOUSE_ID = "+ WarehouseId +" ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserWarehouse>(dbConnection.dr);
            }
        }

        public List<UserWarehouse> GetWarehousesByUserId(int UserId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT* FROM USER_WAREHOUSE AS UW " +
                                            "INNER JOIN(SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE) AS W ON W.WAREHOUSE_ID = UW.WAREHOUSE_ID " +
                                            " WHERE USER_ID = " + UserId + " ";


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserWarehouse>(dbConnection.dr);
            }
        }

    }



}
