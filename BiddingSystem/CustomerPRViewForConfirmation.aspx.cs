using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class CustomerPRViewForConfirmation : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();


        protected void Page_Load(object sender, EventArgs e)
        {
            //UserId = Request.QueryString.Get("UserId");
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerPRViewForConfirmation.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "confirmApprovedPrLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                
                //if (!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 5, 3) && companyLogin.Usertype != "S")
                //{
                //    Response.Redirect("AdminDashboard.aspx");
                //}
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                if (int.Parse(UserId) != 0)
                {
                    try
                    {
                        List<PR_Master> pr_Master = new List<PR_Master>();
                        pr_Master = pr_MasterController.FetchApprovedPRForConfirmation(CompanyId).OrderByDescending(x => x.PrId).ToList();
                        gvPurchaseRequest.DataSource = pr_Master;
                        gvPurchaseRequest.DataBind();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int prid = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                Response.Redirect("CustomerViewPurchaseRequisition2.aspx?PrId=" + prid); 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}