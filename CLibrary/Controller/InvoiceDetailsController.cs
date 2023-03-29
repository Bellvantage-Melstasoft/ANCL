using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface InvoiceDetailsController {
        int SaveInvoiceDetailsInPO(int PoId, int GrnId, int PaymentType, string invoiceNo, DateTime date, decimal invoiceAmount, string VatNo, int isPaymentSettled, string remark, DateTime remarkOn, List<InvoiceImages> invoiceImages);
        List<InvoiceDetails> GetPreviousInvoices(int PoId);
        List<InvoiceDetails> GetInvoicesByGRNCode(int Companyid, string GRNCode);
        List<InvoiceDetails> GetInvoicesByPoCode(int CompanyId, string POCode);
        int DeleteInvoice(int invoiceId, int Userid);
        InvoiceDetails GetInvoicesByInvId(int InvId);
        int Update(int InvoiceId, int PaymentType, string InvNo, DateTime InvoiceDate, decimal InvoiceAmount, string VATNo, int PayemtSettled, string remarks, DateTime RemarksOn, int UserId);
    }

    public class InvoiceDetailsControllerImpl : InvoiceDetailsController {

        public int SaveInvoiceDetailsInPO(int PoId, int GrnId, int PaymentType, string invoiceNo, DateTime date, decimal invoiceAmount, string VatNo, int isPaymentSettled, string remark, DateTime remarkOn, List<InvoiceImages> invoiceImages) {
            DBConnection dbConnection = new DBConnection();
            try {
                InvoiceDetailsDAO invoiceDetails = DAOFactory.CreateInvoiceDetailsDAO();
                return invoiceDetails.SaveInvoiceDetailsInPO( PoId,  GrnId,  PaymentType,  invoiceNo,  date,  invoiceAmount,  VatNo,  isPaymentSettled, remark,  remarkOn, invoiceImages, dbConnection);
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

        public List<InvoiceDetails> GetPreviousInvoices(int PoId) {
            DBConnection dbConnection = new DBConnection();
            try {
                InvoiceDetailsDAO invoiceDetails = DAOFactory.CreateInvoiceDetailsDAO();
                return invoiceDetails.GetPreviousInvoices(PoId, dbConnection);
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

        public List<InvoiceDetails> GetInvoicesByGRNCode(int CompanyId, string GRNCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                InvoiceDetailsDAO invoiceDetails = DAOFactory.CreateInvoiceDetailsDAO();
                return invoiceDetails.GetInvoicesByGRNCode(CompanyId, GRNCode, dbConnection);
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

        public List<InvoiceDetails> GetInvoicesByPoCode(int CompanyId, string POCode) {
            DBConnection dbConnection = new DBConnection();
            try {
                InvoiceDetailsDAO invoiceDetails = DAOFactory.CreateInvoiceDetailsDAO();
                return invoiceDetails.GetInvoicesByPoCode(CompanyId, POCode, dbConnection);
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

        public InvoiceDetails GetInvoicesByInvId(int InvId) {
            DBConnection dbConnection = new DBConnection();
            try {
                InvoiceDetailsDAO invoiceDetails = DAOFactory.CreateInvoiceDetailsDAO();
                return invoiceDetails.GetInvoicesByInvId(InvId, dbConnection);
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

        public int DeleteInvoice(int invoiceId, int Userid) {
            DBConnection dbConnection = new DBConnection();
            try {
                InvoiceDetailsDAO invoiceDetails = DAOFactory.CreateInvoiceDetailsDAO();
                return invoiceDetails.DeleteInvoice(invoiceId, Userid, dbConnection);
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

        public int Update(int InvoiceId, int PaymentType, string InvNo, DateTime InvoiceDate, decimal InvoiceAmount, string VATNo, int PayemtSettled, string remarks, DateTime RemarksOn, int UserId) {
            DBConnection dbConnection = new DBConnection();
            try {
                InvoiceDetailsDAO invoiceDetails = DAOFactory.CreateInvoiceDetailsDAO();
                return invoiceDetails.Update(InvoiceId, PaymentType, InvNo, InvoiceDate,  InvoiceAmount, VATNo, PayemtSettled, remarks, RemarksOn, UserId, dbConnection);
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
    }
    }
