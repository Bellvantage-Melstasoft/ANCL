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
    public partial class Tab2 : System.Web.UI.UserControl
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
        public static int poId = 0;
        List<CompanyLogin> CompanyLoginUserList = new List<CompanyLogin>();
        PR_Master prMaster = new PR_Master();
        CompanyLogin companyLogin = new CompanyLogin();
        public static List<POImportPaymentModeDef> listPOImportPaymentModeDef = new List<POImportPaymentModeDef>();

        public static List<POImportPriceTerms> listPOImportPriceTermsDef = new List<POImportPriceTerms>();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {               
                LoadImportPaymentModeDef();
                LoadImportPriceTerms();
            }
            SetDateValue();
        }

      
        private void SetDateValue()
        {
            TextBox[] textboxes = new TextBox[] { txtDateValue1, txtDateValue2, txtDateValue3, txtDateValue4, txtDateValue5, txtDateValue6, txtDateValue7 , txtDateValue8, txtDateValue9, txtDateValue10, txtDateValue11 };
            foreach (var item in textboxes)
            {
                if (item.Text != "")
                {
                    item.Text = Convert.ToDateTime(item.Text).ToString("yyyy-MM-dd");
                }
            }

        }

        private void LoadImportPriceTerms()
        {
            listPOImportPriceTermsDef = poAddImportDetailController.GetImportPriceTermsDef();
            ddlPriceTerms.DataSource = listPOImportPriceTermsDef;
            ddlPriceTerms.DataValueField = "PriceTermId";
            ddlPriceTerms.DataTextField = "Name";
            ddlPriceTerms.DataBind();
        }

        private void LoadImportPaymentModeDef()
        {
            listPOImportPaymentModeDef = poAddImportDetailController.GetImportPaymentModeDef();
            ddlPaymentMode.DataSource = listPOImportPaymentModeDef;
            ddlPaymentMode.DataValueField = "PaymentModeId";
            ddlPaymentMode.DataTextField = "Name";          
            ddlPaymentMode.DataBind();
        }
        
        protected void btnSaveTab2_Click(object sender, EventArgs e)
        {

        }

        protected void rdoRefundYes_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRefundYes.Checked)
            {
                divRefund.Visible = true;
            }
        }

        protected void rdoRefundNo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoRefundNo.Checked)
            {
                divRefund.Visible = false;
            }
        }

        protected void btnAddNewPaymentNo_Click(object sender, EventArgs e)
        {
            txtPaymentNo.Text = (Convert.ToInt32(txtPaymentNo.Text) + 1).ToString();
        }

        protected void btnClearTab2_Click(object sender, EventArgs e)
        {
            ddlPriceTerms.SelectedIndex = 0;
            ddlPaymentMode.SelectedIndex = 0;
            txtDateValue1.Text = "";
            txtDateValue2.Text = "";
            txtDateValue3.Text = "";
            txtDateValue4.Text = "";
            txtDateValue5.Text = "";
            txtDateValue6.Text = "";
            txtDateValue7.Text = "";
            txtDateValue8.Text = "";
            txtDateValue9.Text = "";
            txtDateValue10.Text = "";
            txtDateValue11.Text = "";
            rdoHypoLoanYes.Checked = true;
            rdoRefundNo.Checked = true;
            divRefund.Visible = false;
        }
    }
}