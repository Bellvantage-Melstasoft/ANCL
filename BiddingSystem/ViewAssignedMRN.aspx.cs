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
    public partial class ViewAssignedMRN : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseControllerInterface WarehouseController = ControllerFactory.CreateWarehouseController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        SubDepartmentControllerInterface subDepartmentControllerInterface = ControllerFactory.CreateSubDepartmentController();
        MrnControllerV2 mrnControllerV2 = ControllerFactory.CreateMrnControllerV2();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefWarehouse";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabWarehouse";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewAssignedMRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewAssignedMRNLink";

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
                if (Session["UserWarehouses"] != null)
                {
                    {
                        try
                        {
                            txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");

                            
                            int SearchStatus = 0;
                            ViewState["SearchStatus"] = SearchStatus;
                            //List<MrnMasterV2> mrnList = mrnControllerV2.FetchAssignedMrnForStoreKeeperByDate(int.Parse(ViewState["UserId"].ToString()), LocalTime.Now);
                            //gvMrn.DataSource = mrnList;
                            //gvMrn.DataBind();

                            List<MrnMasterV2> mrnMaster = mrnControllerV2.FetchAssignedMrnForStoreKeeper(int.Parse(ViewState["UserId"].ToString()));
                            gvMrn.DataSource = mrnMaster;
                            gvMrn.DataBind();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }

            }
        }

        protected void lbtnView_Click(object sender, EventArgs e)
        {
            int MrnId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            Response.Redirect("ViewAssignedMRNDetails.aspx?MrnId=" + MrnId);
        }
        protected void gvAssignSK_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvMrn.PageIndex = e.NewPageIndex;
                if (ViewState["SearchStatus"].ToString() == "1" && ViewState["SearchStatus"] != null) {

                    if (rdbMonth.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                        List<MrnMasterV2> mrnList = mrnControllerV2.FetchAssignedMrnForStoreKeeperByDate(int.Parse(ViewState["UserId"].ToString()), date);
                        gvMrn.DataSource = mrnList;
                        gvMrn.DataBind();
                    }
                    else {
                        //string mrnCode = txtMrnCode.Text.Replace("MRN", "");
                        //string newString = Regex.Replace(txtMrnCode.Text, "[^.0-9]", "");
                        //int mrnCode = int.Parse(newString);
                        string mrnCode = txtMrnCode.Text;
                        MrnMasterV2 mrn = mrnControllerV2.FetchAssignedMrnForStoreKeeperByMrnCode(int.Parse(ViewState["UserId"].ToString()), mrnCode);
                        gvMrn.DataSource = new List<MrnMasterV2>() { mrn }.ToList();
                        gvMrn.DataBind();
                    }
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);

                }

                else if (ViewState["SearchStatus"].ToString() == "0" && ViewState["SearchStatus"] != null) {
                    List<MrnMasterV2> mrnList = mrnControllerV2.FetchAssignedMrnForStoreKeeper(int.Parse(ViewState["UserId"].ToString()));
                    gvMrn.DataSource = mrnList;
                    gvMrn.DataBind();
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
                    List<MrnMasterV2> mrnList = mrnControllerV2.FetchAssignedMrnForStoreKeeperByDate(int.Parse(ViewState["UserId"].ToString()), date);
                    gvMrn.DataSource = mrnList;
                    gvMrn.DataBind();
                }
                else {
                    //string mrnCode = txtMrnCode.Text.Replace("MRN", "");
                    //string newString = Regex.Replace(txtMrnCode.Text, "[^.0-9]", "");
                    //int mrnCode = int.Parse(newString);
                    string mrnCode = txtMrnCode.Text;
                    MrnMasterV2 mrn = mrnControllerV2.FetchAssignedMrnForStoreKeeperByMrnCode(int.Parse(ViewState["UserId"].ToString()), mrnCode);
                    gvMrn.DataSource = new List<MrnMasterV2>() { mrn }.ToList();
                    gvMrn.DataBind();
                }
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }
    }
}