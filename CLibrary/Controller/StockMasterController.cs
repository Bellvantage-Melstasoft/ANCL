using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{

    public interface StockMasterControllerInterface
    {
        int saveStock(int subDepartmentID, int itemID, int qty);
        int getStock(int subDepartmentID, int itemID);

    }
    class StockMasterController : StockMasterControllerInterface
    {
        public int saveStock(int subDepartmentID, int itemID, int qty)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                StockMasterDAOInterface stockMasterDAO = DAOFactory.CreateStockMasterDAO();
                return stockMasterDAO.saveStock(subDepartmentID, itemID, qty, dbConnection);
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

        public int getStock(int subDepartmentID, int itemID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                StockMasterDAOInterface stockMasterDAO = DAOFactory.CreateStockMasterDAO();
                return stockMasterDAO.getStock(subDepartmentID, itemID, dbConnection);
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
