using CLibrary.Common;
using CLibrary.Controller;
using CLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace BiddingSystem.Helpers
{
    public static class TabulationCommon
    {
        public  static void LoadBidItemsToGrid(int BidItemId, GridView gridView, Bidding bid)
        {
            List<SupplierQuotationItem> quotationItems = new List<SupplierQuotationItem>();
             List<BiddingItem> items = bid.BiddingItems;

            DataTable dt = new DataTable();

            dt.Columns.Add("QuotationItemId");
            dt.Columns.Add("QuotationId");
            dt.Columns.Add("BiddingItemId");
            dt.Columns.Add("SupplierId");
            dt.Columns.Add("SupplierName");
            dt.Columns.Add("Ratings");
            dt.Columns.Add("Description");
            dt.Columns.Add("UnitPrice");
            dt.Columns.Add("SubTotal");
            dt.Columns.Add("NbtAmount");
            dt.Columns.Add("VatAmount");
            dt.Columns.Add("NetTotal");
            dt.Columns.Add("SpecComply");
            dt.Columns.Add("SupplierMentionedItemName");
            dt.Columns.Add("Actions");
            dt.Columns.Add("ShowReject");
            dt.Columns.Add("IsBidItemSelected");
            dt.Columns.Add("IsSelected");
            dt.Columns.Add("ActualPrice");

            dt.Columns.Add("SubTotal_Sup");
            dt.Columns.Add("Nbt_sup");
            dt.Columns.Add("vat_sup");
            dt.Columns.Add("NetTotal_sup");


            bid.SupplierQuotations.ForEach(
                sq => quotationItems.AddRange(sq.QuotationItems.Where(sqi => sqi.BiddingItemId == BidItemId && sqi.IsSelected != 2)));

            quotationItems = quotationItems.OrderBy(q => q.UnitPrice).ToList();

            for (int i = 0; i < quotationItems.Count; i++)
            {
                decimal SubTotal_Sup = 0;
                decimal Nbt_sup = 0;
                decimal vat_sup = 0;
                decimal NetTotal_sup = 0;
               
                SupplierQuotation quotation = bid.SupplierQuotations.Find(sq => sq.QuotationId == quotationItems[i].QuotationId);

                for(int j = 0; j < bid.SupplierQuotations[i].QuotationItems.Count; j++)
                {
                    if(quotationItems[i].QuotationId == bid.SupplierQuotations[i].QuotationItems[j].QuotationId)
                    {
                        decimal sub_total = decimal.Parse(bid.SupplierQuotations[i].QuotationItems[j].SubTotal.ToString());
                        decimal nbt = decimal.Parse(bid.SupplierQuotations[i].QuotationItems[j].NbtAmount.ToString());
                        decimal vat = decimal.Parse(bid.SupplierQuotations[i].QuotationItems[j].VatAmount.ToString());

                        SubTotal_Sup += sub_total;
                        Nbt_sup += nbt;
                        vat_sup += vat;

                        decimal TempNet = sub_total + nbt + vat;
                        NetTotal_sup += TempNet;

                    }
                }


                DataRow newRow = dt.NewRow();

                newRow["SubTotal_Sup"] = SubTotal_Sup.ToString("#,0.00");
                newRow["Nbt_sup"] = Nbt_sup.ToString("#,0.00");
                newRow["vat_sup"] = vat_sup.ToString("#,0.00");
                newRow["NetTotal_sup"] = NetTotal_sup.ToString("#,0.00");


                newRow["QuotationItemId"] = quotationItems[i].QuotationItemId;
                newRow["QuotationId"] = quotationItems[i].QuotationId;
                newRow["BiddingItemId"] = quotationItems[i].BiddingItemId;
                newRow["SupplierId"] = quotation.SupplierId;
                newRow["SupplierName"] = quotation.SupplierName;

                if (quotation.SupplierRating.Rating == 0)
                    newRow["Ratings"] = "☆☆☆☆☆";
                else if (quotation.SupplierRating.Rating == 1)
                    newRow["Ratings"] = "★☆☆☆☆";
                else if (quotation.SupplierRating.Rating == 2)
                    newRow["Ratings"] = "★★☆☆☆";
                else if (quotation.SupplierRating.Rating == 3)
                    newRow["Ratings"] = "★★★☆☆";
                else if (quotation.SupplierRating.Rating == 4)
                    newRow["Ratings"] = "★★★★☆";
                else if (quotation.SupplierRating.Rating == 5)
                    newRow["Ratings"] = "★★★★★";

                newRow["Description"] = quotationItems[i].Description;
                newRow["UnitPrice"] = quotationItems[i].UnitPrice.ToString("#,0.00");
                newRow["SubTotal"] = quotationItems[i].SubTotal.ToString("#,0.00");
                newRow["NbtAmount"] = quotationItems[i].NbtAmount.ToString("#,0.00");
                newRow["VatAmount"] = quotationItems[i].VatAmount.ToString("#,0.00");
                newRow["NetTotal"] = quotationItems[i].TotalAmount.ToString("#,0.00");
                newRow["ActualPrice"] = quotationItems[i].ActualPrice.ToString("#,0.00");

                List<SupplierBOM> boms = quotationItems[i].SupplierBOMs;

                if (boms == null || (boms != null && boms.Count == 0))
                {
                    newRow["SpecComply"] = "No Specs";
                }
                else if (boms.Count == boms.Count(b => b.Comply == 1))
                {
                    newRow["SpecComply"] = "Yes";
                }
                else if (boms.Count == boms.Count(b => b.Comply == 0))
                {
                    newRow["SpecComply"] = "No";
                }
                else
                {
                    newRow["SpecComply"] = "Some";
                }
                newRow["SupplierMentionedItemName"] = quotationItems[i].SupplierMentionedItemName;

                newRow["IsBidItemSelected"] = bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1 ? "1" : "0";
                newRow["IsSelected"] = quotationItems[i].IsSelected == 1 ? "1" : "0";

                if (i == 0)
                {
                    if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                        newRow["Actions"] = "0";
                    else
                        newRow["Actions"] = "1";
                }
                else
                {
                    newRow["Actions"] = "0";
                }

                if (quotationItems.Count == 1)
                {
                    newRow["ShowReject"] = "0";
                }
                else
                {
                    if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsQuotationSelected == 1)
                        newRow["ShowReject"] = "0";
                    else
                        newRow["ShowReject"] = "1";
                }

                if (bid.BiddingItems.Find(bi => bi.BiddingItemId == BidItemId).IsTerminated == 1)
                {
                    newRow["Actions"] = "0";
                    newRow["ShowReject"] = "0";

                }

                dt.Rows.Add(newRow);

            }

            gridView.DataSource = dt;
            gridView.DataBind();
        }


        public static  List<BiddingItem> GetBidItems(Bidding bid)
        {
            
            List<BiddingItem> items = bid.BiddingItems;

            //required when reviewing quotations
            items.ForEach(itms => itms.QuotationCount = 0);

            for (int i = 0; i < items.Count; i++)
            {

                for (int j = 0; j < bid.SupplierQuotations.Count; j++)
                {
                    if ((bid.SupplierQuotations[j].QuotationItems.FirstOrDefault(sqi => sqi.BiddingItemId == items[i].BiddingItemId)) != null)
                    {
                        items[i].QuotationCount += 1;


                        for (int x = 0; x < bid.SupplierQuotations[j].QuotationItems.Count; x++)
                        {
                            if (bid.SupplierQuotations[j].QuotationItems[x].QuotationId == bid.SupplierQuotations[j].QuotationId )
                            {
                                items[i].SupplierMentionedItemName = bid.SupplierQuotations[j].QuotationItems[x].SupplierMentionedItemName;
                                items[i].QuotationItemId = bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId;
                                items[i].Description = bid.SupplierQuotations[j].QuotationItems[x].Description;

                               
                              
                            }

                        }
                    }

                }

            }

            return items;
        }


        public static List<BiddingItem> GetBidItemsSupplier(Bidding bid,int QuotationId, int TabID)
        {
            
            List<BiddingItem> items = bid.BiddingItems;
            TabulationDetailController tabulationDetailController = ControllerFactory.CreateTabulationDetailController();

            decimal SubTotal_Sup = 0;
            decimal Nbt_sup = 0;
            decimal vat_sup = 0;
            decimal NetTotal_sup = 0;


            //required when reviewing quotations
        items.ForEach(itms => itms.QuotationCount = 0);

           

                for (int j = 0; j < bid.SupplierQuotations.Count; j++)
                {

                    if(bid.SupplierQuotations[j].QuotationId == QuotationId)
                    {

                        for (int i = 0; i < items.Count; i++)
                        {
                           
                            for (int x = 0; x < bid.SupplierQuotations[j].QuotationItems.Count; x++)
                            {


                                if ((bid.SupplierQuotations[j].QuotationItems[x].QuotationId == QuotationId) && ((bid.SupplierQuotations[j].QuotationItems[x].BiddingItemId == items[i].BiddingItemId)) )
                                {

                                List<TabulationDetail> objSelectedList = tabulationDetailController.GetSelectedStatus(bid.SupplierQuotations[j].QuotationItems[x].QuotationId, bid.SupplierQuotations[j].SupplierId, bid.SupplierQuotations[j].QuotationItems[x].ItemId, bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId);

                                if(objSelectedList.Count > 0)
                                {
                                    decimal subTotal = decimal.Parse(objSelectedList[0].SubTotal.ToString());
                                    decimal nbt = decimal.Parse(objSelectedList[0].NbtAmount.ToString());
                                    decimal vat = decimal.Parse(objSelectedList[0].VAtAmount.ToString());
                                    items[i].Nbt = Convert.ToDecimal(nbt.ToString("#,0.00"));
                                    items[i].vaT = Convert.ToDecimal(vat.ToString("#,0.00"));
                                    items[i].RequestingQty = objSelectedList[0].Qty;
                                    items[i].SubTotal = Convert.ToDecimal(objSelectedList[0].SubTotal.ToString("#,0.00"));
                                    items[i].TotalNbT = objSelectedList[0].NbtAmount;
                                    items[i].TotalVaT = objSelectedList[0].VAtAmount;
                                    items[i].NetTotal = Convert.ToDecimal((subTotal + nbt + vat).ToString("#,0.00"));
                                    
                                }
                                else
                                {
                                    decimal subTotal = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].SubTotal.ToString());
                                    decimal nbt = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].NbtAmount.ToString());
                                    decimal vat = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].VatAmount.ToString());

                                    SubTotal_Sup += subTotal;
                                    Nbt_sup += nbt;
                                    vat_sup += vat;

                                    decimal TempNet = subTotal + nbt + vat;
                                    NetTotal_sup += TempNet;

                                    items[i].SubTotal_Sup = SubTotal_Sup;
                                    items[i].vat_sup = vat_sup;
                                    items[i].Nbt_sup = Nbt_sup;
                                    items[i].NetTotal_sup = NetTotal_sup;


                                    items[i].Nbt = nbt;
                                    items[i].vaT = vat;
                                    items[i].RequestingQty = bid.SupplierQuotations[j].QuotationItems[x].Qty;
                                    items[i].SubTotal = Convert.ToDecimal(bid.SupplierQuotations[j].QuotationItems[x].SubTotal.ToString("#,0.00"));
                                    items[i].TotalNbT = bid.SupplierQuotations[j].QuotationItems[x].NbtAmount;
                                    items[i].TotalVaT = bid.SupplierQuotations[j].QuotationItems[x].VatAmount;
                                    items[i].NetTotal = (subTotal + nbt + vat);
                                }

                                items[i].QuotationCount = bid.SupplierQuotations.Count;
                                    items[i].SupplierMentionedItemName = bid.SupplierQuotations[j].QuotationItems[x].SupplierMentionedItemName;
                                    items[i].QuotationId = bid.SupplierQuotations[j].QuotationItems[x].QuotationId;
                                    items[i].Description = bid.SupplierQuotations[j].QuotationItems[x].Description;
                                    items[i].QuotedPrice = bid.SupplierQuotations[j].QuotationItems[x].UnitPrice;

                                for (int u = 0; u < bid.BiddingItems.Count; u++)
                                {
                                    if (bid.BiddingItems[u].BiddingItemId == items[i].BiddingItemId)
                                    {
                                        items[i].UnitShortName = bid.BiddingItems[u].UnitShortName;
                                    }
                                }

                                    items[i].HasVat = bid.SupplierQuotations[j].QuotationItems[x].HasVat;
                                    items[i].HasNbt = bid.SupplierQuotations[j].QuotationItems[x].HasNbt;
                                    items[i].UnitPrice = bid.SupplierQuotations[j].QuotationItems[x].UnitPrice;
                                    items[i].RequestedTotalQty = bid.SupplierQuotations[j].QuotationItems[x].Qty;
                                    items[i].TablationId = TabID;
                                    items[i].ItemId = bid.SupplierQuotations[j].QuotationItems[x].ItemId;
                                    items[i].SelectedSupplierID = bid.SupplierQuotations[j].SupplierId;
                                    items[i].QuotationItemId = bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId;
                                    items[i].SupplierMentionedItemName = bid.SupplierQuotations[j].QuotationItems[x].SupplierMentionedItemName;
                                    items[i].IsSelectedTB = tabulationDetailController.GetSelectedStatus(bid.SupplierQuotations[j].QuotationItems[x].QuotationId, bid.SupplierQuotations[j].SupplierId, bid.SupplierQuotations[j].QuotationItems[x].ItemId, bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId).Count == 1 ? "1" : "0";

                                int quotationItemId = bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId;
                                List<SupplierBOM> boms = ControllerFactory.CreatesupplierBOMController().GetSupplierBom(quotationItemId);

                                if (boms == null || (boms != null && boms.Count == 0)) {
                                    items[i].SpecComply = "No Specs";
                                }
                                else if (boms.Count == boms.Count(b => b.Comply == 1)) {
                                    items[i].SpecComply = "Yes";
                                }
                                else if (boms.Count == boms.Count(b => b.Comply == 0)) {
                                    items[i].SpecComply = "No";
                                }
                                else {
                                    items[i].SpecComply = "Some";
                                }
                            }
                            }
                        }



                  
                    }
                   
                }

            

            return items;










            //List<BiddingItem> items = new List<BiddingItem>();



            //foreach(SupplierQuotation objSupQuotation in bid.SupplierQuotations)
            //{

            //    if(objSupQuotation.QuotationId == QuotationId)
            //    {
            //        foreach(SupplierQuotationItem  QuotItem in objSupQuotation.QuotationItems)
            //        {

            //            foreach (BiddingItem objItem in bid.BiddingItems)
            //            {
            //                if (QuotItem.BiddingItemId == objItem.BiddingItemId)
            //                {
            //                    objItem.SupplierMentionedItemName = QuotItem.SupplierMentionedItemName;
            //                    objItem.QuotationCount = bid.SupplierQuotations.Count();
            //                    objItem.QuotationItemId = objItem.QuotationItemId;
            //                    items.Add(objItem);
            //                }
            //            }

            //        }

            //    }

            //}



            //return items;
        }

        public static List<BiddingItem> GetBidItemsSupplierForReview(Bidding bid, int QuotationId, int TabID) {

            List<BiddingItem> items = bid.BiddingItems;
            TabulationDetailController tabulationDetailController = ControllerFactory.CreateTabulationDetailController();

            //decimal SubTotal_Sup = 0;
            //decimal Nbt_sup = 0;
            //decimal vat_sup = 0;
            //decimal NetTotal_sup = 0;


            //required when reviewing quotations
            items.ForEach(itms => itms.QuotationCount = 0);



            for (int j = 0; j < bid.SupplierQuotations.Count; j++) {

                if (bid.SupplierQuotations[j].QuotationId == QuotationId) {

                    for (int i = 0; i < items.Count; i++) {

                        for (int x = 0; x < bid.SupplierQuotations[j].QuotationItems.Count; x++) {


                            if ((bid.SupplierQuotations[j].QuotationItems[x].QuotationId == QuotationId) && ((bid.SupplierQuotations[j].QuotationItems[x].BiddingItemId == items[i].BiddingItemId)) && (bid.SupplierQuotations[j].QuotationItems[x].UnitPrice > 0)) {

                                decimal SubTotal_Sup = 0;
                                decimal Nbt_sup = 0;
                                decimal vat_sup = 0;
                                decimal NetTotal_sup = 0;

                                decimal subTotal = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].SubTotal.ToString());
                                    decimal nbt = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].NbtAmount.ToString());
                                    decimal vat = decimal.Parse(bid.SupplierQuotations[j].QuotationItems[x].VatAmount.ToString());

                                    SubTotal_Sup = subTotal;
                                    Nbt_sup = nbt;
                                    vat_sup = vat;

                                    decimal TempNet = subTotal + nbt + vat;
                                    NetTotal_sup += TempNet;

                                    items[i].SubTotal_Sup = SubTotal_Sup;
                                    items[i].vat_sup = vat_sup;
                                    items[i].Nbt_sup = Nbt_sup;
                                    items[i].NetTotal_sup = NetTotal_sup;


                                    items[i].Nbt = nbt;
                                    items[i].vaT = vat;
                                    items[i].RequestingQty = bid.SupplierQuotations[j].QuotationItems[x].Qty;
                                    items[i].SubTotal = Convert.ToDecimal(bid.SupplierQuotations[j].QuotationItems[x].SubTotal.ToString("#,0.00"));
                                    items[i].TotalNbT = bid.SupplierQuotations[j].QuotationItems[x].NbtAmount;
                                    items[i].TotalVaT = bid.SupplierQuotations[j].QuotationItems[x].VatAmount;
                                    items[i].NetTotal = (subTotal + nbt + vat);
                                

                                items[i].QuotationCount = bid.SupplierQuotations.Count;
                                items[i].SupplierMentionedItemName = bid.SupplierQuotations[j].QuotationItems[x].SupplierMentionedItemName;
                                items[i].QuotationId = bid.SupplierQuotations[j].QuotationItems[x].QuotationId;
                                items[i].Description = bid.SupplierQuotations[j].QuotationItems[x].Description;
                                items[i].QuotedPrice = bid.SupplierQuotations[j].QuotationItems[x].UnitPrice;

                                for (int u = 0; u < bid.BiddingItems.Count; u++) {
                                    if (bid.BiddingItems[u].BiddingItemId == items[i].BiddingItemId) {
                                        items[i].UnitShortName = bid.BiddingItems[u].UnitShortName;
                                    }
                                }

                                items[i].HasVat = bid.SupplierQuotations[j].QuotationItems[x].HasVat;
                                items[i].HasNbt = bid.SupplierQuotations[j].QuotationItems[x].HasNbt;
                                items[i].UnitPrice = bid.SupplierQuotations[j].QuotationItems[x].UnitPrice;
                                items[i].RequestedTotalQty = bid.SupplierQuotations[j].QuotationItems[x].Qty;
                                items[i].TablationId = TabID;
                                items[i].ItemId = bid.SupplierQuotations[j].QuotationItems[x].ItemId;
                                items[i].SelectedSupplierID = bid.SupplierQuotations[j].SupplierId;
                                items[i].QuotationItemId = bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId;
                                items[i].SupplierMentionedItemName = bid.SupplierQuotations[j].QuotationItems[x].SupplierMentionedItemName;
                                items[i].IsSelectedTB = tabulationDetailController.GetSelectedStatus(bid.SupplierQuotations[j].QuotationItems[x].QuotationId, bid.SupplierQuotations[j].SupplierId, bid.SupplierQuotations[j].QuotationItems[x].ItemId, bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId).Count == 1 ? "1" : "0";

                                int quotationItemId = bid.SupplierQuotations[j].QuotationItems[x].QuotationItemId;
                                List<SupplierBOM> boms = ControllerFactory.CreatesupplierBOMController().GetSupplierBom(quotationItemId);

                                if (boms == null || (boms != null && boms.Count == 0)) {
                                    items[i].SpecComply = "No Specs";
                                }
                                else if (boms.Count == boms.Count(b => b.Comply == 1)) {
                                    items[i].SpecComply = "Yes";
                                }
                                else if (boms.Count == boms.Count(b => b.Comply == 0)) {
                                    items[i].SpecComply = "No";
                                }
                                else {
                                    items[i].SpecComply = "Some";
                                }
                            }
                        }
                    }




                }

            }



            return items;
            
        }


        public static List<BiddingItem> RemoveNull(List<BiddingItem> objlist)
        {
            List<BiddingItem> Newlist = new List<BiddingItem>();

            foreach(BiddingItem objItem in objlist)
            {
                //if(string.IsNullOrEmpty(objItem.UnitShortName.ToString()))
                if (objItem.UnitShortName == null)  
                {
                    objItem.UnitShortName = "";

                }

                Newlist.Add(objItem);
            }


            return Newlist;
        }

    }
}