<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewPODetails.aspx.cs" Inherits="BiddingSystem.ViewPODetails" %>
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
            <form  runat="server">

 

<section class="content-header">

    


      <h1>
       Edit Company Admnistrator
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">View Purchase Orders</li>
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
            <div class="box-header">
             <%-- <h3 class="box-title">User Details</h3>--%>

              <div class="box-tools">
                <div class="input-group input-group-sm" style="width: 150px;">
                  <input type="text" name="table_search" class="form-control pull-right" placeholder="Search">

                  <div class="input-group-btn">
                    <button type="submit" class="btn btn-default"><i class="fa fa-search"></i></button>
                  </div>
                </div>
              </div>
            </div>
        
             

                  <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
             <asp:GridView runat="server" ID="gvPurchaseOrders" CssClass="table table-responsive tablegv" AutoGenerateColumns="false" GridLines="None" >
                 <Columns>
                     <asp:BoundField  DataField="UserId" HeaderText="UserId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                      <asp:BoundField  DataField="FirstName" HeaderText="Name"/>
                      <asp:BoundField  DataField="EmailAddress" HeaderText="Email address"/>
                      <asp:BoundField  DataField="CreatedDate" HeaderText="CreatedDate"/>
                      <asp:BoundField  DataField="IsActive" HeaderText="Active"/>
                     
                 <asp:TemplateField HeaderText="Edit">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" OnClick="lbtnEdit_Click" style="width:26px;height:20px"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnCancelRequest" ImageUrl="~/images/delete.png" style="width:26px;height:20px;"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
                      
                 </Columns>
             </asp:GridView>
            </div>
        </div>
                      </div>
             
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
      </div>
 
      </div>

</form>

        </body>
    </html>

</asp:Content>
