using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface ItemImageUploadDAO
    {
        int SaveItems(int ItemId, string ImagePath,  DBConnection dbConnection);
        int UpdateItems(int ItemId, string ImagePath, DBConnection dbConnection);
        int SaveMasterItemsImage(int ItemId, string ImagePath, DBConnection dbConnection);
    }

    public class ItemImageUploadDAOImpl : ItemImageUploadDAO
    {
        public int SaveItems(int ItemId, string ImagePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO public.\"ITEM_IMAGE_UPLOAD\" (\"ITEM_ID\", \"IMAGE_PATH\") VALUES ( " + ItemId + ", '" + ImagePath + "' );";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveMasterItemsImage(int ItemId, string ImagePath, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int UpdateItems(int ItemId, string ImagePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE  public.\"ITEM_IMAGE_UPLOAD\"  SET \"IMAGE_PATH\" = '" + ImagePath + "' WHERE \"ITEM_ID\" = " + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    public class ItemImageUploadDAOSQLImpl : ItemImageUploadDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveItems(int ItemId, string ImagePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".ITEM_MASTER_IMAGE_UPLOAD (ITEM_MASTER_ID, ITEM_MASTER_IMAGE_PATH) VALUES ( " + ItemId + ", '" + ImagePath + "' );";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveMasterItemsImage(int ItemId, string ImagePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".ITEM_MASTER_IMAGE_UPLOAD (ITEM_MASTER_ID, ITEM_MASTER_IMAGE_PATH) VALUES ( " + ItemId + ", '" + ImagePath + "' );";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateItems(int ItemId, string ImagePath, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ITEM_MASTER_IMAGE_UPLOAD   WHERE ITEM_MASTER_ID = " + ItemId + "";
            if (Convert.ToDecimal(dbConnection.cmd.ExecuteScalar()) == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".ITEM_MASTER_IMAGE_UPLOAD (ITEM_MASTER_ID, ITEM_MASTER_IMAGE_PATH) VALUES ( " + ItemId + ", '" + ImagePath + "' );";
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                //dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_IMAGE_UPLOAD  SET IMAGE_PATH = '" + ImagePath + "' WHERE ITEM_ID = " + ItemId + "";
                //return dbConnection.cmd.ExecuteNonQuery();

                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".ITEM_MASTER_IMAGE_UPLOAD  SET ITEM_MASTER_IMAGE_PATH = '" + ImagePath + "' WHERE ITEM_MASTER_ID = " + ItemId + "";
                return dbConnection.cmd.ExecuteNonQuery();
            }


        }
    }
}
