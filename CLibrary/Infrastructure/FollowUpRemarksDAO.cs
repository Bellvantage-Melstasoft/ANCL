using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructure {
    public interface FollowUpRemarksDAO {
        int SaveRemark(int PoId, int UserId, string Remark, DBConnection dbConnection);
        List<FollowUpRemark> GetRemarks(int PoId, int CompanyId, DBConnection dbConnection);
        int DeleteRemark(int RemarkId, DBConnection dbConnection);
    }

    public class FollowUpRemarksDAOImpl : FollowUpRemarksDAO {

        public int SaveRemark(int PoId, int UserId, string Remark, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "  INSERT INTO FOLLOW_UP_REMARKS ([PO_ID], [REMARK], [USER_ID], [REMARK_DATE], [IS_ACTIVE], [DELETED_USER]) VALUES (" + PoId + ", '" + Remark + "', " + UserId + ", '" + LocalTime.Now + "', 1, 0) ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public int DeleteRemark(int RemarkId, DBConnection dbConnection) {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = " UPDATE FOLLOW_UP_REMARKS SET IS_ACTIVE = 0 WHERE ID = "+ RemarkId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            return dbConnection.cmd.ExecuteNonQuery();
        }

        public List<FollowUpRemark> GetRemarks(int PoId, int CompanyId, DBConnection dbConnection) {
           
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM FOLLOW_UP_REMARKS AS FM "+
                                                "INNER JOIN(SELECT USER_ID, DEPARTMENT_ID, FIRST_NAME AS USER_NAME FROM COMPANY_LOGIN) AS CL ON CL.USER_ID = FM.USER_ID "+
                                                "WHERE FM.PO_ID = " + PoId + " AND FM.IS_ACTIVE = 1 AND CL.DEPARTMENT_ID ="+ CompanyId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<FollowUpRemark>(dbConnection.dr);
            }
        }
    }
    }
