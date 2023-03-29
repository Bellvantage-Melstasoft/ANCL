using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface SubCategoryStoreKeeperDAO {
        int SaveStoreKeeper(int SubCategoryId, List<int> UserIds, DateTime effectiveDate, DBConnection dbConnection);
        List<SubCategoryStoreKeeper> FetchStoreKeeper(int SubCategoryId, DBConnection dbConnection);
        SubCategoryStoreKeeper FetchStoreKeeperDetails(int SubCategoryId, int userId, DBConnection dbConnection);
        int UpdateStoreKeeper(int SubCategoryId, int UserId, DateTime effectiveDate, DBConnection dbConnection);
        int DeleteStoreKeeper(int SubCategoryId, int userId, DateTime date, DBConnection dbConnection);
        List<SubCategoryStoreKeeper> FetchCurrentStoreKeeper(int SubCategoryId, DBConnection dbConnection);


    }

    public class SubCategoryStoreKeeperDAOSQLImpl : SubCategoryStoreKeeperDAO {

        public int SaveStoreKeeper(int SubCategoryId, List<int> UserIds, DateTime effectiveDate, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            for (int i = 0; i < UserIds.Count; i++) {
                dbConnection.cmd.CommandText += "INSERT INTO SUB_CATEGORY_STORE_KEEPER([SUB_CATEGORY_ID],USER_ID,[EFFECTIVE_DATE]) VALUES(" + SubCategoryId + ", " + UserIds[i] + ",'" + effectiveDate + "' ); \n";

            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteStoreKeeper(int SubCategoryId, int userId, DateTime date, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText += "DELETE FROM SUB_CATEGORY_STORE_KEEPER WHERE SUB_CATEGORY_ID = " + SubCategoryId + "  AND USER_ID= " + userId + " AND EFFECTIVE_DATE = '"+date.ToString("yyyy-MM-dd")+ "'; \n";

            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int UpdateStoreKeeper(int SubCategoryId, int UserId, DateTime effectiveDate, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE SUB_CATEGORY_STORE_KEEPER SET EFFECTIVE_DATE = '" + effectiveDate + "' WHERE SUB_CATEGORY_ID = " + SubCategoryId + " AND USER_ID = " + UserId + " ";

            return dbConnection.cmd.ExecuteNonQuery();
        }


        public List<SubCategoryStoreKeeper> FetchStoreKeeper(int SubCategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT SCSK.*, CL.USER_NAME FROM SUB_CATEGORY_STORE_KEEPER AS SCSK " +
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = SCSK.USER_ID " +
                                            "WHERE SUB_CATEGORY_ID = " + SubCategoryId + " ORDER BY EFFECTIVE_DATE";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SubCategoryStoreKeeper>(dbConnection.dr);
            }
        }

        public SubCategoryStoreKeeper FetchStoreKeeperDetails(int SubCategoryId, int userId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUB_CATEGORY_STORE_KEEPER AS SCSK " +
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = SCSK.USER_ID " +
                                            "INNER JOIN (SELECT CATEGORY_ID, SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS ISC ON ISC.SUB_CATEGORY_ID = SCSK.SUB_CATEGORY_ID " +
                                            "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = ISC.CATEGORY_ID " +
                                            "WHERE SCSK.SUB_CATEGORY_ID = " + SubCategoryId + " AND SCSK.USER_ID = " + userId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SubCategoryStoreKeeper>(dbConnection.dr);
            }
        }


        public List<SubCategoryStoreKeeper> FetchCurrentStoreKeeper(int SubCategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUB_CATEGORY_STORE_KEEPER AS SCS " +
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = SCS.USER_ID " +
                                            " WHERE SUB_CATEGORY_ID = " + SubCategoryId + " AND  EFFECTIVE_DATE = (SELECT TOP 1 SCSK.EFFECTIVE_DATE FROM SUB_CATEGORY_STORE_KEEPER AS SCSK " +
                                             " WHERE SUB_CATEGORY_ID = "+ SubCategoryId + " AND EFFECTIVE_DATE <= '"+ LocalTime.Now +"' ORDER BY EFFECTIVE_DATE DESC) ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SubCategoryStoreKeeper>(dbConnection.dr);
            }
        }



    }
}
