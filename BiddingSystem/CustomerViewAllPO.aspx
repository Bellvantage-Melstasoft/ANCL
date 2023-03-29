<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerViewAllPO.aspx.cs" Inherits="BiddingSystem.CustomerViewAllPO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
	
    <style type="text/css">
        .ui-datepicker-calendar {
            display: none;
        }
        .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }
    </style>
     <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
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
                <section class="content-header">
    <h1>
      View All Purchase Orders
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">View All Purchase Orders </li>
      </ol>
    </section>
                <br />
                <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
       <%-- <div class="box-header with-border">
          <h3 class="box-title" >View Purchase Orders</h3>
            
           <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt" style="margin-left:20px;"></asp:TextBox>
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" class="btn btn-info btn-sm btnRefreshCl"></asp:Button>
            <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="hidden" style="margin-right:10px; max-height:30px;" />
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>--%>

           <div class="box-header with-border">
                             <%--<div class="row">
                                <div class="col-md-4">
                            <h3 class="box-title">Purchase Requests</h3>
                           <%-- <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt"></asp:TextBox>--%>
                            <%-- <asp:Button ID="btnrefresh" runat="server" Text="Refresh" OnClick="btnrefresh_Click" OnClientClick="onButtonClick()" class="btn btn-info"></asp:Button>
                            </div>
                            <div class="col-md-8">
                            <img id="imgMinMaxLoader" class="hide" src="SupplierPortalAssets/assets/img/loader-info.gif" style="height: 40px;" />
                             </div>
                            </div>--%>

                            <div class="row">
                                <div class="col-md-12">
                                   <h3 class="box-title" style="margin-bottom:15px;">Search Purchase Requests</h3>
                         </div>
                            </div>

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkMonth" Text="&nbsp;Month" runat="server" style="cursor:pointer"  CssClass="chkCl" /><br />
                                  <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkPrCode" Text="&nbsp;PR Code" runat="server" style="cursor:pointer"  CssClass="chkCl" /><br />
                                  <asp:TextBox ID="txtPrCode" runat="server" CssClass="form-control" autocomplete="off" PlaceHolder="LCL1 / IMP1"></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkPoCode" Text="&nbsp;PO Code" runat="server" style="cursor:pointer"  CssClass="chkCl" /><br />
                                  <asp:TextBox ID="txtPoCode" runat="server" CssClass="form-control" autocomplete="off" PlaceHolder="PO1"  ></asp:TextBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkCategory" Text="&nbsp;Category" runat="server" style="cursor:pointer"  CssClass="chkCl" /><br />
                                    <asp:ListBox ID="ddlCategory" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black; width:100%"> </asp:ListBox>
                                </div>
                                <%--<div class="col-md-2">
                                    <asp:CheckBox ID="chkWarehouse" Text="&nbsp;Warehouse" runat="server" style="cursor:pointer"  CssClass="chkCl" /><br />
                                    <asp:ListBox ID="ddlWarehouse" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black; width:100%"> </asp:ListBox>
                                </div>--%>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkSupplier" Text="&nbsp;Supplier" runat="server" style="cursor:pointer"  CssClass="chkCl" /><br />
                                    <asp:ListBox ID="ddlSupplier" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black; width:100%"> </asp:ListBox>
                                </div>
                               <%-- <div class="col-md-2">
                                    <asp:CheckBox ID="chkPoType" Text="&nbsp;Po Type" runat="server" style="cursor:pointer"  CssClass="chkCl" /><br />
                                    <asp:DropDownList ID="ddlPoType" runat="server" CssClass="form-control"  > 
                                         
                                        <asp:ListItem  Value="1">General PO </asp:ListItem>  
                                        <asp:ListItem  Value="2">Covering PO</asp:ListItem>   
                                    </asp:DropDownList>  
                                </div>--%>
                            </div>
                             <div class="row">
                                <div class="col-md-12">                                    
                                   <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="onButtonClick()" class="btn btn-info pull-right" style="margin-top:15px;" ></asp:Button>
                                   <img id="imgMinMaxLoader" class="hide  pull-right" src="UserRersourses/assets/img/loader-info.gif" style="margin-left:920px; margin-top:15px; height: 40px;" />
                                 </div>
                         </div>
                            </div>


        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvPurchaseOrder" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="POCode"  HeaderText="PO Code" />
                        <asp:TemplateField HeaderText="Based PR">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    Text='<%#"PR-" + Eval("PrCode").ToString()%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:BoundField DataField="Description"  HeaderText="Quotation For" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="SupplierName"  HeaderText="Supplier Name" />
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="Approval Status">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                    Text="Pending" CssClass="label label-warning"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                    Text="APPROVED" CssClass="label label-success"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                    Text="Rejected" CssClass="label label-danger"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>--%>
                        <asp:TemplateField HeaderText="Approval Date">
                                                                    <ItemTemplate>
                                                                        <%# (DateTime)Eval("ApprovedDate") == DateTime.MinValue ? "Not Approved" : Eval("ApprovedDate") %>
                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                        <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Approved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="Cancel Status">
                                                                    <ItemTemplate>
                                                                       <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsCancelled").ToString() == "0" ? true : false %>'
                                    Text="Not Cancelled" CssClass="label label-success"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsCancelled").ToString() == "1" ? true : false %>'
                                    Text="Cancelled" CssClass="label label-danger"/>

                                                                    </ItemTemplate>
                                                                     </asp:TemplateField>
                        <asp:TemplateField HeaderText="PO Type">
                            <ItemTemplate>
                                 <asp:Label
                                  runat="server"
                                  Visible='<%# Eval("PurchaseProcedure").ToString() == "1" ? true : false %>'
                                  Text="General" CssClass="label label-warning"/>
                               <asp:Label
                                 runat="server"
                                  Visible='<%# Eval("PurchaseProcedure").ToString() == "3" ? true : false %>'
                                   Text="Covering" CssClass="label label-success"/>
                                <asp:Label
                                                                    runat="server"
                                                                    Visible='<%# Eval("PurchaseProcedure").ToString() == "2" ? true : false %>'
                                                                    Text="Cover(GRN)" CssClass="label label-info"/>
                                <%--<asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsDerived").ToString() == "0" ? true : false %>'
                                    Text="General PO" CssClass="label label-success"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "1" ? true : false %>'
                                    Text="Modified PO" CssClass="label label-warning"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "2" ? true : false %>'
                                    Text="Covering PO" CssClass="label label-info"/>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ParentPOCode"  HeaderText="Parent PO" NullDisplayText="None"/>
                        <asp:TemplateField HeaderText="Contains Derived POs">
                            <ItemTemplate>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("WasDerived").ToString() == "0" ? true : false %>'
                                    Text="No" CssClass="label label-danger"/>
                                <asp:Label
                                    runat="server"
                                    Visible='<%# Eval("WasDerived").ToString() == "1" ? true : false %>'
                                    Text="Yes" CssClass="label label-info"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="WarehouseName"  HeaderText="Warehouse" NullDisplayText="Not Found" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
                        <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                              <asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClick="btnView_Click"></asp:LinkButton>
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
    <script src="AdminResources/js/select2.full.min.js"></script>
    <script type="text/javascript">

        Sys.Application.add_load(function () {
            $(function () {
                $('.select2').select2();


            })

        });

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
                    },
                    beforeShow: function (input, inst) {
                        var date = new Date($(this).val());
                        debugger;
                        $(this).datepicker('option', 'defaultDate', date);
                        $(this).datepicker('setDate', date);
                    }
                });

                $('.btnRefreshCl').on({
                    click: function () {
                        $('#loader').removeClass('hidden');
                    }
                })

            });
            
        });

        function onButtonClick() {
            document.getElementById('imgMinMaxLoader').className = "show";
        }

    </script>
</asp:Content>
