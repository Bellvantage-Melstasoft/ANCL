using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface AddItemController
    {
        int SaveItems(int itemid, int CategoryId, int SubCategoryId, string ItemName, int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId, string HsId, string Model, string PartId, int measurementid, int ItemType,int stockMaintainingType);
        int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy,string ReferenceNo, int CompnayId, int measurementid, string hsId, string model, string partId, int ItemType, int ReorderLevel, int stockMaintainingType, List<ItemMeasurement> measurements, List<Conversion> conversions, List<WarehouseInventoryBatches> batches);
        int DeleteItems(int ItemId);
        List<AddItem> FetchItemList();
        List<AddItem> FetchItemListById(int ItemId);
        AddItem FetchItemListByIdObj(int ItemId);
        List<AddItem> FetchItemListDetailed(int companyId);
        int DeleteInActiveItems(int ItemId, int CategoryId, int SubCategoryId);
        int GetIdByItemName(int CompanyId, string ItemName);
        List<AddItem> SearchedItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName);
        List<AddItem> FetchItemsByCategories(int MainCategoryId, int SubCategoryId, int CompanyId);
        AddItem FetchItemObj(int ItemId);
        int UpdateItemStatus(int companyId, int CategoryId, int subCategoryId, int itemid, int status);
        List<AddItem> ItemsInInventoryByCatagory(int MainCategoryId, int SubCategoryId, int level);
        AddItem FetchItemByItemId(int ItemId);
        List<AddItem> GetItemsForMrnAndPr(int MainCategoryId, int SubCategoryId, int ItemType, int companyId);
        List<AddItem> FetchItemListDetailedFilter(int companyId, int mainCatId, int subCatId);
        List<AddItem> FetchItemByItemName(int companyId, string ItemName);
        List<AddItem> FetchItemByItemCode(int companyId, string ItemCode);
        int GetMeasurementIdOfItem(int ItemId, int CompanyId);
        AddItem FetchItemByItemId(int ItemId, int companyid);
        int GetStockMaintaininType(int ItemId, int CompanyId);
    }
    public class AddItemControllerImpl : AddItemController
    {
        public int SaveItems(int itemid, int CategoryId, int SubCategoryId, string ItemName, int IsActive, DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy, string ReferenceNo, int CompnayId,string HsId, string Model , string PartId, int measurementid, int ItemType, int stockMaintainingType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.SaveItems(itemid, CategoryId, SubCategoryId, ItemName, IsActive, CreatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy, ReferenceNo, CompnayId,HsId,Model,PartId,measurementid, ItemType, stockMaintainingType, dbConnection);
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

        public int UpdateItems(int ItemId, int CategoryId, int SubCategoryId, string ItemName,  DateTime CreatedDateTime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy,string ReferenceNo, int CompnayId, int measurementid, string hsId, string model, string partId, int ItemType, int ReorderLevel, int stockMaintainingType, List<ItemMeasurement> measurements, List<Conversion> conversions, List<WarehouseInventoryBatches> batches)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                WarehouseInventoryBatchesDAO batchedDAO = DAOFactory.CreateWarehouseInventoryBatchesDAO();
                WarehouseDAOInterface warehouseDAO = DAOFactory.CreateWarehouseDAO();
                int status= addItemDAO.UpdateItems(ItemId, CategoryId, SubCategoryId, ItemName,  CreatedDateTime, CreatedBy, UpdatedDateTime, UpdatedBy,ReferenceNo,CompnayId, measurementid, hsId, model, partId, ItemType, ReorderLevel, stockMaintainingType ,dbConnection);

                if(status > 0)
                {
                    if (measurements.Count > 0)
                    {
                        status = DAOFactory.CreateItemMeasurementDAO().AddItemMeasurement(measurements, ItemId, CompnayId, dbConnection);
                    }
                    else
                    {
                        status = 1;
                    }

                    if (status > 0)
                    {
                        if (conversions.Count > 0)
                        {
                            status = DAOFactory.CreateConversionDAO().AddConversion(conversions, ItemId, CompnayId, dbConnection);

                        }
                        else
                        {
                            status = 1;
                        }
                        return 1;

                        //if (status > 0) {
                        //    string warhouseIds = string.Join(",", batches.Select(t => t.WarehouseID));

                        //    status = batchedDAO.UpdateItemBatches(ItemId, CompnayId, warhouseIds, stockMaintainingType, batches, dbConnection);

                        //    if (stockMaintainingType != 1 && batches.Count == 0) {
                        //        List<WarehouseInventory> warehouseInventory = warehouseDAO.getWarehouseListAtAddItemsForUpdate(CompnayId, ItemId, dbConnection);

                        //        if (warehouseInventory.Count != 0) {
                        //            List<WarehouseInventoryBatches> listOfbatches = new List<WarehouseInventoryBatches>();
                        //            WarehouseInventoryBatches item = null;
                        //            for (int t = 0; t < warehouseInventory.FindAll(x => x.AvailableQty != 0 && x.StockValue != 0).Count; ++t) {
                        //                item = new WarehouseInventoryBatches();
                        //                item.CompanyId = CompnayId;
                        //                item.WarehouseID = warehouseInventory[t].WarehouseID;
                        //                item.ItemID = ItemId;
                        //                item.AvailableStock = warehouseInventory[t].AvailableQty;
                        //                item.StockValue = warehouseInventory[t].StockValue;
                        //                item.HoldedQty = warehouseInventory[t].HoldedQty;
                        //                item.LastUpdatedBy = Convert.ToInt32(UpdatedBy);
                        //                listOfbatches.Add(item);
                        //            }

                        //            if (batches.Count > 0) {
                        //                batchedDAO.saveWarehouseInventoryBatches(listOfbatches, dbConnection);
                        //            }

                        //        }
                        //    }
                        //    return 1;
                        //}
                        //else {
                        //    dbConnection.RollBack();
                        //    return -5;
                        //}
                    }
                    else
                    {
                        dbConnection.RollBack();
                        return -4;
                    }

                }
                else
                {
                    return status;
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

        public int DeleteItems(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.DeleteItems(ItemId, dbConnection);
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

        public List<AddItem> FetchItemList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemList(dbConnection);
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

        public List<AddItem> FetchItemListById(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemListById(ItemId, dbConnection);
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

        public AddItem FetchItemListByIdObj(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemListByIdObj(ItemId, dbConnection);
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

        public AddItem FetchItemByItemId(int ItemId) {
            DBConnection dbConnection = new DBConnection();
            try {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemByItemId(ItemId, dbConnection);
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

        public List<AddItem> FetchItemListDetailed(int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemListDetailed(companyId,dbConnection);
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
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.DeleteInActiveItems( ItemId,  CategoryId,  SubCategoryId, dbConnection);
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

        public int GetIdByItemName(int CompanyId, string ItemName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.GetIdByItemName(CompanyId, ItemName, dbConnection);
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
        public int GetStockMaintaininType(int ItemId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.GetStockMaintaininType(ItemId, CompanyId, dbConnection);
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


        public List<AddItem> SearchedItemName(int MainCategoryId, int SubCategoryId, int CompanyId, string ItemName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.SearchedItemName(MainCategoryId, SubCategoryId, CompanyId,ItemName, dbConnection);
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

        public List<AddItem> FetchItemsByCategories(int MainCategoryId, int SubCategoryId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemsByCategories( MainCategoryId,  SubCategoryId,  CompanyId, dbConnection);
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

        public AddItem FetchItemObj(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                ItemCategoryDAO itemCategoryDAO = DAOFactory.CreateItemCategoryDAO();
                ItemSubCategoryDAO itemSubCategoryDAO = DAOFactory.CreateItemSubCategoryDAO();
                AddItem item= addItemDAO.FetchItemObj(ItemId, dbConnection);
                
                List<ItemCategory> itemlist= itemCategoryDAO.FetchItemCategoryById(item.CategoryId, dbConnection);


                foreach (var itemp in itemlist)
                {
                    item.CategoryName = itemp.CategoryName;
                }

                item.SubCategoryName = itemSubCategoryDAO.FetchItemSubCategoryListByIdObj(item.SubCategoryId, dbConnection).SubCategoryName;



                return item;

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

        public int UpdateItemStatus(int companyId, int CategoryId, int subCategoryId, int itemid, int status)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.UpdateItemStatus(companyId, CategoryId, subCategoryId, itemid, status, dbConnection);
                    
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


        public List<AddItem> ItemsInInventoryByCatagory(int MainCategoryId, int SubCategoryId, int level)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.ItemsInInventoryByCatagory(MainCategoryId, SubCategoryId, level, dbConnection);
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

        public List<AddItem> GetItemsForMrnAndPr(int MainCategoryId, int SubCategoryId, int ItemType, int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.GetItemsForMrnAndPr(MainCategoryId, SubCategoryId, ItemType, companyId, dbConnection);
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

        public List<AddItem> FetchItemListDetailedFilter(int companyId, int mainCaId, int subCatId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemListDetailedFilter(companyId, mainCaId, subCatId, dbConnection);
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

        public List<AddItem> FetchItemByItemName(int companyId, string ItemName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemByItemName(companyId, ItemName, dbConnection);
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

        public List<AddItem> FetchItemByItemCode(int companyId, string ItemCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemByItemCode(companyId, ItemCode, dbConnection);
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


        public AddItem FetchItemByItemId(int ItemId, int companyid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.FetchItemByItemId(ItemId, companyid, dbConnection);

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
        public int GetMeasurementIdOfItem(int ItemId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                return addItemDAO.GetMeasurementIdOfItem(ItemId, CompanyId, dbConnection);
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
