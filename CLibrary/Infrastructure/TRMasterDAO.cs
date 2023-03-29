using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface TRMasterDAO
    {
        int saveTR(TR_Master trm, DBConnection dbConnection);
        List<TR_Master> FetchtrsByCreatedUser(int companyId, int CreatedBy, DateTime date, int trCode, List<int> toWarehouseIds, List<int> fromWarehouseIds, DBConnection dbConnection);
        List<TR_Master> GetTRListByWarehouseId(List<int> WarehouseId, DateTime date, DBConnection dbConnection);
        List<TR_Master> FetchTRsSByCompanyId(int CompanyId, DateTime date, int trCode, List<int> toWarehouseIds, List<int> fromWarehouseIds, DBConnection dbConnection);
        TR_Master GetTRMasterToView(int TrId, int CompanyId, DBConnection dbConnection);
        List<TR_Master> fetchTRListforApproval(List<int> warehouseIds, DBConnection dbConnection);
        int ApproveOrRejectTR(int TrId, int isApproved, int UserId, string remark, DBConnection dbConnection);
        int TerminateTR(int TrId, int TerminatedBy, string Remarks, DBConnection dbConnection);
        TR_Master getTRM(int trID, DBConnection dbConnection);
        int updateTR(TR_Master tr, DBConnection dbConnection);
        List<TR_Master> fetchSubmittedTRList(List<int> WarehouseIds, DBConnection dbConnection);
        string GetTRCode(int TRId, DBConnection dbConnection);
        List<string> GetTrCodesByTrIds(List<int> TrIds, DBConnection dbConnection);
        List<TR_Master> fetchTrListByCreatedUser(int CompanyId, int CreatedBy, DateTime date, int TRCode, List<int> fromWarehouseIds, List<int> toWarehouseIds, string trStatuts, string approvalStatuts, DBConnection dbConnection);
        List<TR_Master> fetchTrListByCompanyId(int CompanyId, DateTime date, int TRCode, List<int> fromWarehouseIds, List<int> toWarehouseIds, string trStatuts, string approvalStatuts, DBConnection dbConnection);
        List<TR_Master> FetchtrsByCreatedUser(int CreatedBy, DateTime date, DBConnection dbConnection);
        List<TR_Master> FetchTRsSByCompanyId(int CompanyId, DateTime date, DBConnection dbConnection);
    }
        public class TRMasterSQLImpl : TRMasterDAO
        {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
        public List<TR_Master> FetchTRsSByCompanyId(int CompanyId, DateTime date, DBConnection dbConnection)
        {
            List<TR_Master> TRMasters = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TR_MASTER AS TRM  " +
                                            "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON TRM.CREATED_BY=COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS TO_WAREHOUSE_NAME FROM WAREHOUSE ) AS TW ON TRM.TO_WAREHOUSE_ID=TW.WAREHOUSE_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS FROM_WAREHOUSE_NAME FROM WAREHOUSE) AS FW ON TRM.FROM_WAREHOUSE_ID=FW.WAREHOUSE_ID " +
                                            "WHERE TRM.COMPANY_ID = " + CompanyId + " AND TRM.IS_ACTIVE=1 AND MONTH(TRM.CREATED_DATETIME) =" + date.Month + " AND YEAR(TRM.CREATED_DATETIME)= " + date.Year + " ORDER BY TRM.CREATED_DATETIME DESC ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                TRMasters = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
            return TRMasters;

        }

        public int saveTR(TR_Master trm, DBConnection dbConnection)
        {
            string query = "DECLARE @OutputTbl TABLE (ID INT) \n" +
                            "DECLARE @TR_CODE INT \n" +
                            "SELECT @TR_CODE = ISNULL(MAX(TR_CODE),0)+1 FROM TR_MASTER WHERE COMPANY_ID= " + trm.CompanyId + " \n" +
                            "INSERT INTO " + dbLibrary + ".TR_MASTER(COMPANY_ID,TR_CODE,FROM_WAREHOUSE_ID, CREATED_DATETIME,DESCRIPTION,EXPECTED_DATE, CREATED_BY, STATUS,IS_APPROVED,IS_ACTIVE,TO_WAREHOUSE_ID) \n" +
                            "OUTPUT INSERTED.TR_ID INTO @OutputTbl(ID) \n" +
                            "VALUES(" + trm.CompanyId + ",@TR_CODE," + trm.FromWarehouseId + ", '" + LocalTime.Now + "','" + trm.Description + "','" + trm.ExpectedDate + "'," + trm.CreatedBy + ",0,0,1," + trm.ToWarehouseId + ") \n" +
                            "INSERT INTO " + dbLibrary + ".TR_DETAILS (TR_ID,ITEM_ID,DESCRIPTION,REQUESTED_QTY,MEASUREMENT_ID,ISSUED_QTY,RECEIVED_QTY,STATUS,IS_ACTIVE) VALUES \n";
            for (int i = 0; i < trm.TRDetails.Count; i++)
            {
                TR_Details trd = trm.TRDetails[i];
                if (i == trm.TRDetails.Count - 1)
                {
                    query += "((SELECT ID FROM @OutputTbl)," + trd.ItemID + ",'" + trd.Description + "'," + trd.RequestedQTY + "," + trd.MeasurementId + ",0,0,0,1) \n";
                }
                else
                {
                    query += "((SELECT ID FROM @OutputTbl)," + trd.ItemID + ",'" + trd.Description + "'," + trd.RequestedQTY + "," + trd.MeasurementId + ",0,0,0,1), \n";
                }
            }
            query += "INSERT INTO TR_DETAIL_STATUS_LOG \n";
            query += "SELECT TRD_ID,0,'" + LocalTime.Now + "'," + trm.CreatedBy + " FROM TR_DETAILS WHERE TR_ID =(SELECT ID FROM @OutputTbl) \n";
            query += "SELECT @TR_CODE; ";
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = query;
            int X = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            return X;
        }

        public List<TR_Master> FetchtrsByCreatedUser(int companyId, int CreatedBy, DateTime date, int trCode, List<int> toWarehouseIds, List<int> fromWarehouseIds, DBConnection dbConnection)
        {
            List<TR_Master> TRMasters = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM TR_MASTER AS TRM  " +
                                            "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON TRM.CREATED_BY=COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS TO_WAREHOUSE_NAME FROM WAREHOUSE ) AS TW ON TRM.TO_WAREHOUSE_ID=TW.WAREHOUSE_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS FROM_WAREHOUSE_NAME FROM WAREHOUSE) AS FW ON TRM.FROM_WAREHOUSE_ID=FW.WAREHOUSE_ID " +
                                            "WHERE TRM.CREATED_BY =" + CreatedBy + " AND TRM.IS_ACTIVE=1 AND  TRM.COMPANY_ID =" + companyId + " ";


           if (trCode != 0) {
                sql += " AND TRM.TR_CODE =  " + trCode + " ";
            }


            if (date != DateTime.MinValue) {
                sql += " AND MONTH(TRM.CREATED_DATETIME)= " + date.Month.ToString() + " AND YEAR(TRM.CREATED_DATETIME)= " + date.Year.ToString() + " ";
            }

            if (toWarehouseIds.Count > 0) {
                if (toWarehouseIds.Any(wi => wi == 0)) {
                    if (toWarehouseIds.Count == 1) {
                        sql += " AND TRM.TO_WAREHOUSE_ID IS NULL ";
                    }
                    else {
                        sql += " AND (TRM.TO_WAREHOUSE_ID IN  (" + string.Join(",", toWarehouseIds.Where(wi => wi != 0)) + ") OR TRM.TO_WAREHOUSE_ID IS NULL) ";
                    }
                }
                else {
                    sql += " AND TRM.TO_WAREHOUSE_ID IN  (" + string.Join(",", toWarehouseIds) + ") ";
                }
            }

            if (fromWarehouseIds.Count > 0) {
                if (fromWarehouseIds.Any(wi => wi == 0)) {
                    if (fromWarehouseIds.Count == 1) {
                        sql += " AND TRM.FROM_WAREHOUSE_ID IS NULL ";
                    }
                    else {
                        sql += " AND (TRM.FROM_WAREHOUSE_ID IN  (" + string.Join(",", fromWarehouseIds.Where(wi => wi != 0)) + ") OR TRM.FROM_WAREHOUSE_ID IS NULL) ";
                    }
                }
                else {
                    sql += " AND TRM.FROM_WAREHOUSE_ID IN  (" + string.Join(",", fromWarehouseIds) + ") ";
                }
            }

            sql += "ORDER BY TRM.CREATED_DATETIME DESC ";

        
            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                TRMasters = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
            return TRMasters;

        }

        public List<TR_Master> GetTRListByWarehouseId(List<int> WarehouseId, DateTime date, DBConnection dbConnection)
        {
            List<TR_Master> TRMasters = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TR_MASTER AS TRM  " +
                                            "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON TRM.CREATED_BY=COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS TO_WAREHOUSE_NAME FROM WAREHOUSE ) AS TW ON TRM.TO_WAREHOUSE_ID=TW.WAREHOUSE_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS FROM_WAREHOUSE_NAME FROM WAREHOUSE) AS FW ON TRM.FROM_WAREHOUSE_ID=FW.WAREHOUSE_ID " +
                                            "WHERE TRM.FROM_WAREHOUSE_ID IN( " + string.Join(",", WarehouseId) + ") AND TRM.IS_ACTIVE=1 AND MONTH(TRM.CREATED_DATETIME) =" + date.Month + " AND YEAR(TRM.CREATED_DATETIME)= " + date.Year + " ORDER BY TRM.CREATED_DATETIME DESC ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                TRMasters = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
            return TRMasters;

        }

        public List<TR_Master> FetchTRsSByCompanyId(int CompanyId, DateTime date, int trCode, List<int> toWarehouseIds, List<int> fromWarehouseIds, DBConnection dbConnection)
        {
            List<TR_Master> TRMasters = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM TR_MASTER AS TRM  " +
                                            "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON TRM.CREATED_BY=COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS TO_WAREHOUSE_NAME FROM WAREHOUSE ) AS TW ON TRM.TO_WAREHOUSE_ID=TW.WAREHOUSE_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS FROM_WAREHOUSE_NAME FROM WAREHOUSE) AS FW ON TRM.FROM_WAREHOUSE_ID=FW.WAREHOUSE_ID " +
                                            "WHERE TRM.COMPANY_ID = " + CompanyId + " AND TRM.IS_ACTIVE=1 ";


             if (trCode != 0) {
                sql += " AND TRM.TR_CODE =  " + trCode + " ";
            }


            if (date != DateTime.MinValue) {
                sql += " AND MONTH(TRM.CREATED_DATETIME)= " + date.Month.ToString() + " AND YEAR(TRM.CREATED_DATETIME)= " + date.Year.ToString() + " ";
            }

            if (toWarehouseIds.Count > 0) {
                if (toWarehouseIds.Any(wi => wi == 0)) {
                    if (toWarehouseIds.Count == 1) {
                        sql += " AND TRM.TO_WAREHOUSE_ID IS NULL ";
                    }
                    else {
                        sql += " AND (TRM.TO_WAREHOUSE_ID IN  (" + string.Join(",", toWarehouseIds.Where(wi => wi != 0)) + ") OR TRM.TO_WAREHOUSE_ID IS NULL) ";
                    }
                }
                else {
                    sql += " AND TRM.TO_WAREHOUSE_ID IN  (" + string.Join(",", toWarehouseIds) + ") ";
                }
            }

            if (fromWarehouseIds.Count > 0) {
                if (fromWarehouseIds.Any(wi => wi == 0)) {
                    if (fromWarehouseIds.Count == 1) {
                        sql += " AND TRM.FROM_WAREHOUSE_ID IS NULL ";
                    }
                    else {
                        sql += " AND (TRM.FROM_WAREHOUSE_ID IN  (" + string.Join(",", fromWarehouseIds.Where(wi => wi != 0)) + ") OR TRM.FROM_WAREHOUSE_ID IS NULL) ";
                    }
                }
                else {
                    sql += " AND TRM.FROM_WAREHOUSE_ID IN  (" + string.Join(",", fromWarehouseIds) + ") ";
                }
            }

            sql += "ORDER BY TRM.CREATED_DATETIME DESC ";


            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                TRMasters = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
            return TRMasters;

        }
        public TR_Master GetTRMasterToView(int TrId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT TR.*,W.LOCATION AS FROM_LOCATION, WT.LOCATION AS TO_LOCATION, CLC.CREATED_SIGNATURE,CLA.APPROVED_SIGNATURE,CLA.APPROVED_BY_NAME,CLC.CREATED_BY_NAME,CLT.TERMINATED_BY_NAME,CLT.TERMINATED_BY_SIGNATURE,W.PHONE_NO AS FROM_WAREHOUSE_PNO, WT.PHONE_NO AS TO_WAREHOUSE_PNO,W.ADDRESS AS FROM_WAREHOUSE_ADDRESS, WT.ADDRESS AS TO_WAREHOUSE_ADDRESS FROM TR_MASTER AS TR " +
                                            "INNER JOIN WAREHOUSE AS W ON TR.FROM_WAREHOUSE_ID = W.WAREHOUSE_ID " +
                                             "INNER JOIN WAREHOUSE AS WT ON TR.TO_WAREHOUSE_ID = WT.WAREHOUSE_ID " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_BY_NAME, DIGITAL_SIGNATURE AS APPROVED_SIGNATURE FROM COMPANY_LOGIN) AS CLA ON TR.APPROVED_BY = CLA.USER_ID " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME, DIGITAL_SIGNATURE AS CREATED_SIGNATURE FROM COMPANY_LOGIN) AS CLC ON TR.CREATED_BY = CLC.USER_ID " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME, DIGITAL_SIGNATURE AS TERMINATED_BY_SIGNATURE FROM COMPANY_LOGIN) AS CLT ON TR.TERMINATED_BY = CLT.USER_ID " +
                                            "WHERE TR.COMPANY_ID = " + CompanyId + " AND TR_ID = " + TrId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<TR_Master>(dbConnection.dr);
            }
        }

        public List<TR_Master> fetchTRListforApproval(List<int> warehouseIds, DBConnection dbConnection)
        {
            List<TR_Master> tr_Master = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TR_MASTER AS TRM " +
                                        "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON TRM.CREATED_BY = COLOG.USER_ID " +
                                        "LEFT JOIN(SELECT WAREHOUSE_ID, LOCATION AS TO_WAREHOUSE_NAME FROM WAREHOUSE) AS TW ON TRM.TO_WAREHOUSE_ID = TW.WAREHOUSE_ID " +
                                        "LEFT JOIN(SELECT WAREHOUSE_ID, LOCATION AS FROM_WAREHOUSE_NAME FROM WAREHOUSE) AS FW ON TRM.FROM_WAREHOUSE_ID = FW.WAREHOUSE_ID " +
                                        "WHERE TRM.FROM_WAREHOUSE_ID IN(" + string.Join(",", warehouseIds) + ") AND TRM.IS_ACTIVE = 1 " +
                                        "AND TRM.IS_APPROVED = 0 AND TRM.IS_TERMINATED = 0 ORDER BY TRM.CREATED_DATETIME DESC ";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                tr_Master = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
            if (tr_Master.Count > 0)
            {
                foreach (TR_Master trMaster in tr_Master)
                {
                    TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                    trMaster.TRDetails = TrDAO.fetchTRDList(trMaster.TRId, trMaster.CompanyId, dbConnection);
                }
            }
            return tr_Master;

        }

        public int ApproveOrRejectTR(int TrId, int isApproved, int UserId, string remark, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            // dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_MASTER SET IS_APPROVED= " + isApproved + ",APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "' WHERE MRN_ID=" + mrnID;
            string sql = "UPDATE " + dbLibrary + ".TR_MASTER SET IS_APPROVED= " + isApproved + ",APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "', APPROVAL_REMARKS = '" + remark + "' WHERE TR_ID=" + TrId + "; \n";

            if (isApproved == 1)
            {
                sql += "INSERT INTO TR_DETAIL_STATUS_LOG \n";
                sql += "SELECT TRD_ID,1,'" + LocalTime.Now + "', " + UserId + "  FROM TR_DETAILS WHERE TR_ID = " + TrId + " \n";

            }
            else if (isApproved == 2)
            {
                sql += "INSERT INTO TR_DETAIL_STATUS_LOG \n";
                sql += "SELECT TRD_ID,2,'" + LocalTime.Now + "', " + UserId + "  FROM TR_DETAILS WHERE TR_ID = " + TrId + " \n";

            }
            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int TerminateTR(int TrId, int TerminatedBy, string Remarks, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO TR_DETAIL_STATUS_LOG \n");
            sql.Append("SELECT TRD_ID,4,'" + LocalTime.Now + "'," + TerminatedBy + " FROM TR_DETAILS WHERE IS_TERMINATED=0 AND TR_ID =" + TrId + " \n");
            sql.Append("UPDATE TR_MASTER SET IS_TERMINATED=1, TERMINATED_BY=" + TerminatedBy + ", TERMINATED_DATE='" + LocalTime.Now + "', TERMINATED_REASON='" + Remarks.ProcessString() + "', STATUS=2 WHERE TR_ID=" + TrId + " \n");
            sql.Append(" \n");
            sql.Append("UPDATE TR_DETAILS SET IS_TERMINATED=1, TERMINATED_BY=" + TerminatedBy + ", TERMINATED_DATE='" + LocalTime.Now + "', TERMINATED_REASON='" + Remarks.ProcessString() + "',STATUS=6 WHERE TR_ID=" + TrId + " \n");
            sql.Append(" \n");


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public TR_Master getTRM(int trID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            TR_Master trMaster = new TR_Master();
            dbConnection.cmd.CommandText = "SELECT * FROM TR_MASTER AS TRM " +
                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON TRM.CREATED_BY = COLOG.USER_ID " +
                            "WHERE TRM.TR_ID = " + trID + " AND TRM.IS_ACTIVE = 1";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                trMaster = dataAccessObject.GetSingleOject<TR_Master>(dbConnection.dr);
            }
            TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
            trMaster.TRDetails = TrDAO.fetchTRDList(trMaster.TRId, trMaster.CompanyId, dbConnection);
            return trMaster;
        }


        public int updateTR(TR_Master tr, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            String sql = "UPDATE " + dbLibrary + ".TR_MASTER SET DESCRIPTION= '" + tr.Description + "', EXPECTED_DATE= '" + tr.ExpectedDate + "', TO_WAREHOUSE_ID=" + tr.ToWarehouseId + " WHERE TR_ID=" + tr.TRId;
            sql += "INSERT INTO TR_DETAIL_STATUS_LOG \n";
            sql += "SELECT TRD_ID,3,'" + LocalTime.Now + "'," + tr.CreatedBy + " FROM TR_DETAILS WHERE TR_ID = " + tr.TRId + " \n";

            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TR_Master> fetchSubmittedTRList(List<int> WarehouseIds, DBConnection dbConnection)
        {
            List<TR_Master> mrnMasters = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText ="SELECT *,(SELECT COUNT(TRD_ID) FROM TR_DETAILS "+
                                            "WHERE STATUS NOT IN(0,6) AND TR_ID = TRM.TR_ID) AS ITEM_COUNT FROM ONLINE_BIDDING.dbo.TR_MASTER AS TRM "+
                                            "INNER JOIN(SELECT USER_ID, FIRST_NAME AS CREATED_BY_NAME FROM ONLINE_BIDDING.dbo.COMPANY_LOGIN) AS COLOG ON TRM.CREATED_BY = COLOG.USER_ID " +
                                            "LEFT JOIN(SELECT WAREHOUSE_ID, LOCATION AS TO_WAREHOUSE_NAME FROM ONLINE_BIDDING.dbo.WAREHOUSE) AS TW ON TRM.TO_WAREHOUSE_ID = TW.WAREHOUSE_ID " +
                                            "LEFT JOIN(SELECT WAREHOUSE_ID, LOCATION AS FROM_WAREHOUSE_NAME FROM ONLINE_BIDDING.dbo.WAREHOUSE) AS FW ON TRM.FROM_WAREHOUSE_ID = FW.WAREHOUSE_ID " +
                                            "WHERE TRM.TO_WAREHOUSE_ID IN(" + String.Join(",", WarehouseIds) + ") AND TRM.IS_ACTIVE = 1 AND TRM.STATUS = 0 AND TRM.IS_APPROVED = 1 " +
                                            "AND TRM.IS_TERMINATED = 0 ORDER BY TRM.CREATED_DATETIME DESC ";
            
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
           
            return mrnMasters;
        }

        public string GetTRCode(int TRId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT CONCAT('TR',TR_CODE) FROM TR_MASTER WHERE TR_ID=" + TRId;
            return dbConnection.cmd.ExecuteScalar().ToString();
        }

        public List<string> GetTrCodesByTrIds(List<int> TrIds, DBConnection dbConnection)
        {
            List<string> codes = new List<string>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT TR_CODE FROM " + dbLibrary + ".TR_MASTER WHERE TR_ID IN(" + string.Join(",", TrIds) + ")";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        codes.Add(dbConnection.dr[0].ToString());
                    }
                }
            }
            return codes;
        }

        public List<TR_Master> fetchTrListByCreatedUser(int CompanyId, int CreatedBy, DateTime date, int TRCode, List<int> fromWarehouseIds, List<int> toWarehouseIds, string trStatuts, string approvalStatuts, DBConnection dbConnection) {
            List<TR_Master> tr_Master = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM " + dbLibrary + ".TR_MASTER AS TR " +
                 "INNER JOIN TR_DETAILS AS TRD ON TRD.TR_ID = TR.TR_ID " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON TR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS TO_WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS TW ON TR.TO_WAREHOUSE_ID= TW.WAREHOUSE_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS FROM_WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS FW ON TR.FROM_WAREHOUSE_ID= FW.WAREHOUSE_ID " +
               "WHERE TR.CREATED_BY =" + CreatedBy + " AND TR.IS_ACTIVE=1 AND TR.COMPANY_ID = " + CompanyId + " ";

            if (TRCode != 0) {
                sql += " AND TR.TR_CODE =  " + TRCode + " ";
            }


            if (date != DateTime.MinValue) {
                sql += " AND MONTH(TR.CREATED_DATETIME)= " + date.Month.ToString() + " AND YEAR(TR.CREATED_DATETIME)= " + date.Year.ToString() + " ";
            }

            if (fromWarehouseIds.Count > 0) {
                sql += " AND TR.FROM_WAREHOUSE_ID IN (" + string.Join(",", fromWarehouseIds) + ")";
            }

            if (toWarehouseIds.Count > 0) {
                sql += " AND TR.TO_WAREHOUSE_ID IN (" + string.Join(",", toWarehouseIds) + ")";

            }

            if (trStatuts != "") {

                sql += " AND  TRD.STATUS = " + trStatuts + " ";

            }

            if (approvalStatuts != "") {
               
                    sql += " AND  TR.IS_APPROVED = " + approvalStatuts + "  ";
                
            }

            sql += "ORDER BY TR.CREATED_DATETIME DESC";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                tr_Master = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
            return tr_Master;

        }

        public List<TR_Master> fetchTrListByCompanyId(int CompanyId, DateTime date, int TRCode, List<int> fromWarehouseIds, List<int> toWarehouseIds, string trStatuts, string approvalStatuts, DBConnection dbConnection) {
            List<TR_Master> tr_Master = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            string sql = "SELECT * FROM " + dbLibrary + ".TR_MASTER AS TR " +
                 "INNER JOIN TR_DETAILS AS TRD ON TRD.TR_ID = TR.TR_ID " +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON TR.CREATED_BY=COLOG.USER_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS TO_WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS TW ON TR.TO_WAREHOUSE_ID= TW.WAREHOUSE_ID " +
                "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS FROM_WAREHOUSE_NAME FROM " + dbLibrary + ".WAREHOUSE) AS FW ON TR.FROM_WAREHOUSE_ID= FW.WAREHOUSE_ID " +
               "WHERE TR.IS_ACTIVE=1 AND TR.COMPANY_ID = " + CompanyId + " ";

            if (TRCode != 0) {
                sql += " AND TR.TR_CODE =  " + TRCode + " ";
            }


            if (date != DateTime.MinValue) {
                sql += " AND MONTH(TR.CREATED_DATETIME)= " + date.Month.ToString() + " AND YEAR(TR.CREATED_DATETIME)= " + date.Year.ToString() + " ";
            }

            if (fromWarehouseIds.Count > 0) {
                sql += " AND TR.FROM_WAREHOUSE_ID IN (" + string.Join(",", fromWarehouseIds) + ")";
            }

            if (toWarehouseIds.Count > 0) {
                sql += " AND TR.TO_WAREHOUSE_ID IN (" + string.Join(",", toWarehouseIds) + ")";

            }

            if (trStatuts != "") {

                sql += " AND  TRD.STATUS = " + trStatuts + " ";

            }

            if (approvalStatuts != "") {

                sql += " AND  TR.IS_APPROVED = " + approvalStatuts + "  ";

            }

            sql += "ORDER BY TR.CREATED_DATETIME DESC";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                tr_Master = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
            return tr_Master;

        }
        public List<TR_Master> FetchtrsByCreatedUser(int CreatedBy, DateTime date, DBConnection dbConnection)
        {
            List<TR_Master> TRMasters = new List<TR_Master>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TR_MASTER AS TRM  " +
                                            "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_BY_NAME FROM COMPANY_LOGIN) AS COLOG ON TRM.CREATED_BY=COLOG.USER_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS TO_WAREHOUSE_NAME FROM WAREHOUSE ) AS TW ON TRM.TO_WAREHOUSE_ID=TW.WAREHOUSE_ID " +
                                            "LEFT JOIN (SELECT WAREHOUSE_ID,LOCATION AS FROM_WAREHOUSE_NAME FROM WAREHOUSE) AS FW ON TRM.FROM_WAREHOUSE_ID=FW.WAREHOUSE_ID " +
                                            "WHERE TRM.CREATED_BY =" + CreatedBy + " AND TRM.IS_ACTIVE=1 AND MONTH(TRM.CREATED_DATETIME) =" + date.Month + " AND YEAR(TRM.CREATED_DATETIME)= " + date.Year + " ORDER BY TRM.CREATED_DATETIME DESC ";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                TRMasters = dataAccessObject.ReadCollection<TR_Master>(dbConnection.dr);

            }
            return TRMasters;

        }

    }
}
