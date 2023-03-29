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
    public partial class CompanyViewApprovedGrnDetailsView2 : System.Web.UI.Page
    {
        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController gRNDetailsController = ControllerFactory.CreateGRNDetailsController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
        GRNDIssueNoteControllerInterface grndinController = ControllerFactory.CreateGRNDIssueNoteController();
        InventoryControllerInterface inventoryController = ControllerFactory.CreateInventoryController();

        //static string UserId = string.Empty;
      //  private string PRId = string.Empty;
      //  static int grndID = 0;
       // static int itemID = 0;
       // static int pendingQty = 0;
       // static int issuedQty = 0;
       // static int receivedQty = 0;
       // static decimal unitPrice = 0;
       // static decimal issuedStockValue = 0;
       // private string UserDept = string.Empty;
      //  private string OurRef = string.Empty;
       // private string PrCode = string.Empty;
       // private string RequestedDate = string.Empty;
     //   private string UserRef = string.Empty;
      //  private string RequesterName = string.Empty;

        //int CompanyId = 0;
        //int GrnID = 0;
       // int PoID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
               // ((BiddingAdmin)Page.Master).subTabValue = "CompanyViewApprovedGRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "CompanyViewApprovedGRNLink";

                //CompanyId = int.Parse(Session["CompanyId"].ToString());
             //   UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 11) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }

                if (Request.QueryString.Get("GrnID") != null && Request.QueryString.Get("PoID") != null)
                {
                   ViewState["GrnID"]  = int.Parse(Request.QueryString.Get("GrnID"));
                    ViewState["PoID"] = int.Parse(Request.QueryString.Get("PoID"));
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
                    loadData();
                    
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        private void loadData()
        {
            GrnMaster grnMaster = grnController.GetGrnMasterByGrnID(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()));
            PR_Master PR_Master = pR_MasterController.FetchApprovePRDataByPRId(grnMaster._POMaster.BasePr);
            CompanyDepartment companyDepartment = companyDepartmentController.GetDepartmentByDepartmentId(int.Parse(Session["CompanyId"].ToString()));
            lblPOCode.Text = grnMaster._POMaster.POCode;
            lblSupplierName.Text = grnMaster._Supplier.SupplierName; ;
            lblAddress.Text = grnMaster._Supplier.Address1 + "," + grnMaster._Supplier.Address2;
            lblRefNo.Text = PR_Master.OurReference;
            lblCompanyName.Text = companyDepartment.DepartmentName;
            lblCompanyAddress.Text = grnMaster._companyDepartment.Address2 != "" ? grnMaster._companyDepartment.Address1 + ",</br>" + grnMaster._companyDepartment.Address2 + "," : grnMaster._companyDepartment.Address1 + ",";
            lblCity.Text = grnMaster._companyDepartment.City != "" ? grnMaster._companyDepartment.City + "," : grnMaster._companyDepartment.City;
            lblCountry.Text = grnMaster._companyDepartment.Country != "" ? grnMaster._companyDepartment.Country + "." : grnMaster._companyDepartment.Country;
            lblDateNow.Text = LocalTime.Now.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
            //lblSubtotal.Text = grnMaster.TotalAmount.ToString("n");
            //lblVatTotal.Text = grnMaster._POMaster.VatAmount.ToString("n");
            //lblNbtTotal.Text = grnMaster._POMaster.NBTAmount.ToString("n");
            //lblTotal.Text = grnMaster._POMaster.TotalAmount.ToString("n");
            lblgrnComment.InnerText = grnMaster.GrnNote;
            lblReceiveddate.InnerText = grnMaster.GoodReceivedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
            lblInvoiceNo.Text = grnMaster.InvoiceNo;
            List<GrnDetails> l = gRNDetailsController.GrnDetialsGrnApprovedOnly(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            gvPurchaseOrderItems.DataSource = gRNDetailsController.GrnDetialsGrnApprovedOnly(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()), int.Parse(Session["CompanyId"].ToString()));
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

            List<GrnDetails> _grndetails = gRNDetailsController.GrnDetialsGrnApprovedOnly(int.Parse(ViewState["GrnID"].ToString()), int.Parse(ViewState["PoID"].ToString()), int.Parse(Session["CompanyId"].ToString()));

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

        protected void btnIssueToWarehouse_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);

            ViewState["grndID"] = int.Parse(row.Cells[0].Text);
            ViewState["itemID"] = int.Parse(row.Cells[4].Text);
            ViewState["unitPrice"]  = decimal.Parse(row.Cells[6].Text);
            ViewState["receivedQty"] = (int)decimal.Parse(row.Cells[7].Text);
            ViewState["issuedQty"]  = int.Parse(row.Cells[8].Text);
            ViewState["pendingQty"] = int.Parse(ViewState["receivedQty"].ToString())- int.Parse(ViewState["issuedQty"].ToString());

            gvWarehouseInventory.DataSource = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
            gvWarehouseInventory.DataBind();

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $(document).ready(function () { document.getElementById('receivedQtyShow').innerHTML = '" + int.Parse(ViewState["receivedQty"].ToString()) + "'; document.getElementById('issuedQtyShow').innerHTML = '" + int.Parse(ViewState["issuedQty"].ToString()) + "'; document.getElementById('pendingQtyShow').innerHTML = '" + int.Parse(ViewState["pendingQty"].ToString()) + "'; $('#mdlIssueStock').modal('show'); });  </script>", false);

        }

        protected void IssuedQty_TextChanged(object sender, EventArgs e)
        {

            GridView gv = (GridView)((GridViewRow)((TextBox)sender).NamingContainer).NamingContainer;
            
            int pending = int.Parse(ViewState["pendingQty"].ToString());

            foreach (GridViewRow row in gv.Rows)
            {
                TextBox txt = (TextBox)row.FindControl("IssuedQty");
                if (txt.Text != "0")
                {
                    int qty = 0;
                    if (txt.Text != "")
                        qty = int.Parse(txt.Text);
                    
                    if (pending > 0)
                    {

                        if (qty <= pending)
                        {
                            txt.Text = qty.ToString();
                            pending -= qty;
                        }
                        else
                        {
                            txt.Text = pending.ToString();
                            pending = 0;
                        }

                    }
                    else
                    {
                        txt.Text = "0";
                    }


                }
            }

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $(document).ready(function () { document.getElementById('receivedQtyShow').innerHTML = '" + int.Parse(ViewState["receivedQty"].ToString()) + "'; document.getElementById('issuedQtyShow').innerHTML = '" + int.Parse(ViewState["issuedQty"].ToString()) + "'; document.getElementById('pendingQtyShow').innerHTML = '" + int.Parse(ViewState["pendingQty"].ToString()) + "'; $('#mdlIssueStock').modal('show'); });  </script>", false);

        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {
            GridView gv = gvWarehouseInventory;
            int result = 0;
            int issued = 0;
            List<GRNDIssueNote> notes = new List<GRNDIssueNote>();
            List<WarehouseInventoryRaise> inventoryObjList = new List<WarehouseInventoryRaise>();

            foreach (GridViewRow row in gv.Rows)
            {
                TextBox txt = (TextBox)row.FindControl("IssuedQty");
                if (txt.Text != null || txt.Text != "" || txt.Text != "0")
                {

                    int qty = int.Parse(txt.Text);
                    if (qty > 0)
                    {
                        GRNDIssueNote note = new GRNDIssueNote();
                        WarehouseInventoryRaise inventoryObj = new WarehouseInventoryRaise();

                        int warehouse = int.Parse(row.Cells[0].Text);


                        note.GrndID = int.Parse(ViewState["grndID"].ToString());
                        note.ItemID = int.Parse(ViewState["itemID"].ToString());
                        note.WarehouseID = warehouse;
                        note.IssuedQty = qty;
                        note.IssuedBy = int.Parse(Session["UserId"].ToString());
                        note.IssuedStockValue = decimal.Parse(ViewState["unitPrice"].ToString()) * (decimal)qty;

                        inventoryObj.ItemID = int.Parse(ViewState["itemID"].ToString());
                        inventoryObj.WarehouseID = warehouse;
                        inventoryObj.RaisedQty = qty;
                        inventoryObj.RaisedBy = int.Parse(Session["UserId"].ToString());
                        inventoryObj.StockValue = decimal.Parse(ViewState["unitPrice"].ToString()) * (decimal)qty;
                        inventoryObj.GrndID = int.Parse(ViewState["grndID"].ToString());

                        notes.Add(note);
                        inventoryObjList.Add(inventoryObj);

                        issued += qty;
                    }
                }
            }

            result = grndinController.addNewNote(notes);

            if(result>0)
            {
                result = gRNDetailsController.updateGrndIssuedQty(int.Parse(ViewState["grndID"].ToString()), issued);

                if(result>0)
                {
                    result=inventoryController.raiseCompanyStockFromGRN(int.Parse(Session["CompanyId"].ToString()), int.Parse(ViewState["itemID"].ToString()), issued, decimal.Parse(ViewState["unitPrice"].ToString()) * (decimal)issued, int.Parse(Session["UserId"].ToString()));

                    if(result>0)
                    {
                        result = inventoryController.raiseWarehouseStockFromGRN(inventoryObjList);
                        if(result>0)
                        {
                            Response.Redirect("CompanyViewApprovedGrnDetailsView2.aspx?GrnID=" + int.Parse(ViewState["GrnID"].ToString()) + "&PoID=" + int.Parse(ViewState["PoID"].ToString()));
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on raising Warehouse Stock\"; $('#errorAlert').modal('show'); });   </script>", false);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on raising Company Stock\"; $('#errorAlert').modal('show'); });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Issued QTY\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on adding GRN item issue note\"; $('#errorAlert').modal('show'); });   </script>", false);
            }

        }
    }
}