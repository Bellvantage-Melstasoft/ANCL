<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyApproveSupplierByAdmin.aspx.cs" Inherits="BiddingSystem.CompanyApproveSupplierByAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
      <h1>
       Supplier Registration (Approval)
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li><a href="CompanyApproveSupplier.aspx"><i class="fa fa-home"></i> Approval Of The Supplier Request </a></li>
        <li class="active">Supplier Registration</li>
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
<form id="form1" runat="server">
<section class="content">

    <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" runat="server"></asp:Label>
           </strong>
    </div>

      <!-- SELECT2 EXAMPLE -->
      <div class="box box-default">
        <div class="box-header with-border">
          <h3 class="box-title" >Supplier Detailed Request</h3>

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
                <label for="exampleInputEmail1">Supplier ID</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="txtGroupName" runat="server" CssClass="form-control" placeholder="Group Name"></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Name</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="" Text="Singer"></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Address</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" placeholder="" Text="Colombo"></asp:TextBox>
                </div>

                
                <div class="form-group">
                <label for="exampleInputEmail1">Office Contact No.</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" placeholder="" Text="011 22 55555"></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Mobile No.</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" placeholder="" Text="071 22 55555"></asp:TextBox>
                </div>

                  <div class="form-group">
                <label for="exampleInputEmail1">Categoty</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" placeholder="" Text="Furniture,Electronics,Office"></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Reason</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator12" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox11" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="" Text="Trusted customer."></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Requested Date</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="" Text="2018-04-12"></asp:TextBox>
                </div>

               

                </div>
              <!-- /.form-group -->
  
            <!-- /.col -->
            <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1">Business Registration No.</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" placeholder="" Text="BR001"></asp:TextBox>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Vat Registration No.</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" placeholder="" Text="V0001"></asp:TextBox>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Company Type</label>
                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ForeColor="Red"  InitialValue="Select Location" Font-Bold="true" ControlToValidate="TextBox1" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:DropDownList ID="ddlMainCategory" runat="server" CssClass="form-control" >
                <asp:ListItem>Select Company Type</asp:ListItem>
                <asp:ListItem>Sole Proprietorship</asp:ListItem>
                <asp:ListItem>Private Company</asp:ListItem>
                <asp:ListItem>Public Company</asp:ListItem>
                <asp:ListItem>Electrics</asp:ListItem>
                <asp:ListItem>Corporation</asp:ListItem>
                </asp:DropDownList>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">Business Category</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
                <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Business Category" Text=""></asp:TextBox>
                </div>

                <div class="form-group">
               <label for="exampleInputEmail1">Company Type</label><br />
               <img src="AppResources/themes/images/singer.png" CssClass="form-control" style=" width: 150px; height: 150px;margin-left:100px "/>
               </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Uploaded Files</label>
                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtGroupName" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>     
              <%--  <asp:TextBox ID="TextBox4" runat="server" CssClass="form-control" placeholder="Business Category" Text=""></asp:TextBox>--%>
                </div>
            <!-- /.col -->
          </div>
           </div>
          <!-- /.row -->
        </div>
        <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Approve"  ValidationGroup="btnSave" CssClass="btn btn-primary "></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Back"  CssClass="btn btn-danger"></asp:Button>
               
            </span>
                 </div>
        <!-- /.box-body -->
      </div>
      <!-- /.box -->
    </section>
</form>
</asp:Content>