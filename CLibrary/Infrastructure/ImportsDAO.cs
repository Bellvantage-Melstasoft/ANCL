using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface ImportsDAO
    {
        int InsertImportsRates(List<Imports> importRates, DBConnection DbConnection);
        CurrencyRate GetCurrencyDetailsforSupplier( int QuotationId, DBConnection DbConnection);
        int UpdateRates(CurrencyRate objCurrency, DBConnection DbConnection);
        ImportCalucationDetails GetImportDetails(int QuotationId, DBConnection DbConnection);

        CurrencyRate GetExchangeRate(int currenctyTypeId, DBConnection DbConnection);
        List<ImportQuotationItem> GetImportDetailsListForTabulationReview(int bidId, int BidItemId,DBConnection DbConnection);
        ImportCalucationDetails GetExchangeRateSelected(int QuotationId, int TabulationId, int QuotationItemId, DBConnection DbConnection);
        List<ImportQuotationItem> GetAllImportQuotations(int QuotationId, DBConnection DbConnection);
        int UpdateImportValues(List<ImportQuotationItem> ImportQuotationItemList, DBConnection DbConnection);
    }

    public class ImportsDAOImp : ImportsDAO
    {
        public CurrencyRate GetCurrencyDetailsforSupplier( int QuotationId, DBConnection DbConnection)
        {
            DbConnection.cmd.Parameters.Clear();

            DbConnection.cmd.CommandText = @"SELECT DC.*,CR.* FROM IMPORT_QUOTATION AS IM 
                                            LEFT JOIN  DEF_CURRENCY_TYPE AS DC ON DC.CURRENCY_TYPE_ID = IM.CURRENCY_TYPE_ID
                                            LEFT JOIN  CURRENCY_RATE AS CR ON DC.CURRENCY_TYPE_ID = CR.CURRENCY_TYPE_ID
                                            WHERE IM.QUOTATION_ID = " + QuotationId + " AND CR.DATE=(SELECT MAX(DATE) FROM CURRENCY_RATE WHERE CURRENCY_TYPE_ID =IM.CURRENCY_TYPE_ID)";
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (DbConnection.dr = DbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CurrencyRate>(DbConnection.dr);
            }
        }

        public CurrencyRate GetExchangeRate(int currenctyTypeId, DBConnection DbConnection)
        {
            DbConnection.cmd.Parameters.Clear();

            DbConnection.cmd.CommandText = @"SELECT * FROM CURRENCY_RATE WHERE CURRENCY_TYPE_ID="+ currenctyTypeId + " AND DATE=(SELECT MAX(DATE) FROM CURRENCY_RATE WHERE CURRENCY_TYPE_ID ="+ currenctyTypeId + ")";
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (DbConnection.dr = DbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<CurrencyRate>(DbConnection.dr);
            }
        }

        public ImportCalucationDetails GetExchangeRateSelected(int QuotationId, int TabulationId, int QuotationItemId,DBConnection DbConnection)
        {
            DbConnection.cmd.Parameters.Clear();

            DbConnection.cmd.CommandText = @"SELECT * FROM IMPORT_TABULATION_DETAIL WHERE QUOTATION_ID="+ QuotationId + " AND TABULATION_ID="+ TabulationId + " AND QUOTATION_ITEMID="+ QuotationItemId + "";
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (DbConnection.dr = DbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ImportCalucationDetails>(DbConnection.dr);
            }
        }

        public ImportCalucationDetails GetImportDetails(int QuotationId, DBConnection DbConnection)
        {
            DbConnection.cmd.Parameters.Clear();

            DbConnection.cmd.CommandText = @"SELECT CU.NAME,SU.SUPPLIER_NAME AS AGENT ,DT.TERM_NAME AS TERM,IH.HISTORY,DF.PAYMENT_MODE,IT.*, CT.CURRENCY_SHORT_NAME FROM IMPORT_QUOTATION AS IM 
                                            LEFT JOIN  IMPORT_QUOTATION_ITEM AS IT ON IT.QUOTATION_ID = IM.QUOTATION_ID
                                            LEFT JOIN  COUNTRY AS CU ON CU.ID = IM.COUNTRY
                                            LEFT JOIN  SUPPLIER AS SU ON SU.SUPPLIER_ID = IM.SUPPLIER_AGENT  
                                            LEFT JOIN  DEF_PRICE_TERMS AS DT ON DT.TERM_ID = IT.TERM 
                                            LEFT JOIN  IMPORTS_HISTORY AS IH ON IH.HISTORY_ID = IT.HISTORY 
                                            LEFT JOIN  DEF_PAYMENT_MODE AS DF ON DF.PAYMENT_MODE_ID = IM.PAYMENT_MODE_ID 
                                            LEFT JOIN  DEF_CURRENCY_TYPE AS CT ON CT.CURRENCY_TYPE_ID = IM.CURRENCY_TYPE_ID                                                                                        
                                            WHERE IM.QUOTATION_ID = " + QuotationId + "";
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (DbConnection.dr = DbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<ImportCalucationDetails>(DbConnection.dr);
            }
        }

        public List<ImportQuotationItem> GetImportDetailsListForTabulationReview(int bidId,int BidItemId, DBConnection DbConnection) {
            DbConnection.cmd.Parameters.Clear();

            DbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION AS SQ \n" +
                "INNER JOIN SUPPLIER_QUOTATION_ITEM AS SQI ON SQI.QUOTATION_ID = SQ.QUOTATION_ID\n" +
                "INNER JOIN IMPORT_QUOTATION AS IQ ON IQ.QUOTATION_ID = SQ.QUOTATION_ID\n" +
                "INNER JOIN IMPORT_QUOTATION_ITEM AS IQI ON IQI.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID\n" +
                "INNER JOIN (SELECT SUPPLIER_NAME, SUPPLIER_ID FROM SUPPLIER) AS SUP ON SUP.SUPPLIER_ID = SQ.SUPPLIER_ID\n" +
                "INNER JOIN (SELECT SUPPLIER_NAME AS SUPPLIER_AGENT, SUPPLIER_ID FROM SUPPLIER) AS SA ON SA.SUPPLIER_ID = IQ.AGENT_ID\n" +
                "INNER JOIN COUNTRY AS CO ON CO.ID = IQ.COUNTRY\n" +
                "INNER JOIN CURRENCY AS CU ON CU.ID = IQ.CURRENCY_TYPE_ID\n" +
                "INNER JOIN IMPORTS_HISTORY AS IH ON IH.HISTORY_ID = IQI.HISTORY\n" +
                "INNER JOIN DEF_PRICE_TERMS AS TER ON TER.TERM_ID = IQI.TERM\n" +
                "WHERE SQ.BID_ID = " + bidId + " AND SQI.BIDDING_ITEM_ID = "+ BidItemId + "\n";
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (DbConnection.dr = DbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ImportQuotationItem>(DbConnection.dr);
            }
        }

        public List<ImportQuotationItem> GetAllImportQuotations( int QuotationId, DBConnection DbConnection) {
            DbConnection.cmd.Parameters.Clear();

            DbConnection.cmd.CommandText = "SELECT * FROM SUPPLIER_QUOTATION_ITEM AS SQI\n" +
                                            "LEFT JOIN IMPORT_QUOTATION_ITEM AS IQI ON IQI.QUOTATION_ITEM_ID = SQI.QUOTATION_ITEM_ID\n" +
                                             "LEFT JOIN  IMPORTS_HISTORY AS IH ON IH.HISTORY_ID = IQI.HISTORY\n" +
                                              "LEFT JOIN DEF_PRICE_TERMS AS TER ON TER.TERM_ID = IQI.TERM\n" +

                                             "WHERE SQI.QUOTATION_ID = " + QuotationId;
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (DbConnection.dr = DbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ImportQuotationItem>(DbConnection.dr);
            }
        }

        public int InsertImportsRates(List<Imports> importRates, DBConnection dbConnection)
        {
            try
            {
                foreach (Imports obj in importRates)
                {
                    dbConnection.cmd.Parameters.Clear();
                    if(obj.hsId != "")
                    {
                        dbConnection.cmd.CommandText = "[INSERT_IMPORTS_RATES]";
                        dbConnection.cmd.Parameters.AddWithValue("@hsCode", obj.hsId);
                        dbConnection.cmd.Parameters.AddWithValue("@hsName", obj.hsName);
                        dbConnection.cmd.Parameters.AddWithValue("@pal", obj.pal);
                        dbConnection.cmd.Parameters.AddWithValue("@vat", obj.vat);
                        dbConnection.cmd.Parameters.AddWithValue("@cess", obj.cess);
                        dbConnection.cmd.Parameters.AddWithValue("@customDuty", obj.customDuty);
                        dbConnection.cmd.Parameters.AddWithValue("@rate", obj.rate);
                        dbConnection.cmd.Parameters.AddWithValue("@effectiveDate", obj.effectiveDate);

                        dbConnection.cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        dbConnection.cmd.ExecuteNonQuery();
                    }

                }
                return 1;
            }
            catch(Exception ex)
            {
                return -99;
            }
        }

        public int UpdateRates(CurrencyRate objCurrency, DBConnection DbConnection)
        {
            DbConnection.cmd.Parameters.Clear();
            DbConnection.cmd.CommandText = @"UPDATE CURRENCY_RATE  SET SELLING_RATE = " + objCurrency.SellingRate + " , DATE='"+ LocalTime.Now+ "' WHERE CURRENCY_TYPE_ID = " + objCurrency.CurrencyTypeId + " AND  DATE =(SELECT MAX(DATE) FROM CURRENCY_RATE WHERE CURRENCY_TYPE_ID =" + objCurrency.CurrencyTypeId + ")";

            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return DbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateImportValues(List<ImportQuotationItem> ImportQuotationItemList, DBConnection DbConnection) {
            DbConnection.cmd.Parameters.Clear();

            for (int i = 0; i < ImportQuotationItemList.Count; i++) {
                DbConnection.cmd.CommandText += "UPDATE IMPORT_QUOTATION_ITEM SET EXCHANGE_RATE = "+ ImportQuotationItemList[i].ExchangeRate+ ", DUTY_PAL = "+ ImportQuotationItemList[i].DutyPal + ", XID_VALUE = " + ImportQuotationItemList[i].XIDValue + ", CID_VALUE = " + ImportQuotationItemList[i].CIDValue + ", PAL_VALUE = " + ImportQuotationItemList[i].PALValue + ", EIC_VALUE = " + ImportQuotationItemList[i].EICValue + ", VAT_VALUE = " + ImportQuotationItemList[i].VATValue + " WHERE QUOTATION_ID = "+ ImportQuotationItemList[i].QuotationId + " AND QUOTATION_ITEM_ID = " + ImportQuotationItemList[i].QuptationItemId + " ";
                DbConnection.cmd.CommandText += "UPDATE SUPPLIER_QUOTATION_ITEM SET UNIT_PRICE = " + ImportQuotationItemList[i].Sup_UnitPrice + ", SUB_TOTAL = " + ImportQuotationItemList[i].Sup_SubTotal + ", VAT_AMOUNT = " + ImportQuotationItemList[i].Sup_Vat + ", NET_TOTAL = " + ImportQuotationItemList[i].Sup_Netotal + " WHERE QUOTATION_ID = " + ImportQuotationItemList[i].QuotationId + " AND QUOTATION_ITEM_ID = " + ImportQuotationItemList[i].QuptationItemId + " ";
            }
            DbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return DbConnection.cmd.ExecuteNonQuery();
        }
    }
}
