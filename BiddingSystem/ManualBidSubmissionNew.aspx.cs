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
    public partial class ManualBidSubmissionNew : System.Web.UI.Page {
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
                        ViewState["VAT_NBT"] = generalSettingsController.getLatestVatNbt();
                        ViewState["PurchaseType"] = prMaster.PurchaseType;
                        ViewState["DeletedQuotationItemId"] = 0;
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
                    newRow["HasVat"] = "0";
                    newRow["NbtCalculationType"] = "1";
                    newRow["NbtAmount"] = "0.00";
                    newRow["VatAmount"] = "0.00";
                    newRow["NetTotal"] = "0.00";
                    newRow["CIF"] = "0.00";
                    newRow["Duty&pal"] = "0.00";
                    newRow["Clearing"] = "0.00";
                    newRow["Other"] = "0.00";
                    newRow["Other"] = "0.00";
                    newRow["VatRate"] = ListVATNBTValues != null ? ListVATNBTValues.VatRate.ToString("N4") : "0";
                    newRow["NBTRate1"] = ListVATNBTValues != null ? (ListVATNBTValues.NBTRate1 * 100).ToString("N2") + "%" : "0";
                    newRow["NBTRate2"] = ListVATNBTValues != null ? (ListVATNBTValues.NBTRate2 * 100).ToString("N2") + "%" : "0";
                    newRow["HasNBTRate"] = hasNBTRate.ToString();
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
            dt.Columns.Add("ExchangeRate");
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
            dt.Columns.Add("Terms");
            dt.Columns.Add("CIF");
            dt.Columns.Add("Duty&pal");
            dt.Columns.Add("Clearing");
            dt.Columns.Add("History");
            dt.Columns.Add("Validity");
            dt.Columns.Add("Other");
            //dt.Columns.Add("Refno");
            dt.Columns.Add("Estdelivery");
            dt.Columns.Add("Remark");

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

        protected void btnEdit_Click(object sender, EventArgs e) {
            msg.Visible = false;

            var bid = biddingController.GetBidDetailsForQuotationSubmission(int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["CompanyId"].ToString()));

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
                newRow["HsId"] = bid.BiddingItems[i].HsId;
                newRow["Qty"] = quotation.QuotationItems[i].Qty.ToString();
                newRow["UnitShortName"] = bid.BiddingItems[i].UnitShortName == null ? "Not Found" : bid.BiddingItems[i].UnitShortName.ToString();
                newRow["CurrencyId"] = quotation.QuotationItems[i].CurrencyId.ToString();
                newRow["ExchangeRate"] = quotation.QuotationItems[i].ExchangeRate.ToString();
                newRow["Description"] = quotation.QuotationItems[i].Description.ToString();
                newRow["AgentId"] = quotation.QuotationItems[i].AgentId.ToString();// Later chnage
                newRow["Country"] = quotation.QuotationItems[i].Country == null ? "0" : quotation.QuotationItems[i].Country.ToString();// Later chnage
                if (!string.IsNullOrEmpty(quotation.QuotationItems[i].Brand)) {
                    newRow["Brand"] = quotation.QuotationItems[i].Brand.ToString();
                }
                else {
                    newRow["Brand"] = "";
                }

                if (!string.IsNullOrEmpty(quotation.QuotationItems[i].Terms)) {
                    newRow["Terms"] = quotation.QuotationItems[i].Terms.ToString();
                }
                else {
                    newRow["Terms"] = "";
                }

                newRow["EstimatedPrice"] = quotation.QuotationItems[i].EstimatedPrice;
                newRow["UnitPrice"] = quotation.QuotationItems[i].UnitPrice.ToString("N2");
                newRow["SubTotal"] = quotation.QuotationItems[i].SubTotal.ToString();
                newRow["HasNbt"] = quotation.QuotationItems[i].HasNbt.ToString();
                newRow["HasVat"] = quotation.QuotationItems[i].HasVat.ToString();
                newRow["NbtCalculationType"] = quotation.QuotationItems[i].NbtCalculationType.ToString();
                newRow["NbtAmount"] = quotation.QuotationItems[i].NbtAmount.ToString("N2");
                newRow["VatAmount"] = quotation.QuotationItems[i].VatAmount.ToString("N2");
                newRow["NetTotal"] = quotation.QuotationItems[i].TotalAmount.ToString("N2");
                newRow["CIF"] = quotation.QuotationItems[i].CIF.ToString();  // Later chnage
                newRow["Duty&pal"] = quotation.QuotationItems[i].Dutypal.ToString();// Later chnage
                newRow["Clearing"] = quotation.QuotationItems[i].Clearingcost.ToString(); // Later chnage
                newRow["History"] = quotation.QuotationItems[i].History;
                newRow["Validity"] = quotation.QuotationItems[i].Validity.ToString("yyyy-MM-dd");
                newRow["Other"] = quotation.QuotationItems[i].Other.ToString(); // Later chnage
                newRow["VatRate"] = ListVATNBTValues != null ? ListVATNBTValues.VatRate.ToString("N4") : "0";
                newRow["NBTRate1"] = ListVATNBTValues != null ? (ListVATNBTValues.NBTRate1 * 100).ToString("N2") + "%" : "0";
                newRow["NBTRate2"] = ListVATNBTValues != null ? (ListVATNBTValues.NBTRate2 * 100).ToString("N2") + "%" : "0";
                newRow["HasNBTRate"] = hasNBTRate.ToString();
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

                DropDownList ddlHistory = e.Row.FindControl("ddlHistory") as DropDownList;
                ddlHistory.Enabled = true;
                ddlHistory.DataSource = importsHistoryController.GetHistoryForQuotationSubmission();
                ddlHistory.DataValueField = "HistoryId";
                ddlHistory.DataTextField = "History";
                ddlHistory.DataBind();
                ddlHistory.Items.Insert(0, new ListItem("Select", "0"));

                TextBox txt = e.Row.FindControl("txtExchangeRate") as TextBox;
                txt.Text = "1";  // Default Value is 1 

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
            }

            if (ListVATNBTValues != null) {
                if (ListVATNBTValues.NBTRate1 == 0 && ListVATNBTValues.NBTRate2 == 0) {
                    e.Row.Cells[30].CssClass = "hidden";   // hidding NBT Radio button
                    e.Row.Cells[31].CssClass = "hidden";   // hidding NBT Value Column
                }
            }

            if (hndImport.Value != "1")  // if local
            {
                for (int i = 14; i < 23; i++) {
                    e.Row.Cells[i].CssClass = "hidden";
                }
                for (int i = 24; i < 29; i++) {
                    e.Row.Cells[i].CssClass = "hidden";
                }

                e.Row.Cells[8].CssClass = "hidden";

            }
            else {
                e.Row.Cells[13].CssClass = "hidden";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e) {
            msg.Visible = false;
            try {
                int EmptyPrice = 0;
                for (int i = 0; i < gvBidItems.Rows.Count; i++) {
                    if ((gvBidItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text == "") {
                        EmptyPrice = 1;
                       
                    }

                }

                if (EmptyPrice == 0) { 
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
                            quotationItem.Qty = decimal.Parse(gvBidItems.Rows[i].Cells[9].Text);
                            quotationItem.EstimatedPrice = decimal.Parse(gvBidItems.Rows[i].Cells[11].Text);
                            quotationItem.Description = (gvBidItems.Rows[i].FindControl("txtDescription") as TextBox).Text;
                            // quotationItem.CurrencyId = Convert.ToInt32((gvBidItems.Rows[i].FindControl("dllCurrency") as DropDownList).SelectedValue);
                            ///quotationItem.ExchangeRate = Convert.ToDecimal((gvBidItems.Rows[i].FindControl("txtExchangeRate") as TextBox).Text);
                            quotationItem.UnitPrice = decimal.Parse((gvBidItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text);
                            quotationItem.SubTotal = decimal.Parse((gvBidItems.Rows[i].FindControl("txtSubTotal") as TextBox).Text);
                            quotationItem.HasVat = (gvBidItems.Rows[i].FindControl("chkVat") as CheckBox).Checked == true ? 1 : 0;
                            quotationItem.VatAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtVat") as TextBox).Text);
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


                            decimal TotalCost = 0;
                            TotalCost = (newImportQuotationItem.CIF + newImportQuotationItem.ClearingCost + newImportQuotationItem.DutyPal + newImportQuotationItem.Other);
                            newImportQuotationItem.Total = TotalCost;

                            importQuotation.ImportQuotationItemList.Add(newImportQuotationItem);
                        }
                        //else {
                        //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please enter Quoted Price and Description'}); });   </script>", false);

                        //}
                    }
                    int result = 0;
                    if (newQuotation.QuotationItems.Count > 0) {
                        result = quotationController.SaveSupplierQuotation(newQuotation, importQuotation, int.Parse(ViewState["PurchaseType"].ToString()));
                        Bid_Bond_Details bidBondDetail = new JavaScriptSerializer().Deserialize<Bid_Bond_Details>(ViewState["bidBondDetail"].ToString());
                        if (bidBondDetail.IsRequired == 1) {
                            saveSupplierBidBondDetails();
                        }

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
                                item.VatAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtVat") as TextBox).Text);
                                item.HasNbt = (gvBidItems.Rows[i].FindControl("chkNbt") as CheckBox).Checked == true ? 1 : 0;
                                item.NbtCalculationType = (gvBidItems.Rows[i].FindControl("rdoNbt204") as RadioButton).Checked == true ? 1 : 2;
                                item.NbtAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtNbt") as TextBox).Text);
                                item.TotalAmount = decimal.Parse((gvBidItems.Rows[i].FindControl("txtNetTotal") as TextBox).Text);
                                item.Clearingcost = decimal.Parse((gvBidItems.Rows[i].FindControl("txtClearing") as TextBox).Text.Replace(",", ""));
                                item.Dutypal = decimal.Parse((gvBidItems.Rows[i].FindControl("txtdutypal") as TextBox).Text.Replace(",", ""));
                                item.Brand = (gvBidItems.Rows[i].FindControl("txtBrand") as TextBox).Text;
                                item.Terms = (gvBidItems.Rows[i].FindControl("ddlTerms") as DropDownList).SelectedValue;
                                item.CIF = decimal.Parse((gvBidItems.Rows[i].FindControl("txtCif") as TextBox).Text.Replace(",", ""));
                                if (ViewState["PurchaseType"].ToString() == "1") {
                                    item.Validity = DateTime.ParseExact((gvBidItems.Rows[i].FindControl("txtvalidity") as TextBox).Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                                }
                                else {
                                    item.Validity = LocalTime.Now;
                                }

                                item.Other = decimal.Parse((gvBidItems.Rows[i].FindControl("txtother") as TextBox).Text.Replace(",", ""));
                                item.History = (gvBidItems.Rows[i].FindControl("ddlHistory") as DropDownList).SelectedValue;
                                //  item.Refno = (gvBidItems.Rows[i].FindControl("txtRefno") as TextBox).Text;
                                item.Estdelivery = (gvBidItems.Rows[i].FindControl("txtEstdelivery") as TextBox).Text;
                                item.Remark = (gvBidItems.Rows[i].FindControl("txtRemark") as TextBox).Text;

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
                        int result = quotationController.UpdateSupplierQuotation(quotation);
                        Bid_Bond_Details bidBondDetail = new JavaScriptSerializer().Deserialize<Bid_Bond_Details>(ViewState["bidBondDetail"].ToString());
                        if (bidBondDetail.IsRequired == 1) {
                            saveSupplierBidBondDetails();
                        }

                        if (result > 0) {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            ClearFields();
                        }
                        else {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on submitting quotation'}); });   </script>", false);
                        }
                    }
                }
            }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Quotations cannot be empty. Unit price should be 0 for empty quotations.'}); });   </script>", false);
                }
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
            //var selectedValues = (row.FindControl("ddlTerms") as DropDownList).SelectedValue;
            var selectedValues = (row.FindControl("ddlTerms") as DropDownList).SelectedValue;
            if (selectedValues != "0" && selectedValues != "11") {
                (row.FindControl("txtdutypal") as TextBox).Enabled = true;
                (row.FindControl("txtClearing") as TextBox).Enabled = true;
                (row.FindControl("txtCIF") as TextBox).Enabled = true;
                (row.FindControl("txtUnitPrice") as TextBox).Attributes.Add("readonly", "readonly");

                // (row.FindControl("txtUnitPrice") as TextBox).Style.Add("readonly", "readonly!important");
                (row.FindControl("txtother") as TextBox).Enabled = true;

            }
            else {
                (row.FindControl("txtdutypal") as TextBox).Enabled = false;
                (row.FindControl("txtClearing") as TextBox).Enabled = false;
                (row.FindControl("txtCIF") as TextBox).Enabled = false;
                (row.FindControl("txtdutypal") as TextBox).Text = "0.00";
                (row.FindControl("txtClearing") as TextBox).Text = "0.00";
                (row.FindControl("txtCIF") as TextBox).Text = "0.00";
                (row.FindControl("txtUnitPrice") as TextBox).Attributes.Remove("readonly");
                (row.FindControl("txtother") as TextBox).Enabled = false;
                (row.FindControl("txtother") as TextBox).Text = "0.00";
            }
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
            int currencyTypeId = int.Parse(ddlCurrencyDetail.SelectedValue);

            CurrencyRate Rate = currencyRateController.FetchCurrencyRatesByMaxDate(currencyTypeId);

            for (int i = 0; i < gvBidItems.Rows.Count; i++) {
                TextBox TxtRate = (gvBidItems.Rows[i].FindControl("txtExchangeRate")) as TextBox;
                TxtRate.Text = Rate.SellingRate.ToString();
            }
        }
    }
}