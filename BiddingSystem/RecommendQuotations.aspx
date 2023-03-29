<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="RecommendQuotations.aspx.cs" Inherits="BiddingSystem.RecommendQuotations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server" ViewStateMode="Enabled">

    <style type="text/css">
        .ChildGrid > tbody > tr > td:not(table) {
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
            border-bottom: 1px solid #d4d2d2;
        }

        .ChildGrid th {
            color: White;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }

        .ChildGridTwo > tbody > tr > td:not(table) {
            background-color: #f5f5f5 !important;
            color: black;
            border-bottom: 1px solid #d4d2d2;
        }


        .ChildGridTwo > tbody > tr {
            border: 1px solid #d4d2d2;
        }

        .ChildGridTwo th {
            color: white;
            font-size: 10pt;
            line-height: 200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #808080 !important;
        }

        .ChildGridThree td {
            text-align: left;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }

        #ContentSection_gvQuotations > tbody {
            background-color: #fbfafa !important;
            text-align: center;
        }

        #ContentSection_gvQuotations th {
            text-align: center;
        }

        #ContentSection_gvBids th, #ContentSection_gvBids td {
            text-align: center;
        }


        #ContentSection_gvQuotations > tbody > tr:nth-child(2n+1) > td:not(table) {
            border-bottom: 1px solid #555555;
            border-top: 1px solid #f8f8f8;
        }

         .greenBg {
                    background: #7bf768;
                  font-weight: bold;
                    cursor:pointer;
                }
        .CellClick{
            font:bold;
            cursor:pointer;
            text-align:right;
            font-weight: bold;
        }
        .footer-font{
                font-weight: bold;
                background-color:yellowgreen;
                text-align:right !important;
        }
         .alignright{
                text-align:right !important;
                font-weight: bold;
          }
    </style>

    <script src="AdminResources/js/jquery1.8.min.js"></script>
    <script type="text/javascript">
        $("[src*=plus]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "images/minus.png");
        });
        $("[src*=minus]").live("click", function () {
            $(this).attr("src", "images/plus.png");
            $(this).closest("tr").next().remove();
        });
    </script>

    <link href="AdminResources/css/formStyleSheet.css" rel="stylesheet" />
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />

    <section class="content-header">
        <h1>
            View Purchase Requests
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Bid Comparison </li>
        </ol>
    </section>
    <br />

    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>

                <!-- Start : Attachment Modal -->
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
                <!-- End : Attachment Modal -->

                
                <!-- Start : Recommendation Modal -->
                <div id="mdlRecommendations" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 90%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close closeRecommendation" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">TEC Committee Recommendation</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:GridView ID="gvRecommenations" runat="server" CssClass="ChildGrid" Width="100%"
                                                    GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Recommendations Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="RecommendationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="DesignationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="DesignationName" HeaderText="Required From" NullDisplayText="-"/>
                                                        <asp:TemplateField HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#Eval("IsRecommended").ToString() =="1"?"Recommended": Eval("IsRecommended").ToString() =="2"?"Rejected": "Pending"%>' Font-Bold="true" style='<%#Eval("IsRecommended").ToString() =="1"?"color: green":Eval("IsRecommended").ToString() =="2"?"color: Red": "color: gold"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Was Overridden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%#Eval("WasOverriden").ToString() =="1"?"Yes": "No"%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="RecommendedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="RecommendedByName" HeaderText="Recommended By" NullDisplayText="-" />
                                                        <asp:BoundField DataField="RecommendedDate"  HeaderText="Recommended Date" NullDisplayText="-" DataFormatString='<%$ appSettings:dateTimePattern %>' />
                                                        
                                                        
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox style="margin:5px; width:400px;" TextMode="MultiLine" ReadOnly="true" Rows="3" runat="server" Text='<%#Eval("Remarks")%>' Visible='<%#Eval("Remarks") == null? false : true%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="TEC Committee Decision" HeaderStyle-Width="150px" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-success btnOverrideRecommendationApproveCl" runat="server"
                                                                    ID="Button1" Text="Override and Recommend"
                                                                    style="margin-top:3px; width:150px;" Visible='<%#Eval("CanLoggedInUserOverride").ToString() =="1"?true: false%>'></asp:Button>
                                                                <asp:Button CssClass="btn btn-xs btn-danger btnOverrideRecommendationRejectCl" runat="server"
                                                                    ID="Button2" Text="Override and Reject"
                                                                    style="margin-top:3px; width:150px;" Visible='<%#Eval("CanLoggedInUserOverride").ToString() =="1"?true: false%>'></asp:Button>
                                                                <asp:Button CssClass="btn btn-xs btn-success btnApproveCl" runat="server"
                                                                    ID="btnApproveQ" Text="Recommend"
                                                                    style="margin-top:3px; width:150px;" Visible='<%#Eval("CanLoggedInUserRecommend").ToString() =="1"?true: false%>'></asp:Button>
                                                                <asp:Button CssClass="btn btn-xs btn-danger btnRejectCl" runat="server"
                                                                    ID="btnRejectQ" Text="Reject"
                                                                    style="margin-top:3px; width:150px;" Visible='<%#Eval("CanLoggedInUserRecommend").ToString() =="1"?true: false%>'></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- End : Modal Body -->

                            <div class="modal-footer">
                                 <asp:Button ID="btnTechdocView" runat="server" Text="View Docs" CssClass="btn btn-primary pull-right" OnClick="btnTechdocView_Click" Style="margin-right: 10px"  />
                                </div>
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : quantity Modal -->



                 <div id="mdlviewdocsuplod" class="modal modal-primary fade" tabindex="-1" style="z-index:3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close CanceldocUpload" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Upload Tec Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-sm-5">
                                     <div class="form-group">
                                
                                     <asp:FileUpload runat="server" style="display:inline;" AllowMultiple="true"    CssClass="form-control" ID="fileUpload1" ></asp:FileUpload>
                                      
                                </div>

                              </div>
                                        <div class="col-sm-2" > <button class="btn btn-info btn-flat clear"  id="clearDocs" >Clear</button></div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                             <div class="modal-footer">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary pull-right" OnClick="btnUpload_Click" Style="margin-right: 10px"  />
                        </div>
                        </div>
                    </div>
                </div>
                 <div id="mdlviewdocstechCommitee" class="modal modal-primary fade" tabindex="-1" style="z-index:3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close CanceldocView" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvbddifiles" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false"  EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="filename" HeaderText="File Name" />
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="sequenceId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnDownload"  OnClick="lbtnDownload_Click">Download</asp:LinkButton>
                                                             <%--   <iframe id="downloadFrame" style="display:none"></iframe>--%>
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

                   <div id="mdlRequiredQty" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close Cancelselct" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">ADD Quantity</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <div class="form-group">

                                             <label  for="exampleInputEmail1" id="itemname"></label>
                                            
                                        </div>
                                        <div class="form-group">
                                             <label id="mainQuantity" style="font-weight:bold;" for="exampleInputEmail1"></label>
                                            <input type="hidden" id="TabulationId"/>
                                             <input type="hidden" id="QutatuonId"/>
                                            <input type="hidden" id="BidId"/>
                                            <input type="hidden" id="SupplierId"/>
                                            <input type="hidden" id="ItemId"/>
                                            <input type="hidden" id="selectedquanty" />
                                            <input type="hidden" id="Rowno"/>
                                            <input type="hidden" id="cellno" />
                                            <input type="hidden" id="ISEditedAgian" />
                                            <input type="hidden" id="previousqty" />
                                        </div>
                                         
                                        <div class="form-group">
                                             <label  for="exampleInputEmail1">Requesting Quantity </label>
                                            <input type="number" id="txtamount" class="form-control input-md"  />
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
                             <div class="modal-footer">
                                 <input type="button" id="btnadd"  class=" btn btn-primary" value="Select" />
                                 <input type="button" id="btncancel"  class=" btn btn-primary" value="Unselect" />
                                  <input type="button" id="btnclose"  class="btn btn-danger " value="Cancel"  />
                                 </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- Start : Quotations Modal -->
                <div id="mdlQuotations" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Selection Tabulation Sheet</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div class="table-responsive" style="color: black;">
                                           <asp:GridView runat="server" ID="gvQuotations" GridLines="Both"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" selectedindex="1" OnRowCreated="gvQuotations_RowCreated" ShowFooter="true"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvQuotations_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" Visible="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Width="150px" ItemStyle-CssClass=" left" ItemStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Refno" HeaderText="Tender REF. NO:" />
                                                     <asp:BoundField DataField="TabulationId" HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                      <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                     <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                     <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-Font-Bold="true" />
                                                    <asp:TemplateField HeaderText="Attachments">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-default btnViewAttachmentsCl" runat="server"
                                                                Text="View"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Supplier Details">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-success" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                Text="view" Style="margin-right: 4px;"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>


                                            </asp:GridView>

                                            <asp:GridView runat="server" ID="gvImpotsQuotations" GridLines="Both" Visible="false" HeaderStyle-Width="300px" ShowFooter="true"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" selectedindex="1" OnRowCreated="gvImpotsQuotations_RowCreated"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvImpotsQuotations_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White" Width="1500px">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true"  ItemStyle-Width="150px" ItemStyle-CssClass=" left" >
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Refno" HeaderText="Tender REF. NO:" />
                                                     <asp:BoundField DataField="TabulationId" HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                      <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden"  />
                                                     <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden"/>
                                                     <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden"/>
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-Font-Bold="true" />
                                                     <asp:BoundField DataField="Agent" HeaderText="Agent Name" ItemStyle-Font-Bold="true" />
                                                     <asp:BoundField DataField="Country" HeaderText="Country" ItemStyle-Font-Bold="true" />
                                                     <asp:BoundField DataField="Brand" HeaderText="Brand" ItemStyle-Font-Bold="true" />
                                                     <asp:BoundField DataField="Currency" HeaderText="Currency" ItemStyle-Font-Bold="true" />
                                                    <asp:TemplateField HeaderText="Attachments">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-default btnViewAttachmentsCl" runat="server"
                                                                Text="View"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Supplier Details">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-success" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                Text="view" Style="margin-right: 4px;"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>

                                             </asp:GridView>
                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                               <div class="row form-group">
                                 <div class="col-sm-2" >
                                             <label  for="exampleInputEmail1">Tabultion Remark </label>
                                     </div>
                                 <div class="col-md-4" >
                                     <asp:RequiredFieldValidator ErrorMessage="Remark Is required" runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true" InitialValue=""  ControlToValidate="txtareaRemark" ValidationGroup="btnUpdatefinish">*</asp:RequiredFieldValidator> 
                                       <asp:TextBox id="txtareaRemark"  class=" form-control" TextMode="multiline" Columns="30" Rows="5" runat="server" />
                                 </div>
                                         
                                        </div>
                             <div class="modal-footer">
                                    <asp:Button CssClass="btn btn-xs btn-success" runat="server"
                                                ID="btnPrint" Text="print" OnClick="btnPrint_Click"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                 <asp:Button CssClass="btn btn-xs btn-primary" ValidationGroup="btnUpdatefinish" runat="server"
                                                ID="btnUpdatefinish" Text="Update Selection" OnClick="btnUpdatefinish_Click"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                  <asp:Button CssClass="btn btn-xs btn-danger" runat="server"
                                                ID="viewRecomendation" Text="Recomendation" OnClick="btnViewRecommendations_Click"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                     <asp:Button CssClass="btn btn-xs btn-warning" runat="server"
                                                ID="btnuplodDock" Text="Upload Docs" OnClick="btnuplodDock_Click"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>
                <!-- End : Quotations Modal -->
                 <div id="mdlRejectedTabulations" class="modal fade" tabindex="-1" role="dialog" style="z-index: 3000;"
                    aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;">
                        <!-- Start : Modal Content -->
                        <div class="modal-content">
                            <!-- Start : Modal Header -->
                            <div class="modal-header" style="background-color: #3C8DBC; color: white">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Selection Tabulation Sheet</h4>
                            </div>
                            <!-- End : Modal Header -->
                            <!-- Start : Modal Body -->
                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-xs-12">
                                        <!-- Start : Quotation Table -->
                                        <div class="table-responsive" style="color: black;">
                                           <asp:GridView runat="server" ID="gvrjectedTabulationsheet" GridLines="Both"
                                                CssClass="table table-responsive" AutoGenerateColumns="true" selectedindex="1" ShowFooter="true"
                                                DataKeyNames="TabulationId" OnRowDataBound="gvrjectedTabulationsheet_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="NO" ItemStyle-Font-Bold="true" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <span style="font: bold;">
                                                                <%#Container.DataItemIndex + 1%>
                                                            </span>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Refno" HeaderText="Tender REF. NO:" />
                                                     <asp:BoundField DataField="TabulationId" HeaderText="TabulationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                      <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                     <asp:BoundField DataField="BidId" HeaderText="BidId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                     <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" ItemStyle-Font-Bold="true" />
                                                    <asp:TemplateField HeaderText="Attachments" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-default btnViewAttachmentsCl2" runat="server"
                                                                Text="View"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Supplier Details" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden">
                                                        <ItemTemplate>
                                                            <asp:Button CssClass="btn btn-xs btn-success" ID="btnsupplerview" OnClick="btnsupplerview_Click" runat="server"
                                                                Text="view" Style="margin-right: 4px;"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>


                                            </asp:GridView>
                                        </div>
                                        <!-- End : Quotation Table -->
                                    </div>
                                </div>
                            </div>
                              <div class="modal-footer">
                                    <asp:Button CssClass="btn btn-xs btn-success" runat="server"
                                                ID="btnreprint" Text="print" OnClick="btnreprint_Click"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                                  <asp:Button CssClass="btn btn-xs btn-danger" runat="server"
                                                ID="Button3" Text="Recomendation" OnClick="btnViewRecommendations_Click"
                                                Style="margin-top: 3px; width: 100px;"></asp:Button>
                          </div>
                            <!-- End : Modal Body -->
                        </div>
                        <!-- End : Modal Content -->
                    </div>
                </div>

                 <div id="mdlviewdocs" class="modal modal-primary fade" tabindex="-1" style="z-index:3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close " data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvViewdocsrejected" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false"  EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="filename" HeaderText="File Name" />
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="sequenceId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnDownload"  OnClick="lbtnDownload_Click">Download</asp:LinkButton>
                                                             <%--   <iframe id="downloadFrame" style="display:none"></iframe>--%>
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
                <!-- Start : Section -->
                <section class="content">
                    <div class="row">
                        <div class="col-xs-12">
                            <!-- Start : Box -->
                            <div class="box box-info">
                                <!-- Start : Box Header-->
                                <div class="box-header with-border">
                                    <h3 class="box-title">Purchase Request Details</h3>
                                </div>
                                <!-- End : Box Header -->
                                <!-- Start : Box Body-->
                                <div class="box-body">
                            <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>PR No : </strong>
                                        <asp:Label ID="lblPRNo" runat="server" Text=""></asp:Label><br />
                                        <strong>Created On : </strong>
                                        <asp:Label ID="lblCreatedOn" runat="server" Text=""></asp:Label><br />
                                        <strong>Created By : </strong>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Requested By : </strong>
                                        <asp:Label ID="lblRequestBy" runat="server" Text=""></asp:Label><br />
                                        <strong>Requested For : </strong>
                                        <asp:Label ID="lblRequestFor" runat="server" Text=""></asp:Label><br />
                                        <strong>Expense Type : </strong>
                                        <asp:Label ID="lblExpenseType" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Warehouse : </strong>
                                        <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label><br /> 
                                        <strong>MRN No : </strong>
                                        <asp:Label ID="lblMrnId" runat="server" Text=""></asp:Label><br />   
                                        <strong>Department : </strong>
                                        <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label><br />                                      
                                    </address>
                                </div>
                            </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBids" runat="server" CssClass="table table-responsive"
                                                    OnRowDataBound="gvBids_RowDataBound" GridLines="None"
                                                    AutoGenerateColumns="false" DataKeyNames="BidId" Caption="Bids for Purchase Request"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                                    src="images/plus.png" />
                                                                <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                                                    <asp:GridView ID="gvBidItems" runat="server"
                                                                        CssClass="table table-responsive ChildGrid"
                                                                        GridLines="None" AutoGenerateColumns="false"
                                                                        Caption="Items in Bid">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="BiddingItemId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="BidId"
                                                                                HeaderText="BidItemId"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="PrdId"
                                                                                HeaderText="PRDId" HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="CategoryName"
                                                                                HeaderText="Category Name" />
                                                                            <asp:BoundField DataField="SubCategoryId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="SubCategoryName"
                                                                                HeaderText="Sub-Category Name" />
                                                                            <asp:BoundField DataField="ItemId"
                                                                                HeaderText="Item Id"
                                                                                HeaderStyle-CssClass="hidden"
                                                                                ItemStyle-CssClass="hidden" />
                                                                            <asp:BoundField DataField="ItemName"
                                                                                HeaderText="Item Name" />
                                                                            <asp:BoundField DataField="Qty" HeaderText="Quantity" />
                                                                            <asp:BoundField DataField="EstimatedPrice"
                                                                                HeaderText="Estimated Price" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </asp:Panel>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Bid Code"
                                                            HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CreatedUserName" HeaderText="Created By" />
                                                        <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:TemplateField HeaderText="Bid Opened For">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bid Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="NoOfQuotations" HeaderText="Qutations Count" />
                                                        <asp:BoundField DataField="QuotationApprovalRemarks" HeaderText="Approval Remarks" />
                                                        <asp:TemplateField HeaderText="Actions" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnView" Text="Tabulation Sheet" OnClick="btnView_Click"
                                                                    style="width:120px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>

                                                 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <!-- End : Box Body -->
                                <!-- Start : Box Footer -->
                                <div class="box-footer">
                                    <a class="btn btn-info pull-right" href="ViewPrForQuotationConfirmation.aspx"
                                        style="margin-right:10px">Done</a>
                                </div>
                                <!-- End : Box Footer -->
                            </div>
                            <!-- End : Box -->

                                <div class="box box-info">
                                <!-- Start : Box Header-->
                                <div class="box-header with-border">
                                    <h3 class="box-title"> Rejected Tabulation Sheets</h3>
                                </div>
                                <!-- End : Box Header -->
                                <!-- Start : Box Body-->
                                <div class="box-body">

                                    <div class="row">
                                        <div class="col-xs-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvRejectedTabulations" runat="server" CssClass="table table-responsive" GridLines="None"
                                                    AutoGenerateColumns="false" EmptyDataText="No Tabulation Rejected" DataKeyNames="TabulationId" Caption="Rejected Tabulation"  HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                       <asp:BoundField DataField="TabulationId" HeaderText="TabulationId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:TemplateField HeaderText="Bid Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Tabulation Recommended">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsRecommended").ToString() =="1" ? "YES":"NO" %>'
                                                                    ForeColor='<%# Eval("IsRecommended").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Recommendation Docs">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnRecDocs" Text="Download"  OnClick="btnRecDocs_Click"
                                                                    style="width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Is Tabulation Approved">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Bold="true" runat="server" Text='<%# Eval("IsApproved").ToString() =="1" ? "YES":"NO" %>'
                                                                    ForeColor='<%# Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.DeepSkyBlue:System.Drawing.Color.Red %>' />
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="Procurement docs">
                                                            <ItemTemplate>
                                                                 <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnProcDoc" Enabled='<%# Eval("IsRecommended").ToString() =="1" ? true: false %>' Text="Download" OnClick="btnProcDoc_Click"
                                                                    style="width:100px;"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />
                                                        <asp:BoundField DataField="CreatedOn" HeaderText="Created Date"
                                                            DataFormatString='<%$ appSettings:datePattern %>' />
                                                        <asp:TemplateField HeaderText="View" HeaderStyle-Width="110px" ItemStyle-Width="110px">
                                                            <ItemTemplate>
                                                                <asp:Button CssClass="btn btn-xs btn-primary" runat="server"
                                                                    ID="btnRejectedView" Text="Tabulation Sheet" OnClick="btnRejectedView_Click"
                                                                    style="width:100px;"></asp:Button>
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
                </section>
                <!-- End : Section -->

                <!-- Start : Hidden Fields -->
                <asp:HiddenField ID="hdnSubTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNbtTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnVatTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnNetTotal" runat="server" Value="0.00" />
                <asp:HiddenField ID="hdnApprovalRemarks" runat="server" />
                <asp:HiddenField ID="hdnRejectRemarks" runat="server" />
                <asp:HiddenField ID="hdnBidId" runat="server" />
                <asp:HiddenField ID="hdnQuotationId" runat="server" />
                <asp:HiddenField ID="hdnTabulationId" runat="server" />
                <asp:HiddenField ID="hdnRecommendationId" runat="server" />
                <asp:HiddenField ID="PurchaseType" runat="server" />
                <asp:Button ID="btnApprove" runat="server" OnClick="btnApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnReject" runat="server" OnClick="btnReject_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideRecommendationApprove" runat="server" OnClick="btnOverrideRecommendationApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideRecommendationReject" runat="server" OnClick="btnOverrideRecommendationReject_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideApprove" runat="server" OnClick="btnOverrideApprove_Click" CssClass="hidden" />
                <asp:Button ID="btnOverrideReject" runat="server" OnClick="btnOverrideReject_Click" CssClass="hidden" />
                <asp:Button ID="btnViewAttachments" runat="server" OnClick="btnViewAttachments_Click" CssClass="hidden" />
                <asp:Button ID="btnViewRecommendations" runat="server" OnClick="btnViewRecommendations_Click" CssClass="hidden" />

                <asp:HiddenField ID="hdnrejected" runat="server" />
                <asp:HiddenField ID="hdnSelectedChanged" runat="server" />
                  <asp:HiddenField ID="hdnSlectedQutations" runat="server" />
                
                 <asp:HiddenField ID="hdnIsrejected" runat="server" />
                <!-- End : Hidden Fields -->

            </ContentTemplate>
             <Triggers>
                <asp:PostBackTrigger ControlID="btnPrint" />
                 <asp:PostBackTrigger ControlID="btnUpload"/>
                 <asp:PostBackTrigger ControlID="btnreprint" />
            </Triggers>
        </asp:UpdatePanel>

    </form>

    <script type="text/javascript">
        Sys.Application.add_load(function () {
            $(function () {
               
                $('.btnViewAttachmentsCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlQuotations').modal('hide');

                        if ($('#ContentSection_PurchaseType').val() == "L") {
                            var tableRow = $('#ContentSection_gvQuotations').find('> tbody > tr > td:not(table)');
                            $('#ContentSection_hdnQuotationId').val($(tableRow).eq(3).text());
                        }
                        else {
                            var tableRow = $('#ContentSection_gvImpotsQuotations').find('> tbody > tr > td:not(table)');
                            $('#ContentSection_hdnQuotationId').val($(tableRow).eq(3).text());
                        }
                        $('#ContentSection_btnViewAttachments').click();
                    }
                });

                $('#mdlAttachments').on('hide.bs.modal', function () {
                    $('.modal-backdrop').remove();
                    $('#mdlQuotations').modal('show');
                });

                $('.btnViewRecommendationsCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlQuotations').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        $('#ContentSection_btnViewRecommendations').click();
                    }
                });

                $('.closeRecommendation').on({
                    click: function () {
                        if ($('#ContentSection_hdnIsrejected').val() == "true") {
                            event.preventDefault();
                            $('#mdlRecommendations').modal('hide');
                            $('#mdlRejectedTabulations').modal('show');
                            $('div').removeClass('modal-backdrop');
                        }
                        else {
                            event.preventDefault();
                            $('#mdlRecommendations').modal('hide');
                            $('#mdlQuotations').modal('show');
                            $('div').removeClass('modal-backdrop');

                        }
                        
                    }

                });

                //$('#mdlRecommendations').on('hide.bs.modal', function () {
                //    $('.modal-backdrop').remove();
                //    $('#mdlQuotations').modal('show');
                //});

                $('#mdlQuotations').on('shown.bs.modal', function () {
                    $('#mdlQuotations').css('overflow', 'auto');
                    $('body').css("overflow", "hidden");
                    $('body').css("padding-right", "0");
                })

                $('#mdlQuotations').on('hidden.bs.modal', function () {
                    $('body').css("overflow", "auto");
                    $('body').css("padding-right", "0");
                })


                $('.btnOverrideRecommendationApproveCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlRecommendations').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Recommend</strong> the Recommendation?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Approve!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnOverrideRecommendationApprove').click();
                            } else if (
                              result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlRecommendations').modal('show');

                            }
                        });


                    }
                });

                $('.btnOverrideRecommendationRejectCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlRecommendations').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Reject</strong> the Recommendation?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            confirmButtonColor: '#d33',
                            cancelButtonColor: '#3085d6',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Reject!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnRejectRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnOverrideRecommendationReject').click();
                            } else if (
                              result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlRecommendations').modal('show');

                            }
                        });


                    }
                });

                $('.btnApproveCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlRecommendations').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Approve</strong> selected Quotation?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'value='Approved'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Approve!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnApprove').click();
                            } else if (
                              result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlRecommendations').modal('show');

                            }
                        });


                    }
                });

                $('.btnRejectCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#mdlRecommendations').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnRecommendationId').val($(tableRow).eq(0).text());
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Reject</strong> selected Quotation?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            confirmButtonColor: '#d33',
                            cancelButtonColor: '#3085d6',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Reject!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnRejectRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $('#ContentSection_btnReject').click();
                            } else if (
                              result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlRecommendations').modal('show');

                            }
                        });


                    }
                });

                $('.btnOverrideApproveCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#isSelect').val('1');
                        $('#mdlQuotations').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());
                        var supplierName = $(tableRow).eq(4).text();

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Approve</strong> the quotation from <strong>" + supplierName + "</strong>?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required' value='Approved'/></br>",
                            type: 'warning',
                            cancelButtonColor: '#d33',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Approve!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {

                                $('#ContentSection_hdnApprovalRemarks').val($('#ss').val());

                            }
                        }
                        ).then((result) => {
                            if (result.value) {
                                $(document).find('.txtSubTotalCl').removeAttr('disabled');
                                $(document).find('.txtNbtCl').removeAttr('disabled');
                                $(document).find('.txtVatCl').removeAttr('disabled');
                                $(document).find('.txtNetTotalCl').removeAttr('disabled');
                                $('#ContentSection_btnOverrideApprove').click();
                            } else if (
                              result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlQuotations').modal('show');

                            }
                        });


                    }
                });

                $('.btnOverrideRejectCl').on({
                    click: function () {
                        event.preventDefault();
                        $('#isSelect').val('1');
                        $('#mdlQuotations').modal('hide');

                        var tableRow = $(this).closest('tr').find('> td:not(table)');
                        $('#ContentSection_hdnQuotationId').val($(tableRow).eq(1).text());
                        var supplierName = $(tableRow).eq(4).text();

                        swal.fire({
                            title: 'Are you sure?',
                            html: "Are you sure you want to <strong>Override and Reject</strong> the quotation from <strong>" + supplierName + "</strong>?</br></br>"
                                + "<strong id='dd'>Remarks</strong>"
                                + "<input id='ss' type='text' class ='form-control' required='required'/></br>",
                            type: 'warning',
                            confirmButtonColor: '#d33',
                            cancelButtonColor: '#3085d6',
                            showCancelButton: true,
                            showConfirmButton: true,
                            confirmButtonText: 'Yes. Reject It!',
                            cancelButtonText: 'No',
                            allowOutsideClick: false,
                            preConfirm: function () {
                                if ($('#ss').val() == '') {
                                    $('#dd').prop('style', 'color:red');
                                    swal.showValidationError('Remarks Required');
                                    return false;
                                }
                                else {
                                    $('#ContentSection_hdnRejectRemarks').val($('#ss').val());
                                }
                            }
                        }).then((result) => {
                            if (result.value) {

                                $('#ContentSection_btnOverrideReject').click();

                            } else if (
                              result.dismiss === Swal.DismissReason.cancel
                            ) {

                                $('#mdlQuotations').modal('show');

                            }
                        });


                    }
                });

              
                $('.txtNegotiatePriceCl').on({
                    keyup: function () {
                        calculate(this);
                    }
                });

                $('.txtQtyCl').on({
                    keyup: function () {
                        calculate(this);
                    }
                });
                //$(document).on('keyup', '.txtNegotiatePriceCl', function () {

                //    calculate(this);
                //    event.stopPropagation();
                //});

                function calculate(elmt) {
                    var unitPrice = $(elmt).closest('tr').find('.txtNegotiatePriceCl').val();
                    $(elmt).closest('tr').find('.txtNegotiatePriceCl').value = $(elmt).closest('tr').find('.txtNegotiatePriceCl').val();
                    if (unitPrice == '' || unitPrice == null) {
                        unitPrice = 0;
                    }
                    var qty = 0;
                    if ($(elmt).closest('tr').find('.txtQtyCl').val() != '')
                        qty = parseFloat($(elmt).closest('tr').find('.txtQtyCl').val());
                    var subTot = 0;
                    var nbt = 0;
                    var vat = 0;
                    var netTot = 0;

                    subTot = unitPrice * qty;

                    var chkNbt = $(elmt).closest('tr').find('.chkNbtCl').find('input');
                    var chkVat = $(elmt).closest('tr').find('.chkVatCl').find('input');


                    var rdoNbt204 = $(elmt).closest('tr').find('.rdo204').find('input');
                    var rdoNbt2 = $(elmt).closest('tr').find('.rdo2').find('input');

                    if ($(chkNbt).prop('checked') == true) {
                        if ($(rdoNbt204).prop('checked') == true) {
                            nbt = parseFloat((subTot * 2) / 98);
                        }
                        else {
                            nbt = parseFloat((subTot * 2) / 100);
                        }

                    }

                    if ($(chkVat).prop('checked') == true) {

                        vat = parseFloat((subTot + nbt) * 0.15);
                    }

                    netTot = subTot + nbt + vat;

                    $(elmt).closest('tr').find('.txtSubTotalCl').val(subTot.toFixed(2));
                    $(elmt).closest('tr').find('.txtNbtCl').val(nbt.toFixed(2));
                    $(elmt).closest('tr').find('.txtVatCl').val(vat.toFixed(2));
                    $(elmt).closest('tr').find('.txtNetTotalCl').val(netTot.toFixed(2));

                    var tableRows = $(elmt).closest('tbody').find('> tr:not(:has(>td>table))');

                    var globSubTotal = 0;
                    var globTotalNbt = 0;
                    var globTotalVat = 0;
                    var globNetTotal = 0;

                    for (i = 1; i < tableRows.length; i++) {
                        if ($(tableRows[i]).find('.txtSubTotalCl').val() != '')
                            globSubTotal = globSubTotal + parseFloat($(tableRows[i]).find('.txtSubTotalCl').val());
                        if ($(tableRows[i]).find('.txtNbtCl').val() != '')
                            globTotalNbt = globTotalNbt + parseFloat($(tableRows[i]).find('.txtNbtCl').val());
                        if ($(tableRows[i]).find('.txtVatCl').val() != '')
                            globTotalVat = globTotalVat + parseFloat($(tableRows[i]).find('.txtVatCl').val());
                        if ($(tableRows[i]).find('.txtNetTotalCl').val() != '')
                            globNetTotal = globNetTotal + parseFloat($(tableRows[i]).find('.txtNetTotalCl').val());
                    }

                    var tableRow = $(elmt).closest('table').closest('tr').prev().find('>td:not(tr)');
                    //debugger;

                    $(tableRow).eq(5).text(globSubTotal.toFixed(2));
                    $(tableRow).eq(6).text(globTotalNbt.toFixed(2));
                    $(tableRow).eq(7).text(globTotalVat.toFixed(2));
                    $(tableRow).eq(8).text(globNetTotal.toFixed(2));

                    $('#ContentSection_hdnSubTotal').val(globSubTotal.toFixed(2));
                    $('#ContentSection_hdnNbtTotal').val(globTotalNbt.toFixed(2));
                    $('#ContentSection_hdnVatTotal').val(globTotalVat.toFixed(2));
                    $('#ContentSection_hdnNetTotal').val(globNetTotal.toFixed(2));

                }



                $('.CellClick').click(function (ev) {

                    //if ($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(4).text() == "1") {

                    $('#mdlQuotations').modal('hide');

                    if ($('#ContentSection_PurchaseType').val() == "L") {


                        var lable = $('#ContentSection_gvQuotations tr').eq(0).find('th').eq($(this).index() - 1).text();
                        $('#itemname').text("Item Name : " + lable.split('-')[1]);
                        var selectqty = $('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index()).text();
                        selectqty = selectqty.split('*')[1];

                        if (selectqty != "" && selectqty != undefined) {
                            noselct = parseInt(selectqty);
                            $("#btnadd").attr('value', 'Update');
                            var fullqty = $('#ContentSection_gvQuotations tr').eq(0).find('th').eq($(this).index()).text();

                            var ar = fullqty.split('-')
                            var qty = parseInt(ar[ar.length - 1]);

                            var selectedqty = 0

                            $('#ContentSection_gvQuotations tr td:nth-child(' + ($(this).index() + 1) + ')').each(function () {
                                var IscellSelected = $(this).hasClass("greenBg");
                                console.log($(this).text());
                                if (IscellSelected == true) {
                                    console.log($(this).text().split('*')[1]);
                                    selectedqty = selectedqty + parseInt($(this).text().split('*')[1]);
                                }


                            });
                            qty = qty - selectedqty;
                            $("#mainQuantity").text("Rquired Quantity :" + qty);
                            $("#TabulationId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(2).text());
                            $("#QutatuonId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(3).text());
                            $("#BidId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(4).text());
                            $("#SupplierId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(5).text());
                            $("#ItemId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index() - 1).text());
                            $("#selectedquanty").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(1).text());
                            $("#Rowno").val($(this).parent().index());
                            $("#cellno").val($(this).index());
                            $("#ISEditedAgian").val(1);
                            $("#txtamount").val(selectqty);
                            $("#previousqty").val(selectqty);
                            $('#mdlRequiredQty').modal('show');
                            fullqty = "";

                        }
                        else {

                            var fullqty = $('#ContentSection_gvQuotations tr').eq(0).find('th').eq($(this).index()).text();
                            $("#btnadd").attr('value', 'Add');
                            var ar = fullqty.split('-');
                            var qty = parseInt(ar[ar.length - 1]);

                            var selectedqty = 0

                            $('#ContentSection_gvQuotations tr td:nth-child(' + ($(this).index() + 1) + ')').each(function () {
                                var IscellSelected = $(this).hasClass("greenBg");
                                console.log($(this).text());
                                if (IscellSelected == true) {
                                    selectedqty = selectedqty + parseInt($(this).text().split('*')[1]);
                                }


                            });
                            qty = qty - selectedqty;
                            $("#mainQuantity").text("Rquired Quantity :" + qty);
                            $("#TabulationId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(2).text());
                            $("#QutatuonId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(3).text());
                            $("#BidId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(4).text());
                            $("#SupplierId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(5).text());
                            $("#ItemId").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index() - 1).text());
                            $("#selectedquanty").val($('#ContentSection_gvQuotations tr').eq($(this).parent().index()).find('td').eq(1).text());
                            $("#Rowno").val($(this).parent().index());
                            $("#cellno").val($(this).index());
                            $("#ISEditedAgian").val(0);
                            $('#mdlRequiredQty').modal('show');
                            fullqty = "";
                        }
                    }


                    else {


                        var lable = $('#ContentSection_gvImpotsQuotations tr').eq(0).find('th').eq($(this).index() - 1).text();
                        $('#itemname').text("Item Name : " + document.getElementById("ContentSection_gvImpotsQuotations").caption.innerHTML);
                        var selectqty = $('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index()).text();
                        selectqty = selectqty.split('*')[1];

                        if (selectqty != "" && selectqty != undefined) {
                            noselct = parseInt(selectqty);
                            $("#btnadd").attr('value', 'Update');
                            var fullqty = $('#ContentSection_gvImpotsQuotations tr').eq(0).find('th').eq($(this).index()).text();

                            var ar = fullqty.split('-')
                            var qty = parseInt(ar[ar.length - 1]);

                            var selectedqty = 0

                            $('#ContentSection_gvImpotsQuotations tr td:nth-child(' + ($(this).index() + 1) + ')').each(function () {
                                var IscellSelected = $(this).hasClass("greenBg");
                                console.log($(this).text());
                                if (IscellSelected == true) {
                                    console.log($(this).text().split('*')[1]);
                                    selectedqty = selectedqty + parseInt($(this).text().split('*')[1]);
                                }


                            });
                            qty = qty - selectedqty;
                            $("#mainQuantity").text("Rquired Quantity :" + qty);
                            $("#TabulationId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(2).text());
                            $("#QutatuonId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(3).text());
                            $("#BidId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(4).text());
                            $("#SupplierId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(5).text());
                            $("#ItemId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index() - 2).text());
                            $("#selectedquanty").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(1).text());
                            $("#Rowno").val($(this).parent().index());
                            $("#cellno").val($(this).index());
                            $("#ISEditedAgian").val(1);
                            $("#txtamount").val(selectqty);
                            $("#previousqty").val(selectqty);
                            $('#mdlRequiredQty').modal('show');
                            fullqty = "";

                        }
                        else {

                            var fullqty = $('#ContentSection_gvImpotsQuotations tr').eq(0).find('th').eq($(this).index()).text();
                            $("#btnadd").attr('value', 'Add');
                            var ar = fullqty.split('-');
                            var qty = parseInt(ar[ar.length - 1]);

                            var selectedqty = 0

                            $('#ContentSection_gvImpotsQuotations tr td:nth-child(' + ($(this).index() + 1) + ')').each(function () {
                                var IscellSelected = $(this).hasClass("greenBg");
                                console.log($(this).text());
                                if (IscellSelected == true) {
                                    selectedqty = selectedqty + parseInt($(this).text().split('*')[1]);
                                }


                            });
                            qty = qty - selectedqty;
                            $("#mainQuantity").text("Rquired Quantity :" + qty);
                            $("#TabulationId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(2).text());
                            $("#QutatuonId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(3).text());
                            $("#BidId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(4).text());
                            $("#SupplierId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(5).text());
                            $("#ItemId").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq($(this).index() - 2).text());
                            $("#selectedquanty").val($('#ContentSection_gvImpotsQuotations tr').eq($(this).parent().index()).find('td').eq(1).text());
                            $("#Rowno").val($(this).parent().index());
                            $("#cellno").val($(this).index());
                            $("#ISEditedAgian").val(0);
                            $('#mdlRequiredQty').modal('show');
                            fullqty = "";
                        }

                    }

                    //}
                    //else {
                    //    $('#mdlQuotations').modal('hide');

                    //    swal({ type: 'error', title: 'ERROR', text: 'No Authority to Select this', showConfirmButton: true })
                    //    .then(function(isConfirm) {
                    //        if (isConfirm) {
                    //            $('#mdlQuotations').modal('show');
                    //            $('div').removeClass('modal-backdrop');
                    //        }
                    //    });



                    //}
                    ev.preventDefault();
                });




                $("#btnclose").click(function (ev) {
                    $('#mdlQuotations').modal('show');
                    $('#mdlRequiredQty').modal('hide');
                    $("#mainQuantity").text("");
                    $("#txtamount").val("");
                    ev.preventDefault();

                });

                $("#btnadd").click(function (ev) {

                    if ($('#ContentSection_PurchaseType').val() == "L") {


                        var isEdit = $("#ISEditedAgian").val();
                        if (isEdit != "1") {
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);

                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= requiredqty && qty != 0) {

                                var hdnrejectval = $("#ContentSection_hdnrejected").val();
                                if (hdnrejectval != null && hdnrejectval != "") {
                                    var arr = hdnrejectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnrejected").val(arr);
                                }
                                var hdnchangeval = $("#ContentSection_hdnSelectedChanged").val();
                                if (hdnchangeval != null && hdnchangeval != "") {
                                    var arr = hdnchangeval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnSelectedChanged").val(arr);
                                }
                                var hdnval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnval != null && hdnval != "") {
                                    var b = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    var arr = hdnval.split(",");
                                    var conctarr = $.merge($.merge([], arr), b);
                                    $("#ContentSection_hdnSlectedQutations").val(conctarr);

                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSlectedQutations").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).html(val + "*" + qty);
                            }
                            else {
                                $('#mdlRequiredQty').modal('hide');
                                swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                .then(function (isConfirm) {
                                    if (isConfirm) {
                                        $('#mdlRequiredQty').modal('show');
                                        $('div').removeClass('modal-backdrop');
                                    }
                                });

                            }
                            //}
                            //else {
                            //    $('#mdlRequiredQty').modal('hide');
                            //    swal({ type: 'error', title: 'ERROR', text: 'Please enter value', showConfirmButton: true })
                            //        .then(function (isConfirm) {
                            //            if (isConfirm) {
                            //                $('#mdlRequiredQty').modal('show');
                            //                $('div').removeClass('modal-backdrop');
                            //            }
                            //        });
                            //}
                        }

                        else {
                            console.log($("#previousqty").val());
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);
                            var prqty = parseInt($("#previousqty").val());
                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= (requiredqty + prqty) && qty != 0) {

                                var hdnselectval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnselectval != null && hdnselectval != "") {
                                    var arr = hdnselectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnSlectedQutations").val(arr);
                                }
                                var hdnrejectval = $("#ContentSection_hdnrejected").val();
                                if (hdnrejectval != null && hdnrejectval != "") {
                                    var arr = hdnrejectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);

                                        }
                                    }
                                    $("#ContentSection_hdnrejected").val(arr);
                                }

                                var hdnval = $("#ContentSection_hdnSelectedChanged").val();
                                if (hdnval != null && hdnval != "") {

                                    var arr = hdnval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr[i + 5] = qty;
                                        }
                                    }
                                    $("#ContentSection_hdnSelectedChanged").val(arr);


                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSelectedChanged").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0] + "*" + qty);
                                //    }
                                //    else {
                                //        $('#mdlRequiredQty').modal('hide');
                                //        swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                //        .then(function (isConfirm) {
                                //            if (isConfirm) {
                                //                $('#mdlRequiredQty').modal('show');
                                //                $('div').removeClass('modal-backdrop');
                                //            }
                                //        });

                                //    }
                            }
                            else {
                                $('#mdlRequiredQty').modal('hide');
                                swal({ type: 'error', title: 'ERROR', text: 'Please enter value', showConfirmButton: true })
                                    .then(function (isConfirm) {
                                        if (isConfirm) {
                                            $('#mdlRequiredQty').modal('show');
                                            $('div').removeClass('modal-backdrop');
                                        }
                                    });
                            }
                        }

                    }
                    else {


                        var isEdit = $("#ISEditedAgian").val();
                        if (isEdit != "1") {
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);

                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= requiredqty && qty != 0) {

                                var hdnrejectval = $("#ContentSection_hdnrejected").val();
                                if (hdnrejectval != null && hdnrejectval != "") {
                                    var arr = hdnrejectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnrejected").val(arr);
                                }
                                var hdnchangeval = $("#ContentSection_hdnSelectedChanged").val();
                                if (hdnchangeval != null && hdnchangeval != "") {
                                    var arr = hdnchangeval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnSelectedChanged").val(arr);
                                }
                                var hdnval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnval != null && hdnval != "") {
                                    var b = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    var arr = hdnval.split(",");
                                    var conctarr = $.merge($.merge([], arr), b);
                                    $("#ContentSection_hdnSlectedQutations").val(conctarr);

                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSlectedQutations").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val + "*" + qty);
                            }
                            else {
                                $('#mdlRequiredQty').modal('hide');
                                swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                .then(function (isConfirm) {
                                    if (isConfirm) {
                                        $('#mdlRequiredQty').modal('show');
                                        $('div').removeClass('modal-backdrop');
                                    }
                                });

                            }
                            //}
                            //else {
                            //    $('#mdlRequiredQty').modal('hide');
                            //    swal({ type: 'error', title: 'ERROR', text: 'Please enter value', showConfirmButton: true })
                            //        .then(function (isConfirm) {
                            //            if (isConfirm) {
                            //                $('#mdlRequiredQty').modal('show');
                            //                $('div').removeClass('modal-backdrop');
                            //            }
                            //        });
                            //}
                        }

                        else {
                            console.log($("#previousqty").val());
                            var requiredqty = parseInt($("#mainQuantity").text().split(':')[1]);
                            var prqty = parseInt($("#previousqty").val());
                            if ($("#txtamount").val() != "") {
                                var qty = parseInt($("#txtamount").val());
                                //if (qty <= (requiredqty + prqty) && qty != 0) {

                                var hdnselectval = $("#ContentSection_hdnSlectedQutations").val();
                                if (hdnselectval != null && hdnselectval != "") {
                                    var arr = hdnselectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);
                                        }
                                    }
                                    $("#ContentSection_hdnSlectedQutations").val(arr);
                                }
                                var hdnrejectval = $("#ContentSection_hdnrejected").val();
                                if (hdnrejectval != null && hdnrejectval != "") {
                                    var arr = hdnrejectval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr.splice(i, 6);

                                        }
                                    }
                                    $("#ContentSection_hdnrejected").val(arr);
                                }

                                var hdnval = $("#ContentSection_hdnSelectedChanged").val();
                                if (hdnval != null && hdnval != "") {

                                    var arr = hdnval.split(",");
                                    for (var i = 0; i < arr.length; i += 6) {

                                        if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                            arr[i + 5] = qty;
                                        }
                                    }
                                    $("#ContentSection_hdnSelectedChanged").val(arr);


                                }
                                else {
                                    var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                    $("#ContentSection_hdnSelectedChanged").val(a);

                                }

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('CellClick');
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('greenBg');

                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0] + "*" + qty);
                                //    }
                                //    else {
                                //        $('#mdlRequiredQty').modal('hide');
                                //        swal({ type: 'error', title: 'ERROR', text: 'Insert value is more than required', showConfirmButton: true })
                                //        .then(function (isConfirm) {
                                //            if (isConfirm) {
                                //                $('#mdlRequiredQty').modal('show');
                                //                $('div').removeClass('modal-backdrop');
                                //            }
                                //        });

                                //    }
                            }
                            else {
                                $('#mdlRequiredQty').modal('hide');
                                swal({ type: 'error', title: 'ERROR', text: 'Please enter value', showConfirmButton: true })
                                    .then(function (isConfirm) {
                                        if (isConfirm) {
                                            $('#mdlRequiredQty').modal('show');
                                            $('div').removeClass('modal-backdrop');
                                        }
                                    });
                            }
                        }


                    }


                    ev.preventDefault();

                });

                $("#btncancel").click(function (ev) {
                    if ($('#ContentSection_PurchaseType').val() == "L") {

                        console.log($("#previousqty").val());
                        if ($("#previousqty").val() != "" && $("#previousqty").val() != undefined) {
                            var prqty = parseInt($("#previousqty").val());

                            var hdnselectval = $("#ContentSection_hdnSlectedQutations").val();
                            if (hdnselectval != null && hdnselectval != "") {
                                var arr = hdnselectval.split(",");
                                for (var i = 0; i < arr.length; i += 6) {

                                    if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                        arr.splice(i, 6);
                                    }
                                }
                                $("#ContentSection_hdnSlectedQutations").val(arr);
                            }

                            var hdnchangeval = $("#ContentSection_hdnSelectedChanged").val();
                            if (hdnchangeval != null && hdnchangeval != "") {
                                var arr = hdnchangeval.split(",");
                                for (var i = 0; i < arr.length; i += 6) {

                                    if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                        arr.splice(i, 6);
                                    }
                                }
                                $("#ContentSection_hdnSelectedChanged").val(arr);
                            }


                            var hdnval = $("#ContentSection_hdnrejected").val();
                            if (hdnval != null && hdnval != "") {

                                var arr = hdnval.split(",");
                                for (var i = 0; i < arr.length; i += 6) {

                                    if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                        arr[i + 5] = qty;
                                    }
                                }
                                $("#ContentSection_hdnrejected").val(arr);


                            }
                            else {
                                var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                $("#ContentSection_hdnrejected").val(a);

                            }

                            if ($('#ContentSection_PurchaseType').val() == "L") {
                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).removeClass('greenBg');
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).addClass('CellClick');
                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0]);
                            }
                            else {

                                var row = parseInt($("#Rowno").val());
                                var cell = parseInt($("#cellno").val());
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('greenBg');
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('CellClick');
                                $('#mdlQuotations').modal('show');
                                $('#mdlRequiredQty').modal('hide');
                                $("#mainQuantity").text("");
                                $("#txtamount").val("");
                                $("#previousqty").val("");
                                $("#ISEditedAgian").val("");
                                var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                                $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0]);
                                    
                            }

                        }

                        else {
                            $('#mdlRequiredQty').modal('hide');
                            swal({ type: 'error', title: 'ERROR', text: 'Cannot Cancel selection', showConfirmButton: true })
                                .then(function (isConfirm) {
                                    if (isConfirm) {
                                        $('#mdlRequiredQty').modal('show');
                                        $('div').removeClass('modal-backdrop');
                                    }
                                });
                        }
                    }
                    else {
                        console.log($("#previousqty").val());
                        if ($("#previousqty").val() != "" && $("#previousqty").val() != undefined) {
                            var prqty = parseInt($("#previousqty").val());

                            var hdnselectval = $("#ContentSection_hdnSlectedQutations").val();
                            if (hdnselectval != null && hdnselectval != "") {
                                var arr = hdnselectval.split(",");
                                for (var i = 0; i < arr.length; i += 6) {

                                    if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                        arr.splice(i, 6);
                                    }
                                }
                                $("#ContentSection_hdnSlectedQutations").val(arr);
                            }

                            var hdnchangeval = $("#ContentSection_hdnSelectedChanged").val();
                            if (hdnchangeval != null && hdnchangeval != "") {
                                var arr = hdnchangeval.split(",");
                                for (var i = 0; i < arr.length; i += 6) {

                                    if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                        arr.splice(i, 6);
                                    }
                                }
                                $("#ContentSection_hdnSelectedChanged").val(arr);
                            }


                            var hdnval = $("#ContentSection_hdnrejected").val();
                            if (hdnval != null && hdnval != "") {

                                var arr = hdnval.split(",");
                                for (var i = 0; i < arr.length; i += 6) {

                                    if ($("#QutatuonId").val() == arr[i + 1] && $("#ItemId").val() == arr[i + 4]) {
                                        arr[i + 5] = qty;
                                    }
                                }
                                $("#ContentSection_hdnrejected").val(arr);


                            }
                            else {
                                var a = [$("#TabulationId").val(), $("#QutatuonId").val(), $("#BidId").val(), $("#SupplierId").val(), $("#ItemId").val(), $("#txtamount").val()];
                                $("#ContentSection_hdnrejected").val(a);

                            }

                            var row = parseInt($("#Rowno").val());
                            var cell = parseInt($("#cellno").val());
                            $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).removeClass('greenBg');
                            $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).addClass('CellClick');
                            $('#mdlQuotations').modal('show');
                            $('#mdlRequiredQty').modal('hide');
                            $("#mainQuantity").text("");
                            $("#txtamount").val("");
                            $("#previousqty").val("");
                            $("#ISEditedAgian").val("");
                            var val = $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).text();
                            $('#ContentSection_gvImpotsQuotations tr').eq(row).find('td').eq(cell).html(val.split('*')[0]);

                        }

                        else {
                            $('#mdlRequiredQty').modal('hide');
                            swal({ type: 'error', title: 'ERROR', text: 'Cannot Cancel selection', showConfirmButton: true })
                                .then(function (isConfirm) {
                                    if (isConfirm) {
                                        $('#mdlRequiredQty').modal('show');
                                        $('div').removeClass('modal-backdrop');
                                    }
                                });
                        }

                    }
                        ev.preventDefault();

                    });

                    $('.Cancelselct').on({
                        click: function () {
                            event.preventDefault();
                            $('#mdlRequiredQty').modal('hide');
                            $('#mdlQuotations').modal('show');
                            $('div').removeClass('modal-backdrop');
                        }

                    });
                    $('#clearDocs').on({
                        click: function () {
                            event.preventDefault();
                            $('#ContentSection_fileUpload1').val("");
                        }

                    });
                    $('.CanceldocView').on({
                        click: function () {
                            event.preventDefault(); 
                            $('#mdlviewdocstechCommitee').modal('hide');
                            $('#mdlRecommendations').modal('show');
                            $('div').removeClass('modal-backdrop');
                        }

                    });
                    $('.CanceldocUpload').on({
                        click: function () {
                            event.preventDefault();
                            $('#mdlviewdocsuplod').modal('hide');
                            $('#mdlQuotations').modal('show');
                            $('div').removeClass('modal-backdrop');
                        }

                    });


               
            });


        });
    </script>

</asp:Content>
