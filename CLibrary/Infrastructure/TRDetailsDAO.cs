using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface TRDetailsDAO
    {
        List<TR_Details> fetchTRDList(int trId, int CompanyId, DBConnection dbConnection);
        int updateTRD(TR_Details trd, DBConnection dbConnection);
        int addTRD(TR_Details TRD, DBConnection dbConnection);
        int DeleteTRD(int trdid, DBConnection dbConnection);
        TR_Details GetTrdTerminationDetails(int trdId, DBConnection dbConnection);
        int TerminateTRD(int TrID, int TrdID, int TerminatedBy, string Remarks, DBConnection dbConnection);
        int changeTRDStaus(int trdID, int status, DBConnection dbConnection);
        int updateTRdIssuedQty(int trdId, decimal issuedQty, DBConnection dbConnection);
        int updateTRAfterIssue(int TRID, DBConnection dbConnection);
        List<TR_Details> fetchSubmittedTrDList(int trId, int companyID, DBConnection dbConnection);
        int updateTRDReceivedQty(int trdID, decimal receivedQty, DBConnection dbConnection);
        List<TR_Details> fetchTrdItemList(int TrID, int CompanyId, DBConnection dbConnection);
        TR_Details GetTrd(int trdId, DBConnection dbConnection);

    }
    public class TRDetailsSQLImpl : TRDetailsDAO
    {
        public List<TR_Details> fetchTRDList(int trId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TR_DETAILS AS TR  " +
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME, MEASUREMENT_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON TR.ITEM_ID=AIM.ITEM_ID  " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = AIM.MEASUREMENT_ID " +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "WHERE TR.TR_ID =" + trId + " AND TR.IS_ACTIVE=1 ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TR_Details>(dbConnection.dr);
            }
        }

        public int updateTRD(TR_Details trd, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE TR_DETAILS SET DESCRIPTION= '" + trd.Description + "', REQUESTED_QTY= " + trd.RequestedQTY + ",MEASUREMENT_ID  =" + trd.MeasurementId + "  WHERE TRD_ID=" + trd.TRDId;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int addTRD(TR_Details TRD, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO TR_DETAILS (TR_ID,ITEM_ID,DESCRIPTION,REQUESTED_QTY,MEASUREMENT_ID ,RECEIVED_QTY,STATUS,IS_ACTIVE) OUTPUT INSERTED.TRD_ID VALUES " +
                                           "(" + TRD.TRId + "," + TRD.ItemID + ",'" + TRD.Description + "'," + TRD.RequestedQTY + "," + TRD.MeasurementId + ",0,0,1)";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTRD(int trdid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE TR_DETAILS SET IS_ACTIVE=0 WHERE TRD_ID=" + trdid;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public TR_Details GetTrdTerminationDetails(int trdId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT TR.*,CLT.TERMINATED_BY_NAME,CLT.TERMINATED_BY_SIGNATURE FROM TR_DETAILS AS TR " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME, DIGITAL_SIGNATURE AS TERMINATED_BY_SIGNATURE FROM COMPANY_LOGIN) AS CLT ON TR.TERMINATED_BY = CLT.USER_ID\n" +
                                            "WHERE TR.TRD_ID = " + trdId;


            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<TR_Details>(dbConnection.dr);
            }
        }
        
        public int TerminateTRD(int TrID, int TrdID, int TerminatedBy, string Remarks, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE TR_DETAILS SET IS_TERMINATED=1, TERMINATED_BY=" + TerminatedBy + ", TERMINATED_DATE='" + LocalTime.Now + "', TERMINATED_REASON='" + Remarks.ProcessString() + "',STATUS=6 WHERE TRD_ID=" + TrdID + " \n");
            sql.Append(" \n");
            sql.Append("INSERT INTO TR_DETAIL_STATUS_LOG (TRD_ID, STATUS, LOGGED_DATE, USER_ID) \n");
            sql.Append("VALUES(" + TrdID + ",4,'" + LocalTime.Now + "'," + TerminatedBy + ") \n");
            sql.Append(" \n");
            sql.Append("IF NOT EXISTS(SELECT TRD_ID FROM TR_DETAILS WHERE STATUS NOT IN(6,3) AND TR_ID=" + TrID + ") \n");
            sql.Append("IF EXISTS(SELECT TRD_ID FROM TR_DETAILS WHERE STATUS =3 AND TR_ID=" + TrID + ") \n");
            sql.Append("	UPDATE TR_MASTER SET STATUS=1 WHERE TR_ID=" + TrID + " \n");
            sql.Append("ELSE \n");
            sql.Append("	UPDATE TR_MASTER SET IS_TERMINATED=1, TERMINATED_BY=" + TerminatedBy + ", TERMINATED_DATE='" + LocalTime.Now + "', TERMINATED_REASON='" + Remarks.ProcessString() + "', STATUS=2 WHERE TR_ID=" + TrID + " \n");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();

        }
        public int changeTRDStaus(int trdID, int status, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE TR_DETAILS SET STATUS= " + status + " WHERE TRD_ID=" + trdID + "; \n";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateTRdIssuedQty(int trdId, decimal issuedQty, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE TR_DETAILS SET ISSUED_QTY= ISNULL(ISSUED_QTY,0)+" + issuedQty + " WHERE TRD_ID=" + trdId;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateTRAfterIssue(int TRID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF NOT EXISTS (SELECT * FROM TR_DETAILS WHERE TR_ID= " + TRID + " AND STATUS NOT IN (3,6)) \n" +
                                            "UPDATE TR_MASTER SET STATUS = 1 WHERE TR_ID = " + TRID + " \n" +
                                            "ELSE \n" +
                                            "UPDATE TR_MASTER SET STATUS = 0 WHERE TR_ID =  " + TRID + " ";

            return dbConnection.cmd.ExecuteNonQuery();
        }
        public List<TR_Details> fetchSubmittedTrDList(int trId, int companyID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT TRD.TRD_ID,TRD.TR_ID,TRD.ITEM_ID,TRD.DESCRIPTION,TRD.REQUESTED_QTY,TRD.MEASUREMENT_ID, UN.MEASUREMENT_SHORT_NAME,TRD.ISSUED_QTY,TRD.RECEIVED_QTY,TRD.STATUS,TRD.IS_ACTIVE,AIM.ITEM_NAME,CIM.UNIT_PRICE,ISCM.SUB_CATEGORY_ID,ISCM.SUB_CATEGORY_NAME,ICM.CATEGORY_ID,ICM.CATEGORY_NAME,ISNULL(CIM.AVAILABLE_QTY,0) AS A_QTY FROM TR_DETAILS AS TRD " +
                                            "INNER JOIN (SELECT ITEM_ID, SUB_CATEGORY_ID, ITEM_NAME, MEASUREMENT_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + companyID + ") AS AIM " +
                                            "ON TRD.ITEM_ID = AIM.ITEM_ID " +
                                            "LEFT JOIN(SELECT AVAILABLE_QTY, ITEM_ID,(NULLIF(STOCK_VALUE,0)/NULLIF(AVAILABLE_QTY,0)) AS UNIT_PRICE FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = (SELECT TO_WAREHOUSE_ID FROM TR_MASTER WHERE TR_ID=" + trId + ")) AS CIM " +
                                            "ON TRD.ITEM_ID = CIM.ITEM_ID " +
                                            "LEFT JOIN UNIT_MEASUREMENT AS UN ON UN.MEASUREMENT_ID = AIM.MEASUREMENT_ID \n" +
                                            "INNER JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + companyID + ") AS ISCM " +
                                            "ON AIM.SUB_CATEGORY_ID = ISCM.SUB_CATEGORY_ID " +
                                            "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + companyID + ") ICM " +
                                            "ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                                            "WHERE TRD.TR_ID = " + trId + " AND TRD.IS_ACTIVE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TR_Details>(dbConnection.dr);
            }
        }

        public int updateTRDReceivedQty(int trdID, decimal receivedQty, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE TR_DETAILS SET RECEIVED_QTY= RECEIVED_QTY+" + receivedQty + " WHERE TRD_ID=" + trdID;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TR_Details> fetchTrdItemList(int TrID, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TR_DETAILS AS TRD " +
                "INNER JOIN (SELECT ITEM_ID,SUB_CATEGORY_ID,ITEM_NAME, MEASUREMENT_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON TRD.ITEM_ID=AIM.ITEM_ID " +
                "INNER JOIN (SELECT SUB_CATEGORY_ID,SUB_CATEGORY_NAME,CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISCM ON AIM.SUB_CATEGORY_ID=ISCM.SUB_CATEGORY_ID " +
                "LEFT JOIN (SELECT MEASUREMENT_ID, MEASUREMENT_SHORT_NAME FROM UNIT_MEASUREMENT) AS UN ON UN.MEASUREMENT_ID = AIM.MEASUREMENT_ID \n" +
                "INNER JOIN (SELECT CATEGORY_ID,CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") ICM ON ISCM.CATEGORY_ID = ICM.CATEGORY_ID " +
                "WHERE TRD.TR_ID =" + TrID + " AND TRD.IS_ACTIVE= 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TR_Details>(dbConnection.dr);
            }
        }

        public TR_Details GetTrd(int trdId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TR_DETAILS WHERE TRD_ID =" + trdId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<TR_Details>(dbConnection.dr);
            }
        }
    }
}
