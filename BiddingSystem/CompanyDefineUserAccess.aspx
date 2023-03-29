<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyDefineUserAccess.aspx.cs" Inherits="BiddingSystem.CompanyDefineUserAccess" %>

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
            color: #1B6B0D;
            font-size: medium;
        }

        .failMessage {
            color: #C81A34;
            font-size: medium;
        }

        .Grid td {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            border: 1px solid #ddd;
            text-align: center;
        }

        .Grid th {
            background-color: #3c8dbc;
            color: White;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
            border: 1px solid #ddd;
            padding: 8px;
        }

        .ChildGrid td {
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
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
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

    <section class="content-header">
      <h1>
       Define User Access
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Define User Access</li>
      </ol>
    </section>
    <br />   
    
    <form id="form1" runat="server">

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
                        <asp:Button ID="btnYesConfirmYesNo" runat="server" CssClass="btn btn-primary" Text="Yes"></asp:Button>
                        <button id="btnNoConfirmYesNo" type="button" class="btn btn-danger">No</button>
                    </div>
                </div>
            </div>
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
                        <p>Are you sure to Delete this Record ?</p>
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" OnClick="btnDeleteUserAccess_Click" Text="Yes"></asp:Button>
                        <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">No</button>
                    </div>
                </div>
            </div>
        </div>

        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hndfRoleId" runat="server" />
                <section class="content" style="padding-top: 0px">

        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
         </div>
    

      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Custom User Access<h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">User</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlCompanyUsers" ValidationGroup="btnAdd" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlCompanyUsers" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyUsers_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                </div>
                </div>
          </div>

              <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">User Role</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="ddlUserRoles" ValidationGroup="btnAdd" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlUserRoles" runat="server"  CssClass="form-control"></asp:DropDownList>
                </div>
                </div>
           
         
          </div>


          <div class="row">
           <div class="panel-body col-md-6">
    <div class="col-md-12">
    <div class="table-responsive">
    <asp:GridView ID="gvmaincategory" runat="server" CssClass="table table-responsive tablegv Grid" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Records Found" OnRowDataBound="OnRowDataBound"  DataKeyNames="systemDivisionId" AllowPaging="true" PageSize="20"  >
                    <Columns>
                <asp:TemplateField >
                <ItemTemplate>
                    <img alt = "" style="cursor: pointer" src="images/plus.png" />
                    <asp:Panel ID="pnlOrders" CssClass="row" runat="server" Style="display: none">
                        <asp:GridView ID="gvSubcatgry" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="uroleid" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                 <asp:BoundField ItemStyle-Width="150px" DataField="functionId" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="systemDivisionId" HeaderText="" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="functionName" HeaderText="Function Name" />
                                <asp:TemplateField HeaderText="Is Assigned">
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
                    </Columns>
                </asp:GridView>

    </div>
    </div>
    </div>
          
          </div>
         
        </div>
           
      </div>

    </section>

                <div class="row">
                    <div class="co-sm-12">
                        <asp:GridView ID="gvUserRoles" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="userRoleId" HeaderText="function Action Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="userRoleName" HeaderText="Role Name" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="CreatedBy" HeaderText="Created By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                <asp:TemplateField HeaderText="IsActive">
                                    <ItemTemplate>
                                        <asp:Label Text='<%#Eval("IsActive").ToString()== "1"?"Yes":"No" %>' Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Edit">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png"  Style="width: 26px; height: 20px"
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>

        <asp:HiddenField ID="hdnUserId" runat="server" />
    </form>


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
            $("#<%=hdnUserId.ClientID%>").val()
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            return this.false;
        }
    </script>


    <script>

        Sys.Application.add_load(function () {

            $(".deleteUserAccess").click(function () {
                var userID = $(this).parent().prev().prev().prev().prev().prev().children().html();
                $("#<%=hdnUserId.ClientID%>").val(userID);
                showDeleteModal();
            });
        });
    </script>

</asp:Content>
