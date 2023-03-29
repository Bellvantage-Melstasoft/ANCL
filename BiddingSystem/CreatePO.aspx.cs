using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Services;
using System.Web.Script.Services;
using System.Data;
using System.IO;
using System.Globalization;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class CreatePO : System.Web.UI.Page
    {
        #region properties
      //  static int PrId = 0;
       // static int BidId = 0;
      //  static int UserId = 1005;
       // static int CompanyId = 4;
       // static PrMasterV2 PrMaster;
        #endregion

        #region controllers
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        POMasterController poMasterController = ControllerFactory.CreatePOMasterController();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
        QuotationImageController quotationImageController = ControllerFactory.CreateQuotationImageController();
        SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPrForPoCreation.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createPoLink";

              //  UserId = int.Parse(Session["UserId"].ToString());
             //   CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 15) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                        Session["PrId"] = int.Parse(Request.QueryString.Get("PrId"));
                       var PrMaster = pr_MasterController.GetPrForPoCreation(int.Parse(Session["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()));
                        ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);


                        lblPRNo.Text = "PR" + PrMaster.PrCode;
                        lblCreatedOn.Text = PrMaster.CreatedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                        lblCreatedBy.Text = PrMaster.CreatedByName;
                        lblRequestBy.Text = PrMaster.CreatedByName;
                        lblRequestFor.Text = PrMaster.RequiredFor;
                        lblExpenseType.Text = (PrMaster.ExpenseType == 1) ? "Capital Expense" : "Operational Expense";
                        lblDepartment.Text = !String.IsNullOrEmpty(PrMaster.SubDepartmentName) ? PrMaster.SubDepartmentName : "Not Found";
                        lblWarehouse.Text = PrMaster.WarehouseName;
                        lblMrnId.Text = PrMaster.MrnId != 0 ? "MRN" + PrMaster.MrnCode : "Not From MRN";

                        LoadGV();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void LoadGV()
        {
            try
            {
                List<TabulationDetail> items = new List<TabulationDetail>();

                for (int i = 0; i < new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Count; i++)
                {
                    items.AddRange(new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids[i].SelectedTabulation.TabulationDetails);
                    
                }

                gvItems.DataSource = items;
                gvItems.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void btnCreatePO_Click(object sender, EventArgs e)
        //{
        //    List <int> BidIds = new List<int>();
        //    List<POMaster> POMasters = new List<POMaster>();

        //    for (int i = 0; i < gvItems.Rows.Count; i++)
        //    {
        //        if (((gvItems.Rows[i].FindControl("CheckBox1") as CheckBox).Checked))
        //        {
        //            BidIds.Add(int.Parse(gvItems.Rows[i].Cells[3].Text));

        //            POMaster PoMaster = new POMaster();
        //            PoMaster.QuotationId = int.Parse(gvItems.Rows[i].Cells[2].Text);
        //            PoMaster.QuotationApprovedBy = PrMaster.Bids.Find(b => b.BidId == int.Parse(gvItems.Rows[i].Cells[3].Text)).QuotationApprovedBy;
        //            PoMaster.QuotationConfirmedBy = PrMaster.Bids.Find(b => b.BidId == int.Parse(gvItems.Rows[i].Cells[3].Text)).QuotationConfirmedBy;
        //            PoMaster.DepartmentId = int.Parse(Session["CompanyId"].ToString());
        //            PoMaster.BasePr = PrId;
        //            PoMaster.SupplierId = int.Parse(gvItems.Rows[i].Cells[4].Text);
        //            PoMaster.CreatedBy = Session["UserId"].ToString();
        //            PoMaster.VatAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[8].Text), 2);
        //            PoMaster.NBTAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[7].Text), 2);
        //            PoMaster.TotalAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[9].Text), 2);


        //            List<PODetails> PoDetails = new List<PODetails>();


        //            GridView gvQuotationItems = gvItems.Rows[i].FindControl("gvQuotationItems") as GridView;

        //            for (int j = 0; j < gvQuotationItems.Rows.Count; j++)
        //            {
        //                PODetails PoDetail = new PODetails();

        //                PoDetail.QuotationItemId = int.Parse(gvQuotationItems.Rows[j].Cells[0].Text);
        //                PoDetail.ItemId = int.Parse(gvQuotationItems.Rows[j].Cells[1].Text);
        //                PoDetail.ItemPrice = Math.Round(decimal.Parse(gvQuotationItems.Rows[j].Cells[4].Text), 2);
        //                PoDetail.Quantity = int.Parse(gvQuotationItems.Rows[j].Cells[3].Text);
        //                PoDetail.VatAmount = Math.Round(decimal.Parse(gvQuotationItems.Rows[j].Cells[7].Text), 2);
        //                PoDetail.NbtAmount = Math.Round(decimal.Parse(gvQuotationItems.Rows[j].Cells[6].Text), 2);
        //                PoDetail.TotalAmount = Math.Round(decimal.Parse(gvQuotationItems.Rows[j].Cells[8].Text), 2);

        //                PoDetails.Add(PoDetail);
        //            }
        //            PoMaster.PoDetails = PoDetails;

        //            POMasters.Add(PoMaster);


        //        }
        //      //  var IsUploaded = pr_MasterController.CheckfileUploadedofteccommit(BidId, int qutationId);
        //    }

        //    if (POMasters.Count > 0)
        //    {

        //        try
        //        {

        //            int result = poMasterController.SavePO(POMasters, UserId);
        //            //int result = 0;
        //            int PoID = result;
        //            //itemCategoryApprovalController.InsertAuthorityToPOApprove(PoID, PrId, POMasters, PrMaster, CompanyId, UserId);

        //            if (result > 0)
        //            {
        //                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForPoCreation.aspx' }); });   </script>", false);

        //            }
        //            else
        //            {
        //                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Creating PO', showConfirmButton: false,timer: 1500}); });   </script>", false);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Assigning Authorites for Approval PO', showConfirmButton: false,timer: 1500}); });   </script>", false);
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Please Select a Quotation', showConfirmButton: false,timer: 1500}); });   </script>", false);
        //    }



        //}


        protected void btnCreatePO_Click(object sender, EventArgs e)
        {
            List<POMaster> POMasters = new List<POMaster>();
            int supplierId = 0;

            for (int i = 0; i < gvItems.Rows.Count; i++)
            {
                if (((gvItems.Rows[i].FindControl("CheckBox1") as CheckBox).Checked))
                {
                    if (int.Parse(gvItems.Rows[i].Cells[3].Text) != supplierId)
                    {
                        supplierId = int.Parse(gvItems.Rows[i].Cells[3].Text);

                        POMaster PoMaster = new POMaster();
                        PoMaster.DepartmentId = int.Parse(Session["CompanyId"].ToString());
                        PoMaster.BasePr = int.Parse(Session["PrId"].ToString());
                        PoMaster.SupplierId = int.Parse(gvItems.Rows[i].Cells[3].Text);
                        PoMaster.CreatedBy = Session["UserId"].ToString();
                        PoMaster.VatAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[14].Text), 2);
                        PoMaster.NBTAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[13].Text), 2);
                        PoMaster.TotalAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[15].Text), 2);

                        List<PODetails> PoDetails = new List<PODetails>();

                        PODetails PoDetail = new PODetails();

                        PoDetail.QuotationItemId = int.Parse(gvItems.Rows[i].Cells[6].Text);
                        PoDetail.ItemId = int.Parse(gvItems.Rows[i].Cells[7].Text);
                        PoDetail.ItemPrice = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[11].Text), 2);
                        PoDetail.Quantity = int.Parse(gvItems.Rows[i].Cells[11].Text);
                        PoDetail.VatAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[15].Text), 2);
                        PoDetail.NbtAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[14].Text), 2);
                        PoDetail.TotalAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[16].Text), 2);
                        PoDetail.TabulationId= int.Parse(gvItems.Rows[i].Cells[2].Text);
                        PoDetail.TabulationDetailId = int.Parse(gvItems.Rows[i].Cells[1].Text);

                        PoDetails.Add(PoDetail);

                        PoMaster.PoDetails = PoDetails;

                        POMasters.Add(PoMaster);
                    }
                    else
                    {
                        POMaster PoMaster = POMasters.Find(po => po.SupplierId == int.Parse(gvItems.Rows[i].Cells[3].Text));

                        PoMaster.VatAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[14].Text), 2);
                        PoMaster.NBTAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[13].Text), 2);
                        PoMaster.TotalAmount += Math.Round(decimal.Parse(gvItems.Rows[i].Cells[15].Text), 2);

                        PODetails PoDetail = new PODetails();

                        PoDetail.QuotationItemId = int.Parse(gvItems.Rows[i].Cells[6].Text);
                        PoDetail.ItemId = int.Parse(gvItems.Rows[i].Cells[7].Text);
                        PoDetail.ItemPrice = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[11].Text), 2);
                        PoDetail.Quantity = int.Parse(gvItems.Rows[i].Cells[11].Text);
                        PoDetail.VatAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[15].Text), 2);
                        PoDetail.NbtAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[14].Text), 2);
                        PoDetail.TotalAmount = Math.Round(decimal.Parse(gvItems.Rows[i].Cells[16].Text), 2);
                        PoDetail.TabulationId = int.Parse(gvItems.Rows[i].Cells[2].Text);
                        PoDetail.TabulationDetailId = int.Parse(gvItems.Rows[i].Cells[1].Text);

                        PoMaster.PoDetails.Add(PoDetail);

                    }

                }
            }

            if (POMasters.Count > 0)
            {
                int result = poMasterController.SavePO(POMasters, int.Parse(Session["UserId"].ToString()));

                if (result > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrForPoCreation.aspx' }); });   </script>", false);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error On Creating PO', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Please Select a Quotation', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }

        }

        protected void btnViewAttachments_Click(object sender, EventArgs e)
        {
         
            int tabulationId= int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[2].Text);
            int qutationId= int.Parse(((sender as Button).NamingContainer as GridViewRow).Cells[5].Text);
            var qutaion= supplierQuotationController.GetSupplierQuotations(new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Where(x=>x.SelectedTabulation.TabulationId== tabulationId).FirstOrDefault().BidId);

            gvDocs.DataSource = supplierBiddingFileUploadController.GetFilesByQuotationId(qutationId);
            gvDocs.DataBind();

            gvImages.DataSource = quotationImageController.GetQuotationImages(qutationId);
            gvImages.DataBind();
            txtTermsAndConditions.Text = qutaion.Where(x=>x.QuotationId == qutationId).FirstOrDefault().TermsAndCondition;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlAttachments').modal('show') });   </script>", false);
        }

    }
}