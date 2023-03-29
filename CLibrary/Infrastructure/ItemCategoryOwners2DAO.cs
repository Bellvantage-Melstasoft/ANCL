using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface ItemCategoryOwners2DAO {
        int SaveItemCategoryOwners2(int id, int categoryId, string ownerType, List<int> UserIds, DateTime effectiveDate, int userId2, DateTime now, DBConnection dbConnection);
        List<ItemCategoryOwners2> FetchCurrentPOorCO(int CategoryId, DateTime effectiveDate, DBConnection dbConnection);
        List<ItemCategoryOwners2> GetCompanyOwnersandPurchaseOfficersbyCompanyId(int companyId, DBConnection dbConnection);
        ItemCategoryOwners2 FetchItemCategoryOwnerDetails(int categoryId, string type, int UserId, int CompanyId, DBConnection dbConnection);
        int UpdateCategoryOwner(int CategoryId, int UserId, DateTime effectiveDate, string ownerType, List<int> UserIds, int userId2, DateTime PrevEffectveDate, DBConnection dbConnection);
        List<ItemCategoryOwners2> FetchCompanyOwnerHistory(int CategoryId, string type,DateTime date, DBConnection dbConnection);
        List<ItemCategoryOwners2> FetchDates(int CategoryId, string type, DBConnection dbConnection);
        int DeleteCategoryOwners(int categoryId, string ownerType, int UserId, DateTime EffectiveDate, DBConnection dbConnection);
        List<ItemCategoryOwners2> FetchPurchaseOfficers(int CategoryId,DBConnection dbConnection);
        List<ItemCategoryOwners2> FetchCategoryOwners(int CategoryId,DBConnection dbConnection);
        ItemCategoryOwners2 FetchCOMaxDate(int CategoryId, DBConnection dbConnection);
        List<ItemCategoryOwners2> FetchCategoryOwnerNames(int CategoryId, DBConnection dbConnection);
        ItemCategoryOwners2 FetchCOMaxDatePo(int CategoryId, DBConnection dbConnection);
        List<ItemCategoryOwners2> FetchPurchaseOfficerNames(int CategoryId, DBConnection dbConnection);
    }

    public class ItemCategoryOwners2DAOImpl : ItemCategoryOwners2DAO {
        public int SaveItemCategoryOwners2(int id, int categoryId, string ownerType, List<int> UserIds, DateTime effectiveDate, int userId2, DateTime now, DBConnection dbConnection) {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) as cnt FROM ITEM_CATEGORY_OWNERS2" +
                                          " WHERE CATEGORY_ID =" + categoryId + " " +
                                          " AND OWNER_TYPE = '" + ownerType + "' " +
                                          " AND EFFECTIVE_DATE = '" + effectiveDate + "' AND IS_ACTIVE = 1 ";


            dbConnection.cmd.CommandText += "UPDATE ITEM_CATEGORY_OWNERS2 SET IS_ACTIVE = 0 WHERE CATEGORY_ID =" + categoryId + " AND OWNER_TYPE = '" + ownerType + "' AND USER_ID IN ("+string.Join(",", UserIds)+") ";
            decimal x = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString()) != 1) {
                for (int i = 0; i < UserIds.Count; i++) {

                    dbConnection.cmd.CommandText += "INSERT INTO [ITEM_CATEGORY_OWNERS2]([CATEGORY_ID],[OWNER_TYPE],[USER_ID],[EFFECTIVE_DATE],[CREATED_USER],[CREATED_DATE],[IS_ACTIVE]) VALUES(" + categoryId + ", '" + ownerType + "', " + UserIds[i] + ",'" + effectiveDate + "', " + userId2 + ", '" + LocalTime.Now + "', 1); \n";
                }
            }
            else {
                return -1;
            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteCategoryOwners(int categoryId, string ownerType, int UserId, DateTime EffectiveDate, DBConnection dbConnection) {
            dbConnection.cmd.CommandText = "UPDATE ITEM_CATEGORY_OWNERS2 SET IS_ACTIVE = 0 WHERE CATEGORY_ID = "+ categoryId + " AND OWNER_TYPE = '"+ ownerType + "' AND USER_ID = "+ UserId +"  AND EFFECTIVE_DATE = '"+ EffectiveDate + "' ";
         
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemCategoryOwners2> GetCompanyOwnersandPurchaseOfficersbyCompanyId(int companyId, DBConnection dbConnection) {
            List<ItemCategoryOwners2> CategoryOwners;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT t.CATEGORY_ID ,t.CATEGORY_NAME,t.OWNER_TYPE ,max(t.EFFECTIVE_DATE) AS EFFECTIVE_DATE FROM(SELECT DISTINCT ICO.CATEGORY_ID, IC.CATEGORY_NAME, ico.OWNER_TYPE, ICO.EFFECTIVE_DATE FROM ITEM_CATEGORY_OWNERS2 AS ICO " +
                                            "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME, COMPANY_ID FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = ICO.CATEGORY_ID WHERE IC.COMPANY_ID = "+ companyId + ") as t group by t.CATEGORY_ID, t.CATEGORY_NAME,t.OWNER_TYPE ";
            
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                CategoryOwners = dataAccessObject.ReadCollection<ItemCategoryOwners2>(dbConnection.dr);
            }

            for (int i = 0; i < CategoryOwners.Count; i++) {
                CategoryOwners[i].UserName = string.Join(", ",
           DAOFactory.CreateItemCategoryOwners2DAO().FetchCurrentPOorCO(CategoryOwners[i].CategoryId, CategoryOwners[i].EffectiveDate, dbConnection).Select(w => w.UserName));
         
            }

            return CategoryOwners;
        }



        public List<ItemCategoryOwners2> FetchCurrentPOorCO(int CategoryId, DateTime effectiveDate, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT* FROM ITEM_CATEGORY_OWNERS2 AS ICO "+
                                        "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = ICO.USER_ID "+
                                        "WHERE ICO.EFFECTIVE_DATE = (SELECT TOP 1 EFFECTIVE_DATE FROM ITEM_CATEGORY_OWNERS2 AS ICOD "+
                                        "WHERE CATEGORY_ID = "+CategoryId+" AND EFFECTIVE_DATE <= '"+ effectiveDate +"' ORDER BY EFFECTIVE_DATE DESC) "+
                                        "AND CATEGORY_ID = " + CategoryId + " AND IS_ACTIVE = 1";

           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public List<ItemCategoryOwners2> FetchCompanyOwnerHistory(int CategoryId, string type, DateTime date, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT* FROM ITEM_CATEGORY_OWNERS2 AS ICO " +
                                        "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = ICO.USER_ID " +
                                        "WHERE CATEGORY_ID = " + CategoryId + " AND OWNER_TYPE= '"+type+"' AND EFFECTIVE_DATE = '" + date.ToString("yyyy-MM-dd") + "' ORDER BY EFFECTIVE_DATE DESC ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public List<ItemCategoryOwners2> FetchDates(int CategoryId, string type, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT DISTINCT CATEGORY_ID, EFFECTIVE_DATE, OWNER_TYPE FROM ITEM_CATEGORY_OWNERS2 WHERE CATEGORY_ID = " + CategoryId + " AND OWNER_TYPE = '"+ type + "' ORDER BY EFFECTIVE_DATE DESC  ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public ItemCategoryOwners2 FetchItemCategoryOwnerDetails(int categoryId, string type, int UserId, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            
            dbConnection.cmd.CommandText = "SELECT * FROM ITEM_CATEGORY_OWNERS2 AS ICO "+
                                            "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = ICO.USER_ID "+
                                            "INNER JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = ICO.CATEGORY_ID "+
                                           "WHERE ICO.CATEGORY_ID = "+ categoryId + " AND OWNER_TYPE = '"+ type + "' AND ICO.USER_ID = "+ UserId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public int UpdateCategoryOwner(int CategoryId, int UserId, DateTime effectiveDate, string ownerType, List<int> UserIds, int userId2, DateTime PrevEffectveDate, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            // dbConnection.cmd.CommandText = "UPDATE ITEM_CATEGORY_OWNERS2 SET EFFECTIVE_DATE = '" + effectiveDate + "' WHERE CATEGORY_ID = " + CategoryId + " AND USER_ID = " + UserId + " AND "+ownerType+" ";
            dbConnection.cmd.CommandText = "UPDATE ITEM_CATEGORY_OWNERS2 SET IS_ACTIVE = 0 WHERE CATEGORY_ID = " + CategoryId + " AND USER_ID = " + UserId + " AND OWNER_TYPE = '" + ownerType + "' AND EFFECTIVE_DATE = '"+ PrevEffectveDate + "' ";

            if (decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString()) != 1) {
                for (int i = 0; i < UserIds.Count; i++) {
                    dbConnection.cmd.CommandText += "INSERT INTO [ITEM_CATEGORY_OWNERS2]([CATEGORY_ID],[OWNER_TYPE],[USER_ID],[EFFECTIVE_DATE],[CREATED_USER],[CREATED_DATE],[IS_ACTIVE]) VALUES(" + CategoryId + ", '" + ownerType + "', " + UserIds[i] + ",'" + effectiveDate + "', " + userId2 + ", '" + LocalTime.Now + "', 1); \n";
                }
            }
            else {
                return -1;
            }
            return dbConnection.cmd.ExecuteNonQuery();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemCategoryOwners2> FetchCategoryOwners( int CategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM ITEM_CATEGORY_OWNERS2 AS ICO " +
                                        "INNER JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = ICO.CATEGORY_ID " +
                                        "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = ICO.USER_ID " +
                                        "WHERE OWNER_TYPE = 'CO' AND ICO.CATEGORY_ID = " + CategoryId + "  ORDER BY ICO.EFFECTIVE_DATE DESC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public List<ItemCategoryOwners2> FetchPurchaseOfficers( int CategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT* FROM ITEM_CATEGORY_OWNERS2 AS ICO " +
                                        "INNER JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = ICO.CATEGORY_ID " +
                                        "INNER JOIN(SELECT USER_ID, USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = ICO.USER_ID " +
                                        "WHERE OWNER_TYPE = 'PO' AND ICO.CATEGORY_ID = " + CategoryId + "  ORDER BY ICO.EFFECTIVE_DATE DESC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public ItemCategoryOwners2 FetchCOMaxDate(int CategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  TOP(1) * from ITEM_CATEGORY_OWNERS2 WHERE IS_ACTIVE = 1 AND OWNER_TYPE = 'CO' AND CATEGORY_ID = " + CategoryId + " " +
                                             "AND EFFECTIVE_DATE <= '" + LocalTime.Now + "' " +
                                            "ORDER BY EFFECTIVE_DATE DESC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public List<ItemCategoryOwners2> FetchCategoryOwnerNames(int CategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            ItemCategoryOwners2 itemCategoryOwners2 = DAOFactory.CreateItemCategoryOwners2DAO().FetchCOMaxDate(CategoryId, dbConnection);
            DateTime effevtiveDate = itemCategoryOwners2.EffectiveDate;


            dbConnection.cmd.CommandText = "SELECT USER_NAME from ITEM_CATEGORY_OWNERS2 AS ICO " +
                                             "INNER JOIN(SELECT USER_NAME, USER_ID FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = ICO.USER_ID " +
                                             "WHERE IS_ACTIVE = 1 AND OWNER_TYPE = 'CO' AND CATEGORY_ID = "+ CategoryId + " AND EFFECTIVE_DATE = '"+effevtiveDate+"' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public ItemCategoryOwners2 FetchCOMaxDatePo(int CategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  TOP(1) * from ITEM_CATEGORY_OWNERS2 WHERE IS_ACTIVE = 1 AND OWNER_TYPE = 'PO' AND CATEGORY_ID = " + CategoryId + " " +
                                             "AND EFFECTIVE_DATE <= '" + LocalTime.Now + "' " +
                                            "ORDER BY EFFECTIVE_DATE DESC ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategoryOwners2>(dbConnection.dr);
            }
        }

        public List<ItemCategoryOwners2> FetchPurchaseOfficerNames(int CategoryId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            ItemCategoryOwners2 itemCategoryOwners2 = DAOFactory.CreateItemCategoryOwners2DAO().FetchCOMaxDatePo(CategoryId, dbConnection);
            DateTime effevtiveDate = itemCategoryOwners2.EffectiveDate;


            dbConnection.cmd.CommandText = "SELECT USER_NAME from ITEM_CATEGORY_OWNERS2 AS ICO " +
                                             "INNER JOIN(SELECT USER_NAME, USER_ID FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = ICO.USER_ID " +
                                             "WHERE IS_ACTIVE = 1 AND OWNER_TYPE = 'PO' AND CATEGORY_ID = " + CategoryId + " AND EFFECTIVE_DATE = '" + effevtiveDate + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryOwners2>(dbConnection.dr);
            }
        }
    }

}

