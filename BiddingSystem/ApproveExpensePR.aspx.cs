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
    public partial class ApproveExpensePR : System.Web.UI.Page
    {
        PrControllerV2 prControllerV2 = ControllerFactory.CreatePrControllerV2();
        PR_DetailController prDetailController = ControllerFactory.CreatePR_DetailController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PRexpenseController prExpenseController = ControllerFactory.CreatePRexpenseController();
        PRStockDepartmentController prStockDepartmentController = ControllerFactory.CreatePRStockDepartmentController();
        PRDetailsStatusLogController prDetailsStatusLogController = ControllerFactory.CreatePRDetailsStatusLogController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        PrCapexController prCapexController = ControllerFactory.CreatePrCapexController();


        protected void Page_Load(object sender, EventArgs e) {
            serializer.MaxJsonLength = Int32.MaxValue;
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                    ((BiddingAdmin)Page.Master).subTabValue = "ApproveExpensePR.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "approveExpensePRLink";                    
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
                if (!IsPostBack)
                {
                    if (Request.QueryString.Get("PrId") != null || Request.QueryString.Get("PrId") != "")
                    {
                        int prId = Convert.ToInt32(Request.QueryString.Get("PrId"));
                        ViewState["PrId"] = prId;
                        PrMasterV2 pr = prControllerV2.GetPrMasterToView(prId, int.Parse(Session["CompanyId"].ToString()));
                        PrMasterV2 prMaster = prControllerV2.FetchPrWithPrDetails(prId, int.Parse(Session["CompanyId"].ToString()), pr.WarehouseId);
                        ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(prMaster);
                        ViewState["prDetails"] = new JavaScriptSerializer().Serialize(prMaster.PrDetails);
                        LoadDataItemGridView();
                        LoadBudgetInformation();
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private void LoadDataItemGridView()
        {
            try
            {
                List<PrDetailsV2> prDetails = new JavaScriptSerializer().Deserialize<List<PrDetailsV2>>(ViewState["prDetails"].ToString());
                for (int i = 0; i < prDetails.Count; i++) {
                    //decimal whstock = prDetails[i].WarehouseStock;
                    decimal whstock = prDetails[i].AvailableQty;
                    int fromUnit = prDetails[i].DetailId;
                    int toUnit = prDetails[i].DetailIdWHItem;
                    int companyId = int.Parse(Session["CompanyId"].ToString());
                    int itemId = prDetails[i].ItemId;
                    decimal convertedValue = ControllerFactory.CreateConversionController().DoConversion(itemId, companyId, whstock, fromUnit, toUnit);
                    decimal RconvertedValue = Math.Round(convertedValue, 2);

                    //prDetails[i].WarehouseStock = RconvertedValue;
                    //prDetails[i].AvailableQty = RconvertedValue;
                }
                gvDataTable.DataSource = prDetails;
                gvDataTable.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error While loading gridview', showConfirmButton: true,timer: 4000}); });   </script>", false);
                throw ex;
            }
        }

        private void LoadBudgetInformation()
        {
            try
            {
                PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                decimal estimatedCost = prMaster.PrDetails.Sum(t => t.EstimatedAmount * t.RequestedQty);
                if (prMaster.ExpenseType != 1)
                {
                    rdoOperationalExpense.Checked = true;
                    txtEstimatedCost.Text = estimatedCost.ToString("n2");
                    divBudget2.Attributes.Add("style", "display:none");
                }
                else
                {
                    rdoCapitalExpense.Checked = true;
                    txtEstimatedCost.Text = estimatedCost.ToString("n2");
                    divBudget2.Attributes.Add("style", "display:block");
                    if (prMaster.ISBudget == 1)
                    {
                        rdoBudgetEnable.Checked = true;
                        rdoBudgetDisable.Checked = false;
                        txtBudgetAmount.Text = prMaster.BudgetAmount.ToString();
                        txtBudgetInformation.Text = prMaster.BudgetInfo;
                        divBudgetRemark.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        rdoBudgetEnable.Checked = false;
                        rdoBudgetDisable.Checked = true;
                        divBudgetRemark.Attributes.Add("style", "display:block");
                        txtBudgetRemark.Text = prMaster.ExpenseRemarks;
                        divBudgetAmountInfo.Attributes.Add("style", "display:none");
                    }
                }

                prMaster.PrDetails = prControllerV2.FetchPrDetailsList(int.Parse(Request.QueryString.Get("PrId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["PrMaster"] = new JavaScriptSerializer().Serialize(prMaster);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnWarehouseStockUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int update = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int PrId = int.Parse(gvDataTable.Rows[x].Cells[0].Text.ToString());
                int PrdId = int.Parse(gvDataTable.Rows[x].Cells[1].Text.ToString());
                int ItemId = int.Parse(gvDataTable.Rows[x].Cells[2].Text.ToString());
                int WarehouseId = int.Parse(gvDataTable.Rows[x].Cells[9].Text.ToString());
                int fromUnit = int.Parse(gvDataTable.Rows[x].Cells[11].Text.ToString());
                int tounit = int.Parse(gvDataTable.Rows[x].Cells[10].Text.ToString());
                decimal stock = decimal.Parse((gvDataTable.Rows[x].FindControl("txtAvailableQty") as TextBox).Text);
                PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                decimal up = (hdnAddStockPrice.Value).ToString() == "" ? 0 : decimal.Parse(hdnAddStockPrice.Value);
                DateTime expDate = DateTime.MinValue;
                if (hdnExpdate.Value != "") {
                    expDate = DateTime.Parse(hdnExpdate.Value);
                }
                update = prControllerV2.UpdatePrItemWarehouseStock(PrId, PrdId, stock, ItemId, WarehouseId, int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), fromUnit, tounit, up, expDate);
                LoadDataItemGridView();
                if (update > 0)
                {
                    prDetailsStatusLogController.InsertLog(PrdId, int.Parse(Session["UserId"].ToString()), "STCK_UPDTD");
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });window.location.reload();   </script>",
                        false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'EROR',text:'Error occured during saving ', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnPrExpenseApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int save = 0;
                PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                string remark = hdnRemarks.Value.ProcessString();
                save = prControllerV2.ApproveOrRejectPrExpense(prMaster.PrId, 1, remark, Convert.ToInt32(Session["UserId"].ToString()), DateTime.Now);
                if (save > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrStockAvailabilityExpenseApprove.aspx'}); });   </script>",
                        false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during approving ', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void btnPrExpenseReject_Click(object sender, EventArgs e)
        {
            try
            {
                int save = 0;
                PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                string remark = hdnRemarks.Value.ProcessString();
                save = prControllerV2.ApproveOrRejectPrExpense(prMaster.PrId, 2, remark, Convert.ToInt32(Session["UserId"].ToString()), DateTime.Now);
                if (save > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewPrStockAvailabilityExpenseApprove.aspx'}); });    </script>",
                        false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during approving ', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void btnCapexDocs_Click(object sender, EventArgs e) {
            int prId = int.Parse(Request.QueryString.Get("PrId").ToString());
            PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());

            gvCapexDocs.DataSource = prCapexController.GetPrCapexDocs(prId);
            gvCapexDocs.DataBind();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlCapexDocs').modal('show'); });   </script>", false);
        }
        protected void btnBudgetUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int Is_Budget = 0;
                int update = 0;
                int expenseType = 1;
                string budgetInformation = string.Empty;
                decimal budgetAmount = 0;
                string remark = string.Empty;
                if (rdoOperationalExpense.Checked)
                {
                    expenseType = 2;
                    divBudget2.Style["display"] = "none";
                }
                else
                {
                    divBudget2.Style["display"] = "block";
                    if (rdoBudgetEnable.Checked)
                    {
                        Is_Budget = 1;
                        budgetAmount = Convert.ToDecimal(txtBudgetAmount.Text);
                        budgetInformation = txtBudgetInformation.Text;
                        divBudgetRemark.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        remark = txtBudgetRemark.Text;
                        divBudgetRemark.Attributes.Add("style", "display:block");
                        divBudgetAmountInfo.Attributes.Add("style", "display:none");
                    }
                }
                PrMasterV2 prMaster = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                update = prControllerV2.UpdatePrExpense(prMaster.PrId, expenseType, Is_Budget, budgetAmount, remark, budgetInformation, Convert.ToInt32(Session["UserId"].ToString()), Session["UserNameA"].ToString(), DateTime.Now);
                

                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                    "none",
                    "<script>    $(document).ready(function () {  " +
                    " swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); }); " +
                    "  </script>",
                    false);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during updating ', showConfirmButton: true,timer: 4000}); });   </script>", false);
            }
        }
        
    }

}