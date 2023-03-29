<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyCreateSupplier.aspx.cs" Inherits="BiddingSystem.CompanyCreateSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }

        .multiselect-container {
            width: 100%;
        }
    </style>
    
    <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="LoginResources/css/bootstrap-multiselect.css" rel="stylesheet" />
    <link href="AdminResources/css/Wizard.css?version=<%DateTime.Now.ToString(); %>" rel="stylesheet" />

    <form id="form1" runat="server" enctype="multipart/form-data" defaultbutton="btnSave">
        <div class="content-header">
            <h1>Create Supplier<small></small></h1>
            <ol class="breadcrumb">
                <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">Create Supplier</li>
            </ol>
        </div>
        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updatepanel1" runat="server">
            <ContentTemplate>
                <section class="content">
                    <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title" >Supplier Details</h3>

                        <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <div class="row">
                        <div class="col-md-6">
                                <div class="form-group">
                                    <label for="exampleInputEmail1">Name</label><span class="required"> *</span>
                                    <asp:Label ID="lblSupplierName" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtSupplierName" ValidationGroup="btnSave" >Fill this Field</asp:RequiredFieldValidator>
                                    <asp:TextBox ID="txtSupplierName" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                            </div>
                                <div class="form-group">
                                <label for="exampleInputEmail1">Address1</label><span class="required"> *</span>
                               <asp:Label ID="lblAddress1" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" ControlToValidate="txtAddress1" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Address2</label>   
                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                            </div>
                                <div class="form-group">
                                <label for="exampleInputEmail1">Email Address</label><span class="required"> *</span>
                                    <asp:Label ID="Label6" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red"  ValidationExpression="^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$" ControlToValidate="txtEmailAddress" ErrorMessage="Invalid Email Format" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red"  ControlToValidate="txtEmailAddress" ValidationGroup="btnSave" Display="Dynamic">Fill this Field</asp:RequiredFieldValidator>     
                                <asp:TextBox ID="txtEmailAddress" runat="server" type="email" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Office Contact No</label><span class="required"> *</span>
                                <asp:Label ID="Label5" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOfficeContactNo" ValidationGroup="btnSave"
                                ErrorMessage="Maximum 10 Digits are allowed" ForeColor="Red" ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" ControlToValidate="txtOfficeContactNo" ValidationGroup="btnSave" ErrorMessage="Fill this Field" Display="Dynamic"></asp:RequiredFieldValidator>                         
                                <asp:TextBox ID="txtOfficeContactNo" type="number" onkeypress="return isNumberKey(event)" runat="server" CssClass="form-control" placeholder="" Text="" min="0"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Mobile No</label><span class="required"> *</span>
                                <asp:Label ID="Label4" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobileNo" ValidationGroup="btnSave"
                                ErrorMessage="Maximum 10 Digits are allowed" ForeColor="Red" ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="txtMobileNo" ValidationGroup="btnSave">Fill this Field</asp:RequiredFieldValidator>     
                                <asp:TextBox ID="txtMobileNo"  type="number" runat="server" onkeypress="return isNumberKey(event)" CssClass="form-control" placeholder="" Text="" min="0"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Category</label><span class="required"> *</span>
                                <asp:Label ID="Label3" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11"  ErrorMessage="Select atleast one category" ForeColor="Red" ControlToValidate="lstCompanyCategory" ValidationGroup="btnSave"></asp:RequiredFieldValidator>     
                                <asp:ListBox ID="lstCompanyCategory" runat="server" CssClass="form-control"  SelectionMode="Multiple">
                                </asp:ListBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Business Registration No.</label>
                                <asp:TextBox ID="txtBusinesRegNo" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                <div class="col-md-4">
                                <label for="exampleInputEmail1">Is Registered</label>
                                <asp:CheckBox ID="chkRegSup" runat="server" CssClass="form-control" ></asp:CheckBox>
                                    </div>
                                    <div class="col-md-8">
                                        <label for="exampleInputEmail1">Supplier Registration No</label>
                                        <asp:TextBox ID="txtSupRegNo" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                           
                                         </div>
                                     </div>
                                 </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                <label for="exampleInputEmail1">Vat Registration No.</label>    
                                <asp:TextBox ID="txtVatRegNo" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="exampleInputEmail1">Company Type</label><span class="required"> *</span>
                                <asp:Label ID="Label2" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator21" ForeColor="Red" ErrorMessage="Fill this Field" InitialValue="0"  ControlToValidate="ddlCompanyType" ValidationGroup="btnSave"></asp:RequiredFieldValidator>     
                                <asp:DropDownList ID="ddlCompanyType" runat="server" CssClass="form-control" CausesValidation="true" >
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
                               <asp:Label ID="Label1" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" ControlToValidate="ddlBusinessCategory"  ErrorMessage="Fill this Field" ValidationGroup="btnSave"></asp:RequiredFieldValidator>     
                                <asp:DropDownList ID="ddlBusinessCategory" runat="server" CssClass="form-control" CausesValidation="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                    <label for="">Supplier Logo (Jpg, Jpeg, Png, Gif)</label>
                                <asp:RegularExpressionValidator ID="regexValidator" runat="server"  ControlToValidate="fileUploadLogo"  ErrorMessage="( Jpg, Jpeg, Png, Gif Only)" ForeColor="Red"  ValidationExpression="([a-zA-Z0-9\s_\\.\-\)\(x:])+(.png|.PNG|.jpeg|.JPEG|.jpg|.JPG|.gif|.GIF)$" ValidationGroup="btnSave">  </asp:RegularExpressionValidator>
                                    <div class="input-group">
                                            <asp:FileUpload runat="server" style="display:inline;" CssClass="form-control" ID="fileUploadLogo" onchange="readURL(this);" ></asp:FileUpload>
                                            <span class="input-group-btn">
                                                <button class="btn btn-info btn-flat" id="clear" onclick="return clearLogo();" >Clear</button>
                                            </span>
                                            </div>
                                    <div class="col-sm-12">
                                    <div class="col-sm-6">
                                            <div class="panel" style=" background-color:transparent;">
                                            <div class="panel-body">
                                                <asp:Label ID="lblFileUploadError" runat="server"></asp:Label>
                                                <img alt="" src="" runat="server" id="imageid" style="width: 110px;height: 109px;"   />                                    
                                            </div>
                                        </div>
                                    </div>
                                    </div>
                                </div>
                            <div class="form-group">
                                <label for="fileUploadDocs">Upload Files(Multiple Files can be Uploaded)</label>
                                <div class="input-group">
                                        <asp:FileUpload style="display:inline;" runat="server" ID="fileUploadDocs" CssClass="form-control" onchange="readFilesURL(this);" allowMultiple="true" />
                                        <span class="input-group-btn">
                                            <button class="btn btn-info btn-flat" id="clear1" onclick="return clearDocs();" >Clear</button>
                                        </span>
                                </div>
                                <div class="row col-sm-12" style="overflow-y:auto; overflow-x:hidden; max-height:300px;display:none">
                                        <table id="tblUpload" class="table table-hover" >
                                        </table>
                                </div>   
                            </div>

                            <div class="form-group">
                                <label for="exampleInputEmail1">Supplier Type</label><span class="required"> *</span>
                                <asp:Label ID="Label7" runat="server" Text="" CssClass="lblValidate"></asp:Label>
                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" ErrorMessage="Fill this Field" InitialValue="0"  ControlToValidate="ddlSupplierType" ValidationGroup="btnSave"></asp:RequiredFieldValidator>     
                                <asp:DropDownList ID="ddlSupplierType" runat="server" CssClass="form-control" CausesValidation="true" >
                                    <%--<asp:ListItem Value="0">--Select Supplier Type--</asp:ListItem>
                                    <asp:ListItem Value="1">Supplier</asp:ListItem>
                                    <asp:ListItem Value="2">Local Supplier</asp:ListItem>
                                    <asp:ListItem Value="3">Clearence Agent</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label for="exampleInputEmail1">Assign Supplier if he is agent For</label>
                                    <asp:ListBox ID="ddlAgentSupplier"  runat="server" SelectionMode="Multiple" CssClass="select2 form-control" style="color:black; width:100%">
                                </asp:ListBox>
                            </div>
                        </div>
                    </div>
                        <div class="box-footer">
                        <span class="pull-right">
                                <asp:Button ID="btnSave" runat="server" Text="Submit"  ValidationGroup="btnSave" CssClass="btn btn-primary " OnClick="BtnSave_Click" OnClientClick="validate();"></asp:Button>
                                <asp:Button ID="btnClear"  runat="server" Text="Clear"  CssClass="btn btn-danger" OnClick="btnClear_Click"></asp:Button>               
                        </span>
                        </div>
                    </div>
                    </div>
                </section>

                <asp:Button ID="btnSubmitNew" runat="server" OnClick="BtnSave_Click" CssClass="hidden" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSave" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script src="AdminResources/js/select2.full.min.js" type="text/javascript"></script>
    <script src="LoginResources/js/bootstrap-multiselect.js" type="text/javascript"></script>

    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $(function () {
                $('[id*=ContentSection_lstCompanyCategory]').multiselect({
                    includeSelectAllOption: true
                });
                $('.select2').select2();
            });
        });


        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        
        function clearLogo() {
            document.getElementById("ContentSection_fileUploadLogo").value = '';
            document.getElementById("ContentSection_imageid").src = "LoginResources/images/noPerson.png?636765963681404626";
            return false;
        }

        function clearDocs() {
            document.getElementById("ContentSection_fileUploadDocs").value = '';
            document.getElementById('tblUpload').innerHTML = "";
            return false;
        }

        function readURL(input) {
            if (input.files && input.files[0]) {
                var filePath = input.value;
                var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

                if (!allowedExtensions.exec(filePath)) {

                    $("<%=fileUploadLogo.ClientID%>").remove();
                        $("#<%=fileUploadLogo.ClientID %>").css('border-color', 'red');

                        document.getElementById('<%= imageid.ClientID %>').src = 'LoginResources/images/noPerson.png';
                    }
                    else {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById('<%= imageid.ClientID %>').src = e.target.result;
                            $("#<%=fileUploadLogo.ClientID %>").css('border-color', '#d2d6de');
                        }
                        reader.readAsDataURL(input.files[0]);
                    }
                }
            }

            $("#<%=btnSave.ClientID %>").click(function () {

            var fileInput = document.getElementById("ContentSection_fileUploadLogo");

            if (fileInput.files.length != 0) {
                var filePath = fileInput.value;
                var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;

                if (!allowedExtensions.exec(filePath)) {
                    $("#<%=fileUploadLogo.ClientID %>").css('border-color', 'red');
                    return false;
                }

            }
            else {
                $("#<%=fileUploadLogo.ClientID %>").css('border-color', '#d2d6de');
                return true;
            }
        });

        function validate() {
            var SupplierName = document.getElementById('<%=txtSupplierName.ClientID%>').value;
            var Address1 = document.getElementById('<%=txtAddress1.ClientID%>').value;
            var EmailAddress = document.getElementById('<%=txtEmailAddress.ClientID%>').value;
            var OfficeContactNo = document.getElementById('<%=txtOfficeContactNo.ClientID%>').value;
            var MobileNo = document.getElementById('<%=txtMobileNo.ClientID%>').value;
            
            var CompanyType = $("[id*=ddlCompanyType]");
            var selectedCompanyType = CompanyType.val();

            var BusinessCategory = $("[id*=ddlBusinessCategory]");
            var selectedBusinessCategory = BusinessCategory.val();

            var CompanyCategory = $("[id*=lstCompanyCategory]");
            var selectedCompanyCategory = CompanyCategory.val();

            var SupplierType = $("[id*=ddlSupplierType]");
            var selectedSupplierType= SupplierType.val();
            

            if (SupplierName != "" && Address1 != "" && EmailAddress != "" && OfficeContactNo != "" && MobileNo != "" && selectedBusinessCategory != "" && selectedCompanyType != 0 && selectedCompanyCategory != "" && selectedSupplierType != "0") {
                document.getElementById('<%= btnSave.ClientID %>').disabled = true;
                $('#ContentSection_btnSubmitNew').click();
            }
           
        }

        function readFilesURL(input) {
            var output = document.getElementById('tblUpload');
            output.innerHTML = '<tr>';
            output.innerHTML += '<th style="background-color:#e8e8e8;"><b>(' + input.files.length + ') Files has been Selected </b></th>';
            for (var i = 0; i < input.files.length; ++i) {
                output.innerHTML += '<td >' + input.files.item(i).name + '</td>';

            }
            output.innerHTML += '</tr>';
        }

       
    </script>
</asp:Content>
