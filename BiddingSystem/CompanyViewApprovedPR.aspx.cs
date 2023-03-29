using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Controller;
using CLibrary.Common;
using CLibrary.Domain;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Reflection;

namespace BiddingSystem
{
    public partial class CompanyViewApprovedPR : System.Web.UI.Page
    {
        string UserId = string.Empty;
        string CompanyId = string.Empty;
        int NewPrId = 0;
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        PR_DetailController pr_DetailController = ControllerFactory.CreatePR_DetailController();
        TempBOMController tempBOMController = ControllerFactory.CreateTempBOMController();
        PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();
        PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        PR_Replace_FileUploadController pr_Replace_FileUploadController = ControllerFactory.CreatePR_Replace_FileUploadController();
        PR_SupportiveDocumentController pR_SupportiveDocumentController = ControllerFactory.CreatePR_SupportiveDocumentController();
        List<PR_Master> pr_Master = new List<PR_Master>();
        List<PR_Details> pr_Details = new List<PR_Details>();
        public static PR_Details pr_detailsStatic = new PR_Details();
        public static List<PR_Details> pr_DetailsDup = new List<PR_Details>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchaseRequest";
                ((BiddingAdmin)Page.Master).subTabValue = "CompanyViewApprovedPR.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "approvedPRLink";

                UserId = Session["UserId"].ToString();
                CompanyId = Session["CompanyId"].ToString();
                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId),int.Parse( CompanyId), 5, 4) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
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
                try
                {
                    pr_Master = pr_MasterController.FetchApprovePRDataByDeptIdReports(int.Parse(CompanyId));
                    pr_DetailsDup = new List<PR_Details>();
                    foreach (var item in pr_Master)
                    {
                        pr_Details = pr_DetailController.FetchDetailsApprovedPR(item.PrId, item.DepartmentId);
                        pr_DetailsDup.AddRange(pr_Details);
                    }
                    gvPRMastre.DataSource = pr_Master;
                    gvPRMastre.DataBind();
                }
                catch (Exception ex)
                {

                }
            }
        }

        

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string prId = gvPRMastre.DataKeys[e.Row.RowIndex].Value.ToString();
                    GridView gvPRDetails = e.Row.FindControl("gvPRDetails") as GridView;

                    gvPRDetails.DataSource = pr_DetailsDup.Where(x => x.PrId == int.Parse(prId));
                    gvPRDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //protected void lnkBtnEdit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int x = ((GridViewRow)((ImageButton)sender).NamingContainer).RowIndex;
        //        int prid = int.Parse(gvPRMastre.Rows[x].Cells[1].Text);
        //        string basePr =  pr_MasterController.FetchRejectPR(prid).BasePrId.ToString();

        //        PR_Master pr_MasterObj = new PR_Master();
        //        pr_MasterObj = pr_MasterController.FetchRejectPR(prid);

        //        if (int.Parse(basePr) == 0 || basePr == "")
        //        {
        //            basePr = prid.ToString();

        //        }
        //        NewPrId = tempBOMController.GetNextPrIdObj(int.Parse(CompanyId));

        //        int save = pr_MasterController.SavePRMaster(int.Parse(CompanyId), pr_MasterObj.DateOfRequest, pr_MasterObj.QuotationFor, pr_MasterObj.OurReference, pr_MasterObj.RequestedBy, pr_MasterObj.CreatedDateTime, pr_MasterObj.CreatedBy, pr_MasterObj.UpdatedDateTime, pr_MasterObj.UpdatedBy, pr_MasterObj.IsActive, 3, pr_MasterObj.PrIsApprovedOrRejectedBy, pr_MasterObj.PrIsApprovedOrRejectForBidDate, pr_MasterObj.PrIsApprovedForBid, pr_MasterObj.PrIsApprovedOrRejectedBy, pr_MasterObj.PrIsApprovedOrRejectForBidDate, int.Parse(basePr), pr_MasterObj.PrTypeid, pr_MasterObj.expenseType, pr_MasterObj.Ref01, pr_MasterObj.Ref02, pr_MasterObj.Ref03, pr_MasterObj.Ref04, "", "");
        //        if (save > 0)
        //        {

        //            List<PR_Details> pr_DetailsList = pr_DetailController.FetchByPRDetails(prid);
        //            foreach (var item in pr_DetailsList)
        //            {
        //                pr_DetailController.SavePRDetails(NewPrId, item.ItemId, 1, item.ItemDescription, item.ItemUpdatedBy, item.ItemUpdatedDateTime, item.IsActive, item.Replacement, item.ItemQuantity, item.Purpose, item.EstimatedAmount);
        //            }

        //            List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetListRejected(prid);

        //            foreach (var item in pr_BillOfMeterial)
        //            {
        //                pr_BillOfMeterialController.SaveBillOfMeterial(NewPrId, item.ItemId, item.SeqId, item.Meterial, item.Description, item.IsActive, item.CreatedDateTime, item.CreatedBy, item.UpdatedDateTime, item.UpdatedBy);
        //            }

        //            List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFilesRejected(prid);

        //            foreach (var item in pr_FileUpload)
        //            {
        //                string[] getName = Regex.Split(item.FilePath, "/");
        //                string[] newFileNAme = Regex.Split(getName[3], "_");
        //                string newFile = NewPrId + "_" + newFileNAme[1] + "_" + newFileNAme[2];
        //                string NewFilePath = getName[0] + "/" + getName[1] + "/" + NewPrId + "/" + newFile;
        //                pr_FileUploadController.SaveFileUpload(int.Parse(CompanyId), item.ItemId, NewPrId, NewFilePath, item.FileName);
        //            }

        //            List<PR_Replace_FileUpload> _PR_Replace_FileUpload = pr_Replace_FileUploadController.FtechUploadeFilesRejected(prid);

        //            foreach (var item in _PR_Replace_FileUpload)
        //            {
        //                string[] getName = Regex.Split(item.FilePath, "/");
        //                string[] newFileNAme = Regex.Split(getName[3], "_");
        //                string newFile = NewPrId + "_" + newFileNAme[1] + "_" + newFileNAme[2];
        //                string NewFilePath = getName[0] + "/" + getName[1] + "/" + NewPrId + "/" + newFile;
        //                pr_Replace_FileUploadController.SaveFileUpload(int.Parse(CompanyId), item.ItemId, NewPrId, NewFilePath, item.FileName);
        //            }

        //            List<PR_SupportiveDocument> pR_SupportiveDocument = pR_SupportiveDocumentController.FtechUploadeSupportiveDocmentsRejected(prid);

        //            foreach (var item in pR_SupportiveDocument)
        //            {
        //                string[] getName = Regex.Split(item.FilePath, "/");
        //                string[] newFileNAme = Regex.Split(getName[3], "_");
        //                string newFile = NewPrId + "_" + newFileNAme[1] + "_" + newFileNAme[2];
        //                string NewFilePath = getName[0] + "/" + getName[1] + "/" + NewPrId + "/" + newFile;
        //                pR_SupportiveDocumentController.SaveSupporiveFileUpload(int.Parse(CompanyId), item.ItemId, NewPrId, NewFilePath, item.FileName);
        //            }


        //            //----Create new Directory

        //            string NewDirectory = Server.MapPath("PurchaseRequestFiles/" + NewPrId);
        //            int returnType = CreateDirectoryIfNotExists(NewDirectory);

        //            var source = Server.MapPath("PurchaseRequestFiles/");
        //            var target = Server.MapPath("PurchaseRequestFiles");

        //            var sourceFolder = System.IO.Path.Combine(source, prid.ToString());
        //            var targetFolder = System.IO.Path.Combine(target, NewPrId.ToString());

        //            CopyFolderContents(sourceFolder, targetFolder);




                
        //            string ReplacementNewDirectory = Server.MapPath("PrReplacementFiles/" + NewPrId);
        //            int ReplacementreturnType = CreateDirectoryIfNotExists(ReplacementNewDirectory);

        //            var Replacementsource = Server.MapPath("PrReplacementFiles/");
        //            var Replacementtarget = Server.MapPath("PrReplacementFiles");

        //            var ReplacementsourceFolder = System.IO.Path.Combine(Replacementsource, prid.ToString());
        //            var ReplacementtargetFolder = System.IO.Path.Combine(Replacementtarget, NewPrId.ToString());

        //            CopyFolderContents(ReplacementsourceFolder, ReplacementtargetFolder);




        //            string SupportiveNewDirectory = Server.MapPath("PrSupportiveFiles/" + NewPrId);
        //            int SupportivereturnType = CreateDirectoryIfNotExists(SupportiveNewDirectory);

        //            var Supportivesource = Server.MapPath("PrSupportiveFiles/");
        //            var Supportivetarget = Server.MapPath("PrSupportiveFiles");

        //            var SupportivesourceFolder = System.IO.Path.Combine(Supportivesource, prid.ToString());
        //            var SupportivetargetFolder = System.IO.Path.Combine(Supportivetarget, NewPrId.ToString());

        //            CopyFolderContents(SupportivesourceFolder, SupportivetargetFolder);


        //            lblMsg.Text = "Sucessfully created a new PR. Pr Code : " + "PR" + NewPrId;
        //        }

        //        if (save < 0 || save == 0)
        //        {
        //            lblError.Text = "Unsucessfull attempt";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblError.Text = "Unsucessfull attempt" + ex;
        //        throw ex;
        //    }
        //}


        ////------------------Check Create Directory Existing or Not 
        //private int CreateDirectoryIfNotExists(string NewDirectory)
        //{
        //    try
        //    {
        //        int returnType = 0;
        //        // Checking the existance of directory
        //        if (!Directory.Exists(NewDirectory))
        //        {
        //            //delete
        //            //If No any such directory then creates the new one
        //            Directory.CreateDirectory(NewDirectory);
        //            returnType = 1;
        //        }
        //        else
        //        {
        //            //Label1.Text = "Directory Exist";
        //            returnType = 0;
        //        }
        //        return returnType;
        //    }
        //    catch (IOException _err)
        //    {
        //        throw _err;
        //        //Label1.Text = _err.Message; ;
        //    }
        //}


        ////-----------OriginalDirectory
        //private bool CopyFolderContents(string SourcePath, string DestinationPath)
        //{
        //    SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
        //    DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";
        //    try
        //    {
        //        if (Directory.Exists(SourcePath))
        //        {
        //            if (Directory.Exists(DestinationPath) == false)
        //            {
        //                Directory.CreateDirectory(DestinationPath);
        //            }

        //            foreach (string files in Directory.GetFiles(SourcePath))
        //            {
        //                FileInfo fileInfo = new FileInfo(files);
        //                string[] newFileNAme = Regex.Split(fileInfo.Name, "_");
        //                string newFileNameNewPr = NewPrId + "_" + newFileNAme[1] + "_" + newFileNAme[2];
        //                fileInfo.CopyTo(string.Format(@"{0}\{1}", DestinationPath, newFileNameNewPr), true);
        //            }

        //            foreach (string drs in Directory.GetDirectories(SourcePath))
        //            {
        //                DirectoryInfo directoryInfo = new DirectoryInfo(drs);
        //                if (CopyFolderContents(drs, DestinationPath + directoryInfo.Name) == false)
        //                {
        //                    return false;
        //                }
        //            }
        //            //---Delete From Temporary Files
        //           // System.IO.Directory.Delete(SourcePath, true);
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}

        //protected void btnBOM_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
        //        GridView gv = ((GridView)gvPRMastre.Rows[1].Cells[1].FindControl("gvPRDetails"));
        //        int prid = int.Parse(gvPRMastre.Rows[x].Cells[0].Text);
        //        int itemId = int.Parse(gvPRMastre.Rows[x].Cells[1].Text);
        //        List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetList(prid, itemId);
        //        gvBOMDate.DataSource = pr_BillOfMeterial;
        //        gvBOMDate.DataBind();

        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { $('#myModal2').modal('show'); });   </script>", false);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "go")
        //    {
        //        GridViewRow gvR = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
        //        GridView grid2 = (GridView)(gvR.FindControl("gvPRDetails"));

        //        foreach (GridViewRow gv2Row in grid2.Rows)
        //        {
        //            //Label lbl = (Label)(gv2Row.FindControl("Label4"));
        //            //string grid2Rowtext = lbl.Text;
        //            //Response.Write(grid2Rowtext + "<br>");

        //        }
        //    }
        //}

    }
}