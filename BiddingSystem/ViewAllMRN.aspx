<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewAllMRN.aspx.cs" Inherits="BiddingSystem.ViewAllMRN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        .ui-datepicker-calendar {
            display: none;
        }
        .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }

        .expand_caret {
            transform: scale(1.6);
            margin-left: 8px;
            margin-top: -4px;
        }

        a[aria-expanded='false'] > .expand_caret {
            transform: scale(1.6) rotate(-90deg);
        }

        input[type="date"]:not(.has-value):before {
            color: #ada5a5;
            content: attr(placeholder);
        }

        table#ContentSection_gvMrn tbody tr td {
            white-space: nowrap;
            border: 1px solid #f8f8f8;
            vertical-align: middle;
        }
        table#ContentSection_gvMrn tbody tr td.Description {
            white-space: normal!important;
        }
        table#ContentSection_gvMrn tbody tr:nth-child(1) th {
            position:sticky;
            top: 1px;
            background: #3C8DBC;
            color: white;
        }
       .fixed {
          width: 100%;  
        }
       .itemCol {
                width: 50px;
        }
    </style>
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
 <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>--%>

    <script src="AdminResources/googleapis/googleapis-jquery.min.js"></script>
    <link rel="stylesheet" href="AdminResources/googleapis/googleapis-jquery-ui.css">
    <script src="AdminResources/googleapis/googleapis-jquery-ui.js"></script>

    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
<form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>

                <div id="mdlViewMRN" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%;">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">MRN Log</h4>
                            </div>
                            <div class="modal-body row">   
                                    <div class="col-xs-12">
                                        <div> &nbsp</div>
                                        <div style="color: black;">
                                         <asp:GridView ID="gvMrnDetails" runat="server" OnRowDataBound="gvMrnDetails_RowDataBound"
                                          CssClass="table table-responsive" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No MRNs Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
                                             <Columns>
                                                <asp:TemplateField HeaderText="Log">
                                                    <ItemTemplate>
                                                        <img alt=""
                                                            style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png" />
                                                        <asp:Panel ID="pnlStatusLog" runat="server"
                                                            Style="display: none">
                                                            <asp:GridView ID="gvStatusLog"
                                                                runat="server"
                                                                CssClass="table table-responsive ChildGridTwo"
                                                                GridLines="None"
                                                                AutoGenerateColumns="false"
                                                                EmptyDataText="No Log Found" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White">
                                                                <Columns>
                                                                    <asp:BoundField
                                                                        DataField="UserName"
                                                                        HeaderText="Logged By" />
                                                                    <asp:BoundField
                                                                        DataField="LoggedDate"
                                                                        HeaderText="Logged Date and Time" />
                                                                    <asp:TemplateField  HeaderText="Current Status">
                                                                        <ItemTemplate>
                                                                            <asp:Label
                                                                                runat="server"
                                                                                Text='<%# Eval("LogName").ToString()%>' CssClass="label label-info"/>
                                                                          <%--  <asp:Label
                                                                                runat="server"
                                                                                Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                                Text="MRN MODIFIED" CssClass="label label-warning" />
                                                                            <asp:Label
                                                                                runat="server"
                                                                                Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                Text="MRN APPROVED" CssClass="label label-success" />
                                                                            <asp:Label
                                                                                runat="server"
                                                                                Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                                                Text="MRN REJECTED" CssClass="label label-danger" />
                                                                            <asp:Label
                                                                                runat="server"
                                                                                Visible='<%# Eval("Status").ToString() == "5" ? true : false %>'
                                                                                Text="STOCK ISSUE" CssClass="label label-success" />
                                                                            <asp:Label
                                                                                runat="server"
                                                                                Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                                                Text="ADDED TO PR"  CssClass="label label-success"/>
                                                                            <asp:Label
                                                                                runat="server"
                                                                                Visible='<%# Eval("Status").ToString() == "7" ? true : false %>'
                                                                                Text="TERMINATED"  CssClass="label label-danger"/>
                                                                            <asp:Label
                                                                                runat="server"
                                                                                Visible='<%# Eval("Status").ToString() == "8" ? true : false %>'
                                                                                Text="Stock Received" CssClass="label label-warning" />--%>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MrnId" HeaderText="MRNId"
                                                    HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="MrndId" HeaderText="MRNdId"
                                                    HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />                                               
                                                <asp:BoundField DataField="ItemId"
                                                    HeaderText="Item Id"
                                                    HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemName"
                                                    HeaderText="Item Name" />
                                                <asp:BoundField DataField="MeasurementShortName"
                                                    HeaderText="Unit" />
                                                <asp:BoundField DataField="RequestedQty"
                                                    HeaderText="Request Quantity" />
                                                <asp:BoundField DataField="ReceivedQty"
                                                    HeaderText="Received Quantity" />
                                                <asp:BoundField DataField="IssuedQty"
                                                    HeaderText="Issued Quantity" />
                                                <asp:TemplateField HeaderText="MRN Status">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Text='<%# Eval("StatusName").ToString()%>' CssClass="label label-info"/>
                                                 <%--   <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                        Text="Added to PR" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                        Text="Partially Issued" CssClass="label label-info"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                        Text="Fully Issued" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                        Text="Delivered" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "5" ? true : false %>'
                                                        Text="Received" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                        Text="Terminated" CssClass="label label-danger"/> --%>                                                                                          
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </div>
                                <div class="col-xs-12">
                                    <label>Updated Log</label>
                                    <asp:GridView ID="gvUpdatedLog" runat="server" 
                                          CssClass="table table-responsive" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No updated Log Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
                                             <Columns>
                                                <asp:BoundField DataField="MrnId" HeaderText="MRNId"
                                                    HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="UpdatedByName"
                                                    HeaderText="Updated By" />
                                                <asp:BoundField DataField="UpdatedDate"
                                                    HeaderText="Updated Date" />
                                                 <asp:BoundField DataField="UpdateRemarks"
                                                    HeaderText="Remark" />
                                            </Columns>
                                        </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                 <section class="content-header">
                    <h1>View All Material Request Notes<small></small></h1>
                    <ol class="breadcrumb">
                        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">View Material Request Notes </li>
                    </ol>
                </section>
                <br />
                <section class="content">
     <div class="panel panel-default" id="panelMRNBasicSearch" runat="server">
                    <div class="panel-heading">
                        <h3 class="panel-title">Basic Search
                            <a class="arrowdown"  data-target="#basicSearch" data-toggle="collapse"  aria-expanded="false">
                            <span class="expand_caret caret" ></span>
                            </a>
                        </h3>
                    </div>
                    <div class="panel-body collapse" id="basicSearch">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:RadioButton ID="rdbMonth" runat="server" Checked="true" GroupName="basicSearch"></asp:RadioButton>
                                <b>Search by Month</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                                <br>
                                <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt form-control"></asp:TextBox>         
                            </div>
                           <div class="col-md-6">
                            <asp:RadioButton ID="rdbCode" runat="server" GroupName="basicSearch"></asp:RadioButton>
                              <b> Search by MRN Code</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                               <asp:TextBox ID="txtMrnCode" runat="server" CssClass="form-control" PlaceHolder="LCL1 / IMP1" ></asp:TextBox>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-md-11">
                                <asp:Image  runat="server" ID="loadingImage1" class="loadingImage pull-right hidden"   src="AdminResources/images/Spinner-0.6s-200px.gif" style="margin-top:5px;max-height: 40px;" />                                
                            </div>
                            <div class="col-md-1">
                                <asp:Button ID="btnBasicSearch" ValidationGroup="btnBasicSearch" OnClientClick="return BSFieldValidate()" OnClick="btnBasicSearch_Click" runat="server" Text="Search" style="margin-top: 10px;" CssClass ="btn btn-info pull-right" ></asp:Button>
                            </div>
                          </div>
                    </div>
                </div>

                <div class="panel panel-default" id="panelMRNAdvanceSearch" runat="server">
                   <div class="panel-heading">
                        <h3 class="panel-title">Advance Search
                             <a class="arrowdown"  data-target="#advancedSearch" data-toggle="collapse" aria-expanded="false">
                            <span class="expand_caret caret"></span>
                             </a>
                        </h3>            
                   </div>
                   <div class="panel-body collapse" id="advancedSearch">
                    <div class="row">
                      <div class="col-md-3">
                           <div class="form-group">
                               <asp:CheckBox ID="chkDepartment" runat="server"  data-Control="ddlDepartment"></asp:CheckBox>
                                <label for="exampleInputEmail1">Select Department</label>
                              <asp:ListBox ID="ddlDepartment" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black;width:100%" >
                                </asp:ListBox>
                          </div>
                      </div>
                       <div class="col-md-3">
                            <div class="form-group">
                             <asp:CheckBox ID="chkWarehouse" runat="server" data-Control="ddlWarehouse"></asp:CheckBox>
                             <label for="exampleInputEmail1">Select Warehouse</label>
                             <asp:ListBox ID="ddlWarehouse" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black;width:100%">
                             </asp:ListBox>
                            </div>
                       </div>
                       <div class="col-md-3">
                           <div class="form-group">
                           <asp:CheckBox ID="chlPurchaseType" runat="server" data-Control="ddlPurchaseType"></asp:CheckBox>
                           <label for="exampleInputEmail1">Purchase Type</label>
                                <asp:DropDownList ID="ddlPurchaseType" runat="server" CssClass="form-control" > 
                                 <asp:ListItem Value="1">Local</asp:ListItem>  
                                 <asp:ListItem Value="2">Import</asp:ListItem> 
                                </asp:DropDownList>
                            </div>
                        </div>
                       <div class="col-md-3">
                          <div class="form-group">
                           <asp:CheckBox ID="chkPurchaseProcedure" runat="server" data-Control="ddlPurchaseProcedure"></asp:CheckBox>
                            <label for="exampleInputEmail1">Purchase Procedure</label>
                             <asp:DropDownList ID="ddlPurchaseProcedure" runat="server" CssClass="form-control" > 
                                 <asp:ListItem Value="1">Normal</asp:ListItem>  
                                 <asp:ListItem Value="3">Cover</asp:ListItem> 
                             </asp:DropDownList>
                          </div>
                        </div>
                    </div>
                    <div class="row">
                      <div class="col-md-4">
                         <div class="form-group">
                            <asp:CheckBox ID="chkCreatedDate" runat="server" data-Control="chkCreatedDate"></asp:CheckBox>
                                <label>Created Date</label> <label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                           <div class="form-group">
                            <asp:TextBox ID="txtStartCreatdDate" runat="server" Width="50%" CssClass="form-control pull-left customDate"   onchange="dateChange(this)"   type="date" data-date="" data-date-format="DD MMM YYYY"  placeholder="from"  autocomplete="off" ></asp:TextBox>                              
                            <asp:TextBox ID="txtEndCreatedDate" runat="server" Width="50%"  CssClass="form-control pull-right customDate" onchange="dateChange(this)"   type="date" data-date="" data-date-format="DD MMM YYYY" placeholder="to"  autocomplete="off"  ></asp:TextBox>  
                               </div>
                         </div>
                       </div>
                       <div class="col-md-4">
                            <div class="form-group">
                            <asp:CheckBox ID="chkExpectedDate" runat="server" data-Control="chkExpectedDate"></asp:CheckBox>
                                <label>Expected Date</label><label class="lblerror hidden" style="color:red;">*Fill this field</label>
                                <div class="form-group">
                            <asp:TextBox ID="txtStartExpectedDate" runat="server" Width="50%" CssClass="form-control pull-left customDate" onchange="dateChange(this)"   type="date" data-date="" data-date-format="DD MMM YYYY"  placeholder="from"  autocomplete="off" ></asp:TextBox>
                            <asp:TextBox ID="txtEndExpectedDate" runat="server" Width="50%"  CssClass="form-control pull-right customDate" onchange="dateChange(this)"   type="date" data-date="" data-date-format="DD MMM YYYY"  placeholder="to"  autocomplete="off"  ></asp:TextBox>  
                                    </div>
                            </div>
                        </div>
                       <div class="col-md-4">
                            <div class="form-group">
                               <asp:CheckBox ID="chkExpenseType" runat="server" data-Control="ddlExpenseType"></asp:CheckBox>
                             <label for="exampleInputEmail1">Expense Type</label>
                             <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="form-control" >
                                  <asp:ListItem Value="1">Capital Expense</asp:ListItem>  
                                 <asp:ListItem Value="2">Operational Expense</asp:ListItem>  
                             </asp:DropDownList>
                            </div>
                            </div>
                     </div>
                    <div class="row">
                       <div class="col-md-6">
                        <div class="form-group">
                           <asp:CheckBox ID="chkMrnType" runat="server" data-Control="ddlMrnType"></asp:CheckBox>
                         <label for="exampleInputEmail1">MRN Type</label>
                         <asp:DropDownList ID="ddlMrnType" runat="server" CssClass="form-control" >
                              <asp:ListItem Value="1">Stock</asp:ListItem>  
                             <asp:ListItem Value="2">Non-Stock</asp:ListItem> 
                         </asp:DropDownList>
                         </div>
                        </div>
                       <div class="col-md-6">
                           <div class="form-group">
                               <div class="col-md-6">
                                    <asp:CheckBox ID="chkMaincategory" runat="server" data-Control="chkMaincategory"></asp:CheckBox>
                                   <label for="exampleInputEmail1">Category</label><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                                     <asp:DropDownList ID="ddlMainCategory" runat="server" CssClass="form-control" onselectedindexchanged="ddlMainCategory_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
                                   </div>
                                   <div class="col-md-6">
                                       <label for="exampleInputEmail1">Sub Category</label><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                                     <asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control" ></asp:DropDownList>
                                   </div>
                         </div>
                        </div>
                    </div>
                         <div class="row">
                              <div class="col-md-11">
                                  <label id="ASlblErrorMsg" class="pull-right hidden" style="color:red;" >*Select search option</label>
                                <asp:Image  runat="server" ID="loadingImage2" class="loadingImage pull-right hidden"   src="AdminResources/images/Spinner-0.6s-200px.gif" style="margin-top:5px;max-height: 40px;" />                                
                            </div>
                                <div class="col-md-1">
                                <asp:Button ID="btnAdvancedSearch" runat="server" Text="Search" OnClientClick="return ASFieldValidate()" OnClick="btnAdvancedSearch_Click" style="margin-top: 10px;"  CssClass ="btn btn-info pull-right" ></asp:Button>
                                </div>
                            </div>
                    </div>
                 </div>
                    
                <div class="panel panel-default" id="panelGridview" runat="server">
                   <div class="panel-heading">
                        <h3 class="panel-title">Material Request Notes</h3>
                   </div>
                   <div class="panel-body">
                       <div class="table-responsive">
                        <asp:GridView runat="server" ID="gvMrn" GridLines="None"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" CssClass="table table-responsive fixed tablegv" AutoGenerateColumns="false" DataKeyNames="MrnID" EmptyDataText="No records Found"
                            AllowPaging="true"  PageSize="15" OnPageIndexChanging="gvAllMrn_PageIndexChanging">

                             <Columns>
                                <asp:BoundField DataField="MrnID"  HeaderText="MRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:TemplateField HeaderText="MRN Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblMrnCode" Text='<%# Eval("MrnCode") == null ? "MRN0" : "MRN-"+Eval("MrnCode").ToString() %>'></asp:Label>
                           </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department" />
                                <asp:BoundField DataField="CreatedDate"  HeaderText="Created On"  DataFormatString="{0:dd-MM-yyyy}"/> <%--DataFormatString="{0:dd-MM-yyyy hh:mm tt}"--%>
                               <%-- <asp:BoundField DataField="RequiredFor"  HeaderText="Description" ItemStyle-CssClass="Description" />--%>
                                <asp:BoundField DataField="ItemName"  HeaderText="Items" ItemStyle-CssClass="itemCol" />
                                <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date"  DataFormatString="{0:dd-MM-yyyy}"/>
                                <asp:BoundField DataField="StatusName"  HeaderText="Status" />
                                <asp:TemplateField HeaderText="Mrn Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("MrnType").ToString() == "1" ? true : false %>'
                                            Text="Stock" />
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("MrnType").ToString() == "2" ? true : false %>'
                                            Text="Non-stock" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Purchasing Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseType").ToString() == "1" ? true : false %>'
                                            Text="Local" CssClass="label label-warning"/>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseType").ToString() == "2" ? true : false %>'
                                            Text="Import" CssClass="label label-success"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Purchasing Procedure">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseProcedure").ToString() == "1" ? true : false %>'
                                            Text="Normal" CssClass="label label-warning"/>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseProcedure").ToString() == "3" ? true : false %>'
                                            Text="Cover" CssClass="label label-success"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expense Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("ExpenseType").ToString() == "1" ? true : false %>'
                                            Text="Capital Expense" CssClass="label label-warning"/>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("ExpenseType").ToString() == "2" ? true : false %>'
                                            Text="Operational Expense" CssClass="label label-success"/>
                                    </ItemTemplate>
                                </asp:TemplateField>                                 
                                  <asp:TemplateField   HeaderText="History">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" ID="btnLog" Text="View Log" OnClick="btnLog_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CssClass="btn btn-sm btn-info" ID="lbtnView" Text="View" OnClick="lbtnView_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                           </div>
                   </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

      <script src="AdminResources/js/select2.full.min.js" type="text/javascript"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js" type="text/javascript"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js" type="text/javascript"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>
    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/moment.min.js" type="text/javascript"></script>

       <script type="text/javascript">

           function dateChange(obj) {
               obj.className = (obj.value != '' ? obj.className + ' has-value' : obj.className);
               if (obj.value) {
                   $(obj).attr('data-date', moment(obj.value, 'YYYY-MM-DD').format($(obj).attr('data-date-format')));
               } else {
                   $(obj).attr('data-date', '');
               }
           }

           Sys.Application.add_load(function () {

               $(function () {
                   $('.select2').select2();
               });

               $('.collapse').on('show.bs.collapse', function () {
                   $('.collapse.in').each(function () {
                       $(this).collapse('hide');
                   });
               });

               $("#basicSearch").collapse('show');
               var customDates = $(".customDate");
               for (x = 0 ; x < customDates.length ; ++x) {
                   if ($(customDates[x]).val() != "") {
                       $(customDates[x]).attr('data-date', moment($(customDates[x]).val(), 'YYYY-MM-DD').format($(customDates[x]).attr('data-date-format')));
                   }
               }

               $(function () {
                   $('.txtFDt').datepicker({
                       changeMonth: true,
                       changeYear: true,
                       showButtonPanel: true,
                       currentText: 'Present',
                       dateFormat: 'MM yy',
                       onClose: function (dateText, inst) {
                           //Get the selected month value
                           var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                           //Get the selected year value
                           var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                           //set month value to the textbox
                           $(this).datepicker('setDate', new Date(year, month, 1));
                       }
                   });

               });
           });

           function BSFieldValidate() {
               $("#ContentSection_loadingImage1").removeClass("hidden");
               for (x = 0; x < $("#basicSearch input[type=radio]").length; ++x) {
                   var radioObject = $("#basicSearch input[type=radio]")[x];
                   if ($(radioObject).is(":checked")) {
                       if ($($(radioObject).parent().find("input[type=text]")).val() == "") {
                           $($(radioObject).parent().find("label.lblerror")).removeClass("hidden");
                           return false;
                       } else {
                           $($(radioObject).parent().find("label.lblerror")).addClass("hidden");
                       }
                   }
               }
               return true;
           }

           function ASFieldValidate() {
               var flag = false; //To check if any one of the checkbox have not checked
               $("#ContentSection_loadingImage2").removeClass("hidden");
               for (x = 0; x < $("#advancedSearch input[type=checkbox]").length; ++x) {
                   var checkboxObject = $("#advancedSearch input[type=checkbox]")[x];
                   if ($(checkboxObject).is(":checked")) {
                       if ($($(checkboxObject).parent().parent().find("input[type=date]")).length > 0) {  // all for datetime
                           if ($($(checkboxObject).parent().parent().find("input[type=date]")).attr("data-date") == "") {
                               $($(checkboxObject).parent().parent().find("label.lblerror")).removeClass("hidden");
                               $("#ContentSection_loadingImage2").addClass("hidden");
                               return false;
                           } else {
                               $($(checkboxObject).parent().parent().find("label.lblerror")).addClass("hidden");
                           }
                       }
                       flag = true;   //
                   }
               }
               if ($("#ContentSection_chkMaincategory").is(":checked")) {
                   $($("#ContentSection_ddlMainCategory").parent().find("label.lblerror")).addClass("hidden");
                   if ($("#ContentSection_ddlMainCategory").val() != "") {
                       if ($("#ContentSection_ddlSubCategory").val() != "") {
                           $($("#ContentSection_ddlSubCategory").parent().find("label.lblerror")).addClass("hidden");
                       } else {
                           $($("#ContentSection_ddlSubCategory").parent().find("label.lblerror")).removeClass("hidden");
                           $("#ContentSection_loadingImage2").addClass("hidden");
                           return false;
                       }
                   } else {
                       $($("#ContentSection_ddlMainCategory").parent().find("label.lblerror")).removeClass("hidden");
                       $("#ContentSection_loadingImage2").addClass("hidden");
                       return false;
                   }
               }
               if (!flag) {
                   $("#ASlblErrorMsg").removeClass("hidden");
                   $("#ContentSection_loadingImage2").addClass("hidden");
                   return false;
               }
               return true;
           }
    </script>


</asp:Content>
