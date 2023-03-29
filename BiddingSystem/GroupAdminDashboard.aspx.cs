using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web.Services;
using CLibrary.Domain;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Globalization;

namespace BiddingSystem
{
    public partial class GroupAdminDashboard : System.Web.UI.Page
    {
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        //AddItemController addItemController = ControllerFactory.CreateAddItemController();
        //ItemImageUploadController itemImageUploadController = ControllerFactory.CreateItemImageUploadController();

        List<AddItem> _additems = new List<AddItem>();
       public static string ComapnyId = string.Empty;
        static string UserId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if ( Session["UserId"] != null)
            {
              //  ((BiddingAdmin)Page.Master).mainTabValue = "GroupAdminDashboard.aspx";
              
                UserId = Session["UserId"].ToString();
               // _additems = addItemController.FetchItemList();

            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
          
            if (!IsPostBack)
            {
               
                ddlCompany.DataSource = companyDepartmentController.GetDepartmentList().Where(x => x.IsActive == 1).ToList();
                ddlCompany.DataValueField = "DepartmentID";
                ddlCompany.DataTextField = "DepartmentName";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select A Company", ""));

            }
            
            

        }
        protected void btnExpierBid_Click(object sender, EventArgs e)
        {
            int update = biddingController.UpdateExpierBids(int.Parse(ComapnyId));

            if (update > 0)
            {
                lblBiddingExpireMsg.Text = "Expired successfully";
            }
            else
            {
                lblBiddingExpireMsg.Text = "Expired unsuccessful";
            }
        }

        protected void PRChartLoad()
        {
            
            PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
            ChartArea chartArea1 = new ChartArea();
            chartArea1.AxisX.Title = "Months";
            chartArea1.AxisY.Title = "PR Count";
            chrtPR.ChartAreas.Add(chartArea1);

            Legend legend1 = new Legend();
            chrtPR.Legends.Add(legend1);

            Series series1 = new Series();
            series1.ChartType = SeriesChartType.Line;
            series1.Name = "This_Year";
            series1.Color = Color.Blue;
            series1.BorderWidth = 4;
            chrtPR.Series.Add(series1);

            var valueList = pR_MasterController.countTotalPrtochart(int.Parse(ComapnyId), LocalTime.Today.Year).OrderBy(x => DateTime.ParseExact(x.monthName, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
            List<int> prcount = new List<int>();
            List<string> monthName = new List<string>();
            if (valueList.Count() != 0)
            {
                foreach (var item in valueList)
                {
                    prcount.Add(int.Parse(item.prCount));
                    monthName.Add(item.monthName);
                }
                chrtPR.Series["This_Year"].Points.DataBindXY(monthName, prcount);   
            }
            else
            {
                chrtPR.Series["This_Year"].Points.AddXY(LocalTime.Today.ToString("MMMM"), 0);
            }

            var valueList2 = pR_MasterController.countTotalPrtochart(int.Parse(ComapnyId), LocalTime.Today.Year - 1).OrderBy(x => DateTime.ParseExact(x.monthName, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
            Series series2 = new Series();
            series2.ChartType = SeriesChartType.Line;
            series2.Name = "Last_Year";
            series2.Color = Color.Green;
            series2.BorderWidth = 4;
            chrtPR.Series.Add(series2);
            prcount.Clear();
            monthName.Clear();
            if (valueList2.Count() != 0)
            {
                foreach (var item in valueList2)
                {
                    prcount.Add(int.Parse(item.prCount));
                    monthName.Add(item.monthName);
                }
                chrtPR.Series["Last_Year"].Points.DataBindXY(monthName, prcount);

            }
            else
            {
                chrtPR.Series["Last_Year"].Points.AddXY(LocalTime.Today.ToString("MMMM"), 0);
            }
        }

        protected void BIDChartLoad()
        {
           
            PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
            ChartArea chartArea1 = new ChartArea();
            chartArea1.AxisX.Title = "Months";
            chartArea1.AxisY.Title = "BID Count";
            chrtBid.ChartAreas.Add(chartArea1);

            Legend legend1 = new Legend();
            chrtBid.Legends.Add(legend1);

            Series series1 = new Series();
            series1.ChartType = SeriesChartType.Line;
            series1.Name = "This_Year";
            series1.Color = Color.Blue;
            series1.BorderWidth = 4;
            chrtBid.Series.Add(series1);

            List<int> prcount = new List<int>();
            List<string> monthName = new List<string>();
            var valueList = pR_MasterController.FetchTotalBidforChart(int.Parse(ComapnyId), LocalTime.Today.Year).OrderBy(x => DateTime.ParseExact(x.monthName, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
            if (valueList.Count()!=0)
            {
                foreach (var item in valueList)
                {
                    prcount.Add(int.Parse(item.prCount));
                    monthName.Add(item.monthName);
                }
                chrtBid.Series["This_Year"].Points.DataBindXY(monthName, prcount);
            }

            else
            {

                chrtBid.Series["This_Year"].Points.AddXY(LocalTime.Today.ToString("MMMM"), 0);
            }
          
            
            
            Series series2 = new Series();
            series2.ChartType = SeriesChartType.Line;
            series2.Name = "Last_Year";
            series2.Color = Color.Green;
            series2.BorderWidth = 4;
            chrtBid.Series.Add(series2);
            prcount.Clear();
            monthName.Clear();

            var valueList2 = pR_MasterController.FetchTotalBidforChart(int.Parse(ComapnyId), LocalTime.Today.Year - 1).OrderBy(x => DateTime.ParseExact(x.monthName, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
            if (valueList2.Count() != 0)
            {
                
                foreach (var item in valueList2)
                {
                    prcount.Add(int.Parse(item.prCount));
                    monthName.Add(item.monthName);
                }
                chrtBid.Series["Last_Year"].Points.DataBindXY(monthName, prcount);
            }
            else {

                chrtBid.Series["Last_Year"].Points.AddXY(LocalTime.Today.ToString("MMMM"), 0);
            }
        }
        //protected void btnMakeId_Click(object sender, EventArgs e)
        //{
        //    foreach (var item in _additems)
        //    {
        //        itemImageUploadController.SaveItems(item.ItemId, item.ImagePath);
        //    }
        //}
        
        [WebMethod]
        public static List<string> GetMainSummary()
        {
            List<string> MainDetails = new List<string>();
            SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
            CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

            string noOfCompanyUsers = companyLoginController.GetUserListByDepartmentid(int.Parse(ComapnyId)).Count().ToString("00");
            string noOfSuppliers = supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(int.Parse(ComapnyId)).Count().ToString("00");
            MainDetails.Add(noOfCompanyUsers + "~"+ noOfSuppliers);
            return MainDetails;
        }

        [WebMethod]
        public static List<string> GetPRSummary()
        {
            List<string> prDetails = new List<string>();
            //PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
            //string countPr = pR_MasterController.countTotalPr(int.Parse(ComapnyId)).ToString();
            //string countApprovedPr = pR_MasterController.countApprovedPr(int.Parse(ComapnyId)).ToString();
            //string countPendingPr = pR_MasterController.countPendingPr(int.Parse(ComapnyId)).ToString();
            //string countRejecteddPr = pR_MasterController.countRejectedPr(int.Parse(ComapnyId)).ToString();
            //prDetails.Add(countPr + "~" + countApprovedPr + "~" + countRejecteddPr + "~" + countPendingPr);
            return prDetails;
        }

        [WebMethod]
        public static List<string> GetBidSummary()
        {
            List<PR_Details> _PR_DetailsList = new List<PR_Details>();
            List<SupplierQuotation> supplierQuotationSubmitted = new List<SupplierQuotation>();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
            PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
            PR_DetailController pR_DetailController = ControllerFactory.CreatePR_DetailController();
            BiddingController biddingContrller = ControllerFactory.CreateBiddingController();

            string approveForBidOpening = "0";
            string progressBids = "0";
            string submittedBids = "0";
            string closedBids = "0";
            string totalBids = "0";
            //string rejectedBid = "0";

            totalBids = pR_MasterController.FetchTotalPR(int.Parse(ComapnyId)).Where(x => x.CreatedDateTime.Year.Equals(LocalTime.Today.Year)).Count().ToString();
            submittedBids = pR_MasterController.GetPrListForBidSubmission(int.Parse(ComapnyId)).Where(x => x.CreatedDate.Year.Equals(LocalTime.Today.Year)).Count().ToString();
            approveForBidOpening = pR_MasterController.GetPrListForBidApproval(int.Parse(ComapnyId),int.Parse(UserId)).Where(x => x.CreatedDate.Year.Equals(LocalTime.Today.Year)).Count().ToString();
            progressBids = biddingContrller.GetInProgressBids(int.Parse(ComapnyId)).Where(x => x.CreateDate.Year.Equals(LocalTime.Today.Year)).Count().ToString();
            //rejectedBid = pR_DetailController.FetchBidSubmissionDetails().Where(x => x.IsApproveToViewInSupplierPortal == 0 && x.IsActive == 1 && x.CreatedDateTime.Year.Equals(LocalTime.Today.Year)).Count().ToString();
            closedBids = biddingContrller.GetClosedBids(int.Parse(ComapnyId)).Where(x => x.CreateDate.Year.Equals(LocalTime.Today.Year)).Count().ToString();

            //var objs = (from c in _PR_DetailsList
            //            orderby c.PrId
            //            select c).GroupBy(g => g.PrId).Select(x => x.FirstOrDefault());


            //approveForBidOpening = objs.Count().ToString();

            List<string> bidDetails = new List<string>();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();

            bidDetails.Add(totalBids + "~" + submittedBids + "~" + progressBids + "~" + closedBids + "~" + approveForBidOpening );
            return bidDetails;
        }

        [WebMethod]
        public static List<string> GetPOSummary()
        {
            string totalPO = "0";
            string approvePO = "0";
            string rejectedPo = "0";

            POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
            totalPO = pOMasterController.GetPoMasterListByDepartmentIdViewPO(int.Parse(ComapnyId)).Count().ToString();
            approvePO= pOMasterController.GetPoMasterListByDepartmentIdViewPO(int.Parse(ComapnyId)).Count().ToString();
            rejectedPo = pOMasterController.GetPoMasterRejectedListByDepartmentIdViewPO(int.Parse(ComapnyId)).Count().ToString();
            List<string> poDetails = new List<string>();
            poDetails.Add(totalPO + "~" + approvePO + "~" + rejectedPo);
            return poDetails;
        }

        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCompany.SelectedIndex!=0)
            {

                ComapnyId = ddlCompany.SelectedValue;
                Session["CompanyId"] = ddlCompany.SelectedValue;
                PanelDashboaerd.Enabled = true;
                PanelDashboaerd.Visible = true;
                PRChartLoad();
                BIDChartLoad();

            }
            else
            {
                PanelDashboaerd.Enabled = false;
                PanelDashboaerd.Visible = false;
            }
        }
    }
}