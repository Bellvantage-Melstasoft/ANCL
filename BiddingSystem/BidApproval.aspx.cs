using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Script.Serialization;

namespace BiddingSystem
{
    public partial class BidApproval : System.Web.UI.Page
    {
        //static int PrId = 0;
       // static int UserId = 0;
        //static int CompanyId = 0;
       // static PrMasterV2 PrMaster;
       // static PrDetailsV2 PrDetail;

        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController PrMataterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController PrDetailsController = ControllerFactory.CreatePR_DetailController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_Replace_FileUploadController pr_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_SupportiveDocumentController pR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        PR_DetailHistoryController pR_DetailHistoryController = ControllerFactory.CreatePR_DetailHistoryController();
        AddItemController itemController = ControllerFactory.CreateAddItemController();
        BiddingController bidController = ControllerFactory.CreateBiddingController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        BiddingPlanController biddingPlanController = ControllerFactory.CreateBiddingPlanController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabId = "approveforBidOpeninggLink";

              //  UserId = int.Parse(Session["UserId"].ToString());
              //  CompanyId = int.Parse(Session["CompanyId"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 2) && companyLogin.Usertype != "S")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            RemarksLabel.Visible = false;

            if (!IsPostBack)
            {
                if (int.Parse(Session["UserId"].ToString()) != 0)
                {
                    try
                    {
                        ViewState["PrId"] = int.Parse(Request.QueryString.Get("PrId"));
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void LoadData()
        {
          var  PrMaster = PrMataterController.GetPrForBidApproval(int.Parse(ViewState["PrId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
            ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(PrMaster);

            if (PrMaster != null) {

                lblPRNo.Text = "PR-" + PrMaster.PrCode;
                lblCreatedOn.Text = PrMaster.CreatedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                lblCreatedBy.Text = PrMaster.CreatedByName;
                lblRequestBy.Text = PrMaster.CreatedByName;
                lblPrExpecteddate.Text = PrMaster.ExpectedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                lblRequestFor.Text = PrMaster.RequiredFor;
                lblExpenseType.Text = (PrMaster.ExpenseType == 1) ? "Capital Expense" : "Operational Expense";
                lblDepartment.Text = !String.IsNullOrEmpty(PrMaster.SubDepartmentName) ? PrMaster.SubDepartmentName : "Stores";
                lblWarehouse.Text = PrMaster.WarehouseName;
                lblMrnId.Text = PrMaster.MrnId != 0 ? "MRN-" + PrMaster.MrnCode : "Not From MRN";
                lblPurchaseType.Text = PrMaster.PurchaseType == 1 ? "Local" :"Import";
                if (PrMaster.Bids.Where(b => b.IsApproved == 0).Count() > 0)
                {
                    gvBidsForApproval.DataSource = PrMaster.Bids.Where(b => b.IsApproved == 0);
                    gvBidsForApproval.DataBind();
                    divPrDetails.Visible = true;
                }
                else
                {
                    divPrDetails.Visible = false;
                }

                gvBids.DataSource = PrMaster.Bids.Where(b => b.IsApproved != 0);
                gvBids.DataBind();
            }


        }

        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                gvBidItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }

        }



        protected void gvBidsForApproval_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int bidId = int.Parse(gvBidsForApproval.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;

                gvBidItems.DataSource = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }
        }



        protected void btnSubmitBid_Click(object sender, EventArgs e)
        {

            List<GridViewRow> selectedRows = gvBidsForApproval.Rows.OfType<GridViewRow>().Where(r => (r.Cells[0].FindControl("CheckBox1") as CheckBox).Checked == true).ToList();

            if (selectedRows.Count > 0)
            {
                List<int> bidIds = new List<int>();


                for (int i = 0; i < selectedRows.Count; i++)
                {
                    bidIds.Add(int.Parse(selectedRows[i].Cells[2].Text));
                }


                int result = bidController.ApproveBids(bidIds, txtRemarks.Text, int.Parse(Session["UserId"].ToString()));

                if (result > 0)
                {
                    ClearFields();
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on submitting bids', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Please Select At Least One Item', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
        }


        private void ClearFields()
        {
            LoadData();

        }

        protected void btnMoreBidItemDetails_Click(object sender, EventArgs e)
        {

            try
            {

                int PrdId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[2].Text);

              var  PrDetail = PrDetailsController.GetPrDetails(PrdId, int.Parse(Session["CompanyId"].ToString()));
                ViewState["PrDetail"] = new JavaScriptSerializer().Serialize(PrDetail);

                List<PrDetailsV2> PrDetails = new List<PrDetailsV2>();
                PrDetails.Add(PrDetail);
                gvBidMoreDetails.DataSource = PrDetails;
                gvBidMoreDetails.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnViewzReplacementPhotosOfBidItem_Click(object sender, EventArgs e)
        {
            try
            {
                gvViewReplacementImages.DataSource = new JavaScriptSerializer().Deserialize<PrDetailsV2>(ViewState["PrDetail"].ToString()).PrReplacementFileUploads;
                gvViewReplacementImages.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('hide'); $('#mdlReplacementImages').modal('show'); });   </script>", false);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void btnViewUploadPhotosOfBidItem_Click(object sender, EventArgs e)
        {

            try
            {
                gvUploadedPhotos.DataSource = new JavaScriptSerializer().Deserialize<PrDetailsV2>(ViewState["PrDetail"].ToString()).PrFileUploads;
                gvUploadedPhotos.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('hide'); $('#mdlStandardImages').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }

        protected void lblViewBomOfBidItem_Click(object sender, EventArgs e)
        {
            try
            {

                gvBOMDate.DataSource = new JavaScriptSerializer().Deserialize<PrDetailsV2>(ViewState["PrDetail"].ToString()).PrBoms;
                gvBOMDate.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('hide');  $('#mdlItemSpecs').modal('show'); });   </script>", false);

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnViewSupportiveDocumentsOfBidItem_Click(object sender, EventArgs e)
        {

            try
            {

                gvSupportiveDocuments.DataSource = new JavaScriptSerializer().Deserialize<PrDetailsV2>(ViewState["PrDetail"].ToString()).PrSupportiveDocuments;
                gvSupportiveDocuments.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBidMoreDetails').modal('hide'); $('#mdlSupportiveDocs').modal('show'); });   </script>", false);

            }
            catch (Exception)
            {

            }
        }

        protected void BtnBiddingPlan_Click(object sender, EventArgs e) {

            try {

                int bidId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[2].Text);
                List<BiddingPlan> bidingplan = biddingPlanController.GetBiddingPlanByID(bidId);

                dvBiddingplan.DataSource = bidingplan;
                dvBiddingplan.DataBind();
                //dvUpdateplan.Visible = false;
                //dvcomplete.Visible = false;
                //btnClear_Click(sender, e);
                //btneditcancel_Click(sender, e);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBiddingplan').modal('show');});   </script>", false);


            }
            catch (Exception ex) {

            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (txtRemarks.Text == "")
            {
                RemarksLabel.Visible = true;
            }
            else
            {
                List<GridViewRow> selectedRows = gvBidsForApproval.Rows.OfType<GridViewRow>().Where(r => (r.Cells[0].FindControl("CheckBox1") as CheckBox).Checked == true).ToList();

                if (selectedRows.Count > 0)
                {
                    List<int> bidIds = new List<int>();
                    List<int> prdIds = new List<int>();

                    for (int i = 0; i < selectedRows.Count; i++)
                    {
                        bidIds.Add(int.Parse(selectedRows[i].Cells[2].Text));
                        GridView gvBidItems = selectedRows[i].FindControl("gvBidItems") as GridView;

                        for(int j=0;j< gvBidItems.Rows.Count; j++)
                        {
                            prdIds.Add(int.Parse(gvBidItems.Rows[j].Cells[2].Text));
                        }

                    }

                    int result = bidController.RejecteBids(bidIds, prdIds, txtRemarks.Text, int.Parse(Session["UserId"].ToString()));

                    if (result > 0)
                    {
                        ClearFields();
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error on submitting bids', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Please Select At Least One Item', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
            }
        }

    }
}