using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;

namespace CLibrary.Infrastructure {
    public interface MrndIssueNoteBatchesDAO {
        List<MrndIssueNoteBatches> getMrnIssuedInventoryBatches(int mrndInId, DBConnection dbConnection);
        List<MrndIssueNoteBatches> getMrnReceivedInventoryBatches(int mrndInId, DBConnection dbConnection);
        List<MrndIssueNoteBatches> getInventoryBatches(int mrndInId, DBConnection dbConnection);
    }
    public class MrndIssueNoteBatchesDAOSQLImpl : MrndIssueNoteBatchesDAO {

        public List<MrndIssueNoteBatches> getMrnIssuedInventoryBatches(int mrndInId, DBConnection dbConnection) {

            List<MrndIssueNoteBatches> mrnIssuedInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MINB.*, WIB.BATCH_CODE,WIB.WAREHOUSE_ID, WIB.EXPIRY_DATE, AI.ITEM_ID, AI.STOCK_MAINTAINING_TYPE,MD.MRND_ID, MD.MRN_ID, SUM(DRB.RETURN_QTY) AS RETURN_QTY  FROM MRND_ISSUE_NOTE_BATCHES AS MINB " +
                                            "INNER JOIN(SELECT BATCH_ID, BATCH_CODE,EXPIRY_DATE, WAREHOUSE_ID FROM WAREHOUSE_INVENTORY_BATCHES) AS WIB ON WIB.BATCH_ID = MINB.BATCH_ID " +
                                           "INNER JOIN MRND_ISSUE_NOTE AS MRIN ON MINB.MRND_IN_ID = MRIN.MRND_IN_ID " +
                                           "LEFT JOIN (SELECT ITEM_ID, STOCK_MAINTAINING_TYPE FROM ADD_ITEMS ) AS AI ON AI.ITEM_ID = MRIN.ITEM_ID " +
                                           "LEFT JOIN (SELECT MRND_ID, MRN_ID FROM MRN_DETAILS) AS MD ON MD.MRND_ID = MRIN.MRND_ID " +
                                            "LEFT JOIN (SELECT MRND_IN_ID, BATCH_ID, RETURN_QTY FROM DEPARTMENT_RETURN_BATCH) AS DRB ON DRB.BATCH_ID = WIB.BATCH_ID AND DRB.MRND_IN_ID = MINB.MRND_IN_ID " +
                                           "WHERE MINB.MRND_IN_ID = " + mrndInId + " " +
                                           "GROUP BY WIB.BATCH_CODE,WIB.WAREHOUSE_ID, WIB.EXPIRY_DATE, AI.ITEM_ID, AI.STOCK_MAINTAINING_TYPE,MD.MRND_ID, MD.MRN_ID, MINB.BATCH_ID, MINB.ISSUED_QTY, MINB.ISSUED_STOCK_VALUE, MINB.MEASUREMENT_ID, MINB.MRND_IN_ID ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnIssuedInventoryBatches = dataAccessObject.ReadCollection<MrndIssueNoteBatches>(dbConnection.dr);
            }

            return mrnIssuedInventoryBatches;
        }

        

        public List<MrndIssueNoteBatches> getMrnReceivedInventoryBatches(int mrndInId, DBConnection dbConnection) {

            List<MrndIssueNoteBatches> mrnReceivedInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE_BATCHES AS MINB " +
                                            "INNER JOIN (SELECT BATCH_ID, BATCH_CODE,EXPIRY_DATE FROM WAREHOUSE_INVENTORY_BATCHES) AS WIB ON WIB.BATCH_ID = MINB.BATCH_ID " +
                                            "INNER JOIN (SELECT MRND_IN_ID, STATUS FROM MRND_ISSUE_NOTE) AS MRIN ON MRIN.MRND_IN_ID = MINB.MRND_IN_ID " +
                                            "WHERE MINB.MRND_IN_ID = "+ mrndInId + " AND MRIN.STATUS = 3 ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnReceivedInventoryBatches = dataAccessObject.ReadCollection<MrndIssueNoteBatches>(dbConnection.dr);
            }

            return mrnReceivedInventoryBatches;
        }

        public List<MrndIssueNoteBatches> getInventoryBatches(int mrndInId, DBConnection dbConnection) {

            List<MrndIssueNoteBatches> mrnReceivedInventoryBatches;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM MRND_ISSUE_NOTE_BATCHES AS MINB " +
                                           "WHERE MINB.MRND_IN_ID = " + mrndInId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnReceivedInventoryBatches = dataAccessObject.ReadCollection<MrndIssueNoteBatches>(dbConnection.dr);
            }

            return mrnReceivedInventoryBatches;
        }



    }
}
