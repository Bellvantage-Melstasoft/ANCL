using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.Web.Services;
using System.IO;
using System.Globalization;
using System.Web.Hosting;
using System.Threading;
using System.Data;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using Newtonsoft.Json;
using BiddingSystem.ViewModels.CS;

namespace BiddingSystem
{
    public partial class ViewSubmittedForBidListing : System.Web.UI.Page
    {        
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PrControllerV2 PrMataterControllerV2 = ControllerFactory.CreatePrControllerV2();
        PR_DetailController PrDetailsController = ControllerFactory.CreatePR_DetailController();
        BiddingPlanController biddingPlanController = ControllerFactory.CreateBiddingPlanController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        BiddingController bidController = ControllerFactory.CreateBiddingController();
        Procument_Plan_Type_Controller procumentPlanTypeController = ControllerFactory.CreateProcument_Plan_Type_Controller();
        PR_DetailController prDetailController = ControllerFactory.CreatePR_DetailController();
        PR_SupportiveDocumentController prSupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        PR_BillOfMeterialController PR_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        PR_SupportiveDocumentController PR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        ItemCategoryOwnerController itemCategoryOwnerController = ControllerFactory.CreateItemCategoryOwnerController();
        BiddingItemController biddingItemController = ControllerFactory.CreateBiddingItemController();
        PRDetailsStatusLogController pRDetailsStatusLogController = ControllerFactory.CreatePRDetailsStatusLogController();
        static HttpContext current = HttpContext.Current;
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        protected void Page_Load(object sender, EventArgs e)
        {
            serializer.MaxJsonLength = Int32.MaxValue;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "viewSubmittedforBid";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewSubmittedForBidListing.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewSubmittedforBidLink";
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                if (int.Parse(Session["UserId"].ToString()) != 0)
                {
                    try
                    {
                        var PrId = int.Parse(Request.QueryString.Get("PrId"));
                        PrMasterV2 PrMaster = PrMataterControllerV2.GetPrSubmittedBid(PrId, int.Parse(Session["CompanyId"].ToString()));
                        ViewState["PrMaster"] = serializer.Serialize(PrMaster);
                        ViewState["PrId"] = PrMaster.PrId;
                        if (PrMaster != null)
                        {
                            lblWarehouse.Text = PrMaster.WarehouseName;
                            lblPRNo.Text = "PR-"+PrMaster.PrCode;
                            lblCreatedOn.Text = PrMaster.CreatedDate.ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                            lblCreatedBy.Text = PrMaster.CreatedByName;
                            lblRequestFor.Text = PrMaster.RequiredFor;
                            lblExpenseType.Text = (PrMaster.ExpenseType == 1) ? "Capital Expense" : "Operational Expense";
                            lblCategory.Text = PrMaster.PrCategoryName;
                            lblSubCategory.Text = PrMaster.PrSubCategoryName;
                            lblExpectedDate.Text = (PrMaster.ExpectedDate).ToString("dd-MM-yyyy");
                            lblBidCreatedBy.Text = PrMaster.Bids.Count > 0 ? PrMaster.Bids[0].CreatedUserName : "No UserName";
                            lblBidCreatedOn.Text = PrMaster.Bids.Count > 0 ? PrMaster.Bids[0].CreateDate.ToString("dd-MM-yyyy") : "No UserName";
                            lblPurchaseType.Text = (PrMaster.PurchaseType == 1) ? "Local" : "Import";
                            if (PrMaster.MrnCode != null)
                            {
                                divMrnReferenceCode.Visible = true;
                                lblMrnReferenceCode.Text = PrMaster.MrnCode.ToString() != "" ? "MRN-" + PrMaster.MrnCode.ToString() : "No";
                                ViewState["MrnId"] = PrMaster.MrnId;
                            }

                            gvBids.DataSource = PrMaster.Bids;
                            gvBids.DataBind();

                            LoadOfficersContact();  
                            InitializeContactOfficersDatatable();
                            LoadSuppliers();
                            GetSupplierBidEmail(PrMaster);
                            LoadTitleDropdown();

                            LoadAttachmentGridView(PrMaster);


                            List<Bidding> BiddingItems = bidController.FetchBidInfo(int.Parse(Request.QueryString.Get("PrId")));

                            List<int> bidIdList = new List<int>();
                            for (int i = 0; i < BiddingItems.Count; i++) {
                                int BidId = BiddingItems[i].BidId;
                                bidIdList.Add(BidId);
                            }

                            List<BiddingPlan> Bidingplan = biddingPlanController.GetBiddingPlanByIDPrint(bidIdList);
                            dvBiddingplanPrint.DataSource = Bidingplan;
                            dvBiddingplanPrint.DataBind();

                            List<PrDetailsV2> prdetails = PrMaster.PrDetails;
                            List<int> prdids = new List<int>();

                            for (int i = 0; i < prdetails.Count; i++) {
                                int prdId = prdetails[i].PrdId;
                                prdids.Add(prdId);
                            }

                            List<PR_BillOfMeterial> ItemSpecs = ControllerFactory.CreatePR_BillOfMeterialController().GetListForPrint(prdids);
                            GvItemSpec.DataSource = ItemSpecs;
                            GvItemSpec.DataBind();
                        }

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void LoadAttachmentGridView(PrMasterV2 prMaster)
        {
            List<PrBomV2> listItemSpecification = new List<PrBomV2>();
            List<PrReplacementFileUploadV2> listReplacementImages = new List<PrReplacementFileUploadV2>();
            List<PrFileUploadV2> listPrStandardImagesUploads = new List<PrFileUploadV2>();
            List<PrSupportiveDocumentV2> listPrSupportiveDoc = new List<PrSupportiveDocumentV2>();
            foreach (var item in prMaster.PrDetails)
            {
                listItemSpecification.AddRange(item.PrBoms);
                listReplacementImages.AddRange(item.PrReplacementFileUploads);
                listPrStandardImagesUploads.AddRange(item.PrFileUploads);
                listPrSupportiveDoc.AddRange(item.PrSupportiveDocuments);
            };

            gvReplacementImageAttachment.DataSource = listReplacementImages;
            gvReplacementImageAttachment.DataBind();

            gvStandardImageAttachment.DataSource = listPrStandardImagesUploads;
            gvStandardImageAttachment.DataBind();

            gvSupportiveDocumentAttachment.DataSource = listPrSupportiveDoc;
            gvSupportiveDocumentAttachment.DataBind();
        }

        public List<SupplierBidEmailContact> UnRegisteredSuppliers
        {
            get
            {
                if (ViewState["UnRegisteredSuppliers"] == null)
                    ViewState["UnRegisteredSuppliers"] = new List<SupplierBidEmailContact>();
                return (List<SupplierBidEmailContact>)ViewState["UnRegisteredSuppliers"];
            }
        }     

        private void GetSupplierBidEmail(PrMasterV2 prMaster) {
            try {
                List<SupplierBidEmail> ListSupplierBidEmail = supplierController.GetSupplierAssignedToBid(serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrId, serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Select(x => x.BidId).ToList());
                hdnField.Value = "Insert";
                if (ListSupplierBidEmail.Count > 0) {
                    EmailHeader.InnerText = "Send Updated Details To Suppliers(Already one email sent)";
                    try {
                        for (int i = 0; i < gvSupplier.Rows.Count; i++) {
                            if (ListSupplierBidEmail.Any(x => x.SupplierId == Convert.ToInt32(gvSupplier.Rows[i].Cells[0].Text))) {
                                (gvSupplier.Rows[i].FindControl("ckSupplier") as CheckBox).Checked = true;
                            }
                        }

                        List<SupplierBidEmailContact> supplierBidEmailContact = supplierController.GetSupplierBidEmailContact(serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrId);
                        ViewState["supplierBidEmailContact"] = serializer.Serialize(supplierBidEmailContact);
                        gvContactOfficer.DataSource = supplierBidEmailContact;
                        gvContactOfficer.DataBind();
                        divContactInfo.Visible = true;

                        UnRegisteredSuppliers.AddRange(supplierController.GetUnRegisteredSuppliersByPrId(serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrId));
                        gvSupplierTempEmail.DataSource = UnRegisteredSuppliers;
                        gvSupplierTempEmail.DataBind();
                        //divTempSupplier.Visible = true;
                        dvTempSupplier.Visible = true;
                    }
                    catch (Exception ex) {

                    }
                    hdnField.Value = "Update";
                }
            }
            catch (Exception ex) {

            }
        }

        private void InitializeContactOfficersDatatable()
        {
            DataTable dtContactOfficer = new DataTable();
            dtContactOfficer.Columns.AddRange(new DataColumn[4] { new DataColumn("UserId"), new DataColumn("ContactOfficer"), new DataColumn("ContactNo"), new DataColumn("Title")});
            ViewState["ContactOfficer"] = dtContactOfficer;
        }

        private void LoadSuppliers()
        {
            List<Supplier> suppliers = supplierController.GetAllSuppliersToSendBidEmail(serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId);
            ViewState["suppliers"] = serializer.Serialize(suppliers);
            gvSupplier.DataSource = suppliers;
            gvSupplier.DataBind();
        }

        private void LoadTitleDropdown()
        {
            List<CommonReference> ListTitles = new List<CommonReference>();
            ListTitles.Add(new CommonReference { Id = 1, Name = "Mr." });
            ListTitles.Add(new CommonReference { Id = 2, Name = "Mrs." });
            ListTitles.Add(new CommonReference { Id = 3, Name = "Ms." });
            ListTitles.Add(new CommonReference { Id = 4, Name = "Miss." });
            ddlGender.DataSource = ListTitles;
            ddlGender.DataValueField = "Id";
            ddlGender.DataTextField = "Name";
            ddlGender.DataBind();
        }
        
            
        private void LoadOfficersContact()
        {
            CompanyLogin cl = itemCategoryOwnerController.GetCurrentPurchasingOfficer(serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrCategoryId);
            List<CompanyLogin> Cls = companyLoginController.GetUserListByDepartmentid(int.Parse(Session["CompanyId"].ToString()));
            ViewState["UserList"] = serializer.Serialize(Cls);
            ddlContactOfficer.DataSource = Cls;
            ddlContactOfficer.DataValueField = "UserId";
            ddlContactOfficer.DataTextField = "FirstName";
            ddlContactOfficer.DataBind();
            if (cl != null)
            {
                ListItem item = ddlContactOfficer.Items.FindByValue(cl.UserId.ToString());
                if (item != null) {
                    item.Selected = true;

                    CompanyLogin user = Cls.Where(x => x.UserId == cl.UserId).Single();
                    txtContactNo.Text = "";

                    if (user != null) {
                        txtContactNo.Text = user.ContactNo;
                    }
                    else {
                        txtContactNo.Text = string.Empty;
                    }
                    
                }
               
            }
            
        }
        protected void ddlContactOfficer_SelectedIndexChanged(object sender, EventArgs e) {
            txtContactNo.Text = "";
            int id = int.Parse(ddlContactOfficer.SelectedValue);
            CompanyLogin user = serializer.Deserialize<List<CompanyLogin>>(ViewState["UserList"].ToString()).Where(x => x.UserId == id).Single();
            if (user != null) {
                txtContactNo.Text = user.ContactNo;
            }
            else {
                       txtContactNo.Text = string.Empty;
                    }
               
            }
        protected void SendEmail_Click(object sender, EventArgs e)
        {
            List<Bidding> bids = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids;
            
            List<int> BidIds = new List<int>();
            int BidId = 0;
            for (int i = 0; i < bids.Count; i++) {
                 BidId = bids[i].BidId;
                BidIds.Add(BidId);
            }

            PrMasterV2 prMastern  = new JavaScriptSerializer().Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
            List<PrReplacementFileUploadV2> listReplacementImages = new List<PrReplacementFileUploadV2>();
            List<PrFileUploadV2> listPrStandardImagesUploads = new List<PrFileUploadV2>();
            List<PrSupportiveDocumentV2> listPrSupportiveDoc = new List<PrSupportiveDocumentV2>();
            foreach (var item in prMastern.PrDetails)
            {
                
                listReplacementImages.AddRange(item.PrReplacementFileUploads);
                listPrStandardImagesUploads.AddRange(item.PrFileUploads);
                listPrSupportiveDoc.AddRange(item.PrSupportiveDocuments);
            };

            

            foreach (GridViewRow row in gvReplacementImageAttachment.Rows) {
                
                    CheckBox chkreplacementImg = row.FindControl("chkreplacementImg") as CheckBox;
                   
                    if (chkreplacementImg.Checked == false) {

                    int fileId = int.Parse(row.Cells[1].Text);
                    listReplacementImages.RemoveAll(x => x.FileId == fileId);

                    }
               
                gvReplacementImageAttachment.DataSource = listReplacementImages;
                gvReplacementImageAttachment.DataBind();
            }

            foreach (GridViewRow row in gvStandardImageAttachment.Rows) {

                CheckBox chkStandardImg = row.FindControl("chkStandardImg") as CheckBox;
               
                if (chkStandardImg.Checked == false) {

                    int fileId = int.Parse(row.Cells[1].Text);
                    listPrStandardImagesUploads.RemoveAll(x => x.FileId == fileId);

                }

                gvStandardImageAttachment.DataSource = listPrStandardImagesUploads;
                gvStandardImageAttachment.DataBind();
            }


            foreach (GridViewRow row in gvSupportiveDocumentAttachment.Rows) {

                CheckBox chkSupportingDocs = row.FindControl("chkSupportingDocs") as CheckBox;

                if (chkSupportingDocs.Checked == false) {

                    int fileId = int.Parse(row.Cells[1].Text);
                    listPrSupportiveDoc.RemoveAll(x => x.FileId == fileId);

                }

                gvSupportiveDocumentAttachment.DataSource = listPrSupportiveDoc;
                gvSupportiveDocumentAttachment.DataBind();
            }

            List<BiddingItem> Items = biddingItemController.GetBiddingItems(BidIds);
            

            List<Supplier> listSuppliers = new List<Supplier>();
            Supplier supplier = null;
            CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).CreatedBy);
            string createdUserEmail = companyLogin.EmailAddress;
            for (int i = 0; i < gvSupplier.Rows.Count; i++)
            {
                supplier = new Supplier();
                bool check = (gvSupplier.Rows[i].FindControl("ckSupplier") as CheckBox).Checked;
                if (check)
                {
                    supplier.SupplierId = Convert.ToInt32(gvSupplier.Rows[i].Cells[0].Text);
                    supplier.SupplierName = gvSupplier.Rows[i].Cells[2].Text;
                    supplier.Email = gvSupplier.Rows[i].Cells[3].Text;
                    listSuppliers.Add(supplier);
                }
            }

            for (int i = 0; i < UnRegisteredSuppliers.Count; i++)
            {
                supplier = new Supplier();
                supplier.SupplierName = UnRegisteredSuppliers[i].ContactOfficer;
                supplier.Email = UnRegisteredSuppliers[i].Email;
                listSuppliers.Add(supplier);
            }
            List<SupplierBidEmailContact> officerContacts = new List<SupplierBidEmailContact>();
            if (ViewState["supplierBidEmailContact"] != null) {
               officerContacts = serializer.Deserialize<List<SupplierBidEmailContact>>(ViewState["supplierBidEmailContact"].ToString());
            }
            if (listSuppliers.Count != 0 && officerContacts.Count !=0)
            {
                PrMasterV2 prMaster = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString());
                try
                {
                    if (hdnField.Value == "Insert")
                    {
                        int status = 1;
                      bidController.SendEmailToSuppliers(status, createdUserEmail, prMaster, listSuppliers, UnRegisteredSuppliers, officerContacts, bids, current, gvStandardImageAttachment, gvSupportiveDocumentAttachment, gvReplacementImageAttachment);
                    }
                    if (hdnField.Value == "Update")
                    {
                        int status = 2;
                        PrMasterV2 prm = PrMataterControllerV2.GetPrSubmittedBid(serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).PrId, int.Parse(Session["CompanyId"].ToString()));
                       bidController.SendEmailToSuppliers(status, createdUserEmail, prm, listSuppliers, UnRegisteredSuppliers, officerContacts, bids, current, gvStandardImageAttachment, gvSupportiveDocumentAttachment, gvReplacementImageAttachment);
                    }


                    for (int i = 0; i < Items.Count; i++) {
                        int prdId = Items[i].PrdId;
                        pRDetailsStatusLogController.InsertLog(prdId, int.Parse(Session["UserId"].ToString()), "EMAILSENT");

                    }
                    bidController.UpdateEmailStatus(BidIds, int.Parse(Request.QueryString.Get("PrId")));
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                    //"none",
                    //"<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Email Sent', showConfirmButton: true,  cancelButtonText: 'ok', closeOnConfirm: false}); }).then((result) => { window.location = 'SumittedMRNPRView.aspx'});   </script>",
                    //false);
                     //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Email Sent', showConfirmButton: false,timer: 45000}).then((result) => { window.location = 'ViewApprovePR.aspx' });   </script>", false);
                     ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Email Sent', showConfirmButton: false,timer: 4000}).then((result) => { window.location = 'SumittedMRNPRView.aspx'}); });   </script>", false);

                }
                catch (Exception ex)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error while sending email', showConfirmButton: false,timer: 3000}); });   </script>",
                        false);
                }

               
            }

            else {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                        "none",
                        "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Add Supplier/Contact Officer', showConfirmButton: false,timer: 3000}); });   </script>",
                        false);
            }
            
        }



        protected void btnAddContactInfo_Click(object sender, EventArgs e)
        {
            bool status = false;
            if (ddlContactOfficer.Visible)  // when adding new contact that not found in databse then dropdonw is hidden
            {
                status = ddlContactOfficer.SelectedValue.ToString() != "0" && txtContactNo.Text != "";
            }
            else
            {
                status = txtContactOfficer.Text != "" && txtContactNo.Text != "";
            }
            if (status)
            {
                int userId = Convert.ToInt32(ddlContactOfficer.SelectedValue.ToString());
                string contactOffcier = string.Empty;
                if (ddlContactOfficer.Visible)
                {
                    contactOffcier = ddlContactOfficer.SelectedItem.Text;
                }
                else
                {
                    contactOffcier = txtContactOfficer.Text;
                }
                string contactNo = txtContactNo.Text;
                string title = ddlGender.SelectedItem.Text;
                List<SupplierBidEmailContact> supplierBidEmailContact = null;
                if (ViewState["supplierBidEmailContact"] == null)
                {
                    supplierBidEmailContact = new List<SupplierBidEmailContact>();                
                }else
                {
                    supplierBidEmailContact = serializer.Deserialize<List<SupplierBidEmailContact>>(ViewState["supplierBidEmailContact"].ToString());                  
                }

                if (supplierBidEmailContact.Find(t => t.Title == title && t.ContactOfficer == contactOffcier && t.ContactNo == contactNo) == null)
                {
                    supplierBidEmailContact.Add(new SupplierBidEmailContact { UserId = 0, Title = title, ContactOfficer = contactOffcier, ContactNo = contactNo });
                    ViewState["supplierBidEmailContact"] = serializer.Serialize(supplierBidEmailContact);
                }

                gvContactOfficer.DataSource = supplierBidEmailContact;
                gvContactOfficer.DataBind();
                divContactInfo.Visible = true;
            }
            
        }

        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            ddlContactOfficer.Visible = false;
            txtContactOfficer.Visible = true;
            txtContactOfficer.Text = "";
            txtContactNo.Text = "";
        }

        protected void btnDeleteContactInfo_Click(object sender, EventArgs e)
        {
            int RowId = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
            var row = (sender as Button).NamingContainer as GridViewRow;
            string Name = row.Cells[2].Text;
            List<SupplierBidEmailContact> supplierBidEmailContact = serializer.Deserialize<List<SupplierBidEmailContact>>(ViewState["supplierBidEmailContact"].ToString());
            supplierBidEmailContact.Remove(supplierBidEmailContact.Where(note => note.ContactOfficer == Name).First());
            ViewState["supplierBidEmailContact"] = serializer.Serialize(supplierBidEmailContact);
            gvContactOfficer.DataSource = supplierBidEmailContact;
            gvContactOfficer.DataBind();
        }
      
        protected void gvBids_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int bidId = int.Parse(gvBids.DataKeys[e.Row.RowIndex].Value.ToString());
                GridView gvBidItems = e.Row.FindControl("gvBidItems") as GridView;
                
                gvBidItems.DataSource = serializer.Deserialize<PrMasterV2>(ViewState["PrMaster"].ToString()).Bids.Find(b => b.BidId == bidId).BiddingItems;
                gvBidItems.DataBind();
            }

        }

        [WebMethod]
        public static List<string> LoadPRCodes(string input)
        {
            CustomerPREdit cp = new CustomerPREdit();
            input = input.Replace(" ", string.Empty);
            return ((List<string>)cp.Session["PRCodeLists"]).FindAll(item => item.ToLower().Replace(" ", string.Empty).Contains(input.ToLower()));
        }
                
        protected void lbtnBddingplan_Click(object sender, EventArgs e)
        {
            int bidId = int.Parse(((GridViewRow)((LinkButton)sender).NamingContainer).Cells[1].Text);
            ViewState["bidID"] = bidId;
            List<BiddingPlan> bidingplan = biddingPlanController.GetBiddingPlanByID(bidId);

            dvBiddingplan.DataSource = bidingplan;
            dvBiddingplan.DataBind();

            

            dvUpdateplan.Visible = false;
            dvcomplete.Visible = false;
            btnClear_Click(sender, e);
            btneditcancel_Click(sender, e);
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBiddingplan').modal('show');});   </script>", false);

            List<Bidding> BiddingItems = bidController.FetchBidInfo(int.Parse(Request.QueryString.Get("PrId")));

            List<int> bidIdList = new List<int>();
            for (int i = 0; i < BiddingItems.Count; i++) {
                int BidId = BiddingItems[i].BidId;
                bidIdList.Add(bidId);
            }

            List<BiddingPlan> Bidingplan = biddingPlanController.GetBiddingPlanByIDPrint(bidIdList);
            dvBiddingplanPrint.DataSource = Bidingplan;
            dvBiddingplanPrint.DataBind();
        }

        protected void dvBiddingplan_RowDataBound(object sender, GridViewRowEventArgs e)
        {          
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var status = (e.Row.FindControl("lblIscompleted") as Label).Text;
                string withTime = e.Row.Cells[5].Text;
                ViewState["withTime"] = withTime;
                if (withTime == "0")
                {
                    e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                    e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                }
                if (status == "Pending")
                {
                    LinkButton lbn1 = e.Row.FindControl("lblComplete") as LinkButton;
                    lbn1.Visible = true;
                    LinkButton lbn2 = e.Row.FindControl("lbtnEdit") as LinkButton;
                    lbn2.Visible = true;
                }
                else
                {
                    LinkButton lbn2 = e.Row.FindControl("lblview") as LinkButton;
                    lbn2.Visible = true;
                }

            }
        }

        protected void dvBiddingplanPrint_RowDataBound(object sender, GridViewRowEventArgs e) {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                var status = (e.Row.FindControl("lblIscompleted") as Label).Text;
                string withTime = e.Row.Cells[5].Text;
                ViewState["withTime"] = withTime;
                if (withTime == "0") {
                    e.Row.Cells[3].Text = Convert.ToDateTime(e.Row.Cells[3].Text).ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                    e.Row.Cells[4].Text = Convert.ToDateTime(e.Row.Cells[4].Text).ToString(System.Configuration.ConfigurationSettings.AppSettings["datePatternBackend"]);
                }
                
            }
        }

        protected void lblComplete_Click(object sender, EventArgs e)
        {
            dvcomplete.Visible = true;
            var row = ((GridViewRow)((LinkButton)sender).NamingContainer);
           ViewState["BidId"]  = int.Parse(row.Cells[0].Text);
            ViewState["PlanId"] = int.Parse(row.Cells[1].Text);
            lblPlan.Text = row.Cells[2].Text;
            lblStart.Text = row.Cells[3].Text;
            lblEndDate.Text = row.Cells[4].Text;
            txtactualDate.Attributes["min"] = Convert.ToDateTime(lblStart.Text).ToString("yyyy-MM-dd");
            txtactualDate.Attributes["max"] = Convert.ToDateTime(lblEndDate.Text).ToString("yyyy-MM-dd");
            dvUpdateplan.Visible = false;

        }

        protected void lblview_Click(object sender, EventArgs e)
        {

            var row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            ViewState["BidId"] = int.Parse(row.Cells[0].Text);
            ViewState["PlanId"] = int.Parse(row.Cells[1].Text);
            List<BiddingPlanFileUpload> bidingplandoc = biddingPlanController.GetPalanfiles(int.Parse(ViewState["BidId"].ToString()), int.Parse(ViewState["PlanId"].ToString()));
            gvbddinplanfiles.DataSource = bidingplandoc;
            gvbddinplanfiles.DataBind();
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBiddingplan').modal('hide');$('#mdlBiddingPlanDocs').modal('show');});   </script>", false);


        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            lblPlan.Text = "";
            lblStart.Text = "";
            lblEndDate.Text = "";
            txtactualDate.Text = "";
            txtactualTime.Text = "";
            dvcomplete.Visible = false;


            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBiddingplan').modal('show');});   </script>", false);
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (chkIsCompleted.Checked)
            {
                DateTime startdate = Convert.ToDateTime(lblStart.Text);
                DateTime enddate = Convert.ToDateTime(lblEndDate.Text);
                if (ViewState["withTime"].ToString() == "0") {
                    enddate = enddate.AddDays(1).AddSeconds(-1);
                }

                DateTime actualdate = Convert.ToDateTime(txtactualDate.Text + " " + txtactualTime.Text);
                if ((startdate < actualdate) && (actualdate < enddate))
                {


                    var Isupdated = biddingPlanController.UpdateIsComplted(int.Parse(ViewState["BidId"].ToString()), int.Parse(ViewState["PlanId"].ToString()), 1, actualdate);
                    if (Isupdated > 0)
                    {
                        List<BiddingPlanFileUpload> planfiles = new List<BiddingPlanFileUpload>();
                        if (fileUpload1.PostedFile != null && fileUpload1.PostedFile.ContentLength > 0)
                        {
                            int seq = 0;
                            HttpFileCollection uploadedFiles = Request.Files;
                            for (int i = 0; i < uploadedFiles.Count; i++)
                            {

                                HttpPostedFile userPostedFile = uploadedFiles[i];
                                string path = "";
                                var extention = System.IO.Path.GetExtension(userPostedFile.FileName);
                                string filenameWithoutPath = Path.GetFileName(userPostedFile.FileName);
                                string imagename = LocalTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + filenameWithoutPath;
                                path = "~/BiddingPlanDocs/" + imagename;
                                fileUpload1.SaveAs(Server.MapPath("BiddingPlanDocs") + "\\" + imagename);
                                seq = seq + 1;
                                BiddingPlanFileUpload planfile = new BiddingPlanFileUpload();
                                planfile.BidId = int.Parse(ViewState["BidId"].ToString());
                                planfile.PlanId = int.Parse(ViewState["PlanId"].ToString());
                                planfile.filename = imagename;
                                planfile.filepath = path;
                                planfile.sequenceId = seq;
                                planfiles.Add(planfile);


                            }

                            var Issaved = biddingPlanController.SavePalanfiles(planfiles);
                            if (Issaved > 0)
                            {

                                lblPlan.Text = "";
                                lblStart.Text = "";
                                lblEndDate.Text = "";
                                txtactualDate.Text = "";
                                txtactualTime.Text = "";
                                dvcomplete.Visible = false;
                                List<BiddingPlan> bidingplan = biddingPlanController.GetBiddingPlanByID(int.Parse(ViewState["BidId"].ToString()));
                                dvBiddingplan.DataSource = bidingplan;
                                dvBiddingplan.DataBind();

                                List<Bidding> BiddingItems = bidController.FetchBidInfo(int.Parse(Request.QueryString.Get("PrId")));

                                List<int> bidIdList = new List<int>();
                                for (int i = 0; i < BiddingItems.Count; i++) {
                                    int BidId = BiddingItems[i].BidId;
                                    bidIdList.Add(BidId);
                                }

                                List<BiddingPlan> Bidingplan = biddingPlanController.GetBiddingPlanByIDPrint(bidIdList);
                                dvBiddingplanPrint.DataSource = Bidingplan;
                                dvBiddingplanPrint.DataBind();


                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBiddingplan').modal('show');});   </script>", false);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error In uploading Bidding Plan Documents', showConfirmButton: true,timer: 4000}); });   </script>", false);
                            }
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error Indate time selection', showConfirmButton: true,timer: 4000}); });   </script>", false);
                    }

                }
                else
                {

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error In updating Bidding Plan', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }

            }

        }

        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            var row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            string path = row.Cells[1].Text;
            string fileName = row.Cells[0].Text;
            string Filpath = Server.MapPath(path);
            string ext = Path.GetExtension(path);
            System.IO.FileInfo file = new System.IO.FileInfo(Filpath);
            if (file.Exists)
            {
                System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
                response.ClearContent();
                response.Clear();
                //get file extension
                string type = "";

                // set known types based on file extension
                if (ext != null)
                {
                    switch (ext.ToLower())
                    {
                        case ".htm":
                        case ".html":
                            type = "text/HTML";
                            break;

                        case ".txt":
                            type = "text/plain";
                            break;

                        case ".GIF":
                            type = "image/GIF";
                            break;

                        case ".pdf":
                            type = "Application/pdf";
                            break;

                        case ".doc":
                        case ".rtf":
                            type = "Application/msword";
                            break;
                        case ".jpg":
                            type = "image/jpeg";
                            break;
                        case ".csv":
                            type = "text/csv";
                            break;
                        case ".jpeg":
                        case ".xls":
                            type = "application/vnd.xls";
                            break;
                        case ".zip":
                            type = "application/zip";
                            break;
                        case ".ppt":
                            type = "application/vnd.ms-powerpoint";
                            break;
                        case ".png":
                            type = "image/png";
                            break;

                    }
                }
                response.ContentType = type;
                response.AddHeader("Content-Disposition",
                                   "attachment; filename=" + fileName + ";");
                response.TransmitFile(Server.MapPath(path));
                response.Flush();
                response.End();

                //Response.Clear();
                //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name); Response.AddHeader("Content-Length", file.Length.ToString());
                //Response.ContentType = "application/octet-stream";
            }
        }

        protected void lbtnEdit_Click(object sender, EventArgs e)
        {
            var row = ((GridViewRow)((LinkButton)sender).NamingContainer);
            ViewState["BidId"] = int.Parse(row.Cells[0].Text);
            ViewState["PlanId"] = int.Parse(row.Cells[1].Text);
            lblupdatePlan.Text = row.Cells[2].Text;
            string withTime = row.Cells[5].Text;
            string fromDate = Convert.ToDateTime(row.Cells[3].Text).ToString("yyyy-MM-dd");
            string toDate = Convert.ToDateTime(row.Cells[4].Text).ToString("yyyy-MM-dd");
            string startTime = string.Empty;
            string endTime = string.Empty;
            if (withTime == "1")
            {
                startTime = Convert.ToDateTime(row.Cells[3].Text).ToString("HH:mm");
                endTime = Convert.ToDateTime(row.Cells[4].Text).ToString("HH:mm");
                txtStartDTime.Enabled = true;
                txtEndDTime.Enabled = true;
                txtStartDTime.Text = startTime;
                txtEndDTime.Text = endTime;
            }
            else
            {
                txtStartDTime.Text = "";
                txtEndDTime.Text = "";
                txtStartDTime.Enabled = false;
                txtEndDTime.Enabled = false;
            }

            txtstart.Text = fromDate;
            txtend.Text = toDate;
            dvUpdateplan.Visible = true;
            dvcomplete.Visible = false;
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { document.getElementById('ContentSection_dvUpdateplan').scrollIntoView()});   </script>", false);

        }

        protected void btneditcancel_Click(object sender, EventArgs e)
        {
            lblupdatePlan.Text = "";
            txtstart.Text = "";
            txtend.Text = "";
            dvUpdateplan.Visible = false;

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime startdate = Convert.ToDateTime(txtstart.Text + " " + txtStartDTime.Text);
            DateTime enddate = Convert.ToDateTime(txtend.Text + " " + txtEndDTime.Text);
           
            List<BiddingItem> Items = biddingItemController.GetBiddingItemsList(int.Parse(ViewState["bidID"].ToString()));
            if (startdate < enddate)
            {
                var Isupdated = biddingPlanController.UpdateBiddingplan(int.Parse(ViewState["BidId"].ToString()), int.Parse(ViewState["PlanId"].ToString()), startdate, enddate, int.Parse(Session["UserId"].ToString()));
                if (Isupdated > 0)
                {
                    //  btnSendEmail.Visible = true;
                    List<BiddingPlan> bidingplan = biddingPlanController.GetBiddingPlanByID(int.Parse(ViewState["BidId"].ToString()));
                    dvBiddingplan.DataSource = bidingplan;
                    dvBiddingplan.DataBind();
                    

                    for (int i = 0; i < Items.Count; i++) {
                        int prdId = Items[i].PrdId;
                        pRDetailsStatusLogController.InsertLog(prdId, int.Parse(Session["UserId"].ToString()), "UPDTED_PROC_PLN");

                    }

                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlBiddingplan').modal('show');});   </script>", false);
                    btneditcancel_Click(sender, e);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error In updating Bidding Plan', showConfirmButton: true,timer: 4000}); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Incrrects date', showConfirmButton: true,timer: 4000}); });   </script>", false);
            }
        }

        protected void btnSupplierSearch_Click(object sender, EventArgs e)
       {
            lblSupplierSearch.Visible = false;
            if (txtSupplierSearch.Text != "")
            {
                List<Supplier> tt = serializer.Deserialize<List<Supplier>>(ViewState["suppliers"].ToString()).Where(x => x.SupplierName.ToLower().StartsWith(txtSupplierSearch.Text.ToLower())).ToList();
                if (tt.Count > 0)
                {
                    gvSupplier.DataSource = tt;
                }else
                {
                     gvSupplier.DataSource = new List<Supplier> { new Supplier() }.ToList();
                    lblSupplierSearch.Visible = true;
                }
                gvSupplier.DataBind();
            }
            else
            {
                gvSupplier.DataSource = serializer.Deserialize<List<Supplier>>(ViewState["suppliers"].ToString());
                gvSupplier.DataBind();                
            }

            }
        
        protected void btnAddNewTempSupplier_Click(object sender, EventArgs e)
        {
            string supplierName = txtSupplierName.Text;
            string supplierEmail = txtSupplierEmailAddress.Text;
            UnRegisteredSuppliers.Add(new SupplierBidEmailContact { UserId = 0, ContactOfficer = supplierName, Email = supplierEmail });
            gvSupplierTempEmail.DataSource = UnRegisteredSuppliers;
            gvSupplierTempEmail.DataBind();
            divTempSupplier.Visible = true;
            txtSupplierName.Text = "";
            txtSupplierEmailAddress.Text = "";
        }

        protected void gvSupplierTempEmail_Delete_Click(object sender, EventArgs e)
        {
            int RowId = ((GridViewRow)((Button)sender).Parent.Parent).RowIndex;
            var row = (sender as Button).NamingContainer as GridViewRow;
            string Name = row.Cells[1].Text;
            UnRegisteredSuppliers.Remove(UnRegisteredSuppliers.Where(t => t.ContactOfficer == Name).First());
            gvSupplierTempEmail.DataSource = UnRegisteredSuppliers;
            gvSupplierTempEmail.DataBind();
        }

        protected void btnShowNewSupplier_Click(object sender, EventArgs e)
        {
            divTempSupplier.Visible = true;
            dvTempSupplier.Visible = true;
        }

      }
}