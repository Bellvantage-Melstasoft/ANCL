using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class EditInventory : System.Web.UI.Page {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        InventoryControllerInterface inventoryControllerInterface = ControllerFactory.CreateInventoryController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseInventoryBatchesController WarehouseInventoryBatchesController = ControllerFactory.CreateWarehouseInventoryBatchesController();

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
                ((BiddingAdmin)Page.Master).subTabValue = "EditInventory.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "EditInventoryLink";


                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 10, 5) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                // btnPrint.Visible = false;
                pnlBatch.Visible = false;
                try {
                    ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                    ddlMainCateGory.DataValueField = "CategoryId";
                    ddlMainCateGory.DataTextField = "CategoryName";
                    ddlMainCateGory.DataBind();
                    //ddlMainCateGory.Items.Insert(0, new ListItem("All", "0"));
                    ddlMainCateGory.Items.Insert(0, new ListItem("Select Category", "0"));

                    ddlMainCateGory_SelectedIndexChanged(null, null);

                    ddlWarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                    ddlWarehouse.DataValueField = "WarehouseID";
                    ddlWarehouse.DataTextField = "Location";
                    ddlWarehouse.DataBind();
                    // ddlWarehouse.Items.Insert(0, new ListItem("All", "0"));
                    ddlWarehouse.Items.Insert(0, new ListItem("Select Warehouse", "0"));

                }
                catch (Exception ex) {
                }
            }
        }




        protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (int.Parse(ddlMainCateGory.SelectedValue) != 0 || ddlMainCateGory.SelectedValue != "") {
                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(mainCategoryId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                    ddlSubCategory.DataTextField = "SubCategoryName";
                    ddlSubCategory.DataValueField = "SubCategoryId";
                    ddlSubCategory.DataBind();
                    // ddlSubCategory.Items.Insert(0, new ListItem("All", "0"));
                    ddlSubCategory.Items.Insert(0, new ListItem("Slect Sub-Category", "0"));

                    ddlSubCategory_SelectedIndexChanged(null, null);

                }

            }
            catch (Exception ex) {

            }
        }

        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e) {

            try {

                ddlItem.DataSource = addItemController.FetchItemsByCategories(int.Parse(ddlMainCateGory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue), int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsActive == 1).OrderBy(y => y.ItemId).ToList();
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "ItemId";
                ddlItem.DataBind();
                // ddlItem.Items.Insert(0, new ListItem("All", "0"));
                ddlItem.Items.Insert(0, new ListItem("Select Item", "0"));



            }
            catch (Exception ex) {

            }
        }


        protected void ddlBatches_SelectedIndexChanged(object sender, EventArgs e) {
            int ItemId = int.Parse(ddlItem.SelectedValue);
            int WarehouseId = int.Parse(ddlWarehouse.SelectedValue);
            int batchId = int.Parse(ddlBatches.SelectedValue);
            pnlBatchItem.Visible = true;
            try {
                gvBatchDetails.DataSource = WarehouseInventoryBatchesController.getWarehouseInventoryBatchesForInventoryEdit(ItemId, int.Parse(Session["CompanyId"].ToString()), WarehouseId, batchId);
                gvBatchDetails.DataBind();
            }
            catch (Exception ex) {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) {

            int ItemId = int.Parse(ddlItem.SelectedValue);
            int WarehouseId = int.Parse(ddlWarehouse.SelectedValue);

            gvBatchDetails.DataSource = null;
            gvBatchDetails.DataBind();

            gvItems.DataSource = null;
            gvItems.DataBind();
            pnlBatch.Visible = false;
            pnlBatchItem.Visible = false;
            pnlItem.Visible = false;

            if (ItemId != 0) {
                AddItem addItem = addItemController.FetchItemListByIdObj(ItemId);
                if (addItem.StockMaintainingType != 1) {
                    pnlBatch.Visible = true;

                    ddlBatches.DataSource = WarehouseInventoryBatchesController.getWarehouseInventoryBatchesListByWarehouseId(WarehouseId, ItemId, int.Parse(Session["CompanyId"].ToString()));
                    ddlBatches.DataTextField = "BatchCode";
                    ddlBatches.DataValueField = "BatchchId";
                    ddlBatches.DataBind();
                    ddlBatches.Items.Insert(0, new ListItem("Select Batch", "0"));
                }
                else {
                    pnlItem.Visible = true;
                    LoadGvItem();
                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Select an Item'}); });   </script>", false);

            }

        }

        protected void btnSave_Click(object sender, EventArgs e) {
            int ItemId = int.Parse(ddlItem.SelectedValue);
            int WarehouseId = int.Parse(ddlWarehouse.SelectedValue);

            AddItem addItem = addItemController.FetchItemListByIdObj(ItemId);


            if (addItem.StockMaintainingType == 1) {
                decimal newQty = 0;
                decimal newStockValue = 0;

                for (int i = 0; i < gvItems.Rows.Count; i++) {
                    newQty = decimal.Parse(((TextBox)gvItems.Rows[i].FindControl("txtItem")).Text);
                    newStockValue = decimal.Parse(((TextBox)gvItems.Rows[i].FindControl("txtStockValue")).Text);
                }
                int result = inventoryControllerInterface.SaveEditedMasterInventory(WarehouseId, ItemId, newQty, newStockValue, int.Parse(Session["UserId"].ToString()));
                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    LoadGvItem();
                }

            }
            else {
                int BatchId = int.Parse(ddlBatches.SelectedValue);
                decimal newQtyBatch = 0;
                decimal newStockValueBatch = 0;
                decimal newQty = 0;
                decimal newStockValue = 0;
                
                List<WarehouseInventoryBatches> WarehouseInventoryBatchesList = WarehouseInventoryBatchesController.getWarehouseInventoryBatchesForInventory(ItemId, int.Parse(Session["CompanyId"].ToString()),WarehouseId);
                for (int i = 0; i < WarehouseInventoryBatchesList.Count; i++) {
                    if (WarehouseInventoryBatchesList[i].BatchchId != BatchId) {
                        newQty = WarehouseInventoryBatchesList[i].AvailableStock + newQty;
                        newStockValue = WarehouseInventoryBatchesList[i].StockValue + newStockValue;

                    }
                }

                for (int i = 0; i < gvBatchDetails.Rows.Count; i++) {
                    newQtyBatch = decimal.Parse((gvBatchDetails.Rows[i].FindControl("txtBatchItem") as TextBox).Text);
                    newStockValueBatch = decimal.Parse((gvBatchDetails.Rows[i].FindControl("txtStockValueB") as TextBox).Text);
                    
                }
                newQty = newQty + newQtyBatch;
                newStockValue = newStockValue + newStockValueBatch;

                int result = inventoryControllerInterface.SaveEditedBatchInventory(WarehouseId, ItemId, newQty, newStockValue, int.Parse(Session["UserId"].ToString()), BatchId, newQtyBatch, newStockValueBatch);
                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                    int batchId = int.Parse(ddlBatches.SelectedValue);
                    gvBatchDetails.DataSource = WarehouseInventoryBatchesController.getWarehouseInventoryBatchesForInventoryEdit(ItemId, int.Parse(Session["CompanyId"].ToString()), WarehouseId, batchId);
                    gvBatchDetails.DataBind();
                }
            }
        }

        private void LoadGvItem() {
            int ItemId = int.Parse(ddlItem.SelectedValue);
            int WarehouseId = int.Parse(ddlWarehouse.SelectedValue);
            List<WarehouseInventory> FetchItemList = inventoryControllerInterface.FetchItemListDetailed(int.Parse(Session["CompanyId"].ToString()), WarehouseId, ItemId, int.Parse(ddlMainCateGory.SelectedValue.ToString()), int.Parse(ddlSubCategory.SelectedValue.ToString()));
            gvItems.DataSource = FetchItemList;
            gvItems.DataBind();
        }
    }
}