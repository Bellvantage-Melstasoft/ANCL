using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructue;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;

namespace CLibrary.Controller
{
    public interface SupplierBidBondDetailsController
    {
        int saveSupplierBidBondDetails(SupplierBidBondDetails supplierBidBondDetails);
        SupplierBidBondDetails getSupplierBidBondDetails(int bidId, int v);
        void updateSupplierBidBondDetails(SupplierBidBondDetails model);
        List<SupplierBidBondDetails> getSupplierBidBondDetailsByBidId(int bidId);
    }
    public class SupplierBidBondDetailsControllerImpl : SupplierBidBondDetailsController
    {
        public SupplierBidBondDetails getSupplierBidBondDetails(int bidId, int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBidBondDetailsDAO supplierBidBondDetailsDAO = DAOFactory.CreateSupplierBidBondDetailsDAO();
                return supplierBidBondDetailsDAO.getSupplierBidBondDetails(bidId, supplierId, dbConnection);
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

        public List<SupplierBidBondDetails> getSupplierBidBondDetailsByBidId(int bidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBidBondDetailsDAO supplierBidBondDetailsDAO = DAOFactory.CreateSupplierBidBondDetailsDAO();
                return supplierBidBondDetailsDAO.getSupplierBidBondDetailsByBidId(bidId, dbConnection);
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

        public int saveSupplierBidBondDetails(SupplierBidBondDetails supplierBidBondDetails)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBidBondDetailsDAO supplierBidBondDetailsDAO = DAOFactory.CreateSupplierBidBondDetailsDAO();
                return supplierBidBondDetailsDAO.saveSupplierBidBondDetails(supplierBidBondDetails, dbConnection);
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

        public void updateSupplierBidBondDetails(SupplierBidBondDetails model)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBidBondDetailsDAO supplierBidBondDetailsDAO = DAOFactory.CreateSupplierBidBondDetailsDAO();
                supplierBidBondDetailsDAO.updateSupplierBidBondDetails(model, dbConnection);
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
