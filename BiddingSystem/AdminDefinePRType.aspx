﻿<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSuperAdmin.Master" AutoEventWireup="true" CodeBehind="AdminDefinePRType.aspx.cs" Inherits="BiddingSystem.AdminDefinePRType" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <section class="content-header">
      <h1>
       Define PR Type
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Define PR Type</li>
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
        /*.tablegv tr:hover {background-color: #ddd;}*/
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
     <form runat="server" id="form1">
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
                <p>Are you sure you want to delete this record? Or ?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="lnkBtnDelete_Click" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
    </div>
     <section class="content">

     <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
     <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
     <strong>
         <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
     </strong>
     </div>

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Add PR Type</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
      <div class="box-body">
          <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                <label for="exampleInputEmail1">Select Company</label> 
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlCompany" InitialValue="0" ValidationGroup="btnSave" ID="RequiredFieldValidator2" ForeColor="Red">*</asp:RequiredFieldValidator>
                <asp:DropDownList ID="ddlCompany" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>

            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">PR Type</label>
                 <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPRType"  ValidationGroup="btnSave" ID="RequiredFieldValidator3" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:TextBox ID="txtPRType" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
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
        </div>

          <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Save" 
                CssClass="btn btn-primary" ValidationGroup="btnSave" onclick="btnSave_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="clear"  
                CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                </span>
        </div>
     </div>
     </section>

    <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvPrType" runat="server" CssClass="table table-responsive tablegv" GridLines="None" AutoGenerateColumns="false" OnPageIndexChanging="gvPrType_PageIndexChanging" AllowPaging="true" PageSize="10">
        <Columns>
            <asp:BoundField DataField="CompanyId" HeaderText="Company ID" />
            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
            <asp:BoundField DataField="PrTypeId" HeaderText="PrTypeId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PrTypeName" HeaderText="PR Type Name" />
            <asp:BoundField DataField="IsActive"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
            <asp:TemplateField HeaderText="Active">
                <ItemTemplate>
                    <asp:Label ID="Label2" Text='<%#Eval("IsActive").ToString()== "1"?"Yes":"No" %>' Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" ToolTip="Edit" ImageUrl="~/images/document.png" OnClick="lnkBtnEdit_Click" style="width:26px;height:20px"
                        runat="server" />
                </ItemTemplate>
              </asp:TemplateField>
               <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                 <ItemTemplate>
                     <asp:Label  runat="server" ID="lblCompanyId" Text='<%#Eval("CompanyId")%>'></asp:Label>
                 </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" >
                 <ItemTemplate>
                     <asp:Label  runat="server" ID="lblPrTypeId" Text='<%#Eval("PrTypeId")%>'></asp:Label>
                 </ItemTemplate>
               </asp:TemplateField>
              <asp:TemplateField HeaderText="Delete">
                <ItemTemplate>
                    <asp:ImageButton ID="btnCancelRequest"  ImageUrl='<%#Eval("IsActive").ToString()== "1"?"~/images/delete.png":"~/images/dlt.png" %>' Enabled='<%#Eval("IsActive").ToString()== "1"?true:false %>'  ToolTip='<%#Eval("IsActive").ToString()== "1"?"Delete":"Deleted" %>' CssClass="deleteSubCategory" style="width:26px;height:20px;"
                        runat="server" />
                </ItemTemplate>
              </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField  runat="server" ID="hdnPrTypeId"/>
     </form>
     <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
     <script type="text/javascript">
        $("#btnNoConfirmYesNo").on('click').click(function () {
                     var $confirm = $("#modalConfirmYesNo");
                     $confirm.modal('hide');
                     return this.false;
        });
        
    </script>

     <script type="text/javascript">
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

     <script type="text/javascript">
         $(".deleteSubCategory").click(function () {
             var prTypeId = $(this).parent().prev().children().html();
             var companyId = $(this).parent().prev().prev().children().html();
             $("#<%=hdnPrTypeId.ClientID%>").val(prTypeId);
             $("#<%=HiddenField1.ClientID%>").val(companyId);
             showDeleteModal();
             event.preventDefault();
         });
    </script>
</asp:Content>


