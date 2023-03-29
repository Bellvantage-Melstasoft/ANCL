using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Infrastructure;
using CLibrary.Domain;

namespace CLibrary.Controller
{
    public interface TempBOMController
    {
        int SaveTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description);
        //int UpdateTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description);
        int GetNextPrIdObj(int DepartmentId);
        List<TempBOM> GetListById(int DepartmentId, int ItemId);
        int DeleteTempData(int DepartmentId, int ItemId);
        int DeleteTempDataByDeptId(int DepartmentId);
        int UpdateTempBOM(int departmentid, int prid, int itemid, int seqno, string meterial, string description);
        List<TempBOM> GetBOMListByPrIdItemId(int DepartmentId, int prId, int ItemId);
        int DeleteBOMByPrId(int PrId, int DepartmentId, int ItemId);
        List<TempBOM> GetItemspecification(int ItemId);

    }

    public class TempBOMControllerImpl : TempBOMController
    {
        public int SaveTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempBOMDAO tempBOMDAO = DAOFactory.CreateTempBOMDAO();
                return tempBOMDAO.SaveTempBOM(DepartmentId,Prid, ItemId, SeqNo, Meterial, Description, dbConnection);
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

        public int UpdateTempBOM(int departmentid, int prid, int itemid, int seqno, string meterial, string description)
        {
            DBConnection dbconnection = new DBConnection();
            try
            {
                TempBOMDAO tempbomdao = DAOFactory.CreateTempBOMDAO();
                return tempbomdao.UpdateTempBOM(departmentid, prid, itemid, seqno, meterial, description, dbconnection);
            }
            catch (Exception)
            {
                dbconnection.RollBack();
                throw;
            }
            finally
            {
                if (dbconnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbconnection.Commit();
                }
            }
        }
        public int GetNextPrIdObj(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempBOMDAO tempBOMDAO = DAOFactory.CreateTempBOMDAO();
                return tempBOMDAO.GetNextPrIdObj(DepartmentId, dbConnection);
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

        public List<TempBOM> GetListById(int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempBOMDAO tempBOMDAO = DAOFactory.CreateTempBOMDAO();
                return tempBOMDAO.GetListById(DepartmentId,ItemId, dbConnection);
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

        public int DeleteTempData(int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempBOMDAO tempBOMDAO = DAOFactory.CreateTempBOMDAO();
                return tempBOMDAO.DeleteTempData(DepartmentId, ItemId, dbConnection);
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

        public int DeleteTempDataByDeptId(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempBOMDAO tempBOMDAO = DAOFactory.CreateTempBOMDAO();
                return tempBOMDAO.DeleteTempDataByDeptId(DepartmentId, dbConnection);
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

        public List<TempBOM> GetBOMListByPrIdItemId(int DepartmentId, int prId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempBOMDAO tempBOMDAO = DAOFactory.CreateTempBOMDAO();
                return tempBOMDAO.GetBOMListByPrIdItemId(DepartmentId, prId, ItemId, dbConnection);
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

        public List<TempBOM> GetItemspecification( int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempBOMDAO tempBOMDAO = DAOFactory.CreateTempBOMDAO();
                return tempBOMDAO.GetItemspecification(ItemId, dbConnection);
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

        public int DeleteBOMByPrId(int PrId, int DepartmentId, int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TempBOMDAO tempBOMDAO = DAOFactory.CreateTempBOMDAO();
                return tempBOMDAO.DeleteBOMByPrId(PrId, DepartmentId, ItemId, dbConnection);
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
