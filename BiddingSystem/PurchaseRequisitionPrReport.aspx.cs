using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class PurchaseRequisitionPrReport : System.Web.UI.Page {
        PrControllerV2 PrMasterController = ControllerFactory.CreatePrControllerV2();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MrnControllerV2 mrnController = ControllerFactory.CreateMrnControllerV2();
        TabulationMasterController tabulationMasterController = ControllerFactory.CreateTabulationMasterController();
        TabulationRecommendationController tabulationRecommendationController = ControllerFactory.CreateTabulationRecommendationController();
        TabulationApprovalController tabulationApprovalController = ControllerFactory.CreateTabulationApprovalController();

        protected void Page_Load(object sender, EventArgs e) {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "") {
                //((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                //((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                //((BiddingAdmin)Page.Master).subTabValue = "PRInquiryReport_2.aspx";
                //((BiddingAdmin)Page.Master).subTabId = "PRInquiryReport_2Link";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                //if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 5, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                //    Response.Redirect("AdminDashboard.aspx");
                //}
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack) {
                PrMasterV2 PrMaster = PrMasterController.GetPRs(int.Parse(Request.QueryString.Get("PRId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);

                if (PrMaster.MrnId != 0) {
                    pnlMrn.Visible = true;
                    MrnMasterV2 mrnMaster = mrnController.GetMRNMasterToViewRequisitionReport(PrMaster.MrnId, int.Parse(Session["CompanyId"].ToString()));
                    mrnMaster.MrnDetails = mrnController.FetchMrnDetailsList(PrMaster.MrnId, int.Parse(Session["CompanyId"].ToString()));
                    ViewState["MrnDetails"] = new JavaScriptSerializer().Serialize(mrnMaster.MrnDetails);



                    gvMrn.DataSource = new List<MrnMasterV2> { mrnMaster };
                    gvMrn.DataBind();
                }

                gvPurchaseRequest.DataSource = new List<PrMasterV2> { PrMaster };
                gvPurchaseRequest.DataBind();

                gvBids.DataSource = PrMaster.Bids;
                gvBids.DataBind();

                gvPO.DataSource = PrMaster.POsCreated;
                gvPO.DataBind();
                
                List<int> BidIds = new List<int>();
                for (int i = 0; i < PrMaster.Bids.Count; i++) {
                    int BidId = PrMaster.Bids[i].BidId;
                    BidIds.Add(BidId);
                }

                if (BidIds.Count > 0) {
                    gvRecMaster.DataSource = tabulationMasterController.GetTabulationsByBidIdForPurchaseRequisitionReport(BidIds);
                    gvRecMaster.DataBind();

                    gvAppMaster.DataSource = tabulationMasterController.GetTabulationsByBidIdForPurchaseRequisitionReport(BidIds);
                    gvAppMaster.DataBind();

                }
                //gvGrn.DataSource = PrMaster.GRNsCreated;
                //gvGrn.DataBind();

                //if (PrMaster.MrnId == 0) {

                //    gvPurchaseRequest.Columns[3].Visible = false;

                //}
                //else {
                //    gvPurchaseRequest.Columns[3].Visible = true;
                //}
            }
        }

        protected void gvMrn_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvMrnDetails = e.Row.FindControl("gvMrnDetails") as GridView;

                gvMrnDetails.DataSource = new JavaScriptSerializer().Deserialize<List<MrnDetailsV2>>(ViewState["MrnDetails"].ToString());
                gvMrnDetails.DataBind();
            }
        }


        //protected void gvGrn_RowDataBound(object sender, GridViewRowEventArgs e) {
        //    if (e.Row.RowType == DataControlRowType.DataRow) {
        //        int grnId = int.Parse(gvGrn.DataKeys[e.Row.RowIndex].Value.ToString());
        //        GridView gvGrnItems = e.Row.FindControl("gvGrnItems") as GridView;

        //        gvGrnItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).GRNsCreated.Find(grn => grn.GrnId == grnId).GrnDetailsList;
        //        gvGrnItems.DataBind();
        //    }
        //}

        protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                int poId = int.Parse(gvPO.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPoItems = e.Row.FindControl("gvPoItems") as GridView;

                gvPoItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).POsCreated.Find(po => po.PoID == poId).PoDetails;
                gvPoItems.DataBind();
            }

        }

        protected void gvAppMaster_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.DataRow) {
                    GridView gvTabAppDetails = e.Row.FindControl("gvTabAppDetails") as GridView;

                    int TabulationId = int.Parse(e.Row.Cells[1].Text);


                    gvTabAppDetails.DataSource = tabulationApprovalController.tabulationIdListForPurchadeRequisitionReportApp(TabulationId);
                    gvTabAppDetails.DataBind();
                }
            }
            catch (Exception ex) {
                throw;
            }
        }


        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e) {
             try {
                if (e.Row.RowType == DataControlRowType.DataRow) {
                    GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;
                    PrMasterV2 PR = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());

                    gvPrDetails.DataSource = PR.Items;
                    gvPrDetails.DataBind();
                }
            }
            catch (Exception ex) {
               throw;
            }
        }

        protected void gvRecMaster_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.DataRow) {
                    GridView gvTabRecDetails = e.Row.FindControl("gvTabRecDetails") as GridView;
                    
                    int TabulationId = int.Parse(e.Row.Cells[1].Text);


                    gvTabRecDetails.DataSource = tabulationRecommendationController.tabulationIdListForPurchadeRequisitionReport(TabulationId);
                    gvTabRecDetails.DataBind();
                }
            }
            catch (Exception ex) {
                throw;
            }
        }


        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;
                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());

                    PrMasterV2 PRM = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                    List<BiddingItem> bidItems = PRM.Bids.Find(b => b.BidId == bidId).BiddingItems;
                  bidItems.ForEach(bi => bi.QuotationCount = bi.SupplierQuotationItems.Count);

                    

                gvBidItems.DataSource = bidItems;
                gvBidItems.DataBind();
            }
            }
            catch (Exception ex) {
                throw;
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e) {

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { var printWindow = window.open('PurchaseRequisitionPrReportPrint.aspx?PrId=" + HttpContext.Current.Request.QueryString.Get("PrId") + "'); printWindow.print(); printWindow.onafterprint = window.close; });   </script>", false);
        }

        protected void gvBidItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow){
                PrMasterV2 PrMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());

                int BidId = int.Parse(e.Row.Cells[3].Text);
                int BidItemId = int.Parse(e.Row.Cells[2].Text);
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;

                DataTable dt = new DataTable();

                dt.Columns.Add("QuotationItemId");
                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BiddingItemId");
                dt.Columns.Add("SupplierId");
                dt.Columns.Add("SupplierName");
                dt.Columns.Add("Ratings");
                dt.Columns.Add("Description");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("ReqQty");
                dt.Columns.Add("SubTotal");
                dt.Columns.Add("NbtAmount");
                dt.Columns.Add("VatAmount");
                dt.Columns.Add("NetTotal");
                dt.Columns.Add("SpecComply");
                dt.Columns.Add("Actions");
                dt.Columns.Add("ShowReject");
                dt.Columns.Add("IsBidItemSelected");
                dt.Columns.Add("SelectedByName");
                dt.Columns.Add("SelectedDate");
                dt.Columns.Add("IsSelected");

                Bidding bid = PrMaster.Bids.Find(b => b.BidId == BidId);

                List<SupplierQuotationItem> quotationItems = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).SupplierQuotationItems;

                quotationItems = quotationItems.OrderBy(q => q.UnitPrice).ToList();

                for (int i = 0; i < quotationItems.Count; i++) {
                    SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[i].QuotationId);


                    DataRow newRow = dt.NewRow();
                    newRow["QuotationItemId"] = quotationItems[i].QuotationItemId;
                    newRow["QuotationId"] = quotationItems[i].QuotationId;
                    newRow["BiddingItemId"] = quotationItems[i].BiddingItemId;
                    newRow["SupplierId"] = quotation.SupplierId;
                    newRow["SupplierName"] = quotation.SupplierName;

                    if (quotation.SupplierRating.Rating == 0)
                        newRow["Ratings"] = "☆☆☆☆☆";
                    else if (quotation.SupplierRating.Rating == 1)
                        newRow["Ratings"] = "★☆☆☆☆";
                    else if (quotation.SupplierRating.Rating == 2)
                        newRow["Ratings"] = "★★☆☆☆";
                    else if (quotation.SupplierRating.Rating == 3)
                        newRow["Ratings"] = "★★★☆☆";
                    else if (quotation.SupplierRating.Rating == 4)
                        newRow["Ratings"] = "★★★★☆";
                    else if (quotation.SupplierRating.Rating == 5)
                        newRow["Ratings"] = "★★★★★";

                    newRow["Description"] = quotationItems[i].Description;
                    newRow["UnitPrice"] = quotationItems[i].UnitPrice.ToString("N2");
                    newRow["ReqQty"] = quotationItems[i].TQty.ToString("N2");
                    newRow["SubTotal"] = quotationItems[i].TSubTot.ToString("N2");
                    newRow["NbtAmount"] = quotationItems[i].NbtAmount.ToString("N2");
                    newRow["VatAmount"] = quotationItems[i].TVat.ToString("N2");
                    newRow["NetTotal"] = quotationItems[i].TNetTot.ToString("N2");


                    List<SupplierBOM> boms = quotationItems[i].SupplierBOMs;

                    if (boms == null || (boms != null && boms.Count == 0)) {
                        newRow["SpecComply"] = "No Specs";
                    }
                    else if (boms.Count == boms.Count(b => b.Comply == 1)) {
                        newRow["SpecComply"] = "Yes";
                    }
                    else if (boms.Count == boms.Count(b => b.Comply == 0)) {
                        newRow["SpecComply"] = "No";
                    }
                    else {
                        newRow["SpecComply"] = "Some";
                    }

                    newRow["IsBidItemSelected"] = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1 ? "1" : "0";
                    newRow["SelectedByName"] = quotationItems[i].SelectedByName;
                    if (quotationItems[i].SelectedDate == DateTime.MinValue) {
                        newRow["SelectedDate"] = "-";
                    }
                    else {
                        newRow["SelectedDate"] = quotationItems[i].SelectedDate.ToString("dd MMMM yyyy");
                    }
                    newRow["IsSelected"] = quotationItems[i].SelectedQuotation == 1 ? "1" : "0";

                    if (i == 0) {
                        if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                            newRow["Actions"] = "0";
                        else
                            newRow["Actions"] = "1";
                    }
                    else {
                        newRow["Actions"] = "0";
                    }

                    if (quotationItems.Count == 1) {
                        newRow["ShowReject"] = "0";
                    }
                    else {
                        if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                            newRow["ShowReject"] = "0";
                        else
                            newRow["ShowReject"] = "1";
                    }

                    if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsTerminated == 1) {
                        newRow["Actions"] = "0";
                        newRow["ShowReject"] = "0";

                    }
                    dt.Rows.Add(newRow);

                }


                gvQuotationItems.DataSource = dt;
                gvQuotationItems.DataBind();
            }
        }

        

        
    }
}