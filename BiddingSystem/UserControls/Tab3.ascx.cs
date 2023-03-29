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
    public partial class Tab3 : System.Web.UI.UserControl
    {
        PR_MasterController prMasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController prDetailController = ControllerFactory.CreatePR_DetailController();
        POImportDetailController poImportDetailController = ControllerFactory.CreatePOImportDetailController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
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
        public List<POImportTransportModeDef> listTransportMode = new List<POImportTransportModeDef>();
        public static List<CommonReference> listVessel = new List<CommonReference>();
        public static List<CommonReference> listShippingAgent = new List<CommonReference>();
        public static List<CommonReference> listClearingAgent = new List<CommonReference>();
        public static List<CommonReference> listInsurancePolicyDetails = new List<CommonReference>();
        public static List<CommonReference> listContainerSize = new List<CommonReference>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                LoadTransportMode();
                LoadQty();
                LoadContainerSize();
                ClearSession();
            }
            SetDateValue();
        }

        private void SetDateValue()
        {
            TextBox[] textboxes = new TextBox[] { txtOInsuranceDate, txtPerformanceDate };
            foreach (var item in textboxes)
            {
                if (item.Text != "")
                {
                    item.Text = Convert.ToDateTime(item.Text).ToString("yyyy-MM-dd");
                }
            }
        }

        private void ClearSession()
        {
            Session["listVessel"] = null;
            Session["listShippingAgent"] = null;
            Session["listClearingAgent"] = null;
            Session["listInsurancePolicyDetails"] = null;
            listVessel.Clear();
            listShippingAgent.Clear();
            listClearingAgent.Clear();
            listInsurancePolicyDetails.Clear();
        }

        private void LoadContainerSize()
        {
            listContainerSize.Clear();
            listContainerSize.Add(new CommonReference { Id = 1, Name = "20" });
            listContainerSize.Add(new CommonReference { Id = 2, Name = "40" });
            listContainerSize.Add(new CommonReference { Id = 3, Name = "LCL" });
            ddlContainerSize.DataSource = listContainerSize;
            ddlContainerSize.DataTextField = "Name";
            ddlContainerSize.DataValueField = "Id";
            ddlContainerSize.DataBind();
        }

        private void LoadQty()
        {
            ddlMeasurement.DataSource = unitMeasurementController.fetchMeasurementsByCompanyID(int.Parse(Session["CompanyId"].ToString()));
            ddlMeasurement.DataValueField = "measurentId";
            ddlMeasurement.DataTextField = "measurementShortName";
            ddlMeasurement.DataBind();
        }

        private void LoadTransportMode()
        {
            listTransportMode = poImportDetailController.GetImportTransportModeDef();
            ddlTransportMode.DataSource = listTransportMode;
            ddlTransportMode.DataTextField = "Name";
            ddlTransportMode.DataValueField = "Id";
            ddlTransportMode.DataBind();
        }

        protected void btnSaveTab1_Click(object sender, EventArgs e)
        {

        }

        protected void btnShippingAgent_Click(object sender, EventArgs e)
        {
            divShippingAgent.Visible = true;
            int transportModeId = Convert.ToInt32(ddlTransportMode.SelectedValue);
            int referenceId = 1;
            string name = txtOShippingAgent.Text;
            if (listShippingAgent.Where(x => x.Id == transportModeId && x.Name == name).ToList().Count != 0)
            {
                lblgvShippingAgent.Visible = true;
            }
            else
            {
                if (listShippingAgent.Count > 0)
                {
                    referenceId = listShippingAgent.Max(x => x.ReferenceId) + 1;
                }
                listShippingAgent.Add(new CommonReference { ReferenceId = referenceId, Id = transportModeId, Name = name });
                gvShippingAgent.DataSource = listShippingAgent;
                gvShippingAgent.DataBind();
                Session["listShippingAgent"] = listShippingAgent;
                lblgvShippingAgent.Visible = false;
            }
        }

        protected void btnShippingAgentClear_Click(object sender, EventArgs e)
        {
            txtOShippingAgent.Text = "";
        }

        protected void gvShippingAgentDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int referenceId = Convert.ToInt32(row.Cells[0].Text);
            int transportModeId = Convert.ToInt32((row.Cells[1].FindControl("lblTransportId") as Label).Text);
            string name = row.Cells[3].Text;
            listShippingAgent.Remove(listShippingAgent.Where(x => x.ReferenceId == referenceId && x.Id == transportModeId && x.Name == name).First());
            Session["listShippingAgent"] = listShippingAgent;
            gvShippingAgent.DataSource = listShippingAgent;
            gvShippingAgent.DataBind();
        }

        protected void btnVessel_Click(object sender, EventArgs e)
        {
            divVessel.Visible = true;
            int transportModeId = Convert.ToInt32(ddlTransportMode.SelectedValue);
            int referenceId = 1;
            string name = txtOVessel.Text;
            if (listVessel.Where(x => x.Id == transportModeId && x.Name == name).ToList().Count != 0)
            {
                lblgvVessel.Visible = true;
            }
            else
            {
                if (listVessel.Count > 0)
                {
                    referenceId = listVessel.Max(x => x.ReferenceId) + 1;
                }
                listVessel.Add(new CommonReference { ReferenceId = referenceId, Id = transportModeId, Name = name });
                gvVessel.DataSource = listVessel;
                gvVessel.DataBind();
                Session["listVessel"] = listVessel;
                lblgvVessel.Visible = false;
            }
        }

        protected void btnVesselClear_Click(object sender, EventArgs e)
        {
            txtOVessel.Text = "";
        }

        protected void gvVesselDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int referenceId = Convert.ToInt32(row.Cells[0].Text);
            int transportModeId = Convert.ToInt32((row.Cells[1].FindControl("lblTransportId") as Label).Text);
            string name = row.Cells[3].Text;
            listVessel.Remove(listVessel.Where(x => x.ReferenceId == referenceId && x.Id == transportModeId && x.Name== name).First());
            Session["listVessel"] = listVessel;
            gvVessel.DataSource = listVessel;
            gvVessel.DataBind();
        }

        protected void btnClearingAgent_Click(object sender, EventArgs e)
        {
            divClearingAgent.Visible = true;
            int transportModeId = Convert.ToInt32(ddlTransportMode.SelectedValue);
            int referenceId = 1;
            string name = txtOClearingAgent.Text;
            if (listClearingAgent.Where(x => x.Id == transportModeId && x.Name == name).ToList().Count != 0)
            {
                lblgvClearingAgent.Visible = true;
            }
            else
            {
                if (listClearingAgent.Count > 0)
                {
                    referenceId = listClearingAgent.Max(x => x.ReferenceId) + 1;
                }
                listClearingAgent.Add(new CommonReference { ReferenceId = referenceId, Id = transportModeId, Name = name });
                gvClearingAgent.DataSource = listClearingAgent;
                gvClearingAgent.DataBind();
                Session["listClearingAgent"] = listClearingAgent;
                lblgvClearingAgent.Visible = false;
            }
        }

        protected void btnClearingAgentClear_Click(object sender, EventArgs e)
        {
            txtOClearingAgent.Text = "";
        }

        protected void gvClearingAgentDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int referenceId = Convert.ToInt32(row.Cells[0].Text);
            int transportModeId = Convert.ToInt32((row.Cells[1].FindControl("lblTransportId") as Label).Text);
            string name = row.Cells[3].Text;
            listClearingAgent.Remove(listClearingAgent.Where(x => x.ReferenceId == referenceId && x.Id == transportModeId && x.Name == name).First());
            Session["listClearingAgent"] = listClearingAgent;
            gvClearingAgent.DataSource = listClearingAgent;
            gvClearingAgent.DataBind();
        }

        protected void gvInsuranceDelete_Click(object sender, EventArgs e)
        {
            GridViewRow row = (GridViewRow)((Button)sender).NamingContainer;
            int referenceId = Convert.ToInt32(row.Cells[0].Text);
            int transportModeId = Convert.ToInt32((row.Cells[1].FindControl("lblTransportId") as Label).Text);
            string name = row.Cells[3].Text;
            listInsurancePolicyDetails.Remove(listInsurancePolicyDetails.Where(x => x.ReferenceId == referenceId && x.Id == transportModeId && x.Name == name).First());
            Session["listInsurancePolicyDetails"] = listInsurancePolicyDetails;
            gvInsurance.DataSource = listInsurancePolicyDetails;
            gvInsurance.DataBind();
        }

        protected void btnAddInsuranceDetailClear_Click(object sender, EventArgs e)
        {
            txtInsuranceCompany.Text = "";
            txtPolicyNo.Text = "";
            txtOInsuranceDate.Text = "";
        }

        protected void btnAddInsuranceDetail_Click(object sender, EventArgs e)
        {
            divInsurance.Visible = true;
            int transportModeId = Convert.ToInt32(ddlTransportMode.SelectedValue);
            int referenceId = 1;
            string insuranceName = txtInsuranceCompany.Text;
            string date = txtOInsuranceDate.Text;
            string  policyNo = txtPolicyNo.Text;
            if (listInsurancePolicyDetails.Where(x => x.Id == transportModeId && x.Name == insuranceName && x.ReferenceNo == policyNo).ToList().Count != 0)
            {
                lblgvInsuranceCompanyPolicy.Visible = true;
            }
            else
            {
                if (listInsurancePolicyDetails.Count > 0)
                {
                    referenceId = listInsurancePolicyDetails.Max(x => x.ReferenceId) + 1;
                }
                listInsurancePolicyDetails.Add(new CommonReference { ReferenceId = referenceId, Id = transportModeId, Name = insuranceName ,Date = date, ReferenceNo = policyNo });
                gvInsurance.DataSource = listInsurancePolicyDetails;
                gvInsurance.DataBind();
                Session["listInsurancePolicyDetails"] = listInsurancePolicyDetails;
                lblgvInsuranceCompanyPolicy.Visible = false;
            }
        }

        protected void btnAddOQty_Click(object sender, EventArgs e)
        {
            //supplierBidEmailContact.Add(new SupplierBidEmailContact { UserId = 0, ContactOfficer = contactOffcier, ContactNo = contactNo });
            //gvContactOfficer.DataSource = supplierBidEmailContact;
            //gvContactOfficer.DataBind();
            //divContactInfo.Visible = true;
        }

        protected void btnOQtyClear_Click(object sender, EventArgs e)
        {
            txtOShippingAgent.Text = "";
        }
        protected void gvOQtyDelete_Click(object sender, EventArgs e)
        {

        }
        protected void btnAddContainerSize_Click(object sender, EventArgs e)
        {

        }
        protected void btnContainerSizeClear_Click(object sender, EventArgs e)
        {

        }
        protected void gvContainerSizeDelete_Click(object sender, EventArgs e)
        {

        }
        protected void btnAddHSCode_Click(object sender, EventArgs e)
        {

        }
        protected void btnHSCodeClear_Click(object sender, EventArgs e)
        {

        }
        protected void gvHSCodeDelete_Click(object sender, EventArgs e)
        {

        }
        protected void btnAddPerformanceBond_Click(object sender, EventArgs e)
        {

        }
        protected void btnPerformanceBondClear_Click(object sender, EventArgs e)
        {

        }
        protected void gvPerformanceBondDelete_Click(object sender, EventArgs e)
        {

        }

        
    }
}