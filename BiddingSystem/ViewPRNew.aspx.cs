using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class ViewPRNew : System.Web.UI.Page {
        PrControllerV2 prControllerV2 = ControllerFactory.CreatePrControllerV2();
        PRDetailsStatusLogController prDetailsStatusLogController = ControllerFactory.CreatePRDetailsStatusLogController();
        MRNDIssueNoteControllerInterface mrndIssueNoteController = ControllerFactory.CreateMRNDIssueNoteController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        PrCapexController prCapexController = ControllerFactory.CreatePrCapexController();

        protected void Page_Load(object sender, EventArgs e) {
            serializer.MaxJsonLength = Int32.MaxValue;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewMyPR.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewPRLink";
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                PrMasterV2 prMaster = prControllerV2.GetPrMasterToView(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["PRCode"] = "PR" + prMaster.PrCode;
                ViewState["CreatedBy"] = prMaster.CreatedBy;

                if(prMaster.Warehouse != null)
                {
                    lblWarehouseName.Text = prMaster.Warehouse.Location;
                    lblWarehouseAddress.Text = prMaster.Warehouse.Address;
                    lblWarehouseContact.Text = prMaster.Warehouse.PhoneNo;
                }
                

                lblCategory.Text = prMaster.PrCategoryName;
                lblSubCategory.Text = prMaster.PrSubCategoryName;
                lblExpectedDate.Text = (prMaster.ExpectedDate).ToString("dd/MM/yyyy");
                lblPrCode.Text = "PR-" + (prMaster.PrCode).ToString();

                lblDepartmentName.Text = "Stores";
                if (prMaster.SubDepartment.SubDepartmentID != 0)
                {
                    lblDepartmentName.Text = prMaster.SubDepartment.SubDepartmentName;
                    lblDepartmentContact.Text = prMaster.SubDepartment.PhoneNo;
                }

                if (prMaster.MrnMaster.MrnCode != null)
                {
                    pnlMrnApprovedByDetails.Visible = true;
                    lblMrnCreatedByName.Text = prMaster.MrnMaster.CreatedByName;
                    lblMrnCreatedDate.Text = prMaster.MrnMaster.CreatedDate.ToString("dd/MM/yyyy");
                    divMrnReferenceCode.Visible = true;
                    lblMrnReferenceCode.Text = prMaster.MrnMaster.MrnCode.ToString() != "" ? "MRN-" + prMaster.MrnMaster.MrnCode.ToString() : "No";
                    if (File.Exists(HttpContext.Current.Server.MapPath(prMaster.MrnMaster.ApprovedSignature)))
                        imgCreatedBySignatureMrn.ImageUrl = prMaster.MrnMaster.ApprovedSignature;
                    else
                        imgCreatedBySignatureMrn.ImageUrl = "UserSignature/NoSign.jpg";

                }
                lblPurchaseType.Text = prMaster.PurchaseType == 1 ? "Local" : "Import";
                lblPrCreatedByName.Text = prMaster.CreatedByName;
                lblPrCreatedDate.Text = prMaster.CreatedDate.ToString("dd/MM/yyyy");                

                lblExpenseType.Text = prMaster.ExpenseType == 1 ? "Capital Expense" : "Operational Expense";

                if (prMaster.IsPrApproved == 0) {
                    btnTerminatePR.Visible = prMaster.IsTerminated == 0 ? true : false;
                    lblPending.Visible = true;
                    btnModify.Visible = true;
                }
                else if (prMaster.IsPrApproved == 1) {
                    btnTerminatePR.Visible = false;
                    lblApproved.Visible = true;
                    btnModify.Visible = false;
                }
                else {
                    btnTerminatePR.Visible = false;
                    lblRejected.Visible = true;
                    btnModify.Visible = false;
                }
                if(prMaster.ExpenseType == 1) {
                    btnCapexDocs.Visible = true;
                }
                else {
                    btnCapexDocs.Visible = false;
                }

                if (prMaster.IsPrApproved != 0) {
                    pnlApprovedByDetails.Visible = true;
                    lblApprovedByName.Text = prMaster.PrApprovalByName;
                    lblApprovedDate.Text = prMaster.PrApprovalOn.ToString("dd/MM/yyyy");
                    lblRemark.Text = prMaster.PrApprvalRemarks;
                    if (prMaster.IsPrApproved == 1)
                    {
                        lblApprovalText.InnerHtml = "PR Approved By";
                    }
                    else if (prMaster.IsPrApproved == 2)
                    {
                        lblApprovalText.InnerHtml = "PR Rejected By";
                    }
                    if (File.Exists(HttpContext.Current.Server.MapPath(prMaster.ApprovedSignature)))
                        ImgApprovedBySignature.ImageUrl = prMaster.ApprovedSignature;
                    else
                        ImgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                }

                if (prMaster.ExpenseType == 1)
                {
                    spanPrExpense.Visible = true;
                    if (prMaster.IsExpenseApproved != 0)
                    {
                        pnlExpenseApprovalByDetails.Visible = true;
                        lblExpenseApprovedByName.Text = prMaster.ExpenseApprovalByName;
                        lblExpenseApprovedDate.Text = prMaster.ExpenseApproalOn.ToString("dd/MM/yyyy");
                        lblExpenseRemark.Text = prMaster.ExpenseRemarks;
                        if (prMaster.IsExpenseApproved == 1)
                        {
                            lblExpenseApprovalText.InnerHtml = "PR Expense Approved By";
                            lblExpenseApproved.Visible = true;
                        }
                        else if (prMaster.IsExpenseApproved == 2)
                        {
                            lblExpenseApprovalText.InnerHtml = "PR Expense Rejected By";
                            lblExpenseRejected.Visible = true;
                        }
                        if (File.Exists(HttpContext.Current.Server.MapPath(prMaster.ExpenseApprovalSignature)))
                            imgExpApprovedBySignature.ImageUrl = prMaster.ExpenseApprovalSignature;
                        else
                            imgExpApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                    }
                    else
                    {
                        lblExpensePending.Visible = true;
                    }
                }


                if (prMaster.IsTerminated == 1) {
                    pnlTermination.Visible = true;
                    lblTerMinatedByName.Text = prMaster.TerminatedByName;
                    lblTerminatedDate.Text = prMaster.TerminatedOn.ToString("dd/MM/yyyy");
                    lblTerminationRemarks.Text = prMaster.TerminationRemarks;

                    btnTerminatePR.Visible = false;
                    btnModify.Visible = false;

                    if (File.Exists(HttpContext.Current.Server.MapPath(prMaster.TerminatedSignature)))
                        imgTerminatedBySignature.ImageUrl = prMaster.TerminatedSignature;
                    else
                        imgTerminatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                }

                if (File.Exists(HttpContext.Current.Server.MapPath(prMaster.CreatedSignature)))
                    imgCreatedBySignaturePr.ImageUrl = prMaster.CreatedSignature;
                else
                    imgCreatedBySignaturePr.ImageUrl = "UserSignature/NoSign.jpg";

                prMaster.PrDetails = prControllerV2.FetchPrDetailsList(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()));
                for (int i = 0; i < prMaster.PrDetails.Count; i++) {
                    if (prMaster.PrDetails[i].PurchaseType != 2) {
                        gvPRItems.Columns[6].Visible = false;
                        gvPRItems.Columns[7].Visible = false;
                    }
                    if (prMaster.PrDetails[i].PurchaseType == 2) {
                        if (prMaster.PrDetails[i].ImportItemType != 1) {
                            gvPRItems.Columns[7].Visible = false;
                        }
                    }
                }

                gvPRItems.DataSource = prMaster.PrDetails;
                gvPRItems.DataBind();

                ViewState["PrMaster"] = serializer.Serialize(prMaster);
                
                    lblInfo.Text = prMaster.StatusName;
               
            }
        }

        protected void btnModify_Click(object sender, EventArgs e) {
            Response.Redirect("EditPR_V2.aspx?PrId=" + Request.QueryString.Get("PrId"));
        }

        protected void btnClone_Click(object sender, EventArgs e) {
            var PrId = prControllerV2.ClonePR(int.Parse(Request.QueryString.Get("PRId")), int.Parse(Session["UserId"].ToString()));

            var prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            string StatusCode = "CLND";
            if (PrId > 0) {

                for(int i = 0; i< prMaster.PrDetails.Count; i++) {
                    int prdId = prMaster.PrDetails[i].PrdId;

                    prDetailsStatusLogController.InsertLog(prdId, int.Parse(Session["UserId"].ToString()), StatusCode);
                }

            }

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'PR Cloned Successfully', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPRNew.aspx?PrId="+PrId+"';  }) });   </script>", false);

        }

        protected void lbtnMore_Click(object sender, EventArgs e) {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrDetailsV2 prD = prControllerV2.GetPrdTerminationDetails(PrdId);
            prdTerminatedByName.Text = prD.TerminatedByName;
            prdTerminatedDate.Text = prD.TerminatedOn.ToString("dd/MM/yyyy");
            lblPrdTerminationRemarks.Text = prD.TerminationRemarks;

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlTerminationDetails').modal('show'); });   </script>", false);
        }

        protected void lbtnLog_Click(object sender, EventArgs e) {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            gvStatusLog.DataSource = ControllerFactory.CreatePRDetailsStatusLogController().PrLogDetails(PrdId);
            gvStatusLog.DataBind();

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlLog').modal('show'); });   </script>", false);
        }        

        protected void btnTerminate_Click(object sender, EventArgs e) {
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            int result = prControllerV2.TerminatePR(prMaster.PrId,prMaster.MrnId, int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
            var PR = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            if (result > 0) {
                string StatusCode = "TERM";
                for (int i = 0; i < PR.PrDetails.Count; i++) {
                    int prdId = PR.PrDetails[i].PrdId;
                    prDetailsStatusLogController.InsertLog(prdId, int.Parse(Session["UserId"].ToString()), StatusCode);
                }


                if (ViewState["CreatedBy"].ToString() != Session["UserId"].ToString()) {
                    //string emailAddress = ControllerFactory.CreateEmailController().GetMRNCreatorEmail(int.Parse(Request.QueryString.Get("MrnId")));
                    //string subject = "Material Request Note Terminated";
                    //StringBuilder message = new StringBuilder();
                    //message.AppendLine("Dear User,");
                    //message.AppendLine("<br>");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Please be advised that your Material Request Note, <b>" + ViewState["MrnCode"].ToString() + "</b> has been terminated by <b>" + Session["FirstName"].ToString() + "</b> with the Remark: " + hdnRemarks.Value + ".");
                    //message.AppendLine("<br>");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Thanks and Regards,");
                    //message.AppendLine("<br>");
                    //message.AppendLine("Team EzBidLanka.");
                    //EmailGenerator.SendEmailV2(new List<string> { emailAddress }, subject, message.ToString(), true);
                }

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='ViewPrNew.aspx?PrId=" + Request.QueryString.Get("PrId") + "'; });   </script>", false);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on Terminating PR\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }

        protected void lkRemark_Click(object sender, EventArgs e)
        {
            string remark = "Not Found";
            string type = "";
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            if (prDetails.Remarks != "" && prDetails.Remarks != null)
            {
                remark = prDetails.Remarks;
                type = "type: 'info',";
            }

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none",
                "<script>    $(document).ready(function () {  " +
                " swal({  " +
                "    title: 'Remarks',  " +
                 type +
                "    text: '" + remark + "',  " +
                "    confirmButtonClass: 'btn btn-info btn-styled',  " +
                "    confirmButtonText: 'Close',  " +
                "    buttonsStyling: false " +
                " }); " +

                " });   </script>"

                , false);
        }

        protected void lkRepalacementImages_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvViewReplacementImages.DataSource = prDetails.PrReplacementFileUploads;
            gvViewReplacementImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlReplacementImages').modal('show'); });   </script>", false);
        }

        protected void lkStandardImages_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvStandardImages.DataSource = prDetails.PrFileUploads;
            gvStandardImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlFileUpload').modal('show'); });   </script>", false);
        }

        protected void lkSupportiveDocument_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvSupportiveDocuments.DataSource = prDetails.PrSupportiveDocuments;
            gvSupportiveDocuments.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlSupportiveDocs').modal('show'); });   </script>", false);
        }

        protected void lkItemSpecification_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            PrDetailsV2 prDetails = prMaster.PrDetails.Find(t => t.PrdId == PrdId);
            gvBOMDate.DataSource = prDetails.PrBoms;
            gvBOMDate.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItemSpec').modal('show'); });   </script>", false);
        }
        protected void btnCapexDocs_Click(object sender, EventArgs e) {
            int prId = int.Parse(Request.QueryString.Get("PrId").ToString());
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());

            gvCapexDocs.DataSource = prCapexController.GetPrCapexDocs(prId);
            gvCapexDocs.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlCapexDocs').modal('show'); });   </script>", false);
        }

        protected void lkWarehouseStock_Click(object sender, EventArgs e)
        {
            int PrdId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            Inventory AvailableInventory = prMaster.PrDetails.Find(t => t.PrdId == PrdId).AvailableWarehouseStock;
            //gvWarehouseStock.DataSource = new List<PrDetailsV2> { prDetails };
            gvWarehouseStock.DataSource = new List<Inventory> { AvailableInventory };
            gvWarehouseStock.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlWarehouseStock').modal('show'); });   </script>", false);
        }
    }
}
