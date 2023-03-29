using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface ItemCategoryApprovalDAO
    {
        int SaveItemCategoryApprovalLimit(int limitId, int categoryId, int limitFor, int approvalType, int allowedApprovedCount, int canOveride, int overideUserId, decimal minimumValue, decimal maximumValue, int userId, DateTime now, int LocalImportType, DBConnection dbConnection);
        int UpdateItemCategoryApprovalLimit(int limitId, int categoryId, int limitFor, int approvalType, int allowedApprovedCount, int canOveride, int overideUserId, decimal minimumValue, decimal maximumValue, int userId, DateTime now, int LocalImportType, DBConnection dbConnection);
        void saveApprovalLimitCommitte(int limitId, int committeeId, DateTime effectiveDate, DBConnection dbConnection);
        void saveApprovalLimitUser(int limitId, int anyUserDesignationId, DateTime effectiveDate, DBConnection dbConnection);
        void DeleteApprovalLimitCommitte(int limitId,  DBConnection dbConnection);
        void DeleteApprovalLimitUser(int limitId, DBConnection dbConnection);
        void DeleteItemCategoryApprovalLimit(int limitId, DBConnection dbConnection);     
        
        int DeleteItemCategoryApprovalLimit(int companyId, int categoryId, DBConnection dbConnection);
        List<ItemCategoryBidApproval> getItemCategoryBidApprovalByDesignationId(int designationId, DBConnection dbConnection);
        int DeleteItemCategoryApprovalTypes(int companyId, int categoryId, DBConnection dbConnection);
        
        List<ItemCategoryPOApproval> getItemCategoryPOApprovalByDesignationId(int designationId, DBConnection dbConnection);
        int  getItemCategoryPOApprovalCountByDesignationId(int designationId, DBConnection dbConnection);
        int UpdateItemCategoryPOApproval(int poId, int itemId, int categoryId, int designationId,int paDesinationId ,int userId , DateTime now, int loggedInDesignationId, DBConnection dbConnection);
        int getPOApprovalStatus(int poId, DBConnection dbConnection);
       
    }

    public class ItemCategoryApprovalDAOImpl : ItemCategoryApprovalDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];
        
        public int DeleteItemCategoryApprovalTypes(int companyId, int categoryId, DBConnection dbConnection)
        {           
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".APPROVAL_TYPES " +
                                           " WHERE APPROVAL_LIMIT_ID IN (SELECT APPROVAL_LIMIT_ID FROM " + dbLibrary + ".APPROVAL_LIMITS b JOIN "+
                                           " " + dbLibrary + ".ITEM_CATEGORY c ON b.CATEGORY_ID =  c.CATEGORY_ID  "+
                                           " WHERE b.CATEGORY_ID= " + categoryId + " AND c.COMPANY_ID = " + companyId + " )";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteItemCategoryApprovalLimit(int companyId, int categoryId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE a FROM " + dbLibrary + ".APPROVAL_LIMITS AS a " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS b ON a.CATEGORY_ID =  b.CATEGORY_ID " +
                                           " WHERE a.CATEGORY_ID= " + categoryId + " AND b.COMPANY_ID = " + companyId + "";
 
            return dbConnection.cmd.ExecuteNonQuery();
        }
       
        public int getItemCategoryPOApprovalCountByDesignationId(int designationId, DBConnection dbConnection)
        {
            int noOfApprovals = 0;
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM  " + dbLibrary + ".PO_APPROVAL "+
                                               " WHERE  DESIGNATION_ID = " + designationId + " "+
                                               " AND IS_APPROVED = 0  "+
                                               " AND PO_ID IN (SELECT PO_ID FROM  " + dbLibrary + ".PO_MASTER WHERE ( IS_APPROVED = 0 OR IS_APPROVED IS NULL ))";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                noOfApprovals = noOfApprovals + int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM   " + dbLibrary + ".PO_APPROVAL " +
                                               " WHERE  PA_DESIGNATIONID = " + designationId + " " +
                                               " AND HAS_PENDING_APPROVAL = 1" +
                                               " AND IS_APPROVED = 0  AND PO_ID IN (SELECT PO_ID FROM  " + dbLibrary + ".PO_MASTER WHERE ( IS_APPROVED = 0 OR IS_APPROVED IS NULL ))";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                noOfApprovals = noOfApprovals + int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            
            return noOfApprovals;
        }

        public List<ItemCategoryPOApproval> getItemCategoryPOApprovalByDesignationId(int designationId, DBConnection dbConnection)
        {
            List<ItemCategoryPOApproval> listItemApproval = new List<ItemCategoryPOApproval>();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "SELECT * FROM  " + dbLibrary + ".PO_APPROVAL " +
                                           " WHERE  IS_APPROVED = 0  " +
                                           " AND PO_ID IN (SELECT PO_ID FROM  " + dbLibrary + ".PO_MASTER WHERE ( IS_APPROVED = 0 OR IS_APPROVED IS NULL ))";

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                listItemApproval.AddRange(dataAccessObject.ReadCollection<ItemCategoryPOApproval>(dbConnection.dr));
            }
            dbConnection.cmd.CommandText = "SELECT * FROM  " + dbLibrary + ".PO_APPROVAL " +
                                           " WHERE PA_DESIGNATIONID = " + designationId + "" +
                                           " AND HAS_PENDING_APPROVAL = 1" +
                                           " AND IS_APPROVED = 0  " +
                                           " AND PO_ID IN (SELECT PO_ID FROM  " + dbLibrary + ".PO_MASTER WHERE ( IS_APPROVED = 0 OR IS_APPROVED IS NULL ))";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                listItemApproval.AddRange(dataAccessObject.ReadCollection<ItemCategoryPOApproval>(dbConnection.dr));
            }
            return listItemApproval;

        }

        public int UpdateItemCategoryPOApproval(int poId, int itemId, int categoryId, int designationId, int paDesinationId,int userId , DateTime now,int loggedInDesignationId, DBConnection dbConnection)
        {

            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PO_APPROVAL " +
                                           " WHERE PO_ID = " + poId + " " +
                                           " AND ITEM_ID = " + itemId + " " +
                                           " AND HAS_PENDING_APPROVAL = 1" +
                                           " AND DESIGNATION_ID = "+ designationId + " "+
                                           " AND CATEGORY_ID = " + categoryId + " ";

            if (int.Parse(dbConnection.cmd.ExecuteScalar().ToString()) >= 1)
            {
               
                dbConnection.cmd.CommandText = "SELECT PA_DESIGNATIONID FROM " + dbLibrary + ".PO_APPROVAL " +
                                               " WHERE PO_ID = " + poId + " " +
                                               " AND ITEM_ID = " + itemId + " " +
                                               " AND HAS_PENDING_APPROVAL = 1" +
                                               " AND CATEGORY_ID = " + categoryId + " ";
                int alternateDesignationId = 0;
                if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                {
                    alternateDesignationId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }

                if (alternateDesignationId == loggedInDesignationId)
                {
                    dbConnection.cmd.CommandText = "SELECT COUNT FROM " + dbLibrary + ".PO_APPROVAL " +
                                                          " WHERE PO_ID = " + poId + " " +
                                                          " AND ITEM_ID = " + itemId + " " +
                                                          " AND CATEGORY_ID = " + categoryId + " " +
                                                          " AND HAS_PENDING_APPROVAL = 1" +
                                                          " AND PA_DESIGNATIONID = " + paDesinationId + " ";
                    int count = 0;
                    if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                    {
                        count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                    }
                    dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_APPROVAL " +
                                                    " SET IS_APPROVED = 1 " +
                                                    " ,  NO_OF_APPROVAL = " + count + " " +
                                                    " WHERE PO_ID = " + poId + " " +
                                                    " AND ITEM_ID = " + itemId + " " +
                                                    " AND CATEGORY_ID = " + categoryId + " " +
                                                    " AND DESIGNATION_ID = " + designationId + " ";
                    dbConnection.cmd.ExecuteNonQuery();
                    return 1;
                }
                else
                {
                    
                    dbConnection.cmd.CommandText = "SELECT APPROVED_USER FROM " + dbLibrary + ".PO_APPROVAL " +
                                                " WHERE PO_ID = " + poId + " " +
                                                 " AND ITEM_ID = " + itemId + " " +
                                                " AND HAS_PENDING_APPROVAL = 1" +
                                                " AND CATEGORY_ID = " + categoryId + " ";
                    int approvedUser = 0;
                    if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                    {
                        approvedUser = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                    }
                    if (approvedUser != userId)
                    {
                        dbConnection.cmd.CommandText = "SELECT COUNT FROM " + dbLibrary + ".PO_APPROVAL " +
                                                          " WHERE PO_ID = " + poId + " " +
                                                          " AND ITEM_ID = " + itemId + " " +
                                                          " AND CATEGORY_ID = " + categoryId + " " +
                                                          " AND HAS_PENDING_APPROVAL = 1" +
                                                          " AND PA_DESIGNATIONID = " + paDesinationId + " ";
                        int count = 0;
                        if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                        {
                            count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                        }
                        dbConnection.cmd.CommandText = "SELECT NO_OF_APPROVAL FROM " + dbLibrary + ".PO_APPROVAL " +
                                                         " WHERE PO_ID = " + poId + " " +
                                                         " AND ITEM_ID = " + itemId + " " +
                                                         " AND CATEGORY_ID = " + categoryId + " " +
                                                         " AND HAS_PENDING_APPROVAL = 1" +
                                                         " AND PA_DESIGNATIONID = " + paDesinationId + " ";
                        int noOfApproval = 0;
                        if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                        {
                            noOfApproval = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                        }
                       // noOfApproval = noOfApproval + 1;
                        if (count >= noOfApproval)
                        {
                            if(count == 1)
                            {
                                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_APPROVAL " +
                                                           " SET IS_APPROVED = 1 " +
                                                           " , NO_OF_APPROVAL = " + count + " " +
                                                            " WHERE PO_ID = " + poId + " " +
                                                            " AND ITEM_ID = " + itemId + " " +
                                                            " AND CATEGORY_ID = " + categoryId + " " +
                                                            " AND DESIGNATION_ID = " + designationId + " ";
                                dbConnection.cmd.ExecuteNonQuery();
                                return 1;
                            }
                            if (count != noOfApproval)
                            {
                                noOfApproval = noOfApproval + 1;
                                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_APPROVAL " +
                                                            " SET NO_OF_APPROVAL = " + noOfApproval  + " " +
                                                             " WHERE PO_ID = " + poId + " " +
                                                             " AND ITEM_ID = " + itemId + " " +
                                                             " AND CATEGORY_ID = " + categoryId + " " +
                                                             " AND DESIGNATION_ID = " + designationId + " ";
                                dbConnection.cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                noOfApproval = count;
                                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_APPROVAL " +
                                                               " SET IS_APPROVED = 1 " +
                                                               " ,  NO_OF_APPROVAL = " + noOfApproval + " " +
                                                               " WHERE PO_ID = " + poId + " " +
                                                               " AND ITEM_ID = " + itemId + " " +
                                                               " AND CATEGORY_ID = " + categoryId + " " +
                                                               " AND DESIGNATION_ID = " + designationId + " ";
                                dbConnection.cmd.ExecuteNonQuery();
                                return 1;
                            }
                        }
                    }else
                    {
                        return -1;
                    }

                }

                //
                return 2;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT IS_APPROVED FROM " + dbLibrary + ".PO_APPROVAL " +
                                                     " WHERE PO_ID = " + poId + " " +
                                                     " AND ITEM_ID = " + itemId + " " +
                                                     " AND CATEGORY_ID = " + categoryId + " "+
                                                     " AND DESIGNATION_ID = " + designationId + " ";

                int approved = 0;
                if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                {
                    approved = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                }
                if (approved != 1)
                {
                    dbConnection.cmd.CommandText = "SELECT COUNT FROM " + dbLibrary + ".PO_APPROVAL " +
                                                         " WHERE PO_ID = " + poId + " " +
                                                         " AND ITEM_ID = " + itemId + " " +
                                                         " AND CATEGORY_ID = " + categoryId + " " +
                                                         " AND HAS_PENDING_APPROVAL = 0" +
                                                         " AND DESIGNATION_ID = " + designationId + " ";
                    int count = 0;
                    if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                    {
                        count = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                    }
                    if(count == 1)
                    {
                        dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_APPROVAL " +
                                                       " SET IS_APPROVED = 1 " +
                                                        " WHERE PO_ID = " + poId + " " +
                                                        " AND ITEM_ID = " + itemId + " " +
                                                        " AND CATEGORY_ID = " + categoryId + " " +
                                                        " AND DESIGNATION_ID = " + designationId + " ";
                        dbConnection.cmd.ExecuteNonQuery();
                        return 1;
                    }
                    
                    dbConnection.cmd.CommandText = "SELECT NO_OF_APPROVAL FROM " + dbLibrary + ".PO_APPROVAL " +
                                                     " WHERE PO_ID = " + poId + " " +
                                                     " AND ITEM_ID = " + itemId + " " +
                                                     " AND CATEGORY_ID = " + categoryId + " " +
                                                     " AND HAS_PENDING_APPROVAL = 1" +
                                                     " AND PA_DESIGNATIONID = " + paDesinationId + " ";
                    int noOfApproval = 0;
                    if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                    {
                        noOfApproval = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                    }
                    noOfApproval = noOfApproval + 1;
                    if (count >= noOfApproval)
                    {
                        if (count != noOfApproval)
                        {
                            dbConnection.cmd.CommandText = "SELECT APPROVED_USER FROM " + dbLibrary + ".PO_APPROVAL " +
                                                           " WHERE PO_ID = " + poId + " " +
                                                           " AND ITEM_ID = " + itemId + " " +
                                                           " AND CATEGORY_ID = " + categoryId + " " +
                                                           " AND HAS_PENDING_APPROVAL = 1";
                            int approvedUserId = 0;
                            if (dbConnection.cmd.ExecuteScalar().ToString() != "")
                            {
                                approvedUserId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                            }
                            if(approvedUserId == designationId)
                            {
                                return -1;
                            }
                            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_APPROVAL " +
                                                        " SET NO_OF_APPROVAL = " + noOfApproval + " " +
                                                         " WHERE PO_ID = " + poId + " " +
                                                         " AND ITEM_ID = " + itemId + " " +
                                                         " AND CATEGORY_ID = " + categoryId + " " +
                                                         " AND DESIGNATION_ID = " + designationId + " ";
                            dbConnection.cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PO_APPROVAL " +
                                                           " SET IS_APPROVED = 1 " +
                                                           " ,  NO_OF_APPROVAL = " + noOfApproval + " " +
                                                           " WHERE PO_ID = " + poId + " " +
                                                           " AND ITEM_ID = " + itemId + " " +
                                                           " AND CATEGORY_ID = " + categoryId + " " +
                                                           " AND DESIGNATION_ID = " + designationId + " ";
                            dbConnection.cmd.ExecuteNonQuery();
                        }
                    }

                }

                    //
                return 2;
            }
        }

        public int getPOApprovalStatus(int poId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt  FROM  " + dbLibrary + ".PO_APPROVAL " +
                                           " WHERE PO_ID = " + poId + " " +
                                           " AND IS_APPROVED = 0";
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
        }

        public int SaveItemCategoryApprovalLimit(int LimidId , int categoryId, int limitFor, int approvalType, int allowedApprovedCount, int canOveride, int overideDesignationId, decimal minimumValue, decimal maximumValue, int userId, DateTime now, int LocalImportType, DBConnection dbConnection)
        {
            int LimitId;
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".APPROVAL_LIMITS ";
            if (int.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
            {
                LimitId = 1;
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT MAX(LIMIT_ID)+1 FROM " + dbLibrary + ".APPROVAL_LIMITS";
                LimitId = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            }
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".APPROVAL_LIMITS (LIMIT_ID,CATEGORY_ID,MINIMUM_AMOUNT,MAXIMUM_AMOUNT,LIMIT_FOR ,APPROVAL_TYPE ,APPROVAL_COUNT ,CAN_OVERIDE ,OVERIDE_DESIGNATION ,ENTERED_USER,ENTERED_DATE, LIMIT_TYPE )" +
                                            " VALUES (" + LimitId + ", " + categoryId + ", " + minimumValue + " ," + maximumValue + ", " + limitFor + " , " + approvalType + " , " + allowedApprovedCount + " , "+canOveride+" , "+ overideDesignationId + ","+userId+" ,'"+now+"', "+ LocalImportType + ")";
             dbConnection.cmd.ExecuteNonQuery();
            return LimitId;
        }

        public int UpdateItemCategoryApprovalLimit(int LimidId, int categoryId, int limitFor, int approvalType, int allowedApprovedCount, int canOveride, int overideDesignationId, decimal minimumValue, decimal maximumValue, int userId, DateTime now, int LocalImportType, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = " UPDATE " + dbLibrary + ".APPROVAL_LIMITS" +
                                           " SET MINIMUM_AMOUNT = " + minimumValue + " " +
                                           " , MAXIMUM_AMOUNT = " + maximumValue + "" +
                                            " , LIMIT_FOR = " + limitFor + "" +
                                            " , APPROVAL_TYPE = " + approvalType + "" +
                                            " , APPROVAL_COUNT = " + allowedApprovedCount + "" +
                                            " , CAN_OVERIDE = " + canOveride + "" +
                                            " , OVERIDE_DESIGNATION = " + overideDesignationId + "" +
                                            " , ENTERED_USER = " + userId + "" +
                                            " , ENTERED_DATE = '" + now + "'" +
                                            ", LIMIT_TYPE = "+ LocalImportType + " " +
                                            " WHERE LIMIT_ID =" + LimidId + "" +
                                            " AND CATEGORY_ID = " +categoryId+" ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public void saveApprovalLimitCommitte(int limitId, int committeeId, DateTime effectiveDate, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".APPROVAL_LIMIT_COMMITTEE " +
                                           " WHERE APPROVAL_LIMIT_ID = "+ limitId + "" +
                                           " AND COMMITTEE_ID = "+committeeId+" ";
            int x = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (int.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".APPROVAL_LIMIT_COMMITTEE (APPROVAL_LIMIT_ID,COMMITTEE_ID,EFFECTIVE_DATE)" +
                                           " VALUES (" + limitId + ", " + committeeId + ", '" + effectiveDate + "')";
            }else
            {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".APPROVAL_LIMIT_COMMITTEE "+
                                               " SET   COMMITTEE_ID="+ committeeId + ""+
                                               " ,EFFECTIVE_DATE = '"+effectiveDate+"'" +
                                               " WHERE APPROVAL_LIMIT_ID="+limitId+"";
            }
           
            dbConnection.cmd.ExecuteNonQuery();
        }

        public void saveApprovalLimitUser(int limitId, int anyUserDesignationId, DateTime effectiveDate, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".APPROVAL_LIMIT_USER " +
                                           " WHERE LIMIT_ID = " + limitId + "" +
                                           " AND DESIGNATION_ID = " + anyUserDesignationId + " ";
            if (int.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".APPROVAL_LIMIT_USER (LIMIT_ID,DESIGNATION_ID,EFFECTIVE_FROM)" +
                                            " VALUES (" + limitId + ", " + anyUserDesignationId + ", '" + effectiveDate + "')";
            }else
            {
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".APPROVAL_LIMIT_USER " +
                                             " SET   DESIGNATION_ID=" + anyUserDesignationId + "" +
                                             " , EFFECTIVE_FROM = '" + effectiveDate + "'" +
                                             " WHERE LIMIT_ID=" + limitId + "";
            }
            dbConnection.cmd.ExecuteNonQuery();
        }

        public void DeleteApprovalLimitCommitte(int limitId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "Delete " + dbLibrary + ".APPROVAL_LIMIT_COMMITTEE " +
                                            " WHERE APPROVAL_LIMIT_ID=" + limitId + "";
       
            dbConnection.cmd.ExecuteNonQuery();
        }
        
        public void DeleteApprovalLimitUser(int limitId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "Delete " + dbLibrary + ".APPROVAL_LIMIT_USER " +
                                            " WHERE LIMIT_ID=" + limitId + "";

            dbConnection.cmd.ExecuteNonQuery();
        }

        public void DeleteItemCategoryApprovalLimit(int limitId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "Delete " + dbLibrary + ".APPROVAL_LIMITS " +
                                             " WHERE LIMIT_ID=" + limitId + "";

            dbConnection.cmd.ExecuteNonQuery();
        }

        public List<ItemCategoryBidApproval> getItemCategoryBidApprovalByDesignationId(int designationId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM  " + dbLibrary + ".BID_APPROVAL WHERE DESIGNATION_ID = " + designationId + " " +
                                           " AND IS_APPROVED = 0  AND BID_ID IN (SELECT BID_ID FROM  " + dbLibrary + ".BIDDING WHERE IS_QUOTATION_APPROVED = 0 )";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<ItemCategoryBidApproval>(dbConnection.dr);
            }
        }

    }
}

