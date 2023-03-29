using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface PR_BillOfMeterialDAO
    {
        int SaveBillOfMeterial(int PrId, int ItemId, int SeqNo, string Meterial, string Description, int IsActive, DateTime CreatedDatetime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, DBConnection dbConnection);
        List<PR_BillOfMeterial> GetList(int PrId, int ItemId, DBConnection dbConnection);
        List<PR_BillOfMeterial> GetListWithSupplierBOM(int PrId, int ItemId, DBConnection dbConnection);
        List<PR_BillOfMeterial> GetListRejected(int PrId, DBConnection dbConnection);
        int DeletePRBom(int PrId, int ItemId, DBConnection dbConnection);
        List<PR_BillOfMeterial> GetListForPrint(List<int> PrdId, DBConnection dbConnection);

        int DeletePrBoMTrash(int PrId, int ItemId, DBConnection dbConnection);
    }

    

    public class PR_BillOfMeterialDAOSQLImpl : PR_BillOfMeterialDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveBillOfMeterial(int PrId, int ItemId, int SeqNo, string Meterial, string Description, int IsActive, DateTime CreatedDatetime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_BOM (PR_ID, ITEM_ID, SEQ_NO, MATERIAL, DESCRIPTION, IS_ACTIVE, CREATED_DATETIME, CREATED_BY, UPDATED_DATETIME, UPDATED_BY) VALUES ( " + PrId + ", " + ItemId + " , " + SeqNo + ", '" + Meterial + "', '" + Description + "', " + IsActive + ", '" + CreatedDatetime + "', '" + CreatedBy + "' ,'" + UpdatedDateTime + "', '" + UpdatedBy + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_BillOfMeterial> GetListWithSupplierBOM(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM  " + dbLibrary + ".PR_BOM AS A "
                + "INNER JOIN (SELECT PR_ID AS pr,ITEM_ID AS item,COMPLY FROM  " + dbLibrary + ".SUPPLIER_BOM) AS B "
                + "ON A.PR_ID=B.pr AND A.ITEM_ID=B.item "
                + "WHERE A.PR_ID = " + PrId + " AND A.ITEM_ID = " + ItemId + " AND B.pr = " + PrId + " AND B.item = " + ItemId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_BillOfMeterial>(dbConnection.dr);
            }
        }

        public List<PR_BillOfMeterial> GetList(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM  " + dbLibrary + ".PR_BOM WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_BillOfMeterial>(dbConnection.dr);
            }
        }

        public List<PR_BillOfMeterial> GetListRejected(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_BOM WHERE PR_ID = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_BillOfMeterial>(dbConnection.dr);
            }
        }

        public int DeletePRBom(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_BOM SET IS_ACTIVE = 0  WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeletePrBoMTrash(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".PR_BOM   WHERE PR_ID = " + PrId + " AND ITEM_ID = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_BillOfMeterial> GetListForPrint(List<int> PrdId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM  " + dbLibrary + ".PR_BOM AS A " +
                "INNER JOIN (SELECT ITEM_ID, PRD_ID FROM PR_DETAIL ) AS PD ON PD. PRD_ID = A.PRD_ID " +
                "INNER JOIN (SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS ) AS AI ON AI. ITEM_ID = PD.ITEM_ID " +
                "WHERE A.PRD_ID IN ("+String.Join(",", PrdId) +")";
                
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_BillOfMeterial>(dbConnection.dr);
            }
        }
    }
}
