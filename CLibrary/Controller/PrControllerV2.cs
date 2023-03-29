using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace CLibrary.Controller {
    public interface PrControllerV2 {
        int SavePr(PrMasterV2 pr);
        int UpdatePr(PrMasterV2 pr);
        PrMasterV2 GetPrForEditing(int PrId);
        int ClonePR(int prId, int clonedBy);
        List<PrMasterV2> FetchMyPrByBasicSearchByMonth(int createdBy, DateTime date);
        PrMasterV2 FetchMyPrByBasicSearchByPrCode(int createdBy, string prCode);
        List<PrMasterV2> FetchMyPrByAdvanceSearch(int createdBy, List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int prType, int mainCatergoryId, int subCatergoryId);
        List<PrDetailsV2> FetchPrDetailsList(int prId, int companyId);
        List<PrUpdateLog> FetchPrUpdateLog(int prId);
        int TerminatePR(int prID,int mrnId, int userId, string remarks);
        PrDetailsV2 GetPrdTerminationDetails(int prdId);
        PrMasterV2 GetPrMasterToView(int prId, int companyId);
        List<PrMasterV2> FetchPrListforApproval(List<int> warehouseIds, int purchaseType);
        int ApproveOrRejectPr(int expenseType, int prId, int isApproved, int isExpenseApproved, int userId, string remark);
        List<PrMasterV2> FetchPrListForAvailabiltyExpenseApproval();
        PrMasterV2 FetchPrWithPrDetails(int prId, int companyId, int warehouseId);
        int UpdatePrItemWarehouseStock(int prId, int prdId, decimal warhouseStock, int itemId, int warehouseId, int UserId, int companyId, int fromUnit, int toUnit, decimal up, DateTime expDate);
        int ApproveOrRejectPrExpense(int prId, int isApproved, string remark, int userId, DateTime approvedDate);
        int UpdatePrExpense(int prId, int expenseType, int isBudget, decimal budgetAmount, string budgetRemark, string budgetInformation, int userId, string userName, DateTime now);
        List<PrMasterV2> FetchAllPrByBasicSearchByMonth(DateTime date);
        PrMasterV2 FetchAllPrByBasicSearchByPrCode(string prCode);
        List<PrMasterV2> FetchAllPrByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int prType, int mainCatergoryId, int subCatergoryId);
        PrMasterV2 GetPrSubmittedBid(int prId, int companyId);
        PrMasterV2 GetPRs(int PrId, int CompanyId);
        List<PrMasterV2> GetPRListForPrInquiry(int CompanyId);
        int DeleteFromPO(int prId, List<int> itemId, int UserId);
        List<PrMasterV2> FetchPrListforApprovalByDate(List<int> warehouseIds, int purchaseType, DateTime date);
        PrMasterV2 FetchPrListforApprovalByCode(List<int> warehouseIds, int purchaseType, string code);
        List<PrMasterV2> FetchPrListForAvailabiltyExpenseApprovalByPRCode(string PRCode);
        List<PrMasterV2> FetchPrListForAvailabiltyExpenseApprovalByDate(DateTime date);
        PrMasterV2 getPrIdForPRCode(string code);
        List<PrMasterV2> FetchPRByPRCode(string PrCode, int Companyid);
        List<PrMasterV2> GetPrMasterList(int MrnId, int Companyid);
        List<PrDetailsV2> FetchPrDetails(List<int> prId, int companyId);
        List<PrMasterV2> FetchMyPr(int createdBy);
    }

    class PrControllerV2Impl : PrControllerV2 {

        public PrMasterV2 GetPrForEditing(int PrId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                PrDetailsDAOV2 prDetailsDao = DAOFactory.CreatePrDetailsDAOV2();
                PrBomDAOV2 prBomDao = DAOFactory.CreatePrBomDAOV2();
                PrFileUploadDAOV2 prFileUploadDao = DAOFactory.CreatePrFileUploadDAOV2();
                PrReplacementFileUploadDAOV2 prReplacementFileUploadDao = DAOFactory.CreatePrReplacementFileUploadDAOV2();
                PrSupportiveDocumentsDAOV2 prSupportiveDocumentsDao = DAOFactory.CreatePrSupportiveDocumentsDAOV2();
                PrCapexDocDAO prCapexDocDao = DAOFactory.CreatePrCapexDocDAO();

                PrMasterV2 pr = prMasterDao.GetPrForEditing(PrId, dbConnection);

                if (pr != null && pr.PrId != 0) {
                    pr.PrCapexDocs = prCapexDocDao.GetPrCapexDocsForEdit(pr.PrId, dbConnection);
                    pr.PrDetails = prDetailsDao.GetPrDetailsForEdit(pr.PrId, dbConnection);

                    for (int i = 0; i < pr.PrDetails.Count; i++) {
                        pr.PrDetails[i].PrBoms = prBomDao.GetPrdBomForEdit(pr.PrDetails[i].PrdId, dbConnection);
                        pr.PrDetails[i].PrReplacementFileUploads = prReplacementFileUploadDao.GetPrReplacementFileUploadForEdit(pr.PrDetails[i].PrdId, dbConnection);
                        pr.PrDetails[i].PrFileUploads = prFileUploadDao.GetPrFileUploadForEdit(pr.PrDetails[i].PrdId, dbConnection);
                        pr.PrDetails[i].PrSupportiveDocuments = prSupportiveDocumentsDao.GetPrSupportiveDocumentsForEdit(pr.PrDetails[i].PrdId, dbConnection);
                    }

                    return pr;
                }
                else {
                    return null;
                }

            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public PrMasterV2 GetPRs(int PrId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 pr_MasterDAO = DAOFactory.CreatePrMasterDAOV2();
                return pr_MasterDAO.GetPRs(PrId, CompanyId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }

        public PrMasterV2 getPrIdForPRCode(string code) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 pr_MasterDAO = DAOFactory.CreatePrMasterDAOV2();
                return pr_MasterDAO.getPrIdForPRCode(code, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }
        public int SavePr(PrMasterV2 pr) {
            DBConnection dbConnection = new DBConnection();
            try {
                for (int i = 0; i < pr.PrCapexDocs.Count; i++) {
                    string fileName = i + "_" + LocalTime.Now.Ticks + "_" + pr.PrCapexDocs[i].FileName;

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrCapexDocs"))) {

                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrCapexDocs"));
                    }

                    File.WriteAllBytes(HttpContext.Current.Server.MapPath("/PrCapexDocs/" + fileName), Convert.FromBase64String(pr.PrCapexDocs[i].FileData));
                    pr.PrCapexDocs[i].FilePath = "/PrCapexDocs/" + fileName;

                }
                for (int i = 0; i < pr.PrDetails.Count; i++) {
                    for (int j = 0; j < pr.PrDetails[i].PrFileUploads.Count; j++) {

                        string fileName = i + "_" + LocalTime.Now.Ticks + "_" + pr.PrDetails[i].PrFileUploads[j].FileName;

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrFileUploads"))) {

                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrFileUploads"));
                        }

                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("/PrFileUploads/" + fileName), Convert.FromBase64String(pr.PrDetails[i].PrFileUploads[j].FileData));
                        pr.PrDetails[i].PrFileUploads[j].FilePath = "/PrFileUploads/" + fileName;
                    }

                    for (int j = 0; j < pr.PrDetails[i].PrReplacementFileUploads.Count; j++) {

                        string fileName = i + "_" + LocalTime.Now.Ticks + "_" + pr.PrDetails[i].PrReplacementFileUploads[j].FileName;

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrReplacementFileUploads"))) {

                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrReplacementFileUploads"));
                        }

                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("/PrReplacementFileUploads/" + fileName), Convert.FromBase64String(pr.PrDetails[i].PrReplacementFileUploads[j].FileData));
                        pr.PrDetails[i].PrReplacementFileUploads[j].FilePath = "/PrReplacementFileUploads/" + fileName;
                    }

                    for (int j = 0; j < pr.PrDetails[i].PrSupportiveDocuments.Count; j++) {

                        string fileName = i + "_" + LocalTime.Now.Ticks + "_" + pr.PrDetails[i].PrSupportiveDocuments[j].FileName;

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrSupportiveDocs"))) {

                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrSupportiveDocs"));
                        }

                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("/PrSupportiveDocs/" + fileName), Convert.FromBase64String(pr.PrDetails[i].PrSupportiveDocuments[j].FileData));
                        pr.PrDetails[i].PrSupportiveDocuments[j].FilePath = "/PrSupportiveDocs/" + fileName;
                    }
                }

                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                PrDetailsDAOV2 prDetailsDao = DAOFactory.CreatePrDetailsDAOV2();
                PrBomDAOV2 prBomDao = DAOFactory.CreatePrBomDAOV2();
                PrFileUploadDAOV2 prFileUploadDao = DAOFactory.CreatePrFileUploadDAOV2();
                PrReplacementFileUploadDAOV2 prReplacementFileUploadDao = DAOFactory.CreatePrReplacementFileUploadDAOV2();
                PrSupportiveDocumentsDAOV2 prSupportiveDocumentsDao = DAOFactory.CreatePrSupportiveDocumentsDAOV2();
                PrCapexDocDAO prCapexDocDao = DAOFactory.CreatePrCapexDocDAO();

                List<int> prIdCode = prMasterDao.SavePr(pr, dbConnection);

                if (prIdCode.Count > 0) {
                    PRDetailsStatusLogDAO prDetailStatusLogDAO = DAOFactory.CreatePRDetailsStatusLogDAO();
                    pr.PrDetails.ForEach(prd => prd.PrId = prIdCode[0]);
                    pr.PrCapexDocs.ForEach(prd => prd.PrId = prIdCode[0]);

                    int result = 0;
                    for (int i = 0; i < pr.PrCapexDocs.Count; i++) {
                        result = prCapexDocDao.Save(pr.PrCapexDocs[i], dbConnection);
                        if (result <= 0) {
                            dbConnection.RollBack();
                            return -7;
                        }
                    }

                    for (int i = 0; i < pr.PrDetails.Count; i++) {
                        result = prDetailsDao.Save(pr.PrDetails[i], dbConnection);
                        string StatusCode = "CRTD";
                        if (result > 0) {
                            pr.PrDetails[i].PrBoms.ForEach(obj => obj.PrdId = result);
                            pr.PrDetails[i].PrFileUploads.ForEach(obj => obj.PrdId = result);
                            pr.PrDetails[i].PrReplacementFileUploads.ForEach(obj => obj.PrdId = result);
                            pr.PrDetails[i].PrSupportiveDocuments.ForEach(obj => obj.PrdId = result);

                            prDetailStatusLogDAO.InsertLog(result, pr.CreatedBy, StatusCode, dbConnection);

                            for (int j = 0; j < pr.PrDetails[i].PrBoms.Count; j++) {
                                result = prBomDao.Save(pr.PrDetails[i].PrBoms[j], dbConnection);
                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -3;
                                }
                            }

                            for (int j = 0; j < pr.PrDetails[i].PrFileUploads.Count; j++) {
                                result = prFileUploadDao.Save(pr.PrDetails[i].PrFileUploads[j], dbConnection);
                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -4;
                                }
                            }

                            for (int j = 0; j < pr.PrDetails[i].PrReplacementFileUploads.Count; j++) {
                                result = prReplacementFileUploadDao.Save(pr.PrDetails[i].PrReplacementFileUploads[j], dbConnection);
                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -5;
                                }
                            }

                            for (int j = 0; j < pr.PrDetails[i].PrSupportiveDocuments.Count; j++) {
                                result = prSupportiveDocumentsDao.Save(pr.PrDetails[i].PrSupportiveDocuments[j], dbConnection);
                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -6;
                                }
                            }
                        }
                        else {
                            dbConnection.RollBack();
                            return -2;
                        }
                    }

                    return prIdCode[1];
                }
                else {
                    dbConnection.RollBack();
                    return -1;
                }
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public int UpdatePr(PrMasterV2 pr) {
            DBConnection dbConnection = new DBConnection();
            try {
                if (pr.ExpenseType == 2)
                    pr.PrCapexDocs.ForEach(prd => prd.Todo = 3);

                for (int i = 0; i < pr.PrCapexDocs.Count; i++) {
                    if (pr.PrCapexDocs[i].Todo == 1) {
                        string fileName = i + "_" + LocalTime.Now.Ticks + "_" + pr.PrCapexDocs[i].FileName;

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrCapexDocs"))) {

                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrCapexDocs"));
                        }

                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("/PrCapexDocs/" + fileName), Convert.FromBase64String(pr.PrCapexDocs[i].FileData));
                        pr.PrCapexDocs[i].FilePath = "/PrCapexDocs/" + fileName;
                    }
                }

                for (int i = 0; i < pr.PrDetails.Count; i++) {
                    for (int j = 0; j < pr.PrDetails[i].PrFileUploads.Count; j++) {

                        if (pr.PrDetails[i].PrFileUploads[j].Todo == 1) {
                            string fileName = i + "_" + LocalTime.Now.Ticks + "_" + pr.PrDetails[i].PrFileUploads[j].FileName;

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrFileUploads"))) {

                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrFileUploads"));
                            }

                            File.WriteAllBytes(HttpContext.Current.Server.MapPath("/PrFileUploads/" + fileName), Convert.FromBase64String(pr.PrDetails[i].PrFileUploads[j].FileData));
                            pr.PrDetails[i].PrFileUploads[j].FilePath = "/PrFileUploads/" + fileName;
                        }
                    }

                    for (int j = 0; j < pr.PrDetails[i].PrReplacementFileUploads.Count; j++) {
                        if (pr.PrDetails[i].PrReplacementFileUploads[j].Todo == 1) {
                            string fileName = i + "_" + LocalTime.Now.Ticks + "_" + pr.PrDetails[i].PrReplacementFileUploads[j].FileName;

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrReplacementFileUploads"))) {

                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrReplacementFileUploads"));
                            }

                            File.WriteAllBytes(HttpContext.Current.Server.MapPath("/PrReplacementFileUploads/" + fileName), Convert.FromBase64String(pr.PrDetails[i].PrReplacementFileUploads[j].FileData));
                            pr.PrDetails[i].PrReplacementFileUploads[j].FilePath = "/PrReplacementFileUploads/" + fileName;
                        }
                    }

                    for (int j = 0; j < pr.PrDetails[i].PrSupportiveDocuments.Count; j++) {
                        if (pr.PrDetails[i].PrSupportiveDocuments[j].Todo == 1) {
                            string fileName = i + "_" + LocalTime.Now.Ticks + "_" + pr.PrDetails[i].PrSupportiveDocuments[j].FileName;

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrSupportiveDocs"))) {

                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrSupportiveDocs"));
                            }

                            File.WriteAllBytes(HttpContext.Current.Server.MapPath("/PrSupportiveDocs/" + fileName), Convert.FromBase64String(pr.PrDetails[i].PrSupportiveDocuments[j].FileData));
                            pr.PrDetails[i].PrSupportiveDocuments[j].FilePath = "/PrSupportiveDocs/" + fileName;
                        }
                    }
                }

                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                PrDetailsDAOV2 prDetailsDao = DAOFactory.CreatePrDetailsDAOV2();
                PrBomDAOV2 prBomDao = DAOFactory.CreatePrBomDAOV2();
                PrFileUploadDAOV2 prFileUploadDao = DAOFactory.CreatePrFileUploadDAOV2();
                PrReplacementFileUploadDAOV2 prReplacementFileUploadDao = DAOFactory.CreatePrReplacementFileUploadDAOV2();
                PrSupportiveDocumentsDAOV2 prSupportiveDocumentsDao = DAOFactory.CreatePrSupportiveDocumentsDAOV2();
                PrUpdateLogDAO prUpdateLogDAO = DAOFactory.CreatePrUpdateLogDAO();
                PrCapexDocDAO prCapexDocDao = DAOFactory.CreatePrCapexDocDAO();

                int result = prMasterDao.UpdatePr(pr, dbConnection);

                if (result > 0) {
                    pr.PrCapexDocs.ForEach(doc => doc.PrId = pr.PrId);

                    for (int i = 0; i < pr.PrCapexDocs.Count; i++) {
                        if (pr.PrCapexDocs[i].Todo == 1) {
                            result = prCapexDocDao.Save(pr.PrCapexDocs[i], dbConnection);
                        }
                        else if (pr.PrCapexDocs[i].Todo == 3) {
                            result = prCapexDocDao.Delete(pr.PrCapexDocs[i].FileId, dbConnection);
                        }
                        else {
                            result = 1;
                        }

                        if (result <= 0) {
                            dbConnection.RollBack();
                            return -8;
                        }
                    }

                    for (int i = 0; i < pr.PrDetails.Count; i++) {
                        if (pr.PrDetails[i].Todo == 1) {
                            pr.PrDetails[i].PrId = pr.PrId;

                            result = prDetailsDao.Save(pr.PrDetails[i], dbConnection);
                            pr.PrDetails[i].PrdId = result;
                        }
                        else if (pr.PrDetails[i].Todo == 2) {
                            result = prDetailsDao.Update(pr.PrDetails[i], dbConnection);
                        }
                        else if (pr.PrDetails[i].Todo == 3) {
                            result = prDetailsDao.Delete(pr.PrDetails[i].PrdId, dbConnection);

                            pr.PrDetails[i].PrFileUploads.ForEach(obj => obj.Todo = 3);
                            pr.PrDetails[i].PrReplacementFileUploads.ForEach(obj => obj.Todo = 3);
                            pr.PrDetails[i].PrSupportiveDocuments.ForEach(obj => obj.Todo = 3);

                            continue;
                        }
                        else {
                            result = 1;
                        }

                        if (result > 0) {

                            pr.PrDetails[i].PrBoms.ForEach(obj => obj.PrdId = pr.PrDetails[i].PrdId);
                            pr.PrDetails[i].PrFileUploads.ForEach(obj => obj.PrdId = pr.PrDetails[i].PrdId);
                            pr.PrDetails[i].PrReplacementFileUploads.ForEach(obj => obj.PrdId = pr.PrDetails[i].PrdId);
                            pr.PrDetails[i].PrSupportiveDocuments.ForEach(obj => obj.PrdId = pr.PrDetails[i].PrdId);

                            for (int j = 0; j < pr.PrDetails[i].PrBoms.Count; j++) {
                                if (pr.PrDetails[i].PrBoms[j].Todo == 1) {
                                    result = prBomDao.Save(pr.PrDetails[i].PrBoms[j], dbConnection);
                                }
                                else if (pr.PrDetails[i].PrBoms[j].Todo == 3) {
                                    result = prBomDao.Delete(pr.PrDetails[i].PrBoms[j].BomId, dbConnection);
                                }
                                else {
                                    result = 1;
                                }

                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -3;
                                }
                            }

                            for (int j = 0; j < pr.PrDetails[i].PrFileUploads.Count; j++) {
                                if (pr.PrDetails[i].PrFileUploads[j].Todo == 1) {
                                    result = prFileUploadDao.Save(pr.PrDetails[i].PrFileUploads[j], dbConnection);
                                }
                                else if (pr.PrDetails[i].PrFileUploads[j].Todo == 3) {
                                    result = prFileUploadDao.Delete(pr.PrDetails[i].PrFileUploads[j].FileId, dbConnection);
                                }
                                else {
                                    result = 1;
                                }

                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -4;
                                }
                            }

                            for (int j = 0; j < pr.PrDetails[i].PrReplacementFileUploads.Count; j++) {
                                if (pr.PrDetails[i].PrReplacementFileUploads[j].Todo == 1) {
                                    result = prReplacementFileUploadDao.Save(pr.PrDetails[i].PrReplacementFileUploads[j], dbConnection);
                                }
                                else if (pr.PrDetails[i].PrReplacementFileUploads[j].Todo == 3) {
                                    result = prReplacementFileUploadDao.Delete(pr.PrDetails[i].PrReplacementFileUploads[j].FileId, dbConnection);
                                }
                                else {
                                    result = 1;
                                }

                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -5;
                                }
                            }

                            for (int j = 0; j < pr.PrDetails[i].PrSupportiveDocuments.Count; j++) {
                                if (pr.PrDetails[i].PrSupportiveDocuments[j].Todo == 1) {
                                    result = prSupportiveDocumentsDao.Save(pr.PrDetails[i].PrSupportiveDocuments[j], dbConnection);
                                }
                                else if (pr.PrDetails[i].PrSupportiveDocuments[j].Todo == 3) {
                                    result = prSupportiveDocumentsDao.Delete(pr.PrDetails[i].PrSupportiveDocuments[j].FileId, dbConnection);
                                }
                                else {
                                    result = 1;
                                }

                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -6;
                                }
                            }
                        }
                        else {
                            dbConnection.RollBack();
                            return -2;
                        }
                    }

                    result = prUpdateLogDAO.Save(pr.PrUpdateLog, dbConnection);

                    if (result > 0) {
                        for (int i = 0; i < pr.PrCapexDocs.Count; i++) {
                            if (pr.PrCapexDocs[i].Todo == 3) {
                                File.Delete(HttpContext.Current.Server.MapPath(pr.PrCapexDocs[i].FilePath));
                            }
                        }

                        for (int i = 0; i < pr.PrDetails.Count; i++) {

                            for (int j = 0; j < pr.PrDetails[i].PrFileUploads.Count; j++) {
                                if (pr.PrDetails[i].PrFileUploads[j].Todo == 3) {
                                    File.Delete(HttpContext.Current.Server.MapPath(pr.PrDetails[i].PrFileUploads[j].FilePath));
                                }
                            }

                            for (int j = 0; j < pr.PrDetails[i].PrReplacementFileUploads.Count; j++) {
                                if (pr.PrDetails[i].PrReplacementFileUploads[j].Todo == 3) {
                                    File.Delete(HttpContext.Current.Server.MapPath(pr.PrDetails[i].PrReplacementFileUploads[j].FilePath));
                                }
                            }

                            for (int j = 0; j < pr.PrDetails[i].PrSupportiveDocuments.Count; j++) {
                                if (pr.PrDetails[i].PrSupportiveDocuments[j].Todo == 3) {
                                    File.Delete(HttpContext.Current.Server.MapPath(pr.PrDetails[i].PrSupportiveDocuments[j].FilePath));
                                }
                            }
                        }

                        return 1;
                    }
                    else {
                        dbConnection.RollBack();
                        return -7;
                    }

                }
                else {
                    dbConnection.RollBack();
                    return -1;
                }
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public int ClonePR(int prId, int clonedBy) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.ClonePR(prId, clonedBy, dbConnection);
            }
            catch (Exception EX) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchMyPrByBasicSearchByMonth(int createdBy, DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchMyPrByBasicSearchByMonth(createdBy, date, dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchMyPr(int createdBy) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchMyPr(createdBy, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 FetchMyPrByBasicSearchByPrCode(int createdBy, string prCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchMyPrByBasicSearchByPrCode(createdBy, prCode, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchMyPrByAdvanceSearch(int createdBy, List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int prType, int mainCatergoryId, int subCatergoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchMyPrByAdvanceSearch(createdBy, departmentIds, wareHouseIds, purchaseType, purchaseProcedure, createdFromDate, createdToDate, expectedFromDate, expectedToDate, expenseType, prType, mainCatergoryId, subCatergoryId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrDetailsV2> FetchPrDetailsList(int prId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                List<PrDetailsV2> prDetails = prMasterDao.FetchDetailsList(prId, companyId, dbConnection);

                for (int t = 0; t < prDetails.Count; ++t)
                {
                    prDetails[t].PrBoms = DAOFactory.CreatePrBomDAOV2().GetPrdBomForEdit(prDetails[t].PrdId, dbConnection);
                    prDetails[t].PrFileUploads = DAOFactory.CreatePrFileUploadDAOV2().GetPrFileUploadForEdit(prDetails[t].PrdId, dbConnection);
                    prDetails[t].PrReplacementFileUploads = DAOFactory.CreatePrReplacementFileUploadDAOV2().GetPrReplacementFileUploadForEdit(prDetails[t].PrdId, dbConnection);
                    prDetails[t].PrSupportiveDocuments = DAOFactory.CreatePrSupportiveDocumentsDAOV2().GetPrSupportiveDocumentsForEdit(prDetails[t].PrdId, dbConnection);
                    prDetails[t].AvailableWarehouseStock = DAOFactory.CreateInventoryDAO().getAvailableStockValues(prDetails[t].ItemId, prDetails[t].WarehouseId, dbConnection);
                }

                return prDetails;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrDetailsV2> FetchPrDetails(List<int> prId, int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                List<PrDetailsV2> prDetails = prMasterDao.FetchDetails(prId, companyId, dbConnection);

                for (int t = 0; t < prDetails.Count; ++t) {
                    prDetails[t].PrBoms = DAOFactory.CreatePrBomDAOV2().GetPrdBomForEdit(prDetails[t].PrdId, dbConnection);
                    prDetails[t].PrFileUploads = DAOFactory.CreatePrFileUploadDAOV2().GetPrFileUploadForEdit(prDetails[t].PrdId, dbConnection);
                    prDetails[t].PrReplacementFileUploads = DAOFactory.CreatePrReplacementFileUploadDAOV2().GetPrReplacementFileUploadForEdit(prDetails[t].PrdId, dbConnection);
                    prDetails[t].PrSupportiveDocuments = DAOFactory.CreatePrSupportiveDocumentsDAOV2().GetPrSupportiveDocumentsForEdit(prDetails[t].PrdId, dbConnection);
                    prDetails[t].AvailableWarehouseStock = DAOFactory.CreateInventoryDAO().getAvailableStockValues(prDetails[t].ItemId, prDetails[t].WarehouseId, dbConnection);
                }

                return prDetails;
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrUpdateLog> FetchPrUpdateLog(int prId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrUpdateLogDAO prMasterUpdateLogDao = DAOFactory.CreatePrUpdateLogDAO();
                return prMasterUpdateLogDao.FetchPrUpdateLog(prId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int TerminatePR(int prID, int mrnId, int userId, string remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.TerminatePR(prID, mrnId, userId, remarks,dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrDetailsV2 GetPrdTerminationDetails(int prdId)
        {
            throw new NotImplementedException();
        }

        public PrMasterV2 GetPrMasterToView(int prId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                PrMasterV2  prMaster = prMasterDao.GetPrMasterToView(prId, companyId, dbConnection);
                if (prMaster.PrId != 0)
                {
                    prMaster.MrnMaster = DAOFactory.CreateMrnMasterDAOV2().getMrnMasterByMrId(prMaster.MrnId, dbConnection);
                    prMaster.SubDepartment = DAOFactory.CreateSubDepartmentDAO().getDepartmentByID(prMaster.SubDepartmentId, dbConnection);
                    prMaster.Warehouse = DAOFactory.CreateWarehouseDAO().getWarehouseByID(prMaster.WarehouseId, dbConnection);
                }
                return prMaster;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchPrListforApproval(List<int> warehouseIds,int purchaseType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchPrListforApproval(warehouseIds, purchaseType, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        
        public List<PrMasterV2> GetPrMasterList(int MrnId, int Companyid) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.GetPrMasterList(MrnId, Companyid, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public int ApproveOrRejectPr(int expenseType, int prId, int isApproved, int isExpenseApproved, int userId, string remark)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.ApproveOrRejectPr(expenseType, prId, isApproved, isExpenseApproved, userId, remark, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchPrListForAvailabiltyExpenseApproval()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchPrListForAvailabiltyExpenseApproval(dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchPrListForAvailabiltyExpenseApprovalByDate(DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchPrListForAvailabiltyExpenseApprovalByDate(date, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchPrListForAvailabiltyExpenseApprovalByPRCode(string PRCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchPrListForAvailabiltyExpenseApprovalByPRCode(PRCode, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 FetchPrWithPrDetails(int prId, int companyId,int warehouseId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                PrMasterV2 prMaster = prMasterDao.getPrMasterByPrId(prId, dbConnection);
                prMaster.PrDetails = prMasterDao.FetchPrDetailsListNew(prId, companyId, warehouseId, dbConnection);
                return prMaster;
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int UpdatePrItemWarehouseStock(int prId, int prdId, decimal warhouseStock, int itemId, int warehpuseId,int UserId, int companyId, int fromUnit, int toUnit, decimal up, DateTime expDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                InventoryDAOInterface inventoryDAOInterface = DAOFactory.CreateInventoryDAO();

                int result = prMasterDao.UpdatePrItemWarehouseStock(prId, prdId, warhouseStock, itemId, companyId, fromUnit, toUnit, dbConnection);
                if (result > 0) {
                    result = inventoryDAOInterface.UpdateStock(itemId, warehpuseId, warhouseStock, UserId, companyId, up, expDate, dbConnection);

                    if (result > 0) {
                        return 1;
                    }
                    else {
                        dbConnection.RollBack();
                        return -2;
                    }
                }
                else {
                    dbConnection.RollBack();
                    return -1;
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
                {
                    dbConnection.Commit();
                }
            }
        }

        public int ApproveOrRejectPrExpense(int prId, int isApproved, string remark, int userId, DateTime approvedDate)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                prMasterDao.ApproveOrRejectPrExpense(prId, isApproved, remark, userId, approvedDate, dbConnection);
                return 1;
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int UpdatePrExpense(int prId, int expenseType, int isBudget, decimal budgetAmount, string budgetRemark, string budgetInformation, int userId, string userName, DateTime now)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                PrUpdateLogDAO prUpdateLogDAO = DAOFactory.CreatePrUpdateLogDAO();
                prMasterDao.UpdatePrExpense(prId, expenseType, isBudget, budgetAmount, budgetRemark, budgetInformation, userId, dbConnection);
                PrUpdateLog updatedLog = new PrUpdateLog()
                {
                    PrId = prId,
                    UpdatedBy = userId,
                    UpdatedByName = userName,
                    UpdateRemarks = "Updated Expense Type"
                };
                int result = prUpdateLogDAO.Save(updatedLog, dbConnection);
                return result;
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchAllPrByBasicSearchByMonth(DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchAllPrByBasicSearchByMonth(date, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 FetchAllPrByBasicSearchByPrCode(string prCode)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchAllPrByBasicSearchByPrCode(prCode, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
        public List<PrMasterV2> GetPRListForPrInquiry(int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 pr_MasterDAO = DAOFactory.CreatePrMasterDAOV2();
                return pr_MasterDAO.GetPRListForPrInquiry(CompanyId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }
        public List<PrMasterV2> FetchAllPrByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int prType, int mainCatergoryId, int subCatergoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchAllPrByAdvanceSearch(departmentIds, wareHouseIds, purchaseType, purchaseProcedure, createdFromDate, createdToDate, expectedFromDate, expectedToDate, expenseType, prType, mainCatergoryId, subCatergoryId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 GetPrSubmittedBid(int prId, int companyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                PrMasterV2 prmaster = prMasterDao.GetPrSubmittedBid(prId, companyId, dbConnection);
                return prmaster;
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int DeleteFromPO(int prId, List<int> itemId, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrDetailsDAOV2 pr_DetailDAO = DAOFactory.CreatePrDetailsDAOV2();
                return pr_DetailDAO.DeleteFromPO(prId, itemId, UserId, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<PrMasterV2> FetchPrListforApprovalByDate(List<int> warehouseIds, int purchaseType, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchPrListforApprovalByDate(warehouseIds, purchaseType, date, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public List<PrMasterV2> FetchPRByPRCode(string PrCode, int Companyid) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchPRByPRCode(PrCode, Companyid, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public PrMasterV2 FetchPrListforApprovalByCode(List<int> warehouseIds, int purchaseType, string code) {
            DBConnection dbConnection = new DBConnection();
            try {
                PrMasterDAOV2 prMasterDao = DAOFactory.CreatePrMasterDAOV2();
                return prMasterDao.FetchPrListforApprovalByCode(warehouseIds, purchaseType,code, dbConnection);
            }
            catch (Exception) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

       
    }
}
