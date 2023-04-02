using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using CLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface POMasterController
    {
        int SavePOMaster(int departmentid, int prId, int supplierId, DateTime createdDate, string createdBy, decimal vatAmount, decimal nbtAmount, string vatRegNo, string sVatRegNo, decimal totalAmount, int isApproved, string approvedBy, int isReceived, DateTime receivedDate, int BasePr, decimal totalCustomizedAmount, decimal totalCustomizedVat, decimal totalCustomizedNbt, string paymentmethod);
        int SavePOMasterPO(int poId, string pocode, int departmentid, int supplierId, DateTime createdDate, string createdBy, decimal vatAmount, decimal nbtAmount, string vatRegNo, string sVatRegNo, decimal totalAmount, int isApproved, string approvedBy, int isReceived, DateTime receivedDate, int BasePr, decimal totalCustomizedAmount, decimal totalCustomizedVat, decimal totalCustomizedNbt);
        int updatePODetails(int PoId, decimal vatAmount, decimal nbtAmount, decimal totalAmount, decimal customizedTotalAmount, decimal customizedVatAmount, decimal customizedNbtAmount);

        //Get All PO MASTER LIST
        List<POMaster> GetAllPOMAster();

        List<POMaster> GetPoMasterListByDepartmentId(int departmentid);
        POMaster GetPoMasterObjByPoId(int PoId);
        int PoMasterApproval(int poId, int isApprove, int departmentid);
        List<POMaster> GetPoMasterListByByDaterange(int departmentid, DateTime startdate, DateTime enddate);
        int GetMaxPoNumebr();
        int UpdateTotalAmounts(int PoId, decimal vatAmount, decimal nbtAmount, decimal totalAmount, decimal customizedVatAmount, decimal customizedNbtAmount, decimal customizedTotalAmount);
        List<POMaster> GetPoMasterListByDepartmentIdViewPO(int departmentid);
        List<POMaster> GetPoMasterRejectedListByDepartmentIdViewPO(int departmentid);

        POMaster GetPoMasterObjByPoIdView(int PoIdint);
        int updatePaymentMethodByPoId(int PoId, int departmentid, string paymentMethod);

        //--GRN resources
        List<POMaster> GetPoMasterListByDepartmentIdToGRN(int departmentid);
        List<POMaster> GetPoMasterListByDepartmentIdTogrn(int departmentid);
        List<POMaster> GetPoMasterListByWarehouseIdTogrn(List<int> WarehouseIds);
        POMaster GetPoMasterToGenerateGRN(int PoId, int CompanyId, decimal nbtVal1, decimal nbtVal2, decimal vatVal);
        int GenerateCoveringPO(POMaster poMaster);
        //POMaster GetPoMasterToViewPO(int PoId, int CompanyId);

        int rejectPOMaster(int poId);
        List<POMaster> GetPoMasterListByDepartmentIdEditMode(int departmentid);
        POMaster GetPoMasterObjByPoIdRaised(int PoId, int companyId);


        List<POMaster> FetchApprovedPOForConfirmation(int Department);
        int ConfirmOrDenyPOApproval(int poId, int confirm);
        int SavePO(List<POMaster> PoMasters, int UserId);
        List<POMaster> GetPoMasterListWithImport(int departmentid);
        int ApprovePOMaster(int poId, int userId);
        List<int> GetPoCountForDashboard(int CompanyId, int yearsearch, int purchaseType);
        List<ItemPurchaseHistory> GetItemPurchaseHistories(int ItemId);
        List<int> SavePONew(List<POMaster> PoMasters, int UserId);
        List<POMaster> ViewAllPOS(int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType, List<int> supplierIds = null);
        List<POMaster> ViewMyPOS(int UserId, int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType);
        POMaster GetPoMasterToEditPO(int PoId, int CompanyId);
        List<int> UpdatePO(POMaster poMaster, int UserId);
        List<POMaster> GetPosForApproval(int CompanyId, int UserId);
        POMaster GetPoMasterToViewPO(int PoId, int CompanyId);
        List<POMaster> GetModifiedPosForApproval(int CompanyId, int UserId);
        int ParentApprovePO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, string PoRemark);
        int ApproveGeneralPO(int PoId, int UserId, string Remarks, int PaymentMethod, string PoRemark);
        int RejectGeneralPO(int PoId, int UserId, string Remarks, int PaymentMethod);
        int ParentRejectPO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, int RejectionAction, int ParentPOId);
        int UpdatePrintCount(int PoId);
        int CancelPo(int Poid);
        List<POMaster> ViewCancelledPOS(int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType);
        List<POMaster> GetAllPosByPrIdFor(List<int> PrId);
        int UpdatePoEmailStatus(int Poid);
        List<POMaster> GetAllPosByPrId(int PrId);
        List<POMaster> GetAPPROVEDPosByPrId(int PrId);
        int CheckPoGrns(int PoId);
        List<POMaster> GetPosForPrint(int CompanyId, int UserId);
        List<POMaster> GetPosForInvoice(int CompanyId, int UserId);
        int GetPoId(int PrId);
        int ApprovedCoveringPOCount(int PrId);
    }
    public class POMasterControllerImpl : POMasterController
    {
        public List<POMaster> GetPoMasterListByByDaterange(int departmentid, DateTime startdate, DateTime enddate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterListByByDaterange(departmentid, startdate, enddate, dbConnection);
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


        //Get All PO MASTER LIST
        public List<POMaster> GetAllPOMAster()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetAllPOMAster(dbConnection);
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

        public List<POMaster> GetAllPosByPrId(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetAllPosByPrId(PrId, dbConnection);
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

        public List<POMaster> GetAPPROVEDPosByPrId(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetAPPROVEDPosByPrId(PrId, dbConnection);
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

        public List<POMaster> GetPoMasterListByDepartmentId(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterListByDepartmentId(departmentid, dbConnection);
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

        public List<POMaster> GetAllPosByPrIdFor(List<int> PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetAllPosByPrIdFor(PrId, dbConnection);
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

        public int CancelPo(int Poid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.CancelPo(Poid, dbConnection);
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

        public int UpdatePoEmailStatus(int Poid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.UpdatePoEmailStatus(Poid, dbConnection);
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

        public POMaster GetPoMasterObjByPoId(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                POMaster POMasterObj = new POMaster();
                POMasterObj = pOMasterDAO.GetPoMasterObjByPoId(PoId, dbConnection);
                POMasterObj._companyDepartment = companyDepartmentDAO.GetDepartmentByDepartmentId(POMasterObj.DepartmentId, dbConnection);
                POMasterObj._Supplier = supplierDAO.GetSupplierBySupplierId(POMasterObj.SupplierId, dbConnection);

                return POMasterObj;
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

        public POMaster GetPoMasterObjByPoIdRaised(int PoId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                POMaster POMasterObj = new POMaster();
                POMasterObj = pOMasterDAO.GetPoMasterObjByPoIdRaised(PoId, companyId, dbConnection);
                POMasterObj._companyDepartment = companyDepartmentDAO.GetDepartmentByDepartmentId(POMasterObj.DepartmentId, dbConnection);
                POMasterObj._Supplier = supplierDAO.GetSupplierBySupplierId(POMasterObj.SupplierId, dbConnection);
                POMasterObj._Warehouse = DAOFactory.CreateWarehouseDAO().getWarehouseByID(POMasterObj.DeliverToWarehouse, dbConnection);

                return POMasterObj;
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

        public POMaster GetPoMasterObjByPoIdView(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                POMaster POMasterObj = new POMaster();
                POMasterObj = pOMasterDAO.GetPoMasterObjByPoIdView(PoId, dbConnection);
                POMasterObj._companyDepartment = companyDepartmentDAO.GetDepartmentByDepartmentId(POMasterObj.DepartmentId, dbConnection);
                POMasterObj._Supplier = supplierDAO.GetSupplierBySupplierId(POMasterObj.SupplierId, dbConnection);

                return POMasterObj;
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

        public int PoMasterApproval(int poId, int isApprove, int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.PoMasterApproval(poId, isApprove, departmentid, dbConnection);
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

        public int SavePOMaster(int departmentid, int prId, int supplierId, DateTime createdDate, string createdBy, decimal vatAmount, decimal nbtAmount, string vatRegNo, string sVatRegNo, decimal totalAmount, int isApproved, string approvedBy, int isReceived, DateTime receivedDate, int BasePr, decimal totalCustomizedAmount, decimal totalCustomizedVat, decimal totalCustomizedNbt, string paymentmethod)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.SavePOMaster(departmentid, prId, supplierId, createdDate, createdBy, vatAmount, nbtAmount, vatRegNo, sVatRegNo, totalAmount, isApproved, approvedBy, isReceived, receivedDate, BasePr, totalCustomizedAmount, totalCustomizedVat, totalCustomizedNbt, paymentmethod, dbConnection);
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

        public int SavePOMasterPO(int poId, string pocode, int departmentid, int supplierId, DateTime createdDate, string createdBy, decimal vatAmount, decimal nbtAmount, string vatRegNo, string sVatRegNo, decimal totalAmount, int isApproved, string approvedBy, int isReceived, DateTime receivedDate, int BasePr, decimal totalCustomizedAmount, decimal totalCustomizedVat, decimal totalCustomizedNbt)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.SavePOMasterPO(poId, pocode, departmentid, supplierId, createdDate, createdBy, vatAmount, nbtAmount, vatRegNo, sVatRegNo, totalAmount, isApproved, approvedBy, isReceived, receivedDate, BasePr, totalCustomizedAmount, totalCustomizedVat, totalCustomizedNbt, dbConnection);
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

        public int updatePODetails(int PoId, decimal vatAmount, decimal nbtAmount, decimal totalAmount, decimal customizedTotalAmount, decimal customizedVatAmount, decimal customizedNbtAmount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.updatePODetails(PoId, vatAmount, nbtAmount, totalAmount, customizedTotalAmount, customizedVatAmount, customizedNbtAmount, dbConnection);
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

        public int GetMaxPoNumebr()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetMaxPoNumebr(dbConnection);
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

        public int UpdateTotalAmounts(int PoId, decimal vatAmount, decimal nbtAmount, decimal totalAmount, decimal customizedVatAmount, decimal customizedNbtAmount, decimal customizedTotalAmount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.UpdateTotalAmounts(PoId, vatAmount, nbtAmount, totalAmount, customizedVatAmount, customizedNbtAmount, customizedTotalAmount, dbConnection);
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

        public List<POMaster> GetPoMasterListByDepartmentIdViewPO(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterListByDepartmentIdViewPO(departmentid, dbConnection);
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
        public List<POMaster> GetPoMasterRejectedListByDepartmentIdViewPO(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterRejectedListByDepartmentIdViewPO(departmentid, dbConnection);
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

        public List<POMaster> GetPoMasterListByDepartmentIdToGRN(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterListByDepartmentIdToGRN(departmentid, dbConnection);
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

        public int rejectPOMaster(int poId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.rejectPOMaster(poId, dbConnection);
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

        public int updatePaymentMethodByPoId(int PoId, int departmentid, string paymentMethod)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.updatePaymentMethodByPoId(PoId, departmentid, paymentMethod, dbConnection);
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

        public List<POMaster> GetPoMasterListByDepartmentIdEditMode(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterListByDepartmentIdEditMode(departmentid, dbConnection);
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

        public List<POMaster> FetchApprovedPOForConfirmation(int Department)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.FetchApprovedPOForConfirmation(Department, dbConnection);
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

        public int ConfirmOrDenyPOApproval(int poId, int confirm)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ConfirmOrDenyPOApproval(poId, confirm, dbConnection);
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

        public int SavePO(List<POMaster> PoMasters, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                int x = pOMasterDAO.SavePO(PoMasters, UserId, dbConnection);
                return x;
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

        public int ApprovePOMaster(int poId, int userId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ApprovePOMaster(poId, userId, dbConnection);
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

        public List<int> GetPoCountForDashboard(int CompanyId, int yearsearch, int purchaseType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoCountForDashboard(CompanyId, yearsearch, purchaseType, dbConnection);
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

        public List<POMaster> GetPoMasterListWithImport(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterListWithImport(departmentid, dbConnection);
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

        public List<ItemPurchaseHistory> GetItemPurchaseHistories(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetItemPurchaseHistories(ItemId, dbConnection);
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

        public List<int> SavePONew(List<POMaster> PoMasters, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.SavePONew(PoMasters, UserId, dbConnection);
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
        public List<POMaster> ViewAllPOS(int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType, List<int> supplierId = null)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ViewAllPOS(CompanyId, date, prcode, pocode, caregoryIds, warehouseIds, poType, dbConnection, supplierId);
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

        public List<POMaster> ViewCancelledPOS(int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ViewCancelledPOS(CompanyId, date, prcode, pocode, caregoryIds, warehouseIds, poType, dbConnection);
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

        public List<POMaster> ViewMyPOS(int UserId, int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ViewMyPOS(UserId, CompanyId, date, prcode, pocode, caregoryIds, warehouseIds, poType, dbConnection);
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

        public POMaster GetPoMasterToEditPO(int PoId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterToEditPO(PoId, CompanyId, dbConnection);
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

        public List<int> UpdatePO(POMaster poMaster, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();

                return pOMasterDAO.UpdatePO(poMaster, UserId, dbConnection);

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

        public List<POMaster> GetPosForApproval(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPosForApproval(CompanyId, UserId, dbConnection);
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

        public List<POMaster> GetPosForInvoice(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPosForInvoice(CompanyId, UserId, dbConnection);
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

        public List<POMaster> GetPosForPrint(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPosForPrint(CompanyId, UserId, dbConnection);
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
        public POMaster GetPoMasterToViewPO(int PoId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterToViewPO(PoId, CompanyId, dbConnection);
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

        public List<POMaster> GetModifiedPosForApproval(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetModifiedPosForApproval(CompanyId, UserId, dbConnection);
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

        public int ParentApprovePO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, string PoRemark)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ParentApprovePO(PoId, Remarks, PaymentMethod, PoType, UserId, IsParentApproved, PoRemark, dbConnection);
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

        public int ApproveGeneralPO(int PoId, int UserId, string Remarks, int PaymentMethod, string PoRemark)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ApproveGeneralPO(PoId, UserId, Remarks, PaymentMethod, PoRemark, dbConnection);
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

        public int RejectGeneralPO(int PoId, int UserId, string Remarks, int PaymentMethod)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.RejectGeneralPO(PoId, UserId, Remarks, PaymentMethod, dbConnection);
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
        public int ParentRejectPO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, int RejectionAction, int ParentPOId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ParentRejectPO(PoId, Remarks, PaymentMethod, PoType, UserId, IsParentApproved, RejectionAction, ParentPOId, dbConnection);
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

        public List<POMaster> GetPoMasterListByDepartmentIdTogrn(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterListByDepartmentIdTogrn(departmentid, dbConnection);
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

        public List<POMaster> GetPoMasterListByWarehouseIdTogrn(List<int> WarehouseIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoMasterListByWarehouseIdTogrn(WarehouseIds, dbConnection);
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

        public POMaster GetPoMasterToGenerateGRN(int PoId, int CompanyId, decimal nbtVal1, decimal nbtVal2, decimal vatVal)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                POMaster po = pOMasterDAO.GetPoMasterToGenerateGRN(PoId, CompanyId, dbConnection);

                for (int i = 0; i < po.PoDetails.Count; i++)
                {

                    po.PoDetails[i].SubTotal = Math.Round(po.PoDetails[i].SubTotal, 2);
                    if (po.PoDetails[i].HasNbt == 1)
                    {
                        if (po.PoDetails[i].NbtCalculationType == 1)
                        {
                            po.PoDetails[i].NbtAmount = Math.Round((po.PoDetails[i].SubTotal * nbtVal1), 2);
                        }
                        else
                        {
                            po.PoDetails[i].NbtAmount = Math.Round((po.PoDetails[i].SubTotal * nbtVal2), 2);
                        }
                    }

                    if (po.PoDetails[i].HasVat == 1)
                    {
                        //po.PoDetails[i].VatAmount = Math.Round(((po.PoDetails[i].SubTotal + po.PoDetails[i].NbtAmount) * vatVal), 2);

                        if (po.PoDetails[i].PoPurchaseType != 2)
                        {
                            po.PoDetails[i].VatAmount = Math.Round(((po.PoDetails[i].SubTotal) * vatVal), 2);
                        }
                    }

                    //po.PoDetails[i].TotalAmount = Math.Round(po.PoDetails[i].SubTotal + po.PoDetails[i].NbtAmount + po.PoDetails[i].VatAmount, 2);
                    po.PoDetails[i].TotalAmount = Math.Round(po.PoDetails[i].SubTotal + po.PoDetails[i].VatAmount, 2);

                    po.SubTotal += po.PoDetails[i].SubTotal;
                    po.NBTAmount += po.PoDetails[i].NbtAmount;
                    po.VatAmount += po.PoDetails[i].VatAmount;
                    po.TotalAmount += po.PoDetails[i].TotalAmount;

                }

                return po;
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

        public int GenerateCoveringPO(POMaster poMaster)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GenerateCoveringPO(poMaster, dbConnection);
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

        public int UpdatePrintCount(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.UpdatePrintCount(PoId, dbConnection);
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

        public int GetPoId(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.GetPoId(PrId, dbConnection);
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
        public int ApprovedCoveringPOCount(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.ApprovedCoveringPOCount(PrId, dbConnection);
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

        public int CheckPoGrns(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                return pOMasterDAO.CheckPoGrns(PoId, dbConnection);
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
