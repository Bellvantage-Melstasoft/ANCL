using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface PRDStockInfoLogController
    {
        PRDStockInfo GetProStockInfoLogById(int prDId);
        
        void savePRDStockInfoLog(int PRD_Id, decimal stockBalance,decimal lastPaurchasePrice, int supplierId ,DateTime? LastPurchaseDate ,decimal avgConsumption, string enteredUser, DateTime enteredDate);

        PRDStockInfo GetProStockInfoLogByItemId(int itemId);

        void upadtePRDStockInfoLog(int prID, int itemID , decimal stockBalance , decimal avgConsumption, string enteredUser, DateTime enteredDate);
    }

    public class PRDStockInfoLogControllerImpl : PRDStockInfoLogController
    {

        public PRDStockInfo GetProStockInfoLogById(int prDId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRDStockInfoLogDAO DAO = DAOFactory.CreatePRDStockInfoLogDAO();
                return DAO.GetProStockInfoLogById(prDId, dbConnection);

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

        public void savePRDStockInfoLog(int PRD_Id, decimal stockBalance, decimal lastPaurchasePrice, int supplierId, DateTime? LastPurchaseDate, decimal avgConsumption, string enteredUser, DateTime enteredDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRDStockInfoLogDAO DAO = DAOFactory.CreatePRDStockInfoLogDAO();
                DAO.savePRDStockInfoLog(PRD_Id, stockBalance, lastPaurchasePrice, supplierId, LastPurchaseDate, avgConsumption, enteredUser, enteredDate, dbConnection);

            }
            catch (Exception ex)
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

        public PRDStockInfo GetProStockInfoLogByItemId(int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRDStockInfoLogDAO DAO = DAOFactory.CreatePRDStockInfoLogDAO();
                return DAO.GetProStockInfoLogByItemId(itemId, dbConnection);

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

        public void upadtePRDStockInfoLog(int prID, int itemId, decimal stockBalance, decimal avgConsumption, string enteredUser, DateTime enteredDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRDStockInfoLogDAO DAO = DAOFactory.CreatePRDStockInfoLogDAO();
                DAO.upadtePRDStockInfoLog(prID, itemId,stockBalance,avgConsumption , enteredUser , enteredDate, dbConnection);

            }
            catch (Exception ex)
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
