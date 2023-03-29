<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ViewPO.aspx.cs" Inherits="BiddingSystem.ViewPO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <style type="text/css">
        body {
        }

        .tablegv {
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

      
            

            .tablegv td, .tablegv th {
                border: 1px solid #ddd;
                padding: 8px;
                 color:black;
            }

            .tablegv tr:nth-child(even) {
                background-color: #f2f2f2;
                color:black;
            }

            .tablegv tr:hover {
                background-color: #ddd;
                color:black;
            }

            .tablegv th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #3C8DBC;
                color: white;
            }

        .successMessage {
            color: #1B6B0D;
            font-size: medium;
        }

        .failMessage {
            color: #C81A34;
            font-size: medium;
        }

        table, td, tr {
            border-color: black;

        }
        .margin{
            margin-top:5px;
        }
    </style>
    <%--<div class="row">
        <div class="col-xs-12">
            <img src="AdminResources/images/logo.png" class="center-block" />
          <h2 class="page-header" style="text-align:center;">
            <i class="fa fa-envelope"></i> PURCHASE ORDER (PO)
          </h2>
        </div>
        <!-- /.col -->
      </div>--%>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <section class="content-header">
    <h1>
      View Purchase Order
        <small></small>
      </h1>
      <ol class="breadcrumb">
          
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">View Purchase Order </li>
      </ol>
    </section>
    <br />

    <form runat="server" id="form1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
        <section class="content" style="padding-top: 0px">


            
                    <div id="mdlFollowUpRemark" class="modal modal-primary fade" tabindex="-1"  role="dialog" aria-hidden="true" >
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Follow Up Remarks</h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                        <div class="col-md-12">
                                            <label style="color:black">Remark</label>
                                             <asp:TextBox ID="txtFollowUpRemark" runat="server" TextMode="MultiLine" Rows="4" Width =" 100%" style="color:black; margin-bottom:20px"></asp:TextBox>
                                            </div>
                                    </div>
                                <div class="row">
                                        <div class="col-md-12">
                                             <asp:Button runat="server" CssClass="btn btn-success pull-right " Text="Save Remark" id="btnSave" style="margin-bottom:10px" OnClick ="btnSave_Click" OnClientClick="RemoveBackdrop()"/>
               
                                            </div>
                                    </div>
                                <div class="login-w3ls">
                                     
                                    <div class="row">
                                        <div class="col-md-12">
                                             <div class="table-responsive">
                                            <asp:GridView runat="server" ID="gvRemarks" AutoGenerateColumns="false" EmptyDataText="No Records Found"
                                                CssClass="table table-responsive tablegv" EnableViewState="true">
                                                <Columns>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="PoId" HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="Remark" HeaderText="Remark" />
                                                    <asp:BoundField DataField="RemarkDate" HeaderText="Remark Date" DataformatString="{0:dd-MMMM-yyyy}" />
                                                    <asp:BoundField DataField="UserName" HeaderText="Remark By" />
                                                    <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:Button runat="server" CssClass="btn btn-danger btn-xs " Text="Delete" id="btnDelete"   OnClientClick='<%#"DeleteRemark(event,"+Eval("Id").ToString()+")" %>' />
               
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



             <div id="modalSelectCheckBox" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#e66657">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h2 id="lblTitle" style="color:white;" class="modal-title">Alert!!</h2>
            </div>
            <div class="modal-body" style="background-color:white">
                <h4>Atleast select one Item to Approve PO</h4>
            </div>
            <div class="modal-footer" style="background-color:white">
                <button id="btnOkAlert"  type="button" class="btn btn-danger" >OK</button>
            </div>
        </div>
    </div>
</div>
          <div id="modalRejectSubmitCheckBox" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color:#e66657">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h2 id="lblTitleR" style="color:white;" class="modal-title">Alert!!</h2>
            </div>
            <div class="modal-body" style="background-color:white">
                <h4>Atleast select one Item to Reject PO</h4>
            </div>
            <div class="modal-footer" style="background-color:white">
                <button id="btnOkAlertR"  type="button" class="btn btn-danger" >OK</button>
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
                <p>Are you sure you want to reject this PO?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary" OnClick="btnReject_Click"  Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo2"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>
 <div id="mdlAttachments" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Attachments Quotations</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Panel ID="pnlImages" runat="server">
                                                <label for="fileImages">Uploded Images</label>
                                                <asp:GridView ID="gvImages" runat="server" ShowHeader="False"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Image Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationImageId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:HyperLink runat="server" href='<%#Eval("ImagePath").ToString().Remove(0,2)%>' Target="_blank">
                                                                            <asp:Image runat="server" ImageUrl='<%#Eval("ImagePath")%>' style="max-height:50px; width:auto; margin:5px" />
                                                                </asp:HyperLink>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                        <div class="form-group">
                                            <asp:Panel ID="pnlDocs" runat="server" Width="100%">
                                                <label for="fileImages">Uploded Documents</label>
                                                <asp:GridView ID="gvDocs" runat="server" ShowHeader="False"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Document Found" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField DataField="QuotationFileId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField ItemStyle-Height="30px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton
                                                                    Text='<%#Eval("FileName")%>' runat="server" href='<%#Eval("FilePath").ToString().Remove(0,2)%>' target="_blank" Style="margin-right: 5px;" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                         <div class="form-group">
                                            <asp:Panel ID="pnlcondtion" runat="server" Width="100%">
                                                <label for="fileImages">Terms And Conditons</label>
                                                <asp:TextBox TextMode="MultiLine" Rows="10"  ID="txtTermsAndConditions" Enabled="false"  runat="server" CssClass="form-control text-bold"></asp:TextBox>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>

     <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Attachment</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive tablegv" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PrId" HeaderText="PrId" />
            <asp:BoundField DataField="FilePath" HeaderText="FilePath"/>
            <asp:BoundField DataField="FileName" HeaderText="FileName" />
         
              <asp:TemplateField HeaderText="View">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnEdit" ImageUrl="~/images/view-icon-614x460.png"  style="width:39px;height:26px"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Download">
                  <ItemTemplate>
                      <asp:ImageButton ID="btnCancelRequest" ImageUrl="~/images/Downloads2.png"  style="width:26px;height:20px;margin-top:4px;"
                          runat="server" />
                  </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>               
    </div> 
     <div>
          <label id="lbMailMessage"  style="margin:3px; color:maroon; text-align:center;"></label>
     </div>
                </div>	
			</div>
		  </div>
		</div>
	  </div>
     </div>


            <div id="mdlTerminated" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 60%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Terminated PO</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                               
                                <div class="row">
                                    <div class="col-xs-12">


                                        <div>
                                            &nbsp
                                        </div>

                                        <!-- Start : Items Table -->
                                        <div style="color: black;">

                                         <asp:GridView ID="gvTerminatedDetails" runat="server"
                                                                            CssClass="table table-responsive"
                                                                            GridLines="None" AutoGenerateColumns="false"
                                                                            EmptyDataText="No MRNs Found" RowStyle-BackColor="#f5f2f2" HeaderStyle-BackColor="#525252" HeaderStyle-ForeColor="White">
                                                                            <Columns>
                                                                   
                                                                    <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                                     <asp:BoundField DataField="TerminationRemarks" HeaderText="Terminated Remarks"  />
                                                                    <asp:BoundField DataField="TerminatedByName" HeaderText="Terminated By" />
                                                                    <asp:BoundField DataField="TerminatedOn" HeaderText="Terminated On" />

                                                                  
                                                                </Columns>
                                                            </asp:GridView>

                                        </div>
                                        <!-- End : MRN Table -->
                                    </div>
                                </div>
                            </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>


 <%--  <div class="content" style="position: relative;background: #fff;overflow:hidden; border: 1px solid #f4f4f4;" id="divPrintPo" runat="server" >    <!-- Main content -->
 --%>     
       
       
       
           <div class="box box-info">
                        <%--<div class="box-header">
                            <img src="AdminResources/images/logo.png" class="center-block" />--%>
                               <%--<img src="AdminResources/images/ImportLogo1.png" alt="" id="Logo1"/>    
                             <ASP:Image ID="ShippingLogo" Visibile="False" ImgUrl="AdminResources/images/ImportLogo1.png"  runat="server"/>--%>
                           <%-- <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3>--%>
<%--                            <ASP:Image id="myImage" Visibile="False" ImgUrl="AdminResources/images/logo.png" runat="server"/>
                           --%> 
                        <%--</div>--%>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-xs-6">
                                <img src="AdminResources/images/logo.png" align="right" />
                                
                                 
                               
                                    </div>
                                <div class="col-xs-6 ">
                                    <%--<strong>COMPANY: </strong>--%>
                                    <b><asp:Label  runat="server" ID="lblCompName" Font-Size="Medium"></asp:Label></b><br>
                                    <b><asp:Label  runat="server" ID="lblcompAdd" Font-Size="Medium"></asp:Label></b><br>
                                    <strong>TP: </strong>
                                    <asp:Label runat="server" ID="lblTpNo"></asp:Label><br>
                                    <strong>FAX: </strong>
                                    <asp:Label runat="server" ID="lblFax"></asp:Label><br>
                                     <strong>VAT: </strong>
                                    <asp:Label runat="server" ID="lblCompVatNo"></asp:Label><br>
                                    </div>
                                </div>
                            <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Order</h3> 
                             <hr>
                            <div class="row">
                                <div class="col-md-3">
                                    <strong>SUPPLIER: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblSupName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblSupplierContact"></asp:Label>
                                </div>

                                <%--<div class="col-md-3">
                                    --%>
                                    <%--<strong>COMPANY: </strong>
                                    <asp:Label runat="server" ID="lblCompName"></asp:Label><br>
                                    <strong>VAT NO: </strong>
                                    <asp:Label runat="server" ID="lblCompVatNo"></asp:Label><br>
                                    <strong>TELEPHONE: </strong>
                                    <asp:Label runat="server" ID="lblTpNo"></asp:Label><br>
                                    <strong>FAX: </strong>
                                    <asp:Label runat="server" ID="lblFax"></asp:Label><br>--%>
                                    <%--<strong>STORE KEEPER: </strong>
                                    <asp:Label runat="server" ID="lblSK"></asp:Label><br>--%>
                                <%--</div>--%>
                                <div class="col-md-3">
                                    <strong>DELIVERING WAREHOUSE: </strong>
                                    <br>
                                    <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseAddress"></asp:Label><br>
                                    <asp:Label runat="server" ID="lblWarehouseContact"></asp:Label>
                                    <br>
                                    <strong>STORE KEEPER: </strong>
                                    <asp:Label runat="server" ID="lblStoreKeeper"></asp:Label><br>
                                    <br>
                                </div>
                                <div class="col-md-3">
                                     <strong>DATE: </strong>
                                    <asp:Label runat="server" ID="lblDate"></asp:Label><br>
                                    <strong>PO CODE: </strong>
                                    <asp:Label runat="server" ID="lblPO"></asp:Label><br>
                                    <strong>BASED PR: </strong>
                                    <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                    <%--<strong>QUOTATION FOR: </strong>
                                    <asp:Label runat="server" ID="lblQuotationFor"></asp:Label><br>--%>
                                    <%--<strong>APPROVAL STATUS: </strong>--%>
                                    <%--<asp:Label runat="server" ID="lblPending" CssClass="label label-warning" Visible="false" Text="Pending"></asp:Label>--%>
                                    <%--<asp:Label runat="server" ID="lblApproved" CssClass="label label-success" Visible="false" Text="Approved"></asp:Label>--%>
                                    <%--<asp:Label runat="server" ID="lblRejected" CssClass="label label-danger" Visible="false" Text="Rejected"></asp:Label><br>--%>
                                    <strong>PO TYPE: </strong>
                                    <asp:Label runat="server" ID="lblGeneral" CssClass="label label-success" Visible="false" Text="General PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblCovering" CssClass="label label-info" Visible="false" Text="Covering PO"></asp:Label>
                                    <asp:Label runat="server" ID="lblModified" CssClass="label label-warning" Visible="false" Text="Modified PO"></asp:Label><br>
                                   
                                    
                                </div>

                                <div class="col-xs-3">
                                    <asp:Panel runat="server" ID="pnlPaymentMethod" Visible="false">
                                        <strong>PAYMENT METHOD: </strong>
                                        <asp:Label runat="server" ID="lblPaymentType"></asp:Label>
                                    </asp:Panel>
                                   <%-- <asp:Panel runat="server" ID="pnlReason" Visible="false">
                                        <strong>REMARKS: </strong>
                                        <asp:Label runat="server" ID="lblRemarks"></asp:Label>
                                    </asp:Panel>--%>
                                    <asp:Panel runat="server" ID="pnlCancel" Visible="false">
                                        
                                        <strong>PO Status: </strong>
                                        <asp:Label runat="server" ID="lblCancel" CssClass="label label-danger" ></asp:Label>
                                    </asp:Panel>
                                    
                                        <strong>MRN Department: </strong>
                                        <asp:Label runat="server" ID="lblDepartment"></asp:Label> <br>
                                     <strong>PR Purchase Type : </strong>
                                        <asp:Label ID="lblPurchaseType" runat="server" Text=""></asp:Label><br />
                                    <strong>PO Purchase Type : </strong>
                                        <asp:Label ID="lblPoPurchaseType" runat="server" Text=""></asp:Label><br />
                                     <strong>Agent Name : </strong>
                                        <asp:Label ID="lblAgentName" runat="server" Text=""></asp:Label><br />
                                     <asp:Panel ID="panelParentPr" runat="server" Visible ="false">
                                             <strong>Parent PR : </strong>
                                                <asp:Label ID="lblParentPr" runat="server" Text=""></asp:Label><br />
                                                  </asp:Panel>
                                     
                                    <br>

                                </div>
                            </div>
                            

                            <div class="row">
                                <div class="col-md-12">
                                    <br />
                                    <br />
                                    <div class="table-responsive">
                                       <asp:GridView runat="server" ID="gvPoItems" AutoGenerateColumns="false" OnRowDataBound="gvPOItems_RowDataBound"
                                            CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                            <Columns>
                                                <asp:BoundField DataField="PodId" HeaderText="POD ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemName" HeaderText="Default Item Name" />
                                                <asp:BoundField DataField="SupplierMentionedItemName" HeaderText="Supplier mentioned Item Name" NullDisplayText="Not Found"/>
                                                <%-- <asp:TemplateField HeaderText="Supplier mentioned Item Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Text='<%# Eval("SupplierMentionedItemName").ToString() == "" ? "Not Found" : Eval("SupplierMentionedItemName").ToString() %>'/>
                                                                                
                                                                                 </ItemTemplate>
                                                               </asp:TemplateField>--%>
                                                
                                                <%--<asp:BoundField DataField="MeasurementShortName" HeaderText="Measurement" NullDisplayText="Not Found" />--%>
                                                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                            <ItemTemplate>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "0" ? true : false %>'
                                                                                    Text="Awaiting Receival" CssClass="label label-warning"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "1" ? true : false %>'
                                                                                    Text="Partially Received" CssClass="label label-info"/>
                                                                                <asp:Label
                                                                                    runat="server"
                                                                                    Visible='<%# Eval("Status").ToString() == "2" ? true : false %>'
                                                                                    Text="Fully Received" CssClass="label label-success"/>
                                                                                <asp:LinkButton 
                                                                                    runat="server" ID="btnMrn" 
                                                                                    Visible='<%# Eval("Status").ToString() == "3" ? true : false %>'
                                                                                    Text="Terminated" CssClass="label label-danger" OnClick="btnTerminated_Click" ></asp:LinkButton>
                                                                     </ItemTemplate>
                                                               </asp:TemplateField>
                                                <asp:BoundField DataField="TermName" HeaderText="Term" />
                                                <asp:BoundField DataField="MeasurementName" HeaderText="Unit" NullDisplayText="Not Found"/>
                                                <asp:BoundField DataField="Quantity" HeaderText="Requested QTY" DataFormatString="{0:N2}"/>
                                                <asp:BoundField DataField="UnitPriceForeign" HeaderText="Quoted Unit Price(Foreign)"
                                                            ItemStyle-Font-Bold="true" />
                                                        <asp:BoundField DataField="UnitPriceLocal" HeaderText="Quoted Unit Price(Local)"
                                                            ItemStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="ReceivedQty" HeaderText="Recieved QTY" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="WaitingQty" HeaderText="Waiting QTY" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="PendingQty" HeaderText="Pending QTY" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                
                                                <asp:BoundField DataField="ItemPrice" HeaderText="Quoted Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="NbtAmount" HeaderText="NBT" DataFormatString="{0:N2}" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                <asp:BoundField DataField="VatAmount" HeaderText="VAT" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="TotalAmount" HeaderText="NetTotal" DataFormatString="{0:N2}" />
                                                <asp:TemplateField HeaderText="PO Purchase Type" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                     <asp:Label runat="server"  Text='<%# Eval("PoPurchaseType").ToString() == "1" ? "Local":"Import" %>'></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="SupplierAgentName" HeaderText="Agent Name" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                 <asp:BoundField DataField="SparePartNumber" HeaderText="Spare Part Number"/>
                                                <asp:TemplateField HeaderText="Attachments">
                                                <ItemTemplate>
                                                    <asp:Button CssClass="btn btn-xs btn-default" OnClick="btnViewAttachments_Click" runat="server"
                                                        Text="View"></asp:Button>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                        <div class="row">
                            <div class="col-md-8">
                                 <asp:Panel ID="PanenImports" runat="server" Visible="false">
                                     <%--<div class="col-md-2">
                                    <img src="AdminResources/images/ImportLogo1.png" class="left-block" height="100" width="100" />
                                     </div>--%>
                                      <%--<asp:Panel ID="pnlLogo" runat="server" Visible="false">--%>
                            <%--<img src="AdminResources/images/ImportLogo1.png" class="left-block" height="120" width="140" /><br>--%>
                                 <%--</asp:Panel>--%>
                                      
                                    <%-- <strong>PRICE TERMS: </strong>
                                    <asp:Label runat="server" ID="lblPriceTerms"></asp:Label><br>--%>
                                     <asp:Panel runat="server" ID="pnlLogo" Visible="false">
                                         <label>Shipping Mark : </label> 
                                 <img src="AdminResources/images/ImportLogo1.png"  height="80" width="120" /><br>
                               </asp:Panel>
                                    <strong>CURRENCY: </strong>
                                    <asp:Label runat="server" ID="lblCurrency"></asp:Label><br>
                                    <strong>PAYMENT MODE: </strong>
                                    <asp:Label runat="server" ID="lblPaymentMode"></asp:Label><br>
                            </asp:Panel>
                                <asp:Panel ID="Remarks" runat="server" >
                                    <div class="form-group">
                                        <label>Remarks : </label>
                                        <asp:Label TextMode="MultiLine" Rows="6" runat="server" ID="txtRemarks"></asp:Label>

                                    </div>
                                    </asp:Panel>

                                </div>

                                <div class="col-md-4">
                                    <%--<p class="lead">SUMMARY</p>--%>
                                    <div class="table-responsive">
                                        <table class="table table-striped">
                                            <tbody>
                                                <tr>
                                                    <td><b>TOTAL</b></td>
                                                    <td id="tdSubTotal" class="text-right" runat="server"></td>
                                                </tr>
                                                
                                                <%--<tr>
                                                    <td><b>NBT</b></td>
                                                    <td id="tdNbt" class="text-right" runat="server"></td>
                                                </tr>--%>
                                                <tr>
                                                    <td><b>VAT</b></td>
                                                    <td id="tdVat" class="text-right" runat="server"></td>
                                                </tr>
                                                <tr>
                                                    <td><b>NETTOTAL</b></td>
                                                    <td id="tdNetTotal" class="text-right" runat="server"></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <hr />
                          <div class="row">
                                <div class="col-xs-4 col-sm-4 text-center">
                                    <asp:Image ID="imgCreatedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                    <%--<asp:Label runat="server" ID="lblCreatedByName"></asp:Label><br />--%>
                                     <asp:Label runat="server" ID="lblCreatedByDesignation"></asp:Label><br />
                                    <asp:Label runat="server" ID="lblCreatedDate"></asp:Label><br />
                                    <b>PO Created By</b>
                                    <hr style="padding-left:10px; padding-right:10px;" />
                                </div>
                               <%-- <asp:Panel ID="pnlParentApprovedByDetails" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgParentApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <asp:Label runat="server" ID="lblParentApprovedByName"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblParentApprovedDate"></asp:Label><br />
                                        <b id="lblParentApprovalText" runat="server"></b>
                                        <hr style="padding-left:10px; padding-right:10px;" />
                                        <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblParentApprovalRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                    </div>
                                </asp:Panel>--%>
                                <asp:Panel ID="pnlApprovedBy" runat="server" Visible="false">
                                    <div class="col-xs-4 col-sm-4 text-center">
                                        <asp:Image ID="imgApprovedBySignature" Style="width: 100px; height:50px;" runat="server" /><br />
                                        <%--<asp:Label runat="server" ID="lblApprovedByName"></asp:Label><br />--%>
                                         <asp:Label runat="server" ID="lblApprovedByDesignation"></asp:Label><br />
                                        <asp:Label runat="server" ID="lblApprovedDate"></asp:Label><br />
                                        <b id="lblApprovalText" runat="server"></b>
                                        <hr style="padding-left:10px; padding-right:10px;" />
                                        <strong>REMARKS</strong><br />
                                        <asp:Label runat="server" ID="lblApprovalRemarks" CssClass="text-left" style="padding-left:10px;"></asp:Label>
                                    </div>
                                </asp:Panel>

                        </div>
                        </div>
                        <div class="box-footer">
                            <asp:Button runat="server" ID="btnModify" CssClass="btn btn-warning" Text="Edit PO" OnClick="btnModify_Click" />
                            <asp:Button runat="server" ID="btnPrint"  Text="Print PO" CssClass="btn btn-success" OnClick="btnPrint_Click" />
                            <asp:Button runat="server" ID="btnCancel"  Text="Cancel" CssClass="btn btn-danger" OnClientClick="Cancel()" Visible="false" />
                            <asp:Button runat="server" ID="btnFollowUpRemark" CssClass="btn btn-info" Text="Follow Up Remarks" OnClick="btnFollowUpRemark_Click" />
                           <%--<div id="printerDiv"  style="display:none"></div>--%>
                        </div>
                    </div>


         <asp:Panel ID="pnlDerivedFrom" runat="server" Visible="false">
                        <div class="box box-info">
                            <div class="box-header with-border">
                              <h3 class="box-title">Derrived From Purchase Orders (Parent)</h3>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                              </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                              <div class="row">
                                <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvDerivedFrom" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                            <asp:BoundField DataField="POCode"  HeaderText="PO Code" />
                                            <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                                            <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                            <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                        Text="Pending" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                        Text="APPROVED" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                        Text="Rejected" CssClass="label label-danger"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>
                                            <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>
                                            <asp:TemplateField HeaderText="PO Type">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "0" ? true : false %>'
                                                        Text="General PO" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "1" ? true : false %>'
                                                        Text="Modified PO" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "2" ? true : false %>'
                                                        Text="Covering PO" CssClass="label label-info"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contains Derived POs">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("WasDerived").ToString() == "0" ? true : false %>'
                                                        Text="No" CssClass="label label-danger"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("WasDerived").ToString() == "1" ? true : false %>'
                                                        Text="Yes" CssClass="label label-info"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnViewPO" Text="View" OnClick="lbtnViewPO_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>         
                              </div>
         
                            </div>
                            <!-- /.box-body -->
                          </div>
                    </asp:Panel>


       <asp:Panel ID="pnlDerrivedPOs" runat="server" Visible="false">
                        <div class="box box-info">
                            <div class="box-header with-border">
                              <h3 class="box-title" >Derrived Purchase Orders (Child)</h3>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                              </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                              <div class="row">
                                <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvDerivedPOs" EmptyDataText="No Records Found" GridLines="None" CssClass="table table-responsive"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="PoID"  HeaderText="PoID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                            <asp:BoundField DataField="POCode"  HeaderText="PO Code" />
                                            <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                                            <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                            <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                        Text="Pending" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                        Text="APPROVED" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                        Text="Rejected" CssClass="label label-danger"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>
                                            <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>
                                            <asp:TemplateField HeaderText="PO Type">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "0" ? true : false %>'
                                                        Text="General PO" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "1" ? true : false %>'
                                                        Text="Modified PO" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsDerived").ToString() == "1" && Eval("IsDerivedType").ToString() == "2" ? true : false %>'
                                                        Text="Covering PO" CssClass="label label-info"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contains Derived POs">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("WasDerived").ToString() == "0" ? true : false %>'
                                                        Text="No" CssClass="label label-danger"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("WasDerived").ToString() == "1" ? true : false %>'
                                                        Text="Yes" CssClass="label label-info"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Actions">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnViewPO" Text="View" OnClick="lbtnViewPO_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>         
                              </div>
         
                            </div>
                            <!-- /.box-body -->
                          </div>
                    </asp:Panel>
       
            <asp:Panel ID="pnlGeneratedGRNs" runat="server" Visible="false">
                        <div class="box box-info">
                            <div class="box-header with-border">
                              <h3 class="box-title" >Generated Good Received Notes</h3>
                              <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                              </div>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                              <div class="row">
                                <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvGRNs" EmptyDataText="No records Found" GridLines="None" CssClass="table table-responsive"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="GrnId"  HeaderText="GrnId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                            <asp:BoundField DataField="GrnCode"  HeaderText="GRN Code" />
                                            <asp:BoundField DataField="PoCode"  HeaderText="PO Code" />
                                            <asp:BoundField DataField="GoodReceivedDate"  HeaderText="Good Received Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}" />
                                            <asp:BoundField DataField="CreatedDate"  HeaderText="Created Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}"/>
                                            <asp:BoundField DataField="CreatedByName"  HeaderText="Created By"/>
                                            <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "0" ? true : false %>'
                                                        Text="Pending" CssClass="label label-warning"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "1" ? true : false %>'
                                                        Text="APPROVED" CssClass="label label-success"/>
                                                    <asp:Label
                                                        runat="server"
                                                        Visible='<%# Eval("IsApproved").ToString() == "2" ? true : false %>'
                                                        Text="Rejected" CssClass="label label-danger"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ApprovedDate"  HeaderText="Approval Date"   dataformatstring="{0:dd-MM-yyyy hh:mm tt}" NullDisplayText="Not Found"/>
                                            <asp:BoundField DataField="ApprovedByName"  HeaderText="Approved By" NullDisplayText="Not Found"/>

                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lbtnViewGrn" Text="View" OnClick="lbtnViewGrn_Click"
                                                        ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    </div>
                                </div>         
                              </div>
         
                            </div>
                            <!-- /.box-body -->
                          </div>
                    </asp:Panel>
                    
       
       
   <%-- </div> --%>
    </section>
                <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" CssClass="hidden" />
                <asp:Button ID="btnDeleteFollowUpRemark" runat="server" OnClick="btnDelete_Click" CssClass="hidden" />
                <asp:HiddenField runat="server" ID="hdnRemarkId" />
                </ContentTemplate>
            </asp:UpdatePanel>
    </form>
   <script type="text/javascript">
       function Cancel() {

           swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to Cancel PO?</br></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                           
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnConfirm').click();
                            }
                        });
       }
       function DeleteRemark(e, Id) {
           e.preventDefault();
           $('#ContentSection_hdnRemarkId').val(Id);
           $('#mdlFollowUpRemark').modal('hide');
           $('div').removeClass('modal-backdrop');

           swal.fire({
               title: 'Are you sure?',
               html: "Are you sure you want to <strong>Delete</strong> the remark?</br></br>",
               type: 'warning',
               cancelButtonColor: '#d33',
               showCancelButton: true,
               showConfirmButton: true,
               confirmButtonText: 'yes',
               cancelButtonText: 'No',
               allowOutsideClick: false,
               preConfirm: function () {
                  

               }
           }
           ).then((result) => {
               if (result.value) {


                   $('#ContentSection_btnDeleteFollowUpRemark').click();
               } else if (result.dismiss === Swal.DismissReason.cancel) {

               }
           });


       }
       function RemoveBackdrop() {
            
            $('#mdlFollowUpRemark').hide();
            $('.modal-backdrop').remove();
            $('body').css("overflow", "auto");
            $('body').css("padding-right", "0");
        }
   </script>
</asp:Content>
