using CLibrary.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using System.Data;
using System.Data.SqlClient;

namespace CLibrary.Infrastructure
{
    public interface AfterPO_DAO
    {
        DataTable getDateOptions(DBConnection DbConnection);
        DataTable getCustomChargesTypes(DBConnection DbConnection);
        DataTable getChargesTypes(DBConnection DbConnection);
        DataTable getShippingChargesType(DBConnection DbConnection);
        bool SaveAfterPODetails(AfterPO objAfterPO, DBConnection DbConnection);
        List<AfterPO> GetAfterPODetails(DBConnection DbConnection);
        List<AfterPO> GetAfterPODetailsByMonth(DateTime date,DBConnection DbConnection);


    }

    public class AfterPO_DAOImp : AfterPO_DAO
    {
        string dbLibrary = System.Configuration.ConfigurationSettings.AppSettings["dbLibrary"];

        public List<AfterPO> GetAfterPODetails(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select * from  " + dbLibrary + ".AFTER_PO";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AfterPO>(dbConnection.dr);
            }
        }

        public List<AfterPO> GetAfterPODetailsByMonth(DateTime date,DBConnection dbConnection)
        {
            //MONTH(MRNM.CREATED_DATETIME) =" + date.Month + " AND YEAR(MRNM.CREATED_DATETIME)=" + date.Year + " ORDER BY MRNM.CREATED_DATETIME DESC
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select * from  " + dbLibrary + ".AFTER_PO WHERE MONTH(PERFORMANCE_DATE) = " + date.Month + " AND YEAR(PERFORMANCE_DATE) = " + date.Year + "ORDER BY PERFORMANCE_DATE DESC";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;
            using (dbConnection.dr = dbConnection.cmd.ExecuteReader())
            {
                DataAccessObject dataAccessObject = new DataAccessObject();
                return dataAccessObject.ReadCollection<AfterPO>(dbConnection.dr);
            }
        }

        public DataTable getChargesTypes(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select ID,NAME from  " + dbLibrary + ".DEF_CHARGES";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            SqlDataAdapter adap = new SqlDataAdapter(dbConnection.cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return dt;
        }

        public DataTable getCustomChargesTypes(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select ID,NAME from " + dbLibrary + ".DEF_CUSTOM_CHARGES";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            SqlDataAdapter adap = new SqlDataAdapter(dbConnection.cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return dt;
        }

        public DataTable getDateOptions(DBConnection dbConnection)
        {  
                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = "select ID,Name from " + dbLibrary + ".DEF_DATE_AFTER_PO";
                dbConnection.cmd.CommandType = System.Data.CommandType.Text;

                SqlDataAdapter adap = new SqlDataAdapter(dbConnection.cmd);
                DataTable dt = new DataTable();
                adap.Fill(dt);
                return dt;
            
        }

        public DataTable getShippingChargesType(DBConnection dbConnection)
        {
            dbConnection.cmd.Parameters.Clear();
            dbConnection.cmd.CommandText = "select ID,NAME from " + dbLibrary + ".DEF_SHIPPING_CHARGES";
            dbConnection.cmd.CommandType = System.Data.CommandType.Text;

            SqlDataAdapter adap = new SqlDataAdapter(dbConnection.cmd);
            DataTable dt = new DataTable();
            adap.Fill(dt);
            return dt;
        }

        public bool SaveAfterPODetails(AfterPO objAfterPO, DBConnection dbConnection)
        {
            try
            {
                int id = 0;
                dbConnection.cmd.CommandText = "SELECT COUNT (ID) + 1 AS MAXid FROM " + dbLibrary + ".AFTER_PO";
                id = int.Parse(dbConnection.cmd.ExecuteScalar().ToString());

                dbConnection.cmd.Parameters.Clear();
                dbConnection.cmd.CommandText = @"INSERT INTO " + dbLibrary + ".AFTER_PO(ID,HYPER_LOAN,LC_OPENING,LDS,EXPIRY,ETD,ETA,VESSEL,SHIPPING_AGENT,INSURANCE_COMPANY,INSURANCE_DATE,INSURANCE_POLICY_NO,PERFORMANCE_BOND_NO," +
                                                 "PERFORMANCE_DATE,CHARGE_VALUE,INSURANCE_CHARGE,FREIGHT,DUTY,CUSTOM_DUTY,CESS,PAL,VAT,NBT,OTHER_CUSTOM_CHARGES,RANK_CONTAINER,TERMINAL,WEIGHING_CHARGES,SLPA,AGENT_DO_CHARGE,CONTAINER_DEPOSIT,WASHING_CHARGES,CLEARING_AND_TRANSPORT) " +
                                                 "VALUES(" + id + ",'" + objAfterPO.HyperLoan + "','" + objAfterPO.LC_Opening + "','" + objAfterPO.LDS + "','" + objAfterPO.Expiry + "','" + objAfterPO.ETD + "','" + objAfterPO.ETA + "','" + objAfterPO.Vessel + "','" +
                                                 objAfterPO.ShippingAgent + "','" + objAfterPO.InsuranceCompany + "','" + objAfterPO.InsuranceDate + "','" + objAfterPO.PolicyNo + "','" + objAfterPO.PerformanceBondNo + "','" + objAfterPO.PerformanceDate + "'," +
                                                 objAfterPO.ChargeValue + "," + objAfterPO.InsuranceCharge + "," + objAfterPO.Freight + "," + objAfterPO.Duty + "," + objAfterPO.CustomDuty + "," + objAfterPO.CESS + "," + objAfterPO.PAL + "," + objAfterPO.VAT + "," + objAfterPO.NBT + "," +
                                                 +objAfterPO.OtherCustomCharges + "," + objAfterPO.RANK_CONTAINER + "," + objAfterPO.TERMINAL + "," + objAfterPO.WeighingCharges + ",'" + objAfterPO.SLPA + "'," + objAfterPO.AgentDoCharges + "," + objAfterPO.ContainerDeposit + "," + objAfterPO.WashingCharges + "," + objAfterPO.ClearingAndTransportCharges + ")";


                int status = dbConnection.cmd.ExecuteNonQuery();

                if (status > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
        }
    }
}
