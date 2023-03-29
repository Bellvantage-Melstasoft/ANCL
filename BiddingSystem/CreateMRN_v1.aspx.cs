using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.Text;
using CLibrary.Domain;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Data;

namespace BiddingSystem
{
    public partial class CreateMRN_v1 : System.Web.UI.Page
    {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        ItemImageUploadController itemImageUploadController = ControllerFactory.CreateItemImageUploadController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
        PrTypeController prTypeController = ControllerFactory.CreatePrTypeController();
        Mrn_Replace_FileUpload_Controller mrn_Replace_FileUpload_Controller = ControllerFactory.CreateMrn_Replace_FileUpload_Controller();
        MRNmasterController MRNmasterController = ControllerFactory.CreateMRNmasterController();
        MRexpenseController mrexpenseController = ControllerFactory.CreateMRexpenseController();
        MRNDetailController mrnDetailController = ControllerFactory.CreateMRNDetailController();
        MRNBomController mrnBomController = ControllerFactory.CreateMRNBomController();
        MRNFileUploadController mrnFileUploadController = ControllerFactory.CreateMRNFileUploadController();
        MRsupportiveDocumentController mrsupportiveDocumentController = ControllerFactory.CreateMRsupportiveDocumentController();
        TempMRN_BOMController tempMRN_BOMController = ControllerFactory.CreateTempMRN_BOMController();
        TempMRN_FileUploadController tempMRN_FileUploadController = ControllerFactory.CreateTempMRN_FileUploadController();
        TempMRN_SupportiveDocumentController tempMRN_SupportiveDocumentController = ControllerFactory.CreateTempMRN_SupportiveDocumentController();
        TempMRN_FileUploadReplacementController tempMRN_FileUploadReplacementController = ControllerFactory.TempMRN_FileUploadReplacementController();

        static string UserId = string.Empty;
        static int CompanyId = 0;
        static string SubDepartment = string.Empty;
        static int itemId = 0;
        List<TempDataSet> listToBind = new List<TempDataSet>();
        List<TempMRN_BOM> TempBOMlistByMrnId = new List<TempMRN_BOM>();
        public List<string> BomStringList = new List<string>();
        public int editRowIndex = 0;
        int mrnid = 0;
        public int ItemIdFilterd = 0;
        public enum ReplacementRdo : int { No = 1, Yes }
        public static double estimatedCost = 0.00;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ViewState["CompanyIdData"] = Session["CompanyId"].ToString();
                ViewState["UserIdData"] = Session["UserId"].ToString();
            }

            if (ViewState["CompanyIdData"] != null && ViewState["UserIdData"].ToString() != "")
            {
                Session["CompanyId"] = ViewState["CompanyIdData"].ToString();
                Session["UserId"] = ViewState["UserIdData"].ToString();
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "CreateMRN_v1.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createMRNLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                SubDepartment = companyLogin.SubDepartmentID;
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 5, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }

            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            txtDepName.Enabled = false;
            txtPrNumber.Enabled = false;
            msg.Visible = false;

            if (!IsPostBack)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt.Columns.AddRange(new DataColumn[3] { new DataColumn("itemId"), new DataColumn("Meterial"), new DataColumn("Description") });

                    ViewState["Specification"] = dt;
                    this.BindGrid();

                    Session["FinalListPRCreate"] = null;
                    DateTimeRequested.Text = LocalTime.Now.ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]);
                    //Add one row in the table
                    LoadPrTypes();
                    LoadPtTypes();
                    bindWarehouse();
                    LoadDDLMainCatregory();
                    //Delete Temp BOM Data and Temp FileUpload Data
                    tempMRN_BOMController.DeleteTempDataByDeptId(CompanyId);
                    tempMRN_FileUploadController.DeleteTempDataFileUploadDepartmentId(CompanyId);

                    tempMRN_FileUploadReplacementController.DeleteTempDataFileUploadCompanyId(CompanyId);
                    tempMRN_SupportiveDocumentController.DeleteTempSupporiveFileUploadCompanyId(CompanyId);

                    //---Delete From Temporary Files
                    int meterialRequestId = tempMRN_BOMController.GetNextMrnIdObj(CompanyId);
                    var SourcePath = Server.MapPath("TempPurchaseRequestFiles");
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                    // Delete temp Replacement Images from directory
                    var SourcePathReplacementImages = Server.MapPath("TempPrReplacementFiles");
                    System.IO.DirectoryInfo dis = new DirectoryInfo(SourcePathReplacementImages);

                    foreach (FileInfo file in dis.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in dis.GetDirectories())
                    {
                        dir.Delete(true);
                    }

                    var SourcePathSupportiveDocument = Server.MapPath("TempPrReplacementFiles");
                    System.IO.DirectoryInfo dif = new DirectoryInfo(SourcePathSupportiveDocument);

                    foreach (FileInfo file in dif.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo did in dif.GetDirectories())
                    {
                        did.Delete(true);
                    }


                    bindMeasurement();

                    if (Session["CompanyId"].ToString() == "3")
                    {
                        if (ddlPrType.SelectedValue == "4")
                        {
                            divJobNo.Visible = false;
                            divVehicleNo.Visible = false;
                            divMake.Visible = false;
                            divModel.Visible = false;
                        }
                    }
                    txtRequiredDate.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            else
            {

                if (Session["FinalListPRCreate"] != null)
                {
                    listToBind.AddRange(((List<TempDataSet>)Session["FinalListPRCreate"]).ToList());
                }

            }

            if (rdoCapitalExpense.Checked)
            {
                rdoCapitalExpense_CheckedChanged(null, null);
            }
            else
            {
                rdoOperationalExpense_CheckedChanged(null, null);
            }

            int MrnId = tempMRN_BOMController.GetNextMrnIdObj((CompanyId));
            string companyName = companyDepartmentController.GetDepartmentByDepartmentId((CompanyId)).DepartmentName;

            int mrnCode = MRNmasterController.FetchMRCode(CompanyId);

            txtDepName.Text = companyName;
            txtPrNumber.Text = mrnCode.ToString();
            txtRequestedBy.Text = Session["UserNameA"].ToString();
            lblAlertMsg.Text = "";
            ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "InitClient();", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>     $('#itemCount').text($('#tableCols tr').length - 1);   </script>", false);

        }

        private void LoadPrTypes()
        {
            try
            {
                ddlPrType.DataSource = prTypeController.FetchPRTypesByCompanyId(CompanyId);
                ddlPrType.DataValueField = "PrTypeId";
                ddlPrType.DataTextField = "PrTypeName";
                ddlPrType.DataBind();

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

                // Done For ANCL
                if (rdoCapitalExpense.Checked)
                {
                    ddlPrType.SelectedValue = "8";
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
                throw;
            }
        }

        private void LoadDDLSubCatregory()
        {

            if (ddlMainCateGory.SelectedValue != "")
            {
                int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                LoadSubCategoryDDL(mainCategoryId);
            }
        }

        protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMainCateGory.SelectedIndex != 0 && ddlMainCateGory.SelectedValue != "")
                {
                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    LoadSubCategoryDDL(mainCategoryId);
                    lblSubcatergory.InnerText = "(*)";
                    lblSubcatergory.Visible = true;
                    lblItemCategory.InnerText = "(*)";
                    lblItemCategory.Visible = true;
                    lblItemQuantity.InnerText = "(*)";
                    lblItemQuantity.Visible = true;
                    lblDescription.InnerText = "(*)";
                    lblDescription.Visible = true;

                    lblMainCategory.Visible = false;
                }
                else
                {
                    ddlSubCategory.Items.Clear();
                    ddlItemName.Items.Clear();
                    lblMainCategory.InnerText = "(*)";
                    lblMainCategory.Visible = true;
                }

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
                    PODetailsController poDetailsController = ControllerFactory.CreatePODetailsController();
                    POHistory purchaseOrderHistory = poDetailsController.GetPoHistoryByItemId(int.Parse(ddlItemName.SelectedValue));
                    txtLastPurchaseSupplier.Text = purchaseOrderHistory.SupplierName;
                    hndSupplierId.Value = purchaseOrderHistory.SupplierId.ToString();
                    txtPurchaseOrderNo.Text = purchaseOrderHistory.PurchaseOrderId.ToString();
                    txtLastPurchasePrice.Text = purchaseOrderHistory.ItemPrice.ToString();
                    txtLastPurchaseDate.Text = purchaseOrderHistory.PurchaseDate.ToString();
                    hdnLastPurchasePrice.Value = purchaseOrderHistory.ItemPrice.ToString();
                    hdnLastPurchaseDate.Value = purchaseOrderHistory.PurchaseDate.ToString();

                    lblItemCategory.Visible = false;
                }
                else
                {
                    lblItemCategory.Visible = true;
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

        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubCategory.SelectedIndex != 0 && ddlSubCategory.SelectedValue != "")
                {
                    Session["MainCategoryId"] = ddlMainCateGory.SelectedValue;
                    Session["SubCategoryId"] = ddlSubCategory.SelectedValue;
                    int categoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    int subCategoryId = int.Parse(ddlSubCategory.SelectedValue);

                    ddlItemName.DataSource = addItemController.FetchItemsByCategories(categoryId, subCategoryId, CompanyId).OrderBy(y => y.ItemId).ToList();
                    ddlItemName.DataTextField = "ItemName";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));

                    lblSubcatergory.Visible = false;
                }
                else
                {
                    ddlItemName.Items.Clear();
                    lblSubcatergory.Visible = true;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyPurchaseRequestNote.aspx");
        }

        protected void btnSavePR_Click(object sender, EventArgs e)
        {
            try
            {
                string procedure = "C";
                if (rdoNormalProcedure.Checked)
                {
                    procedure = "N";
                }
                int Is_Budget = 0;
                if (rdoBudgetEnable.Checked)
                {
                    Is_Budget = 1;
                }

                string purchaseType = ddlPtType.SelectedValue;

                if (gvDatataTable.Rows.Count > 0)
                {
                    string ExpenseType = "Capital Expense";
                    int isApproved = 0;
                    if (rdoOperationalExpense.Checked == true)
                    {
                        ExpenseType = "Operational Expense";
                        isApproved = 1;
                    }

                    int meterialRequestId = MRNmasterController.SaveMRMaster(CompanyId, int.Parse(SubDepartment), LocalTime.Now, txtMrnDescription.Text, DateTime.Parse(txtRequiredDate.Text), UserId, 0, 0, 1, txtQuotationFor.Text, int.Parse(ddlPrType.SelectedValue), ExpenseType, procedure, purchaseType, int.Parse(ddlWarehouse.SelectedValue), int.Parse(ddlMainCateGory.SelectedValue));
                    List<TempMRN_BOM> temBOM = new List<TempMRN_BOM>();
                    List<TempMRN_FileUpload> tempMrFileUpload = new List<TempMRN_FileUpload>();
                    List<TempMRN_FileUploadReplacement> tempReplacementFileUpload = new List<TempMRN_FileUploadReplacement>();
                    List<TempMRN_SupportiveDocument> tempMR_SupportiveDocument = new List<TempMRN_SupportiveDocument>();
                    if (meterialRequestId > 0)
                    {
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
                        mrexpenseController.SaveMRExpense(meterialRequestId, Is_Budget, Convert.ToDecimal(txtBudgetAmount.Text), txtBudgetRemark.Text, txtBudgetInformation.Text, isApproved);
                    }
                    if (meterialRequestId > 0)
                    {
                        int count = 0;
                        foreach (var item in listToBind)
                        {

                            int mrDetailId = mrnDetailController.SaveMRNDetails(meterialRequestId, item.ItemId, item.MeasurementId, item.ItemDescription, "", LocalTime.Now, 1, item.ReplacementId, item.ItemQuantity, item.Remarks, item.EstimatedAmount, item.FileSampleProvided, item.Remarks, item.MeasurementId);
                            string lastPurchaseDate = (txtLastPurchaseDate.Text == "") ? null : txtLastPurchaseDate.Text;


                            // prDStockInfoController.savePRDStockInfo(mrDetailId, item.StockBalance, item.LastPurchasePrice, item.SupplierId, item.LastPurchaseDate, item.AvgConsumption);
                            temBOM = tempMRN_BOMController.GetListById(CompanyId, item.ItemId);

                            foreach (var itemBom in temBOM)
                            {
                                mrnBomController.SaveBillOfMeterial(itemBom.MrnId, itemBom.ItemId, itemBom.Meterial, itemBom.Description, 1, LocalTime.Now, itemBom.DepartmentId.ToString(), LocalTime.Now, UserId);
                                tempMRN_BOMController.DeleteTempData(itemBom.DepartmentId, itemBom.ItemId);
                            }

                            tempMrFileUpload = tempMRN_FileUploadController.GetTempMrnFiles(item.ItemId, meterialRequestId);
                            tempReplacementFileUpload = tempMRN_FileUploadReplacementController.GetTempMrnFiles(item.ItemId, meterialRequestId);
                            tempMR_SupportiveDocument = tempMRN_SupportiveDocumentController.GetTempMrnSupportiveFiles(item.ItemId, meterialRequestId);
                            //-------------Copy TempFolder To Original Folder
                            var source = Server.MapPath("TempPurchaseRequestFiles");
                            var target = Server.MapPath("PurchaseRequestFiles");
                            var sourceFolder = System.IO.Path.Combine(source, meterialRequestId.ToString());
                            var targetFolder = System.IO.Path.Combine(target, meterialRequestId.ToString());
                            CopyFolderContents(sourceFolder, targetFolder);

                            int saveFileUpload = 0;
                            foreach (var itemFileUpload in tempMrFileUpload)
                            {
                                saveFileUpload = mrnFileUploadController.SaveFileUpload(itemFileUpload.DepartmnetId, itemFileUpload.ItemId, itemFileUpload.MrnId, itemFileUpload.FilePath.Replace("TempPurchaseRequestFiles", "PurchaseRequestFiles"), itemFileUpload.FileName);
                                tempMRN_FileUploadController.DeleteTempDataFileUpload(itemFileUpload.DepartmnetId, itemFileUpload.ItemId);
                            }

                            // replace replacement images from temp folder to original folder
                            var sourceReplacement = Server.MapPath("TempPrReplacementFiles");
                            var targetReplacement = Server.MapPath("PrReplacementFiles");
                            var sourceFolderReplacement = System.IO.Path.Combine(sourceReplacement, meterialRequestId.ToString());
                            var targetFolderReplacement = System.IO.Path.Combine(targetReplacement, meterialRequestId.ToString());
                            CopyFolderContents(sourceFolderReplacement, targetFolderReplacement);

                            foreach (var itemReplacementFileUpload in tempReplacementFileUpload)
                            {

                                //create to mrn

                                int saveReplacementFileUpload = mrn_Replace_FileUpload_Controller.SaveFileUpload(itemReplacementFileUpload.DepartmnetId, itemReplacementFileUpload.ItemId, itemReplacementFileUpload.MrnId, itemReplacementFileUpload.FilePath.Replace("TempPrReplacementFiles", "PrReplacementFiles"), itemReplacementFileUpload.FileName);
                                tempMRN_FileUploadReplacementController.DeleteTempDataFileUpload(itemReplacementFileUpload.DepartmnetId, itemReplacementFileUpload.ItemId);
                            }
                            //  supporive Documents from temp folder to original folder
                            var sourceSupporiveDocuments = Server.MapPath("TempPrSupportiveFiles");
                            var targetSupporiveDocuments = Server.MapPath("PrSupportiveFiles");
                            var sourceFolderSupporiveDocuments = System.IO.Path.Combine(sourceSupporiveDocuments, meterialRequestId.ToString());
                            var targetFolderSupporiveDocuments = System.IO.Path.Combine(targetSupporiveDocuments, meterialRequestId.ToString());
                            CopyFolderContents(sourceFolderSupporiveDocuments, targetFolderSupporiveDocuments);

                            foreach (var itemSupportiveDocument in tempMR_SupportiveDocument)
                            {
                                int saveSupportiveDocumentFileUpload = mrsupportiveDocumentController.SaveSupporiveFileUpload(itemSupportiveDocument.DepartmnetId, itemSupportiveDocument.ItemId, itemSupportiveDocument.MrnId, itemSupportiveDocument.FilePath.Replace("TempPrSupportiveFiles", "PrSupportiveFiles"), itemSupportiveDocument.FileName);
                                tempMRN_SupportiveDocumentController.DeleteTempSupporiveFileUpload(itemSupportiveDocument.DepartmnetId, itemSupportiveDocument.ItemId);
                            }



                            count++;
                        }
                        if (listToBind.Count() == count)
                        {
                            // DisplayMessage("Material Request has been created successfully", false);
                            ClearFields();
                            Session["FinalListPRCreate"] = null;
                            int mrCode = MRNmasterController.FetchMRCode(CompanyId);
                            txtPrNumber.Text = mrCode.ToString();
                            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#SuccessAlert').modal('show'); });   </script>", false);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>  alert('Hi');   </script>", false);
                            Session["CompanyId"] = CompanyId;
                            Session["UserId"] = UserId;

                        }
                    }
                    lblAlertMsg.Text = "";
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('errorMessage').innerHTML = \"Please add atleast one item\"; $('#errorAlert').modal('show'); });   </script>", false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;

        }

        [WebMethod]
        public static List<string> LoadItemNames(string input)
        {
            CompanyPurchaseRequestNote cp = new CompanyPurchaseRequestNote();
            int mainCategory = int.Parse(cp.Session["MainCategoryId"].ToString());
            int subCategory = int.Parse(cp.Session["SubCategoryId"].ToString());
            AddItemController addItemController = ControllerFactory.CreateAddItemController();
            List<AddItem> additems = addItemController.SearchedItemName(mainCategory, subCategory, CompanyId, input);

            List<string> itemNameclz = new List<string>();

            foreach (var item in additems)
            {
                itemNameclz.Add(item.ItemName);
            }

            input = input.Replace(" ", string.Empty);

            return (itemNameclz.FindAll(item => item.ToLower().Replace(" ", string.Empty).Contains(input.ToLower())));
        }

        //----------------Dynamically Load To grid view PR Detail Data
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (btnAdd.Text == "Add Item")
                {
                    int count = gvDatataTable.Rows.Cast<GridViewRow>()
     .Where(row => row.Cells[4].Text == ddlItemName.SelectedValue && row.Cells[2].Text == ddlSubCategory.SelectedValue).Count();

                    if (count == 0)
                    {
                        lblAlertMsg.Text = "";
                        if (txtQty.Text != "" && ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && txtEstimatedAmount.Text != "")
                        {

                            string folderFilePath = string.Empty;
                            //if (fileUpload1.HasFile)
                            //{
                            int MrnId = tempMRN_BOMController.GetNextMrnIdObj(CompanyId);
                            string NewDirectory = Server.MapPath("TempPurchaseRequestFiles/" + MrnId);
                            int returnType = CreateDirectoryIfNotExists(NewDirectory);

                            string NewDirectoryForReplacement = Server.MapPath("TempPrReplacementFiles/" + MrnId);
                            int returnTypeR = CreateDirectoryIfNotExists(NewDirectoryForReplacement);

                            string NewDirectoryForSupportiveDocument = Server.MapPath("TempPrSupportiveFiles/" + MrnId);
                            int returnTypeS = CreateDirectoryIfNotExists(NewDirectoryForSupportiveDocument);
                            //if(returnType == 1)
                            //{
                            string contentType = "image/png";
                            string fileName = string.Empty;

                            tempMRN_BOMController.DeleteBOMByMrnId(MrnId, CompanyId, int.Parse(ddlItemName.SelectedValue));

                            DataTable dt = (DataTable)ViewState["Specification"];

                            foreach (DataRow row in dt.Rows)
                            {
                                tempMRN_BOMController.SaveTempBOM(CompanyId, MrnId, int.Parse(ddlItemName.SelectedValue), row["Meterial"].ToString(), row["Description"].ToString());

                            }

                            if (gvPrUploadedFiles.Rows.Count > 0)
                            {
                                tempMRN_FileUploadController.DeleteTempMrnDetailFileUpload(MrnId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                                for (int t = 0; t < gvPrUploadedFiles.Rows.Count; t++)
                                {
                                    Image image = (Image)gvPrUploadedFiles.Rows[t].FindControl("imgPicture");
                                    int saveFilePath = tempMRN_FileUploadController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId,
                                        image.ImageUrl, gvPrUploadedFiles.Rows[t].Cells[3].Text);
                                }
                            }

                            if (gvSupporiveFiles.Rows.Count > 0)
                            {
                                tempMRN_SupportiveDocumentController.DeleteTempMrnDetailSupporiveUpload(MrnId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                                for (int t = 0; t < gvSupporiveFiles.Rows.Count; t++)
                                {
                                    int saveFilePath = tempMRN_SupportiveDocumentController.SaveTempSupportiveUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId,
                                        gvSupporiveFiles.Rows[t].Cells[2].Text, gvSupporiveFiles.Rows[t].Cells[3].Text);
                                }
                            }

                            if (gvRepacementImages.Rows.Count > 0)
                            {
                                tempMRN_FileUploadReplacementController.DeleteTempMrnDetailFileUpload(MrnId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                                for (int t = 0; t < gvRepacementImages.Rows.Count; t++)
                                {
                                    Image image = (Image)gvRepacementImages.Rows[t].FindControl("imgPicture");
                                    int saveFilePath = tempMRN_FileUploadReplacementController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue),
                                        MrnId, image.ImageUrl, gvRepacementImages.Rows[t].Cells[3].Text);
                                }

                            }
                            HttpFileCollection uploads = HttpContext.Current.Request.Files;
                            for (int i = 0; i < uploads.Count; i++)
                            {
                                if (uploads.AllKeys[i] == "files")
                                {
                                    //HttpPostedFile postedFile = uploads[i];

                                    //string CreateFileName = MrnId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                    //string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                    //string FileName = Path.GetFileName(postedFile.FileName);
                                    //string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                    //if (postedFile.ContentLength > 0)
                                    //{
                                    //    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + MrnId + "/" + filename01)))
                                    //    {
                                    //        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + MrnId + "/" + filename01));
                                    //    }

                                    //    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + MrnId + "/" + filename01));
                                    //    folderFilePath = "~/TempPurchaseRequestFiles/" + MrnId + "/" + filename01;
                                    //    int saveFilePath = tempMRN_FileUploadController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId, folderFilePath, FileName);
                                    //}
                                }
                                if (uploads.AllKeys[i] == "fileReplace[]")
                                {
                                    //HttpPostedFile postedFile = uploads[i];

                                    //string CreateFileName = MrnId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                    //string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                    //string FileName = Path.GetFileName(postedFile.FileName);
                                    //string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                    //if (postedFile.ContentLength > 0)
                                    //{
                                    //    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + MrnId + "/" + filename01)))
                                    //    {
                                    //        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + MrnId + "/" + filename01));
                                    //    }

                                    //    //if (_tempPR_FileUploadReplacement.Count == 0)
                                    //    //{
                                    //    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + MrnId + "/" + filename01));
                                    //    folderFilePath = "~/TempPrReplacementFiles/" + MrnId + "/" + filename01;
                                    //    int saveFilePath = tempMRN_FileUploadReplacementController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId, folderFilePath, FileName);
                                    //    //}
                                    //}
                                }


                                if (uploads.AllKeys[i] == "supportivefiles[]")
                                {
                                    //HttpPostedFile postedFile = uploads[i];

                                    //string CreateFileName = MrnId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                    //string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                    //string FileName = Path.GetFileName(postedFile.FileName);
                                    //string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                    //if (postedFile.ContentLength > 0)
                                    //{
                                    //    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + MrnId + "/" + filename01)))
                                    //    {
                                    //        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + MrnId + "/" + filename01));
                                    //    }


                                    //    //if (_tempPrFileUpload.Count == 0)
                                    //    //{
                                    //    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + MrnId + "/" + filename01));
                                    //    folderFilePath = "~/TempPrSupportiveFiles/" + MrnId + "/" + filename01;
                                    //    int saveFilePath = tempMRN_SupportiveDocumentController.SaveTempSupportiveUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId, folderFilePath, FileName);
                                    //}
                                    //}
                                }

                            }
                            lblAlertMsg.Text = "";
                            // }
                            //else
                            // {
                            //    lblAlertMsg.Text = "This item is already existing in the current PR.";
                            // }

                            int File_Sample_Provided = 0;
                            if (rdoFileSampleEnable.Checked)
                            {
                                File_Sample_Provided = 1;
                            }

                            List<TempDataSet> listData = new List<TempDataSet>();
                            TempDataSet dataSet = new TempDataSet();
                            dataSet.MainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                            dataSet.MainCategoryName = ddlMainCateGory.SelectedItem.Text;
                            dataSet.SubCategoryId = int.Parse(ddlSubCategory.SelectedValue);
                            dataSet.SubcategoryName = ddlSubCategory.SelectedItem.Text;
                            dataSet.ItemId = int.Parse(ddlItemName.SelectedValue);
                            dataSet.ItemName = ddlItemName.SelectedItem.Text;
                            dataSet.ItemQuantity = decimal.Parse(txtQty.Text);
                            dataSet.ItemDescription = txtDescription.Text;
                            dataSet.Purpose = txtPurpose.Text;  // remark
                            dataSet.EstimatedAmount = decimal.Parse(txtEstimatedAmount.Text == "" ? "0" : txtEstimatedAmount.Text);
                            dataSet.MeasurementId = int.Parse(ddlMeasurement.SelectedValue);
                            dataSet.FileSampleProvided = File_Sample_Provided;
                            dataSet.Remarks = txtPurpose.Text;   // remark
                            dataSet.StockBalance = int.Parse(txtStockBalance.Text);
                            dataSet.AvgConsumption = decimal.Parse(txtAvgConsumption.Text);
                            dataSet.LastPurchasePrice = decimal.Parse(hdnLastPurchasePrice.Value);
                            dataSet.LastPurchaseDate = DateTime.Parse(hdnLastPurchaseDate.Value);
                            dataSet.SupplierId = int.Parse(hndSupplierId.Value);


                            if (rdoEnable.Checked)
                            {
                                dataSet.ReplacementName = ReplacementRdo.Yes.ToString();
                                if (ReplacementRdo.Yes.ToString() == "Yes")
                                    dataSet.ReplacementId = 1;
                            }
                            if (rdoDisable.Checked)
                            {
                                dataSet.ReplacementName = ReplacementRdo.No.ToString();
                                if (ReplacementRdo.No.ToString() == "No")
                                    dataSet.ReplacementId = 0;
                            }
                            if (listToBind.FindAll(i => i.ItemId == dataSet.ItemId).Count == 0)
                            {
                                listToBind.Add(dataSet);
                                ddlMainCateGory.SelectedValue = dataSet.MainCategoryId.ToString();
                                ddlMainCateGory.Enabled = false;
                                lblAlertMsg.Text = "Item details Added successfully";

                                lblAlertMsg.Attributes.CssStyle.Add("color", "Green");
                                cleardatatable();
                            }
                            else
                            {
                                lblAlertMsg.Text = "This item is already existing.";

                                lblAlertMsg.Attributes.CssStyle.Add("color", "Red");
                                cleardatatable();
                            }


                            Session["FinalListPRCreate"] = listToBind;

                            gvDatataTable.DataSource = ((List<TempDataSet>)Session["FinalListPRCreate"]).ToList();
                            gvDatataTable.DataBind();

                            ddlMainCateGory.SelectedValue = dataSet.MainCategoryId.ToString();
                            ddlMainCateGory.Enabled = false;

                            lblcount.Text = "0";
                            txtPurpose.Text = "";
                            txtDescription.Text = "";
                            txtEstimatedAmount.Text = "";
                            rdoDisable.Checked = true;
                            txtQty.Text = "";
                            ddlMeasurement.SelectedIndex = 0;
                            LoadDDLMainCatregory();
                            ddlSubCategory.SelectedIndex = 0;
                            ddlItemName.SelectedIndex = 0;
                            txtStockBalance.Text = "0";
                            txtAvgConsumption.Text = "0";
                            txtLastPurchasePrice.Text = "";
                            txtLastPurchaseDate.Text = "";
                            hndSupplierId.Value = "";

                            ddlMainCateGory.Enabled = false;
                        }

                        ddlSubCategory.Enabled = true;
                        ddlItemName.Enabled = true;


                        for (int i = 0; i < listToBind.Count; i++)
                        {
                            estimatedCost += (double)(listToBind[i].ItemQuantity * listToBind[i].EstimatedAmount);
                        }

                        txtEstimatedCost.Text = estimatedCost.ToString("N2");

                        clearFields();
                    }
                    else
                    {

                        lblAlertMsg.Text = "This item is already existing.";
                        clear();
                        lblAlertMsg.Attributes.CssStyle.Add("color", "Red");
                        ddlMainCateGory.Enabled = false;
                    }
                }

                if (btnAdd.Text == "Update Item")
                {
                    lblAlertMsg.Text = "";
                    string folderFilePath = string.Empty;
                    //if (fileUpload1.HasFile)
                    //{
                    int MrnId = tempMRN_BOMController.GetNextMrnIdObj(CompanyId);
                    string NewDirectory = Server.MapPath("TempPurchaseRequestFiles/" + MrnId);
                    int returnType = CreateDirectoryIfNotExists(NewDirectory);

                    string NewDirectoryForReplacement = Server.MapPath("TempPrReplacementFiles/" + MrnId);
                    int returnTypeR = CreateDirectoryIfNotExists(NewDirectoryForReplacement);

                    string NewDirectoryForSupportiveDocument = Server.MapPath("TempPrSupportiveFiles/" + MrnId);
                    int returnTypeS = CreateDirectoryIfNotExists(NewDirectoryForSupportiveDocument);
                    //if(returnType == 1)
                    //{
                    string contentType = "image/png";
                    string fileName = string.Empty;

                    tempMRN_BOMController.DeleteBOMByMrnId(MrnId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                    DataTable dt = (DataTable)ViewState["Specification"];

                    foreach (DataRow row in dt.Rows)
                    {
                        tempMRN_BOMController.SaveTempBOM(CompanyId, MrnId, int.Parse(ddlItemName.SelectedValue), row["Meterial"].ToString(), row["Description"].ToString());

                    }
                    listToBind.RemoveAll(x => x.MainCategoryId == int.Parse(ddlMainCateGory.SelectedValue) && x.SubCategoryId == int.Parse(ddlSubCategory.SelectedValue) && x.ItemId == int.Parse(ddlItemName.SelectedValue));

                    if (gvPrUploadedFiles.Rows.Count > 0)
                    {
                        tempMRN_FileUploadController.DeleteTempMrnDetailFileUpload(MrnId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                        for (int t = 0; t < gvPrUploadedFiles.Rows.Count; t++)
                        {
                            Image image = (Image)gvPrUploadedFiles.Rows[t].FindControl("imgPicture");
                            int saveFilePath = tempMRN_FileUploadController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId,
                                image.ImageUrl, gvPrUploadedFiles.Rows[t].Cells[3].Text);
                        }
                    }

                    if (gvSupporiveFiles.Rows.Count > 0)
                    {
                        tempMRN_SupportiveDocumentController.DeleteTempMrnDetailSupporiveUpload(MrnId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                        for (int t = 0; t < gvSupporiveFiles.Rows.Count; t++)
                        {
                            int saveFilePath = tempMRN_SupportiveDocumentController.SaveTempSupportiveUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId,
                                gvSupporiveFiles.Rows[t].Cells[2].Text, gvSupporiveFiles.Rows[t].Cells[3].Text);
                        }
                    }

                    if (gvRepacementImages.Rows.Count > 0)
                    {
                        tempMRN_FileUploadReplacementController.DeleteTempMrnDetailFileUpload(MrnId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                        for (int t = 0; t < gvRepacementImages.Rows.Count; t++)
                        {
                            Image image = (Image)gvRepacementImages.Rows[t].FindControl("imgPicture");
                            int saveFilePath = tempMRN_FileUploadReplacementController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue),
                                MrnId, image.ImageUrl, gvRepacementImages.Rows[t].Cells[3].Text);
                        }

                    }

                    if (listToBind.FindAll(i => i.ItemId == int.Parse(ddlItemName.SelectedValue)).Count == 0)
                    {
                        HttpFileCollection uploads = HttpContext.Current.Request.Files;
                        for (int i = 0; i < uploads.Count; i++)
                        {
                            if (uploads.AllKeys[i] == "files[]")
                            {
                                //HttpPostedFile postedFile = uploads[i];
                                //List<TempMRN_FileUpload> _TempMRN_FileUpload = new List<TempMRN_FileUpload>();

                                //_TempMRN_FileUpload = tempMRN_FileUploadController.GetPrUpoadFilesListByMRNIdItemId(CompanyId, MrnId, itemId);

                                //int maxNumber = 0;

                                //foreach (var item in _TempMRN_FileUpload)
                                //{
                                //    int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                //    if (CalNumber > maxNumber)
                                //        maxNumber = CalNumber;
                                //}

                                //string CreateFileName = MrnId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                //string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                //string FileName = Path.GetFileName(postedFile.FileName);
                                //string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                //if (postedFile.ContentLength > 0)
                                //{
                                //    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + MrnId + "/" + filename01)))
                                //    {
                                //        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + MrnId + "/" + filename01));
                                //    }

                                //    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + MrnId + "/" + filename01));
                                //    folderFilePath = "~/TempPurchaseRequestFiles/" + MrnId + "/" + filename01;
                                //    int saveFilePath = tempMRN_FileUploadController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId, folderFilePath, FileName);
                                //}
                            }
                            if (uploads.AllKeys[i] == "fileReplace[]")
                            {
                                //HttpPostedFile postedFile = uploads[i];

                                //List<TempMRN_FileUploadReplacement> _TempPR_FileUploadReplacement = new List<TempMRN_FileUploadReplacement>();

                                //_TempPR_FileUploadReplacement = tempMRN_FileUploadReplacementController.GetMRNUpoadFilesListByMRNIdItemId(CompanyId, MrnId, itemId);
                                //int maxNumber = 0;

                                //foreach (var item in _TempPR_FileUploadReplacement)
                                //{
                                //    int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                //    if (CalNumber > maxNumber)
                                //        maxNumber = CalNumber;
                                //}

                                //string CreateFileName = MrnId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (maxNumber + 1).ToString();
                                //string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                //string FileName = Path.GetFileName(postedFile.FileName);
                                //string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                //if (postedFile.ContentLength > 0)
                                //{
                                //    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + MrnId + "/" + filename01)))
                                //    {
                                //        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + MrnId + "/" + filename01));
                                //    }

                                //    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + MrnId + "/" + filename01));
                                //    folderFilePath = "~/TempPrReplacementFiles/" + MrnId + "/" + filename01;
                                //    int saveFilePath = tempMRN_FileUploadReplacementController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId, folderFilePath, FileName);
                                //}
                            }

                            if (uploads.AllKeys[i] == "supportivefiles[]")
                            {
                                //HttpPostedFile postedFile = uploads[i];
                                //List<TempMRN_SupportiveDocument> tempMRN_SupportiveDocument = new List<TempMRN_SupportiveDocument>();

                                //tempMRN_SupportiveDocument = tempMRN_SupportiveDocumentController.GetMRNSupporiveUpoadFilesListByMRNIdItemId(CompanyId, MrnId, itemId);

                                //int maxNumber = 0;

                                //foreach (var item in tempMRN_SupportiveDocument)
                                //{
                                //    int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                //    if (CalNumber > maxNumber)
                                //        maxNumber = CalNumber;
                                //}

                                //string CreateFileName = MrnId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                //string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                //string FileName = Path.GetFileName(postedFile.FileName);
                                //string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                //if (postedFile.ContentLength > 0)
                                //{
                                //    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + MrnId + "/" + filename01)))
                                //    {
                                //        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + MrnId + "/" + filename01));
                                //    }

                                //    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + MrnId + "/" + filename01));
                                //    folderFilePath = "~/TempPrSupportiveFiles/" + MrnId + "/" + filename01;
                                //    int saveFilePath = tempMRN_SupportiveDocumentController.SaveTempSupportiveUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), MrnId, folderFilePath, FileName);
                                //}
                            }
                        }
                        lblAlertMsg.Text = "";
                    }
                    else
                    {
                        lblAlertMsg.Text = "This item is already existing in the current MRN.";
                    }

                    List<TempDataSet> listData = new List<TempDataSet>();
                    TempDataSet dataSet = new TempDataSet();
                    dataSet.MainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    dataSet.MainCategoryName = ddlMainCateGory.SelectedItem.Text;
                    dataSet.SubCategoryId = int.Parse(ddlSubCategory.SelectedValue);
                    dataSet.SubcategoryName = ddlSubCategory.SelectedItem.Text;
                    dataSet.ItemId = int.Parse(ddlItemName.SelectedValue);
                    dataSet.ItemName = ddlItemName.SelectedItem.Text;
                    dataSet.ItemQuantity = decimal.Parse(txtQty.Text);
                    dataSet.ItemDescription = txtDescription.Text;
                    dataSet.Remarks = txtPurpose.Text;
                    dataSet.Purpose = txtPurpose.Text;
                    dataSet.EstimatedAmount = decimal.Parse(txtEstimatedAmount.Text == "" ? "0" : txtEstimatedAmount.Text);
                    dataSet.MeasurementId = int.Parse(ddlMeasurement.SelectedValue);
                    dataSet.StockBalance = int.Parse(txtStockBalance.Text);
                    dataSet.AvgConsumption = decimal.Parse(txtAvgConsumption.Text);
                    dataSet.LastPurchasePrice = decimal.Parse(hdnLastPurchasePrice.Value);
                    dataSet.LastPurchaseDate = DateTime.Parse(hdnLastPurchaseDate.Value);
                    dataSet.SupplierId = int.Parse(hndSupplierId.Value);

                    if (rdoEnable.Checked)
                    {
                        dataSet.ReplacementName = ReplacementRdo.Yes.ToString();
                        if (ReplacementRdo.Yes.ToString() == "Yes")
                            dataSet.ReplacementId = 1;
                    }
                    if (rdoDisable.Checked)
                    {
                        dataSet.ReplacementName = ReplacementRdo.No.ToString();
                        if (ReplacementRdo.No.ToString() == "No")
                            dataSet.ReplacementId = 0;
                    }
                    if (listToBind.FindAll(i => i.ItemId == dataSet.ItemId).Count == 0)
                    {
                        listToBind.Add(dataSet);
                        ddlMainCateGory.SelectedValue = dataSet.MainCategoryId.ToString();
                        ddlMainCateGory.Enabled = false;
                        lblAlertMsg.Text = "Item details updated successfully";
                        lblAlertMsg.Attributes.CssStyle.Add("color", "Green");
                        cleardatatable();
                    }
                    else
                    {
                        // lblAlertMsg.Text = "This item is already existing in the current PR.";
                    }

                    Session["FinalListPRCreate"] = listToBind;

                    gvDatataTable.DataSource = ((List<TempDataSet>)Session["FinalListPRCreate"]).ToList();
                    gvDatataTable.DataBind();

                    clear();

                    for (int i = 0; i < listToBind.Count; i++)
                    {
                        estimatedCost += (double)(listToBind[i].ItemQuantity * listToBind[i].EstimatedAmount);
                    }

                    txtEstimatedCost.Text = estimatedCost.ToString("N2");
                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void clear()
        {
            lblcount.Text = "0";
            txtPurpose.Text = "";
            txtDescription.Text = "";
            rdoDisable.Checked = true;
            txtQty.Text = "";
            txtEstimatedAmount.Text = "";
            //LoadDDLMainCatregory();
            ddlMainCateGory.Enabled = false;
            ddlSubCategory.SelectedIndex = 0;
            ddlItemName.SelectedIndex = 0;
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
            btnAdd.Text = "Add Item";
            gvPrUploadedFiles.DataSource = null;
            gvPrUploadedFiles.DataBind();
            gvRepacementImages.DataSource = null;
            gvRepacementImages.DataBind();
            gvSupporiveFiles.DataSource = null;
            gvSupporiveFiles.DataBind();
            //ddlSubCategory.DataSource = null;
            //ddlSubCategory.DataBind();
            txtStockBalance.Text = "0";
            txtAvgConsumption.Text = "0";
            txtLastPurchasePrice.Text = "";
            txtLastPurchaseDate.Text = "";
            hndSupplierId.Value = "";

            ddlItemName.DataSource = null;
            ddlItemName.DataBind();
            ddlMeasurement.SelectedIndex = 0;
        }

        //-----------OriginalDirectory (Not in use)
        public void TargetFileCreate()
        {
            var source = Server.MapPath("TempPurchaseRequestFiles");
            var target = Server.MapPath("PurchaseRequestFiles");
            foreach (var dir in Directory.GetDirectories(source))
            {
                var targetFolder = System.IO.Path.Combine(target, "2");
                if (!Directory.Exists(targetFolder))
                {
                    Directory.CreateDirectory(targetFolder);
                }
            }
        }

        //-----------OriginalDirectory
        private bool CopyFolderContents(string SourcePath, string DestinationPath)
        {
            SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
            DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";
            try
            {
                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                    {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    foreach (string files in Directory.GetFiles(SourcePath))
                    {
                        FileInfo fileInfo = new FileInfo(files);
                        fileInfo.CopyTo(string.Format(@"{0}\{1}", DestinationPath, fileInfo.Name), true);
                    }

                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(drs);
                        if (CopyFolderContents(drs, DestinationPath + directoryInfo.Name) == false)
                        {
                            return false;
                        }
                    }
                    //---Delete From Temporary Files
                    System.IO.Directory.Delete(SourcePath, true);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //------------------Check Create Directory Existing or Not 
        private int CreateDirectoryIfNotExists(string NewDirectory)
        {
            try
            {
                int returnType = 0;
                // Checking the existance of directory
                if (!Directory.Exists(NewDirectory))
                {
                    //delete
                    //If No any such directory then creates the new one
                    Directory.CreateDirectory(NewDirectory);
                    returnType = 1;
                }
                else
                {
                    //Label1.Text = "Directory Exist";
                    returnType = 0;
                }
                return returnType;
            }
            catch (IOException _err)
            {
                throw _err;
                //Label1.Text = _err.Message; ;
            }
        }

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
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

                lblAlertMsg.Text = "";
                btnAdd.Text = "Add Item";
                txtPurpose.Text = "";
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

                ddlItemName.Text = "";
                //ddlItemName.DataBind();

                ddlMainCateGory.Enabled = false;
                ddlSubCategory.Enabled = true;
                ddlItemName.Enabled = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearFields()
        {
            lblcount.Text = "0";
            txtPurpose.Text = "";
            txtDescription.Text = "";
            rdoDisable.Checked = true;
            txtQty.Text = "";
            txtEstimatedAmount.Text = "";
            LoadDDLMainCatregory();
            ddlMainCateGory.SelectedIndex = 0;
            ddlSubCategory.Items.Clear();
            ddlItemName.Items.Clear();
            ddlItemName.Enabled = true;
            ddlSubCategory.Enabled = true;
            ddlMainCateGory.Enabled = true;
            btnAdd.Text = "Add Item";
            gvPrUploadedFiles.DataSource = null;
            gvPrUploadedFiles.DataBind();
            gvRepacementImages.DataSource = null;
            gvRepacementImages.DataBind();
            gvSupporiveFiles.DataSource = null;
            gvSupporiveFiles.DataBind();
            //ddlSubCategory.DataSource = null;
            //ddlSubCategory.DataBind();

            ddlItemName.DataSource = null;
            ddlItemName.DataBind();
            if (ddlMeasurement.DataSource != null)
            {
                ddlMeasurement.SelectedIndex = 0;
            }
            txtMrnDescription.Text = "";

            txtQuotationFor.Text = "";
            txtRef.Text = "";
            txtRequestedBy.Text = "";
            txtEstimatedAmount.Text = "";
            txtRequiredDate.Text = "";

            //   ddlExpenseType.SelectedIndex = 0;
            estimatedCost = 0.00;
            txtEstimatedCost.Text = "";
            rdoBudgetEnable.Checked = true;
            txtBudgetAmount.Text = "";
            txtBudgetInformation.Text = "";

            gvDatataTable.DataSource = null;
            gvDatataTable.DataBind();

            if (ddlPrType.DataSource != null)
            {
                ddlPrType.SelectedIndex = 0;
            }

        }

        protected void lbtnViewBOM_Click(object sender, EventArgs e)
        {
            try
            {
                int MrnId = tempMRN_BOMController.GetNextMrnIdObj(CompanyId);
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);
                List<TempMRN_BOM> tempBOMList = tempMRN_BOMController.GetBOMListByMrnIdItemId(CompanyId, MrnId, itemId);
                gvTempBoms.DataSource = tempBOMList;
                gvTempBoms.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalViewBom').modal('show'); });   </script>", false);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#myModalViewBom').modal('show');   </script>", false);
                Session["CompanyId"] = CompanyId;
                Session["UserId"] = UserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnexisting_Click(object sender, EventArgs e)
        {
            if (ddlItemName.SelectedValue != "")
            {
                int itemID = int.Parse(ddlItemName.SelectedValue);
                if (itemID != 0)
                {
                    List<TempMRN_BOM> tempBOMList = tempMRN_BOMController.GetItemspecification(itemID);
                    gvSpecificationBoms.DataSource = tempBOMList;
                    gvSpecificationBoms.DataBind();
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#specification').modal('show');   </script>", false);
                }
                else
                {
                    DisplayMessage("Please select Item", true);
                }
            }

        }

        protected void lbtnViewUploadPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int MrnId = tempMRN_BOMController.GetNextMrnIdObj(CompanyId);
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);

                List<TempMRN_FileUpload> GetTempMrnFiles = tempMRN_FileUploadController.GetTempMrnFiles(itemId, MrnId);
                gvUploadedPhotos.DataSource = GetTempMrnFiles;
                gvUploadedPhotos.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#myModalUploadedPhotos').modal('show');   </script>", false);

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);
                Session["CompanyId"] = CompanyId;
                Session["UserId"] = UserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnViewzReplacementPhotos_Click(object sender, EventArgs e)
        {
            try
            {
                int MrnId = tempMRN_BOMController.GetNextMrnIdObj(CompanyId);
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);

                List<TempMRN_FileUploadReplacement> GetTempReplacementFiles = tempMRN_FileUploadReplacementController.GetMRNUpoadFilesListByMRNIdItemId(CompanyId, MrnId, itemId);
                gvReplacementPhotos.DataSource = GetTempReplacementFiles;
                gvReplacementPhotos.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalReplacementPhotos').modal('show'); });   </script>", false);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>   $('#myModalReplacementPhotos').modal('show');   </script>", false);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#myModalReplacementPhotos').modal('show');   </script>", false);
                Session["CompanyId"] = CompanyId;
                Session["UserId"] = UserId;

            }
            catch (Exception)
            {


            }
        }

        //---------------Edit Dynamic Table Data
        protected void btnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                int ItemId = int.Parse(hdnItemId.Value);
                if (ItemId != 0 && ItemId != null)
                {

                    int MrnId = tempMRN_BOMController.GetNextMrnIdObj(CompanyId);

                    int deleteBom = tempMRN_BOMController.DeleteBOMByMrnId(MrnId, CompanyId, ItemId);


                    var SourcePath = Server.MapPath("TempPrReplacementFiles/" + MrnId);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    List<TempMRN_FileUploadReplacement> _TempMRN_FileUploadReplacement = tempMRN_FileUploadReplacementController.GetMRNUpoadFilesListByMRNIdItemId(CompanyId, MrnId, ItemId).ToList();

                    foreach (var item in _TempMRN_FileUploadReplacement)
                    {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in di.GetFiles())
                        {
                            if (file.Name == getNAme[3])
                            {
                                File.Delete(Server.MapPath("TempPrReplacementFiles/" + MrnId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempReplacementImage = tempMRN_FileUploadReplacementController.DeleteTempMrnDetailFileUpload(MrnId, CompanyId, ItemId);

                    var SourcePathFileUpload = Server.MapPath("TempPurchaseRequestFiles/" + MrnId);
                    System.IO.DirectoryInfo di1 = new DirectoryInfo(SourcePath);

                    List<TempMRN_FileUpload> TempMrnFileUpload = tempMRN_FileUploadController.GetPrUpoadFilesListByMRNIdItemId(CompanyId, MrnId, ItemId).ToList();

                    foreach (var item in _TempMRN_FileUploadReplacement)
                    {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in di1.GetFiles())
                        {
                            if (file.Name == getNAme[3])
                            {
                                File.Delete(Server.MapPath("TempPurchaseRequestFiles/" + MrnId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempFileUpload = tempMRN_FileUploadController.DeleteTempMrnDetailFileUpload(MrnId, CompanyId, ItemId);

                    var SourcePathSupportive = Server.MapPath("TempPrSupportiveFiles/" + MrnId);
                    System.IO.DirectoryInfo dis = new DirectoryInfo(SourcePath);

                    List<TempMRN_SupportiveDocument> _TempMRN_SupportiveDocument = tempMRN_SupportiveDocumentController.GetMRNSupporiveUpoadFilesListByMRNIdItemId(CompanyId, MrnId, ItemId).ToList();

                    foreach (var item in _TempMRN_SupportiveDocument)
                    {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in dis.GetFiles())
                        {
                            if (file.Name == getNAme[3])
                            {
                                File.Delete(Server.MapPath("TempPrSupportiveFiles/" + MrnId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempSupportiveDocument = tempMRN_SupportiveDocumentController.DeleteTempMrnDetailSupporiveUpload(MrnId, CompanyId, ItemId);





                    listToBind.RemoveAll(i => (i.ItemId == ItemId));
                    Session["FinalListPRCreate"] = listToBind;

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //DisplayMessage("Item details have been deleted successfully", false);

                    gvDatataTable.DataSource = ((List<TempDataSet>)Session["FinalListPRCreate"]).ToList().OrderBy(i => i.ItemId);
                    gvDatataTable.DataBind();
                }

                else
                {
                    DisplayMessage("Please select Item to Delete!!", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //---------------Edit Dynamic Table Data
        protected void btnEditItem_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BomStringList = new List<string>();
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                mrnid = tempMRN_BOMController.GetNextMrnIdObj(CompanyId);
                itemId = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);

                btnAdd.Text = "Update Item";
                List<TempDataSet> tempDataSetUpdate = new List<TempDataSet>();
                tempDataSetUpdate = listToBind.Where(z => z.ItemId == itemId).ToList();

                TempBOMlistByMrnId = tempMRN_BOMController.GetBOMListByMrnIdItemId(CompanyId, mrnid, itemId);
                if (TempBOMlistByMrnId.Count != 0)
                {
                    foreach (var item in TempBOMlistByMrnId)
                    {
                        BomStringList.Add(item.Meterial + "-" + item.Description);
                    }
                    //string json = new JavaScriptSerializer().Serialize(BomStringList);
                    //HiddenField2.Value = json;
                }
                lblcount.Text = BomStringList.Count.ToString();
                TempDataSet tempDataSet = new TempDataSet();
                foreach (var item in tempDataSetUpdate)
                {
                    tempDataSet.MainCategoryId = item.MainCategoryId;
                    tempDataSet.MainCategoryName = item.MainCategoryName;
                    tempDataSet.SubCategoryId = item.SubCategoryId;
                    tempDataSet.SubcategoryName = item.SubcategoryName;
                    tempDataSet.ItemId = item.ItemId;
                    tempDataSet.ItemName = item.ItemName;
                    tempDataSet.ItemQuantity = item.ItemQuantity;
                    tempDataSet.ItemDescription = item.ItemDescription;
                    tempDataSet.BiddingPeriod = item.BiddingPeriod;
                    tempDataSet.ReplacementId = item.ReplacementId;
                    tempDataSet.ReplacementName = item.ReplacementName;
                    tempDataSet.Purpose = item.Purpose;
                    tempDataSet.EstimatedAmount = item.EstimatedAmount;
                    tempDataSet.MeasurementId = item.MeasurementId;
                    tempDataSet.FileSampleProvided = item.FileSampleProvided;
                    tempDataSet.Remarks = item.Remarks;
                    tempDataSet.StockBalance = item.StockBalance;
                    tempDataSet.AvgConsumption = item.AvgConsumption;
                    tempDataSet.LastPurchasePrice = item.LastPurchasePrice;
                    tempDataSet.LastPurchaseDate = item.LastPurchaseDate;
                    tempDataSet.SupplierId = item.SupplierId;
                }

                if (listToBind.Count() > 0)
                {
                    LoadDDLMainCatregory();
                    ddlMainCateGory.SelectedValue = tempDataSet.MainCategoryId.ToString();

                    ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(tempDataSet.MainCategoryId, int.Parse(Session["CompanyId"].ToString()));
                    ddlSubCategory.DataValueField = "SubCategoryId";
                    ddlSubCategory.DataTextField = "SubCategoryName";
                    ddlSubCategory.DataBind();
                    ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));

                    ddlItemName.Enabled = false;
                    ddlSubCategory.Enabled = false;
                    ddlMainCateGory.Enabled = false;


                    ddlSubCategory.SelectedValue = tempDataSet.SubCategoryId.ToString();
                    //ddlItemName.SelectedValue = tempDataSet.ItemId.ToString();
                    txtQty.Text = tempDataSet.ItemQuantity.ToString();
                    txtDescription.Text = tempDataSet.ItemDescription;
                    ddlMeasurement.SelectedValue = tempDataSet.MeasurementId.ToString();
                    if (tempDataSet.ReplacementId == 1)
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
                    txtPurpose.Text = tempDataSet.Purpose;
                    txtEstimatedAmount.Text = tempDataSet.EstimatedAmount.ToString();
                    ddlMeasurement.SelectedValue = tempDataSet.MeasurementId.ToString();

                    if (tempDataSet.FileSampleProvided == 1)
                        rdoFileSampleEnable.Checked = true;
                    else
                        rdoFileSampleDisable.Checked = true;

                    txtBudgetRemark.Text = tempDataSet.Remarks;
                    txtStockBalance.Text = tempDataSet.StockBalance.ToString();
                    txtAvgConsumption.Text = tempDataSet.AvgConsumption.ToString();
                    txtLastPurchasePrice.Text = tempDataSet.LastPurchasePrice.ToString();
                    txtLastPurchaseDate.Text = tempDataSet.LastPurchaseDate.ToString();
                    txtLastPurchaseSupplier.Text = tempDataSet.SupplierId.ToString();
                    hndSupplierId.Value = tempDataSet.SupplierId.ToString();
                    hdnLastPurchaseDate.Value = tempDataSet.LastPurchaseDate.ToString();
                    hdnLastPurchasePrice.Value = tempDataSet.LastPurchasePrice.ToString();

                    ddlItemName.DataSource = addItemController.FetchItemsByCategories(tempDataSet.MainCategoryId, tempDataSet.SubCategoryId, CompanyId).OrderBy(y => y.ItemId).ToList();
                    ddlItemName.DataTextField = "ItemName";
                    ddlItemName.DataValueField = "ItemId";
                    ddlItemName.DataBind();
                    ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));
                    ddlItemName.SelectedValue = tempDataSet.ItemId.ToString();

                    gvRepacementImages.DataSource = tempMRN_FileUploadReplacementController.GetMRNUpoadFilesListByMRNIdItemId(CompanyId, mrnid, itemId).ToList();
                    gvRepacementImages.DataBind();
                    List<TempMRN_FileUpload> tempMRN_FileUpload = tempMRN_FileUploadController.GetPrUpoadFilesListByMRNIdItemId(CompanyId, mrnid, itemId).ToList();
                    gvPrUploadedFiles.DataSource = tempMRN_FileUpload;
                    gvPrUploadedFiles.DataBind();

                    List<TempMRN_SupportiveDocument> tempMRN_SupportiveDocument = tempMRN_SupportiveDocumentController.GetMRNSupporiveUpoadFilesListByMRNIdItemId(CompanyId, mrnid, itemId).ToList();
                    gvSupporiveFiles.DataSource = tempMRN_SupportiveDocument;
                    gvSupporiveFiles.DataBind();
                }
                lblcount.Text = BomStringList.Count.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>     $('#itemCount').text($('#tableCols tr').length - 1);   </script>", false);
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
                //Response.Redirect(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "'); });   </script>", false);

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
                //System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "','_blank'); });   </script>", false);

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
                string Mrnid = gvPrUploadedFiles.Rows[x].Cells[0].Text;
                string itemid = gvPrUploadedFiles.Rows[x].Cells[1].Text;
                string imagepath = gvPrUploadedFiles.Rows[x].Cells[2].Text;
                int deleteImages = tempMRN_FileUploadController.GetTempMrnFilesTemp(int.Parse(itemid), int.Parse(Mrnid), imagepath);

                if (deleteImages > 0)
                {
                    //lblImageDeletedMsg.Text = "Image deleted successfully";
                    //lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Green");
                    gvPrUploadedFiles.DataSource = tempMRN_FileUploadController.GetTempMrnFiles(int.Parse(itemid), int.Parse(Mrnid));
                    gvPrUploadedFiles.DataBind();

                    var SourcePath = Server.MapPath("TempPurchaseRequestFiles/" + Mrnid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("TempPurchaseRequestFiles/" + Mrnid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    //lblImageDeletedMsg.Text = "Action unsuccessfull";
                    //lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Red");
                    gvPrUploadedFiles.DataSource = tempMRN_FileUploadController.GetTempMrnFiles(int.Parse(Mrnid), int.Parse(itemid));
                    gvPrUploadedFiles.DataBind();
                }
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
                string Mrnid = tempMRN_BOMController.GetNextMrnIdObj(CompanyId).ToString();
                string itemid = gvRepacementImages.Rows[x].Cells[1].Text;
                string imagepath = gvRepacementImages.Rows[x].Cells[2].Text;
                int deleteTempReplacementImage = tempMRN_FileUploadReplacementController.GetTempMrnFilesTemp(int.Parse(itemid), int.Parse(Mrnid), imagepath);

                if (deleteTempReplacementImage > 0)
                {
                    lblReplaceimageDelete.Text = "Image deleted successfully";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Green");
                    gvRepacementImages.DataSource = tempMRN_FileUploadReplacementController.GetTempMrnFiles(int.Parse(itemid), int.Parse(Mrnid));
                    gvRepacementImages.DataBind();

                    var SourcePath = Server.MapPath("TempPrReplacementFiles/" + Mrnid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("TempPrReplacementFiles/" + Mrnid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    lblReplaceimageDelete.Text = "Action unsuccessfull";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Red");
                    gvRepacementImages.DataSource = tempMRN_FileUploadReplacementController.GetTempMrnFiles(int.Parse(Mrnid), int.Parse(itemid));
                    gvRepacementImages.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getJsonBomStringListData()
        {
            var DataList = BomStringList;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        public string getBOMData()
        {
            string data = "";
            List<TempMRN_BOM> _Mrn_BillOfMeterial = tempMRN_BOMController.GetBOMListByMrnIdItemId(CompanyId, mrnid, itemId);
            foreach (var item in _Mrn_BillOfMeterial)
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
                TempMRN_BOMController mrn_BillOfMeterialController = ControllerFactory.CreateTempMRN_BOMController();
                List<TempMRN_BOM> mrn_BillOfMeterial = mrn_BillOfMeterialController.GetBOMListByMrnIdItemId(CompanyId, int.Parse(MrnId), int.Parse(ItemId));

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

        protected void ddlPrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Session["CompanyId"].ToString() == "1")
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
                else
                {
                    if (ddlPrType.SelectedValue == "4" || ddlPrType.SelectedValue == "3" || ddlPrType.SelectedValue == "5")
                    {
                        divJobNo.Visible = false;
                        divVehicleNo.Visible = false;
                        divMake.Visible = false;
                        divModel.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
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

        protected void lbtnViewUploadSupporiveDocument_Click(object sender, EventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvSupporiveFiles.Rows[x].Cells[2].Text;
                //System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "','_blank'); });   </script>", false);

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
                string mrnid = gvSupporiveFiles.Rows[x].Cells[0].Text;
                string itemid = gvSupporiveFiles.Rows[x].Cells[1].Text;
                string imagepath = gvSupporiveFiles.Rows[x].Cells[2].Text;
                int deleteSupporiveDocuments = tempMRN_SupportiveDocumentController.GetTempMrnSupportiveFilesTemp(int.Parse(itemid), int.Parse(mrnid), imagepath);

                if (deleteSupporiveDocuments > 0)
                {
                    gvSupporiveFiles.DataSource = tempMRN_SupportiveDocumentController.GetTempMrnSupportiveFiles(int.Parse(itemid), int.Parse(mrnid));
                    gvSupporiveFiles.DataBind();

                    var SourcePath = Server.MapPath("TempPrSupportiveFiles/" + mrnid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("TempPrSupportiveFiles/" + mrnid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    gvSupporiveFiles.DataSource = tempMRN_SupportiveDocumentController.GetTempMrnSupportiveFiles(int.Parse(mrnid), int.Parse(itemid));
                    gvSupporiveFiles.DataBind();
                }
            }
            catch (Exception ex)
            {

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
                //   ddlMeasurement.Items.Insert(0, new ListItem("Select Measurement", "0"));
            }
            catch (Exception)
            {

                throw;
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

        protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvDatataTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        [WebMethod]
        public List<ListItem> Getcate(string mainCategoryId)
        {

            List<ItemSubCategory> catlist = itemSubCategoryController.FetchItemSubCategoryByCategoryId(int.Parse(mainCategoryId), int.Parse(Session["CompanyId"].ToString()));

            List<ListItem> cate = new List<ListItem>();
            foreach (var item in catlist)
            {
                cate.Add(new ListItem
                {
                    Value = item.SubCategoryId.ToString(),
                    Text = item.SubCategoryName.ToString()
                });
            }

            return cate;

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

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {

            if (txtmeterialNew.Text != "" || txtdescriptionNew.Text != "")
            {

                int mrnId = int.Parse(txtPrNumber.Text);
                int itemId = int.Parse(ddlItemName.SelectedValue);

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

        private void cleardatatable()
        {
            DataTable dt1 = (DataTable)ViewState["Specification"];
            if (dt1.Rows.Count > 0)
            {
                dt1.Rows.Clear();
                ViewState["Specification"] = dt1;
            }

            lblcount.Text = "0";
        }

        protected void btnspecifications_Click(object sender, EventArgs e)
        {
            if (ddlItemName.Text != "" && ddlItemName.Text != null)
            {
                int mrnId = int.Parse(txtPrNumber.Text);
                int itemId = int.Parse(ddlItemName.SelectedValue);

                DataTable dt = new DataTable();
                dt.Columns.AddRange(new DataColumn[3] { new DataColumn("itemId"), new DataColumn("Meterial"), new DataColumn("Description") });
                dt = (DataTable)ViewState["Specification"];

                if (btnAdd.Text == "Update Item")
                {
                    if (dt.Rows.Count == 0)
                    {
                        TempBOMlistByMrnId = tempMRN_BOMController.GetBOMListByMrnIdItemId(CompanyId, mrnId, itemId);
                        TempBOMlistByMrnId.ToList().ForEach(i => dt.Rows.Add(i.ItemId, i.Meterial, i.Description));
                    }
                }

                ViewState["Specification"] = dt;
                BindGrid();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none",
                    "<script>   $('#specification').modal('show');  populateThings();   </script>", false);


            }
            else
            {
                DisplayMessage("Please fill Item name", true);
            }
            // ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#specification').modal('show');   </script>", false);

        }

        public class TempDataSet
        {
            public int mainCategoryId = 0;
            public string mainCategoryName = string.Empty;
            public int subCategoryId = 0;
            public string subcategoryName = string.Empty;
            public int itemId = 0;
            public string itemName = string.Empty;
            public decimal itemQuantity = 0;
            public string itemDescription = string.Empty;
            public int biddingPeriod = 0;
            public int replacementId = 0;
            public string replacementName = string.Empty;
            public string purpose = string.Empty;
            public decimal estimatedAmount = 0;

            public int MainCategoryId { get; set; }
            public string MainCategoryName { get; set; }
            public int SubCategoryId { get; set; }
            public string SubcategoryName { get; set; }
            public int ItemId { get; set; }
            public string ItemName { get; set; }
            public decimal ItemQuantity { get; set; }
            public string ItemDescription { get; set; }
            public int BiddingPeriod { get; set; }
            public int ReplacementId { get; set; }
            public string ReplacementName { get; set; }
            public string Purpose { get; set; }
            public decimal EstimatedAmount { get; set; }
            public int MeasurementId { get; set; }
            public int FileSampleProvided { get; set; }
            public string Remarks { get; set; }
            public int StockBalance { get; set; }
            public decimal AvgConsumption { get; set; }
            public decimal LastPurchasePrice { get; set; }
            public int SupplierId { get; set; }
            public DateTime LastPurchaseDate { get; set; }
        }

        public class DetailsClass //Class for binding data
        {
            public string Meterial { get; set; }
            public string Description { get; set; }
        }

        public class ArrayItems
        {
            public string Metirial { get; set; }
            public string Description { get; set; }
        }

        // Class Image Files 
        public class ImageFiles
        {
            public string Name { get; set; }
            public byte[] Data { get; set; }
        }

        //------------------------PR Details Temparory store
        public class TempBOMDataSet
        {
            public TempBOMDataSet(int SeqNo, string Meterial, string Description)
            {
                seqNo = SeqNo;
                meterial = Meterial;
                description = Description;
            }

            private int seqNo;
            private string meterial;
            private string description;

            public int SeqNo
            {
                get { return seqNo; }
                set { seqNo = value; }
            }

            public string Meterial
            {
                get { return meterial; }
                set { meterial = value; }
            }

            public string Description
            {
                get { return description; }
                set { description = value; }
            }
        }

        protected void btnUploadImage_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = Request.Files["files"];
            HttpFileCollection uploads = HttpContext.Current.Request.Files;

            if (txtPrNumber.Text != "" && ddlItemName.SelectedValue != "")
            {
                List<TempMRN_FileUpload> tempList = new List<TempMRN_FileUpload>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile filex = Request.Files[i];
                    if (filex.ContentType.Contains("image"))
                    {
                        TempMRN_FileUpload tempMRN_FileUpload = null;
                        string CreateFileName = txtPrNumber.Text + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                        string FileName = Path.GetFileName(filex.FileName);
                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();
                        string NewDirectory = Server.MapPath("TempPurchaseRequestFiles/" + txtPrNumber.Text);
                        CreateDirectoryIfNotExists(NewDirectory);
                        if (filex.ContentLength > 0)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + txtPrNumber.Text + "/" + filename01)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + txtPrNumber.Text + "/" + filename01));
                            }
                        }
                        var src = HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + txtPrNumber.Text + "/" + filename01);
                        filex.SaveAs(src);
                        string x = "~/TempPurchaseRequestFiles/" + txtPrNumber.Text + "/" + filename01;
                        tempMRN_FileUpload = new TempMRN_FileUpload
                        {
                            MrnId = Convert.ToInt32(txtPrNumber.Text),
                            ItemId = Convert.ToInt32(ddlItemName.SelectedItem.Value),
                            FileName = Path.GetFileName(filex.FileName),
                            FilePath = x
                        };
                        tempList.Add(tempMRN_FileUpload);
                        gvPrUploadedFiles.DataSource = tempList;
                        gvPrUploadedFiles.DataBind();
                    }
                }

            }

        }

        protected void btnUploadDocument_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = Request.Files["supportivefiles"];
            HttpFileCollection uploads = HttpContext.Current.Request.Files;

            if (txtPrNumber.Text != "" && ddlItemName.SelectedValue != "")
            {
                List<TempMRN_SupportiveDocument> tempList = new List<TempMRN_SupportiveDocument>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile filex = Request.Files[i];
                    if (filex.ContentType.Contains("application") && filex.ContentLength > 0)
                    {
                        TempMRN_SupportiveDocument tempMRN_SupportiveDocument = null;
                        string CreateFileName = txtPrNumber.Text + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                        string FileName = Path.GetFileName(filex.FileName);
                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();
                        string NewDirectory = Server.MapPath("TempPrSupportiveFiles/" + txtPrNumber.Text);
                        CreateDirectoryIfNotExists(NewDirectory);

                        if (filex.ContentLength > 0)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + txtPrNumber.Text + "/" + filename01)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + txtPrNumber.Text + "/" + filename01));
                            }
                        }
                        var src = HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + txtPrNumber.Text + "/" + filename01);
                        filex.SaveAs(src);
                        string x = "~/TempPrSupportiveFiles/" + txtPrNumber.Text + "/" + filename01;
                        tempMRN_SupportiveDocument = new TempMRN_SupportiveDocument
                        {
                            MrnId = Convert.ToInt32(txtPrNumber.Text),
                            ItemId = Convert.ToInt32(ddlItemName.SelectedItem.Value),
                            FileName = Path.GetFileName(filex.FileName),
                            FilePath = x
                        };
                        tempList.Add(tempMRN_SupportiveDocument);
                        gvSupporiveFiles.DataSource = tempList;
                        gvSupporiveFiles.DataBind();
                    }
                }
            }
        }

        protected void btnUploadReplaceImage_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = Request.Files["fileReplace"];
            HttpFileCollection uploads = HttpContext.Current.Request.Files;

            if (txtPrNumber.Text != "" && ddlItemName.SelectedValue != "")
            {
                List<TempMRN_FileUploadReplacement> tempList = new List<TempMRN_FileUploadReplacement>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile filex = Request.Files[i];
                    if (filex.ContentType.Contains("image"))
                    {
                        TempMRN_FileUploadReplacement tempMRN_FileUploadReplacement = null;
                        string CreateFileName = txtPrNumber.Text + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                        string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                        string FileName = Path.GetFileName(filex.FileName);
                        string filename01 = UploadedFileName + "." + FileName.Split('.').Last();
                        string NewDirectory = Server.MapPath("TempPrReplacementFiles/" + txtPrNumber.Text);
                        CreateDirectoryIfNotExists(NewDirectory);

                        if (filex.ContentLength > 0)
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + txtPrNumber.Text + "/" + filename01)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + txtPrNumber.Text + "/" + filename01));
                            }
                            var src = HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + txtPrNumber.Text + "/" + filename01);
                            filex.SaveAs(src);
                            string x = "~/TempPrReplacementFiles/" + txtPrNumber.Text + "/" + filename01;
                            tempMRN_FileUploadReplacement = new TempMRN_FileUploadReplacement
                            {
                                MrnId = Convert.ToInt32(txtPrNumber.Text),
                                ItemId = Convert.ToInt32(ddlItemName.SelectedItem.Value),
                                FileName = Path.GetFileName(filex.FileName),
                                FilePath = x
                            };
                             tempList.Add(tempMRN_FileUploadReplacement);
                            gvRepacementImages.DataSource = tempList;
                            gvRepacementImages.DataBind();

                        }
                    }
                }
            }
        }
    }
}