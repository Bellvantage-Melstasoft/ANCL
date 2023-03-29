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
using System.Data;

namespace BiddingSystem
{
    public partial class BidDetaildWise : System.Web.UI.Page
    {
        public BiddingController biddingController = ControllerFactory.CreateBiddingController();
        public SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

        PR_FileUploadController pR_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        PR_Replace_FileUploadController pR_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();

        List<Bidding> BiddingList = new List<Bidding>();
        public List<string> BiddingAllListStr = new List<string>();

        int supplierId = 0;
        string PrId = string.Empty;
        string ItemId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["supplierId"] != null && Session["supplierId"].ToString() != "")
            {
                supplierId = int.Parse(Session["supplierId"].ToString());
            }
            else
            {
                Response.Redirect("LoginPageSupplier.aspx");
            }

            if (!IsPostBack)
            {
                try
                {
                    string imageFilePath = string.Empty;
                    //----------Get All Bids Opened
                    BiddingList = biddingController.GetAllBidding(supplierId).Where(x => x.IsApproveToViewInSupplierPortal == 1).ToList();

                    foreach (var biddingList in BiddingList)
                    {
                        //----------If conditions check the admin selected image to display in the supplier portal (Replacement , Standard, Default image)
                        if (biddingList.defaultImageLocationCode == 1)
                        {
                            PR_FileUpload pr_FileUploadObj = pR_FileUploadController.fetchPr_FileuploadObjForDefaultImage(biddingList.PrId, biddingList.ItemId);
                            if (pr_FileUploadObj.FilePath != "")
                            {
                                biddingList.ImagePath = pr_FileUploadObj.FilePath;
                            }
                        }
                        if (biddingList.defaultImageLocationCode == 2)
                        {
                            PR_Replace_FileUpload pr_ReplaceUploadObj = pR_Replace_FileUploadController.fetchPR_Replace_FileUploadObjForDefaultImage(biddingList.PrId, biddingList.ItemId);
                            if (pr_ReplaceUploadObj.FilePath != "")
                            {
                                biddingList.ImagePath = pr_ReplaceUploadObj.FilePath;
                            }
                        }

                        //--------------Get Supplier Bid Status
                        SupplierQuotation supplierQuotation = supplierQuotationController.GetGivenQuotatios(biddingList.PrId, biddingList.ItemId, supplierId);

                        decimal amount = supplierQuotation.PerItemPrice;
                        int isPending = supplierQuotation.IsSelected;

                        //-----------Condtions are used to check the supplier Portal Button text (Eg: "Edit Bid","Pending Bid","Bid" )
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
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //Bind dummy datatable to grid view to bind data in it
                BindDummyItem(); 
            }
        }

        //---------------Add Specification values to DataTable
        public void BindDummyItem()
        {
            try
            {
                DataTable dtGetData = new DataTable();
                dtGetData.Columns.Add("PrId");
                dtGetData.Columns.Add("ItemId");
                dtGetData.Columns.Add("SeqId");
                dtGetData.Columns.Add("Meterial");
                dtGetData.Columns.Add("Description");
                dtGetData.Rows.Add();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //----------------------Pass Data toClient Side All Opened Bids--------------------------
        public string getJsonBiddingItemListAll()
        {
            var DataList = BiddingAllListStr;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        //--------------------View Item Specification ClickEvent
        [WebMethod]
        public static DetailsClass[] GetPRIDandItemId(string data)
        {
            List<DetailsClass> Detail = new List<DetailsClass>();

            string jsonData = data;
            string PrId = string.Empty;
            string ItemId = string.Empty;

            if (jsonData != "")
            {
                string[] value = jsonData.Split('_');
                string val1 = value[0];
                string val2 = value[1];
                PrId = val1;
                ItemId = val2;

                PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
                List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetList(int.Parse(PrId), int.Parse(ItemId));

                foreach (var item in pr_BillOfMeterial)
                {
                    DetailsClass DataObj = new DetailsClass();
                    DataObj.PrId = item.PrId.ToString();
                    DataObj.ItemId = item.ItemId.ToString();
                    DataObj.SeqId = item.SeqId.ToString();
                    DataObj.Meterial = item.Meterial;
                    DataObj.Description = item.Description;
                    Detail.Add(DataObj);
                }
            }

            return Detail.ToArray();
        }

        //----------Class for binding data
        public class DetailsClass 
        {
            public string PrId { get; set; }
            public string ItemId { get; set; }
            public string SeqId { get; set; }
            public string Meterial { get; set; }
            public string Description { get; set; }
        }
    }
}