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
    public partial class ViewMRNNew : System.Web.UI.Page {
        MrnControllerV2 mrnController = ControllerFactory.CreateMrnControllerV2();
        MRNDIssueNoteControllerInterface mrndIssueNoteController = ControllerFactory.CreateMRNDIssueNoteController();
        MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        MRNCapexDocController mRNCapexDocController = ControllerFactory.CreateMRNCapexDocController();

        protected void Page_Load(object sender, EventArgs e) {
            serializer.MaxJsonLength = Int32.MaxValue;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewMRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewMRNLink";
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                MrnMasterV2 mrnMaster = mrnController.GetMRNMasterToView(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["MrnCode"] = "MRN" + mrnMaster.MrnCode;
                ViewState["CreatedBy"] = mrnMaster.CreatedBy;
                //  ViewState["MrndId"] = mrnMaster.MrnDetails.MrndId;

                lblWarehouseName.Text = mrnMaster.Warehouse.Location;
                lblWarehouseAddress.Text = mrnMaster.Warehouse.Address;
                lblWarehouseContact.Text = mrnMaster.Warehouse.PhoneNo;

                lblCategory.Text = mrnMaster.MrnCategoryName;
                lblSubCategory.Text = mrnMaster.MrnSubCategoryName;
                lblExpectedDate.Text = (mrnMaster.ExpectedDate).ToString("dd/MM/yyyy");
                lblMrnCode.Text = "MRN-" + (mrnMaster.MrnCode).ToString();

                lblDepartmentName.Text = mrnMaster.SubDepartment.SubDepartmentName;
                lblDepartmentContact.Text = mrnMaster.SubDepartment.PhoneNo;

                lblCreatedByName.Text = mrnMaster.CreatedByName;
                lblCreatedDate.Text = mrnMaster.CreatedDate.ToString("dd/MM/yyyy");

                lblExpenseType.Text = mrnMaster.ExpenseType == 1 ? "Capital Expense" : "Operational Expense";

                if (mrnMaster.ExpenseType == 1) {
                    btnCapexDocs.Visible = true;
                }
                else {
                    btnCapexDocs.Visible = false;
                }
                if (mrnMaster.IsMrnApproved == 0) {
                    btnTerminateMRN.Visible = mrnMaster.IsTerminated == 0 ? true : false;
                    lblPending.Visible = true;
                    btnModify.Visible = true;
                }
                else if (mrnMaster.IsMrnApproved == 1) {
                    btnTerminateMRN.Visible = false;
                    lblApproved.Visible = true;
                    btnModify.Visible = false;
                }
                else {
                    btnTerminateMRN.Visible = false;
                    lblRejected.Visible = true;
                    btnModify.Visible = false;
                }

                if (mrnMaster.IsMrnApproved != 0) {
                    pnlApprovedByDetails.Visible = true;
                    lblApprovedByName.Text = mrnMaster.MrnApprovalByName;
                    lblApprovedDate.Text = mrnMaster.MrnApprovalOn.ToString("dd/MM/yyyy");
                    lblRemark.Text = mrnMaster.MrnApprvalRemarks;
                    if (mrnMaster.IsMrnApproved == 1) {
                        lblApprovalText.InnerHtml = "MRN Approved By";
                    }
                    else if (mrnMaster.IsMrnApproved == 2) {
                        lblApprovalText.InnerHtml = "MRN Rejected By";
                    }
                    if (File.Exists(HttpContext.Current.Server.MapPath(mrnMaster.ApprovedSignature)))
                        ImgApprovedBySignature.ImageUrl = mrnMaster.ApprovedSignature;
                    else
                        ImgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                }

                if (mrnMaster.ExpenseType == 1) {
                    spanMrnExpense.Visible = true;
                    if (mrnMaster.IsExpenseApproved != 0) {
                        pnlExpenseApprovalByDetails.Visible = true;
                        lblExpenseApprovedByName.Text = mrnMaster.ExpenseApprovalByName;
                        lblExpenseApprovedDate.Text = mrnMaster.ExpenseApproalOn.ToString("dd/MM/yyyy");
                        lblExpenseRemark.Text = mrnMaster.ExpenseRemarks;
                        if (mrnMaster.IsExpenseApproved == 1) {
                            lblExpenseApprovalText.InnerHtml = "MRN Expense Approved By";
                            lblExpenseApproved.Visible = true;
                        }
                        else if (mrnMaster.IsExpenseApproved == 2) {
                            lblExpenseApprovalText.InnerHtml = "MRN Expense Rejected By";
                            lblExpenseRejected.Visible = true;
                        }
                        if (File.Exists(HttpContext.Current.Server.MapPath(mrnMaster.ExpenseApprovalSignature)))
                            imgExpApprovedBySignature.ImageUrl = mrnMaster.ExpenseApprovalSignature;
                        else
                            imgExpApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                    }
                    else {
                        lblExpensePending.Visible = true;
                    }
                }


                if (mrnMaster.IsTerminated == 1) {
                    pnlTermination.Visible = true;
                    lblTerMinatedByName.Text = mrnMaster.TerminatedByName;
                    lblTerminatedDate.Text = mrnMaster.TerminatedOn.ToString("dd/MM/yyyy");
                    lblTerminationRemarks.Text = mrnMaster.TerminationRemarks;

                    btnTerminateMRN.Visible = false;
                    btnModify.Visible = false;

                    if (File.Exists(HttpContext.Current.Server.MapPath(mrnMaster.TerminatedSignature)))
                        imgTerminatedBySignature.ImageUrl = mrnMaster.TerminatedSignature;
                    else
                        imgTerminatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                }

                if (File.Exists(HttpContext.Current.Server.MapPath(mrnMaster.CreatedSignature)))
                    imgCreatedBySignature.ImageUrl = mrnMaster.CreatedSignature;
                else
                    imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

                mrnMaster.MrnDetails = mrnController.FetchMrnDetailsList(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));

                for (int i = 0; i < mrnMaster.MrnDetails.Count; i++) {
                    if (mrnMaster.MrnDetails[i].PurchaseType != 2) {
                        gvMRNItems.Columns[7].Visible = false;
                        gvMRNItems.Columns[8].Visible = false;
                    }
                    if (mrnMaster.MrnDetails[i].PurchaseType == 2) {
                        if (mrnMaster.MrnDetails[i].ImportItemType != 1) {
                            gvMRNItems.Columns[8].Visible = false;
                        }
                    }
                }

                 gvMRNItems.DataSource = mrnMaster.MrnDetails;
                gvMRNItems.DataBind();

                //   return serializer.Serialize(response);

                // ViewState["MrnMaster"] = new JavaScriptSerializer().Serialize(mrnMaster);
                ViewState["MrnMaster"] = serializer.Serialize(mrnMaster);
                lblInfo.Text = mrnMaster.StatusName;
                lblPurchaseType.Text = mrnMaster.PurchaseType == 1 ? "Local" : "Import";
                //if (mrnMaster.Status == 1) {
                //    lblInfo.Visible = true;
                //    lblInfo.Text = mrnMaster.StatusName;
                //}
                //else if (mrnMaster.Status == 2) {
                //    lblDanger.Visible = true;
                //    lblDanger.Text = mrnMaster.StatusName;
                //}
                //else if (mrnMaster.Status == 3) {
                //    lblInfo.Visible = true;
                //    lblInfo.Text = mrnMaster.StatusName;
                //}
                //else if (mrnMaster.Status == 4) {
                //    lblInfo.Visible = true;
                //    lblInfo.Text = mrnMaster.StatusName;
                //}
                //else if (mrnMaster.Status == 5) {
                //    lblDanger.Visible = true;
                //    lblDanger.Text = mrnMaster.StatusName;
                //}
                //else if (mrnMaster.Status == 6) {
                //    lblSuccess.Visible = true;
                //    lblSuccess.Text = mrnMaster.StatusName;
                //}
                //else if (mrnMaster.Status == 7) {
                //    lblDanger.Visible = true;
                //    lblDanger.Text = mrnMaster.StatusName;
                //}
                //else if (mrnMaster.Status == 8) {
                //    lblInfo.Visible = true;
                //    lblInfo.Text = mrnMaster.StatusName;
                //}
                //else if (mrnMaster.Status == 9) {
                //    lblWarning.Visible = true;
                //    lblWarning.Text = mrnMaster.StatusName;
                //}

            }
        }

        protected void btnModify_Click(object sender, EventArgs e) {
            Response.Redirect("EditMRN_V2.aspx?MrnId=" + Request.QueryString.Get("MrnId"));
        }

        protected void btnClone_Click(object sender, EventArgs e) {
            var MrnId = mrnController.CloneMRN(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["UserId"].ToString()));
            // var MRN = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            var MRN = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());

            int result = mrnController.updateMrnMasterAfterClone(int.Parse(Request.QueryString.Get("MrnId")));


            if (MrnId > 0) {

                for (int i = 0; i < MRN.MrnDetails.Count; i++) {
                    int mrndId = MRN.MrnDetails[i].MrndId;


                    mrnDetailsStatusLogController.InsertLogAfterClone(mrndId, int.Parse(Session["UserId"].ToString()));
                }

            }

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'MRN Cloned Successfully', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewMRNNew.aspx?MrnId=" + MrnId + "';  }) });   </script>", false);

        }

        protected void lbtnMore_Click(object sender, EventArgs e) {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            MrnDetailsV2 mrnd = mrnController.GetMrndTerminationDetails(MrndId);
            mrndTerminatedByName.Text = mrnd.TerminatedByName;
            mrndTerminatedDate.Text = mrnd.TerminatedOn.ToString("dd/MM/yyyy");
            lblMrndTerminationRemarks.Text = mrnd.TerminationRemarks;

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlTerminationDetails').modal('show'); });   </script>", false);
        }

        protected void lbtnLog_Click(object sender, EventArgs e) {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            gvStatusLog.DataSource = ControllerFactory.CreateMrnDetailStatusLogController().MrnLogDetails(MrndId);
            gvStatusLog.DataBind();

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlLog').modal('show'); });   </script>", false);
        }
        protected void lbtnIssueNote_Click(object sender, EventArgs e) {
            try {
                int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
                gvIssueNote.DataSource = mrndIssueNoteController.FetchIssueNoteDetailsByMrnDetailsId(MrndId);
                gvIssueNote.DataBind();
                ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlViewIssueNote').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {

            }
        }
        protected void btnTerminate_Click(object sender, EventArgs e) {
            int result = mrnController.TerminateMRN(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
            // var MRN = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            var MRN = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());


            if (result > 0) {


                for (int i = 0; i < MRN.MrnDetails.Count; i++) {
                    int mrndId = MRN.MrnDetails[i].MrndId;

                    mrnDetailsStatusLogController.InsertLogTerminate(mrndId, int.Parse(Session["UserId"].ToString()));
                }


                if (ViewState["CreatedBy"].ToString() != Session["UserId"].ToString()) {
                    string emailAddress = ControllerFactory.CreateEmailController().GetMRNCreatorEmail(int.Parse(Request.QueryString.Get("MrnId")));
                    string subject = "Material Request Note Terminated";
                    StringBuilder message = new StringBuilder();
                    message.AppendLine("Dear User,");
                    message.AppendLine("<br>");
                    message.AppendLine("<br>");
                    message.AppendLine("Please be advised that your Material Request Note, <b>" + ViewState["MrnCode"].ToString() + "</b> has been terminated by <b>" + Session["FirstName"].ToString() + "</b> with the Remark: " + hdnRemarks.Value + ".");
                    message.AppendLine("<br>");
                    message.AppendLine("<br>");
                    message.AppendLine("Thanks and Regards,");
                    message.AppendLine("<br>");
                    message.AppendLine("Team EzBidLanka.");
                    EmailGenerator.SendEmailV2(new List<string> { emailAddress }, subject, message.ToString(), true);
                }

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='ViewMRNNew.aspx?MrnId=" + Request.QueryString.Get("MrnId") + "'; });   </script>", false);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on Terminating MRN\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }

        protected void lkRemark_Click(object sender, EventArgs e) {
            string remark = "Not Found";
            string type = "";
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            //   MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            if (mrnDetails.Remarks != "" && mrnDetails.Remarks != null) {
                remark = mrnDetails.Remarks;
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

        protected void lkRepalacementImages_Click(object sender, EventArgs e) {

            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            //  MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvViewReplacementImages.DataSource = mrnDetails.MrnReplacementFileUploads;
            gvViewReplacementImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlReplacementImages').modal('show'); });   </script>", false);
        }

        protected void lkStandardImages_Click(object sender, EventArgs e) {

            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            //  MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvStandardImages.DataSource = mrnDetails.MrnFileUploads;
            gvStandardImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlFileUpload').modal('show'); });   </script>", false);
        }

        protected void lkSupportiveDocument_Click(object sender, EventArgs e) {

            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            // MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvSupportiveDocuments.DataSource = mrnDetails.MrnSupportiveDocuments;
            gvSupportiveDocuments.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlSupportiveDocs').modal('show'); });   </script>", false);
        }
        protected void btnCapexDocs_Click(object sender, EventArgs e) {
            int MrnId = int.Parse(Request.QueryString.Get("MrnId").ToString());
            gvCapexDocs.DataSource = ControllerFactory.CreateMRNCapexDocController().GetMrnCapexDocs(MrnId);
            gvCapexDocs.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlCapexDocs').modal('show'); });   </script>", false);
        }

        protected void lkItemSpecification_Click(object sender, EventArgs e) {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[0].Text);
            //  MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvBOMDate.DataSource = mrnDetails.MrnBoms;
            gvBOMDate.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItemSpec').modal('show'); });   </script>", false);
        }
    }
}