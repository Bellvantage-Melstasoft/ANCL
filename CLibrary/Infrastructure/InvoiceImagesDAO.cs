using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface InvoiceImagesDAO {
        List<InvoiceImages> GetInvoiceImages(int InvoiceId, DBConnection dbConnection);
    }

    public class InvoiceImagesDAOSQLImpl : InvoiceImagesDAO {
        public List<InvoiceImages> GetInvoiceImages(int InvoiceId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM INVOICE_IMAGES AS ID WHERE INVOICE_ID = " + InvoiceId + "  ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<InvoiceImages>(dbConnection.dr);
            }
        }
    }
    }
