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
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class CustomerViewPurchaseOrder : System.Web.UI.Page
    {
        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController pODetailsController = ControllerFactory.CreatePODetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

      //  static string UserId = string.Empty;
      //  private string PRId = string.Empty;
        //static int quationid = 0;
       // private string UserDept = string.Empty;
       // private string OurRef = string.Empty;
       // private string PrCode = string.Empty;
      //  private string RequestedDate = string.Empty;
      //  private string UserRef = string.Empty;
       // private string RequesterName = string.Empty;
       // public static PR_Master prMaster = new PR_Master();
       // int CompanyId = 0;
       // static int PoId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
               // CompanyId = int.Parse(Session["CompanyId"].ToString());
               // UserId = Session["UserId"].ToString();

                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
               // ((BiddingAdmin)Page.Master).subTabValue = "CusromerPOView.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewPOLink";
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 8) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }

                if (Session["PoId"] != null)
                {
                    ViewState["PoId"] = int.Parse(Session["PoId"].ToString());
                }
                else
                {
                    Response.Redirect("CusromerPOView.aspx");
                }


                //if (Session["EmailSentStatus"] != null)
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Email Sent', showConfirmButton: false,timer: 1500}); });   </script>", false);
                //    Session["EmailSentStatus"] = null;
                //}

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
                    string QryString = Request.QueryString.Get("Report");
                    if (QryString=="1")
                    {
                        btnDownload.Visible = false;
                    }

                    POMaster pOMaster = pOMasterController.GetPoMasterObjByPoIdView(int.Parse(ViewState["PoId"].ToString()));
                    var prMaster = pR_MasterController.FetchApprovePRDataByPRId(pOMaster.BasePr);
                    ViewState["prMaster"] = new JavaScriptSerializer().Serialize(prMaster);

                    ViewState["quationid"] = pOMaster.QuotationId;
                    txtTermsAndConditions.Text = supplierQuotationController.GetSupplierQuotationbyQutationId(int.Parse(ViewState["quationid"].ToString())).TermsAndCondition;
                    lblPOCode.Text = pOMaster.POCode;
                    lblSupplierName.Text = pOMaster._Supplier.SupplierName; ;
                    lblAddress.Text = pOMaster._Supplier.Address1 + "," + pOMaster._Supplier.Address2;
                    lblCompanyName.Text = pOMaster._companyDepartment.DepartmentName;
                    lblPhoneNo.Text = pOMaster._companyDepartment.PhoneNO == "" ? pOMaster._companyDepartment.MobileNo : pOMaster._companyDepartment.PhoneNO;
                    lblFaxNo.Text = pOMaster._companyDepartment.FaxNO;
                    // lblRefNo.Text = prMaster.OurReference;
                    lblBasedPr.Text = prMaster.PrCode;

                    lblPaymentMethod.Text = pOMaster.PaymentMethod == "V"?"Cheque Payment" : "Cash Payment";
                    lblApprovedByName.Text = pOMaster.ApprovedByName;
                    lblstorekeeper.Text = pOMaster.StoreKeeperName;

                    foreach (PODetails item in pOMaster._PODetails)
                    {
                        item.supplierQuotationItem = supplierQuotationController.GetQuotationItemsByQuotationItemId(item.QuotationItemId);
                    }

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
                        if(item.IsCustomizedAmount == 1){
                            SubTotalCus = item.CustomizedAmount * item.Quantity;
                            VatTotalCus = item.CustomizedVat;
                            NbtTotalCus = item.CustomizedNbt;

                        }

                        if(item.IsCustomizedAmount == 0){
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

       // public string getPRDetailData()
       // {
            //try
          //  {
                //string data = "";
                //List<PR_Details> pr_Details = pr_DetailController.FetchPR_DetailsByDeptIdAndPrId(int.Parse(PRId));
                //if (pr_Details.Count > 0)
                //{
                //    foreach (var item in pr_Details)
                //    {
                //        int itemId = item.ItemId;
                //        string Description = item.ItemDescription;
                //        string Purpose = item.Purpose;
                //        decimal Quantity = item.ItemQuantity;
                //        string Replacement = "No";
                //        if (item.Replacement == 1)
                //        {
                //            Replacement = "Yes";
                //        }
                //        data += "<tr><td>" + itemId + "</td><td>" + Description + "</td><td>" + "<input type='button' value='View Attachments' class='btn btn-info' />" + "</td><td>" + Purpose + "</td><td>" + Quantity + "</td><td>" + Replacement + "</td></tr>";
                //    }
                //}
                //return data;
           // }
          //  catch (Exception ex)
           // {
           //     throw ex;
           //}
            
       // }
        //-----------2 : Rejected 1: Approved
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
               // pOMasterController.PoMasterApproval(PoId ,1);
                Response.Redirect("CusromerPOView.aspx");
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
               // pOMasterController.PoMasterApproval(PoId, 2);
                Response.Redirect("CusromerPOView.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(new JavaScriptSerializer().Deserialize<PR_Master>(ViewState["prMaster"].ToString()).CreatedBy));
                string createdUserEmail = companyLogin.EmailAddress;
                //Response.Redirect("POPrint.aspx?toDo=2");


                HtmlToPdf converter = new HtmlToPdf();

                // create a new pdf document converting an url 
                PdfDocument doc = converter.ConvertUrl(HttpContext.Current.Request.Url.AbsoluteUri.Replace("CustomerViewPurchaseOrder.aspx", "POPDF.aspx") );

                MemoryStream pdfStream = new MemoryStream();

                // save pdf document into a MemoryStream 
                doc.Save(pdfStream);

                // reset stream position 
                pdfStream.Position = 0;
                string message = "Dear Supplier,\n\nPlease find the purchase order below.";
                // create email message 
                MailMessage mail = new MailMessage("NoReply@ezbidlanka.com", "sameeraguna22@gmail.com");
                mail.CC.Add(new MailAddress(createdUserEmail));
                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("NoReply@ezbidlanka.com", "ezbidlive@123");
                client.Host = "mail.ezbidlanka.com";
                mail.Subject = "Purchase Order";
                mail.Body = message;
                

                mail.Attachments.Add(new Attachment(pdfStream, "PurchaseOrder.pdf"));


                client.Send(mail);
                //Session["EmailSentStatus"] = "Success";
                //Response.Redirect("CustomerViewPurchaseOrder.aspx");

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Email has been sent successfully.', showConfirmButton: false,timer: 1500}); });   </script>", false);


                //string htmlString = print.Value;


                //int webPageWidth = 1024;

                //int webPageHeight = 0;

                //// instantiate a html to pdf converter object 
                //HtmlToPdf converter = new HtmlToPdf();

                //// set converter options 
                //converter.Options.PdfPageSize = PdfPageSize.A4;
                //converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                //converter.Options.WebPageWidth = webPageWidth;
                //converter.Options.WebPageHeight = webPageHeight;

                //// create a new pdf document converting an url 
                //PdfDocument doc = converter.ConvertHtmlString(htmlString);

                //// save pdf document 
                //doc.Save(Response, false, "Sample.pdf");

                //// close pdf document 
                //doc.Close();



                //Response.ContentType = "application/pdf";
                //Response.AddHeader("content-disposition", "attachment;filename=Panel.pdf");
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //StringWriter sw = new StringWriter();
                //HtmlTextWriter hw = new HtmlTextWriter(sw);
                //divToDownoad.RenderControl(hw);
                //StringReader sr = new StringReader(sw.ToString());
                //Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                //pdfDoc.Open();
                //htmlparser.Parse(sr);
                //pdfDoc.Close();
                //Response.Write(pdfDoc);

                //string filename = base.Server.MapPath("~/GeneratedPO/" + "UserDetails.pdf");
                //HttpContext.Current.Request.SaveAs(filename, false);

                //Response.End();

            }
            catch (Exception)
            {

                throw;
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            //
        }
    }
}