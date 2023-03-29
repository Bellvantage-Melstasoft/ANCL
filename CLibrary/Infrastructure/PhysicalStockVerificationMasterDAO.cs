using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CLibrary.Infrastructure {
    public interface PhysicalStockVerificationMasterDAO {
       PhysicalstockVerificationMaster GetCreatedApprovedDetails(int warehouseId, DateTime date, DBConnection dbConnection);
        int SaveStock(PhysicalstockVerificationMaster psvm, DBConnection dbConnection);
        int ApproveStock(int psvmId, int companyId, int approvedBy, DateTime approvedDate, string remaks, DBConnection dbConnection);
        int RejectStock(int psvmId, int status, int approvedBy, DateTime date, string remark, DBConnection dbConnection);
        int UpdateStock(PhysicalstockVerificationMaster psvm, DBConnection dbConnection);

    }

        public class PhysicalStockVerificationMasterDAOSQLImpl : PhysicalStockVerificationMasterDAO {
        public PhysicalstockVerificationMaster GetCreatedApprovedDetails(int warehouseId, DateTime date, DBConnection dbConnection) {


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PSVM.*, CLC.CREATED_SIGNATURE, CLC.CREATED_USER_NAME, CLA.APPROVED_USER_NAME, CLA.APPROVED_USER_SIGNATURE FROM PHYSICAL_STOCK_VERIFICATION_MASTER AS PSVM " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME, DIGITAL_SIGNATURE AS CREATED_SIGNATURE FROM COMPANY_LOGIN) AS CLC ON PSVM.CREATED_BY = CLC.USER_ID " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_USER_NAME, DIGITAL_SIGNATURE AS APPROVED_USER_SIGNATURE FROM COMPANY_LOGIN) AS CLA ON PSVM.APPROVED_BY = CLA.USER_ID " +
                                            "WHERE WAREHOUSE_ID = "+ warehouseId + " AND   MONTH(PSVM.MONTH)=" + date.Month + " AND YEAR(PSVM.MONTH) =" + date.Year + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PhysicalstockVerificationMaster>(dbConnection.dr);
            }
        }


            public int SaveStock(PhysicalstockVerificationMaster psvm, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DECLARE @PSVMID TABLE(PSVM_ID INT) \n";
            dbConnection.cmd.CommandText += "INSERT INTO PHYSICAL_STOCK_VERIFICATION_MASTER (WAREHOUSE_ID, MONTH, CREATED_BY, CREATED_DATE) \n" +
                                             "OUTPUT INSERTED.PSVM_ID INTO @PSVMID VALUES (" + psvm.WarehouseId + ",EOMONTH('" + psvm.Month + "')," + psvm.CreatedBy + ",'" +LocalTime.Now+ "')  \n";
            for (int i = 0; i < psvm.PSVDetails.Count; i++) {
                dbConnection.cmd.CommandText += "INSERT INTO PHYSICAL_STOCK_VERIFICATION_DETAIL([PSVM_ID],ITEM_ID,[SYS_AVAILABLE_QTY],SYS_STOCK_VALUE,PHYSICAL_AVAILABLE_QTY,PHYSICAL_STOCK_VALUE,IS_MODIFIED,REMARKS) \n" +
                                                "VALUES((SELECT (PSVM_ID) FROM @PSVMID), "+ psvm.PSVDetails[i].ItemId + ", " + psvm.PSVDetails[i].SysAvailableQty + ", " + psvm.PSVDetails[i].SysStockValue + ", " + psvm.PSVDetails[i].PhysicalAvailableQty + ", " + psvm.PSVDetails[i].PhysicalstockValue + ", " + psvm.PSVDetails[i].IsModified + ",'" + psvm.PSVDetails[i].Remarks.ProcessString() + "' )\n";

            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ApproveStock(int psvmId, int companyId, int approvedBy, DateTime approvedDate, string remaks, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "[APPROVE_PHYSICAL_STOCK_VERIFICATION]";

            dbConnection.cmd.Parameters.AddWithValue("@PSVM_ID", psvmId);
            dbConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", companyId);
            dbConnection.cmd.Parameters.AddWithValue("@APPROVED_BY", approvedBy);
            dbConnection.cmd.Parameters.AddWithValue("@APPROVED_DATE", approvedDate);
            dbConnection.cmd.Parameters.AddWithValue("@APPROVAL_REMARKS", remaks);


            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
        
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int RejectStock(int psvmId, int status, int approvedBy, DateTime date, string remark, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE PHYSICAL_STOCK_VERIFICATION_MASTER SET APPROVAL_STATUS = " + status + " , APPROVED_BY = " + approvedBy + " , APPROVED_DATE = '" + date + "', APPROVAL_REMARKS = '" + remark + "'   WHERE PSVM_ID = " + psvmId + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int UpdateStock(PhysicalstockVerificationMaster psvm, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE PHYSICAL_STOCK_VERIFICATION_MASTER SET APPROVAL_STATUS = 0, APPROVED_BY = NULL, APPROVED_DATE= NULL, APPROVAL_REMARKS = NULL WHERE PSVM_ID = " + psvm.PSVMId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            for (int i = 0; i < psvm.PSVDetails.Count; i++) {
                dbConnection.cmd.CommandText += "UPDATE PHYSICAL_STOCK_VERIFICATION_DETAIL SET PHYSICAL_AVAILABLE_QTY =" + psvm.PSVDetails[i].PhysicalAvailableQty + " , PHYSICAL_STOCK_VALUE = " + psvm.PSVDetails[i].PhysicalstockValue + ", REMARKS ='" + psvm.PSVDetails[i].Remarks.ProcessString() + "', IS_MODIFIED = " + psvm.PSVDetails[i].IsModified + " WHERE PSVD_ID = " + psvm.PSVDetails[i].PSVDId + " \n";
            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

    }


}
