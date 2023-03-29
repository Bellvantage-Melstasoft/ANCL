using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Helper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class ViewSubmittedTR : System.Web.UI.Page {

        //MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        InventoryControllerInterface inventoryController = ControllerFactory.CreateInventoryController();
        //MRNDIssueNoteControllerInterface mrndINController = ControllerFactory.CreateMRNDIssueNoteController();
        EmailController emailController = ControllerFactory.CreateEmailController();
        //MrnDetailStatusLogController mrnDetailStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        //MrnDetailStatusLogController logController = ControllerFactory.CreateMrnDetailStatusLogController();
        TRMasterController tRMasterController = ControllerFactory.CreateTRMasterController();
        TRDetailsController tRDetailsController = ControllerFactory.CreateTRDetailsController();
        TrDetailStatusLogController trDetailStatusLogController = ControllerFactory.CreateTrDetailStatusLogController();
       


        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefTR";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabTR";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewSubmittedTR.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "submittedTRLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 13, 5) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.IsHead == 1).Count() > 0) {

                    try {
                        gvApprovRejectTR.DataSource = tRMasterController.fetchSubmittedTRList((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.IsHead == 1).Select(d => d.WrehouseId).ToList());
                        gvApprovRejectTR.DataBind();


                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be a Warehouse Head to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                }
            }

        }

        protected void btnView_Click(object sender, EventArgs e) {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        protected void gvApprovRejectTR_RowDataBound(object sender, GridViewRowEventArgs e) {
            try {
                if (e.Row.RowType == DataControlRowType.DataRow) {
                    int trID = int.Parse(gvApprovRejectTR.DataKeys[e.Row.RowIndex].Value.ToString());
                    GridView gvTRDetails = e.Row.FindControl("gvApprovRejectTR") as GridView;
                 //  ViewState["FromwarehouseId"] = int.Parse(e.Row.Cells[11].Text);

                    gvTRDetails.DataSource = tRDetailsController.fetchSubmittedTrDList(trID, int.Parse(ViewState["CompanyId"].ToString()));
                    gvTRDetails.DataBind();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void btnIssue_Click(object sender, EventArgs e) {
            GridView gv = gvWarehouseInventory;
            int result = 0;
            decimal issued = 0;
            int trdStatus = 0;
            List<TRDIssueNote> notes = new List<TRDIssueNote>();
            List<Inventory> inventoryObjList = new List<Inventory>();
            foreach (GridViewRow row in gv.Rows) {
                TextBox txt = (TextBox)row.FindControl("IssuedQty");
                if (txt.Text != null && txt.Text != "" && txt.Text != "0") {
                    decimal qty = decimal.Parse(txt.Text);

                    if (qty > 0) {
                        TRDIssueNote note = new TRDIssueNote();
                        Inventory inventoryObj = new Inventory();

                        int warehouse = int.Parse(row.Cells[0].Text);
                        int item = int.Parse(row.Cells[1].Text);

                        IssuedInventoryDetails IssuedInventory = new IssuedInventoryDetails();

                        if (ViewState["StockMaintainingType"].ToString() == "1") {
                            IssuedInventory.ItemId = item;
                            IssuedInventory.TotalIssuedQty = qty;
                            IssuedInventory.TotalIssuedStockValue = decimal.Parse(ViewState["UnitPrice"].ToString()) * qty;
                        }
                        else {

                            IssuedInventory.ItemId = item;
                            IssuedInventory.TotalIssuedQty = qty;
                            IssuedInventory.Batches = inventoryController.GetIssuedInventoryBatches(warehouse, item, qty, int.Parse(ViewState["StockMaintainingType"].ToString()));
                            IssuedInventory.TotalIssuedStockValue = IssuedInventory.Batches.Sum(b => b.IssuedStockValue);
                        }

                        note.TRDId = int.Parse(ViewState["TRDId"].ToString());
                        note.ItemId = item;
                        note.WarehouseId = warehouse;
                        note.IssuedQTY = qty;
                        note.IssuedBy = int.Parse(Session["UserId"].ToString());
                        note.Status = 1;
                        note.IssuedStockValue = IssuedInventory.TotalIssuedStockValue;
                        note.IssuedBatches = IssuedInventory.Batches;

                        inventoryObj.ItemID = item;
                        inventoryObj.WarehouseID = warehouse;
                        inventoryObj.IssuedQty = qty;
                        inventoryObj.LastUpdatedBy = int.Parse(Session["UserId"].ToString());
                        inventoryObj.IssuedStockValue = IssuedInventory.TotalIssuedStockValue;
                        note.IssuedBatches = IssuedInventory.Batches;
                        inventoryObj.FromWarehouseId = int.Parse(ViewState["FromwarehouseId"].ToString());
                        
                        notes.Add(note);
                        inventoryObjList.Add(inventoryObj);

                        issued += qty;
                    }
                }
            }

            if (issued + decimal.Parse(ViewState["IssuedQty"].ToString()) < decimal.Parse(ViewState["RequestedQty"].ToString()))
                trdStatus = 2;
            else
                trdStatus = 3;

            result = inventoryController.UpdateIssuedTRItems(int.Parse(ViewState["TRDId"].ToString()), issued, trdStatus, int.Parse(Session["CompanyId"].ToString()), int.Parse(ViewState["ItemId"].ToString()), int.Parse(Session["UserId"].ToString()), inventoryObjList, notes, (int.Parse(ViewState["TRId"].ToString())));

            if (result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='ViewSubmittedTR.aspx'; });   </script>", false);
            }
            else if (result == -1) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Warehouse stock\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
            else if (result == -2) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on adding TR item issue note\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
            else if (result == -3) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Warehouse stock\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
            else if (result == -4) {

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Warehouse stock\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
            else if (result == -5) {

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on changing TR item status\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
            else if (result == -6) {

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating TR item issued QTY\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
            else if (result == -7) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating TR after issue\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
            else if (result == -8) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Log after issue\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
            
        }
      
        protected void btnIssueFromStock_Click(object sender, EventArgs e) {
            GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);

            ViewState["TRDId"] = row.Cells[0].Text;
            ViewState["ItemId"] = row.Cells[3].Text;
            ViewState["AvailableQty"] = decimal.Parse(row.Cells[8].Text);
            ViewState["RequestedQty"] = decimal.Parse(row.Cells[5].Text);
            ViewState["IssuedQty"] = decimal.Parse(row.Cells[6].Text);
            ViewState["PendingQty"] = decimal.Parse(row.Cells[5].Text) - decimal.Parse(row.Cells[6].Text);
            ViewState["TRId"] = int.Parse(row.Cells[14].Text);
            ViewState["ItemName"] = row.Cells[4].Text;

            GridViewRow rows = ((GridViewRow)((Button)sender).NamingContainer.NamingContainer.NamingContainer);
            ViewState["TRCode"] = (rows.Cells[2].FindControl("lblTRCode") as Label).Text;
            ViewState["FromwarehouseId"] = rows.Cells[12].Text;

            List<Inventory> list = inventoryController.fetchWarehouseInventoryTR(int.Parse(ViewState["TRId"].ToString()), int.Parse(row.Cells[3].Text));
            ViewState["UnitPrice"] = list[0].StockValue / (list[0].AvailableQty + list[0].HoldedQty);
            ViewState["StockMaintainingType"] = list[0].StockMaintainingType;
            gvWarehouseInventory.DataSource = list;
            gvWarehouseInventory.DataBind();
            hiddenpending.Value = ViewState["PendingQty"].ToString();
            hiddenavaiability.Value = ViewState["AvailableQty"].ToString();


            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $(document).ready(function () { document.getElementById('requestedQtyShow').innerHTML = '"
                + ViewState["RequestedQty"].ToString() + "'; document.getElementById('issuedQtyShow').innerHTML = '"
                + ViewState["IssuedQty"].ToString() + "'; document.getElementById('pendingQtyShow').innerHTML = '"
                + ViewState["PendingQty"].ToString() + "'; document.getElementById('availableQtyShow').innerHTML = '"
                + ViewState["AvailableQty"].ToString() + "'; $('#mdlIssueStock').modal('show'); });  </script>", false);
            
        }
        protected void btnAddToPR_Click(object sender, EventArgs e) {

            int CompanyId = int.Parse(Session["CompanyId"].ToString());
            IConversionController conveter = ControllerFactory.CreateConversionController();

            var trdList = ViewState["TrdList"] as List<int> == null ? new List<int>() : ViewState["TrdList"] as List<int>;

            GridViewRow newRow = ((GridViewRow)((Button)sender).NamingContainer);

            if (!trdList.Any(m => m == int.Parse(newRow.Cells[0].Text))) {

                ViewState["ItemName"] = newRow.Cells[4].Text;
                ViewState["TRId"] = int.Parse(newRow.Cells[14].Text);

                Button btn = newRow.FindControl("btnAddToPR") as Button;
                btn.Visible = false;


                List<CompnayPurchaseRequestNote.TempDataSet> tempDataSetlist =
                    ViewState["TempDataSet"] == null ? new List<CompnayPurchaseRequestNote.TempDataSet>() :
                    new JavaScriptSerializer().Deserialize<List<CompnayPurchaseRequestNote.TempDataSet>>(ViewState["TempDataSet"].ToString());

                GridViewRow rows = ((GridViewRow)((Button)sender).NamingContainer.NamingContainer.NamingContainer);
                ViewState["TRCode"] = (rows.Cells[2].FindControl("lblTRCode") as Label).Text;

                CompnayPurchaseRequestNote.TempDataSet tempDataSetNew = tempDataSetlist.Find(l => l.ItemId == int.Parse(newRow.Cells[3].Text) && l.WarehouseId == int.Parse(rows.Cells[9].Text));
                if (tempDataSetNew != null) {
                    if (tempDataSetNew.measurementId != int.Parse(hdnMeasurementId.Value)) {
                        tempDataSetNew.ItemQuantity = conveter.DoConversion(tempDataSetNew.ItemId, CompanyId, tempDataSetNew.ItemQuantity, tempDataSetNew.measurementId, int.Parse(hdnMeasurementId.Value));
                    }
                    decimal qty = decimal.Parse(newRow.Cells[5].Text) - decimal.Parse(newRow.Cells[6].Text);

                    if (int.Parse(hdnMeasurementId.Value) != int.Parse(hdnReqMeasurementId.Value)) {
                        qty = conveter.DoConversion(tempDataSetNew.ItemId, CompanyId, qty, int.Parse(hdnReqMeasurementId.Value), int.Parse(hdnMeasurementId.Value));
                    }
                    tempDataSetNew.measurementId = int.Parse(hdnMeasurementId.Value);
                    tempDataSetNew.MeasurementName = hdnMeasurementName.Value;

                    tempDataSetNew.ItemQuantity += qty;
                    tempDataSetNew.TRCodes = string.Join(",", (tempDataSetNew.TRCodes + "," + ViewState["TRCode"].ToString()).Split(',').ToList().Distinct());
                }
                else {
                    tempDataSetNew = new CompnayPurchaseRequestNote.TempDataSet();
                    tempDataSetNew.MainCategoryId = int.Parse(newRow.Cells[11].Text);
                    tempDataSetNew.MainCategoryName = newRow.Cells[1].Text;
                    tempDataSetNew.SubCategoryId = int.Parse(newRow.Cells[12].Text);
                    tempDataSetNew.SubcategoryName = newRow.Cells[2].Text;
                    tempDataSetNew.ItemId = int.Parse(newRow.Cells[3].Text);
                    tempDataSetNew.ItemName = newRow.Cells[4].Text;
                    tempDataSetNew.ItemQuantity = decimal.Parse(newRow.Cells[5].Text) - decimal.Parse(newRow.Cells[6].Text);
                    tempDataSetNew.EstimatedAmount = decimal.Parse(newRow.Cells[13].Text);
                    tempDataSetNew.measurementId = int.Parse(newRow.Cells[15].Text);

                    tempDataSetNew.TRCodes = ViewState["TRCode"].ToString();
                    tempDataSetNew.WarehouseId = int.Parse(rows.Cells[9].Text);
                    tempDataSetNew.WarehouseName = rows.Cells[3].Text;

                    if (int.Parse(hdnMeasurementId.Value) != int.Parse(hdnReqMeasurementId.Value)) {
                        tempDataSetNew.ItemQuantity = conveter.DoConversion(tempDataSetNew.ItemId, CompanyId, tempDataSetNew.ItemQuantity, int.Parse(hdnReqMeasurementId.Value), int.Parse(hdnMeasurementId.Value));
                    }

                    tempDataSetlist.Add(tempDataSetNew);
                }

                trdList.Add(int.Parse(newRow.Cells[0].Text));
                ViewState["TrdList"] = trdList;

                ViewState["TempDataSet"] = new JavaScriptSerializer().Serialize(tempDataSetlist);

                gvAddToPR.DataSource = tempDataSetlist;
                gvAddToPR.DataBind();
            }
            else {
                Button btn = newRow.FindControl("btnAddToPR") as Button;
                btn.Visible = false;
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                 "<script>    $(document).ready(function () { swal({type: 'error', title: 'ERROR', html: 'TR Item Already Added to the List'}); });   </script>", false);

            }

        }

        protected void btnCreatePR_Click(object sender, EventArgs e) {

            var trdList = ViewState["TrdList"] as List<int>;
            List<CompnayPurchaseRequestNote.TempDataSet> tempDataSetlist = new JavaScriptSerializer().Deserialize<List<CompnayPurchaseRequestNote.TempDataSet>>(ViewState["TempDataSet"].ToString());


            var grouped = tempDataSetlist.GroupBy(c => new { c.MainCategoryId, c.WarehouseId }).Select(group => new { group.Key, Items = group.ToList() }).ToList();


            PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
            PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();

            List<int> PrIds = new List<int>();

            for (int i = 0; i < grouped.Count; i++) {
                int purchaseRequestId = pr_MasterController.SavePRMaster(int.Parse(Session["CompanyId"].ToString()), LocalTime.Now, "Transfer Request Notes: " + string.Join(", ", grouped[i].Items.Select(itm => itm.TRCodes).Distinct()), "", Session["FirstName"].ToString(), LocalTime.Now, Session["UserId"].ToString(), LocalTime.Now, "", 1, 0, "", LocalTime.Now, 0, "", LocalTime.Now, 0, 0, "Operational Expense", "", "", "", "", "", "", "", 0, grouped[i].Key.MainCategoryId, grouped[i].Key.WarehouseId);
                PrIds.Add(purchaseRequestId);


                foreach (var item in grouped[i].Items) {
                    pr_DetailController.SavePRDetails(purchaseRequestId, item.ItemId, item.measurementId, item.ItemDescription, "", LocalTime.Now, 1, item.ReplacementId, item.ItemQuantity, item.Purpose, item.EstimatedAmount);
                }
            }

            if (trdList.Count > 0) {
                foreach (int trd in trdList) {
                    tRDetailsController.changeTRDStaus(trd, 1);
                    trDetailStatusLogController.UpdateTRLog(trd, int.Parse(Session["UserId"].ToString()), 6);
                }
            }

            List<string> PrCodes = pr_MasterController.GetPrCodesByPrIds(PrIds);

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                "<script>    $(document).ready(function () { swal({type: 'success', title: 'Success', html: 'Purchase Requisitions was created successfully with follwing code(s). <br/>" + string.Join(", ", PrCodes) + "'}); });   </script>", false);

            ViewState["TrdList"] = null;

            gvAddToPR.DataSource = null;
            gvAddToPR.DataBind();

        }

        protected void btnTerminateTR_Click(object sender, EventArgs e) {
            int result = tRMasterController.TerminateTR(int.Parse(hdnTRID.Value), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);

            if (result > 0) {
                string trCode = tRMasterController.GetTRCode(int.Parse(hdnTRID.Value));
                string email = ControllerFactory.CreateEmailController().GetTRCreatorEmail(int.Parse(hdnTRID.Value));

                string subject = "Transfer Request Note Terminated";

                StringBuilder message = new StringBuilder();

                message.AppendLine("Dear User,");
                message.AppendLine("<br>");
                message.AppendLine("<br>");
                message.AppendLine("Please be advised that your Transfer Request Note, <b>" + trCode + "</b> has been terminated by <b>" + Session["FirstName"].ToString() + "</b> with the Remark: " + hdnRemarks.Value + ".");
                message.AppendLine("<br>");
                message.AppendLine("<br>");
                message.AppendLine("Thanks and Regards,");
                message.AppendLine("<br>");
                message.AppendLine("Team EzBidLanka.");

                EmailGenerator.SendEmail(new List<string> { email }, subject, message.ToString(), true);

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on Terminating TR\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }

        protected void btnTerminateTRD_Click(object sender, EventArgs e) {
            int result = tRDetailsController.TerminateTRD(int.Parse(hdnTRID.Value), int.Parse(hdnTRDID.Value), int.Parse(Session["UserId"].ToString()), hdnRemarks.Value);

            if (result > 0) {
                string trCode = tRMasterController.GetTRCode(int.Parse(hdnTRID.Value));
                string email = ControllerFactory.CreateEmailController().GetTRCreatorEmail(int.Parse(hdnTRID.Value));

                string subject = "An Item From Transfer Request Note was Terminated";

                StringBuilder message = new StringBuilder();

                message.AppendLine("Dear User,");
                message.AppendLine("<br>");
                message.AppendLine("<br>");
                message.AppendLine("Please be advised that the Item, <b>" + hdnItemName.Value + "</b> from <b>" + trCode + "</b> has been terminated by <b>" + Session["FirstName"].ToString() + "</b> with the Remark: " + hdnRemarks.Value + ".");
                message.AppendLine("<br>");
                message.AppendLine("<br>");
                message.AppendLine("Thanks and Regards,");
                message.AppendLine("<br>");
                message.AppendLine("Team EzBidLanka.");

                EmailGenerator.SendEmail(new List<string> { email }, subject, message.ToString(), true);

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='ViewSubmittedTR.aspx'; });   </script>", false);
            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on Terminating TR Item\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetItemMeasurements(int ItemId) {
            try {
                return JsonConvert.SerializeObject(
                        new {
                            Status = 200,
                            Data = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(ItemId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(md =>
                               new {
                                   Id = md.DetailId,
                                   Name = md.ShortCode
                               })
                        });
            }
            catch (Exception ex) {
                return null;
            }
        }

        [WebMethod]
        public static string IssueInventory(int TrdId, int TrdStatus, int TrId, TRDIssueNote Note,int FromWarehouseId) {
            try {

                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());

                IConversionController conveter = ControllerFactory.CreateConversionController();
                IMeasurementDetailController uomController = ControllerFactory.CreateMeasurementDetailController();

                MeasurementDetail stockUom = uomController.GetStockMaintainingMeasurement(Note.ItemId, CompanyId);

                decimal IssuedQty = 0;
                if (Note.RequestedMeasurementId == Note.MeasurementId) {
                    IssuedQty = Note.IssuedQTY;
                }
                else {
                    IssuedQty = conveter.DoConversion(Note.ItemId, CompanyId, Note.IssuedQTY, Note.MeasurementId, Note.RequestedMeasurementId);
                }

                Note.IssuedBy = int.Parse(HttpContext.Current.Session["UserId"].ToString());
                Note.Status = 1;

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                Inventory inventoryObj = new Inventory();
                inventoryObj.ItemID = Note.ItemId;
                inventoryObj.WarehouseID = Note.WarehouseId;
                inventoryObj.IssuedQty = Note.IssuedQTY;
                inventoryObj.LastUpdatedBy = int.Parse(HttpContext.Current.Session["UserId"].ToString());
                inventoryObj.IssuedStockValue = Note.IssuedStockValue;
                inventoryObj.FromWarehouseId = FromWarehouseId;
                inventoryObj.IssuedBatches = serializer.Deserialize<List<IssuedInventoryBatches>>(serializer.Serialize(Note.IssuedBatches));

                if (stockUom.DetailId != Note.MeasurementId) {
                    inventoryObj.IssuedQty = conveter.DoConversion(Note.ItemId, CompanyId, inventoryObj.IssuedQty, Note.MeasurementId, stockUom.DetailId);

                    for (int i = 0; i < inventoryObj.IssuedBatches.Count; i++) {
                        inventoryObj.IssuedBatches[i].IssuedQty = conveter.DoConversion(Note.ItemId, CompanyId, inventoryObj.IssuedBatches[i].IssuedQty, Note.MeasurementId, stockUom.DetailId);
                    }
                }

                int result = ControllerFactory.CreateInventoryController().UpdateIssuedTRItems(
                    TrdId,
                    IssuedQty,
                    TrdStatus,
                    CompanyId,
                    Note.ItemId,
                    int.Parse(HttpContext.Current.Session["UserId"].ToString()),
                    new List<Inventory> { inventoryObj },
                    new List<TRDIssueNote> { Note },
                    TrId);

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
            catch (Exception ex) {
                return null;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static string GetStockInfo(int ItemId, int WarehouseId, int MeasurementId, decimal RequestedQty, decimal IssuedQty, int RequestedMeasurement) {
            try {
                int CompanyId = int.Parse(HttpContext.Current.Session["CompanyId"].ToString());

                IConversionController conveter = ControllerFactory.CreateConversionController();
                IMeasurementDetailController uomController = ControllerFactory.CreateMeasurementDetailController();

                WarehouseInventoryDetail inventoryDetail = ControllerFactory.CreateInventoryController().GetWarehouseInventoryDetailToIssue(ItemId, WarehouseId, CompanyId);

                if (MeasurementId == RequestedMeasurement) {
                    inventoryDetail.RequestedQty = RequestedQty;
                    inventoryDetail.IssuedQty = IssuedQty;
                }
                else {
                    inventoryDetail.RequestedQty = conveter.DoConversion(ItemId, CompanyId, RequestedQty, RequestedMeasurement, MeasurementId);
                    inventoryDetail.IssuedQty = conveter.DoConversion(ItemId, CompanyId, IssuedQty, RequestedMeasurement, MeasurementId);
                }

                if (inventoryDetail.StockMaintainingUom != MeasurementId) {
                    inventoryDetail.AvailableQty = conveter.DoConversion(ItemId, CompanyId, inventoryDetail.AvailableQty, inventoryDetail.StockMaintainingUom, MeasurementId);
                    inventoryDetail.HoldedQty = conveter.DoConversion(ItemId, CompanyId, inventoryDetail.HoldedQty, inventoryDetail.StockMaintainingUom, MeasurementId);

                    if (inventoryDetail.StockMaintainingType != 1) {
                        for (int i = 0; i < inventoryDetail.Batches.Count; i++) {
                            inventoryDetail.Batches[i].AvailableStock = conveter.DoConversion(ItemId, CompanyId, inventoryDetail.Batches[i].AvailableStock, inventoryDetail.StockMaintainingUom, MeasurementId);
                            inventoryDetail.Batches[i].HoldedQty = conveter.DoConversion(ItemId, CompanyId, inventoryDetail.Batches[i].HoldedQty, inventoryDetail.StockMaintainingUom, MeasurementId);
                        }
                    }
                }

                return JsonConvert.SerializeObject(
                        new {
                            Status = 200,
                            Data = inventoryDetail
                        });
            }
            catch (Exception ex) {
                return null;
            }
        }

    }
}