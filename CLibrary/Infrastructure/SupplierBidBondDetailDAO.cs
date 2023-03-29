using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using CLibrary.Common;
using CLibrary.Domain;

namespace CLibrary.Infrastructue
{
   public interface SupplierBidBondDetailsDAO
    {
        int saveSupplierBidBondDetails(SupplierBidBondDetails supplierBidBondDetails, DBConnection dbConnection);
        SupplierBidBondDetails getSupplierBidBondDetails(int bidId, int supplierId, DBConnection dbConnection);
        void updateSupplierBidBondDetails(SupplierBidBondDetails model, DBConnection dbConnection);
        List<SupplierBidBondDetails> getSupplierBidBondDetailsByBidId(int bidId, DBConnection dbConnection);
    }

    public class SupplierBidBondDetailsDAOImpl : SupplierBidBondDetailsDAO
    {

        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public SupplierBidBondDetails getSupplierBidBondDetails(int bidId, int supplierId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_BID_BOND_DETAILS WHERE BID_ID = " + bidId + " AND SUPPLIER_ID= " + supplierId + "";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.GetSingleOject<SupplierBidBondDetails>(dbConnection.dr);
            }
        }

        public List<SupplierBidBondDetails> getSupplierBidBondDetailsByBidId(int bidId, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandText = "SELECT * FROM " + dbLibrary + ".SUPPLIER_BID_BOND_DETAILS WHERE BID_ID = " + bidId + " ";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<SupplierBidBondDetails>(dbConnection.dr);
            }
        }

        public int saveSupplierBidBondDetails(SupplierBidBondDetails supplierBidBondDetails, DBConnection dbConnection)
        {
            dbConnection.cmd.CommandText = "SELECT COUNT(*) AS cnt FROM " + dbLibrary + ".SUPPLIER_BID_BOND_DETAILS WHERE BID_ID = " + supplierBidBondDetails.Bid_Id + " AND SUPPLIER_ID = " + supplierBidBondDetails.Supplier_Id + "";
            var count = decimal.Parse(dbConnection.cmd.ExecuteScalar().ToString());
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            if (count == 0)
            {
                dbConnection.cmd.CommandText = "INSERT INTO " + dbLibrary + ".SUPPLIER_BID_BOND_DETAILS (BID_ID , SUPPLIER_ID , BOND_NO ,BANK,BOND_AMOUNT , EXPIRE_DATE_OF_BOND,RECEIPT_NO)"+
                                               " VALUES (" + supplierBidBondDetails.Bid_Id + "," + supplierBidBondDetails.Supplier_Id + ",'" + supplierBidBondDetails.Bond_No + "', '" + supplierBidBondDetails.Bank + "' ," + supplierBidBondDetails.Bond_Amount + ",'" + supplierBidBondDetails.Expire_Date_Of_Bond + "', '"+ supplierBidBondDetails.Receipt_No + "')";
                return dbConnection.cmd.ExecuteNonQuery();

            }
            else
            {
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;
                dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_BID_BOND_DETAILS " +
                                               " SET BOND_NO = '" + supplierBidBondDetails.Bond_No + "' " +
                                               " , BANK = '" + supplierBidBondDetails.Bank + "' " +
                                               " , BOND_AMOUNT = " + supplierBidBondDetails.Bond_Amount + " " +
                                               " , EXPIRE_DATE_OF_BOND = '" + supplierBidBondDetails.Expire_Date_Of_Bond + "'" +
                                               " , RECEIPT_NO = '" + supplierBidBondDetails.Receipt_No + "' " +
                                               " WHERE BID_ID = " + supplierBidBondDetails.Bid_Id + " AND SUPPLIER_ID= " + supplierBidBondDetails.Supplier_Id + "";
               return  dbConnection.cmd.ExecuteNonQuery();
            }
        }

        public void updateSupplierBidBondDetails(SupplierBidBondDetails model, DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();

            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            dbConnection.cmd.CommandText = "UPDATE " + dbLibrary + ".SUPPLIER_BID_BOND_DETAILS " +
                                           " SET BOND_NO = '"  + model.Bond_No + "' " +
                                           " , BANK = '" + model.Bank + "' " +
                                           " , BOND_AMOUNT = "+ model.Bond_Amount +" " +
                                           " , EXPIRE_DATE_OF_BOND = '" + model.Expire_Date_Of_Bond + "'" +
                                           " , RECEIPT_NO = '" + model.Receipt_No + "' "  +
                                           " WHERE BID_ID = " + model.Bid_Id + " AND SUPPLIER_ID= " + model.Supplier_Id + "";
            dbConnection.cmd.ExecuteNonQuery();
        }
    }

    
}
