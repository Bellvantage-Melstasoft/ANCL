<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingSuperAdmin.Master" AutoEventWireup="true" CodeBehind="AdminCreateDepartmentUser.aspx.cs" Inherits="BiddingSystem.AdminCreateDepartmentUser" %>
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



        </head>

        <body>
            <form id="form1" runat="server"  defaultbutton="btnSave">

            <section class="content-header">
      <h1>
       Company Admin Registration
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Company Admin Registration </li>
      </ol>
    </section>
  

      <div class="row" style=" visible="false">
        <div class="col-sm-12">
           <div class="alert  alert-info  alert-dismissable" id="Div1" runat="server" visible="false">
               <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
               <strong>
                   <asp:Label ID="Label2" runat="server" ForeColor="White"></asp:Label>
               </strong>
           </div>
        </div>
    </div>


<section class="content">

    <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" runat="server" ForeColor="White"></asp:Label>
           </strong>
    </div>

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
         <%-- <h3 class="box-title" >User Registration</h3>--%>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-6">
              
                <div class="form-group" >
                <label for="exampleInputEmail1">Select Company</label>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDepartments" InitialValue="0"  Font-Bold="true" ValidationGroup="btnSave" ID="RequiredFieldValidator2" ForeColor="Red">*</asp:RequiredFieldValidator>
               <asp:DropDownList runat="server" ID="ddlDepartments" CssClass="form-control"> </asp:DropDownList>
               </div>


                 <div class="form-group">
                <label for="exampleInputEmail1">Name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Font-Bold="true" ControlToValidate="txtName" ValidationExpression="[a-zA-Z \.]*$" ErrorMessage="Enter Valid characters" ForeColor="Red" ValidationGroup="btnSave"/>
                      <asp:TextBox ID="txtName" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Email Address</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtEmilAddress" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"  Font-Bold="true" ForeColor="Red" ErrorMessage="Enter valid Email Id" ControlToValidate="txtEmilAddress" ValidationGroup="btnSave" SetFocusOnError="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" > </asp:RegularExpressionValidator>
                <asp:TextBox ID="txtEmilAddress" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>

              
                   
                  <div class="form-group">
                <label for="exampleInputEmail1">Is Active</label>
                 <asp:CheckBox ID="chkIsActive" runat="server" CssClass="form-control" TextMode="Password"></asp:CheckBox>
                </div>


                </div>

               <div class="col-md-6">
                     <div class="form-group">
                <label for="exampleInputEmail1">User name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtusername" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:RegularExpressionValidator Display = "Dynamic" ControlToValidate = "txtusername" Font-Bold="true" ID="RegularExpressionValidator3" ValidationGroup="btnSave" ForeColor="Red" ValidationExpression = "^[\s\S]{4,15}$" runat="server" ErrorMessage="Minimum 4 and Maximum 15 characters required."></asp:RegularExpressionValidator>
                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control"></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Password</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtpassword" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" placeholder=""  TextMode="Password"></asp:TextBox>
                </div>
                
                 <div class="form-group">
                <label for="exampleInputEmail1">Confirm Password</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator16" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtConfirmPassword" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                 <asp:CompareValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtConfirmPassword" ValidationGroup="btnSave" ControlToCompare="txtpassword">Password does not match</asp:CompareValidator>     
                
                     <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control"  TextMode="Password"></asp:TextBox>
                </div>

                   </div>
            
           </div>
        
        </div>
        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Submit"  ValidationGroup="btnSave" CssClass="btn btn-primary " OnClick="BtnSave_Click"></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear" OnClick="btnClear_Click"  CssClass="btn btn-danger"></asp:Button>
               
            </span>
                 </div>
      </div>
      <!-- /.box -->
    </section>
</form>

             <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
            <script type="text/javascript">


    $(function () {

        $('[id*=ContentSection_ddlcategory]').multiselect({
            includeSelectAllOption: true
            
        });
        
    
        });
</script>


        </body>
    </html>

</asp:Content>
