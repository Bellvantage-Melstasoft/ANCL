<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="ApproveExpensePR.aspx.cs" Inherits="BiddingSystem.ApproveExpensePR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style>
        table#ContentSection_gvDataTable tbody tr td {
            white-space: nowrap;
            border: 1px solid #f8f8f8;
            vertical-align: middle;
        }

            table#ContentSection_gvDataTable tbody tr td.Description {
                white-space: normal !important;
            }

        .required {
            color: red;
            font-weight: bold;
        }

        .paddingRight {
            margin-right: 10px;
        }
    </style>



    <section class="content-header">
    <h1>Manage Expense Type - Inventory Report / Information Report</h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Inventory Report / Information Report</li>
        </ol>
    </section>

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div class="modal fade" id="mdlCapexDocs" role="dialog">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-body">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">CAPEX DOCUMENTS</h4>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvCapexDocs" runat="server" CssClass="table table-responsive"
                                                        GridLines="None" HeaderStyle-BackColor="#275591" HeaderStyle-ForeColor="White"
                                                        AutoGenerateColumns="false" EmptyDataText="No Documents Found" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                                        <Columns>
                                                            <asp:BoundField DataField="PrId" HeaderStyle-CssClass="hidden"
                                                                ItemStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                                                ItemStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                            <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                                ItemStyle-CssClass="hidden" />
                                                            <asp:TemplateField HeaderText="Preview">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" href='<%#Eval("FilePath")%>'>View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <section class="content">
               <div class="box box-info" >
                <div class="box-header with-border">
                    <h3 class="box-title" >Inventory Report</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                    </div>
                </div>
  
                <div class="box-body">
                    <div class="co-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvDataTable" runat="server" AutoGenerateColumns="False" Caption="Add Item Available Quantity"
                                HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" EmptyDataText="No Items Found" CssClass="table table-responsive" >
				            <Columns>					
					            <asp:BoundField DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden PrId"></asp:BoundField>
                                <asp:BoundField DataField="PrdId" HeaderText="PrdId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden PrdId"></asp:BoundField>
					            <asp:BoundField DataField="ItemId" HeaderText="ItemId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden ItemId"></asp:BoundField>
					            <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-CssClass="ItemName"/>
					            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="Description"/>
					            <%--<asp:BoundField DataField="RequestedQty" HeaderText="Requested Quantity" ItemStyle-CssClass="RequestedQty" /> --%>
                                <asp:TemplateField HeaderText ="Requested Quantity">
						            <ItemTemplate>
						            <asp:Label ID="lblReqQty" type="text" runat="server" Text='<%#Eval("RequestedQty")%>'></asp:Label>
                                    <asp:label ID="lblUnit"  type="text" runat="server" Text='<%#Eval("MeasurementShortName")%>'></asp:label>
					            </ItemTemplate>
					            </asp:TemplateField>
                                <asp:BoundField DataField="EstimatedAmount" HeaderText="Estimated Cost" ItemStyle-CssClass="EstimatedAmount" DataFormatString = "{0:N2}"/>
                                <asp:TemplateField HeaderText ="Warehouse Stock">
						            <ItemTemplate>
						            <%--<asp:TextBox ID="txtAvailableQty" CssClass="clAvailableQty" type="decimal" min="0" runat="server" Text='<%#Eval("AvailableQty")%>' OnClick='<%#"WarehousestockAvailability(event,"+Eval("WarehouseStock").ToString()+", this)" %>' ></asp:TextBox>--%>
                                    <asp:TextBox ID="txtAvailableQty" CssClass="clAvailableQty" type="decimal" min="0" runat="server" Text='<%#Eval("AvailableQty")%>' OnClick='<%#"WarehousestockAvailability(event,"+Eval("AvailableQty").ToString()+", "+Eval("StockMaintainingType").ToString()+",  this)" %>' ></asp:TextBox>
                                        <asp:label ID="txtUnit"  type="text" runat="server" Text='<%#Eval("WarehouseUnit")%>'></asp:label>
					            </ItemTemplate>
					            </asp:TemplateField>
                                <asp:TemplateField HeaderText ="Action">
						            <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnUpdate"  CssClass="btn btn-xs btn-warning" Text="Update" OnClick="btnWarehouseStockUpdate_Click" ></asp:LinkButton>
                                    </ItemTemplate>
					            </asp:TemplateField>	
                                <asp:BoundField DataField="WarehouseId" HeaderText="WarehouseId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden "></asp:BoundField>
                                <asp:BoundField DataField="DetailId" HeaderText="Measurement Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden "></asp:BoundField>
                                <asp:BoundField DataField="DetailIdWHItem" HeaderText="Stock Measurement Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden "></asp:BoundField>
                                <asp:BoundField DataField="WarehouseUnit" HeaderText="Stock Measurement" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden WarehouseUnit"></asp:BoundField>
				                <asp:BoundField DataField="StockMaintainingType" HeaderText="Stock Maintaining Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden StockMaintainingType"></asp:BoundField>
				           
                            </Columns>
			            </asp:GridView>
                    </div>
                </div>
                </div>
                </div>
                <div class="box box-info" id="divPRExpenseType" runat="server" visible="true">
                    <div class="box-header with-border">
                        <h3 class="box-title" >Approve PR Expense Type & Budget Details </h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                <div class="box-body">
                  <div class="col-md-6">
	                <div class="row">
		                <div class="col-md-6">
			                <div class="form-group">
			                <label for="Expense">Expense Type</label>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                    <asp:RadioButton ID="rdoCapitalExpense" runat="server"   GroupName="Expense"  Checked onclick="HideExpense('CapitalExpense')" >
                                    </asp:RadioButton>
                                    </span>
                                    <asp:TextBox ID="txtCapitalExpense" disabled="disabled" runat="server"  class="form-control" Text="Capital Expense" ></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="Expense" style="visibility:hidden">Expense Type</label>
                                <div class="input-group">
                                    <span class="input-group-addon">
                                        <asp:RadioButton ID="rdoOperationalExpense"  runat="server" onclick="HideExpense('OperationalExpense')" GroupName="Expense"  ></asp:RadioButton>
                                    </span>
                                    <asp:TextBox ID="txtOperationalExpense" disabled="disabled" runat="server" class="form-control" Text="Operational Expense"></asp:TextBox>
                                </div>
                            </div>  
                        </div>
                    </div>
                <div class="form-group">
		                <label for="estimatedCost">Estimated Cost</label><label id="Label3" style="color:red;"></label>
	                   <asp:TextBox ID="txtEstimatedCost"  runat="server" readonly="true" CssClass="form-control" value="0"></asp:TextBox>
	                </div>
                
                <div id="divBudget2" runat="server">
                    <div class="row">
		                 <div class="col-md-6">
			                    <div class="form-group">
				                    <label for="budget">Budget</label>
				                    <div class="input-group">
						                    <span class="input-group-addon">
						                    <asp:RadioButton ID="rdoBudgetEnable" runat="server"   onclick="showHideBudgetRemark(this.value)" GroupName="Budget"  Checked>
						                    </asp:RadioButton>
						                    </span>
						                    <asp:TextBox ID="txtBudgetYes" runat="server"  class="form-control" Text="Yes"></asp:TextBox>
				                    </div>
			                    </div>
		                    </div>
				         <div class="col-md-6">
				                        <div class="form-group">
				                        <label for="budget" style="visibility:hidden">Budget</label>
					                        <div class="input-group">
						                    <span class="input-group-addon">
						                        <asp:RadioButton ID="rdoBudgetDisable"  runat="server"  onclick="showHideBudgetRemark(this.value)" GroupName="Budget"  ></asp:RadioButton>
						                    </span>
					                        <asp:TextBox ID="txtBudgetNo" runat="server" class="form-control" Text="No"></asp:TextBox>
				                        </div>
				                        </div>
				                    </div>
			        </div>
				    <div class="form-group" id='divBudgetRemark' runat="server">
					    <label for="budgetRemark">Remark</label><span class="required"> *</span><label id="lblBudgetRemark" style="color:red;"></label>
				        <asp:TextBox ID="txtBudgetRemark" TextMode="MultiLine" runat="server"  CssClass="form-control"></asp:TextBox>
				    </div>
                    <div id="divBudgetAmountInfo" runat="server">
			            <div class="form-group">
					        <label for="budgetAmount">Budget Amount</label><span class="required"> *</span><label id="lblBudgetAmount" style="color:red;"></label>
				            <asp:TextBox ID="txtBudgetAmount"  runat="server" value="0"  type="number" CssClass="form-control"></asp:TextBox>
				        </div>
				        <div class="form-group">
					        <label for="budgetInformation">Budget Information</label><span class="required"> *</span><label id="lblBudgetInformation" style="color:red;" ></label>
				            <asp:TextBox ID="txtBudgetInformation" TextMode="MultiLine"  runat="server"  CssClass="form-control"></asp:TextBox>
				        </div>
                    </div>
			        </div>
                </div>

                <div class="col-sm-12">
                    <div class="col-sm-6">
                        <asp:Button ID="btnBudgetUpdate" runat="server" Text="Update" OnClientClick="return ValidateExpense()"  Enabled="true"  CssClass="btn btn-warning pull-right"  OnClick="btnBudgetUpdate_Click"></asp:Button>                          
                        <input type="button"   class="btn btn-danger pull-right"  value="Reject" onclick="rejectPR(event)"  style="margin-right: 10px;"></input>
                        <input type="button"  class="btn btn-primary pull-right"   value="Approve" onclick="approvePR(event)" style="margin-right: 10px;"></input>                         
                        <asp:Button runat="server" ID="btnCapexDocs" CssClass="btn btn-info pull-right paddingRight" Text="Capex Docs" OnClick="btnCapexDocs_Click"  />
                          </div>
                     <asp:Button ID="btnApprovehnd" runat="server" OnClick="btnPrExpenseApprove_Click" CssClass="hidden" />
                     <asp:Button ID="btnRejecthnd" runat="server" onclick="btnPrExpenseReject_Click"  CssClass="hidden" />
                     <asp:HiddenField ID="hdnRemarks" runat="server" />
                </div>
                </div>
             </div>
    </section>

                <asp:HiddenField ID="hdnAddStock" runat="server" />
                <asp:HiddenField ID="hdnAddStockPrice" runat="server" />
                <asp:HiddenField ID="hdnExpdate" runat="server" />
                <%--<asp:Button ID="btnAddToStockhnd" runat="server" onclick="btnAddToStock_Click"  CssClass="hidden" />--%>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>

    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>

    <script type="text/javascript">
        function showHideBudgetRemark(obj) {
            if (obj != "rdoBudgetEnable") {
                $("[id *= divBudgetRemark]").css("display", "block");
                $("[id *= divBudgetAmountInfo]").css("display", "none");
            }
            else {
                $("[id *= divBudgetRemark]").css("display", "none");
                $("[id *= divBudgetAmountInfo]").css("display", "block");
            }
        }

        function HideExpense(rdo) {
            if (rdo == 'CapitalExpense') {
                $("[id *= divBudget2]").css("display", "block")
                if ($("[id *= rdoBudgetDisable]").is(':checked')) {
                    showHideBudgetRemark("rdoBudgetDisable");
                } else {
                    showHideBudgetRemark("rdoBudgetEnable");
                }
            } else if (rdo == 'OperationalExpense') {
                $("[id *= divBudget2]").css("display", "none");
            }
        }

        function ValidateExpense() {
            $("#lblBudgetRemark").text("");
            $("#lblBudgetAmount").text("");
            $("#lblBudgetInformation").text("");
            if ($("[id *= rdoBudgetDisable]").is(':checked') && $("[id *= txtBudgetRemark]").val() == "") {
                $("#lblBudgetRemark").text("Fill This Feild")
                return false;
            }
            if ($("[id *= rdoBudgetEnable]").is(':checked')) {
                if ($("[id *= txtBudgetAmount]").val() == "") {
                    $("#lblBudgetAmount").text("Fill This Feild")
                    return false;
                } else if ($("[id *= txtBudgetInformation]").val() == "") {
                    $("#lblBudgetInformation").text("Fill This Feild")
                    return false;
                }
            }
        }

        function approvePR(e) {
            e.preventDefault();
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Approve</strong> the PR Expense?</br></br>"
                    + "<strong id='dd'>Remarks</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required' value='Approved'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Approve',
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
                    $('#ContentSection_btnApprovehnd').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                }
            });
        }

        function rejectPR(e) {
            e.preventDefault();
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Reject</strong> the PR Expense?</br></br>"
                    + "<strong id='dd'>Remarks</strong>"
                    + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Reject',
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
                    $('#ContentSection_btnRejecthnd').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                }
            });
        }

        function WarehousestockAvailability(e, stockValue, stockMaintainingType, obj) {

            if (stockValue == "0.00") {


                var itemName = $(obj).closest("tr").find("td.ItemName").text();
                var unit = $(obj).closest("tr").find("td.WarehouseUnit").text();

                if (stockMaintainingType != 1) {

                    swal.fire({
                        title: 'Add Stock',
                        html: "Please enter folowing details</br></br>"
                            + "<label>Item Name : </label><label>" + itemName + "</label><br/>"
                            + "<div class='col-sm-12' style='padding-bottom: 5px;'>"
                            + "<strong style='float: left'>Item Quantity in " + unit + " : </strong>"
                            + "<input style='float: right;width: 70%;' id='ItemQty' type='number' class ='form-control' required='required'/> </div></br>"
                            + "<div class='col-sm-12'>"
                            + "<strong style='float: left'>Item Unit Price per " + unit + " : </strong>"
                            + "<input id='ItemPrice' style='float: right;width: 70%;' type='number' class ='form-control' required='required'/></div></br>"
                            + "<div class='col-sm-12'>"
                            + "<strong style='float: left'>Batch Expiry Date : </strong>"
                            + "<input id='expDate' style='float: right;width: 70%;' type='date' class ='form-control' required='required'/></div></br>",
                        type: 'warning',
                        cancelButtonColor: '#d33',
                        showCancelButton: true,
                        showConfirmButton: true,
                        confirmButtonText: 'OK',
                        cancelButtonText: 'CANCEL',
                        allowOutsideClick: false,
                        preConfirm: function () {
                            if ($('#ItemQty').val() == '') {
                                $('#dd1').prop('style', 'color:red');
                                swal.showValidationError('Quantity Required');
                                return false;
                            } else {
                                $('#ContentSection_hdnAddStock').val($('#ItemQty').val());

                            }
                            if ($('#ItemPrice').val() == '') {
                                $('#dd2').prop('style', 'color:red');
                                swal.showValidationError('Price Required');
                                return false;
                            } else {
                                $('#ContentSection_hdnAddStockPrice').val($('#ItemPrice').val());

                            }

                            $('#ContentSection_hdnExpdate').val($('#expDate').val());


                        }
                    }


                    ).then((result) => {
                        if (result.value) {
                            $(obj).closest('tr').find('.clAvailableQty').val($('#ItemQty').val())
                            // $('#ContentSection_btnAddToStockhnd').click();
                        } else if (result.dismiss === Swal.DismissReason.cancel) {
                        }
                    });
                }


                else {
                    swal.fire({
                        title: 'Add Stock',
                        html: "Please enter folowing details</br></br>"
                            + "<label>Item Name : </label><label>" + itemName + "</label><br/>"
                            + "<div class='col-sm-12' style='padding-bottom: 5px;'>"
                            + "<strong style='float: left'>Item Quantity in " + unit + " : </strong>"
                            + "<input style='float: right;width: 70%;' id='ItemQty' type='number' class ='form-control' required='required'/> </div></br>"
                            + "<div class='col-sm-12'>"
                            + "<strong style='float: left'>Item Unit Price per " + unit + " : </strong>"
                            + "<input id='ItemPrice' style='float: right;width: 70%;' type='number' class ='form-control' required='required'/></div></br>",
                        type: 'warning',
                        cancelButtonColor: '#d33',
                        showCancelButton: true,
                        showConfirmButton: true,
                        confirmButtonText: 'OK',
                        cancelButtonText: 'CANCEL',
                        allowOutsideClick: false,
                        preConfirm: function () {
                            if ($('#ItemQty').val() == '') {
                                $('#dd1').prop('style', 'color:red');
                                swal.showValidationError('Quantity Required');
                                return false;
                            } else {
                                $('#ContentSection_hdnAddStock').val($('#ItemQty').val());

                            }
                            if ($('#ItemPrice').val() == '') {
                                $('#dd2').prop('style', 'color:red');
                                swal.showValidationError('Price Required');
                                return false;
                            } else {
                                $('#ContentSection_hdnAddStockPrice').val($('#ItemPrice').val());

                            }


                        }
                    }


                    ).then((result) => {
                        if (result.value) {
                            $(obj).closest('tr').find('.clAvailableQty').val($('#ItemQty').val())
                            // $('#ContentSection_btnAddToStockhnd').click();
                        } else if (result.dismiss === Swal.DismissReason.cancel) {
                        }
                    });
                }
            }

        }
    </script>
</asp:Content>
