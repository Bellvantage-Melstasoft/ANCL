<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyComparisionSheet.aspx.cs" Inherits="BiddingSystem.CompanyComparisionSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        #myModal .modal-dialog {
            width: 90%;
        }

        #myModal2 .modal-dialog {
            width: 50%;
        }
    </style>

    <form id="Form1" runat="server">
        <section class="content-header">
    <h1>
       Monitor Bids
        <small></small>
      </h1>
    <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li><a href="CompanyBidClosed.aspx">Monitor Bids</a></li>
        <li class="active">Company Bid Comparison</li>
      </ol>
    </section>
        <br />

        <section class="content">
      <center>
       <div id="modalConfirmYesNo" class="modal fade">
        <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-danger" style=" background-color: #d73925; ">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 style="text-align:left;color: White; font-weight:bold" id="lblTitleConfirmYesNo" class="modal-title" >Alert</h4>
            </div>
            <div class="modal-body" style="text-align:left">
                <p >Already selected a supplier to this item ?</p>
            </div>
            <br />
            <div class="modal-footer">
                <%-- <asp:Button ID="btnYesConfirmYesNo" runat="server"  CssClass="btn btn-primary"  OnClick="btnSavePR_Click" Text="Yes" ></asp:Button>--%>
                <button id="btnNoConfirmYesNo"  type="button" class="btn btn-danger" >Ok</button>
            </div>
        </div>
     </div>
   </div>


    <div id="modalForRejectReason" class="modal fade">
        <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-danger" style=" background-color: #d73925; ">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 style="text-align:left;color: White; font-weight:bold" id="H1" class="modal-title" >Alert</h4>
            </div>
            <div class="modal-body" style="text-align:left">
                 <div class="col-md-12">
                     <div class="form-group" style="text-align:left;">
                     <label for="exampleInputEmail1">Reason For Reject</label>
                     <asp:TextBox ID="txtRejectedReason" TextMode="MultiLine" runat="server" placeholder = "Reason For Reject" CssClass="form-control"></asp:TextBox>
                     </div>
                </div>
            </div>
            <br />
            <div class="modal-footer">
                 <asp:Button ID="btnRejection" runat="server"  CssClass="btn btn-primary"  OnClientClick="GetDetails()" Text="Yes" ></asp:Button>
                <button id="btnRejectionCancel"  type="button" class="btn btn-danger" >Cancel</button>
            </div>
        </div>
     </div>
   </div>


   <div id="Div2" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header"  style="background-color:#d73925;">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4  class="modal-title pull-left" style=" color: white; font-weight: bold; ">Alert</h4>
            </div>
            <div class="modal-body">
                <p style="font-weight:bold;  font-size:medium;" class="pull-left">Select a supplier first.</p>
            </div>
            <br />
            <div class="modal-footer">
            <span class="btn btn-danger"  data-dismiss="modal" aria-label="Close" id="Span1">OK</span>
            </div>
        </div>
    </div>
  </div>

   <div id="modalDeleteYesNo" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color: #3c8dbc;">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span> 
                </button>
                <h4 id="lblTitleDeleteYesNo" class="modal-title pull-left" style="color:White; font-weight:bold;">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p class="pull-left">Are you sure to Reset supplier ?</p>
            </div>
            <br />
            <div class="modal-footer">
                 <asp:Button ID="btnDelete" runat="server"  CssClass="btn btn-primary"  OnClick="lblResetSupplier_Click" Text="Yes" ></asp:Button>
                <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>
  

  <!-- Modal -->
      <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header" style="background-color:#3c8dbc">
          <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span>
          </button>
          <h4 class="modal-title" id="myModalLabel"  style="color:White;font-weight:bold;">Bids For <asp:Label ID="lblItemName" runat="server" Text="" style="color:White;font-weight:bold;"></asp:Label></h4>
        </div>
        <asp:ScriptManager ID="ContentScriptManager" runat="server" />
        <asp:UpdatePanel ID="ContentUpdatePanel" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
        <div class="modal-body">
         <div class="row">
            <div class="col-md-12">
                <div class="row">
 
                <div class="col-md-6">

                    <div class="form-group">
                                         
                                        <div class="row">
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <label >Item Name        :</label>
                                            </div>
                                            <div class="col-md-7 " style="text-align:left;">
                                               <asp:Label ID="lblItem" runat="server" Text="" ></asp:Label>
                                            </div>
                                         </div>
                                         <div class="row">
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <label >Item Description :</label>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                 <asp:Label ID="lblItemDes" runat="server" Text="" ></asp:Label>
                                            </div>
                                         </div>
                                        
                                        <div class="row">
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <label >Quantity         :</label>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                 <asp:Label ID="lblQty" runat="server" Text="" ></asp:Label>
                                            </div>
                                        </div>
                                      <%--  <div class="row">
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <label >Previous Purchased History</label>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <label><a href="#"  data-toggle="modal" data-target="#myModal"  >View Purchase History</a></label>
                                            </div>
                                        </div>--%>

                                         <div class="row">
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <label >Bid Submitted Supplier Count : </label>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                 <asp:Label ID="lblBidSubmittedSupplierCount" runat="server" Text=""  style="color:Red"></asp:Label>
                                            </div>
                                        </div>

                                         <div class="row" style ="visibility:hidden;">
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <label >Item Id</label>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                 <asp:Label ID="lblItemids" runat="server" Text=""></asp:Label>
                                            </div>
                                         </div>
                                         <div class="row" style ="visibility:hidden;">
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <label >Pr Id</label>
                                            </div>
                                            <div class="col-md-3 col-sm-6 col-xs-12" style="text-align:left;">
                                                <asp:Label ID="lblPr" runat="server" Text=""></asp:Label>
                                              
                                            </div>
                                         </div>
                                    </div>

                </div>
               
                 <div class="col-md-4">
                 <div class="row">
                 <div class="col-md-12">
                    <label style=" margin-left: -195px; ">Previouse Purchased History</label>
                 </div>
                 <div class="col-md-12">
                    <div class="form-group">
                                           
                                      <asp:GridView ID="gvLastPurchase" AutoGenerateColumns="false" EmptyDataText="No Previous Order Found" GridLines="None" CssClass="table table-responsive table-hover" runat="server"> 
                                      <Columns>
                                          <asp:BoundField  DataField="supplierName" HeaderText="Previous Supplier" />
                                           <asp:BoundField  DataField="itemPrice" HeaderText="Item Price" />
                                          
                                          <asp:TemplateField HeaderText="Rate">
                                              <ItemTemplate>
                                                  <asp:Label runat="server" ID="lblratingStar" Text='<%#Eval("RatingStar")%>' ForeColor="Red" Font-Size="Large" ></asp:Label>
                                              </ItemTemplate>
                                          </asp:TemplateField>
                                      </Columns>
                                      </asp:GridView>
                                       
                                    </div>
                </div>
                </div>
                </div>
                     <div class="col-md-1"></div>
                     </div>
            </div>         
          </div>

         <div class="co-md-12">
         <div class="table-responsive">
       <asp:GridView ID="gvSupplier" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField  DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="SupplierName" HeaderText="Supplier Name" />
            <asp:BoundField  DataField="PerItemPrice" HeaderText="Per Item Price" />
            <asp:TemplateField HeaderText="Negotiable Item Amount">
            <ItemTemplate>
            <asp:TextBox ID="txtCuztomizeAmount" type="number" runat="server" ToolTip=' <%# Eval("CustomizeAmount") %>' autocomplete="off" Text=' <%# Eval("PerItemPrice") %>' ForeColor="Red" CssClass="txtCuztomizeAmount" style="text-align:right;"></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Negotiable NBT" HeaderStyle-Font-Bold="true"  ItemStyle-HorizontalAlign="Center">
              <ItemTemplate>
                  <asp:TextBox CssClass="NNbt" runat="server" ID="txNNbt" Text=' <%# Eval("Nbt") %>' ForeColor="Red" Width="90%" Enabled="false" ></asp:TextBox>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Negotiable Vat" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" >
              <ItemTemplate>
                  <asp:TextBox CssClass="NVat" runat="server" ID="txtNVat" Text=' <%# Eval("Vat") %>' ForeColor="Red" Width="90%"  Enabled="false"></asp:TextBox>
              </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="Negotiable Total" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Black" ItemStyle-HorizontalAlign="Center" >
              <ItemTemplate>
                  <asp:TextBox CssClass="NTotal" runat="server" ID="txtNTotal" Text=' <%# Eval("TotalPrice") %>' ForeColor="Red" Enabled="false"></asp:TextBox>
              </ItemTemplate>
             </asp:TemplateField>
            <asp:BoundField  DataField="ItemQuantity" HeaderText="Quantity" />
            <asp:BoundField  DataField="Amount" HeaderText="Amount" DataFormatString="{0:F}"/>
            <asp:BoundField  DataField="Nbt" HeaderText="NBT" DataFormatString="{0:F}"/>
            <asp:BoundField  DataField="Vat" HeaderText="VAT" DataFormatString="{0:F}"/>
            <asp:BoundField  DataField="TotalPrice" HeaderText="TotalPrice" DataFormatString="{0:F}"/>
            <asp:BoundField  DataField="BidOpeningId" HeaderText="BidOpeningId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField >
              <ItemTemplate>
                  <asp:Button CssClass="btn btn-primary" runat="server" ID="btnSelect" Text="Select" Visible='<%#Eval("DefaultSupplier").ToString() == "1" ? true :false %>' OnClientClick="GetSelectedSupplierId()" ></asp:Button>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
              <ItemTemplate>
                  <asp:Button CssClass="btn btn-danger" runat="server" ID="btnReject" Text="Reject" Visible='<%#Eval("DefaultSupplier").ToString() == "1" ? true :false %>'  OnClientClick="GetRejectedSupplierId();" ></asp:Button>
              </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField  DataField="isVatIncluded"   HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
        </Columns>
    </asp:GridView>
    </div>
         </div>

                <div class="row">
     
                <div class="col-md-6"  style="visibility:hidden"> 
                <div class="col-md-6" style=" text-align: left; ">
                 <div class="form-group">
                <label for="exampleInputEmail1">Reject</label>
                <div class="input-group">
                        <span class="input-group-addon">
                        <asp:RadioButton ID="rdoEnable" runat="server" GroupName="RegularMenu" value='Yes' ></asp:RadioButton>
                        </span>
                        <asp:TextBox ID="txtEnable" runat="server" class="form-control" Text="Yes"></asp:TextBox>
                  </div>
                  </div>
                  </div>
                <div class="col-md-6">
                 <div class="form-group">
                <label for="exampleInputEmail1" style="visibility:hidden">Replacement</label>
                     <div class="input-group">
                        <span class="input-group-addon">
                          <asp:RadioButton ID="rdoDisable" runat="server" GroupName="RegularMenu" value='No' Checked ></asp:RadioButton>
                        </span>
                     <asp:TextBox ID="txtDisable" runat="server" class="form-control" Text="No"></asp:TextBox>
                  </div>
                  </div>
                </div>
                </div>

           <div class="row" runat="server" id="divRejectReason">
                <div class="col-md-4" id="ReasonForRejectSupplier">
                     <div class="form-group" style="text-align:left;">
                     <label for="exampleInputEmail1">Reason For Reject</label>
                     <asp:TextBox ID="txtReason" TextMode="MultiLine" runat="server" placeholder = "Reason For Reject" CssClass="form-control"></asp:TextBox>
                     </div>
                </div>
              
              <div class="col-md-1" >
                    <div class="form-group" style="text-align:left;padding-left: 15px;">
                    <asp:Button ID="btnNext" runat="server" Text="Next Supplier" class="btn btn-success" OnClientClick="GetDetails()" ></asp:Button>
                     </div>
                </div>

                </div>
           
               </div>
        </div>
         </ContentTemplate>
         <Triggers>
         <asp:AsyncPostBackTrigger ControlID="gvSupplier" />
         </Triggers>
         </asp:UpdatePanel>
       
      </div>
    </div>
  </div>
  
      <div id="SuccessAlert" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header"  style="background-color:#3c8dbc;">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4  class="modal-title pull-left" style=" color: white; font-weight: bold; ">Alert</h4>
            </div>
            <div class="modal-body">
                <p style="font-weight:bold;  font-size:medium;" class="pull-left">Supplier selected successfully.</p>
            </div>
            <br />
            <div class="modal-footer">
            <span class="btn btn-info"  data-dismiss="modal" aria-label="Close" id="okbtn" onclick="ClientClickSelectSupplier()">OK</span>
            </div>
        </div>
    </div>
</div>

   <div id="Div3" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header"  style="background-color:#3c8dbc;">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4  class="modal-title pull-left" style=" color: white; font-weight: bold; ">Alert</h4>
            </div>
            <div class="modal-body">
                <p style="font-weight:bold;  font-size:medium;" class="pull-left">Supplier has been Rejected successfully.</p>
            </div>
            <br />
            <div class="modal-footer">
            <span class="btn btn-info"  data-dismiss="modal" aria-label="Close" id="Span2" onclick="ClientClickSelectSupplier()">OK</span>
            </div>
        </div>
    </div>
</div>

      </center>
     <div class="box box-info" id="divItems" runat="server" >
        <div class="box-header with-border">
          <h3 class="box-title" >Bid Completed Item Details (Bid Comparison)</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
         <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="GridView1" runat="server" CssClass="table table-responsive tablegv" EmptyDataText="No Records Found"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField  DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="PrID" HeaderText="PrID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField  DataField="ItemQuantity" HeaderText="Quantity" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="Total" HeaderText="Bid Submitted Suppliers" />
             <asp:BoundField  DataField="selectSupplier"   HeaderText="selectSupplier" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:BoundField  DataField="poRaisedSq"  HeaderText="poRaisedSq" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             
               <asp:TemplateField HeaderText="Status of Supplier Selected">
                <ItemTemplate>
                    <asp:Label ID="txtApproved" Text='<%#Eval("selectSupplier").ToString()=="1"?"Supplier Selected":"Pending" %>' ForeColor='<%#Eval("selectSupplier").ToString()=="1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Select Supplier">
                <ItemTemplate>
                    <asp:LinkButton ID="lblChooseSupplier" Enabled='<%#Eval("selectSupplier").ToString()=="1"?false:true %>' OnClick="lblChooseSupplier_Click" ForeColor='<%#Eval("selectSupplier").ToString()=="1"?System.Drawing.Color.LightGray:System.Drawing.Color.Blue %>' runat="server">View Supplier Bids</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            
             <%--<asp:TemplateField HeaderText="Reset Supplier">
               <ItemTemplate>
                   <asp:ImageButton ID="lblResetSupplier" ImageUrl="~/images/delete.png" CssClass="deleteUserAccess"  style="width:26px;height:20px;" runat="server" />
               </ItemTemplate>
             </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="Reset Supplier" HeaderStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:LinkButton style="visibility:hidden;" ID="lblResetSupplier" Enabled='<%#Eval("selectSupplier").ToString()=="0"?false:true %>' CssClass="deleteUserAccess" ForeColor='<%#Eval("selectSupplier").ToString()=="0"?System.Drawing.Color.LightGray:System.Drawing.Color.Blue %>'   runat="server" Text="Reset Supplier"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField  DataField="BidOpeningId" HeaderText="BidOpeningId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:BoundField  DataField="SupplierRejected" HeaderText="SupplierRejected" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
            
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
        <!-- /.box-body -->
         <div class="modal-footer">
          <%--<asp:Button ID="btnProceedPO" runat="server" CssClass="btn btn-info " 
                 Text="OK" style="margin-left: 4px;" onclick="btnProceedPO_Click"></asp:Button>--%>
          <%--<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger pull-right" Text="Cancel" 
                 onclick="btnCancel_Click"></asp:Button>--%>
        </div>
      </div>
  
     <div class="box box-info" id="div1" runat="server" >
        <div class="box-header with-border">
          <h3 class="box-title" >PO Submitted Items</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
       <asp:GridView ID="gvSubmittedPO" runat="server" CssClass="table table-responsive tablegv" EmptyDataText="No Records Found"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField  DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="PrID" HeaderText="PrID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="ItemName" HeaderText="Item Name" />            
            <asp:BoundField  DataField="ItemQuantity" HeaderText="Quantity" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="RejCount" HeaderText="Rejected Times" />
            <asp:BoundField  DataField="selectSupplier"   HeaderText="selectSupplier" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField  DataField="poRaisedSq"  HeaderText="poRaisedSq" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:BoundField  DataField="isPoApprovedSq"  HeaderText="isPoApprovedSq" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderText = "Status">
            <ItemTemplate>
            <asp:Label runat="server" Text='<%#Eval("selectSupplier").ToString()=="1" && Eval("poRaisedSq").ToString()=="1"? "Pending Approval":"Rejeted"  %>' ForeColor='<%#Eval("selectSupplier").ToString()=="1" && Eval("poRaisedSq").ToString()=="1"?System.Drawing.Color.Orange:System.Drawing.Color.Red %>'></asp:Label>
            </ItemTemplate>

            </asp:TemplateField>
            <asp:BoundField  DataField="BidOpeningId" HeaderText="BidOpeningId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
           
           <%-- <asp:TemplateField HeaderText="Action">
             <ItemTemplate>
                 <asp:LinkButton ID="lblRejectPO" OnClick="lblRjtPO_Click" runat="server">Reject Item From PO</asp:LinkButton>
             </ItemTemplate>
            </asp:TemplateField>--%>
            
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
        <!-- /.box-body -->
      </div>
     </section>
        <asp:HiddenField ID="hdnCustomizedAmount" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnCustomizedVat" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnCustomizedNbt" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnCustomizedTotalAmount" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnitemId" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnPrId" runat="server"></asp:HiddenField>

        <asp:HiddenField ID="hdnModalPrId" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnModalItemId" runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnModalSupplierId" runat="server"></asp:HiddenField>

    </form>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#ReasonForRejectSupplier').hide();
            $('#ContentSection_btnNext').hide();

            $('#ContentSection_rdoEnable').change(function () {
                $('#ReasonForRejectSupplier').show();
                $('#ContentSection_btnNext').show();
            });

            $('#ContentSection_rdoDisable').change(function () {
                $('#ReasonForRejectSupplier').hide();
                $('#ContentSection_btnNext').hide();
            });
        });
    </script>

    <script type="text/javascript">

        $(".txtCuztomizeAmount").keyup(function () {
            var rQty = $(this).parent().next().next().next().next().html();
            var price = $(this).val();
            var isVat = $(this).parent().next().next().next().next().next().next().next().next().next().next().next().next().html();
            if (isVat == 1) {
                //alert(rQty);
                var nbt = rQty * price * 2 / 98;
                var vat = ((rQty * price) + nbt) * 0.15;
                var total = (rQty * price) + nbt + vat

                $(this).parent().next().find(".NNbt").val(nbt.toFixed(2));
                // $(this).parent().next().next().next().html(nbt.toFixed(2));
                $(this).parent().next().next().find(".NVat").val(vat.toFixed(2));
                $(this).parent().next().next().next().find(".NTotal").val(total.toFixed(2));
            }
            else {
                var nbt = 0;
                var vat = 0;
                var total = (rQty * price) + nbt + vat

                $(this).parent().next().find(".NNbt").val(nbt.toFixed(2));
                // $(this).parent().next().next().next().html(nbt.toFixed(2));
                $(this).parent().next().next().find(".NVat").val(vat.toFixed(2));
                $(this).parent().next().next().next().find(".NTotal").val(total.toFixed(2));

            }

        });
    </script>

    <script type="text/javascript">
        function GetDetails() {
            var gridView = document.getElementById('<%= gvSupplier.ClientID %>');
          var cell = gridView.rows[1].cells[0];
          var SupplierId = cell.childNodes[0];
          var x = SupplierId;
          var ItemId = $('#ContentSection_lblItemids').text();
          var PrID = $('#ContentSection_lblPr').text();
          var Reason = $('#ContentSection_txtReason').val();
          var BidOpeningId = gridView.rows[1].cells[9].childNodes[0];

          var customAmount;


          for (var i = 0; i < gridView.rows.length - 1; i++) {
              var CustomizeAmount = $("input[id*=txtCuztomizeAmount]")
              if (CustomizeAmount[i].value != '') {
                  customAmount = (CustomizeAmount[i].value);
              }


          }

          var supplierQuotation = {};
          supplierQuotation.SupplierId = parseInt(x.textContent);
          supplierQuotation.ItemId = ItemId;
          supplierQuotation.PrID = PrID;
          supplierQuotation.Reason = Reason;
          supplierQuotation.BidOpeningId = BidOpeningId.data;
          supplierQuotation.CustomizeAmount = customAmount;


          $.ajax({
              type: "POST",
              url: "CompanyComparisionSheet.aspx/UpdateRejectedSuppliers",
              data: '{supplierQuotation: ' + JSON.stringify(supplierQuotation) + '}',
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (response) {
                  $('#Div3').modal('show');
                  event.preventDefault();
                  return false;
                  //alert("User has been added successfully.");
                  //window.location.reload();
              }
          });
          event.preventDefault();
          return false;
      }


      function GetSelectedSupplierId() {
          debugger;
          var gridView = document.getElementById('<%= gvSupplier.ClientID %>');
          var cell = gridView.rows[1].cells[0];
          var SupplierId = cell.childNodes[0];
          var BidOpeningId = gridView.rows[1].cells[9].childNodes[0];
          var x = SupplierId;
          var ItemId = $('#ContentSection_lblItemids').text();
          var PrID = $('#ContentSection_lblPr').text();


          var customAmount;
          var nbtAmount;
          var vatAmount;
          var totalAmount;

          for (var i = 0; i < gridView.rows.length - 1; i++) {
              var CustomizeAmount = $("input[id*=txtCuztomizeAmount]")
              if (CustomizeAmount[i].value != '') {
                  customAmount = (CustomizeAmount[i].value);
              }

              var NbtAmount = $("input[id*=txNNbt]")
              if (NbtAmount[i].value != '') {
                  nbtAmount = (NbtAmount[i].value);
              }

              var VatAmount = $("input[id*=txtNVat]")
              if (VatAmount[i].value != '') {
                  vatAmount = (VatAmount[i].value);
              }

              var TotalAmount = $("input[id*=NTotal]")
              if (TotalAmount[i].value != '') {
                  totalAmount = (TotalAmount[i].value);
              }





              var supplierQuotation = {};
              supplierQuotation.SupplierId = parseInt(x.textContent);
              supplierQuotation.ItemId = ItemId;
              supplierQuotation.PrID = PrID;
              supplierQuotation.BidOpeningId = BidOpeningId.data;
              supplierQuotation.CustomizeAmount = customAmount;

              supplierQuotation.Nbt = nbtAmount;
              supplierQuotation.Vat = vatAmount;
              supplierQuotation.TotalPrice = totalAmount;

              $.ajax({
                  type: "POST",
                  url: "CompanyComparisionSheet.aspx/GetSupplierId",
                  data: '{supplierQuotation: ' + JSON.stringify(supplierQuotation) + '}',
                  contentType: "application/json; charset=utf-8",
                  dataType: "json",
                  success: function (response) {
                      $('#SuccessAlert').modal('show');
                      // window.location.href = "CompanyComparisionSheet.aspx?PrId=" + PrID;
                      event.preventDefault();
                      return false;
                  }
              });
              event.preventDefault();
              return false;
          }
      }
    </script>

    <script>
        function GetRejectedSupplierId() {

            $('#divRejectReason').show();
            return false;
        }
    </script>


    <script type="text/javascript">
        function ClientClickSelectSupplier() {
            var PrID = $('#ContentSection_lblPr').text();
            window.location.href = "CompanyComparisionSheet.aspx?PrId=" + PrID;
        }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            var icount = 0;
            var vatValidate = false;
            var gv = document.getElementById("<%= GridView1.ClientID %>");

            for (var i = 1; i < gv.rows.length; i++) {
                var row = gv.rows[i];
                var targetcell = row.cells[5];
                var ApproveStatus = targetcell.innerHTML;


                if (ApproveStatus == 1) {
                    icount++;
                }
            }

            if ((icount) >= 1) {
                $('#ContentSection_btnProceedPO').attr('disabled', false);

            }
            else {
                $('#ContentSection_btnProceedPO').attr('disabled', true);
            }
        });
    </script>
    <script type="text/javascript">

        $("#btnNoConfirmYesNo").on('click').click(function () {
            var $confirm = $("#modalConfirmYesNo");
            $confirm.modal('hide');
            return this.false;
        });


        $("#btnRejectionCancel").on('click').click(function () {
            var $confirm = $("#modalForRejectReason");
            $confirm.modal('hide');
            return this.false;
        });

        $("#btnCancelSupplieModal").on('click').click(function () {
            var $confirm = $("#myModal");
            $confirm.modal('hide');
            return this.false;
        });

        function hideDeleteModal() {
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('hide');
            return this.false;
        }
        function showDeleteModal() {
            $("#<%=hdnitemId.ClientID%>").val();
            $("#<%=hdnPrId.ClientID%>").val();
            var $confirm = $("#modalDeleteYesNo");
            $confirm.modal('show');
            return this.false;
        }
    </script>

    <script type="text/javascript">
        $(".deleteUserAccess").click(function () {
            var itemid = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().prev().html();
            var prid = $(this).parent().prev().prev().prev().prev().prev().prev().prev().prev().html();
            var selectSupplier = $(this).parent().prev().prev().prev().prev().html();
            var rejectedSupplierCount = $(this).parent().next().next().html();

            $("#<%=hdnitemId.ClientID%>").val(itemid);
            $("#<%=hdnPrId.ClientID%>").val(prid);

            if (selectSupplier == "1" || parseInt(rejectedSupplierCount) > 0) {
                showDeleteModal();
            }
            if (selectSupplier == "0" && parseInt(rejectedSupplierCount) == 0) {
                // $('#Div2').modal('show');
                // window.location.href = "CompanyComparisionSheet.aspx?PrId=" + PrID;
                event.preventDefault();
                return false;
            }
            event.preventDefault();
            return false;
        });
    </script>

    <script>
        $(function () {
            $(".txtCuztomizeAmount").keypress(function () {
                if (event.keyCode != 69 && event.keyCode != 101 && event.keyCode != 45 && event.keyCode != 43 && event.keyCode != 42) {
                } else {
                    return false;
                }
            });
        });
    </script>

</asp:Content>
