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
    public partial class Tab4 : System.Web.UI.UserControl
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

        public static List<POImportChargesDefinition> listPOImportChargesDef = new List<POImportChargesDefinition>();
        public static List<POImportChargesDefinition> tempImCharDe4 = new List<POImportChargesDefinition>();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                listPOImportChargesDef.Clear();
                Session["tempImRefDe4"] = null;
                tempImCharDe4.Clear();
                LoadImportChargesDef();
                LoadCurrencyDropdown();
            }
        }

     

        private void LoadImportChargesDef()
        {
            listPOImportChargesDef = poAddImportDetailController.GetImportPOImportChargesDef();
            ddlChargesType.DataSource = listPOImportChargesDef;
            ddlChargesType.DataValueField = "ImportChargeDefId";
            ddlChargesType.DataTextField = "Name";
            ddlChargesType.DataBind();
        }

        private void LoadCurrencyDropdown()
        {
            ddlCurrency.DataSource = unitMeasurementController.fetchCurrency();
            ddlCurrency.DataValueField = "Id";
            ddlCurrency.DataTextField = "Name";
            ddlCurrency.DataBind();
        }

        protected void btnAddImportChargeDefi_Click(object sender, EventArgs e)
        {
            var ddlOrderNumber = (DropDownList)Parent.FindControl("ddlOrderNumber");

            int orderId = Convert.ToInt32(ddlOrderNumber.SelectedValue);
            int importChargesDefId = Convert.ToInt32(ddlChargesType.SelectedValue);
            string value = txtImportChargesValue.Text;
            string currency = ddlCurrency.SelectedItem.Text;
            decimal exchangeRate = Convert.ToDecimal(txtExchangeRate.Text);

            if (btnAddImportChargeDefi.Text != "Update" && tempImCharDe4.Where(x => x.ImportChargeDefId == importChargesDefId && x.OrderId == orderId && x.Currency == currency).ToList().Count != 0)
            {
                lblgvImportReferenceDefiError.Visible = true;
            }
            else
            {
                if (tempImCharDe4.Where(x => x.ImportChargeDefId == importChargesDefId && x.OrderId == orderId && x.Currency == currency).ToList().Count != 0)
                {
                    tempImCharDe4.Find(x => x.ImportChargeDefId == importChargesDefId).Name = value;
                    tempImCharDe4.Find(x => x.ImportChargeDefId == importChargesDefId).Currency = currency;
                    tempImCharDe4.Find(x => x.ImportChargeDefId == importChargesDefId).ExchangeRate = exchangeRate;
                }
                else
                {
                    tempImCharDe4.Add(new POImportChargesDefinition { ImportChargeDefId = importChargesDefId, Name = value, OrderId = orderId , Currency= currency ,ExchangeRate = exchangeRate });
                }
                Session["tempImRefDe4"] = tempImCharDe4;
                gvImportReferenceDefi.DataSource = tempImCharDe4.Where(x => x.OrderId == orderId);
                gvImportReferenceDefi.DataBind();
                btnAddImportChargeDefi.Text = "Add";
                btnAddImportChargeDefi.CssClass = "btn btn-primary pull-right";
                lblgvImportReferenceDefiError.Visible = false;
            }
        }

        protected void btnImportRedEdit_Click(object sender, EventArgs e)
        {

            btnAddImportChargeDefi.Text = "Update";
            btnAddImportChargeDefi.CssClass = "btn btn-danger pull-right";
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            txtImportChargesValue.Text = row.Cells[3].Text;
            ddlChargesType.SelectedValue = row.Cells[1].Text;
            ddlCurrency.SelectedItem.Text = row.Cells[4].Text;
            txtExchangeRate.Text = row.Cells[5].Text;
            //txtOrderNo.Text = row.Cells[0].Text;
        }

        protected void btnImportRedDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int importChargeDefId = Convert.ToInt32(row.Cells[1].Text);
            int orderId = Convert.ToInt32((row.Cells[0].FindControl("lblOrderId") as Label).Text);
            string currency = row.Cells[4].Text;
            tempImCharDe4.Remove(tempImCharDe4.Where(x => x.ImportChargeDefId == importChargeDefId && x.OrderId == orderId && x.Currency == currency).First());
            Session["tempImRefDe4"] = tempImCharDe4;
            gvImportReferenceDefi.DataSource = tempImCharDe4.Where(x => x.OrderId == orderId);
            gvImportReferenceDefi.DataBind();
        }

        protected void btnSaveTab1_Click(object sender, EventArgs e)
        {

        }
    }
}