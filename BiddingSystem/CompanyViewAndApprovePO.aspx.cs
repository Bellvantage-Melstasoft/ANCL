using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;

namespace BiddingSystem
{
    public partial class CompanyViewAndApprovePO : System.Web.UI.Page
    {
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        QuotationImageController quotationImageController = ControllerFactory.CreateQuotationImageController();
        SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();
      

       // static string UserId = string.Empty;
       // private string PRId = string.Empty;

       // private string UserDept = string.Empty;
      //  private string OurRef = string.Empty;
       // private string PrCode = string.Empty;
       // private string RequestedDate = string.Empty;
      //  private string UserRef = string.Empty;
       // private string RequesterName = string.Empty;
       // private int basePr = 0;
        //int CompanyId = 0;
       // int PoId = 0;
      //  static int quationid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                //((BiddingAdmin)Page.Master).subTabValue = "CustomerApprovePO.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ApprovePOLink";

               // CompanyId = int.Parse(Session["CompanyId"].ToString());
               // UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }

                if (Session["PoId"] != null) {
                    ViewState["PoId"] = int.Parse(Session["PoId"].ToString());
                }
                else
                {
                    Response.Redirect("CusromerPOView.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            lblDateNow.Text = LocalTime.Today.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
            if (!IsPostBack)
            {
                try
                {
                    POMaster pOMaster = pOMasterController.GetPoMasterObjByPoIdRaised(int.Parse(ViewState["PoId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                   ViewState["quationid"] = pOMaster.QuotationId;
                    ViewState["basePr"] = pOMaster.BasePr;
                    Session["BasePRID"] = pOMaster.BasePr;
                    lblBasedPr.Text = pr_MasterController.FetchApprovePRDataByPRId(int.Parse(ViewState["basePr"].ToString())).PrCode;
                    lblPOCode.Text = pOMaster.POCode;
                    lblSupplierName.Text = pOMaster._Supplier.SupplierName; ;
                    lblAddress.Text = pOMaster._Supplier.Address1 + "," + pOMaster._Supplier.Address2;
                    lblstorekeeper.Text = pOMaster.StoreKeeperName;
                    foreach (PODetails item in pOMaster._PODetails)
                    {
                        item.supplierQuotationItem = supplierQuotationController.GetQuotationItemsByQuotationItemId(item.QuotationItemId);
                    }                    
                    gvPurchaseOrderItems.DataSource = pOMaster._PODetails;
                    gvPurchaseOrderItems.DataBind();
                    //
                    lblCompanyName.Text = pOMaster._companyDepartment.DepartmentName;
                    lblVatNo.Text = pOMaster._companyDepartment.VatNo;
                    lblPhoneNo.Text = pOMaster._companyDepartment.PhoneNO;
                    lblFaxNo.Text = pOMaster._companyDepartment.FaxNO;
                   // lblRefNo.Text = pr_MasterController.FetchApprovePRDataByPRId(basePr).Ref01;
                  
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //protected void btnView_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        //        int itemId = int.Parse(gvPurchaseOrderItems.Rows[x].Cells[0].Text);
        //        List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
        //        gvUploadFiles.DataSource = pr_FileUpload;
        //        gvUploadFiles.DataBind();

        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

        //-----------2 : Rejected 1: Approved
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int result = pOMasterController.ApprovePOMaster(int.Parse(ViewState["PoId"].ToString()), int.Parse(Session["UserId"].ToString()));
                if (result > 0)
                {
                    result = pODetailsController.ApprovePoDetails(int.Parse(ViewState["PoId"].ToString()));
                    if (result > 0)
                    {
                        result = pOMasterController.updatePaymentMethodByPoId(int.Parse(ViewState["PoId"].ToString()), int.Parse(Session["CompanyId"].ToString()), ddlPaymentMethod.SelectedValue == "card" ? "V" : "C");
                        if (result > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title:'SUCCESS',text: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerApprovePO.aspx'}); });   </script>", false);
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Updating Payment Method', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Approving PO Details', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Approving PO Master', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                int result = pOMasterController.rejectPOMaster(int.Parse(ViewState["PoId"].ToString()));
                if (result > 0)
                {
                    result = pODetailsController.RejectPoDetails(int.Parse(ViewState["PoId"].ToString()));
                    if (result > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title:'SUCCESS',text: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerApprovePO.aspx'}); });   </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Rejecting PO Details', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Rejecting PO Master', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class POSubmitted
        {
            public POSubmitted(int ItemId, int PrId, int PoId, decimal UnitPrice, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, decimal CustomizedUnitPrice, decimal CustomizedVatAmount, decimal CustomizedNbtAmount, decimal CustomizedTotalAmount)
            {
                prId = PrId;
                itemId = ItemId;
                poId = PoId;
                unitPrice = UnitPrice;
                vatAmount = VatAmount;
                nbtAmount = NbtAmount;
                totalAmount = TotalAmount;
                customizedUnitPrice = CustomizedUnitPrice;
                customizedVatAmount = CustomizedVatAmount;
                customizedNbtAmount = CustomizedNbtAmount;
                customizedTotalAmount = CustomizedTotalAmount;
            }

            private int itemId;
            private int prId;
            private int poId;
            private decimal unitPrice;
            private decimal vatAmount;
            private decimal nbtAmount;
            private decimal totalAmount;
            private decimal customizedUnitPrice;
            private decimal customizedVatAmount;
            private decimal customizedNbtAmount;
            private decimal customizedTotalAmount;

            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            public int PrId
            {
                get { return prId; }
                set { prId = value; }
            }

            public int PoId
            {
                get { return poId; }
                set { poId = value; }
            }

            public decimal UnitPrice
            {
                get { return unitPrice; }
                set { unitPrice = value; }
            }

            public decimal VatAmount
            {
                get { return vatAmount; }
                set { vatAmount = value; }
            }

            public decimal NbtAmount
            {
                get { return nbtAmount; }
                set { nbtAmount = value; }
            }

            public decimal TotalAmount
            {
                get { return totalAmount; }
                set { totalAmount = value; }
            }

            public decimal CustomizedUnitPrice
            {
                get { return customizedUnitPrice; }
                set { customizedUnitPrice = value; }
            }

            public decimal CustomizedVatAmount
            {
                get { return customizedVatAmount; }
                set { customizedVatAmount = value; }
            }

            public decimal CustomizedNbtAmount
            {
                get { return customizedNbtAmount; }
                set { customizedNbtAmount = value; }
            }

            public decimal CustomizedTotalAmount
            {
                get { return customizedTotalAmount; }
                set { customizedTotalAmount = value; }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void btnViewAttachments_Click(object sender, EventArgs e)
        {

           
            var qutaion = supplierQuotationController.GetSupplierQuotationbyQutationId(int.Parse(ViewState["quationid"].ToString()));

            gvDocs.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(ViewState["quationid"].ToString()));
            gvDocs.DataBind();

            gvImages.DataSource = quotationImageController.GetQuotationImages(int.Parse(ViewState["quationid"].ToString()));
            gvImages.DataBind();
            txtTermsAndConditions.Text = qutaion.TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlAttachments').modal('show') });   </script>", false);
        }
    }
}