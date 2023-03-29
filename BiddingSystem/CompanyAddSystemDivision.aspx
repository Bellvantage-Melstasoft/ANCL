<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSuperAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyAddSystemDivision.aspx.cs" Inherits="BiddingSystem.CompanyAddSystemDivision" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
      <h1>
       Define Parent Node
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Define Parent Node</li>
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
          .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height:200%;
            border: 1px solid #ddd;
            text-align:center;
        }
        .Grid th
        {
            background-color: #3c8dbc;
            color: White;
            font-size: 10pt;
            line-height:200%;
            text-align :center;
            border: 1px solid #ddd;
            padding: 8px;
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%;
            text-align:center;
        }
        .ChildGrid th
        {
            color: White;
            font-size: 10pt;
            line-height:200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
        .AlgRgh
        {
          text-align:center;
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
                <button id="btnNoConfirmYesNo"  type="button" class="btn btn-danger" onclick="hideModal();" >No</button>
            </div>
        </div>
    </div>
</div>

       <div id="modalConfirmChildNode" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="lblTitleConfirmChildNode" class="modal-title">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p>Are you sure to submit your details ?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary"  OnClick="btnSave_Click" Text="Yes" ></asp:Button>
                <button id="btnNoConfirmChildNode"  type="button" class="btn btn-danger" onclick="hideModal();" >No</button>
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
                 <asp:Button ID="Button2" runat="server"  CssClass="btn btn-primary"  OnClick="lbtnDeleteSystemFunction_Click" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>

    <asp:ScriptManager runat="server"></asp:ScriptManager>
          <asp:UpdatePanel runat="server">
              <ContentTemplate>
<asp:HiddenField  ID="HiddenField1" runat="server"/>
    <section class="content">

        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
         </div>


      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Parent Node Details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Parent Node Name</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSystemDivision" ValidationGroup="btnSave" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:TextBox ID="txtSystemDivision" runat="server" CssClass="form-control"></asp:TextBox>
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
                 <asp:Button ID="btnSave" runat="server" Text="Save" 
                CssClass="btn btn-primary" ValidationGroup="btnSave" OnClick="btnSave_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"   CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                </span>
              </div>

        <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvSystemDivisions" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false" EmptyDataText="No System Divisions Found">
        <Columns>
            <asp:BoundField DataField="systemDivisionId" HeaderText="System Division Id" />
            <asp:BoundField DataField="systemDivisionName" HeaderText="Parent Node Name" />
            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
          
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



     <div class="alert  alert-info  alert-dismissable" id="msg2" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessageTwo" ForeColor="White" runat="server"></asp:Label>
           </strong>
         </div>


      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Assign Child Node to Parent Node</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
                <div class="box-body">

                     <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Select Parent Node</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlSystemDivisions" ValidationGroup="btnAddFunctionToDivision" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlSystemDivisions" runat="server" OnSelectedIndexChanged="ddlSystemDivisions_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                </div>
                </div>
               
         
          </div>
                    <div class="row">

                        <div class="col-md-6">
                <div class="form-group">
                <label for="exampleInputEmail1">Child Node Sequence No</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ControlToValidate="txtFunctionSeqience" ValidationGroup="btnAddFunctionToDivision" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtFunctionSeqience" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                </div>
                        </div>
                      <div class="row">
               <div class="col-md-6">
                <div class="form-group">
                <label for="exampleInputEmail1">Child Node Name</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"  ControlToValidate="txtDivisionFunction" ValidationGroup="btnAddFunctionToDivision" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtDivisionFunction" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

            </div>
                    </div>
                    <div class="row">
                         <div class="col-md-6">
                          <span class="pull-right ">
                 <asp:Button ID="btnAddFunctionToDivision" runat="server" Text="ADD" CssClass="btn btn-primary"  ValidationGroup="btnAddFunctionToDivision" OnClick="btnAddFunctionToDivision_Click"></asp:Button>
                 <asp:Button ID="btnCancelFunction"  runat="server" Text="clear"  OnClick="btnCancelFunction_Click"  CssClass="btn btn-danger"></asp:Button>
                </span>
                    </div>
                        </div>

                    <div class="panel-body">
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

                               <asp:TemplateField HeaderText="Sequence Id"  ItemStyle-Width="100px" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblfunctionId" Text='<%# Eval("functionId") %>' runat="server"></asp:Label>
                                            </ItemTemplate>
                                             <EditItemTemplate>
                                                <asp:TextBox ID="txtfunctionId" runat="server" Text='<%# Bind("functionId")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>


                              <asp:TemplateField HeaderText="Child Node">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfunctionName" runat="server" Text='<%# Eval("functionName")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtfunctionName" runat="server" Text='<%# Bind("functionName")%>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                            
                   <asp:TemplateField HeaderText="Edit" >
                  <ItemTemplate>
                      <asp:ImageButton ID="lbtnEditSystemFunction" CommandName="Edit" ImageUrl="~/images/document.png"  style="width:26px;height:20px"  runat="server" />
                  </ItemTemplate>
                       <EditItemTemplate>
                            <asp:LinkButton runat="server" CommandName="Update" Text="Update" />
                             <asp:LinkButton runat="server" CommandName="Cancel" Text="Cancel" ForeColor="Maroon" />
                       </EditItemTemplate>
                </asp:TemplateField>

                             <asp:TemplateField  HeaderText="Delete">
                  <ItemTemplate>
                      <asp:ImageButton ID="lbtnDeleteSystemFunction" ImageUrl="~/images/delete.png"  CssClass="lbtnDeleteSystemFunction_Click"  style="width:26px;height:20px" runat="server" />
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
              <Triggers>
                  <asp:PostBackTrigger ControlID="btnSave" />
              </Triggers>
          </asp:UpdatePanel>
      <asp:HiddenField  ID="hdnSystemDivisionId" runat="server"/>
 
</form>
     <script src="AdminResources/js/datetimepicker/jQUERY.min.js"></script>
 
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
    <script type="text/javascript">
        function hideModal() {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        }
    </script>


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

            $(".lbtnDeleteSystemFunction_Click").click(function () {
                var SystemDivisionId = $(this).parent().prev().prev().prev().children().html();
                $("#<%=hdnSystemDivisionId.ClientID%>").val(SystemDivisionId);
                showDeleteModal();
            });
        });
    </script>
</asp:Content>
