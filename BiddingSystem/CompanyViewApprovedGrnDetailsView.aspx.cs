using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class CompanyViewApprovedGrnDetailsView : System.Web.UI.Page
    {
        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        static string UserId = string.Empty;
        private string PRId = string.Empty;

        private string UserDept = string.Empty;
        private string OurRef = string.Empty;
        private string PrCode = string.Empty;
        private string RequestedDate = string.Empty;
        private string UserRef = string.Empty;
        private string RequesterName = string.Empty;

        int CompanyId = 0;
        int GrnID = 0;
        int PoID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
               // ((BiddingAdmin)Page.Master).subTabValue = "CompanyViewApprovedGRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "CompanyViewApprovedGRNLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 11) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }

                if (Request.QueryString.Get("GrnID") != null && Request.QueryString.Get("PoID") != null)
                {
                    GrnID = int.Parse(Request.QueryString.Get("GrnID"));
                    PoID = int.Parse(Request.QueryString.Get("PoID"));
                }
                else
                {
                    Response.Redirect("CompanyViewApprovedGRN.aspx");
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

                    GrnMaster grnMaster = grnController.GetGrnMasterByGrnID(GrnID, PoID);
                    PR_Master PR_Master = pR_MasterController.FetchApprovePRDataByPRId(grnMaster._POMaster.BasePr);
                    CompanyDepartment companyDepartment = companyDepartmentController.GetDepartmentByDepartmentId(CompanyId);
                    lblPOCode.Text = grnMaster._POMaster.POCode;
                    lblSupplierName.Text = grnMaster._Supplier.SupplierName; ;
                    lblAddress.Text = grnMaster._Supplier.Address1 + "," + grnMaster._Supplier.Address2;
                    lblRefNo.Text = PR_Master.OurReference.ToString();
                    lblCompanyName.Text = companyDepartment.DepartmentName;
                    lblCompanyAddress.Text = grnMaster._companyDepartment.Address2 != "" ? grnMaster._companyDepartment.Address1 + ",</br>" + grnMaster._companyDepartment.Address2 + "," : grnMaster._companyDepartment.Address1 + ",";
                    lblCity.Text = grnMaster._companyDepartment.City != "" ? grnMaster._companyDepartment.City + "," : grnMaster._companyDepartment.City;
                    lblCountry.Text = grnMaster._companyDepartment.Country != "" ? grnMaster._companyDepartment.Country + "." : grnMaster._companyDepartment.Country;
                    lblDateNow.Text = LocalTime.Now.ToString("dd/MM/yyyy");
                    //lblSubtotal.Text = grnMaster.TotalAmount.ToString("n");
                    //lblVatTotal.Text = grnMaster._POMaster.VatAmount.ToString("n");
                    //lblNbtTotal.Text = grnMaster._POMaster.NBTAmount.ToString("n");
                    //lblTotal.Text = grnMaster._POMaster.TotalAmount.ToString("n");
                    lblgrnComment.InnerText = grnMaster.GrnNote;
                    lblReceiveddate.InnerText = grnMaster.GoodReceivedDate.ToString("yyyy-MM-dd");

                    gvPurchaseOrderItems.DataSource = gRNDetailsController.GrnDetialsGrnApprovedOnly(GrnID, PoID, CompanyId);
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

                    List<GrnDetails> _grndetails = gRNDetailsController.GrnDetialsGrnApprovedOnly(GrnID, PoID, CompanyId);

                    foreach (var item in _grndetails)
                    {
                            SubTotal = item.ItemPrice * item.Quantity;
                            VatTotal = item.VatAmount;
                            NbtTotal = item.NbtAmount;
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
    }
}