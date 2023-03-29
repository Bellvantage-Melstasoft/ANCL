using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using CLibrary.Common;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CompanyApproveBids : System.Web.UI.Page
    {
        static List<PurchaseRequest> PurchaseRequestList;
        static List<SupplierResponse> SupplierResponseList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                panelPurchaseRequset.Visible = false;
                //panelSupplierResponse.Visible = false;

                SupplierResponseList = new List<SupplierResponse>();

                SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Abans pvt ltd", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(3), estimateAmountPerUnit = 95000, itemname = "ASUS-ZenBook-UX430UN" });
                SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "MetroPolitian pvt ltd", quantity = 25, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 97000, itemname = "ASUS-ZenBook-UX430UN" });
                SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Singer", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(6), estimateAmountPerUnit = 94000, itemname = "ASUS-ZenBook-UX430UN" });
                SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Softlogic", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(10), estimateAmountPerUnit = 93000, itemname = "ASUS-ZenBook-UX430UN" });

                SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Abans pvt ltd", quantity = 5, estimateDeliveryDate = LocalTime.Now.AddDays(3), estimateAmountPerUnit = 69000, itemname = "Monitor LU28E510DS/ZA" });
                SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "MetroPolitian pvt ltd", quantity = 10, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 70000, itemname = "Monitor LU28E510DS/ZA" });
                SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Singer", quantity = 5, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 72000, itemname = "Monitor LU28E510DS/ZA" });
                SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Softlogic", quantity = 7, estimateDeliveryDate = LocalTime.Now.AddDays(1), estimateAmountPerUnit = 69500, itemname = "Monitor LU28E510DS/ZA" });
                lblItemName.Text = "ASUS-ZenBook-UX430UN";

                var listSupllier = SupplierResponseList.Distinct().Where(y=>y.PRId == 0001).ToList();

                //foreach (int value in listSupllier)
                //{
                //    Console.WriteLine("After: {0}", value);
                //}

                //lblItemName.Text = SupplierResponseList.GroupBy(x => x.PRId).Select(g => g.First()).Distinct().Where(y=>y.PRId == 0001).ToString();
                gvSupplierResponse.DataSource = SupplierResponseList.Where(x=>x.PRId == 001).OrderBy(y=>y.estimateAmountPerUnit);
                gvSupplierResponse.DataBind();
            }
        }


        protected void ddlTypeWise_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTypeWise.SelectedIndex == 1)
            {
                PurchaseRequestList = new List<PurchaseRequest>();
                PurchaseRequestList.Add(new PurchaseRequest { PRId = 0001, itemCode = "ASUS-ZenBook-UX430UN", itemDescription = "NVIDIA GeForce GTX 1070 8GB graphics card and Intel Core i7-7770HQ processor at 2.8 GHz (3.8 GHz overclocked), 12GB of DDR4 RAM and 128GB SSD", estimateAmountPerUnit = 96000, quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(10) });
                PurchaseRequestList.Add(new PurchaseRequest { PRId = 0002, itemCode = " Monitor LU28E510DS/ZA", itemDescription = "28\" UE510 UHD,Samsung Monitor", estimateAmountPerUnit = 70000, quantity = 5, estimateDeliveryDate = LocalTime.Now.AddDays(5) });


                panelPurchaseRequset.Visible = true;
                //panelSupplierResponse.Visible = false;
                gvPurchaseRequest.DataSource = PurchaseRequestList;
                gvPurchaseRequest.DataBind();
            }
            else
            {
                panelPurchaseRequset.Visible = false;
                //panelSupplierResponse.Visible = false;
                PurchaseRequestList = new List<PurchaseRequest>();
                SupplierResponseList = new List<SupplierResponse>();
            }

        }

        public class PurchaseRequest
        {
            public int PRId { get; set; }
            public string itemCode { get; set; }
            public string itemDescription { get; set; }
            public int quantity { get; set; }
            public DateTime estimateDeliveryDate { get; set; }
            public int estimateAmountPerUnit { get; set; }
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

        //protected void lbtnView_Click(object sender, EventArgs e)
        //{
        //    SupplierResponseList = new List<SupplierResponse>();

        //    SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Abans pvt ltd", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(3), estimateAmountPerUnit = 95000, itemname = "ASUS-ZenBook-UX430UN" });
        //    SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "MetroPolitian pvt ltd", quantity = 25, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 97000, itemname = "ASUS-ZenBook-UX430UN" });
        //    SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Singer", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(6), estimateAmountPerUnit = 94000, itemname = "ASUS-ZenBook-UX430UN" });
        //    SupplierResponseList.Add(new SupplierResponse { PRId = 0001, supplierName = "Softlogic", quantity = 30, estimateDeliveryDate = LocalTime.Now.AddDays(10), estimateAmountPerUnit = 93000, itemname = "ASUS-ZenBook-UX430UN" });

        //    SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Abans pvt ltd", quantity = 5, estimateDeliveryDate = LocalTime.Now.AddDays(3), estimateAmountPerUnit = 69000, itemname = "Monitor LU28E510DS/ZA" });
        //    SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "MetroPolitian pvt ltd", quantity = 10, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 70000, itemname = "Monitor LU28E510DS/ZA" });
        //    SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Singer", quantity = 5, estimateDeliveryDate = LocalTime.Now.AddDays(4), estimateAmountPerUnit = 72000, itemname = "Monitor LU28E510DS/ZA" });
        //    SupplierResponseList.Add(new SupplierResponse { PRId = 0002, supplierName = "Softlogic", quantity = 7, estimateDeliveryDate = LocalTime.Now.AddDays(1), estimateAmountPerUnit = 69500, itemname = "Monitor LU28E510DS/ZA" });



        //    int r = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        //    int ID = int.Parse(gvPurchaseRequest.Rows[r].Cells[0].Text);
        //    //panelSupplierResponse.Visible = true;

        //    //gvSupplierResponse.DataSource = SupplierResponseList.Where(x => x.PRId == ID).OrderBy(y => y.estimateAmountPerUnit).ToList();
        //    //gvSupplierResponse.DataBind();
        //}


        protected void chkSelect_OnCheckedChanged(object sender, System.EventArgs e)
        {
            //foreach (GridViewRow oldrow in gvSupplierResponse.Rows)
            //{
            //    ((CheckBox)oldrow.FindControl("chkSelect")).Checked = false;
            //}


            //Set the new selected row
            CheckBox rb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((CheckBox)row.FindControl("chkSelect")).Checked = true;

        }

    }
}