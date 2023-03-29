using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructue
{
   public interface SupplierAssigneToCompanyDAO
    {
        int saveAssigneeSupplierWithCompany(int supplierid, int companyId, DateTime requesteddate, int isSupplierFollow, DBConnection dbConnection);
        int saveAssigneSupplierWithCompanyByCompany(int supplierid, int companyId, DateTime requesteddate, int isApprove, int isSupplierFollow, int isTermAgreed, DBConnection dbConnection);
        int updateAssigneeSupplierWithCompany(int supplierid, int companyId, int isApprove, DBConnection dbConnection);

        int unFollowCompanyBySupplier(int supplierid, int companyId, DBConnection dbConnection);
        List<SupplierAssignedToCompany> GetSupplierRequestsByCompanyId(int companyId, DBConnection dbConnection);
        List<SupplierAssignedToCompany> GetCompanyListBySupplierId(int supplierid, DBConnection dbConnection);
        int ApproveSupplierByCompanyId(int supplierid, int companyId, DBConnection dbConnection);
        int RejectSupplierByCompanyId(int supplierid, int companyId, DBConnection dbConnection);
        int BlockSupplierByCompanyId(int supplierid, int companyId, DBConnection dbConnection);
        int FollowActiveSupplierByCompanyId(int supplierid, int companyId, int isActiveFolow, DBConnection dbConnection);
        List<SupplierAssignedToCompany> GetSupplierAssignedCompanies(int supplierid,  DBConnection dbConnection);
        SupplierAssignedToCompany GetSupplierOfCompanyObj(int supplierid, int companyId, DBConnection dbConnection);

        List<SupplierAssignedToCompany> GetCompanyListBySupplierIdforRequest(int supplierid, DBConnection dbConnection);
        int updateUnfollowSupplier(int supplierid, int companyId, int isFollow, DBConnection dbConnection);
        List<SupplierAssignedToCompany> GetSupplierRequestsByName(int companyId, string text, DBConnection dbConnection);

    }
    
    public class SupplierAssigneToCompanyDAOSQLImpl : SupplierAssigneToCompanyDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int ApproveSupplierByCompanyId(int supplierid, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET  IS_APPROVE  =" + 1 + " WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int BlockSupplierByCompanyId(int supplierid, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET  IS_APPROVE  =" + 3 + " WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierAssignedToCompany> GetCompanyListBySupplierId(int supplierid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY WHERE SUPPLIER_ID = " + supplierid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierAssignedToCompany>(dbConnection.dr);
            }
        }

        public List<SupplierAssignedToCompany> GetSupplierRequestsByCompanyId(int companyId, DBConnection dbConnection)
        {
            List<SupplierAssignedToCompany> list = new List<SupplierAssignedToCompany>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT s.SUPPLIER_ID , s.SUPPLIER_NAME, s.EMAIL ,s.ADDRESS01, s.ADDRESS02 ,s.OFFICE_CONTACT_NO, s.MOBILE_NO, "+
                                           " s.BUSINESS_REGISTRATION_NO, s.IS_ACTIVE, sa.SUPPLIER_ID, sa.IS_APPROVE, sa.SUPPLIER_FOLLOW , sa.REQUSETED_DATE, s.IS_REGISTERED_SUPPLIER, s.SUPPLIER_REGISTRATION_N0 " +
                                           "  FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY sa INNER JOIN " + dbLibrary + ".SUPPLIER as s ON  sa.SUPPLIER_ID = s.SUPPLIER_ID "+
                                           " WHERE sa.COMPANY_ID = " + companyId + " AND s.IS_ACTIVE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                list = dataAccessObject.ReadCollection<SupplierAssignedToCompany>(dbConnection.dr);
            }

            for (int i = 0; i < list.Count; i++)
            {
                list[i].mainItemCategory = string.Join(", ",
                DAOFactory.createSupplierCategoryDAO().GetSupplierCategoryBySupplierId(list[i].SupplierId, dbConnection).Select(w => w.CategoryName));

                list[i].isAgentSupplier = DAOFactory.createSupplierDAO().getSupplierAgent2(list[i].SupplierId , dbConnection).Count == 0 ? 1 : 0;
            }
            return list;
        }

        public List<SupplierAssignedToCompany> GetSupplierRequestsByName(int companyId, string text, DBConnection dbConnection) {
            List<SupplierAssignedToCompany> list = new List<SupplierAssignedToCompany>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT s.SUPPLIER_ID , s.SUPPLIER_NAME, s.EMAIL ,s.ADDRESS01, s.ADDRESS02 ,s.OFFICE_CONTACT_NO, s.MOBILE_NO, " +
                                           " s.BUSINESS_REGISTRATION_NO, s.IS_ACTIVE, sa.SUPPLIER_ID, sa.IS_APPROVE, sa.SUPPLIER_FOLLOW , sa.REQUSETED_DATE, s.IS_REGISTERED_SUPPLIER, s.SUPPLIER_REGISTRATION_N0 " +
                                           "  FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY sa INNER JOIN " + dbLibrary + ".SUPPLIER as s ON  sa.SUPPLIER_ID = s.SUPPLIER_ID " +
                                           " WHERE sa.COMPANY_ID = " + companyId + " AND s.IS_ACTIVE = 1 AND SUPPLIER_NAME LIKE '%"+ text + "%' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                list = dataAccessObject.ReadCollection<SupplierAssignedToCompany>(dbConnection.dr);
            }

            for (int i = 0; i < list.Count; i++) {
                list[i].mainItemCategory = string.Join(", ",
                DAOFactory.createSupplierCategoryDAO().GetSupplierCategoryBySupplierId(list[i].SupplierId, dbConnection).Select(w => w.CategoryName));

                list[i].isAgentSupplier = DAOFactory.createSupplierDAO().getSupplierAgent2(list[i].SupplierId, dbConnection).Count == 0 ? 1 : 0;
            }
            return list;
        }

        public int RejectSupplierByCompanyId(int supplierid, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET  IS_APPROVE  =" + 2 + " WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int saveAssigneeSupplierWithCompany(int supplierid, int companyId, DateTime requesteddate, int isSupplierFollow, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY WHERE SUPPLIER_ID = " + supplierid + " AND COMPANY_ID = " + companyId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            int result = 0;
            if (count == 0 && isSupplierFollow == 1)
            {

                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY (SUPPLIER_ID , COMPANY_ID , REQUSETED_DATE ,IS_APPROVE,SUPPLIER_FOLLOW) VALUES (" + supplierid + "," + companyId + ",'" + requesteddate + "'," + 0 + "," + 1 + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                result = dbConnection.cmd.ExecuteNonQuery();

            }
            else
            {
                if (isSupplierFollow == 0)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET SUPPLIER_FOLLOW = " + 0 + " WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId;
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    result = dbConnection.cmd.ExecuteNonQuery();
                }
                if (isSupplierFollow == 1)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET SUPPLIER_FOLLOW = " + 1 + " WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId;
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    result = dbConnection.cmd.ExecuteNonQuery();
                }

            }

            return result;
        }

        public int saveAssigneSupplierWithCompanyByCompany(int supplierid, int companyId, DateTime requesteddate, int isApprove, int isSupplierFollow, int isTermAgreed, DBConnection dbConnection)
        {

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY WHERE SUPPLIER_ID = " + supplierid + " AND COMPANY_ID = " + companyId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY (SUPPLIER_ID , COMPANY_ID , REQUSETED_DATE ,IS_APPROVE,SUPPLIER_FOLLOW , TERM_CONDITIONS_AGREED ) VALUES (" + supplierid + "," + companyId + ",'" + requesteddate + "'," + isApprove + "," + isSupplierFollow + " , " + isTermAgreed + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET SUPPLIER_FOLLOW  = " + isSupplierFollow + " ,TERM_CONDITIONS_AGREED = " + isTermAgreed + ", REQUSETED_DATE = '" + requesteddate + "'  WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
        }

        public int updateAssigneeSupplierWithCompany(int supplierid, int companyId, int isApprove, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET  IS_APPROVE  ='" + isApprove + " WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierAssignedToCompany> GetSupplierAssignedCompanies(int supplierid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY  AS SATC" +
                                           " INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS CD" +
                                           " ON CD.DEPARTMENT_ID = SATC.COMPANY_ID " +
                                           " WHERE SATC.SUPPLIER_ID = " + supplierid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierAssignedToCompany>(dbConnection.dr);
            }
        }

        public SupplierAssignedToCompany GetSupplierOfCompanyObj(int supplierid, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT s.SUPPLIER_NAME,s.SUPPLIER_TYPE, s.EMAIL ,s.SUPPLIER_REGISTRATION_N0, s.IS_REGISTERED_SUPPLIER ,s.ADDRESS01, s.ADDRESS02 ,s.OFFICE_CONTACT_NO, s.MOBILE_NO, s.BUSINESS_REGISTRATION_NO,  s.VAT_REG_NO,  s.COMPNY_TYPE,  s.BUSINESS_CATEGORY , s.SUPPLIER_LOGO, s.IS_ACTIVE, sa.SUPPLIER_ID, sa.IS_APPROVE, sa.SUPPLIER_FOLLOW   FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY sa INNER JOIN " + dbLibrary + ".SUPPLIER as s ON  sa.SUPPLIER_ID = s.SUPPLIER_ID  WHERE sa.COMPANY_ID = " + companyId + " AND sa.SUPPLIER_ID = " + supplierid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierAssignedToCompany>(dbConnection.dr);
            }
        }

        public int FollowActiveSupplierByCompanyId(int supplierid, int companyId, int isActiveFolow, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET  SUPPLIER_FOLLOW  =" + isActiveFolow + " WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierAssignedToCompany> GetCompanyListBySupplierIdforRequest(int supplierid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY AS sac  INNER JOIN " + dbLibrary + ".COMPANY_DEPARTMENT AS cd ON (sac.COMPANY_ID = cd.DEPARTMENT_ID) WHERE sac.SUPPLIER_ID = " + supplierid + " AND cd.IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierAssignedToCompany>(dbConnection.dr);
            }
        }

        public int unFollowCompanyBySupplier(int supplierid, int companyId, DBConnection dbConnection)
        {

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY WHERE SUPPLIER_ID = " + supplierid + " AND COMPANY_ID = " + companyId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY (SUPPLIER_ID , COMPANY_ID , REQUSETED_DATE ,IS_APPROVE,SUPPLIER_FOLLOW , TERM_CONDITIONS_AGREED) VALUES (" + supplierid + "," + companyId + ",'" + LocalTime.Now + "'," + 0 + "," + 0 + "," + 0 + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();

            }
            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET  IS_APPROVE  = 0 ,SUPPLIER_FOLLOW  = 0 ,TERM_CONDITIONS_AGREED = 0  WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }



        }

        public int updateUnfollowSupplier(int supplierid, int companyId, int isFollow, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY SET  SUPPLIER_FOLLOW  =" + isFollow + ",TERM_CONDITIONS_AGREED = 0 WHERE SUPPLIER_ID =" + supplierid + " AND  COMPANY_ID = " + companyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
