using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;


namespace CLibrary.Controller
{
    public interface TRDetailsController
    {
        List<TR_Details> fetchTRDList(int trId, int CompanyId);
        int updateTRD(TR_Details trd);
        int addTRD(TR_Details TRD);
        int DeleteTRD(int trdid);
        TR_Details GetTrdTerminationDetails(int trdId);
        int TerminateTRD(int TrID, int TrdID, int TerminatedBy, string Remarks);
        int changeTRDStaus(int trdID, int status);
        int updateTRAfterIssue(int TRID);
        List<TR_Details> fetchSubmittedTrDList(int trId, int companyID);
        List<TR_Details> fetchTrdItemList(int TrID, int CompanyId);
        TR_Details GetTrd(int trdId);
    }

    public class TRDetailsControllerImpl : TRDetailsController
    {
        public List<TR_Details> fetchTRDList(int trId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.fetchTRDList(trId, CompanyId, dbConnection);

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

        public int updateTRD(TR_Details trd)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.updateTRD(trd, dbConnection);

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

        public int addTRD(TR_Details TRD)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.addTRD(TRD, dbConnection);

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

        public int DeleteTRD(int trdid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.DeleteTRD(trdid, dbConnection);

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
        public TR_Details GetTrdTerminationDetails(int trdId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.GetTrdTerminationDetails(trdId, dbConnection);

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
        
        public int TerminateTRD(int TrID, int TrdID, int TerminatedBy, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.TerminateTRD(TrID, TrdID, TerminatedBy, Remarks, dbConnection);

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

        public int changeTRDStaus(int trdID, int status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.changeTRDStaus(trdID, status, dbConnection);

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
        public int updateTRAfterIssue(int TRID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.updateTRAfterIssue(TRID, dbConnection);

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

        public List<TR_Details> fetchSubmittedTrDList(int trId, int companyID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.fetchSubmittedTrDList(trId, companyID, dbConnection);

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

        public List<TR_Details> fetchTrdItemList(int TrID, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.fetchTrdItemList(TrID, CompanyId, dbConnection);

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

        public TR_Details GetTrd(int trdId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TRDetailsDAO TrDAO = DAOFactory.CreateTRDetailsDAO();
                return TrDAO.GetTrd(trdId, dbConnection);

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
