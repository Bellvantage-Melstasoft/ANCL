using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class GenerateGRN : System.Web.UI.Page
    {
        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController poDetailsController = ControllerFactory.CreatePODetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();

       // static string UserId = string.Empty;
        //private string PRId = string.Empty;

       // private string UserDept = string.Empty;
       // private string OurRef = string.Empty;
        //private string PrCode = string.Empty;
        //private string RequestedDate = string.Empty;
        //private string UserRef = string.Empty;
        //private string RequesterName = string.Empty;

        //public string vatAmount = string.Empty;
        //public string nbtAmount = string.Empty;
        //public string totalAmount = string.Empty;
        //public string quantity = string.Empty;

       // int CompanyId = 0;
       // int PoId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                //((BiddingAdmin)Page.Master).subTabValue = "CustomerViewApprovedPurchaseOrder.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "generateGRNLink";

              //  CompanyId = int.Parse(Session["CompanyId"].ToString());
               // UserId = Session["UserId"].ToString();

                if (Session["PoId"] != null)
                {
                    ViewState["PoId"] = int.Parse(Session["PoId"].ToString());
                }
                else
                {
                    Response.Redirect("CustomerApprovedPurchaseOrder.aspx");
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

                    POMaster pOMaster = pOMasterController.GetPoMasterObjByPoId(int.Parse(ViewState["PoId"].ToString()));
                    PR_Master pR_Master = pR_MasterController.FetchApprovePRDataByPRId(pOMaster.BasePr);

                    lblRefNo.Text = pR_Master.OurReference;
                    lblPOCode.Text = pOMaster.POCode;
                    lblSupplierName.Text = pOMaster._Supplier.SupplierName; ;
                    lblAddress.Text = pOMaster._Supplier.Address1 + "," + pOMaster._Supplier.Address2;
                    lblCompanyName.Text = pOMaster._companyDepartment.DepartmentName;
                    lblVatNo.Text = pOMaster._companyDepartment.VatNo;
                    lblPhoneNo.Text = pOMaster._companyDepartment.PhoneNO;
                    lblFaxNo.Text = pOMaster._companyDepartment.FaxNO;




                    lblCompanyAddress.Text = pOMaster._companyDepartment.Address2 != "" ? pOMaster._companyDepartment.Address1 + ",</br>" + pOMaster._companyDepartment.Address2 + "," : pOMaster._companyDepartment.Address1 + ",";
                    lblCity.Text = pOMaster._companyDepartment.City != "" ? pOMaster._companyDepartment.City + "," : pOMaster._companyDepartment.City;
                    lblCountry.Text = pOMaster._companyDepartment.Country != "" ? pOMaster._companyDepartment.Country + "." : pOMaster._companyDepartment.Country;
                    //lblCompanyAddress.Text = pOMaster._companyDepartment.Address1;
                    //lblCity.Text = pOMaster._companyDepartment.City;
                    //lblCountry.Text = pOMaster._companyDepartment.Country+".";


                    ViewState["totalAmount"] = pOMaster.TotalAmount.ToString("n");
                    ViewState["vatAmount"] = pOMaster.VatAmount.ToString("n");
                    ViewState["nbtAmount"]  = pOMaster.NBTAmount.ToString("n");
                    ViewState["quantity"]  = pOMaster.Quantity.ToString("n");

                    List<PODetails> _poDetails = new List<PODetails>();
                    List<GrnDetails> _grnDetails = new List<GrnDetails>();

                    _poDetails = poDetailsController.GetPODetailsApproved(int.Parse(Session["CompanyId"].ToString()));
                    _grnDetails = gRNDetailsController.GetGrnDetails(int.Parse(ViewState["PoId"].ToString()));
                    if (_grnDetails.Count == 0)
                    {
                        gvPurchaseOrderItems.DataSource = _poDetails.Where(x => x.PoId == (int.Parse(ViewState["PoId"].ToString())));
                        gvPurchaseOrderItems.DataBind();
                    }

                    else
                    {
                        List<GrnToGenerateItemList> grnToGenerateItemList = new List<GrnToGenerateItemList>();

                        foreach (var itemPo in _poDetails.Where(c => c.PoId == int.Parse(ViewState["PoId"].ToString())))
                        {
                            foreach (var itemGrn in _grnDetails)
                            {
                                //var result = from item in _grnDetails
                                //             group _grnDetails by item.ItemId into grp
                                //             let sum = _grnDetails.Where(x => x.GrnId == itemGrn.GrnId && x.ItemId == itemGrn.ItemId).Sum(x => x.Quantity)
                                //             select new
                                //             {
                                //                 ItemId = grp.Key,
                                //                 Sum = sum,
                                //             };
                                var result = from item in _grnDetails
                                             group _grnDetails by item.ItemId into grp
                                             let sum = _grnDetails.Where(x => x.ItemId == grp.Key && x.PoId == itemPo.PoId).Sum(x => x.Quantity)
                                             select new
                                             {
                                                 ItemId = grp.Key,
                                                 PoId = itemPo.PoId,
                                                 Sum = sum,
                                             };

                                if (itemPo.PoId == itemGrn.PoId && itemGrn.ItemId == itemPo.ItemId)
                                {
                                    decimal poItemQty = itemPo.Quantity;
                                    decimal grnItemQty = itemGrn.Quantity;

                                    string sumQtu = string.Empty;
                                    string itemidlstQty = string.Empty;

                                    foreach (var itemresult in result)
                                    {
                                        if (itemGrn.ItemId == itemresult.ItemId)
                                        {
                                            sumQtu = itemresult.Sum.ToString();
                                            itemidlstQty = itemresult.ItemId.ToString();
                                            break;
                                        }
                                    }

                                    decimal balanceQuantity = poItemQty - decimal.Parse(sumQtu);
                                    if (itemGrn.IsGrnApproved == 2 && itemGrn.IsGrnRaised == 2)
                                    {
                                        balanceQuantity = balanceQuantity + itemGrn.Quantity;
                                        sumQtu = sumQtu + itemGrn.Quantity;
                                        decimal restQty = decimal.Parse(sumQtu);
                                        grnToGenerateItemList.Add(new GrnToGenerateItemList(itemPo.PoId, itemPo.CategoryName, itemGrn.ItemId, itemPo.SubCategoryName, itemPo.ItemName, ((itemPo.VatAmount) / poItemQty) * restQty, ((itemPo.NbtAmount) / poItemQty) * restQty, ((itemPo.TotalAmount) / poItemQty) * restQty, ((itemPo.CustomizedVat) / poItemQty) * restQty, ((itemPo.CustomizedNbt) / poItemQty) * restQty, ((itemPo.CustomizedTotalAmount) / poItemQty) * restQty, ((itemPo.CustomizedAmount)), itemPo.ItemPrice, balanceQuantity, itemPo.IsCustomizedAmount, itemGrn.AddToGrnCount));
                                    }

                                    if (poItemQty != decimal.Parse(sumQtu))
                                    {
                                        decimal restQty = poItemQty - decimal.Parse(sumQtu);


                                        grnToGenerateItemList.Add(new GrnToGenerateItemList(itemPo.PoId, itemPo.CategoryName, itemGrn.ItemId, itemPo.SubCategoryName, itemPo.ItemName, ((itemPo.VatAmount) / poItemQty) * restQty, ((itemPo.NbtAmount) / poItemQty) * restQty, ((itemPo.TotalAmount) / poItemQty) * restQty, ((itemPo.CustomizedVat) / poItemQty) * restQty, ((itemPo.CustomizedNbt) / poItemQty) * restQty, ((itemPo.CustomizedTotalAmount) / poItemQty) * restQty, ((itemPo.CustomizedAmount)), itemPo.ItemPrice, balanceQuantity, itemPo.IsCustomizedAmount, itemGrn.AddToGrnCount));
                                    }
                                }
                            }
                        }
                        var distinctItems = grnToGenerateItemList.GroupBy(x => x.ItemId).Select(y => y.First());
                        gvPurchaseOrderItems.DataSource = distinctItems;
                        gvPurchaseOrderItems.DataBind();
                    }
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
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerViewApprovedPurchaseOrder.aspx");
        }

        //-----------2 : Rejected 1: Approved
        protected void btnGenerateGrn_Click(object sender, EventArgs e)
        {
            try
            {
                POMaster pOMaster = pOMasterController.GetPoMasterObjByPoId(int.Parse(ViewState["PoId"].ToString()));
                if (pOMaster.PoID != 0)
                {
                    int generateGrnmasterId = grnController.SaveGrnMaster(int.Parse(ViewState["PoId"].ToString()), int.Parse(Session["CompanyId"].ToString()), pOMaster.SupplierId, DateTime.ParseExact(GoodreceivedDate.Value, "dd/MM/yy", CultureInfo.InvariantCulture), 0, Session["UserId"].ToString(), LocalTime.Now, txtGRNote.Text, txtInvoiceNo.Text);
                    if (generateGrnmasterId > 0)
                    {

                        DataTable dt = new DataTable();
                        dt.Columns.Add("ITEM_ID");
                        dt.Columns.Add("EXTRA_QTY");
                        dt.Columns.Add("NBT");
                        dt.Columns.Add("VAT");
                        dt.Columns.Add("TOTAL");

                        decimal totalVat = 0, totalNbt = 0, totAmount = 0;

                        for (int i = 0; i < gvPurchaseOrderItems.Rows.Count; i++)
                        {
                            int poId = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[0].Text);
                            int itemid = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[2].Text);
                            int addGrnCount = int.Parse(gvPurchaseOrderItems.Rows[i].Cells[19].Text);
                            Label itmPriz = (gvPurchaseOrderItems.Rows[i].FindControl("txtApproved")) as Label;
                            decimal itemPrice = decimal.Parse(itmPriz.Text);

                            int requestedQty = (int)decimal.Parse(gvPurchaseOrderItems.Rows[i].Cells[13].Text);

                            TextBox tb = (gvPurchaseOrderItems.Rows[i].FindControl("txtQty")) as TextBox;
                            decimal Qty = 0;
                            if (tb.Text == "")
                            {
                                Qty = 1;
                            }
                            else
                            {
                                Qty = decimal.Parse(tb.Text);
                            }

                            TextBox itemVat = (gvPurchaseOrderItems.Rows[i].FindControl("txtApprovedVat")) as TextBox;
                            decimal itemVatAmount = (decimal.Parse(itemVat.Text)/requestedQty)*Qty;

                            // Label itemNbt = (gvPurchaseOrderItems.Rows[i].FindControl("txtApprovedNbt")) as Label;

                            TextBox itemNbt = (gvPurchaseOrderItems.Rows[i].FindControl("txtApprovedNbt")) as TextBox;

                            decimal itemNbtAmount = (decimal.Parse(itemNbt.Text) / requestedQty)*Qty;

                            //  decimal nbtval = decimal.Parse( gvPurchaseOrderItems.Rows[i].Cells[17].tex

                            TextBox itemTotal = (gvPurchaseOrderItems.Rows[i].FindControl("txtApprovedAmount")) as TextBox;
                            decimal itemTotalAmount = (decimal.Parse(itemTotal.Text) / requestedQty)*Qty;

                            if (Qty > requestedQty)
                            {
                                totalVat += decimal.Parse(itemVat.Text) / requestedQty * ((int)Qty - requestedQty);
                                totalNbt += decimal.Parse(itemNbt.Text) / requestedQty * ((int)Qty - requestedQty);
                                totAmount += decimal.Parse(itemTotal.Text) / requestedQty * ((int)Qty - requestedQty);

                                dt.Rows.Add(itemid, (int)Qty - requestedQty, decimal.Parse(itemNbt.Text) / requestedQty * ((int)Qty - requestedQty),
                                    decimal.Parse(itemVat.Text) / requestedQty * ((int)Qty - requestedQty),
                                    decimal.Parse(itemTotal.Text) / requestedQty * ((int)Qty - requestedQty));

                                int saveGRNDetails = gRNDetailsController.SaveGrnDetails(generateGrnmasterId, poId, itemid, itemPrice, requestedQty,
                                    decimal.Parse(itemTotal.Text) / requestedQty * ((int)Qty - requestedQty),
                                    decimal.Parse(itemVat.Text) / requestedQty * ((int)Qty - requestedQty), 
                                    decimal.Parse(itemNbt.Text) / requestedQty * ((int)Qty - requestedQty));

                            }
                            else
                            {
                                int saveGRNDetails = gRNDetailsController.SaveGrnDetails(generateGrnmasterId, poId, itemid, itemPrice, Qty, itemTotalAmount, itemVatAmount, itemNbtAmount);

                            }

                            if (addGrnCount == 1)
                            {
                                gRNDetailsController.UpdateGrnReectAddToGrnCount(generateGrnmasterId, poId, itemid, 2);
                            }

                        }

                        if (dt.Rows.Count > 0)
                        {
                            int result = grnController.GenrateCoveringPr(int.Parse(ViewState["PoId"].ToString()), pOMaster.BasePr, int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()), totalVat, totalNbt, totAmount, dt);

                            if (result > 0)
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title:'SUCCESS',text: 'GRN Genrated with Covering Purchase Requests for Extra Quantity', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerViewApprovedPurchaseOrder.aspx'}); });   </script>", false);
                            else
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'ERROR',text: 'Error on creating Covering Purchase Requests for Extra Quantity', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerViewApprovedPurchaseOrder.aspx'}); });   </script>", false);

                        }
                        else
                        {

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title:'SUCCESS',text: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'CustomerViewApprovedPurchaseOrder.aspx'}); });   </script>", false);
                            //Response.Redirect("CustomerViewApprovedPurchaseOrder.aspx", false);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public class GrnToGenerateItemList
        {
            public GrnToGenerateItemList(int PoID, string CategoryName, int ItemId, string SubCategoryName, string ItemName, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, decimal CustomizedVat, decimal CustomizedNbt, decimal CustomizedTotalAmount, decimal CustomizedAmount, decimal ItemPrice, decimal Quantity, int IsCustomizedAmount, int AddToGrnCount)
            {
                poID = PoID;
                categoryName = CategoryName;
                itemId = ItemId;
                subCategoryName = SubCategoryName;
                itemName = ItemName;
                vatAmount = VatAmount;
                nbtAmount = NbtAmount;
                totalAmount = TotalAmount;
                customizedVat = CustomizedVat;
                customizedNbt = CustomizedNbt;
                customizedTotalAmount = CustomizedTotalAmount;
                customizedAmount = CustomizedAmount;
                itemPrice = ItemPrice;
                quantity = Quantity;
                isCustomizedAmount = IsCustomizedAmount;
                addToGrnCount = AddToGrnCount;
            }

            private int poID;
            private string categoryName;
            private int itemId;
            private string subCategoryName;
            private string itemName;
            private decimal vatAmount;
            private decimal nbtAmount;
            private decimal totalAmount;
            private decimal customizedVat;
            private decimal customizedNbt;
            private decimal customizedTotalAmount;
            private decimal customizedAmount;
            private decimal itemPrice;
            private decimal quantity;
            private int isCustomizedAmount;
            private int addToGrnCount;

            public int PoID
            {
                get { return poID; }
                set { poID = value; }
            }

            public string CategoryName
            {
                get { return categoryName; }
                set { categoryName = value; }
            }

            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            public string SubCategoryName
            {
                get { return subCategoryName; }
                set { subCategoryName = value; }
            }

            public string ItemName
            {
                get { return itemName; }
                set { itemName = value; }
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

            public decimal CustomizedVat
            {
                get { return customizedVat; }
                set { customizedVat = value; }
            }

            public decimal CustomizedNbt
            {
                get { return customizedNbt; }
                set { customizedNbt = value; }
            }

            public decimal CustomizedTotalAmount
            {
                get { return customizedTotalAmount; }
                set { customizedTotalAmount = value; }
            }

            public decimal CustomizedAmount
            {
                get { return customizedAmount; }
                set { customizedAmount = value; }
            }

            public decimal ItemPrice
            {
                get { return itemPrice; }
                set { itemPrice = value; }
            }

            public decimal Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }

            public int IsCustomizedAmount
            {
                get { return isCustomizedAmount; }
                set { isCustomizedAmount = value; }
            }

            public int AddToGrnCount
            {
                get { return addToGrnCount; }
                set { addToGrnCount = value; }
            }
        }
    }
}