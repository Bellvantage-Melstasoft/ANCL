using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface TabulationDetailDAO
    {
        List<TabulationDetail> GetTabulationDetailsByTabulationId(int TabulationId,  DBConnection dBConnection);
        List<TabulationDetail> GetTabulationDetailsByForPoCreation(int TabulationId, int CompanyId, int BidId, DBConnection dBConnection);
        int InsertTabulationDetails(int tabulationId, int bidid, DBConnection dBConnection);
        int UpdateTabulationDetails(int tabulationId, int qutationId, int bidid, int supplierId, int itemId, int qty, DBConnection dBConnection);
        int UpdateUnselectedTabulationDetails(int tabulationId, int qutationId, int bidid, int supplierId, int itemId, int qty, DBConnection dBConnection);
        int UpdateTabulationDetail(decimal Qty, decimal VAtAmount, decimal NbtAmount, decimal NetTotal, decimal SubTotal, int ItemId, int TabulationId, int QuotationId, int SupplierId, string remark, string SupMentionedName, int Finalized, int UserId, DBConnection dBConnection);
        List<TabulationDetail> GetTabulationDetailsByQuotationId(List<int> quotationIdList, DBConnection dBConnection);
        int TerminateItems(List<int> TabulationDetailIds, int UserId, string Remarks, DBConnection dbConnection);
        int  DeleteTabulationDetail(int ItemId, int TabulationId, int QuotationId, int SupplierId, int Finalized, DBConnection dbConnection);

        List<TabulationDetail> GetSelectedStatus(int QuotationId, int SupplierId,int ItemID, int QuotationItemId, DBConnection dbConnection);
        int InsertIntoImportDetails(ImportCalucationDetails objImportDetails, int TabulationID,  int QuotationItemId, DBConnection dbConnection);

        int DeleteImportDetails(int QuotationId, int TabulationID, DBConnection dbConnection);
        List<TabulationDetail> getSelectedQuotationDetails(int ItemId, int TabulationId, int QuotationId, int SupplierId, DBConnection dbConnection);
        TabulationDetail GetSelectedStatusForTabulation(int QuotationId, int SupplierId, int ItemID, int QuotationItemId, DBConnection dbConnection);
        List<TabulationDetail> GetTabulationDetail(int TabulationId, DBConnection dBConnection);
        int UpdateTabulationDetailImport(decimal Qty, decimal VAtAmount, decimal NbtAmount, decimal NetTotal, decimal SubTotal, int ItemId, int TabulationId, int QuotationId,  string remark, int UserId, decimal UnitPriceForeign, decimal UnitPriceLKR, string PurchaseType, decimal DayNo, DBConnection dbConnection);
        TabulationDetail GetSelectedQuotation(int QuotationId, int SupplierId, int ItemID, int QuotationItemId, DBConnection dbConnection);
        int DeleteTabulationDetailImport(int ItemId, int TabulationId, int QuotationId, int UserId, DBConnection dbConnection);
        List<TabulationDetail> GetTabulationDetailsForImportsByTabulationId(int TabulationId, DBConnection dBConnection);
        List<TabulationDetail> GetTabulationDetailImports(int TabulationId, DBConnection dBConnection);
        int UpdateTabulationDetailForCoveringPR(List<SupplierQuotationItem> supplierQuotationItem, int UserId, string Remark, DBConnection dbConnection);
        int updatePayementType(int QuotationId, DBConnection dbConnection);
        int UpdateUnitPrice(int QuotationId, int QuotationItemId, string term, decimal airFreigt, decimal insuarance, DBConnection dbConnection);
    }

    class TabulationDetailDAOImpl : TabulationDetailDAO
    {
        public List<TabulationDetail> GetTabulationDetailsByForPoCreation(int TabulationId, int CompanyId,int BidId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM TABULATION_DETAIL AS TD\n" +
                                            "INNER JOIN (SELECT ITEM_ID, BID_ID, PRD_ID FROM BIDDING_ITEM WHERE BID_ID = " + BidId+" ) AS BI ON BI.ITEM_ID = TD.ITEM_ID  " +
                                            "INNER JOIN (SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON TD.ITEM_ID = AIM.ITEM_ID\n" +
                                            "INNER JOIN (SELECT BID_ID, PR_ID FROM BIDDING ) AS B ON BI.BID_ID = B.BID_ID  " +
                                            "LEFT JOIN (SELECT PURCHASE_TYPE, PR_ID, IMPORT_ITEM_TYPE FROM PR_MASTER) AS PR ON PR.PR_ID = B.PR_ID\n" +
                                            "LEFT JOIN (SELECT PR_ID, PRD_ID, SPARE_PART_NUMBER FROM PR_DETAIL) AS PRD ON PRD.PRD_ID = BI.PRD_ID\n" +

                                            "LEFT JOIN (SELECT QUOTATION_ITEM_ID,QUOTATION_ID, CIF AS IMP_CIF, TERM AS IMP_TERM FROM IMPORT_QUOTATION_ITEM) AS IQI ON IQI.QUOTATION_ITEM_ID = TD.QUOTATION_ITEM_ID\n" +
                                           "LEFT JOIN (SELECT QUOTATION_ID, CURRENCY_TYPE_ID, SUPPLIER_AGENT FROM IMPORT_QUOTATION) AS IQ ON IQ.QUOTATION_ID = IQI.QUOTATION_ID\n" +
                                           "LEFT JOIN (SELECT CURRENCY_SHORT_NAME AS CURRENCY_NAME, CURRENCY_TYPE_ID FROM DEF_CURRENCY_TYPE) AS CUR ON IQ.CURRENCY_TYPE_ID = CUR.CURRENCY_TYPE_ID\n" +
                                           "LEFT JOIN (SELECT SUPPLIER_ID,SUPPLIER_NAME AS SUPPLIER_AGENT_NAME FROM SUPPLIER ) AS SUP ON SUP.SUPPLIER_ID = IQ.SUPPLIER_AGENT " +

                                            "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON TD.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                                           "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = TD.MEASUREMENT_ID \n" +
                                           "LEFT JOIN (SELECT QUOTATION_ID, QUOTATION_REFERENCE_CODE FROM SUPPLIER_QUOTATION) AS SQ ON SQ.QUOTATION_ID = TD.QUOTATION_ID \n" +
                                           "WHERE TD.IS_SELECTED=1 AND IS_ADDED_TO_PO=0 AND TD.IS_TERMINATED = 0 AND TD.TABULATION_ID = " + TabulationId;

            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dBConnection.dr);
            }
        }

        public List<TabulationDetail> GetTabulationDetailsByTabulationId(int TabulationId, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT TD.*, MD.MEASUREMENT_SHORT_NAME, BI.SUPPLIER_ITEM ,BID.BIDDING_ITEM_ID, BID.PRD_ID, TAM.BID_ID, AI.ITEM_NAME FROM TABULATION_DETAIL AS TD\n" +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON TD.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "LEFT JOIN (SELECT ITEM_ID, ITEM_NAME  FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = TD.ITEM_ID\n" +
                "LEFT JOIN (SELECT NAME AS CURRENCY_NAME ,ID  FROM CURRENCY) AS CU ON TD.CURRENCY_ID = CU.ID\n" +
                "LEFT JOIN (SELECT AGENT_NAME, AGENT_ID FROM SUPPLIER_AGENT) AS SA ON TD.AGENT_ID= SA.AGENT_ID\n" +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID= TD.MEASUREMENT_ID\n" +
                "LEFT JOIN (SELECT TABULATION_ID, BID_ID  FROM TABULATION_MASTER) AS TAM ON TAM.TABULATION_ID = TD.TABULATION_ID\n" +
                 "LEFT JOIN (SELECT BIDDING_ITEM_ID, BID_ID, PRD_ID  FROM BIDDING_ITEM) AS BID ON BID.BID_ID = TAM.BID_ID\n" +
                "LEFT JOIN (SELECT QUOTATION_ITEM_ID, SUPPLIER_MENTIONED_ITEM_NAME AS SUPPLIER_ITEM FROM SUPPLIER_QUOTATION_ITEM) AS BI ON BI.QUOTATION_ITEM_ID = TD.QUOTATION_ITEM_ID\n" +
                "WHERE TD.TABULATION_ID = " + TabulationId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dBConnection.dr);
            }
        }

        public List<TabulationDetail> GetTabulationDetailsForImportsByTabulationId(int TabulationId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM TABULATION_DETAIL AS TD\n" +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON TD.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "LEFT JOIN (SELECT ITEM_ID, ITEM_NAME  FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = TD.ITEM_ID\n" +
                "LEFT JOIN (SELECT NAME AS CURRENCY_NAME ,ID  FROM CURRENCY) AS CU ON TD.CURRENCY_ID = CU.ID\n" +
                "LEFT JOIN (SELECT AGENT_NAME, AGENT_ID FROM SUPPLIER_AGENT) AS SA ON TD.AGENT_ID= SA.AGENT_ID\n" +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID= TD.MEASUREMENT_ID\n" +
                "LEFT JOIN (SELECT TABULATION_ID, BID_ID  FROM TABULATION_MASTER) AS TAM ON TAM.TABULATION_ID = TD.TABULATION_ID\n" +
                 "LEFT JOIN (SELECT BIDDING_ITEM_ID, BID_ID, PRD_ID  FROM BIDDING_ITEM) AS BID ON BID.BID_ID = TAM.BID_ID\n" +
                "LEFT JOIN (SELECT QUOTATION_ITEM_ID,QUOTATION_ID, SUPPLIER_MENTIONED_ITEM_NAME AS SUPPLIER_ITEM FROM SUPPLIER_QUOTATION_ITEM) AS SQI ON SQI.QUOTATION_ITEM_ID = TD.QUOTATION_ITEM_ID\n" +
                "LEFT JOIN IMPORT_QUOTATION AS IQ ON IQ.QUOTATION_ID = SQI.QUOTATION_ID\n" +
                "LEFT JOIN (SELECT QUOTATION_ID, QUOTATION_ITEM_ID,BRAND AS IMP_BRAND,CIF AS IMP_CIF, CLEARING_COST AS IMP_CLEARING, OTHER AS IMP_OTHER, HISTORY AS IMP_HISTORY_ID, VALIDITY AS IMP_VALIDITY, EST_DELIVERY AS IMP_ESTDELIVERY, REMARK AS IMP_REMARK, TERM, MILL, XID_RATE, CID_RATE, PAL_RATE, EIC_RATE,AIR_FREIGHT, INSURENCE, VAT_RATE, EXCHANGE_RATE AS EXCHANGE_RATE_IMP, XID_VALUE, CID_VALUE, PAL_VALUE, EIC_VALUE, VAT_VALUE, DUTY_PAL, HS_ID AS NEW_HS_ID FROM IMPORT_QUOTATION_ITEM) AS IQI ON IQI.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID\n" +
               "LEFT JOIN (SELECT SUPPLIER_NAME AS SUPPLIER_AGENT_NAME, SUPPLIER_ID FROM SUPPLIER) AS SU ON SU.SUPPLIER_ID = IQ.SUPPLIER_AGENT\n" +
               "LEFT JOIN (SELECT ID, NAME AS COUNTRY_NAME FROM COUNTRY) AS CO ON CO.ID = IQ.COUNTRY\n" +
               "LEFT JOIN (SELECT CURRENCY_TYPE_ID, CURRENCY_NAME AS IMP_CURRENCY_NAME FROM DEF_CURRENCY_TYPE) AS DCU ON DCU.CURRENCY_TYPE_ID = IQ.CURRENCY_TYPE_ID\n" +
               "LEFT JOIN  (SELECT HISTORY_ID, HISTORY AS IMP_HISTORY FROM IMPORTS_HISTORY) AS IH ON IH.HISTORY_ID = IQI.IMP_HISTORY_ID\n" +
               "LEFT JOIN DEF_PRICE_TERMS AS TER ON TER.TERM_ID = IQI.TERM\n" +
                "WHERE TD.TABULATION_ID = " + TabulationId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dBConnection.dr);
            }
        }

        public List<TabulationDetail> GetTabulationDetail(int TabulationId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT TD.*, MD.MEASUREMENT_SHORT_NAME, BI.SUPPLIER_ITEM , AI.ITEM_NAME FROM TABULATION_DETAIL AS TD\n" +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON TD.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "LEFT JOIN (SELECT ITEM_ID, ITEM_NAME  FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = TD.ITEM_ID\n" +
                "LEFT JOIN (SELECT NAME AS CURRENCY_NAME ,ID  FROM CURRENCY) AS CU ON TD.CURRENCY_ID = CU.ID\n" +
                "LEFT JOIN (SELECT AGENT_NAME, AGENT_ID FROM SUPPLIER_AGENT) AS SA ON TD.AGENT_ID= SA.AGENT_ID\n" +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID= TD.MEASUREMENT_ID\n" +
                "LEFT JOIN (SELECT TABULATION_ID, BID_ID  FROM TABULATION_MASTER) AS TAM ON TAM.TABULATION_ID = TD.TABULATION_ID\n" +
                 //"LEFT JOIN (SELECT BIDDING_ITEM_ID, BID_ID, PRD_ID  FROM BIDDING_ITEM) AS BID ON BID.BID_ID = TAM.BID_ID\n" +
                "LEFT JOIN (SELECT QUOTATION_ITEM_ID, SUPPLIER_MENTIONED_ITEM_NAME AS SUPPLIER_ITEM FROM SUPPLIER_QUOTATION_ITEM) AS BI ON BI.QUOTATION_ITEM_ID = TD.QUOTATION_ITEM_ID\n" +
                "WHERE TD.TABULATION_ID = " + TabulationId;
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dBConnection.dr);
            }
        }

        public List<TabulationDetail> GetTabulationDetailImports(int TabulationId, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

          

            dBConnection.cmd.CommandText = "SELECT * FROM TABULATION_DETAIL AS TD\n" +
                "INNER JOIN (SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS S ON TD.SUPPLIER_ID= S.SUPPLIER_ID\n" +
                "LEFT JOIN (SELECT ITEM_ID, ITEM_NAME  FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = TD.ITEM_ID\n" +
                "LEFT JOIN (SELECT NAME AS CURRENCY_NAME ,ID  FROM CURRENCY) AS CU ON TD.CURRENCY_ID = CU.ID\n" +
                "LEFT JOIN (SELECT AGENT_NAME, AGENT_ID FROM SUPPLIER_AGENT) AS SA ON TD.AGENT_ID= SA.AGENT_ID\n" +
                "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID= TD.MEASUREMENT_ID\n" +
                "LEFT JOIN (SELECT TABULATION_ID, BID_ID  FROM TABULATION_MASTER) AS TAM ON TAM.TABULATION_ID = TD.TABULATION_ID\n" +
                "LEFT JOIN (SELECT QUOTATION_ITEM_ID,QUOTATION_ID, SUPPLIER_MENTIONED_ITEM_NAME AS SUPPLIER_ITEM FROM SUPPLIER_QUOTATION_ITEM) AS SQI ON SQI.QUOTATION_ITEM_ID = TD.QUOTATION_ITEM_ID\n" +
                "LEFT JOIN IMPORT_QUOTATION AS IQ ON IQ.QUOTATION_ID = SQI.QUOTATION_ID\n" +
                "LEFT JOIN (SELECT QUOTATION_ID, QUOTATION_ITEM_ID,BRAND AS IMP_BRAND,CIF AS IMP_CIF, CLEARING_COST AS IMP_CLEARING, OTHER AS IMP_OTHER, HISTORY AS IMP_HISTORY_ID, VALIDITY AS IMP_VALIDITY, EST_DELIVERY AS IMP_ESTDELIVERY, REMARK AS IMP_REMARK, TERM, MILL, XID_RATE, CID_RATE, PAL_RATE, EIC_RATE,AIR_FREIGHT, INSURENCE, VAT_RATE, EXCHANGE_RATE AS EXCHANGE_RATE_IMP, XID_VALUE, CID_VALUE, PAL_VALUE, EIC_VALUE, VAT_VALUE, DUTY_PAL, HS_ID AS NEW_HS_ID FROM IMPORT_QUOTATION_ITEM) AS IQI ON IQI.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID\n" +
               "LEFT JOIN (SELECT SUPPLIER_NAME AS SUPPLIER_AGENT_NAME, SUPPLIER_ID FROM SUPPLIER) AS SU ON SU.SUPPLIER_ID = IQ.SUPPLIER_AGENT\n" +
               "LEFT JOIN (SELECT ID, NAME AS COUNTRY_NAME FROM COUNTRY) AS CO ON CO.ID = IQ.COUNTRY\n" +
               "LEFT JOIN (SELECT CURRENCY_TYPE_ID, CURRENCY_NAME AS IMP_CURRENCY_NAME FROM DEF_CURRENCY_TYPE) AS DCU ON DCU.CURRENCY_TYPE_ID = IQ.CURRENCY_TYPE_ID\n" +
               "LEFT JOIN  (SELECT HISTORY_ID, HISTORY AS IMP_HISTORY FROM IMPORTS_HISTORY) AS IH ON IH.HISTORY_ID = IQI.IMP_HISTORY_ID\n" +
               "LEFT JOIN DEF_PRICE_TERMS AS TER ON TER.TERM_ID = IQI.TERM\n" +
                "WHERE TD.TABULATION_ID = " + TabulationId;

            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dBConnection.dr);
            }
        }

        public int InsertTabulationDetails(int tabulationId, int bidid, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();
            //dBConnection.cmd.CommandText = "[usp_InsertTabulationDetails]";
            //dBConnection.cmd.Parameters.AddWithValue("@tabulationId", tabulationId);
            //dBConnection.cmd.Parameters.AddWithValue("@bidId", bidid);
            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return int.Parse(dBConnection.cmd.ExecuteScalar().ToString());
        }
        public int UpdateTabulationDetails(int tabulationId,int qutationId, int bidid,int supplierId,int itemId,int qty, DBConnection dBConnection)
        {
            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "[usp_UpdateTabulationDetailsSelectedItems]";
            dBConnection.cmd.Parameters.AddWithValue("@tabulationId", tabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@qutationId", qutationId);
            dBConnection.cmd.Parameters.AddWithValue("@bidid", bidid);
            dBConnection.cmd.Parameters.AddWithValue("@supplierId", supplierId);
            dBConnection.cmd.Parameters.AddWithValue("@itemId", itemId);
            dBConnection.cmd.Parameters.AddWithValue("@qty", qty);
            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateUnselectedTabulationDetails(int tabulationId, int qutationId, int bidid, int supplierId, int itemId, int qty, DBConnection dBConnection)
        {

            dBConnection.cmd.Parameters.Clear();
            dBConnection.cmd.CommandText = "[usp_UpdateTabulationDetailsUnSelectedItems]";
            dBConnection.cmd.Parameters.AddWithValue("@tabulationId", tabulationId);
            dBConnection.cmd.Parameters.AddWithValue("@qutationId", qutationId);
            dBConnection.cmd.Parameters.AddWithValue("@bidid", bidid);
            dBConnection.cmd.Parameters.AddWithValue("@supplierId", supplierId);
            dBConnection.cmd.Parameters.AddWithValue("@itemId", itemId);
            dBConnection.cmd.Parameters.AddWithValue("@qty", qty);
            dBConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;

            return dBConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateTabulationDetail(decimal Qty, decimal VAtAmount, decimal NbtAmount, decimal NetTotal, decimal SubTotal, int ItemId, int TabulationId, int QuotationId, int SupplierId, string remark,string SupMentionedName,int Finalized, int UserId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = @"UPDATE TABULATION_DETAIL SET QTY="+ Qty + ", VAT_AMOUNT = "+ VAtAmount + ",NBT_AMOUNT="+ NbtAmount + ",NET_TOTAL=" + NetTotal + ",SUB_TOTAL="+ SubTotal + ",IS_SELECTED=1,APPROVAL_REMARK = '" + remark + "', IS_FINALIZED=" + Finalized + ", QUOTATION_SELECTED_BY = "+ UserId + ", QUOTATION_SELECTED_ON = '"+LocalTime.Now+"' " +
                                            "WHERE ITEM_ID = "+ ItemId + " AND TABULATION_ID= "+ TabulationId + " AND QUOTATION_ID="+ QuotationId + "  AND SUPPLIER_ID="+ SupplierId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateTabulationDetailImport(decimal Qty, decimal VAtAmount, decimal NbtAmount, decimal NetTotal, decimal SubTotal, int ItemId, int TabulationId, int QuotationId, string remark, int UserId, decimal UnitPriceForeign, decimal UnitPriceLKR, string PurchaseType, decimal DayNo, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = @"UPDATE TABULATION_DETAIL SET QTY=" + Qty + ", VAT_AMOUNT = " + VAtAmount + ",NBT_AMOUNT=" + NbtAmount + ",NET_TOTAL=" + NetTotal + ",SUB_TOTAL=" + SubTotal + ",IS_SELECTED=1,APPROVAL_REMARK = '" + remark + "',  QUOTATION_SELECTED_BY = " + UserId + ", QUOTATION_SELECTED_ON = '" + LocalTime.Now + "' ,  UNIT_PRICE_FOREIGN = " + UnitPriceForeign + " ,  UNIT_PRICE_LOCAL = " + UnitPriceLKR + ",  DAY_NO = " + DayNo + " ,  PAYMENT_TYPE = '" + PurchaseType + "' " +
                                            "WHERE ITEM_ID = " + ItemId + " AND TABULATION_ID= " + TabulationId + " AND QUOTATION_ID=" + QuotationId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateTabulationDetailForCoveringPR(List<SupplierQuotationItem> supplierQuotationItem, int UserId, string Remark, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            for (int i = 0; i < supplierQuotationItem.Count; i++) {
            dbConnection.cmd.CommandText += "UPDATE TABULATION_DETAIL SET QTY=" + supplierQuotationItem[i].Qty + ", VAT_AMOUNT = " + supplierQuotationItem[i].VatAmount + ",NBT_AMOUNT=" + supplierQuotationItem[i].NbtAmount + ",NET_TOTAL=" + supplierQuotationItem[i].TotalAmount + ",SUB_TOTAL=" + supplierQuotationItem[i].SubTotal + ",IS_SELECTED=1,APPROVAL_REMARK = '" + Remark + "',  QUOTATION_SELECTED_BY = " + UserId + ", QUOTATION_SELECTED_ON = '" + LocalTime.Now + "' " +
                                            "WHERE ITEM_ID = " + supplierQuotationItem[i].ItemId + " AND QUOTATION_ID=" + supplierQuotationItem[i].QuotationId + "  ";
            }
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTabulationDetailImport( int ItemId, int TabulationId, int QuotationId,  int UserId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE TABULATION_DETAIL SET QTY=0, VAT_AMOUNT = 0,NBT_AMOUNT=0,NET_TOTAL=0,SUB_TOTAL=0,IS_SELECTED=0,APPROVAL_REMARK = '',  QUOTATION_SELECTED_BY = " + UserId + ", QUOTATION_SELECTED_ON = '" + LocalTime.Now + "', UNIT_PRICE_LOCAL=0, UNIT_PRICE_FOREIGN=0 " +
                                            "WHERE ITEM_ID = " + ItemId + " AND TABULATION_ID= " + TabulationId + " AND QUOTATION_ID=" + QuotationId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateUnitPrice(int QuotationId, int QuotationItemId, string term, decimal airFreigt, decimal insuarance,DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            decimal UPLocal = 0;
            dbConnection.cmd.CommandText = "SELECT CIF FROM IMPORT_QUOTATION_ITEM WHERE  QUOTATION_ID=" + QuotationId + " AND QUOTATION_ITEM_ID = " + QuotationItemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            decimal unitPrice = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandText = "SELECT EXCHANGE_RATE FROM IMPORT_QUOTATION_ITEM WHERE  QUOTATION_ID=" + QuotationId + " AND QUOTATION_ITEM_ID = " + QuotationItemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            decimal Rate = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if(term == "9" || term == "8") {
                UPLocal = unitPrice * Rate * 1.001m;
            }
            else if (term == "3" || term == "4" || term == "16" || term == "17") {
                UPLocal = (unitPrice * Rate) + (airFreigt + insuarance);
            }
            else if (term == "11") {
                UPLocal = unitPrice;
            }
            else {
                UPLocal = unitPrice * Rate;
            }


            dbConnection.cmd.CommandText = "UPDATE TABULATION_DETAIL SET UNIT_PRICE_FOREIGN = (SELECT CIF FROM IMPORT_QUOTATION_ITEM WHERE  QUOTATION_ID=" + QuotationId + " AND QUOTATION_ITEM_ID = " + QuotationItemId + " ), UNIT_PRICE_LOCAL = "+ UPLocal + " " +
                                            "WHERE  QUOTATION_ID=" + QuotationId + " AND QUOTATION_ITEM_ID = "+ QuotationItemId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int updatePayementType(int QuotationId,  DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT PAYMENT_MODE_ID FROM IMPORT_QUOTATION WHERE QUOTATION_ID = " + QuotationId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            int ID = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            string Type = "0";
            if (ID == 1) {
                Type = "Advance";
            }
            else if (ID == 2) {
                Type = "On Arrival";
            }
            else if (ID == 3) {
                Type = "LC at sight";
            }
            else if (ID == 4) {
                Type = "L/C usance";
            }
            else if (ID == 5) {
                Type = "D/A";
            }
            else if (ID == 6) {
                Type = "D/P";
            }
            

            dbConnection.cmd.CommandText = "UPDATE TABULATION_DETAIL SET DAY_NO = (SELECT NO_OF_DAYS_PAYEMENT_MODE FROM IMPORT_QUOTATION WHERE QUOTATION_ID = "+ QuotationId + ") , PAYMENT_TYPE = '"+ Type + "' " +
                                            "WHERE QUOTATION_ID=" + QuotationId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TabulationDetail> GetTabulationDetailsByQuotationId(List<int> quotationIdList, DBConnection dBConnection) {
            dBConnection.cmd.Parameters.Clear();

            dBConnection.cmd.CommandText = "SELECT * FROM TABULATION_DETAIL AS TD \n" +
                                            "INNER JOIN(SELECT ITEM_ID, ITEM_NAME FROM ADD_ITEMS) AS AI ON AI.ITEM_ID = TD.ITEM_ID \n" +
                                            "INNER JOIN(SELECT SUPPLIER_ID, SUPPLIER_NAME FROM SUPPLIER) AS SUP ON SUP.SUPPLIER_ID = TD.SUPPLIER_ID \n" +
                                           "WHERE QUOTATION_ID IN (" + string.Join(",", quotationIdList) + ") ";
            dBConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dBConnection.dr = dBConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dBConnection.dr);
            }
        }

        public int TerminateItems(List<int> TabulationDetailIds, int UserId, string Remarks,  DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            StringBuilder sql = new StringBuilder();

             sql.AppendLine("UPDATE TABULATION_DETAIL SET IS_TERMINATED = 1, TERMINATED_BY=" + UserId + ",TERMINATED_DATE= '" + LocalTime.Now + "',TERMINATION_REMARKS ='" + Remarks.ProcessString() + "' WHERE TABULATION_DETAIL_ID IN(" + string.Join(",", TabulationDetailIds) + "); ");
            
            //sql.AppendLine("UPDATE PR_DETAIL SET CURRENT_STATUS= (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='TERM') WHERE PRD_ID IN (");
            //sql.AppendLine("SELECT PRD_ID FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID IN (SELECT BIDDING_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ITEM_ID IN(SELECT QUOTATION_ITEM_ID FROM TABULATION_DETAIL WHERE TABULATION_DETAIL_ID IN(" + string.Join(",", TabulationDetailIds) + "))))");

            //Inserting PR_DETAIL Status Update log
            sql.AppendLine("INSERT INTO PR_DETAIL_STATUS_LOG");
            sql.AppendLine("SELECT PRD_ID,(SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='TERM'),'" + LocalTime.Now + "'," + UserId + " FROM BIDDING_ITEM WHERE BIDDING_ITEM_ID IN (SELECT BIDDING_ITEM_ID FROM SUPPLIER_QUOTATION_ITEM WHERE QUOTATION_ITEM_ID IN(SELECT QUOTATION_ITEM_ID FROM TABULATION_DETAIL WHERE TABULATION_DETAIL_ID IN(" + string.Join(",", TabulationDetailIds) + ")))");

            dbConnection.cmd.CommandText = sql.ToString();
            return dbConnection.cmd.ExecuteNonQuery();
        }


        public int DeleteTabulationDetail( int ItemId, int TabulationId, int QuotationId, int SupplierId, int Finalized, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = @"UPDATE TABULATION_DETAIL SET IS_SELECTED=0, QTY = 0, SUB_TOTAL = 0, VAT_AMOUNT = 0, NBT_AMOUNT = 0,NET_TOTAL = 0, APPROVAL_REMARK = '', IS_FINALIZED=" + Finalized + "  , QUOTATION_SELECTED_BY = 0, QUOTATION_SELECTED_ON = '' " +
                                            "WHERE ITEM_ID = " + ItemId + " AND TABULATION_ID= " + TabulationId + " AND QUOTATION_ID=" + QuotationId + "  AND SUPPLIER_ID=" + SupplierId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TabulationDetail> GetSelectedStatus(int QuotationId, int SupplierId,int ItemID, int QuotationItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM TABULATION_DETAIL WHERE QUOTATION_ID=" + QuotationId + "  AND SUPPLIER_ID=" + SupplierId + " AND ITEM_ID ="+ ItemID + " AND QUOTATION_ITEM_ID="+ QuotationItemId + " AND IS_SELECTED=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dbConnection.dr);
            }
        }

        public TabulationDetail GetSelectedQuotation(int QuotationId, int SupplierId, int ItemID, int QuotationItemId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM TABULATION_DETAIL WHERE QUOTATION_ID=" + QuotationId + "  AND SUPPLIER_ID=" + SupplierId + " AND ITEM_ID =" + ItemID + " AND QUOTATION_ITEM_ID=" + QuotationItemId + " AND IS_SELECTED=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<TabulationDetail>(dbConnection.dr);
            }
        }


        public TabulationDetail GetSelectedStatusForTabulation(int QuotationId, int SupplierId, int ItemID, int QuotationItemId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM TABULATION_DETAIL WHERE QUOTATION_ID=" + QuotationId + "  AND SUPPLIER_ID=" + SupplierId + " AND ITEM_ID =" + ItemID + " AND QUOTATION_ITEM_ID=" + QuotationItemId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<TabulationDetail>(dbConnection.dr);
            }
        }

        public int InsertIntoImportDetails(ImportCalucationDetails objImportDetails,int TabulationID, int QuotationItemId,DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = @"INSERT INTO IMPORT_TABULATION_DETAIL(QUOTATION_ID,TABULATION_ID,ORIGINAL_CIF_AMOUNT,CIF_AMOUNT_LKR,DUTY_PAL_OTHER,DUTY_PAL_AMOUNT,LANDED_COST_LKR,COST_OF_CHEMICALS,CLEARING_COST_LKR,EST_DELIVERY,CLEARING_COST,EXCHANGE_RATE,QUOTATION_ITEMID) " +
                                            "VALUES("+ objImportDetails .QuotationId+ "," + TabulationID + "," + objImportDetails.OrginalCIFAmount + "," + objImportDetails.CIFAmountLKR + "," + objImportDetails.DuctyPALOther + "," + objImportDetails.DutyPALAmount + "," +
                                            "" + objImportDetails.LandedCostLKR + "," + objImportDetails.CostOfChemicals + "," + objImportDetails.ClearingCostLKR + ",'" + objImportDetails.EstDelivery + "'," + objImportDetails.ClearingCost + ","+ objImportDetails .ExchangeRateValueOld+ ","+ QuotationItemId + ")";
            
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteImportDetails(int QuotationId, int TabulationID, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = @"DELETE FROM  IMPORT_TABULATION_DETAIL WHERE QUOTATION_ID=" + QuotationId + " AND TABULATION_ID="+ TabulationID + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TabulationDetail> getSelectedQuotationDetails(int ItemId, int TabulationId, int QuotationId, int SupplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM TABULATION_DETAIL WHERE ITEM_ID = " + ItemId + " AND TABULATION_ID= " + TabulationId + " AND QUOTATION_ID=" + QuotationId + "  AND SUPPLIER_ID=" + SupplierId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TabulationDetail>(dbConnection.dr);
            }

        }
    }
}
