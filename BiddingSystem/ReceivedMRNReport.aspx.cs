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
    public partial class ReceivedMRNReport : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        DepartmentWarehouseController departmentWarehouseController = ControllerFactory.CreateDepartmentWarehouse();
        MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        MrndIssueNoteBatchController MrndIssueNoteBatchController = ControllerFactory.CreateMrndIssueNoteBatchController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefReports";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabReports";
                ((BiddingAdmin)Page.Master).subTabValue = "ReceivedMRNReport.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ReceivedMRNReportLink";

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
                if (Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Count() > 0)
                {

                    try
                    {
                        ddlDepartment.DataSource = ControllerFactory.CreateUserSubDepartment().getDepartmentListByDepartmentIds((Session["UserDepartments"] as List<UserSubDepartment>).Select(d => d.DepartmentId).ToList());
                        ddlDepartment.DataValueField = "SubDepartmentID";
                        ddlDepartment.DataTextField = "SubDepartmentName";
                        ddlDepartment.DataBind();
                        ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));

                        ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                        ddlMainCateGory.DataValueField = "CategoryId";
                        ddlMainCateGory.DataTextField = "CategoryName";
                        ddlMainCateGory.DataBind();
                        ddlMainCateGory.Items.Insert(0, new ListItem("All", "0"));

                        ddlMainCateGory_SelectedIndexChanged(null, null);

                        ddlWarehouses.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                        ddlWarehouses.DataValueField = "WarehouseId";
                        ddlWarehouses.DataTextField = "Location";
                        ddlWarehouses.DataBind();
                        ddlWarehouses.Items.Insert(0, new ListItem("Select Warehouse", ""));
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
                        ddlDepartment.DataSource = ControllerFactory.CreateUserSubDepartment().getDepartmentList(int.Parse(Session["CompanyId"].ToString()));
                        ddlDepartment.DataValueField = "SubDepartmentID";
                        ddlDepartment.DataTextField = "SubDepartmentName";
                        ddlDepartment.DataBind();
                        ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));

                        ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                        ddlMainCateGory.DataValueField = "CategoryId";
                        ddlMainCateGory.DataTextField = "CategoryName";
                        ddlMainCateGory.DataBind();
                        ddlMainCateGory.Items.Insert(0, new ListItem("All", "0"));

                        ddlMainCateGory_SelectedIndexChanged(null, null);

                        ddlWarehouses.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                        ddlWarehouses.DataValueField = "WarehouseId";
                        ddlWarehouses.DataTextField = "Location";
                        ddlWarehouses.DataBind();
                        ddlWarehouses.Items.Insert(0, new ListItem("Select Warehouse", ""));

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

        //protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e) {

        //    //if (Session["UserDepartments"] != null && (Session["UserDepartments"] as List<UserSubDepartment>).Count() > 0) {
        //        int DepartmentId = int.Parse(ddlDepartment.SelectedValue);
        //        try {

        //            ddlWarehouses.DataSource = departmentWarehouseController.GetWarehouseNameDepartmentId(DepartmentId);
        //            ddlWarehouses.DataValueField = "WarehouseId";
        //            ddlWarehouses.DataTextField = "WarehouseName";
        //            ddlWarehouses.DataBind();
        //            ddlWarehouses.Items.Insert(0, new ListItem("Select Warehouse", ""));

        //        }
        //        catch (Exception ex) {
        //            throw ex;
        //        }
        //  //  }
        //}

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
            int DepartmentId = int.Parse(ddlDepartment.SelectedValue);
            string DateTo = DateTime.Parse(dtTo.Text.ProcessString()).ToString();
            string DateFrom = DateTime.Parse(dtFrom.Text.ProcessString()).ToString();

            List<int> WarehouseIds = new List<int>();

            for (int i = 0; i < ddlWarehouses.Items.Count; i++)
            {
                if (ddlWarehouses.Items[i].Selected)
                {
                    WarehouseIds.Add(int.Parse(ddlWarehouses.Items[i].Value));
                }
            }


            List<MRNDIssueNote> result = mrndinController.ReceivedMRNDetails(DepartmentId, WarehouseIds, DateTo, DateFrom, int.Parse(ViewState["CompanyId"].ToString()), int.Parse(ddlItem.SelectedValue.ToString()), int.Parse(ddlMainCateGory.SelectedValue.ToString()), int.Parse(ddlSubCategory.SelectedValue.ToString()));
            ViewState["ExcelList"] = result;
            if (result.Count > 0)
            {

                btnPrint.Visible = true;
                lblSumValue.Visible = true;
                lblvalue.Visible = true;

            }
            var totalSum = result.Sum(s => s.StValue);
            var Department = ddlDepartment.SelectedItem;
            var dateTo = dtFrom.Text;
            var dateFrom = dtTo.Text;

            List<string> WarehouseNames = new List<string>();

            for (int i = 0; i < ddlWarehouses.Items.Count; i++)
            {
                if (ddlWarehouses.Items[i].Selected)
                {
                    WarehouseNames.Add(ddlWarehouses.Items[i].Text);
                }
            }

            lblWarehouses.Text = string.Join(", ", WarehouseNames);
            lblDepartment.Text = Department.ToString();
            lblTo.Text = DateTime.Parse(dateTo).ToString("dd/MM/yyyy");
            lblFrom.Text = DateTime.Parse(dateFrom).ToString("dd/MM/yyyy");
            lblSumValue.Text = totalSum.ToString("N2");

            gvItems.DataSource = result;
            gvItems.DataBind();
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    GridView gvMRNBatchDetails = e.Row.FindControl("gvMRNBatchDetails") as GridView;


                    int mrndInId = int.Parse(e.Row.Cells[1].Text);

                    gvMRNBatchDetails.DataSource = MrndIssueNoteBatchController.getMrnReceivedInventoryBatches(mrndInId);
                    gvMRNBatchDetails.DataBind();

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
            List<MRNDIssueNote> result = (List<MRNDIssueNote>)ViewState["ExcelList"];
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







