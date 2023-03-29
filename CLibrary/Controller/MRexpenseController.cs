using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface MRexpenseController
    {
        MRexpense GetMRexpenseById(int MRId);

        int SaveMRExpense(int MRId, int IsBudget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo , int isApproved);

        void UpdateMRExpense(int MRId, int Is_Budget, decimal BudgetAmount, string BudgetRemark, string BudgetInfor, int isApproved);
        int ApproveMRNExpense(int mrnId);
    }

    public class MRexpenseControllerImpl : MRexpenseController
    {
       

        public MRexpense GetMRexpenseById(int MRId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRexpenseDAO DAO = DAOFactory.CreateMRexpenseDAO();
                return DAO.GetMRexpenseById(MRId, dbConnection);

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

        public int SaveMRExpense(int MRId, int IsBudget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo , int isApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRexpenseDAO DAO = DAOFactory.CreateMRexpenseDAO();
                DAO.SaveMRExpense(MRId, IsBudget, BudgetAmount, BudgetRemark, BudgetInfo, isApproved, dbConnection);
                return 1;
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

        public void UpdateMRExpense(int MRId, int Is_Budget, decimal BudgetAmount, string BudgetRemark, string BudgetInfor, int isApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRexpenseDAO DAO = DAOFactory.CreateMRexpenseDAO();
                DAO.UpdateMRExpense(MRId, Is_Budget, BudgetAmount, BudgetRemark, BudgetInfor, isApproved, dbConnection);
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

        public int ApproveMRNExpense(int mrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRexpenseDAO DAO = DAOFactory.CreateMRexpenseDAO();
                DAO.ApproveMRNExpense(mrnId , dbConnection);
                return 1;
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
    }
}
