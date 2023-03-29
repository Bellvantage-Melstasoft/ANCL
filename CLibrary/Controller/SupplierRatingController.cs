using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;


namespace CLibrary.Controller
{
   public interface SupplierRatingController
    {
        int SupplierRating(int supplierid, int companyId, int rating, int isBlackList, int isActive, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy,string remarks);
        SupplierRatings GetSupplierRatingBySupplierIdAndCompanyId(int supplierid, int companyId);
    }

    public class SupplierRatingControllerImpl : SupplierRatingController
    {
        public SupplierRatings GetSupplierRatingBySupplierIdAndCompanyId(int supplierid, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierRatingDAO supplierRatingDAO = DAOFactory.createSupplierRatingDAO();
                return supplierRatingDAO.GetSupplierRatingBySupplierIdAndCompanyId( supplierid,  companyId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int SupplierRating(int supplierid, int companyId, int rating, int isBlackList, int isActive, DateTime createdDate, string createdBy, DateTime updatedDate, string updatedBy, string remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierRatingDAO supplierRatingDAO = DAOFactory.createSupplierRatingDAO();
                return supplierRatingDAO.SupplierRating( supplierid,  companyId,  rating,  isBlackList,  isActive,  createdDate,  createdBy,  updatedDate,  updatedBy,remarks, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
    }
}
