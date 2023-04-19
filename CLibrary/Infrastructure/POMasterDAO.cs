using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CLibrary.Infrastructure
{
    public interface POMasterDAO
    {
        int SavePOMaster(int departmentid, int prId, int supplierId, DateTime createdDate, string createdBy, decimal vatAmount, decimal nbtAmount, string vatRegNo, string sVatRegNo, decimal totalAmount, int isApproved, string approvedBy, int isReceived, DateTime receivedDate, int BasePr, decimal totalCustomizedAmount, decimal totalCustomizedVat, decimal totalCustomizedNbt, string paymentmethod, DBConnection dbConnection);
        int SavePOMasterPO(int poId, string pocode, int departmentid, int supplierId, DateTime createdDate, string createdBy, decimal vatAmount, decimal nbtAmount, string vatRegNo, string sVatRegNo, decimal totalAmount, int isApproved, string approvedBy, int isReceived, DateTime receivedDate, int BasePr, decimal totalCustomizedAmount, decimal totalCustomizedVat, decimal totalCustomizedNbt, DBConnection dbConnection);
        int updatePODetails(int PoId, decimal vatAmount, decimal nbtAmount, decimal totalAmount, decimal customizedTotalAmount, decimal customizedVatAmount, decimal customizedNbtAmount, DBConnection dbConnection);
        List<POMaster> GetPoMasterListByDepartmentId(int departmentid, DBConnection dbConnection);
        POMaster GetPoMasterObjByPoId(int PoId, DBConnection dbConnection);
        int PoMasterApproval(int poId, int isApprove, int departmentid, DBConnection dbConnection);
        List<POMaster> GetPoMasterListByByDaterange(int departmentid, DateTime startdate, DateTime enddate, DBConnection dbConnection);
        int GetMaxPoNumebr(DBConnection dbConnection);
        int UpdateTotalAmounts(int PoId, decimal vatAmount, decimal nbtAmount, decimal totalAmount, decimal customizedVatAmount, decimal customizedNbtAmount, decimal customizedTotalAmount, DBConnection dbConnection);
        List<POMaster> GetPoMasterListByDepartmentIdViewPO(int departmentid, DBConnection dbConnection);
        List<POMaster> GetPoMasterRejectedListByDepartmentIdViewPO(int departmentid, DBConnection dbConnection);
        List<POMaster> GetModifiedPosForApproval(int CompanyId, int UserId, DBConnection dbConnection);

        POMaster GetPoMasterObjByPoIdView(int PoId, DBConnection dbConnection);

        //Grn Resources
        List<POMaster> GetPoMasterListByDepartmentIdToGRN(int departmentid, DBConnection dbConnection);
        List<POMaster> GetPoMasterListByDepartmentIdTogrn(int departmentid, DBConnection dbConnection);
        List<POMaster> GetPoMasterListByWarehouseIdTogrn(List<int> WarehouseIds, DBConnection dbConnection);
        POMaster GetPoMasterToGenerateGRN(int PoId, int CompanyId, DBConnection dbConnection);
        int GenerateCoveringPO(POMaster poMaster, DBConnection dbConnection);
        // POMaster GetPoMasterToViewPO(int PoId, int CompanyId, DBConnection dbConnection);


        int rejectPOMaster(int poId, DBConnection dbConnection);
        int updatePaymentMethodByPoId(int PoId, int departmentid, string paymentMethod, DBConnection dbConnection);
        List<POMaster> GetPoMasterListByDepartmentIdEditMode(int departmentid, DBConnection dbConnection);

        POMaster GetPoMasterObjByPoIdRaised(int PoId, int CompanyId, DBConnection dbConnection);

        List<POMaster> FetchApprovedPOForConfirmation(int Department, DBConnection dbConnection);
        int ConfirmOrDenyPOApproval(int poId, int confirm, DBConnection dbConnection);

        int SavePO(List<POMaster> PoMasters, int UserId, DBConnection dbConnection);
        int ApprovePOMaster(int poId, int userId, DBConnection dbConnection);


        //New Methods By Salman created on 2019-03-29
        List<POMaster> GetPoMastersForPrInquiryByQuotationId(int QuotationId, DBConnection dbConnection);


        List<int> GetPoCountForDashboard(int CompanyId, int yearsearch, int purchaseType, DBConnection dbConnection);
        List<POMaster> GetPoMasterListWithImport(int departmentid, DBConnection dbConnection);
        List<ItemPurchaseHistory> GetItemPurchaseHistories(int ItemId, DBConnection dbConnection);
        List<int> SavePONew(List<POMaster> PoMasters, int UserId, DBConnection dbConnection);
        List<POMaster> ViewAllPOS(int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType, DBConnection dbConnection, List<int> supplierIds = null);
        List<POMaster> ViewMyPOS(int UserId, int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType, DBConnection dbConnection);
        List<string> getPoDetailsByGrnId(int GrnId, DBConnection dbConnection);
        POMaster GetPoMasterToEditPO(int PoId, int CompanyId, DBConnection dbConnection);
        List<int> UpdatePO(POMaster poMaster, int UserId, DBConnection dbConnection);
        List<POMaster> GetPosForApproval(int CompanyId, int UserId, DBConnection dbConnection);
        POMaster GetPoMasterToViewPO(int PoId, int CompanyId, DBConnection dbConnection);
        int ParentApprovePO(int PoId, string Remarks, int PaymentMethod, int POType, int UserId, int IsParentApproved, string PoRemark, DBConnection dbConnection);
        int ApproveGeneralPO(int PoId, int UserId, string Remarks, int PaymentMethod, string PoRemark, DBConnection dbConnection);
        //int RejectPO(int PoId, int UserId, string Remarks, int PaymentMethod, int RejectionAction, int ParentPoId, DBConnection dbConnection);
        int RejectGeneralPO(int PoId, int UserId, string Remarks, int PaymentMethod, DBConnection dbConnection);
        int ParentRejectPO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, int RejectionAction, int ParentPOId, DBConnection dbConnection);
        int UpdatePrintCount(int PoId, DBConnection dbConnection);
        List<POMaster> GetAllPosByPrId(int PrId, DBConnection dbConnection);

        //Get All PO MASTER LIST
        List<POMaster> GetAllPOMAster(DBConnection dbConnection);
        POMaster GetPoMasterObjByPoIdNew(int GrnId, DBConnection dbConnection);
        int CancelPo(int Poid, DBConnection dbConnection);
        List<POMaster> ViewCancelledPOS(int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType, DBConnection dbConnection);
        List<POMaster> GetAllPosByPrIdFor(List<int> PrId, DBConnection dbConnection);
        int UpdatePoEmailStatus(int poId, DBConnection dbConnection);
        List<POMaster> GetAPPROVEDPosByPrId(int PrId, DBConnection dbConnection);
        int CheckPoGrns(int PoId, DBConnection dbConnection);
        List<POMaster> GetPosForPrint(int CompanyId, int UserId, DBConnection dbConnection);
        List<POMaster> GetPosForInvoice(int CompanyId, int UserId, DBConnection dbConnection);
        int CreateCoveringPR(int PoId, int PrId, int grnId, int UserId, DBConnection dBConnection);
        int GetPoId(int PrId, DBConnection dbConnection);
        int ApprovedCoveringPOCount(int PrId, DBConnection dbConnection);
    }

    public class POMasterDAOSQLImpl : POMasterDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<POMaster> GetPoMasterListByByDaterange(int departmentid, DateTime startdate, DateTime enddate, DBConnection dbConnection)
        {
            List<POMaster> GetPoMasterList = new List<POMaster>();
            PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY ,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.IS_APPROVED,PM.REQUIRED_FOR  " +
                                            " FROM " + dbLibrary + ".PO_MASTER  AS POM " +
                                           "INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           //"LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PM.MRNREFERENCE_NO = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           //"LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS CREATED_BY FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON POM.CREATED_BY=CL.USER_ID\n" +
                                           " WHERE POM.DEPARTMENT_ID =" + departmentid + " AND  ( POM.CREATED_DATE BETWEEN  '" + startdate + "' AND  '" + enddate + "')" +
                                           " AND POD.IS_PO_RAISED =1" +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY ,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,PM.REQUIRED_FOR";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoMasterList = dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
            foreach (var item in GetPoMasterList)
            {
                item._PODetails = pODetailsDAO.GetPoDetailsByPoId(item.PoID, dbConnection);
            }
            return GetPoMasterList;
        }

        //Get All PO MASTER LIST
        public List<POMaster> GetAllPOMAster(DBConnection dbConnection)
        {
            POMaster GetPoMasterObj = new POMaster();
            PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT *,b.PR_TYPE,b.PURCHASE_TYPE FROM PO_MASTER a " +
                "INNER JOIN PR_MASTER b ON a.BASED_PR=b.PR_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);

            }

        }
        public List<POMaster> GetPoMasterListByDepartmentId(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY ,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.IS_APPROVED,SD.DEPARTMENT_NAME,PM.REQUIRED_FOR " +
                                           " FROM " + dbLibrary + ".PO_MASTER  AS POM " +
                                           "INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PM.MRN_ID = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS CREATED_BY FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON POM.CREATED_BY=CL.USER_ID\n" +
                                           " WHERE POM.DEPARTMENT_ID =" + departmentid + "  " +
                                           " AND POD.IS_PO_RAISED =1" +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY ,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,SD.DEPARTMENT_NAME,PM.REQUIRED_FOR";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }
        public POMaster GetPoMasterObjByPoIdNew(int GrnId, DBConnection dbConnection)
        {
            POMaster GetPoMasterObj = new POMaster();
            PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PO_GRN AS PG " +
                "LEFT JOIN PO_MASTER AS POM ON POM.PO_ID = PG.PO_ID " +
                "INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS STORE_KEEPER FROM COMPANY_LOGIN) AS SK ON SK.USER_ID = PM.CREATED_BY\n" +
                " WHERE GRN_ID =" + GrnId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoMasterObj = dataAccessObject.GetSingleOject<POMaster>(dbConnection.dr);

            }
            //GetPoMasterObj._PODetails = pODetailsDAO.GetPoDetailsByPoId(PoId, dbConnection);
            return GetPoMasterObj;
        }


        public POMaster GetPoMasterObjByPoId(int PoId, DBConnection dbConnection)
        {
            POMaster GetPoMasterObj = new POMaster();
            PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PO_MASTER  WHERE PO_ID =" + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoMasterObj = dataAccessObject.GetSingleOject<POMaster>(dbConnection.dr);

            }
            GetPoMasterObj._PODetails = pODetailsDAO.GetPoDetailsByPoId(PoId, dbConnection);
            return GetPoMasterObj;
        }

        public POMaster GetPoMasterObjByPoIdRaised(int PoId, int CompanyId, DBConnection dbConnection)
        {
            try
            {
                POMaster GetPoMasterObj = new POMaster();
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT POM.*,PM.IMPORT_ITEM_TYPE,PM.CLONED_FROM_PR, CL.STORE_KEEPER_NAME,AD.APPROVED_DESIGNATION_NAME, APD.CREATED_DESIGNATION_NAME, PD.PARENT_APPROVED_DESIGNATION_NAME, PM.PURCHASE_TYPE,PM.PURCHASE_PROCEDURE, SUB.DEPARTMENT_NAME, SK.STORE_KEEPER, PM.REQUIRED_FOR, CLA.*,CLC.* ,CLPA.*  FROM " + dbLibrary + ".PO_MASTER POM " +
                                                "INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                                 "LEFT JOIN (SELECT MRN_ID, SUB_DEPARTMENT_ID FROM MRN_MASTER) AS MRN ON PM.MRN_ID =MRN.MRN_ID  " +
                                                 "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUB ON SUB.SUB_DEPARTMENT_ID = MRN.SUB_DEPARTMENT_ID " +
                                                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, USER_ID,FIRST_NAME AS STORE_KEEPER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN ) AS CL ON PM.STORE_KEEPER_ID = CL.USER_ID\n" +
                                                "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_USER_NAME, DESIGNATION_ID AS APPROVED_DESIGNATION_ID, DIGITAL_SIGNATURE AS APPROVED_SIGNATURE FROM COMPANY_LOGIN) AS CLA ON POM.APPROVED_BY = CLA.USER_ID\n" +
                                                "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME, DESIGNATION_ID AS CREATED_DESIGNATION_ID, DIGITAL_SIGNATURE AS CREATED_SIGNATURE FROM COMPANY_LOGIN) AS CLC ON POM.CREATED_BY = CLC.USER_ID\n" +
                                                "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS PARENT_APPROVED_USER_NAME,DESIGNATION_ID AS PARENT_APPROVED_DESIGNATION_ID, DIGITAL_SIGNATURE AS PARENT_APPROVED_USER_SIGNATURE FROM COMPANY_LOGIN) AS CLPA ON POM.PARENT_APPROVED_USER = CLPA.USER_ID\n" +
                                               "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS STORE_KEEPER FROM COMPANY_LOGIN) AS SK ON SK.USER_ID = PM.CREATED_BY\n" +
                                               "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS APPROVED_DESIGNATION_NAME FROM DESIGNATION ) AS AD ON AD.DESIGNATION_ID = CLA.APPROVED_DESIGNATION_ID \n" +
                                               "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS CREATED_DESIGNATION_NAME FROM DESIGNATION ) AS APD ON APD.DESIGNATION_ID = CLC.CREATED_DESIGNATION_ID \n" +
                                               "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS PARENT_APPROVED_DESIGNATION_NAME FROM DESIGNATION ) AS PD ON PD.DESIGNATION_ID = CLPA.PARENT_APPROVED_DESIGNATION_ID \n" +
                                               "LEFT JOIN PO_MASTER AS POMD ON POM.IS_DERIVED_FROM_PO = POMD.PO_ID \n" +
                                                "WHERE POM.PO_ID =" + PoId;
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
                {
                    DataAccessObject dataAccessObject = new DataAccessObject();
                    GetPoMasterObj = dataAccessObject.GetSingleOject<POMaster>(dbConnection.dr);

                }
                GetPoMasterObj._PODetails = pODetailsDAO.GetPODetailsToViewPo(PoId, CompanyId, dbConnection);
                GetPoMasterObj.DerivedPOs = GetDerrivedPOs(PoId, dbConnection);
                if (GetPoMasterObj.IsDerived == 1)
                {
                    GetPoMasterObj.DerivedFromPOs = GetDerrivedFromPOs(GetPoMasterObj.IsDerivedFromPo, dbConnection);
                }
                GetPoMasterObj.GeneratedGRNs = DAOFactory.createGrnDAO().GetGeneratedGRNsForPo(PoId, dbConnection);
                return GetPoMasterObj;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public POMaster GetPoMasterObjByPoIdView(int PoId, DBConnection dbConnection)
        {
            POMaster GetPoMasterObj = new POMaster();
            PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PO_MASTER AS PM " +
                                            "INNER JOIN (SELECT USER_ID, USER_NAME,FIRST_NAME AS APPROVED_USER_NAME FROM COMPANY_LOGIN) AS CL ON CONVERT(INT, PM.APPROVED_BY) = CL.USER_ID " +
                                            "INNER JOIN (SELECT STORE_KEEPER_ID,PR_ID  FROM PR_MASTER) AS PRM ON PRM.PR_ID = PM.BASED_PR " +
                                            "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, USER_ID,FIRST_NAME AS STORE_KEEPER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN ) AS SK ON PRM.STORE_KEEPER_ID = SK.USER_ID\n" +
                                            "WHERE PM.PO_ID =" + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoMasterObj = dataAccessObject.GetSingleOject<POMaster>(dbConnection.dr);

            }
            GetPoMasterObj._PODetails = pODetailsDAO.GetPoDetailsByPoIdIsApproveTrue(PoId, dbConnection);
            return GetPoMasterObj;
        }

        public int PoMasterApproval(int poId, int isApprove, int departmentid, DBConnection dbConnection)
        {
            string POCode = string.Empty;
            POCode = "PO" + poId;


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET   PO_CODE='" + POCode + "', IS_APPROVED = " + isApprove + " , APPROVED_BY  = '" + LocalTime.Now + "'  WHERE PO_ID = " + poId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdatePoEmailStatus(int poId, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET PO_EMAIL_STATUS = 1 WHERE PO_ID = " + poId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SavePOMaster(int departmentid, int prId, int supplierId, DateTime createdDate, string createdBy, decimal vatAmount, decimal nbtAmount, string vatRegNo, string sVatRegNo, decimal totalAmount, int isApproved, string approvedBy, int isReceived, DateTime receivedDate, int BasePr, decimal totalCustomizedAmount, decimal totalCustomizedVat, decimal totalCustomizedNbt, string paymentmethod, DBConnection dbConnection)
        {
            int POId = 0;
            string POCode = string.Empty;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PO_MASTER";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                POId = 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (PO_ID) + 1 AS MAXid FROM " + dbLibrary + ".PO_MASTER";
                POId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PO_MASTER (PO_ID, PO_CODE, DEPARTMENT_ID, SUPPLIER_ID, CREATED_DATE, CREATED_BY,  VAT_AMOUNT, NBT_AMOUNT, VAT_REG_NO, SVAT_REG_NO, TOTAL_AMOUNT, IS_APPROVED, APPROVED_BY, IS_RECEIVED,IS_RECEIVED_DATE,BASED_PR, TOTAL_CUSTOMIZED_AMOUNT, TOTAL_CUSTOMIZED_VAT, TOTAL_CUSTOMIZED_NBT ,PAYMENT_METHOD) VALUES ( " + POId + ", '" + "" + "" + "' , " + departmentid + "," + supplierId + ", '" + createdDate + "', '" + createdBy + "', " + vatAmount + ", " + nbtAmount + ", '" + vatRegNo + "', '" + sVatRegNo + "', " + totalAmount + ", " + isApproved + ", '" + approvedBy + "', " + isReceived + ", '" + receivedDate + "'," + BasePr + ", " + 0 + ", " + 0 + ", " + 0 + ",'" + paymentmethod + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();

            return POId;
        }

        public int SavePOMasterPO(int poId, string pocode, int departmentid, int supplierId, DateTime createdDate, string createdBy, decimal vatAmount, decimal nbtAmount, string vatRegNo, string sVatRegNo, decimal totalAmount, int isApproved, string approvedBy, int isReceived, DateTime receivedDate, int BasePr, decimal totalCustomizedAmount, decimal totalCustomizedVat, decimal totalCustomizedNbt, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PO_MASTER (PO_CODE, DEPARTMENT_ID, SUPPLIER_ID, CREATED_DATE, CREATED_BY,  VAT_AMOUNT, NBT_AMOUNT, VAT_REG_NO, SVAT_REG_NO, TOTAL_AMOUNT, IS_APPROVED, APPROVED_BY, IS_RECEIVED,IS_RECEIVED_DATE,BASED_PR, TOTAL_CUSTOMIZED_AMOUNT, TOTAL_CUSTOMIZED_VAT, TOTAL_CUSTOMIZED_NBT) VALUES ('" + pocode + "" + "' , " + departmentid + "," + supplierId + ", '" + createdDate + "', '" + createdBy + "', " + vatAmount + ", " + nbtAmount + ", '" + vatRegNo + "', '" + sVatRegNo + "', " + totalAmount + ", " + isApproved + ", '" + approvedBy + "', " + isReceived + ", '" + receivedDate + "'," + BasePr + "," + totalCustomizedAmount + " ," + totalCustomizedVat + ", " + totalCustomizedNbt + " );";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        //public int GetPoId(int PrId, DBConnection dbConnection) {
        //    dbConnection.cmd.Parameters.Clear();
        //    dbConnection.cmd.CommandText = "SELECT PO_ID FROM PO_MASTER WHERE BASED_PR = "+ PrId + " ";
        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
        //    int Id = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        //    return Id;
        //}

        public int GetPoId(int PrId, DBConnection dbConnection)
        {
            int id = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM PO_MASTER WHERE BASED_PR = " + PrId + " ";
            int count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count > 0)
            {
                dbConnection.cmd.CommandText = "SELECT PO_ID FROM PO_MASTER WHERE BASED_PR = " + PrId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                id = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            return id;
        }


        public int ApprovedCoveringPOCount(int PrId, DBConnection dbConnection)
        {
            int count = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM PO_MASTER WHERE BASED_PR = " + PrId + " AND IS_APPROVED = 0 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            return count;
        }

        public int CancelPo(int Poid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE PO_MASTER SET IS_CANCELLED = 1 WHERE PO_ID = " + Poid + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updatePODetails(int PoId, decimal vatAmount, decimal nbtAmount, decimal totalAmount, decimal customizedTotalAmount, decimal customizedVatAmount, decimal customizedNbtAmount, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET VAT_AMOUNT = " + vatAmount + " , NBT_AMOUNT  = " + nbtAmount + " , TOTAL_AMOUNT  = " + totalAmount + ", TOTAL_CUSTOMIZED_AMOUNT = " + customizedTotalAmount + ", TOTAL_CUSTOMIZED_VAT = " + customizedVatAmount + ",TOTAL_CUSTOMIZED_NBT=" + customizedNbtAmount + "  WHERE PO_ID = " + PoId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetMaxPoNumebr(DBConnection dbConnection)
        {
            int POId = 0;
            string POCode = string.Empty;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PO_MASTER";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                POId = 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (PO_ID)+1 AS MAXid FROM " + dbLibrary + ".PO_MASTER";
                POId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            return POId;
        }

        public int UpdateTotalAmounts(int PoId, decimal vatAmount, decimal nbtAmount, decimal totalAmount, decimal customizedVatAmount, decimal customizedNbtAmount, decimal customizedTotalAmount, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET VAT_AMOUNT = " + vatAmount + ",NBT_AMOUNT = " + nbtAmount + ",TOTAL_AMOUNT=" + totalAmount + ",TOTAL_CUSTOMIZED_VAT=" + customizedVatAmount + ", TOTAL_CUSTOMIZED_NBT=" + customizedNbtAmount + ", TOTAL_CUSTOMIZED_AMOUNT=" + customizedTotalAmount + "   WHERE PO_ID = " + PoId + " ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<POMaster> GetPoMasterListByDepartmentIdViewPO(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY,PM.PR_CODE,PM.PURCHASE_TYPE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.PO_EMAIL_STATUS, " +
                                           " POM.VAT_AMOUNT,POM.NBT_AMOUNT,POM.TOTAL_AMOUNT,POM.TOTAL_CUSTOMIZED_VAT,POM.TOTAL_CUSTOMIZED_VAT,POM.TOTAL_CUSTOMIZED_NBT ,SD.DEPARTMENT_NAME,PM.REQUIRED_FOR" +
                                           " FROM " + dbLibrary + ".PO_MASTER AS POM " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PM.MRN_ID = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                           "INNER JOIN (SELECT USER_ID,USER_NAME AS CREATED_BY FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON POM.CREATED_BY=CL.USER_ID\n" +
                                           " WHERE POM.DEPARTMENT_ID =" + departmentid + " AND (POM.IS_CANCELLED = 0 OR POM.IS_CANCELLED IS NULL)  " +
                                           " AND POD.IS_PO_RAISED =1 AND POM.IS_APPROVED =1 " +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,PM.PURCHASE_TYPE,POM.CREATED_DATE,CL.CREATED_BY,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,POM.PO_EMAIL_STATUS, POM.VAT_AMOUNT,POM.NBT_AMOUNT,POM.TOTAL_AMOUNT,POM.TOTAL_CUSTOMIZED_VAT,POM.TOTAL_CUSTOMIZED_VAT,POM.TOTAL_CUSTOMIZED_NBT,SD.DEPARTMENT_NAME,PM.REQUIRED_FOR ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }
        public List<POMaster> GetPoMasterRejectedListByDepartmentIdViewPO(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, " +
                                           " POM.VAT_AMOUNT,POM.NBT_AMOUNT,POM.TOTAL_AMOUNT,POM.TOTAL_CUSTOMIZED_VAT,POM.TOTAL_CUSTOMIZED_VAT,POM.TOTAL_CUSTOMIZED_NBT " +
                                           " FROM " + dbLibrary + ".PO_MASTER AS POM " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                           " WHERE POM.DEPARTMENT_ID =" + departmentid + "  " +
                                           " AND POD.IS_PO_RAISED =0 AND POD.IS_PO_APPROVED =2 " +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,POM.VAT_AMOUNT,POM.NBT_AMOUNT,POM.TOTAL_AMOUNT,POM.TOTAL_CUSTOMIZED_VAT,POM.TOTAL_CUSTOMIZED_VAT,POM.TOTAL_CUSTOMIZED_NBT ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> GetPoMasterListByDepartmentIdToGRN(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT POD.QUANTITY,POM.PO_CODE,POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, SU.SUPPLIER_NAME,SD.DEPARTMENT_NAME,PM.REQUIRED_FOR " +
                                           " FROM " + dbLibrary + ".PO_MASTER AS POM " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PM.MRNREFERENCE_NO = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                           " WHERE POM.DEPARTMENT_ID =" + departmentid + "  " +
                                           " AND POD.IS_PO_RAISED =1 AND POD.IS_PO_APPROVED =1 " +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,POD.QUANTITY,SD.DEPARTMENT_NAME,PM.REQUIRED_FOR ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public int rejectPOMaster(int poId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET  IS_APPROVED = 2   WHERE PO_ID = " + poId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ApprovePOMaster(int poId, int UserId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            string sql = "UPDATE " + dbLibrary + ".PO_MASTER SET  IS_APPROVED = 1, APPROVED_BY=" + UserId + ", APPROVED_DATE = '" + LocalTime.Now + "' WHERE PO_ID = " + poId + "; ";

            sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 9 WHERE PRD_ID IN(" +
                    "SELECT PRD_ID FROM BIDDING_ITEM WHERE BID_ID IN(" +
                    "SELECT BID_ID FROM SUPPLIER_QUOTATION WHERE QUOTATION_ID = (" +
                    "SELECT QUOTATION_ID FROM PO_MASTER WHERE PO_ID = " + poId + "))); ";

            sql += "INSERT INTO PR_DETAIL_STATUS_LOG " +
                    "SELECT PRD_ID,12,'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BID_ID IN(" +
                    "SELECT BID_ID FROM SUPPLIER_QUOTATION WHERE QUOTATION_ID = (" +
                    "SELECT QUOTATION_ID FROM PO_MASTER WHERE PO_ID = " + poId + ")); ";

            dbConnection.cmd.CommandText = sql;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updatePaymentMethodByPoId(int PoId, int departmentid, string paymentMethod, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET  PAYMENT_METHOD ='" + paymentMethod + "' WHERE PO_ID = " + PoId + " AND DEPARTMENT_ID =" + departmentid + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<POMaster> GetPoMasterListByDepartmentIdEditMode(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT " +
                                           " FROM " + dbLibrary + ".PO_MASTER AS POM " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                           " WHERE POM.DEPARTMENT_ID =" + departmentid + "  " +
                                           " AND POD.IS_PO_RAISED =0  AND POD.IS_PO_EDIT_MODE=1 " +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> FetchApprovedPOForConfirmation(int Department, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.IS_APPROVED,POM.PO_IS_CONFIRMED_APPROVAL" +
                                           " FROM " + dbLibrary + ".PO_MASTER  AS POM " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                           " WHERE POM.DEPARTMENT_ID =" + Department + "  " +
                                           " AND POM.PO_IS_CONFIRMED_APPROVAL =0" +
                                           " AND POM.IS_APPROVED =1" +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,POM.PO_IS_CONFIRMED_APPROVAL";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public int ConfirmOrDenyPOApproval(int poId, int confirm, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET PO_IS_CONFIRMED_APPROVAL=" + confirm + " WHERE PO_ID=" + poId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int SavePO(List<POMaster> PoMasters, int UserId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DECLARE @PoIdTable TABLE (POID INT)");
            for (int i = 0; i < PoMasters.Count; i++)
            {


                sql.AppendLine("INSERT INTO PO_MASTER ([PO_CODE],[DEPARTMENT_ID],[SUPPLIER_ID],[CREATED_DATE],[CREATED_BY],[VAT_AMOUNT],[NBT_AMOUNT],[TOTAL_AMOUNT],[BASED_PR],[QUOTATION_ID],[QUOTATION_APPROVED_BY],[QUOTATION_CONFIRMED_BY])");
                sql.AppendLine("OUTPUT INSERTED.PO_ID INTO @PoIdTable(POID)");
                sql.AppendLine("VALUES ((SELECT 'PO'+CONVERT(VARCHAR,COUNT (PO_ID) + 1) FROM PO_MASTER WHERE DEPARTMENT_ID= " + PoMasters[i].DepartmentId + ")," + PoMasters[i].DepartmentId + "," + PoMasters[i].SupplierId + ",'" + LocalTime.Now + "','" + PoMasters[i].CreatedBy + "'," + PoMasters[i].VatAmount + "," + PoMasters[i].NBTAmount + "," + PoMasters[i].TotalAmount + "," + PoMasters[i].BasePr + "," + PoMasters[i].QuotationId + "," + PoMasters[i].QuotationApprovedBy + "," + PoMasters[i].QuotationConfirmedBy + ");");

                for (int j = 0; j < PoMasters[i].PoDetails.Count; j++)
                {
                    sql.AppendLine("INSERT INTO PO_DETAILS ([PO_ID],[QUOTATION_ITEM_ID],[ITEM_PRICE],[QUANTITY],[TOTAL_AMOUNT],[VAT_AMOUNT],[NBT_AMOUNT],[ITEM_ID],[BASED_PO],[IS_PO_EDIT_MODE],[TABULATION_ID],[TABULATION_DETAIL_ID])");
                    sql.AppendLine("VALUES ((SELECT MAX(POID) FROM @PoIdTable)," + PoMasters[i].PoDetails[j].QuotationItemId + "," + PoMasters[i].PoDetails[j].ItemPrice + "," + PoMasters[i].PoDetails[j].Quantity + "," + PoMasters[i].PoDetails[j].TotalAmount + "," + PoMasters[i].PoDetails[j].VatAmount + "," + PoMasters[i].PoDetails[j].NbtAmount + "," + PoMasters[i].PoDetails[j].ItemId + ",(SELECT MAX(POID) FROM @PoIdTable),1," + PoMasters[i].PoDetails[j].TabulationId + "," + PoMasters[i].PoDetails[j].TabulationDetailId + ");");

                    sql.AppendLine("UPDATE PR_DETAIL SET IS_PO_RAISED = 1 WHERE PR_ID = " + PoMasters[i].BasePr + " AND ITEM_ID =" + PoMasters[i].PoDetails[j].ItemId + " AND IS_ACTIVE = 1;");

                    sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= 8 WHERE PR_ID = " + PoMasters[i].BasePr + " AND ITEM_ID =" + PoMasters[i].PoDetails[j].ItemId);

                    //Inserting PR_DETAIL Status Update log
                    sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
                    sql.AppendLine("SELECT PRD_ID,11,'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL WHERE PR_ID = " + PoMasters[i].BasePr + " AND ITEM_ID =" + PoMasters[i].PoDetails[j].ItemId);


                    sql.AppendLine("UPDATE TABULATION_DETAIL SET IS_ADDED_TO_PO= 1 WHERE TABULATION_DETAIL_ID = " + PoMasters[i].PoDetails[j].TabulationDetailId);

                }
            }
            sql.AppendLine();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }

        public List<POMaster> GetPoMastersForPrInquiryByQuotationId(int QuotationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT * FROM PO_MASTER AS POM " +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON POM.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "INNER JOIN (SELECT USER_ID,FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CL ON POM.CREATED_BY=CL.USER_ID\n" +
                "WHERE POM.QUOTATION_ID=" + QuotationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<int> GetPoCountForDashboard(int CompanyId, int yearsearch, int purchaseType, DBConnection dbConnection)
        {
            List<int> count = new List<int>();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PO_ID) FROM PO_MASTER inner join PR_MASTER on PO_MASTER.BASED_PR=PR_MASTER.PR_ID WHERE PR_MASTER.PURCHASE_TYPE=" + purchaseType + " AND (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ") AND PO_MASTER.DEPARTMENT_ID=" + CompanyId;

            count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PO_ID) FROM PO_MASTER inner join PR_MASTER on PO_MASTER.BASED_PR=PR_MASTER.PR_ID WHERE PR_MASTER.PURCHASE_TYPE=" + purchaseType + " AND  (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ") AND IS_APPROVED =0 AND PO_MASTER.DEPARTMENT_ID=" + CompanyId;

            count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PO_ID) FROM PO_MASTER inner join PR_MASTER on PO_MASTER.BASED_PR=PR_MASTER.PR_ID WHERE PR_MASTER.PURCHASE_TYPE=" + purchaseType + " AND  (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ") AND IS_APPROVED =1 AND PO_MASTER.DEPARTMENT_ID=" + CompanyId;

            count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(PO_ID) FROM PO_MASTER inner join PR_MASTER on PO_MASTER.BASED_PR=PR_MASTER.PR_ID WHERE PR_MASTER.PURCHASE_TYPE=" + purchaseType + " AND  (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ") AND IS_APPROVED =2 AND PO_MASTER.DEPARTMENT_ID=" + CompanyId;

            count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));


            return count;
        }

        public List<POMaster> GetPoMasterListWithImport(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY ,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.IS_APPROVED,SD.DEPARTMENT_NAME,PM.REQUIRED_FOR " +
                                           " FROM " + dbLibrary + ".PO_MASTER  AS POM " +
                                           "INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PM.MRN_ID = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS CREATED_BY FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON POM.CREATED_BY=CL.USER_ID\n" +
                                           " WHERE POM.DEPARTMENT_ID =" + departmentid + "  " +
                                           " AND POD.IS_PO_RAISED =1 AND  PM.PURCHASE_TYPE = '2'" +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY ,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,SD.DEPARTMENT_NAME,PM.REQUIRED_FOR";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<ItemPurchaseHistory> GetItemPurchaseHistories(int ItemId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT TOP 30 POM.PO_ID,POM.PO_CODE, POM.CREATED_DATE,POD.ITEM_PRICE,POD.QUANTITY, MED.SHORT_CODE FROM PO_MASTER AS POM \n");
            sql.Append("INNER JOIN PO_DETAILS AS POD ON POM.PO_ID = POD.PO_ID \n");
            sql.Append("LEFT JOIN MEASUREMENT_DETAIL AS MED ON MED.DETAIL_ID = POD.MEASUREMENT_ID \n");
            sql.Append("WHERE POM.IS_APPROVED=1 AND ITEM_ID=" + ItemId + " \n");
            sql.Append("ORDER BY POM.CREATED_DATE DESC");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemPurchaseHistory>(dbConnection.dr);
            }
        }

        public List<int> SavePONew(List<POMaster> PoMasters, int UserId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DECLARE @PoIdTable TABLE (POID INT)");
            sql.AppendLine("DECLARE @PoCodeTable TABLE (POCode INT)");
            for (int i = 0; i < PoMasters.Count; i++)
            {
                sql.AppendLine("INSERT INTO @PoCodeTable");
                sql.AppendLine("SELECT COUNT(PO_ID)+1 FROM PO_MASTER WHERE DEPARTMENT_ID= " + PoMasters[i].DepartmentId + ";");

                sql.AppendLine("INSERT INTO PO_MASTER ([PO_CODE],[DEPARTMENT_ID],[SUPPLIER_ID],[CREATED_DATE],[CREATED_BY],[VAT_AMOUNT],[NBT_AMOUNT],[TOTAL_AMOUNT],[BASED_PR],[DELIVER_TO_WAREHOUSE],[IS_CURRENT],[QUOTATION_ID],[QUOTATION_APPROVED_BY],[QUOTATION_CONFIRMED_BY], [PRINT_COUNT], [REMARKS])");
                sql.AppendLine("OUTPUT INSERTED.PO_ID INTO @PoIdTable(POID)");
                sql.AppendLine("VALUES ((SELECT 'PO'+CONVERT(VARCHAR,MAX (POCode)) FROM @PoCodeTable)," + PoMasters[i].DepartmentId + "," + PoMasters[i].SupplierId + ",'" + LocalTime.Now + "','" + PoMasters[i].CreatedBy + "'," + PoMasters[i].VatAmount + "," + PoMasters[i].NBTAmount + "," + PoMasters[i].TotalAmount + "," + PoMasters[i].BasePr + "," + PoMasters[i].DeliverToWarehouse + ", 1, " + PoMasters[i].QuotationId + "," + PoMasters[i].QuotationApprovedBy + "," + PoMasters[i].QuotationConfirmedBy + ",0, '" + PoMasters[i].Remarks + "');");

                for (int j = 0; j < PoMasters[i].PoDetails.Count; j++)
                {
                    sql.AppendLine("INSERT INTO PO_DETAILS ([PO_ID],[QUOTATION_ITEM_ID],[ITEM_PRICE],[QUANTITY],[TOTAL_AMOUNT],[VAT_AMOUNT],[NBT_AMOUNT],[ITEM_ID],[BASED_PO],[IS_PO_EDIT_MODE],[HAS_NBT],[NBT_CALCULATION_TYPE],[HAS_VAT],[TABULATION_ID],[TABULATION_DETAIL_ID], [MEASUREMENT_ID],[RECEIVED_QTY], [STATUS], [SUPPLIER_MENTIONED_ITEM_NAME], [PO_PURCHASE_TYPE])");
                    sql.AppendLine("VALUES ((SELECT MAX(POID) FROM @PoIdTable)," + PoMasters[i].PoDetails[j].QuotationItemId + "," + PoMasters[i].PoDetails[j].ItemPrice + "," + PoMasters[i].PoDetails[j].Quantity + "," + PoMasters[i].PoDetails[j].TotalAmount + "," + PoMasters[i].PoDetails[j].VatAmount + "," + PoMasters[i].PoDetails[j].NbtAmount + "," + PoMasters[i].PoDetails[j].ItemId + ",(SELECT MAX(POID) FROM @PoIdTable),1," + PoMasters[i].PoDetails[j].HasNbt + "," + PoMasters[i].PoDetails[j].NbtCalculationType + "," + PoMasters[i].PoDetails[j].HasVat + "," + PoMasters[i].PoDetails[j].TabulationId + "," + PoMasters[i].PoDetails[j].TabulationDetailId + ", " + PoMasters[i].PoDetails[j].MeasurementId + ", 0, 0, '" + PoMasters[i].PoDetails[j].SupplierMentionedItemName + "', " + PoMasters[i].PoDetails[j].PoPurchaseType + ");");

                    sql.AppendLine("UPDATE PR_DETAIL SET IS_PO_RAISED = 1 WHERE PR_ID = " + PoMasters[i].BasePr + " AND ITEM_ID =" + PoMasters[i].PoDetails[j].ItemId + " AND IS_ACTIVE = 1;");

                    sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='APPRV_PO') WHERE PR_ID = " + PoMasters[i].BasePr + " AND ITEM_ID =" + PoMasters[i].PoDetails[j].ItemId);

                    //Inserting PR_DETAIL Status Update log
                    sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
                    sql.AppendLine("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='CRTD_PO'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL WHERE PR_ID = " + PoMasters[i].BasePr + " AND ITEM_ID =" + PoMasters[i].PoDetails[j].ItemId);


                    sql.AppendLine("UPDATE TABULATION_DETAIL SET IS_ADDED_TO_PO= 1 WHERE TABULATION_DETAIL_ID = " + PoMasters[i].PoDetails[j].TabulationDetailId);

                }
            }
            sql.AppendLine("");
            sql.AppendLine("SELECT * FROM @PoCodeTable");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                List<int> PoCodes = new List<int>();

                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        PoCodes.Add(int.Parse(dbConnection.dr[0].ToString()));
                    }
                }

                return PoCodes;
            }
        }
        public List<POMaster> ViewAllPOS(int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType, DBConnection dbConnection, List<int> supplierIds = null)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT POM.*,CLC.FIRST_NAME AS CREATED_USER_NAME,CLA.FIRST_NAME AS APPROVED_USER_NAME,W.LOCATION AS WAREHOUSE_NAME,S.SUPPLIER_NAME,PRM.PR_CODE, PRM.REQUIRED_FOR,PRM.PURCHASE_TYPE,PRM.PURCHASE_PROCEDURE, POMD.PO_CODE AS PARENT_PO_CODE FROM PO_MASTER AS POM \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLC ON POM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLA ON POM.APPROVED_BY = CLA.USER_ID \n");
            sql.Append("LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n");
            sql.Append("LEFT JOIN PO_MASTER AS POMD ON POM.IS_DERIVED_FROM_PO = POMD.PO_ID \n");
            sql.Append("INNER JOIN SUPPLIER AS S ON POM.SUPPLIER_ID = S.SUPPLIER_ID \n");
            sql.Append("INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n");
            sql.Append("LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME  FROM ITEM_CATEGORY_MASTER) AS ICM ON PRM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n");
            sql.Append("WHERE POM.DEPARTMENT_ID=" + CompanyId + " \n");

            if (prcode != "")
            {
                sql.Append(" AND PRM.PR_CODE =  '" + prcode + "'");
            }

            if (pocode != "")
            {
                sql.Append(" AND POM.PO_CODE =  '" + pocode + "'");
            }

            if (date != DateTime.MinValue)
            {
                sql.Append(" AND MONTH(POM.CREATED_DATE)= " + date.Month.ToString() + " AND YEAR(POM.CREATED_DATE)= " + date.Year.ToString() + " ");
            }

            if (caregoryIds.Count > 0)
            {
                sql.Append(" AND ICM.CATEGORY_ID IN (" + string.Join(",", caregoryIds) + ")");
            }


            if (warehouseIds.Count > 0)
            {
                if (warehouseIds.Any(wi => wi == 0))
                {
                    if (warehouseIds.Count == 1)
                    {
                        sql.Append(" AND W.WAREHOUSE_ID IS NULL ");
                    }
                    else
                    {
                        sql.Append(" AND (W.WAREHOUSE_ID IN  (" + string.Join(",", warehouseIds.Where(wi => wi != 0)) + ") OR W.WAREHOUSE_ID IS NULL) ");
                    }
                }
                else
                {
                    sql.Append(" AND W.WAREHOUSE_ID IN  (" + string.Join(",", warehouseIds) + ") ");
                }
            }

            if (poType != 0)
            {
                if (poType == 1)
                {
                    //sql.Append(" AND POM.IS_DERIVED = 0 ");
                    sql.Append(" AND PRM.PURCHASE_PROCEDURE = 1 ");
                }

                if (poType == 2)
                {
                    //sql.Append(" AND POM.IS_DERIVED = 1 AND POM.IS_DERIVED_TYPE = 1 ");
                    sql.Append(" AND PRM.PURCHASE_PROCEDURE = 2 ");
                }

                if (poType == 3)
                {
                    sql.Append(" AND POM.IS_DERIVED = 1 AND POM.IS_DERIVED_TYPE = 2 ");
                }
            }

            if (supplierIds != null)
            {
                if (supplierIds.Count > 0)
                {
                    if (supplierIds.Any(si => si == 0))
                    {
                        if (supplierIds.Count == 1)
                        {
                            sql.Append(" AND S.SUPPLIER_ID IS NULL ");
                        }
                        else
                        {
                            sql.Append(" AND (S.SUPPLIER_ID IN  (" + string.Join(",", supplierIds.Where(si => si != 0)) + ") OR S.SUPPLIER_ID IS NULL) ");
                        }
                    }
                    else
                    {
                        sql.Append(" AND S.SUPPLIER_ID IN  (" + string.Join(",", supplierIds) + ") ");
                    }
                }
            }


            sql.Append("ORDER BY POM.CREATED_DATE ASC");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> ViewCancelledPOS(int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType, DBConnection dbConnection)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT POM.*,PRM.PURCHASE_PROCEDURE, CLC.FIRST_NAME AS CREATED_USER_NAME,CLA.FIRST_NAME AS APPROVED_USER_NAME,W.LOCATION AS WAREHOUSE_NAME,S.SUPPLIER_NAME,PRM.PR_CODE, PRM.REQUIRED_FOR, POMD.PO_CODE AS PARENT_PO_CODE FROM PO_MASTER AS POM \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLC ON POM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLA ON POM.APPROVED_BY = CLA.USER_ID \n");
            sql.Append("LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n");
            sql.Append("LEFT JOIN PO_MASTER AS POMD ON POM.IS_DERIVED_FROM_PO = POMD.PO_ID \n");
            sql.Append("INNER JOIN SUPPLIER AS S ON POM.SUPPLIER_ID = S.SUPPLIER_ID \n");
            sql.Append("INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n");
            sql.Append("LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME  FROM ITEM_CATEGORY_MASTER) AS ICM ON PRM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n");
            sql.Append("WHERE POM.DEPARTMENT_ID=" + CompanyId + " AND POM.IS_CANCELLED = 1 \n");

            if (prcode != "")
            {
                sql.Append(" AND PRM.PR_CODE =  '" + prcode + "'");
            }

            if (pocode != "")
            {
                sql.Append(" AND POM.PO_CODE =  '" + pocode + "'");
            }

            if (date != DateTime.MinValue)
            {
                sql.Append(" AND MONTH(POM.CREATED_DATE)= " + date.Month.ToString() + " AND YEAR(POM.CREATED_DATE)= " + date.Year.ToString() + " ");
            }

            if (caregoryIds.Count > 0)
            {
                sql.Append(" AND ICM.CATEGORY_ID IN (" + string.Join(",", caregoryIds) + ")");
            }


            if (warehouseIds.Count > 0)
            {
                if (warehouseIds.Any(wi => wi == 0))
                {
                    if (warehouseIds.Count == 1)
                    {
                        sql.Append(" AND W.WAREHOUSE_ID IS NULL ");
                    }
                    else
                    {
                        sql.Append(" AND (W.WAREHOUSE_ID IN  (" + string.Join(",", warehouseIds.Where(wi => wi != 0)) + ") OR W.WAREHOUSE_ID IS NULL) ");
                    }
                }
                else
                {
                    sql.Append(" AND W.WAREHOUSE_ID IN  (" + string.Join(",", warehouseIds) + ") ");
                }
            }

            if (poType != 0)
            {
                if (poType == 1)
                {
                    //sql.Append(" AND POM.IS_DERIVED = 0 ");
                    sql.Append(" AND PRM.PURCHASE_PROCEDURE = 1 ");
                }

                if (poType == 2)
                {
                    // sql.Append(" AND POM.IS_DERIVED = 1 AND POM.IS_DERIVED_TYPE = 1 ");
                    sql.Append(" AND PRM.PURCHASE_PROCEDURE = 2 ");
                }

                if (poType == 3)
                {
                    sql.Append(" AND POM.IS_DERIVED = 1 AND POM.IS_DERIVED_TYPE = 2 ");
                }


            }

            sql.Append("ORDER BY POM.CREATED_DATE ASC");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> ViewMyPOS(int UserId, int CompanyId, DateTime date, string prcode, string pocode, List<int> caregoryIds, List<int> warehouseIds, int poType, DBConnection dbConnection)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT POM.*,CLC.FIRST_NAME AS CREATED_USER_NAME,PRM.PURCHASE_PROCEDURE,PRM.PURCHASE_TYPE,CLA.FIRST_NAME AS APPROVED_USER_NAME,W.LOCATION AS WAREHOUSE_NAME,S.SUPPLIER_NAME,PRM.PR_CODE, PRM.REQUIRED_FOR, POMD.PO_CODE AS PARENT_PO_CODE FROM PO_MASTER AS POM \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLC ON POM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLA ON POM.APPROVED_BY = CLA.USER_ID \n");
            sql.Append("LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n");
            sql.Append("LEFT JOIN PO_MASTER AS POMD ON POM.IS_DERIVED_FROM_PO = POMD.PO_ID \n");
            sql.Append("INNER JOIN SUPPLIER AS S ON POM.SUPPLIER_ID = S.SUPPLIER_ID \n");
            sql.Append("INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n");
            sql.Append("LEFT JOIN(SELECT CATEGORY_ID, CATEGORY_NAME  FROM ITEM_CATEGORY_MASTER) AS ICM ON PRM.PR_CATEGORY_ID = ICM.CATEGORY_ID \n");
            sql.Append("WHERE POM.DEPARTMENT_ID=" + CompanyId + " AND POM.CREATED_BY=" + UserId + " \n");

            if (prcode != "")
            {
                sql.Append(" AND PRM.PR_CODE =  '" + prcode + "'");
            }

            if (pocode != "")
            {
                sql.Append(" AND POM.PO_CODE =  '" + pocode + "'");
            }

            if (date != DateTime.MinValue)
            {
                sql.Append(" AND MONTH(POM.CREATED_DATE)= " + date.Month.ToString() + " AND YEAR(POM.CREATED_DATE)= " + date.Year.ToString() + " ");
            }

            if (caregoryIds.Count > 0)
            {
                sql.Append(" AND ICM.CATEGORY_ID IN (" + string.Join(",", caregoryIds) + ")");
            }


            if (warehouseIds.Count > 0)
            {
                if (warehouseIds.Any(wi => wi == 0))
                {
                    if (warehouseIds.Count == 1)
                    {
                        sql.Append(" AND W.WAREHOUSE_ID IS NULL ");
                    }
                    else
                    {
                        sql.Append(" AND (W.WAREHOUSE_ID IN  (" + string.Join(",", warehouseIds.Where(wi => wi != 0)) + ") OR W.WAREHOUSE_ID IS NULL) ");
                    }
                }
                else
                {
                    sql.Append(" AND W.WAREHOUSE_ID IN  (" + string.Join(",", warehouseIds) + ") ");
                }
            }

            if (poType != 0)
            {
                if (poType == 1)
                {
                    //sql.Append(" AND POM.IS_DERIVED = 0 ");
                    sql.Append(" AND PRM.PURCHASE_PROCEDURE = 1 ");
                }

                if (poType == 2)
                {
                    // sql.Append(" AND POM.IS_DERIVED = 1 AND POM.IS_DERIVED_TYPE = 1 ");
                    sql.Append(" AND PRM.PURCHASE_PROCEDURE = 2 ");
                }

                if (poType == 3)
                {
                    sql.Append(" AND POM.IS_DERIVED = 1 AND POM.IS_DERIVED_TYPE = 2 ");
                }


            }

            sql.Append("ORDER BY POM.CREATED_DATE ASC");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> GetDerrivedFromPOs(int DerrivedPO, DBConnection dbConnection)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT POM.*,CLC.FIRST_NAME AS CREATED_USER_NAME,CLA.FIRST_NAME AS APPROVED_USER_NAME,W.LOCATION AS WAREHOUSE_NAME,S.SUPPLIER_NAME,PRM.PR_CODE FROM PO_MASTER AS POM \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLC ON POM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLA ON POM.APPROVED_BY = CLA.USER_ID \n");
            sql.Append("LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n");
            sql.Append("INNER JOIN SUPPLIER AS S ON POM.SUPPLIER_ID = S.SUPPLIER_ID \n");
            sql.Append("INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n");
            sql.Append("WHERE PO_ID =" + DerrivedPO);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> GetDerrivedPOs(int DerrivedPO, DBConnection dbConnection)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT POM.*,CLC.FIRST_NAME AS CREATED_USER_NAME,CLA.FIRST_NAME AS APPROVED_USER_NAME,W.LOCATION AS WAREHOUSE_NAME,S.SUPPLIER_NAME,PRM.PR_CODE FROM PO_MASTER AS POM \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLC ON POM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLA ON POM.APPROVED_BY = CLA.USER_ID \n");
            sql.Append("LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n");
            sql.Append("INNER JOIN SUPPLIER AS S ON POM.SUPPLIER_ID = S.SUPPLIER_ID \n");
            sql.Append("INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n");
            sql.Append("WHERE IS_DERIVED_FROM_PO =" + DerrivedPO);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<string> getPoDetailsByGrnId(int GrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT B.PO_CODE, C.PR_CODE FROM PO_GRN AS A " +
                                            "INNER JOIN(SELECT PO_ID, PO_CODE, BASED_PR FROM  PO_MASTER) AS B " +
                                            "ON A.PO_ID = B.PO_ID " +
                                            "INNER JOIN (SELECT PR_ID, PR_CODE FROM PR_MASTER) AS C " +
                                            "ON C.PR_ID = B.BASED_PR WHERE GRN_ID = " + GrnId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            List<string> poCodes = new List<string>();
            string PrCode = String.Empty;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        poCodes.Add(dbConnection.dr[0].ToString());
                        PrCode = dbConnection.dr[1].ToString();
                    }
                }
            }
            List<string> returnList = new List<string>();
            returnList.Add(string.Join(", ", poCodes));
            returnList.Add(PrCode);

            return returnList;


        }

        public POMaster GetPoMasterToEditPO(int PoId, int CompanyId, DBConnection dbConnection)
        {
            POMaster po;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT POM.*,PRM.PR_CODE,cl.*,cll.* FROM PO_MASTER AS POM \n" +
                                            "INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_USER_NAME, DIGITAL_SIGNATURE AS APPROVED_SIGNATURE FROM COMPANY_LOGIN) AS CL ON POM.APPROVED_BY = CL.USER_ID\n" +
                                             "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME, DIGITAL_SIGNATURE AS CREATED_SIGNATURE FROM COMPANY_LOGIN) AS CLL ON POM.CREATED_BY = CLL.USER_ID\n" +
                                            "WHERE PO_ID = " + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                po = dataAccessObject.GetSingleOject<POMaster>(dbConnection.dr);
            }

            if (po != null)
            {
                po.QuotationFor = DAOFactory.CreatePR_MasterDAO().GetQuotationForbyPrCode(po.DepartmentId, po.PrCode, dbConnection);

                po._Supplier = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(po.SupplierId, dbConnection);
                po._Warehouse = DAOFactory.CreateWarehouseDAO().getWarehouseByID(po.DeliverToWarehouse, dbConnection);
                po.PoDetails = DAOFactory.createPODetailsDAO().GetPODetailsToViewPo(po.PoID, CompanyId, dbConnection);
            }

            return po;
        }

        public List<int> UpdatePO(POMaster poMaster, int UserId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.AppendLine("DECLARE @PoIdTable TABLE (POID INT)");
            sql.AppendLine("DECLARE @PoCodeTable TABLE (POCode INT)");
            sql.AppendLine("INSERT INTO @PoCodeTable");
            sql.AppendLine("SELECT COUNT(PO_ID)+1 FROM PO_MASTER WHERE DEPARTMENT_ID= " + poMaster.DepartmentId + ";");

            sql.AppendLine("INSERT INTO PO_MASTER ([PO_CODE],[DEPARTMENT_ID],[SUPPLIER_ID],[CREATED_DATE],[CREATED_BY],[VAT_AMOUNT],[NBT_AMOUNT],[TOTAL_AMOUNT],[BASED_PR],[DELIVER_TO_WAREHOUSE],[IS_DERIVED],[IS_DERIVED_TYPE],[IS_DERIVED_FROM_PO],[DERIVING_REASON],[PARENT_APPROVED_USER],[IS_APPROVED_BY_PARENT_APPROVED_USER],[IS_CURRENT],[QUOTATION_ID],[QUOTATION_APPROVED_BY],[QUOTATION_CONFIRMED_BY], [PRINT_COUNT],[REMARKS])");
            sql.AppendLine("OUTPUT INSERTED.PO_ID INTO @PoIdTable(POID)");
            sql.AppendLine("VALUES ((SELECT 'PO'+CONVERT(VARCHAR,MAX (POCode)) FROM @PoCodeTable)," + poMaster.DepartmentId + "," + poMaster.SupplierId + ",'" + LocalTime.Now + "','" + poMaster.CreatedBy + "'," + poMaster.VatAmount + "," + poMaster.NBTAmount + "," + poMaster.TotalAmount + "," + poMaster.BasePr + "," + poMaster.DeliverToWarehouse + ",1,1," + poMaster.IsDerivedFromPo + ",'" + poMaster.DerivingReason + "'," + (poMaster.ParentApprovedUser == 0 ? "NULL" : poMaster.ParentApprovedUser.ToString()) + "," + (poMaster.ParentApprovedUser == 0 ? "1" : "0") + ", 1, " + poMaster.QuotationId + "," + poMaster.QuotationApprovedBy + "," + poMaster.QuotationConfirmedBy + ", 0, '" + poMaster.Remarks + "');");

            for (int j = 0; j < poMaster.PoDetails.Count; j++)
            {
                sql.AppendLine("INSERT INTO PO_DETAILS ([PO_ID],[QUOTATION_ITEM_ID],[ITEM_PRICE],[QUANTITY],[TOTAL_AMOUNT],[VAT_AMOUNT],[NBT_AMOUNT],[ITEM_ID],[BASED_PO],[IS_PO_EDIT_MODE],[HAS_NBT],[NBT_CALCULATION_TYPE],[HAS_VAT],[TABULATION_ID],[TABULATION_DETAIL_ID],[MEASUREMENT_ID],[RECEIVED_QTY], [STATUS], [SUPPLIER_MENTIONED_ITEM_NAME])");
                sql.AppendLine("VALUES ((SELECT MAX(POID) FROM @PoIdTable)," + poMaster.PoDetails[j].QuotationItemId + "," + poMaster.PoDetails[j].ItemPrice + "," + poMaster.PoDetails[j].Quantity + "," + poMaster.PoDetails[j].TotalAmount + "," + poMaster.PoDetails[j].VatAmount + "," + poMaster.PoDetails[j].NbtAmount + "," + poMaster.PoDetails[j].ItemId + ",(SELECT MAX(POID) FROM @PoIdTable),1," + poMaster.PoDetails[j].HasNbt + "," + poMaster.PoDetails[j].NbtCalculationType + "," + poMaster.PoDetails[j].HasVat + "," + poMaster.PoDetails[j].TabulationId + "," + poMaster.PoDetails[j].TabulationDetailId + ", " + poMaster.PoDetails[j].MeasurementId + ", 0, 0, '" + poMaster.PoDetails[j].SupplierMentionedItemName + "');");

                sql.AppendLine("UPDATE PR_DETAIL SET IS_PO_RAISED = 1 WHERE PR_ID = " + poMaster.BasePr + " AND ITEM_ID =" + poMaster.PoDetails[j].ItemId + " AND IS_ACTIVE = 1;");

                sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='APPRV_PO') WHERE PR_ID = " + poMaster.BasePr + " AND ITEM_ID =" + poMaster.PoDetails[j].ItemId);

                //Inserting PR_DETAIL Status Update log
                sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
                sql.AppendLine("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='PO_MDFD'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL WHERE PR_ID = " + poMaster.BasePr + " AND ITEM_ID =" + poMaster.PoDetails[j].ItemId);

                sql.AppendLine("");
                sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='APPRV_PO') \n");
                sql.Append("	WHERE PR_ID = " + poMaster.BasePr + " AND \n");
                sql.Append("	ITEM_ID =" + poMaster.PoDetails[j].ItemId);
            }

            sql.AppendLine("");
            sql.AppendLine("UPDATE PO_MASTER SET WAS_DERIVED = 1, WAS_DERIVED_TYPE=1,DERIVING_REASON='" + poMaster.DerivingReason + "',IS_CURRENT=0 WHERE PO_ID =" + poMaster.IsDerivedFromPo);
            sql.AppendLine("");
            sql.AppendLine("SELECT * FROM @PoIdTable");
            sql.AppendLine("UNION ALL");
            sql.AppendLine("SELECT * FROM @PoCodeTable");


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                List<int> PoIdAndCode = new List<int>();

                if (dbConnection.dr.HasRows)
                {
                    while (dbConnection.dr.Read())
                    {
                        PoIdAndCode.Add(int.Parse(dbConnection.dr[0].ToString()));
                    }
                }

                return PoIdAndCode;
            }
        }

        public List<POMaster> GetPosForApproval(int CompanyId, int UserId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT POM.PO_ID,POM.PO_CODE,PM.PR_CODE,PM.PURCHASE_TYPE,PM.REQUIRED_FOR,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT,PM.PURCHASE_PROCEDURE, POM.IS_APPROVED,POM.IS_DERIVED,POM.IS_DERIVED_TYPE, POM.CREATED_DATE " +
                                            "FROM PO_MASTER  AS POM " +
                                            "INNER JOIN PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR " +
                                            "INNER JOIN PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID " +
                                            "INNER JOIN SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID " +
                                            "WHERE POM.DEPARTMENT_ID = " + CompanyId + " " +
                                            "AND (ISNULL(POM.IS_APPROVED,0)=0 AND ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=0) OR (ISNULL(POM.IS_APPROVED,0)=0 AND ISNULL(POM.WAS_DERIVED,0) =1 AND ISNULL(POM.IS_CURRENT,0)=1) OR (ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=1 AND POM.IS_DERIVED_TYPE=2 AND ISNULL(POM.IS_APPROVED_BY_PARENT_APPROVED_USER,0)=1 AND ISNULL(POM.IS_APPROVED,0)=0) OR (ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=1 AND ISNULL(POM.IS_APPROVED_BY_PARENT_APPROVED_USER,0)=0 AND ISNULL(POM.PARENT_APPROVED_USER,0)=" + UserId + ") " +
                                            "GROUP BY PM.PURCHASE_PROCEDURE,POM.PO_ID,POM.PO_CODE,PM.PR_CODE,PM.PURCHASE_TYPE,PM.REQUIRED_FOR,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,POM.IS_DERIVED,POM.IS_DERIVED_TYPE, POM.CREATED_DATE ORDER BY POM.CREATED_DATE ASC ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> GetPosForInvoice(int CompanyId, int UserId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT PM.PURCHASE_PROCEDURE,POM.PO_ID,POM.PO_CODE,PM.PR_CODE,PM.REQUIRED_FOR,PM.PURCHASE_TYPE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.IS_APPROVED,POM.IS_DERIVED,POM.IS_DERIVED_TYPE, POM.CREATED_DATE " +
                                            "FROM PO_MASTER  AS POM " +
                                            "INNER JOIN PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR " +
                                            "INNER JOIN PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID " +
                                            "INNER JOIN SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID " +
                                            "WHERE POM.DEPARTMENT_ID = " + CompanyId + " AND POM.IS_APPROVED = 1 AND PAYMENT_METHOD = 4 " +
                                            //"AND (ISNULL(POM.IS_APPROVED,0)=0 AND ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=0) OR (ISNULL(POM.IS_APPROVED,0)=0 AND ISNULL(POM.WAS_DERIVED,0) =1 AND ISNULL(POM.IS_CURRENT,0)=1) OR (ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=1 AND POM.IS_DERIVED_TYPE=2 AND ISNULL(POM.IS_APPROVED_BY_PARENT_APPROVED_USER,0)=1 AND ISNULL(POM.IS_APPROVED,0)=0) OR (ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=1 AND ISNULL(POM.IS_APPROVED_BY_PARENT_APPROVED_USER,0)=0 AND ISNULL(POM.PARENT_APPROVED_USER,0)=" + UserId + ") " +
                                            "GROUP BY PM.PURCHASE_PROCEDURE,POM.PO_ID,POM.PO_CODE,PM.PR_CODE,PM.REQUIRED_FOR,PM.PURCHASE_TYPE,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,POM.IS_DERIVED,POM.IS_DERIVED_TYPE, POM.CREATED_DATE ORDER BY POM.CREATED_DATE ASC ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }
        public List<POMaster> GetPosForPrint(int CompanyId, int UserId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT POM.PO_ID,POM.PO_CODE,PM.PR_CODE,PM.PURCHASE_TYPE,PM.REQUIRED_FOR,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.IS_APPROVED,POM.IS_DERIVED,POM.IS_DERIVED_TYPE, POM.CREATED_DATE " +
                                            "FROM PO_MASTER  AS POM " +
                                            "INNER JOIN PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR " +
                                            "INNER JOIN PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID " +
                                            "INNER JOIN SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID " +
                                            "WHERE POM.DEPARTMENT_ID = " + CompanyId + " AND POM.IS_APPROVED != 2 AND (POM.IS_CANCELLED = 0 OR POM.IS_CANCELLED IS NULL) " +
                                            //"AND (ISNULL(POM.IS_APPROVED,0)=0 AND ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=0) OR (ISNULL(POM.IS_APPROVED,0)=0 AND ISNULL(POM.WAS_DERIVED,0) =1 AND ISNULL(POM.IS_CURRENT,0)=1) OR (ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=1 AND POM.IS_DERIVED_TYPE=2 AND ISNULL(POM.IS_APPROVED_BY_PARENT_APPROVED_USER,0)=1 AND ISNULL(POM.IS_APPROVED,0)=0) OR (ISNULL(POM.WAS_DERIVED,0) =0 AND POM.IS_DERIVED=1 AND ISNULL(POM.IS_APPROVED_BY_PARENT_APPROVED_USER,0)=0 AND ISNULL(POM.PARENT_APPROVED_USER,0)=" + UserId + ") " +
                                            "GROUP BY POM.PO_ID,POM.PO_CODE,PM.PR_CODE,PM.PURCHASE_TYPE,PM.REQUIRED_FOR,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,POM.IS_DERIVED,POM.IS_DERIVED_TYPE, POM.CREATED_DATE ORDER BY POM.CREATED_DATE ASC ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }
        public POMaster GetPoMasterToViewPO(int PoId, int CompanyId, DBConnection dbConnection)
        {
            POMaster po;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT POM.*,AD.APPROVED_DESIGNATION_NAME,PRM.IMPORT_ITEM_TYPE, APD.CREATED_DESIGNATION_NAME, PD.PARENT_APPROVED_DESIGNATION_NAME, SUB.DEPARTMENT_NAME,PRM.PR_CODE,PRM.PURCHASE_TYPE,PRM.REQUIRED_FOR,CLA.*,CLC.* ,SK.STORE_KEEPER, PRM.PURCHASE_TYPE,PRM.PURCHASE_PROCEDURE, PRM.CLONED_FROM_PR, CLPA.*,POMD.PO_CODE AS PARENT_PO_CODE FROM PO_MASTER AS POM \n" +
                                            "INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_USER_NAME,DESIGNATION_ID AS APPROVED_DESIGNATION_ID, DIGITAL_SIGNATURE AS APPROVED_SIGNATURE FROM COMPANY_LOGIN) AS CLA ON POM.APPROVED_BY = CLA.USER_ID\n" +
                                             "LEFT JOIN (SELECT MRN_ID, SUB_DEPARTMENT_ID FROM MRN_MASTER) AS MRN ON PRM.MRN_ID =MRN.MRN_ID  " +
                                             "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT) AS SUB ON SUB.SUB_DEPARTMENT_ID = MRN.SUB_DEPARTMENT_ID " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME,DESIGNATION_ID AS CREATED_DESIGNATION_ID, DIGITAL_SIGNATURE AS CREATED_SIGNATURE FROM COMPANY_LOGIN) AS CLC ON POM.CREATED_BY = CLC.USER_ID\n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS PARENT_APPROVED_USER_NAME, DESIGNATION_ID AS PARENT_APPROVED_DESIGNATION_ID,DIGITAL_SIGNATURE AS PARENT_APPROVED_USER_SIGNATURE FROM COMPANY_LOGIN) AS CLPA ON POM.PARENT_APPROVED_USER = CLPA.USER_ID\n" +
                                            "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS APPROVED_DESIGNATION_NAME FROM DESIGNATION ) AS AD ON AD.DESIGNATION_ID = CLA.APPROVED_DESIGNATION_ID \n" +
                                               "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS CREATED_DESIGNATION_NAME FROM DESIGNATION ) AS APD ON APD.DESIGNATION_ID = CLC.CREATED_DESIGNATION_ID \n" +
                                               "LEFT JOIN (SELECT DESIGNATION_ID, DESIGNATION_NAME AS PARENT_APPROVED_DESIGNATION_NAME FROM DESIGNATION ) AS PD ON PD.DESIGNATION_ID = CLPA.PARENT_APPROVED_DESIGNATION_ID \n" +

                                            "LEFT JOIN PO_MASTER AS POMD ON POM.IS_DERIVED_FROM_PO = POMD.PO_ID \n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS STORE_KEEPER FROM COMPANY_LOGIN) AS SK ON SK.USER_ID = PRM.CREATED_BY\n" +
                                            "WHERE POM.PO_ID = " + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                po = dataAccessObject.GetSingleOject<POMaster>(dbConnection.dr);
            }

            if (po != null)
            {
                po._Supplier = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(po.SupplierId, dbConnection);
                po._Warehouse = DAOFactory.CreateWarehouseDAO().getWarehouseByID(po.DeliverToWarehouse, dbConnection);
                po.PoDetails = DAOFactory.createPODetailsDAO().GetPODetailsToViewPo(po.PoID, CompanyId, dbConnection);
                po.DerivedPOs = GetDerrivedPOs(po.PoID, dbConnection);
                if (po.IsDerived == 1)
                {
                    po.DerivedFromPOs = GetDerrivedFromPOs(po.IsDerivedFromPo, dbConnection);
                }
                po.GeneratedGRNs = DAOFactory.createGrnDAO().GetGeneratedGRNsForPo(po.PoID, dbConnection);
                po._companyDepartment = DAOFactory.createCompanyDepartment().GetDepartmentByDepartmentId(po.DepartmentId, dbConnection);
            }

            return po;
        }

        public List<POMaster> GetModifiedPosForApproval(int CompanyId, int UserId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();


            dbConnection.cmd.CommandText = "SELECT POM.PO_ID,POM.PO_CODE,PM.PR_CODE,PM.REQUIRED_FOR,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.IS_APPROVED,POM.IS_DERIVED,POM.IS_DERIVED_TYPE, POM.CREATED_DATE " +
                                            "FROM PO_MASTER  AS POM  " +
                                            "LEFT JOIN PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR   " +
                                            "LEFT JOIN PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID   " +
                                            "LEFT JOIN SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID   " +
                                            "WHERE POM.DEPARTMENT_ID = " + CompanyId + " AND " +
                                            "(ISNULL(POM.IS_CURRENT,0) =1 AND POM.IS_DERIVED=1 AND  ISNULL(POM.IS_DERIVED_TYPE,0)=1 AND ISNULL(POM.IS_APPROVED_BY_PARENT_APPROVED_USER,0)=1 AND ISNULL(POM.IS_APPROVED,0)=0) " +
                                            "GROUP BY POM.PO_ID,POM.PO_CODE,PM.PR_CODE,PM.REQUIRED_FOR,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,POM.IS_DERIVED,POM.IS_DERIVED_TYPE, POM.CREATED_DATE ORDER BY POM.CREATED_DATE ASC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        //public int ParentApprovePO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, DBConnection dbConnection) {

        //    dbConnection.cmd.Parameters.Clear();

        //    if (PoType == 2) {
        //        dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET [IS_APPROVED_BY_PARENT_APPROVED_USER] =1,[PARENT_APPROVED_USER_APPROVAL_DATE]='" + LocalTime.Now + "',[PARENT_APPROVED_USER_APPROVAL_REMARKS]='" + Remarks + "',PAYMENT_METHOD='" + PaymentMethod + "' WHERE PO_ID = " + PoId + "; \n";
        //        dbConnection.cmd.CommandText += "UPDATE PO_MASTER SET IS_APPROVED= 1, APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "', APPROVAL_REMARKS ='" + Remarks + "'  WHERE PO_ID = " + PoId + "; \n";
        //    }
        //    else {
        //        if (IsParentApproved == 0) {
        //            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET [IS_APPROVED_BY_PARENT_APPROVED_USER] =1,[PARENT_APPROVED_USER_APPROVAL_DATE]='" + LocalTime.Now + "',[PARENT_APPROVED_USER_APPROVAL_REMARKS]='" + Remarks + "',PAYMENT_METHOD='" + PaymentMethod + "' WHERE PO_ID = " + PoId + "; \n";
        //        }
        //        else if (IsParentApproved == 1) {
        //            dbConnection.cmd.CommandText = "UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', IS_APPROVED= 1, APPROVAL_REMARKS ='" + Remarks + "',  APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n";

        //        }
        //    }


        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
        //    return dbConnection.cmd.ExecuteNonQuery();

        //}

        //public int ApprovePO(int PoId, int UserId, string Remarks, int PaymentMethod, DBConnection dbConnection) {
        //    // Approval for general POs
        //    dbConnection.cmd.Parameters.Clear();

        //    dbConnection.cmd.CommandText = "UPDATE PO_MASTER SET IS_APPROVED= 1, APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "', APPROVAL_REMARKS ='" + Remarks + "'  WHERE PO_ID = " + PoId + "; \n";

        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
        //    return dbConnection.cmd.ExecuteNonQuery();

        //}

        public int ParentApprovePO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, string PoRemark, DBConnection dbConnection)
        {

            StringBuilder sql = new StringBuilder();

            if (PoType == 2)
            { //covering  po

                sql.Append("UPDATE " + dbLibrary + ".PO_MASTER SET [IS_APPROVED_BY_PARENT_APPROVED_USER] =1,[PARENT_APPROVED_USER_APPROVAL_DATE]='" + LocalTime.Now + "',[PARENT_APPROVED_USER_APPROVAL_REMARKS]='" + Remarks + "',PAYMENT_METHOD='" + PaymentMethod + "' WHERE PO_ID = " + PoId + "; \n");
                sql.Append(" \n");
                sql.Append("UPDATE PO_MASTER SET IS_APPROVED= 1, REMARKS = '" + PoRemark + "', APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "', APPROVAL_REMARKS ='" + Remarks + "'  WHERE PO_ID = " + PoId + "; \n");
                sql.Append(" \n");

                sql.Append(" INSERT INTO PR_DETAIL_STATUS_LOG \n");
                sql.Append(" SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='APPRVD_PO'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
                sql.Append(" WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                sql.Append(" ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
                sql.Append(" \n");

                sql.Append("UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='GNRTE_GRN') \n");
                sql.Append(" WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                sql.Append(" ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");


            }
            else
            {
                if (IsParentApproved == 0)
                { // modified PO without parent approval
                    sql.Append("UPDATE " + dbLibrary + ".PO_MASTER SET [IS_APPROVED_BY_PARENT_APPROVED_USER] =1,[PARENT_APPROVED_USER_APPROVAL_DATE]='" + LocalTime.Now + "',[PARENT_APPROVED_USER_APPROVAL_REMARKS]='" + Remarks + "',PAYMENT_METHOD='" + PaymentMethod + "' WHERE PO_ID = " + PoId + "; \n");

                }
                else if (IsParentApproved == 1)
                { // modified PO with parent approval
                    sql.Append("UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', IS_APPROVED= 1, APPROVAL_REMARKS ='" + Remarks + "',  APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + " \n");
                    sql.Append(" \n");

                    sql.Append(" INSERT INTO PR_DETAIL_STATUS_LOG \n");
                    sql.Append(" SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='APPRVD_PO'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
                    sql.Append(" WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                    sql.Append(" ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
                    sql.Append(" \n");

                    sql.Append("UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='GNRTE_GRN') \n");
                    sql.Append(" WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                    sql.Append(" ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
                }
            }


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();

        }



        public int ApproveGeneralPO(int PoId, int UserId, string Remarks, int PaymentMethod, string PoRemark, DBConnection dbConnection)
        {
            // Approval for general POs
            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', REMARKS = '" + PoRemark + "', IS_APPROVED= 1, APPROVED_BY=" + UserId + ", APPROVAL_REMARKS ='" + Remarks + "',APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + " \n");
            sql.Append(" \n");

            sql.Append(" INSERT INTO PR_DETAIL_STATUS_LOG \n");
            sql.Append(" SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='APPRVD_PO'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
            sql.Append(" WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
            sql.Append(" ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
            sql.Append(" \n");

            sql.Append("UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='GNRTE_GRN') \n");
            sql.Append(" WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
            sql.Append(" ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();

        }

        //public int RejectPO(int PoId, int UserId, string Remarks, int PaymentMethod, DBConnection dbConnection) {

        //    dbConnection.cmd.Parameters.Clear();
        //    dbConnection.cmd.CommandText = "UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', IS_APPROVED= 2, APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n";
        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
        //    return dbConnection.cmd.ExecuteNonQuery();

        //}

        public int RejectGeneralPO(int PoId, int UserId, string Remarks, int PaymentMethod, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            // Reject General PO
            sql.Append("UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', IS_APPROVED= 2,APPROVAL_REMARKS='" + Remarks + "', APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n");
            sql.Append(" \n");

            sql.Append("INSERT INTO PR_DETAIL_STATUS_LOG \n");
            sql.Append("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='RJCTED_PO'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
            sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
            sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
            sql.Append(" \n");

            sql.Append("UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED') \n");
            sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
            sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
            sql.Append(" \n");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();


        }


        //public int ParentRejectPO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, int RejectionAction, int ParentPOId, DBConnection dbConnection) {

        //    dbConnection.cmd.Parameters.Clear();

        //    if (PoType == 2) { // Covering PO
        //        dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET [IS_APPROVED_BY_PARENT_APPROVED_USER] =2,[PARENT_APPROVED_USER_APPROVAL_DATE]='" + LocalTime.Now + "',[PARENT_APPROVED_USER_APPROVAL_REMARKS]='" + Remarks + "',PAYMENT_METHOD='" + PaymentMethod + "' WHERE PO_ID = " + PoId + "; \n";
        //        dbConnection.cmd.CommandText += "UPDATE PO_MASTER SET IS_APPROVED= 2, APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n";
        //    }
        //    else { // Modified PO
        //        if (IsParentApproved == 0) {
        //            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_MASTER SET [IS_APPROVED_BY_PARENT_APPROVED_USER] =2,[PARENT_APPROVED_USER_APPROVAL_DATE]='" + LocalTime.Now + "',[PARENT_APPROVED_USER_APPROVAL_REMARKS]='" + Remarks + "' WHERE PO_ID = " + PoId + "; \n";
        //            dbConnection.cmd.CommandText += "UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', IS_APPROVED= 2, APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n";

        //        }
        //        if (IsParentApproved == 1) {
        //            dbConnection.cmd.CommandText = "UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', IS_APPROVED= 2, APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n";

        //        }
        //    }
        //    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
        //    return dbConnection.cmd.ExecuteNonQuery();

        //}

        public int ParentRejectPO(int PoId, string Remarks, int PaymentMethod, int PoType, int UserId, int IsParentApproved, int RejectionAction, int ParentPoId, DBConnection dbConnection)
        {

            StringBuilder sql = new StringBuilder();

            if (PoType == 2)
            { // Covering PO
                sql.Append("UPDATE " + dbLibrary + ".PO_MASTER SET [IS_APPROVED_BY_PARENT_APPROVED_USER] =2,[PARENT_APPROVED_USER_APPROVAL_DATE]='" + LocalTime.Now + "',[PARENT_APPROVED_USER_APPROVAL_REMARKS]='" + Remarks + "',PAYMENT_METHOD='" + PaymentMethod + "' WHERE PO_ID = " + PoId + "; \n");
                sql.Append(" \n");
                sql.Append("UPDATE PO_MASTER SET IS_APPROVED= 2, APPROVED_BY=" + UserId + ",APPROVAL_REMARKS='" + Remarks + "', APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n");
                sql.Append(" \n");

                sql.Append("DECLARE @PARENT_PO_ID INT=" + ParentPoId + ", @COVERING_PO_ID INT=" + PoId + ",@GRN_ID INT \n");
                sql.Append(" \n");
                sql.Append("SELECT @GRN_ID = GRN_ID FROM PO_GRN WHERE PO_ID=@COVERING_PO_ID \n");
                sql.Append(" \n");
                sql.Append("UPDATE GRN_MASTER SET IS_APPROVED= 2,APPROVED_BY=" + UserId + ", APPROVED_DATE='" + LocalTime.Now + "',APPROVAL_REMARKS='Covering PO Rejected with the Remarks: " + Remarks + "' WHERE GRN_ID=@GRN_ID \n");
                sql.Append(" \n");
                sql.Append("DECLARE @GRN_DETAILS TABLE(ITEM_ID INT, AVAILABLE_QTY DECIMAL(18,2)) \n");
                sql.Append("		 \n");
                sql.Append("INSERT INTO @GRN_DETAILS \n");
                sql.Append("SELECT ITEM_ID,QUANTITY FROM GRN_DETAILS WHERE GRN_ID=@GRN_ID \n");
                sql.Append("		 \n");
                sql.Append("DECLARE @COVERING_PO_DETAILS TABLE(ITEM_ID INT, QTY DECIMAL(18,2)) \n");
                sql.Append("		 \n");
                sql.Append("INSERT INTO @COVERING_PO_DETAILS \n");
                sql.Append("SELECT ITEM_ID,QUANTITY FROM PO_DETAILS WHERE PO_ID=@COVERING_PO_ID \n");
                sql.Append(" \n");
                sql.Append("DECLARE @CPO_ROWS INT, @LOOPED_CPO_ROWS INT = 0 \n");
                sql.Append(" \n");
                sql.Append("SELECT @CPO_ROWS= COUNT(*) FROM @COVERING_PO_DETAILS \n");
                sql.Append(" \n");
                sql.Append("-- UPDATING COVERING PO: START \n");
                sql.Append("WHILE(@LOOPED_CPO_ROWS != @CPO_ROWS) \n");
                sql.Append("BEGIN \n");
                sql.Append("	DECLARE @CPO_ITEM_ID INT \n");
                sql.Append("	DECLARE @CPO_QTY DECIMAL(18,2) \n");
                sql.Append(" \n");
                sql.Append("	SELECT @CPO_ITEM_ID=ITEM_ID, @CPO_QTY = QTY FROM @COVERING_PO_DETAILS ORDER BY ITEM_ID OFFSET @LOOPED_CPO_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
                sql.Append(" \n");
                sql.Append("	UPDATE @GRN_DETAILS SET AVAILABLE_QTY -= @CPO_QTY WHERE ITEM_ID = @CPO_ITEM_ID \n");
                sql.Append(" \n");
                sql.Append("	SET @LOOPED_CPO_ROWS += 1 \n");
                sql.Append("END \n");
                sql.Append(" \n");
                sql.Append("UPDATE PO_DETAILS SET WAITING_QTY=0 WHERE PO_ID =@COVERING_PO_ID \n");
                sql.Append("-- UPDATING COVERING PO: END \n");
                sql.Append(" \n");
                sql.Append("-- UPDATING PARENT PO: START \n");
                sql.Append("DECLARE @GRND_ROWS INT \n");
                sql.Append("DECLARE @LOOPED_GRND_ROWS INT \n");
                sql.Append("DECLARE @GRND_ITEM_ID INT \n");
                sql.Append("DECLARE @GRND_RECEIVED_QTY  DECIMAL(18,2) \n");
                sql.Append(" \n");
                sql.Append("SET @LOOPED_GRND_ROWS=0 \n");
                sql.Append("SELECT @GRND_ROWS = COUNT(*) FROM @GRN_DETAILS \n");
                sql.Append(" \n");
                sql.Append("WHILE(@LOOPED_GRND_ROWS != @GRND_ROWS) \n");
                sql.Append("BEGIN \n");
                sql.Append("	SELECT @GRND_ITEM_ID=ITEM_ID, @GRND_RECEIVED_QTY=AVAILABLE_QTY FROM @GRN_DETAILS ORDER BY ITEM_ID OFFSET @LOOPED_GRND_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
                sql.Append(" \n");
                sql.Append("	UPDATE PO_DETAILS SET WAITING_QTY-= @GRND_RECEIVED_QTY WHERE ITEM_ID=@GRND_ITEM_ID AND PO_ID = @PARENT_PO_ID \n");
                sql.Append(" \n");
                sql.Append("	SET @LOOPED_GRND_ROWS+=1 \n");
                sql.Append(" \n");
                sql.Append("END \n");
            }
            else
            { // Modified PO

                sql.Append("INSERT INTO PR_DETAIL_STATUS_LOG \n");
                sql.Append("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='RJCTED_PO'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
                sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
                sql.Append(" \n");

                if (IsParentApproved == 0)
                {

                    sql.Append("UPDATE " + dbLibrary + ".PO_MASTER SET [IS_APPROVED_BY_PARENT_APPROVED_USER] =2,[PARENT_APPROVED_USER_APPROVAL_DATE]='" + LocalTime.Now + "',[PARENT_APPROVED_USER_APPROVAL_REMARKS]='" + Remarks + "' WHERE PO_ID = " + PoId + "; \n");
                    sql.Append("UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', IS_APPROVED= 2, APPROVAL_REMARKS = 'Rejected By Parent PO Approved User with the remarks: " + Remarks + "', APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n");

                    if (RejectionAction == 1)
                    {
                        sql.Append("UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED') \n");
                        sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                        sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
                        sql.Append(" \n");
                    }
                    else
                    {
                        //reverse to previous
                        sql.Append("UPDATE PO_MASTER SET IS_CURRENT=0 WHERE PO_ID=" + PoId + " \n");
                        sql.Append("UPDATE PO_MASTER SET IS_CURRENT=1 WHERE PO_ID=" + ParentPoId + " \n");

                        sql.Append("INSERT INTO PR_DETAIL_STATUS_LOG \n");
                        sql.Append("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='PO_REVISD'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
                        sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                        sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
                        sql.Append(" \n");

                        sql.Append("IF EXISTS(SELECT PO_ID FROM PO_MASTER WHERE PO_ID =" + ParentPoId + " AND IS_APPROVED=0) \n");
                        sql.Append("BEGIN \n");
                        sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='APPRV_PO') \n");
                        sql.Append("	WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + ParentPoId + ") AND \n");
                        sql.Append("	ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID =" + ParentPoId + "); \n");
                        sql.Append("END \n");
                        sql.Append("ELSE IF EXISTS(SELECT PO_ID FROM PO_MASTER WHERE PO_ID =" + ParentPoId + " AND IS_APPROVED=1) \n");
                        sql.Append("BEGIN \n");
                        sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='GNRTE_GRN') \n");
                        sql.Append("	WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + ParentPoId + ") AND \n");
                        sql.Append("	ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID =" + ParentPoId + "); \n");
                        sql.Append("END \n");
                        sql.Append("ELSE \n");
                        sql.Append("BEGIN \n");
                        sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED') \n");
                        sql.Append("	WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + ParentPoId + ") AND \n");
                        sql.Append("	ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID =" + ParentPoId + "); \n");
                        sql.Append("END");
                    }

                }
                if (IsParentApproved == 1)
                {
                    sql.Append("UPDATE PO_MASTER SET PAYMENT_METHOD='" + PaymentMethod + "', IS_APPROVED= 2,APPROVAL_REMARKS='" + Remarks + "', APPROVED_BY=" + UserId + ",APPROVED_DATE='" + LocalTime.Now + "'  WHERE PO_ID = " + PoId + "; \n");

                    if (RejectionAction == 1)
                    {

                        sql.Append("UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED') \n");
                        sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                        sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
                        sql.Append(" \n");
                    }
                    else
                    {
                        //reverse to previous
                        sql.Append("UPDATE PO_MASTER SET IS_CURRENT=0 WHERE PO_ID=" + PoId + " \n");
                        sql.Append("UPDATE PO_MASTER SET IS_CURRENT=1 WHERE PO_ID=" + ParentPoId + " \n");

                        sql.Append("INSERT INTO PR_DETAIL_STATUS_LOG \n");
                        sql.Append("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='PO_REVISD'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
                        sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
                        sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = " + PoId + ");  \n");
                        sql.Append(" \n");

                        sql.Append("IF EXISTS(SELECT PO_ID FROM PO_MASTER WHERE PO_ID =" + ParentPoId + " AND IS_APPROVED=0) \n");
                        sql.Append("BEGIN \n");
                        sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='APPRV_PO') \n");
                        sql.Append("	WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + ParentPoId + ") AND \n");
                        sql.Append("	ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID =" + ParentPoId + "); \n");
                        sql.Append("END \n");
                        sql.Append("ELSE IF EXISTS(SELECT PO_ID FROM PO_MASTER WHERE PO_ID =" + ParentPoId + " AND IS_APPROVED=1) \n");
                        sql.Append("BEGIN \n");
                        sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='GNRTE_GRN') \n");
                        sql.Append("	WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + ParentPoId + ") AND \n");
                        sql.Append("	ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID =" + ParentPoId + "); \n");
                        sql.Append("END \n");
                        sql.Append("ELSE \n");
                        sql.Append("BEGIN \n");
                        sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED') \n");
                        sql.Append("	WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + ParentPoId + ") AND \n");
                        sql.Append("	ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID =" + ParentPoId + "); \n");
                        sql.Append("END");
                    }
                }
            }

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<POMaster> GetPoMasterListByDepartmentIdTogrn(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            String sql = "";
            //sql = sql + "SELECT PR.PR_CODE,PR.REQUIRED_FOR,POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,SU.SUPPLIER_NAME,W.LOCATION AS WAREHOUSE_NAME, PM.PURCHASE_TYPE FROM PO_MASTER AS POM " + "\n";
            //sql = sql + "  INNER JOIN(SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER)AS SU " + "\n";
            //sql = sql + "  ON POM.SUPPLIER_ID = SU.SUPPLIER_ID " + "\n";
            //sql = sql + "  INNER JOIN PR_MASTER AS PR " + "\n";
            //sql = sql + "  ON POM.BASED_PR = PR.PR_ID " + "\n";
            //sql = sql + "  INNER JOIN (SELECT PO_ID, QUANTITY, RECEIVED_QTY,WAITING_QTY,STATUS FROM PO_DETAILS) AS POD ON POM.PO_ID = POD.PO_ID " + "\n";
            //sql = sql + "  LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n";
            //sql = sql + "  WHERE PM.PURCHASE_PROCEDURE !=2 AND POD.QUANTITY > (ISNULL(POD.RECEIVED_QTY,0) + ISNULL(POD.WAITING_QTY,0)) AND POD.STATUS !=3 AND POM.IS_APPROVED=1 AND  (POM.IS_CANCELLED = 0 OR POM.IS_CANCELLED IS NULL) AND " + "\n";
            //sql = sql + "  POM.DEPARTMENT_ID = " + departmentid + "\n";
            //sql = sql + "  GROUP BY PR.PR_CODE,PR.REQUIRED_FOR,POM.PO_CODE,POM.CREATED_DATE,SU.SUPPLIER_NAME,POM.PO_ID,W.LOCATION, PM.PURCHASE_TYPE ORDER BY POM.CREATED_DATE DESC";

            sql = sql + "SELECT PR.PR_CODE,PR.REQUIRED_FOR,POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,SU.SUPPLIER_NAME,W.LOCATION AS WAREHOUSE_NAME, PR.PURCHASE_TYPE FROM PO_MASTER AS POM " + "\n";
            sql = sql + "  INNER JOIN(SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER)AS SU " + "\n";
            sql = sql + "  ON POM.SUPPLIER_ID = SU.SUPPLIER_ID " + "\n";
            sql = sql + "  INNER JOIN PR_MASTER AS PR " + "\n";
            sql = sql + "  ON POM.BASED_PR = PR.PR_ID " + "\n";
            sql = sql + "  INNER JOIN (SELECT PO_ID, QUANTITY, RECEIVED_QTY,WAITING_QTY,STATUS FROM PO_DETAILS) AS POD ON POM.PO_ID = POD.PO_ID " + "\n";
            sql = sql + "  LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n";
            sql = sql + "  WHERE PR.PURCHASE_PROCEDURE !=2 AND POD.QUANTITY > (ISNULL(POD.RECEIVED_QTY,0) + ISNULL(POD.WAITING_QTY,0)) AND POD.STATUS !=3 AND POM.IS_APPROVED=1 AND  (POM.IS_CANCELLED = 0 OR POM.IS_CANCELLED IS NULL) AND " + "\n";
            sql = sql + "  POM.DEPARTMENT_ID = " + departmentid + "\n";
            sql = sql + "  GROUP BY PR.PR_CODE,PR.REQUIRED_FOR,POM.PO_CODE,POM.CREATED_DATE,SU.SUPPLIER_NAME,POM.PO_ID,W.LOCATION, PR.PURCHASE_TYPE ORDER BY POM.CREATED_DATE DESC";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> GetPoMasterListByWarehouseIdTogrn(List<int> WarehouseIds, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            String sql = "";
            sql = sql + "  SELECT PM.PR_CODE,PM.REQUIRED_FOR,POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,SU.SUPPLIER_NAME,W.LOCATION AS WAREHOUSE_NAME, PM.PURCHASE_TYPE FROM PO_MASTER AS POM " + "\n";
            sql = sql + "  INNER JOIN(SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER)AS SU " + "\n";
            sql = sql + "  ON POM.SUPPLIER_ID = SU.SUPPLIER_ID " + "\n";
            sql = sql + "  INNER JOIN PR_MASTER AS PM " + "\n";
            sql = sql + "  ON PM.PR_ID = POM.BASED_PR " + "\n";
            sql = sql + "  INNER JOIN (SELECT PO_ID, QUANTITY, RECEIVED_QTY,WAITING_QTY,STATUS FROM PO_DETAILS) AS POD ON POM.PO_ID = POD.PO_ID " + "\n";
            sql = sql + "  LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n";
            sql = sql + "  WHERE PM.PURCHASE_PROCEDURE !=2 AND POD.QUANTITY > (ISNULL(POD.RECEIVED_QTY,0) + ISNULL(POD.WAITING_QTY,0)) AND POM.IS_APPROVED=1 AND (POM.IS_CANCELLED = 0 OR POM.IS_CANCELLED IS NULL) AND " + "\n";
            sql = sql + "  POM.DELIVER_TO_WAREHOUSE IN( " + string.Join(",", WarehouseIds) + ") \n";
            sql = sql + "  GROUP BY PM.PR_CODE,PM.REQUIRED_FOR,POM.PO_CODE,POM.CREATED_DATE,SU.SUPPLIER_NAME,POM.PO_ID,W.LOCATION, PM.PURCHASE_TYPE ORDER BY POM.CREATED_DATE ASC";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public POMaster GetPoMasterToGenerateGRN(int PoId, int CompanyId, DBConnection dbConnection)
        {
            POMaster po;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT POM.PO_ID,PRM.PURCHASE_TYPE, POM.PO_CODE,SK.STORE_KEEPER,POM.BASED_PR,PRM.REQUIRED_FOR,POM.DELIVER_TO_WAREHOUSE,POM.APPROVED_BY,POM.SUPPLIER_ID,POM.PAYMENT_METHOD,PRM.PR_CODE, POM.QUOTATION_ID FROM PO_MASTER AS POM \n" +
                                            "INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n" +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS STORE_KEEPER FROM COMPANY_LOGIN) AS SK ON SK.USER_ID = PRM.CREATED_BY\n" +
                                            "WHERE PO_ID = " + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                po = dataAccessObject.GetSingleOject<POMaster>(dbConnection.dr);
            }

            if (po != null)
            {
                po._Supplier = DAOFactory.createSupplierDAO().GetSupplierBySupplierId(po.SupplierId, dbConnection);
                po.PoDetails = DAOFactory.createPODetailsDAO().GetPODetailsToGenerateGRN(po.PoID, CompanyId, dbConnection);
                po.Warehouse = DAOFactory.CreateWarehouseDAO().getWarehouseByID(po.DeliverToWarehouse, dbConnection);
            }

            return po;
        }
        public List<POMaster> GetAllPosByPrId(int PrId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT POM.*,CLC.FIRST_NAME AS CREATED_USER_NAME,CLA.FIRST_NAME AS APPROVED_USER_NAME,W.LOCATION AS WAREHOUSE_NAME,S.SUPPLIER_NAME,PRM.PR_CODE,POMD.PO_CODE AS PARENT_PO_CODE FROM PO_MASTER AS POM \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLC ON POM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLA ON POM.APPROVED_BY = CLA.USER_ID \n");
            sql.Append("LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n");
            sql.Append("LEFT JOIN PO_MASTER AS POMD ON POM.IS_DERIVED_FROM_PO = POMD.PO_ID \n");
            sql.Append("INNER JOIN SUPPLIER AS S ON POM.SUPPLIER_ID = S.SUPPLIER_ID \n");
            sql.Append("INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n");
            sql.Append("WHERE POM.BASED_PR =" + PrId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<POMaster> GetAPPROVEDPosByPrId(int PrId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM PO_MASTER AS POM \n");
            sql.Append("WHERE POM.IS_APPROVED = 1 AND POM.BASED_PR =" + PrId);

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }
        public List<POMaster> GetAllPosByPrIdFor(List<int> PrId, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT POM.*,CLC.FIRST_NAME AS CREATED_USER_NAME,CLA.FIRST_NAME AS APPROVED_USER_NAME,W.LOCATION AS WAREHOUSE_NAME,S.SUPPLIER_NAME,PRM.PR_CODE,POMD.PO_CODE AS PARENT_PO_CODE FROM PO_MASTER AS POM \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLC ON POM.CREATED_BY = CLC.USER_ID \n");
            sql.Append("LEFT JOIN COMPANY_LOGIN AS CLA ON POM.APPROVED_BY = CLA.USER_ID \n");
            sql.Append("LEFT JOIN WAREHOUSE AS W ON POM.DELIVER_TO_WAREHOUSE = W.WAREHOUSE_ID \n");
            sql.Append("LEFT JOIN PO_MASTER AS POMD ON POM.IS_DERIVED_FROM_PO = POMD.PO_ID \n");
            sql.Append("INNER JOIN SUPPLIER AS S ON POM.SUPPLIER_ID = S.SUPPLIER_ID \n");
            sql.Append("INNER JOIN PR_MASTER AS PRM ON POM.BASED_PR = PRM.PR_ID \n");
            sql.Append("WHERE POM.BASED_PR IN (" + string.Join(",", PrId) + ") ");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }


        public int CreateCoveringPR(int PoId, int PrId, int grnId, int UserId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "[CLONE_FOR_COVERING_PR]";
            dBConnection.cmd.Parameters.AddWithValue("@PO_ID", PoId);
            dBConnection.cmd.Parameters.AddWithValue("@PR_ID", PrId);
            dBConnection.cmd.Parameters.AddWithValue("@GRN_ID", grnId);
            dBConnection.cmd.Parameters.AddWithValue("@CREATED_BY", UserId);
            dBConnection.cmd.Parameters.AddWithValue("@CREATED_ON", LocalTime.Now);

            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }


        public int GenerateCoveringPO(POMaster poMaster, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("BEGIN \n");
            sql.Append(" \n");
            sql.Append("DECLARE @PO_ID TABLE (ID INT) \n");
            sql.Append(" \n");
            sql.Append("INSERT INTO [dbo].[PO_MASTER] \n");
            sql.Append("           ([PO_CODE] \n");
            sql.Append("           ,[DEPARTMENT_ID] \n");
            sql.Append("           ,[SUPPLIER_ID] \n");
            sql.Append("           ,[BASED_PR] \n");
            sql.Append("           ,[CREATED_DATE] \n");
            sql.Append("           ,[CREATED_BY] \n");
            sql.Append("           ,[VAT_AMOUNT] \n");
            sql.Append("           ,[NBT_AMOUNT] \n");
            sql.Append("           ,[TOTAL_AMOUNT] \n");
            sql.Append("           ,[DELIVER_TO_WAREHOUSE] \n");
            sql.Append("           ,[IS_APPROVED] \n");
            sql.Append("           ,[WAS_DERIVED] \n");
            sql.Append("           ,[WAS_DERIVED_TYPE] \n");
            sql.Append("           ,[IS_DERIVED] \n");
            sql.Append("           ,[IS_DERIVED_TYPE] \n");
            sql.Append("           ,[IS_DERIVED_FROM_PO] \n");
            sql.Append("           ,[DERIVING_REASON] \n");
            sql.Append("           ,[PARENT_APPROVED_USER] \n");
            sql.Append("           ,[IS_APPROVED_BY_PARENT_APPROVED_USER] \n");
            sql.Append("           ,[PAYMENT_METHOD] \n");
            sql.Append("           ,[IS_CURRENT]) \n");
            sql.Append("	 OUTPUT INSERTED.PO_ID INTO @PO_ID \n");
            sql.Append("     VALUES \n");
            sql.Append("           ((SELECT CONCAT('PO',COUNT(PO_ID)+1) FROM PO_MASTER WHERE DEPARTMENT_ID=" + poMaster.DepartmentId + "), \n");
            sql.Append("		   " + poMaster.DepartmentId + "," + poMaster.SupplierId + "," + poMaster.BasePr + ", \n");
            sql.Append("		   '" + LocalTime.Now + "', \n");
            sql.Append("		   " + poMaster.CreatedBy + "," + poMaster.VatAmount + "," + poMaster.NBTAmount + "," + poMaster.TotalAmount + "," + poMaster.DeliverToWarehouse + ",0,0,0,1,2," + poMaster.IsDerivedFromPo + ",'Exceeded Ordered Quantity'," + poMaster.ParentApprovedUser + ",0,'" + poMaster.PaymentMethod + "',0); \n");

            for (int i = 0; i < poMaster.PoDetails.Count; i++)
            {
                sql.Append(" \n");
                sql.Append("INSERT INTO [dbo].[PO_DETAILS] \n");
                sql.Append("           ([PO_ID] \n");
                sql.Append("           ,[QUOTATION_ITEM_ID] \n");
                sql.Append("           ,[ITEM_ID] \n");
                sql.Append("           ,[ITEM_PRICE] \n");
                sql.Append("           ,[QUANTITY] \n");
                sql.Append("           ,[VAT_AMOUNT] \n");
                sql.Append("           ,[NBT_AMOUNT] \n");
                sql.Append("           ,[TOTAL_AMOUNT] \n");
                sql.Append("           ,[RECEIVED_QTY] \n");
                sql.Append("           ,[WAITING_QTY] \n");
                sql.Append("           ,[MEASUREMENT_ID] \n");
                sql.Append("           ,[STATUS] \n");
                sql.Append("           ,[SUPPLIER_MENTIONED_ITEM_NAME]) \n");
                sql.Append("     VALUES \n");
                sql.Append("           ((SELECT MAX(ID) FROM @PO_ID), \n");
                sql.Append("		   (SELECT [QUOTATION_ITEM_ID] FROM PO_DETAILS WHERE PO_DETAILS_ID= " + poMaster.PoDetails[i].PodId + "), \n");
                sql.Append("		   " + poMaster.PoDetails[i].ItemId + "," + poMaster.PoDetails[i].ItemPrice + "," + poMaster.PoDetails[i].Quantity + "," + poMaster.PoDetails[i].VatAmount + "," + poMaster.PoDetails[i].NbtAmount + "," + poMaster.PoDetails[i].TotalAmount + ",0," + poMaster.PoDetails[i].Quantity + "," + poMaster.PoDetails[i].MeasurementId + ", 0,'" + poMaster.PoDetails[i].SupplierMentionedItemName + "' ); \n");
                sql.Append(" \n");
            }
            sql.Append("UPDATE PO_MASTER SET WAS_DERIVED = 1 WHERE PO_ID=" + poMaster.IsDerivedFromPo + "; \n");
            sql.Append(" \n");
            sql.Append("SELECT MAX(ID) FROM @PO_ID; \n");
            sql.Append(" \n");
            sql.Append("END");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }
        public int UpdatePrintCount(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE PO_MASTER SET PRINT_COUNT += 1 WHERE PO_ID=" + PoId;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int CheckPoGrns(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "DECLARE @X INT " +
                                            "IF EXISTS " +
                                            "(SELECT* FROM PO_GRN WHERE PO_ID = " + PoId + ") " +
                                            "SELECT count(PO_ID) FROM PO_GRN WHERE PO_ID = " + PoId + " " +
                                            "ELSE " +
                                            "SELECT count(PO_ID) FROM PO_GRN WHERE PO_ID = " + PoId + " ";


            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }
    }
}
