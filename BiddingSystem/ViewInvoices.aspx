<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewInvoices.aspx.cs" Inherits="BiddingSystem.ViewInvoices" %>

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
        /*.ChildGrid td {
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

        .ChildGridTwo td {
            background-color: #dcd4d4 !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
        }

        .ChildGridTwo th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #56585b !important;
            color: white;
        }

        #ContentSection_gvPurchaseRequest tbody tr td{
            vertical-align:middle;
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
                white-space: normal !important;
            }

        table#ContentSection_gvPr tbody tr:nth-child(1) th {
            position: sticky;
            top: 1px;
            background: #3C8DBC;
            color: white;
        }

        .marginBtm {
            margin-bottom: 5px;
        }
    </style>
    <%--<script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" type="text/css" />--%>
    <script src="AdminResources/js/jquery1.8.min.js"></script>

    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/themes/smoothness/jquery-ui.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.js"></script>--%>
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />

    <script src="AdminResources/googleapis/googleapis-jquery.min.js"></script>
    <link rel="stylesheet" href="AdminResources/googleapis/googleapis-jquery-ui.css">
    <script src="AdminResources/googleapis/googleapis-jquery-ui.js"></script>

    <section class="content-header">
        <h1>View Invoices
        <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
            <li class="active">View Invoices </li>
        </ol>
    </section>
    <br />
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>

                <div id="mdlImages" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Quotation Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvImages" runat="server" CssClass="table table-responsive TestTable"
                                                    EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="InvoiceId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:BoundField DataField="ImagePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("ImagePath") %>'
                                                                    Height="80px" Width="100px" />
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

                <div id="mdlInvUpdate" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Update Invoice Details</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">

                                                <div class="box box-info">
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblInvNo" runat="server" Text="Invoice Number" Style="color: black"></asp:Label>
                                                                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtInvNo" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator2" ForeColor="Red">*</asp:RequiredFieldValidator>--%>

                                                                    <asp:TextBox ID="txtInvNo" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblDate" runat="server" Text="Invoice Date" Style="color: black"></asp:Label>
                                                                    <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="txtDate" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator7" ForeColor="Red">*</asp:RequiredFieldValidator>--%>

                                                                    <asp:TextBox ID="txtDate" type="date" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblAmount" runat="server" Text="Invoice Amount" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtremark" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator3" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtAmount" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblnewDate" runat="server" Text="Date" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewDate" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator6" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtNewDate" type="date" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-6">
                                                                <div class="form-group">
                                                                    <asp:Label ID="lblVatNo" runat="server" Text="VAT Number" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtVatNo" InitialValue="" ValidationGroup="btnSave" ID="RequiredFieldValidator4" ForeColor="Red">* Please Fil this Field</asp:RequiredFieldValidator>
                                                                    <asp:TextBox ID="txtVatNo" runat="server" Style="color: black" CssClass="form-control"></asp:TextBox>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblPaymentType" runat="server" Text="Payment Type" Style="color: black"></asp:Label>
                                                                    <asp:DropDownList runat="server" ID="ddlPaymentMethod" CssClass="form-control">
                                                                        <%--<asp:ListItem Value="">Select Payment method</asp:ListItem>--%>
                                                                        <asp:ListItem Value="1">Cash</asp:ListItem>
                                                                        <asp:ListItem Value="2">Cheque</asp:ListItem>
                                                                        <asp:ListItem Value="3">Credit</asp:ListItem>
                                                                        <asp:ListItem Value="4">Advanced Payment</asp:ListItem>
                                                                        <asp:ListItem Value="5">None</asp:ListItem>

                                                                    </asp:DropDownList>
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblPayment" runat="server" Text="Is Payment Settled" Style="color: black"></asp:Label>
                                                                    <asp:CheckBox ID="ChkPayment" runat="server" CssClass="form-control" />
                                                                </div>

                                                                <div class="form-group">
                                                                    <asp:Label ID="lblRemark" runat="server" Text="Remark" Style="color: black"></asp:Label>
                                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtremark" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator5" ForeColor="Red">*</asp:RequiredFieldValidator>

                                                                    <asp:TextBox ID="txtremark" runat="server" TextMode="MultiLine" Rows="3" Style="color: black" CssClass="form-control"></asp:TextBox>

                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-primary pull-right margin" Text="Update" OnClick="btnUpdate_Click" ValidationGroup="btnUpdate" />
                                                                </div>
                                                            </div>
                                                        </div>


                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



                <section class="content" style="padding-top: 0px">

                    <div class="panel panel-default" id="panelPRBasicSearch" runat="server">
                        <div class="panel-heading">
                            <h3 class="panel-title">Basic Search
                            <a class="arrowdown" data-target="#basicSearch" data-toggle="collapse" aria-expanded="false">
                                <span class="expand_caret caret"></span>
                            </a>
                            </h3>
                        </div>
                        <div class="panel-body collapse" id="basicSearch">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:RadioButton ID="rdbPO" runat="server" Checked="true" GroupName="basicSearch"></asp:RadioButton>
                                    <b>Search by PO Code</b><label class="lblerror hidden" style="color: red;">*Fill this field</label>
                                    <br>
                                    <asp:TextBox ID="txtPO" runat="server" CssClass="txtFDt form-control" PlaceHolder="Ex: PO01"></asp:TextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:RadioButton ID="rdbGRN" runat="server" GroupName="basicSearch"></asp:RadioButton>
                                    <b>Search by GRN Code</b><label class="lblerror hidden" style="color: red;">*Fill this field</label>
                                    <asp:TextBox ID="txtGRN" runat="server" CssClass="form-control" PlaceHolder="Ex: GRN01"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-11">
                                    <asp:Image runat="server" ID="loadingImage1" class="loadingImage pull-right hidden" src="AdminResources/images/Spinner-0.6s-200px.gif" Style="margin-top: 5px; max-height: 40px;" />
                                </div>
                                <div class="col-md-1">
                                    <asp:Button ID="btnBasicSearch" ValidationGroup="btnBasicSearch" OnClientClick="return BSFieldValidate()" OnClick="btnBasicSearch_Click" runat="server" Text="Search" Style="margin-top: 10px;" CssClass="btn btn-info pull-right"></asp:Button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- SELECT2 EXAMPLE -->
                    <div class="box box-info" id="panelInvoices" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Invoices</h3>

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
                                        <asp:GridView runat="server" ID="gvInvoices" GridLines="None"
                                            CssClass="table table-responsive" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                                            AutoGenerateColumns="false" EmptyDataText="No Invoices Found" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:BoundField DataField="InvoiceId" HeaderText="InvoiceId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                <asp:BoundField DataField="PoId" HeaderText="PoId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="GrnId" HeaderText="GrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                <%--<asp:TemplateField HeaderText="PO Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Eval("POCode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="PO Code">
                                                    <ItemTemplate>
                                                        <a href='<%# "ViewPO.aspx?PoId=" + Eval("PoID") %>'><%# Eval("POCode") %></a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GRN Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%#Eval("GRNCode") == null? "-": Eval("GRNCode").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="InvoiceNo" HeaderText="Invoice No" />
                                                <%--<asp:BoundField DataField="InvoiceDate"  HeaderText="Invoice Date" Dataformatstring="{0: dd MMMM yyyy}"/>--%>
                                                <asp:TemplateField HeaderText="Invoice Date">
                                                    <ItemTemplate>
                                                        <%--<asp:Label runat="server" ID="lblExpDate" CssClass="lblExpDate" Text='<%# Eval("ExpiryDate", "{0:dd/MM/yyyy}").ToString() %>'></asp:Label>--%>
                                                        <asp:Label runat="server" Text='<%# (DateTime)Eval("InvoiceDate") == DateTime.MinValue ? "Not Found" : Eval("InvoiceDate", "{0:dd MMM yyyy}") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="InvoiceAmount" HeaderText="Invoice Amount" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="VatNo" HeaderText="VAT No" />

                                                <asp:TemplateField HeaderText="Payment Type">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            Text='<%# Eval("PaymentType").ToString() == "1" ? "Cash" : Eval("PaymentType").ToString() == "2" ? "Cheque" :Eval("PaymentType").ToString() == "3" ? "Credit" :Eval("PaymentType").ToString() == "4" ? "Advanced Payment" :Eval("PaymentType").ToString() == "5" ? "None" : "Not Found" %>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Payment Settled">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            Text='<%# Eval("IsPaymentSettled").ToString() == "0" ? "No" : "Yes" %>' />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PO Cancel Status">
                                                    <ItemTemplate>
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("IsCancelled").ToString() == "0" ? true : false %>'
                                                            Text="No" CssClass="label label-success" />
                                                        <asp:Label
                                                            runat="server"
                                                            Visible='<%# Eval("IsCancelled").ToString() == "1" ? true : false %>'
                                                            Text="Yes" CssClass="label label-danger" />

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Remark" HeaderText="Remark" NullDisplayText="-" />
                                                <asp:TemplateField HeaderText="Remark On">
                                                    <ItemTemplate>
                                                        <%--<asp:Label runat="server" ID="lblExpDate" CssClass="lblExpDate" Text='<%# Eval("ExpiryDate", "{0:dd/MM/yyyy}").ToString() %>'></asp:Label>--%>
                                                        <asp:Label runat="server" Text='<%# (DateTime)Eval("RemarkOn") == DateTime.MinValue ? "Not Found" : Eval("RemarkOn", "{0:dd MMM yyyy}") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Images">
                                                    <ItemTemplate>
                                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnViewImages"
                                                            runat="server" OnClick="btnViewImages_Click"
                                                            Text="View" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" CssClass="btn btn-warning btn-xs btnCancel marginBtm" Text="Terminate" ID="btnDelete" Visible='<%# int.Parse(Eval("IsActive").ToString()) == 1 ? true:false %>' OnClientClick='<%#"DeleteInvoice(event,"+Eval("InvoiceId").ToString()+")" %>' />
                                                        <br>
                                                        <asp:Label runat="server" Visible='<%#int.Parse(Eval("IsActive").ToString()) == 1 ? false:true %>' Text="Terminated" CssClass="label label-danger" />
                                                        <asp:Button runat="server" CssClass="btn btn-primary btn-xs btnUpdate" Text="Update" ID="btnUpdate" Visible='<%# int.Parse(Eval("IsActive").ToString()) == 1 ? true:false %>' OnClick="btnView_Click" OnClientClick="RemoveBackdrop()" />

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
                <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="hidden" />
                <asp:HiddenField runat="server" ID="hdnInvId" />
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

            $('.select2').select2();

            $('.collapse').on('show.bs.collapse', function () {
                $('.collapse.in').each(function () {
                    $(this).collapse('hide');
                });
            });

            $("#basicSearch").collapse('show');
            var customDates = $(".customDate");
            for (x = 0; x < customDates.length; ++x) {
                if ($(customDates[x]).val() != "") {
                    customDates[x].className = (customDates[x].value != '' ? customDates[x].className + ' has-value' : customDates[x].className);
                    $(customDates[x]).attr('data-date', moment($(customDates[x]).val(), 'YYYY-MM-DD').format($(customDates[x]).attr('data-date-format')));
                }
            }


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


        function DeleteInvoice(e, Invid) {
            e.preventDefault();
            $('#ContentSection_hdnInvId').val(Invid);

            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want to delete this invoice?</br></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,

            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnDelete').click();
                }
            });
        }

        function RemoveBackdrop() {

            $('#myModalAddDesignation').hide();
            $('.modal-backdrop').remove();
            $('body').css("overflow", "auto");
            $('body').css("padding-right", "0");
        }



    </script>

</asp:Content>
