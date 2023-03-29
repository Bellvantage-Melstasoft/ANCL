using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLibrary.Domain;
using CLibrary.Common;
using CLibrary.Infrastructure;

namespace CLibrary.Controller {
    public interface GrndReturnNoteController {
        int UpdateReturnGrnNote(List<GrnReturnDetails> GrnReturnDetailslist, string Remark, int UserId, int SupplierId, int WarehouseId, decimal SumSubTotal, decimal SumVatTotal, decimal SumNetTotal, int GrnId, int SupplierreturnOption);
    }

    public class GrndReturnNoteControllerImpl : GrndReturnNoteController {

        public int UpdateReturnGrnNote(List<GrnReturnDetails> GrnReturnDetailslist, string Remark, int UserId, int SupplierId, int WarehouseId, decimal SumSubTotal, decimal SumVatTotal, decimal SumNetTotal, int GrnId, int SupplierreturnOption) {
            DBConnection dbConnection = new DBConnection();
            try {

                GrnReturnMasterDAO grnReturnMasterDAO = DAOFactory.CreateGrnReturnMasterDAO();
                GrnReturnDetailsDAO GrnReturnDetailsDAO = DAOFactory.CreateGrnReturnDetailsDAO();

                int Result = grnReturnMasterDAO.SaveReturnMasterDetails(Remark, UserId, WarehouseId, SumSubTotal, SumVatTotal, SumNetTotal, GrnId, SupplierId, SupplierreturnOption, dbConnection);
               
                if (Result > 0) {
                    if (GrnReturnDetailslist.Count > 0) {
                        for (int i = 0; i < GrnReturnDetailslist.Count; i++) {
                            Result = GrnReturnDetailsDAO.SaveGrnDetails(GrnId, GrnReturnDetailslist[i].GrndId, GrnReturnDetailslist[i].ItemId, GrnReturnDetailslist[i].MeasurementId, GrnReturnDetailslist[i].ReturnedQty, GrnReturnDetailslist[i].SubTotal, GrnReturnDetailslist[i].VatValue, GrnReturnDetailslist[i].NetTotal, GrnReturnDetailslist[i].UnitPrice,  dbConnection);

                            if (GrnReturnDetailslist[i].IsApproved == 0) {
                                Result = GrnReturnDetailsDAO.UpdateWaitingQty(GrnReturnDetailslist[i].ItemId, GrnReturnDetailslist[i].podId, GrnReturnDetailslist[i].ReturnedQty, dbConnection);
                            }
                            else if (GrnReturnDetailslist[i].IsApproved == 1) {
                                Result = GrnReturnDetailsDAO.UpdateReceivedQty(GrnReturnDetailslist[i].ItemId, GrnReturnDetailslist[i].podId, GrnReturnDetailslist[i].ReturnedQty, dbConnection);

                            }
                        }
                       
                    }
                    return 1;
                }
                else {
                    dbConnection.RollBack();
                    return -1;
                }
              
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
