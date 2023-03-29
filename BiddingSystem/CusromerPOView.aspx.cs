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
    public partial class CusromerPOView : System.Web.UI.Page
    {
       
      //  static string UserId = string.Empty;
      //  int CompanyId = 0;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CusromerPOView.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewPOLink";

               // CompanyId = int.Parse(Session["CompanyId"].ToString());
             //   UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 8) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                        List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
                        pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentIdViewPO(int.Parse(Session["CompanyId"].ToString()));
                        pOMasterListByDepartmentid = pOMasterListByDepartmentid.OrderBy(x => x.CreatedDate).ThenBy(t => t.POCode).ToList();
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
                //Response.Redirect("CustomerViewPurchaseOrder.aspx"); 
                Response.Redirect("ViewPOReportEmail.aspx?PoId=" + PoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}