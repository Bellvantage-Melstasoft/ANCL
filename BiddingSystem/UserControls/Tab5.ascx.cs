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
    public partial class Tab5 : System.Web.UI.UserControl
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
        public static List<POImportChargesDefinition> tempImCharDe5 = new List<POImportChargesDefinition>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                listPOImportChargesDef.Clear();
                Session["tempImRefDe5"] = null;
                tempImCharDe5.Clear();
                LoadImportOtherChargesDef();
                LoadSLPADropdown();
            }
        }

        private void LoadSLPADropdown()
        {
            ddlImportSLPA.Items.Insert(0, new ListItem("SLPA", "0"));
            ddlImportSLPA.DataBind();
        }

        private void LoadImportOtherChargesDef()
        {
            listPOImportChargesDef = poAddImportDetailController.GetImportPOImportCustomChargesDef();
            ddlImportCustomChargeType.DataSource = listPOImportChargesDef;
            ddlImportCustomChargeType.DataValueField = "ImportChargeDefId";
            ddlImportCustomChargeType.DataTextField = "Name";
            ddlImportCustomChargeType.DataBind();
        }

        protected void btnAddImportCustomChaDefi_Click(object sender, EventArgs e)
        {
            var ddlOrderNumber = (DropDownList)Parent.FindControl("ddlOrderNumber");

            int orderId = Convert.ToInt32(ddlOrderNumber.SelectedValue);
            int importChargesDefId = Convert.ToInt32(ddlImportCustomChargeType.SelectedValue);
            string value = txtImportCustomChargeValue.Text;
            int importSLPA = Convert.ToInt32(ddlImportSLPA.SelectedValue);
            string slpa = txtImportSLPA.Text;

            if (btnAddImportCustomChaDefi.Text != "Update" && tempImCharDe5.Where(x => x.ImportChargeDefId == importChargesDefId && x.OrderId == orderId && x.SLPA == slpa).ToList().Count != 0)
            {
                lblgvImportReferenceDefiError.Visible = true;
            }
            else
            {
                if (tempImCharDe5.Where(x => x.ImportChargeDefId == importChargesDefId && x.OrderId == orderId && x.SLPA == slpa).ToList().Count != 0)
                {
                    tempImCharDe5.Find(x => x.ImportChargeDefId == importChargesDefId).Name = value;
                    tempImCharDe5.Find(x => x.ImportChargeDefId == importChargesDefId).SLPA = slpa;
                }
                else
                {
                    tempImCharDe5.Add(new POImportChargesDefinition { ImportChargeDefId = importChargesDefId, Name = value, OrderId = orderId, SLPA = slpa });
                }
                Session["tempImRefDe5"] = tempImCharDe5;
                gvImportReferenceDefi.DataSource = tempImCharDe5.Where(x => x.OrderId == orderId);
                gvImportReferenceDefi.DataBind();
                btnAddImportCustomChaDefi.Text = "Add";
                btnAddImportCustomChaDefi.CssClass = "btn btn-primary pull-right";
                lblgvImportReferenceDefiError.Visible = false;
            }
        }

        protected void btnImportRedEdit_Click(object sender, EventArgs e)
        {
            btnAddImportCustomChaDefi.Text = "Update";
            btnAddImportCustomChaDefi.CssClass = "btn btn-danger pull-right";
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            ddlImportCustomChargeType.SelectedValue = row.Cells[1].Text;
            txtImportCustomChargeValue.Text = row.Cells[3].Text;
        }

        protected void btnImportRedDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int importChargeDefId = Convert.ToInt32(row.Cells[1].Text);
            int orderId = Convert.ToInt32((row.Cells[0].FindControl("lblOrderId") as Label).Text);
            string slpa = txtImportSLPA.Text;
            tempImCharDe5.Remove(tempImCharDe5.Where(x => x.ImportChargeDefId == importChargeDefId && x.OrderId == orderId && x.SLPA == slpa).First());
            Session["tempImRefDe5"] = tempImCharDe5;
            gvImportReferenceDefi.DataSource = tempImCharDe5.Where(x => x.OrderId == orderId);
            gvImportReferenceDefi.DataBind();
        }

        protected void btnSaveTab1_Click(object sender, EventArgs e)
        {

        }
    }
}