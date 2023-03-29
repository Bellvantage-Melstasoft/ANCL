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
using System.Web.Services;

namespace BiddingSystem
{
    public partial class SupplierIndex : System.Web.UI.Page
    {

        public string PendingBidCount = string.Empty;
        public BiddingController biddingController = ControllerFactory.CreateBiddingController();
        public SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        SupplierCategoryController supplierCategoryController = ControllerFactory.CreatesupplierCategoryController();

        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        SupplierController supplierController = ControllerFactory.CreateSupplierController();


        List<Bidding> BiddingList = new List<Bidding>();
        List<Bidding> BiddingListLatest = new List<Bidding>();

        public List<string> supplierMainCategoryList = new List<string>();
        public List<string> supplierMainCategoryListWithSubCategory = new List<string>();
        public List<string> supplierAssignedCompany = new List<string>();

        public List<string> BiddingAllListStr = new List<string>();
        public List<string> BiddingAllLatestListStr = new List<string>();

        public string supplier = string.Empty;
        int supplierId = 0;
        public string SupplierName = string.Empty;
        public string SupplierLogo = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {


            if (Session["supplierId"] != null && Session["supplierId"].ToString() != "")
            {
                supplierId = int.Parse(Session["supplierId"].ToString());
                Supplier supplierDetails = supplierController.GetSupplierBySupplierId(supplierId);
                SupplierName = supplierDetails.SupplierName;
                SupplierLogo = supplierDetails.SupplierLogo;
            }
            //if (Session["supplierId"] != null && Session["supplierId"].ToString() != "")
            //{
            //    supplierId = int.Parse(Session["supplierId"].ToString());
            //    supplier = supplierId.ToString();
            //}
            else
            {
                Response.Redirect("LoginPageSupplier.aspx");
            }

            if (!IsPostBack)
            {
                try
                {
                    //-----------Get All Opened Bids 
                    string imageFilePath = string.Empty;
                    BiddingList = biddingController.GetAllBiddingNew(supplierId).Where(x => x.IsApproveToViewInSupplierPortal == 1).ToList();

                    foreach (var biddingList in BiddingList)
                    {
                        //if (biddingList.defaultImageLocationCode == 1)
                        //{
                        //    PR_FileUpload pr_FileUploadObj = pR_FileUploadController.fetchPr_FileuploadObjForDefaultImage(biddingList.PrId, biddingList.ItemId);
                        //    if(pr_FileUploadObj.FilePath != "")
                        //    {
                        //        biddingList.ImagePath = pr_FileUploadObj.FilePath;
                        //    }
                        //}
                        //if (biddingList.defaultImageLocationCode == 2)
                        //{
                        //    PR_Replace_FileUpload pr_ReplaceUploadObj = pR_Replace_FileUploadController.fetchPR_Replace_FileUploadObjForDefaultImage(biddingList.PrId, biddingList.ItemId);
                        //    if (pr_ReplaceUploadObj.FilePath != "")
                        //    {
                        //        biddingList.ImagePath = pr_ReplaceUploadObj.FilePath;
                        //    }
                        //}

                        //----------Fetch Loged Supplier Quotation Status
                        SupplierQuotation supplierQuotation = supplierQuotationController.GetGivenQuotatios(biddingList.PrId, biddingList.ItemId, supplierId);

                        decimal amount = supplierQuotation.PerItemPrice;
                        int isPending = supplierQuotation.IsSelected;

                        //-----------Condtions are used to check the supplier Portal Button text (Eg: "Edit Bid","Pending Bid","Bid" )

                        if (amount != 0 && isPending == 0)
                        {
                            BiddingAllListStr.Add(biddingList.PrId + "-" + biddingList.ItemId.ToString() + "-" + biddingList.ItemName + "-" + biddingList.DepartmentID + "-" + biddingList.DepartmentName + "-" + biddingList.ImagePathDep + "-" + biddingList.displayImageUrl + "-" + biddingList.StartDate + "-" + biddingList.EndDate + "-" + biddingList.BiddingOrderId + "-" + "Edit Bid"+"-"+biddingList.CategoryId + "-" + biddingList.SubCategoryId );
                        }
                        if (amount == 0 && isPending == 1)
                        {
                            BiddingAllListStr.Add(biddingList.PrId + "-" + biddingList.ItemId.ToString() + "-" + biddingList.ItemName + "-" + biddingList.DepartmentID + "-" + biddingList.DepartmentName + "-" + biddingList.ImagePathDep + "-" + biddingList.displayImageUrl + "-" + biddingList.StartDate + "-" + biddingList.EndDate + "-" + biddingList.BiddingOrderId + "-" + "Pending Bid" + "-" + biddingList.CategoryId + "-" + biddingList.SubCategoryId);
                        }
                        if (amount == 0 && isPending == 0)
                        {
                            BiddingAllListStr.Add(biddingList.PrId + "-" + biddingList.ItemId.ToString() + "-" + biddingList.ItemName + "-" + biddingList.DepartmentID + "-" + biddingList.DepartmentName + "-" + biddingList.ImagePathDep + "-" + biddingList.displayImageUrl + "-" + biddingList.StartDate + "-" + biddingList.EndDate + "-" + biddingList.BiddingOrderId + "-" + "Bid" + "-" + biddingList.CategoryId + "-" + biddingList.SubCategoryId);
                        }
                    }

                    PendingBidCount = supplierQuotationController.GetSupplierPendingBids(supplierId).Count.ToString();
                    List<SupplierCategory> _supplierCategory = supplierCategoryController.GetSupplierCategoryWithCategoryName(supplierId);

                    foreach (var item in _supplierCategory)
                    {
                        supplierMainCategoryList.Add(item.CategoryId + "-" + item.CategoryName + "-" + item.SupplierId);
                    }

                    List<SupplierCategory> _supplierCategoryAndSubCategory = supplierCategoryController.GetSupplierCategoryAndSubCategory(supplierId);

                    foreach (var itemWithSub in _supplierCategoryAndSubCategory)
                    {
                        supplierMainCategoryListWithSubCategory.Add(itemWithSub.CategoryId + "-" + itemWithSub.SubCategoryId + "-" + itemWithSub.SubCategoryName);
                    }

                    List<SupplierAssignedToCompany> supplierAssigneToCompany = supplierAssigneToCompanyController.GetSupplierAssignedCompanies(supplierId);

                    foreach (var itemCompany in supplierAssigneToCompany)
                    {
                        supplierAssignedCompany.Add(itemCompany.DepartmentID + "-" + itemCompany.DepartmentName + "-" + itemCompany.ImagePath);
                    }
                    //-----------Get Data to Latest Bids (Last 10 Bids)

                    BiddingListLatest = biddingController.GetAllLatestBids(supplierId).Where(x => x.IsApproveToViewInSupplierPortal == 1).ToList();

                    foreach (var item in BiddingListLatest)
                    {

                        //if (item.defaultImageLocationCode == 1)
                        //{
                        //    PR_FileUpload pr_FileUploadObj = pR_FileUploadController.fetchPr_FileuploadObjForDefaultImage(item.PrId, item.ItemId);
                        //    if (pr_FileUploadObj.FilePath != "")
                        //    {
                        //        item.ImagePath = pr_FileUploadObj.FilePath;
                        //    }
                        //}
                        //if (item.defaultImageLocationCode == 2)
                        //{
                        //    PR_Replace_FileUpload pr_ReplaceUploadObj = pR_Replace_FileUploadController.fetchPR_Replace_FileUploadObjForDefaultImage(item.PrId, item.ItemId);
                        //    if (pr_ReplaceUploadObj.FilePath != "")
                        //    {
                        //        item.ImagePath = pr_ReplaceUploadObj.FilePath;
                        //    }
                        //}

                        SupplierQuotation supplierQuotation = supplierQuotationController.GetGivenQuotatios(item.PrId, item.ItemId, supplierId);

                        decimal amount = supplierQuotation.PerItemPrice;
                        int isPending = supplierQuotation.IsSelected;

                        //-----------Condtions are used to check the supplier Portal Button text (Eg: "Edit Bid","Pending Bid","Bid" )

                        if (amount != 0 && isPending == 0)
                        {
                            BiddingAllLatestListStr.Add(item.PrId + "-" + item.ItemId.ToString() + "-" + item.ItemName + "-" + item.DepartmentID + "-" + item.DepartmentName + "-" + item.ImagePathDep + "-" + item.displayImageUrl + "-" + item.StartDate + "-" + item.EndDate + "-" + item.BiddingOrderId + "-" + "Edit Bid" + "-" + item.CategoryId + "-" + item.SubCategoryId);
                        }
                        if (amount == 0 && isPending == 1)
                        {
                            BiddingAllLatestListStr.Add(item.PrId + "-" + item.ItemId.ToString() + "-" + item.ItemName + "-" + item.DepartmentID + "-" + item.DepartmentName + "-" + item.ImagePathDep + "-" + item.displayImageUrl + "-" + item.StartDate + "-" + item.EndDate + "-" + item.BiddingOrderId + "-" + "Pending Bid" + "-" + item.CategoryId + "-" + item.SubCategoryId);
                        }
                        if (amount == 0 && isPending == 0)
                        {
                            BiddingAllLatestListStr.Add(item.PrId + "-" + item.ItemId.ToString() + "-" + item.ItemName + "-" + item.DepartmentID + "-" + item.DepartmentName + "-" + item.ImagePathDep + "-" + item.displayImageUrl + "-" + item.StartDate + "-" + item.EndDate + "-" + item.BiddingOrderId + "-" + "Bid" + "-" + item.CategoryId + "-" + item.SubCategoryId);
                        }
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        //----------------------Pass Supplier ID toClient Side--------------------------
        public string getJsonSupplier()
        {
            return (new JavaScriptSerializer()).Serialize(supplierId);
        }

        //----------------------Pass Data toClient Side--------------------------
        public string getJsonBiddingItemListAll()
        {
            var DataList = BiddingAllListStr;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        //----------------------Pass Data toClient Side--------------------------
        public string getJsonBiddingItemListAllLatest()
        {
            var DataListLatest = BiddingAllLatestListStr;
            return (new JavaScriptSerializer()).Serialize(DataListLatest);
        }

        //-----------------Check the logged supplier already Bided or Keep it as Later Bid or not yet Bid
        //-----------------If,  not yet Bid   -> Button Keep Text as "BID"
        //-----------------If,  already Bided -> Button Keep Text as "EDIT BID"
        //-----------------If,  Later Bid     -> Button Keep Text as "PENDING BID"
        [WebMethod]
        public static DetailsClass[] GetBidPendingOrNot(string data, string sup)
        {
            List<DetailsClass> Detail = new List<DetailsClass>();

            string jsonData = data;
            string PrId = string.Empty;
            string ItemId = string.Empty;
            int supplierid = int.Parse(sup);
            if (jsonData != "")
            {
                string[] value = jsonData.Split('_');
                string val1 = value[0];
                string val2 = value[1];
                PrId = val1;
                ItemId = val2;
                SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
                List<SupplierQuotation> _supplierQuotation = supplierQuotationController.GetPendingCountOfSupplier(int.Parse(PrId), int.Parse(ItemId), supplierid).ToList();
                int countofPendings = _supplierQuotation.Count;
                List<SupplierQuotation> _supplierQuotationAlreadyBid = supplierQuotationController.GetAlreadyBidCountOfSupplier(int.Parse(PrId), int.Parse(ItemId), supplierid).ToList();
                int alreadyBidCount = _supplierQuotationAlreadyBid.Count;

                DetailsClass DataObj = new DetailsClass();
                DataObj.CountPending = countofPendings.ToString();
                DataObj.AlreadyBidCount = alreadyBidCount.ToString();
                Detail.Add(DataObj);
            }
            return Detail.ToArray();
        }

        //Class for binding data
        public class DetailsClass
        {
            public string CountPending { get; set; }
            public string AlreadyBidCount { get; set; }
        }

        public class SupplierQuotData
        {
            public decimal PerItemPrice { get; set; }
            public decimal VatAmount { get; set; }
            public decimal NbtAmount { get; set; }
            public decimal TotalAmount { get; set; }
            public int CheckBoxChecked { get; set; }
        }

        public string getJsonSupplierMainCategory()
        {
            var SupplierMainCategory = supplierMainCategoryList;
            return (new JavaScriptSerializer()).Serialize(SupplierMainCategory);
        }

        public string getJsonSupplierMainCategoryAndSubCat()
        {
            var SupplierMainCategorySub = supplierMainCategoryListWithSubCategory;
            return (new JavaScriptSerializer()).Serialize(SupplierMainCategorySub);
        }

        public string getJsonComapanyList()
        {
            var SupplierCompanyList = supplierAssignedCompany;
            return (new JavaScriptSerializer()).Serialize(SupplierCompanyList);
        }

        //-----------------Not Using
        [WebMethod]
        public static SupplierQuotData[] GetBidExistDataEdit(string data, string sup)
        {
            List<SupplierQuotData> supplierQuotData = new List<SupplierQuotData>();

            string jsonData = data;
            string PrId = string.Empty;
            string ItemId = string.Empty;
            int supplierid = int.Parse(sup);
            if (jsonData != "")
            {
                string[] value = jsonData.Split('_');
                string val1 = value[0];
                string val2 = value[1];
                PrId = val1;
                ItemId = val2;
                SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
                List<SupplierQuotation> _supplierQuotationAlreadyBid = supplierQuotationController.GetAlreadyBidCountOfSupplier(int.Parse(PrId), int.Parse(ItemId), supplierid).ToList();

                foreach (var item in _supplierQuotationAlreadyBid)
                {
                    SupplierQuotData DataObj = new SupplierQuotData();

                    DataObj.PerItemPrice = item.PerItemPrice;
                    DataObj.VatAmount = item.VatAmount;
                    DataObj.NbtAmount = item.NbtAmount;
                    DataObj.TotalAmount = item.TotalAmount;
                    if (item.TotalAmount > 0)
                    {
                        DataObj.CheckBoxChecked = 1;
                    }
                    else
                    {
                        DataObj.CheckBoxChecked = 0;
                    }
                    supplierQuotData.Add(DataObj);
                }
            }
            return supplierQuotData.ToArray();
        }
    }
}