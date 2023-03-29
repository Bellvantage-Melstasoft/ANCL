using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
   public interface QuotationImageDAO
    {
        int SaveQuatationImage(int quotationId, int prId, int itemId, int supplierId, string imagePath, DBConnection dBConnection);
        int UpdateQuatationImageByQuotationId(int quotationId, int prId, int itemId, int supplierId, string imagePath, DBConnection dBConnection);


        //New Methods By Salman created on 2019-01-17
        List<QuotationImage> GetQuotationImages(int QuotationId, DBConnection dbConnection);

    }

    public class QuotationImageDAOSQLImpl : QuotationImageDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<QuotationImage> GetQuotationImages(int QuotationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM QUOTATION_IMAGES WHERE QUOTATION_ID = " + QuotationId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<QuotationImage>(dbConnection.dr);
            }
        }

        public int SaveQuatationImage(int quotationId, int prId, int itemId, int supplierId, string imagePath, DBConnection dBConnection)
        {
            throw new NotImplementedException();
        }

        public int UpdateQuatationImageByQuotationId(int quotationId, int prId, int itemId, int supplierId, string imagePath, DBConnection dBConnection)
        {
            throw new NotImplementedException();
        }
    }  
}
