<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="EditPO.aspx.cs" Inherits="BiddingSystem.EditPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
       input[type=number]::-webkit-inner-spin-button, 
input[type=number]::-webkit-outer-spin-button { 
    -webkit-appearance: none;
    -moz-appearance: none;
    appearance: none;
    margin: 0; 
}
    </style>
    
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <section class="content-header">
    <h1>
      Edit Purchase Order
        <small></small>
      </h1>
      <ol class="breadcrumb">
          
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Edit Purchase Order </li>
      </ol>
    </section>
    <br />

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
        <section class="content" style="padding-top: 0px">

   <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;" id="divPrintPo" runat="server" >    <!-- Main content -->
      
           <div class="box box-info">
                        <%--<div class="box-header">
                            <img src="AdminResources/images/logo.png" class="center-block" />
                            <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3>
                            <hr>
                        </div>--%>
                        <%--<div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <strong>SUPPLIER: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblSupName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                                </div>

                                <div class="col-md-3">
                                    
                                    <strong>COMPANY: </strong>
                                    <asp:Label runat="server" ID="lblCompName"></asp:Label><br>
                                    <strong>VAT NO: </strong>
                                    <asp:Label runat="server" ID="lblCompVatNo"></asp:Label><br>
                                    <strong>TELEPHONE: </strong>
                                    <asp:Label runat="server" ID="lblTpNo"></asp:Label><br>
                                    <strong>FAX: </strong>
                                    <asp:Label runat="server" ID="lblFax"></asp:Label><br>
                                    <strong>STORE KEEPER: </strong>
                                    <asp:Label runat="server" ID="lblSK"></asp:Label><br>
                                </div>
                                <div class="col-md-3">
                                    <strong>DELIVERING WAREHOUSE: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>
                                </div>
                                <div class="col-md-3">
                                     <strong>DATE: </strong>
                                    <asp:Label runat="server" ID="lblDate"></asp:Label><br>
                                    <strong>PO CODE: </strong>
                                    <asp:Label runat="server" ID="lblPO"></asp:Label><br>
                                    <strong>BASED PR: </strong>
                                    <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                    <strong>QUOTATION FOR: </strong>
                                    <asp:Label runat="server" ID="lblQuotationFor"></asp:Label><br>
                                    <strong>APPROVAL STATUS: </strong>
                                    <asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>
                                    <asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>
                                    <asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>
                                    <strong>PO TYPE: </strong>
                                    <asp:Label runat="server" ID="lblGeneral" CssClass="label label-success" Visible="false" Text="General PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblCovering" CssClass="label label-info" Visible="false" Text="Covering PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblModified" CssClass="label label-warning" Visible="false" Text="Modified PO"></asp:Label><br>
                                    <asp:Panel runat="server" ID="pnlPaymentMethod" Visible="false">
                                        <strong>PAYMENT METHOD: </strong>
                                        <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="pnlReason" Visible="false">
                                        <strong>REMARKS: </strong>
                                        <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                                    </asp:Panel>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <br />
                                    <div class="table-responsive">
                                       <asp:GridView runat="server" ID="gvPoItems" AutoGenerateColumns="false"
                                            CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                            <Columns>
                                                <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                                                    Text="Awaiting Receival" CssClass="label label-warning"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                                    Text="Partially Received" CssClass="label label-info"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                                    Text="Fully Received" CssClass="label label-success"/>
                                                                                <asp:LinkButton 
                                                                                    runat="server" ID="btnMrn" 
                                                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                    Text="Terminated" CssClass="label label-danger" ></asp:LinkButton>
                                                                     </ItemTemplate>
                                                               </asp:TemplateField>
                                                <asp:BoundField DataField="Quantity" HeaderText="Requested QTY" />
                                                <asp:BoundField DataField="ReceivedQty" HeaderText="Recieved QTY" />
                                                <asp:BoundField DataField="WaitingQty" HeaderText="Waiting QTY" />
                                                <asp:BoundField DataField="PendingQty" HeaderText="Pending QTY" />
                                                
                                                <asp:BoundField DataField="ItemPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="TotalAmount" HeaderText="NetTotal" DataFormatString="{0:N2}" />
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>--%>

                       <%-- <div class="row">
                                <div class="col-md-4 col-md-push-8">
                                    <p class="lead">SUMMARY</p>
                                    <div class="table-responsive">
                                        <table class="table table-striped">
                                            <tbody>
                                                <tr>
                                                    <td><b>TOTAL</b></td>
                                                    <td id="tdSubTotal" class="text-right" runat="server"></td>
                                                </tr>
                                                
                                                <tr>
                                                    <td><b>NBT</b></td>
                                                    <td id="tdNbt" class="text-right" runat="server"></td>
                                                </tr>
                                                <tr>
                                                    <td><b>VAT</b></td>
                                                    <td id="tdVat" class="text-right" runat="server"></td>
                                                </tr>
                                                <tr>
                                                    <td><b>NETTOTAL</b></td>
                                                    <td id="tdNetTotal" class="text-right" runat="server"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <hr />--%>
                         
                       <%-- </div>
                        <div class="box-footer">--%>
                            <%--<asp:Button runat="server" ID="btnModify" CssClass="btn btn-warning" Text="Edit PO" OnClick="btnModify_Click" />--%>
                            <%--<asp:Button runat="server" ID="btnPrint"  Text="Print PO" CssClass="btn btn-success" OnClick="btnPrint_Click" />
                           --%> <%--<div id="printerDiv"  style="display:none"></div>--%>
                       <%-- </div>
                    </div>--%>







               
                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3>
                            <hr>
                        </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <strong>SUPPLIER: </strong>
                                        <br>
                                        <asp:Label runat="server" ID="lblsupplierName"></asp:Label><br>
                                        <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                        <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                                    </div>
                                    <div class="col-md-4">
                                        <strong>DELIVERING WAREHOUSE: </strong>
                                        <br>
                                        <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                        <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                        <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label><br />
                                        
                                        
                                    </div>
                                    <div class="col-md-4">
                                        <strong>PO CODE: </strong>
                                        <asp:Label runat="server" ID="lblPOCode"></asp:Label><br>
                                        <strong>BASED PR: </strong>
                                        <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                        <strong>PO TYPE: </strong>
                                        <asp:Label runat="server" ID="lblGeneral" CssClass="label label-success" Visible="false" Text="General PO"></asp:Label>
                                        <asp:Label runat="server" ID="lblCovering" CssClass="label label-info" Visible="false" Text="Covering PO"></asp:Label>
                                        <asp:Label runat="server" ID="lblModified" CssClass="label label-warning" Visible="false" Text="Modified PO"></asp:Label><br>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <br />
                                        <br />
                                        <div class="table-responsive">
                                            <asp:GridView runat="server" ID="gvPoItems" AutoGenerateColumns="false" 
                                                CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                                <Columns>
                                                    <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="QuotationItemId" HeaderText="QuotationItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Default Item Name" />
                                                     <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier mentioned Item Name"  NullDisplayText="Not Found"  />
                                                                                <%--<asp:TemplateField HeaderText="Supplier mentioned Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server" ID="SupplierMentionedItemName" CssClass="SupplierMentionedItemName"
                                                                                    Text='<%# Eval("SupplierMentionedItemName").ToString() == "" ? "Not Found" : Eval("SupplierMentionedItemName").ToString() %>'/>
                                                                                
                                                                                 </ItemTemplate>
                                                               </asp:TemplateField>--%>
                                                    <asp:BoundField DataField="MeasurementName" HeaderText="Measurement" NullDisplayText="Not Found" />
                                                    <asp:TemplateField HeaderText="QTY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQuantity" Text='<%# Eval("Quantity") %>'
                                                                type="number" step=".01" min="0" runat="server"
                                                                autocomplete="off" CssClass="txtQuantityCl" Width="150px" onkeyup="calculate(this)"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Actual Price">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtActualPrice" Text='<%#Eval("ActualPrice")%>'
                                                                type="number" step=".01" min="0" runat="server" Width="80px"
                                                                autocomplete="off" CssClass="txtActualPriceCl" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Quoted Price">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtUnitPrice" Text='<%#Eval("ItemPrice")%>'
                                                                type="number" step=".01" min="0" runat="server" Width="150px"
                                                                autocomplete="off" CssClass="txtUnitPriceCl" onkeyup="calculate(this)" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sub Total">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtSubTotal" Enabled="false" Text='<%#Eval("SubTotal","{0:N2}")%>' 
                                                                runat="server" Width="150px" CssClass="txtSubTotalCl" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Include VAT">
                                                        <ItemTemplate>
                                                            <%--<asp:CheckBox ID="chkNbt" Text="NBT" runat="server" style="cursor:pointer"
                                                                Checked='<%#Eval("HasNbt").ToString() =="1"?true:false%>' Visible ="false"
                                                                CssClass="chkNbtCl" OnChange="calculate(this)"/><br />--%>
                                                            <asp:CheckBox ID="chkVat" Text="VAT" runat="server" style="cursor:pointer"
                                                                Checked='<%#Eval("HasVat").ToString() =="1"?true:false%>'
                                                                CssClass="chkVatCl" OnChange="calculate(this)" VatRate ="0.08"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NBT Percentage" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdoNbt204" GroupName="grpPercentage" style="cursor:pointer"
                                                                Text="2.04%" runat="server" Checked='<%#Eval("NbtCalculationType").ToString() =="1"?true:false%>'
                                                                CssClass="rdo204"  OnChange="calculate(this)"/><br />
                                                            <asp:RadioButton ID="rdoNbt2" GroupName="grpPercentage" style="cursor:pointer"
                                                                Text="2.00%" runat="server" Checked='<%#Eval("NbtCalculationType").ToString() =="1"?false:true%>'
                                                                CssClass="rdo2"  OnChange="calculate(this)"/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NBT" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNbt" Enabled="false" Text='<%#Eval("NbtAmount","{0:N2}")%>'
                                                                runat="server" Width="150px" CssClass="txtNbtCl" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="VAT">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtVat" Enabled="false" Text='<%#Eval("VatAmount","{0:N2}")%>'
                                                                runat="server" Width="150px" CssClass="txtVatCl" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Net Total">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtNetTotal" Enabled="false" Text='<%#Eval("TotalAmount","{0:N2}")%>'
                                                                runat="server" Width="150px" CssClass="txtNetTotalCl" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" ID="btnDelete"
                                                                    CssClass="btn btn-xs btn-danger" Width="80px" OnClick="btnDelete_Click" 
                                                                    Text="Delete"></asp:Button>
                                                     </ItemTemplate>
                                                 </asp:TemplateField>
                                                    <asp:BoundField DataField="BasePr" HeaderText="PR ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="MeasurementId" HeaderText="Measurement Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-xs-8">
                                        <asp:Panel ID="Remarks" runat="server" >
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox TextMode="MultiLine" Rows="6" runat="server" ID="txtRemarks" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                    </asp:Panel>

                                        </div>

                                        <div class="col-md-4>
                                    <%--<div class="col-md-4 col-md-push-8">--%>
                                        <p class="lead">SUMMARY</p>
                                        <div class="table-responsive">
                                            <table class="table table-striped">
                                                <tbody>
                                                    <tr>
                                                        <td><b>TOTAL</b></td>
                                                        <td id="tdSubTotal" class="text-right" runat="server"></td>
                                                    </tr>
                                                    
                                                    <%--<tr>
                                                        <td><b>NBT</b></td>
                                                        <td id="tdNbt" class="text-right" runat="server"></td>
                                                    </tr>--%>
                                                    <tr>
                                                        <td><b>VAT</b></td>
                                                        <td id="tdVat" class="text-right" runat="server"></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>NETTOTAL</b></td>
                                                        <td id="tdNetTotal" class="text-right" runat="server"></td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        <div class="box-footer">
                            <asp:Button runat="server" Text="Save Changes" CssClass="btn btn-warning" OnClientClick="update()" />
                            <%--<img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-warning.gif" class="hidden loader" style="margin-right:10px; max-height:30px;" />--%>
                            <asp:Button runat="server" CssClass="btn btn-danger btnCancelCl" Text="Cancel" OnClick="bCancel_Click" />
                        </div>
                    </div>
     

       
       
    </div> 
       </div> 
    </section>

                <asp:HiddenField ID="hdnSubTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnDiscount" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtTotal" runat="server" Value="0.00" />

                <asp:HiddenField ID="hdnVatRate" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtRate1" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtRate2" runat="server" Value="0.00" />

                <asp:HiddenField ID="hdnVatTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNetTotal" runat="server" Value="0.00" />
                    <asp:HiddenField runat="server" ID="hdnRemarks" />

                    <asp:Button runat="server" ID="btnUpdate" CssClass="hidden" OnClick="btnUpdate_Click"/>
                    <asp:Button runat="server" ID="btnCancel" CssClass="hidden" OnClick="btnCancel_Click"/>
                </ContentTemplate>
            </asp:UpdatePanel>
    </form>
  

 
    <script>
        $(function () {
            Sys.Application.add_load(function () {
                $('.btnUpdateCl').on({
                    click: function () {
                        event.preventDefault();
                        swal.fire({
                            title: 'Are you sure?',
                            html: "Updating a PO creates a new PO. Are you sure you want save changes?</br></br>"
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
                                $('.btnUpdateCl').addClass('hidden');
                                $('.loader').removeClass('hidden');
                                $(document).find('.txtSubTotalCl').removeAttr('disabled');
                                $(document).find('.txtNbtCl').removeAttr('disabled');
                                $(document).find('.txtVatCl').removeAttr('disabled');
                                $(document).find('.txtNetTotalCl').removeAttr('disabled');

                                $('#ContentSection_btnUpdate').click();
                            }
                        });
                    }
                });


                $('.txtUnitPriceCl').on({
                    keyup: function () {
                        calculate(this);
                    },
                    change: function () {
                        calculate(this);
                    }
                });


                //$('.txtActualPriceCl').on({
                //    keyup: function () {
                //        $(this).closest('tr').find('.txtUnitPriceCl').val($(this).val());
                //        calculate(this);
                //    },
                //    change: function () {
                //        calculate(this);
                //    }
                //});

                $('.txtQuantityCl').on({
                    keyup: function () {
                        calculate(this);
                    },
                    change: function () {
                        calculate(this);
                    }
                });

                $('.chkNbtCl').on({
                    change: function () {
                        calculate(this);
                    }
                });

                $('.chkVatCl').on({
                    change: function () {
                        calculate(this);
                    }
                });

                $('.rdo204').on({
                    change: function () {
                        calculate(this);
                    }
                });

                $('.rdo2').on({
                    change: function () {
                        calculate(this);
                    }
                });
            });
        });

        function update() {
            swal.fire({
                            title: 'Are you sure?',
                            html: "Updating a PO creates a new PO. Are you sure you want save changes?</br></br>"
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
                                $('.btnUpdateCl').addClass('hidden');
                                $('.loader').removeClass('hidden');
                                $(document).find('.txtSubTotalCl').removeAttr('disabled');
                                $(document).find('.txtNbtCl').removeAttr('disabled');
                                $(document).find('.txtVatCl').removeAttr('disabled');
                                $(document).find('.txtNetTotalCl').removeAttr('disabled');

                                $('#ContentSection_btnUpdate').click();
                            }
                        });
        }

        function calculate(elmt) {

            var unitPrice = $(elmt).closest('tr').find('.txtUnitPriceCl').val();

            if (unitPrice == '' || unitPrice == null) {
                unitPrice = 0;
            }
            var qty = $(elmt).closest('tr').find('.txtQuantityCl').val();

            if (qty == '' || qty == null) {
                qty = 0;
            }
            var subTot = 0;
            var nbt = 0;
            var vat = 0;
            var netTot = 0;

            subTot = unitPrice * qty;


            var chkNbt = $(elmt).closest('tr').find('.chkNbtCl').find('input');
            var chkVat = $(elmt).closest('tr').find('.chkVatCl').find('input');

            var vatRate = $('#ContentSection_hdnVatRate').val();
            var nbtRate1 = $('#ContentSection_hdnNbtRate1').val();
             var nbtRate2 =  $('#ContentSection_hdnNbtRate2').val();
          

            var rdoNbt204 = $(elmt).closest('tr').find('.rdo204').find('input');
            var rdoNbt2 = $(elmt).closest('tr').find('.rdo2').find('input');

            if ($(chkNbt).prop('checked') == true) {
                if ($(rdoNbt204).prop('checked') == true) {
                    //nbt = parseFloat((subTot * 2) / 98);
                    nbt = parseFloat((subTot * nbtRate1));
                }
                else {
                    //nbt = parseFloat((subTot * 2) / 100);
                    nbt = parseFloat((subTot * nbtRate2));
                }

            }

            if ($(chkVat).prop('checked') == true) {

                //vat = parseFloat((subTot + nbt) * 0.08);
                vat = parseFloat((subTot + nbt) * vatRate);
            }

            netTot = subTot + nbt + vat;

            $(elmt).closest('tr').find('.txtSubTotalCl').val(subTot.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
            $(elmt).closest('tr').find('.txtNbtCl').val(nbt.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
            $(elmt).closest('tr').find('.txtVatCl').val(vat.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
            $(elmt).closest('tr').find('.txtNetTotalCl').val(netTot.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));

            var tableRows = $(elmt).closest('tbody').find('> tr:not(:has(>td>table))');

            var globSubTotal = 0;
            var globTotalNbt = 0;
            var globTotalVat = 0;
            var globNetTotal = 0;
            var globDiscount = 0;

            for (i = 1; i < tableRows.length; i++) {
                //if ($(tableRows[i]).find('.txtActualPriceCl').val() != '')
                //    globSubTotal = globSubTotal + (parseFloat($(tableRows[i]).find('.txtQuantityCl').val()) * parseFloat($(tableRows[i]).find('.txtActualPriceCl').val()));
                //if ($(tableRows[i]).find('.txtSubTotalCl').val() != '' && $(tableRows[i]).find('.txtActualPriceCl').val() != '')
                //    globDiscount = globDiscount + ((parseFloat($(tableRows[i]).find('.txtQuantityCl').val()) * parseFloat($(tableRows[i]).find('.txtActualPriceCl').val())) - parseFloat($(tableRows[i]).find('.txtSubTotalCl').val()));
                 if ($(tableRows[i]).find('.txtSubTotalCl').val() != '')
                    globSubTotal = globSubTotal + (parseFloat($(tableRows[i]).find('.txtQuantityCl').val()) * parseFloat($(tableRows[i]).find('.txtUnitPriceCl').val()));
                
                if ($(tableRows[i]).find('.txtNbtCl').val() != '')
                    globTotalNbt = globTotalNbt + parseFloat($(tableRows[i]).find('.txtNbtCl').val().replace(/,(?=.*\.\d+)/g, ''));
                if ($(tableRows[i]).find('.txtVatCl').val() != '')
                    globTotalVat = globTotalVat + parseFloat($(tableRows[i]).find('.txtVatCl').val().replace(/,(?=.*\.\d+)/g, ''));
                if ($(tableRows[i]).find('.txtNetTotalCl').val() != '')
                    globNetTotal = globNetTotal + parseFloat($(tableRows[i]).find('.txtNetTotalCl').val().replace(/,(?=.*\.\d+)/g, ''));
            }

            $('#ContentSection_tdSubTotal').html(globSubTotal.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
            $('#ContentSection_tdDiscount').html(globDiscount.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
            $('#ContentSection_tdNbt').html(globTotalNbt.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
            $('#ContentSection_tdVat').html(globTotalVat.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));
            $('#ContentSection_tdNetTotal').html(globNetTotal.toFixed(2).replace(/\B(?<!\.\d*)(?=(\d{3})+(?!\d))/g, ","));


            $('#ContentSection_hdnSubTotal').val(globSubTotal.toFixed(2));
            $('#ContentSection_hdnDiscount').val(globDiscount.toFixed(2));
            $('#ContentSection_hdnNbtTotal').val(globTotalNbt.toFixed(2));
            $('#ContentSection_hdnVatTotal').val(globTotalVat.toFixed(2));
            $('#ContentSection_hdnNetTotal').val(globNetTotal.toFixed(2));
        }

    </script>


</asp:Content>
