using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface FollowUpRemarksController {
        int SaveRemark(int PoId, int UserId, string Remrk);
        List<FollowUpRemark> GetRemarks(int PoId, int CompanyId);
        int DeleteRemark(int RemarkId);

        }

        public class FollowUpRemarksControllerImpl : FollowUpRemarksController {

        public int SaveRemark(int PoId, int UserId, string Remrk) {
            DBConnection dbConnection = new DBConnection();
            try {
                FollowUpRemarksDAO followUpRemarksDAO = DAOFactory.CreateFollowUpRemarksDAO();
                return followUpRemarksDAO.SaveRemark(PoId, UserId, Remrk,  dbConnection);
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

        public List<FollowUpRemark> GetRemarks(int PoId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                FollowUpRemarksDAO followUpRemarksDAO = DAOFactory.CreateFollowUpRemarksDAO();
                return followUpRemarksDAO.GetRemarks(PoId, CompanyId, dbConnection);
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

        public int DeleteRemark(int RemarkId) {
            DBConnection dbConnection = new DBConnection();
            try {
                FollowUpRemarksDAO followUpRemarksDAO = DAOFactory.CreateFollowUpRemarksDAO();
                return followUpRemarksDAO.DeleteRemark(RemarkId, dbConnection);
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
