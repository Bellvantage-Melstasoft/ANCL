using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface MrnDetailsStatusLogController
    {
        List<MRNDetailsStatusLog> MrnLogDetails(int MrndId);
        int InsertLog(int MrndId, int UserId, int Status);
        int UpdateMRNLog(int mrndId, int userId, int Status);
        int InsertLogTerminate(int MrndId, int UserId);
        int InsertLogAfterClone(int MrndId, int UserId);
        int InsertLogModified(int MrndId, int UserId);
        int InsertLogAddToPR(int MrndId, int UserId);
        int InsertLogIssueStock(int MrndId, int UserId);
        int InsertLogReceive(int MrndId, int UserId);
        int InsertLogConfirmation(int MrndId, int UserId);
        int InsertLogRejection(int MrndId, int UserId);
        int InsertStockReturned(int MrndId, int UserId);
        int InsertStockReturnForApproval(int MrndId, int UserId);
        int InsertDepartmetStockReturned(int MrndId, int UserId);
    }
    public class MrnDetailsStatusLogControllerImpl : MrnDetailsStatusLogController
    {
      //  public int InsertLog(int MrndId, int UserId, int Status)
        //{
            //DBConnection dbConnection = new DBConnection();
            //try
            //{
            //    MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
            //    return mrnDetailStatusLogDAO.InsertLog(MrndId, UserId, Status, dbConnection);
            //}
            //catch (Exception)
            //{
            //    dbConnection.RollBack();
            //    throw;
            //}
            //finally
            //{
            //    if (dbConnection.con.State == System.Data.ConnectionState.Open)
            //    {
            //        dbConnection.Commit();
            //    }
            //}
        //    return 1;
        //}

        public List<MRNDetailsStatusLog> MrnLogDetails(int MrndId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.GetMrnDStatusByMrnDId(MrndId, dbConnection);
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

        public int InsertLog(int MrndId, int UserId, int Status) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLog(MrndId, UserId, Status, dbConnection);
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
        public int InsertLogReceive(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLogReceive(MrndId, UserId, dbConnection);
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
        public int InsertLogConfirmation(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLogConfirmation(MrndId, UserId, dbConnection);
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

        public int InsertLogRejection(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLogRejection(MrndId, UserId, dbConnection);
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

        public int InsertStockReturned(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertStockReturned(MrndId, UserId, dbConnection);
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

        public int InsertDepartmetStockReturned(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertDepartmetStockReturned(MrndId, UserId, dbConnection);
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

        public int InsertStockReturnForApproval(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertStockReturnForApproval(MrndId, UserId, dbConnection);
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
        public int InsertLogIssueStock(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLogIssueStock(MrndId, UserId, dbConnection);
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

        public int InsertLogAddToPR(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLogAddToPR(MrndId, UserId, dbConnection);
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

        public int InsertLogModified(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLogModified(MrndId, UserId, dbConnection);
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
        public int InsertLogTerminate(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLogTerminate(MrndId, UserId, dbConnection);
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
        public int InsertLogAfterClone(int MrndId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                return mrnDetailStatusLogDAO.InsertLogAfterClone(MrndId, UserId, dbConnection);
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
        public int UpdateMRNLog(int mrndId, int userId, int Status)
        {
            //DBConnection dbConnection = new DBConnection();
            //try
            //{
            //    MrnDetailStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
            //    return mrnDetailStatusLogDAO.UpdateMRNLog(mrndId, userId, Status, dbConnection);
            //}
            //catch (Exception ex)
            //{
            //    dbConnection.RollBack();
            //    throw;
            //}
            //finally
            //{
            //    if (dbConnection.con.State == System.Data.ConnectionState.Open)
            //        dbConnection.Commit();
            //}
            return 1;
        }
    }
}
