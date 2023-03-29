using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface IMeasurementDetailDAO
    {
        MeasurementDetail GetStockMaintainingMeasurement(int ItemId, int CompanyId, DBConnection dbConnection);
        List<MeasurementDetail> GetMeasurementDetailsOfItem(int ItemId, int CompanyId, DBConnection dbConnection);
        List<MeasurementDetail> GetAllMeasurementsOfCompany(int CompanyId, DBConnection dbConnection);
        List<MeasurementDetail> GetAllCustomMeasurementsOfCompany(int CompanyId, DBConnection dbConnection);
        int UpdateCustomMeasurementActiveStatus(int MeasurementId, int IsActive, DBConnection dbConnection);
        int CreateCustomMeasurement(string MeasurementName, string ShortCode, int CompanyId, int CreatedBy, int IsActive, DBConnection dbConnection);
        List<MeasurementDetail> GetMeasurementDetailsByMasterId(int MeasurementMasterId, int CompanyId, DBConnection dbConnection);

    }

    public class MeasurementDetailDAO : IMeasurementDetailDAO
    {
        public MeasurementDetail GetStockMaintainingMeasurement(int ItemId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $@"SELECT MD.DETAIL_ID,MD.[MEASUREMENT_NAME],MD.[SHORT_CODE] FROM [MEASUREMENT_DETAIL] AS MD
                                                INNER JOIN ADD_ITEMS AS AI ON AI.MEASUREMENT_ID = MD.[DETAIL_ID]
                                                WHERE AI.[ITEM_ID] = {ItemId} AND AI.[COMPANY_ID] ={CompanyId}";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<MeasurementDetail>(dbConnection.dr);

            }
        }

        public List<MeasurementDetail> GetMeasurementDetailsOfItem(int ItemId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $@"SELECT MD.DETAIL_ID,MD.[MEASUREMENT_NAME],MD.[SHORT_CODE] FROM [MEASUREMENT_DETAIL] AS MD
                                                INNER JOIN [ITEM_MEASUREMENT] AS IM ON IM.[MEASUREMENT_DETAIL_ID] = MD.[DETAIL_ID]
                                                WHERE IM.[ITEM_ID] = {ItemId} AND IM.[COMPANY_ID] ={CompanyId}";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MeasurementDetail>(dbConnection.dr);

            }
        }

        public List<MeasurementDetail> GetAllMeasurementsOfCompany(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $@"SELECT * FROM MEASUREMENT_DETAIL WHERE (MASTER_ID != 5 AND IS_ACTIVE=1) OR (MASTER_ID=5 AND IS_ACTIVE=1 AND COMPANY_ID={CompanyId})";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MeasurementDetail>(dbConnection.dr);

            }
        }

        public List<MeasurementDetail> GetAllCustomMeasurementsOfCompany(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $@"SELECT MD.*,CL.FIRST_NAME AS CREATED_BY_NAME FROM MEASUREMENT_DETAIL AS MD
                                            INNER JOIN COMPANY_LOGIN AS CL ON MD.CREATED_BY = CL.USER_ID
                                            WHERE MD.MASTER_ID=5 AND MD.COMPANY_ID={CompanyId}";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MeasurementDetail>(dbConnection.dr);

            }
        }

        public int UpdateCustomMeasurementActiveStatus(int MeasurementId, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $"UPDATE MEASUREMENT_DETAIL SET IS_ACTIVE={IsActive} WHERE DETAIL_ID={MeasurementId}";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int CreateCustomMeasurement(string MeasurementName, string ShortCode, int CompanyId, int CreatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM MEASUREMENT_DETAIL WHERE  MEASUREMENT_NAME ='" + MeasurementName + "'";
            var existCount = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (existCount == 0) {
               
                dbConnection.cmd.CommandText = $@"INSERT INTO  [MEASUREMENT_DETAIL] ([DETAIL_ID],[MASTER_ID],[MEASUREMENT_NAME],[SHORT_CODE],[IS_BASE],[COMPANY_ID],[IS_ACTIVE],[CREATED_BY],[CREATED_DATE])
                                                VALUES((SELECT ISNULL(MAX(DETAIL_ID),0)+1 FROM [MEASUREMENT_DETAIL]),5,'{MeasurementName}','{ShortCode}',0,{CompanyId},{IsActive},{CreatedBy},'{LocalTime.Now}')";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else {
                return -1;
            }
                
        }

        public List<MeasurementDetail> GetMeasurementDetailsByMasterId(int MeasurementMasterId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = $"SELECT * FROM [MEASUREMENT_DETAIL] WHERE MASTER_ID={MeasurementMasterId} AND IS_ACTIVE=1 ";
            //If Custom, requires CompanyId
            if (MeasurementMasterId == 5)
            {
                dbConnection.cmd.CommandText += "AND COMPANY_ID =" + CompanyId;
            }
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<MeasurementDetail>(dbConnection.dr);

            }
        }
    }
}
