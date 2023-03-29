using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;


namespace CLibrary.Infrastructure
{
    public interface DashboardDAO
    {
        List<int> GetCountForDashboard(int companyId, int yearsearch,int DesignationId, DBConnection dbConnection);
        List<MrnMaster> fetchMrnList(int subDepartmentID, DBConnection dbConnection);
        List<PR_Master> FetchApprovePRDataByDeptId(int departmentId, DBConnection dbConnection);
        List<PR_Master> GetPrListForBidSubmission(int companyId, DBConnection dbConnection);
        List<PR_Master> GetPrListForBidApproval(int companyId, DBConnection dbConnection);
        List<POMaster> GetPoMasterListByDepartmentId(int departmentid, DBConnection dbConnection);
    }

    public class DashboardDAOImpl : DashboardDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<PR_Master> FetchApprovePRDataByDeptId(int departmentId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT Top 5 * FROM " + dbLibrary + ".PR_MASTER PM\n" +
                 "INNER JOIN (SELECT SUB_DEPARTMENT_ID, USER_ID FROM COMPANY_LOGIN ) AS CL ON PM.CREATED_BY = CL.USER_ID\n" +
                 "INNER JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON CL.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                " WHERE PM.IS_ACTIVE = '1' AND PM.DEPARTMENT_ID=" + departmentId + " AND PM.PR_IS_APPROVED= 0 ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public List<MrnMaster> fetchMrnList(int subDepartmentID, DBConnection dbConnection)
        {
            List<MrnMaster> mrnMasters = new List<MrnMaster>();
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT Top 5 * FROM " + dbLibrary + ".MRN_MASTER AS MRNM " +
                                           "INNER JOIN (SELECT USER_ID,USER_NAME,FIRST_NAME FROM " + dbLibrary + ".COMPANY_LOGIN) AS COLOG ON MRNM.CREATED_BY=COLOG.USER_ID " +
                                           "INNER JOIN (SELECT SUB_DEPARTMENT_ID,DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT) AS SUBDEP ON MRNM.SUB_DEPARTMENT_ID= SUBDEP.SUB_DEPARTMENT_ID " +
                                           "WHERE MRNM.SUB_DEPARTMENT_ID=" + subDepartmentID + " AND MRNM.IS_ACTIVE=1  AND MRNM.IS_MRN_APPROVED = 0" +
                                           " ORDER BY MRNM.CREATED_DATETIME DESC";
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                mrnMasters = dataAccessObject.ReadCollection<MrnMaster>(dbConnection.dr);
            }
            return mrnMasters;
        }

        public List<int> GetCountForDashboard(int CompanyId, int yearsearch, int DesignationId, DBConnection dbConnection)
        {
            List<int> Count = new List<int>();
            //noOfCompanyUsers
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM COMPANY_LOGIN WHERE  (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ")  AND DEPARTMENT_ID=" + CompanyId;

            Count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            //noOfSupplier
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM " + dbLibrary + ".SUPPLIER_ASSIGNED_TO_COMPANY  WHERE  (DATEPART(yyyy, REQUSETED_DATE)=" + yearsearch + ") AND COMPANY_ID = " + CompanyId + "";

            Count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            //noOfPendingQuotationApproval
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM  " + dbLibrary + ".BID_APPROVAL WHERE   (DATEPART(yyyy, ENTERED_DATE)=" + yearsearch + ")  AND DESIGNATION_ID = " + DesignationId + " " +
                                             " AND IS_APPROVED = 0  AND BID_ID IN (SELECT BID_ID FROM  " + dbLibrary + ".BIDDING WHERE IS_QUOTATION_APPROVED = 0 )";

            Count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            //noOfPendingPOApproval
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM " + dbLibrary + ".PO_APPROVAL WHERE (DATEPART(yyyy, ENTERED_DATE)=" + yearsearch + ") AND IS_APPROVED=0 AND DESIGNATION_ID = " + DesignationId + "";

            Count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));

            //successTransaction
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT COUNT(*) FROM " + dbLibrary + ".GRN_MASTER WHERE (DATEPART(yyyy, CREATED_DATE)=" + yearsearch + ") AND IS_APPROVED=1";

            Count.Add(int.Parse(dbConnection.cmd.ExecuteScalar().ToString()));


            return Count;
        }

        public List<POMaster> GetPoMasterListByDepartmentId(int departmentid, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " SELECT Top 5 POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY ,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME,COUNT(POD.ITEM_ID) AS ITEMCOUNT, POM.IS_APPROVED,SD.DEPARTMENT_NAME,PM.QUOTATION_FOR " +
                                           " FROM " + dbLibrary + ".PO_MASTER  AS POM " +
                                           "INNER JOIN " + dbLibrary + ".PR_MASTER AS PM ON PM.PR_ID =POM.BASED_PR  " +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PM.MRNREFERENCE_NO = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                           " INNER JOIN " + dbLibrary + ".PO_DETAILS AS POD ON POD.PO_ID =POM.PO_ID  " +
                                           " INNER JOIN " + dbLibrary + ".SUPPLIER AS SU ON SU.SUPPLIER_ID =POM.SUPPLIER_ID  " +
                                            "INNER JOIN (SELECT USER_ID,USER_NAME AS CREATED_BY FROM " + dbLibrary + ".COMPANY_LOGIN) AS CL ON POM.CREATED_BY=CL.USER_ID\n" +
                                           " WHERE POM.DEPARTMENT_ID =" + departmentid + "  " +
                                           " AND POD.IS_PO_RAISED =1 AND POM.IS_APPROVED = 0" +
                                           " GROUP BY POM.PO_ID,POM.PO_CODE,POM.CREATED_DATE,CL.CREATED_BY ,PM.PR_CODE,POM.BASED_PR,SU.SUPPLIER_NAME ,POM.IS_APPROVED,SD.DEPARTMENT_NAME,PM.QUOTATION_FOR";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<POMaster>(dbConnection.dr);
            }
        }

        public List<PR_Master> GetPrListForBidApproval(int companyId, DBConnection dbConnection)
        {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT Top 5 * FROM " + dbLibrary + ".PR_MASTER AS PM\n" +
                                           " LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM " + dbLibrary + ".MRN_MASTER ) AS MRM "+
                                           " ON PM.MRNREFERENCE_NO = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                           //"INNER JOIN (SELECT SUB_DEPARTMENT_ID, USER_ID FROM COMPANY_LOGIN ) AS CL ON PM.CREATED_BY = CL.USER_ID\n" +
                                           "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM " + dbLibrary + ".SUB_DEPARTMENT ) AS SD "+
                                           " ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                           "WHERE PM.DEPARTMENT_ID = " + companyId + " AND\n" +
                                           "PM.PR_ID IN (SELECT PR_ID FROM " + dbLibrary + ".BIDDING WHERE IS_APPROVED = 0 GROUP BY PR_ID)";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }

        public List<PR_Master> GetPrListForBidSubmission(int companyId, DBConnection dbConnection)
        {
            //getting PR Masters
            dbConnection.cmd.Parameters.Clear();
            //dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".PR_MASTER AS PM\n" +
            //                                "LEFT JOIN (SELECT CATEGORY_ID,COMPANY_ID, CATEGORY_NAME FROM " + dbLibrary + ".ITEM_CATEGORY WHERE COMPANY_ID=" + companyId + ") AS IC ON PM.ITEM_CATEGORY_ID = IC.CATEGORY_ID\n" +
            //                                // "INNER JOIN (SELECT SUB_DEPARTMENT_ID, USER_ID,FIRST_NAME AS STORE_KEEPER_NAME FROM " + dbLibrary + ".COMPANY_LOGIN ) AS CL ON PM.STORE_KEEPER_ID = CL.USER_ID\n" +
            //                                "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PM.MRNREFERENCE_NO = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
            //                               "LEFT JOIN (SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
            //                               " INNER JOIN " + dbLibrary + ".PR_EXPENSE AS PREX ON PM.PR_ID = PREX.PR_ID " +
            //                               "WHERE PM.DEPARTMENT_ID = " + companyId + " AND PM.PR_IS_APPROVED= 1 AND PM.IS_ACTIVE = 1 AND PREX.IS_APPROVED=1 AND\n" +
            //                                "PM.PR_ID IN (SELECT PR_ID FROM PR_DETAIL WHERE SUBMIT_FOR_BID = 0 GROUP BY PR_ID)";

            dbConnection.cmd.CommandText = "SELECT * FROM PR_MASTER AS PM\n" +
                                             "LEFT JOIN(SELECT CATEGORY_ID, COMPANY_ID, CATEGORY_NAME FROM ITEM_CATEGORY WHERE COMPANY_ID = 6) AS IC ON PM.PR_CATEGORY_ID = IC.CATEGORY_ID\n" +
                                            "LEFT JOIN(SELECT SUB_DEPARTMENT_ID, MRN_ID FROM MRN_MASTER ) AS MRM ON PM.MRN_ID = (SELECT CONVERT(varchar(10), MRM.MRN_ID))\n" +
                                            "LEFT JOIN(SELECT SUB_DEPARTMENT_ID, DEPARTMENT_NAME FROM SUB_DEPARTMENT ) AS SD ON MRM.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID\n" +
                                            "INNER JOIN PR_EXPENSE AS PREX ON PM.PR_ID = PREX.PR_ID WHERE PM.COMPANY_ID = 6 AND PM.IS_PR_APPROVED = 1 AND PM.IS_ACTIVE = 1 AND PREX.IS_APPROVED = 1 AND\n" +
                                            "PM.PR_ID IN(SELECT PR_ID FROM PR_DETAIL WHERE SUBMIT_FOR_BID = 0 GROUP BY PR_ID)\n";



            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<PR_Master>(dbConnection.dr);
            }
        }
    }
}
