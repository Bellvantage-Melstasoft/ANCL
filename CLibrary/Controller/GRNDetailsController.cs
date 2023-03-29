using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface GRNDetailsController
    {
        int SaveGrnDetails(int GrnId, int PoId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount);
        List<GrnDetails> GetGrnDetailsByPoId(int GrnId, int PoId);
        GrnDetails GetGrnDetailsObjByPoIdandItemId(int GrnId, int PoId, int itemid);
        List<GrnDetails> GetGrnDetails(int PoId);
        List<GrnDetails> GetGrnDetailsAll(int DepartmentId);
        List<GrnDetails> GrnDetialsGrnApproved(int GrnId, int PoId, int DepartmentId);
        int UpdateApprovedGrn(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason);
        List<GrnDetails> GrnDetialsGrnApprovedOnly(int GrnId, int PoId, int DepartmentId);
        int UpdateApprovedGrnNewGrnId(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason, int NewGrnId);
        int UpdateGrnReectIsPoRaised(int GrnId, int PoId, int ItemId, int isGrnRaised);
        int UpdateGrnReectAddToGrnCount(int GrnId, int PoId, int ItemId,int count);
        int updateGrndIssuedQty(int grndID, int issuedQty);
    }

    public class GRNDetailsControllerImpl : GRNDetailsController
    {
        public List<GrnDetails> GetGrnDetailsByPoId(int GrnId, int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.GetGrnDetailsByPoId( GrnId,  PoId, dbConnection);
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

        public GrnDetails GetGrnDetailsObjByPoIdandItemId(int GrnId, int PoId, int itemid)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.GetGrnDetailsObjByPoIdandItemId( GrnId,  PoId,  itemid, dbConnection);
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

        public int SaveGrnDetails(int GrnId, int PoId, int itemId, decimal itemPrce, decimal quntity, decimal totalAmount, decimal vatAmount, decimal nbtAmount)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.SaveGrnDetails( GrnId,  PoId,  itemId,  itemPrce,  quntity,  totalAmount,  vatAmount,  nbtAmount, dbConnection);
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

        public List<GrnDetails> GetGrnDetails(int PoId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.GetGrnDetails( PoId, dbConnection);
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

        public List<GrnDetails> GetGrnDetailsAll(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.GetGrnDetailsAll(DepartmentId ,dbConnection);
            }
            catch (Exception ex)
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

        public List<GrnDetails> GrnDetialsGrnApproved(int GrnId, int PoId, int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.GrnDetialsGrnApproved(GrnId, PoId ,DepartmentId, dbConnection);
            }
            catch (Exception ex)
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

        public int UpdateApprovedGrn(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.UpdateApprovedGrn(GrnId, PoId, ItemId, IsApproveStatus, ApprovedBy, ApprovedDatetime, RejectedReason, dbConnection);
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

        public List<GrnDetails> GrnDetialsGrnApprovedOnly(int GrnId, int PoId, int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.GrnDetialsGrnApprovedOnly(GrnId, PoId, DepartmentId, dbConnection);
            }
            catch (Exception ex)
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

        public int UpdateApprovedGrnNewGrnId(int GrnId, int PoId, int ItemId, int IsApproveStatus, int ApprovedBy, DateTime ApprovedDatetime, string RejectedReason, int NewGrnId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.UpdateApprovedGrnNewGrnId(GrnId, PoId, ItemId, IsApproveStatus, ApprovedBy, ApprovedDatetime, RejectedReason,NewGrnId, dbConnection);
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

        public int UpdateGrnReectIsPoRaised(int GrnId, int PoId, int ItemId, int isGrnRaised)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.UpdateGrnReectIsPoRaised(GrnId, PoId, ItemId, isGrnRaised, dbConnection);
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

        public int UpdateGrnReectAddToGrnCount(int GrnId, int PoId, int ItemId,int count)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.UpdateGrnReectAddToGrnCount(GrnId, PoId, ItemId,count, dbConnection);
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

        public int updateGrndIssuedQty(int grndID, int issuedQty)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GRNDetailsDAO gRNDetailsDAO = DAOFactory.createGRNDetailsDAO();
                return gRNDetailsDAO.updateGrndIssuedQty(grndID, issuedQty, dbConnection);
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
