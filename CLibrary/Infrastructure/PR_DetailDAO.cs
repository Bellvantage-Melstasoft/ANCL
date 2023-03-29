using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;
using System.Data;

namespace CLibrary.Infrastructure
{
    public interface PR_DetailDAO
    {
        int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided , string Remarks , int MeasurementId, DBConnection dbConnection);
       
        
        List<PR_Details> FetchPR_DetailsByDeptIdAndPrId(int PrId, int companyid, DBConnection dbConnection);
        DataTable FetchPR_DetailsByDeptIdAndPrIdDatatable(int PrId, int companyid, DBConnection dbConnection);
        
        
        List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsApproved(int PrId, int companyId, DBConnection dbConnection);
        List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsRejected(int PrId, int companyId, DBConnection dbConnection);
        List<PR_Details> FetchByPRDetails(int PrId, DBConnection dbConnection);
        PR_Details FetchPR_DetailsBidComparion(int PrId, DBConnection dbConnection);
        List<PR_Details> FetchBidCompletedPR(int CompanyId, DBConnection dbConnection);
        List<PR_Details> GetAllItems(int PrId, DBConnection dbConnection);
        List<AddItem> GetItemsByPrID(int PrId, DBConnection dbConnection);
        List<AddItem> GetItemsByMrnID(int MrnId, DBConnection dbConnection);
        PR_Details FetchPR_DetailsByPrIdAndItemId(int PrId, int itemId, DBConnection dbConnection);
        int UpdatePRDetails(int PrId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId, DBConnection dbConnection);
        int DeletePrDetailByPrIDAndItemId(int prID, int ItemId, DBConnection dbConnection);
        List<PR_Details> FetchDetailsRejectedPR(int prID, int companyId, DBConnection dbConnection);
        List<PR_Details> FetchDetailsApprovedPR(int prID, int companyId, DBConnection dbConnection);
        int UpdateUpdateForBid(int prID, int ItemId, int submitForBidStatus, int RejectedCount, DBConnection dbConnection);
        int UpdateRejectUpdateForBid(int prID, int ItemId, int submitForBidStatus, DBConnection dbConnection);
        List<PR_Details> FetchBidSubmissionDetails(DBConnection dbConnection);
        List<PR_Details> FetchPR_DetailsByPrIdList(int PrId, DBConnection dbConnection);
        List<PR_Details> FetchNotSubmitedItemsToSupplierPortalView(int PrId, int companyid, DBConnection dbConnection);
        List<PR_Details> FetchtSubmitedItemsToSupplierPortalView(int PrId, int companyId, DBConnection dbConnection);
        int UpdateIsPoRaised(int PrId, int ItemId, int IsPoRaised, DBConnection dbConnection);
        int UpdateIsPoAproved(int PrId, int ItemId, int IsApproved, DBConnection dbConnection);
        int UpdateByRejectPO(int PrId, int ItemId, DBConnection dbConnection);
        int updateReplacementImageStatus(int PrId, int ItemId, int replacementStatus, DBConnection dbConnection);

        int UpdateIsPoRaisedRejectedCount(int PrId, int ItemId, int RejectedCount, DBConnection dbConnection);

        //---2018-09-18  Changes Dcsl
        int UpdateUpdateForBidType(int prID, int ItemId, int BidTypeManualOrBid, DBConnection dbConnection);
        int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity, DBConnection dbConnection);

        //New Methods By Salman created on 2019-01-17
        List<PrDetailsV2> GetPrDetailsForBidSubmission(int PrId, int CompanyId, int WarehouseId, DBConnection dbConnection);
        PrDetailsV2 GetPrDetails(int PrdId, int CompanyId, DBConnection dbConnection);
        List<PR_Details> GetPrDetailsByPRid(int PrId, int CompanyId, DBConnection dbConnection);
        int UpdateStatus(int userId, int prdId, DBConnection dbConnection);
        //Reorder function stock by Pasindu 2020/04/29
        int SavePRDetailsV2(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, DBConnection dbConnection);
        int UpdateterminatedStatus(int userId, int prdId, DBConnection dbConnection);
        int UpdatePRStatus(int userId, int prdId, string DetailStatus,  DBConnection dbConnection);
        int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, DBConnection dbConnection);
        int UpdateLogStatus(int userId, int prdId,  string LogStatus, DBConnection dbConnection);
        int UpdatePRStatuss(int userId, int prdId, string DetailStatus, string LogStatus, DBConnection dbConnection);
        int UpdatePRStatusFoeCancelledPos(int userId, string DetailStatus, List<int> ItemIds, int PrId, DBConnection dbConnection);
    }

    //public class PR_DetailDAOImpl : PR_DetailDAO
    //{
    //    public int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"PR_DETAIL\" WHERE  \"PR_ID\" = " + PrId + " AND  \"ITEM_ID\" = " + ItemId + "";
    //        var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

    //        if (count == 0)
    //        {
    //            dbConnection.cmd.Parameters.Clear();
    //            dbConnection.cmd.CommandText = "INSERT INTO public.\"PR_DETAIL\" (\"PR_ID\", \"ITEM_ID\", \"UNIT\", \"ITEM_DESCRIPTION\", \"ITEM_UPDATED_BY\", \"ITEM_UPDATED_DATETIME\", \"IS_ACTIVE\", \"REPLACEMENT\",\"ITEM_QUANTITY\",\"PURPOSE\",\"SUBMIT_FOR_BID\",\"PR_IS_REJECTED_COUNT\",\"IS_PO_RAISED\",\"IS_PO_REJECTED_COUNT\",\"IS_PO_APPROVED\",\"ESTIMATED_AMOUNT\") VALUES ( " + PrId + ", " + ItemId + " , " + Unit + ", '" + ItemDescription + "', '" + ItemUpdatedBy + "', '" + ItemUpdatedDateTime + "', " + IsActive + ", " + Replacement + "," + ItemQuantity + ",'" + Purpose + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + EstimatedAmount + ");";
    //            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //            return dbConnection.cmd.ExecuteNonQuery();
    //        }
    //        else
    //        {
    //            return -1;
    //        }


    //    }

    //    public List<PR_Details> FetchPR_DetailsByDeptIdAndPrId(int PrId, int companyid, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL\" INNER JOIN public.\"PR_MASTER\" ON public.\"PR_MASTER\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" INNER JOIN public.\"ADD_ITEMS\" ON public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\" INNER JOIN public.\"ITEM_CATEGORY\"     ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" =   public.\"ADD_ITEMS\".\"CATEGORY_ID\" INNER JOIN public.\"ITEM_SUB_CATEGORY\" ON  public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" = public.\"ADD_ITEMS\".\"SUB_CATEGORY_ID\" WHERE public.\"PR_DETAIL\".\"IS_ACTIVE\"=1  AND public.\"PR_DETAIL\".\"PR_ID\"=" + PrId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsApproved(int PrId, int companyId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL\" INNER JOIN public.\"PR_MASTER\" ON public.\"PR_MASTER\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" INNER JOIN public.\"ADD_ITEMS\" ON public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\" WHERE  public.\"PR_MASTER\".\"PR_IS_APPROVED\"=1 AND public.\"PR_DETAIL\".\"PR_ID\"=" + PrId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public List<PR_Details> FetchByPRDetails(int PrId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL\"  WHERE \"PR_ID\"=" + PrId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public PR_Details FetchPR_DetailsByPrIdAndItemId(int PrId, int itemId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL\" INNER JOIN public.\"ADD_ITEMS\" ON public.\"PR_DETAIL\".\"ITEM_ID\" = public.\"ADD_ITEMS\".\"ITEM_ID\" WHERE  public.\"PR_DETAIL\".\"PR_ID\" = " + PrId + " AND public.\"PR_DETAIL\".\"ITEM_ID\" = " + itemId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.GetSingleOject<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public PR_Details FetchPR_DetailsBidComparion(int PrId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL\" INNER JOIN public.\"ADD_ITEMS\" ON public.\"PR_DETAIL\".\"ITEM_ID\" = public.\"ADD_ITEMS\".\"ITEM_ID\" WHERE  public.\"PR_DETAIL\".\"PR_ID\" = " + PrId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.GetSingleOject<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public List<PR_Details> FetchBidCompletedPR(int CompanyId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "select public.\"PR_DETAIL\".\"PR_ID\",public.\"PR_MASTER\".\"PR_CODE\",public.\"BIDDING\".\"END_DATE\" from public.\"PR_MASTER\" INNER JOIN public.\"PR_DETAIL\" ON public.\"PR_DETAIL\".\"PR_ID\" = public.\"PR_MASTER\".\"PR_ID\" INNER JOIN public.\"BIDDING\" ON public.\"PR_DETAIL\".\"PR_ID\" = public.\"BIDDING\".\"PR_ID\" INNER JOIN public.\"SUPPLIER_QUOTATION\" ON public.\"SUPPLIER_QUOTATION\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" WHERE public.\"BIDDING\".\"END_DATE\" < '" +  LocalTime.Now + "' AND public.\"PR_MASTER\".\"DEPARTMENT_ID\"=" + CompanyId + " AND public.\"PR_DETAIL\".\"IS_PO_RAISED\" = 0 AND public.\"SUPPLIER_QUOTATION\".\"IS_SELECTED\" = 0 GROUP BY public.\"PR_DETAIL\".\"PR_ID\",public.\"PR_MASTER\".\"PR_CODE\",public.\"BIDDING\".\"END_DATE\";";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public List<PR_Details> GetAllItems(int PrId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "select * from  public.\"PR_DETAIL\" where \"PR_ID\" = " + PrId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public int UpdatePRDetails(int PrId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        if (oldItemId == ItemId)
    //        {
    //            dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET  \"ITEM_ID\" = " + ItemId + ", \"UNIT\" = " + Unit + ", \"ITEM_DESCRIPTION\" = '" + ItemDescription + "', \"ITEM_UPDATED_BY\" = '" + ItemUpdatedBy + "', \"ITEM_UPDATED_DATETIME\" = '" + ItemUpdatedDateTime + "', \"IS_ACTIVE\" = " + IsActive + ", \"REPLACEMENT\" = " + Replacement + ",\"ITEM_QUANTITY\" = " + ItemQuantity + ",\"PURPOSE\" = '" + Purpose + "',\"ESTIMATED_AMOUNT\"=" + EstimatedAmount + "  WHERE  \"PR_ID\" = " + PrId + " AND  \"ITEM_ID\" = " + oldItemId + ";";
    //            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //            return dbConnection.cmd.ExecuteNonQuery();
    //        }
    //        else
    //        {
    //            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.\"PR_DETAIL\" WHERE  \"PR_ID\" = " + PrId + " AND  \"ITEM_ID\" = " + oldItemId + " ";
    //            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

    //            if (count == 0)
    //            {
    //                dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET  \"ITEM_ID\" = " + ItemId + ", \"UNIT\" = " + Unit + ", \"ITEM_DESCRIPTION\" = '" + ItemDescription + "', \"ITEM_UPDATED_BY\" = '" + ItemUpdatedBy + "', \"ITEM_UPDATED_DATETIME\" = '" + ItemUpdatedDateTime + "', \"IS_ACTIVE\" = " + IsActive + ", \"REPLACEMENT\" = " + Replacement + ",\"ITEM_QUANTITY\" = " + ItemQuantity + ",\"PURPOSE\" = '" + Purpose + "',\"ESTIMATED_AMOUNT\"=" + EstimatedAmount + "  WHERE  \"PR_ID\" = " + PrId + " AND  \"ITEM_ID\" = " + oldItemId + ";";
    //                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //                return dbConnection.cmd.ExecuteNonQuery();
    //            }
    //            else
    //            {
    //                return -1;
    //            }

    //        }
    //    }

    //    public int DeletePrDetailByPrIDAndItemId(int prID, int ItemId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "DELETE FROM public.\"PR_DETAIL\" WHERE \"PR_ID\" = " + prID + " AND \"ITEM_ID\" =" + ItemId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public List<PR_Details> FetchDetailsRejectedPR(int prID, int companyId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL\" INNER JOIN public.\"PR_MASTER\" ON public.\"PR_MASTER\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" INNER JOIN public.\"ADD_ITEMS\" ON public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\" INNER JOIN public.\"ITEM_CATEGORY\"     ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" =   public.\"ADD_ITEMS\".\"CATEGORY_ID\" INNER JOIN public.\"ITEM_SUB_CATEGORY\" ON  public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" = public.\"ADD_ITEMS\".\"SUB_CATEGORY_ID\" WHERE  public.\"PR_DETAIL\".\"PR_ID\"=" + prID + "  AND public.\"PR_DETAIL\".\"IS_ACTIVE\"=1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }
    //    public List<PR_Details> FetchDetailsApprovedPR(int prID, int companyId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL\" INNER JOIN public.\"PR_MASTER\" ON public.\"PR_MASTER\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" INNER JOIN public.\"ADD_ITEMS\" ON public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\" INNER JOIN public.\"ITEM_CATEGORY\"     ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" =   public.\"ADD_ITEMS\".\"CATEGORY_ID\" INNER JOIN public.\"ITEM_SUB_CATEGORY\" ON  public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" = public.\"ADD_ITEMS\".\"SUB_CATEGORY_ID\" WHERE  public.\"PR_DETAIL\".\"PR_ID\"=" + prID + "  AND public.\"PR_DETAIL\".\"IS_ACTIVE\"=1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public int UpdateUpdateForBid(int prID, int ItemId, int submitForBidStatus, int RejectedCount, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET  \"SUBMIT_FOR_BID\" = " + submitForBidStatus + ", \"PR_IS_REJECTED_COUNT\" = " + RejectedCount + "  WHERE \"PR_ID\" = " + prID + " AND \"ITEM_ID\" =" + ItemId + " AND \"IS_ACTIVE\" = 1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public List<PR_Details> FetchBidSubmissionDetails(DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = " SELECT * FROM public.\"PR_MASTER\"  " +
    //                                       " INNER JOIN public.\"PR_DETAIL\" ON public.\"PR_MASTER\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" " +
    //                                       " WHERE public.\"PR_DETAIL\".\"IS_ACTIVE\" = 1 AND public.\"PR_DETAIL\".\"SUBMIT_FOR_BID\" =1";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public List<PR_Details> FetchPR_DetailsByPrIdList(int PrId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT * FROM public.\"PR_DETAIL\" INNER JOIN public.\"PR_MASTER\" ON public.\"PR_MASTER\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" INNER JOIN public.\"ADD_ITEMS\" ON public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\" INNER JOIN public.\"ITEM_CATEGORY\"     ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" =   public.\"ADD_ITEMS\".\"CATEGORY_ID\" INNER JOIN public.\"ITEM_SUB_CATEGORY\" ON  public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" = public.\"ADD_ITEMS\".\"SUB_CATEGORY_ID\" WHERE  public.\"PR_DETAIL\".\"IS_ACTIVE\"= 1 AND public.\"PR_DETAIL\".\"PR_ID\"=" + PrId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public List<PR_Details> FetchNotSubmitedItemsToSupplierPortalView(int PrId, int companyid, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = " SELECT *  FROM public.\"PR_DETAIL\" " +
    //                                       " INNER JOIN public.\"PR_MASTER\" ON public.\"PR_MASTER\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" " +
    //                                       " INNER JOIN public.\"ADD_ITEMS\" ON public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\"  " +
    //                                       " INNER JOIN public.\"ITEM_CATEGORY\"  ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" = public.\"ADD_ITEMS\".\"CATEGORY_ID\"  " +
    //                                       " INNER JOIN public.\"ITEM_SUB_CATEGORY\" ON  public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" = public.\"ADD_ITEMS\".\"SUB_CATEGORY_ID\"  " +
    //                                       " INNER JOIN public.\"BIDDING\" ON public.\"BIDDING\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" " +
    //                                       " AND public.\"BIDDING\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\"  " +
    //                                       " WHERE  public.\"PR_DETAIL\".\"IS_ACTIVE\"= 1 AND public.\"PR_DETAIL\".\"PR_ID\"= " + PrId + " AND public.\"BIDDING\".\"IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL\" = 0";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public List<PR_Details> FetchtSubmitedItemsToSupplierPortalView(int PrId, int companyId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = " SELECT * FROM public.\"PR_DETAIL\" " +
    //                                      " INNER JOIN public.\"PR_MASTER\" ON public.\"PR_MASTER\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" " +
    //                                      " INNER JOIN public.\"ADD_ITEMS\" ON public.\"ADD_ITEMS\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\"  " +
    //                                      " INNER JOIN public.\"ITEM_CATEGORY\"  ON public.\"ITEM_CATEGORY\".\"CATEGORY_ID\" = public.\"ADD_ITEMS\".\"CATEGORY_ID\"  " +
    //                                      " INNER JOIN public.\"ITEM_SUB_CATEGORY\" ON  public.\"ITEM_SUB_CATEGORY\".\"SUB_CATEGORY_ID\" = public.\"ADD_ITEMS\".\"SUB_CATEGORY_ID\"  " +
    //                                      " INNER JOIN public.\"BIDDING\" ON public.\"BIDDING\".\"PR_ID\" = public.\"PR_DETAIL\".\"PR_ID\" " +
    //                                      " AND public.\"BIDDING\".\"ITEM_ID\" = public.\"PR_DETAIL\".\"ITEM_ID\"  " +
    //                                      " WHERE  public.\"PR_DETAIL\".\"IS_ACTIVE\"= 1 AND public.\"PR_DETAIL\".\"PR_ID\"= " + PrId + "";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public int UpdateIsPoRaised(int PrId, int ItemId, int IsPoRaised, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET  \"IS_PO_RAISED\" = " + IsPoRaised + "  WHERE \"PR_ID\" = " + PrId + " AND \"ITEM_ID\" =" + ItemId + " AND \"IS_ACTIVE\" = 1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public int UpdateIsPoAproved(int PrId, int ItemId, int IsApproved, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET  \"IS_PO_APPROVED\" = " + IsApproved + "  WHERE \"PR_ID\" = " + PrId + " AND \"ITEM_ID\" =" + ItemId + " AND \"IS_ACTIVE\" = 1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public int UpdateByRejectPO(int PrId, int ItemId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET \"IS_PO_RAISED\" = 0, \"IS_PO_REJECTED_COUNT\" = 0, \"IS_PO_APPROVED\" = 0 WHERE  \"PR_ID\" = " + PrId + " AND  \"ITEM_ID\" = " + ItemId + ";";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public int updateReplacementImageStatus(int PrId, int ItemId, int replacementStatus, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET \"REPLACEMENT\" = " + replacementStatus + " WHERE  \"PR_ID\" = " + PrId + " AND  \"ITEM_ID\" = " + ItemId + ";";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public int UpdateIsPoRaisedRejectedCount(int PrId, int ItemId, int RejectedCount, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET   \"IS_PO_REJECTED_COUNT\"=" + RejectedCount + "  WHERE \"PR_ID\" = " + PrId + " AND \"ITEM_ID\" =" + ItemId + " AND \"IS_ACTIVE\" = 1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public int UpdateUpdateForBidType(int prID, int ItemId, int BidTypeManualOrBid, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET  \"BID_TYPE_MANUAL_BID\" = " + BidTypeManualOrBid + "  WHERE \"PR_ID\" = " + prID + " AND \"ITEM_ID\" =" + ItemId + " AND \"IS_ACTIVE\" = 1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public int UpdateRejectUpdateForBid(int prID, int ItemId, int submitForBidStatus, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET  \"SUBMIT_FOR_BID\" = " + submitForBidStatus + " WHERE \"PR_ID\" = " + prID + " AND \"ITEM_ID\" =" + ItemId + " AND \"IS_ACTIVE\" = 1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();
    //        dbConnection.cmd.CommandText = "UPDATE public.\"PR_DETAIL\" SET  \"ITEM_QUANTITY\" = " + itemQuantity + "  WHERE \"PR_ID\" = " + prID + " AND \"ITEM_ID\" =" + ItemId + " AND \"IS_ACTIVE\" = 1 ";
    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;
    //        return dbConnection.cmd.ExecuteNonQuery();
    //    }

    //    public List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsRejected(int PrId, int companyId, DBConnection dbConnection)
    //    {
    //        dbConnection.cmd.Parameters.Clear();

    //        dbConnection.cmd.CommandText = "SELECT TOP 1 * FROM ONLINE_BIDDING.dbo.PR_DETAIL AS PD " +
    //                                         "INNER JOIN ONLINE_BIDDING.dbo.PR_MASTER AS PM " +
    //                                         "ON PM.PR_ID = PD.PR_ID " +
    //                                         "INNER JOIN ONLINE_BIDDING.dbo.ADD_ITEMS AS AI " +
    //                                         "ON AI.ITEM_ID = PD.ITEM_ID " +
    //                                         "INNER JOIN ONLINE_BIDDING.DBO.BIDDING AS BD " +
    //                                         "ON PD.PR_ID = BD.PR_ID AND AI.ITEM_ID = BD.ITEM_ID " +
    //                                         "WHERE PM.PR_IS_APPROVED = 1 " +
    //                                         "AND PD.PR_ID = " + PrId + " " +
    //                                         "AND AI.COMPANY_ID = " + companyId + " " +
    //                                         "ORDER BY BD.BIDDING_ORDER_ID DESC";

    //        dbConnection.cmd.CommandType = System.Data.CommandType.Text;

    //        using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
    //        {
    //            DataAccessObject dataAccessObject = new DataAccessObject();
    //            return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
    //        }
    //    }

    //    public List<PR_Details> GetPrDetailsByPrIdForBidSubmission(int PrId, DBConnection dbConnection)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class PR_DetailDAOSQLImpl : PR_DetailDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_DETAIL WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_DETAIL (PR_ID, ITEM_ID, UNIT, ITEM_DESCRIPTION, ITEM_UPDATED_BY, ITEM_UPDATED_DATETIME, IS_ACTIVE, REPLACEMENT,ITEM_QUANTITY,PURPOSE,SUBMIT_FOR_BID,PR_IS_REJECTED_COUNT,IS_PO_RAISED,IS_PO_REJECTED_COUNT,IS_PO_APPROVED,ESTIMATED_AMOUNT,PR_IS_APPROVED_FOR_BID ,SAMPLE_PROVIDED ,REMARKS,MEASUREMENT_ID ) VALUES ( " + PrId + ", " + ItemId + " , " + Unit + ", '" + ItemDescription + "', '" + ItemUpdatedBy + "', '" + ItemUpdatedDateTime + "', " + IsActive + ", " + Replacement + "," + ItemQuantity + ",'" + Purpose + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + EstimatedAmount + "," + 0 + " ," + FileSampleProvided + " , '" + Remarks + "' ,  " + MeasurementId + ");";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.ExecuteNonQuery();
                dbConnection.cmd.CommandText = "SELECT MAX(PRD_ID) AS cnt FROM " + dbLibrary + ".PR_DETAIL WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + "";
                int PRD_ID = Int32.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                return PRD_ID;
            }
            else
            {
                return -1;
            }


        }
        //Reorder function stock by Pasindu 2020/04/29
        public int SavePRDetailsV2(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_DETAIL WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_DETAIL (PR_ID, ITEM_ID, UNIT, ITEM_DESCRIPTION, ITEM_UPDATED_BY, ITEM_UPDATED_DATETIME, IS_ACTIVE, REPLACEMENT,ITEM_QUANTITY,PURPOSE,SUBMIT_FOR_BID,PR_IS_REJECTED_COUNT,IS_PO_RAISED,IS_PO_REJECTED_COUNT,IS_PO_APPROVED,ESTIMATED_AMOUNT,PR_IS_APPROVED_FOR_BID) VALUES ( " + PrId + ", " + ItemId + " , " + Unit + ", '" + ItemDescription + "', '" + ItemUpdatedBy + "', '" + ItemUpdatedDateTime + "', " + IsActive + ", " + Replacement + "," + ItemQuantity + ",'" + Purpose + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + EstimatedAmount + "," + 0 + ");";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                return -1;
            }


        }
        public List<PR_Details> FetchPR_DetailsByDeptIdAndPrId(int PrId, int companyid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PD INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = PD.PR_ID INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = AI.CATEGORY_ID INNER JOIN  " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON  ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID WHERE PD.IS_ACTIVE=1  AND PD.PR_ID=" + PrId + " AND AI.COMPANY_ID=" + companyid + " AND ISC.COMPANY_ID=" + companyid + " AND IC.COMPANY_ID=" + companyid + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public DataTable FetchPR_DetailsByDeptIdAndPrIdDatatable(int PrId, int companyid, DBConnection dbConnection)
        {
            string connectionString = System.Configuration.ConfigurationSettings.AppSettings["dbConString"].ToString();
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(connectionString);

            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                cmd.Connection = con;

                string query = "SELECT PD.PRD_ID as ProductDetailId ,PD.REMARKS AS Remarks  ,PD.REPLACEMENT AS Replacement, PD.ITEM_ID as ItemId ,AI.ITEM_NAME as ItemName ,PD.ITEM_DESCRIPTION as ItemDescription ,PD.ITEM_QUANTITY as ItemQuantity, PM.PR_PROCEDURE as Proceduree,PM.PURCHASE_TYPE as PurchaseType ,PM.REQUIRED_DATE as RequiredDate  FROM " + dbLibrary + ".PR_DETAIL AS PD INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = PD.PR_ID INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = AI.CATEGORY_ID INNER JOIN  " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON  ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID WHERE PD.IS_ACTIVE=1  AND PD.PR_ID=" + PrId + " AND AI.COMPANY_ID=" + companyid + " AND ISC.COMPANY_ID=" + companyid + " AND IC.COMPANY_ID=" + companyid + " ";

                cmd.Parameters.Clear();
                cmd.CommandText = query;

                cmd.CommandType = System.Data.CommandType.Text;
                System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter(cmd);

                con.Open();
                da.Fill(dt);
            }
            catch (Exception)
            {
                throw;
            }

            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return dt;
        }
        public List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsApproved(int PrId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PD " +
                                                "INNER JOIN " + dbLibrary + ".PR_MASTER AS PM " +
                                                "ON PM.PR_ID = PD.PR_ID " +
                                                "INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI " +
                                                "ON AI.ITEM_ID = PD.ITEM_ID " +
                                                "WHERE PM.PR_IS_APPROVED = 1 " +
                                                "AND PD.PR_ID = " + PrId + " " +
                                                "AND AI.COMPANY_ID = " + companyId;

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public List<PR_Details> FetchPR_DetailsByDeptIdAndPrIdIsRejected(int PrId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT TOP 1 * FROM " + dbLibrary + ".PR_DETAIL AS PD " +
                                             "INNER JOIN " + dbLibrary + ".PR_MASTER AS PM " +
                                             "ON PM.PR_ID = PD.PR_ID " +
                                             "INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI " +
                                             "ON AI.ITEM_ID = PD.ITEM_ID " +
                                             "INNER JOIN " + dbLibrary + ".BIDDING AS BD " +
                                             "ON PD.PR_ID = BD.PR_ID AND AI.ITEM_ID = BD.ITEM_ID " +
                                             "WHERE PM.PR_IS_APPROVED = 1 " +
                                             "AND PD.PR_ID = " + PrId + " " +
                                             "AND AI.COMPANY_ID = " + companyId + " " +
                                             "ORDER BY BD.BIDDING_ORDER_ID DESC";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public List<PR_Details> FetchByPRDetails(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM PR_DETAIL AS PD\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME, SUB_CATEGORY_ID FROM ADD_ITEMS_MASTER) AS AIM ON PD.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN(SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME, CATEGORY_ID FROM ITEM_SUB_CATEGORY) AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN(SELECT CATEGORY_ID, CATEGORY_NAME FROM ITEM_CATEGORY) AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "INNER JOIN(SELECT DETAIL_ID, SHORT_CODE FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = PD.MEASUREMENT_ID\n" +
                                            "LEFT JOIN (SELECT ITEM_ID, WAREHOUSE_ID, AVAILABLE_QTY FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = (SELECT WAREHOUSE_ID FROM PR_MASTER WHERE PR_ID = "+ PrId + ") ) AS WIM ON WIM.ITEM_ID = AIM.ITEM_ID " +
                                             "WHERE PD.PR_ID = " + PrId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public PR_Details FetchPR_DetailsByPrIdAndItemId(int PrId, int itemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PD INNER JOIN  " + dbLibrary + ".ADD_ITEMS AS AI ON PD.ITEM_ID = AI.ITEM_ID WHERE  PD.PR_ID = " + PrId + " AND PD.ITEM_ID = " + itemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PR_Details>(dbConnection.dr);
            }
        }

        public PR_Details FetchPR_DetailsBidComparion(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PD INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON PD.ITEM_ID = AI.ITEM_ID WHERE  PD.PR_ID = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<PR_Details>(dbConnection.dr);
            }
        }

        public List<PR_Details> FetchBidCompletedPR(int CompanyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT PD.PR_ID,PM.PR_CODE,BI.END_DATE from " + dbLibrary + ".PR_MASTER AS PM " +
                 "INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PD.PR_ID = PM.PR_ID " +
                 "INNER JOIN " + dbLibrary + ".BIDDING AS BI ON PD.PR_ID = BI.PR_ID " +
                 "INNER JOIN " + dbLibrary + ".SUPPLIER_QUOTATION AS SQ ON SQ.PR_ID = PD.PR_ID " +
                 "WHERE BI.END_DATE < '" + LocalTime.Now + "' AND PM.DEPARTMENT_ID=" + CompanyId + " AND PD.IS_PO_RAISED = 0 AND SQ.IS_SELECTED = 0 " +
                 "GROUP BY PD.PR_ID,PM.PR_CODE,BI.END_DATE;";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public List<PR_Details> GetAllItems(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "select * from " + dbLibrary + ".PR_DETAIL where PR_ID = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public List<AddItem> GetItemsByPrID(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "select Replace(I.ITEM_NAME,',',' ') AS ITEM_NAME from " + dbLibrary + ".ADD_ITEMS_MASTER AS I inner join " + dbLibrary + ".PR_DETAIL AS P ON P.ITEM_ID = I.ITEM_ID where PR_ID = " + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public List<AddItem> GetItemsByMrnID(int MrnId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "select Replace(I.ITEM_NAME,',',' ') AS ITEM_NAME from " + dbLibrary + ".ADD_ITEMS_MASTER AS I inner join " + dbLibrary + ".MRN_DETAILS AS M ON M.ITEM_ID = I.ITEM_ID where MRN_ID = " + MrnId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AddItem>(dbConnection.dr);
            }
        }

        public int UpdatePRDetails(int PrId, int oldItemId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, int FileSampleProvided, string Remarks, int MeasurementId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            if (oldItemId == ItemId)
            {
                dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".PR_DETAIL SET  ITEM_ID = " + ItemId + ", UNIT = " + Unit + ", ITEM_DESCRIPTION = '" + ItemDescription + "', ITEM_UPDATED_BY = '" + ItemUpdatedBy + "', ITEM_UPDATED_DATETIME = '" + ItemUpdatedDateTime + "', IS_ACTIVE = " + IsActive + ", REPLACEMENT = " + Replacement + ",ITEM_QUANTITY = " + ItemQuantity + ",PURPOSE = '" + Purpose + "',ESTIMATED_AMOUNT=" + EstimatedAmount + " , SAMPLE_PROVIDED =" + FileSampleProvided + " , REMARKS ='" + Remarks + "', MEASUREMENT_ID = " + MeasurementId + " WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + oldItemId + ";";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                return dbConnection.cmd.ExecuteNonQuery();
            }
            else
            {
                dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM public.PR_DETAIL WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + oldItemId + " ";
                var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                if (count == 0)
                {
                    dbConnection.cmd.CommandText = "UPDATE public.PR_DETAIL SET  ITEM_ID = " + ItemId + ", UNIT = " + Unit + ", ITEM_DESCRIPTION = '" + ItemDescription + "', ITEM_UPDATED_BY = '" + ItemUpdatedBy + "', ITEM_UPDATED_DATETIME = '" + ItemUpdatedDateTime + "', IS_ACTIVE = " + IsActive + ", REPLACEMENT = " + Replacement + ",ITEM_QUANTITY = " + ItemQuantity + ",PURPOSE = '" + Purpose + "',ESTIMATED_AMOUNT=" + EstimatedAmount + "  WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + oldItemId + ";";
                    //    dbConnection.cmd.CommandText = "UPDATE       .PR_DETAIL SET  ITEM_ID = " + ItemId + ", UNIT = " + Unit + ", ITEM_DESCRIPTION = '" + ItemDescription + "', ITEM_UPDATED_BY = '" + ItemUpdatedBy + "', ITEM_UPDATED_DATETIME = '" + ItemUpdatedDateTime + "', IS_ACTIVE = " + IsActive + ", REPLACEMENT = " + Replacement + ",ITEM_QUANTITY = " + ItemQuantity + ",PURPOSE = '" + Purpose + "',ESTIMATED_AMOUNT=" + EstimatedAmount + " , SAMPLE_PROVIDED =" + FileSampleProvided + " , REMARKS ='" + Remarks + "', MEASUREMENT_ID = " + MeasurementId + " WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + oldItemId + ";";

                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                    return dbConnection.cmd.ExecuteNonQuery();
                }
                else
                {
                    return -1;
                }

            }
        }

        public int DeletePrDetailByPrIDAndItemId(int prID, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "DELETE FROM " + dbLibrary + ".PR_DETAIL WHERE PR_ID = " + prID + " AND ITEM_ID =" + ItemId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_Details> FetchDetailsRejectedPR(int prID, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PD INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = PD.PR_ID INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID =  AI.CATEGORY_ID INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON  ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID WHERE  PD.PR_ID=" + prID + "  AND PD.IS_ACTIVE=1 AND AI.COMPANY_ID=" + companyId + " AND ISC.COMPANY_ID = " + companyId + " AND IC.COMPANY_ID = " + companyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }
        public List<PR_Details> FetchDetailsApprovedPR(int prID, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PD " +
                                           " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = PD.PR_ID " +
                                           " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID =  AI.CATEGORY_ID " +
                                           " INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON  ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID " +
                                           " WHERE  PD.PR_ID=" + prID + "  AND PD.IS_ACTIVE=1 AND AI.COMPANY_ID=" + companyId + " " +
                                           " AND ISC.COMPANY_ID = " + companyId + " AND IC.COMPANY_ID = " + companyId;
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public int UpdateUpdateForBid(int prID, int ItemId, int submitForBidStatus, int RejectedCount, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET  SUBMIT_FOR_BID = " + submitForBidStatus + ", PR_IS_REJECTED_COUNT = " + RejectedCount + "  WHERE PR_ID = " + prID + " AND ITEM_ID =" + ItemId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PR_Details> FetchBidSubmissionDetails(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM " +
                                           " INNER JOIN " + dbLibrary + ".PR_DETAIL AS PD ON PM.PR_ID = PD.PR_ID " +
                                           " WHERE PD.IS_ACTIVE = 1 AND PD.SUBMIT_FOR_BID =1 AND PD.PR_IS_APPROVED_FOR_BID=0";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public List<PR_Details> FetchPR_DetailsByPrIdList(int PrId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PD INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = PD.PR_ID INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC ON IC.CATEGORY_ID = AI.CATEGORY_ID INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON  ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID WHERE  PD.IS_ACTIVE= 1 AND PD.PR_ID=" + PrId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public List<PR_Details> FetchNotSubmitedItemsToSupplierPortalView(int PrId, int companyid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT *  FROM " + dbLibrary + ".PR_DETAIL AS PD" +
                                           " INNER JOIN  " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = PD.PR_ID " +
                                           " INNER JOIN  " + dbLibrary + ".ADD_ITEMS  AS AI ON AI.ITEM_ID =PD.ITEM_ID  " +
                                           " INNER JOIN  " + dbLibrary + ".ITEM_CATEGORY AS IC  ON IC.CATEGORY_ID = AI.CATEGORY_ID  " +
                                           " INNER JOIN  " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON  ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID  " +
                                           " INNER JOIN  " + dbLibrary + ".BIDDING AS BI ON BI.PR_ID = PD.PR_ID " +
                                           " AND BI.ITEM_ID = PD.ITEM_ID  " +
                                           " WHERE  PD.IS_ACTIVE= 1 AND PD.PR_ID= " + PrId + " AND BI.IS_APPROVE_TOVIEW_IN_SUPPLIER_PORTAL = 0 AND AI.COMPANY_ID = " + companyid + " AND IC.COMPANY_ID=" + companyid + "  AND ISC.COMPANY_ID = " + companyid + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public List<PR_Details> FetchtSubmitedItemsToSupplierPortalView(int PrId, int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = " SELECT * FROM " + dbLibrary + ".PR_DETAIL AS PD" +
                                          " INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID = PD.PR_ID " +
                                          " INNER JOIN " + dbLibrary + ".ADD_ITEMS AS AI ON AI.ITEM_ID = PD.ITEM_ID  " +
                                          " INNER JOIN " + dbLibrary + ".ITEM_CATEGORY AS IC  ON IC.CATEGORY_ID = AI.CATEGORY_ID  " +
                                          " INNER JOIN " + dbLibrary + ".ITEM_SUB_CATEGORY AS ISC ON ISC.SUB_CATEGORY_ID = AI.SUB_CATEGORY_ID  " +
                                          " INNER JOIN " + dbLibrary + ".BIDDING AS BI ON BI.PR_ID = PD.PR_ID " +
                                          " AND BI.ITEM_ID = PD.ITEM_ID  " +
                                          " WHERE PD.IS_ACTIVE= 1 AND PD.PR_ID= " + PrId + " AND  AI.COMPANY_ID = " + companyId + " AND IC.COMPANY_ID= " + companyId + " AND ISC.COMPANY_ID = " + companyId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }
        }

        public int UpdateIsPoRaised(int PrId, int ItemId, int IsPoRaised, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET  IS_PO_RAISED = " + IsPoRaised + "  WHERE PR_ID = " + PrId + " AND ITEM_ID =" + ItemId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateIsPoAproved(int PrId, int ItemId, int IsApproved, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET  IS_PO_APPROVED = " + IsApproved + "  WHERE PR_ID = " + PrId + " AND ITEM_ID =" + ItemId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateByRejectPO(int PrId, int ItemId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET IS_PO_RAISED = 0, IS_PO_REJECTED_COUNT = 0, IS_PO_APPROVED = 0 WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int updateReplacementImageStatus(int PrId, int ItemId, int replacementStatus, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET REPLACEMENT = " + replacementStatus + " WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateIsPoRaisedRejectedCount(int PrId, int ItemId, int RejectedCount, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET   IS_PO_REJECTED_COUNT=" + RejectedCount + "  WHERE PR_ID = " + PrId + " AND ITEM_ID =" + ItemId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateUpdateForBidType(int prID, int ItemId, int BidTypeManualOrBid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET  BID_TYPE_MANUAL_BID = " + BidTypeManualOrBid + "  WHERE PR_ID = " + prID + " AND ITEM_ID =" + ItemId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateRejectUpdateForBid(int prID, int ItemId, int submitForBidStatus, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET  SUBMIT_FOR_BID = " + submitForBidStatus + ",PR_IS_APPROVED_FOR_BID=0 WHERE PR_ID = " + prID + " AND ITEM_ID =" + ItemId + " AND IS_ACTIVE = 1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateItemQuantityFromBidSubmitting(int prID, int ItemId, decimal itemQuantity, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".PR_DETAIL SET  ITEM_QUANTITY = " + itemQuantity + "  WHERE PR_ID = " + prID + " AND ITEM_ID =" + ItemId + " AND IS_ACTIVE = 1 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public PrDetailsV2 GetPrDetails(int PrdId, int CompanyId, DBConnection dbConnection)
        {
            PrDetailsV2 PrDetail;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PR_DETAIL AS PD\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON PD.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT PRD_ID, QTY FROM BIDDING_ITEM) AS BI ON BI.PRD_ID = PD.PRD_ID "+
                                             "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS MD ON MD.DETAIL_ID = PD.MEASUREMENT_ID " +
                                             "WHERE PD.PRD_ID = " + PrdId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrDetail = dataAccessObject.GetSingleOject<PrDetailsV2>(dbConnection.dr);
            }

            if (PrDetail != null)
            {

                //getting BOMS of PR Detail
                PrDetail.PrBoms = DAOFactory.CreatePrBomDAOV2().GetPrdBomForEdit(PrDetail.PrdId, dbConnection);
                //getting Replacement Images of PR Detail
                PrDetail.PrReplacementFileUploads = DAOFactory.CreatePrReplacementFileUploadDAOV2().GetPrReplacementFileUploadForEdit(PrDetail.PrdId, dbConnection);
                //getting Standard Images of PR Detail
                PrDetail.PrFileUploads = DAOFactory.CreatePrFileUploadDAOV2().GetPrFileUploadForEdit(PrDetail.PrdId, dbConnection);
                //getting Supportive Docs of PR Detail
                PrDetail.PrSupportiveDocuments = DAOFactory.CreatePrSupportiveDocumentsDAOV2().GetPrSupportiveDocumentsForEdit(PrDetail.PrdId, dbConnection);
            }

            return PrDetail;
        }

        public List<PR_Details> GetPrDetailsByPRid(int PrId, int CompanyId, DBConnection dbConnection)
        {
            List<PR_Details> PrDetail = new List<PR_Details>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM PR_DETAIL AS PD\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON PD.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "WHERE PD.PR_ID = " + PrId + ";";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                PrDetail = dataAccessObject.ReadCollection<PR_Details>(dbConnection.dr);
            }


            return PrDetail;
        }
        
        public int UpdateStatus(int userId, int prdId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='TABAPPRVAL') WHERE PRD_ID = " + prdId + " ";
            dbConnection.cmd.CommandText += "INSERT INTO PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES ("+ prdId + ", (SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='BIDEXPRD'), '" + LocalTime.Now+"', "+ userId + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdatePRStatuss(int userId, int prdId, string DetailStatus, string LogStatus, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='" + DetailStatus + "') WHERE PRD_ID = " + prdId + " ";
            dbConnection.cmd.CommandText += "INSERT INTO PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + prdId + ", (SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='" + LogStatus + "'), '" + LocalTime.Now + "', " + userId + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdatePRStatus(int userId, int prdId, string DetailStatus,  DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='"+DetailStatus+"') WHERE PRD_ID = " + prdId + " ";
            //dbConnection.cmd.CommandText += "INSERT INTO PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + prdId + ", (SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='" + LogStatus + "'), '" + LocalTime.Now + "', " + userId + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }
        public int UpdateLogStatus(int userId, int prdId,  string LogStatus, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='" + DetailStatus + "') WHERE PRD_ID = " + prdId + " ";
            dbConnection.cmd.CommandText += "INSERT INTO PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + prdId + ", (SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='" + LogStatus + "'), '" + LocalTime.Now + "', " + userId + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateterminatedStatus(int userId, int prdId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='TERM') WHERE PRD_ID = " + prdId + " ";
            dbConnection.cmd.CommandText += "INSERT INTO PR_DETAIL_STATUS_LOG (PRD_ID, STATUS, LOGGED_DATE, USER_ID) VALUES (" + prdId + ", (SELECT PR_DETAILS_LOG_ID FROM DEF_PR_DETAILS_LOG WHERE LOG_CODE='TERM'), '" + LocalTime.Now + "', " + userId + ") ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<PrDetailsV2> GetPrDetailsForBidSubmission(int PrId, int CompanyId, int WarehouseId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM PR_DETAIL AS PD\n" +
                                             "INNER JOIN (SELECT ITEM_ID, ITEM_NAME,COMPANY_ID, SUB_CATEGORY_ID, MEASUREMENT_ID FROM ADD_ITEMS WHERE COMPANY_ID=" + CompanyId + ") AS AIM ON PD.ITEM_ID = AIM.ITEM_ID\n" +
                                             "INNER JOIN (SELECT SUB_CATEGORY_ID, SUB_CATEGORY_NAME,COMPANY_ID, CATEGORY_ID FROM ITEM_SUB_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS ISC ON AIM.SUB_CATEGORY_ID = ISC.SUB_CATEGORY_ID\n" +
                                             "INNER JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID=" + CompanyId + ") AS IC ON ISC.CATEGORY_ID = IC.CATEGORY_ID\n" +
                                             "LEFT JOIN (SELECT DETAIL_ID, SHORT_CODE AS MEASUREMENT_SHORT_NAME FROM MEASUREMENT_DETAIL) AS UN ON UN.DETAIL_ID = PD.MEASUREMENT_ID \n" +
                                             "LEFT JOIN (SELECT ITEM_ID, WAREHOUSE_ID, AVAILABLE_QTY FROM WAREHOUSE_INVENTORY_MASTER WHERE WAREHOUSE_ID = "+WarehouseId+ ") AS INM ON INM.ITEM_ID = PD.ITEM_ID \n" +
                                             "LEFT JOIN (SELECT DETAIL_ID AS W_DETAIL_ID, SHORT_CODE AS WAREHOUSE_UNIT FROM MEASUREMENT_DETAIL) AS U ON U.W_DETAIL_ID = AIM.MEASUREMENT_ID \n" +
                                             "WHERE PD.PR_ID = " + PrId + " AND PD.SUBMIT_FOR_BID=0 AND PD.IS_TERMINATED !=1";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PrDetailsV2>(dbConnection.dr);
            }
        }

        public int SavePRDetails(int PrId, int ItemId, int Unit, string ItemDescription, string ItemUpdatedBy, DateTime ItemUpdatedDateTime, int IsActive, int Replacement, decimal ItemQuantity, string Purpose, decimal EstimatedAmount, DBConnection dbConnection) {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".PR_DETAIL WHERE  PR_ID = " + PrId + " AND  ITEM_ID = " + ItemId + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0) {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".PR_DETAIL (PR_ID, ITEM_ID, UNIT, ITEM_DESCRIPTION, ITEM_UPDATED_BY, ITEM_UPDATED_DATETIME, IS_ACTIVE, REPLACEMENT,ITEM_QUANTITY,PURPOSE,SUBMIT_FOR_BID,PR_IS_REJECTED_COUNT,IS_PO_RAISED,IS_PO_REJECTED_COUNT,IS_PO_APPROVED,ESTIMATED_AMOUNT,PR_IS_APPROVED_FOR_BID) VALUES ( " + PrId + ", " + ItemId + " , " + Unit + ", '" + ItemDescription + "', '" + ItemUpdatedBy + "', '" + ItemUpdatedDateTime + "', " + IsActive + ", " + Replacement + "," + ItemQuantity + ",'" + Purpose + "'," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + 0 + "," + EstimatedAmount + "," + 0 + ");";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                return dbConnection.cmd.ExecuteNonQuery();
            }
            else {
                return -1;
            }


        }

        public int UpdatePRStatusFoeCancelledPos(int userId, string DetailStatus, List<int> ItemIds, int PrId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT COUNT(PRD_ID) AS COUNT FROM PR_DETAIL AS PRD " +
                                            "INNER JOIN(SELECT PO_ID, BASED_PR FROM PO_MASTER WHERE IS_CANCELLED = 0 AND IS_APPROVED = 1) AS POM ON POM.BASED_PR = PRD.PR_ID " +
                                            "LEFT JOIN(SELECT PO_ID FROM PO_DETAILS) AS POD ON POD.PO_ID = POM.PO_ID " +
                                            "WHERE PRD.PR_ID = " + PrId + " AND PRD.IS_PO_RAISED = 1 AND PRD.ITEM_ID IN (" + string.Join(",", ItemIds) + ") ";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());

            if (count == 0) {
                dbConnection.cmd.CommandText = "UPDATE PR_DETAIL SET CURRENT_STATUS = (SELECT PR_DETAILS_STATUS_ID FROM DEF_PR_DETAILS_STATUS WHERE STATUS_CODE='" + DetailStatus + "') WHERE PR_ID = "+ PrId + " AND ITEM_ID IN (" + string.Join(",", ItemIds) + ") ";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                
            }
            return dbConnection.cmd.ExecuteNonQuery();
        }
    }
      
    
}
