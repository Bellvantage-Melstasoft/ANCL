using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface PRexpenseDAO
    {

        PRexpense GetProStockInfoById(int prId, DBConnection dbConnection);
        void SavePRExpense(int purchaseRequestId, int IsBudget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo,int isApproved, DBConnection dbConnection);

        void UpdatePRExpense(int prID, int Is_Budget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo,int isApproved, DBConnection dbConnection);
        void ApprovePRExpense(int prId, DBConnection dbConnection);
    }


    public class PRexpenseDAOImpl : PRexpenseDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        

        public PRexpense GetProStockInfoById(int prId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".PR_EXPENSE WHERE PR_ID = " + prId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PRexpense>(dbConnection.dr);
            }
        }


        public void SavePRExpense(int purchaseRequestId, int IsBudget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo,int isApproved, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_EXPENSE (PR_ID, IS_BUDGET, BUDGET_AMOUNT, REMARKS, BUDGET_INFORMATION ,IS_APPROVED) VALUES" +
                       "( " + purchaseRequestId + ", " + IsBudget + " , " + BudgetAmount + ", '" + BudgetRemark + "', '" + BudgetInfo + "' , "+ isApproved + " );";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public void UpdatePRExpense(int prID, int Is_Budget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo,int isApproved, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();         
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_EXPENSE "+
                                           " SET  IS_BUDGET = " + Is_Budget + " , "+
                                           " BUDGET_AMOUNT = " + BudgetAmount + " , " +
                                           " REMARKS = '" + BudgetRemark + "', "+
                                           " BUDGET_INFORMATION = '" + BudgetInfo + "' ,"+
                                           " IS_APPROVED = "+ isApproved + " " +
                                           " WHERE PR_ID = " + prID + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }

        public void ApprovePRExpense(int prId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_EXPENSE SET IS_APPROVED= 1 " +
                                           " WHERE PR_ID = " + prId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }
    }

}
