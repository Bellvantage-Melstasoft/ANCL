using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLibrary.Controller
{
    public interface AddItemPOReportsController
    {
        List<AddItemPOReports> GetItemPoReports();

    }
    public class AddItemPOReportsControllerImpl : AddItemPOReportsController
    {
        public List<AddItemPOReports> GetItemPoReports()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AddItemPOReportsDAO addItemPOReportsDAO = DAOFactory.CreateAddItemPOReportsDAO();
                return addItemPOReportsDAO.GetItemPoReports(dbConnection);


            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }
    }
}
