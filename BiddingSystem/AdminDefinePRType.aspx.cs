using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;

namespace BiddingSystem
{
    public partial class AdminDefinePRType : System.Web.UI.Page
    {
     //   int AdminId = 0;
      //  static int prTypeId = 0;
      //  static int CompanyId = 0;
      //  static string prTypeName = string.Empty;
      //  private int isActive = 0;

        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
        PrTypeController prTypeController = ControllerFactory.CreatePrTypeController();

        protected void Page_Load(object sender, EventArgs e)
        {
            int AdminId = 0;
            if (Session["AdminId"] != null)
            {
                AdminId = int.Parse(Session["AdminId"].ToString());
            }
            else
            {
                Response.Redirect("LoginPageAdmin.aspx");
            }


            if (!IsPostBack)
            {
                BindDepartments();
                BidPRTypeDataGrid();
            }
            msg.Visible = false;
        }

        protected void BindDepartments()
        {
            try
            {
                ddlCompany.DataSource = companyDepartmentController.GetDepartmentList().Where(x => x.IsActive == 1).ToList();
                ddlCompany.DataTextField = "DepartmentName";
                ddlCompany.DataValueField = "DepartmentID";
                ddlCompany.DataBind();
                ddlCompany.Items.Insert(0, new ListItem("Select Department", "0"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void BidPRTypeDataGrid()
        {
            try
            {
                gvPrType.DataSource = prTypeController.FetchAllPRTypes();
                gvPrType.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int chkactive = 0;
            if (chkIsavtive.Checked)
            {
                chkactive = 1;
            }
            try
            {
                if (btnSave.Text == "Save")
                {

                    int savePrType = prTypeController.SavePRTypes(int.Parse(ddlCompany.SelectedValue), txtPRType.Text, chkactive);

                    if (savePrType != -1)
                    {
                        if (savePrType > 0)
                        {
                            BidPRTypeDataGrid();
                            ClearFields();
                            DisplayMessage("PR Type has been Created Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on create Sub Category", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("PR Type already exists", true);
                    }
                }
                if (btnSave.Text == "Update")
                {
                    int PrtypeId = int.Parse(Session["PRTYPE_ID"].ToString());
                    int updatePrType = prTypeController.UpdatePRTypes(PrtypeId, int.Parse(ddlCompany.SelectedValue), txtPRType.Text, chkactive);
                    if (updatePrType != -1)
                    {
                        if (updatePrType > 0)
                        {
                            BidPRTypeDataGrid();
                            ClearFields();
                            DisplayMessage("PR Type has been Updated Successfully", false);
                        }
                        else
                        {
                            DisplayMessage("Error on update PR Type", true);
                        }
                    }
                    else
                    {
                        DisplayMessage("PR Type already exists", true);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearFields();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkBtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                BindDepartments();
                int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
               int CompanyId = int.Parse(gvPrType.Rows[x].Cells[0].Text);
                ddlCompany.SelectedValue = CompanyId.ToString();
              string  prTypeName = gvPrType.Rows[x].Cells[3].Text;
                txtPRType.Text = prTypeName;
                Session["PRTYPE_ID"] = gvPrType.Rows[x].Cells[2].Text;
             int   isActive = int.Parse(gvPrType.Rows[x].Cells[4].Text);

                btnSave.Text = "Update";
                if (isActive == 1)
                {
                    chkIsavtive.Checked = true;
                }
                else
                {
                    chkIsavtive.Checked = false;
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { document.body.scrollTop = 50; document.documentElement.scrollTop = 50;});   </script>", false);

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkBtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string prtypeid = hdnPrTypeId.Value;
                string companyid = HiddenField1.Value;

                string prTypeId = prtypeid;
                if (prTypeId != "" && prTypeId != null)
                {
                    int deletePRType = prTypeController.DeletePRTypes(int.Parse(prTypeId), int.Parse(companyid));

                    if (deletePRType > 0)
                    {
                        DisplayMessage("PR Type has been Deleted Successfully", false);
                        BidPRTypeDataGrid();
                        ClearFields();
                    }
                    else
                    {
                        DisplayMessage("Error on Delete PR Type", true);
                    }
                }
                else
                {
                    DisplayMessage("Please Select PR Type to Delete", true);
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPrType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPrType.PageIndex = e.NewPageIndex;
                BidPRTypeDataGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearFields()
        {
            txtPRType.Text = "";
            ddlCompany.SelectedIndex = 0;
            chkIsavtive.Checked = true;
            btnSave.Text = "Save";
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

        protected void confirmation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#modalConfirmYesNo').modal('show'); });   </script>", false);
        }
    }
}