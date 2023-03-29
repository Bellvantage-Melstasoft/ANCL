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
    public partial class ViewPrForQuotationRecommendation : System.Web.UI.Page
    {
      //  static string UserId = string.Empty;
      //  int CompanyId = 0;
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
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForQuotationRecommendation.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "quotationApprovalLink";

               // UserId = Session["UserId"].ToString();
               // CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 13) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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

                        List<int> RecommendableTabulationIds = tabulationMasterController
                            .GetRecommendableTabulations(
                            int.Parse(Session["UserId"].ToString()),
                            int.Parse(Session["DesignationId"].ToString())
                            );
                        Session["RecommendableTabulationIds"] = RecommendableTabulationIds;

                        List<PrMasterV2> pr_Master = null;

                        if(RecommendableTabulationIds.Count>0)
                            pr_Master = pr_MasterController.GetPrListForQuotationApproval(int.Parse(Session["CompanyId"].ToString()), RecommendableTabulationIds).ToList();

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
                purchaseType = int.Parse(gvPurchaseRequest.Rows[x].Cells[15].Text);
                int ImportItemType = int.Parse(gvPurchaseRequest.Rows[x].Cells[18].Text);

                if (purchaseType == 1) {
                    Response.Redirect("RecommendQuotationsItemWise.aspx?PrId=" + prid);
                }
                else {
                    if (ImportItemType == 1) {
                        Response.Redirect("RecommendQuotationsItemWiseImportSparePart.aspx?PrId=" + prid);
                    }
                    else {
                        Response.Redirect("RecommendQuotationsItemWiseImport.aspx?PrId=" + prid);
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