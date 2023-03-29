using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface ProcurementCommitteeDAO
    {
        List<ProcurementCommittee> FetchAllProcurementCommittee(int companyId , DBConnection dbConnection);
        int SaveProcurementCommittee(string procurementComName, DateTime createdDate, int createdUser, int companyId, DBConnection dbConnection);
    }

    public class ProcurementCommitteeDAOImpl : ProcurementCommitteeDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<ProcurementCommittee> FetchAllProcurementCommittee(int companyId ,DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PROCUREMENT_COMMITTEE" +
                                           " WHERE COMPANY_ID= " + companyId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ProcurementCommittee>(dbConnection.dr);
            }
        }

        public int SaveProcurementCommittee(string procurementComName, DateTime createdDate , int createdUser, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = " SELECT COUNT(*) as cnt FROM " + dbLibrary + ".PROCUREMENT_COMMITTEE" +
                                           " WHERE COMMITTEE_NAME = '" + procurementComName + "' " +
                                           " AND COMPANY_ID =" + companyId + "";
            if (decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PROCUREMENT_COMMITTEE" +
                                               " VALUES ('"+ procurementComName + "','"+ createdDate + "',"+ createdUser + " ,"+ companyId + ")";

            }else
            {
                //dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PROCUREMENT_COMMITTEE" +
                //                              " SET COMMITTEE_NAME = '"+ procurementComName + "' "+
                //                              " WHERE COMMITTEE_NAME "
            }

            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

  
}
