using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class ViewInvoices : System.Web.UI.Page {
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        InvoiceDetailsController invoiceDetailsController = ControllerFactory.CreateInvoiceDetailsController();
        InvoiceImageController invoiceImageController = ControllerFactory.CreateInvoiceImageController();

        protected void Page_Load(object sender, EventArgs e) {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "") {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabId = "ViewInvoicesLink";
                
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(Session["UserId"].ToString()), int.Parse(Session["CompanyId"].ToString()), 6, 27) && companyLogin.Usertype != "S") && companyLogin.Usertype != "GA") {
                    Response.Redirect("AdminDashboard.aspx");
                }
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }


            if (!IsPostBack) {
                if (int.Parse(Session["UserId"].ToString()) != 0) {
                    try {
                       
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }
        }
        protected void btnView_Click(object sender, EventArgs e) {
            int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
            int InvoiceId = int.Parse(gvInvoices.Rows[x].Cells[0].Text);
            ViewState["InvoiceId"] = InvoiceId;

            InvoiceDetails Details = invoiceDetailsController.GetInvoicesByInvId(InvoiceId);

            txtAmount.Text = Details.InvoiceAmount.ToString();
            txtDate.Text = Details.InvoiceDate == DateTime.MinValue ? "" : Details.InvoiceDate.ToString("yyyy-MM-dd");
            txtNewDate.Text = Details.RemarkOn == DateTime.MinValue ? "" : Details.RemarkOn.ToString("yyyy-MM-dd");
            txtInvNo.Text = Details.InvoiceNo == null? "": Details.InvoiceNo;
            txtremark.Text = Details.Remark;
            txtVatNo.Text = Details.VatNo == null ? "" : Details.VatNo;
            if (Details.IsPaymentSettled == 1) {
                ChkPayment.Checked = true;
            }
            else {
                ChkPayment.Checked = false;
            }
            ddlPaymentMethod.SelectedValue = Details.PaymentType.ToString();
            
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlInvUpdate').modal('show'); });   </script>", false);

        }
        protected void btnUpdate_Click(object sender, EventArgs e) {

            int paymentSettled = ChkPayment.Checked == true ? 1 : 0;
            int paymentType = int.Parse(ddlPaymentMethod.SelectedValue);
            int result = invoiceDetailsController.Update(int.Parse(ViewState["InvoiceId"].ToString()), paymentType, txtInvNo.Text, txtDate.Text == "" ? DateTime.MinValue : DateTime.Parse(txtDate.Text), decimal.Parse(txtAmount.Text), txtVatNo.Text, paymentSettled, txtremark.Text, DateTime.Parse(txtNewDate.Text), int.Parse(Session["UserId"].ToString()));

            if (result > 0) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}) });   </script>", false);
                txtAmount.Text = "";
                txtDate.Text = "";
                txtNewDate.Text = "";
                txtInvNo.Text = "";
                txtremark.Text = "";
                txtVatNo.Text = "";
                ChkPayment.Checked = false;
                ddlPaymentMethod.SelectedValue = "1";

                if (rdbGRN.Checked) {

                    //string newString = Regex.Replace(txtGRN.Text, "[^.0-9]", "");
                    //int GRNCode = int.Parse(newString);
                    string GRNCode = txtGRN.Text;

                    gvInvoices.DataSource = invoiceDetailsController.GetInvoicesByGRNCode(int.Parse(Session["CompanyId"].ToString()), GRNCode);
                    gvInvoices.DataBind();

                }
                else {
                    //string newString = Regex.Replace(txtPO.Text, "[^.0-9]", "");
                    //int POCode = int.Parse(newString);
                    string POCode = txtPO.Text;

                    gvInvoices.DataSource = invoiceDetailsController.GetInvoicesByPoCode(int.Parse(Session["CompanyId"].ToString()), POCode);
                    gvInvoices.DataBind();

                }
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('div').removeClass('modal-backdrop'); $('#mdlInvUpdate').modal('hide'); });   </script>", false);

        }
        protected void btnViewImages_Click(object sender, EventArgs e) {
            try {

                int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                int InvoiceId = int.Parse(gvInvoices.Rows[x].Cells[0].Text);
                gvImages.DataSource = invoiceImageController.GetInvoiceImages(InvoiceId);
                gvImages.DataBind();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlImages').modal('show'); });   </script>", false);
            }
            catch (Exception ex) {
                 }
        }

        protected void btnBasicSearch_Click(object sender, EventArgs e) {
            try {
                if (int.Parse(Session["UserId"].ToString()) != 0) {
                    if (rdbGRN.Checked) {

                        //string newString = Regex.Replace(txtGRN.Text, "[^.0-9]", "");
                        //int GRNCode = int.Parse(newString);
                        string GRNCode = txtGRN.Text;

                        gvInvoices.DataSource = invoiceDetailsController.GetInvoicesByGRNCode(int.Parse(Session["CompanyId"].ToString()), GRNCode);
                        gvInvoices.DataBind();

                    }
                    else {
                        //string newString = Regex.Replace(txtPO.Text, "[^.0-9]", "");
                        //int POCode = int.Parse(newString);
                        string POCode = txtPO.Text;

                        gvInvoices.DataSource = invoiceDetailsController.GetInvoicesByPoCode(int.Parse(Session["CompanyId"].ToString()), POCode);
                        gvInvoices.DataBind();

                    }
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#basicSearch').collapse('show'); });   </script>", false);
                }
            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e) {
            try {
                //int x = ((GridViewRow)((Button)sender).NamingContainer).RowIndex;
                //int InvoiceId = int.Parse(gvInvoices.Rows[x].Cells[0].Text);
                int InvoiceId = int.Parse(hdnInvId.Value);

                int delete = invoiceDetailsController.DeleteInvoice(InvoiceId, int.Parse(Session["UserId"].ToString()));

                if (delete > 0) {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'success',title: 'Your work has been saved'}) });   </script>", false);
                    if (rdbGRN.Checked) {
                        
                        string GRNCode = txtGRN.Text;

                        gvInvoices.DataSource = invoiceDetailsController.GetInvoicesByGRNCode(int.Parse(Session["CompanyId"].ToString()), GRNCode);
                        gvInvoices.DataBind();

                    }
                    else {
                        
                       
                        string POCode = txtPO.Text;

                        gvInvoices.DataSource = invoiceDetailsController.GetInvoicesByPoCode(int.Parse(Session["CompanyId"].ToString()), POCode);
                        gvInvoices.DataBind();

                    }
                }
                else {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'Error Occured!'}) });   </script>", false);

                }

            }
            catch (Exception ex) {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title: 'ERROR',text:'Error  " + ex.Message + "- Contact Administrator'}); });   </script>", false);
            }
        }
    }


}