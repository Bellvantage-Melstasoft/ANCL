<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CreateRole.aspx.cs" Inherits="BiddingSystem.CreateRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">


    <html>
    <head>
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

            .activePhase {
                text-align: center;
                border-radius: 3px;
            }
        </style>
        <style>
            /** {
  box-sizing: border-box;
}
body {
  font: 16px Arial;  
}

input {
  border: 1px solid transparent;
  background-color: #f1f1f1;
  padding: 10px;
  font-size: 16px;
}
input[type=text] {
  background-color: #f1f1f1;
  width: 100%;
}*/
        </style>
    </head>

    <body>
        <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
        <script src="AdminResources/js/jquery1.8.min.js"></script>
        <script src="AdminResources/js/autoCompleter.js"></script>
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
        <section class="content-header">
      <h1>
       Create User Role
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Create User Role</li>
      </ol>
    </section>
        <br />


        <form id="form1" runat="server">

            <section class="content" style="padding-top: 0px">

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
                 <%--<asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="lnkBtnDelete_Click" Text="Yes" ></asp:Button>--%>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
         <%-- <h3 class="box-title" >Create Company</h3>--%>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body ">
           <asp:ScriptManager runat="server" ID="sm">
                </asp:ScriptManager>
                 <asp:UpdatePanel ID="Updatepanel1" runat="server">
                <ContentTemplate>
          <div class="row">
            <div class="col-md-6">         
                <div class="form-group">
                <label for="exampleInputEmail1">Role Name</label>  
                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtRoleName"  ValidationGroup="btnSave" ID="RequiredFieldValidator1" ForeColor="Red">*</asp:RequiredFieldValidator>
                    <asp:TextBox runat="server" ID="txtRoleName" CssClass="form-control " autocomplete="off" ></asp:TextBox>
                </div>
                 
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
        <!-- /.box-body -->
      </div>
      <!-- /.box -->
    </section>
    <div class="panel-body">
        <div class="co-md-12">
            <div class="table-responsive">


                 <div class="box-tools pull-right">
                                            <div class="input-group input-group-sm" style="width: 150px;">
                                                <input type="text" name="table_search" class="form-control pull-right"
                                                    placeholder="Search by role" id="txtSearch">
                                                <%--<asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>--%>
                                          <%--  <asp:Button ID="btnSearch" runat="server" Text="Search" OnClientClick="Search();"></asp:Button>--%>

                                                <div class="input-group-btn">
                                                    <button type="submit" class="btn btn-default pull-right"><i
                                                            class="fa fa-search" onclick="searchRole_Click"></i></button>
                                                </div>
                                            </div>
                                        </div>


                <asp:GridView ID="gvDepartments" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" >
                    <Columns>
                         <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                            <ItemTemplate>
                                <asp:Label  runat="server" ID="lblCompanyId" Text='<%#Eval("userRoleId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="userRoleId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="userRoleName" HeaderText="Role Name" />
                        <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                        <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                        <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                        <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By" />
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
                        

                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnDeleteCompany" ImageUrl='<%#Eval("IsActive").ToString()== "1"?"~/images/delete.png":"~/images/dlt.png" %>'  ToolTip='<%#Eval("IsActive").ToString()== "1"?"Delete":"Restore" %>'   style="width:26px;height:20px;" runat="server" OnClientClick='<%#"Delete(event," +Eval("userRoleId").ToString()+", " +Eval("IsActive").ToString()+")" %>' />
                               <%-- <asp:ImageButton ID="btnDeleteCompany" ImageUrl="~/images/delete.png"  class="deleteCompany" Style="width: 26px; height: 20px;" runat="server"/>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
      <div class="box-body ">
          <div class="box box-info">
            
          <div class="col-md-6">
         <h3 class="box-title" > Assign Fuctions to  Role <h3>
      </h1>
                 <div class="form-group">
                <label for="exampleInputEmail1">Role name</label>
                 <asp:DropDownList ID="ddlRole" runat="server"  AutoPostBack="True"  CssClass="form-control" onselectedindexchanged="ddlRole_SelectedIndexChanged"></asp:DropDownList>
                </div>
               
                </div>
          <div class="panel-body">
        <div class="co-md-12">
            <div class="table-responsive">
                <asp:GridView ID="gvmaincategory" runat="server" CssClass="table table-responsive tablegv Grid" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" PageSize="20" DataKeyNames="systemDivisionId" OnRowDataBound="OnRowDataBound" >
                    <Columns>
                <asp:TemplateField>
                <ItemTemplate>
                    <img alt = "" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                        <asp:GridView ID="gvSubcatgry" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="userRoleId" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                 <asp:BoundField ItemStyle-Width="150px" DataField="functionId" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="systemDivisionId" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="functionName" HeaderText="Function Name" />
                                <asp:TemplateField HeaderText="Assigned">
                            <ItemTemplate>
                                <asp:Label CssClass="activePhase" runat="server" ID="lblIsActive1" Text='<%#Eval("IsActive").ToString()=="1"?"Yes":"No"%>'
                                    Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Assign">
                        <ItemTemplate>
                                  <asp:LinkButton ID="lnbtnAssignfunction" AutoPostBack="True" OnClick="lnbtnAssignfunction_Click" CssClass="Assignfunction"  Text='<%#Eval("IsActive").ToString()== "1"?"Unassign":"Assign" %>' runat="server" /> 
                            </ItemTemplate>
                        </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </ItemTemplate>
            </asp:TemplateField>
                         <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                            <ItemTemplate>
                                <asp:Label  runat="server" ID="lblCompanyId" Text='<%#Eval("systemDivisionId")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="userRoleId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                         <asp:BoundField DataField="systemDivisionId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="systemDivisionName" HeaderText="Main Catagory Name" />
                        <asp:TemplateField HeaderText="Is Assgined">
                            <ItemTemplate>
                                <asp:Label CssClass="activePhase" runat="server" ID="lblassigncategory" Text='<%#Eval("IsActive").ToString()=="1"?"Yes":"No"%>'
                                    Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Assign">
                             <ItemTemplate>
                                  <asp:LinkButton ID="lnbtnAssigncategory" AutoPostBack="True" OnClick="lnbtnAssigncategory_Click" CssClass="Assignmaincategory"  Text='<%#Eval("IsActive").ToString()== "1"?"Unassign":"Assign" %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
        </div>
          
        
            <br />
            <br />
            <div class="row">
            <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="GridView1" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
             <asp:TemplateField HeaderText="Active">
                         <ItemTemplate >
                             <asp:Label  runat="server"  ID="lblUserId" Text='<%#Eval("userId")%>'  ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
            <asp:BoundField DataField="userId" HeaderText="User Id" />
            <asp:BoundField DataField="userRoleID" HeaderText="CreatedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="firstName" HeaderText="User Name" />
             <asp:BoundField DataField="userRoleName" HeaderText="User Role" />
             
          
                <asp:TemplateField HeaderText="Delete">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnDeleteUserAccess" ImageUrl="~/images/delete.png" CssClass="deleteUserAccess"  style="width:26px;height:20px;" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
        </div>




      </div>
      
         </ContentTemplate>
        </asp:UpdatePanel>
         <asp:HiddenField ID="hdnRoleId" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="RoleList" runat="server" />

             <asp:Button ID="btnDeleteRole" runat="server" OnClick="btnDeleteRole_Click" CssClass="hidden" />
                <asp:Button ID="btnRestoreRole" runat="server" OnClick="btnRestoreRole_Click" CssClass="hidden" />
            <asp:Button ID="btnRoleList" runat="server" OnClick="btnbtnRoleList_Click" CssClass="hidden" />
        </form>

        <script src="AdminResources/js/jquery-1.10.2.min.js"></script>

        <script type="text/javascript">

            Sys.Application.add_load(function () {

                $(".deleteCompany").click(function () {
                    var companyId = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().children().html();
                    $("#<%=hdnRoleId.ClientID%>").val(companyId);
                    showDeleteModal();
                    event.preventDefault();
                });
            });
        </script>

        <script type="text/javascript">


            function hideModal() {
                var $confirm = $("#modalConfirmYesNo");
                $confirm.modal('hide');
                return this.false;
            }
            function hideDeleteModal() {
                var $confirm = $("#modalDeleteYesNo");
                $confirm.modal('hide');
                return this.false;
            }
            function showDeleteModal() {
                var $confirm = $("#modalDeleteYesNo");
                $confirm.modal('show');
                return this.false;

            }

          $('#txtSearch').keyup(function () {
               
            $('#<%=gvDepartments.ClientID%>').find('tr:gt(0)').hide();
            var data = $('#txtSearch').val();
            var len = data.length;
            if (len > 0) {
                $('#<%=gvDepartments.ClientID%>').find('tbody tr').each(function () {
                    coldata = $(this).children().eq(2);
                    var temp = coldata.text().toUpperCase().indexOf(data.toUpperCase());
                    if (temp === 0) {
                        $(this).show();
                    }
                });
            } else {
                $('#<%=gvDepartments.ClientID%>').find('tr:gt(0)').show();
            }
             })

            function Search() {
                $('.select2').select2({
                    ajax: {
                        url: "CreateCustomer.aspx/LoadCustomers",
                        type: "POST",
                        data: function (params) {
                            var t = params.term;
                            var query = {
                                search: t
                            }
                            return JSON.stringify(query);
                        },
                        contentType: "application/json; charset=utf-8",
                        dataType: 'json',
                        processResults: function (data) {
                            var result = [];
                            for (var i = 0; i < data.d.length; i++) {
                                result.push({ id: data.d[i].Id, text: data.d[i].Text });
                            }
                            return {
                                results: result
                            };
                        },
                    }
                });
            }

            //Sys.Application.add_load(function () {
            //    $(function () {


            //        $('#ContentSection_txtSearch').on({
            //            keyup: function () {

            //                $.ajax({
            //                    type: "POST",
            //                    url: "CreateRole.aspx/SearchRole",
            //                    data: '{text: "' + $("#ContentSection_txtSearch").val() + '"}',
            //                    contentType: "application/json; charset=utf-8",
            //                    dataType: 'json',
            //                    success: function (data) {

            //                        for (var i = 0; i < data.d.length; i++) {
            //                            $("#gvDepartments").append("<tr><td>" + data.d[i].userRoleName +
            //                                "</td><td>" + data.d[i].CreatedDate + "</td></tr>");

            //                            var x = data.d[i].userRoleName;
            //                            var y = data.d[i].CreatedDate;
            //                            var z = 0;

            //                             $('#ContentSection_RoleList').val(data.d[i]);
            //                        var a = $('#ContentSection_RoleList').val();
            //                            $('#ContentSection_btnRoleList').click();
            //                        }

                                     
                                   
                                    
            //                    }

            //                })




            //            }
            //        });
            //    });
            //});

            function Delete(e, RoleId, IsActive) {
                e.preventDefault();
                $('#ContentSection_hdnRoleId').val(RoleId);

                if (IsActive == 1) {
                    swal.fire({
                        title: 'Are you sure?',
                        html: "Are you sure you want to <strong>Delete</strong> the Role?</br></br>",

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


                            $('#ContentSection_btnDeleteRole').click();
                        } else if (result.dismiss === Swal.DismissReason.cancel) {

                        }
                    });
                }
                else {
                    swal.fire({
                        title: 'Are you sure?',
                        html: "Are you sure you want to <strong>Restore</strong> the Role?</br></br>",

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


                            $('#ContentSection_btnRestoreRole').click();
                        } else if (result.dismiss === Swal.DismissReason.cancel) {

                        }
                    });
                }


            }
        </script>



    </body>





    </html>


</asp:Content>
