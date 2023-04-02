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


namespace BiddingSystem
{
    public partial class CompanyViewAndConfirmPO : System.Web.UI.Page
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

        static string UserId = string.Empty;
        private string PRId = string.Empty;

        private string UserDept = string.Empty;
        private string OurRef = string.Empty;
        private string PrCode = string.Empty;
        private string RequestedDate = string.Empty;
        private string UserRef = string.Empty;
        private string RequesterName = string.Empty;
        private int basePr = 0;
        int CompanyId = 0;
        static int PoId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyViewAndConfirmPO.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ConfirmPOLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }

                if (Session["PoId"] != null)
                {
                    PoId = int.Parse(Session["PoId"].ToString());
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
            lblDateNow.Text = LocalTime.Today.ToString("dd-MM-yyyy");
            if (!IsPostBack)
            {
                try
                {
                    POMaster pOMaster = pOMasterController.GetPoMasterObjByPoIdRaised(PoId, int.Parse(Session["CompanyId"].ToString()));
                    basePr = pOMaster.BasePr;
                    Session["BasePRID"] = pOMaster.BasePr;
                    lblBasedPr.Text = pr_MasterController.FetchApprovePRDataByPRId(basePr).PrCode;
                    lblSupplierName.Text = pOMaster._Supplier.SupplierName; ;
                    lblAddress.Text = pOMaster._Supplier.Address1 + "," + pOMaster._Supplier.Address2;

                    lblCompanyName.Text = pOMaster._companyDepartment.DepartmentName;
                    gvPurchaseOrderItems.DataSource = pOMaster._PODetails;
                    lblVatNo.Text = pOMaster._companyDepartment.VatNo;
                    lblPhoneNo.Text = pOMaster._companyDepartment.PhoneNO;
                    lblFaxNo.Text = pOMaster._companyDepartment.FaxNO;
                    lblRefNo.Text = pr_MasterController.FetchApprovePRDataByPRId(basePr).Ref01;
                    gvPurchaseOrderItems.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvPurchaseOrderItems.Rows[x].Cells[0].Text);
                List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFiles(int.Parse(PRId), itemId);
                gvUploadFiles.DataSource = pr_FileUpload;
                gvUploadFiles.DataBind();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //-----------2 : Rejected 1: Approved
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                pOMasterController.ConfirmOrDenyPOApproval(int.Parse(PRId), 1);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location = 'CustomerPOConfirmation.aspx'; });   </script>", false);
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
                pOMasterController.ConfirmOrDenyPOApproval(int.Parse(PRId), 2);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location = 'CustomerPOConfirmation.aspx'; });   </script>", false);
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
    }
}