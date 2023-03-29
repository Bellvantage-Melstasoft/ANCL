using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
    public interface StockOverrideLogController
    {
        List<StockOverrideLog> GetStockLog(int ItemId);
        List<StockOverrideLog> GetOVerRiddenLog(int WarehouseId, int companyId, int itemid, int maincategoryid, int subcategoryid, DateTime to, DateTime from);

    }
    public class StockOverrideLogControllerImpl : StockOverrideLogController
    {

        public List<StockOverrideLog> GetStockLog(int ItemId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                StockOverrideLogDAO stockOverrideLogDAO = DAOFactory.CreateStockOverrideLogDAO();
                return stockOverrideLogDAO.GetStockLog(ItemId, dbConnection);
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

        public List<StockOverrideLog> GetOVerRiddenLog(int WarehouseId, int companyId, int itemid, int maincategoryid, int subcategoryid, DateTime to, DateTime from) {
            DBConnection dbConnection = new DBConnection();
            try {
                StockOverrideLogDAO stockOverrideLogDAO = DAOFactory.CreateStockOverrideLogDAO();
                return stockOverrideLogDAO.GetOVerRiddenLog( WarehouseId,  companyId,  itemid,  maincategoryid,  subcategoryid, to,from , dbConnection);
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