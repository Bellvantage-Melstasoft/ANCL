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
    public partial class Tab1 : System.Web.UI.UserControl
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
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                

            }
          
        }
        
        protected void btnSaveTab1_Click(object sender, EventArgs e)
        {
          
        }

        protected void btnClearTab1_Click(object sender, EventArgs e)
        {
            txtLCValue.Text = "";
            txtTNo.Text = "";
            txtGuranteeNo.Text = "";
            txtHypoLoanNo.Text = "";
            txtBLNo.Text = "";
            txtCUSDECNo.Text = "";
            txtCRefNo.Text = "";
        }
    }
}

