using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface SupplierBOMController
    {
        int SaveSupplierBOM(int SupplierId, int PrId, int ItemId, int SeqNo, string Meterial, string Description, int IsActive, DateTime CreatedDatetime, int Comply, string Remarks);
        int UpdateSupplierBOM(int SupplierId, int PrId, int ItemId, int SeqNo, string Meterial, string Description,  int Comply, string Remarks);
        List<SupplierBOM> GetSupplierList(int SupplierId, int PrId, int ItemId);
        List<SupplierBOM> GetSupplierBom(int QuotationItemId);
    }

    public class SupplierBOMControllerImpl : SupplierBOMController
    {
        public int SaveSupplierBOM(int SupplierId, int PrId, int ItemId, int SeqNo, string Meterial, string Description, int IsActive, DateTime CreatedDatetime, int Comply, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBOMDAO supplierBOMDAO = DAOFactory.CreateSupplierBOMDAO();
                return supplierBOMDAO.SaveSupplierBOM(SupplierId, PrId, ItemId, SeqNo, Meterial, Description, IsActive, CreatedDatetime, Comply, Remarks, dbConnection);
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

        public int UpdateSupplierBOM(int SupplierId, int PrId, int ItemId, int SeqNo, string Meterial, string Description, int Comply, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBOMDAO supplierBOMDAO = DAOFactory.CreateSupplierBOMDAO();
                return supplierBOMDAO.UpdateSupplierBOM( SupplierId,  PrId,  ItemId,  SeqNo,  Meterial,  Description, Comply,  Remarks, dbConnection);
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

        public List<SupplierBOM> GetSupplierList(int SupplierId, int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBOMDAO supplierBOMDAO = DAOFactory.CreateSupplierBOMDAO();
                return supplierBOMDAO.GetSupplierList(SupplierId,  PrId,  ItemId, dbConnection);
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

        public List<SupplierBOM> GetSupplierBom(int QuotationItemId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierBOMDAO supplierBOMDAO = DAOFactory.CreateSupplierBOMDAO();
                return supplierBOMDAO.GetSupplierBom(QuotationItemId, dbConnection);
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
