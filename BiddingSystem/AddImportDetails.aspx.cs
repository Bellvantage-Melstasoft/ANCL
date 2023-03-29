using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data;

namespace BiddingSystem
{
    public partial class AddImportDetails : System.Web.UI.Page
    {
        PR_MasterController prMasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController prDetailController = ControllerFactory.CreatePR_DetailController();
        POImportDetailController poAddImportDetailController = ControllerFactory.CreatePOImportDetailController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PRexpenseController prExpenseController = ControllerFactory.CreatePRexpenseController();
        PRStockDepartmentController prStockDepartmentController = ControllerFactory.CreatePRStockDepartmentController();

       // List<PR_Details> prDetails = new List<PR_Details>();
       // public static string UserId = string.Empty;
      //  int CompanyId = 0;
       // static int departmentId = 0;
       // public static int poId = 0;
      //  List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
       // PR_Master prMaster = new PR_Master();
      //  CompanyLogin companyLogin = new CompanyLogin();

       // public static List<Order> listOrder = new List<Order>();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                List<Order> listOrder = new List<Order>();
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                    ((BiddingAdmin)Page.Master).subTabValue = "ViewPoForAddImportDetails.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "createPoAddImportLink";

                   // departmentId = int.Parse(Session["CompanyId"].ToString());
                  //  CompanyId = int.Parse(Session["CompanyId"].ToString());
                //    UserId = Session["UserId"].ToString();
                   var companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 4, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
                msg.Visible = false;
                if (!IsPostBack)
                {
                   var CompanyLoginUserList = companyLoginController.GetAllUserList();
                   ViewState["poId"]  = Convert.ToInt32(Request.QueryString.Get("PoId"));
                    if (Request.QueryString.Get("PoId") != null || Request.QueryString.Get("PoId") != "")
                    {
                        try
                        {
                            MultiView1.SetActiveView(View1);
                            listOrder.Clear();
                            Session["PoId"] = Request.QueryString.Get("PoId");
                            string prCode = Session["PrCode"].ToString();
                            int prId = Convert.ToInt32(Session["PrId"].ToString());
                            txtPrCode.Text = prCode;
                            string code = prCode.Substring(prCode.LastIndexOf('R') + 1);
                            txtOrderIndentNo.Text = "IMP" + code + "_01_" + DateTime.Now.Year;
                            txtRequisitionNo.Text = "REQ" + Session["PrCode"] +"_"+ DateTime.Now.Year;
                            txtPOCode.Text = Session["PoCode"].ToString();

                            listOrder.Add(new Order { Id = 1, OrderNo = "01" });
                            ddlOrderNumber.DataValueField = "Id";
                            ddlOrderNumber.DataTextField = "OrderNo";
                            ddlOrderNumber.DataSource = listOrder;
                            ddlOrderNumber.DataBind();
                            ViewState["listOrder"] = new JavaScriptSerializer().Serialize(listOrder);
                        }
                        
                        catch (Exception ex)
                        {

                        }
                    }
                    
                }
            }
            catch (Exception)
            {

            }
        }

      

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
                int index = Int32.Parse(e.Item.Value);
                MultiView1.ActiveViewIndex = index;
            
        }

        

        protected void ddlOrderNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var gvImportReferenceDefi = this.Tab1.FindControl("gvImportReferenceDefi") as GridView;
                string prCode = Session["PrCode"].ToString();
                string code = prCode.Substring(prCode.LastIndexOf('R') + 1);
                txtOrderIndentNo.Text = "IMP" + code + "_"+ ddlOrderNumber.SelectedItem.Text + "_"+ DateTime.Now.Year;
                List<POImportReferenceDefinition> obj = (List<POImportReferenceDefinition>)Session["tempImRefDe"];
                gvImportReferenceDefi.DataSource = obj.Where(x => x.OrderId == Convert.ToInt32(ddlOrderNumber.SelectedValue)).ToList();
                gvImportReferenceDefi.DataBind();

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAddNewOrderImportRefe_Click(object sender, EventArgs e)
        {
            var listOrder = new JavaScriptSerializer().Deserialize<List<Order>>(ViewState["listOrder"].ToString());
            int id = Convert.ToInt32(ddlOrderNumber.SelectedValue) + 1;
            string orderNo = id.ToString("00");
            string prCode = Session["PrCode"].ToString();
            string code = prCode.Substring(prCode.LastIndexOf('R') + 1);
            txtOrderIndentNo.Text = "IMP" + code + "_"+ orderNo +"_"+ DateTime.Now.Year;
            listOrder.Add(new Order { Id = id, OrderNo = orderNo });
            ddlOrderNumber.DataSource = listOrder;
            ddlOrderNumber.DataBind();
            ddlOrderNumber.SelectedValue = id.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int save = 0;


                if (save > 0)
                {
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                    //    "none",
                    //    "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });window.location.reload();   </script>",
                    //    false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);

                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void RefreshGrid()
        {
            LoadDataItemGridView();
        }


        private void LoadDataItemGridView()
        {
            try
            {

                //gvDataTable.DataSource = prDetails;
                // gvDataTable.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While loading gridview', showConfirmButton: true,timer: 4000}); });   </script>", false);
                throw ex;
            }
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

    public class Order
    {
        public int Id { set; get; }
        public string OrderNo { set; get; }
    }
}

