using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller {
    public interface PhysicalStockVerificationMasterController {
        PhysicalstockVerificationMaster GetCreatedApprovedDetails(int warehouseId, DateTime date);
        int SaveStock(PhysicalstockVerificationMaster psvm);
        int UpdateStock(PhysicalstockVerificationMaster psvm);
        int ApproveStock(int psvmId, int companyId, int approvedBy, DateTime approvedDate, string remaks);
        int RejectStock(int psvmId, int status, int approvedBy, DateTime date, string remark);
    }
        

        public class PhysicalStockVerificationMasterControllerImpl : PhysicalStockVerificationMasterController {

        public PhysicalstockVerificationMaster GetCreatedApprovedDetails(int warehouseId, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                PhysicalStockVerificationMasterDAO physicalStockVerificationMasterDAO = DAOFactory.createUserPhysicalStockVerificationMasterDAO();
                PhysicalstockVerificationMaster psvm = physicalStockVerificationMasterDAO.GetCreatedApprovedDetails(warehouseId, date, dbConnection);
                if (psvm.WarehouseId != 0) {
                    psvm.PSVDetails = DAOFactory.createPhysicalstockVerificationDetailsDAO().GetPSVDbyPSVMId(psvm.PSVMId, dbConnection);

                    return psvm;
                }

                return null;
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

        public int SaveStock(PhysicalstockVerificationMaster psvm) {
            DBConnection dbConnection = new DBConnection();
            try {
                PhysicalStockVerificationMasterDAO physicalStockVerificationMasterDAO = DAOFactory.createUserPhysicalStockVerificationMasterDAO();
                return physicalStockVerificationMasterDAO.SaveStock(psvm, dbConnection);
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

        public int UpdateStock(PhysicalstockVerificationMaster psvd) {
            DBConnection dbConnection = new DBConnection();
            try {
                PhysicalStockVerificationMasterDAO physicalStockVerificationMasterDAO = DAOFactory.createUserPhysicalStockVerificationMasterDAO();
                return physicalStockVerificationMasterDAO.UpdateStock(psvd, dbConnection);
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

        public int ApproveStock(int psvmId, int companyId, int approvedBy, DateTime approvedDate, string remaks) {
            DBConnection dbConnection = new DBConnection();
            try {
                PhysicalStockVerificationMasterDAO physicalStockVerificationMasterDAO = DAOFactory.createUserPhysicalStockVerificationMasterDAO();
                return physicalStockVerificationMasterDAO.ApproveStock(psvmId, companyId, approvedBy, approvedDate, remaks, dbConnection);    
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

        public int RejectStock(int psvmId, int status, int approvedBy, DateTime date, string remark) {
            DBConnection dbConnection = new DBConnection();
            try {
                PhysicalStockVerificationMasterDAO physicalStockVerificationMasterDAO = DAOFactory.createUserPhysicalStockVerificationMasterDAO();
                return physicalStockVerificationMasterDAO.RejectStock(psvmId, status, approvedBy, date, remark, dbConnection);
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
