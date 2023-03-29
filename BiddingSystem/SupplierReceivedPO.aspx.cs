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
    public partial class SupplierReceivedPO : System.Web.UI.Page
    {
        string PrId = string.Empty;
        string ItemId = string.Empty;
        int supplierId = 0;
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        public List<string> ReceivedPO = new List<string>();

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
                    GetSupplierReceivedPO();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void GetSupplierReceivedPO()
        {
            try
            {
                List<Bidding> bidding =  biddingController.GetRaisedPOSupplier(supplierId);

                foreach (var item in bidding)
                {
                    ReceivedPO.Add(item.PoID+ "-" +item.POCode + "-" + item.CreatedDate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string getJsonReceived()
        {
            var DataList = ReceivedPO;
            return (new JavaScriptSerializer()).Serialize(DataList);
        }

    }
}