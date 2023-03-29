using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface MRNControllerInterface
    {
        int saveMrn(MrnMaster mrn);
        int ApproveOrRejectMrn(int mrnID, int isApproved);
        int DeleteMRND(int mrndID);
        int addMRND(MrnDetails mrnd);
        int updateMRN(MrnMaster mrn);
        int updateMRND(MrnDetails mrnd);
        int updateMRNDIssuedQty(int mrndID, decimal issuedQty);
        int updateMRNDReceivedQty(int mrndID, decimal issuedQty);
        int changeMRNDStaus(int mrndID, int status);
        int changeMRNStaus(int mrnID, int status);
        List<MrnMaster> fetchApprovedOrRejectedMrnList(int companyID, int isApproved);
        List<MrnMaster> fetchMrnList(int subDepartmentID);
        List<MrnDetails> fetchMrnDList(int mrnID);
        List<MrnMaster> fetchSubmittedMrnList(int companyID);
        List<MrnDetails> fetchSubmittedMrnDList(int mrnID, int companyID);
        MrnMaster getMRNM(int mrnID);
        MrnDetails getMRND(int mrndID);
        int updateMRNAfterIssue(int mrnID);
        List<MrnMaster> AdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype);
        List<MrnMaster> FetchMyMrnByBasicSearchByMonth(int CreatedBy, DateTime date);
        MrnMaster FetchMyMrnByBasicSearchByMrId(int CreatedBy, int mrnId);
    }

    class MRNController : MRNControllerInterface
    {
        public int addMRND(MrnDetails mrnd)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.addMRND(mrnd,dbConnection);

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

       

        public int ApproveOrRejectMrn(int mrnID, int isApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.ApproveOrRejectMrn(mrnID, isApproved, dbConnection);

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

        public int changeMRNDStaus(int mrndID, int status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.changeMRNDStaus(mrndID, status, dbConnection);

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

        public int changeMRNStaus(int mrnID, int status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.changeMRNStaus(mrnID, status, dbConnection);

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

        public int DeleteMRND(int mrndID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.DeleteMRND(mrndID, dbConnection);

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

        public List<MrnMaster> fetchApprovedOrRejectedMrnList(int companyID, int isApproved)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.fetchApprovedOrRejectedMrnList(companyID, isApproved, dbConnection);

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

        public List<MrnDetails> fetchMrnDList(int mrnID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.fetchMrnDList(mrnID, dbConnection);

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

        public List<MrnMaster> fetchMrnList(int subDepartmentID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.fetchMrnList(subDepartmentID, dbConnection);

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

        public List<MrnDetails> fetchSubmittedMrnDList(int mrnID, int companyID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.fetchSubmittedMrnDList(mrnID, companyID, dbConnection);

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

        public List<MrnMaster> fetchSubmittedMrnList(int companyID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.fetchSubmittedMrnList(companyID, dbConnection);

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

        public MrnDetails getMRND(int mrndID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.getMRND(mrndID, dbConnection);

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

        public MrnMaster getMRNM(int mrnID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.getMRNM(mrnID, dbConnection);

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

        public int saveMrn(MrnMaster mrn)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.saveMrn(mrn, dbConnection);

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

        public int updateMRN(MrnMaster mrn)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.updateMRN(mrn, dbConnection);

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

        public int updateMRNAfterIssue(int mrnID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.updateMRNAfterIssue(mrnID, dbConnection);

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

        public int updateMRND(MrnDetails mrnd)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.updateMRND(mrnd, dbConnection);

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

        public int updateMRNDIssuedQty(int mrndID, decimal issuedQty)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.updateMRNDIssuedQty(mrndID, issuedQty, dbConnection);

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

        public int updateMRNDReceivedQty(int mrndID, decimal issuedQty)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.updateMRNDReceivedQty(mrndID, issuedQty, dbConnection);

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
        public List<MrnMaster> AdvanceSearch(int companyId, int categoryId, int subDepartmentID, int serchtype, string searchkey, string usertype)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.AdvanceSearch( companyId,  categoryId,  subDepartmentID,  serchtype,  searchkey,  usertype, dbConnection);

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

        public List<MrnMaster> FetchMyMrnByBasicSearchByMonth(int CreatedBy, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.FetchMrnsByCreatedUser(CreatedBy, date, dbConnection);

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
        
        public MrnMaster FetchMyMrnByBasicSearchByMrId(int CreatedBy, int mrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnDAOInterface mrnDAO = DAOFactory.CreateMRNDAO();
                return mrnDAO.FetchMyMrnByBasicSearchByMrId(CreatedBy, mrnId, dbConnection);
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
