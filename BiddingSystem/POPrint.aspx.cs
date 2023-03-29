using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Data;
using System.IO;

using SelectPdf;
using System.Net.Mail;
using System.Net.Mime;

namespace BiddingSystem
{
    public partial class POPrint : System.Web.UI.Page
    {

        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        
       // private string PRId = string.Empty;
        
        //private string OurRef = string.Empty;
        //private string PrCode = string.Empty;
        //private string RequestedDate = string.Empty;
        //private string UserRef = string.Empty;
        //private string RequesterName = string.Empty;

      //  int CompanyId = 0;
       // int PoId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            lblDateNow.Text = LocalTime.Today.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);


            if (!IsPostBack)
            {
                try
                {
                   int PoId = int.Parse(Request.QueryString.Get("PoId"));

                    POMaster pOMaster = pOMasterController.GetPoMasterObjByPoIdView(PoId);
                    PR_Master prMaster = pR_MasterController.FetchApprovePRDataByPRId(pOMaster.BasePr);
                    lblPOCode.Text = pOMaster.POCode;
                    lblSupplierName.Text = pOMaster._Supplier.SupplierName;
                    lblAddress.Text = pOMaster._Supplier.Address1 + "," + pOMaster._Supplier.Address2;
                    lblCompanyName.Text = pOMaster._companyDepartment.DepartmentName;
                    lblPhoneNo.Text = pOMaster._companyDepartment.PhoneNO == "" ? pOMaster._companyDepartment.MobileNo : pOMaster._companyDepartment.PhoneNO;
                    lblFaxNo.Text = pOMaster._companyDepartment.FaxNO;
                    lblRefNo.Text = prMaster.OurReference;

                    lblPaymentMethod.Text = pOMaster.PaymentMethod == "V" ? "Cheque Payment" : "Cash Payment";
                    lblApprovedByName.Text = pOMaster.ApprovedByName;
                    //lblRefNo.Text= pOMaster._companyDepartment.
                    //lblSubtotal.Text = pOMaster.TotalAmount.ToString("n");
                    //lblVatTotal.Text = pOMaster.VatAmount.ToString("n");
                    //lblNbtTotal.Text = pOMaster.NBTAmount.ToString("n");
                    //lblTotal.Text = pOMaster.TotalAmount.ToString("n");
                    gvPurchaseOrderItems.DataSource = pOMaster._PODetails;
                    lblVatNo.Text = pOMaster._companyDepartment.VatNo;
                    gvPurchaseOrderItems.DataBind();

                    decimal SubTotal = 0;
                    decimal VatTotal = 0;
                    decimal NbtTotal = 0;
                    decimal SubTotalCus = 0;
                    decimal VatTotalCus = 0;
                    decimal NbtTotalCus = 0;
                    decimal TotalVat = 0;
                    decimal TotalNbt = 0;
                    decimal TotalSubAmount = 0;
                    decimal TotalAmount = 0;

                    foreach (var item in pOMaster._PODetails)
                    {
                        if (item.IsCustomizedAmount == 1)
                        {
                            SubTotalCus = item.CustomizedAmount * item.Quantity;
                            VatTotalCus = item.CustomizedVat;
                            NbtTotalCus = item.CustomizedNbt;

                        }

                        if (item.IsCustomizedAmount == 0)
                        {
                            SubTotal = item.ItemPrice * item.Quantity;
                            VatTotal = item.VatAmount;
                            NbtTotal = item.NbtAmount;
                        }
                    }

                    TotalSubAmount = SubTotal + SubTotalCus;
                    TotalNbt = NbtTotal + NbtTotalCus;
                    TotalVat = VatTotalCus + VatTotal;

                    TotalAmount = TotalSubAmount + TotalNbt + TotalVat;

                    lblSubtotal.Text = TotalSubAmount.ToString("n");
                    lblVatTotal.Text = TotalVat.ToString("n");
                    lblNbtTotal.Text = TotalNbt.ToString("n");
                    lblTotal.Text = TotalAmount.ToString("n");

                   

                    //if (toDo == 1)
                    //{
                    //    // get html of the page 
                    //    TextWriter myWriter = new StringWriter();
                    //    HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
                    //    base.Render(htmlWriter);

                    //    // instantiate a html to pdf converter object 
                    //    HtmlToPdf converter = new HtmlToPdf();
                    //    converter.Options.PdfPageSize = PdfPageSize.A4;
                    //    converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                    //    converter.Options.WebPageWidth = 1024;
                    //    converter.Options.WebPageHeight = 0;
                    //    // create a new pdf document converting the html string of the page 
                    //    PdfDocument doc = converter.ConvertHtmlString(
                    //        myWriter.ToString(), Request.Url.AbsoluteUri);

                    //    // save pdf document 
                    //    doc.Save(Response, false, "Sample.pdf");

                    //    // close pdf document 
                    //    doc.Close();
                    //}
                    //else
                    //{
                    //    // get html of the page 
                    //    TextWriter myWriter = new StringWriter();
                    //    HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
                    //    base.Render(htmlWriter);

                    //    // instantiate a html to pdf converter object 
                    //    HtmlToPdf converter = new HtmlToPdf();
                    //    converter.Options.PdfPageSize = PdfPageSize.A4;
                    //    converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                    //    converter.Options.WebPageWidth = 1024;
                    //    converter.Options.WebPageHeight = 0;
                    //    // create a new pdf document converting the html string of the page 
                    //    PdfDocument doc = converter.ConvertHtmlString(
                    //        myWriter.ToString(), Request.Url.AbsoluteUri);

                    //    MemoryStream pdfStream = new MemoryStream();

                    //    // save pdf document into a MemoryStream 
                    //    doc.Save(pdfStream);

                    //    // reset stream position 
                    //    pdfStream.Position = 0;
                    //    string message = "Dear Supplier,\n\nPlease find the purchase order below.";
                    //    // create email message 
                    //    MailMessage mail = new MailMessage("NoReply@ezbidlanka.com", "salman.primary@gmail.com");
                    //    SmtpClient client = new SmtpClient();
                    //    client.Port = 25;
                    //    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //    client.UseDefaultCredentials = false;
                    //    client.Credentials = new System.Net.NetworkCredential("NoReply@ezbidlanka.com", "ezbidlive@123");
                    //    client.Host = "mail.ezbidlanka.com";
                    //    mail.Subject = "Purchase Order";
                    //    mail.Body = message;
                    //    //               
                    //    //string x = System.Web.HttpContext.Current.Server.MapPath(@"AdminResources\\images\\lakehouselogo.jpg");
                    //    //string mediaType = MediaTypeNames.Image.Jpeg;
                    //    //LinkedResource logo = new LinkedResource(x, mediaType);
                    //    //logo.ContentId = Guid.NewGuid().ToString();
                    //    //logo.ContentId = "companylogo";
                    //    //logo.ContentType.MediaType = mediaType;
                    //    //logo.ContentType.Name = logo.ContentId;
                    //    //logo.TransferEncoding = TransferEncoding.Base64;
                    //    //AlternateView av1 = AlternateView.CreateAlternateViewFromString("<html><body><img src=cid:companylogo/><br></body></html>" + message, null, MediaTypeNames.Text.Html);
                    //    //av1.LinkedResources.Add(logo);
                    //    //mail.AlternateViews.Add(av1);
                    //    //mail.IsBodyHtml = true;

                    //    mail.Attachments.Add(new Attachment(pdfStream, "PurchaseOrder.pdf"));


                    //    client.Send(mail);
                    //    Session["EmailSentStatus"] = "Success";
                    //    //// send email 
                    //    //// close pdf document 
                    //    Response.Redirect("CustomerViewPurchaseOrder.aspx");
                        
                    //}


                }
                catch (Exception ex)
                {
                }

            }

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}