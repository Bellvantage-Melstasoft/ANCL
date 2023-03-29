using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;

namespace BiddingSystem
{
    public partial class ViewPrForQuotationRejectedApprovalNew : System.Web.UI.Page
    {
        //static string UserId = string.Empty;
       // int CompanyId = 0;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        TabulationMasterController tabulationMasterController = ControllerFactory.CreateTabulationMasterController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefApproval";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabApproval";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForQuotationRejectedApprovalNew.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewPrForQuotationRejectedApprovalNewLink";

                //UserId = Session["UserId"].ToString();
                //CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 13, 4) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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


                        List<int> RejectedApprovableTabulationIds = tabulationMasterController
                            .GetRejectedApprovableTabulations(
                            int.Parse(Session["UserId"].ToString()),
                            int.Parse(Session["DesignationId"].ToString())
                            );
                        Session["RejectedApprovableTabulationIds"] = RejectedApprovableTabulationIds;

                        List<PrMasterV2> pr_Master = null;

                        if(RejectedApprovableTabulationIds.Count>0)
                            pr_Master = pr_MasterController.GetPrListForQuotationAppRejected(int.Parse(Session["CompanyId"].ToString())).ToList();

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
                int purchaseType = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                purchaseType = int.Parse(gvPurchaseRequest.Rows[x].Cells[4].Text);
                int ImportItemType = int.Parse(gvPurchaseRequest.Rows[x].Cells[18].Text);
                // Response.Redirect("ApproveQuotationsNew.aspx?PrId=" + prid);

                if (purchaseType == 1) {
                    Response.Redirect("ApproveQuotationsItemWiseRejected.aspx?PrId=" + prid);
                }
                else if (purchaseType == 2) {
                    if (ImportItemType == 1) {
                        Response.Redirect("ApproveQuotationsItemWiseRejectedImportSparePart.aspx?PrId=" + prid);
                    }
                    else {
                        Response.Redirect("ApproveQuotationsItemWiseRejectedImport.aspx?PrId=" + prid);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}