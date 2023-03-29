using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Common;

namespace BiddingSystem
{
    public partial class CustomerPRViewOnly : System.Web.UI.Page
    {
        static List<PurchaseRequest> PurchaseRequestList;
        static List<SupplierResponse> SupplierResponseList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PurchaseRequestList = new List<PurchaseRequest>();
                PurchaseRequestList.Add(new PurchaseRequest { PRId = "PR001", departments = "IT Department", quotationfor = "Purchase a laptop for Melsta Logistics", createddate = "2018/04/30"});
                PurchaseRequestList.Add(new PurchaseRequest { PRId = "PR002", departments = "Maintenance", quotationfor = "Purchase a laptop for Melsta Logistics", createddate = "2018/05/05"});
                PurchaseRequestList.Add(new PurchaseRequest { PRId = "PR003", departments = "Callcenter", quotationfor = "Purchase wire for Melsta Logistics", createddate = "2018/05/15"});
                PurchaseRequestList.Add(new PurchaseRequest { PRId = "PR004", departments = "packaging", quotationfor = "Purchase metal boxes for Melsta Logistics", createddate = "2018/05/16" });




                gvPurchaseRequest.DataSource = PurchaseRequestList;
                gvPurchaseRequest.DataBind();

            }
        }


        protected void lbtnView_Click(object sender, EventArgs e)
        {
            Response.Redirect("CustomerViewPurchaseRequisition_1.aspx"); 
        }

        public class PurchaseRequest
        {
            public string PRId { get; set; }
            public string departments { get; set; }
            public string quotationfor { get; set; }
            public string createddate { get; set; }
        }


        public class SupplierResponse
        {
            public int PRId { get; set; }
            public string supplierName { get; set; }
            public int quantity { get; set; }
            public DateTime estimateDeliveryDate { get; set; }
            public int estimateAmountPerUnit { get; set; }
            public string itemname { get; set; }

        }

        protected void lbtnView_Click1(object sender, EventArgs e)
        {
            SupplierResponseList = new List<SupplierResponse>();

            SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Abans pvt ltd", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(3), estimateAmountPerUnit = 95000, itemname = "ASUS-ZenBook-UX430UN" });
            SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "MetroPolitian pvt ltd", quantity = 25, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 97000, itemname = "ASUS-ZenBook-UX430UN" });
            SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Singer", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(6), estimateAmountPerUnit = 94000, itemname = "ASUS-ZenBook-UX430UN" });
            SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Softlogic", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(10), estimateAmountPerUnit = 93000, itemname = "ASUS-ZenBook-UX430UN" });

            SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Abans pvt ltd", quantity = 5, estimateDeliveryDate = LocalTime.Now.AddDays(3), estimateAmountPerUnit = 69000, itemname = "Monitor LU28E510DS/ZA" });
            SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "MetroPolitian pvt ltd", quantity = 10, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 70000, itemname = "Monitor LU28E510DS/ZA" });
            SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Singer", quantity = 5, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 72000, itemname = "Monitor LU28E510DS/ZA" });
            SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Softlogic", quantity = 7, estimateDeliveryDate = LocalTime.Now.AddDays(1), estimateAmountPerUnit = 69500, itemname = "Monitor LU28E510DS/ZA" });
            
        }
        
    }
}