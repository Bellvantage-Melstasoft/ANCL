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
    public partial class CompanyEditUserAccess : System.Web.UI.Page
    {
        string UserId = string.Empty;
        static int CompanyId = 0;

        CheckBox chkList1;
        List<UserRole> getAllUserRolesList = new List<UserRole>();
        List<FunctionAction> getAllActionList = new List<FunctionAction>();
        UserRole userRoleObj = new UserRole();
        List<CompanyUserAccess> GetUserAccessByUserIdList = new List<CompanyUserAccess>();
        List<CompanyUserAccess> GetUserAccessByUserId = new List<CompanyUserAccess>();

        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        UserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
        FunctionActionController functionActionController = ControllerFactory.CreateFunctionActionController();
        RoleFunctionController roleFunctionController = ControllerFactory.CreateRoleFunctionController();
        SystemDivisionController systemDivisionController = ControllerFactory.CreateSystemDivisionController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();

       
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    CompanyId = int.Parse(Session["CompanyId"].ToString());
                    UserId = Session["UserId"].ToString();
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
                msg.Visible = false;

                if (!IsPostBack)
               {
                    LoadCompanyUsers();
                    LoadSystemDivisions();
                    LoadUserRoles();
                    bindCheckBox();
                    divUpdate.Visible = false;
                
                getAllUserRolesList = userRoleController.FetchUserRole(int.Parse(Session["CompanyId"].ToString()));
            }
            }
            catch (Exception)
            {

            }
        }


        // ------------------events-----------------------------------------
        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }
        
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
            try 
	        {	   
                List<ListItem> selected = chkActionList.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
                    int DeleteStatus = companyUserAccessController.DeleteAllCompanyUserAccessByDividionId(int.Parse(ddlCompanyUsers.SelectedValue), CompanyId, int.Parse(ddlUserRoles.SelectedValue));
                    foreach (var item in selected)
                    {
                        int userRoleId = companyUserAccessController.SaveCompanyUserAccess(int.Parse(ddlCompanyUsers.SelectedValue), CompanyId, int.Parse(ddlUserRoles.SelectedValue),1, int.Parse(item.Value), LocalTime.Now, UserId, LocalTime.Now, UserId);
                    }
                    DisplayMessage("User Access has been Defined Successfully", false);
                    divUpdate.Visible = false;
                    loadUserAccessByDDlUserSelect();


            }
	        catch (Exception)
	        {
	        
	        }
        }
        
        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFiled();
        }
        
        protected void ddlUserRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < chkActionList.Items.Count; i++)
                {
                    chkActionList.Items[i].Selected = false;
                }
                
                int selectedUserRoleId = int.Parse(ddlUserRoles.SelectedValue);
                userRoleObj = userRoleController.FetchUserRoleObjByRoleId(selectedUserRoleId);
                if (userRoleObj.userRoleId != 0)
                {
                    List<RoleFunction> roleFunctionList = new List<RoleFunction>();
                    roleFunctionList = userRoleObj._roleFunctionList.Where(X => X.userRoleId == userRoleObj.userRoleId).ToList();

                    for (int i = 0; i < chkActionList.Items.Count; i++)
                    {
                        foreach (var item in roleFunctionList)
                        {
                            if (item.functionId == int.Parse(chkActionList.Items[i].Value))
                                chkActionList.Items[i].Selected = true;
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                string userRoleId = gvUserAccess.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView gvActions = e.Row.FindControl("gvActions") as GridView;


                gvActions.DataSource = GetUserAccessByUserId.Where(x=>x.userRoleID == int.Parse(userRoleId));
                gvActions.DataBind();
            }
        }
       
        protected void ddlCompanyUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadUserAccessByDDlUserSelect();
        }
        
        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                LoadSystemDivisions();
                LoadUserRoles();
                bindCheckBox();
                divUpdate.Visible = true;

                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                int UserId = int.Parse(gvUserAccess.Rows[x].Cells[0].Text);
                int departmentId = int.Parse(gvUserAccess.Rows[x].Cells[1].Text);
                int systemDivisionId = int.Parse(gvUserAccess.Rows[x].Cells[2].Text);
                int userRoleId = int.Parse(gvUserAccess.Rows[x].Cells[3].Text);

                GetUserAccessByUserId = companyUserAccessController.FetchCompanyUserAccessByUserId(UserId, departmentId);
                ddlSystemDivisions.SelectedValue = systemDivisionId.ToString();
                ddlUserRoles.SelectedValue = userRoleId.ToString();

                try
                {
                    for (int i = 0; i < chkActionList.Items.Count; i++)
                    {
                        chkActionList.Items[i].Selected = false;
                    }
                    
                        for (int i = 0; i < chkActionList.Items.Count; i++)
                        {
                            foreach (var item in GetUserAccessByUserId.Where(t=>t.sysDivisionId== systemDivisionId && t.userRoleID== userRoleId))
                            {
                                if (item.actionId == int.Parse(chkActionList.Items[i].Value))
                                    chkActionList.Items[i].Selected = true;
                            }

                        }
                }
                catch (Exception)
                {

                }



            }
            catch (Exception)
            {

             
            }
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                int UserId = int.Parse(gvUserAccess.Rows[x].Cells[0].Text);
                int departmentId = int.Parse(gvUserAccess.Rows[x].Cells[1].Text);
                int systemDivisionId = int.Parse(gvUserAccess.Rows[x].Cells[2].Text);
                int userRoleId = int.Parse(gvUserAccess.Rows[x].Cells[3].Text);

                int DeleteStatus = companyUserAccessController.DeleteAllCompanyUserAccessByDividionId(UserId, departmentId,  userRoleId);
                if (DeleteStatus > 0)
                {
                    DisplayMessage("User Role has been Deleted Successfully", false);
                }
                else
                {
                    DisplayMessage("Error On Delete User Role" ,true);
                }
            }
            catch (Exception)
            {

             
            }
        }

        // ------------------End events-----------------------------------------



        // ------------------Bindings-----------------------------------------
        private void loadUserAccessByDDlUserSelect()
        {

            divUpdate.Visible = false;
            if (ddlCompanyUsers.SelectedIndex != 0)
            {
                List<CompanyUserAccess> GroupedUserAccessByUserId = new List<CompanyUserAccess>();
                GetUserAccessByUserId = companyUserAccessController.FetchCompanyUserAccessByUserId(int.Parse(ddlCompanyUsers.SelectedValue), CompanyId);

                var duplicates = GetUserAccessByUserId.GroupBy(x => new { x.userId, x.departmentId, x.sysDivisionId, x.userRoleID, x.sysDivisionName, x.userRoleName }).Where(x => x.Skip(1).Any()).ToList();

                foreach (var item in duplicates)
                {
                    GroupedUserAccessByUserId.Add(new CompanyUserAccess { userId = item.Key.userId, departmentId = item.Key.departmentId, sysDivisionId = item.Key.sysDivisionId, userRoleID = item.Key.userRoleID, sysDivisionName = item.Key.sysDivisionName, userRoleName = item.Key.userRoleName });
                }

                gvUserAccess.DataSource = GroupedUserAccessByUserId;
                gvUserAccess.DataBind();
            }
            else
            {
                gvUserAccess.DataSource = null;
                gvUserAccess.DataBind();
            }
        }
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
        private void LoadSystemDivisions()
        {
            try
            {
                ddlSystemDivisions.DataSource = systemDivisionController.FetchSystemDivision();
                ddlSystemDivisions.DataTextField = "systemDivisionName";
                ddlSystemDivisions.DataValueField = "systemDivisionId";
                ddlSystemDivisions.DataBind();
                ddlSystemDivisions.Items.Insert(0, new ListItem("Select System Division", "0"));
            }
            catch (Exception ex)
            {

            }
        }
        private void LoadCompanyUserAccess()
        {
            try
            {
                gvUserAccess.DataSource = companyUserAccessController.FetchCompanyUserAccessByUserId(int.Parse(ddlCompanyUsers.SelectedValue), CompanyId);
                ddlSystemDivisions.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        
        private void LoadCompanyUsers()
        {
            try
            {

                ddlCompanyUsers.DataSource = companyLoginController.GetUserListByDepartmentid(CompanyId);
                ddlCompanyUsers.DataTextField = "FirstName";
                ddlCompanyUsers.DataValueField = "UserId";
                ddlCompanyUsers.DataBind();
                ddlCompanyUsers.Items.Insert(0, new ListItem("Select User", "0"));

            }
            catch (Exception)
            {

            }
        }
        private void bindCheckBox()
        {
            try
            {
                chkActionList.Items.Clear();
                getAllActionList = functionActionController.FetchFunctionAction();
                foreach (var item in getAllActionList)
                {
                    ListItem lItem = new ListItem();
                    lItem.Text = item.functionActionName;
                    lItem.Value = item.functionActionId.ToString();
                    //lItem.Selected = Convert.ToBoolean(sdr["IsSelected"]);
                    chkActionList.Items.Add(lItem);
                }
            }
            catch (Exception)
            {

            }
        }
        private void ClearFiled()
        {
            divUpdate.Visible = false;
            ddlCompanyUsers.SelectedIndex = 0;
            ddlSystemDivisions.SelectedIndex = 0;
            ddlUserRoles.SelectedIndex = 0;

            CompanyId = 0;
            UserId = string.Empty;

            for (int i = 0; i < chkActionList.Items.Count; i++)
            {
                chkActionList.Items[i].Selected = false;
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
        // ------------------Bindings-----------------------------------------
    }
}