using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure
{
    public interface TempBOMDAO
    {
        int SaveTempBOM(int DepartmentId,int Prid, int ItemId, int SeqNo, string Meterial, string Description, DBConnection dbConnection);
        //int UpdateTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description, DBConnection dbConnection);
        int GetNextPrIdObj(int DepartmentId, DBConnection dbConnection);
        List<TempBOM> GetListById(int DepartmentId, int ItemId, DBConnection dbConnection);
        int DeleteTempData(int DepartmentId, int ItemId, DBConnection dbConnection);
        int DeleteTempDataByDeptId(int DepartmentId, DBConnection dbConnection);
        int UpdateTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description, DBConnection dbConnection);
        List<TempBOM> GetBOMListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection);
        List<TempBOM> GetItemspecification( int ItemId, DBConnection dbConnection);
        int DeleteBOMByPrId(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection);
    }

    public class TempBOMDAOImpl : TempBOMDAO
    {
        public int SaveTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO public.\"TempBOM\" (\"DEPARTMENT_ID\", \"PR_ID\", \"ITEM_ID\", \"SEQ_NO\", \"METERIAL\", \"DESCRIPTION\") VALUES ( " + DepartmentId + ", " + Prid + " , " + ItemId + ", " + SeqNo + ", '" + Meterial + "', '" + Description + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetNextPrIdObj(int DepartmentId, DBConnection dbConnection)
        {
            int PrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"PR_MASTER\"";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                PrId = 001;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (\"PR_ID\")+1 AS MAXid FROM public.\"PR_MASTER\"";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return PrId;
            }
        }
        public List<TempBOM> GetItemspecification( int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempBOM\"  WHERE \"DEPARTMENT_ID\" = \"ITEM_ID\"=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempBOM>(dbConnection.dr);
            }
        }

        public List<TempBOM> GetListById(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempBOM\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempBOM>(dbConnection.dr);
            }
        }

        public int DeleteTempData(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempBOM\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempDataByDeptId(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempBOM\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE public.\"TempBOM\" SET  \"METERIAL\" = '" + Meterial + "', \"DESCRIPTION\" = '" + Description + "' WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"PR_ID\" = " + Prid + " AND  \"ITEM_ID\" = " + ItemId + " AND \"SEQ_NO\" = " + SeqNo + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempBOM> GetBOMListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"TempBOM\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\" = " + prId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempBOM>(dbConnection.dr);
            }
        }
        

        public int DeleteBOMByPrId(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"TempBOM\"  WHERE \"DEPARTMENT_ID\" = " + DepartmentId + " AND \"ITEM_ID\"=" + ItemId + " AND \"PR_ID\" = " + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }

    public class TempBOMDAOSQLImpl : TempBOMDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".TempBOM (DEPARTMENT_ID, PR_ID, ITEM_ID, SEQ_NO, METERIAL, DESCRIPTION) VALUES ( " + DepartmentId + ", " + Prid + " , " + ItemId + ", " + SeqNo + ", '" + Meterial + "', '" + Description + "');";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int GetNextPrIdObj(int DepartmentId, DBConnection dbConnection)
        {
            int PrId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_MASTER";

            var count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (count == 0)
            {
                PrId = 001;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX (PR_ID)+1 AS MAXid FROM " + dbLibrary + ".PR_MASTER";
                PrId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return PrId;
            }
        }

        public List<TempBOM> GetListById(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempBOM  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempBOM>(dbConnection.dr);
            }
        }

        public int DeleteTempData(int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempBOM  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteTempDataByDeptId(int DepartmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempBOM  WHERE DEPARTMENT_ID = " + DepartmentId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateTempBOM(int DepartmentId, int Prid, int ItemId, int SeqNo, string Meterial, string Description, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".TempBOM SET  METERIAL = '" + Meterial + "', DESCRIPTION = '" + Description + "' WHERE DEPARTMENT_ID = " + DepartmentId + " AND PR_ID = " + Prid + " AND  ITEM_ID = " + ItemId + " AND SEQ_NO = " + SeqNo + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<TempBOM> GetBOMListByPrIdItemId(int DepartmentId, int prId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TempBOM  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND PR_ID = " + prId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempBOM>(dbConnection.dr);
            }
        }

        public List<TempBOM> GetItemspecification( int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".ADD_ITEMS_BOM  WHERE ITEM_ID=" + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TempBOM>(dbConnection.dr);
            }
        }

        

        public int DeleteBOMByPrId(int PrId, int DepartmentId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".TempBOM  WHERE DEPARTMENT_ID = " + DepartmentId + " AND ITEM_ID=" + ItemId + " AND PR_ID = " + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
}
