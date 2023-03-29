using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Web.Services;
using CLibrary.Infrastructue;
using CLibrary.Common;
using CLibrary.Controller;
using System.Text;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
        GenetratePassword genetratePassword = new GenetratePassword();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

           
            if (!IsPostBack)
            {
                pnlEnterEmail.Visible = true;
                pnlVerify.Visible = false;
                pnlReset.Visible = false;
                pnlLogin.Visible = false;
            }
            lblError1.Text = "";
            lblError2.Text = "";
            }
            catch (Exception)
            {

               
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {


              
                if (supplierController.checkExistingEmailAddress(txtEmailAddress.Text))
                {
                    SupplierLogin supplierDetail = new SupplierLogin();
                    supplierDetail = supplierLoginController.SupplierLoginByEmailAddress(txtEmailAddress.Text);
                    string supplierName = supplierDetail.SupplierName;
                    Session["VerificationCode"] = genetratePassword.GetActivationCode();


                    string to = txtEmailAddress.Text; //To address    
                    string from = "navanava2810@gmail.com"; //From address    
                    MailMessage message = new MailMessage(from, to);
                    
                    string body = "<html><body><div class='col-sm-3'></div><div class='col-sm-6' style='background-color:#f7f9fe;padding:10px;border-radius:7px; margin-top:5px; width:60%; height:100%;border:solid 1px;border-color:#380799;' >";
                    body += "<div class='row' style='text-align:center;' > <h1 style='text-align:center;display:inline;color:#3a91e5;border-bottom:solid 1px;border-bottom-color:#329907;'>Reset Password</h1></div>";
                    body += "</br><p style='font-weight:bold;'>Dear "+ supplierName + ",</p>";
                    body += " <p> Please use the following verification code to verify your account for Reset password</p>";
                    body += " <p  style='font-weight:bold; font-size:large;'> " + Session["VerificationCode"] + "</p>";
                    body += "<p>Thank you,</p>";
                    body += "<p>Best regards,</p>";
                    body += "<p>Melstacorp Pvt Ltd.</p>";

                    body += "</div><div class='col-sm-3'></div></body></html>";
                    
                    message.Subject = "Email Verification for password change";
                    message.Body = body;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 25); //Gmail smtp    
                    System.Net.NetworkCredential basicCredential1 = new
                    System.Net.NetworkCredential("navanava2810@gmail.com", "navawsxzaq");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    try
                    {
                        client.Send(message);
                    }

                    catch (Exception ex)
                    {
                        lblError1.Text = "Problem occurs while sms sending";
                        pnlEnterEmail.Visible = true;
                        pnlVerify.Visible = false;
                        pnlLogin.Visible = false;
                        pnlReset.Visible = false;
                        throw ex;
                    }
                    
                    pnlEnterEmail.Visible = false;
                    pnlVerify.Visible = true;
                    pnlLogin.Visible = false;
                    pnlReset.Visible = false;
                }
                else
                {
                    lblError1.Text = "Email address does not exist!!";
                    pnlEnterEmail.Visible = true;
                    pnlVerify.Visible = false;
                    pnlLogin.Visible = false;
                    pnlReset.Visible = false;
                }
            }
            catch (Exception)
            {

                lblError1.Text = "Problem occurs while sms sending";
                pnlEnterEmail.Visible = true;
                pnlVerify.Visible = false;
                pnlLogin.Visible = false;
                pnlReset.Visible = false;
            }
        }

        protected void btnVerification_Click(object sender, EventArgs e)
        {
            try
            {

           
            if (Session["VerificationCode"].ToString() != "" || Session["VerificationCode"].ToString() != null)
            {
                if (Session["VerificationCode"].ToString() == txtVerification.Text)
                {
                    pnlVerify.Visible = false;
                    pnlReset.Visible = true;
                    pnlLogin.Visible = false;
                        pnlEnterEmail.Visible = false;
                    }
                else
                {
                    lblError2.Text = "Invalid Verification code";
                        pnlVerify.Visible = true;
                        pnlReset.Visible = false;
                        pnlLogin.Visible = false;
                        pnlEnterEmail.Visible = false;
                    }
            }
            else
            {
                lblError1.Text = "Session Time out !! Please try again";
                    pnlVerify.Visible = true;
                    pnlReset.Visible = false;
                    pnlLogin.Visible = false;
                    pnlEnterEmail.Visible = false;
                }
            }
            catch (Exception)
            {
                pnlVerify.Visible = true;
                pnlReset.Visible = false;
                pnlLogin.Visible = false;
                pnlEnterEmail.Visible = false;

            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {


                int resetPasswordStatus = supplierLoginController.resetPassword(txtEmailAddress.Text, txtPassword.Text);
                if (resetPasswordStatus > 0)
                {
                    pnlEnterEmail.Visible = false;
                    pnlReset.Visible = false;
                    pnlLogin.Visible = true;
                    pnlHeading.Visible = false;
                    pnlVerify.Visible = false;
                }
                else
                {
                    lblError3.Text = "Problem occurs in password reset";
                    pnlEnterEmail.Visible = false;
                    pnlReset.Visible = true;
                    pnlLogin.Visible = false;
                    pnlHeading.Visible = false;
                    pnlVerify.Visible = false;
                }
            }
            catch (Exception)
            {
                pnlEnterEmail.Visible = false;
                pnlReset.Visible = true;
                pnlLogin.Visible = false;
                pnlHeading.Visible = false;
                pnlVerify.Visible = false;

            }
        }

        //[WebMethod]
        //public static string login(string email, string password)
        //{
        //    try
        //    {
        //        //ClientRegistrationController clientRegistrationController = ControllerFactory.createClientRegistrationController();
        //        //string userId = clientRegistrationController.UserLogin(email, password);
        //        //if (userId != "" && userId != null)
        //        //{
        //        //    HttpContext.Current.Session["UserId"] = userId;
        //        //    return "1";
        //        //}
        //        //else
        //        //{
        //        //    return "0";
        //        //}
        //    }
        //    catch
        //    {
        //        return "0";
        //    }
       // }
    }
}