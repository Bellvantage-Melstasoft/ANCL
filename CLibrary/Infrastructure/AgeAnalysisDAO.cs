using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Infrastructure
{
    public interface AgeAnalysisDAO
    {
        List<AgeAnalysis> GetAgeAnalysis(DBConnection dbConnection);

    }
    public class AgeAnalysisDAOImpl : AgeAnalysisDAO
    {
        public List<AgeAnalysis> GetAgeAnalysis(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PO.PO_CODE,c.CREATED_DATE,c.GOOD_RECEIVED_DATE,d.SUPPLIER_ID,d.SUPPLIER_NAME " +
                ",e.ITEM_NAME,b.ITEM_PRICE,b.QUANTITY,b.RECEIVED_QTY,b.WAITING_QTY,b.TOTAL_AMOUNT,MR.SUB_DEPARTMENT_ID,f.PURCHASING_OFFICER FROM PO_GRN a " +
                "INNER JOIN PO_MASTER PO ON PO.PO_ID=a.PO_ID " +
                "INNER JOIN PR_MASTER PR ON PR.PR_ID=PO.BASED_PR " +
                "INNER JOIN MRN_MASTER MR ON MR.MRN_ID=PR.MRN_ID " +
                "INNER JOIN PO_DETAILS b ON b.PO_ID=a.PO_ID " +
                "INNER JOIN GRN_MASTER c ON a.GRN_ID=c.GRN_ID " +
                "INNER JOIN SUPPLIER d ON d.SUPPLIER_ID=c.SUPPLIER_ID " +
                "INNER JOIN ADD_ITEMS e ON e.ITEM_ID= b.ITEM_ID " +
                "INNER JOIN BIDDING f ON f.PR_ID=PR.PR_ID ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AgeAnalysis>(dbConnection.dr);
            }
        }
    }
}
