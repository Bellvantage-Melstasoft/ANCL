using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface PRexpenseController
    {
        PRexpense GetPRexpenseById(int prId);

        void SavePRExpense(int purchaseRequestId, int IsBudget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo , int isApproved);

        void UpdatePRExpense(int prID, int Is_Budget, decimal BudgetAmount, string BudgetRemark, string BudgetInfor ,int isApproved);
        int ApprovePRExpense(int prId);
        
    }

    public class PRexpenseControllerImpl : PRexpenseController
    {
        public PRexpense GetPRexpenseById(int prId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRexpenseDAO DAO = DAOFactory.CreatePRexpenseDAO();
                return DAO.GetProStockInfoById(prId, dbConnection);

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

        public void SavePRExpense(int purchaseRequestId, int IsBudget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo ,int isApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRexpenseDAO prExpenseDAO = DAOFactory.CreatePRexpenseDAO();
                prExpenseDAO.SavePRExpense(purchaseRequestId, IsBudget, BudgetAmount, BudgetRemark, BudgetInfo, isApproved, dbConnection);
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

        public void UpdatePRExpense(int prID, int Is_Budget, decimal BudgetAmount, string BudgetRemark, string BudgetInfor,int isApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRexpenseDAO pr_MasterDAO = DAOFactory.CreatePRexpenseDAO();
                pr_MasterDAO.UpdatePRExpense(prID, Is_Budget, BudgetAmount, BudgetRemark,BudgetInfor, isApproved , dbConnection);
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

        public int ApprovePRExpense(int prId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRexpenseDAO DAO = DAOFactory.CreatePRexpenseDAO();
                DAO.ApprovePRExpense(prId, dbConnection);
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
