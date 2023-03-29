using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
  public interface UserRoleDAO
    {
        int SaveUserRole(string roleName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive,int CompanyId, DBConnection dbConnection);
        int UpdateUserRole(int roleId, string roleName,  DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int DeleteUserRole(int roleId, int CompanyId, DBConnection dbConnection);
        List<UserRole> FetchUserRole(int CompanyId, DBConnection dbConnection);
        UserRole FetchUserRoleObjByRoleId(int roleId,DBConnection dbConnection);
        List<UserRole> SearchUserRole(string text, int CompanyId, DBConnection dbConnection);
        int RestoreRole(int roleId, int CompanyId, DBConnection dbConnection);

    }

    //public class UserRoleDAOImp : UserRoleDAO
    //{
    //    public int DeleteUserRole(int roleId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "UPDATE public.\"USER_ROLE\" SET  \"IS_ACTIVE\" = 0 WHERE \"ROLE_ID\" = " + roleId + "";

    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public List<UserRole> FetchUserRole(int CompanyId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"USER_ROLE\"";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<UserRole>(dbConnection.dr);
    //        }
    //    }

    //    public UserRole FetchUserRoleObjByRoleId(int roleId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"USER_ROLE\" WHERE  \"ROLE_ID\" = " + roleId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.GetSingleOject<UserRole>(dbConnection.dr);
    //        }
    //    }

    //    public int SaveUserRole(string roleName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, int CompanyId, DBConnection dbConnection)
    //    {
    //        int UserRoleId = 0;

    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"USER_ROLE\" WHERE \"ROLE_NAME\" = '" + roleName + "'";
    //        var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

    //        if (countExist == 0)
    //        {
    //            dbConnection.cmd.Parameters.Clear();

    //            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"USER_ROLE\" ";
    //            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

    //            if (count == 0)
    //            {
    //                UserRoleId = 1;
    //            }
    //            else
    //            {
    //                dbConnection.cmd.CommandText = "SELECT MAX (\"ROLE_ID\")+1 AS MAXid FROM public.\"USER_ROLE\" ";
    //                UserRoleId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
    //            }
    //            dbConnection.cmd.CommandText = "INSERT INTO  public.\"USER_ROLE\" (\"ROLE_ID\" , \"ROLE_NAME\" , \"CREATED_DATE\" , \"CREATED_BY\", \"UPDATED_DATE\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES (" + UserRoleId + ",'" + roleName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
    //            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //            dbConnection.cmd.ExecuteNonQuery();
    //            return UserRoleId;
    //        }
    //        else
    //        {
    //            return -1;
    //        }
    //    }

    //    public int UpdateUserRole(int roleId, string roleName, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"USER_ROLE\" WHERE \"ROLE_NAME\" = '" + roleName + "' AND \"ROLE_ID\" != " + roleId + "";
    //        var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
    //        if (countExist == 0)
    //        {
    //            dbConnection.cmd.Parameters.Clear();
    //            dbConnection.cmd.CommandText = "UPDATE public.\"USER_ROLE\" SET \"ROLE_NAME\" = '" + roleName + "' , \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"ROLE_ID\" = " + roleId + " ";
    //            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //            return dbConnection.cmd.ExecuteNonQuery();
    //        }
    //        else
    //        {
    //            return -1;
    //        }
    //    }
    //}

    public class UserRoleDAOSQLImp : UserRoleDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int DeleteUserRole(int roleId,int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".USER_ROLE SET  IS_ACTIVE = 0 WHERE ROLE_ID = " + roleId + " AND COMPANY_ID = "+ CompanyId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<UserRole> FetchUserRole(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".USER_ROLE where COMPANY_ID=" + CompanyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserRole>(dbConnection.dr);
            }
        }

        public UserRole FetchUserRoleObjByRoleId(int roleId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".USER_ROLE WHERE  ROLE_ID = " + roleId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<UserRole>(dbConnection.dr);
            }
        }

        public int SaveUserRole(string roleName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, int CompanyId, DBConnection dbConnection)
        {
            int UserRoleId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".USER_ROLE WHERE ROLE_NAME = '" + roleName + "' AND COMPANY_ID=" + CompanyId;
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM  " + dbLibrary + ".USER_ROLE ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    UserRoleId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (ROLE_ID)+1 AS MAXid FROM " + dbLibrary + ".USER_ROLE ";
                    UserRoleId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".USER_ROLE (ROLE_ID , ROLE_NAME , CREATED_DATE , CREATED_BY, UPDATED_DATE, UPDATED_BY, IS_ACTIVE,COMPANY_ID) VALUES (" + UserRoleId + ",'" + roleName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ", " + CompanyId + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();
                return UserRoleId;
            }
            else
            {
                return -1;
            }
        }

        public int UpdateUserRole(int roleId, string roleName, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".USER_ROLE WHERE ROLE_NAME = '" + roleName + "' AND ROLE_ID != " + roleId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".USER_ROLE SET ROLE_NAME = '" + roleName + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " WHERE ROLE_ID = " + roleId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public List<UserRole> SearchUserRole(string text, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".USER_ROLE where  IS_ACTIVE = 1 AND ROLE_NAME LIKE '%" + text + "%' AND COMPANY_ID=" + CompanyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserRole>(dbConnection.dr);
            }
        }
        public int RestoreRole(int roleId, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".USER_ROLE SET  IS_ACTIVE = 1 WHERE ROLE_ID = " + roleId + " AND COMPANY_ID = " + CompanyId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
