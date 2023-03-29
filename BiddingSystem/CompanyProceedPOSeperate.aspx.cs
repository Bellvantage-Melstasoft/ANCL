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
    public partial class CompanyProceedPOSeperate : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyProceedPOSeperate.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "RaisePOLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 6) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                if (int.Parse(UserId) != 0)
                {
                    try
                    {
                        List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
                        pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentIdEditMode(CompanyId).Where(W => W.IsApproved == 0).ToList();


                        gvPurchaseOrder.DataSource = pOMasterListByDepartmentid;
                        gvPurchaseOrder.DataBind();
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
                int PoId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Session["PoId"] = PoId;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Response.Redirect("CompanyProceedPOSeperateView.aspx" ,true);
        }
    }
}