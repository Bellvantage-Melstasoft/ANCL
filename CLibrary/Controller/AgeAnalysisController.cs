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
    public interface AgeAnalysisController
    {
        List<AgeAnalysis> GetAgeAnalysis();
    }
    public class AgeAnalysisControllerImpl : AgeAnalysisController
    {

        public List<AgeAnalysis> GetAgeAnalysis()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                AgeAnalysisDAO ageAnalysisDAO = DAOFactory.CreateAgeAnalysisDAO();
                return ageAnalysisDAO.GetAgeAnalysis(dbConnection);

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
