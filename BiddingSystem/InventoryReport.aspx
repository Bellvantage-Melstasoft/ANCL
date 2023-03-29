<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="InventoryReport.aspx.cs" Inherits="BiddingSystem.InventoryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
     #myModal .modal-dialog {
       width: 90%;
     }
     #myModalViewBom .modal-dialog {
       width: 50%;
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
    .tablegv tr:nth-child(even){background-color: #f2f2f2;}
    /*.tablegv tr:hover {background-color: #ddd;}*/
    .tablegv th {
        padding-top: 12px;
        padding-bottom: 12px;
        text-align: left;
        {
        background-color: #3C8DBC;
        color: white;
    }
    .successMessage
            color: #1B6B0D;
            font-size: medium;
        }
        
        .failMessage
        {
            color: #C81A34;
            font-size: medium;
        }
         
        .tableCol{
        width: 100%;
        margin-bottom: 20px;
		border-collapse: collapse;
		background: #fff;
    }
    .tableCol, .thCol, .tdCol{
        border: 1px solid #cdcdcd;
        color:Black;
    }
    .tableCol .thCol, .tableCol .tdCol{
        padding: 10px;
        text-align: left;
        color:Black;
    }
    
     .TestTable {
         font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
         border-collapse: collapse;
         width: 100%;
     }
     
     .TestTable td, #TestTable th {
         border: 1px solid #ddd;
         padding: 8px;
     }
     
     .TestTable tr:nth-child(even){background-color: #f2f2f2;}
     
     .TestTable tr:hover {background-color: #ddd;}
     
     .TestTable th {
         padding-top: 12px;
         padding-bottom: 12px;
         text-align: left;
         background-color: #4CAF50;
         color: white;
     }
     
     #myModalViewBom
     {
         display:none;
     }
      
</style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <section class="content-header">
    <h1>
    Inventory Report
    </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Inventory Report</li>
      </ol>
    </section>
    <form id="Form1" runat="server">
    <div id="errorAlert" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #ff0000;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title" style="color: white; font-weight: bold;">
                        ERROR</h4>
                </div>
                <div class="modal-body">
                    <p id="errorMessage" style="font-weight: bold; font-size: medium;">
                    </p>
                </div>
                <div class="modal-footer">
                    <span class="btn btn-danger" data-dismiss="modal" aria-label="Close">OK</span>
                </div>
            </div>
        </div>
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <div class="box box-info" id="panelPurchaseRequset" runat="server">
                <div class="box-header with-border">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="col-sm-6">
                                <label>
                                    Search By</label>
                                <div cssclass="form-control">
                                    <asp:DropDownList ID="ddlSearchBy" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSearchBy_SelectedIndexChanged"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="">-Please Select-</asp:ListItem>
                                        <asp:ListItem Value="0">Category Wise</asp:ListItem>
                                        <asp:ListItem Value="2">Re Order Level</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12">
                            <div id="dvCatagory" class="col-sm-6" runat="server" visible="false">
                                <label>
                                    Catagory</label>
                                <div class="input-group margin">
                                    <asp:DropDownList ID="ddlCatagory" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlCatagory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span class="input-group-btn">
                                        <asp:Button runat="server" ID="btnSearchCatagory" OnClick="btnSearchCatagory_Click"
                                            ValidationGroup="ddlCatagory" CssClass="btn btn-info" Text="Search" />
                                    </span>
                                </div>
                            </div>
                            <div id="dvSubCatagory" class="col-sm-6" runat="server" visible="false">
                                <label>
                                    Sub Catagory</label>
                                <div class="input-group margin">
                                    <asp:DropDownList ID="ddlSubCatagory" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSubCatagory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <span class="input-group-btn">
                                        <asp:Button runat="server" ID="btnSearchSubCatagory" OnClick="btnSearchSubCatagory_Click"
                                            ValidationGroup="ddlSubCatagory" CssClass="btn btn-info" Text="Search" />
                                    </span>
                                </div>
                            </div>
                            <div id="dvLevel" class="col-sm-6" runat="server" visible="false">
                                <label>
                                    Re Order Level</label>
                                <div class="input-group margin">
                                    <asp:TextBox ID="txtLevel" runat="server" CssClass="form-control"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:Button runat="server" ID="btnSearchLevel" OnClick="btnSearchLevel_Click" ValidationGroup="txtLevel"
                                            CssClass="btn btn-info" Text="Search" />
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView runat="server" ID="gvReport" EmptyDataText="No records Found" GridLines="None"
                                    CssClass="table table-responsive" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="UpdatedBy" HeaderText="Item Name" />
                                        <asp:BoundField DataField="IsActive" HeaderText="Qty" />
                                        <asp:BoundField DataField="SubCategoryId" HeaderText="Re Order Level" />
                                        <asp:BoundField DataField="CreatedBy" HeaderText="Warehouse " />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtnViewDetails" Text="View Details" OnClick="lbtnViewDetails_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtnPR" Text="Create PR" OnClick="lbtnPR_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="PR Quantity" HeaderStyle-CssClass="alignCenter">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantity" type="number" Width="100%" EnableViewState="true" CssClass="clsTextToCalculate form-control input-sm"
                                                    runat="server"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId" ItemStyle-CssClass="hidden"
                                            HeaderStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="CompanyId" HeaderText="CompanyId" ItemStyle-CssClass="hidden"
                                            HeaderStyle-CssClass="hidden" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer">
                        <div class="form-group">
                            <asp:Button ID="btnCreatePR" runat="server" Text="Proceed to Create PR" CssClass="btn btn-primary pull-right"
                                OnClick="btnCreatePR_Click" Visible="false"></asp:Button>
                        </div>
                    </div>
                    <div class="col-xs-12">
                        <asp:Label ID="lbMessage" CssClass="alert-danger" Text="" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div id="myModalViewBom" class="modal modal-primary fade in" tabindex="-1" role="dialog"
                aria-hidden="false">
                <div class="modal-dialog">
                    <!-- Modal content-->
                    <div class="modal-content" style="background-color: #a2bdcc;">
                        <div class="modal-header" style="background-color: #7bd47dfa;">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="False">×</span></button>
                            <h4 class="modal-title">
                                View Specification</h4>
                        </div>
                        <div class="modal-body">
                            <div class="login-3lsw">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvTempBoms" runat="server" CssClass="table table-responsive TestTable"
                                                EmptyDataText="No Specifications Found" Style="border-collapse: collapse; color: black;"
                                                GridLines="None" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="itemId" HeaderText="Item Id" ItemStyle-CssClass="hidden"
                                                        HeaderStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SeqNo" HeaderText="Seq_ID" />
                                                    <asp:BoundField DataField="Meterial" HeaderText="Material" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div>
                                        <label id="lbMailMessage1" style="margin: 3px; color: maroon; text-align: center;">
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
    <script type="text/javascript">

        function InitClient() {
            $("input[type=checkbox]").addClass("checkBoxChecked");

            $(".checkBoxChecked").change(function () {

                if ($(this).prop("checked") == false) {


                    $(this).parent().parent().find(".clsTextToCalculate").val("");

                }

                if ($(this).prop("checked") == true) {
                    $(this).parent().parent().find(".clsTextToCalculate").focus();
                    var qty = 0;
                    var reOrder = 0;
                    var orderFill = 0;
                    $(this).parent().parent().children().eq(3).each(function () {
                        reOrder = parseFloat($(this).html());
                    });
                    $(this).parent().parent().children().eq(2).each(function () {
                        qty = parseFloat($(this).html());
                    });
                    orderFill = reOrder - qty;

                    if (orderFill < 0) {
                        $(this).parent().parent().find(".clsTextToCalculate").val("");
                    } else {

                        $(this).parent().parent().find(".clsTextToCalculate").val(orderFill);
                    }

                }
            });
        }
    </script>
</asp:Content>
