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
    public partial class ItemPoReport : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        AddItemPOReportsController addItemPOReportsController = ControllerFactory.CreateAddItemPOReportsController();


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
                    BindItem();
                    BindSupplier();
                    BindDataSource();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void BindDataSource()
        {
            List<AddItemPOReports> addItemPOReports = addItemPOReportsController.GetItemPoReports();
            gvItemPoReport.DataSource = addItemPOReports;
            gvItemPoReport.DataBind();

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

        private void BindSupplier()
        {
            List<Supplier> supplierList = new List<Supplier>();
            SupplierController supplierController = ControllerFactory.CreateSupplierController();
            supplierList = supplierController.GetAllSupplierList();
            ddlSupplier.DataSource = supplierList;
            ddlSupplier.DataTextField = "supplierName";
            ddlSupplier.DataValueField = "supplierId";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-Select-", ""));
        }

        private void BindItem()
        {
            List<AddItem> addItems = new List<AddItem>();
            AddItemController addItemController = ControllerFactory.CreateAddItemController();
            addItems = addItemController.FetchItemList();
            ddlItem.DataSource = addItems;
            ddlItem.DataTextField = "ItemName";
            ddlItem.DataValueField = "ItemId";
            ddlItem.DataBind();
            ddlItem.Items.Insert(0, new ListItem("-Select-", ""));

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            List<AddItemPOReports> addItemPOReports = new List<AddItemPOReports>();
            addItemPOReports = addItemPOReportsController.GetItemPoReports();
            if (ddlStatus.SelectedValue != "")
            {
                addItemPOReports = addItemPOReports.Where(x => x.IsApproved == Convert.ToInt32(ddlStatus.SelectedValue)).ToList();

            }

            if (ddlSubCategory.SelectedValue != "")
            {
                addItemPOReports = addItemPOReports.Where(x => x.SubCategoryId == Convert.ToInt32(ddlSubCategory.SelectedValue)).ToList();

            }

            if (ddlCategory.SelectedValue != "")
            {
                addItemPOReports = addItemPOReports.Where(x => x.CategoryId == Convert.ToInt32(ddlCategory.SelectedValue)).ToList();

            }
            if (ddlItem.SelectedValue != "")
            {
                addItemPOReports = addItemPOReports.Where(x => x.ItemID == Convert.ToInt32(ddlItem.SelectedValue)).ToList();
            }

            if (ddlSupplier.SelectedValue != "")
            {
                addItemPOReports = addItemPOReports.Where(x => x.SupplierId == Convert.ToInt32(ddlSupplier.SelectedValue)).ToList();

            }
            if (txtEndDate.Text != "" && txtStartDate.Text != "")
            {
                addItemPOReports = addItemPOReports.Where(x => x.CreatedDate <= DateTime.Parse(txtEndDate.Text) && x.CreatedDate >= DateTime.Parse(txtStartDate.Text)).ToList();

            }

            gvItemPoReport.DataSource = addItemPOReports;
            gvItemPoReport.DataBind();

        }
    }
}