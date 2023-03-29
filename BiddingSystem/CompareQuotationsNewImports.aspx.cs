using BiddingSystem.Helpers;
using BiddingSystem.ViewModels.CS;
using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CompareQuotationsNewImports : System.Web.UI.Page
    {
        #region Controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        SupplierQuotationItemController supplierQuotationItemController = ControllerFactory.CreateSupplierQuotationItemController();
        BiddingItemController biddingItemController = ControllerFactory.CreateBiddingItemController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        TabulationMasterController tabulationMasterController = ControllerFactory.CreateTabulationMasterController();
        TabulationDetailController tabulationDetailController = ControllerFactory.CreateTabulationDetailController();
        ImportsController importsController = ControllerFactory.createImportsController();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForQuotationComparison.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "bidComparrisionLink";

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 5) && companyLogin.Usertype != "S")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }

                Session["PrID"] = Convert.ToInt32(Request.QueryString.Get("PrId"));

                Session["PRDetails"] = pr_MasterController.getPRMasterDetailByPrId(Convert.ToInt32(Session["PrID"].ToString()));
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
                        Session["Status"] = null;
                        var PrId = int.Parse(Request.QueryString.Get("PrId"));
                        BindBasicDetails(PrId, Convert.ToInt32(Session["CompanyId"].ToString()));
                        LoadGV();

                        //Session["SelectedQuotations"] = null;
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }

                }
            }


        }

        #region Events

        protected void btnView_Click(object sender, EventArgs e)
        {
            int BidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            Session["BidId"] = BidId;
            ViewState["BidCode"] = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Where(x => x.BidId == BidId).FirstOrDefault().BidCode.ToString();
            ViewState["tabulationId"] = tabulationMasterController.InsertTabulationMaster(int.Parse(Session["PrId"].ToString()), int.Parse(Session["BidId"].ToString()), int.Parse(Session["UserId"].ToString()), Convert.ToInt32(Session["MesurementID"]));
            ViewState["PurchaseType"] = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).PurchaseType;



            LoadBidItems(gvItems);
            LoadBidQuotations();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('show');  }); ShowSelectedRows();  </script>", false);

        }

        protected void gvItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;
                int BidItemId = int.Parse(gvItems.DataKeys[e.Row.RowIndex].Value.ToString());
                Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(Session["BidId"].ToString()));
                List<SupplierQuotationItem> quotationItems =LoadBidItemsToGridComparison(BidItemId, gvQuotationItems, bid);
                BindCalucationDetails(BidItemId, gvQuotationItems, bid, quotationItems);
                PushSeletedRows(gvQuotationItems, "items");
            }

        }


        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());

                List<BiddingItem> listBiddingitems = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                listBiddingitems = TabulationCommon.RemoveNull(listBiddingitems);
                gvBidItems.DataSource = listBiddingitems;
                gvBidItems.DataBind();
            }
        }

        protected void btnTerminate_Click(object sender, EventArgs e)
        {
            var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString());
            foreach (GridViewRow row in gvBids.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckRow = (row.Cells[0].FindControl("CheckBoxG1") as CheckBox);
                    if (CheckRow.Checked)
                    {

                        int bidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);

                        int result = biddingController.TerminateBid(bidId, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value.ProcessString());

                        if (result > 0)
                        {
                            PrMaster.Bids.Find(b => b.BidId == bidId).IsTerminated = 1;
                            PrMaster.Bids.Find(b => b.BidId == bidId).BiddingItems.ForEach(bi => bi.IsTerminated = 1);
                            Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                            LoadGV();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on Terminating Bid', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        }
                    }
                }
            }
        }

        //Method For Select Item Wise or Supplierwise
        [WebMethod]
        public static string SelectBidItemClick(TabulationDetail TabulationDet)
        {
            try
            {
                 
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    Page page = (Page)HttpContext.Current.Handler;
                    HiddenField hdnQuotationCount = (HiddenField)page.FindControl("hdnRejectedQuotationCount");
                    string ListCount = "";
                    ResultVM response = new ResultVM();
                    string Message = "";
                    int result = 0;
                    string btnStatus = "";
                    try
                    {
                        
                        if (HttpContext.Current.Session["SelectedQuotations"] == null  )
                        {
                            List<TabulationDetail> TabulationDetailsList= new List<TabulationDetail>();

                            TabulationDetailsList.Add(TabulationDet);

                            if(TabulationDet.SelectedValue == 0)
                            {
                               
                                var itemtoRemove = TabulationDetailsList.Single(x => x.QuotationId == TabulationDet.QuotationId && x.QuotationItemId == TabulationDet.QuotationItemId);
                                TabulationDetailsList.Remove(itemtoRemove);
                                HttpContext.Current.Session["SelectedQuotations"] = new JavaScriptSerializer().Serialize(TabulationDetailsList);
                                result = ControllerFactory.CreateTabulationDetailController().DeleteTabulationDetailImports(TabulationDet.ItemId, TabulationDet.TabulationId, TabulationDet.QuotationId, TabulationDet.SupplierId, 0, int.Parse(HttpContext.Current.Session["UserId"].ToString()), int.Parse(HttpContext.Current.Session["BidId"].ToString()), int.Parse(HttpContext.Current.Session["PrID"].ToString()));

                                Message = "Removed Successfully..!!";
                                btnStatus = "Remove";
                            }
                            else
                            {
                                HttpContext.Current.Session["SelectedQuotations"] = new JavaScriptSerializer().Serialize(TabulationDetailsList);

                                ListCount = TabulationDetailsList.Count.ToString();

                                List<ImportCalucationDetails> listDetails = GetImportDetails(TabulationDet.QuotationId, TabulationDet.QuotationItemId);
                                //result = ControllerFactory.CreateTabulationDetailController().UpdateTabulationDetailImports(int.Parse(HttpContext.Current.Session["UserId"].ToString()), TabulationDet.TotQty, TabulationDet.VAtAmount, TabulationDet.NbtAmount, TabulationDet.NetTotal, TabulationDet.SubTotal, TabulationDet.ItemId,
                                //         TabulationDet.TabulationId, TabulationDet.QuotationId, TabulationDet.SupplierId, TabulationDet.ApprovalRemark, (TabulationDet.SupplierMentionedItemName != null ? TabulationDet.SupplierMentionedItemName : ""), 0, int.Parse(HttpContext.Current.Session["BidId"].ToString()),
                                //         int.Parse(HttpContext.Current.Session["PrID"].ToString()), listDetails[0], TabulationDet.QuotationItemId);

                                Message = "Selected Successfully..!!";
                                btnStatus = "Select";
                            }
                            
                        }
                        else
                        {
                            List<TabulationDetail> TabulationDetailsList = new JavaScriptSerializer().Deserialize<List<TabulationDetail>>(HttpContext.Current.Session["SelectedQuotations"].ToString());

                            if(TabulationDetailsList.Count > 0)
                            {
                                if (TabulationDetailsList.Where(x => x.QuotationId == TabulationDet.QuotationId && x.QuotationItemId == TabulationDet.QuotationItemId && x.Qty >= TabulationDet.TotQty).ToList().Count == 0)
                                {
                                    TabulationDetailsList.Add(TabulationDet);

                                    HttpContext.Current.Session["SelectedQuotations"] = new JavaScriptSerializer().Serialize(TabulationDetailsList);

                                    ListCount = TabulationDetailsList.Count.ToString();
                                    ImportCalucationDetails importCalucationDetails = new JavaScriptSerializer().Deserialize<PR_Master>(HttpContext.Current.Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(HttpContext.Current.Session["BidId"].ToString())).SupplierQuotations.Find(s => s.QuotationId == TabulationDet.QuotationId).objImportCalucationDetails;
                                    //result = ControllerFactory.CreateTabulationDetailController().UpdateTabulationDetailImports(int.Parse(HttpContext.Current.Session["UserId"].ToString()), TabulationDet.TotQty, TabulationDet.VAtAmount, TabulationDet.NbtAmount, TabulationDet.NetTotal, TabulationDet.SubTotal, TabulationDet.ItemId,
                                    //TabulationDet.TabulationId, TabulationDet.QuotationId, TabulationDet.SupplierId, TabulationDet.ApprovalRemark, (TabulationDet.SupplierMentionedItemName != null ? TabulationDet.SupplierMentionedItemName : ""), 0, int.Parse(HttpContext.Current.Session["BidId"].ToString()),
                                    //int.Parse(HttpContext.Current.Session["PrID"].ToString()), importCalucationDetails, TabulationDet.QuotationItemId);


                                    Message = "Selected Successfully..!!";
                                    btnStatus = "Select";
                                }
                                else
                                {
                                    if (TabulationDetailsList.Where(x => x.SelectedValue == 1).ToList().Count > 0 && TabulationDet.SelectedValue == 1)
                                    {
                                        result = 2;
                                        btnStatus = "Select";
                                    }
                                    else
                                    {
                                        var itemtoRemove = TabulationDetailsList.Single(x => x.QuotationId == TabulationDet.QuotationId && x.QuotationItemId == TabulationDet.QuotationItemId);
                                        TabulationDetailsList.Remove(itemtoRemove);
                                        HttpContext.Current.Session["SelectedQuotations"] = new JavaScriptSerializer().Serialize(TabulationDetailsList);
                                        result = ControllerFactory.CreateTabulationDetailController().DeleteTabulationDetailImports( TabulationDet.ItemId, TabulationDet.TabulationId, TabulationDet.QuotationId, TabulationDet.SupplierId,0, int.Parse(HttpContext.Current.Session["UserId"].ToString()), int.Parse(HttpContext.Current.Session["BidId"].ToString()), int.Parse(HttpContext.Current.Session["PrID"].ToString()));

                                        Message = "Removed Successfully..!!";
                                        btnStatus = "Remove";
                                    }

                                }
                            }
                            else
                            {
                                List<TabulationDetail> TabulationDetailsListNew = new List<TabulationDetail>();

                                TabulationDetailsListNew.Add(TabulationDet);
                                HttpContext.Current.Session["SelectedQuotations"] = new JavaScriptSerializer().Serialize(TabulationDetailsListNew);

                                ListCount = TabulationDetailsListNew.Count.ToString();

                                ImportCalucationDetails importCalucationDetails = new JavaScriptSerializer().Deserialize<PR_Master>(HttpContext.Current.Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(HttpContext.Current.Session["BidId"].ToString())).SupplierQuotations.Find(s => s.QuotationId == TabulationDet.QuotationId).objImportCalucationDetails;

                                //result = ControllerFactory.CreateTabulationDetailController().UpdateTabulationDetailImports(int.Parse(HttpContext.Current.Session["UserId"].ToString()), TabulationDet.TotQty, TabulationDet.VAtAmount, TabulationDet.NbtAmount, TabulationDet.NetTotal, TabulationDet.SubTotal, TabulationDet.ItemId,
                                //    TabulationDet.TabulationId, TabulationDet.QuotationId, TabulationDet.SupplierId, TabulationDet.ApprovalRemark, (TabulationDet.SupplierMentionedItemName != null ? TabulationDet.SupplierMentionedItemName : ""), 0, int.Parse(HttpContext.Current.Session["BidId"].ToString()),
                                //    int.Parse(HttpContext.Current.Session["PrID"].ToString()), importCalucationDetails, TabulationDet.QuotationItemId);
                                Message = "Selected Successfully..!!";
                                btnStatus = "Select";
                            }
                            

                            
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        result = 0;

                    }
                    
                    if (result == 1)
                    {

                        response.Status = 200;
                        response.Data = Message;
                        response.ButtonStatus = btnStatus;
                    }
                    else if(result == 2)
                    {
                        response.Status = 500;
                        response.Data = "You Have Already Selected Item..!!";
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }

        //Method For Finalize Tabulation 
        [WebMethod]
        public static string FinalizeTabulationClick(SupplierQuotationItem SupQutItem)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    //var PrMasterDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PRDetails"].ToString());
                    ResultVM response = new ResultVM();
                    int result = 0;
                    if (HttpContext.Current.Session["SelectedQuotations"] != null)
                    {
                        List<TabulationDetail> TabulationDetailsList = new JavaScriptSerializer().Deserialize<List<TabulationDetail>>(HttpContext.Current.Session["SelectedQuotations"].ToString());

                        decimal totVAt = 0;
                        decimal totNbt = 0;
                        decimal totNetTot = 0;
                        decimal totSubTot = 0;
                        int TabulationId = 0;

                       

                        for (int i = 0; i < TabulationDetailsList.Count; i++)
                        {

                            totVAt = totVAt + TabulationDetailsList[i].VAtAmount;
                            totNbt = totNbt + TabulationDetailsList[i].NbtAmount;
                            totNetTot = totNetTot + TabulationDetailsList[i].NetTotal;
                            totSubTot = totSubTot + TabulationDetailsList[i].SubTotal;
                            TabulationId = TabulationDetailsList[i].TabulationId;

                        }
                        int categoryId = new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PrMaster"].ToString()).PrCategoryId;

                        result = ControllerFactory.CreateTabulationMasterController().UpdateTabulationDetails(int.Parse(HttpContext.Current.Session["UserId"].ToString()), totVAt, totNbt, totNetTot, totSubTot, int.Parse(HttpContext.Current.Session["BidId"].ToString()), TabulationId, int.Parse(HttpContext.Current.Session["PrID"].ToString()), categoryId,
                            int.Parse(HttpContext.Current.Session["DesignationId"].ToString()), SupQutItem.Remark, TabulationDetailsList, 2);
                    }
                    else
                    {
                        result = 0;
                    }

                    if (result > 0)
                    {

                        response.Status = 200;
                        response.Data = "Tabulation Finalized..!";
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }


        [WebMethod]
        public static string ReOpenBidClick(SupplierQuotationItem SupQutItem)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    //var PrMasterDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PRDetails"].ToString());
                    ResultVM response = new ResultVM();
                    int result = 0;
                    var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(HttpContext.Current.Session["PrMaster"].ToString());
                    int bidId = SupQutItem.BidId;



                     result = ControllerFactory.CreateBiddingController().ResetSelections(bidId);

                    if (result > 0)
                    {
                        result = ControllerFactory.CreateBiddingController().ReOpenBid(bidId, int.Parse(HttpContext.Current.Session["UserId"].ToString()), LocalTime.Now.AddDays(int.Parse(SupQutItem.Days.ProcessString())), SupQutItem.Remark);

                        if (result > 0)
                        {
                            PrMaster.Bids.Remove(PrMaster.Bids.Find(b => b.BidId == bidId));
                            HttpContext.Current.Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                            
                        }
                       
                    }

                    if (result > 0)
                    {

                        response.Status = 200;
                        response.Data = "Bid Re Opened..!";
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error on Reopening Bid";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }


        protected void gvQuotationItemsSup_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvItemSupllier = (GridView)e.Row.FindControl("gvItemSupllier");//add this


                DataRowView dr = (DataRowView)e.Row.DataItem;
                string field1 = dr[0].ToString();
                int QuotationId = Convert.ToInt32(dr[1]);

                LoadBidItemsSupplier(gvItemSupllier, QuotationId);
                ////LoadBidItems(gvItemSupllier);

            }


        }



        protected void btnAttachMents_Click(object sender, EventArgs e)
        {
            var QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);
            int BidID = Convert.ToInt32(Session["BidId"].ToString());
            var Attachementlist = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == BidID).SupplierQuotations.Find(q => q.QuotationId == int.Parse(QuotationId.ToString()));

            gvDocs.DataSource = Attachementlist.UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = Attachementlist.QuotationImages;
            gvImages.DataBind();
            txtTermsAndConditions.Text = Attachementlist.TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('hide'); $('#mdlAttachments').modal('show') });   </script>", false);

        }

        protected void btnsupplerview_Click(object sender, EventArgs e)
        {
            var SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[4].Text);
            Response.Redirect("CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId);
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>window.open ('CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId + "','_blank');</script>",false);
        }

        protected void btnPurchased_Click(object sender, EventArgs e)
        {
            var ItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[5].Text);

            List<PODetails> listDetails = pODetailsController.GetPUrchasedItems(ItemId, int.Parse(Session["CompanyId"].ToString()));
            gvPurchasedItems.DataSource = listDetails;
            gvPurchasedItems.DataBind();
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItems').modal('show'); });   </script>", false);

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('hide'); $('#mdlItems').modal('show'); });   </script>", false);


        }



        [WebMethod]
        public static string ApproveClick(SupplierQuotationItem SupQutItem)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    var PrMasterDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PRDetails"].ToString());
                    string ListCount = "";
                    ResultVM response = new ResultVM();
                    int approve = 0;
                    try
                    {
                        approve = ControllerFactory.CreatePR_MasterController().ApproveBid(int.Parse(HttpContext.Current.Session["PrID"].ToString()), int.Parse(HttpContext.Current.Session["CompanyId"].ToString()), SupQutItem.Remark);



                    }
                    catch (Exception ex)
                    {
                        approve = 0;
                    }



                    if (approve > 0)
                    {

                        response.Status = 200;
                        response.Data = "PR " + PrMasterDetails.PrCode;
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewPrForTabulationsheetApprovals.aspx");
        }



        protected void btnPrint_Click(object sender, EventArgs e)
        {

            int no = 0;
            GridView gv = new GridView();
            gv.AutoGenerateColumns = false;

            gv.Columns.Add(new BoundField() { DataField = "No", HeaderText = "#" });
            gv.Columns.Add(new BoundField() { DataField = "ItemName", HeaderText = "Item Name" });
            gv.Columns.Add(new BoundField() { DataField = "Supplier", HeaderText = "Supplier" });
            gv.Columns.Add(new BoundField() { DataField = "SupplierMentionedItemName", HeaderText = "Supplier Mentioned Item Name" });
            gv.Columns.Add(new BoundField() { DataField = "Refno", HeaderText = "Reference No" });
            
            gv.Columns.Add(new BoundField() { DataField = "Country", HeaderText = "Country Name" });
            gv.Columns.Add(new BoundField() { DataField = "Agent", HeaderText = "Agent Name" });
            gv.Columns.Add(new BoundField() { DataField = "PaymentMode", HeaderText = "Payment Mode" });
            gv.Columns.Add(new BoundField() { DataField = "Brand", HeaderText = "Brand" });
            gv.Columns.Add(new BoundField() { DataField = "Mill", HeaderText = "Mill" });
            gv.Columns.Add(new BoundField() { DataField = "Term", HeaderText = "Term" });
            gv.Columns.Add(new BoundField() { DataField = "Remarks", HeaderText = "Remark" });
            gv.Columns.Add(new BoundField() { DataField = "Validity", HeaderText = "Validity" });
            gv.Columns.Add(new BoundField() { DataField = "ImportHistory", HeaderText = "Import History" });
            gv.Columns.Add(new BoundField() { DataField = "EstDelivery", HeaderText = "Est Delivery" });
            gv.Columns.Add(new BoundField() { DataField = "CurrencyShortName", HeaderText = "Currency" });

            gv.Columns.Add(new BoundField() { DataField = "CIF($)", HeaderText = "Total CIF ($)" });
            gv.Columns.Add(new BoundField() { DataField = "CIF(LKR)", HeaderText = "Total CIF (LKR)" });
            gv.Columns.Add(new BoundField() { DataField = "DutyPalTax", HeaderText = "Duty, PAL, Other TAX" });
            gv.Columns.Add(new BoundField() { DataField = "CostOfChemicals", HeaderText = "Cost of Chemicals" });
            gv.Columns.Add(new BoundField() { DataField = "EstLandedCost", HeaderText = "Est Landed Cost" });
            gv.Columns.Add(new BoundField() { DataField = "EstClearing", HeaderText = "Est Clearing/Mt" });
            gv.Columns.Add(new BoundField() { DataField = "EstCost", HeaderText = "Est Cost/Mt (LKR)" });

            gv.Columns.Add(new BoundField() { DataField = "MeasurementShortName", HeaderText = "Unit" });
            gv.Columns.Add(new BoundField() { DataField = "UnitPrice", HeaderText = "Unit Price" });
            gv.Columns.Add(new BoundField() { DataField = "Quantity", HeaderText = "Quantity" });
            gv.Columns.Add(new BoundField() { DataField = "SubTotal", HeaderText = "SubTotal" });
            gv.Columns.Add(new BoundField() { DataField = "NbtAmount", HeaderText = "NBT" });
            gv.Columns.Add(new BoundField() { DataField = "VatAmount", HeaderText = "VAT" });
            gv.Columns.Add(new BoundField() { DataField = "NetTotal", HeaderText = "NetTotal" });
            gv.Columns.Add(new BoundField() { DataField = "SelectedQuantity", HeaderText = "Quantity" });
            gv.Columns.Add(new BoundField() { DataField = "SelectedSubTotal", HeaderText = "SubTotal" });
            gv.Columns.Add(new BoundField() { DataField = "SelectedNbtAmount", HeaderText = "NBT" });
            gv.Columns.Add(new BoundField() { DataField = "SelectedVatAmount", HeaderText = "VAT" });
            gv.Columns.Add(new BoundField() { DataField = "SelecetedNetTotal", HeaderText = "NetTotal" });
            gv.Columns.Add(new BoundField() { DataField = "IsSelected", HeaderText = "Action Status" });

            DataTable dt = new DataTable();

            dt.Columns.Add("No");
            dt.Columns.Add("ItemName");
            dt.Columns.Add("Supplier");
            dt.Columns.Add("SupplierMentionedItemName");
            dt.Columns.Add("Refno");
            dt.Columns.Add("Country");
            dt.Columns.Add("Agent");
            dt.Columns.Add("PaymentMode");
            dt.Columns.Add("Brand");
            dt.Columns.Add("Mill");
            dt.Columns.Add("Term");
            dt.Columns.Add("Remarks");
            dt.Columns.Add("Validity");
            dt.Columns.Add("ImportHistory");
            dt.Columns.Add("EstDelivery");
            dt.Columns.Add("CurrencyShortName");

            dt.Columns.Add("CIF($)");
            dt.Columns.Add("CIF(LKR)");
            dt.Columns.Add("DutyPalTax");
            dt.Columns.Add("CostOfChemicals");
            dt.Columns.Add("EstLandedCost");
            dt.Columns.Add("EstClearing");
            dt.Columns.Add("EstCost");

            dt.Columns.Add("MeasurementShortName");
            dt.Columns.Add("UnitPrice", typeof(string));
            dt.Columns.Add("Quantity", typeof(string));
            dt.Columns.Add("SubTotal", typeof(string));
            dt.Columns.Add("NbtAmount", typeof(string));
            dt.Columns.Add("VatAmount", typeof(string));
            dt.Columns.Add("NetTotal", typeof(string));
            dt.Columns.Add("SelectedQuantity", typeof(string));
            dt.Columns.Add("SelectedSubTotal", typeof(string));
            dt.Columns.Add("SelectedNbtAmount", typeof(string));
            dt.Columns.Add("SelectedVatAmount", typeof(string));
            dt.Columns.Add("SelecetedNetTotal", typeof(string));
            dt.Columns.Add("IsSelected");

            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(Session["BidId"].ToString()));
            
            List<SupplierQuotationItem> Items = new List<SupplierQuotationItem>();

            bid.SupplierQuotations.ForEach(sq => Items.AddRange(sq.QuotationItems));

            Items = Items.OrderBy(qi => qi.ItemId).ToList();

            List<int> ItemIds = Items.Select(qi => qi.ItemId).Distinct().OrderBy(i => i).ToList();

            for (int i = 0; i < ItemIds.Count; i++) {
                List<SupplierQuotationItem> quotationItems = Items.Where(item => item.ItemId == ItemIds[i]).OrderBy(item => item.SubTotal).ToList();

                


                for (int j = 0; j < quotationItems.Count; j++) {
                    no = no + 1;
                    int IsSelected = 0;
                    decimal TotQtySel = 0;
                    decimal UnitPriceSel = 0;
                    decimal SubTotalSel = 0;
                    decimal NbtAmountSel = 0;
                    decimal VatAmountSel = 0;
                    decimal NetTotalSel = 0;

                    //decimal UnitPrice = 0;
                    //decimal SubTotal = 0;
                    //decimal NbtAmount = 0;
                    //decimal VatAmount = 0;
                    //decimal TotalAmount = 0;
                    decimal CIFOriginal = 0;
                    decimal CIFLKR = 0;
                    decimal DutyPalTax = 0;
                    decimal CostOfChemicals = 0;
                    decimal EstClearing = 0;
                    decimal EstCost = 0;
                    decimal LandingCost = 0;
                    decimal EstClearingLKR = 0;
                    

                    SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[j].QuotationId);
                    int TabulationId = bid.Tabulations.Find(b => b.BidId == int.Parse(Session["BidId"].ToString())).TabulationId;
                    List<TabulationDetail> SelectedQuotationList = tabulationDetailController.GetTabulationDetailsByTabulationId(TabulationId);

                    ImportCalucationDetails importDetails = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[j].QuotationId).objImportCalucationDetails;
                    List<ImportCalucationDetails> listDetails = GetImportDetails(quotationItems[j].QuotationId, quotationItems[j].QuotationItemId);

                    //if (Session["SelectedQuotations"] != null) {
                    //List<TabulationDetail> SelectedQuotationList = new JavaScriptSerializer().Deserialize<List<TabulationDetail>>(Session["SelectedQuotations"].ToString());

                    for (int k = 0; k < SelectedQuotationList.Count; k++) {
                        if (SelectedQuotationList[k].IsSelected == 1) {
                            if (quotationItems[j].QuotationItemId == SelectedQuotationList[k].QuotationItemId) {
                                IsSelected = 1;
                                TotQtySel = SelectedQuotationList[k].TotQty;
                                UnitPriceSel = SelectedQuotationList[k].UnitPrice;
                                SubTotalSel = SelectedQuotationList[k].SubTotal;
                                NbtAmountSel = SelectedQuotationList[k].NbtAmount;
                                VatAmountSel = SelectedQuotationList[k].VAtAmount;
                                NetTotalSel = SelectedQuotationList[k].NetTotal;


                            }
                        }
                    }
                   // }

                    if (listDetails != null) {
                        for (int m = 0; m < listDetails.Count; m++) {
                            //UnitPrice = listDetails[m].UnitPrice;
                            //SubTotal = listDetails[m].SubTotal;
                            //NbtAmount = listDetails[m].NBT;
                            //VatAmount = listDetails[m].VAT;
                            //TotalAmount = listDetails[m].NetTotal;
                            CIFOriginal = listDetails[m].OrginalCIFAmount;
                            CIFLKR = listDetails[m].CIFAmountLKR;
                            DutyPalTax = listDetails[m].DuctyPALOther;
                            CostOfChemicals = listDetails[m].CostOfChemicals;
                            LandingCost = listDetails[m].LandedCostLKR;
                            EstClearing = listDetails[m].ClearingCost;
                            EstClearingLKR = listDetails[m].ClearingCostLKR;
                            EstCost = listDetails[m].ClearingCostLKR;
                        }
                    }

                    
                    DataRow newRow = dt.NewRow();
                    newRow["No"] = no;
                    newRow["ItemName"] = quotationItems[j].ItemName;
                    newRow["Supplier"] = quotation.SupplierName;
                    newRow["SupplierMentionedItemName"] = quotationItems[j].SupplierMentionedItemName == "" ? "-" : quotationItems[j].SupplierMentionedItemName;
                    newRow["Refno"] = quotation.QuotationReferenceCode;
                    
                    newRow["Country"] = importDetails.Country; ;
                    newRow["Agent"] = importDetails.Agent; ;
                    newRow["PaymentMode"] = importDetails.PaymentMode; ;
                    newRow["Brand"] = importDetails.Brand; ;
                    newRow["Mill"] = importDetails.Mill; ;
                    newRow["Term"] = importDetails.Term;
                    newRow["Remarks"] = importDetails.Remarks;
                    newRow["Validity"] = importDetails.Validity;
                    newRow["ImportHistory"] = importDetails.ImportHistory;
                    newRow["EstDelivery"] = importDetails.EstDelivery;
                    newRow["CurrencyShortName"] = importDetails.CurrencyShortName;

                    newRow["CIF($)"] = String.Format("{0:0,0.00}", CIFOriginal);
                    newRow["CIF(LKR)"] = String.Format("{0:0,0.00}", CIFLKR);
                    newRow["DutyPalTax"] = String.Format("{0:0,0.00}", DutyPalTax);
                    newRow["CostOfChemicals"] = String.Format("{0:0,0.00}", CostOfChemicals);
                    newRow["EstLandedCost"] = String.Format("{0:0,0.00}", LandingCost);
                    newRow["EstClearing"] = String.Format("{0:0,0.00}", EstClearing);
                    newRow["EstCost"] = String.Format("{0:0,0.00}", EstCost);
                    newRow["MeasurementShortName"] = quotationItems[j].MeasurementShortName;
                    //////newRow["UnitPrice"] = String.Format("{0:0,0.00}", UnitPrice);
                    //////newRow["Quantity"] = String.Format("{0:0,0.00}", quotationItems[j].Qty);
                    //////newRow["SubTotal"] = String.Format("{0:0,0.00}", SubTotal);
                    //////newRow["NbtAmount"] = String.Format("{0:0,0.00}", NbtAmount);
                    //////newRow["VatAmount"] = String.Format("{0:0,0.00}", VatAmount);
                    //////newRow["NetTotal"] = String.Format("{0:0,0.00}", TotalAmount);
                    newRow["UnitPrice"] = String.Format("{0:0,0.00}", quotationItems[j].UnitPrice);
                    newRow["Quantity"] = String.Format("{0:0,0.00}", quotationItems[j].Qty);
                    newRow["SubTotal"] = String.Format("{0:0,0.00}", quotationItems[j].SubTotal);
                    newRow["NbtAmount"] = String.Format("{0:0,0.00}", quotationItems[j].NbtAmount);
                    newRow["VatAmount"] = String.Format("{0:0,0.00}", quotationItems[j].VatAmount);
                    newRow["NetTotal"] = String.Format("{0:0,0.00}", quotationItems[j].TotalAmount);
                    newRow["SelectedQuantity"] = String.Format("{0:0,0.00}", TotQtySel);
                    newRow["SelectedSubTotal"] = String.Format("{0:0,0.00}", SubTotalSel);
                    newRow["SelectedNbtAmount"] = String.Format("{0:0,0.00}", NbtAmountSel);
                    newRow["SelectedVatAmount"] = String.Format("{0:0,0.00}", VatAmountSel);
                    newRow["SelecetedNetTotal"] = String.Format("{0:0,0.00}", NetTotalSel);
                    newRow["IsSelected"] = IsSelected == 0 ? "Not Selected" : "Selected";


                    dt.Rows.Add(newRow);
                }
            }



            gv.DataSource = dt;
            gv.DataBind();


            GridViewRow FirstRow = gv.Rows[0];
            
            Response.Clear();
            Response.Buffer = true;
            string FileName = "B" + bid.BidCode + "_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            string headerTable = @"<Table><tr><td  colspan='36'><center><h4>Quotation Tabulation For Bid No: B" + bid.BidCode + " </h4></center> </td></tr></Table> <hr>";
            Response.Write(headerTable);
            Response.Charset = "";
            string value = @"<Table><tr><td  colspan='25'></td><td  colspan='5' style= 'border: 0.5px solid black;'><center><b>Requested Values</b></center> </td><td  colspan='5' style= 'border: 0.5px solid black;'><center><b>Selected Values</b></center> </td></tr></Table>";
            Response.Write(value);
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter()) {
                HtmlTextWriter hw = new HtmlTextWriter(sw);


                gv.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gv.HeaderRow.Cells) {
                    cell.BackColor = gv.HeaderStyle.BackColor;
                }
                
                for (int i = 0; i < gv.Rows.Count; i++) {
                    GridViewRow currentRow = gv.Rows[i];
                    if (currentRow.Cells[35].Text == "Selected") {
                        currentRow.BackColor = Color.LightGray;

                    }
                }
             
                gv.RenderControl(hw);
                
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }

        }



        protected void btnConfirmRates_Click(object sender, EventArgs e)
        {
            int BidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            LoadRates(BidId);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlRates').modal('show'); });   </script>", false);
        }


        protected void btnDoneRate_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            Panel pnlCommon = (Panel)btn.NamingContainer.FindControl("pnlCommon");

            List<CurrencyRate> ListRates = new List<CurrencyRate>();
            int count = 0;
            int BidId = 0;
            foreach(GridViewRow row in gvRatss.Rows)
            {
                CurrencyRate objCurrencyRate = new CurrencyRate();
                objCurrencyRate.CurrencyTypeId =  Convert.ToInt32 (row.Cells[2].Text);
                objCurrencyRate.SellingRate = Convert.ToDecimal((row.Cells[5].FindControl("txtRate") as TextBox).Text);
                ListRates.Add(objCurrencyRate);
                BidId = Convert.ToInt32(row.Cells[6].Text);
                count++;
            }

            int result = importsController.UpdateRates(ListRates);

            if(result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   PopupRateSection("+ BidId + ");  </script>", false);
                Session["PrMaster"] = new JavaScriptSerializer().Serialize(GetPRMasterDetails());
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'error',title: 'ERROR',text:'Error in Updating Currency Details..!!', showConfirmButton: true,timer: 4000}).then((result) => { $('#mdlRates').modal('show') }); $('.modal-backdrop').remove(); });   </script>", false);
            }
            
           
        }




        [WebMethod]
      
        public static string SaveRatesClick(List<CurrencyRate> dataList)
        {
            try
            {
                if (HttpContext.Current.Session["UserId"] != null)
                {
                    //var PrMasterDetails = new JavaScriptSerializer().Deserialize<PrMasterV2>(HttpContext.Current.Session["PRDetails"].ToString());
                    ResultVM response = new ResultVM();
                    int result = 0;
                    

                    if (result > 0)
                    {

                        response.Status = 200;
                        response.Data = "Bid Re Opened..!";
                    }
                    else
                    {
                        response.Status = 500;
                        response.Data = "Error on Reopening Bid";
                    }


                    return JsonConvert.SerializeObject(response);
                }
                else
                {
                    return SessionExpired();
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return ServerError();
            }
        }



       

        protected void btnImportDet_Click(object sender, EventArgs e)
        {
            int QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);
            int QuotationItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);

            List<ImportCalucationDetails> listDetails = GetImportDetails(QuotationId, QuotationItemId);
            GridImportDetails.DataSource = listDetails;
            GridImportDetails.DataBind();


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('hide'); $('#mdlImportDetails').modal('show'); });   </script>", false);
        }
        protected void btnImportDetSu_Click(object sender, EventArgs e)
        {
            int QuotationId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[3].Text);
            int QuotationItemId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);
            List<ImportCalucationDetails> listDetails = GetImportDetails(QuotationId, QuotationItemId);
            GridImportDetails.DataSource = listDetails;
            GridImportDetails.DataBind();


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('hide'); $('#mdlImportDetails').modal('show'); });   </script>", false);
        }


        #endregion


        #region Methods

        public void BindBasicDetails(int prId, int CompanyId)
        {
            var PrMaster = GetPRMasterDetails();

            lblPRNo.Text = "PR" + PrMaster.PrCode;
            lblCreatedOn.Text = PrMaster.CreatedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
            lblCreatedBy.Text = PrMaster.CreatedByName;
            lblRequestBy.Text = PrMaster.CreatedByName;
            lblRequestFor.Text = PrMaster.RequiredFor;
            lblExpenseType.Text = (PrMaster.ExpenseType == 1) ? "Capital Expense" : "Operational Expense";
            lblDepartment.Text = !String.IsNullOrEmpty(PrMaster.SubDepartmentName) ? PrMaster.SubDepartmentName : "Not Found";
            lblWarehouse.Text = PrMaster.WarehouseName;
            lblMrnId.Text = PrMaster.MrnId != 0 ? "MRN" + PrMaster.MrnCode : "Not From MRN";

         

            ViewState["PrId"] = prId;
            Session["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
            Session["MesurementID"] = PrMaster.MesurementId;
        }


        public PrMasterV2 GetPRMasterDetails()
        {
            
            var SelectionPendingBidIds = Session["SelectionPendingBidIds"] as List<int>;
            ViewState["SelectionPendingBidIds"] = SelectionPendingBidIds;

            ViewState["PrId"] = int.Parse(Request.QueryString.Get("PrId"));
            var PrMaster = pr_MasterController.GetPrForQuotationComparisonImports(int.Parse(ViewState["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()), SelectionPendingBidIds);

            return PrMaster;
        }

        private void LoadGV()
        {
            try
            {
                var PrMaster = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString());
                PrMaster.Bids.ForEach(b => { b.NoOfQuotations = b.SupplierQuotations.Count; });
                PurchaseType.Value = PrMaster.PurchaseType.ToString();
               

                gvBids.DataSource = PrMaster.Bids.Where(b => b.IsQuotationApproved == 0 && b.IsQuotationConfirmed == 0);
                gvBids.DataBind();

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static List<ImportCalucationDetails> GetImportDetails(int QuotationId, int QuotationItemId)
        {
            List<ImportCalucationDetails> listImports = new List<ImportCalucationDetails>();

            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(HttpContext.Current.Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == Convert.ToInt32(HttpContext.Current.Session["BidId"].ToString()));

            List<SupplierQuotation> listSupplierQuotationList = bid.SupplierQuotations;

            foreach(SupplierQuotation objSupplierQuotation in listSupplierQuotationList)
            {
                if(objSupplierQuotation.objImportCalucationDetails.QuotationId == QuotationId)
                {
                    ImportCalucationDetails objImportDet = new ImportCalucationDetails();
                    objImportDet.QuotationId = objSupplierQuotation.objImportCalucationDetails.QuotationId;
                    objImportDet.ExchnageRateNew = ControllerFactory.createImportsController().GetExchangeRate(objSupplierQuotation.objCurrencyDetails.CurrencyTypeId).SellingRate;
                    objImportDet.ExchangeRateValueOld = objSupplierQuotation.objImportCalucationDetails.ExchangeRateValueOld;
                    objImportDet.SNumber = "sdfsf";
                    objImportDet.ReferenceNumber = objSupplierQuotation.QuotationReferenceCode;
                    objImportDet.Brand = objSupplierQuotation.objImportCalucationDetails.Brand;
                    objImportDet.Supplier = objSupplierQuotation.SupplierName;
                    objImportDet.Mill = objSupplierQuotation.objImportCalucationDetails.Mill;
                    objImportDet.Country = objSupplierQuotation.objImportCalucationDetails.Country;
                    objImportDet.Agent = objSupplierQuotation.objImportCalucationDetails.Agent;
                    objImportDet.Gsm = objSupplierQuotation.objImportCalucationDetails.Gsm;
                    objImportDet.Term = objSupplierQuotation.objImportCalucationDetails.Term;
                    objImportDet.OrginalCIFAmount = objSupplierQuotation.objImportCalucationDetails.OrginalCIFAmount ;
                    objImportDet.CIFAmountLKR = (objSupplierQuotation.objImportCalucationDetails.OrginalCIFAmount * objImportDet.ExchnageRateNew);
                    objImportDet.DuctyPALOther = objSupplierQuotation.objImportCalucationDetails.DutyPAL + objSupplierQuotation.objImportCalucationDetails.Other;
                    objImportDet.DutyPALAmount = (((objSupplierQuotation.objImportCalucationDetails.OrginalCIFAmount * objImportDet.ExchnageRateNew) / 100) * objSupplierQuotation.objImportCalucationDetails.DutyPAL);
                    objImportDet.LandedCostLKR = objImportDet.CIFAmountLKR + objImportDet.DutyPALAmount; //  NewCIF amount  + Duty&PAL Amount
                    objImportDet.CostOfChemicals = objImportDet.CIFAmountLKR + objImportDet.DutyPALAmount + objImportDet.LandedCostLKR; //  NewCIF amount  + Duty&PAL Amount + Landed Cost
                    objImportDet.ClearingCostLKR = (objSupplierQuotation.objImportCalucationDetails.ClearingCost / objImportDet.ExchangeRateValueOld) * objImportDet.ExchnageRateNew;
                    objImportDet.Validity = Convert.ToDateTime( objSupplierQuotation.objImportCalucationDetails.Validity).ToShortDateString();
                    objImportDet.EstDelivery = objSupplierQuotation.objImportCalucationDetails.EstDelivery;
                    objImportDet.ImportHistory = objSupplierQuotation.objImportCalucationDetails.ImportHistory;
                    objImportDet.PaymentMode = objSupplierQuotation.objImportCalucationDetails.PaymentMode;
                    objImportDet.Remarks = objSupplierQuotation.objImportCalucationDetails.Remarks;
                    objImportDet.ClearingCost = objImportDet.ClearingCostLKR;
                    objImportDet.Currency = objSupplierQuotation.objCurrencyDetails.CurrentcyName;
                    objImportDet.UnitPrice = (objSupplierQuotation.QuotationItems.Find(x=>x.QuotationItemId == QuotationItemId).UnitPrice / objImportDet.ExchangeRateValueOld) * objImportDet.ExchnageRateNew;

                    objImportDet.ExchangeRateValueOld = objImportDet.ExchnageRateNew;
                    listImports.Add(objImportDet);
                    
                }


            }
            
            return listImports;

        }

        private static string SessionExpired()
        {
            return
                JsonConvert.SerializeObject(
                new ResultVM()
                {
                    Status = 401,
                    Data = "Session Expired"
                });
        }

        private static string ServerError()
        {
            return
                JsonConvert.SerializeObject(
                new ResultVM()
                {
                    Status = 500,
                    Data = "Server Error Occured"
                });
        }


        private void LoadBidItems(GridView gridView)
        {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(Session["BidId"].ToString()));

            List<BiddingItem> items = TabulationCommon.GetBidItems(bid);
            gridView.DataSource = items;
            gridView.DataBind();


        }


        private void LoadRates(int BidId)
        {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == BidId);
                
            List<SupplierQuotation> QuotationList = bid.SupplierQuotations;
            QuotationList = QuotationList.GroupBy(p => p.objCurrencyDetails.CurrencyTypeId)
                              .Select(g => g.First())
                              .ToList();
            gvRatss.DataSource = QuotationList;
            gvRatss.DataBind();


        }

        private void LoadBidItemsSupplier (GridView gridView, int QuotationId)
        {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(Session["BidId"].ToString()));
            int TabId = int.Parse(ViewState["tabulationId"].ToString());
            List<BiddingItem> items = TabulationCommon.GetBidItemsSupplier(bid, QuotationId, TabId);
            gridView.DataSource = items;
            gridView.DataBind();


        }

        public void LoadBidQuotations()
        {
            Bidding bid = new JavaScriptSerializer().Deserialize<PR_Master>(Session["PrMaster"].ToString()).Bids.Find(b => b.BidId == int.Parse(Session["BidId"].ToString()));

            List<BiddingItem> items = TabulationCommon.GetBidItems(bid);
            foreach (BiddingItem item in items)
            {
                List<SupplierQuotationItem> quotationItems = LoadBidItemsToGridComparison(item.BiddingItemId, gvQuotationItemsSup, bid);
                //BindCalucationDetails(item.BiddingItemId, gvQuotationItemsSup, bid, quotationItems);
            }

        }

        
        public List<SupplierQuotationItem> LoadBidItemsToGridComparison(int BidItemId, GridView gridView, Bidding bid)
        {
            List<SupplierQuotationItem> quotationItems = new List<SupplierQuotationItem>();

            DataTable dt = new DataTable();

            dt.Columns.Add("QuotationItemId");
            dt.Columns.Add("QuotationId");
            dt.Columns.Add("BiddingItemId");
            dt.Columns.Add("SupplierId");
            dt.Columns.Add("ReferenceNo");
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
            dt.Columns.Add("HasVat");
            dt.Columns.Add("HasNbt");
            dt.Columns.Add("NbtType");
            dt.Columns.Add("RequestedTotalQty");
            dt.Columns.Add("ItemId");
            dt.Columns.Add("TabulationId");
            dt.Columns.Add("QuotationCount");
            dt.Columns.Add("SupplierMentionedItemName");
            dt.Columns.Add("IsSelectedTB");
            dt.Columns.Add("SelectedQuantity"); 
            //List<TabulationMaster> tabulationMaster = bid.Tabulations;
            //int tabId = 0;
            //for (int k=0; k< tabulationMaster.Count; k++) {
            //     tabId = tabulationMaster[k].TabulationId;
            //}
            int tabId = int.Parse(ViewState["tabulationId"].ToString());
            bid.SupplierQuotations.ForEach(
                sq => quotationItems.AddRange(sq.QuotationItems.Where(sqi => sqi.BiddingItemId == BidItemId && sqi.IsSelected != 2)));

            quotationItems = quotationItems.OrderBy(q => q.UnitPrice).Take(3).ToList();

            for (int i = 0; i < quotationItems.Count; i++)
            {


                SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[i].QuotationId);


                DataRow newRow = dt.NewRow();
                newRow["QuotationItemId"] = quotationItems[i].QuotationItemId;
                newRow["QuotationId"] = quotationItems[i].QuotationId;
                newRow["BiddingItemId"] = quotationItems[i].BiddingItemId;
                newRow["SupplierId"] = quotation.SupplierId;
                newRow["SupplierName"] = quotation.SupplierName;
              
                newRow["ReferenceNo"] = quotation.QuotationReferenceCode;
                newRow["Description"] = quotationItems[i].Description;
                List<TabulationDetail> objdetails = tabulationDetailController.GetSelectedStatus(quotationItems[i].QuotationId, quotation.SupplierId, quotationItems[i].ItemId, quotationItems[i].QuotationItemId);

                decimal UnitPrice = 0;
                decimal SubTotal = 0;
                decimal NbtAmount = 0;
                decimal VatAmount = 0;
                decimal TotalAmount = 0;
                decimal Qty = 0;
                decimal NewRate = 0;
                decimal oldRate = 0;
                if (objdetails.Count > 0)
                {
                    oldRate = importsController.GetExchangeRateSelected(quotationItems[i].QuotationId, tabId, quotationItems[i].QuotationItemId).ExchangeRate;
                    NewRate = importsController.GetExchangeRate(quotation.objCurrencyDetails.CurrencyTypeId).SellingRate;
                    UnitPrice = (objdetails[0].UnitPrice / oldRate) * NewRate;
                    SubTotal = (objdetails[0].SubTotal / oldRate) * NewRate;
                    NbtAmount = (objdetails[0].NbtAmount / oldRate) * NewRate;
                    VatAmount = (objdetails[0].VAtAmount / oldRate) * NewRate;
                    TotalAmount = (objdetails[0].NetTotal / oldRate) * NewRate;
                    Qty = objdetails[0].TotQty;

                    quotationItems[i].UnitPrice = UnitPrice;
                    quotationItems[i].SubTotal = SubTotal;
                    quotationItems[i].NbtAmount = NbtAmount;
                    quotationItems[i].VatAmount = VatAmount;
                    quotationItems[i].TotalAmount = TotalAmount;
                }
                else
                {
                    UnitPrice = quotationItems[i].UnitPrice;
                    SubTotal = quotationItems[i].SubTotal;
                    NbtAmount =  quotationItems[i].NbtAmount;
                    VatAmount = quotationItems[i].VatAmount;
                    TotalAmount = quotationItems[i].TotalAmount;
                    Qty = quotationItems[i].Qty;
                }

                newRow["UnitPrice"] = UnitPrice;
                newRow["SubTotal"] = SubTotal;
                newRow["NbtAmount"] = NbtAmount;
                newRow["VatAmount"] = VatAmount;
                newRow["NetTotal"] = TotalAmount;
                newRow["HasVat"] = quotationItems[i].HasVat;
                newRow["HasNbt"] = quotationItems[i].HasNbt;
                newRow["NbtType"] = quotationItems[i].NbtCalculationType;

                ViewState["SubTotal"] = SubTotal;
                ViewState["NbtAmount"] = NbtAmount;
                ViewState["VatAmount"] = VatAmount;
                ViewState["TotalAmount"] = TotalAmount;
                newRow["RequestedTotalQty"] = quotationItems[i].Qty;
                newRow["ItemId"] = quotationItems[i].ItemId;
                newRow["TabulationId"] = tabId;
                newRow["QuotationCount"] = bid.SupplierQuotations.Count;
                newRow["SupplierMentionedItemName"] = quotationItems[i].SupplierMentionedItemName;
                List <SupplierBOM> boms = quotationItems[i].SupplierBOMs;

                if (boms == null || (boms != null && boms.Count == 0))
                {
                    newRow["SpecComply"] = "No Specs";
                }
                else if (boms.Count == boms.Count(b => b.Comply == 1))
                {
                    newRow["SpecComply"] = "Yes";
                }
                else if (boms.Count == boms.Count(b => b.Comply == 0))
                {
                    newRow["SpecComply"] = "No";
                }
                else
                {
                    newRow["SpecComply"] = "Some";
                }


                newRow["IsBidItemSelected"] = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1 ? "1" : "0";
                newRow["IsSelected"] = quotationItems[i].IsSelected == 1 ? "1" : "0";

                if (i == 0)
                {
                    if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                        newRow["Actions"] = "0";
                    else
                        newRow["Actions"] = "1";

                }
                else
                {
                    newRow["Actions"] = "0";
                }

                if (quotationItems.Count == 1)
                {
                    newRow["ShowReject"] = "0";
                }
                else
                {
                    if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                        newRow["ShowReject"] = "0";
                    else
                        newRow["ShowReject"] = "1";
                }

                if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsTerminated == 1)
                {
                    newRow["Actions"] = "0";
                    newRow["ShowReject"] = "0";

                }
                newRow["IsSelectedTB"] = tabulationDetailController.GetSelectedStatus(quotationItems[i].QuotationId, quotation.SupplierId, quotationItems[i].ItemId, quotationItems[i].QuotationItemId).Count == 1 ? "1" : "0";
                newRow["SelectedQuantity"] = Qty;
                dt.Rows.Add(newRow);

            }

            gridView.DataSource = dt;
            gridView.DataBind();


            return quotationItems;
        }

        public void BindCalucationDetails(int BidItemId, GridView gridView, Bidding bid,List<SupplierQuotationItem> quotationItems)
        {
            for (int j = 0; j <= gridView.Rows.Count - 1; j++)
            {

                TextBox RequestingQty = (gridView.Rows[j].FindControl("txtRequestingQty")) as TextBox;
                Label SubTotal = (gridView.Rows[j].FindControl("lblSubTotal")) as Label;
                Label Nbt = (gridView.Rows[j].FindControl("lblNbt")) as Label;
                Label Vat = (gridView.Rows[j].FindControl("lblVat")) as Label;
                Label NetTotal = (gridView.Rows[j].FindControl("lblNetTotal")) as Label;


                TextBox TSubTotal = (gridView.Rows[j].FindControl("txtSubTotal")) as TextBox;
                TextBox TNbt = (gridView.Rows[j].FindControl("txtNbt")) as TextBox;
                TextBox TVat = (gridView.Rows[j].FindControl("txtVat")) as TextBox;
                TextBox TNetTotal = (gridView.Rows[j].FindControl("txtNetTotal")) as TextBox;

                Label selectedQty = (gridView.Rows[j].FindControl("lblSelectedQuantity")) as Label;

                if (j == 0)
                {

                    decimal qty = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).Qty;
                
                    if (!string.IsNullOrEmpty(selectedQty.Text))
                    {
                        RequestingQty.Text = selectedQty.Text;
                        
                    }
                    else
                    {
                        RequestingQty.Text = qty.ToString();
                    }
                  

                    for (int i = 0; i < quotationItems.Count; i++)
                    {
                        SubTotal.Text = quotationItems[0].SubTotal.ToString("##,0.00");
                        Nbt.Text = quotationItems[0].NbtAmount.ToString("##,0.00");
                        Vat.Text = quotationItems[0].VatAmount.ToString("##,0.00");

                        decimal subTotal = decimal.Parse(quotationItems[0].SubTotal.ToString());
                        decimal nbt = decimal.Parse(quotationItems[0].NbtAmount.ToString());
                        decimal vat = decimal.Parse(quotationItems[0].VatAmount.ToString());
                        NetTotal.Text = (subTotal + nbt + vat).ToString("##,0.00");

                        TSubTotal.Text = quotationItems[0].SubTotal.ToString();
                        TNbt.Text = quotationItems[0].NbtAmount.ToString();
                        TVat.Text = quotationItems[0].VatAmount.ToString();
                        TNetTotal.Text = (subTotal + nbt + vat).ToString();
                    }


                }
                else
                {
                    RequestingQty.Text = "0.00";
                    SubTotal.Text = "0.00";
                    Nbt.Text = "0.00";
                    Vat.Text = "0.00";
                    NetTotal.Text = "0.00";

                    TSubTotal.Text = "0.00";
                    TNbt.Text = "0.00";
                    TVat.Text = "0.00";
                    TNetTotal.Text = "0.00";
                }

            }
        }



        public void PushSeletedRows(GridView gridView, string status)
        {
            //Session["SelectedQuotations"] = null;
            List<TabulationDetail> TabulationDetailsList = new List<TabulationDetail>();
            foreach (GridViewRow row in gridView.Rows)
            {
                Label Seleted = (Label)row.FindControl("lblIsSelectedTB");

                if(Seleted.Text == "1")
                {
                   
                    TabulationDetail TabulationDet = new TabulationDetail();
                    if (status == "items")
                    {
                        TabulationDet.QuotationId =Convert.ToInt32( row.Cells[2].Text);
                        TabulationDet.QuotationItemId = Convert.ToInt32(row.Cells[1].Text);
                        string test = ((TextBox)row.FindControl("txtReqTotQty")).Text;
                        TabulationDet.TotQty = Convert.ToDecimal(((TextBox)row.FindControl("txtRequestingQty")).Text);
                        TabulationDet.VAtAmount = Convert.ToDecimal(((TextBox)row.FindControl("txtVat")).Text);
                        TabulationDet.NbtAmount = Convert.ToDecimal(((TextBox)row.FindControl("txtNbt")).Text);
                        TabulationDet.SubTotal = Convert.ToDecimal(((TextBox)row.FindControl("txtSubTotal")).Text);
                        TabulationDet.NetTotal = Convert.ToDecimal(((TextBox)row.FindControl("txtNetTotal")).Text);
                        TabulationDet.ApprovalRemark = "";
                        TabulationDet.TabulationId = Convert.ToInt32(((Label)row.FindControl("lblTablationId")).Text); ;
                        TabulationDet.SupplierId = Convert.ToInt32(row.Cells[4].Text);
                        TabulationDet.ItemId = Convert.ToInt32(((Label)row.FindControl("lblItemId")).Text); ;
                        TabulationDet.SelectedValue = Convert.ToInt32(((TextBox)row.FindControl("txtSelectedQ")).Text);
                        TabulationDet.Qty = Decimal.ToInt32(Convert.ToDecimal(((TextBox)row.FindControl("txtReqTotQty")).Text));
                        TabulationDet.SupplierMentionedItemName = row.Cells[14].Text;


                    }
                    
                    TabulationDetailsList.Add(TabulationDet);
                }
              
            }

            Session["SelectedQuotations"] = new JavaScriptSerializer().Serialize(TabulationDetailsList);


        }

        

        #endregion

      
    }
}