<%@ Import Namespace="Newtonsoft.Json" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="ApproveExpenseMRN.aspx.cs" Inherits="BiddingSystem.ApproveExpenseMRN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style>
         table#ContentSection_gvDataTable tbody tr td {
            white-space: nowrap;
            border: 1px solid #f8f8f8;
            vertical-align: middle;
        }
        table#ContentSection_gvDataTable tbody tr td.Description {
            white-space: normal!important;
        }
        .required {
            color: red;
            font-weight: bold;
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
              <section class="content" >
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
					            <asp:BoundField DataField="MrnId" HeaderText="MrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden MrnId"></asp:BoundField>
                                <asp:BoundField DataField="MrndId" HeaderText="MRND_ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden MrndId"></asp:BoundField>
					            <asp:BoundField DataField="ItemId" HeaderText="ItemId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden ItemId"></asp:BoundField>
					            <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-CssClass="ItemName"/>
					            <asp:BoundField DataField="Description" HeaderText="Description" ItemStyle-CssClass="Description"/>
					            <%--<asp:BoundField DataField="RequestedQty" HeaderText="Requested Quantity" ItemStyle-CssClass="RequestedQty" />--%> 
                                <asp:TemplateField HeaderText ="Requested Quantity">
						            <ItemTemplate>
						            <asp:Label ID="lblReqQty" type="text" runat="server" Text='<%#Eval("RequestedQty")%>'></asp:Label>
                                    <asp:label ID="lblUnit"  type="text" runat="server" Text='<%#Eval("MeasurementShortCode")%>'></asp:label>
					            </ItemTemplate>
					            </asp:TemplateField>
                                <asp:BoundField DataField="EstimatedAmount" HeaderText="Estimated Cost" ItemStyle-CssClass="EstimatedAmount" />
                                <asp:TemplateField HeaderText ="Department Stock">
						            <ItemTemplate>
						            <asp:TextBox ID="txtAvailableQty"  type="number" min="0" runat="server" Text='<%#Eval("DepartmentStock")%>'></asp:TextBox>	
                                    <asp:label ID="txtUnit"  type="text" runat="server" Text='<%#Eval("MeasurementShortCode")%>'></asp:label>
					            </ItemTemplate>
					            </asp:TemplateField>
                                <asp:TemplateField HeaderText ="Action">
						            <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="lbtnUpdate"  CssClass="btn btn-xs btn-warning" Text="Update" OnClick="btnDepartmentStockUpdate_Click" ></asp:LinkButton>
                                    </ItemTemplate>
					            </asp:TemplateField>								
				            </Columns>
			            </asp:GridView>
                    </div>
                </div>
                </div>

               <%-- <div class="box-footer">
                    <div class="col-md-12">
                        <span>
                            <asp:Button ID="btnDepartmentStockUpdate" runat="server" Enabled="true" Text="Update"  style="float:right" CssClass="btn btn-warning"  OnClick="btnDepartmentStockUpdate_Click"></asp:Button>               
                        </span>  
                    </div>
                 </div>--%>
                </div>
                <div class="box box-info" id="divMRNExpenseType" runat="server" visible="true">
                    <div class="box-header with-border">
                        <h3 class="box-title" >Approve MRN Expense Type & Budget Details </h3>
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
                                    <asp:RadioButton ID="rdoCapitalExpense" runat="server"   GroupName="Expense"  onclick="HideExpense('CapitalExpense')" >
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
                        <asp:Button ID="btnBudgetUpdate" runat="server" Text="Update" OnClientClick="return ValidateExpense()"  Enabled="true"  CssClass="btn btn-warning pull-right"  OnClick="btnMRNExpenseUpdate_Click"></asp:Button>                          
                        <input type="button"   class="btn btn-danger pull-right"  value="Reject" onclick="rejectMRN(event)"  style="margin-right: 10px;"></input>
                        <input type="button"  class="btn btn-primary pull-right"   value="Approve" onclick="approveMRN(event)" style="margin-right: 10px;"></input>                         
                    </div>
                     <asp:Button ID="btnApprovehnd" runat="server" OnClick="btnMRNExpenseApprove_Click" CssClass="hidden" />
                     <asp:Button ID="btnRejecthnd" runat="server" onclick="btnMRNExpenseReject_Click"  CssClass="hidden" />
                     <asp:HiddenField ID="hdnRemarks" runat="server" />
                </div>
                </div>
             </div>
    </section>
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

        function approveMRN(e) {
            e.preventDefault();
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Approve</strong> the MRN Expense?</br></br>"
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

        function rejectMRN(e) {
            e.preventDefault();
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to <strong>Reject</strong> the MRN Expense?</br></br>"
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
    </script>
</asp:Content>
