using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface SupplierQuotationItemDAO
    {
        //New Methods By Salman created on 2019-01-17
        List<SupplierQuotationItem> GetAllQuotationItems(int QuotationId, int CompanyId, DBConnection dbConnection);
        SupplierQuotationItem GetQuotationItemsByQuotationItemId(int quotationItemId, DBConnection dbConnection);
        List<SupplierQuotationItem> GetAllQuotationItemsByBidItemId(int BidItemId, int CompanyId, DBConnection dbConnection);
        int RejectQuotationItems(int QuotationItemId, string remark, DBConnection dbConnection);
        SupplierQuotationItem GetSelectedQuotationItem(int BidItemId, int CompanyId, DBConnection dbConnection);
        int SelectQuotationItem(int QuotationItemId, int UserId, string Remarks, DBConnection dbConnection);
        int RejectQuotationItem(int QuotationItemId, int UserId, string Remarks, DBConnection dbConnection);
        int GetCount(int quotationItemId, DBConnection dbConnection);
        List<SupplierQuotationItem> GetAllQuotationImportItems(int QuotationId, int CompanyId, DBConnection dbConnection);
        int CreateCoveringPrQuotations(int ParentPrId, decimal TotalVat, decimal TotalSubTotal, decimal TotalNetTotal, int UserId, DBConnection dbConnection);
        List<SupplierQuotationItem> GetAllQuotationItemsForCoveringPR(int QuotationId, DBConnection dbConnection);
    }
    class SupplierQuotationItemDAOImpl : SupplierQuotationItemDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<SupplierQuotationItem> GetAllQuotationItems(int QuotationId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION_ITEM AS SQI\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON SQI.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT BIDDING_ITEM_ID, PRD_ID FROM BIDDING_ITEM) AS BI ON BI.BIDDING_ITEM_ID = SQI.BIDDING_ITEM_ID \n" +
                                             "INNER JOIN (SELECT PRD_ID, MEASUREMENT_ID FROM PR_DETAIL) AS PD ON PD.PRD_ID = BI.PRD_ID \n" +
                                             "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = PD.MEASUREMENT_ID \n" +
                                             "WHERE SQI.QUOTATION_ID = " + QuotationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotationItem>(dbConnection.dr);
            }
        }

        public List<SupplierQuotationItem> GetAllQuotationItemsForCoveringPR(int QuotationId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION_ITEM AS SQI\n" +
                                             "WHERE SQI.QUOTATION_ID = " + QuotationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotationItem>(dbConnection.dr);
            }
        }

        public List<SupplierQuotationItem> GetAllQuotationImportItems(int QuotationId, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION_ITEM AS SQI\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON SQI.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT BIDDING_ITEM_ID, PRD_ID FROM BIDDING_ITEM) AS BI ON BI.BIDDING_ITEM_ID = SQI.BIDDING_ITEM_ID \n" +
                                             "INNER JOIN (SELECT PRD_ID, MEASUREMENT_ID FROM PR_DETAIL) AS PD ON PD.PRD_ID = BI.PRD_ID \n" +
                                             "INNER JOIN (SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = PD.MEASUREMENT_ID \n" +
                                             "LEFT JOIN (SELECT QUOTATION_ID, QUOTATION_REFERENCE_CODE FROM SUPPLIER_QUOTATION) AS SUPQ ON SUPQ.QUOTATION_ID = SQI.QUOTATION_ID\n" +
                                            "LEFT JOIN IMPORT_QUOTATION AS IQ ON IQ.QUOTATION_ID = SQI.QUOTATION_ID\n" +
                                            "LEFT JOIN (SELECT QUOTATION_ID, QUOTATION_ITEM_ID,BRAND AS IMP_BRAND,CIF AS IMP_CIF, CLEARING_COST AS IMP_CLEARING, OTHER AS IMP_OTHER, HISTORY AS IMP_HISTORY_ID, VALIDITY AS IMP_VALIDITY, EST_DELIVERY AS IMP_ESTDELIVERY, REMARK AS IMP_REMARK, TERM, MILL, XID_RATE, CID_RATE, PAL_RATE, EIC_RATE,AIR_FREIGHT, INSURENCE, VAT_RATE, EXCHANGE_RATE AS EXCHANGE_RATE_IMP, XID_VALUE, CID_VALUE, PAL_VALUE, EIC_VALUE, VAT_VALUE, DUTY_PAL, HS_ID AS NEW_HS_ID FROM IMPORT_QUOTATION_ITEM) AS IQI ON IQI.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID\n" +
                                            "LEFT JOIN (SELECT SUPPLIER_NAME AS SUPPLIER_AGENT_NAME, SUPPLIER_ID FROM SUPPLIER) AS SA ON SA.SUPPLIER_ID = IQ.SUPPLIER_AGENT\n" +
                                             "LEFT JOIN (SELECT ID, NAME AS COUNTRY_NAME FROM COUNTRY) AS CO ON CO.ID = IQ.COUNTRY\n" +
                                             "LEFT JOIN (SELECT CURRENCY_TYPE_ID, CURRENCY_NAME FROM DEF_CURRENCY_TYPE) AS CU ON CU.CURRENCY_TYPE_ID = IQ.CURRENCY_TYPE_ID\n" +
                                             "LEFT JOIN  (SELECT HISTORY_ID, HISTORY AS IMP_HISTORY FROM IMPORTS_HISTORY) AS IH ON IH.HISTORY_ID = IQI.IMP_HISTORY_ID\n" +
                                             "LEFT JOIN DEF_PRICE_TERMS AS TER ON TER.TERM_ID = IQI.TERM\n" +
                                             //"LEFT JOIN(SELECT TABULATION_ID, QUOTATION_ITEM_ID FROM TABULATION_DETAIL) AS TABD ON TABD.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID "+
                                             "WHERE SQI.QUOTATION_ID = " + QuotationId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotationItem>(dbConnection.dr);
            }
        }

        public SupplierQuotationItem GetQuotationItemsByQuotationItemId(int quotationItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM "+ dbLibrary + ".SUPPLIER_QUOTATION_ITEM AS SQI\n" +                                             
                                           "WHERE SQI.QUOTATION_ITEM_ID = " + quotationItemId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierQuotationItem>(dbConnection.dr);
            }
        }

        

        public List<SupplierQuotationItem> GetAllQuotationItemsByBidItemId(int BidItemId, int CompanyId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION_ITEM AS SQI\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON SQI.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             //"LEFT JOIN (SELECT QUOTATION_ITEM_ID, IS_SELECTED AS SELECTED_QUOTATION, QUOTATION_SELECTED_ON, QUOTATION_SELECTED_BY, QTY AS T_QTY, SUB_TOTAL AS T_SUB_TOT, VAT_AMOUNT AS T_VAT, NET_TOTAL AS T_NET FROM TABULATION_DETAIL) AS TQ ON TQ.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID "+
                                             "LEFT JOIN (SELECT QUOTATION_ITEM_ID, IS_SELECTED AS SELECTED_QUOTATION, QUOTATION_SELECTED_ON, QUOTATION_SELECTED_BY, QTY AS T_QTY, SUB_TOTAL AS T_SUB_TOT, VAT_AMOUNT AS T_VAT, NET_TOTAL AS T_NET, APPROVAL_REMARK AS SUPPLIER_APPROVAL_REMARK FROM TABULATION_DETAIL) AS TQ ON TQ.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID " +
                                             "LEFT JOIN (SELECT USER_ID, USER_NAME AS SELECTED_BY_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = TQ.QUOTATION_SELECTED_BY " +
                                            
                                             "WHERE SQI.BIDDING_ITEM_ID = " + BidItemId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierQuotationItem>(dbConnection.dr);
            }
        }

        public int RejectQuotationItems(int QuotationItemId, string remark, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE SUPPLIER_QUOTATION_ITEM SET IS_QUOTATION_ITEM_APPROVED = 2 , IS_QUOTATION_ITEM_APPROVAL_REMARK = '" + remark + "' WHERE QUOTATION_ITEM_ID = " + QuotationItemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetCount(int quotationItemId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT COUNT(QUOTATION_ITEM_ID) FROM SUPPLIER_QUOTATION_ITEM WHERE BIDDING_ITEM_ID =  " + quotationItemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public SupplierQuotationItem GetSelectedQuotationItem(int BidItemId, int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION_ITEM AS SQI\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON SQI.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT PRD_ID, BIDDING_ITEM_ID FROM BIDDING_ITEM) AS BI ON SQI.BIDDING_ITEM_ID = BI.BIDDING_ITEM_ID \n" +
                                             "INNER JOIN (SELECT PRD_ID,UNIT FROM PR_DETAIL) AS PRD ON PRD.PRD_ID= BI.PRD_ID\n" +
                                             "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PRD.UNIT \n" +
                                             "WHERE SQI.BIDDING_ITEM_ID = " + BidItemId + " AND SQI.IS_SELECTED = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;


            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierQuotationItem>(dbConnection.dr);
            }
        }
        public int RejectQuotationItem(int QuotationItemId, int UserId, string Remarks, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            string sql = "UPDATE SUPPLIER_QUOTATION_ITEM SET IS_SELECTED = 2, [QUOTATION_SELECTED_BY]=" + UserId + ", [QUOTATION_SELECTION_DATE] ='" + LocalTime.Now + "',[SELECTION_REMARKS]='" + Remarks.ProcessString() + "' WHERE QUOTATION_ITEM_ID = " + QuotationItemId + ";\n";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SelectQuotationItem(int QuotationItemId, int UserId, string Remarks, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            string sql = "UPDATE SUPPLIER_QUOTATION_ITEM SET IS_SELECTED = 1, [QUOTATION_SELECTED_BY]=" + UserId + ", [QUOTATION_SELECTION_DATE] ='" + LocalTime.Now + "',[SELECTION_REMARKS]='" + Remarks.ProcessString() + "' WHERE QUOTATION_ITEM_ID = " + QuotationItemId + ";\n";

            sql += "UPDATE SUPPLIER_QUOTATION SET IS_SELECTED = 1 WHERE QUOTATION_ID = (SELECT QUOTATION_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ITEM_ID = " + QuotationItemId + ");\n";

            sql += "UPDATE BIDDING_ITEM SET IS_QUOTATION_SELECTED = 1, [QUOTATION_SELECTED_BY]=" + UserId + ", [QUOTATION_SELECTION_DATE] ='" + LocalTime.Now + "',[SELECTION_REMARKS]='" + Remarks.ProcessString() + "' WHERE BIDDING_ITEM_ID = (SELECT BIDDING_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ITEM_ID = " + QuotationItemId + ");\n";

            sql += "UPDATE PR_DETAIL SET CURRENT_STATUS= 5 WHERE PRD_ID =(SELECT PRD_ID FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID = (SELECT BIDDING_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ITEM_ID = " + QuotationItemId + ")); ";

            sql += "INSERT INTO PR_DETAIL_STATUS_LOG  SELECT PRD_ID,6,'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID = (SELECT BIDDING_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ITEM_ID = " + QuotationItemId + "); ";

            dbConnection.cmd.CommandText = sql;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int CreateCoveringPrQuotations(int ParentPrId, decimal TotalVat, decimal TotalSubTotal, decimal TotalNetTotal,int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "CLONE_COVERING_PR_QUOTATIONS";
            
            dbConnection.cmd.Parameters.AddWithValue("@PARENT_PR_ID", ParentPrId);
            dbConnection.cmd.Parameters.AddWithValue("@SUB_TOTAL", TotalSubTotal);
            dbConnection.cmd.Parameters.AddWithValue("@VAT", TotalVat);
            dbConnection.cmd.Parameters.AddWithValue("@NET_TOTAL", TotalNetTotal);
            dbConnection.cmd.Parameters.AddWithValue("@USER_ID", UserId);

            dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
            int X = int.Parse(dbConnection.cmd.ExecuteNonQuery().ToString());
            return X;
        }
    }
}
