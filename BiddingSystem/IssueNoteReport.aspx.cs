using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class IssueNoteReport : System.Web.UI.Page {


        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        DepartmentWarehouseController departmentWarehouseController = ControllerFactory.CreateDepartmentWarehouse();
        MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        MrndIssueNoteBatchController mrndIssueNoteBatchController = ControllerFactory.CreateMrndIssueNoteBatchController();


        protected void Page_Load(object sender, EventArgs e) {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
                ((BiddingAdmin)Page.Master).subTabValue = "IssueNoteReport.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "IssuedNoteReportLink";

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

                        ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                        ddlMainCateGory.DataValueField = "CategoryId";
                        ddlMainCateGory.DataTextField = "CategoryName";
                        ddlMainCateGory.DataBind();
                        ddlMainCateGory.Items.Insert(0, new ListItem("All", "0"));

                        ddlMainCateGory_SelectedIndexChanged(null, null);

                        ddlDepartments.DataSource = ControllerFactory.CreateSubDepartmentController().getDepartmentList(int.Parse(Session["CompanyId"].ToString()));
                        ddlDepartments.DataValueField = "SubDepartmentID";
                        ddlDepartments.DataTextField = "SubDepartmentName";
                        ddlDepartments.DataBind();
                        ddlDepartments.Items.Insert(0, new ListItem("Select a Department", ""));
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



                        ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                        ddlMainCateGory.DataValueField = "CategoryId";
                        ddlMainCateGory.DataTextField = "CategoryName";
                        ddlMainCateGory.DataBind();
                        ddlMainCateGory.Items.Insert(0, new ListItem("All", "0"));

                        ddlMainCateGory_SelectedIndexChanged(null, null);

                        ddlDepartments.DataSource = ControllerFactory.CreateSubDepartmentController().getDepartmentList(int.Parse(Session["CompanyId"].ToString()));
                        ddlDepartments.DataValueField = "SubDepartmentID";
                        ddlDepartments.DataTextField = "SubDepartmentName";
                        ddlDepartments.DataBind();
                        ddlDepartments.Items.Insert(0, new ListItem("Select a Department", ""));

                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
                btnPrint.Visible = false;
                lblSumValue.Visible = false;
                lblvalue.Visible = false;
            }

        }

        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e) {

          //  if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Count() > 0) {
                //int WarehouseId = int.Parse(ddlWarehouse.SelectedValue);
                //try {
                //    int warehouseId = int.Parse(ddlWarehouse.SelectedValue);
                //    ddlDepartments.DataSource = departmentWarehouseController.GetDepartmentNameByWarehouseId(WarehouseId);
                //    ddlDepartments.DataValueField = "SubDepartmentId";
                //    ddlDepartments.DataTextField = "DepartmentName";
                //    ddlDepartments.DataBind();
                //    ddlDepartments.Items.Insert(0, new ListItem("Select Departments", ""));

                //}
                //catch (Exception ex) {
                //    throw ex;
                //}
         //   }
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
            int WarehouseId = int.Parse(ddlWarehouse.SelectedValue);
            string DateTo = DateTime.Parse(dtTo.Text.ProcessString()).ToString();
            string DateFrom = DateTime.Parse(dtFrom.Text.ProcessString()).ToString();
            //  int DepartmentId = int.Parse(ddlDepartments.SelectedValue);
            List<int> DepartmentIds = new List<int>();

            for (int i = 0; i < ddlDepartments.Items.Count; i++) {
                if (ddlDepartments.Items[i].Selected) {
                    DepartmentIds.Add(int.Parse(ddlDepartments.Items[i].Value));
                }
            }

            List<MRNDIssueNote>  result = mrndinController.IssueNoteDetails(WarehouseId, DepartmentIds, DateTo, DateFrom, int.Parse(ViewState["CompanyId"].ToString()), int.Parse(ddlItem.SelectedValue.ToString()), int.Parse(ddlMainCateGory.SelectedValue.ToString()), int.Parse(ddlSubCategory.SelectedValue.ToString()));
            if (result.Count > 0) {

                btnPrint.Visible = true;
                lblSumValue.Visible = true;
                lblvalue.Visible = true;

            }
            var totalSum = result.Sum(s => s.StValue);
            var Warehouse = ddlWarehouse.SelectedItem;
            var dateTo = dtFrom.Text;
            var dateFrom = dtTo.Text;

            List<string> DepartmentNames = new List<string>();

            for (int i = 0; i < ddlDepartments.Items.Count; i++) {
                if (ddlDepartments.Items[i].Selected) {
                    DepartmentNames.Add(ddlDepartments.Items[i].Text);
                }
            }

            lblDepartments.Text = string.Join(", ", DepartmentNames);
            lblWarehouse.Text = Warehouse.ToString();
            lblTo.Text = DateTime.Parse(dateTo).ToString("dd/MM/yyyy");
            lblFrom.Text = DateTime.Parse(dateFrom).ToString("dd/MM/yyyy");
            
            lblSumValue.Text = totalSum.ToString("N2");

            gvItems.DataSource = result;
                gvItems.DataBind();
            }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.DataRow) {

                    GridView gvMRNBatchDetails = e.Row.FindControl("gvMRNBatchDetails") as GridView;

                  
                    int mrndInId = int.Parse(e.Row.Cells[1].Text);

                    gvMRNBatchDetails.DataSource = mrndIssueNoteBatchController.getMrnIssuedInventoryBatches(mrndInId);
                    gvMRNBatchDetails.DataBind();

                }


            }
            catch (Exception ex) {
            }
        }

    




}
    }

