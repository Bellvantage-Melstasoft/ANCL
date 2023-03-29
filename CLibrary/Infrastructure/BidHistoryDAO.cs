using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface BidHistoryDAO
    {
        int SaveBidHistory(int quotationNo, string biddedBy, int bidderId, decimal unitPrice, decimal vatAmount, decimal nbtAmount, decimal totalAmount, DateTime bidSubmittedDate, DBConnection dbConnection);
        List<BidHistory> fetchBidHistoryByQuoytationNo(int quotationNo, DBConnection dbConnection);
    }
    public class BidHistoryDAOImpl : BidHistoryDAO
    {
        public List<BidHistory> fetchBidHistoryByQuoytationNo(int quotationNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.BID_HISTORY WHERE QUOTATION_NO = " + quotationNo + " ORDER BY  public.BID_HISTORY.BIDDER_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BidHistory>(dbConnection.dr);
            }
        }

        public int SaveBidHistory(int quotationNo, string biddedBy, int bidderId, decimal unitPrice, decimal vatAmount, decimal nbtAmount, decimal totalAmount, DateTime bidSubmittedDate, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO  public.BID_HISTORY (QUOTATION_NO , BIDDER_ID , BIDDED_BY , BID_SUBMITED_DATE, VAT_AMOUNT, NBT_AMOUNT, PER_ITEM_PRICE , TOTAL_AMOUNT) VALUES (" + quotationNo + "," + bidderId + ",'" + biddedBy + "','" + bidSubmittedDate + "', " + vatAmount + ", " + nbtAmount + "," + unitPrice + "," + totalAmount + ")";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }
    }


    public class BidHistoryDAOSQLImpl : BidHistoryDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<BidHistory> fetchBidHistoryByQuoytationNo(int quotationNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".BID_HISTORY WHERE QUOTATION_NO = " + quotationNo + " ORDER BY  public.BID_HISTORY.BIDDER_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<BidHistory>(dbConnection.dr);
            }
        }

        public int SaveBidHistory(int quotationNo, string biddedBy, int bidderId, decimal unitPrice, decimal vatAmount, decimal nbtAmount, decimal totalAmount, DateTime bidSubmittedDate, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".BID_HISTORY (QUOTATION_NO , BIDDER_ID , BIDDED_BY , BID_SUBMITED_DATE, VAT_AMOUNT, NBT_AMOUNT, PER_ITEM_PRICE , TOTAL_AMOUNT) VALUES (" + quotationNo + "," + bidderId + ",'" + biddedBy + "','" + bidSubmittedDate + "', " + vatAmount + ", " + nbtAmount + "," + unitPrice + "," + totalAmount + ")";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
