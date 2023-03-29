<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyApproveSupplier.aspx.cs" Inherits="BiddingSystem.CompanyApproveSupplier" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
<section class="content-header">
      <h1>
       Approval Of The Supplier Request
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Approval of the Supplier Request</li>
      </ol>
    </section>
    <br />
     <section class="content">
          <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Supplier Requests</h3>

              <div class="box-tools">
                <div class="input-group input-group-sm" style="width: 150px;">
                <select class="form-control pull-right">
                <option>Sort By</option>
                <option>Approved</option>
                <option>Pending</option>
                <option>Denied</option>
                </select>
                  <%--<input type="text" name="table_search" class="form-control pull-right" placeholder="Search">
--%>
                </div>
              </div>
            </div>
            <!-- /.box-header -->
            <div class="box-body table-responsive no-padding">
              <table class="table table-hover">
                <tr>
                  <th>ID</th>
                  <th>User</th>
                  <th>Requested Date</th>
                  <th>Category</th>
                  <th>Reason</th>
                  <th>Status</th>
                </tr>
                <tr>
                  <td>183</td>
                  <td>Singer</td>
                  <td>11-4-2018</td>
                  
                  <td>Furniture,Electronics,Office</td>
                  <td>Trusted customer.</td>
                  <td><span class="label label-success" id="183" onClick="reply_click(this.id)" style="cursor:pointer">Approved</span></td>
                </tr>
                <tr>
                  <td>184</td>
                  <td>Damro</td>
                  <td>12-4-2018</td>
                  
                  <td>Furniture,Electronics,Office</td>
                  <td>Pending confirmation</td>
                  <td><span class="label label-warning" id="184" onClick="reply_click(this.id)"  style="cursor:pointer">Pending</span></td>
                </tr>
                <tr>
                  <td>185</td>
                  <td>Abans</td>
                  <td>13-4-2018</td>
                  
                  <td>Electronics</td>
                  <td>Trusted Customer.</td>
                  <td><span class="label label-success" id="185" onClick="reply_click(this.id)"  style="cursor:pointer">Approved</span></td>
                </tr>
                <tr>
                  <td>186</td>
                  <td>Innovex Electrics</td>
                  <td>14-4-2018</td>
                  
                  <td>11-7-2014</td>
                  <td>Pending status.</td>
                  <td><span class="label label-danger" id="186" onClick="reply_click(this.id)"  style="cursor:pointer">Denied</span></td>
                </tr>
                 <tr>
                  <td>187</td>
                  <td>Atlas Bookshop</td>
                  <td>15-4-2018</td>
                  
                  <td>Stationary</td>
                  <td>Pending status.</td>
                  <td><span class="label label-danger" id="187" onClick="reply_click(this.id)"  style="cursor:pointer">Denied</span></td>
                </tr>
                 <tr>
                  <td>188</td>
                  <td>Perera Spare Parts</td>
                  <td>16-4-2018</td>
                  
                  <td>Vehicle Spare Parts</td>
                  <td>Pending status.</td>
                  <td><span class="label label-danger" id="188" onClick="reply_click(this.id)"  style="cursor:pointer">Denied</span></td>
                </tr>
                 <tr>
                  <td>189</td>
                  <td>Dambulu Furniture</td>
                  <td>19-4-2018</td>
                  
                  <td>Furniture</td>
                  <td>Pending status.</td>
                  <td><span class="label label-danger" id="189" onClick="reply_click(this.id)"  style="cursor:pointer">Denied</span></td>
                </tr>
              </table>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
      </div>
      </section>

      <script type="text/javascript">
          function reply_click(clicked_id) {
              window.location.replace("CompanyApproveSupplierByAdmin.aspx?RequestId=" + clicked_id);
          }
      </script>
</asp:Content>
