using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller
{
    public interface ItemCategoryApprovalController
    {
        int ManageItemCategoryApprovalLimit(int limitId, int categoryId, int limitFor, int approvalType, int allowedApprovedCount, int canOveride, int overideUserId, decimal minimumValue, decimal maximumValue,int committeeId,int anyUserDesignationId , DateTime effectiveDate, int userId, DateTime now, string action, int designationId, int LocalImportType);
        int DeleteItemCategoryApprovalLimitDetials(int limitId);

        List<ItemCategoryBidApproval> GetItemCategoryBidApprovalByDesignationId(int designationId);

        int GetItemCategoryPOApprovalCountByDesignationId(int designationId);
        List<ItemCategoryPOApproval> GetItemCategoryPOApprovalByDesignationId(int designationId);
        int UpdateItemCategoryPOApproval(int poId, int itemId, int categoryId, int designationId ,int paDesinationId,int userId ,int loggedInDesignationId);
        int getPOApprovalStatus(int poId);

    }

    public class ItemCategoryApprovalControllerImpl : ItemCategoryApprovalController
    {
       
        public int GetItemCategoryPOApprovalCountByDesignationId(int designationId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryApprovalDAO itemCategoryDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                return itemCategoryDAO.getItemCategoryPOApprovalCountByDesignationId(designationId, dbConnection);
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

        public List<ItemCategoryPOApproval> GetItemCategoryPOApprovalByDesignationId(int designationId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryApprovalDAO itemCategoryDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                return itemCategoryDAO.getItemCategoryPOApprovalByDesignationId(designationId, dbConnection);
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

        public int UpdateItemCategoryPOApproval(int poId, int itemId, int categoryId, int designationId, int paDesinationId,int userId,int loggedInDesignationId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryApprovalDAO itemCategoryDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                return itemCategoryDAO.UpdateItemCategoryPOApproval(poId, itemId, categoryId, designationId, paDesinationId, userId, LocalTime.Now, loggedInDesignationId, dbConnection);
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

        public int getPOApprovalStatus(int poId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryApprovalDAO itemCategoryDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                return itemCategoryDAO.getPOApprovalStatus(poId, dbConnection);
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

        public int ManageItemCategoryApprovalLimit(int limitId, int categoryId, int limitFor, int approvalType, int allowedApprovedCount, int canOveride, int overideUserId, decimal minimumValue, decimal maximumValue, int committeeId, int anyUserDesignationId ,DateTime effectiveDate, int userId, DateTime now, string action, int designationId, int LocalImportType)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryApprovalDAO itemCategoryApprovalDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                if (action == "Save")
                {
                    limitId =  itemCategoryApprovalDAO.SaveItemCategoryApprovalLimit(limitId, categoryId, limitFor, approvalType, allowedApprovedCount, canOveride, overideUserId, minimumValue, maximumValue, userId, now, LocalImportType,dbConnection);
                    if(approvalType == 2)
                    {
                        itemCategoryApprovalDAO.saveApprovalLimitCommitte(limitId,committeeId,effectiveDate, dbConnection);
                    }else if(approvalType == 3)
                    {
                        itemCategoryApprovalDAO.saveApprovalLimitUser(limitId, anyUserDesignationId, effectiveDate, dbConnection);
                    }else
                    {
                        // do nothing
                       
                    }
                    return 1;
                }
                else
                {
                    itemCategoryApprovalDAO.UpdateItemCategoryApprovalLimit(limitId, categoryId, limitFor, approvalType, allowedApprovedCount, canOveride, overideUserId, minimumValue, maximumValue, userId, now, LocalImportType, dbConnection);
                    
                    if (approvalType == 2)
                    {
                        itemCategoryApprovalDAO.saveApprovalLimitCommitte(limitId, committeeId, effectiveDate, dbConnection);
                        //Delete any if found 
                        itemCategoryApprovalDAO.DeleteApprovalLimitUser(limitId ,dbConnection);
                    }
                    else if (approvalType == 3)
                    {                       
                        itemCategoryApprovalDAO.saveApprovalLimitUser(limitId, anyUserDesignationId, effectiveDate, dbConnection); 
                        //Delete any if found 
                        itemCategoryApprovalDAO.DeleteApprovalLimitCommitte(limitId,dbConnection);
                    }
                    else
                    {
                        // do nothing
                    }

                    // ** Edit recommandation and approval PRs

                    //if (limitFor == 1) {
                    //    //TabulationRecommendationDAO tabulationRecommendationDAO = DAOFactory.CreateTabulationRecommendationDAO();
                    //    //tabulationRecommendationDAO.UpdateApprovalLimitForRecommandation(categoryId, userId, anyUserDesignationId, minimumValue, maximumValue, dbConnection);
                    //    List<TabulationRecommendation> tabulationIdList = DAOFactory.CreateTabulationRecommendationDAO().tabulationIdList(categoryId, minimumValue, maximumValue, dbConnection);

                    //    for (int i = 0; i < tabulationIdList.Count; i++) {
                    //        TabulationMaster tabulationMaster = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByTabulationId(tabulationIdList[i].TabulationId, dbConnection);
                    //        int deleteTabulations = DAOFactory.CreateTabulationRecommendationDAO().DeleteTabulations(tabulationIdList[i].TabulationId, dbConnection);

                    //        DAOFactory.CreateTabulationMasterDAO().PopulateRecommendation(tabulationMaster.TabulationId, categoryId, tabulationMaster.NetTotal, userId, designationId, tabulationMaster.SelectionRemarks, dbConnection);

                    //    }

                    //}

                    //else if (limitFor == 2) {
                    //    //TabulationApprovalDAO tabulationApprovalDAO = DAOFactory.CreateTabulationApprovalDAO();
                    //    //tabulationApprovalDAO.UpdateApprovalLimitForApproval(categoryId, userId, anyUserDesignationId, minimumValue, maximumValue, dbConnection);
                    //    List<TabulationApproval> tabulationIdList = DAOFactory.CreateTabulationApprovalDAO().tabulationIdList(categoryId, minimumValue, maximumValue, dbConnection);

                    //    for (int i = 0; i < tabulationIdList.Count; i++) {
                    //        TabulationMaster tabulationMaster = DAOFactory.CreateTabulationMasterDAO().GetTabulationsByTabulationId(tabulationIdList[i].TabulationId, dbConnection);
                    //        int deleteTabulations = DAOFactory.CreateTabulationApprovalDAO().DeleteTabulations(tabulationIdList[i].TabulationId, dbConnection);
                    //        DAOFactory.CreateTabulationMasterDAO().PopulateApproval(tabulationMaster.TabulationId, categoryId, tabulationMaster.NetTotal, userId, designationId, tabulationMaster.SelectionRemarks, dbConnection);

                    //    }
                    //} **//

                    return 1;
                }
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

        public int DeleteItemCategoryApprovalLimitDetials(int limitId)
        {
            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryApprovalDAO itemCategoryApprovalDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                itemCategoryApprovalDAO.DeleteItemCategoryApprovalLimit(limitId, dbConnection);
                itemCategoryApprovalDAO.DeleteApprovalLimitUser(limitId,  dbConnection);                   
                itemCategoryApprovalDAO.DeleteApprovalLimitCommitte(limitId,  dbConnection);
                return 1;
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

        public List<ItemCategoryBidApproval> GetItemCategoryBidApprovalByDesignationId(int designationId)
        {

            DBConnection dbConnection = new DBConnection();
            try
            {
                ItemCategoryApprovalDAO itemCategoryDAO = DAOFactory.CreateItemCategoryApprovalDAO();
                return itemCategoryDAO.getItemCategoryBidApprovalByDesignationId(designationId, dbConnection);
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
    }
}
