<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSuperAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyAddFunctionAction.aspx.cs" Inherits="BiddingSystem.CompanyAddFunctionAction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
      <h1>
       Define Role Actions
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Define Role Actions</li>
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
          <h3 class="box-title" >Role Action Details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Action Name</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtActionName" ValidationGroup="btnSave" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:TextBox ID="txtActionName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                </div>
                </div>
                <div class="col-md-6">
                <div class="form-group">
                <label for="exampleInputEmail1">IsActive</label>
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
                CssClass="btn btn-primary" ValidationGroup="btnSave" OnClick="confirmation_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="clear"  
                CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                </span>
              </div>

      </div>
   
    </section>

    <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvMainCategory" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="functionActionId" HeaderText="function Action Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="functionActionName" HeaderText="Action" />
            <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedDate" HeaderText="Updated Date" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="UpdatedBy" HeaderText="Updated By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
          
         <asp:TemplateField HeaderText="IsActive">
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
           </ContentTemplate>
              <Triggers>
                  <asp:PostBackTrigger ControlID="btnSave" />
              </Triggers>
          </asp:UpdatePanel>
      
 
</form>
    
</asp:Content>
