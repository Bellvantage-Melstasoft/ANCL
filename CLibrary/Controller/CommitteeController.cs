using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface CommitteeController
    {
        List<Committee> FetchAllCommittee(int companyId);
        int ManageCommittee(int id , string committeeName,DateTime createdDate , int createdUser, int companyId ,string action, int CommiteeType);
        int DeleteCommittee(int id);
        List<CommitteeMember> FetchAllCommitteeMembers();
        int ManageCommitteeMember(int id, int committeeid, int designationId, int sequenceNo, int allowedApprovedCount, int canOveride, int overideDesignationId, DateTime effectiveDate, int userId, DateTime now, string action);
        int DeleteCommitteeMember(int id);
        int SaveUploadedtechcommitteefiles(List<TecCommitteeFileUpload> details);
        List<TecCommitteeFileUpload> Gettechcommitteefiles(int bidId, int tabulationId, string committeeType);
        int IsDocsUploaded(int bidId, int tabulationId, string committeeType);
        int FetchApprovalLimitCommittee(int limitId);
        int GetUploedasequence(int bidId, int tabulationId, string committeeType);
    }

    public class CommitteeControllerImpl : CommitteeController
    {
        public int DeleteCommittee(int id)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.DeleteCommittee(id, dbConnection);
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

        public int DeleteCommitteeMember(int id)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.DeleteCommitteeMember(id, dbConnection);
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

        public List<Committee> FetchAllCommittee(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.FetchAllCommittee(companyId , dbConnection);
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

        public List<CommitteeMember> FetchAllCommitteeMembers()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.FetchAllCommitteeMembers(dbConnection);
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

        public int ManageCommittee(int id , string committeeName, DateTime createdDate, int createdUser, int companyId, string action, int CommiteeType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                if (action == "Save")
                {
                    CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                    return committeeDAO.SaveCommittee(id,committeeName, createdDate, createdUser, companyId,  CommiteeType, dbConnection);
                }else
                {
                    CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                    return committeeDAO.UpdateCommittee(id,committeeName, createdDate, createdUser, companyId,  CommiteeType, dbConnection);
                }
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

        public int ManageCommitteeMember(int id, int committeeid, int designationId, int sequenceNo, int allowedApprovedCount, int canOveride, int overideDesignationId, DateTime effectiveDate, int userId, DateTime now, string action)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                if (action == "Save")
                {
                    CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                    return committeeDAO.SaveCommitteeMember(id, committeeid, designationId, sequenceNo, allowedApprovedCount, canOveride, overideDesignationId , effectiveDate , userId, now , dbConnection);
                }
                else
                {
                    CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                    return committeeDAO.UpdateCommitteeMember(id, committeeid, designationId, sequenceNo, allowedApprovedCount, canOveride, overideDesignationId, effectiveDate, userId, now, dbConnection);
                }
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
        public int SaveUploadedtechcommitteefiles(List<TecCommitteeFileUpload> details)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.SaveUploadedtechcommitteefiles(details, dbConnection);
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

        public List<TecCommitteeFileUpload> Gettechcommitteefiles(int bidId, int tabulationId, string committeeType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.Gettechcommitteefiles(bidId, tabulationId, committeeType, dbConnection);
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

        public int IsDocsUploaded(int bidId, int tabulationId, string committeeType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.IsDocsUploaded(bidId, tabulationId, committeeType,dbConnection);
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

        public int FetchApprovalLimitCommittee(int limitId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.FetchApprovalLimitCommittee(limitId, dbConnection);
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

        public int GetUploedasequence(int bidId, int tabulationId, string committeeType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                CommitteeDAO committeeDAO = DAOFactory.CreateCommitteeDAO();
                return committeeDAO.GetUploedasequence(bidId, tabulationId, committeeType, dbConnection);
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
