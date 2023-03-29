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
    public partial class CustomerGRNReturn : System.Web.UI.Page
    {

        GrnController grnController = ControllerFactory.CreateGrnController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerGRNReturn.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "CustomerGRNReturnLink";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 29) && companyLogin.Usertype != "S")
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

                        List<GrnMaster> GetGrnForReturn = new List<GrnMaster>();
                        GetGrnForReturn = grnController.GetGrnForReturn(int.Parse(Session["CompanyId"].ToString()));

                        

                        gvGrn.DataSource = GetGrnForReturn.OrderBy(x => x.CreatedDate);
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
                int GrnId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                GrnId = int.Parse(gvGrn.Rows[x].Cells[0].Text);
                Response.Redirect("ReturnGRN.aspx?GrnId=" + GrnId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       


    }
}