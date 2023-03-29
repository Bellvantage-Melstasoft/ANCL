using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{

    public interface StockRaiseControllerInterface
    {
        int raiseStock(int subDepartmentID, int itemID, int raisedQty,int raisedBy);

    }
    class StockRaiseController : StockRaiseControllerInterface
    {
        public int raiseStock(int subDepartmentID, int itemID, int raisedQty, int raisedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                StockRaiseDAOInterface stockRaiseDAO = DAOFactory.CreateStockRaiseDAO();
                return stockRaiseDAO.raiseStock(subDepartmentID, itemID, raisedQty,raisedBy, dbConnection);
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
