using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CLibrary.Controller
{
    public interface GrnController
    {
        int SaveGrnMaster(int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, string InvoiceNo);
        int SaveGrnMasterDup(int GrnId, int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, int BasedGrn, string InvoiceNo);
        int UpdateRejectedGrn(int grnid, int poId, DateTime goodReceivedDate, string grnNote);
        List<GrnMaster> GetGRNmasterListByDepartmentId(int departmentid);

        //Get All 
        List<GrnMaster> GetAllGRNmasterList();
        GrnMaster GetGrnMasterByPoId(int PoId);
        GrnMaster GetGrnMasterByGrnID(int grnId, int poID);
        int grnMasterApproval(int grnId, int isApprove, string approvedby, int departmentId, string GrnCode);
        List<GrnMaster> GetgrnMasterListByByDateRange(int departmentid, DateTime startdate, DateTime enddate);
        List<GrnMaster> GetgrnMasterListByPoCode(int departmentid, string poCode);

        List<GrnMaster> GetAllDetailsGrn(int departmentid);
        GrnMaster CheckGrnExistMasterByGrnID(int grnId, int poID);
        int GetMaxGrnCode(int departmentId);
        int GetMaxGrnCodeDup();
        List<GrnMaster> GetAllDetailsGrnIsApproved(int departmentid);

        List<GrnMaster> FetchApprovedGRNForConfirmation(int Department);
        int ConfirmOrDenyGrnApproval(int grnId, int confirm);
        int GenrateCoveringPr(int PoId, int PrId, int CompanyId, int UserId, decimal TotVat, decimal TotNbt, decimal TotAmount, DataTable ItemDetails);

        List<GrnMaster> GetAllDetailsGrnforreport(int departmentid);

        //MOdified for GRN NEW
        int GenerateGRN(GrnMaster grnMaster, int PoId, int PrId, int UserId, List<InvoiceDetails> invoiceDetails, List<InvoiceImages> invoiceImages);
        GrnMaster getGrnDetailsByGrnCode(string grnCode, int CompanyId);
        List<GrnMaster> grnForApproval(int CompanyId, int UserId);
        List<GrnMaster> GetGrnsByCompanyId(int CompanyId, DateTime date);
        GrnMaster GetGrnMasterByGrnID(int grnId);
        int ValidateGrnBeforeApprove(int grnId);
        int ApproveGrn(int GrnId, int UserId, string Remarks);
        int RejectGrn(int GrnId, int UserId, string Remarks);
        List<GrnMaster> GetGRNmasterListBygrnCode(int departmentid, string GrnCode);
        List<GrnMaster> GetGRNmasterListByPOCode(int departmentid, string PoCode);
        List<GrnMaster> GetGrnForReturn(int departmentid);
        int UpdateGrnCoverigPR(int GrId, int CompanyId);
    }
    public class GrnControllerImpl : GrnController
    {
        public GrnMaster GetGrnMasterByGrnID(int grnId, int poID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                GrnMaster grnMasterObj = new GrnMaster();
                grnMasterObj = grnDAO.GetGrnMasterByGrnID(grnId, poID, dbConnection);
                grnMasterObj._companyDepartment = companyDepartmentDAO.GetDepartmentByDepartmentId(grnMasterObj.CompanyId, dbConnection);
                grnMasterObj._Supplier = supplierDAO.GetSupplierBySupplierId(grnMasterObj.Supplierid, dbConnection);
                grnMasterObj._POMaster = pOMasterDAO.GetPoMasterObjByPoId(grnMasterObj.PoId, dbConnection);
                return grnMasterObj;

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


        //Get All 
        public List<GrnMaster> GetAllGRNmasterList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetAllGRNmasterList(dbConnection);
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

        public GrnMaster GetGrnMasterByPoId(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetGrnMasterByPoId(PoId, dbConnection);
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

        public List<GrnMaster> GetgrnMasterListByByDateRange(int departmentid, DateTime startdate, DateTime enddate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetgrnMasterListByByDateRange(departmentid, startdate, enddate, dbConnection);
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

        public List<GrnMaster> GetGRNmasterListByDepartmentId(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetGRNmasterListByDepartmentId(departmentid, dbConnection);
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

        public List<GrnMaster> GetGRNmasterListByPOCode(int departmentid, string PoCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetGRNmasterListByPOCode(departmentid, PoCode, dbConnection);
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

        public List<GrnMaster> GetGRNmasterListBygrnCode(int departmentid, string GrnCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetGRNmasterListBygrnCode(departmentid, GrnCode, dbConnection);
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

        public List<GrnMaster> GetgrnMasterListByPoCode(int departmentid, string poCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetgrnMasterListByPoCode(departmentid, poCode, dbConnection);
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

        public int grnMasterApproval(int grnId, int isApprove, string approvedby, int departmentId, string GrnCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.grnMasterApproval(grnId, isApprove, approvedby, departmentId, GrnCode, dbConnection);
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

        public int SaveGrnMaster(int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, string InvoiceNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.SaveGrnMaster(poId, companyId, supplierid, goodReceivedDate, totalAmount, createdBy, createdDate, grnNote, InvoiceNo, dbConnection);
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

        public int SaveGrnMasterDup(int GrnId, int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, int BasedGrn, string InvoiceNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.SaveGrnMasterDup(GrnId, poId, companyId, supplierid, goodReceivedDate, totalAmount, createdBy, createdDate, grnNote, BasedGrn, InvoiceNo, dbConnection);
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

        public int UpdateRejectedGrn(int grnid, int poId, DateTime goodReceivedDate, string grnNote)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.UpdateRejectedGrn(grnid, poId, goodReceivedDate, grnNote, dbConnection);
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

        public List<GrnMaster> GetAllDetailsGrn(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetAllDetailsGrn(departmentid, dbConnection);
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

        public GrnMaster CheckGrnExistMasterByGrnID(int grnId, int poID)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.CheckGrnExistMasterByGrnID(grnId, poID, dbConnection);
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

        public int GetMaxGrnCode(int departmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetMaxGrnCode(departmentId, dbConnection);
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

        public int GetMaxGrnCodeDup()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetMaxGrnCodeDup(dbConnection);
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

        public List<GrnMaster> GetAllDetailsGrnIsApproved(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetAllDetailsGrnIsApproved(departmentid, dbConnection);
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

        public List<GrnMaster> FetchApprovedGRNForConfirmation(int Department)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.FetchApprovedGRNForConfirmation(Department, dbConnection);
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

        public int ConfirmOrDenyGrnApproval(int grnId, int confirm)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.ConfirmOrDenyGrnApproval(grnId, confirm, dbConnection);
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

        public int GenrateCoveringPr(int PoId, int PrId, int CompanyId, int UserId, decimal TotVat, decimal TotNbt, decimal TotAmount, DataTable ItemDetails)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GenrateCoveringPr(PoId, PrId, CompanyId, UserId, TotVat, TotNbt, TotAmount, ItemDetails, dbConnection);
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

        public List<GrnMaster> GetAllDetailsGrnforreport(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetAllDetailsGrnforreport(departmentid, dbConnection);
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
        public int GenerateGRN(GrnMaster grnMaster, int PoId, int PrId, int UserId, List<InvoiceDetails> invoiceDetails, List<InvoiceImages> invoiceImages)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                InvoiceDetailsDAO invoiceDetailsDAO = DAOFactory.CreateInvoiceDetailsDAO();
                int GRnId = grnDAO.GenerateGRN(grnMaster, dbConnection);
                if (GRnId > 0)
                {
                    if (invoiceDetails.Count > 0)
                    {
                        for (int i = 0; i < invoiceDetails.Count; i++)
                        {
                            int Result = invoiceDetailsDAO.SaveInvoiceDetailsInPO(PoId, GRnId, invoiceDetails[i].PaymentType, invoiceDetails[i].InvoiceNo, invoiceDetails[i].InvoiceDate, invoiceDetails[i].InvoiceAmount, invoiceDetails[i].VatNo, invoiceDetails[i].IsPaymentSettled, invoiceDetails[i].Remark, invoiceDetails[i].RemarkOn, invoiceImages, dbConnection);
                        }
                    }
                    return GRnId;
                }
                else
                {
                    dbConnection.RollBack();
                    return -1;
                }




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

        //public int GenerateGRN(GrnMaster grnMaster, int PoId, int PrId, int UserId)
        //{
        //    DBConnection dbConnection = new DBConnection();
        //    try
        //    {
        //        GrnDAO grnDAO = DAOFactory.createGrnDAO();
        //        POMasterDAO prMasterDAO = DAOFactory.createPOMasterDAO();

        //        int grnId =  grnDAO.GenerateGRN(grnMaster, dbConnection);
        //        if (grnId > 0) {
        //            int result = prMasterDAO.CreateCoveringPR(PoId, PrId, grnId, UserId, dbConnection);
        //            if (result > 0) {
        //                return 1;
        //            }
        //            else {
        //                return -1;
        //            }
        //        }

        //        else {
        //            return -1;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        dbConnection.RollBack();
        //        throw;
        //    }
        //    finally
        //    {
        //        if (dbConnection.con.State == System.Data.ConnectionState.Open)
        //        {
        //            dbConnection.Commit();
        //        }
        //    }
        //}

        public GrnMaster getGrnDetailsByGrnCode(string grnCode, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.getGrnDetailsByGrnCode(grnCode, CompanyId, dbConnection);
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

        public int UpdateGrnCoverigPR(int GrId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.UpdateGrnCoverigPR(GrId, CompanyId, dbConnection);
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

        public List<GrnMaster> grnForApproval(int CompanyId, int UserId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.grnForApproval(CompanyId, UserId, dbConnection);
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

        public List<GrnMaster> GetGrnForReturn(int departmentid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetGrnForReturn(departmentid, dbConnection);
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

        public List<GrnMaster> GetGrnsByCompanyId(int CompanyId, DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetGrnsByCompanyId(CompanyId, date, dbConnection);
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

        public GrnMaster GetGrnMasterByGrnID(int grnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
                POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                GrnMaster grnMasterObj = new GrnMaster();
                WarehouseDAOInterface WarehouseDAOInterface = DAOFactory.CreateWarehouseDAO();
                grnMasterObj = grnDAO.GetGrnMasterByGrnID(grnId, dbConnection);
                List<string> poCodes = DAOFactory.createPOMasterDAO().getPoDetailsByGrnId(grnMasterObj.GrnId, dbConnection);

                grnMasterObj.POCode = poCodes[0];
                grnMasterObj.PrCode = poCodes[1];



                grnMasterObj.QuotationFor = DAOFactory.CreatePR_MasterDAO().GetQuotationForbyPrCode(grnMasterObj.CompanyId, grnMasterObj.PrCode, dbConnection);

                if (grnMasterObj != null)
                {
                    grnMasterObj._companyDepartment = companyDepartmentDAO.GetDepartmentByDepartmentId(grnMasterObj.CompanyId, dbConnection);
                    grnMasterObj._Supplier = supplierDAO.GetSupplierBySupplierId(grnMasterObj.Supplierid, dbConnection);
                    //grnMasterObj._POMaster = pOMasterDAO.GetPoMasterObjByPoId(grnId, dbConnection);
                    grnMasterObj._POMaster = pOMasterDAO.GetPoMasterObjByPoIdNew(grnId, dbConnection);
                    grnMasterObj._Warehouse = WarehouseDAOInterface.getWarehouseByID(grnMasterObj.WarehouseId, dbConnection);



                }
                return grnMasterObj;
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

        public int ValidateGrnBeforeApprove(int grnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.ValidateGrnBeforeApprove(grnId, dbConnection);
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

        public int ApproveGrn(int GrnId, int UserId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.ApproveGrn(GrnId, UserId, Remarks, dbConnection);
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

        public int RejectGrn(int GrnId, int UserId, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.RejectGrn(GrnId, UserId, Remarks, dbConnection);
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
