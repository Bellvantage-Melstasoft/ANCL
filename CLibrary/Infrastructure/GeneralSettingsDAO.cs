using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface GeneralSettingsDAO
    {
        int SaveGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation,int ManualBidAllowsOnlySelectedItems, DBConnection dbConnection);
        int UpdateGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems, DBConnection dbConnection);
        int DeleteGeneralSettings(int DepartmentId, DBConnection dbConnection);
        List<GeneralSetting> FetchGeneralSettingsList(DBConnection dbConnection);
        List<GeneralSetting> FetchGeneralSettingsListById(int DepartmentId, DBConnection dbConnection);
        GeneralSetting FetchGeneralSettingsListByIdObj(int DepartmentId, DBConnection dbConnection);
        VAT_NBT getLatestVatNbt(DBConnection dbConnection);
        int InsertVatNBT(decimal vat, decimal nbt1, decimal nbt2, DBConnection dbConnection);

    }

    public class GeneralSettingsDAOImpl : GeneralSettingsDAO
    {
        public int SaveGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "INSERT INTO public.\"GENERAL_SETTINGS\" (\"DEPARTMENT_ID\", \"BID_OPENING_PERIOD\", \"CAN_OVERRIDE\", \"BID_ONLY_REGISTERED_SUPPLIER\",\"VIEW_BIDS_ONLINE_UPONPR_CREATION\",\"MANUAL_BID_ALLOWS_ONLY_SELECETED_ITEMS\" ) VALUES ( " + DepartmentId + ", " + BidOpeningPeriod + " , " + CanOverride + ", " + BidOnlyRegisteredSupplier + ", " + ViewBidsOnlineUponPrCreation + ", " + ManualBidAllowsOnlySelectedItems + " );";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE public.\"GENERAL_SETTINGS\" SET \"BID_OPENING_PERIOD\" = " + BidOpeningPeriod + ", \"CAN_OVERRIDE\" = " + CanOverride + ", \"BID_ONLY_REGISTERED_SUPPLIER\" = " + BidOnlyRegisteredSupplier + " ,\"VIEW_BIDS_ONLINE_UPONPR_CREATION\" = " + ViewBidsOnlineUponPrCreation + ",\"MANUAL_BID_ALLOWS_ONLY_SELECETED_ITEMS\"=" + ManualBidAllowsOnlySelectedItems + "  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteGeneralSettings(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"GENERAL_SETTINGS\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GeneralSetting> FetchGeneralSettingsList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GENERAL_SETTINGS\" ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GeneralSetting>(dbConnection.dr);
            }
        }

        public List<GeneralSetting> FetchGeneralSettingsListById(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GENERAL_SETTINGS\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GeneralSetting>(dbConnection.dr);
            }
        }

        public GeneralSetting FetchGeneralSettingsListByIdObj(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"GENERAL_SETTINGS\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<GeneralSetting>(dbConnection.dr);
            }
        }

        public VAT_NBT getLatestVatNbt(DBConnection dbConnection)
        {
            throw new NotImplementedException();
        }

        public int InsertVatNBT(decimal vat, decimal nbt1, decimal nbt2, DBConnection dbConnection) {
            throw new NotImplementedException();
        }
    }

    public class GeneralSettingsDAOSQLImpl : GeneralSettingsDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".GENERAL_SETTINGS (DEPARTMENT_ID, BID_OPENING_PERIOD, CAN_OVERRIDE, BID_ONLY_REGISTERED_SUPPLIER,VIEW_BIDS_ONLINE_UPONPR_CREATION,MANUAL_BID_ALLOWS_ONLY_SELECETED_ITEMS ) VALUES ( " + DepartmentId + ", " + BidOpeningPeriod + " , " + CanOverride + ", " + BidOnlyRegisteredSupplier + ", " + ViewBidsOnlineUponPrCreation + ", " + ManualBidAllowsOnlySelectedItems + " );";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateGeneralSettings(int DepartmentId, decimal BidOpeningPeriod, int CanOverride, int BidOnlyRegisteredSupplier, int ViewBidsOnlineUponPrCreation, int ManualBidAllowsOnlySelectedItems, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".GENERAL_SETTINGS SET BID_OPENING_PERIOD = " + BidOpeningPeriod + ", CAN_OVERRIDE = " + CanOverride + ", BID_ONLY_REGISTERED_SUPPLIER = " + BidOnlyRegisteredSupplier + " ,VIEW_BIDS_ONLINE_UPONPR_CREATION = " + ViewBidsOnlineUponPrCreation + ",MANUAL_BID_ALLOWS_ONLY_SELECETED_ITEMS=" + ManualBidAllowsOnlySelectedItems + "  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteGeneralSettings(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".GENERAL_SETTINGS  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<GeneralSetting> FetchGeneralSettingsList(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GENERAL_SETTINGS ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GeneralSetting>(dbConnection.dr);
            }
        }

        public List<GeneralSetting> FetchGeneralSettingsListById(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GENERAL_SETTINGS  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<GeneralSetting>(dbConnection.dr);
            }
        }

        public GeneralSetting FetchGeneralSettingsListByIdObj(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".GENERAL_SETTINGS  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<GeneralSetting>(dbConnection.dr);
            }
        }

        public VAT_NBT getLatestVatNbt(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM VAT_NBT_VALUE WHERE Id = (SELECT MAX(Id) FROM VAT_NBT_VALUE)";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())

            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<VAT_NBT>(dbConnection.dr);


            }
            
        }

        public int InsertVatNBT(decimal vat, decimal nbt1, decimal nbt2,DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO VAT_NBT_VALUE(EFFECTIVE_DATE, VAT_VALUE,NBT_VALUE_1, NBT_VALUE_2) VALUES('"+LocalTime.Now+"',"+vat+", "+nbt1+","+nbt2+")";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();


        }



    }
}
