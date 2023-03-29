using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.IO;

namespace BiddingService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBiddingService" in both code and config file together.
    [ServiceContract]
    public interface IBiddingService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetAllBiddingData?supplierId={supplierId}")]
        Response GetAllBiddingData(string supplierId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetAllLatestBiddingData?supplierId={supplierId}")]
        Response GetAllLatestBiddingData(string supplierId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SupplierLogin?username={username}&password={password}")]
        Response SupplierLogin(string username, string password);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetAllCategories")]
        Response GetAllCategories();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetSubCategoriesByCategoryId?categoryId={categoryId}")]
        Response GetSubCategoriesByCategoryId(string categoryId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetItemsByCategoryAndMainCategory?categoryId={categoryId}&subcategoryId={subcategoryId}&companyId={companyId}")]
        Response GetItemsByCategoryAndMainCategory(string categoryId, string subcategoryId, string companyId);

        //--Get PR Detials ----Bid Details -----Bid Submission Pr Details----------------------
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetPRItemDataEachBid?prId={prId}&itemId={itemId}&biddingorderId={biddingorderId}")]
        Response GetPRItemDataEachBid(string prId, string itemId, string biddingorderId);

        //--Fetch Images 
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetPRItemImagesEachBid?prId={prId}&itemId={itemId}")]
        Response GetPRItemImagesEachBid(string prId, string itemId);

        //--PR-BOM Details
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetPRBOMDetailsEachBid?prId={prId}&itemId={itemId}")]
        Response GetPRBOMDetailsEachBid(string prId, string itemId);

        //--Btn Later Click
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SavePendingBidStatus?prId={prId}&itemId={itemId}&supplierId={supplierId}&biddingorderId={biddingorderId}&isVatInclude={isVatInclude}")]
        Response SavePendingBidStatus(string prId, string itemId, string supplierId, string biddingorderId,string isVatInclude);

        //---Btn-Apply Now Click
        //---Supplier Attachments Save Mode
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetBiddingAlredyExistingForLoggedSupplier?prId={prId}&itemId={itemId}&supplierId={supplierId}")]
        Response GetBiddingAlredyExistingForLoggedSupplier(string prId, string itemId, string supplierId);

        //--------Get Supplier Pending Bids
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetSuppliersPendingBids?supplierId={supplierId}")]
        Response GetSuppliersPendingBids(string supplierId);

        //--------Get Supplier Received PO
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetSupplierReceivedPOs?supplierId={supplierId}")]
        Response GetSupplierReceivedPOs(string supplierId);

        //------Get Supplier Received PO Details
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetSupplierReceivedPODetailed?poId={poId}")]
        Response GetSupplierReceivedPODetailed(string poId);

        //------Save Bidded
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveSupplierBidForItem?itemId={itemId}&prId={prId}&supplierId={supplierId}&itemPrice={itemPrice}&vatAmount={vatAmount}&nbtAmount={nbtAmount}&totatlAmount={totatlAmount}&SupplierTermsConditions={SupplierTermsConditions}&BidOrderingNo={BidOrderingNo}&isVatInclude={isVatInclude}")]
        Response SaveSupplierBidForItem(string itemId, string prId, string supplierId, string itemPrice, string vatAmount, string nbtAmount, string totatlAmount, string SupplierTermsConditions, string BidOrderingNo, string isVatInclude);

        //------Update Bidded. --existingQuotationId =  GetBiddingAlredyExistingForLoggedSupplier
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdateSupplierBidForItem?existingQuotationId={existingQuotationId}&perItemPrice={perItemPrice}&VatAmount={VatAmount}&NbtAmount={NbtAmount}&TotalAmount={TotalAmount}&TermsandConditions={TermsandConditions}&isVatInclude={isVatInclude}")]
        Response UpdateSupplierBidForItem(string existingQuotationId, string perItemPrice, string VatAmount, string NbtAmount, string TotalAmount, string TermsandConditions, string isVatInclude);

        //------Supplier Bid Status ---BidHistory Table
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveSupplierBiddingStatus?quotationNo={quotationNo}&bidderId={bidderId}&unitPrice={unitPrice}&vatAmount={vatAmount}&nbtAmount={nbtAmount}&totalAmount={totalAmount}&bidSubmittedDate={bidSubmittedDate}")]
        Response SaveSupplierBiddingStatus(string quotationNo, string bidderId, string unitPrice, string vatAmount, string nbtAmount, string totalAmount, string bidSubmittedDate);

        //-------2018-09-28
        //-------Supplier Register -- Login Page
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SupplierRegistraion?username={username}&password={password}&email={email}&supplierName={supplierName}&officeContactno={officeContactno}")]
        Response SupplierRegistraion(string username, string password, string email, string supplierName, string officeContactno);

        //-------Load Supplier Already Exist Data
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetExistingDataSupplierRegistration?supplierId={supplierId}")]
        Response GetExistingDataSupplierRegistration(string supplierId);
        

        //-------Load Nature of Business
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "LoadNatureOfBusiness")]
        Response LoadNatureOfBusiness();

        //-------Load Main Categories
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "LoadMainCategoriesSupplierPortal")]
        Response LoadMainCategoriesSupplierPortal();

        //-------Update Supplier Uploaded Files
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SupplierLoadUploadedFile?supplierId={supplierId}")]
        Response SupplierLoadUploadedFile(string supplierId);
        
        //------Update Supplier Data
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "UpdateSupplierDetails?supplierId={supplierId}&supplierName={supplierName}&address1={address1}&address2={address2}&officeContactno={officeContactno}&mobileno={mobileno}&businssRegNo={businssRegNo}&vatregNo={vatregNo}&companytypeId={companytypeId}&businessCategory={businessCategory}&logoPath={logoPath}&IsrequestFromSupplier={IsrequestFromSupplier}&IdCreatedBAmin={IdCreatedBAmin}&IsApproved={IsApproved}&IsActive={IsActive}")]
        Response UpdateSupplierDetails(string supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, string companytypeId, string businessCategory, string logoPath, string IsrequestFromSupplier, string IdCreatedBAmin, string IsApproved, string IsActive);

        //------Post Supplier Image
        //[OperationContract]
        //[WebInvoke]
        //Response PostImage(Stream stream, string supplierId);

        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "PostImage?stream={stream}&supplierId={supplierId}")]
        //Response PostImage(Stream stream, string supplierId);

        //------Send Requests for Companies

        //------Request Companies

        //-----Load Company Requests
        //[OperationContract]
        //[WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        //    UriTemplate = "LoadComapanyRequest?supplierId={supplierId}")]
        //Response LoadComapanyRequest(string supplierId);

        //-----Get Bid Status Of supplier 
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetBiddingStatusOfSupplier?prId={prId}&itemId={itemId}&supplierId={supplierId}")]
        Response GetBiddingStatusOfSupplier(string prId, string itemId, string supplierId);

        //----Save Supplier Tocken
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "SaveSupplierTocken?supplierId={supplierId}&tockenId={tockenId}")]
        Response SaveSupplierTocken(string supplierId, string tockenId);


        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
          UriTemplate = "SupplierEditBids?prId={prId}&itemId={itemId}&supplierId={supplierId}")]
        Response SupplierEditBids(string prId, string itemId,string supplierId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "SaveSupplierBOM?prId={prId}&itemId={itemId}&supplierId={supplierId}&SeqNo={SeqNo}&Meterial={Meterial}&Description={Description}&Comply={Comply}&Remarks={Remarks}")]
        Response SaveSupplierBOM(string prId, string itemId, string supplierId, string SeqNo, string Meterial, string Description,  string Comply, string Remarks);


        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SubmitSupplierBid")]
        Response SubmitSupplierBid(SupplierBids supplierBids);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "UpdateSupplierDetails")]
        Response UpdateSupplierDetailsTemp(SupplierDetailsSVC supplierDetailsObj);

        







        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SaveSupplierLogo")]
        Response SaveSupplierLogo(SaveSupplierLogo saveSupplierLogo);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SaveSupplierDocuments")]
        Response SaveSupplierDocuments(SupplierDocuments supplierDocuments);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DeleteSupplierDocument?imageId={imageId}&imageType={imageType}")]
        Response DeleteSupplierDocument(string imageId, string imageType);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SaveSupplierBiddingDocuments")]
        Response SaveSupplierBiddingDocuments(SupplierBiddingDocuments supplierDocuments);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "DeleteSupplierBiddingDocument?fileName={fileName}&supplierId={supplierId}&prId={prId}&itemId={itemId}")]
        Response DeleteSupplierBiddingDocument(string fileName, string supplierId, string prId, string itemId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetReceivedSupplierPO?supplierId={supplierId}")]
        Response GetReceivedSupplierPO(string supplierId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetReceivedSupplierPOItems?poId={poId}")]
        Response GetReceivedSupplierPOItems(string poId);








        
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetCompanies")]
        Response GetCompanies();

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "FetchAssignedCompaniesWithSupplier?supplierId={supplierId}")]
        Response FetchAssignedCompaniesWithSupplier(string supplierId);

        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SendRequestToCompany")]
        Response SendRequestToCompany(SupplierAssignCompanySVC supplierAssignCompanyObj);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
         UriTemplate = "SupplierFollwingCompanyStatus?supplierId={supplierId}&companyId={companyId}&isFollow={isFollow}")]
        Response SupplierFollwingCompanyStatus(string supplierId, string companyId, string isFollow);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "GetCompaniesAssignWithSupplierId?supplierId={supplierId}")]
        Response GetCompaniesAssignWithSupplierId(string supplierId);
    }
}
