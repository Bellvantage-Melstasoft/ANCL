using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class ReorderItems : System.Web.UI.Page {

        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        InventoryControllerInterface inventoryControllerInterface = ControllerFactory.CreateInventoryController();
        
        protected void Page_Load(object sender, EventArgs e) {
            pnlItems.Visible = false;
            pnlAddedItems.Visible = false;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefWarehouse";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabWarehouse";
                ((BiddingAdmin)Page.Master).subTabValue = "ReorderItems.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ReorderItemsLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 12, 4) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Count() > 0) {

                    try {
                        ddlWarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseDetailsByWarehouseId((Session["UserWarehouses"] as List<UserWarehouse>).Select(d => d.WrehouseId).ToList());
                        ddlWarehouse.DataValueField = "WarehouseID";
                        ddlWarehouse.DataTextField = "Location";
                        ddlWarehouse.DataBind();
                        ddlWarehouse.Items.Insert(0, new ListItem("Select a Warehouse", ""));

                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }

                else {
                    try {
                        ddlWarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                        ddlWarehouse.DataValueField = "WarehouseID";
                        ddlWarehouse.DataTextField = "Location";
                        ddlWarehouse.DataBind();
                        ddlWarehouse.Items.Insert(0, new ListItem("Select a Warehouse", ""));

                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }

            }

        }
        protected void btnSearch_Click(object sender, EventArgs e) {
            pnlItems.Visible = true;
            int WarehouseId = int.Parse(ddlWarehouse.SelectedValue);

            List<WarehouseInventory> inventoryList = inventoryControllerInterface.GetWarehouseLowInventory(int.Parse(ViewState["CompanyId"].ToString()), WarehouseId);
            ViewState["ItemList"] = new JavaScriptSerializer().Serialize(inventoryList);

            gvItems.DataSource = inventoryList;
            gvItems.DataBind();
        }


        protected void btnAdd_Click(object sender, EventArgs e) {
            pnlItems.Visible = true;
            pnlAddedItems.Visible = true;
            var Items = new JavaScriptSerializer().Deserialize<List<WarehouseInventory>>(ViewState["ItemList"].ToString());
            var item = Items.Find(i => i.ItemID == int.Parse(hdnItemId.Value.Replace(",","")));

            List<CompanyPurchaseRequestNote.TempDataSet> tempDataSetlist;
            
            if (ViewState["DataList"] != null) {
                tempDataSetlist = new JavaScriptSerializer().Deserialize<List<CompanyPurchaseRequestNote.TempDataSet>>(ViewState["DataList"].ToString());
            }
            else {
                tempDataSetlist = new List<CompanyPurchaseRequestNote.TempDataSet>();
            }

            CompanyPurchaseRequestNote.TempDataSet tempDataSet = new CompanyPurchaseRequestNote.TempDataSet();

            tempDataSet.MainCategoryId = item.CategoryID;
            tempDataSet.MainCategoryName = item.CategoryName;
            tempDataSet.SubCategoryId = item.SubCategoryID;
            tempDataSet.SubcategoryName = item.SubCategoryName;
            tempDataSet.ItemId = item.ItemID;
            tempDataSet.ItemName = item.ItemName;
            tempDataSet.ItemQuantity = decimal.Parse(hdnQuantity.Value);
            tempDataSet.EstimatedAmount = decimal.Parse("0.00");
            tempDataSet.WarehouseId = item.WarehouseID;
            tempDataSet.WarehouseName = item.Location;


            tempDataSetlist.Add(tempDataSet);

            ViewState["DataList"] = new JavaScriptSerializer().Serialize(tempDataSetlist);


            Items.Remove(item);
            ViewState["ItemList"] = new JavaScriptSerializer().Serialize(Items);

            gvItems.DataSource = Items;
            gvItems.DataBind();

            gvAddToPR.DataSource = tempDataSetlist;
            gvAddToPR.DataBind();

        }
        protected void btnCreatePR_Click(object sender, EventArgs e) {
            pnlItems.Visible = true;


            //List<PrDetailsV2> tempDataSetlist = new JavaScriptSerializer().Deserialize<List<PrDetailsV2>>(ViewState["DataList"].ToString());

            //var grouped = tempDataSetlist.GroupBy(c => new { c.CategoryId,c.SubCategoryId, c.WarehouseId }).Select(group => new { group.Key, Items = group.ToList() }).ToList();


            //PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
            //PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();

            //List<int> PrIds = new List<int>();

            //for (int i = 0; i < grouped.Count; i++)
            //{
            //   // PrMasterV2 pr
            //    //int purchaseRequestId = ControllerFactory.CreatePrControllerV2().SavePr(prMaster);
            //    int purchaseRequestId = pr_MasterController.SavePRMasterV2(int.Parse(Session["CompanyId"].ToString()), LocalTime.Now, "Restocking", "", Session["FirstName"].ToString(), LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, "", 1, 0, "", LocalTime.Now, 0, "", LocalTime.Now, 0, 0, "Operational Expense", "", "", "", "", "", "", "", 0, grouped[i].Key.MainCategoryId, grouped[i].Key.WarehouseId);
            //    PrIds.Add(purchaseRequestId);


            //    foreach (var item in grouped[i].Items)
            //    {
            //        pr_DetailController.SavePRDetailsV2(purchaseRequestId, item.ItemId, item.measurementId, item.ItemDescription, "", LocalTime.Now, 1, item.ReplacementId, item.ItemQuantity, item.Purpose, item.EstimatedAmount);
            //    }
            //}

            //List<string> PrCodes = pr_MasterController.GetPrCodesByPrIds(PrIds);

            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
            //    "<script>    $(document).ready(function () { swal({type: 'success', title: 'Success', html: 'Purchase Requisitions was created successfully with follwing code(s). <br/>" + string.Join(", ", PrCodes) + "'}); });   </script>", false);

            //ViewState["DataList"] = null;

            //gvAddToPR.DataSource = null;
            //gvAddToPR.DataBind();

        }

    }
}
