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
using System.Globalization;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class CreatePONewImports : System.Web.UI.Page
    {
        #region properties
      //  static int PrId = 0;
       // static int BidId = 0;
      //  static int UserId = 1005;
       // static int CompanyId = 4;
       // static PrMasterV2 PrMaster;
        #endregion

        #region controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        POMasterController poMasterController = ControllerFactory.CreatePOMasterController();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
        QuotationImageController quotationImageController = ControllerFactory.CreateQuotationImageController();
        SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        TabulationDetailController tabulationDetailController = ControllerFactory.CreateTabulationDetailController();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForPoCreation.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createPoLink";

              //  UserId = int.Parse(Session["UserId"].ToString());
             //   CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 15) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                        Session["PrId"] = int.Parse(Request.QueryString.Get("PrId"));
                       var PrMaster = pr_MasterController.GetPrForPoCreation(int.Parse(Session["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
                        ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                        ViewState["PurchaseType"] = PrMaster.PurchaseType;
                        ViewState["ImportItemType"] = PrMaster.ImportItemType;

                        lblPRNo.Text = "PR-" + PrMaster.PrCode;
                        lblCreatedOn.Text = PrMaster.CreatedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                        lblCreatedBy.Text = PrMaster.CreatedByName;
                        lblRequestBy.Text = PrMaster.CreatedByName;
                        lblRequestFor.Text = PrMaster.RequiredFor;
                        lblExpenseType.Text = (PrMaster.ExpenseType == 1) ? "Capital Expense" : "Operational Expense";
                        lblDepartment.Text = !String.IsNullOrEmpty(PrMaster.SubDepartmentName) ? PrMaster.SubDepartmentName : "Not Found";
                        lblWarehouse.Text = PrMaster.WarehouseName;
                        lblMrnId.Text = PrMaster.MrnId != 0 ? "MRN-" + PrMaster.MrnCode : "Not From MRN";
                        lblPurchaseType.Text = PrMaster.PurchaseType ==1 ? "Local" : "Import";
                        ViewState["WarehouseId"] = PrMaster.WarehouseId;

                        LoadGV();
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
                List<TabulationDetail> items = new List<TabulationDetail>();

                for (int i = 0; i < new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Count; i++)
                {
                    items.AddRange(new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids[i].SelectedTabulation.TabulationDetails);
                    
                }

                if (ViewState["PurchaseType"].ToString() == "1") {
                    gvItems.Columns[16].Visible = false;
                    gvItems.Columns[17].Visible = false;
                    gvItems.Columns[18].Visible = false;
                    gvItems.Columns[30].Visible = false;
                    gvItems.Columns[31].Visible = false;
                }

                if (ViewState["PurchaseType"].ToString() == "2") {
                    if (ViewState["ImportItemType"].ToString() == "2") {
                        gvItems.Columns[31].Visible = false;
                    }
                }

                for (int i = 0; i < items.Count; i++) {
                    items[i].Number = i+1;

                }

                gvItems.DataSource = items;
                gvItems.DataBind();
               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        

        protected void btnCreatePO_Click(object sender, EventArgs e)
        {
            List<POMaster> POMasters = new List<POMaster>();
            int supplierId = 0;

            for (int i = 0; i < gvItems.Rows.Count; i++)
            {
                if (((gvItems.Rows[i].FindControl("CheckBox1") as CheckBox).Checked))
                {
                    if (int.Parse(gvItems.Rows[i].Cells[4].Text) != supplierId)
                    {
                        supplierId = int.Parse(gvItems.Rows[i].Cells[4].Text);

                        POMaster PoMaster = new POMaster();
                        PoMaster.DepartmentId = int.Parse(Session["CompanyId"].ToString());
                        PoMaster.BasePr = int.Parse(Session["PrId"].ToString());
                        PoMaster.SupplierId = int.Parse(gvItems.Rows[i].Cells[4].Text);
                        PoMaster.DeliverToWarehouse = int.Parse(ViewState["WarehouseId"].ToString());
                        PoMaster.CreatedBy = Session["UserId"].ToString();
                        PoMaster.VatAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[21].Text), 4);
                        PoMaster.NBTAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[20].Text), 4);
                        PoMaster.TotalAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[22].Text), 4);
                        PoMaster.QuotationId = int.Parse(gvItems.Rows[i].Cells[6].Text);
                        PoMaster.Remarks = txtRemarks.Text == "" ? "" : txtRemarks.Text;


                        List<PODetails> PoDetails = new List<PODetails>();

                        PODetails PoDetail = new PODetails();

                        PoDetail.QuotationItemId = int.Parse(gvItems.Rows[i].Cells[7].Text);
                        PoDetail.ItemId = int.Parse(gvItems.Rows[i].Cells[8].Text);
                        PoDetail.ItemPrice = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[19].Text), 4);
                        PoDetail.Quantity = decimal.Parse(gvItems.Rows[i].Cells[15].Text);
                        PoDetail.VatAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[22].Text), 4);
                        PoDetail.NbtAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[21].Text), 4);
                        PoDetail.TotalAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[23].Text), 4);
                        PoDetail.TabulationId= int.Parse(gvItems.Rows[i].Cells[3].Text);
                        PoDetail.TabulationDetailId = int.Parse(gvItems.Rows[i].Cells[2].Text);
                        PoDetail.HasNbt = int.Parse(gvItems.Rows[i].Cells[25].Text);
                        PoDetail.NbtCalculationType = int.Parse(gvItems.Rows[i].Cells[26].Text);
                        PoDetail.HasVat = int.Parse(gvItems.Rows[i].Cells[24].Text);
                        PoDetail.MeasurementId = int.Parse(gvItems.Rows[i].Cells[27].Text);
                        PoDetail.SupplierMentionedItemName = gvItems.Rows[i].Cells[11].Text;
                        PoDetail.PoPurchaseType = (gvItems.Rows[i].FindControl("rdbLcl") as RadioButton).Checked == true ? 1 : 2;

                        PoDetails.Add(PoDetail);

                        PoMaster.PoDetails = PoDetails;

                        POMasters.Add(PoMaster);
                    }
                    else
                    {
                        POMaster PoMaster = POMasters.Find(po => po.SupplierId == int.Parse(gvItems.Rows[i].Cells[4].Text));

                        PoMaster.VatAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[22].Text), 4);
                        PoMaster.NBTAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[21].Text), 4);
                        PoMaster.TotalAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[23].Text), 4);
                        PoMaster.QuotationId = int.Parse(gvItems.Rows[i].Cells[6].Text);

                        PODetails PoDetail = new PODetails();

                        PoDetail.QuotationItemId = int.Parse(gvItems.Rows[i].Cells[7].Text);
                        PoDetail.ItemId = int.Parse(gvItems.Rows[i].Cells[8].Text);
                        PoDetail.ItemPrice = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[19].Text), 4);
                        PoDetail.Quantity = decimal.Parse(gvItems.Rows[i].Cells[15].Text);
                        PoDetail.VatAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[22].Text), 4);
                        PoDetail.NbtAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[21].Text), 4);
                        PoDetail.TotalAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[23].Text), 4);
                        PoDetail.TabulationId = int.Parse(gvItems.Rows[i].Cells[3].Text);
                        PoDetail.TabulationDetailId = int.Parse(gvItems.Rows[i].Cells[2].Text);
                        PoDetail.HasNbt = int.Parse(gvItems.Rows[i].Cells[25].Text);
                        PoDetail.NbtCalculationType = int.Parse(gvItems.Rows[i].Cells[26].Text);
                        PoDetail.HasVat = int.Parse(gvItems.Rows[i].Cells[24].Text);
                        PoDetail.MeasurementId = int.Parse(gvItems.Rows[i].Cells[27].Text);
                        PoDetail.SupplierMentionedItemName = gvItems.Rows[i].Cells[11].Text;
                        PoDetail.PoPurchaseType = (gvItems.Rows[i].FindControl("rdbLcl") as RadioButton).Checked == true ? 1 : 2;

                        PoMaster.PoDetails.Add(PoDetail);

                    }

                }
            }

            if (POMasters.Count > 0)
            {
               // int result = poMasterController.SavePONew(POMasters, int.Parse(Session["UserId"].ToString()));
                List<int> result = poMasterController.SavePONew(POMasters, int.Parse(Session["UserId"].ToString()));

                if (result.Count > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForPoCreation.aspx' }); });   </script>", false);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Creating PO', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Please Select a Quotation', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }

        }
        protected void lbtnLog_Click(object sender, EventArgs e) {
            try {
                int prid = 0;
                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvItems.Rows[x].Cells[27].Text);
                gvStatusLog.DataSource = ControllerFactory.CreatePRDetailsStatusLogController().PrLogDetails(prid);
                gvStatusLog.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlLog').modal('show'); });   </script>", false);

            }
            catch (Exception ex) {
                throw ex;
            }

        }
        protected void btnViewAttachments_Click(object sender, EventArgs e)
        {
         
            int tabulationId= int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[3].Text);
            int qutationId= int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[6].Text);
            var qutaion= supplierQuotationController.GetSupplierQuotations(new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Where(x=>x.SelectedTabulation.TabulationId== tabulationId).FirstOrDefault().BidId);

            gvDocs.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(qutationId);
            gvDocs.DataBind();

            gvImages.DataSource = quotationImageController.GetQuotationImages(qutationId);
            gvImages.DataBind();
            txtTermsAndConditions.Text = qutaion.Where(x=>x.QuotationId == qutationId).FirstOrDefault().TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlAttachments').modal('show') });   </script>", false);
        }

        protected void btnTerminate_Click(object sender, EventArgs e) {
            List<int> TabulationDetailIds = new List<int>();
            
           // List<int> ItemIds = new List<int>();

            for (int i = 0; i < gvItems.Rows.Count; i++) {
                if (((gvItems.Rows[i].FindControl("CheckBox1") as CheckBox).Checked)) {
                    TabulationDetailIds.Add(int.Parse(gvItems.Rows[i].Cells[2].Text));
                  //  ItemIds.Add(int.Parse(gvItems.Rows[i].Cells[7].Text));

                }
            }

            if (TabulationDetailIds.Count > 0) {
                int result = tabulationDetailController.TerminateItems(TabulationDetailIds, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value.ProcessString());

                if (result > 0) {

                    var PrMaster = pr_MasterController.GetPrForPoCreation(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
                    if (PrMaster.Bids.Count > 0) {

                        ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);
                        LoadGV();
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                    else {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForPoCreation.aspx' }); });   </script>", false);

                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Terminating Item', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Please Select a Quotation', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
        }

    }
}