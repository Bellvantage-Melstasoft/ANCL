using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using CLibrary.Common;

namespace CLibrary.Controller
{
    public interface GeneralSettingsController
    {
        int SaveGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems);
        int UpdateGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems);
        int DeleteGeneralSettings(int DepartmentId);
        List<GeneralSetting> FetchGeneralSettingsList();
        List<GeneralSetting> FetchGeneralSettingsListById(int DepartmentId);
        GeneralSetting FetchGeneralSettingsListByIdObj(int DepartmentId);
       VAT_NBT getLatestVatNbt();
       int InsertVatNBT(decimal vat, decimal nbt1, decimal nbt2);

    }

    public class GeneralSettingsControllerImpl : GeneralSettingsController
    {
        public int SaveGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GeneralSettingsDAO generalSettingsDAO = DAOFactory.CreateGeneralSettingsDAO();
                return generalSettingsDAO.SaveGeneralSettings(DepartmentId, BidOpeningPeriod, CanOverride, BidOnlyRegisteredSupplier, ViewBidsOnlineUponPrCreation,ManualBidAllowsOnlySelectedItems, dbConnection);
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
        public int UpdateGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GeneralSettingsDAO generalSettingsDAO = DAOFactory.CreateGeneralSettingsDAO();
                return generalSettingsDAO.UpdateGeneralSettings(DepartmentId, BidOpeningPeriod, CanOverride, BidOnlyRegisteredSupplier, ViewBidsOnlineUponPrCreation,ManualBidAllowsOnlySelectedItems, dbConnection);
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
        public int DeleteGeneralSettings(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GeneralSettingsDAO generalSettingsDAO = DAOFactory.CreateGeneralSettingsDAO();
                return generalSettingsDAO.DeleteGeneralSettings(DepartmentId, dbConnection);
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
        public List<GeneralSetting> FetchGeneralSettingsList()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GeneralSettingsDAO generalSettingsDAO = DAOFactory.CreateGeneralSettingsDAO();
                return generalSettingsDAO.FetchGeneralSettingsList(dbConnection);
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
        public List<GeneralSetting> FetchGeneralSettingsListById(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GeneralSettingsDAO generalSettingsDAO = DAOFactory.CreateGeneralSettingsDAO();
                return generalSettingsDAO.FetchGeneralSettingsListById(DepartmentId ,dbConnection);
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
        public GeneralSetting FetchGeneralSettingsListByIdObj(int DepartmentId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GeneralSettingsDAO generalSettingsDAO = DAOFactory.CreateGeneralSettingsDAO();
                return generalSettingsDAO.FetchGeneralSettingsListByIdObj(DepartmentId, dbConnection);
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

        public VAT_NBT getLatestVatNbt()
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                GeneralSettingsDAO generalSettingsDAO = DAOFactory.CreateGeneralSettingsDAO();
                return generalSettingsDAO.getLatestVatNbt(dbConnection);
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
        public int InsertVatNBT(decimal vat, decimal nbt1, decimal nbt2) {
            DBConnection dbConnection = new DBConnection();
            try {
                GeneralSettingsDAO generalSettingsDAO = DAOFactory.CreateGeneralSettingsDAO();
                return generalSettingsDAO.InsertVatNBT(vat, nbt1,nbt2,dbConnection);
            }
            catch (Exception ex) {
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
