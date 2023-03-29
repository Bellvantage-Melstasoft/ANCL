using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface CompanyUserAccessDAO
    {
        int SaveCompanyUserAccess(int userId, int departmemtId,int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection);
        int UpdateCompanyUserAccess(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection);
        int DeleteCompanyUserAccessByDividionId(int userId, int departmemtId, DBConnection dbConnection);
        int DeleteAllCompanyUserAccessByDividionId(int userId, int departmemtId,int userRoleId, DBConnection dbConnection);

        int DeleteAllCompanyUserAccessByRoleIdAndDividionId(int userId, int departmemtId, int userRoleId,int sysDivisionId, DBConnection dbConnection);
        int DeleteCompanyUserAccessByUserId(int userId, int departmemtId, DBConnection dbConnection);
        int SaveCompanyUserAccessvusename(string username, int departmemtId, int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection);
        List<CompanyUserAccess> FetchCompanyUserAccessByUserId(int userId, int departmemtId, DBConnection dbConnection);
        List<CompanyUserAccess> GetCompanyUserAccessObjByUserId(int userId, int departmemtId, DBConnection dbConnection);
        bool isAvilableAccess(int userId, int departmemtId, int? systemDivisionId, int? functionId, DBConnection dbConnection);
        List<CompanyUserAccess> FetchCompanyUserAccessByDepartentId(int departmemtId, DBConnection dbConnection);
        int DeleteAllCompanyUserAccessByRoleIdAndFunctionId(int userId, int departmemtId, int userRoleId, int sysDivisionId, int functionId, DBConnection dbConnection);
        List<CompanyUserAccess> FetchCompanyUserAccessByUserIdAndUserRoleIdAndDivId(int userId, int departmemtId,int userRoleId, int sysDivId, DBConnection dbConnection);
        CompanyUserAccess FetchUserRoleIdByUserId(int userId, int companyId, DBConnection dbConnection);
    }
    public class CompanyUserAccessDAOImpl : CompanyUserAccessDAO
    {
        public int DeleteAllCompanyUserAccessByDividionId(int userId, int departmemtId, int userRoleId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"COMPANY_USER_ACCESS\" WHERE \"USER_ID\" = " + userId + " AND  \"DEPARTMENT_ID\"  =" + departmemtId + "  AND \"USER_ROLE_ID\" =" + userRoleId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int DeleteAllCompanyUserAccessByRoleIdAndDividionId(int userId, int departmemtId, int userRoleId, int sysDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"COMPANY_USER_ACCESS\" WHERE \"USER_ID\" = " + userId + " AND  \"DEPARTMENT_ID\"  =" + departmemtId + "  AND \"USER_ROLE_ID\" =" + userRoleId + " AND \"SYSTEM_DIVISION_ID\" = " + sysDivisionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int DeleteCompanyUserAccessByDividionId(int userId, int departmemtId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int DeleteCompanyUserAccessByUserId(int userId, int departmemtId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"COMPANY_USER_ACCESS\" WHERE \"USER_ID\" = " + userId + " AND  \"DEPARTMENT_ID\"  =" + departmemtId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByDepartentId(int departmemtId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT cl.\"FIRST_NAME\" , CUA.\"USER_ID\" ,  CUA.\"DEPARTMENT_ID\" ,  CUA.\"SYSTEM_DIVISION_ID\"  , CUA.\"USER_ROLE_ID\" , CUA.\"ACTION_ID\", CUA.\"CREATED_DATE\",CUA.\"CREATED_BY\",CUA.\"UPDATED_DATE\",CUA.\"UPDATED_BY\", UR.\"ROLE_NAME\" FROM public.\"COMPANY_USER_ACCESS\" AS CUA     INNER JOIN \"USER_ROLE\" AS UR ON CUA.\"USER_ROLE_ID\" = UR.\"ROLE_ID\"  INNER JOIN \"COMPANY_LOGIN\" as cl ON CUA.\"USER_ID\" = cl.\"USER_ID\"  WHERE CUA.\"DEPARTMENT_ID\" =" + departmemtId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByUserId(int userId, int departmemtId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  CUA.\"USER_ID\" ,  CUA.\"DEPARTMENT_ID\" ,  CUA.\"SYSTEM_DIVISION_ID\"  , CUA.\"USER_ROLE_ID\" , CUA.\"ACTION_ID\", CUA.\"CREATED_DATE\",CUA.\"CREATED_BY\",CUA.\"UPDATED_DATE\",CUA.\"UPDATED_BY\",UR.\"ROLE_NAME\" FROM public.\"COMPANY_USER_ACCESS\" AS CUA     INNER JOIN \"USER_ROLE\" AS UR ON CUA.\"USER_ROLE_ID\" = UR.\"ROLE_ID\"   WHERE CUA.\"USER_ID\" = " + userId + " AND CUA.\"DEPARTMENT_ID\" =" + departmemtId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByUserIdAndUserRoleIdAndDivId(int userId, int departmemtId, int userRoleId, int sysDivId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  CUA.\"USER_ID\" ,  CUA.\"DEPARTMENT_ID\" ,  CUA.\"SYSTEM_DIVISION_ID\"  , CUA.\"USER_ROLE_ID\" , CUA.\"ACTION_ID\", CUA.\"CREATED_DATE\",CUA.\"CREATED_BY\",CUA.\"UPDATED_DATE\",CUA.\"UPDATED_BY\",UR.\"ROLE_NAME\" FROM public.\"COMPANY_USER_ACCESS\" AS CUA     INNER JOIN \"USER_ROLE\" AS UR ON CUA.\"USER_ROLE_ID\" = UR.\"ROLE_ID\"   WHERE CUA.\"USER_ID\" = " + userId + " AND CUA.\"DEPARTMENT_ID\" =" + departmemtId + "  AND  CUA.\"SYSTEM_DIVISION_ID\" =" + sysDivId + "  AND CUA.\"USER_ROLE_ID\"  = " + userRoleId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public List<CompanyUserAccess> GetCompanyUserAccessObjByUserId(int userId, int departmemtId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT cl.\"FIRST_NAME\" , CUA.\"USER_ID\" ,  CUA.\"DEPARTMENT_ID\" ,  CUA.\"SYSTEM_DIVISION_ID\"  , CUA.\"USER_ROLE_ID\" , CUA.\"ACTION_ID\", CUA.\"CREATED_DATE\",CUA.\"CREATED_BY\",CUA.\"UPDATED_DATE\",CUA.\"UPDATED_BY\", UR.\"ROLE_NAME\" FROM public.\"COMPANY_USER_ACCESS\" AS CUA     INNER JOIN \"USER_ROLE\" AS UR ON CUA.\"USER_ROLE_ID\" = UR.\"ROLE_ID\"  INNER JOIN \"COMPANY_LOGIN\" as cl ON CUA.\"USER_ID\" = cl.\"USER_ID\"  WHERE CUA.\"DEPARTMENT_ID\" =" + departmemtId + " AND CUA.\"USER_ID\" = " + userId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public bool isAvilableAccess(int userId, int departmemtId, int? systemDivisionId, int? functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_USER_ACCESS\" WHERE \"USER_ID\" = " + userId + " AND \"DEPARTMENT_ID\" = " + departmemtId + " AND \"SYSTEM_DIVISION_ID\" = " + systemDivisionId + "  AND \"ACTION_ID\" = " + functionId + "";
            int countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist > 0)
                return true;
            else
                return false;
        }

        public int SaveCompanyUserAccess(int userId, int departmemtId, int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_USER_ACCESS\" WHERE \"USER_ID\" = " + userId + " AND \"DEPARTMENT_ID\" = " + departmemtId + " AND \"USER_ROLE_ID\" = " + roleId + " AND \"SYSTEM_DIVISION_ID\" = " + systemDivisionId + "  AND \"ACTION_ID\" = " + functionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {

                dbConnection.cmd.CommandText = "INSERT INTO  public.\"COMPANY_USER_ACCESS\" (\"USER_ID\" , \"DEPARTMENT_ID\"  , \"USER_ROLE_ID\", \"SYSTEM_DIVISION_ID\" ,\"ACTION_ID\", \"CREATED_DATE\", \"CREATED_BY\" , \"UPDATED_DATE\" ,\"UPDATED_BY\" ) VALUES (" + userId + "," + departmemtId + "," + roleId + "," + systemDivisionId + "," + functionId + ",'" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "')";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateCompanyUserAccess(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }
        public int SaveCompanyUserAccessvusename(string username, int departmemtId, int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            return 0;
        }
        public int DeleteAllCompanyUserAccessByRoleIdAndFunctionId(int userId, int departmemtId, int userRoleId, int sysDivisionId, int functionId, DBConnection dbConnection)
        {
            return 0;
        }

        public CompanyUserAccess FetchUserRoleIdByUserId(int userId, int companyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }

    public class CompanyUserAccessDAOSQLImpl : CompanyUserAccessDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int DeleteAllCompanyUserAccessByDividionId(int userId, int departmemtId, int userRoleId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE USER_ID = " + userId + " AND  DEPARTMENT_ID  =" + departmemtId + "  AND USER_ROLE_ID =" + userRoleId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int DeleteAllCompanyUserAccessByRoleIdAndDividionId(int userId, int departmemtId, int userRoleId, int sysDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE USER_ID = " + userId + " AND  DEPARTMENT_ID  =" + departmemtId + "  AND USER_ROLE_ID =" + userRoleId + " AND SYSTEM_DIVISION_ID = " + sysDivisionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int DeleteAllCompanyUserAccessByRoleIdAndFunctionId(int userId, int departmemtId, int userRoleId, int sysDivisionId, int functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE USER_ID = " + userId + " AND  DEPARTMENT_ID  =" + departmemtId + "  AND USER_ROLE_ID =" + userRoleId + " AND SYSTEM_DIVISION_ID = " + sysDivisionId + " AND ACTION_ID="+functionId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int DeleteCompanyUserAccessByDividionId(int userId, int departmemtId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int DeleteCompanyUserAccessByUserId(int userId, int departmemtId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE USER_ID = " + userId + " AND  DEPARTMENT_ID  =" + departmemtId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByDepartentId(int departmemtId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT cl.FIRST_NAME , CUA.USER_ID ,  CUA.DEPARTMENT_ID ,  CUA.SYSTEM_DIVISION_ID  , "+
                                           " CUA.USER_ROLE_ID , CUA.ACTION_ID, CUA.CREATED_DATE,CUA.CREATED_BY,CUA.UPDATED_DATE,CUA.UPDATED_BY,"+
                                           " UR.ROLE_NAME FROM " + dbLibrary + ".COMPANY_USER_ACCESS AS CUA "+
                                           " INNER JOIN " + dbLibrary + ".USER_ROLE AS UR ON CUA.USER_ROLE_ID = UR.ROLE_ID"+
                                           " INNER JOIN " + dbLibrary + ".COMPANY_LOGIN as cl ON CUA.USER_ID = cl.USER_ID "+
                                           " WHERE CUA.DEPARTMENT_ID =" + departmemtId + " AND and UR.COMPANY_ID =" + departmemtId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByUserId(int userId, int departmemtId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  CUA.USER_ID ,  CUA.DEPARTMENT_ID ,  CUA.SYSTEM_DIVISION_ID  , CUA.USER_ROLE_ID , CUA.ACTION_ID, CUA.CREATED_DATE,CUA.CREATED_BY,CUA.UPDATED_DATE,CUA.UPDATED_BY,UR.ROLE_NAME FROM " + dbLibrary + ".COMPANY_USER_ACCESS AS CUA     INNER JOIN " + dbLibrary + ".USER_ROLE AS UR ON CUA.USER_ROLE_ID = UR.ROLE_ID   WHERE CUA.USER_ID = " + userId + " AND CUA.DEPARTMENT_ID =" + departmemtId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public List<CompanyUserAccess> FetchCompanyUserAccessByUserIdAndUserRoleIdAndDivId(int userId, int departmemtId, int userRoleId, int sysDivId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  CUA.USER_ID ,  CUA.DEPARTMENT_ID ,  CUA.SYSTEM_DIVISION_ID  , CUA.USER_ROLE_ID , CUA.ACTION_ID, CUA.CREATED_DATE,CUA.CREATED_BY,CUA.UPDATED_DATE,CUA.UPDATED_BY,UR.ROLE_NAME FROM " + dbLibrary + ".COMPANY_USER_ACCESS AS CUA     INNER JOIN " + dbLibrary + ".USER_ROLE AS UR ON CUA.USER_ROLE_ID = UR.ROLE_ID   WHERE CUA.USER_ID = " + userId + " AND CUA.DEPARTMENT_ID =" + departmemtId + "  AND  CUA.SYSTEM_DIVISION_ID =" + sysDivId + "  AND CUA.USER_ROLE_ID  = " + userRoleId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public CompanyUserAccess FetchUserRoleIdByUserId(int userId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  CUA.USER_ID ,  CUA.DEPARTMENT_ID ,  CUA.SYSTEM_DIVISION_ID  , CUA.USER_ROLE_ID , CUA.ACTION_ID, CUA.CREATED_DATE,CUA.CREATED_BY,CUA.UPDATED_DATE,CUA.UPDATED_BY,UR.ROLE_NAME " +
                                            " FROM " + dbLibrary + ".COMPANY_USER_ACCESS AS CUA     INNER JOIN " + dbLibrary + ".USER_ROLE AS UR " +
                                            " ON CUA.USER_ROLE_ID = UR.ROLE_ID   WHERE CUA.USER_ID = " + userId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public List<CompanyUserAccess> GetCompanyUserAccessObjByUserId(int userId, int departmemtId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT cl.FIRST_NAME , CUA.USER_ID ,  CUA.DEPARTMENT_ID ,  CUA.SYSTEM_DIVISION_ID  , CUA.USER_ROLE_ID , CUA.ACTION_ID, CUA.CREATED_DATE,CUA.CREATED_BY,CUA.UPDATED_DATE,CUA.UPDATED_BY, UR.ROLE_NAME FROM " + dbLibrary + ".COMPANY_USER_ACCESS AS CUA     INNER JOIN " + dbLibrary + ".USER_ROLE AS UR ON CUA.USER_ROLE_ID = UR.ROLE_ID  INNER JOIN " + dbLibrary + ".COMPANY_LOGIN as cl ON CUA.USER_ID = cl.USER_ID  WHERE CUA.DEPARTMENT_ID =" + departmemtId + " AND CUA.USER_ID = " + userId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyUserAccess>(dbConnection.dr);
            }
        }

        public bool isAvilableAccess(int userId, int departmemtId, int? systemDivisionId, int? functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE USER_ID = " + userId + " AND DEPARTMENT_ID = " + departmemtId + " AND SYSTEM_DIVISION_ID = " + systemDivisionId + "  AND ACTION_ID = " + functionId + "";
            int countExist = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist > 0)
                return true;
            else
                return false;
        }

        public int SaveCompanyUserAccess(int userId, int departmemtId, int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE USER_ID = " + userId + " AND DEPARTMENT_ID = " + departmemtId + " AND USER_ROLE_ID = " + roleId + " AND SYSTEM_DIVISION_ID = " + systemDivisionId + "  AND ACTION_ID = " + functionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMPANY_USER_ACCESS (USER_ID , DEPARTMENT_ID  , USER_ROLE_ID, SYSTEM_DIVISION_ID ,ACTION_ID, CREATED_DATE, CREATED_BY , UPDATED_DATE ,UPDATED_BY ) VALUES (" + userId + "," + departmemtId + "," + roleId + "," + systemDivisionId + "," + functionId + ",'" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "')";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }
        public int SaveCompanyUserAccessvusename (string username, int departmemtId, int roleId, int? systemDivisionId, int? functionId, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            CompanyLogin userlogin = new CompanyLogin();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT  * FROM " + dbLibrary + ".COMPANY_LOGIN WHERE USER_NAME = '" + username + "'";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
               userlogin= dataAccessObject.GetSingleOject<CompanyLogin>(dbConnection.dr);
            }
            var userId = userlogin.UserId;
            if (userId!=0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE USER_ID = " + userId + " AND DEPARTMENT_ID = " + departmemtId + " AND USER_ROLE_ID = " + roleId + " AND SYSTEM_DIVISION_ID = " + systemDivisionId + "  AND ACTION_ID = " + functionId + "";
                var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());



                if (countExist == 0)
                {

                    dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMPANY_USER_ACCESS (USER_ID , DEPARTMENT_ID  , USER_ROLE_ID, SYSTEM_DIVISION_ID ,ACTION_ID, CREATED_DATE, CREATED_BY , UPDATED_DATE ,UPDATED_BY ) VALUES (" + userId + "," + departmemtId + "," + roleId + "," + systemDivisionId + "," + functionId + ",'" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "')";
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
                return -1;
            }
        }

        public int UpdateCompanyUserAccess(int roleId, int oldFunctionId, int newFunctionId, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }
}
