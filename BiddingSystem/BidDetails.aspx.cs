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
using System.IO;

namespace BiddingSystem
{
    public partial class BidDetails : System.Web.UI.Page
    {
        string ParameterId = string.Empty;
        string PrId = string.Empty;
        string ItemId = string.Empty;
        public string ImagePath = string.Empty;
        public string EndTime = string.Empty;
        public string BidOrderId = string.Empty;
        int supplierId = 0;
        
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

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
            string satatusPending =  Request.QueryString.Get("Status");

            if (satatusPending == "P")
            {
               
               // divVisbilityBtn.Visible = false;
            }
            else {
               // divVisbilityBtn.Visible = true;
            }

            //----------Get the Selected Bid Values By Querystring (PrId, ItemId, BidOrderId)
            ParameterId = Request.QueryString.Get("Info");
            if (ParameterId != "")
            {
                string[] value = ParameterId.Split('_');
                PrId = value[0];
                ItemId = value[1];
                BidOrderId = value[2];
            }

            if(!IsPostBack){
                try
                {
                   Bidding _biddingList = biddingController.GetBiddingDetails(int.Parse(PrId), int.Parse(ItemId));
                   lblCompanyName.Text = _biddingList.DepartmentName;
                   lblItemName.Text = _biddingList.ItemName;
                   lblItemNamePopup.Text = _biddingList.ItemName;
                   ImagePath = _biddingList.ImagePath;
                   EndTime = _biddingList.EndDate.ToString();
                   lblDescription.Text = _biddingList.ItemDescription;
                   lblTermsConditions.Text = _biddingList.BidTermsAndConditions;
                   gvUploadFiles.DataSource = pr_FileUploadController.FtechUploadeFiles(int.Parse(PrId), int.Parse(ItemId));
                   gvUploadFiles.DataBind();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //----------------Pass to the Client Side Popup
                getBOMtData();
                msg.Visible = false;
            }
        }

        //-----------------------Pass data to the client Side - Image Path
        public string getJsonItemImagePath()
        {
            var Image = ImagePath;
            return (new JavaScriptSerializer()).Serialize(Image);
        }

        //-----------------------Pass data to the client Side - End Date
        public string getJsonEndDateTime()
        {
            var EndDate = EndTime;
            return (new JavaScriptSerializer()).Serialize(EndDate);
        }

        //------------------BOM Bin Html Table
        public string getBOMtData()
        {
            try
            {
                string data = "";
                List<PR_BillOfMeterial> _pr_BillOfMeterial = pr_BillOfMeterialController.GetList(int.Parse(PrId), int.Parse(ItemId));
                if (_pr_BillOfMeterial.ToList().Count > 0)
                {
                    foreach (var item in _pr_BillOfMeterial)
                    {
                        int SeqNo = item.SeqId;
                        string Meterial = item.Meterial;
                        string Description = item.Description;
                        data += "<tr><td>" + SeqNo + "</td><td>" + Meterial + "</td><td>" + Description + "</td></tr>";
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //------------If Supplier Select Bid Later 
        //------------Mechanism (DataBase Table :  SupplierQuotation)
        //------------SupplierQuotation : IsSelected Status become 1 : IsSelected : 1
        //------------If supplier initially choosed to bid Later IsSelected : 1 , If some how supplier bid for that later bid flag is also in IsSeleted = 1
        //------------We are keeping that IsSeleted status to keep the supplier's bidding flow as a histroy
        //------------Then we can identify supplier intially choosed to bid later and after that he/she put the bid
        protected void btnLater_Click(object sender, EventArgs e)
        {
            try
            {
                int BidSave =  supplierQuotationController.SaveQuatation(int.Parse(ItemId), int.Parse(PrId), supplierId, 0, 1, "", 0, 0, 0, 0, "", 0,"",BidOrderId,0);
                Bidding _biddingList = biddingController.GetBiddingDetails(int.Parse(PrId), int.Parse(ItemId));
                if(BidSave > 0)
                {
                    ImagePath = _biddingList.ImagePath;
                    EndTime = _biddingList.EndDate.ToString();
                    lblSussess.Visible = true;
                    lblError.Visible = false;
                    lblSussess.Text = "Pending Bid Saved Successfully.";
                }
                else{
                    ImagePath = _biddingList.ImagePath;
                    EndTime = _biddingList.EndDate.ToString();
                    lblSussess.Visible = false;
                    lblError.Visible = true;
                    lblError.Text = "Pending Bid Submit Failed.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnApplyNow_Click(object sender, EventArgs e)
        {
            try
            {
               Bidding _biddingList = biddingController.GetBiddingDetails(int.Parse(PrId), int.Parse(ItemId));
               //-------Keep the image visible 
               ImagePath = _biddingList.ImagePath;

               Response.Redirect("BidSubmission.aspx?Info=" + ParameterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnView_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                string filepath = gvUploadFiles.Rows[x].Cells[4].Text;
                System.Diagnostics.Process.Start(HttpContext.Current.Server.MapPath(filepath));
            }
            catch (Exception)
            {
                DisplayMessage("File Not be Found", true);
            }
        }

        protected void btnDownload_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
                string filepath = gvUploadFiles.Rows[x].Cells[4].Text;
                if (!string.IsNullOrEmpty(filepath) && File.Exists(HttpContext.Current.Server.MapPath(filepath)))
                {
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + Path.GetFileName(filepath));
                    HttpContext.Current.Response.WriteFile(HttpContext.Current.Server.MapPath(filepath));
                    HttpContext.Current.Response.End();
                }
                else
                {
                    DisplayMessage("File Not be Found", true);
                }
            }
            catch (Exception ex)
            {
                DisplayMessage("File Not be Found", true);
                throw ex;
            }
        }

        //-------------Display message 
        private void DisplayMessage(string message, bool isError)
        {
            msg.Visible = true;
            if (isError)
            {
                lbMessage.CssClass = "failMessage";
                msg.Attributes["class"] = "alert alert-danger alert-dismissable";
            }
            else
            {
                lbMessage.CssClass = "successMessage";
                msg.Attributes["class"] = "alert alert-success alert-dismissable";
            }
            lbMessage.Text = message;
        }
    }
}