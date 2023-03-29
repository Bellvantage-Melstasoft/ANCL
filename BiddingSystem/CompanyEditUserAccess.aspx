<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyEditUserAccess.aspx.cs" Inherits="BiddingSystem.CompanyEditUserAccess" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
      <h1>
       Edit User Access
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Edit User Access</li>
      </ol>
    </section>
    <br /> 


    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
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
                 <asp:Button ID="btnYesConfirmYesNo" runat="server"  CssClass="btn btn-primary"  OnClick="btnUpdate_Click" Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>
  
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
          <h3 class="box-title" >User Access Details</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">

            <div runat="server" id="divUpdate">

            <div class="row">

                        <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">System Division</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0" ControlToValidate="ddlSystemDivisions" ValidationGroup="btnSave" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlSystemDivisions" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                </div>
                </div>



            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">User Role</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0" ControlToValidate="ddlUserRoles" ValidationGroup="btnSave" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlUserRoles" runat="server" OnSelectedIndexChanged="ddlUserRoles_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control" Enabled="false"></asp:DropDownList>
                </div>
                </div>
            <div class="col-md-6">
                    <div class="form-group">
                <label for="exampleInputEmail1">Define Action</label>
                        <div style="margin:5px;margin-left:10px;">
                <asp:CheckBoxList ID="chkActionList" runat="server" ></asp:CheckBoxList>
                            </div>
                </div>
                </div>
         
          </div>

               <div class="box-footer">
               <span class="pull-right">
                 <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="btn btn-primary" ValidationGroup="btnUpdate" OnClick="btnUpdate_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Cancel"  CssClass="btn btn-danger" onclick="btnClear_Click"></asp:Button>
                </span>
              </div>
            </div>
          
            <br />
            <br />




          <div class="row">

            <div class="col-md-12">
                 <div class="form-group">
                <label for="exampleInputEmail1">User</label>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0" ControlToValidate="ddlCompanyUsers" ValidationGroup="btnSave" ForeColor="Red">*</asp:RequiredFieldValidator>
                 <asp:DropDownList ID="ddlCompanyUsers" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyUsers_SelectedIndexChanged"></asp:DropDownList>
                </div>
                </div>
          </div>


          



          <div class="row">
     <div class="panel-body">
    <div class="co-md-12">
  <div class="form-group">
    <asp:GridView ID="gvUserAccess" EmptyDataText="No Records Found" runat="server" AutoGenerateColumns="false" CssClass="Grid table table-responsive" DataKeyNames="userRoleId" OnRowDataBound="OnRowDataBound">
    <Columns>
          <asp:BoundField ItemStyle-Width="150px" DataField="userId" HeaderText="userId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
          <asp:BoundField ItemStyle-Width="150px" DataField="departmentId" HeaderText="departmentId"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
          <asp:BoundField ItemStyle-Width="150px" DataField="sysDivisionId" HeaderText="Division Id"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
          <asp:BoundField ItemStyle-Width="150px" DataField="userRoleID" HeaderText="userRoleID"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
        
          <asp:BoundField ItemStyle-Width="150px" DataField="sysDivisionName" HeaderText="DivisionName" />
          <asp:BoundField ItemStyle-Width="150px" DataField="userRoleName" HeaderText="user Role" />
        <asp:TemplateField >
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="images/plus.png" />
                <asp:Panel ID="pnlRoles" runat="server" Style="display: none">
                    <asp:GridView ID="gvActions" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="functionActionName" HeaderText="Actions" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
            <ItemTemplate>
                <asp:ImageButton ID="lbtnEdit" ImageUrl="~/images/document.png" OnClick="lbtnEdit_Click" style="width:26px;height:20px"  runat="server" />
            </ItemTemplate>
        </asp:TemplateField>

          <asp:TemplateField>
            <ItemTemplate>
                 <asp:ImageButton ID="lbtnDelete" ImageUrl="~/images/delete.png"  OnClientClick="return confirm('Are you sure to Delete?');" OnClick="lbtnDelete_Click"   style="width:26px;height:20px;" runat="server" />
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
</asp:Content>
