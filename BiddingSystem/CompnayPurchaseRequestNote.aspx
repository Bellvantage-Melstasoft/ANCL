<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompnayPurchaseRequestNote.aspx.cs" Inherits="BiddingSystem.CompnayPurchaseRequestNote" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        #myModal .modal-dialog {
            width: 90%;
        }

        #myModalViewBom .modal-dialog {
            width: 50%;
        }

        #specification .modal-dialog {
            width: 50%;
        }

        #myModal2 .modal-dialog {
            width: 50%;
        }

        #myModalUploadedPhotos .modal-dialog {
            width: 50%;
        }

        #myModalReplacementPhotos .modal-dialog {
            width: 50%;
        }
    </style>
    <style type="text/css">
        .form {
            margin: 20px 0;
        }

        form input, button {
            padding: 6px;
            font-size: 18px;
        }

        .tableCol {
            width: 100%;
            margin-bottom: 20px;
            border-collapse: collapse;
            background: #fff;
        }

        .tableCol, .thCol, .tdCol {
            border: 1px solid #cdcdcd;
            color: Black;
        }

            .tableCol .thCol, .tableCol .tdCol {
                padding: 10px;
                text-align: left;
                color: Black;
            }

        body {
            background: #ccc;
        }

        .add-row, .delete-row {
            font-size: 16px;
            font-weight: 600;
            padding: -1px 16px;
        }
    </style>
    <style type="text/css">
        .TestTable {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            .TestTable td, #TestTable th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            .TestTable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            .TestTable tr:hover {
                background-color: #ddd;
            }

            .TestTable th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
    </style>
    <style type="text/css">
        #myModal .modal-dialog {
            width: 60%;
        }

        #myModal2 .modal-dialog {
            width: 70%;
        }

        #myModalViewBom .modal-dialog {
            width: 50%;
        }

        #specification .modal-dialog {
            width: 50%;
        }

        #myModalUploadedPhotos .modal-dialog {
            width: 50%;
        }

        #myModalReplacementPhotos .modal-dialog {
            width: 50%;
        }
    </style>
    <style type="text/css">
        input[type="file"] {
            display: block;
        }

        .imageThumb {
            max-height: 75px;
            border: 2px solid;
            padding: 1px;
            cursor: pointer;
        }

        .pip {
            display: inline-block;
            margin: 10px 10px 0 0;
        }

        .remove {
            display: block;
            background: #fff;
            border: 1px solid #fff;
            color: red;
            text-align: center;
            cursor: pointer;
        }

            .remove:hover {
                background: white;
                color: black;
            }
    </style>
    <style type="text/css">
        #myModal .modal-dialog {
            width: 90%;
        }

        #myModal2 .modal-dialog {
            width: 50%;
        }

        #myModalViewBom .modal-dialog {
            width: 50%;
        }

        #specification .modal-dialog {
            width: 50%;
        }

        #myModalUploadedPhotos .modal-dialog {
            width: 50%;
        }

        #myModalReplacementPhotos .modal-dialog {
            width: 50%;
        }
    </style>
    <style type="text/css">
        #myTable {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #myTable td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #myTable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #myTable tr:hover {
                background-color: #ddd;
            }

            #myTable th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
    </style>
    <style type="text/css">
        #TestTable {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #TestTable td, #TestTable th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #TestTable tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #TestTable tr:hover {
                background-color: #ddd;
            }

            #TestTable th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
    </style>
    <style type="text/css">
        input[type="file"] {
            display: block;
        }

        .imageThumb {
            max-height: 75px;
            border: 2px solid;
            margin: 10px 10px 0 0;
            padding: 1px;
        }
    </style>

    <link href="AdminResources/css/Wizard.css" rel="stylesheet" />

    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="https://cdn.ckeditor.com/4.11.4/basic/ckeditor.js"></script>
    <!-- bootstrap datepicker -->
    <section class="content-header">
      <h1>
       Purchase Request 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Purchase Request</li>
      </ol>
    </section>
    <br />
    <script type="text/javascript">

        var files;
        var a = new Array();
        var b = new Array();
        var c = new Array();

        var fileReplace;
        var x = new Array();
        var y = new Array();
        var z = new Array();
        $(document).ready(function () {
            document.getElementById("fileReplace").disabled = true;
        });
        function InitClient() {

            //var dtp01 = new DateTimePicker('#ContentSection_DateTimeRequested', { pickerClass: 'datetimepicker-blue', timePicker: true, timePickerFormat: 12, format: 'Y/m/d h:i' });
            if ($('#ContentSection_rdoEnable').is(':checked')) {
                document.getElementById("fileReplace").disabled = false;
            }
            else {
                document.getElementById("fileReplace").disabled = true;
            }


            if (window.File && window.FileList && window.FileReader) {
                $('#ContentSection_rdoEnable').change(function () {
                    document.getElementById("fileReplace").disabled = false;
                });
                $('#ContentSection_rdoDisable').change(function () {
                    document.getElementById("fileReplace").disabled = true;
                    $("#fileReplace").val('');
                });

                if (files == null) {
                    $("#files").trigger("change");
                }
                else {
                    for (var i = 0; i < a.length; i++) {
                        var f = a[i];
                        var fileReader = new FileReader();
                        var file = c[i];
                        $("<img></img>", {
                            class: "imageThumb",
                            src: b[i],
                            title: file.name,
                            name: 'test'
                        }).insertAfter("#files");
                        fileReader.readAsDataURL(f);
                    }
                }
            } else {
                alert("Your browser doesn't support to File API")
            }

            $("#files").on("change", function (e) {
                var $fileUpload = $("input#files[type='file']");

                if (parseInt($fileUpload.get(0).files.length) > 5) {
                    e.preventDefault();
                    e.stopPropagation();
                    document.getElementById('files').value = "";
                    document.getElementById('filesPip').innerHTML = "";
                    document.getElementById('errorMessage').innerHTML = "You can only upload a maximum of 5 files";
                    $('#errorAlert').modal('show');
                    return false;
                }
                files = e.target.files,
                    filesLength = files.length;
                for (var i = 0; i < filesLength; i++) {
                    var f = files[i]
                    a.push(f);
                    var fileReader = new FileReader();
                    fileReader.onload = (function (e) {
                        var file = e.target;
                        c.push(e.target);
                        b.push(e.target.result);
                        $('#filesPip').append("<span class=\"pip\" style='text-align: center;'>" +
                            "<img class=\"imageThumb\" src=\"" + e.target.result + "\" title=\"" + f.name + "\"/>" +
                            "<br/><span id=\"remove\" class=\"imageThumb\"  file ='" + f.name + "' style='border:none;'><img src='images/delete.png' style='width:16px;' /></span>" +
                            "</span>");


                        $("#remove").click(function (e) {

                            var cats = [];
                            e.preventDefault();
                            $(this).parent().remove('');
                            var removeArray = new Array();
                            removeArray = files;
                            cats.push(files);
                            var file = $(this).attr('file');
                            for (var i = 0; i < files.length; i++) {
                                if (files[i].name == file) {
                                    cats.splice(cats.indexOf(files), 1);
                                    //files.splice(files.index(files), 1);
                                    //files.splice(i, 1);
                                    // files.splice(i, 1);
                                    break;
                                }
                            }


                            var i = Array.indexOf($(this).index);
                            if (i != -1) {
                                Array.splice(i, 1);
                            }

                            $(this).parent(".pip").remove();

                        });

                    });
                    fileReader.readAsDataURL(f);
                }
            });

            InitClientReplace();
            InitClientSupportive();
            LoadData();
        }

        function InitClientReplace() {

            if (window.File && window.FileList && window.FileReader) {
                $('#ContentSection_rdoEnable').change(function () {
                    document.getElementById("fileReplace").disabled = false;
                });
                $('#ContentSection_rdoDisable').change(function () {
                    document.getElementById("fileReplace").disabled = true;
                    fileReplace = [];
                    fileReader.readAsDataURL("");
                });
                if (fileReplace == null) {
                    $("#fileReplace").trigger("change");
                }
                else {
                    for (var k = 0; k < x.length; k++) {
                        var f = x[k];
                        var fileReader = new FileReader();
                        var file = z[k];
                        $("<img></img>", {
                            class: "imageThumb",
                            src: y[k],
                            title: file.name,
                            name: 'test'
                        }).insertAfter("#fileReplace");
                        fileReader.readAsDataURL(f);
                    }
                }
            } else {
                alert("Your browser doesn't support to File API")
            }

            $("#fileReplace").on("change", function (u) {


                var $replacedFile = $("input#fileReplace[type='file']");
                if (parseInt($replacedFile.get(0).files.length) > 5) {
                    document.getElementById('fileReplace').value = "";
                    document.getElementById('fileReplacePip').innerHTML = "";
                    document.getElementById('errorMessage').innerHTML = "You can only upload a maximum of 5 files";
                    $('#errorAlert').modal('show');
                    return false;
                }

                fileReplace = u.target.files,
                    filesLength = fileReplace.length;
                for (var k = 0; k < filesLength; k++) {
                    var f = fileReplace[k]
                    x.push(f);
                    var fileReader = new FileReader();
                    fileReader.onload = (function (u) {
                        var file = u.target;
                        z.push(u.target);
                        y.push(u.target.result);
                        $('#fileReplacePip').append("<span class=\"pip\" style='text-align: center;'>" +
                            "<img class=\"imageThumb\" src=\"" + u.target.result + "\" title=\"" + file.name + "\"/>" +
                            "<br/><span id=\"removeReplace\" class=\"imageThumb\"  file ='" + f.name + "' style='border:none;'><img src='images/delete.png' style='width:16px;' /></span>" +
                            "</span>");

                        $("#removeReplace").click(function (e) {

                            e.preventDefault();
                            $(this).parent().remove('');
                            var removeArray = new Array();
                            removeArray = files;
                            var file = $(this).attr('file');
                            for (var z = 0; z < files.length; z++) {
                                if (files[z].name == file) {
                                    files.splice(z, 1);
                                    // files.remove();
                                    break;
                                }
                            }

                            var z = Array.indexOf($(this).index);
                            if (z != -1) {
                                Array.splice(z, 1);
                            }

                            $(this).parent(".pip").remove();
                        });

                    });
                    fileReader.readAsDataURL(f);
                }
            });
        }

        function InitClientSupportive() {
            $("#supportivefiles").on("change", function (u) {

                debugger;
                var $supportivefiles = $("input#supportivefiles[type='file']");
                if (parseInt($supportivefiles.get(0).files.length) > 10) {
                    document.getElementById('supportivefiles').value = "";
                    document.getElementById('errorMessage').innerHTML = "You can only upload a maximum of 10 files";
                    document.getElementById('tblUpload').innerHTML = "";
                    $('#errorAlert').modal('show');
                    return false;
                }
            });
        }
    </script>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <body>
            <div class="modal modal-primary fade" id="myModal">
                <div class="modal-dialog">
                    <div class="modal-content" style="background-color: White;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="text-align: center;">Item Specifications</h4>
                            <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2"></div>
                                <div class="col-md-8">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="color: Black;">Material</label>
                                                <input type="text" id="meterial" placeholder="Material" class="form-control" autocomplete="off" />
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="color: Black;">Description</label>
                                                <input type="text" id="description" placeholder="Description" class="form-control" autocomplete="off" />
                                            </div>
                                        </div>
                                        <div class="col-md-4" style="margin-left: -120px; margin-top: 30px;">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="visibility: hidden;">Select Main Category</label>
                                                <asp:HyperLink ID="button" href="#" runat="server" Visible="true" CssClass="add-row" Style="color: Red;">
                        <img src="images/PlusSign.gif" border="0" style="margin-right:4px;vertical-align: middle;" alt="Add Row" />Add Row</asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="table-responsive">
                                        <div class="form">
                                        </div>
                                        <table class="tableCol" id="tableCols">
                                            <thead>
                                                <tr>
                                                    <th class="thCol">Action</th>
                                                    <th class="thCol">Material</th>
                                                    <th class="thCol">Description</th>
                                                </tr>
                                            </thead>
                                            <tbody class="tbodyCol" id="tbodyCol">
                                            </tbody>

                                        </table>
                                        <asp:HyperLink ID="HyperLink1" href="#" runat="server" Visible="true" CssClass="delete-row" Style="color: Red;">
                        <img src="images/dlt.png" border="0" style="margin-right:4px;vertical-align: middle;width: 20px;margin-top: -4px;" alt="Delete Row" />Delete Row</asp:HyperLink>
                                        <br />
                                        <div class="col-md-12">
                                            <div class="col-md-6">
                                                <!-- hyper lin button for add row in above table which call the javascript function create above-->
                                                <div style="margin-top: 5px;">
                                                </div>
                                            </div>
                                        </div>


                                    </div>
                                </div>
                                <div class="col-md-2"></div>
                            </div>

                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">Close</button>
                            <%--  <button type="button" class="btn btn-outline">Submit</button>--%>
                        </div>
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div id="modalConfirmYesNo" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button"
                                class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 id="lblTitleConfirmYesNo" class="modal-title">Confirmation</h4>
                        </div>
                        <div class="modal-body">
                            <p>Are you sure to submit your details ?</p>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnYesConfirmYesNo" runat="server" CssClass="btn btn-primary" OnClick="btnSavePR_Click" Text="Yes" OnClientClick="return scrollToTop();"></asp:Button>
                            <button id="btnNoConfirmYesNo" type="button" class="btn btn-danger">No</button>
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

            <asp:ScriptManager runat="server" ID="sm">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>



                    <section class="content">
                        <div class="panel wizard">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <h4>
                        	                <b>CREATE</b> PURCHASE REQUEST <br>
                        	            </h4>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <ul class="nav nav-pills">
                                            <li id="liBasic" class="active" style="width: 33%;" onclick=" $('#ContentSection_hdnActiveTab').val('#basic'); $('.btnPrev').addClass('hidden'); $('.btnNext').removeClass('hidden');  $('.btnCreate').addClass('hidden');"><a href="#basic" data-toggle="tab" aria-expanded="true">BASIC INFO</a></li>
                                            <li id="liAddItem" style="width: 33%;" onclick=" $('#ContentSection_hdnActiveTab').val('#addItem'); $('.btnPrev').removeClass('hidden'); $('.btnNext').removeClass('hidden');  $('.btnCreate').addClass('hidden');"><a href="#addItem" data-toggle="tab">ADD ITEMS</a></li>
                                            <li id="liFinalize" style="width: 33.3333%;" onclick=" $('#ContentSection_hdnActiveTab').val('#finalize'); $('.btnNext').addClass('hidden'); $('.btnPrev').removeClass('hidden'); $('.btnCreate').removeClass('hidden');"><a href="#finalize" data-toggle="tab">FINALIZE</a></li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="tab-content">
                                                    
                                                    <div class="tab-pane active" id="basic">
                                                        <div class="row">
                                                            <div class="col-xs-12">
                                                        
                                                                 <div class="panel panel-default" id="topPanel">
                                                                    <!-- /.box-header -->
                                                                    <div class="panel-body">
                                                                      <div class="row">
                                                                         <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label for="exampleInputEmail1">Department Name</label>
                                                                                <asp:TextBox ID="txtDepName" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                             <div class="form-group">
                                                                                <label for="exampleInputEmail1">Requisition No.</label>
                                                                                <asp:TextBox ID="txtPrNumber" runat="server"  Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label for="exampleInputEmail1">Date Of Request</label>
                                                                                <asp:TextBox ID="DateTimeRequested" runat="server"  CssClass="form-control" ></asp:TextBox>
                                                                            </div>
                                                                          <!-- /.form-group -->
                                                                            <div class="form-group">
                                                                                <label for="exampleInputEmail1">PR Type</label>
                                                                               <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlPrType" ValidationGroup="btnSavePR">*</asp:RequiredFieldValidator>     --%>
                                                                                <asp:DropDownList ID="ddlPrType" runat="server" CssClass="form-control" >
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="form-group">
                                                                                <label for="exampleInputEmail1">Category</label>
                                                                               <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlPrType" ValidationGroup="btnSavePR">*</asp:RequiredFieldValidator>     --%>
                                                                                <asp:DropDownList ID="ddlPRCategory" runat="server" CssClass="form-control ddlPRCategoryCl">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                             <div class="form-group">
                                                                                <label for="exampleInputEmail1">Warehouse</label>
                                                                               <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlPrType" ValidationGroup="btnSavePR">*</asp:RequiredFieldValidator>     --%>
                                                                                <asp:DropDownList ID="ddlWarehouse" runat="server" CssClass="form-control" >
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <asp:Panel ID="pnlprExtra" runat="server" Visible="false">
                                                                            <div class="form-group"  id="divJobNo" runat="server">
                                                                              <label for="exampleInputEmail1">Job No.</label>
                                                                             <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtRef" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     --%>
                                                                              <asp:TextBox ID="txtRef" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                             <div class="form-group" id="divVehicleNo" runat="server">
                                                                              <label for="exampleInputEmail1">Vehicle No.</label>
                                                                              <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtVehicleNo" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     --%>
                                                                              <asp:TextBox ID="txtVehicleNo" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                             <div class="form-group"  id="divMake" runat="server">
                                                                              <label for="exampleInputEmail1">Make</label>
                                                                             <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtMake" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     --%>
                                                                              <asp:TextBox ID="txtMake" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                            </div>

                                                                             <div class="form-group"  id="divModel" runat="server">
                                                                              <label for="exampleInputEmail1">Model</label>
                                                                              <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtModel" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     --%>
                                                                              <asp:TextBox ID="txtModel" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                                </asp:Panel>

                                                                              <div class="form-group">
                                                                                <label for="exampleInputEmail1">Expense Type</label><label id="lblExpenseType" style="color:red;"></label>
                                                                                <asp:DropDownList ID="ddlExpenseType" TextMode="MultiLine" runat="server"  CssClass="form-control">
                                                                                    <asp:ListItem Value="Capital Expense" Selected="True">Capital Expense</asp:ListItem>
                                                                                       <asp:ListItem Value="Operational Expense">Operational Expense</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>

                                                                             <div class="form-group">
                                                                                <label for="exampleInputEmail1">Requested By</label><label id="lblRequestBy" style="color:red;"></label>
                                                                               <asp:TextBox ID="txtRequestedBy" runat="server"  CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                            
                                                                             <div class="form-group">
                                                                                <label for="exampleInputEmail1">Quotation For</label><label id="lblQuotationFor" style="color:red;"></label>
                                                                                <asp:TextBox ID="txtQuotationFor" TextMode="MultiLine" runat="server" Rows="8"  CssClass="form-control"></asp:TextBox>
                                                                            </div>
                                                                          <!-- /.form-group -->
                                                                        </div>
                                                                        <!-- /.col -->
                                                                      </div>
                                                                      <!-- /.row -->
                                                                    </div>
                                                                    <!-- /.box-body -->
                                                                  </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="tab-pane" id="addItem">
                                                        <div class="row">
                                                            <div class="col-xs-12">
                                                                <div class="panel panel-default" id="AddItemsDiv">
                                                                    <!-- /.box-header -->


                                                                    <div class="panel-body">
                                                                      <div class="row">
                                                                          <div class="col-md-6">
                                                                            <%--<div class="form-group">
                                                                                <div class="row">
                                                                                    <div class="col-xs-12">
                                                                                        <label for="exampleInputEmail1">Main Category</label>
                                                                                        <asp:TextBox runat="server" ReadOnly="true" class="form-control" ID="txtCategory">
                                                                                            </asp:TextBox>
                                                                                    </div>
                                                                                    <%--<div class="col-xs-11" style="padding-right:2px;">
                                                                                        <label for="exampleInputEmail1">Main Category</label>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlMainCateGory" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                                                                                        <asp:DropDownList ID="ddlMainCateGory"  Width="100%"  runat="server" CssClass="form-control category-cl"  
                                                                                            AutoPostBack="true" 
                                                                                            onselectedindexchanged="ddlMainCateGory_SelectedIndexChanged">
                                                                                        </asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-xs-1" style="padding-left:2px;">
                                                                                          <label for="exampleInputEmail1">&nbsp;&nbsp;</label>
                                                                                        <asp:LinkButton runat="server" ID="btnRefreshCategory" OnClick="btnRefreshCategory_Click" ToolTip="Refresh" CssClass="btn btn-primary form-control" style="padding:8px 0px 0px 0px;"><i class="fa fa-refresh"></i></asp:LinkButton>
                                                                                     </div>
                                                                                 </div>

                                                                            </div>
                                                                            <div class="form-group">
                                                                                 <div class="row">
                                                                                     <div class="col-xs-12">
                                                                                        <label for="exampleInputEmail1">Sub Category</label>
                                                                                        <asp:TextBox runat="server"  ReadOnly="true" class="form-control" ID="txtSubCategory">
                                                                                            </asp:TextBox>
                                                                                    </div>
                                                                                    <%--<div class="col-xs-11" style="padding-right:2px;">
                                                                                        <label for="exampleInputEmail1">Sub Category</label>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlSubCategory" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                                                                                        <asp:DropDownList ID="ddlSubCategory"  Width="100%" runat="server" CssClass="form-control sub-category-cl"  AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged"></asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-xs-1" style="padding-left:2px;">
                                                                                          <label for="exampleInputEmail1">&nbsp;&nbsp;</label>
                                                                                        <asp:LinkButton runat="server" ID="btnRefreshSubCategory" OnClick="btnRefreshSubCategory_Click" ToolTip="Refresh" CssClass="btn btn-primary form-control" style="padding:8px 0px 0px 0px;"><i class="fa fa-refresh"></i></asp:LinkButton>
                                                                                     </div>
                                                                                 </div>
                                                                            </div>--%>
                                                                             <div class="form-group">
                                                                                 <div class="row">
                                                                                     <div class="col-xs-10" style="padding-right:2px;">
                                                                                        <label for="exampleInputEmail1">Item Name</label><label style="color:red" runat="server" id="itemNameVal" visible="false">*</label>
                                                                                        <asp:TextBox runat="server" ReadOnly="true" class="form-control" ID="txtItem">
                                                                                            </asp:TextBox>
                                                                                    </div>
                                                                                     <div class="col-xs-2" style="padding-left:2px;">
                                                                                        <label for="exampleInputEmail1">&nbsp;&nbsp;</label>
                                                                                        <asp:Button CssClass="btn btn-primary form-control searchItem" runat="server" ID="btnSearch" Text="Search" OnClientClick="f()" />
                                                                                    </div>
                                                                                    <%--<div class="col-xs-11" style="padding-right:2px;">
                                                                                        <label for="exampleInputEmail1" style="display:inline-block;">Item Name</label>
                                                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlItemName" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>    
                                                                                        <asp:DropDownList ID="ddlItemName"  Width="100%" runat="server" CssClass="form-control item-cl"></asp:DropDownList>
                                                                                    </div>
                                                                                    <div class="col-xs-1" style="padding-left:2px;">
                                                                                          <label for="exampleInputEmail1">&nbsp;&nbsp;</label>
                                                                                        <asp:LinkButton runat="server" ID="btnRefreshItem" OnClick="btnRefreshItem_Click" ToolTip="Refresh" CssClass="btn btn-primary form-control" style="padding:8px 0px 0px 0px;"><i class="fa fa-refresh"></i></asp:LinkButton>
                                                                                     </div>--%>
                                                                                 </div>
                                                                            </div>

                  
                                                                          <%--<div class="form-group">
                                                                            <label>Minimal</label>
                                                                            <select class="select2 form-control ">
                                                                              <option selected="selected">Alabama</option>
                                                                              <option>Alaska</option>
                                                                              <option>California</option>
                                                                              <option>Delaware</option>
                                                                              <option>Tennessee</option>
                                                                              <option>Texas</option>
                                                                              <option>Washington</option>
                                                                            </select>
                                                                          </div>--%>
                                                                              
                                                                             <div class="form-group">
                                                                                <label for="exampleInputEmail1">Item Description</label>
                                                                                 <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="6" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                                                            </div>

                                                                              <div class="form-group">
                                                                                  <div class="row">
                                                                                      <div class="col-xs-5">
                                                                                          <label for="exampleInputEmail1">Estimated Unit Price</label>
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEstimatedAmount" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                   
                                                                             <asp:TextBox ID="txtEstimatedAmount" type="number" runat="server" CssClass="form-control" placeholder="" step=".01" min="0"></asp:TextBox>
                                                                                      </div>
                                                                                      <div class="col-xs-3" style="padding-right:2px;">
                                                                                          <label for="exampleInputEmail1">Item Quantity</label>
                                                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtQty" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                                                                                         <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" placeholder="" type="number" step=".01" min="0.01"></asp:TextBox>
                                                                                      </div>
                                                                                      <div class="col-xs-3" style="padding-left:2px; padding-right:2px;">
                                                                                          <label for="exampleInputEmail1">Measurement</label>
                                                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlMeasurement" InitialValue="0" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                                                                                <asp:DropDownList ID="ddlMeasurement" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                                                      </div>
                                                                                      <div class="col-xs-1" style="padding-left:2px;">
                              
                                                                                          <label for="exampleInputEmail1">&nbsp;&nbsp;</label>
                                                                                <asp:LinkButton runat="server" ID="btnRefreshMeasurement" OnClick="btnRefreshMeasurement_Click" ToolTip="Refresh" CssClass="btn btn-primary form-control" style="padding:8px 0px 0px 0px;"><i class="fa fa-refresh"></i></asp:LinkButton>
                                                                                      </div>
                                                                                  </div>
                                                                              </div>

                                                                              <div class="form-group">
                                                                                <label for="exampleInputEmail1">Item Specifications</label>&nbsp;<label id="itemCount" style="color:red; font-weight:bold;  border:solid 1px;  border-color:blue; border-radius:5px;"></label>
                                                                                <button type="button"  class="btn btn-group-justified" data-toggle="modal" data-target="#myModal">Add Item Description</button>
                                                                            </div>

                                                                          <!-- /.form-group -->
                                                                        </div>

                                                                          <div class="col-md-6">
                                                                           
                                                                            
                                                                             <div class="form-group">
                                                                             <label for="exampleInputEmail1">Remarks</label>
                                                                             <%-- <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtPurpose" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     --%>
                                                                             <asp:TextBox ID="txtPurpose" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                                                                           </div>
                                                                            <%--<div class="form-group">
                                                                                <asp:Button ID="btnexisting" CssClass="btn btn-group-justified" runat="server" OnClick="btnexisting_Click" Text="Existing Item Specification"></asp:Button>
                   
                                                                            </div>--%>
                                                                          <div class="row">
                                                                            <div class="col-md-6">
                                                                             <div class="form-group">
                                                                            <label for="exampleInputEmail1">Replacement</label>
                                                                            <div class="input-group">
                                                                                    <span class="input-group-addon">
                                                                                    <asp:RadioButton ID="rdoEnable" runat="server" GroupName="RegularMenu"></asp:RadioButton>
                                                                                    </span>
                                                                                    <asp:TextBox ID="txtEnable" runat="server" class="form-control" Text="Yes"></asp:TextBox>
                                                                              </div>
                                                                              </div>
                                                                              </div>
                                                                            <div class="col-md-6">
                                                                             <div class="form-group">
                                                                            <label for="exampleInputEmail1" style="visibility:hidden">Replacement</label>
                                                                                 <div class="input-group">
                                                                                    <span class="input-group-addon">
                                                                                      <asp:RadioButton ID="rdoDisable" runat="server" GroupName="RegularMenu" Checked></asp:RadioButton>
                                                                                    </span>
                                                                                 <asp:TextBox ID="txtDisable" runat="server" class="form-control" Text="No"></asp:TextBox>
                                                                              </div>
                                                                              </div>
                                                                            </div>
                                                                           </div>
                                                                            <div class="form-group" id="divReplacementImage">
                                                                                <label for="exampleInputEmail1">Upload Replacement Images (jpg,jpeg,png,gif)</label>
                                                                                &nbsp;&nbsp;
                                                                                <asp:Label ID="lblReplaceimageDelete" runat="server" Text="" style = "font-weight: bold;"></asp:Label>
                                                                                <input type="file" class="form-control" id="fileReplace" name="fileReplace[]"  accept="image/*" data-type='image' multiple/>
                                                                                <div id="fileReplacePip"></div>
                                                                                <div>
                                                                                  <asp:GridView runat="server" ID="gvRepacementImages" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive">
                                                                                      <Columns>
                                                                                          <asp:BoundField DataField="PrId"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="ItemId"  ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="FilePath" HeaderText="FilePath" ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="FileName" HeaderText="FileName"/>
                                                                                          <asp:TemplateField HeaderText="Image">
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="imgPicture" runat="server" ImageUrl='<%# Eval("FilePath") %>' style="width:100px"/>
                                                                                            </ItemTemplate>
                                                                                          </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Large Preview">
                                                                                              <ItemTemplate>
                                                                                                  <asp:LinkButton runat="server" ID="lbtnViewReplacementImage" OnClick="lbtnViewReplacementImage_Click">View</asp:LinkButton>
                                                                                              </ItemTemplate>
                                                                                          </asp:TemplateField>

                                                                                          <asp:TemplateField HeaderText="Delete">
                                                                                              <ItemTemplate>
                                                                                                  <asp:LinkButton runat="server" ID="lbtnDeleteReplacementImage" OnClick="lbtnDeleteReplacementImage_Click">Delete</asp:LinkButton>
                                                                                              </ItemTemplate>
                                                                                          </asp:TemplateField>
                                                                                      </Columns>
                                                                                  </asp:GridView>
                                                                              </div>
                                                                                <%--<asp:FileUpload runat="server" CssClass="form-control" ID="fileUpload2" AllowMultiple="true" ></asp:FileUpload>--%>
                                                                            </div>
                                                                           <%-- <div class="form-group">
                                                                                <label for="exampleInputEmail1">Upload Image(Multiple)</label>
                                                                                <asp:FileUpload runat="server" CssClass="form-control" ID="fileUpload1" AllowMultiple="true" ></asp:FileUpload>
                                                                            </div>--%>
                                                                           <div class="form-group">
                                                                                <label for="exampleInputEmail1">Upload Standard Images (jpg,jpeg,png,gif)</label>&nbsp;&nbsp;
                                                                                <asp:Label ID="lblImageDeletedMsg" runat="server" Text="" style = "font-weight: bold;"></asp:Label>
                                                                                <input type="file" class="form-control"  id="files" name="files[]" accept="image/*" data-type='image' multiple  />
                                                                              <div id="filesPip">

                                                                              </div>


                                                                                   <asp:GridView runat="server" ID="gvPrUploadedFiles" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive">
                                                                                      <Columns>
                                                                                          <asp:BoundField DataField="PrId"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="ItemId"  ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="FilePath" HeaderText="FilePath" ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="FileName" HeaderText="FileName"/>
                              
                                                                                          <asp:TemplateField HeaderText="Image">
                                                                                            <ItemTemplate>
                                                                                                <asp:Image ID="imgPicture" runat="server" ImageUrl='<%# Eval("FilePath") %>' target="_blank" style="width:100px"/>
                                                                                            </ItemTemplate>
                                                                                          </asp:TemplateField>
                                                                                          <asp:TemplateField HeaderText="Large Preview">
                                                                                              <ItemTemplate>
                                                                                                  <asp:LinkButton runat="server" ID="lbtnViewUploadImage" OnClick="lbtnViewUploadImage_Click">View</asp:LinkButton>
                                                                                              </ItemTemplate>
                                                                                          </asp:TemplateField>

                                                                                          <asp:TemplateField HeaderText="Delete">
                                                                                              <ItemTemplate>
                                                                                                  <asp:LinkButton runat="server" ID="lbtnDeleteUploadImage" OnClick="lbtnDeleteUploadImage_Click" >Delete</asp:LinkButton>
                                                                                              </ItemTemplate>
                                                                                          </asp:TemplateField>
                                                                                      </Columns>
                                                                                  </asp:GridView>
            
               
                                                                            </div>
               
              

                                                                              <div class="form-group">
                                                                                <label for="exampleInputEmail1">Upload Supportive Documents</label>&nbsp;&nbsp;
                                                                                <asp:Label ID="Label1" runat="server" Text="" style = "font-weight: bold;"></asp:Label>
                                                                                <input type="file" class="form-control"  id="supportivefiles" name="supportivefiles[]"  multiple onchange="readFilesURL(this);" />
                                                                                   <div class="form-group"  style="overflow-y:auto; overflow-x:hidden; max-height:300px;">

                                                                              <table id="tblUpload" class="table table-hover" style="border:none;"  border="1">
                                                                             </table>

                                                                          </div> 
                   
                                                                                   <asp:GridView runat="server" ID="gvSupporiveFiles" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive">
                                                                                      <Columns>
                                                                                          <asp:BoundField DataField="PrId"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="ItemId"  ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="FilePath" HeaderText="FilePath" ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden"/>
                                                                                          <asp:BoundField DataField="FileName" HeaderText="FileName"/>
                              
                                                                                          <asp:TemplateField HeaderText="Large Preview">
                                                                                              <ItemTemplate>
                                                                                                  <asp:LinkButton runat="server" ID="lbtnViewUploadSupporiveDocument" OnClick="lbtnViewUploadSupporiveDocument_Click">View</asp:LinkButton>
                                                                                              </ItemTemplate>
                                                                                          </asp:TemplateField>

                                                                                          <asp:TemplateField HeaderText="Delete">
                                                                                              <ItemTemplate>
                                                                                                  <asp:LinkButton runat="server" ID="lbtnDeleteSupportiveDocument" OnClick="lbtnDeleteSupportiveDocument_Click" >Delete</asp:LinkButton>
                                                                                              </ItemTemplate>
                                                                                          </asp:TemplateField>
                                                                                      </Columns>
                                                                                  </asp:GridView>
             
                                                                            </div>
                                                                              
                                                                          <!-- /.form-group -->
                                                                        </div>
                                                                      </div>
                                                                    </div>

                                                                    <!-- /.box-body -->
                                                                    <div class="panel-footer">
                                                                    <div class="form-group">
                                                                          <label for="exampleInputEmail1" style="visibility:hidden">Category Name</label>
                                                                           <asp:Button ID="btnClear" ValidationGroup="btnAdd" runat="server" Text="Clear" 
                                                                              CssClass="btn btn-danger pull-right" onclick="btnClear_Click" OnClientClick="return ClearBom();"/>
                                                                          <asp:Button ID="btnAdd" ValidationGroup="btnAdd" runat="server" Text="Add Item" 
                                                                              CssClass="btn btn-primary pull-right btnAddCl" onclick="btnAdd_Click" OnClientClick="return BindToList()" style="margin-right:10px" />
                                                                          <asp:Label runat="server" ID="lblAlertMsg" Text="" CssClass="pull-right" style="margin-right:20px;margin-top: 8px;color:  red;font-weight: bold;" ></asp:Label>
                                                                      </div>
                                                                    </div>
                                                                  </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-xs-12">
                                                                 <div class="panel panel-default">
                                                                     <div class="panel-body">
                                                                 <div class="row">
                                                                    <div class="col-md-12">
                                                                         <asp:GridView ID="gvDatataTable" runat="server" EmptyDataText="No Items Found" AutoGenerateColumns="false" CssClass="table table-responsive table-striped"
                                                                                        GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:BoundField DataField="MainCategoryId" HeaderText="MainCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                            <asp:BoundField DataField="MainCategoryName" HeaderText="Category Name" />
                                                                                            <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                            <asp:BoundField DataField="SubcategoryName" HeaderText="Sub Category Name" />
                                                                                            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                                                            <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
                                                                                            <asp:BoundField DataField="ItemDescription" HeaderText="Description" />
                                                                                            <asp:BoundField DataField="BiddingPeriod" HeaderText="Bidding Period" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                            <asp:BoundField DataField="ReplacementId" HeaderText="ReplacementId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                                                            <asp:BoundField DataField="ReplacementName" HeaderText="Replacement" />
                                                                                            <asp:BoundField DataField="Purpose" HeaderText="Remarks" />
                                   
                                                                                           <asp:TemplateField HeaderText="IsActive"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                             <ItemTemplate>
                                                                                                <asp:Label Text='<%#Eval("ItemId") %>' ID="lblItemId" runat="server"></asp:Label>
                                                                                             </ItemTemplate>
                                                                                           </asp:TemplateField>

                                                                                               <asp:TemplateField HeaderText="Replacement Images">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="btnViewzReplacementPhotos" OnClick="btnViewzReplacementPhotos_Click" runat="server" Text="View Replacement Photos"/>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>

                                                                                             <asp:TemplateField HeaderText="Images">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="btnViewUploadPhotos" OnClick="lbtnViewUploadPhotos_Click" runat="server" Text="View Photos" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                              <asp:TemplateField HeaderText="Specification">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="btnViewBomDetails" OnClick="lbtnViewBOM_Click" runat="server" Text="View Item Specifications" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Edit">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btnEditItem" ImageUrl="~/images/document.png" OnClick="btnEditItem_Click" OnClientClick="return scrollToEdit()" runat="server" style="width:25px;" />
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateField>
                                                                                            <asp:TemplateField HeaderText="Delete">
                                                                                                <ItemTemplate>
                                                                                                    <asp:ImageButton ID="btnDeleteItem" ImageUrl="~/images/delete.png"  CssClass="deleteItem" OnClick="btnDeleteItem_Click"
                                                                                                        runat="server" style="width:25px;"/>
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
                                               
                                            <div class="tab-pane" id="finalize">
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body">
                                                                <div class="form-group">
                                                                                <label for="exampleInputEmail1">Terms and Conditions</label>
                                                                                <%--<asp:TextBox ID="txtTerms" name="txtTerms" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>--%>
                                                                    <CKEditor:CKEditorControl ID="txtTerms" BasePath="/ckeditor/" runat="server" Toolbar="Basic"></CKEditor:CKEditorControl>
                                                                            </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            
                                                <div class="row">
                                                    <div class="col-xs-12">
                                                        <div class="panel panel-default">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-xs-12">
                                                                        
                                                                        <img id="loader" alt="" src="SupplierPortalAssets/assets/img/loader-info.gif" class="pull-right hidden" style="margin-right:10px; max-height:40px;" />
                                                                        <asp:Button ID="btnSavePR"  runat="server" Text="Create Purchase Request"  ValidationGroup="btnSavePR1"  CssClass="btn btn-primary pull-right btnCreate hidden" OnClick="btnSavePR_Click"></asp:Button>
                                                                        
                                                                        <button type="button" class=" btn-md pull-right btnNext">Next</button>
                                                                        <button  type="button" class=" btn-md pull-right btnPrev" >Previous</button>
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
                    </section>



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
                                    <p style="font-weight: bold; font-size: medium;">PR has been created Successfully</p>
                                </div>
                                <div class="modal-footer">
                                    <span class="btn btn-info" data-dismiss="modal" aria-label="Close">OK</span>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div id="mdlSearch" class="modal fade">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #3c8dbc;">
                                    <button type="button"
                                        class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                    <h4 class="modal-title" style="color: white; font-weight: bold;">Search Item</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-md-12 text-center">
                                            <label>Search by Item Name / Item Code</label></br>
                                            <input id='txtItemName' type='text' class='form-control' placeholder="Type first four letters" autofocus /></br>
                                            
                                            <img id="imgProgressSearch" src="AdminResources/images/Spinner-0.6s-200px.gif" style="max-height: 40px" /></br>
                                            <table id="tblItems" class="table table-responsive">
                                                <thead style="background-color: #3c8dbc; color: white;">
                                                    <tr>
                                                        <th class="text-center">Category</th>
                                                        <th class="text-center">SubCategory</th>
                                                        <th class="text-center">Item</th>
                                                        <th class="text-center">Action</th>
                                                    </tr>
                                                </thead>
                                                <tbody id="tblItemsBody">
                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <span class="btn btn-info" data-dismiss="modal" aria-label="Close">OK</span>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="myModalViewBom" class="modal modal-primary fade in" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content" style="background-color: #a2bdcc;">
                                <div class="modal-header" style="background-color: #7bd47dfa;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">View Bill Of Materials</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="login-w3ls">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvTempBoms" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Specifications Found" Style="border-collapse: collapse; color: black;"
                                                        GridLines="None" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="PrId" HeaderText="Pr Id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="ItemId" HeaderText="Item Id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="SeqNo" HeaderText="Seq_ID" />
                                                            <asp:BoundField DataField="Meterial" HeaderText="Material" />
                                                            <asp:BoundField DataField="Description" HeaderText="Description" />

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div>
                                                <label id="lbMailMessage1" style="margin: 3px; color: maroon; text-align: center;"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="myModalUploadedPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content" style="background-color: #a2bdcc;">
                                <div class="modal-header" style="background-color: #7bd47dfa;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Uploaded Item Photos</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="login-w3ls">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                        AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                            <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
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

                    <div id="myModalReplacementPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content" style="background-color: #a2bdcc;">
                                <div class="modal-header" style="background-color: #7bd47dfa;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                    <h4 class="modal-title">Replacement Item Photos</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="login-w3ls">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvReplacementPhotos" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                        AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                            <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
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

                    <section class="content">


      

     </section>

                    <section class="content">

                        
     <div class="row" style=" visible="false">
        <div class="col-sm-12">
           <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
    </div>
        </div>
    </div>
     </section>
                    <asp:HiddenField ID="HiddenField2" runat="server"></asp:HiddenField>



                    <div id="specification" class="modal modal-primary fade in" tabindex="-1" role="dialog" aria-hidden="false">
                        <div class="modal-dialog">
                            <!-- Modal content-->
                            <div class="modal-content" style="background-color: #a2bdcc;">
                                <div class="modal-header" style="background-color: #7bd47dfa;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="False">×</span></button>
                                    <h4 class="modal-title">View Specification</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="login-3lsw">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvSpecificationBoms" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Specifications Found" Style="border-collapse: collapse; color: black;"
                                                        GridLines="None" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField DataField="itemId" HeaderText="Item Id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="SeqNo" HeaderText="Seq_ID" />
                                                            <asp:BoundField DataField="MATERIAL" HeaderText="Material" />
                                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div>
                                                <label id="Label2" style="margin: 3px; color: maroon; text-align: center;"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:HiddenField ID="hdnSelectedItemId" runat="server"></asp:HiddenField>

                    <asp:HiddenField ID="hdnReqDate" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnItemId" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnItemName" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnField" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnActiveTab" runat="server" Value="#basic"></asp:HiddenField>
                    <asp:HiddenField ID="hdnShowSuccess" runat="server" Value="0"></asp:HiddenField>
                    <asp:HiddenField ID="HiddenField1" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnCategoryId" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnSubCategoryId" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnCategoryName" runat="server"></asp:HiddenField>
                    <asp:HiddenField ID="hdnSubCategoryName" runat="server"></asp:HiddenField>
                    <asp:Button runat="server" ID="btnItemSelected" OnClick="btnItemSelected_Click" CssClass="hidden" />

                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnAdd" />
                    <asp:PostBackTrigger ControlID="btnClear" />
                </Triggers>
            </asp:UpdatePanel>

        </body>
    </form>
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>
    <script src="AdminResources/js/select2.full.min.js"></script>

    <script type="text/javascript">

        $(function () {

            $('.ddlPRCategoryCl').select2()
            //$('.category-cl').select2()
            //$('.sub-category-cl').select2()
            //$('.item-cl').select2()


            <%--$('#<%=txtQty.ClientID%>').keypress(function (e) {
                if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42) {
                } else {
                    return false;
                }
            });--%>
        });

        <%--$(function () {
            $('#<%=txtEstimatedAmount.ClientID%>').keypress(function (e) {
                if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42) {
                } else {
                    return false;
                }
            });
        });--%>

        /*function showBOMModal()
        {
            document.getElementById('myModalViewBom').style.display = "block";
        }

        function showUploadedPhotosModal() {
            document.getElementById('myModalUploadedPhotos').style.display = "block";
        }

        function showReplacementPhotosModal() {
            document.getElementById('myModalReplacementPhotos').style.display = "block";
        }*/

        function scrollToEdit() {
            document.getElementById("AddItemsDiv").scrollIntoView();
            return true;
        }

        function scrollToTop() {
            ScrollPageToUp();
            return true;

        }







    </script>

    <script type="text/javascript">
        function insRow() {
            var tbl = document.getElementById('myTable');
            var len = tbl.rows.length;
            var row = tbl.insertRow(len);
            for (var i = 0; i < tbl.rows[0].cells.length - 1; i++) {
                row.insertCell(i).innerHTML = "<input type=text style=color:Black;>";
            }
            row.insertCell(tbl.rows[0].cells.length - 1).innerHTML = '<input type="button" onclick="delRow(this)" value="Delete Row" style=" background-color: red; ">';
        }

        function delRow(obj) {
            var row = obj;
            while (row.nodeName.toLowerCase() != 'tr') {
                row = row.parentNode;
            }
            var tbl = document.getElementById('myTable');
            tbl.deleteRow(row.rowIndex);

        }
    </script>

    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>



    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $(document).ready(function () {

                CKEDITOR.replaceClass = "ckeditor";

                $('.nav a[href="' + $('#ContentSection_hdnActiveTab').val() + '"]').tab('show');

                if ($('#ContentSection_hdnActiveTab').val() == '#basic') {
                    $('.btnPrev').addClass('hidden');
                }

                if ($('#ContentSection_hdnShowSuccess').val() == "1") {
                    debugger;
                    swal({
                        type: 'success', title: 'SUCCESS', text: 'Your work has been saved', showConfirmButton: false, timer: 1500
                    }).then(
                        (result) => {
                            $('#ContentSection_hdnShowSuccess').val('0');
                            $('#loader').addClass('hidden');
                            $('#ContentSection_hdnActiveTab').val('#basic');
                            $('.nav a[href="#basic"]').tab('show');
                            $('.btnNext').removeClass('hidden');
                            $('.btnPrev').addClass('hidden');
                            $('.btnCreate').addClass('hidden');
                        });
                }
                $('.btnCreate').on({
                    click: function () {
                        $('#loader').removeClass('hidden');
                        $(this).addClass('hidden');
                    }
                })

                $('.btnNext').on({
                    click: function () {

                        event.preventDefault();

                        if ($('#ContentSection_hdnActiveTab').val() == '#basic') {
                            //$('#liAddItem').click();
                            $('#ContentSection_hdnActiveTab').val('#addItem');
                            $('.nav a[href="#addItem"]').tab('show');
                            $('.btnPrev').removeClass('hidden');
                            $('.btnNext').removeClass('hidden');
                        }
                        else {
                            $('#ContentSection_hdnActiveTab').val('#finalize');
                            $('.nav a[href="#finalize"]').tab('show');
                            $(this).addClass('hidden');
                            $('.btnCreate').removeClass('hidden');

                        }
                        window.scrollTo({ top: 0, behavior: 'smooth' });
                    }
                })



                $('.btnPrev').on({
                    click: function () {
                        event.preventDefault();

                        if ($('#ContentSection_hdnActiveTab').val() == '#addItem') {
                            //$('#liAddItem').click();
                            $('#ContentSection_hdnActiveTab').val('#basic');
                            $('.nav a[href="#basic"]').tab('show');
                            $('.btnNext').removeClass('hidden');
                            $('.btnPrev').addClass('hidden');
                            $('.btnCreate').addClass('hidden');
                        }
                        else {
                            $('#ContentSection_hdnActiveTab').val('#addItem');
                            $('.nav a[href="#addItem"]').tab('show');
                            $('.btnPrev').removeClass('hidden');
                            $('.btnNext').removeClass('hidden');
                            $('.btnCreate').addClass('hidden');

                        }
                        window.scrollTo({ top: 0, behavior: 'smooth' });
                    }
                })

                $(".deleteItem").click(function () {
                    var itemId = $(this).parent().prev().prev().prev().prev().prev().children().html();
                    $("#<%=hdnItemId.ClientID%>").val(itemId);
                    document.body.scrollTop = 0;
                    document.documentElement.scrollTop = 0;
                });

                $('.searchItem').on({
                    click: function () {
                        event.preventDefault();

                        $('#mdlSearch').modal('show');

                    }
                })

                var currentRequest = null;
                $('#imgProgressSearch').hide();

                $('#txtItemName').on({
                    keyup: function () {


                        if ($("#txtItemName").val().length > 1) {


                            $('#imgProgressSearch').show();

                            currentRequest = jQuery.ajax({
                                type: "POST",
                                url: "CompnayPurchaseRequestNote.aspx/SearchItem",
                                data: '{text: "' + $("#txtItemName").val() + '",categoryId:' + $('#ContentSection_ddlPRCategory').val() + '}',
                                contentType: "application/json; charset=utf-8",
                                dataType: 'json',
                                beforeSend: function () {
                                    if (currentRequest != null) {
                                        currentRequest.abort();
                                    }
                                },
                                success: function (response) {
                                    if (response.d.length == 0) {
                                        $('#tblItemsBody').html("<tr><td class='hidden'>0" +
                                            "</td><td>Undefined" +
                                            "</td><td class='hidden'>0" +
                                            "</td><td>Undefined" +
                                            "</td><td class='hidden'>" + Math.floor(new Date().getTime() / 1000) +
                                            "</td><td>" + $("#txtItemName").val() +
                                            "</td><td><a class='btn btn-info btn-xs btnSelectItem'>Select</a></td></tr>");
                                        $('#imgProgressSearch').hide();
                                    }
                                    else {
                                        var arr = response.d;
                                        var htmlText = "";
                                        for (var i = 0; i < arr.length; i++) {
                                            htmlText += "<tr><td class='hidden'>" + arr[i].CategoryId +
                                                "</td><td>" + arr[i].CategoryName +
                                                "</td><td class='hidden'>" + arr[i].SubCatergoryId +
                                                "</td><td>" + arr[i].SubCategoryName +
                                                "</td><td class='hidden'>" + arr[i].ItemId +
                                                "</td><td>" + arr[i].ItemName +
                                                "</td><td><a class='btn btn-info btn-xs btnSelectItem'>Select</a></td></tr>";
                                        }

                                        $('#tblItemsBody').html(htmlText);
                                        var a = $('#imgProgressSearch');
                                        $('#imgProgressSearch').hide();
                                    }
                                }
                            });
                        }
                        else {

                            $('#tblItemsBody').html('');
                        }

                    }
                });

                $("#tblItemsBody").delegate(".btnSelectItem", "click", function () {
                    event.preventDefault();


                    var row = $(this).closest('tr').find('td');


                    $('#ContentSection_hdnCategoryId').val($(row).eq(0).text());
                    $('#ContentSection_hdnCategoryName').val($(row).eq(1).text());
                    //$('#ContentSection_txtCategory').val($(row).eq(1).text());
                    $('#ContentSection_hdnSubCategoryId').val($(row).eq(2).text());
                    $('#ContentSection_hdnSubCategoryName').val($(row).eq(3).text());
                    //$('#ContentSection_txtSubCategory').val($(row).eq(3).text());
                    $('#ContentSection_hdnItemId').val($(row).eq(4).text());
                    $('#ContentSection_hdnItemName').val($(row).eq(5).text());
                    $('#ContentSection_txtItem').val($(row).eq(5).text());
                    $('#mdlSearch').modal('hide');
                    $('#ContentSection_btnItemSelected').click();
                });


            });
            <%--$("#<% =txtItem.ClientID%>").on({
            keyup: function () {
                $("#<% =txtItem.ClientID%>").autocomplete({
                    minLength: 1,
                    appendTo: "#container",
                    source: function (request, response) {
                        $.ajax({
                            type: "POST",
                            contentType: "application/json; charset=utf-8",
                            url: "CompnayPurchaseRequestNote.aspx/LoadItemNames",
                            data: "{input:'" + request.term + "'}",
                            dataType: "json",
                            success: function (output) {
                                var array = output.error ? [] : $.map(output.d, function (m) {
                                    return {
                                        label: m.label,
                                        value: m.value
                                    };
                                });

                                response(array);

                            },
                            error: function (errormsg) {
                                alert("Invalid Name");
                            }
                        });
                    },
                    select: function (event, ui) {
                        $("#<% =txtItem.ClientID%>").val(ui.item.label); // display the selected text
                        $("#<%=hdnSelectedItemId.ClientID%>").val(ui.item.value); // save selected id to hidden input
                        return false;
                    }
                });
            }
        });--%>
        });
    </script>

    <script type="text/javascript">



        $("#btnNoConfirmYesNo").on('click').click(function () {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        });


        $("#<%=btnSavePR.ClientID%>").click(function () {
            if ($("#<%=txtRequestedBy.ClientID%>").val() == "" || $("#<%=txtQuotationFor.ClientID%>").val() == "") {
                event.preventDefault();

                $("#lblRequestBy").text('');
                $("#lblQuotationFor").text('');

                if ($("#<%=txtRequestedBy.ClientID%>").val() == "") {
                    $("#lblRequestBy").text('*');
                }
                if ($("#<%=txtQuotationFor.ClientID%>").val() == "") {
                    $("#lblQuotationFor").text('*');
                }
                $('#loader').addClass('hidden');

                swal({ type: 'error', title: 'ERROR', text: 'Please fill out all the fields marked with *', showConfirmButton: false, timer: 1500 });

                $('#ContentSection_hdnActiveTab').val('#basic');
                $('.nav a[href="#basic"]').tab('show');
                $('.btnNext').removeClass('hidden');
                $('.btnPrev').addClass('hidden');
                $('.btnCreate').addClass('hidden');


            }
            else {
                for (instance in CKEDITOR.instances) {
                    CKEDITOR.instances[instance].updateElement();
                }
            }
        });




    </script>



    <%-- <script type="text/javascript">
        $(document).ready(function () {
            $('#ContentSection_rdoEnable').change(function () {
                document.getElementById("fileReplace").disabled = false;
            });
            $('#ContentSection_rdoDisable').change(function () {
                document.getElementById("fileReplace").disabled = true;
            });
        });
    </script>--%>

    <script type="text/javascript">
        $('#btnOki').click(function () {
            debugger;
            var $confirm = $("#SuccessAlert");
            $confirm.modal('hide');
            return this.false;
            event.preventdefault();
        });

    </script>


    <script type="text/javascript">
        $(document).ready(function () {
            $(".add-row").click(function () {
                $("#meterial").css('background-color', '#ffffff');
                $("#description").css('background-color', '#ffffff');
                var meterial = $("#meterial").val();
                var description = $("#description").val();
                if (meterial != "" && description != "") {
                    var markup = "<tr><td class='tdCol'><input type='checkbox' name='record'></td><td class='tdCol'>" + meterial + "</td><td class='tdCol'>" + description + "</td></tr>";
                    $(".tableCol .tbodyCol").append(markup);
                    $("#meterial").val("");
                    $("#description").val("");
                }
                else {
                    meterial == "" ? $("#meterial").css('background-color', '#f5cece') : $("#meterial").css('background-color', '#ffffff');
                    description == "" ? $("#description").css('background-color', '#f5cece') : $("#description").css('background-color', '#ffffff');
                }
                var rowCount = $('#tableCols tr').length - 1;
                $("#itemCount").text(rowCount);

            });

            // Find and remove selected table rows
            $(".delete-row").click(function () {
                $(".tableCol .tbodyCol").find('input[name="record"]').each(function () {
                    if ($(this).is(":checked")) {
                        $(this).parents("tr").remove();
                    }
                });
                var rowCount = $('#tableCols tr').length - 1;
                $("#itemCount").text(rowCount);
            });
        });
    </script>

    <script type="text/javascript">
        function LoadData() {
            var DataList = $("#ContentSection_HiddenField2").val();
            var parsedTest = JSON.parse(DataList);

            var text = "";
            var field = "";
            for (var i = 1; i <= parsedTest.length; i++) {
                field = parsedTest[i - 1].split('-');
                var htmlcode =
                    ' <tr> ' +
                    ' <td class=tdCol> ' +
                    ' <input type=checkbox name=record> ' +
                    ' </td> ' +
                    ' <td class=tdCol>' + field[0] + '</td> ' +
                    ' <td class=tdCol>' + field[1] + '</td> ' +
                    ' </tr> ' + ''
                text += htmlcode;
            }
            document.getElementById("tbodyCol").innerHTML = text;
        }
    </script>

    <script type="text/javascript">
        function GetSelectedRow(lnk) {

            $("#ContentSection_HiddenField1").val("1");
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var prid = row.cells[1].innerText;
            var itemid = row.cells[7].innerText;
            var ratus = prid + "-" + itemid;
            var jsonText = JSON.stringify({ data: ratus });
            var text = "";
            $.ajax({
                type: "POST",
                url: "CustomerPREdit.aspx/GetPRBomDetailsIds?data=" + ratus,
                data: jsonText,
                contentType: "application/json",
                dataType: "json",
                success: function (msg) {
                    if (msg.d.length > 0) {
                        for (var i = 0; i < msg.d.length; i++) {
                            var htmlcode =
                                ' <tr> ' +
                                ' <td class=tdCol> ' +
                                ' <input type=checkbox name=record> ' +
                                ' </td> ' +
                                ' <td class=tdCol>' + msg.d[i].Meterial + '</td> ' +
                                ' <td class=tdCol>' + msg.d[i].Description + '</td> ' +
                                ' </tr> ' + ''
                            text += htmlcode;
                        }
                        document.getElementById("tbodyCol").innerHTML = text;
                    }
                },
                error: function (result) {
                },
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });

            return true;

        }

        function BindToList() {

            var myTableArray = [];

            $("#tableCols tr").each(function () {
                var arrayOfThisRow = [];
                var tableData = $(this).find('td');
                if (tableData.length > 0) {
                    tableData.each(function () { arrayOfThisRow.push($(this).text()); });
                    myTableArray.push(arrayOfThisRow);
                }
            });

            $("#ContentSection_hdnField").val(myTableArray);

            // alert($("#ContentSection_hdnField").val());
            return true;
        }

    </script>

    <script type="text/javascript">
        function ClearBom() {
            document.getElementById('fileReplace').value = "";
            document.getElementById('files').value = "";
            document.getElementById('supportivefiles').value = "";
            $("#tbodyCol tr").remove();

            return true;
        }
    </script>
    <script type="text/javascript">

        function ScrollPageToUp() {
            $("#msg").scrollTop();
            return true;
        }

    </script>
    <script>
        $(document).ready(function () {
            $("#itemCount").text($('#tableCols tr').length - 1);
        });

    </script>
    <script>
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
    <script>
        $(function () {
            $('#<%=txtEstimatedAmount.ClientID%>').keypress(function (e) {
                if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42) {
                } else {
                    return false;
                }
            });
        });





    </script>

    <script>
        //function f() {
        //    $(document).ready(function () {

        //        $('#ContentSection_txtItemName').focus();

        //        alert("dffv");
        //        setTimeout(function () {
        //            // after 1000 ms we add the class animated to the login/register card
        //            $('.card').removeClass('card-hidden');
        //        }, 300);
        //    });
        //}

        $(document).on('ready', function () {
            // Set modal form input to autofocus when autofocus attribute is set
            $('.modal').on('shown.bs.modal', function () {
                $(this).find($.attr('autofocus')).focus();
            });
        });
  </script>


</asp:Content>
