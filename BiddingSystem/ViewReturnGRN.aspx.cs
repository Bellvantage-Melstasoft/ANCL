using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class ViewReturnGRN : System.Web.UI.Page
    {

        GrnController grnController = ControllerFactory.CreateGrnController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        GrnReturnMasterController grnReturnMasterController = ControllerFactory.CreateGrnReturnMasterController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewReturnGRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewReturnGRNLink";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 33) && companyLogin.Usertype != "S")
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
                if (int.Parse(Session["UserId"].ToString()) != 0)
                {
                    try
                    {

                        List<GrnReturnMaster> GetReturnedGRNs = new List<GrnReturnMaster>();
                        GetReturnedGRNs = grnReturnMasterController.GetReturnedGRN();

                        

                        gvGrn.DataSource = GetReturnedGRNs.OrderBy(x => x.ReturnedOn);
                        gvGrn.DataBind();



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
                int GrnReturnId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                GrnReturnId = int.Parse(gvGrn.Rows[x].Cells[0].Text);
                Response.Redirect("ViewReturnedGRNDetails.aspx?GrnReturnId=" + GrnReturnId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       


    }
}