using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem
{
    public partial class comparisionToLastYear : System.Web.UI.Page
    {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefSupplier";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabSupplier";
                    ((BiddingAdmin)Page.Master).subTabValue = "CompanyUpdatingAndRatingSupplier.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "editSupplierLink";

                    ViewState["CompanyId"] = int.Parse(Session["CompanyId"].ToString());
                    ViewState["userId"] = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));
                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(ViewState["userId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 3, 3) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
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

                    BindDataTable();
                    BindDataToDropDown();

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void BindDataToDropDown()
        {
            ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
            ddlCategory.DataSource = itemCategoryController.FetchItemCategoryList(6);
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", ""));


            PrTypeController prTypeController = ControllerFactory.CreatePrTypeController();
            ddlPRType.DataSource = prTypeController.FetchAllPRTypes();
        }

        private void BindDataTable()
        {
            List<ComparisionToLastYearPOReport> comparisionToLastYearPOReport = new List<ComparisionToLastYearPOReport>();
            ComparisionToLastYearPOReportController comparisionToLastYearPOReportController = ControllerFactory.CreateComparisionToLastYearPOReportController();
            comparisionToLastYearPOReport = comparisionToLastYearPOReportController.GetComparisionToLastYearPOReports();
            var ListPOCode = comparisionToLastYearPOReport.Select(x => x.POCode).Distinct();
            var ListYear = comparisionToLastYearPOReport.Select(x => x.ApprovedDate.Year).Distinct();

            //Header Names
            List<string> headers = new List<string>() { "Quantity", "VAT_AMOUNT", "NBT_AMOUNT", "TOTAL_AMOUNT" };



            TableHeaderRow thr1 = new TableHeaderRow();
            TableHeaderCell thc1 = new TableHeaderCell();

            TableHeaderRow thr2 = new TableHeaderRow();
            TableHeaderCell thc2 = new TableHeaderCell();

            thc1.Text = "";
            thr1.Cells.Add(thc1);

            thc2.Text = "PO_CODE";
            thr2.Cells.Add(thc2);
            thr2.Font.Size = 12;
            thr2.Font.Bold = true;

            foreach (int itemLocation in comparisionToLastYearPOReport.Select(x => x.ApprovedDate.Year).Distinct())
            {
                TableHeaderCell thc1i = new TableHeaderCell();
                thc1i.Text = itemLocation.ToString();
                thr1.Cells.Add(thc1i);

                int count = 0;

                foreach (var headerName in headers)
                {
                    count++;
                    TableHeaderCell thc2i = new TableHeaderCell();
                    thc2i.Text = headerName;
                    thr2.Cells.Add(thc2i);
                }

                thr1.HorizontalAlign = HorizontalAlign.Center;
                thr1.Font.Size = 12;
                thr1.Font.Bold = true;
                thc1i.ColumnSpan = count;
            }


            tblTaSummary.Rows.Add(thr1);
            tblTaSummary.Rows.Add(thr2);


            int flag2 = 0;
            //? Wise the "POCODE" wise
            foreach (var itemListPOCode in ListPOCode)
            {
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = itemListPOCode;
                tr.Cells.Add(tc1);

                foreach (var ItemListYear in ListYear)
                {
                    TableCell tc21 = new TableCell();
                    TableCell tc22 = new TableCell();
                    TableCell tc23 = new TableCell();
                    //TableCell tc24 = new TableCell();
                    TableCell tc25 = new TableCell();
                    flag2 = 0;

                    foreach (var item in comparisionToLastYearPOReport.Where(x => x.POCode == itemListPOCode))
                    {
                        if (item.ApprovedDate.Year == ItemListYear)
                        {
                            // data set 
                            flag2 = 1;
                            tc21.Text = item.Qunatity.ToString();
                            tr.Cells.Add(tc21);
                            tc22.Text = item.VATAmount.ToString();
                            tr.Cells.Add(tc22);
                            tc23.Text = item.NBTAmount.ToString();
                            tr.Cells.Add(tc23);
                            //tc24.Text = (item.TotalAmount + item.PhysicalCount).ToString();
                            //tr.Cells.Add(tc24);
                            tc25.Text = item.TotalAmount.ToString();
                            tr.Cells.Add(tc25);
                        }
                    }
                    if (flag2 == 0)
                    {
                        tc21.Text = "N/A";
                        tr.Cells.Add(tc21);
                        tc22.Text = "N/A";
                        tr.Cells.Add(tc22);
                        tc23.Text = "N/A";
                        tr.Cells.Add(tc23);
                        //tc24.Text = "0";
                        //tr.Cells.Add(tc24);
                        tc25.Text = "N/A";
                        tr.Cells.Add(tc25);
                    }


                }
                tblTaSummary.Rows.Add(tr);
            }
        }
    }
}
