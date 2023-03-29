using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface MRNStockDepartmentController
    {
        MRNStockDepartment GetMRNStockDepartmentMrnId(int mrnId);
        MRNStockDepartment GetMRNStockDepartmentMrnItemId(int mrnId , int itemId);
        int saveMRNStockDepartment(List<MRNStockDepartment> listMrnStockDepartment, int userId, DateTime enteredDate);
    }

    public class MRNStockDepartmentControllerImpl : MRNStockDepartmentController
    {

        public MRNStockDepartment GetMRNStockDepartmentMrnId(int mrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNStockDepartmentDAO DAO = DAOFactory.CreateMRNStockDepartmentDAO();
                return DAO.GetMRNStockDepartmentMrnId(mrnId, dbConnection);

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

        public MRNStockDepartment GetMRNStockDepartmentMrnItemId(int mrnid ,int itemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNStockDepartmentDAO DAO = DAOFactory.CreateMRNStockDepartmentDAO();
                return DAO.GetMRNStockDepartmentMrnItemId(mrnid ,itemId, dbConnection);

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

        public int saveMRNStockDepartment(List<MRNStockDepartment> listMrnStockDepartment, int userId, DateTime enteredDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNStockDepartmentDAO DAO = DAOFactory.CreateMRNStockDepartmentDAO();
                foreach(MRNStockDepartment item in listMrnStockDepartment)
                {
                    DAO.saveMRNStockDepartment(item.MrnId, item.MrnDId, item.ItemId, item.RequestedQty, item.AvailableQty,item.MeasurementId, userId, enteredDate, dbConnection);
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
