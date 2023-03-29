using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Text.RegularExpressions;

namespace BiddingSystem {

    public partial class ViewApprovePR : System.Web.UI.Page {

        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseControllerInterface WarehouseController = ControllerFactory.CreateWarehouseController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        SubDepartmentControllerInterface subDepartmentControllerInterface = ControllerFactory.CreateSubDepartmentController();
        PrControllerV2 prControllerV2 = ControllerFactory.CreatePrControllerV2();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewApprovePr.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approvePRLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 12, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Count() > 0)
                {
                    {
                        try
                        {
                            txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                            int SearchStatus = 0;
                            ViewState["SearchStatus"] = SearchStatus;

                            int purchaseType = 1;
                            if (Session["UserId"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["prUser"].ToString()) {
                                purchaseType = 2;
                            }
                            List<PrMasterV2> prList = prControllerV2.FetchPrListforApproval((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType);
                            //List<PrMasterV2> prList = prControllerV2.FetchPrListforApprovalByDate((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType, LocalTime.Now);
                            gvPr.DataSource = prList;
                            gvPr.DataBind();
                            ///approve import for channa, created by Pasindu 2020 / 05 / 06
                            //    int purchaseType = 1;
                            //    if (Session["UserId"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["prUser"].ToString()) {
                            //        purchaseType = 2;
                            //    }

                            //List<PrMasterV2> prMaster = prControllerV2.FetchPrListforApproval((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType);
                            //gvPr.DataSource = prMaster;
                            //gvPr.DataBind();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

            }
        }
        protected void gvPr1_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvPr.PageIndex = e.NewPageIndex;
                int purchaseType = 1;
                if (Session["UserId"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["prUser"].ToString()) {
                    purchaseType = 2;
                }
                if (ViewState["SearchStatus"].ToString() == "1" && ViewState["SearchStatus"] != null) {
                    if (rdbMonth.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

                        //List<PrMasterV2> prMaster = prControllerV2.FetchPrListforApproval((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType);
                        //gvPr.DataSource = prMaster;
                        //gvPr.DataBind();

                        List<PrMasterV2> prList = prControllerV2.FetchPrListforApprovalByDate((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType, date);
                        gvPr.DataSource = prList;
                        gvPr.DataBind();
                    }
                    else {
                        //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                        //int prCode = int.Parse(newString);

                        string prCode = txtPrCode.Text;
                        PrMasterV2 pr = prControllerV2.FetchPrListforApprovalByCode((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType, prCode);
                        gvPr.DataSource = null;
                        if (pr.PrCode != null) {
                            gvPr.DataSource = new List<PrMasterV2>() { pr }.ToList();
                        }
                        gvPr.DataBind();
                    }


                }
                else if (ViewState["SearchStatus"].ToString() == "0" && ViewState["SearchStatus"] != null) {
                    //List<PrMasterV2> prList = prControllerV2.FetchPrListforApprovalByDate((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType, LocalTime.Now);
                    List<PrMasterV2> prList = prControllerV2.FetchPrListforApproval((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType);

                    gvPr.DataSource = prList;
                    gvPr.DataBind();
                }
            }
            catch (Exception) {

                throw;
            }
        }

        protected void btnBasicSearch_Click(object sender, EventArgs e) {
            try {
                int purchaseType = 1;
                if (Session["UserId"].ToString() == System.Configuration.ConfigurationSettings.AppSettings["prUser"].ToString()) {
                    purchaseType = 2;
                }
                int SearchStatus = 1;
                ViewState["SearchStatus"] = SearchStatus;
                if (rdbMonth.Checked) {
                    DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                    
                    //List<PrMasterV2> prMaster = prControllerV2.FetchPrListforApproval((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType);
                    //gvPr.DataSource = prMaster;
                    //gvPr.DataBind();

                    List<PrMasterV2> prList = prControllerV2.FetchPrListforApprovalByDate((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType, date);
                    gvPr.DataSource = prList;
                    gvPr.DataBind();
                }
                else {
                    //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                    //int prCode = int.Parse(newString);
                    string prCode = txtPrCode.Text;


                    PrMasterV2 pr = prControllerV2.FetchPrListforApprovalByCode((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), purchaseType, prCode);
                    gvPr.DataSource = null;
                    if (pr.PrCode != null) {
                        gvPr.DataSource = new List<PrMasterV2>() { pr }.ToList();
                    }
                    gvPr.DataBind();
                }
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            int PrId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            Response.Redirect("PRApproveNew.aspx?PrId=" + PrId);
        }
    }
}