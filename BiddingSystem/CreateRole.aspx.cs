using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Controller;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace BiddingSystem
{
    public partial class CreateRole : System.Web.UI.Page
    {
        #region Properties

       // int adminId = 0;
       // static int roleId = 0;
        //string UserId = string.Empty;
       // string CompanyId = string.Empty;

      //  public List<string> userList= new List<string>();
       // List<UserRole> companyDepartments = new List<UserRole>();
        UserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        RoleFunctionController roleFunctionController = ControllerFactory.CreateRoleFunctionController();
        SystemDivisionController systemDivisionController = ControllerFactory.CreateSystemDivisionController();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefUserCreation";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabUserCreation";
                    ((BiddingAdmin)Page.Master).subTabValue = "CreateRole.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "createRolelink";


                   // UserId = Session["UserId"].ToString();
                   // CompanyId = Session["CompanyId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 2, 4) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                    LoadUserRolesforAssigne();
                    LoadGV();
                }
                msg.Visible = false;
            }
             
            catch (Exception)
            {

            }
}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnSave.Text == "Save")
                {
                    int roleid = userRoleController.SaveUserRole(txtRoleName.Text, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), chkIsavtive.Checked == true ? 1 : 0,int.Parse(Session["CompanyId"].ToString()));
                    if (roleid != -1)
                    {
                        if (roleid > 0)
                        {



                            LoadUserRolesforAssigne();
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'User Role has been created successfully', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            //DisplayMessage("User Role has been created successfully", false);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'User Role Creation problem', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            //DisplayMessage("User Role Creation problem", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'User Role Name already exists', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //DisplayMessage("User Role Name already exists", true);
                    }
                }



                if (btnSave.Text == "Update")
                {
                    if (int.Parse(ViewState["roleId"].ToString()) != 0)
                    {

                        int updateroleStatus = userRoleController.UpdateUserRole(int.Parse(ViewState["roleId"].ToString()), txtRoleName.Text, LocalTime.Now, Session["UserId"].ToString(), chkIsavtive.Checked == true ? 1 : 0);

                        if (updateroleStatus != -1)
                        {
                            if (updateroleStatus > 0)
                            {
                                LoadUserRolesforAssigne();
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'User Role has been updated successfully', showConfirmButton: false,timer: 1500}); });   </script>", false);
                                //DisplayMessage("User Role has been updated successfully", false);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'User Role update Problem', showConfirmButton: true,timer: 4000}); });   </script>", false);
                                //DisplayMessage("User Role update Problem", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'User Role Name already exists', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            //DisplayMessage("User Role Name already exists", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select User Role', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        //DisplayMessage("Please Select User Role", true);
                    }
                }
                clearFields();
                LoadGV();

            }
            catch (Exception ex)
            {
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

        private void LoadGV()
        {
            try
            {
                    gvDepartments.DataSource = userRoleController.FetchUserRole(int.Parse(Session["CompanyId"].ToString()));
                    gvDepartments.DataBind();
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                ViewState["roleId"] = int.Parse(gvDepartments.Rows[x].Cells[1].Text);
                UserRole companyRoleObj = userRoleController.FetchUserRoleObjByRoleId(int.Parse(ViewState["roleId"].ToString()));
                if (companyRoleObj.userRoleId != 0)
                {
                    txtRoleName.Text = companyRoleObj.userRoleName;


                    if (companyRoleObj.IsActive == 1)
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
            catch (Exception)
            {

                throw;
            }
        }

        //protected void lnkBtnDelete_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string userroleId = hdnRoleId.Value;
        //        if (userroleId != "" && userroleId != null)
        //        {
        //            int deleteuserRoleStatus = userRoleController.DeleteUserRole(int.Parse(userroleId));
        //            if (deleteuserRoleStatus > 0)
        //            {
        //                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'User Role has been Deleted Successfully', showConfirmButton: false,timer: 1500}); });   </script>", false);
        //                //DisplayMessage("User Role has been Deleted Successfully", false);
        //                clearFields();
        //                LoadGV();
        //                LoadUserRolesforAssigne();

        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on Delete User Role', showConfirmButton: true,timer: 4000}); });   </script>", false);
        //                //DisplayMessage("Error on Delete User Role", true);
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Select User Role to Delete', showConfirmButton: true,timer: 4000}); });   </script>", false);
        //            //DisplayMessage("Please Select User Role to Delete", true);
        //        }






        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}


        private void clearFields()
        {
            txtRoleName.Text = "";
            btnSave.Text = "Save";
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clearFields();
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        protected void gvDepartments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //try
            //{
            //    gvDepartments.PageIndex = e.NewPageIndex;
            //    LoadGV();
            //}
            //catch (Exception)
            //{

            //}
        }

        private void LoadUserRolesforAssigne()
        {
            try
            {

                ddlRole.DataSource = userRoleController.FetchUserRole(int.Parse(Session["CompanyId"].ToString())).OrderBy(x => x.userRoleName);
                ddlRole.DataTextField = "userRoleName";
                ddlRole.DataValueField = "userRoleId";
                ddlRole.DataBind();
                ddlRole.Items.Insert(0, new ListItem("Select User Role", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnbtnAssigncategory_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                var role = gvmaincategory.Rows[x].Cells[2].Text;
                var devision = gvmaincategory.Rows[x].Cells[3].Text;


                if (int.Parse(role) != 0)
                {

                    int deletedevisionRecords = roleFunctionController.DeleteSysDivisonsByRoleIdAndDivId(int.Parse(role), int.Parse(devision));
                }
                else
                {
                    List<SystemDivisionFunction> Function = systemDivisionController.FetchSystemDivisionFunctionsBySystemDivisionId(int.Parse(devision));
                    foreach (var item in Function)
                    {
                        int isSaved = roleFunctionController.SaveRoleFunction(int.Parse(ddlRole.SelectedValue), int.Parse(devision), item.functionId, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), 1);
                    }

                }
                ddlRole_SelectedIndexChanged(sender, e);


            }
            catch (Exception)
            {

                throw;
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
                var gvdevisiondevisionId = gvmaincategory.Rows[b].Cells[3].Text;



                if (int.Parse(gvfunctionroleId) != 0)
                {

                    int deletedevisionRecords = roleFunctionController.DeleteRolewithdevisionFunction(int.Parse(gvfunctionroleId), int.Parse(gvdevisiondevisionId), int.Parse(gvfunctionId));

                }
                else
                {

                    int isSaved = roleFunctionController.SaveRoleFunction(int.Parse(ddlRole.SelectedValue), int.Parse(gvdevisiondevisionId), int.Parse(gvfunctionId), LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), 1);

                }
                ddlRole_SelectedIndexChanged(sender, e);


            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {

            int roleid = int.Parse(ddlRole.SelectedValue);
            List<RoleFunction> GetRoleFunctionByRoleId = roleFunctionController.FetchRoledevisionByRoleIdforgrid(roleid);
            if (GetRoleFunctionByRoleId.Count() > 0 && roleid != 0)
            {
                foreach (var item in GetRoleFunctionByRoleId)
                {

                    if (item.userRoleId == 0)
                    {
                        item.systemDivisionId = int.Parse(item.DevisionId.ToString());
                        item.IsActive = 0;

                    }

                }
                gvmaincategory.DataSource = GetRoleFunctionByRoleId;
                gvmaincategory.DataBind();
            }
            if (roleid == 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Select A role', showConfirmButton: true,timer: 4000}); });   </script>", false);
                //DisplayMessage("Select A role", true);
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
                    int roleid = int.Parse(ddlRole.SelectedValue);
                    GetRoleFunctionByRoleId = roleFunctionController.FetchRoleFunctionByRoleIdforgrid(roleid, int.Parse(catogoryId));

                    if (GetRoleFunctionByRoleId.Count() > 0 && roleid != 0)
                    {
                        foreach (var item in GetRoleFunctionByRoleId)
                        {
                            if (item.userRoleId == 0)
                            {
                                item.systemDivisionId = item.functionDevisionID;
                                item.functionId = item.fId;
                                item.IsActive = 0;
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

        protected void btnDeleteRole_Click(object sender, EventArgs e) {
            try {
                int UserRoleId = int.Parse(hdnRoleId.Value);

                int result = userRoleController.DeleteUserRole(UserRoleId, int.Parse(Session["CompanyId"].ToString()));
                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'User Role has been Deleted Successfully', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    clearFields();
                    LoadGV();
                    LoadUserRolesforAssigne();

                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void btnRestoreRole_Click(object sender, EventArgs e) {
            try {

                int UserRoleId = int.Parse(hdnRoleId.Value);

                int result = userRoleController.RestoreRole(UserRoleId, int.Parse(Session["CompanyId"].ToString()));
                if (result > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'User Role has been Restored Successfully', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    clearFields();
                    LoadGV();
                    LoadUserRolesforAssigne();

                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }
        protected void btnbtnRoleList_Click(object sender, EventArgs e) {
            var List = hdnRoleId.Value.ToList();
        }

            


        //[WebMethod]
        //public static List<UserRole> SearchRole(string text) {
        //    if (HttpContext.Current.Session["CompanyId"] != null) {
        //        //return ControllerFactory.CreateUserRoleController().SearchUserRole(text, int.Parse(HttpContext.Current.Session["CompanyId"].ToString()));
        //        List<UserRole> List =  ControllerFactory.CreateUserRoleController().SearchUserRole(text, int.Parse(HttpContext.Current.Session["CompanyId"].ToString()));
        //        return List;
        //    }
        //    else {
        //        return null;
        //    }
        //}
    }
}