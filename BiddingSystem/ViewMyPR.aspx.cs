using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;

namespace BiddingSystem {
    public partial class ViewMyPR : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseControllerInterface WarehouseController = ControllerFactory.CreateWarehouseController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        SubDepartmentControllerInterface subDepartmentControllerInterface = ControllerFactory.CreateSubDepartmentController();
        PrControllerV2 prControllerV2 = ControllerFactory.CreatePrControllerV2();
        PRDetailsStatusLogController prDetailStatusLogController = ControllerFactory.CreatePRDetailsStatusLogController();

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewMyPR.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewPRLink";

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

                txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                try {
                    BindDepartment();
                    BindWarehouses();
                    LoadDDLMainCatregory();
                    int SearchStatus = 0;
                    ViewState["SearchStatus"] = SearchStatus;

                    List<PrMasterV2> prList = prControllerV2.FetchMyPrByBasicSearchByMonth(int.Parse(ViewState["UserId"].ToString()), LocalTime.Now);
                    //List<PrMasterV2> prList = prControllerV2.FetchMyPr(int.Parse(ViewState["UserId"].ToString()));
                    
                    gvPr.DataSource = prList;
                    gvPr.DataBind();
                }
                catch (Exception ex) {
                    throw ex;
                }

            }

        }

        private void BindWarehouses() {
            try {
                ddlWarehouse.DataSource = WarehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                ddlWarehouse.DataValueField = "WarehouseId";
                ddlWarehouse.DataTextField = "Location";
                ddlWarehouse.DataBind();
                ddlWarehouse.Items.Insert(0, new ListItem("Select Warehouse", ""));
            }
            catch (Exception) {

                throw;
            }
        }

        private void BindDepartment() {
            try {
                ddlDepartment.DataSource = subDepartmentControllerInterface.getUserSubDepartmentByUserId(int.Parse(Session["UserId"].ToString()));
                ddlDepartment.DataValueField = "SubDepartmentID";
                ddlDepartment.DataTextField = "SubDepartmentName";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select Department", ""));

            }
            catch (Exception) {

                throw;
            }
        }

        private void LoadDDLMainCatregory() {
            try {
                ddlMainCategory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                ddlMainCategory.DataValueField = "CategoryId";
                ddlMainCategory.DataTextField = "CategoryName";
                ddlMainCategory.DataBind();
                ddlMainCategory.Items.Insert(0, new ListItem("Select Main Category", ""));
            }
            catch (Exception ex) {
            }
        }

        protected void ddlMainCategory_SelectedIndexChanged(object sender, EventArgs e) {
            try {
                if (int.Parse(ddlMainCategory.SelectedValue) != 0 || ddlMainCategory.SelectedValue != "") {
                    int mainCategoryId = int.Parse(ddlMainCategory.SelectedValue);
                    ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(mainCategoryId, int.Parse(Session["CompanyId"].ToString()));
                    ddlSubCategory.DataTextField = "SubCategoryName";
                    ddlSubCategory.DataValueField = "SubCategoryId";
                    ddlSubCategory.DataBind();
                    ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#advancedSearch').collapse('show'); });   </script>", false);

                }

            }
            catch (Exception ex) {

            }
        }
        
        protected void lbtnView_Click(object sender, EventArgs e) {
            int PrId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            Response.Redirect("ViewPRNew.aspx?PrId=" + PrId);
        }

        protected void btnBasicSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int SearchStatus = 1;
                ViewState["SearchStatus"] = SearchStatus;
                if (rdbMonth.Checked)
                {
                    DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                    List<PrMasterV2> prList = prControllerV2.FetchMyPrByBasicSearchByMonth(int.Parse(ViewState["UserId"].ToString()), date);
                    gvPr.DataSource = prList;
                    gvPr.DataBind();
                }
                else
                {
                    PrMasterV2 pr = prControllerV2.FetchMyPrByBasicSearchByPrCode(int.Parse(ViewState["UserId"].ToString()), txtPrCode.Text);
                    gvPr.DataSource = null; 
                    if (pr.PrCode != null)
                    {
                        gvPr.DataSource = new List<PrMasterV2>() { pr }.ToList();
                    }
                    gvPr.DataBind();
                }
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }

        protected void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int SearchStatus = 2;
                ViewState["SearchStatus"] = SearchStatus;
                List<int> DepartmentIds = new List<int>();
                if (chkDepartment.Checked) {
                    for (int i = 0; i < ddlDepartment.Items.Count; i++)
                    {
                        if (ddlDepartment.Items[i].Selected)
                        {
                            DepartmentIds.Add(int.Parse(ddlDepartment.Items[i].Value));
                        }
                    }
                }
                List<int> WareHouseIds = new List<int>();
                if (chkWarehouse.Checked)
                {
                    for (int i = 0; i < ddlWarehouse.Items.Count; i++)
                    {
                        if (ddlWarehouse.Items[i].Selected)
                        {
                            WareHouseIds.Add(int.Parse(ddlWarehouse.Items[i].Value));
                        }
                    }
                }
                int purchaseType = 0;
                if (chlPurchaseType.Checked) {
                    purchaseType = Convert.ToInt32(ddlPurchaseType.SelectedValue);
                }
                int purchaseProcedure = 0;
                if (chkPurchaseProcedure.Checked) {
                    purchaseProcedure = Convert.ToInt32(ddlPurchaseProcedure.SelectedValue);
                }
                string CreatedFromDate = string.Empty;
                string CreatedToDate = string.Empty;
                if (chkCreatedDate.Checked) {
                    CreatedFromDate = txtStartCreatdDate.Text;
                    CreatedToDate = txtEndCreatedDate.Text;
                }
                string ExpectedFromDate = string.Empty;
                string ExpectedToDate = string.Empty;
                if (chkExpectedDate.Checked) {
                    ExpectedFromDate = txtStartExpectedDate.Text;
                    ExpectedToDate = txtEndExpectedDate.Text;
                }
                int expenseType = 0;
                if (chkExpenseType.Checked) {
                    expenseType = Convert.ToInt32(ddlExpenseType.SelectedValue); 
                }
                int prType = 0;
                if (chkPrType.Checked) {
                    prType = Convert.ToInt32(ddlPrType.SelectedValue);
                }
                int mainCatergoryId = 0;
                int subCatergoryId = 0;
                if (chkMaincategory.Checked) {
                    mainCatergoryId = Convert.ToInt32(ddlMainCategory.SelectedValue);
                    subCatergoryId = Convert.ToInt32(ddlSubCategory.SelectedValue);
                }

                List<PrMasterV2> prList = prControllerV2.FetchMyPrByAdvanceSearch(
                    int.Parse(ViewState["UserId"].ToString()), 
                    DepartmentIds,
                    WareHouseIds,
                    purchaseType,
                    purchaseProcedure,
                    CreatedFromDate,
                    CreatedToDate,
                    ExpectedFromDate,
                    ExpectedToDate,
                    expenseType,
                    prType,
                    mainCatergoryId,
                    subCatergoryId);
                gvPr.DataSource = prList;
                gvPr.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#advancedSearch').collapse('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }

        protected void btnLog_Click(object sender, EventArgs e)
        {
            int PrId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            List<PrDetailsV2> prDetails = prControllerV2.FetchPrDetailsList(PrId, int.Parse(Session["CompanyId"].ToString()));
            gvPrDetails.DataSource = prDetails;
            gvPrDetails.DataBind();
            List<PrUpdateLog> prUpdatedLog = prControllerV2.FetchPrUpdateLog(PrId);
            gvUpdatedLog.DataSource = prUpdatedLog;
            gvUpdatedLog.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlViewPr').modal('show'); });   </script>", false);
        }
        protected void gvPr_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvPr.PageIndex = e.NewPageIndex;
                if (ViewState["SearchStatus"].ToString() == "1" && ViewState["SearchStatus"] != null) {

                    if (rdbMonth.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                        List<PrMasterV2> prList = prControllerV2.FetchMyPrByBasicSearchByMonth(int.Parse(ViewState["UserId"].ToString()), date);
                        gvPr.DataSource = prList;
                        gvPr.DataBind();
                    }
                    else {
                        PrMasterV2 pr = prControllerV2.FetchMyPrByBasicSearchByPrCode(int.Parse(ViewState["UserId"].ToString()), txtPrCode.Text);
                        gvPr.DataSource = null;
                        if (pr.PrCode != null) {
                            gvPr.DataSource = new List<PrMasterV2>() { pr }.ToList();
                        }
                        gvPr.DataBind();
                    }
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);

                }
                else if (ViewState["SearchStatus"].ToString() == "2" && ViewState["SearchStatus"] != null) {
                    List<int> DepartmentIds = new List<int>();
                    if (chkDepartment.Checked) {
                        for (int i = 0; i < ddlDepartment.Items.Count; i++) {
                            if (ddlDepartment.Items[i].Selected) {
                                DepartmentIds.Add(int.Parse(ddlDepartment.Items[i].Value));
                            }
                        }
                    }
                    List<int> WareHouseIds = new List<int>();
                    if (chkWarehouse.Checked) {
                        for (int i = 0; i < ddlWarehouse.Items.Count; i++) {
                            if (ddlWarehouse.Items[i].Selected) {
                                WareHouseIds.Add(int.Parse(ddlWarehouse.Items[i].Value));
                            }
                        }
                    }
                    int purchaseType = 0;
                    if (chlPurchaseType.Checked) {
                        purchaseType = Convert.ToInt32(ddlPurchaseType.SelectedValue);
                    }
                    int purchaseProcedure = 0;
                    if (chkPurchaseProcedure.Checked) {
                        purchaseProcedure = Convert.ToInt32(ddlPurchaseProcedure.SelectedValue);
                    }
                    string CreatedFromDate = string.Empty;
                    string CreatedToDate = string.Empty;
                    if (chkCreatedDate.Checked) {
                        CreatedFromDate = txtStartCreatdDate.Text;
                        CreatedToDate = txtEndCreatedDate.Text;
                    }
                    string ExpectedFromDate = string.Empty;
                    string ExpectedToDate = string.Empty;
                    if (chkExpectedDate.Checked) {
                        ExpectedFromDate = txtStartExpectedDate.Text;
                        ExpectedToDate = txtEndExpectedDate.Text;
                    }
                    int expenseType = 0;
                    if (chkExpenseType.Checked) {
                        expenseType = Convert.ToInt32(ddlExpenseType.SelectedValue);
                    }
                    int prType = 0;
                    if (chkPrType.Checked) {
                        prType = Convert.ToInt32(ddlPrType.SelectedValue);
                    }
                    int mainCatergoryId = 0;
                    int subCatergoryId = 0;
                    if (chkMaincategory.Checked) {
                        mainCatergoryId = Convert.ToInt32(ddlMainCategory.SelectedValue);
                        subCatergoryId = Convert.ToInt32(ddlSubCategory.SelectedValue);
                    }

                    List<PrMasterV2> prList = prControllerV2.FetchMyPrByAdvanceSearch(
                        int.Parse(ViewState["UserId"].ToString()),
                        DepartmentIds,
                        WareHouseIds,
                        purchaseType,
                        purchaseProcedure,
                        CreatedFromDate,
                        CreatedToDate,
                        ExpectedFromDate,
                        ExpectedToDate,
                        expenseType,
                        prType,
                        mainCatergoryId,
                        subCatergoryId);
                    gvPr.DataSource = prList;
                    gvPr.DataBind();

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#advancedSearch').collapse('show'); });   </script>", false);

                }
                else if (ViewState["SearchStatus"].ToString() == "0" && ViewState["SearchStatus"] != null) {
                    List<PrMasterV2> prList = prControllerV2.FetchMyPrByBasicSearchByMonth(int.Parse(ViewState["UserId"].ToString()), LocalTime.Now);
                    gvPr.DataSource = prList;
                    gvPr.DataBind();
                }
                }
            catch (Exception) {

                throw;
            }
        }
        protected void gvPrDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PrDId = int.Parse(e.Row.Cells[2].Text);
                GridView gvStatusLog = e.Row.FindControl("gvStatusLog") as GridView;
                gvStatusLog.DataSource = prDetailStatusLogController.PrLogDetails(PrDId);
                gvStatusLog.DataBind();
            }

        }


    }
}