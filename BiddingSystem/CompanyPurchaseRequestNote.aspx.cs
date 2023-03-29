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

namespace BiddingSystem
{
    public partial class CompanyPurchaseRequestNote : System.Web.UI.Page
    {
        ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
        ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();
        AddItemController addItemController = ControllerFactory.CreateAddItemController();
        ItemImageUploadController itemImageUploadController = ControllerFactory.CreateItemImageUploadController();
        TempBOMController tempBOMController = ControllerFactory.CreateTempBOMController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        GeneralSettingsController generalSettingsController = ControllerFactory.CreateGeneralSettingsController();
        TempPrFileUploadController tempPrFileUploadController = ControllerFactory.CreateTempPrFileUploadController();
        TempPR_SupportiveDocumentController tempPR_SupportiveDocumentController = ControllerFactory.CreateTempPR_SupportiveDocumentController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PrTypeController prTypeController = ControllerFactory.CreatePrTypeController();
        TempPR_FileUploadReplacementController tempPR_FileUploadReplacementController = ControllerFactory.CreateTempPR_FileUploadReplacementController();
        PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_SupportiveDocumentController pR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        UnitMeasurementController unitMeasurementController = ControllerFactory.CreateUnitMeasurementController();
        PRexpenseController prExpenseController = ControllerFactory.CreatePRexpenseController();
        PRDStockInfoLogController prDStockInfoController = ControllerFactory.CreatePRDStockInfoLogController();
        WarehouseControllerInterface warehouseController = ControllerFactory.CreateWarehouseController();

        static string UserId = string.Empty;
        List<TempDataSet> listToBind = new List<TempDataSet>();
        List<TempBOM> TempBOMlistByPrId = new List<TempBOM>();
        public List<string> BomStringList = new List<string>();
        CompanyLogin companyLogin = null;
        static int CompanyId = 0;
        public int editRowIndex = 0;
        int prid = 0;
        static int itemId = 0;
        public int ItemIdFilterd = 0;
        public enum ReplacementRdo : int { No = 1, Yes }

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
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyPurchaseRequestNote.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createPRLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                 companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

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
            
            //if ((Session["WarehouseID"] != null && Session["WarehouseID"].ToString() == "1")&& companyLogin.WarehouseID!=0)
            //{

                if (!IsPostBack)
                {

                    try
                    {
                        DateTimeRequested.Text = LocalTime.Now.ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]);
                        //Add one row in the table
                        LoadPrTypes();
                        LoadPtTypes();
                        LoadDDLMainCatregory();
                        //Delete Temp BOM Data and Temp FileUpload Data
                        tempBOMController.DeleteTempDataByDeptId(CompanyId);
                        tempPrFileUploadController.DeleteTempDataFileUploadCompanyId(CompanyId);

                        tempPR_FileUploadReplacementController.DeleteTempDataFileUploadCompanyId(CompanyId);
                        tempPR_SupportiveDocumentController.DeleteTempSupporiveFileUploadCompanyId(CompanyId);

                        //---Delete From Temporary Files
                        int purchaseRequestId = tempBOMController.GetNextPrIdObj(CompanyId);
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

                        Session["FinalListPRCreate"] = null;
                        loadWarehouse();
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

                int PrId = tempBOMController.GetNextPrIdObj((CompanyId));
                string companyName = companyDepartmentController.GetDepartmentByDepartmentId((CompanyId)).DepartmentName;
                string prCode = pr_MasterController.FetchPRCode(CompanyId);
                txtRequestedBy.Text = Session["UserNameA"].ToString();

                txtDepName.Text = companyName;
                txtPrNumber.Text = prCode;
                //---------------Modal Popup
                // CreateTable(new string[1], new string[1]);

                lblAlertMsg.Text = "";
                ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "InitClient();", true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>     $('#itemCount').text($('#tableCols tr').length - 1);   </script>", false);
            //}
            //else
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You have to be Store Keeper for Creating PR', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
            //}
        }

        private void loadWarehouse()
        {
            try
            {
                ddlWarehouse.DataSource = warehouseController.getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
                ddlWarehouse.DataValueField = "WarehouseID";
                ddlWarehouse.DataTextField = "Location";
                ddlWarehouse.DataBind();
            }
            catch (Exception)
            {

                throw;
            }
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

        //---------------Load Sub Category DDL
        protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlMainCateGory.SelectedIndex != 0 && ddlMainCateGory.SelectedValue != "")
                {
                    int mainCategoryId = int.Parse(ddlMainCateGory.SelectedValue);
                    // ddlItemName.Items.Clear();
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

        //---------------Load Items DDL
        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlSubCategory.SelectedIndex != 0 && ddlSubCategory.SelectedValue != "")
                {
                    //Session["ItemNameLists"])
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
            public string WarehouseName { get; set; }
            public int WarehouseId { get; set; }
            public int measurementId { get; set; }
            public DateTime LastPurchaseDate { get; set; }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Response.Redirect("CompanyPurchaseRequestNote.aspx");
        }

        //--------------------Proceed PR
        protected void btnSavePR_Click(object sender, EventArgs e)
        {
            try
            {
                //--------------------Save PR Master
                //int replacement = 0;
                //if(rdoEnable.Checked){
                //    replacement = 1;
                //}
                //if (rdoDisable.Checked)
                //{
                //    replacement = 0;
                //}
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
                int isApproved = 0;
                string ExpenseType = "Capital Expense";
                if (rdoOperationalExpense.Checked == true)
                {
                    ExpenseType = "Operational Expense";
                    isApproved = 1;
                }
                if (gvDatataTable.Rows.Count > 0)
                {
                    int purchaseRequestId = pr_MasterController.SavePRMaster(CompanyId, DateTime.Parse(DateTimeRequested.Text), txtQuotationFor.Text, txtRef.Text, txtRequestedBy.Text, LocalTime.Now, UserId, LocalTime.Now, "", 1, 0, "", LocalTime.Now, 0, "", LocalTime.Now, 0, int.Parse(ddlPrType.SelectedValue), ExpenseType, txtRef.Text, txtVehicleNo.Text, txtMake.Text, txtModel.Text, "", "", procedure, purchaseType, DateTime.Parse(txtRequiredDate.Text), txtMRNReferenceNo.Text, int.Parse(ddlMainCateGory.SelectedValue));
                    List<TempBOM> temBOM = new List<TempBOM>();
                    List<TempPrFileUpload> tempPrFileUpload = new List<TempPrFileUpload>();
                    List<TempPR_FileUploadReplacement> tempReplacementFileUpload = new List<TempPR_FileUploadReplacement>();
                    List<TempPR_SupportiveDocument> tempPR_SupportiveDocument = new List<TempPR_SupportiveDocument>();
                    if (purchaseRequestId > 0)
                    {
                        if(rdoOperationalExpense.Checked == true)
                        {
                            txtBudgetAmount.Text = "0";
                            txtBudgetInformation.Text = "";
                            txtBudgetRemark.Text = "";
                        }else
                        {
                            if (rdoBudgetDisable.Checked == true)
                            {
                                txtBudgetAmount.Text = "0";
                                txtBudgetInformation.Text = "";
                            }
                        }
                        prExpenseController.SavePRExpense(purchaseRequestId, Is_Budget, Convert.ToDecimal(txtBudgetAmount.Text), txtBudgetRemark.Text, txtBudgetInformation.Text , isApproved);
                    }
                    if (purchaseRequestId > 0)
                    {
                        int count = 0;
                        foreach (var item in listToBind)
                        {

                            int prDetailId = pr_DetailController.SavePRDetails(purchaseRequestId, item.ItemId, 1, item.ItemDescription, "", LocalTime.Now, 1, item.ReplacementId, item.ItemQuantity, item.Purpose, item.EstimatedAmount, item.FileSampleProvided, item.Remarks, item.MeasurementId);
                            string lastPurchaseDate = (txtLastPurchaseDate.Text == "") ? null : txtLastPurchaseDate.Text;

                            prDStockInfoController.savePRDStockInfoLog(prDetailId, item.StockBalance, item.LastPurchasePrice, item.SupplierId, item.LastPurchaseDate, item.AvgConsumption , UserId , LocalTime.Now);
                            temBOM = tempBOMController.GetListById(CompanyId, item.ItemId);

                            foreach (var itemBom in temBOM)
                            {
                                pr_BillOfMeterialController.SaveBillOfMeterial(itemBom.PrId, itemBom.ItemId, itemBom.SeqNo, itemBom.Meterial, itemBom.Description, 1, LocalTime.Now, itemBom.DepartmentId.ToString(), LocalTime.Now, UserId);
                                tempBOMController.DeleteTempData(itemBom.DepartmentId, itemBom.ItemId);
                            }

                            tempPrFileUpload = tempPrFileUploadController.GetTempPrFiles(item.ItemId, purchaseRequestId);
                            tempReplacementFileUpload = tempPR_FileUploadReplacementController.GetTempPrFiles(item.ItemId, purchaseRequestId);
                            tempPR_SupportiveDocument = tempPR_SupportiveDocumentController.GetTempPrSupportiveFiles(item.ItemId, purchaseRequestId);
                            //-------------Copy TempFolder To Original Folder
                            var source = Server.MapPath("TempPurchaseRequestFiles");
                            var target = Server.MapPath("PurchaseRequestFiles");
                            var sourceFolder = System.IO.Path.Combine(source, purchaseRequestId.ToString());
                            var targetFolder = System.IO.Path.Combine(target, purchaseRequestId.ToString());
                            CopyFolderContents(sourceFolder, targetFolder);

                            int saveFileUpload = 0;
                            foreach (var itemFileUpload in tempPrFileUpload)
                            {
                                saveFileUpload = pr_FileUploadController.SaveFileUpload(itemFileUpload.DepartmnetId, itemFileUpload.ItemId, itemFileUpload.PrId, itemFileUpload.FilePath.Replace("TempPurchaseRequestFiles", "PurchaseRequestFiles"), itemFileUpload.FileName);
                                tempPrFileUploadController.DeleteTempDataFileUpload(itemFileUpload.DepartmnetId, itemFileUpload.ItemId);
                            }


                            // replace replacement images from temp folder to original folder
                            var sourceReplacement = Server.MapPath("TempPrReplacementFiles");
                            var targetReplacement = Server.MapPath("PrReplacementFiles");
                            var sourceFolderReplacement = System.IO.Path.Combine(sourceReplacement, purchaseRequestId.ToString());
                            var targetFolderReplacement = System.IO.Path.Combine(targetReplacement, purchaseRequestId.ToString());
                            CopyFolderContents(sourceFolderReplacement, targetFolderReplacement);

                            foreach (var itemReplacementFileUpload in tempReplacementFileUpload)
                            {
                                int saveReplacementFileUpload = pR_Replace_FileUploadController.SaveFileUpload(itemReplacementFileUpload.DepartmnetId, itemReplacementFileUpload.ItemId, itemReplacementFileUpload.PrId, itemReplacementFileUpload.FilePath.Replace("TempPrReplacementFiles", "PrReplacementFiles"), itemReplacementFileUpload.FileName);
                                tempPR_FileUploadReplacementController.DeleteTempDataFileUpload(itemReplacementFileUpload.DepartmnetId, itemReplacementFileUpload.ItemId);
                            }
                            //  supporive Documents from temp folder to original folder
                            var sourceSupporiveDocuments = Server.MapPath("TempPrSupportiveFiles");
                            var targetSupporiveDocuments = Server.MapPath("PrSupportiveFiles");
                            var sourceFolderSupporiveDocuments = System.IO.Path.Combine(sourceSupporiveDocuments, purchaseRequestId.ToString());
                            var targetFolderSupporiveDocuments = System.IO.Path.Combine(targetSupporiveDocuments, purchaseRequestId.ToString());
                            CopyFolderContents(sourceFolderSupporiveDocuments, targetFolderSupporiveDocuments);

                            foreach (var itemSupportiveDocument in tempPR_SupportiveDocument)
                            {
                                int saveSupportiveDocumentFileUpload = pR_SupportiveDocumentController.SaveSupporiveFileUpload(itemSupportiveDocument.DepartmnetId, itemSupportiveDocument.ItemId, itemSupportiveDocument.PrId, itemSupportiveDocument.FilePath.Replace("TempPrSupportiveFiles", "PrSupportiveFiles"), itemSupportiveDocument.FileName);
                                tempPR_SupportiveDocumentController.DeleteTempSupporiveFileUpload(itemSupportiveDocument.DepartmnetId, itemSupportiveDocument.ItemId);
                            }



                            count++;
                        }
                        if (listToBind.Count() == count)
                        {
                          //  DisplayMessage("Purchase Request has been created successfully", false);
                            ClearFields();
                            Session["FinalListPRCreate"] = null;
                            string prCode = pr_MasterController.FetchPRCode(CompanyId);
                            txtPrNumber.Text = prCode;
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
                    lblAlertMsg.Text = "";
                    if (txtQty.Text != "" && ddlMainCateGory.SelectedIndex != 0 && ddlSubCategory.SelectedIndex != 0 && ddlItemName.SelectedIndex != 0 && txtEstimatedAmount.Text != "")
                    {
                        
                        string folderFilePath = string.Empty;
                        //if (fileUpload1.HasFile)
                        //{
                        int PrId = tempBOMController.GetNextPrIdObj(CompanyId);
                        string NewDirectory = Server.MapPath("TempPurchaseRequestFiles/" + PrId);
                        int returnType = CreateDirectoryIfNotExists(NewDirectory);

                        string NewDirectoryForReplacement = Server.MapPath("TempPrReplacementFiles/" + PrId);
                        int returnTypeR = CreateDirectoryIfNotExists(NewDirectoryForReplacement);

                        string NewDirectoryForSupportiveDocument = Server.MapPath("TempPrSupportiveFiles/" + PrId);
                        int returnTypeS = CreateDirectoryIfNotExists(NewDirectoryForSupportiveDocument);
                      
                        string contentType = "image/png";
                        string fileName = string.Empty;
                       
                        string[] arr = hdnField.Value.Split(',');
                        var list = arr.ToList();

                        list.RemoveAll(o => string.IsNullOrWhiteSpace(o));
                        List<ArrayItems> arraList = new List<ArrayItems>();
                        if (list != null || arr[1] != "")
                        {
                            for (int i = 0; i < list.Count; i += 2)
                            {
                                ArrayItems arrayItems = new ArrayItems();
                                arrayItems.Metirial = list[i];
                                arrayItems.Description = list[i + 1];
                                arraList.Add(arrayItems);
                            }
                        }
                        tempBOMController.DeleteBOMByPrId(PrId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                        for (int i = 0; i < arraList.Count(); i++)
                        {
                            tempBOMController.SaveTempBOM(CompanyId, PrId, int.Parse(ddlItemName.SelectedValue), i + 1, arraList[i].Metirial, arraList[i].Description);
                        }

                        HttpFileCollection uploads = HttpContext.Current.Request.Files;
                        for (int i = 0; i < uploads.Count; i++)
                        {
                            if (uploads.AllKeys[i] == "files[]")
                            {
                                HttpPostedFile postedFile = uploads[i];

                                string CreateFileName = PrId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0)
                                {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01)))
                                    {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01));
                                    }
                                    
                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPurchaseRequestFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPrFileUploadController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), PrId, folderFilePath, FileName);
                                }
                            }
                            if (uploads.AllKeys[i] == "fileReplace[]")
                            {
                                HttpPostedFile postedFile = uploads[i];

                                string CreateFileName = PrId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0)
                                {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01)))
                                    {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01));
                                    }

                                    //if (_tempPR_FileUploadReplacement.Count == 0)
                                    //{
                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPrReplacementFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPR_FileUploadReplacementController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), PrId, folderFilePath, FileName);
                                    //}
                                }
                            }

                            if (uploads.AllKeys[i] == "supportivefiles[]")
                            {
                                HttpPostedFile postedFile = uploads[i];

                                string CreateFileName = PrId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0)
                                {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01)))
                                    {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01));
                                    }


                                    //if (_tempPrFileUpload.Count == 0)
                                    //{
                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPrSupportiveFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPR_SupportiveDocumentController.SaveTempSupportiveUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), PrId, folderFilePath, FileName);
                                }
                                //}
                            }

                        }
                        lblAlertMsg.Text = "";
                       
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
                            lblAlertMsg.Text = "";
                        }
                        else
                        {
                            lblAlertMsg.Text = "This item is already existing in the current PR.";
                        }

                        Session["FinalListPRCreate"] = listToBind;

                        gvDatataTable.DataSource = ((List<TempDataSet>)Session["FinalListPRCreate"]).ToList();
                        gvDatataTable.DataBind();

                        ddlMainCateGory.SelectedValue = dataSet.MainCategoryId.ToString();
                        ddlMainCateGory.Enabled = false;

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

                    double estimatedCost = 0.00;

                    for (int i = 0; i < listToBind.Count; i++)
                    {
                        estimatedCost += (double)(listToBind[i].ItemQuantity * listToBind[i].EstimatedAmount);
                    }

                    txtEstimatedCost.Text = estimatedCost.ToString("N2");
                }
                if (btnAdd.Text == "Update Item")
                {
                    lblAlertMsg.Text = "";
                    string folderFilePath = string.Empty;
                    //if (fileUpload1.HasFile)
                    //{
                    int PrId = tempBOMController.GetNextPrIdObj(CompanyId);
                    string NewDirectory = Server.MapPath("TempPurchaseRequestFiles/" + PrId);
                    int returnType = CreateDirectoryIfNotExists(NewDirectory);

                    string NewDirectoryForReplacement = Server.MapPath("TempPrReplacementFiles/" + PrId);
                    int returnTypeR = CreateDirectoryIfNotExists(NewDirectoryForReplacement);

                    string NewDirectoryForSupportiveDocument = Server.MapPath("TempPrSupportiveFiles/" + PrId);
                    int returnTypeS = CreateDirectoryIfNotExists(NewDirectoryForSupportiveDocument);
                    //if(returnType == 1)
                    //{
                    string contentType = "image/png";
                    string fileName = string.Empty;

                    string[] arr = hdnField.Value.Split(',');
                    var list = arr.ToList();

                    list.RemoveAll(o => string.IsNullOrWhiteSpace(o));
                    List<ArrayItems> arraList = new List<ArrayItems>();
                    if (list != null || arr[1] != "")
                    {
                        for (int i = 0; i < list.Count; i += 2)
                        {
                            ArrayItems arrayItems = new ArrayItems();
                            arrayItems.Metirial = list[i];
                            arrayItems.Description = list[i + 1];
                            arraList.Add(arrayItems);
                        }
                    }
                    tempBOMController.DeleteBOMByPrId(PrId, CompanyId, int.Parse(ddlItemName.SelectedValue));
                    for (int i = 0; i < arraList.Count(); i++)
                    {
                        tempBOMController.SaveTempBOM(CompanyId, PrId, int.Parse(ddlItemName.SelectedValue), i + 1, arraList[i].Metirial, arraList[i].Description);
                    }

                    listToBind.RemoveAll(x => x.MainCategoryId == int.Parse(ddlMainCateGory.SelectedValue) && x.SubCategoryId == int.Parse(ddlSubCategory.SelectedValue) && x.ItemId == int.Parse(ddlItemName.SelectedValue));

                    if (listToBind.FindAll(i => i.ItemId == int.Parse(ddlItemName.SelectedValue)).Count == 0)
                    {
                        HttpFileCollection uploads = HttpContext.Current.Request.Files;
                        for (int i = 0; i < uploads.Count; i++)
                        {
                            if (uploads.AllKeys[i] == "files[]")
                            {
                                HttpPostedFile postedFile = uploads[i];
                                List<TempPrFileUpload> _TempPrFileUpload = new List<TempPrFileUpload>();

                                _TempPrFileUpload = tempPrFileUploadController.GetPrUpoadFilesListByPrIdItemId(CompanyId, PrId, itemId);

                                int maxNumber = 0;

                                foreach (var item in _TempPrFileUpload)
                                {
                                    int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                    if (CalNumber > maxNumber)
                                        maxNumber = CalNumber;
                                }

                                string CreateFileName = PrId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0)
                                {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01)))
                                    {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01));
                                    }

                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPurchaseRequestFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPrFileUploadController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), PrId, folderFilePath, FileName);
                                }
                            }
                            if (uploads.AllKeys[i] == "fileReplace[]")
                            {
                                HttpPostedFile postedFile = uploads[i];

                                List<TempPR_FileUploadReplacement> _TempPR_FileUploadReplacement = new List<TempPR_FileUploadReplacement>();

                                _TempPR_FileUploadReplacement = tempPR_FileUploadReplacementController.GetPrUpoadFilesListByPrIdItemId(CompanyId, PrId, itemId);
                                int maxNumber = 0;

                                foreach (var item in _TempPR_FileUploadReplacement)
                                {
                                    int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                    if (CalNumber > maxNumber)
                                        maxNumber = CalNumber;
                                }

                                string CreateFileName = PrId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (maxNumber + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0)
                                {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01)))
                                    {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01));
                                    }

                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPrReplacementFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPR_FileUploadReplacementController.SaveTempImageUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), PrId, folderFilePath, FileName);
                                }
                            }

                            if (uploads.AllKeys[i] == "supportivefiles[]")
                            {
                                HttpPostedFile postedFile = uploads[i];
                                List<TempPR_SupportiveDocument> tempPR_SupportiveDocument = new List<TempPR_SupportiveDocument>();

                                tempPR_SupportiveDocument = tempPR_SupportiveDocumentController.GetPrSupporiveUpoadFilesListByPrIdItemId(CompanyId, PrId, itemId);

                                int maxNumber = 0;

                                foreach (var item in tempPR_SupportiveDocument)
                                {
                                    int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                    if (CalNumber > maxNumber)
                                        maxNumber = CalNumber;
                                }

                                string CreateFileName = PrId + "_" + int.Parse(ddlItemName.SelectedValue) + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0)
                                {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01)))
                                    {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01));
                                    }

                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPrSupportiveFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPR_SupportiveDocumentController.SaveTempSupportiveUpload(CompanyId, int.Parse(ddlItemName.SelectedValue), PrId, folderFilePath, FileName);
                                }
                            }
                        }
                        lblAlertMsg.Text = "";
                    }
                    else
                    {
                        lblAlertMsg.Text = "This item is already existing in the current PR.";
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

                    }
                    else
                    {
                        // lblAlertMsg.Text = "This item is already existing in the current PR.";
                    }

                    Session["FinalListPRCreate"] = listToBind;

                    gvDatataTable.DataSource = ((List<TempDataSet>)Session["FinalListPRCreate"]).ToList();
                    gvDatataTable.DataBind();

                    txtPurpose.Text = "";
                    txtDescription.Text = "";
                    rdoDisable.Checked = true;
                    txtQty.Text = "";
                    txtEstimatedAmount.Text = "";
                    LoadDDLMainCatregory();
                    ddlSubCategory.SelectedIndex = 0;
                    ddlItemName.SelectedIndex = 0;
                    ddlItemName.Enabled = true;
                    ddlSubCategory.Enabled = true;
                    btnAdd.Text = "Add Item";
                    gvPrUploadedFiles.DataSource = null;
                    gvPrUploadedFiles.DataBind();
                    gvRepacementImages.DataSource = null;
                    gvRepacementImages.DataBind();
                    gvSupporiveFiles.DataSource = null;
                    gvSupporiveFiles.DataBind();
                    ddlSubCategory.DataSource = null;
                    ddlSubCategory.DataBind();
                    txtStockBalance.Text = "0";
                    txtAvgConsumption.Text = "0";
                    txtLastPurchasePrice.Text = "";
                    txtLastPurchaseDate.Text = "";
                    hndSupplierId.Value = "";

                    ddlItemName.DataSource = null;
                    ddlItemName.DataBind();
                    ddlMeasurement.SelectedIndex = 0;

                    double estimatedCost = 0.00;

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

        private void ClearFields()
        {
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
            ddlSubCategory.DataSource = null;
            ddlSubCategory.DataBind();

            ddlItemName.DataSource = null;
            ddlItemName.DataBind();
            ddlMeasurement.SelectedIndex = 0;


            txtQuotationFor.Text = "";
            txtRef.Text = "";
            txtRequestedBy.Text = "";
            txtEstimatedAmount.Text = "";
            //DateTimeRequested.Text = "";
            
            //estimatedCost = 0.00;
            txtEstimatedCost.Text = "";
            rdoBudgetEnable.Checked = true;
            txtBudgetAmount.Text = "";
            txtBudgetInformation.Text = "";

            gvDatataTable.DataSource = null;
            gvDatataTable.DataBind();

        }

        // Class Image Files 
        public class ImageFiles
        {
            public string Name { get; set; }
            public byte[] Data { get; set; }
        }

        protected void lbtnViewBOM_Click(object sender, EventArgs e)
        {
            try
            {
                int PrId = tempBOMController.GetNextPrIdObj(CompanyId);
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);
                List<TempBOM> tempBOMList = tempBOMController.GetBOMListByPrIdItemId(CompanyId, PrId, itemId);
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
                    List<TempBOM> tempBOMList = tempBOMController.GetItemspecification(itemID);
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
                int PrId = tempBOMController.GetNextPrIdObj(CompanyId);
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);

                List<TempPrFileUpload> GetTempPrFiles = tempPrFileUploadController.GetTempPrFiles(itemId, PrId);
                gvUploadedPhotos.DataSource = GetTempPrFiles;
                gvUploadedPhotos.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>   $('#myModalUploadedPhotos').modal('show');   </script>", false);
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
                int PrId = tempBOMController.GetNextPrIdObj(CompanyId);
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                int itemId = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);

                List<TempPR_FileUploadReplacement> GetTempReplacementFiles = tempPR_FileUploadReplacementController.GetPrUpoadFilesListByPrIdItemId(CompanyId, PrId, itemId);
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

                    int PrId = tempBOMController.GetNextPrIdObj(CompanyId);

                    int deleteBom = tempBOMController.DeleteBOMByPrId(PrId, CompanyId, ItemId);


                    var SourcePath = Server.MapPath("TempPrReplacementFiles/" + PrId);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    List<TempPR_FileUploadReplacement> _tempPR_FileUploadReplacement = tempPR_FileUploadReplacementController.GetPrUpoadFilesListByPrIdItemId(CompanyId, PrId, ItemId).ToList();

                    foreach (var item in _tempPR_FileUploadReplacement)
                    {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in di.GetFiles())
                        {
                            if (file.Name == getNAme[3])
                            {
                                File.Delete(Server.MapPath("TempPrReplacementFiles/" + PrId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempReplacementImage = tempPR_FileUploadReplacementController.DeleteTempPrDetailFileUpload(PrId, CompanyId, ItemId);

                    var SourcePathFileUpload = Server.MapPath("TempPurchaseRequestFiles/" + PrId);
                    System.IO.DirectoryInfo di1 = new DirectoryInfo(SourcePath);

                    List<TempPrFileUpload> TempPrFileUpload = tempPrFileUploadController.GetPrUpoadFilesListByPrIdItemId(CompanyId, PrId, ItemId).ToList();

                    foreach (var item in _tempPR_FileUploadReplacement)
                    {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in di1.GetFiles())
                        {
                            if (file.Name == getNAme[3])
                            {
                                File.Delete(Server.MapPath("TempPurchaseRequestFiles/" + PrId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempFileUpload = tempPrFileUploadController.DeleteTempPrDetailFileUpload(PrId, CompanyId, ItemId);


                    var SourcePathSupportive = Server.MapPath("TempPrSupportiveFiles/" + PrId);
                    System.IO.DirectoryInfo dis = new DirectoryInfo(SourcePath);

                    List<TempPR_SupportiveDocument> _tempPR_SupportiveDocument = tempPR_SupportiveDocumentController.GetPrSupporiveUpoadFilesListByPrIdItemId(CompanyId, PrId, ItemId).ToList();

                    foreach (var item in _tempPR_SupportiveDocument)
                    {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in dis.GetFiles())
                        {
                            if (file.Name == getNAme[3])
                            {
                                File.Delete(Server.MapPath("TempPrSupportiveFiles/" + PrId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempSupportiveDocument = tempPR_SupportiveDocumentController.DeleteTempPrDetailSupporiveUpload(PrId, CompanyId, ItemId);





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
                prid = tempBOMController.GetNextPrIdObj(CompanyId);
                itemId = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);

                btnAdd.Text = "Update Item";
                List<TempDataSet> tempDataSetUpdate = new List<TempDataSet>();
                tempDataSetUpdate = listToBind.Where(z => z.ItemId == itemId).ToList();

                TempBOMlistByPrId = tempBOMController.GetBOMListByPrIdItemId(CompanyId, prid, itemId);
                if (TempBOMlistByPrId.Count != 0)
                {
                    foreach (var item in TempBOMlistByPrId)
                    {
                        BomStringList.Add(item.Meterial + "-" + item.Description);
                    }
                    string json = new JavaScriptSerializer().Serialize(BomStringList);
                    HiddenField2.Value = json;
                }

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


                    //ddlItemName.DataSource = addItemController.FetchItemList().Where(y => y.SubCategoryId == tempDataSet.SubCategoryId).ToList();
                    //ddlItemName.DataTextField = "ItemName";
                    //ddlItemName.DataValueField = "ItemId";
                    //ddlItemName.DataBind();
                    //ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));



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

                    gvRepacementImages.DataSource = tempPR_FileUploadReplacementController.GetPrUpoadFilesListByPrIdItemId(CompanyId, prid, itemId).ToList();
                    gvRepacementImages.DataBind();
                    List<TempPrFileUpload> TempPrFileUpload = tempPrFileUploadController.GetPrUpoadFilesListByPrIdItemId(CompanyId, prid, itemId).ToList();
                    gvPrUploadedFiles.DataSource = TempPrFileUpload;
                    gvPrUploadedFiles.DataBind();

                    List<TempPR_SupportiveDocument> TempPR_SupportiveDocument = tempPR_SupportiveDocumentController.GetPrSupporiveUpoadFilesListByPrIdItemId(CompanyId, prid, itemId).ToList();
                    gvSupporiveFiles.DataSource = TempPR_SupportiveDocument;
                    gvSupporiveFiles.DataBind();
                }
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
                string prid = gvPrUploadedFiles.Rows[x].Cells[0].Text;
                string itemid = gvPrUploadedFiles.Rows[x].Cells[1].Text;
                string imagepath = gvPrUploadedFiles.Rows[x].Cells[2].Text;
                int deleteImages = tempPrFileUploadController.GetTempPrFilesTemp(int.Parse(itemid), int.Parse(prid), imagepath);

                if (deleteImages > 0)
                {
                    //lblImageDeletedMsg.Text = "Image deleted successfully";
                    //lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Green");
                    gvPrUploadedFiles.DataSource = tempPrFileUploadController.GetTempPrFiles(int.Parse(itemid), int.Parse(prid));
                    gvPrUploadedFiles.DataBind();

                    var SourcePath = Server.MapPath("TempPurchaseRequestFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("TempPurchaseRequestFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    //lblImageDeletedMsg.Text = "Action unsuccessfull";
                    //lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Red");
                    gvPrUploadedFiles.DataSource = tempPrFileUploadController.GetTempPrFiles(int.Parse(prid), int.Parse(itemid));
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
                string prid = tempBOMController.GetNextPrIdObj(CompanyId).ToString();
                string itemid = gvRepacementImages.Rows[x].Cells[1].Text;
                string imagepath = gvRepacementImages.Rows[x].Cells[2].Text;
                int deleteTempReplacementImage = tempPR_FileUploadReplacementController.GetTempPrFilesTemp(int.Parse(itemid), int.Parse(prid), imagepath);

                if (deleteTempReplacementImage > 0)
                {
                    lblReplaceimageDelete.Text = "Image deleted successfully";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Green");
                    gvRepacementImages.DataSource = tempPR_FileUploadReplacementController.GetTempPrFiles(int.Parse(itemid), int.Parse(prid));
                    gvRepacementImages.DataBind();

                    var SourcePath = Server.MapPath("TempPrReplacementFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("TempPrReplacementFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    lblReplaceimageDelete.Text = "Action unsuccessfull";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Red");
                    gvRepacementImages.DataSource = tempPR_FileUploadReplacementController.GetTempPrFiles(int.Parse(prid), int.Parse(itemid));
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
            List<TempBOM> _PR_BillOfMeterial = tempBOMController.GetBOMListByPrIdItemId(CompanyId, prid, itemId);
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
                TempBOMController pr_BillOfMeterialController = ControllerFactory.CreateTempBOMController();
                List<TempBOM> pr_BillOfMeterial = pr_BillOfMeterialController.GetBOMListByPrIdItemId(CompanyId, int.Parse(PrId), int.Parse(ItemId));

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

        private void clearFields()
        {
            try
            {
                lblAlertMsg.Text = "";
                btnAdd.Text = "Add Item";
                txtPurpose.Text = "";
                txtDescription.Text = "";
                txtQty.Text = "";
                txtEstimatedAmount.Text = "";
                LoadDDLMainCatregory();
                gvPrUploadedFiles.DataSource = null;
                gvPrUploadedFiles.DataBind();

                gvSupporiveFiles.DataSource = null;
                gvSupporiveFiles.DataBind();

                gvRepacementImages.DataSource = null;
                gvRepacementImages.DataBind();

                ddlMainCateGory.SelectedIndex = 0;
                ddlSubCategory.Items.Clear();
                ddlSubCategory.DataBind();

                ddlItemName.Text = "";
                //ddlItemName.DataBind();

                ddlMainCateGory.Enabled = true;
                ddlSubCategory.Enabled = true;
                ddlItemName.Enabled = true;
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
                string prid = gvSupporiveFiles.Rows[x].Cells[0].Text;
                string itemid = gvSupporiveFiles.Rows[x].Cells[1].Text;
                string imagepath = gvSupporiveFiles.Rows[x].Cells[2].Text;
                int deleteSupporiveDocuments = tempPR_SupportiveDocumentController.GetTempPrSupportiveFilesTemp(int.Parse(itemid), int.Parse(prid), imagepath);

                if (deleteSupporiveDocuments > 0)
                {
                    gvSupporiveFiles.DataSource = tempPR_SupportiveDocumentController.GetTempPrSupportiveFiles(int.Parse(itemid), int.Parse(prid));
                    gvSupporiveFiles.DataBind();

                    var SourcePath = Server.MapPath("TempPrSupportiveFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles())
                    {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3])
                        {
                            File.Delete(Server.MapPath("TempPrSupportiveFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else
                {
                    gvSupporiveFiles.DataSource = tempPR_SupportiveDocumentController.GetTempPrSupportiveFiles(int.Parse(prid), int.Parse(itemid));
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
                //ddlMeasurement.Items.Insert(0, new ListItem("Select Measurement", "0"));
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
    }
}