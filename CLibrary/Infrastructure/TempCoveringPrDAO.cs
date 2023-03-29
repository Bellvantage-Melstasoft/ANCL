using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {

    public interface TempCoveringPrDAO {
        int AddCoveringPrData(List<TempCoverigPr> PrDetails, int ParentPrId, int PoId, DBConnection dbConnection);
        int DeleteCoveringPrData(int ParentPrId, DBConnection dbConnection);
    }

    public class TempCoveringPrDAOSQLImpl : TempCoveringPrDAO {

        public int AddCoveringPrData(List<TempCoverigPr> PrDetails, int ParentPrId, int PoId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            for (int i = 0; i < PrDetails.Count; i++) {
                
                dbConnection.cmd.CommandText += "INSERT INTO TEMP_PR_DETAILS_FOR_COVERING_PR ([PARENT_PR_ID], [POD_ID], [ITEM_ID], [EXTRA_QTY], [RECEIVED_QTY], [PO_ID], [PRD_ID] ) " +
                    "VALUES (" + ParentPrId + ", " + PrDetails[i].PodId + ", " + PrDetails[i].ItemId + ", " + PrDetails[i].ExtraQty + ", " + PrDetails[i].ReceivedQty + ", " + PoId + ", " + PrDetails[i].PrdId + ") ";
            }
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteCoveringPrData(int ParentPrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM TEMP_PR_DETAILS_FOR_COVERING_PR WHERE PARENT_PR_ID = " + ParentPrId + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
    }
