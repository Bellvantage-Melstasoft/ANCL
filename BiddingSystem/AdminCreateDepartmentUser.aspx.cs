using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Controller;

namespace BiddingSystem
{
    public partial class AdminCreateDepartmentUser : System.Web.UI.Page
    {
     //   int AdminId = 0;
      
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();

        protected void Page_Load(object sender, EventArgs e)
        {
            int AdminId = 0;
            if (Session["AdminId"] != null)
            {
                AdminId = int.Parse(Session["AdminId"].ToString());
            }
            else
            {
                Response.Redirect("LoginPageAdmin.aspx");
            }
            

            if (!IsPostBack)
            {
                BindDepartments();
            }
            msg.Visible = false;
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int SaveUser = companyLoginController.SaveCompanyLogin(int.Parse(ddlDepartments.SelectedValue),txtusername.Text,"", txtpassword.Text.Encrypt(), "S", txtName.Text, txtEmilAddress.Text, LocalTime.Now, "Admin", LocalTime.Now, "Admin", chkIsActive.Checked == true ? 1 : 0,0);
                if (SaveUser != -1)
                {
                    if (SaveUser > 0)
                    {
                        DisplayMessage("Company administrator has been created successfully", false);
                        clearFields();
                    }
                    else
                    {
                        DisplayMessage("Problem in Creation", false);
                    }
                }
                else
                {
                    DisplayMessage("Username/Email address is already used!!", true);
                }


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


        protected void BindDepartments()
        {
            ddlDepartments.DataSource = companyDepartmentController.GetDepartmentList().Where(x=>x.IsActive==1).ToList();
            ddlDepartments.DataTextField = "DepartmentName";
            ddlDepartments.DataValueField = "DepartmentID";
            ddlDepartments.DataBind();
            ddlDepartments.Items.Insert(0, new ListItem("Select Department", "0"));
        }

        private void clearFields()
        {
            ddlDepartments.SelectedIndex = 0;
            txtName.Text = "";
            txtpassword.Text = "";
            txtusername.Text = "";
            txtConfirmPassword.Text = "";
            txtEmilAddress.Text = "";
            chkIsActive.Checked = false;

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            clearFields();
        }
    }


}