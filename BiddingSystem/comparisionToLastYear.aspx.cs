using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
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
        ComparisionToLastYearPOReportController comparisionToLastYearPOReportController = ControllerFactory.CreateComparisionToLastYearPOReportController();

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
                    DataTable dtSupplierReport = comparisionToLastYearPOReportController.GetComparisionToLastYearSupplierReports();
                    DataTable dtItemReport = comparisionToLastYearPOReportController.GetComparisionToLastYearItemReports();
                    // BindDataPOTable(List<compar>);
                    BindDataTable1(dtSupplierReport);
                    BindDataToDropDownToPOTable();
                    BindDatatoItemReport(dtItemReport);
                    BindDataToDropDownToSupplierTable();
                    BindDataToDropDownToItemTable();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void BindDataToDropDownToPOTable()
        {
            ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();
            ddlCategory.DataSource = itemCategoryController.FetchItemCategoryList(6);
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryId";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", ""));

            //CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
            //List<CompanyDepartment> companyDepartmentslist = companyDepartmentController.GetDepartmentList();
            //ddlDepartment.DataSource = companyDepartmentslist;
            //ddlDepartment.DataTextField = "departmentName";
            //ddlDepartment.DataValueField = "departmentID";
            //ddlDepartment.DataBind();
            //ddlCategory.Items.Insert(0, new ListItem("-Select-", ""));


            SubDepartmentControllerInterface subDepartmentController = ControllerFactory.CreateSubDepartmentController();
            List<SubDepartment> subDepartmentsList = subDepartmentController.getDepartmentList(6);
            ddlDepartment.DataSource = subDepartmentsList;
            ddlDepartment.DataTextField = "SubDepartmentName";
            ddlDepartment.DataValueField = "SubDepartmentID";
            ddlDepartment.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-Select-", ""));



        }

        public void BindDataToDropDownToSupplierTable()
        {
            CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
            List<CompanyDepartment> companyDepartmentslist = companyDepartmentController.GetDepartmentList();
            ddlDepartmentType.DataSource = companyDepartmentslist;
            ddlDepartmentType.DataTextField = "departmentName";
            ddlDepartmentType.DataValueField = "departmentID";
            ddlDepartmentType.DataBind();
            ddlDepartmentType.Items.Insert(0, new ListItem("-Select-", ""));

            SupplierController supplierController = ControllerFactory.CreateSupplierController();
            List<Supplier> suppliersList = supplierController.GetSupplierList();

            ddlSupplier.DataSource = suppliersList;
            ddlSupplier.DataTextField = "SupplierName";
            ddlSupplier.DataValueField = "SupplierId";
            ddlSupplier.DataBind();
            ddlSupplier.Items.Insert(0, new ListItem("-Select-", ""));

        }

        public void BindDataToDropDownToItemTable()
        {
            SupplierController supplierController = ControllerFactory.CreateSupplierController();
            List<Supplier> suppliersList = supplierController.GetSupplierList();

            ddlSupplier2.DataSource = suppliersList;
            ddlSupplier2.DataTextField = "SupplierName";
            ddlSupplier2.DataValueField = "SupplierId";
            ddlSupplier2.DataBind();
            ddlSupplier2.Items.Insert(0, new ListItem("-Select-", ""));
        }

        private void BindDataPOTable(List<ComparisionToLastYearPOReport> comparisionToLastYearPOReport)
        {
            if (comparisionToLastYearPOReport.Count == 0)
            {
                comparisionToLastYearPOReport = comparisionToLastYearPOReportController.GetComparisionToLastYearPOReports();
            }

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


            tblPOReport.Rows.Add(thr1);
            tblPOReport.Rows.Add(thr2);



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
                tblPOReport.Rows.Add(tr);
            }
        }

        public void BindDataTable1(DataTable dtSupplierReport)
        {
            //DataTable dtSupplierReport = comparisionToLastYearPOReportController.GetComparisionToLastYearSupplierReports();
            var Years = dtSupplierReport.AsEnumerable().Select(row => row["PURCHASED_YEAR"]).Distinct().ToList();
            var Suppliers = dtSupplierReport.AsEnumerable().Select(row => row["SUPPLIER_NAME"]).Distinct().ToList();

            List<string> headers = new List<string>() { "Department_ID", "PR_TYPE", "Expense_Type", "Purchase_Type", "Quantity", "Amount" };

            TableHeaderRow thr1 = new TableHeaderRow();
            TableHeaderCell thc1 = new TableHeaderCell();

            TableHeaderRow thr2 = new TableHeaderRow();
            TableHeaderCell thc2 = new TableHeaderCell();

            thc1.Text = "";
            thr1.Cells.Add(thc1);

            thc2.Text = "Supplier Name";
            thr2.Cells.Add(thc2);
            thr2.Font.Size = 12;
            thr2.Font.Bold = true;

            foreach (var item in Years)
            {
                TableHeaderCell thc1i = new TableHeaderCell();
                thc1i.Text = item.ToString();
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


            tblSupplierReport.Rows.Add(thr1);
            tblSupplierReport.Rows.Add(thr2);

            int flag2 = 0;
            //? Wise the "POCODE" wise
            foreach (var item in Suppliers)
            {
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = item.ToString();
                tr.Cells.Add(tc1);

                foreach (var item1 in Years)
                {
                    TableCell tc21 = new TableCell();
                    TableCell tc22 = new TableCell();
                    TableCell tc23 = new TableCell();
                    TableCell tc24 = new TableCell();
                    TableCell tc25 = new TableCell();
                    TableCell tc26 = new TableCell();
                    flag2 = 0;

                    foreach (DataRow row in dtSupplierReport.Rows)
                    {
                        if (row["PURCHASED_YEAR"].ToString() == item1.ToString() && row["Supplier_Name"].ToString() == item.ToString())
                        {
                            // data set 
                            flag2 = 1;
                            tc21.Text = row["DEPARTMENT_ID"].ToString().ToString();
                            tr.Cells.Add(tc21);
                            tc22.Text = row["PR_TYPE"].ToString().ToString();
                            tr.Cells.Add(tc22);
                            tc23.Text = row["EXPENSE_TYPE"].ToString().ToString();
                            tr.Cells.Add(tc23);
                            tc24.Text = row["PO_PURCHASE_TYPE"].ToString().ToString();
                            tr.Cells.Add(tc24);
                            tc25.Text = row["QUANTITY"].ToString().ToString();
                            tr.Cells.Add(tc25);
                            tc26.Text = row["AMOUNT"].ToString().ToString();
                            tr.Cells.Add(tc26);
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
                        tc24.Text = "N/A";
                        tr.Cells.Add(tc24);
                        tc25.Text = "N/A";
                        tr.Cells.Add(tc25);
                        tc26.Text = "N/A";
                        tr.Cells.Add(tc26);
                    }


                }
                tblSupplierReport.Rows.Add(tr);
            }
        }

        public void BindDatatoItemReport(DataTable dtItemReport)
        {
            //DataTable dtItemReport = comparisionToLastYearPOReportController.GetComparisionToLastYearItemReports();
            var Years = dtItemReport.AsEnumerable().Select(row => row["PURCHASED_YEAR"]).Distinct().ToList();
            var Items = dtItemReport.AsEnumerable().Select(row => row["ITEM_ID"]).Distinct().ToList();

            List<string> headers = new List<string>() { "SUPPLIER_NAME", "Expense_Type", "Purchase_Type", "Quantity", "Amount" };

            TableHeaderRow thr1 = new TableHeaderRow();
            TableHeaderCell thc1 = new TableHeaderCell();

            TableHeaderRow thr2 = new TableHeaderRow();
            TableHeaderCell thc2 = new TableHeaderCell();

            thc1.Text = "";
            thr1.Cells.Add(thc1);

            thc2.Text = "ITEM_ID";
            thr2.Cells.Add(thc2);
            thr2.Font.Size = 12;
            thr2.Font.Bold = true;

            foreach (var item in Years)
            {
                TableHeaderCell thc1i = new TableHeaderCell();
                thc1i.Text = item.ToString();
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


            tblItemReport.Rows.Add(thr1);
            tblItemReport.Rows.Add(thr2);

            int flag2 = 0;
            //? Wise the "POCODE" wise
            foreach (var item in Items)
            {
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell();
                tc1.Text = item.ToString();
                tr.Cells.Add(tc1);

                foreach (var item1 in Years)
                {
                    TableCell tc21 = new TableCell();
                    TableCell tc22 = new TableCell();
                    TableCell tc23 = new TableCell();
                    TableCell tc24 = new TableCell();
                    TableCell tc25 = new TableCell();
                    //TableCell tc26 = new TableCell();
                    flag2 = 0;

                    foreach (DataRow row in dtItemReport.Rows)
                    {
                        if (row["PURCHASED_YEAR"].ToString() == item1.ToString() && row["ITEM_ID"].ToString() == item.ToString())
                        {
                            // data set 
                            flag2 = 1;
                            tc21.Text = row["SUPPLIER_NAME"].ToString().ToString();
                            tr.Cells.Add(tc21);
                            tc22.Text = row["EXPENSE_TYPE"].ToString().ToString();
                            tr.Cells.Add(tc22);
                            tc23.Text = row["PO_PURCHASE_TYPE"].ToString().ToString();
                            tr.Cells.Add(tc23);
                            tc24.Text = row["QUANTITY"].ToString().ToString();
                            tr.Cells.Add(tc24);
                            tc25.Text = row["AMOUNT"].ToString().ToString();
                            tr.Cells.Add(tc25);
                            //tc26.Text = row["AMOUNT"].ToString().ToString();
                            //tr.Cells.Add(tc26);
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
                        tc24.Text = "N/A";
                        tr.Cells.Add(tc24);
                        tc25.Text = "N/A";
                        tr.Cells.Add(tc25);
                        //tc26.Text = "N/A";
                        //tr.Cells.Add(tc26);
                    }


                }
                tblItemReport.Rows.Add(tr);
            }
        }

        protected void btnSearchPoTable_Click(object sender, EventArgs e)
        {
            List<ComparisionToLastYearPOReport> comparisionToLastYearPOReport = new List<ComparisionToLastYearPOReport>();
            comparisionToLastYearPOReport = comparisionToLastYearPOReportController.GetComparisionToLastYearPOReports();

            if (ddlPRType.SelectedValue != "")
            {
                comparisionToLastYearPOReport = comparisionToLastYearPOReport.Where(x => x.PRType == Convert.ToInt32(ddlPRType.SelectedValue)).ToList();
            }

            if (ddlPurchasingType.SelectedValue != "")
            {
                comparisionToLastYearPOReport = comparisionToLastYearPOReport.Where(x => x.PurchaseType == Convert.ToInt32(ddlPurchasingType.SelectedValue)).ToList();

            }
            if (ddlCategory.SelectedValue != "")
            {
                comparisionToLastYearPOReport = comparisionToLastYearPOReport.Where(x => x.PRCategoryId == Convert.ToInt32(ddlCategory.SelectedValue)).ToList();

            }
            if (ddlDepartment.SelectedValue != "")
            {
                comparisionToLastYearPOReport = comparisionToLastYearPOReport.Where(x => x.SubDepartmentId == Convert.ToInt32(ddlDepartment.SelectedValue)).ToList();

            }


            BindDataPOTable(comparisionToLastYearPOReport);
        }

        protected void btnSupplierSearch_Click(object sender, EventArgs e)
        {
            DataTable dtSupplierReport = comparisionToLastYearPOReportController.GetComparisionToLastYearSupplierReports();
            IEnumerable<DataRow> filteredRows = dtSupplierReport.AsEnumerable();

            if (ddlPR.SelectedValue != "")
            {
                filteredRows = filteredRows.Where(row => row.Field<int>("PR_TYPE") == Convert.ToInt32(ddlPR.SelectedValue));
            }

            if (ddlpurchase.SelectedValue != "")
            {
                filteredRows = filteredRows.Where(row => row.Field<int>("PO_PURCHASE_TYPE") == Convert.ToInt32(ddlpurchase.SelectedValue));
            }

            if (ddlSupplier.SelectedValue != "")
            {
                filteredRows = filteredRows.Where(row => row.Field<string>("SUPPLIER_NAME") == ddlSupplier.SelectedItem.Text);
            }

            if (ddlDepartmentType.SelectedValue != "")
            {
                filteredRows = filteredRows.Where(row => row.Field<int>("DEPARTMENT_ID") == Convert.ToInt32(ddlDepartmentType.SelectedValue));
            }

            if (filteredRows.Any())
            {
                DataTable filteredDt = filteredRows.CopyToDataTable();
                BindDataTable1(filteredDt);
            }
            else
            {
                BindDataTable1(dtSupplierReport);
            }

        }

        protected void btnSearchItem_Click(object sender, EventArgs e)
        {
            DataTable dtItemReport = comparisionToLastYearPOReportController.GetComparisionToLastYearItemReports();
            IEnumerable<DataRow> filteredRows = dtItemReport.AsEnumerable();

            if (ddlPurchaseType2.SelectedValue != "")
            {
                filteredRows = filteredRows.Where(row => row.Field<int>("PO_PURCHASE_TYPE") == Convert.ToInt32(ddlPurchaseType2.SelectedValue));
            }

            if (ddlSupplier2.SelectedValue != "")
            {
                filteredRows = filteredRows.Where(row => row.Field<string>("SUPPLIER_NAME") == ddlSupplier2.SelectedItem.Text);
            }

            if (filteredRows.Any())
            {
                DataTable filteredDt = filteredRows.CopyToDataTable();
                BindDatatoItemReport(filteredDt);
            }
            else
            {
                BindDatatoItemReport(dtItemReport);
            }
        }
    }
}
