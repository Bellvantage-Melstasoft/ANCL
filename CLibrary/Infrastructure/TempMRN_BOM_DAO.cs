using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    interface TempMRN_BOM_DAO
    {
        int SaveTempBOM(int DepartmentId, int Mrnid, int ItemId, string Meterial, string Description, DBConnection dbConnection);

        List<TempMRN_BOM> GetListById(int DepartmentId, int ItemId, DBConnection dbConnection);

        int DeleteTempDataByDeptId(int DepartmentId, DBConnection dbConnection);

        int GetNextMrnIdObj(int DepartmentId, DBConnection dbConnection);

        int DeleteTempData(int DepartmentId, int ItemId, DBConnection dbConnection);

        int DeleteBOMByMrnId(int MrnId, int DepartmentId, int ItemId, DBConnection dbConnection);

        List<TempMRN_BOM> GetBOMListByMrnItemId(int DepartmentId, int MrnId, int itemId, DBConnection dbConnection);

        List<TempMRN_BOM> GetItemspecification(int ItemId, DBConnection dbConnection);
    }

    public class TempMRN_BOM_DAOImpl : TempMRN_BOM_DAO
    {

        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveTempBOM(int DepartmentId, int Mrnid, int ItemId, string Meterial, string Description, DBConnection dbConnection)
        {

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".TempMRN_BOM WHERE  MRN_ID = " + Mrnid + " AND  ITEM_ID = " + ItemId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            count = count + 1;
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".TempMRN_BOM (DEPARTMENT_ID, MRN_ID, ITEM_ID, SEQ_NO, METERIAL, DESCRIPTION) VALUES ( " + DepartmentId + ", " + Mrnid + " , " + ItemId + ", " + count + ", '" + Meterial + "', '" + Description + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempMRN_BOM> GetListById(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_BOM  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempMRN_BOM>(dbConnection.dr);
            }
        }
        public int DeleteTempDataByDeptId(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_BOM  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetNextMrnIdObj(int DepartmentId, DBConnection dbConnection)
        {
            int MrnId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".MRN_MASTER";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                MrnId = 001;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (MRN_ID)+1 AS MAXid FROM " + dbLibrary + ".MRN_MASTER";
                MrnId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return MrnId;
            }
        }

        public int DeleteTempData(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_BOM  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteBOMByMrnId(int MrnId, int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempMRN_BOM  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND MRN_ID = " + MrnId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempMRN_BOM> GetBOMListByMrnItemId(int DepartmentId, int Mrnid, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_BOM  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND MRN_ID = " + Mrnid + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempMRN_BOM>(dbConnection.dr);
            }
        }

        public List<TempMRN_BOM> GetItemspecification(int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempMRN_BOM  WHERE ITEM_ID=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempMRN_BOM>(dbConnection.dr);
            }
        }

    }
}
