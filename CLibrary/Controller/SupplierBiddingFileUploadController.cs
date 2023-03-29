using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface SupplierBiddingFileUploadController
    {
        int SaveFiles(List<SupplierBiddingFileUpload> list);
        int SaveFiles(int SupplierId, int QuotationId, int PrId, int ItemId, string FileName, string FilePath);
        List<SupplierBiddingFileUpload> GetFilesByQuotationId(int quotationId);
        List<SupplierBiddingFileUpload> GetFilesByQuotationId(int PrId, int ItemId, int SupplierId);
        int DeleteFileUploads(int PrId, int ItemId, int SupplierId, string FilePath);
        int DeleteFileUploads(string fileName);
    }

    public class SupplierBiddingFileUploadControllerImpl : SupplierBiddingFileUploadController
    {
        public int SaveFiles(int SupplierId, int QuotationId, int PrId, int ItemId, string FileName, string FilePath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBiddingFileUploadDAO supplierBiddingFileUploadDAO = DAOFactory.CreateSupplierBiddingFileUploadDAO();
                return supplierBiddingFileUploadDAO.SaveFiles(SupplierId, QuotationId, PrId, ItemId, FileName, FilePath, dbConnection);
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

        public int SaveFiles(List<SupplierBiddingFileUpload> list)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBiddingFileUploadDAO suplierImageUploadDAO = DAOFactory.CreateSupplierBiddingFileUploadDAO();
                return suplierImageUploadDAO.SaveFiles(list, dbConnection);
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

        public List<SupplierBiddingFileUpload> GetFilesByQuotationId(int PrId, int ItemId, int SupplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBiddingFileUploadDAO supplierBiddingFileUploadDAO = DAOFactory.CreateSupplierBiddingFileUploadDAO();
                return supplierBiddingFileUploadDAO.GetFilesByQuotationId(PrId,ItemId,SupplierId, dbConnection);
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

        public List<SupplierBiddingFileUpload> GetFilesByQuotationId(int quotationId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBiddingFileUploadDAO supplierBiddingFileUploadDAO = DAOFactory.CreateSupplierBiddingFileUploadDAO();
                return supplierBiddingFileUploadDAO.GetFilesByQuotationId(quotationId, dbConnection);
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

        public int DeleteFileUploads(int PrId, int ItemId, int SupplierId, string FilePath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBiddingFileUploadDAO supplierBiddingFileUploadDAO = DAOFactory.CreateSupplierBiddingFileUploadDAO();
                return supplierBiddingFileUploadDAO.DeleteFileUploads(PrId, ItemId, SupplierId, FilePath, dbConnection);
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

        public int DeleteFileUploads(string fileName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierBiddingFileUploadDAO supplierBiddingFileUploadDAO = DAOFactory.CreateSupplierBiddingFileUploadDAO();
                return supplierBiddingFileUploadDAO.DeleteFileUploads(fileName, dbConnection);
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
