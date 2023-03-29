using CLibrary.Common;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure
{
   public interface MaterialRequestDAO
    {
        int SaveMaterialRequest(int itemId, int subDepartmentId, int submkitteddate, int requestedQty, int receivedQty, int isCompleted, DBConnection dbConnection);
        int updateMaterialQty(int itemId, int subDepartmentId,  int receivedQty, DBConnection dbConnection);
    }
    public class MaterialRequestDAOImpl : MaterialRequestDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SaveMaterialRequest(int itemId, int subDepartmentId, int submkitteddate, int requestedQty, int receivedQty, int isCompleted, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".MATERIAL_REQUEST WHERE ITEM_ID = " + itemId + " AND SUB_DEPARTMENT_ID = " + subDepartmentId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            int result = 0;
            if (count == 0)
            {

                dbConnection.cmd.Parameters.Clear();

                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".MATERIAL_REQUEST (ITEM_ID , SUB_DEPARTMENT_ID , SUBMITTED_DATE ,REQUESTED_QTY ,RECEIVED_QTY, IS_COMPLETED) VALUES (" + itemId + "," + subDepartmentId + ",'" + LocalTime.Now + "'," +requestedQty + "," + 0 + ", " + 0 + ")";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                result = dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT REQUESTED_QTY AS Qty FROM " + dbLibrary + ".MATERIAL_REQUEST WHERE ITEM_ID = " + itemId + " AND SUB_DEPARTMENT_ID = " + subDepartmentId + "";
                var qty = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MATERIAL_REQUEST SET REQUESTED_QTY = " + (qty + requestedQty) + ", IS_COMPLETED = "+ isCompleted + " WHERE  ITEM_ID = " + itemId + " AND SUB_DEPARTMENT_ID = " + subDepartmentId + "";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                result = dbConnection.cmd.ExecuteNonQuery();

            }
            return result;

        }

        public int updateMaterialQty(int itemId, int subDepartmentId, int receivedQty,  DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT REQUESTED_QTY AS Qty FROM " + dbLibrary + ".MATERIAL_REQUEST WHERE ITEM_ID = " + itemId + " AND SUB_DEPARTMENT_ID = " + subDepartmentId + "";
            var qty = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            dbConnection.cmd.CommandText = "SELECT RECEIVED_QTY AS Qty FROM " + dbLibrary + ".MATERIAL_REQUEST WHERE ITEM_ID = " + itemId + " AND SUB_DEPARTMENT_ID = " + subDepartmentId + "";
            var recQty = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".MATERIAL_REQUEST SET REQUESTED_QTY = " + (qty - receivedQty) + ", RECEIVED_QTY = " + (recQty + receivedQty) + " IS_COMPLETED = " +( qty > receivedQty ? 0 : 1) + " WHERE  ITEM_ID = " + itemId + " AND SUB_DEPARTMENT_ID = " + subDepartmentId + "";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
           
        }
    }

}
