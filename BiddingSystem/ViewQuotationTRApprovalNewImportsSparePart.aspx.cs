using BiddingSystem.Helpers;
using BiddingSystem.ViewModels.CS;
using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class ViewQuotationTRApprovalNewImportsSparePart : System.Web.UI.Page {

        #region Controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        SupplierQuotationItemController supplierQuotationItemController = ControllerFactory.CreateSupplierQuotationItemController();
        BiddingItemController biddingItemController = ControllerFactory.CreateBiddingItemController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        PR_DetailController pR_DetailController = ControllerFactory.CreatePR_DetailController();
        ImportsController importsController = ControllerFactory.createImportsController();
        #endregion
        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "") {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewQuotationTRApproval.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewPrForTabulationSheetApprovalLink";


                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 5) && companyLogin.Usertype != "S") {
                    Response.Redirect("AdminDashboard.aspx");
                }

                Session["PrID"] = Convert.ToInt32(Request.QueryString.Get("PrId"));

                Session["PRDetails"] = pr_MasterController.getPRMasterDetailByPrId(Convert.ToInt32(Session["PrID"].ToString()));
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack) {
                if (int.Parse(Session["UserId"].ToString()) != 0) {
                    try {


                        var PrId = int.Parse(Request.QueryString.Get("PrId"));
                        BindBasicDetails(PrId, Convert.ToInt32(Session["CompanyId"].ToString()));
                        LoadGV();
                        gvBids.Columns[11].Visible = false;

                    }
                    catch (Exception ex) {

                        throw;
                    }

                }
            }
        }

        protected void btnConfirmRates_Click(object sender, EventArgs e) {
            int BidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);


            List<SupplierQuotation> QuotationList = quotationController.ConfirmRates(BidId);


            gvRatss.DataSource = QuotationList;
            gvRatss.DataBind();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlRates').modal('show'); });   </script>", false);

        }

        protected void btnReopen_Click(object sender, EventArgs e) {
            try {
                var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(HttpContext.Current.Session["PrMaster"].ToString());

                int result = ControllerFactory.CreateBiddingController().ResetSelections(int.Parse(hdnBidId.Value));

                if (result > 0) {
                    result = ControllerFactory.CreateBiddingController().ReOpenBid(int.Parse(hdnBidId.Value), int.Parse(HttpContext.Current.Session["UserId"].ToString()), LocalTime.Now.AddDays(int.Parse(hdnReOpenDays.Value)), hdnReopenRemarks.Value);

                    if (result > 0) {
                        PrMaster.Bids.Remove(PrMaster.Bids.Find(b => b.BidId == int.Parse(hdnBidId.Value)));
                        HttpContext.Current.Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                        List<BiddingItem> Biddinglist = ControllerFactory.CreateBiddingController().GetPrdIdByBidId(int.Parse(hdnBidId.Value));

                        for (int i = 0; i < Biddinglist.Count; i++) {
                            int prdId = Biddinglist[i].PrdId;
                            ControllerFactory.CreatePR_DetailController().UpdatePRStatuss(int.Parse(HttpContext.Current.Session["UserId"].ToString()), prdId, "QTATNCOLECTN", "BDREOPND");
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForTabulationsheetApprovals.aspx' });; });   </script>", false);

                        }
                    }

                }
            }
            catch (Exception ex) {

            }

        }

        protected void btnDoneRate_Click(object sender, EventArgs e) {

            List<CurrencyRate> CurrencyList = new List<CurrencyRate>();

            for (int i = 0; i < gvRatss.Rows.Count; i++) {
                CurrencyRate NewCurrency = new CurrencyRate();


                NewCurrency.CurrencyTypeId = int.Parse(gvRatss.Rows[i].Cells[3].Text);
                NewCurrency.SellingRate = decimal.Parse(((TextBox)gvRatss.Rows[i].FindControl("txtRate")).Text);


                CurrencyList.Add(NewCurrency);
            }

            int result = ControllerFactory.CreateCurrencyRateController().UpdateRates(CurrencyList);

            if (result > 0) {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                gvBids.Columns[11].Visible = true;
                gvBids.Columns[12].Visible = false;

            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on Confirming Rates', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
        }

        #region Events

        protected void btnView_Click(object sender, EventArgs e) {
            try {
                int BidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
                ViewState["BidId"] = BidId;
                Session["BidId"] = BidId;

                LoadBidItems();
                //LoadBidQuotations();
                LoadBidItemSup();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {

            }
        }

        protected void Save(object sender, EventArgs e) {
            try {
                List<ImportQuotationItem> ImportQuotationItemList = new JavaScriptSerializer().Deserialize<List<ImportQuotationItem>>(ViewState["ImportQuotationItemList"].ToString());
                List<int> QuotationIds = quotationController.GetQuotationsByBidId(int.Parse(ViewState["BidId"].ToString()));
                int UpdateImports = importsController.UpdateImportValues(ImportQuotationItemList, QuotationIds);

                if (UpdateImports > 0) {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on Confirming Rates', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
                ViewState["ImportQuotationItemList"] = null;
            }
            catch (Exception ex) {

            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;
                int BidItemId = int.Parse(gvItems.DataKeys[e.Row.RowIndex].Value.ToString());
                Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));
                LoadBidImportItemsToGrid(BidItemId, gvQuotationItems, bid);
                //List<SupplierQuotation> SupplierQuotationList = quotationController.GetImportDetailsListForTabulationReview(int.Parse(ViewState["BidId"].ToString()));
                //List<ImportQuotationItem> SupplierQuotationList = ControllerFactory.createImportsController().GetImportDetailsListForTabulationReview(int.Parse(ViewState["BidId"].ToString()));
                //gvQuotationItems.DataSource = SupplierQuotationList;
                //gvQuotationItems.DataBind();
            }


        }

        private void LoadBidImportItemsToGrid(int BidItemId, GridView gridView, Bidding bid) {
            List<SupplierQuotationItem> quotationItems = new List<SupplierQuotationItem>();

            List<BiddingItem> items = bid.BiddingItems;

            DataTable dt = new DataTable();

            dt.Columns.Add("QuotationItemId");
            dt.Columns.Add("QuotationId");
            dt.Columns.Add("BiddingItemId");
            dt.Columns.Add("SupplierId");
            dt.Columns.Add("SupplierName");
            dt.Columns.Add("ReferenceCode");
            dt.Columns.Add("SupplierMentionedItemName");
            dt.Columns.Add("SupplierAgent");
            dt.Columns.Add("Country");
            dt.Columns.Add("CurrencyType");
            dt.Columns.Add("ImpBrand");
            dt.Columns.Add("Mill");
            dt.Columns.Add("ImpRemark");
            dt.Columns.Add("ImpHistory");
            dt.Columns.Add("ImpValidity");
            dt.Columns.Add("ImpEstDelivery");
            dt.Columns.Add("ImpClearing");
            dt.Columns.Add("ImpOther");
            dt.Columns.Add("TermName");
            dt.Columns.Add("UnitPrice");
            dt.Columns.Add("SubTotal");
            dt.Columns.Add("NbtAmount");
            dt.Columns.Add("VatAmount");
            dt.Columns.Add("NetTotal");
            dt.Columns.Add("PaymentModeId");
            dt.Columns.Add("NoOfDaysPaymentMode");
            dt.Columns.Add("SpecComply");
            dt.Columns.Add("ImpCIF");
            dt.Columns.Add("UnitPrice(lkr)");

            bid.SupplierQuotations.ForEach(
                sq => quotationItems.AddRange(sq.QuotationItems.Where(sqi => sqi.BiddingItemId == BidItemId && sqi.IsSelected != 2)));

            quotationItems = quotationItems.OrderBy(q => q.UnitPrice).ToList();

            for (int i = 0; i < quotationItems.Count; i++) {

                decimal UP = 0;
                decimal subTotal = 0;
                decimal NetTotal = 0;
                decimal VATValue = 0;

                decimal CIDValue = 0;
                decimal PALValue = 0;
                decimal EICValue = 0;
                decimal XIDValue = 0;
                decimal TotalDuty = 0;

                string Term = quotationItems[i].Term.ToString();
                decimal CIF = decimal.Parse(quotationItems[i].ImpCIF.ToString());
                decimal XID = decimal.Parse(quotationItems[i].xid.ToString());
                decimal CID = decimal.Parse(quotationItems[i].cid.ToString());
                decimal PAL = decimal.Parse(quotationItems[i].pal.ToString());
                decimal EIC = decimal.Parse(quotationItems[i].eic.ToString());
                decimal AirFreight = decimal.Parse(quotationItems[i].AirFreight.ToString());
                decimal Insurance = decimal.Parse(quotationItems[i].Insurance.ToString());
                decimal VatRate = decimal.Parse(quotationItems[i].VatRate.ToString());
                int HasVAT = int.Parse(quotationItems[i].HasVat.ToString());
                decimal Clearing = decimal.Parse(quotationItems[i].ImpClearing.ToString());
                decimal Other = decimal.Parse(quotationItems[i].ImpOther.ToString());
                decimal qty = decimal.Parse(quotationItems[i].Qty.ToString());
                decimal exchangeRate = ControllerFactory.CreateCurrencyRateController().GetRateByID(int.Parse(quotationItems[i].CurrencyTypeId.ToString()));

                SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[i].QuotationId);

                //for (int j = 0; j < bid.SupplierQuotations[i].QuotationItems.Count; j++) {
                //    if (quotationItems[i].QuotationId == bid.SupplierQuotations[i].QuotationItems[j].QuotationId) {




                if (Term == "1" || Term == "7" || Term == "12") {
                    decimal UPLKR = CIF * exchangeRate;

                    CIDValue = UPLKR * CID;
                    PALValue = UPLKR * PAL;
                    EICValue = UPLKR * EIC * 1.1m;
                    XIDValue = ((UPLKR * 1.15m) + (CIDValue + PALValue + EICValue)) * XID;
                    TotalDuty = 0;


                    if (HasVAT == 1) {
                        VATValue = ((UPLKR * 1.1m) + (CIDValue + PALValue + EICValue + XIDValue)) * VatRate;
                    }
                    TotalDuty = CIDValue + PALValue + EICValue + XIDValue;
                    UP = TotalDuty + Clearing + Other + UPLKR;
                    subTotal = UP * qty;
                    NetTotal = subTotal + VATValue;


                }

                else if (Term == "2" || Term == "14" || Term == "13" || Term == "15" || Term == "6") {
                    decimal UPLKR = CIF * exchangeRate;

                    CIDValue = UPLKR * CID;
                    PALValue = UPLKR * PAL;
                    EICValue = UPLKR * EIC * 1.1m;
                    XIDValue = ((UPLKR * 1.15m) + (CIDValue + PALValue + EICValue)) * XID;
                    TotalDuty = 0;


                    if (HasVAT == 1) {
                        VATValue = ((UPLKR * 1.1m) + (CIDValue + PALValue + EICValue + XIDValue)) * VatRate;
                    }
                    TotalDuty = CIDValue + PALValue + EICValue + XIDValue;
                    UP = TotalDuty + Clearing + Other + UPLKR;
                    subTotal = UP * qty;
                    NetTotal = subTotal + VATValue;

                }

                if (Term == "4" || Term == "16" || Term == "17" || Term == "3") {
                    decimal UPLKR = (CIF * exchangeRate) + (Insurance + AirFreight);

                    CIDValue = (UPLKR * CID);
                    PALValue = UPLKR * PAL;
                    EICValue = UPLKR * EIC * 1.1m;
                    XIDValue = ((UPLKR * 1.15m) + (CIDValue + PALValue + EICValue)) * XID;
                    TotalDuty = 0;


                    if (HasVAT == 1) {
                        VATValue = ((UPLKR * 1.1m) + (CIDValue + PALValue + EICValue + XIDValue)) * VatRate;
                    }
                    TotalDuty = CIDValue + PALValue + EICValue + XIDValue;
                    UP = TotalDuty + Clearing + Other + UPLKR;
                    subTotal = UP * qty;
                    NetTotal = subTotal + VATValue;

                }

                if (Term == "11") {
                    UP = CIF + Clearing + Other;
                    subTotal = UP * qty;
                    decimal VAT = 0;
                    NetTotal = 0;
                    if (HasVAT == 1) {
                        VAT = subTotal * VatRate;
                    }
                    VATValue = VAT;
                    NetTotal = subTotal + VAT;

                }

                if (Term == "9" || Term == "8") {
                    decimal UPLKR = CIF * 1.001m * exchangeRate;

                    CIDValue = UPLKR * CID;
                    PALValue = UPLKR * PAL;
                    EICValue = UPLKR * EIC * 1.1m;
                    XIDValue = ((UPLKR * 1.15m) + (CIDValue + PALValue + EICValue)) * XID;
                    TotalDuty = 0;


                    if (HasVAT == 1) {
                        VATValue = ((UPLKR * 1.1m) + (CIDValue + PALValue + EICValue + XIDValue)) * VatRate;
                    }
                    TotalDuty = CIDValue + PALValue + EICValue + XIDValue;
                    UP = TotalDuty + Clearing + Other + UPLKR;
                    subTotal = UP * qty;
                    NetTotal = subTotal + VATValue;



                }


                //    }
                //}


                DataRow newRow = dt.NewRow();


                newRow["QuotationItemId"] = quotationItems[i].QuotationItemId;
                newRow["QuotationId"] = quotationItems[i].QuotationId;
                newRow["BiddingItemId"] = quotationItems[i].BiddingItemId;
                newRow["SupplierId"] = quotation.SupplierId;
                newRow["SupplierName"] = quotation.SupplierName;
                newRow["ReferenceCode"] = quotationItems[i].ReferenceCode;
                newRow["SupplierMentionedItemName"] = quotationItems[i].SupplierMentionedItemName;
                newRow["SupplierAgent"] = quotationItems[i].SupplierAgent;
                newRow["Country"] = quotationItems[i].Country;
                newRow["CurrencyType"] = quotationItems[i].CurrencyType;
                newRow["ImpBrand"] = quotationItems[i].ImpBrand;
                newRow["Mill"] = quotationItems[i].Mill;
                newRow["ImpRemark"] = quotationItems[i].ImpRemark;
                newRow["ImpHistory"] = quotationItems[i].ImpHistory;
                newRow["ImpValidity"] = quotationItems[i].ImpValidity.AddMinutes(330);
                newRow["ImpEstDelivery"] = quotationItems[i].ImpEstDelivery;
                newRow["ImpClearing"] = quotationItems[i].ImpClearing;
                newRow["ImpOther"] = quotationItems[i].ImpOther;
                newRow["TermName"] = quotationItems[i].TermName;

                newRow["UnitPrice"] = UP.ToString("#,0.00");
                newRow["SubTotal"] = subTotal.ToString("#,0.00");
                newRow["NbtAmount"] = quotationItems[i].NbtAmount.ToString("#,0.00");
                newRow["VatAmount"] = VATValue.ToString("#,0.00");
                newRow["NetTotal"] = NetTotal.ToString("#,0.00");
                newRow["PaymentModeId"] = quotationItems[i].PaymentModeId;
                newRow["NoOfDaysPaymentMode"] = quotationItems[i].NoOfDaysPaymentMode;
                newRow["ImpCIF"] = decimal.Parse(quotationItems[i].ImpCIF.ToString("#,0.00"));
                if (quotationItems[i].TermName == "EXW" || quotationItems[i].TermName == "FOB" || quotationItems[i].TermName == "FCA" || quotationItems[i].TermName == "FAS") {
                    newRow["UnitPrice(lkr)"] = ((CIF * exchangeRate)+(AirFreight + Insurance)).ToString("#,0.00");
                }
                else if (quotationItems[i].TermName == "CNF" || quotationItems[i].TermName == "CFR" ) {
                    newRow["UnitPrice(lkr)"] = ((CIF * exchangeRate) * 1.001m).ToString("#,0.00");
                }
                else if (quotationItems[i].TermName == "Local") {
                    newRow["UnitPrice(lkr)"] = (CIF).ToString("#,0.00");
                }
                else {
                    newRow["UnitPrice(lkr)"] = (CIF * exchangeRate).ToString("#,0.00");
                }


                dt.Rows.Add(newRow);

                if (ViewState["ImportQuotationItemList"] == null) {
                    List<ImportQuotationItem> ImportQuotationItemList = new List<ImportQuotationItem>();
                    ImportQuotationItem newItem = new ImportQuotationItem();
                    newItem.ExchangeRate = exchangeRate;
                    newItem.DutyPal = TotalDuty;
                    newItem.XIDValue = XIDValue;
                    newItem.CIDValue = CIDValue;
                    newItem.PALValue = PALValue;
                    newItem.EICValue = EICValue;
                    newItem.VATValue = VATValue;
                    newItem.QuotationId = quotationItems[i].QuotationId;
                    newItem.QuptationItemId = quotationItems[i].QuotationItemId;

                    newItem.Sup_UnitPrice = UP;
                    newItem.Sup_SubTotal = subTotal;
                    newItem.Sup_Vat = VATValue;
                    newItem.Sup_Netotal = NetTotal;

                    ImportQuotationItemList.Add(newItem);

                    ViewState["ImportQuotationItemList"] = new JavaScriptSerializer().Serialize(ImportQuotationItemList);
                }
                else {
                    List<ImportQuotationItem> ImportQuotationItemList = new JavaScriptSerializer().Deserialize<List<ImportQuotationItem>>(ViewState["ImportQuotationItemList"].ToString());
                    ImportQuotationItem newItem = new ImportQuotationItem();
                    newItem.ExchangeRate = exchangeRate;
                    newItem.DutyPal = TotalDuty;
                    newItem.XIDValue = XIDValue;
                    newItem.CIDValue = CIDValue;
                    newItem.PALValue = PALValue;
                    newItem.EICValue = EICValue;
                    newItem.VATValue = VATValue;
                    newItem.QuotationId = quotationItems[i].QuotationId;
                    newItem.QuptationItemId = quotationItems[i].QuotationItemId;

                    newItem.Sup_UnitPrice = UP;
                    newItem.Sup_SubTotal = subTotal;
                    newItem.Sup_Vat = VATValue;
                    newItem.Sup_Netotal = NetTotal;

                    ImportQuotationItemList.Add(newItem);

                    ViewState["ImportQuotationItemList"] = new JavaScriptSerializer().Serialize(ImportQuotationItemList);
                }


            }

            gridView.DataSource = dt;
            gridView.DataBind();
        }


        private void LoadBidItemSup() {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(Session["BidId"].ToString()));

            try {
                List<SupplierQuotation> items = quotationController.GetSupplierQuotations(bid.BidId);
                gvQuotationItemsSup.DataSource = items;
                gvQuotationItemsSup.DataBind();
            }
            catch (Exception ex) {

            }

        }
        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());

                List<BiddingItem> listBiddingitems = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                listBiddingitems = TabulationCommon.RemoveNull(listBiddingitems);
                gvBidItems.DataSource = listBiddingitems;
                gvBidItems.DataBind();
            }
        }

        protected void btnTerminate_Click(object sender, EventArgs e) {
            var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString());
            foreach (GridViewRow row in gvBids.Rows) {
                if (row.RowType == DataControlRowType.DataRow) {
                    CheckBox CheckRow = (row.Cells[0].FindControl("CheckBoxG1") as CheckBox);
                    if (CheckRow.Checked) {

                        int bidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);

                        int result = biddingController.TerminateBid(bidId, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value.ProcessString());

                        if (result > 0) {
                            PrMaster.Bids.Find(b => b.BidId == bidId).IsTerminated = 1;
                            PrMaster.Bids.Find(b => b.BidId == bidId).BiddingItems.ForEach(bi => bi.IsTerminated = 1);
                            Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                            LoadGV();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on Terminating Bid', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        }
                    }
                }
            }
        }



        protected void gvQuotationItemsSup_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvItemSupllier = (GridView)e.Row.FindControl("gvItemSupllier");//add this

                //DataRowView dr = (DataRowView)e.Row.DataItem;
                //string field1 = dr[0].ToString();
                //int QuotationId = Convert.ToInt32(dr[1]);
                int QuotationId = int.Parse(e.Row.Cells[3].Text);
                LoadBidItemsSupplier(gvItemSupllier, QuotationId);

            }


        }

        protected void btnAttachMentsItem_Click(object sender, EventArgs e) {
            var QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);
            int BidID = Convert.ToInt32(ViewState["BidId"].ToString());
            var Attachementlist = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == BidID).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString()));

            gvDocs.DataSource = Attachementlist.UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = Attachementlist.QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = Attachementlist.TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlQuotations').modal('hide'); $('#mdlAttachments').modal('show') });   </script>", false);

        }

        protected void btnAttachMents_Click(object sender, EventArgs e) {
            var QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[3].Text);
            int BidID = Convert.ToInt32(ViewState["BidId"].ToString());
            var Attachementlist = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == BidID).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString()));

            gvDocs.DataSource = Attachementlist.UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = Attachementlist.QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = Attachementlist.TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlQuotations').modal('hide'); $('#mdlAttachments').modal('show') });   </script>", false);

        }


        protected void btnsupplerview_Click(object sender, EventArgs e) {
            var SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);
            Response.Redirect("CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>window.open ('CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId + "','_blank');</script>",false);
        }


        protected void btnPurchased_Click(object sender, EventArgs e) {
            var ItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);

            List<PODetails> listDetails = pODetailsController.GetPUrchasedItems(ItemId, int.Parse(Session["CompanyId"].ToString()));
            gvPurchasedItems.DataSource = listDetails;
            gvPurchasedItems.DataBind();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItems').modal('show'); });   </script>", false);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlQuotations').modal('hide'); $('#mdlItems').modal('show'); });   </script>", false);


        }
        protected void btnPurchasedItem_Click(object sender, EventArgs e) {
            var ItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[5].Text);

            List<PODetails> listDetails = pODetailsController.GetPUrchasedItems(ItemId, int.Parse(Session["CompanyId"].ToString()));
            gvPurchasedItems.DataSource = listDetails;
            gvPurchasedItems.DataBind();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItems').modal('show'); });   </script>", false);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlQuotations').modal('hide'); $('#mdlItems').modal('show'); });   </script>", false);


        }

        protected void lbtnApprove_Click(object sender, EventArgs e) {
            try {
                if (ViewState["ImportQuotationItemList"] != null) {
                    List<ImportQuotationItem> ImportQuotationItemList = new JavaScriptSerializer().Deserialize<List<ImportQuotationItem>>(ViewState["ImportQuotationItemList"].ToString());
                    List<int> QuotationIds = quotationController.GetQuotationsByBidId(int.Parse(ViewState["BidId"].ToString()));
                    int UpdateImports = importsController.UpdateImportValues(ImportQuotationItemList, QuotationIds);
                    string ProceedRemark = txtRemark.Text;

                    if (UpdateImports > 0) {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        ViewState["ImportQuotationItemList"] = null;
                        int approve = ControllerFactory.CreatePR_MasterController().ApproveBidTabImports(int.Parse(hdnBidId.Value), int.Parse(HttpContext.Current.Session["CompanyId"].ToString()), ProceedRemark);

                        List<BiddingItem> Biddinglist = ControllerFactory.CreateBiddingController().GetPrdIdByBidId(int.Parse(hdnBidId.Value));
                        if (approve > 0) {
                            for (int i = 0; i < Biddinglist.Count; i++) {
                                int prdId = Biddinglist[i].PrdId;
                                ControllerFactory.CreatePR_DetailController().UpdatePRStatuss(int.Parse(HttpContext.Current.Session["UserId"].ToString()), prdId, "BIDCMPRSN", "TABREVAPPROVD");
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForTabulationsheetApprovals.aspx' }); });   </script>", false);

                            }
                        }

                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on saving data', showConfirmButton: false,timer: 1500}); });   </script>", false);

                    }

                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'View quotations before Proceed the bid', showConfirmButton: false,timer: 3000}); });   </script>", false);

                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        [WebMethod]
        public static string ApproveClick(SupplierQuotationItem SupQutItem) {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {
                    string ProceedRemark = SupQutItem.ProceedRemark;
                    var PrMasterDetails = ControllerFactory.CreatePR_MasterController().getPRMasterDetailByPrId(Convert.ToInt32(HttpContext.Current.Session["PrID"].ToString()));
                    string ListCount = "";
                    ResultVM response = new ResultVM();
                    int approve = 0;
                    try {
                        //int bidId = int.Parse(HttpContext.Current.Session["BidId"].ToString());
                        int bidId = SupQutItem.BidId;
                        //  approve = ControllerFactory.CreatePR_MasterController().ApproveBidTab(bidId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString()), SupQutItem.Remark, ProceedRemark);
                        // List<PR_Details> prDetailList = ControllerFactory.CreatePR_DetailController().GetPrDetailsByPrId(int.Parse(HttpContext.Current.Session["PrID"].ToString()), int.Parse(HttpContext.Current.Session["CompanyId"].ToString()));

                        List<BiddingItem> Biddinglist = ControllerFactory.CreateBiddingController().GetPrdIdByBidId(bidId);
                        if (approve > 0) {
                            for (int i = 0; i < Biddinglist.Count; i++) {
                                int prdId = Biddinglist[i].PrdId;
                                ControllerFactory.CreatePR_DetailController().UpdatePRStatuss(int.Parse(HttpContext.Current.Session["UserId"].ToString()), prdId, "BIDCMPRSN", "TABREVAPPROVD");

                            }
                        }
                    }
                    catch (Exception ex) {
                        approve = 0;
                    }



                    if (approve > 0) {

                        response.Status = 200;
                        response.Data = "PR " + PrMasterDetails.PrCode;
                    }
                    else {
                        response.Status = 500;
                        response.Data = "Error";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }


        protected void btnBack_Click(object sender, EventArgs e) {
            Response.Redirect("ViewPrForTabulationsheetApprovals.aspx");
        }
        #endregion


        #region Methods

        public void BindBasicDetails(int prId, int CompanyId) {
            var PrMaster = pr_MasterController.GetPrForImportTabulationReview(prId, CompanyId);

            lblPRNo.Text = "PR-" + PrMaster.PrCode;
            lblCreatedOn.Text = PrMaster.CreatedDateTime.ToString("dd-MM-yyyy");
            lblCreatedBy.Text = PrMaster.CreatedByName;
            lblRequestBy.Text = PrMaster.CreatedByName;
            lblMRNApprovedBy.Text = PrMaster.ApprovedByName == null ? "Not from MRN" : PrMaster.ApprovedByName;
            lblMrnCreatedBy.Text = PrMaster.MRNCreatedByName == null ? "Not from MRN" : PrMaster.MRNCreatedByName;
            lblMrnId.Text = PrMaster.MrnCode != null ? "MRN-" + PrMaster.MrnCode : "Not From MRN";
            lblRequestFor.Text = PrMaster.RequiredFor;
            lblExpenseType.Text = (PrMaster.expenseType == "1") ? "Capital Expense" : "Operational Expense";
            lblWhAddress.Text = PrMaster.Address;
            lblWhContactNo.Text = PrMaster.PhoneNo;
            lblWhName.Text = PrMaster.Location;
            lblPurchaseType.Text = int.Parse(PrMaster.PurchaseType) == 1 ? "Local" : "Import";
            if (PrMaster.Location != null) {
                pnlWarehouse.Visible = true;
                pnlNotFound.Visible = false;
            }
            else {
                pnlNotFound.Visible = true;
                pnlWarehouse.Visible = false;
            }

            ViewState["PrId"] = prId;
            Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
        }

        private void LoadGV() {
            try {
                var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString());
                PrMaster.Bids.ForEach(b => { b.NoOfQuotations = b.SupplierQuotations.Count; });

                if ((Session["IsHeadOfProcurement"] != null && Session["IsHeadOfProcurement"].ToString() == "1")) {
                    PrMaster.Bids.ForEach(b => { b.Visibility = 1; });
                }

                List<Bidding> BiddingList = PrMaster.Bids;
                for (int i = 0; i < BiddingList.Count; i++) {
                    DateTime Startdate = BiddingList[i].StartDate;
                    DateTime Newstartdate = Startdate.AddMinutes(330);
                    BiddingList[i].StartDate = Newstartdate;

                    DateTime EndDate = BiddingList[i].EndDate;
                    DateTime NewEndDate = EndDate.AddMinutes(330);
                    BiddingList[i].EndDate = NewEndDate;
                }


                gvBids.DataSource = PrMaster.Bids.Where(b => b.IsQuotationApproved == 0 && b.IsQuotationConfirmed == 0 && b.IsTabulationReviewApproved != 1 && b.IsTabulationReviewApproved != 2);
                gvBids.DataBind();


            }
            catch (Exception ex) {
                throw ex;
            }
        }




        private static string SessionExpired() {
            return
                JsonConvert.SerializeObject(
                new ResultVM() {
                    Status = 401,
                    Data = "Session Expired"
                });
        }

        private static string ServerError() {
            return
                JsonConvert.SerializeObject(
                new ResultVM() {
                    Status = 500,
                    Data = "Server Error Occured"
                });
        }


        private void LoadBidItems() {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));
            try {
                List<BiddingItem> items = TabulationCommon.GetBidItems(bid);
                gvItems.DataSource = items;
                gvItems.DataBind();
            }
            catch (Exception EX) {

            }

        }

        private void LoadBidItemsSupplier(GridView gridView, int QuotationId) {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));
            //int TabId = int.Parse(ViewState["tabulationId"].ToString());
            int TabId = 0;
            List<BiddingItem> items = GetBidItemsSupplierForImportsReview(bid, QuotationId, TabId);
            //gridView.DataSource = items.Where(x => x.QuotedPrice != 0);
            gridView.DataSource = items;
            gridView.DataBind();


        }
        private List<BiddingItem> GetBidItemsSupplierForImportsReview(Bidding bid, int QuotationId, int TabID) {
            List<BiddingItem> items = bid.BiddingItems;
            TabulationDetailController tabulationDetailController = ControllerFactory.CreateTabulationDetailController();

            items.ForEach(itms => itms.QuotationCount = 0);



            for (int j = 0; j < bid.SupplierQuotations.Count; j++) {

                if (bid.SupplierQuotations[j].QuotationId == QuotationId) {

                    for (int i = 0; i < items.Count; i++) {

                        for (int x = 0; x < bid.SupplierQuotations[j].QuotationItems.Count; x++) {


                            if ((bid.SupplierQuotations[j].QuotationItems[x].QuotationId == QuotationId) && ((bid.SupplierQuotations[j].QuotationItems[x].BiddingItemId == items[i].BiddingItemId)) && (bid.SupplierQuotations[j].QuotationItems[x].UnitPrice > 0)) {

                                //decimal SubTotal_Sup = 0;
                                //decimal Nbt_sup = 0;
                                //decimal vat_sup = 0;
                                //decimal NetTotal_sup = 0;

                                //decimal subTotal = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].SubTotal.ToString());
                                //decimal nbt = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].NbtAmount.ToString());
                                //decimal vat = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].VatAmount.ToString());

                                //SubTotal_Sup = subTotal;
                                //Nbt_sup = nbt;
                                //vat_sup = vat;

                                //decimal TempNet = subTotal + nbt + vat;
                                //NetTotal_sup += TempNet;

                                //items[i].SubTotal_Sup = SubTotal_Sup;
                                //items[i].vat_sup = vat_sup;
                                //items[i].Nbt_sup = Nbt_sup;
                                //items[i].NetTotal_sup = NetTotal_sup;


                                //items[i].Nbt = nbt;
                                //items[i].vaT = vat;
                                items[i].RequestingQty = bid.SupplierQuotations[j].QuotationItems[x].Qty;
                                //items[i].SubTotal = Convert.ToDecimal(bid.SupplierQuotations[j].QuotationItems[x].SubTotal.ToString("#,0.00"));
                                //items[i].TotalNbT = bid.SupplierQuotations[j].QuotationItems[x].NbtAmount;
                                // items[i].TotalVaT = bid.SupplierQuotations[j].QuotationItems[x].VatAmount;
                                //items[i].NetTotal = (subTotal + nbt + vat);


                                items[i].QuotationCount = bid.SupplierQuotations.Count;
                                items[i].SupplierMentionedItemName = bid.SupplierQuotations[j].QuotationItems[x].SupplierMentionedItemName;
                                items[i].QuotationId = bid.SupplierQuotations[j].QuotationItems[x].QuotationId;
                                //items[i].Description = bid.SupplierQuotations[j].QuotationItems[x].Description;
                                items[i].QuotedPrice = bid.SupplierQuotations[j].QuotationItems[x].UnitPrice;

                                for (int u = 0; u < bid.BiddingItems.Count; u++) {
                                    if (bid.BiddingItems[u].BiddingItemId == items[i].BiddingItemId) {
                                        items[i].UnitShortName = bid.BiddingItems[u].UnitShortName;
                                    }
                                }

                                items[i].HasVat = bid.SupplierQuotations[j].QuotationItems[x].HasVat;
                                items[i].HasNbt = bid.SupplierQuotations[j].QuotationItems[x].HasNbt;
                                //items[i].UnitPrice = bid.SupplierQuotations[j].QuotationItems[x].UnitPrice;
                                items[i].RequestedTotalQty = bid.SupplierQuotations[j].QuotationItems[x].Qty;
                                items[i].TablationId = TabID;
                                items[i].ItemId = bid.SupplierQuotations[j].QuotationItems[x].ItemId;
                                // items[i].SelectedSupplierID = bid.SupplierQuotations[j].SupplierId;
                                items[i].QuotationItemId = bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId;
                                items[i].SupplierMentionedItemName = bid.SupplierQuotations[j].QuotationItems[x].SupplierMentionedItemName;
                                items[i].Mill = bid.SupplierQuotations[j].QuotationItems[x].Mill;

                                items[i].Country = bid.SupplierQuotations[j].QuotationItems[x].Country;
                                items[i].CurrencyType = bid.SupplierQuotations[j].QuotationItems[x].CurrencyType;
                                items[i].ImpBrand = bid.SupplierQuotations[j].QuotationItems[x].ImpBrand;
                                items[i].SupplierAgent = bid.SupplierQuotations[j].QuotationItems[x].SupplierAgent;
                                items[i].ImpRemark = bid.SupplierQuotations[j].QuotationItems[x].ImpRemark;
                                items[i].ImpHistory = bid.SupplierQuotations[j].QuotationItems[x].ImpHistory;
                                items[i].ImpValidity = bid.SupplierQuotations[j].QuotationItems[x].ImpValidity;
                                items[i].ImpEstDelivery = bid.SupplierQuotations[j].QuotationItems[x].ImpEstDelivery;
                                items[i].ImpClearing = bid.SupplierQuotations[j].QuotationItems[x].ImpClearing;
                                items[i].ImpOther = bid.SupplierQuotations[j].QuotationItems[x].ImpOther;
                                items[i].TermName = bid.SupplierQuotations[j].QuotationItems[x].TermName;
                                items[i].ReferenceCode = bid.SupplierQuotations[j].QuotationItems[x].ReferenceCode;
                                items[i].PaymentModeDays = bid.SupplierQuotations[j].QuotationItems[x].NoOfDaysPaymentMode;
                                items[i].PaymentModeId = bid.SupplierQuotations[j].QuotationItems[x].PaymentModeId;
                                

                                string Term = bid.SupplierQuotations[j].QuotationItems[x].Term.ToString();
                                decimal CIF = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].ImpCIF.ToString());
                                decimal XID = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].xid.ToString());
                                decimal CID = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].cid.ToString());
                                decimal PAL = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].pal.ToString());
                                decimal EIC = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].eic.ToString());
                                decimal AirFreight = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].AirFreight.ToString());
                                decimal Insurance = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].Insurance.ToString());
                                decimal VatRate = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].VatRate.ToString());
                                int HasVAT = int.Parse(bid.SupplierQuotations[j].QuotationItems[x].HasVat.ToString());
                                decimal Clearing = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].ImpClearing.ToString());
                                decimal Other = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].ImpOther.ToString());
                                decimal qty = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].Qty.ToString());

                                decimal exchangeRate = ControllerFactory.CreateCurrencyRateController().GetRateByID(int.Parse(bid.SupplierQuotations[j].QuotationItems[x].CurrencyTypeId.ToString()));
                                items[i].UnitPriceForeign = decimal.Parse(CIF.ToString("#,0.00"));

                                if (Term == "1" || Term == "7" || Term == "12") {
                                    decimal UPLKR = CIF * exchangeRate;

                                    decimal CIDValue = UPLKR * CID; 
                                    decimal PALValue = UPLKR * PAL;
                                    decimal EICValue = UPLKR * EIC * 1.1m;
                                    decimal XIDValue = ((UPLKR * 1.15m) + (CIDValue + PALValue + EICValue))*XID;
                                    decimal VATValue = 0;
                                    decimal TotalDuty = 0;
                                    decimal UP = 0;

                                    if (HasVAT == 1) {
                                        VATValue = ((UPLKR * 1.1m) + (CIDValue + PALValue + EICValue + XIDValue)) * VatRate;
                                    }
                                    TotalDuty = CIDValue + PALValue + EICValue + XIDValue;
                                    UP = TotalDuty + Clearing + Other + UPLKR;
                                    decimal subTotal = UP * qty;
                                    decimal NetTotal = subTotal + VATValue;

                                    items[i].UnitPrice = UP;
                                    items[i].SubTotal_Sup = subTotal;
                                    items[i].vat_sup = VATValue;
                                    items[i].NetTotal_sup = NetTotal;
                                    items[i].UnitPriceLkr = decimal.Parse(UPLKR.ToString("#,0.00"));


                                }

                                else if (Term == "2" || Term == "14" || Term == "13" || Term == "15" || Term == "6") {
                                    decimal UPLKR = CIF * exchangeRate;

                                    decimal CIDValue = UPLKR * CID;
                                    decimal PALValue = UPLKR * PAL;
                                    decimal EICValue = UPLKR * EIC * 1.1m;
                                    decimal XIDValue = ((UPLKR * 1.15m) + (CIDValue + PALValue + EICValue)) * XID;
                                    decimal VATValue = 0;
                                    decimal TotalDuty = 0;
                                    decimal UP = 0;

                                    if (HasVAT == 1) {
                                        VATValue = ((UPLKR * 1.1m) + (CIDValue + PALValue + EICValue + XIDValue)) * VatRate;
                                    }
                                    TotalDuty = CIDValue + PALValue + EICValue + XIDValue;
                                    UP = TotalDuty + Clearing + Other + UPLKR;
                                    decimal subTotal = UP * qty;
                                    decimal NetTotal = subTotal + VATValue;

                                    items[i].UnitPrice = UP;
                                    items[i].SubTotal_Sup = subTotal;
                                    items[i].vat_sup = VATValue;
                                    items[i].NetTotal_sup = NetTotal;
                                    items[i].UnitPriceLkr = decimal.Parse(UPLKR.ToString("#,0.00"));
                                }

                                else if (Term == "4" || Term == "16" || Term == "17" || Term == "3") {
                                    decimal UPLKR = (CIF * exchangeRate) + (Insurance + AirFreight);
                                    
                                    decimal CIDValue = (UPLKR * CID);
                                    decimal PALValue = UPLKR * PAL;
                                    decimal EICValue = UPLKR * EIC * 1.1m;
                                    decimal XIDValue = ((UPLKR * 1.15m) + (CIDValue + PALValue + EICValue)) * XID;
                                    decimal VATValue = 0;
                                    decimal TotalDuty = 0;
                                    decimal UP = 0;

                                    if (HasVAT == 1) {
                                        VATValue = ((UPLKR * 1.1m) + (CIDValue + PALValue + EICValue + XIDValue)) * VatRate;
                                    }
                                    TotalDuty = CIDValue + PALValue + EICValue + XIDValue;
                                    UP = TotalDuty + Clearing + Other + UPLKR;
                                    decimal subTotal = UP * qty;
                                    decimal NetTotal = subTotal + VATValue;

                                    items[i].UnitPrice = UP;
                                    items[i].SubTotal_Sup = subTotal;
                                    items[i].vat_sup = VATValue;
                                    items[i].NetTotal_sup = NetTotal;
                                    items[i].UnitPriceLkr = decimal.Parse(UPLKR.ToString("#,0.00"));
                                }

                                else if (Term == "11") {
                                    decimal UP = CIF + Clearing + Other;
                                    decimal subTotal = UP * qty;
                                    decimal VAT = 0;
                                    decimal NetTotal = 0;
                                    if (HasVAT == 1) {
                                        VAT = subTotal * VatRate;
                                    }
                                    NetTotal = subTotal + VAT;

                                    items[i].UnitPrice = UP;
                                    items[i].SubTotal_Sup = subTotal;
                                    items[i].vat_sup = VAT;
                                    items[i].NetTotal_sup = NetTotal;
                                    items[i].UnitPriceLkr = decimal.Parse(UP.ToString("#,0.00"));
                                }

                                else if (Term == "9" || Term == "8") {
                                    decimal UPLKR = CIF * 1.001m * exchangeRate;

                                    decimal CIDValue = UPLKR * CID;
                                    decimal PALValue = UPLKR * PAL;
                                    decimal EICValue = UPLKR * EIC * 1.1m;
                                    decimal XIDValue = ((UPLKR * 1.15m) + (CIDValue + PALValue + EICValue)) * XID;
                                    decimal VATValue = 0;
                                    decimal TotalDuty = 0;
                                    decimal UP = 0;

                                    if (HasVAT == 1) {
                                        VATValue = ((UPLKR * 1.1m) + (CIDValue + PALValue + EICValue + XIDValue)) * VatRate;
                                    }
                                    TotalDuty = CIDValue + PALValue + EICValue + XIDValue;
                                    UP = TotalDuty + Clearing + Other + UPLKR;
                                    decimal subTotal = UP * qty;
                                    decimal NetTotal = subTotal + VATValue;

                                    items[i].UnitPrice = UP;
                                    items[i].SubTotal_Sup = subTotal;
                                    items[i].vat_sup = VATValue;
                                    items[i].NetTotal_sup = NetTotal;
                                    items[i].UnitPriceLkr = decimal.Parse(UPLKR.ToString("#,0.00"));
                                }



                            }
                        }
                    }




                }

            }



            return items;

        }

        public void LoadBidQuotations() {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));

            List<BiddingItem> items = TabulationCommon.GetBidItems(bid);
            foreach (BiddingItem item in items) {
                TabulationCommon.LoadBidItemsToGrid(item.BiddingItemId, gvQuotationItemsSup, bid);
            }



        }
        #endregion

        protected void btnReopenBid_Click(object sender, EventArgs e) {

        }



        protected void Unnamed_Click(object sender, EventArgs e) {

        }

        [WebMethod]
        public static string ReOpenBidClick(SupplierQuotationItem SupQutItem) {
            try {
                if (HttpContext.Current.Session["UserId"] != null) {
                    //var PrMasterDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PRDetails"].ToString());
                    ResultVM response = new ResultVM();
                    int result = 0;
                    var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(HttpContext.Current.Session["PrMaster"].ToString());
                    int bidId = SupQutItem.BidId;



                    result = ControllerFactory.CreateBiddingController().ResetSelections(bidId);

                    if (result > 0) {
                        result = ControllerFactory.CreateBiddingController().ReOpenBid(bidId, int.Parse(HttpContext.Current.Session["UserId"].ToString()), LocalTime.Now.AddDays(int.Parse(SupQutItem.Days.ProcessString())), SupQutItem.Remark);

                        if (result > 0) {
                            PrMaster.Bids.Remove(PrMaster.Bids.Find(b => b.BidId == bidId));
                            HttpContext.Current.Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                            List<BiddingItem> Biddinglist = ControllerFactory.CreateBiddingController().GetPrdIdByBidId(bidId);

                            for (int i = 0; i < Biddinglist.Count; i++) {
                                int prdId = Biddinglist[i].PrdId;
                                ControllerFactory.CreatePR_DetailController().UpdatePRStatuss(int.Parse(HttpContext.Current.Session["UserId"].ToString()), prdId, "QTATNCOLECTN", "BDREOPND");

                            }
                        }

                    }

                    if (result > 0) {

                        response.Status = 200;
                        response.Data = "Bid Re Opened..!";
                    }
                    else {
                        response.Status = 500;
                        response.Data = "Error on Reopening Bid";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else {
                    return SessionExpired();
                }
            }
            catch (Exception ex) {
                Logger.LogError(ex);
                return ServerError();
            }
        }
        public string ProcessMyDataItem(object myValue) {
            if (myValue == null) {
                return "0";
            }

            return myValue.ToString();
        }

    }
}