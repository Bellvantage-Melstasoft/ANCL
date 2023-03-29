using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;

namespace CLibrary.Infrastructure {
    public interface TrdIssueNoteBatchesDAO {
        List<TrdIssueNoteBatches> getTrIssuedInventoryBatches(int trdInId, DBConnection dbConnection);
        List<TrdIssueNoteBatches> getTrReceivedInventoryBatches(int trdInId, DBConnection dbConnection);


    }
    public class TrdIssueNoteBatchesDAOSQLImpl : TrdIssueNoteBatchesDAO {


        public List<TrdIssueNoteBatches> getTrIssuedInventoryBatches(int trdInId, DBConnection dbConnection) {

            List<TrdIssueNoteBatches> trIssuedInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TRD_ISSUE_NOTE_BATCHES AS TINB " +
                                            "INNER JOIN (SELECT BATCH_ID, BATCH_CODE FROM WAREHOUSE_INVENTORY_BATCHES) AS WIB ON WIB.BATCH_ID = TINB.BATCH_ID " +
                                            "WHERE TRD_IN_ID = "+ trdInId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                trIssuedInventoryBatches = dataAccessObject.ReadCollection<TrdIssueNoteBatches>(dbConnection.dr);
            }

            return trIssuedInventoryBatches;
        }

        public List<TrdIssueNoteBatches> getTrReceivedInventoryBatches(int trdInId, DBConnection dbConnection) {

            List<TrdIssueNoteBatches> trReceivedInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM TRD_ISSUE_NOTE_BATCHES AS TINB " +
                                            "INNER JOIN (SELECT BATCH_ID, BATCH_CODE FROM WAREHOUSE_INVENTORY_BATCHES) AS WIB ON WIB.BATCH_ID = TINB.BATCH_ID " +
                                            "INNER JOIN (SELECT TRD_IN_ID, STATUS FROM TRD_ISSUE_NOTE) AS TRIN ON TRIN.TRD_IN_ID = TINB.TRD_IN_ID "+
                                            "WHERE TINB.TRD_IN_ID = " + trdInId + " AND TRIN.STATUS = 3 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                trReceivedInventoryBatches = dataAccessObject.ReadCollection<TrdIssueNoteBatches>(dbConnection.dr);
            }

            return trReceivedInventoryBatches;
        }
    }
    }
