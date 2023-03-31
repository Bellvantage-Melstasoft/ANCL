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

using System.Data;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class EditPO : System.Web.UI.Page
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
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        PrControllerV2 PrControllerV2 = ControllerFactory.CreatePrControllerV2();

        decimal subTot = 0;
        decimal nbt = 0;
        decimal vat = 0;
        decimal tot = 0;
        decimal disc = 0;
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

                if (Session["PoId"] != null)
                {
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
                    ViewState["PrId"] = null;
                    VAT_NBT LatestVatNbt = generalSettingsController.getLatestVatNbt();
                    var vatValue = LatestVatNbt.VatRate;
                    var nbtVal1 = LatestVatNbt.NBTRate1;
                    var nbtVal2 = LatestVatNbt.NBTRate2;

                    hdnVatRate.Value = vatValue.ToString();
                    hdnNbtRate1.Value = nbtVal1.ToString();
                    hdnNbtRate2.Value = nbtVal2.ToString();

                    POMaster poMaster = pOMasterController.GetPoMasterToEditPO(int.Parse(Request.QueryString.Get("PoId")), int.Parse(Session["CompanyId"].ToString()));
                    SupplierQuotation ImportDetails = supplierQuotationController.GetImportDetails(int.Parse(Request.QueryString.Get("PoId")), int.Parse(Session["CompanyId"].ToString()));

                    lblsupplierName.Text = poMaster._Supplier.SupplierName;
                    lblSupplierAddress.Text = poMaster._Supplier.Address1;
                    lblSupplierContact.Text = poMaster._Supplier.OfficeContactNo + " / " + poMaster._Supplier.PhoneNo;
                    txtRemarks.Text = poMaster.Remarks == null ? "-" : poMaster.Remarks;

                    lblWarehouseName.Text = poMaster._Warehouse.Location;
                    lblWarehouseAddress.Text = poMaster._Warehouse.Address;
                    lblWarehouseContact.Text = poMaster._Warehouse.PhoneNo;
                    ViewState["WarehouseId"] = poMaster._Warehouse.WarehouseID;

                    lblPOCode.Text = poMaster.POCode;
                    lblPrCode.Text = poMaster.PrCode;

                    gvPoItems.DataSource = poMaster.PoDetails;
                    gvPoItems.DataBind();


                    tdSubTotal.InnerHtml = poMaster.PoDetails.Sum(pd => pd.SubTotal).ToString("N2");
                    //tdNbt.InnerHtml = poMaster.NBTAmount.ToString("N2");
                    tdVat.InnerHtml = poMaster.VatAmount.ToString("N2");
                    tdNetTotal.InnerHtml = poMaster.TotalAmount.ToString("N2");

                    //hdnSubTotal.Value = poMaster.PoDetails.Sum(pd => pd.SubTotal).ToString("N2");
                    //hdnNbtTotal.Value = poMaster.NBTAmount.ToString("N2");
                    //hdnVatTotal.Value = poMaster.VatAmount.ToString("N2");
                    //hdnNetTotal.Value = poMaster.TotalAmount.ToString("N2");

                    //if (poMaster.PurchaseType == 2) {
                    //    PanenImports.Visible = true;
                    //    lblCurrency.Text = ImportDetails.CurrencyShortname;
                    //    lblPriceTerms.Text = ImportDetails.TermName;
                    //    lblPaymentMode.Text = ImportDetails.PaymentMode;
                    //}

                    if ((gvPoItems.Rows.Count.ToString()) == "1")
                    {

                        gvPoItems.Columns[14].Visible = false;

                    }

                    if (poMaster.IsDerived == 0)
                    {
                        lblGeneral.Visible = true;
                    }
                    else if (poMaster.IsDerived == 1 && poMaster.IsDerivedType == 1)
                    {
                        lblModified.Visible = true;
                    }
                    else
                    {
                        lblCovering.Visible = true;
                    }

                    ViewState["PoMaster"] = new JavaScriptSerializer().Serialize(poMaster);
                    ViewState["ItemIds"] = new List<int>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        //public VAT_NBT ListVATNBTValues {
        //    get {
        //        if (ViewState["VAT_NBT"] == null)
        //            ViewState["VAT_NBT"] = new List<VAT_NBT>();
        //        return (VAT_NBT)ViewState["VAT_NBT"];
        //    }
        //}

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("ViewPO.aspx?PoId=" + Request.QueryString.Get("PoId"));
        }

        protected void bCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("EditPo.aspx?PoId=" + Request.QueryString.Get("PoId"));
        }


        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {



                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                ViewState["PodId"] = int.Parse(gvPoItems.Rows[x].Cells[0].Text);
                ViewState["PrId"] = int.Parse(gvPoItems.Rows[x].Cells[15].Text);
                //ViewState["ItemId"] = int.Parse(gvPoItems.Rows[x].Cells[2].Text);
                int itemid = int.Parse(gvPoItems.Rows[x].Cells[2].Text);

                List<int> itemIds = ViewState["ItemIds"] as List<int>;
                if (ViewState["ItemIds"] == null)
                {
                    itemIds = new List<int>();
                    itemIds.Add(itemid);
                    ViewState["ItemIds"] = itemIds;
                }
                else
                {
                    itemIds.Add(itemid);
                    ViewState["ItemIds"] = itemIds;
                }





                gvPoItems.Rows[x].Visible = false;
                TextBox txtUnitP = (TextBox)((GridViewRow)((Button)sender).NamingContainer).FindControl("txtUnitPrice");
                //TextBox txtActP = (TextBox)((GridViewRow)((Button)sender).NamingContainer).FindControl("txtActualPrice");
                TextBox txtQty = (TextBox)((GridViewRow)((Button)sender).NamingContainer).FindControl("txtQuantity");
                TextBox subtot = (TextBox)((GridViewRow)((Button)sender).NamingContainer).FindControl("txtSubTotal");
                TextBox txtnbt = (TextBox)((GridViewRow)((Button)sender).NamingContainer).FindControl("txtNbt");
                TextBox txtvat = (TextBox)((GridViewRow)((Button)sender).NamingContainer).FindControl("txtVat");
                TextBox txttot = (TextBox)((GridViewRow)((Button)sender).NamingContainer).FindControl("txtNetTotal");
                txtUnitP.Text = "0.00";
                txtQty.Text = "0.00";
                subtot.Text = "0.00";
                txtnbt.Text = "0.00";
                txtvat.Text = "0.00";
                txttot.Text = "0.00";
                //txtActP.Text = "0.00";

                if (gvPoItems.Rows.OfType<GridViewRow>().Where(r => r.Visible == true).Count() <= 1)
                {
                    gvPoItems.Columns[14].Visible = false;
                }

                foreach (GridViewRow gvrow in gvPoItems.Rows)
                {


                    subTot = subTot + (decimal.Parse(((TextBox)(gvrow.FindControl("txtUnitPrice"))).Text) * decimal.Parse(((TextBox)(gvrow.FindControl("txtQuantity"))).Text));
                    //disc += (decimal.Parse(((TextBox)(gvrow.FindControl("txtActualPrice"))).Text) * decimal.Parse(((TextBox)(gvrow.FindControl("txtQuantity"))).Text)) - decimal.Parse(((TextBox)(gvrow.FindControl("txtSubTotal"))).Text);
                    nbt = nbt + decimal.Parse(((TextBox)(gvrow.FindControl("txtNbt"))).Text);
                    vat = vat + decimal.Parse(((TextBox)(gvrow.FindControl("txtVat"))).Text);
                    tot = tot + decimal.Parse(((TextBox)(gvrow.FindControl("txtNetTotal"))).Text);

                }
                decimal NetTotal = tot + vat + nbt;
                //tdNbt.InnerHtml = nbt.ToString("N2");
                tdVat.InnerHtml = vat.ToString("N2");
                tdSubTotal.InnerHtml = subTot.ToString("N2");
                tdNetTotal.InnerHtml = tot.ToString("N2");
                //  tdDiscount.InnerHtml = disc.ToString("N2");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            POMaster OldPo = new JavaScriptSerializer().Deserialize<POMaster>(ViewState["PoMaster"].ToString());
            foreach (GridViewRow gvrow in gvPoItems.Rows)
            {

                subTot = subTot + decimal.Parse(((TextBox)(gvrow.FindControl("txtSubTotal"))).Text);
                nbt = nbt + decimal.Parse(((TextBox)(gvrow.FindControl("txtNbt"))).Text);
                vat = vat + decimal.Parse(((TextBox)(gvrow.FindControl("txtVat"))).Text);
                tot = tot + decimal.Parse(((TextBox)(gvrow.FindControl("txtNetTotal"))).Text);

            }


            POMaster PoMaster = new POMaster();
            PoMaster.DepartmentId = int.Parse(Session["CompanyId"].ToString());
            PoMaster.BasePr = OldPo.BasePr;
            PoMaster.SupplierId = OldPo.SupplierId;
            PoMaster.CreatedBy = Session["UserId"].ToString();
            PoMaster.DeliverToWarehouse = int.Parse(ViewState["WarehouseId"].ToString());
            //PoMaster.VatAmount += Math.Round(decimal.Parse(hdnVatTotal.Value), 2);
            //PoMaster.NBTAmount += Math.Round(decimal.Parse(hdnNbtTotal.Value), 2);
            //PoMaster.TotalAmount += Math.Round(decimal.Parse(hdnNetTotal.Value), 2);
            PoMaster.VatAmount = Math.Round(vat, 2);
            PoMaster.NBTAmount = Math.Round(nbt, 2);
            PoMaster.TotalAmount = Math.Round(tot, 2);
            PoMaster.IsDerivedFromPo = OldPo.PoID;
            PoMaster.DerivingReason = hdnRemarks.Value.ProcessString();
            PoMaster.QuotationFor = OldPo.QuotationFor;
            PoMaster.Remarks = txtRemarks.Text == null ? "" : txtRemarks.Text;

            if (OldPo.IsDerived == 1 && OldPo.IsDerivedType == 1)
            {
                PoMaster.ParentApprovedUser = OldPo.ParentApprovedUser;
            }
            else
            {
                PoMaster.ParentApprovedUser = OldPo.ApprovedBy != null && OldPo.ApprovedBy != "" ? int.Parse(OldPo.ApprovedBy) : 0;
            }

            PoMaster.PoDetails = new List<PODetails>();


            for (int i = 0; i < gvPoItems.Rows.Count; i++)
            {

                PODetails PoDetail = new PODetails();
                if ((gvPoItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text != "" && (decimal.Parse((gvPoItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text).ToString()) != "0.00")
                {
                    PoDetail.QuotationItemId = int.Parse(gvPoItems.Rows[i].Cells[1].Text);
                    PoDetail.ItemId = int.Parse(gvPoItems.Rows[i].Cells[2].Text);
                    PoDetail.ItemName = gvPoItems.Rows[i].Cells[3].Text;
                    PoDetail.MeasurementId = int.Parse(gvPoItems.Rows[i].Cells[16].Text);
                    //PoDetail.ActualPrice = (gvPoItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text == "" ? Math.Round(decimal.Parse((gvPoItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text), 2) : Math.Round(decimal.Parse((gvPoItems.Rows[i].FindControl("txtActualPrice") as TextBox).Text), 2);
                    PoDetail.ItemPrice = Math.Round(decimal.Parse((gvPoItems.Rows[i].FindControl("txtUnitPrice") as TextBox).Text), 2);
                    PoDetail.Quantity = Math.Round(decimal.Parse((gvPoItems.Rows[i].FindControl("txtQuantity") as TextBox).Text), 2);
                    PoDetail.VatAmount = Math.Round(decimal.Parse((gvPoItems.Rows[i].FindControl("txtVat") as TextBox).Text), 2);
                    PoDetail.NbtAmount = Math.Round(decimal.Parse((gvPoItems.Rows[i].FindControl("txtNbt") as TextBox).Text), 2);
                    PoDetail.TotalAmount = Math.Round(decimal.Parse((gvPoItems.Rows[i].FindControl("txtNetTotal") as TextBox).Text), 2);
                    //PoDetail.HasNbt = (gvPoItems.Rows[i].FindControl("chkNbt") as CheckBox).Checked == true ? 1 : 0;
                    PoDetail.NbtCalculationType = (gvPoItems.Rows[i].FindControl("rdoNbt204") as RadioButton).Checked == true ? 1 : 2;
                    PoDetail.HasVat = (gvPoItems.Rows[i].FindControl("chkVat") as CheckBox).Checked == true ? 1 : 0;
                    PoDetail.SupplierMentionedItemName = gvPoItems.Rows[i].Cells[4].Text;

                    PoMaster.PoDetails.Add(PoDetail);
                }

            }

            List<int> result = pOMasterController.UpdatePO(PoMaster, int.Parse(Session["UserId"].ToString()));

            if (result.Count > 0)
            {

                if (ViewState["PrId"] != null)
                {
                    int results = PrControllerV2.DeleteFromPO(int.Parse(ViewState["PrId"].ToString()), ViewState["ItemIds"] as List<int>, int.Parse(Session["UserId"].ToString()));
                }

                // sendEmails(result[0], int.Parse(Session["CompanyId"].ToString()));
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'PO has been modified to a new PO with the code PO" + result[1] + "'}).then((result) => { $('.btnUpdateCl').addClass('hidden'); $('.loader').addClass('hidden');  window.location = 'ViewPO.aspx?PoId=" + result[0] + "' }); });   </script>", false);

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Updating PO', showConfirmButton: false,timer: 1500}); $('.loader').addClass('hidden');  $('.btnUpdateCl').removeClass('hidden'); });   </script>", false);
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