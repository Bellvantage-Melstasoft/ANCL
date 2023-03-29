using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;

namespace CLibrary.Controller
{
    public interface TRMasterController
    {
        int saveTR(TR_Master trm);
       // List<int> saveTR(List<TR_Master> trm, int userId);
        List<TR_Master> FetchTRsByCreatedUser(int companyId, int CreatedBy, DateTime date, int trCode, List<int> toWarehouseIds, List<int> fromWarehouseIds);
        List<TR_Master> GetTRListByWarehouseId(List<int> WarehouseId, DateTime date);
        List<TR_Master> FetchTRsSByCompanyId(int CompanyId, DateTime date, int trCode, List <int> toWarehouseIds, List<int> fromWarehouseIds);
        TR_Master GetTRMasterToView(int TrId, int CompanyId);
        List<TR_Master> fetchTRListforApproval(List<int> warehouseIds);
        int ApproveOrRejectTR(int TrId, int isApproved, int UserId, string remark);
        int TerminateTR(int TrId, int TerminatedBy, string Remarks);
        TR_Master getTRM(int trID);
        int updateTR(TR_Master tr);
        List<TR_Master> fetchSubmittedTRList(List<int> WarehouseIds);
        String GetTRCode(int TRId);
        List<string> GetTrCodesByTrIds(List<int> TrIds);
        List<TR_Master> fetchTrListByCreatedUser(int CompanyId, int CreatedBy, DateTime date, int TRCode, List<int> fromWarehouseIds, List<int> toWarehouseIds, string trStatuts, string approvalStatuts);
        List<TR_Master> fetchTrListByCompanyId(int CompanyId, DateTime date, int TRCode, List<int> fromWarehouseIds, List<int> toWarehouseIds, string trStatuts, string approvalStatuts);
        List<TR_Master> FetchTRsByCreatedUser(int CreatedBy, DateTime date);
        List<TR_Master> FetchTRsSByCompanyId(int CompanyId, DateTime date);

    }

        public class TRMasterControllerImpl : TRMasterController
        {
        public List<TR_Master> FetchTRsSByCompanyId(int CompanyId, DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.FetchTRsSByCompanyId(CompanyId, date, dbConnection);

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
        public int saveTR(TR_Master trm)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.saveTR(trm, dbConnection);

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

        public List<TR_Master> FetchTRsByCreatedUser(int companyId, int CreatedBy, DateTime date, int trCode, List<int> toWarehouseIds, List<int> fromWarehouseIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.FetchtrsByCreatedUser(companyId, CreatedBy, date, trCode, toWarehouseIds, fromWarehouseIds, dbConnection);

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
         
        public TR_Master getTRM(int trId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.getTRM(trId, dbConnection);

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
        public int updateTR(TR_Master tr)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.updateTR(tr, dbConnection);

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


        public List<TR_Master> GetTRListByWarehouseId(List<int> WarehouseId, DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.GetTRListByWarehouseId(WarehouseId, date, dbConnection);

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

        public List<TR_Master> FetchTRsSByCompanyId(int CompanyId, DateTime date, int trCode, List<int> toWarehouseIds, List<int> fromWarehouseIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.FetchTRsSByCompanyId(CompanyId, date, trCode, toWarehouseIds, fromWarehouseIds, dbConnection);

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

        public TR_Master GetTRMasterToView(int TrId, int CompanyId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.GetTRMasterToView(TrId, CompanyId, dbConnection);

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

        public List<TR_Master> fetchTRListforApproval(List<int> warehouseIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.fetchTRListforApproval(warehouseIds, dbConnection);

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

        public int ApproveOrRejectTR(int TrId, int isApproved, int UserId, string remark)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.ApproveOrRejectTR(TrId, isApproved, UserId, remark, dbConnection);

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
       

        public int TerminateTR(int TrId, int TerminatedBy, string Remarks)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.TerminateTR(TrId, TerminatedBy, Remarks, dbConnection);

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

        public List<TR_Master> fetchSubmittedTRList(List<int> fetchSubmittedTRList)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.fetchSubmittedTRList(fetchSubmittedTRList, dbConnection);

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
        public String GetTRCode(int TRId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.GetTRCode(TRId, dbConnection);

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

        public List<string> GetTrCodesByTrIds(List<int> TrIds)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.GetTrCodesByTrIds(TrIds, dbConnection);

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
        //public List<int> saveTR(List<TR_Master> trm, int userId)
        //{
            //DBConnection dbConnection = new DBConnection();
            //try
            //{
            //    TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
            //    MrnDAOInterface MrnDAO = DAOFactory.CreateMRNDAO();
            //    MrnDetailStatusLogDAO mrnDetailStatusLogDAO = DAOFactory.CreateMrnDetailStatusLogDAO();
            //    List<int> TRcodes = new List<int>();

            //    for (int i = 0; i < trm.Count; i++)
            //    {
            //        int TRCode = trDAO.saveTR(trm[i], dbConnection);
            //        TRcodes.Add(TRCode);

            //        for (int j = 0; j < trm[i].TRDetails.Count; j++)
            //        {
            //            int result = MrnDAO.updateMRNDetails(trm[i].TRDetails[j].TRDId, dbConnection);
                        
            //            if(result > 0)
            //            {
            //                 result = mrnDetailStatusLogDAO.UpdateMRNLog(trm[i].TRDetails[j].TRDId,userId,9, dbConnection);
            //            }
            //        }
            //    }
            //    return TRcodes;
            //}
            //catch (Exception)
            //{
            //    dbConnection.RollBack();
            //    throw;
            //}
            //finally
            //{
            //    if (dbConnection.con.State == System.Data.ConnectionState.Open)
            //    {
            //        dbConnection.Commit();
            //    }
            //}
        //}

        public List<TR_Master> fetchTrListByCreatedUser(int CompanyId, int CreatedBy, DateTime date, int TRCode, List<int> fromWarehouseIds, List<int> toWarehouseIds, string trStatuts, string approvalStatuts) {
            DBConnection dbConnection = new DBConnection();
            try {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.fetchTrListByCreatedUser(CompanyId, CreatedBy, date, TRCode, fromWarehouseIds, toWarehouseIds, trStatuts, approvalStatuts, dbConnection);

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

        public List<TR_Master> fetchTrListByCompanyId(int CompanyId, DateTime date, int TRCode, List<int> fromWarehouseIds, List<int> toWarehouseIds, string trStatuts, string approvalStatuts) { 
        DBConnection dbConnection = new DBConnection();
            try {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.fetchTrListByCompanyId(CompanyId, date, TRCode, fromWarehouseIds, toWarehouseIds, trStatuts, approvalStatuts, dbConnection);

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
        public List<TR_Master> FetchTRsByCreatedUser(int CreatedBy, DateTime date)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                TRMasterDAO trDAO = DAOFactory.CreateTRMasterDAO();
                return trDAO.FetchtrsByCreatedUser(CreatedBy, date, dbConnection);

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
