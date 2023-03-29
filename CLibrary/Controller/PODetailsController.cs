using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface PODetailsController
    {
       int SavePoDetails(int PoId, int quotationId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount, decimal customizeAmount, decimal customizedVat, decimal customizedNbt, int isCustomizedAmount, decimal customizedTotalAmount);
        List<PODetails> GetPoDetailsByPoId(int PoId);
        PODetails GetPoDetailsObjByPoIdandItemId(int PoId, int itemid);
        List<PODetails> GetSumOfAll(int PoId);
        int UpdateIsRaisedPO(int PoId, int itemId, int isPORaised);
        int UpdateIsRaisedPOAndPOApprovode(int PoId, int itemId, int IsPoApprvoed);
        PODetails GetPoDetailsListByIsApprovedPO(int PoId);
        int UpdatePOApprovedNextLevel(int PoId, int newPOid, int itemId, int IsPoApprvoed, int BasedPO);
        List<PODetails> GetAllListFromPoId(int PoId);
	List<PODetails> TerminatedPO(int PodId);

       //-----------GRn Modifications
        List<PODetails> GetPODetailsApproved(int departmentId);
        int RejectPoDetails(int poId);
        List<PODetails> GetPOdetailsListBypoid(int PoId, int CompanyId);

        int UpdatePOEditMode(int PoId, int ItemId);

        List<PODetails> GetPoDetailsByPoIdPoRaised(int PoId);
        int ApprovePoDetails(int poId);

        POHistory GetPoHistoryByItemId(int ItemId);
        List<PODetails> GetPUrchasedItems(int ItemId, int CompanyId);

        int TerminatePoDetail(int PoId, List<int> PoDetailIds, int TerminatedBy, string Remarks);
        List<PODetails> GetPODetailsToViewPo(int PoId, int CompanyId);
        List<PODetails> GetPoDetails(int PoId);

    }
    public class PODetailsControllerImpl : PODetailsController
    {
        public List<PODetails> GetPoDetailsByPoId(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetPoDetailsByPoId( PoId, dbConnection);
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
       
            public List<PODetails> GetPoDetails(int PoId) {
                DBConnection dbConnection = new DBConnection();
                try {
                    PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                    return pODetailsDAO.GetPoDetails(PoId, dbConnection);
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

            public List<PODetails> GetPoDetailsByPoIdPoRaised(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetPoDetailsByPoIdPoRaised(PoId, dbConnection);
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

        public List<PODetails> GetPODetailsToViewPo(int PoId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetPODetailsToViewPo(PoId, CompanyId, dbConnection);
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

        public PODetails GetPoDetailsObjByPoIdandItemId(int PoId, int itemid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetails pODetails = new PODetails();
                AddItemDAO addItemDAO = DAOFactory.CreateAddItemDAO();
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                pODetails= pODetailsDAO.GetPoDetailsObjByPoIdandItemId( PoId,  itemid, dbConnection);
                pODetails._AddItem = addItemDAO.FetchItemListByIdObj(itemid, dbConnection);
                return pODetails;
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

        public POHistory GetPoHistoryByItemId(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                POHistory pOHistory = pODetailsDAO.GetPoHistoryByItemId(ItemId, dbConnection);
                return pOHistory;
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

        public int SavePoDetails(int PoId, int quotationId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount, decimal customizeAmount, decimal customizedVat, decimal customizedNbt, int isCustomizedAmount, decimal customizedTotalAmount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.SavePoDetails( PoId,  quotationId,  itemId,  itemPrce,  quntity,  totalAmount,  vatAmount,  nbtAmount, customizeAmount, customizedVat, customizedNbt,isCustomizedAmount,customizedTotalAmount, dbConnection);
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

        public List<PODetails> GetSumOfAll(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetSumOfAll(PoId, dbConnection);
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

        public int UpdateIsRaisedPO(int PoId, int itemId, int isPORaised)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.UpdateIsRaisedPO(PoId, itemId,isPORaised, dbConnection);
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

        public int UpdateIsRaisedPOAndPOApprovode(int PoId, int itemId, int IsPoApprvoed)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.UpdateIsRaisedPOAndPOApprovode(PoId, itemId, IsPoApprvoed, dbConnection);
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

        public PODetails GetPoDetailsListByIsApprovedPO(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetPoDetailsListByIsApprovedPO(PoId, dbConnection);
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

        public int UpdatePOApprovedNextLevel(int PoId, int newPOid, int itemId, int IsPoApprvoed, int BasedPO)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.UpdatePOApprovedNextLevel( PoId, newPOid, itemId, IsPoApprvoed,  BasedPO, dbConnection);
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

        public List<PODetails> GetAllListFromPoId(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetAllListFromPoId(PoId, dbConnection);
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

        public List<PODetails> GetPODetailsApproved(int departmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetPODetailsApproved(departmentId,dbConnection);
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

        public int RejectPoDetails(int poId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.RejectPoDetails( poId, dbConnection);
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

        public int UpdatePOEditMode(int PoId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.UpdatePOEditMode(PoId, ItemId, dbConnection);
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

        public int ApprovePoDetails(int poId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.ApprovePoDetails(poId, dbConnection);
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

        public List<PODetails> GetPUrchasedItems(int ItemId, int CompanyId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetPUrchasedItems(ItemId, CompanyId, dbConnection);
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

        public int TerminatePoDetail(int PoId, List<int> PoDetailIds, int TerminatedBy, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.TerminatePoDetail(PoId, PoDetailIds, TerminatedBy, Remarks, dbConnection);
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

        public List<PODetails> TerminatedPO(int PodId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.TerminatedPO(PodId, dbConnection);
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

        public List<PODetails> GetPOdetailsListBypoid(int PoId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                PODetailsDAO pODetailsDAO = DAOFactory.createPODetailsDAO();
                return pODetailsDAO.GetPOdetailsListBypoid(PoId, CompanyId, dbConnection);
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
