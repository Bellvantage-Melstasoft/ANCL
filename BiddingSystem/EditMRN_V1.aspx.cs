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
    public partial class EditMRN_V1 : System.Web.UI.Page
    {
        public string descriptionList = "";
        MRNmasterController mrnmasterController = ControllerFactory.CreateMRNmasterController();
        MRsupportiveDocumentController mrsupportiveDocumentController = ControllerFactory.CreateMRsupportiveDocumentController();
        MRNFileUploadController mrnFileUploadController = ControllerFactory.CreateMRNFileUploadController();
        MRNDetailController mrnDetailController = ControllerFactory.CreateMRNDetailController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        TempBOMController tempBOMController = ControllerFactory.CreateTempBOMController();
        TempPrFileUploadController tempPrFileUploadController = ControllerFactory.CreateTempPrFileUploadController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();

        TempPR_FileUploadReplacementController tempPR_FileUploadReplacementController = ControllerFactory.CreateTempPR_FileUploadReplacementController();
        Mrn_Replace_FileUpload_Controller mrn_Replace_FileUpload_Controller = ControllerFactory.CreateMrn_Replace_FileUpload_Controller();
        PrTypeController prTypeController = ControllerFactory.CreatePrTypeController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();

        TempMRN_BOMController tempMRN_BOMController = ControllerFactory.CreateTempMRN_BOMController();
        TempMRN_FileUploadController tempMRN_FileUploadController = ControllerFactory.CreateTempMRN_FileUploadController();
        TempMRN_SupportiveDocumentController tempMRN_SupportiveDocumentController = ControllerFactory.CreateTempMRN_SupportiveDocumentController();
        TempMRN_FileUploadReplacementController tempMRN_FileUploadReplacementController = ControllerFactory.TempMRN_FileUploadReplacementController();

        MRNBomController mrnBomController = ControllerFactory.CreateMRNBomController();
        MRexpenseController mrexpenseController = ControllerFactory.CreateMRexpenseController();

        public enum ReplacementRdo : int { No = 1, Yes }
        static string UserId = string.Empty;
        List<MrnDetails> listToBind = new List<MrnDetails>();
        List<MrnDetails> prDetailsListByMrnId = new List<MrnDetails>();
        List<TempMRN_BOM> TempBOMlistByMrnId = new List<TempMRN_BOM>();
        List<TempMRN_FileUpload> tempMrnFileUpload = new List<TempMRN_FileUpload>();
        List<int> ProcdeListWithId = new List<int>();
        public List<string> BomStringList = new List<string>();
        List<MrnDetails> GvBindPrDetail = new List<MrnDetails>();


        public int editRowIndex = 0;
        public string myText = string.Empty;

        int Mrnid = 0;
        static int departmentId = 0;
        static int itemId = 0;
        int ItemIdFilterd = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "EditMRN_V1.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewMRNLink";

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

            List<MRN_Master> mrn_Master = new List<MRN_Master>();
            mrn_Master = mrnmasterController.FetchDetailsToEdit(departmentId).OrderBy(x => x.MrnId).ToList();
            foreach (var item in mrn_Master)
            {
                ProcdeListWithId.Add(item.MrnId);
            }
            Session.Add("MRNCodeLists", ProcdeListWithId);

            msg.Visible = false;

            if (txtRequiredDate.Text != "")
            {
                txtRequiredDate.Text = Convert.ToDateTime(txtRequiredDate.Text).ToString("yyyy-MM-dd");
            }

            if (!IsPostBack)
            {
                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("itemId"), new DataColumn("Meterial"), new DataColumn("Description") });

                ViewState["Specification"] = dt;
                this.BindGrid();

                bindWarehouse();
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

                string MrncodeQryString = Request.QueryString.Get("MrnId");

                if (MrncodeQryString != null || MrncodeQryString != "")
                {
                    try
                    {
                        txtPRCode.Text = MrncodeQryString;
                        string mrnCode = txtPRCode.Text;
                        if (mrnCode == "")
                        {
                            lblPrCode.Text = "Required a MRN_CODE";
                            lblPrCode.Attributes.CssStyle.Add("color", "Red");
                        }
                        else
                        {
                            Session["MRNId"] = mrnCode;
                            lblPrCode.Text = "";
                            txtBudgetAmount.Text = "";
                            txtBudgetInformation.Text = "";
                            txtQuotationFor.Text = "";
                            List<MRN_Master> _MRN_MasterList = mrnmasterController.FetchDetailsToEdit(departmentId).Where(x => x.MrnId == int.Parse(mrnCode)).ToList();
                            foreach (var item in _MRN_MasterList)
                            {
                                ddlMainCateGory.SelectedValue = item.itemCatId.ToString();
                                ddlMainCateGory.Enabled = false;
                                if (ddlMainCateGory.SelectedValue != "")
                                {
                                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                                    LoadSubCategoryDDL(mainCategoryId);
                                }

                                txtPrNumber.Text = item.MrnId.ToString();
                                txtQuotationFor.Text = item.QuotationFor;
                                int companyId = item.CompanyId;
                                txtDepName.Text = companyDepartmentController.GetDepartmentByDepartmentId(companyId).DepartmentName;
                                string ExpenseType = item.ExpenseType;                                
                                DateTimeRequested.Text = item.CreatedDateTime.ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]);
                                txtRequiredDate.Text = item.ExpectedDate.ToString("yyyy-MM-dd");
                                txtMrnDescription.Text = item.Description.ToString();
                                Mrnid = item.MrnId;
                                Session["MRNId"] = Mrnid.ToString();
                                ddlPtType.SelectedValue = item.PurchaseType;
                                if (item.PrProcedure == "N")
                                {
                                    rdoNormalProcedure.Checked = true;
                                }
                                if (item.PrProcedure == "C")
                                {
                                    rdoCoveringProcedure.Checked = true;
                                }

                                ddlPrType.SelectedValue = item.PrTypeId.ToString();

                                if (item.ExpenseType != "")
                                {
                                    if (item.ExpenseType == "Operational Expense")
                                    {
                                        rdoOperationalExpense.Checked= true;
                                        rdoOperationalExpense_CheckedChanged(null, null);
                                    }
                                    else
                                    {
                                        rdoCapitalExpense.Checked = true;
                                        rdoCapitalExpense_CheckedChanged(null, null);
                                        MRexpense mrexpense = mrexpenseController.GetMRexpenseById(item.MrnId);
                                        if (mrexpense.IsBudget == 1)
                                        {
                                            rdoBudgetEnable.Checked = true;
                                            txtBudgetAmount.Text = mrexpense.BudgetAmount.ToString();
                                            txtBudgetInformation.Text = mrexpense.BudgetInfo;
                                        }else
                                        {
                                            rdoBudgetDisable.Checked = true;
                                            txtBudgetRemark.Text = mrexpense.Remarks.ToString();
                                        }
                                    }
                                }
                            }
                            LoadPrDetailGv();
                        }
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }
                }                
            }
            lblMessageAddItems.Text = "";
            lblImageDeletedMsg.Text = "";
            lblPrCode.Text = "";
            ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "InitClient();", true);
        }


        private void bindMeasurement()
        {
            try
            {
                ddlMeasurement.DataSource = unitMeasurementController.fetchMeasurementsByCompanyID(int.Parse(Session["CompanyId"].ToString()));
                ddlMeasurement.DataValueField = "measurentId";
                ddlMeasurement.DataTextField = "measurementShortName";
                ddlMeasurement.DataBind();
                // ddlMeasurement.Items.Insert(0, new ListItem("Select Measurememt", "0"));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void enableFields(bool value)
        {
            txtEnable.CssClass = "form-control";
            txtDisable.CssClass = "form-control";
            ddlSubCategory.Enabled = value;
            ddlItemName.Enabled = value;
            txtQty.Enabled = value;
            txtDescription.Enabled = value;
            txtPurpose.Enabled = value;
            txtEstimatedAmount.Enabled = value;
            //rdoEnable.Enabled = value;
            //rdoDisable.Enabled = value;
            txtEnable.Enabled = value;
            txtDisable.Enabled = value;
            btnAdd.Enabled = value;
        }


        [WebMethod]
        public static List<string> LoadPRCodes(string input)
        {
            EditMRN_V1 cp = new EditMRN_V1();
            input = input.Replace(" ", string.Empty);
            return ((List<string>)cp.Session["MRNCodeLists"]).FindAll(item => item.ToLower().Replace(" ", string.Empty).Contains(input.ToLower()));
        }


        protected void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                string mrnCode = txtPRCode.Text;
                int prcodeval = mrnmasterController.GetDetailsByMrnCode(departmentId, mrnCode);
                if (mrnCode == "")
                {
                    lblPrCode.Text = "Required a MRN_CODE";
                    lblPrCode.Attributes.CssStyle.Add("color", "Red");
                }
                if (prcodeval == 0)
                {
                    lblPrCode.Text = "MRN_CODE does not exist in the system.";
                    lblPrCode.Attributes.CssStyle.Add("color", "Red");
                }
                else
                {
                    lblPrCode.Text = "";
                    txtBudgetAmount.Text = "";
                    txtBudgetInformation.Text = "";
                    txtQuotationFor.Text = "";

                    List<MRN_Master> _MRN_MasterList = mrnmasterController.FetchDetailsToEdit(departmentId).Where(x => x.MrnId == int.Parse(mrnCode)).ToList();

                    if (_MRN_MasterList.Count != 0)
                    {
                        foreach (var item in _MRN_MasterList)
                        {
                            ddlMainCateGory.SelectedValue = item.itemCatId.ToString();
                            ddlMainCateGory.Enabled = false;
                            if (ddlMainCateGory.SelectedValue != "")
                            {
                                int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                                LoadSubCategoryDDL(mainCategoryId);

                            }

                            txtPrNumber.Text = item.MrnId.ToString();
                            txtQuotationFor.Text = item.QuotationFor;
                            int companyId = item.CompanyId;
                            txtDepName.Text = companyDepartmentController.GetDepartmentByDepartmentId(companyId).DepartmentName;
                            string ExpenseType = item.ExpenseType;
                            DateTimeRequested.Text = item.CreatedDateTime.ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]);
                            txtRequiredDate.Text = item.ExpectedDate.ToShortDateString();
                            txtMrnDescription.Text = item.Description.ToString();
                            Mrnid = item.MrnId;
                            Session["MRNId"] = Mrnid.ToString();
                            ddlPtType.SelectedValue = item.PurchaseType;
                            if (item.PrProcedure == "N")
                            {
                                rdoNormalProcedure.Checked = true;
                            }
                            if (item.PrProcedure == "C")
                            {
                                rdoCoveringProcedure.Checked = true;
                            }

                            ddlPrType.SelectedValue = item.PrTypeId.ToString();

                            if (item.ExpenseType != "")
                            {
                                if (item.ExpenseType == "Operational Expense")
                                {
                                    rdoOperationalExpense.Checked = true;
                                    rdoOperationalExpense_CheckedChanged(null, null);
                                }
                                else
                                {
                                    rdoCapitalExpense.Checked = true;
                                    rdoCapitalExpense_CheckedChanged(null, null);
                                    MRexpense mrexpense = mrexpenseController.GetMRexpenseById(item.MrnId);
                                    if (mrexpense.IsBudget == 1)
                                    {
                                        rdoBudgetEnable.Checked = true;
                                        txtBudgetAmount.Text = mrexpense.BudgetAmount.ToString();
                                        txtBudgetInformation.Text = mrexpense.BudgetInfo;
                                    }
                                    else
                                    {
                                        rdoBudgetDisable.Checked = true;
                                        txtBudgetRemark.Text = mrexpense.Remarks.ToString();
                                    }
                                }
                            }
                        }
                        LoadPrDetailGv();
                    }
                    else
                    {
                        lblPrCode.Text = "MRN_CODE does not exist in the system OR might be added to PR";
                        lblPrCode.Attributes.CssStyle.Add("color", "Red");
                    }
                    clearFields();
                    enableFields(true);
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { document.body.scrollTop = 750; document.documentElement.scrollTop = 750;});   </script>", false);

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
                List<MrnDetails> _MrnDetailsLists = mrnDetailController.FetchDetailsRejectedMRN(int.Parse(Session["MRNId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                GvBindPrDetail = _MrnDetailsLists.ToList();
                gvDataTable.DataSource = _MrnDetailsLists;
                gvDataTable.DataBind();
                txtEstimatedCost.Text = _MrnDetailsLists.Sum(t => t.EstimatedAmount*t.RequestedQty).ToString();
                txtEstimatedCost.Enabled = false;
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

                Mrnid = int.Parse(gvDataTable.Rows[x].Cells[1].Text);
                itemId = int.Parse(gvDataTable.Rows[x].Cells[7].Text);

                hdnMrnid.Value = Mrnid.ToString();
                hdnitemId.Value = itemId.ToString();

                string itemName = (gvDataTable.Rows[x].Cells[8].Text);
                btnAdd.Text = "Update Item";

                List<MrnDetails> GetMRNDetailsByMRID = new List<MrnDetails>();
                GetMRNDetailsByMRID = mrnDetailController.FetchMRN_DetailsByMRNIdList(Mrnid);
                if (GetMRNDetailsByMRID.Count() > 0)
                {

                    MrnDetails mrnDetails = GetMRNDetailsByMRID.Find(a => (a.MrnId == Mrnid) && (a.ItemId == itemId));

                    LoadDDLMainCatregory();
                    ddlMainCateGory.SelectedValue = mrnDetails.MainCategoryId.ToString();

                    ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(mrnDetails.MainCategoryId, int.Parse(Session["CompanyId"].ToString()));
                    ddlSubCategory.DataValueField = "SubCategoryId";
                    ddlSubCategory.DataTextField = "SubCategoryName";
                    ddlSubCategory.DataBind();
                    ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));


                    // ddlItemName.Text = itemName;
                    ddlItemName.DataSource = addItemController.FetchItemsByCategories(mrnDetails.MainCategoryId, mrnDetails.SubCategoryId, int.Parse(Session["CompanyId"].ToString())).OrderBy(y => y.ItemId).ToList();
                    ddlItemName.DataTextField = "ItemName";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));

                    ddlItemName.Enabled = false;
                    ddlSubCategory.Enabled = false;
                    ddlMainCateGory.Enabled = false;


                    ddlSubCategory.SelectedValue = mrnDetails.SubCategoryId.ToString();
                    // ddlItemName.Text = mrnDetails.ItemName;
                    ddlItemName.SelectedValue = mrnDetails.ItemId.ToString();
                    txtQty.Text = mrnDetails.RequestedQty.ToString();
                    txtDescription.Text = mrnDetails.Description;
                    txtEstimatedAmount.Text = mrnDetails.EstimatedAmount.ToString();
                    if (mrnDetails.Replacement == 1)
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
                    // txtBiddingPeriod.Text = "2";                     
                    txtPurpose.Text = mrnDetails.Purpose;
                    ddlMeasurement.SelectedValue = mrnDetails.MeasurementId.ToString();
                    if (mrnDetails.SampleProvided == 1)
                    {
                        rdoFileSampleEnable.Checked = true;
                    }
                    else
                    {
                        rdoFileSampleDisable.Checked = true;
                    }

                    TempBOMlistByMrnId = tempMRN_BOMController.GetBOMListByMrnIdItemId(departmentId, Mrnid, itemId);
                    tempMrnFileUpload = tempMRN_FileUploadController.GetTempMrnFiles(itemId, Mrnid);

                    List<MRNBom> _MRNBom = mrnBomController.GetList(Mrnid, itemId);//
                    lblcount.Text = _MRNBom.Count.ToString();

                    if (_MRNBom.Count != 0)
                    {
                        foreach (var item in _MRNBom)
                        {
                            BomStringList.Add(item.Meterial + "-" + item.Description);
                        }
                    }

                    if (TempBOMlistByMrnId.Count != 0)
                    {
                        foreach (var item in TempBOMlistByMrnId)
                        {
                            BomStringList.Add(item.Meterial + "-" + item.Description);
                        }
                    }
                    gvRepacementImages.DataSource = mrn_Replace_FileUpload_Controller.FtechUploadeFiles(Mrnid, itemId);
                    gvRepacementImages.DataBind();

                    gvPrUploadedFiles.DataSource = mrnFileUploadController.FtechUploadeFiles(Mrnid, itemId);
                    gvPrUploadedFiles.DataBind();

                    gvSupporiveFiles.DataSource = mrsupportiveDocumentController.FtechUploadeSupporiveFiles(Mrnid, itemId);
                    gvSupporiveFiles.DataBind();

                    enableFields(true);



                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  document.body.scrollTop = 50; document.documentElement.scrollTop = 50;});   </script>", false);

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
            List<MRNBom> _MRNBom = mrnBomController.GetList(Mrnid, itemId);
            foreach (var item in _MRNBom)
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
            string MrnId = string.Empty;
            string ItemId = string.Empty;

            if (jsonData != "")
            {
                string[] value = jsonData.Split('-');
                string val1 = value[0];
                string val2 = value[1];
                MrnId = val1;
                ItemId = val2;
                MRNBomController mrnBomController = ControllerFactory.CreateMRNBomController();
                List<MRNBom> mrn_BillOfMeterial = mrnBomController.GetList(int.Parse(MrnId), int.Parse(ItemId));

                foreach (var item in mrn_BillOfMeterial)
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

                mrnBomController.DeleteMRNBom(int.Parse(Session["MRNId"].ToString()), itemId);
                mrnFileUploadController.DeleteFileUpload(int.Parse(Session["MRNId"].ToString()), itemId);
                int mrnDetaDelete = mrnDetailController.DeleteMRNDetailByMRNIDAndItemId(int.Parse(Session["MRNId"].ToString()), itemId);
                if (mrnDetaDelete > 0)
                {
                    LoadPrDetailGv();
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //DisplayMessage("PR Detail has been Delete Successfully", false);
                }
                else
                {
                    DisplayMessage("Error On Delete MRN", true);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------------MRN Details 
        private void LoadMrnDetailGv()
        {
            try
            {
                List<MrnDetails> _MRN_DetailsLists = mrnDetailController.FetchDetailsRejectedMRN(int.Parse(Session["MrnId"].ToString()), int.Parse(Session["CompanyId"].ToString()));
                GvBindPrDetail = _MRN_DetailsLists.ToList();
                gvDataTable.DataSource = _MRN_DetailsLists;
                gvDataTable.DataBind();
                txtEstimatedCost.Text = _MRN_DetailsLists.Sum(t => t.EstimatedAmount).ToString();
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

        //---------------Load Sub Category DDL
        protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(ddlMainCateGory.SelectedValue) != 0 || ddlMainCateGory.SelectedValue != "")
                {
                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    //ddlItemName.Text = "";
                    LoadSubCategoryDDL(mainCategoryId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void bindWarehouse()
        {
            try
            {
                WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();
                ddlWarehouse.DataSource = warehouseController.getWarehouseList1();
                ddlWarehouse.DataValueField = "warehouseID";
                ddlWarehouse.DataTextField = "location";
                ddlWarehouse.DataBind();
                //ddlWarehouse.Items.Insert(0, new ListItem("Select Warehouse", "0"));
            }
            catch (Exception)
            {

                throw;
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
                if (Session["MRNId"] != null)
                {
                    string ExpenseType = "Capital Expense";
                    int isApproved = 0;
                    if (rdoOperationalExpense.Checked == true)
                    {
                        ExpenseType = "Operational Expense";
                        isApproved = 1;
                    }


                    MRN_Master _MRN_Master = mrnmasterController.FetchMRNByMrnId(int.Parse(Session["MRNId"].ToString()));
                    string procedure = "C";
                    if (rdoNormalProcedure.Checked)
                    {
                        procedure = "N";
                    }
                    string purchaseType = ddlPtType.SelectedValue;

                    int UpdateMRN = mrnmasterController.UpdateMRMaster(int.Parse(Session["MRNId"].ToString()), _MRN_Master.CompanyId, _MRN_Master.SubDepartmentId, LocalTime.Now, txtDescription.Text, Convert.ToDateTime(txtRequiredDate.Text), UserId, 0, 0, 1, txtQuotationFor.Text, int.Parse(ddlPrType.SelectedValue), ExpenseType, procedure, purchaseType);

                    MRexpense mrexpense = mrexpenseController.GetMRexpenseById(int.Parse(Session["MRNId"].ToString()));
                    int Is_Budget = 0;
                    if (rdoBudgetEnable.Checked)
                    {
                        Is_Budget = 1;
                    }

                    if (rdoOperationalExpense.Checked == true)
                    {
                        txtBudgetAmount.Text = "0";
                        txtBudgetInformation.Text = "";
                        txtBudgetRemark.Text = "";
                    }
                    else
                    {
                        if (rdoBudgetDisable.Checked == true)
                        {
                            txtBudgetAmount.Text = "0";
                            txtBudgetInformation.Text = "";
                        }
                    }
                    mrexpenseController.UpdateMRExpense(int.Parse(Session["MRNId"].ToString()), Is_Budget,Convert.ToDecimal(txtBudgetAmount.Text), txtBudgetRemark.Text, txtBudgetInformation.Text , isApproved);
                    

                    if (UpdateMRN > 0)
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);

                        Session["MRNId"] = null;
                        clearFields();
                        clearPrDetails();
                        ddlMainCateGory.SelectedIndex = 0;
                        ddlMainCateGory.Enabled = true;
                        txtDescription.Text = "";
                        txtQuotationFor.Text = "";
                        txtEstimatedAmount.Text = "";
                        txtBudgetAmount.Text = "";
                        txtBudgetInformation.Text = "";
                        txtBudgetRemark.Text = "";
                        txtEstimatedCost.Text = "";
                    }
                    else
                    {
                        DisplayMessage("Error on Update MRN", true);
                    }
                }
                else
                {
                    DisplayMessage("Please Select Material Requistion", true);
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
            txtDescription.Text = "";
            txtEstimatedAmount.Text = "";
            txtRequiredDate.Text = "";
            DateTimeRequested.Text = "";
            gvDataTable.DataSource = null;
            gvDataTable.DataBind();

        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {

                if (Session["MRNId"] != null)
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
                            int saveMRN_Detail = mrnDetailController.SaveMRNDetails(int.Parse(Session["MRNId"].ToString()), ItemIdFilterd, int.Parse(ddlMeasurement.SelectedValue), txtDescription.Text, UserId, LocalTime.Now, 1, replaceMentId, decimal.Parse(txtQty.Text), txtPurpose.Text, decimal.Parse(txtEstimatedAmount.Text), fileSampleProvided, txtPurpose.Text, int.Parse(ddlMeasurement.SelectedValue));
                            // int savePR_Detail = 0;
                            if (saveMRN_Detail > 0)
                            {
                                DataTable dt = (DataTable)ViewState["Specification"];

                                foreach (DataRow row in dt.Rows)
                                {
                                    mrnBomController.SaveBillOfMeterial(int.Parse(Session["MRNId"].ToString()), ItemIdFilterd, row["Meterial"].ToString(), row["Description"].ToString(), 1, LocalTime.Now, UserId, LocalTime.Now, UserId);

                                }

                                // Update File Upload Items 

                                string folderFilePath = string.Empty;


                                HttpFileCollection uploads = HttpContext.Current.Request.Files;
                                for (int i = 0; i < uploads.Count; i++)
                                {
                                    if (uploads.AllKeys[i] == "files[]")
                                    {
                                        List<MRNFileUpload> GetFilesByPrItemId = mrnFileUploadController.FtechUploadeFiles(int.Parse(Session["MrnId"].ToString()), ItemIdFilterd);
                                        int maxNumber = 0;

                                        foreach (var item in GetFilesByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploads[i];

                                        string CreateFileName = Session["MRNId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + Session["MRNId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PurchaseRequestFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PurchaseRequestFiles/" + Session["MrnId"].ToString() + "/" + filename01;
                                            int saveFilePath = mrnFileUploadController.SaveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["MRNId"].ToString()), folderFilePath, FileName);
                                        }

                                    }
                                    if (uploads.AllKeys[i] == "fileReplace[]")
                                    {

                                        List<Mrn_Replace_File_Upload> GetReplacementFilesByPrItemId = mrn_Replace_FileUpload_Controller.FtechUploadeFiles(int.Parse(Session["MRNId"].ToString()), ItemIdFilterd);

                                        int maxNumber = 0;

                                        foreach (var item in GetReplacementFilesByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploads[i];

                                        string CreateFileName = Session["MRNId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + Session["MRNId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrReplacementFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PrReplacementFiles/" + Session["MRNId"].ToString() + "/" + filename01;
                                            int saveFilePath = mrn_Replace_FileUpload_Controller.SaveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["MRNId"].ToString()), folderFilePath, FileName);
                                        }
                                    }

                                    if (uploads.AllKeys[i] == "supportivefiles[]")
                                    {
                                        List<MRNSupportiveDocument> GetMRN_SupportiveDocumentsByMrnItemId = mrsupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(Session["MRNId"].ToString()), ItemIdFilterd);

                                        int maxNumber = 0;

                                        foreach (var item in GetMRN_SupportiveDocumentsByMrnItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploads[i];

                                        string CreateFileName = Session["MRNId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + Session["MRNId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrSupportiveFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PrSupportiveFiles/" + Session["MRNId"].ToString() + "/" + filename01;
                                            int saveFilePath = mrsupportiveDocumentController.SaveSupporiveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["MRNId"].ToString()), folderFilePath, FileName);
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
                                DisplayMessage("This MRN Detail already Exist", true);
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
                                //ItemIdFilterd = addItemController.GetIdByItemName(departmentId, ddlItemName.Text.Trim());
                                ItemIdFilterd = int.Parse(ddlItemName.SelectedValue);
                            }
                            int fileSampleProvided = 1;
                            if (rdoFileSampleDisable.Checked)
                            {
                                fileSampleProvided = 0;
                            }
                            int updatePR_Detail = mrnDetailController.UpdateMRNDetails(int.Parse(Session["MRNId"].ToString()), itemId, ItemIdFilterd, int.Parse(ddlMeasurement.SelectedValue), txtDescription.Text, UserId, LocalTime.Now, 1, replaceMentId, decimal.Parse(txtQty.Text), txtPurpose.Text, decimal.Parse(txtEstimatedAmount.Text), fileSampleProvided, txtPurpose.Text, int.Parse(ddlMeasurement.SelectedValue));

                            if (updatePR_Detail > 0)
                            {
                                mrnBomController.DeleteMRNBoMTrash(int.Parse(Session["MRNId"].ToString()), itemId);

                                DataTable dt = (DataTable)ViewState["Specification"];

                                foreach (DataRow row in dt.Rows)
                                {
                                    mrnBomController.SaveBillOfMeterial(int.Parse(Session["MRNId"].ToString()), ItemIdFilterd, row["Meterial"].ToString(), row["Description"].ToString(), 1, LocalTime.Now, UserId, LocalTime.Now, UserId);
                                }


                                // Update File Upload Items 

                                string folderFilePath = string.Empty;

                                HttpFileCollection uploadsT = HttpContext.Current.Request.Files;
                                for (int i = 0; i < uploadsT.Count; i++)
                                {
                                    if (uploadsT.AllKeys[i] == "files[]")
                                    {
                                        List<MRNFileUpload> GetFilesByMrnItemId = mrnFileUploadController.FtechUploadeFiles(int.Parse(Session["MRNId"].ToString()), ItemIdFilterd);
                                        int maxNumber = 0;

                                        foreach (var item in GetFilesByMrnItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploadsT[i];

                                        string CreateFileName = Session["MRNId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + Session["MRNId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PurchaseRequestFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PurchaseRequestFiles/" + Session["MRNId"].ToString() + "/" + filename01;
                                            int saveFilePath = mrnFileUploadController.SaveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["MRNId"].ToString()), folderFilePath, FileName);
                                        }

                                    }
                                    if (uploadsT.AllKeys[i] == "fileReplace[]")
                                    {

                                        List<Mrn_Replace_File_Upload> GetReplacementFilesByPrItemId = mrn_Replace_FileUpload_Controller.FtechUploadeFiles(int.Parse(Session["MRNId"].ToString()), ItemIdFilterd);

                                        int maxNumber = 0;

                                        foreach (var item in GetReplacementFilesByPrItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploadsT[i];

                                        string CreateFileName = Session["MRNId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + Session["MRNId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrReplacementFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PrReplacementFiles/" + Session["MRNId"].ToString() + "/" + filename01;
                                            int saveFilePath = mrn_Replace_FileUpload_Controller.SaveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["MRNId"].ToString()), folderFilePath, FileName);
                                        }
                                    }


                                    if (uploadsT.AllKeys[i] == "supportivefiles[]")
                                    {

                                        List<MRNSupportiveDocument> GetSupportiveDocumentByMRNItemId = mrsupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(Session["MRNId"].ToString()), ItemIdFilterd);

                                        int maxNumber = 0;

                                        foreach (var item in GetSupportiveDocumentByMRNItemId)
                                        {
                                            int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                            if (CalNumber > maxNumber)
                                                maxNumber = CalNumber;
                                        }

                                        HttpPostedFile postedFile = uploadsT[i];

                                        string CreateFileName = Session["MRNId"].ToString() + "_" + ItemIdFilterd + "_" + (maxNumber + 1).ToString();
                                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                        string FileName = Path.GetFileName(postedFile.FileName);
                                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                        if (postedFile.ContentLength > 0)
                                        {
                                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + Session["MRNId"].ToString() + "/" + filename01)))
                                            {
                                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            }
                                            postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/PrSupportiveFiles/" + Session["MRNId"].ToString() + "/" + filename01));
                                            folderFilePath = "~/PrSupportiveFiles/" + Session["MRNId"].ToString() + "/" + filename01;
                                            int saveSupportiveFilePath = mrsupportiveDocumentController.SaveSupporiveFileUpload(departmentId, ItemIdFilterd, int.Parse(Session["MRNId"].ToString()), folderFilePath, FileName);
                                        }
                                    }


                                }

                                DisplayMessage("This MRN Item has been Updated Successfully", false);
                                LoadPrDetailGv();
                                clearFields();
                            }
                            else
                            {
                                DisplayMessage("This MRN Detail already Exist", true);

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
                lblcount.Text = "0";

                btnAdd.Text = "Add Item";
                txtPurpose.Text = "";
                // txtMrnDescription.Text = "";
                txtDescription.Text = "";
                txtQty.Text = "";
                txtEstimatedAmount.Text = "";
                //  LoadDDLMainCatregory();
                gvPrUploadedFiles.DataSource = null;
                gvPrUploadedFiles.DataBind();

                gvSupporiveFiles.DataSource = null;
                gvSupporiveFiles.DataBind();

                gvRepacementImages.DataSource = null;
                gvRepacementImages.DataBind();

                ddlMainCateGory.Enabled = false;
                //ddlSubCategory.Items.Clear();
                //ddlSubCategory.DataBind();

               // ddlItemName.Text = "";
                //ddlItemName.DataBind();

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
                string Mrnid = hndImageMrnid.Value;
                string itemid = hndImageItemId.Value;
                string imagepath = hdnImageFilePath.Value;
                int deleteImages = mrnFileUploadController.DeleteParticularFile(int.Parse(Mrnid), int.Parse(itemid), imagepath);

                if (deleteImages > 0)
                {
                    lblImageDeletedMsg.Text = "Image deleted successfully";
                    lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Green");
                    gvPrUploadedFiles.DataSource = mrnFileUploadController.FtechUploadeFiles(int.Parse(Mrnid), int.Parse(itemid));
                    gvPrUploadedFiles.DataBind();

                    var SourcePath = Server.MapPath("PurchaseRequestFiles/" + Mrnid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("PurchaseRequestFiles/" + Mrnid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    lblImageDeletedMsg.Text = "Action unsuccessfull";
                    lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Red");
                    gvPrUploadedFiles.DataSource = mrnFileUploadController.FtechUploadeFiles(int.Parse(Mrnid), int.Parse(itemid));
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
                string Mrnid = gvRepacementImages.Rows[x].Cells[0].Text;
                string itemid = gvRepacementImages.Rows[x].Cells[1].Text;
                string imagepath = gvRepacementImages.Rows[x].Cells[2].Text;
                int deleteImages = mrn_Replace_FileUpload_Controller.DeleteParticularReplaceFile(int.Parse(Mrnid), int.Parse(itemid), imagepath);

                if (deleteImages > 0)
                {
                    lblReplaceimageDelete.Text = "Image deleted successfully";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Green");
                    gvRepacementImages.DataSource = mrn_Replace_FileUpload_Controller.FtechUploadeFiles(int.Parse(Mrnid), int.Parse(itemid));
                    gvRepacementImages.DataBind();

                    var SourcePath = Server.MapPath("PrReplacementFiles/" + Mrnid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("PrReplacementFiles/" + Mrnid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    lblReplaceimageDelete.Text = "Action unsuccessfull";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Red");
                    gvRepacementImages.DataSource = mrn_Replace_FileUpload_Controller.FtechUploadeFiles(int.Parse(Mrnid), int.Parse(itemid));
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
                //if (ddlPrType.SelectedValue == "1")
                //{
                //    divJobNo.Visible = false;
                //    divVehicleNo.Visible = false;
                //    divMake.Visible = false;
                //    divModel.Visible = false;
                //}
                //else if (ddlPrType.SelectedValue == "2")
                //{
                //    divJobNo.Visible = true;
                //    divVehicleNo.Visible = true;
                //    divMake.Visible = true;
                //    divModel.Visible = true;
                //}

                //if (ddlPrType.SelectedValue == "7")
                //{
                //    divBudget.Visible = false;
                //    rdoBudgetDisable.Checked = true;
                //}
                //else
                //{
                //    divBudget.Visible = true;
                //    rdoBudgetEnable.Checked = true;
                //}

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
                string Mrnid = gvSupporiveFiles.Rows[x].Cells[0].Text;
                string itemid = gvSupporiveFiles.Rows[x].Cells[1].Text;
                string imagepath = gvSupporiveFiles.Rows[x].Cells[2].Text;
                int deleteSupporiveDocument = mrsupportiveDocumentController.DeleteParticularSupporiveFile(int.Parse(Mrnid), int.Parse(itemid), imagepath);

                if (deleteSupporiveDocument > 0)
                {
                    lblSupporiveDelete.Text = "Document has been deleted successfully";
                    lblSupporiveDelete.Attributes.CssStyle.Add("color", "Green");
                    gvSupporiveFiles.DataSource = mrsupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(Mrnid), int.Parse(itemid));
                    gvSupporiveFiles.DataBind();

                    var SourcePath = Server.MapPath("PrSupportiveFiles/" + Mrnid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("PrSupportiveFiles/" + Mrnid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    lblSupporiveDelete.Text = "Action unsuccessfull";
                    lblSupporiveDelete.Attributes.CssStyle.Add("color", "Red");
                    gvSupporiveFiles.DataSource = mrsupportiveDocumentController.FtechUploadeSupporiveFiles(int.Parse(Mrnid), int.Parse(itemid));
                    gvSupporiveFiles.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {

            if (txtmeterialNew.Text != "" || txtdescriptionNew.Text != "")
            {

                int mrnId = int.Parse(hdnMrnid.Value);
                int itemId = int.Parse(hdnitemId.Value);

                string meterial = txtmeterialNew.Text;
                string description = txtdescriptionNew.Text;

                DataTable dt = (DataTable)ViewState["Specification"];
                dt.Rows.Add(itemId, meterial, description);

                this.BindGrid();
                ViewState["Specification"] = dt;

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $('.modal-backdrop').remove();  $('#specification').modal('show'); </script>", false);

                txtmeterialNew.Text = txtdescriptionNew.Text = null;
            }
        }

        protected void BindGrid()
        {
            DataTable dt = (DataTable)ViewState["Specification"];
            lblcount.Text = dt.Rows.Count.ToString();
            gvSpecificationBoms.DataSource = (DataTable)ViewState["Specification"];
            gvSpecificationBoms.DataBind();
        }

        protected void ButtonClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>  $('.modal-backdrop').remove();    </script>", false);
        }
        protected void btnDeleteRow_Click(object sender, EventArgs e)
        {
            int RowId = ((GridViewRow)((ImageButton)sender).Parent.Parent).RowIndex;
            DataTable dt = (DataTable)ViewState["Specification"];
            dt.Rows[RowId].Delete();
            ViewState["Specification"] = dt;
            BindGrid();
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>  $('.modal-backdrop').remove();  $('#specification').modal('show');   </script>", false);

        }
        protected void btnexisting_Click(object sender, EventArgs e)
        {

            if (ddlItemName.SelectedItem.Text != "" && ddlItemName.SelectedItem.Text != null)
            {
                int mrnId = 0;
                int itemId = 0;

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("itemId"), new DataColumn("Meterial"), new DataColumn("Description") });

                if (btnAdd.Text == "Update Item")
                {
                    mrnId = int.Parse(hdnMrnid.Value);
                    itemId = int.Parse(hdnitemId.Value);

                    if (dt.Rows.Count == 0)
                    {
                        List<MRNBom> additemspecification = mrnBomController.GetList(mrnId, itemId);

                        additemspecification.ToList().ForEach(i => dt.Rows.Add(i.ItemId, i.Meterial, i.Description));
                    }

                }

                ViewState["Specification"] = dt;

                BindGrid();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#specification').modal('show');   </script>", false);
            }
            else
            {
                DisplayMessage("Please fill Item name", true);
            }

        }

       

        protected void ddlItemName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0)
                {
                    int itemId = addItemController.GetIdByItemName(int.Parse(Session["CompanyId"].ToString()), ddlItemName.SelectedItem.Text);
                    if (itemId == 0)
                    {
                        ddlItemName.Text = "";
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Invalid Item name\"; $('#errorAlert').modal('show'); });   </script>", false);
                    }
                    else
                    {
                        if (txtPrNumber.Text != string.Empty)
                        {
                            hdnMrnid.Value = txtPrNumber.Text;
                            hdnitemId.Value = itemId.ToString();
                        }

                    }
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


        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnNewItem_Click(object sender, EventArgs e)
        {
            btnAdd.Text = "Add Item";
            
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
    }
}