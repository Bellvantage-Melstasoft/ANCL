using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface GRNDIssueNoteDAOInterface
    {
        int addNewNote(List<GRNDIssueNote> notes, DBConnection dbConnection);
        List<GRNDIssueNote> fetchGRNDIssueNoteList(int grndID, DBConnection dbConnection);
    }
    class GRNDIssueNoteDAO : GRNDIssueNoteDAOInterface
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int addNewNote(List<GRNDIssueNote> notes, DBConnection dbConnection)
        {
            string sql = "";
            foreach(GRNDIssueNote note in notes)
            {
                sql += "INSERT INTO " + dbLibrary + ".GRND_ISSUE_NOTE (GRND_ID,ITEM_ID,WAREHOUSE_ID,ISSUED_QTY,ISSUED_BY,ISSUED_ON,ISSUED_STOCK_VALUE) VALUES " +
                       "(" + note.GrndID + "," + note.ItemID + "," + note.WarehouseID + "," + note.IssuedQty + "," + note.IssuedBy + ",'" +  LocalTime.Now + "'," + note.IssuedStockValue + "); ";
            }
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql; 
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GRNDIssueNote> fetchGRNDIssueNoteList(int grndID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM GRND_ISSUE_NOTE AS GRNDIN "+
                                            "INNER JOIN (SELECT WAREHOUSE_ID, LOCATION FROM WAREHOUSE)AS W ON GRNDIN.WAREHOUSE_ID = W.WAREHOUSE_ID "+
                                            "WHERE GRNDIN.GRND_ID = "+grndID;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GRNDIssueNote>(dbConnection.dr);
            }
        }
    }
}
