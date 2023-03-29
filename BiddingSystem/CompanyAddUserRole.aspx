<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSuperAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyAddUserRole.aspx.cs" Inherits="BiddingSystem.CompanyAddUserRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
      <h1>
       Define User Roles
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Define User Roles</li>
      </ol>
    </section>
    <br /> 
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
    .tablegv tr:nth-child(even){background-color: #f2f2f2;}
    .tablegv tr:hover {background-color: #ddd;}
    .tablegv th {
        padding-top: 12px;
        padding-bottom: 12px;
        text-align: left;
        background-color: #3C8DBC;
        color: white;
    }
    .successMessage
        {
            color: #1B6B0D;
            font-size: medium;
        }
        
        .failMessage
        {
            color: #C81A34;
            font-size: medium;
        }
</style>
<script src="AdminResources/js/jquery-1.10.2.min.js"></script>
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
                 <asp:Button ID="btnYesConfirmYesNo" runat="server"  CssClass="btn btn-primary"  OnClick="btnSave_Click" Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo"  type="button" class="btn btn-danger" >No</button>
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
                <p>Are you sure you want to delete this record? Or? </p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="lbtnDeleteSystemFunction_Click" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>


    <asp:ScriptManager runat="server"></asp:ScriptManager>
          <asp:UpdatePanel runat="server">
              <ContentTemplate>
<asp:HiddenField  ID="hndfRoleId" runat="server"/>
    <section class="content">

        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
         </div>


      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >User Role Details<h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">User Role</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserRoeName" ValidationGroup="btnSave" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:TextBox ID="txtUserRoeName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                </div>
                </div>
            <div class="col-md-6">
                <div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
                <div class="col-sm-offset-2 col-sm-10" >
                <div class="checkbox">
                <label>
                <asp:CheckBox ID="chkIsavtive" runat="server" Checked></asp:CheckBox>
                </label>
                </div>
                </div>
                </div>
            </div>
         
          </div>

        </div>
          
        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save"  CssClass="btn btn-primary" ValidationGroup="btnSave" OnClick="btnSave_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"  
                CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                </span>
              </div>

        <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvUserRoles" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="userRoleId" HeaderText="Role Id" ItemStyle-CssClass="hidden"  HeaderStyle-CssClass="hidden"/>
            <asp:BoundField DataField="userRoleName" HeaderText="Role Name" />
            <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" ItemStyle-CssClass="hidden"  HeaderStyle-CssClass="hidden" />
            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" ItemStyle-CssClass="hidden"  HeaderStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" ItemStyle-CssClass="hidden"  HeaderStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By"  ItemStyle-CssClass="hidden"  HeaderStyle-CssClass="hidden"/>
          
         <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>
                 <asp:Label Text='<%#Eval("IsActive").ToString()== "1"?"Yes":"No" %>' Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
              <asp:TemplateField HeaderText="Edit">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" OnClick="lnkBtnEdit_Click" style="width:26px;height:20px"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
            <%--    <asp:TemplateField HeaderText="Delete">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnCancelRequest" ImageUrl="~/images/delete.png" OnClick="lnkBtnDelete_Click" style="width:26px;height:20px;"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>--%>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
      </div>

        <br />
        <div class="alert  alert-info  alert-dismissable" id="msg2" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessageTwo" ForeColor="White" runat="server"></asp:Label>
           </strong>
         </div>

      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Assign Nodes to User Role</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
                <div class="box-body">

                     <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Select User Role</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlUserRoles" ValidationGroup="btnAddUderSystemDivision" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlUserRoles" runat="server" OnSelectedIndexChanged="ddUserRoles_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                </div>
                </div>
               
         
          </div>

                       <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Select Parent Node</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="ddlSystemDivision" ValidationGroup="btnAddUderSystemDivision"  ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlSystemDivision" OnSelectedIndexChanged="ddlSystemDivision_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                </div>
                </div>
               
          </div>
                      <div class="row">

            <div class="col-md-12">
                 <div class="form-group">
                <label for="exampleInputEmail1">Choose Child Nodes</label>
                 <asp:CheckBoxList ID="chkActionList" runat="server" ></asp:CheckBoxList>
                </div>
                </div>
              
          </div>
                    <div class="row">
                         <div class="col-md-6">
                          <span class="pull-right ">
                 <asp:Button ID="btnAddUderSystemDivision" runat="server" Text="ADD" CssClass="btn btn-primary"  ValidationGroup="btnAddUderSystemDivision" OnClick="btnAddUderSystemDivision_Click"></asp:Button>
                 <asp:Button ID="btnCancelUserRoles"  runat="server" Text="Clear"  OnClick="btnCancelUserRoles_Click"  CssClass="btn btn-danger"></asp:Button>
                </span>
                    </div>
                        </div>
                    <br />

                    <div class="panel panel-default" id="divSystemDivOfUserRole" runat="server">
                        <div class="panel-heading">
                            <div class="panel-title"><p style="font-weight:bold;" id="UserRoleNameTite" runat="server"></p></div>
                      </div>

                    <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">

          <asp:GridView ID="gvSystemDivisions" runat="server" AutoGenerateColumns="false" CssClass = "table table-responsive tablegv" >
                        <Columns>
                            <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                               <ItemTemplate>
                                   <asp:Label Text='<%#Eval("userRoleId") %>'  runat="server"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>

                             <asp:TemplateField  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                               <ItemTemplate>
                                   <asp:Label Text='<%#Eval("systemDivisionId") %>'  runat="server"></asp:Label>
                               </ItemTemplate>
                           </asp:TemplateField>

                          <asp:BoundField DataField="userRoleId" HeaderText="userRole Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                          <asp:BoundField DataField="systemDivisionId" HeaderText="system Division Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                          <asp:BoundField DataField="systemDivisionName" HeaderText="Parent Node" />
                     <asp:TemplateField  HeaderText="Delete">
                  <ItemTemplate>
                      <asp:ImageButton ID="lbtnDeleteSystemFunction" CommandName="Delete" ImageUrl="~/images/delete.png"  CssClass="deleteParentNode"  style="width:26px;height:20px" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

    </div>
    </div>
    </div>
                           
                    </div>










                    <%--<div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">

          <asp:GridView ID="gvFunctions" runat="server" AutoGenerateColumns="false" DataKeyNames="functionId" CssClass = "ChildGrid table table-responsive tablegv" 
                        OnRowDeleting="gvFunctions_RowDeleting" OnRowEditing="gvFunctions_RowEditing" OnRowUpdating="gvFunctions_RowUpdating" OnRowCancelingEdit="gvFunctions_RowCancelingEdit">
                        <Columns>
                          <asp:BoundField DataField="systemDivisionId" HeaderText="system Division Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                          <asp:TemplateField HeaderText="Sequence Id"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblsystemDivisionId" Text='<%# Eval("systemDivisionId") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                               <asp:TemplateField HeaderText="Sequence Id" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblfunctionId" Text='<%# Eval("functionId") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:TextBox ID="txtfunctionId" runat="server" Text='<%# Bind("functionId")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                              <asp:TemplateField ItemStyle-Width="100px" HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfunctionName" runat="server" Text='<%# Eval("functionName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtfunctionName" runat="server" Text='<%# Bind("functionName")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                            
                   <asp:TemplateField  ShowHeader="false">
                  <ItemTemplate>
                      <asp:ImageButton ID="lbtnEditSystemFunction" CommandName="Edit" ImageUrl="~/images/document.png"  style="width:26px;height:20px"  runat="server" />
                  </ItemTemplate>
                       <EditItemTemplate>
                            <asp:LinkButton runat="server" CommandName="Update" Text="Update" OnClientClick="return confirm('Are you sure to Update?');" />
                             <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" ForeColor="Maroon" />
                       </EditItemTemplate>
                </asp:TemplateField>

                             <asp:TemplateField  ShowHeader="false">
                  <ItemTemplate>
                      <asp:ImageButton ID="lbtnDeleteSystemFunction" CommandName="Delete" ImageUrl="~/images/delete.png"  OnClientClick="return confirm('Are you sure to Delete?');"  style="width:26px;height:20px" runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>

                        </Columns>
                    </asp:GridView>

    </div>
    </div>
    </div>--%>


                    </div>


              </div>



   
    </section>

    
           </ContentTemplate>
              <Triggers>
                  <asp:PostBackTrigger ControlID="btnSave" />
              </Triggers>
          </asp:UpdatePanel>
      
 <asp:HiddenField  runat="server" ID="hdnUserRoleId"/>
     <asp:HiddenField  runat="server" ID="hdnsysDivisionId"/>
</form>

     <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
     <script type="text/javascript">

       
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

     <script>

        Sys.Application.add_load(function() {

            $(".deleteParentNode").click(function () {
                var UserRoleId = $(this).parent().prev().prev().prev().prev().prev().children().html();
                var sysDivisionId = $(this).parent().prev().prev().prev().prev().children().html();
                $("#<%=hdnUserRoleId.ClientID%>").val(UserRoleId);
                $("#<%=hdnsysDivisionId.ClientID%>").val(sysDivisionId);
                showDeleteModal();
                event.preventDefault();
            });
        });
    </script>


</asp:Content>
