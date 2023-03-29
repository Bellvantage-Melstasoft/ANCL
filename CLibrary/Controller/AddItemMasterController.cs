using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface AddItemMasterController
    {
        int SaveItems(int CategoryId, int SubCategoryId, string ItemName, int IsActive, DateTime CreatedDateTime, string CreatedBy,
        DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, string HsId, string Model, string PartId, int measurementId,
        int ItemType, int stockMaintainingType, List<Conversion> conversions, int CompnayId, List<ItemMeasurement> measurements, List<WarehouseInventory> inventoryList, string Remarks, List<WarehouseInventoryBatches> batches, List<ArraySpec> BOMList);
        // int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy,string ReferenceNo);
        int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int companyId, int measurementid);
        int DeleteItems(int ItemId);
        List<AddItemMaster> FetchItemList();
        List<AddItemMaster> FetchItemListById(int ItemId);
        AddItemMaster FetchItemListByIdObj(int ItemId);
        List<AddItemMaster> FetchItemListDetailed();
        int DeleteInActiveItems(int ItemId, int CategoryId, int SubCategoryId);
        int GetIdByItemName(string ItemName);
        List<AddItemMaster> SearchedItemName(int MainCategoryId, int SubCategoryId, string ItemName, int companyid);
        List<AddItemMaster> FetchItemsByCategories(int MainCategoryId, int SubCategoryId);
        AddItemMaster FetchItemObj(int ItemId);
    }
    public class AddItemMasterControllerImpl : AddItemMasterController
    {

        public int SaveItems(int CategoryId, int SubCategoryId, string ItemName, int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime,
            string UpdatedBy, string ReferenceNo, string HsId, string Model, string PartId, int measurementId, int ItemType, int stockMaintainingType,
            List<Conversion> conversions, int CompnayId, List<ItemMeasurement> measurements, List<WarehouseInventory> inventoryList, string Remarks, List<WarehouseInventoryBatches> batches, List<ArraySpec> BOMList)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                int resulItems;
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                InventoryDAOInterface inventoryDAOInterface = DAOFactory.CreateInventoryDAO();
                WarehouseInventoryBatchesDAO warehouseInventoryBatchesDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                ItemImageUploadDAO itemImageUploadDAO = DAOFactory.CreateItemImageUploadDAO();
                AddItemBOMDAO addItemBOMDAO = DAOFactory.CreateAddItemBOMDAO();
                int resultMaster = addItemMasterDAO.SaveItems(CategoryId, SubCategoryId, ItemName, IsActive, CreatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy, ReferenceNo, HsId, Model, PartId, measurementId, ItemType, stockMaintainingType, dbConnection);

                if (resultMaster > 0)
                {
                     resulItems = addItemDAO.SaveItems(resultMaster, CategoryId, SubCategoryId, ItemName, IsActive, CreatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy, ReferenceNo, CompnayId, HsId, Model, PartId, measurementId, ItemType, stockMaintainingType, dbConnection);

                    int MeasurementResult = DAOFactory.CreateItemMeasurementDAO().AddItemMeasurement(measurements, resultMaster, CompnayId, dbConnection);

                    if (MeasurementResult > 0)
                    {

                        int ConversionResult = 0;

                        if (conversions.Count > 0)
                        {
                            ConversionResult = DAOFactory.CreateConversionDAO().AddConversion(conversions, resultMaster, CompnayId, dbConnection);
                        }
                        else
                        {
                            ConversionResult = 1;
                        }

                        if (ConversionResult > 0)
                        {
                            if (resulItems > 0)
                            {
                                int stock = inventoryDAOInterface.addNewCompanyStock(CompnayId, resulItems, 0, int.Parse(CreatedBy), dbConnection);


                                if (stock > 0)
                                {
                                    if (inventoryList != null)
                                    {
                                        if (!string.IsNullOrEmpty(Remarks))
                                        {

                                            stock = inventoryDAOInterface.addWarehouseStock(inventoryList, int.Parse(CreatedBy), resulItems, CompnayId, Remarks, dbConnection);

                                            if (stock <= 0)
                                            {
                                                dbConnection.RollBack();
                                                return -6;
                                            }

                                            batches.ForEach(t => t.ItemID = resultMaster);
                                            if (stockMaintainingType != 1)
                                            {
                                                stock = warehouseInventoryBatchesDAO.saveWarehouseInventoryBatches(batches, dbConnection);
                                                if (stock <= 0)
                                                {
                                                    dbConnection.RollBack();
                                                    return -7;
                                                }
                                                else{

                                                    for (int i = 0; i < BOMList.Count(); i++)
                                                    {
                                                        addItemBOMDAO.SaveAddItemBOM(CompnayId, resulItems, i + 1, BOMList[i].Metirial, BOMList[i].Description, LocalTime.Now, LocalTime.Now, CreatedBy, UpdatedBy, 1, dbConnection);
                                                    }

                                                    return resulItems;
                                                }
                                            }
                                            else
                                            {
                                                for (int i = 0; i < BOMList.Count(); i++)
                                                {
                                                    addItemBOMDAO.SaveAddItemBOM(CompnayId, resulItems, i + 1, BOMList[i].Metirial, BOMList[i].Description, LocalTime.Now, LocalTime.Now, CreatedBy, UpdatedBy, 1, dbConnection);
                                                }
                                                return resulItems;
                                            }
                                        }
                                        else
                                        {
                                            dbConnection.RollBack();
                                            return -5;
                                        }


                                    }
                                    else
                                    {

                                        for (int i = 0; i < BOMList.Count(); i++)
                                        {
                                            addItemBOMDAO.SaveAddItemBOM(CompnayId, resulItems, i + 1, BOMList[i].Metirial, BOMList[i].Description, LocalTime.Now, LocalTime.Now, CreatedBy, UpdatedBy, 1, dbConnection);
                                        }
                                        return stock;

                                    }
                                }


                               
                            }
                            else
                            {
                                dbConnection.RollBack();
                                return -3;
                            }
                        }
                        else
                        {
                            dbConnection.RollBack();
                            return -9;
                        }
                    }
                    else
                    {
                        dbConnection.RollBack();
                        return -8;
                    }

                }
                else
                {
                    dbConnection.RollBack();
                    return -2;
                }

                return resulItems;
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



        public int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int companyId, int measurementid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.UpdateItems(ItemId, CategoryId, SubCategoryId, ItemName, CreatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy, ReferenceNo, companyId, measurementid, dbConnection);
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

        public int DeleteItems(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.DeleteItems(ItemId, dbConnection);
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

        public List<AddItemMaster> FetchItemList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.FetchItemList(dbConnection);
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

        public List<AddItemMaster> FetchItemListById(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.FetchItemListById(ItemId, dbConnection);
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

        public AddItemMaster FetchItemListByIdObj(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.FetchItemListByIdObj(ItemId, dbConnection);
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

        public List<AddItemMaster> FetchItemListDetailed()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.FetchItemListDetailed(dbConnection);
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

        public int DeleteInActiveItems(int ItemId, int CategoryId, int SubCategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.DeleteInActiveItems(ItemId, CategoryId, SubCategoryId, dbConnection);
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

        public int GetIdByItemName(string ItemName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.GetIdByItemName(ItemName, dbConnection);
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

        public List<AddItemMaster> SearchedItemName(int MainCategoryId, int SubCategoryId, string ItemName, int companyid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.SearchedItemName(MainCategoryId, SubCategoryId, ItemName, companyid, dbConnection);
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

        public List<AddItemMaster> FetchItemsByCategories(int MainCategoryId, int SubCategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.FetchItemsByCategories(MainCategoryId, SubCategoryId, dbConnection);
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

        public AddItemMaster FetchItemObj(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemMasterDAO addItemMasterDAO = DAOFactory.CreateAddItemMasterDAO();
                return addItemMasterDAO.FetchItemObj(ItemId, dbConnection);
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
