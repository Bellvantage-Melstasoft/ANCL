using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
   public interface RoleFunctionDAO
    {
        int SaveRoleFunction(int roleId, int sysDivisionid, int functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int UpdateRoleFunction(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int DeleteRoleFunction(int roleId, int functionId, DBConnection dbConnection);
        int DeleteRoleFunctionByRoleId(int roleId,DBConnection dbConnection);
        int DeleteSysDivisonsByRoleIdAndDivId(int roleId,int sysDivisionId, DBConnection dbConnection);
        List<RoleFunction> FetchRoleFunctionByRoleId(int roleId, DBConnection dbConnection);
        List<RoleFunction> FetchRoleFunctionByRoleIdAndDivId(int roleId, int sysDivisionid, DBConnection dbConnection);
        List<RoleFunction> FetchRoledevisionByRoleIdforgrid(int roleId, DBConnection dbConnection);
        List<RoleFunction> FetchRoleFunctionByRoleIdforgrid(int roleId,int catagory, DBConnection dbConnection);
        int DeleteRolewithdevisionFunction(int roleId, int sysDivisionid, int functionId, DBConnection dbConnection);
        List<RoleFunction> FetchAccessdevisionByUseridforgrid(int roleId, int userId, DBConnection dbConnection);
        List<RoleFunction> FetchAccessFunctionByUseridforgrid(int roleId, int catagory, int userId, DBConnection dbConnection);
    }
    public class RoleFunctionDAOImpl : RoleFunctionDAO
    {
        public int DeleteRoleFunction(int roleId, int functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"ROLE_FUNCTION\" SET  \"IS_ACTIVE\" = 0 WHERE \"ROLE_ID\" = " + roleId + " AND \"FUNCTION_ID\" = " + functionId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteRoleFunctionByRoleId(int roleId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"ROLE_FUNCTION\"  WHERE \"ROLE_ID\" = " + roleId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteSysDivisonsByRoleIdAndDivId(int roleId, int sysDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"ROLE_FUNCTION\"  WHERE \"ROLE_ID\" = " + roleId + " AND  \"SYSTEM_DIVISION_ID\" = " + sysDivisionId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<RoleFunction> FetchRoleFunctionByRoleId(int roleId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT rf.\"ROLE_ID\" ,rf.\"SYSTEM_DIVISION_ID\" ,rf.\"FUNCTION_ID\" ,rf.\"CREATED_DATE\" ,rf.\"CREATED_BY\" ,rf.\"UPDATED_DATE\" ,rf.\"UPDATED_BY\" ,rf.\"IS_ACTIVE\" , sd.\"DIVISION_NAME\", ur.\"ROLE_NAME\"   FROM public.\"ROLE_FUNCTION\" AS rf INNER JOIN public.\"SYSTEM_DIVISION\" AS sd ON rf.\"SYSTEM_DIVISION_ID\" = sd.\"DIVISION_ID\" INNER JOIN public.\"USER_ROLE\" AS ur ON rf.\"ROLE_ID\" = ur.\"ROLE_ID\" WHERE rf.\"ROLE_ID\"  = " + roleId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<RoleFunction>(dbConnection.dr);
            }
        }

        public List<RoleFunction> FetchRoleFunctionByRoleIdAndDivId(int roleId, int sysDivisionid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT rf.\"ROLE_ID\" ,rf.\"SYSTEM_DIVISION_ID\" ,rf.\"FUNCTION_ID\" ,rf.\"CREATED_DATE\" ,rf.\"CREATED_BY\" ,rf.\"UPDATED_DATE\" ,rf.\"UPDATED_BY\" ,rf.\"IS_ACTIVE\" , sd.\"DIVISION_NAME\", ur.\"ROLE_NAME\"   FROM public.\"ROLE_FUNCTION\" AS rf INNER JOIN public.\"SYSTEM_DIVISION\" AS sd ON rf.\"SYSTEM_DIVISION_ID\" = sd.\"DIVISION_ID\" INNER JOIN public.\"USER_ROLE\" AS ur ON rf.\"ROLE_ID\" = ur.\"ROLE_ID\" WHERE rf.\"ROLE_ID\" =" + roleId + " AND rf.\"SYSTEM_DIVISION_ID\" =" + sysDivisionid + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<RoleFunction>(dbConnection.dr);
            }
        }

        public int SaveRoleFunction(int roleId, int sysDivisionid, int functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ROLE_FUNCTION\" WHERE \"ROLE_ID\" = " + roleId + " AND \"SYSTEM_DIVISION_ID\" = " + sysDivisionid + "  AND \"FUNCTION_ID\" = " + functionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"ROLE_FUNCTION\" (\"ROLE_ID\" ,\"SYSTEM_DIVISION_ID\", \"FUNCTION_ID\" , \"CREATED_DATE\" , \"CREATED_BY\", \"UPDATED_DATE\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES (" + roleId + "," + sysDivisionid + "," + functionId + ",'" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateRoleFunction(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            if (oldFunctionId != newFunctionId)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"ROLE_FUNCTION\" WHERE \"ROLE_ID\" = '" + roleId + "' AND \"FUNCTION_ID\" != " + newFunctionId + "";
                var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (countExist == 0)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE public.\"ROLE_FUNCTION\" SET \"FUNCTION_ID\" = '" + newFunctionId + "' , \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"ROLE_ID\" = " + roleId + " ";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"ROLE_FUNCTION\" SET \"FUNCTION_ID\" = '" + newFunctionId + "' , \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"ROLE_ID\" = " + roleId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }

        }
        public List<RoleFunction> FetchRoledevisionByRoleIdforgrid(int roleId, DBConnection dbConnection)
        {
            return null;
        }
        public List<RoleFunction> FetchRoleFunctionByRoleIdforgrid(int roleId,int catagory,DBConnection dbConnection)
        { 
            return null; 
        
        }
        public int DeleteRolewithdevisionFunction(int roleId, int sysDivisionid, int functionId, DBConnection dbConnection)
        {
            return 0;
        }
        public List<RoleFunction> FetchAccessFunctionByUseridforgrid(int roleId, int catagory, int userId, DBConnection dbConnection)
        {
            return null; 
        }

        public List<RoleFunction> FetchAccessdevisionByUseridforgrid(int roleId, int userId, DBConnection dbConnection)
        {
            return null;
        }
    }

    public class RoleFunctionDAOSQLImpl : RoleFunctionDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int DeleteRoleFunction(int roleId, int functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ROLE_FUNCTION SET  IS_ACTIVE = 0 WHERE ROLE_ID = " + roleId + " AND FUNCTION_ID = " + functionId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteRoleFunctionByRoleId(int roleId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".ROLE_FUNCTION  WHERE ROLE_ID = " + roleId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteSysDivisonsByRoleIdAndDivId(int roleId, int sysDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".ROLE_FUNCTION  WHERE ROLE_ID = " + roleId + " AND  SYSTEM_DIVISION_ID = " + sysDivisionId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<RoleFunction> FetchRoleFunctionByRoleId(int roleId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT rf.ROLE_ID ,rf.SYSTEM_DIVISION_ID ,rf.FUNCTION_ID ,rf.CREATED_DATE ,rf.CREATED_BY ,rf.UPDATED_DATE ,rf.UPDATED_BY ,rf.IS_ACTIVE , sd.DIVISION_NAME, ur.ROLE_NAME   FROM " + dbLibrary + ".ROLE_FUNCTION AS rf INNER JOIN " + dbLibrary + ".SYSTEM_DIVISION AS sd ON rf.SYSTEM_DIVISION_ID = sd.DIVISION_ID INNER JOIN " + dbLibrary + ".USER_ROLE AS ur ON rf.ROLE_ID = ur.ROLE_ID WHERE rf.ROLE_ID  = " + roleId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<RoleFunction>(dbConnection.dr);
            }
        }

        public List<RoleFunction> FetchRoleFunctionByRoleIdAndDivId(int roleId, int sysDivisionid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT rf.ROLE_ID ,rf.SYSTEM_DIVISION_ID ,rf.FUNCTION_ID ,rf.CREATED_DATE ,rf.CREATED_BY ,rf.UPDATED_DATE ,rf.UPDATED_BY ,rf.IS_ACTIVE , sd.DIVISION_NAME, ur.ROLE_NAME   FROM " + dbLibrary + ".ROLE_FUNCTION AS rf INNER JOIN " + dbLibrary + ".SYSTEM_DIVISION AS sd ON rf.SYSTEM_DIVISION_ID = sd.DIVISION_ID INNER JOIN " + dbLibrary + ".USER_ROLE AS ur ON rf.ROLE_ID = ur.ROLE_ID WHERE rf.ROLE_ID =" + roleId + " AND rf.SYSTEM_DIVISION_ID =" + sysDivisionid + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<RoleFunction>(dbConnection.dr);
            }
        }
        public List<RoleFunction> FetchRoledevisionByRoleIdforgrid(int roleId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " With sysdivision AS(SELECT DIVISION_ID, DIVISION_NAME FROM " + dbLibrary + ".SYSTEM_DIVISION)," +
                                           " roles AS( SELECT DISTINCT SYSTEM_DIVISION_ID,ROLE_ID,IS_ACTIVE FROM " + dbLibrary + ".ROLE_FUNCTION)" +
                                           " SELECT r.*,s.* FROM roles r " +
                                           " RIGHT JOIN sysdivision s on  r.SYSTEM_DIVISION_ID=s.DIVISION_ID" +
                                           " AND r.ROLE_ID=" + roleId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<RoleFunction>(dbConnection.dr);
            }
        }

        public List<RoleFunction> FetchRoleFunctionByRoleIdforgrid(int roleId,int catagory, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " With sysfunction AS(SELECT FUNCTION_ID AS SYS_FUNCTION_ID,SYS_DIVISION_ID,FUNCTION_NAME FROM " + dbLibrary + ".SYSDIVISION_FUNCTION WHERE SYS_DIVISION_ID = " + catagory + ")," +
                                           " roles AS( SELECT * FROM " + dbLibrary + ".ROLE_FUNCTION where SYSTEM_DIVISION_ID =" + catagory + ")" +
                                           " SELECT r.*,s.* FROM roles r " +
                                           " RIGHT JOIN sysfunction s on r.FUNCTION_ID=s.SYS_FUNCTION_ID" +
                                           " AND r.SYSTEM_DIVISION_ID=s.SYS_DIVISION_ID AND r.ROLE_ID=" + roleId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<RoleFunction>(dbConnection.dr);
            }
        }

        public int SaveRoleFunction(int roleId, int sysDivisionid, int functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ROLE_FUNCTION WHERE ROLE_ID = " + roleId + " AND SYSTEM_DIVISION_ID = " + sysDivisionid + "  AND FUNCTION_ID = " + functionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".ROLE_FUNCTION (ROLE_ID ,SYSTEM_DIVISION_ID, FUNCTION_ID , CREATED_DATE , CREATED_BY, UPDATED_DATE, UPDATED_BY, IS_ACTIVE) VALUES (" + roleId + "," + sysDivisionid + "," + functionId + ",'" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateRoleFunction(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            if (oldFunctionId != newFunctionId)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".ROLE_FUNCTION WHERE ROLE_ID = '" + roleId + "' AND FUNCTION_ID != " + newFunctionId + "";
                var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (countExist == 0)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ROLE_FUNCTION SET FUNCTION_ID = '" + newFunctionId + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " WHERE ROLE_ID = " + roleId + " ";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ROLE_FUNCTION SET FUNCTION_ID = '" + newFunctionId + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " WHERE ROLE_ID = " + roleId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }

        }
        public int DeleteRolewithdevisionFunction(int roleId,int sysDivisionid, int functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".ROLE_FUNCTION WHERE ROLE_ID = " + roleId + " AND FUNCTION_ID = " + functionId + "AND SYSTEM_DIVISION_ID=" + sysDivisionid + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public List<RoleFunction> FetchAccessdevisionByUseridforgrid(int roleId,int userId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " With sysdivision AS(SELECT DIVISION_ID, DIVISION_NAME FROM " + dbLibrary + ".SYSTEM_DIVISION)," +
                                           " roles AS( SELECT DISTINCT SYSTEM_DIVISION_ID,USER_ROLE_ID,[USER_ID] FROM " + dbLibrary + ".COMPANY_USER_ACCESS)" +
                                           " SELECT r.*,s.* FROM roles r " +
                                           " RIGHT JOIN sysdivision s on  r.SYSTEM_DIVISION_ID=s.DIVISION_ID" +
                                           " AND r.USER_ROLE_ID="+roleId+" AND r.[USER_ID] ="+userId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<RoleFunction>(dbConnection.dr);
            }
        }

        public List<RoleFunction> FetchAccessFunctionByUseridforgrid(int roleId, int catagory,int userId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " With sysfunction AS(SELECT FUNCTION_ID AS SYS_FUNCTION_ID,SYS_DIVISION_ID,FUNCTION_NAME FROM " + dbLibrary + ".SYSDIVISION_FUNCTION WHERE SYS_DIVISION_ID = " + catagory + ")," +
                                           " roles AS( SELECT * FROM " + dbLibrary + ".COMPANY_USER_ACCESS where SYSTEM_DIVISION_ID =" + catagory + ")" +
                                           " SELECT r.*,s.* FROM roles r " +
                                           " RIGHT JOIN sysfunction s on r.ACTION_ID=s.SYS_FUNCTION_ID" +
                                           " AND r.SYSTEM_DIVISION_ID=s.SYS_DIVISION_ID AND r.USER_ROLE_ID=" + roleId + " and r.[USER_ID]= " + userId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<RoleFunction>(dbConnection.dr);
            }
        }
    }
}
