using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class SupplierItemReport : System.Web.UI.Page
    {

        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierItemReportController supplierItemReportController = ControllerFactory.CreateSupplierItemReportController();

        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefSupplier";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabSupplier";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyUpdatingAndRatingSupplier.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "editSupplierLink";

                    ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                    ViewState["userId"] = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["userId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 3, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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

                    BindCategory();
                    BindSubCategory();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void BindDataSource()
        {
            List<SupplierAddItemReport> supplierAddItemReports = supplierItemReportController.GetSuppliers();

            gvSupplierItemReport.DataSource = supplierAddItemReports;
            gvSupplierItemReport.DataBind();

        }
        private void BindSubCategory()
        {
            ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
            ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryList(6);
            ddlSubCategory.DataTextField = "SubCategoryName";
            ddlSubCategory.DataValueField = "SubCategoryId";
            ddlSubCategory.DataBind();
            ddlSubCategory.Items.Insert(0, new ListItem("-Select-", ""));
        }

        private void BindCategory()
        {
            ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
            ddlCategory.DataSource = itemCategoryController.FetchItemCategoryList(6);
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", ""));
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<SupplierAddItemReport> supplierAddItemReports = supplierItemReportController.GetSuppliers();


            if (ddlStatus.SelectedValue != "")
            {
                supplierAddItemReports = supplierAddItemReports.Where(x => x.IsApproved == Convert.ToInt32(ddlStatus.SelectedValue)).ToList();

            }

            if (ddlSubCategory.SelectedValue != "")
            {
                supplierAddItemReports = supplierAddItemReports.Where(x => x.SubCategoryId == Convert.ToInt32(ddlSubCategory.SelectedValue)).ToList();

            }

            if (ddlCategory.SelectedValue != "")
            {
                supplierAddItemReports = supplierAddItemReports.Where(x => x.CategoryId == Convert.ToInt32(ddlCategory.SelectedValue)).ToList();

            }

            gvSupplierItemReport.DataSource = supplierAddItemReports;
            gvSupplierItemReport.DataBind();
        }

        protected void btnSearchAll_Click(object sender, EventArgs e)
        {
            BindDataSource();
        }
    }
}