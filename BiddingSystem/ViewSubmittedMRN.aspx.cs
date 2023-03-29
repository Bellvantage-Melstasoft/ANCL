using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Services;

namespace BiddingSystem
{
    public partial class ViewSubmittedMRN : System.Web.UI.Page
    {

        static string UserId = string.Empty;
        static int SubDepartmentID = 0;
        int CompanyId = 0;
        static int mrndID = 0;
        static int itemID = 0;
        static int availableQty = 0;
        static int requestedQty = 0;
        static int issuedQty = 0;
        static int pendingQty = 0;
        static int addedQty = 0;
        static int mrnID = 0;
        static int whouseId = 0;
        static decimal unitprice = 0;
       
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        ItemCategoryOwnerController itemCategoryOwnerController = ControllerFactory.CreateItemCategoryOwnerController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        InventoryControllerInterface inventoryController = ControllerFactory.CreateInventoryController();
        MRNDIssueNoteControllerInterface mrndINController = ControllerFactory.CreateMRNDIssueNoteController();
        MRNmasterController mrnmasterController = ControllerFactory.CreateMRNmasterController();
        MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        public string datetimepattern = System.Configuration.ConfigurationSettings.AppSettings["datePattern"];
        MrnDetailsStatusLogController logController = ControllerFactory.CreateMrnDetailStatusLogController();
        static List<int> mrndList = new List<int>();
        static List<MrnMaster> mrnMaster = new List<MrnMaster>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewSubmittedMRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "submittedMRNLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 12, 4) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (int.Parse(UserId) != 0)
                {
                    try
                    {
                        GVLoad();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
               
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                //int mrnID = 0;
                //int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                //mrnID = int.Parse(gvMRN.Rows[x].Cells[1].Text);
                //Response.Redirect("EditMRN.aspx?id=" + mrnID);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void GVLoad()
        {
            var ishodwh = int.Parse(Session["IsHeadOfWarehouse"].ToString());
            
            if (ishodwh == 1)
            {
                whouseId = int.Parse(Session["WarehouseID"].ToString());
                mrnMaster = mrnmasterController.fetchSubmittedMrnListbywarehouseId(CompanyId, whouseId).OrderByDescending(x => x.CreatedDate).ToList();
                List<MrnDetails> mrnDetails = new List<MrnDetails>();
                foreach (MrnMaster item in mrnMaster)
                {
                    mrnDetails = mrnmasterController.fetchSubmittedMrnDListbywarehouse(item.MrnID, whouseId);
                    item.MrnDetails = mrnDetails;
                }
                List<MrnMaster> tempmrn = mrnMaster.Where(x => x.MrnDetails.Count == 0).ToList();
                mrnMaster = mrnMaster.Where(x => x.MrnDetails.Count > 0).ToList();
                mrnMaster = mrnMaster.OrderBy(x => x.MrnDetails.Max(t => t.Status)).ToList();
                mrnMaster.AddRange(tempmrn);
                gvviewMRNhead.DataSource = mrnMaster;
                gvviewMRNhead.DataBind();

                panelApprovRejectMRN.Visible = false;
                pnlOtheruser.Visible = false;
                panelforheadofWH.Visible = true;
            }
            else if (int.Parse(Session["WarehouseID"].ToString()) > 0)

            {

                whouseId = int.Parse(Session["WarehouseID"].ToString());
                mrnMaster = mrnmasterController.fetchSubmittedMrnListbywarehouseId(CompanyId, whouseId).OrderByDescending(x => x.CreatedDate).ToList().Where(x => x.StoreKeeperId == int.Parse(UserId)).ToList();
                List<MrnDetails> mrnDetails = new List<MrnDetails>();
                foreach (MrnMaster item in mrnMaster)
                {
                    mrnDetails = mrnmasterController.fetchSubmittedMrnDListbywarehouse(item.MrnID, whouseId);
                    item.MrnDetails = mrnDetails;
                }
                List<MrnMaster> tempmrn = mrnMaster.Where(x => x.MrnDetails.Count == 0).ToList();
                mrnMaster = mrnMaster.Where(x => x.MrnDetails.Count > 0).ToList();
                mrnMaster = mrnMaster.OrderBy(x => x.MrnDetails.Max(t => t.Status)).ToList();
                mrnMaster.AddRange(tempmrn);
                gvApprovRejectMRN.DataSource = mrnMaster;
                gvApprovRejectMRN.DataBind();
                panelApprovRejectMRN.Visible = true;
                pnlOtheruser.Visible = false;
                panelforheadofWH.Visible = false;
              
            }
            else
            {

                gvotheruser.DataSource = mrnmasterController.fetchSubmittedMrnListForOther(CompanyId).OrderByDescending(x => x.CreatedDate).ToList();
                gvotheruser.DataBind();
                panelApprovRejectMRN.Visible = false;
                pnlOtheruser.Visible = true;
                panelforheadofWH.Visible = false;
            }

        }



        protected void gvApprovRejectMRN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (whouseId>0)
                {

               
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int mrnID = int.Parse(gvApprovRejectMRN.DataKeys[e.Row.RowIndex].Value.ToString());
                    GridView gvMRNDetails = e.Row.FindControl("gvApprovRejectMRND") as GridView;
                    if (whouseId==0)
                    {
                            //List<MrnDetails> a = mrnmasterController.fetchSubmittedMrnDList(mrnID, CompanyId);
                            List<MrnDetails> a = new List<MrnDetails>();
                            if (mrnMaster.Find(x => x.MrnID == mrnID) != null)
                            {
                                a = mrnMaster.Find(x => x.MrnID == mrnID).MrnDetails;
                            }
                            gvMRNDetails.DataSource = a;
                        gvMRNDetails.DataBind();
                    }
                    else
                    {
                        List<MrnDetails> a = mrnmasterController.fetchSubmittedMrnDListbywarehouse(mrnID, whouseId);
                        if (a.Any(x=>x.Status==1))
                        {
                            Button btnaddtopr = (e.Row.FindControl("btnAddToPR") as Button);
                            btnaddtopr.Visible = false;
                        }
                        else if (a.All(x=>x.Status>2))
                        {
                            Button btnaddtopr = (e.Row.FindControl("btnAddToPR") as Button);
                            btnaddtopr.Visible = false;
                        }
                        gvMRNDetails.DataSource = a;
                        gvMRNDetails.DataBind();
                    }
                }
                }
                else
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        int mrnID = int.Parse(gvotheruser.DataKeys[e.Row.RowIndex].Value.ToString());
                        GridView gvMRNDetails = e.Row.FindControl("gvApprovRejectMRND") as GridView;
                        if (whouseId == 0)
                        {
                            //List<MrnDetails> a = mrnmasterController.fetchSubmittedMrnDList(mrnID, CompanyId);
                            gvMRNDetails.DataSource = mrnmasterController.fetchSubmittedMrnDList(mrnID, CompanyId);
                            gvMRNDetails.DataBind();
                        }
                        else
                        {
                            List<MrnDetails> a = mrnmasterController.fetchSubmittedMrnDListbywarehouse(mrnID, whouseId);
                            if (a.Any(x => x.Status == 1))
                            {
                                Button btnaddtopr = (e.Row.FindControl("btnAddToPR") as Button);
                                btnaddtopr.Visible = false;
                            }
                            else if (a.All(x => x.Status > 2))
                            {
                                Button btnaddtopr = (e.Row.FindControl("btnAddToPR") as Button);
                                btnaddtopr.Visible = false;
                            }
                            gvMRNDetails.DataSource = a;
                            gvMRNDetails.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnIssue_Click(object sender, EventArgs e)
        {

           
            GridView gv = gvWarehouseInventory;
            int result = 0;
            int issued = 0;
            int mrndStatus = 0;
            MRNDIssueNote notes = new MRNDIssueNote();
            Inventory inventoryObjList = new Inventory();

            GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
            TextBox txt = (TextBox)row.FindControl("IssuedQty");
            if (txt.Text != null && txt.Text != "" && txt.Text != "0")
            {
                int qty = int.Parse(txt.Text);
                if ((requestedQty - issuedQty) >= qty)
                {
                    if (qty > 0)
                    {


                        int warehouse = int.Parse(row.Cells[0].Text);
                        int item = int.Parse(row.Cells[1].Text);


                        notes.MrndID = mrndID;
                        notes.ItemID = item;
                        notes.WarehouseID = warehouse;
                        notes.IssuedQty = qty;
                        notes.IssuedBy = int.Parse(Session["UserId"].ToString());
                        notes.Status = 1;

                        inventoryObjList.ItemID = item;
                        inventoryObjList.WarehouseID = warehouse;
                        inventoryObjList.IssuedQty = qty;
                        inventoryObjList.LastUpdatedBy = int.Parse(Session["UserId"].ToString());
                        issued += qty;
                    }
                    if (issued + issuedQty < requestedQty)
                        mrndStatus = 2;
                    else
                        mrndStatus = 3;

                    result = mrnmasterController.updateMRNDIssuedQty(mrndID, issued);

                    if (result > 0)
                    {
                        result = mrnmasterController.changeMRNDStaus(mrndID, mrndStatus);

                        if (result > 0)
                        {
                            result = inventoryController.updateCompanyStockAfterIssue(int.Parse(Session["CompanyId"].ToString()), itemID, issued, int.Parse(Session["UserId"].ToString()),0);

                            if (result > 0)
                            {
                                result = inventoryController.updateWarehouseStockAfterIssuesformonewarehouse(inventoryObjList);



                                if (result > 0)
                                {
                                    result = mrndINController.addNewIssueNoteonewarehouse(notes);

                                    if (result > 0)
                                    {
                                        MRNDIssueNote mrndin = new MRNDIssueNote();
                                        mrndin.MrndInID = result;
                                        mrndin.DeliveredBy = int.Parse(Session["UserId"].ToString());

                                        result = mrndinController.updateIssueNoteAfterDelivered(mrndin);
                                        if (result > 0)
                                        {
                                            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $(document).ready(function () { $('#mdlIssueStock').modal('hide'); });  </script>", false);
                                            ////ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"Stock has been successfully issued\"; $('#SuccessAlert').modal('show'); });   </script>", false);
                                            mrnmasterController.updateMRNAfterIssue(mrnID);
                                            //gvApprovRejectMRN.DataSource = mrnController.fetchSubmittedMrnList(int.Parse(Session["CompanyId"].ToString())).ToList();
                                            //gvApprovRejectMRN.DataBind();
                                            GVLoad();
                                            Response.Redirect("WarehouseIssuNote.aspx?MrnissueId=" + mrndin.MrndInID);

                                            //Response.Redirect("ViewSubmittedMRN.aspx");



                                        }
                                        else
                                        {

                                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on adding MRN item issue note\"; $('#errorAlert').modal('show'); });   </script>", false);
                                        }
                                    }

                                  
                                    else
                                    {
                                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on adding MRN item issue note\"; $('#errorAlert').modal('show'); });   </script>", false);

                                    }
                                }
                                else
                                {
                                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Warehouse stock\"; $('#errorAlert').modal('show'); });   </script>", false);

                                }
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Company stock\"; $('#errorAlert').modal('show'); });   </script>", false);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on changing MRN item status\"; $('#errorAlert').modal('show'); });   </script>", false);

                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating MRN item issued QTY\"; $('#errorAlert').modal('show'); });   </script>", false);

                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating MRN item issued QTY\"; $('.modal-backdrop').remove(); $('#errorAlert').modal('show'); });   </script>", false);
                }



            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating MRN item issued QTY\";$('.modal-backdrop').remove(); $('#errorAlert').modal('show'); });   </script>", false);
            }
            

         


            
        }

        protected void btnIssueFromStock_Click(object sender, EventArgs e)
        {
            addedQty = 0;
            GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
            
            mrndID = int.Parse(row.Cells[0].Text);
            itemID = int.Parse(row.Cells[3].Text);
            availableQty = int.Parse(row.Cells[8].Text);
            requestedQty = int.Parse(row.Cells[5].Text);
            issuedQty = int.Parse(row.Cells[6].Text);
            pendingQty = requestedQty - issuedQty;
            mrnID= int.Parse(row.Cells[13].Text);
            gvWarehouseInventory.DataSource = inventoryController.fetchWarehouseInventorybyWarehouseId(int.Parse(Session["CompanyId"].ToString()), int.Parse(Session["WarehouseID"].ToString()), int.Parse(row.Cells[3].Text));
            gvWarehouseInventory.DataBind();

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $(document).ready(function () { document.getElementById('requestedQtyShow').innerHTML = '"+requestedQty+ "'; document.getElementById('issuedQtyShow').innerHTML = '" + issuedQty + "'; document.getElementById('pendingQtyShow').innerHTML = '" + pendingQty + "'; document.getElementById('availableQtyShow').innerHTML = '" + availableQty + "'; $('#mdlIssueStock').modal('show'); });  </script>", false);

        }

        //protected void IssuedQty_TextChanged(object sender, EventArgs e)
        //{
            
        //    GridView gv = (GridView)((GridViewRow)((TextBox)sender).NamingContainer).NamingContainer;

        //    int added = 0;
        //    int pending = pendingQty;
        //    int available = availableQty;

        //    foreach(GridViewRow row in gv.Rows)
        //    {
        //        TextBox txt = (TextBox)row.FindControl("IssuedQty");
        //        if (txt.Text != "0")
        //        {
        //            int qty = 0;
        //            if (txt.Text != "")
        //                qty = int.Parse(txt.Text);

        //            if (qty >= int.Parse(row.Cells[3].Text))
        //                qty = int.Parse(row.Cells[3].Text);

        //            if (pending > 0)
        //            {
        //                if (qty <= available)
        //                {
        //                    if (qty <= pending)
        //                    {
        //                        txt.Text = qty.ToString();
        //                        available -= qty;
        //                        pending -= qty;
        //                    }
        //                    else
        //                    {
        //                        txt.Text = pending.ToString();
        //                        available -= pending;
        //                        pending = 0;
        //                    }
        //                }
        //                else
        //                {
        //                    if(pending>available)
        //                    {
        //                        if(qty<=pending)
        //                        {
        //                            txt.Text = qty.ToString();
        //                            available =0;
        //                            pending -= qty;
        //                        }
        //                        else
        //                        {
        //                            txt.Text = pending.ToString();
        //                            available = 0;
        //                            pending =0;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (qty <= pending)
        //                        {
        //                            txt.Text = qty.ToString();
        //                            available -= qty;
        //                            pending -= qty;
        //                        }
        //                        else
        //                        {
        //                            txt.Text = pending.ToString();
        //                            available -= pending;
        //                            pending = 0;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                txt.Text = "0";
        //            }

                    
        //        }
        //    }
        //    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $(document).ready(function () { document.getElementById('requestedQtyShow').innerHTML = '" + requestedQty + "'; document.getElementById('issuedQtyShow').innerHTML = '" + issuedQty + "'; document.getElementById('pendingQtyShow').innerHTML = '" + pendingQty + "'; document.getElementById('availableQtyShow').innerHTML = '" + availableQty + "'; $('#mdlIssueStock').modal('show'); });  </script>", false);
        //}

        protected void btnAddToPR_Click(object sender, EventArgs e)
        {
            
            GridViewRow newRow = ((GridViewRow)((Button)sender).NamingContainer);

            var prcode=pR_MasterController.SaveMRNtoPR(int.Parse(newRow.Cells[1].Text), int.Parse(UserId), CompanyId);
            if (prcode !=null)
            {
                var list = mrnmasterController.fetchSubmittedMrnDList(int.Parse(newRow.Cells[1].Text), CompanyId);
                foreach (var item in list)
                {
                    if (item.Status<3)
                    {
                        mrnmasterController.changeMRNDStaus(item.Mrnd_ID, 1);
                    }
                }
               
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500});});   </script>", false);
                // Session["FinalListPRCreate"] = tempDataSetlist;
                mrndList = new List<int>();
                Response.Redirect("CustomerPREdit.aspx?PrCode=" + prcode + "");
            }

            if (mrndList.Count > 0) {
                foreach (int mrnd in mrndList) {
                   // mrnController.changeMRNDStaus(mrnd, 1);
                    logController.InsertLog(mrnd, int.Parse(Session["UserId"].ToString()), 7);
                }
            }

        }

      

        protected void btnaddinventry_Click(object sender, EventArgs e)
        {
            GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);

            mrndID = int.Parse(row.Cells[0].Text);
            itemID = int.Parse(row.Cells[3].Text);
            txtItemName.Text = row.Cells[4].Text;
            unitprice = decimal.Parse(row.Cells[12].Text);
            if (unitprice==0)
            {
                txtUnitPrice.Enabled = true;
            }
            else
            {
                txtUnitPrice.Enabled = false;
                txtUnitPrice.Text = unitprice.ToString();
            }

            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $(document).ready(function () {$('#mdladdtoinventory').modal('show'); });  </script>", false);
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            WarehouseInventoryRaise warehouseInventoryRaise = new WarehouseInventoryRaise();
            warehouseInventoryRaise.ItemID = itemID;
            
            warehouseInventoryRaise.RaisedBy = int.Parse(UserId);
            warehouseInventoryRaise.RaisedQty = int.Parse(txtQuantity.Text);
            warehouseInventoryRaise.RaisedWarehouseID = whouseId;
            warehouseInventoryRaise.WarehouseID = whouseId;
            warehouseInventoryRaise.StockValue = decimal.Parse(txtUnitPrice.Text) * decimal.Parse(txtQuantity.Text);
            //int Issaved=inventoryController.raiseWarehouseStockInMrnManual(warehouseInventoryRaise);
            //if (Issaved > 0)
            //{
            //    Issaved = inventoryController.raiseCompanyStockInMrn(CompanyId, itemID, int.Parse(txtQuantity.Text), warehouseInventoryRaise.StockValue, int.Parse(UserId));
            //    if (Issaved>0)
            //    {
            //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='ViewSubmittedMRN.aspx'; });   </script>", false);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Warehouse stock\"; $('#errorAlert').modal('show'); });   </script>", false);
            //    }
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Warehouse stock\"; $('#errorAlert').modal('show'); });   </script>", false);
            //}
           
        }

        protected void lbtntagstorekeeper_Click(object sender, EventArgs e)
        {
            GridViewRow Row = ((GridViewRow)((Button)sender).NamingContainer);
            mrndID = int.Parse(Row.Cells[1].Text);
            DropDownList DropDownList1 = (Row.FindControl("ddlStorekeeper") as DropDownList);
            var storkeeper=DropDownList1.SelectedValue;
           int isupdated= mrnmasterController.UpdateMRMasterwithStoreKeeper(mrndID,int.Parse(storkeeper));
            if (isupdated>0)
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); window.location='ViewSubmittedMRN.aspx'; });   </script>", false);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on inserting Store keeper stock\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }

        protected void gvviewMRNhead_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    int mrnID = int.Parse(gvviewMRNhead.DataKeys[e.Row.RowIndex].Value.ToString());
                    List<MrnDetails> a = new List<MrnDetails>();
                    if (mrnMaster.Find(x => x.MrnID == mrnID) != null)
                    {
                        a = mrnMaster.Find(x => x.MrnID == mrnID).MrnDetails;
                    }
                    if (a.Any(x => x.Status == 1))
                    {
                        Button btnaddtopr = (e.Row.FindControl("btnAddToPR") as Button);
                        btnaddtopr.Visible = false;
                    }
                    else if (a.All(x => x.Status > 2))
                    {
                        Button btnaddtopr = (e.Row.FindControl("btnAddToPR") as Button);
                        btnaddtopr.Visible = false;
                    }
                    GridView gvMRNDetails = e.Row.FindControl("gvviewMRNheadD") as GridView;

                    //List<MrnDetails> a = mrnController.fetchSubmittedMrnDList(mrnID, CompanyId);
                    gvMRNDetails.DataSource = a;
                    gvMRNDetails.DataBind();

                    DropDownList DropDownList1 = (e.Row.FindControl("ddlStorekeeper") as DropDownList);
                    DropDownList1.DataSource = itemCategoryOwnerController.FetchAllItemCategoryOwners("SK");
                    DropDownList1.DataTextField = "FName";
                    DropDownList1.DataValueField = "UserId";
                    DropDownList1.DataBind();
                    DropDownList1.Items.Insert(0, new ListItem("--Select Storekeeper--", "0"));
                    if (e.Row.Cells[9].Text != "0")
                    {
                        DropDownList1.SelectedValue = e.Row.Cells[9].Text;
                        DropDownList1.Enabled = false;
                        Button btnstorekeeper = (e.Row.FindControl("lbtntagstorekeeper") as Button);
                        btnstorekeeper.Enabled = false;

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvviewMRNhead_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvviewMRNhead.PageIndex = e.NewPageIndex;
                gvviewMRNhead.DataSource = mrnMaster;
                gvviewMRNhead.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //protected void gvApprovRejectMRND_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    Button Btn1 = e.Row.FindControl("btnIssueFromStock") as Button;
        //    ScriptManager.GetCurrent(this.Parent.Page).RegisterAsyncPostBackControl(Btn1);
        //}
    }
}