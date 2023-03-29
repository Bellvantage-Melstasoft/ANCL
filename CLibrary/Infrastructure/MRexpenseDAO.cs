using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
  public interface MRexpenseDAO
  {
      MRexpense GetMRexpenseById(int MRId, DBConnection dbConnection);
      void SaveMRExpense(int MRId, int IsBudget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo,int isApproved, DBConnection dbConnection);

      void UpdateMRExpense(int MRId, int Is_Budget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo,int isApproved, DBConnection dbConnection);
        void ApproveMRNExpense(int mrnId, DBConnection dbConnection);
    }

  public class MRexpenseDAOImpl : MRexpenseDAO
  {
      string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
        
      public MRexpense GetMRexpenseById(int MRId, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();

          dbConnection.cmd.CommandText = "SELECT *  FROM " + dbLibrary + ".MRN_EXPENSE WHERE MRN_ID = " + MRId + " ";
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;

          using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
          {
              DataAccessObject dataAccessObject = new DataAccessObject();
              return dataAccessObject.GetSingleOject<MRexpense>(dbConnection.dr);
          }
      }


      public void SaveMRExpense(int MRId, int IsBudget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo,int isApproved, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MRN_EXPENSE (MRN_ID, IS_BUDGET, BUDGET_AMOUNT, REMARKS, BUDGET_INFORMATION , IS_APPROVED) VALUES" +
                     "( " + MRId + ", " + IsBudget + " , " + BudgetAmount + ", '" + BudgetRemark + "', '" + BudgetInfo + "' , "+ isApproved + ");";          
          dbConnection.cmd.ExecuteNonQuery();
      }

      public void UpdateMRExpense(int MRId, int Is_Budget, decimal BudgetAmount, string BudgetRemark, string BudgetInfo,int isApproved, DBConnection dbConnection)
      {
          dbConnection.cmd.Parameters.Clear();
          dbConnection.cmd.CommandType = System.Data.CommandType.Text;
          dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_EXPENSE SET  IS_BUDGET = " + Is_Budget + " , "+
                                         " BUDGET_AMOUNT = " + BudgetAmount + " , REMARKS = '" + BudgetRemark + "', "+
                                         " BUDGET_INFORMATION = '" + BudgetInfo + "' , IS_APPROVED = "+ isApproved + "  " +
                                         " WHERE MRN_ID = " + MRId + ";";
          
          dbConnection.cmd.ExecuteNonQuery();
      }

        public void ApproveMRNExpense(int mrnId , DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MRN_EXPENSE SET IS_APPROVED= 1 "+
                                           " WHERE MRN_ID = " + mrnId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
