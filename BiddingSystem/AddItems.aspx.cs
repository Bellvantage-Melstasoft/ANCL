using BiddingSystem.Helpers;
using BiddingSystem.Helpers;
using BiddingSystem.ViewModels.CS;
using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class AddItems : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        ItemImageUploadController itemImageUploadController = ControllerFactory.CreateItemImageUploadController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        AddItemBOMController addItemBOMController = ControllerFactory.CreateAddItemBOMController();
        InventoryControllerInterface inventoryController = ControllerFactory.CreateInventoryController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
        HSController HSController = ControllerFactory.CreateHScodeController();
        ItemSubCategoryMasterController itemSubCategoryMasterController = ControllerFactory.CreateItemSubCategoryMasterController();
        AddItemMasterController addItemMasterController = ControllerFactory.CreateAddItemMasterController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
        StockOverrideLogController stockOverrideLogController = ControllerFactory.CreateStockOverrideLogController();
        WarehouseInventoryBatchesController warehouseInventoryBatchesController = ControllerFactory.CreateWarehouseInventoryBatchesController();
        IConversionController iConversionController = ControllerFactory.CreateConversionController();
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefItemCategory";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabItemCategory";
                ((BiddingAdmin)Page.Master).subTabValue = "AddItems.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "addItemsLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 4, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }

            msg.Visible = false;
            if (!IsPostBack) {
                try {
                    ViewState["status"] = 0;
                    pnlSpec.Visible = true;

                    Session["AddedMeasurements"] = null;
                    ViewState["PageIndexNo"] = 1;
                    populateDropdown();
                    LoadGV();

                    ddlSortMainCateory_SelectedIndexChanged(null, null);
                    BindUomTypes(null);
                    BindMeasurementDetails();

                    imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();

                   
                }
                catch (Exception ex) {
                }
            }
            //ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "GetButtonCick();", true);
        }

        private void populateDropdown() {
            try {
                // Main Category
                List<ItemCategoryMaster> itemCategoryMaster = itemCategoryMasterController.FetchItemCategoryfORSubCategoryCreationList(int.Parse(Session["CompanyId"].ToString()));
                ViewState["MainCategory"] = new JavaScriptSerializer().Serialize(itemCategoryMaster);
                ddlItemMasterCategory.DataSource = itemCategoryMaster;
                ddlItemMasterCategory.DataValueField = "CategoryId";
                ddlItemMasterCategory.DataTextField = "CategoryName";
                ddlItemMasterCategory.DataBind();
                ddlItemMasterCategory.Items.Insert(0, new ListItem("Select All", ""));

                //below one - Main category
                ddlMainCategory.DataSource = itemCategoryMaster;
                ddlMainCategory.DataValueField = "CategoryId";
                ddlMainCategory.DataTextField = "CategoryName";
                ddlMainCategory.DataBind();
                ddlMainCategory.Items.Insert(0, new ListItem("Select Main Category", ""));

                //HS Codes
                List<DutyRates> DutyRateList = ControllerFactory.CreateDutyRatesController().GetRates();

                for (int i = 0; i < DutyRateList.Count; i++) {
                    DutyRateList[i].HsIdName = DutyRateList[i].HsIdName + " - " + DutyRateList[i].HsId;
                }

                ddlHsCode.DataSource = DutyRateList;
                ddlHsCode.DataValueField = "HsId";
                ddlHsCode.DataTextField = "HsIdName";
                ddlHsCode.DataBind();
                ddlHsCode.Items.Insert(0, new ListItem("Select HS Code", "0"));

                //// Measurements
                //ddlMeasurement.DataSource = unitMeasurementController.fetchMeasurementsByCompanyID(int.Parse(Session["CompanyId"].ToString()));
                //ddlMeasurement.DataValueField = "measurentId";
                //ddlMeasurement.DataTextField = "measurementShortName";
                //ddlMeasurement.DataBind();
                //ddlMeasurement.Items.Insert(0, new ListItem("Select measurement type", ""));


                LoadDDLMainCatregoryForSort();
            }
            catch (Exception ex) {
                throw;
            }
        }

        private void LoadGV() {
            try {
                string itemName = txtItemName.Text;

                //gvAddItems.DataSource = addItemController.FetchItemListDetailed(int.Parse(ViewState["CompanyId"].ToString())).Where(p => p.CompanyId == int.Parse(ViewState["CompanyId"].ToString())).OrderBy(x => x.ItemId).ToList();
                gvAddItems.DataSource = addItemController.FetchItemListDetailed(int.Parse(ViewState["CompanyId"].ToString())).Where(p => p.CompanyId == int.Parse(ViewState["CompanyId"].ToString())).OrderBy(x => x.ItemName).ToList();
                gvAddItems.DataBind();
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            try {
                try {


                    List<AddItemMaster> fetchItems = addItemMasterController.SearchedItemName(int.Parse(ddlItemMasterCategory.SelectedValue), int.Parse(ddlItemMasterSubCategory.SelectedValue), txtFindItemName.Text.ProcessString(), int.Parse(ViewState["CompanyId"].ToString()));

                    gvMasterItemList.DataSource = fetchItems;
                    gvMasterItemList.DataBind();

                    if (fetchItems.Count() == 0) {
                        ddlMainCategory.SelectedValue = ddlItemMasterCategory.SelectedValue;
                        ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(int.Parse(ddlItemMasterCategory.SelectedValue), int.Parse(ViewState["CompanyId"].ToString()));
                        ddlSubCategory.DataTextField = "SubCategoryName";
                        ddlSubCategory.DataValueField = "SubCategoryId";
                        ddlSubCategory.DataBind();
                        ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));
                        //ddlSubCategory.SelectedIndex = ddlSubCategory.sea ddlItemMasterSubCategory.SelectedValue;
                        ddlSubCategory.Items.FindByValue(ddlItemMasterSubCategory.SelectedValue).Selected = true;

                        txtItemName.Text = txtFindItemName.Text;
                        ClearFields();

                    }
                }
                catch (Exception) {

                    throw;
                }
            }
            catch (Exception) {

                throw;
            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e) {
            try {
                if (ddlItemMasterCategory.SelectedValue != "" && ddlItemMasterSubCategory.SelectedValue != "") {
                    ddlMainCategory.SelectedValue = ddlItemMasterCategory.SelectedValue;
                    hndAddNewItem.Value = "1";
                    ddlMainCategory_SelectedIndexChanged(sender, e);
                    RequiredFieldValidator7.Visible = false;
                    //lblSelectSubError.Visible = false;
                    txtItemName.Text = "";
                    txtReferenceNo.Text = "";
                    btnSave.Text = "Save";
                    //txtreorder.Text = "0";
                    hdnImgPathEdit.Value = "";
                    itemCount.InnerText = "";
                    imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();
                }
                else {
                    //lblSelectSubError.Visible = true;
                    RequiredFieldValidator7.Visible = true;
                }
            }
            catch (Exception ex) {
                throw;
            }
        }

        //public List<AddItemBOM> GetItemDescripyionlist()
        //{
        //    List<AddItemBOM> ItemDescriptionlist = new List<AddItemBOM>();

        //    foreach( GridViewRow row in gvTempBoms.Rows)
        //    {
        //        AddItemBOM itemdes = new AddItemBOM();

        //        itemdes.companyId = Convert.ToInt32(Session["CompanyId"].ToString());

        //        itemdes.SeqNo = Convert.ToInt32(row.Cells[1].Text);
        //        itemdes.Meterial =row.Cells[2].Text.ToString();
        //        itemdes.Description = row.Cells[2].Text.ToString();
        //        itemdes.isActive = 0;
        //        itemdes.createdDate = LocalTime.Now;
        //        itemdes.createdBy = Session["UserId"].ToString();

        //        ItemDescriptionlist.Add(itemdes);

        //    }

        //    return ItemDescriptionlist;




        //}

        [WebMethod]

        public static string GetItemDescripyionlist() {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {

                    ResultVM response = new ResultVM();

                    return JsonConvert.SerializeObject(response);
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }

        }

        private static string SessionExpired() {
            return
                JsonConvert.SerializeObject(
                new ResultVM() {
                    Status = 401,
                    Data = "Session Expired"
                });
        }

        private static string ServerError() {
            return
                JsonConvert.SerializeObject(
                new ResultVM() {
                    Status = 500,
                    Data = "Server Error Occured"
                });
        }



        protected void btnSave_Click(object sender, EventArgs e) {
            try {
                if (string.IsNullOrEmpty(txtReferenceNo.Text)) { // since this is mandortory while inserting into table  , making text if user doesnt enter (requested by user to mremove mandotory)
                    string temp = ddlSubCategory.SelectedItem.Text.Substring(0, 3);
                    txtReferenceNo.Text = temp + txtItemName.Text.Replace(" ", string.Empty);
                }
                //txtreorder.Text = txtreorder.Text == "" ? "0" : txtreorder.Text;
                #region NewUpdate - Adee
                if (btnSave.Text == "Save") {
                    List<WarehouseInventoryBatches> warehouseInventoryBatches = new List<WarehouseInventoryBatches>();
                    if (Session["ItemBatches"] != null) {
                        warehouseInventoryBatches = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(Session["ItemBatches"].ToString());

                        for (int i = 0; i < warehouseInventoryBatches.Count; i++) {
                            if (warehouseInventoryBatches[i].ExpiryDate != DateTime.MinValue) {
                                warehouseInventoryBatches[i].ExpiryDate = warehouseInventoryBatches[i].ExpiryDate.AddMinutes(330);
                            }
                        }
                    }
                    if (txtRemarks.Text == "")
                        txtRemarks.Text = "Initial";

                    List<ItemMeasurement> measurements = new List<ItemMeasurement>();
                    List<Conversion> conversions = new List<Conversion>();

                    List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;

                    for (int i = 0; i < addedMeasurements.Count; i++) {
                        measurements.Add(new ItemMeasurement {
                            MeasurementDetailId = addedMeasurements[i].MeasurementId,
                            IsBase = addedMeasurements[i].IsBase,
                            IsActive = addedMeasurements[i].IsActive
                        });

                        if (addedMeasurements[i].ConversionToId != 0) {
                            conversions.Add(new Conversion {
                                FromId = addedMeasurements[i].MeasurementId,
                                ToId = addedMeasurements[i].ConversionToId,
                                Multiplier = addedMeasurements[i].Multiplier,
                                IsActive = 1
                            });
                        }
                    }

                    string[] arr = hdnField.Value.Split(',');
                    var list = arr.ToList();
                    list.RemoveAll(o => string.IsNullOrWhiteSpace(o));
                    List<ArraySpec> arraList = new List<ArraySpec>();
                    if (list != null || arr[1] != "") {
                        for (int i = 0; i < list.Count; i += 2) {
                            ArraySpec arrayItems = new ArraySpec();
                            arrayItems.Metirial = list[i];
                            arrayItems.Description = list[i + 1];
                            arraList.Add(arrayItems);
                        }
                    }


                    int ItemMasterId = addItemMasterController.SaveItems(int.Parse(ddlMainCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue), txtItemName.Text, 1, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(),
                        txtReferenceNo.Text, ddlHsCode.SelectedValue, txtModel.Text, txtPartId.Text, int.Parse(ddlMeasurement.SelectedValue), int.Parse(ddlItemType.SelectedValue),
                        int.Parse(ddlStockMaintainingType.SelectedValue), conversions, int.Parse(Session["CompanyId"].ToString()), measurements, Session["Warehouses"] == null ? null : Session["Warehouses"] as List<WarehouseInventory>,
                        txtRemarks.Text, warehouseInventoryBatches, arraList);
                    if (ItemMasterId > 0) {
                        int imageUpload = 0;
                        if (fileUpload1.PostedFile != null && fileUpload1.PostedFile.FileName != "") {
                            string nameOfUploadedFile1 = ItemMasterId + "_" + int.Parse(ddlMainCategory.SelectedValue) + "_" + int.Parse(ddlSubCategory.SelectedValue) + "_1";
                            string UploadedFileName1 = nameOfUploadedFile1.Replace(" ", String.Empty);
                            string FileName1 = Path.GetFileName(fileUpload1.PostedFile.FileName);
                            string filename2 = UploadedFileName1 + "." + FileName1.Split('.').Last();

                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/ItemCategory/AddItemImage/" + filename2))) {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/ItemCategory/AddItemImage/" + filename2));
                            }
                            fileUpload1.SaveAs(HttpContext.Current.Server.MapPath("~/ItemCategory/AddItemImage/" + filename2));
                            string MasteritemimagePath = "~/ItemCategory/AddItemImage/" + filename2;
                            ViewState["imagePath"] = "~/ItemCategory/AddItemImage/" + filename2;
                            imageUpload = itemImageUploadController.SaveMasterItemsImage(ItemMasterId, MasteritemimagePath);
                        }
                        else {
                            imageUpload = itemImageUploadController.SaveItems(ItemMasterId, "~/LoginResources/images/noItem.png");
                        }

                        Session["Warehouses"] = null;
                        ClearFields();
                        LoadGV();
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);


                    }
                    else if (ItemMasterId == -7) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on adding Warehouse Inventory Batches', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                    else if (ItemMasterId == -6) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on adding Warehouse stock', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                    else if (ItemMasterId == -5) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Add Remarks for warehouse stock', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                    else if (ItemMasterId == -4) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on adding company stock', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                    else if (ItemMasterId == -3) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Item Code exixts', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                    else if (ItemMasterId == -2) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Item name exixts', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                    else if (ItemMasterId == -1) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Item Name already exists in Item Master', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on create Item Master', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }
                }

                if (btnSave.Text == "Update") {
                    List<WarehouseInventoryBatches> batchList = null;
                    //if (ViewState["BatchAvailability"].ToString() != null) {
                    //    batchList = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(ViewState["BatchAvailability"].ToString());
                    //}

                    
                    //if (int.Parse(ViewState["status"].ToString()) == 0 ) {
                    //    if (ddlMeasurement.SelectedValue.ToString() != (string)Session["SeletedMesurementId"].ToString()) {
                    //        //List<WarehouseInventory> listInventory = Session["Warehouses"] as List<WarehouseInventory>;

                    //        foreach (WarehouseInventory newInventory in Session["Warehouses"] as List<WarehouseInventory>) { 
                    //                decimal available = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), newInventory.AvailableQty, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));
                    //                decimal reorderLevel = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), newInventory.ReorderLevel, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));

                    //                newInventory.AvailableQty = decimal.Parse(available.ToString("N2"));
                    //                newInventory.ReorderLevel = decimal.Parse(reorderLevel.ToString("N2"));

                    //            }
                            
                    //    }
                    //}

                    int stockMaintaingType = addItemController.GetStockMaintaininType(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                    int newStockMaintainingType = int.Parse(ddlStockMaintainingType.SelectedValue);

                    List<WarehouseInventory> InventoryList = ControllerFactory.CreateInventoryController().GetInventoryByItemId(int.Parse(Session["itemId"].ToString()));

                    if (stockMaintaingType != newStockMaintainingType) {

                        if (stockMaintaingType == 1 && (newStockMaintainingType == 2 || newStockMaintainingType == 3)) {
                            warehouseInventoryBatchesController.AddBAtchesMaintainingTypeChanges(InventoryList, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                        }
                        else if ((stockMaintaingType == 2 || stockMaintaingType == 3) && newStockMaintainingType == 1 ) {
                            warehouseInventoryBatchesController.DeleteStockMaintainingTypeChanges(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                        }
                    }

                    List<WarehouseInventoryBatches> warehouseInventoryBatches = new List<WarehouseInventoryBatches>();
                    if (Session["ItemBatches"] != null) {
                        warehouseInventoryBatches = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(Session["ItemBatches"].ToString());
                    }



                    List<ItemMeasurement> measurements = new List<ItemMeasurement>();
                    List<Conversion> conversions = new List<Conversion>();

                    if (Session["AddedMeasurements"] != null) {
                        List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;
                        for (int i = 0; i < addedMeasurements.Count; i++) {
                            if (addedMeasurements[i].Status == 1) {
                                measurements.Add(new ItemMeasurement {
                                    MeasurementDetailId = addedMeasurements[i].MeasurementId,
                                    IsBase = addedMeasurements[i].IsBase,
                                    IsActive = addedMeasurements[i].IsActive
                                });

                                if (addedMeasurements[i].ConversionToId != 0) {
                                    conversions.Add(new Conversion {
                                        FromId = addedMeasurements[i].MeasurementId,
                                        ToId = addedMeasurements[i].ConversionToId,
                                        Multiplier = addedMeasurements[i].Multiplier,
                                        IsActive = 1
                                    });
                                }
                            }
                        }
                    }




                    if (int.Parse(Session["itemId"].ToString()) != 0) {

                        int ItemStockMaintainingType = ControllerFactory.CreateAddItemController().GetStockMaintaininType(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                        List<WarehouseInventoryBatches> deletedBatches = new List<WarehouseInventoryBatches>();
                        if (ViewState["deletedBatches"] != null) {
                            deletedBatches = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(ViewState["deletedBatches"].ToString());
                        }

                        List<WarehouseInventoryBatches> addedBatches = new List<WarehouseInventoryBatches>();
                        if (ViewState["addedBatches"] != null) {
                            addedBatches = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(ViewState["addedBatches"].ToString());

                            for (int i = 0; i < addedBatches.Count; i++) {
                                if (addedBatches[i].ExpiryDate != DateTime.MinValue) {
                                    addedBatches[i].ExpiryDate = addedBatches[i].ExpiryDate.AddMinutes(330);
                                }
                            }
                        }

                        int updateStatus = addItemController.UpdateItems(int.Parse(Session["itemId"].ToString()), int.Parse(ddlMainCategory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue), txtItemName.Text, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), txtReferenceNo.Text, int.Parse(Session["CompanyId"].ToString()), int.Parse(ddlMeasurement.SelectedValue), ddlHsCode.SelectedValue, txtModel.Text, txtPartId.Text, int.Parse(ddlItemType.SelectedValue), 0, int.Parse(ddlStockMaintainingType.SelectedValue), measurements, conversions, warehouseInventoryBatches);
                        if (updateStatus > 0) {
                            int imageUpload = 0;
                            if (fileUpload1.PostedFile != null && fileUpload1.PostedFile.FileName != "") {
                                string nameOfUploadedFile = int.Parse(Session["itemId"].ToString()) + "_" + int.Parse(ddlMainCategory.SelectedValue) + "_" + int.Parse(ddlSubCategory.SelectedValue) + "_1";
                                string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                                string FileName = Path.GetFileName(fileUpload1.PostedFile.FileName);
                                string filename1 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/ItemCategory/AddItemImage/" + filename1))) {
                                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/ItemCategory/AddItemImage/" + filename1));
                                }

                                fileUpload1.SaveAs(HttpContext.Current.Server.MapPath("~/ItemCategory/AddItemImage/" + filename1));

                                ViewState["imagePath"] = "~/ItemCategory/AddItemImage/" + filename1;

                                imageUpload = itemImageUploadController.UpdateItems(int.Parse(Session["itemId"].ToString()), ViewState["imagePath"].ToString());
                            }
                            int stock = 0;
                            if (Session["Warehouses"] != null) {
                                
                                List<WarehouseInventoryBatches> saveAddedBtchList = addedBatches.Where(ab => ab.addStatus != 2).ToList();
                                stock = inventoryController.updateWarehouseStock(Session["Warehouses"] as List<WarehouseInventory>, int.Parse(Session["UserId"].ToString()), int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), txtRemarks.Text, deletedBatches, ItemStockMaintainingType, addedBatches, batchList);
                            }

                            if (stock > 0 || Session["Warehouses"] == null) {
                                if (Session["Warehouses"] != null) {
                                    Session["Warehouses"] = null;
                                    ViewState["deletedBatches"] = null;
                                    ViewState["addedBatches"] = null;
                                    LoadGV();
                                    LoadDDLMainCatregoryForSort();
                                    ddlSortMainCateory_SelectedIndexChanged(null, null);
                                    ClearFields();
                                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                                }
                                else {

                                    Session["Warehouses"] = null;
                                    ViewState["deletedBatches"] = null;
                                    ViewState["addedBatches"] = null;
                                    LoadGV();
                                    ClearFields();
                                    LoadDDLMainCatregoryForSort();
                                    ddlSortMainCateory_SelectedIndexChanged(null, null);
                                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                                    //DisplayMessage("Item has been Created successfully", false);
                                }
                            }
                            else {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on Adding Stock Details', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            }

                        }
                        else if (updateStatus == -5) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on updating Conversion', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        }
                        else if (updateStatus == -4) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on updating Measurements', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        }
                        else if (updateStatus == -3) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on update Batches', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        }
                        else if (updateStatus == -2) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Item reference number already exists!', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        }
                        else if (updateStatus == -1) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Item Name already exists', showConfirmButton: true,timer: 4000}); });   </script>", false);

                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on update Item', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        }
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select Item', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }
                }


                #endregion
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void ClearFields() {
            hndAddNewItem.Value = "1";
            txtItemName.Text = "";
            ddlMainCategory.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlStockMaintainingType.Enabled = true;
            ddlMeasurement.Enabled = true;
            txtReferenceNo.Text = "";
            btnSave.Text = "Save";
            txtModel.Text = "";
            txtPartId.Text = "";
            hdnImgPathEdit.Value = "";
            itemCount.InnerText = "";

            lbAddedUOM.Items.Clear();
            Session["AddedMeasurements"] = null;
            ViewState["SelectedMasterId"] = null;
            Session["Warehouses"] = null;
            Session["ItemBatches"] = null;
            populateDropdown();
            BindSubCategory(0);
            LoadGV();
            ViewState["status"] = 0;
            ddlSortMainCateory_SelectedIndexChanged(null, null);
            BindUomTypes(null);
            BindMeasurementDetails();
            ddlMeasurement.Items.Clear();
            ddlStockMaintainingType.SelectedValue = "1";

            pnlSpec.Visible = true;
            imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();

            gvWarehouseInventory.DataSource = null;
            gvWarehouseInventory.DataBind();

            gvItemBatch.DataSource = null;
            gvItemBatch.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e) {
            try {
                ClearFields();

            }
            catch (Exception ex) {

            }
        }
        protected void btnCustomview_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int ItemId = int.Parse(gvAddItems.Rows[x].Cells[3].Text);
                List<Conversion> CustomConversion = ControllerFactory.CreateConversionController().GetCustomConversions(ItemId, int.Parse(Session["CompanyId"].ToString()));
                gvCustomMeasurement.DataSource = CustomConversion;
                gvCustomMeasurement.DataBind();


                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop'); $('#mdlCustom').modal('show'); });   </script>", false);


            }
            catch (Exception ex) {

            }
        }
        
        protected void lnkBtnDelete_Click(object sender, EventArgs e) {
            try {
                int updateStatus = 0;
                string itemId = hdnItemId.Value;
                string categoryid = hdnCatecoryIdId.Value;
                string subCategory = hdnSubCategoryId.Value;
                if (itemId != "" && itemId != null && subCategory != "" && subCategory != null && categoryid != null && categoryid != "" && hdnStatus.Value != "" && hdnStatus.Value != null) {
                    if (hdnStatus.Value == "Yes") {
                        updateStatus = addItemController.UpdateItemStatus(int.Parse(Session["CompanyId"].ToString()), int.Parse(categoryid), int.Parse(subCategory), int.Parse(itemId), 0);
                        LoadGV();
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                    if (hdnStatus.Value == "No") {
                        updateStatus = addItemController.UpdateItemStatus(int.Parse(Session["CompanyId"].ToString()), int.Parse(categoryid), int.Parse(subCategory), int.Parse(itemId), 1);
                        LoadGV();
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }

                    if (updateStatus > 0) {
                        hdnItemId.Value = null;
                        hdnCatecoryIdId.Value = null;
                        hdnSubCategoryId.Value = null;
                        hdnStatus.Value = null;
                        btnSearch_Click(sender, e);
                        imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on Delete Item', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please select Item to Delete!!', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }

            }
            catch (Exception ex) {

            }

        }

        protected void btnEdit_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                Session["itemId"] = int.Parse(gvAddItems.Rows[x].Cells[3].Text);

                AddItem item = addItemController.FetchItemByItemId(int.Parse(Session["itemId"].ToString()));
                txtItemName.Text = item.ItemName;
                txtModel.Text = item.Model;
                txtPartId.Text = item.PartId;
                txtReferenceNo.Text = item.ReferenceNo;
                //txtreorder.Text = item.ReorderLevel.ToString();
                ddlItemType.SelectedValue = item.ItemType.ToString();
                ddlMainCategory.SelectedValue = item.CategoryId.ToString();

                var SubCategory = itemSubCategoryController.FetchItemSubCategoryByCategoryId(int.Parse(ddlMainCategory.SelectedValue), int.Parse(Session["CompanyId"].ToString())); ;
                ddlSubCategory.DataSource = SubCategory;
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
                ddlSubCategory.SelectedValue = item.SubCategoryId.ToString();

                List<AddItemBOM> BOMList = addItemBOMController.GetBOMListByItemId(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemId"].ToString()));
                itemCount.InnerText = BOMList.Count.ToString();
                if (item.MeasurementId == 0) {
                    ddlMeasurement.SelectedIndex = 0;
                }
                else {
                    ddlMeasurement.SelectedValue = item.MeasurementId.ToString();
                }

                List<DutyRates> DutyRateList = ControllerFactory.CreateDutyRatesController().GetRates();
                for (int i = 0; i < DutyRateList.Count; i++) {
                    DutyRateList[i].HsIdName = DutyRateList[i].HsIdName + " - " + DutyRateList[i].HsId;
                }
                ddlHsCode.DataSource = DutyRateList;
                ddlHsCode.DataValueField = "HsId";
                ddlHsCode.DataTextField = "HsIdName";
                ddlHsCode.DataBind();
                ddlHsCode.Items.Insert(0, new ListItem("Select HS Code", "0"));

                if (item.HsId == null || item.HsId == "" || item.HsId == "0") {
                    ddlHsCode.SelectedIndex = 0;
                }
                else {
                    ddlHsCode.SelectedValue = item.HsId.ToString();
                }

                if (item.ItemImagePath == "" || item.ItemImagePath == "&nbsp;") {
                    imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();
                }
                else {
                    imageid.Src = item.ItemImagePath + "?" + LocalTime.Now.Ticks.ToString();
                }

                btnSave.Text = "Update";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                    "none",
                    "<script>    $(document).ready(function () { document.getElementById('DivEdit').scrollIntoView(true);     });   </script>",
                    false);
            }
            catch (Exception ex) {

            }
        }

        private void DisplayMessage(string message, bool isError) {
            msg.Visible = true;
            if (isError) {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }

        protected void gvAddItems_OnPageIndexChanging(Object sender, GridViewPageEventArgs e) {
            try {
                if (int.Parse(ViewState["PageIndexNo"].ToString()) == 1) {
                    gvAddItems.PageIndex = e.NewPageIndex;
                    int mainCaId = int.Parse(ddlSortMainCateory.SelectedValue);
                    int subCatId = int.Parse(ddlSortSubCateory.SelectedValue);

                    gvAddItems.DataSource = addItemController.FetchItemListDetailedFilter(int.Parse(ViewState["CompanyId"].ToString()), mainCaId, subCatId).Where(p => p.CompanyId == int.Parse(ViewState["CompanyId"].ToString())).OrderBy(x => x.ItemName).ToList();
                    gvAddItems.DataBind();
                }

                if (int.Parse(ViewState["PageIndexNo"].ToString()) == 2) {
                    gvAddItems.PageIndex = e.NewPageIndex;
                    var name = rbtnItemName.Checked == true ? 1 : 2;

                    if (name == 1) {

                        gvAddItems.DataSource = addItemController.FetchItemByItemName(int.Parse(ViewState["CompanyId"].ToString()), ViewState["ItemName"].ToString()).OrderBy(x => x.ItemName);
                        gvAddItems.DataBind();
                    }
                }

                if (int.Parse(ViewState["PageIndexNo"].ToString()) == 3) {
                    gvAddItems.PageIndex = e.NewPageIndex;
                    var code = rbtnItemCode.Checked == true ? 1 : 2;

                    if (code == 1) {

                        gvAddItems.DataSource = addItemController.FetchItemByItemCode(int.Parse(ViewState["CompanyId"].ToString()), ViewState["ItemCode"].ToString()).OrderBy(x => x.ItemName);
                        gvAddItems.DataBind();

                        ViewState["PageLoad"] = 3;
                    }


                }

            }
            catch (Exception) {
                throw;
            }
        }

        protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                BindSubCategory(Convert.ToInt32(ddlMainCategory.SelectedValue));

            }
            catch (Exception ex) {
                throw;
            }
        }

        public void BindSubCategory(int Selectedvalue) {
            if (Selectedvalue != 0 || !string.IsNullOrEmpty(Selectedvalue.ToString())) {
                //int mainCategoryId = int.Parse(ddlMainCategory.SelectedValue);
                ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(Selectedvalue, int.Parse(Session["CompanyId"].ToString()));
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));

                if (hndAddNewItem.Value == "1") {
                    ddlSubCategory.SelectedValue = ddlItemMasterSubCategory.SelectedValue;
                }
            }
        }
        protected void ddlItemMasterCategory_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (ddlItemMasterCategory.SelectedValue != "") {
                    ddlItemMasterSubCategory.DataSource = itemSubCategoryMasterController.FetchItemSubCategoryByCategoryId(int.Parse(ddlItemMasterCategory.SelectedValue));
                    ddlItemMasterSubCategory.DataValueField = "SubCategoryId";
                    ddlItemMasterSubCategory.DataTextField = "SubCategoryName";
                    ddlItemMasterSubCategory.DataBind();
                    ddlItemMasterSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));
                }
                else {
                    gvAddItems.DataSource = new JavaScriptSerializer().Deserialize<List<AddItem>>(ViewState["ItemList"].ToString()).OrderBy(x => x.ItemName);
                    gvAddItems.DataBind();
                    ddlItemMasterSubCategory.DataSource = new List<AddItem>();
                    ddlItemMasterSubCategory.DataBind();
                    ddlItemMasterSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));
                    ddlItemMasterSubCategory.SelectedIndex = 0;
                }
            }
            catch (Exception) {

                throw;
            }
        }
        
        protected void btnDoneBom_Click(object sender, EventArgs e) {
            List<AddItemBOM> List = new JavaScriptSerializer().Deserialize<List<AddItemBOM>>(Session["ItemBomNew"].ToString());

            int result = addItemBOMController.UpdateItemBoms(List, int.Parse(Session["itemid"].ToString()), int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
            if (result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('div').removeClass('modal-backdrop'); $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}) });   </script>", false);

            }

        }
        protected void btnDelete_Click(object sender, EventArgs e) {
            btnBomDone.Visible = true;
            int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
            int RandomId = int.Parse(gvBom.Rows[x].Cells[0].Text);

            List<AddItemBOM> List =  new JavaScriptSerializer().Deserialize<List<AddItemBOM>>(Session["ItemBomNew"].ToString());
            for (int i = 0; i < List.Count; i++) {
                if (List[i].num == RandomId) {
                    List.Remove(List[i]);
                }
            }
            Session["ItemBomNew"] = new JavaScriptSerializer().Serialize(List);

            gvBom.DataSource = List;
            gvBom.DataBind();
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop'); $('#mdlBOM').modal('show'); });   </script>", false);

        }
        protected void btnAddBom_Click(object sender, EventArgs e) {
            btnBomDone.Visible = true;
            Random r = new Random();
            if (Session["ItemBomNew"] != null) {

                
                List<AddItemBOM> ItemBom = new JavaScriptSerializer().Deserialize<List<AddItemBOM>>(Session["ItemBomNew"].ToString());
                AddItemBOM newItemBom = new AddItemBOM();
                newItemBom.num = r.Next();
                newItemBom.Material = txtMaterial.Text;
                newItemBom.Description = txtDescription.Text;

                ItemBom.Add(newItemBom);
                Session["ItemBomNew"] = new JavaScriptSerializer().Serialize(ItemBom);

                gvBom.DataSource = ItemBom;
                gvBom.DataBind();
            }
            else {
                List<AddItemBOM> ItemBom = new List<AddItemBOM>();

                AddItemBOM newItemBom = new AddItemBOM();
                newItemBom.num = r.Next();
                newItemBom.Material = txtMaterial.Text;
                newItemBom.Description = txtDescription.Text;

                ItemBom.Add(newItemBom);
                Session["ItemBomNew"] = new JavaScriptSerializer().Serialize(ItemBom);

                gvBom.DataSource = ItemBom;
                gvBom.DataBind();
            }
            txtDescription.Text = "";
            txtMaterial.Text = "";
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop'); $('#mdlBOM').modal('show'); });   </script>", false);

        }
        protected void btnViewBOM_Click(object sender, EventArgs e) {
            try {
                Random r = new Random();
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvAddItems.Rows[x].Cells[3].Text);
                Session["itemid"] = itemId.ToString();

                List<AddItemBOM> ItemBom = addItemBOMController.GetBOMListByItemId(int.Parse(Session["CompanyId"].ToString()), itemId);
                

                for (int i = 0; i < ItemBom.Count; i++) {
                    ItemBom[i].num = r.Next();

                }
                Session["ItemBomNew"] = new JavaScriptSerializer().Serialize(ItemBom);
                gvBom.DataSource = ItemBom;
                gvBom.DataBind();
                //msgpanel.Visible = false;
                //lblSuccess.Visible = false;
                //lblDanger.Visible = false;

                ////view Specification

                //
                //List<AddItemBOM> BOMList = addItemBOMController.GetBOMListByItemId(int.Parse(Session["CompanyId"].ToString()), itemId);
                //gvTempBoms.DataSource = BOMList;
                //gvTempBoms.DataBind();

                //imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {$('div').removeClass('modal-backdrop');  $('#mdlBOM').modal('show'); });   </script>", false);

            }
            catch (Exception ex) {
                throw ex;
            }

        }

        protected void ddlUOMTypes_SelectedIndexChanged(object sender, EventArgs e) {

            BindMeasurementDetails();

        }

        protected void btnAddUOM_Click(object sender, EventArgs e) {
            if (lbUOMs.SelectedValue != null && lbUOMs.SelectedValue != "") {
                List<MeasurementDetail> Uoms = ViewState["Measurements"] as List<MeasurementDetail>;

                MeasurementDetail selectedMeasurement = Uoms.Find(u => u.DetailId == int.Parse(lbUOMs.SelectedValue));

                ViewState["SelectedMasterId"] = selectedMeasurement.MasterId;

                List<AddedMeasurementsVM> addedMeasurements;

                if (Session["AddedMeasurements"] == null) {
                    addedMeasurements = new List<AddedMeasurementsVM>();

                    //Master Id 5 is Custom
                    if (selectedMeasurement.MasterId == 5) {
                        addedMeasurements.Add(new AddedMeasurementsVM {
                            MeasurementId = selectedMeasurement.DetailId,
                            MeasurementName = selectedMeasurement.MeasurementName,
                            MasterId = 5,
                            IsBase = 1,
                            IsActive = 1,
                            Status = 1
                        });
                    }
                    else {
                        if (selectedMeasurement.IsBase == 1) {
                            addedMeasurements.Add(new AddedMeasurementsVM {
                                MeasurementId = selectedMeasurement.DetailId,
                                MeasurementName = selectedMeasurement.MeasurementName,
                                MasterId = selectedMeasurement.MasterId,
                                IsBase = 1,
                                IsActive = 1,
                                IsStandard = 1,
                                Status = 1
                            });
                        }
                        else {
                            MeasurementDetail baseMeasurement = Uoms.Find(u => u.MasterId == int.Parse(ddlUOMTypes.SelectedValue) && u.IsBase == 1);

                            addedMeasurements.Add(new AddedMeasurementsVM {
                                MeasurementId = baseMeasurement.DetailId,
                                MeasurementName = baseMeasurement.MeasurementName,
                                MasterId = baseMeasurement.MasterId,
                                IsBase = 1,
                                IsActive = 0,
                                IsStandard = 1,
                                Status = 1
                            });

                            addedMeasurements.Add(new AddedMeasurementsVM {
                                MeasurementId = selectedMeasurement.DetailId,
                                MeasurementName = selectedMeasurement.MeasurementName,
                                MasterId = selectedMeasurement.MasterId,
                                IsBase = 0,
                                IsActive = 1,
                                IsStandard = 1,
                                ConversionToId = baseMeasurement.DetailId,
                                Multiplier = ControllerFactory.CreateConversionTableMasterController().GetConversionToBase(selectedMeasurement.DetailId).Multiplier,
                                Status = 1
                            });
                        }
                    }

                    Session["AddedMeasurements"] = addedMeasurements;
                    BindAddedMeasurements();
                }
                else {
                    addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;

                    if (selectedMeasurement.MasterId == 5) {
                        if (addedMeasurements.Exists(am => am.MasterId == 5)) {
                            //Requesting Multiplier

                            string html = "";
                            bool addedFirst = false; //to select the first option in the following loop

                            for (int i = 0; i < addedMeasurements.Count; i++) {
                                if (addedMeasurements[i].MasterId == 5) {
                                    if (addedFirst) {
                                        html += $"<option value='{addedMeasurements[i].MeasurementId}' selected> {addedMeasurements[i].MeasurementName} </option>";
                                        addedFirst = !addedFirst;
                                    }
                                    else {
                                        html += $"<option value='{addedMeasurements[i].MeasurementId}'> {addedMeasurements[i].MeasurementName} </option>";
                                    }
                                }
                            }

                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                                @"<script>    
                                $(document).ready(function () { 
                                swal.fire({
                                    title: 'Conversion',
                                    html: `<div class='row'>
                                                <div class='col-md-12'>
                                                    <strong id = 'qq' >How many</strong></br>
                                                    <select id = 'pp' class ='form-control'>
                                                    " + html + @"
                                                    </select>
                                                </div>
                                            </div>
                                           <div class='row'>
                                                <div class='col-md-12'>
                                                    <strong id = 'dd'>Are in 1 " + selectedMeasurement.MeasurementName + @"?</strong></br>
                                                    <input id = 'ss' type='text' class ='form-control'></br>
                                                </div>
                                           </div>`,
                                    type: 'warning',
                                    cancelButtonColor: '#d33',
                                    showCancelButton: true,
                                    showConfirmButton: true,
                                    confirmButtonText: 'Okay',
                                    cancelButtonText: 'Cancel',
                                    allowOutsideClick: false,
                                    preConfirm: function() {
                                        if ($('#ss').val() == '' || parseFloat($('#ss').val()) == 0) {
                                            $('#dd').prop('style', 'color:red');
                                            swal.showValidationError('Conversion Multiplier is Required');
                                            return false;
                                        }
                                        else {
                                            $('#ContentSection_hdnMultiplier').val($('#ss').val());
                                            $('#ContentSection_hdnSelectedMeasurement').val($('#pp option:selected').val());
                                        }
                                    }
                                }
                                ).then((result) => {
                                        if (result.value) {

                                        $('#ContentSection_btnConversionBetweenCustom').click();
                                        }
                                    });
                                });   
                            </script>",
                                false);
                        }
                        else {
                            //Requesting Multiplier

                            string html = "";
                            bool addedFirst = false; //to select the first option in the following loop

                            for (int i = 0; i < addedMeasurements.Count; i++) {
                                if (addedMeasurements[i].IsStandard == 1) {
                                    if (addedFirst) {
                                        html += $"<option value='{addedMeasurements[i].MeasurementId}' selected> {addedMeasurements[i].MeasurementName} </option>";
                                        addedFirst = !addedFirst;
                                    }
                                    else {
                                        html += $"<option value='{addedMeasurements[i].MeasurementId}'> {addedMeasurements[i].MeasurementName} </option>";
                                    }
                                }
                            }

                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                                @"<script>    
                                $(document).ready(function () { 
                                swal.fire({
                                    title: 'Conversion',
                                    html: `<div class='row'>
                                                <div class='col-md-12'>
                                                    <strong id = 'qq' >How many</strong></br>
                                                    <select id = 'pp' class ='form-control'>
                                                    " + html + @"
                                                    </select>
                                                </div>
                                            </div>
                                           <div class='row'>
                                                <div class='col-md-12'>
                                                    <strong id = 'dd'>Are in 1 " + selectedMeasurement.MeasurementName + @"?</strong></br>
                                                    <input id = 'ss' type='text' class ='form-control'></br>
                                                </div>
                                           </div>`,
                                    type: 'warning',
                                    cancelButtonColor: '#d33',
                                    showCancelButton: true,
                                    showConfirmButton: true,
                                    confirmButtonText: 'Okay',
                                    cancelButtonText: 'Cancel',
                                    allowOutsideClick: false,
                                    preConfirm: function() {
                                        if ($('#ss').val() == '' || parseFloat($('#ss').val()) == 0) {
                                            $('#dd').prop('style', 'color:red');
                                            swal.showValidationError('Conversion Multiplier is Required');
                                            return false;
                                        }
                                        else {
                                            $('#ContentSection_hdnMultiplier').val($('#ss').val());
                                            $('#ContentSection_hdnSelectedMeasurement').val($('#pp option:selected').val());
                                        }
                                    }
                                }
                                ).then((result) => {
                                        if (result.value) {

                                        $('#ContentSection_btnAddFirstCustomAfterStandard').click();
                                        }
                                    });
                                });   
                            </script>",
                                false);
                        }
                    }
                    else {
                        if (addedMeasurements.Exists(am => am.IsStandard == 1)) {
                            if (addedMeasurements.Exists(am => am.MeasurementId == selectedMeasurement.DetailId)) {
                                addedMeasurements.Find(am => am.MeasurementId == selectedMeasurement.DetailId).IsActive = 1;
                            }
                            else {
                                addedMeasurements.Add(new AddedMeasurementsVM {
                                    MeasurementId = selectedMeasurement.DetailId,
                                    MeasurementName = selectedMeasurement.MeasurementName,
                                    MasterId = selectedMeasurement.MasterId,
                                    IsBase = 0,
                                    IsActive = 1,
                                    IsStandard = 1,
                                    ConversionToId = addedMeasurements.Find(am => am.IsStandard == 1 && am.IsBase == 1).MeasurementId,
                                    Multiplier = ControllerFactory.CreateConversionTableMasterController().GetConversionToBase(selectedMeasurement.DetailId).Multiplier,
                                    Status = 1
                                });
                            }

                            Session["AddedMeasurements"] = addedMeasurements;
                            BindAddedMeasurements();
                        }
                        else {
                            string html = "";
                            bool addedFirst = false; //to select the first option in the following loop

                            for (int i = 0; i < addedMeasurements.Count; i++) {
                                if (addedMeasurements[i].MasterId == 5) {
                                    if (addedFirst) {
                                        html += $"<option value='{addedMeasurements[i].MeasurementId}' selected> {addedMeasurements[i].MeasurementName} </option>";
                                        addedFirst = !addedFirst;
                                    }
                                    else {
                                        html += $"<option value='{addedMeasurements[i].MeasurementId}'> {addedMeasurements[i].MeasurementName} </option>";
                                    }
                                }
                            }

                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                                @"<script>    
                                $(document).ready(function () { 
                                swal.fire({
                                    title: 'Conversion',
                                    html: `<div class='row'>
                                                <div class='col-md-12'>
                                                    <strong id = 'qq' >How many</strong></br>
                                                    <select id = 'pp' class ='form-control'>
                                                    " + html + @"
                                                    </select>
                                                </div>
                                            </div>
                                           <div class='row'>
                                                <div class='col-md-12'>
                                                    <strong id = 'dd'>Are in 1 " + selectedMeasurement.MeasurementName + @"?</strong></br>
                                                    <input id = 'ss' type='text' class ='form-control'></br>
                                                </div>
                                           </div>`,
                                    type: 'warning',
                                    cancelButtonColor: '#d33',
                                    showCancelButton: true,
                                    showConfirmButton: true,
                                    confirmButtonText: 'Okay',
                                    cancelButtonText: 'Cancel',
                                    allowOutsideClick: false,
                                    preConfirm: function() {
                                        if ($('#ss').val() == '' || parseFloat($('#ss').val()) == 0) {
                                            $('#dd').prop('style', 'color:red');
                                            swal.showValidationError('Conversion Multiplier is Required');
                                            return false;
                                        }
                                        else {
                                            $('#ContentSection_hdnMultiplier').val($('#ss').val());
                                            $('#ContentSection_hdnSelectedMeasurement').val($('#pp option:selected').val());
                                        }
                                    }
                                }
                                ).then((result) => {
                                        if (result.value) {

                                        $('#ContentSection_btnConversionBetweenCustom').click();
                                        }
                                    });
                                });   
                            </script>",
                                false);
                        }
                    }
                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select A Measurement to Add', showConfirmButton: true}); });   </script>", false);
            }
        }

        protected void btnRemoveUOM_Click(object sender, EventArgs e) {
            if (lbAddedUOM.SelectedValue != null && lbAddedUOM.SelectedValue != "") {
                List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;
                AddedMeasurementsVM selectedMeasurement = addedMeasurements.Find(am => am.MeasurementId == int.Parse(lbAddedUOM.SelectedValue));

                ViewState["SelectedMasterId"] = selectedMeasurement.MasterId;

                if (selectedMeasurement.Status == 1) {
                    if (selectedMeasurement.IsStandard == 1) {
                        if (selectedMeasurement.IsBase == 1) {
                            selectedMeasurement.IsActive = 0;
                        }
                        else {
                            addedMeasurements.Remove(selectedMeasurement);
                            if (addedMeasurements.Count(am => am.IsStandard == 1 && am.IsBase == 0) == 0) {
                                var standardBase = addedMeasurements.Find(am => am.IsStandard == 1 && am.IsBase == 1);
                                if (standardBase.Status == 1) {
                                    addedMeasurements.Remove(standardBase);
                                }
                                else {
                                    standardBase.IsActive = 1;
                                }
                            }
                        }
                        if (addedMeasurements.Count > 0) {
                            Session["AddedMeasurements"] = addedMeasurements;
                        }
                        else {
                            Session["AddedMeasurements"] = null;
                        }
                        BindAddedMeasurements();
                    }
                    else {
                        if (selectedMeasurement.IsBase == 1) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                                @"<script>    
                                $(document).ready(function () { 
                                swal.fire({
                                    title: 'Are You Sure?',
                                    text: `Removing Base Measurement of Custom Measurment Type Will Remove All Added Custom Measurements.`,
                                    type: 'warning',
                                    cancelButtonColor: '#d33',
                                    showCancelButton: true,
                                    showConfirmButton: true,
                                    confirmButtonText: 'Yes',
                                    cancelButtonText: 'Cancel',
                                    allowOutsideClick: false
                                }).then((result) => {
                                        if (result.value) {

                                        $('#ContentSection_btnRemoveAllCustomMeasurements').click();
                                        }
                                    });
                                });   
                            </script>",
                                false);
                        }
                        else {
                            addedMeasurements.Remove(selectedMeasurement);
                            if (addedMeasurements.Count > 0) {
                                Session["AddedMeasurements"] = addedMeasurements;
                            }
                            else {
                                Session["AddedMeasurements"] = null;
                            }
                            BindAddedMeasurements();
                        }
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'DENIED',text:'Removing Previously Added Measurement is Not Allowed Since They Might be in Use', showConfirmButton: true}); });   </script>", false);
                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select A Measurement to Remove', showConfirmButton: true}); });   </script>", false);
            }
        }

        

            protected void btnManageStock_Click(object sender, EventArgs e) {
            try {
                if (ViewState["status"].ToString() == "1") {

                    
                    lblstock.Text = "[" + ddlMeasurement.SelectedItem.ToString() + "]";
                    if (btnSave.Text == "Save") {

                        if (ddlStockMaintainingType.SelectedValue.ToString() == "1") {
                            // when intial saving no item id
                            List<WarehouseInventory> warehousesTemp = new List<WarehouseInventory>();
                            warehousesTemp = Session["Warehouses"] as List<WarehouseInventory>;

                            gvWarehouseInventory.DataSource = warehousesTemp;
                            gvWarehouseInventory.DataBind();
                            //populateChangesgvWarehouseInventory(warehousesTemp);
                        }
                        else {
                            List<WarehouseInventory> warehouses;
                            warehouses = ControllerFactory.CreateWarehouseController().getWarehouseListAtAddItems(int.Parse(Session["CompanyId"].ToString()));
                            gvWarehouseInventory.DataSource = warehouses;
                            gvWarehouseInventory.DataBind();
                            populateChangesgvWarehouseInventory(warehouses);
                        }
                    }
                    else {
                        gvWarehouseInventory.DataSource = null;
                        gvWarehouseInventory.DataBind();


                        //if (ddlMeasurement.SelectedValue.ToString() == (string)Session["SeletedMesurementId"].ToString()) {
                            // when already saved item
                            List<WarehouseInventory> warehousesTemp = new List<WarehouseInventory>();
                            warehousesTemp = Session["Warehouses"] as List<WarehouseInventory>;

                            gvWarehouseInventory.DataSource = warehousesTemp;
                            gvWarehouseInventory.DataBind();
                            //populateChangesgvWarehouseInventory(warehousesTemp);
                        //}
                        //else {

                        //    List<WarehouseInventory> warehousesTemp = new List<WarehouseInventory>();
                        //    warehousesTemp = Session["Warehouses"] as List<WarehouseInventory>;

                        //    warehousesTemp = ControllerFactory.CreateWarehouseController().getWarehouseListAtAddItemsForUpdate(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemId"].ToString()));
                        //    List<WarehouseInventory> NewList = ConvertedInvertList(warehousesTemp, Convert.ToInt32(Session["SeletedMesurementId"].ToString()));
                        //    gvWarehouseInventory.DataSource = NewList;

                        //    gvWarehouseInventory.DataBind();
                        //    //populateChangesgvWarehouseInventory(NewList);

                        //}

                    }

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "Popup", "<script>    $(document).ready(function () {  $('#mdlManageStock').modal('show'); });   </script>", false);
                

                }
                else { 
                    List<WarehouseInventory> warehouses;
                    lblstock.Text = "[" + ddlMeasurement.SelectedItem.ToString() + "]";
                    if (btnSave.Text == "Save") {
                        // when intial saving no item id
                        warehouses = ControllerFactory.CreateWarehouseController().getWarehouseListAtAddItems(int.Parse(Session["CompanyId"].ToString()));
                        gvWarehouseInventory.DataSource = warehouses;
                        gvWarehouseInventory.DataBind();
                        populateChangesgvWarehouseInventory(warehouses);
                    }
                    else {
                        gvWarehouseInventory.DataSource = null;
                        gvWarehouseInventory.DataBind();


                        //if (ddlMeasurement.SelectedValue.ToString() == (string)Session["SeletedMesurementId"].ToString()) {
                            // when already saved item
                            warehouses = ControllerFactory.CreateWarehouseController().getWarehouseListAtAddItemsForUpdate(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemId"].ToString()));
                            gvWarehouseInventory.DataSource = warehouses;
                            gvWarehouseInventory.DataBind();
                            populateChangesgvWarehouseInventory(warehouses);
                        //}
                        //else {


                        //    warehouses = ControllerFactory.CreateWarehouseController().getWarehouseListAtAddItemsForUpdate(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemId"].ToString()));
                        //    List<WarehouseInventory> NewList = ConvertedInvertList(warehouses, Convert.ToInt32(Session["SeletedMesurementId"].ToString()));
                        //    gvWarehouseInventory.DataSource = NewList;

                        //    gvWarehouseInventory.DataBind();
                        //    populateChangesgvWarehouseInventory(NewList);

                        //}

                    }

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "Popup", "<script>    $(document).ready(function () {  $('#mdlManageStock').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex) {

                throw ex;
            }

        }


        public List<WarehouseInventory> ConvertedInvertList(List<WarehouseInventory> invertryList, int MesurementID) {
            List<WarehouseInventory> ConvertedList = new List<WarehouseInventory>();
            int ItemStockMaintainingType = ControllerFactory.CreateAddItemController().GetStockMaintaininType(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

            List<WarehouseInventory> warehouses = ControllerFactory.CreateWarehouseController().getWarehouseListAtAddItemsForUpdate(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemId"].ToString()));
            if (ItemStockMaintainingType == 1) {
                for (int i = 0; i < warehouses.Count; i++) {
                    WarehouseInventory newWAehouse = warehouses[i];
                    decimal available = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), warehouses[i].AvailableQty, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));
                    decimal reorderLevel = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), warehouses[i].ReorderLevel, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));

                    newWAehouse.AvailableQty = decimal.Parse(available.ToString("N2"));
                    newWAehouse.ReorderLevel = decimal.Parse(reorderLevel.ToString("N2"));

                    ConvertedList.Add(newWAehouse);
                }

            }
            else {
                List<WarehouseInventoryBatches> warehouseInventoryBatches = ControllerFactory.CreateWarehouseInventoryBatchesController().getAllWarehouseInventoryBatches(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemId"].ToString()));

                for (int i = 0; i < warehouses.Count; i++) {
                    WarehouseInventory newWaehouse = warehouses[i];
                    //decimal reorderLevel = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), warehouses[i].ReorderLevel, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));
                    //decimal available = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), warehouses[i].AvailableQty, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));


                    //newWaehouse.ReorderLevel = decimal.Parse(reorderLevel.ToString("N2"));
                    //newWaehouse.AvailableQty = decimal.Parse(available.ToString("N2"));
                    //ConvertedList.Add(newWaehouse);
                }

                //for (int i = 0; i < warehouseInventoryBatches.Count; i++) {
                //    WarehouseInventoryBatches newBatch = warehouseInventoryBatches[i];
                //    decimal available = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), warehouses[i].ReorderLevel, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));

                //    newBatch.AvailableStock = decimal.Parse(available.ToString("N2"));

                //}
            }

            //foreach (WarehouseInventory objInventry in invertryList) {
            // objInventry.AvailableQty = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), objInventry.AvailableQty, MesurementID, int.Parse(ddlMeasurement.SelectedValue));

            //    objInventry.ReorderLevel = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), objInventry.ReorderLevel, MesurementID, int.Parse(ddlMeasurement.SelectedValue));
            //    ConvertedList.Add(objInventry);
            //}

            return ConvertedList;
        }

       

        private void BindUomTypes(int? SelectedStandard) {
            List<MeasurementMaster> Uoms = ControllerFactory.CreateMeasurementMasterController().GetMeasurementMasters();
            if (SelectedStandard == null) {
                ddlUOMTypes.DataSource = ControllerFactory.CreateMeasurementMasterController().GetMeasurementMasters();
                ddlUOMTypes.DataValueField = "Id";
                ddlUOMTypes.DataTextField = "MeasurementName";
                ddlUOMTypes.DataBind();
            }
            else {
                ddlUOMTypes.Items.Clear();

                for (int i = 0; i < Uoms.Count; i++) {
                    if (Uoms[i].Id == SelectedStandard || Uoms[i].Id == 5) {
                        ddlUOMTypes.Items.Add(new ListItem(Uoms[i].MeasurementName, Uoms[i].Id.ToString()));
                    }
                    else {
                        ddlUOMTypes.Items.Add(new ListItem(Uoms[i].MeasurementName, Uoms[i].Id.ToString(), false));
                    }
                }
            }
        }

        private void BindMeasurementDetails() {
            List<MeasurementDetail> Uoms = ControllerFactory.CreateMeasurementDetailController()
                .GetMeasurementDetailsByMasterId(int.Parse(ddlUOMTypes.SelectedValue), int.Parse(Session["CompanyId"].ToString()));

            if (Session["AddedMeasurements"] == null) {
                lbUOMs.DataSource = Uoms;
                lbUOMs.DataValueField = "DetailId";
                lbUOMs.DataTextField = "MeasurementName";
                lbUOMs.DataBind();
            }
            else {
                List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;

                lbUOMs.Items.Clear();

                for (int i = 0; i < Uoms.Count; i++) {
                    if (!addedMeasurements.Exists(ad => ad.MeasurementId == Uoms[i].DetailId && ad.IsActive == 1)) {
                        lbUOMs.Items.Add(new ListItem(Uoms[i].MeasurementName, Uoms[i].DetailId.ToString()));
                    }
                }
            }

            ViewState["Measurements"] = Uoms;
        }


        private void BindAddedMeasurements() {

            

            if (Session["AddedMeasurements"] != null) {
                //var Measurement = ddlMeasurement.SelectedValue;

                List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;

                if (addedMeasurements.Exists(am => am.IsStandard == 1)) {
                    BindUomTypes(addedMeasurements.Find(am => am.IsStandard == 1 && am.IsBase == 1).MasterId);
                }
                else {
                    BindUomTypes(null);
                }

                ddlUOMTypes.SelectedValue = ViewState["SelectedMasterId"].ToString();


                BindMeasurementDetails();

                lbAddedUOM.DataSource = addedMeasurements.Where(am => am.IsActive == 1);
                lbAddedUOM.DataValueField = "MeasurementId";
                lbAddedUOM.DataTextField = "MeasurementName";
                lbAddedUOM.DataBind();

                ddlMeasurement.DataSource = addedMeasurements.Where(am => am.IsActive == 1);
                ddlMeasurement.DataValueField = "MeasurementId";
                ddlMeasurement.DataTextField = "MeasurementName";
                ddlMeasurement.DataBind();
                //if (Measurement != "") {
                    //ddlMeasurement.SelectedValue = Measurement.ToString();
                //}
            }
            else {
                BindUomTypes(null);
                BindMeasurementDetails();
                lbAddedUOM.Items.Clear();
                //ddlMeasurement.Items.Clear();
            }
        }



        protected void btnConversionBetweenCustom_Click(object sender, EventArgs e) {
            List<MeasurementDetail> Uoms = ViewState["Measurements"] as List<MeasurementDetail>;

            MeasurementDetail selectedMeasurement = Uoms.Find(u => u.DetailId == int.Parse(lbUOMs.SelectedValue));

            List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;

            decimal multiplier = decimal.Parse(hdnMultiplier.Value);

            if (int.Parse(hdnSelectedMeasurement.Value) != addedMeasurements.Find(am => am.MasterId == 5 && am.IsBase == 1).MeasurementId) {
                multiplier = multiplier * addedMeasurements.Find(am => am.MeasurementId == int.Parse(hdnSelectedMeasurement.Value)).Multiplier;
            }

            addedMeasurements.Add(new AddedMeasurementsVM {
                MeasurementId = selectedMeasurement.DetailId,
                MeasurementName = selectedMeasurement.MeasurementName,
                MasterId = 5,
                IsBase = 0,
                IsActive = 1,
                ConversionToId = addedMeasurements.Find(am => am.MasterId == 5 && am.IsBase == 1).MeasurementId,
                Multiplier = multiplier,
                Status = 1
            });

            Session["AddedMeasurements"] = addedMeasurements;
            BindAddedMeasurements();
        }

        protected void btnAddFirstCustomAfterStandard_Click(object sender, EventArgs e) {
            List<MeasurementDetail> Uoms = ViewState["Measurements"] as List<MeasurementDetail>;

            MeasurementDetail selectedMeasurement = Uoms.Find(u => u.DetailId == int.Parse(lbUOMs.SelectedValue));

            List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;

            decimal multiplier = decimal.Parse(hdnMultiplier.Value);

            AddedMeasurementsVM standardBase = addedMeasurements.Find(am => am.IsStandard == 1 && am.IsBase == 1);

            if (int.Parse(hdnSelectedMeasurement.Value) != standardBase.MeasurementId) {
                multiplier = multiplier * addedMeasurements.Find(am => am.MeasurementId == int.Parse(hdnSelectedMeasurement.Value)).Multiplier;
            }

            addedMeasurements.Add(new AddedMeasurementsVM {
                MeasurementId = selectedMeasurement.DetailId,
                MeasurementName = selectedMeasurement.MeasurementName,
                MasterId = 5,
                IsBase = 1,
                IsActive = 1,
                ConversionToId = standardBase.MeasurementId,
                Multiplier = multiplier,
                Status = 1
            });

            standardBase.ConversionToId = selectedMeasurement.DetailId;
            standardBase.Multiplier = 1 / multiplier;

            Session["AddedMeasurements"] = addedMeasurements;
            BindAddedMeasurements();
        }

        protected void btnAddFirstStandardAfterCustom_Click(object sender, EventArgs e) {
            List<MeasurementDetail> Uoms = ViewState["Measurements"] as List<MeasurementDetail>;

            MeasurementDetail selectedMeasurement = Uoms.Find(u => u.DetailId == int.Parse(lbUOMs.SelectedValue));

            List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;

            decimal multiplier = decimal.Parse(hdnMultiplier.Value);

            AddedMeasurementsVM customBase = addedMeasurements.Find(am => am.MasterId == 5 && am.IsBase == 1);

            if (int.Parse(hdnSelectedMeasurement.Value) != customBase.MeasurementId) {
                multiplier = multiplier * addedMeasurements.Find(am => am.MeasurementId == int.Parse(hdnSelectedMeasurement.Value)).Multiplier;
            }

            if (selectedMeasurement.IsBase == 1) {
                addedMeasurements.Add(new AddedMeasurementsVM {
                    MeasurementId = selectedMeasurement.DetailId,
                    MeasurementName = selectedMeasurement.MeasurementName,
                    MasterId = selectedMeasurement.MasterId,
                    IsBase = 1,
                    IsActive = 1,
                    IsStandard = 1,
                    ConversionToId = customBase.MeasurementId,
                    Multiplier = multiplier,
                    Status = 1
                });

                customBase.ConversionToId = selectedMeasurement.DetailId;
                customBase.Multiplier = 1 / multiplier;
            }
            else {
                MeasurementDetail baseMeasurement = Uoms.Find(u => u.MasterId == int.Parse(ddlUOMTypes.SelectedValue) && u.IsBase == 1);

                var addedMeasurement = new AddedMeasurementsVM {
                    MeasurementId = selectedMeasurement.DetailId,
                    MeasurementName = selectedMeasurement.MeasurementName,
                    MasterId = selectedMeasurement.MasterId,
                    IsBase = 0,
                    IsActive = 1,
                    IsStandard = 1,
                    ConversionToId = baseMeasurement.DetailId,
                    Multiplier = ControllerFactory.CreateConversionTableMasterController().GetConversionToBase(selectedMeasurement.DetailId).Multiplier,
                    Status = 1
                };

                var standardBase = new AddedMeasurementsVM {
                    MeasurementId = baseMeasurement.DetailId,
                    MeasurementName = baseMeasurement.MeasurementName,
                    MasterId = baseMeasurement.MasterId,
                    IsBase = 1,
                    IsActive = 0,
                    IsStandard = 1,
                    ConversionToId = customBase.MeasurementId,
                    Multiplier = multiplier * addedMeasurement.Multiplier,
                    Status = 1
                };

                customBase.ConversionToId = standardBase.MeasurementId;
                customBase.Multiplier = 1 / standardBase.Multiplier;

                addedMeasurements.Add(standardBase);
                addedMeasurements.Add(addedMeasurement);
            }

            Session["AddedMeasurements"] = addedMeasurements;
            BindAddedMeasurements();
        }

        protected void btnRemoveAllCustomMeasurements_Click(object sender, EventArgs e) {

            List<AddedMeasurementsVM> addedMeasurements = Session["AddedMeasurements"] as List<AddedMeasurementsVM>;

            if (addedMeasurements.Count(am => am.MasterId == 5 && am.Status == 0) == 0) {
                addedMeasurements.RemoveAll(am => am.MasterId == 5);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'DENIED',text:'Removing All Custom Measurements is Not Allowed Since They Might be in Use', showConfirmButton: true}); });   </script>", false);
            }

            if (addedMeasurements.Count > 0) {
                Session["AddedMeasurements"] = addedMeasurements;
            }
            else {
                Session["AddedMeasurements"] = null;
            }
            BindAddedMeasurements();
        }

        private void populateChangesgvWarehouseInventory(List<WarehouseInventory> warehouses) {
            if (ddlStockMaintainingType.SelectedValue != "1") {
                // when other than average
                List<WarehouseInventory> warehousesTemp = new List<WarehouseInventory>();
                if (Session["Warehouses"] != null) {
                    //warehousesTemp = Session["Warehouses"] as List<WarehouseInventory>;
                    warehousesTemp = warehouses;
                    gvWarehouseInventory.DataSource = warehousesTemp;
                    gvWarehouseInventory.DataBind();
                }
                //else
                //{
                //    gvWarehouseInventory.DataSource = warehouses;
                //    gvWarehouseInventory.DataBind();
                //}
                for (int t = 0; t < gvWarehouseInventory.Rows.Count; ++t) {
                    for (int r = 0; r < gvWarehouseInventory.Rows[t].Cells.Count; ++r) {
                        Button btnAddBatch = gvWarehouseInventory.Rows[t].Cells[r].FindControl("btnAddBatch") as Button;
                        btnAddBatch.Visible = true;
                        TextBox txtStock = gvWarehouseInventory.Rows[t].Cells[r].FindControl("txtStock") as TextBox;
                        txtStock.Visible = false;
                        Label lblStock = gvWarehouseInventory.Rows[t].Cells[r].FindControl("lblStock") as Label;
                        lblStock.Visible = true;
                        TextBox txtStockValue = gvWarehouseInventory.Rows[t].Cells[r].FindControl("txtStockValue") as TextBox;
                        txtStockValue.Visible = false;
                        Label lblStockValue = gvWarehouseInventory.Rows[t].Cells[r].FindControl("lblStockValue") as Label;
                        lblStockValue.Visible = true;

                        List<WarehouseInventoryBatches> list = new List<WarehouseInventoryBatches>();
                        if (Session["ItemBatches"] != null) {
                            //if (ddlMeasurement.SelectedValue.ToString() == (string)Session["SeletedMesurementId"].ToString()) {
                            //    list = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(Session["ItemBatches"].ToString());
                            //}
                            //else {
                                list = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(Session["ItemBatches"].ToString());
                                for (int i = 0; i < list.Count; i++) {
                                    WarehouseInventoryBatches newBatch = list[i];
                                    if (newBatch.AvailableStock > 0) {
                                       // decimal available = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), newBatch.AvailableStock, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));

                                        //newBatch.AvailableStock = decimal.Parse(available.ToString("N2"));
                                        
                                    }
                                }
                            //}
                        }
                        lblStock.Text = "0.00";
                        lblStockValue.Text = "0.00";
                        if (list.Count() > 0) {
                            int warehouseId = Convert.ToInt32(gvWarehouseInventory.Rows[t].Cells[0].Text);
                            lblStock.Text = list.Where(g => g.WarehouseID == warehouseId).Count() > 0 ? list.Where(g => g.WarehouseID == warehouseId).Sum(g => g.AvailableStock).ToString("0.00") : "0.00";
                            lblStockValue.Text = list.Where(g => g.WarehouseID == warehouseId).Count() > 0 ? list.Where(g => g.WarehouseID == warehouseId).Sum(g => g.StockValue).ToString("0.00") : "0.00";
                        }
                    }
                }
            }
            else {

                if (Session["Warehouses"] != null) {
                    List<WarehouseInventory> warehousesTemp = new List<WarehouseInventory>();
                    warehousesTemp = Session["Warehouses"] as List<WarehouseInventory>;
                    if (Session["SeletedMesurementId"] != null) {
                        warehousesTemp = ConvertedInvertList(warehousesTemp, Convert.ToInt32(Session["SeletedMesurementId"].ToString()));
                    }
                    for (int t = 0; t < gvWarehouseInventory.Rows.Count; ++t) {
                        for (int r = 0; r < gvWarehouseInventory.Rows[t].Cells.Count; ++r) {
                            if (warehousesTemp.Count() > 0) {
                                int warehouseId = Convert.ToInt32(gvWarehouseInventory.Rows[t].Cells[0].Text);
                                TextBox txtStock = gvWarehouseInventory.Rows[t].Cells[r].FindControl("txtStock") as TextBox;
                                TextBox txtStockValue = gvWarehouseInventory.Rows[t].Cells[r].FindControl("txtStockValue") as TextBox;
                                TextBox txtReorderLevel = gvWarehouseInventory.Rows[t].Cells[r].FindControl("txtReorderLevel") as TextBox;
                                txtStock.Text = "0";
                                txtStockValue.Text = "0";
                                txtReorderLevel.Text = "0";
                                if (warehousesTemp.Where(g => g.WarehouseID == warehouseId).Count() > 0) {
                                    txtStock.Text = warehousesTemp.Where(g => g.WarehouseID == warehouseId).First().AvailableQty.ToString("0.00");
                                    txtStockValue.Text = warehousesTemp.Where(g => g.WarehouseID == warehouseId).First().StockValue.ToString("0.00");
                                    txtReorderLevel.Text = warehousesTemp.Where(g => g.WarehouseID == warehouseId).First().ReorderLevel.ToString("0.00");
                                }
                            }
                        }
                    }
                }
            }
        }

        protected void btnAddBatch_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                int warehouseId = int.Parse(gvWarehouseInventory.Rows[x].Cells[0].Text);
                int itemId = int.Parse(gvWarehouseInventory.Rows[x].Cells[1].Text);   // this will be zero if new item
                WarehouseInventoryBatchesController cont = ControllerFactory.CreateWarehouseInventoryBatchesController();
                if (Session["ItemBatches"] != null) {
                    List<WarehouseInventoryBatches> list = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(Session["ItemBatches"].ToString());

                    //if (ddlMeasurement.SelectedValue.ToString() == (string)Session["SeletedMesurementId"].ToString()) {
                        list = list.Where(t => t.WarehouseID == warehouseId && t.IsActive == 1).ToList();
                        ViewState["BatchAvailability"] = new JavaScriptSerializer().Serialize(list);

                        for (int i = 0; i < list.Count; i++) {
                        if (list[i].ExpiryDate != DateTime.MinValue) {
                            list[i].ExpiryDate = list[i].ExpiryDate.AddMinutes(330);
                        }
                        }

                        gvItemBatch.DataSource = list;
                        gvItemBatch.DataBind();
                    //}
                    //else {
                    //    List<WarehouseInventory> warehouses = ControllerFactory.CreateWarehouseController().getWarehouseListAtAddItemsForUpdate(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemId"].ToString()));
                    //    //List<WarehouseInventoryBatches> warehouseInventoryBatches = ControllerFactory.CreateWarehouseInventoryBatchesController().getAllWarehouseInventoryBatches(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemId"].ToString()));

                    //    //for (int i = 0; i < warehouseInventoryBatches.Count; i++) {
                    //    //    WarehouseInventoryBatches newBatch = warehouseInventoryBatches[i];
                    //    //    decimal available = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), warehouses[i].ReorderLevel, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));

                    //    //    newBatch.AvailableStock = decimal.Parse(available.ToString("N2"));

                    //    //}
                    //    list = list.Where(t => t.WarehouseID == warehouseId && t.IsActive == 1).ToList();
                    //    for (int i = 0; i < list.Count; i++) {
                    //        WarehouseInventoryBatches newBatch = list[i];
                    //        if (newBatch.AvailableStock > 0) {
                    //            decimal available = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), newBatch.AvailableStock, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));
                            
                    //        newBatch.AvailableStock = decimal.Parse(available.ToString("N2"));
                    //        //list.Add(newBatch);
                    //        }
                    //    }
                    //    ViewState["BatchAvailability"] = new JavaScriptSerializer().Serialize(list);
                    //    gvItemBatch.DataSource = list;
                    //    gvItemBatch.DataBind();
                    //}
                }
                else {
                    List<WarehouseInventoryBatches> batches = cont.getWarehouseInventoryBatchesListByWarehouseId(warehouseId, itemId, int.Parse(Session["CompanyId"].ToString()));
                    Session["ItemBatches"] = new JavaScriptSerializer().Serialize(batches);
                    gvItemBatch.DataSource = batches;
                    gvItemBatch.DataBind();
                }
                ViewState["warehouseId"] = warehouseId;
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "Popup", "<script>    $(document).ready(function () {  $('.modal-backdrop').remove();  $('#mdlAddBatch').modal('show'); });   </script>", false);


            }
            catch (Exception ex) {

                throw ex;
            }
        }

        protected void btnDone_Click(object sender, EventArgs e) {
            try {
                
                btnSave.Enabled = true;
                Session["Warehouses"] = null;
                
                ViewState["status"] = 1;

                if (gvWarehouseInventory.Rows.Count > 0) {
                    List<WarehouseInventory> warehousesTemp = new List<WarehouseInventory>();
                    for (int i = 0; i < gvWarehouseInventory.Rows.Count; i++) {
                        if (ddlStockMaintainingType.SelectedValue == "1") {
                            if ((gvWarehouseInventory.Rows[i].Cells[2].FindControl("txtStock") as TextBox).Text != "" && decimal.Parse((gvWarehouseInventory.Rows[i].Cells[2].FindControl("txtStock") as TextBox).Text) > 0) {
                                WarehouseInventory wi = new WarehouseInventory();
                                wi.WarehouseID = int.Parse(gvWarehouseInventory.Rows[i].Cells[0].Text.ToString());
                                wi.Location = (gvWarehouseInventory.Rows[i].Cells[2].FindControl("lblLocation") as Label).Text;
                                wi.AvailableQty = decimal.Parse((gvWarehouseInventory.Rows[i].Cells[2].FindControl("txtStock") as TextBox).Text == "" ? "0" : (gvWarehouseInventory.Rows[i].Cells[2].FindControl("txtStock") as TextBox).Text);
                                wi.StockValue = decimal.Parse((gvWarehouseInventory.Rows[i].Cells[3].FindControl("txtStockValue") as TextBox).Text == "" ? "0" : (gvWarehouseInventory.Rows[i].Cells[3].FindControl("txtStockValue") as TextBox).Text);
                                wi.ReorderLevel = decimal.Parse((gvWarehouseInventory.Rows[i].Cells[4].FindControl("txtReorderLevel") as TextBox).Text == "" ? "0" : (gvWarehouseInventory.Rows[i].Cells[4].FindControl("txtReorderLevel") as TextBox).Text);
                                warehousesTemp.Add(wi);

                            }
                        }
                        else {
                            //if ((gvWarehouseInventory.Rows[i].Cells[2].FindControl("lblStock") as Label).Text != "" && decimal.Parse((gvWarehouseInventory.Rows[i].Cells[2].FindControl("lblStock") as Label).Text) > 0) {
                            WarehouseInventory wi = new WarehouseInventory();
                            wi.WarehouseID = int.Parse(gvWarehouseInventory.Rows[i].Cells[0].Text.ToString());
                            wi.Location = (gvWarehouseInventory.Rows[i].Cells[2].FindControl("lblLocation") as Label).Text;
                            wi.AvailableQty = decimal.Parse((gvWarehouseInventory.Rows[i].Cells[2].FindControl("lblStock") as Label).Text == "" ? "0" : (gvWarehouseInventory.Rows[i].Cells[2].FindControl("lblStock") as Label).Text);
                            wi.StockValue = decimal.Parse((gvWarehouseInventory.Rows[i].Cells[3].FindControl("lblStockValue") as Label).Text == "" ? "0" : (gvWarehouseInventory.Rows[i].Cells[3].FindControl("lblStockValue") as Label).Text);
                            wi.ReorderLevel = decimal.Parse((gvWarehouseInventory.Rows[i].Cells[4].FindControl("txtReorderLevel") as TextBox).Text == "" ? "0" : (gvWarehouseInventory.Rows[i].Cells[4].FindControl("txtReorderLevel") as TextBox).Text);
                            warehousesTemp.Add(wi);
                            // }
                        }
                    }

                    Session["Warehouses"] = warehousesTemp;
                }
                //else
                //{
                //    Session["Warehouses"] = null;
                //}

                AddImage((string)Session["imgFilename"]);
            }
            catch (Exception ex) {

                throw ex;
            }
        }

        protected void btnAddBatchDone_Click(object sender, EventArgs e) {
            List<WarehouseInventoryBatches> list = new List<WarehouseInventoryBatches>();
            bool flag = false;
            if (Session["ItemBatches"] != null) {
                list = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(Session["ItemBatches"].ToString());
                for (int i = 0; i < list.Count; i++) {
                    WarehouseInventoryBatches newBatch = list[i];
                    if (newBatch.AvailableStock > 0) {
                        if (Session["itemId"] != null) {
                            decimal available = iConversionController.DoConversion(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()), newBatch.AvailableStock, Convert.ToInt32(Session["SeletedMesurementId"].ToString()), int.Parse(ddlMeasurement.SelectedValue));

                            newBatch.AvailableStock = decimal.Parse(available.ToString("N2"));
                        }
                    }
                }
            }

           
                for (int t = 0; t < gvWarehouseInventory.Rows.Count; ++t) {

                    int warehouseId = Convert.ToInt32(gvWarehouseInventory.Rows[t].Cells[0].Text);
                    Label lblStock = gvWarehouseInventory.Rows[t].FindControl("lblStock") as Label;
                    Label lblStockValue = gvWarehouseInventory.Rows[t].FindControl("lblStockValue") as Label;
                    if (list.Count > 0) {
                        if (list.Where(g => g.WarehouseID == warehouseId).Count() > 0) {
                            lblStock.Text = list.Where(g => g.WarehouseID == warehouseId).Sum(g => g.AvailableStock).ToString("0.00");
                            lblStockValue.Text = list.Where(g => g.WarehouseID == warehouseId).Sum(g => g.StockValue).ToString("0.00");
                        }
                        else {
                            lblStock.Text = "0.00";
                            lblStockValue.Text = "0.00";
                        }
                    }
                    else {
                        lblStock.Text = "0.00";
                        lblStockValue.Text = "0.00";
                    }
                }
           
                
            


            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "Popup", "<script>    $(document).ready(function () {  $('.modal-backdrop').remove(); $('#mdlAddBatch').modal('hide');  $('#mdlManageStock').modal('show'); });   </script>", false); ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "Popup", "<script>    $(document).ready(function () {  $('.modal-backdrop').remove();  $('#mdlAddBatch').modal('show'); });   </script>", false);
        }

        protected void btnAddBatch_Click1(object sender, EventArgs e) {
            btnSave.Enabled = false;
            if (Session["ItemBatches"] != null) {
                List<WarehouseInventoryBatches> list = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(Session["ItemBatches"].ToString());
                int warehouseId = Convert.ToInt32(ViewState["warehouseId"].ToString());
                if (txtStock.Text != "" && txtStockValue.Text != "") {
                    if (list.Where(t => t.AvailableStock == Convert.ToDecimal(txtStock.Text) && t.StockValue == Convert.ToDecimal(txtStockValue.Text) && t.WarehouseID == warehouseId).Count() == 0) {
                        WarehouseInventoryBatches obj = new WarehouseInventoryBatches {
                            WarehouseID = Convert.ToInt32(ViewState["warehouseId"].ToString()),
                            ItemID = 0,
                            AvailableStock = txtStock.Text == "" ? 0 : Convert.ToDecimal(txtStock.Text),
                            StockValue = txtStockValue.Text == "" ? 0 : Convert.ToDecimal(txtStockValue.Text),
                            CompanyId = int.Parse(Session["CompanyId"].ToString()),
                            LastUpdatedBy = int.Parse(Session["UserId"].ToString()),
                            ExpiryDate = txtexpdate.Text == "" ? DateTime.MinValue : DateTime.Parse(txtexpdate.Text),
                            IsActive = 1,
                            addStatus = 1
                        };
                        list.Add(obj);

                        if (ViewState["addedBatches"] == null) {
                            List<WarehouseInventoryBatches> addedBatches = new List<WarehouseInventoryBatches>();
                            addedBatches.Add(obj);
                            ViewState["addedBatches"] = new JavaScriptSerializer().Serialize(addedBatches);
                        }
                        else {
                            List<WarehouseInventoryBatches> addedBatches = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(ViewState["addedBatches"].ToString());

                            addedBatches.Add(obj);
                            ViewState["addedBatches"] = new JavaScriptSerializer().Serialize(addedBatches);
                        }

                        Session["ItemBatches"] = new JavaScriptSerializer().Serialize(list);
                        gvItemBatch.DataSource = list.Where(t => t.WarehouseID == warehouseId && t.IsActive == 1).ToList(); ;
                        gvItemBatch.DataBind();
                    }
                }
            }
            else {
                List<WarehouseInventoryBatches> list = new List<WarehouseInventoryBatches>();
                WarehouseInventoryBatches obj = new WarehouseInventoryBatches {
                    WarehouseID = Convert.ToInt32(ViewState["warehouseId"].ToString()),
                    ItemID = 0,
                    AvailableStock = txtStock.Text == "" ? 0 : Convert.ToDecimal(txtStock.Text),
                    StockValue = txtStockValue.Text == "" ? 0 : Convert.ToDecimal(txtStockValue.Text)
                };
                list.Add(obj);
                Session["ItemBatches"] = new JavaScriptSerializer().Serialize(list);
                gvItemBatch.DataSource = list;
                gvItemBatch.DataBind();
            }
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "Popup", "<script>    $(document).ready(function () {  $('.modal-backdrop').remove();  $('#mdlAddBatch').modal('show'); });   </script>", false);

            txtStock.Text = "";
            txtStockValue.Text = "";
            txtexpdate.Text = "";
        }

        protected void btnDeleteBatch_Click(object sender, EventArgs e) {
            btnSave.Enabled = false;
            int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
            if (Session["ItemBatches"] != null) {
                int warehouseId = Convert.ToInt32(ViewState["warehouseId"].ToString());
                List<WarehouseInventoryBatches> list = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(Session["ItemBatches"].ToString());
                // list = list.Where(t => t.WarehouseID == warehouseId).ToList();
                Label lblStock = gvItemBatch.Rows[x].Cells[2].FindControl("lblStock") as Label;
                decimal stock = Convert.ToDecimal(lblStock.Text);
                Label lblStockValue = gvItemBatch.Rows[x].Cells[3].FindControl("lblStockValue") as Label;
                decimal stockValue = Convert.ToDecimal(lblStockValue.Text);
                WarehouseInventoryBatches obj = new WarehouseInventoryBatches();
                if (list.Where(t => t.AvailableStock == stock && t.StockValue == stockValue && t.WarehouseID == warehouseId).Count() != 0) {
                    obj = list.Where(t => t.AvailableStock == stock && t.StockValue == stockValue && t.WarehouseID == warehouseId).First();
                }

                list.Remove(obj);
                if (ViewState["addedBatches"] != null) {
                    List<WarehouseInventoryBatches> addedBatches = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(ViewState["addedBatches"].ToString());
                    addedBatches = list;
                    ViewState["addedBatches"] = new JavaScriptSerializer().Serialize(addedBatches);
                }


                if (ViewState["deletedBatches"] == null) {
                    List<WarehouseInventoryBatches> deletedBatches = new List<WarehouseInventoryBatches>();
                    deletedBatches.Add(obj);
                    ViewState["deletedBatches"] = new JavaScriptSerializer().Serialize(deletedBatches);
                }
                else {
                    List<WarehouseInventoryBatches> deletedBatches = new JavaScriptSerializer().Deserialize<List<WarehouseInventoryBatches>>(ViewState["deletedBatches"].ToString());

                    deletedBatches.Add(obj);
                    ViewState["deletedBatches"] = new JavaScriptSerializer().Serialize(deletedBatches);
                }


                if (obj.BatchchId != 0) {
                    //warehouseInventoryBatchesController.deleteWarehouseInventoryBatches(warehouseId, obj.ItemID, obj.BatchchId, obj.CompanyId, obj.BatchchId);
                }



                Session["ItemBatches"] = new JavaScriptSerializer().Serialize(list);
                gvItemBatch.DataSource = null;
                gvItemBatch.DataBind();
                if (list.Where(g => g.WarehouseID == warehouseId).Count() > 0) {
                    gvItemBatch.DataSource = list.Where(g => g.WarehouseID == warehouseId).ToList();
                    gvItemBatch.DataBind();
                }
            }
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "Popup", "<script>   $(document).ready(function () {    $('.modal-backdrop').remove();  $('#mdlAddBatch').modal('show');  });  </script>", false);
        }


        private void bindMeasurement() {
            try {
                ddlMeasurement.DataSource = unitMeasurementController.fetchMeasurementsByCompanyID(int.Parse(ViewState["CompanyId"].ToString()));
                ddlMeasurement.DataValueField = "measurentId";
                ddlMeasurement.DataTextField = "measurementShortName";
                ddlMeasurement.DataBind();
                ddlMeasurement.Items.Insert(0, new ListItem("Select measurement type", ""));
            }
            catch (Exception) {

                throw;
            }
        }

        private void LoadDDLMainCatregoryForSort() {
            try {

                ddlSortMainCateory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(ViewState["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                ddlSortMainCateory.DataValueField = "CategoryId";
                ddlSortMainCateory.DataTextField = "CategoryName";
                ddlSortMainCateory.DataBind();
                ddlSortMainCateory.Items.Insert(0, new ListItem("All", "0"));
            }
            catch (Exception ex) {
            }
        }

        protected void ddlSortMainCateory_SelectedIndexChanged(object sender, EventArgs e) {

            try {
                if (!string.IsNullOrEmpty(ddlSortMainCateory.SelectedValue.ToString())) {
                    int mainCategoryId = int.Parse(ddlSortMainCateory.SelectedValue);
                    ddlSortSubCateory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(mainCategoryId, int.Parse(ViewState["CompanyId"].ToString()));
                    ddlSortSubCateory.DataTextField = "SubCategoryName";
                    ddlSortSubCateory.DataValueField = "SubCategoryId";
                    ddlSortSubCateory.DataBind();
                    ddlSortSubCateory.Items.Insert(0, new ListItem("All", "0"));
                }

            }
            catch (Exception ex) {

            }
        }

        protected void btnSort_Click(object sender, EventArgs e) {
            try {

                int mainCaId = int.Parse(ddlSortMainCateory.SelectedValue);
                int subCatId = int.Parse(ddlSortSubCateory.SelectedValue);


                gvAddItems.DataSource = addItemController.FetchItemListDetailedFilter(int.Parse(ViewState["CompanyId"].ToString()), mainCaId, subCatId).OrderBy(x => x.ItemName);
                gvAddItems.DataBind();

                ViewState["PageIndexNo"] = 1;
                txtSearchItemCode.Text = string.Empty;
                txtSearchItemName.Text = string.Empty;

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void btnSearchByNameOrCode_Click(object sender, EventArgs e) {
            try {
                var code = rbtnItemCode.Checked == true ? 1 : 2;
                var name = rbtnItemName.Checked == true ? 1 : 2;

                if (name == 1) {

                    ViewState["ItemName"] = txtSearchItemName.Text;
                    gvAddItems.DataSource = addItemController.FetchItemByItemName(int.Parse(ViewState["CompanyId"].ToString()), txtSearchItemName.Text).OrderBy(x => x.ItemName);
                    gvAddItems.DataBind();

                    ViewState["PageIndexNo"] = 2;
                    txtSearchItemCode.Text = string.Empty;
                    LoadDDLMainCatregoryForSort();
                    ddlSortMainCateory_SelectedIndexChanged(null, null);
                }

                if (code == 1) {

                    ViewState["ItemCode"] = txtSearchItemCode.Text;
                    gvAddItems.DataSource = addItemController.FetchItemByItemCode(int.Parse(ViewState["CompanyId"].ToString()), txtSearchItemCode.Text).OrderBy(x => x.ItemName);
                    gvAddItems.DataBind();


                    ViewState["PageIndexNo"] = 3;
                    txtSearchItemName.Text = string.Empty;
                    LoadDDLMainCatregoryForSort();
                    ddlSortMainCateory_SelectedIndexChanged(null, null);
                }

            }
            catch (Exception ex) {

            }
        }

        protected void btnEdit_Click1(object sender, ImageClickEventArgs e) {

            Session["editClick"] = null;

            try {
                LoadDDLMainCatregory();
                pnlSpec.Visible = false;

                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                int companyId = int.Parse(ViewState["CompanyId"].ToString());
                Session["itemId"] = gvAddItems.Rows[x].Cells[3].Text.ToString();
                hndIsEdit.Value = "1";
                Session["Warehouses"] = null;
                Session["ItemBatches"] = null;
                Session["SeletedMesurementId"] = null;
                var ItemId = int.Parse(gvAddItems.Rows[x].Cells[3].Text.ToString());
                AddItem addItem = addItemController.FetchItemByItemId(ItemId, int.Parse(ViewState["CompanyId"].ToString()));

                var categoryId = addItem.CategoryId;
                var itemName = addItem.ItemName;
                var subCategoryName = addItem.SubCategoryName;
                var categoryName = addItem.CategoryName;
                var subCategoryId = addItem.SubCategoryId;
                var refNo = addItem.ReferenceNo;
                var isActive = addItem.IsActive;
                var craetedDate = addItem.CreatedDateTime;
                var createdy = addItem.CreatedBy;
                var updataedDate = addItem.UpdatedDateTime;
                var updatedBy = addItem.UpdatedBy;
                var reorderLevel = addItem.ReorderLevel;
                var measurementId = addItem.MeasurementId;
                var orderCode = addItem.OrderCode;
                var hsCode = addItem.HsId;



                ddlMainCategory.SelectedValue = categoryId.ToString();
                ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(int.Parse(ddlMainCategory.SelectedValue), int.Parse(Session["CompanyId"].ToString()));
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
                txtReferenceNo.Text = refNo;
                //txtreorder.Text = reorderLevel.ToString();
                ddlSubCategory.SelectedValue = subCategoryId.ToString();
                ddlItemType.SelectedValue = addItem.ItemType.ToString();
                txtItemName.Text = itemName;
                txtModel.Text = addItem.Model;
                txtPartId.Text = addItem.PartId;
                List<DutyRates> DutyRateList = ControllerFactory.CreateDutyRatesController().GetRates();
                ddlHsCode.DataSource = DutyRateList;
                for (int i = 0; i < DutyRateList.Count; i++) {
                    DutyRateList[i].HsIdName = DutyRateList[i].HsIdName + " - " + DutyRateList[i].HsId;
                }
                ddlHsCode.DataValueField = "HsId";
                ddlHsCode.DataTextField = "HsIdName";
                ddlHsCode.DataBind();
                ddlHsCode.SelectedValue = hsCode;

                if (addItem.HsId == null || addItem.HsId == "" || addItem.HsId == "0") {
                    ddlHsCode.SelectedIndex = 0;
                }
                else {
                    ddlHsCode.SelectedValue = addItem.HsId.ToString();
                }

                if (addItem.ItemImagePath == "" || addItem.ItemImagePath == "&nbsp;") {
                    imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();
                }
                else {
                    imageid.Src = addItem.ItemImagePath + "?" + LocalTime.Now.Ticks.ToString();
                }

                btnSave.Text = "Update";

                if (categoryName == "Undefined" && subCategoryName == "Undefined") {
                    ddlMainCategory.Enabled = true;
                    ddlSubCategory.Enabled = true;
                }
                else {

                    ddlMainCategory.Enabled = false;
                    ddlSubCategory.Enabled = false;
                }
                ddlStockMaintainingType.Enabled = false;
                ddlMeasurement.Enabled = false;
                List<AddedMeasurementsVM> addedMeasurements = new List<AddedMeasurementsVM>();
                List<ItemMeasurement> itemMeasurements = ControllerFactory.CreateItemMeasurementController().GetItemMeasurements(ItemId, int.Parse(Session["CompanyId"].ToString()));
                List<Conversion> conversions = ControllerFactory.CreateConversionController().GetItemConversions(ItemId, int.Parse(Session["CompanyId"].ToString()));


                for (int i = 0; i < itemMeasurements.Count; i++) {
                    AddedMeasurementsVM am = new AddedMeasurementsVM();

                    am.MeasurementId = itemMeasurements[i].MeasurementDetailId;
                    am.MeasurementName = itemMeasurements[i].MeasurementName;
                    am.MasterId = itemMeasurements[i].MasterId;
                    am.IsActive = itemMeasurements[i].IsActive;
                    am.IsBase = itemMeasurements[i].IsBase;
                    am.IsStandard = itemMeasurements[i].MasterId != 5 ? 1 : 0;

                    var conversion = conversions.Find(c => c.FromId == itemMeasurements[i].MeasurementDetailId);

                    if (conversion != null) {
                        am.ConversionToId = conversion.ToId;
                        am.Multiplier = conversion.Multiplier;
                    }

                    addedMeasurements.Add(am);
                }

                ViewState["SelectedMasterId"] = addedMeasurements[0].MasterId;
                Session["AddedMeasurements"] = addedMeasurements;
                BindAddedMeasurements();


                ddlMeasurement.SelectedValue = measurementId.ToString();

                Session["SeletedMesurementId"] = measurementId.ToString();

                ddlStockMaintainingType.SelectedValue = addItem.StockMaintainingType.ToString();
                List<WarehouseInventoryBatches> batches = ControllerFactory.CreateWarehouseInventoryBatchesController().getWarehouseInventoryBatches(ItemId, companyId);
                if (batches.Count == 0) {
                    List<WarehouseInventory> warehouseInventory = ControllerFactory.CreateWarehouseController().getWarehouseListAtAddItemsForUpdate(companyId, ItemId);
                    if (warehouseInventory.Count > 0) {
                        WarehouseInventoryBatches item = null;
                        for (int t = 0; t < warehouseInventory.FindAll(y => y.AvailableQty != 0 && y.StockValue != 0).Count; ++t) {
                            item = new WarehouseInventoryBatches();
                            item.CompanyId = companyId;
                            item.WarehouseID = warehouseInventory[t].WarehouseID;
                            item.ItemID = ItemId;
                            item.AvailableStock = warehouseInventory[t].AvailableQty;
                            item.StockValue = warehouseInventory[t].StockValue;
                            item.HoldedQty = warehouseInventory[t].HoldedQty;
                            batches.Add(item);
                        }
                    }
                }
                Session["ItemBatches"] = new JavaScriptSerializer().Serialize(batches);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { document.body.scrollTop = 50; document.documentElement.scrollTop = 50;});   </script>", false);

            }

            catch (Exception ex) {

            }

            Session["editClick"] = "Clicked";
        }

        public void AddImage(string ImagePath) {

            if (ImagePath == "" || ImagePath == "&nbsp;") {
                imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();
            }
            else {
                imageid.Src = ImagePath;
            }
        }


        private void LoadDDLMainCatregory() {
            try {
                ddlMainCategory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(ViewState["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                ddlMainCategory.DataValueField = "CategoryId";
                ddlMainCategory.DataTextField = "CategoryName";
                ddlMainCategory.DataBind();
                ddlMainCategory.Items.Insert(0, new ListItem("Select Main Category", ""));
            }
            catch (Exception ex) {
            }
        }

        protected void btnViewLog_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvAddItems.Rows[x].Cells[3].Text);


                gvStockLog.DataSource = stockOverrideLogController.GetStockLog(itemId);
                gvStockLog.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlLog').modal('show'); });   </script>", false);


            }
            catch (Exception ex) {

                throw ex;
            }
        }

        private void LoadMasterDDLMainCatregory() {
            try {
                ddlItemMasterCategory.DataSource = itemCategoryMasterController.FetchItemCategoryfORSubCategoryCreationList(int.Parse(ViewState["CompanyId"].ToString()));
                ddlItemMasterCategory.DataValueField = "CategoryId";
                ddlItemMasterCategory.DataTextField = "CategoryName";
                ddlItemMasterCategory.DataBind();
                ddlItemMasterCategory.Items.Insert(0, new ListItem("Select Master Main Category", ""));
            }
            catch (Exception ex) {

            }
        }

        protected void btnTake_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int categoryId = int.Parse(gvMasterItemList.Rows[x].Cells[0].Text.ToString());
                int subcategoryId = int.Parse(gvMasterItemList.Rows[x].Cells[1].Text.ToString());
                int itemId = int.Parse(gvMasterItemList.Rows[x].Cells[2].Text.ToString());

                AddItemMaster addItemMaster = addItemMasterController.FetchItemListByIdObj(itemId);
                if (addItemMaster.ItemId > 0) {
                    int saveItemStatus = addItemController.SaveItems(itemId, addItemMaster.CategoryId, addItemMaster.SubCategoryId, addItemMaster.ItemName, addItemMaster.IsActive, LocalTime.Now, ViewState["UserId"].ToString(), LocalTime.Now, ViewState["UserId"].ToString(), addItemMaster.ReferenceNo, int.Parse(ViewState["CompanyId"].ToString()), ddlHsCode.SelectedValue, txtModel.Text, txtPartId.Text, int.Parse(ddlMeasurement.SelectedValue), int.Parse(ddlItemType.SelectedValue), int.Parse(ddlStockMaintainingType.SelectedValue));
                    if (saveItemStatus > 0) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("Item has been clone successfully", false);
                        LoadGV();
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on cloning Item', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //DisplayMessage("Error on cloning Item", true);
                    }

                    AddItem addItem = addItemController.FetchItemByItemId(0, int.Parse(ViewState["CompanyId"].ToString()));


                    if (addItem.ItemImagePath == "" || addItem.ItemImagePath == "&nbsp;") {
                        imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();
                    }
                    else {
                        imageid.Src = addItem.ItemImagePath + "?" + LocalTime.Now.Ticks.ToString();
                    }
                }
            }
            catch (Exception ex) {

                throw;
            }
        }

        protected void btnEditSpec_Click(object sender, ImageClickEventArgs e) {
            try {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                int itemid = Convert.ToInt32(gvTempBoms.Rows[x].Cells[0].Text);
                int Seqno = Convert.ToInt32(gvTempBoms.Rows[x].Cells[1].Text);
                string material = ((TextBox)gvTempBoms.Rows[x].FindControl("txtMaterial")).Text.ToString();
                string description = ((TextBox)gvTempBoms.Rows[x].FindControl("txtDescription")).Text.ToString();

                int result = addItemBOMController.UpdateAddItemBOM(int.Parse(ViewState["CompanyId"].ToString()), itemid, Seqno, material, description, LocalTime.Now, Session["UserId"].ToString(), 1);
                if (result > 0) {
                    msgpanel.Visible = true;
                    lblSuccess.Visible = true;
                }
                else {
                    msgpanel.Visible = true;
                    lblDanger.Visible = true;
                }

                List<AddItemBOM> BOMList = addItemBOMController.GetBOMListByItemId(int.Parse(Session["CompanyId"].ToString()), itemid);
                gvTempBoms.DataSource = BOMList;
                gvTempBoms.DataBind();

                imageid.Src = "~/LoginResources/images/noItem.png" + "?" + LocalTime.Now.Ticks.ToString();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('#myModalViewBom').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {

                lblDanger.Visible = true;
            }
        }

        protected void btnSpecSave_Click(object sender, EventArgs e) {
            //int itemid = Convert.ToInt32(gvTempBoms.Rows[0].Cells[0].Text);
            //int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            //int itemId = int.Parse(gvAddItems.Rows[x].Cells[3].Text);
            //changes by Aadil to Add Item Specification on Edit
            string[] arr = hdnField.Value.Split(',');
            var list = arr.ToList();
            list.RemoveAll(o => string.IsNullOrWhiteSpace(o));
            List<ArraySpec> arraList = new List<ArraySpec>();
            if (list != null || arr[1] != "") {
                for (int i = 0; i < list.Count; i += 2) {
                    ArraySpec arrayItems = new ArraySpec();
                    arrayItems.Metirial = list[i];
                    arrayItems.Description = list[i + 1];
                    arraList.Add(arrayItems);
                }
            }

            int result = addItemBOMController.SaveItemSpecDetails(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(Session["itemid"].ToString()), arraList, LocalTime.Now, LocalTime.Now, Session["UserId"].ToString(), "", 1);

            List<AddItemBOM> BOMList = addItemBOMController.GetBOMListByItemId(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["itemid"].ToString()));
            gvTempBoms.DataSource = BOMList;
            gvTempBoms.DataBind();

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('#myModalViewBom').modal('show'); });   </script>", false);
        }

        protected void ddlStockMaintainingType_SelectedIndexChanged(object sender, EventArgs e) {

            ViewState["addedBatches"] = null;
            Session["Warehouses"] = null;
            ViewState["deletedBatches"] = null;
            Session["ItemBatches"] = null;

            //    //    if (Session["itemId"] != null) {
            //    //        int stockMaintaingType = addItemController.GetStockMaintaininType(int.Parse(Session["itemId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

            //    //        if (stockMaintaingType != int.Parse(ddlStockMaintainingType.SelectedValue)) {
            //    //            if (stockMaintaingType == 1 || int.Parse(ddlStockMaintainingType.SelectedValue) == 1) {
            //    //                btnManageStock.Enabled = false;
            //    //            }
            //    //            else {
            //    //                btnManageStock.Enabled = true;
            //    //            }
            //    //        }
            //    //        else {
            //    //            btnManageStock.Enabled = true;
            //    //        }
            //    //    }
            //Session["Warehouses"] = null;
        }
        //    
        }
    public class ArraySpecifications {
        public string Metirial { get; set; }
        public string Description { get; set; }
    }
}