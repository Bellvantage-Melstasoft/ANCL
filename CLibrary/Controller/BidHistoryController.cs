using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
   public interface BidHistoryController
    {
        int SaveBidHistory(int quotationNo, string biddedBy, int bidderId, decimal unitPrice, decimal vatAmount, decimal nbtAmount, decimal totalAmount, DateTime bidSubmittedDate);
        List<BidHistory> fetchBidHistoryByQuoytationNo(int quotationNo);
    }
    public class BidHistoryControllerImpl : BidHistoryController
    {
        public List<BidHistory> fetchBidHistoryByQuoytationNo(int quotationNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BidHistoryDAO bidHistoryDAO = DAOFactory.CreateBidHistoryDAO();
                return bidHistoryDAO.fetchBidHistoryByQuoytationNo(quotationNo, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int SaveBidHistory(int quotationNo, string biddedBy, int bidderId, decimal unitPrice, decimal vatAmount, decimal nbtAmount, decimal totalAmount, DateTime bidSubmittedDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                BidHistoryDAO bidHistoryDAO = DAOFactory.CreateBidHistoryDAO();
                return bidHistoryDAO.SaveBidHistory( quotationNo,  biddedBy,  bidderId,  unitPrice,  vatAmount,  nbtAmount,  totalAmount,  bidSubmittedDate, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    }
}
