using System;
using System.Collections.Generic;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface CompanyDepartmentDAO
    {
        int saveDepartment( string departmentName, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address2, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo, DBConnection dbConnection);
        int updateDepartment(int departmentId, string departmentName, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address2, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo, string termsPath, DBConnection dbConnection);
        List<CompanyDepartment> GetDepartmentList(DBConnection dbConnection);
        CompanyDepartment GetDepartmentByDepartmentId(int departmentId,DBConnection dbConnection);
        CompanyDepartment GetDepartmentLogin(string username, string password, DBConnection dbConnection);
        int updateDepartmentLogo(int departmentId, string logo, DBConnection dbConnection);
        int deleteCompany(int departmentId, DBConnection dbConnection);
        int updateDepartmentTermsConditions(int departmentId, string termsPath, DBConnection dbConnection);
        List<CompanyDepartment> AssignSupplierWithCompany(int supplier, DBConnection dbConnection);
    }

    public class CompanyDepartmentDAOImpl : CompanyDepartmentDAO
    {
        public List<CompanyDepartment> AssignSupplierWithCompany(int supplier, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int deleteCompany(int departmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE  public.\"COMPANY_DEPARTMENT\"  SET \"IS_ACTIVE\" = 0 WHERE  \"DEPARTMENT_ID\" = " + departmentId + "";
            dbConnection.cmd.ExecuteNonQuery();
            return departmentId;
        }

        public CompanyDepartment GetDepartmentByDepartmentId(int departmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"COMPANY_DEPARTMENT\" WHERE \"DEPARTMENT_ID\" = " + departmentId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyDepartment>(dbConnection.dr);
            }
        }

        public List<CompanyDepartment> GetDepartmentList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"COMPANY_DEPARTMENT\" ORDER BY \"DEPARTMENT_NAME\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyDepartment>(dbConnection.dr);
            }
        }

        public CompanyDepartment GetDepartmentLogin(string username, string password, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"COMPANY_DEPARTMENT\" WHERE \"USER_NAME\" = '" + username + "' AND \"PASSWORD\" = '" + password + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyDepartment>(dbConnection.dr);
            }
        }

        public int saveDepartment(string departmentName, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address2, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo, DBConnection dbConnection)
        {
            int departmentId = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_DEPARTMENT\" WHERE \"DEPARTMENT_NAME\" = '" + departmentName + "'";
            var countExistName = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExistName == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_DEPARTMENT\"";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (count == 0)
                {
                    departmentId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"DEPARTMENT_ID\") + 1 AS MAXid FROM public.\"COMPANY_DEPARTMENT\"";
                    departmentId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"COMPANY_DEPARTMENT\" (\"DEPARTMENT_ID\" , \"DEPARTMENT_NAME\" , \"CREATED_DATE\" , \"CREATED_BY\", \"UPDATED_DATE\",\"UPDATED_BY\",\"IS_ACTIVE\", \"DEPARTMENT_IMAGE_PATH\" ,\"ADDRESS1\" , \"ADDRESS2\" , \"CITY\", \"COUNTRY\" , \"PHONE_NO\", \"MOBILE_NO\", \"FAX_NO\", \"VAT_NO\" ) VALUES " +
                    "(" + departmentId + ",'" + departmentName + "','" + createdDate + "','" + createdBy + "','" + updatedDate + "','" + updatedBy + "'," + isActive + ",'" + imagePath + "','" + address1 + "','" + address2 + "','" + city + "','" + country + "','" + phoneNo + "','" + mobileNo + "','" + faxNo + "','" + vatNo + "')";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();
                return departmentId;
            }
            else
            {
                return -1;
            }
        }

        public int updateDepartment(int departmentId, string departmentName, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address2, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo, string termsPath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"COMPANY_DEPARTMENT\" WHERE \"DEPARTMENT_NAME\" = '" + departmentName + "' AND \"DEPARTMENT_ID\" != " + departmentId + "";
            var countExistName = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExistName == 0)
            {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE  public.\"COMPANY_DEPARTMENT\"  SET \"DEPARTMENT_NAME\"  = '" + departmentName + "',  \"UPDATED_DATE\"  = '" + updatedDate + "' ,\"UPDATED_BY\"  = '" + updatedBy + "',\"IS_ACTIVE\" =" + isActive + ", \"DEPARTMENT_IMAGE_PATH\" = '" + imagePath + "', \"ADDRESS1\" = '" + address1 + "', \"ADDRESS2\" = '" + address2 + "', \"CITY\" = '" + city + "', \"COUNTRY\" = '" + country + "' , \"PHONE_NO\" = '" + phoneNo + "', \"MOBILE_NO\" = '" + mobileNo + "', \"FAX_NO\" = '" + faxNo + "', \"VAT_NO\" = '" + vatNo + "' ,  \"TERM_CONDITION_FILE_PATH\" ='" + termsPath + "'  WHERE  \"DEPARTMENT_ID\" = " + departmentId + "";
                dbConnection.cmd.ExecuteNonQuery();
                return departmentId;
            }
            else
            {
                return -1;
            }
        }

        public int updateDepartmentLogo(int departmentId, string logo, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE  public.\"COMPANY_DEPARTMENT\"  SET  \"DEPARTMENT_IMAGE_PATH\" = '" + logo + "' WHERE  \"DEPARTMENT_ID\" = " + departmentId + "";
            dbConnection.cmd.ExecuteNonQuery();
            return departmentId;
        }

        public int updateDepartmentTermsConditions(int departmentId, string termsPath, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE  public.\"COMPANY_DEPARTMENT\"  SET  \"TERM_CONDITION_FILE_PATH\" = '" + termsPath + "' WHERE  \"DEPARTMENT_ID\" = " + departmentId + "";
            dbConnection.cmd.ExecuteNonQuery();
            return departmentId;
        }
    }

    public class CompanyDepartmentDAOSQLImpl : CompanyDepartmentDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

       

        public int deleteCompany(int departmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".COMPANY_DEPARTMENT  SET IS_ACTIVE = 0 WHERE  DEPARTMENT_ID = " + departmentId + "";
            dbConnection.cmd.ExecuteNonQuery();
            return departmentId;
        }

        public CompanyDepartment GetDepartmentByDepartmentId(int departmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_DEPARTMENT WHERE DEPARTMENT_ID = " + departmentId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyDepartment>(dbConnection.dr);
            }
        }

        public List<CompanyDepartment> GetDepartmentList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_DEPARTMENT ORDER BY DEPARTMENT_NAME";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyDepartment>(dbConnection.dr);
            }
        }

        public CompanyDepartment GetDepartmentLogin(string username, string password, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_DEPARTMENT WHERE USER_NAME = '" + username + "' AND PASSWORD = '" + password + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CompanyDepartment>(dbConnection.dr);
            }
        }

        public int saveDepartment(string departmentName, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address2, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo, DBConnection dbConnection)
        {
            int departmentId = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_DEPARTMENT WHERE DEPARTMENT_NAME = '" + departmentName + "'";
            var countExistName = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExistName == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_DEPARTMENT";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (count == 0)
                {
                    departmentId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (DEPARTMENT_ID) + 1 AS MAXid FROM " + dbLibrary + ".COMPANY_DEPARTMENT";
                    departmentId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMPANY_DEPARTMENT (DEPARTMENT_ID , DEPARTMENT_NAME , CREATED_DATE , CREATED_BY, UPDATED_DATE,UPDATED_BY,IS_ACTIVE, DEPARTMENT_IMAGE_PATH ,ADDRESS1 , ADDRESS2 , CITY, COUNTRY , PHONE_NO, MOBILE_NO, FAX_NO, VAT_NO ) VALUES " +
                    "(" + departmentId + ",'" + departmentName + "','" + createdDate + "','" + createdBy + "','" + updatedDate + "','" + updatedBy + "'," + isActive + ",'" + imagePath + "','" + address1 + "','" + address2 + "','" + city + "','" + country + "','" + phoneNo + "','" + mobileNo + "','" + faxNo + "','" + vatNo + "')";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();
                return departmentId;
            }
            else
            {
                return -1;
            }
        }

        public int updateDepartment(int departmentId, string departmentName, DateTime updatedDate, string updatedBy, int isActive, string imagePath, string address1, string address2, string city, string country, string phoneNo, string mobileNo, string faxNo, string vatNo, string termsPath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".COMPANY_DEPARTMENT WHERE DEPARTMENT_NAME = '" + departmentName + "' AND DEPARTMENT_ID != " + departmentId + "";
            var countExistName = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExistName == 0)
            {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".COMPANY_DEPARTMENT  SET DEPARTMENT_NAME  = '" + departmentName + "',  UPDATED_DATE  = '" + updatedDate + "' ,UPDATED_BY  = '" + updatedBy + "',IS_ACTIVE =" + isActive + ", DEPARTMENT_IMAGE_PATH = '" + imagePath + "', ADDRESS1 = '" + address1 + "', ADDRESS2 = '" + address2 + "', CITY = '" + city + "', COUNTRY = '" + country + "' , PHONE_NO = '" + phoneNo + "', MOBILE_NO = '" + mobileNo + "', FAX_NO = '" + faxNo + "', VAT_NO = '" + vatNo + "' ,  TERM_CONDITION_FILE_PATH ='" + termsPath + "'  WHERE  DEPARTMENT_ID = " + departmentId + "";
                dbConnection.cmd.ExecuteNonQuery();
                return departmentId;
            }
            else
            {
                return -1;
            }
        }

        public int updateDepartmentLogo(int departmentId, string logo, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".COMPANY_DEPARTMENT  SET  DEPARTMENT_IMAGE_PATH = '" + logo + "' WHERE  DEPARTMENT_ID = " + departmentId + "";
            dbConnection.cmd.ExecuteNonQuery();
            return departmentId;
        }

        public int updateDepartmentTermsConditions(int departmentId, string termsPath, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".COMPANY_DEPARTMENT  SET  TERM_CONDITION_FILE_PATH = '" + termsPath + "' WHERE  DEPARTMENT_ID = " + departmentId + "";
            dbConnection.cmd.ExecuteNonQuery();
            return departmentId;
        }

        public List<CompanyDepartment> AssignSupplierWithCompany(int supplier, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMPANY_DEPARTMENT  AS CD LEFT JOIN SUPPLIER_ASSIGNED_TO_COMPANY AS SATC ON (CD.DEPARTMENT_ID = SATC.COMPANY_ID ) WHERE CD.IS_ACTIVE = 1 AND SATC.SUPPLIER_ID = "+ supplier + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CompanyDepartment>(dbConnection.dr);
            }
        }
    }
}
