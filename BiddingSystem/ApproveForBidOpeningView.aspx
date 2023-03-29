<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ApproveForBidOpeningView.aspx.cs" Inherits="BiddingSystem.ApproveForBidOpeningView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
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
</style>
  <style type="text/css">
     .TestTable {
         font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
         border-collapse: collapse;
         width: 100%;
     }
     
     .TestTable td, #TestTable th {
         border: 1px solid #ddd;
         padding: 8px;
     }
     
     .TestTable tr:nth-child(even){background-color: #f2f2f2;}
     
     .TestTable tr:hover {background-color: #ddd;}
     
     .TestTable th {
         padding-top: 12px;
         padding-bottom: 12px;
         text-align: left;
         background-color: #4CAF50;
         color: white;
     }
  </style>
  <style type="text/css">
     #myModal .modal-dialog {
       width: 60%;
     }
     #myModal2 .modal-dialog {
       width: 70%;
     }
     #myModal3 .modal-dialog {
       width: 90%;
     }
  </style>
  <style type="text/css">
        input[type="file"]
        {
            display: block;
        }
        .imageThumb
        {
            max-height: 75px;
            border: 2px solid;
            margin: 10px 10px 0 0;
            padding: 1px;
        }
   </style>
 <section class="content-header">
      <h1>
       Approve for Bid Opening
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active"> Approve for Bid Opening</li>
      </ol>
    </section>
    <br />

    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="sm"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server">
            <ContentTemplate>

                <div id="SuccessAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #3c8dbc;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">Success</h4>
                            </div>
                            <div class="modal-body">
                                <p id="successMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-info" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="modalSelectCheckBox" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #e66657">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h2 id="lblTitle" style="color: white;" class="modal-title">Alert!!</h2>
                            </div>
                            <div class="modal-body" style="background-color: white">
                                <h4>Atleast select one Item for Bid Opening</h4>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button id="btnOkAlert" type="button" class="btn btn-danger">OK</button>
                            </div>
                        </div>
                    </div>
                </div>


                <div id="modalRejectSubmitCheckBox" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #e66657">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h2 id="lblTitleR" style="color: white;" class="modal-title">Alert!!</h2>
                            </div>
                            <div class="modal-body" style="background-color: white">
                                <h4>Atleast select one Item for Reject Bid Opening</h4>
                            </div>
                            <div class="modal-footer" style="background-color: white">
                                <button id="btnOkAlertR" type="button" class="btn btn-danger">OK</button>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1;"><span aria-hidden="true" style="opacity: 1; color: white;">×</span></button>
                                <h4 class="modal-title">Attachment</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive TestTable" Style="border-collapse: collapse; color: black;"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PR Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FilePath" HeaderText="File Path" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileName" HeaderText="File Name" />

                                                        <asp:TemplateField HeaderText="View">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnEdit" ImageUrl="~/images/view-icon-614x460.png" Style="width: 39px; height: 26px"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnCancelRequest" ImageUrl="~/images/Downloads2.png" Style="width: 26px; height: 20px; margin-top: 4px;"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div>
                                            <label id="lbMailMessage" style="margin: 3px; color: maroon; text-align: center;"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModal2" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;"><span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">BOM (Bill of Material)</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive TestTable" Style="border-collapse: collapse; color: black;"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="SeqId" HeaderText="Seq Id" />
                                                        <asp:BoundField DataField="Meterial" HeaderText="Material" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div>
                                            <label id="Label1" style="margin: 3px; color: maroon; text-align: center;"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModal3" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;"><span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Settings For :&nbsp;&nbsp;&nbsp;<asp:Label ID="lblItemNAme" runat="server" Text=""></asp:Label></h4>

                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvSettingsTableModal" runat="server" CssClass="table table-responsive TestTable" Style="border-collapse: collapse; color: black;"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="StartDate" HeaderText="Bid Start Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                                        <asp:BoundField DataField="EndDate" HeaderText="Bid End Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                                        <asp:BoundField DataField="BidOpeningPeriod" HeaderText="Days" DataFormatString="{0:F0}" />
                                                        <asp:BoundField DataField="CanOverride" HeaderText="Can Override" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Can Override">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("CanOverride").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BidOnlyRegisteredSupplier" HeaderText="Bid Only Registered Supplier" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Bid Only Registered Supplier">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("BidOnlyRegisteredSupplier").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ViewBidsOnlineUponPrCreation" HeaderText="View Bids Online Upon PRCreation" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="View Bids Online Upon PR Creation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("ViewBidsOnlineUponPrCreation").ToString()=="1"?"Yes":"No"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BidTermsAndConditions" HeaderText="Terms/Conditions" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div>
                                            <label id="Label2" style="margin: 3px; color: maroon; text-align: center;"></label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="myModalUploadedPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Item Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive TestTable" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Standard Images Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
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
                </div>

                <div id="myModalReplacementPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Replacement Item Images</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvReplacementPhotos" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Replacement Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
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
                </div>

                <div id="myModalSupportiveDocuments" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Supportive Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvSupportiveDocuments" runat="server" CssClass="table table-responsive TestTable" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Preview">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnViewUploadSupporiveDocument" OnClick="lbtnViewUploadSupporiveDocument_Click">View</asp:LinkButton>
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
                </div>

                <div id="modalApprove" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 id="lblTitleConfirmYesNo1" class="modal-title">Confirmation</h4>
                            </div>
                            <div class="modal-body">
                                <p>Are you sure to Approve Purchase Requisition?</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnYesConfirmYesNo" runat="server" CssClass="btn btn-primary" OnClick="btnApprove_Click" Text="Yes"></asp:Button>
                                <button id="btnNoConfirmYesNo1" type="button" class="btn btn-danger">No</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="modalReject" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 id="lblTitleConfirmYesNo2" class="modal-title">Confirmation</h4>
                            </div>
                            <div class="modal-body">
                                <p>Are you sure to Reject Purchase Requisition?</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" OnClick="btnReject_Click" Text="Yes"></asp:Button>
                                <button id="btnNoConfirmYesNo2" type="button" class="btn btn-danger">No</button>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- SELECT2 EXAMPLE -->
                <section class="content" style="position: relative; background: #fff; border: 1px solid #f4f4f4;">    
   <!-- Main content -->
      <!-- title row -->
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header">
            <i class="fa fa-copy"></i> Approval to View in Supplier Bidding Portal
            <small class="pull-right" style="color:Black;">Date:<asp:Label ID="lblDateNow" runat="server" Text=""></asp:Label></small>
          </h2>
        </div>
        <!-- /.col -->
      </div>
      <!-- info row -->
      <div class="row">
        <div class="col-sm-4" style=" margin-left: 16px; ">
          
          <address>
            <strong>Department</strong><br />
            User Department : &nbsp;&nbsp;&nbsp;<asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label><br />
            Our Ref.: &nbsp;&nbsp;&nbsp; <asp:Label ID="lblRef" runat="server" Text=""></asp:Label><br />
            PR. No : &nbsp;&nbsp;&nbsp;<asp:Label ID="lblPRCode" runat="server" Text=""></asp:Label><br />
            Date : &nbsp;&nbsp;&nbsp;<asp:Label ID="lblRequestedDate" runat="server" Text=""></asp:Label><br />
            <%--User Ref : <asp:Label ID="lblUserRef" runat="server" Text=""></asp:Label>--%>
          </address>
        </div>
        <!-- /.col -->
        <div class="col-sm-4 invoice-col">
          <address>
            <strong>Requester</strong><br />
          Name: <asp:Label ID="lblRequesterName" runat="server" Text=""></asp:Label>
          </address>
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
      <div class="panel-body" id="divBidForApproval" runat="server">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvPRView" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckUncheckCheckboxes(this);"/>
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />
            <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
            <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
             <asp:BoundField DataField="BiddingOrderId" HeaderText="Bidding Order Id" />
             <asp:TemplateField HeaderText="Replacement">
              <ItemTemplate>
                  <asp:Label ID="Label1" runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
           
              <asp:TemplateField HeaderText="Replacement Images">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnViewReplacementPhotos" OnClick="btnViewReplacementPhotos_Click" runat="server" Text="View"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
            
              <asp:TemplateField HeaderText="Standard Images">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnViewUploadPhotos" OnClick="btnViewUploadPhotos_Click" runat="server" Text="View"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

             <asp:TemplateField HeaderText="Item Specification">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblViewBom" Text="View" OnClick="btnBOM_Click"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Settings">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblViewSettings" Text="View" OnClick="lblViewSettings_Click"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Supportive Documents">
                   <ItemTemplate>
                       <asp:LinkButton ID="btnViewSupportiveDocuments" OnClick="btnViewSupportiveDocuments_Click1" runat="server" Text="View"/>
                   </ItemTemplate>
              </asp:TemplateField>
            <asp:BoundField ItemStyle-Width="200px" DataField="BidTypeMaualOrBid" HeaderText="Bid Type Manual/Online Bid" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:TemplateField HeaderText="Bid Type Manual/Online Bid">
              <ItemTemplate>
                  <asp:Label ID="Label22" runat="server" Text='<%# Eval("BidTypeMaualOrBid").ToString() =="1" ? "Supplier Online Bid":"Supplier Manual Bid" %>' Font-Bold="true" ForeColor='<%#Eval("BidTypeMaualOrBid").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
      <!-- this row will not appear when printing -->
      <div class="row no-print">
        <div class="col-xs-12">
        <%--  <a href="#" target="_blank" class="btn btn-success"><i class="fa fa-print"></i> Print</a>--%>
           <asp:Button id="btnRejectPR"  runat="server" class="btn btn-warning pull-right"  OnClientClick = "return validataAlFields(2);" Text="Reject PR" onclick="btnRejectPR_Click" ></asp:Button>
           <asp:Button id="btnApprove" runat="server"  class="btn btn-primary pull-right"  OnClick="btnApprove_Click"  OnClientClick = "return validataAlFields(1);" Text="Approve" style="margin-right: 10px;"></asp:Button>
           
        </div>

      </div>
      <br />
      <div id="rejectedReason" runat="server" visible="false">
      <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label for="exampleInputEmail1">Reason for Reject</label> 
                <asp:TextBox ID="txtRejectReason" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="" Text=""></asp:TextBox>
            </div>
        </div>
      </div>
      <div class="row">
      <div class="col-md-12">
        <div class="form-group">
        <asp:Button id="btnReject"  runat="server" class="btn btn-danger pull-right" OnClick="confirmationToReject_Click" Text="Reject" ></asp:Button>
        </div>
        </div>
        </div>
      </div>
      </section>

                <section class="content" style="position: relative; background: #fff; border: 1px solid #f4f4f4;"> 
        <div class="row" runat="server" id="divVisibity" visible="false">
        <div class="col-xs-12">
          <h2 class="page-header">
            <i class="fa fa-"></i> Appoved or Rejected Items
          </h2>
        </div>
        <!-- /.col -->
      </div>
        <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="GridView1" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="BiddingOrderId" HeaderText="Bidding Id" />
            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />
            <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
            <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
            <asp:BoundField DataField="ReasonToRejectBidOpening" HeaderText="Reason For Reject" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            
            <asp:TemplateField HeaderText="Reason For Reject">
                <ItemTemplate>
                    <asp:Label ID="txtApprovedBidOpening" Text='<%#Eval("ReasonToRejectBidOpening") %>' ForeColor='<%#Eval("ReasonToRejectBidOpening").ToString()!=""?System.Drawing.Color.Red:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
           <%-- <asp:BoundField DataField="Replacement" HeaderText="Replacement" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:TemplateField HeaderText="Replacement">
              <ItemTemplate>
                  <asp:Label ID="Label1" runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>--%>
            
             <asp:TemplateField HeaderText="Images">
                     <ItemTemplate>
                       <asp:LinkButton ID="btnViewUploadPhotosGv" OnClick="btnViewUploadPhotosGv_Click" runat="server" Text="View Photos"/>
                     </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField HeaderText="BOM">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblViewBomGV" Text="View BOM" OnClick="btnBOM_ClickGv"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Settings">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblViewSettingsGV" Text="View Settings" OnClick="lblViewSettings_ClickGv"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField ItemStyle-Width="200px" DataField="BidTypeMaualOrBid" HeaderText="Bid Type Manual /Online Bid" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:TemplateField HeaderText="Bid Type Maual/Online Bid">
              <ItemTemplate>
                  <asp:Label ID="Label22" runat="server" Text='<%# Eval("BidTypeMaualOrBid").ToString() =="1" ? "Supplier Online Bid":"Supplier Manual Bid" %>' Font-Bold="true" ForeColor='<%#Eval("BidTypeMaualOrBid").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
             <asp:BoundField DataField="IsApproveToViewInSupplierPortal" HeaderText="IsApproveToViewInSupplierPortal" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderText="Bid Status">
                <ItemTemplate>
                    <asp:Label ID="txtApprovedSupplierPortal" Text='<%#Eval("IsApproveToViewInSupplierPortal").ToString()=="1" || Eval("IsApproveToViewInSupplierPortal").ToString()=="4"?"Bid Opened":"Bid Rejected" %>' ForeColor='<%#Eval("IsApproveToViewInSupplierPortal").ToString()=="1"|| Eval("IsApproveToViewInSupplierPortal").ToString()=="4"?System.Drawing.Color.Green:System.Drawing.Color.Red %>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </section>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
     <script language = "javascript" type = "text/javascript">
         function CheckUncheckCheckboxes(CheckBox) {
             //get target base & child control.
             var TargetBaseControl = document.getElementById('<%= this.gvPRView.ClientID %>');
             var TargetChildControl = "CheckBox1";

             //get all the control of the type INPUT in the base control.
             var Inputs = TargetBaseControl.getElementsByTagName("input");

             for (var n = 0; n < Inputs.length; ++n)
                 if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                     Inputs[n].checked = CheckBox.checked;
                 }
                 else {

                 }
         }
   </script>

    <script>
function validataAlFields(status) {
    var isValid = false;
    var count = 0;
    var gridView = document.getElementById('<%= gvPRView.ClientID %>');
    for (var i = 1; i < gridView.rows.length; i++) {
        var inputs = gridView.rows[i].getElementsByTagName('input');
        if (inputs != null && inputs[0] != null) {
            if (inputs[0].type == "checkbox") {
                if (inputs[0].checked) {
                    count++;
                    
                }
            }
        }
    }

    if (count > 0) {
        if (status == 1) {
            return true;
        }
        if (status == 2) {
            $('#modalReject').modal('show');
            return false;
          
        }
        
    }

    else {
        if (status == 1)
        {
            $('#modalSelectCheckBox').modal('show');
        }
        if (status == 2) {
            $('#modalRejectSubmitCheckBox').modal('show');
        }
      
        return false;
    }
}

        $("#btnOkAlert").click(function () {
            $('#modalSelectCheckBox').modal('hide');
        });

        $("#btnOkAlertR").click(function () {
            $('#modalRejectSubmitCheckBox').modal('hide');
        });
        $("#btnNoConfirmYesNo2").click(function () {
            $('#modalReject').modal('hide');
        });
        
        
    </script>



</asp:Content>
