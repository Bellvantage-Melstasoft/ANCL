using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using System.Data;

namespace CLibrary.Infrastructure
{
    public interface ItemCategoryDAO
    {

        int SaveItemCategory(int companyId, int categoryid, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int UpdateItemCategory(int companyId, int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int DeleteItemCategory(int companyId, int CategoryId, DBConnection dbConnection);
        List<ItemCategory> FetchItemCategoryList(int companyId, DBConnection dbConnection);
        List<ItemCategory> FetchItemCategoryById(int CategoryId, DBConnection dbConnection);
        ItemCategory FetchItemCategoryListByIdObj(int companyId, int CategoryId, DBConnection dbConnection);
        int UpdateItemCategoryStatus(int companyId, int CategoryId,int status, DBConnection dbConnection);
        DataTable FetchItemCategoryApprovalLimits(int categoryId, DBConnection dbConnection);
        DataTable FetchItemCategoryApprovalLimitsAuthority(int companyId, int approvalLimitId, DBConnection dbConnection);
        DataTable FetchItemCategoryApprovalLimitsImport(int categoryId, DBConnection dbConnection);

        ItemCategory FetchItemByItemId(int ItemId, int companyId, DBConnection dbConnection);

        int GetPurchasingOfficer(int CategoryId, DBConnection dbConnection);
        int GetApprovalLimits(int CategoryId, int LimitFor, decimal MaxValue, decimal MinValue, DBConnection dbConnection);
    }

    public class ItemCategoryDAOImpl : ItemCategoryDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveItemCategory(int companyId, int categoryid, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_CATEGORY\" WHERE \"COMPANY_ID\" = " + companyId + " AND \"CATEGORY_NAME\" = '" + CategoryName + "'";
            var countNameExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countNameExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_CATEGORY\" WHERE \"COMPANY_ID\" = " + companyId + " AND \"CATEGORY_ID\" = " + categoryid + " ";
                var countCategoryExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (countCategoryExist == 0)
                {
                    dbConnection.cmd.CommandText = "INSERT INTO  public.\"ITEM_CATEGORY\" (\"COMPANY_ID\",\"CATEGORY_ID\" , \"CATEGORY_NAME\" , \"CRAETED_DATE\" , \"CREATED_BY\", \"UPDATED_DATE\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES ( " + companyId + "," + categoryid + ",'" + CategoryName + "','" + CategorDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                return -1;
            }
        }

        public int UpdateItemCategory(int companyId, int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ITEM_CATEGORY\" WHERE \"CATEGORY_NAME\" = '" + CategoryName + "' AND \"CATEGORY_ID\" != " + CategoryId + "";
            var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"ITEM_CATEGORY\" SET \"CATEGORY_NAME\" = '" + CategoryName + "' , \"CRAETED_DATE\" = '" + CategorDate + "', \"CREATED_BY\" = '" + CreatedBy + "', \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"CATEGORY_ID\" = " + CategoryId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteItemCategory(int companyId, int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"ITEM_CATEGORY\" SET  \"IS_ACTIVE\" = 0 WHERE \"CATEGORY_ID\" = " + CategoryId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemCategory> FetchItemCategoryList(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_CATEGORY\" ORDER BY  public.\"ITEM_CATEGORY\".\"CATEGORY_NAME\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategory>(dbConnection.dr);
            }
        }

        public List<ItemCategory> FetchItemCategoryById(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_CATEGORY\"  WHERE CATEGORY_ID =" + CategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategory>(dbConnection.dr);
            }
        }

        public ItemCategory FetchItemCategoryListByIdObj(int companyId, int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"ITEM_CATEGORY\"  WHERE CATEGORY_ID =" + CategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategory>(dbConnection.dr);
            }
        }

        public int UpdateItemCategoryStatus(int companyId, int CategoryId, int status, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public DataTable FetchItemCategoryApprovalLimits(int categoryId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public DataTable FetchItemCategoryApprovalLimitsAuthority(int companyId, int approvalLimitId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public ItemCategory FetchItemByItemId(int ItemId, int companyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int GetPurchasingOfficer(int CategoryId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int GetApprovalLimits(int CategoryId, int LimitFor, decimal MaxValue, decimal MinValue, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public DataTable FetchItemCategoryApprovalLimitsImport(int categoryId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }
    }

    public class ItemCategoryDAOSQLImpl : ItemCategoryDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveItemCategory(int companyId, int categoryid, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM "+ dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID = " + companyId + " AND CATEGORY_NAME = '" + CategoryName + "'";
            var countNameExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countNameExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID = " + companyId + " AND CATEGORY_ID = " + categoryid + " ";
                var countCategoryExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (countCategoryExist == 0)
                {
                    dbConnection.cmd.CommandText = "INSERT INTO  " + dbLibrary + ".ITEM_CATEGORY (COMPANY_ID,CATEGORY_ID , CATEGORY_NAME , CRAETED_DATE , CREATED_BY, UPDATED_DATE, UPDATED_BY, IS_ACTIVE) VALUES ( " + companyId + "," + categoryid + ",'" + CategoryName + "','" + CategorDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -2;
                }
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT CATEGORY_ID FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID = " + companyId + " AND CATEGORY_NAME = '" + CategoryName + "'";
                return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
        }

        public int UpdateItemCategory(int companyId, int CategoryId, string CategoryName, DateTime CategorDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ITEM_CATEGORY WHERE CATEGORY_NAME = '" + CategoryName + "' AND CATEGORY_ID = " + CategoryId + "";
            var countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 1)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_CATEGORY "+
                                               " SET CATEGORY_NAME = '" + CategoryName + "' , " +  
                                               " UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', "+ 
                                               " IS_ACTIVE = " + IsActive + " "+
                                               " WHERE CATEGORY_ID = " + CategoryId + " AND COMPANY_ID = " + companyId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteItemCategory(int companyId, int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "Delete " + dbLibrary + ".ITEM_CATEGORY  WHERE CATEGORY_ID = " + CategoryId + " AND COMPANY_ID = "+companyId+"";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemCategory> FetchItemCategoryList(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID = "+companyId+" ORDER BY  " + dbLibrary + ".ITEM_CATEGORY.CATEGORY_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategory>(dbConnection.dr);
            }
        }

        public List<ItemCategory> FetchItemCategoryById(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_CATEGORY  WHERE CATEGORY_ID =" + CategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategory>(dbConnection.dr);
            }
        }

        public ItemCategory FetchItemCategoryListByIdObj(int companyId, int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_CATEGORY  WHERE CATEGORY_ID =" + CategoryId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategory>(dbConnection.dr);
            }
        }

        public int UpdateItemCategoryStatus(int companyId, int CategoryId, int status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_CATEGORY SET  IS_ACTIVE = "+status+" WHERE CATEGORY_ID = " + CategoryId + " AND COMPANY_ID = " + companyId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public DataTable FetchItemCategoryApprovalLimits(int categoryId, DBConnection dbConnection)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string query = "SELECT DISTINCT a.* , b.EFFECTIVE_DATE as effectiveDate , c.EFFECTIVE_FROM as effectiveFrom , c.DESIGNATION_ID , d.COMMITTEE_NAME as committeeName ,d.COMMITTEE_ID" +
                        " FROM " + dbLibrary + ".APPROVAL_LIMITS a " +
                        " left join " + dbLibrary + ".APPROVAL_LIMIT_COMMITTEE b on b.APPROVAL_LIMIT_ID = a.LIMIT_ID " +
                        " left join " + dbLibrary + ".APPROVAL_LIMIT_USER c on c.LIMIT_ID = a.LIMIT_ID " +
                        " left join " + dbLibrary + ".COMMITTEE d on d.COMMITTEE_ID = b.COMMITTEE_ID " +
                        " WHERE a.CATEGORY_ID =" + categoryId + " AND a.LIMIT_TYPE = 1 "  +
                        " Order by LIMIT_FOR, MINIMUM_AMOUNT";
            
            dbConnection.cmd.CommandText = query;
            var dataReader = dbConnection.cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
           
        }

        public DataTable FetchItemCategoryApprovalLimitsImport(int categoryId, DBConnection dbConnection) {
            System.Data.DataTable dt = new System.Data.DataTable();
            string query = "SELECT DISTINCT a.* , b.EFFECTIVE_DATE as effectiveDate , c.EFFECTIVE_FROM as effectiveFrom , c.DESIGNATION_ID , d.COMMITTEE_NAME as committeeName ,d.COMMITTEE_ID" +
                        " FROM " + dbLibrary + ".APPROVAL_LIMITS a " +
                        " left join " + dbLibrary + ".APPROVAL_LIMIT_COMMITTEE b on b.APPROVAL_LIMIT_ID = a.LIMIT_ID " +
                        " left join " + dbLibrary + ".APPROVAL_LIMIT_USER c on c.LIMIT_ID = a.LIMIT_ID " +
                        " left join " + dbLibrary + ".COMMITTEE d on d.COMMITTEE_ID = b.COMMITTEE_ID " +
                        " WHERE a.CATEGORY_ID =" + categoryId + " AND a.LIMIT_TYPE = 2 " +
                        " Order by LIMIT_FOR, MINIMUM_AMOUNT";

            dbConnection.cmd.CommandText = query;
            var dataReader = dbConnection.cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;

        }

        public DataTable FetchItemCategoryApprovalLimitsAuthority(int companyId, int approvalLimitId, DBConnection dbConnection)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            string query = "SELECT DISTINCT a.APPROVAL_TYPES_ID, d.DESIGNATION_NAME , a.SEQUENCE_OF_APPROVAL , a.COUNT , a.HAS_PENDING_APPROVAL , COALESCE(e.DESIGNATION_NAME,'0') as PA_DESIGNATION_NAME " +
                        " FROM " + dbLibrary + ".APPROVAL_TYPES a " +
                        " JOIN " + dbLibrary + ".APPROVAL_LIMITS b ON a.APPROVAL_LIMIT_ID = b.APPROVAL_LIMIT_ID " +
                        " JOIN " + dbLibrary + ".ITEM_CATEGORY c ON c.CATEGORY_ID = b.CATEGORY_ID" +
                        " LEFT JOIN " + dbLibrary + ".DESIGNATION d ON a.DESIGNATION_ID = d.DESIGNATION_ID" +
                        " LEFT JOIN " + dbLibrary + ".DESIGNATION e ON a.PA_DESIGNATIONID = e.DESIGNATION_ID" +
                        " WHERE a.APPROVAL_LIMIT_ID =" + approvalLimitId + " AND c.COMPANY_ID = " + companyId + "";

            dbConnection.cmd.CommandText = query;
            var dataReader = dbConnection.cmd.ExecuteReader();
            var dataTable = new DataTable();
            dataTable.Load(dataReader);
            return dataTable;
        }

        public ItemCategory FetchItemByItemId(int ItemId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS  WHERE COMPANY_ID =" + companyId +""  +
                                           " AND ITEM_ID = " + ItemId  + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ItemCategory>(dbConnection.dr);
            }
        }

        public int GetPurchasingOfficer(int CategoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  TOP 1 USER_ID FROM ITEM_CATEGORY_OWNERS WHERE CATEGORY_ID=" + CategoryId + " "+
                                           " AND OWNER_TYPE='PO' AND EFFECTIVE_DATE <= '" +  LocalTime.Now + "' ORDER BY EFFECTIVE_DATE DESC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            object o = dbConnection.cmd.ExecuteScalar();

            if (o == null)
                return 0;
            else
                return int.Parse(o.ToString());
        }

        public int GetApprovalLimits(int CategoryId, int LimitFor, decimal MaxValue, decimal MinValue, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            int val = 0;

            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM APPROVAL_LIMITS WHERE CATEGORY_ID = "+ CategoryId + " AND LIMIT_FOR = " + LimitFor + " AND MINIMUM_AMOUNT > " + MaxValue + " ";
           int val1 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (val1 > 0) {
                val = 1;
            }


            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM APPROVAL_LIMITS WHERE CATEGORY_ID = " + CategoryId + " AND LIMIT_FOR = " + LimitFor + " AND MAXIMUM_AMOUNT < "+MinValue+" ";
           int val2 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (val2 > 0) {
                val = 2;
            }

            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM APPROVAL_LIMITS WHERE CATEGORY_ID = " + CategoryId + " AND LIMIT_FOR = " + LimitFor + " AND MINIMUM_AMOUNT > " + MaxValue + " ";
            int val3 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (val3 > 0) {
                dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM APPROVAL_LIMITS WHERE CATEGORY_ID = " + CategoryId + " AND LIMIT_FOR = " + LimitFor + " AND MAXIMUM_AMOUNT < " + MinValue + " ";
                int val4 = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (val4 > 0) {
                    val = 3;
                }
            }

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return val;

           
        }


    }
}

