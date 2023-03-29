using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface PODetailsDAO
    {
        int SavePoDetails(int PoId,  int quotationId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount,decimal customizeAmount, decimal customizedVat, decimal customizedNbt,int isCustomizedAmount,decimal customizedTotalAmount, DBConnection dbConnection);
        List<PODetails> GetPoDetailsByPoId(int PoId, DBConnection dbConnection);
        PODetails GetPoDetailsObjByPoIdandItemId(int PoId, int itemid, DBConnection dbConnection);
        List<PODetails> GetSumOfAll(int PoId, DBConnection dbConnection);
        int UpdateIsRaisedPO(int PoId,int itemId,int isPORaised, DBConnection dbConnection);
        int UpdateIsRaisedPOAndPOApprovode(int PoId, int itemId, int IsPoApprvoed, DBConnection dbConnection);
        PODetails GetPoDetailsListByIsApprovedPO(int PoId,  DBConnection dbConnection);
        int UpdatePOApprovedNextLevel(int PoId,int newPOid, int itemId, int IsPoApprvoed,int BasedPO, DBConnection dbConnection);
        List<PODetails> GetAllListFromPoId(int PoId, DBConnection dbConnection);
        List<PODetails> GetPoDetailsByPoIdIsApproveTrue(int PoId, DBConnection dbConnection);
       
       //--GRN Modification
        List<PODetails> GetPODetailsApproved(int departmentId, DBConnection dbConnection);
        List<PODetails> GetPoDetailsByPoId(int PoId, int PrId, DBConnection dbConnection);
        List<PODetails> GetPOdetailsListBypoid(int poid, int CompanyId, DBConnection dbConnection);

        int RejectPoDetails(int poId, DBConnection dbConnection);

        int UpdatePOEditMode(int PoId, int ItemId, DBConnection dbConnection);

        List<PODetails> GetPoDetailsByPoIdPoRaised(int PoId, DBConnection dbConnection);
        int ApprovePoDetails(int poId, DBConnection dbConnection);

        POHistory GetPoHistoryByItemId(int ItemId, DBConnection dbConnection);

        //New Methods By Salman created on 2019-03-29
        List<PODetails> GetPoDetailsForPrInquiry(int Poid, int CompanyId, DBConnection dbConnection);
         List<PODetails> GetPUrchasedItems(int ItemId, int GACompanyId, DBConnection dbConnection);
        List<PODetails> GetPODetailsToViewPo(int PoId, int CompanyId, DBConnection dbConnection);
        List<PODetails> TerminatedPO(int PodId, DBConnection dbConnection);

        //Used in GRN NEW
        List<PODetails> GetPODetailsToGenerateGRN(int PoId, int CompanyId, DBConnection dbConnection);
        int TerminatePoDetail(int PoId, List<int> PoDetailIds, int TerminatedBy, string Remarks, DBConnection dbConnection);
        List<PODetails> GetPoDetails(int PoId, DBConnection dbConnection);
    }


    public class PODetailsDAOImpl : PODetailsDAO
    {
         string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<PODetails> GetPoDetailsByPoId(int PoId, DBConnection dbConnection)
        {
            List<PODetails> GetPoDetailsByPoId = new List<PODetails>();
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PO_DETAILS\"  WHERE  \"IS_PO_RAISED\"=0 AND \"IS_PO_APPROVED\"=0 AND  \"PO_ID\" =" + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoDetailsByPoId = dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
            foreach (var item in GetPoDetailsByPoId)
            {
                item._AddItem = addItemDAO.FetchItemListByIdObj(item.ItemId, dbConnection);
            }
            return GetPoDetailsByPoId;
        }

        public List<PODetails> GetPoDetailsByPoIdPoRaised(int PoId, DBConnection dbConnection)
        {
            List<PODetails> GetPoDetailsByPoId = new List<PODetails>();
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PO_DETAILS\"  WHERE  \"IS_PO_RAISED\"=1 AND \"IS_PO_APPROVED\"=0 AND  \"PO_ID\" =" + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoDetailsByPoId = dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
            foreach (var item in GetPoDetailsByPoId)
            {
                item._AddItem = addItemDAO.FetchItemListByIdObj(item.ItemId, dbConnection);
            }
            return GetPoDetailsByPoId;
        }

        public List<PODetails> GetPoDetailsByPoIdIsApproveTrue(int PoId, DBConnection dbConnection)
        {
            List<PODetails> GetPoDetailsByPoId = new List<PODetails>();
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PO_DETAILS\"  WHERE  \"IS_PO_RAISED\"=1 AND \"IS_PO_APPROVED\"=1 AND  \"PO_ID\" =" + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoDetailsByPoId = dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
            foreach (var item in GetPoDetailsByPoId)
            {
                item._AddItem = addItemDAO.FetchItemListByIdObj(item.ItemId, dbConnection);
            }
            return GetPoDetailsByPoId;
        }

        public PODetails GetPoDetailsObjByPoIdandItemId(int PoId, int itemid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"PO_DETAILS\"  WHERE \"PO_ID\" =" + PoId + " AND \"ITEM_ID\" =" + itemid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PODetails>(dbConnection.dr);
            }
        }

        public POHistory GetPoHistoryByItemId(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            string query = "SELECT a.PO_ID,a.CREATED_DATE , a.SUPPLIER_ID , b.ITEM_ID, b.ITEM_PRICE , d.SUPPLIER_NAME "+
                           " FROM " + dbLibrary + ".PO_MASTER a , " + dbLibrary + ".PO_DETAILS b, " + dbLibrary + ".SUPPLIER d "+
                           " WHERE a.PO_ID = b.PO_ID AND a.SUPPLIER_ID = d.SUPPLIER_ID AND b.ITEM_ID =380 AND " +  
                           " a.PO_ID = (SELECT MAX(PO_ID) FROM  " + dbLibrary + ".PO_DETAILS c " +
                           " WHERE  c.ITEM_ID = 380)";

            dbConnection.cmd.CommandText = query;
           

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<POHistory>(dbConnection.dr);
            }
        }

        public int SavePoDetails(int PoId, int quotationId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount, decimal customizeAmount, decimal customizedVat, decimal customizedNbt, int isCustomizedAmount, decimal customizedTotalAmount, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "INSERT INTO public.\"PO_DETAILS\" (\"PO_ID\", \"QUOTATION_ID\", \"ITEM_ID\", \"ITEM_PRICE\", \"QUANTITY\", \"VAT_AMOUNT\", \"NBT_AMOUNT\",  \"TOTAL_AMOUNT\",\"IS_PO_RAISED\",\"IS_PO_APPROVED\",\"CUSTOMIZED_AMOUNT\", \"CUSTOMIZED_VAT\", \"CUSTOMIZED_NBT\",\"IS_CUSTOMIZED_AMOUNT\",\"CUSTOMIZED_TOTAL_AMOUNT\",\"BASED_PO\",\"RECEIVED_QTY\") VALUES ( " + PoId + ", " + quotationId + " , " + itemId + ", " + itemPrce + ", " + quntity + ", " + vatAmount + ", " + nbtAmount + "," + totalAmount + ", " + 0 + ", " + 0 + ", " + customizeAmount + ", " + customizedVat + ", " + customizedNbt + "," + isCustomizedAmount + ", " + customizedTotalAmount + ", " + 0 + ", " + 0 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PODetails> GetSumOfAll(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT public.\"PO_DETAILS\".\"PO_ID\" , " +
                                           " SUM (public.\"PO_DETAILS\".\"VAT_AMOUNT\") AS VAT_AMOUNT, " +
                                           " SUM (public.\"PO_DETAILS\".\"NBT_AMOUNT\") AS NBT_AMOUNT, " +
                                           " SUM (public.\"PO_DETAILS\".\"TOTAL_AMOUNT\") AS TOTAL_AMOUNT, " +
                                           " SUM (public.\"PO_DETAILS\".\"CUSTOMIZED_VAT\") AS CUSVAT_AMOUNT, " +
                                           " SUM (public.\"PO_DETAILS\".\"CUSTOMIZED_NBT\") AS CUSNBT_AMOUNT, " +
                                           " SUM (public.\"PO_DETAILS\".\"CUSTOMIZED_TOTAL_AMOUNT\") AS CUSTOTAL_AMOUNT " +
                                           " FROM public.\"PO_MASTER\" " +
                                           " INNER JOIN public.\"PO_DETAILS\" ON public.\"PO_MASTER\".\"PO_ID\" = public.\"PO_DETAILS\".\"PO_ID\" " +
                                           " WHERE public.\"PO_DETAILS\".\"PO_ID\" = " + PoId + "  " +
                                           " GROUP BY public.\"PO_DETAILS\".\"PO_ID\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public int UpdateIsRaisedPO(int PoId, int itemId, int isPORaised, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE public.\"PO_DETAILS\" SET \"IS_PO_RAISED\" = " + isPORaised + "   WHERE \"PO_ID\" = " + PoId + " AND \"ITEM_ID\"= " + itemId + " ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateIsRaisedPOAndPOApprovode(int PoId, int itemId, int IsPoApprvoed, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE public.\"PO_DETAILS\" SET \"IS_PO_APPROVED\" = " + IsPoApprvoed + "   WHERE \"PO_ID\" = " + PoId + " AND \"ITEM_ID\"= " + itemId + " AND \"IS_PO_RAISED\"=1 ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public PODetails GetPoDetailsListByIsApprovedPO(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT \"PO_ID\", SUM(\"VAT_AMOUNT\") AS sumvatAmount, SUM(\"NBT_AMOUNT\") AS sumnbtmount, SUM(\"TOTAL_AMOUNT\") AS sumtotalAmount, SUM(\"CUSTOMIZED_TOTAL_AMOUNT\") AS CUSTOTAL_AMOUNT, SUM(\"CUSTOMIZED_VAT\") AS CUSVAT_AMOUNT, SUM(\"CUSTOMIZED_NBT\") AS CUSNBT_AMOUNT " +
                                           " FROM public.\"PO_DETAILS\"  " +
                                           " WHERE \"PO_ID\" =" + PoId + " AND \"IS_PO_APPROVED\" = 1" +
                                           " GROUP BY \"PO_ID\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PODetails>(dbConnection.dr);
            }
        }

        public int UpdatePOApprovedNextLevel(int PoId, int newPOid, int itemId, int IsPoApprvoed, int BasedPO, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE public.\"PO_DETAILS\" SET \"PO_ID\" = " + newPOid + " ,\"IS_PO_APPROVED\" = " + IsPoApprvoed + ",\"BASED_PO\"=" + BasedPO + "   WHERE \"PO_ID\" = " + PoId + " AND \"ITEM_ID\"= " + itemId + " ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<PODetails> GetAllListFromPoId(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM \"PO_DETAILS\" WHERE \"PO_ID\" = " + PoId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public List<PODetails> GetPODetailsApproved(int departmentId, DBConnection dbConnection)
        {
            List<PODetails> _podetails1 = new List<PODetails>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT * FROM public.\"PO_DETAILS\"  " +
                                           " INNER JOIN public.\"PO_MASTER\" ON public.\"PO_MASTER\".\"PO_ID\"  = public.\"PO_DETAILS\".\"PO_ID\" " +
                                           " INNER JOIN public.\"ADD_ITEMS\" ON public.\"ADD_ITEMS\".\"ITEM_ID\"  = public.\"PO_DETAILS\".\"ITEM_ID\" " +
                                           " INNER JOIN public.\"ITEM_CATEGORY\" ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\"  = public.\"ADD_ITEMS\".\"CATEGORY_ID\" " +
                                           " INNER JOIN public.\"ITEM_SUB_CATEGORY\" ON public.\"ADD_ITEMS\".\"SUB_CATEGORY_ID\"  = public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" " +
                                           " WHERE  public.\"PO_DETAILS\".\"IS_PO_RAISED\"=1 AND public.\"PO_DETAILS\".\"IS_PO_APPROVED\"=1 AND public.\"PO_MASTER\".\"DEPARTMENT_ID\"=" + departmentId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public int RejectPoDetails(int poId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"PO_DETAILS\" SET  \"IS_PO_APPROVED\" = 2 , \"IS_PO_RAISED\" = 2   WHERE \"PO_ID\" = " + poId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdatePOEditMode(int PoId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"PO_DETAILS\" SET  \"IS_PO_EDIT_MODE\" = 1   WHERE \"PO_ID\" = " + PoId + " AND \"ITEM_ID\"= " + ItemId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ApprovePoDetails(int poId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<PODetails> GetPoDetailsForPrInquiry(int Poid, int CompanyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<PODetails> GetPUrchasedItems(int ItemId, int GACompanyId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<PODetails> GetPODetailsToViewPo(int PoId, int CompanyId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<PODetails> TerminatedPO(int PodId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<PODetails> GetPoDetailsByPoId(int PoId, int PrId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<PODetails> GetPOdetailsListBypoid(int poid, int CompanyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<PODetails> GetPODetailsToGenerateGRN(int PoId, int CompanyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int TerminatePoDetail(int PoId, List<int> PoDetailIds, int TerminatedBy, string Remarks, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<PODetails> GetPoDetails(int PoId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }
    }

    public class PODetailsDAOSQLImpl : PODetailsDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<PODetails> GetPoDetailsByPoId(int PoId, DBConnection dbConnection)
        {
            List<PODetails> GetPoDetailsByPoId = new List<PODetails>();
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PO_DETAILS  WHERE  IS_PO_RAISED=0 AND IS_PO_APPROVED=0 AND  PO_ID =" + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoDetailsByPoId = dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
            foreach (var item in GetPoDetailsByPoId)
            {
                item._AddItem = addItemDAO.FetchItemListByIdObj(item.ItemId, dbConnection);
            }
            return GetPoDetailsByPoId;
        }

        public List<PODetails> GetPoDetailsByPoIdPoRaised(int PoId, DBConnection dbConnection)
        {
            List<PODetails> GetPoDetailsByPoId = new List<PODetails>();
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PO_DETAILS  WHERE  IS_PO_RAISED=1 AND IS_PO_APPROVED=0 AND  PO_ID =" + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoDetailsByPoId = dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
            foreach (var item in GetPoDetailsByPoId)
            {
                item._AddItem = addItemDAO.FetchItemListByIdObj(item.ItemId, dbConnection);
            }
            return GetPoDetailsByPoId;
        }

        

        public List<PODetails> GetPoDetailsByPoIdIsApproveTrue(int PoId, DBConnection dbConnection)
        {
            List<PODetails> GetPoDetailsByPoId = new List<PODetails>();
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PO_DETAILS  WHERE  IS_PO_RAISED=1 AND IS_PO_APPROVED=1 AND  PO_ID =" + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoDetailsByPoId = dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
            foreach (var item in GetPoDetailsByPoId)
            {
                item._AddItem = addItemDAO.FetchItemListByIdObj(item.ItemId, dbConnection);
            }
            return GetPoDetailsByPoId;
        }

        public POHistory GetPoHistoryByItemId(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            string query = "SELECT a.PO_ID,a.CREATED_DATE , a.SUPPLIER_ID , b.ITEM_ID, b.ITEM_PRICE , d.SUPPLIER_NAME " +
                           " FROM " + dbLibrary + ".PO_MASTER a , " + dbLibrary + ".PO_DETAILS b, " + dbLibrary + ".SUPPLIER d " +
                           " WHERE a.PO_ID = b.PO_ID AND a.SUPPLIER_ID = d.SUPPLIER_ID AND b.ITEM_ID = " + ItemId + " AND " +
                           " a.PO_ID = (SELECT MAX(PO_ID) FROM  " + dbLibrary + ".PO_DETAILS c " +
                           " WHERE  c.ITEM_ID = " + ItemId + ")";

            dbConnection.cmd.CommandText = query;


            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<POHistory>(dbConnection.dr);
            }
        }

        public PODetails GetPoDetailsObjByPoIdandItemId(int PoId, int itemid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PO_DETAILS  WHERE PO_ID =" + PoId + " AND ITEM_ID =" + itemid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PODetails>(dbConnection.dr);
            }
        }

        public int SavePoDetails(int PoId, int quotationId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount, decimal customizeAmount, decimal customizedVat, decimal customizedNbt, int isCustomizedAmount, decimal customizedTotalAmount, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PO_DETAILS (PO_ID, QUOTATION_ID, ITEM_ID, ITEM_PRICE, QUANTITY, VAT_AMOUNT, NBT_AMOUNT,  TOTAL_AMOUNT,IS_PO_RAISED,IS_PO_APPROVED,CUSTOMIZED_AMOUNT, CUSTOMIZED_VAT, CUSTOMIZED_NBT,IS_CUSTOMIZED_AMOUNT,CUSTOMIZED_TOTAL_AMOUNT,BASED_PO,RECEIVED_QTY) VALUES ( " + PoId + ", " + quotationId + " , " + itemId + ", " + itemPrce + ", " + quntity + ", " + vatAmount + ", " + nbtAmount + "," + totalAmount + ", " + 0 + ", " + 0 + ", " + customizeAmount + ", " + customizedVat + ", " + customizedNbt + "," + isCustomizedAmount + ", " + customizedTotalAmount + ", " + 0 + ", " + 0 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PODetails> GetSumOfAll(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT POD.PO_ID , " +
                                           " SUM (POD.VAT_AMOUNT) AS VAT_AMOUNT, " +
                                           " SUM (POD.NBT_AMOUNT) AS NBT_AMOUNT, " +
                                           " SUM (POD.TOTAL_AMOUNT) AS TOTAL_AMOUNT, " +
                                           " SUM (POD.CUSTOMIZED_VAT) AS CUSVAT_AMOUNT, " +
                                           " SUM (POD.CUSTOMIZED_NBT) AS CUSNBT_AMOUNT, " +
                                           " SUM (POD.CUSTOMIZED_TOTAL_AMOUNT) AS CUSTOTAL_AMOUNT " +
                                           " FROM " + dbLibrary + ".PO_MASTER  AS POM " +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POM.PO_ID = POD.PO_ID " +
                                           " WHERE POD.PO_ID = " + PoId + "  " +
                                           " GROUP BY POD.PO_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public int UpdateIsRaisedPO(int PoId, int itemId, int isPORaised, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".PO_DETAILS SET IS_PO_RAISED = " + isPORaised + "   WHERE PO_ID = " + PoId + " AND ITEM_ID= " + itemId + " ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateIsRaisedPOAndPOApprovode(int PoId, int itemId, int IsPoApprvoed, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".PO_DETAILS SET IS_PO_APPROVED = " + IsPoApprvoed + "   WHERE PO_ID = " + PoId + " AND ITEM_ID= " + itemId + " AND IS_PO_RAISED=1 ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public PODetails GetPoDetailsListByIsApprovedPO(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT PO_ID, SUM(VAT_AMOUNT) AS sumvatAmount, SUM(NBT_AMOUNT) AS sumnbtmount, SUM(TOTAL_AMOUNT) AS sumtotalAmount, SUM(CUSTOMIZED_TOTAL_AMOUNT) AS CUSTOTAL_AMOUNT, SUM(CUSTOMIZED_VAT) AS CUSVAT_AMOUNT, SUM(CUSTOMIZED_NBT) AS CUSNBT_AMOUNT " +
                                           " FROM " + dbLibrary + ".PO_DETAILS  " +
                                           " WHERE PO_ID =" + PoId + " AND IS_PO_APPROVED = 1" +
                                           " GROUP BY PO_ID";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PODetails>(dbConnection.dr);
            }
        }

        public int UpdatePOApprovedNextLevel(int PoId, int newPOid, int itemId, int IsPoApprvoed, int BasedPO, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_DETAILS SET PO_ID = " + newPOid + " ,IS_PO_APPROVED = " + IsPoApprvoed + ",BASED_PO=" + BasedPO + "   WHERE PO_ID = " + PoId + " AND ITEM_ID= " + itemId + " ";
            return dBConnection.cmd.ExecuteNonQuery();
        }

        public List<PODetails> GetAllListFromPoId(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PO_DETAILS WHERE PO_ID = " + PoId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public List<PODetails> GetPODetailsApproved(int departmentId, DBConnection dbConnection)
        {
            List<PODetails> _podetails1 = new List<PODetails>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PO_DETAILS AS POD " +
                                           " INNER JOIN " + dbLibrary + ".PO_MASTER AS POM ON POM.PO_ID  = POD.PO_ID " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID  = POD.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID  = AI.CATEGORY_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON AI.SUB_CATEGORY_ID  = ISC.SUB_CATEGORY_ID " +
                                           " WHERE  POD.IS_PO_RAISED=1 AND POD.IS_PO_APPROVED=1 AND AI.COMPANY_ID = "+ departmentId + " AND IC.COMPANY_ID = "+departmentId+" AND ISC.COMPANY_ID = "+departmentId+" AND POM.DEPARTMENT_ID=" + departmentId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public List<PODetails> GetPoDetails(int PoId, DBConnection dbConnection) {
            List<PODetails> _podetails1 = new List<PODetails>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PO_DETAILS WHERE PO_ID = "+ PoId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public int RejectPoDetails(int poId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE "+ dbLibrary +".PO_DETAILS SET  IS_PO_APPROVED = 2 , IS_PO_RAISED = 2   WHERE PO_ID = " + poId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int ApprovePoDetails(int poId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_DETAILS SET  IS_PO_APPROVED = 1 WHERE PO_ID = " + poId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdatePOEditMode(int PoId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".PO_DETAILS SET  IS_PO_EDIT_MODE = 1   WHERE PO_ID = " + PoId + " AND ITEM_ID= " + ItemId + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PODetails> GetPoDetailsForPrInquiry(int PoId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM PO_DETAILS AS POD\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON POD.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "WHERE POD.PO_ID = " + PoId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public List<PODetails> GetPUrchasedItems(int ItemId, int GACompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT TOP 10 POD.ITEM_PRICE,CD.DEPARTMENT_NAME,POM.PO_CODE,POM.PO_ID,POM.CREATED_DATE, POD.ITEM_ID, SUP.SUPPLIER_NAME, POD.QUANTITY FROM PO_DETAILS AS POD  " + "\n" +
                                             "  INNER JOIN PO_MASTER AS POM ON POD.PO_ID = POM.PO_ID" + "\n" +
                                             "  INNER JOIN COMPANY_DEPARTMENT AS CD ON POM.DEPARTMENT_ID=CD.DEPARTMENT_ID " + "\n" +
                                             "  INNER JOIN ADD_ITEMS_MASTER AS AIM ON POD.ITEM_ID = AIM.ITEM_ID " + "\n" +
                                             " INNER JOIN SUPPLIER AS SUP ON SUP.SUPPLIER_ID = POM.SUPPLIER_ID " + "\n" +
                                             "  WHERE POM.IS_APPROVED=1 AND CD.DEPARTMENT_ID= " + GACompanyId + " AND POD.ITEM_ID= " + ItemId + " " + "\n" +
                                             "  ORDER BY POD.ITEM_PRICE";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }
        public List<PODetails> GetPODetailsToViewPo(int PoId, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.CommandText = "SELECT POD.*,AI.ITEM_ID,SUPA.SUPPLIER_AGENT_NAME,AI.ITEM_NAME,POM.BASED_PR,(POD.QUANTITY-(POD.WAITING_QTY + POD.RECEIVED_QTY)) AS PENDING_QTY,(POD.ITEM_PRICE * POD.QUANTITY) AS SUB_TOTAL,M.MEASUREMENT_NAME, DPT.TERM_NAME, PRD.SPARE_PART_NUMBER,TABD.UNIT_PRICE_LOCAL, TABD.UNIT_PRICE_FOREIGN FROM PO_DETAILS AS POD \n" +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME,MEASUREMENT_ID, COMPANY_ID FROM ADD_ITEMS WHERE COMPANY_ID= " + CompanyId + ") AS AI ON POD.ITEM_ID = AI.ITEM_ID \n" +
                                            "LEFT JOIN(SELECT PO_ID, BASED_PR FROM PO_MASTER) AS POM ON POM.PO_ID = POD.PO_ID \n" +
                                            "LEFT JOIN (SELECT DETAIL_ID,SHORT_CODE AS MEASUREMENT_NAME FROM MEASUREMENT_DETAIL) AS M ON M.DETAIL_ID = POD.MEASUREMENT_ID \n" +
                                            "LEFT JOIN (SELECT QUOTATION_ITEM_ID, QUOTATION_ID, TERM FROM IMPORT_QUOTATION_ITEM ) AS IQI ON IQI.QUOTATION_ITEM_ID = POD.QUOTATION_ITEM_ID \n" +
                                            "LEFT JOIN (SELECT QUOTATION_ID, SUPPLIER_AGENT FROM IMPORT_QUOTATION ) AS SA ON SA.QUOTATION_ID = IQI.QUOTATION_ID \n" +
                                            "LEFT JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME AS SUPPLIER_AGENT_NAME FROM SUPPLIER ) AS SUPA ON SA.SUPPLIER_AGENT = SUPA.SUPPLIER_ID \n" +
                                            "LEFT JOIN(SELECT PR_ID, IMPORT_ITEM_TYPE FROM PR_MASTER) AS PRM ON PRM.PR_ID = POM.BASED_PR \n" +
                                            "LEFT JOIN (SELECT TERM_ID, TERM_NAME FROM DEF_PRICE_TERMS ) AS DPT ON IQI.TERM = DPT.TERM_ID \n" +
                                            "LEFT JOIN (SELECT QUOTATION_ITEM_ID, BIDDING_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM ) AS SQI ON SQI.QUOTATION_ITEM_ID = POD.QUOTATION_ITEM_ID \n" +
                                            "LEFT JOIN (SELECT BIDDING_ITEM_ID, PRD_ID, BID_ID FROM BIDDING_ITEM ) AS BI ON BI.BIDDING_ITEM_ID = SQI.BIDDING_ITEM_ID \n" +
                                            "LEFT JOIN (SELECT PRD_ID,SPARE_PART_NUMBER FROM PR_DETAIL ) AS PRD ON BI.PRD_ID = PRD.PRD_ID \n" +
                                            "INNER JOIN(SELECT TABULATION_ID, BID_ID, PR_ID, IS_SELECTED FROM TABULATION_MASTER WHERE IS_SELECTED = 1) AS TABM ON PRM.PR_ID = TABM.PR_ID AND BI.BID_ID = TABM.BID_ID \n "+
                                            "LEFT JOIN TABULATION_DETAIL AS TABD ON TABD.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID AND  TABD.QUOTATION_ID = SA.QUOTATION_ID AND TABD.ITEM_ID = AI.ITEM_ID AND TABD.TABULATION_ID = TABM.TABULATION_ID \n" +
                                            "WHERE POD.PO_ID = " + PoId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public List<PODetails> TerminatedPO(int PodId, DBConnection dbConnection) {
            dbConnection.cmd.CommandText = "SELECT * FROM PO_DETAILS AS POD " +
                                            "LEFT JOIN (SELECT USER_ID, FIRST_NAME AS TERMINATED_BY_NAME FROM COMPANY_LOGIN) AS CL AN CL.USER_ID = POD.TERMINATED_BY "+
                                            "WHERE PO_DETAILS_ID = " + PodId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public List<PODetails> GetPOdetailsListBypoid(int poid, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = " SELECT POD.QUANTITY,POM.PO_CODE,POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, SU.SUPPLIER_NAME " +
            //                               " FROM " + dbLibrary + ".PO_MASTER AS POM " +
            //                               " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
            //                               " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
            //                               " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
            //                               " WHERE POM.DEPARTMENT_ID =" + departmentid + "  " +
            //                               " AND POD.IS_PO_RAISED =1 AND POD.IS_PO_APPROVED =1 " +
            //                               " GROUP BY POM.PO_ID,POM.PO_CODE,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,POD.QUANTITY ";



            dbConnection.cmd.CommandText = "SELECT POD.*, POD.PO_ID,POD.ITEM_ID, POD.VAT_AMOUNT,POD.QUANTITY,POD.RECEIVED_QTY, POD.NBT_AMOUNT, POD.TOTAL_AMOUNT, POD.ITEM_PRICE, AD.ITEM_NAME, SUB.SUB_CATEGORY_NAME,CAT.CATEGORY_NAME,UM.SHORT_CODE AS MEASUREMENT_SHORT_NAME, (POD.ITEM_PRICE * POD.QUANTITY) AS SUB_TOTAL FROM PO_DETAILS AS POD " + "\n" +
                                             "  INNER JOIN (SELECT ITEM_ID, ITEM_NAME,SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AD " + "\n" +
                                             "  ON POD.ITEM_ID = AD.ITEM_ID " + "\n" +
                                             "  INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS SUB " + "\n" +
                                             "  ON SUB.SUB_CATEGORY_ID = AD.SUB_CATEGORY_ID " + "\n" +
                                             "  INNER JOIN (SELECT CATEGORY_NAME, CATEGORY_ID FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS CAT " + "\n" +
                                             "  ON CAT.CATEGORY_ID = SUB.CATEGORY_ID " + "\n" +
                                             "  LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS UM ON UM.DETAIL_ID= POD.MEASUREMENT_ID \n" +
                                             "  WHERE POD.PO_ID = " + poid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public List<PODetails> GetPoDetailsByPoId(int PoId, int PrId, DBConnection dbConnection)
        {
            List<PODetails> GetPoDetailsByPoId = new List<PODetails>();
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            // dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PO_DETAILS  WHERE  PO_ID =" + PoId;
            dbConnection.cmd.CommandText = "SELECT POD.*,UM.MEASUREMENT_SHORT_NAME FROM PO_DETAILS AS POD " +
            "INNER JOIN (SELECT PR_ID,ITEM_ID,UNIT FROM PR_DETAIL WHERE PR_ID= " + PrId + ") AS PRD ON POD.ITEM_ID= PRD.ITEM_ID " +
            "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UM ON UM.DETAIL_ID= POD.MEASUREMENT_ID \n" +
            "WHERE  POD.PO_ID =" + PoId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                GetPoDetailsByPoId = dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
            foreach (var item in GetPoDetailsByPoId)
            {
                item._AddItem = addItemDAO.FetchItemListByIdObj(item.ItemId, dbConnection);
            }
            return GetPoDetailsByPoId;
        }

        public List<PODetails> GetPODetailsToGenerateGRN(int PoId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT POD.*, POD.PO_DETAILS_ID,AI.ITEM_ID,AI.ITEM_NAME,QUANTITY,RECEIVED_QTY,(QUANTITY-(WAITING_QTY + ISNULL(RECEIVED_QTY,0))) AS PENDING_QTY, ITEM_PRICE,(ITEM_PRICE * (QUANTITY-(WAITING_QTY + ISNULL(RECEIVED_QTY,0)))) AS SUB_TOTAL,POD.HAS_NBT,POD.NBT_CALCULATION_TYPE,POD.HAS_VAT,UM.MEASUREMENT_SHORT_NAME,AI.STOCK_MAINTAINING_TYPE,UM.DETAIL_ID AS MEASUREMENT_ID FROM PO_DETAILS AS POD \n" +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME,MEASUREMENT_ID, COMPANY_ID, STOCK_MAINTAINING_TYPE FROM ADD_ITEMS WHERE COMPANY_ID= " + CompanyId + ") AS AI ON POD.ITEM_ID = AI.ITEM_ID \n" +
                                            "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UM ON UM.DETAIL_ID= POD.MEASUREMENT_ID \n" +
                                            "WHERE PO_ID = " + PoId + " AND (QUANTITY-(WAITING_QTY + ISNULL(RECEIVED_QTY,0))) >0 AND STATUS !=3";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PODetails>(dbConnection.dr);
            }
        }

        public int TerminatePoDetail(int PoId, List<int> PoDetailIds, int TerminatedBy, string Remarks, DBConnection dbConnection)
        {

            StringBuilder sql = new StringBuilder();

            sql.Append("UPDATE PO_DETAILS SET STATUS=3,TERMINATION_REMARKS='" + Remarks.ProcessString() + "',TERMINATED_BY=" + TerminatedBy + ",TERMINATED_ON='" + LocalTime.Now + "' WHERE PO_DETAILS_ID IN (" + string.Join(", ", PoDetailIds) + ") \n");
            sql.Append(" \n");
            sql.Append("UPDATE PR_DETAIL SET CURRENT_STATUS=(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED') \n");
            sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
            sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_DETAILS_ID IN (" + string.Join(", ", PoDetailIds) + "));  \n");
            sql.Append(" \n");
            sql.Append("INSERT INTO PR_DETAIL_STATUS_LOG \n");
            sql.Append("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='PROCTERM'),'" + LocalTime.Now + "'," + TerminatedBy + " FROM PR_DETAIL \n");
            sql.Append("WHERE PR_ID = (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND \n");
            sql.Append("ITEM_ID IN(SELECT ITEM_ID FROM PO_DETAILS WHERE PO_DETAILS_ID IN (" + string.Join(", ", PoDetailIds) + "));  \n");
            sql.Append(" \n");
            sql.Append("IF EXISTS(SELECT * FROM PR_DETAIL WHERE PR_ID= (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND CURRENT_STATUS != (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED')) \n");
            sql.Append("BEGIN \n");
            sql.Append("    IF NOT EXISTS(SELECT * FROM PR_DETAIL WHERE PR_ID= (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ") AND CURRENT_STATUS NOT IN((SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='CMPLTD'),(SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='PROC_ENDED'))) \n");
            sql.Append("	    UPDATE PR_MASTER SET CURRENT_STATUS=(SELECT PR_STATUS_ID FROM DEF_PR_STATUS WHERE STATUS_CODE='COMP') WHERE PR_ID= (SELECT BASED_PR FROM PO_MASTER WHERE PO_ID = " + PoId + ")");
            sql.Append("END \n");
            dbConnection.cmd.CommandText = sql.ToString();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
