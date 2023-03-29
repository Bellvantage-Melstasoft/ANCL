using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface TempQuotationForCoverigPrDAO {
        int AddQuotationData(List<TempQuotationsForCoverinPr> QuotatioDetails, int ParentPrId, DBConnection dbConnection);
        int DeleteQuotationData(int ParentPrId, DBConnection dbConnection);
    }
    public class TempQuotationForCoverigPrDAOSQLImpl : TempQuotationForCoverigPrDAO {

        public int AddQuotationData(List<TempQuotationsForCoverinPr> QuotatioDetails, int ParentPrId,  DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            for (int i = 0; i < QuotatioDetails.Count; i++) {

                dbConnection.cmd.CommandText += "INSERT INTO TEMP_QUOTATION_DETAILS_FOR_COVERING_PR ([QUOTATIO_ID], [QUOTATIO_ITEM_ID], [ITEM_ID], [UNIT_PRICE], [QTY], [VAT], [NET_TOTAL],[SUB_TOTAL], [PARNT_PR_ID], [SUPPLIER_ID] ) " +
                    "VALUES (" + QuotatioDetails[i].QuotationId + ", " + QuotatioDetails[i].QuotationItemId + ", " + QuotatioDetails[i].ItemId + "," + QuotatioDetails[i].UnitPrice + ", " + QuotatioDetails[i].Qty + ", " + QuotatioDetails[i].Vat + ", " + QuotatioDetails[i].NetTotal + ", " + QuotatioDetails[i].SubTotal + ", "+ ParentPrId + ",  " + QuotatioDetails[i].SupplierId + " ) ";
            }
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int DeleteQuotationData(int ParentPrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM TEMP_QUOTATION_DETAILS_FOR_COVERING_PR WHERE PARNT_PR_ID = " + ParentPrId + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
    }
