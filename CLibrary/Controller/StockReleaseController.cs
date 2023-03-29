using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface StockReleaseControllerInterface
    {
        int raiseStock(int subDepartmentID, int itemID, int releasedQty, int releasedBy, int releaseType, int releaseSubDepartmentID);

    }
    class StockReleaseController : StockReleaseControllerInterface
    {
        public int raiseStock(int subDepartmentID, int itemID, int releasedQty, int releasedBy, int releaseType, int releaseSubDepartmentID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                StockReleaseDAOInterface stockReleaseDAO = DAOFactory.CreateStockReleaseDAO();
                return stockReleaseDAO.releaseStock(subDepartmentID, itemID, releasedQty, releasedBy, releaseType, releaseSubDepartmentID, dbConnection);
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
