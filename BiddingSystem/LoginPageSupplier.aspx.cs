using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Common;
using System.Web.Services;
using System.IO;

namespace BiddingSystem
{
    public partial class LoginPageSupplier : System.Web.UI.Page
    {
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["supplierId"] = null;
                if (Request.Cookies["UserNameS"] != null && Request.Cookies["PasswordS"] != null)
                {
                    txtUserName.Text = Request.Cookies["UserNameS"].Value;
                    txtPwd.Attributes["value"] = Request.Cookies["PasswordS"].Value;
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
                SupplierLogin SupplierLoginObj = supplierLoginController.SupplierLogin(txtUserName.Text, txtPwd.Text);
                if (SupplierLoginObj.Supplierid != 0 && SupplierLoginObj.IsActive==1)
                {

                    if (chkRemember.Checked)
                    {
                        Response.Cookies["UserNameS"].Expires = LocalTime.Now.AddDays(30);
                        Response.Cookies["PasswordS"].Expires = LocalTime.Now.AddDays(30);
                    }
                    else
                    {
                        Response.Cookies["UserNameS"].Expires = LocalTime.Now.AddDays(-1);
                        Response.Cookies["PasswordS"].Expires = LocalTime.Now.AddDays(-1);

                    }
                    Response.Cookies["UserNameS"].Value = txtUserName.Text.Trim();
                    Response.Cookies["PasswordS"].Value = txtPwd.Text.Trim();

                    
                    Session["supplierId"] = SupplierLoginObj.Supplierid;
                    Response.Redirect("SupplierInitialFrontViewInner.aspx");
                }
                else
                {
                    lblLoginmessage.Text = "Invalid Credential!!";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        

        [WebMethod]
        public static string register(string name, string email,string contactNo, string username, string password)
        {
            try
            {
                string result = string.Empty;
                SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
                SupplierController supplierController = ControllerFactory.CreateSupplierController();
                CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
                SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();

                int supplierId = supplierController.saveSupplier(name, "", "", email, "", contactNo, LocalTime.Now.ToString("yyyy-MM-dd"), "", "", 0, 0, "", 1, 0, 0, 1 );
                //int supplierId = supplierLoginController.saveSupplierLogin(username, password, email, 0, 1);
                if (supplierId != -1)
                {
                    if (supplierId > 0)
                    {
                        HttpContext.Current.Session["supplierId"] = supplierId;
                        //int saveSupplier = supplierController.saveSupplier(supplierId, name, "", "", email, "", contactNo, LocalTime.Now.ToString("yyyy-MM-dd"), "", "", 0, 0, "", 1, 0, 0, 1);
                        int saveSupplier = supplierLoginController.saveSupplierLogin(supplierId,username, password, email, 0, 1);
                        if (saveSupplier > 0)
                        {
                            string NewDirectory = HttpContext.Current.Server.MapPath("SupplierBiddingFileUpload/" + supplierId);
                            if (!Directory.Exists(NewDirectory))
                            {
                                Directory.CreateDirectory(NewDirectory);
                            }
                            result = "1";
                        }
                    }
                    else
                    {
                        result = "0";
                    }
                }
                else
                {
                    result = "-1";
                }

                return result;

            }
            catch
            {
                return "0";
            }
        }



        [WebMethod]
        public static string checkExistCompanyName(string name)
        {
            try
            {
                SupplierController supplierController = ControllerFactory.CreateSupplierController();
                bool existName= supplierController.checkExistingSuppliername(name);
                if(existName)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
               
            }
            catch (Exception)
            {

                return "0";
            }
            
        }



        private int CreateDirectoryIfNotExists(string NewDirectory)
        {
            try
            {
                int returnType = 0;
                // Checking the existance of directory
                if (!Directory.Exists(NewDirectory))
                {
                    //delete
                    //If No any such directory then creates the new one
                    Directory.CreateDirectory(NewDirectory);
                    returnType = 1;
                }
                else
                {
                    //Label1.Text = "Directory Exist";
                    returnType = 0;
                }
                return returnType;
            }
            catch (IOException _err)
            {
                throw _err;
                //Label1.Text = _err.Message; ;
            }
        }

    }
}