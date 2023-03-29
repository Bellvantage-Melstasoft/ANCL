using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface CompanyTypeController {
        List<CompanyType> FetchCompanyTypeList();
    }

    public class CompanyTypeControllerImpl : CompanyTypeController {

        public List<CompanyType> FetchCompanyTypeList() {
            DBConnection dbConnection = new DBConnection();
            try {
                CompanyTypeDAO companyTypeDAO = DAOFactory.CreateCompanyTypeDAO();
                return companyTypeDAO.FetchCompanyTypeList(dbConnection);
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
    }
    }
