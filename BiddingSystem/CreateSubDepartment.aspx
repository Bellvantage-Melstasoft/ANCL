<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CreateSubDepartment.aspx.cs" Inherits="BiddingSystem.CreateSubDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style>
        #snackbar, #snackbarMobileNumber, #snackbarFileUpload {
            visibility: hidden;
            min-width: 250px;
            margin-left: -125px;
            background-color: #333;
            color: #fff;
            text-align: center;
            border-radius: 2px;
            padding: 16px;
            position: fixed;
            z-index: 1;
            left: 50%;
            bottom: 30px;
            font-size: 17px;
        }

            #snackbar.show, #snackbarMobileNumber.show, #snackbarFileUpload.show {
                visibility: visible;
                -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
                animation: fadein 0.5s, fadeout 0.5s 2.5s;
            }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        @-webkit-keyframes fadein {
            from {
                bottom: 0;
                opacity: 0;
            }

            to {
                bottom: 30px;
                opacity: 1;
            }
        }

        @keyframes fadein {
            from {
                bottom: 0;
                opacity: 0;
            }

            to {
                bottom: 30px;
                opacity: 1;
            }
        }

        @-webkit-keyframes fadeout {
            from {
                bottom: 30px;
                opacity: 1;
            }

            to {
                bottom: 0;
                opacity: 0;
            }
        }

        @keyframes fadeout {
            from {
                bottom: 30px;
                opacity: 1;
            }

            to {
                bottom: 0;
                opacity: 0;
            }
        }
    </style>
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

        .successMessage {
            color: white;
            font-size: medium;
        }

        .failMessage {
            color: white;
            font-size: medium;
        }

        .select2-container--default .select2-selection--multiple .select2-selection__rendered .select2-selection__choice {
            color: black;
        }

        .activePhase {
            text-align: center;
            border-radius: 3px;
        }
    </style>

    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
  
    <section class="content-header">
      <h1>
       Create Department
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Create Department</li>
      </ol>
    </section>
    <br />




    <form id="form1" runat="server">
        <asp:HiddenField ID="hdnImgPathEdit" runat="server" />
        <asp:HiddenField ID="hdnTGermsPathEdit" runat="server" />

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

                <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                    <strong>
                        <asp:Label ID="lbMessage" runat="server"></asp:Label>
                    </strong>
                </div>

                <div id="modalDeleteYesNo" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 id="lblTitleDeleteYesNo" class="modal-title">Confirmation</h4>
                            </div>
                            <div class="modal-body">
                                <p>Are you sure you want to delete this record? Or? </p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" OnClick="lnkBtnDelete_Click" OnClientClick="return hideDeleteModal();" Text="Yes"></asp:Button>
                                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">No</button>
                            </div>
                        </div>
                    </div>
                </div>
                <section class="content" style="padding-top: 0px">

        


        

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
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
                <label for="exampleInputEmail1">Department Name</label>  
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDepartmentName"  ValidationGroup="btnSave" ID="RequiredFieldValidator1" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtDepartmentName" CssClass="form-control " autocomplete="off" ></asp:TextBox>
                </div>
                      <div class="form-group">
                <label for="exampleInputEmail1">Phone No</label>  
                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtPhoneNo" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNo"  ValidationGroup="btnSave" ErrorMessage="Phone no should be in 10 Digits" ForeColor="Red" ValidationExpression="[0-9]{10}" ></asp:RegularExpressionValidator>
                    <asp:TextBox runat="server" ID="txtPhoneNo" CssClass="form-control"  type="number" autocomplete="off" min="0" ></asp:TextBox>
                </div>
                </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="exampleInputEmail1">Head of Department</label>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="ddlHeadOfDepartment" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                     <asp:ListBox ID="ddlHeadOfDepartment" SelectionMode="Multiple" runat="server" CssClass="select2 form-control"   style="color:black; width:100%">
                    </asp:ListBox>
                   <%-- <asp:DropDownList ID="ddlHeadOfDepartment" runat="server" CssClass="form-control">
                    </asp:DropDownList>--%>
                </div
                <div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
                <asp:CheckBox ID="chkIsavtive"  runat="server"  CssClass="form-control" Checked></asp:CheckBox>
                </div>
                </div>
            </div>          
        </div>
       
        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="btn btn-primary" ValidationGroup="btnSave" onclick="btnSave_Click" ></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"  CssClass="btn btn-danger" OnClick="btnClear_Click" ></asp:Button>
                </span>
              </div>
        </div>
        <!-- /.box-body -->
     
      <!-- /.box -->
    </section>

                <div class="panel-body">
                    <div class="co-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvDepartments" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvDepartments_PageIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDepartmentId" Text='<%#Eval("SubDepartmentID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="SubDepartmentID" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="SubDepartmentName" HeaderText="Department Name" />
                                    <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                                    <asp:BoundField DataField="HeadOfDepartmentName" HeaderText="Head of Department" />
                                    <asp:TemplateField HeaderText="Active">
                                        <ItemTemplate>
                                            <asp:Label CssClass="activePhase" runat="server" ID="lblIsActive" Text='<%#Eval("IsActive").ToString()=="1"?"Yes":"No"%>'
                                                Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" OnClick="lnkBtnEdit_Click"
                                                Style="width: 26px; height: 20px" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDeleteCompany" ImageUrl='<%#Eval("IsActive").ToString()== "1"?"~/images/delete.png":"~/images/dlt.png" %>' Enabled='<%#Eval("IsActive").ToString()== "1"?true:false %>' ToolTip='<%#Eval("IsActive").ToString()== "1"?"Delete":"Deleted" %>'  CssClass="deleteCompany" Style="width: 26px; height: 20px;" runat="server" />
                                            <%-- <asp:ImageButton ID="btnDeleteCompany" ImageUrl="~/images/delete.png"  class="deleteCompany" Style="width: 26px; height: 20px;" runat="server"/>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>


                <asp:Button ID="btnSubDepartmentDelete" runat="server" OnClick="lnkBtnDelete_Click" CssClass="hidden" />
                <asp:HiddenField ID="hdnSubDepartmentId" runat="server" />
                <asp:HiddenField ID="hdnShowSuccess" runat="server" Value="0"></asp:HiddenField>

            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID ="btnSave" />
                </Triggers>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnDepartmentId" runat="server" />
        <asp:HiddenField ID="hdnTermsConditionUrl" runat="server" />
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <div id="snackbar">
            Phone number should has 10 Digits
        </div>
        <div id="snackbarMobileNumber">
            Mobile number should has 10 Digits
        </div>
        <div id="snackbarFileUpload">
            Please upload file having extensions .jpeg/.jpg/.png/.gif only.
        </div>
    </form>
    <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>
    <script type="text/javascript">

        $(function(){
            $(':input[type=number]').on('mousewheel',function(e){ $(this).blur(); });
        });

        $(function () {
            $('#<%=txtPhoneNo.ClientID%>').keypress(function (e) {
                    if (e.which != 69 && e.which != 101 && e.which != 45 && e.which != 43 && e.which != 42 )  {
                        if(<%=txtPhoneNo.ClientID%>.value.length<10)
                    { }
                    else {
                        return false;
                    }
                } else {
                    return false;
                }
                });
            });

        var countries = <%= getJsonDepartmentList() %>;
        autocomplete(document.getElementById("ContentSection_txtDepartmentName"), countries);

    </script>
     <%--<script type="text/javascript" src="AdminResources/js/jquery1.8.min.js"></script>--%>

     <script src="AdminResources/js/select2.full.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />
    <script src="AdminResources/js/daterangepicker.js" type="text/javascript"></script>
    <script type="text/javascript">

        
        Sys.Application.add_load(function () {
            $(function () {
                $('.select2').select2();
            })


            $(document).ready(function () {

               
                if ($('#ContentSection_hdnShowSuccess').val() == "1") {
                    //debugger;
                    swal({
                        type: 'success', title: 'SUCCESS', text: 'Your work has been saved', showConfirmButton: false, timer: 1500
                    }).then(
                        (result) => {
                            $('#ContentSection_hdnShowSuccess').val('0');
                        });
                }





            $('.deleteCompany').on({
                click: function () {

                    event.preventDefault();
                    var tableRow = $(this).closest('tr:not(:has(>td>table))')[0].cells;
                    $('#ContentSection_hdnSubDepartmentId').val($(tableRow).eq(1).text());
                        
                    swal.fire({
                        title: 'Are you sure?',
                        text: 'You will be able to revert this',
                        type: 'warning',
                        cancelButtonColor: '#d33',
                        showCancelButton: true,
                        showConfirmButton: true,
                        confirmButtonText: 'Yes, Delete it!',
                        cancelButtonText: 'No',
                        allowOutsideClick: false,
                       
                    }

                      ).then((result) => {
                          if (result.value) {
                              $('#ContentSection_btnSubDepartmentDelete').click();
                          }
                      });
                        
                }
            });

            });

        });
            

       
        function hideModal()
        {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function hideDeleteModal()
        {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function showDeleteModal()
        {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            return this.false;
         
        }
    </script>
</asp:Content>
