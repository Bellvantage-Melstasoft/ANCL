using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface SubDepartmentDAOInterface
    {
        int saveSubDepartment(string departmentName, string phoneNo, int companyID, int isActive, List<int> UserIds, DBConnection dbConnection);
        int updateSubDepartment(int subDepartmentID, string departmentName, string phoneNo, int companyID, int isActive, List<int> UserIds, DBConnection dbConnection);
        int deleteSubDepartment(int subDepartmentID, DBConnection dbConnection);
        List<SubDepartment> getDepartmentList(int companyID,DBConnection dbConnection);
        SubDepartment getDepartmentByID(int departmentID, DBConnection dbConnection);
        int isUserHeadOfDepartment(int userID, DBConnection dbConnection);
        int getsubDepartment(int companyID, int userID, DBConnection dbConnection);
        List<SubDepartment> getAllDepartmentList(int companyID, DBConnection dbConnection);
        int isUserHeadOfProcurement(object userID, DBConnection dbConnection);

        List<SubDepartment> getUserSubDepartmentByUserId(int UserId, DBConnection dbConnection);
        List<SubDepartment> getDepartmentListByDepartmentIds(List<int> DepartmentIds, DBConnection dbConnection);
    }

    class SubDepartmentDAO: SubDepartmentDAOInterface
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int saveSubDepartment(string departmentName,string phoneNo, int companyID, int isActive, List<int> UserIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DECLARE @SUBDEPARTMENT_IDS TABLE(SUB_DEPARTMENT_ID INT) \n";
            dbConnection.cmd.CommandText += "IF NOT EXISTS (SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT WHERE DEPARTMENT_NAME = '" + departmentName + "' AND COMPANY_ID=" + companyID + ") " +
                "INSERT INTO " + dbLibrary + ".SUB_DEPARTMENT (DEPARTMENT_NAME,PHONE_NO,COMPANY_ID,IS_ACTIVE) OUTPUT INSERTED.SUB_DEPARTMENT_ID INTO @SUBDEPARTMENT_IDS VALUES ('" + departmentName + "','" + phoneNo + "'," + companyID + "," + isActive + ")";

            for (int i = 0; i < UserIds.Count; i++) {
                dbConnection.cmd.CommandText += "INSERT INTO USER_SUB_DEPARTMENT([USER_ID],SUB_DEPARTMENT_ID,[IS_HEAD]) VALUES(" + UserIds[i] + ", (SELECT MAX(SUB_DEPARTMENT_ID) FROM @SUBDEPARTMENT_IDS), 1); \n";

            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateSubDepartment(int subDepartmentID,string departmentName, string phoneNo,int companyID, int isActive, List<int> UserIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS(SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " AND DEPARTMENT_NAME = '" + departmentName + "') " +
                                            "UPDATE " + dbLibrary + ".SUB_DEPARTMENT SET DEPARTMENT_NAME = '" + departmentName + "',PHONE_NO = '" + phoneNo + "',IS_ACTIVE = " + isActive + " " +
                                            "WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " " +
                                            "ELSE IF NOT EXISTS (SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT WHERE DEPARTMENT_NAME = '" + departmentName + "' AND COMPANY_ID=" + companyID + ") " +
                                            "UPDATE " + dbLibrary + ".SUB_DEPARTMENT SET DEPARTMENT_NAME='"+departmentName+"',PHONE_NO='"+phoneNo+"',IS_ACTIVE="+isActive+ " WHERE SUB_DEPARTMENT_ID=" + subDepartmentID + "";

            dbConnection.cmd.CommandText += "DELETE FROM USER_SUB_DEPARTMENT WHERE SUB_DEPARTMENT_ID = " + subDepartmentID + " ;";

            for (int i = 0; i < UserIds.Count; i++) {
                dbConnection.cmd.CommandText += "INSERT INTO USER_SUB_DEPARTMENT([USER_ID],SUB_DEPARTMENT_ID,[IS_HEAD]) VALUES(" + UserIds[i] + ", " + subDepartmentID + ", 1); \n";
            }

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int deleteSubDepartment(int subDepartmentID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUB_DEPARTMENT SET IS_ACTIVE=0 WHERE SUB_DEPARTMENT_ID=" + subDepartmentID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SubDepartment> getDepartmentList(int companyID,DBConnection dbConnection)
        {
            List<SubDepartment> subDepartments;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT AS SD " +
                "WHERE SD.COMPANY_ID=" + companyID + " AND SD.IS_ACTIVE = 1 ORDER BY SD.DEPARTMENT_NAME ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                subDepartments = dataAccessObject.ReadCollection<SubDepartment>(dbConnection.dr);
            }
            for (int i = 0; i < subDepartments.Count; i++) {
                subDepartments[i].HeadOfDepartmentName = string.Join(", ",
                    DAOFactory.createUserSubDepartmentDAO().GetDepartmentHeadsByDepartmentId(subDepartments[i].SubDepartmentID, dbConnection).Select(w => w.UserName));
            }
            return subDepartments;
        }
        
        public SubDepartment getDepartmentByID(int departmentID,DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT WHERE SUB_DEPARTMENT_ID="+departmentID;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SubDepartment>(dbConnection.dr);
            }
        }

        public int isUserHeadOfDepartment(int userID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT WHERE HEAD_OF_DEPARTMENT=" + userID;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    dbConnection.dr.Read();
                    return int.Parse(dbConnection.dr["SUB_DEPARTMENT_ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        public int getsubDepartment(int companyID, int userID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT AS SD " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,USER_ID FROM COMPANY_LOGIN) AS CL ON SD.SUB_DEPARTMENT_ID=CL.SUB_DEPARTMENT_ID" +
                " WHERE SD.COMPANY_ID=" + companyID + " AND CL.USER_ID="+ userID + " ORDER BY SD.DEPARTMENT_NAME ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
           
        }

        public List<SubDepartment> getAllDepartmentList(int companyID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT AS SD "+
                                           "WHERE SD.COMPANY_ID=" + companyID + " ORDER BY SD.DEPARTMENT_NAME ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SubDepartment>(dbConnection.dr);
            }
        }

        public List<SubDepartment> getUserSubDepartmentByUserId(int UserId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM SUB_DEPARTMENT AS SD " +
                                            "INNER JOIN (SELECT SUB_DEPARTMENT_ID, USER_ID FROM USER_SUB_DEPARTMENT WHERE USER_ID = " + UserId + ") AS USD ON USD.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID ";
                                            
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SubDepartment>(dbConnection.dr);
            }
        }
        public int isUserHeadOfProcurement(object userID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT "+ 
            //                               " WHERE HEAD_OF_DEPARTMENT=" + userID +" "+
            //                               " AND DEPARTMENT_NAME ='Procurement'";

            dbConnection.cmd.CommandText = "SELECT * FROM USER_SUB_DEPARTMENT WHERE SUB_DEPARTMENT_ID = 1017 AND IS_HEAD = 1 AND USER_ID =" + userID + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    dbConnection.dr.Read();
                    return int.Parse(dbConnection.dr["SUB_DEPARTMENT_ID"].ToString());
                }
                else
                {
                    return -1;
                }
            }
        }
        public List<SubDepartment> getDepartmentListByDepartmentIds(List<int> DepartmentIds, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUB_DEPARTMENT WHERE IS_ACTIVE= 1 AND SUB_DEPARTMENT_ID IN (" + string.Join(",", DepartmentIds) + "); ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SubDepartment>(dbConnection.dr);
            }
        }
    }

}
