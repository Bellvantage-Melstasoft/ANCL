using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;

namespace CLibrary.Controller {
    public interface TrdIssueNoteBatchController {
        List<TrdIssueNoteBatches> getTrIssuedInventoryBatches(int trndInId);
        List<TrdIssueNoteBatches> getTrReceivedInventoryBatches(int trndInId);
    }

    public class TrdIssueNoteBatchControllerImpl : TrdIssueNoteBatchController {
   

            public List<TrdIssueNoteBatches> getTrIssuedInventoryBatches(int trndInId) {
                DBConnection dbConnection = new DBConnection();
                try {
                TrdIssueNoteBatchesDAO trdIssueNoteBatchesDAO = DAOFactory.CreateTrdIssueNoteBatchesDAO();
                    return trdIssueNoteBatchesDAO.getTrIssuedInventoryBatches(trndInId, dbConnection);
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

        public List<TrdIssueNoteBatches> getTrReceivedInventoryBatches(int trndInId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TrdIssueNoteBatchesDAO trdIssueNoteBatchesDAO = DAOFactory.CreateTrdIssueNoteBatchesDAO();
                return trdIssueNoteBatchesDAO.getTrReceivedInventoryBatches(trndInId, dbConnection);
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
