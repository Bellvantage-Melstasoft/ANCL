using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.IO;
using System.Globalization;
using System.Threading.Tasks;

namespace BiddingSystem
{
    public partial class ViewRecommendedforPO : System.Web.UI.Page
    {
        static string UserId = string.Empty;
        static int BidId = 0;
        static int QutationId = 0;
        int CompanyId = 0;
        CompanyUserAccessController companyUserAccessController = ControllerFactory.CreateCompanyUserAccessController();
        PR_MasterController pr_MasterController = ControllerFactory.CreatePR_MasterController();
        CompanyLoginController companyLoginController = ControllerFactory.CreateCompanyLoginController();
        POMasterController poMasterController = ControllerFactory.CreatePOMasterController();
        CommitteeController committeeController = ControllerFactory.CreateProcurementCommitteeController();
        public string datetimepattern = System.Configuration.ConfigurationSettings.AppSettings["datePattern"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CompanyId"] != null && Session["UserId"].ToString() != "")
            {
                ((BiddingAdmin)Page.Master).mainTabValue = "hrefPurchasing";
                ((BiddingAdmin)Page.Master).subTabTitle = "subTabPurchasing";
                ((BiddingAdmin)Page.Master).subTabValue = "ViewRecommendedforPO.aspx";
                ((BiddingAdmin)Page.Master).subTabId = "tecReckLink";

                UserId = Session["UserId"].ToString();
                CompanyId = int.Parse(Session["CompanyId"].ToString());

                

                CompanyLogin companyLogin = companyLoginController.GetUserbyuserId(int.Parse(Session["UserId"].ToString()));

                if ((!companyUserAccessController.isAvilableAccess(int.Parse(UserId), CompanyId, 6, 17) && companyLogin.Usertype != "S") &&  companyLogin.Usertype != "GA")
                {
                    Response.Redirect("AdminDashboard.aspx");
                }
                //else if (Session["DesignationId"] == null || Session["DesignationId"].ToString() != "25")
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "none", "<script>    $(document).ready(function () { swal({ type: 'error',title:'Access Denied' ,text:'You must be Purchasing Officer to view this page', showConfirmButton: true,closeOnConfirm: true}).then((result) => {window.location = 'AdminDashboard.aspx'});  });   </script>", false);
                //}
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
                        List<PR_Master> pr_Master = pr_MasterController.GetPrListForDocumentUploadPoCreation(CompanyId);
                        ((BoundField)gvPurchaseRequest.Columns[4]).DataFormatString = datetimepattern;
                        ((BoundField)gvPurchaseRequest.Columns[8]).DataFormatString = datetimepattern;
                        gvPurchaseRequest.DataSource = pr_Master.OrderByDescending(r => r.PrId).ToList();
                        gvPurchaseRequest.DataBind();
                        
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

          
        }

        

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if ((fileUpload1.PostedFile != null && fileUpload1.PostedFile.ContentLength > 0))
            {
                List<TecCommitteeFileUpload> Committeefiles = new List<TecCommitteeFileUpload>();
                int seq = 0;
                HttpFileCollection uploadedFiles = Request.Files;
                for (int i = 0; i < uploadedFiles.Count; i++)
                {

                    HttpPostedFile userPostedFile = uploadedFiles[i];
                    string path = "";
                    var extention = System.IO.Path.GetExtension(userPostedFile.FileName);
                    string filenameWithoutPath = Path.GetFileName(userPostedFile.FileName);
                    string imagename = LocalTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture) + filenameWithoutPath;
                    path = "~/TechCommitteeDocs/" + imagename;
                    fileUpload1.SaveAs(Server.MapPath("TechCommitteeDocs") + "\\" + imagename);
                    seq = seq + 1;
                    TecCommitteeFileUpload Committeefile = new TecCommitteeFileUpload();
                    Committeefile.BidId = BidId;
                    Committeefile.tabulationId = QutationId;
                    Committeefile.filename = imagename;
                    Committeefile.filepath = path;
                    Committeefile.sequenceId = seq;
                    Committeefiles.Add(Committeefile);


                }

                var isSaved = committeeController.SaveUploadedtechcommitteefiles(Committeefiles);
                if (isSaved>0)
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'success',title: 'Your Documents have been saved', showConfirmButton: false,timer: 1500}); window.location='ViewRecommendedforPO.aspx'; });   </script>", false);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'error',title: 'ERROR',text:'Error Indate time selection', showConfirmButton: true,timer: 4000});$('.modal-backdrop').remove();$('#mdladddocs').modal('show'); });   </script>", false);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('.modal-backdrop').remove();swal({ type: 'error',title: 'ERROR',text:'Error Indate time selection', showConfirmButton: true,timer: 4000});$('.modal-backdrop').remove();$('#mdladddocs').modal('show'); });   </script>", false);
            }
        }

        protected void lbtnupload_Click(object sender, EventArgs e)
        {
            int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
            var gvqutation = (GridView)gvPurchaseRequest.Rows[x].FindControl("gvQuotations");
           QutationId = int.Parse(gvqutation.Rows[x].Cells[0].Text);
            BidId = int.Parse(gvqutation.Rows[x].Cells[1].Text);
            ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script> $(document).ready(function () { $('#mdladddocs').modal('show'); });  </script>", false);
        }

        protected void lbtnDownload_Click(object sender, EventArgs e)
        {
            try
            {

              
                var row = ((GridViewRow)((LinkButton)sender).NamingContainer);
                string path = row.Cells[1].Text;
                path = path.Remove(0, 2);
                string Filpath = Server.MapPath(path);  
                string fileName = row.Cells[0].Text;
                var response = System.Web.HttpContext.Current;
                //var result= await Task.Run(() => StartDownloads(path, fileName, response));
                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { window.open('/" + path + "');$('.modal-backdrop').remove(); $('#mdlviewdocs').modal('show');});   </script>", false);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            //Response.Clear();
            //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name); Response.AddHeader("Content-Length", file.Length.ToString());
            //Response.ContentType = "application/octet-stream";
        }

        //private bool StartDownloads(string path, string fileName,HttpContext response)
        //{
          
        //        string Filpath = Server.MapPath(path);
        //        System.IO.FileInfo file = new System.IO.FileInfo(Filpath);
        //        if (file.Exists)
        //        {
                   
        //            response.Response.ClearContent();
        //            response.Response.Clear();
        //            string ext = Path.GetExtension(path); //get file extension
        //            string type = "";

        //            // set known types based on file extension
        //            //if (ext != null)
        //            //{
        //            //    switch (ext.ToLower())
        //            //    {
        //            //        case ".htm":
        //            //        case ".html":
        //            //            type = "text/HTML";
        //            //            break;

        //            //        case ".txt":
        //            //            type = "text/plain";
        //            //            break;

        //            //        case ".GIF":
        //            //            type = "image/GIF";
        //            //            break;

        //            //        case ".pdf":
        //            //            type = "Application/pdf";
        //            //            break;

        //            //        case ".doc":
        //            //        case ".rtf":
        //            //            type = "Application/msword";
        //            //            break;
        //            //        case ".jpg":
        //            //            type = "image/jpeg";
        //            //            break;
        //            //        case ".csv":
        //            //            type = "text/csv";
        //            //            break;
        //            //        case ".jpeg":
        //            //        case ".xls":
        //            //            type = "application/vnd.xls";
        //            //            break;
        //            //        case ".zip":
        //            //            type = "application/zip";
        //            //            break;
        //            //        case ".ppt":
        //            //            type = "application/vnd.ms-powerpoint";
        //            //            break;
        //            //        case ".png":
        //            //            type = "image/png";
        //            //            break;

        //            //    }
        //            //}
        //            //response.Response.ContentType = type;
        //            //response.Response.AddHeader("Content-Disposition",
        //            //                   "attachment; filename=" + fileName + ";");
        //            //response.Response.TransmitFile(Server.MapPath(path));

        //        response.Response.ContentType = "application/octet-stream";

        //        response.Response.WriteFile(Server.MapPath(path));
        //        response.Response.Flush();
        //        //response.Response.End();
        //        response.Response.SuppressContent = true;
        //        response.ApplicationInstance.CompleteRequest();

        //        return true;
                
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //    //
        //    //
        //    //ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { var iframe = document.getElementById('downloadFrame');iframe.src = '"+ path + "';iframe.type = '" + type + "'; $('.modal-backdrop').remove(); $('#mdlviewdocs').modal('show'); });   </script>", false);

        //}

        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {

                int x = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;
                var gvqutation = (GridView)gvPurchaseRequest.Rows[x].FindControl("gvQuotations");
                QutationId = int.Parse(gvqutation.Rows[x].Cells[0].Text);
                BidId = int.Parse(gvqutation.Rows[x].Cells[1].Text);
                List<TecCommitteeFileUpload> bidingplandoc = committeeController.Gettechcommitteefiles(BidId, QutationId,"T");
                gvbddinplanfiles.DataSource = bidingplandoc;
                gvbddinplanfiles.DataBind();

                ScriptManager.RegisterClientScriptBlock(Updatepanel1, this.Updatepanel1.GetType(), "none", "<script>    $(document).ready(function () { $('#mdlviewdocs').modal('show');});   </script>", false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        protected void gvPurchaseRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var PrId = int.Parse(gvPurchaseRequest.DataKeys[e.Row.RowIndex].Value.ToString());

                PrMasterV2 PrMaster = pr_MasterController.GetPrForPoCreation(PrId, CompanyId, int.Parse(Session["UserId"].ToString()));

                List<SupplierQuotation> quotations = new List<SupplierQuotation>();

                for (int i = 0; i < PrMaster.Bids.Count; i++)
                {
                    quotations.Add(PrMaster.Bids[i].SelectedQuotation);
                }

                GridView gvQuotations = e.Row.FindControl("gvQuotations") as GridView;
                gvQuotations.DataSource = quotations;
                gvQuotations.DataBind();
            }
        }

        protected void gvQuotations_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var IsUploaed=int.Parse(e.Row.Cells[8].Text);
                if (IsUploaed>0)
                {
                    ((LinkButton)e.Row.Cells[9].FindControl("lbtnView")).Visible=true;               
                }
                else
                {
                    ((LinkButton)e.Row.Cells[9].FindControl("lbtnupload")).Visible = true;
                }
            }
        }

        protected void gvbddinplanfiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = e.Row.FindControl("lbtnDownload") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterAsyncPostBackControl(lb);
            }
        }
    }
}