<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewPrStockAvailabilityExpenseApprove.aspx.cs" Inherits="BiddingSystem.ViewPrStockAvailabilityExpenseApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        /*table#ContentSection_gvPr tbody tr td {
            white-space: nowrap;
            border: 1px solid #f8f8f8;
            vertical-align: middle;
        }
           table#ContentSection_gvPr tbody tr td.Department ,table#ContentSection_gvPr tbody tr td.Description {
            white-space: normal!important;
        }
        table#ContentSection_gvPr tbody tr:nth-child(1) th {
            position:sticky;
            top: 1px;
            background: #3C8DBC;
            color: white;
        }*/

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

        table#ContentSection_gvPr tbody tr td {
            white-space: nowrap;
            border: 1px solid #f8f8f8;
            vertical-align: middle;
        }
        table#ContentSection_gvPr tbody tr td.Description {
            white-space: normal!important;
        }
        table#ContentSection_gvPr tbody tr:nth-child(1) th {
            position:sticky;
            top: 1px;
            background: #3C8DBC;
            color: white;
        }
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
                <section class="content-header">
                    <h1>Stock Availability / Expense Approval Purchase Request Notes<small></small></h1>
                    <ol class="breadcrumb">
                        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
                        <li class="active">Availability /Expense Approve Purchase Request Notes </li>
                    </ol>
                </section>
                <br />
                <section class="content">

                    <div class="panel panel-default" id="panelPRBasicSearch" runat="server">
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
                              <b> Search by PR Code</b><label class="lblerror hidden" style="color:red;" >*Fill this field</label>
                               <asp:TextBox ID="txtPrCode" runat="server" PlaceHolder="LCL1 / IMP1" CssClass="form-control"  ></asp:TextBox>
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

                
                <div class="panel panel-default" id="panelGridview" runat="server">
                   <div class="panel-heading">
                        <h3 class="panel-title">Purchase Request Notes</h3>
                   </div>
                   <div class="panel-body" style="overflow-x:scroll" >
                        <asp:GridView runat="server" ID="gvPr" GridLines="None" CssClass="table table-responsive tablegv" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"  AutoGenerateColumns="false" DataKeyNames="PrId" EmptyDataText="No records Found"
                            AllowPaging="true"  PageSize="15" OnPageIndexChanging="gvPrExApp_PageIndexChanging">
                        <Columns>
                        <asp:BoundField DataField="PrId"  HeaderText="PR ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="PR Code">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblPrCode" Text='<%# "PR-"+Eval("PrCode").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department" ItemStyle-CssClass="Department">
                            <ItemTemplate>
                                <asp:Label  runat="server" Text='<%# Eval("SubDepartmentName")!=null? Eval("SubDepartmentName").ToString(): "Stores" %>'   />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created On"  DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="RequiredFor"  HeaderText="Description" ItemStyle-CssClass="Description"/>
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date"  DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="WarehouseName"  HeaderText="Warehouse" />                       
                        <asp:TemplateField HeaderText="Pr Type">
                                    <ItemTemplate>
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PrType").ToString() == "1" ? true : false %>'
                                            Text="Stock" />
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PrType").ToString() == "2" ? true : false %>'
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
                                        <asp:Label
                                            runat="server"
                                            Visible='<%# Eval("PurchaseProcedure").ToString() == "2" ? true : false %>'
                                            Text="Cover(GRN)" CssClass="label label-info"/>
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
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" CssClass="btn btn-sm btn-info" Text="View" OnClick="lbtnView_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                   </div>
                </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

     <%--<script src="AdminResources/js/jquery-1.10.2.min.js"></script>--%>
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

            $('.select2').select2();

            $('.collapse').on('show.bs.collapse', function () {
                $('.collapse.in').each(function () {
                    $(this).collapse('hide');
                });
            });

            $("#basicSearch").collapse('show');
            var customDates = $(".customDate");
            for (x = 0 ; x < customDates.length ; ++x) {
                if ($(customDates[x]).val() != "") {
                    customDates[x].className = (customDates[x].value != '' ? customDates[x].className + ' has-value' : customDates[x].className);
                    $(customDates[x]).attr('data-date', moment($(customDates[x]).val(), 'YYYY-MM-DD').format($(customDates[x]).attr('data-date-format')));
                }
            }

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

        
    </script>

</asp:Content>
