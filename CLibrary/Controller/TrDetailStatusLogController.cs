using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface TrDetailStatusLogController
    {
        int UpdateTRLog(int trdId, int userId, int Status);
        List<TrDetailStatusLog> TRLogDetails(int TRDId);

    }

    public class TrDetailStatusLogControllerImpl : TrDetailStatusLogController
    {
        public int UpdateTRLog(int trdId, int userId, int Status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TrDetailStatusLogDAO trDetailStatusLogDAO = DAOFactory.CreateTrDetailStatusLogDAO();
                return trDetailStatusLogDAO.UpdateTRLog(trdId, userId, Status, dbConnection);
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

        public List<TrDetailStatusLog> TRLogDetails(int TRDId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TrDetailStatusLogDAO trDetailStatusLogDAO = DAOFactory.CreateTrDetailStatusLogDAO();
                return trDetailStatusLogDAO.TRLogDetails(TRDId, dbConnection);
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
    }
    }
