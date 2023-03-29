using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class CompnayPurchaseRequestNote : System.Web.UI.Page {
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


        //static string UserId = string.Empty;
        //List<TempDataSet> listToBind = new List<TempDataSet>();
        //List<TempBOM> TempBOMlistByPrId = new List<TempBOM>();
        //public List<string> BomStringList = new List<string>();

        //static int CompanyId = 0;
        //public int editRowIndex = 0;
        //string[] Tes1ItemTextBoxValue;
        //string[] Tes2ItemTextBoxValue;
        //DateTime Requested;
        //int prid = 0;
        //static int itemId = 0;
        //public int ItemIdFilterd = 0;
        ////string[] Tes3ItemTextBoxValue;
        //public enum ReplacementRdo : int { No = 1, Yes }

        //static int CategoryId = 0;
        //static int SubCatergoryId = 0;
        //static string itemName = "";

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["UserId"] == null) {
                if (ViewState["UserId"] == null || ViewState["UserId"].ToString() == string.Empty) {

                    Response.Redirect("LoginPage.aspx");
                }
                else {
                    Session["CompanyId"] = ViewState["CompanyId"].ToString();
                    Session["UserId"] = ViewState["UserId"].ToString();
                }
            }
            // HiddenField2.Value = "test";
            if (Session["CompanyId"] != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "CompnayPurchaseRequestNote.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "createPRLink";

                ViewState["CompanyId"] = Session["CompanyId"].ToString();
                ViewState["UserId"] = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(ViewState["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["UserId"].ToString()), int.Parse(ViewState["CompanyId"].ToString()), 5, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }

            else {
                Response.Redirect("LoginPage.aspx");
            }
            txtDepName.Enabled = false;
            txtPrNumber.Enabled = false;
            msg.Visible = false;
            if (!IsPostBack) {
                try {

                    //if (Session["SubDepartmentID"] != null && Session["SubDepartmentID"].ToString() != "")
                    //{
                    if (Session["UserWarehouses"] != null && (Session["UserWarehouses"] as List<UserWarehouse>).Count > 0) {
                        LoadUserWarehouses();
                        
                    }
                    else {
                        LoadCompanyWarehouses();

                    }


                        DateTimeRequested.Text = LocalTime.Now.ToString();
                    ////Add one row in the table
                    LoadPrTypes();

                   
                  
                    //LoadDDLMainCatregory();
                    //Delete Temp BOM Data and Temp FileUpload Data
                    tempBOMController.DeleteTempDataByDeptId(int.Parse(ViewState["CompanyId"].ToString()));
                    tempPrFileUploadController.DeleteTempDataFileUploadCompanyId(int.Parse(ViewState["CompanyId"].ToString()));

                    tempPR_FileUploadReplacementController.DeleteTempDataFileUploadCompanyId(int.Parse(ViewState["CompanyId"].ToString()));
                    tempPR_SupportiveDocumentController.DeleteTempSupporiveFileUploadCompanyId(int.Parse(ViewState["CompanyId"].ToString()));

                    //---Delete From Temporary Files
                    int purchaseRequestId = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString()));
                    var SourcePath = Server.MapPath("TempPurchaseRequestFiles");
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles()) {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.GetDirectories()) {
                        dir.Delete(true);
                    }

                    // Delete temp Replacement Images from directory
                    var SourcePathReplacementImages = Server.MapPath("TempPrReplacementFiles");
                    System.IO.DirectoryInfo dis = new DirectoryInfo(SourcePathReplacementImages);

                    foreach (FileInfo file in dis.GetFiles()) {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in dis.GetDirectories()) {
                        dir.Delete(true);
                    }

                    var SourcePathSupportiveDocument = Server.MapPath("TempPrReplacementFiles");
                    System.IO.DirectoryInfo dif = new DirectoryInfo(SourcePathSupportiveDocument);

                    foreach (FileInfo file in dif.GetFiles()) {
                        file.Delete();
                    }
                    foreach (DirectoryInfo did in dif.GetDirectories()) {
                        did.Delete(true);
                    }

                    LoadPRCategory();
                    //bindMeasurement();

                    //if (Session["int.Parse(ViewState["CompanyId"].ToString())"].ToString() == "3")
                    //{
                    //    if (ddlPrType.SelectedValue == "4")
                    //    {
                    //        divJobNo.Visible = false;
                    //        divVehicleNo.Visible = false;
                    //        divMake.Visible = false;
                    //        divModel.Visible = false;
                    //    }
                    //}

                    if (Session["FinalListPRCreate"] != null) {
                        if (((List<TempDataSet>)Session["FinalListPRCreate"]).ToList().Count > 0) {
                            gvDatataTable.DataSource = ((List<TempDataSet>)Session["FinalListPRCreate"]).ToList();
                            gvDatataTable.DataBind();
                        }
                    }
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You are not assigned to a department yet.', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                    //}
                }
                catch (Exception ex) {
                    throw ex;
                }

            }
            else {
                if (Session["FinalListPRCreate"] != null) {
                    ViewState["ListToBind"] = new JavaScriptSerializer().Serialize((List<TempDataSet>)Session["FinalListPRCreate"]);
                }
            }
            int PrId = tempBOMController.GetNextPrIdObj((int.Parse(ViewState["CompanyId"].ToString())));
            string companyName = companyDepartmentController.GetDepartmentByDepartmentId((int.Parse(ViewState["CompanyId"].ToString()))).DepartmentName;
            string prCode = pr_MasterController.FetchPRCode(int.Parse(ViewState["CompanyId"].ToString()));

            txtDepName.Text = companyName;
            txtPrNumber.Text = prCode;
            //---------------Modal Popup
            // CreateTable(new string[1], new string[1]);

            lblAlertMsg.Text = "";
            ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "InitClient();", true);
            //  ScriptManager.RegisterStartupScript(Updatepanel1, this.Updatepanel1.GetType(), "YourUniqueScriptKey", "InitClientReplace();", true);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>     $('#itemCount').text($('#tableCols tr').length - 1);   </script>", false);
        }

        private void LoadUserWarehouses() {
            ddlWarehouse.DataSource = ControllerFactory.CreateUserWarehouse().GetWarehousesByUserId(int.Parse(ViewState["UserId"].ToString()));
            ddlWarehouse.DataValueField = "WrehouseId";
            ddlWarehouse.DataTextField = "Location";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("Select a Warehouse", ""));
        }

        private void LoadCompanyWarehouses() {
            ddlWarehouse.DataSource = ControllerFactory.CreateWarehouseController().getWarehouseList(int.Parse(Session["CompanyId"].ToString()));
            ddlWarehouse.DataValueField = "WarehouseID";
            ddlWarehouse.DataTextField = "Location";
            ddlWarehouse.DataBind();
            ddlWarehouse.Items.Insert(0, new ListItem("Select a Warehouse", ""));
        }

        private void LoadPrTypes() {
            try {
                ddlPrType.DataSource = prTypeController.FetchPRTypesByCompanyId(int.Parse(ViewState["CompanyId"].ToString()));
                ddlPrType.DataValueField = "PrTypeId";
                ddlPrType.DataTextField = "PrTypeName";
                ddlPrType.DataBind();

                if (ddlPrType.SelectedValue == "1") {
                    divJobNo.Visible = false;
                    divVehicleNo.Visible = false;
                    divMake.Visible = false;
                    divModel.Visible = false;
                }
                else if (ddlPrType.SelectedValue == "2") {
                    divJobNo.Visible = true;
                    divVehicleNo.Visible = true;
                    divMake.Visible = true;
                    divModel.Visible = true;
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void LoadPRCategory() {
            try {
                ddlPRCategory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["CompanyId"].ToString())).Where(c => c.CategoryName != "Undefined").OrderBy(cat => cat.CategoryName);
                ddlPRCategory.DataValueField = "CategoryId";
                ddlPRCategory.DataTextField = "CategoryName";
                ddlPRCategory.DataBind();
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.ddlPRCategoryCl').select2(); });   </script>", false);

            }
            catch (Exception ex) {
                throw;
            }
        }

        //private void LoadDDLMainCatregory()
        //{
        //    try
        //    {
        //        ddlMainCateGory.DataSource = itemCategoryController.FetchItemCategoryList(int.Parse(Session["int.Parse(ViewState["CompanyId"].ToString())"].ToString())).OrderBy(cat=>cat.CategoryName);
        //        ddlMainCateGory.DataValueField = "CategoryId";
        //        ddlMainCateGory.DataTextField = "CategoryName";
        //        ddlMainCateGory.DataBind();
        //        ddlMainCateGory.Items.Insert(0, new ListItem("Select Main Category", ""));

        //        ddlSubCategory.Items.Clear();
        //        ddlItemName.Items.Clear();
        //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.category-cl').select2(); });   </script>", false);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //---------------Load Sub Category DDL
        //protected void ddlMainCateGory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlMainCateGory.SelectedIndex != 0 && hdnCategoryId.Value != "")
        //        {
        //            CategoryId = int.Parse(hdnCategoryId.Value);
        //            ddlItemName.Items.Clear();
        //            LoadSubCategoryDDL(CategoryId);
        //            //txtItem.Text = "";
        //            hdnItemId.Value = "";
        //        }
        //        else
        //        {
        //            ddlSubCategory.Items.Clear();
        //            //txtItem.Text = "";
        //            hdnItemId.Value = "";
        //            ddlItemName.Items.Clear();
        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        //private void LoadSubCategoryDDL(int SubCatId)
        //{
        //    try
        //    {
        //        ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(SubCatId, int.Parse(Session["int.Parse(ViewState["CompanyId"].ToString())"].ToString())).OrderBy(subCat=> subCat.SubCategoryName);
        //        ddlSubCategory.DataTextField = "SubCategoryName";
        //        ddlSubCategory.DataValueField = "SubCategoryId";
        //        ddlSubCategory.DataBind();
        //        ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));
        //        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //---------------Load Items DDL
        //protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (ddlSubCategory.SelectedIndex != 0 && hdnSubCategoryId.Value != "")
        //        {
        //            SubCatergoryId = int.Parse(hdnSubCategoryId.Value);
        //            ////Session["ItemNameLists"])
        //            Session["MainCategoryId"] = hdnCategoryId.Value;
        //            Session["SubCategoryId"] = hdnSubCategoryId.Value;
        //            int categoryId = int.Parse(hdnCategoryId.Value);
        //            int subCategoryId = int.Parse(hdnSubCategoryId.Value);

        //            ddlItemName.Items.Clear();
        //            List<AddItem> items = addItemController.FetchItemsByCategoriesForDropdown(categoryId, subCategoryId, int.Parse(ViewState["CompanyId"].ToString()));

        //            ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));
        //            for (int i = 0; i < items.Count; i++)
        //            {
        //                ddlItemName.Items.Add(new ListItem(items[i].ItemName, items[i].ItemId.ToString()));
        //            }
        //            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

        //            //txtItem.Text = "";
        //            //hdnItemId.Value = "";
        //        }
        //        else
        //        {
        //            ddlItemName.Items.Clear();
        //            //txtItem.Text = "";
        //            //hdnItemId.Value = "";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //------------------------PR Details Temparory store
        public class TempBOMDataSet {
            public TempBOMDataSet(int SeqNo, string Meterial, string Description) {
                seqNo = SeqNo;
                meterial = Meterial;
                description = Description;
            }

            private int seqNo;
            private string meterial;
            private string description;

            public int SeqNo {
                get { return seqNo; }
                set { seqNo = value; }
            }

            public string Meterial {
                get { return meterial; }
                set { meterial = value; }
            }

            public string Description {
                get { return description; }
                set { description = value; }
            }
        }

        public class TempDataSet {
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
            public int measurementId { get; set; }
            public int IsNew { get; set; }
            public string MRNCodes { get; set; }

            public string TRCodes { get; set; }
            public int WarehouseId { get; set; }
            public string WarehouseName { get; set; }
            public string MeasurementName { get; set; }
        }

        protected void btnOK_Click(object sender, EventArgs e) {
            Response.Redirect("CompnayPurchaseRequestNote.aspx");
        }

        //--------------------Proceed PR
        protected void btnSavePR_Click(object sender, EventArgs e) {
            try {
                //--------------------Save PR Master
                //int replacement = 0;
                //if(rdoEnable.Checked){
                //    replacement = 1;
                //}
                //if (rdoDisable.Checked)
                //{
                //    replacement = 0;
                //}
                if (gvDatataTable.Rows.Count > 0) {
                    //int purchaseRequestId = pr_MasterController.SavePRMaster(int.Parse(ViewState["CompanyId"].ToString()), DateTime.Parse(DateTimeRequested.Text), txtQuotationFor.Text, txtRef.Text, txtRequestedBy.Text, LocalTime.Now, int.Parse(ViewState["UserId"].ToString()), LocalTime.Now, "", 1, 0, "", LocalTime.Now, 0, "", LocalTime.Now, 0, int.Parse(ddlPrType.SelectedValue), ddlExpenseType.SelectedValue, txtRef.Text, txtVehicleNo.Text, txtMake.Text, txtModel.Text, "", "", txtTerms.Text,int.Parse(Session["SubDepartmentID"].ToString()));
                    int purchaseRequestId = pr_MasterController.SavePRMaster(int.Parse(ViewState["CompanyId"].ToString()), DateTime.Parse(DateTimeRequested.Text.ProcessString()), txtQuotationFor.Text.ProcessString(), txtRef.Text, txtRequestedBy.Text.ProcessString(), LocalTime.Now, ViewState["UserId"].ToString(), LocalTime.Now, "", 1, 0, "", LocalTime.Now, 0, "", LocalTime.Now, 0, int.Parse(ddlPrType.SelectedValue), ddlExpenseType.SelectedValue, txtRef.Text, txtVehicleNo.Text, txtMake.Text, txtModel.Text, "", "", txtTerms.Text, 0, int.Parse(ddlPRCategory.SelectedValue.ToString()), int.Parse(ddlWarehouse.SelectedValue.ToString()));
                    List<TempBOM> temBOM = new List<TempBOM>();
                    List<TempPrFileUpload> tempPrFileUpload = new List<TempPrFileUpload>();
                    List<TempPR_FileUploadReplacement> tempReplacementFileUpload = new List<TempPR_FileUploadReplacement>();
                    List<TempPR_SupportiveDocument> tempPR_SupportiveDocument = new List<TempPR_SupportiveDocument>();

                    if (purchaseRequestId > 0) {
                        var listToBind = new JavaScriptSerializer().Deserialize<List<TempDataSet>>(ViewState["ListToBind"].ToString());
                        int count = 0;
                        foreach (var item in listToBind) {
                            int oldItemId = item.ItemId;
                            if (item.IsNew == 1) {
                                item.ItemId = addItemController.addUncategorizedItem(item.ItemName.ProcessString(), ViewState["UserId"].ToString(), int.Parse(ViewState["CompanyId"].ToString()),item.measurementId);
                            }

                            pr_DetailController.SavePRDetails(purchaseRequestId, item.ItemId, item.measurementId, item.ItemDescription, "", LocalTime.Now, 1, item.ReplacementId, item.ItemQuantity, item.Purpose, item.EstimatedAmount);
                            temBOM = tempBOMController.GetListById(int.Parse(ViewState["CompanyId"].ToString()), oldItemId);

                            foreach (var itemBom in temBOM) {
                                pr_BillOfMeterialController.SaveBillOfMeterial(itemBom.PrId, itemBom.ItemId, itemBom.SeqNo, itemBom.Meterial, itemBom.Description, 1, LocalTime.Now, itemBom.DepartmentId.ToString(), LocalTime.Now, ViewState["UserId"].ToString());
                                tempBOMController.DeleteTempData(itemBom.DepartmentId, oldItemId);
                            }

                            tempPrFileUpload = tempPrFileUploadController.GetTempPrFiles(oldItemId, purchaseRequestId);
                            tempReplacementFileUpload = tempPR_FileUploadReplacementController.GetTempPrFiles(oldItemId, purchaseRequestId);
                            tempPR_SupportiveDocument = tempPR_SupportiveDocumentController.GetTempPrSupportiveFiles(oldItemId, purchaseRequestId);
                            //-------------Copy TempFolder To Original Folder
                            var source = Server.MapPath("TempPurchaseRequestFiles");
                            var target = Server.MapPath("PurchaseRequestFiles");
                            var sourceFolder = System.IO.Path.Combine(source, purchaseRequestId.ToString());
                            var targetFolder = System.IO.Path.Combine(target, purchaseRequestId.ToString());
                            CopyFolderContents(sourceFolder, targetFolder);

                            int saveFileUpload = 0;
                            foreach (var itemFileUpload in tempPrFileUpload) {
                                saveFileUpload = pr_FileUploadController.SaveFileUpload(itemFileUpload.DepartmnetId, item.ItemId, itemFileUpload.PrId, itemFileUpload.FilePath.Replace("TempPurchaseRequestFiles", "PurchaseRequestFiles"), itemFileUpload.FileName);
                                tempPrFileUploadController.DeleteTempDataFileUpload(itemFileUpload.DepartmnetId, itemFileUpload.ItemId);
                            }


                            // replace replacement images from temp folder to original folder
                            var sourceReplacement = Server.MapPath("TempPrReplacementFiles");
                            var targetReplacement = Server.MapPath("PrReplacementFiles");
                            var sourceFolderReplacement = System.IO.Path.Combine(sourceReplacement, purchaseRequestId.ToString());
                            var targetFolderReplacement = System.IO.Path.Combine(targetReplacement, purchaseRequestId.ToString());
                            CopyFolderContents(sourceFolderReplacement, targetFolderReplacement);

                            foreach (var itemReplacementFileUpload in tempReplacementFileUpload) {
                                int saveReplacementFileUpload = pR_Replace_FileUploadController.SaveFileUpload(itemReplacementFileUpload.DepartmnetId, item.ItemId, itemReplacementFileUpload.PrId, itemReplacementFileUpload.FilePath.Replace("TempPrReplacementFiles", "PrReplacementFiles"), itemReplacementFileUpload.FileName);
                                tempPR_FileUploadReplacementController.DeleteTempDataFileUpload(itemReplacementFileUpload.DepartmnetId, itemReplacementFileUpload.ItemId);
                            }
                            //  supporive Documents from temp folder to original folder
                            var sourceSupporiveDocuments = Server.MapPath("TempPrSupportiveFiles");
                            var targetSupporiveDocuments = Server.MapPath("PrSupportiveFiles");
                            var sourceFolderSupporiveDocuments = System.IO.Path.Combine(sourceSupporiveDocuments, purchaseRequestId.ToString());
                            var targetFolderSupporiveDocuments = System.IO.Path.Combine(targetSupporiveDocuments, purchaseRequestId.ToString());
                            CopyFolderContents(sourceFolderSupporiveDocuments, targetFolderSupporiveDocuments);

                            foreach (var itemSupportiveDocument in tempPR_SupportiveDocument) {
                                int saveSupportiveDocumentFileUpload = pR_SupportiveDocumentController.SaveSupporiveFileUpload(itemSupportiveDocument.DepartmnetId, item.ItemId, itemSupportiveDocument.PrId, itemSupportiveDocument.FilePath.Replace("TempPrSupportiveFiles", "PrSupportiveFiles"), itemSupportiveDocument.FileName);
                                tempPR_SupportiveDocumentController.DeleteTempSupporiveFileUpload(itemSupportiveDocument.DepartmnetId, itemSupportiveDocument.ItemId);
                            }



                            count++;
                        }
                        if (listToBind.Count() == count) {

                            Thread th = new Thread(new ThreadStart(() => sendEmails(purchaseRequestId)));
                            th.Start();
                            //DisplayMessage("Purchase Requestition has been created successfully", false);
                            ClearFields();
                            Session["FinalListPRCreate"] = null;
                            string prCode = pr_MasterController.FetchPRCode(int.Parse(ViewState["CompanyId"].ToString()));
                            txtPrNumber.Text = prCode;
                            ViewState["ListToBind"] = null;
                            //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#SuccessAlert').modal('show'); });   </script>", false);
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>  alert('Hi');   </script>", false);
                            gvDatataTable.DataSource = null;
                            gvDatataTable.DataBind();

                            hdnShowSuccess.Value = "1";
                            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () {  });   </script>", false);

                        }
                        hdnShowSuccess.Value = "1";
                    }
                    lblAlertMsg.Text = "";
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({type: 'error', title: 'ERROR', text: 'Please add atleast one item', showConfirmButton: false, timer: 1500}); $('#ContentSection_hdnActiveTab').val('#addItem'); $('.nav a[href=\"#addItem\"]').tab('show'); $('.btnPrev').removeClass('hidden'); $('.btnNext').removeClass('hidden'); $('.btnCreate').addClass('hidden'); $('#loader').addClass('hidden'); });   </script>", false);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
            //ddlItemName.Enabled = true;
            //ddlSubCategory.Enabled = true;
            //ddlMainCateGory.Enabled = true;
        }


        private void sendEmails(int PrId) {
            PR_Master prmaster = pr_MasterController.FetchApprovePRDataByDeptIdAndPRId(int.Parse(ViewState["CompanyId"].ToString()), PrId);
            prmaster.PrDetails = pr_DetailController.FetchPR_DetailsByDeptIdAndPrId(PrId, int.Parse(ViewState["CompanyId"].ToString()));

            List<string> emails = companyLoginController.GetUserEmailsForApproval(1, prmaster.CategoryId, prmaster.PrDetails.Sum(pd => pd.ItemQuantity * pd.EstimatedAmount), int.Parse(ViewState["CompanyId"].ToString()), 5, 3).Distinct().ToList();
            StringBuilder message = new StringBuilder();

            if (emails.Count > 0) {
                //string to = string.Join(",", emails);
                string subject = "New Purchase Requests for Approval";
                message.AppendLine("Dear User,");
                message.AppendLine("<br>");
                message.AppendLine("<br>");
                message.AppendLine("You have new Purchase requests pending for approval. Please login to your account at admin.ezbidlanka.lk and provide your approval to proceed further.");
                message.AppendLine("<br>");
                message.AppendLine("<br>");

                message.Append("    <div class=\"container border\">");
                message.Append("        <div class=\"row\">");
                message.Append("            <div class=\"col-12 text-center\" style=\"text-align: center;\">");
                message.Append("                <hr>");
                message.Append("                <h2>Purchase Request</h2>");
                message.Append("                <hr>");
                message.Append("            </div>");
                message.Append("        </div>");
                message.Append("        <div class=\"row\">");
                message.Append("            <div class=\"col-sm-6\">");
                message.Append("                <span><b>PR Code : </b>" + prmaster.PrCode + "</span><br>");
                message.Append("                <span><b>Company : </b>" + prmaster.departmentName + "</span><br>");
                message.Append("                <span><b>Date of Request : </b>" + prmaster.DateOfRequest.ToString("dd/MM/yyyy hh:mm tt") + "</span><br>");
                message.Append("            </div>");
                message.Append("            <div class=\"col-sm-6\">");
                message.Append("                <span><b>Requester : </b>" + prmaster.RequestedBy + "</span><br>");
                message.Append("                <span><b>Quotation For : </b>" + prmaster.QuotationFor + "</span><br>");
                message.Append("            </div>");
                message.Append("        </div>");
                message.Append("        <div class=\"row\">");
                message.Append("            <div class=\"col-md-12\">");
                message.Append("                <br>");
                message.Append("                <div class=\"table-responsive\" style=\"width: 100%\">");
                message.Append("                    <div>");
                message.Append("                        <table class=\"table tablegv\"  style=\"width: 100%; border: 1px solid black; border-collapse: collapse;\">");
                message.Append("                            <tbody>");
                message.Append("                                <tr style=\"color:White;background-color:#858585;\">");
                message.Append("                                    <th style=\"border: 1px solid black;\" scope=\"col\">Item Name</th>");
                message.Append("                                    <th style=\"border: 1px solid black;\" scope=\"col\">Item Description</th>");
                message.Append("                                    <th style=\"border: 1px solid black;\" scope=\"col\">Remarks</th>");
                message.Append("                                    <th style=\"border: 1px solid black;\" scope=\"col\">Quantity</th>");
                message.Append("                                    <th style=\"border: 1px solid black;\" scope=\"col\">Estimated Unit Price</th>");
                message.Append("                                    <th style=\"border: 1px solid black;\" scope=\"col\">Estimated Total Amount</th>");
                message.Append("                                    <th style=\"border: 1px solid black;\" scope=\"col\">Replacement</th>");
                message.Append("                                </tr>");

                for (int i = 0; i < prmaster.PrDetails.Count; i++) {


                    message.Append("                                <tr style=\"background-color:#FAFAFA;\">");
                    message.Append("                                    <td style=\"border: 1px solid black; text-align: center;\">" + prmaster.PrDetails[i].ItemName + "</td>");
                    message.Append("                                    <td style=\"border: 1px solid black; text-align: justify;\">" + prmaster.PrDetails[i].ItemDescription + "</td>");
                    message.Append("                                    <td style=\"border: 1px solid black; text-align: justify;\">" + prmaster.PrDetails[i].Purpose + " </td>");
                    message.Append("                                    <td style=\"border: 1px solid black; text-align: center;\">" + prmaster.PrDetails[i].ItemQuantity.ToString("N0") + "</td>");
                    message.Append("                                    <td style=\"border: 1px solid black; text-align: right;\">" + prmaster.PrDetails[i].EstimatedAmount.ToString("N2") + "</td>");
                    message.Append("                                    <td style=\"border: 1px solid black; text-align: right;\">" + (prmaster.PrDetails[i].ItemQuantity * prmaster.PrDetails[i].EstimatedAmount).ToString("N2") + "</td>");
                    if (prmaster.PrDetails[i].Replacement == 1)
                        message.Append("                                    <td style=\"border: 1px solid black; text-align: center;\">Yes</td>");
                    else
                        message.Append("                                    <td style=\"border: 1px solid black; text-align: center;\">No</td>");
                    message.Append("                                </tr>");

                }
                message.Append("                            </tbody>");
                message.Append("                        </table>");
                message.Append("                    </div>");
                message.Append("                </div>");
                message.Append("            </div>");
                message.Append("        </div>");
                message.AppendLine("<br>");
                message.AppendLine("<br>");
                message.Append("        <div class=\"row\">");
                message.Append("            <div class=\"col-md-12\">");
                message.Append("                <span><b>Terms and Conditions : </b></span><br>");
                message.Append("                " + prmaster.BidTermsAndConditions + "");
                message.Append("            </div>");
                message.Append("        </div>");
                message.Append("    </div>");

                message.AppendLine("<br>");
                message.AppendLine("<hr>");
                message.AppendLine("<br>");
                message.AppendLine("<br>");
                message.AppendLine("Thanks and Regards,");
                message.AppendLine("<br>");
                message.AppendLine("Team EzBidLanka.");
                message.AppendLine("");

                EmailGenerator.SendEmail(emails, subject, message.ToString(), true);
            }
        }


        public class Name {
            public int value;
            public string label;
        }

        //[WebMethod]
        //public static List<Name> LoadItemNames(string input)
        //{
        //    CompnayPurchaseRequestNote cp = new CompnayPurchaseRequestNote();
        //    AddItemController addItemController = ControllerFactory.CreateAddItemController();

        //    List<AddItem> additems = addItemController.SearchedItemName(int.Parse(ViewState["CategoryId"].ToString()), SubCatergoryId, int.Parse(ViewState["CompanyId"].ToString()), input);      //CategoryId and SubCategoryId currently not in use

        //    List<Name> itemNameclz = new List<Name>();

        //    foreach (var item in additems)
        //    {
        //        Name n = new Name();
        //        n.value = item.ItemId;
        //        n.label = item.ItemName;

        //        itemNameclz.Add(n);
        //    }


        //    return (itemNameclz);
        //}

        //[WebMethod]
        //public static List<List<string>> LoadItemNamess(string input)
        //{


        //    CompnayPurchaseRequestNote cp = new CompnayPurchaseRequestNote();
        //    AddItemController addItemController = ControllerFactory.CreateAddItemController();
        //    List<AddItem> additems = addItemController.SearchedItemName(CategoryId, SubCatergoryId, int.Parse(ViewState["CompanyId"].ToString()), input);

        //    List<List<string>> itemNameclz = new List<List<string>>();

        //    foreach (var item in additems)
        //    {
        //        List<string> l = new List<string>();
        //        l.Add(item.ItemId.ToString());
        //        l.Add(item.ItemName);
        //        itemNameclz.Add(l);
        //    }

        //    input = input.Replace(" ", string.Empty);

        //    return (itemNameclz);
        //}

        //----------------Dynamically Load To grid view PR Detail Data
        protected void btnAdd_Click(object sender, EventArgs e) {
            try {
                if (txtQty.Text != "" && hdnCategoryId.Value != "" && hdnSubCategoryId.Value != "" && hdnItemId.Value != "" && txtEstimatedAmount.Text != "") {
                    if (btnAdd.Text == "Add Item") {
                        lblAlertMsg.Text = "";

                        //For maintain value of text box
                        //Tes1ItemTextBoxValue = Request.Form.GetValues("Test1TextBox");
                        //Tes2ItemTextBoxValue = Request.Form.GetValues("Test2TextBox");
                        //Tes3ItemTextBoxValue = Request.Form.GetValues("Test3TextBox");
                        //CreateTable(Tes1ItemTextBoxValue, Tes2ItemTextBoxValue);

                        string folderFilePath = string.Empty;
                        //if (fileUpload1.HasFile)
                        //{
                        int PrId = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString()));
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

                        //if (ddlItemName.Text != "")
                        //{
                        //    ItemIdFilterd = addItemController.GetIdByItemName(int.Parse(ViewState["CompanyId"].ToString()), ddlItemName.Text.Trim());
                        //}

                        //if (listToBind.FindAll(i => i.ItemId == (ItemIdFilterd)).Count == 0)
                        //{
                        //List<TempPrFileUpload> _tempPrFileUpload = tempPrFileUploadController.GetTempPrFiles(int.Parse(hdnItemId.Value), PrId);
                        //List<TempPR_FileUploadReplacement> _tempPR_FileUploadReplacement = tempPR_FileUploadReplacementController.GetTempPrFiles(int.Parse(hdnItemId.Value), PrId);
                        string[] arr = hdnField.Value.Split(',');
                        var list = arr.ToList();

                        list.RemoveAll(o => string.IsNullOrWhiteSpace(o));
                        List<ArrayItems> arraList = new List<ArrayItems>();
                        if (list != null || arr[1] != "") {
                            for (int i = 0; i < list.Count; i += 2) {
                                ArrayItems arrayItems = new ArrayItems();
                                arrayItems.Metirial = list[i];
                                arrayItems.Description = list[i + 1];
                                arraList.Add(arrayItems);
                            }
                        }
                        tempBOMController.DeleteBOMByPrId(PrId, int.Parse(ViewState["CompanyId"].ToString()), int.Parse(hdnItemId.Value));
                        for (int i = 0; i < arraList.Count(); i++) {
                            tempBOMController.SaveTempBOM(int.Parse(ViewState["CompanyId"].ToString()), PrId, int.Parse(hdnItemId.Value), i + 1, arraList[i].Metirial, arraList[i].Description);
                        }

                        HttpFileCollection uploads = HttpContext.Current.Request.Files;
                        for (int i = 0; i < uploads.Count; i++) {
                            if (uploads.AllKeys[i] == "files[]") {
                                HttpPostedFile postedFile = uploads[i];

                                string CreateFileName = PrId + "_" + int.Parse(hdnItemId.Value) + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0) {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01))) {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01));
                                    }


                                    //if (_tempPrFileUpload.Count == 0)
                                    //{
                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPurchaseRequestFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPrFileUploadController.SaveTempImageUpload(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(hdnItemId.Value), PrId, folderFilePath, FileName);
                                }
                                //}
                            }
                            if (uploads.AllKeys[i] == "fileReplace[]") {
                                HttpPostedFile postedFile = uploads[i];

                                string CreateFileName = PrId + "_" + int.Parse(hdnItemId.Value) + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0) {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01))) {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01));
                                    }

                                    //if (_tempPR_FileUploadReplacement.Count == 0)
                                    //{
                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPrReplacementFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPR_FileUploadReplacementController.SaveTempImageUpload(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(hdnItemId.Value), PrId, folderFilePath, FileName);
                                    //}
                                }
                            }


                            if (uploads.AllKeys[i] == "supportivefiles[]") {
                                HttpPostedFile postedFile = uploads[i];

                                string CreateFileName = PrId + "_" + int.Parse(hdnItemId.Value) + "_" + (i + 1).ToString();
                                string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                string FileName = Path.GetFileName(postedFile.FileName);
                                string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                if (postedFile.ContentLength > 0) {
                                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01))) {
                                        System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01));
                                    }


                                    //if (_tempPrFileUpload.Count == 0)
                                    //{
                                    postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01));
                                    folderFilePath = "~/TempPrSupportiveFiles/" + PrId + "/" + filename01;
                                    int saveFilePath = tempPR_SupportiveDocumentController.SaveTempSupportiveUpload(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(hdnItemId.Value), PrId, folderFilePath, FileName);
                                }
                                //}
                            }

                        }
                        lblAlertMsg.Text = "";
                        // }
                        //else
                        // {
                        //    lblAlertMsg.Text = "This item is already existing in the current PR.";
                        // }
                        List<TempDataSet> listData = new List<TempDataSet>();
                        TempDataSet dataSet = new TempDataSet();
                        dataSet.MainCategoryId = int.Parse(hdnCategoryId.Value);
                        dataSet.MainCategoryName = hdnCategoryName.Value;
                        dataSet.SubCategoryId = int.Parse(hdnSubCategoryId.Value);
                        dataSet.SubcategoryName = hdnSubCategoryName.Value;
                        dataSet.ItemId = int.Parse(hdnItemId.Value);
                        dataSet.ItemName = hdnItemName.Value;
                        dataSet.ItemQuantity = decimal.Parse(txtQty.Text);
                        dataSet.ItemDescription = txtDescription.Text;
                        dataSet.Purpose = txtPurpose.Text;
                        dataSet.EstimatedAmount = decimal.Parse(txtEstimatedAmount.Text == "" ? "0" : txtEstimatedAmount.Text);
                        dataSet.measurementId = int.Parse(ddlMeasurement.SelectedValue);
                        dataSet.IsNew = hdnCategoryId.Value == "0" ? 1 : 0;
                        if (rdoEnable.Checked) {
                            dataSet.ReplacementName = "Yes";
                            dataSet.ReplacementId = 1;
                        }
                        if (rdoDisable.Checked) {
                            dataSet.ReplacementName = "No";
                            dataSet.ReplacementId = 0;
                        }

                        List<TempDataSet> listToBind;
                        if (ViewState["ListToBind"] == null) {
                            listToBind = new List<TempDataSet>();
                        }
                        else {
                            listToBind = new JavaScriptSerializer().Deserialize<List<TempDataSet>>(ViewState["ListToBind"].ToString());
                        }

                        if (listToBind.FindAll(i => i.ItemId == dataSet.ItemId || i.ItemName == dataSet.ItemName).Count == 0) {
                            listToBind.Add(dataSet);
                            lblAlertMsg.Text = "";
                        }
                        else {
                            lblAlertMsg.Text = "This item is already existing in the current PR.";
                        }

                        Session["FinalListPRCreate"] = listToBind;

                        gvDatataTable.DataSource = ((List<TempDataSet>)Session["FinalListPRCreate"]).ToList();
                        gvDatataTable.DataBind();

                        txtPurpose.Text = "";
                        txtDescription.Text = "";
                        txtEstimatedAmount.Text = "";
                        rdoDisable.Checked = true;
                        txtQty.Text = "";
                        ddlMeasurement.Items.Clear();


                        //txtCategory.Text = "";
                        hdnCategoryName.Value = "";
                        hdnCategoryId.Value = "";

                        //txtSubCategory.Text = "";
                        hdnSubCategoryName.Value = "";
                        hdnSubCategoryId.Value = "";

                        txtItem.Text = "";
                        hdnItemName.Value = "";
                        hdnItemId.Value = "";


                        hdnField.Value = "";
                    }
                    if (btnAdd.Text == "Update Item") {
                        lblAlertMsg.Text = "";
                        string folderFilePath = string.Empty;
                        //if (fileUpload1.HasFile)
                        //{
                        int PrId = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString()));
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
                        if (list != null || arr[1] != "") {
                            for (int i = 0; i < list.Count; i += 2) {
                                ArrayItems arrayItems = new ArrayItems();
                                arrayItems.Metirial = list[i];
                                arrayItems.Description = list[i + 1];
                                arraList.Add(arrayItems);
                            }
                        }
                        //hdnItemId.Value = hdnItemId.Value.Replace(",", "");

                        tempBOMController.DeleteBOMByPrId(PrId, int.Parse(ViewState["CompanyId"].ToString()), int.Parse(hdnItemId.Value));
                        for (int i = 0; i < arraList.Count(); i++) {
                            tempBOMController.SaveTempBOM(int.Parse(ViewState["CompanyId"].ToString()), PrId, int.Parse(hdnItemId.Value), i + 1, arraList[i].Metirial, arraList[i].Description);
                        }

                        var listToBind = new JavaScriptSerializer().Deserialize<List<TempDataSet>>(ViewState["ListToBind"].ToString());



                        listToBind.RemoveAll(x => x.MainCategoryId == int.Parse(hdnCategoryId.Value) && x.SubCategoryId == int.Parse(hdnSubCategoryId.Value) && x.ItemId == int.Parse(hdnItemId.Value));

                        if (listToBind.FindAll(i => i.ItemId == int.Parse(hdnItemId.Value)).Count == 0) {
                            HttpFileCollection uploads = HttpContext.Current.Request.Files;
                            for (int i = 0; i < uploads.Count; i++) {
                                if (uploads.AllKeys[i] == "files[]") {
                                    HttpPostedFile postedFile = uploads[i];
                                    List<TempPrFileUpload> _TempPrFileUpload = new List<TempPrFileUpload>();

                                    _TempPrFileUpload = tempPrFileUploadController.GetPrUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), PrId, int.Parse(ViewState["ItemId"].ToString()));

                                    int maxNumber = 0;

                                    foreach (var item in _TempPrFileUpload) {
                                        int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                        if (CalNumber > maxNumber)
                                            maxNumber = CalNumber;
                                    }

                                    string CreateFileName = PrId + "_" + int.Parse(hdnItemId.Value) + "_" + (i + 1).ToString();
                                    string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                    string FileName = Path.GetFileName(postedFile.FileName);
                                    string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                    if (postedFile.ContentLength > 0) {
                                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01))) {
                                            System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01));
                                        }

                                        postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPurchaseRequestFiles/" + PrId + "/" + filename01));
                                        folderFilePath = "~/TempPurchaseRequestFiles/" + PrId + "/" + filename01;
                                        int saveFilePath = tempPrFileUploadController.SaveTempImageUpload(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(hdnItemId.Value), PrId, folderFilePath, FileName);
                                    }
                                }
                                if (uploads.AllKeys[i] == "fileReplace[]") {
                                    HttpPostedFile postedFile = uploads[i];

                                    List<TempPR_FileUploadReplacement> _TempPR_FileUploadReplacement = new List<TempPR_FileUploadReplacement>();

                                    _TempPR_FileUploadReplacement = tempPR_FileUploadReplacementController.GetPrUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), PrId, int.Parse(ViewState["ItemId"].ToString()));
                                    int maxNumber = 0;

                                    foreach (var item in _TempPR_FileUploadReplacement) {
                                        int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                        if (CalNumber > maxNumber)
                                            maxNumber = CalNumber;
                                    }

                                    string CreateFileName = PrId + "_" + int.Parse(hdnItemId.Value) + "_" + (maxNumber + 1).ToString();
                                    string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                    string FileName = Path.GetFileName(postedFile.FileName);
                                    string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                    if (postedFile.ContentLength > 0) {
                                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01))) {
                                            System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01));
                                        }

                                        postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrReplacementFiles/" + PrId + "/" + filename01));
                                        folderFilePath = "~/TempPrReplacementFiles/" + PrId + "/" + filename01;
                                        int saveFilePath = tempPR_FileUploadReplacementController.SaveTempImageUpload(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(hdnItemId.Value), PrId, folderFilePath, FileName);
                                    }
                                }

                                if (uploads.AllKeys[i] == "supportivefiles[]") {
                                    HttpPostedFile postedFile = uploads[i];
                                    List<TempPR_SupportiveDocument> tempPR_SupportiveDocument = new List<TempPR_SupportiveDocument>();

                                    tempPR_SupportiveDocument = tempPR_SupportiveDocumentController.GetPrSupporiveUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), PrId, int.Parse(ViewState["ItemId"].ToString()));

                                    int maxNumber = 0;

                                    foreach (var item in tempPR_SupportiveDocument) {
                                        int CalNumber = int.Parse(item.FilePath.Split('.').First().Split('/').Last().Split('_').Last());
                                        if (CalNumber > maxNumber)
                                            maxNumber = CalNumber;
                                    }

                                    string CreateFileName = PrId + "_" + int.Parse(hdnItemId.Value) + "_" + (i + 1).ToString();
                                    string UploadedFileName = CreateFileName.Replace(" ", string.Empty);
                                    string FileName = Path.GetFileName(postedFile.FileName);
                                    string filename01 = UploadedFileName + "." + FileName.Split('.').Last();

                                    if (postedFile.ContentLength > 0) {
                                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01))) {
                                            System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01));
                                        }

                                        postedFile.SaveAs(HttpContext.Current.Server.MapPath("~/TempPrSupportiveFiles/" + PrId + "/" + filename01));
                                        folderFilePath = "~/TempPrSupportiveFiles/" + PrId + "/" + filename01;
                                        int saveFilePath = tempPR_SupportiveDocumentController.SaveTempSupportiveUpload(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(hdnItemId.Value), PrId, folderFilePath, FileName);
                                    }
                                }
                            }
                            lblAlertMsg.Text = "";
                        }
                        else {
                            lblAlertMsg.Text = "This item is already existing in the current PR.";
                        }

                        List<TempDataSet> listData = new List<TempDataSet>();
                        TempDataSet dataSet = new TempDataSet();
                        dataSet.MainCategoryId = int.Parse(hdnCategoryId.Value);
                        dataSet.MainCategoryName = hdnCategoryName.Value;
                        dataSet.SubCategoryId = int.Parse(hdnSubCategoryId.Value);
                        dataSet.SubcategoryName = hdnSubCategoryName.Value;
                        dataSet.ItemId = int.Parse(hdnItemId.Value);
                        dataSet.ItemName = hdnItemName.Value;
                        dataSet.ItemQuantity = decimal.Parse(txtQty.Text);
                        dataSet.ItemDescription = txtDescription.Text;
                        dataSet.Purpose = txtPurpose.Text;
                        dataSet.EstimatedAmount = decimal.Parse(txtEstimatedAmount.Text == "" ? "0" : txtEstimatedAmount.Text);
                        dataSet.measurementId = int.Parse(ddlMeasurement.SelectedValue);
                        dataSet.IsNew = hdnCategoryId.Value == "0" ? 1 : 0;
                        if (rdoEnable.Checked) {
                            dataSet.ReplacementName = "Yes";
                            dataSet.ReplacementId = 1;
                        }
                        if (rdoDisable.Checked) {
                            dataSet.ReplacementName = "No";
                            dataSet.ReplacementId = 0;
                        }
                        if (listToBind.FindAll(i => i.ItemId == dataSet.ItemId || i.ItemName == dataSet.ItemName).Count == 0) {
                            listToBind.Add(dataSet);
                            lblAlertMsg.Text = "Item details updated successfully";
                            lblAlertMsg.Attributes.CssStyle.Add("color", "Green");

                        }
                        else {
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
                        hdnCategoryId.Value = "";


                        //txtCategory.Text = "";
                        hdnCategoryName.Value = "";
                        hdnCategoryId.Value = "";

                        //txtSubCategory.Text = "";
                        hdnSubCategoryName.Value = "";
                        hdnSubCategoryId.Value = "";

                        txtItem.Text = "";
                        hdnItemName.Value = "";
                        hdnItemId.Value = "";
                        //ddlMainCateGory.SelectedIndex = 0;
                        //ddlItemName.Enabled = true;
                        //btnRefreshItem.Enabled = true;
                        //ddlSubCategory.Enabled = true;
                        //btnRefreshSubCategory.Enabled = true;
                        //ddlMainCateGory.Enabled = true;
                        //btnRefreshCategory.Enabled = true;
                        btnAdd.Text = "Add Item";
                        gvPrUploadedFiles.DataSource = null;
                        gvPrUploadedFiles.DataBind();
                        gvRepacementImages.DataSource = null;
                        gvRepacementImages.DataBind();
                        gvSupporiveFiles.DataSource = null;
                        gvSupporiveFiles.DataBind();
                        //ddlSubCategory.DataSource = null;
                        //ddlSubCategory.DataBind();

                        //ddlItemName.DataSource = null;
                        //ddlItemName.DataBind();
                        //txtItem.Text = "";
                        hdnItemId.Value = "";
                        ddlMeasurement.Items.Clear();

                        btnSearch.Enabled = true;
                        hdnField.Value = "";
                        //txtItem.Enabled = true;

                    }

                    itemNameVal.Visible = false;
                }
                else {
                    txtItem.Text = hdnItemName.Value;
                    itemNameVal.Visible = true;
                }



            }
            catch (Exception ex) {
                throw ex;
            }
        }

        //-----------OriginalDirectory (Not in use)
        public void TargetFileCreate() {
            var source = Server.MapPath("TempPurchaseRequestFiles");
            var target = Server.MapPath("PurchaseRequestFiles");
            foreach (var dir in Directory.GetDirectories(source)) {
                var targetFolder = System.IO.Path.Combine(target, "2");
                if (!Directory.Exists(targetFolder)) {
                    Directory.CreateDirectory(targetFolder);
                }
            }
        }

        //-----------OriginalDirectory
        private bool CopyFolderContents(string SourcePath, string DestinationPath) {
            SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
            DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";
            try {
                if (Directory.Exists(SourcePath)) {
                    if (Directory.Exists(DestinationPath) == false) {
                        Directory.CreateDirectory(DestinationPath);
                    }

                    foreach (string files in Directory.GetFiles(SourcePath)) {
                        FileInfo fileInfo = new FileInfo(files);
                        fileInfo.CopyTo(string.Format(@"{0}\{1}", DestinationPath, fileInfo.Name), true);
                    }

                    foreach (string drs in Directory.GetDirectories(SourcePath)) {
                        DirectoryInfo directoryInfo = new DirectoryInfo(drs);
                        if (CopyFolderContents(drs, DestinationPath + directoryInfo.Name) == false) {
                            return false;
                        }
                    }
                    //---Delete From Temporary Files
                    System.IO.Directory.Delete(SourcePath, true);
                }
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }

        //------------------Check Create Directory Existing or Not 
        private int CreateDirectoryIfNotExists(string NewDirectory) {
            try {
                int returnType = 0;
                // Checking the existance of directory
                if (!Directory.Exists(NewDirectory)) {
                    //delete
                    //If No any such directory then creates the new one
                    Directory.CreateDirectory(NewDirectory);
                    returnType = 1;
                }
                else {
                    //Label1.Text = "Directory Exist";
                    returnType = 0;
                }
                return returnType;
            }
            catch (IOException _err) {
                throw _err;
                //Label1.Text = _err.Message; ;
            }
        }

        protected void confirmation_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }

        private void DisplayMessage(string message, bool isError) {
            msg.Visible = true;
            if (isError) {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }

            lbMessage.Text = message;

        }

        private void ClearFields() {
            txtPurpose.Text = "";
            txtDescription.Text = "";
            rdoDisable.Checked = true;
            txtQty.Text = "";
            txtEstimatedAmount.Text = "";
            //LoadDDLMainCatregory();
            //ddlMainCateGory.SelectedIndex = 0;
            //ddlSubCategory.Items.Clear();
            //ddlItemName.Items.Clear();

            //ddlItemName.Enabled = true;
            //btnRefreshItem.Enabled = true;
            //ddlSubCategory.Enabled = true;
            //btnRefreshSubCategory.Enabled = true;
            //ddlMainCateGory.Enabled = true;
            //btnRefreshCategory.Enabled = true;

            btnAdd.Text = "Add Item";
            gvPrUploadedFiles.DataSource = null;
            gvPrUploadedFiles.DataBind();
            gvRepacementImages.DataSource = null;
            gvRepacementImages.DataBind();
            gvSupporiveFiles.DataSource = null;
            gvSupporiveFiles.DataBind();
            //ddlSubCategory.DataSource = null;
            //ddlSubCategory.DataBind();

            //ddlItemName.DataSource = null;
            //ddlItemName.DataBind();
            //txtItem.Text = "";


            //txtCategory.Text = "";
            hdnCategoryId.Value = "";
            hdnCategoryName.Value = "";
            hdnSubCategoryId.Value = "";
            hdnSubCategoryName.Value = "";
            txtItem.Text = "";
            hdnItemName.Value = "";
            hdnItemId.Value = "";

            //txtSubCategory.Text = "";
            hdnSubCategoryId.Value = "";

            btnSearch.Enabled = true;

            ddlMeasurement.Items.Clear();


            txtQuotationFor.Text = "";
            txtRef.Text = "";
            txtRequestedBy.Text = "";
            txtEstimatedAmount.Text = "";
            txtTerms.Text = "";
            //DateTimeRequested.Text = "";

            gvDatataTable.DataSource = null;
            gvDatataTable.DataBind();

        }

        // Class Image Files 
        public class ImageFiles {
            public string Name { get; set; }
            public byte[] Data { get; set; }
        }

        protected void lbtnViewBOM_Click(object sender, EventArgs e) {
            try {
                int PrId = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString()));
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                ViewState["ItemId"] = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);
                List<TempBOM> tempBOMList = tempBOMController.GetBOMListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), PrId, int.Parse(ViewState["ItemId"].ToString()));
                gvTempBoms.DataSource = tempBOMList;
                gvTempBoms.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalViewBom').modal('show'); });   </script>", false);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#myModalViewBom').modal('show');   </script>", false);
            }
            catch (Exception ex) {
                throw ex;
            }

        }
        protected void btnexisting_Click(object sender, EventArgs e) {
            if (hdnItemId.Value != "") {
                int itemID = int.Parse(hdnItemId.Value);
                if (itemID != 0) {
                    List<TempBOM> tempBOMList = tempBOMController.GetItemspecification(itemID);
                    gvSpecificationBoms.DataSource = tempBOMList;
                    gvSpecificationBoms.DataBind();
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#specification').modal('show');   </script>", false);
                }
                else {
                    DisplayMessage("Please select Item", true);
                }
            }

        }


        protected void lbtnViewUploadPhotos_Click(object sender, EventArgs e) {
            try {
                int PrId = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString()));
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                ViewState["ItemId"] = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);

                List<TempPrFileUpload> GetTempPrFiles = tempPrFileUploadController.GetTempPrFiles(int.Parse(ViewState["ItemId"].ToString()), PrId);
                gvUploadedPhotos.DataSource = GetTempPrFiles;
                gvUploadedPhotos.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>   $('#myModalUploadedPhotos').modal('show');   </script>", false);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#myModalUploadedPhotos').modal('show');   </script>", false);

                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalUploadedPhotos').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void btnViewzReplacementPhotos_Click(object sender, EventArgs e) {
            try {
                int PrId = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString()));
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                ViewState["ItemId"] = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);

                List<TempPR_FileUploadReplacement> GetTempReplacementFiles = tempPR_FileUploadReplacementController.GetPrUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), PrId, int.Parse(ViewState["ItemId"].ToString()));
                gvReplacementPhotos.DataSource = GetTempReplacementFiles;
                gvReplacementPhotos.DataBind();
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModalReplacementPhotos').modal('show'); });   </script>", false);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>   $('#myModalReplacementPhotos').modal('show');   </script>", false);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>   $('#myModalReplacementPhotos').modal('show');   </script>", false);

            }
            catch (Exception) {


            }
        }
        //---------------Edit Dynamic Table Data
        protected void btnDeleteItem_Click(object sender, EventArgs e) {
            try {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

                int ItemId = int.Parse(hdnItemId.Value);
                if (ItemId != 0) {

                    int PrId = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString()));

                    int deleteBom = tempBOMController.DeleteBOMByPrId(PrId, int.Parse(ViewState["CompanyId"].ToString()), ItemId);


                    var SourcePath = Server.MapPath("TempPrReplacementFiles/" + PrId);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    List<TempPR_FileUploadReplacement> _tempPR_FileUploadReplacement = tempPR_FileUploadReplacementController.GetPrUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), PrId, ItemId).ToList();

                    foreach (var item in _tempPR_FileUploadReplacement) {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in di.GetFiles()) {
                            if (file.Name == getNAme[3]) {
                                File.Delete(Server.MapPath("TempPrReplacementFiles/" + PrId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempReplacementImage = tempPR_FileUploadReplacementController.DeleteTempPrDetailFileUpload(PrId, int.Parse(ViewState["CompanyId"].ToString()), ItemId);

                    var SourcePathFileUpload = Server.MapPath("TempPurchaseRequestFiles/" + PrId);
                    System.IO.DirectoryInfo di1 = new DirectoryInfo(SourcePath);

                    List<TempPrFileUpload> TempPrFileUpload = tempPrFileUploadController.GetPrUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), PrId, ItemId).ToList();

                    foreach (var item in _tempPR_FileUploadReplacement) {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in di1.GetFiles()) {
                            if (file.Name == getNAme[3]) {
                                File.Delete(Server.MapPath("TempPurchaseRequestFiles/" + PrId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempFileUpload = tempPrFileUploadController.DeleteTempPrDetailFileUpload(PrId, int.Parse(ViewState["CompanyId"].ToString()), ItemId);


                    var SourcePathSupportive = Server.MapPath("TempPrSupportiveFiles/" + PrId);
                    System.IO.DirectoryInfo dis = new DirectoryInfo(SourcePath);

                    List<TempPR_SupportiveDocument> _tempPR_SupportiveDocument = tempPR_SupportiveDocumentController.GetPrSupporiveUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), PrId, ItemId).ToList();

                    foreach (var item in _tempPR_SupportiveDocument) {
                        string[] getNAme = Regex.Split(item.FilePath, "/");

                        foreach (FileInfo file in dis.GetFiles()) {
                            if (file.Name == getNAme[3]) {
                                File.Delete(Server.MapPath("TempPrSupportiveFiles/" + PrId + "/" + getNAme[3]));
                            }
                        }
                    }

                    int deleteTempSupportiveDocument = tempPR_SupportiveDocumentController.DeleteTempPrDetailSupporiveUpload(PrId, int.Parse(ViewState["CompanyId"].ToString()), ItemId);


                    var listToBind = new JavaScriptSerializer().Deserialize<List<TempDataSet>>(ViewState["ListToBind"].ToString());

                    listToBind.RemoveAll(i => (i.ItemId == ItemId));
                    Session["FinalListPRCreate"] = listToBind;

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                    //DisplayMessage("Item details have been deleted successfully", false);

                    gvDatataTable.DataSource = ((List<TempDataSet>)Session["FinalListPRCreate"]).ToList().OrderBy(i => i.ItemId);
                    gvDatataTable.DataBind();
                }

                else {
                    DisplayMessage("Please select Item to Delete!!", true);
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        //---------------Edit Dynamic Table Data
        protected void btnEditItem_Click(object sender, ImageClickEventArgs e) {
            try {


                var BomStringList = new List<string>();
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                ViewState["PrId"] = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString()));
                ViewState["ItemId"] = int.Parse(gvDatataTable.Rows[x].Cells[4].Text);
                ViewState["ItemName"] = gvDatataTable.Rows[x].Cells[5].Text;

                btnAdd.Text = "Update Item";
                List<TempDataSet> tempDataSetUpdate = new List<TempDataSet>();
                var listToBind = new JavaScriptSerializer().Deserialize<List<TempDataSet>>(ViewState["ListToBind"].ToString());
                tempDataSetUpdate = listToBind.Where(z => z.ItemId == int.Parse(ViewState["ItemId"].ToString())).ToList();

                var TempBOMlistByPrId = tempBOMController.GetBOMListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["ItemId"].ToString()));
                if (TempBOMlistByPrId.Count != 0) {
                    foreach (var item in TempBOMlistByPrId) {
                        BomStringList.Add(item.Meterial + "-" + item.Description);
                    }
                    string json = new JavaScriptSerializer().Serialize(BomStringList);
                    ViewState["BomStringList"] = BomStringList;
                    HiddenField2.Value = json;
                }
                ViewState["TempBOMlistByPrId"] = new JavaScriptSerializer().Serialize(TempBOMlistByPrId);
                TempDataSet tempDataSet = new TempDataSet();
                foreach (var item in tempDataSetUpdate) {
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
                    tempDataSet.measurementId = item.measurementId;
                }

                if (listToBind.Count() > 0) {
                    //LoadDDLMainCatregory();
                    //ddlSubCategory.DataSource = itemSubCategoryController.FetchItemSubCategoryByCategoryId(tempDataSet.MainCategoryId, int.Parse(Session["int.Parse(ViewState["CompanyId"].ToString())"].ToString()));
                    //ddlSubCategory.DataValueField = "SubCategoryId";
                    //ddlSubCategory.DataTextField = "SubCategoryName";
                    //ddlSubCategory.DataBind();
                    //ddlSubCategory.Items.Insert(0, new ListItem("Select Sub Category", ""));

                    List<AddItem> items = addItemController.FetchItemsByCategoriesForDropdown(tempDataSet.MainCategoryId, tempDataSet.SubCategoryId, int.Parse(ViewState["CompanyId"].ToString()));

                    //ddlItemName.Items.Insert(0, new ListItem("Select Item", ""));
                    //for (int i = 0; i < items.Count; i++)
                    //{
                    //    ddlItemName.Items.Add(new ListItem(items[i].ItemName, items[i].ItemId.ToString()));
                    //}


                    //txtItem.Text = "";

                    //ddlItemName.Enabled = false;
                    //btnRefreshItem.Enabled = false;
                    //ddlSubCategory.Enabled = false;
                    //btnRefreshSubCategory.Enabled = false;
                    //ddlMainCateGory.Enabled = false;
                    //btnRefreshCategory.Enabled = false;



                    hdnCategoryId.Value = tempDataSet.MainCategoryId.ToString();
                    hdnCategoryName.Value = tempDataSet.MainCategoryName;
                    //txtCategory.Text = tempDataSet.MainCategoryName;

                    hdnSubCategoryId.Value = tempDataSet.SubCategoryId.ToString();
                    hdnSubCategoryName.Value = tempDataSet.SubcategoryName;
                    //txtSubCategory.Text = tempDataSet.SubcategoryName;

                    hdnItemId.Value = tempDataSet.ItemId.ToString();
                    hdnItemName.Value = tempDataSet.ItemName;
                    txtItem.Text = tempDataSet.ItemName;
                    bindMeasurement();

                    txtQty.Text = tempDataSet.ItemQuantity.ToString();
                    txtDescription.Text = tempDataSet.ItemDescription;
                    ddlMeasurement.SelectedValue = tempDataSet.measurementId.ToString();
                    if (tempDataSet.ReplacementId == 1) {
                        rdoEnable.Enabled = true;
                        rdoEnable.Checked = true;
                        rdoDisable.Checked = false;
                    }
                    else {
                        rdoDisable.Enabled = true;
                        rdoDisable.Checked = true;
                        rdoEnable.Checked = false;
                    }
                    txtPurpose.Text = tempDataSet.Purpose;
                    txtEstimatedAmount.Text = tempDataSet.EstimatedAmount.ToString();


                    gvRepacementImages.DataSource = tempPR_FileUploadReplacementController.GetPrUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["ItemId"].ToString())).ToList();
                    gvRepacementImages.DataBind();
                    List<TempPrFileUpload> TempPrFileUpload = tempPrFileUploadController.GetPrUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["ItemId"].ToString())).ToList();
                    gvPrUploadedFiles.DataSource = TempPrFileUpload;
                    gvPrUploadedFiles.DataBind();

                    List<TempPR_SupportiveDocument> TempPR_SupportiveDocument = tempPR_SupportiveDocumentController.GetPrSupporiveUpoadFilesListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["ItemId"].ToString())).ToList();
                    gvSupporiveFiles.DataSource = TempPR_SupportiveDocument;
                    gvSupporiveFiles.DataBind();
                    btnSearch.Enabled = false;
                }

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void lbtnViewUploadImage_Click(object sender, EventArgs e) {

            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvPrUploadedFiles.Rows[x].Cells[2].Text;
                //System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
                //Response.Redirect(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "'); });   </script>", false);

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void lbtnViewReplacementImage_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvRepacementImages.Rows[x].Cells[2].Text;
                //System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "','_blank'); });   </script>", false);

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void lbtnDeleteUploadImage_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string prid = gvPrUploadedFiles.Rows[x].Cells[0].Text;
                string itemid = gvPrUploadedFiles.Rows[x].Cells[1].Text;
                string imagepath = gvPrUploadedFiles.Rows[x].Cells[2].Text;
                int deleteImages = tempPrFileUploadController.GetTempPrFilesTemp(int.Parse(itemid), int.Parse(prid), imagepath);

                if (deleteImages > 0) {
                    //lblImageDeletedMsg.Text = "Image deleted successfully";
                    //lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Green");
                    gvPrUploadedFiles.DataSource = tempPrFileUploadController.GetTempPrFiles(int.Parse(itemid), int.Parse(prid));
                    gvPrUploadedFiles.DataBind();

                    var SourcePath = Server.MapPath("TempPurchaseRequestFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles()) {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3]) {
                            File.Delete(Server.MapPath("TempPurchaseRequestFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else {
                    //lblImageDeletedMsg.Text = "Action unsuccessfull";
                    //lblImageDeletedMsg.Attributes.CssStyle.Add("color", "Red");
                    gvPrUploadedFiles.DataSource = tempPrFileUploadController.GetTempPrFiles(int.Parse(prid), int.Parse(itemid));
                    gvPrUploadedFiles.DataBind();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void lbtnDeleteReplacementImage_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string prid = tempBOMController.GetNextPrIdObj(int.Parse(ViewState["CompanyId"].ToString())).ToString();
                string itemid = gvRepacementImages.Rows[x].Cells[1].Text;
                string imagepath = gvRepacementImages.Rows[x].Cells[2].Text;
                int deleteTempReplacementImage = tempPR_FileUploadReplacementController.GetTempPrFilesTemp(int.Parse(itemid), int.Parse(prid), imagepath);

                if (deleteTempReplacementImage > 0) {
                    lblReplaceimageDelete.Text = "Image deleted successfully";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Green");
                    gvRepacementImages.DataSource = tempPR_FileUploadReplacementController.GetTempPrFiles(int.Parse(itemid), int.Parse(prid));
                    gvRepacementImages.DataBind();

                    var SourcePath = Server.MapPath("TempPrReplacementFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles()) {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3]) {
                            File.Delete(Server.MapPath("TempPrReplacementFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else {
                    lblReplaceimageDelete.Text = "Action unsuccessfull";
                    lblReplaceimageDelete.Attributes.CssStyle.Add("color", "Red");
                    gvRepacementImages.DataSource = tempPR_FileUploadReplacementController.GetTempPrFiles(int.Parse(prid), int.Parse(itemid));
                    gvRepacementImages.DataBind();
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public string getJsonBomStringListData() {
            return ViewState["BomStringList"].ToString();
        }


        public string getBOMData() {
            string data = "";
            List<TempBOM> _PR_BillOfMeterial = tempBOMController.GetBOMListByPrIdItemId(int.Parse(ViewState["CompanyId"].ToString()), int.Parse(ViewState["PrId"].ToString()), int.Parse(ViewState["ItemId"].ToString()));
            foreach (var item in _PR_BillOfMeterial) {
                string Meterial = item.Meterial;
                string Description = item.Description;
                data += "<tr><td>" + Meterial + "</td><td>" + Description + "</td></tr>";

            }
            return data;
        }

        [WebMethod]
        public static DetailsClass[] GetPRBomDetailsIds(string data) {
            List<DetailsClass> Detail = new List<DetailsClass>();

            if (HttpContext.Current.Session["CompanyId"] != null) {
                string jsonData = data;
                string PrId = string.Empty;
                string ItemId = string.Empty;

                if (jsonData != "") {
                    string[] value = jsonData.Split('-');
                    string val1 = value[0];
                    string val2 = value[1];
                    PrId = val1;
                    ItemId = val2;
                    TempBOMController pr_BillOfMeterialController = ControllerFactory.CreateTempBOMController();
                    List<TempBOM> pr_BillOfMeterial = pr_BillOfMeterialController.GetBOMListByPrIdItemId(int.Parse(HttpContext.Current.Session["CompanyId"].ToString()), int.Parse(PrId), int.Parse(ItemId));

                    foreach (var item in pr_BillOfMeterial) {
                        DetailsClass DataObj = new DetailsClass();
                        DataObj.Meterial = item.Meterial;
                        DataObj.Description = item.Description;
                        Detail.Add(DataObj);
                    }
                }
            }
            return Detail.ToArray();
        }

        protected void btnClear_Click(object sender, EventArgs e) {
            try {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

                clearFields();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        private void clearFields() {
            try {
                lblAlertMsg.Text = "";
                btnAdd.Text = "Add Item";
                txtPurpose.Text = "";
                txtDescription.Text = "";
                txtQty.Text = "";
                txtEstimatedAmount.Text = "";
                //LoadDDLMainCatregory();
                gvPrUploadedFiles.DataSource = null;
                gvPrUploadedFiles.DataBind();

                gvSupporiveFiles.DataSource = null;
                gvSupporiveFiles.DataBind();

                gvRepacementImages.DataSource = null;
                gvRepacementImages.DataBind();

                //txtCategory.Text = "";
                hdnCategoryName.Value = "";
                hdnCategoryId.Value = "";

                //txtSubCategory.Text = "";
                hdnSubCategoryName.Value = "";
                hdnSubCategoryId.Value = "";

                txtItem.Text = "";
                hdnItemName.Value = "";
                hdnItemId.Value = "";

                //txtItem.Text = "";
                hdnItemId.Value = "";
                btnSearch.Enabled = true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public class DetailsClass //Class for binding data
        {
            public string Meterial { get; set; }
            public string Description { get; set; }
        }

        public class ArrayItems {
            public string Metirial { get; set; }
            public string Description { get; set; }
        }

        protected void ddlPrType_SelectedIndexChanged(object sender, EventArgs e) {
            //try
            //{
            //    if (Session["int.Parse(ViewState["CompanyId"].ToString())"].ToString() == "1")
            //    {
            //        if (ddlPrType.SelectedValue == "1")
            //        {
            //            divJobNo.Visible = false;
            //            divVehicleNo.Visible = false;
            //            divMake.Visible = false;
            //            divModel.Visible = false;
            //        }
            //        else if (ddlPrType.SelectedValue == "2")
            //        {
            //            divJobNo.Visible = true;
            //            divVehicleNo.Visible = true;
            //            divMake.Visible = true;
            //            divModel.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        if (ddlPrType.SelectedValue == "4" || ddlPrType.SelectedValue == "3" || ddlPrType.SelectedValue == "5")
            //        {
            //            divJobNo.Visible = false;
            //            divVehicleNo.Visible = false;
            //            divMake.Visible = false;
            //            divModel.Visible = false;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        protected void lbtnViewUploadSupporiveDocument_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string filepath = gvSupporiveFiles.Rows[x].Cells[2].Text;
                //System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
                filepath = filepath.Replace("~/", string.Empty);
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('" + filepath + "','_blank'); });   </script>", false);

            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void lbtnDeleteSupportiveDocument_Click(object sender, EventArgs e) {
            try {
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                string prid = gvSupporiveFiles.Rows[x].Cells[0].Text;
                string itemid = gvSupporiveFiles.Rows[x].Cells[1].Text;
                string imagepath = gvSupporiveFiles.Rows[x].Cells[2].Text;
                int deleteSupporiveDocuments = tempPR_SupportiveDocumentController.GetTempPrSupportiveFilesTemp(int.Parse(itemid), int.Parse(prid), imagepath);

                if (deleteSupporiveDocuments > 0) {
                    gvSupporiveFiles.DataSource = tempPR_SupportiveDocumentController.GetTempPrSupportiveFiles(int.Parse(itemid), int.Parse(prid));
                    gvSupporiveFiles.DataBind();

                    var SourcePath = Server.MapPath("TempPrSupportiveFiles/" + prid);
                    System.IO.DirectoryInfo di = new DirectoryInfo(SourcePath);

                    foreach (FileInfo file in di.GetFiles()) {
                        string[] getNAme = Regex.Split(imagepath, "/");
                        if (file.Name == getNAme[3]) {
                            File.Delete(Server.MapPath("TempPrSupportiveFiles/" + prid + "/" + getNAme[3]));
                        }
                    }
                }
                else {
                    gvSupporiveFiles.DataSource = tempPR_SupportiveDocumentController.GetTempPrSupportiveFiles(int.Parse(prid), int.Parse(itemid));
                    gvSupporiveFiles.DataBind();
                }
            }
            catch (Exception ex) {

            }
        }

        private void bindMeasurement() {
            try {
                if(hdnCategoryId.Value =="" || hdnCategoryId.Value == "0") {
                    ddlMeasurement.DataSource = ControllerFactory.CreateMeasurementDetailController().GetAllMeasurementsOfCompany(int.Parse(Session["CompanyId"].ToString()));
                }
                else {
                    ddlMeasurement.DataSource = ControllerFactory.CreateMeasurementDetailController().GetMeasurementDetailsOfItem(int.Parse(hdnItemId.Value), int.Parse(Session["CompanyId"].ToString()));
                }
                ddlMeasurement.DataValueField = "DetailId";
                ddlMeasurement.DataTextField = "ShortCode";
                ddlMeasurement.DataBind();
            }
            catch (Exception) {

                throw;
            }
        }

        [WebMethod]
        public static IEnumerable<object> SearchItem(string text, int categoryId) {
            return ControllerFactory.CreateAddItemController().SearchItems(text, categoryId, int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Select(itms => new {
                CategoryId = itms.CategoryId,
                CategoryName = itms.CategoryName,
                SubCatergoryId = itms.SubCategoryId,
                SubCategoryName = itms.SubCategoryName,
                ItemId = itms.ItemId,
                ItemName = itms.ItemName
            });
        }

        protected void btnRefreshMeasurement_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

            txtItem.Text = hdnItemName.Value;
            bindMeasurement();
        }

        protected void btnItemSelected_Click(object sender, EventArgs e) {
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

            txtItem.Text = hdnItemName.Value;
            bindMeasurement();
        }

        //protected void btnRefreshCategory_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

        //    LoadDDLMainCatregory();
        //}

        //protected void btnRefreshSubCategory_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

        //    ddlMainCateGory_SelectedIndexChanged(null, null);
        //}

        //protected void btnRefreshItem_Click(object sender, EventArgs e)
        //{
        //    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () {  $('.category-cl').select2(); $('.sub-category-cl').select2(); $('.item-cl').select2(); $('.wizard .nav > li a[href=#addItem]').tab('show'); });   </script>", false);

        //    ddlSubCategory_SelectedIndexChanged(null, null);
        //}

    }
}