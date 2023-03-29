using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
   public interface SystemDivisionDAO
    {
        int SaveSystemDivision(string systemDivisionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
        int UpdateSystemDivision(int systemDivisionId, string systemDivisionName, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection);
    
        int DeleteSystemDivision(int systemDivisionId, DBConnection dbConnection);
       
        List<SystemDivision> FetchSystemDivision(DBConnection dbConnection);
        List<SystemDivisionFunction> FetchSystemDivisionFunctionsBySystemDivisionId(int systemDivisionId,DBConnection dbConnection);
        SystemDivision FetchSystemDivisionBySystemDivisionId(int systemDivisionId,DBConnection dbConnection);
        SystemDivisionFunction FetchSystemDivisionFunctionsBySystemDivisionIdandFinctionId(int systemDivisionId,int functionId, DBConnection dbConnection);
        int updateSystemDivisionFunction(int systemDivisionId, int functionId, string functionName, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection);
        int SaveSystemDivisionfunction(int systemDivisionId, string systemDivisionfunctionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection);



        int assignSystemDivisionWithFunction(int systemDivisionId, int functionId, string functionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection);
        int updateSystemDivisionWithFunction(int systemDivisionId, int oldfunctionId, int newfunctionId, string newfunctionName, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection);
        int DeleteSystemDivisionFunction(int systemDivisionId, int functionId, DBConnection dbConnection);

    }
    public class SystemDivisionDAOImpl : SystemDivisionDAO
    {
        public int assignSystemDivisionWithFunction(int systemDivisionId, int functionId, string functionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"SYSDIVISION_FUNCTION\" WHERE \"SYS_DIVISION_ID\" = " + systemDivisionId + " AND \"FUNCTION_ID\" = " + functionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {

                dbConnection.cmd.CommandText = "INSERT INTO  public.\"SYSDIVISION_FUNCTION\" (\"SYS_DIVISION_ID\" , \"FUNCTION_ID\" , \"FUNCTION_NAME\" , \"CREATED_DATE\" , \"CREATED_BY\", \"UPDATED_DATE\", \"UPDATED_BY\") VALUES (" + systemDivisionId + "," + functionId + ",'" + functionName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "')";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

         public int SaveSystemDivisionfunction(int systemDivisionId, string systemDivisionfunctionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            return 0;
        }

        public int DeleteSystemDivision(int systemDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM public.\"SYSTEM_DIVISION\" WHERE \"DIVISION_ID\" = " + systemDivisionId + "";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteSystemDivisionFunction(int systemDivisionId, int functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM public.\"SYSDIVISION_FUNCTION\" WHERE  \"SYS_DIVISION_ID\" = " + systemDivisionId + " AND  \"FUNCTION_ID\" = " + functionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<SystemDivision> FetchSystemDivision(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"SYSTEM_DIVISION\"";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SystemDivision>(dbConnection.dr);
            }
        }

        public SystemDivision FetchSystemDivisionBySystemDivisionId(int systemDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM public.\"SYSTEM_DIVISION\" WHERE  \"DIVISION_ID\" = " + systemDivisionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SystemDivision>(dbConnection.dr);
            }
        }

        public List<SystemDivisionFunction> FetchSystemDivisionFunctionsBySystemDivisionId(int systemDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  sdf.\"SYS_DIVISION_ID\" ,sdf.\"FUNCTION_ID\" , sdf.\"CREATED_DATE\" ,  sdf.\"CREATED_BY\" ,  sdf.\"UPDATED_DATE\" ,  sdf.\"UPDATED_BY\" ,  sd.\"DIVISION_NAME\",  sdf.\"FUNCTION_NAME\"   FROM public.\"SYSDIVISION_FUNCTION\" AS sdf INNER JOIN  public.\"SYSTEM_DIVISION\" AS sd ON sdf.\"SYS_DIVISION_ID\" =sd.\"DIVISION_ID\" WHERE sdf.\"SYS_DIVISION_ID\" = " + systemDivisionId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SystemDivisionFunction>(dbConnection.dr);
            }
        }

        public SystemDivisionFunction FetchSystemDivisionFunctionsBySystemDivisionIdandFinctionId(int systemDivisionId, int functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  sdf.\"SYS_DIVISION_ID\" ,sdf.\"FUNCTION_ID\" , sdf.\"CREATED_DATE\" ,  sdf.\"CREATED_BY\" ,  sdf.\"UPDATED_DATE\" ,  sdf.\"UPDATED_BY\" ,  sd.\"DIVISION_NAME\",  sdf.\"FUNCTION_NAME\"   FROM public.\"SYSDIVISION_FUNCTION\" AS sdf INNER JOIN  public.\"SYSTEM_DIVISION\" AS sd ON sdf.\"SYS_DIVISION_ID\" =sd.\"DIVISION_ID\" WHERE sdf.\"SYS_DIVISION_ID\" = " + systemDivisionId + " AND sdf.\"FUNCTION_ID\" =" + functionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SystemDivisionFunction>(dbConnection.dr);
            }
        }

        public int SaveSystemDivision(string systemDivisionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            decimal systemDivisionId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"SYSTEM_DIVISION\" WHERE \"DIVISION_NAME\" = '" + systemDivisionName + "'";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"SYSTEM_DIVISION\" ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    systemDivisionId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (\"DIVISION_ID\")+1 AS MAXid FROM public.\"SYSTEM_DIVISION\" ";
                    systemDivisionId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO  public.\"SYSTEM_DIVISION\" (\"DIVISION_ID\" , \"DIVISION_NAME\" , \"CREATED_DATE\" , \"CREATED_BY\", \"UPDATED_DATE\", \"UPDATED_BY\", \"IS_ACTIVE\") VALUES (" + systemDivisionId + ",'" + systemDivisionName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateSystemDivision(int systemDivisionId, string systemDivisionName, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"SYSTEM_DIVISION\" WHERE \"DIVISION_NAME\" = '" + systemDivisionName + "' AND \"DIVISION_ID\" != " + systemDivisionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"SYSTEM_DIVISION\" SET \"DIVISION_NAME\" = '" + systemDivisionName + "' , \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "', \"IS_ACTIVE\" = " + IsActive + " WHERE \"DIVISION_ID\" = " + systemDivisionId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int updateSystemDivisionWithFunction(int systemDivisionId, int oldfunctionId, int newfunctionId, string newfunctionName, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            if (oldfunctionId == newfunctionId)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE public.\"SYSDIVISION_FUNCTION\" SET   \"FUNCTION_ID\" = " + newfunctionId + ", \"FUNCTION_NAME\" = '" + newfunctionName + "' , \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "' WHERE \"SYS_DIVISION_ID\" = " + systemDivisionId + " AND  \"FUNCTION_ID\" = " + oldfunctionId + "";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"SYSDIVISION_FUNCTION\" WHERE \"FUNCTION_ID\" = '" + newfunctionId + "' AND \"SYS_DIVISION_ID\" = " + systemDivisionId + "";
                var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (countExist == 0)
                {
                    dbConnection.cmd.CommandText = "UPDATE public.\"SYSDIVISION_FUNCTION\" SET   \"FUNCTION_ID\" = " + newfunctionId + ", \"FUNCTION_NAME\" = '" + newfunctionName + "' , \"UPDATED_DATE\" = '" + UpdatedDate + "', \"UPDATED_BY\" = '" + UpdatedBy + "' WHERE \"SYS_DIVISION_ID\" = " + systemDivisionId + " AND  \"FUNCTION_ID\" = " + oldfunctionId + "";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }
            }

        }
        public int updateSystemDivisionFunction(int systemDivisionId, int functionId,string functionName, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            return 0;
        }
    }

    public class SystemDivisionDAOSQLImpl : SystemDivisionDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int assignSystemDivisionWithFunction(int systemDivisionId, int functionId, string functionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SYSDIVISION_FUNCTION WHERE SYS_DIVISION_ID = " + systemDivisionId + " AND FUNCTION_ID = " + functionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SYSDIVISION_FUNCTION (SYS_DIVISION_ID , FUNCTION_ID , FUNCTION_NAME , CREATED_DATE , CREATED_BY, UPDATED_DATE, UPDATED_BY) VALUES (" + systemDivisionId + "," + functionId + ",'" + functionName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "')";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int DeleteSystemDivision(int systemDivisionId, DBConnection dbConnection)
        {
            int Isdelete = 0;
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SYSTEM_DIVISION WHERE DIVISION_ID = " + systemDivisionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            Isdelete=dbConnection.cmd.ExecuteNonQuery();
            if (Isdelete > 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = " SELECT COUNT(*) FROM " + dbLibrary + ".ROLE_FUNCTION WHERE SYSTEM_DIVISION_ID=" + systemDivisionId + "";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (count != 0)
                {

                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = " DELETE FROM " + dbLibrary + ".ROLE_FUNCTION WHERE SYSTEM_DIVISION_ID=" + systemDivisionId + "";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    Isdelete = dbConnection.cmd.ExecuteNonQuery();

                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = " SELECT COUNT(*) FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE SYSTEM_DIVISION_ID=" + systemDivisionId + "";
                    var count2 = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                    if (count2 != 0)
                    {
                        if (Isdelete > 0)
                        {
                            dbConnection.cmd.CommandText = " DELETE FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE SYSTEM_DIVISION_ID=" + systemDivisionId + "";
                            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                            return dbConnection.cmd.ExecuteNonQuery();
                        }
                    }
                }

            }

            return Isdelete;  
            
        }

        public int DeleteSystemDivisionFunction(int systemDivisionId, int functionId, DBConnection dbConnection)
        {
            int Isdelete = 0;

            dbConnection.cmd.Parameters.Clear();
             
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".SYSDIVISION_FUNCTION WHERE  SYS_DIVISION_ID = " + systemDivisionId + " AND  FUNCTION_ID = " + functionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            Isdelete=dbConnection.cmd.ExecuteNonQuery();
            if (Isdelete > 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = " SELECT COUNT(*) FROM " + dbLibrary + ".ROLE_FUNCTION WHERE SYSTEM_DIVISION_ID=" + systemDivisionId + "AND FUNCTION_ID =" + functionId + "";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (count!=0)
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = " DELETE FROM " + dbLibrary + ".ROLE_FUNCTION WHERE SYSTEM_DIVISION_ID=" + systemDivisionId + "AND FUNCTION_ID =" + functionId + "";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    Isdelete = dbConnection.cmd.ExecuteNonQuery();

                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = " SELECT COUNT(*) FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE SYSTEM_DIVISION_ID =" + systemDivisionId + " AND ACTION_ID =" + functionId + "";
                    var count2 = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                    if (count2!=0)
                    {
                        if (Isdelete > 0)
                        {
                            dbConnection.cmd.Parameters.Clear();

                            dbConnection.cmd.CommandText = " DELETE FROM " + dbLibrary + ".COMPANY_USER_ACCESS WHERE SYSTEM_DIVISION_ID=" + systemDivisionId + "ACTION_ID =" + functionId + "";

                            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                            return dbConnection.cmd.ExecuteNonQuery();

                        }
                        
                    }
                   
                }
                
               
            }

            return Isdelete;
           
        }

        public List<SystemDivision> FetchSystemDivision(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SYSTEM_DIVISION";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SystemDivision>(dbConnection.dr);
            }
        }

        public SystemDivision FetchSystemDivisionBySystemDivisionId(int systemDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SYSTEM_DIVISION WHERE  DIVISION_ID = " + systemDivisionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SystemDivision>(dbConnection.dr);
            }
        }

        public List<SystemDivisionFunction> FetchSystemDivisionFunctionsBySystemDivisionId(int systemDivisionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  sdf.SYS_DIVISION_ID ,sdf.FUNCTION_ID , sdf.CREATED_DATE ,  sdf.CREATED_BY ,  sdf.UPDATED_DATE ,  sdf.UPDATED_BY ,  sd.DIVISION_NAME,  sdf.FUNCTION_NAME   FROM  " + dbLibrary + ".SYSDIVISION_FUNCTION AS sdf INNER JOIN   " + dbLibrary + ".SYSTEM_DIVISION AS sd ON sdf.SYS_DIVISION_ID =sd.DIVISION_ID WHERE sdf.SYS_DIVISION_ID = " + systemDivisionId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SystemDivisionFunction>(dbConnection.dr);
            }
        }
        public SystemDivisionFunction FetchSystemDivisionFunctionsBySystemDivisionIdandFinctionId(int systemDivisionId, int functionId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT  sdf.SYS_DIVISION_ID ,sdf.FUNCTION_ID , sdf.CREATED_DATE ,  sdf.CREATED_BY ,  sdf.UPDATED_DATE ,  sdf.UPDATED_BY ,  sd.DIVISION_NAME,  sdf.FUNCTION_NAME   FROM  " + dbLibrary + ".SYSDIVISION_FUNCTION AS sdf INNER JOIN   " + dbLibrary + ".SYSTEM_DIVISION AS sd ON sdf.SYS_DIVISION_ID =sd.DIVISION_ID WHERE sdf.SYS_DIVISION_ID = " + systemDivisionId + "AND sdf.FUNCTION_ID =" + functionId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SystemDivisionFunction>(dbConnection.dr);
            }
        }

        public int SaveSystemDivision(string systemDivisionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            decimal systemDivisionId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SYSTEM_DIVISION WHERE DIVISION_NAME = '" + systemDivisionName + "'";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SYSTEM_DIVISION ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    systemDivisionId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (DIVISION_ID)+1 AS MAXid FROM " + dbLibrary + ".SYSTEM_DIVISION ";
                    systemDivisionId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SYSTEM_DIVISION (DIVISION_ID , DIVISION_NAME , CREATED_DATE , CREATED_BY, UPDATED_DATE, UPDATED_BY, IS_ACTIVE) VALUES (" + systemDivisionId + ",'" + systemDivisionName + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "', " + IsActive + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int UpdateSystemDivision(int systemDivisionId, string systemDivisionName, DateTime UpdatedDate, string UpdatedBy, int IsActive, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SYSTEM_DIVISION WHERE DIVISION_NAME = '" + systemDivisionName + "' AND DIVISION_ID != " + systemDivisionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SYSTEM_DIVISION SET DIVISION_NAME = '" + systemDivisionName + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "', IS_ACTIVE = " + IsActive + " WHERE DIVISION_ID = " + systemDivisionId + " ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

        public int updateSystemDivisionWithFunction(int systemDivisionId, int oldfunctionId, int newfunctionId, string newfunctionName, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            if(oldfunctionId==newfunctionId)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SYSDIVISION_FUNCTION SET   FUNCTION_ID = " + newfunctionId + ", FUNCTION_NAME = '" + newfunctionName + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "' WHERE SYS_DIVISION_ID = " + systemDivisionId + " AND  FUNCTION_ID = " + oldfunctionId + "";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SYSDIVISION_FUNCTION WHERE FUNCTION_ID = '" + newfunctionId + "' AND SYS_DIVISION_ID = " + systemDivisionId + "";
                var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (countExist == 0)
                {
                    dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SYSDIVISION_FUNCTION SET   FUNCTION_ID = " + newfunctionId + ", FUNCTION_NAME = '" + newfunctionName + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "' WHERE SYS_DIVISION_ID = " + systemDivisionId + " AND  FUNCTION_ID = " + oldfunctionId + "";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }
                }
           
        }



        public int updateSystemDivisionFunction(int systemDivisionId, int functionId,string functionName, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SYSDIVISION_FUNCTION SET  FUNCTION_NAME = '" + functionName + "' , UPDATED_DATE = '" + UpdatedDate + "', UPDATED_BY = '" + UpdatedBy + "' WHERE SYS_DIVISION_ID = " + systemDivisionId + " AND  FUNCTION_ID = " + functionId + "";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();

        }



        public int SaveSystemDivisionfunction(int systemDivisionId, string systemDivisionfunctionName, DateTime CreatedDate, string CreatedBy, DateTime UpdatedDate, string UpdatedBy, DBConnection dbConnection)
        {
            decimal functionId = 0;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SYSDIVISION_FUNCTION WHERE FUNCTION_NAME = '" + systemDivisionfunctionName + "'AND SYS_DIVISION_ID =" + systemDivisionId + "";
            var countExist = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (countExist == 0)
            {
                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SYSDIVISION_FUNCTION WHERE SYS_DIVISION_ID =" + systemDivisionId + " ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    functionId = 1;
                }
                else
                {
                    dbConnection.cmd.CommandText = "SELECT MAX (FUNCTION_ID)+1 AS MAXid FROM " + dbLibrary + ".SYSDIVISION_FUNCTION WHERE SYS_DIVISION_ID =" + systemDivisionId + "";
                    functionId = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SYSDIVISION_FUNCTION (SYS_DIVISION_ID , FUNCTION_ID , CREATED_DATE , CREATED_BY, UPDATED_DATE, UPDATED_BY,FUNCTION_NAME) VALUES (" + systemDivisionId + ",'" + functionId + "','" + CreatedDate + "','" + CreatedBy + "', '" + UpdatedDate + "', '" + UpdatedBy + "','" + systemDivisionfunctionName + "')";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }
        }

    }
}
