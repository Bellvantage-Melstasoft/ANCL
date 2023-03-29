using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class ViewAssignedMRNDetails : System.Web.UI.Page
    {
        MrnControllerV2 mrnController = ControllerFactory.CreateMrnControllerV2();
        MRNDIssueNoteControllerInterface mrndIssueNoteController = ControllerFactory.CreateMRNDIssueNoteController();
        MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        WarehouseControllerInterface wareHouseController = ControllerFactory.CreateWarehouseController();
        MRNDetailController mrnDetailController = ControllerFactory.CreateMRNDetailController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        AddItemController addItem = ControllerFactory.CreateAddItemController();

        public static int  ware = 0;
        protected void Page_Load(object sender, EventArgs e) {
            serializer.MaxJsonLength = Int32.MaxValue;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefWarehouse";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabWarehouse";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewAssignedMRNDetails.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewAssignedMRNLink";
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                MrnMasterV2 mrnMaster = mrnController.GetMRNMasterToView(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["MrnCode"] = "MRN" + mrnMaster.MrnCode;
                ViewState["CreatedBy"] = mrnMaster.CreatedBy;
                ViewState["WarehouseStock"] = null;
                WarehouseId.Text = mrnMaster.WarehouseId.ToString();
                ware = int.Parse(WarehouseId.Text);
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
                lblPurchaseType.Text = mrnMaster.PurchaseType == 1 ? "Local" : "Import";
                foreach (GridViewRow r in gvMRNItems.Rows) {
                    try {

                        if (r.RowType == DataControlRowType.DataRow) {
                            CheckBox CheckRow = (r.Cells[0].FindControl("CheckBox1") as CheckBox);
                            int MrndId = int.Parse(r.Cells[1].ToString());
                            List<MrnDetails> mrndList = mrnDetailController.FetchFullyIssuedMrnDetails(int.Parse(Request.QueryString.Get("MrnId")));

                            for (int i = 0; i < mrndList.Count; i++) {
                                if (mrndList[i].Status == 6 || mrndList[i].Status == 13) {
                                    CheckRow.Enabled = false;
                                }
                                else {
                                    CheckRow.Enabled = true;
                                }
                            }


                        }
                    }
                    catch (Exception ex) {

                    }
                }

                if (mrnMaster.IsMrnApproved == 0)
                {
                    btnTerminateMRN.Visible = mrnMaster.IsTerminated == 0 ? true : false;
                    lblPending.Visible = true;
                }
                else if (mrnMaster.IsMrnApproved == 1)
                {
                    btnTerminateMRN.Visible = false;
                    lblApproved.Visible = true;
                }
                else
                {
                    btnTerminateMRN.Visible = false;
                    lblRejected.Visible = true;
                }

                if (mrnMaster.IsMrnApproved != 0)
                {
                    pnlApprovedByDetails.Visible = true;
                    lblApprovedByName.Text = mrnMaster.MrnApprovalByName;
                    lblApprovedDate.Text = mrnMaster.MrnApprovalOn.ToString("dd/MM/yyyy");
                    lblRemark.Text = mrnMaster.MrnApprvalRemarks;
                    if (mrnMaster.IsMrnApproved == 1)
                    {
                        lblApprovalText.InnerHtml = "MRN Approved By";
                    }
                    else if (mrnMaster.IsMrnApproved == 2)
                    {
                        lblApprovalText.InnerHtml = "MRN Rejected By";
                    }

                    if (File.Exists(HttpContext.Current.Server.MapPath(mrnMaster.ApprovedSignature)))
                        ImgApprovedBySignature.ImageUrl = mrnMaster.ApprovedSignature;
                    else
                        ImgApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                }

                if (mrnMaster.ExpenseType == 1)
                {
                    spanMrnExpense.Visible = true;
                    if (mrnMaster.IsExpenseApproved != 0)
                    {
                        pnlExpenseApprovalByDetails.Visible = true;
                        lblExpenseApprovedByName.Text = mrnMaster.ExpenseApprovalByName;
                        lblExpenseApprovedDate.Text = mrnMaster.ExpenseApproalOn.ToString("dd/MM/yyyy");
                        lblExpenseRemark.Text = mrnMaster.ExpenseRemarks;
                        if (mrnMaster.IsExpenseApproved == 1)
                        {
                            lblExpenseApprovalText.InnerHtml = "MRN Expense Approved By";
                            lblExpenseApproved.Visible = true;
                        }
                        else if (mrnMaster.IsExpenseApproved == 2)
                        {
                            lblExpenseApprovalText.InnerHtml = "MRN Expense Rejected By";
                            lblExpenseRejected.Visible = true;
                        }

                        if (File.Exists(HttpContext.Current.Server.MapPath(mrnMaster.ApprovedSignature)))
                            imgExpApprovedBySignature.ImageUrl = mrnMaster.ExpenseApprovalSignature;
                        else
                            imgExpApprovedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                    }
                    else
                    {
                        lblExpensePending.Visible = true;
                    }
                }


                if (mrnMaster.IsTerminated == 1)
                {
                    pnlTermination.Visible = true;
                    lblTerMinatedByName.Text = mrnMaster.TerminatedByName;
                    lblTerminatedDate.Text = mrnMaster.TerminatedOn.ToString("dd/MM/yyyy");
                    lblTerminationRemarks.Text = mrnMaster.TerminationRemarks;
                    btnTerminateMRN.Visible = false;

                    if (File.Exists(HttpContext.Current.Server.MapPath(mrnMaster.ApprovedSignature)))
                        imgTerminatedBySignature.ImageUrl = mrnMaster.TerminatedSignature;
                    else
                        imgTerminatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";
                }

                if (File.Exists(HttpContext.Current.Server.MapPath(mrnMaster.CreatedSignature)))
                    imgCreatedBySignature.ImageUrl = mrnMaster.CreatedSignature;
                else
                    imgCreatedBySignature.ImageUrl = "UserSignature/NoSign.jpg";

                mrnMaster.MrnDetails = mrnController.FetchMrnDetailsListWithoutTerminated(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()),mrnMaster.WarehouseId);
                ViewState["MrnMaster"] = serializer.Serialize(mrnMaster);

                
                if ( mrnMaster.MrnDetails.Count>0) {

                    List<WarehouseInventory> warehouseStocklist = wareHouseController.FetchItemAvailableStock(mrnMaster.WarehouseId, mrnMaster.MrnDetails.Select(t => t.ItemId));
                    ViewState["WarehouseStock"] = serializer.Serialize(warehouseStocklist); 
                }
                else {
                    Response.Redirect("ViewAssignedMRN.aspx");
                }
               

                gvMRNItems.DataSource = mrnMaster.MrnDetails;
                gvMRNItems.DataBind();
                
                    lblInfo.Text = mrnMaster.StatusName;
                
                
                btnTerminateMRN.Visible = true;
                if (mrnMaster.MrnDetails.All(t => t.Status == 6))
                {
                    btnAddToPR.Enabled = false;
                }
            }
        }

        protected void btnModify_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditMRN_V2.aspx?MrnId=" + Request.QueryString.Get("MrnId"));
        }

        protected void btnClone_Click(object sender, EventArgs e)
        {
            var MrnId = mrnController.CloneMRN(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["UserId"].ToString()));
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'MRN Cloned Successfully', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewMRNNew.aspx?MrnId=" + MrnId + "';  }) });   </script>", false);

        }

        protected void lbtnMore_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            MrnDetailsV2 mrnd = mrnController.GetMrndTerminationDetails(MrndId);
            mrndTerminatedByName.Text = mrnd.TerminatedByName;
            mrndTerminatedDate.Text = mrnd.TerminatedOn.ToString("dd/MM/yyyy");
            lblMrndTerminationRemarks.Text = mrnd.TerminationRemarks;

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlTerminationDetails').modal('show'); });   </script>", false);
        }

        protected void lbtnLog_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            gvStatusLog.DataSource = ControllerFactory.CreateMrnDetailStatusLogController().MrnLogDetails(MrndId);
            gvStatusLog.DataBind();

            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlLog').modal('show'); });   </script>", false);
        }

        protected void lbtnIssueNote_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            gvIssueNote.DataSource = mrndIssueNoteController.FetchIssueNoteDetailsByMrnDetailsId(MrndId);
            gvIssueNote.DataBind();
            ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlViewIssueNote').modal('show'); });   </script>", false);
        }

        protected void btnTerminate_Click(object sender, EventArgs e)
        {
            int result = mrnController.TerminateMRN(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            if (result > 0)
            {
                if (ViewState["CreatedBy"].ToString() != Session["UserId"].ToString())
                {
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

                for (int i = 0; i < mrnMaster.MrnDetails.Count; i++)
                {
                    mrnDetailsStatusLogController.InsertLogTerminate(mrnMaster.MrnDetails[i].MrndId, int.Parse(Session["UserId"].ToString()));
                }

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='ViewAssignedMRNDetails.aspx?MrnId=" + Request.QueryString.Get("MrnId") + "'; });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on Terminating MRN\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }

        protected void lkRemark_Click(object sender, EventArgs e)
        {
            string remark = "Not Found";
            string type = "";
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            if (mrnDetails.Remarks != "" && mrnDetails.Remarks != null)
            {
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

        protected void lkRepalacementImages_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvViewReplacementImages.DataSource = mrnDetails.MrnReplacementFileUploads;
            gvViewReplacementImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlReplacementImages').modal('show'); });   </script>", false);
        }

        protected void lkStandardImages_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvStandardImages.DataSource = mrnDetails.MrnFileUploads;
            gvStandardImages.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlFileUpload').modal('show'); });   </script>", false);
        }

        protected void lkSupportiveDocument_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvSupportiveDocuments.DataSource = mrnDetails.MrnSupportiveDocuments;
            gvSupportiveDocuments.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlSupportiveDocs').modal('show'); });   </script>", false);
        }

        protected void lkItemSpecification_Click(object sender, EventArgs e)
        {
            int MrndId = int.Parse(((sender as LinkButton).NamingContainer as GridViewRow).Cells[1].Text);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == MrndId);
            gvBOMDate.DataSource = mrnDetails.MrnBoms;
            gvBOMDate.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlItemSpec').modal('show'); });   </script>", false);
        }

        protected void btnIssueStock_Click(object sender, EventArgs e)
        {
            decimal issueQuantity = Convert.ToDecimal(hdnIssueStock.Value);
            decimal prevoiusIssuedQuantity = Convert.ToDecimal(hndPreviousIssuedQty.Value);
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            if (issueQuantity != 0)
            {
                decimal requestedQty = Convert.ToDecimal(hndRequestedQty.Value);
                int status = 2; // partially issues
                if (issueQuantity >= (requestedQty - prevoiusIssuedQuantity))
                {
                    status = 3; // fully issues
                    mrnMaster.Status = 1;  // fully issues
                }
                int mrndId = Convert.ToInt32(hndMrndId.Value);
                int itemId = Convert.ToInt32(hndItemId.Value);
                MrnDetailsV2 mrnDetail = mrnMaster.MrnDetails.Find(t => t.MrndId == mrndId && t.ItemId == itemId);
                MRNDIssueNote mrnIssueNotes = new MRNDIssueNote();
                mrnIssueNotes.MrndID = mrndId;
                mrnIssueNotes.ItemID = itemId;
                mrnIssueNotes.WarehouseID = mrnMaster.WarehouseId;
                mrnIssueNotes.IssuedQty = issueQuantity;
                mrnIssueNotes.IssuedBy = int.Parse(Session["UserId"].ToString());
                mrnIssueNotes.Status = 2; // issued(status:1) & deliverd(status:2)
                mrnIssueNotes.MeasurementId = mrnDetail.MeasurementId;

                mrnDetail.IssuedQty = issueQuantity;
                mrnDetail.Status = status;
                int flag = mrnDetailController.StockKeeperIssueQuantity(mrnMaster.WarehouseId, mrnDetail, mrnIssueNotes, mrnMaster.Status, int.Parse(Session["UserId"].ToString()));
                if (flag > 0)
                {
                    mrnDetailsStatusLogController.InsertLog(mrndId, int.Parse(Session["UserId"].ToString()), 6);
                    ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(),
                            "none",
                            "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });  location.reload(); </script>",
                            false);

                    List<WarehouseInventory> warehouseStocklist = wareHouseController.FetchItemAvailableStock(mrnMaster.WarehouseId, mrnMaster.MrnDetails.Select(t => t.ItemId));
                    ViewState["WarehouseStock"] = serializer.Serialize(warehouseStocklist);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during issue quantity ', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
            }
        }

        protected void btnAddToStock_Click(object sender, EventArgs e)
        {
            decimal addToStock = Convert.ToDecimal(hdnAddStock.Value);
            decimal itemPrice = Convert.ToDecimal(hdnAddStockPrice.Value);
            DateTime expDate = DateTime.MinValue;
            if (hdnExpdate.Value != "") {
                expDate = DateTime.Parse(hdnExpdate.Value);
            }
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            if (addToStock != 0)
            {
                int mrndId = Convert.ToInt32(hndMrndId.Value);
                int itemId = Convert.ToInt32(hndItemId.Value);
                AddItem item = addItem.FetchItemByItemId(itemId);

                WarehouseInventoryRaise warehouseInventoryRaise = new WarehouseInventoryRaise();
                warehouseInventoryRaise.ItemID = itemId;
                warehouseInventoryRaise.RaisedBy = int.Parse(Session["UserId"].ToString());
                warehouseInventoryRaise.RaisedQty = addToStock;
                warehouseInventoryRaise.RaisedWarehouseID = mrnMaster.WarehouseId;
                warehouseInventoryRaise.WarehouseID = mrnMaster.WarehouseId;
                warehouseInventoryRaise.StockValue = itemPrice * addToStock;
                warehouseInventoryRaise.StockMaintainigType = item.StockMaintainingType;
                warehouseInventoryRaise.CompanyId = int.Parse(Session["CompanyId"].ToString());

                MrnDetailsV2 mrnDetail = mrnMaster.MrnDetails.Find(t => t.MrndId == mrndId && t.ItemId == itemId);
                int flag = mrnDetailController.StoreKeeperAddToStock(mrnDetail, warehouseInventoryRaise, int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["UserId"].ToString()), expDate);
                if (flag > 0)
                {

                    ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(),
                            "none",
                            "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });  location.reload(); </script>",
                            false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during add to stock ', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
            }
        }

        protected void btnTerminateItemhnd_Click(object sender, EventArgs e)
        {
            int mrndId = Convert.ToInt32(hndMrndId.Value);
            int itemId = Convert.ToInt32(hndItemId.Value);
            string remark = hndTerminationItemRemark.Value.ProcessString();
            int flag = mrnDetailController.StoreKeeperTerminateItem(mrndId, itemId, remark, int.Parse(Session["UserId"].ToString()), int.Parse(Request.QueryString.Get("MrnId")));
            if (flag > 0)
            {
                mrnDetailsStatusLogController.InsertLogTerminate(mrndId, int.Parse(Session["UserId"].ToString()));

                ScriptManager.RegisterClientScriptBlock(this.Updatepanel1, this.Updatepanel1.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { location.reload(); }); });   </script>",
                        false);

               
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during terminate item', showConfirmButton: true,timer: 4000}); });   </script>", false);
            }
        }

        protected void gvMRNItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
                if (mrnMaster.MrnDetails.All(t => t.Status == 1))
                {
                    CheckBox chk = e.Row.FindControl("CheckBox2") as CheckBox;
                    chk.Enabled = false;
                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
                int mrndId = Convert.ToInt32(e.Row.Cells[1].Text.ToString());
                int itemId = Convert.ToInt32(e.Row.Cells[2].Text.ToString());
                List<WarehouseInventory> warehouselist = serializer.Deserialize<List<WarehouseInventory>>(ViewState["WarehouseStock"].ToString());
                WarehouseInventory warehouseStock = warehouselist.Find(t => t.ItemID == itemId);

                MrnDetailsV2 mrnDetails = mrnMaster.MrnDetails.Find(t => t.MrndId == mrndId);

                if (warehouseStock != null && warehouseStock.AvailableQty > 0 && mrnDetails.Status != 13 )
                {
                    //LinkButton issueStock = e.Row.FindControl("btnIssueFromStock") as LinkButton;
                    //issueStock.Visible = true;

                    Button issueStock = e.Row.FindControl("btnIssueFromStock") as Button;
                    issueStock.Visible = true;
                    
                }

                if (mrnDetails.Status != 13 && mrnDetails.Status != 6) {

                    LinkButton AddStock = e.Row.FindControl("lbtnAddStock") as LinkButton;
                    AddStock.Visible = true;
                }
                if (mrnDetails.Status != 13) {

                    LinkButton lbtnTerminate = e.Row.FindControl("lbtnMore") as LinkButton;
                    lbtnTerminate.Visible = true;
                }

                //if (mrnDetails.Status == 13) {
                    

                //    Label lblNobtn = e.Row.FindControl("lblBtns") as Label;
                //    lblNobtn.Visible = true;

                //}

                MrnDetailsV2 mrnDetail = mrnMaster.MrnDetails.Find(t => t.MrndId == mrndId);
                if (mrnDetail.Status == 1)
                {
                    CheckBox chk = e.Row.FindControl("CheckBox1") as CheckBox;
                    chk.Enabled = false;
                }

                
                List<MrnDetails> mrndList = mrnDetailController.FetchFullyIssuedMrnDetails(int.Parse(Request.QueryString.Get("MrnId")));
                CheckBox CheckRow = (e.Row.Cells[0].FindControl("CheckBox1") as CheckBox);
                for (int i = 0; i < mrndList.Count; i++) {
                    if (mrndList[i].Mrnd_ID == mrndId) {
                        if (mrndList[i].Status == 6 || mrndList[i].Status == 13) {
                            CheckRow.Enabled = false;
                        }
                        else {
                            CheckRow.Enabled = true;
                        }
                    }
                }

            }
        }

        protected void btnAddToPR_Click(object sender, EventArgs e)
        {
            List<int> MrndIds = new List<int>();
            foreach (GridViewRow row in gvMRNItems.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckRow = (row.Cells[0].FindControl("CheckBox1") as CheckBox);
                    if (CheckRow.Checked)
                    {

                        MrndIds.Add(Convert.ToInt32(row.Cells[1].Text));
                    }
                }
            }
            MrnMasterV2 mrnMaster = serializer.Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
            List<MrnDetailsV2> mrnDetails = mrnMaster.MrnDetails.Where(p => MrndIds.Any(p2 => p2 == p.MrndId)).ToList();
            mrnMaster.MrnDetails = mrnDetails;
            int PrId = 0;

            if (mrnMaster.ExpenseType == 1) {
                mrnMaster.IsExpenseApproved = 0;
                mrnMaster.ExpenseApprovalBy = 0;

            }
            string PrCode = mrnController.AddMRNtoPR(mrnMaster, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()) ,out PrId);
            if (!string.IsNullOrEmpty(PrCode))
            {

                for (int i = 0; i < MrndIds.Count; i++)
                {
                    mrnDetailsStatusLogController.InsertLogAddToPR(MrndIds[i], int.Parse(Session["UserId"].ToString()));
                }

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                "<script>    $(document).ready(function () { " +
                " swal({ type: 'success',confirmButtonText: 'OK' , " +
                " showConfirmButton: true ,allowOutsideClick: false, " +
                "title: 'New PR sucessfully created.<br/> Your PR Code is " + PrCode + "' " +
                " }).then((result) => { " +
                "if (result.value){" +
                " window.location = 'ViewPRNew.aspx?PrId=" + PrId + "' " +
                " } " +
                " }); " +
                " });    </script>", false);

            }
        }

        //insert convertion code

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetItemMeasurements(int ItemId)
        {
            try
            {
                return JsonConvert.SerializeObject(
                        new
                        {
                            Status = 200,
                            Data = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(ItemId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(md =>
                               new
                               {
                                   Id = md.DetailId,
                                   Name = md.ShortCode,

                               })
                        });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetWarehousesAndMeasurements(int ItemId)
        {
            try
            {
                return
                        JsonConvert.SerializeObject(
                        new
                        {
                            Status = 200,
                            Data = new
                            {
                                Warehouses = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(w => new
                                {
                                    w.WarehouseID,
                                    WarehouseName = w.Location

                                }),
                                Measurements = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(ItemId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(md =>
                                new
                                {
                                    Id = md.DetailId,
                                    Name = md.ShortCode
                                })
                            }
                        });

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetStockInfo(int ItemId, int WarehouseId, int MeasurementId, decimal RequestedQty, decimal IssuedQty, int RequestedMeasurement)
        {
            try
            {
               
                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());
                WarehouseId = ware;
                IConversionController conveter = ControllerFactory.CreateConversionController();
                IMeasurementDetailController uomController = ControllerFactory.CreateMeasurementDetailController();

                WarehouseInventoryDetail inventoryDetail = ControllerFactory.CreateInventoryController().GetWarehouseInventoryDetailToIssue(ItemId, WarehouseId, CompanyId);

                if (MeasurementId == RequestedMeasurement)
                {
                    inventoryDetail.RequestedQty = RequestedQty;
                    inventoryDetail.IssuedQty = IssuedQty;
                    
                }
                else
                {
                    inventoryDetail.RequestedQty = conveter.DoConversion(ItemId, CompanyId, RequestedQty, RequestedMeasurement, MeasurementId);
                    inventoryDetail.IssuedQty = conveter.DoConversion(ItemId, CompanyId, IssuedQty, RequestedMeasurement, MeasurementId);
                }

                if (inventoryDetail.StockMaintainingUom != MeasurementId)
                {
                    inventoryDetail.AvailableQty = conveter.DoConversion(ItemId, CompanyId, inventoryDetail.AvailableQty, inventoryDetail.StockMaintainingUom, MeasurementId);
                    inventoryDetail.HoldedQty = conveter.DoConversion(ItemId, CompanyId, inventoryDetail.HoldedQty, inventoryDetail.StockMaintainingUom, MeasurementId);

                    if (inventoryDetail.StockMaintainingType != 1)
                    {
                        for (int i = 0; i < inventoryDetail.Batches.Count; i++)
                        {
                            inventoryDetail.Batches[i].AvailableStock = conveter.DoConversion(ItemId, CompanyId, inventoryDetail.Batches[i].AvailableStock, inventoryDetail.StockMaintainingUom, MeasurementId);
                            inventoryDetail.Batches[i].HoldedQty = conveter.DoConversion(ItemId, CompanyId, inventoryDetail.Batches[i].HoldedQty, inventoryDetail.StockMaintainingUom, MeasurementId);
                            if (inventoryDetail.Batches[i].ExpiryDate == DateTime.MinValue) {
                                }
                        }
                    }
                }

                return JsonConvert.SerializeObject(
                        new
                        {
                            Status = 200,
                            Data = inventoryDetail
                        });
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [WebMethod]
        public static string IssueInventory(int MrndId, int MrndStatus, int MrnId, MRNDIssueNote Note)
        {
            try
            {

                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());

                IConversionController conveter = ControllerFactory.CreateConversionController();
                IMeasurementDetailController uomController = ControllerFactory.CreateMeasurementDetailController();

                MeasurementDetail stockUom = uomController.GetStockMaintainingMeasurement(Note.ItemID, CompanyId);

                decimal IssuedQty = 0;
                if (Note.RequestedMeasurementId == Note.MeasurementId)
                {
                    IssuedQty = Note.IssuedQty;
                }
                else
                {
                    IssuedQty = conveter.DoConversion(Note.ItemID, CompanyId, Note.IssuedQty, Note.MeasurementId, Note.RequestedMeasurementId);
                }

                Note.IssuedBy = int.Parse(HttpContext.Current.Session["UserId"].ToString());
                Note.Status = 1;
                Note.StValue = Note.IssuedStockValue;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                Inventory inventoryObj = new Inventory();
                inventoryObj.ItemID = Note.ItemID;
                inventoryObj.WarehouseID = Note.WarehouseID;
                inventoryObj.IssuedQty = Note.IssuedQty;
                inventoryObj.LastUpdatedBy = int.Parse(HttpContext.Current.Session["UserId"].ToString());
                inventoryObj.IssuedStockValue = Note.IssuedStockValue;
                inventoryObj.IssuedBatches = serializer.Deserialize<List<IssuedInventoryBatches>>(serializer.Serialize(Note.IssuedBatches));

                if (stockUom.DetailId != Note.MeasurementId)
                {
                    inventoryObj.IssuedQty = conveter.DoConversion(Note.ItemID, CompanyId, inventoryObj.IssuedQty, Note.MeasurementId, stockUom.DetailId);

                    for (int i = 0; i < inventoryObj.IssuedBatches.Count; i++)
                    {
                        inventoryObj.IssuedBatches[i].IssuedQty = conveter.DoConversion(Note.ItemID, CompanyId, inventoryObj.IssuedBatches[i].IssuedQty, Note.MeasurementId, stockUom.DetailId);
                    }
                }

                if (IssuedQty != 0) {

                    int result = ControllerFactory.CreateInventoryController().UpdateIssuedItems(
                        MrndId,
                        IssuedQty,
                        MrndStatus,
                        CompanyId,
                        Note.ItemID,
                        int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                        new List<Inventory> { inventoryObj },
                        new List<MRNDIssueNote> { Note },
                        MrnId);

                    if (result > 0) {
                        return JsonConvert.SerializeObject(
                                new {
                                    Status = 200,
                                    Data = "Success"
                                });
                    }
                    else {
                        return JsonConvert.SerializeObject(
                                new {
                                    Status = 500,
                                    Data = "Error"
                                });
                    }
                }
                else {
                    return JsonConvert.SerializeObject(
                                new {
                                    Status = 600,
                                    Data = "Error"
                                });
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}