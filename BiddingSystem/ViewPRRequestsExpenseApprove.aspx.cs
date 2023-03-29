using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.Text;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class ViewPRRequestsExpenseApprove : System.Web.UI.Page
    {
        PR_MasterController PrMasterController = ControllerFactory.CreatePR_MasterController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        public static  List<SubDepartment> listSubDeparment = new List<SubDepartment>();
        public static List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
        public static List<ItemCategory> listItemCategory = new List<ItemCategory>();
        static int UserId = 0;
        static int CompanyId = 0;
        static int subDepartmentId = 0;
        static List<PR_Master> PrMaster = null;
        static int Warehouseid = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewPRRequestsExpenseApprove.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewPRRequestsExpenseApproveLink";


                UserId = int.Parse(Session["UserId"].ToString());
                CompanyId = int.Parse(Session["CompanyId"].ToString());
                subDepartmentId = int.Parse(Session["SubDepartmentID"].ToString());
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
               // listItemCategory = itemCategoryController.FetchItemCategoryList(companyLogin.DepartmentId);
              //  listSubDeparment = subDepartmentController.getDepartmentList(companyLogin.DepartmentId);
                CompanyLoginUserList = companyLoginController.GetAllUserList();
                if ((!companyUserAccessController.isAvilableAccess(UserId, CompanyId, 5, 7) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                LoadPRGridView();
            }else
            {

            }
        }

        private void LoadPRGridView()
        {
            PrMaster = PrMasterController.FetchApprovedPR(CompanyId).OrderByDescending(x => x.CreatedDateTime).ThenBy(t=>t.PrCode).ToList();
            List<PR_Details> prDetails = new List<PR_Details>();
            foreach (PR_Master item in PrMaster)
            {                
                prDetails = PrMasterController.GetOnlyPRDetails(item.PrId, CompanyId);
                item.PrDetails = prDetails;
            }            
            gvPurchaseRequest.DataSource = PrMaster;
            gvPurchaseRequest.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
                int prId = int.Parse(row.Cells[1].Text);
                Response.Redirect("ApproveExpensePR.aspx?PrId=" + prId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int PrID = int.Parse(gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvPrDetails = e.Row.FindControl("gvPrDetails") as GridView;

                gvPrDetails.DataSource = PrMaster.Find(x => x.PrId == PrID).PrDetails;
                gvPrDetails.DataBind();
            }
        }

        protected void gvPurchaseRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            try
            {
                gvPurchaseRequest.PageIndex = e.NewPageIndex;
                gvPurchaseRequest.DataSource = PrMaster;
                gvPurchaseRequest.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
        }

       


    }
}