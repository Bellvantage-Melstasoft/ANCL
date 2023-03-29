using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Domain;
using CLibrary.Common;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace BiddingSystem
{
    public partial class SupplierAdvancedSearchInner : System.Web.UI.Page
    {
        public BiddingController biddingController = ControllerFactory.CreateBiddingController();
        public SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

        List<Bidding> BiddingList = new List<Bidding>();
        List<Bidding> BiddingListLatest = new List<Bidding>();
        public List<string> BiddingAllListStr = new List<string>();
        public List<string> BiddingAllLatestListStr = new List<string>();
        public string supplier = string.Empty;
        int supplierId = 0;

        public string PendingBidCount = string.Empty;
        SupplierCategoryController supplierCategoryController = ControllerFactory.CreatesupplierCategoryController();
        SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
        public List<string> supplierMainCategoryList = new List<string>();
        public List<string> supplierMainCategoryListWithSubCategory = new List<string>();
        public List<string> supplierAssignedCompany = new List<string>();

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
                            BiddingAllListStr.Add(biddingList.PrId + "-" + biddingList.ItemId.ToString() + "-" + biddingList.ItemName + "-" + biddingList.DepartmentID + "-" + biddingList.DepartmentName + "-" + biddingList.ImagePathDep + "-" + biddingList.ImagePath + "-" + biddingList.StartDate + "-" + biddingList.EndDate + "-" + biddingList.BiddingOrderId + "-" + "Edit Bid");
                        }
                        if (amount == 0 && isPending == 1)
                        {
                            BiddingAllListStr.Add(biddingList.PrId + "-" + biddingList.ItemId.ToString() + "-" + biddingList.ItemName + "-" + biddingList.DepartmentID + "-" + biddingList.DepartmentName + "-" + biddingList.ImagePathDep + "-" + biddingList.ImagePath + "-" + biddingList.StartDate + "-" + biddingList.EndDate + "-" + biddingList.BiddingOrderId + "-" + "Pending Bid");
                        }
                        if (amount == 0 && isPending == 0)
                        {
                            BiddingAllListStr.Add(biddingList.PrId + "-" + biddingList.ItemId.ToString() + "-" + biddingList.ItemName + "-" + biddingList.DepartmentID + "-" + biddingList.DepartmentName + "-" + biddingList.ImagePathDep + "-" + biddingList.ImagePath + "-" + biddingList.StartDate + "-" + biddingList.EndDate + "-" + biddingList.BiddingOrderId + "-" + "Bid");
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
                            BiddingAllLatestListStr.Add(item.PrId + "-" + item.ItemId.ToString() + "-" + item.ItemName + "-" + item.DepartmentID + "-" + item.DepartmentName + "-" + item.ImagePathDep + "-" + item.ImagePath + "-" + item.StartDate + "-" + item.EndDate + "-" + item.BiddingOrderId + "-" + "Edit Bid");
                        }
                        if (amount == 0 && isPending == 1)
                        {
                            BiddingAllLatestListStr.Add(item.PrId + "-" + item.ItemId.ToString() + "-" + item.ItemName + "-" + item.DepartmentID + "-" + item.DepartmentName + "-" + item.ImagePathDep + "-" + item.ImagePath + "-" + item.StartDate + "-" + item.EndDate + "-" + item.BiddingOrderId + "-" + "Pending Bid");
                        }
                        if (amount == 0 && isPending == 0)
                        {
                            BiddingAllLatestListStr.Add(item.PrId + "-" + item.ItemId.ToString() + "-" + item.ItemName + "-" + item.DepartmentID + "-" + item.DepartmentName + "-" + item.ImagePathDep + "-" + item.ImagePath + "-" + item.StartDate + "-" + item.EndDate + "-" + item.BiddingOrderId + "-" + "Bid");
                        }
                    }

                    PendingBidCount = supplierQuotationController.GetSupplierPendingBids(supplierId).Count.ToString();
                    List<SupplierCategory> _supplierCategory = supplierCategoryController.GetSupplierCategoryWithCategoryName(supplierId);

                    foreach (var item in _supplierCategory)
                    {
                        supplierMainCategoryList.Add(item.CategoryId + "-" + item.CategoryName + "-" + item.SupplierId);
                    }

                    List<SupplierAssignedToCompany> supplierAssigneToCompany = supplierAssigneToCompanyController.GetSupplierAssignedCompanies(supplierId);

                    foreach (var itemCompany in supplierAssigneToCompany)
                    {
                        supplierAssignedCompany.Add(itemCompany.DepartmentID + "-" + itemCompany.DepartmentName + "-" + itemCompany.ImagePath);
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

        public string getJsonPendingBidCount()
        {
            var PendingCount = PendingBidCount;
            return (new JavaScriptSerializer()).Serialize(PendingCount);
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