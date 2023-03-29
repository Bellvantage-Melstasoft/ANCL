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
//using iTextSharp.text.pdf;
using System.Data;
using System.Net.Mail;
using System.Web.Script.Serialization;
using SelectPdf;
using System.Net;

//using PdfDocument = SelectPdf.PdfDocument;

namespace BiddingSystem
{
    public partial class ViewPOReportEmail : System.Web.UI.Page
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
           
            if (!IsPostBack)
            {
                try
                {
                    POMaster pOMaster = pOMasterController.GetPoMasterObjByPoIdRaised(int.Parse(Request.QueryString.Get("PoId")), int.Parse(Session["CompanyId"].ToString()));
                    SupplierQuotation ImportDetails = supplierQuotationController.GetImportDetails(int.Parse(Request.QueryString.Get("PoId")), int.Parse(Session["CompanyId"].ToString()));
                    ViewState["PurchaseType"] = pOMaster.PurchaseType;

                    ViewState["quationid"] = pOMaster.QuotationId;
                    ViewState["basePr"] = pOMaster.BasePr;
                    Session["BasePRID"] = pOMaster.BasePr;
                    //lblBasedPr.Text = pr_MasterController.FetchApprovePRDataByPRId(int.Parse(ViewState["basePr"].ToString())).PrCode;
                    //lblPOCode.Text = pOMaster.POCode;
                    //lblSupplierName.Text = pOMaster._Supplier.SupplierName; ;
                    //lblAddress.Text = pOMaster._Supplier.Address1 + "," + pOMaster._Supplier.Address2;
                    //lblstorekeeper.Text = pOMaster.StoreKeeperName;
                    foreach (PODetails item in pOMaster._PODetails)
                    {
                        item.supplierQuotationItem = supplierQuotationController.GetQuotationItemsByQuotationItemId(item.QuotationItemId);
                    }                    
                    //gvPurchaseOrderItems.DataSource = pOMaster._PODetails;
                    //gvPurchaseOrderItems.DataBind();
                    //
                    lblCompName.Text = pOMaster._companyDepartment.DepartmentName;
                    lblCompVatNo.Text = pOMaster._companyDepartment.VatNo;
                    lblTpNo.Text = pOMaster._companyDepartment.PhoneNO;
                    lblFax.Text = pOMaster._companyDepartment.FaxNO;
                    lblcompAdd.Text = pOMaster._companyDepartment.Address1;
                    // lblRefNo.Text = pr_MasterController.FetchApprovePRDataByPRId(basePr).Ref01;
                    lblPurchaseType.Text = pOMaster.PurchaseType == 1 ? "Local" : "Import";

                    if (pOMaster.PurchaseType == 2) {
                        if (pOMaster.ImportItemType == 2) {
                            gvPoItems.Columns[20].Visible = false;
                        }
                    }
                    if (pOMaster.PurchaseType == 1) {
                        gvPoItems.Columns[19].Visible = false;
                        gvPoItems.Columns[20].Visible = false;
                    }

                    lblSupName.Text = pOMaster._Supplier.SupplierName;
                    lblSupplierAddress.Text = pOMaster._Supplier.Address1;
                    lblSupplierContact.Text = pOMaster._Supplier.OfficeContactNo + " / " + pOMaster._Supplier.PhoneNo;

                    lblWarehouseName.Text = pOMaster._Warehouse.Location;
                    lblWarehouseAddress.Text = pOMaster._Warehouse.Address;
                    lblWarehouseContact.Text = pOMaster._Warehouse.PhoneNo;
                    lblStoreKeeper.Text = pOMaster.StoreKeeper;
                    txtRemarks.Text = pOMaster.Remarks == null ? "-" : pOMaster.Remarks;

                    lblPO.Text = pOMaster.POCode;
                    lblPrCode.Text = "PR-"+ pr_MasterController.FetchApprovePRDataByPRId(int.Parse(ViewState["basePr"].ToString())).PrCode;
                    //lblQuotationFor.Text = pOMaster.Description;
                    //lblSK.Text = pOMaster.StoreKeeperName;
                    lblDate.Text = LocalTime.Today.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);

                    if (pOMaster.PurchaseType == 2) {
                        gvPoItems.Columns[16].Visible = false;
                    }

                    if (pOMaster.IsApproved == 0) {
                        lblPending.Visible = true;
                    }
                    else if (pOMaster.IsApproved == 1) {
                        lblApproved.Visible = true;
                    }
                    else {
                        lblRejected.Visible = true;
                    }

                    if (pOMaster.IsDerived == 0) {
                        lblGeneral.Visible = true;
                    }
                    else if (pOMaster.IsDerived == 1 && pOMaster.IsDerivedType == 1) {
                        lblModified.Visible = true;
                    }
                    else {
                        lblCovering.Visible = true;
                    }

                    

                    //tdSubTotal.InnerHtml = pOMaster._PODetails.Sum(pd => pd.SubTotal).ToString("N2");
                    ////tdNbt.InnerHtml = pOMaster.NBTAmount.ToString("N2");
                    //tdVat.InnerHtml = pOMaster.VatAmount.ToString("N2");
                    //tdNetTotal.InnerHtml = pOMaster.TotalAmount.ToString("N2");

                    
                    if (pOMaster.PurchaseType == 2) {
                        PanenImports.Visible = true;
                        pnlLogo.Visible = true;
                       
                        lblCurrency.Text = ImportDetails.CurrencyShortname;
                    //lblPriceTerms.Text = ImportDetails.TermName;
                    lblPaymentMode.Text = ImportDetails.PaymentMode;
                    }


                    //lblCreatedByName.Text = pOMaster.CreatedByName;
                    lblCreatedByDesignation.Text = pOMaster.CreatedDesignationName;
                    lblCreatedDate.Text = pOMaster.CreatedDate.ToString("dd/MM/yyyy");


                    if (File.Exists(HttpContext.Current.Server.MapPath(pOMaster.CreatedSignature)))
                        imgCreatedBySignature.ImageUrl = pOMaster.CreatedSignature;
                    else
                        imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

                    if (pOMaster.IsApproved != 0) {
                        pnlApprovedBy.Visible = true;
                        //lblApprovedByName.Text = pOMaster.ApprovedByName;
                        lblApprovedByDesignation.Text = pOMaster.ApprovedDesignationName;
                        lblApprovedDate.Text = pOMaster.ApprovedDate.ToString("dd/MM/yyyy");
                        lblApprovalRemarks.Text = pOMaster.ApprovalRemarks;
                        if (File.Exists(HttpContext.Current.Server.MapPath(pOMaster.ApprovedSignature)))
                            imgApprovedBySignature.ImageUrl = pOMaster.ApprovedSignature;
                        else
                            imgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                    }

                    if (pOMaster.IsApproved == 1) {
                        lblApprovalText.InnerHtml = "Approved By";
                    }
                    else if (pOMaster.IsApproved == 2) {
                        lblApprovalText.InnerHtml = "Rejected By";
                    }

                    
                    if (pOMaster.IsDerived == 1 && pOMaster.IsApprovedByParentApprovedUser != 0 && pOMaster.ParentApprovedByName != "" && pOMaster.ParentApprovedByName != null) {
                        pnlParentApprovedByDetails.Visible = true;
                        lblParentApprovalRemarks.Text = pOMaster.ParentApprovedUserApprovalRemarks;
                        //lblParentApprovedByName.Text = pOMaster.ParentApprovedByName;
                        lblParentApprovedByDesignation.Text = pOMaster.ParentApprovedDesignationName;
                        lblParentApprovedDate.Text = pOMaster.ParentApprovedUserApprovalDate.ToString("dd/MM/yyyy");

                        if (File.Exists(HttpContext.Current.Server.MapPath(pOMaster.ParentApprovedBySignature)))
                            imgParentApprovedBySignature.ImageUrl = pOMaster.ParentApprovedBySignature;
                        else
                            imgParentApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                    }

                    if (pOMaster.IsDerived == 1 && pOMaster.IsApprovedByParentApprovedUser == 1 && pOMaster.ParentApprovedByName != "" && pOMaster.ParentApprovedByName != null) {
                        lblParentApprovalText.InnerHtml = "Parent Approved User: APPROVED";
                    }
                    else if (pOMaster.IsDerived == 1 && pOMaster.IsApprovedByParentApprovedUser == 2 && pOMaster.ParentApprovedByName != "" && pOMaster.ParentApprovedByName != null) {
                        lblParentApprovalText.InnerHtml = "Parent Approved User: REJECTED";
                    }
                    
                    //gvPoItems.DataSource = pOMaster._PODetails;
                    //gvPoItems.DataBind();
                    int VarPoPurchaseType = 0;
                    string VarSupplierAgentName = "";
                    decimal Total = 0;

                    for (int i = 0; i < pOMaster._PODetails.Count; i++) {
                        VarPoPurchaseType = pOMaster._PODetails[i].PoPurchaseType;
                        VarSupplierAgentName = pOMaster._PODetails[i].SupplierAgentName;
                        if (pOMaster.PurchaseType == 2){
                            if (pOMaster._PODetails[i].PoPurchaseType == 1) {
                                PanenImports.Visible = false;
                                pnlLogo.Visible = false;
                            }
                            if (pOMaster._PODetails[i].PoPurchaseType == 2) {

                                pOMaster._PODetails[i].SubTotal = pOMaster._PODetails[i].UnitPriceForeign * pOMaster._PODetails[i].Quantity;
                                pOMaster._PODetails[i].VatAmount = 0;
                                pOMaster._PODetails[i].TotalAmount = pOMaster._PODetails[i].SubTotal + pOMaster._PODetails[i].VatAmount;
                                pOMaster.VatAmount = 0;
                                Total = Total + pOMaster._PODetails[i].SubTotal;
                                pOMaster.TotalAmount = Total;
                            }
                        }
                    }

                    if (pOMaster.PaymentMethod != null && pOMaster.PaymentMethod != "") {
                        if (VarPoPurchaseType == 1) {
                            lblPaymentType.Text = pOMaster.PaymentMethod == "1" ? "Cash Payment" : pOMaster.PaymentMethod == "2" ? "Cheque Payment" : pOMaster.PaymentMethod == "3" ? "Credit Payment" : pOMaster.PaymentMethod == "4" ? "Advance Payment" : "-";
                        }
                        else if (VarPoPurchaseType == 2) {
                            lblPaymentType.Text = pOMaster.PaymentMethod == "1" ? "Advance" : pOMaster.PaymentMethod == "2" ? "On Arrival" : pOMaster.PaymentMethod == "3" ? "LC at sight" : pOMaster.PaymentMethod == "4" ? "L/C usance" : pOMaster.PaymentMethod == "5" ? "D/A" : pOMaster.PaymentMethod == "6" ? "D/P" : "-";

                        }
                        pnlPaymentMethod.Visible = true;
                    }

                    gvPoItems.DataSource = pOMaster._PODetails;
                    gvPoItems.DataBind();

                    tdSubTotal.InnerHtml = pOMaster._PODetails.Sum(pd => pd.SubTotal).ToString("N2");
                    //tdNbt.InnerHtml = pOMaster.NBTAmount.ToString("N2");
                    tdVat.InnerHtml = pOMaster.VatAmount.ToString("N2");
                    tdNetTotal.InnerHtml = pOMaster.TotalAmount.ToString("N2");

                    lblPoPurchaseType.Text = VarPoPurchaseType == 1 ? "Local" : "Import";
                    lblAgentName.Text = VarSupplierAgentName;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void gvPOItems_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.Header) {
                if (ViewState["PurchaseType"].ToString() != "2") {

                    e.Row.Cells[5].CssClass = "hidden";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow) {
                if (ViewState["PurchaseType"].ToString() != "2") {

                    e.Row.Cells[5].CssClass = "hidden";
                }
            }
        }
        

       
        
        protected void btnTerminated_Click(object sender, EventArgs e) {
            int podId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            
            gvTerminatedDetails.DataSource = pODetailsController.TerminatedPO(podId);
            gvTerminatedDetails.DataBind();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlTerminated').modal('show'); });   </script>", false);

        }

        protected void btnPrint_Click(object sender, EventArgs e) {

            ControllerFactory.CreatePOMasterController().UpdatePrintCount(int.Parse(HttpContext.Current.Request.QueryString.Get("PoId")));
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { var printWindow = window.open('ViewPOForPrint.aspx?PoId=" + HttpContext.Current.Request.QueryString.Get("PoId") + "'); printWindow.print(); printWindow.onafterprint = window.close; });   </script>", false);
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

        protected void ss_Click(object sender, EventArgs e) {
            try {
                int PoId = int.Parse(Request.QueryString.Get("PoId"));
                int CompanyId = int.Parse(Session["CompanyId"].ToString());
                Response.Redirect(String.Format("POPDF.aspx?PoId={0}&CompanyId={1}", PoId, CompanyId));
                //Response.Redirect("POPDF.aspx?PoId=" + PoId + "&CompanyId=" + CompanyId);
            }
            catch (Exception ex) {


            }
        }

                protected void btnDownload_Click(object sender, EventArgs e) {
            try {
                CompanyLogin companyLogin = companyLoginController.GetUserbyPOId(int.Parse(Request.QueryString.Get("PoId")));
                string createdUserEmail = companyLogin.EmailAddress;

                Supplier supplier = ControllerFactory.CreateSupplierController().getSupplierByPOId(int.Parse(Request.QueryString.Get("PoId")));
                string SupplierEmail = supplier.Email;
                int PoId = int.Parse(Request.QueryString.Get("PoId"));
                int CompanyId = int.Parse(Session["CompanyId"].ToString());
                HtmlToPdf converter = new HtmlToPdf();
               // PdfDocument doc = converter.ConvertUrl(HttpContext.Current.Request.Url.AbsoluteUri.Replace("ViewPOReportEmail.aspx", "POPDF.aspx?PoId=" + PoId+" & CompanyId = " + CompanyId + " ") );
                PdfDocument doc = converter.ConvertUrl(HttpContext.Current.Request.Url.AbsoluteUri.Replace("ViewPOReportEmail.aspx", String.Format("POPDF.aspx?PoId={0}&CompanyId={1}", PoId, CompanyId  )));
               
                MemoryStream pdfStream = new MemoryStream();

                // save pdf document into a MemoryStream 
                doc.Save(pdfStream);
                pdfStream.Position = 0;

                string message = "Dear Supplier,\n\nPlease find the purchase order below.";


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587);
                mail.From = new MailAddress("procurement@lakehouse.lk");
                mail.To.Add(SupplierEmail);
                mail.CC.Add(new MailAddress(createdUserEmail));
                mail.Subject = "Purchase Order";
                mail.Body = message;
                mail.Attachments.Add(new Attachment(pdfStream, "PurchaseOrder.pdf"));

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("procurement@lakehouse.lk", "user@123lh");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Email Sent', showConfirmButton: false,timer: 4000})});   </script>", false);

                //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Email Sent', showConfirmButton: false,timer: 4000}).then((result) => { window.location = 'CusromerPOView.aspx'}); });   </script>", false);
                ControllerFactory.CreatePOMasterController().UpdatePoEmailStatus(int.Parse(Request.QueryString.Get("PoId")));


                //MailMessage mail = new MailMessage("admin@ezbidlanka.com", SupplierEmail);
                //mail.CC.Add(new MailAddress(createdUserEmail));
                //mail.Subject = "Purchase Order";
                //mail.Body = message;
                //mail.Attachments.Add(new Attachment(pdfStream, "PurchaseOrder.pdf"));

                //using (SmtpClient sc = new SmtpClient("smtp.yandex.ru", 587)) {
                //    sc.EnableSsl = true;
                //    sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                //    sc.UseDefaultCredentials = false;
                //    sc.Credentials = new NetworkCredential("admin@ezbidlanka.com", "Bell@1234");

                //    sc.Send(mail);
                //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Email Sent', showConfirmButton: false,timer: 4000}).then((result) => { window.location = 'CusromerPOView.aspx'}); });   </script>", false);

                //}


            }
            catch (Exception ex) {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error while sending email', showConfirmButton: false,timer: 3000}); });   </script>",
                        false);
            }
        }
    }
}