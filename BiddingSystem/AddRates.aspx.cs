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
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Globalization;

namespace BiddingSystem {
    public partial class AddRates : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        DefCurrencyTypeController DefCurrencyTypeController = ControllerFactory.CreateDefCurrencyTypeController();
        CurrencyRateController CurrencyRateController = ControllerFactory.CreateCurrencyRateController();
        DutyRatesController dutyRatesController = ControllerFactory.CreateDutyRatesController();
        DataTable dtexcel = new DataTable();

        protected void Page_Load(object sender, EventArgs e) {
            try {
                if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                    ((BiddingAdmin)Page.Master).mainTabValue = "hrefItemCategory";
                    ((BiddingAdmin)Page.Master).subTabTitle = "subTabItemCategory";
                    ((BiddingAdmin)Page.Master).subTabValue = "AddRates.aspx";
                    ((BiddingAdmin)Page.Master).subTabId = "AddRatesLink";

                    // CompanyId = int.Parse(Session["CompanyId"].ToString());
                    //   UserId = Session["UserId"].ToString();                   

                    CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                    if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 32) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                        Response.Redirect("AdminDashboard.aspx");
                    }
                }
                else {
                    Response.Redirect("LoginPage.aspx");
                }

                if (!IsPostBack) {

                    populateCurrencyList();
                    LoadCurrency();
                    LoadRates();
                    LoadRates();
                    LoadCountry();
                    LoadHsCodes();

                    txtExerciseRate.Text = "0.00";
                    txtDutyRate.Text = "0.00";
                    txtPalRate.Text = "0.00";
                    txtCessRate.Text = "0.00";

                }

            }
            catch (Exception) {

            }
        }

        private void LoadCurrency() {

            gvCurrency.DataSource = CurrencyRateController.fetchCurrencyDetails();
            gvCurrency.DataBind();
        }

        private void LoadCountry() {

            gvCountry.DataSource = ControllerFactory.CreateCurrencyRateController().fetchCountry();
            gvCountry.DataBind();
        }

        private void LoadRates() {

            gvHSRates.DataSource = dutyRatesController.GetRates();
            gvHSRates.DataBind();
        }

        protected void gvHSRates_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvHSRates.PageIndex = e.NewPageIndex;
                gvHSRates.DataSource = dutyRatesController.GetRates();
                gvHSRates.DataBind();
            }
            catch (Exception) {

            }
        }
        protected void gvCurrency_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvCurrency.PageIndex = e.NewPageIndex;
                gvCurrency.DataSource = CurrencyRateController.fetchCurrencyDetails();
                gvCurrency.DataBind();
            }
            catch (Exception) {

            }
        }

        protected void gvCountry_PageIndexChanging(object sender, GridViewPageEventArgs e) {
            try {
                gvCountry.PageIndex = e.NewPageIndex;
                gvCountry.DataSource = ControllerFactory.CreateCurrencyRateController().fetchCountry();
                gvCountry.DataBind();
            }
            catch (Exception) {

            }
        }
        private void populateCurrencyList() {
            List<DefCurrencyType> CurrencyList = DefCurrencyTypeController.FetchDefCurrencyTypeList();
            ddlCurrencyType.DataSource = CurrencyList;
            ddlCurrencyType.DataValueField = "CurrencyTypeId";
            ddlCurrencyType.DataTextField = "CurrencyShortName";
            ddlCurrencyType.DataBind();

        }

        
        private void LoadHsCodes() {
            List<DutyRates> Rates = ControllerFactory.CreateDutyRatesController().GetRates();
            for (int i = 0; i < Rates.Count; i++) {
                Rates[i].HsIdName = Rates[i].HsIdName + " - " + Rates[i].HsId;
            }
            List<DutyRates> DutyRateList = Rates;
            ddlHsCode.DataSource = DutyRateList;
            ddlHsCode.DataValueField = "HsId";
            ddlHsCode.DataTextField = "HsIdName";
            ddlHsCode.DataBind();
            ddlHsCode.Items.Insert(0, new ListItem("Select HS Code", "0"));

        }
        private void ClearFields() {
            txtCurrencyName.Text = "";
            txtCurrencyShortName.Text = "";
            ddlCurrencyType.Items.Clear();
            TxtEffectiveDate.Text = "";
            txtBuyingRate.Text = "";
            txtSellingRate.Text = "";
            txtExerciseRate.Text = "";
            txtDutyRate.Text = "";
            txtPalRate.Text = "";
            txtCessRate.Text = "";
            txtCountry.Text = "";
            txtHsCodeName.Text = "";
            LoadHsCodes();
            txtHsCode.Text = "";
            txtHsCode.Enabled = true;
            txtHsCodeName.Enabled = true;
            btnDuty.Text = "Save";

        }
        protected void btnSaveCurrencyType_Click(object sender, EventArgs e) {
            string CurrencyName = txtCurrencyName.Text;
            string CurrencyShortName = txtCurrencyShortName.Text;

            int Save = DefCurrencyTypeController.SaveCurrencyType(CurrencyName, CurrencyShortName);

            if (Save > 0) {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                ClearFields();
                populateCurrencyList();
                LoadCurrency();
            }

        }

        protected void btnSaveCurrencyRates_Click(object sender, EventArgs e) {
            int CurrencyType = int.Parse(ddlCurrencyType.SelectedValue);
            DateTime Date = DateTime.Parse(TxtEffectiveDate.Text);
            decimal Buyingrate = decimal.Parse(txtBuyingRate.Text);
            decimal Sellingrate = decimal.Parse(txtSellingRate.Text);

            int Save = CurrencyRateController.SaveCurrencyRates(CurrencyType, Date, Buyingrate, Sellingrate);

            if (Save > 0) {
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                ClearFields();
                populateCurrencyList();
                LoadCurrency();
            }

        }

        protected void btnDuty_Click(object sender, EventArgs e) {

            string HSCode = "";
            string HsCodeName = "";
            int Save = 0;


            if (btnDuty.Text == "Update") {
                HSCode = ddlHsCode.SelectedValue;

            }
            else {
                HSCode = txtHsCode.Text;
                HsCodeName = txtHsCodeName.Text;
            }
           
            string HsCode = txtHsCode.Text;
            decimal XID = decimal.Parse(txtExerciseRate.Text);
            decimal CID = decimal.Parse(txtDutyRate.Text);
            decimal PAL = decimal.Parse(txtPalRate.Text);
            decimal EIC = decimal.Parse(txtCessRate.Text);

            if (btnDuty.Text == "Save") {
                int Count = 0;
                if ((HSCode == "" || HsCodeName == "")) {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'HS Code and Code Name cannot be empty', showConfirmButton: false,timer: 1500}); });   </script>", false);
                }
                else {
                    Count = dutyRatesController.HsCOdeAvailability(HSCode);
                    if (Count > 0) {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'HS Code already exists', showConfirmButton: false,timer: 1500}); });   </script>", false);

                    }
                    else {
                        Save = dutyRatesController.SaveCurrencyRates(HSCode, XID, CID, PAL, EIC, HsCodeName);
                    }
                }

            }
            else {
                Save = dutyRatesController.SaveCurrencyRates(HSCode, XID, CID, PAL, EIC, HsCodeName);
            }

            if (Save > 0) {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                ClearFields();
                LoadRates();
                LoadHsCodes();
            }
            txtExerciseRate.Text = "0.00";
            txtDutyRate.Text = "0.00";
            txtPalRate.Text = "0.00";
            txtCessRate.Text = "0.00";
        }

        protected void btnCountry_Click(object sender, EventArgs e) {
            string Country = txtCountry.Text;
            int Save = dutyRatesController.SaveCountry(Country);

            if (Save > 0) {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'SUCCESS',text:'Your work has been saved', showConfirmButton: false,timer: 1500}); });   </script>", false);
                ClearFields();
                LoadCountry();
            }
        }


        protected void btnUpload_Click(object sender, EventArgs e) {
           

            try {
                if (FileUpload1.HasFile == true) {

                    if (Path.GetExtension(FileUpload1.PostedFile.FileName).Equals(".xlsx")) {
                        if (System.IO.File.Exists(Server.MapPath("~/ExcelFile/" + System.IO.Path.GetFileName("Rates.xlsx")))) {

                            string FilePath = Server.MapPath("~/ExcelFile/" + System.IO.Path.GetFileName("Rates.xlsx"));
                            DataTable Stock = new DataTable();
                            bool hasHeaders = true;
                            string HDR = hasHeaders ? "Yes" : "No";


                            // System.IO.File.Delete(FilePath);


                            FileUpload1.PostedFile.SaveAs(FilePath);
                            Stock = Import_To_DataTable(FilePath, HDR);
                            if (dtexcel.Columns.Count == 6) {
                                string consString = System.Configuration.ConfigurationSettings.AppSettings["dbConString"].ToString();

                                using (SqlConnection con = new SqlConnection(consString)) {
                                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con)) {

                                        if (Stock.Rows.Count > 0) {

                                            try {

                                                sqlBulkCopy.DestinationTableName = "dbo.DUTY_RATES";

                                                sqlBulkCopy.ColumnMappings.Add(0, 0);//"HS_ID", "HS_ID");
                                                sqlBulkCopy.ColumnMappings.Add(1, 1);//("DATE", "DATE");
                                                sqlBulkCopy.ColumnMappings.Add(2, 2);//("XID", "XID");
                                                sqlBulkCopy.ColumnMappings.Add(3, 3);//("CID", "CID");
                                                sqlBulkCopy.ColumnMappings.Add(4, 4);//("PAL", "PAL");
                                                sqlBulkCopy.ColumnMappings.Add(5, 5);//("EIC", "EIC");
                                                sqlBulkCopy.ColumnMappings.Add(6, 6);//("HS_ID_NAME", "HS_ID_NAME");

                                                con.Open();
                                                sqlBulkCopy.WriteToServer(Stock);
                                                lblMessage.Visible = true;
                                                lblMessage.ForeColor = System.Drawing.Color.Green;
                                                lblMessage.Text = "Updated Succesfully ";
                                                con.Close();
                                                LoadRates();
                                                LoadHsCodes();

                                            }
                                            catch (Exception ex) {
                                                lblMessage.Visible = true;
                                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                                lblMessage.Text = "Cannot Update.";
                                                //throw ex;
                                            }


                                        }
                                        


                                    }
                                }
                            }
                            else {
                                lblMessage.ForeColor = System.Drawing.Color.Red;
                                lblMessage.Text = "Excel file has invalid columns ";
                            }
                        }
                        else {
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            lblMessage.Text = "Cannot locate the file.";

                        }
                    }


                    else {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Invalid File Format , Upload Files  with extention of .xlsx formate.";
                    }
                }
                else {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "No file found";
                }
                LoadHsCodes();
            }
            catch (Exception ex) {
                throw ex;
            }


        }

        private DataTable Import_To_DataTable(string FilePath, string HDR) {
            OleDbConnection conn = new OleDbConnection();

            DataTable stock = new DataTable();
            dtexcel.CaseSensitive = false;
            List<string> HsCodes = new List<string>();
            string hs_id = "";
            decimal xid = 0;
            decimal cid = 0;
            decimal pal = 0;
            decimal eic = 0;
            string hs_name = "";
            DateTime date = LocalTime.Now;
            try {

                string connectionString = "";
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=" + HDR + ";IMEX=1\"";
                conn = new OleDbConnection(connectionString);
                conn.Open();

                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                DataRow schemaRow = schemaTable.Rows[0];
                string sheet = schemaRow["TABLE_NAME"].ToString();
                if (!sheet.EndsWith("_")) {
                    string query = "SELECT  * FROM [" + sheet + "]";
                    OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                    dtexcel.Locale = CultureInfo.CurrentCulture;
                    daexcel.Fill(dtexcel);

                }



                stock.Columns.AddRange(new DataColumn[7] {
                       new DataColumn("HS_ID", typeof(string)),
                       new DataColumn("DATE", typeof(DateTime)),
                       new DataColumn("XID", typeof(decimal)),
                       new DataColumn("CID",typeof(decimal)),
                       new DataColumn("PAL",typeof(decimal)),
                       new DataColumn("EIC",typeof(decimal)),
                       new DataColumn("HS_ID_NAME",typeof(string)),
                       });

               
                if (dtexcel.Columns.Count == 6) {
                    List<DutyRates> List = dutyRatesController.GetRates();
                    for (int j = 0; j < dtexcel.Rows.Count; j++) {
                        if (dtexcel.Rows[j][0] != DBNull.Value) {
                            for (int i = 0; i < List.Count; i++) {
                                if (List[i].HsId == dtexcel.Rows[j][0].ToString()) {
                                    HsCodes.Add(List[i].HsId);
                                }
                            }
                        }
                    }
                    if (HsCodes.Count > 0) {
                        List<DutyRates> DutyRatesList = dutyRatesController.GetRatesByHsCodesList(HsCodes);
                        int Result = dutyRatesController.InsertDutyRatesHistory(DutyRatesList);
                    }

                    for (int x = 0; x < dtexcel.Rows.Count; x++) {
                        if (dtexcel.Rows[x][0] != DBNull.Value) {
                            try {
                                hs_id = dtexcel.Rows[x][0].ToString();
                                xid = decimal.Parse(dtexcel.Rows[x][1].ToString() == "" ? "0" : dtexcel.Rows[x][1].ToString());
                                cid = decimal.Parse(dtexcel.Rows[x][2].ToString() == "" ? "0" : dtexcel.Rows[x][2].ToString());
                                pal = decimal.Parse(dtexcel.Rows[x][3].ToString() == "" ? "0" : dtexcel.Rows[x][3].ToString());
                                eic = decimal.Parse(dtexcel.Rows[x][4].ToString() == "" ? "0" : dtexcel.Rows[x][4].ToString());
                                hs_name = dtexcel.Rows[x][5].ToString();

                               

                                if (hs_id != null) {
                                        DataRow dr = stock.NewRow();
                                        dr[0] = hs_id;
                                        dr[1] = date;
                                        dr[2] = xid;
                                        dr[3] = cid;
                                        dr[4] = pal;
                                        dr[5] = eic;
                                        dr[6] = hs_name;
                                        stock.Rows.Add(dr);

                                    }

                                
                               
                            }
                            catch (Exception ex) {
                                throw ex;

                            }
                        }
                    }
                }
                conn.Close();
                return stock;
            }
            catch (Exception ex) {
                throw ex;
            }
            finally {
                conn.Close();

            }

        }

        protected void ddlHsCode_SelectedIndexChanged(object sender, EventArgs e) {
           
            btnDuty.Text = "Update";
            string HSCode = ddlHsCode.SelectedValue;
            if (ddlHsCode.SelectedValue == "0") {
                txtHsCode.Enabled = true;
                txtHsCodeName.Enabled = true;
            }
            else {
                txtHsCode.Enabled = false;
                txtHsCodeName.Enabled = false;
            }

            DutyRates dutyRates = ControllerFactory.CreateDutyRatesController().GetRatesByHSCode(HSCode);
            txtExerciseRate.Text = dutyRates.XID.ToString();
            txtDutyRate.Text = dutyRates.CID.ToString();
            txtPalRate.Text = dutyRates.PAL.ToString();
            txtCessRate.Text = dutyRates.EIC.ToString();
        }

        protected void btnClear_Click(object sender, EventArgs e) {
            txtHsCode.Enabled = true;
            txtHsCodeName.Enabled = true;
            btnDuty.Text = "Save";

            txtHsCode.Text = "";
            txtHsCodeName.Text = "";
            LoadHsCodes();
            txtCessRate.Text = "";
            txtPalRate.Text = "";
            txtDutyRate.Text = "";
            txtExerciseRate.Text = "";
        }
    }


}