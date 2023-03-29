using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class DepartmentViewUser : System.Web.UI.Page
    {
        #region Properties

        int AdminId = 0;
        static int viewUserId = 0;
        int userId = 0;
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        List<CompanyLogin> getUserListByCompanyId = new List<CompanyLogin>();
        CompanyLogin userobj = new CompanyLogin();

        #endregion

       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

            if (Session["AdminId"] != null)
            {
                AdminId = int.Parse(Session["AdminId"].ToString());
            }
            else
            {
                Response.Redirect("LoginPageAdmin.aspx");
            }

                msg.Visible = false;
            if (!IsPostBack)
            {
                Bindusers();
            }

            }
            catch (Exception)
            {

            }
        }

        protected void btnDeleteCompanyAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                string UserId = hdnCompanyUser.Value;
                if (UserId != "" && UserId != null)
                {
                    //int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                    //viewUserId = int.Parse( gvCompanyUsers.Rows[x].Cells[0].Text);
                    int updateInactive = companyLoginController.UpdateInactiveUsers(int.Parse(UserId), 0);
                    if(updateInactive > 0)
                    {
                        hdnCompanyUser.Value = null;
                        Bindusers();
                        divUpdateAdmin.Visible = false;
                        DisplayMessage("Company Admin has been Deleted Successfully", false);
                    }
                    else
                    {
                        DisplayMessage("Error on Delete Company Admin", true);
                    }
                   
                }
                else
                {
                    DisplayMessage("Please Select Company admin to Delete", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                divUpdateAdmin.Visible = true;
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                viewUserId = int.Parse( gvCompanyUsers.Rows[x].Cells[1].Text);
                userobj = companyLoginController.GetUserbyuserId(viewUserId);
                if(userobj.UserId != 0)
                {
                   // ddlDepartments.SelectedValue = userobj.DepartmentId.ToString();
                    txtName.Text = userobj.FirstName;
                    txtusername.Text = userobj.Username;
                    txtpassword.Text = userobj.Password.Decrypt();
                    txtEmilAddress.Text = userobj.EmailAddress;
                  
                    if (userobj.IsActive == 1)
                    {
                        chkIsActive.Checked = true;
                    }
                    else
                    {
                        chkIsActive.Checked = false;
                    }
                    
                }


            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

           
            if (viewUserId != 0)
            {
                int Updateuser = companyLoginController.UpdateCompanyLogin(viewUserId, txtusername.Text,"", txtpassword.Text.Encrypt(), "S", txtName.Text, txtEmilAddress.Text, LocalTime.Now, "Admin", chkIsActive.Checked ? 1 : 0,0);
                    if (Updateuser != -1)
                    {
                        if (Updateuser > 0)
                        {
                            DisplayMessage("Company Admin Details has been Updated successfully", false);
                            clearFields();
                            divUpdateAdmin.Visible = false;
                            hdnCompanyUser.Value = null;
                            viewUserId = 0;
                            Bindusers();
                        }
                        else
                        {
                            DisplayMessage("Error on Update Company Admin", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("User Name / Email Address Already Exist", true);
                    }
               
            }
            else
            {
                DisplayMessage("Please select Company Administrator", true);

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
        
        protected void Bindusers()
        {
            try
            {

            ddlDepartments.DataSource = companyDepartmentController.GetDepartmentList().Where(x=>x.IsActive==1).ToList();
            ddlDepartments.DataTextField = "DepartmentName";
            ddlDepartments.DataValueField = "DepartmentID";
            ddlDepartments.DataBind();
            ddlDepartments.Items.Insert(0, new ListItem("Select Department", "0"));


            getUserListByCompanyId = companyLoginController.GetAllUserList().Where(x=>x.Usertype=="S").ToList();

            //if (getUserListByCompanyId.Count() > 0)
            //{
                gvCompanyUsers.DataSource = getUserListByCompanyId;
                gvCompanyUsers.DataBind();
           // }

            }
            catch (Exception)
            {

            }
        }

        protected void ddlDepartments_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            if (ddlDepartments.SelectedIndex != 0)
            {
                getUserListByCompanyId = companyLoginController.GetUserListByDepartmentid(int.Parse(ddlDepartments.SelectedValue)).Where(x => x.Usertype == "S").ToList();

                gvCompanyUsers.DataSource = getUserListByCompanyId;
                gvCompanyUsers.DataBind();
            }
            else
            {
                Bindusers();
            }
            clearFields();

            }
            catch (Exception)
            {

            
            }
        }

        private void clearFields()
        {
            //ddlDepartments.SelectedIndex = 0;
            txtName.Text = "";
            txtEmilAddress.Text = "";
            txtpassword.Text = "";
            txtusername.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divUpdateAdmin.Visible = false;
            viewUserId = 0;
            clearFields();
        }

        protected void gvCompanyUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCompanyUsers.PageIndex = e.NewPageIndex;
                if (ddlDepartments.SelectedIndex != 0)
                {
                    getUserListByCompanyId = companyLoginController.GetUserListByDepartmentid(int.Parse(ddlDepartments.SelectedValue)).Where(x => x.Usertype == "S").ToList();

                    gvCompanyUsers.DataSource = getUserListByCompanyId;
                    gvCompanyUsers.DataBind();
                }
                else
                {
                    Bindusers();
                }
            }
            catch (Exception)
            {

             
            }
        }
    }
    }
