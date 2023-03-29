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
using System.Data;
using static BiddingSystem.CustomerGRNView;

namespace BiddingSystem
{
    public partial class AdminDashboard : System.Web.UI.Page
    {
        BiddingController biddingController = ControllerFactory.CreateBiddingController();
        MRNControllerInterface mrnController = ControllerFactory.CreateMRNController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        POMasterController pOMasterController = ControllerFactory.CreatePOMasterController();
        ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        GrnController grnController = ControllerFactory.CreateGrnController();
        DashboardController dashboardController = ControllerFactory.CreateDashboardController();
      //  AddItemController addItemController = ControllerFactory.CreateAddItemController();
        //ItemImageUploadController itemImageUploadController = ControllerFactory.CreateItemImageUploadController();

       // List<AddItem> _additems = new List<AddItem>();
        //static string ComapnyId = string.Empty;
       // static string UserId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
      

            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "AdminDashboard.aspx";

                //ComapnyId = Session["CompanyId"].ToString();
                //UserId = Session["UserId"].ToString();
                // _additems = addItemController.FetchItemList();

            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }

            if (!IsPostBack)
            {
                //PRChartLoad();
           //     BIDChartLoad();
                LoadYear();

                int yearsearch= Convert.ToInt32(ViewState["Year"]);

                if (yearsearch == 0) {
                    yearsearch = LocalTime.Now.Year;
                }

                GetData(yearsearch);
                
                var SubDepartmentID = 0;
                if (Session["SubDepartmentID"]!=null)
                {
                    SubDepartmentID = int.Parse(Session["SubDepartmentID"].ToString());
                }

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                //gvMRN.DataSource = mrnController.fetchMrnList(SubDepartmentID).Where(mrn => mrn.IsApproved == 0).OrderByDescending(x => x.CreatedDate).Take(5).ToList();
                gvMRN.DataSource = dashboardController.fetchMrnList(SubDepartmentID).Where(mrn => mrn.IsApproved == 0).OrderByDescending(x => x.CreatedDate).ToList();
                gvMRN.DataBind();

                if (Session["IsHeadOfWarehouse"] != null && Session["IsHeadOfWarehouse"].ToString() == "1")
                {
                    List<PR_Master> pr_Master = new List<PR_Master>();
                    // pr_Master = pr_MasterController.FetchApprovePRDataByDeptId(int.Parse(Session["CompanyId"].ToString())).OrderByDescending(x => x.PrId).Take(5).ToList();
                    pr_Master = dashboardController.FetchApprovePRDataByDeptId(int.Parse(Session["CompanyId"].ToString())).OrderByDescending(x => x.PrId).ToList();
                    gvPurchaseRequest.DataSource = pr_Master;
                    gvPurchaseRequest.DataBind();
                }
                else
                {
                    gvPurchaseRequest.DataSource = null;
                    gvPurchaseRequest.DataBind();
                }

                if (companyLogin.DesignationId == 14)
                {
                    //var submissionList = pr_MasterController.GetPrListForBidSubmission(int.Parse(ComapnyId)).OrderByDescending(r => r.PrId).Take(5).ToList();
                    var submissionList = dashboardController.GetPrListForBidSubmission(int.Parse(Session["CompanyId"].ToString())).OrderByDescending(r => r.PrId).ToList();
                    List<PR_Master> temp = new List<PR_Master>();
                    foreach (PR_Master item in submissionList)
                    {
                        foreach (var t in pr_MasterController.GetPRDetails(item.PrId, int.Parse(Session["CompanyId"].ToString())))
                        {
                            if (t.SubmitForBid == 1)
                            {
                                temp.Add(item);
                            }
                            else
                            {
                                item.PrDetails = pr_MasterController.GetPRDetails(item.PrId, int.Parse(Session["CompanyId"].ToString()));
                            }
                        }
                    }
                    submissionList.RemoveAll(a => temp.Exists(w => w.PrId == a.PrId));
                    gvsubmitforbid.DataSource = submissionList.OrderByDescending(r => r.PrId).Take(5).ToList(); ;
                    gvsubmitforbid.DataBind();
                }
                else
                {
                    gvsubmitforbid.DataSource = null;
                    gvsubmitforbid.DataBind();
                }

                var Submittedlist = pr_MasterController.GetPrListForBidApproval(int.Parse(Session["CompanyId"].ToString()),int.Parse(Session["UserId"].ToString()));
                // Allowing only assigned Purchasing Offcier 
                foreach (PrMasterV2 item in Submittedlist)
                {
                    item.Bids = pr_MasterController.GetPrForBidApproval(item.PrId, int.Parse(Session["CompanyId"].ToString())).Bids;
                }
                Submittedlist = Submittedlist.Where(x => x.Bids.Any(t => t.PurchasingOfficer == companyLogin.UserId)).ToList();

                // eror comes below line -> after doing all chnages try to fix it
                /*
                gvacceptbids.DataSource = Submittedlist.OrderByDescending(r => r.PrId).Take(5).ToList();
               gvacceptbids.DataBind();


                
                List<POMaster> pOMasterListByDepartmentid = new List<POMaster>();
                //pOMasterListByDepartmentid = pOMasterController.GetPoMasterListByDepartmentId(int.Parse(ComapnyId)).Where(W => W.IsApproved == 0).Take(5).ToList();
                pOMasterListByDepartmentid = dashboardController.GetPoMasterListByDepartmentId(int.Parse(Session["CompanyId"].ToString())).ToList();
                if (Request.QueryString.Get("UserId") != null)
                {
                    var DesignationId = companyLogin.DesignationId;
                    int clickedUserId = int.Parse(Request.QueryString.Get("UserId"));
                    List<ItemCategoryPOApproval> listItemCategoryPOApproval = itemCategoryApprovalController.GetItemCategoryPOApprovalByDesignationId(DesignationId);
                    pOMasterListByDepartmentid = pOMasterListByDepartmentid.Where(p => listItemCategoryPOApproval.Any(p2 => p2.Po_Id == p.PoID)).Take(5).ToList();
                    gvPurchaseOrder.DataSource = pOMasterListByDepartmentid;
                    gvPurchaseOrder.DataBind();
                }
                else
                {
                    gvPurchaseOrder.DataSource = pOMasterListByDepartmentid;
                    gvPurchaseOrder.DataBind();
                }


                int GrnStatus = 0;
                List<GrnMaster> GrnMasterListByCompanyId = new List<GrnMaster>();
                List<GrnMaster> GrnMasterListByCompanyIdDup = new List<GrnMaster>();
                GrnMasterListByCompanyId = grnController.GetAllDetailsGrn(int.Parse(Session["CompanyId"].ToString()));

                List<GrnToApproveList> grnToApproveList = new List<GrnToApproveList>();

                //-----------Check duplicate values in the list
                var query = GrnMasterListByCompanyId.GroupBy(x => x.GrnId)
                           .Where(g => g.Count() > 1)
                           .Select(y => y.Key)
                           .ToList();
                if (query.Count > 0)
                {
                    var grpDupes = from f in GrnMasterListByCompanyId
                                   group f by f.GrnId into grps
                                   where grps.Count() > 1
                                   select grps;

                    foreach (var item in grpDupes)
                    {
                        foreach (var itemsList in item)
                        {
                            GrnMasterListByCompanyIdDup.Add(itemsList);
                        }
                    }


                    GrnMasterListByCompanyId = GrnMasterListByCompanyId.Where(p => !GrnMasterListByCompanyIdDup.Any(x => x.PoId == p.PoId)).ToList();
                    GrnMasterListByCompanyIdDup.RemoveAll(r => r.IsGrnApproved == 1);
                    GrnMasterListByCompanyIdDup.RemoveAll(r => r.IsGrnRaised != 1);

                    var grpDupes1 = from f in GrnMasterListByCompanyIdDup
                                    group f by f.GrnId into grps
                                    where grps.Count() > 1
                                    select grps;


                    var objs = (from c in GrnMasterListByCompanyIdDup
                                orderby c.GrnId
                                select c).GroupBy(g => g.GrnId).Select(x => x.FirstOrDefault());

                    foreach (var item in objs)
                    {
                        if ((item.IsGrnRaised == 2 || item.IsGrnRaised == 0) && item.IsGrnApproved == 0)
                        {
                            GrnStatus = 2;
                        }
                        if ((item.IsGrnRaised == 1) && item.IsGrnApproved == 0)
                        {
                            GrnStatus = 0;
                        }
                        if ((item.IsGrnRaised == 1) && item.IsGrnApproved == 1)
                        {
                            GrnStatus = 1;
                        }
                        grnToApproveList.Add(new GrnToApproveList(item.GrnId, item.PoId, item.POCode, item.PrCode, item.SupplierName, item.GoodReceivedDate, item.IsGrnRaised, item.IsGrnApproved, GrnStatus,item.subdepartment,item.Description));
                    }
                }

                foreach (var item in GrnMasterListByCompanyId)
                {
                    if ((item.IsGrnRaised == 2 || item.IsGrnRaised == 0) && item.IsGrnApproved == 0)
                    {
                        GrnStatus = 2;
                    }
                    if ((item.IsGrnRaised == 1) && item.IsGrnApproved == 0)
                    {
                        GrnStatus = 0;
                    }
                    if ((item.IsGrnRaised == 1) && item.IsGrnApproved == 1)
                    {
                        GrnStatus = 1;
                    }
                    grnToApproveList.Add(new GrnToApproveList(item.GrnId, item.PoId, item.POCode, item.PrCode, item.SupplierName, item.GoodReceivedDate, item.IsGrnRaised, item.IsGrnApproved, GrnStatus, item.subdepartment, item.Description));
                }

                gvGRN.DataSource = grnToApproveList.Where(v => v.GrnStatusCount == 0).OrderBy(t => t.GrnId).Take(5).ToList();
                gvGRN.DataBind();

                
                */
            }



        }

        protected void GetData(int yearsearch)
        {
            List<int> bidCount = biddingController.GetBidCountForDashboard(int.Parse(Session["CompanyId"].ToString()), yearsearch,1);

            totalBids.InnerText = bidCount[0].ToString();
            pendingBidCreation.InnerText = bidCount[1].ToString();
            pendingApprovalBids.InnerText = bidCount[2].ToString();
            inprogressBids.InnerText = bidCount[3].ToString();
            closedBids.InnerText = bidCount[4].ToString();

            List<int> bidCountI =  biddingController.GetBidCountForDashboard(int.Parse(Session["CompanyId"].ToString()), yearsearch, 2);

            totalBidsI.InnerText = bidCountI[0].ToString();
            pendingBidCreationI.InnerText = bidCountI[1].ToString();
            pendingApprovalBidsI.InnerText = bidCountI[2].ToString();
            inprogressBidsI.InnerText = bidCountI[3].ToString();
            closedBidsI.InnerText = bidCountI[4].ToString();

            List<int> poCount = ControllerFactory.CreatePOMasterController().GetPoCountForDashboard(int.Parse(Session["CompanyId"].ToString()), yearsearch, 1);

            totalPO.InnerText = poCount[0].ToString();
            pendingPO.InnerText = poCount[1].ToString();
            approvedPo.InnerText = poCount[2].ToString();
            rejectedPo.InnerText = poCount[3].ToString();


            List<int> poCountI = ControllerFactory.CreatePOMasterController().GetPoCountForDashboard(int.Parse(Session["CompanyId"].ToString()), yearsearch, 2);

            totalPOI.InnerText = poCountI[0].ToString();
            pendingPOI.InnerText = poCountI[1].ToString();
            approvedPoI.InnerText = poCountI[2].ToString();
            rejectedPoI.InnerText = poCountI[3].ToString();

            List<int> pRCount = ControllerFactory.CreatePR_MasterController().GetPRCountForDashboard(int.Parse(Session["CompanyId"].ToString()), yearsearch, 1);
            totalPr.InnerText = pRCount[0].ToString();
            pendingPR.InnerText = pRCount[1].ToString();
            ApprovePr.InnerText = pRCount[2].ToString();
            RejectePr.InnerText = pRCount[3].ToString();

            List<int> pRCountI = ControllerFactory.CreatePR_MasterController().GetPRCountForDashboard(int.Parse(Session["CompanyId"].ToString()), yearsearch, 2);
            totalPrI.InnerText = pRCountI[0].ToString();
            pendingPRI.InnerText = pRCountI[1].ToString();
            ApprovePrI.InnerText = pRCountI[2].ToString();
            RejectePrI.InnerText = pRCountI[3].ToString();

            CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

            CompanyLogin loggedUser = companyLoginController.GetUserbyuserId(Convert.ToInt32(Session["UserId"].ToString()));
            List<int> Count = ControllerFactory.CreateDashboardController().GetCountForDashboard(int.Parse(Session["CompanyId"].ToString()), yearsearch, loggedUser.DesignationId);
            noOfCompanyUsers.InnerText = Count[0].ToString();
            noOfSupplier.InnerText = Count[1].ToString();
            //noOfPendingQuotationApproval.InnerText = Count[2].ToString();
            //noOfPendingPOApproval.InnerText = Count[3].ToString();
            successTransaction.InnerText = Count[4].ToString();


          
        }
        
        protected void btnExpierBid_Click(object sender, EventArgs e)
        {
            int update = biddingController.UpdateExpierBids(int.Parse(Session["CompanyId"].ToString()));

            if (update > 0)
            {
                lblBiddingExpireMsg.Text = "Expired successfully";
            }
            else
            {
                lblBiddingExpireMsg.Text = "Expired unsuccessful";
            }
        }

        //protected void PRChartLoad()
        //{

        //    PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        //    ChartArea chartArea1 = new ChartArea();
        //    chartArea1.AxisX.Title = "Months";
        //    chartArea1.AxisY.Title = "PR Count";
        //    chrtPR.ChartAreas.Add(chartArea1);

        //    Legend legend1 = new Legend();
        //    chrtPR.Legends.Add(legend1);

        //    Series series1 = new Series();
        //    series1.ChartType = SeriesChartType.Line;
        //    series1.Name = "This_Year";
        //    series1.Color = Color.Blue;
        //    series1.BorderWidth = 4;
        //    chrtPR.Series.Add(series1);

        //    var valueList = pR_MasterController.countTotalPrtochart(int.Parse(ComapnyId), LocalTime.Today.Year).OrderBy(x => DateTime.ParseExact(x.monthName, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
        //    List<int> prcount = new List<int>();
        //    List<string> monthName = new List<string>();
        //    if (valueList.Count() != 0)
        //    {
        //        foreach (var item in valueList)
        //        {
        //            prcount.Add(int.Parse(item.prCount));
        //            monthName.Add(item.monthName);
        //        }
        //        chrtPR.Series["This_Year"].Points.DataBindXY(monthName, prcount);
        //    }
        //    else
        //    {
        //        chrtPR.Series["This_Year"].Points.AddXY(LocalTime.Today.ToString("MMMM"), 0);
        //    }

        //    var valueList2 = pR_MasterController.countTotalPrtochart(int.Parse(ComapnyId), LocalTime.Today.Year - 1).OrderBy(x => DateTime.ParseExact(x.monthName, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
        //    Series series2 = new Series();
        //    series2.ChartType = SeriesChartType.Line;
        //    series2.Name = "Last_Year";
        //    series2.Color = Color.Green;
        //    series2.BorderWidth = 4;
        //    chrtPR.Series.Add(series2);
        //    prcount.Clear();
        //    monthName.Clear();
        //    if (valueList2.Count() != 0)
        //    {
        //        foreach (var item in valueList2)
        //        {
        //            prcount.Add(int.Parse(item.prCount));
        //            monthName.Add(item.monthName);
        //        }
        //        chrtPR.Series["Last_Year"].Points.DataBindXY(monthName, prcount);

        //    }
        //    else
        //    {
        //        chrtPR.Series["Last_Year"].Points.AddXY(LocalTime.Today.ToString("MMMM"), 0);
        //    }
        //}
        protected void LoadYear()
        {
            int from = 2018;
            int to = LocalTime.Now.Year;

            List<int> years = Enumerable.Range(from, to - from + 1).ToList();
            ddlYear.DataSource = years;
            ddlYear.DataBind();

            ddlYear.Items.FindByText(to.ToString()).Selected = true;


        }

        //protected void BIDChartLoad()
        //{

        //    PR_MasterController pR_MasterController = ControllerFactory.CreatePR_MasterController();
        //    ChartArea chartArea1 = new ChartArea();
        //    chartArea1.AxisX.Title = "Months";
        //    chartArea1.AxisY.Title = "BID Count";
        //    chrtBid.ChartAreas.Add(chartArea1);

        //    Legend legend1 = new Legend();
        //    chrtBid.Legends.Add(legend1);

        //    Series series1 = new Series();
        //    series1.ChartType = SeriesChartType.Line;
        //    series1.Name = "This_Year";
        //    series1.Color = Color.Blue;
        //    series1.BorderWidth = 4;
        //    chrtBid.Series.Add(series1);

        //    List<int> prcount = new List<int>();
        //    List<string> monthName = new List<string>();
        //    var valueList = pR_MasterController.FetchTotalBidforChart(int.Parse(ComapnyId), LocalTime.Today.Year).OrderBy(x => DateTime.ParseExact(x.monthName, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
        //    if (valueList.Count() != 0)
        //    {
        //        foreach (var item in valueList)
        //        {
        //            prcount.Add(int.Parse(item.prCount));
        //            monthName.Add(item.monthName);
        //        }
        //        chrtBid.Series["This_Year"].Points.DataBindXY(monthName, prcount);
        //    }

        //    else
        //    {

        //        chrtBid.Series["This_Year"].Points.AddXY(LocalTime.Today.ToString("MMMM"), 0);
        //    }



        //    Series series2 = new Series();
        //    series2.ChartType = SeriesChartType.Line;
        //    series2.Name = "Last_Year";
        //    series2.Color = Color.Green;
        //    series2.BorderWidth = 4;
        //    chrtBid.Series.Add(series2);
        //    prcount.Clear();
        //    monthName.Clear();

        //    var valueList2 = pR_MasterController.FetchTotalBidforChart(int.Parse(ComapnyId), LocalTime.Today.Year - 1).OrderBy(x => DateTime.ParseExact(x.monthName, "MMMM", CultureInfo.CurrentCulture).Month).ToList();
        //    if (valueList2.Count() != 0)
        //    {

        //        foreach (var item in valueList2)
        //        {
        //            prcount.Add(int.Parse(item.prCount));
        //            monthName.Add(item.monthName);
        //        }
        //        chrtBid.Series["Last_Year"].Points.DataBindXY(monthName, prcount);
        //    }
        //    else
        //    {

        //        chrtBid.Series["Last_Year"].Points.AddXY(LocalTime.Today.ToString("MMMM"), 0);
        //    }
        //}
        //protected void btnMakeId_Click(object sender, EventArgs e)
        //{
        //    foreach (var item in _additems)
        //    {
        //        itemImageUploadController.SaveItems(item.ItemId, item.ImagePath);
        //    }
        //}

        [WebMethod]
        public static object GetMainSummary()
        {
         
            var Data = (object)null;
            List<string> MainDetails = new List<string>();
            SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
            CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

            ItemCategoryApprovalController itemCategoryApprovalController = ControllerFactory.CreateItemCategoryApprovalController();
            CompanyLogin loggedUser = companyLoginController.GetUserbyuserId(Convert.ToInt32(HttpContext.Current.Session["UserId"].ToString()));
            List<ItemCategoryBidApproval> listItemCategoryBidApproval = itemCategoryApprovalController.GetItemCategoryBidApprovalByDesignationId(loggedUser.DesignationId);
            List<int> PrId = listItemCategoryBidApproval.Select(t => t.PRId).ToList();
            int PoIdCount = itemCategoryApprovalController.GetItemCategoryPOApprovalCountByDesignationId(loggedUser.DesignationId);

            string noOfCompanyUsers = companyLoginController.GetUserListByDepartmentid(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Count().ToString("00");
            string noOfSuppliers = supplierAssigneToCompanyController.GetSupplierRequestsByCompanyId(int.Parse(HttpContext.Current.Session["CompanyId"].ToString())).Count().ToString("00");

            Data = new { NoOfCompanyUsers = noOfCompanyUsers, NoOfSuppliers = noOfSuppliers, NoOfPrId = PrId.Count , UserId = loggedUser.UserId , NoOfPoId = PoIdCount };
            return Data;
        }

        protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            string year=ddlYear.SelectedValue;
            ViewState["Year"] = year;
            GetData(int.Parse(ViewState["Year"].ToString()));
        }

        [WebMethod]
        public static List<object> GetMRNandPRcountList()
        {
            MRNmasterController mrnmasterController = ControllerFactory.CreateMRNmasterController();
            PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();

            DataTable dtMrn = mrnmasterController.GetMRNCountForDashBoard();
            DataTable dtPr = pr_MasterController.GetPRCountForDashBoard();

            List<object> iData = new List<object>();
            List<string> labels = new List<string>();
            List<string> y1 = new List<string>();
            List<string> y2 = new List<string>();


            labels = dtMrn.AsEnumerable()
                                       .Select(r => r.Field<string>("Range"))
                                       .ToList();

            List<int> l1 = dtMrn.AsEnumerable()
                                       .Select(r => r.Field<int>("Count"))
                                       .ToList();
            y1 = l1.ConvertAll<string>(delegate (int i) { return i.ToString(); });


            List<int> l2 = dtPr.AsEnumerable()
                                       .Select(r => r.Field<int>("Count"))
                                       .ToList();
            y2 = l2.ConvertAll<string>(delegate (int i) { return i.ToString(); });

            iData.Add(labels);//label
            iData.Add(y1);//mrn
            iData.Add(y2);//pr

            return iData;
        }

        
        protected void lbtnViewPRPending_Click(object sender, EventArgs e)
        {
            
            try
            {
                int prid = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                prid = int.Parse(gvPurchaseRequest.Rows[x].Cells[0].Text);
                Response.Redirect("CustomerViewPurchaseRequisition.aspx?PrId=" + prid);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        protected void lbtnViewPoapproval_Click(object sender, EventArgs e)
        {
        
            try
            {
                int PoId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                PoId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Session["PoId"] = PoId;
                Response.Redirect("CompanyViewAndApprovePO.aspx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        protected void lbtnViewgrn_Click(object sender, EventArgs e)
        {
        
            int GrnId = 0;
            int PoID = 0;
            int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            GrnId = int.Parse(gvGRN.Rows[x].Cells[0].Text);
            PoID = int.Parse(gvGRN.Rows[x].Cells[1].Text);
            Session["GrnID"] = GrnId;
            Session["PoID"] = PoID;
            Response.Redirect("CustomerGRNApproval.aspx");
            
        }

        protected void lbtnViewssumitbids_Click(object sender, EventArgs e)
        {
    
            int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            var prid = int.Parse(gvsubmitforbid.Rows[x].Cells[0].Text);
            Response.Redirect("SubmitPRForBidListing.aspx?PrId=" + prid); 
        }

        protected void lbtnViewSumittedbids_Click(object sender, EventArgs e)
        {
        
           int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
          var prid = int.Parse(gvacceptbids.Rows[x].Cells[0].Text);
            Response.Redirect("ViewSubmittedForBidListing.aspx?PrId=" + prid);
            
        }
        
    }
}