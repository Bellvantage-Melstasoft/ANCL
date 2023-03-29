using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Data;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class UpdateSupplierBidBondDetails : System.Web.UI.Page
    {

       // static Bidding bid;
       // static int BidId = 0;
       // static int UserId = 0;
      //  static int CompanyId = 0;

        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        SupplierQuotationController quotationController = ControllerFactory.CreateSupplierQuotationController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        SupplierBidBondDetailsController supplierBidBondDetailController = ControllerFactory.CreateSupplierBidBondDetailsController();
      //  public static List<SupplierBidBondDetails> supplierAllBidBondDetails = new List<SupplierBidBondDetails>();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefManualBids";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabManualBids";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForManualQuotationSubmission.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ManualBidsLink";


              //  UserId = int.Parse(Session["UserId"].ToString());
               // CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 7, 3) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                       ViewState["BidId"]  = int.Parse(Request.QueryString.Get("BidId"));
                        var bid = biddingController.GetBidDetailsForQuotationSubmission(int.Parse(ViewState["BidId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                        ViewState["bid"] = new JavaScriptSerializer().Serialize(bid);
                      var supplierAllBidBondDetails = supplierBidBondDetailController.getSupplierBidBondDetailsByBidId(int.Parse(ViewState["BidId"].ToString()));
                        ViewState["supplierAllBidBondDetails"] = new JavaScriptSerializer().Serialize(supplierAllBidBondDetails);
                        LoadAllSuppliers();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void LoadAllSuppliers()
        {
            try
            {
                List<int> CategoryIds = new List<int>();
                for(int i=0; i< new JavaScriptSerializer().Deserialize<Bidding>(ViewState["bid"].ToString()).BiddingItems.Count;i++)
                {
                    CategoryIds.Add(new JavaScriptSerializer().Deserialize<Bidding>(ViewState["bid"].ToString()).BiddingItems[i].CategoryId);
                }

                CategoryIds=CategoryIds.Distinct().ToList();
                var supplierAllBidBondDetails = new JavaScriptSerializer().Deserialize<List<SupplierBidBondDetails>>(ViewState["supplierAllBidBondDetails"].ToString());
                List<Supplier> supplier = new List<Supplier>();
                if (new JavaScriptSerializer().Deserialize<Bidding>(ViewState["bid"].ToString()).BidOpenTo == 1)
                    supplier = supplierController.GetApprovedSuppliersForQuotationSubmission(int.Parse(Session["CompanyId"].ToString()), CategoryIds);
                else
                    supplier = supplierController.GetAllSuppliersForQuotationSubmission(CategoryIds);

                if (supplierAllBidBondDetails.Count != 0)
                {
                    supplier = supplier.Where(x => supplierAllBidBondDetails.Any(t => t.Supplier_Id == x.SupplierId)).ToList();
                }

                ddlSuppliers.DataSource = supplier;

                ddlSuppliers.DataTextField = "SupplierName";
                ddlSuppliers.DataValueField = "SupplierId";
                ddlSuppliers.DataBind();
                ddlSuppliers.Items.Insert(0, "Select A Supplier");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void ddlSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSupplierBidBondDetails();
        }


        private SupplierBidBondDetails LoadSupplierBidBondDetails()
        {
            SupplierBidBondDetails supplierBidBondDetails= new SupplierBidBondDetails();
            if (ddlSuppliers.SelectedIndex != -1)
            {
                if (ddlSuppliers.SelectedIndex != 0)
                {

                    supplierBidBondDetails = supplierBidBondDetailController.getSupplierBidBondDetails(int.Parse(ViewState["BidId"].ToString()), Convert.ToInt32(ddlSuppliers.SelectedValue));
                    if (supplierBidBondDetails.Bid_Id != 0)
                    {
                        txtBondNo.Text = supplierBidBondDetails.Bond_No;
                        txtBank.Text = supplierBidBondDetails.Bank;
                        txtBondAmount.Text = supplierBidBondDetails.Bond_Amount.ToString();
                        txtExpireDOB.Text = supplierBidBondDetails.Expire_Date_Of_Bond.ToString("MM/dd/yyyy");
                        txtReceiptNo.Text = supplierBidBondDetails.Receipt_No;
                        DisplayMessage("Record Exist, Update", false);
                        btnSubmit.Text = "Update";
                    }else
                    {
                        txtExpireDOB.Text = "";
                    }
                }
            }
            return supplierBidBondDetails;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int save = 0;
            if (ddlSuppliers.SelectedValue != "")
            {
                SupplierBidBondDetails model = new SupplierBidBondDetails
                {
                    Bid_Id = int.Parse(ViewState["BidId"].ToString()),
                    Supplier_Id = Convert.ToInt32(ddlSuppliers.SelectedValue),
                    Bond_No = txtBondNo.Text,
                    Bank = txtBank.Text,
                    Bond_Amount = Convert.ToDecimal(txtBondAmount.Text),
                    Expire_Date_Of_Bond = Convert.ToDateTime(txtExpireDOB.Text),
                    Receipt_No = txtReceiptNo.Text
                };
              save  = supplierBidBondDetailController.saveSupplierBidBondDetails(model);
            }
           
            if(save > 0)
            {
              //  ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(),"none", "<script> document.getElementById('successMessage').innerHTML = \"Supplier has been created successfully\"; $('#SuccessAlert').modal('show');   </script>", false);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                DisplayMessage("Updated successfully", false);
                ClearFields();
            }else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Updating Bid Bond Details failed', showConfirmButton: true,timer: 4000}); });   </script>", false);
                DisplayMessage("Updation Failed ", true);
            }
        }

        private void ClearFields()
        {
            ddlSuppliers.SelectedIndex = 0;
            txtBondNo.Text = "";
            txtBank.Text = "";
            txtBondAmount.Text = "";
            txtExpireDOB.Text = "";
            txtReceiptNo.Text = "";
        }

        private void DisplayMessage(string message, bool isError)
        {
            msg.Visible = true;
            if (isError)
            {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else
            {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }
    }
}