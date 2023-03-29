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
    public partial class InventorySummaryReport : System.Web.UI.Page {
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
                ((BiddingAdmin)Page.Master).subTabValue = "InventorySummaryReport.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "InventoryReportLinkFull";


                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 8, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                btnPrint.Visible = false;
                try {
                    ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                    ddlMainCateGory.DataValueField = "CategoryId";
                    ddlMainCateGory.DataTextField = "CategoryName";
                    ddlMainCateGory.DataBind();
                    ddlMainCateGory.Items.Insert(0, new ListItem("All", "0"));

                    ddlMainCateGory_SelectedIndexChanged(null, null);

                    ddlWarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                    ddlWarehouse.DataValueField = "WarehouseID";
                    ddlWarehouse.DataTextField = "Location";
                    ddlWarehouse.DataBind();
                    ddlWarehouse.Items.Insert(0, new ListItem("All", "0"));

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
                    ddlSubCategory.Items.Insert(0, new ListItem("All", "0"));

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
                ddlItem.Items.Insert(0, new ListItem("All", "0"));



            }
            catch (Exception ex) {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            btnPrint.Visible = true;
            List<WarehouseInventory> FetchItemList = inventoryControllerInterface.FetchItemListDetailed(int.Parse(Session["CompanyId"].ToString()), int.Parse(ddlWarehouse.SelectedValue.ToString()), int.Parse(ddlItem.SelectedValue.ToString()), int.Parse(ddlMainCateGory.SelectedValue.ToString()), int.Parse(ddlSubCategory.SelectedValue.ToString()));

            gvItems.DataSource = FetchItemList.Where(x => x.AvailableQty != 0 || x.StockValue != 0);
            gvItems.DataBind();
        }


        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.DataRow) {

                    GridView gvBatchDetails = e.Row.FindControl("gvBatchDetails") as GridView;

                    int ItemId = int.Parse(e.Row.Cells[1].Text);
                    int WarehouseId = int.Parse(e.Row.Cells[2].Text);

                    gvBatchDetails.DataSource = WarehouseInventoryBatchesController.getWarehouseInventoryBatchesForInventory(ItemId, int.Parse(Session["CompanyId"].ToString()), WarehouseId);
                    gvBatchDetails.DataBind();

                }


            }
            catch (Exception ex) {
            }
        }

            }
}