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
    public partial class CompanyAddCommittee : System.Web.UI.Page
    {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
        ItemCategoryOwnerController itemCategoryOwnerController = ControllerFactory.CreateItemCategoryOwnerController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
        DesignationController designationController = ControllerFactory.CreateDesignationController();
        CommitteeController approvalCategoryTypeController = ControllerFactory.CreateApprovalCategoryTypeController();
        CommitteeController committeeController = ControllerFactory.CreateProcurementCommitteeController();
        //static string UserId = string.Empty;
        // int CompanyId = 0;
        // private string categoryName = string.Empty;
        // private string createdBy = string.Empty;
        // private string updatedBy = string.Empty;
        public static List<Committee> ListCommittee = new List<Committee>();
        public static List<Designation> listDesignation = new List<Designation>();
        public static List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
       
      //  public static List<ItemCategory> ListMaincategory = new List<ItemCategory>();
      //  public static List<CommitteeMember> ListCommitteeMember = new List<CommitteeMember>();
      //  public List<string> CategoryList = new List<string>();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefItemCategory";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabItemCategory";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyAddCommittee.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "addAddCommitteeLink";

                   // CompanyId = int.Parse(Session["CompanyId"].ToString());
                   // UserId = Session["UserId"].ToString();
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
                if (!IsPostBack)
                {
                    CompanyLoginUserList = companyLoginController.GetAllUserList();
                    LoadDesignationDropdown();
                    LoadCommitteeGridView();
                    LoadCommitteeDropdown();
                    ClearFields();
                }
                // Fix for: If gridview or part of page get refresh some times date picker doent work
                if (effectiveDate.Text != "")
                {
                    effectiveDate.Text = Convert.ToDateTime(effectiveDate.Text).ToString("yyyy-MM-dd");
                }
            }
            catch (Exception ex)
            {

            }
        }

        

        private void LoadCommitteeDropdown()
        {
            ListCommittee = committeeController.FetchAllCommittee(int.Parse(Session["CompanyId"].ToString()));
            ddlCommittee.DataSource = ListCommittee;
            ddlCommittee.DataValueField = "CommitteeId";
            ddlCommittee.DataTextField = "CommitteeName";
            ddlCommittee.DataBind();
        }
              
        private void LoadDesignationDropdown()
        {
            try
            {
                listDesignation = designationController.GetDesignationList();
                ddlDesignation.DataSource = listDesignation;
                ddlDesignation.DataValueField = "DesignationId";
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataBind();

                ddlOverRideDesignation.DataSource = listDesignation;
                ddlOverRideDesignation.DataValueField = "DesignationId";
                ddlOverRideDesignation.DataTextField = "DesignationName";
                ddlOverRideDesignation.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //----------------------Save Procurement Committee Name
        protected void btnCommitteeSave_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                if (hndCommitteeAction.Value == "Save")
                {
                    status = committeeController.ManageCommittee(0, txtCommitteeName.Text, LocalTime.Now, Convert.ToInt32(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), "Save", int.Parse(ddlLocalImport.SelectedValue));
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });window.location.reload();   </script>",
                        false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While Saving', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }
                }
                else
                {
                    status = committeeController.ManageCommittee(Convert.ToInt32(hndCommitteeEditRowId.Value), txtCommitteeName.Text, LocalTime.Now, Convert.ToInt32(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), "Update", int.Parse(ddlLocalImport.SelectedValue));
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                            "none",
                            "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); }); window.location.reload();  </script>",
                            false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While loading gridview', showConfirmButton: true,timer: 4000}); });   </script>", false);
                throw ex;
            }

        }
        
        protected void btnCommitteeMemberSave_Click(object sender, EventArgs e)
        {
            try
            {
                int status = 0;
                int OverideDesignationId = 0;
                int sequenceNo = 0;
                int committeeid = Convert.ToInt32(ddlCommittee.SelectedValue);
                int designationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                if(divSequence.Visible == true)
                {
                    sequenceNo = Convert.ToInt32(txtSequence.Text);
                }
                int allowedApprovedCount = Convert.ToInt32(txtAllowedApprovalCount.Text);
                int canOveride = Convert.ToInt32(ddlOveride.SelectedValue);
                if (canOveride == 1)
                {
                    OverideDesignationId = Convert.ToInt32(ddlOverRideDesignation.SelectedValue);
                }
                DateTime effectiveDatet = Convert.ToDateTime(effectiveDate.Text);
               
                if (hndCommitteeMemberAction.Value == "Save")
                {
                   
                    status = committeeController.ManageCommitteeMember(0, committeeid, designationId, sequenceNo, allowedApprovedCount, canOveride, OverideDesignationId, effectiveDatet, Convert.ToInt32(Session["UserId"].ToString()) , LocalTime.Now ,"Save");
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>",
                        false);
                        RefreshGrid();
                        ClearFields();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While Saving', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }
                }
                else
                {
                    status = committeeController.ManageCommitteeMember(Convert.ToInt32(hdnCommitteeMemberEditRowId.Value), committeeid, designationId, sequenceNo, allowedApprovedCount, canOveride, OverideDesignationId, effectiveDatet, Convert.ToInt32(Session["UserId"].ToString()), LocalTime.Now, "Update");
                    if (status > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                            "none",
                            "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); }); </script>",
                            false);
                        RefreshGrid();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While loading gridview', showConfirmButton: true,timer: 4000}); });   </script>", false);
                throw ex;
            }

        }

        private void RefreshGrid()
        {
            LoadCommitteeGridView();
        }

        //-----------------------Load Gv Data
        private void LoadCommitteeGridView()
        {
            try
            {
                ListCommittee = committeeController.FetchAllCommittee(int.Parse(Session["CompanyId"].ToString()));
                gvCommittee.DataSource = ListCommittee;
                gvCommittee.DataBind();
                gvCommitteeMemberMaster.DataSource = ListCommittee;
                gvCommitteeMemberMaster.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While loading gridview', showConfirmButton: true,timer: 4000}); });   </script>", false);
                throw ex;
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



        protected void gvCommittee_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCommittee.PageIndex = e.NewPageIndex;
                LoadCommitteeGridView();
            }
            catch (Exception)
            {

            }
        }

        protected void lnkBtnCommitteeDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int save = 0;
                save = committeeController.DeleteCommittee(Convert.ToInt32(hndCommitteeDeleteRowId.Value));
                if (save > 0)
                {
                    DisplayMessage("Changes saved successfully", false);
                    RefreshGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });window.location.reload();   </script>", false);

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected void lnkBtnCommitteeMemberDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int save = 0;
                save = committeeController.DeleteCommitteeMember(Convert.ToInt32(hdnCommitteeMemberDeleteRowId.Value));
                if (save > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });window.location.reload();   </script>",
                        false);
                    RefreshGrid();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }


        protected void gvCommitteeMemberMaster_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    List<CommitteeMember> ListCommitteeMember = new List<CommitteeMember>();

                    string committeeId = gvCommitteeMemberMaster.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvCommitteeMember = e.Row.FindControl("gvCommitteeMember") as GridView;
                    List<CommitteeMember> committeeMember = new List<CommitteeMember>();
                    ListCommitteeMember = committeeController.FetchAllCommitteeMembers();
                    ListCommitteeMember = ListCommitteeMember.OrderBy(x => x.CommitteeId).ToList();
                    committeeMember = ListCommitteeMember.FindAll(x => x.CommitteeId == Convert.ToInt32(committeeId));
                    gvCommitteeMember.DataSource = committeeMember;
                    gvCommitteeMember.DataBind();
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void gvCommitteeMemberMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCommitteeMemberMaster.PageIndex = e.NewPageIndex;
                LoadCommitteeGridView();
            }
            catch (Exception)
            {

            }
        }

        protected void ddlOveride_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOveride.SelectedValue == "0") {
                divOverideDesignation.Style.Add("display", "none");
            } else {
                divOverideDesignation.Style.Add("display", "block");
            }
        }

        

        private void ClearFields()
        {
            ddlOveride.SelectedValue = "0";
            effectiveDate.Text = "";
            hdnCommitteeMemberEditRowId.Value = "";
        }

        protected void btnCommitteeMemberClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            btnCommitteeMemberSave.Text = "Save";
        }

    }
   
}

