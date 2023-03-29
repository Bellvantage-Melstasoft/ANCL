<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewIssuedInventory.aspx.cs" Inherits="BiddingSystem.ViewIssuedInventory" %>

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
        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }

        .ChildGrid th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
        .ui-datepicker-calendar {
            display: none;
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
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>--%>
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />

    <script src="AdminResources/googleapis/googleapis-jquery.min.js"></script>
    <link rel="stylesheet" href="AdminResources/googleapis/googleapis-jquery-ui.css">
    <script src="AdminResources/googleapis/googleapis-jquery-ui.js"></script>

    <section class="content-header">
    <h1>
      View Issued Inventory
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Issued Inventory</li>
      </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>

                <div id="SuccessAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">Success</h4>
                            </div>
                            <div class="modal-body">
                                <p id="successMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-info" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="errorAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #ff0000;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">ERROR</h4>
                            </div>
                            <div class="modal-body">
                                <p id="errorMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-danger" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <section class="content" style="padding-top: 0px">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelApprovRejectMRN" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Delivered Inventory</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
              <div class="row">
                            <div class="col-md-5">
                                <asp:RadioButton ID="rdbMonthD" runat="server" Checked="true" GroupName="basicSearchD"></asp:RadioButton>
                                <b>Search by Delivered Month</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                                <br>
                                <asp:TextBox ID="txtFDtD" runat="server" CssClass="txtFDt form-control"  style="margin-bottom: 10px;"></asp:TextBox>         
                            </div>
                           <div class="col-md-5">
                            <asp:RadioButton ID="rdbCodeD" runat="server" GroupName="basicSearchD"></asp:RadioButton>
                              <b> Search by MRN Code</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                               <asp:TextBox ID="txtMrnCodeD" runat="server" CssClass="form-control" style="margin-bottom: 10px;" PlaceHolder="LCL1 / IMP1"></asp:TextBox>
                            </div>
                        
                         
                            <%--<div class="col-md-11">
                                <asp:Image  runat="server" ID="loadingImage1" class="loadingImage pull-right hidden"   src="AdminResources/images/Spinner-0.6s-200px.gif" style="margin-top:5px;max-height: 40px;" />                                
                            </div>--%>
                            <div class="col-md-2">
                                <asp:Button ID="btnBasicSearchDelivered" ValidationGroup="btnBasicSearchDelivered" OnClientClick="return BSFieldValidate()" OnClick="btnBasicSearchDelivered_Click" runat="server" Text="Search" style="margin-top: 20px;" CssClass ="btn btn-info " ></asp:Button>
                            </div>
                         </div>
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvReceivedInventory" GridLines="None" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                  CssClass="table table-responsive tablegv" AutoGenerateColumns="false" EmptyDataText="No records Found" AllowPaging="true"  PageSize="15" OnPageIndexChanging="gvReceivedInventory_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="MrndInID"  HeaderText="MRNDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MrndID"  HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ItemID"  HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                       
                         <asp:TemplateField HeaderText="MRN Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%#Eval("MrnCode") == null? "MRN0": "MRN-"+ Eval("MrnCode").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department Name"/>
                        <asp:BoundField DataField="IssuedQty"  HeaderText="Issued Qty"/>
                        <asp:BoundField DataField="ShortCode"  HeaderText="Unit" NullDisplayText="Not Found"/>
                        <asp:BoundField DataField="DeliveredUser"  HeaderText="Delivered By"/>
                        <asp:BoundField DataField="DeliveredOn" DataFormatString='<%$ appSettings:dateTimePattern %>' HeaderText="Delivered On"/>
                         <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClick="btnView_Click" ></asp:LinkButton>
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

                <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelMRN"  runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Issued Inventory</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
              <div class="row">
                            <div class="col-md-5">
                                <asp:RadioButton ID="rdbMonth" runat="server" Checked="true" GroupName="basicSearch"></asp:RadioButton>
                                <b>Search by Issued Month</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                                <br>
                                <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt form-control"  style="margin-bottom: 10px;"></asp:TextBox>         
                            </div>
                           <div class="col-md-5">
                            <asp:RadioButton ID="rdbCode" runat="server" GroupName="basicSearch"></asp:RadioButton>
                              <b> Search by MRN Code</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                               <asp:TextBox ID="txtMrnCode" runat="server" CssClass="form-control" PlaceHolder="LCL1 / IMP1" style="margin-bottom: 10px;" ></asp:TextBox>
                            </div>
                        
                         
                            <%--<div class="col-md-11">
                                <asp:Image  runat="server" ID="loadingImage1" class="loadingImage pull-right hidden"   src="AdminResources/images/Spinner-0.6s-200px.gif" style="margin-top:5px;max-height: 40px;" />                                
                            </div>--%>
                            <div class="col-md-2">
                                <asp:Button ID="btnBasicSearchIssued" ValidationGroup="btnBasicSearchIssued" OnClientClick="return BSFieldValidate()" OnClick="btnBasicSearchIssued_Click" runat="server" Text="Search" style="margin-top: 20px;" CssClass ="btn btn-info " ></asp:Button>
                            </div>
                         </div>
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                
              <asp:GridView runat="server" ID="gvDeliveredInventory" GridLines="None" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                  CssClass="table table-responsive tablegv" AutoGenerateColumns="false" DataKeyNames="MrndInID" EmptyDataText="No records Found"
                  AllowPaging="true"  PageSize="15" OnPageIndexChanging="gvDeliveredInventory_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="MrndInID"  HeaderText="MRNDIN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="MrndID"  HeaderText="MRND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="ItemID"  HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                         <asp:TemplateField HeaderText="MRN Code">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# "MRN-"+ Eval("MrnCode").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:BoundField DataField="ItemName"  HeaderText="Item Name" />
                        <asp:BoundField DataField="SubDepartmentName"  HeaderText="Department Name"/>
                        <asp:BoundField DataField="IssuedQty"  HeaderText="Issued QTY"/>
                         <asp:BoundField DataField="ShortCode"  HeaderText="Unit" NullDisplayText="Not Found"/>
                        <asp:BoundField DataField="IssuedUser"  HeaderText="Issued By"/>
                        <asp:BoundField DataField="IssuedOn" DataFormatString='<%$ appSettings:dateTimePattern %>' HeaderText="Issued On"/>

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:Button runat="server" ID="btnReceive" CssClass="btn btn-sm btn-success" Text="Deliver" OnClick="btnReceive_Click"></asp:Button>
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
        Sys.Application.add_load(function () {
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

                $('.txtFDtD').datepicker({
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
    </script>
</asp:Content>

