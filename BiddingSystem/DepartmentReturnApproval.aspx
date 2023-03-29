<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="DepartmentReturnApproval.aspx.cs" Inherits="BiddingSystem.DepartmentReturnApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <script src="AdminResources/js/jquery1.8.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>

    <style type="text/css">
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

       
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
    <h1>
      View Rejected Inventory
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Rejected Inventory</li>
      </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>



                <div id="mdlReturnAvg" class="modal fade">
                    <div class="modal-dialog" style="width: 60%">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Return Stock</h4>
                            </div>
                            <div class="modal-body">

                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>Return Qty</label>
                                            <asp:TextBox runat="server" ID="txtReturnQty" TextMode="Number" step="any" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnReturnStock_Click" CssClass="btn btn-info"></asp:Button>

                            </div>
                        </div>
                    </div>
                </div>

                <div id="mdlReturnBatch" class="modal fade">
                    <div class="modal-dialog" style="width: 60%">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Return Stock</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>Select Batch</label>
                                            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 col-md-push-3 text-center">
                                        <div class="form-group">
                                            <label>Return Qty</label>
                                            <asp:TextBox runat="server" ID="TextBox1" TextMode="Number" step="any" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button runat="server" ID="Button1" Text="Submit" OnClick="btnReturnStock_Click" CssClass="btn btn-info"></asp:Button>

                            </div>
                        </div>
                    </div>
                </div>



                <section class="content" style="padding-top: 0px">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelMRN" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Rejected Inventory</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                
              <asp:GridView runat="server" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"  OnRowDataBound="gvDeliveredInventory_RowDataBound"  AllowPaging="true"  PageSize="15" OnPageIndexChanging="gvDeliveredInventory_PageIndexChanging"
                  ID="gvDeliveredInventory" GridLines="None" CssClass="table table-responsive tablegv" AutoGenerateColumns="false" DataKeyNames="MrndInID" EmptyDataText="No records Found">
                    <Columns>

                         <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <img alt="" class='<%# Eval("StockMaintainingType").ToString() =="1"?"hidden":"" %>'
                                                                            style="cursor: pointer;margin-top: -6px;"
                                                                            src="images/plus.png" />
                                                                        <asp:Panel ID="pnlIssuedBatches" runat="server"
                                                                            Style="display: none">
                                                                            <asp:GridView ID="gvIssuedBatches" runat="server"
                                                                                CssClass="table table-responsive ChildGridTwo"
                                                                                GridLines="None"
                                                                                AutoGenerateColumns="false"
                                                                                Caption="Issues Batch Details">
                                                                                <Columns>
                                                                                     <asp:BoundField DataField="MrndInID"  HeaderText="MRNDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                 <asp:BoundField DataField="MrndID"  HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                               <asp:BoundField DataField="ItemID"  HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                                <asp:BoundField DataField="MrnID"  HeaderText="MRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                               <asp:BoundField DataField="WarehouseID"  HeaderText="Wareouse Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                <asp:BoundField DataField="StockMaintainingType"  HeaderText="Stock Maintaining Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                <asp:BoundField DataField="BatchId"  HeaderText="Batch Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                 <asp:TemplateField HeaderText="Batch Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="btnBatch" Text='<%# "Batch-"+Eval("BatchCode").ToString() %>' OnClick="btnMrn_Click"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>               
                                                                                <asp:BoundField DataField="IssuedQty"  HeaderText="Issued Qty" />
                                                                                <asp:BoundField DataField="ReturnQty"  HeaderText="Returned QTY"/>
                                                                                <asp:BoundField DataField="BatchExpiryDate"  HeaderText="ExpiryDate" />
                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton runat="server" ID="btnBReturn" Text="Return Stock to Warehouse" Visible='<%#Eval("StockMaintainingType").ToString() == "1"? false:true %>'
                                                                                            OnClientClick='<%#"ReturnBatchStock(event,"+Eval("MrndInID").ToString()+","+Eval("MrndID").ToString()+","+Eval("ItemID").ToString()+","+Eval("MrnID").ToString()+", "+Eval("IssuedQty").ToString()+" , "+Eval("WarehouseID").ToString()+",  "+Eval("StockMaintainingType").ToString()+" , "+Eval("BatchId").ToString()+", "+Eval("ReturnQty").ToString()+")" %>'></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                    </asp:TemplateField>
                        
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </asp:Panel>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>




                        <asp:BoundField DataField="MrndInID"  HeaderText="MRNDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MrndID"  HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ItemID"  HeaderText="Item ID"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MrnID"  HeaderText="MRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="MRN Code">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="btnMrn" Text='<%# "MRN-"+Eval("MrnCode").ToString() %>' OnClick="btnMrn_Click"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department Name"/>
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />                        
                        <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY"/>
                        <asp:BoundField DataField="ShortCode"  HeaderText="Unit" NullDisplayText="Not Found"/>
                       <asp:BoundField DataField="WarehouseID"  HeaderText="Wareouse Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="StockMaintainingType"  HeaderText="Stock Maintaining Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ReturnQty"  HeaderText="Returned QTY"/>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnReturn" Text="Return Stock to Warehouse" Visible='<%#Eval("StockMaintainingType").ToString() != "1"? false:true %>'
                                    OnClientClick='<%#"ReturnStock(event,"+Eval("MrndInID").ToString()+","+Eval("MrndID").ToString()+","+Eval("ItemID").ToString()+","+Eval("MrnID").ToString()+", "+Eval("IssuedQty").ToString()+" , "+Eval("WarehouseID").ToString()+",  "+Eval("StockMaintainingType").ToString()+", "+Eval("ReturnQty").ToString()+")" %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>

                </div>
            </div>         
          </div>
         
        </div>
        <!-- /.box-body -->
      </div>
      <!-- /.box -->
    </section>


                <section class="content" style="padding-top: 0px">
    
    </section>
                <asp:Button ID="lbtnReturn" runat="server" OnClick="btnReturnStock_Click" CssClass="hidden" />
                <asp:HiddenField ID="hdnMrndInID" runat="server" />
                <asp:HiddenField ID="hdnMrndID" runat="server" />
                <asp:HiddenField ID="hdnIssuesQty" runat="server" />
                <asp:HiddenField ID="hdnItemID" runat="server" />
                <asp:HiddenField ID="hdnMrnID" runat="server" />
                <asp:HiddenField ID="hdnWarehouseID" runat="server" />
                <asp:HiddenField ID="hdnStockMaintainingType" runat="server" />
                <asp:HiddenField ID="hdnQty" runat="server" />
                <asp:HiddenField ID="hdnBatchId" runat="server" />
                <asp:HiddenField ID="hdnPrevReturnedQty" runat="server" />
                
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">


        function ReturnStock(e, MrndInID, MrndID, ItemId, MrnId, IssuedQty, WarehouseId, StockMaintainingType, PrevReturnQty) {
            e.preventDefault();
            $('#ContentSection_hdnMrndInID').val(MrndInID);
            $('#ContentSection_hdnMrndID').val(MrndID);
            $('#ContentSection_hdnIssuesQty').val(IssuedQty);
            $('#ContentSection_hdnItemID').val(ItemId);
            $('#ContentSection_hdnMrnID').val(MrnId);
            $('#ContentSection_hdnWarehouseID').val(WarehouseId);
            $('#ContentSection_hdnStockMaintainingType').val(StockMaintainingType);
             $('#ContentSection_hdnPrevReturnedQty').val(PrevReturnQty);
            
            swal.fire({
                title: 'Are you sure?',
                html: "Add return quantity </br></br>"
                    + "<strong id='dd'>Return Quatity</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Return',
                cancelButtonText: 'Close',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnQty').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {

                    $('#ContentSection_lbtnReturn').click();

                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });


        }

        function ReturnBatchStock(e, MrndInID, MrndID, ItemId, MrnId, IssuedQty, WarehouseId, StockMaintainingType, BatchId, PrevReturnQty) {
            e.preventDefault();
            $('#ContentSection_hdnMrndInID').val(MrndInID);
            $('#ContentSection_hdnMrndID').val(MrndID);
            $('#ContentSection_hdnIssuesQty').val(IssuedQty);
            $('#ContentSection_hdnItemID').val(ItemId);
            $('#ContentSection_hdnMrnID').val(MrnId);
            $('#ContentSection_hdnWarehouseID').val(WarehouseId);
            $('#ContentSection_hdnStockMaintainingType').val(StockMaintainingType);
            $('#ContentSection_hdnBatchId').val(BatchId);
             $('#ContentSection_hdnPrevReturnedQty').val(PrevReturnQty);

            swal.fire({
                title: 'Are you sure?',
                html: "Add return quantity </br></br>"
                    + "<strong id='dd'>Return Quatity</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Return',
                cancelButtonText: 'Close',
                allowOutsideClick: false,
                preConfirm: function () {
                    if ($('#ss').val() == '') {
                        $('#dd').prop('style', 'color:red');
                        swal.showValidationError('Remarks Required');
                        return false;
                    }
                    else {
                        $('#ContentSection_hdnQty').val($('#ss').val());
                    }

                }
            }
            ).then((result) => {
                if (result.value) {

                    $('#ContentSection_lbtnReturn').click();

                } else if (result.dismiss === Swal.DismissReason.cancel) {

                }
            });


        }

        //function ReturnStock(e, MrndInID, MrndID, ItemId, MrnId, IssuedQty, WarehouseId, StockMaintainingType) {
        //    e.preventDefault();
        //    $('#ContentSection_hdnMrndInID').val(MrndInID);
        //    $('#ContentSection_hdnMrndID').val(MrndID);
        //    $('#ContentSection_hdnIssuesQty').val(IssuedQty);
        //    $('#ContentSection_hdnItemID').val(ItemId);
        //    $('#ContentSection_hdnMrnID').val(MrnId);
        //    $('#ContentSection_hdnWarehouseID').val(WarehouseId);
        //    $('#ContentSection_hdnStockMaintainingType').val(StockMaintainingType);

        //    if (StockMaintainingType == 1) {
        //        $('#mdlReturnAvg').modal('show');
        //    }
        //    else {
        //        $('#mdlReturnBatch').modal('show');
        //    }
        //}
    </script>
</asp:Content>

