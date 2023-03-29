using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface ItemImageUploadController
    {
        int SaveItems(int ItemId, string ImagePath);
        int UpdateItems(int ItemId, string ImagePath);
        int SaveMasterItemsImage(int ItemId, string ImagePath);
    }

    public class ItemImageUploadControllerImpl : ItemImageUploadController
    {
        public int SaveItems(int ItemId, string ImagePath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemImageUploadDAO itemImageUploadDAO = DAOFactory.CreateItemImageUploadDAO();
                return itemImageUploadDAO.SaveItems(ItemId, ImagePath, dbConnection);
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

        public int SaveMasterItemsImage(int ItemId, string ImagePath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemImageUploadDAO itemImageUploadDAO = DAOFactory.CreateItemImageUploadDAO();
                return itemImageUploadDAO.SaveMasterItemsImage( ItemId,  ImagePath, dbConnection);
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

        public int UpdateItems(int ItemId, string ImagePath)
        {
            DBConnection dbConnection = new DBConnection();

            try
            {
                ItemImageUploadDAO itemImageUploadDAO = DAOFactory.CreateItemImageUploadDAO();
                return itemImageUploadDAO.UpdateItems(ItemId, ImagePath, dbConnection);
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
