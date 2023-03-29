using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface HSController
    {
        List<HScode> getAllHScodes();
    }
    public class HSControllerImpl : HSController
    {

        public List<HScode> getAllHScodes()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                HScodeDAO hscodeDAO = DAOFactory.CreateHScodeDAO();
                return hscodeDAO.getAllHScodes(dbConnection);
            }
            catch (Exception ex)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    
    }
}
