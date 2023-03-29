using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;

namespace CLibrary.Infrastructure
{
    public interface CommitteeDAO
    {
        List<Committee> FetchAllCommittee(int companyId , DBConnection dbConnection);
        int SaveCommittee(int id ,string committeeName, DateTime createdDate, int createdUser, int companyId, int CommiteeType, DBConnection dbConnection);
        int UpdateCommittee(int id, string committeeName, DateTime createdDate, int createdUser, int companyId, int CommiteeType, DBConnection dbConnection);
        int DeleteCommittee(int id, DBConnection dbConnection);
        List<CommitteeMember> FetchAllCommitteeMembers(DBConnection dbConnection);
        int SaveCommitteeMember(int id, int committeeid, int designationId, int sequenceNo, int allowedApprovedCount, int canOveride, int overideDesignationId, DateTime effectiveDate, int userId, DateTime now, DBConnection dbConnection);
        int UpdateCommitteeMember(int id, int committeeid, int designationId, int sequenceNo, int allowedApprovedCount, int canOveride, int overideDesignationId, DateTime effectiveDate, int userId, DateTime now, DBConnection dbConnection);
        int DeleteCommitteeMember(int id, DBConnection dbConnection);
        
        int SaveUploadedtechcommitteefiles(List<TecCommitteeFileUpload> details, DBConnection dbConnection);
        List<TecCommitteeFileUpload> Gettechcommitteefiles(int bidId, int tabulationId, string committeeType, DBConnection dbConnection);
        int IsDocsUploaded(int bidId, int tabulationId, string committeeType, DBConnection dbConnection);
        int FetchApprovalLimitCommittee(int limitId, DBConnection dbConnection);
        int GetUploedasequence(int bidId, int tabulationId, string committeeType, DBConnection dbConnection);
    }

    public class CommitteeDAOImpl : CommitteeDAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public int DeleteCommittee(int id, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = " DELETE " + dbLibrary + ".COMMITTEE" +
                                           " WHERE COMMITTEE_ID = " + id + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<Committee> FetchAllCommittee(int companyId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMMITTEE" +
                                           " WHERE COMPANY_ID= " + companyId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<Committee>(dbConnection.dr);
            }
        }

        public List<CommitteeMember> FetchAllCommitteeMembers(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".COMMITTEE_MEMBERS";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<CommitteeMember>(dbConnection.dr);
            }
        }

        public int SaveCommittee(int id, string committeeName, DateTime createdDate, int createdUser, int companyId, int CommiteeType, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = " SELECT COUNT(*) as cnt FROM " + dbLibrary + ".COMMITTEE" +
                                           " WHERE COMMITTEE_NAME = '" + committeeName + "' " +
                                           " AND COMPANY_ID =" + companyId + "";
            if (decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMMITTEE" +
                                               " VALUES ('" + committeeName + "','" + createdDate + "'," + createdUser + " ," + companyId + " , "+ CommiteeType + " )";

            }
            else
            {
                return -1;
            }

            return dbConnection.cmd.ExecuteNonQuery();
        }



        public int UpdateCommittee(int id, string committeeName, DateTime createdDate, int createdUser, int companyId, int CommiteeType, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = " UPDATE " + dbLibrary + ".COMMITTEE" +
                                           " SET COMMITTEE_NAME = '" + committeeName + "' " +
                                           " , CREATED_DATE='" + createdDate + "' " +
                                           " , CREATED_USER ='" + createdUser + "' " +
                                           " , COMMITTEE_TYPE ='" + CommiteeType + "' " +
                                           " WHERE COMMITTEE_ID =" + id + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveCommitteeMember(int id, int committeeid, int designationId, int sequenceNo, int allowedApprovedCount, int canOveride, int overideDesignationId, DateTime effectiveDate, int userId, DateTime now, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = " SELECT COUNT(*) as cnt FROM " + dbLibrary + ".COMMITTEE_MEMBERS" +
                                           " WHERE COMMITTEE_ID = " + committeeid + " " +
                                           " AND DESIGNATION_ID =" + designationId + "";
            if (decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString()) == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".COMMITTEE_MEMBERS" +
                                               " VALUES (" + committeeid + "," + designationId + "," + sequenceNo + "," + allowedApprovedCount + "," + canOveride + " , " + overideDesignationId + ",'" + effectiveDate + "'," + userId + ",'" + now + "')";

            }
            else
            {
                return -1;
            }

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int UpdateCommitteeMember(int id, int committeeid, int designationId, int sequenceNo, int allowedApprovedCount, int canOveride, int overideDesignationId, DateTime effectiveDate, int userId, DateTime now, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = " UPDATE " + dbLibrary + ".COMMITTEE_MEMBERS" +
                                           " SET COMMITTEE_ID = " + committeeid + " " +
                                           " , DESIGNATION_ID = " + designationId + "" +
                                            " , SEQUENCE_OF_APPROVAL = " + sequenceNo + "" +
                                            " , ALLOWED_APPROVAL_COUNT = " + allowedApprovedCount + "" +
                                            " , CAN_OVERIDE = " + canOveride + "" +
                                            " , OVERIDE_DESIGNATION_ID = " + overideDesignationId + "" +
                                            " , EFFECTIVE_DATE = '" + effectiveDate + "'" +
                                            " , ENTERED_USER = " + userId + "" +
                                            " , ENTERED_DATE = '" + now + "'" +
                                            " WHERE ID =" + id + "";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteCommitteeMember(int id, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = " DELETE " + dbLibrary + ".COMMITTEE_MEMBERS" +
                                           " WHERE ID = " + id + " ";
            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int SaveUploadedtechcommitteefiles(List<TecCommitteeFileUpload> details, DBConnection dbConnection)
        {
            int count = 0;
            foreach (var item in details)
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".TEC_COMMITTEE_FILE_UPLOAD (BID_ID, TABULATION_ID, SEQ_ID, FILE_PATH, FILE_NAME,COMMITTEE_TYPE) VALUES ( @BID_ID, @TABULATION_ID, @SEQ_ID, @FILE_PATH, @FILE_NAME,@COMMITTEE_TYPE);";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.Parameters.AddWithValue("@BID_ID", item.BidId);
                dbConnection.cmd.Parameters.AddWithValue("@TABULATION_ID", item.tabulationId);
                dbConnection.cmd.Parameters.AddWithValue("@SEQ_ID", item.sequenceId);
                dbConnection.cmd.Parameters.AddWithValue("@FILE_PATH", item.filepath);
                dbConnection.cmd.Parameters.AddWithValue("@FILE_NAME", item.filename);
                dbConnection.cmd.Parameters.AddWithValue("@COMMITTEE_TYPE", item.commiteetype);
                var Issaved = dbConnection.cmd.ExecuteNonQuery();
                if (Issaved > 0)
                {
                    count = ++count;
                }

            }

            if (details.Count == count)
            {
                if (details.FirstOrDefault().commiteetype == "T")
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".TABULATION_MASTER SET IS_RECOMMENDATION_DOC_UPLOADED = 1 WHERE BID_ID=" + details.FirstOrDefault().BidId + " AND TABULATION_ID=" + details.FirstOrDefault().tabulationId + "  ;";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    var Issaved = dbConnection.cmd.ExecuteNonQuery();
                    if (Issaved > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    dbConnection.cmd.Parameters.Clear();
                    dbConnection.cmd.CommandText = "UPDATE  " + dbLibrary + ".TABULATION_MASTER SET IS_APPROVAL_DOC_UPLOADED = 1 WHERE BID_ID=" + details.FirstOrDefault().BidId + " AND TABULATION_ID=" + details.FirstOrDefault().tabulationId + "  ;";
                    dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                    var Issaved = dbConnection.cmd.ExecuteNonQuery();
                    if (Issaved > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {


                return 0;

            }
               

        }

        public List<TecCommitteeFileUpload> Gettechcommitteefiles(int bidId, int tabulationId,string committeeType, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".TEC_COMMITTEE_FILE_UPLOAD WHERE BID_ID=" + bidId + " AND TABULATION_ID=" + tabulationId + " AND COMMITTEE_TYPE ='" + committeeType+"'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<TecCommitteeFileUpload>(dbConnection.dr);
            }


        }

        public int IsDocsUploaded(int bidId, int tabulationId, string committeeType,DBConnection dbConnection)
        {
            if (committeeType == "T")
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT IS_RECOMMENDATION_DOC_UPLOADED FROM  " + dbLibrary + ".TABULATION_MASTER WHERE BID_ID=" + bidId + " AND TABULATION_ID=" + tabulationId + "  ;";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                var Issaved = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (Issaved > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            else
            {
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "SELECT IS_APPROVAL_DOC_UPLOADED FROM  " + dbLibrary + ".TABULATION_MASTER WHERE BID_ID=" + bidId + " AND TABULATION_ID=" + tabulationId + "  ;";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                var Issaved = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());
                if (Issaved > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int GetUploedasequence(int bidId, int tabulationId, string committeeType, DBConnection dbConnection)
        {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT MAX(SEQ_ID) FROM " + dbLibrary + ".TEC_COMMITTEE_FILE_UPLOAD WHERE BID_ID=" + bidId + " AND TABULATION_ID=" + tabulationId + " AND COMMITTEE_TYPE ='" + committeeType + "'";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            return int.Parse(dbConnection.cmd.ExecuteScalar().ToString());


        }

        public int FetchApprovalLimitCommittee(int limitId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            int committeeId = 0;
            dbConnection.cmd.CommandText = "SELECT COMMITTEE_ID FROM " + dbLibrary + ".APPROVAL_LIMIT_COMMITTEE" +
                                           " WHERE APPROVAL_LIMIT_ID= " + limitId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            if (dbConnection.cmd.ExecuteScalar() != null)
            {
                committeeId = Convert.ToInt32(dbConnection.cmd.ExecuteScalar().ToString());
            }
            return committeeId;

        }
    }
  
}
