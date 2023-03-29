<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewAllTR.aspx.cs" Inherits="BiddingSystem.ViewAllTR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
 <style type="text/css">
        .ui-datepicker-calendar {
            display: none;
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

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>--%>

    <script src="AdminResources/googleapis/googleapis-jquery.min.js"></script>
    <link rel="stylesheet" href="AdminResources/googleapis/googleapis-jquery-ui.css">
    <script src="AdminResources/googleapis/googleapis-jquery-ui.js"></script>
<form runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                
                <!-- Start : tr Modal -->
                <div id="mdlViewTR" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">TR Log</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                               
                                <div class="row">
                                    <div class="col-xs-12">


                                        <div>
                                            &nbsp
                                        </div>

                                        <!-- Start : Items Table -->
                                        <div style="color: black;">

                                         <asp:GridView ID="gvTRDetails" runat="server" OnRowDataBound="gvTRDetails_RowDataBound"
                                                                            CssClass="table table-responsive"
                                                                            GridLines="None" AutoGenerateColumns="false"
                                                                            EmptyDataText="No TRs Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
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
                                                                                            DataField="FirstName"
                                                                                            HeaderText="Logged By" />
                                                                                        <asp:BoundField
                                                                                            DataField="LoggedDate"
                                                                                            HeaderText="Logged Date and Time" />
                                                                                        <asp:TemplateField  HeaderText="Current Status">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                                                                    Text="TR CREATED" CssClass="label label-info"/>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                                                    Text="TR APPROVED" CssClass="label label-success" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                                                    Text="TR REJECTED" CssClass="label label-danger" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                                    Text="TR MODIFIED" CssClass="label label-warning" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                                                                    Text="TR TERMINATED" CssClass="label label-danger" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                                                                    Text="ADDED TO PR" CssClass="label label-info" />
                                                                                               <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "7" ? true : false %>'
                                                                                                    Text="ISSUED FROM STOCK" CssClass="label label-success" />
                                                                                                 <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "8" ? true : false %>'
                                                                                                    Text="RECEIVED" CssClass="label label-success" />
                                                                                                <%--  <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                                                    Text="TR CREATED" CssClass="label label-info"/>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                                                    Text="TR MODIFIED" CssClass="label label-warning" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                                    Text="TR APPROVED" CssClass="label label-success" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                                                                    Text="TR REJECTED" CssClass="label label-danger" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                                                                    Text="ADDED TO PR" CssClass="label label-success" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "7" ? true : false %>'
                                                                                                    Text="STOCK ISSUE"  CssClass="label label-success"/>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "7" ? true : false %>'
                                                                                                    Text="TERMINATED"  CssClass="label label-danger"/>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "8" ? true : false %>'
                                                                                                    Text="STOCK RECEIVED" CssClass="label label-warning" />--%>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </asp:Panel>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="TRId" HeaderText="TRId"
                                                                        HeaderStyle-CssClass="hidden"
                                                                        ItemStyle-CssClass="hidden" />
                                                                     <asp:BoundField DataField="TRDId" HeaderText="TRDId"
                                                                        HeaderStyle-CssClass="hidden"
                                                                        ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField DataField="CategoryId"
                                                                        HeaderText="Category Id"
                                                                        HeaderStyle-CssClass="hidden"
                                                                        ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField DataField="CategoryName"
                                                                        HeaderText="Category Name" />
                                                                    <asp:BoundField DataField="SubCategoryId"
                                                                        HeaderText="SubCategory Id"
                                                                        HeaderStyle-CssClass="hidden"
                                                                        ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField DataField="SubCategoryName"
                                                                        HeaderText="Sub-Category Name" />
                                                                    <asp:BoundField DataField="ItemId"
                                                                        HeaderText="Item Id"
                                                                        HeaderStyle-CssClass="hidden"
                                                                        ItemStyle-CssClass="hidden" />
                                                                    <asp:BoundField DataField="ItemName"
                                                                        HeaderText="Item Name" />
                                                                    <asp:BoundField DataField="ShortName"
                                                                        HeaderText="Unit" />
                                                                    <asp:BoundField DataField="RequestedQTY"
                                                                        HeaderText="Request Quantity" />
                                                                    <asp:BoundField DataField="ReceivedQty"
                                                                        HeaderText="Received Quantity" />
                                                                    <asp:BoundField DataField="IssuedQty"
                                                                        HeaderText="Issued Quantity" />
                                                                    <asp:TemplateField HeaderText="TR Status">
                                                                                        <ItemTemplate>
                                                                                               <%-- <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                                                                    Text="TR CREATED" CssClass="label label-info"/>
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                                                    Text="TR APPROVED" CssClass="label label-success" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                                                    Text="TR REJECTED" CssClass="label label-danger" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                                    Text="TR MODIFIED" CssClass="label label-warning" />
                                                                                                <asp:Label
                                                                                                    runat="server"
                                                                                                    Visible='<%# Eval("Status").ToString() == "4" ? true : false %>'
                                                                                                    Text="TR TERMINATED" CssClass="label label-danger" />--%>
                                                                                             <asp:Label
                                                                                                runat="server"
                                                                                                Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                                                                Text="Pending" CssClass="label label-warning"/>
                                                                                            <asp:Label
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
                                                                                                Text="Stock Delivered" CssClass="label label-success"/>
                                                                                            <asp:Label
                                                                                                runat="server"
                                                                                                Visible='<%# Eval("Status").ToString() == "5" ? true : false %>'
                                                                                                Text="stock Received" CssClass="label label-success"/>
                                                                                            <asp:Label
                                                                                                runat="server"
                                                                                                Visible='<%# Eval("Status").ToString() == "6" ? true : false %>'
                                                                                                Text="Terminated" CssClass="label label-danger"/>
                                                                                           

                                                                                                
                                                                                            </ItemTemplate>
                                                                                    </asp:TemplateField>
                                        
                                                                </Columns>
                                                            </asp:GridView>

                                        </div>
                                        <!-- End : tr Table -->
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <section class="content-header">
    <h1>
      View Transfer Request Notes
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">View Transfer Request Notes </li>
      </ol>
    </section>
                <br />
                <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
           <div class="box-header with-border">
          <h3 class="box-title" >View Transfer Request Notes</h3>
            
           <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt" style="margin-left:20px;"></asp:TextBox>
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" class="btn btn-info btn-sm btnRefreshCl"></asp:Button>
            <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="hidden" style="margin-right:10px; max-height:30px;" />
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvTR" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="TRId"  HeaderText="TR ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:TemplateField HeaderText="TR Code">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# "TR"+Eval("TrCode").ToString() %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="FromWarehouse"  HeaderText="From Warehouse" />
                        <asp:BoundField DataField="ToWarehouse"  HeaderText="To Warehouse" />
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:BoundField DataField="CreatedDatetime"  HeaderText="Created On"  DataFormatString="{0:dd-MM-yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date"  DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Description"  HeaderText="Description" />
                        <asp:TemplateField HeaderText="Approval Status">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                    Text="Pending" CssClass="label label-warning"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                    Text="Approved" CssClass="label label-success"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                    Text="Rejected" CssClass="label label-danger"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TR Status">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                    Text="Completion Pending" CssClass="label label-warning"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                    Text="Completed" CssClass="label label-success"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                    Text="Terminated" CssClass="label label-danger"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="View Details" OnClick="lbtnView_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnLog" Text="View Log" OnClick="btnLog_Click"></asp:LinkButton>
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

                $('.btnRefreshCl').on({
                    click: function () {
                        $('#loader').removeClass('hidden');
                    }
                })

            });
            
        });
    </script>


</asp:Content>
