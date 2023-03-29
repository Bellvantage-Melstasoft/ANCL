using CLibrary.Common;
using CLibrary.Domain;
using CLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLibrary.Controller {
    public interface PhysicalStockverificationDetailsController {
        List<physicalStockVerificationDetails> GetPSVDetails(int PSVMId);

    }

    public class PhysicalStockverificationDetailsControllerImpl : PhysicalStockverificationDetailsController {
        public List<physicalStockVerificationDetails> GetPSVDetails(int PSVMId) {
            DBConnection dbConnection = new DBConnection();
            try {
                PhysicalstockVerificationDetailsDAO physicalstockVerificationDetailsDAO = DAOFactory.createPhysicalstockVerificationDetailsDAO();
                return physicalstockVerificationDetailsDAO.GetPSVDbyPSVMId(PSVMId, dbConnection);
            }
            catch (Exception) {
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
