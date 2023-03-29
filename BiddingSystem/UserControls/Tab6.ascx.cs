using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem.UserControls
{
    public partial class Tab6 : System.Web.UI.UserControl
    {
        PR_MasterController prMasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController prDetailController = ControllerFactory.CreatePR_DetailController();
        POImportDetailController poAddImportDetailController = ControllerFactory.CreatePOImportDetailController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PRexpenseController prExpenseController = ControllerFactory.CreatePRexpenseController();
        PRStockDepartmentController prStockDepartmentController = ControllerFactory.CreatePRStockDepartmentController();
        List<PR_Details> prDetails = new List<PR_Details>();
        public static string UserId = string.Empty;
        int CompanyId = 0;
        static int departmentId = 0;
        public static int poId = 0;
        List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
        PR_Master prMaster = new PR_Master();
        CompanyLogin companyLogin = new CompanyLogin();

        public static List<POImportChargesDefinition> listPOImportShippingAgentDef = new List<POImportChargesDefinition>();
        public static List<POImportChargesDefinition> tempImCharDe6 = new List<POImportChargesDefinition>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                listPOImportShippingAgentDef.Clear();
                Session["tempImRefDe6"] = null;
                tempImCharDe6.Clear();
                LoadImportShippingAgentDef();
            }
        }

        private void LoadImportShippingAgentDef()
        {
            listPOImportShippingAgentDef.Add(new POImportChargesDefinition { ImportChargeDefId = 1, Name = "DO Charges" });
            listPOImportShippingAgentDef.Add(new POImportChargesDefinition { ImportChargeDefId = 2, Name = "Container Deposit" });
            listPOImportShippingAgentDef.Add(new POImportChargesDefinition { ImportChargeDefId = 3, Name = "Washing Charges" });
            //listPOImportShippingAgentDef = poAddImportDetailController.GetImportPOImportShippingAgentDef();
            ddlImportShippingAgent.DataSource = listPOImportShippingAgentDef;
            ddlImportShippingAgent.DataValueField = "ImportChargeDefId";
            ddlImportShippingAgent.DataTextField = "Name";
            ddlImportShippingAgent.DataBind();
        }

        protected void btnAddImportShippingAgent_Click(object sender, EventArgs e)
        {
            var ddlOrderNumber = (DropDownList)Parent.FindControl("ddlOrderNumber");

            int orderId = Convert.ToInt32(ddlOrderNumber.SelectedValue);
            int importChargesDefId = Convert.ToInt32(ddlImportShippingAgent.SelectedValue);
            string value = txtImportShippingAgentValue.Text;
            string clearing = txtClearing.Text;
            string other = txtOther.Text;

            if (btnAddImportShippingAgentDefi.Text != "Update" && tempImCharDe6.Where(x => x.ImportChargeDefId == importChargesDefId && x.OrderId == orderId).ToList().Count != 0)
            {
                lblgvImportReferenceDefiError.Visible = true;
            }
            else
            {
                if (tempImCharDe6.Where(x => x.ImportChargeDefId == importChargesDefId && x.OrderId == orderId).ToList().Count != 0)
                {
                    tempImCharDe6.Find(x => x.ImportChargeDefId == importChargesDefId).Name = value;
                }
                else
                {
                    tempImCharDe6.Add(new POImportChargesDefinition { ImportChargeDefId = importChargesDefId, Name = value, OrderId = orderId, Clearing = clearing, Others = other });
                }
                Session["tempImRefDe6"] = tempImCharDe6;
                gvImportReferenceDefi.DataSource = tempImCharDe6.Where(x => x.OrderId == orderId);
                gvImportReferenceDefi.DataBind();
                btnAddImportShippingAgentDefi.Text = "Add";
                btnAddImportShippingAgentDefi.CssClass = "btn btn-primary pull-right";
                lblgvImportReferenceDefiError.Visible = false;
            }
        }

        protected void btnImportRedEdit_Click(object sender, EventArgs e)
        {

            btnAddImportShippingAgentDefi.Text = "Update";
            btnAddImportShippingAgentDefi.CssClass = "btn btn-danger pull-right";
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            txtImportShippingAgentValue.Text = row.Cells[3].Text;
           // ddlChargesType.SelectedValue = row.Cells[1].Text;
           // ddlCurrency.SelectedItem.Text = row.Cells[4].Text;
           // txtExchangeRate.Text = row.Cells[5].Text;
            //txtOrderNo.Text = row.Cells[0].Text;
        }

        protected void btnImportRedDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int importChargeDefId = Convert.ToInt32(row.Cells[1].Text);
            int orderId = Convert.ToInt32((row.Cells[0].FindControl("lblOrderId") as Label).Text);
            tempImCharDe6.Remove(tempImCharDe6.Where(x => x.ImportChargeDefId == importChargeDefId && x.OrderId == orderId).First());
            Session["tempImRefDe6"] = tempImCharDe6;
            gvImportReferenceDefi.DataSource = tempImCharDe6.Where(x => x.OrderId == orderId);
            gvImportReferenceDefi.DataBind();
        }

        protected void btnSaveTab1_Click(object sender, EventArgs e)
        {

        }
    }
}