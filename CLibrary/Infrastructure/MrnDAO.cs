using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface MrnDAOInterface
    {
        int saveMrn(MrnMaster mrn, DBConnection dbConnection);
        int ApproveOrRejectMrn(int mrnID,int isApproved, DBConnection dbConnection);
        int DeleteMRND(int mrndID, DBConnection dbConnection);
        int addMRND(MrnDetails mrnd, DBConnection dbConnection);
        int updateMRN(MrnMaster mrn, DBConnection dbConnection);
        int updateMRND(MrnDetails mrnd, DBConnection dbConnection);
        int updateMRNDIssuedQty(int mrndID, decimal issuedQty, DBConnection dbConnection);
        int updateMRNDReceivedQty(int mrndID, decimal issuedQty, DBConnection dbConnection);
        int changeMRNDStaus(int mrndID, int status, DBConnection dbConnection);
        int changeMRNStaus(int mrnID, int status, DBConnection dbConnection);
        List<MrnMaster> fetchApprovedOrRejectedMrnList(int companyID,int isApproved, DBConnection dbConnection);
        List<MrnMaster> fetchMrnList(int subDepartmentID, DBConnection dbConnection);
        List<MrnDetails> fetchMrnDList(int mrnID, DBConnection dbConnection);
        List<MrnMaster> fetchSubmittedMrnList(int companyID, DBConnection dbConnection);
        List<MrnDetails> fetchSubmittedMrnDList(int mrnID, int companyID, DBConnection dbConnection);
        MrnMaster getMRNM(int mrnID, DBConnection dbConnection);
        MrnDetails getMRND(int mrndID, DBConnection dbConnection);
        int updateMRNAfterIssue(int mrnID, DBConnection dbConnection);
        List<MrnMaster> AdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype, DBConnection dbConnection);

        List<MrnMaster> FetchMrnsByCreatedUser(int CreatedBy, DateTime date, DBConnection dbConnection);
        MrnMaster FetchMyMrnByBasicSearchByMrId(int createdBy, int mrnId, DBConnection dbConnection);
    }
    class MrnDAO : MrnDAOInterface
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int addMRND(MrnDetails mrnd, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_DETAILS (MRN_ID,ITEM_ID,DESCRIPTION,REQUESTED_QTY,RECEIVED_QTY,STATUS,IS_ACTIVE) OUTPUT INSERTED.MRND_ID VALUES " +
                                           "(" + mrnd.MrnId + "," + mrnd.ItemId + ",'" + mrnd.Description + "'," + mrnd.RequestedQty + ",0,0,1)";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ApproveOrRejectMrn(int mrnID,int isApproved, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER SET IS_APPROVED= " + isApproved + " WHERE MRN_ID=" + mrnID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int changeMRNDStaus(int mrndID, int status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS SET STATUS= " + status + " WHERE MRND_ID=" + mrndID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int changeMRNStaus(int mrnID, int status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER SET STATUS= " + status + " WHERE MRN_ID=" + mrnID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteMRND(int mrndID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS SET IS_ACTIVE=0 WHERE MRND_ID=" + mrndID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<MrnMaster> fetchApprovedOrRejectedMrnList(int companyID, int isApproved, DBConnection dbConnection)
        {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "WHERE MRNM.COMPANY_ID=" + companyID + " AND MRNM.IS_ACTIVE=1 AND MRNM.IS_APPROVED="+isApproved+" ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }
            if (mrnMasters.Count > 0)
            {
                foreach (MrnMaster mrnMaster in mrnMasters)
                {
                    mrnMaster.MrnDetails = fetchMrnDList(mrnMaster.MrnID, dbConnection);
                }
            }
            return mrnMasters;
        }

        public List<MrnDetails> fetchMrnDList(int mrnID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_DETAILS AS MRND "+
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS_MASTER) AS AIM ON MRND.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER) AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER) ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "WHERE MRND.MRN_ID =" + mrnID+ " AND MRND.IS_ACTIVE=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr);
            }
        }

        public List<MrnMaster> fetchMrnList(int subDepartmentID, DBConnection dbConnection)
        {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,USER_NAME,FIRST_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "WHERE MRNM.SUB_DEPARTMENT_ID=" + subDepartmentID + " AND MRNM.IS_ACTIVE=1 ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters= dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }
            if (mrnMasters.Count > 0)
            {
                foreach (MrnMaster mrnMaster in mrnMasters)
                {
                    mrnMaster.MrnDetails=fetchMrnDList(mrnMaster.MrnID,dbConnection);
                }
            }
            return mrnMasters;

        }

        public List<MrnDetails> fetchSubmittedMrnDList(int mrnID, int companyID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MRND.MRND_ID,MRND.MRN_ID,MRND.ITEM_ID,MRND.DESCRIPTION,MRND.REQUESTED_QTY,MRND.ISSUED_QTY,MRND.RECEIVED_QTY,MRND.STATUS,MRND.IS_ACTIVE,AIM.ITEM_NAME,CIM.UNIT_PRICE,ISCM.SUB_CATEGORY_ID,ISCM.SUB_CATEGORY_NAME,ICM.CATEGORY_ID,ICM.CATEGORY_NAME,ISNULL(CIM.AVAILABLE_QTY,0) AS A_QTY FROM " + dbLibrary + ".MRN_DETAILS AS MRND " +
                                            "INNER JOIN (SELECT ITEM_ID, SUB_CATEGORY_ID, ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS_MASTER) AS AIM " +
                                            "ON MRND.ITEM_ID = AIM.ITEM_ID "+
                                            "LEFT JOIN(SELECT AVAILABLE_QTY, ITEM_ID, COMPANY_ID,(NULLIF(STOCK_VALUE,0)/NULLIF(AVAILABLE_QTY,0)) AS UNIT_PRICE FROM " + dbLibrary + ".COMPANY_INVENTORY_MASTER WHERE COMPANY_ID = " + companyID + ") AS CIM " +
                                            "ON MRND.ITEM_ID = CIM.ITEM_ID "+
                                            "INNER JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME, CATEGORY_ID FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER) AS ISCM " +
                                            "ON AIM.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID "+
                                            "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER) ICM " +
                                            "ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID "+
                                            "WHERE MRND.MRN_ID = " + mrnID + " AND MRND.IS_ACTIVE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr);
            }
        }

       

        public List<MrnMaster> fetchSubmittedMrnList(int companyID, DBConnection dbConnection)
        {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "WHERE MRNM.COMPANY_ID=" + companyID + " AND MRNM.IS_ACTIVE=1 AND MRNM.STATUS=0 AND MRNM.IS_APPROVED=1 ORDER BY MRNM.EXPECTED_DATE DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }
            //if (mrnMasters.Count > 0)
            //{
            //    foreach (MrnMaster mrnMaster in mrnMasters)
            //    {
            //        mrnMaster.MrnDetails = fetchSubmittedMrnDList(mrnMaster.MrnID,companyID, dbConnection);
            //    }
            //}
            return mrnMasters;
        }

        public MrnDetails getMRND(int mrndID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_DETAILS AS MRND " +
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME FROM " + dbLibrary + ".ADD_ITEMS_MASTER) AS AIM ON MRND.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM " + dbLibrary + ".ITEM_SUB_CATEGORY_MASTER) AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY_MASTER) ICM ON ISCM.SUB_CATEGORY_ID = ICM.CATEGORY_ID " +
                "WHERE MRND.MRND_ID =" + mrndID;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr).First();
            }
        }

        public MrnMaster getMRNM(int mrnID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            MrnMaster mrnMaster = new MrnMaster();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,USER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "WHERE MRNM.MRN_ID=" + mrnID + " AND MRNM.IS_ACTIVE=1 ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMaster= dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr).First();
            }
            mrnMaster.MrnDetails = fetchMrnDList(mrnMaster.MrnID, dbConnection);
            return mrnMaster;
        }

        public int saveMrn(MrnMaster mrn, DBConnection dbConnection)
        {
            string query = "DECLARE @OutputTbl TABLE (ID INT) "+
                            "INSERT INTO " + dbLibrary + ".MRN_MASTER(COMPANY_ID,SUB_DEPARTMENT_ID, CREATED_DATETIME,DESCRIPTION,EXPECTED_DATE, CREATED_BY, STATUS,IS_APPROVED,IS_ACTIVE) " +
                            "OUTPUT INSERTED.MRN_ID INTO @OutputTbl(ID) "+
                            "VALUES(" + mrn.CompanyID + "," + mrn.SubDepartmentID + ", '" +  LocalTime.Now + "','" + mrn.Description + "','" + mrn.ExpectedDate + "'," + mrn.CreatedBy + ",0,0,1) " +
                            "INSERT INTO " + dbLibrary + ".MRN_DETAILS (MRN_ID,ITEM_ID,DESCRIPTION,REQUESTED_QTY,RECEIVED_QTY,STATUS,IS_ACTIVE) VALUES ";
            for(int i=0;i<mrn.MrnDetails.Count;i++)
            {
                MrnDetails mrnd = mrn.MrnDetails[i];
                if(i== mrn.MrnDetails.Count-1)
                {
                    query += "((SELECT ID FROM @OutputTbl)," + mrnd.ItemId + ",'" + mrnd.Description + "'," + mrnd.RequestedQty + ",0,0,1)";
                }
                else
                {
                    query += "((SELECT ID FROM @OutputTbl)," + mrnd.ItemId + ",'" + mrnd.Description + "'," + mrnd.RequestedQty + ",0,0,1),";
                }
            }
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = query;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        
        public int updateMRN(MrnMaster mrn, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER SET DESCRIPTION= '" + mrn.Description + "', EXPECTED_DATE= '" + mrn.ExpectedDate + "' WHERE MRN_ID=" + mrn.MrnID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateMRNAfterIssue(int mrnID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF NOT EXISTS (SELECT * FROM MRN_DETAILS WHERE MRN_ID= " + mrnID+ " AND STATUS !=(SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='FULLYISSUE') AND STATUS !=(SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='TERM')) " +
                                            "BEGIN "+
                                            //"   UPDATE MRN_MASTER SET STATUS = 1 WHERE MRN_ID = "+mrnID+" "+
                                            "   UPDATE MRN_MASTER SET STATUS = (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='COMP') WHERE MRN_ID = " + mrnID + " " +
                                            "   SELECT 1 " +
                                            "END " +
                                            "ELSE "+
                                            "BEGIN " +
                                            "   SELECT 1 " +
                                            "END ";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int updateMRND(MrnDetails mrnd, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS SET DESCRIPTION= '" + mrnd.Description + "', REQUESTED_QTY= " + mrnd.RequestedQty + " WHERE MRND_ID=" + mrnd.Mrnd_ID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateMRNDIssuedQty(int mrndID, decimal issuedQty, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS SET ISSUED_QTY= ISSUED_QTY+" + issuedQty + " WHERE MRND_ID=" + mrndID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateMRNDReceivedQty(int mrndID, decimal receivedQty, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS SET RECEIVED_QTY= RECEIVED_QTY+" + receivedQty + " WHERE MRND_ID = "+ mrndID + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public List<MrnMaster> AdvanceSearch(int companyId,int categoryId,int subDepartmentID,int serchtype,string searchkey,string usertype, DBConnection dbConnection)
        {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "[dbo].[usp_MRNAdvanceSearch]";
            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dbConnection.cmd.Parameters.AddWithValue("@CompanyId", companyId);
            dbConnection.cmd.Parameters.AddWithValue("@categoryId", categoryId);
            dbConnection.cmd.Parameters.AddWithValue("@subDepartmentId", subDepartmentID);
            dbConnection.cmd.Parameters.AddWithValue("@serchWord", searchkey);
            dbConnection.cmd.Parameters.AddWithValue("@usertype", usertype);
            dbConnection.cmd.Parameters.AddWithValue("@searchtype", serchtype);
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }
            if (mrnMasters.Count > 0)
            {
                foreach (MrnMaster mrnMaster in mrnMasters)
                {
                    mrnMaster.MrnDetails = fetchMrnDList(mrnMaster.MrnID, dbConnection);
                }
            }
            return mrnMasters;

        }

        public List<MrnMaster> FetchMrnsByCreatedUser(int CreatedBy, DateTime date, DBConnection dbConnection) {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.CREATED_BY =" + CreatedBy + " AND MRNM.IS_ACTIVE=1 AND MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);

            }
            return mrnMasters;

        }

        public MrnMaster FetchMyMrnByBasicSearchByMrId(int createdBy, int mrnId, DBConnection dbConnection)
        {
            MrnMaster mrnMaster = new MrnMaster();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS W ON MRNM.WAREHOUSE_ID=W.WAREHOUSE_ID " +
                "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                "LEFT JOIN (SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON IC.CATEGORY_ID = MRNM.MRN_CATEGORY_ID " +
                "LEFT JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME FROM ITEM_SUB_CATEGORY) AS IBC ON IBC.SUB_CATEGORY_ID = MRNM.MRN_SUB_CATEGORY_ID " +
                "WHERE MRNM.CREATED_BY =" + createdBy + " AND MRNM.IS_ACTIVE=1 AND MRNM.MRN_ID  =" + mrnId + "  ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
               return  dataAccessObject.GetSingleOject<MrnMaster>(dbConnection.dr);
            }
        }

        
    }
}
