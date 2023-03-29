using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;

namespace BiddingSystem
{
    public partial class SupplierViewPODetails : System.Web.UI.Page
    {
        int supplierId = 0;
        string poid = string.Empty;

        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();

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
                    string poIdQryStr = Request.QueryString.Get("Info");
                    if(poIdQryStr != "")
                    {
                        poid = poIdQryStr;
                        GetPODetails();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        private void GetPODetails() {
            try
            {
                POMaster pOMaster = pOMasterController.GetPoMasterObjByPoIdView(int.Parse(poid));
                gvPurchaseOrderItems.DataSource = pOMaster._PODetails;
                gvPurchaseOrderItems.DataBind();

                decimal SubTotal = 0;
                decimal VatTotal = 0;
                decimal NbtTotal = 0;
                decimal SubTotalCus = 0;
                decimal VatTotalCus = 0;
                decimal NbtTotalCus = 0;
                decimal TotalVat = 0;
                decimal TotalNbt = 0;
                decimal TotalSubAmount = 0;
                decimal TotalAmount = 0;

                foreach (var item in pOMaster._PODetails)
                {
                    if (item.IsCustomizedAmount == 1)
                    {
                        SubTotalCus =SubTotalCus + item.CustomizedAmount * item.Quantity;
                        VatTotalCus =VatTotalCus + item.CustomizedVat;
                        NbtTotalCus =NbtTotalCus + item.CustomizedNbt;
                    }

                    if (item.IsCustomizedAmount == 0)
                    {
                        SubTotal = SubTotal + item.ItemPrice * item.Quantity;
                        VatTotal = VatTotal + item.VatAmount;
                        NbtTotal = NbtTotal + item.NbtAmount;
                    }

                    TotalSubAmount = SubTotal + SubTotalCus;
                    TotalNbt = NbtTotal + NbtTotalCus;
                    TotalVat = VatTotalCus + VatTotal;

                    TotalAmount = TotalSubAmount + TotalNbt + TotalVat;

                    
                }
                lblSubtotal.Text = TotalSubAmount.ToString("n");
                lblVatTotal.Text = TotalVat.ToString("n");
                lblNbtTotal.Text = TotalNbt.ToString("n");
                lblTotal.Text = TotalAmount.ToString("n");
               
                //int PoId = 0;
                //int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                //PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                //Session["PoId"] = PoId;
                //Response.Redirect("CustomerViewPurchaseOrder.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }   
        }
    }
}