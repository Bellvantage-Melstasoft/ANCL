using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;
using System.Data;
namespace CLibrary.Infrastructure
{
    public interface MRNDetailDAO
    {
        int SaveMRNDetails(int MrnId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId, DBConnection dbConnection);
        List<MrnDetails> FetchDetailsRejectedMRN(int MrnId, int companyId, DBConnection dbConnection);

        List<MrnDetails> FetchMRN_DetailsByMRNIdList(int MrnId, DBConnection dbConnection);

        int DeleteMRNDetailByMRNIDAndItemId(int MrnId, int itemId, DBConnection dbConnection);

        int UpdateMRNDetails(int MrnId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId, DBConnection dbConnection);

        List<MrnDetails> GetMrnDetailsByMRNid(int mrnID, int companyId, DBConnection dbConnection);

        PR_Master GetPRByMRNid(int mrnID, int companyId, DBConnection dbConnection);
        void UpdateMrnDetailIssueStock(MrnDetailsV2 mrnDetail, DBConnection dbConnection);
        void TerminateItem(int mrndId, int itemId, string remark,int updatedBy, int mrnID, DBConnection dbConnection);
        List<MrnDetails> FetchFullyIssuedMrnDetails(int MrnId, DBConnection dbConnection);
        MrnDetails GetMrnDetailsByMrndId(int mrndID, DBConnection dbConnection);
        string GetMrnStatusByStatusId(int statusID, DBConnection dbConnection);
    }

    public class MRNDetailDAOImpl : MRNDetailDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveMRNDetails(int MrnId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".MRN_DETAILS WHERE  MRN_ID = " + MrnId + " AND  ITEM_ID = " + ItemId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_DETAILS (MRN_ID, ITEM_ID, DESCRIPTION, REQUESTED_QTY, ISSUED_QTY, RECEIVED_QTY, STATUS, IS_ACTIVE, MEASUREMENT_ID,UNIT, REPLACEMENT, PURPOSE,REMARKS, ESTIMATED_AMOUNT,  SAMPLE_PROVIDED) VALUES ( " + MrnId + ", " + ItemId + " , '" + ItemDescription + "', '" + ItemQuantity + "', " + 0 + ", " + 0 + ", " + 0 + ", " + IsActive + "," + MeasurementId + "," + MeasurementId + "," + Replacement + ",'" + Purpose + "','" + Remarks + "'," + EstimatedAmount + "," + FileSampleProvided + ");";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();
                dbConnection.cmd.CommandText = "SELECT MAX(MRND_ID) AS cnt FROM " + dbLibrary + ".MRN_DETAILS WHERE  MRN_ID = " + MrnId + " AND  ITEM_ID = " + ItemId + "";
                int MRND_ID = Int32.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                return MRND_ID;
            }
            else
            {
                return -1;
            }


        }

        public List<MrnDetails> FetchDetailsRejectedMRN(int MrnId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_DETAILS AS PD INNER JOIN " + dbLibrary + ".MRN_MASTER AS PM ON PM.MRN_ID = PD.MRN_ID INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID =  AI.CATEGORY_ID INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON  ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID WHERE  PD.MRN_ID=" + MrnId + "  AND PD.IS_ACTIVE=1 AND AI.COMPANY_ID=" + companyId + " AND ISC.COMPANY_ID = " + companyId + " AND IC.COMPANY_ID = " + companyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr);
            }
        }

        public List<MrnDetails> FetchMRN_DetailsByMRNIdList(int MrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".MRN_DETAILS AS PD INNER JOIN " + dbLibrary + ".MRN_MASTER AS PM ON PM.MRN_ID = PD.MRN_ID INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = AI.CATEGORY_ID INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON  ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID WHERE  PD.IS_ACTIVE= 1 AND PD.MRN_ID=" + MrnId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr);
            }
        }

        public int DeleteMRNDetailByMRNIDAndItemId(int MrnId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".MRN_DETAILS WHERE MRN_ID = " + MrnId + " AND ITEM_ID =" + itemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int UpdateMRNDetails(int MrnId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            if (oldItemId == ItemId)
            {
                dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".MRN_DETAILS SET  ITEM_ID = " + ItemId + ",  DESCRIPTION = '" + ItemDescription + "',  IS_ACTIVE = " + IsActive + ", REPLACEMENT = " + Replacement + ",REQUESTED_QTY = " + ItemQuantity + ",PURPOSE = '" + Purpose + "',ESTIMATED_AMOUNT=" + EstimatedAmount + " , SAMPLE_PROVIDED =" + FileSampleProvided + " , REMARKS ='" + Remarks + "', MEASUREMENT_ID = " + MeasurementId + " WHERE  MRN_ID = " + MrnId + " AND  ITEM_ID = " + oldItemId + ";";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM MRN_DETAILS WHERE  MRN_ID = " + MrnId + " AND  ITEM_ID = " + oldItemId + " ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    dbConnection.cmd.CommandText = "UPDATE MRN_DETAILS SET  ITEM_ID = " + ItemId + ", UNIT = " + Unit + ", DESCRIPTION = '" + ItemDescription + "', IS_ACTIVE = " + IsActive + ", REPLACEMENT = " + Replacement + ",REQUESTED_QTY = " + ItemQuantity + ",PURPOSE = '" + Purpose + "',ESTIMATED_AMOUNT=" + EstimatedAmount + "  WHERE  MRN_ID = " + MrnId + " AND  ITEM_ID = " + oldItemId + ";";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }

            }
        }

        public List<MrnDetails> GetMrnDetailsByMRNid(int mrnID, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_DETAILS AS MRND\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + companyId + ") AS AIM ON MRND.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "WHERE MRND.MRN_ID = " + mrnID + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr);
            }
        }

        public PR_Master GetPRByMRNid(int mrnID, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM " +
                                           "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON PM.CREATED_BY = CL.USER_ID\n" +
                                           " WHERE MRNREFERENCE_NO = " + mrnID + " " +
                                           " AND DEPARTMENT_ID = " + companyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PR_Master>(dbConnection.dr);
            }
        }

        public List<MrnDetails> FetchFullyIssuedMrnDetails(int MrnId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

           // dbConnection.cmd.CommandText = "SELECT * FROM MRN_DETAILS WHERE MRN_ID = "+ MrnId + " AND STATUS IN  (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE IN ('FULLYISSUE'))";
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_DETAILS WHERE MRN_ID = " + MrnId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MrnDetails>(dbConnection.dr);
            }
        }


        public void UpdateMrnDetailIssueStock(MrnDetailsV2 mrnDetail, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS "+
                                           " SET ISSUED_QTY= ISSUED_QTY+" + mrnDetail.IssuedQty + " , STATUS= " + mrnDetail.Status + " " +
                                           " WHERE MRND_ID=" + mrnDetail.MrndId;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public void TerminateItem(int mrndId, int itemId,string remark,int updatedBy, int mrnID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_DETAILS " +
                                           " SET IS_TERMINATED= 1  , TERMINATED_BY= " + updatedBy + ", TERMINATION_REMARKS='"+remark+"' ," +
                                           " TERMINATED_ON ='" + LocalTime.Now + "' , STATUS = (SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='TERM') " +
                                           " WHERE MRND_ID=" + mrndId + " AND ITEM_ID= "+itemId+" ";

            dbConnection.cmd.CommandText += "IF NOT EXISTS (SELECT * FROM MRN_DETAILS WHERE MRN_ID= " + mrnID + " AND STATUS !=(SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='FULLYISSUE') AND STATUS !=(SELECT MRN_DETAILS_STATUS_ID FROM DEF_MRN_DETAILS_STATUS WHERE STATUS_CODE='TERM')) " +
                                           "BEGIN " +
                                            "   UPDATE MRN_MASTER SET STATUS = (SELECT MRN_STATUS_ID FROM DEF_MRN_STATUS WHERE STATUS_CODE='COMP') WHERE MRN_ID = " + mrnID + " " +

                                           "END ";


            dbConnection.cmd.ExecuteNonQuery();
        }

        public MrnDetails GetMrnDetailsByMrndId(int mrndID,  DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRN_DETAILS AS MRND\n" +
                                             "WHERE MRND.MRND_ID = " + mrndID + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MrnDetails>(dbConnection.dr);
            }
        }

        public string GetMrnStatusByStatusId(int statusID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MRND.STATUS_NAME FROM DEF_MRN_STATUS AS MRND\n" +
                                             "WHERE MRND.MRN_STATUS_ID = " + statusID + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery().ToString();
        }

    }
}
