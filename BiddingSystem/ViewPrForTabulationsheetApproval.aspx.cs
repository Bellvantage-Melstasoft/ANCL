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
    public partial class ViewPrForTabulationsheetApproval : System.Web.UI.Page
    {
       // static string UserId = string.Empty;
       // int CompanyId = 0;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForTabulationsheetApproval.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewPrForTabulationSheetApprovalLink";


              //  UserId = Session["UserId"].ToString();
              //  CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 19) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                        //List<int> SelectableQuotationIds = quotationController
                        //    .GetSelectableQuotationIdsForLoggedInUser(
                        //    int.Parse(Session["UserId"].ToString()),
                        //    int.Parse(Session["DesignationId"].ToString()),
                        //    int.Parse(Session["CompanyId"].ToString())
                        //    );
                        //Session["SelectableQuotaions"] = SelectableQuotationIds;

                        //List<int> SelectionPendingQuotationIds = quotationController.GetSelectionPendingQuotationIdsForLoggedInUser(
                        //    int.Parse(Session["UserId"].ToString()),
                        //    int.Parse(Session["DesignationId"].ToString()),
                        //    int.Parse(Session["CompanyId"].ToString())
                        //    );


                        List<int> SelectionPendingBidIds = ControllerFactory.CreateBiddingController().GetSelectionPendingBidIds(
                            int.Parse(Session["UserId"].ToString()),
                            int.Parse(Session["DesignationId"].ToString()),
                            int.Parse(Session["CompanyId"].ToString())
                            );

                        Session["SelectionPendingBidIds"] = SelectionPendingBidIds;

                        List<PrMasterV2> pr_Master = null;

                        if (SelectionPendingBidIds.Count > 0)
                        {
                            pr_Master = pr_MasterController.GetPrListForQuotationComparisonOld(SelectionPendingBidIds).OrderByDescending(r => r.PrId).ToList();
                        }


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
                int prid = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[0].Text);
                Response.Redirect("ViewQuotations.aspx?PrId=" + prid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}