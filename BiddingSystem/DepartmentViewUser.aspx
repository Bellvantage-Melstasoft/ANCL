<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSuperAdmin.Master" AutoEventWireup="true" CodeBehind="DepartmentViewUser.aspx.cs" Inherits="BiddingSystem.DepartmentViewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <html>
        <head>
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
            color: white;
            font-size: medium;
        }
        
        .failMessage
        {
            color: white;
            font-size: medium;
        }
        .activePhase
        {
            text-align:center;
            border-radius:3px;
        }
</style>

               <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
        </head>
        <body>
            <form  runat="server">

 

<section class="content-header">

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
                 <asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="btnDeleteCompanyAdmin_Click" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>


      <h1>
       Edit Company Administrator
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="SuperAdminDashboard.aspx.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Edit Company Administrator</li>
      </ol>

    </section>
 
<div class="content" style="min-height:inherit;" >

            <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                <strong>
                  <asp:Label ID="lbMessage" runat="server"></asp:Label>
                </strong>
             </div>
             <div class="row" style="min-height:inherit;">
        <div class="col-xs-12">
          <div class="box">
               <div class="form-group" style="margin:10px;">
                <label for="exampleInputEmail1">Select Company</label>
                <asp:DropDownList runat="server" ID="ddlDepartments" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlDepartments_OnSelectedIndexChanged"> </asp:DropDownList>
               </div>
              <br />
           
                  <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
             <asp:GridView runat="server" ID="gvCompanyUsers" CssClass="table table-responsive tablegv" AutoGenerateColumns="false" GridLines="None" EmptyDataText="No Administrators Found" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvCompanyUsers_PageIndexChanging">
                 <Columns>
                      <asp:TemplateField ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                         <ItemTemplate >
                             <asp:Label  runat="server"  ID="lblUserId" Text='<%#Eval("UserId")%>'  ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>

                     <asp:BoundField  DataField="UserId" HeaderText="UserId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                      <asp:BoundField  DataField="FirstName" HeaderText="Name"/>
                      <asp:BoundField  DataField="EmailAddress" HeaderText="Email Address"/>
                      <asp:BoundField  DataField="CreatedDate" HeaderText="Created Date"  DataFormatString="{0:dd/MM/yyyy HH:mm tt}"/>
                       <asp:BoundField  DataField="createdBy" HeaderText="Created By"/>
                       <asp:BoundField  DataField="updatedDate" HeaderText="Updated Date"  DataFormatString="{0:dd/MM/yyyy HH:mm tt}"/>
                       <asp:BoundField  DataField="updatedBy" HeaderText="Updated By" />
                     
                     <asp:TemplateField HeaderText="Active">
                         <ItemTemplate >
                             <asp:Label CssClass="activePhase" runat="server" ID="lblIsActive" Text='<%#Eval("IsActive").ToString()=="1"?"Yes":"No"%>' Font-Bold="true" ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' ></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                 <asp:TemplateField HeaderText="Edit">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" OnClick="lbtnEdit_Click" style="width:26px;height:20px"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnDeleteCompanyAdmin" ImageUrl='<%#Eval("IsActive").ToString()== "1"?"~/images/delete.png":"~/images/dlt.png" %>' Enabled='<%#Eval("IsActive").ToString()== "1"?true:false %>'  ToolTip='<%#Eval("IsActive").ToString()== "1"?"Delete":"Deleted" %>'  CssClass="deleteCompanyAdmin" style="width:26px;height:20px;"
                          runat="server" />
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

<div id="divUpdateAdmin" runat="server" visible="false">
<section class="content" >

      <div class="box box-info">
        <div class="box-header with-border">
          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <div class="box-body">
          <div class="row">
            <div class="col-md-6">
              
                 <div class="form-group">
                <label for="exampleInputEmail1">Name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Font-Bold="true" ControlToValidate="txtName" ValidationExpression="[a-zA-Z \.]*$" ErrorMessage="Enter Valid characters" ForeColor="Red" ValidationGroup="btnSave"/>
                      <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Email Address</label>
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  ForeColor="Red" Font-Bold="true" ErrorMessage="Enter valid Email Id" ControlToValidate="txtEmilAddress" ValidationGroup="btnSave" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" > </asp:RegularExpressionValidator>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmilAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtEmilAddress" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                  <div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
                 <asp:CheckBox ID="chkIsActive" runat="server" CssClass="form-control" placeholder="" TextMode="Password"></asp:CheckBox>
                </div>

                </div>

               <div class="col-md-6">
                    <div class="form-group">
                <label for="exampleInputEmail1">User name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtusername" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
               <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtusername" Font-Bold="true" ID="RegularExpressionValidator3" ValidationGroup="btnSave" ForeColor="Red" ValidationExpression = "^[\s\S]{4,15}$" runat="server" ErrorMessage="Minimum 4 and Maximum 15 characters required."></asp:RegularExpressionValidator>
               <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Password</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtpassword" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
                </div>
                   </div>
            
           </div>
        
        </div>
        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnUdate" runat="server" Text="Update"  ValidationGroup="btnSave" CssClass="btn btn-primary " OnClick="BtnUpdate_Click"></asp:Button>
                 <asp:Button ID="btnCancel"  runat="server" Text="Cancel"  OnClick="btnCancel_Click" CssClass="btn btn-danger"></asp:Button>
               
            </span>
                 </div>
      </div>
    </section>
</div>
                <asp:HiddenField ID="hdnCompanyUser" runat="server" />
</form>
        </body>
    </html>
     <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
       <script type="text/javascript">
           function reply_click(clicked_id) {
               window.location.replace("CompanyUpdatingAndRatingSupplier.aspx?RequestId=" + clicked_id);
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
            $("#<%=hdnCompanyUser.ClientID%>").val()
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            event.preventDefault();
            return this.false;
          
        }
    </script>


    <script type="text/javascript">

      //  Sys.Application.add_load(function() {

            $(".deleteCompanyAdmin").click(function () {
                var userID = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().prev().children().html();
                $("#<%=hdnCompanyUser.ClientID%>").val(userID);
                showDeleteModal();
            });
      //  });
    </script>

</asp:Content>
