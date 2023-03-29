using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using System.Data;
using System.Data.SqlClient;

namespace CLibrary.Infrastructure
{
  public  interface GrnDAO
    {
        int SaveGrnMaster(int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, string InvoiceNo, DBConnection dbConnection);
        int SaveGrnMasterDup(int GrnId, int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote,int BasedGrn, string InvoiceNo, DBConnection dbConnection);
        int UpdateRejectedGrn(int grnid,int poId, DateTime goodReceivedDate, string grnNote, DBConnection dbConnection);
        GrnMaster GetGrnMasterByPoId(int PoId, DBConnection dbConnection);
        GrnMaster GetGrnMasterByGrnID(int grnId, int poID, DBConnection dbConnection);
        List<GrnMaster> GetGRNmasterListByDepartmentId(int departmentid, DBConnection dbConnection);
        int grnMasterApproval(int grnId, int isApprove, string approvedby, int departmentId, string GrnCode, DBConnection dbConnection);
        List<GrnMaster> GetgrnMasterListByByDateRange(int departmentid, DateTime startdate, DateTime enddate, DBConnection dbConnection);
        List<GrnMaster> GetgrnMasterListByPoCode(int departmentid, string poCode, DBConnection dbConnection);

        List<GrnMaster> GetAllDetailsGrn(int departmentid, DBConnection dbConnection);
        GrnMaster CheckGrnExistMasterByGrnID(int grnId, int poID, DBConnection dbConnection);
        int GetMaxGrnCode(int departmentId, DBConnection dbConnection);
        int GetMaxGrnCodeDup( DBConnection dbConnection);
        List<GrnMaster> GetAllDetailsGrnIsApproved(int departmentid, DBConnection dbConnection);

        List<GrnMaster> FetchApprovedGRNForConfirmation(int Department, DBConnection dbConnection);
        int ConfirmOrDenyGrnApproval(int grnId, int confirm, DBConnection dbConnection);


        //New Methods By Salman created on 2019-03-29
        List<GrnMaster> GetGrnMastersForPrInquiryByPoId(int Poid, DBConnection dbConnection);

        int GenrateCoveringPr(int PoId, int PrId, int CompanyId, int UserId, decimal TotVat, decimal TotNbt, decimal TotAmount, DataTable ItemDetails, DBConnection dbConnection);


        List<GrnMaster> GetAllDetailsGrnforreport(int departmentid, DBConnection dbConnection);

        //Modified for GRN NEW
        int GenerateGRN(GrnMaster grnMaster, DBConnection dbConnection);
        GrnMaster getGrnDetailsByGrnCode(string grnCode, int CompanyId, DBConnection dbConnection);
        List<GrnMaster> GetGeneratedGRNsForPo(int PoId, DBConnection dbConnection);
        List<GrnMaster> grnForApproval(int departmentid, int UserId, DBConnection dbConnection);
        List<GrnMaster> GetGrnsByCompanyId(int CompanyId, DateTime date, DBConnection dbConnection);
        GrnMaster GetGrnMasterByGrnID(int grnId, DBConnection dbConnection);
        int ValidateGrnBeforeApprove(int GrnId, DBConnection dbConnection);
        int ApproveGrn(int GrnId, int UserId, string Remarks, DBConnection dbConnection);
        int RejectGrn(int GrnId, int UserId, string Remarks, DBConnection dbConnection);
        List<GrnMaster> GetGRNmasterListBygrnCode(int departmentid, string GrnCode, DBConnection dbConnection);
        List<GrnMaster> GetGRNmasterListByPOCode(int departmentid, string PoCode, DBConnection dbConnection);
        List<GrnMaster> GetGrnForReturn(int departmentid, DBConnection dbConnection);
        int UpdateGrnCoverigPR(int grnId, int CompayId, DBConnection dbConnection);
    }
    
    public class GrnDAOImpl : GrnDAO
    {
        public GrnMaster GetGrnMasterByGrnID(int grnId, int poID, DBConnection dbConnection)
        {
            GrnMaster GetGrnMasterObj = new GrnMaster();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_MASTER\"  WHERE \"GRN_ID\" = " + grnId + " AND \"PO_ID\" = " + poID + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterObj = dataAccessObject.GetSingleOject<GrnMaster>(dbConnection.dr);

            }
            GetGrnMasterObj._GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(grnId, poID, dbConnection);
            return GetGrnMasterObj;
        }

        public GrnMaster GetGrnMasterByPoId(int PoId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetgrnMasterListByByDateRange(int departmentid, DateTime startdate, DateTime enddate, DBConnection dbConnection)
        {
            List<GrnMaster> GetGrnMasterList = new List<GrnMaster>();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_MASTER\"  WHERE \"DEPARTMENT_ID\" =" + departmentid + " AND  ( \"GOOD_RECEIVED_DATE\" BETWEEN  '" + startdate + "' AND  '" + enddate + "')";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterList = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
            foreach (var item in GetGrnMasterList)
            {
                item._GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(item.GrnId, item.PoId, dbConnection);
                item._POMaster = pOMasterDAO.GetPoMasterObjByPoId(item.PoId, dbConnection);
                item._Supplier = supplierDAO.GetSupplierBySupplierId(item.Supplierid, dbConnection);
            }
            return GetGrnMasterList;
        }

        public List<GrnMaster> GetgrnMasterListByPoCode(int departmentid, string poCode, DBConnection dbConnection)
        {
            List<GrnMaster> GetGrnMasterList = new List<GrnMaster>();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_MASTER\" AS gm WHERE gm.\"PO_ID\" = (SELECT \"PO_ID\"  FROM public.\"PO_MASTER\" AS pm WHERE  pm.\"PO_CODE\" =  '" + poCode + "')";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterList = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);

            }
            foreach (var item in GetGrnMasterList)
            {
                item._GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(item.GrnId, item.PoId, dbConnection);
                item._POMaster = pOMasterDAO.GetPoMasterObjByPoId(item.PoId, dbConnection);
                item._Supplier = supplierDAO.GetSupplierBySupplierId(item.Supplierid, dbConnection);
            }
            return GetGrnMasterList;
        }

        public List<GrnMaster> GetGRNmasterListByDepartmentId(int departmentid, DBConnection dbConnection)
        {
            List<GrnMaster> GetGrnMasterList = new List<GrnMaster>();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            POMasterDAO POMasterDAO = DAOFactory.createPOMasterDAO();
            PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_MASTER\"  WHERE \"DEPARTMENT_ID\" =" + departmentid;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterList = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
            foreach (var item in GetGrnMasterList)
            {
                item._Supplier = supplierDAO.GetSupplierBySupplierId(item.Supplierid, dbConnection);
                item._GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(item.GrnId, item.PoId, dbConnection);
                item._POMaster = POMasterDAO.GetPoMasterObjByPoId(item.PoId, dbConnection);
                item._PRMaster = pr_MasterDAO.FetchRejectPR(item._POMaster.BasePr, dbConnection);
            }
            return GetGrnMasterList;
        }

        public int grnMasterApproval(int grnId, int isApprove, string approvedby, int departmentId, string GrnCode, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"GRN_MASTER\" SET \"IS_APPROVED\" = " + isApprove + " , \"APPROVED_BY\"  = '" + approvedby + "', \"APPROVED_DATE\" = '" + LocalTime.Now + "',\"GRN_CODE\"='" + GrnCode + "'  WHERE \"GRN_ID\" = " + grnId + " AND \"DEPARTMENT_ID\" = " + departmentId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveGrnMaster(int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, string InvoiceNo, DBConnection dbConnection)
        {
            int grnId = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"GRN_MASTER\"";
            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                grnId = 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (\"GRN_ID\")+1 AS MAXid FROM public.\"GRN_MASTER\"";
                grnId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            dbConnection.cmd.CommandText = "INSERT INTO public.\"GRN_MASTER\" (\"GRN_ID\", \"PO_ID\",\"DEPARTMENT_ID\", \"SUPPLIER_ID\", \"TOTAL_AMOUNT\", \"CREATED_BY\", \"CREATED_DATE\", \"GRN_NOTE\", \"GOOD_RECEIVED_DATE\", \"IS_APPROVED\", \"APPROVED_BY\", \"APPROVED_DATE\") VALUES" +
                                                                   " ( " + grnId + ", " + poId + "," + companyId + "," + supplierid + ", " + totalAmount + ", '" + createdBy + "', '" + createdDate + "', '" + grnNote + "', '" + goodReceivedDate + "', " + 0 + ",'Admin' ,'" + LocalTime.Now + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();

            return grnId;
        }

        public int SaveGrnMasterDup(int GrnId, int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, int BasedGrn, string InvoiceNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "INSERT INTO public.\"GRN_MASTER\" (\"GRN_ID\", \"PO_ID\",\"DEPARTMENT_ID\", \"SUPPLIER_ID\", \"TOTAL_AMOUNT\", \"CREATED_BY\", \"CREATED_DATE\", \"GRN_NOTE\", \"GOOD_RECEIVED_DATE\", \"IS_APPROVED\", \"APPROVED_BY\", \"APPROVED_DATE\",\"BASED_GRN_ID\") VALUES" +
                                                                   " ( " + GrnId + ", " + poId + "," + companyId + "," + supplierid + ", " + totalAmount + ", '" + createdBy + "', '" + createdDate + "', '" + grnNote + "', '" + goodReceivedDate + "', " + 0 + ",'Admin' ,'" + LocalTime.Now + "'," + BasedGrn + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateRejectedGrn(int grnid, int poId, DateTime goodReceivedDate, string grnNote, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"GRN_MASTER\" SET \"IS_APPROVED\" = " + 0 + " , \"GRN_NOTE\" = '" + grnNote + "' , \"GOOD_RECEIVED_DATE\" = '" + goodReceivedDate + "'   WHERE \"GRN_ID\" = " + grnid + " AND \"PO_ID\" = " + poId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnMaster> GetAllDetailsGrn(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " select * from public.\"GRN_MASTER\" " +
                                           " inner join public.\"GRN_DETAILS\" on public.\"GRN_DETAILS\".\"GRN_ID\" = public.\"GRN_MASTER\".\"GRN_ID\" " +
                                           " inner join public.\"SUPPLIER\" on public.\"SUPPLIER\".\"SUPPLIER_ID\" = public.\"GRN_MASTER\".\"SUPPLIER_ID\" " +
                                           " inner join public.\"PO_MASTER\" on public.\"PO_MASTER\".\"PO_ID\" = public.\"GRN_MASTER\".\"PO_ID\" " +
                                           " inner join public.\"PR_MASTER\" on public.\"PR_MASTER\".\"PR_ID\" = public.\"PO_MASTER\".\"BASED_PR\" " +
                                           " where public.\"GRN_MASTER\".\"DEPARTMENT_ID\" = " + departmentid + " AND public.\"GRN_DETAILS\".\"IS_GRN_RAISED\" = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
        }

        public GrnMaster CheckGrnExistMasterByGrnID(int grnId, int poID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_MASTER\"  WHERE public.\"GRN_MASTER\".\"GRN_ID\" = " + grnId + " AND public.\"GRN_MASTER\".\"PO_ID\" = " + poID + " AND  public.\"GRN_MASTER\".\"IS_APPROVED\" = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<GrnMaster>(dbConnection.dr);
            }
        }

        public int GetMaxGrnCode(int departmentId, DBConnection dbConnection)
        {
            string GrnId = string.Empty;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT Max(public.\"GRN_MASTER\".\"GRN_ID\") As MaxGrn FROM public.\"GRN_MASTER\"  WHERE public.\"GRN_MASTER\".\"DEPARTMENT_ID\" = " + departmentId + "  AND  public.\"GRN_MASTER\".\"IS_APPROVED\" = 1";
            GrnId = dbConnection.cmd.ExecuteScalar().ToString();
            int newGrnId = 0;
            if (GrnId == null || GrnId == "")
            {
                newGrnId = 0;
            }
            else
            {
                newGrnId = int.Parse(GrnId);
            }

            return newGrnId;
        }

        public int GetMaxGrnCodeDup(DBConnection dbConnection)
        {
            string GrnId = string.Empty;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT Count(public.\"GRN_MASTER\".\"GRN_ID\") As MaxGrn FROM public.\"GRN_MASTER\"  ";
            GrnId = dbConnection.cmd.ExecuteScalar().ToString();
            int newGrnId = 0;
            if (GrnId == null || GrnId == "")
            {
                newGrnId = 0;
            }
            else
            {
                newGrnId = int.Parse(GrnId);
            }

            return newGrnId;
        }

        public List<GrnMaster> GetAllDetailsGrnIsApproved(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " select * from public.\"GRN_MASTER\" " +
                                           " inner join public.\"GRN_DETAILS\" on public.\"GRN_DETAILS\".\"GRN_ID\" = public.\"GRN_MASTER\".\"GRN_ID\" " +
                                           " inner join public.\"SUPPLIER\" on public.\"SUPPLIER\".\"SUPPLIER_ID\" = public.\"GRN_MASTER\".\"SUPPLIER_ID\" " +
                                           " inner join public.\"PO_MASTER\" on public.\"PO_MASTER\".\"PO_ID\" = public.\"GRN_MASTER\".\"PO_ID\" " +
                                           " inner join public.\"PR_MASTER\" on public.\"PR_MASTER\".\"PR_ID\" = public.\"PO_MASTER\".\"BASED_PR\" " +
                                           " where public.\"GRN_MASTER\".\"DEPARTMENT_ID\" = " + departmentid + " AND public.\"GRN_DETAILS\".\"IS_GRN_RAISED\" = 1  AND public.\"GRN_MASTER\".\"IS_APPROVED\" = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
        }

        public int ConfirmOrDenyGrnApproval(int grnId, int confirm, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> FetchApprovedGRNForConfirmation(int Department, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetGrnMastersForPrInquiryByPoId(int Poid, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int GenrateCoveringPr(int PoId, int PrId, int CompanyId, int UserId, decimal TotVat, decimal TotNbt, decimal TotAmount, DataTable ItemDetails, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetAllDetailsGrnforreport(int departmentid, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int GenerateGRN(GrnMaster grnMaster, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public GrnMaster getGrnDetailsByGrnCode(string grnCode, int CompanyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetGeneratedGRNsForPo(int PoId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> grnForApproval(int departmentid, int UserId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetGrnsByCompanyId(int CompanyId, DateTime date, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public GrnMaster GetGrnMasterByGrnID(int grnId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int ValidateGrnBeforeApprove(int GrnId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int ApproveGrn(int GrnId, int UserId, string Remarks, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int RejectGrn(int GrnId, int UserId, string Remarks, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetGRNmasterListBygrnCode(int departmentid, string GrnCode, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetGRNmasterListByPOCode(int departmentid, string PoCode, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetGrnForReturn(int departmentid, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public int UpdateGrnCoverigPR(int grnId, int CompayId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }
    }

    public class GrnDAOSQLImpl : GrnDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public GrnMaster GetGrnMasterByGrnID(int grnId, int poID, DBConnection dbConnection)
        {
            GrnMaster GetGrnMasterObj = new GrnMaster();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER  WHERE GRN_ID = " + grnId + " AND PO_ID = " + poID + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterObj = dataAccessObject.GetSingleOject<GrnMaster>(dbConnection.dr);

            }
            GetGrnMasterObj._GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(grnId, poID, dbConnection);
            return GetGrnMasterObj;
        }

        public GrnMaster GetGrnMasterByPoId(int PoId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnMaster> GetgrnMasterListByByDateRange(int departmentid, DateTime startdate, DateTime enddate, DBConnection dbConnection)
        {
            List<GrnMaster> GetGrnMasterList = new List<GrnMaster>();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER  WHERE DEPARTMENT_ID =" + departmentid + " AND  ( GOOD_RECEIVED_DATE BETWEEN  '" + startdate + "' AND  '" + enddate + "')";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterList = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
            foreach (var item in GetGrnMasterList)
            {
                item._GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(item.GrnId, item.PoId, dbConnection);
                item._POMaster = pOMasterDAO.GetPoMasterObjByPoId(item.PoId, dbConnection);
                item._Supplier = supplierDAO.GetSupplierBySupplierId(item.Supplierid, dbConnection);
            }
            return GetGrnMasterList;
        }

        public List<GrnMaster> GetgrnMasterListByPoCode(int departmentid, string poCode, DBConnection dbConnection)
        {
            List<GrnMaster> GetGrnMasterList = new List<GrnMaster>();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER AS gm WHERE gm.PO_ID = (SELECT PO_ID  FROM " + dbLibrary + ".PO_MASTER AS pm WHERE  pm.PO_CODE =  '" + poCode + "')";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterList = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);

            }
            foreach (var item in GetGrnMasterList)
            {
                item._GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(item.GrnId, item.PoId, dbConnection);
                item._POMaster = pOMasterDAO.GetPoMasterObjByPoId(item.PoId, dbConnection);
                item._Supplier = supplierDAO.GetSupplierBySupplierId(item.Supplierid, dbConnection);
            }
            return GetGrnMasterList;
        }

        public List<GrnMaster> GetGRNmasterListByDepartmentId(int departmentid, DBConnection dbConnection)
        {
            List<GrnMaster> GetGrnMasterList = new List<GrnMaster>();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            POMasterDAO POMasterDAO = DAOFactory.createPOMasterDAO();
            PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER AS GM  " +
                "LEFT JOIN PO_GRN AS PG ON PG.GRN_ID = GM.GRN_ID "+
                "LEFT JOIN (SELECT PO_CODE, PO_ID FROM PO_MASTER) AS PM ON PM.PO_ID = PG.PO_ID "+
                "LEFT JOIN (SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER) AS SUP ON SUP.SUPPLIER_ID = GM.SUPPLIER_ID " +
                "WHERE GM.TOTAL_AMOUNT > 0 AND DEPARTMENT_ID =" + departmentid;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterList = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
            foreach (var item in GetGrnMasterList)
            {
                item._Supplier = supplierDAO.GetSupplierBySupplierId(item.Supplierid, dbConnection);
                item._GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(item.GrnId, item.PoId, dbConnection);
                item._POMaster = POMasterDAO.GetPoMasterObjByPoId(item.PoId, dbConnection);
                item._PRMaster = pr_MasterDAO.FetchRejectPR(item._POMaster.BasePr, dbConnection);
            }
            return GetGrnMasterList;
        }

        public List<GrnMaster> GetGRNmasterListByPOCode(int departmentid, string PoCode,  DBConnection dbConnection) {
            List<GrnMaster> GetGrnMasterList = new List<GrnMaster>();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            POMasterDAO POMasterDAO = DAOFactory.createPOMasterDAO();
            PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PO_MASTER AS PO  " +
                "LEFT JOIN PO_GRN AS PG ON PG.PO_ID = PO.PO_ID " +
                "LEFT JOIN GRN_MASTER AS GRN ON GRN.GRN_ID = PG.GRN_ID " +
                "LEFT JOIN (SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER) AS SUP ON SUP.SUPPLIER_ID = GRN.SUPPLIER_ID " +
                "WHERE GRN.TOTAL_AMOUNT > 0 AND GRN.DEPARTMENT_ID = " + departmentid+" AND PO.PO_CODE = '"+ PoCode + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterList = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
            
            return GetGrnMasterList;
        }

        public List<GrnMaster> GetGRNmasterListBygrnCode(int departmentid, string GrnCode, DBConnection dbConnection) {
            List<GrnMaster> GetGrnMasterList = new List<GrnMaster>();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            POMasterDAO POMasterDAO = DAOFactory.createPOMasterDAO();
            PR_MasterDAO pr_MasterDAO = DAOFactory.CreatePR_MasterDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER AS GM  " +
                "LEFT JOIN PO_GRN AS PG ON PG.GRN_ID = GM.GRN_ID " +
                "LEFT JOIN (SELECT PO_CODE, PO_ID FROM PO_MASTER) AS PM ON PM.PO_ID = PG.PO_ID " +
                "LEFT JOIN (SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER) AS SUP ON SUP.SUPPLIER_ID = GM.SUPPLIER_ID " +
                "WHERE GM.TOTAL_AMOUNT > 0 AND DEPARTMENT_ID =" + departmentid+" AND GRN_CODE = '"+ GrnCode + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterList = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }

            return GetGrnMasterList;
        }

        public int grnMasterApproval(int grnId, int isApprove, string approvedby, int departmentId, string GrnCode, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".GRN_MASTER SET IS_APPROVED = " + isApprove + " , APPROVED_BY  = '" + approvedby + "', APPROVED_DATE = '" + LocalTime.Now + "',GRN_CODE='" + GrnCode + "'  WHERE GRN_ID = " + grnId + " AND DEPARTMENT_ID = " + departmentId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveGrnMaster(int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, string InvoiceNo, DBConnection dbConnection)
        {
            int grnId = 0;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".GRN_MASTER";
            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                grnId = 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (GRN_ID)+1 AS MAXid FROM " + dbLibrary + ".GRN_MASTER";
                grnId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".GRN_MASTER (GRN_ID, PO_ID,DEPARTMENT_ID, SUPPLIER_ID, TOTAL_AMOUNT, CREATED_BY, CREATED_DATE, GRN_NOTE, GOOD_RECEIVED_DATE, IS_APPROVED, APPROVED_BY, APPROVED_DATE,INVOICE_NO) VALUES" +
                                                                   " ( " + grnId + ", " + poId + "," + companyId + "," + supplierid + ", " + totalAmount + ", '" + createdBy + "', '" + createdDate + "', '" + grnNote + "', '" + goodReceivedDate + "', " + 0 + ",'Admin' ,'" + LocalTime.Now + "','" + InvoiceNo + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.ExecuteNonQuery();

            return grnId;
        }

        public int SaveGrnMasterDup(int GrnId, int poId, int companyId, int supplierid, DateTime goodReceivedDate, decimal totalAmount, string createdBy, DateTime createdDate, string grnNote, int BasedGrn, string InvoiceNo, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".GRN_MASTER (GRN_ID, PO_ID,DEPARTMENT_ID, SUPPLIER_ID, TOTAL_AMOUNT, CREATED_BY, CREATED_DATE, GRN_NOTE, GOOD_RECEIVED_DATE, IS_APPROVED, APPROVED_BY, APPROVED_DATE,BASED_GRN_ID ,INVOICE_NO) VALUES" +
                                                                   " ( " + GrnId + ", " + poId + "," + companyId + "," + supplierid + ", " + totalAmount + ", '" + createdBy + "', '" + createdDate + "', '" + grnNote + "', '" + goodReceivedDate + "', " + 0 + ",'Admin' ,'" + LocalTime.Now + "'," + BasedGrn + ",'" +InvoiceNo + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateRejectedGrn(int grnid, int poId, DateTime goodReceivedDate, string grnNote, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".GRN_MASTER SET IS_APPROVED = " + 0 + " , GRN_NOTE = '" + grnNote + "' , GOOD_RECEIVED_DATE = '" + goodReceivedDate + "'   WHERE GRN_ID = " + grnid + " AND PO_ID = " + poId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnMaster> GetAllDetailsGrn(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  GM.GRN_ID,GM.GRN_CODE,POM.PO_ID,POM.PO_CODE,POM.BASED_PR,PR.PR_CODE,SU.SUPPLIER_NAME,GM.CREATED_DATE,GM.IS_APPROVED,GD.IS_GRN_RAISED,SD.DEPARTMENT_NAME,PR.REQUIRED_FOR FROM " + dbLibrary + ".GRN_MASTER AS GM " +
                                            "INNER JOIN " + dbLibrary + ".GRN_DETAILS AS GD ON GD.GRN_ID = GM.GRN_ID " +
                                            "INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID = GM.SUPPLIER_ID " +
                                            "INNER JOIN " + dbLibrary + ".PO_MASTER AS POM ON POM.PO_ID = GM.PO_ID " +
                                            "INNER JOIN " + dbLibrary + ".PR_MASTER AS PR ON PR.PR_ID = POM.BASED_PR " +
                                             "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PR.MRNREFERENCE_NO = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                            "WHERE GM.DEPARTMENT_ID = " + departmentid + " AND GD.IS_GRN_RAISED = 1  AND GM.IS_APPROVED = 0 "+
                                            "GROUP BY  GM.GRN_ID,GM.GRN_CODE,POM.PO_ID,POM.PO_CODE,POM.BASED_PR,PR.PR_CODE,SU.SUPPLIER_NAME,GM.CREATED_DATE,GM.IS_APPROVED,GD.IS_GRN_RAISED,SD.DEPARTMENT_NAME,PR.REQUIRED_FOR";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
        }

        public GrnMaster CheckGrnExistMasterByGrnID(int grnId, int poID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER  WHERE GRN_ID = " + grnId + " AND PO_ID = " + poID + " AND  IS_APPROVED = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<GrnMaster>(dbConnection.dr);
            }
        }

        public int GetMaxGrnCode(int departmentId, DBConnection dbConnection)
        {
            string GrnId = string.Empty;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT Max(GRN_ID) As MaxGrn FROM  " + dbLibrary + ".GRN_MASTER  WHERE DEPARTMENT_ID = " + departmentId + "  AND  IS_APPROVED = 1";
            GrnId = dbConnection.cmd.ExecuteScalar().ToString();
            int newGrnId = 0;
            if (GrnId == null || GrnId == "")
            {
                newGrnId = 0;
            }
            else
            {
                newGrnId = int.Parse(GrnId);
            }

            return newGrnId;
        }

        public int GetMaxGrnCodeDup(DBConnection dbConnection)
        {
            string GrnId = string.Empty;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT Count(GRN_ID) As MaxGrn FROM " + dbLibrary + ".GRN_MASTER  ";
            GrnId = dbConnection.cmd.ExecuteScalar().ToString();
            int newGrnId = 0;
            if (GrnId == null || GrnId == "")
            {
                newGrnId = 0;
            }
            else
            {
                newGrnId = int.Parse(GrnId);
            }

            return newGrnId;
        }

        public List<GrnMaster> GetAllDetailsGrnIsApproved(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT GM.GRN_ID,GM.GRN_CODE,GM.GOOD_RECEIVED_DATE,POM.PO_ID,POM.PO_CODE,POM.BASED_PR,PR.PR_CODE,SU.SUPPLIER_NAME,GM.CREATED_DATE,GM.IS_APPROVED,GD.IS_GRN_RAISED,SD.DEPARTMENT_NAME,PR.REQUIRED_FOR FROM " + dbLibrary + ".GRN_MASTER AS GM " +
                                             "INNER JOIN " + dbLibrary + ".GRN_DETAILS AS GD ON GD.GRN_ID = GM.GRN_ID " +
                                             "INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID = GM.SUPPLIER_ID " +
                                             "INNER JOIN " + dbLibrary + ".PO_MASTER AS POM ON POM.PO_ID = GM.PO_ID " +
                                             "INNER JOIN " + dbLibrary + ".PR_MASTER AS PR ON PR.PR_ID = POM.BASED_PR " +
                                               "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PR.MRNREFERENCE_NO = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                             "WHERE GM.DEPARTMENT_ID = " + departmentid + " AND GD.IS_GRN_RAISED = 1  AND GM.IS_APPROVED = 1 " +
                                             "GROUP BY  GM.GRN_ID,GM.GRN_CODE,GM.GOOD_RECEIVED_DATE,POM.PO_ID,POM.PO_CODE,POM.BASED_PR,PR.PR_CODE,SU.SUPPLIER_NAME,GM.CREATED_DATE,GM.IS_APPROVED,GD.IS_GRN_RAISED,SD.DEPARTMENT_NAME,PR.REQUIRED_FOR";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
        }

        public int ConfirmOrDenyGrnApproval(int grnId,int confirm, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".GRN_MASTER SET GRN_IS_CONFIRMED_APPROVAL = " + confirm + " WHERE GRN_ID = " + grnId;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateGrnCoverigPR(int grnId, int CompayId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".GRN_MASTER SET CLONED_COVERING_PR = (SELECT MAX(PR_ID) FROM PR_MASTER) WHERE GRN_ID = " + grnId +" AND DEPARTMENT_ID = "+ CompayId + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnMaster> FetchApprovedGRNForConfirmation(int Department, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER WHERE DEPARTMENT_ID=" + Department + " AND IS_APPROVED= 1 AND GRN_IS_CONFIRMED_APPROVAL=0 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
        }

        public List<GrnMaster> GetGrnMastersForPrInquiryByPoId(int Poid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER AS GRNM " +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON GRNM.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "INNER JOIN (SELECT PO_ID,PO_CODE FROM PO_MASTER) AS PM ON PM.PO_ID = GRNM.PO_ID\n" +
                "WHERE GRNM.PO_ID=" + Poid;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
        }

        public int GenrateCoveringPr(int PoId, int PrId, int CompanyId, int UserId, decimal TotVat, decimal TotNbt, decimal TotAmount, DataTable ItemDetails, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "[GENERATE_COVERING_PR]";
            dbConnection.cmd.Parameters.AddWithValue("@PO_ID", PoId);
            dbConnection.cmd.Parameters.AddWithValue("@PR_ID", PrId);
            dbConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", CompanyId);
            dbConnection.cmd.Parameters.AddWithValue("@USER_ID", UserId);
            dbConnection.cmd.Parameters.AddWithValue("@TOT_VAT", TotVat);
            dbConnection.cmd.Parameters.AddWithValue("@TOT_NBT", TotNbt);
            dbConnection.cmd.Parameters.AddWithValue("@TOT_AMOUNT", TotAmount);

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ITEM_DETAILS";
            param.Value = ItemDetails;
            dbConnection.cmd.Parameters.Add(param);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnMaster> GetAllDetailsGrnforreport(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  GM.GRN_ID,GM.GRN_CODE,GM.GOOD_RECEIVED_DATE,POM.PO_ID,POM.PO_CODE,POM.BASED_PR,PR.PR_CODE,SU.SUPPLIER_NAME,GM.CREATED_DATE,GM.IS_APPROVED,GD.IS_GRN_RAISED,SD.DEPARTMENT_NAME,PR.REQUIRED_FOR FROM " + dbLibrary + ".GRN_MASTER AS GM " +
                                            "INNER JOIN " + dbLibrary + ".GRN_DETAILS AS GD ON GD.GRN_ID = GM.GRN_ID " +
                                            "INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID = GM.SUPPLIER_ID " +
                                            "INNER JOIN " + dbLibrary + ".PO_MASTER AS POM ON POM.PO_ID = GM.PO_ID " +
                                            "INNER JOIN " + dbLibrary + ".PR_MASTER AS PR ON PR.PR_ID = POM.BASED_PR " +
                                             "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PR.MRNREFERENCE_NO = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                            "WHERE GM.DEPARTMENT_ID = " + departmentid + " AND GD.IS_GRN_RAISED = 1 " +
                                            "GROUP BY  GM.GRN_ID,GM.GRN_CODE,GM.GOOD_RECEIVED_DATE,POM.PO_ID,POM.PO_CODE,POM.BASED_PR,PR.PR_CODE,SU.SUPPLIER_NAME,GM.CREATED_DATE,GM.IS_APPROVED,GD.IS_GRN_RAISED,SD.DEPARTMENT_NAME,PR.REQUIRED_FOR";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
        }





        public List<GrnMaster> GetGrnForReturn(int departmentid, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  * FROM GRN_MASTER AS GM " +
                                            "LEFT JOIN ( SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER) AS SU ON SU.SUPPLIER_ID = GM.SUPPLIER_ID " +
                                            "LEFT JOIN ( SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME FROM COMPANY_LOGIN) AS CCL ON CCL.USER_ID = GM.CREATED_BY " +
                                            "LEFT JOIN ( SELECT USER_ID, FIRST_NAME AS APPROVED_USER_NAME FROM COMPANY_LOGIN) AS ACL ON ACL.USER_ID = GM.APPROVED_BY " +
                                            "LEFT JOIN ( SELECT WAREHOUSE_ID, LOCATION AS WAREHOUSE_NAME FROM WAREHOUSE) AS W ON W.WAREHOUSE_ID = GM.WAREHOUSE_ID " +
                                            "WHERE IS_APPROVED != 2 AND GM.DEPARTMENT_ID = " + departmentid + " AND GRN_ID IN (SELECT GRN_ID FROM GRN_DETAILS WHERE QUANTITY > 0) ";

           dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }
        }



        public int GenerateGRN(GrnMaster grnMaster, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("BEGIN \n");
            sql.Append(" \n");
            sql.Append("DECLARE @GRN_ID INT \n");
            sql.Append("DECLARE @GRN_CODE INT \n");
            sql.Append(" \n");
            sql.Append("SELECT @GRN_ID= ISNULL(MAX(GRN_ID),0)+1 FROM GRN_MASTER \n");
            sql.Append("SELECT @GRN_CODE= COUNT(GRN_ID)+1 FROM GRN_MASTER WHERE DEPARTMENT_ID=" + grnMaster.CompanyId + " \n");
            sql.Append(" \n");


            sql.Append("INSERT INTO GRN_MASTER \n");
            sql.Append("([GRN_ID],[TOTAL_AMOUNT],[CREATED_BY],[CREATED_DATE],[GRN_NOTE],[GOOD_RECEIVED_DATE],[IS_APPROVED],[DEPARTMENT_ID],[GRN_CODE],[SUPPLIER_ID],[NBT_TOTAL],[VAT_TOTAL],[WAREHOUSE_ID]) \n");
            sql.Append("VALUES \n");
            sql.Append("(@GRN_ID," + grnMaster.TotalAmount + ",'" + grnMaster.CreatedBy + "','" + grnMaster.CreatedDate + "','" + grnMaster.GrnNote.ProcessString() + "','" + grnMaster.GoodReceivedDate + "',0," + grnMaster.CompanyId + ",(SELECT CONCAT('GRN',@GRN_CODE))," + grnMaster.Supplierid + "," + grnMaster.TotalNbt + "," + grnMaster.TotalVat + "," + grnMaster.WarehouseId + "); \n");
            sql.Append(" \n");

            for (int i = 0; i < grnMaster.GrnDetailsList.Count; i++)
            {
                sql.Append(" \n");
                sql.Append("INSERT INTO GRN_DETAILS \n");
                sql.Append("([GRN_ID],[ITEM_ID],[ITEM_PRICE],[QUANTITY],[ISSUED_QTY],[VAT_AMOUNT],[NBT_AMOUNT],[TOTAL_AMOUNT],[FREE_QTY],[EXPIRY_DATE],MEASUREMENT_ID, SUPPLIER_MENTIONED_ITEM_NAME, POD_ID) \n");
                sql.Append("VALUES \n");

                if (grnMaster.GrnDetailsList[i].ExpiryDate != DateTime.MinValue)
                {
                    sql.Append("(@GRN_ID," + grnMaster.GrnDetailsList[i].ItemId + "," + grnMaster.GrnDetailsList[i].ItemPrice + "," + grnMaster.GrnDetailsList[i].Quantity + ",0," + grnMaster.GrnDetailsList[i].VatAmount + "," + grnMaster.GrnDetailsList[i].NbtAmount + "," + grnMaster.GrnDetailsList[i].TotalAmount + ", " + grnMaster.GrnDetailsList[i].FreeQty + ", '" + grnMaster.GrnDetailsList[i].ExpiryDate + "'," + grnMaster.GrnDetailsList[i].MeasurementId + ", '" + grnMaster.GrnDetailsList[i].SupplierMentionedItemName + "', " + grnMaster.GrnDetailsList[i].PodId + "); \n");
                    sql.Append(" \n");
                }
                else
                {
                    sql.Append("(@GRN_ID," + grnMaster.GrnDetailsList[i].ItemId + "," + grnMaster.GrnDetailsList[i].ItemPrice + "," + grnMaster.GrnDetailsList[i].Quantity + ",0," + grnMaster.GrnDetailsList[i].VatAmount + "," + grnMaster.GrnDetailsList[i].NbtAmount + "," + grnMaster.GrnDetailsList[i].TotalAmount + ", " + grnMaster.GrnDetailsList[i].FreeQty + ", NULL," + grnMaster.GrnDetailsList[i].MeasurementId + ", '" + grnMaster.GrnDetailsList[i].SupplierMentionedItemName + "', " + grnMaster.GrnDetailsList[i].PodId + "); \n");
                    sql.Append(" \n");
                }

                sql.Append("UPDATE PO_DETAILS SET WAITING_QTY = ISNULL(WAITING_QTY,0) + " + grnMaster.GrnDetailsList[i].WaitingQty + " WHERE PO_DETAILS_ID=" + grnMaster.GrnDetailsList[i].PodId + "; \n");
                sql.Append(" \n");
                sql.Append("INSERT INTO PR_DETAIL_STATUS_LOG \n");
                sql.Append("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='GRNCRTD'),'" + LocalTime.Now + "'," + grnMaster.CreatedBy + " FROM PR_DETAIL \n");
                sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + grnMaster.PoIds[0] + ") AND \n");
                sql.Append("ITEM_ID = " + grnMaster.GrnDetailsList[i].ItemId + ";  \n");
                sql.Append(" \n");

            }

            for (int i = 0; i < grnMaster.PoIds.Count; i++)
            {
                sql.Append("INSERT INTO PO_GRN \n");
                sql.Append("VALUES \n");
                sql.Append("(" + grnMaster.PoIds[i] + ",@GRN_ID); \n");
                sql.Append(" \n");
            }

            for (int i = 0; i < grnMaster.UploadedFiles.Count; i++)
            {
                sql.Append("INSERT INTO GRN_FILES (GRN_ID, FILE_NAME, LOCATION) \n");
                sql.Append("VALUES \n");
                sql.Append("(@GRN_ID, '" + grnMaster.UploadedFiles[i].FileName.ProcessString() + "', '" + grnMaster.UploadedFiles[i].Location.ProcessString() + "'); \n");
                sql.Append(" \n");
            }

            sql.Append("SELECT @GRN_ID \n");
            sql.Append("END");

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public GrnMaster getGrnDetailsByGrnCode(string grnCode, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            SupplierDAO supplierDAO = DAOFactory.createSupplierDAO();
            PR_MasterDAO pRMMasterDAO = DAOFactory.CreatePR_MasterDAO();
            CompanyDepartmentDAO companyDepartmentDAO = DAOFactory.createCompanyDepartment();
            POMasterDAO pOMasterDAO = DAOFactory.createPOMasterDAO();
            GrnDAO grnDAO = DAOFactory.createGrnDAO();
            WarehouseDAOInterface WarehouseDAOInterface = DAOFactory.CreateWarehouseDAO();

            GrnMaster GetGrnMasterObj = new GrnMaster();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER  AS GRN " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_USER_NAME, DIGITAL_SIGNATURE AS APPROVED_SIGNATURE FROM COMPANY_LOGIN) AS CL ON GRN.APPROVED_BY = CL.USER_ID\n" +
                                             "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME, DIGITAL_SIGNATURE AS CREATED_SIGNATURE FROM COMPANY_LOGIN) AS CLL ON GRN.CREATED_BY = CLL.USER_ID\n" +
                                             "WHERE GRN_CODE = '" + grnCode + "' AND DEPARTMENT_ID = " + CompanyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterObj = dataAccessObject.GetSingleOject<GrnMaster>(dbConnection.dr);

            }

            List<string> poCodes = DAOFactory.createPOMasterDAO().getPoDetailsByGrnId(GetGrnMasterObj.GrnId, dbConnection);

            GetGrnMasterObj.POCode = poCodes[0];
            GetGrnMasterObj.PrCode = poCodes[1];

            GetGrnMasterObj.QuotationFor = DAOFactory.CreatePR_MasterDAO().GetQuotationForbyPrCode(GetGrnMasterObj.CompanyId, GetGrnMasterObj.PrCode, dbConnection);

            GetGrnMasterObj.GrnDetailsList = gRNDetailsDAO.GetGrnDetailsByPoId(GetGrnMasterObj.GrnId, GetGrnMasterObj.CompanyId, dbConnection);
            GetGrnMasterObj._companyDepartment = companyDepartmentDAO.GetDepartmentByDepartmentId(GetGrnMasterObj.CompanyId, dbConnection);
            GetGrnMasterObj._Supplier = supplierDAO.GetSupplierBySupplierId(GetGrnMasterObj.Supplierid, dbConnection);
            GetGrnMasterObj._POMaster = pOMasterDAO.GetPoMasterObjByPoId(GetGrnMasterObj.PoId, dbConnection);
            GetGrnMasterObj._Warehouse = WarehouseDAOInterface.getWarehouseByID(GetGrnMasterObj.WarehouseId, dbConnection);




            return GetGrnMasterObj;
        }

        public List<GrnMaster> GetGeneratedGRNsForPo(int PoId, DBConnection dbConnection)
        {

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT GRN.* , SUP.SUPPLIER_NAME,CLC.FIRST_NAME AS CREATED_USER_NAME,CLA.FIRST_NAME AS APPROVED_USER_NAME FROM GRN_MASTER AS GRN \n");
            sql.Append("LEFT JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS SUP \n");
            sql.Append("ON GRN.SUPPLIER_ID = SUP.SUPPLIER_ID \n");
            sql.Append("LEFT JOIN (SELECT USER_ID, FIRST_NAME FROM COMPANY_LOGIN) AS CLC \n");
            sql.Append("ON GRN.CREATED_BY = CLC.USER_ID \n");
            sql.Append("LEFT JOIN (SELECT USER_ID, FIRST_NAME FROM COMPANY_LOGIN) AS CLA \n");
            sql.Append("ON GRN.APPROVED_BY = CLA.USER_ID \n");
            sql.Append("WHERE GRN.GRN_ID IN (SELECT GRN_ID FROM PO_GRN WHERE PO_ID=" + PoId + ")");

            List<GrnMaster> grns = new List<GrnMaster>();

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                grns = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);

            }
            for (int i = 0; i < grns.Count; i++)
            {
                List<string> poCodes = DAOFactory.createPOMasterDAO().getPoDetailsByGrnId(grns[i].GrnId, dbConnection);

                grns[i].POCode = poCodes[0];
                grns[i].PrCode = poCodes[1];

            }


            return grns;
        }

        public List<GrnMaster> grnForApproval(int departmentid, int UserId, DBConnection dbConnection)
        {

            List<GrnMaster> grns = new List<GrnMaster>();

            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "[GET_GRNS_FOR_APPROVAL]";

            dbConnection.cmd.Parameters.AddWithValue("@USER_ID", UserId);
            dbConnection.cmd.Parameters.AddWithValue("@COMPANY_ID", departmentid);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                grns = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);

            }

            for (int i = 0; i < grns.Count; i++)
            {
                List<string> poCodes = DAOFactory.createPOMasterDAO().getPoDetailsByGrnId(grns[i].GrnId, dbConnection);

                grns[i].POCode = poCodes[0];
                grns[i].PrCode = poCodes[1];

                grns[i].QuotationFor = DAOFactory.CreatePR_MasterDAO().GetQuotationForbyPrCode(departmentid, grns[i].PrCode, dbConnection);
            }


            return grns;
        }

        public List<GrnMaster> GetGrnsByCompanyId(int CompanyId, DateTime date, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            List<GrnMaster> grns;
            String sql = "";
            sql = sql + "SELECT GRN.* , SUP.SUPPLIER_NAME,W.LOCATION AS WAREHOUSE_NAME FROM GRN_MASTER AS GRN " + "\n";
            sql = sql + "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS SUP " + "\n";
            sql = sql + "ON GRN.SUPPLIER_ID = SUP.SUPPLIER_ID " + "\n";
            sql = sql + "LEFT JOIN WAREHOUSE AS W ON GRN.WAREHOUSE_ID = W.WAREHOUSE_ID " + "\n";
            sql = sql + "WHERE GRN.TOTAL_AMOUNT > 0 AND DEPARTMENT_ID =" + CompanyId + " AND MONTH(GRN.CREATED_DATE) = " + date.Month + " AND YEAR(GRN.CREATED_DATE) = " + date.Year + " ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                grns = dataAccessObject.ReadCollection<GrnMaster>(dbConnection.dr);
            }

            for (int i = 0; i < grns.Count; i++)
            {
                List<string> poCodes = DAOFactory.createPOMasterDAO().getPoDetailsByGrnId(grns[i].GrnId, dbConnection);

                grns[i].POCode = poCodes[0];
                grns[i].PrCode = poCodes[1];

                grns[i].QuotationFor = DAOFactory.CreatePR_MasterDAO().GetQuotationForbyPrCode(CompanyId, grns[i].PrCode, dbConnection);

            }


            return grns;
        }

        public GrnMaster GetGrnMasterByGrnID(int grnId, DBConnection dbConnection)
        {
            GrnMaster GetGrnMasterObj = new GrnMaster();
            GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_MASTER  AS GRN " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS APPROVED_USER_NAME, DIGITAL_SIGNATURE AS APPROVED_SIGNATURE FROM COMPANY_LOGIN) AS CL ON GRN.APPROVED_BY = CL.USER_ID\n" +
                                             "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS CREATED_USER_NAME, DIGITAL_SIGNATURE AS CREATED_SIGNATURE FROM COMPANY_LOGIN) AS CLL ON GRN.CREATED_BY = CLL.USER_ID\n" +
                                            "WHERE GRN_ID = " + grnId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetGrnMasterObj = dataAccessObject.GetSingleOject<GrnMaster>(dbConnection.dr);

            }
            GetGrnMasterObj._GrnDetailsList = gRNDetailsDAO.GetGrnDetails(grnId, GetGrnMasterObj.CompanyId, GetGrnMasterObj.WarehouseId, dbConnection);

            return GetGrnMasterObj;
        }

        public int ValidateGrnBeforeApprove(int GrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "IF EXISTS(SELECT PO_ID FROM PO_MASTER WHERE PO_ID IN (SELECT PO_ID FROM PO_GRN WHERE GRN_ID = " + GrnId + ") AND IS_APPROVED != 1) " +
                                           "BEGIN SELECT 0 END " +
                                            "ELSE " +
                                            "BEGIN SELECT 1 END ";
            return Convert.ToInt32(dbConnection.cmd.ExecuteScalar());
        }

        public int ApproveGrn(int GrnId, int UserId, string Remarks, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DECLARE @GRN_ID INT=" + GrnId + " \n");
            sql.Append("DECLARE @GRND_ROWS INT \n");
            sql.Append("DECLARE @LOOPED_GRND_ROWS INT \n");
            sql.Append("DECLARE @GRND_ITEM_ID INT \n");
            sql.Append("DECLARE @GRND_RECEIVED_QTY  DECIMAL(18,2) \n");
            sql.Append(" \n");
            sql.Append("UPDATE GRN_MASTER SET IS_APPROVED= 1,APPROVED_BY=" + UserId + ", APPROVED_DATE='" + LocalTime.Now + "',APPROVAL_REMARKS='" + Remarks + "' WHERE GRN_ID=@GRN_ID \n");
            sql.Append(" \n");
            sql.Append("IF((SELECT COUNT(PO_ID) FROM PO_GRN WHERE GRN_ID =@GRN_ID) > 1) \n");
            sql.Append("BEGIN \n");
            sql.Append(" \n");
            sql.Append("	DECLARE @PARENT_PO_ID INT, @COVERING_PO_ID INT \n");
            sql.Append(" \n");
            sql.Append("	SELECT TOP 1 @PARENT_PO_ID = PO_ID FROM PO_GRN WHERE GRN_ID =@GRN_ID ORDER BY PO_ID ASC \n");
            sql.Append("	SELECT TOP 1 @COVERING_PO_ID = PO_ID FROM PO_GRN WHERE GRN_ID =@GRN_ID ORDER BY PO_ID DESC \n");
            sql.Append(" \n");
            sql.Append(" \n");
            sql.Append("	DECLARE @GRN_DETAILS TABLE(ITEM_ID INT, AVAILABLE_QTY DECIMAL(18,2)) \n");
            sql.Append("		 \n");
            sql.Append("	INSERT INTO @GRN_DETAILS \n");
            sql.Append("	SELECT ITEM_ID,QUANTITY FROM GRN_DETAILS WHERE GRN_ID=@GRN_ID \n");
            sql.Append("		 \n");
            sql.Append("	DECLARE @COVERING_PO_DETAILS TABLE(ITEM_ID INT, QTY DECIMAL(18,2)) \n");
            sql.Append("		 \n");
            sql.Append("	INSERT INTO @COVERING_PO_DETAILS \n");
            sql.Append("	SELECT ITEM_ID,QUANTITY FROM PO_DETAILS WHERE PO_ID=@COVERING_PO_ID \n");
            sql.Append(" \n");
            sql.Append("	DECLARE @CPO_ROWS INT, @LOOPED_CPO_ROWS INT = 0 \n");
            sql.Append(" \n");
            sql.Append("	SELECT @CPO_ROWS= COUNT(*) FROM @COVERING_PO_DETAILS \n");
            sql.Append(" \n");
            sql.Append("	-- UPDATING COVERING PO: START \n");
            sql.Append("	WHILE(@LOOPED_CPO_ROWS != @CPO_ROWS) \n");
            sql.Append("	BEGIN \n");
            sql.Append("		DECLARE @CPO_ITEM_ID INT \n");
            sql.Append("		DECLARE @CPO_QTY DECIMAL(18,2) \n");
            sql.Append(" \n");
            sql.Append("		SELECT @CPO_ITEM_ID=ITEM_ID, @CPO_QTY = QTY FROM @COVERING_PO_DETAILS ORDER BY ITEM_ID OFFSET @LOOPED_CPO_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
            sql.Append(" \n");
            sql.Append("		UPDATE @GRN_DETAILS SET AVAILABLE_QTY -= @CPO_QTY WHERE ITEM_ID = @CPO_ITEM_ID \n");
            sql.Append(" \n");
            sql.Append("		SET @LOOPED_CPO_ROWS += 1 \n");
            sql.Append("	END \n");
            sql.Append(" \n");
            sql.Append("	UPDATE PO_DETAILS SET RECEIVED_QTY=QUANTITY, WAITING_QTY=0,STATUS=2 WHERE PO_ID =@COVERING_PO_ID \n");
            sql.Append("	-- UPDATING COVERING PO: END \n");
            sql.Append(" \n");
            sql.Append("	-- UPDATING PARENT PO: START \n");
            sql.Append("	SET @LOOPED_GRND_ROWS=0 \n");
            sql.Append("	SELECT @GRND_ROWS = COUNT(*) FROM @GRN_DETAILS \n");
            sql.Append(" \n");
            sql.Append("	WHILE(@LOOPED_GRND_ROWS != @GRND_ROWS) \n");
            sql.Append("	BEGIN \n");
            sql.Append("			 \n");
            sql.Append("		SELECT @GRND_ITEM_ID=ITEM_ID, @GRND_RECEIVED_QTY=AVAILABLE_QTY FROM @GRN_DETAILS ORDER BY ITEM_ID OFFSET @LOOPED_GRND_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
            sql.Append(" \n");
            sql.Append("		UPDATE PO_DETAILS SET RECEIVED_QTY= ISNULL(RECEIVED_QTY,0)+ @GRND_RECEIVED_QTY, WAITING_QTY-= @GRND_RECEIVED_QTY, STATUS = CASE WHEN (RECEIVED_QTY+@GRND_RECEIVED_QTY)=QUANTITY THEN 2 ELSE 1 END WHERE ITEM_ID=@GRND_ITEM_ID AND PO_ID = @PARENT_PO_ID \n");    // update po details status. if partially received=1 if fully received=2
            sql.Append(" \n");
            sql.Append("		SET @LOOPED_GRND_ROWS+=1 \n");
            sql.Append(" \n");
            sql.Append("	END \n");
            sql.Append("	-- UPDATING PARENT PO: END \n");
            sql.Append("END \n");
            sql.Append("ELSE \n");
            sql.Append("BEGIN \n");
            sql.Append("	DECLARE @PO_ID INT \n");
            sql.Append("	SELECT @PO_ID=PO_ID FROM PO_GRN WHERE GRN_ID=@GRN_ID \n");
            sql.Append(" \n");
            sql.Append("	SELECT @GRND_ROWS = COUNT(ITEM_ID) FROM GRN_DETAILS WHERE GRN_ID=@GRN_ID \n");
            sql.Append(" \n");
            sql.Append("	SET @LOOPED_GRND_ROWS =0 \n");
            sql.Append(" \n");
            sql.Append("	WHILE(@LOOPED_GRND_ROWS != @GRND_ROWS) \n");
            sql.Append("	BEGIN \n");
            sql.Append(" \n");
            sql.Append("		SELECT @GRND_ITEM_ID=ITEM_ID, @GRND_RECEIVED_QTY = QUANTITY FROM GRN_DETAILS WHERE GRN_ID=@GRN_ID ORDER BY ITEM_ID OFFSET @LOOPED_GRND_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
            sql.Append(" \n");
            sql.Append("		UPDATE PO_DETAILS SET WAITING_QTY = WAITING_QTY-@GRND_RECEIVED_QTY, RECEIVED_QTY= ISNULL(RECEIVED_QTY,0)+@GRND_RECEIVED_QTY, STATUS = CASE WHEN (RECEIVED_QTY+@GRND_RECEIVED_QTY)=QUANTITY THEN 2 ELSE 1 END WHERE PO_ID=@PO_ID AND ITEM_ID=@GRND_ITEM_ID \n");      // update po details status. if partially received=1 if fully received=2
            sql.Append(" \n");
            sql.Append("		SET @LOOPED_GRND_ROWS+= 1 \n");
            sql.Append("	END \n");
            sql.Append("END");

            sql.Append(" \n");
            sql.Append("INSERT INTO PR_DETAIL_STATUS_LOG \n");
            sql.Append("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='GRNAPPRVD'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
            sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = (SELECT TOP 1 PO_ID FROM PO_GRN WHERE GRN_ID =" + GrnId + ")) AND \n");
            sql.Append("ITEM_ID IN (SELECT ITEM_ID FROM GRN_DETAILS WHERE GRN_ID =" + GrnId + ");  \n");
            sql.Append(" \n");


            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            int result = dbConnection.cmd.ExecuteNonQuery();

            // Updating Inventory
            if (result > 0)
            {
                sql = new StringBuilder();
                sql.Append("DECLARE @GRN_ID INT=" + GrnId + ",@USER_ID INT=" + UserId + ", @PR_ID INT,@PO_ID INT,@WAREHOUSE_ID INT,@COMPANY_ID INT");

                sql.Append("	 \n");
                sql.Append("	 \n");
                sql.Append("SELECT TOP 1 @PO_ID =PO_ID FROM PO_GRN WHERE GRN_ID=@GRN_ID");
                sql.Append("	 \n");
                sql.Append("SELECT @PR_ID =BASED_PR FROM PO_MASTER WHERE PO_ID = @PO_ID");
                sql.Append("	 \n");
                sql.Append("SELECT @WAREHOUSE_ID = WAREHOUSE_ID, @COMPANY_ID=DEPARTMENT_ID FROM GRN_MASTER WHERE GRN_ID =@GRN_ID");
                sql.Append("	 \n");
                sql.Append("	 \n");

                sql.Append("DECLARE @GRND_ROWS INT, @GRND_LOOPED_ROWS INT =0");
                sql.Append("	 \n");
                sql.Append("	 \n");

                sql.Append("SELECT @GRND_ROWS= COUNT (*) FROM GRN_DETAILS WHERE GRN_ID=@GRN_ID");
                sql.Append("	 \n");
                sql.Append("	 \n");

                sql.Append("WHILE(@GRND_ROWS != @GRND_LOOPED_ROWS) \n");
                sql.Append("BEGIN \n");
                sql.Append("	 \n");
                sql.Append("	DECLARE @ITEM_ID INT, @QUANTITY DECIMAL(18,2),@GRND_ID INT,@TOTAL_AMOUNT DECIMAL(18,2),@FREE_QTY DECIMAL(18,2),@EXPIRY_DATE DATE,@MEASUREMENT_ID INT,@STOCK_MAINTAINING_UOM INT, @QUANTITY2 DECIMAL(18,2),@TOTAL_AMOUNT2 DECIMAL(18,2),@FREE_QTY2 DECIMAL(18,2) \n");
                sql.Append(" \n");
                sql.Append("	SELECT @GRND_ID=GRND_ID, @ITEM_ID=ITEM_ID, @QUANTITY=QUANTITY,@TOTAL_AMOUNT=TOTAL_AMOUNT,@FREE_QTY=FREE_QTY,@EXPIRY_DATE=[EXPIRY_DATE],@MEASUREMENT_ID=MEASUREMENT_ID FROM GRN_DETAILS WHERE GRN_ID= @GRN_ID ORDER BY ITEM_ID OFFSET @GRND_LOOPED_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
                sql.Append(" \n");
                sql.Append("	-- GRND ISSUE NOTE \n");
                sql.Append("	INSERT INTO GRND_ISSUE_NOTE (GRND_ID,ITEM_ID,WAREHOUSE_ID,ISSUED_QTY,ISSUED_BY,ISSUED_ON,ISSUED_STOCK_VALUE,MEASUREMENT_ID) VALUES \n");
                sql.Append("	(@GRND_ID,@ITEM_ID,@WAREHOUSE_ID,@QUANTITY+@FREE_QTY,@USER_ID,'" + LocalTime.Now + "',@TOTAL_AMOUNT,@MEASUREMENT_ID) \n");
                sql.Append(" \n");
                sql.Append("    SELECT @STOCK_MAINTAINING_UOM= MEASUREMENT_ID FROM ADD_ITEMS WHERE ITEM_ID=@ITEM_ID AND COMPANY_ID=@COMPANY_ID \n");
                sql.Append("     \n");
                sql.Append("    IF(@MEASUREMENT_ID != @STOCK_MAINTAINING_UOM) \n");
                sql.Append("    BEGIN \n");
                sql.Append("    	DECLARE @CONVERTED_QTY_TBL TABLE(QTY DECIMAL(18,5)) \n");
                sql.Append("     \n");
                sql.Append("    	INSERT INTO @CONVERTED_QTY_TBL \n");
                sql.Append("    	EXEC [dbo].[DO_CONVERSION] \n");
                sql.Append("    		@COUNT = @QUANTITY, \n");
                sql.Append("    		@FROM_VAL_TYPE = @MEASUREMENT_ID, \n");
                sql.Append("    		@TO_VAL_TYPE = @STOCK_MAINTAINING_UOM, \n");
                sql.Append("    		@ITEM_ID = @ITEM_ID, \n");
                sql.Append("    		@COMPANY_ID = @COMPANY_ID \n");
                sql.Append("     \n");
                sql.Append("    		SELECT @QUANTITY=QTY FROM @CONVERTED_QTY_TBL \n");
                sql.Append("    END \n");
                sql.Append(" \n");
                sql.Append("     \n");
                sql.Append("    IF(@MEASUREMENT_ID != @STOCK_MAINTAINING_UOM) \n");
                sql.Append("    BEGIN \n");
                sql.Append("    	DECLARE @CONVERTED_QTY_TBL_1 TABLE(QTY DECIMAL(18,5)) \n");
                sql.Append("     \n");
                sql.Append("    	INSERT INTO @CONVERTED_QTY_TBL_1 \n");
                sql.Append("    	EXEC [dbo].[DO_CONVERSION] \n");
                sql.Append("    		@COUNT = @FREE_QTY, \n");
                sql.Append("    		@FROM_VAL_TYPE = @MEASUREMENT_ID, \n");
                sql.Append("    		@TO_VAL_TYPE = @STOCK_MAINTAINING_UOM, \n");
                sql.Append("    		@ITEM_ID = @ITEM_ID, \n");
                sql.Append("    		@COMPANY_ID = @COMPANY_ID \n");
                sql.Append("     \n");
                sql.Append("    		SELECT @FREE_QTY=QTY FROM @CONVERTED_QTY_TBL_1 \n");
                sql.Append("    END \n");
                sql.Append(" \n");
                sql.Append("	-- WAREHOUSE INVENTORY RAISE NOTE \n");
                sql.Append("	INSERT INTO WAREHOUSE_INVENTORY_RAISE (WAREHOUSE_ID,ITEM_ID,RAISED_QTY,RAISED_DATE,RAISED_BY,RAISED_TYPE,GRND_ID) \n");
                sql.Append("	VALUES(@WAREHOUSE_ID,@ITEM_ID,ISNULL(@QUANTITY,0)+ISNULL(@FREE_QTY,0),'" + LocalTime.Now + "',@USER_ID,1,@GRND_ID) \n");
                sql.Append("	 \n");
                sql.Append("	-- RAISING WAREHOUSE INVENTORY \n");
                sql.Append("	IF EXISTS (SELECT * FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID =@WAREHOUSE_ID AND ITEM_ID = @ITEM_ID) \n");
                sql.Append("	BEGIN");
                sql.Append("	    SELECT @QUANTITY2 = AVAILABLE_QTY, @TOTAL_AMOUNT2 = STOCK_VALUE FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID =@WAREHOUSE_ID AND ITEM_ID = @ITEM_ID \n");
                sql.Append("		UPDATE WAREHOUSE_INVENTORY_MASTER SET STOCK_VALUE= ISNULL(@TOTAL_AMOUNT2,0)+ISNULL(@TOTAL_AMOUNT,0), AVAILABLE_QTY= ISNULL(@QUANTITY2,0)+ISNULL(@QUANTITY,0)+ISNULL(@FREE_QTY,0),LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=@USER_ID WHERE WAREHOUSE_ID = @WAREHOUSE_ID AND ITEM_ID = @ITEM_ID \n");
                sql.Append("	END");
                sql.Append("	ELSE \n");
                sql.Append("		INSERT INTO WAREHOUSE_INVENTORY_MASTER (WAREHOUSE_ID,ITEM_ID,AVAILABLE_QTY,HOLDED_QTY,STOCK_VALUE,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) \n");
                sql.Append("        VALUES (@WAREHOUSE_ID,@ITEM_ID,ISNULL(@QUANTITY,0)+ISNULL(@FREE_QTY,0),0,@TOTAL_AMOUNT,'" + LocalTime.Now + "',@USER_ID,1); \n");
                sql.Append(" \n");
                sql.Append("	-- RAISING COMPANY INVENTORY \n");
                sql.Append("	IF EXISTS (SELECT * FROM COMPANY_INVENTORY_MASTER WHERE COMPANY_ID = @COMPANY_ID AND ITEM_ID = @ITEM_ID) \n");
                sql.Append("		UPDATE COMPANY_INVENTORY_MASTER SET STOCK_VALUE= STOCK_VALUE+@TOTAL_AMOUNT, AVAILABLE_QTY= AVAILABLE_QTY+@QUANTITY+@FREE_QTY,LAST_UPDATED_DATE='" + LocalTime.Now + "',LAST_UPDATED_BY=@USER_ID WHERE COMPANY_ID = @COMPANY_ID AND ITEM_ID = @ITEM_ID; \n");
                sql.Append("	ELSE \n");
                sql.Append("		INSERT INTO COMPANY_INVENTORY_MASTER (COMPANY_ID,ITEM_ID,AVAILABLE_QTY,STOCK_VALUE,REORDER_LEVEL,LAST_UPDATED_DATE,LAST_UPDATED_BY,IS_ACTIVE) \n");
                sql.Append("		VALUES (@COMPANY_ID,@ITEM_ID,@QUANTITY+@FREE_QTY,@TOTAL_AMOUNT,0,'" + LocalTime.Now + "',@USER_ID,1) \n");
                sql.Append(" \n");
                sql.Append("    IF((SELECT STOCK_MAINTAINING_TYPE FROM ADD_ITEMS WHERE ITEM_ID=@ITEM_ID AND COMPANY_ID=@COMPANY_ID) != 1) \n");
                sql.Append("    BEGIN \n");
                sql.Append("        INSERT INTO WAREHOUSE_INVENTORY_BATCHES VALUES( \n");
                sql.Append("        	(SELECT ISNULL(MAX([BATCH_CODE]),0)+1 FROM WAREHOUSE_INVENTORY_BATCHES WHERE ITEM_ID=@ITEM_ID AND COMPANY_ID=@COMPANY_ID), \n");
                sql.Append("        	@WAREHOUSE_ID,@ITEM_ID,	@COMPANY_ID,@EXPIRY_DATE,@QUANTITY+@FREE_QTY,0,@TOTAL_AMOUNT,GETDATE(),@USER_ID,1,0,@GRND_ID)");
                sql.Append("    END \n");
                sql.Append(" \n");
                sql.Append("	SET @GRND_LOOPED_ROWS+=1 \n");
                sql.Append("	 \n");
                sql.Append("END");

                sql.Append("	 \n");
                sql.Append("	 \n");
                sql.Append("IF EXISTS(SELECT PO_DETAILS_ID FROM PO_DETAILS WHERE PO_ID = @PO_ID AND ISNULL(RECEIVED_QTY,0) = QUANTITY) \n");
                sql.Append("	UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='CMPLTD') \n");
                sql.Append("    WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = @PO_ID) AND \n");
                sql.Append("    ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_ID = @PO_ID AND ISNULL(RECEIVED_QTY,0) = QUANTITY)");

                sql.Append("	 \n");
                sql.Append("	 \n");
                sql.Append("IF NOT EXISTS(SELECT * FROM PR_DETAIL WHERE PR_ID= @PR_ID AND CURRENT_STATUS NOT IN((SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='25'),(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED'))) \n");
                sql.Append("	UPDATE PR_MASTER SET CURRENT_STATUS=(SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='COMP') WHERE PR_ID=@PR_ID");


                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = sql.ToString();
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                result = dbConnection.cmd.ExecuteNonQuery();
            }

            return result;
        }

        public int RejectGrn(int GrnId, int UserId, string Remarks, DBConnection dbConnection)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("DECLARE @GRN_ID INT=" + GrnId + "  \n");
            sql.Append("DECLARE @GRND_ROWS INT \n");
            sql.Append("DECLARE @LOOPED_GRND_ROWS INT \n");
            sql.Append("DECLARE @GRND_ITEM_ID INT \n");
            sql.Append("DECLARE @GRND_RECEIVED_QTY  DECIMAL(18,2) \n");
            sql.Append(" \n");
            sql.Append("UPDATE GRN_MASTER SET IS_APPROVED= 2,APPROVED_BY=" + UserId + ", APPROVED_DATE='" + LocalTime.Now + "',APPROVAL_REMARKS='" + Remarks + "' WHERE GRN_ID=@GRN_ID \n");
            sql.Append(" \n");
            sql.Append("IF((SELECT COUNT(PO_ID) FROM PO_GRN WHERE GRN_ID =@GRN_ID) > 1) \n");
            sql.Append("BEGIN \n");
            sql.Append(" \n");
            sql.Append("	DECLARE @PARENT_PO_ID INT, @COVERING_PO_ID INT \n");
            sql.Append(" \n");
            sql.Append("	SELECT TOP 1 @PARENT_PO_ID = PO_ID FROM PO_GRN WHERE GRN_ID =@GRN_ID ORDER BY PO_ID ASC \n");
            sql.Append("	SELECT TOP 1 @COVERING_PO_ID = PO_ID FROM PO_GRN WHERE GRN_ID =@GRN_ID ORDER BY PO_ID DESC \n");
            sql.Append(" \n");
            sql.Append(" \n");
            sql.Append("	DECLARE @GRN_DETAILS TABLE(ITEM_ID INT, AVAILABLE_QTY DECIMAL(18,2)) \n");
            sql.Append("		 \n");
            sql.Append("	INSERT INTO @GRN_DETAILS \n");
            sql.Append("	SELECT ITEM_ID,QUANTITY FROM GRN_DETAILS WHERE GRN_ID=@GRN_ID \n");
            sql.Append("		 \n");
            sql.Append("	DECLARE @COVERING_PO_DETAILS TABLE(ITEM_ID INT, QTY DECIMAL(18,2)) \n");
            sql.Append("		 \n");
            sql.Append("	INSERT INTO @COVERING_PO_DETAILS \n");
            sql.Append("	SELECT ITEM_ID,QUANTITY FROM PO_DETAILS WHERE PO_ID=@COVERING_PO_ID \n");
            sql.Append(" \n");
            sql.Append("	DECLARE @CPO_ROWS INT, @LOOPED_CPO_ROWS INT = 0 \n");
            sql.Append(" \n");
            sql.Append("	SELECT @CPO_ROWS= COUNT(*) FROM @COVERING_PO_DETAILS \n");
            sql.Append(" \n");
            sql.Append("	-- UPDATING COVERING PO: START \n");
            sql.Append("	WHILE(@LOOPED_CPO_ROWS != @CPO_ROWS) \n");
            sql.Append("	BEGIN \n");
            sql.Append("		DECLARE @CPO_ITEM_ID INT \n");
            sql.Append("		DECLARE @CPO_QTY DECIMAL(18,2) \n");
            sql.Append(" \n");
            sql.Append("		SELECT @CPO_ITEM_ID=ITEM_ID, @CPO_QTY = QTY FROM @COVERING_PO_DETAILS ORDER BY ITEM_ID OFFSET @LOOPED_CPO_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
            sql.Append(" \n");
            sql.Append("		UPDATE @GRN_DETAILS SET AVAILABLE_QTY -= @CPO_QTY WHERE ITEM_ID = @CPO_ITEM_ID \n");
            sql.Append(" \n");
            sql.Append("		SET @LOOPED_CPO_ROWS += 1 \n");
            sql.Append("	END \n");
            sql.Append(" \n");
            sql.Append("	UPDATE PO_DETAILS SET WAITING_QTY=0 WHERE PO_ID =@COVERING_PO_ID \n");
            sql.Append("	UPDATE PO_MASTER SET IS_APPROVED =2, APPROVED_BY=" + UserId + ", APPROVED_DATE='" + LocalTime.Now + "', APPROVAL_REMARKS='GRN Rejected With The Remark: " + Remarks + "', IS_APPROVED_BY_PARENT_APPROVED_USER=2,PARENT_APPROVED_USER=" + UserId + ", PARENT_APPROVED_USER_APPROVAL_DATE='" + LocalTime.Now + "', PARENT_APPROVED_USER_APPROVAL_REMARKS='GRN Rejected With The Remark: " + Remarks + "' WHERE PO_ID= @COVERING_PO_ID \n");
            sql.Append("	-- UPDATING COVERING PO: END \n");
            sql.Append(" \n");
            sql.Append("	-- UPDATING PARENT PO: START \n");
            sql.Append("	SET @LOOPED_GRND_ROWS=0 \n");
            sql.Append("	SELECT @GRND_ROWS = COUNT(*) FROM @GRN_DETAILS \n");
            sql.Append(" \n");
            sql.Append("	WHILE(@LOOPED_GRND_ROWS != @GRND_ROWS) \n");
            sql.Append("	BEGIN \n");
            sql.Append("			 \n");
            sql.Append("		SELECT @GRND_ITEM_ID=ITEM_ID, @GRND_RECEIVED_QTY=AVAILABLE_QTY FROM @GRN_DETAILS ORDER BY ITEM_ID OFFSET @LOOPED_GRND_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
            sql.Append(" \n");
            sql.Append("		UPDATE PO_DETAILS SET WAITING_QTY-= @GRND_RECEIVED_QTY WHERE ITEM_ID=@GRND_ITEM_ID AND PO_ID = @PARENT_PO_ID \n");
            sql.Append(" \n");
            sql.Append("		SET @LOOPED_GRND_ROWS+=1 \n");
            sql.Append(" \n");
            sql.Append("	END \n");
            sql.Append("	-- UPDATING PARENT PO: END \n");
            sql.Append("END \n");
            sql.Append("ELSE \n");
            sql.Append("BEGIN \n");
            sql.Append("	DECLARE @PO_ID INT \n");
            sql.Append("	SELECT @PO_ID=PO_ID FROM PO_GRN WHERE GRN_ID=@GRN_ID \n");
            sql.Append(" \n");
            sql.Append("	SELECT @GRND_ROWS = COUNT(ITEM_ID) FROM GRN_DETAILS WHERE GRN_ID=@GRN_ID \n");
            sql.Append(" \n");
            sql.Append("	SET @LOOPED_GRND_ROWS =0 \n");
            sql.Append(" \n");
            sql.Append("	WHILE(@LOOPED_GRND_ROWS != @GRND_ROWS) \n");
            sql.Append("	BEGIN \n");
            sql.Append(" \n");
            sql.Append("		SELECT @GRND_ITEM_ID=ITEM_ID, @GRND_RECEIVED_QTY = QUANTITY FROM GRN_DETAILS WHERE GRN_ID=@GRN_ID ORDER BY ITEM_ID OFFSET @LOOPED_GRND_ROWS ROWS FETCH NEXT 1 ROWS ONLY \n");
            sql.Append(" \n");
            sql.Append("		UPDATE PO_DETAILS SET WAITING_QTY = WAITING_QTY-@GRND_RECEIVED_QTY WHERE PO_ID=@PO_ID AND ITEM_ID=@GRND_ITEM_ID \n");
            sql.Append(" \n");
            sql.Append("		SET @LOOPED_GRND_ROWS+= 1 \n");
            sql.Append("	END \n");
            sql.Append("END \n");

            sql.Append(" \n");
            sql.Append("INSERT INTO PR_DETAIL_STATUS_LOG \n");
            sql.Append("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='GRNRJCTD'),'" + LocalTime.Now + "'," + UserId + " FROM PR_DETAIL \n");
            sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = (SELECT TOP 1 PO_ID FROM PO_GRN WHERE GRN_ID =" + GrnId + ")) AND \n");
            sql.Append("ITEM_ID IN (SELECT ITEM_ID FROM GRN_DETAILS WHERE GRN_ID =" + GrnId + ");  \n");
            sql.Append(" \n");



            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();

        }
    }
}
