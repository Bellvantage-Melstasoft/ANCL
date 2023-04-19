<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyViewApprovedGRN.aspx.cs" Inherits="BiddingSystem.CompanyViewApprovedGRN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <html>
    <head>
        <style>
            .activePhase {
                text-align: center;
                border-radius: 3px;
            }

            .ui-datepicker-calendar {
                display: none;
            }
        </style>
        <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
        <%--  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>--%>

        <script src="AdminResources/googleapis/googleapis-jquery.min.js"></script>
        <link rel="stylesheet" href="AdminResources/googleapis/googleapis-jquery-ui.css">
        <script src="AdminResources/googleapis/googleapis-jquery-ui.js"></script>

    </head>

    <section class="content-header">
        <h1>View GRNs
        <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i>Home</a></li>
            <li class="active">View GRNs </li>
        </ol>
    </section>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content">
                    <div class="box box-info" id="panelPurchaseRequset" runat="server">
                        <div class="box-header with-border">
                            <h3>View GRN</h3>
                            <asp:TextBox ID="txtFDt" runat="server" CssClass="txtFDt" Style="margin-left: 20px;"></asp:TextBox>
                            <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click" class="btn btn-info btn-sm btnRefreshCl"></asp:Button>
                            <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="hidden" style="margin-right: 10px; max-height: 30px;" />

                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvPurchaseOrder" EmptyDataText="No records Found" GridLines="None" CssClass="table table-responsive"
                                            AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="GrnId" HeaderText="GRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="GrnCode" HeaderText="GRN Code" />
                                                <asp:BoundField DataField="PoID" HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <%--<asp:BoundField DataField="POCode"  HeaderText="PO Code"/>--%>
                                                <asp:TemplateField HeaderText="PO Code">
                                                    <ItemTemplate>
                                                        <a href='<%# "ViewPO.aspx?PoId=" + Eval("PoID") %>'><%# Eval("POCode") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="PrCode"  HeaderText="Based PR Code"/>--%>
                                                <asp:TemplateField HeaderText="PR Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# "PR-"+Eval("PrCode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="QuotationFor" HeaderText="Quotation For" />
                                                <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" />
                                                <asp:BoundField DataField="GoodReceivedDate" HeaderText="Good Received Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                                <asp:BoundField DataField="IsGrnRaised" HeaderText="Is PO Raise" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="IsGrnApproved" HeaderText="Is PO Approved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                                <asp:TemplateField HeaderText="Approval Status">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                            Text="Pending" CssClass="label label-warning" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                            Text="APPROVED" CssClass="label label-success" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                            Text="Rejected" CssClass="label label-danger" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="WarehouseName" HeaderText="Warehouse" />
                                                <%--  <asp:BoundField DataField="GrnStatusCount"  HeaderText="Grn Status" />--%>
                                                <%-- <asp:TemplateField HeaderText="GRN Status">
                          <ItemTemplate>
                  <asp:Label ID="txtApproved" Text='<%#Eval("GrnStatusCount").ToString()=="1"?"GRN Approved":"Pending Approval" %>' ForeColor='<%#Eval("GrnStatusCount").ToString()=="1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                          </ItemTemplate>
                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnView" Text="View" OnClick="btnView_Click1"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    </html>


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
