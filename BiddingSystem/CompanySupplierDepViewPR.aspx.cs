using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class CompanySupplierDepViewPR : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PrControllerV2 prController = ControllerFactory.CreatePrControllerV2();

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "") {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabId = "submitForBiddingLink";
                
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }


            if (!IsPostBack) {
                if (int.Parse(Session["UserId"].ToString()) != 0) {
                    try {
                        int SearchStatus = 0;
                        ViewState["SearchStatus"] = SearchStatus;

                        //gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidSubmission(int.Parse(Session["CompanyId"].ToString()));
                        gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidSubmissionWithItem(int.Parse(Session["CompanyId"].ToString()));
                        gvPurchaseRequest.DataBind();
                        txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");

                        //gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidSubmissionByDate(int.Parse(Session["CompanyId"].ToString()), LocalTime.Now);
                        //gvPurchaseRequest.DataBind();
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
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

                            gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidSubmissionByDate(int.Parse(Session["CompanyId"].ToString()), date);
                            gvPurchaseRequest.DataBind();

                        }
                        else {
                            //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                            //int prCode = int.Parse(newString);
                            string prCode = txtPrCode.Text;

                            gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidSubmissionByPrCode(int.Parse(Session["CompanyId"].ToString()), prCode);
                            gvPurchaseRequest.DataBind();

                        }
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
                    }

                }
                else if (ViewState["SearchStatus"].ToString() == "0" && ViewState["SearchStatus"] != null) {
                    gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidSubmission(int.Parse(Session["CompanyId"].ToString()));
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

                    gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidSubmissionByDate(int.Parse(Session["CompanyId"].ToString()), date);
                    gvPurchaseRequest.DataBind();

                }
                else {
                    //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                    //int prCode = int.Parse(newString);
                    string prCode = txtPrCode.Text;

                    gvPurchaseRequest.DataSource = pr_MasterController.GetPrListForBidSubmissionByPrCode(int.Parse(Session["CompanyId"].ToString()), prCode);
                    gvPurchaseRequest.DataBind();

                }
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }

        protected void btnView_Click(object sender, EventArgs e) {
            try {
                int prid = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[0].Text);
                Response.Redirect("SubmitPRForBidListing.aspx?PrId=" + prid);
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        protected void btnReject_ClickNew(object sender, EventArgs e)
        {
            int prid = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[0].Text);
            ViewState["PrId"] = prid;
            //ScriptManager.RegisterStartupScript(this, GetType(), "myFunction", "myFunction()", true);
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "rejectPR", "rejectPR(event)", true);
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                // int prid = int.Parse(((GridViewRow)((Button)sender).NamingContainer).Cells[0].Text);
                int prId = int.Parse(ViewState["PrId"].ToString());
                PrMasterV2 prMaster = pr_MasterController.getPRMasterDetailByPrId(prId);
                int expenseType = prMaster.ExpenseType;
                
                string remarks = hdnRemarks.Value.ProcessString();
                if (prController.ApproveOrRejectPr(expenseType, prId, 2, 0, int.Parse(Session["UserId"].ToString()), remarks) > 0)
                {
                    List<string> emailAddress = ControllerFactory.CreateEmailController().WHHeadandMRNCreatorEmails(prId);
             
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CompanySupplierDepViewPR.aspx'}); });   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on reject PR\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}