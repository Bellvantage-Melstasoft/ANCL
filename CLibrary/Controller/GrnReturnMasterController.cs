using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CLibrary.Controller {
    public interface GrnReturnMasterController {
        List<GrnMaster> GetGrnForReturn(int departmentid);
        List<GrnReturnMaster> GetReturnedGRN();
        GrnReturnMaster GetReturnedGrnDetails(int GrnReturnid);
    }

    public class GrnReturnMasterControllerImpl : GrnReturnMasterController {
        public List<GrnMaster> GetGrnForReturn(int departmentid) {
            DBConnection dbConnection = new DBConnection();
            try {
                GrnDAO grnDAO = DAOFactory.createGrnDAO();
                return grnDAO.GetGrnForReturn(departmentid, dbConnection);
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

        public List<GrnReturnMaster> GetReturnedGRN() {
            DBConnection dbConnection = new DBConnection();
            try {
                GrnReturnMasterDAO grnDAO = DAOFactory.CreateGrnReturnMasterDAO();
                return grnDAO.GetReturnedGRN(dbConnection);
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

        public GrnReturnMaster GetReturnedGrnDetails(int GrnReturnid) {
            DBConnection dbConnection = new DBConnection();
            try {
                GrnReturnMasterDAO grnDAO = DAOFactory.CreateGrnReturnMasterDAO();
                return grnDAO.GetReturnedGrnDetails(GrnReturnid,dbConnection);
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
