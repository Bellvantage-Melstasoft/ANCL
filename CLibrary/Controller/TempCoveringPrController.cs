using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {

    public interface TempCoveringPrController {
        int AddCoveringPrData(List<TempCoverigPr> PrDetails, int ParentPrId, int PoId);
        int DeleteCoveringPrData(int ParentPrId);
    }

    public class TempCoveringPrControllerImpl : TempCoveringPrController {

        public int AddCoveringPrData(List<TempCoverigPr> PrDetails, int ParentPrId, int PoId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TempCoveringPrDAO tempCoverigPr = DAOFactory.CreateTempCoveringPrDAO();
                return tempCoverigPr.AddCoveringPrData(PrDetails, ParentPrId, PoId, dbConnection);
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

        public int DeleteCoveringPrData( int ParentPrId) {
            DBConnection dbConnection = new DBConnection();
            try {
                TempCoveringPrDAO tempCoverigPr = DAOFactory.CreateTempCoveringPrDAO();
                return tempCoverigPr.DeleteCoveringPrData( ParentPrId , dbConnection);
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
