using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface QuotationImageController
    {
        List<QuotationImage> GetQuotationImages(int QuotationId);
    }
    public class QuotationImageControllerImpl : QuotationImageController
    {
        public List<QuotationImage> GetQuotationImages(int QuotationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                QuotationImageDAO DAO = DAOFactory.createQuotationImageDAO();
                return DAO.GetQuotationImages(QuotationId, dbConnection);
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
