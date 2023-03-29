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
    public partial class CustomerViewApprovedPurchaseOrder : System.Web.UI.Page
    {

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

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                if (!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 9) && companyLogin.Usertype != "S")
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
                try
                {

                    if (Session["UserWarehouses"] != null)
                    {
                        if ((Session["UserWarehouses"] as List<UserWarehouse>).Count > 0)
                        {
                            gvPurchaseOrder.DataSource = pOMasterController.GetPoMasterListByWarehouseIdTogrn((Session["UserWarehouses"] as List<UserWarehouse>).Select(w => w.WrehouseId).ToList());
                            gvPurchaseOrder.DataBind();
                        }
                        else
                        {
                            gvPurchaseOrder.DataSource = pOMasterController.GetPoMasterListByDepartmentIdTogrn(int.Parse(Session["CompanyId"].ToString()));
                            gvPurchaseOrder.DataBind();
                        }
                    }
                    else
                    {
                        gvPurchaseOrder.DataSource = pOMasterController.GetPoMasterListByDepartmentIdTogrn(int.Parse(Session["CompanyId"].ToString()));
                        gvPurchaseOrder.DataBind();
                    }



                    //GetGrnDetailsAll
                    //List<GrnMaster> GrnMasterListByCompanyId = new List<GrnMaster>();
                    //GrnMasterListByCompanyId = grnController.GetGRNmasterListByDepartmentId(CompanyId);
                    //gvRejectedGrns.DataSource = GrnMasterListByCompanyId.Where(x => x.IsApproved == 2).ToList();
                    //gvRejectedGrns.DataBind();


                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                int PoId = 0;
                int PurchaseType = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[1].Text);
                PurchaseType = int.Parse(gvPurchaseOrder.Rows[x].Cells[8].Text);
                if (PurchaseType == 1) {
                    Response.Redirect("GenerateGRNNew.aspx?PoId=" + PoId);
                }
                else {
                    Response.Redirect("GenerateGRNNewImport.aspx?PoId=" + PoId);
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                int GrnId = 0;
                int PoID = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                GrnId = int.Parse(gvRejectedGrns.Rows[x].Cells[0].Text);
                PoID = int.Parse(gvRejectedGrns.Rows[x].Cells[1].Text);
                Session["GrnID"] = GrnId;
                Session["PoID"] = PoID;
                Response.Redirect("EditRejectedGRN.aspx");



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string poid = gvPurchaseOrder.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvPoDetails = e.Row.FindControl("gvPoDetails") as GridView;

                    List<PODetails> poDetails = poDetailsController.GetPOdetailsListBypoid(int.Parse(poid), int.Parse(Session["CompanyId"].ToString()));
                    
                    gvPoDetails.DataSource = poDetails;
                    gvPoDetails.DataBind();


                    //gvPoDetails.DataSource = grnToGenerateItemList.GroupBy(x => x.ItemId).Select(y => y.First()).ToList();

                    //gvPoDetails.DataBind();
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