using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Script.Serialization;
using CLibrary.Domain;
using CLibrary.Controller;
using CLibrary.Common;
using System.Web;
using System.IO;
using System.Drawing;

namespace BiddingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BiddingService" in code, svc and config file together.
    public class BiddingService : IBiddingService
    {
        public Response GetAllBiddingData(string supplierId)
        {
            Response response = new Response();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<Bidding> BidList = biddingController.GetAllBidding(int.Parse(supplierId));

                int status;
                if (BidList.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(BidList);
                }

                else if (BidList.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response GetAllLatestBiddingData(string supplierId)
        {
            Response response = new Response();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<Bidding> BidList = biddingController.GetAllLatestBids(int.Parse(supplierId));

                int status;
                if (BidList.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(BidList);
                }

                else if (BidList.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = "Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response SupplierLogin(string username, string password)
        { 
            Response response = new Response();
            SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                SupplierLogin supplierLogin = supplierLoginController.SupplierLogin(username, password);

                int status;
                if (supplierLogin.Supplierid != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierLogin);
                }

                else if (supplierLogin.Supplierid == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = "Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response GetAllCategories()
        {
            Response response = new Response();
            ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<ItemCategory> itemCategory = itemCategoryController.FetchItemCategoryList(1);

                int status;
                if (itemCategory.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(itemCategory);
                }

                else if (itemCategory.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = "Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response GetSubCategoriesByCategoryId(string categoryId)
        {
            Response response = new Response();
            ItemSubCategoryController itemSubCategoryController = ControllerFactory.CreateItemSubCategoryController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<ItemSubCategory> itemSubCategory = itemSubCategoryController.FetchItemSubCategoryByCategoryId(int.Parse(categoryId),1);

                int status;
                if (itemSubCategory.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(itemSubCategory);
                }

                else if (itemSubCategory.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = "Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response GetItemsByCategoryAndMainCategory(string categoryId, string subcategoryId, string companyId)
        {
            Response response = new Response();
            AddItemController addItemController = ControllerFactory.CreateAddItemController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<AddItem> addItem = addItemController.FetchItemsByCategories(int.Parse(categoryId), int.Parse(subcategoryId), int.Parse(companyId));

                int status;
                if (addItem.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(addItem);
                }

                else if (addItem.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = "Something went wrong. Please try again later.";
            }
            return response;
        }

        //--Bid Detailed wise (Get all data of one bid) --BidSubmission Also getting data through this service
        public Response GetPRItemDataEachBid(string prId, string itemId, string biddingorderId)
        {
            Response response = new Response();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                Bidding BidList = biddingController.GetBiddingDetailsSvc(int.Parse(prId), int.Parse(itemId), biddingorderId);

                int status;
                if (BidList.PrId > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(BidList);
                }

                else if (BidList.PrId <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }
        //--Fetch Images 
        public Response GetPRItemImagesEachBid(string prId, string itemId)
        {
            Response response = new Response();
            PR_FileUploadController pr_FileUploadController = ControllerFactory.CreatePR_FileUploadController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<PR_FileUpload> pr_FileUpload = pr_FileUploadController.FtechUploadeFiles(int.Parse(prId), int.Parse(itemId));

                int status;
                if (pr_FileUpload.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(pr_FileUpload);
                }

                else if (pr_FileUpload.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }
        //--PR-BOM Details
        public Response GetPRBOMDetailsEachBid(string prId, string itemId)
        {
            Response response = new Response();
            PR_BillOfMeterialController pr_BillOfMeterialController = ControllerFactory.CreatePR_BillOfMeterialController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<PR_BillOfMeterial> pr_BillOfMeterial = pr_BillOfMeterialController.GetList(int.Parse(prId), int.Parse(itemId));

                int status;
                if (pr_BillOfMeterial.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(pr_BillOfMeterial);
                }

                else if (pr_BillOfMeterial.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }
        //--Btn Later Click
        public Response SavePendingBidStatus(string prId, string itemId,string supplierId, string biddingorderId,string isVatInclude)
        {
            Response response = new Response();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                int supplierQuotation = supplierQuotationController.SaveQuatation( int.Parse(itemId), int.Parse(prId), int.Parse(supplierId), 0, 1, "", 0, 0, 0, 0, "", 0, "", biddingorderId,int.Parse( isVatInclude));

                int status;
                if (supplierQuotation > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierQuotation);
                }

                else if (supplierQuotation <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //------Apply Now-------

        //----Check Bid Number Existing or Not According To the Supplier
        public Response GetBiddingAlredyExistingForLoggedSupplier(string prId, string itemId, string supplierId)
        {
            Response response = new Response();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                Bidding bidding = biddingController.GetBiddingDetailsExisting(int.Parse(prId), int.Parse(itemId), int.Parse(supplierId));

                int status;
                if (bidding.PrId > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(bidding);
                }

                else if (bidding.PrId <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //--------Get Supplier Pending Bids
        public Response GetSuppliersPendingBids(string supplierId)
        {
            Response response = new Response();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<SupplierQuotation> supplierQuotation = supplierQuotationController.GetSupplierPendingBids(int.Parse(supplierId));

                int status;
                if (supplierQuotation.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierQuotation);
                }

                else if (supplierQuotation.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //--------Get Supplier Received PO
        public Response GetSupplierReceivedPOs(string supplierId)
        {
            Response response = new Response();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<Bidding> bidding = biddingController.GetRaisedPOSupplier(int.Parse(supplierId));

                int status;
                if (bidding.Count != 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(bidding);
                }

                else if (bidding.Count == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //-------Get Supplier Received PO Details 
        public Response GetSupplierReceivedPODetailed(string poId)
        {
            Response response = new Response();
            POMasterController poMasterController = ControllerFactory.CreatePOMasterController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                POMaster poMaster = poMasterController.GetPoMasterObjByPoIdView(int.Parse(poId));

                int status;
                if (poMaster.PoID > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(poMaster);
                }

                else if (poMaster.PoID <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //------Save Bidded
        public Response SaveSupplierBidForItem(string itemId, string prId, string supplierId, string itemPrice, string vatAmount, string nbtAmount, string totatlAmount, string SupplierTermsConditions, string BidOrderingNo,string isVatInclude)
        {
            Response response = new Response();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                int supplierQuotation = supplierQuotationController.SaveQuatation(int.Parse(itemId), int.Parse(prId), int.Parse(supplierId),int.Parse(itemPrice),0,"",decimal.Parse(vatAmount), decimal.Parse(nbtAmount), decimal.Parse(totatlAmount),0, "", 0, SupplierTermsConditions, BidOrderingNo ,int.Parse( isVatInclude));

                int status;
                if (supplierQuotation > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierQuotation);
                }

                else if (supplierQuotation <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //------Update Bidded
        public Response UpdateSupplierBidForItem(string existingQuotationId, string perItemPrice, string VatAmount, string NbtAmount, string TotalAmount, string TermsandConditions, string isVatInclude)
        {
            Response response = new Response();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                //int UpdatePendingBids(int quotationId, decimal PerItemPrice, decimal VatAmount, decimal NbtAmount, decimal TotalAmount, string TermsandConditions
                int supplierQuotation = supplierQuotationController.UpdatePendingBids(int.Parse(existingQuotationId), decimal.Parse(perItemPrice), decimal.Parse(VatAmount), decimal.Parse(NbtAmount), decimal.Parse(TotalAmount), TermsandConditions,int.Parse( isVatInclude));

                int status;
                if (supplierQuotation > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierQuotation);
                }

                else if (supplierQuotation <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //------Supplier Bid Status ---BidHistory Table
        public Response SaveSupplierBiddingStatus(string quotationNo, string bidderId, string unitPrice, string vatAmount, string nbtAmount, string totalAmount, string bidSubmittedDate)
        {
            Response response = new Response();
            BidHistoryController bidHistoryController = ControllerFactory.CreateBidHistoryController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                int saveBidHistory = bidHistoryController.SaveBidHistory(int.Parse(quotationNo), "S",int.Parse(bidderId), decimal.Parse(unitPrice), decimal.Parse(vatAmount), decimal.Parse(nbtAmount), decimal.Parse(totalAmount), DateTime.Parse(bidSubmittedDate.ToString()));

                int status;
                if (saveBidHistory > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(saveBidHistory);
                }

                else if (saveBidHistory <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //-------2018-09-28
        //-------Supplier Register -- Login Page
        public Response SupplierRegistraion(string username, string password, string email, string supplierName, string officeContactno)
        {
            Response response = new Response();

            SupplierLoginController supplierLoginController = ControllerFactory.CreateSupplierLoginController();
            SupplierController supplierController = ControllerFactory.CreateSupplierController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            try
            {
                string result = string.Empty;
                int saveSupplierSave = 0;
                //int supplierID = supplierLoginController.saveSupplierLogin(username, password, email, 0, 1);
                int supplierID = 0;
                if (supplierID != -1)
                {
                    if (supplierID > 0)
                    {
                        saveSupplierSave = supplierController.saveSupplierSVC(supplierID, supplierName, "", "", email, "", officeContactno, LocalTime.Now.ToString("yyyy-MM-dd"), "", "", 0, 0, "", 1, 0, 0, 1);
                    }
                    
                    if(supplierID > 0 && saveSupplierSave > 0)
                    {
                        response.ID = 200;
                        response.Data = javaScriptSerializer.Serialize(supplierID);
                    }
                    else
                    {
                        response.ID = 500;
                        response.Data = "Error on Supplier Registration";
                    }
                }
                else
                {
                    response.ID = 400;
                    response.Data ="Username or Email is already exists";
                }
                
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //-------Create Profile 
        public Response GetExistingDataSupplierRegistration(string supplierId)
        {
            Response response = new Response();
            SupplierController supplierController = ControllerFactory.CreateSupplierController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                Supplier SupplierList = supplierController.GetSupplierBySupplierId(int.Parse(supplierId));

                int status;
                if (SupplierList.SupplierId > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(SupplierList);
                }

                else if (SupplierList.SupplierId <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //-------Load Nature Of Business
        public Response LoadNatureOfBusiness()
        {
            Response response = new Response();
            NaturseOfBusinessController naturseOfBusinessController = ControllerFactory.CreateNaturseOfBusinessController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<NaturseOfBusiness> naturseOfBusiness = naturseOfBusinessController.FetchBusinessCategoryList();

                int status;
                if (naturseOfBusiness.Count > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(naturseOfBusiness);
                }

                else if (naturseOfBusiness.Count <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //-------Load Main Category
        public Response LoadMainCategoriesSupplierPortal()
        {
            Response response = new Response();
            ItemCategoryController itemCategoryController = ControllerFactory.CreateItemCategoryController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                List<ItemCategory> itemCategoryList = itemCategoryController.FetchItemCategoryList(1);

                int status;
                if (itemCategoryList.Count > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(itemCategoryList);
                }

                else if (itemCategoryList.Count <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //-------Load Uploaded File
        public Response SupplierLoadUploadedFile(string supplierId)
        {
            Response response = new Response();
            SuplierImageUploadController suplierImageUploadController = ControllerFactory.CreateSuplierImageUploadController();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            try
            {
                List<SuplierImageUpload> suplierImageUpload = suplierImageUploadController.GetSupplierImagesBySupplierId(int.Parse(supplierId)).ToList();
                int status;
                if (suplierImageUpload.Count > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(suplierImageUpload);
                }

                else if (suplierImageUpload.Count <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //------Update Supplier Data
        //logo_path = "~/Supplier/Logo/FilenameWithExtention"
        //Eg:  ~/Supplier/Logo/10003_1.jpg
        public Response UpdateSupplierDetails(string supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, string companytypeId, string businessCategory, string logoPath, string IsrequestFromSupplier, string IdCreatedBAmin, string IsApproved, string IsActive)
        {
            Response response = new Response();
            SupplierController supplierController = ControllerFactory.CreateSupplierController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                int supplierdata = supplierController.updateSupplier(int.Parse(supplierId), supplierName, address1, address2, officeContactno, mobileno, businssRegNo, vatregNo, int.Parse(companytypeId), int.Parse(businessCategory), logoPath, 0, 1, 1, 1 ,"", 0, 0, ""); // since not using services temporary email empty send

                if (supplierdata > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierdata);
                }

                else if (supplierdata <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        

        public Response SaveSupplierLogo(SaveSupplierLogo saveSupplierLogo)
        {

            Response response = new Response();

            SupplierController supplierController = ControllerFactory.CreateSupplierController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                //string solutionDir = System.Configuration.ConfigurationManager.AppSettings["rootFolder"].ToString();// System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                string solutionDir = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                //solutionDir = solutionDir.Replace("BiddingService\\", "BiddingSystem\\");
                MemoryStream stream = new MemoryStream(saveSupplierLogo.logo);
                
                
                Image logo = Image.FromStream(stream);
                if (File.Exists(solutionDir+"Supplier\\Logo\\" + saveSupplierLogo.supplierId + "_1" + saveSupplierLogo.extension))
                {
                    File.Delete(solutionDir+"Supplier\\Logo\\" + saveSupplierLogo.supplierId + "_1" + saveSupplierLogo.extension);
                }
                logo.Save(solutionDir+"Supplier\\Logo\\" + saveSupplierLogo.supplierId + "_1" + saveSupplierLogo.extension);


                int supplierdata = supplierController.saveSupplierLogo(saveSupplierLogo.supplierId, "~/Supplier/Logo/" + saveSupplierLogo.supplierId + "_1" + saveSupplierLogo.extension);
                if (supplierdata > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierdata);
                }
                else if (supplierdata <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";

            }
            return response;

        }

        public Response DeleteSupplierDocument(string imageId,string imageType)
        {

            Response response = new Response();

            SuplierImageUploadController supplierImageController = ControllerFactory.CreateSuplierImageUploadController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                string solutionDir = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                //string solutionDir = System.Configuration.ConfigurationManager.AppSettings["rootFolder"].ToString();
                if (File.Exists(solutionDir+"Supplier\\Documents\\" + imageId + imageType))
                {
                    File.Delete(solutionDir+"Supplier\\Documents\\" + imageId + imageType);
                }

                int supplierdata = supplierImageController.deleteUploadedSupplierFile(imageId);
                

                if (supplierdata > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierdata);
                }

                else if (supplierdata <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response SaveSupplierDocuments(SupplierDocuments supplierDocuments)
        {

            Response response = new Response();

            SuplierImageUploadController supplierImageController = ControllerFactory.CreateSuplierImageUploadController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {

                string solutionDir = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                //string solutionDir = System.Configuration.ConfigurationManager.AppSettings["rootFolder"].ToString();

                int lastImageId = 0;

                if (supplierImageController.GetSupplierImagesBySupplierId(supplierDocuments.supplierId).Count > 0)
                    lastImageId = int.Parse((supplierImageController.GetSupplierImagesBySupplierId(supplierDocuments.supplierId)[0].ImageId).Split('_').Last().ToString());

                List<SuplierImageUpload> list = new List<SuplierImageUpload>();

                foreach (SupplierDocument doc in supplierDocuments.docs)
                {
                    lastImageId += 1;

                    SuplierImageUpload newDoc = new SuplierImageUpload();
                    newDoc.SupplierId = supplierDocuments.supplierId;
                    newDoc.ImageId = supplierDocuments.supplierId + "_" + lastImageId;
                    newDoc.IsActive = 1;
                    newDoc.ImageFileName = supplierDocuments.supplierId + "_" + lastImageId + doc.type;
                    newDoc.SupplierImagePath = "~/Supplier/Documents/";
                    list.Add(newDoc);
                    File.WriteAllBytes(solutionDir+"Supplier\\Documents\\" + supplierDocuments.supplierId + "_" + lastImageId + doc.type,doc.doc);

                }
                

                int result = supplierImageController.saveUploadedSupplierImage(list);
                if(result > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(result);
                }
                else if (result <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";

            }
            return response;
        }


        public Response SaveSupplierBiddingDocuments(SupplierBiddingDocuments supplierBiddingDocuments)
        {

            Response response = new Response();

            SupplierBiddingFileUploadController controller = ControllerFactory.CreateSupplierBiddingFileUploadController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {

                string solutionDir = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                //string solutionDir = System.Configuration.ConfigurationManager.AppSettings["rootFolder"].ToString();

                if (!(Directory.Exists(solutionDir+"\\SupplierBiddingFileUpload\\" + supplierBiddingDocuments.supplierId)))
                {
                    Directory.CreateDirectory(solutionDir+"\\SupplierBiddingFileUpload\\" + supplierBiddingDocuments.supplierId);
                }

                if (!(Directory.Exists(solutionDir+"\\SupplierBiddingFileUpload\\" + supplierBiddingDocuments.supplierId + "\\" + supplierBiddingDocuments.prId + "_" + supplierBiddingDocuments.itemId)))
                {
                    Directory.CreateDirectory(solutionDir+"\\SupplierBiddingFileUpload\\" + supplierBiddingDocuments.supplierId + "\\" + supplierBiddingDocuments.prId + "_" + supplierBiddingDocuments.itemId);
                }

                int lastImageId = 0;

                if (controller.GetFilesByQuotationId(supplierBiddingDocuments.quotationId).Count > 0)
                    lastImageId = int.Parse((controller.GetFilesByQuotationId(supplierBiddingDocuments.quotationId)[0].FileName.Split('.').First()).Split('_').Last().ToString());

                List<SupplierBiddingFileUpload> list = new List<SupplierBiddingFileUpload>();

                foreach (SupplierBiddingDocument doc in supplierBiddingDocuments.docs)
                {
                    lastImageId += 1;

                    SupplierBiddingFileUpload newDoc = new SupplierBiddingFileUpload();
                    //newDoc.SupplierId = supplierBiddingDocuments.supplierId;
                    //newDoc.QuotationId = supplierBiddingDocuments.quotationId;
                    //newDoc.PrId = supplierBiddingDocuments.prId;
                    //newDoc.ItemId = supplierBiddingDocuments.itemId;
                    newDoc.FileName = supplierBiddingDocuments.quotationId + "_" + lastImageId + doc.type;
                    newDoc.FilePath = "~/SupplierBiddingFileUpload/" + supplierBiddingDocuments.supplierId + "/" + supplierBiddingDocuments.prId + "_" + supplierBiddingDocuments.itemId + "/";
                    list.Add(newDoc);
                    File.WriteAllBytes(solutionDir+"\\SupplierBiddingFileUpload\\" + supplierBiddingDocuments.supplierId + "\\" + supplierBiddingDocuments.prId + "_" + supplierBiddingDocuments.itemId + "\\" + supplierBiddingDocuments.quotationId + "_" + lastImageId + doc.type, doc.doc);

                }


                int result = controller.SaveFiles(list);
                if (result > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(result);
                }
                else if (result <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";

            }
            return response;
        }

        public Response DeleteSupplierBiddingDocument(string fileName,string supplierId,string prId,string itemId)
        {

            Response response = new Response();

            SupplierBiddingFileUploadController controller = ControllerFactory.CreateSupplierBiddingFileUploadController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {

                string solutionDir = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
                //string solutionDir = System.Configuration.ConfigurationManager.AppSettings["rootFolder"].ToString();

                if (File.Exists(solutionDir+"\\SupplierBiddingFileUpload\\" + supplierId + "\\" + prId + "_" + itemId+"\\"+fileName))
                {
                    File.Delete(solutionDir+"\\SupplierBiddingFileUpload\\" + supplierId + "\\" + prId + "_" + itemId + "\\" + fileName);
                }

                int supplierdata = controller.DeleteFileUploads(fileName);


                if (supplierdata > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierdata);
                }

                else if (supplierdata <= 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response GetReceivedSupplierPO(string supplierId)
        {
            Response response = new Response();

            BiddingController controller = ControllerFactory.CreateBiddingController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            List<Bidding> bidding = new List<Bidding>();
            try
            {
                bidding=controller.GetRaisedPOSupplier(int.Parse(supplierId));
                if (bidding!=null)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(bidding);
                }
                else
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response GetReceivedSupplierPOItems(string poId)
        {
            Response response = new Response();

            POMasterController controller = ControllerFactory.CreatePOMasterController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            POMaster pOMaster = new POMaster();
            POItems items = new POItems();
            try
            {
                pOMaster = controller.GetPoMasterObjByPoIdView(int.Parse(poId));
                
                if (pOMaster != null)
                {
                    decimal SubTotal = 0;
                    decimal VatTotal = 0;
                    decimal NbtTotal = 0;
                    decimal SubTotalCus = 0;
                    decimal VatTotalCus = 0;
                    decimal NbtTotalCus = 0;
                    decimal TotalVat = 0;
                    decimal TotalNbt = 0;
                    decimal TotalSubAmount = 0;
                    decimal TotalAmount = 0;

                    foreach (var item in pOMaster._PODetails)
                    {
                        if (item.IsCustomizedAmount == 1)
                        {
                            SubTotalCus = SubTotalCus + item.CustomizedAmount * item.Quantity;
                            VatTotalCus = VatTotalCus + item.CustomizedVat;
                            NbtTotalCus = NbtTotalCus + item.CustomizedNbt;
                        }

                        if (item.IsCustomizedAmount == 0)
                        {
                            SubTotal = SubTotal + item.ItemPrice * item.Quantity;
                            VatTotal = VatTotal + item.VatAmount;
                            NbtTotal = NbtTotal + item.NbtAmount;
                        }

                        TotalSubAmount = SubTotal + SubTotalCus;
                        TotalNbt = NbtTotal + NbtTotalCus;
                        TotalVat = VatTotalCus + VatTotal;

                        TotalAmount = TotalSubAmount + TotalNbt + TotalVat;


                    }

                    items.PO = pOMaster;
                    items.TotalAmount = TotalAmount;
                    items.TotalNbt = TotalNbt;
                    items.TotalSubAmount = TotalSubAmount;
                    items.TotalVat = TotalVat;

                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(items);
                }
                else
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }


        //-----Post Supplier Image
        //public Response PostImage(Stream stream,string supplierId)
        //{
        //    Response response = new Response();
        //    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
        //    try
        //    {
        //        string FileName = string.Empty;

        //        SupplierController supplierController = ControllerFactory.CreateSupplierController();
        //        Supplier supplier =supplierController.GetSupplierBySupplierId(int.Parse(supplierId));
        //        if (supplier.SupplierLogo != "")
        //        {
        //            string[] filename = supplier.SupplierLogo.Split(new[] { "/" }, StringSplitOptions.None);
        //            FileName = filename[2];
        //            byte[] buffer = new byte[10000];
        //            stream.Read(buffer, 0, 10000);
        //            FileStream f = new FileStream("E:\\BiddingSystem\\BiddingSystem\\Supplier\\Logo\\" + FileName, FileMode.OpenOrCreate);
        //            f.Write(buffer, 0, buffer.Length);
        //            f.Close();
        //            stream.Close();
        //            response.Data = "Recieved the image on server";
        //            response.ID = 200;
        //        }
        //        else
        //        {
        //            response.ID = 300;
        //            response.Data = "Supplier Logo not uploaded.";
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        response.Data = ex.ToString();
        //        response.ID = 500;
        //    }
        //    return response;
        //}

        public Response GetBiddingStatusOfSupplier(string prId, string itemId, string supplierId)
        {
            Response response = new Response();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                SupplierQuotation supplierQuotation = supplierQuotationController.GetGivenQuotatios(int.Parse(prId), int.Parse(itemId), int.Parse(supplierId));

                //if (supplierQuotation.BidOpeningId != "")
                //{
                //    response.ID = 200;
                //    response.Data = javaScriptSerializer.Serialize(supplierQuotation);
                //}

                //else if (supplierQuotation.BidOpeningId == "")
                //{
                //    response.ID = 300;
                //    response.Data = "No records found.";
                //}
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        //-----------Save Supplier tocken
        public Response SaveSupplierTocken(string supplierId, string tockenId)
        {
            Response response = new Response();
            SupplierController supplierController = ControllerFactory.CreateSupplierController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                int updateToken = supplierController.UpdateSupplierDeviceTocken(int.Parse(supplierId), tockenId);

                if (updateToken > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(updateToken);
                }

                else if (updateToken == 0)
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response SupplierEditBids(string prId, string itemId, string supplierId)
        {
            Response response = new Response();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
            SupplierBOMController supplierBOMController = ControllerFactory.CreatesupplierBOMController();
            SupplierBiddingFileUploadController supplierBiddingFileUploadController = ControllerFactory.CreateSupplierBiddingFileUploadController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                SupplierQuotation supplierQuotationObj = supplierQuotationController.GetSupplierEditBidDetails(int.Parse(prId), int.Parse(itemId), int.Parse(supplierId));
                //supplierQuotationObj.fetchEditBidAttachements= supplierBiddingFileUploadController.GetFilesByQuotationId(int.Parse(prId), int.Parse(itemId), int.Parse(supplierId));
                //supplierQuotationObj.fetchEditBidBom = supplierBOMController.GetSupplierList(int.Parse(supplierId), int.Parse(prId), int.Parse(itemId));

                if (supplierQuotationObj.SupplierId > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(supplierQuotationObj);
                }

                else 
                {
                    response.ID = 300;
                    response.Data = "No records found.";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

       

        public Response SaveSupplierBOM(string prId, string itemId, string supplierId, string SeqNo, string Meterial, string Description, string Comply, string Remarks)
        {
            Response response = new Response();
            SupplierBOMController supplierBOMController = ControllerFactory.CreatesupplierBOMController();

            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                int submitSupplierBOM = supplierBOMController.SaveSupplierBOM(int.Parse(supplierId), int.Parse(prId), int.Parse(itemId), int.Parse(SeqNo), Meterial, Description, 1, LocalTime.Now, int.Parse(Comply), Remarks);

                int status;
                if (submitSupplierBOM > 0)
                {
                    response.ID = 200;
                    response.Data = javaScriptSerializer.Serialize(submitSupplierBOM);
                }

                else
                {
                    response.ID = 300;
                    response.Data = "Supplier Bom Submission Failed";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message; //"Something went wrong. Please try again later.";
            }
            return response;
        }

        public Response SubmitSupplierBid(SupplierBids supplierBidsObj)
        {
            Response response = new Response();
            SupplierBOMController supplierBOMController = ControllerFactory.CreatesupplierBOMController();
            BiddingController biddingController = ControllerFactory.CreateBiddingController();
            SupplierQuotationController supplierQuotationController = ControllerFactory.CreateSupplierQuotationController();
            BidHistoryController bidHistoryController = ControllerFactory.CreateBidHistoryController();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

            try
            {
                SupplierQuotation SupplierQuotationobj = new SupplierQuotation();
                //SupplierQuotationobj.fetchEditBidBom = new List<SupplierBOM>();

                SupplierQuotationobj = (SupplierQuotation)ObjConverter.ObjectCoverter(supplierBidsObj, SupplierQuotationobj);

                foreach (var item in supplierBidsObj._SupplierBidBomsSVC)
                {
                    SupplierBOM supplierBomObj = new SupplierBOM();
                    //SupplierQuotationobj.fetchEditBidBom.Add((SupplierBOM)ObjConverter.ObjectCoverter(item, supplierBomObj));
                }

                int QuotationId = 0;
                int existingBidsQuotationNumber = 0;// biddingController.GetBiddingDetailsExisting(supplierBidsObj.PrID, supplierBidsObj.ItemId, supplierBidsObj.SupplierId).QuotationNo;
                if (existingBidsQuotationNumber == 0)
                {
                    QuotationId = supplierQuotationController.SaveQuatation(supplierBidsObj.ItemId, supplierBidsObj.PrID, supplierBidsObj.SupplierId, supplierBidsObj.PerItemPrice, 0, "", supplierBidsObj.VatAmount, supplierBidsObj.NbtAmount, supplierBidsObj.SubTotal, 0, "", 0, supplierBidsObj.BidTermsAndConditions, supplierBidsObj.BidOpeningId,supplierBidsObj.isVatInclude);
                    if (QuotationId > 0)
                    {
                        int saveBidHistory = bidHistoryController.SaveBidHistory(QuotationId, "S", supplierBidsObj.SupplierId, supplierBidsObj.PerItemPrice, supplierBidsObj.VatAmount, supplierBidsObj.NbtAmount, supplierBidsObj.SubTotal, LocalTime.Now);
                    }
                }
                else
                {
                    int updateQuotationId = supplierQuotationController.UpdatePendingBids(existingBidsQuotationNumber, supplierBidsObj.PerItemPrice, supplierBidsObj.VatAmount, supplierBidsObj.NbtAmount, supplierBidsObj.SubTotal, supplierBidsObj.BidTermsAndConditions, supplierBidsObj.isVatInclude);
                    if (updateQuotationId > 0)
                    {
                        int saveBidHistory1 = bidHistoryController.SaveBidHistory(updateQuotationId, "S", supplierBidsObj.SupplierId, supplierBidsObj.PerItemPrice, supplierBidsObj.VatAmount, supplierBidsObj.NbtAmount, supplierBidsObj.SubTotal, LocalTime.Now);
                    }
                }


                if (QuotationId > 0 || existingBidsQuotationNumber > 0)
                {

                    foreach (var item in supplierBidsObj._SupplierBidBomsSVC)
                    {
                        int prid = item.PrId;
                        int itemid = item.ItemId;
                        int seq = item.SeqId;
                        string meterial = item.Meterial;
                        string description = item.Description;
                        int comply = item.Comply;
                        string Remarks = item.Remarks;

                        int saveSublierBomStatus = supplierBOMController.SaveSupplierBOM(supplierBidsObj.SupplierId, prid, itemid, seq, meterial, description, 1, LocalTime.Now, comply, Remarks);
                    }

                    if (QuotationId > 0)
                    {
                        response.ID = 200;
                        response.Data = javaScriptSerializer.Serialize("Bids has been Submitted Suucessfully");
                    }
                    if (existingBidsQuotationNumber > 0)
                    {
                        response.ID = 200;
                        response.Data = javaScriptSerializer.Serialize("Bids has been Updated Suucessfully");
                    }
                }
                else
                {
                    response.ID = 300;
                    response.Data = "Error occur in Bid Submission";
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message;
            }
            return response;
        }

        public Response UpdateSupplierDetailsTemp(SupplierDetailsSVC supplierDetailsObj)
        {
            Response response = new Response();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            SupplierCategoryController supplierCategoryController = ControllerFactory.CreatesupplierCategoryController();
            SupplierController supplierController = ControllerFactory.CreateSupplierController();
            try
            {
                int updatesupplier = supplierController.updateSupplierTemperory(supplierDetailsObj.SupplierId, supplierDetailsObj.SupplierName, supplierDetailsObj.Address1, supplierDetailsObj.Address2, supplierDetailsObj.OfficeContactNo, supplierDetailsObj.PhoneNo, supplierDetailsObj.BusinessRegistrationNumber, supplierDetailsObj.VatRegistrationNumber, supplierDetailsObj.CompanyType, supplierDetailsObj.BusinessCatecory);
                if (updatesupplier > 0)
                {

                    int deleteSupplierCategory = supplierCategoryController.deleteSupplierCategoryBySupplierid(supplierDetailsObj.SupplierId);
                    if (supplierDetailsObj._SupplierCategory != null)
                    {
                        foreach (var item in supplierDetailsObj._SupplierCategory)
                        {
                            supplierCategoryController.saveSupplierCategory(supplierDetailsObj.SupplierId, item.CategoryId, 1);
                        }
                    }
                    response.Data = javaScriptSerializer.Serialize("Supplier has been Updated Successfully");
                    response.ID = 200;
                }
                else
                {
                    response.Data = javaScriptSerializer.Serialize("Error Occur in  Updated Supplier");
                    response.ID = 300;
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message;
            }
           
            return response;
        }

        public Response GetCompanies()
        {
            Response response = new Response();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
            List<CompanyDepartment> fetchDepartmentList = new List<CompanyDepartment>();
            try
            {
                fetchDepartmentList = companyDepartmentController.GetDepartmentList().Where(x => x.IsActive == 1).ToList();
                if (fetchDepartmentList != null)
                {
                    response.Data = javaScriptSerializer.Serialize(fetchDepartmentList);
                    response.ID = 200;
                }
                else
                {
                    response.Data = "Error Occured";
                    response.ID = 300;
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message;
            }
           
            return response;
        }

        public Response FetchAssignedCompaniesWithSupplier(string supplierId)
        {
              Response response = new Response();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
            List<SupplierAssignedToCompany> fetchCompanyListAssignWithSupplier = new List<SupplierAssignedToCompany>();

            try
            {
                fetchCompanyListAssignWithSupplier = supplierAssigneToCompanyController.GetCompanyListBySupplierIdforRequest(int.Parse(supplierId));
                if (fetchCompanyListAssignWithSupplier != null)
                {
                    response.Data = javaScriptSerializer.Serialize(fetchCompanyListAssignWithSupplier);
                    response.ID = 200;
                }
                else
                {
                    response.Data = "Error Occured";
                    response.ID = 300;
                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message;
            }
           
            return response;
        }

        public Response SendRequestToCompany(SupplierAssignCompanySVC supplierAssignCompanyObj)
        {
            Response response = new Response();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
            List<RequestcompanyList> requestcompanyList = new List<RequestcompanyList>();

            try
            {
                requestcompanyList = supplierAssignCompanyObj._RequestcompanyList;
                foreach (var item in requestcompanyList)
                {
                    int requestCompanyStatus = supplierAssigneToCompanyController.saveAssigneSupplierWithCompanyByCompany(item.supplierId, item.companyId, LocalTime.Now, 0, item.isFollow, 1);
                }
                response.Data = "Requests has been sent successfully";
                response.ID = 200;

            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message;
            }

            return response; 
        }

        public Response SupplierFollwingCompanyStatus(string supplierId, string companyId, string isFollow)
        {
            Response response = new Response();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();

            try
            {
                int updateStatus = supplierAssigneToCompanyController.updateUnfollowSupplier(int.Parse(supplierId), int.Parse(companyId),int.Parse(isFollow));
                if(updateStatus>0)
                {
                    if(isFollow =="1")
                    {
                        response.Data = "Status-Follow";
                        response.ID = 200;
                    }
                    if(isFollow=="0")
                    {
                        response.Data = "Status-Unfollow";
                        response.ID = 200;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message;
            }
            return response;
        }

        public Response GetCompaniesAssignWithSupplierId(string supplierId)
        {
            Response response = new Response();
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            CompanyDepartmentController companyDepartmentController = ControllerFactory.CreateCompanyDepartmentController();
            List<CompanyDepartment> fetchDepartmentList = new List<CompanyDepartment>();
            SupplierAssigneToCompanyController supplierAssigneToCompanyController = ControllerFactory.CreateSupplierAssigneToCompanyController();
            List<SupplierAssignedToCompany> fetchCompanyListAssignWithSupplier = new List<SupplierAssignedToCompany>();
            try
            {
                fetchDepartmentList = companyDepartmentController.GetDepartmentList().Where(x => x.IsActive == 1).ToList();

                fetchCompanyListAssignWithSupplier = supplierAssigneToCompanyController.GetCompanyListBySupplierIdforRequest(int.Parse(supplierId));

                foreach (var item in fetchDepartmentList)
                {
                    foreach (var item1 in fetchCompanyListAssignWithSupplier)
                    {
                        if (item.DepartmentID == item1.CompanyId)
                        {
                            item.isApproved = item1.IsApproved;
                            item.isSupplierFollow = item1.SupplierFollowing;
                            item.isTermsAgreed = item1.IsAgreedTerms;
                        }
                    }
                }


                if (fetchDepartmentList != null)
                {
                    response.Data = javaScriptSerializer.Serialize(fetchDepartmentList);
                    response.ID = 200;
                }
                else
                {
                    response.Data = "Error Occured";
                    response.ID = 300;
                }


            }

            catch (Exception ex)
            {
                response.ID = 500;
                response.Data = ex.Message;
            }

            return response;

        }
    }
}
