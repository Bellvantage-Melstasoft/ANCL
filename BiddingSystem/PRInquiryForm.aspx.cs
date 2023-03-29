using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.Text;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class PRInquiryForm : System.Web.UI.Page
    {
        PR_MasterController PrMasterController = ControllerFactory.CreatePR_MasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        public static List<SubDepartment> listSubDeparment = new List<SubDepartment>();
        public static List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
        public static List<ItemCategory> listItemCategory = new List<ItemCategory>();
        static int UserId = 0;
        static int CompanyId = 0;
        static List<PR_Master> ListPrMaster = null;
        static PR_Master PrMaster = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "PRInquiryForm.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "PRInquiryFormLink";


                UserId = int.Parse(Session["UserId"].ToString());
                CompanyId = int.Parse(Session["CompanyId"].ToString());

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                listItemCategory = itemCategoryController.FetchItemCategoryList(companyLogin.DepartmentId);
                listSubDeparment = subDepartmentController.getDepartmentList(companyLogin.DepartmentId);
                CompanyLoginUserList = companyLoginController.GetAllUserList();
                if ((!companyUserAccessController.isAvilableAccess(UserId, CompanyId, 5, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                LoadCategory();
                LoadDepartment();
            }
        }

        private void LoadCategory()
        {
            try
            {
                ddlCategories.DataSource = listItemCategory;
                ddlCategories.DataValueField = "CategoryId";
                ddlCategories.DataTextField = "CategoryName";
                ddlCategories.DataBind();
                ddlCategories.Items.Insert(0, new ListItem("Select A Categoty", "0"));

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void LoadDepartment()
        {
            try
            {
                ddlDepartment.DataSource = listSubDeparment;
                ddlDepartment.DataValueField = "SubDepartmentID";
                ddlDepartment.DataTextField = "SubDepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select A Department", "0"));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ListPrMaster = PrMasterController.AdvanceSearchPRForInquiry(CompanyId, int.Parse(ddlAdvancesearchitems.SelectedValue), int.Parse(ddlCategories.SelectedValue), int.Parse(ddlDepartment.SelectedValue), txtSearch.Text);
            gvPurchaseRequest.DataSource = ListPrMaster;
            gvPurchaseRequest.DataBind();
            gvPurchaseRequest.Visible = true;
            if (ListPrMaster.Count >0)
            {
                pnlPrDetails.Visible = true;               
                pnlBidDetails.Visible = false;
            }else
            {
                pnlPrDetails.Visible = false;
                pnlBidDetails.Visible = false;
            }
            if (ddlAdvancesearchitems.SelectedValue == "3")
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                    "<script>    $(document).ready(function () { " +
                    " $( '#ContentSection_txtSearch' ).addClass( 'customDate' );" +
                    " $( '#ContentSection_txtSearch' ).attr('onchange', 'dateChange(this)');  " +
                    " $( '#ContentSection_txtSearch' ).prop('type', 'date') ;  " +
                    " $( '#ContentSection_txtSearch' ).attr('data-date', '');  " +
                    " $( '#ContentSection_txtSearch' ).attr('data-date-format', 'DD MMMM YYYY');  " +
                     " }); </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                      "<script>    $(document).ready(function () { " +
                      " $( '#ContentSection_txtSearch' ).removeClass( 'customDate' ); " +
                      "$( '#ContentSection_txtSearch' ).prop('type', 'text'); " +
                       " $( '#ContentSection_txtSearch' ).removeAttr('onchange');  " +
                       " $( '#ContentSection_txtSearch' ).removeAttr('data-date');  " +
                      " $( '#ContentSection_txtSearch' ). removeAttr('data-date-format'); " +
                      "  });  </script>", false);
            }
            LoadPRs();
            if (ddlAdvancesearchitems.SelectedValue == "3" && txtSearch.Text != "")
            {
                txtSearch.Text = Convert.ToDateTime(txtSearch.Text).ToString("yyyy-MM-dd");
            }
        }

        private void LoadPRs()
        {

            try
            {
                ddlPr.DataSource = ListPrMaster;
                ddlPr.DataValueField = "PrId";
                ddlPr.DataTextField = "PrCode";
                ddlPr.DataBind();
                ddlPr.Items.Insert(0, new ListItem("Select A PR", ""));
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.select2').select2(); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlPr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPr.SelectedIndex != 0)
            {
                PrMaster = ListPrMaster.Find(x => x.PrId == Convert.ToInt32(ddlPr.SelectedValue));
                ddlBids.Items.Clear();

                ddlBids.Items.Insert(0, new ListItem("Select a Bid to view more details..", ""));
                for (int i = 0; i < PrMaster.Bids.Count; i++)
                {
                    ddlBids.Items.Add(new ListItem("B" + PrMaster.Bids[i].BidCode, PrMaster.Bids[i].BidId.ToString()));
                }

            }
            else
            {
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
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                    "<script>    $(document).ready(function () { " +
                    " $( '#ContentSection_txtSearch' ).addClass( 'customDate' );" +
                    " $( '#ContentSection_txtSearch' ).attr('onchange', 'dateChange(this)');  " +
                    " $( '#ContentSection_txtSearch' ).prop('type', 'date') ;  " +
                    " $( '#ContentSection_txtSearch' ).attr('data-date', '');  " +
                    " $( '#ContentSection_txtSearch' ).attr('data-date-format', 'DD MMMM YYYY');  " +
                     " }); </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                    "<script>    $(document).ready(function () { " +
                    " $( '#ContentSection_txtSearch' ).removeClass( 'customDate' ); " +
                    "$( '#ContentSection_txtSearch' ).prop('type', 'text'); " +
                     " $( '#ContentSection_txtSearch' ).removeAttr('onchange');  " +
                     " $( '#ContentSection_txtSearch' ).removeAttr('data-date');  " +
                    " $( '#ContentSection_txtSearch' ). removeAttr('data-date-format'); " +
                    "  });  </script>", false);
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
            ddlDepartment.SelectedIndex = 0;
            gvPurchaseRequest.Visible = false;
            pnlPrDetails.Visible = false;
            pnlBidDetails.Visible = false;
            ListPrMaster = null;
            gvPurchaseRequest.DataSource = ListPrMaster;
            gvPurchaseRequest.DataBind();
        }


        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PrID = int.Parse(gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;

                gvPrDetails.DataSource = ListPrMaster.Find(x => x.PrId == PrID).PrDetails;
                gvPrDetails.DataBind();
            }
        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                gvBidItems.DataSource = PrMaster.Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }
        }

        protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int poId = int.Parse(gvPO.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPoItems = e.Row.FindControl("gvPoItems") as GridView;

                gvPoItems.DataSource = PrMaster.Bids.Find(b => b.BidId == int.Parse(ddlBids.SelectedValue)).POsCreated.Find(po=> po.PoID == poId).PoDetails;
                gvPoItems.DataBind();
            }

        }

        protected void gvQuotations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int quotationId = int.Parse(gvQuotations.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;

                gvQuotationItems.DataSource = PrMaster.Bids.Find(b => b.BidId == int.Parse(ddlBids.SelectedValue)).SupplierQuotations.Find(sq=> sq.QuotationId ==quotationId).QuotationItems;
                gvQuotationItems.DataBind();
            }

        }

        protected void gvGrn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int grnId = int.Parse(gvGrn.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvGrnItems = e.Row.FindControl("gvGrnItems") as GridView;

                gvGrnItems.DataSource = PrMaster.Bids.Find(b => b.BidId == int.Parse(ddlBids.SelectedValue)).GRNsCreated.Find(grn => grn.GrnId == grnId)._GrnDetailsList;
                gvGrnItems.DataBind();
            }
        }

        protected void ddlBids_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlBids.SelectedIndex != 0)
            {
                gvBids.DataSource = PrMaster.Bids.Where(b => b.BidId == int.Parse(ddlBids.SelectedValue));
                gvBids.DataBind();

                gvQuotations.DataSource = PrMaster.Bids.Find(b => b.BidId == int.Parse(ddlBids.SelectedValue)).SupplierQuotations;
                gvQuotations.DataBind();

                gvPO.DataSource = PrMaster.Bids.Find(b => b.BidId == int.Parse(ddlBids.SelectedValue)).POsCreated;
                gvPO.DataBind();

                gvGrn.DataSource = PrMaster.Bids.Find(b => b.BidId == int.Parse(ddlBids.SelectedValue)).GRNsCreated;
                gvGrn.DataBind();

                pnlBidDetails.Visible = true;
            }
            else
            {
                pnlBidDetails.Visible = false;
            }
        }

       

        protected void gvPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPurchaseRequest.PageIndex = e.NewPageIndex;
                gvPurchaseRequest.DataSource = ListPrMaster;
                gvPurchaseRequest.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}