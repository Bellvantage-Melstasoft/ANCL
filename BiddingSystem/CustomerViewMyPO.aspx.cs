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
    public partial class CustomerViewMyPO : System.Web.UI.Page
    {
       // static string UserId = string.Empty;
       // int CompanyId = 0;
       // int DesignationId = 0;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
        //public static CompanyLogin companyLogin = new CompanyLogin();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerViewMyPO.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "ViewMyPOLink";

               // CompanyId = int.Parse(Session["CompanyId"].ToString());
              //  UserId = Session["UserId"].ToString();
               var companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                ViewState["DesignationId"] = companyLogin.DesignationId;
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 21) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
                //else if (Session["DesignationId"] == null || Session["DesignationId"].ToString() != "14")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be Head of Procurement to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                //}
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

                        
                        ////if ((Session["IsHeadOfProcurement"] != null && Session["IsHeadOfProcurement"].ToString() == "1") || (int.Parse(ViewState["DesignationId"].ToString()) == 25)) // Head Of Procurment
                        ////{
                            LoadDCatregory();
                            txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                            string prCode = "";
                            string poCode = "";
                            int poType = 0;
                            List<int> CategoryIds = new List<int>();
                            List<int> warehouseIds = new List<int>();
                            chkMonth.Checked = true;

                            if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Count > 0) {
                                LoadUserWarehouses();


                            }
                            else {
                                LoadCompanyWarehouses();

                            }

                            gvPurchaseOrder.DataSource = pOMasterController.ViewMyPOS(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), LocalTime.Now, prCode, poCode, CategoryIds, warehouseIds, poType);
                            gvPurchaseOrder.DataBind();


                            //List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
                            //pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentId(int.Parse(Session["CompanyId"].ToString())).Where(W => W.IsApproved == 0).OrderByDescending(r => r.CreatedDate).ThenBy(i => i.POCode).ToList();
                            //if (Request.QueryString.Get("UserId") != null)
                            //{
                            //    gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderByDescending(x => x.PoID).ToList();
                            //    gvPurchaseOrder.DataBind();
                            //}
                            //else
                            //{
                            //    gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderByDescending(x => x.PoID).ToList();
                            //    gvPurchaseOrder.DataBind();
                            //}
                        ////}else
                        ////{
                        ////    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You have to be Head Of Procurement or Purchasing Officer', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                        ////}
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void LoadDCatregory() {
            try {

                ddlCategory.DataSource = ControllerFactory.CreateItemCategoryController().FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString())).Where(x => x.IsActive == 1);
                ddlCategory.DataValueField = "CategoryId";
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataBind();
                //ddlCategory.Items.Insert(0, new ListItem("Select Category", ""));
            }
            catch (Exception ex) {
            }
        }

        private void LoadUserWarehouses() {
            ddlWarehouse.DataSource = ControllerFactory.CreateUserWarehouse().GetWarehousesByUserId(int.Parse(Session["UserId"].ToString()));
            ddlWarehouse.DataValueField = "WrehouseId";
            ddlWarehouse.DataTextField = "Location";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("Not Found", "0"));
        }

        private void LoadCompanyWarehouses() {
            ddlWarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
            ddlWarehouse.DataValueField = "WarehouseID";
            ddlWarehouse.DataTextField = "Location";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("Not Found", "0"));
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int PoId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Session["PoId"] = PoId;
                // Response.Redirect("CompanyViewAndApprovePO.aspx");
                Response.Redirect("ViewPO.aspx?PoId=" + PoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e) {
            //DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
            if (chkMonth.Checked || chkPrCode.Checked || chkCategory.Checked || chkWarehouse.Checked || chkPoCode.Checked ) {

                DateTime date = DateTime.MinValue;
                if (chkMonth.Checked) {
                    date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);
                }

                string prCode = "";
                if (chkPrCode.Checked && txtPrCode.Text != "") {
                    //string newString = Regex.Replace(txtPrCode.Text, "[^.0-9]", "");
                    //prCode = int.Parse(newString);
                    prCode = txtPrCode.Text;
                }
                
                string poCode = "";
                if (chkPoCode.Checked) {
                    poCode = txtPoCode.Text;
                }

                int poType = 0;
                //if (chkPoType.Checked) {
                //    poType = int.Parse(ddlPoType.SelectedValue);
                //}

                List<int> CategoryIds = new List<int>();
                if (chkCategory.Checked) {
                    for (int i = 0; i < ddlCategory.Items.Count; i++) {
                        if (ddlCategory.Items[i].Selected) {
                            CategoryIds.Add(int.Parse(ddlCategory.Items[i].Value));
                        }
                    }
                }

                List<int> warehouseIds = new List<int>();
                if (chkWarehouse.Checked) {
                    for (int i = 0; i < ddlWarehouse.Items.Count; i++) {
                        if (ddlWarehouse.Items[i].Selected) {
                            warehouseIds.Add(int.Parse(ddlWarehouse.Items[i].Value));
                        }
                    }
                }

                
                gvPurchaseOrder.DataSource = pOMasterController.ViewMyPOS(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), date, prCode, poCode, CategoryIds, warehouseIds, poType);
                gvPurchaseOrder.DataBind();

                //gvPurchaseOrder.DataSource = poMasterController.ViewAllPOS(int.Parse(Session["CompanyId"].ToString()), date, prCode, poCode, CategoryIds, warehouseIds, poType);
                //gvPurchaseOrder.DataBind();

            }
            else {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Please Select At Least One Sorting Option', showConfirmButton: false,timer: 1500}); });   </script>", false);
            }

        }

        protected void gvPurchaseOrder_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
            if (Request.QueryString.Get("UserId") != null)
            {
                int clickedUserId = int.Parse(Request.QueryString.Get("UserId"));
                List<ItemCategoryPOApproval> listItemCategoryPOApproval = itemCategoryApprovalController.GetItemCategoryPOApprovalByDesignationId(int.Parse(ViewState["DesignationId"].ToString()));
                pOMasterListByDepartmentid = pOMasterListByDepartmentid.Where(p => listItemCategoryPOApproval.Any(p2 => p2.Po_Id == p.PoID)).ToList();
                gvPurchaseOrder.PageIndex = e.NewPageIndex;
                gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderByDescending(x => x.PoID).ToList();
                gvPurchaseOrder.DataBind();
            }
            else
            {
                gvPurchaseOrder.PageIndex = e.NewPageIndex;
                gvPurchaseOrder.DataSource = pOMasterListByDepartmentid.OrderByDescending(x => x.PoID).ToList();
                gvPurchaseOrder.DataBind();
            }
        }
    }
}