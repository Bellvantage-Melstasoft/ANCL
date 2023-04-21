using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class StockOverRideReport : System.Web.UI.Page
    {


        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        DepartmentWarehouseController departmentWarehouseController = ControllerFactory.CreateDepartmentWarehouse();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        StockOverrideLogController StockOverrideLogController = ControllerFactory.CreateStockOverrideLogController();
        StockOverrideBatchLogController stockOverrideBatchLogController = ControllerFactory.CreateStockOverrideBatchLogController();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
                ((BiddingAdmin)Page.Master).subTabValue = "StockOverRideReport.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "StockOverRideReporttLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 12, 4) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Count() > 0)
                {

                    try
                    {
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


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

                else
                {
                    try
                    {
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



                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                btnPrint.Visible = false;
                lblSumValue.Visible = false;
                lblvalue.Visible = false;
            }

        }


        protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(ddlMainCateGory.SelectedValue) != 0 || ddlMainCateGory.SelectedValue != "")
                {
                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(mainCategoryId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                    ddlSubCategory.DataTextField = "SubCategoryName";
                    ddlSubCategory.DataValueField = "SubCategoryId";
                    ddlSubCategory.DataBind();
                    ddlSubCategory.Items.Insert(0, new ListItem("All", "0"));

                    ddlSubCategory_SelectedIndexChanged(null, null);

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                ddlItem.DataSource = addItemController.FetchItemsByCategories(int.Parse(ddlMainCateGory.SelectedValue), int.Parse(ddlSubCategory.SelectedValue), int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsActive == 1).OrderBy(y => y.ItemId).ToList();
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataValueField = "ItemId";
                ddlItem.DataBind();
                ddlItem.Items.Insert(0, new ListItem("All", "0"));



            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int WarehouseId = int.Parse(ddlWarehouse.SelectedValue);
            DateTime DateTo = DateTime.Parse(dtTo.Text.ProcessString());
            DateTime DateFrom = DateTime.Parse(dtFrom.Text.ProcessString());

            List<StockOverrideLog> result = StockOverrideLogController.GetOVerRiddenLog(WarehouseId, int.Parse(ViewState["CompanyId"].ToString()), int.Parse(ddlItem.SelectedValue.ToString()), int.Parse(ddlMainCateGory.SelectedValue.ToString()), int.Parse(ddlSubCategory.SelectedValue.ToString()), DateTo, DateFrom);
            ViewState["ExcelList"] = result;

            if (result.Count > 0)
            {

                btnPrint.Visible = true;
                lblSumValue.Visible = true;
                lblvalue.Visible = true;

            }
            var totalSum = result.Sum(s => s.OverridingStockValue);
            var Warehouse = ddlWarehouse.SelectedItem;

            List<string> DepartmentNames = new List<string>();

            lblWarehouse.Text = Warehouse.ToString();

            lblSumValue.Text = totalSum.ToString("N2");

            gvItems.DataSource = result.Where(w => w.ExistedQty != 0 || w.OverridingQty != 0);
            gvItems.DataBind();

        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    GridView gvBatchDetails = e.Row.FindControl("gvBatchDetails") as GridView;


                    int LogId = int.Parse(e.Row.Cells[1].Text);

                    gvBatchDetails.DataSource = stockOverrideBatchLogController.getOverriddenBathes(LogId);
                    gvBatchDetails.DataBind();

                }


            }
            catch (Exception ex)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnRun_ServerClick(object sender, EventArgs e)
        {
            List<StockOverrideLog> result = (List<StockOverrideLog>)ViewState["ExcelList"];
            gvItems.DataSource = result;
            gvItems.DataBind();

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Company PR Report" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvItems.GridLines = GridLines.Both;
            //tblTaSummary.HeaderStyle.Font.Bold = true;
            gvItems.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();


        }
    }
}

