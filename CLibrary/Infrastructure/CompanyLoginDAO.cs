using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface CompanyLoginDAO
    {
        int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int designationId, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo, DBConnection dbConnection);
        int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int designationId, DBConnection dbConnection);

        int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo, DBConnection dbConnection);
        int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId, DBConnection dbConnection);
        CompanyLogin GetCompanyLogin(string username, string password, DBConnection dbConnection);
        CompanyLogin GetUserbyuserId(int userId, DBConnection dbConnection);
        List<CompanyLogin> GetUserListByDepartmentid(int Departmentid, DBConnection dbConnection);
        List<CompanyLogin> GetAllUserList(DBConnection dbConnection);

        //Get User By Designation
        List<CompanyLogin> GetAllUserListByDesignation(int Designation, DBConnection dbConnection);

        int UpdateInactiveUsers(int userID, int isActive, DBConnection dbConnection);
        int ChangePassword(string UserName, string OldPassword, string NewPassword, DBConnection dbConnection);
        List<CompanyLogin> GetStorekeepers(int warehouseId, DBConnection dbConnection);
        CompanyLogin GetUserDetailsbyCatergoryId(int categoryId, string type, DBConnection dbConnection);

        //Modified for GRN new
        List<string> GetUserEmailsForApprovalbyWarehouseId(int FunctionId, int CategoryId, decimal Sum, int CompanyId, int SysDivisionId, int SysActionId, int warehouseId, DBConnection dbConnection);
        List<string> GetEmailsByUserId(int userId, DBConnection dbConnection);
        List<string> GetWarehouseHeadsEmails(int WarehouseId, DBConnection dbConnection);
        CompanyLogin GetUserbyPOId(int PoId, DBConnection dbConnection);
        List<CompanyLogin> GetUserListByName(int Departmentid, string text, DBConnection dbConnection);
    }

    public class CompanyLoginDAOImpl : CompanyLoginDAO
    {
        public List<CompanyLogin> GetAllUserList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"COMPANY_LOGIN\" ORDER BY \"FIRST_NAME\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyLogin>(dbConnection.dr);
            }
        }

        //Get by designation
        public List<CompanyLogin> GetAllUserListByDesignation(int Designation, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"COMPANY_LOGIN\" WHERE DESIGNATION_ID = " + Designation + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyLogin>(dbConnection.dr);
            }
        }

        public CompanyLogin GetCompanyLogin(string username, string password, DBConnection dbConnection)
        {
            CompanyLogin GetCompanyLogin = new CompanyLogin();


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"COMPANY_LOGIN\"  WHERE \"USER_NAME\" = '" + username + "' AND \"PASSWORD\" = '" + password + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetCompanyLogin = dataAccessObject.GetSingleOject<CompanyLogin>(dbConnection.dr);
            }

            if (GetCompanyLogin.UserId != 0)
            {
                if (GetCompanyLogin.IsActive == 1)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "SELECT \"IS_ACTIVE\" FROM public.\"COMPANY_DEPARTMENT\"  WHERE \"DEPARTMENT_ID\"  = " + GetCompanyLogin.DepartmentId + "";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    var departmentActive = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                    if (departmentActive == 1)
                    {
                        return GetCompanyLogin;
                    }
                    else
                    {
                        GetCompanyLogin.UserId = -3;
                        return GetCompanyLogin;
                    }
                }
                else
                {
                    GetCompanyLogin.UserId = -2;
                    return GetCompanyLogin;
                }

            }
            else
            {
                GetCompanyLogin.UserId = -1;
                return GetCompanyLogin;
            }

        }

        public CompanyLogin GetUserbyuserId(int userId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"COMPANY_LOGIN\"  WHERE \"USER_ID\" = " + userId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyLogin>(dbConnection.dr);
            }
        }

        public List<CompanyLogin> GetUserListByDepartmentid(int Departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"COMPANY_LOGIN\"  WHERE \"DEPARTMENT_ID\" = " + Departmentid + " ORDER BY \"FIRST_NAME\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyLogin>(dbConnection.dr);
            }
        }

        public int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int designationId, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_LOGIN\" WHERE  (\"USER_NAME\" ='" + username + "' OR \"EMAIL_ADDRESS\" = '" + emailAddress + "') ";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {

                int userid = 0;

                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_LOGIN\"";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    userid = 1001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"USER_ID\") + 1 AS MAXid FROM public.\"COMPANY_LOGIN\"";
                    userid = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }


                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"COMPANY_LOGIN\" (\"DEPARTMENT_ID\" , \"USER_ID\" , \"USER_NAME\" , \"PASSWORD\"  , \"USER_TYPE\"  , \"FIRST_NAME\" , \"EMAIL_ADDRESS\" ,\"CREATED_DATE\"  , \"CREATED_BY\" , \"UPDATED_DATE\"  ,\"UPDATED_BY\"  ,\"IS_ACTIVE\" ) VALUES (" + departmentid + "," + userid + ",'" + username + "','" + password + "','" + userType + "','" + firstname + "','" + emailAddress + "','" + createdDate + "','" + createdBy + "','" + updatedDate + "','" + updatedby + "'," + isActive + ")";

                int status = dbConnection.cmd.ExecuteNonQuery();
                if (status > 0)
                {
                    return userid;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        public int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int designationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_LOGIN\" WHERE  (\"USER_NAME\" ='" + username + "' OR \"EMAIL_ADDRESS\" = '" + emailAddress + "') ";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {

                int userid = 0;

                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_LOGIN\"";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    userid = 1001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"USER_ID\") + 1 AS MAXid FROM public.\"COMPANY_LOGIN\"";
                    userid = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }


                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"COMPANY_LOGIN\" (\"DEPARTMENT_ID\" , \"USER_ID\" , \"USER_NAME\" , \"PASSWORD\"  , \"USER_TYPE\"  , \"FIRST_NAME\" , \"EMAIL_ADDRESS\" ,\"CREATED_DATE\"  , \"CREATED_BY\" , \"UPDATED_DATE\"  ,\"UPDATED_BY\"  ,\"IS_ACTIVE\" ) VALUES (" + departmentid + "," + userid + ",'" + username + "','" + password + "','" + userType + "','" + firstname + "','" + emailAddress + "','" + createdDate + "','" + createdBy + "','" + updatedDate + "','" + updatedby + "'," + isActive + ")";

                int status = dbConnection.cmd.ExecuteNonQuery();
                if (status > 0)
                {
                    return userid;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }


        public int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_LOGIN\" WHERE  (\"USER_NAME\" ='" + username + "' OR \"EMAIL_ADDRESS\" = '" + emailAddress + "') AND  \"USER_ID\" != " + userID + "";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"COMPANY_LOGIN\" SET \"USER_NAME\" = '" + username + "' , \"PASSWORD\"  = '" + password + "' , \"USER_TYPE\"  = '" + userType + "' , \"FIRST_NAME\" = '" + firstname + "' ,  \"EMAIL_ADDRESS\"  = '" + emailAddress + "' ,\"UPDATED_DATE\"  = '" + updatedDate + "' ,\"UPDATED_BY\"  = '" + updatedby + "' ,\"IS_ACTIVE\" = " + isActive + " , \"DESIGNATION_ID\" = " + designationId + " WHERE \"USER_ID\" = " + userID + "";
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_LOGIN\" WHERE  (\"USER_NAME\" ='" + username + "' OR \"EMAIL_ADDRESS\" = '" + emailAddress + "') AND  \"USER_ID\" != " + userID + "";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"COMPANY_LOGIN\" SET \"USER_NAME\" = '" + username + "' , \"PASSWORD\"  = '" + password + "' , \"USER_TYPE\"  = '" + userType + "' , \"FIRST_NAME\" = '" + firstname + "' ,  \"EMAIL_ADDRESS\"  = '" + emailAddress + "' ,\"UPDATED_DATE\"  = '" + updatedDate + "' ,\"UPDATED_BY\"  = '" + updatedby + "' ,\"IS_ACTIVE\" = " + isActive + ", \"DESIGNATION_ID\" = " + designationId + " WHERE \"USER_ID\" = " + userID + "";
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateInactiveUsers(int userID, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"COMPANY_LOGIN\" SET \"IS_ACTIVE\" = " + isActive + " WHERE \"USER_ID\" = " + userID + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ChangePassword(string UserName, string OldPassword, string NewPassword, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<CompanyLogin> GetStorekeepers(int warehouseId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public CompanyLogin GetUserDetailsbyCatergoryId(int categoryId, string type, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<string> GetUserEmailsForApprovalbyWarehouseId(int FunctionId, int CategoryId, decimal Sum, int CompanyId, int SysDivisionId, int SysActionId, int warehouseId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<string> GetEmailsByUserId(int userId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }
        public List<string> GetWarehouseHeadsEmails(int WarehouseId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public CompanyLogin GetUserbyPOId(int PoId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<CompanyLogin> GetUserListByName(int Departmentid, string text, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }
    }

    public class CompanyLoginDAOSQLImpl : CompanyLoginDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
        public List<string> GetWarehouseHeadsEmails(int WarehouseId, DBConnection dbConnection)
        {
            List<string> emails = new List<string>();
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT DISTINCT(EMAIL_ADDRESS) FROM COMPANY_LOGIN WHERE USER_ID IN(" +
                                            "SELECT USER_ID FROM USER_WAREHOUSE WHERE WAREHOUSE_ID = " + WarehouseId + " AND IS_HEAD=1) ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        emails.Add(dbConnection.dr[0].ToString());
                    }
                }
            }

            return emails;
        }
        public List<CompanyLogin> GetAllUserList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_LOGIN ORDER BY FIRST_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyLogin>(dbConnection.dr);
            }
        }

        //Get by designation
        public List<CompanyLogin> GetAllUserListByDesignation(int Designation, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "  SELECT * FROM  " + dbLibrary + ".COMPANY_LOGIN WHERE DESIGNATION_ID = " + Designation + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyLogin>(dbConnection.dr);
            }
        }
        public CompanyLogin GetCompanyLogin(string username, string password, DBConnection dbConnection)
        {
            CompanyLogin GetCompanyLogin = new CompanyLogin();


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_LOGIN  WHERE USER_NAME = '" + username + "' AND PASSWORD = '" + password + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetCompanyLogin = dataAccessObject.GetSingleOject<CompanyLogin>(dbConnection.dr);
            }

            if (GetCompanyLogin.UserId != 0)
            {
                if (GetCompanyLogin.IsActive == 1)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "SELECT IS_ACTIVE FROM " + dbLibrary + ".COMPANY_DEPARTMENT  WHERE DEPARTMENT_ID  = " + GetCompanyLogin.DepartmentId + "";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    var departmentActive = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                    if (departmentActive == 1)
                    {
                        return GetCompanyLogin;
                    }
                    else
                    {
                        GetCompanyLogin.UserId = -3;
                        return GetCompanyLogin;
                    }
                }
                else
                {
                    GetCompanyLogin.UserId = -2;
                    return GetCompanyLogin;
                }

            }
            else
            {
                GetCompanyLogin.UserId = -1;
                return GetCompanyLogin;
            }

        }

        public CompanyLogin GetUserbyuserId(int userId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_LOGIN  WHERE USER_ID = " + userId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyLogin>(dbConnection.dr);
            }
        }

        public CompanyLogin GetUserbyPOId(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM COMPANY_LOGIN WHERE USER_ID = (SELECT CREATED_BY FROM PO_MASTER WHERE PO_ID = " + PoId + " )";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyLogin>(dbConnection.dr);
            }
        }



        public List<CompanyLogin> GetUserListByDepartmentid(int Departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            List<CompanyLogin> users;
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_LOGIN AS A " +
                "WHERE A.DEPARTMENT_ID = " + Departmentid + " " +
                "ORDER BY A.FIRST_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                users = dataAccessObject.ReadCollection<CompanyLogin>(dbConnection.dr);
            }
            for (int i = 0; i < users.Count; i++)
            {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".USER_SUB_DEPARTMENT AS USD " +
                                                "INNER JOIN SUB_DEPARTMENT AS SD ON SD.SUB_DEPARTMENT_ID = USD.SUB_DEPARTMENT_ID " +
                                               " WHERE USER_ID = " + users[i].UserId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
                {
                    DataAccessObject dataAccessObject = new DataAccessObject();
                    users[i].DepartmentList = dataAccessObject.ReadCollection<UserSubDepartment>(dbConnection.dr);

                }

                if (users[i].DepartmentList.Count > 0)
                    users[i].SubDepartmentName = string.Join(", ", users[i].DepartmentList.Select(d => d.DepartmentName));
                else
                    users[i].SubDepartmentName = "NOT ASSIGNED";

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".USER_WAREHOUSE AS UW " +
                                                "INNER JOIN WAREHOUSE AS W ON W.WAREHOUSE_ID = UW.WAREHOUSE_ID " +
                                               " WHERE USER_ID = " + users[i].UserId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
                {
                    DataAccessObject dataAccessObject = new DataAccessObject();
                    users[i].WarehouseList = dataAccessObject.ReadCollection<UserWarehouse>(dbConnection.dr);

                }
                if (users[i].WarehouseList.Count > 0)
                    users[i].WarehouseName = string.Join(", ", users[i].WarehouseList.Select(d => d.Location));
                else
                    users[i].WarehouseName = "NOT ASSIGNED";
            }
            return users;
        }

        public List<CompanyLogin> GetUserListByName(int Departmentid, string text, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            List<CompanyLogin> users;
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_LOGIN AS A " +
                "WHERE A.DEPARTMENT_ID = " + Departmentid + " AND FIRST_NAME LIKE '%" + text + "%' " +
                "ORDER BY A.FIRST_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                users = dataAccessObject.ReadCollection<CompanyLogin>(dbConnection.dr);
            }
            for (int i = 0; i < users.Count; i++)
            {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".USER_SUB_DEPARTMENT AS USD " +
                                                "INNER JOIN SUB_DEPARTMENT AS SD ON SD.SUB_DEPARTMENT_ID = USD.SUB_DEPARTMENT_ID " +
                                               " WHERE USER_ID = " + users[i].UserId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
                {
                    DataAccessObject dataAccessObject = new DataAccessObject();
                    users[i].DepartmentList = dataAccessObject.ReadCollection<UserSubDepartment>(dbConnection.dr);

                }

                if (users[i].DepartmentList.Count > 0)
                    users[i].SubDepartmentName = string.Join(", ", users[i].DepartmentList.Select(d => d.DepartmentName));
                else
                    users[i].SubDepartmentName = "NOT ASSIGNED";

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".USER_WAREHOUSE AS UW " +
                                                "INNER JOIN WAREHOUSE AS W ON W.WAREHOUSE_ID = UW.WAREHOUSE_ID " +
                                               " WHERE USER_ID = " + users[i].UserId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
                {
                    DataAccessObject dataAccessObject = new DataAccessObject();
                    users[i].WarehouseList = dataAccessObject.ReadCollection<UserWarehouse>(dbConnection.dr);

                }
                if (users[i].WarehouseList.Count > 0)
                    users[i].WarehouseName = string.Join(", ", users[i].WarehouseList.Select(d => d.Location));
                else
                    users[i].WarehouseName = "NOT ASSIGNED";
            }
            return users;
        }

        public int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int designationId, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_LOGIN WHERE  (USER_NAME ='" + username + "' OR EMAIL_ADDRESS = '" + emailAddress + "' OR EMPLOYEE_NO = '" + empNo + "') ";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {

                int userid = 0;

                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_LOGIN";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    userid = 1001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (USER_ID) + 1 AS MAXid FROM " + dbLibrary + ".COMPANY_LOGIN";
                    userid = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }


                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMPANY_LOGIN (DEPARTMENT_ID , USER_ID , USER_NAME ,EMPLOYEE_NO, PASSWORD  , USER_TYPE  , FIRST_NAME , EMAIL_ADDRESS ,CREATED_DATE  , CREATED_BY , UPDATED_DATE  ,UPDATED_BY  ,IS_ACTIVE, DESIGNATION_ID, CONTACT_NO) VALUES (" + departmentid + "," + userid + ",'" + username + "','" + empNo + "','" + password + "','" + userType + "','" + firstname + "','" + emailAddress + "','" + createdDate + "','" + createdBy + "','" + updatedDate + "','" + updatedby + "'," + isActive + "," + designationId + ",'" + contactNo + "') ";

                for (int i = 0; i < usersubdepartment.Count; i++)
                {
                    dbConnection.cmd.CommandText += "INSERT INTO " + dbLibrary + ".USER_SUB_DEPARTMENT (USER_ID , SUB_DEPARTMENT_ID ,IS_HEAD) VALUES (" + userid + "," + usersubdepartment[i].DepartmentId + "," + usersubdepartment[i].IsHead + "); \n";
                }

                for (int i = 0; i < warehouse.Count; i++)
                {
                    dbConnection.cmd.CommandText += "INSERT INTO " + dbLibrary + ".USER_WAREHOUSE (USER_ID , WAREHOUSE_ID ,IS_HEAD, USER_TYPE) VALUES (" + userid + "," + warehouse[i].WrehouseId + "," + warehouse[i].IsHead + "," + warehouse[i].UserType + "); \n";
                }

                int status = dbConnection.cmd.ExecuteNonQuery();
                if (status > 0)
                {
                    return userid;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        public int SaveCompanyLogin(int departmentid, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedby, int isActive, int designationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_LOGIN WHERE  (USER_NAME ='" + username + "' OR EMAIL_ADDRESS = '" + emailAddress + "' OR EMPLOYEE_NO = '" + empNo + "') ";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {

                int userid = 0;

                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_LOGIN";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    userid = 1001;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (USER_ID) + 1 AS MAXid FROM " + dbLibrary + ".COMPANY_LOGIN";
                    userid = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }


                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMPANY_LOGIN (DEPARTMENT_ID , USER_ID , USER_NAME ,EMPLOYEE_NO, PASSWORD  , USER_TYPE  , FIRST_NAME , EMAIL_ADDRESS ,CREATED_DATE  , CREATED_BY , UPDATED_DATE  ,UPDATED_BY  ,IS_ACTIVE,DESIGNATION_ID ) VALUES (" + departmentid + "," + userid + ",'" + username + "','" + empNo + "','" + password + "','" + userType + "','" + firstname + "','" + emailAddress + "','" + createdDate + "','" + createdBy + "','" + updatedDate + "','" + updatedby + "'," + isActive + "," + designationId + ")";

                int status = dbConnection.cmd.ExecuteNonQuery();
                if (status > 0)
                {
                    return userid;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return -1;
            }
        }

        public int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId, List<UserSubDepartment> usersubdepartment, List<UserWarehouse> warehouse, string contactNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_LOGIN WHERE  (USER_NAME ='" + username + "' OR EMAIL_ADDRESS = '" + emailAddress + "' OR EMPLOYEE_NO = '" + empNo + "') AND  USER_ID != " + userID + "";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".COMPANY_LOGIN SET USER_NAME = '" + username + "', EMPLOYEE_NO  = '" + empNo + "' , PASSWORD  = '" + password + "' , USER_TYPE  = '" + userType + "' , FIRST_NAME = '" + firstname + "' ,  EMAIL_ADDRESS  = '" + emailAddress + "' ,UPDATED_DATE  = '" + updatedDate + "' ,UPDATED_BY  = '" + updatedby + "' ,IS_ACTIVE = " + isActive + ", DESIGNATION_ID=" + designationId + ", CONTACT_NO = '" + contactNo + "'  WHERE USER_ID = " + userID + "";

                dbConnection.cmd.CommandText += "DELETE FROM USER_SUB_DEPARTMENT WHERE USER_ID = " + userID + " \n";
                for (int i = 0; i < usersubdepartment.Count; i++)
                {
                    dbConnection.cmd.CommandText += "INSERT INTO " + dbLibrary + ".USER_SUB_DEPARTMENT (USER_ID , SUB_DEPARTMENT_ID ,IS_HEAD) VALUES (" + userID + "," + usersubdepartment[i].DepartmentId + "," + usersubdepartment[i].IsHead + "); \n";
                }

                dbConnection.cmd.CommandText += "DELETE FROM USER_WAREHOUSE WHERE USER_ID = " + userID + "\n ";
                for (int i = 0; i < warehouse.Count; i++)
                {
                    dbConnection.cmd.CommandText += "INSERT INTO " + dbLibrary + ".USER_WAREHOUSE (USER_ID , WAREHOUSE_ID ,IS_HEAD, USER_TYPE) VALUES (" + userID + "," + warehouse[i].WrehouseId + "," + warehouse[i].IsHead + ", " + warehouse[i].UserType + "); \n";
                }
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateCompanyLogin(int userID, string username, string empNo, string password, string userType, string firstname, string emailAddress, DateTime updatedDate, string updatedby, int isActive, int designationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_LOGIN WHERE  (USER_NAME ='" + username + "' OR EMAIL_ADDRESS = '" + emailAddress + "' OR EMPLOYEE_NO = '" + empNo + "') AND  USER_ID != " + userID + "";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".COMPANY_LOGIN SET USER_NAME = '" + username + "', EMPLOYEE_NO  = '" + empNo + "' , PASSWORD  = '" + password + "' , USER_TYPE  = '" + userType + "' , FIRST_NAME = '" + firstname + "' ,  EMAIL_ADDRESS  = '" + emailAddress + "' ,UPDATED_DATE  = '" + updatedDate + "' ,UPDATED_BY  = '" + updatedby + "' ,IS_ACTIVE = " + isActive + " ,DESIGNATION_ID=" + designationId + " WHERE USER_ID = " + userID + "";
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }


        public int UpdateInactiveUsers(int userID, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".COMPANY_LOGIN SET IS_ACTIVE = " + isActive + " WHERE USER_ID = " + userID + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ChangePassword(string UserName, string OldPassword, string NewPassword, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_LOGIN WHERE USER_NAME ='" + UserName + "' AND PASSWORD = '" + OldPassword + "'";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 1)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".COMPANY_LOGIN SET PASSWORD  = '" + NewPassword + "' WHERE USER_NAME = '" + UserName + "'";
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public List<CompanyLogin> GetStorekeepers(int warehouseId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "Select * From " + dbLibrary + ".COMPANY_LOGIN  WHERE DESIGNATION_ID=18 AND WAREHOUSE_ID=" + warehouseId + " ";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyLogin>(dbConnection.dr);
            }
        }

        public CompanyLogin GetUserDetailsbyCatergoryId(int categoryId, string type, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  *  FROM " + dbLibrary + ".[COMPANY_LOGIN] " +
                                           " where USER_ID = (Select Top 1 USER_ID FROM " + dbLibrary + ".[ITEM_CATEGORY_OWNERS] " +
                                           " WHERE CATEGORY_ID = " + categoryId + " " +
                                           " AND OWNER_TYPE = '" + type + "' AND EFFECTIVE_DATE <= '" + LocalTime.Now + "' ORDER BY EFFECTIVE_DATE DESC)";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyLogin>(dbConnection.dr);
            }
        }

        public List<string> GetUserEmailsForApprovalbyWarehouseId(int FunctionId, int CategoryId, decimal Sum, int CompanyId, int SysDivisionId, int SysActionId, int warehouseId, DBConnection dbConnection)
        {
            List<string> userId = new List<string>();
            dbConnection.cmd.Parameters.Clear();

            String sql = "";
            sql = sql + "BEGIN " + "\n";
            sql = sql + "DECLARE @LIMIT_USER_IDS TABLE(ID INT) " + "\n";
            sql = sql + "		 " + "\n";
            sql = sql + "INSERT INTO @LIMIT_USER_IDS " + "\n";
            //sql = sql + "SELECT USER_ID FROM APPROVAL_LIMIT_USERS WHERE LIMIT_ID IN( " + "\n";
            //sql = sql + "SELECT LIMIT_ID FROM APPROVAL_LIMIT WHERE LIMIT_FROM <=" + Sum + " AND LIMIT_TO >= " + Sum + " AND FUNCTION_ID = " + FunctionId + " AND CATEGORY_ID=" + CategoryId + " AND COMPANY_ID=" + CompanyId + " " + "\n";
            sql = sql + " " + "\n";
            sql = sql + "SELECT USER_ID FROM COMPANY_LOGIN WHERE USER_ID IN( " + "\n";
            sql = sql + "SELECT ID FROM @LIMIT_USER_IDS) " + "\n";
            sql = sql + "UNION ALL " + "\n";
            sql = sql + "SELECT USER_ID FROM COMPANY_LOGIN WHERE USER_ID IN( " + "\n";
            sql = sql + "SELECT USER_ID FROM COMPANY_USER_ACCESS WHERE SYSTEM_DIVISION_ID=" + SysDivisionId + " AND ACTION_ID=" + SysActionId + " AND USER_ID IN( " + "\n";
            sql = sql + "SELECT USER_ID FROM COMPANY_LOGIN WHERE USER_ID NOT IN ( " + "\n";
            //sql = sql + "SELECT USER_ID FROM APPROVAL_LIMIT_USERS WHERE LIMIT_ID IN( " + "\n";
            //sql = sql + "SELECT LIMIT_ID FROM APPROVAL_LIMIT WHERE FUNCTION_ID = " + FunctionId + " AND COMPANY_ID=" + CompanyId + ")) AND DEPARTMENT_ID=" + CompanyId + ") " + "\n";
            // sql = sql + "UNION ALL " + "\n";
            sql = sql + "SELECT USER_ID FROM COMPANY_LOGIN WHERE USER_TYPE='S' AND DEPARTMENT_ID=" + CompanyId + " " + "\n";
            sql = sql + "END";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        userId.Add(dbConnection.dr[0].ToString());
                    }
                }
            }
            List<string> emails = new List<string>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID IN (SELECT USER_ID FROM USER_WAREHOUSE WHERE IS_HEAD = 1 AND WAREHOUSE_ID = " + warehouseId + " AND USER_ID IN(" + string.Join(",", userId) + ")) ";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        emails.Add(dbConnection.dr[0].ToString());
                    }
                }
            }
            return emails;
        }

        public List<string> GetEmailsByUserId(int userId, DBConnection dbConnection)
        {

            List<string> emails = new List<string>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT EMAIL_ADDRESS FROM COMPANY_LOGIN WHERE USER_ID= " + userId + " ";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        emails.Add(dbConnection.dr[0].ToString());
                    }
                }
            }
            return emails;
        }
    }
}
