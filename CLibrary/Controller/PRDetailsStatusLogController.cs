using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface PRDetailsStatusLogController
    {
        List<PRDetailsStatusLog> PrLogDetails(int prDId);
        int InsertLog(int prdId, int UserId, string StatusCode);
        int UpdatePRStatusLog(int userId, int prdId, string LogStatus);
        int UpdatePRStatusLogForPoCancel(int userId, string LogStatus, int itemId, int PrId);
    }

    class PRDetailsStatusLogControllerImpl : PRDetailsStatusLogController
    {
        public int InsertLog(int prdId, int UserId, string StatusCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PRDetailsStatusLogDAO prDetailStatusLogDAO = DAOFactory.CreatePRDetailsStatusLogDAO();
                return prDetailStatusLogDAO.InsertLog(prdId, UserId, StatusCode, dbConnection);
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

        public List<PRDetailsStatusLog> PrLogDetails(int prDId)
        {
            DBConnection dbConnection = new DBConnection();
            try {
                PRDetailsStatusLogDAO prDetailsStatusLogDAO = DAOFactory.CreatePRDetailsStatusLogDAO();
                return prDetailsStatusLogDAO.GetPrDStatusByPrDId(prDId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public int UpdatePRStatusLog(int userId, int prdId, string LogStatus) {
            DBConnection dbConnection = new DBConnection();
            try {
                PRDetailsStatusLogDAO prDetailsStatusLogDAO = DAOFactory.CreatePRDetailsStatusLogDAO();
                return prDetailsStatusLogDAO.UpdatePRStatusLog(userId, prdId, LogStatus, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public int UpdatePRStatusLogForPoCancel(int userId, string LogStatus, int itemId, int PrId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PRDetailsStatusLogDAO prDetailsStatusLogDAO = DAOFactory.CreatePRDetailsStatusLogDAO();
                return prDetailsStatusLogDAO.UpdatePRStatusLogForPoCancel(userId, LogStatus, itemId, PrId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
    }
}
