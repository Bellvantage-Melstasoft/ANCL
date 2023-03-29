using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.Reflection;

namespace BiddingSystem
{
    public partial class CompanyBidClosed : System.Web.UI.Page
    {
        static int CompanyId = 0;
        static string UserId = string.Empty;
        static string PrId = string.Empty;
        static string Item = string.Empty;
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyBidClosed.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "bidComparrisionLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 5) && companyLogin.Usertype != "S" )|| companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            if(!IsPostBack){
                GVBidSubmitted();
            }
        }

        private void GVBidSubmitted()
        {
            try
            {
                List<PR_Details> CompletedPR = pr_DetailController.FetchBidCompletedPR((CompanyId)).OrderBy(x => x.PrId).ToList();

                for (int a = 0; a < CompletedPR.Count; a++)
                {
                    List<SupplierQuotation> supplierQuotation = supplierQuotationController.GetDetailsBidComparison(CompletedPR[a].PrId, int.Parse(Session["CompanyId"].ToString()))
                        .Where(sq=> sq.IsSelected==0).ToList();

                    if (supplierQuotation.Count != 0)
                    {
                        bool hasBids = false;
                        for (int i = 0; i < supplierQuotation.Count; i++)
                        {
                            if (supplierQuotationController.GetBidSupplierListForItem(supplierQuotation[i].PrID, supplierQuotation[i].ItemId).Count > 0)
                            {
                                hasBids = true;
                                break;
                            }
                        }
                        if (!hasBids)
                        {
                            CompletedPR.RemoveAt(a);
                            a -= 1;
                        }
                    }
                    else
                    {
                        CompletedPR.RemoveAt(a);
                        a -= 1;
                    }
                }
                if (CompletedPR.Count != 0)
                {
                    gVClosedBids.DataSource = CompletedPR;
                    gVClosedBids.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int prid = int.Parse(gVClosedBids.Rows[x].Cells[0].Text);
                PrId = (gVClosedBids.Rows[x].Cells[0].Text);

                if (prid != 0)
                {
                    Response.Redirect("CompanyComparisionSheet.aspx?PrId=" + prid);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}