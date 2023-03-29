<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="GeneralSettings.aspx.cs" Inherits="BiddingSystem.GeneralSettings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>

<section class="content-header">
      <h1>
       General Settings
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">General Settings</li>
      </ol>
    </section>
    <br />


    <div class="row" style=" visible="false">
        <div class="col-sm-12">
           <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" runat="server"></asp:Label>
           </strong>
    </div>
        </div>
    </div>
    
    <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
            <!-- Main content -->
    <form id="form1" runat="server">
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Settings</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="example2" class="table table-bordered table-hover" style="border: none;font-size:15px;color:Black">
                <tbody>
                <tr>
                  <td>Bid opening period(days)</td>
     
                  <td>
                  <asp:TextBox ID="txtNoOfDay" class="form-control"  style="width:70px" runat="server" placeholder="days"></asp:TextBox>
                  </td>
                  <td></td>
                </tr>
                <tr>
                  <td>Can override</td>
     
                  <td>
                  <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="chkBidOpenYes" runat="server" Text="Yes"  GroupName="chkBid"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                  <td>
                       <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="chkBidOpenNo" runat="server" Text="No"  GroupName="chkBid"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                </tr>
                <tr>
                  <td>Bid only submitted by registered supplier</td>
          
                    <td>
                  <div class="checkbox">
                     <label>
                         <asp:RadioButton ID="chkRegSupYes" runat="server" Text="Yes"  GroupName="chkReg"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                  <td>
                       <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="chkRegSupNo" runat="server" Text="No"  GroupName="chkReg"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                </tr>
                <tr>
                  <td>View bids online upon PR creation</td>

                    <td>
                  <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="chkViewBidsYes" runat="server" Text="Yes"  GroupName="chkViewBids"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                  <td>
                       <div class="checkbox">
                     <label>
                       <asp:RadioButton ID="chkViewBidsNo" runat="server" Text="No"  GroupName="chkViewBids"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                </tr>

                <tr>
                  <td>Manual bid allows only selected item/s</td>

                    <td>
                  <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="rdoManualBidAllowsSelectedItems" runat="server" Text="Yes"  GroupName="chkViewBidsMan"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                  <td>
                       <div class="checkbox">
                     <label>
                       <asp:RadioButton ID="rdoManualBidAllowsAllItems" runat="server" Text="No"  GroupName="chkViewBidsMan"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                </tr>



                       <tr>
                  <td>Include NBT/VAT</td>

                    <td>
                  <div class="checkbox">
                   
                      <asp:Panel ID="pnlNBT" runat="server">

                         <table id="vatNbtTable" class="table table-bordered " style="border: none;font-size:15px;color:Black">
                             <tr>
                                 <td>
                                     <asp:Label ID="lblNBT1" runat="server" Text="NBT Value 1"></asp:Label>
                                 </td>

                                 <td>
                                        <asp:TextBox ID="txtNBT1" runat="server"></asp:TextBox>
                                 </td>
                             </tr>

                             <tr>
                                 <td>
                                     <asp:Label ID="lblNBT2" runat="server" Text="NBT Value 2"></asp:Label>
                                 </td>

                                 <td>
                                       <asp:TextBox ID="txtNBT2" runat="server"></asp:TextBox>
                                 </td>
                             </tr>

                             <tr>
                                 <td>
                                     <asp:Label ID="lblVat" runat="server" Text="VAT Value"></asp:Label>
                                 </td>

                                 <td>
                                        <asp:TextBox ID="txtVAT" runat="server"></asp:TextBox>
                                 </td>
                             </tr>

                              <tr>
                                  <td>
                                  </td>
                                 <td>
                                     <asp:Button ID="btnSaveNBTVAT" runat="server" Text="Save" class="btn btn-info pull-right " onclick="btnSaveNBTVAT_Click" ></asp:Button>
                                 </td>
                             </tr>
                         
                         </table>

                </tr>

                         
                      </asp:Panel>
                  </div>
                  </td>
                
                </tr>





                <tr>
                 
                  <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-info" 
                          onclick="btnSave_Click"></asp:Button>
                  </td>
                </tr>
                </tbody>
              </table>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
          </div>
         
    </div>
    </section>
    </form>
    </div>
    </div>
    </div>

</asp:Content>
