using CLibrary.Common;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface EmailController
    {
        List<string> WHHeadandMRNCreatorEmails(int mrnId);
        List<string> MRNCreatorandMRNApproverEmails(int mrnId);
        List<string> MRNCreatorApproverissuedPersonEmails(int mrndId, int mrndInId);
        List<string> MRNCreatorDeliveredAndissuedPersonEmails(int mrndId, int mrndInId);
        string GetMRNCreatorEmail(int MrnId);
        string GetTRCreatorEmail(int TRId);
    }

    public class EmailControllerImpl : EmailController
    {
        public List<string> WHHeadandMRNCreatorEmails(int mrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                EmailDAO emailDAO = DAOFactory.CreateEmailDAO();
                return emailDAO.WHHeadandMRNCreatorEmails(mrnId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }


        public List<string> MRNCreatorandMRNApproverEmails(int mrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                EmailDAO emailDAO = DAOFactory.CreateEmailDAO();
                return emailDAO.MRNCreatorandMRNApproverEmails(mrnId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }


        public List<string> MRNCreatorApproverissuedPersonEmails(int mrndId, int mrndInId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                EmailDAO emailDAO = DAOFactory.CreateEmailDAO();
                return emailDAO.MRNCreatorApproverissuedPersonEmails(mrndId, mrndInId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }


        public List<string> MRNCreatorDeliveredAndissuedPersonEmails(int mrndId, int mrndInId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                EmailDAO emailDAO = DAOFactory.CreateEmailDAO();
                return emailDAO.MRNCreatorDeliveredAndissuedPersonEmails(mrndId, mrndInId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public string GetMRNCreatorEmail(int MrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                EmailDAO emailDAO = DAOFactory.CreateEmailDAO();
                return emailDAO.GetMRNCreatorEmail(MrnId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
        public string GetTRCreatorEmail(int TRId) {
            DBConnection dbConnection = new DBConnection();
            try {
                EmailDAO emailDAO = DAOFactory.CreateEmailDAO();
                return emailDAO.GetTRCreatorEmail(TRId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    }
}
