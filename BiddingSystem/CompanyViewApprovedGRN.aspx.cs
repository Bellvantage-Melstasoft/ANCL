using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class CompanyViewApprovedGRN : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        int CompanyId = 0;
        GrnController grnController = ControllerFactory.CreateGrnController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyViewApprovedGRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "CompanyViewApprovedGRNLink";

                CompanyId = int.Parse(Session["CompanyId"].ToString());
                UserId = Session["UserId"].ToString();

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 11) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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
                if (int.Parse(UserId) != 0)
                {
                    try
                    {

                        txtFDt.Text = LocalTime.Now.ToString("MMMM yyyy");
                        List<GrnMaster> viewApprovedgrn = new List<GrnMaster>();
                        viewApprovedgrn = grnController.GetGrnsByCompanyId(CompanyId, LocalTime.Now);

                        if (Session["UserWarehouses"] != null)
                        {
                            if ((Session["UserWarehouses"] as List<UserWarehouse>).Count > 0)
                            {
                                viewApprovedgrn = viewApprovedgrn.Where(grn => (Session["UserWarehouses"] as List<UserWarehouse>).Any(w => w.WrehouseId == grn.WarehouseId)).ToList();
                            }
                        }




                        gvPurchaseOrder.DataSource = viewApprovedgrn.OrderBy(x => x.CreatedDate);
                        gvPurchaseOrder.DataBind();



                        //int GrnStatus = 0;
                        //List<GrnMaster> GrnMasterListByCompanyId = new List<GrnMaster>();
                        //List<GrnMaster> GrnMasterListByCompanyIdDup = new List<GrnMaster>();
                        //GrnMasterListByCompanyId = grnController.GetAllDetailsGrnIsApproved(CompanyId);

                        //List<GrnToApproveListClz> grnToApproveList = new List<GrnToApproveListClz>();

                        ////-----------Check duplicate values in the list
                        ////var query = GrnMasterListByCompanyId.GroupBy(x => x.GrnId)
                        ////           .Where(g => g.Count() > 1)
                        ////           .Select(y => y.Key)
                        ////           .ToList();
                        //if (GrnMasterListByCompanyId.Count > 0)
                        //{
                        //    //var grpDupes = from f in GrnMasterListByCompanyId
                        //    //               group f by f.GrnId into grps
                        //    //               where grps.Count() > 1
                        //    //               select grps;

                        //    //foreach (var item in grpDupes)
                        //    //{
                        //    //    foreach (var itemsList in item)
                        //    //    {
                        //    //        GrnMasterListByCompanyIdDup.Add(itemsList);
                        //    //    }
                        //    //}


                        //    //GrnMasterListByCompanyId = GrnMasterListByCompanyId.Where(p => !GrnMasterListByCompanyIdDup.Any(x => x.PoId == p.PoId)).ToList();
                        //    //GrnMasterListByCompanyIdDup.RemoveAll(r => r.IsGrnApproved == 1);
                        //    //GrnMasterListByCompanyIdDup.RemoveAll(r => r.IsGrnRaised != 1);

                        //    //var grpDupes1 = from f in GrnMasterListByCompanyIdDup
                        //    //                group f by f.GrnId into grps
                        //    //                where grps.Count() > 1
                        //    //                select grps;


                        //    //var objs = (from c in GrnMasterListByCompanyIdDup
                        //    //            orderby c.GrnId
                        //    //            select c).GroupBy(g => g.GrnId).Select(x => x.FirstOrDefault());

                        //    foreach (var item in GrnMasterListByCompanyId)
                        //    {
                        //        if ((item.IsGrnRaised == 2 || item.IsGrnRaised == 0) && item.IsApproved == 0)
                        //        {
                        //            GrnStatus = 2;
                        //        }
                        //        if ((item.IsGrnRaised == 1) && item.IsApproved == 0)
                        //        {
                        //            GrnStatus = 0;
                        //        }
                        //        if ((item.IsGrnRaised == 1) && item.IsApproved == 1)
                        //        {
                        //            GrnStatus = 1;
                        //        }
                        //        grnToApproveList.Add(new GrnToApproveListClz(item.GrnId, item.PoId, item.POCode, item.PrCode, item.SupplierName, item.GoodReceivedDate, item.IsGrnRaised, item.IsGrnApproved, GrnStatus, item.GrnCode));
                        //    }




                        //    gvPurchaseOrder.DataSource = grnToApproveList.Where(v => v.GrnStatusCount == 1).OrderBy(t => t.GrnId);
                        //    gvPurchaseOrder.DataBind();
                        //}
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }
        }

        protected void btnView_Click1(object sender, EventArgs e)
        {
            try
            {
                int GrnId = 0;
                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                GrnId = int.Parse(gvPurchaseOrder.Rows[x].Cells[0].Text);
                Response.Redirect("CompanyGrnReportView.aspx?PoID=0&GrnId=" + GrnId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            DateTime date = DateTime.ParseExact(txtFDt.Text.ProcessString(), "MMMM yyyy", CultureInfo.InvariantCulture);

            gvPurchaseOrder.DataSource = grnController.GetGrnsByCompanyId(CompanyId, date);
            gvPurchaseOrder.DataBind();

        }

    }
}

//    public class GrnToApproveListClz
//    {
//        public GrnToApproveListClz(int GrnId, int PoID, string POCode, string PrCode, string SupplierName, DateTime GoodReceivedDate, int IsGrnRaised, int IsGrnApproved, int GrnStatusCount, string GrnCode)
//        {
//            grnId = GrnId;
//            poID = PoID;
//            pOCode = POCode;
//            prCode = PrCode;
//            supplierName = SupplierName;
//            goodReceivedDate = GoodReceivedDate;
//            isGrnRaised = IsGrnRaised;
//            isGrnApproved = IsGrnApproved;
//            grnStatusCount = GrnStatusCount;
//            grnCode = GrnCode;
//        }

//        private int grnId;
//        private int poID;
//        private string pOCode;
//        private string prCode;
//        private string supplierName;
//        private DateTime goodReceivedDate;
//        private int isGrnRaised;
//        private int isGrnApproved;
//        private int grnStatusCount;
//        private string grnCode;

//        public int GrnId
//        {
//            get { return grnId; }
//            set { grnId = value; }
//        }

//        public int PoID
//        {
//            get { return poID; }
//            set { poID = value; }
//        }

//        public string POCode
//        {
//            get { return pOCode; }
//            set { pOCode = value; }
//        }

//        public string PrCode
//        {
//            get { return prCode; }
//            set { prCode = value; }
//        }

//        public string SupplierName
//        {
//            get { return supplierName; }
//            set { supplierName = value; }
//        }

//        public DateTime GoodReceivedDate
//        {
//            get { return goodReceivedDate; }
//            set { goodReceivedDate = value; }
//        }

//        public int IsGrnRaised
//        {
//            get { return isGrnRaised; }
//            set { isGrnRaised = value; }
//        }

//        public int IsGrnApproved
//        {
//            get { return isGrnApproved; }
//            set { isGrnApproved = value; }
//        }

//        public int GrnStatusCount
//        {
//            get { return grnStatusCount; }
//            set { grnStatusCount = value; }
//        }

//        public string GrnCode
//        {
//            get { return grnCode; }
//            set { grnCode = value; }
//        }
//    }
//}
//}

