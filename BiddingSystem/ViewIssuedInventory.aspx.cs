using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BiddingSystem
{
    public partial class ViewIssuedInventory : System.Web.UI.Page
    {

        //static string UserId = string.Empty;
       // static int warehouseID = 0;
        //int CompanyId = 0;
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        MRNDIssueNoteControllerInterface mrndinController = ControllerFactory.CreateMRNDIssueNoteController();
        InventoryControllerInterface inventoryController = ControllerFactory.CreateInventoryController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefWarehouse";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabWarehouse";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewIssuedInventory.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewIssuedInventoryLink";

               // CompanyId = int.Parse(Session["CompanyId"].ToString());
               // UserId = Session["UserId"].ToString();
                
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 10, 3) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                    txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                    txtFDtD.Text = LocalTime.Now.ToString("MMMM yyyy");

                    int SearchStatusIssued = 0;
                    ViewState["SearchStatusIssued"] = SearchStatusIssued;

                    int SearchStatusDelivered = 0;
                    ViewState["SearchStatusDelivered"] = SearchStatusDelivered;

                    //if (Session["IsHeadOfWarehouse"] != null && Session["IsHeadOfWarehouse"].ToString() == "1")
                    //{
                    //    ViewState["warehouseID"] = int.Parse(Session["WarehouseID"].ToString());
                    if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Count() > 0) {

                        try {
                            gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINList((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList());
                            gvDeliveredInventory.DataBind();

                            gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouse((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList());
                            gvReceivedInventory.DataBind();

                            //gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINListByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), LocalTime.Now);
                            //gvDeliveredInventory.DataBind();

                            //gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouseByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), LocalTime.Now);
                            //gvReceivedInventory.DataBind();


                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be a Warehouse Head to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                    }
                    
                }
            }
        }

        protected void btnReceive_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)(((Button)sender).NamingContainer);

            MRNDIssueNote mrndin = new MRNDIssueNote();
            mrndin.MrndInID= int.Parse(row.Cells[0].Text);
            mrndin.MrndID = int.Parse(row.Cells[1].Text);
            mrndin.DeliveredBy = int.Parse(Session["UserId"].ToString());

            int result = mrndinController.updateIssueNoteAfterDelivered(mrndin);

            if(result>0)
            {
                result = 1;//inventoryController.updateWarehouseStockAfterDelivered(int.Parse(row.Cells[0].Text),(int.Parse(ViewState["warehouseID"].ToString())),int.Parse(row.Cells[2].Text), int.Parse(row.Cells[5].Text), int.Parse(Session["UserId"].ToString()));
                
                if(result>0)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('successMessage').innerHTML = \"Inventory updated successfully\"; $('#SuccessAlert').modal('show'); });   </script>", false);

                    //gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINList((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList());
                    //gvDeliveredInventory.DataBind();
                    gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINListByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), LocalTime.Now);
                    gvDeliveredInventory.DataBind();

                    //gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouse((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList());
                    //gvReceivedInventory.DataBind();
                    gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouseByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), LocalTime.Now);
                    gvReceivedInventory.DataBind();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Material Request Note\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Error on updating Material Request Issue Note\"; $('#errorAlert').modal('show'); });   </script>", false);
            }
        }

        protected void gvDeliveredInventory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvDeliveredInventory.PageIndex = e.NewPageIndex;
                if (ViewState["SearchStatusIssued"].ToString() == "1" && ViewState["SearchStatusIssued"] != null) {
                    if (rdbMonth.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                        gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINListByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), date);
                        gvDeliveredInventory.DataBind();

                    }
                    else {
                        //string newString = Regex.Replace(txtMrnCode.Text, "[^.0-9]", "");
                        //int mrnCode = int.Parse(newString);
                        string mrnCode = txtMrnCode.Text;

                        gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINListByMrnCode((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), mrnCode);
                        gvDeliveredInventory.DataBind();
                    }

                }

                else if (ViewState["SearchStatusIssued"].ToString() == "0" && ViewState["SearchStatusIssued"] != null) {
                    if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Count() > 0) {
                        //gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINListByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), LocalTime.Now);
                        gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINList((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList());
                        gvDeliveredInventory.DataBind();
                    }
                    }
                }
            catch (Exception) {

                throw;
            }
        }

        protected void gvReceivedInventory_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvReceivedInventory.PageIndex = e.NewPageIndex;
                if (ViewState["SearchStatusDelivered"].ToString() == "1" && ViewState["SearchStatusDelivered"] != null) {
                    if (rdbMonthD.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDtD.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                        gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouseByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), date);
                        gvReceivedInventory.DataBind();

                    }
                    else {
                        //string newString = Regex.Replace(txtMrnCodeD.Text, "[^.0-9]", "");
                        //int mrnCode = int.Parse(newString);
                        string mrnCode = txtMrnCode.Text;
                        gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouseByMrnCode((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), mrnCode);
                        gvReceivedInventory.DataBind();
                    }

                }

                else if (ViewState["SearchStatusDelivered"].ToString() == "0" && ViewState["SearchStatusDelivered"] != null) {
                    if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Count() > 0) {
                        //gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouseByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), LocalTime.Now);
                        gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouse((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList());
                        gvReceivedInventory.DataBind();
                    }
                }
            }
            catch (Exception) {

                throw;
            }
        }

        protected void btnBasicSearchIssued_Click(object sender, EventArgs e) {
            int SearchStatusIssued = 1;
            ViewState["SearchStatusIssued"] = SearchStatusIssued;

            if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Count() > 0) {

                try {
                    if (rdbMonth.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                        gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINListByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), date);
                        gvDeliveredInventory.DataBind();

                    }
                    else {
                        //string newString = Regex.Replace(txtMrnCode.Text, "[^.0-9]", "");
                        //int mrnCode = int.Parse(newString);
                        string mrnCode = txtMrnCode.Text;

                        gvDeliveredInventory.DataSource = mrndinController.fetchIssuedMrndINListByMrnCode((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), mrnCode);
                        gvDeliveredInventory.DataBind();
                    }

                }
                catch (Exception ex) {
                    throw ex;
                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be a Warehouse Head to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
            }
        }


        protected void btnBasicSearchDelivered_Click(object sender, EventArgs e) {
            int SearchStatusDelivered = 1;
            ViewState["SearchStatusDelivered"] = SearchStatusDelivered;

            if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Count() > 0) {

                try {
                    if (rdbMonthD.Checked) {
                        DateTime date = DateTime.ParseExact(txtFDtD.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                        gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouseByMonth((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), date);
                        gvReceivedInventory.DataBind();

                    }
                    else {
                        //string newString = Regex.Replace(txtMrnCodeD.Text, "[^.0-9]", "");
                        //int mrnCode = int.Parse(newString);
                        string mrnCode = txtMrnCode.Text;

                        gvReceivedInventory.DataSource = mrndinController.fetchDeliveredMrndINListWarehouseByMrnCode((Session["UserWarehouses"] as List<UserWarehouse>).Where(d => d.UserType == 1).Select(d => d.WrehouseId).ToList(), mrnCode);
                        gvReceivedInventory.DataBind();
                    }

                }
                catch (Exception ex) {
                    throw ex;
                }
            }
            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be a Warehouse Head to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
            }
        }



        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int issunoteId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                issunoteId = int.Parse(gvReceivedInventory.Rows[x].Cells[0].Text);
                Response.Redirect("WarehouseIssuNote.aspx?MrnissueId=" + issunoteId+ "&&TYPE=R");
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('CompanyPRReportView.aspx?PrId="+ PrId + "');", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}