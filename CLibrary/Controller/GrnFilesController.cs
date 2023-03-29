using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;

namespace CLibrary.Controller
{
    public interface GrnFilesController
    {
        List<GrnFiles> GetGrnFilesByGrnId(int GrnId);
    }

    public class GrnFilesControllerImpl : GrnFilesController
    {


        public List<GrnFiles> GetGrnFilesByGrnId(int GrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GrnFilesDAO supplierRatingDAO = DAOFactory.CreateGrnFilesDAO();
                return supplierRatingDAO.GetGrnFilesByGrnId(GrnId, dbConnection);
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