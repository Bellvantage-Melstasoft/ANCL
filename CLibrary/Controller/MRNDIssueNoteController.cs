using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface MRNDIssueNoteControllerInterface
    {
        int addNewIssueNote(List<MRNDIssueNote> notes);
        int updateIssueNoteAfterDelivered(MRNDIssueNote note);
        int updateIssueNoteAfterReceived(MRNDIssueNote note);
        List<MRNDIssueNote> fetchDeliveredMrndINList(List<int> DepartmentIds);
        List<MRNDIssueNote> fetchReceivedMrndINList(List<int> DepartmentIds);
        List<MRNDIssueNote> fetchIssuedMrndINList(List<int> warehouseID);
        List<MRNDIssueNote> fetchDeliveredMrndINListWarehouse(List<int> warehouseID);
        int addNewIssueNoteonewarehouse(MRNDIssueNote note);
        int updateIssueNoteAfterDeliveredmrnissue(MRNDIssueNote note);
        List<MRNDIssueNote> FetchforIssueNote(List<int> warehouseID, int issunoteId);
        string FetchMRNREFNo(int issunoteId);
        List<MRNDIssueNote> FetchIssueNoteDetailsByMrnDetailsId(int mrndId);
        List<MRNDIssueNote> fetchDeliveredMrndINListByCompanyId(int CompanyId);
        List<MRNDIssueNote> fetchReceivedMrndINListByCompanyId(int CompanyId);
        List<MRNDIssueNote> IssueNoteDetails(int WarehouseId, List<int> DepartmentIds, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid);
        List<MRNDIssueNote> ReceivedMRNDetails(int CompanyId, List<int> WarehouseId, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid);
        List<MRNDIssueNote> fetchIssuedMrndINListByMonth(List<int> warehouseID, DateTime date);
        List<MRNDIssueNote> fetchIssuedMrndINListByMrnCode(List<int> warehouseID, string Code);
        List<MRNDIssueNote> fetchDeliveredMrndINListWarehouseByMonth(List<int> warehouseID, DateTime date);
        List<MRNDIssueNote> fetchDeliveredMrndINListWarehouseByMrnCode(List<int> warehouseID, string code);
        int updateIssueNoteBeforeConfirmation(MRNDIssueNote note);
        List<MRNDIssueNote> fetchDeliveredMrndINListToConfirm(List<int> DepartmentIds);
        List<MRNDIssueNote> fetchDeliveredMrndINListToConfirmByCompanyId(int CompanyId);
        List<MRNDIssueNote> fetchConfirmedMrndINList(List<int> DepartmentIds);
        List<MRNDIssueNote> fetchConfirmedMrndINListByCompanyId(int CompanyId);
        int updateIssueNoteAfterRejected(MRNDIssueNote note, int status);
        List<MRNDIssueNote> fetchRejectedMrndINListToConfirm(List<int> DepartmentIds);
        List<MRNDIssueNote> fetchRejectedMrndINListToConfirmByCompanyId(int CompanyId);
        List<MRNDIssueNote> fetchReturnedStockTowarehouse(List<int> DepartmentIds);
        List<MRNDIssueNote> fetchReturnedStockByCompanyId(int CompanyId);
        List<MRNDIssueNote> fetchConfirmedMrndINListForRetur(List<int> DepartmentIds);
        List<MRNDIssueNote> fetchConfirmedMrndINListByCompanyIdForReturn(int CompanyId);
        int updateIssueNoteForApproval(int MrndInID);
        List<MRNDIssueNote> fetchRejectedMrndINListToApprove();
        List<MRNDIssueNote> fetchRejectedMrndINListToApproveByCompanyId(int CompanyId);
    }
    class MRNDIssueNoteController : MRNDIssueNoteControllerInterface
    {
        public int addNewIssueNote(List<MRNDIssueNote> notes)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.addNewIssueNote(notes, dbConnection);

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

        public int updateIssueNoteForApproval(int MrndInID) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.updateIssueNoteForApproval(MrndInID, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public int addNewIssueNoteonewarehouse(MRNDIssueNote note)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.addNewIssueNoteonewarehouse(note, dbConnection);

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

        public List<MRNDIssueNote> fetchDeliveredMrndINList(List<int> DepartmentIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchDeliveredMrndINList(DepartmentIds, dbConnection);

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

        public List<MRNDIssueNote> fetchDeliveredMrndINListToConfirm(List<int> DepartmentIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchDeliveredMrndINListToConfirm(DepartmentIds, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<MRNDIssueNote> fetchRejectedMrndINListToConfirm(List<int> DepartmentIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchRejectedMrndINListToConfirm(DepartmentIds, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<MRNDIssueNote> fetchRejectedMrndINListToApprove() {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchRejectedMrndINListToApprove( dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<MRNDIssueNote> fetchReturnedStockTowarehouse(List<int> DepartmentIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchReturnedStockTowarehouse(DepartmentIds, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<MRNDIssueNote> fetchDeliveredMrndINListByCompanyId(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchDeliveredMrndINListByCompanyId(CompanyId, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchDeliveredMrndINListToConfirmByCompanyId(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchDeliveredMrndINListToConfirmByCompanyId(CompanyId, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchRejectedMrndINListToConfirmByCompanyId(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchRejectedMrndINListToConfirmByCompanyId(CompanyId, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchRejectedMrndINListToApproveByCompanyId(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchRejectedMrndINListToApproveByCompanyId(CompanyId, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchReturnedStockByCompanyId(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchReturnedStockByCompanyId(CompanyId, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<MRNDIssueNote> fetchDeliveredMrndINListWarehouse(List<int> warehouseID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchDeliveredMrndINListWarehouse(warehouseID, dbConnection);

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

        public List<MRNDIssueNote> fetchDeliveredMrndINListWarehouseByMonth(List<int> warehouseID, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchDeliveredMrndINListWarehouseByMonth(warehouseID, date, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<MRNDIssueNote> fetchDeliveredMrndINListWarehouseByMrnCode(List<int> warehouseID, string code) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchDeliveredMrndINListWarehouseByMrnCode(warehouseID, code, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchIssuedMrndINList(List<int> warehouseID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchIssuedMrndINList(warehouseID, dbConnection);

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
        public List<MRNDIssueNote> fetchIssuedMrndINListByMonth(List<int> warehouseID, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchIssuedMrndINListByMonth(warehouseID, date, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<MRNDIssueNote> fetchIssuedMrndINListByMrnCode(List<int> warehouseID, string Code) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchIssuedMrndINListByMrnCode(warehouseID, Code, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchReceivedMrndINList(List<int> DepartmentIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchReceivedMrndINList(DepartmentIds, dbConnection);

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

        public List<MRNDIssueNote> fetchConfirmedMrndINList(List<int> DepartmentIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchConfirmedMrndINList(DepartmentIds, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchConfirmedMrndINListForRetur(List<int> DepartmentIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchConfirmedMrndINListForRetur(DepartmentIds, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchReceivedMrndINListByCompanyId(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchReceivedMrndINListByCompanyId(CompanyId, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchConfirmedMrndINListByCompanyId(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchConfirmedMrndINListByCompanyId(CompanyId, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> fetchConfirmedMrndINListByCompanyIdForReturn(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.fetchConfirmedMrndINListByCompanyIdForReturn(CompanyId, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public int updateIssueNoteAfterDelivered(MRNDIssueNote note)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.updateIssueNoteAfterDelivered(note, dbConnection);

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

        public int updateIssueNoteAfterDeliveredmrnissue(MRNDIssueNote note)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.updateIssueNoteAfterDeliveredmrnissue(note, dbConnection);

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

        public int updateIssueNoteAfterReceived(MRNDIssueNote note)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.updateIssueNoteAfterReceived(note, dbConnection);

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

        public int updateIssueNoteAfterRejected(MRNDIssueNote note, int status) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.updateIssueNoteAfterRejected(note, status, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public int updateIssueNoteBeforeConfirmation(MRNDIssueNote note) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.updateIssueNoteBeforeConfirmation(note, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<MRNDIssueNote> FetchforIssueNote(List<int> warehouseID, int issunoteId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.FetchforIssueNote(warehouseID, issunoteId, dbConnection);

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

        public string FetchMRNREFNo(int issunoteId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.FetchMRNREFNo( issunoteId, dbConnection);

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

        public List<MRNDIssueNote> FetchIssueNoteDetailsByMrnDetailsId(int mrndId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.FetchIssueNoteDetailsByMrnDetailsId(mrndId, dbConnection);

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
        public List<MRNDIssueNote> IssueNoteDetails(int WarehouseId, List<int> DepartmentIds, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.IssueNoteDetails(WarehouseId, DepartmentIds, toDate, fromDate, companyId, itemid, maincategoryid, subcategoryid, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MRNDIssueNote> ReceivedMRNDetails(int CompanyId, List<int> WarehouseId, string toDate, string fromDate, int companyId, int itemid, int maincategoryid, int subcategoryid) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRNDIssueNoteDAOInterface DAO = DAOFactory.CreateMRNDIssueNoteDAO();
                return DAO.ReceivedMRNDetails(CompanyId, WarehouseId, toDate, fromDate, companyId, itemid, maincategoryid, subcategoryid, dbConnection);

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
    }
}
