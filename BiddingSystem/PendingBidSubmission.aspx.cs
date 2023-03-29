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

namespace BiddingSystem
{
    public partial class PendingBidSubmission : System.Web.UI.Page
    {
        int supplierId = 0;
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
        List<SupplierQuotation> supplierQuotation = new List<SupplierQuotation>();
        public List<string> PendingDetails = new List<string>();
        public string PendingBidCount = string.Empty;
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

            if(!IsPostBack){
                try
                {
                    supplierQuotation = supplierQuotationController.GetSupplierPendingBids(supplierId);
                    foreach (var item in supplierQuotation)
                    {
                        PendingDetails.Add(item.PrID+"-"+item.ItemId+"-"+item.ItemName+"-"+item.EndDate+"-"+item.ImagePath+"-" + item.DepartmentName +"-"+item.BidOpeningId);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                PendingBidCount = supplierQuotation.Count().ToString();
            }
        }

        //----------------------Pass Data toClient Side--------------------------
        public string getJsonBiddingPendingItemList()
        {
            var DataList = PendingDetails;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

        public string getJsonBiddingPendingBidCount()
        {
            var DataListPendingBidCount = PendingBidCount;
            return (new JavaScriptSerializer()).Serialize(DataListPendingBidCount);
        }
    }
}