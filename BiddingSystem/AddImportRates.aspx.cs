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
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Data;
using System.IO;
using OfficeOpenXml;
using System.Web.Services.Description;

namespace BiddingSystem
{
    public partial class AddImportRates : System.Web.UI.Page
    {
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        ImportsController importsController = ControllerFactory.createImportsController();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null)
                {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                    ((BiddingAdmin)Page.Master).subTabValue = "AddImportRates.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "addImportRatesLink";

                    //CompanyId = int.Parse(Session["CompanyId"].ToString());
                    var UserId = Session["UserId"].ToString();
                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), int.Parse(Session["CompanyId"].ToString()), 1, 1) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA")
                    {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else
                {
                    Response.Redirect("LoginPage.aspx");
                }

                //msg.Visible = false;
                //if (!IsPostBack)
                //{
                //    bindMeasurements();
                //}

                //msg.Visible = false;
            }
            catch (Exception)
            {
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(fileuploadExcel.HasFile && Path.GetExtension(fileuploadExcel.FileName)== ".xlsx")
            {
                List<Imports> dataList = new List<Imports>();
                

                using (var excel = new ExcelPackage(fileuploadExcel.PostedFile.InputStream))
                {
                    var tbl = new DataTable();
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                    var ws = excel.Workbook.Worksheets.First();
                    var hasHeader = true;

                    // add DataColumns to DataTable
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text
                            : String.Format("Column {0}", firstRowCell.Start.Column));

                    // add DataRows to DataTable
                    int startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        Imports objImport = new Imports();
                        int count = 0;
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.NewRow();
                        foreach (var cell in wsRow)
                        {
                            
                            row[cell.Start.Column - 1] = cell.Text;
                            switch (count)
                            {
                                case 0:
                                    objImport.hsId = cell.Text;
                                    break;
                                case 1:
                                    objImport.hsName = cell.Text;
                                    break;
                                case 2:
                                    objImport.pal = Convert.ToInt32(cell.Text);
                                    break;
                                case 3:
                                    objImport.vat = Convert.ToInt32(cell.Text);
                                    break;
                                case 4:
                                    objImport.cess = Convert.ToInt32(cell.Text);
                                    break;
                                case 5:
                                    objImport.customDuty = Convert.ToInt32(cell.Text);
                                    break;
                                case 6:
                                    objImport.rate = Convert.ToDouble(cell.Text);
                                    break;
                                case 7:
                                    objImport.effectiveDate = cell.Text;
                                    break;
                                default:
                                    break;
                            }

                            count++;

                        }
                        dataList.Add(objImport);                            
                        tbl.Rows.Add(row);
                    }


                    int result = importsController.InsertImportRates(dataList);
                    if(result == 1)
                    {
                        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Rates inserted Successfully ', showConfirmButton: false,timer: 4000}); });   </script>", false);
                        DisplayMessage("Imports Rates Saved",false);
                    }
                    else
                    {
                        // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error occured during saving ', showConfirmButton: false,timer: 4000}); });   </script>", false);
                        DisplayMessage("Error in Saving", true);
                    }
                }

                
            }
            else
            {
                var msg = "You did not specify a file to upload.";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string document = Server.MapPath("~/ImportDocs/ImportRateTemplate.xlsx");
                string filename = "ImportRateTemplate.xlsx";
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.AppendHeader("content-disposition", "attachment; filename=" + filename);
                response.ContentType = "application/octet-stream";
                response.WriteFile(document);
                response.Flush();
                response.End();
            }
            catch (Exception)
            {
                throw;
            }
        }

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