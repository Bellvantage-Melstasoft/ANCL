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
    public partial class CompanyAddUserRole : System.Web.UI.Page
    {
        CheckBox chkList1;
        UserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
        FunctionActionController functionActionController = ControllerFactory.CreateFunctionActionController();
        RoleFunctionController roleFunctionController = ControllerFactory.CreateRoleFunctionController();
        SystemDivisionController systemDivisionController = ControllerFactory.CreateSystemDivisionController();
        List<UserRole> getAllUserRolesList = new List<UserRole>();
        List<FunctionAction> getAllActionList = new List<FunctionAction>();
        UserRole userRoleObj = new UserRole();
        int adminId = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["AdminId"] != null && Session["AdminId"].ToString() != "")
                {
                    adminId = int.Parse(Session["AdminId"].ToString());
                }
                else
                {
                    Response.Redirect("LoginPageAdmin.aspx");
                }
                msg.Visible = false;
                msg2.Visible = false;

                if (!IsPostBack)
               {
                    LoadUserRoles();
                    LoadddlUserRoles();
                    ClearFields2();
               }
            }
            catch (Exception)
            {

            }
        }

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }

        //----------------------Save Main Category
        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            try 
	        {	   
              int chkBoxIsActive = 0;

              if (chkIsavtive.Checked)
	          {
	              chkBoxIsActive = 1;	 
	          }
                List<ListItem> selected = chkActionList.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
               

                if (btnSave.Text == "Save")
              {
                  int userRoleId = userRoleController.SaveUserRole(txtUserRoeName.Text, LocalTime.Now, adminId.ToString(), LocalTime.Now, adminId.ToString(), chkBoxIsActive, int.Parse(Session["CompanyId"].ToString()));
                    if (userRoleId != -1)
                    {
                        if (userRoleId > 0)
                        {
                            ClearFiled();
                            LoadUserRoles();
                            LoadddlUserRoles();
                            DisplayMessage("User Role has been Created Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on Create Uset Role", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("User Role name already exists", true);
                    }
              }
              if (btnSave.Text == "Update")
              {
                  int UpdateUserRole = userRoleController.UpdateUserRole(int.Parse(hndfRoleId.Value), txtUserRoeName.Text,LocalTime.Now, adminId.ToString(), chkBoxIsActive);
                    if (UpdateUserRole != -1)
                    {
                        if (UpdateUserRole > 0)
                        {
                            ClearFiled();
                            LoadUserRoles();
                            LoadddlUserRoles();
                            DisplayMessage("User Role has been Updated Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on Update User Role", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("User Role name already exists", true);
                    }
              }
	        }
	        catch (Exception)
	        {
	        	
	        	throw;
	        }
        }

        //-----------------------Load Gv Data
        private void LoadUserRoles() 
        {
            try
            {
                gvUserRoles.DataSource = userRoleController.FetchUserRole(int.Parse(Session["CompanyId"].ToString()));
                gvUserRoles.DataBind();
            }
            catch (Exception ex)
            {
                
            }
        }

        private void LoadddlUserRoles()
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
                throw ex;
            }
        }

        private void bindCheckBox()
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
        //-------------------Edit Saved Data
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
            
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                int roeId = int.Parse(gvUserRoles.Rows[x].Cells[0].Text);
                userRoleObj = userRoleController.FetchUserRoleObjByRoleId(roeId);
                if(userRoleObj.userRoleId != 0)
                {
                    txtUserRoeName.Text = userRoleObj.userRoleName;
                    hndfRoleId.Value = userRoleObj.userRoleId.ToString();
                    if (userRoleObj.IsActive == 1)
                    {
                        chkIsavtive.Checked = true;
                    }
                    else
                    {
                        chkIsavtive.Checked = false;
                    }
                    btnSave.Text = "Update";
                }
               
            }
            catch (Exception ex)
            {
                
                
            }
        }

        //-----------------Delete Saved Data
        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                //int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                //categoryId = int.Parse(gvMainCategory.Rows[x].Cells[0].Text);
                //categoryName = gvMainCategory.Rows[x].Cells[1].Text;
                //txtMainCategoryName.Text = categoryName;
                //isActive = int.Parse(gvMainCategory.Rows[x].Cells[6].Text);
                //if (isActive == 1)
                //{
                //    chkIsavtive.Checked = true;
                //}
                //else {
                //    chkIsavtive.Checked = false;
                //}
                //int deleteItem = itemCategoryController.DeleteItemCategory(categoryId);

                //if (deleteItem == 1)
                //{

                //}
                //else { }
            }
            catch (Exception ex)
            {
             
            }
        }

       

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFiled();
        }

        private void ClearFiled()
        {
            txtUserRoeName.Text = "";
            adminId = 0;
            chkIsavtive.Checked = true;
            btnSave.Text = "Save";
        }
       

        protected void ddUserRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if(ddlUserRoles.SelectedIndex != 0)
                {
                    divSystemDivOfUserRole.Visible = true;
                    UserRoleNameTite.InnerText = "Parent Node Of "+ ddlUserRoles.SelectedItem.Text;
                    ddlSystemDivision.DataSource = systemDivisionController.FetchSystemDivision();
                    ddlSystemDivision.DataTextField = "systemDivisionName";
                    ddlSystemDivision.DataValueField = "systemDivisionId";
                    ddlSystemDivision.DataBind();
                    ddlSystemDivision.Items.Insert(0, new ListItem("Select Parent Node", "0"));
                    SystemDivisionBindingByRoleId();

                }
                else
                {
                    divSystemDivOfUserRole.Visible = false;
                    ddlSystemDivision.DataSource = null;
                    ddlSystemDivision.DataTextField = "systemDivisionName";
                    ddlSystemDivision.DataValueField = "systemDivisionId";
                    ddlSystemDivision.DataBind();
                    ddlSystemDivision.Items.Insert(0, new ListItem("Select Parent Node", "0"));

                    gvSystemDivisions.DataSource = null;
                    gvSystemDivisions.DataBind();
                }
            }
            catch (Exception)
            {

            }
        }

        protected void ddlSystemDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSystemDivision.SelectedIndex != 0)
                {
                    List<SystemDivisionFunction> GetFunctionListBySysDivisionId = new List<SystemDivisionFunction>();
                    List<RoleFunction> RoleFunction = new List<RoleFunction>();
                    GetFunctionListBySysDivisionId = systemDivisionController.FetchSystemDivisionFunctionsBySystemDivisionId(int.Parse(ddlSystemDivision.SelectedValue));
                    RoleFunction = roleFunctionController.FetchRoleFunctionByRoleIdAndDivId(int.Parse(ddlUserRoles.SelectedValue), int.Parse(ddlSystemDivision.SelectedValue));
                    chkActionList.Items.Clear();
                    foreach (var item in GetFunctionListBySysDivisionId)
                    {
                        ListItem lItem = new ListItem();
                        lItem.Text = item.functionName;
                        lItem.Value = item.functionId.ToString();
                        chkActionList.Items.Add(lItem);
                    }
                    for (int i = 0; i < chkActionList.Items.Count; i++)
                    {
                        foreach (var item in RoleFunction)
                        {
                            if (item.functionId == int.Parse(chkActionList.Items[i].Value))
                                chkActionList.Items[i].Selected = true;
                        }
                    }

                }
                else
                {
                    chkActionList.Items.Clear();
                }
            }
            catch (Exception)
            {

            }
        }

        private void SystemDivisionBindingByRoleId()
        {


            List<RoleFunction> GroupedUserAccessByUserId = new List<RoleFunction>();
            List<RoleFunction> GetRollFunctionsByRoleId = new List<RoleFunction>();



            GetRollFunctionsByRoleId = roleFunctionController.FetchRoleFunctionByRoleId(int.Parse(ddlUserRoles.SelectedValue));
            if(GetRollFunctionsByRoleId.Count()>0)
            {
                GroupedUserAccessByUserId= GetRollFunctionsByRoleId.GroupBy(x => x.systemDivisionId).Select(g => g.First()).ToList();
            }
           // var duplicates = GetRollFunctionsByRoleId.GroupBy(x => new { x.systemDivisionId, x.systemDivisionName }).Where(x => x.Skip(0).Any()).ToList();

           // foreach (var item in duplicates)
           // {
           //     GroupedUserAccessByUserId.Add(new RoleFunction { systemDivisionId = item.Key.systemDivisionId, systemDivisionName = item.Key.systemDivisionName, userRoleId = int.Parse(ddlUserRoles.SelectedValue) });
           // }

            gvSystemDivisions.DataSource = GroupedUserAccessByUserId;
            gvSystemDivisions.DataBind();
        }


        protected void btnAddUderSystemDivision_Click(object sender, EventArgs e)
        {
            try
            {
                int deleteAllRoleFunctions = roleFunctionController.DeleteSysDivisonsByRoleIdAndDivId(int.Parse(ddlUserRoles.SelectedValue), int.Parse(ddlSystemDivision.SelectedValue));

                List<ListItem> selected = chkActionList.Items.Cast<ListItem>().Where(li => li.Selected).ToList();
                foreach (var item in selected)
                {
                  int assignUserWithSysDivisionStatus = roleFunctionController.SaveRoleFunction(int.Parse(ddlUserRoles.SelectedValue), int.Parse(ddlSystemDivision.SelectedValue),int.Parse(item.Value), LocalTime.Now, adminId.ToString(), LocalTime.Now, adminId.ToString(), 1);
                }
                DisplayMessageTwo("Child Node has been Assigned to User Role Successfully", false);
                ClearFields2();

                if (ddlUserRoles.SelectedIndex != 0)
                {
                    divSystemDivOfUserRole.Visible = true;
                    UserRoleNameTite.InnerText = "Parent Node Of " + ddlUserRoles.SelectedItem.Text;
                    ddlSystemDivision.DataSource = systemDivisionController.FetchSystemDivision();
                    ddlSystemDivision.DataTextField = "systemDivisionName";
                    ddlSystemDivision.DataValueField = "systemDivisionId";
                    ddlSystemDivision.DataBind();
                    ddlSystemDivision.Items.Insert(0, new ListItem("Select Parent Node", "0"));
                    SystemDivisionBindingByRoleId();

                }
                else
                {
                    divSystemDivOfUserRole.Visible = false;
                    ddlSystemDivision.DataSource = null;
                    ddlSystemDivision.DataTextField = "systemDivisionName";
                    ddlSystemDivision.DataValueField = "systemDivisionId";
                    ddlSystemDivision.DataBind();
                    ddlSystemDivision.Items.Insert(0, new ListItem("Select Parent Node", "0"));

                    gvSystemDivisions.DataSource = null;
                    gvSystemDivisions.DataBind();
                }




            }
            catch (Exception)
            {
                DisplayMessageTwo("Problem Occurs on Assigned Child Node to User Role", false);
            }
        }

        protected void btnCancelUserRoles_Click(object sender, EventArgs e)
        {
            try
            {

                ClearFields2();

            }
            catch (Exception)
            {

            }
        }

        private void ClearFields2()
        {
            try
            {
               // ddlUserRoles.SelectedIndex = 0;
                ddlSystemDivision.SelectedIndex = 0;
                ddlUserRoles.SelectedIndex = 0;
                chkActionList.Items.Clear();
                divSystemDivOfUserRole.Visible = false;

            }
            catch (Exception)
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

        private void DisplayMessageTwo(string message, bool isError)
        {
            msg2.Visible = true;
            if (isError)
            {
                lbMessageTwo.CssClass = "failMessage";
                msg2.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else
            {
                lbMessageTwo.CssClass = "successMessage";
                msg2.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessageTwo.Text = message;

        }

        protected void lbtnDeleteSystemFunction_Click(object sender, EventArgs e)
        {
            try
            {
                string UserRoleId = hdnUserRoleId.Value;
                string sysDivisionId = hdnsysDivisionId.Value;
                if (UserRoleId != "" && sysDivisionId != "")
                {
                    int deleteUserRoleDivisionStatus = roleFunctionController.DeleteSysDivisonsByRoleIdAndDivId(int.Parse(UserRoleId), int.Parse(sysDivisionId));
                    if (deleteUserRoleDivisionStatus > 0)
                    {
                        DisplayMessageTwo("Parent Node has been Deleted  Successfully", false);
                        SystemDivisionBindingByRoleId();
                        ddlSystemDivision.SelectedIndex = 0;
                        chkActionList.Items.Clear();
                    }
                    else
                    {
                        DisplayMessageTwo("Error on Delete Parent Node", true);
                    }
                }
                else
                {
                    DisplayMessageTwo("Please Select Parent node", true);
                }
                    //int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
               // int RoleId = int.Parse(gvSystemDivisions.Rows[x].Cells[0].Text);
               // int sysDivisionId = int.Parse(gvSystemDivisions.Rows[x].Cells[1].Text);
                
            }
            catch (Exception)
            {

            }
        }
    }
}