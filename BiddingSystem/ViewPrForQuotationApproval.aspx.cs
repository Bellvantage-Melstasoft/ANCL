using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;

namespace BiddingSystem
{
    public partial class ViewPrForQuotationApproval : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        public static CompanyLogin companyLogin = new CompanyLogin();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
        public string datetimepattern = System.Configuration.ConfigurationSettings.AppSettings["datePattern"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForQuotationApproval.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "quotationApprovalLink";


                UserId = Session["UserId"].ToString();
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 13) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                if (int.Parse(UserId) != 0)
                {
                    try
                    {
                        ddlSupplier.DataSource = Utils.getallSuppliers(CompanyId);
                        ddlSupplier.DataTextField = "SupplierName";
                        ddlSupplier.DataValueField = "SupplierId";
                        ddlSupplier.DataBind();
                        ddlSupplier.Items.Insert(0, new ListItem("Select Supplier", "0"));


                        ddlCategories.DataSource = Utils.getallCtegories(CompanyId);
                        ddlCategories.DataTextField = "CategoryName";
                        ddlCategories.DataValueField = "CategoryId";
                        ddlCategories.DataBind();
                        ddlCategories.Items.Insert(0, new ListItem("Select a Categoty", "0"));

                        List<PR_Master> pr_Master = pr_MasterController.GetPrListForQuotationApproval(CompanyId);
                        ((BoundField)gvPurchaseRequest.Columns[3]).DataFormatString = datetimepattern;
                        ((BoundField)gvPurchaseRequest.Columns[7]).DataFormatString = datetimepattern;
                        if (Request.QueryString.Get("UserId") != null)
                        {
                            int clickedUserId = int.Parse(Request.QueryString.Get("UserId"));
                            List<ItemCategoryBidApproval> listItemCategoryBidApproval = itemCategoryApprovalController.GetItemCategoryBidApprovalByDesignationId(companyLogin.DesignationId);
                            pr_Master = pr_Master.Where(p => listItemCategoryBidApproval.Any(p2 => p2.PRId == p.PrId)).ToList();
                            gvPurchaseRequest.DataSource = pr_Master.OrderByDescending(r => r.PrId).ToList();
                            gvPurchaseRequest.DataBind();
                        }
                        else{
                            gvPurchaseRequest.DataSource = pr_Master.OrderByDescending(r => r.PrId).ToList();
                            gvPurchaseRequest.DataBind();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int prid = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                Response.Redirect("ApproveQuotations.aspx?PrId=" + prid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ((BoundField)gvPurchaseRequest.Columns[3]).DataFormatString = datetimepattern;
            ((BoundField)gvPurchaseRequest.Columns[7]).DataFormatString = datetimepattern;
            List<PR_Master> pr_Master=pr_MasterController.GetQuotationBidddingrAdvanceSearch(CompanyId, int.Parse(ddlCategories.SelectedValue), int.Parse(ddlSupplier.SelectedValue), int.Parse(ddlAdvancesearchitems.SelectedValue), txtSearch.Text, "S");
            if (Request.QueryString.Get("UserId") != null)
            {
               
                int clickedUserId = int.Parse(Request.QueryString.Get("UserId"));
                List<ItemCategoryBidApproval> listItemCategoryBidApproval = itemCategoryApprovalController.GetItemCategoryBidApprovalByDesignationId(companyLogin.DesignationId);
                pr_Master = pr_Master.Where(p => listItemCategoryBidApproval.Any(p2 => p2.PRId == p.PrId)).ToList();
                gvPurchaseRequest.DataSource = pr_Master.OrderByDescending(r => r.PrId).ToList();
                gvPurchaseRequest.DataBind();
            }
            else
            {
                gvPurchaseRequest.DataSource = pr_Master.OrderByDescending(r => r.PrId).ToList();
                gvPurchaseRequest.DataBind();
            }
          
            if (ddlAdvancesearchitems.SelectedValue == "3")
            {

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $( '#ContentSection_txtSearch' ).addClass( 'date1' );$( '#ContentSection_txtSearch' ).prop('type', 'date') });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $( '#ContentSection_txtSearch' ).removeClass( 'date1' );$( '#ContentSection_txtSearch' ).prop('type', 'text')});   </script>", false);
            }
        }

        protected void ddlAdvancesearchitems_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            if (ddlAdvancesearchitems.SelectedIndex != 0)
            {

                txtSearch.Enabled = true;
                if (ddlAdvancesearchitems.SelectedValue == "3")
                {

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $( '#ContentSection_txtSearch' ).addClass( 'date1' );$( '#ContentSection_txtSearch' ).prop('type', 'date') });   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $( '#ContentSection_txtSearch' ).removeClass( 'date1' );$( '#ContentSection_txtSearch' ).prop('type', 'text')});   </script>", false);
                }
            }
            else
            {
                txtSearch.Enabled = false;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            ddlAdvancesearchitems.SelectedIndex = 0;
            ddlCategories.SelectedIndex = 0;
            ddlSupplier.SelectedIndex = 0;

            List<PR_Master> pr_Master = pr_MasterController.GetPrListForQuotationApproval(CompanyId);

            if (Request.QueryString.Get("UserId") != null)
            {
                int clickedUserId = int.Parse(Request.QueryString.Get("UserId"));
                List<ItemCategoryBidApproval> listItemCategoryBidApproval = itemCategoryApprovalController.GetItemCategoryBidApprovalByDesignationId(companyLogin.DesignationId);
                pr_Master = pr_Master.Where(p => listItemCategoryBidApproval.Any(p2 => p2.PRId == p.PrId)).ToList();
                gvPurchaseRequest.DataSource = pr_Master.OrderByDescending(r => r.PrId).ToList();
                gvPurchaseRequest.DataBind();
            }
            else
            {
                gvPurchaseRequest.DataSource = pr_Master.OrderByDescending(r => r.PrId).ToList();
                gvPurchaseRequest.DataBind();
            }
        }
    }
}