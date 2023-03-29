using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Controller;
using System.Net.Mail;
using System.Net;

namespace BiddingSystem
{
    public partial class CompanyCreateUser : System.Web.UI.Page
    {
        #region Properies
        //int CompanyId = 0;
       // string userId = string.Empty;
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        UserRoleController userRoleController = ControllerFactory.CreateUserRoleController();
        RoleFunctionController roleFunctionController = ControllerFactory.CreateRoleFunctionController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        DesignationController designationController = ControllerFactory.CreateDesignationController();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefUserCreation";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabUserCreation";
                    ((BiddingAdmin)Page.Master).subTabId = "createUserLink";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyCreateUser.aspx";


                   // CompanyId = int.Parse(Session["CompanyId"].ToString());
                   // userId = Session["UserId"].ToString();

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 2, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                    loadDepartments();
                    loadWarehouses();
                    loadroles();
                    loadDesignations();
                    
                }
                msg.Visible = false;
            }
            catch (Exception)
            {

            }

        }
        private void loadDesignations()
        {
            try
            {
                ddlDesignation.DataSource = designationController.GetDesignationList();
                ddlDesignation.DataValueField = "DesignationId";
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataBind();
                ddlDesignation.Items.Insert(0, new ListItem("--Select Designation--", ""));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        private void loadroles()
        {
            try
            {
                ddlUserZRole.DataSource = userRoleController.FetchUserRole(int.Parse(Session["CompanyId"].ToString())).OrderBy(f => f.userRoleName);
                ddlUserZRole.DataTextField = "userRoleName";
                ddlUserZRole.DataValueField = "userRoleId";
                ddlUserZRole.DataBind();
                ddlUserZRole.Items.Insert(0, new ListItem("Select User Role", ""));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

       


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                List<UserSubDepartment> UsersubDepartment = new List<UserSubDepartment>();

                for (int i = 0; i < gvdepartments.Rows.Count; i++) {
                    if (((CheckBox)gvdepartments.Rows[i].FindControl("chkDepartments")).Checked) {
                        UserSubDepartment subDepartment = new UserSubDepartment();
                        subDepartment.DepartmentId = int.Parse(gvdepartments.Rows[i].Cells[1].Text);
                        subDepartment.IsHead = ((CheckBox)gvdepartments.Rows[i].FindControl("chkIsHead")).Checked == true ? 1 : 0;
                        UsersubDepartment.Add(subDepartment);

                    }
                }
                
                List<UserWarehouse> UserWarehouse = new List<UserWarehouse>();

                for (int i = 0; i < gvwarehouse.Rows.Count; i++) {

                    if (((CheckBox)gvwarehouse.Rows[i].FindControl("chkwarehouse")).Checked) {
                        UserWarehouse warehouse = new UserWarehouse();

                        warehouse.WrehouseId = int.Parse(gvwarehouse.Rows[i].Cells[1].Text);
                        warehouse.UserType = int.Parse(((DropDownList)gvwarehouse.Rows[i].FindControl("ddlUserType")).SelectedValue);
                       UserWarehouse.Add(warehouse);
                       

                    }

                }





                int SaveUser = 0;
                string userType = "S";  // kept for while (sha)
                //if (ddlDepartment.SelectedIndex == 0)
                //{
                //    SaveUser = companyLoginController.SaveCompanyLogin(CompanyId, txtusername.Text, txtEmpNo.Text, txtpassword.Text.Encrypt(), userType, txtName.Text, txtEmilAddress.Text, LocalTime.Now, userId, LocalTime.Now, userId, chkIsActive.Checked == true ? 1 : 0, int.Parse(ddlDesignation.SelectedValue));
                //}
                //else
                //{
                //    //AdminCreateDepartmentUser 
                //    var warehouseId = 0;
                //    if (ddlwarehouse.SelectedIndex>0)
                //    {
                //        warehouseId = int.Parse(ddlwarehouse.SelectedValue);
                //    }

                //    SaveUser = companyLoginController.SaveCompanyLogin(CompanyId, txtusername.Text, txtEmpNo.Text, txtpassword.Text.Encrypt(), userType, txtName.Text, txtEmilAddress.Text, LocalTime.Now, userId, LocalTime.Now, userId, chkIsActive.Checked == true ? 1 : 0, int.Parse(ddlDepartment.SelectedValue), int.Parse(ddlDesignation.SelectedValue), warehouseId);
                //}

                SaveUser = companyLoginController.SaveCompanyLogin(int.Parse(Session["CompanyId"].ToString()), txtusername.Text, txtEmpNo.Text, txtpassword.Text.Encrypt(), userType, txtName.Text, txtEmilAddress.Text, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString(), chkIsActive.Checked == true ? 1 : 0, int.Parse(ddlDesignation.SelectedValue), UsersubDepartment, UserWarehouse, txtContactNo.Text);


                List<RoleFunction> GetRoleFunctionByRoleId = new List<RoleFunction>();
                GetRoleFunctionByRoleId = roleFunctionController.FetchRoleFunctionByRoleId(int.Parse(ddlUserZRole.SelectedValue));
                
                if (SaveUser != -1)
                {
                    if (SaveUser > 0)
                    {
                        if (GetRoleFunctionByRoleId.Count() > 0)
                        {
                            foreach (var item in GetRoleFunctionByRoleId)
                            {
                                int saveUserAccess = companyUserAccessController.SaveCompanyUserAccessvusename(txtusername.Text, int.Parse(Session["CompanyId"].ToString()), int.Parse(ddlUserZRole.SelectedValue), item.systemDivisionId, item.functionId, LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, Session["UserId"].ToString());
                            }

                        }
                        string message = "Dear " + txtName.Text + ",\n\n" +
                            "Please find your login credentials for www.ezbidlanka.com below.\n\n" +
                            "UserName: " + txtusername.Text + "\n" +
                            "Password: " + txtpassword.Text + "\n\n" +
                            "Regards,\nTeam EzBidlanka.";

                        SendEmail(txtEmilAddress.Text, message);

                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                        //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("User has been created successfully", false);
                        ClearFields();
                        loadDepartments();
                        loadWarehouses();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on creating User'}); });   </script>", false);
                        //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on creating User\"; $('#errorAlert').modal('show'); });   </script>", false);
                        //DisplayMessage("Error On User creation", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'This Username/Email Address Or Employee No already exists'}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"This username / Email Address already exists\"; $('#errorAlert').modal('show'); });   </script>", false);
                    //DisplayMessage("This username / Email Address already exists", true);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void loadDepartments() {
           

            gvdepartments.DataSource = subDepartmentController.getDepartmentList(int.Parse(Session["CompanyId"].ToString()));
            gvdepartments.DataBind();

        }

        private void loadWarehouses() {
            gvwarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
            gvwarehouse.DataBind();
        }


        private void SendEmail(string to, string message)
        {
            try
            {
                MailMessage mail = new MailMessage("NoReply@ezbidlanka.com", to);
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential("NoReply@ezbidlanka.com", "ezbidlive@123");
                client.Host = "mail.ezbidlanka.com";
                mail.Subject = "Login Credentials for www.ezbidlanka.com";
                mail.Body = message;
                client.Send(mail);
            }
            catch (Exception)
            {

            }
        }

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtName.Text = "";
            txtEmilAddress.Text = "";
            txtusername.Text = "";
            txtpassword.Text = "";
            txtEmilAddress.Text = "";
            txtEmpNo.Text = "";
            chkIsActive.Checked = false;
            ddlUserZRole.SelectedIndex = 0;


        }

        [System.Web.Services.WebMethod]
        public static int ChangePassword(string userName, string password, string oldPassword)
        {

            return ControllerFactory.CreateCompanyLoginController().ChangePassword(userName, oldPassword.Encrypt(), password.Encrypt());
        }

        protected void ddlDesignation_SelectedIndexChanged(object sender, EventArgs e)
        {

            btnSaveDesignation.Visible = true;
            btnUpdateDesignation.Visible = false;
            if (ddlDesignation.SelectedIndex != 0)
            {
                //btnEditDesignation.Enabled = true;
            }
            else
            {
                //btnEditDesignation.Enabled = false;
            }
           
        }

        protected void btnSaveDesignation_Click(object sender, EventArgs e)
        {

            string newDesignation = txtDesignationName.Text.Replace(",", "");
            designationController.SaveDesignation(newDesignation, Session["UserId"].ToString(), LocalTime.Now);
            DisplayMessage("Designation Added Succesfully", false);

            loadDesignations();
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>     $('.modal-backdrop').remove();   </script>", false);

        }


        protected void btnCreateNewDesignation_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $('#myModalAddDesignation').modal('show'); </script>", false);
            txtDesignationName.Text = "";

            btnSaveDesignation.Text = "Save Designation";
        }

        protected void btnEditDesignation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $('#myModalAddDesignation').modal('show'); </script>", false);

            btnSaveDesignation.Visible = false;
            btnUpdateDesignation.Visible = true;
            txtDesignationName.Text = ddlDesignation.SelectedItem.ToString().Replace(",", "");
            btnSaveDesignation.Text = "Update Designation";

        }

        protected void btnUpdateDesignation_Click(object sender, EventArgs e)
        {

            string newDesignation = txtDesignationName.Text.Replace(",", "");
            int designationId = int.Parse(ddlDesignation.SelectedValue);
            designationController.UpdateDesignation(designationId, newDesignation, Session["UserId"].ToString(), LocalTime.Now);
            DisplayMessage("Designation Updated Succesfully", false);

            loadDesignations();

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>     $('.modal-backdrop').remove();   </script>", false);


        }
    }
}