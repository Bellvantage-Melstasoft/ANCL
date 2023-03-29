using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace CLibrary.Controller {
    public interface MrnControllerV2 {
        int SaveMrn(MrnMasterV2 mrn);
        int UpdateMrn(MrnMasterV2 mrn);
        MrnMasterV2 GetMrnForEditing(int MrnId);
        int CloneMRN(int mrnId, int clonedBy);
        List<MrnMasterV2> FetchMyMrnByBasicSearchByMonth(int createdBy, DateTime date);
        MrnMasterV2 FetchMyMrnByBasicSearchByMrnCode(int createdBy, string mrnCode);
        List<MrnMasterV2> FetchMyMrnByAdvanceSearch(int createdBy, List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId);
        MrnMasterV2 GetMRNMasterToView(int mrnId, int companyId);
        List<MrnDetailsV2> FetchMrnDetailsList(int mrnId, int companyId);
        MrnDetailsV2 GetMrndTerminationDetails(int mrndId);
        int TerminateMRN(int MrnID, int TerminatedBy, string Remarks);
        List<MrnMasterV2> FetchMrnListforApproval(List<int> subDepartmentIds);
        int ApproveOrRejectMrn(int expenseType, int mrnId, int isApproved, int isExpenseApproved, int userId, string remark);
        List<MrnMasterV2> FetchAllMrnByBasicSearchByMonth(DateTime date);
        MrnMasterV2 FetchAllMrnByBasicSearchByMrId(int mrnId);
        List<MrnMasterV2> FetchAllMrnByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId);
        List<MrnMasterV2> FetchMrnListForAvailabiltyExpenseApproval();
        MrnMasterV2 FetchMrnWithMrnDetails(int mrnId, int companyId);
        int UpdateMrnItemDepartmentStock(List<MrnDetailsV2> listMrnDetails, int userId, string userName, string remark);
        int ApproveOrRejectMRNExpense(int mrnId, int isApproved, string remark, int userId, DateTime approvedDate);
        int UpdateMRNExpense(int mrnId, int expenseType, int isBudget, decimal budgetAmount, string budgetRemark, string budgetInformation, int userId, string userName, DateTime now);
        List<MrnUpdateLog> FetchMrnUpdateLog(int mrnId);
        List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeper(int companyId);
        List<MrnMasterV2> FetchMrnByWarehouseIdToAssignStoreKeeper(List<int> warehouseId);
        MrnMasterV2 FetchAssignedMrnForStoreKeeperByMrnCode(int storekeeperId, string mrnCode);
        // List<MrnMasterV2> FetchMrnByCompanyId(int companyId);
        int UpdateStoreKeeperToMRN(int storeKeeperId, int MRNId);
        List<MrnDetailsV2> FetchMrnDetailsListWithoutTerminated(int mrnId, int companyId, int WarehouseId);
        string AddMRNtoPR(MrnMasterV2 mrnMaster, int userId, int companyId, out int PrId);
        int UpdateMRNItemDepartmentStock(int mrnId, int mrndId, decimal departmentStock);
        MrnMasterV2 FetchAllMrnByBasicSearchByMrnCode(string mrnId);
        List<MrnMasterV2> FetchApprovedMrnByBasicSearchByMonth(DateTime date, List<int> warehouseId);
        MrnMasterV2 FetchApprovedMrnByBasicSearchByMrnCode(string mrnId, List<int> warehouseId);
        List<MrnMasterV2> FetchApprovedMrnByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId);
        List<MrnMasterV2> FetchMrnForExpAppByBasicSearchByMonth(DateTime date);
        List<MrnBomV2> GetMrndBomForEdit(int mrndId);
        int updateMrnMasterAfterClone(int mrnId);
        MrnMasterV2 FetchMrnForExpAppByBasicSearchByMrnCode(string mrnId);
        List<MrnMasterV2> FetchMrnForExpAppByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId);
        List<MrnMasterV2> FetchAssignedMrnForStoreKeeperByDate(int storekeeperId, DateTime date);
        List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeperByDate(int companyId, DateTime date);
        List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeperByMrnCode(int companyId, string MrnCode);
        List<MrnMasterV2> FetchMrnListforApprovalByDate(List<int> subDepartmentIds, DateTime date);
        List<MrnMasterV2> FetchMrnListforApprovalByMrnCode(List<int> subDepartmentIds, string MrnCode);
        List<MrnMasterV2> FetchMRNByMRNCode(string MrnCode, int Companyid);
        MrnMasterV2 GetMRNMasterToViewRequisitionReport(int mrnId, int companyId);
        List<MrnMasterV2> FetchMyMrn(int createdBy);
        List<MrnMasterV2> FetchMrnForExpApp();
        List<MrnMasterV2> FetchAllMrnh();
        List<MrnMasterV2> FetchApprovedMrnByBasicSearch(List<int> warehouseId);
        List<MrnMasterV2> FetchAssignedMrnForStoreKeeper(int storekeeperId);
    }
    class MrnControllerV2Impl : MrnControllerV2 {
        public MrnMasterV2 GetMrnForEditing(int MrnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                MrnDetailsDAOV2 mrnDetailsDao = DAOFactory.CreateMrnDetailsDAOV2();
                MrnBomDAOV2 mrnBomDao = DAOFactory.CreateMrnBomDAOV2();
                MrnFileUploadDAOV2 mrnFileUploadDao = DAOFactory.CreateMrnFileUploadDAOV2();
                MrnReplacementFileUploadDAOV2 mrnReplacementFileUploadDao = DAOFactory.CreateMrnReplacementFileUploadDAOV2();
                MrnSupportiveDocumentsDAOV2 mrnSupportiveDocumentsDao = DAOFactory.CreateMrnSupportiveDocumentsDAOV2();
                MrnCapexDocDAO mrnCapexDocDao = DAOFactory.CreateMrnCapexDocDAO();

                MrnMasterV2 mrn = mrnMasterDao.GetMrnForEditing(MrnId, dbConnection);

                if (mrn != null && mrn.MrnId != 0) {
                    mrn.MrnCapexDocs = mrnCapexDocDao.GetMrnCapexDocsForEdit(mrn.MrnId, dbConnection);
                    mrn.MrnDetails = mrnDetailsDao.GetMrnDetailsForEdit(mrn.MrnId, dbConnection);

                    for (int i = 0; i < mrn.MrnDetails.Count; i++) {
                        mrn.MrnDetails[i].MrnBoms = mrnBomDao.GetMrndBomForEdit(mrn.MrnDetails[i].MrndId, dbConnection);
                        mrn.MrnDetails[i].MrnReplacementFileUploads = mrnReplacementFileUploadDao.GetMrnReplacementFileUploadForEdit(mrn.MrnDetails[i].MrndId, dbConnection);
                        mrn.MrnDetails[i].MrnFileUploads = mrnFileUploadDao.GetMrnFileUploadForEdit(mrn.MrnDetails[i].MrndId, dbConnection);
                        mrn.MrnDetails[i].MrnSupportiveDocuments = mrnSupportiveDocumentsDao.GetMrnSupportiveDocumentsForEdit(mrn.MrnDetails[i].MrndId, dbConnection);
                    }

                    return mrn;
                }
                else {
                    return null;
                }

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

        public int SaveMrn(MrnMasterV2 mrn) {
            DBConnection dbConnection = new DBConnection();
            try {
                for (int i = 0; i < mrn.MrnCapexDocs.Count; i++) {
                    string fileName = i + "_" + LocalTime.Now.Ticks + "_" + mrn.MrnCapexDocs[i].FileName;

                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/MrnCapexDocs"))) {

                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/MrnCapexDocs"));
                    }

                    File.WriteAllBytes(HttpContext.Current.Server.MapPath("/MrnCapexDocs/" + fileName), Convert.FromBase64String(mrn.MrnCapexDocs[i].FileData));
                    mrn.MrnCapexDocs[i].FilePath = "/MrnCapexDocs/" + fileName;

                }

                for (int i = 0; i < mrn.MrnDetails.Count; i++) {
                    for (int j = 0; j < mrn.MrnDetails[i].MrnFileUploads.Count; j++) {

                        string fileName = i + "_" + LocalTime.Now.Ticks + "_" + mrn.MrnDetails[i].MrnFileUploads[j].FileName;

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("/MrnFileUploads"))) {

                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/MrnFileUploads"));
                        }

                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("/MrnFileUploads/" + fileName), Convert.FromBase64String(mrn.MrnDetails[i].MrnFileUploads[j].FileData));
                        mrn.MrnDetails[i].MrnFileUploads[j].FilePath = "/MrnFileUploads/" + fileName;
                    }

                    for (int j = 0; j < mrn.MrnDetails[i].MrnReplacementFileUploads.Count; j++) {

                        string fileName = i + "_" + LocalTime.Now.Ticks + "_" + mrn.MrnDetails[i].MrnReplacementFileUploads[j].FileName;

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("/MrnReplacementFileUploads"))) {

                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/MrnReplacementFileUploads"));
                        }

                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("/MrnReplacementFileUploads/" + fileName), Convert.FromBase64String(mrn.MrnDetails[i].MrnReplacementFileUploads[j].FileData));
                        mrn.MrnDetails[i].MrnReplacementFileUploads[j].FilePath = "/MrnReplacementFileUploads/" + fileName;
                    }

                    for (int j = 0; j < mrn.MrnDetails[i].MrnSupportiveDocuments.Count; j++) {

                        string fileName = i + "_" + LocalTime.Now.Ticks + "_" + mrn.MrnDetails[i].MrnSupportiveDocuments[j].FileName;

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("/MrnSupportiveDocs"))) {

                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/MrnSupportiveDocs"));
                        }

                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("/MrnSupportiveDocs/" + fileName), Convert.FromBase64String(mrn.MrnDetails[i].MrnSupportiveDocuments[j].FileData));
                        mrn.MrnDetails[i].MrnSupportiveDocuments[j].FilePath = "/MrnSupportiveDocs/" + fileName;
                    }
                }

                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                MrnDetailsDAOV2 mrnDetailsDao = DAOFactory.CreateMrnDetailsDAOV2();
                MrnBomDAOV2 mrnBomDao = DAOFactory.CreateMrnBomDAOV2();
                MrnFileUploadDAOV2 mrnFileUploadDao = DAOFactory.CreateMrnFileUploadDAOV2();
                MrnReplacementFileUploadDAOV2 mrnReplacementFileUploadDao = DAOFactory.CreateMrnReplacementFileUploadDAOV2();
                MrnCapexDocDAO mrnCapexDocDao = DAOFactory.CreateMrnCapexDocDAO();
                MrnSupportiveDocumentsDAOV2 mrnSupportiveDocumentsDao = DAOFactory.CreateMrnSupportiveDocumentsDAOV2();
                MrnDetailsStatusLogController mrnDetailsStatusLogController = ControllerFactory.CreateMrnDetailStatusLogController();

                List<int> mrnIdCode = mrnMasterDao.SaveMrn(mrn, dbConnection);

                if (mrnIdCode.Count > 0) {
                    mrn.MrnDetails.ForEach(mrnd => mrnd.MrnId = mrnIdCode[0]);
                    mrn.MrnCapexDocs.ForEach(mrnd => mrnd.MrnId = mrnIdCode[0]);

                    int result = 0;
                    for (int i = 0; i < mrn.MrnCapexDocs.Count; i++) {
                        result = mrnCapexDocDao.Save(mrn.MrnCapexDocs[i], dbConnection);
                        if (result <= 0) {
                            dbConnection.RollBack();
                            return -7;
                        }
                    }

                    for (int i = 0; i < mrn.MrnDetails.Count; i++) {

                        result = mrnDetailsDao.Save(mrn.MrnDetails[i], dbConnection);
                        if (result > 0) {

                            mrn.MrnDetails[i].MrnBoms.ForEach(obj => obj.MrndId = result);
                            mrn.MrnDetails[i].MrnFileUploads.ForEach(obj => obj.MrndId = result);
                            mrn.MrnDetails[i].MrnReplacementFileUploads.ForEach(obj => obj.MrndId = result);
                            mrn.MrnDetails[i].MrnSupportiveDocuments.ForEach(obj => obj.MrndId = result);


                            MRNDetailsStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
                            // Inserting log into "MRN_DETAIL_STATUS_LOG"
                            mrnDetailStatusLogDAO.InsertLog(result, mrn.CreatedBy, 0, dbConnection);

                            for (int j = 0; j < mrn.MrnDetails[i].MrnBoms.Count; j++) {
                                result = mrnBomDao.Save(mrn.MrnDetails[i].MrnBoms[j], dbConnection);
                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -3;
                                }
                            }

                            for (int j = 0; j < mrn.MrnDetails[i].MrnFileUploads.Count; j++) {
                                result = mrnFileUploadDao.Save(mrn.MrnDetails[i].MrnFileUploads[j], dbConnection);
                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -4;
                                }
                            }

                            for (int j = 0; j < mrn.MrnDetails[i].MrnReplacementFileUploads.Count; j++) {
                                result = mrnReplacementFileUploadDao.Save(mrn.MrnDetails[i].MrnReplacementFileUploads[j], dbConnection);
                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -5;
                                }
                            }

                            for (int j = 0; j < mrn.MrnDetails[i].MrnSupportiveDocuments.Count; j++) {
                                result = mrnSupportiveDocumentsDao.Save(mrn.MrnDetails[i].MrnSupportiveDocuments[j], dbConnection);
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

                    return mrnIdCode[1];
                }
                else {
                    dbConnection.RollBack();
                    return -1;
                }
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

        public int UpdateMrn(MrnMasterV2 mrn) {
            DBConnection dbConnection = new DBConnection();
            try {
                if (mrn.ExpenseType == 2)
                    mrn.MrnCapexDocs.ForEach(doc => doc.Todo = 3);

                for (int i = 0; i < mrn.MrnCapexDocs.Count; i++) {
                    if (mrn.MrnCapexDocs[i].Todo == 1) {
                        string fileName = i + "_" + LocalTime.Now.Ticks + "_" + mrn.MrnCapexDocs[i].FileName;

                        if (!Directory.Exists(HttpContext.Current.Server.MapPath("/MrnCapexDocs"))) {

                            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/MrnCapexDocs"));
                        }

                        File.WriteAllBytes(HttpContext.Current.Server.MapPath("/MrnCapexDocs/" + fileName), Convert.FromBase64String(mrn.MrnCapexDocs[i].FileData));
                        mrn.MrnCapexDocs[i].FilePath = "/MrnCapexDocs/" + fileName;
                    }
                }

                for (int i = 0; i < mrn.MrnDetails.Count; i++) {
                    for (int j = 0; j < mrn.MrnDetails[i].MrnFileUploads.Count; j++) {

                        if (mrn.MrnDetails[i].MrnFileUploads[j].Todo == 1) {
                            string fileName = i + "_" + LocalTime.Now.Ticks + "_" + mrn.MrnDetails[i].MrnFileUploads[j].FileName;

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/MrnFileUploads"))) {

                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/MrnFileUploads"));
                            }

                            File.WriteAllBytes(HttpContext.Current.Server.MapPath("/MrnFileUploads/" + fileName), Convert.FromBase64String(mrn.MrnDetails[i].MrnFileUploads[j].FileData));
                            mrn.MrnDetails[i].MrnFileUploads[j].FilePath = "/MrnFileUploads/" + fileName;
                        }
                    }

                    for (int j = 0; j < mrn.MrnDetails[i].MrnReplacementFileUploads.Count; j++) {
                        if (mrn.MrnDetails[i].MrnReplacementFileUploads[j].Todo == 1) {
                            string fileName = i + "_" + LocalTime.Now.Ticks + "_" + mrn.MrnDetails[i].MrnReplacementFileUploads[j].FileName;

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/MrnReplacementFileUploads"))) {

                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/MrnReplacementFileUploads"));
                            }

                            File.WriteAllBytes(HttpContext.Current.Server.MapPath("/MrnReplacementFileUploads/" + fileName), Convert.FromBase64String(mrn.MrnDetails[i].MrnReplacementFileUploads[j].FileData));
                            mrn.MrnDetails[i].MrnReplacementFileUploads[j].FilePath = "/MrnReplacementFileUploads/" + fileName;
                        }
                    }

                    for (int j = 0; j < mrn.MrnDetails[i].MrnSupportiveDocuments.Count; j++) {
                        if (mrn.MrnDetails[i].MrnSupportiveDocuments[j].Todo == 1) {
                            string fileName = i + "_" + LocalTime.Now.Ticks + "_" + mrn.MrnDetails[i].MrnSupportiveDocuments[j].FileName;

                            if (!Directory.Exists(HttpContext.Current.Server.MapPath("/MrnSupportiveDocs"))) {

                                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/MrnSupportiveDocs"));
                            }

                            File.WriteAllBytes(HttpContext.Current.Server.MapPath("/MrnSupportiveDocs/" + fileName), Convert.FromBase64String(mrn.MrnDetails[i].MrnSupportiveDocuments[j].FileData));
                            mrn.MrnDetails[i].MrnSupportiveDocuments[j].FilePath = "/MrnSupportiveDocs/" + fileName;
                        }
                    }
                }

                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                MrnDetailsDAOV2 mrnDetailsDao = DAOFactory.CreateMrnDetailsDAOV2();
                MrnBomDAOV2 mrnBomDao = DAOFactory.CreateMrnBomDAOV2();
                MrnFileUploadDAOV2 mrnFileUploadDao = DAOFactory.CreateMrnFileUploadDAOV2();
                MrnReplacementFileUploadDAOV2 mrnReplacementFileUploadDao = DAOFactory.CreateMrnReplacementFileUploadDAOV2();
                MrnCapexDocDAO mrnCapexDocDao = DAOFactory.CreateMrnCapexDocDAO();
                MrnSupportiveDocumentsDAOV2 mrnSupportiveDocumentsDao = DAOFactory.CreateMrnSupportiveDocumentsDAOV2();
                MrnUpdateLogDAO mrnUpdateLogDAO = DAOFactory.CreateMrnUpdateLogDAO();

                int result = mrnMasterDao.UpdateMrn(mrn, dbConnection);

                if (result > 0) {
                    mrn.MrnCapexDocs.ForEach(doc => doc.MrnId = mrn.MrnId);

                    for (int i = 0; i < mrn.MrnCapexDocs.Count; i++) {
                        if (mrn.MrnCapexDocs[i].Todo == 1) {
                            result = mrnCapexDocDao.Save(mrn.MrnCapexDocs[i], dbConnection);
                        }
                        else if (mrn.MrnCapexDocs[i].Todo == 3) {
                            result = mrnCapexDocDao.Delete(mrn.MrnCapexDocs[i].FileId, dbConnection);
                        }
                        else {
                            result = 1;
                        }

                        if (result <= 0) {
                            dbConnection.RollBack();
                            return -8;
                        }
                    }

                    for (int i = 0; i < mrn.MrnDetails.Count; i++) {
                        if (mrn.MrnDetails[i].Todo == 1) {
                            mrn.MrnDetails[i].MrnId = mrn.MrnId;

                            result = mrnDetailsDao.Save(mrn.MrnDetails[i], dbConnection);
                            mrn.MrnDetails[i].MrndId = result;
                        }
                        else if (mrn.MrnDetails[i].Todo == 2) {
                            result = mrnDetailsDao.Update(mrn.MrnDetails[i], dbConnection);
                        }
                        else if (mrn.MrnDetails[i].Todo == 3) {
                            result = mrnDetailsDao.Delete(mrn.MrnDetails[i].MrndId, dbConnection);

                            mrn.MrnDetails[i].MrnFileUploads.ForEach(obj => obj.Todo = 3);
                            mrn.MrnDetails[i].MrnReplacementFileUploads.ForEach(obj => obj.Todo = 3);
                            mrn.MrnDetails[i].MrnSupportiveDocuments.ForEach(obj => obj.Todo = 3);

                            continue;
                        }
                        else {
                            result = 1;
                        }

                        if (result > 0) {

                            mrn.MrnDetails[i].MrnBoms.ForEach(obj => obj.MrndId = mrn.MrnDetails[i].MrndId);
                            mrn.MrnDetails[i].MrnFileUploads.ForEach(obj => obj.MrndId = mrn.MrnDetails[i].MrndId);
                            mrn.MrnDetails[i].MrnReplacementFileUploads.ForEach(obj => obj.MrndId = mrn.MrnDetails[i].MrndId);
                            mrn.MrnDetails[i].MrnSupportiveDocuments.ForEach(obj => obj.MrndId = mrn.MrnDetails[i].MrndId);

                            for (int j = 0; j < mrn.MrnDetails[i].MrnBoms.Count; j++) {
                                if (mrn.MrnDetails[i].MrnBoms[j].Todo == 1) {
                                    result = mrnBomDao.Save(mrn.MrnDetails[i].MrnBoms[j], dbConnection);
                                }
                                else if (mrn.MrnDetails[i].MrnBoms[j].Todo == 3) {
                                    result = mrnBomDao.Delete(mrn.MrnDetails[i].MrnBoms[j].BomId, dbConnection);
                                }
                                else {
                                    result = 1;
                                }

                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -3;
                                }
                            }

                            for (int j = 0; j < mrn.MrnDetails[i].MrnFileUploads.Count; j++) {
                                if (mrn.MrnDetails[i].MrnFileUploads[j].Todo == 1) {
                                    result = mrnFileUploadDao.Save(mrn.MrnDetails[i].MrnFileUploads[j], dbConnection);
                                }
                                else if (mrn.MrnDetails[i].MrnFileUploads[j].Todo == 3) {
                                    result = mrnFileUploadDao.Delete(mrn.MrnDetails[i].MrnFileUploads[j].FileId, dbConnection);
                                }
                                else {
                                    result = 1;
                                }

                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -4;
                                }
                            }

                            for (int j = 0; j < mrn.MrnDetails[i].MrnReplacementFileUploads.Count; j++) {
                                if (mrn.MrnDetails[i].MrnReplacementFileUploads[j].Todo == 1) {
                                    result = mrnReplacementFileUploadDao.Save(mrn.MrnDetails[i].MrnReplacementFileUploads[j], dbConnection);
                                }
                                else if (mrn.MrnDetails[i].MrnReplacementFileUploads[j].Todo == 3) {
                                    result = mrnReplacementFileUploadDao.Delete(mrn.MrnDetails[i].MrnReplacementFileUploads[j].FileId, dbConnection);
                                }
                                else {
                                    result = 1;
                                }

                                if (result <= 0) {
                                    dbConnection.RollBack();
                                    return -5;
                                }
                            }

                            for (int j = 0; j < mrn.MrnDetails[i].MrnSupportiveDocuments.Count; j++) {
                                if (mrn.MrnDetails[i].MrnSupportiveDocuments[j].Todo == 1) {
                                    result = mrnSupportiveDocumentsDao.Save(mrn.MrnDetails[i].MrnSupportiveDocuments[j], dbConnection);
                                }
                                else if (mrn.MrnDetails[i].MrnSupportiveDocuments[j].Todo == 3) {
                                    result = mrnSupportiveDocumentsDao.Delete(mrn.MrnDetails[i].MrnSupportiveDocuments[j].FileId, dbConnection);
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

                    result = mrnUpdateLogDAO.Save(mrn.MrnUpdateLog, dbConnection);

                    if (result > 0) {
                        for (int i = 0; i < mrn.MrnCapexDocs.Count; i++) {
                            if (mrn.MrnCapexDocs[i].Todo == 3) {
                                File.Delete(HttpContext.Current.Server.MapPath(mrn.MrnCapexDocs[i].FilePath));
                            }
                        }

                        for (int i = 0; i < mrn.MrnDetails.Count; i++) {

                            for (int j = 0; j < mrn.MrnDetails[i].MrnFileUploads.Count; j++) {
                                if (mrn.MrnDetails[i].MrnFileUploads[j].Todo == 3) {
                                    File.Delete(HttpContext.Current.Server.MapPath(mrn.MrnDetails[i].MrnFileUploads[j].FilePath));
                                }
                            }

                            for (int j = 0; j < mrn.MrnDetails[i].MrnReplacementFileUploads.Count; j++) {
                                if (mrn.MrnDetails[i].MrnReplacementFileUploads[j].Todo == 3) {
                                    File.Delete(HttpContext.Current.Server.MapPath(mrn.MrnDetails[i].MrnReplacementFileUploads[j].FilePath));
                                }
                            }

                            for (int j = 0; j < mrn.MrnDetails[i].MrnSupportiveDocuments.Count; j++) {
                                if (mrn.MrnDetails[i].MrnSupportiveDocuments[j].Todo == 3) {
                                    File.Delete(HttpContext.Current.Server.MapPath(mrn.MrnDetails[i].MrnSupportiveDocuments[j].FilePath));
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


        public List<MrnMasterV2> FetchMyMrnByBasicSearchByMonth(int createdBy, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMyMrnByBasicSearchByMonth(createdBy, date, dbConnection);
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

        public List<MrnMasterV2> FetchMyMrn(int createdBy) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMyMrn(createdBy, dbConnection);
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

        public List<MrnMasterV2> FetchMRNByMRNCode(string MrnCode, int Companyid) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMRNByMRNCode(MrnCode, Companyid, dbConnection);
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

        public List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeper(int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnByCompanyIdToAssignStoreKeeper(companyId, dbConnection);
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

        public List<MrnMasterV2> FetchMrnByWarehouseIdToAssignStoreKeeper(List<int> warehouseId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnByWarehouseIdToAssignStoreKeeper(warehouseId, dbConnection);
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


        public MrnMasterV2 FetchMyMrnByBasicSearchByMrnCode(int createdBy, string mrnCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMyMrnByBasicSearchByMrnCode(createdBy, mrnCode, dbConnection);
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

        public List<MrnMasterV2> FetchMyMrnByAdvanceSearch(int createdBy, List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMyMrnByAdvanceSearch(createdBy, departmentIds, wareHouseIds, purchaseType, purchaseProcedure, createdFromDate, createdToDate, expectedFromDate, expectedToDate, expenseType, mrnType, mainCatergoryId, subCatergoryId, dbConnection);
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

        public MrnMasterV2 GetMRNMasterToView(int mrnId, int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                MrnMasterV2 mrnMaster = mrnMasterDao.GetMRNMasterToView(mrnId, companyId, dbConnection);
                if (mrnMaster.MrnId != 0) {
                    mrnMaster.SubDepartment = DAOFactory.CreateSubDepartmentDAO().getDepartmentByID(mrnMaster.SubDepartmentId, dbConnection);
                    mrnMaster.Warehouse = DAOFactory.CreateWarehouseDAO().getWarehouseByID(mrnMaster.WarehouseId, dbConnection);
                    // mrnMaster.MrnDetails = DAOFactory.CreateMrnDetailsDAOV2().GetMrnDetails(mrnMaster.MrnId, dbConnection);
                    mrnMaster.MrnCapexDocs = DAOFactory.CreateMrnCapexDocDAO().GetMrnCapexDocs(mrnMaster.MrnId,dbConnection);
                }
                return mrnMaster;
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
        public MrnMasterV2 GetMRNMasterToViewRequisitionReport(int mrnId, int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                MrnMasterV2 mrnMaster = mrnMasterDao.GetMRNMasterToViewRequisitionReport(mrnId, companyId, dbConnection);
                if (mrnMaster.MrnId != 0) {
                    mrnMaster.SubDepartment = DAOFactory.CreateSubDepartmentDAO().getDepartmentByID(mrnMaster.SubDepartmentId, dbConnection);
                    mrnMaster.Warehouse = DAOFactory.CreateWarehouseDAO().getWarehouseByID(mrnMaster.WarehouseId, dbConnection);
                    // mrnMaster.MrnDetails = DAOFactory.CreateMrnDetailsDAOV2().GetMrnDetails(mrnMaster.MrnId, dbConnection);
                    mrnMaster.MrnCapexDocs = DAOFactory.CreateMrnCapexDocDAO().GetMrnCapexDocs(mrnMaster.MrnId, dbConnection);
                }
                return mrnMaster;
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
        public List<MrnDetailsV2> FetchMrnDetailsList(int mrnId, int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                List<MrnDetailsV2> mrnDetails = mrnMasterDao.FetchMrnDetailsList(mrnId, companyId, dbConnection);

                for (int t = 0; t < mrnDetails.Count; ++t) {
                    mrnDetails[t].MrnBoms = DAOFactory.CreateMrnBomDAOV2().GetMrndBomForEdit(mrnDetails[t].MrndId, dbConnection);
                    mrnDetails[t].MrnFileUploads = DAOFactory.CreateMrnFileUploadDAOV2().GetMrnFileUploadForEdit(mrnDetails[t].MrndId, dbConnection);
                    mrnDetails[t].MrnReplacementFileUploads = DAOFactory.CreateMrnReplacementFileUploadDAOV2().GetMrnReplacementFileUploadForEdit(mrnDetails[t].MrndId, dbConnection);
                    mrnDetails[t].MrnSupportiveDocuments = DAOFactory.CreateMrnSupportiveDocumentsDAOV2().GetMrnSupportiveDocumentsForEdit(mrnDetails[t].MrndId, dbConnection);
                }

                return mrnDetails;
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

        public MrnDetailsV2 GetMrndTerminationDetails(int mrndId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.GetMrndTerminationDetails(mrndId, dbConnection);
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

        public int TerminateMRN(int MrnID, int TerminatedBy, string Remarks) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.TerminateMRN(MrnID, TerminatedBy, Remarks, dbConnection);
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

        public List<MrnMasterV2> FetchMrnListforApproval(List<int> subDepartmentIds) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnListforApproval(subDepartmentIds, dbConnection);
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

        public int ApproveOrRejectMrn(int expenseType, int mrnId, int isApproved, int isExpenseApproved, int userId, string remark) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.ApproveOrRejectMrn(expenseType, mrnId, isApproved, isExpenseApproved, userId, remark, dbConnection);
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

        public List<MrnMasterV2> FetchAllMrnByBasicSearchByMonth(DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchAllMrnByBasicSearchByMonth(date, dbConnection);
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

        public List<MrnMasterV2> FetchAllMrnh() {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchAllMrnh(dbConnection);
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

        public MrnMasterV2 FetchAllMrnByBasicSearchByMrId(int mrnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchAllMrnByBasicSearchByMrId(mrnId, dbConnection);
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

        public List<MrnMasterV2> FetchAllMrnByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchAllMrnByAdvanceSearch(departmentIds, wareHouseIds, purchaseType, purchaseProcedure, createdFromDate, createdToDate, expectedFromDate, expectedToDate, expenseType, mrnType, mainCatergoryId, subCatergoryId, dbConnection);
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

        public List<MrnMasterV2> FetchMrnListForAvailabiltyExpenseApproval() {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnListForAvailabiltyExpenseApproval(dbConnection);
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

        public MrnMasterV2 FetchMrnWithMrnDetails(int mrnId, int companyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                MrnMasterV2 mrnMaster = mrnMasterDao.FetchAllMrnByBasicSearchByMrId(mrnId, dbConnection);
                mrnMaster.MrnDetails = mrnMasterDao.FetchMrnDetailsList(mrnId, companyId, dbConnection);
                return mrnMaster;
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

        public int UpdateMrnItemDepartmentStock(List<MrnDetailsV2> listMrnDetails, int userId, string userName, string remark) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                MrnUpdateLogDAO mrnUpdateLogDAO = DAOFactory.CreateMrnUpdateLogDAO();
                for (int t = 0; t < listMrnDetails.Count; ++t) {
                    mrnMasterDao.UpdateMrnItemDepartmentStock(listMrnDetails[t].MrnId, listMrnDetails[t].MrndId, listMrnDetails[t].DepartmentStock, dbConnection);
                }
                MrnUpdateLog updatedLog = new MrnUpdateLog() {
                    MrnId = listMrnDetails[0].MrnId,
                    UpdatedBy = userId,
                    UpdatedByName = userName,
                    UpdateRemarks = remark
                };
                int result = mrnUpdateLogDAO.Save(updatedLog, dbConnection);
                return result;
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

        public int ApproveOrRejectMRNExpense(int mrnId, int isApproved, string remark, int userId, DateTime approvedDate) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                mrnMasterDao.ApproveOrRejectMRNExpense(mrnId, isApproved, remark, userId, approvedDate, dbConnection);
                return 1;
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

        public int UpdateStoreKeeperToMRN(int storeKeeperId, int MRNId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                mrnMasterDao.UpdateStoreKeeperToMRN(storeKeeperId, MRNId, dbConnection);
                return 1;
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

        public int UpdateMRNExpense(int mrnId, int expenseType, int isBudget, decimal budgetAmount, string budgetRemark, string budgetInformation, int userId, string userName, DateTime now) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                MrnUpdateLogDAO mrnUpdateLogDAO = DAOFactory.CreateMrnUpdateLogDAO();
                mrnMasterDao.UpdateMRNExpense(mrnId, expenseType, isBudget, budgetAmount, budgetRemark, budgetInformation, userId, dbConnection);
                MrnUpdateLog updatedLog = new MrnUpdateLog() {
                    MrnId = mrnId,
                    UpdatedBy = userId,
                    UpdatedByName = userName,
                    UpdateRemarks = "Updated Expense Type"
                };
                int result = mrnUpdateLogDAO.Save(updatedLog, dbConnection);
                return result;
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

        public int CloneMRN(int mrnId, int clonedBy) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.CloneMRN(mrnId, clonedBy, dbConnection);
            }
            catch (Exception Ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }

        public List<MrnUpdateLog> FetchMrnUpdateLog(int mrnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnUpdateLogDAO mrnMasterUpdateLogDao = DAOFactory.CreateMrnUpdateLogDAO();
                return mrnMasterUpdateLogDao.FetchMrnUpdateLog(mrnId, dbConnection);
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

        public MrnMasterV2 FetchAssignedMrnForStoreKeeperByMrnCode(int storekeeperId, string mrnCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchAssignedMrnForStoreKeeperByMrnCode(storekeeperId, mrnCode, dbConnection);
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

        public List<MrnDetailsV2> FetchMrnDetailsListWithoutTerminated(int mrnId, int companyId ,int WarehouseId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                List<MrnDetailsV2> mrnDetails = mrnMasterDao.FetchMrnDetailsListWithoutTerminated(mrnId, companyId, WarehouseId, dbConnection);

                for (int t = 0; t < mrnDetails.Count; ++t) {
                    mrnDetails[t].MrnBoms = DAOFactory.CreateMrnBomDAOV2().GetMrndBomForEdit(mrnDetails[t].MrndId, dbConnection);
                    mrnDetails[t].MrnFileUploads = DAOFactory.CreateMrnFileUploadDAOV2().GetMrnFileUploadForEdit(mrnDetails[t].MrndId, dbConnection);
                    mrnDetails[t].MrnReplacementFileUploads = DAOFactory.CreateMrnReplacementFileUploadDAOV2().GetMrnReplacementFileUploadForEdit(mrnDetails[t].MrndId, dbConnection);
                    mrnDetails[t].MrnSupportiveDocuments = DAOFactory.CreateMrnSupportiveDocumentsDAOV2().GetMrnSupportiveDocumentsForEdit(mrnDetails[t].MrndId, dbConnection);
                }

                return mrnDetails;
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
        public int UpdateMRNItemDepartmentStock(int mrnId, int mrndId, decimal departmentStock) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.UpdateMRNItemDepartmentStock(mrnId, mrndId, departmentStock, dbConnection);
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
        public string AddMRNtoPR(MrnMasterV2 mrnMaster, int userId, int companyId, out int PrId) {
            DBConnection dbConnection = null;
            try {
                dbConnection = new DBConnection();
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                // copying attachement to Pr folder and changing the file path
                for (int t = 0; t < mrnMaster.MrnDetails.Count; ++t) {
                    //Replacement Images of MR Detail is copied to PR Detail
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrReplacementFileUploads")))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrReplacementFileUploads"));
                    for (int h = 0; h < mrnMaster.MrnDetails[t].MrnReplacementFileUploads.Count; ++h) {
                        string fileToCopy = HttpContext.Current.Server.MapPath("/MrnReplacementFileUploads") + "\\" + Path.GetFileName(mrnMaster.MrnDetails[t].MrnReplacementFileUploads[h].FilePath);
                        string destination = HttpContext.Current.Server.MapPath("/PrReplacementFileUploads") + "\\" + Path.GetFileName(fileToCopy);
                        File.Copy(fileToCopy, destination, true);
                        mrnMaster.MrnDetails[t].MrnReplacementFileUploads[h].FilePath = "/PrReplacementFileUploads/" + Path.GetFileName(fileToCopy);
                    }
                    //Standard Images of MR Detail is copied to PR Detail
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrFileUploads")))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrFileUploads"));
                    for (int h = 0; h < mrnMaster.MrnDetails[t].MrnFileUploads.Count; ++h) {
                        string fileToCopy = HttpContext.Current.Server.MapPath("/MrnFileUploads") + "\\" + Path.GetFileName(mrnMaster.MrnDetails[t].MrnFileUploads[h].FilePath);
                        string destination = HttpContext.Current.Server.MapPath("/PrFileUploads") + "\\" + Path.GetFileName(fileToCopy);
                        File.Copy(fileToCopy, destination, true);
                        mrnMaster.MrnDetails[t].MrnFileUploads[h].FilePath = "/PrFileUploads/" + Path.GetFileName(fileToCopy);
                    }
                    //Supportive Docs of MR Detail is copied to PR Detail
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrSupportiveDocs")))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrSupportiveDocs"));
                    for (int h = 0; h < mrnMaster.MrnDetails[t].MrnSupportiveDocuments.Count; ++h) {
                        string fileToCopy = HttpContext.Current.Server.MapPath("/MrnSupportiveDocs") + "\\" + Path.GetFileName(mrnMaster.MrnDetails[t].MrnSupportiveDocuments[h].FilePath);
                        string destination = HttpContext.Current.Server.MapPath("/PrSupportiveDocs") + "\\" + Path.GetFileName(fileToCopy);
                        File.Copy(fileToCopy, destination, true);
                        mrnMaster.MrnDetails[t].MrnSupportiveDocuments[h].FilePath = "/PrSupportiveDocs/" + Path.GetFileName(fileToCopy);
                    }                   
                }

                //Capex document of MR Master is copied to PR Master
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("/PrCapexDocs")))
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("/PrCapexDocs"));
                for (int h = 0; h < mrnMaster.MrnCapexDocs.Count; ++h)
                {
                    string fileToCopy = HttpContext.Current.Server.MapPath("/MrnCapexDocs") + "\\" + Path.GetFileName(mrnMaster.MrnCapexDocs[h].FilePath);
                    string destination = HttpContext.Current.Server.MapPath("/PrCapexDocs") + "\\" + Path.GetFileName(fileToCopy);
                    File.Copy(fileToCopy, destination, true);
                    mrnMaster.MrnCapexDocs[h].FilePath = "/PrCapexDocs/" + Path.GetFileName(fileToCopy);
                }

                string PRCode = mrnMasterDao.AddMRNtoPR(mrnMaster, userId, companyId, out PrId, dbConnection);
                return PRCode;
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

        public MrnMasterV2 FetchAllMrnByBasicSearchByMrnCode(string mrnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchAllMrnByBasicSearchByMrnCode(mrnId, dbConnection);
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
        public List<MrnMasterV2> FetchApprovedMrnByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchApprovedMrnByAdvanceSearch(departmentIds, wareHouseIds, purchaseType, purchaseProcedure, createdFromDate, createdToDate, expectedFromDate, expectedToDate, expenseType, mrnType, mainCatergoryId, subCatergoryId, dbConnection);
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
        public List<MrnMasterV2> FetchApprovedMrnByBasicSearchByMonth(DateTime date, List<int> warehouseId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchApprovedMrnByBasicSearchByMonth(date, warehouseId, dbConnection);
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

        public List<MrnMasterV2> FetchApprovedMrnByBasicSearch(List<int> warehouseId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchApprovedMrnByBasicSearch(warehouseId, dbConnection);
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
        public MrnMasterV2 FetchApprovedMrnByBasicSearchByMrnCode(string mrnId, List<int> warehouseId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchApprovedMrnByBasicSearchByMrnCode(mrnId, warehouseId, dbConnection);
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

        public List<MrnBomV2> GetMrndBomForEdit(int mrndId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                MrnBomDAOV2 mrnMasterDao = DAOFactory.CreateMrnBomDAOV2();
                return mrnMasterDao.GetMrndBomForEdit(mrndId, dbConnection);
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

        public List<MrnMasterV2> FetchMrnListforApprovalByDate(List<int> subDepartmentIds, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnListforApprovalByDate(subDepartmentIds, date, dbConnection);
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

        public List<MrnMasterV2> FetchMrnListforApprovalByMrnCode(List<int> subDepartmentIds, string MrnCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnListforApprovalByMrnCode(subDepartmentIds, MrnCode, dbConnection);
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
        public int updateMrnMasterAfterClone(int mrnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.updateMrnMasterAfterClone(mrnId, dbConnection);
            }
            catch (Exception Ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();
                }
            }
        }
        public MrnMasterV2 FetchMrnForExpAppByBasicSearchByMrnCode(string mrnId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnForExpAppByBasicSearchByMrnCode(mrnId, dbConnection);
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

        public List<MrnMasterV2> FetchMrnForExpAppByAdvanceSearch(List<int> departmentIds, List<int> wareHouseIds, int purchaseType, int purchaseProcedure, string createdFromDate, string createdToDate, string expectedFromDate, string expectedToDate, int expenseType, int mrnType, int mainCatergoryId, int subCatergoryId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnForExpAppByAdvanceSearch(departmentIds, wareHouseIds, purchaseType, purchaseProcedure, createdFromDate, createdToDate, expectedFromDate, expectedToDate, expenseType, mrnType, mainCatergoryId, subCatergoryId, dbConnection);
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

        public List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeperByDate(int companyId, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnByCompanyIdToAssignStoreKeeperByDate(companyId, date, dbConnection);
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

        public List<MrnMasterV2> FetchMrnByCompanyIdToAssignStoreKeeperByMrnCode(int companyId, string MrnCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnByCompanyIdToAssignStoreKeeperByMrnCode(companyId, MrnCode, dbConnection);
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
        public List<MrnMasterV2> FetchAssignedMrnForStoreKeeperByDate(int storekeeperId, DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchAssignedMrnForStoreKeeperByDate(storekeeperId, date, dbConnection);
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

        public List<MrnMasterV2> FetchAssignedMrnForStoreKeeper(int storekeeperId) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchAssignedMrnForStoreKeeper(storekeeperId, dbConnection);
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
        public List<MrnMasterV2> FetchMrnForExpAppByBasicSearchByMonth(DateTime date) {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnForExpAppByBasicSearchByMonth(date, dbConnection);
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

        public List<MrnMasterV2> FetchMrnForExpApp() {
            DBConnection dbConnection = new DBConnection();
            try {
                MrnMasterDAOV2 mrnMasterDao = DAOFactory.CreateMrnMasterDAOV2();
                return mrnMasterDao.FetchMrnForExpApp( dbConnection);
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
