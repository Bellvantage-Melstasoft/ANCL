using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface NaturseOfBusinessController
    {
        int SaveBusinessCategory(string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int UpdateBusinessCategory(int BusinessCategoryId, string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive);
        int DeleteBusinessCategory(int BusinessCategoryId);
        List<NaturseOfBusiness> FetchBusinessCategoryList();
    }
    public class NaturseOfBusinessControllerImpl : NaturseOfBusinessController
    {
        public int DeleteBusinessCategory(int BusinessCategoryId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                NaturseOfBusinessDAO naturseOfBusinessDAO = DAOFactory.CreateNaturseOfBusinessDAO();
                return naturseOfBusinessDAO.DeleteBusinessCategory( BusinessCategoryId, dbConnection);
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

        public List<NaturseOfBusiness> FetchBusinessCategoryList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                NaturseOfBusinessDAO naturseOfBusinessDAO = DAOFactory.CreateNaturseOfBusinessDAO();
                return naturseOfBusinessDAO.FetchBusinessCategoryList( dbConnection);
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

        public int SaveBusinessCategory(string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                NaturseOfBusinessDAO naturseOfBusinessDAO = DAOFactory.CreateNaturseOfBusinessDAO();
                return naturseOfBusinessDAO.SaveBusinessCategory(BusinessCategoryName, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, IsActive, dbConnection);
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

        public int UpdateBusinessCategory(int BusinessCategoryId, string BusinessCategoryName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                NaturseOfBusinessDAO naturseOfBusinessDAO = DAOFactory.CreateNaturseOfBusinessDAO();
                return naturseOfBusinessDAO.UpdateBusinessCategory( BusinessCategoryId,  BusinessCategoryName,  CreatedDate,  CreatedBy,  UpdatedDate,  UpdatedBy,  IsActive, dbConnection);
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
