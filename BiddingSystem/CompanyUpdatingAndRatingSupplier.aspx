<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyUpdatingAndRatingSupplier.aspx.cs" Inherits="BiddingSystem.CompanyUpdatingAndRatingSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

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

            .tablegv th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }

        .Empty {
            font-size: 40px;
            cursor: pointer;
            background-color: transparent;
            border: none;
        }

            .Empty:hover {
                font-size: 40px;
                cursor: pointer;
                background-color: transparent;
                border: none;
            }

        .Filled {
            color: gold;
            font-size: 40px;
            cursor: pointer;
            background-color: transparent;
            border: none;
        }

        .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }

        #ContentSection_gvSupplierList tbody tr td {
            white-space: nowrap;
            border: 1px solid #f8f8f8;
            vertical-align: middle;
        }

        .multiselect-container {
            width: 100%;
        }

        .grid_scroll {
            overflow: auto;
            width: 100px;
        }
    </style>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="LoginResources/css/bootstrap-multiselect.css" rel="stylesheet" />
    <link href="AdminResources/css/Wizard.css?version=<%DateTime.Now.ToString(); %>" rel="stylesheet" />

    <div class="content-header">
        <h1>Update and Rating Supplier  <small></small></h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
            <li class="active">Update and Rating Supplier</li>
        </ol>
    </div>
    <br />

    <form id="form1" runat="server">

        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                <section class="content" style="padding-top: 0px">


                    <div id="mdlItemCategories" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index: 3001" aria-hidden="true">
                        <div class="modal-dialog" style="width: 900px;">
                            <div class="modal-content" style="background-color: #a2bdcc;">
                                <div class="modal-header" style="background-color: #7bd47dfa;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                        <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                    <h4 class="modal-title">Item Categories</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="login-w3ls">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvItemCategories" runat="server" CssClass="table table-responsive TestTable"
                                                        Style="border-collapse: collapse; color: black;" GridLines="None"
                                                        AutoGenerateColumns="false" EmptyDataText="No Item Categories found">
                                                        <Columns>
                                                            <asp:BoundField DataField="mainItemCategory" HeaderText="Item category" />

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





                    <div class="box box-info">
                        <div class="box-header">
                            <h3 class="box-title">Supplier Details</h3>
                            <div class="box-tools">
                                <div class="input-group input-group-sm" style="width: 162px;">
                                    <asp:TextBox type="text" name="table_search" class="form-control pull-right" placeholder="Search by name" ID="txtSearch" runat="server" />
                                    <div class="input-group-btn">
                                        <asp:Button type="submit" Text="Search" class="btn btn-default fa fa-search" runat="server" autopostback="true" OnClick="SearchAll_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12" style="color: black; overflow-x: scroll;">
                                    <asp:GridView runat="server" ID="gvSupplierList" EmptyDataText="No Active Suppliers Found!"
                                        CssClass="table table-responsive tablegv" AutoGenerateColumns="false"
                                        GridLines="None" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                        <Columns>
                                            <asp:BoundField DataField="SupplierId" HeaderText="Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden SupplierId" />
                                            <asp:BoundField DataField="supplierName" HeaderText="Name" />
                                            <asp:BoundField DataField="Address1" HeaderText="Address" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="PhoneNo" HeaderText="Contact No" />
                                            <asp:TemplateField HeaderText="Main Item Catergory">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnItemCategory" OnClick="lbtnItem_Click" CssClass="btn btn-xs btn-info">View</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier/Agent">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("isAgentSupplier").ToString() == "1" ? true : false %>'
                                                        Text="Only A Supplier" CssClass="label label-info" />
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("isAgentSupplier").ToString() == "0" ? true : false %>'
                                                        Text="Supplier & Agent" CssClass="label label-warning " />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Is Supplier Registered">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSupplierregisteredStatus" CssClass="label label-info" runat="server" Text='<%# Eval("IsRegisteredSupplier").ToString() =="1" ? "Registered":"Not Registred" %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:BoundField DataField="SupplierRegistration" HeaderText="Reg No" />

                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <%-- <asp:LinkButton runat="server" ID="lbtnEdit" CssClass="btn btn-xs btn-warning"
                                                            OnClick="lbtnEdit_Click"
                                                            OnClientClick="return scrollFunction()">Edit--%>
                                                    <asp:LinkButton runat="server" ID="lbtnEdit" OnClick="lbtnEdit_Click" CssClass="btn btn-xs btn-warning" OnClientClick="return scrollFunction()">Edit</asp:LinkButton>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnDelete" CssClass="btn btn-xs btn-danger"
                                                        OnClientClick="deleteSupplier(event,this);">Delete
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>

                </section>

                <section class="content" id="sectUpdateSupplier" runat="server">
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Supplier Details</h3>
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                    <i
                                        class="fa fa-minus"></i>
                                </button>
                                <button type="button" class="btn btn-box-tool" data-widget="remove">
                                    <i
                                        class="fa fa-remove"></i>
                                </button>
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Supplier ID</label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5"
                                            ForeColor="Red" Font-Bold="true" ControlToValidate="txtSupplierId"
                                            ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSupplierId" runat="server" Enabled="false"
                                            CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Name</label><span class="required"> *</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1"
                                            ForeColor="Red" Font-Bold="true" ControlToValidate="txtSupplierName"
                                            ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtSupplierName" runat="server"
                                            CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Email Address</label><span class="required"> *</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3"
                                            ForeColor="Red" Font-Bold="true" ControlToValidate="txtEmailAddress"
                                            ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="form-control"
                                            Text=""></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Address1</label><span class="required"> *</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6"
                                            ForeColor="Red" Font-Bold="true" ControlToValidate="txtAddress1"
                                            ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtAddress1" runat="server"
                                            CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Address2</label>
                                        <asp:TextBox ID="txtAddress2" runat="server"
                                            CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Office Contact No</label><span class="required"> *</span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOfficeContactNo" ValidationGroup="btnSave"
                                            ErrorMessage="Maximum 10 Digits are allowed" ForeColor="Red" ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" ControlToValidate="txtOfficeContactNo" ValidationGroup="btnSave" ErrorMessage="Fill this Field" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtOfficeContactNo" runat="server"
                                            CssClass="form-control" onkeypress="return isNumberKey(event)" placeholder="" type="number"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Mobile No</label><span class="required"> *</span>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobileNo" ValidationGroup="btnSave"
                                            ErrorMessage="Maximum 10 Digits are allowed" ForeColor="Red" ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="txtMobileNo" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>
                                        <asp:TextBox ID="txtMobileNo" runat="server"
                                            CssClass="form-control" placeholder="" type="number"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Categories</label><span class="required"> *</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ErrorMessage="Select atleast one category" ForeColor="Red" ControlToValidate="lstCompanyCategory" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                                        <asp:ListBox ID="lstCompanyCategory" runat="server" CssClass="form-control"
                                            SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Business Registration No.</label>
                                        <asp:TextBox ID="txtBusinesRegNo" runat="server"
                                            CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Assign Supplier if he is agent For</label>
                                        <asp:ListBox ID="ddlAgentSupplier" runat="server" SelectionMode="Multiple" CssClass="select2 form-control" Style="color: black; width: 100%"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Vat Registration No.</label>
                                        <asp:TextBox ID="txtVatRegNo" runat="server"
                                            CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Company Type</label><span class="required"> *</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator21" ForeColor="Red" ErrorMessage="Fill this Field" InitialValue="0" ControlToValidate="ddlCompanyType" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlCompanyType" runat="server"
                                            CssClass="form-control">
                                            <%--<asp:ListItem Value="0">Select Company Type</asp:ListItem>
                        <asp:ListItem Value="1">Sole Proprietorship</asp:ListItem>
                        <asp:ListItem Value="2">Private Company</asp:ListItem>
                        <asp:ListItem Value="3">Public Company</asp:ListItem>
                        <asp:ListItem Value="4">Electrics</asp:ListItem>
                        <asp:ListItem Value="5">Corporation</asp:ListItem>
                        <asp:ListItem Value="6">Limited</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Nature Of Business</label><span class="required"> *</span>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="ddlBusinessCategory" ErrorMessage="Fill this Field" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlBusinessCategory" runat="server"
                                            CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="">Supplier Logo (Jpg, Jpeg, Png, Gif)</label>
                                        <asp:RegularExpressionValidator ID="regexValidator" runat="server" ControlToValidate="fileUploadLogo" ErrorMessage="( Jpg, Jpeg, Png, Gif Only)" ForeColor="Red" ValidationExpression="([a-zA-Z0-9\s_\\.\-\)\(x:])+(.png|.PNG|.jpeg|.JPEG|.jpg|.JPG|.gif|.GIF)$" ValidationGroup="btnSave">  </asp:RegularExpressionValidator>
                                        <div class="input-group">
                                            <asp:FileUpload runat="server" Style="display: inline;" CssClass="form-control" ID="fileUploadLogo" onchange="readURL(this);"></asp:FileUpload>
                                            <span class="input-group-btn">
                                                <button class="btn btn-info btn-flat" id="clear" onclick="return clearLogo();">Clear</button>
                                            </span>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-6">
                                                <div class="panel" style="background-color: transparent;">
                                                    <div class="panel-body">
                                                        <asp:Label ID="lblFileUploadError" runat="server"></asp:Label>
                                                        <img alt="" src="" runat="server" id="imageid" style="width: 110px; height: 109px;" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="fileUploadDocs">Uploaded Files(Multiple Files can Upload)</label>
                                        <div class="input-group">
                                            <asp:FileUpload Style="display: inline;" runat="server" ID="fileUploadDocs" CssClass="form-control" AllowMultiple="true" />
                                            <span class="input-group-btn">
                                                <button class="btn btn-info btn-flat" id="clear1" onclick="return clearDocs();">Clear</button>
                                            </span>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Supplier Documents</label>
                                        <div>
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <asp:GridView runat="server" ID="gvUserDocuments"
                                                        AutoGenerateColumns="false" GridLines="None"
                                                        CssClass="table table-responsive " HeaderStyle-CssClass="hidden"
                                                        EmptyDataText="No Documents Found">
                                                        <Columns>
                                                            <asp:BoundField DataField="ImageId" HeaderText="ImageId"
                                                                ItemStyle-CssClass="hidden ImageId" HeaderStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="SupplierImagePath" HeaderText="Path"
                                                                ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="ImageFileName" />
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lbtnview"
                                                                        href='<%#Eval("SupplierImagePath").ToString().Replace("~/","") %>' target="_blank">View</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lbtnFileDelete" OnClientClick="deleteUploadedFile(event,this);" CssClass="btn btn-xs btn-danger">Delete</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="exampleInputEmail1">Supplier Type</label><span class="required"> *</span>
                                        <asp:Label ID="Label7" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" ErrorMessage="Fill this Field" InitialValue="0" ControlToValidate="ddlSupplierType" ValidationGroup="btnSave"></asp:RequiredFieldValidator>
                                        <asp:DropDownList ID="ddlSupplierType" runat="server" CssClass="form-control" CausesValidation="true">
                                            <%--<asp:ListItem Value="0">--Select Supplier Type--</asp:ListItem>
                                    <asp:ListItem Value="1">Supplier</asp:ListItem>
                                    <asp:ListItem Value="2">Local Supplier</asp:ListItem>
                                    <asp:ListItem Value="3">Clearence Agent</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                    <div class="form-group">
                                        <div class="row">
                                            <div class="col-md-4">
                                                <label for="exampleInputEmail1">Is Registered</label>
                                                <asp:CheckBox ID="chkRegSup" runat="server" CssClass="form-control"></asp:CheckBox>
                                            </div>
                                            <div class="col-md-8">
                                                <label for="exampleInputEmail1">Supplier Registration No</label>
                                                <asp:TextBox ID="txtSupRegNo" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divEditSupplier">
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Supplier Rating</h3>

                                <div class="box-tools pull-right">
                                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                                        <i
                                            class="fa fa-minus"></i>
                                    </button>
                                    <button type="button" class="btn btn-box-tool" data-widget="remove">
                                        <i
                                            class="fa fa-remove"></i>
                                    </button>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <span style="padding: 3px;">
                                                <label for="exampleInputEmail1">Rating :</label>
                                                <asp:Label ID="lblRate" runat="server"></asp:Label>
                                            </span>
                                            <div style="">
                                                <button id="Rating1" onmouseover="return Decide(1);"
                                                    onclick="return Decide(1);" ondblclick="return Decide(0);"
                                                    class="Empty" style="outline: none;">
                                                    ☆</button>
                                                <button id="Rating2" onmouseover="return Decide(2);"
                                                    onclick="return Decide(2);" class="Empty"
                                                    style="outline: none;">
                                                    ☆</button>
                                                <button id="Rating3" onmouseover="return Decide(3);"
                                                    onclick="return Decide(3);" class="Empty"
                                                    style="outline: none;">
                                                    ☆</button>
                                                <button id="Rating4" onmouseover="return Decide(4);"
                                                    onclick="return Decide(4);" class="Empty"
                                                    style="outline: none;">
                                                    ☆</button>
                                                <button id="Rating5" onmouseover="return Decide(5);"
                                                    onclick="return Decide(5);" class="Empty"
                                                    style="outline: none;">
                                                    ☆</button>
                                            </div>

                                        </div>
                                    </div>


                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Remarks</label>
                                            <asp:TextBox ID="txtRemarks" TextMode="MultiLine" runat="server"
                                                CssClass="form-control" placeholder="Remark"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Black List</label>
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <asp:RadioButton ID="rdoBlockYes" runat="server"
                                                        GroupName="radioBlackList"></asp:RadioButton>
                                                </span>
                                                <asp:TextBox ID="TextBox4" runat="server" class="form-control"
                                                    Text="Yes"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1" style="visibility: hidden">
                                                SMS
                                    Enable/Disable</label>
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <asp:RadioButton ID="rdoBlockNo" runat="server"
                                                        GroupName="radioBlackList"></asp:RadioButton>
                                                </span>
                                                <asp:TextBox ID="TextBox10" runat="server" class="form-control"
                                                    Text="No"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1">Active</label>
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <asp:RadioButton ID="rdoActive" runat="server"
                                                        GroupName="radioActive"></asp:RadioButton>
                                                </span>
                                                <asp:TextBox ID="txtEnable" runat="server" class="form-control"
                                                    Text="Yes"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="exampleInputEmail1" style="visibility: hidden">Is Follow</label>
                                            <div class="input-group">
                                                <span class="input-group-addon">
                                                    <asp:RadioButton ID="rdoInActive" runat="server"
                                                        GroupName="radioActive"></asp:RadioButton>
                                                </span>
                                                <asp:TextBox ID="txtDisable" runat="server" class="form-control"
                                                    Text="No"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <label for="exampleInputEmail1">Upload Any Complain Document</label>
                                        <div class="input-group">
                                            <asp:FileUpload Style="display: inline;" runat="server" ID="ComplianFileUpload" CssClass="form-control" AllowMultiple="true" />
                                            <span class="input-group-btn">
                                                <button class="btn btn-info btn-flat" id="clear2" onclick="return clearComplainDocs();">Clear</button>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="form-group col-sm-12">
                                        <label for="exampleInputEmail1">Complain Document</label>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <asp:GridView runat="server" ID="gvComplainDocument"
                                            AutoGenerateColumns="false" GridLines="None"
                                            CssClass="table table-responsive " HeaderStyle-CssClass="hidden"
                                            EmptyDataText="No Documents Found">
                                            <Columns>
                                                <asp:BoundField DataField="ImageId" HeaderText="ImageId"
                                                    ItemStyle-CssClass="hidden ImageId" HeaderStyle-CssClass="hidden ImageId" />
                                                <asp:BoundField DataField="SupplierImagePath" HeaderText="Path"
                                                    ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ImageFileName" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnview"
                                                            href='<%#Eval("SupplierImagePath").ToString().Replace("~/","") %>' target="_blank">View</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnComplainDocument"
                                                            OnClick="lbtnComplainDocument_Click" Visible="false">Download
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnComplainFileDelete" OnClientClick="deleteUploadedComplainFile(event,this);" CssClass="btn btn-xs btn-danger">Delete</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer">
                                <span class="pull-right">
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="updateSupplier(event,this);" ValidationGroup="btnSave"
                                        CssClass="btn btn-primary" Enabled="false"></asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel"
                                        OnClick="btnCancel_Click" CssClass="btn btn-danger"></asp:Button>
                                </span>
                            </div>
                        </div>
                    </div>
                </section>
                <asp:HiddenField runat="server" ID="hdnSupplierRate" />
                <asp:HiddenField runat="server" ID="hdnSuplierLogoEdit" />
                <asp:Button ID="btnDeleteSupplier" runat="server" OnClick="btnDeleteSupplier_Click" CssClass="hidden" />
                <asp:HiddenField runat="server" ID="hndUploadedFileId" />
                <asp:Button ID="btnDeleteUploadedFile" runat="server" OnClick="lbtnDeleteUploadedFile_Click" CssClass="hidden" />
                <asp:HiddenField runat="server" ID="hndUploadedComplainFileId" />
                <asp:Button ID="btnDeleteUploadedComplainFile" runat="server" OnClick="lbtnDeleteUploadedComplainFile_Click" CssClass="hidden" />
                <asp:HiddenField runat="server" ID="hndSupplierId" />
                <asp:Button ID="btnUpdate2" runat="server" OnClick="btnUpdate_Click" CssClass="hidden" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpdate2" />
            </Triggers>
        </asp:UpdatePanel>
    </form>



    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script src="AdminResources/js/select2.full.min.js" type="text/javascript"></script>
    <script src="LoginResources/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script type="text/javascript">

        function scrollFunction() {
            document.getElementById("ContentSection_sectUpdateSupplier").scrollIntoView();
            return true;
        }

        Sys.Application.add_load(function () {
            $(function () {
                $('[id*=ContentSection_lstCompanyCategory]').multiselect({
                    includeSelectAllOption: true
                });
                $('.select2').select2();
            });
        });

        function deleteSupplier(e, obj) {
            e.preventDefault();
            $('#ContentSection_hndSupplierId').val($(obj).closest("tr").find("td.SupplierId").text());
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want delete this Supplier?</br></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                }
            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnDeleteSupplier').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                }
            });
        }

        function updateSupplier(e, obj) {
            e.preventDefault();
            swal.fire({
                title: 'Confirmation!',
                html: "Click Yes to update supplier?</br></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                }
            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnUpdate2').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                }
            });
        }

        function deleteUploadedFile(e, obj) {
            e.preventDefault();
            $('#ContentSection_hndUploadedFileId').val($(obj).closest("tr").find("td.ImageId").text());
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want delete this file permanently?</br></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                }
            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnDeleteUploadedFile').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                }
            });
        }

        function deleteUploadedComplainFile(e, obj) {
            e.preventDefault();
            $('#ContentSection_hndUploadedComplainFileId').val($(obj).closest("tr").find("td.ImageId").text());
            swal.fire({
                title: 'Are you sure?',
                html: "Are you sure you want delete this file permanently?</br></br>",
                type: 'warning',
                cancelButtonColor: '#d33',
                showCancelButton: true,
                showConfirmButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                allowOutsideClick: false,
                preConfirm: function () {
                }
            }
            ).then((result) => {
                if (result.value) {
                    $('#ContentSection_btnDeleteUploadedComplainFile').click();
                } else if (result.dismiss === Swal.DismissReason.cancel) {
                }
            });
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById('<%= imageid.ClientID %>').src = e.target.result;
                }
                reader.readAsDataURL(input.files[0]);
            }
        }


        function Decide(option) {
            var temp = "";
            var ratePoint = "0";
            document.getElementById('<%= lblRate.ClientID %>').innerText = '';
                if (option == 0) {
                    document.getElementById('Rating1').className = "Empty";
                    document.getElementById('Rating2').className = "Empty";
                    document.getElementById('Rating3').className = "Empty";
                    document.getElementById('Rating4').className = "Empty";
                    document.getElementById('Rating5').className = "Empty";
                    temp = "0 Rate";
                    ratePoint = "0";
                }
                if (option == 1) {
                    document.getElementById('Rating1').className = "Filled";
                    document.getElementById('Rating2').className = "Empty";
                    document.getElementById('Rating3').className = "Empty";
                    document.getElementById('Rating4').className = "Empty";
                    document.getElementById('Rating5').className = "Empty";
                    temp = "Poor";
                    ratePoint = "1";
                }
                if (option == 2) {
                    document.getElementById('Rating1').className = "Filled";
                    document.getElementById('Rating2').className = "Filled";
                    document.getElementById('Rating3').className = "Empty";
                    document.getElementById('Rating4').className = "Empty";
                    document.getElementById('Rating5').className = "Empty";
                    temp = "Ok";
                    ratePoint = "2";

                }
                if (option == 3) {
                    document.getElementById('Rating1').className = "Filled";
                    document.getElementById('Rating2').className = "Filled";
                    document.getElementById('Rating3').className = "Filled";
                    document.getElementById('Rating4').className = "Empty";
                    document.getElementById('Rating5').className = "Empty";
                    temp = "Fair";
                    ratePoint = "3";
                }
                if (option == 4) {
                    document.getElementById('Rating1').className = "Filled";
                    document.getElementById('Rating2').className = "Filled";
                    document.getElementById('Rating3').className = "Filled";
                    document.getElementById('Rating4').className = "Filled";
                    document.getElementById('Rating5').className = "Empty";
                    temp = "Good";
                    ratePoint = "4";
                }
                if (option == 5) {
                    document.getElementById('Rating1').className = "Filled";
                    document.getElementById('Rating2').className = "Filled";
                    document.getElementById('Rating3').className = "Filled";
                    document.getElementById('Rating4').className = "Filled";
                    document.getElementById('Rating5').className = "Filled";
                    temp = "Best";
                    ratePoint = "5";
                }
                document.getElementById('<%= lblRate.ClientID %>').innerText = temp;
                document.getElementById('<%= hdnSupplierRate.ClientID %>').value = ratePoint;
            return false;
        }

        function showpass(check_box) {
            var spass = document.getElementById("ContentSection_txtPassword");
            if (check_box.checked)
                spass.setAttribute("type", "text");
            else
                spass.setAttribute("type", "password");
        }
        function showpassC(check_box) {
            var spass = document.getElementById("ContentSection_txtConfirmPassword");
            if (check_box.checked)
                spass.setAttribute("type", "text");
            else
                spass.setAttribute("type", "password");
        }

        function clearLogo() {
            document.getElementById("ContentSection_fileUploadLogo").value = '';
            var s = $("#<%=hdnSuplierLogoEdit.ClientID%>").val();
                document.getElementById("ContentSection_imageid").src = $("#<%=hdnSuplierLogoEdit.ClientID%>").val();
            return false;
        }

        function clearDocs() {
            document.getElementById("ContentSection_fileUploadDocs").value = '';
            return false;
        }

        function clearComplainDocs() {
            document.getElementById("ContentSection_gvComplainDocument").value = '';
            return false;
        }

    </script>
</asp:Content>
