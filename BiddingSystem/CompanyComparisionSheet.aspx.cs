using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Services;
using System.Web.Script.Services;
using System.Reflection;
using System.Data;

namespace BiddingSystem
{
    public partial class CompanyComparisionSheet : System.Web.UI.Page
    {
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        POMasterController po_MasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController po_DetailsController = ControllerFactory.CreatePODetailsController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        static string UserId = string.Empty;
        static int CompanyId = 0;
        static string PrId = string.Empty;
        static string Item = string.Empty;
        string bidOrderingId = string.Empty;
        List<SupplierQuotation> _supplierQuotation = new List<SupplierQuotation>();
        List<SupplierQuotation> _supplierQuotationDup = new List<SupplierQuotation>();
        List<SupplierQuotation> _supplierQuotationDupList02 = new List<SupplierQuotation>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
               // ((BiddingAdmin)Page.Master).subTabValue = "CompanyBidClosed.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "bidComparrisionLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 5) && companyLogin.Usertype != "S" )|| companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            PrId = Request.QueryString.Get("PrId");

            if(!IsPostBack){
                try
                {
                    GvBind();
                    GvSubmittedBids();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        private void GvBind()
        {
            try
            {
                if (int.Parse(PrId) != 0)
                {
                    //divItems
                    List<SupplierQuotation> supplierQuotation = supplierQuotationController.GetDetailsBidComparison(int.Parse(PrId), int.Parse(Session["CompanyId"].ToString()));
                    
                    //btnProceedPO.Visible = true;
                    bool hasSuppliers = false;

                    for (int i = 0; i < supplierQuotation.Count; i++)
                    {
                        if (supplierQuotationController.GetBidSupplierListForItem(int.Parse(PrId), supplierQuotation[i].ItemId).Count > 0)
                        {
                            hasSuppliers = true;
                            break;
                        }
                        else
                        {
                            supplierQuotation.RemoveAt(i);
                            i -= 1;
                        }
                    }

                    if (!hasSuppliers)
                    {
                        //btnProceedPO.Enabled = false;

                    }


                    if (supplierQuotation.Count != 0)
                    {
                        GridView1.DataSource = supplierQuotation;
                        GridView1.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        Response.Redirect("CompanyBidClosed.aspx");
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GvSubmittedBids()
        {
            try
            {
                List<SupplierQuotation> supplierQuotation = supplierQuotationController.GetDetailsSubmitPO(int.Parse(PrId));
                gvSubmittedPO.DataSource = supplierQuotation;
                gvSubmittedPO.DataBind();
                List<SupplierQuotation> supplierQuotation1 = supplierQuotationController.GetDetailsBidComparison(int.Parse(PrId), int.Parse(Session["CompanyId"].ToString()));

                 if (supplierQuotation1.Count != 0)
                 {
                     GvBind();

                 }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void bind(int prid, int itemId)
        {
            DataTable dt = new DataTable();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
            List<SupplierQuotation> supplierQuotation = supplierQuotationController.GetBidSupplierListForItem(prid, itemId);
            decimal minmumValue = supplierQuotation.Where(v => v.IsRejected == 0).Min(t => t.Amount);
            List<SupplierQuotation> supplierQuotationOrderBy = supplierQuotation.Where(y => y.Amount == minmumValue).ToList();

            ListtoDataTable lsttodt = new ListtoDataTable();
            dt = lsttodt.ToDataTable(supplierQuotationOrderBy);

            gvSupplier = Session["gvSupplier"] as GridView;
            gvSupplier.DataSource = dt;
            gvSupplier.DataBind();
        }

        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }

        [WebMethod]
        [ScriptMethod]
        public static void UpdateRejectedSuppliers(SupplierQuotation supplierQuotation)
        {
            try
            {
                decimal supplierCustomizeAmount = 0;
                SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
                if (supplierQuotation.CustomizeAmount == 0)
                {
                    supplierCustomizeAmount = 0;
                }
                else {
                    supplierCustomizeAmount = supplierQuotation.CustomizeAmount;
                }
                int rejectedcount = 0;
                int selectedCount = 0;
                List<SupplierQuotation> _SupplierQuotation = supplierQuotationController.GetPendingCountOfSupplier(supplierQuotation.PrID, supplierQuotation.ItemId, supplierQuotation.SupplierId);
                foreach (var item in _SupplierQuotation)
                {
                    rejectedcount = item.SupplierRejectedCount;
                    selectedCount = item.SupplierSelectedCount;
                    break;
                }
                if (rejectedcount == 0)
                {
                    rejectedcount = 1;
                }
                if (rejectedcount != 0)
                {
                    rejectedcount = rejectedcount + 1;
                }

                int Update = supplierQuotationController.UpdateIsRejectedSupplier(supplierQuotation.PrID, supplierQuotation.ItemId, supplierQuotation.SupplierId, supplierQuotation.Reason, supplierCustomizeAmount, selectedCount,rejectedcount);
                if (Update > 0)
                {
                    List<SupplierQuotation> supplierQuotations = supplierQuotationController.GetBidSupplierListForItem(supplierQuotation.PrID, supplierQuotation.ItemId);

                    CompanyComparisionSheet bid = new CompanyComparisionSheet();
                    if(supplierQuotations.Count()>0)
                    {
                        bid.bind(supplierQuotation.PrID, supplierQuotation.ItemId);
                    }
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void lblResetSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                int itemId = int.Parse(hdnitemId.Value);
                int prid = int.Parse(hdnPrId.Value);
                int resetSupplier = 0;
                if (prid > 0 && itemId > 0 )
                {
                    resetSupplier = supplierQuotationController.resetSelecteingSuppliert(prid, itemId);
                }
                if (resetSupplier>0)
                {
                    GvBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void lblChooseSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {



                    int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                    int itemId = int.Parse(GridView1.Rows[x].Cells[0].Text);
                    int prid = int.Parse(GridView1.Rows[x].Cells[1].Text);
                    List<PreviousPurchase> fetchPreviousPurchase = new List<PreviousPurchase>();

                    fetchPreviousPurchase = biddingController.GetLastPurchaseSupplier(itemId, CompanyId);
                    List<SupplierQuotation> _SupplierQuotation = supplierQuotationController.GetSupplierPrIdItemId(prid, itemId).ToList();

                    if (_SupplierQuotation.Count == 0)
                    {
                        if (fetchPreviousPurchase.Count() > 0)
                        {
                            foreach (var item in fetchPreviousPurchase)
                            {
                                if (item.Rating == 1)
                                {
                                    item.RatingStar = "★☆☆☆☆";
                                }
                                if (item.Rating == 2)
                                {
                                    item.RatingStar = "★★☆☆☆";
                                }
                                if (item.Rating == 3)
                                {
                                    item.RatingStar = "★★★☆☆";
                                }
                                if (item.Rating == 4)
                                {
                                    item.RatingStar = "★★★★☆";
                                }
                                if (item.Rating == 5)
                                {
                                    item.RatingStar = "★★★★★";
                                }
                            }
                        }
                        gvLastPurchase.DataSource = fetchPreviousPurchase.Take(5);
                        gvLastPurchase.DataBind();

                        string itemName = GridView1.Rows[x].Cells[2].Text;
                        bidOrderingId = GridView1.Rows[x].Cells[10].Text;
                        lblItemName.Text = itemName;
                        lblItem.Text = itemName;
                        lblPr.Text = prid.ToString();
                        lblItemids.Text = itemId.ToString();
                        if (supplierQuotationController.GetBidSupplierListForItem(prid, itemId).Count > 0)
                        {
                            lblQty.Text = supplierQuotationController.GetBidSupplierListForItem(prid, itemId).First().ItemQuantity.ToString();
                        }
                        else
                        {
                            lblQty.Text = "0";
                        }
                        BindGvModal(prid, itemId);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void BindGvModal(int prid, int itemId)
        {
            try
            {
                List<SupplierQuotation> supplierQuotationOrderBy = new List<SupplierQuotation>();
                List<SupplierQuotation> supplierQuotation = supplierQuotationController.GetBidSupplierForItem(prid, itemId).Where(x => x.BidOpeningId == bidOrderingId).ToList();
                lblBidSubmittedSupplierCount.Text = supplierQuotation.Count.ToString();
                supplierQuotation = supplierQuotation.OrderBy(c => c.TotalPrice).ToList();



                if (supplierQuotation.Count() > 3)
                {
                    supplierQuotationOrderBy = supplierQuotation.Take(3).ToList();
                    for (int i = 0; i < supplierQuotationOrderBy.Count(); i++) 
                    { 
			           if(i==0)
                       {
                        supplierQuotationOrderBy[i].DefaultSupplier = 1;
                       }
                      else
                       {
                           supplierQuotationOrderBy[i].DefaultSupplier = 0;
                       }
			        }
                }
                else
                {
                    supplierQuotationOrderBy = supplierQuotation;

                    for (int i = 0; i < supplierQuotationOrderBy.Count(); i++)
                    {
                        if (i == 0)
                        {
                            supplierQuotationOrderBy[i].DefaultSupplier = 1;
                        }
                        else
                        {
                            supplierQuotationOrderBy[i].DefaultSupplier = 0;
                        }
                    }

                }
                gvSupplier.DataSource = supplierQuotationOrderBy;
                gvSupplier.DataBind();

                Session["gvSupplier"] = gvSupplier;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //protected void btnProceedPO_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='CompanyBidClosed.aspx'; });   </script>", false);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //protected void btnProceedPO_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //Response.Redirect("CompanyProceedPOSeperate.aspx",false);
        //        _supplierQuotation = supplierQuotationController.GetNecessaryDataForPO(int.Parse(PrId));

        //        //-----------Check duplicate values in the list
        //        var query = _supplierQuotation.GroupBy(x => x.SupplierId)
        //                   .Where(g => g.Count() > 1)
        //                   .Select(y => y.Key)
        //                   .ToList();

        //        if (query.Count > 0)
        //        {
        //            var grpDupes = from f in _supplierQuotation
        //                           group f by f.SupplierId into grps
        //                           where grps.Count() > 1
        //                           select grps;

        //            foreach (var item in grpDupes)
        //            {
        //                foreach (var itemsList in item)
        //                {
        //                    _supplierQuotationDup.Add(itemsList);
        //                }

        //                if (_supplierQuotationDup.ToList().Count > 0)
        //                {
        //                    var duplicates = _supplierQuotationDup.GroupBy(x => new { x.SupplierId, x.PrID })
        //                                    .Where(x => x.Skip(1).Any()).ToList();
        //                    int pridDup = duplicates[0].Key.PrID;
        //                    int supplierId = duplicates[0].Key.SupplierId;
        //                    int poid = po_MasterController.SavePOMaster(CompanyId, pridDup, supplierId, DateTime.Now, (UserId), 0, 0, "", "", 0, 0, "", 0, DateTime.Now, int.Parse(PrId), 0, 0, 0, "");

        //                    foreach (var items in _supplierQuotationDup)
        //                    {
        //                        int isCustomized = 0;
        //                        if (items.CustomizeAmount != 0 && items.VatAmount != 0 && items.NbtAmount != 0)
        //                        {
        //                            isCustomized = 1;
        //                            if (hdnCustomizedAmount.Value == "")
        //                            {
        //                                hdnCustomizedAmount.Value = items.CustomizeAmount.ToString();
        //                                decimal itemQuantity = items.ItemQuantity;
        //                                decimal totalAmountWithoutVatNbt = items.CustomizeAmount * items.ItemQuantity;
        //                                hdnCustomizedNbt.Value = ((totalAmountWithoutVatNbt * 2) / 98).ToString();
        //                                hdnCustomizedVat.Value = (((totalAmountWithoutVatNbt + decimal.Parse(hdnCustomizedNbt.Value)) * 15) / 100).ToString();
        //                                decimal totalAmountWithVatNbt = totalAmountWithoutVatNbt + decimal.Parse(hdnCustomizedNbt.Value) + decimal.Parse(hdnCustomizedVat.Value);
        //                                hdnCustomizedTotalAmount.Value = totalAmountWithVatNbt.ToString();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            isCustomized = 0;
        //                            hdnCustomizedAmount.Value = "0";
        //                            hdnCustomizedNbt.Value = "0";
        //                            hdnCustomizedVat.Value = "0";
        //                            hdnCustomizedTotalAmount.Value = "0";
        //                        }
        //                        int podetail = po_DetailsController.SavePoDetails(poid, items.QuotationNo, items.ItemId, items.PerItemPrice, items.ItemQuantity, items.TotalAmount, items.VatAmount, items.NbtAmount, Math.Round(decimal.Parse(hdnCustomizedAmount.Value), 2), Math.Round(decimal.Parse(hdnCustomizedVat.Value), 2), Math.Round(decimal.Parse(hdnCustomizedNbt.Value), 2), isCustomized, Math.Round(decimal.Parse(hdnCustomizedTotalAmount.Value),2));
        //                        pr_DetailController.UpdateIsPoRaised(items.PrID, items.ItemId, 1);
        //                        biddingController.UpdateBiddingPORaised(items.PrID, items.ItemId, 1);
        //                        supplierQuotationController.UpdateIsRaisedPO(items.PrID, items.ItemId, items.SupplierId, 1);

        //                        ////////////////////////////////////////////////
        //                        po_DetailsController.UpdateIsRaisedPO(poid, items.ItemId, 0);
        //                        po_DetailsController.UpdatePOEditMode(poid, items.ItemId);
        //                        //////////////////////////////////////////////

        //                        hdnCustomizedAmount.Value = "";
        //                        hdnCustomizedNbt.Value = "";
        //                        hdnCustomizedVat.Value = "";
        //                        hdnCustomizedTotalAmount.Value = "";
        //                    }

        //                    List<PODetails> poDetails = po_DetailsController.GetSumOfAll(poid).ToList();

        //                    foreach (var itemUpdate in poDetails)
        //                    {
        //                        int Update = po_MasterController.updatePODetails(poid, itemUpdate.VatAmount, itemUpdate.NbtAmount, itemUpdate.TotalAmount, Math.Round(itemUpdate.CusTotalAmount, 2), Math.Round(itemUpdate.CusVatAmount, 2), Math.Round(itemUpdate.CusNbtAmount, 2));
        //                    }

        //                    _supplierQuotationDupList02 = _supplierQuotationDup.ToList();
        //                    _supplierQuotationDup.Clear();
        //                }
        //            }


        //            if (_supplierQuotationDupList02.ToList().Count != 0)
        //            {
        //                var differences = _supplierQuotation.Except(_supplierQuotationDupList02);
        //                List<SupplierQuotation> _supplierQuotationNew = new List<SupplierQuotation>();
        //                foreach (var item in differences)
        //                {
        //                    _supplierQuotationNew.Add(item);
        //                }

        //                if (_supplierQuotationNew.ToList().Count > 0)
        //                {
        //                    int poid = 0;
        //                    foreach (var item in _supplierQuotationNew)
        //                    {
        //                        poid = po_MasterController.SavePOMaster(CompanyId, item.PrID, item.SupplierId, DateTime.Now, UserId, item.VatAmount, item.NbtAmount, "", "", item.TotalAmount, 0, "", 0, DateTime.Now, int.Parse(PrId), 0, 0, 0, "");
        //                        int isCustomized = 0;
        //                        if (item.CustomizeAmount != 0 && item.VatAmount != 0 && item.NbtAmount != 0)
        //                        {
        //                            isCustomized = 1;
        //                            if (hdnCustomizedAmount.Value == "")
        //                            {
        //                                hdnCustomizedAmount.Value = item.CustomizeAmount.ToString();
        //                                decimal itemQuantity = item.ItemQuantity;
        //                                decimal totalAmountWithoutVatNbt = item.CustomizeAmount * item.ItemQuantity;
        //                                hdnCustomizedNbt.Value = ((totalAmountWithoutVatNbt * 2) / 98).ToString();
        //                                hdnCustomizedVat.Value = (((totalAmountWithoutVatNbt + decimal.Parse(hdnCustomizedNbt.Value)) * 15) / 100).ToString();
        //                                decimal totalAmountWithVatNbt = totalAmountWithoutVatNbt + decimal.Parse(hdnCustomizedNbt.Value) + decimal.Parse(hdnCustomizedVat.Value);
        //                                hdnCustomizedTotalAmount.Value = totalAmountWithVatNbt.ToString();
        //                            }
        //                        }
        //                        else
        //                        {
        //                            isCustomized = 0;
        //                            hdnCustomizedAmount.Value = "0";
        //                            hdnCustomizedNbt.Value = "0";
        //                            hdnCustomizedVat.Value = "0";
        //                            hdnCustomizedTotalAmount.Value = "0";
        //                        }

        //                        if (poid > 0)
        //                        {
        //                            int podetail = po_DetailsController.SavePoDetails(poid, item.QuotationNo, item.ItemId, item.PerItemPrice, item.ItemQuantity, item.TotalAmount, item.VatAmount, item.NbtAmount, Math.Round(decimal.Parse(hdnCustomizedAmount.Value), 2), Math.Round(decimal.Parse(hdnCustomizedVat.Value), 2), Math.Round(decimal.Parse(hdnCustomizedNbt.Value), 2), isCustomized, Math.Round(decimal.Parse(hdnCustomizedTotalAmount.Value), 2));
        //                            pr_DetailController.UpdateIsPoRaised(item.PrID, item.ItemId, 1);
        //                            biddingController.UpdateBiddingPORaised(item.PrID, item.ItemId, 1);
        //                            supplierQuotationController.UpdateIsRaisedPO(item.PrID, item.ItemId, item.SupplierId, 1);

        //                            ////////////////////////////////////////////////
        //                            int updatepod = po_DetailsController.UpdateIsRaisedPO(poid, item.ItemId, 0);
        //                            po_DetailsController.UpdatePOEditMode(poid, item.ItemId);
        //                            ////////////////////////////////////////////////

        //                        }
        //                        hdnCustomizedAmount.Value = "";
        //                        hdnCustomizedNbt.Value = "";
        //                        hdnCustomizedVat.Value = "";
        //                        hdnCustomizedTotalAmount.Value = "";
        //                    }


        //                    List<PODetails> poDetails = po_DetailsController.GetSumOfAll(poid).ToList();
        //                    foreach (var itemUpdate in poDetails)
        //                    {
        //                        int Update = po_MasterController.updatePODetails(poid, itemUpdate.VatAmount, itemUpdate.NbtAmount, itemUpdate.TotalAmount, Math.Round(itemUpdate.CusTotalAmount, 2), Math.Round(itemUpdate.CusVatAmount, 2), Math.Round(itemUpdate.CusNbtAmount, 2));
        //                    }
        //                }
        //            }
        //        }

        //        else
        //        {
        //            _supplierQuotation = supplierQuotationController.GetNecessaryDataForPO(int.Parse(PrId));
        //            foreach (var items in _supplierQuotation)
        //            {
        //                int poid = po_MasterController.SavePOMaster(CompanyId, items.PrID, items.SupplierId, DateTime.Now, UserId, items.VatAmount, items.NbtAmount, "", "", items.TotalAmount, 0, "", 0, DateTime.Now, int.Parse(PrId), 0, 0, 0, "");
        //                int isCustomized = 0;
        //                if (items.CustomizeAmount != 0 && items.VatAmount != 0 && items.NbtAmount != 0)
        //                {
        //                    isCustomized = 1;
        //                    if (hdnCustomizedAmount.Value == "")
        //                    {
        //                        hdnCustomizedAmount.Value = items.CustomizeAmount.ToString();
        //                        decimal itemQuantity = items.ItemQuantity;
        //                        decimal totalAmountWithoutVatNbt = items.CustomizeAmount * items.ItemQuantity;
        //                        hdnCustomizedNbt.Value = ((totalAmountWithoutVatNbt * 2) / 98).ToString();
        //                        hdnCustomizedVat.Value = (((totalAmountWithoutVatNbt + decimal.Parse(hdnCustomizedNbt.Value)) * 15) / 100).ToString();
        //                        decimal totalAmountWithVatNbt = totalAmountWithoutVatNbt + decimal.Parse(hdnCustomizedNbt.Value) + decimal.Parse(hdnCustomizedVat.Value);
        //                        hdnCustomizedTotalAmount.Value = totalAmountWithVatNbt.ToString();
        //                    }
        //                }
        //                else
        //                {
        //                    isCustomized = 0;
        //                    hdnCustomizedAmount.Value = "0";
        //                    hdnCustomizedNbt.Value = "0";
        //                    hdnCustomizedVat.Value = "0";
        //                    hdnCustomizedTotalAmount.Value = "0";
        //                }

        //                if (poid > 0)
        //                {
        //                    int podetail = po_DetailsController.SavePoDetails(poid, items.QuotationNo, items.ItemId, items.PerItemPrice, items.ItemQuantity, items.TotalAmount, items.VatAmount, items.NbtAmount, Math.Round(decimal.Parse(hdnCustomizedAmount.Value), 2), Math.Round(decimal.Parse(hdnCustomizedVat.Value), 2), Math.Round(decimal.Parse(hdnCustomizedNbt.Value), 2), isCustomized, Math.Round(decimal.Parse(hdnCustomizedTotalAmount.Value), 2));
        //                    pr_DetailController.UpdateIsPoRaised(items.PrID, items.ItemId, 1);
        //                    biddingController.UpdateBiddingPORaised(items.PrID, items.ItemId, 1);
        //                    supplierQuotationController.UpdateIsRaisedPO(items.PrID, items.ItemId, items.SupplierId, 1);

        //                    ////////////////////////////////////////////////
        //                    po_DetailsController.UpdateIsRaisedPO(poid, items.ItemId, 0);
        //                    po_DetailsController.UpdatePOEditMode(poid, items.ItemId);
        //                    ////////////////////////////////////////////////
        //                }

        //                hdnCustomizedAmount.Value = "";
        //                hdnCustomizedNbt.Value = "";
        //                hdnCustomizedVat.Value = "";
        //                hdnCustomizedTotalAmount.Value = "";

        //                List<PODetails> poDetails = po_DetailsController.GetSumOfAll(poid).ToList();
        //                foreach (var itemUpdate in poDetails)
        //                {
        //                    int Update = po_MasterController.updatePODetails(poid, itemUpdate.VatAmount, itemUpdate.NbtAmount, itemUpdate.TotalAmount, Math.Round(itemUpdate.CusTotalAmount, 2), Math.Round(itemUpdate.CusVatAmount, 2), Math.Round(itemUpdate.CusNbtAmount, 2));
        //                }
        //            }
        //        }
        //        GvBind();
        //        GvSubmittedBids();
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title:'SUCCESS',text: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CompanyBidClosed.aspx'}); });   </script>", false);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //protected void btnCancel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("AdminDashboard.aspx");
        //}

        [WebMethod]
        [ScriptMethod]
        public static void GetSupplierId(SupplierQuotation supplierQuotation)
        {
            try
            {
                int supplierId = supplierQuotation.SupplierId;
                int prid = supplierQuotation.PrID;
                int itemid = supplierQuotation.ItemId;
                string BidOpeningId = supplierQuotation.BidOpeningId;
                SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
                decimal supplierCustomizeAmount = 0;
                if (supplierQuotation.CustomizeAmount == 0)
                {
                    supplierCustomizeAmount = 0;
                }
                else
                {
                    supplierCustomizeAmount = supplierQuotation.CustomizeAmount;
                }
                int rejectedcount = 0;
                int selectedCount = 0;
                List<SupplierQuotation> _SupplierQuotation = supplierQuotationController.GetPendingCountOfSupplier(supplierQuotation.PrID, supplierQuotation.ItemId, supplierQuotation.SupplierId);
                foreach (var item in _SupplierQuotation)
                {
                    rejectedcount = item.SupplierRejectedCount;
                    selectedCount = item.SupplierSelectedCount;
                    break;
                }
                if (selectedCount == 0)
                {
                    selectedCount = 1;
                }
                if (selectedCount != 0)
                {
                    selectedCount = selectedCount + 1;
                }

                int updateApproveSupplier = supplierQuotationController.UpdateNegotiateAmount(prid, itemid, supplierId, supplierCustomizeAmount,selectedCount,rejectedcount, supplierQuotation.Vat, supplierQuotation.Nbt, supplierQuotation.TotalPrice);
                //bidding update
                if (updateApproveSupplier == 1)
                {
                
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnCancelSupplieModal_Click(object sender, EventArgs e)
        {
        }


        //protected void lblRjtPO_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        //        int itemId = int.Parse(gvSubmittedPO.Rows[x].Cells[0].Text);
        //        int prId = int.Parse(gvSubmittedPO.Rows[x].Cells[1].Text);

        //        List<SupplierQuotation> _SupplierQuotation = new List<SupplierQuotation>();

        //        _SupplierQuotation = supplierQuotationController.GetSuppliersList(prId, itemId);

        //        foreach (var item in _SupplierQuotation)
        //        {
        //            supplierQuotationController.UpdateIsRaisedPOReject(prId, itemId, item.SupplierId,2,"",1);
        //        }

        //        biddingController.UpdateBiddingPORaised(prId, itemId, 2);
        //        pr_DetailController.UpdateIsPoRaised(prId, itemId,0);


        //        bind(prId, itemId);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public class DuplicateList
        {
           public int PrId { get; set; }
           public int supplierId { get; set; }
        }


        protected void btnSelect_Onclick(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btnRejection_OnClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}