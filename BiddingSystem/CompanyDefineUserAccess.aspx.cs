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

namespace BiddingSystem
{
    public partial class CompanyDefineUserAccess : System.Web.UI.Page
    {
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        UserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
        RoleFunctionController roleFunctionController = ControllerFactory.CreateRoleFunctionController();
        FunctionActionController functionActionController = ControllerFactory.CreateFunctionActionController();
        SystemDivisionController systemDivisionController = ControllerFactory.CreateSystemDivisionController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        List<UserRole> getAllUserRolesList = new List<UserRole>();
        List<FunctionAction> getAllActionList = new List<FunctionAction>();
        UserRole userRoleObj = new UserRole();
        string UserId = string.Empty;
        static int CompanyId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefUserCreation";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabUserCreation";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyDefineUserAccess.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "defineUserAccessLink";

                    CompanyId = int.Parse(Session["CompanyId"].ToString());
                    UserId = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 2, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                    ddlUserRoles.Enabled = false;

                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
                msg.Visible = false;

                if (!IsPostBack)
                {
                    LoadCompanyUsers();
                    LoadUserRoles();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }


        //-----------------------Load Gv Data
        private void LoadUserRoles()
        {
            try
            {
                ddlUserRoles.DataSource = userRoleController.FetchUserRole(int.Parse(Session["CompanyId"].ToString()));
                ddlUserRoles.DataTextField = "userRoleName";
                ddlUserRoles.DataValueField = "userRoleId";
                ddlUserRoles.DataBind();
                ddlUserRoles.Items.Insert(0, new ListItem("Select User Role", "0"));
            }
            catch (Exception ex)
            {

            }
        }
        
        private void LoadCompanyUsers()
        {
            try
            {
                ddlCompanyUsers.DataSource = companyLoginController.GetUserListByDepartmentid(CompanyId).Where(x => x.UserId != int.Parse(UserId)).ToList();
                ddlCompanyUsers.DataTextField = "FirstName";
                ddlCompanyUsers.DataValueField = "UserId";
                ddlCompanyUsers.DataBind();
                ddlCompanyUsers.Items.Insert(0, new ListItem("Select User", "0"));
            }
            catch (Exception)
            {

            }
        }
        
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFiled();
        }

        private void ClearFiled()
        {
            ddlCompanyUsers.SelectedIndex = 0;
            ddlUserRoles.SelectedIndex = 0;
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

        protected void ddlCompanyUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCompanyUsers.SelectedIndex != 0)
            {

                List<CompanyUserAccess> GetCompanyUserRole = companyUserAccessController.FetchCompanyUserAccessByUserId(int.Parse(ddlCompanyUsers.SelectedValue), CompanyId);

                if (GetCompanyUserRole.Count() > 0)
                {
                    int userRoleId = GetCompanyUserRole.FirstOrDefault().userRoleID;
                    if (userRoleId > 0)
                    {
                        ddlUserRoles.Enabled = false;
                        ddlUserRoles.SelectedValue = userRoleId.ToString();
                    }

                    List<RoleFunction> GetRoleFunctionByRoleId = roleFunctionController.FetchAccessdevisionByUseridforgrid(userRoleId, int.Parse(ddlCompanyUsers.SelectedValue));
                    if (GetRoleFunctionByRoleId.Count() > 0 && userRoleId != 0)
                    {
                        foreach (var item in GetRoleFunctionByRoleId)
                        {

                            if (item.uroleid == 0)
                            {
                                item.systemDivisionId = int.Parse(item.DevisionId.ToString());
                                item.IsActive = 0;

                            }
                            else
                            {
                                item.systemDivisionId = int.Parse(item.DevisionId.ToString());
                                item.IsActive = 1;
                            }

                        }
                        gvmaincategory.Visible = true;
                        gvmaincategory.DataSource = GetRoleFunctionByRoleId;
                        gvmaincategory.DataBind();
                    }

                }
                else
                {
                    ddlUserRoles.SelectedIndex = 0;
                    gvmaincategory.Visible = false;
                }
            }
        }
        
        protected void btnDeleteUserAccess_Click(object sender, EventArgs e)
        {
            try
            {
                string userId = hdnUserId.Value;
                if (userId != "" && userId != null)
                {
                    // int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                    //int usertId = int.Parse(gvUserAccess.Rows[x].Cells[0].Text);
                    int deleteUserRoleByUserId = companyUserAccessController.DeleteCompanyUserAccessByUserId(int.Parse(userId), CompanyId);
                    if (deleteUserRoleByUserId > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("User access has been Deleted Successfully", false);
                       // AllLoadUserAccess();
                    }
                    else
                    {
                        DisplayMessage("Problem occurs on Delete User access", true);
                    }
                }
                else
                {
                    DisplayMessage("Please Select user to Delete", true);
                }
            }
            catch (Exception)
            {

            }
        }
        
        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    string catogoryId = gvmaincategory.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvSubcatgry = e.Row.FindControl("gvSubcatgry") as GridView;
                    List<RoleFunction> GetRoleFunctionByRoleId = new List<RoleFunction>();
                    int roleid = int.Parse(ddlUserRoles.SelectedValue);
                    GetRoleFunctionByRoleId = roleFunctionController.FetchAccessFunctionByUseridforgrid(roleid, int.Parse(catogoryId), int.Parse(ddlCompanyUsers.SelectedValue));

                    if (GetRoleFunctionByRoleId.Count() > 0 && roleid != 0)
                    {
                        foreach (var item in GetRoleFunctionByRoleId)
                        {
                            if (item.uroleid == 0)
                            {
                                item.systemDivisionId = item.functionDevisionID;
                                item.functionId = item.fId;
                                item.IsActive = 0;

                            }
                            else
                            {
                                item.systemDivisionId = item.functionDevisionID;
                                item.functionId = item.fId;
                                item.IsActive = 1;
                            }

                        }

                        gvSubcatgry.DataSource = GetRoleFunctionByRoleId;
                        gvSubcatgry.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnbtnAssignfunction_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow gvfunctnrow = (GridViewRow)((LinkButton)sender).NamingContainer;
                GridView gvfunction = (GridView)(gvfunctnrow.Parent.Parent);
                GridViewRow gvCatagary = (GridViewRow)(gvfunction.NamingContainer);
                int b = gvCatagary.RowIndex;
                int x = gvfunctnrow.RowIndex;

                var gvfunctionroleId = gvfunction.Rows[x].Cells[0].Text;
                var gvfunctionId = gvfunction.Rows[x].Cells[1].Text;
                var gvdevisiondevisionId = gvfunction.Rows[x].Cells[2].Text;
                
                if (int.Parse(gvfunctionroleId) != 0)
                {
                    int deletedevisionRecords = companyUserAccessController.DeleteAllCompanyUserAccessByRoleIdAndFunctionId(int.Parse(ddlCompanyUsers.SelectedValue), CompanyId, int.Parse(ddlUserRoles.SelectedValue), int.Parse(gvdevisiondevisionId), int.Parse(gvfunctionId));

                }
                else
                {

                    int userRoleId = companyUserAccessController.SaveCompanyUserAccess(int.Parse(ddlCompanyUsers.SelectedValue), CompanyId, int.Parse(ddlUserRoles.SelectedValue), int.Parse(gvdevisiondevisionId), int.Parse(gvfunctionId), LocalTime.Now, UserId, LocalTime.Now, UserId);
                }


                ddlCompanyUsers_SelectedIndexChanged(sender, e);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}