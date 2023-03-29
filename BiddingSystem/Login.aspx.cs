using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Common;


namespace BiddingSystem
{
    public partial class Login : System.Web.UI.Page
    {
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["UserNameA"] != null && Request.Cookies["PasswordA"] != null)
                {
                    txtUserName.Text = Request.Cookies["UserNameA"].Value;
                    txtPwd.Attributes["value"] = Request.Cookies["PasswordA"].Value;
                }
                else
                {
                    txtUserName.Text = "";
                    txtPwd.Attributes["value"] = null;
                }
            }

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                CompanyLogin companyLoginObj = companyLoginController.GetCompanyLogin(txtUserName.Text, txtPwd.Text.Encrypt());

                if (companyLoginObj.UserId != -1)
                {
                    if (companyLoginObj.UserId != -2)
                    {
                        if (companyLoginObj.UserId != -3)
                        {
                            if (chkRemember.Checked)
                            {
                                Response.Cookies["UserNameA"].Expires = LocalTime.Now.AddDays(30);
                                Response.Cookies["PasswordA"].Expires = LocalTime.Now.AddDays(30);
                            }
                            else
                            {
                                Response.Cookies["UserNameA"].Expires = LocalTime.Now.AddDays(-1);
                                Response.Cookies["PasswordA"].Expires = LocalTime.Now.AddDays(-1);
                            }
                            Response.Cookies["UserNameA"].Value = txtUserName.Text.Trim();
                            Response.Cookies["PasswordA"].Value = txtPwd.Text.Trim();


                            Session["UserNameA"] = txtUserName.Text.Trim();
                            Session["CompanyId"] = companyLoginObj.DepartmentId;
                            Session["UserId"] = companyLoginObj.UserId;
                            Session["SubDepartmentID"] = companyLoginObj.SubDepartmentID;

                            int result = warehouseController.isUserHeadOfWarehouse(companyLoginObj.UserId);
                            if (result > 0)
                            {
                                Session["WarehouseID"] = result.ToString();
                                Session["IsHeadOfWarehouse"] = "1";
                            }
                            else
                            {
                                Session["IsHeadOfWarehouse"] = "0";
                            }

                            result = subDepartmentController.isUserHeadOfDepartment(companyLoginObj.UserId);
                            if (result > 0)
                            {
                                Session["SubDepartmentID"] = result.ToString();
                                Session["IsHeadOfDepartment"] = "1";
                            }
                            else
                            {
                                Session["IsHeadOfDepartment"] = "0";
                            }



                            Response.Redirect("AdminDashboard.aspx", false);

                        }
                        else
                        {
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Your Company Has Been Inactivated', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            lblError.Text = "Your Company Has Been Inactivated";
                        }
                    }
                    else
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'You Have Been Inactivated', showConfirmButton: true,timer: 4000}); });   </script>", false);
                        lblError.Text = "You Have Been Inactivated";
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Invalid Credential', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    lblError.Text = "Invalid Credential";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}