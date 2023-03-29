using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using System.Globalization;

namespace BiddingSystem
{
    public partial class ViewPrForBidApproval : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        
        protected void Page_Load(object sender, EventArgs e)
        {
          


            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForBidApproval.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approveforBidOpeninggLink";
                
                var companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                ViewState["companyLogin"] = new JavaScriptSerializer().Serialize(companyLogin);
                if (((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 2) || companyLogin.DesignationId!=25 )&& companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
                else
                {

                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }


            if (!IsPostBack)
            {
                int SearchStatus = 0;
                ViewState["SearchStatus"] = SearchStatus;

                //txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                //gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidApprovalByDate(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()), LocalTime.Now); ;
                //gvPurchaseRequest.DataBind();
                if (int.Parse(Session["UserId"].ToString()) != 0) {
                    try {
                        gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidApproval(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString())); ;
                        gvPurchaseRequest.DataBind();
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }
        }
        protected void btnBasicSearch_Click(object sender, EventArgs e) {
            try {
                int SearchStatus = 1;
                ViewState["SearchStatus"] = SearchStatus;
                if (int.Parse(Session["UserId"].ToString()) != 0) {
                    if (rdbMonth.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

                        gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidApprovalByDate(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()), date); ;
                        gvPurchaseRequest.DataBind();

                    }
                    else {
                        //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                        //int prCode = int.Parse(newString);

                        string prCode = txtPrCode.Text;

                        gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidApprovalByPrCode(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()), prCode); ;
                        gvPurchaseRequest.DataBind();
                    }
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
                }
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int prid = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                Response.Redirect("BidApproval.aspx?PrId=" + prid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvPurchaseRequest.PageIndex = e.NewPageIndex;

                if (ViewState["SearchStatus"].ToString() == "1" && ViewState["SearchStatus"] != null) {

                    if (int.Parse(Session["UserId"].ToString()) != 0) {
                        if (rdbMonth.Checked) {
                            DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

                            gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidApprovalByDate(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()), date); ;
                            gvPurchaseRequest.DataBind();

                        }
                        else {
                            //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                            //int prCode = int.Parse(newString);
                            string prCode = txtPrCode.Text;
                            gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidApprovalByPrCode(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()), prCode); ;
                            gvPurchaseRequest.DataBind();

                        }
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
                    }

                }
                else if (ViewState["SearchStatus"].ToString() == "0" && ViewState["SearchStatus"] != null) {
                    gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidApproval(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
                    gvPurchaseRequest.DataBind();
                }
            }
            catch (Exception ex) {

                throw;
            }
        }


    }
}