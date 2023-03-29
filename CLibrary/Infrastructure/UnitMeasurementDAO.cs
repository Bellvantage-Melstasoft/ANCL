using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface UnitMeasurementDAO
    {
        int saveMeasurement(int companyId, string measurementName, string measurementShortName, string createdBy, DateTime createdDate, string updatedBy, DateTime updatedDate, int isActive, DBConnection dbConnection);
        int updateMeasurement(int companyId, int measurementId, string measurementName, string measurementShortName, string updatedBy, DateTime updatedDate, int isActive, DBConnection dbConnection);
        List<UnitMeasurement> fetchMeasurementsByCompanyID(int companyId, DBConnection dbConnection);
        UnitMeasurement fetchMeasurementObjByMeasurementId(int measurementId, DBConnection dbConnection);
        int deleteMesurement(int measurementId, int companyId, DBConnection dbConnection);
        List<Currency> fetchCurrency(DBConnection dbConnection);
    }
    public class UnitMeasurementDAOImpl : UnitMeasurementDAO
    {
        public int deleteMesurement(int measurementId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE public.\"UNIT_MEASUREMENT\" SET \"IS_ACTIVE\" = " + 0 + " WHERE  \"MEASUREMENT_ID\" = " + measurementId + " AND  \"COMPANY_ID\" = " + companyId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Currency> fetchCurrency(DBConnection dbConnection)
        {
          return null;
        }

        public UnitMeasurement fetchMeasurementObjByMeasurementId(int measurementId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "select * from  public.\"UNIT_MEASUREMENT\" where \"MEASUREMENT_ID\" = " + measurementId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<UnitMeasurement>(dbConnection.dr);
            }
        }

        public List<UnitMeasurement> fetchMeasurementsByCompanyID(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "select * from  public.\"UNIT_MEASUREMENT\" where \"COMPANY_ID\" = " + companyId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UnitMeasurement>(dbConnection.dr);
            }
        }

        public int saveMeasurement(int companyId, string measurementName, string measurementShortName, string createdBy, DateTime createdDate, string updatedBy, DateTime updatedDate, int isActive, DBConnection dbConnection)
        {
            int measurementId = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"UNIT_MEASUREMENT\" ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                measurementId = 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (\"MEASUREMENT_ID\")+1 AS MAXid FROM public.\"UNIT_MEASUREMENT\" ";
                measurementId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            
            dbConnection.cmd.CommandText = "INSERT INTO public.\"UNIT_MEASUREMENT\" (\"MEASUREMENT_ID\", \"COMPANY_ID\", \"MEASUREMENT_NAME\", \"MEASUREMENT_SHORT_NAME\", \"CREATED_DATE\", \"CREATED_BY\", \"UPDATED_DATE\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES" +
                        "( " + measurementId + ", " + companyId + " , '" + measurementName + "', '" + measurementShortName + "', '" + createdDate + "', '" + createdBy + "', '" + updatedDate + "', '" + updatedBy + "', " + 1 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateMeasurement(int companyId, int measurementId, string measurementName, string measurementShortName, string updatedBy, DateTime updatedDate, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE public.\"UNIT_MEASUREMENT\" SET  \"MEASUREMENT_NAME\" = '" + measurementName + "', \"MEASUREMENT_SHORT_NAME\" = '" + measurementShortName + "', \"UPDATED_DATE\" = '" + updatedDate + "', \"UPDATED_BY\" = '" + updatedBy + "', \"IS_ACTIVE\" = " + isActive + " WHERE  \"MEASUREMENT_ID\" = " + measurementId + " AND  \"COMPANY_ID\" = " + companyId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    


    // SQL Server Implementation

    public class UnitMeasurementDAOSQLImpl : UnitMeasurementDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int deleteMesurement(int measurementId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".UNIT_MEASUREMENT SET IS_ACTIVE = " + 0 + " WHERE  MEASUREMENT_ID = " + measurementId + " AND  COMPANY_ID = " + companyId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Currency> fetchCurrency(DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "select * from " + dbLibrary + ".CURRENCY ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Currency>(dbConnection.dr);
            }
        }

        public UnitMeasurement fetchMeasurementObjByMeasurementId(int measurementId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "select * from " + dbLibrary + ".UNIT_MEASUREMENT where MEASUREMENT_ID = " + measurementId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<UnitMeasurement>(dbConnection.dr);
            }
        }

        public List<UnitMeasurement> fetchMeasurementsByCompanyID(int companyId, DBConnection dbConnection)  
        {
            dbConnection.cmd.CommandText = "SELECT A.MEASUREMENT_ID,A.COMPANY_ID,A.MEASUREMENT_NAME,A.MEASUREMENT_SHORT_NAME,B.CREATED_BY,C.UPDATED_BY,A.IS_ACTIVE,A.CREATED_DATE,A.UPDATED_DATE " +
                "FROM "+ dbLibrary +".UNIT_MEASUREMENT AS A " +
                "INNER JOIN(SELECT USER_ID, USER_NAME AS CREATED_BY FROM dbo.COMPANY_LOGIN)AS B ON A.CREATED_BY = B.USER_ID " +
                "INNER JOIN (SELECT USER_ID, USER_NAME AS UPDATED_BY FROM dbo.COMPANY_LOGIN)AS C ON A.UPDATED_BY = C.USER_ID " +
                "WHERE COMPANY_ID ="+companyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UnitMeasurement>(dbConnection.dr);
            }
        }

        public int saveMeasurement(int companyId, string measurementName, string measurementShortName, string createdBy, DateTime createdDate, string updatedBy, DateTime updatedDate, int isActive, DBConnection dbConnection)
        {
            int measurementId = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".UNIT_MEASUREMENT ";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                measurementId = 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (MEASUREMENT_ID)+1 AS MAXid FROM " + dbLibrary + ".UNIT_MEASUREMENT ";
                measurementId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".UNIT_MEASUREMENT (MEASUREMENT_ID, COMPANY_ID, MEASUREMENT_NAME, MEASUREMENT_SHORT_NAME, CREATED_DATE, CREATED_BY, UPDATED_DATE, UPDATED_BY, IS_ACTIVE) VALUES" +
                        "( " + measurementId + ", " + companyId + " , '" + measurementName + "', '" + measurementShortName + "', '" + createdDate + "', '" + createdBy + "', '" + updatedDate + "', '" + updatedBy + "', " + 1 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;


            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateMeasurement(int companyId, int measurementId, string measurementName, string measurementShortName, string updatedBy, DateTime updatedDate, int isActive, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".UNIT_MEASUREMENT SET  MEASUREMENT_NAME = '" + measurementName + "', MEASUREMENT_SHORT_NAME = '" + measurementShortName + "', UPDATED_DATE = '" + updatedDate + "', UPDATED_BY = '" + updatedBy + "', IS_ACTIVE = " + isActive + " WHERE  MEASUREMENT_ID = " + measurementId + " AND  COMPANY_ID = " + companyId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
