using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface GRNDIssueNoteControllerInterface
    {
        int addNewNote(List<GRNDIssueNote> notes);
        List<GRNDIssueNote> fetchGRNDIssueNoteList(int grndID);
    }
    class GRNDIssueNoteController : GRNDIssueNoteControllerInterface
    {
        public int addNewNote(List<GRNDIssueNote> notes)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDIssueNoteDAOInterface DAO = DAOFactory.CreateGRNDIssueNoteDAO();
                return DAO.addNewNote(notes, dbConnection);

            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<GRNDIssueNote> fetchGRNDIssueNoteList(int grndID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDIssueNoteDAOInterface DAO = DAOFactory.CreateGRNDIssueNoteDAO();
                return DAO.fetchGRNDIssueNoteList(grndID, dbConnection);

            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
    }
}
