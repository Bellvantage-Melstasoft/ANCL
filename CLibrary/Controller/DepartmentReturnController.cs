using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface DepartmentReturnController {
        decimal SumReturedQty(int MrndInID);
        List<DepartmetReturn> fetchReturnedStock();
        List<DepartmetReturn> FetchApprovedStock();
    }
    public class DepartmentReturnControllerImpl : DepartmentReturnController {

        public decimal SumReturedQty(int MrndInID) {
            DBConnection dbConnection = new DBConnection();
            try {
                DepartmentReturnDAO DAO = DAOFactory.CreateDepartmentReturnDAO();
                return DAO.SumReturedQty(MrndInID, dbConnection);

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


        public List<DepartmetReturn> fetchReturnedStock() {
            DBConnection dbConnection = new DBConnection();
            try {
                DepartmentReturnDAO DAO = DAOFactory.CreateDepartmentReturnDAO();
                return DAO.fetchReturnedStock(dbConnection);

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
        public List<DepartmetReturn> FetchApprovedStock() {
            DBConnection dbConnection = new DBConnection();
            try {
                DepartmentReturnDAO DAO = DAOFactory.CreateDepartmentReturnDAO();
                return DAO.FetchApprovedStock(dbConnection);

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
