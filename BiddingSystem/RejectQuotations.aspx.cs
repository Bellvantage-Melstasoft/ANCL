using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.IO;
using System.Drawing;
using System.Globalization;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class RejectQuotations : System.Web.UI.Page
    {
        #region properties
      //  static int PrId = 0;
       // static int BidId = 0;
      //  static string PrCode ;
       // static string BidCode ;
       // static string ReprintPrCode;
       // static string ReprintBidCode;
       // static int UserId = 0;
      //  static int DesignationId = 0;
      //  static int CompanyId = 0;
       // static PrMasterV2 PrMaster;
      //  static List<int> SelectionPendingBidIds;
       //static int Itemcount=0;
       // static int reprintItemcount = 0;
       // static int tabulationId = 0;
       // static TabulationMaster tabulationMaster;
        #endregion

        #region controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        TabulationMasterController tabulationMasterController = ControllerFactory.CreateTabulationMasterController();
        TabulationDetailController tabulationDetailController = ControllerFactory.CreateTabulationDetailController();
        CommitteeController committeeController = ControllerFactory.CreateProcurementCommitteeController();
        SubDepartmentControllerInterface departmentController = ControllerFactory.CreateSubDepartmentController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForQuotationComparison.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "bidComparrisionLink";


               // UserId = int.Parse(Session["UserId"].ToString());
              //  DesignationId = int.Parse(Session["DesignationId"].ToString());
               // CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 5) && companyLogin.Usertype != "S")
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
                        var SelectionPendingBidIds = Session["SelectionPendingBidIds"] as List<int>;
                        ViewState["SelectionPendingBidIds"] = SelectionPendingBidIds;

                        ViewState["PrId"]  = int.Parse(Request.QueryString.Get("PrId"));
                        var PrMaster = pr_MasterController.GetPrForQuotationComparison(int.Parse(ViewState["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()), SelectionPendingBidIds);
                        ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);

                        
                        ViewState["PrCode"] = "PR" + PrMaster.PrCode;
                        LoadBidItems();


                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        

        private void LoadBidItems() {
            ViewState["BidId"] = int.Parse(Request.QueryString.Get("bidId"));
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));
            List<BiddingItem> items = bid.BiddingItems;

            //required when reviewing quotations
            items.ForEach(itms => itms.QuotationCount = 0);

            for (int i = 0; i < items.Count; i++) {

                for (int j = 0; j < bid.SupplierQuotations.Count; j++) {
                    if ((bid.SupplierQuotations[j].QuotationItems.FirstOrDefault(sqi => sqi.BiddingItemId == items[i].BiddingItemId)) != null)
                        items[i].QuotationCount += 1;
                }

            }

            gvItems.DataSource = items;
            gvItems.DataBind();
        }


        protected void btnReject_Click(object sender, EventArgs e) {
            try {
                if (ViewState["RejectedQuotationItems"] == null) {
                    List<SupplierQuotationItem> quotationList = new List<SupplierQuotationItem>();

                    SupplierQuotationItem quotation = new SupplierQuotationItem();

                    quotation.SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);
                    quotation.SupplierName = ((sender as Button).NamingContainer as GridViewRow).Cells[5].Text.ToString();
                    quotation.ItemId = int.Parse(((sender as Button).NamingContainer.NamingContainer.NamingContainer as GridViewRow).Cells[5].Text);
                    quotation.QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);
                    quotation.BiddingItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[3].Text);
                    quotation.BidId = int.Parse(((sender as Button).NamingContainer.NamingContainer.NamingContainer as GridViewRow).Cells[3].Text);

                    quotationList.Add(quotation);

                    ViewState["RejectedQuotationItems"] = new JavaScriptSerializer().Serialize(quotationList);
                }

                else {

                    List<SupplierQuotationItem> quotationList = new JavaScriptSerializer().Deserialize<List<SupplierQuotationItem>>(ViewState["RejectedQuotationItems"].ToString());
                    SupplierQuotationItem quotation = new SupplierQuotationItem();

                    quotation.SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);
                    quotation.SupplierName = ((sender as Button).NamingContainer as GridViewRow).Cells[5].Text.ToString();
                    quotation.ItemId = int.Parse(((sender as Button).NamingContainer.NamingContainer.NamingContainer as GridViewRow).Cells[5].Text);
                    quotation.QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);
                    quotation.BiddingItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[3].Text);
                    quotation.BidId = int.Parse(((sender as Button).NamingContainer.NamingContainer.NamingContainer as GridViewRow).Cells[3].Text);

                    quotationList.Add(quotation);

                    ViewState["RejectedQuotationItems"] = new JavaScriptSerializer().Serialize(quotationList);
                }



            }
            catch (Exception ex) {

            }

        }

        protected void btnsupplerview_Click(object sender, EventArgs e) {
            var SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);
            Response.Redirect("CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId);
        }

        protected void gvQuotations_RowCreated(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                for (int i = 14; i < 14 + (int.Parse(ViewState["Itemcount"].ToString()) * 3); i += 3) {

                    e.Row.Cells[i + 3].CssClass = "CellClick";


                }

            }
        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;
                int BidItemId = int.Parse(gvItems.DataKeys[e.Row.RowIndex].Value.ToString());

                List<SupplierQuotationItem> quotationItems = new List<SupplierQuotationItem>();

                DataTable dt = new DataTable();

                dt.Columns.Add("QuotationItemId");
                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BiddingItemId");
                dt.Columns.Add("SupplierId");
                dt.Columns.Add("SupplierName");
                dt.Columns.Add("Description");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("SubTotal");
                dt.Columns.Add("NbtAmount");
                dt.Columns.Add("VatAmount");
                dt.Columns.Add("NetTotal");
                dt.Columns.Add("SpecComply");
                dt.Columns.Add("Actions");
                dt.Columns.Add("ShowReject");
                dt.Columns.Add("IsBidItemSelected");
                dt.Columns.Add("IsSelected");

                Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(ViewState["BidId"].ToString()));

                bid.SupplierQuotations.ForEach(
                    sq => quotationItems.AddRange(sq.QuotationItems.Where(sqi => sqi.BiddingItemId == BidItemId && sqi.IsSelected != 2)));

                quotationItems = quotationItems.OrderBy(q => q.UnitPrice).Take(3).ToList();

                for (int i = 0; i < quotationItems.Count; i++) {
                    SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[i].QuotationId);


                    DataRow newRow = dt.NewRow();
                    newRow["QuotationItemId"] = quotationItems[i].QuotationItemId;
                    newRow["QuotationId"] = quotationItems[i].QuotationId;
                    newRow["BiddingItemId"] = quotationItems[i].BiddingItemId;
                    newRow["SupplierId"] = quotation.SupplierId;
                    newRow["SupplierName"] = quotation.SupplierName;
                    newRow["Description"] = quotationItems[i].Description;
                    newRow["UnitPrice"] = quotationItems[i].UnitPrice;
                    newRow["SubTotal"] = quotationItems[i].SubTotal;
                    newRow["NbtAmount"] = quotationItems[i].NbtAmount;
                    newRow["VatAmount"] = quotationItems[i].VatAmount;
                    newRow["NetTotal"] = quotationItems[i].TotalAmount;

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
                    newRow["IsSelected"] = quotationItems[i].IsSelected == 1 ? "1" : "0";

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
    
