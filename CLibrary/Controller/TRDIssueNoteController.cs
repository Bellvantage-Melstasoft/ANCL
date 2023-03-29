using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
namespace CLibrary.Controller
{
    public interface TRDIssueNoteController
    {
        List<TRDIssueNote> fetchDeliveredTRdINList(List<int> warehouseIds);
        List<TRDIssueNote> fetchReceivedTrdINList(List<int> warehouseIds);
        List<TRDIssueNote> fetchIssueNoteDetails(int TrdId);
        List<TRDIssueNote> IssueNoteDetails(int WarehouseId, List<int> WarehouseIds, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid);
        List<TRDIssueNote> ReceivedTRDetails(int ReceivedToWarehouse, List<int> WarehouseId, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid);
    }

    public class TRDIssueNoteControllerImpl : TRDIssueNoteController
    {
        public List<TRDIssueNote> fetchDeliveredTRdINList(List<int> warehouseIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDIssueNoteDAO tRDIssueNoteDAO = DAOFactory.CreateTRDIssueNoteDAO();
                return tRDIssueNoteDAO.fetchDeliveredTRdINList(warehouseIds, dbConnection);
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
            throw new NotImplementedException();
        }


        public List<TRDIssueNote> fetchReceivedTrdINList(List<int> warehouseIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDIssueNoteDAO tRDIssueNoteDAO = DAOFactory.CreateTRDIssueNoteDAO();
                return tRDIssueNoteDAO.fetchReceivedTrdINList(warehouseIds, dbConnection);
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
            throw new NotImplementedException();
        }

        public List<TRDIssueNote> fetchIssueNoteDetails(int TrdId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDIssueNoteDAO tRDIssueNoteDAO = DAOFactory.CreateTRDIssueNoteDAO();
                return tRDIssueNoteDAO.fetchIssueNoteDetails(TrdId, dbConnection);
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
            throw new NotImplementedException();
        }

        public List<TRDIssueNote> IssueNoteDetails(int WarehouseId, List<int> WarehouseIds, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDIssueNoteDAO tRDIssueNoteDAO = DAOFactory.CreateTRDIssueNoteDAO();
                return tRDIssueNoteDAO.IssueNoteDetails(WarehouseId, WarehouseIds, toDate, fromDate, companyId, itemid, maincategoryid, subcategoryid, dbConnection);
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
            throw new NotImplementedException();
        }


        public List<TRDIssueNote> ReceivedTRDetails(int ReceivedToWarehouse, List<int> WarehouseId, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRDIssueNoteDAO tRDIssueNoteDAO = DAOFactory.CreateTRDIssueNoteDAO();
                return tRDIssueNoteDAO.ReceivedTRDetails(ReceivedToWarehouse, WarehouseId, toDate, fromDate, companyId, itemid, maincategoryid, subcategoryid, dbConnection);
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
            throw new NotImplementedException();
        }
    }
}
