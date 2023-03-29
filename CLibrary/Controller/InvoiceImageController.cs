using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface InvoiceImageController {
        List<InvoiceImages> GetInvoiceImages(int InvoiceId);
    }
    public class InvoiceImageControllerImpl : InvoiceImageController {

        public List<InvoiceImages> GetInvoiceImages(int InvoiceId) {

            DBConnection dbConnection = new DBConnection();
            try {
                InvoiceImagesDAO invoiceImagesDAO = DAOFactory.CreateInvoiceImagesDAO();
                return invoiceImagesDAO.GetInvoiceImages(InvoiceId, dbConnection);
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

    }
}
