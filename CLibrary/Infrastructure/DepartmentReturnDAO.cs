using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface DepartmentReturnDAO {
        int InsertReturnDepartmentDetail(int MrdInId, int WarehouseId, int ItemId, decimal ReturnedQty, decimal ReturnStock, int ReturnBy, int StockMaitaiinType, DBConnection dBConnection);
        decimal SumReturedQty(int MrndInID, DBConnection dbConnection);
        List<DepartmetReturn> fetchReturnedStock(DBConnection dbConnection);
        int UpdateDepartmentReturnApproval(int UserId, int departmentReturnId, DBConnection dBConnection);
        List<DepartmetReturn> FetchApprovedStock(DBConnection dbConnection);
    }

    public class DepartmentReturnDAOImpl : DepartmentReturnDAO {

        public int InsertReturnDepartmentDetail( int MrdInId, int WarehouseId, int ItemId, decimal ReturnedQty, decimal ReturnStock,  int ReturnBy, int StockMaitaiinType, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            string sql = "DECLARE @IDs TABLE(ID INT) " +
                 "INSERT INTO DEPARTMENT_RETURN ([MRND_IN_ID], [WAREHOUSE_ID], [ITEM_ID], [RETURN_QTY], [RETURN_STOCK], [RETURN_ON], [RETURN_BY], [STOCK_MAINTAINING_TYPE]) " +
                 " OUTPUT INSERTED.DEPARTMENT_RETURN_ID INTO @IDs(ID) " +
                 "VALUES(" + MrdInId + ", " + WarehouseId + ", " + ItemId + ",  " + ReturnedQty + ", " + ReturnStock + " , '" + LocalTime.Now + "', " + ReturnBy + " , "+ StockMaitaiinType + ") "+
                "SELECT ID FROM @IDs ";

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = sql;
            return int.Parse(dBConnection.cmd.ExecuteScalar().ToString());
            
        }

        public decimal SumReturedQty(int MrndInID, DBConnection dbConnection) {
            decimal ReturnedQty = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COunt(*) FROM DEPARTMENT_RETURN WHERE MRND_IN_ID = " + MrndInID + " ";
            int Count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (Count > 0) {
                dbConnection.cmd.CommandText = "SELECT SUM(RETURN_QTY) FROM DEPARTMENT_RETURN WHERE MRND_IN_ID = " + MrndInID + " ";
            }
            ReturnedQty = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            return ReturnedQty;
        }


        public List<DepartmetReturn> fetchReturnedStock( DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM DEPARTMENT_RETURN AS DR " +
                                            "INNER JOIN (SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS ) AS AI ON AI.ITEM_ID = DR.ITEM_ID " +
                                            "LEFT JOIN (SELECT BATCH_ID, DEPARTMENT_RETURN_ID FROM DEPARTMENT_RETURN_BATCH ) AS DRB ON DRB.DEPARTMENT_RETURN_ID  = DR.DEPARTMENT_RETURN_ID " +
                                            "LEFT JOIN (SELECT BATCH_ID, BATCH_CODE FROM WAREHOUSE_INVENTORY_BATCHES) AS WIB ON WIB.BATCH_ID = DRB.BATCH_ID " +
                                            "LEFT JOIN (SELECT MRND_ID, MRND_IN_ID FROM MRND_ISSUE_NOTE) AS MIN ON MIN.MRND_IN_ID = DR.MRND_IN_ID " +
                                            "WHERE APPROVE_STATUS != 1 OR APPROVE_STATUS IS NULL ";
            
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DepartmetReturn>(dbConnection.dr);
            }
        }

        public List<DepartmetReturn> FetchApprovedStock(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM DEPARTMENT_RETURN AS DR " +
                                            "INNER JOIN (SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS ) AS AI ON AI.ITEM_ID = DR.ITEM_ID " +
                                            "LEFT JOIN (SELECT BATCH_ID, DEPARTMENT_RETURN_ID FROM DEPARTMENT_RETURN_BATCH ) AS DRB ON DRB.DEPARTMENT_RETURN_ID  = DR.DEPARTMENT_RETURN_ID " +
                                            "LEFT JOIN (SELECT BATCH_ID, BATCH_CODE FROM WAREHOUSE_INVENTORY_BATCHES) AS WIB ON WIB.BATCH_ID = DRB.BATCH_ID " +
                                            "LEFT JOIN (SELECT MRND_ID, MRND_IN_ID FROM MRND_ISSUE_NOTE) AS MIN ON MIN.MRND_IN_ID = DR.MRND_IN_ID " +
                                            "WHERE APPROVE_STATUS = 1";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<DepartmetReturn>(dbConnection.dr);
            }
        }

        public int UpdateDepartmentReturnApproval(int UserId, int departmentReturnId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            string sql = "UPDATE DEPARTMENT_RETURN SET APPROVED_BY = "+ UserId + ", APPROVED_ON = '"+LocalTime.Now+"', APPROVE_STATUS = 1 WHERE DEPARTMENT_RETURN_ID = "+ departmentReturnId + " ";

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = sql;
            return dBConnection.cmd.ExecuteNonQuery();

        }

    }
}
