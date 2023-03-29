using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface ProcurementCommitteeController
    {
        List<ProcurementCommittee> FetchAllProcurementCommittee(int companyId);
        int SaveProcurementCommittee(string procurementComName,DateTime createdDate , int createdUser, int companyId);
    }

    public class ProcurementCommitteeControllerImpl : ProcurementCommitteeController
    {
        public List<ProcurementCommittee> FetchAllProcurementCommittee(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ProcurementCommitteeDAO procurementCommitteeDAO = DAOFactory.CreateProcurementCommitteeDAO();
                return procurementCommitteeDAO.FetchAllProcurementCommittee(companyId , dbConnection);
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

        public int SaveProcurementCommittee(string procurementComName, DateTime createdDate, int createdUser, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ProcurementCommitteeDAO procurementCommitteeDAO = DAOFactory.CreateProcurementCommitteeDAO();
                return procurementCommitteeDAO.SaveProcurementCommittee(procurementComName, createdDate, createdUser, companyId, dbConnection);
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
