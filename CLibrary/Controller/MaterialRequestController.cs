using CLibrary.Common;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller
{
   public interface MaterialRequestController
    {
        int SaveMaterialRequest(int itemId, int subDepartmentId, int submkitteddate, int requestedQty, int receivedQty, int isCompleted);
        int updateMaterialQty(int itemId, int subDepartmentId, int receivedQty);
    }
    public class MaterialRequestControllerImpl : MaterialRequestController
    {
        public int SaveMaterialRequest(int itemId, int subDepartmentId, int submkitteddate, int requestedQty, int receivedQty, int isCompleted)
        {
            //DBConnection dbConnection = new DBConnection();
            //try
            //{
            //    MaterialRequestDAO materialRequestDAO = DAOFactory.CreateMaterialRequestDAO();
            //    return materialRequestDAO.SaveMaterialRequest( itemId,  subDepartmentId,  submkitteddate,  requestedQty,  receivedQty,  isCompleted, dbConnection);
            //}
            //catch (Exception ex)
            //{
            //    dbConnection.RollBack();
            //    throw;
            //}
            //finally
            //{
            //    if (dbConnection.con.State == System.Data.ConnectionState.Open)
            //        dbConnection.Commit();
            //}
            return 0;
        }

        public int updateMaterialQty(int itemId, int subDepartmentId, int receivedQty)
        {
            //DBConnection dbConnection = new DBConnection();
            //try
            //{
            //    MaterialRequestDAO materialRequestDAO = DAOFactory.CreateMaterialRequestDAO();
            //    return materialRequestDAO.updateMaterialQty( itemId,  subDepartmentId,  receivedQty, dbConnection);
            //}
            //catch (Exception ex)
            //{
            //    dbConnection.RollBack();
            //    throw;
            //}
            //finally
            //{
            //    if (dbConnection.con.State == System.Data.ConnectionState.Open)
            //        dbConnection.Commit();
            //}
            return 0;
        }
    }
}
