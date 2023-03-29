using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructue;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;

namespace CLibrary.Controller
{
    public interface SupplierController
    {
        int saveSupplier(string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string requestedDate, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive);
        int saveSupplierLogo(int supplierId, string logoPath);
        int updateSupplier(int supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive, string emailAddress, int Suppliertype, int IssupplierRegistred, string SupRegNo);
        int updateSupplierByAdmin(int supplierId, string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive);
        List<Supplier> GetSupplierList();
        Supplier GetSupplierBySupplierId(int supplierId);
        bool checkExistingEmailAddress(string emailAddress);
        bool checkExistingSuppliername(string supplierName);
        List<Supplier> GetSupplierListisApproved();

        //----Service SVC save and Create Folder 
        int saveSupplierSVC(int supplieriD, string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string requestedDate, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive);

        Supplier GetSupplierBySupplierIdSVC(int supplierId);

        int UpdateSupplierDeviceTocken(int supplierId, string supplierTocken);


        int updateSupplierTemperory(int supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory);

        List<Supplier> GetApprovedSuppliersForQuotationSubmission(int CompanyId, List<int> CategoryIds);
        List<Supplier> GetAllSuppliersForQuotationSubmission(List<int> CategoryIds);
        List<Supplier> GetAllSuppliersToSendBidEmail(int CategoryId);

        void SaveSupplierEmailForBidSubmission(int PrId, int BidId, int SupplierId, string SupplierName, string EmailAddress);
        List<SupplierBidEmail> GetSupplierAssignedToBid(int PrId, List<int> BidId);
        //void SaveSupplierBidEmailContact(int PrId , DataTable dt);
        List<SupplierBidEmailContact> GetSupplierBidEmailContact(int PrId);
        void SaveSupplierBidEmailContact(int prId, List<SupplierBidEmailContact> supplierBidEmailContact);
        List<CommonReference> getCountry();
        List<SupplierAgent> getSupplierAgent();
        int manageSupplierAgentDetails(SupplierAgent supplierAgent, string action);
        List<SupplierAgent> getSupplierAgentBySupplierId(int supplierId);
        void SaveUnRegisteredSupplier(int prId, List<SupplierBidEmailContact> tempSuppliers);
        List<SupplierBidEmailContact> GetUnRegisteredSuppliersByPrId(int prId);
        void SaveSupplierAgent2(int supplierId, List<int> supplierIds);
        List<SupplierAgent2> getSupplierAgent2(int supplierId);
        int manageSaveSupplier(Supplier supplier, SupplierLogin supplierLogin, SupplierAssignedToCompany supplierAssignedToCompany, IEnumerable<ListItem> selectedItemCategory, FileUpload fileUploadLogo, 
        HttpFileCollection uploadedFile, SupplierRatings supplierRating, List<int> supplierIds, out string errormsg);
        int deleteSupplier(int supplierId);
        void UpdateSupplierAgent2(int supplierId, List<int> supplierIds);
        List<Supplier> FetchClearingAgentList();
        List<Supplier> FetchSupplierAgent();
        Supplier getSupplierByPOId(int POId);
    }
    public class SupplierControllerImpl : SupplierController
    {
        public bool checkExistingEmailAddress(string emailAddress)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.checkExistingEmailAddress(emailAddress, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public bool checkExistingSuppliername(string supplierName)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.checkExistingSuppliername(supplierName, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public Supplier GetSupplierBySupplierId(int supplierId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                Supplier SupplierObj = new Supplier();
                SupplierLoginDAO supplierLoginDAO = DAOFactory.createSupplierLoginDAO();
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();


                SupplierObj = supplierDAO.GetSupplierBySupplierId(supplierId, dbConnection);
                SupplierObj._SupplierLogin = supplierLoginDAO.GetSupplierLoginBySupplierId(SupplierObj.SupplierId, dbConnection);
                SupplierObj._SuplierImageUploadList = suplierImageUploadDAO.GetSupplierImagesBySupplierId(SupplierObj.SupplierId, dbConnection);
                SupplierObj._SupplierCategory = supplierCategoryDAO.GetSupplierCategoryBySupplierId(SupplierObj.SupplierId, dbConnection);
                return SupplierObj;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }


        public List<Supplier> GetSupplierList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.GetSupplierList(dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int saveSupplier(string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string requestedDate, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.saveSupplier(supplierName, address1, address2, email, officeContactno, mobileno, requestedDate, businssRegNo, vatregNo, companytypeId, businessCategory, logoPath, IsrequestFromSupplier, IdCreatedBAmin, IsApproved, IsActive,0,0,"", dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int saveSupplierLogo(int supplierId, string logoPath)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.saveSupplierLogo(supplierId, logoPath, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int updateSupplier(int supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive, string emailAddress, int Suppliertype, int IssupplierRegistred, string SupRegNo)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.updateSupplier(supplierId, supplierName, address1, address2, officeContactno, mobileno, businssRegNo, vatregNo, companytypeId, businessCategory, logoPath, IsrequestFromSupplier, IdCreatedBAmin, IsApproved, IsActive, emailAddress, Suppliertype, IssupplierRegistred, SupRegNo, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int updateSupplierByAdmin(int supplierId, string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.updateSupplierByAdmin(supplierId, supplierName, address1, address2, email, officeContactno, mobileno, businssRegNo, vatregNo, companytypeId, businessCategory, logoPath, IsrequestFromSupplier, IdCreatedBAmin, IsApproved, IsActive, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<Supplier> GetSupplierListisApproved()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.GetSupplierListisApproved(dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        //---------------20198-10-01 Changes to services
        public int saveSupplierSVC(int supplieriD, string supplierName, string address1, string address2, string email, string officeContactno, string mobileno, string requestedDate, string businssRegNo, string vatregNo, int companytypeId, int businessCategory, string logoPath, int IsrequestFromSupplier, int IdCreatedBAmin, int IsApproved, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                //const string BaseDirectory = @"C:\Users\Yashoman\Desktop\Bid Changes 2018-06-05 Onwards\BiddingSystem\SupplierBiddingFileUpload\";
                string rootFolder = ConfigurationSettings.AppSettings["rootFolder"].ToString();

                string BaseDirectory = @"\SupplierBiddingFileUpload\";
                BaseDirectory = rootFolder + BaseDirectory;

                string DirectoryName = supplieriD.ToString();

                Directory.CreateDirectory(BaseDirectory + DirectoryName);

                string path = BaseDirectory + supplieriD;
                if (!Directory.Exists(DirectoryName))
                {
                    Directory.CreateDirectory(path);
                }

                return supplierDAO.saveSupplier(supplierName, address1, address2, email, officeContactno, mobileno, requestedDate, businssRegNo, vatregNo, companytypeId, businessCategory, logoPath, IsrequestFromSupplier, IdCreatedBAmin, IsApproved, IsActive,0,0, "", dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public Supplier GetSupplierBySupplierIdSVC(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.GetSupplierBySupplierIdSVC(supplierId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public Supplier getSupplierByPOId(int POId) {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.getSupplierByPOId(POId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int UpdateSupplierDeviceTocken(int supplierId, string supplierTocken)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.UpdateSupplierDeviceTocken(supplierId, supplierTocken, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int updateSupplierTemperory(int supplierId, string supplierName, string address1, string address2, string officeContactno, string mobileno, string businssRegNo, string vatregNo, int companytypeId, int businessCategory)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.updateSupplierTemperory(supplierId, supplierName, address1, address2, officeContactno, mobileno, businssRegNo, vatregNo, companytypeId, businessCategory, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }


        public List<Supplier> GetApprovedSuppliersForQuotationSubmission(int CompanyId, List<int> CategoryIds)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.GetApprovedSuppliersForQuotationSubmission(CompanyId, CategoryIds, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<Supplier> GetAllSuppliersForQuotationSubmission(List<int> CategoryIds)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.GetAllSuppliersForQuotationSubmission(CategoryIds, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<Supplier> GetAllSuppliersToSendBidEmail(int CategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.GetAllSuppliersToSendBidEmail(CategoryId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public void SaveSupplierEmailForBidSubmission(int PrId, int BidId, int SupplierId, string SupplierName, string EmailAddress)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                supplierDAO.SaveSupplierEmailForBidSubmission(PrId, BidId, SupplierId, SupplierName, EmailAddress, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<SupplierBidEmail> GetSupplierAssignedToBid(int PrId, List<int> BidId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                List<SupplierBidEmail> ListSupplierBidEmail = new List<SupplierBidEmail>();
                for (int x = 0; x < BidId.Count; ++x)
                {
                    ListSupplierBidEmail.AddRange(supplierDAO.GetSupplierAssignedToBid(PrId, BidId[x], dbConnection));
                }
                //   ListSupplierBidEmail = ListSupplierBidEmail.DistinctBy(note => note.Author);
                return ListSupplierBidEmail;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public void SaveSupplierBidEmailContact(int prId, List<SupplierBidEmailContact> supplierBidEmailContact)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                supplierDAO.DeleteSupplierBidEmailContact(prId, dbConnection);
                for (int i = 0; i < supplierBidEmailContact.Count; i++)
                {
                    supplierDAO.SaveSupplierBidEmailContact(prId, supplierBidEmailContact[i].ContactOfficer, supplierBidEmailContact[i].ContactNo, supplierBidEmailContact[i].Title, dbConnection);
                }
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<SupplierBidEmailContact> GetSupplierBidEmailContact(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                List<SupplierBidEmailContact> ListSupplierBidEmailContact = new List<SupplierBidEmailContact>();
                ListSupplierBidEmailContact = supplierDAO.GetSupplierBidEmailContact(PrId, dbConnection);
                return ListSupplierBidEmailContact;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<CommonReference> getCountry()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.getCountry(dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        List<SupplierAgent> SupplierController.getSupplierAgent()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.getSupplierAgent(dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int manageSupplierAgentDetails(SupplierAgent supplierAgent, string action)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO SupplierDAO = DAOFactory.createSupplierDAO();
                if (action == "Insert")
                {
                    return SupplierDAO.insertSupplierAgentDetails(supplierAgent.SupplierId, supplierAgent.AgentName, supplierAgent.Address, supplierAgent.Email, supplierAgent.ContactNo, dbConnection);
                }
                else if (action == "Update")
                {
                    return SupplierDAO.updateSupplierAgentDetails(supplierAgent.AgentId, supplierAgent.AgentName, supplierAgent.Address, supplierAgent.Email, supplierAgent.ContactNo, dbConnection);
                }
                else
                {
                    return SupplierDAO.deleteSupplierAgentDetails(supplierAgent.AgentId, supplierAgent.AgentName, dbConnection);
                }
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<SupplierAgent> getSupplierAgentBySupplierId(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.getSupplierAgentBySupplierId(supplierId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public void SaveUnRegisteredSupplier(int prId, List<SupplierBidEmailContact> tempSuppliers)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                foreach (var item in tempSuppliers)
                {
                    supplierDAO.SaveUnRegisteredSupplier(prId, item, dbConnection);
                }
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<SupplierBidEmailContact> GetUnRegisteredSuppliersByPrId(int prId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                List<SupplierBidEmailContact> ListUnRegisteredSuppliers = new List<SupplierBidEmailContact>();
                ListUnRegisteredSuppliers = supplierDAO.GetUnRegisteredSuppliersByPrId(prId, dbConnection);
                return ListUnRegisteredSuppliers;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public void SaveSupplierAgent2(int supplierId, List<int> supplierIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                supplierDAO.SaveSupplierAgent2(supplierId, supplierIds, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<SupplierAgent2> getSupplierAgent2(int supplierId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.getSupplierAgent2(supplierId, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int  manageSaveSupplier(Supplier supplier, SupplierLogin supplierLogin, SupplierAssignedToCompany supplierAssignedToCompany,
        IEnumerable<ListItem> selectedItemCategory, FileUpload fileUploadLogo, HttpFileCollection uploadedFile, SupplierRatings supplierRating, 
        List<int> supplierIds, out string errormsg)
        {
            DBConnection dbConnection = null;
            errormsg = string.Empty;
            try {
                dbConnection = new DBConnection();
                SupplierLoginDAO SupplierLoginDAO = DAOFactory.createSupplierLoginDAO();
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                SupplierAssigneToCompanyDAO supplierAssigneToCompanyDAO = DAOFactory.createSupplierAssigneToCompanyDAO();
                SupplierCategoryDAO supplierCategoryDAO = DAOFactory.createSupplierCategoryDAO();
                SuplierImageUploadDAO suplierImageUploadDAO = DAOFactory.createSuplierImageUploadDAO();
                SupplierRatingDAO supplierRatingDAO = DAOFactory.createSupplierRatingDAO();

                // save Supplier details into table "SUPPLIER" & return supplierId  
                int supplierId = supplierDAO.saveSupplier(supplier.SupplierName, supplier.Address1, supplier.Address2,
                    supplier.Email, supplier.OfficeContactNo, supplier.PhoneNo, supplier.RequestedDate,
                    supplier.BusinessRegistrationNumber, supplier.VatRegistrationNumber, supplier.CompanyType,
                    supplier.BusinessCatecory, "", supplier.IsRequestFromSupplier, supplier.IsCreatedByAdmin, 1, supplier.IsActive, supplier.SupplierType,supplier.IsRegisteredSupplier, supplier.SupplierRegistration,  dbConnection);
                if (supplierId > 0) {
                    // save Supplier Login into table "Company_Login"
                    SupplierLoginDAO.saveSupplierLogin(supplierId, supplierLogin.Username, supplierLogin.Password, supplierLogin.Email, 1, 1, dbConnection);

                    //Save short details to table "AssigneSupplierWithCompany"
                    supplierAssigneToCompanyDAO.saveAssigneSupplierWithCompanyByCompany(supplierId, supplierAssignedToCompany.CompanyId, LocalTime.Now, 1, 1, 1, dbConnection);

                    // Save Main item Catergory into "SUPPLIER_CATEGORY"
                    foreach (ListItem item in selectedItemCategory) {
                        supplierCategoryDAO.saveSupplierCategory(supplierId, int.Parse(item.Value), 1, dbConnection);
                    }

                    // Update & Save Supplier Logo into table "SUPPLIER"
                    if (fileUploadLogo.PostedFile != null && fileUploadLogo.PostedFile.FileName != "") {
                        if (fileUploadLogo.PostedFile != null && fileUploadLogo.PostedFile.FileName != "") {
                            string nameOfUploadedFile = supplierId + "_1";
                            string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                            string FileName = Path.GetFileName(fileUploadLogo.PostedFile.FileName);
                            string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Supplier/Logo/" + filename1))) {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Supplier/Logo/" + filename1));
                            }
                            fileUploadLogo.SaveAs(HttpContext.Current.Server.MapPath("~/Supplier/Logo/" + UploadedFileName + '.' + FileName.Split('.').Last()));
                            string supplierLogoPath = "~/Supplier/Logo/" + filename1;
                            supplierDAO.saveSupplierLogo(supplierId, supplierLogoPath, dbConnection);
                        }
                    }

                    //Save Supplier files into table "SUPPLIER_IMAGE_UPLOAD"
                    if (uploadedFile != null) {
                        if (uploadedFile.Count <= 10)    // 10 FILES RESTRICTION.
                        {
                            for (int i = 1; i <= uploadedFile.Count - 1; i++) {
                                HttpPostedFile hpf = uploadedFile[i];
                                string nameOfUploadedFile = supplierId + "_" + i.ToString();
                                string UploadedFileName = nameOfUploadedFile.Replace(" ", String.Empty);
                                string FileName = Path.GetFileName(hpf.FileName);
                                string filename1 = UploadedFileName + "." + FileName.Split('.').Last();
                                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + filename1))) {
                                    System.IO.File.Delete(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + filename1));
                                }

                                hpf.SaveAs(HttpContext.Current.Server.MapPath("~/Supplier/Documents/" + UploadedFileName + '.' + FileName.Split('.').Last()));

                                string supplierDocpath = "~/Supplier/Documents/" + filename1;
                                suplierImageUploadDAO.saveUploadedSupplierFile(supplierId, supplierDocpath, FileName, nameOfUploadedFile, 1, dbConnection);
                            }
                        }
                    }

                    // save supplier Rating , simply inserts record into table "SUPPLIER_RATINGS" without rating
                    supplierRatingDAO.SupplierRating(supplierId, supplierRating.Companyid, 0, 0, 1, supplierRating.CreatedDate, supplierRating.CreatedBy, supplierRating.UpdatedDate, supplierRating.UpdatedBy, "", dbConnection);

                    // save supplier as agent of another supplier into table "SUPPLIER_AGENT_2"  (This happens if agent also might be  a supplier)
                    supplierDAO.SaveSupplierAgent2(supplierId, supplierIds, dbConnection);
                    return 1;
                }

                else {
                    return -1;
                }
            }
            catch (Exception ex) {
                errormsg = ex.Message;
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public int deleteSupplier(int supplierId)
        {
            DBConnection dbConnection = null;
            try
            {
                dbConnection = new DBConnection();
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                supplierDAO.deleteSupplier(supplierId, dbConnection);
                return 1;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public void UpdateSupplierAgent2(int supplierId, List<int> supplierIds)
        {
            DBConnection dbConnection = null;
            try
            {
                dbConnection = new DBConnection();
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                supplierDAO.UpdateSupplierAgent2(supplierId, supplierIds, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<Supplier> FetchClearingAgentList() {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.FetchClearingAgentList(dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }

        public List<Supplier> FetchSupplierAgent() {
            DBConnection dbConnection = new DBConnection();
            try {
                SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
                return supplierDAO.FetchSupplierAgent(dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    }
}
