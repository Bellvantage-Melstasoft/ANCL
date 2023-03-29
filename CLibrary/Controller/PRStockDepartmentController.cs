using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface PRStockDepartmentController
    {
        PRStockDepartment GetPRStockDepartmentMrnId(int prId);
        PRStockDepartment GetPRStockDepartmentMrnItemId(int prId , int itemId);
        int savePRStockDepartment(List<PRStockDepartment> listPrStockDepartment, int userId, DateTime enteredDate);
    }

    public class PRStockDepartmentControllerImpl : PRStockDepartmentController
    {

        public PRStockDepartment GetPRStockDepartmentMrnId(int prId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRStockDepartmentDAO DAO = DAOFactory.CreatePRStockDepartmentDAO();
                return DAO.GetPRStockDepartmentMrnId(prId, dbConnection);

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

        public PRStockDepartment GetPRStockDepartmentMrnItemId(int prId, int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRStockDepartmentDAO DAO = DAOFactory.CreatePRStockDepartmentDAO();
                return DAO.GetPRStockDepartmentMrnItemId(prId, itemId, dbConnection);

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

        public int savePRStockDepartment(List<PRStockDepartment> listPrStockDepartment, int userId, DateTime enteredDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRStockDepartmentDAO DAO = DAOFactory.CreatePRStockDepartmentDAO();
                foreach(PRStockDepartment item in listPrStockDepartment)
                {
                    DAO.savePRStockDepartment(item.PrId, item.PrdId, item.ItemId, item.RequestedQty, item.AvailableQty,item.MeasurementId, userId, enteredDate, dbConnection);
                }
                return 1;

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
