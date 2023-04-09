using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Infrastructure
{
    public interface AddItemPOReportsDAO
    {
        List<AddItemPOReports> GetItemPoReports(DBConnection dbConnection);
    }
    public class AddItemPOReportsDAOSqlImpl : AddItemPOReportsDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<AddItemPOReports> GetItemPoReports(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT a.SUPPLIER_ID,d.SUPPLIER_NAME,a.IS_APPROVED,a.TOTAL_AMOUNT,c.ITEM_ID,c.ITEM_NAME,c.CATEGORY_ID,c.SUB_CATEGORY_ID,c.CREATED_DATETIME  FROM " + dbLibrary + " .PO_MASTER a " +
                "INNER JOIN SUPPLIER d ON d.SUPPLIER_ID=a.SUPPLIER_ID " +
                "INNER JOIN PO_DETAILS b ON a.PO_ID=b.PO_ID " +
                "INNER JOIN ADD_ITEMS c ON b.ITEM_ID=c.ITEM_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItemPOReports>(dbConnection.dr);
            }
        }
    }
}
