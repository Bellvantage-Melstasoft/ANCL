using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface TempQuotatioForCoveringPrController {
        int AddQuotationData(List<TempQuotationsForCoverinPr> QuotatioDetails, int ParentPrId);
        int DeleteQuotationData(int ParentPrId);
    }
    public class TempQuotatioForCoveringPrControllerImpl : TempQuotatioForCoveringPrController {

        public int AddQuotationData(List<TempQuotationsForCoverinPr> QuotatioDetails, int ParentPrId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TempQuotationForCoverigPrDAO tempCoverigPr = DAOFactory.CreateTempQuotationForCoverigPrDAO();
                return tempCoverigPr.AddQuotationData(QuotatioDetails, ParentPrId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }

        public int DeleteQuotationData(int ParentPrId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TempQuotationForCoverigPrDAO tempCoverigPr = DAOFactory.CreateTempQuotationForCoverigPrDAO();
                return tempCoverigPr.DeleteQuotationData( ParentPrId, dbConnection);
            }
            catch (Exception ex) {
                dbConnection.RollBack();
                throw;
            }
            finally {
                if (dbConnection.con.State == System.Data.ConnectionState.Open) {
                    dbConnection.Commit();

                }
            }
        }
    }
    }
