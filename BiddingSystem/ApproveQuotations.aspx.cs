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

namespace BiddingSystem
{
    public partial class ApproveQuotations : System.Web.UI.Page
    {
        #region properties
        static int PrId = 0;
        static int BidId = 0;
        static int UserId = 0;
        static int CompanyId = 0;
        static int viewedQuotation = 0;
        static PR_Master PrMaster;
        public static int categoryId;
        public string datetimepattern = System.Configuration.ConfigurationSettings.AppSettings["datePattern"];
        #endregion

        #region controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForQuotationApproval.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "quotationApprovalLink";


                UserId = int.Parse(Session["UserId"].ToString());
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                hndDesignationId.Value = companyLogin.DesignationId.ToString();
                hndUserId.Value = companyLogin.UserId.ToString();
                if (!companyUserAccessController.isAvilableAccess(UserId, CompanyId, 6, 13) && companyLogin.Usertype != "S")
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
                if (UserId != 0)
                {
                    try
                    {
                        PrId = int.Parse(Request.QueryString.Get("PrId"));
                        PrMaster = pr_MasterController.GetPrForQuotationApproval(PrId, CompanyId);

                        lblPRNo.Text = PrMaster.PrCode;
                        lblCreatedOn.Text = PrMaster.CreatedDateTime.ToString("dd-MM-yyyy");
                        lblCreatedBy.Text = PrMaster.CreatedByName;
                        lblRequestBy.Text = PrMaster.RequestedBy;
                        lblRequestFor.Text = PrMaster.QuotationFor;
                        lblExpenseType.Text = (PrMaster.expenseType == "1") ? "Capital Expense" : "Operational Expense";

                        LoadGV();
                        //  categoryId

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void LoadGV()
        {
            try
            {
                ((BoundField)gvBids.Columns[5]).DataFormatString = datetimepattern;
                ((BoundField)gvBids.Columns[6]).DataFormatString = datetimepattern;
                ((BoundField)gvBids.Columns[7]).DataFormatString = datetimepattern;
                PrMaster.Bids.ForEach(b => { b.NoOfQuotations = b.SupplierQuotations.Count; b.NoOfRejectedQuotations = b.SupplierQuotations.Count(sq => sq.IsSelected == 2); });
                gvBids.DataSource = PrMaster.Bids;
                gvBids.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());
               
                gvBidItems.DataSource = PrMaster.Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
                //  categoryId = 
            }
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            BidId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[1].Text);
            LoadQuotations();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlQuotations').modal('show'); });   </script>", false);

        }

        private void LoadQuotations()
        {
            try
            {
                viewedQuotation = 1;
                List<SupplierQuotation> quotations;

                DataTable dt = new DataTable();

                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BidId");
                dt.Columns.Add("SupplierId");
                dt.Columns.Add("SupplierName");
                dt.Columns.Add("SubTotal");
                dt.Columns.Add("NbtAmount");
                dt.Columns.Add("VatAmount");
                dt.Columns.Add("NetTotal");
                dt.Columns.Add("TermsAndCondition");
                dt.Columns.Add("Actions");
                dt.Columns.Add("IsSelected");
                dt.Columns.Add("SelectionRemarks");
                dt.Columns.Add("IsUploadeded");
                dt.Columns.Add("RecomondationType");

                quotations = PrMaster.Bids.Find(b => b.BidId == BidId).SupplierQuotations.OrderBy(q => q.NetTotal).ToList();


                for (int i = 0; i < quotations.Count; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow["QuotationId"] = quotations[i].QuotationId.ToString();
                    newRow["BidId"] = quotations[i].BidId.ToString();
                    newRow["SupplierId"] = quotations[i].SupplierId.ToString();
                    newRow["SupplierName"] = quotations[i].SupplierName;
                    newRow["SubTotal"] = quotations[i].SubTotal.ToString();
                    newRow["NbtAmount"] = quotations[i].NbtAmount.ToString();
                    newRow["VatAmount"] = quotations[i].VatAmount.ToString();
                    newRow["NetTotal"] = quotations[i].NetTotal.ToString();
                    newRow["TermsAndCondition"] = quotations[i].TermsAndCondition;
                    newRow["IsSelected"] = quotations[i].IsSelected;
                    newRow["SelectionRemarks"] = quotations[i].SelectionRemarks;
                    newRow["IsUploadeded"] = quotations[i].IsUploadeded;
                    newRow["RecomondationType"] = quotations[i].RecomondationType;

                    if (quotations[i].IsSelected == 1)
                    {
                        newRow["Actions"] = "1";
                        hdnSubTotal.Value = quotations[i].SubTotal.ToString();
                        hdnNbtTotal.Value = quotations[i].NbtAmount.ToString();
                        hdnVatTotal.Value = quotations[i].VatAmount.ToString();
                        hdnNetTotal.Value = quotations[i].NetTotal.ToString();
                    }
                    else
                    {
                        newRow["Actions"] = "0";
                    }

                    dt.Rows.Add(newRow);
                }

                gvQuotations.DataSource = dt;
                gvQuotations.DataBind();
                hdnApprovalRemarks.Value = "";
                hdnRejectRemarks.Value = "";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvQuotations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int QuotationId = int.Parse(gvQuotations.DataKeys[e.Row.RowIndex].Value.ToString());
                int BidId = int.Parse(e.Row.Cells[2].Text);
                GridView gvQuotationItems = e.Row.FindControl("gvQuotationItems") as GridView;

                DataTable dt = new DataTable();

                dt.Columns.Add("QuotationItemId");
                dt.Columns.Add("QuotationId");
                dt.Columns.Add("BiddingItemId");
                dt.Columns.Add("BidId");
                dt.Columns.Add("CategoryId");
                dt.Columns.Add("CategoryName");
                dt.Columns.Add("SubCategoryId");
                dt.Columns.Add("SubCategoryName");
                dt.Columns.Add("ItemId");
                dt.Columns.Add("ItemName");
                dt.Columns.Add("Qty");
                dt.Columns.Add("EstimatedPrice");
                dt.Columns.Add("UnitPrice");
                dt.Columns.Add("SubTotal");
                dt.Columns.Add("HasNbt");
                dt.Columns.Add("HasVat");
                dt.Columns.Add("NbtCalculationType");
                dt.Columns.Add("NbtAmount");
                dt.Columns.Add("VatAmount");
                dt.Columns.Add("NetTotal");
                dt.Columns.Add("EnableFields");


                SupplierQuotation quotation = PrMaster.Bids.Find(b => b.BidId == BidId).SupplierQuotations.Find(sq => sq.QuotationId == QuotationId);

                for (int i = 0; i < quotation.QuotationItems.Count; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow["QuotationItemId"] = quotation.QuotationItems[i].QuotationItemId.ToString();
                    newRow["QuotationId"] = quotation.QuotationItems[i].QuotationId.ToString();
                    newRow["BiddingItemId"] = quotation.QuotationItems[i].BiddingItemId.ToString();
                    newRow["BidId"] = quotation.BidId.ToString();
                    newRow["CategoryId"] = quotation.QuotationItems[i].CategoryId.ToString();
                    newRow["CategoryName"] = quotation.QuotationItems[i].CategoryName;
                    newRow["SubCategoryId"] = quotation.QuotationItems[i].SubCategoryId.ToString();
                    newRow["SubCategoryName"] = quotation.QuotationItems[i].SubCategoryName;
                    newRow["ItemId"] = quotation.QuotationItems[i].ItemId.ToString();
                    newRow["ItemName"] = quotation.QuotationItems[i].ItemName;
                    newRow["Qty"] = quotation.QuotationItems[i].Qty.ToString();
                    newRow["EstimatedPrice"] = quotation.QuotationItems[i].EstimatedPrice;
                    newRow["UnitPrice"] = quotation.QuotationItems[i].UnitPrice.ToString();
                    newRow["SubTotal"] = quotation.QuotationItems[i].SubTotal.ToString();
                    newRow["HasNbt"] = quotation.QuotationItems[i].HasNbt.ToString();
                    newRow["HasVat"] = quotation.QuotationItems[i].HasVat.ToString();
                    newRow["NbtCalculationType"] = quotation.QuotationItems[i].NbtCalculationType.ToString();
                    newRow["NbtAmount"] = quotation.QuotationItems[i].NbtAmount.ToString();
                    newRow["VatAmount"] = quotation.QuotationItems[i].VatAmount.ToString();
                    newRow["NetTotal"] = quotation.QuotationItems[i].TotalAmount.ToString();

                    if (quotation.IsSelected == 1)
                        newRow["EnableFields"] = "1";
                    else
                        newRow["EnableFields"] = "0";

                    dt.Rows.Add(newRow);
                }

                gvQuotationItems.DataSource = dt;
                gvQuotationItems.DataBind();


            }
        }

        protected void gvQuotationItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView gvSpecs = e.Row.FindControl("gvSpecs") as GridView;
                GridView gvQuotationItems = (gvSpecs.NamingContainer as GridViewRow).NamingContainer as GridView;

                int quotationId = int.Parse(gvQuotations.DataKeys[((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).RowIndex].Value.ToString());
                int bidId = int.Parse(((e.Row.NamingContainer as GridView).NamingContainer as GridViewRow).Cells[2].Text);
                int quotationItemId = int.Parse(gvQuotationItems.DataKeys[e.Row.RowIndex].Value.ToString());

                DataTable dt = new DataTable();

                dt.Columns.Add("Material");
                dt.Columns.Add("Description");
                dt.Columns.Add("Comply");

                List<SupplierBOM> boms = PrMaster.Bids.Find(b => b.BidId == bidId).SupplierQuotations.Find(sq => sq.QuotationId == quotationId).QuotationItems.Find(qi => qi.QuotationItemId == quotationItemId).SupplierBOMs;

                for (int i = 0; i < boms.Count; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow["Material"] = boms[i].Material;
                    newRow["Description"] = boms[i].Description;
                    newRow["Comply"] = boms[i].Comply.ToString();

                    dt.Rows.Add(newRow);
                }

                gvSpecs.DataSource = dt;
                gvSpecs.DataBind();
            }
        }

        [WebMethod]
        public static List<SupplierQuotation> GetQuotations(string BidId)
        {
            return PrMaster.Bids.Find(b => b.BidId == int.Parse(BidId)).SupplierQuotations;
        }

        protected void btnViewAttachments_Click(object sender, EventArgs e)
        {
            gvDocs.DataSource = PrMaster.Bids.Find(b => b.BidId == BidId).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).UploadedFiles;
            gvDocs.DataBind();

            gvImages.DataSource = PrMaster.Bids.Find(b => b.BidId == BidId).SupplierQuotations.Find(q => q.QuotationId == int.Parse(hdnQuotationId.Value)).QuotationImages;
            gvImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlAttachments').modal('show') });   </script>", false);
        }


        protected void btnReject_Click(object sender, EventArgs e)
        {
            viewedQuotation = 0;
            int result = biddingController.ApproveOrRejectSelectedQuotation(int.Parse(hdnBidId.Value), 2, hdnRejectRemarks.Value, UserId);

            if (result > 0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationApproval.aspx' });; });   </script>", false);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on rejecting quotation'}); });   </script>", false);
            }
        }


        protected void btnApprove_Click(object sender, EventArgs e)
        {

            SupplierQuotation quotation = PrMaster.Bids.Find(b => b.BidId == int.Parse(hdnBidId.Value)).SupplierQuotations.Find(q => q.IsSelected == 1);

            if ((quotation.RecomondationType==2 && quotation.IsUploadeded==1)|| quotation.RecomondationType != 2)
            {



                if ((decimal.Parse(hdnSubTotal.Value) != quotation.SubTotal) && viewedQuotation == 1)
                {
                    viewedQuotation = 0;

                    GridViewRow quotationRow = null;

                    for (int i = 0; i < gvQuotations.Rows.Count; i++)
                    {
                        if (int.Parse(gvQuotations.Rows[i].Cells[1].Text) == quotation.QuotationId)
                        {
                            quotationRow = gvQuotations.Rows[i];
                            break;
                        }
                    }

                    quotation.SubTotal = decimal.Parse(hdnSubTotal.Value);
                    quotation.NbtAmount = decimal.Parse(hdnNbtTotal.Value);
                    quotation.VatAmount = decimal.Parse(hdnVatTotal.Value);
                    quotation.NetTotal = decimal.Parse(hdnNetTotal.Value);

                    GridView gvQuotationItems = quotationRow.FindControl("gvQuotationItems") as GridView;

                    for (int i = 0; i < gvQuotationItems.Rows.Count; i++)
                    {
                        SupplierQuotationItem quotationItem = quotation.QuotationItems.Find(qi => qi.QuotationItemId == int.Parse(gvQuotationItems.Rows[i].Cells[0].Text));
                        quotationItem.Qty = int.Parse((gvQuotationItems.Rows[i].FindControl("txtQty") as TextBox).Text);
                        quotationItem.UnitPrice = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtNegotiatePrice") as TextBox).Text);
                        quotationItem.SubTotal = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtSubTotal") as TextBox).Text);
                        quotationItem.NbtAmount = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtNbt") as TextBox).Text);
                        quotationItem.VatAmount = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtVat") as TextBox).Text);
                        quotationItem.TotalAmount = decimal.Parse((gvQuotationItems.Rows[i].FindControl("txtNetTotal") as TextBox).Text);
                    }

                    int result = quotationController.UpdateSupplierQuotation(quotation);

                    if (result > 0)
                    {
                        result = biddingController.ApproveOrRejectSelectedQuotation(int.Parse(hdnBidId.Value), 1, hdnApprovalRemarks.Value, UserId);

                        if (result > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationApproval.aspx' });; });   </script>", false);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving quotation'}); });   </script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on updating quotation'}); });   </script>", false);
                    }


                }
                else
                {
                    int result = biddingController.ApproveOrRejectSelectedQuotation(int.Parse(hdnBidId.Value), 1, hdnApprovalRemarks.Value, UserId);

                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForQuotationApproval.aspx' });; });   </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error on approving quotation'}); });   </script>", false);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Please Uplaod Tech Committee Documents to Proceed'}); });   </script>", false);
            }
        }

        [WebMethod]
        public static object GetApprovalAuthorities(int BidId)
        {
            var Data = (object)null;
            int categoryId = PrMaster.Bids.Find(b => b.BidId == BidId).BiddingItems.First().CategoryId;
            DesignationController designationController = ControllerFactory.CreateDesignationController();
            List<Designation> listDesignation = designationController.GetDesignationList();
            ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
            List<ItemCategoryBidApproval> listItemCategoryBidApproval = itemCategoryApprovalController.GetItemCategoryBidApproval(BidId, categoryId, PrId);
            CompanyLoginController companyLoginController1 = ControllerFactory.CreateCompanyLoginController();
            CompanyLogin companyLogin1 = null;
            if (listItemCategoryBidApproval.Count == 1 && listItemCategoryBidApproval[0].DesignationId == 0) {
                companyLogin1 = companyLoginController1.GetUserbyuserId(listItemCategoryBidApproval[0].UserId);
            }
            if (listItemCategoryBidApproval != null)
            {
                Data = new { ListAssignAuthorities = listItemCategoryBidApproval, ListDesignation = listDesignation , ListUser = companyLogin1 };
            }
            return Data;
        }

        [WebMethod]
        public static object ApprovalBidItem(int BidId , int CategoryId , int DesignationId)
        {
            var Data = (object)null;
            ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
            int update = itemCategoryApprovalController.UpdateItemCategoryBidApproval(BidId, PrId, CategoryId, DesignationId);
            if(update == 0)
            {
                Data = new { Message = "AllApproved" };
            }
            return Data;
        }

        protected void btnsupplerview_Click(object sender, EventArgs e)
        {
            var SupplierId = int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[3].Text);
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('CompanyUpdatingAndRatingSupplier.aspx?ID=" + SupplierId+ "'); $('.modal-backdrop').remove(); $('#mdlQuotations').modal('show'); });   </script>", false);
            
            
        }
    }
}