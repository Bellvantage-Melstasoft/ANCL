using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class ViewPrStockAvailabilityExpenseApprove : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseControllerInterface WarehouseController = ControllerFactory.CreateWarehouseController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        SubDepartmentControllerInterface subDepartmentControllerInterface = ControllerFactory.CreateSubDepartmentController();
        PrControllerV2 prControllerV2 = ControllerFactory.CreatePrControllerV2();

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrStockAvailabilityExpenseApprove.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "availabilityExpensePRLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 12, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                try {
                    txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                    //List<PrMasterV2> prMaster = prControllerV2.FetchPrListForAvailabiltyExpenseApproval();
                    //gvPr.DataSource = prMaster;
                    //gvPr.DataBind();
                    int SearchStatus = 0;
                    ViewState["SearchStatus"] = SearchStatus;
                    
                    //List<PrMasterV2> prMaster = prControllerV2.FetchPrListForAvailabiltyExpenseApprovalByDate(LocalTime.Now);
                    List<PrMasterV2> prMaster = prControllerV2.FetchPrListForAvailabiltyExpenseApproval();
                    gvPr.DataSource = prMaster;
                    gvPr.DataBind();
                }
                catch (Exception ex) {
                    throw ex;
                }


            }
        }



        protected void lbtnView_Click(object sender, EventArgs e) {
            int PrId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            Response.Redirect("ApproveExpensePR.aspx?PrId=" + PrId);
        }
        protected void gvPrExApp_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvPr.PageIndex = e.NewPageIndex;
               
                if (ViewState["SearchStatus"].ToString() == "1" && ViewState["SearchStatus"] != null) {
                    if (rdbMonth.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                        List<PrMasterV2> prMaster = prControllerV2.FetchPrListForAvailabiltyExpenseApprovalByDate(date);
                        gvPr.DataSource = prMaster;
                        gvPr.DataBind();
                    }
                    else {

                        //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                        //int prCode = int.Parse(newString);
                        string prCode = txtPrCode.Text;
                        List<PrMasterV2> prMaster = prControllerV2.FetchPrListForAvailabiltyExpenseApprovalByPRCode(prCode);
                        gvPr.DataSource = prMaster;
                        gvPr.DataBind();
                    }
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);

                }
                else if (ViewState["SearchStatus"].ToString() == "0" && ViewState["SearchStatus"] != null) {
                    List<PrMasterV2> prMaster = prControllerV2.FetchPrListForAvailabiltyExpenseApproval();
                    gvPr.DataSource = prMaster;
                    gvPr.DataBind();
                }
            }
            catch (Exception) {

                throw;
            }
        }
        protected void btnBasicSearch_Click(object sender, EventArgs e) {
            try {
                int SearchStatus = 1;
                ViewState["SearchStatus"] = SearchStatus;
                if (rdbMonth.Checked) {
                    DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                    List<PrMasterV2> prMaster = prControllerV2.FetchPrListForAvailabiltyExpenseApprovalByDate(date);
                    gvPr.DataSource = prMaster;
                    gvPr.DataBind();
                }
                else {

                    //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                    //int prCode = int.Parse(newString);
                    string prCode = txtPrCode.Text;

                    List<PrMasterV2> prMaster = prControllerV2.FetchPrListForAvailabiltyExpenseApprovalByPRCode(prCode);
                    gvPr.DataSource = prMaster;
                    gvPr.DataBind();
                }
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }

       
    }
}