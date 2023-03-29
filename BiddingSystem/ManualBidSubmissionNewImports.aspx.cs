using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Web.Script.Serialization;

namespace BiddingSystem {
    public partial class ManualBidSubmissionNewImports : System.Web.UI.Page {
        PR_MasterController prMasterController = ControllerFactory.CreatePR_MasterController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        SupplierBidBondDetailsController supplierBidBondDetailController = ControllerFactory.CreateSupplierBidBondDetailsController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
        SupplierQuotationController supplierQuotation = ControllerFactory.CreateSupplierQuotationController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        PR_DetailController pR_DetailController = ControllerFactory.CreatePR_DetailController();
        DefCurrencyTypeController defCurrencyTypeController = ControllerFactory.CreateDefCurrencyTypeController();
        DefPaymentModeController defPaymentModeController = ControllerFactory.CreateDefPaymentModeController();
        DefPriceTermsController defPriceTermsController = ControllerFactory.CreateDefPriceTermsController();
        DefTransportModeController defTransportModeController = ControllerFactory.CreateDefTransportModeController();
        DefContainerSizeController defContainerSizeController = ControllerFactory.CreateDefContainerSizeController();
        CurrencyRateController currencyRateController = ControllerFactory.CreateCurrencyRateController();
        ImportsHistoryController importsHistoryController = ControllerFactory.CreateImportsHistoryController();
        DutyRatesController dutyRatesController = ControllerFactory.CreateDutyRatesController();

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "") {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForManualQuotationSubmission.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ManualBidsLink";

                ViewState["UserId"] = int.Parse(Session["UserId"].ToString());
                ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 7, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                if (int.Parse(Session["UserId"].ToString()) != 0) {
                    try {
                        ViewState["BidId"] = Request.QueryString.Get("BidId");
                        var bid = biddingController.GetBidDetailsForQuotationSubmission(int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                        ViewState["Bid"] = new JavaScriptSerializer().Serialize(bid);
                        ViewState["ChooseSpecsFrom"] = "0";
                        var prMaster = prMasterController.getPRMasterDetailByPrId(bid.PrId);
                        ViewState["prMaster"] = new JavaScriptSerializer().Serialize(prMaster);
                        BidCode.InnerHtml = "BID : B" + bid.BidCode;
                        bidNote.InnerHtml = (bid.BidOpenTo == 1) ? "Quotations for this Bid can only be subimitted by Registered Suppliers" : "Quotations for this bid can be subimitted by both Registered and Not Registered Suppliers";
                        hdnEndDate.Value = bid.EndDate.ToString();
                       VAT_NBT VAtNbt = generalSettingsController.getLatestVatNbt();
                        ViewState["VAT_NBT"] = VAtNbt;
                        hdnVATRate.Value = VAtNbt.VatRate.ToString();
                        ViewState["PurchaseType"] = prMaster.PurchaseType;
                        ViewState["DeletedQuotationItemId"] = 0;
                        ViewState["IsHSCodeName"] = 0;
                        hdnEdit.Value = "0";
                        if (prMaster.PurchaseType == 2) {
                            //LoadAgentsDropdown();
                            LoadCountryDropdown();
                            ddlAgent.Visible = true;
                            ddlCountry.Visible = true;
                            ddlCurrencyDetail.Visible = true;
                            hndImport.Value = "1";

                            pnlImports.Visible = true;
                            LoadCurrencyDetail();
                            LoadPaymentMode();
                            LoadPriceTerms();
                            LoadTransportMode();
                            LoadContainerSize();
                            LoadClearingAgent();

                        }
                        LoadAllSuppliers();
                        LoadGV();
                        txtExpireDOB.Text = "";

                        var bidBondDetail = biddingController.GetBidBondDetailByBidId(int.Parse(ViewState["BidId"].ToString()));
                        ViewState["bidBondDetail"] = new JavaScriptSerializer().Serialize(bidBondDetail);
                        if (bidBondDetail.IsRequired == 1) {
                            divBidBondDetail.Visible = true;
                        }
                        int IsQuotationItemApproved = bid.SupplierQuotations.Where(x => x.QuotationItems != null).SelectMany(x => x.QuotationItems.Where(z => z.IsQuotationItemApproved == 2)).ToList().Count;
                        if (IsQuotationItemApproved > 0) {
                            gvRejectedQuotationAndItems.DataSource = bid.SupplierQuotations;
                            gvRejectedQuotationAndItems.DataBind();
                            btnShowRejectedQuotation.Visible = true;
                        }


                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }

        }

        public VAT_NBT ListVATNBTValues {
            get {
                if (ViewState["VAT_NBT"] == null)
                    ViewState["VAT_NBT"] = new List<VAT_NBT>();
                return (VAT_NBT)ViewState["VAT_NBT"];
            }
        }

        private void LoadCountryDropdown() {
            var listCountry = supplierController.getCountry();
            ddlCountry.DataSource = listCountry;
            ddlCountry.DataValueField = "Id";
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "Select Country");
        }

        private void LoadAgentsDropdown() {

        }

        private void LoadAllSuppliers() {
            try {
                var bid = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString());
                List<int> CategoryIds = new List<int>();
                for (int i = 0; i < bid.BiddingItems.Count; i++) {
                    CategoryIds.Add(bid.BiddingItems[i].CategoryId);
                }

                CategoryIds = CategoryIds.Distinct().ToList();

                if (bid.BidOpenTo == 1)
                    ddlSuppliers.DataSource = supplierController.GetApprovedSuppliersForQuotationSubmission(int.Parse(Session["CompanyId"].ToString()), CategoryIds);
                else
                    ddlSuppliers.DataSource = supplierController.GetAllSuppliersForQuotationSubmission(CategoryIds);

                ddlSuppliers.DataTextField = "SupplierName";
                ddlSuppliers.DataValueField = "SupplierId";
                ddlSuppliers.DataBind();
                ddlSuppliers.Items.Insert(0, new ListItem("Select Supplier", "0"));
            }
            catch (Exception ex) {
                throw;
            }
        }

        protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e) {
            LoadGV();
            if (ddlSuppliers.SelectedIndex != 0) {
                Bid_Bond_Details bidBondDetail = new JavaScriptSerializer().Deserialize<Bid_Bond_Details>(ViewState["bidBondDetail"].ToString());
                if (bidBondDetail.IsRequired == 1) {
                    ClearSupplierBidBondDetails();
                    LoadSupplierBidBondDetails();
                }
                var listSupplierAgent = supplierController.FetchSupplierAgent();
                //ddlAgent.DataSource = listSupplierAgent.Where(x => x.SupplierId == int.Parse(ddlSuppliers.SelectedValue)).ToList();
                //ddlAgent.DataValueField = "AgentId";
                //ddlAgent.DataTextField = "AgentName";
                ddlAgent.DataSource = listSupplierAgent;
                ddlAgent.DataValueField = "SupplierId";
                ddlAgent.DataTextField = "SupplierName";
                ddlAgent.DataBind();
                ddlAgent.Items.Insert(0, "Select Agent");
            }
        }

        private void ClearSupplierBidBondDetails() {
            txtBondNo.Text = "";
            txtBank.Text = "";
            txtBondAmount.Text = "";
            txtExpireDOB.Text = "";
            txtReceiptNo.Text = "";
        }

        private void LoadSupplierBidBondDetails() {
            if (ddlSuppliers.SelectedIndex != 0) {
                SupplierBidBondDetails model = supplierBidBondDetailController.getSupplierBidBondDetails(int.Parse(ViewState["BidId"].ToString()), int.Parse(ddlSuppliers.SelectedValue.ToString()));
                if (model.Bid_Id != 0) {
                    txtBondNo.Text = model.Bond_No;
                    txtBank.Text = model.Bank;
                    txtBondAmount.Text = model.Bond_Amount.ToString();
                    txtExpireDOB.Text = model.Expire_Date_Of_Bond.ToString("MM/dd/yyyy");
                    txtReceiptNo.Text = model.Receipt_No;
                }
            }
        }

        private bool haveNBT() {

            if (ListVATNBTValues != null) {
                if (ListVATNBTValues.NBTRate1 == 0 && ListVATNBTValues.NBTRate2 == 0) {
                    return false;
                }
            }
            return true;
        }


        private void LoadCurrencyDetail() {
            try {
                ddlCurrencyDetail.DataSource = defCurrencyTypeController.FetchDefCurrencyTypeList();
                ddlCurrencyDetail.DataValueField = "CurrencyTypeId";
                ddlCurrencyDetail.DataTextField = "CurrencyShortName";
                ddlCurrencyDetail.DataBind();
                ddlCurrencyDetail.Items.Insert(0, new ListItem("Select Currency Type", "0"));
            }
            catch (Exception ex) {
            }
        }

        private void LoadPaymentMode() {
            try {
                ddlPaymentMode.DataSource = defPaymentModeController.FetchDefPaymentModeList();
                ddlPaymentMode.DataValueField = "PaymentModeId";
                ddlPaymentMode.DataTextField = "PaymentMode";
                ddlPaymentMode.DataBind();
                ddlPaymentMode.Items.Insert(0, new ListItem("Select Payment Mode", "0"));
            }
            catch (Exception ex) {
            }
        }

        private void LoadPriceTerms() {
            try {
                //ddlPriceTerms.DataSource = defPriceTermsController.FetchDefPriceTermsList();
                //ddlPriceTerms.DataValueField = "TermId";
                //ddlPriceTerms.DataTextField = "TermName";
                //ddlPriceTerms.DataBind();
                //ddlPriceTerms.Items.Insert(0, new ListItem("Select Price Terms", "0"));
            }
            catch (Exception ex) {
            }
        }

        private void LoadTransportMode() {
            try {
                ddlTransportMode.DataSource = defTransportModeController.FetchDefTransportModeList();
                ddlTransportMode.DataValueField = "ModeId";
                ddlTransportMode.DataTextField = "Name";
                ddlTransportMode.DataBind();
                ddlTransportMode.Items.Insert(0, new ListItem("Select Mode", "0"));
            }
            catch (Exception ex) {
            }
        }

        private void LoadContainerSize() {
            try {
                ddlContainerSize.DataSource = defContainerSizeController.FetchDefContainerSizeList();
                ddlContainerSize.DataValueField = "Id";
                ddlContainerSize.DataTextField = "Size";
                ddlContainerSize.DataBind();
                ddlContainerSize.Items.Insert(0, new ListItem("Select Container Size", "0"));
            }
            catch (Exception ex) {
            }
        }
        private void LoadClearingAgent() {
            try {
                ddlClearingAgent.DataSource = supplierController.FetchClearingAgentList();
                ddlClearingAgent.DataValueField = "SupplierId";
                ddlClearingAgent.DataTextField = "SupplierName";
                ddlClearingAgent.DataBind();
                ddlClearingAgent.Items.Insert(0, new ListItem("Select Agent", "0"));
            }
            catch (Exception ex) {
            }
        }




        private void LoadGV() {
            try {
                var bid = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString());
                int hasNBTRate = 1;
                if (ListVATNBTValues != null) {
                    if (ListVATNBTValues.NBTRate1 == 0 && ListVATNBTValues.NBTRate2 == 0) {
                        hasNBTRate = 2;
                    }
                }

                DataTable dt = CreateDataTable();

                for (int i = 0; i < bid.BiddingItems.Count; i++) {
                    DataRow newRow = dt.NewRow();

                    newRow["BiddingItemId"] = bid.BiddingItems[i].BiddingItemId.ToString();
                    newRow["BidId"] = bid.BiddingItems[i].BidId.ToString();
                    newRow["PrdId"] = bid.BiddingItems[i].PrdId.ToString();
                    newRow["CategoryId"] = bid.BiddingItems[i].CategoryId.ToString();
                    newRow["CategoryName"] = bid.BiddingItems[i].CategoryName;
                    newRow["SubCategoryId"] = bid.BiddingItems[i].SubCategoryId.ToString();
                    newRow["SubCategoryName"] = bid.BiddingItems[i].SubCategoryName;
                    newRow["ItemId"] = bid.BiddingItems[i].ItemId.ToString();
                    newRow["ItemName"] = bid.BiddingItems[i].ItemName;
                    newRow["HsId"] = bid.BiddingItems[i].HsId;
                    newRow["Qty"] = bid.BiddingItems[i].Qty.ToString();
                    newRow["UnitShortName"] = bid.BiddingItems[i].UnitShortName == null ? "Not Found" : bid.BiddingItems[i].UnitShortName.ToString();
                    newRow["EstimatedPrice"] = bid.BiddingItems[i].EstimatedPrice.ToString();
                    newRow["SubTotal"] = "0.00";
                    newRow["HasNbt"] = "1";
                    newRow["HasVat"] = "";
                    newRow["NbtCalculationType"] = "1";
                    newRow["NbtAmount"] = "0.00";
                    newRow["VatAmount"] = "0.00";
                    newRow["NetTotal"] = "0.00";
                    newRow["CIF"] = "0.00";
                    newRow["UnitPriceLkr"] = "0.00";
                    newRow["CIFInLkr"] = "0.00";
                    newRow["Duty&pal"] = "0.00";
                    newRow["Clearing"] = "0.00";
                    newRow["Other"] = "0.00";
                    newRow["Other"] = "0.00";
                    newRow["VatRate"] = ListVATNBTValues != null ? ListVATNBTValues.VatRate.ToString("N4") : "0";
                    newRow["NBTRate1"] = ListVATNBTValues != null ? (ListVATNBTValues.NBTRate1 * 100).ToString("N2") + "%" : "0";
                    newRow["NBTRate2"] = ListVATNBTValues != null ? (ListVATNBTValues.NBTRate2 * 100).ToString("N2") + "%" : "0";
                    newRow["HasNBTRate"] = hasNBTRate.ToString();
                    newRow["Duty&palView"] = "0.00";
                    newRow["UnitPriceView"] = "0.00";
                    newRow["SubTotal"] = "0.00";
                    dt.Rows.Add(newRow);
                }

                gvBidItems.DataSource = dt;
                gvBidItems.DataBind();

                hdnSubTotal.Value = "0.00";
                hdnNbtTotal.Value = "0.00";
                hdnVatTotal.Value = "0.00";
                hdnNetTotal.Value = "0.00";
                txtTermsAndConditions.Text = "";
                txtSuppQuotRefCode.Text = "";

                gvDocs.DataSource = null;
                gvDocs.DataBind();

                gvImages.DataSource = null;
                gvImages.DataBind();

                gvPreviousQuotations.DataSource = null;
                gvPreviousQuotations.DataBind();

                pnlQuotations.Visible = false;
                pnlDocs.Visible = false;
                pnlImages.Visible = false;
                btnSubmit.Text = "Submit Quotation";

                if (ddlSuppliers.SelectedIndex != 0) {
                    List<SupplierQuotation> supplierQuotation = bid.SupplierQuotations.Where(sq => sq.SupplierId == int.Parse(ddlSuppliers.SelectedValue.ToString())).ToList();
                    if (supplierQuotation.Count > 0) {
                        pnlQuotations.Visible = true;
                        gvPreviousQuotations.DataSource = supplierQuotation;
                        gvPreviousQuotations.DataBind();
                    }
                }
            }
            catch (Exception ex) {

            }
        }

        private DataTable CreateDataTable() {
            DataTable dt = new DataTable();
            dt.Columns.Add("BiddingItemId");
            dt.Columns.Add("BidId");
            dt.Columns.Add("PrdId");
            dt.Columns.Add("CategoryId");
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("SubCategoryId");
            dt.Columns.Add("SubCategoryName");
            dt.Columns.Add("ItemId");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("SupplierMentionedItemName");
            dt.Columns.Add("HsId");
            dt.Columns.Add("Qty");
            dt.Columns.Add("UnitShortName");
            dt.Columns.Add("CurrencyId");
            dt.Columns.Add("ExchangeRateImp");
            dt.Columns.Add("EstimatedPrice");
            dt.Columns.Add("Description");
            dt.Columns.Add("UnitPrice");
            dt.Columns.Add("SubTotal");
            dt.Columns.Add("HasNbt");
            dt.Columns.Add("HasVat");
            dt.Columns.Add("NbtCalculationType");
            dt.Columns.Add("NbtAmount");
            dt.Columns.Add("VatAmount");
            dt.Columns.Add("VatRate");
            dt.Columns.Add("NBTRate1");
            dt.Columns.Add("NBTRate2");
            dt.Columns.Add("HasNBTRate");
            dt.Columns.Add("NetTotal");
            dt.Columns.Add("QuotationId");
            dt.Columns.Add("QuotationItemId");
            dt.Columns.Add("AgentId");
            dt.Columns.Add("Country");
            dt.Columns.Add("Brand");
            dt.Columns.Add("Mill");
            dt.Columns.Add("Terms");
            dt.Columns.Add("CIF");
            dt.Columns.Add("UnitPriceLkr");
            dt.Columns.Add("CIFInLkr");
            dt.Columns.Add("Duty&pal");
            dt.Columns.Add("Clearing");
            dt.Columns.Add("History");
            dt.Columns.Add("Validity");
            dt.Columns.Add("Other");
            //dt.Columns.Add("Refno");
            dt.Columns.Add("Estdelivery");
            dt.Columns.Add("Remark");

            dt.Columns.Add("XIDRate");
            dt.Columns.Add("CIDRate");
            dt.Columns.Add("PALRate");
            dt.Columns.Add("EICRate");
            dt.Columns.Add("XIDValue");
            dt.Columns.Add("CIDValue");
            dt.Columns.Add("PALValue");
            dt.Columns.Add("EICValue");
            dt.Columns.Add("VATRateIMP");
            dt.Columns.Add("VATValueIMP");
            dt.Columns.Add("AirFreight");
            dt.Columns.Add("Insurance");
            dt.Columns.Add("ImpEstDelivery");
            dt.Columns.Add("ImpRemark");
            dt.Columns.Add("Duty&palView");
            dt.Columns.Add("UnitPriceView");
            dt.Columns.Add("SubTotalView");

            return dt;
        }

        protected void gvPreviousQuotations_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {

                int QuotationId = int.Parse(gvPreviousQuotations.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;
                var bid = biddingController.GetBidDetailsForQuotationSubmission(int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                gvQuotationItems.DataSource = bid.SupplierQuotations.Find(sq => sq.QuotationId == QuotationId).QuotationItems;
                //gvQuotationItems.DataSource = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).SupplierQuotations.Find(sq => sq.QuotationId == QuotationId).QuotationItems;
                gvQuotationItems.DataBind();

            }
        }

        protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvSpecs = e.Row.FindControl("gvSpecs") as GridView;
                GridView gvQuotationItems = (gvSpecs.NamingContainer as GridViewRow).NamingContainer as GridView;
                int quotationId = int.Parse(gvPreviousQuotations.DataKeys[((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).RowIndex].Value.ToString());
                int quotationItemId = int.Parse(gvQuotationItems.DataKeys[e.Row.RowIndex].Value.ToString());
                gvSpecs.DataSource = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).SupplierQuotations.Find(sq => sq.QuotationId == quotationId).QuotationItems.Find(qi => qi.QuotationItemId == quotationItemId).SupplierBOMs;
                gvSpecs.DataBind();
            }
        }
        protected void TextUP_Click(object sender, EventArgs e) {

            string HSCode = hdnHs.Value;
            string term = hdnTerm.Value;
            ViewState["HSCode"] = HSCode;

            DutyRates Rates = ControllerFactory.CreateDutyRatesController().GetRatesByHSCode(HSCode);
            txtInsurance.Text = "0.00";
            txtAirFreight.Text = "0.00";
            txtCID.Text = Rates.CID == 0 ? "0.00" : Rates.CID.ToString();
            txtEIC.Text = Rates.EIC == 0 ? "0.00" : Rates.EIC.ToString();
            txtPAL.Text = Rates.PAL == 0 ? "0.00" : Rates.PAL.ToString();
            txtXID.Text = Rates.XID == 0 ? "0.00" : Rates.XID.ToString();

            if (term == "3" || term == "4" || term == "16" || term == "17") {
                pnlInsurance.Visible = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  $('#mdlInsuaranceAF').modal('show'); });   </script>", false);

            }
            else if(term == "1" || term == "7" || term == "12" || term == "2" || term == "14" || term == "13" || term == "15" || term == "6" || term == "9" || term == "8" ) {
                pnlInsurance.Visible = false;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  $('#mdlInsuaranceAF').modal('show'); });   </script>", false);

            }

            hdnHs.Value = "0";
            hdnTerm.Value = "0";
        }

        protected void btnEdit_Click(object sender, EventArgs e) {
            msg.Visible = false;
            hdnEdit.Value = "1";
            var bid = biddingController.GetBidDetailsForQuotationSubmissionImports(int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

            //var bid = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString());
            var quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text.ToString()));
            ViewState["Quotation"] = new JavaScriptSerializer().Serialize(quotation);
            txtSuppQuotRefCode.Text = quotation.QuotationReferenceCode;
            int hasNBTRate = 1;
            if (ListVATNBTValues != null) {
                if (ListVATNBTValues.NBTRate1 == 0 && ListVATNBTValues.NBTRate2 == 0) {
                    hasNBTRate = 2;
                }
            }

            ddlAgent.SelectedValue = quotation.SupplierAgent.ToString();
            ddlClearingAgent.SelectedValue = quotation.AgentId.ToString();
            ddlCurrencyDetail.SelectedValue = quotation.CurrencyTypeId.ToString();
            ddlContainerSize.SelectedValue = quotation.ContainersizeId.ToString();
            ddlPaymentMode.SelectedValue = quotation.PaymentModeId.ToString();
            ddlCountry.SelectedValue = quotation.Country.ToString();
            ddlTransportMode.SelectedValue = quotation.TransportModeId.ToString();

            ddlAgent.Enabled = false;
            //ddlClearingAgent.Enabled = false;
            ddlCurrencyDetail.Enabled = false;
            //ddlContainerSize.Enabled = false;
            //ddlPaymentMode.Enabled = false;
            ddlCountry.Enabled = false;
            //ddlTransportMode.Enabled = false;
            txtNoOfDays.Text = quotation.PaymentModeDays.ToString();

            
            if (quotation.PaymentModeId == 4 || quotation.PaymentModeId == 5) {
                pnlNoOfDays.Visible = true;
            }
            else {
                pnlNoOfDays.Visible = false;
            }


            DataTable dt = CreateDataTable();

            for (int i = 0; i < quotation.QuotationItems.Count; i++) {

                

                DataRow newRow = dt.NewRow();

                newRow["BiddingItemId"] = quotation.QuotationItems[i].BiddingItemId.ToString();
                newRow["BidId"] = quotation.BidId.ToString();
                newRow["PrdId"] = bid.BiddingItems.Find(bi => bi.BiddingItemId == quotation.QuotationItems[i].BiddingItemId).PrdId.ToString();
                newRow["CategoryId"] = quotation.QuotationItems[i].CategoryId.ToString();
                newRow["CategoryName"] = quotation.QuotationItems[i].CategoryName;
                newRow["SubCategoryId"] = quotation.QuotationItems[i].SubCategoryId.ToString();
                newRow["SubCategoryName"] = quotation.QuotationItems[i].SubCategoryName;
                newRow["ItemId"] = quotation.QuotationItems[i].ItemId.ToString();
                newRow["ItemName"] = quotation.QuotationItems[i].ItemName;
                newRow["SupplierMentionedItemName"] = quotation.QuotationItems[i].SupplierMentionedItemName.ToString();
                //newRow["HsId"] = bid.BiddingItems[i].HsId.ToString();
                newRow["HsId"] = quotation.QuotationItems[i].NewHSId.ToString();
                newRow["Qty"] = quotation.QuotationItems[i].Qty.ToString();
                newRow["UnitShortName"] = bid.BiddingItems[i].UnitShortName == null ? "Not Found" : bid.BiddingItems[i].UnitShortName.ToString();
                newRow["CurrencyId"] = quotation.QuotationItems[i].CurrencyId.ToString();
                newRow["ExchangeRateImp"] = quotation.QuotationItems[i].ExchangeRateImp.ToString();
                newRow["Description"] = quotation.QuotationItems[i].Description.ToString();
                newRow["AgentId"] = quotation.QuotationItems[i].AgentId.ToString();// Later chnage
                newRow["Country"] = quotation.QuotationItems[i].Country.ToString();// Later chnage
                if (!string.IsNullOrEmpty(quotation.QuotationItems[i].ImpBrand)) {
                    newRow["Brand"] = quotation.QuotationItems[i].ImpBrand.ToString();
                }
                else {
                    newRow["Brand"] = "";
                }

                if (!string.IsNullOrEmpty(quotation.QuotationItems[i].Term)) {
                    newRow["Terms"] = quotation.QuotationItems[i].Term.ToString();
                }
                else {
                    newRow["Terms"] = "";
                }

                
                newRow["Mill"] = quotation.QuotationItems[i].Mill;
                newRow["EstimatedPrice"] = quotation.QuotationItems[i].EstimatedPrice;
                newRow["UnitPrice"] = quotation.QuotationItems[i].UnitPrice.ToString();
                newRow["SubTotal"] = quotation.QuotationItems[i].SubTotal.ToString();
                newRow["HasNbt"] = quotation.QuotationItems[i].HasNbt.ToString();
                newRow["HasVat"] = quotation.QuotationItems[i].HasVat.ToString();
                newRow["NbtCalculationType"] = quotation.QuotationItems[i].NbtCalculationType.ToString();
                newRow["NbtAmount"] = quotation.QuotationItems[i].NbtAmount.ToString("N2");
                newRow["VatAmount"] = quotation.QuotationItems[i].VatAmount.ToString("N2");
                newRow["NetTotal"] = quotation.QuotationItems[i].TotalAmount.ToString("N2");
                newRow["CIF"] = quotation.QuotationItems[i].ImpCIF.ToString();  // Later chnage
                newRow["Duty&pal"] = quotation.QuotationItems[i].DutypalImp.ToString();// Later chnage
                newRow["Clearing"] = quotation.QuotationItems[i].ImpClearing.ToString(); // Later chnage
                newRow["History"] = quotation.QuotationItems[i].ImpHistoryID;
                newRow["Validity"] = quotation.QuotationItems[i].ImpValidity.ToString("yyyy-MM-dd");
                newRow["Other"] = quotation.QuotationItems[i].ImpOther.ToString(); // Later chnage
                newRow["VatRate"] = ListVATNBTValues != null ? ListVATNBTValues.VatRate.ToString("N4") : "0";
                newRow["NBTRate1"] = ListVATNBTValues != null ? (ListVATNBTValues.NBTRate1 * 100).ToString("N2") + "%" : "0";
                newRow["NBTRate2"] = ListVATNBTValues != null ? (ListVATNBTValues.NBTRate2 * 100).ToString("N2") + "%" : "0";
                newRow["HasNBTRate"] = hasNBTRate.ToString();
              
                newRow["XIDRate"] = quotation.QuotationItems[i].xid.ToString();
                newRow["CIDRate"] = quotation.QuotationItems[i].cid.ToString();
                newRow["PALRate"] = quotation.QuotationItems[i].pal.ToString();
                newRow["EICRate"] = quotation.QuotationItems[i].eic.ToString();
                newRow["XIDValue"] = quotation.QuotationItems[i].XIDValue.ToString();
                newRow["CIDValue"] = quotation.QuotationItems[i].CIDValue.ToString();
                newRow["PALValue"] = quotation.QuotationItems[i].PALValue.ToString();
                newRow["EICValue"] = quotation.QuotationItems[i].EICValue.ToString();
                newRow["VATRateIMP"] = quotation.QuotationItems[i].VatRate.ToString();
                newRow["VATValueIMP"] = quotation.QuotationItems[i].VATValueIMP.ToString();
                newRow["AirFreight"] = quotation.QuotationItems[i].AirFreight.ToString();
                newRow["Insurance"] = quotation.QuotationItems[i].Insurance.ToString();

                newRow["ImpEstDelivery"] = quotation.QuotationItems[i].ImpEstDelivery.ToString();
                newRow["ImpRemark"] = quotation.QuotationItems[i].ImpRemark.ToString();

                

                if (quotation.QuotationItems[i].Term.ToString() == "1" || quotation.QuotationItems[i].Term.ToString() == "7" || quotation.QuotationItems[i].Term.ToString() == "12") {
                    newRow["UnitPriceLkr"] = (quotation.QuotationItems[i].ImpCIF * quotation.QuotationItems[i].ExchangeRateImp).ToString("N2");
                    newRow["CIFInLkr"] = "0.00";
                }

                if (quotation.QuotationItems[i].Term.ToString() == "8" || quotation.QuotationItems[i].Term.ToString() == "9" ) {
                    newRow["CIFInLkr"] = (quotation.QuotationItems[i].ImpCIF * quotation.QuotationItems[i].ExchangeRateImp * 1.001m).ToString("N2");
                    newRow["UnitPriceLkr"] = "0.00";
                }

                if (quotation.QuotationItems[i].Term.ToString() == "2" || quotation.QuotationItems[i].Term.ToString() == "14" || quotation.QuotationItems[i].Term.ToString() == "13" || quotation.QuotationItems[i].Term.ToString() == "15" || quotation.QuotationItems[i].Term.ToString() == "6" ) {
                    newRow["CIFInLkr"] = newRow["UnitPriceLkr"] = (quotation.QuotationItems[i].ImpCIF * quotation.QuotationItems[i].ExchangeRateImp).ToString("N2");
                    newRow["UnitPriceLkr"] = "0.00";
                }

                if (quotation.QuotationItems[i].Term.ToString() == "4" || quotation.QuotationItems[i].Term.ToString() == "16" || quotation.QuotationItems[i].Term.ToString() == "17" || quotation.QuotationItems[i].Term.ToString() == "3" ) {
                    newRow["CIFInLkr"] = newRow["UnitPriceLkr"] = ((quotation.QuotationItems[i].ImpCIF * quotation.QuotationItems[i].ExchangeRateImp) + (quotation.QuotationItems[i].AirFreight + quotation.QuotationItems[i].Insurance)).ToString("N2");
                    newRow["UnitPriceLkr"] = "0.00";
                }
                if (quotation.QuotationItems[i].Term.ToString() == "11" ) {
                    newRow["UnitPriceLkr"] = newRow["UnitPriceLkr"] = (quotation.QuotationItems[i].ImpCIF).ToString("N2");
                    newRow["CIFInLkr"] = "0.00";
                }
                newRow["Duty&palView"] = quotation.QuotationItems[i].DutypalImp.ToString("N2");// Later chnage
                newRow["UnitPriceView"] = quotation.QuotationItems[i].UnitPrice.ToString("N2");
                newRow["SubTotalView"] = quotation.QuotationItems[i].SubTotal.ToString("N2");
                dt.Rows.Add(newRow);
            }

            gvBidItems.DataSource = dt;
            gvBidItems.DataBind();

            //for import
            //for (int i = 0; i < gvBidItems.Rows.Count; i++)
            //{
            //    if (quotation.QuotationItems != null)
            //    {
            //        if (quotation.QuotationItems[i].Terms != "0" && quotation.QuotationItems[i].Terms != "LOCAL")
            //        {
            //            (gvBidItems.Rows[i].FindControl("txtdutypal") as TextBox).Enabled = true;
            //            (gvBidItems.Rows[i].FindControl("txtClearing") as TextBox).Enabled = true;
            //            (gvBidItems.Rows[i].FindControl("txtCif") as TextBox).Enabled = true;
            //            (gvBidItems.Rows[i].FindControl("txtother") as TextBox).Enabled = true;
            //        }
            //    }

            //}

            hdnSubTotal.Value = quotation.SubTotal.ToString();
            hdnNbtTotal.Value = quotation.NbtAmount.ToString();
            hdnVatTotal.Value = quotation.VatAmount.ToString();
            hdnNetTotal.Value = quotation.NetTotal.ToString();

            txtTermsAndConditions.Text = quotation.TermsAndCondition;

            gvDocs.DataSource = quotation.UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = quotation.QuotationImages;
            gvImages.DataBind();

            pnlDocs.Visible = true;
            pnlImages.Visible = true;
            ViewState["ChooseSpecsFrom"] = "1";
            btnSubmit.Text = "Update Quotation";

        }

        protected void btnNewQuotationSubmit_Click(object sender, EventArgs e) {
            //LoadGV();
            Page.MaintainScrollPositionOnPostBack = false;
            Page.SetFocus(divToScroll);
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", " <script> $(document).ready(function () { fnScroll(); });</script>", false);
            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('html,body').animate({ scrollTop: $('#ContentSection_gvBidItems').offset().top},  'slow') });   </script>", false);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
            //    "<script> $(document).ready(function () {   $('html,body').animate({ scrollTop: $('#ContentSection_gvBidItems').offset().top},  'slow'); });   </script> "
            //    , false);
        }

        protected void gvBidItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Header) {
                if (ListVATNBTValues != null) {
                    if (ListVATNBTValues.NBTRate1 == 0 && ListVATNBTValues.NBTRate2 == 0) {
                        e.Row.Cells[29].Text = "Include VAT";
                    }
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow) {
                int biddingItemId = int.Parse(gvBidItems.DataKeys[e.Row.RowIndex].Value.ToString());
                GridViewRow gvrow = e.Row;
                GridView gvSpecs = e.Row.FindControl("gvSpecs") as GridView;
                int HistoryselectedVal = 0;
                int termselectedVal = 0;

                TextBox txtHistory = e.Row.FindControl("txtHHistory") as TextBox;
                if (txtHistory.Text != "") {
                    HistoryselectedVal = int.Parse(txtHistory.Text);
                }
                TextBox txtterm = e.Row.FindControl("txtHTerm") as TextBox;
                if (txtterm.Text != "") {
                    termselectedVal = int.Parse(txtterm.Text);
                }

                TextBox txtHasVAT = e.Row.FindControl("txtHasVAT") as TextBox;
                CheckBox chkVAT = e.Row.FindControl("chkVat") as CheckBox;
                if (txtHasVAT.Text == "") {
                    chkVAT.Checked = true;
                    chkVAT.Enabled = false;
                }
                if (txtHasVAT.Text == "0") {
                    chkVAT.Checked = false;
                    chkVAT.Enabled = false;
                }
                if (txtHasVAT.Text == "1") {
                    chkVAT.Checked = true;
                    chkVAT.Enabled = true;
                }


                DropDownList drop = e.Row.FindControl("dllCurrency") as DropDownList;
                drop.DataSource = unitMeasurementController.fetchCurrency();
                drop.DataValueField = "Id";
                drop.DataTextField = "Name";
                drop.DataBind();

                DropDownList ddlDrop = e.Row.FindControl("ddlTerms") as DropDownList;
                ddlDrop.Enabled = true;
                ddlDrop.DataSource = defPriceTermsController.FetchDefPriceTermsList();
                ddlDrop.DataValueField = "TermId";
                ddlDrop.DataTextField = "TermName";
                ddlDrop.DataBind();
                ddlDrop.Items.Insert(0, new ListItem("Select Terms", "0"));
                ddlDrop.SelectedValue = termselectedVal.ToString();

                DropDownList ddlHistory = e.Row.FindControl("ddlHistory") as DropDownList;
                ddlHistory.Enabled = true;
                ddlHistory.DataSource = importsHistoryController.GetHistoryForQuotationSubmission();
                ddlHistory.DataValueField = "HistoryId";
                ddlHistory.DataTextField = "History";
                ddlHistory.DataBind();
                ddlHistory.Items.Insert(0, new ListItem("Select", "0"));
                ddlHistory.SelectedValue = HistoryselectedVal.ToString();

                TextBox txt = e.Row.FindControl("txtExchangeRate") as TextBox;
                if (txt.Text == null || txt.Text =="")  {
                    txt.Text = "1";  // Default Value is 1 
                }
                TextBox txtHSID = e.Row.FindControl("txtHSID") as TextBox;

                PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["prMaster"].ToString());
                if (prMaster.PurchaseType == 2) {
                    drop.Enabled = false;
                    txt.Enabled = true;
                    txtHSID.Enabled = true;

                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Material");
                dt.Columns.Add("Description");
                dt.Columns.Add("Comply");

                if (ViewState["ChooseSpecsFrom"].ToString() == "0") {
                    List<PrBomV2> boms = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).BiddingItems.Find(bi => bi.BiddingItemId == biddingItemId).PrDetail.PrBoms;
                    for (int i = 0; i < boms.Count; i++) {
                        DataRow newRow = dt.NewRow();
                        newRow["Material"] = boms[i].Material;
                        newRow["Description"] = boms[i].Description;
                        newRow["Comply"] = "1";
                        dt.Rows.Add(newRow);
                    }
                }
                else {
                    List<SupplierBOM> boms = null;
                    var quotation = new JavaScriptSerializer().Deserialize<SupplierQuotation>(ViewState["Quotation"].ToString());
                    if (quotation.QuotationItems.Find(qi => qi.BiddingItemId == biddingItemId) != null) {
                        boms = quotation.QuotationItems.Find(qi => qi.BiddingItemId == biddingItemId).SupplierBOMs;
                        for (int i = 0; i < boms.Count; i++) {
                            DataRow newRow = dt.NewRow();
                            newRow["Material"] = boms[i].Material;
                            newRow["Description"] = boms[i].Description;
                            newRow["Comply"] = boms[i].Comply.ToString();
                            dt.Rows.Add(newRow);
                        }
                    }
                }

                gvSpecs.DataSource = dt;
                gvSpecs.DataBind();

                if (hndImport.Value == "1") {
                    //DropDownList ddlDrop = e.Row.FindControl("ddlTerms") as DropDownList;
                    //ddlDrop.Enabled = true;



                    //DropDownList ddlhistory = e.Row.FindControl("ddlHistory") as DropDownList;
                    //ddlhistory.Enabled = true;

                    TextBox brand = e.Row.FindControl("txtBrand") as TextBox;
                    brand.Enabled = true;

                    //TextBox unitprc = e.Row.FindControl("txtUnitPrice") as TextBox;
                    //unitprc.Enabled = false;

                    CheckBox chknbt = e.Row.FindControl("chkNbt") as CheckBox;
                    chknbt.Checked = false;
                }
                if (hdnEdit.Value == "1") {
                    TextBox HsCode = e.Row.FindControl("txtHSID") as TextBox;
                    HsCode.Enabled = false;

                    //TextBox txtUnitPriceLkrView = e.Row.FindControl("txtUnitPriceLkrView") as TextBox;
                    //txtUnitPriceLkrView.Enabled = false;

                    //TextBox txtCIFInLkrView = e.Row.FindControl("txtCIFInLkrView") as TextBox;
                    //txtCIFInLkrView.Enabled = false;

                    //TextBox txtDutypalCalView = e.Row.FindControl("txtDutypalCalView") as TextBox;
                    //txtDutypalCalView.Enabled = false;

                    //TextBox txtUnitPriceView = e.Row.FindControl("txtUnitPriceView") as TextBox;
                    //txtUnitPriceView.Enabled = false;

                    //TextBox txtSubTotalView = e.Row.FindControl("txtSubTotalView") as TextBox;
                    //txtSubTotalView.Enabled = false;

                    //TextBox txtNetTotalView = e.Row.FindControl("txtNetTotalView") as TextBox;
                    //txtNetTotalView.Enabled = false;

                    int Term =  int.Parse(ddlDrop.SelectedValue);
                    if (Term != 0) {
                        if (Term == 1 || Term == 7 || Term == 12) {
                            TextBox txtCifPriceLKR = e.Row.FindControl("txtCIFInLkr") as TextBox;
                            txtCifPriceLKR.Enabled = false;
                        }
                        else {
                            TextBox txtPriceLKR = e.Row.FindControl("txtUnitPriceLkr") as TextBox;
                            txtPriceLKR.Enabled = false;
                        }
                    }
                }
            }

            if (ListVATNBTValues != null) {
                if (ListVATNBTValues.NBTRate1 == 0 && ListVATNBTValues.NBTRate2 == 0) {
                    e.Row.Cells[30].CssClass = "hidden";   // hidding NBT Radio button
                    e.Row.Cells[31].CssClass = "hidden";   // hidding NBT Value Column
                }
            }

            if (hndImport.Value != "1")  // if local
            {
                for (int i = 12; i < 23; i++) {
                    e.Row.Cells[i].CssClass = "hidden";
                }
                for (int i = 25; i < 29; i++) {
                    e.Row.Cells[i].CssClass = "hidden";
                }

                e.Row.Cells[6].CssClass = "hidden";

            }
            else {
                e.Row.Cells[11].CssClass = "hidden";
            }
            
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            msg.Visible = false;
            try {
                //int EmptyPrice = 0;
                //for (int i = 0; i < gvBidItems.Rows.Count; i++) {
                //    if ((gvBidItems.Rows[i].FindControl("txtCif") as TextBox).Text == "0") {
                //        EmptyPrice = 1;

                //    }

                //}

               // if (EmptyPrice == 0) {
                IList<HttpPostedFile> images = fileImages.PostedFiles;
                IList<HttpPostedFile> docs = fileDocs.PostedFiles;
                int BidId = int.Parse(ViewState["BidId"].ToString());
                if (btnSubmit.Text == "Submit Quotation") {
                    // Import Quotation Details
                    ImportQuotation importQuotation = new ImportQuotation();
                    importQuotation.ImportQuotationItemList = new List<ImportQuotationItem>();

                    if (int.Parse(ViewState["PurchaseType"].ToString()) == 2) {
                        importQuotation.CurrencyTypeId = ddlCurrencyDetail.SelectedValue == "0" ? 0 : int.Parse(ddlCurrencyDetail.SelectedValue);
                        //  importQuotation.TermId = ddlPriceTerms.SelectedValue == "0" ? 0 : int.Parse(ddlPriceTerms.SelectedValue);
                        importQuotation.PaymentModeId = ddlPaymentMode.SelectedValue == "0" ? 0 : int.Parse(ddlPaymentMode.SelectedValue);
                        importQuotation.AgentId = ddlClearingAgent.SelectedValue == "0" ? 0 : int.Parse(ddlClearingAgent.SelectedValue);
                        importQuotation.TransportModeId = ddlTransportMode.SelectedValue == "0" ? 0 : int.Parse(ddlTransportMode.SelectedValue);
                        importQuotation.ContainerSizeId = ddlContainerSize.SelectedValue == "0" ? 0 : int.Parse(ddlContainerSize.SelectedValue);
                        importQuotation.SupplierAgentId = ddlAgent.Visible != false ? int.Parse(ddlAgent.SelectedValue) : 0;
                        importQuotation.Country = ddlCountry.Visible != false ? int.Parse(ddlCountry.SelectedValue) : 0;
                        importQuotation.PaymentModeDays = txtNoOfDays.Text == "" ? 0:int.Parse(txtNoOfDays.Text);
                    }

                    SupplierQuotation newQuotation = new SupplierQuotation();
                    newQuotation.QuotationReferenceCode = txtSuppQuotRefCode.Text;
                    newQuotation.BidId = BidId;
                    newQuotation.SupplierId = int.Parse(ddlSuppliers.SelectedValue.ToString());
                    //newQuotation.QuotationReferenceCode = txtSuppQuotRefCode.ToString();
                    newQuotation.SubTotal = decimal.Parse(hdnSubTotal.Value);
                    newQuotation.NbtAmount = decimal.Parse(hdnNbtTotal.Value);
                    newQuotation.VatAmount = decimal.Parse(hdnVatTotal.Value);
                    newQuotation.NetTotal = decimal.Parse(hdnNetTotal.Value);
                    newQuotation.TermsAndCondition = txtTermsAndConditions.Text;


                    newQuotation.QuotationImages = new List<QuotationImage>();
                    for (int i = 0; i < images.Count; i++) {
                        if (images[i].ContentLength > 0) {
                            string filePath = "/SupplierQuotation/Images/" + BidId + "_" + newQuotation.SupplierId + "_" + i + "_" + LocalTime.Now.Ticks + "_" + images[i].FileName;
                            images[i].SaveAs(HttpContext.Current.Server.MapPath(filePath));
                            QuotationImage image = new QuotationImage() {
                                ImagePath = "~" + filePath
                            };
                            newQuotation.QuotationImages.Add(image);
                        }
                    }

                    newQuotation.UploadedFiles = new List<SupplierBiddingFileUpload>();
                    for (int i = 0; i < docs.Count; i++) {
                        if (docs[i].ContentLength > 0) {
                            string filePath = "/SupplierQuotation/Docs/" + BidId + "_" + newQuotation.SupplierId + "_" + i + "_" + LocalTime.Now.Ticks + "_" + docs[i].FileName;
                            docs[i].SaveAs(HttpContext.Current.Server.MapPath(filePath));
                            SupplierBiddingFileUpload doc = new SupplierBiddingFileUpload() {
                                FileName = docs[i].FileName,
                                FilePath = "~" + filePath

                            };
                            newQuotation.UploadedFiles.Add(doc);
                        }
                    }

                    newQuotation.QuotationItems = new List<SupplierQuotationItem>();
                    for (int i = 0; i < gvBidItems.Rows.Count; i++) {
                        SupplierQuotationItem quotationItem = new SupplierQuotationItem();
                        ImportQuotationItem newImportQuotationItem = new ImportQuotationItem();
                        string description = (gvBidItems.Rows[i].FindControl("txtDescription") as TextBox).Text;
                        decimal unitPrice = 0;
                        if ((gvBidItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text != "") {
                            unitPrice = decimal.Parse((gvBidItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text);
                        }
                        var ds = decimal.Parse((gvBidItems.Rows[i].FindControl("txtNetTotal") as TextBox).Text);

                        // if ((description != "" || hndImport.Value == "1") && unitPrice != 0)
                        if ((description != "" || int.Parse(ViewState["PurchaseType"].ToString()) == 2)) {

                            quotationItem.BiddingItemId = int.Parse(gvBidItems.Rows[i].Cells[0].Text);
                            //quotationItem.ItemId = int.Parse(gvBidItems.Rows[i].Cells[7].Text);
                            quotationItem.ItemId = int.Parse((gvBidItems.Rows[i].FindControl("lblItemId") as Label).Text);
                            quotationItem.SupplierMentionedItemName = (gvBidItems.Rows[i].FindControl("txtSupItemName") as TextBox).Text == "" ? "" : (gvBidItems.Rows[i].FindControl("txtSupItemName") as TextBox).Text;
                            quotationItem.Qty = decimal.Parse(gvBidItems.Rows[i].Cells[7].Text);
                            quotationItem.EstimatedPrice = decimal.Parse(gvBidItems.Rows[i].Cells[9].Text);
                            quotationItem.Description = (gvBidItems.Rows[i].FindControl("txtDescription") as TextBox).Text;
                            // quotationItem.CurrencyId = Convert.ToInt32((gvBidItems.Rows[i].FindControl("dllCurrency") as DropDownList).SelectedValue);
                            ///quotationItem.ExchangeRate = Convert.ToDecimal((gvBidItems.Rows[i].FindControl("txtExchangeRate") as TextBox).Text);
                            quotationItem.UnitPrice = (gvBidItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text);
                            quotationItem.SubTotal = decimal.Parse((gvBidItems.Rows[i].FindControl("txtSubTotal") as TextBox).Text);
                            quotationItem.HasVat = (gvBidItems.Rows[i].FindControl("chkVat") as CheckBox).Checked == true ? 1 : 0;
                            //quotationItem.VatAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtVat") as TextBox).Text);
                            quotationItem.VatAmount = (gvBidItems.Rows[i].FindControl("txtVATValue") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtVATValue") as TextBox).Text);

                            if (haveNBT()) {
                                quotationItem.HasNbt = (gvBidItems.Rows[i].FindControl("chkNbt") as CheckBox).Checked == true ? 1 : 0;
                                quotationItem.NbtCalculationType = (gvBidItems.Rows[i].FindControl("rdoNbt204") as RadioButton).Checked == true ? 1 : 2;
                                quotationItem.NbtAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtNbt") as TextBox).Text);
                            }
                            else {
                                // if no nbt
                                quotationItem.HasNbt = 0;
                                quotationItem.NbtCalculationType = 0;
                                quotationItem.NbtAmount = 0;
                            }
                            quotationItem.TotalAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtNetTotal") as TextBox).Text);
                            
                            //quotationItem.AgentId = ddlAgent.Visible != false ? Convert.ToInt32(ddlAgent.SelectedValue) : 0;
                            //quotationItem.Country = ddlCountry.Visible != false ? ddlCountry.SelectedItem.Text : "Local";
                            ///quotationItem.Clearingcost = decimal.Parse((gvBidItems.Rows[i].FindControl("txtClearing") as TextBox).Text.Replace(",", ""));
                            /// quotationItem.Dutypal = decimal.Parse((gvBidItems.Rows[i].FindControl("txtdutypal") as TextBox).Text.Replace(",", ""));
                            ///quotationItem.Brand = (gvBidItems.Rows[i].FindControl("txtBrand") as TextBox).Text;
                            ///quotationItem.CIF = decimal.Parse((gvBidItems.Rows[i].FindControl("txtCif") as TextBox).Text);
                            // quotationItem.Terms = (gvBidItems.Rows[i].FindControl("ddlTerms") as DropDownList).SelectedValue;
                            ///quotationItem.Validity = (gvBidItems.Rows[i].FindControl("txtvalidity") as TextBox).Text != "" ? DateTime.ParseExact((gvBidItems.Rows[i].FindControl("txtvalidity") as TextBox).Text, "yyyy-MM-dd", CultureInfo.InvariantCulture) : DateTime.Now;
                            ///quotationItem.Other = decimal.Parse((gvBidItems.Rows[i].FindControl("txtother") as TextBox).Text.Replace(",", ""));
                            ///quotationItem.History = (gvBidItems.Rows[i].FindControl("ddlHistory") as DropDownList).SelectedValue;
                            //  quotationItem.Refno=(gvBidItems.Rows[i].FindControl("txtRefno") as TextBox).Text;
                            ///quotationItem.Estdelivery = (gvBidItems.Rows[i].FindControl("txtEstdelivery") as TextBox).Text;
                            ///quotationItem.Remark = (gvBidItems.Rows[i].FindControl("txtRemark") as TextBox).Text;

                            quotationItem.SupplierBOMs = new List<SupplierBOM>();
                            GridView gvBom = gvBidItems.Rows[i].FindControl("gvSpecs") as GridView;
                            for (int j = 0; j < gvBom.Rows.Count; j++) {
                                SupplierBOM bom = new SupplierBOM() {
                                    Material = (gvBom.Rows[j].FindControl("chkSpec") as CheckBox).Text,
                                    Description = gvBom.Rows[j].Cells[1].Text,
                                    Comply = (gvBom.Rows[j].FindControl("chkSpec") as CheckBox).Checked == true ? 1 : 0
                                };
                                quotationItem.SupplierBOMs.Add(bom);
                            }
                            newQuotation.QuotationItems.Add(quotationItem);


                            // Import Quotation Item Details
                            newImportQuotationItem.Brand = (gvBidItems.Rows[i].FindControl("txtBrand") as TextBox).Text;
                            newImportQuotationItem.CIF = decimal.Parse((gvBidItems.Rows[i].FindControl("txtCif") as TextBox).Text);
                            newImportQuotationItem.ClearingCost = decimal.Parse((gvBidItems.Rows[i].FindControl("txtClearing") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.DutyPal = decimal.Parse((gvBidItems.Rows[i].FindControl("txtdutypal") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.History = int.Parse((gvBidItems.Rows[i].FindControl("ddlHistory") as DropDownList).SelectedValue);
                            newImportQuotationItem.Other = decimal.Parse((gvBidItems.Rows[i].FindControl("txtother") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.Remark = (gvBidItems.Rows[i].FindControl("txtRemark") as TextBox).Text;
                            newImportQuotationItem.Validity = (gvBidItems.Rows[i].FindControl("txtvalidity") as TextBox).Text != "" ? DateTime.ParseExact((gvBidItems.Rows[i].FindControl("txtvalidity") as TextBox).Text, "yyyy-MM-dd", CultureInfo.InvariantCulture) : DateTime.Now;
                            newImportQuotationItem.ExchangeRate = Convert.ToDecimal((gvBidItems.Rows[i].FindControl("txtExchangeRate") as TextBox).Text);
                            newImportQuotationItem.EstDelivery = (gvBidItems.Rows[i].FindControl("txtEstdelivery") as TextBox).Text;
                            newImportQuotationItem.Term = (gvBidItems.Rows[i].FindControl("ddlTerms") as DropDownList).SelectedValue;
                            newImportQuotationItem.HsId = (gvBidItems.Rows[i].FindControl("txtHSID") as TextBox).Text;
                            newImportQuotationItem.Mill = (gvBidItems.Rows[i].FindControl("txtMill") as TextBox).Text;
                            newImportQuotationItem.ExchangeRateValue = decimal.Parse((gvBidItems.Rows[i].FindControl("txtExchangeRate") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.XIDRate = (gvBidItems.Rows[i].FindControl("txtXIDRate") as TextBox).Text == "" ? 0 :decimal.Parse((gvBidItems.Rows[i].FindControl("txtXIDRate") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.CIDRate = (gvBidItems.Rows[i].FindControl("txtCIDRate") as TextBox).Text == "" ? 0 :decimal.Parse((gvBidItems.Rows[i].FindControl("txtCIDRate") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.PALRate = (gvBidItems.Rows[i].FindControl("txtPALRate") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtPALRate") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.EICRate = (gvBidItems.Rows[i].FindControl("txtEICRate") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtEICRate") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.AirFreight = (gvBidItems.Rows[i].FindControl("txtAirFreightRate") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtAirFreightRate") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.Insurance = (gvBidItems.Rows[i].FindControl("txtInsurance") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtInsurance") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.XIDValue = (gvBidItems.Rows[i].FindControl("txtXIDValue") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtXIDValue") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.CIDValue = (gvBidItems.Rows[i].FindControl("txtCIDValue") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtCIDValue") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.PALValue = (gvBidItems.Rows[i].FindControl("txtPALValue") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtPALValue") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.EICValue = (gvBidItems.Rows[i].FindControl("txtEICValue") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtEICValue") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.VATValue = (gvBidItems.Rows[i].FindControl("txtVATValue") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtVATValue") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.VATRate = (gvBidItems.Rows[i].FindControl("txtVATRate") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtVATRate") as TextBox).Text.Replace(",", ""));
                            newImportQuotationItem.ItemId = int.Parse((gvBidItems.Rows[i].FindControl("lblItemId") as Label).Text);

                            //decimal TotalCost = 0;
                            //TotalCost = (newImportQuotationItem.CIF + newImportQuotationItem.ClearingCost + newImportQuotationItem.DutyPal + newImportQuotationItem.Other);
                            newImportQuotationItem.Total = 0;

                            importQuotation.ImportQuotationItemList.Add(newImportQuotationItem);
                        }
                        //else {
                        //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please enter Quoted Price and Description'}); });   </script>", false);

                        //}
                    }

                    int result = quotationController.SaveSupplierQuotation(newQuotation, importQuotation, int.Parse(ViewState["PurchaseType"].ToString()));
                    Bid_Bond_Details bidBondDetail = new JavaScriptSerializer().Deserialize<Bid_Bond_Details>(ViewState["bidBondDetail"].ToString());
                    if (bidBondDetail.IsRequired == 1) {
                        saveSupplierBidBondDetails();
                    }


                    if (result > 0) {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                        ClearFields();
                    }
                    else {
                        if (result == -1) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Already quotation reference code exist !, please try another code'}); });   </script>", false);
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on submitting quotation'}); });   </script>", false);
                        }
                    }

                }
                else {       // Update Quotation
                    var quotation = new JavaScriptSerializer().Deserialize<SupplierQuotation>(ViewState["Quotation"].ToString());
                    quotation.SubTotal = decimal.Parse(hdnSubTotal.Value);
                    quotation.NbtAmount = decimal.Parse(hdnNbtTotal.Value);
                    quotation.VatAmount = decimal.Parse(hdnVatTotal.Value);
                    quotation.NetTotal = decimal.Parse(hdnNetTotal.Value);
                    quotation.TermsAndCondition = txtTermsAndConditions.Text;

                    quotation.PaymentModeId = int.Parse(ddlPaymentMode.SelectedValue);
                    quotation.AgentId = int.Parse(ddlClearingAgent.SelectedValue);
                    quotation.TransportModeId = int.Parse(ddlTransportMode.SelectedValue);
                    quotation.ContainersizeId = int.Parse(ddlContainerSize.SelectedValue);
                    quotation.PaymentModeDays = int.Parse(txtNoOfDays.Text);

                    //  quotation.QuotationReferenceCode = txtSuppQuotRefCode.ToString();

                    if (quotation.QuotationImages == null)
                        quotation.QuotationImages = new List<QuotationImage>();

                    for (int i = 0; i < images.Count; i++) {
                        if (images[i].ContentLength > 0) {
                            string filePath = "/SupplierQuotation/Images/" + BidId + "_" + quotation.SupplierId + "_" + i + "_" + LocalTime.Now.Ticks + "_" + images[i].FileName;

                            images[i].SaveAs(HttpContext.Current.Server.MapPath(filePath));

                            QuotationImage image = new QuotationImage() {
                                ImagePath = "~" + filePath,
                                RecordStatus = 1
                            };

                            quotation.QuotationImages.Add(image);
                        }
                    }
                    if (quotation.UploadedFiles == null)
                        quotation.UploadedFiles = new List<SupplierBiddingFileUpload>();

                    for (int i = 0; i < docs.Count; i++) {
                        if (docs[i].ContentLength > 0) {
                            string filePath = "/SupplierQuotation/Docs/" + BidId + "_" + quotation.SupplierId + "_" + i + "_" + LocalTime.Now.Ticks + "_" + docs[i].FileName;

                            docs[i].SaveAs(HttpContext.Current.Server.MapPath(filePath));

                            SupplierBiddingFileUpload doc = new SupplierBiddingFileUpload() {
                                FileName = docs[i].FileName,
                                FilePath = "~" + filePath,
                                RecordStatus = 1

                            };

                            quotation.UploadedFiles.Add(doc);
                        }
                    }

                    int Error = 0;

                    foreach (var item in quotation.QuotationItems) {
                        for (int i = 0; i < gvBidItems.Rows.Count; i++) {
                            if (Convert.ToInt32(gvBidItems.Rows[i].Cells[0].Text) == item.BiddingItemId) {
                                //   quotationItem.ItemReferenceCode = (gvBidItems.Rows[i].FindControl("txtItemRefCode") as TextBox).Text;
                                item.Description = (gvBidItems.Rows[i].FindControl("txtDescription") as TextBox).Text;
                                item.CurrencyId = Convert.ToInt32((gvBidItems.Rows[i].FindControl("dllCurrency") as DropDownList).SelectedValue);
                                item.ExchangeRate = Convert.ToDecimal((gvBidItems.Rows[i].FindControl("txtExchangeRate") as TextBox).Text);
                                item.UnitPrice = (gvBidItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text == "" ? 0 : decimal.Parse((gvBidItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text);
                                item.SubTotal = decimal.Parse((gvBidItems.Rows[i].FindControl("txtSubTotal") as TextBox).Text);
                                item.HasVat = (gvBidItems.Rows[i].FindControl("chkVat") as CheckBox).Checked == true ? 1 : 0;
                                item.VatAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtVATValue") as TextBox).Text);
                                item.HasNbt = (gvBidItems.Rows[i].FindControl("chkNbt") as CheckBox).Checked == true ? 1 : 0;
                                item.NbtCalculationType = (gvBidItems.Rows[i].FindControl("rdoNbt204") as RadioButton).Checked == true ? 1 : 2;
                                item.NbtAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtNbt") as TextBox).Text);
                                item.TotalAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtNetTotal") as TextBox).Text);
                                item.Clearingcost = decimal.Parse((gvBidItems.Rows[i].FindControl("txtClearing") as TextBox).Text.Replace(",", ""));
                                item.Dutypal = decimal.Parse((gvBidItems.Rows[i].FindControl("txtdutypal") as TextBox).Text.Replace(",", ""));
                                item.Brand = (gvBidItems.Rows[i].FindControl("txtBrand") as TextBox).Text;
                                item.Term = (gvBidItems.Rows[i].FindControl("ddlTerms") as DropDownList).SelectedValue;
                                item.CIF = decimal.Parse((gvBidItems.Rows[i].FindControl("txtCif") as TextBox).Text.Replace(",", ""));
                                item.Validity = DateTime.ParseExact((gvBidItems.Rows[i].FindControl("txtvalidity") as TextBox).Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                                //if (ViewState["PurchaseType"].ToString() == "1") {
                                //    item.Validity = DateTime.ParseExact((gvBidItems.Rows[i].FindControl("txtvalidity") as TextBox).Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                                //}
                                //else {
                                //    item.Validity = LocalTime.Now;
                                //}

                                item.Other = decimal.Parse((gvBidItems.Rows[i].FindControl("txtother") as TextBox).Text.Replace(",", ""));
                                item.History = (gvBidItems.Rows[i].FindControl("ddlHistory") as DropDownList).SelectedValue;
                                //  item.Refno = (gvBidItems.Rows[i].FindControl("txtRefno") as TextBox).Text;
                                item.Estdelivery = (gvBidItems.Rows[i].FindControl("txtEstdelivery") as TextBox).Text;
                                item.Remark = (gvBidItems.Rows[i].FindControl("txtRemark") as TextBox).Text;

                                item.SupplierMentionedItemName = (gvBidItems.Rows[i].FindControl("txtSupItemName") as TextBox).Text;
                                item.HsId = (gvBidItems.Rows[i].FindControl("txtHSIDNew") as TextBox).Text;
                                item.Mill = (gvBidItems.Rows[i].FindControl("txtMill") as TextBox).Text;

                                item.xid = decimal.Parse((gvBidItems.Rows[i].FindControl("txtXIDRate") as TextBox).Text.Replace(",", ""));
                                item.cid = decimal.Parse((gvBidItems.Rows[i].FindControl("txtCIDRate") as TextBox).Text.Replace(",", ""));
                                item.eic = decimal.Parse((gvBidItems.Rows[i].FindControl("txtEICRate") as TextBox).Text.Replace(",", ""));
                                item.pal = decimal.Parse((gvBidItems.Rows[i].FindControl("txtPALRate") as TextBox).Text.Replace(",", ""));
                                item.XIDValue = decimal.Parse((gvBidItems.Rows[i].FindControl("txtXIDValue") as TextBox).Text.Replace(",", ""));
                                item.EICValue = decimal.Parse((gvBidItems.Rows[i].FindControl("txtEICValue") as TextBox).Text.Replace(",", ""));
                                item.PALValue = decimal.Parse((gvBidItems.Rows[i].FindControl("txtPALValue") as TextBox).Text.Replace(",", ""));
                                item.CIDValue = decimal.Parse((gvBidItems.Rows[i].FindControl("txtCIDValue") as TextBox).Text.Replace(",", ""));
                                item.VatRate = decimal.Parse((gvBidItems.Rows[i].FindControl("txtVATRate") as TextBox).Text.Replace(",", ""));
                                item.AirFreight = decimal.Parse((gvBidItems.Rows[i].FindControl("txtAirFreightRate") as TextBox).Text.Replace(",", ""));
                                item.Insurance = decimal.Parse((gvBidItems.Rows[i].FindControl("txtInsurance") as TextBox).Text.Replace(",", ""));


                                item.SupplierBOMs = new List<SupplierBOM>();

                                GridView gvBom = gvBidItems.Rows[i].FindControl("gvSpecs") as GridView;

                                for (int j = 0; j < gvBom.Rows.Count; j++) {
                                    SupplierBOM bom = new SupplierBOM() {
                                        Material = (gvBom.Rows[j].FindControl("chkSpec") as CheckBox).Text,
                                        Description = gvBom.Rows[j].Cells[1].Text,
                                        Comply = (gvBom.Rows[j].FindControl("chkSpec") as CheckBox).Checked == true ? 1 : 0
                                    };
                                    item.SupplierBOMs.Add(bom);
                                }

                                gvImages.DataSource = quotation.QuotationImages;
                                gvImages.DataBind();
                                break;
                            }
                        }
                    }

                    if (Error != 1) {
                        //int result = quotationController.UpdateSupplierQuotation(quotation);
                        int result = quotationController.UpdateSupplierQuotationImports(quotation);
                        Bid_Bond_Details bidBondDetail = new JavaScriptSerializer().Deserialize<Bid_Bond_Details>(ViewState["bidBondDetail"].ToString());
                        if (bidBondDetail.IsRequired == 1) {
                            saveSupplierBidBondDetails();
                        }

                        if (result > 0) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                            hdnEdit.Value = "0";
                            ClearFields();
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on submitting quotation'}); });   </script>", false);
                        }
                    }
                }
                //}
                //else {
                //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Add Remark for every Quotatios.'}); });   </script>", false);
                //}
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        private void saveSupplierBidBondDetails() {
            if (ddlSuppliers.SelectedValue != "") {
                SupplierBidBondDetails model = new SupplierBidBondDetails {
                    Bid_Id = int.Parse(ViewState["BidId"].ToString()),
                    Supplier_Id = Convert.ToInt32(ddlSuppliers.SelectedValue),
                    Bond_No = txtBondNo.Text,
                    Bank = txtBank.Text,
                    Bond_Amount = Convert.ToDecimal(txtBondAmount.Text),
                    Expire_Date_Of_Bond = DateTime.ParseExact(txtExpireDOB.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    Receipt_No = txtReceiptNo.Text
                };
                supplierBidBondDetailController.saveSupplierBidBondDetails(model);
            }
        }

        private void ClearFields() {
            ViewState["ChooseSpecsFrom"] = "0";
            ViewState["Bid"] = new JavaScriptSerializer().Serialize(biddingController.GetBidDetailsForQuotationSubmission(int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["CompanyId"].ToString())));
            txtTermsAndConditions.Text = "";
            LoadAllSuppliers();
            LoadGV();
            if (ddlCountry.Items.Count != 0) {
                // ddlCountry.SelectedIndex = 1;
            }
            PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["prMaster"].ToString());
            if (prMaster.PurchaseType == 2) {
                ddlAgent.Items.Clear();

                LoadCurrencyDetail();
                LoadPaymentMode();
                LoadPriceTerms();
                LoadTransportMode();
                LoadContainerSize();
                LoadClearingAgent();

                ddlTransportMode.SelectedIndex = 0;
                ddlContainerSize.SelectedIndex = 0;
                ddlClearingAgent.SelectedIndex = 0;
                ddlPaymentMode.SelectedIndex = 0;
                ddlCountry.SelectedIndex = 0;
                ddlCurrencyDetail.SelectedIndex = 0;

            }
            gvPreviousQuotations.DataSource = null;
            gvPreviousQuotations.DataBind();
            pnlQuotations.Visible = false;
            txtSuppQuotRefCode.Text = "";
            txtBondNo.Text = "";
            txtBank.Text = "";
            txtBondAmount.Text = "";
            txtExpireDOB.Text = "";
            txtReceiptNo.Text = "";
            pnlNoOfDays.Visible = false;
            txtNoOfDays.Text = "0";
            ddlAgent.Enabled = true;
            ddlCountry.Enabled = true;
            ddlCurrencyDetail.Enabled = true;

        }

        protected void btnMoreBidItemDetails_Click(object sender, EventArgs e) {
            try {
                var bidItemId = int.Parse(((GridViewRow)((Button)sender).NamingContainer).Cells[0].Text);
                ViewState["BidItemId"] = bidItemId;
                List<PrDetailsV2> PrDetails = new List<PrDetailsV2>();
                PrDetails.Add(new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).BiddingItems.Find(b => b.BiddingItemId == bidItemId).PrDetail);
                gvBidMoreDetails.DataSource = PrDetails;
                gvBidMoreDetails.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                throw;
            }
        }

        protected void btnViewzReplacementPhotosOfBidItem_Click(object sender, EventArgs e) {
            try {
                gvViewReplacementImages.DataSource = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).BiddingItems.Find(b => b.BiddingItemId == int.Parse(ViewState["BidItemId"].ToString())).PrDetail.PrReplacementFileUploads;
                gvViewReplacementImages.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlReplacementImages').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
                throw;
            }

        }

        protected void btnViewUploadPhotosOfBidItem_Click(object sender, EventArgs e) {
            try {
                gvUploadedPhotos.DataSource = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).BiddingItems.Find(b => b.BiddingItemId == int.Parse(ViewState["BidItemId"].ToString())).PrDetail.PrFileUploads;
                gvUploadedPhotos.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlStandardImages').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
                throw;
            }
        }

        protected void lblViewBomOfBidItem_Click(object sender, EventArgs e) {
            try {
                gvBOMDate.DataSource = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).BiddingItems.Find(b => b.BiddingItemId == int.Parse(ViewState["BidItemId"].ToString())).PrDetail.PrBoms;
                gvBOMDate.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItemSpecs').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
                throw;
            }
        }

        protected void btnViewSupportiveDocumentsOfBidItem_Click(object sender, EventArgs e) {
            try {
                gvSupportiveDocuments.DataSource = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).BiddingItems.Find(b => b.BiddingItemId == int.Parse(ViewState["BidItemId"].ToString())).PrDetail.PrSupportiveDocuments;
                gvSupportiveDocuments.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlSupportiveDocs').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
                throw;
            }
        }

        protected void btnRemoveFile_Click(object sender, EventArgs e) {
            int fileId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);
            var quotation = new JavaScriptSerializer().Deserialize<SupplierQuotation>(ViewState["Quotation"].ToString());
            quotation.UploadedFiles.Find(f => f.QuotationFileId == fileId).RecordStatus = 2;
            gvDocs.DataSource = quotation.UploadedFiles.Where(f => f.RecordStatus != 2);
            gvDocs.DataBind();
        }

        protected void btnRemoveImage_Click(object sender, EventArgs e) {
            int imgeId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[0].Text);
            var quotation = new JavaScriptSerializer().Deserialize<SupplierQuotation>(ViewState["Quotation"].ToString());
            quotation.QuotationImages.Find(img => img.QuotationImageId == imgeId).RecordStatus = 2;
            gvImages.DataSource = quotation.QuotationImages.Where(img => img.RecordStatus != 2);
            gvImages.DataBind();
        }

        private void DisplayMessage(string message, bool isError) {
            msg.Visible = true;
            if (isError) {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }

        protected void ddlTerms_SelectedIndexChanged(object sender, EventArgs e) {
            GridViewRow row = ((sender as DropDownList).NamingContainer as GridViewRow);
            ViewState["IsHSCodeName"] = 0;
            int RowId = row.RowIndex + 1;
            hdnRowIndex.Value = RowId.ToString();

            //(row.FindControl("txtUnitPriceLkrView") as TextBox).Enabled = false;
            //(row.FindControl("txtCIFInLkrView") as TextBox).Enabled = false;
            //(row.FindControl("txtDutypalCalView") as TextBox).Enabled = false;
            //(row.FindControl("txtUnitPriceView") as TextBox).Enabled = false;
            //(row.FindControl("txtSubTotalView") as TextBox).Enabled = false;
            //(row.FindControl("txtNetTotalView") as TextBox).Enabled = false;

            if ((row.FindControl("txtHSID") as TextBox).Text == "0" && (row.FindControl("ddlTerms") as DropDownList).SelectedValue != "11") {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Enter valid HS Code'}); });   </script>", false);
                DropDownList List = (row.FindControl("ddlTerms") as DropDownList);
                List.SelectedValue = "0";
            }
            else {
                
                //var selectedValues = (row.FindControl("ddlTerms") as DropDownList).SelectedValue;
                var selectedValues = (row.FindControl("ddlTerms") as DropDownList).SelectedValue;
                string HsCode = "";
                (row.FindControl("txtUnitPriceLkr") as TextBox).Enabled = true;
                (row.FindControl("txtUnitPriceLkrView") as TextBox).Enabled = true;

                (row.FindControl("txtCIFInLkr") as TextBox).Enabled = true;
                (row.FindControl("txtCIFInLkrView") as TextBox).Enabled = true;
                (row.FindControl("chkVat") as CheckBox).Enabled = true;
                (row.FindControl("chkVat") as CheckBox).Checked = true;

                (row.FindControl("txtXIDRate") as TextBox).Text = "0.00";
                (row.FindControl("txtCIDRate") as TextBox).Text = "0.00";
                (row.FindControl("txtPALRate") as TextBox).Text = "0.00";
                (row.FindControl("txtEICRate") as TextBox).Text = "0.00";
                (row.FindControl("txtAirFreightRate") as TextBox).Text = "0.00";
                (row.FindControl("txtInsurance") as TextBox).Text = "0.00";
                (row.FindControl("txtXIDValue") as TextBox).Text = "0.00";
                (row.FindControl("txtCIDValue") as TextBox).Text = "0.00";
                (row.FindControl("txtPALValue") as TextBox).Text = "0.00";
                (row.FindControl("txtEICValue") as TextBox).Text = "0.00";
                (row.FindControl("txtVATValue") as TextBox).Text = "0.00";
                (row.FindControl("txtVATRate") as TextBox).Text = "0.00";
                (row.FindControl("txtChangedVAT") as TextBox).Text = "0.00";

                //if (selectedValues != "0" && selectedValues != "11")
                if (selectedValues == "0" || selectedValues == "11") {
                    //(row.FindControl("txtdutypal") as TextBox).Enabled = false;
                    (row.FindControl("txtClearing") as TextBox).Enabled = true;
                    (row.FindControl("txtCIF") as TextBox).Enabled = true;
                    //(row.FindControl("txtUnitPrice") as TextBox).Attributes.Add("readonly", "readonly");
                    (row.FindControl("txtSubTotal") as TextBox).Text = "0.00";
                    (row.FindControl("txtNetTotal") as TextBox).Text = "0.00";
                    // (row.FindControl("txtUnitPrice") as TextBox).Style.Add("readonly", "readonly!important");

                    (row.FindControl("txtSubTotalView") as TextBox).Text = "0.00";
                    (row.FindControl("txtNetTotalView") as TextBox).Text = "0.00";
                    

                    (row.FindControl("txtother") as TextBox).Enabled = true;
                    if (hdnEdit.Value == "1") {
                        HsCode = (row.FindControl("txtHSIDNew") as TextBox).Text;
                    }
                    else {
                        HsCode = (row.FindControl("txtHSID") as TextBox).Text;
                    }
                    (row.FindControl("txtHSID") as TextBox).Enabled = false;
                    (row.FindControl("txtCIFInLkr") as TextBox).Enabled = false;
                    (row.FindControl("txtCIFInLkrView") as TextBox).Enabled = false;
                    (row.FindControl("txtdutypal") as TextBox).Enabled = false;
                    (row.FindControl("txtUnitPrice") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceView") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPrice") as TextBox).Enabled = true;
                    (row.FindControl("txtdutypal") as TextBox).Text = "0.00";
                    (row.FindControl("txtDutypalCalView") as TextBox).Text = "0.00";
                    (row.FindControl("txtClearing") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIF") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIFInLkr") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceLkr") as TextBox).Text = "0.00";
                    (row.FindControl("txtother") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIFInLkrView") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceLkrView") as TextBox).Text = "0.00";

                }
                else if (selectedValues == "3" || selectedValues == "4" || selectedValues == "16" || selectedValues == "17") {
                    (row.FindControl("txtdutypal") as TextBox).Text = "0.00";
                    (row.FindControl("txtDutypalCalView") as TextBox).Text = "0.00";
                    (row.FindControl("txtClearing") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIF") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIFInLkr") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceLkr") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPrice") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceView") as TextBox).Text = "0.00";
                    (row.FindControl("txtSubTotal") as TextBox).Text = "0.00";
                    (row.FindControl("txtNetTotal") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceLkr") as TextBox).Enabled = false;
                    (row.FindControl("txtUnitPriceLkrView") as TextBox).Enabled = false;
                    (row.FindControl("txtCIFInLkr") as TextBox).Enabled = true;
                    (row.FindControl("txtCIFInLkrView") as TextBox).Enabled = true;
                    (row.FindControl("txtother") as TextBox).Enabled = true;
                    (row.FindControl("txtother") as TextBox).Text = "0.00";
                    (row.FindControl("txtSubTotalView") as TextBox).Text = "0.00";
                    (row.FindControl("txtNetTotalView") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIFInLkrView") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceLkrView") as TextBox).Text = "0.00";


                    if (hdnEdit.Value == "1") {
                        HsCode = (row.FindControl("txtHSIDNew") as TextBox).Text;
                    }
                    else {
                        HsCode = (row.FindControl("txtHSID") as TextBox).Text;
                    }
                    (row.FindControl("txtHSID") as TextBox).Enabled = false;
                    pnlInsurance.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  $('#mdlInsuaranceAF').modal('show'); });   </script>", false);

                }
                else {
                    //(row.FindControl("txtdutypal") as TextBox).Enabled = false;
                    (row.FindControl("txtClearing") as TextBox).Enabled = true;
                    (row.FindControl("txtCIF") as TextBox).Enabled = true;
                    (row.FindControl("txtdutypal") as TextBox).Text = "0.00";
                    (row.FindControl("txtDutypalCalView") as TextBox).Text = "0.00";
                    (row.FindControl("txtClearing") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIF") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIFInLkr") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceLkr") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPrice") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceView") as TextBox).Text = "0.00";
                    (row.FindControl("txtSubTotal") as TextBox).Text = "0.00";
                    (row.FindControl("txtNetTotal") as TextBox).Text = "0.00";
                    //(row.FindControl("txtUnitPrice") as TextBox).Attributes.Remove("readonly");
                    (row.FindControl("txtother") as TextBox).Enabled = true;
                    (row.FindControl("txtother") as TextBox).Text = "0.00";
                    (row.FindControl("txtSubTotalView") as TextBox).Text = "0.00";
                    (row.FindControl("txtNetTotalView") as TextBox).Text = "0.00";
                    (row.FindControl("txtCIFInLkrView") as TextBox).Text = "0.00";
                    (row.FindControl("txtUnitPriceLkrView") as TextBox).Text = "0.00";


                    if (hdnEdit.Value == "1") {
                        HsCode = (row.FindControl("txtHSIDNew") as TextBox).Text;
                    }
                    else {
                        HsCode = (row.FindControl("txtHSID") as TextBox).Text;
                    }
                    (row.FindControl("txtHSID") as TextBox).Enabled = false;
                    (row.FindControl("txtUnitPriceLkr") as TextBox).Enabled = false;
                    (row.FindControl("txtUnitPriceLkrView") as TextBox).Enabled = false;
                    pnlInsurance.Visible = false;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  $('#mdlInsuaranceAF').modal('show'); });   </script>", false);

                }

                for (int i = 0; i < gvBidItems.Rows.Count; i++) {
                    if (selectedValues == "8" || selectedValues == "9") {
                        //gvBidItems.Columns[18].HeaderStyle.CssClass = "hidden" ;
                        //gvBidItems.Columns[18].ItemStyle.CssClass = "hidden";

                        //gvBidItems.Columns[19].HeaderStyle.CssClass = "show";
                        //gvBidItems.Columns[19].ItemStyle.CssClass = "show";
                        (row.FindControl("txtUnitPriceLkr") as TextBox).Enabled = false;
                        (row.FindControl("txtUnitPriceLkrView") as TextBox).Enabled = false;
                        (row.FindControl("txtCIFInLkr") as TextBox).Enabled = true;
                        (row.FindControl("txtCIFInLkrView") as TextBox).Enabled = true;

                    }
                    if (selectedValues == "1" || selectedValues == "7" || selectedValues == "12") {
                        //gvBidItems.Columns[19].HeaderStyle.CssClass = "hidden";
                        //gvBidItems.Columns[19].ItemStyle.CssClass = "hidden";

                        //gvBidItems.Columns[18].HeaderStyle.CssClass = "show";
                        //gvBidItems.Columns[18].ItemStyle.CssClass = "show";
                        (row.FindControl("txtCIFInLkr") as TextBox).Enabled = false;
                        (row.FindControl("txtCIFInLkrView") as TextBox).Enabled = false;
                        (row.FindControl("txtUnitPriceLkr") as TextBox).Enabled = true;
                        (row.FindControl("txtUnitPriceLkrView") as TextBox).Enabled = true;
                    }
                }
                hdnEdit.Value = "0";
                plHsCode.Visible = false;
                ViewState["HSCode"] = HsCode;
                DutyRates Rates = ControllerFactory.CreateDutyRatesController().GetRatesByHSCode(HsCode);
                txtInsurance.Text = "0.00";
                txtAirFreight.Text = "0.00";
                txtCID.Text = Rates.CID == 0 ? "0.00" : Rates.CID.ToString();
                txtEIC.Text = Rates.EIC == 0 ? "0.00" : Rates.EIC.ToString();
                txtPAL.Text = Rates.PAL == 0 ? "0.00" : Rates.PAL.ToString();
                txtXID.Text = Rates.XID == 0 ? "0.00" : Rates.XID.ToString();
                txtHSName.Text = "";
                if (Rates.CID == 0 && Rates.EIC == 0 && Rates.PAL == 0 && Rates.XID == 0 ) {
                    plHsCode.Visible = true;
                    ViewState["IsHSCodeName"] = 1;
                }

            }
            
        }
        protected void hdnBtnHSCde_Click(object sender, EventArgs e) {
        //    string HSID = hdnHSID.Value;
        //    txtCID.Text ="5";
        //    txtEIC.Text = "5";
        //    txtPAL.Text = "5";
        //    txtXID.Text = "5";
        //    ItemValue.Text = hdnItemValue.Value;
        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlcaluculation').modal('show'); });   </script>", false);

        }

        protected void btnQuoItemDelete_Click(object sender, EventArgs e) {
            msg.Visible = false;
            try {
                //GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                //int quotationId = int.Parse(row.Cells[1].Text);
                //int quotationItemId = int.Parse(row.Cells[0].Text);
                //int itemId = int.Parse(row.Cells[2].Text);

                int quotationId = int.Parse(hdnQuotationId.Value);
                int quotationItemId = int.Parse(hdnQuotationItemId.Value);
                int itemId = int.Parse(hdnItemId.Value);
                decimal subTotal = decimal.Parse(hdnSubTot.Value);
                decimal nbt = decimal.Parse(hdnNbt.Value);
                decimal vat = decimal.Parse(hdnVat.Value);
                decimal netTot = decimal.Parse(hdnNetTot.Value);

                ViewState["DeletedQuotationItemId"] = quotationItemId;

                int delete = supplierQuotation.DeleteSubmittedSupplierQuotation(quotationId, itemId, quotationItemId, subTotal, vat, nbt, netTot);


                if (delete > 0) {
                    LoadGV();
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Successfully deleted the record', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    var bid = biddingController.GetBidDetailsForQuotationSubmission(int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                    if (ddlSuppliers.SelectedIndex != 0) {
                        List<SupplierQuotation> supplierQuotation = bid.SupplierQuotations.Where(sq => sq.SupplierId == int.Parse(ddlSuppliers.SelectedValue.ToString())).ToList();
                        if (supplierQuotation.Count >= 0) {
                            pnlQuotations.Visible = true;
                            gvPreviousQuotations.DataSource = supplierQuotation;
                            gvPreviousQuotations.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void PreQuotationItemsView_Click(object sender, EventArgs e) {
            try {
                var bid = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString());
                SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text.ToString()));
                List<SupplierQuotation> listquotation = new List<SupplierQuotation>();
                listquotation.Add(quotation);
                ViewState["QuotationId"] = quotation.QuotationId;
                gvQuotationMoreDetails.DataSource = listquotation;
                gvQuotationMoreDetails.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotationMoreDetails').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
                throw;
            }
        }

        protected void btnViewQuotationImages_Click(object sender, EventArgs e) {
            try {
                gvQuotationImages.DataSource = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).SupplierQuotations.Find(b => b.QuotationId == int.Parse(ViewState["QuotationId"].ToString())).QuotationImages;
                gvQuotationImages.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotationImages').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }

        protected void btnViewQuotationSupportiveDocuments_Click(object sender, EventArgs e) {
            try {

                List<SupplierBiddingFileUpload> Files = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString()).SupplierQuotations.Find(b => b.QuotationId == int.Parse(ViewState["QuotationId"].ToString())).UploadedFiles;
                for (int i = 0; i < Files.Count; i++) {
                    Files[i].FilePath = Files[i].FilePath.ToString().Replace("~/", "");
                }
                gvQuotationDocs.DataSource = Files;
                gvQuotationDocs.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotationDocs').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }


        protected void btnShowRejectedQuotation_Click(object sender, EventArgs e) {

            if (hndHideShow.Value == "0") {
                btnShowRejectedQuotation.Text = "Hide Rejected Quotations";
                gvRejectedQuotationAndItems.Visible = true;
                hndHideShow.Value = "1";
                btnShowRejectedQuotation.CssClass = "btn btn-warning btn-sm";
            }
            else {
                btnShowRejectedQuotation.Text = "Show Rejected Quotations";
                gvRejectedQuotationAndItems.Visible = false;
                hndHideShow.Value = "0";
                btnShowRejectedQuotation.CssClass = "btn btn-danger btn-sm";
            }

        }

        protected void gvRejectedQuotationAndItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvQuotationItem = e.Row.FindControl("gvQuotationItems") as GridView;
                GridView gvSupplierQuotation = (gvQuotationItem.NamingContainer as GridViewRow).NamingContainer as GridView;
                int QuotationId = int.Parse(gvSupplierQuotation.DataKeys[e.Row.RowIndex].Value.ToString());
                Bidding bid = new JavaScriptSerializer().Deserialize<Bidding>(ViewState["Bid"].ToString());
                int bidId = Convert.ToInt32(ViewState["BidId"].ToString());
                List<SupplierQuotationItem> quotationItems = bid.SupplierQuotations.Find(t => t.QuotationId == QuotationId).QuotationItems;
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;
                gvQuotationItems.DataSource = quotationItems;
                gvQuotationItems.DataBind();
            }
        }

        protected void ddlCurrencyDetail_SelectedIndexChanged(object sender, EventArgs e) {
            LoadGV();
            int currencyTypeId = int.Parse(ddlCurrencyDetail.SelectedValue);
            
            CurrencyRate Rate = currencyRateController.FetchCurrencyRatesByMaxDate(currencyTypeId);

            for (int i = 0; i < gvBidItems.Rows.Count; i++) {
                TextBox TxtRate = (gvBidItems.Rows[i].FindControl("txtExchangeRate")) as TextBox;
                TxtRate.Text = Rate.SellingRate.ToString();
            }
        }

        protected void btnDone_Click(object sender, EventArgs e) {

            VAT_NBT VatNbt = ViewState["VAT_NBT"] as VAT_NBT;

            decimal Insurance = txtInsurance.Text == "" ? 0: decimal.Parse(txtInsurance.Text);
            decimal AirFreight = txtAirFreight.Text == "" ? 0: decimal.Parse(txtAirFreight.Text);
            hdnInsurance.Value = txtInsurance.Text == "" ? "0.00" : txtInsurance.Text;
            hdnAirFreightRate.Value = txtAirFreight.Text == "" ? "0.00" : txtAirFreight.Text;
            string hsCodeNname = txtHSName.Text == "" ? "" : txtHSName.Text;

            decimal Total = Insurance + AirFreight;
            hdnInsuranceAF.Value = Total.ToString();

            hdnXID.Value = txtXID.Text == "" ? "0.00" : txtXID.Text;
            hdnCID.Value = txtCID.Text == "" ? "0.00" : txtCID.Text;
            hdnPAL.Value = txtPAL.Text == "" ? "0.00" : txtPAL.Text;
            hdnEIC.Value = txtEIC.Text == "" ? "0.00" : txtEIC.Text;
            hdnVATRate.Value = VatNbt.VatRate.ToString("N2");
            

            if (int.Parse(ViewState["IsHSCodeName"].ToString()) == 0) {
                dutyRatesController.SaveCurrencyRates(ViewState["HSCode"].ToString(), decimal.Parse(txtXID.Text), decimal.Parse(txtCID.Text), decimal.Parse(txtPAL.Text), decimal.Parse(txtEIC.Text), hsCodeNname);

            }
            else {
                if (txtHSName.Text == "") {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'HS Code Name Cannot be empty'}); });   </script>", false);

                }
                else {
                    dutyRatesController.SaveCurrencyRates(ViewState["HSCode"].ToString(), decimal.Parse(txtXID.Text), decimal.Parse(txtCID.Text), decimal.Parse(txtPAL.Text), decimal.Parse(txtEIC.Text), hsCodeNname);

                }
            }

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove(); $('#mdlInsuaranceAF').modal('hide'); CalculateSummaryInTermChange(); });   </script>", false);
            
        }

        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e) {
            int PaymentMode = int.Parse(ddlPaymentMode.SelectedValue);

            if (PaymentMode == 4 || PaymentMode == 5) {
                pnlNoOfDays.Visible = true;
            }
            else {
                pnlNoOfDays.Visible = false;
            }
        }
    }
}