using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class ViewPrForTabulationsheetApprovals : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForTabulationsheetApproval.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewPrForTabulationSheetApprovalLink";
                
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 19) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (int.Parse(Session["UserId"].ToString()) != 0) {

                    txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                    int SearchStatus = 0;
                    ViewState["SearchStatus"] = SearchStatus;

                    //gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForQuotationComparisonReviwByDate(int.Parse(Session["CompanyId"].ToString()), LocalTime.Now);
                    //gvPurchaseRequest.DataBind();
                    //List<PrMasterV2> prMasterV2List = pr_MasterController.GetPrListForQuotationComparisonReviw(int.Parse(Session["CompanyId"].ToString())).ToList();
                    List<PrMasterV2> prMasterV2List = pr_MasterController.GetPrListForQuotationComparisonReviwWithItem(int.Parse(Session["CompanyId"].ToString())).ToList();
                    gvPurchaseRequest.DataSource = prMasterV2List;
                    gvPurchaseRequest.DataBind();


                }
            }
        }
        protected void gvPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvPurchaseRequest.PageIndex = e.NewPageIndex;

                if (ViewState["SearchStatus"].ToString() == "1" && ViewState["SearchStatus"] != null) {

                    if (int.Parse(Session["UserId"].ToString()) != 0) {
                        if (rdbMonth.Checked) {
                            DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

                            gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForQuotationComparisonReviwByDate(int.Parse(Session["CompanyId"].ToString()), date);
                            gvPurchaseRequest.DataBind();

                        }
                        else {
                            //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                            //int prCode = int.Parse(newString);
                            string prCode = txtPrCode.Text;

                            gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForQuotationComparisonReviwByPrCode(int.Parse(Session["CompanyId"].ToString()), prCode);
                            gvPurchaseRequest.DataBind();
                        }
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
                    }

                }
                else if (ViewState["SearchStatus"].ToString() == "0" && ViewState["SearchStatus"] != null) {
                    gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForQuotationComparisonReviw(int.Parse(Session["CompanyId"].ToString()));
                    gvPurchaseRequest.DataBind();
                }
            }
            catch (Exception ex) {

                throw;
            }
        }
        protected void btnBasicSearch_Click(object sender, EventArgs e) {
            try {
                
                int SearchStatus = 1;
                ViewState["SearchStatus"] = SearchStatus;
                if (rdbMonth.Checked) {
                    DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

                    gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForQuotationComparisonReviwByDate(int.Parse(Session["CompanyId"].ToString()), date);
                    gvPurchaseRequest.DataBind();
                }
                else {
                    //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                    //int prCode = int.Parse(newString);
                    string prCode = txtPrCode.Text;

                    gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForQuotationComparisonReviwByPrCode(int.Parse(Session["CompanyId"].ToString()), prCode);
                    gvPurchaseRequest.DataBind();
                }
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                int prid = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                int PurchaseType = int.Parse(gvPurchaseRequest.Rows[x].Cells[11].Text);

                if (PurchaseType == 1) {
                    Response.Redirect("ViewQuotationTRApprovalNew.aspx?PrId=" + prid);
                }
                else {
                    Response.Redirect("ViewQuotationTRApprovalNewImports.aspx?PrId=" + prid);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void lbtnViewNew_Click(object sender, EventArgs e) {
            try {
                int prid = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                int PurchaseType = int.Parse(gvPurchaseRequest.Rows[x].Cells[11].Text);
                int ImportItemType = int.Parse(gvPurchaseRequest.Rows[x].Cells[13].Text);

                if (PurchaseType == 1) {
                    Response.Redirect("ViewQuotationTRApprovalNew.aspx?PrId=" + prid);
                }
                else {
                    if (ImportItemType == 1) {
                        Response.Redirect("ViewQuotationTRApprovalNewImportsSparePart.aspx?PrId=" + prid);
                    }
                    else {
                        Response.Redirect("ViewQuotationTRApprovalNewImports_2.aspx?PrId=" + prid);
                    }
                    
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }
    }
}