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
using System.Net.Mail;
using System.Net;

namespace BiddingSystem
{
    public partial class CompanyInitialRequest : System.Web.UI.Page
    {
        int CompanyId = 0;
        string userId = string.Empty;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefSupplier";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabSupplier";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyInitialRequest.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approveSuppierLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                userId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(userId), CompanyId, 3, 1) && companyLogin.Usertype != "S" )&& companyLogin.Usertype != "GA")
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

                BindSupplierRequest();
            }
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);
        }


        private void BindSupplierRequest()

        {
            List<SupplierAssignedToCompany> GetSupplierAssigneToCompany = new List<SupplierAssignedToCompany>();
            GetSupplierAssigneToCompany = supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(CompanyId).Where(x => x.IsApproved == 0 && x.SupplierFollowing==1).ToList();
            gvApprovalSupplierList.DataSource = GetSupplierAssigneToCompany;
            gvApprovalSupplierList.DataBind();
        }
        protected void lbtnView_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                string supplierid = gvApprovalSupplierList.Rows[x].Cells[0].Text;
                Session["supplierId"] = supplierid;
                Response.Redirect("CustomerApproveSupplier.aspx");

            }
            catch (Exception)
            {

              
            }
        }


       
    protected void lbtnSendMessage_Click(object sender, EventArgs e)
    {
            try
            {
                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                string supplierEmailAddress = gvApprovalSupplierList.Rows[x].Cells[2].Text;
                lblReceiverEmailAddress.Text = supplierEmailAddress;

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);

            }
            catch (Exception)
            {

                throw;
            }
        }


        [WebMethod]
        public static string sendMessage(string emailAddress, string subject, string messageBody)
        {
            try
            {


                using (MailMessage mm = new MailMessage("ananrun10@gmail.com", emailAddress))
                {
                    mm.Subject = subject;
                    string body = messageBody;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("ananrun10@gmail.com", "qazxswedc123$");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);

                }
                return "1";

            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }

        protected void gvApprovalSupplierList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvApprovalSupplierList.PageIndex = e.NewPageIndex;
                BindSupplierRequest();
            }
            catch (Exception)
            {
                
            }
        }
    }
}