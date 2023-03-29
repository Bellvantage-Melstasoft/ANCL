using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.IO;
using System.Text;
using System.Web.Services;
using System.Data;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace BiddingSystem
{
    public partial class CustomerPREdit : System.Web.UI.Page
    {
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pR_DetailController = ControllerFactory.CreatePR_DetailController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        TempBOMController tempBOMController = ControllerFactory.CreateTempBOMController();
        TempPrFileUploadController tempPrFileUploadController = ControllerFactory.CreateTempPrFileUploadController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PRexpenseController prExpenseController = ControllerFactory.CreatePRexpenseController();
        PODetailsController poDetailsController = ControllerFactory.CreatePODetailsController();
        PRDStockInfoLogController prdStockInfoController = ControllerFactory.CreatePRDStockInfoLogController();

        TempPR_FileUploadReplacementController tempPR_FileUploadReplacementController = ControllerFactory.CreateTempPR_FileUploadReplacementController();
        PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PrTypeController prTypeController = ControllerFactory.CreatePrTypeController();
        PR_SupportiveDocumentController pR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        TempPR_SupportiveDocumentController tempPR_SupportiveDocumentController = ControllerFactory.CreateTempPR_SupportiveDocumentController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();

        public enum ReplacementRdo : int { No = 1, Yes }
        static string UserId = string.Empty;
        List<PR_Details> listToBind = new List<PR_Details>();
        List<PR_Details> prDetailsListByPrId = new List<PR_Details>();
        List<TempBOM> TempBOMlistByPrId = new List<TempBOM>();
        List<TempPrFileUpload> tempPrFileUpload = new List<TempPrFileUpload>();
        List<string> ProcdeListWithId = new List<string>();
        public List<string> BomStringList = new List<string>();
        List<PR_Details> GvBindPrDetail = new List<PR_Details>();


        public int editRowIndex = 0;
        public string myText = string.Empty;

        int prid = 0;
        static int departmentId = 0;
        static int itemId = 0;
        int ItemIdFilterd = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewAllPurchaseRequestsEdit.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "editPRLink";

                departmentId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), departmentId, 5, 2) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

          //  List<PR_Master> pr_Master = new List<PR_Master>();
          //  pr_Master = pr_MasterController.FetchDetailsToEdit(departmentId).OrderBy(x => x.PrId).ToList();
            //foreach (var item in pr_Master)
            //{
            //    ProcdeListWithId.Add(item.PrCode);
            //}
            Session.Add("PRCodeLists", ProcdeListWithId);          

            msg.Visible = false;
            if (txtRequiredDate.Text != "")
            {
                txtRequiredDate.Text = Convert.ToDateTime(txtRequiredDate.Text).ToString("yyyy-MM-dd");
            }

            if (Session["WarehouseID"] != null && Session["WarehouseID"].ToString() == "1")
            {
                if (!IsPostBack)
                {
                    bindMeasurement();
                    if (int.Parse(UserId) != 0)
                    {
                        try
                        {
                            LoadDDLMainCatregory();
                            Session["FinalList"] = null;
                            btnAdd.Text = "Add Item";
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                    ddlPrType.DataSource = prTypeController.FetchPRTypesByCompanyId(departmentId);
                    ddlPrType.DataValueField = "PrTypeId";
                    ddlPrType.DataTextField = "PrTypeName";
                    ddlPrType.DataBind();

                    LoadPtTypes();

                    string PrcodeQryString = Request.QueryString.Get("PrCode");

                    if (PrcodeQryString != null || PrcodeQryString != "")
                    {
                        try
                        {
                            txtPRCode.Text = PrcodeQryString;
                            string prcode = txtPRCode.Text;
                            if (prcode == "")
                            {
                                lblPrCode.Text = "Required a PR_CODE";
                                lblPrCode.Attributes.CssStyle.Add("color", "Red");
                            }
                            else
                            {
                                lblPrCode.Text = "";
                                txtBudgetAmount.Text = "";
                                txtBudgetInformation.Text = "";
                                txtRequestedBy.Text = "";
                                txtQuotationFor.Text = "";
                                txtMRNReferenceNo.Text = "";
                                txtRequiredDate.Text = "";
                                List<PR_Master> _PR_MasterList = pr_MasterController.FetchDetailsToEdit(departmentId).Where(x => x.PrCode == prcode).ToList();
                                string itemCatId = "";
                                foreach (var item in _PR_MasterList)
                                {
                                    itemCatId = item.ItemCategoryId.ToString();

                                    txtPrNumber.Text = item.PrCode;
                                    txtQuotationFor.Text = item.QuotationFor;
                                    txtRef.Text = item.OurReference;
                                    int departmentid = item.DepartmentId;
                                    txtDepName.Text = companyDepartmentController.GetDepartmentByDepartmentId(departmentid).DepartmentName;
                                    txtRequestedBy.Text = item.RequestedBy;
                                    string ExpenseType = item.expenseType;
                                    DateTimeRequested.Text = item.DateOfRequest.ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]);
                                    prid = item.PrId;
                                    Session["PRId"] = prid.ToString();
                                    txtRequiredDate.Text = item.RequiredDate.ToString("yyyy-MM-dd");
                                   // txtRequiredDate.Text = item.RequiredDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]);
                                    txtMRNReferenceNo.Text = item.MRNRefNumber;
                                    ddlPtType.SelectedValue = item.PurchaseType;
                                    if (item.PrProcedure == "N")
                                    {
                                        rdoNormalProcedure.Checked = true;
                                    }
                                    if (item.PrProcedure == "C")
                                    {
                                        rdoCoveringProcedure.Checked = true;
                                    }


                                    if (item.PrTypeid == 1)
                                    {
                                        divJobNo.Visible = false;
                                        divVehicleNo.Visible = false;
                                        divMake.Visible = false;
                                        divModel.Visible = false;
                                    }
                                    if (item.PrTypeid == 2)
                                    {
                                        divJobNo.Visible = true;
                                        divVehicleNo.Visible = true;
                                        divMake.Visible = true;
                                        divModel.Visible = true;
                                    }

                                    txtVehicleNo.Text = item.Ref02;
                                    txtMake.Text = item.Ref03;
                                    txtModel.Text = item.Ref04;
                                    ddlPrType.SelectedValue = item.PrTypeid.ToString();

                                    if (item.expenseType != "")
                                    {
                                        if (item.expenseType == "Operational Expense")
                                        {
                                            rdoOperationalExpense.Checked = true;
                                            rdoOperationalExpense_CheckedChanged(null, null);
                                        }
                                        else
                                        {
                                            rdoCapitalExpense.Checked = true;
                                            rdoCapitalExpense_CheckedChanged(null, null);
                                            PRexpense prExpense = prExpenseController.GetPRexpenseById(item.PrId);
                                            if (prExpense.IsBudget == 1)
                                            {
                                                rdoBudgetEnable.Checked = true;
                                                txtBudgetAmount.Text = prExpense.BudgetAmount.ToString();
                                                txtBudgetInformation.Text = prExpense.BudgetInfo;
                                            }else
                                            {
                                                rdoBudgetDisable.Checked = true;
                                                txtBudgetRemark.Text = prExpense.Remarks.ToString();
                                            }
                                        }
                                    }
                                    
                                }

                                ddlMainCateGory.SelectedValue = itemCatId;
                                if (ddlMainCateGory.SelectedValue != "")
                                {
                                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                                    LoadSubCategoryDDL(mainCategoryId);
                                }
                                ddlMainCateGory.SelectedValue = itemCatId;
                                ddlMainCateGory.Enabled = false;

                                LoadPrDetailGv();
                                clearFields();
                                enableFields(true);
                            }
                          

                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }
                }

                else
                {
                }
                lblMessageAddItems.Text = "";
                lblImageDeletedMsg.Text = "";
                lblPrCode.Text = "";
                ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "InitClient();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You have to be Store Keeper for Creating PR', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
            }
        }


        private void bindMeasurement()
        {
            try
            {
                ddlMeasurement.DataSource = unitMeasurementController.fetchMeasurementsByCompanyID(int.Parse(Session["CompanyId"].ToString()));
                ddlMeasurement.DataValueField = "measurentId";
                ddlMeasurement.DataTextField = "measurementShortName";
                ddlMeasurement.DataBind();
                //ddlMeasurement.Items.Insert(0, new ListItem("Select Measurememt", "0"));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void enableFields(bool value)
        {
          //  ddlMainCateGory.Enabled = value;
            ddlSubCategory.Enabled = value;
            ddlItemName.Enabled = value;
            txtQty.Enabled = value;
            txtDescription.Enabled = value;
            txtPurpose.Enabled = value;
            txtEstimatedAmount.Enabled = value;
            rdoEnable.Enabled = value;
            rdoDisable.Enabled = value;
            txtEnable.Enabled = value;
            txtDisable.Enabled = value;
            btnAdd.Enabled = value;
        }


        [WebMethod]
        public static List<string> LoadPRCodes(string input)
        {
            CustomerPREdit cp = new CustomerPREdit();
            input = input.Replace(" ", string.Empty);
            return ((List<string>)cp.Session["PRCodeLists"]).FindAll(item => item.ToLower().Replace(" ", string.Empty).Contains(input.ToLower()));
        }
        
        protected void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                string prcode = txtPRCode.Text;
                int prcodeval = pr_MasterController.GetDetailsByPrCode(departmentId, prcode);
                if (prcode == "")
                {
                    lblPrCode.Text = "Required a PR_CODE";
                    lblPrCode.Attributes.CssStyle.Add("color", "Red");
                }
                if (prcodeval == 0)
                {
                    lblPrCode.Text = "PR_CODE does not exist in the system.";
                    lblPrCode.Attributes.CssStyle.Add("color", "Red");
                }
                else
                {
                    lblPrCode.Text = "";
                    txtBudgetAmount.Text = "";
                    txtBudgetInformation.Text = "";
                    txtRequestedBy.Text = "";
                    txtQuotationFor.Text = "";
                    txtMRNReferenceNo.Text = "";
                    txtRequiredDate.Text = "";
                    List<PR_Master> _PR_MasterList = pr_MasterController.FetchDetailsToEdit(departmentId).Where(x => x.PrCode == prcode).ToList();
                    string itemCatId = "";
                    foreach (var item in _PR_MasterList)
                    {
                        itemCatId = item.ItemCategoryId.ToString();                       

                        txtPrNumber.Text = item.PrCode;
                        txtQuotationFor.Text = item.QuotationFor;
                        txtRef.Text = item.OurReference;
                        int departmentid = item.DepartmentId;
                        txtDepName.Text = companyDepartmentController.GetDepartmentByDepartmentId(departmentid).DepartmentName;
                        txtRequestedBy.Text = item.RequestedBy;
                        //ddlExpenseType.SelectedValue = item.expenseType;
                        string ExpenseType = item.expenseType;
                        DateTimeRequested.Text = item.DateOfRequest.ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]);
                        prid = item.PrId;
                        Session["PRId"] = prid.ToString();
                        txtRequiredDate.Text = item.RequiredDate.ToString("MM/dd/yyyy");
                        txtMRNReferenceNo.Text = item.MRNRefNumber;
                        ddlPtType.SelectedValue = item.PurchaseType;
                        if (item.PrProcedure == "N")
                        {
                            rdoNormalProcedure.Checked = true;
                        }
                        if (item.PrProcedure == "C")
                        {
                            rdoCoveringProcedure.Checked = true;
                        }


                        if (item.PrTypeid == 1)
                        {
                            divJobNo.Visible = false;
                            divVehicleNo.Visible = false;
                            divMake.Visible = false;
                            divModel.Visible = false;
                        }
                        if (item.PrTypeid == 2)
                        {
                            divJobNo.Visible = true;
                            divVehicleNo.Visible = true;
                            divMake.Visible = true;
                            divModel.Visible = true;
                        }

                        txtVehicleNo.Text = item.Ref02;
                        txtMake.Text = item.Ref03;
                        txtModel.Text = item.Ref04;
                        ddlPrType.SelectedValue = item.PrTypeid.ToString();

                        if (item.expenseType != "")
                        {
                            if (item.expenseType == "Operational Expense")
                            {
                                rdoOperationalExpense.Checked = true;
                                rdoOperationalExpense_CheckedChanged(null, null);
                            }
                            else
                            {
                                rdoCapitalExpense.Checked = true;
                                rdoCapitalExpense_CheckedChanged(null, null);
                                PRexpense prExpense = prExpenseController.GetPRexpenseById(item.PrId);
                                if (prExpense.IsBudget == 1)
                                {
                                    rdoBudgetEnable.Checked = true;
                                    txtBudgetAmount.Text = prExpense.BudgetAmount.ToString();
                                    txtBudgetInformation.Text = prExpense.BudgetInfo;
                                }
                                else
                                {
                                    rdoBudgetDisable.Checked = true;
                                    txtBudgetRemark.Text = prExpense.Remarks.ToString();
                                }
                            }
                        }
                    }

                    ddlMainCateGory.SelectedValue = itemCatId;
                    if (ddlMainCateGory.SelectedValue != "")
                    {
                        int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                        LoadSubCategoryDDL(mainCategoryId);
                    }
                    ddlMainCateGory.SelectedValue = itemCatId;
                    ddlMainCateGory.Enabled = false;

                    LoadPrDetailGv();
                    clearFields();
                    enableFields(true);

                    if (txtRequiredDate.Text != "")
                    {
                        txtRequiredDate.Text = Convert.ToDateTime(txtRequiredDate.Text).ToString("yyyy-MM-dd");
                    }
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { document.body.scrollTop = 750; document.documentElement.scrollTop = 750;});   </script>", false);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnEditItems_Click(object sender, EventArgs e)
        {
            try
            {
                BomStringList = new List<string>();
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvDataTable.Rows[x].Cells[1].Text);
                itemId = int.Parse(gvDataTable.Rows[x].Cells[7].Text);
                string itemName = (gvDataTable.Rows[x].Cells[8].Text);
                btnAdd.Text = "Update Item";

                List<PR_Details> GetPrDetailsByPrID = new List<PR_Details>();
                PR_Details pR_Details = new PR_Details();
                GetPrDetailsByPrID = pR_DetailController.FetchPR_DetailsByPrIdList(prid);
                if (GetPrDetailsByPrID.Count() > 0)
                {
                    pR_Details = GetPrDetailsByPrID.Find(a => (a.PrId == prid) && (a.ItemId == itemId));

                    LoadDDLMainCatregory();
                    ddlMainCateGory.SelectedValue = pR_Details.MainCategoryId.ToString();

                    ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(pR_Details.MainCategoryId, int.Parse(Session["CompanyId"].ToString()));
                    ddlSubCategory.DataValueField = "SubCategoryId";
                    ddlSubCategory.DataTextField = "SubCategoryName";
                    ddlSubCategory.DataBind();
                    ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));


                    // ddlItemName.Text = itemName;
                    ddlItemName.DataSource = addItemController.FetchItemsByCategories(pR_Details.MainCategoryId, pR_Details.SubCategoryId, int.Parse(Session["CompanyId"].ToString())).OrderBy(y => y.ItemId).ToList();
                    ddlItemName.DataTextField = "ItemName";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));


                    ddlItemName.Enabled = false;
                    ddlSubCategory.Enabled = false;
                    ddlMainCateGory.Enabled = false;


                    ddlSubCategory.SelectedValue = pR_Details.SubCategoryId.ToString();
                    // ddlItemName.Text = pR_Details.ItemName;
                    ddlItemName.SelectedValue = pR_Details.ItemId.ToString();
                    txtQty.Text = pR_Details.ItemQuantity.ToString();
                    txtDescription.Text = pR_Details.ItemDescription;
                    txtEstimatedAmount.Text = pR_Details.EstimatedAmount.ToString();
                    if (pR_Details.Replacement == 1)
                    {
                        rdoEnable.Enabled = true;
                        rdoEnable.Checked = true;
                        rdoDisable.Checked = false;
                    }
                    else
                    {
                        rdoDisable.Enabled = true;
                        rdoDisable.Checked = true;
                        rdoEnable.Checked = false;
                    }
                    // txtBiddingPeriod.Text = "2";                        // have to ask
                    txtPurpose.Text = pR_Details.Purpose;
                    ddlMeasurement.SelectedValue = pR_Details.MeasurmentId.ToString();
                    if (pR_Details.SampleProvided == 1)
                    {
                        rdoFileSampleEnable.Checked = true;
                    }
                    else
                    {
                        rdoFileSampleDisable.Checked = true;
                    }

                    TempBOMlistByPrId = tempBOMController.GetBOMListByPrIdItemId(departmentId, prid, itemId);
                    tempPrFileUpload = tempPrFileUploadController.GetTempPrFiles(itemId, prid);

                    List<PR_BillOfMeterial> _PR_BillOfMeterial = pr_BillOfMeterialController.GetList(prid, itemId);
                    hdnItemCount.Value = _PR_BillOfMeterial.Count.ToString();
                    if (_PR_BillOfMeterial.Count != 0)
                    {
                        foreach (var item in _PR_BillOfMeterial)
                        {
                            BomStringList.Add(item.Meterial + "-" + item.Description);
                        }
                    }

                    if (TempBOMlistByPrId.Count != 0)
                    {
                        foreach (var item in TempBOMlistByPrId)
                        {
                            BomStringList.Add(item.Meterial + "-" + item.Description);
                        }
                    }
                    gvRepacementImages.DataSource = pR_Replace_FileUploadController.FtechUploadeFiles(prid, itemId);
                    gvRepacementImages.DataBind();
                    gvPrUploadedFiles.DataSource = pr_FileUploadController.FtechUploadeFiles(prid, itemId);
                    gvPrUploadedFiles.DataBind();
                    gvSupporiveFiles.DataSource = pR_SupportiveDocumentController.FtechUploadeSupporiveFiles(prid, itemId);
                    gvSupporiveFiles.DataBind();

                    //MaintainScrollPositionOnPostBack = true;
                    enableFields(true);


                    PRDStockInfo PRDStockInfo = prdStockInfoController.GetProStockInfoLogById(pR_Details.PrdId); // But only comes one item
                    //if (listPRDStockInfo
                    //{
                    //    pRDStockInfo = listPRDStockInfo[0];
                    //}
                    if(PRDStockInfo.ProId != 0){
                        txtStockBalance.Text = PRDStockInfo.StockBalance.ToString();
                        txtAvgConsumption.Text = PRDStockInfo.AvgConsumption.ToString();
                    }

                    }

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  document.body.scrollTop = 50; document.documentElement.scrollTop = 50;});   </script>", false);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none",
                                                        "<script>"+
                                                        " $(document).ready(function () {  "+
                                                        " document.body.scrollTop = 50;"+
                                                        " document.documentElement.scrollTop = 50;" +
                                                        " controlValidate();" +
                                                         "}); "+
                                                        "</script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getJsonBomStringList()
        {
            var DataList = BomStringList;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }



        public string getBOMData()
        {
            string data = "";
            List<PR_BillOfMeterial> _PR_BillOfMeterial = pr_BillOfMeterialController.GetList(prid, itemId);
            foreach (var item in _PR_BillOfMeterial)
            {
                string Meterial = item.Meterial;
                string Description = item.Description;
                data += "<tr><td>" + Meterial + "</td><td>" + Description + "</td></tr>";

            }
            return data;
        }


        [WebMethod]
        public static DetailsClass[] GetPRBomDetailsIds(string data)
        {
            List<DetailsClass> Detail = new List<DetailsClass>();

            string jsonData = data;
            string PrId = string.Empty;
            string ItemId = string.Empty;

            if (jsonData != "")
            {
                string[] value = jsonData.Split('-');
                string val1 = value[0];
                string val2 = value[1];
                PrId = val1;
                ItemId = val2;
                PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
                List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetList(int.Parse(PrId), int.Parse(ItemId));

                foreach (var item in pr_BillOfMeterial)
                {
                    DetailsClass DataObj = new DetailsClass();
                    DataObj.Meterial = item.Meterial;
                    DataObj.Description = item.Description;
                    Detail.Add(DataObj);
                }
            }

            return Detail.ToArray();
        }

        public class DetailsClass //Class for binding data
        {
            public string Meterial { get; set; }
            public string Description { get; set; }
        }


        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                itemId = int.Parse(gvDataTable.Rows[x].Cells[7].Text);

                pr_BillOfMeterialController.DeletePRBom(int.Parse(Session["PRId"].ToString()), itemId);
                pr_FileUploadController.DeleteFileUpload(int.Parse(Session["PRId"].ToString()), itemId);
                int prDetaDelete = pR_DetailController.DeletePrDetailByPrIDAndItemId(int.Parse(Session["PRId"].ToString()), itemId);
                if (prDetaDelete > 0)
                {
                    LoadPrDetailGv();
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //DisplayMessage("PR Detail has been Delete Successfully", false);
                }
                else
                {
                    DisplayMessage("Error On Delete PR", true);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------PR Details 
        private void LoadPrDetailGv()
        {
            try
            {
                List<PR_Details> _PR_DetailsLists = pR_DetailController.FetchDetailsRejectedPR(int.Parse(Session["PRId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                GvBindPrDetail = _PR_DetailsLists.ToList();
                gvDataTable.DataSource = _PR_DetailsLists;
                gvDataTable.DataBind();
                txtEstimatedCost.Text = _PR_DetailsLists.Sum(t => t.EstimatedAmount * t.ItemQuantity).ToString();
                txtEstimatedCost.Enabled = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------Load Main Category DDL
        private void LoadDDLMainCatregory()
        {
            try
            {
                ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString()));
                ddlMainCateGory.DataValueField = "CategoryId";
                ddlMainCateGory.DataTextField = "CategoryName";
                ddlMainCateGory.DataBind();
                ddlMainCateGory.Items.Insert(0, new ListItem("Select Main Category", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(ddlMainCateGory.SelectedValue) != 0 || ddlMainCateGory.SelectedValue != "")
                {
                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                  //  ddlItemName.Text = "";
                    LoadSubCategoryDDL(mainCategoryId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadSubCategoryDDL(int SubCatId)
        {
            try
            {
                ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(SubCatId, int.Parse(Session["CompanyId"].ToString()));
                ddlSubCategory.DataTextField = "SubCategoryName";
                ddlSubCategory.DataValueField = "SubCategoryId";
                ddlSubCategory.DataBind();
                ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Load Items DDL
        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(ddlMainCateGory.SelectedValue) != 0 || ddlMainCateGory.SelectedValue != "")
                {
                    //Session["ItemNameLists"])
                    Session["MainCategoryIdEdit"] = ddlMainCateGory.SelectedValue;
                    Session["SubCategoryIdEdit"] = ddlSubCategory.SelectedValue;
                    int categoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    int subCategoryId = int.Parse(ddlSubCategory.SelectedValue);

                    ddlItemName.DataSource = addItemController.FetchItemsByCategories(categoryId, subCategoryId, int.Parse(Session["CompanyId"].ToString())).OrderBy(y => y.ItemId).ToList();
                    ddlItemName.DataTextField = "ItemName";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));
                }
                else
                {
                    ddlItemName.Items.Clear();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnUpdateData_Click(object sender, EventArgs e)
        {
            try
            {
                if (Session["PRId"] != null)
                {
                    PR_Master _prmaster = pr_MasterController.FetchRejectPR(int.Parse(Session["PRId"].ToString()));
                    string ExpenseType = "Capital Expense";
                    if (rdoOperationalExpense.Checked == true)
                    {
                        ExpenseType = "Operational Expense";
                    }

                    string procedure = "C";
                    if (rdoNormalProcedure.Checked)
                    {
                        procedure = "N";
                    }
                    int UpdatePR = pr_MasterController.UpdatePRMaster(int.Parse(Session["PRId"].ToString()), _prmaster.DepartmentId, DateTime.Parse(DateTimeRequested.Text), txtQuotationFor.Text, txtRef.Text, txtRequestedBy.Text, LocalTime.Now, UserId, 1, 0, "", LocalTime.Now, 0, "", LocalTime.Now, int.Parse(ddlPrType.SelectedValue), ExpenseType, ddlPrType.SelectedValue == "1" ? "" : txtRef.Text, ddlPrType.SelectedValue == "1" ? "" : txtVehicleNo.Text, ddlPrType.SelectedValue == "1" ? "" : txtMake.Text, ddlPrType.SelectedValue == "1" ? "" : txtModel.Text, "", "", procedure, ddlPtType.SelectedValue, Convert.ToDateTime(txtRequiredDate.Text), txtMRNReferenceNo.Text);
                    PRexpense prExpense = prExpenseController.GetPRexpenseById(int.Parse(Session["PRId"].ToString()));
                    int Is_Budget = 0;
                    if (rdoBudgetEnable.Checked)
                    {
                        Is_Budget = 1;
                    }
                    int isApproved = 0;
                    if (rdoOperationalExpense.Checked == true)
                    {
                        txtBudgetAmount.Text = "0";
                        txtBudgetInformation.Text = "";
                        txtBudgetRemark.Text = "";
                        isApproved = 1;
                    }
                    else
                    {
                        if (rdoBudgetDisable.Checked == true)
                        {
                            txtBudgetAmount.Text = "0";
                            txtBudgetInformation.Text = "";
                        }                        
                    }
                    if(txtBudgetAmount.Text == "")
                    {
                        txtBudgetAmount.Text = "0";
                    }
                    prExpenseController.UpdatePRExpense(int.Parse(Session["PRId"].ToString()), Is_Budget, Convert.ToDecimal(txtBudgetAmount.Text), txtBudgetRemark.Text, txtBudgetInformation.Text , isApproved);
                    if (UpdatePR > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                        //DisplayMessage("PR has been Updated Successfuly", false);
                        Session["PRId"] = null;
                        clearFields();
                        clearPrDetails();
                        txtMRNReferenceNo.Text = "";
                        txtRequiredDate.Text = "";
                        txtQuotationFor.Text = "";
                        txtEstimatedAmount.Text = "";
                        txtBudgetAmount.Text = "";
                        txtBudgetInformation.Text = "";
                        txtBudgetRemark.Text = "";
                        txtEstimatedCost.Text = "";
                    }
                    else
                    {
                        DisplayMessage("Error on Update PR", true);
                    }
                }
                else
                {
                    DisplayMessage("Please Select Purchase Requistion", true);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void clearPrDetails()
        {
            txtDepName.Text = "";
            txtPRCode.Text = "";
            txtPrNumber.Text = "";
            txtPurpose.Text = "";
            txtRef.Text = "";
            txtEstimatedAmount.Text = "";
            txtRequestedBy.Text = "";
            DateTimeRequested.Text = "";
            gvDataTable.DataSource = null;
            gvDataTable.DataBind();

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["PRId"] != null)
                {
                    int replaceMentId = 0;
                    if (txtQty.Text != "" && ddlItemName.SelectedValue != "" && ddlMainCateGory.SelectedValue != "" && ddlSubCategory.SelectedValue != "")
                    {
                        if (btnAdd.Text == "Add Item")
                        {
                            if (rdoEnable.Checked)
                            {
                                replaceMentId = 1;
                            }
                            if (rdoDisable.Checked)
                            {
                                replaceMentId = 0;
                            }
                            if (ddlItemName.SelectedItem.Text != "")
                            {
                                // ItemIdFilterd = addItemController.GetIdByItemName(departmentId, ddlItemName.Text.Trim());
                                ItemIdFilterd = int.Parse(ddlItemName.SelectedValue);
                            }
                            int fileSampleProvided = 1;
                            if (rdoFileSampleDisable.Checked)
                            {
                                fileSampleProvided = 0;
                            }

                            // TODO
                            int savePR_Detail = pR_DetailController.SavePRDetails(int.Parse(Session["PRId"].ToString()), ItemIdFilterd, 1, txtDescription.Text, UserId, LocalTime.Now, 1, replaceMentId, decimal.Parse(txtQty.Text), txtPurpose.Text, decimal.Parse(txtEstimatedAmount.Text), fileSampleProvided, txtPurpose.Text, int.Parse(ddlMeasurement.SelectedValue));
                            
                            // int savePR_Detail = 0;
                            if (savePR_Detail > 0)
                            {
                                string[] arr = hdnField.Value.Split(',');
                                var list = arr.ToList();

                                list.RemoveAll(o => string.IsNullOrWhiteSpace(o));
                                List<ArrayItems> arraList = new List<ArrayItems>();
                                if (list != null || arr[1] != "")
                                {
                                    for (int i = 0; i < list.Count; i += 2)
                                    {
                                        ArrayItems arrayItems = new ArrayItems();
                                        arrayItems.Description = list[i];
                                        arrayItems.Metirial = list[i + 1];
                                        arraList.Add(arrayItems);
                                    }
                                }
                                pr_BillOfMeterialController.DeletePRBom(int.Parse(Session["PRId"].ToString()), ItemIdFilterd);
                                for (int i = 0; i < arraList.Count(); i++)
                                {
                                    pr_BillOfMeterialController.SaveBillOfMeterial(int.Parse(Session["PRId"].ToString()), ItemIdFilterd, i + 1, arraList[i].Metirial, arraList[i].Description, 1, LocalTime.Now, UserId, LocalTime.Now, UserId);
                                }

                                // Update File Upload Items 

                                string folderFilePath = string.Empty;


                                HttpFileCollection uploads = HttpContext.Current.Request.Files;
                                for (int i = 0; i < uploads.Count; i++)
                                {
                                    if (uploads.AllKeys[i] == "files[]")
                                    {
                                        List<PR_FileUpload> GetFilesByPrItemId = pr_FileUploadController.FtechUploadeFiles(int.Parse(Session["PRId"].ToString()), ItemIdFilterd);
                                        int maxNumber = 0;

                                        foreach (var item in GetFilesByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploads[i];

                                        string CreateFileName = Session["PRId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + Session["PRId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PurchaseRequestFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PurchaseRequestFiles/" + Session["PRId"].ToString() + "/" + filename01;
                                            int saveFilePath = pr_FileUploadController.SaveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["PRId"].ToString()), folderFilePath, FileName);
                                        }

                                    }
                                    if (uploads.AllKeys[i] == "fileReplace[]")
                                    {

                                        List<PR_Replace_FileUpload> GetReplacementFilesByPrItemId = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(Session["PRId"].ToString()), ItemIdFilterd);

                                        int maxNumber = 0;

                                        foreach (var item in GetReplacementFilesByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploads[i];

                                        string CreateFileName = Session["PRId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + Session["PRId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrReplacementFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PrReplacementFiles/" + Session["PRId"].ToString() + "/" + filename01;
                                            int saveFilePath = pR_Replace_FileUploadController.SaveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["PRId"].ToString()), folderFilePath, FileName);
                                        }
                                    }

                                    if (uploads.AllKeys[i] == "supportivefiles[]")
                                    {

                                        List<PR_SupportiveDocument> GetPR_SupportiveDocumentsByPrItemId = pR_SupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(Session["PRId"].ToString()), ItemIdFilterd);

                                        int maxNumber = 0;

                                        foreach (var item in GetPR_SupportiveDocumentsByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploads[i];

                                        string CreateFileName = Session["PRId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + Session["PRId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrSupportiveFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PrSupportiveFiles/" + Session["PRId"].ToString() + "/" + filename01;
                                            int saveFilePath = pR_SupportiveDocumentController.SaveSupporiveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["PRId"].ToString()), folderFilePath, FileName);
                                        }
                                    }





                                }
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                                //DisplayMessage("This PR Detail has been created Successfully", false);
                                LoadPrDetailGv();
                                clearFields();


                            }
                            else
                            {
                                DisplayMessage("This PR Detail already Exist", true);
                            }
                        }
                    }
                    else
                    {
                        lblMessageAddItems.Text = "These fields cannot be empty.";
                    }


                    if (btnAdd.Text == "Update Item")
                    {
                        if (txtQty.Text != "" && ddlItemName.SelectedValue != "" && ddlMainCateGory.SelectedValue != "" && ddlSubCategory.SelectedValue != "")
                        {
                            if (rdoEnable.Checked)
                            {
                                replaceMentId = 1;
                            }
                            if (rdoDisable.Checked)
                            {
                                replaceMentId = 0;
                            }
                            if (ddlItemName.SelectedValue != "")
                            {
                                // ItemIdFilterd = addItemController.GetIdByItemName(departmentId, ddlItemName.Text.Trim());
                                ItemIdFilterd = int.Parse(ddlItemName.SelectedValue);
                            }
                            int fileSampleProvided = 1;
                            if (rdoFileSampleDisable.Checked)
                            {
                                fileSampleProvided = 0;
                            }
                            int updatePR_Detail = pR_DetailController.UpdatePRDetails(int.Parse(Session["PRId"].ToString()), itemId, ItemIdFilterd, 1, txtDescription.Text, UserId, LocalTime.Now, 1, replaceMentId, decimal.Parse(txtQty.Text), txtPurpose.Text, decimal.Parse(txtEstimatedAmount.Text), fileSampleProvided, txtPurpose.Text, int.Parse(ddlMeasurement.SelectedValue));
                            prdStockInfoController.upadtePRDStockInfoLog(int.Parse(Session["PRId"].ToString()), itemId, Convert.ToDecimal(txtStockBalance.Text), Convert.ToDecimal(txtAvgConsumption.Text) , UserId , LocalTime.Now);
                            if (updatePR_Detail > 0)
                            {
                                string[] arr = hdnField.Value.Split(',');
                                var list = arr.ToList();

                                list.RemoveAll(o => string.IsNullOrWhiteSpace(o));
                                List<ArrayItems> arraList = new List<ArrayItems>();
                                if (list != null || arr[1] != "")
                                {
                                    for (int i = 0; i < list.Count; i += 2)
                                    {
                                        ArrayItems arrayItems = new ArrayItems();
                                        arrayItems.Description = list[i];
                                        arrayItems.Metirial = list[i + 1];
                                        arraList.Add(arrayItems);
                                    }
                                }
                                pr_BillOfMeterialController.DeletePrBoMTrash(int.Parse(Session["PRId"].ToString()), itemId);
                                for (int i = 0; i < arraList.Count(); i++)
                                {
                                    pr_BillOfMeterialController.SaveBillOfMeterial(int.Parse(Session["PRId"].ToString()), ItemIdFilterd, i + 1, arraList[i].Metirial, arraList[i].Description, 1, LocalTime.Now, UserId, LocalTime.Now, UserId);
                                }

                                // Update File Upload Items 

                                string folderFilePath = string.Empty;


                                HttpFileCollection uploadsT = HttpContext.Current.Request.Files;
                                for (int i = 0; i < uploadsT.Count; i++)
                                {
                                    if (uploadsT.AllKeys[i] == "files[]")
                                    {
                                        List<PR_FileUpload> GetFilesByPrItemId = pr_FileUploadController.FtechUploadeFiles(int.Parse(Session["PRId"].ToString()), ItemIdFilterd);
                                        int maxNumber = 0;

                                        foreach (var item in GetFilesByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploadsT[i];

                                        string CreateFileName = Session["PRId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + Session["PRId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PurchaseRequestFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PurchaseRequestFiles/" + Session["PRId"].ToString() + "/" + filename01;
                                            int saveFilePath = pr_FileUploadController.SaveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["PRId"].ToString()), folderFilePath, FileName);
                                        }

                                    }
                                    if (uploadsT.AllKeys[i] == "fileReplace[]")
                                    {

                                        List<PR_Replace_FileUpload> GetReplacementFilesByPrItemId = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(Session["PRId"].ToString()), ItemIdFilterd);

                                        int maxNumber = 0;

                                        foreach (var item in GetReplacementFilesByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploadsT[i];

                                        string CreateFileName = Session["PRId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + Session["PRId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrReplacementFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PrReplacementFiles/" + Session["PRId"].ToString() + "/" + filename01;
                                            int saveFilePath = pR_Replace_FileUploadController.SaveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["PRId"].ToString()), folderFilePath, FileName);
                                        }
                                    }


                                    if (uploadsT.AllKeys[i] == "supportivefiles[]")
                                    {

                                        List<PR_SupportiveDocument> GetSupportiveDocumentByPrItemId = pR_SupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(Session["PRId"].ToString()), ItemIdFilterd);

                                        int maxNumber = 0;

                                        foreach (var item in GetSupportiveDocumentByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploadsT[i];

                                        string CreateFileName = Session["PRId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + Session["PRId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrSupportiveFiles/" + Session["PRId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PrSupportiveFiles/" + Session["PRId"].ToString() + "/" + filename01;
                                            int saveSupportiveFilePath = pR_SupportiveDocumentController.SaveSupporiveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["PRId"].ToString()), folderFilePath, FileName);
                                        }
                                    }


                                }

                                DisplayMessage("This PR Detail has been Updated Successfully", false);
                                LoadPrDetailGv();
                                clearFields();
                            }
                            else
                            {
                                DisplayMessage("This PR Detail already Exist", true);

                            }
                        }
                        else
                        {
                            lblMessageAddItems.Text = "These fields cannot be empty.";
                        }

                    }

                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static List<string> LoadItemNames(string input)
        {
            CompanyPurchaseRequestNote cp = new CompanyPurchaseRequestNote();
            int mainCategory = int.Parse(cp.Session["MainCategoryIdEdit"].ToString());
            int subCategory = int.Parse(cp.Session["SubCategoryIdEdit"].ToString());
            AddItemController addItemController = ControllerFactory.CreateAddItemController();
            List<AddItem> additems = addItemController.SearchedItemName(mainCategory, subCategory, departmentId, input);

            List<string> itemNameclz = new List<string>();

            foreach (var item in additems)
            {
                itemNameclz.Add(item.ItemName);
            }

            input = input.Replace(" ", string.Empty);

            return (itemNameclz.FindAll(item => item.ToLower().Replace(" ", string.Empty).Contains(input.ToLower())));
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                clearFields();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class ArrayItems
        {
            public string Metirial { get; set; }
            public string Description { get; set; }
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

        private void clearFields()
        {
            try
            {
                btnAdd.Text = "Add Item";
                txtPurpose.Text = "";
                txtDescription.Text = "";
                txtQty.Text = "";
                txtEstimatedAmount.Text = "";
               // LoadDDLMainCatregory();
                gvPrUploadedFiles.DataSource = null;
                gvPrUploadedFiles.DataBind();

                gvRepacementImages.DataSource = null;
                gvRepacementImages.DataBind();

                gvSupporiveFiles.DataSource = null;
                gvSupporiveFiles.DataBind();

              //  ddlMainCateGory.SelectedIndex = 0;
                ddlSubCategory.Items.Clear();
                ddlSubCategory.DataBind();

              //  ddlItemName.Text = "";
               // ddlItemName.DataBind();

                ddlMainCateGory.Enabled = false;
                ddlSubCategory.Enabled = true;
                if (ddlSubCategory.SelectedIndex != -1)
                {
                    ddlSubCategory.SelectedIndex = 0;
                }
                ddlItemName.Enabled = true;
                if (ddlItemName.SelectedIndex != -1)
                {
                    ddlItemName.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnDeleteUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string prid = gvPrUploadedFiles.Rows[x].Cells[0].Text;
                string itemid = gvPrUploadedFiles.Rows[x].Cells[1].Text;
                string imagepath = gvPrUploadedFiles.Rows[x].Cells[2].Text;
                int deleteImages = pr_FileUploadController.DeleteParticularFile(int.Parse(prid), int.Parse(itemid), imagepath);

                if (deleteImages > 0)
                {
                    lblImageDeletedMsg.Text = "Image deleted successfully";
                    lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Green");
                    gvPrUploadedFiles.DataSource = pr_FileUploadController.FtechUploadeFiles(int.Parse(prid), int.Parse(itemid));
                    gvPrUploadedFiles.DataBind();

                    var SourcePath = Server.MapPath("PurchaseRequestFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("PurchaseRequestFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    lblImageDeletedMsg.Text = "Action unsuccessfull";
                    lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Red");
                    gvPrUploadedFiles.DataSource = pr_FileUploadController.FtechUploadeFiles(int.Parse(prid), int.Parse(itemid));
                    gvPrUploadedFiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnViewReplacementImage_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvRepacementImages.Rows[x].Cells[2].Text;
                // System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>$(document).ready(function(){  window.open('" + filepath + "');   });</script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnDeleteReplacementImage_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string prid = gvRepacementImages.Rows[x].Cells[0].Text;
                string itemid = gvRepacementImages.Rows[x].Cells[1].Text;
                string imagepath = gvRepacementImages.Rows[x].Cells[2].Text;
                int deleteImages = pR_Replace_FileUploadController.DeleteParticularReplaceFile(int.Parse(prid), int.Parse(itemid), imagepath);

                if (deleteImages > 0)
                {
                    lblReplaceimageDelete.Text = "Image deleted successfully";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Green");
                    gvRepacementImages.DataSource = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(prid), int.Parse(itemid));
                    gvRepacementImages.DataBind();

                    var SourcePath = Server.MapPath("PrReplacementFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("PrReplacementFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    lblReplaceimageDelete.Text = "Action unsuccessfull";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Red");
                    gvRepacementImages.DataSource = pR_Replace_FileUploadController.FtechUploadeFiles(int.Parse(prid), int.Parse(itemid));
                    gvRepacementImages.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnViewUploadImage_Click(object sender, EventArgs e)
        {

            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvPrUploadedFiles.Rows[x].Cells[2].Text;
                //System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>$(document).ready(function(){  window.open('" + filepath + "');   });</script>", false);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlPrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlPrType.SelectedValue == "1")
                {
                    divJobNo.Visible = false;
                    divVehicleNo.Visible = false;
                    divMake.Visible = false;
                    divModel.Visible = false;
                }
                else if (ddlPrType.SelectedValue == "2")
                {
                    divJobNo.Visible = true;
                    divVehicleNo.Visible = true;
                    divMake.Visible = true;
                    divModel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnViewUploadSupporiveDocument_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvSupporiveFiles.Rows[x].Cells[2].Text;
                // System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>$(document).ready(function(){  window.open('" + filepath + "');   });</script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnDeleteSupportiveDocument_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string prid = gvSupporiveFiles.Rows[x].Cells[0].Text;
                string itemid = gvSupporiveFiles.Rows[x].Cells[1].Text;
                string imagepath = gvSupporiveFiles.Rows[x].Cells[2].Text;
                int deleteSupporiveDocument = pR_SupportiveDocumentController.DeleteParticularSupporiveFile(int.Parse(prid), int.Parse(itemid), imagepath);

                if (deleteSupporiveDocument > 0)
                {
                    lblSupporiveDelete.Text = "Document has been deleted successfully";
                    lblSupporiveDelete.Attributes.CssStyle.Add("color", "Green");
                    gvSupporiveFiles.DataSource = pR_SupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(prid), int.Parse(itemid));
                    gvSupporiveFiles.DataBind();

                    var SourcePath = Server.MapPath("PrSupportiveFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("PrSupportiveFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    lblSupporiveDelete.Text = "Action unsuccessfull";
                    lblSupporiveDelete.Attributes.CssStyle.Add("color", "Red");
                    gvSupporiveFiles.DataSource = pR_SupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(prid), int.Parse(itemid));
                    gvSupporiveFiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnexisting_Click(object sender, EventArgs e)
        {

            if (ddlItemName.SelectedValue != "" && ddlItemName.SelectedItem != null)
            {
                CompanyPurchaseRequestNote cp = new CompanyPurchaseRequestNote();
                int mainCategory;
                int subCategory;
                if (cp.Session["MainCategoryIdEdit"] != null && cp.Session["SubCategoryIdEdit"] != null)
                {
                    mainCategory = int.Parse(cp.Session["MainCategoryIdEdit"].ToString());
                    subCategory = int.Parse(cp.Session["SubCategoryIdEdit"].ToString());
                }else {
                    mainCategory = int.Parse(ddlMainCateGory.SelectedValue);
                    subCategory = int.Parse(ddlSubCategory.SelectedValue);
                    AddItemBOMController addItemBOMController = ControllerFactory.CreateAddItemBOMController();
                    List<AddItemBOM> additemspecification = addItemBOMController.GetbyItemName(mainCategory, subCategory, departmentId, ddlItemName.SelectedItem.Text);
                    gvSpecificationBoms.DataSource = additemspecification;
                    gvSpecificationBoms.DataBind();
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#specification').modal('show');   </script>", false);
                }
            }
            else
            {
                DisplayMessage("Please fill Item name", true);
            }

        }

        protected void ddlPtType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0)
                {
                    int itemId = addItemController.GetIdByItemName(int.Parse(Session["CompanyId"].ToString()), ddlItemName.SelectedItem.Text);
                    POHistory purchaseOrderHistory = poDetailsController.GetPoHistoryByItemId(itemId);
                    txtLastPurchaseSupplier.Text = purchaseOrderHistory.SupplierName;
                    hndSupplierId.Value = purchaseOrderHistory.SupplierId.ToString();
                    txtPurchaseOrderNo.Text = purchaseOrderHistory.PurchaseOrderId.ToString();
                    txtLastPurchasePrice.Text = purchaseOrderHistory.ItemPrice.ToString();
                    txtLastPurchaseDate.Text = purchaseOrderHistory.PurchaseDate.ToString();
                    // PRDStockInfo prdStockInfo = prdStockInfoController.GetProStockInfoByItemId(itemId);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        private void LoadPtTypes()
        {
            try
            {
                ddlPtType.Items.Add(new ListItem("Local", "L"));
                ddlPtType.Items.Add(new ListItem("Import", "I"));
                ddlPtType.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnNewItem_Click(object sender, EventArgs e)
        {           
            btnAdd.Text = "Add Item";
            int categoryId = int.Parse(ddlMainCateGory.SelectedValue);
            int subCategoryId = int.Parse(ddlSubCategory.SelectedValue);
            ddlItemName.DataSource = addItemController.FetchItemsByCategories(categoryId, subCategoryId, int.Parse(Session["CompanyId"].ToString())).OrderBy(y => y.ItemId).ToList();
            ddlItemName.DataTextField = "ItemName";
            ddlItemName.DataValueField = "ItemId";
            ddlItemName.DataBind();
            ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));
        }

        protected void rdoCapitalExpense_CheckedChanged(object sender, EventArgs e)
        {
            divRadioBudget.Visible = true;
            divBudget.Visible = true;
            ddlPrType.SelectedValue = "8";
            if (rdoBudgetEnable.Checked)
            {
                divBudgetAmount.Visible = true;
                divBudgetInformation.Visible = true;
                divBudgetRemark.Visible = false;
            }
            else
            {
                divBudgetRemark.Visible = true;
            }
        }

        protected void rdoOperationalExpense_CheckedChanged(object sender, EventArgs e)
        {
            divRadioBudget.Visible = false;
            divBudget.Visible = false;
            ddlPrType.SelectedValue = "7";
        }

        protected void rdoBudgetEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBudgetEnable.Checked)
            {
                divBudgetRemark.Visible = false;
                divBudgetAmount.Visible = true;
                divBudgetInformation.Visible = true;
            }
            else
            {
                divBudgetRemark.Visible = true;
                divBudgetAmount.Visible = false;
                divBudgetInformation.Visible = false;
            }
        }

        protected void rdoBudgetDisable_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoBudgetDisable.Checked)
            {
                divBudgetRemark.Visible = true;
                divBudgetAmount.Visible = false;
                divBudgetInformation.Visible = false;
            }
            else
            {
                divBudgetAmount.Visible = true;
                divBudgetInformation.Visible = true;
                divBudgetRemark.Visible = false;
            }
        }

        protected void gvDataTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}