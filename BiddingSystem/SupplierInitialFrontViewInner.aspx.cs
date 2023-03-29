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
    public partial class SupplierInitialFrontViewInner : System.Web.UI.Page
    {
        public BiddingController biddingController = ControllerFactory.CreateBiddingController();
        public SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

        List<Bidding> BiddingList = new List<Bidding>();
        List<Bidding> BiddingListLatest = new List<Bidding>();
        public List<string> BiddingAllListStr = new List<string>();
        public List<string> BiddingAllLatestListStr = new List<string>();
        public List<string> CompanyList = new List<string>();

        ItemCategoryMasterController itemCategoryMasterController = ControllerFactory.CreateItemCategoryMasterController();
        ItemSubCategoryMasterController itemSubCategoryMasterController = ControllerFactory.CreateItemSubCategoryMasterController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();




        public List<string> supplierMainCategoryList = new List<string>();
        public List<string> supplierMainCategoryListWithSubCategory = new List<string>();
        public string supplier = string.Empty;
        int supplierId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["supplierId"] != null && Session["supplierId"].ToString() != "")
            {
                supplierId = int.Parse(Session["supplierId"].ToString());
                supplier = supplierId.ToString();
            }
            else
            {
            }

            if (!IsPostBack)
            {
                try
                {
                    BiddingList = biddingController.GetAllBidding(supplierId).Where(x => x.IsApproveToViewInSupplierPortal == 1).ToList();
                    foreach (var biddingList in BiddingList)
                    {
                        SupplierQuotation supplierQuotation = supplierQuotationController.GetGivenQuotatios(biddingList.PrId, biddingList.ItemId, supplierId);
                        decimal amount = supplierQuotation.PerItemPrice;
                        int isPending = supplierQuotation.IsSelected;
                        if (amount != 0 && isPending == 0)
                        {

                            BiddingAllListStr.Add(biddingList.PrId + "-" + biddingList.ItemId.ToString() + "-" + biddingList.ItemName + "-" + biddingList.DepartmentID + "-" + biddingList.DepartmentName + "-" + biddingList.ImagePathDep + "-" + biddingList.displayImageUrl + "-" + biddingList.StartDate + "-" + biddingList.EndDate + "-" + biddingList.BiddingOrderId + "-" + "Edit Bid" + "-" + biddingList.CategoryId + "-" + biddingList.SubCategoryId);
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

                    BiddingListLatest = biddingController.GetAllLatestBids(supplierId).Where(x => x.IsApproveToViewInSupplierPortal == 1).ToList();

                    foreach (var item in BiddingListLatest)
                    {
                        SupplierQuotation supplierQuotation = supplierQuotationController.GetGivenQuotatios(item.PrId, item.ItemId, supplierId);
                        decimal amount = supplierQuotation.PerItemPrice;
                        int isPending = supplierQuotation.IsSelected;
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


                    List<ItemCategoryMaster> allCategoryList = itemCategoryMasterController.FetchItemCategoryList();

                    foreach (var item in allCategoryList)
                    {
                        supplierMainCategoryList.Add(item.CategoryId + "-" + item.CategoryName);
                    }

                    List<ItemSubCategoryMaster> allSubCategoryist = itemSubCategoryMasterController.FetchItemSubCategoryListForInitialFrontView();

                    foreach (var itemWithSub in allSubCategoryist)
                    {
                        supplierMainCategoryListWithSubCategory.Add(itemWithSub.CategoryId + "-" + itemWithSub.SubCategoryId + "-" + itemWithSub.SubCategoryName);
                    }

                    List<CompanyDepartment> allCompanies = companyDepartmentController.GetDepartmentList();

                    foreach (var itemCompany in allCompanies)
                    {
                        CompanyList.Add(itemCompany.DepartmentID + "-" + itemCompany.DepartmentName + "-" + itemCompany.ImagePath);
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        //
        public string getJsonSupplier()
        {
            var Supplier = supplier;
            return (new JavaScriptSerializer()).Serialize(Supplier);
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
            var AllCompanyList = CompanyList;
            return (new JavaScriptSerializer()).Serialize(AllCompanyList);
        }

        [WebMethod]
        public static DetailsClass[] GetBidPendingOrNot(string data, string sup)
        {
            List<DetailsClass> Detail = new List<DetailsClass>();

            string jsonData = data;
            string PrId = string.Empty;
            string ItemId = string.Empty;
            int supplierid = 0;
            if (sup != "")
            {
                supplierid = int.Parse(sup);
            }

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

        public class DetailsClass //Class for binding data
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

        [WebMethod]
        public static string login(string email, string password)
        {
            SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
            SupplierController supplierController = ControllerFactory.CreateSupplierController();

            try
            {
                SupplierLogin SupplierLoginObj = supplierLoginController.SupplierLogin(email, password);
                if (SupplierLoginObj.Supplierid != 0 && SupplierLoginObj.IsActive == 1)
                {
                    HttpContext.Current.Session["supplierId"] = SupplierLoginObj.Supplierid;
                    // HttpContext.Current.Response.Redirect("SupplierInitialFrontView.aspx");
                    return "1";
                }
                else
                {
                    return "0";
                    // lblLoginmessage.Text = "Invalid Credential!!";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        protected void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("SupplierAdvancedSearchInner.aspx", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}