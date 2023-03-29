using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Text;
using CLibrary.Domain;


namespace CLibrary.Infrastructure
{
    public interface SupplierRatingDAO
    {
        int SupplierRating(int supplierid, int companyId, int rating, int isBlackList, int isActive, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy,string remarks, DBConnection dbConnection);
        SupplierRatings GetSupplierRatingBySupplierIdAndCompanyId(int supplierid, int companyId, DBConnection dbConnection);
    }

    public class SupplierRatingDAOSQLImpl : SupplierRatingDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public SupplierRatings GetSupplierRatingBySupplierIdAndCompanyId(int supplierid, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_RATINGS WHERE  SUPPLIER_ID = " + supplierid + " AND  COMPANY_ID =" + companyId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierRatings>(dbConnection.dr);
            }
        }

        public int SupplierRating(int supplierid, int companyId, int rating, int isBlackList, int isActive, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy, string remarks, DBConnection dbConnection)
        {

            int result = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_RATINGS WHERE SUPPLIER_ID = " + supplierid + " AND  COMPANY_ID = " + companyId + "";
            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_RATINGS (RATING, IS_BLACKLIST, IS_ACTIVE, CREATED_BY, CREATED_DATE, UPDATED_BY, UPDATED_DATE, SUPPLIER_ID, COMPANY_ID, REMARKS ) VALUES" + " ( " + rating + ", " + isBlackList + " , " + isActive + ", '" + createdBy + "', '" + createdDate + "', '" + updatedBy + "', '" + updatedDate + "', " + supplierid + ", " + companyId + ",'" + remarks + "');";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                result = dbConnection.cmd.ExecuteNonQuery();
            }
            if (count > 0)
            {

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_RATINGS  SET  RATING = " + rating + ", IS_BLACKLIST = " + isBlackList + ",IS_ACTIVE =" + isActive + " , UPDATED_BY = '" + updatedBy + "',  UPDATED_DATE = '" + updatedDate + "',  REMARKS = '" + remarks + "'  WHERE  SUPPLIER_ID = " + supplierid + " AND  COMPANY_ID =" + companyId + "";
                result = dbConnection.cmd.ExecuteNonQuery();
            }
            return result;
        } 
    }
}
