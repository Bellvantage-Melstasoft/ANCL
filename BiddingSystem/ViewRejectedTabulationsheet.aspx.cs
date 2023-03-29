using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class ViewRejectedTabulationsheet : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        PR_MasterController prMasterController = ControllerFactory.CreatePR_MasterController();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewRejectedTabulationsheet.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewRejectedTabulationsheetLink";

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
                        //List<int> SelectionRejectedBidIds = ControllerFactory.CreateBiddingController().GetSelectionRejectedBidIds(
                        //    int.Parse(Session["UserId"].ToString()),
                        //    int.Parse(Session["DesignationId"].ToString()),
                        //    int.Parse(Session["CompanyId"].ToString())
                        //    );

                        List<PrMasterV2> RejectedBidPrs = prMasterController.GetPrListForRejectedBids(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
                        List<int> SelectionRejectedBidIds = new List<int>();
                        if (RejectedBidPrs.Count > 0) {

                            for (int i = 0; i < RejectedBidPrs.Count; i++) {
                                List<Bidding> RejectedBids = RejectedBidPrs[i].Bids;

                                for (int j = 0; j < RejectedBids.Count; j++) {
                                    int BidId = RejectedBids[j].BidId;

                                    SelectionRejectedBidIds.Add(BidId);
                                }
                            }
                        }

                        Session["SelectionRejectedBidIds"] = SelectionRejectedBidIds;

                        List<PrMasterV2> pr_Master = null;

                        if (SelectionRejectedBidIds.Count > 0)
                        {
                            pr_Master = pr_MasterController.GetPrRejectedQuotationTabulationSheet(SelectionRejectedBidIds, int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString())).OrderByDescending(r => r.PrId).ToList();
                        }

                        ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(pr_Master);
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
                GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                int BidId = int.Parse(row.Cells[2].Text);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", " <script> window.open('manualBidSubmissionNew.aspx?BidId=" + BidId + "', '_blank'); </script>", false);
                //Response.Write("<script></ script >");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PrId = int.Parse(gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvBids = e.Row.FindControl("gvBids") as GridView;
                List<PrMasterV2> PrMasters = new JavaScriptSerializer().Deserialize<List<PrMasterV2>>(ViewState["PrMaster"].ToString());
                gvBids.DataSource = PrMasters.Find(pr => pr.PrId == PrId).Bids;
                gvBids.DataBind();
            }
        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;
                GridView Bids = (gvBidItems.NamingContainer as GridViewRow).NamingContainer as GridView;
                int PrId = int.Parse(gvPurchaseRequest.DataKeys[((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).RowIndex].Value.ToString());
                ViewState["PrId"] = PrId;
                int bidId = int.Parse(Bids.DataKeys[e.Row.RowIndex].Value.ToString());
                ViewState["BidId"] = bidId;
                List<PrMasterV2> PrMasters = new JavaScriptSerializer().Deserialize<List<PrMasterV2>>(ViewState["PrMaster"].ToString());
                gvBidItems.DataSource = PrMasters.Find(pr => pr.PrId == PrId).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();

                GridView gvSupplierQuotation = e.Row.FindControl("gvSupplierQuotation") as GridView;
                gvSupplierQuotation.DataSource = PrMasters.Find(pr => pr.PrId == PrId).Bids.Find(b => b.BidId == bidId).SupplierQuotations;
                gvSupplierQuotation.DataBind();
            }
        }

        protected void gvSupplierQuotation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvQuotationItem = e.Row.FindControl("gvQuotationItems") as GridView;
                GridView gvSupplierQuotation = (gvQuotationItem.NamingContainer as GridViewRow).NamingContainer as GridView;
                int QuotationId = int.Parse(gvSupplierQuotation.DataKeys[e.Row.RowIndex].Value.ToString());
                List<PrMasterV2> PrMasters = new JavaScriptSerializer().Deserialize<List<PrMasterV2>>(ViewState["PrMaster"].ToString());
                int prId = Convert.ToInt32(ViewState["PrId"].ToString());
                int bidId = Convert.ToInt32(ViewState["BidId"].ToString());
                List<SupplierQuotationItem> quotationItems = PrMasters.Find(pr => pr.PrId == prId).Bids.Find(b => b.BidId == bidId).SupplierQuotations.Find(t => t.QuotationId == QuotationId).QuotationItems;
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;
                gvQuotationItems.DataSource = quotationItems;
                gvQuotationItems.DataBind();
            }
        }

        protected void lbtnConfirmSubmission_Click(object sender, EventArgs e)
        {
            var bidIId = int.Parse(((GridViewRow)((Button)sender).NamingContainer).Cells[2].Text);
            List<PrMasterV2> PrMasters = new JavaScriptSerializer().Deserialize<List<PrMasterV2>>(ViewState["PrMaster"].ToString());
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            var prId = int.Parse(((GridViewRow)((Button)sender).NamingContainer).Cells[3].Text);
            PrMasterV2 prMaster = PrMasters.Find(pr => pr.PrId == prId);
            List<int> SelectionPendingBidIds = prMaster.Bids.Select(r => r.BidId).ToList();

            PrMasterV2 pr_Master = pr_MasterController.GetPrRejectedQuotationTabulationSheet(SelectionPendingBidIds, int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString())).FirstOrDefault();            
            bool hasRejectedQuotation = false;
            for (int x = 0; x < pr_Master.Bids.Count; ++x)
            {
                List<SupplierQuotation> supplierQuotation = pr_Master.Bids[x].SupplierQuotations;
                var quotationLineItem = supplierQuotation.Where(t => t.QuotationItems != null).ToList().SelectMany(y => y.QuotationItems);
                if (quotationLineItem.Where(t => t.IsQuotationItemApproved == 2).Count() > 0)
                {
                    hasRejectedQuotation = true;
                    break;
                }
            }
            if (hasRejectedQuotation)
            {
                string html =   "<div>" +
                                "Correct all <b>Rejected Quotations</b><br>" +
                                "To view rejected quotaions <b>click on plus signs </br> "+
                                "BidItems->Quotation </b><br>" +
                                "Then come back to confirm </div>";
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',html:'"+ html + "'}); });   </script>", false);
                
            }
            else
            {
                prMasterController.UpdateTabulationReviewApproval(bidIId, prMaster.CompanyId);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewRejectedTabulationsheet.aspx' }); });   </script>", false);
            }

        }
    }
}