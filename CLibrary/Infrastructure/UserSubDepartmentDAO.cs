using CLibrary.Common;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Infrastructure {
    public interface UserSubDepartmentDAO {

        List<UserSubDepartment> getUserSubDepartmentdetails(int UserId, DBConnection dbConnection);
        List<UserSubDepartment> getDepartmentHeads(int subDepartmentId, DBConnection dbConnection);
        List<UserSubDepartment> DepartmentDetails(int subDepartmentId, DBConnection dbConnection);
        List<UserSubDepartment> GetDepartmentHeadsByDepartmentId(int DepartmentId, DBConnection dbConnection);
        
    }
    public class UserSubDepartmentDAOSQLImpl : UserSubDepartmentDAO {
        public List<UserSubDepartment> getUserSubDepartmentdetails(int UserId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT USD.*,SD.DEPARTMENT_NAME FROM USER_SUB_DEPARTMENT AS USD INNER JOIN SUB_DEPARTMENT AS SD ON USD.SUB_DEPARTMENT_ID= SD.SUB_DEPARTMENT_ID WHERE USD.USER_ID =" + UserId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserSubDepartment>(dbConnection.dr);
            }
        }

        public List<UserSubDepartment> getDepartmentHeads(int subDepartmentId, DBConnection dbConnection) {

            List<UserSubDepartment> heads;

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT SUB.*, CL.FIRST_NAME FROM USER_SUB_DEPARTMENT AS SUB "+
                "INNER JOIN COMPANY_LOGIN AS CL ON CL.USER_ID = SUB.USER_ID "+
                "WHERE SUB.SUB_DEPARTMENT_ID = " +subDepartmentId+ " AND IS_HEAD = 1 ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                heads =  dataAccessObject.ReadCollection<UserSubDepartment>(dbConnection.dr);

                return heads;
            }
        }


        public List<UserSubDepartment> DepartmentDetails(int subDepartmentId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT * FROM USER_SUB_DEPARTMENT AS SUB " +
                "INNER JOIN SUB_DEPARTMENT AS SD ON SUB.SUB_DEPARTMENT_ID = SD.SUB_DEPARTMENT_ID " +
                "WHERE SUB.SUB_DEPARTMENT_ID = " + subDepartmentId + " ";

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserSubDepartment>(dbConnection.dr);

               
            }
        }

        public List<UserSubDepartment> GetDepartmentHeadsByDepartmentId(int DepartmentId, DBConnection dbConnection) {

            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "SELECT U.USER_ID, CL.FIRST_NAME FROM USER_SUB_DEPARTMENT AS U " +
                                            "INNER JOIN COMPANY_LOGIN AS CL ON CL.USER_ID = U.USER_ID " +
                                            "WHERE U.IS_HEAD = 1 AND U.SUB_DEPARTMENT_ID = "+ DepartmentId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader()) {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<UserSubDepartment>(dbConnection.dr);
            }
        }


    


}
}

