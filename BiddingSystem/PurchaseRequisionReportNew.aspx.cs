using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class PurchaseRequisionReportNew : System.Web.UI.Page {
        MrnControllerV2 mrnController = ControllerFactory.CreateMrnControllerV2();
        MRNDIssueNoteControllerInterface mrndIssueNoteController = ControllerFactory.CreateMRNDIssueNoteController();
        MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        MRNCapexDocController mRNCapexDocController = ControllerFactory.CreateMRNCapexDocController();
        PrControllerV2 prControllerV2 = ControllerFactory.CreatePrControllerV2();
        PR_DetailController pR_DetailController = ControllerFactory.CreatePR_DetailController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        BiddingItemController biddingItemController = ControllerFactory.CreateBiddingItemController();
        SupplierQuotationItemController supplierQuotationItemController = ControllerFactory.CreateSupplierQuotationItemController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        TabulationRecommendationController tabulationRecommendationController = ControllerFactory.CreateTabulationRecommendationController();
        TabulationMasterController tabulationMasterController = ControllerFactory.CreateTabulationMasterController();
        TabulationApprovalController tabulationApprovalController = ControllerFactory.CreateTabulationApprovalController();

        protected void Page_Load(object sender, EventArgs e) {
            serializer.MaxJsonLength = Int32.MaxValue;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                //((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                //((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                //((BiddingAdmin)Page.Master).subTabValue = "ViewMRN.aspx";
                //((BiddingAdmin)Page.Master).subTabId = "viewMRNLink";
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                MrnMasterV2 mrnMaster = mrnController.GetMRNMasterToViewRequisitionReport(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["MrnCode"] = "MRN" + mrnMaster.MrnCode;
                ViewState["CreatedBy"] = mrnMaster.CreatedBy;

                mrnMaster.MrnDetails = mrnController.FetchMrnDetailsList(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["MrnDetails"] = new JavaScriptSerializer().Serialize(mrnMaster.MrnDetails);


                gvMRNItems.DataSource = new List<MrnMasterV2> { mrnMaster };
                gvMRNItems.DataBind();


                List<PrMasterV2> PrMasterV2List = prControllerV2.GetPrMasterList(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                List<int> PRIds = new List<int>();


                PrMasterV2 prMaster = null;
                if (PrMasterV2List.Count > 0) {
                    for (int i = 0; i < PrMasterV2List.Count; i++) {
                        prMaster = prControllerV2.GetPrMasterToView(PrMasterV2List[i].PrId, int.Parse(Session["CompanyId"].ToString()));
                        PRIds.Add(PrMasterV2List[i].PrId);

                        if (prMaster.MrnId != 0 && prMaster.MrnId == int.Parse(Request.QueryString.Get("MrnId"))) {
                            pnlPR.Visible = true;

                        }
                    }
                }
                ViewState["PrMasterV2List"] = new JavaScriptSerializer().Serialize(PrMasterV2List);

                gvPRItems.DataSource = PrMasterV2List;
                    gvPRItems.DataBind();

                List<Bidding> BidList = new List<Bidding>();
                if (PRIds.Count > 0) {
                     BidList = biddingController.FetchBidInfoForPRRequisitionReport(PRIds);
                    gvBids.DataSource = BidList;
                    gvBids.DataBind();

                    gvPO.DataSource = pOMasterController.GetAllPosByPrIdFor(PRIds);
                    gvPO.DataBind();
                }

               

                List<int> BidIds = new List<int>();
                for (int i = 0; i < BidList.Count; i++) {
                    int Bidid = BidList[i].BidId;

                    BidIds.Add(Bidid);
                }
                if (BidIds.Count > 0) {
                    gvRecMaster.DataSource = tabulationMasterController.GetTabulationsByBidIdForPurchaseRequisitionReport(BidIds);
                    gvRecMaster.DataBind();

                    gvAppMaster.DataSource = tabulationMasterController.GetTabulationsByBidIdForPurchaseRequisitionReport(BidIds);
                    gvAppMaster.DataBind();
                }

                //gvGrn.DataSource = PrMaster.GRNsCreated;
                //gvGrn.DataBind();

            }


        }
        protected void btnPrint_Click(object sender, EventArgs e) {

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { var printWindow = window.open('PurchaseRequisionReportNewPrint.aspx?MrnId=" + HttpContext.Current.Request.QueryString.Get("MrnId") + "'); printWindow.print(); printWindow.onafterprint = window.close; });   </script>", false);
        }

        protected void gvPO_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                int poId = int.Parse(gvPO.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPoItems = e.Row.FindControl("gvPoItems") as GridView;

                gvPoItems.DataSource = pODetailsController.GetPODetailsToViewPo(poId, int.Parse(Session["CompanyId"].ToString()));
                gvPoItems.DataBind();
            }

        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.DataRow) {
                    GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;
                    int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());
                   // int biddingItemId = int.Parse(e.Row.Cells[2].Text);
                    List<BiddingItem> bidItems = biddingItemController.GetAllBidItems(bidId, int.Parse(Session["CompanyId"].ToString()));

                    //int count = 0;
                    for (int i = 0; i < bidItems.Count; i++) {
                        int count = supplierQuotationItemController.GetCount(bidItems[i].BiddingItemId);
                        bidItems[i].QuotationCount = count;
                    }
                    //bidItems.ForEach(bi => bi.QuotationCount = count);

                       
                    gvBidItems.DataSource = bidItems;
                    gvBidItems.DataBind();
                }
            }
            catch (Exception ex) {
                throw;
            }
        }
        protected void gvMRNItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvMrnDetails = e.Row.FindControl("gvMrnDetails") as GridView;

                gvMrnDetails.DataSource = new JavaScriptSerializer().Deserialize<List<MrnDetailsV2>>(ViewState["MrnDetails"].ToString());
                gvMrnDetails.DataBind();
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
        protected void gvPRItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                int prId = int.Parse(gvPRItems.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;

                gvPrDetails.DataSource = pR_DetailController.FetchByPRDetails(prId);
                gvPrDetails.DataBind();
            }
        }
        protected void gvBidItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
               // PrMasterV2 PrMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());

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

                // Bidding bid = PrMaster.Bids.Find(b => b.BidId == BidId);
                List<BiddingItem> BiddingItemList = ControllerFactory.CreateBiddingItemController().FechBiddingItems(BidId);
                List<SupplierQuotationItem> quotationItems = supplierQuotationItemController.GetAllQuotationItemsByBidItemId(BidItemId, int.Parse(Session["Companyid"].ToString()));

                quotationItems = quotationItems.OrderBy(q => q.UnitPrice).ToList();

                for (int i = 0; i < quotationItems.Count; i++) {
                    //SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[i].QuotationId);
                    SupplierQuotation quotation = supplierQuotationController.GetSupplierQuotations(BidId).Find(sq => sq.QuotationId == quotationItems[i].QuotationId);

                    DataRow newRow = dt.NewRow();
                    newRow["QuotationItemId"] = quotationItems[i].QuotationItemId;
                    newRow["QuotationId"] = quotationItems[i].QuotationId;
                    newRow["BiddingItemId"] = quotationItems[i].BiddingItemId;
                    newRow["SupplierId"] = quotation.SupplierId;
                    newRow["SupplierName"] = quotation.SupplierName;

                    //if (quotation.SupplierRating.Rating == 0)
                    //    newRow["Ratings"] = "☆☆☆☆☆";
                    //else if (quotation.SupplierRating.Rating == 1)
                    //    newRow["Ratings"] = "★☆☆☆☆";
                    //else if (quotation.SupplierRating.Rating == 2)
                    //    newRow["Ratings"] = "★★☆☆☆";
                    //else if (quotation.SupplierRating.Rating == 3)
                    //    newRow["Ratings"] = "★★★☆☆";
                    //else if (quotation.SupplierRating.Rating == 4)
                    //    newRow["Ratings"] = "★★★★☆";
                    //else if (quotation.SupplierRating.Rating == 5)
                    //    newRow["Ratings"] = "★★★★★";

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

                    newRow["IsBidItemSelected"] = BiddingItemList.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1 ? "1" : "0";
                    newRow["SelectedByName"] = quotationItems[i].SelectedByName;
                    if (quotationItems[i].SelectedDate == DateTime.MinValue) {
                        newRow["SelectedDate"] = "-";
                    }
                    else {
                        newRow["SelectedDate"] = quotationItems[i].SelectedDate.ToString("dd MMMM yyyy");
                    }
                    newRow["IsSelected"] = quotationItems[i].SelectedQuotation == 1 ? "1" : "0";

                    if (i == 0) {
                        if (BiddingItemList.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
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
                        if (BiddingItemList.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                            newRow["ShowReject"] = "0";
                        else
                            newRow["ShowReject"] = "1";
                    }

                    if (BiddingItemList.Find(bi => bi.BiddingItemId == BidItemId).IsTerminated == 1) {
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