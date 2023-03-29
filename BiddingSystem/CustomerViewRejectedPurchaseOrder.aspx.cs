using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;

namespace BiddingSystem
{
    public partial class CustomerViewRejectedPurchaseOrder : System.Web.UI.Page
    {

        static string UserId = string.Empty;
        int CompanyId = 0;
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        PODetailsController poDetailsController = ControllerFactory.CreatePODetailsController();
        GrnController grnController = ControllerFactory.CreateGrnController();
        GRNDetailsController grnDetailsController = ControllerFactory.CreateGRNDetailsController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CustomerViewApprovedPurchaseOrder.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "generateGRNLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 9) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack)
            {
                if (int.Parse(UserId) != 0)
                {
                    try
                    {
                        List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
                        List<GrnDetails> _grnDetails = new List<GrnDetails>();

                        pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentIdToGRN(CompanyId);

                        _grnDetails = grnDetailsController.GetGrnDetailsAll(CompanyId).Where(x =>  x.IsGrnApproved == 2 ).ToList();


                        List<GrnToGenerateItemListPO> grnToGenerateItemList = new List<GrnToGenerateItemListPO>();

                        foreach (var itemPo in pOMasterListByDepartmentid)
                        {
                            if ((_grnDetails.Where(x => x.PoId == itemPo.PoID)).ToList().Count != 0)
                            {
                                foreach (var itemGrn in _grnDetails.Where(x => x.PoId == itemPo.PoID && x.ItemId == itemPo.ItemId))
                                {
                                    var result = from item in _grnDetails
                                                 where item.GrnId == itemGrn.GrnId
                                                 group _grnDetails by item.ItemId into grp
                                                 let sum = _grnDetails.Where(x => x.ItemId == grp.Key && x.PoId == itemPo.PoID).Sum(x => x.Quantity)
                                                 select new
                                                 {
                                                     ItemId = grp.Key,
                                                     PoId = itemPo.PoID,
                                                     Sum = sum,
                                                 };

                                    if (itemPo.PoID == itemGrn.PoId)
                                    {
                                        string sumQtu = string.Empty;
                                        string itemidlstQty = string.Empty;

                                        decimal poItemQty = itemPo.Quantity;
                                        decimal grnItemQty = itemGrn.Quantity;

                                        foreach (var itemresult in result)
                                        {
                                            if (itemGrn.ItemId == itemresult.ItemId)
                                            {
                                                sumQtu = itemresult.Sum.ToString();
                                                itemidlstQty = itemresult.ItemId.ToString();
                                                break;
                                            }
                                        }

                                        decimal balanceQuantity = poItemQty - decimal.Parse(sumQtu);

                                        if (itemGrn.IsGrnApproved == 2 && itemGrn.IsGrnRaised == 2)
                                        {
                                            balanceQuantity = balanceQuantity + itemGrn.Quantity;
                                            sumQtu = sumQtu + itemGrn.Quantity;
                                            decimal restQty = decimal.Parse(sumQtu);
                                            if (balanceQuantity > 0)
                                            {
                                                grnToGenerateItemList.Add(new GrnToGenerateItemListPO(itemPo.PoID, "", itemGrn.ItemId, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, itemPo.BasePr, itemPo.SupplierName, itemPo.POCode, itemPo.PrCode));
                                            }
                                        }

                                        if (balanceQuantity > 0)
                                        {
                                            grnToGenerateItemList.Add(new GrnToGenerateItemListPO(itemPo.PoID, "", itemGrn.ItemId, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, itemPo.BasePr, itemPo.SupplierName, itemPo.POCode, itemPo.PrCode));
                                        }
                                    }

                                }
                            }

                            else
                            {
                                grnToGenerateItemList.Add(new GrnToGenerateItemListPO(itemPo.PoID, "", itemPo.ItemId, "", "", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, itemPo.BasePr, itemPo.SupplierName, itemPo.POCode, itemPo.PrCode));
                            }

                        }
                        //var distinctItems = grnToGenerateItemList.GroupBy(x => x.ItemId).Select(y => y.First());
                        //gvPurchaseOrderItems.DataSource = distinctItems;
                        //gvPurchaseOrderItems.DataBind();
                        gvPurchaseOrder.DataSource = grnToGenerateItemList.GroupBy(x => x.PoID).Select(y => y.First()).ToList();
                        gvPurchaseOrder.DataBind();

                      

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }


      

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string poid = gvPurchaseOrder.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvPRDetails = e.Row.FindControl("gvPoDetails") as GridView;

                    List<PODetails> _poDetails = new List<PODetails>();
                    List<GrnDetails> _grnDetailsList = new List<GrnDetails>();
                    List<GrnToGenerateItemList> grnToGenerateItemList = new List<GrnToGenerateItemList>();

                    _poDetails = poDetailsController.GetPODetailsApproved(CompanyId);

                    _grnDetailsList = grnDetailsController.GetGrnDetailsAll(CompanyId);

                    foreach (var itemPo in _poDetails.Where(c => c.PoId == int.Parse(poid)))
                    {
                        if (_grnDetailsList.Where(x => x.PoId == itemPo.PoId).ToList().Count != 0)
                        {
                            foreach (var itemGrn in _grnDetailsList.Where(r => r.PoId == itemPo.PoId))
                            {
                                //var result = from item in _grnDetailsList
                                //             group _grnDetailsList by item.ItemId into grp
                                //             let sum = _grnDetailsList.Where(x => x.GrnId == itemGrn.GrnId && x.ItemId == itemGrn.ItemId).Sum(x => x.Quantity)
                                //             select new
                                //             {
                                //                 ItemId = grp.Key,
                                //                 Sum = sum,
                                //             };

                                var result = from item in _grnDetailsList
                                             group _grnDetailsList by item.ItemId into grp
                                             let sum = _grnDetailsList.Where(x => x.ItemId == grp.Key && x.PoId == itemPo.PoId).Sum(x => x.Quantity)
                                             select new
                                             {
                                                 ItemId = grp.Key,
                                                 PoId = itemPo.PoId,
                                                 Sum = sum,
                                             };

                                if (itemPo.PoId == itemGrn.PoId && itemGrn.ItemId == itemPo.ItemId)
                                {
                                    decimal poItemQty = itemPo.Quantity;
                                    decimal grnItemQty = itemGrn.Quantity;

                                    string sumQtu = string.Empty;
                                    string itemidlstQty = string.Empty;

                                    foreach (var itemresult in result)
                                    {
                                        if (itemGrn.ItemId == itemresult.ItemId)
                                        {
                                            sumQtu = itemresult.Sum.ToString();
                                            itemidlstQty = itemresult.ItemId.ToString();
                                            break;
                                        }
                                    }
                                    if (sumQtu == "")
                                    {
                                        sumQtu = "0";
                                    }
                                    decimal balanceQuantity = 0;
                                    balanceQuantity = poItemQty - decimal.Parse(sumQtu);

                                    //if (itemGrn.AddToGrnCount != 2)
                                    //{
                                    //    if (itemGrn.IsGrnApproved == 2 && itemGrn.IsGrnRaised == 2)
                                    //    {
                                    //        balanceQuantity = balanceQuantity + itemGrn.Quantity;
                                    //    }
                                    //}
                                    if (itemGrn.IsGrnApproved == 2 && itemGrn.IsGrnRaised == 2)
                                    {
                                        balanceQuantity = balanceQuantity + itemGrn.Quantity;
                                        sumQtu = sumQtu + itemGrn.Quantity;
                                        decimal restQty = decimal.Parse(sumQtu);
                                        grnToGenerateItemList.Add(new GrnToGenerateItemList(itemPo.PoId, itemPo.CategoryName, itemGrn.ItemId, itemPo.SubCategoryName, itemPo.ItemName, ((itemPo.VatAmount) / poItemQty) * restQty, ((itemPo.NbtAmount) / poItemQty) * restQty, ((itemPo.TotalAmount) / poItemQty) * restQty, ((itemPo.CustomizedVat) / poItemQty) * restQty, ((itemPo.CustomizedNbt) / poItemQty) * restQty, ((itemPo.CustomizedTotalAmount) / poItemQty) * restQty, ((itemPo.CustomizedAmount)), itemPo.ItemPrice, balanceQuantity, itemPo.IsCustomizedAmount));
                                    }


                                    if (poItemQty != decimal.Parse(sumQtu))
                                    {
                                        decimal ItemPrice = itemPo.ItemPrice;
                                        decimal SubTotal = ItemPrice * balanceQuantity;
                                        decimal NbtAmount = (SubTotal * 2) / 98;

                                        decimal VatAmount = ((NbtAmount + SubTotal) * 15) / 100;

                                        decimal TotalAmount = SubTotal + NbtAmount + VatAmount;

                                        decimal CustomizedAmount = itemPo.CustomizedAmount;
                                        decimal CustomizedSubTotal = CustomizedAmount * balanceQuantity;
                                        decimal CustomizedNbt = (CustomizedSubTotal * 2) / 98;

                                        decimal CustomizedVat = (CustomizedNbt + CustomizedSubTotal) * 15 / 100;

                                        decimal CustomizedTotalAmount = CustomizedSubTotal + CustomizedNbt + CustomizedVat;


                                        grnToGenerateItemList.Add(new GrnToGenerateItemList(itemPo.PoId, itemPo.CategoryName, itemGrn.ItemId, itemPo.SubCategoryName, itemPo.ItemName, VatAmount, NbtAmount, TotalAmount, CustomizedVat, CustomizedNbt, CustomizedTotalAmount, itemPo.CustomizedAmount, itemPo.ItemPrice, balanceQuantity, itemPo.IsCustomizedAmount));
                                    }
                                }
                            }
                        }
                        else
                        {
                            grnToGenerateItemList.Add(new GrnToGenerateItemList(itemPo.PoId, itemPo.CategoryName, itemPo.ItemId, itemPo.SubCategoryName, itemPo.ItemName, itemPo.VatAmount, itemPo.NbtAmount, itemPo.TotalAmount, itemPo.CustomizedVat, itemPo.CustomizedNbt, itemPo.CustomizedTotalAmount, itemPo.CustomizedAmount, itemPo.ItemPrice, itemPo.Quantity, itemPo.IsCustomizedAmount));
                        }
                    }
                    gvPRDetails.DataSource = grnToGenerateItemList.GroupBy(x => x.ItemId).Select(y => y.First()).ToList();
                    gvPRDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class GrnToGenerateItemListPO
        {
            public GrnToGenerateItemListPO(int PoID, string CategoryName, int ItemId, string SubCategoryName, string ItemName, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, decimal CustomizedVat, decimal CustomizedNbt, decimal CustomizedTotalAmount, decimal CustomizedAmount, decimal ItemPrice, decimal Quantity, int IsCustomizedAmount, int BasedPr, string SupplierName, string POCode, string PrCode)
            {
                poID = PoID;
                categoryName = CategoryName;
                itemId = ItemId;
                subCategoryName = SubCategoryName;
                itemName = ItemName;
                vatAmount = VatAmount;
                nbtAmount = NbtAmount;
                totalAmount = TotalAmount;
                customizedVat = CustomizedVat;
                customizedNbt = CustomizedNbt;
                customizedTotalAmount = CustomizedTotalAmount;
                customizedAmount = CustomizedAmount;
                itemPrice = ItemPrice;
                quantity = Quantity;
                isCustomizedAmount = IsCustomizedAmount;
                basedPr = BasedPr;
                supplierName = SupplierName;
                poCode = POCode;
                prCode = PrCode;
            }

            private int poID;
            private string categoryName;
            private int itemId;
            private string subCategoryName;
            private string itemName;
            private decimal vatAmount;
            private decimal nbtAmount;
            private decimal totalAmount;
            private decimal customizedVat;
            private decimal customizedNbt;
            private decimal customizedTotalAmount;
            private decimal customizedAmount;
            private decimal itemPrice;
            private decimal quantity;
            private int isCustomizedAmount;
            private int basedPr;
            private string supplierName;
            private string poCode;
            private string prCode;

            public int PoID
            {
                get { return poID; }
                set { poID = value; }
            }

            public string CategoryName
            {
                get { return categoryName; }
                set { categoryName = value; }
            }

            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            public string SubCategoryName
            {
                get { return subCategoryName; }
                set { subCategoryName = value; }
            }

            public string ItemName
            {
                get { return itemName; }
                set { itemName = value; }
            }

            public decimal VatAmount
            {
                get { return vatAmount; }
                set { vatAmount = value; }
            }

            public decimal NbtAmount
            {
                get { return nbtAmount; }
                set { nbtAmount = value; }
            }

            public decimal TotalAmount
            {
                get { return totalAmount; }
                set { totalAmount = value; }
            }

            public decimal CustomizedVat
            {
                get { return customizedVat; }
                set { customizedVat = value; }
            }

            public decimal CustomizedNbt
            {
                get { return customizedNbt; }
                set { customizedNbt = value; }
            }

            public decimal CustomizedTotalAmount
            {
                get { return customizedTotalAmount; }
                set { customizedTotalAmount = value; }
            }

            public decimal CustomizedAmount
            {
                get { return customizedAmount; }
                set { customizedAmount = value; }
            }

            public decimal ItemPrice
            {
                get { return itemPrice; }
                set { itemPrice = value; }
            }

            public decimal Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }

            public int IsCustomizedAmount
            {
                get { return isCustomizedAmount; }
                set { isCustomizedAmount = value; }
            }

            public int BasedPr
            {
                get { return basedPr; }
                set { basedPr = value; }
            }

            public string SupplierName
            {
                get { return supplierName; }
                set { supplierName = value; }
            }

            public string POCode
            {
                get { return poCode; }
                set { poCode = value; }
            }

            public string PrCode
            {
                get { return prCode; }
                set { prCode = value; }
            }

        }
        public class GrnToGenerateItemList
        {
            public GrnToGenerateItemList(int PoID, string CategoryName, int ItemId, string SubCategoryName, string ItemName, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, decimal CustomizedVat, decimal CustomizedNbt, decimal CustomizedTotalAmount, decimal CustomizedAmount, decimal ItemPrice, decimal Quantity, int IsCustomizedAmount)
            {
                poID = PoID;
                categoryName = CategoryName;
                itemId = ItemId;
                subCategoryName = SubCategoryName;
                itemName = ItemName;
                vatAmount = VatAmount;
                nbtAmount = NbtAmount;
                totalAmount = TotalAmount;
                customizedVat = CustomizedVat;
                customizedNbt = CustomizedNbt;
                customizedTotalAmount = CustomizedTotalAmount;
                customizedAmount = CustomizedAmount;
                itemPrice = ItemPrice;
                quantity = Quantity;
                isCustomizedAmount = IsCustomizedAmount;
            }

            private int poID;
            private string categoryName;
            private int itemId;
            private string subCategoryName;
            private string itemName;
            private decimal vatAmount;
            private decimal nbtAmount;
            private decimal totalAmount;
            private decimal customizedVat;
            private decimal customizedNbt;
            private decimal customizedTotalAmount;
            private decimal customizedAmount;
            private decimal itemPrice;
            private decimal quantity;
            private int isCustomizedAmount;

            public int PoID
            {
                get { return poID; }
                set { poID = value; }
            }

            public string CategoryName
            {
                get { return categoryName; }
                set { categoryName = value; }
            }

            public int ItemId
            {
                get { return itemId; }
                set { itemId = value; }
            }

            public string SubCategoryName
            {
                get { return subCategoryName; }
                set { subCategoryName = value; }
            }

            public string ItemName
            {
                get { return itemName; }
                set { itemName = value; }
            }

            public decimal VatAmount
            {
                get { return vatAmount; }
                set { vatAmount = value; }
            }

            public decimal NbtAmount
            {
                get { return nbtAmount; }
                set { nbtAmount = value; }
            }

            public decimal TotalAmount
            {
                get { return totalAmount; }
                set { totalAmount = value; }
            }

            public decimal CustomizedVat
            {
                get { return customizedVat; }
                set { customizedVat = value; }
            }

            public decimal CustomizedNbt
            {
                get { return customizedNbt; }
                set { customizedNbt = value; }
            }

            public decimal CustomizedTotalAmount
            {
                get { return customizedTotalAmount; }
                set { customizedTotalAmount = value; }
            }

            public decimal CustomizedAmount
            {
                get { return customizedAmount; }
                set { customizedAmount = value; }
            }

            public decimal ItemPrice
            {
                get { return itemPrice; }
                set { itemPrice = value; }
            }

            public decimal Quantity
            {
                get { return quantity; }
                set { quantity = value; }
            }

            public int IsCustomizedAmount
            {
                get { return isCustomizedAmount; }
                set { isCustomizedAmount = value; }
            }
        }
    }
}