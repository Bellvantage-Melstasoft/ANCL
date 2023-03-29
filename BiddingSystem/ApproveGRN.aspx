<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ApproveGRN.aspx.cs" Inherits="BiddingSystem.ApproveGRN" %>

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
                                <i class="fa fa-envelope"></i>Good Recieved Note (GRN)
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
                                <asp:Panel ID="PnlWarehouse" runat="server">
                                <strong>DELIVERING WAREHOUSE: </strong>
                                <br>
                                <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>

                                <br>
                                    </asp:Panel>
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
                               <%-- <strong>QUOTATION FOR: </strong>
                                <asp:Label runat="server" ID="lblquotationfor"></asp:Label><br>--%>
                                <strong>RECEIVED DATE: </strong>
                                <asp:Label runat="server" ID="lblReceiveddate"></asp:Label><br>
                                 <strong>PAYMENT TYPE: </strong>
                                    <asp:Label runat="server" ID="lblPaymenttype"></asp:Label><br>
                                <strong>PURCHASE TYPE: </strong>
                                    <asp:Label runat="server" ID="lblPurchaseType"></asp:Label><br>
                                <strong>APPROVAL STATUS: </strong>
                                <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label><br>
                                <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label><br>
                                <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                                
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="co-md-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPurchaseOrderItems" runat="server" CssClass="table table-responsive"
                                        AutoGenerateColumns="false" HeaderStyle-BackColor="LightGray">
                                        <Columns>
                                            <asp:BoundField DataField="PoId" HeaderText="Po Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <%--  <asp:BoundField DataField="QuotationId" HeaderText="Quotation Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
                                            <asp:BoundField DataField="ItemId" HeaderText="Item No" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="ReferenceNo" HeaderText="Item Code" />
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier Mentioned Item Name" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="MeasurementShortName" HeaderText="Measurement" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText" />
                                            <asp:BoundField DataField="FreeQty" HeaderText="Free Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="centerHeaderText" />
                                            <asp:TemplateField HeaderText="Expiry Date">
                                               <ItemTemplate>
                                                <%# (DateTime)Eval("ExpiryDate") == DateTime.MinValue ? "Not Defined" : string.Format("{0:MM-dd-yyyy}", (DateTime)Eval("ExpiryDate")) %>
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ItemPrice" HeaderText="Unit Price" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                             <asp:BoundField DataField="SubTotal" HeaderText="Sub Total" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="VatAmount" HeaderText="Vat Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />
                                            <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" DataFormatString="{0:N2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-CssClass="rightHeaderText" />


                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="col-xs-12 col-sm-12">
                            <div class="col-xs-8 col-sm-8">
                                <strong>GRN Note: </strong>
                                <asp:Label runat="server" ID="lblgrnNote"></asp:Label><br>
                            </div>
                            <div class="col-xs-4">
                                <p class="lead">Amount Details</p>

                                <div class="table-responsive">
                                    <table class="table">
                                        <tr>
                                            <td style="width: 50%">Subtotal:</td>
                                            <td class="text-right">
                                                <asp:Label runat="server" ID="lblSubtotal"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>Vat Total</td>
                                            <td class="text-right">
                                                <asp:Label runat="server" ID="lblVatTotal"></asp:Label></td>
                                        </tr>
                                        <%--<tr>
                                            <td>NBT Total:</td>
                                            <td class="text-right">
                                                <asp:Label runat="server" ID="lblNbtTotal"></asp:Label></td>
                                        </tr>--%>
                                        <tr>
                                            <td><b>Total:</b></td>
                                            <td class="text-right"><b>
                                                <asp:Label runat="server" ID="lblTotal"></asp:Label></b></td>
                                        </tr>
                                    </table>
                                </div>
                            </div>


                            <%--                <div class="row">
                    <table style="width: 50%;">
                        <tr style="width: 100%;">
                            <td>Received Date&nbsp;</td>
                            <td>:&nbsp;</td>
                            <td style="margin: 10px; padding: 10px;">
                                <label id="lblReceiveddate" runat="server" class="form-control"></label>
                            </td>
                        </tr>
                        <tr style="width: 100%;">
                            <td>Remarks&nbsp;</td>
                            <td>:&nbsp;</td>
                            <td style="margin: 10px; padding: 10px;">
                                <label id="lblgrnComment" runat="server" class="form-control"></label>
                            </td>
                        </tr>

                    </table>
                </div>--%>
                        </div>


                        <hr />
                        <div class="row">
                            <div class="col-xs-4 col-sm-4  text-center">

                                <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                <asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />
                                <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                <b>PO Created By</b>
                            </div>

                            <div class="col-xs-4 col-sm-4  text-center">
                                <asp:Panel ID="pnlApprovedBy" runat="server">
                                    <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height: 50px;" runat="server" /><br />
                                    <asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                    <b>PO Approved By</b>
                                </asp:Panel>
                            </div>


                            <div class="col-xs-4 col-sm-4">
                                <asp:Panel ID="pnlRemark" runat="server">
                                    <div style="height: 50px; width: 100px;"></div>
                                    <strong>Remarks: </strong>
                                    <asp:Label runat="server" ID="lblgrnComment" Text=""></asp:Label>
                                </asp:Panel>
                            </div>
                        </div>

                        <hr />
                        <div class="row no-print">
                            <div class="col-xs-12">
                                <asp:Button runat="server" CssClass="btn btn-warning btnApproveCl" Text="Approve GRN" />
                                <asp:Button runat="server" CssClass="btn btn-danger btnRejectCl" Text="Reject GRN" />
                            </div>
                        </div>
                    </div>
                </div>

                <asp:HiddenField ID="hdnRemarks" runat="server" />
                <asp:HiddenField ID="hdnCanApprove" runat="server" />
                <asp:Button runat="server" ID="btnApprove" OnClick="btnApprove_Click" CssClass="hidden" />
                <asp:Button runat="server" ID="btnReject" OnClick="btnReject_Click" CssClass="hidden" />

            </ContentTemplate>

        </asp:UpdatePanel>
    </form>


    <script>
        $(function () {
            $('.btnApproveCl').on({
                click: function () {
                    event.preventDefault();

                    if ($('#ContentSection_hdnCanApprove').val() == "1") {

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <b>Approve</b> this GRN?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {
                                if ($('#ss').val() == '') {
                                    $('#dd').prop('style', 'color:red');
                                    swal.showValidationError('Remarks Required');
                                    return false;
                                }
                                else {
                                    $('#ContentSection_hdnRemarks').val($('#ss').val());
                                }

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnApprove').click();
                            }
                        });
                    }
                    else {
                        swal.fire({
                            title: 'DENIED!',
                            html: "Looks Like You Have An Unapproved Covering PO For This GRN. You Are Not Allowed To Approve This GRN Until The Covering PO Gets Approved",
                            type: 'error',
                            showCancelButton: false,
                            showConfirmButton: true,
                            confirmButtonText: 'OKAY',
                            allowOutsideClick: false
                        });
                    }
                }
            });

            $('.btnRejectCl').on({
                click: function () {
                    event.preventDefault();
                    swal.fire({
                        title: 'Are you sure?',
                        html: "Are you sure you want to <b>Reject</b> this PO?</br></br>"
                            + "<strong id='dd'>Remarks</strong>"
                            + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                        type: 'warning',
                        cancelButtonColor: '#d33',
                        showCancelButton: true,
                        showConfirmButton: true,
                        confirmButtonText: 'Yes',
                        cancelButtonText: 'No',
                        allowOutsideClick: false,
                        preConfirm: function () {
                            if ($('#ss').val() == '') {
                                $('#dd').prop('style', 'color:red');
                                swal.showValidationError('Remarks Required');
                                return false;
                            }
                            else {
                                $('#ContentSection_hdnRemarks').val($('#ss').val());
                            }

                        }
                    }
                    ).then((result) => {
                        if (result.value) {

                            $('#ContentSection_btnReject').click();
                        }
                    });
                }
            });

        })
    </script>
</asp:Content>
