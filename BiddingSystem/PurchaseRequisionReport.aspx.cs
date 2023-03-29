using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiddingSystem {
    public partial class PurchaseRequisionReport : System.Web.UI.Page {
        MrnControllerV2 mrnController = ControllerFactory.CreateMrnControllerV2();
        MRNDIssueNoteControllerInterface mrndIssueNoteController = ControllerFactory.CreateMRNDIssueNoteController();
        MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        MRNCapexDocController mRNCapexDocController = ControllerFactory.CreateMRNCapexDocController();
        PrControllerV2 prControllerV2 = ControllerFactory.CreatePrControllerV2();


        protected void Page_Load(object sender, EventArgs e) {
            serializer.MaxJsonLength = Int32.MaxValue;
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != null) {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefMRN";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabMRN";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewMRN.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "viewMRNLink";
            }
            else {
                Response.Redirect("LoginPage.aspx");
            }
            if (!IsPostBack) {
                MrnMasterV2 mrnMaster = mrnController.GetMRNMasterToViewRequisitionReport(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                ViewState["MrnCode"] = "MRN" + mrnMaster.MrnCode;
                ViewState["CreatedBy"] = mrnMaster.CreatedBy;
                lblWarehouseName.Text = mrnMaster.Warehouse.Location;
                lblCategory.Text = mrnMaster.MrnCategoryName;
                lblSubCategory.Text = mrnMaster.MrnSubCategoryName;
                lblExpectedDate.Text = (mrnMaster.ExpectedDate).ToString("dd/MM/yyyy");
                lblMrnCode.Text = "MRN" + (mrnMaster.MrnCode).ToString();
                lblDepartmentName.Text = mrnMaster.SubDepartment.SubDepartmentName;
                lblExpenseType.Text = mrnMaster.ExpenseType == 1 ? "Capital Expense" : "Operational Expense";
                
                mrnMaster.MrnDetails = mrnController.FetchMrnDetailsList(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                gvMRNItems.DataSource = mrnMaster.MrnDetails;
                gvMRNItems.DataBind();

                List< PrMasterV2 > PrMasterV2List = prControllerV2.GetPrMasterList(int.Parse(Request.QueryString.Get("MrnId")), int.Parse(Session["CompanyId"].ToString()));
                List<int> Ids = new List<int>();
                List<string> PrCodes = new List<string>();
                PrMasterV2 prMaster = null;
                if (PrMasterV2List.Count > 0 ) {
                    for (int i = 0; i < PrMasterV2List.Count; i++) {
                        

                         prMaster = prControllerV2.GetPrMasterToView(PrMasterV2List[i].PrId, int.Parse(Session["CompanyId"].ToString()));
                    if (prMaster.MrnId != 0 && prMaster.MrnId == int.Parse(Request.QueryString.Get("MrnId"))) {
                        pnlPR.Visible = true;
                            Ids.Add(PrMasterV2List[i].PrId);
                            PrCodes.Add(PrMasterV2List[i].PrCode);

                            
                        }
                    }
                    prMaster.PrDetails = prControllerV2.FetchPrDetails(Ids, int.Parse(Session["CompanyId"].ToString()));
                    gvPRItems.DataSource = prMaster.PrDetails;
                    gvPRItems.DataBind();

                }

            }
        }

        
        
        
        

        
    }
}