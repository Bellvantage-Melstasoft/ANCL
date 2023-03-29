using System;
using System.Collections.Generic;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface SupplierDAO
    {
        int saveSupplier(string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string requestedDate, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive,int supplierType, int isRegistered, string SupRegNo, DBConnection dbConnection);
        int saveSupplierLogo(int supplierId, string logoPath, DBConnection dbConnection);
        int updateSupplier(int supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive,string emailAddress , int Suppliertype, int IssupplierRegistred, string SupRegNo, DBConnection dbConnection);
        int updateSupplierByAdmin(int supplierId, string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive, DBConnection dbConnection);
        List<Supplier> GetSupplierList(DBConnection dbConnection);
        Supplier GetSupplierBySupplierId(int supplierId, DBConnection dbConnection);
        bool checkExistingEmailAddress(string emailAddress, DBConnection dbConnection);
        bool checkExistingSuppliername(string supplierName, DBConnection dbConnection);

        List<Supplier> GetSupplierListisApproved(DBConnection dbConnection);
        Supplier GetSupplierBySupplierIdSVC(int supplierId, DBConnection dbConnection);

        int UpdateSupplierDeviceTocken(int supplierId, string supplierTocken, DBConnection dbConnection);

        int updateSupplierTemperory(int supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, DBConnection dbConnection);

        List<Supplier> GetApprovedSuppliersForQuotationSubmission(int CompanyId, List<int> CategoryIds, DBConnection dbConnection);
        List<Supplier> GetAllSuppliersForQuotationSubmission(List<int> CategoryIds, DBConnection dbConnection);
        List<Supplier> GetAllSuppliersToSendBidEmail(int CategoryId, DBConnection dbConnection);
        void SaveSupplierEmailForBidSubmission(int prId, int bidId, int supplierId, string supplierName, string emailAddress, DBConnection dbConnection);
       List<SupplierBidEmail> GetSupplierAssignedToBid(int prId, int bidId, DBConnection dbConnection);
        void SaveSupplierBidEmailContact(int prId, string contactName, string ContactNo, string Title ,DBConnection dbConnection);
        List<SupplierBidEmailContact> GetSupplierBidEmailContact(int prId, DBConnection dbConnection);
        void DeleteSupplierBidEmailContact(int prId, DBConnection dbConnection);
        List<CommonReference> getCountry(DBConnection dbConnection);
        List<SupplierAgent> getSupplierAgent(DBConnection dbConnection);
        int insertSupplierAgentDetails(int supplierId, string name, string address, string email, string contactNo, DBConnection dbConnection);
        int updateSupplierAgentDetails(int agentId, string name, string address, string email, string contactNo, DBConnection dbConnection);
        int deleteSupplierAgentDetails(int agentId, string name, DBConnection dbConnection);
        List<SupplierAgent> getSupplierAgentBySupplierId(int supplierId, DBConnection dbConnection);
        void SaveUnRegisteredSupplier(int prId, SupplierBidEmailContact unregisteredSuppliers, DBConnection dbConnection);
        List<SupplierBidEmailContact> GetUnRegisteredSuppliersByPrId(int prId, DBConnection dbConnection);
        void SaveSupplierAgent2(int supplierId, List<int> supplierIds, DBConnection dbConnection);
        List<SupplierAgent2> getSupplierAgent2(int supplierId, DBConnection dbConnection);
        void deleteSupplier(int supplierId, DBConnection dbConnection);
        void UpdateSupplierAgent2(int supplierId, List<int> supplierIds, DBConnection dbConnection);
        List<Supplier> FetchClearingAgentList(DBConnection dbConnection);
        List<Supplier> FetchSupplierAgent(DBConnection dbConnection);
        Supplier getSupplierByPOId(int PoId, DBConnection dbConnection);
    }    

    public class SupplierDAOSQLImpl : SupplierDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public bool checkExistingEmailAddress(string emailAddress, DBConnection dbConnection)
        {
            int departmentId = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER WHERE EMAIL = '" + emailAddress + "'";
            var countExistEmail = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExistEmail > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool checkExistingSuppliername(string supplierName, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER WHERE SUPPLIER_NAME = '" + supplierName + "'";
            var countExistName = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExistName > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Supplier GetSupplierBySupplierId(int supplierId, DBConnection dbConnection)
        {
            Supplier supplier;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER AS A " +
                            "LEFT JOIN (SELECT BUSINESS_CATEGORY_NAME, BUSINESS_CATEGORY_ID AS BUSSINESSID FROM " + dbLibrary + ".NATURE_OF_BUSINESS) AS B " +
                            "ON A.BUSINESS_CATEGORY = B.BUSSINESSID " +
                            "WHERE A.SUPPLIER_ID = " + supplierId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                supplier = dataAccessObject.GetSingleOject<Supplier>(dbConnection.dr);
            }

            if (supplier != null)
            {
                string sql = "";
                sql = sql + "IF EXISTS(SELECT SUPPLIER_ID FROM SUPPLIER_LOGIN WHERE SUPPLIER_ID=" + supplier.SupplierId + " AND USR_NAME IS NOT NULL AND USR_NAME !='') " + "\n";
                sql = sql + "BEGIN SELECT 1 AS W END " + "\n";
                sql = sql + "ELSE IF EXISTS(SELECT SUPPLIER_ID FROM SUPPLIER_LOGIN WHERE SUPPLIER_ID=" + supplier.SupplierId + " AND  (INVITATION_CODE IS NULL OR INVITATION_CODE ='')) " + "\n";
                sql = sql + "BEGIN SELECT 2 AS W END " + "\n";
                sql = sql + "ELSE IF EXISTS(SELECT SUPPLIER_ID FROM SUPPLIER_LOGIN WHERE SUPPLIER_ID=" + supplier.SupplierId + " AND  INVITATION_CODE IS NOT NULL AND INVITATION_CODE !='') " + "\n";
                sql = sql + "BEGIN SELECT 3 AS W END " + "\n";
                sql = sql + "ELSE " + "\n";
                sql = sql + "BEGIN SELECT 4 AS W END";

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = sql;
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                supplier.InvitationStatus = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            return supplier;
        }

        public Supplier GetSupplierBySupplierIdSVC(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER AS SU INNER JOIN " + dbLibrary + ".SUPPLIER_LOGIN AS SUL ON SU.SUPPLIER_ID = SUL.SUPPLIER_ID  WHERE  SU.SUPPLIER_ID = " + supplierId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Supplier>(dbConnection.dr);
            }
        }

        public List<Supplier> GetSupplierList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_ASSIGNED_TO_COMPANY AS ANCL " +
                                              "INNER JOIN(SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS SUP " +
                                              "ON SUP.SUPPLIER_ID = ANCL.SUPPLIER_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Supplier>(dbConnection.dr);
            }
        }



        public int saveSupplier(string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string requestedDate, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive,int supplierType, int isRegistered, string SupRegNo, DBConnection dbConnection)
        {
            int supplierId = 0;
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER WHERE  SUPPLIER_NAME ='" + supplierName + "'";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (existCount == 0) {
                dbConnection.cmd.CommandText = "DECLARE @SUPPLIER_ID int " +
                                           "INSERT INTO " + dbLibrary + ".SUPPLIER (SUPPLIER_NAME , ADDRESS01, ADDRESS02 , EMAIL,  OFFICE_CONTACT_NO,  MOBILE_NO,  REQUESTED_DATE,  BUSINESS_REGISTRATION_NO,  VAT_REG_NO,  COMPNY_TYPE,  BUSINESS_CATEGORY,  SUPPLIER_LOGO,  IS_REQUESTFROM_SUPPLIER,  IS_CREATEDBY_ADMIN,  IS_APPROVED ,  IS_ACTIVE,SUPPLIER_TYPE, IS_REGISTERED_SUPPLIER, SUPPLIER_REGISTRATION_N0)" +
                                           "  OUTPUT inserted.SUPPLIER_ID as SUPPLIER_ID" +
                                           " VALUES " + "('" + supplierName + "','" + address1 + "','" + address2 + "','" + email + "','" + officeContactno + "','" + mobileno + "','" + requestedDate + "','" + businssRegNo + "','" + vatregNo + "'," + companytypeId + "," + businessCategory + ",'" + logoPath + "'," + IsrequestFromSupplier + "," + IdCreatedBAmin + "," + IsApproved + "," + IsActive + "," + supplierType + ", " + isRegistered + ", '" + SupRegNo + "')" +
                                           " select @SUPPLIER_ID";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                supplierId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                
            }
            else {
                supplierId = -1;
            }
            return supplierId;
        }

        public int saveSupplierLogo(int supplierId, string logoPath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER SET SUPPLIER_LOGO='" + logoPath + "' WHERE SUPPLIER_ID=" + supplierId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateSupplier(int supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive,string emailAddress, int Suppliertype, int IssupplierRegistred, string SupRegNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER "+
                                           " SET  SUPPLIER_NAME = '" + supplierName + "', ADDRESS01 = '" + address1 + "',ADDRESS02 ='" + address2 + "' ,"+
                                           " OFFICE_CONTACT_NO = '" + officeContactno + "',  MOBILE_NO = '" + mobileno + "',"+
                                           " BUSINESS_REGISTRATION_NO = '" + businssRegNo + "',  VAT_REG_NO = '" + vatregNo + "', SUPPLIER_TYPE = "+ Suppliertype + " ,"+
                                           "  COMPNY_TYPE = " + companytypeId + ",  BUSINESS_CATEGORY = " + businessCategory + ","+
                                           "  SUPPLIER_LOGO = '" + logoPath + "',  IS_REQUESTFROM_SUPPLIER = " + IsrequestFromSupplier + ","+
                                           "  IS_CREATEDBY_ADMIN = " + IdCreatedBAmin + ",  IS_APPROVED  = " + IsApproved + ",  IS_ACTIVE = " + IsActive + " , IS_REGISTERED_SUPPLIER = "+ IssupplierRegistred + ", SUPPLIER_REGISTRATION_N0 = '"+ SupRegNo + "', " +
                                           " EMAIL = '"+emailAddress+"' " +
                                           " WHERE  SUPPLIER_ID = " + supplierId + "";
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public int updateSupplierByAdmin(int supplierId, string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER  SET  SUPPLIER_NAME = '" + supplierName + "', ADDRESS01 = '" + address1 + "',ADDRESS02 ='" + address2 + "' , EMAIL = '" + email + "',  OFFICE_CONTACT_NO = '" + officeContactno + "',  MOBILE_NO = '" + mobileno + "',  BUSINESS_REGISTRATION_NO = '" + businssRegNo + "',  VAT_REG_NO = '" + vatregNo + "',  COMPNY_TYPE = " + companytypeId + ",  BUSINESS_CATEGORY = " + businessCategory + ",  SUPPLIER_LOGO = '" + logoPath + "',  IS_REQUESTFROM_SUPPLIER = " + IsrequestFromSupplier + ",  IS_CREATEDBY_ADMIN = " + IdCreatedBAmin + ",  IS_APPROVED  = " + IsApproved + ",  IS_ACTIVE = " + IsActive + " WHERE  SUPPLIER_ID = " + supplierId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Supplier> GetSupplierListisApproved(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " select * from " + dbLibrary + ".SUPPLIER AS SU" +
                                           " inner join " + dbLibrary + ".SUPPLIER_LOGIN  AS SUL on SU.SUPPLIER_ID = SUL.SUPPLIER_ID " +
                                           " where SUL.IS_ACTIVE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Supplier>(dbConnection.dr);
            }
        }

        public int UpdateSupplierDeviceTocken(int supplierId, string supplierTocken, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER  SET  SUPPLIER_DEVICE_TOCKEN = '" + supplierTocken + "'  WHERE  SUPPLIER_ID = " + supplierId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateSupplierTemperory(int supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER  SET  SUPPLIER_NAME = '" + supplierName + "', ADDRESS01 = '" + address1 + "',ADDRESS02 ='" + address2 + "' ,  OFFICE_CONTACT_NO = '" + officeContactno + "',  MOBILE_NO = '" + mobileno + "', BUSINESS_REGISTRATION_NO = '" + businssRegNo + "',  VAT_REG_NO = '" + vatregNo + "',  COMPNY_TYPE = " + companytypeId + ",  BUSINESS_CATEGORY = '" + businessCategory + "' WHERE  SUPPLIER_ID = " + supplierId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Supplier> GetApprovedSuppliersForQuotationSubmission(int CompanyId, List<int> CategoryIds, DBConnection dbConnection)
        {
            string sql = "SELECT SUPPLIER_ID,SUPPLIER_NAME FROM SUPPLIER WHERE SUPPLIER_ID IN (\n" +
                            "SELECT SUPPLIER_ID FROM SUPPLIER_ASSIGNED_TO_COMPANY WHERE COMPANY_ID = " + CompanyId + " AND IS_APPROVE = 1) AND SUPPLIER_ID IN (\n" +
                            "SELECT SUPPLIER_ID FROM SUPPLIER_CATEGORY WHERE CATEGORY_ID IN (";
            for (int i = 0; i < CategoryIds.Count; i++)
            {
                if (i == CategoryIds.Count - 1)
                {
                    sql += CategoryIds[i];
                }
                else
                {
                    sql += CategoryIds[i] + ",";
                }
            }
            sql += ")\n" +
            "GROUP BY SUPPLIER_ID HAVING COUNT(*) = " + CategoryIds.Count + ") ORDER BY SUPPLIER_NAME ASC ";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Supplier>(dbConnection.dr);
            }
        }

        public List<Supplier> GetAllSuppliersForQuotationSubmission(List<int> CategoryIds, DBConnection dbConnection)
        {
            string sql = "SELECT SUPPLIER_ID,SUPPLIER_NAME FROM SUPPLIER WHERE SUPPLIER_ID IN (\n" +
                            "SELECT SUPPLIER_ID FROM SUPPLIER_CATEGORY WHERE CATEGORY_ID IN (";
            for (int i = 0; i < CategoryIds.Count; i++)
            {
                if (i == CategoryIds.Count - 1)
                {
                    sql += CategoryIds[i];
                }
                else
                {
                    sql += CategoryIds[i] + ",";
                }
            }
            sql += ")\n" +
            "GROUP BY SUPPLIER_ID HAVING COUNT(*) = " + CategoryIds.Count + ") ORDER BY SUPPLIER_NAME ASC ";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Supplier>(dbConnection.dr);
            }
        }

        public List<Supplier> GetAllSuppliersToSendBidEmail(int CategoryId, DBConnection dbConnection)
        {
            string sql = "SELECT SUPPLIER_ID,SUPPLIER_NAME,EMAIL,IS_REGISTERED_SUPPLIER FROM SUPPLIER WHERE (EMAIL IS NOT NULL OR EMAIL !='') AND IS_ACTIVE = 1 AND SUPPLIER_ID IN (\n" +
                            "SELECT SUPPLIER_ID FROM SUPPLIER_CATEGORY WHERE CATEGORY_ID = "+CategoryId+")";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Supplier>(dbConnection.dr);
            }
        }

        public Supplier getSupplierByPOId(int PoId, DBConnection dbConnection) {
            string sql = "SELECT * FROM SUPPLIER WHERE SUPPLIER_ID =(SELECT SUPPLIER_ID FROM PO_MASTER WHERE PO_ID = "+ PoId + ") ";
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<Supplier>(dbConnection.dr);
            }
        }

        public void SaveSupplierEmailForBidSubmission(int prId, int bidId, int supplierId, string supplierName, string emailAddress, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_BID_EMAIL WHERE PR_ID = " + prId + " AND Bid_Id = "+ bidId + " " ;
            var countExistEmail = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExistEmail > 0)
            {
                dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SUPPLIER_BID_EMAIL WHERE PR_ID = " + prId + " AND Bid_Id = " + bidId + " AND SUPPLIER_ID= "+supplierId+" ";
                dbConnection.cmd.ExecuteNonQuery();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_BID_EMAIL(PR_ID,Bid_Id,SUPPLIER_ID,SUPPLIER_NAME,EMAIL)" +
                                               " VALUES("+prId+","+bidId+","+supplierId+",'"+supplierName+"' ,'"+emailAddress+"')";
               dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_BID_EMAIL(PR_ID,Bid_Id,SUPPLIER_ID,SUPPLIER_NAME,EMAIL)" +
                                                 " VALUES(" + prId + "," + bidId + "," + supplierId + ",'" + supplierName + "' ,'" + emailAddress + "')";
                dbConnection.cmd.ExecuteNonQuery();
            }
        }

        public List<SupplierBidEmail> GetSupplierAssignedToBid(int prId, int bidId, DBConnection dbConnection)
        {
            string sql = "SELECT * FROM " + dbLibrary + ".SUPPLIER_BID_EMAIL WHERE PR_ID = " + prId + " AND Bid_Id = " + bidId + " "; 

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBidEmail>(dbConnection.dr);
            }
        }

        public void SaveSupplierBidEmailContact(int prId, string contactName, string ContactNo,string Title, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_BID_EMAIL_CONTACT WHERE  PR_ID = " + prId + " AND Contact_Name='" + contactName + "' ";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (existCount == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_BID_EMAIL_CONTACT(PR_ID,USER_ID ,Contact_Name,Contact_No,TITLE)" +
                                                " VALUES(" + prId + ", 0 ,'" + contactName + "','" + ContactNo + "','"+Title+"')";

                dbConnection.cmd.ExecuteNonQuery();
            }
        }

        public List<SupplierBidEmailContact> GetSupplierBidEmailContact(int prId, DBConnection dbConnection)
        {
            string sql = "SELECT * FROM " + dbLibrary + ".SUPPLIER_BID_EMAIL_CONTACT WHERE PR_ID = " + prId + " ";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBidEmailContact>(dbConnection.dr);
            }
        }

        public void DeleteSupplierBidEmailContact(int prId, DBConnection dbConnection)
        {           
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SUPPLIER_BID_EMAIL_CONTACT WHERE PR_ID = " + prId + " ";
            dbConnection.cmd.ExecuteNonQuery();              
        }

        public List<CommonReference> getCountry(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COUNTRY ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CommonReference>(dbConnection.dr);
            }
        }

        public List<SupplierAgent> getSupplierAgent(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_AGENT ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierAgent>(dbConnection.dr);
            }
        }

        public List<Supplier> FetchSupplierAgent(DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER WHERE SUPPLIER_TYPE = 2 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Supplier>(dbConnection.dr);
            }
        }

        public int insertSupplierAgentDetails(int supplierId, string name, string address, string email, string contactNo, DBConnection dbConnection)
        {
            int status = 0;
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_AGENT WHERE  SUPPLIER_ID = " + supplierId + " AND AGENT_NAME='"+ name + "' ";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            int maxAgentId = 0;
            if (existCount == 0)
            {
                dbConnection.cmd.CommandText = "SELECT MAX(AGENT_ID) FROM " + dbLibrary + ".SUPPLIER_AGENT";
                if (dbConnection.cmd.ExecuteScalar() != null)
                {
                    maxAgentId = Convert.ToInt32(dbConnection.cmd.ExecuteScalar().ToString());
                }
                maxAgentId = maxAgentId + 1;
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_AGENT (SUPPLIER_ID , AGENT_ID , AGENT_NAME  , ADDRESS  , EMAIL  , CONTACT_NO )" +
                                               " VALUES (" + supplierId + "," + maxAgentId + ",'" + name + "','" + address + "','" + email + "'," + contactNo + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return status;
            }
        }

        public int updateSupplierAgentDetails(int agentId, string name, string address, string email, string contactNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_AGENT" +
                                           " WHERE AGENT_ID =" + agentId + "";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount> 0)
            {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_AGENT " +
                                               " SET  AGENT_NAME  ='" + name + "', " +
                                               " ADDRESS  ='" + address + "' , " +
                                               " EMAIL  ='" + email + "' , " +
                                               " CONTACT_NO  =" + contactNo + " " +
                                               " WHERE AGENT_ID =" + agentId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int deleteSupplierAgentDetails(int agentId, string name, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SUPPLIER_AGENT " +
                                           "WHERE AGENT_ID =" + agentId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SupplierAgent> getSupplierAgentBySupplierId(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_AGENT WHERE SUPPLIER_ID =" + supplierId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierAgent>(dbConnection.dr);
            }
        }

        public void SaveUnRegisteredSupplier(int prId, SupplierBidEmailContact unregisteredSuppliers, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".UNREGISTERED_SUPPLIER WHERE  PR_ID = " + prId + " AND SUPPLIER_NAME='" + unregisteredSuppliers.ContactOfficer + "' ";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (existCount == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".UNREGISTERED_SUPPLIER(PR_ID, SUPPLIER_NAME ,EMAIL)" +
                                           " VALUES(" + prId + ",'" + unregisteredSuppliers.ContactOfficer + "','" + unregisteredSuppliers.Email + "')";
                dbConnection.cmd.ExecuteNonQuery();
            }
        }

        public List<SupplierBidEmailContact> GetUnRegisteredSuppliersByPrId(int prId, DBConnection dbConnection)
        {
            string sql = "SELECT PR_ID , SUPPLIER_NAME AS Contact_Name,EMAIL FROM " + dbLibrary + ".UNREGISTERED_SUPPLIER WHERE PR_ID = " + prId + " ";

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBidEmailContact>(dbConnection.dr);
            }
        }

        public void SaveSupplierAgent2(int supplierId, List<int> supplierIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            string sql = string.Empty;
            if (supplierIds.Count > 0)
            {
                for (int i = 0; i < supplierIds.Count; i++)
                {
                    sql += "INSERT INTO SUPPLIER_AGENT_2(SUPPLIER_ID,SUPPLIER_AGENT_ID,IS_ACTIVE) " +
                                                    " VALUES(" + supplierId + ", " + supplierIds[i] + ", 1); \n";
                }
                dbConnection.cmd.CommandText = sql;
                dbConnection.cmd.ExecuteNonQuery();
            }
        }

        public List<SupplierAgent2> getSupplierAgent2(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_AGENT_2 AS A " +
                                           "WHERE A.SUPPLIER_ID = " + supplierId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierAgent2>(dbConnection.dr);
            }
        }

        public void deleteSupplier(int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER SET IS_ACTIVE=0  WHERE SUPPLIER_ID=" + supplierId + "";
            dbConnection.cmd.ExecuteNonQuery();
        }

        public void UpdateSupplierAgent2(int supplierId, List<int> supplierIds, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "DELETE FROM SUPPLIER_AGENT_2 WHERE SUPPLIER_ID = " + supplierId + ";";

            for (int i = 0; i < supplierIds.Count; i++)
            {
                dbConnection.cmd.CommandText += "INSERT INTO SUPPLIER_AGENT_2(SUPPLIER_ID,SUPPLIER_AGENT_ID,IS_ACTIVE) VALUES(" + supplierId + ", " + supplierIds[i] + ", 1); \n";
            }

            dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Supplier> FetchClearingAgentList(DBConnection dbConnection) {
            string sql = "SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER WHERE SUPPLIER_TYPE = 3";
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Supplier>(dbConnection.dr);
            }
        }
    }
}
