using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class LoginPageAdmin : System.Web.UI.Page
    {
        SuperAdminController superAdminController = ControllerFactory.CreateSuperAdminController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.Cookies["UserName"] != null && Request.Cookies["Password"] != null)
                {
                    txtUserName.Text = Request.Cookies["UserName"].Value;
                    txtPwd.Attributes["value"] = Request.Cookies["Password"].Value;
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                SuperAdminController superAdminController = ControllerFactory.CreateSuperAdminController();
                SuperAdmin superAdminObj = new SuperAdmin();
                superAdminObj = superAdminController.GetSuperAdminLogin(txtUserName.Text, txtPwd.Text);

                
                if (superAdminObj.AdminId != 0)
                {
                    if (chkRemember.Checked)
                    {
                        Response.Cookies["UserName"].Expires = LocalTime.Now.AddDays(30);
                        Response.Cookies["Password"].Expires = LocalTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["UserName"].Expires = LocalTime.Now.AddDays(-1);
                        Response.Cookies["Password"].Expires = LocalTime.Now.AddDays(-1);

                    }
                    Response.Cookies["UserName"].Value = txtUserName.Text.Trim();
                    Response.Cookies["Password"].Value = txtPwd.Text.Trim();

                    Session["AdminId"] = superAdminObj.AdminId;
                    Response.Redirect("SuperAdminDashboard.aspx");
                   
                }
                else
                {
                    lblLoginAlert.Text = "Invalid Credentials!!";
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}