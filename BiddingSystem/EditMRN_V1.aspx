<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="EditMRN_V1.aspx.cs" Inherits="BiddingSystem.EditMRN_V1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <style type="text/css">
        #myModal .modal-dialog {
            width: 90%;
        }

        #myModal2 .modal-dialog {
            width: 50%;
        }

        #specification .modal-dialog {
            width: 60%;
        }

        #myModalUploadedPhotos .modal-dialog {
            width: 50%;
        }


        #myModalReplacementPhotos .modal-dialog {
            width: 50%;
        }

        .input-group .form-control {
            position: unset;
        }

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

        #myModal .modal-dialog {
            width: 60%;
        }

        #myModal2 .modal-dialog {
            width: 70%;
        }

        #specification .modal-dialog {
            width: 60%;
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
            width: 60%;
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

        .modal-backdrop + .modal-backdrop {
            opacity: 0;
        }
    </style>
    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/moment.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <section class="content-header" style="margin-bottom: -15px;">
	<h1>
	  Edit Material Requisition 
		<small></small>
	  </h1>
	<ol class="breadcrumb">
		<li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
		<li class="active">Edit Material Requisition</li>
	  </ol>
	</section>
    <br />
    <script type="text/javascript">
        var files;
        var a = new Array();
        var b = new Array();
        var c = new Array();
        var count = 0;

        var fileReplace;
        var x = new Array();
        var y = new Array();
        var z = new Array();
        $(document).ready(function () {
            document.getElementById("fileReplace").disabled = true;
        });

        function InitClient() {
            $("#itemCount").text($("#ContentSection_hdnItemCount").val());
            //var dtp01 = new DateTimePicker('#ContentSection_DateTimeRequested', { pickerClass: 'datetimepicker-blue', timePicker: true, timePickerFormat: 12, format: 'Y/m/d h:i' });
            //		debugger;
            if (window.File && window.FileList && window.FileReader) {
                $('#ContentSection_rdoEnable').change(function () {
                    document.getElementById("fileReplace").disabled = false;
                });
                $('#ContentSection_rdoDisable').change(function () {
                    document.getElementById("fileReplace").disabled = true;
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
                    $('#filesPip').html("");
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
                            //  debugger;
                            e.preventDefault();
                            $(this).parent().remove('');
                            var removeArray = new Array();
                            removeArray = files;
                            var file = $(this).attr('file');
                            for (var i = 0; i < files.length; i++) {
                                if (files[i].name == file) {
                                    removeArray.splice(i, 1);
                                    // files.splice(i, 1);
                                    break;
                                }
                            }

                            var i = Array.indexOf($(this).index);
                            if (i != -1) {
                                Array.splice(i, 1);
                            }
                            count--;

                            $(this).parent(".pip").remove();
                        });

                    });

                    fileReader.readAsDataURL(f);
                }
            });

            InitClientReplace();
            InitClientSupportive();
        }

        function InitClientReplace() {
            //debugger;
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
                        $('#fileReplacePip').html("");
                        y.push(u.target.result);
                        $('#fileReplacePip').append("<span class=\"pip\" style='text-align: center;'>" +
							"<img class=\"imageThumb\" src=\"" + u.target.result + "\" title=\"" + file.name + "\"/>" +
							"<br/><span id=\"removeReplace\" class=\"imageThumb\"  file ='" + f.name + "' style='border:none;'><img src='images/delete.png' style='width:16px;' /></span>" +
							"</span>");

                        $("#removeReplace").click(function (e) {
                            // debugger;
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
                            count--;

                            $(this).parent(".pip").remove();
                        });

                    });
                    fileReader.readAsDataURL(f);
                }
            });
        }

        function InitClientSupportive() {
            $("#supportivefiles").on("change", function (u) {

                //debugger;
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
        <asp:Label ID="lblTheva" runat="server" Text=""></asp:Label>
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

        <asp:ScriptManager runat="server" ID="sm">
        </asp:ScriptManager>
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

                <div id="modalDeleteYesNo" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 id="lblTitleDeleteYesNo" class="modal-title">Confirmation</h4>
                            </div>
                            <div class="modal-body">
                                <p>Are you sure you want to delete ? This record will permanently get deleted</p>
                            </div>
                            <div class="modal-footer">
                                <button id="btnDeleteConfirm" type="button" class="btn btn-primary" onclick="btnDelete()">
                                    Yes</button>
                                <button id="btnNoDeleteYesNo1" onclick="hideDeleteModal()" type="button" class="btn btn-danger">
                                    No</button>
                            </div>
                        </div>
                    </div>
                </div>


                <section class="content" style="padding-top: 0px;">
	  <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
		<a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
		<strong>
			<asp:Label ID="lbMessage" runat="server"></asp:Label>
		</strong>
		</div>
	  <br />
	  <!-- SELECT2 EXAMPLE -->
	  <div id="panelPurchaseRequset" runat="server">

	  <div class="box box-primary" id="topPanel1">
		<div class="box-header with-border">
		  <h3 class="box-title" >View Material Requests</h3>

		  <div class="box-tools pull-right">
			<button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
			<button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
		  </div>
		</div>
		<!-- /.box-header -->
		<div class="box-body" >
		  <div class="row">
		 <%-- <div class="col-md-12">--%>
			<div class="col-md-10">
			<div class="form-group">
			 <label for="exampleInputEmail1">MRN Code </label>&nbsp;&nbsp;
			 <asp:Label ID="lblPrCode" runat="server" Text="" style="font-weight:bold;"></asp:Label>
			 <asp:TextBox ID="txtPRCode" type="number" CssClass="form-control input-md" AutoComplete="off" ReadOnly="true" runat="server" />
			</div>
			</div>
			<div class="col-md-2">
			<div class="form-group">
			 <label for="exampleInputEmail1" style="visibility:hidden">Enter PR Code </label>
			 <asp:Button ID="Edit" CssClass="btn btn-info" runat="server" Text="Edit" 
					style="margin-top: 28px;float:left;display:none;" onclick="Edit_Click" ></asp:Button>
			</div>
		  </div>
		<%--  <div class="col-md-2">
		  <div class="form-group">
			 <label for="exampleInputEmail1" style="visibility:hidden">Enter PR Code </label>
			 <asp:Button ID="btnRefresh" CssClass="btn btn-success" runat="server" Text="Refresh"  onclick="btnRefresh_Click"
					 ></asp:Button>
			</div>
		  </div>
		  </div> --%>
		  </div>   
		  </div>

		  <div class="box-header with-border">
		  <h3 class="box-title" style=" font-weight: bold; color: #286d9e; ">Basic Information</h3>

		  <div class="box-tools pull-right">
			<button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
			<button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
		  </div>
		</div>
		  <div class="box-body">
		  <div class="row">
		  <%--  <div class="col-md-6">
				<div class="form-group">
					<label for="exampleInputEmail1">Department Name</label>
					<asp:TextBox ID="txtDepName" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
				</div>
				<div class="form-group">
					<label for="exampleInputEmail1">Date Of Request</label>
					<asp:TextBox ID="DateTimeRequested" runat="server"  CssClass="form-control" Text="2018-05-19"></asp:TextBox>
				</div>
				<div class="form-group">
					<label for="exampleInputEmail1">Our Reference</label>
					  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtRef" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
					<asp:TextBox ID="txtRef" runat="server"  CssClass="form-control"></asp:TextBox>
				</div>
			  <!-- /.form-group -->
			</div>
			<div class="col-md-6">
				
				<div class="form-group">
					<label for="exampleInputEmail1">Requisition Number</label>
					<asp:TextBox ID="txtPrNumber" runat="server"  Enabled="false" CssClass="form-control"></asp:TextBox>
				</div>
				 <div class="form-group">
					<label for="exampleInputEmail1">Requested By</label>
					  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtRequestedBy" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
					<asp:TextBox ID="txtRequestedBy" runat="server"  CssClass="form-control"></asp:TextBox>
				</div>
				<div class="form-group">
					<label for="exampleInputEmail1">Quotation For</label>
					<asp:TextBox ID="txtQuotationFor" TextMode="MultiLine" runat="server"  CssClass="form-control"></asp:TextBox>
				</div>
		  
			  <!-- /.form-group -->
			</div>--%>




			   <div class="col-md-6">
				<div class="form-group">
					<label for="exampleInputEmail1">Company Name</label>
					<asp:TextBox ID="txtDepName" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
				</div>
			 <%--   <div class="form-group">
					<label for="exampleInputEmail1">Sub Department Name</label>
					<asp:TextBox ID="txtSubDepartment" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
				</div>--%>
				 <div class="form-group">
					<label for="exampleInputEmail1">Requisition No.</label>
					<asp:TextBox ID="txtPrNumber" runat="server"  Enabled="false" CssClass="form-control"></asp:TextBox>
				</div>
				<div class="form-group">
					<label for="exampleInputEmail1">Date Of Request</label>
					<asp:TextBox ID="DateTimeRequested" runat="server" ReadOnly="true" CssClass="form-control" ></asp:TextBox>
				</div>
		  
				 <div class="form-group">
					<label for="exampleInputEmail1">Requested For</label><label id="lblQuotationFor" style="color:red;">(*)</label>
					<asp:TextBox ID="txtQuotationFor" TextMode="MultiLine" runat="server"  CssClass="form-control"></asp:TextBox>
				</div>
                   <div class="form-group">
					<label for="requiredDate">Required Date</label><label id="lblRequiredDate" style="color:red;">(*)</label>
					<asp:TextBox ID="txtRequiredDate" runat="server" CssClass="form-control customDate" AutoPostBack="true"  type="date" data-date="" data-date-format="DD MMMM YYYY" placeholder="Required Date" ></asp:TextBox>
				</div>
                                    <div class="form-group">
                    <label for="exampleInputEmail1">Warehouse</label>
                    <asp:DropDownList ID="ddlWarehouse" runat="server" CssClass="form-control"  
                        OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged" >
                    </asp:DropDownList>
                </div>

                     <div class="form-group">
                    <label for="exampleInputEmail1">Requisition Main Category</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlMainCateGory" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
                    <asp:DropDownList ID="ddlMainCateGory" runat="server" CssClass="form-control"  
                        AutoPostBack="true" onselectedindexchanged="ddlMainCateGory_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
				<!-- -->
				 
			  <!-- /.form-group -->
			</div>
			<div class="col-md-6">
				

                <div class="row">
                     <div class="col-md-6">
                 <div class="form-group">
                <label for="Expense">Expense Type</label>

                <div class="input-group">
                        <span class="input-group-addon">
                        <asp:RadioButton ID="rdoCapitalExpense" runat="server" AutoPostBack="true"   GroupName="Expense"  OnCheckedChanged="rdoCapitalExpense_CheckedChanged"  Checked  >
                        </asp:RadioButton>
                        </span>
                        <asp:TextBox ID="txtCapitalExpense" disabled="disabled" runat="server"  class="form-control" Text="Capital Expense" ></asp:TextBox>
                  </div>
                     </div>
                         </div>
                     <div class="col-md-6">
                 <div class="form-group">
                     <label for="Expense" style="visibility:hidden">Expense Type</label>
                     <div class="input-group">
                        <span class="input-group-addon">
                          <asp:RadioButton ID="rdoOperationalExpense" AutoPostBack="true" runat="server" OnCheckedChanged="rdoOperationalExpense_CheckedChanged" GroupName="Expense"  ></asp:RadioButton>
                        </span>
                     <asp:TextBox ID="txtOperationalExpense" disabled="disabled" runat="server" class="form-control" Text="Operational Expense"></asp:TextBox>
                  </div>
                  </div>  
                         </div>
                     </div>

                <div class="form-group">
					<label for="estimatedCost">Estimated Cost</label><label id="Label3" style="color:red;"></label>
				   <asp:TextBox ID="txtEstimatedCost"  runat="server"  CssClass="form-control" value="0"></asp:TextBox>
				</div>

                <div class="row" id="divRadioBudget" runat="server" visible="false" >
				<div class="col-md-6">
				 <div class="form-group">
				<label for="budget">Budget</label>
				<div class="input-group">
						<span class="input-group-addon">
						<asp:RadioButton ID="rdoBudgetEnable" runat="server"   AutoPostBack="true" OnCheckedChanged="rdoBudgetEnable_CheckedChanged" GroupName="Budget"  Checked>
						</asp:RadioButton>
						</span>
						<asp:TextBox ID="txtBudgetYes" disabled="disabled" runat="server"  class="form-control" Text="Yes"></asp:TextBox>
				  </div>
				  </div>
				  </div>
				<div class="col-md-6">
				 <div class="form-group">
				  <label for="budget" style="visibility:hidden">Budget</label>
					 <div class="input-group">
						<span class="input-group-addon">
						  <asp:RadioButton ID="rdoBudgetDisable"  runat="server" AutoPostBack="true"  OnCheckedChanged="rdoBudgetDisable_CheckedChanged" GroupName="Budget"  ></asp:RadioButton>
						</span>
					 <asp:TextBox ID="txtBudgetNo" runat="server" class="form-control" Text="No"></asp:TextBox>
				  </div>
				  </div>
				</div>
			   </div>

                <div id="divBudget" runat="server" visible="false">
				<div class="form-group" id='divBudgetRemark' runat="server" visible="false">
					<label for="budgetRemark">Remark</label><label id="Label4" style="color:red;"></label>
				   <asp:TextBox ID="txtBudgetRemark" TextMode="MultiLine" runat="server" value="0" CssClass="form-control"></asp:TextBox>
				</div>

			<div class="form-group" id="divBudgetAmount" runat="server" visible="false" >
					<label for="budgetAmount">Budget Amount</label><label id="Label5" style="color:red;"></label>
				   <asp:TextBox ID="txtBudgetAmount"  runat="server"  type="number" CssClass="form-control"></asp:TextBox>
				</div>

				<div class="form-group" id="divBudgetInformation" runat="server" visible="false">
					<label for="budgetInformation">Budget Information</label><label id="lblBudgetInformation" style="color:red;" ></label>
				   <asp:TextBox ID="txtBudgetInformation"  runat="server"  CssClass="form-control"></asp:TextBox>
				</div>
				</div>

                <div class="form-group">
					<label for="exampleInputEmail1">MRN Type</label>
					<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlPrType" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
					<asp:DropDownList ID="ddlPrType" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlPrType_SelectedIndexChanged" Enabled="true">
					</asp:DropDownList>
				</div>
				 <div class="form-group">
					<label for="exampleInputEmail1">Purchase Type</label>
					<asp:DropDownList ID="ddlPtType" runat="server" CssClass="form-control" >
					</asp:DropDownList>
				</div>

				<div class="row">
				<div class="col-md-6">
				 <div class="form-group">
				<label for="procedure">Procedure</label>
				<div class="input-group">
						<span class="input-group-addon">
						<asp:RadioButton ID="rdoNormalProcedure" runat="server"  AutoPostBack="true" GroupName="Procedure"  ></asp:RadioButton>
						</span>
					   <asp:TextBox ID="txtProcedureEnable" Enabled="false"  runat="server"    CssClass="form-control"  Text="Normal"></asp:TextBox>
				  </div>
				  </div>
				  </div>
				<div class="col-md-6">
				 <div class="form-group">
				  <label for="procedure" style="visibility:hidden">Procedure</label>
					 <div class="input-group">
						<span class="input-group-addon">
						  <asp:RadioButton ID="rdoCoveringProcedure"   AutoPostBack="true" runat="server" GroupName="Procedure"  ></asp:RadioButton>
						</span>
					<asp:TextBox ID="txtProcedureDisable" Enabled="false" runat="server"   CssClass="form-control"   Text="Covering"></asp:TextBox>
				  </div>
				  </div>
				</div>
			   </div>
				
                
						  <div class="form-group"  runat="server" visible="false">
					<label for="description">Description (Not Compulsory Feild)</label>
					<asp:TextBox ID="txtMrnDescription" TextMode="MultiLine" Width="50%" runat="server"  CssClass="form-control"  placeholder="Description" ></asp:TextBox>
				</div>
			</div>
			<!-- /.col -->
		  </div>
		  <!-- /.row -->
		</div>
		</div>

		




				  <div class="box box-primary">
				  <div class="box-header with-border">
		  <h3 class="box-title" style=" font-weight: bold; color: #286d9e; ">Item Details</h3>
                       <asp:Button ID="btnNewItem" runat="server" Text="Add New Item" 
				  CssClass="btn btn-warning pull-right" onclick="btnNewItem_Click" OnClientClick="scrollFunction(1)"  style="margin-right: 10px;"/>
		  </div>
		  <div class="box-tools pull-right">
		  <%--  <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>--%>
			<button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
		  </div>
		  <div class="box-body">
		 <div class="row">
		 <div class="col-md-12">
							<asp:GridView ID="gvDataTable" runat="server" AutoGenerateColumns="false" EmptyDataText="No Items Found" CssClass="table table-responsive table-stripeds"
								GridLines="None">
								<Columns>
									<asp:TemplateField HeaderText ="MrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
									<ItemTemplate>
									<asp:Label ID="lblMrnId"  CssClass="MrnId" runat="server" Text='<%#Eval("MrnId")%>'></asp:Label>
									</ItemTemplate>
									</asp:TemplateField>
									<asp:BoundField DataField="MrnId" HeaderText="MrnId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
									<asp:BoundField DataField="MainCategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
									<asp:BoundField DataField="MainCategoryName" HeaderText="Category Name" />
									<asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
									<asp:BoundField DataField="SubcategoryName" HeaderText="Sub Category Name" />
									<asp:TemplateField HeaderText ="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
									<ItemTemplate >
									<asp:Label ID="lblItemId"  CssClass="itemid" runat="server" Text='<%#Eval("ItemId")%>'></asp:Label>
									</ItemTemplate>
									</asp:TemplateField>
									<asp:BoundField DataField="ItemId" HeaderText="ItemId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
									<asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                    <asp:BoundField DataField="Description" HeaderText="Description" />
									<asp:BoundField DataField="RequestedQty" HeaderText="Quantity" />
						            <asp:BoundField DataField="EstimatedAmount" HeaderText="Estimated Amount" />
									<asp:BoundField DataField="Replacement" HeaderText="Replacement" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
								   <asp:TemplateField HeaderText ="Replacement">
								   <ItemTemplate>
								   <asp:Label ID="Label1" runat="server" Text='<%#Eval("Replacement").ToString()=="1"?"Yes":"No"%>'></asp:Label>
								   </ItemTemplate>
								   </asp:TemplateField>
									<asp:BoundField DataField="Purpose" HeaderText="Remarks"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
									 <asp:TemplateField HeaderText="Edit">
										<ItemTemplate>
										 <asp:LinkButton ID="lnkSelectEdit"   Text="Edit"  OnClick="btnEditItems_Click" runat="server"  OnClientClick = "scrollFunction(0); return GetSelectedRow(this);"/>      
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField HeaderText="Delete">
										<ItemTemplate>
											<asp:ImageButton CssClass="btnCancelRequest" ID="btnCancelRequest" OnClick="btnDelete_Click"  OnClientClick="return scrollToTop()" ImageUrl="~/images/delete.png" runat="server" style="width:25px;"/>
										</ItemTemplate>
									</asp:TemplateField>
								</Columns>
							</asp:GridView>
						</div>
		</div>
		 <div >
		<div class="form-group">
		</div>
		</div>
		</div>
		</div>
          
		<!-- /.box-header -->
		
		</div>
	</section>
                <section class="content" style="padding-top: 0px;"> 
	 <div class="box box-danger" id="divScrollItem">
		<div class="box-header with-border">
		  <h3 class="box-title" id="HeadName" style=" font-weight: bold; color: #286d9e; " >Edit Items</h3> &nbsp;&nbsp;&nbsp;
           
		  <asp:Label ID="lblMessageAddItems" runat="server" Text="" style="color:Red; font-weight:bold;"></asp:Label>
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
					<label for="exampleInputEmail1">Sub Category</label>
					  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlSubCategory" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
					<asp:DropDownList ID="ddlSubCategory" runat="server" CssClass="form-control"  AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged" >
					</asp:DropDownList>
				</div>
				<div class="form-group">
					<label for="exampleInputEmail1">Item Name</label>
					  <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlItemName" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
					   <%--<asp:TextBox ID="ddlItemName" CssClass="form-control input-md" AutoComplete="off" runat="server" onkeyup="SearchItemName()"  OnTextChanged="txtddlItemNameOnTextChanged" AutoPostBack="true"/>--%>
					<asp:DropDownList ID="ddlItemName" runat="server" CssClass="form-control"  onselectedindexchanged="ddlItemName_SelectedIndexChanged" ></asp:DropDownList>
				</div>
				<div class="form-group">
					
				<div style="display:inline-block;width:60%;" >
					<label for="exampleInputEmail1">Item Quantity</label>
					<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtQty" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
					<asp:TextBox ID="txtQty" runat="server" CssClass="form-control" placeholder="" type="number"  min="0"></asp:TextBox>
					</div>
					<div  style="display:inline-block;width:35%;"  >
						<label for="exampleInputEmail1"></label>
						<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" Font-Bold="true"  ControlToValidate="ddlMeasurement" InitialValue="0" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
						<asp:DropDownList ID="ddlMeasurement" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
					</div>
				</div>
				<div class="form-group">
					<label for="exampleInputEmail1">Item Description</label>
					 <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
				</div>
				<div class="form-group">
					<label for="exampleInputEmail1">Remarks</label>
					<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtPurpose" ValidationGroup="btnAdd">*</asp:RequiredFieldValidator>     
					 <asp:TextBox ID="txtPurpose" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
				</div>
			  <!-- /.form-group -->
			</div>

			  <div class="col-md-6">
				<div class="form-group">
				 <label for="exampleInputEmail1">Estimated Amount</label>
				 <asp:TextBox ID="txtEstimatedAmount" type="number"  runat="server" CssClass="form-control" placeholder="" min="0" value="0"></asp:TextBox>
			   </div>

			   <div class="row">
				<div class="col-md-6">
				 <div class="form-group">
				<label for="fileSampleProvided">File/Sample Provided</label>
				<div class="input-group">
						<span class="input-group-addon">
						<asp:RadioButton ID="rdoFileSampleEnable" runat="server" GroupName="FileSample"></asp:RadioButton>
						</span>
						<asp:TextBox ID="txtFileSampleEnable" disabled="disabled" runat="server" class="form-control" Text="Yes"></asp:TextBox>
				  </div>
				  </div>
				  </div>
				<div class="col-md-6">
				 <div class="form-group">
				<label for="fileSampleProvided" style="visibility:hidden">File/Sample Provided</label>
					 <div class="input-group">
						<span class="input-group-addon">
						  <asp:RadioButton ID="rdoFileSampleDisable" runat="server" GroupName="FileSample" Checked></asp:RadioButton>
						</span>
					 <asp:TextBox ID="txtFileSampleDisable" disabled="disabled" runat="server" class="form-control" Text="No"></asp:TextBox>
				  </div>
				  </div>
				</div>
			   </div>

			  <div class="form-group">
					<label for="exampleInputEmail1">Item Specifications</label>&nbsp;<asp:Label ID="lblcount" runat="server" Text="0" style="color:red; font-weight:bold;  border:solid 1px;  border-color:blue; border-radius:5px;"></asp:Label></div>
				<div class="form-group">
					<asp:Button ID="btnexisting" CssClass="btn btn-group-justified" runat="server" OnClick="btnexisting_Click" Text="Item Specification"></asp:Button>
				</div>
			  <div class="row">
				<div class="col-md-6">
				 <div class="form-group">
				<label for="exampleInputEmail1">Replacement</label>
				<div class="input-group">
						<span class="input-group-addon">
						<asp:RadioButton ID="rdoEnable" runat="server" GroupName="RegularMenu"></asp:RadioButton>
						</span>
						<asp:TextBox ID="txtEnable" disabled="disabled" runat="server" class="form-control" Text="Yes"></asp:TextBox>
                 
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
					 <asp:TextBox ID="txtDisable" runat="server" disabled="disabled" class="form-control" Text="No"></asp:TextBox>
				  </div>
				  </div>
				</div>

			   </div>
				<div class="form-group" id="divReplacementImage">
					<label for="exampleInputEmail1">Upload Replacement Images (jpg,jpeg,png,gif,pdf)</label>&nbsp;&nbsp;
					<asp:Label ID="lblReplaceimageDelete" runat="server" Text="" style = "font-weight: bold;"></asp:Label>
					<input type="file" class="form-control" id="fileReplace" name="fileReplace[]" multiple accept="image/*"/>
					<div class="col-sm-12"  style="overflow:auto;width:100%; max-height:400px;" >
						<div id="fileReplacePip"></div>
					  <asp:GridView runat="server" ID="gvRepacementImages" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive" >
						  <Columns>
							  <asp:BoundField DataField="MrnId"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
							  <asp:BoundField DataField="ItemId"  ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden"/>
							  <asp:BoundField DataField="FilePath" HeaderText="FilePath" ItemStyle-CssClass="hidden"   HeaderStyle-CssClass="hidden"/>
							  <asp:BoundField DataField="FileName" HeaderText="FileName"  />
							  
							   <asp:TemplateField HeaderText="Image" HeaderStyle-Width="40px">
								<ItemTemplate>
									<asp:Image ID="imgPicture" runat="server" ImageUrl='<%# Eval("FilePath") %>' style="width:100px"/>
								</ItemTemplate>
							  </asp:TemplateField>
							  <asp:TemplateField HeaderText="Large Preview" >
								  <ItemTemplate>
									  <asp:LinkButton runat="server" ID="lbtnViewReplacementImage" OnClick="lbtnViewReplacementImage_Click">View</asp:LinkButton>
								  </ItemTemplate>
							  </asp:TemplateField>

							  <asp:TemplateField HeaderText="Delete"  HeaderStyle-Width="40px">
								  <ItemTemplate>
									  <asp:LinkButton runat="server" ID="lbtnDeleteReplacementImage" OnClick="lbtnDeleteReplacementImage_Click">Delete</asp:LinkButton>
								  </ItemTemplate>
							  </asp:TemplateField>
						  </Columns>
					  </asp:GridView>
				  </div>
				</div>
				<div class="form-group">
					
					<label for="exampleInputEmail1">Upload Standard Images (jpg,jpeg,png,gif,pdf)</label>&nbsp;&nbsp;
					<asp:Label ID="lblImageDeletedMsg" runat="server" Text="" style = "font-weight: bold;"></asp:Label>
					<input type="file" class="form-control"  id="files" name="files[]" multiple accept="image/*"/>
					<div class="col-sm-12"  style="overflow:auto;width:100%; max-height:400px;" >
						<div id="filesPip"></div>
					  <asp:GridView runat="server" ID="gvPrUploadedFiles" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive">
						  <Columns>
							  <asp:BoundField DataField="MrnId"  ItemStyle-CssClass="hidden MrnId" HeaderStyle-CssClass="hidden MrnId"/>
							  <asp:BoundField DataField="ItemId"  ItemStyle-CssClass="hidden ItemId"   HeaderStyle-CssClass="hidden ItemId"/>
							  <asp:BoundField DataField="FilePath" HeaderText="FilePath" ItemStyle-CssClass="hidden FilePath"   HeaderStyle-CssClass="hidden"/>
							  <asp:BoundField DataField="FileName" HeaderText="FileName"/>
							  
							  <asp:TemplateField HeaderText="Image">
								<ItemTemplate>
									<asp:Image ID="imgPicture" runat="server" ImageUrl='<%# Eval("FilePath") %>' style="width:100px"/>
								</ItemTemplate>
							  </asp:TemplateField>
							  <asp:TemplateField HeaderText="Large Preview">
								  <ItemTemplate>
									  <asp:LinkButton runat="server" ID="lbtnViewUploadImage" OnClick="lbtnViewUploadImage_Click">View</asp:LinkButton>
								  </ItemTemplate>
							  </asp:TemplateField>

							  <asp:TemplateField HeaderText="Delete">
								  <ItemTemplate>
									  <asp:LinkButton runat="server" ID="lbtnDeleteUploadImage" OnClientClick="showWarning(this)" CssClass="lbtnDeleteUploadImagecls"  >Delete</asp:LinkButton>                                     
								  </ItemTemplate>
							  </asp:TemplateField>
						  </Columns>
					  </asp:GridView>
						</div>
				  </div>



				  <div class="form-group">
					<label for="exampleInputEmail1">Upload Supportive Documents</label>&nbsp;&nbsp;
				  <asp:Label ID="lblSupporiveDelete" runat="server" Text="" style = "font-weight: bold;"></asp:Label>
					<input type="file" class="form-control"  id="supportivefiles" name="supportivefiles[]"  multiple onchange="readFilesURL(this);" />
					   <div class="form-group"  style="overflow-y:auto; overflow-x:hidden; max-height:300px;">

				  <table id="tblUpload" class="table table-hover" style="border:none;"  border="1">
				 </table>

			  </div> 
					 <div class="col-sm-12"  style="overflow:auto;width:100%; max-height:400px;" >
					   <asp:GridView runat="server" ID="gvSupporiveFiles" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive">
						  <Columns>
							  <asp:BoundField DataField="MrnId"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
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
									  <asp:LinkButton runat="server" ID="lbtnDeleteSupportiveDocument" OnClientClick="showDeleteModal()"   OnClick="lbtnDeleteSupportiveDocument_Click">Delete</asp:LinkButton>
								  </ItemTemplate>
							  </asp:TemplateField>
						  </Columns>
					  </asp:GridView>
			 </div>
				</div>




			  <!-- /.form-group -->
			</div>
		  </div>

		  <div class="row" runat="server" visible="false"> 
		  <div class="col-md-12">
				<h3 class="box-title"> Purchase History</h3>
			   <hr />
			   </div>
			   <div class="col-md-6">

					<div class="form-group">
					<asp:HiddenField ID="hndSupplierId" runat="server"  value="0"></asp:HiddenField>
					<label for="lastPurchaseSupplier">Last Purchased Supplier</label><label id="lblLastPurchaseSupplier" style="color:red;"></label>
					<asp:TextBox ID="txtLastPurchaseSupplier"  runat="server"  CssClass="form-control" disabled></asp:TextBox>
					</div>

					<div class="form-group">
					<label for="lastPurchasePrice">Price</label><label id="lblLastPurchasePrice" style="color:red;"></label>
				   <asp:TextBox ID="txtLastPurchasePrice"  runat="server"  Text="0" CssClass="form-control" disabled></asp:TextBox>
					</div>
				</div>
				  <div class="col-md-6">

				  <div class="form-group">
				  <label for="lastPurchaseDate">Last Purchased Date</label><label id="lblLastPurchaseDate" style="color:red;"></label>
						<asp:TextBox ID="txtLastPurchaseDate"  runat="server" AutoPostBack="true" CssClass="form-control" disabled></asp:TextBox>
				  </div>

				  <div class="form-group">
				  <label for="PurchaseOrderNo">Purchased Order No</label><label id="lblLastPurchaseOrderNo" style="color:red;"></label>
						<asp:TextBox ID="txtPurchaseOrderNo"  runat="server"  CssClass="form-control" disabled></asp:TextBox>
				  </div>

				  </div>
			   </div>   
			  
			  <div class="row" runat="server" visible="false"> 
			  <div class="col-md-12">
				<h3 class="box-title"> Inventory</h3>
				<hr />
				</div>
			   <hr />
			   <div class="col-md-6">
				<div class="form-group">
					<label for="stockBalance">Stock Balance</label><label id="lblStockBalance" style="color:red;"></label>
				   <asp:TextBox ID="txtStockBalance" Text="0" runat="server"  CssClass="form-control"></asp:TextBox>
				</div>
				</div>
				<div class="col-md-6">
				  <div class="form-group">
					<label for="avgConsumption">AVG Consumption</label><label id="lblAvgConsumption" style="color:red;"></label>
				   <asp:TextBox ID="txtAvgConsumption" Text="0"  runat="server"  CssClass="form-control"></asp:TextBox>
				</div>
				<div class="form-group" style="display:none">
					<label for="nonStock">Non Stock</label><label id="lblNonStock" style="color:red;"></label>
				   <asp:TextBox ID="txtNonStock"  runat="server"  CssClass="form-control"></asp:TextBox>
				</div>
				</div>
				</div>

		</div>

		<asp:HiddenField ID="hdnField"  runat="server" />
		  <asp:HiddenField ID="hdnMrnid"  runat="server" />
			<asp:HiddenField ID="hdnitemId"  runat="server" />
		   <asp:HiddenField ID="HiddenField1"  runat="server" />
		   <asp:HiddenField ID="hdnItemCount"  runat="server" />

		<!-- /.box-body -->
		<div class="box-footer">
		<div class="form-group">
			  <label for="exampleInputEmail1" style="visibility:hidden">Category Name</label>
				<asp:Button ID="btnClear" ValidationGroup="btnAdd" runat="server" Text="Clear" 
				  CssClass="btn btn-danger pull-right" onclick="btnClear_Click" OnClientClick="return ClearBom()"/>
			  <asp:Button ID="btnAdd" ValidationGroup="btnAdd" runat="server" Text="Add Item" 
				  CssClass="btn btn-primary pull-right" onclick="btnAdd_Click"  OnClientClick="return BindToList()" style="margin-right: 10px;"/>
				  
		  </div>
		</div>
		<br />
		<div class="box-footer">
		<div class="form-group">
			 <label for="exampleInputEmail1" style="visibility:hidden">Category Name</label>
			 <asp:Button ID="btnUpdateData"  runat="server" Text="Update and Proceed MRN" 
				  CssClass="btn btn-success pull-right" onclick="btnUpdateData_Click"  />
		</div>
		</div>
	  </div>
	  </section>
                <div id="specification" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="false">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="False">×</span></button>
                                <h4 class="modal-title">View Specification</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="color: Black;">Meterial</label>

                                                <asp:TextBox ID="txtmeterialNew" runat="server" type="text" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="color: Black;">Description</label>

                                                <asp:TextBox ID="txtdescriptionNew" runat="server" type="text" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-4" style="margin-left: -120px; margin-top: 30px;">
                                            <div class="form-group">
                                                <label for="exampleInputEmail1" style="visibility: hidden;">Select Main Category</label>
                                                <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">
						                                <img src="images/PlusSign.gif" border="0" style="margin-right:4px;vertical-align: middle;" alt="" />Add Row</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">

                                        <asp:GridView ID="gvSpecificationBoms" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Specifications Found" Style="border-collapse: collapse; color: black;"
                                            GridLines="None" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="itemId" HeaderText="Item Id" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <%--	<asp:BoundField DataField="SeqId" HeaderText="Seq_ID" />--%>
                                                <asp:BoundField DataField="Meterial" HeaderText="Material" />
                                                <asp:BoundField DataField="Description" HeaderText="Description" />
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:ImageButton CssClass="btnCancelRequest" ID="btnDeleteRow" OnClick="btnDeleteRow_Click" ImageUrl="~/images/delete.png" runat="server" Style="width: 25px;" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                    <div>
                                        <label id="Label2" style="margin: 3px; color: maroon; text-align: center;"></label>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-danger pull-right" data-dismiss="modal" data-backdrop="false">Close</button>
                                <%-- <asp:Button ID="ButtonClose" class="btn btn-danger pull-right" runat="server" Text="Close" onclick="ButtonClose_Click"/>--%>
                            </div>

                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hndImageMrnid" runat="server" />
                <asp:HiddenField ID="hndImageItemId" runat="server" />
                <asp:HiddenField ID="hdnImageFilePath" runat="server" />
                <asp:Button ID="btnDeleteUploadImagehnd" runat="server" OnClick="lbtnDeleteUploadImage_Click" CssClass="hidden" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
        </asp:UpdatePanel>
    </form>
    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(function () {
            $('#<%=txtQty.ClientID%>').keypress(function (e) {
                if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42) {
                } else {
                    return false;
                }
            });
        });

        $(function () {
            $('#<%=txtEstimatedAmount.ClientID%>').keypress(function (e) {
                if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42) {
                } else {
                    return false;
                }
            });
        });

        $("#ContentSection_btnUpdateData").click(function () {

            if ($("#<%=txtQuotationFor.ClientID%>").val() != "" && $("#<%=txtRequiredDate.ClientID%>").val() != "" && $("#<%=ddlMainCateGory.ClientID%>").val() != "") {
		        return true;
		    }
		    else {
		        $("#lblQuotationFor").text('');
		        $("#lblRequiredDate").text('');

		        if ($("#<%=txtQuotationFor.ClientID%>").val() == "") {
                    $("#lblQuotationFor").text(' * Please Fill this Field');
                }
                if ($("#<%=txtRequiredDate.ClientID%>").val() == "") {
		            $("#lblRequiredDate").text(' * Please Fill this Field');
		        }

		        document.getElementById('errorMessage').innerHTML = "Please Fill out the required fields marked with * mark";
		        $('#errorAlert').modal('show');

		        document.body.scrollTop = 100;
		        document.documentElement.scrollTop = 100;
		        return false;
		    }

		});

        function scrollFunction(value) {
            document.getElementById('divScrollItem').scrollIntoView();
            if (value == 1) {
                $("#HeadName").text("Add Item")
            }
        }

        function scrollToTop() {
            document.getElementById('ContentSection_Updatepanel1').scrollIntoView();
            return true;
        }

        Sys.Application.add_load(function () {
            //onload set date value
            var this1 = $("#ContentSection_txtRequiredDate");
            if (this1.val() != "") {
                $("#ContentSection_txtRequiredDate").attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
            }
        });
    </script>

    <script type="text/javascript">

        $("[id *= ContentSection_ddlItemName]").change(function () {
            alert($(this).val());
        });

        $("#btnNoConfirmYesNo").on('click').click(function () {
            //  debugger;
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        });

    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            function GetAnimation() {
                $('html,body').animate({
                    scrollTop: $("#divScrollItem").offset().top
                }, 'slow');
            }

            //onload set date value
            var this1 = $("#ContentSection_txtRequiredDate");
            if (this1.val() != "") {
                $("#ContentSection_txtRequiredDate").attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
            }
        });
    </script>


    <script type="text/javascript">
        function GetSelectedRow(lnk) {

            document.getElementById("tbodyCol").innerHTML = null;
            $("#itemCount").text("0");
            $("#ContentSection_HiddenField1").val("1");
            $("#ContentSection_hdnItemCount").val("0");


            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var MrnId = row.cells[1].innerText;
            var itemid = row.cells[7].innerText;
            var ratus = MrnId + "-" + itemid;
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
                    $("#ContentSection_hdnItemCount").val(msg.d.length);

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
        //---------------All Item Allocated For Supplier Accroding to selected Department Type
        var DataList = <%= getJsonBomStringList() %>
        LoadIntialBom();
        function LoadIntialBom() {
            // debugger;
            var text = "";
            var field = "";
            for (var i = 1; i <= DataList.length; i++) {
                field = DataList[i - 1].split('-');
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

        function ClearBom() {
            document.getElementById('fileReplace').value = "";
            document.getElementById('files').value = "";
            document.getElementById('supportivefiles').value = "";
            $("#tbodyCol tr").remove();
            return true;
        }

        function ScrollPageToUp() {
            $("#msg").scrollTop();
            return true;
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

        $(".customDate").on("change", function () {
            if (this.value) {
                $(this).attr('data-date', moment(this.value, 'YYYY-MM-DD').format($(this).attr('data-date-format')));
            } else {
                $(this).attr('data-date', '');
            }
        }).trigger("change")

        function showWarning(obj) {
           // debugger;
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            $('#ContentSection_hndImageMrnid').val($(obj).closest('tr').find("td.MrnId").text());
            $('#ContentSection_hndImageItemId').val($(obj).closest('tr').find("td.ItemId").text());
            $('#ContentSection_hdnImageFilePath').val($(obj).closest('tr').find("td.FilePath").text());
            event.preventDefault();

            return this.false;
        }

      

        function showDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            event.preventDefault();
            return this.false;
        }

        function btnDelete() {
            $('#ContentSection_btnDeleteUploadImagehnd').click();
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }

        function hideDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }
    </script>

</asp:Content>
