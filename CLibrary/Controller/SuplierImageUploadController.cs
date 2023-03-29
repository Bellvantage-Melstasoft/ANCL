using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructue;
using System;
using System.Collections.Generic;
using System.Text;

namespace CLibrary.Controller
{
   public interface SuplierImageUploadController
    {
        int saveUploadedSupplierImage(int supplierId, string supplierImagePath, string filename, string imageId, int isActive);
        int saveUploadedSupplierImage(List<SuplierImageUpload> list);
        int updateUploadedSupplierImage(int supplierId, string supplierImagePath, string filename, string imageId, int isActive);
        List<SuplierImageUpload> GetSupplierImagesBySupplierId(int supplierId);
        int deleteUploadedSupplierFile(string fileId);
        int saveUploadedSupplierComplianDoc(int v1, string v2, string fileName, string nameOfUploadedFile, int v3);
        int deleteUploadedComplianFile(string fileId);
        List<SuplierImageUpload> GetSupplierComplianDocumentBySupplierId(int supplierId);
    }

    public class SuplierImageUploadControllerImpl : SuplierImageUploadController
    {


        public int deleteUploadedSupplierFile(string fileId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                return suplierImageUploadDAO.deleteUploadedSupplierFile(fileId, dbConnection);
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

        public List<SuplierImageUpload> GetSupplierImagesBySupplierId(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                return suplierImageUploadDAO.GetSupplierImagesBySupplierId( supplierId,dbConnection);
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

        public int saveUploadedSupplierImage(List<SuplierImageUpload> list)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                return suplierImageUploadDAO.saveUploadedSupplierFile(list, dbConnection);
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

        public int saveUploadedSupplierImage(int supplierId, string supplierImagePath, string filename, string imageId, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                return suplierImageUploadDAO.saveUploadedSupplierFile(supplierId, supplierImagePath, filename, imageId, isActive, dbConnection);
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

        public int updateUploadedSupplierImage(int supplierId, string supplierImagePath, string filename, string imageId, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                return suplierImageUploadDAO.updateUploadedSupplierFile( supplierId,  supplierImagePath,  filename,  imageId, isActive, dbConnection);
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

        public int saveUploadedSupplierComplianDoc(int supplierId, string supplierImagePath, string filename, string imageId, int isActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                return suplierImageUploadDAO.saveUploadedSupplierComplianDoc(supplierId, supplierImagePath, filename, imageId, isActive, dbConnection);
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

        public int deleteUploadedComplianFile(string fileId)
        {
            DBConnection dbConnection = null;
            try
            {
                dbConnection = new DBConnection();
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                return suplierImageUploadDAO.deleteUploadedComplianFile(fileId, dbConnection);
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

        public List<SuplierImageUpload> GetSupplierComplianDocumentBySupplierId(int supplierId)
        {
            DBConnection dbConnection = null;
            try
            {
                dbConnection = new DBConnection();
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                return suplierImageUploadDAO.GetSupplierComplianDocumentBySupplierId(supplierId, dbConnection);
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
