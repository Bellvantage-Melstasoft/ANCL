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
    public partial class ApproveExpenseMRN : System.Web.UI.Page
    {
        MrnControllerV2 mrnController = ControllerFactory.CreateMrnControllerV2();
        MRNDetailController mrnDetailController = ControllerFactory.CreateMRNDetailController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        MRexpenseController mrexpenseController = ControllerFactory.CreateMRexpenseController();
        MRNStockDepartmentController mrnStockDepartmentController = ControllerFactory.CreateMRNStockDepartmentController();
        MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                    ((BiddingAdmin)Page.Master).subTabValue = "ApproveExpenseMRN.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "approveExpenseMRNLink";
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }
                if (!IsPostBack)
                {
                    if (Request.QueryString.Get("MrnId") != null || Request.QueryString.Get("MrnId") != "")
                    {

                        int mrnId = Convert.ToInt32(Request.QueryString.Get("MrnId"));
                        ViewState["MRNId"] = mrnId;
                        MrnMasterV2 mrnMaster = mrnController.FetchMrnWithMrnDetails(mrnId, int.Parse(Session["CompanyId"].ToString()));
                        ViewState["MrnMaster"] = new JavaScriptSerializer().Serialize(mrnMaster);
                        ViewState["mrnDetails"] = new JavaScriptSerializer().Serialize(mrnMaster.MrnDetails);
                        LoadDataItemGridView();
                        LoadBudgetInformation();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadDataItemGridView()
        {
            try
            {
                List<MrnDetailsV2> mrnDetails = new JavaScriptSerializer().Deserialize<List<MrnDetailsV2>>(ViewState["mrnDetails"].ToString());
                gvDataTable.DataSource = mrnDetails;
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
                MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
                decimal estimatedCost = Decimal.Round(mrnMaster.MrnDetails.Sum(t => t.EstimatedAmount * t.RequestedQty),2);
                if (mrnMaster.ExpenseType != 1)
                {
                    rdoOperationalExpense.Checked = true;
                    txtEstimatedCost.Text = estimatedCost.ToString();
                    divBudget2.Attributes.Add("style", "display:none");
                }
                else
                {
                    rdoCapitalExpense.Checked = true;
                    txtEstimatedCost.Text = estimatedCost.ToString();
                    divBudget2.Attributes.Add("style", "display:block");
                    if (mrnMaster.ISBudget == 1)
                    {
                        rdoBudgetEnable.Checked = true;
                        rdoBudgetDisable.Checked = false;
                        txtBudgetAmount.Text = mrnMaster.BudgetAmount.ToString();
                        txtBudgetInformation.Text = mrnMaster.BudgetInfo;
                        divBudgetRemark.Attributes.Add("style", "display:none");
                    }
                    else
                    {
                        rdoBudgetEnable.Checked = false;
                        rdoBudgetDisable.Checked = true;
                        divBudgetRemark.Attributes.Add("style", "display:block");
                        txtBudgetRemark.Text = mrnMaster.ExpenseRemarks;
                        divBudgetAmountInfo.Attributes.Add("style", "display:none");
                    }
                }

                mrnMaster.MrnDetails = mrnController.FetchMrnDetailsList(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["MrnMaster"] = new JavaScriptSerializer().Serialize(mrnMaster);



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnDepartmentStockUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                int update = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int MrnId = int.Parse(gvDataTable.Rows[x].Cells[0].Text.ToString());
                int MrnDId = int.Parse(gvDataTable.Rows[x].Cells[1].Text.ToString());
                decimal stock = decimal.Parse((gvDataTable.Rows[x].FindControl("txtAvailableQty") as TextBox).Text);
                
                update = mrnController.UpdateMRNItemDepartmentStock(MrnId, MrnDId, stock);
                LoadDataItemGridView();
                if (update > 0)
                {
                    mrnDetailsStatusLogController.InsertLog(MrnDId, int.Parse(Session["UserId"].ToString()), 11);                    
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

        protected void btnMRNExpenseApprove_Click(object sender, EventArgs e)
        {
            try
            {
                int save = 0;
                MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
                string remark = hdnRemarks.Value.ProcessString();
                save = mrnController.ApproveOrRejectMRNExpense(int.Parse(Request.QueryString.Get("MrnId").ToString()), 1, remark, Convert.ToInt32(Session["UserId"].ToString()), DateTime.Now);
                if (save > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewMrnStockAvailabilityExpenseApprove.aspx'}); });   </script>",
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

        protected void btnMRNExpenseReject_Click(object sender, EventArgs e)
        {
            try
            {
                int save = 0;
                MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
                string remark = hdnRemarks.Value.ProcessString();
                save = mrnController.ApproveOrRejectMRNExpense(mrnMaster.MrnId,2, remark, Convert.ToInt32(Session["UserId"].ToString()), DateTime.Now);
                if (save > 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.UpdatePanel1.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}).then((result) => { window.location = 'ViewMrnStockAvailabilityExpenseApprove.aspx'}); });    </script>",
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

        protected void btnMRNExpenseUpdate_Click(object sender, EventArgs e)
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
                MrnMasterV2 mrnMaster = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());
                update = mrnController.UpdateMRNExpense(mrnMaster.MrnId, expenseType, Is_Budget, budgetAmount, remark, budgetInformation, Convert.ToInt32(Session["UserId"].ToString()), Session["UserNameA"].ToString(), DateTime.Now);

               // var MRN = new JavaScriptSerializer().Deserialize<MrnMasterV2>(ViewState["MrnMaster"].ToString());

                //if (update > 0) {
                //    for (int i = 0; i < MRN.MrnDetails.Count; i++) {
                //        int mrndId = MRN.MrnDetails[i].MrndId;

                //        mrnDetailsStatusLogController.InsertLog(mrndId, int.Parse(Session["UserId"].ToString()), 11);

                //    }

                //}

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