<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerUpdateSupplier.aspx.cs" Inherits="BiddingSystem.CustomerUpdateSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    
    <html>
        <head>
             <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
        </head>
        <body>
            <form  runat="server">

<section class="content-header">
      <h1>
       Update Supplier
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Update Supplier</li>
      </ol>
    </section>
    <br />

       <div class="modal modal-primary fade" id="myModal">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title">Mail box</h4>  <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
              </div>
              <div class="modal-body">
               <div class="table-responsive">
                    <div class="col-md-12">
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Compose New Message</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <div class="form-group">
                <input class="form-control" placeholder="To:" value="singermega@gmail.com">
              </div>
              <div class="form-group">
                <input class="form-control" placeholder="Subject:" value="Request for more information">
              </div>
              <div class="form-group">
           <textarea id="compose-textarea" class="form-control" style="height: 300px; ">               

                    </textarea>
              </div>
              <div class="form-group">
                <div class="btn btn-default btn-file">
                  <i class="fa fa-paperclip"></i> Attachment
                  <input name="attachment" type="file">
                </div>
                <p class="help-block">Max. 32MB</p>
              </div>
            </div>
            <!-- /.box-body -->
            <div class="box-footer">
              <div class="pull-right">
               <%-- <button type="button" class="btn btn-default"><i class="fa fa-pencil"></i> Draft</button>--%>
                <button type="submit" class="btn btn-primary"><i class="fa fa-envelope-o"></i> Send</button>
              </div>
              <button type="reset" class="btn btn-default"><i class="fa fa-times"></i> Discard</button>
            </div>
            <!-- /.box-footer -->
          </div>
          <!-- /. box -->
        </div>
                   </div>
              </div>
              <div class="modal-footer">
                <button type="button" class="btn btn-outline pull-right" data-dismiss="modal">Close</button>
              </div>
            </div>
            <!-- /.modal-content -->
          </div>
          <!-- /.modal-dialog -->
        </div>
    <section class="content">
      
             <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Supplier Details</h3>

              <div class="box-tools">
                <div class="input-group input-group-sm" style="width: 150px;">
                  <input type="text" name="table_search" class="form-control pull-right" placeholder="Search">

                  <div class="input-group-btn">
                    <button type="submit" class="btn btn-default"><i class="fa fa-search"></i></button>
                  </div>
                </div>
              </div>
            </div>
            <!-- /.box-header -->



            <div class="box-body table-responsive no-padding">
             <asp:GridView runat="server" ID="gvSupplierList" CssClass="table table-responsive" AutoGenerateColumns="false" GridLines="None" >
                 <Columns>
                     <asp:BoundField  DataField="SupplierId" HeaderText="Id"/>
                      <asp:BoundField  DataField="SupplierName" HeaderText="Name"/>
                      <asp:BoundField  DataField="Email" HeaderText="Email address"/>
                      <asp:BoundField  DataField="PhoneNo" HeaderText="Contact No"/>
                      <asp:BoundField  DataField="IsApproved" HeaderText="Status"/>
                      <asp:BoundField  DataField="IsActive" HeaderText="Active"/>
                     <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton runat="server" ID="lbtnEdit" OnClick="lbtnEdit_Click">Edit</asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>
                   <%--  <asp:TemplateField>
                         <ItemTemplate>
                             <asp:LinkButton runat="server" ID="lbtnView" >View</asp:LinkButton>
                         </ItemTemplate>
                     </asp:TemplateField>--%>
                      
                 </Columns>
             </asp:GridView>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
      </div>
 
      </section>
       

</form>

        </body>
    </html>









       <script type="text/javascript">
           function reply_click(clicked_id) {
               window.location.replace("CompanyUpdatingAndRatingSupplier.aspx?RequestId=" + clicked_id);
           }
      </script>

</asp:Content>
