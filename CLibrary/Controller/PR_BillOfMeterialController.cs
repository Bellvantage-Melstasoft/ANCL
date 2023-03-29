using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface PR_BillOfMeterialController
    {
        int SaveBillOfMeterial(int PrId, int ItemId, int SeqNo, string Meterial, string Description, int IsActive, DateTime CreatedDatetime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy);
        List<PR_BillOfMeterial> GetList(int PrId, int ItemId);
        List<PR_BillOfMeterial> GetListWithSupplierBOM(int PrId, int ItemId);
        List<PR_BillOfMeterial> GetListRejected(int PrId);
        int DeletePRBom(int PrId, int ItemId);
        int DeletePrBoMTrash(int PrId, int ItemId);
        List<PR_BillOfMeterial> GetListForPrint(List<int> PrdId);
    }

    public class PR_BillOfMeterialControllerImpl : PR_BillOfMeterialController
    {
        public int SaveBillOfMeterial(int PrId, int ItemId, int SeqNo, string Meterial, string Description, int IsActive, DateTime CreatedDatetime, string CreatedBy, DateTime UpdatedDateTime, string UpdatedBy)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_BillOfMeterialDAO pr_BillOfMeterialDAO = DAOFactory.CreatePR_BillOfMeterialDAO();
                return pr_BillOfMeterialDAO.SaveBillOfMeterial(PrId, ItemId, SeqNo, Meterial, Description, IsActive, CreatedDatetime, CreatedBy, UpdatedDateTime, UpdatedBy, dbConnection);
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

        public List<PR_BillOfMeterial> GetList(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_BillOfMeterialDAO pr_BillOfMeterialDAO = DAOFactory.CreatePR_BillOfMeterialDAO();
                return pr_BillOfMeterialDAO.GetList(PrId,ItemId, dbConnection);
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

        public List<PR_BillOfMeterial> GetListWithSupplierBOM(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_BillOfMeterialDAO pr_BillOfMeterialDAO = DAOFactory.CreatePR_BillOfMeterialDAO();
                return pr_BillOfMeterialDAO.GetListWithSupplierBOM(PrId, ItemId, dbConnection);
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

        public List<PR_BillOfMeterial> GetListRejected(int PrId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_BillOfMeterialDAO pr_BillOfMeterialDAO = DAOFactory.CreatePR_BillOfMeterialDAO();
                return pr_BillOfMeterialDAO.GetListRejected(PrId, dbConnection);
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

        public int DeletePRBom(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_BillOfMeterialDAO pr_BillOfMeterialDAO = DAOFactory.CreatePR_BillOfMeterialDAO();
                return pr_BillOfMeterialDAO.DeletePRBom(PrId, ItemId, dbConnection);
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

        public int DeletePrBoMTrash(int PrId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PR_BillOfMeterialDAO pr_BillOfMeterialDAO = DAOFactory.CreatePR_BillOfMeterialDAO();
                return pr_BillOfMeterialDAO.DeletePrBoMTrash( PrId,  ItemId, dbConnection);
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

        public List<PR_BillOfMeterial> GetListForPrint(List<int> PrdId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PR_BillOfMeterialDAO pr_BillOfMeterialDAO = DAOFactory.CreatePR_BillOfMeterialDAO();
                return pr_BillOfMeterialDAO.GetListForPrint(PrdId, dbConnection);
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

