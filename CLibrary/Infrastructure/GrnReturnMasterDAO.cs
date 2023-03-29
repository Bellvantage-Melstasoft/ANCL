using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface GrnReturnMasterDAO {
        int SaveReturnMasterDetails(string Remark, int UserId, int WarehouseId, decimal SumSubTotal, decimal SumVatTotal, decimal SumNetTotal, int GrnId, int SupplierId, int SupplierreturnOption, DBConnection dBConnection);
        List<GrnReturnMaster> GetReturnedGRN(DBConnection dbConnection);
        GrnReturnMaster GetReturnedGrnDetails(int GrnReturnId, DBConnection dbConnection);
        int GetPOIdForReturnGRN(int GrnId, DBConnection dBConnection);
    }


    public class GrnReturnMasterDAOImpl : GrnReturnMasterDAO {
        public int SaveReturnMasterDetails(string Remark, int UserId, int WarehouseId, decimal SumSubTotal, decimal SumVatTotal, decimal SumNetTotal,int GrnId,int SupplierId, int SupplierreturnOption, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " INSERT INTO GRN_RETURN_MASTER VALUES(" + GrnId + ", " + WarehouseId + ", " + SupplierId + ", '" + Remark + "', " + UserId + ", '" + LocalTime.Now + "' , " + SumVatTotal + ", " + SumSubTotal + ", " + SumNetTotal + ", " + SupplierreturnOption + " )";
            dBConnection.cmd.CommandText += " UPDATE GRN_MASTER SET TOTAL_AMOUNT = TOTAL_AMOUNT-"+ SumNetTotal + ", VAT_TOTAL = VAT_TOTAL-" + SumVatTotal + " WHERE GRN_ID = "+ GrnId + "  ";
            

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnReturnMaster> GetReturnedGRN(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  * FROM GRN_RETURN_MASTER AS GM " +
                                            "LEFT JOIN ( SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER) AS SU ON SU.SUPPLIER_ID = GM.SUPPLIER_ID " +
                                            "LEFT JOIN ( SELECT USER_ID, FIRST_NAME AS RETURNED_USER_NAME FROM COMPANY_LOGIN) AS CCL ON CCL.USER_ID = GM.RETURNED_BY " +
                                            "LEFT JOIN ( SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON W.WAREHOUSE_ID = GM.WAREHOUSE_ID " +
                                            "LEFT JOIN ( SELECT GRN_ID, GRN_CODE FROM GRN_MASTER) AS GRN ON GRN.GRN_ID = GM.GRN_ID ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnReturnMaster>(dbConnection.dr);
            }
        }

        public GrnReturnMaster GetReturnedGrnDetails(int GrnReturnId,DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  * FROM GRN_RETURN_MASTER WHERE GRN_RETURN_ID = " + GrnReturnId + " ";
                                            
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<GrnReturnMaster>(dbConnection.dr);
            }
        }

        public int GetPOIdForReturnGRN( int GrnId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = " SELECT PO_ID FROM PO_GRN WHERE GRN_ID = "+ GrnId + " ";

            int PoId = int.Parse(dBConnection.cmd.ExecuteScalar().ToString());
            return PoId;
        }
    }
    }
