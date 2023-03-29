using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data;

namespace BiddingSystem
{
    public partial class CompanyAddMainCategory : System.Web.UI.Page
    {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
        DesignationController designationController = ControllerFactory.CreateDesignationController();
        CommitteeController approvalCategoryTypeController = ControllerFactory.CreateApprovalCategoryTypeController();
        CommitteeController committeeController = ControllerFactory.CreateProcurementCommitteeController();
        // static string UserId = string.Empty;
        // int CompanyId = 0;
        // static int categoryId = 0;
        // private string categoryName = string.Empty;
        //  private string createdBy = string.Empty;
        // private string updatedBy = string.Empty;

        public static List<ItemCategory> getAllMaincategoryList = new List<ItemCategory>();
        public List<string> CategoryList = new List<string>();
        public static List<Designation> listDesignation = new List<Designation>();
        public static List<RefApprovalTye> ListRefApprovalTye = new List<RefApprovalTye>();
        public static List<RefLimitFor> ListRefLimitFor = new List<RefLimitFor>();
        public static List<ItemCategory> itemCategory = new List<ItemCategory>();
     //   public static List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
     //  public static List<Committee> ListCommittee = new List<Committee>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefItemCategory";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabItemCategory";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyAddMainCategory.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "addMainCategoryLink";

                   // CompanyId = int.Parse(Session["CompanyId"].ToString());
                 //   UserId = Session["UserId"].ToString();                   

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 4, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
                msg.Visible = false;
                LoadAllClassObjects();
                
                if (!IsPostBack)
                {
                    LoadMainCategoryDetails();                    
                    foreach (var item in getAllMaincategoryList)
                    {
                        CategoryList.Add(item.CategoryName);
                    }
                    List<CompanyLogin> CompanyLoginUserList = companyLoginController.GetAllUserList();
                    LoadDesignationDropdown();   
                    LoadLimitForDropdown();
                    LoadApprovalTypeDropdown();
                    LoadCommitteeDropdown();
                    ClearFields();
                }

                // Fix for: If gridview or part of page get refresh some times date picker doent work
                if (effectiveDate.Text != "")
                {
                    effectiveDate.Text = Convert.ToDateTime(effectiveDate.Text).ToString("yyyy-MM-dd");
                }
            }
            catch (Exception)
            {

            }
        }

        private void LoadAllClassObjects()
        {
            getAllMaincategoryList = new List<ItemCategory>();
            ListRefApprovalTye = new List<RefApprovalTye>();
            ListRefLimitFor = new List<RefLimitFor>();
            listDesignation = new List<Designation>();
            getAllMaincategoryList = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
            ListRefApprovalTye.Add(new RefApprovalTye { Id = 1, Description = "Initiated User" });
            ListRefApprovalTye.Add(new RefApprovalTye { Id = 2, Description = "Committee" });
            ListRefApprovalTye.Add(new RefApprovalTye { Id = 3, Description = "Any One User" });
            ListRefLimitFor.Add(new RefLimitFor { Id = 1, Description = "Quotation Recommendation" });
            ListRefLimitFor.Add(new RefLimitFor { Id = 2, Description = "Quotation Approval" });
            listDesignation = designationController.GetDesignationList();
        }

        private void LoadLimitForDropdown()
        {

            ddlLimitFor.DataSource = ListRefLimitFor;
            ddlLimitFor.DataValueField = "Id";
            ddlLimitFor.DataTextField = "Description";
            ddlLimitFor.DataBind();
        }

        private void LoadApprovalTypeDropdown()
        {
            ddlApprovalType.DataSource = ListRefApprovalTye;
            ddlApprovalType.DataValueField = "Id";
            ddlApprovalType.DataTextField = "Description";
            ddlApprovalType.DataBind();
        }

        private void LoadDesignationDropdown()
        {
            try
            {                
                ddlOverideDesignation.DataSource = listDesignation;
                ddlOverideDesignation.DataValueField = "DesignationId";
                ddlOverideDesignation.DataTextField = "DesignationName";
                ddlOverideDesignation.DataBind();

                ddlAnyUserDesignation.DataSource = listDesignation;
                ddlAnyUserDesignation.DataValueField = "DesignationId";
                ddlAnyUserDesignation.DataTextField = "DesignationName";
                ddlAnyUserDesignation.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
   
        private void LoadCommitteeDropdown()
        {
            List<Committee> ListCommittee = committeeController.FetchAllCommittee(int.Parse(Session["CompanyId"].ToString()));
            ddlCommittee.DataSource = ListCommittee;
            ddlCommittee.DataValueField = "CommitteeId";
            ddlCommittee.DataTextField = "CommitteeName";
            ddlCommittee.DataBind();
        }

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }

        protected void btnEdit_Click(object sender, EventArgs e) {
            int limitFor = int.Parse(LimitFor.Value);
            decimal MaxAmount = decimal.Parse(MaximumAmount.Value);
            decimal MinAmount = decimal.Parse(MinimumAmount.Value);
            int category = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[1].Text);

            int result = ControllerFactory.CreateItemCategoryController().GetApprovalLimits(category, limitFor, MaxAmount, MinAmount);
            if (result == 1) {
                txtMaximumValue.Enabled = false;
                txtMinimumValue.Enabled = true;
            }
            else if (result == 2) {
                txtMinimumValue.Enabled = false;
                txtMaximumValue.Enabled = true;
            }
             else if (result == 3) {
                txtMaximumValue.Enabled = false;
                txtMinimumValue.Enabled = false;
            }
        }
            
        //----------------------Save Main Category
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                int chkBoxIsActive = 0;
                int save = 0;

                if (chkIsavtive.Checked)
                {
                    chkBoxIsActive = 1;
                }
                int overideDesignationId = 0;
                int AnyUserDesignationId = 0;
                int committeeId = 0;
                DateTime effectiveDatet = LocalTime.Now;
                string categoryName = txtMainCategoryName.Text;
                int LimitFor = Convert.ToInt32(ddlLimitFor.SelectedValue);
                int approvalType = Convert.ToInt32(ddlApprovalType.SelectedValue);
                if (approvalType == 2)
                {
                    committeeId = Convert.ToInt32(ddlCommittee.SelectedValue);
                    if (effectiveDate.Text != "")
                    {
                        effectiveDatet = Convert.ToDateTime(effectiveDate.Text);
                    }
                }
                else if (approvalType == 3)
                {
                    AnyUserDesignationId = Convert.ToInt32(ddlAnyUserDesignation.SelectedValue);
                    if (effectiveDate.Text != "")
                    {
                        effectiveDatet = Convert.ToDateTime(effectiveDate.Text);
                    }
                }
                int allowedApprovedCount = Convert.ToInt32(txtAllowedApprovalCount.Text);
                int canOveride = Convert.ToInt32(ddlOveride.SelectedValue);
                if (canOveride == 1)
                {
                    overideDesignationId = Convert.ToInt32(ddlOverideDesignation.SelectedValue);
                }
                decimal minimumValue = Convert.ToDecimal(txtMinimumValue.Text);
                decimal maximumValue = Convert.ToDecimal(txtMaximumValue.Text);

                if (hndAction.Value == "Save")
                {
                    int categoryId = itemCategoryMasterController.ManageItemCategory(categoryId = 0, txtMainCategoryName.Text, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), chkBoxIsActive, int.Parse(Session["CompanyId"].ToString()), "Save");
                    save = itemCategoryApprovalController.ManageItemCategoryApprovalLimit(0, categoryId, LimitFor, approvalType, allowedApprovedCount, canOveride, overideDesignationId, minimumValue, maximumValue, committeeId, AnyUserDesignationId, effectiveDatet, Convert.ToInt32(Session["UserId"].ToString()), LocalTime.Now, "Save", int.Parse(Session["DesignationId"].ToString()), int.Parse(ddlLocalImport.SelectedValue));

                    if (save > 0)
                    {

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>",
                        false);
                        LoadMainCategoryDetails();
                        ClearFields();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While Saving', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }

                }
                else
                {
                    int categoryId = itemCategoryMasterController.ManageItemCategory(Convert.ToInt32(hndCategoryId.Value), txtMainCategoryName.Text, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), chkBoxIsActive, int.Parse(Session["CompanyId"].ToString()), "Update");
                    save = itemCategoryApprovalController.ManageItemCategoryApprovalLimit(Convert.ToInt32(hndLimitId.Value), Convert.ToInt32(hndCategoryId.Value), LimitFor, approvalType, allowedApprovedCount, canOveride, overideDesignationId, minimumValue, maximumValue, committeeId, AnyUserDesignationId, effectiveDatet, Convert.ToInt32(Session["UserId"].ToString()), LocalTime.Now, "Update", int.Parse(Session["DesignationId"].ToString()), int.Parse(ddlLocalImport.SelectedValue));
                    if (save > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                                "none",
                                "<script>    " +
                                "$(document).ready(function () { " +
                                " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });  " +
                                " </script>",
                                false);
                        LoadMainCategoryDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While Saving', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        //-----------------------Load Gv Data
        private void LoadMainCategoryDetails()
        {
            try
            {
                itemCategory = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));               
                gvMainCategory.DataSource = itemCategory;
                gvMainCategory.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //-------------------Edit Saved Data
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                //categoryId = int.Parse(gvMainCategory.Rows[x].Cells[1].Text);
                //categoryName = gvMainCategory.Rows[x].Cells[2].Text;
                //createdDate = DateTime.Parse(gvMainCategory.Rows[x].Cells[3].Text);
                //createdBy = gvMainCategory.Rows[x].Cells[4].Text;
                //updatedDate = DateTime.Parse(gvMainCategory.Rows[x].Cells[5].Text);
                //updatedBy = gvMainCategory.Rows[x].Cells[6].Text;
                //isActive = int.Parse(gvMainCategory.Rows[x].Cells[7].Text);
                //txtMainCategoryName.Text = categoryName;
                //HiddenField1.Value = categoryId.ToString();
                //if (isActive == 1)
                //{
                //    chkIsavtive.Checked = true;
                //}
                //else
                //{
                //    chkIsavtive.Checked = false;
                //}
                //btnSave.Text = "Update";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { document.body.scrollTop = 50; document.documentElement.scrollTop = 50;});   </script>", false);

            }
            catch (Exception ex)
            {


            }
        }

        //-----------------Deactivate Saved Data
        protected void lnkBtnDeactivate_Click(object sender, EventArgs e)
        {
            try
            {

                int updateStatus = 0;
                string categoryId = hdnMainCatecoryId.Value;
                if (categoryId != "" && categoryId != null && hdnStatus.Value != "" && hdnStatus.Value != null)
                {

                    if (hdnStatus.Value == "Yes")
                    {
                        updateStatus = itemCategoryController.UpdateItemCategoryStatus(int.Parse(Session["CompanyId"].ToString()), int.Parse(categoryId), 0);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                    if (hdnStatus.Value == "No")
                    {
                        updateStatus = itemCategoryController.UpdateItemCategoryStatus(int.Parse(Session["CompanyId"].ToString()), int.Parse(categoryId), 1);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }


                    if (updateStatus > 0)
                    {
                        ClearFields();
                        hdnMainCatecoryId.Value = null;
                        hdnStatus.Value = null;
                        LoadMainCategoryDetails();

                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please select Category!!', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int delete = 0;
                string categoryId = hdnMainCatecoryId.Value;
                if (categoryId != "" && categoryId != null)
                {
                    delete = itemCategoryMasterController.DeleteItemCategory(int.Parse(Session["CompanyId"].ToString()) , Convert.ToInt32(categoryId));
                    
                    if (delete > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        DisplayMessage("Category has been deleted successfully", false);
                        ClearFields();
                        hdnMainCatecoryId.Value = null;
                        hdnStatus.Value = null;
                        LoadMainCategoryDetails();
                        txtMinimumValue.Enabled = true;
                        txtMaximumValue.Enabled = true;
                    }else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Category Doest Exist!', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please select Category!!', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            txtMinimumValue.Text = "1";
            txtMaximumValue.Text = "";
        }

        private void ClearFields()
        {
           // txtMainCategoryName.Text = "";
            chkIsavtive.Checked = true;
            txtFindCategortName.Text = "";
            btnSave.Text = "Save";
            ddlLimitFor.SelectedValue = "1";
            ddlLimitFor.SelectedValue = "1";
            ddlApprovalType.SelectedValue = "1";
            txtAllowedApprovalCount.Text = "1";
            ddlOveride.SelectedValue = "0";
            ddlApprovalType.SelectedValue = "1";
            divSelectCommittee.Style.Add("display", "none");
            divEffectiveDate.Style.Add("display", "none");
            divSelectDesignation.Style.Add("display", "none");
            divOverideDesignation.Style.Add("display", "none");
            hndCategoryId.Value = "";
        }

        protected void lnkLimitBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int delete = 0;
                string limitId = hndLimitId.Value;
                if (limitId != "" && limitId != null)
                {
                    delete = itemCategoryApprovalController.DeleteItemCategoryApprovalLimitDetials(Convert.ToInt32(limitId));

                    if (delete > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>",
                        false);
                        LoadMainCategoryDetails();
                        DisplayMessage("Changes saved successfully", false);
                        ClearFields();
                        hdnMainCatecoryId.Value = null;
                        hdnStatus.Value = null;
                        txtMinimumValue.Enabled = true;
                        txtMaximumValue.Enabled = true;
                        txtMinimumValue.Text = "";
                        txtMaximumValue.Text = "";
                        hndAction.Value = "Save";
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While Deleting', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please select Category!!', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void DisplayMessage(string message, bool isError)
        {
            msg.Visible = true;
            if (isError)
            {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else
            {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }

        public string getJsonCategoryList()
        {
            var DataList = CategoryList;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }
        
        protected void gvMainCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvMainCategory.PageIndex = e.NewPageIndex;
                gvMainCategory.DataSource = itemCategory;
                gvMainCategory.DataBind();
            }
            catch (Exception)
            {

            }
        }
        
        protected void btnTake_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int categoryId = int.Parse(gvCategoryList.Rows[x].Cells[0].Text);
                ItemCategoryMaster itemCategoryMasterObj = itemCategoryMasterController.FetchItemCategoryListByIdObj(categoryId);

                int saveItemCategory = itemCategoryController.SaveItemCategory(int.Parse(Session["CompanyId"].ToString()), categoryId, itemCategoryMasterObj.CategoryName, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), 1);
                if (saveItemCategory > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //DisplayMessage("Category has been clone successfully", false);
                    LoadMainCategoryDetails();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on cloning category', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    //DisplayMessage("Error on cloning category", true);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        protected void btnSearch_Click1(object sender, EventArgs e)
        {
            try
            {
                List<ItemCategoryMaster> fetchMasterCategories = itemCategoryMasterController.searchCategoryName(txtFindCategortName.Text, int.Parse(Session["CompanyId"].ToString()));

                gvCategoryList.DataSource = fetchMasterCategories;
                gvCategoryList.DataBind();

                if (gvCategoryList.Rows.Count == 0)
                {
                    txtMainCategoryName.Text = txtFindCategortName.Text;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private DataTable GetApprovalLimitData(int categoryId, int type)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[15] {
                    new DataColumn("LIMIT_ID"),
                    new DataColumn("CATEGORY_ID"),
                    new DataColumn("MINIMUM_AMOUNT"),
                    new DataColumn("MAXIMUM_AMOUNT"),
                    new DataColumn("LIMIT_FOR"),
                    new DataColumn("APPROVAL_TYPE"),
                    new DataColumn("committeeName"),
                    new DataColumn("APPROVAL_COUNT") ,
                    new DataColumn("CAN_OVERIDE"),
                    new DataColumn("OVERIDE_DESIGNATION") ,
                    new DataColumn("effectiveDate") ,
                    new DataColumn("effectiveFrom"),
                    new DataColumn("DESIGNATION_ID"),
                    new DataColumn("COMMITTEE_ID"),
                    new DataColumn("LIMIT_TYPE")
                });
                if (type == 1) {
                    dt = itemCategoryController.FetchItemCategoryApprovalLimits(categoryId);
                }
                if (type == 2) {
                    dt = itemCategoryController.FetchItemCategoryApprovalLimitsImport(categoryId);
                }
                foreach (DataRow row in dt.Rows)
                {
                     if(row["effectiveDate"].ToString() == "")
                    {
                        row["effectiveDate"] = row["effectiveFrom"];
                    }
                    if (row["committeeName"].ToString() == "")
                    {
                        row["committeeName"] =  ListRefApprovalTye.Find(x => x.Id == Convert.ToInt32(row["APPROVAL_TYPE"].ToString())).Description;

                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new DataTable();
        }
        
        protected void OnRowDataBound1(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    string categoryId = gvMainCategory.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvApprovalLimits = e.Row.FindControl("gvApprovalLimits") as GridView;
                    
                    gvApprovalLimits.DataSource = GetApprovalLimitData(Convert.ToInt32(categoryId), 1);
                    gvApprovalLimits.DataBind();

                    GridView gvApprovalLimitsImport = e.Row.FindControl("gvApprovalLimitsImport") as GridView;

                    gvApprovalLimitsImport.DataSource = GetApprovalLimitData(Convert.ToInt32(categoryId), 2);
                    gvApprovalLimitsImport.DataBind();




                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void ddlApprovalType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlApprovalType.SelectedValue == "2")
            {
                divSelectCommittee.Style.Add("display", "block"); 
                divEffectiveDate.Style.Add("display", "block");
                divSelectDesignation.Style.Add("display", "none");
                txtAllowedApprovalCount.ReadOnly = false;
            }
            else if (ddlApprovalType.SelectedValue == "3")
            {
                divSelectDesignation.Style.Add("display", "block");
                divSelectCommittee.Style.Add("display", "none");
                divEffectiveDate.Style.Add("display", "block");
                txtAllowedApprovalCount.Text = "1";
                txtAllowedApprovalCount.ReadOnly = true;
            }
            else
            {
                divSelectCommittee.Style.Add("display", "none");
                divEffectiveDate.Style.Add("display", "none");
                divSelectDesignation.Style.Add("display", "none");
                txtAllowedApprovalCount.Text = "1";
                txtAllowedApprovalCount.ReadOnly = true;
            }
        }

        protected void ddlOveride_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOveride.SelectedValue == "0")
            {
                divOverideDesignation.Style.Add("display", "none");
            }
            else
            {
                divOverideDesignation.Style.Add("display", "block");
            }
        }
    }

    public class RefApprovalTye
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class RefLimitFor
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
    
    // used to send json object from clienside to webmethod 
    public class ApprovalLimit
    {
        public int ApproveLimitId { get; set; }  // temporry Id when adding new catergory
        public int TypeId { get; set; } // use at frontend
        public int ApprovalLimitFrom { get; set; }
        public int ApprovalLimitTo { get; set; }
        public List<CommitteeMember> AssignedDesignation { get; set; }        
    }   

    
}