using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;

namespace CLibrary.Controller {
    public interface MrndIssueNoteBatchController {
        List<MrndIssueNoteBatches> getMrnIssuedInventoryBatches(int mrndInId);
        List<MrndIssueNoteBatches> getMrnReceivedInventoryBatches(int mrndInId);
        List<MrndIssueNoteBatches> getInventoryBatches(int mrndInId);
    }

    public class MrndIssueNoteBatchControllerImpl : MrndIssueNoteBatchController {

        public List<MrndIssueNoteBatches> getMrnIssuedInventoryBatches(int mrndInId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrndIssueNoteBatchesDAO mrndIssueNoteBatches = DAOFactory.CreateMrndIssueNoteBatchesDAO();
                return mrndIssueNoteBatches.getMrnIssuedInventoryBatches(mrndInId, dbConnection);
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

        public List<MrndIssueNoteBatches> getMrnReceivedInventoryBatches(int mrndInId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrndIssueNoteBatchesDAO mrndIssueNoteBatches = DAOFactory.CreateMrndIssueNoteBatchesDAO();
                return mrndIssueNoteBatches.getMrnReceivedInventoryBatches(mrndInId, dbConnection);
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

        public List<MrndIssueNoteBatches> getInventoryBatches(int mrndInId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrndIssueNoteBatchesDAO mrndIssueNoteBatches = DAOFactory.CreateMrndIssueNoteBatchesDAO();
                return mrndIssueNoteBatches.getInventoryBatches(mrndInId, dbConnection);
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
