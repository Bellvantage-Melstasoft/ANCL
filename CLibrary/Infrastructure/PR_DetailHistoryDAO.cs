using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface PR_DetailHistoryDAO
    {
        int SavePRHistoryDetails(int PrId, int ItemId,string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, decimal ItemQuantity,  DBConnection dbConnection);
       
        List<PR_Details> FetchPRHistoryDetailsByDeptIdAndPrId(int PrId, DBConnection dbConnection);
        int DeletePrHistoryDetailByPrIDAndItemId(int prID, int ItemId, DBConnection dbConnection);
        int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity, DBConnection dbConnection);
    }

    public class PR_DetailHistoryDAOImpl : PR_DetailHistoryDAO
    {
        public int SavePRHistoryDetails(int PrId, int ItemId, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, decimal ItemQuantity, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"PR_DETAIL_HISTORY\" WHERE  \"PR_ID\" = " + PrId + " AND  \"ITEM_ID\" = " + ItemId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO public.\"PR_DETAIL_HISTORY\" (\"PR_ID\", \"ITEM_ID\",\"ITEM_UPDATED_BY\", \"ITEM_UPDATED_DATETIME\", \"IS_ACTIVE\",\"ITEM_QUANTITY\") VALUES ( " + PrId + ", " + ItemId + " , '" + ItemUpdatedBy + "', '" + ItemUpdatedDateTime + "', " + IsActive + "," + ItemQuantity + ");";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }


        }

        public List<PR_Details> FetchPRHistoryDetailsByDeptIdAndPrId(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL_HISTORY\"  WHERE public.\"PR_DETAIL_HISTORY\".\"IS_ACTIVE\"= 1  AND public.\"PR_DETAIL_HISTORY\".\"PR_ID\"=" + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public int DeletePrHistoryDetailByPrIDAndItemId(int prID, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE  public.\"PR_DETAIL_HISTORY\"  SET \"IS_ACTIVE\" = 0 WHERE \"PR_ID\" = " + prID + " AND \"ITEM_ID\" =" + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL_HISTORY\" SET  \"ITEM_QUANTITY\" = " + itemQuantity + "  WHERE \"PR_ID\" = " + prID + " AND \"ITEM_ID\" =" + ItemId + " AND \"IS_ACTIVE\" = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    public class PR_DetailHistoryDAOSQLImpl : PR_DetailHistoryDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SavePRHistoryDetails(int PrId, int ItemId, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, decimal ItemQuantity, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_DETAIL_HISTORY WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_DETAIL_HISTORY (PR_ID, ITEM_ID,ITEM_UPDATED_BY, ITEM_UPDATED_DATETIME, IS_ACTIVE,ITEM_QUANTITY) VALUES ( " + PrId + ", " + ItemId + " , '" + ItemUpdatedBy + "', '" + ItemUpdatedDateTime + "', " + IsActive + "," + ItemQuantity + ");";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }


        }

        public List<PR_Details> FetchPRHistoryDetailsByDeptIdAndPrId(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL_HISTORY  WHERE IS_ACTIVE= 1  AND PR_ID=" + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public int DeletePrHistoryDetailByPrIDAndItemId(int prID, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL_HISTORY  SET IS_ACTIVE = 0 WHERE PR_ID = " + prID + " AND ITEM_ID =" + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL_HISTORY SET  ITEM_QUANTITY = " + itemQuantity + "  WHERE PR_ID = " + prID + " AND ITEM_ID =" + ItemId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
