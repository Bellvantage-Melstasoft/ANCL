using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface InvoiceDetailsDAO {
        int SaveInvoiceDetailsInPO(int PoId, int GrnId, int PaymentType, string invoiceNo, DateTime date, decimal invoiceAmount, string VatNo, int isPaymentSettled, string remark, DateTime remarkOn, List<InvoiceImages> invoiceImages, DBConnection dbConnection);
        List<InvoiceDetails> GetPreviousInvoices(int Poid, DBConnection dbConnection);
        List<InvoiceDetails> GetInvoicesByGRNCode(int CompanyId, string GRNCode, DBConnection dbConnection);
        List<InvoiceDetails> GetInvoicesByPoCode(int CompanyId, string POCode, DBConnection dbConnection);
        int DeleteInvoice(int InvoiceId, int Userid, DBConnection dbConnection);
        InvoiceDetails GetInvoicesByInvId(int InvId, DBConnection dbConnection);
        int Update(int InvoiceId, int PaymentType, string InvNo, DateTime InvoiceDate, decimal InvoiceAmount, string VATNo, int PayemtSettled, string remarks, DateTime RemarksOn, int UserId, DBConnection dbConnection);
    }
    public class InvoiceDetailsDAOImpl : InvoiceDetailsDAO {

        public int SaveInvoiceDetailsInPO(int PoId, int GrnId, int PaymentType, string invoiceNo, DateTime date, decimal invoiceAmount, string VatNo, int isPaymentSettled, string remark, DateTime remarkOn, List<InvoiceImages> invoiceImages, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            if (date == DateTime.MinValue) {
                dbConnection.cmd.CommandText = "DECLARE @INVOICE_TABLE TABLE (ID INT) "+
                                            "INSERT INTO INVOICE_DETAILS(PO_ID, GRN_ID,PAYMENT_TYPE, INVOICE_NO, INVOICE_AMOUNT, VAT_NO, IS_PAYMENT_SETTLED, IS_ACTIVE, REMARKS, REMARK_ON) " +
                                            "OUTPUT INSERTED.[INVOICE_ID] INTO @INVOICE_TABLE(ID) VALUES (" + PoId + "," + GrnId + "," + PaymentType + ",'" + invoiceNo + "'," + invoiceAmount + ",'" + VatNo + "', " + isPaymentSettled + ", 1, '"+ remark + "', '" + remarkOn + "' ) ";
            }
            else {
                dbConnection.cmd.CommandText = "DECLARE @INVOICE_TABLE TABLE (ID INT) " +
                                            "INSERT INTO INVOICE_DETAILS(PO_ID, GRN_ID,PAYMENT_TYPE, INVOICE_NO, INVOICE_DATE, INVOICE_AMOUNT, VAT_NO, IS_PAYMENT_SETTLED, IS_ACTIVE, REMARKS, REMARK_ON) " +
                                            "OUTPUT INSERTED.[INVOICE_ID] INTO @INVOICE_TABLE(ID) VALUES (" + PoId + "," + GrnId + "," + PaymentType + ",'" + invoiceNo + "','" + date + "'," + invoiceAmount + ",'" + VatNo + "', " + isPaymentSettled + ", 1, '" + remark + "', '" + remarkOn + "' ) ";
            }

            if (invoiceImages.Count > 0) {
                for (int i = 0; i< invoiceImages.Count; i++) {
                    dbConnection.cmd.CommandText += "INSERT INTO [INVOICE_IMAGES] ([INVOICE_ID],[IMAGE_PATH]) VALUES ((SELECT MAX(ID) FROM @INVOICE_TABLE),'" + invoiceImages[i].ImagePath.ProcessString() + "'); ";
                }
                }
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<InvoiceDetails> GetPreviousInvoices(int Poid, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM iNVOICE_DETAILS AS ID WHERE PO_ID = "+ Poid + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<InvoiceDetails>(dbConnection.dr);
            }
        }

        public List<InvoiceDetails> GetInvoicesByGRNCode(int CompanyId, string GRNCode, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM INVOICE_DETAILS AS ID " +
                "LEFT JOIN (SELECT PO_ID, PO_CODE, IS_CANCELLED FROM PO_MASTER) AS PM ON PM.PO_ID = ID.PO_ID " +
                "LEFT JOIN (SELECT PO_ID, GRN_ID FROM PO_GRN) AS PG ON PG.PO_ID = PM.PO_ID " +
                "LEFT JOIN (SELECT GRN_CODE, GRN_ID FROM GRN_MASTER) AS GM ON GM.GRN_ID = PG.GRN_ID  " +
                "WHERE GM.GRN_CODE = '" + GRNCode + "'  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<InvoiceDetails>(dbConnection.dr);
            }
        }

        public List<InvoiceDetails> GetInvoicesByPoCode(int CompanyId, string POCode, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM INVOICE_DETAILS AS ID " +
                "LEFT JOIN (SELECT PO_ID, PO_CODE, IS_CANCELLED FROM PO_MASTER) AS PM ON PM.PO_ID = ID.PO_ID " +
                "LEFT JOIN (SELECT PO_ID, GRN_ID FROM PO_GRN) AS PG ON PG.PO_ID = PM.PO_ID " +
                "LEFT JOIN (SELECT GRN_CODE, GRN_ID FROM GRN_MASTER) AS GM ON GM.GRN_ID = PG.GRN_ID " +
                "WHERE PM.PO_CODE = '"+ POCode + "' ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<InvoiceDetails>(dbConnection.dr);
            }
        }

        public InvoiceDetails GetInvoicesByInvId(int InvId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM INVOICE_DETAILS AS ID WHERE INVOICE_ID = " + InvId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<InvoiceDetails>(dbConnection.dr);
            }
        }

        public int DeleteInvoice(int InvoiceId, int Userid, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE INVOICE_DETAILS SET IS_ACTIVE = 0 , TERMINATED_BY = "+Userid+", TERMINATED_ON = '"+LocalTime.Now+"' WHERE INVOICE_ID = "+ InvoiceId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int Update(int InvoiceId, int PaymentType, string InvNo, DateTime InvoiceDate, decimal InvoiceAmount, string VATNo, int PayemtSettled, string remarks, DateTime RemarksOn, int UserId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();


            if (InvoiceDate == DateTime.MinValue) {
                dbConnection.cmd.CommandText = "UPDATE INVOICE_DETAILS SET PAYMENT_TYPE = " + PaymentType + ", INVOICE_NO = '" + InvNo + "',  INVOICE_AMOUNT = " + InvoiceAmount + ", VAT_NO = '" + VATNo + "', IS_PAYMENT_SETTLED = " + PayemtSettled + ", " +
                                           "REMARKS = '" + remarks + "', REMARK_ON = '" + RemarksOn + "',UPDATED_BY = " + UserId + ", UPDATED_ON = '" + LocalTime.Now + "' WHERE INVOICE_ID = " + InvoiceId + " ";
            }
            else {
                dbConnection.cmd.CommandText = "UPDATE INVOICE_DETAILS SET PAYMENT_TYPE = " + PaymentType + ", INVOICE_NO = '" + InvNo + "', INVOICE_DATE = '" + InvoiceDate + "', INVOICE_AMOUNT = " + InvoiceAmount + ", VAT_NO = '" + VATNo + "', IS_PAYMENT_SETTLED = " + PayemtSettled + ", " +
                                            "REMARKS = '" + remarks + "', REMARK_ON = '" + RemarksOn + "',UPDATED_BY = " + UserId + ", UPDATED_ON = '" + LocalTime.Now + "' WHERE INVOICE_ID = " + InvoiceId + " ";
            }


             dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    }
