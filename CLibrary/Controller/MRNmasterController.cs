using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;
using System.Data;

namespace CLibrary.Controller
{
    public interface MRNmasterController
    {
        int FetchMRCode(int CompanyId);
        int SaveMRMaster(int CompanyId, int SubDeparmentId, DateTime CreatedDate, string description, DateTime ExpectedDate, string CreatedBy, int Status, int isApproved, int isActive, string QuotationFor, int PrTypeId, string ExpenseType, string PrProcedure, string PurchaseType, int WarehouseID, int itemCatId);
        List<MRN_Master> FetchDetailsToEdit(int CompanyId);
        int GetDetailsByMrnCode(int CompanyId, string MrnCode);
        int UpdateMRMasterwithStoreKeeper(int mrnId, int userid);
        List<MrnDetails> fetchSubmittedMrnDListbywarehouse(int mrnID, int warehouseId);
        List<MrnDetails> fetchSubmittedMrnDList(int mrnID, int companyID);
        MRN_Master FetchMRNByMrnId(int Mrnid);
        int UpdateMRMaster(int Mrnid, int CompanyId, int SubDeparmentId, DateTime CreatedDate, string description, DateTime ExpectedDate, string CreatedBy, int Status, int isApproved, int isActive, string QuotationFor, int PrTypeId, string ExpenseType, string PrProcedure, string PurchaseType);
        List<MrnMaster> fetchSubmittedMrnListbywarehouseId(int companyID, int warehouseId);
        List<MrnMaster> fetchSubmittedMrnList(int companyID);
        int updateMRNDIssuedQty(int mrndID, int issuedQty);
        int updateMRNDReceivedQty(int mrndID, int receivedQty);
        int changeMRNDStaus(int mrndID, int status);
        int updateMRNAfterIssue(int mrnID);
        int AutoAssignStorekerper(int mrnID, int categoryId);
        List<MrnMaster> GetMRNListForViewMyMRN(int companyId, int userId);
        List<MrnMaster> GetMRNListForMrnInquiry(int companyId);
        List<MrnMaster> AdvanceSearchMRNForInquiry(int companyId, int searchBy, int categoryId, int subdepartmentId, string searchText);

        DataTable GetMRNCountForDashBoard();
        List<MrnMaster> FetchMRNToEdit(int companyId, int subDepatmentId);
        List<MrnDetails> FetchMRNItemDetails(int mrnID, int companyId);
        List<MrnMaster> fetchSubmittedMrnListForOther(int companyId);
        List<MrnMaster> getMrnByDepartment(int companyId, List<int> departmentId);
        MrnMaster GetMRNMasterByMrnId(int mrnID);
        List<MrnMaster> FetchMrnByMrnCode(int CompanyId, string mrnCode);
        List<MrnMaster> FetchMRNByDate(int companyId, DateTime ToDate, DateTime FromDate);
    }
    public class MRNmasterControllerImpl : MRNmasterController
    {


        public int SaveMRMaster(int CompanyId, int SubDeparmentId, DateTime CreatedDate, string description, DateTime ExpectedDate, string CreatedBy, int Status, int isApproved, int isActive, string QuotationFor, int PrTypeId, string ExpenseType, string PrProcedure, string PurchaseType, int WarehouseID, int itemCatId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();

                return mrn_MasterDAO.SaveMRMaster(CompanyId, SubDeparmentId, CreatedDate, description, ExpectedDate, CreatedBy, Status, isApproved, isActive, QuotationFor, PrTypeId, ExpenseType, PrProcedure, PurchaseType, WarehouseID, itemCatId, dbConnection);
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
        public List<MRN_Master> FetchDetailsToEdit(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.FetchDetailsToEdit(CompanyId, dbConnection);
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

        public int UpdateMRMasterwithStoreKeeper(int mrnId, int userid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.UpdateMRMasterwithStoreKeeper(mrnId, userid, dbConnection);
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

        public List<MrnDetails> fetchSubmittedMrnDList(int mrnID, int companyID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.fetchSubmittedMrnDList(mrnID, companyID, dbConnection);
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

        public List<MrnDetails> fetchSubmittedMrnDListbywarehouse(int mrnID, int warehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.fetchSubmittedMrnDListbywarehouse(mrnID, warehouseId, dbConnection);
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
        public int GetDetailsByMrnCode(int CompanyId, string MrnCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.GetDetailsByMrnCode(CompanyId, MrnCode, dbConnection);
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
        public MRN_Master FetchMRNByMrnId(int Mrnid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.FetchMRNByMrnId(Mrnid, dbConnection);
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

        public int UpdateMRMaster(int Mrnid, int CompanyId, int SubDeparmentId, DateTime CreatedDate, string description, DateTime ExpectedDate, string CreatedBy, int Status, int isApproved, int isActive, string QuotationFor, int PrTypeId, string ExpenseType, string PrProcedure, string PurchaseType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.UpdateMRMaster(Mrnid, CompanyId, SubDeparmentId, description, ExpectedDate, Status, isActive, isApproved, isActive, QuotationFor, PrTypeId, ExpenseType, PrProcedure, PurchaseType, dbConnection);
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

        public int FetchMRCode(int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.FetchMRCode(CompanyId, dbConnection);
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

        public List<MrnMaster> fetchSubmittedMrnListbywarehouseId(int companyID, int warehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.fetchSubmittedMrnListbywarehouseId(companyID, warehouseId, dbConnection);
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

        public List<MrnMaster> fetchSubmittedMrnList(int companyID)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.fetchSubmittedMrnList(companyID, dbConnection);
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

        public int updateMRNDIssuedQty(int mrndID, int issuedQty)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.updateMRNDIssuedQty(mrndID, issuedQty, dbConnection);
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

        public int updateMRNDReceivedQty(int mrndID, int receivedQty)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.updateMRNDReceivedQty(mrndID, receivedQty, dbConnection);
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

        public int changeMRNDStaus(int mrndID, int status)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.changeMRNDStaus(mrndID, status, dbConnection);
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

        public int updateMRNAfterIssue(int mrnID)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.updateMRNAfterIssue(mrnID, dbConnection);
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

        public int AutoAssignStorekerper(int mrnID, int categoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.AutoAssignStorekerper(mrnID, categoryId, dbConnection);
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

        public List<MrnMaster> GetMRNListForViewMyMRN(int companyId, int userId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.GetMRNListForViewMyMRN(companyId, userId, dbConnection);
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

        public List<MrnMaster> GetMRNListForMrnInquiry(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.GetMRNListForMrnInquiry(companyId, dbConnection);
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


      

        public DataTable GetMRNCountForDashBoard()
        {
            DBConnection dbConnection = new DBConnection();

            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.GetMRNCountForDashBoard(dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<MrnMaster> FetchMRNToEdit(int companyId ,int subDepatmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.FetchMRNToEdit(companyId, subDepatmentId, dbConnection);
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

        public List<MrnDetails> FetchMRNItemDetails(int mrnID  ,int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                return DAOFactory.CreateMRNDetailDAO().GetMrnDetailsByMRNid(mrnID, companyId, dbConnection);
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

        public List<MrnMaster> fetchSubmittedMrnListForOther(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.fetchSubmittedMrnListForOther(companyId, dbConnection);
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

        public List<MrnMaster> AdvanceSearchMRNForInquiry(int companyId, int searchBy, int categoryId, int subdepartmentId, string searchText)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.AdvanceSearchMRNForInquiry(companyId, searchBy, categoryId, subdepartmentId, searchText,  dbConnection);
            }
            catch (Exception ex)
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

        public List<MrnMaster> getMrnByDepartment(int companyId, List<int> departmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.getMrnByDepartment(companyId, departmentId, dbConnection);
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



        public MrnMaster GetMRNMasterByMrnId(int mrnID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.GetMRNMasterByMrnId(mrnID, dbConnection);
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

        public List<MrnMaster> FetchMrnByMrnCode(int CompanyId, string mrnCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.FetchMrnByMrnCode(CompanyId, mrnCode, dbConnection);
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

        public List<MrnMaster> FetchMRNByDate(int companyId, DateTime ToDate, DateTime FromDate) {
            DBConnection dbConnection = new DBConnection();
            try {
                MRN_MasterDAO mrn_MasterDAO = DAOFactory.CreateMRN_MasterDAO();
                return mrn_MasterDAO.FetchMRNByDate(companyId, ToDate, FromDate, dbConnection);
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
