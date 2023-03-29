using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
   public interface GRNDetailsDAO
    {
        int SaveGrnDetails( int GrnId, int PoId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount,DBConnection dbConnection);
        List<GrnDetails> GetGrnDetailsByPoId(int GrnId, int PoId, DBConnection dbConnection);
        List<GrnDetails> GetGrnDetails(int GrnId, int PoId, int WarehouseId, DBConnection dbConnection);
        GrnDetails GetGrnDetailsObjByPoIdandItemId(int GrnId,int PoId, int itemid, DBConnection dbConnection);
        List<GrnDetails> GetGrnDetails(int PoId, DBConnection dbConnection);
        List<GrnDetails> GetGrnDetailsAll(int DepartmentId, DBConnection dbConnection);
        int updateGrndIssuedQty(int grndID,int issuedQty, DBConnection dbConnection);
        List<GrnDetails> GrnDetialsGrnApproved(int GrnId, int PoId, int DepartmentId, DBConnection dbConnection);
        int UpdateApprovedGrn(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason, DBConnection dbConnection);
        List<GrnDetails> GrnDetialsGrnApprovedOnly(int GrnId, int PoId, int DepartmentId, DBConnection dbConnection);
        int UpdateApprovedGrnNewGrnId(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason, int NewGrnId, DBConnection dbConnection);
        int UpdateGrnReectIsPoRaised(int GrnId, int PoId, int ItemId, int isGrnRaised, DBConnection dbConnection);

        int UpdateGrnReectAddToGrnCount(int GrnId, int PoId, int ItemId, int count, DBConnection dbConnection);

        //New Methods By Salman created on 2019-03-29
        List<GrnDetails> GetGrnDetailsForPrInquiry(int GrnId, int CompanyId, DBConnection dbConnection);
    }
    public class GRNDetailsDAOImpl : GRNDetailsDAO
    {
        public List<GrnDetails> GetGrnDetailsByPoId(int GrnId, int PoId, int WarehouseId, DBConnection dbConnection)
        {
            List<GrnDetails> GetGrnDetailsByPoId = new List<GrnDetails>();
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_DETAILS\"  WHERE \"PO_ID\" =" + PoId + " AND \"GRN_ID\" = " + GrnId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }

            return GetGrnDetailsByPoId;
        }

        public GrnDetails GetGrnDetailsObjByPoIdandItemId(int GrnId, int PoId, int itemid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_DETAILS\"  WHERE PO_ID =" + PoId + " AND ITEM_ID =" + itemid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<GrnDetails>(dbConnection.dr);
            }
        }

        public int SaveGrnDetails(int GrnId, int PoId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "INSERT INTO public.\"GRN_DETAILS\" (\"GRN_ID\", \"PO_ID\", \"ITEM_ID\", \"ITEM_PRICE\", \"QUANTITY\", \"VAT_AMOUNT\", \"NBT_AMOUNT\",  \"TOTAL_AMOUNT\", \"IS_GRN_RAISED\", \"GRN_APPROVED_DATE_TIME\",\"IS_GRN_APPROVED\",\"ADD_TO_GRN_COUNT\" ) VALUES ( " + GrnId + ", " + PoId + " , " + itemId + "," + itemPrce + ", " + quntity + ", " + vatAmount + ", " + nbtAmount + ", " + totalAmount + ", " + 1 + ", '" + LocalTime.Now + "', " + 0 + "," + 0 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnDetails> GetGrnDetails(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_DETAILS\"  WHERE \"PO_ID\" =" + PoId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }

        public List<GrnDetails> GetGrnDetailsAll(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GRN_DETAILS\" INNER JOIN public.\"GRN_MASTER\" ON  public.\"GRN_MASTER\".\"GRN_ID\" =  public.\"GRN_DETAILS\".\"GRN_ID\" WHERE public.\"GRN_MASTER\".\"DEPARTMENT_ID\" = " + DepartmentId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }

        public List<GrnDetails> GrnDetialsGrnApproved(int GrnId, int PoId, int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " select * from public.\"GRN_DETAILS\" " +
                                           " inner join public.\"GRN_MASTER\" on public.\"GRN_DETAILS\".\"GRN_ID\" = public.\"GRN_MASTER\".\"GRN_ID\" " +
                                           " inner join public.\"ADD_ITEMS\"  on public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"GRN_DETAILS\".\"ITEM_ID\" " +
                                           " where public.\"GRN_DETAILS\".\"IS_GRN_RAISED\" = 1 and public.\"GRN_DETAILS\".\"IS_GRN_APPROVED\" = 0 and public.\"GRN_MASTER\".\"DEPARTMENT_ID\" = " + DepartmentId + " and public.\"GRN_DETAILS\".\"GRN_ID\" = " + GrnId + " and  public.\"GRN_DETAILS\".\"PO_ID\" = " + PoId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }

        public int UpdateApprovedGrn(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE \"GRN_DETAILS\"  SET \"IS_GRN_APPROVED\" =" + IsApproveStatus + ", \"GRN_APPROVED_DATE_TIME\"='" + ApprovedDatetime + "',\"GRN_APPROVED_BY\" =" + ApprovedBy + ", \"REASON_FOR_REJECT\" ='" + RejectedReason + "'  WHERE \"GRN_ID\"=" + GrnId + " AND \"PO_ID\" = " + PoId + " AND \"ITEM_ID\" = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        //--Grn Reject
        public int UpdateGrnReectIsPoRaised(int GrnId, int PoId, int ItemId, int isGrnRaised, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE \"GRN_DETAILS\"  SET \"IS_GRN_RAISED\" =" + isGrnRaised + "  WHERE \"GRN_ID\"=" + GrnId + " AND \"PO_ID\" = " + PoId + " AND \"ITEM_ID\" = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnDetails> GrnDetialsGrnApprovedOnly(int GrnId, int PoId, int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " select * from public.\"GRN_DETAILS\" " +
                                           " inner join public.\"GRN_MASTER\" on public.\"GRN_DETAILS\".\"GRN_ID\" = public.\"GRN_MASTER\".\"GRN_ID\" " +
                                           " inner join public.\"ADD_ITEMS\"  on public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"GRN_DETAILS\".\"ITEM_ID\" " +
                                           " where public.\"GRN_DETAILS\".\"IS_GRN_RAISED\" = 1 and public.\"GRN_DETAILS\".\"IS_GRN_APPROVED\" = 1 and public.\"GRN_MASTER\".\"DEPARTMENT_ID\" = " + DepartmentId + " and public.\"GRN_DETAILS\".\"GRN_ID\" = " + GrnId + " and  public.\"GRN_DETAILS\".\"PO_ID\" = " + PoId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }

        public int UpdateApprovedGrnNewGrnId(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason, int NewGrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE \"GRN_DETAILS\"  SET \"IS_GRN_APPROVED\" =" + IsApproveStatus + ", \"GRN_APPROVED_DATE_TIME\"='" + ApprovedDatetime + "',\"GRN_APPROVED_BY\" =" + ApprovedBy + ", \"REASON_FOR_REJECT\" ='" + RejectedReason + "',\"GRN_ID\"=" + NewGrnId + "  WHERE \"GRN_ID\"=" + GrnId + " AND \"PO_ID\" = " + PoId + " AND \"ITEM_ID\" = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateGrnReectAddToGrnCount(int GrnId, int PoId, int ItemId, int count, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE \"GRN_DETAILS\"  SET \"ADD_TO_GRN_COUNT\" =" + count + "  WHERE \"GRN_ID\"=" + GrnId + " AND \"PO_ID\" = " + PoId + " AND \"ITEM_ID\" = " + ItemId + " AND \"IS_GRN_APPROVED\" =2 AND \"IS_GRN_RAISED\" = 2;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateGrndIssuedQty(int grndID, int issuedQty, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnDetails> GetGrnDetailsForPrInquiry(int GrnId, int CompanyId, DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public List<GrnDetails> GetGrnDetailsByPoId(int GrnId, int PoId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }

        public List<GrnDetails> GetGrnDetails(int GrnId, int PoId, int WarehouseId, DBConnection dbConnection) {
            throw new NotImplementedException();
        }
    }


    public class GRNDetailsDAOSQLImpl : GRNDetailsDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<GrnDetails> GetGrnDetailsByPoId(int GrnId, int CompanyId, DBConnection dbConnection)
        {
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText =
                                "SELECT GRN.*,GM.IS_APPROVED,AI.STOCK_MAINTAINING_TYPE, GM.WAREHOUSE_ID, AI.REFERENCE_NO, ITEM_NAME,UM.MEASUREMENT_SHORT_NAME, (GRN.QUANTITY*GRN.ITEM_PRICE) AS SUB_TOTAL FROM GRN_DETAILS AS GRN " +
                                "INNER JOIN ADD_ITEMS AS AI ON AI.ITEM_ID = GRN.ITEM_ID " +
                                "INNER JOIN GRN_MASTER AS GM ON GM.GRN_ID = GRN.GRN_ID " +
                                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UM ON UM.DETAIL_ID= GRN.MEASUREMENT_ID \n" +
                                "WHERE GRN.GRN_ID = " + GrnId + " AND GRN.QUANTITY > 0 AND AI.COMPANY_ID=" + CompanyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }

        }
        public List<GrnDetails> GetGrnDetails(int GrnId, int CompanyId, int WarehouseId, DBConnection dbConnection) {
            AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText =
                                "SELECT GRN.*,GM.IS_APPROVED,AI.STOCK_MAINTAINING_TYPE, GM.WAREHOUSE_ID, AI.REFERENCE_NO, ITEM_NAME,UM.MEASUREMENT_SHORT_NAME, (GRN.QUANTITY*GRN.ITEM_PRICE) AS SUB_TOTAL, WIB.AVAILABLE_DETAIL_STOCK, WIM.AVAILABLE_MASTER_STOCK FROM GRN_DETAILS AS GRN " +
                                "LEFT JOIN ADD_ITEMS AS AI ON AI.ITEM_ID = GRN.ITEM_ID " +
                                "LEFT JOIN GRN_MASTER AS GM ON GM.GRN_ID = GRN.GRN_ID " +
                                "LEFT JOIN (SELECT WAREHOUSE_ID, ITEM_ID, AVAILABLE_QTY AS AVAILABLE_MASTER_STOCK FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = " + WarehouseId + ") AS WIM ON WIM.ITEM_ID = GRN.ITEM_ID " +
                                "LEFT JOIN (SELECT ITEM_ID, GRND_ID, AVAILABLE_QTY AS AVAILABLE_DETAIL_STOCK FROM WAREHOUSE_INVENTORY_BATCHES WHERE WAREHOUSE_ID = " + WarehouseId + ") AS WIB ON WIB.GRND_ID = GRN.GRND_ID " +
                                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UM ON UM.DETAIL_ID= GRN.MEASUREMENT_ID \n" +
                                "WHERE GRN.GRN_ID = " + GrnId + " AND GRN.QUANTITY > 0 AND AI.COMPANY_ID=" + CompanyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }

        }
        public GrnDetails GetGrnDetailsObjByPoIdandItemId(int GrnId, int PoId, int itemid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_DETAILS  WHERE PO_ID =" + PoId + " AND ITEM_ID =" + itemid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<GrnDetails>(dbConnection.dr);
            }
        }

        public int SaveGrnDetails(int GrnId, int PoId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".GRN_DETAILS (GRN_ID, PO_ID, ITEM_ID, ITEM_PRICE, QUANTITY, VAT_AMOUNT, NBT_AMOUNT,  TOTAL_AMOUNT, IS_GRN_RAISED, GRN_APPROVED_DATE_TIME,IS_GRN_APPROVED,ADD_TO_GRN_COUNT ) VALUES ( " + GrnId + ", " + PoId + " , " + itemId + "," + itemPrce + ", " + quntity + ", " + vatAmount + ", " + nbtAmount + ", " + totalAmount + ", " + 1 + ", '" + LocalTime.Now + "', " + 0 + "," + 0 + ");";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnDetails> GetGrnDetails(int PoId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_DETAILS  WHERE PO_ID =" + PoId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }

        public List<GrnDetails> GetGrnDetailsAll(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GRN_DETAILS  AS GD INNER JOIN " + dbLibrary + ".GRN_MASTER AS GM ON  GM.GRN_ID =  GD.GRN_ID WHERE GM.DEPARTMENT_ID = " + DepartmentId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }

        public List<GrnDetails> GrnDetialsGrnApproved(int GrnId, int PoId, int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " select * from " + dbLibrary + ".GRN_DETAILS AS GD " +
                                           " inner join " + dbLibrary + ".GRN_MASTER AS GM on GD.GRN_ID = GM.GRN_ID " +
                                           " inner join " + dbLibrary + ".ADD_ITEMS_MASTER AS AI  on AI.ITEM_ID = GD.ITEM_ID " +
                                           " where GD.IS_GRN_RAISED = 1 and GD.IS_GRN_APPROVED = 0 and GM.DEPARTMENT_ID = " + DepartmentId + " and GD.GRN_ID = " + GrnId + " and  GD.PO_ID = " + PoId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }

        public int UpdateApprovedGrn(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".GRN_DETAILS  SET IS_GRN_APPROVED =" + IsApproveStatus + ", GRN_APPROVED_DATE_TIME='" + ApprovedDatetime + "',GRN_APPROVED_BY =" + ApprovedBy + ", REASON_FOR_REJECT ='" + RejectedReason + "'  WHERE GRN_ID=" + GrnId + " AND PO_ID = " + PoId + " AND ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        //--Grn Reject
        public int UpdateGrnReectIsPoRaised(int GrnId, int PoId, int ItemId, int isGrnRaised, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".GRN_DETAILS  SET IS_GRN_RAISED =" + isGrnRaised + "  WHERE GRN_ID=" + GrnId + " AND PO_ID = " + PoId + " AND ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnDetails> GrnDetialsGrnApprovedOnly(int GrnId, int PoId, int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " select * from " + dbLibrary + ".GRN_DETAILS AS GD" +
                                           " inner join " + dbLibrary + ".GRN_MASTER AS GM on GD.GRN_ID = GM.GRN_ID " +
                                           " inner join " + dbLibrary + ".ADD_ITEMS_MASTER AS AI  on AI.ITEM_ID = GD.ITEM_ID " +
                                           " where GD.IS_GRN_RAISED = 1 and GD.IS_GRN_APPROVED = 1 and GM.DEPARTMENT_ID = " + DepartmentId + " and GD.GRN_ID = " + GrnId + " and  GD.PO_ID = " + PoId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }

        public int UpdateApprovedGrnNewGrnId(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason, int NewGrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".GRN_DETAILS  SET IS_GRN_APPROVED =" + IsApproveStatus + ", GRN_APPROVED_DATE_TIME='" + ApprovedDatetime + "',GRN_APPROVED_BY =" + ApprovedBy + ", REASON_FOR_REJECT ='" + RejectedReason + "',GRN_ID=" + NewGrnId + "  WHERE GRN_ID=" + GrnId + " AND PO_ID = " + PoId + " AND ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateGrnReectAddToGrnCount(int GrnId, int PoId, int ItemId, int count, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".GRN_DETAILS  SET ADD_TO_GRN_COUNT =" + count + "  WHERE GRN_ID=" + GrnId + " AND PO_ID = " + PoId + " AND ITEM_ID = " + ItemId + " AND IS_GRN_APPROVED =2 AND IS_GRN_RAISED = 2;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateGrndIssuedQty(int grndID, int issuedQty, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".GRN_DETAILS  SET ISSUED_QTY =ISSUED_QTY+" + issuedQty + "  WHERE GRND_ID=" + grndID;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GrnDetails> GetGrnDetailsForPrInquiry(int GrnId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM GRN_DETAILS AS GRND\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON GRND.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = GRND.MEASUREMENT_ID " +
                                             "WHERE GRND.GRN_ID = " + GrnId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GrnDetails>(dbConnection.dr);
            }
        }
    }
}
