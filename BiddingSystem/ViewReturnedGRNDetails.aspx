<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewReturnedGRNDetails.aspx.cs" Inherits="BiddingSystem.ViewReturnedGRNDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        .centerHeaderText {
            text-align: center;
        }

        .rightHeaderText {
            text-align: right;
        }

        body {
        }


        @media print {
            body {
            }
        }

        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .tablegv td, .tablegv th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .tablegv tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .tablegv tr:hover {
                background-color: #ddd;
            }

            .tablegv th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }

        .successMessage {
            color: #1B6B0D;
            font-size: medium;
        }

        .failMessage {
            color: #C81A34;
            font-size: medium;
        }

        table, td, tr {
            border-color: black;
        }
        /*th{

          background-color:lightgray;
      }*/

        /*@page{
          size:A4;
          margin:0;
          size:portrait;
          -webkit-print-color-adjust:exact !important
      }*/
    </style>

    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
     <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
    <h1>
       GOOD RECEIVED NOTE
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">View GRN</li>
      </ol>
    </section>
    <br />
    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <div class="content" style="position: relative; background: #fff; overflow: hidden; border: 1px solid #f4f4f4; padding: 20px; margin: 10px 25px;" id="divPrintPo">
                    <!-- Main content -->


                    <div class="row">
                        <div class="col-xs-12">
                            <h2 class="page-header" style="text-align: center;">
                                <i class="fa fa-envelope"></i>Good Return Note
                            </h2>
                        </div>
                        <!-- /.col -->
                    </div>


                    <div class="row invoice-info">

                        <div class="row">
                            <div class="col-xs-4">
                                <strong>SUPPLIER: </strong>
                                <br>
                                <asp:Label runat="server" ID="lblsupplierName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                            </div>
                            <div class="col-xs-4">
                                <strong>DELIVERING WAREHOUSE: </strong>
                                <br>
                                <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>

                                <br>
                                <strong>STORE KEEPER: </strong>
                                <asp:Label runat="server" ID="lblStoreKeeper"></asp:Label><br>
                                <br>
                            </div>
                            <div class="col-xs-4">
                                <strong>GRN CODE: </strong>
                                <asp:Label runat="server" ID="lblGrnCode"></asp:Label><br>
                                <strong>PO CODE: </strong>
                                <asp:Label runat="server" ID="lblPOCode"></asp:Label><br>
                                <strong>BASED PR: </strong>
                                <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                               
                                <strong>RECEIVED DATE: </strong>
                                <asp:Label runat="server" ID="lblReceiveddate"></asp:Label><br>
                                <strong>PAYMENT TYPE: </strong>
                                <asp:Label runat="server" ID="lblPaymenttype"></asp:Label><br>
                                 </div>
                        </div>

                        <div class="panel-body">
                            <div class="co-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive"
                                        AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
                                        <Columns>
                                            
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="ShortCode" HeaderText="Measurement" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="ReturnedQty" HeaderText="Returned Qty" NullDisplayText="Not Found" />
                                            
                                            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="SubTotal" HeaderText="Sub Total" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="VatValue" HeaderText="Vat Value" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="NetTotal" HeaderText="Net Total" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                          
                                            
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                            <label>Supplier Return Option : </label>
                                            <br>
                                            <asp:Label  runat="server" ID="lblSupplierReturnOption" Width="100%"></asp:Label>
                                        </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Panel ID="Remarks" runat="server">
                                        <div class="form-group">
                                            <label>Remarks : </label>
                                            <br>
                                            <asp:Label TextMode="MultiLine" Rows="6" runat="server" ID="txtRemarks" Width="100%"></asp:Label>
                                        </div>
                                    </asp:Panel>
                                </div>
                                        <div class="col-md-4 col-md-push-2" style="text-align:right; font-size:16px;">
                                            <div class="row">
                                            <div class="col-xs-6">
                                                <label  class="summary-title">Sub Total</label>
                                            </div>
                                            <div class="col-xs-6">
                                                <asp:Label runat="server" ID="lblSubTotal"></asp:Label>
                                            </div>
                                                </div>
                                          <div class="row">
                                            <div class="col-xs-6" >
                                                <label class="summary-title">Total VAT</label>

                                            </div>
                                            <div class="col-xs-6" >
                                                
                                                <asp:Label runat="server" ID="lblVatTotalNew"></asp:Label><br>
                                            </div>
                                              </div>
                                              <div class="row">
                                            <div class="col-xs-6">
                                                <label class="summary-title">Net Total</label>
                                            </div>
                                            <div class="col-xs-6">
                                         <asp:Label runat="server" ID="lblNetTotalNew"></asp:Label>
                                                </div>
                                            </div>
                                        </div>                                                
                                   
                            </div>


                        </div>
                        
                    

                        
                    </div>
                </div>

                

            </ContentTemplate>
             
        </asp:UpdatePanel>
    </form>


     <script type="text/javascript">



      
    </script>
</asp:Content>
