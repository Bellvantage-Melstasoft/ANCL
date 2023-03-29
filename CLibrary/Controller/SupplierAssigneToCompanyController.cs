using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Infrastructue;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
   public interface SupplierAssigneToCompanyController
    {
        int saveAssigneeSupplierWithCompany(int supplierid, int companyId, DateTime requesteddate, int isSupplierFollow);
        int updateAssigneeSupplierWithCompany(int supplierid, int companyId, int isApprove);
        List<SupplierAssignedToCompany> GetSupplierRequestsByCompanyId(int companyId);
        List<SupplierAssignedToCompany> GetCompanyListBySupplierId(int supplierid);
        int ApproveSupplierByCompanyId(int supplierid, int companyId);
        int RejectSupplierByCompanyId(int supplierid, int companyId);

        int saveAssigneSupplierWithCompanyByCompany(int supplierid, int companyId, DateTime requesteddate, int isApprove, int isSupplierFollow, int isTermAgreed);
        int BlockSupplierByCompanyId(int supplierid, int companyId);
        List<SupplierAssignedToCompany> GetSupplierAssignedCompanies(int supplierid);
        SupplierAssignedToCompany GetSupplierOfCompanyObj(int supplierid, int companyId);
        int FollowActiveSupplierByCompanyId(int supplierid, int companyId, int isActiveFolow);
        List<SupplierAssignedToCompany> GetCompanyListBySupplierIdforRequest(int supplierid);
        int unFollowCompanyBySupplier(int supplierid, int companyId);
        int updateUnfollowSupplier(int supplierid, int companyId, int isFollow);
        List<SupplierAssignedToCompany> GetSupplierRequestsByName(int companyId, string text);
    }

    public class SupplierAssigneToCompanyControllerImpl : SupplierAssigneToCompanyController
    {
        public int ApproveSupplierByCompanyId(int supplierid, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.ApproveSupplierByCompanyId(supplierid, companyId, dbConnection);
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

        public int BlockSupplierByCompanyId(int supplierid, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.BlockSupplierByCompanyId(supplierid, companyId, dbConnection);
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

        public List<SupplierAssignedToCompany> GetCompanyListBySupplierId(int supplierid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.GetCompanyListBySupplierId(supplierid, dbConnection);
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

        public List<SupplierAssignedToCompany> GetSupplierRequestsByCompanyId(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                List<SupplierAssignedToCompany> GetSupplierRequestsByCompanyId = new List<SupplierAssignedToCompany>();
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                GetSupplierRequestsByCompanyId = supplierAssigneToCompanyDAO.GetSupplierRequestsByCompanyId(companyId, dbConnection);
                foreach (var item in GetSupplierRequestsByCompanyId)
                {
                    item._Supplier = supplierDAO.GetSupplierBySupplierId(item.SupplierId, dbConnection);
                }
                return GetSupplierRequestsByCompanyId;
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
        public List<SupplierAssignedToCompany> GetSupplierRequestsByName(int companyId, string text) {
            DBConnection dbConnection = new DBConnection();
            try {
                List<SupplierAssignedToCompany> GetSupplierRequestsByCompanyId = new List<SupplierAssignedToCompany>();
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                GetSupplierRequestsByCompanyId = supplierAssigneToCompanyDAO.GetSupplierRequestsByName(companyId, text, dbConnection);
                foreach (var item in GetSupplierRequestsByCompanyId) {
                    item._Supplier = supplierDAO.GetSupplierBySupplierId(item.SupplierId, dbConnection);
                }
                return GetSupplierRequestsByCompanyId;
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
        public int RejectSupplierByCompanyId(int supplierid, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.RejectSupplierByCompanyId(supplierid, companyId, dbConnection);
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

        public int saveAssigneeSupplierWithCompany(int supplierid, int companyId, DateTime requesteddate, int isSupplierFollow)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.saveAssigneeSupplierWithCompany(supplierid, companyId, requesteddate, isSupplierFollow, dbConnection);
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

        public int saveAssigneSupplierWithCompanyByCompany(int supplierid, int companyId, DateTime requesteddate, int isApprove, int isSupplierFollow, int isTermAgreed)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.saveAssigneSupplierWithCompanyByCompany(supplierid, companyId, requesteddate, isApprove, isSupplierFollow, isTermAgreed, dbConnection);
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

        public int updateAssigneeSupplierWithCompany(int supplierid, int companyId, int isApprove)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.updateAssigneeSupplierWithCompany(supplierid, companyId, isApprove, dbConnection);
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

        public List<SupplierAssignedToCompany> GetSupplierAssignedCompanies(int supplierid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.GetSupplierAssignedCompanies(supplierid, dbConnection);
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

        public SupplierAssignedToCompany GetSupplierOfCompanyObj(int supplierid, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                SupplierAssignedToCompany supplierAssigneToCompanyObj = supplierAssigneToCompanyDAO.GetSupplierOfCompanyObj(supplierid, companyId, dbConnection);
                supplierAssigneToCompanyObj._SuplierImageUploadList = suplierImageUploadDAO.GetSupplierImagesBySupplierId(supplierAssigneToCompanyObj.SupplierId, dbConnection);
                supplierAssigneToCompanyObj._SupplierCategory = supplierCategoryDAO.GetSupplierCategoryBySupplierId(supplierAssigneToCompanyObj.SupplierId, dbConnection);
                supplierAssigneToCompanyObj.SupplierComplainDocument = suplierImageUploadDAO.GetSupplierComplianDocumentBySupplierId(supplierAssigneToCompanyObj.SupplierId, dbConnection);
                return supplierAssigneToCompanyObj;
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

        public int FollowActiveSupplierByCompanyId(int supplierid, int companyId, int isActiveFolow)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.FollowActiveSupplierByCompanyId(supplierid, companyId, isActiveFolow, dbConnection);
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

        public List<SupplierAssignedToCompany> GetCompanyListBySupplierIdforRequest(int supplierid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.GetCompanyListBySupplierIdforRequest(supplierid, dbConnection);
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

        public int unFollowCompanyBySupplier(int supplierid, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.unFollowCompanyBySupplier( supplierid,  companyId, dbConnection);
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

        public int updateUnfollowSupplier(int supplierid, int companyId, int isFollow)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                return supplierAssigneToCompanyDAO.updateUnfollowSupplier( supplierid,  companyId,  isFollow, dbConnection);
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

        

    }
}
