<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewSubmittedForBidListing.aspx.cs" Inherits="BiddingSystem.ViewSubmittedForBidListing" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

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
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/moment.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" />
    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <style type="text/css">
        
        @media screen
        {
            .divPrint{display:none;}
        }
        .ChildGrid td {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height: 200%;
            text-align: center;
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

        .GridViewEmptyText {
            color: Red;
            font-weight: bold;
            font-size: 14px;
        }

        .addButton {
            padding: 2px 6px 2px 6px;
            border-radius: 11px;
            background-color: red;
            color: white;
        }

        .searchBox {
            border-radius: 12px;
            padding-left: 6px;
        }

        span.expand_caret {
            transform: scale(1.6);
            margin-left: 8px;
            margin-top: -4px;
        }

        a[aria-expanded='false'] > h4 > span.expand_caret {
            transform: scale(1.6) rotate(-90deg);
        }
        .margin{
            margin-left:5px
        }
    </style>

    <section class="content-header" >
    <h1>
     Update Procurement Plan/Send Email to Suppliers
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Update Procurement Plan/Send Email to Suppliers </li>
      </ol>
    </section>
    <br />


    <form runat="server">
        <asp:HiddenField ID="hdnField" runat="server" />

        <asp:ScriptManager runat="server" EnablePartialRendering="true"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <section class="content" style="padding-top: 0px" id="divPrintPo">

            <div id="modalSendEmail" class="modal fade" style="z-index: 9999;">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                <h4 id="" class="modal-title">Confirmation</h4>
                            </div>
                            <div class="modal-body">
                                <asp:Label Visible ="false" id="lblEmailMessage" runat="server" style="color:green;font-size:12px;font-weight:800" Text="Email Sent Successfully" > </asp:Label>
                                <p>Are you sure you have updated all details ?</p>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary"  Text="Yes"></asp:Button>
                                <button  onclick="return hideSendEmailModal();" type="button" class="btn btn-danger">No</button>
                            </div>
                        </div>
                    </div>
                </div>
                    
            <div id="mdlBiddingplan" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index:3001" aria-hidden="true">
                    <div class="modal-dialog" style="width: 900px;">
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Procurement Plan</h4>
                            </div>
                            <div class="modal-body" >
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="dvBiddingplan" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;" GridLines="None"
                                                    AutoGenerateColumns="false" OnRowDataBound="dvBiddingplan_RowDataBound" EmptyDataText="No Bidding Plan Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="BidId" HeaderText="PR Id"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="PlanId" HeaderText="Plan Id"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="Planname" HeaderText="Plan" />
                                                        <asp:BoundField DataField="StartDate"  HeaderText="Start Date" DataFormatString='<%$ appSettings:dateTimePattern %>'   />
                                                        <asp:BoundField DataField="EndDate"  HeaderText="End Date" DataFormatString='<%$ appSettings:dateTimePattern %>'  />
                                                         <asp:BoundField DataField="WithTime"  HeaderText="With Time" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"  />
                                                        <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIscompleted" runat="server" Text='<%# Eval("Iscompleted").ToString() =="1" ? "Completed":"Pending" %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblactualdate" runat="server" Text='<%# Eval("ActualDate").ToString() =="1/1/0001 12:00:00 AM" ? "-":DateTime.Parse(Eval("ActualDate").ToString()).ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]) %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" Visible="false" ID="lblComplete" OnClick="lblComplete_Click" OnClientClick="actualplanscroll()" >Complete</asp:LinkButton>
                                                                <asp:LinkButton runat="server" Visible="false"  ID="lblview" OnClick="lblview_Click">View</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server"  Visible="false" ID="lbtnEdit" OnClientClick="editscroll()" OnClick="lbtnEdit_Click">Update</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                 <asp:Button ID="btnSendEmail" class="btn btn-success" Visible="false" runat="server" OnClientClick="SendEmail(this)"  Text="Send Changes To Supplier" />
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

            <div id="mdlBiddingPlanDocs" class="modal modal-primary fade" tabindex="-1" style="z-index:3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close clmdlBiddingPlanDocs" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Bidding Plan Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvbddinplanfiles" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="filename" HeaderText="File Name" />
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="sequenceId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnDownload" OnClick="lbtnDownload_Click">Download</asp:LinkButton>
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


            <div class="box box-info" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Procurement Plan</h3>
                        </div>
                          <div class="box-body">
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-4">
                                    <address>
                                        <strong>WAREHOUSE: </strong>
                                        <asp:Label runat="server" ID="lblWarehouse"></asp:Label><br>
                                        <strong>PR NO : </strong>
                                        <a Title="Click To Go To PR" data-toggle="tooltip" data-placement="bottom" onclick="ViewPr()" class="text-navy" style="cursor:pointer;font-size: 16px;">
                                         <asp:Label ID="lblPRNo" runat="server" Text="" CssClass="label label-info"></asp:Label> </a><br />
                                        <strong>PR CREATED ON : </strong>
                                        <asp:Label ID="lblCreatedOn" runat="server" Text=""></asp:Label><br />
                                        <strong>PR CREATED BY : </strong>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-4">
                                    <address>
                                        <strong>CATEGORY: </strong>
                                        <asp:Label runat="server" ID="lblCategory"></asp:Label><br>
                                        <strong>SUB-CATEGORY: </strong>
                                        <asp:Label runat="server" ID="lblSubCategory"></asp:Label><br>
                                        <strong>REQUESTED FOR : </strong>
                                        <asp:Label ID="lblRequestFor" runat="server" Text=""></asp:Label><br />
                                        <strong>EXPENSE TYPE : </strong>
                                        <asp:Label ID="lblExpenseType" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                  <div class="col-md-4 col-sm-4 col-xs-4">
                                      <address>
                                        <strong>EXPECTED DATE: </strong>
                                        <asp:Label runat="server" ID="lblExpectedDate"></asp:Label><br>
                                        <strong>BID CREATED BY : </strong>
                                        <asp:Label runat="server" ID="lblBidCreatedBy"></asp:Label><br>
                                        <strong>BID CREATED ON : </strong>
                                        <asp:Label runat="server" ID="lblBidCreatedOn"></asp:Label><br>
                                          <strong>Purchase Type </strong>
                                        <asp:Label ID="lblPurchaseType" runat="server" Text=""></asp:Label><br />
                                        <div  runat="server" Visible="false" id="divMrnReferenceCode">
                                            <strong>MRN Reference Code: </strong>
                                            <a Title="Click To Go To MRN" data-toggle="tooltip" data-placement="bottom" onclick="ViewMrn()" class="text-navy" style="cursor:pointer;font-size: 16px;">
                                            <asp:Label runat="server" ID="lblMrnReferenceCode"></asp:Label></a>
                                        </div>                        
                                    </address>
                                  </div>
                            </div>
                          
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvBids" GridLines="None" CssClass="table table-responsive table-striped "
                                            AutoGenerateColumns="false" DataKeyNames="BidId" OnRowDataBound="gvBids_RowDataBound" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                                            EmptyDataText="No records Found">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                                                        <asp:Panel ID="pnlBidItems" runat="server" Style="display: none">
                                            <asp:GridView ID="gvBidItems" runat="server" CssClass="table table-responsive ChildGrid"
                                                GridLines="None" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="BiddingItemId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="BidItemId hidden" ItemStyle-CssClass="BidItemId hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="BidItemId hidden" ItemStyle-CssClass="BidItemId hidden" />
                                                    <asp:BoundField DataField="PrdId" HeaderText="PRDId"
                                                        HeaderStyle-CssClass="PRDId hidden" ItemStyle-CssClass="PRDId hidden" />
                                                    <asp:BoundField DataField="CategoryId" HeaderText="Category Id"
                                                        HeaderStyle-CssClass="CategoryId hidden" ItemStyle-CssClass="CategoryId hidden" />
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                                    <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="ItemId hidden" ItemStyle-CssClass="ItemId hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                   <%-- <asp:BoundField DataField="Qty" HeaderText="Quantity"/>--%>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:label ID="lblInventory"  type="text" runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price (Unit)"/>                                                   
                                                      <asp:TemplateField HeaderText="More Info" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                                        <ItemTemplate>
                                                            <a title="Remarks" data-toggle="tooltip" data-placement="top" onclick="ViewPr()" class="text-aqua" style="cursor:pointer">View</a>                                                           
                                                        </ItemTemplate>
                                                     </asp:TemplateField>   
                                                </Columns>
                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="BidId" HeaderText="BidId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="Bid Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# "B"+Eval("BidCode").ToString() %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:BoundField DataField="CreatedUserName" HeaderText="Created By" />
                                                <asp:BoundField DataField="CreateDate" HeaderText="Created Date" DataFormatString='<%$ appSettings:datePattern %>'
                                                    />
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString='<%$ appSettings:datePattern %>'
                                                     />
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString='<%$ appSettings:datePattern %>'
                                                     />
                                                <asp:TemplateField HeaderText="Bid Opened For">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("BidOpeningPeriod").ToString()+" Days" %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bid Type">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("BidOpenType").ToString() =="1" ? "Online":Eval("BidOpenType").ToString() =="2" ? "Manual":"Online & Manual" %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bid Status">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Eval("IsApproved").ToString() =="0" ? "Pending":Eval("IsApproved").ToString() =="1" ? "Approved":"Rejected" %>' ForeColor='<%# Eval("IsApproved").ToString() =="0" ? System.Drawing.Color.DeepSkyBlue:Eval("IsApproved").ToString() =="1" ? System.Drawing.Color.Green:System.Drawing.Color.Red %>'/>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bidding Plan" ItemStyle-CssClass="no-print" HeaderStyle-CssClass="no-print">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnBddingplan" OnClick="lbtnBddingplan_Click">View</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

            <div class="box box-info" visible="false" id="dvcomplete" runat="server">
                <div class="box-header with-border">
                    <h3 class="box-title">Bidding Plan Completing & Upload Documents</h3>
                </div>
                <div class="box-body">
                <div class="row">
                    <div class="col-md-4 col-sm-6 col-xs-12">
                        <address>
                            <strong>Plan : </strong>
                            <asp:Label ID="lblPlan" runat="server" Text=""></asp:Label><br />
                            <strong>Start Date : </strong>
                            <asp:Label ID="lblStart"  DataFormatString='<%$ appSettings:dateTimePattern %>' runat="server" Text=""></asp:Label><br />
                            <strong>End Date : </strong>
                            <asp:Label ID="lblEndDate"  DataFormatString='<%$ appSettings:dateTimePattern %>' runat="server" Text=""></asp:Label><br />
                        </address>
                    </div>
                    <div class="col-md-4 col-sm-6 col-xs-12">
                    </div>
                </div>
                <div class="row col-sm-7">
                        <div class="form-group">
                            <div class="col-sm-12" style="padding-left: 0px;">
                                <label for="exampleInputEmail1">Actual Date</label><label id="Label6" style="color:red"></label>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtactualDate" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator9" ForeColor="Red">*</asp:RequiredFieldValidator>                
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtactualTime" InitialValue="" ValidationGroup="btnAdd" ID="RequiredFieldValidator3" ForeColor="Red">*</asp:RequiredFieldValidator>                
                            </div>
                                   
                        <div class="col-sm-6" style="padding-left: 0px;">
                            <asp:TextBox runat="server" ID="txtactualDate"   AutoPostBack="true"  type="date" data-date="" data-date-format="DD MMMM YYYY" CssClass="form-control customDate"/>                                      
                        </div>
                                <div class="col-sm-6">
                            <asp:TextBox ID="txtactualTime" runat="server" style="float: right" CssClass="form-control"
                                        type="time" autocomplete="off"  data-time="" data-time-format="hh:mm tt" ></asp:TextBox>
                                    </div>
                            </div>
                        <div class="form-group">
                        <label for="exampleInputEmail1">Is Completed</label>
                        <asp:CheckBox ID="chkIsCompleted" CssClass="form-control"  runat="server" Checked="true"></asp:CheckBox>
                        </div>
                        <div class="form-group">                                
                    <div class="col-sm-10">
                        <label for="exampleInputEmail1">Upload Documents</label>
                            <asp:FileUpload runat="server" style="display:inline;" AllowMultiple="true"   CssClass="form-control" ID="fileUpload1" ></asp:FileUpload>
                    </div>
                    <div class="col-sm-2" style="padding-top:25px"> <button class="btn btn-info btn-flat clear"  id="clearDocs" >Clear</button></div>
                </div>
                </div>
                </div>
                <div class="box-footer">
                    <div class="form-group">
               <asp:Button ID="btnClear" runat="server" Text="Cancel" 
                  CssClass="btn btn-danger pull-right" onclick="btnClear_Click"/>
              <asp:Button ID="btnAdd" ValidationGroup="btnAdd" runat="server" Text="Save" 
                  CssClass="btn btn-primary pull-right" onclick="btnAdd_Click" style="margin-right:10px" />              
          </div>
                </div>
            </div>

            <div class="box box-info" visible="false" id="dvUpdateplan" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Bidding Plan Update</h3>
                        </div>
                          <div class="box-body">
                            <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Plan : </strong>
                                        <asp:Label ID="lblupdatePlan" runat="server" Text=""></asp:Label><br />                                    
                                    </address>
                                </div>
                              <%--  <div class="col-md-4 col-sm-6 col-xs-12"></div>--%>
                            </div>

                                  <div class="row">
                                  <div class="col-md-6">
                                      <div class="form-group">
                                     <label for="exampleInputEmail1" style="float: left;">Start Date : </label>
                                       <asp:RequiredFieldValidator runat="server" ControlToValidate="txtstart"  Display="Dynamic" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator2" ForeColor="Red">*Please select date</asp:RequiredFieldValidator>                
                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator29"  Display="Dynamic"  ForeColor="Red" Font-Bold="true" ControlToValidate="txtStartDTime" ValidationGroup="btnUpdate">*Please select time</asp:RequiredFieldValidator>
                                 
                                          <asp:TextBox ID="txtstart" runat="server" CssClass="form-control customDate" style="width: 40%;float: left;"  onchange="dateChange(this)"   type="date" data-date="" data-date-format="DD MMM YYYY"></asp:TextBox>                                   
                                      <asp:TextBox ID="txtStartDTime" runat="server" style="width: 40%;float: right" CssClass="form-control" type="time" autocomplete="off"  data-time="" data-time-format="hh:mm tt" ></asp:TextBox>    
                                  </div>

                                      </div>

                                  <div class="col-md-6">
                                       <div class="form-group">                                      
                                       <label for="exampleInputEmail1" style="float: left;">End Date : </label>
                                       <asp:RequiredFieldValidator runat="server" ControlToValidate="txtend"  Display="Dynamic" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator1" ForeColor="Red">*Please select date</asp:RequiredFieldValidator>  
                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator779"  Display="Dynamic" ForeColor="Red" Font-Bold="true" ControlToValidate="txtEndDTime" ValidationGroup="btnUpdate">*Please select time</asp:RequiredFieldValidator>                                      
                                     
                                           <asp:TextBox ID="txtend" runat="server" CssClass="form-control customDate" style="width: 40%;float: left;"   onchange="dateChange(this)"  type="date" data-date="" data-date-format="DD MMM YYYY"></asp:TextBox>                                       
                                       <asp:TextBox ID="txtEndDTime" runat="server" style="width: 40%;float: right" CssClass="form-control" type="time"  data-time="" data-time-format="hh:mm tt"  
                                                    autocomplete="off"  ></asp:TextBox>   
                                   </div>

                                      </div>
                                  </div>
                            <%--  <div class="row col-sm-7">
                                  <div class="form-group">
                                   <asp:RequiredFieldValidator runat="server" ControlToValidate="txtstart"  Display="Dynamic" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator2" ForeColor="Red">*Please select date</asp:RequiredFieldValidator>                
                                 <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator29"  Display="Dynamic"  ForeColor="Red" Font-Bold="true" ControlToValidate="txtStartDTime" ValidationGroup="btnUpdate">*Please select time</asp:RequiredFieldValidator>
                                   </div>
                                  <div class="form-group">
                                     <label for="exampleInputEmail1" style="float: left;">Start Date : </label>
                                      <asp:TextBox ID="txtstart" runat="server" CssClass="form-control customDate" style="width: 40%;float: left;"  onchange="dateChange(this)"   type="date" data-date="" data-date-format="DD MMM YYYY"></asp:TextBox>                                   
                                      <asp:TextBox ID="txtStartDTime" runat="server" style="width: 40%;float: right" CssClass="form-control" type="time" autocomplete="off"  data-time="" data-time-format="hh:mm tt" ></asp:TextBox>    
                                  </div>
                                  <div class="form-group">
                                       <asp:RequiredFieldValidator runat="server" ControlToValidate="txtend"  Display="Dynamic" InitialValue="" ValidationGroup="btnUpdate" ID="RequiredFieldValidator1" ForeColor="Red">*Please select date</asp:RequiredFieldValidator>  
                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator779"  Display="Dynamic" ForeColor="Red" Font-Bold="true" ControlToValidate="txtEndDTime" ValidationGroup="btnUpdate">*Please select time</asp:RequiredFieldValidator>                                      
                                      </div>
                                   <div class="form-group">                                      
                                       <label for="exampleInputEmail1" style="float: left;">End Date : </label>
                                       <asp:TextBox ID="txtend" runat="server" CssClass="form-control customDate" style="width: 40%;float: left;"   onchange="dateChange(this)"  type="date" data-date="" data-date-format="DD MMM YYYY"></asp:TextBox>                                       
                                       <asp:TextBox ID="txtEndDTime" runat="server" style="width: 40%;float: right" CssClass="form-control" type="time"  data-time="" data-time-format="hh:mm tt"  
                                                    autocomplete="off"  ></asp:TextBox>   
                                   </div>

                              </div>--%>
                          
                        </div>
                         
                          <div class="box-footer">
        <div class="form-group">
              
               <asp:Button ID="btneditcancel" runat="server" Text="Cancel" 
                  CssClass="btn btn-danger pull-right" onclick="btneditcancel_Click"/>
              <asp:Button ID="btnUpdate" ValidationGroup="btnAdd" runat="server" Text="Update" 
                  CssClass="btn btn-primary pull-right" onclick="btnUpdate_Click" style="margin-right:10px" />
              
          </div>
        </div>
                     </div>
                     

            <div class="box box-info"  id="divSendEmail" runat="server">
                        <div class="box-header with-border no-print">
                            <h3 class="box-title" runat="server" id="EmailHeader">Send Email To Suppliers </h3>
                        </div>
                        <!-- /.box-header -->

                          <div class="box-body">
                            <div class="row">
                                
                                <div class="col-md-2 no-print" runat="server" id="divBid" Visible="false " >
                                        <strong>Select Bid Id : </strong>    
                                       <asp:DropDownList ID="ddlBidId" runat="server"  ></asp:DropDownList>  
                                     <label runat="server" id="BidIdEMa" style="color:red" visible="false">* Please Select Bid Code </label>                                    
                                </div>
                                <div class="col-sm-12 no-print">
                                <div class="col-md-3" >
                                        <h4><span>Select Suppliers :   <label style="color:red"> *</label>  
                                         <a id="linkAddNewSupplier" Title="Add Unregistered Suppliers" data-toggle="tooltip" data-placement="bottom"  class="text-navy" style="cursor:pointer;font-size: 16px;">
                                             <asp:Button ID="btnAddNewSupplier" CssClass="addButton" runat="server" Text="+"  OnClick="btnShowNewSupplier_Click" />
                                          </a>  
                                      </span></h4>
                                       <label runat="server" id="supplierEmailError" style="color:red" visible="false">* Please Select atleast one Supplier</label> 
                                </div>
                                    <div class="col-md-5" style="text-align: right;padding-bottom: 5px;">
                                        <asp:Label ID="lblSupplierSearch" style="padding-right: 80px;color: red;" runat="server" Text="No Record Found" Visible="false" ></asp:Label>
                                      <asp:TextBox ID="txtSupplierSearch" runat="server" cssclass="searchBox" style="width: 75%;height: 35px;" PlaceHolder="Search By Name" onkeyup="SearchSupplier()" ></asp:TextBox>
                                        <asp:Button ID="btnSupplierSearch" runat="server" OnClick="btnSupplierSearch_Click" Autopostback="true" style="display:none"/>
                                    </div>
                                </div>
                                <div class="col-sm-12 no-print">         
                                     <div class="col-md-2"></div>                           
                                <div class="col-md-6" style="height: 250px;overflow: auto">
                                    <asp:GridView ID="gvSupplier" runat="server" CssClass="table table-responsive"   HeaderStyle-CssClass="Freezing "
                                     GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                                        EmptyDataText="No Records Found" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="GridViewEmptyText">
                                                    <Columns>                 
                                                    <asp:BoundField DataField="SupplierId" HeaderText="supplierId" 
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" > </asp:BoundField>
                                                        <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                        <asp:CheckBox ID="ckSupplier"  Visible='<%# Eval("supplierId").ToString() != "0" ? true : false %>' runat="server" />
                                                     </ItemTemplate>
                                                            </asp:TemplateField>                                                        
                                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name"/>
                                                    <asp:BoundField DataField="Email" HeaderText="Email" />   
                                                         <asp:TemplateField HeaderText="Supplier registered Status">
                                                                <ItemTemplate>
                                                                <asp:Label ID="lblSupplierregisteredStatus" CssClass="label label-info" runat="server" Text='<%# Eval("IsRegisteredSupplier").ToString() =="1" ? "Registered":"Not Registred" %>'></asp:Label>
                                                           </ItemTemplate>
                                                            </asp:TemplateField> 
                                               </Columns>
                                            </asp:GridView>
                                </div>
                                <div class="col-sm-6"></div>
                                </div>

                               


                                <div id="divTempSupplier" class="col-sm-12" runat="server" Visible="false">
                                    <div class="box-header with-border">
                                        <h3 class="box-title" runat="server" id="H1">Add emails for unregistered suppliers </h3>
                                    </div>
                                     <div class="box-body">
                            <div class="row">
                                 <div class="col-md-12 form-group">
                                     <div class="col-md-5" style="padding-left: 0px;">
                                            <div class="col-md-5" style="padding-left: 0px;">
                                                <h4 class="col-sm-12">Supplier Name</h4>                                                
                                                <label id="lblEmSupName" style="color:red;font-size:12px;display:none;">*Fill This Field</label>   
                                                </div>
                                           <div class="col-md-6">  
                                               <asp:TextBox ID="txtSupplierName"  runat="server" ValidationGroup="btnSupplierTempEmail"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                      </div>
                                      <div class="col-md-5">
                                            <div class="col-md-5" style="padding-left: 0px;">
                                                <h4 for="lbl" class="col-sm-12" >Email Address</h4>
                                                <label id="lblEmSupAddres" style="color:red;font-size:12px;display:none;">*Fill This Field</label>   
                                            </div>
                                            <div class="col-md-6">             
                                                <asp:TextBox ID="txtSupplierEmailAddress" type="email" ValidationGroup="btnSupplierTempEmail" runat="server"  CssClass="form-control"></asp:TextBox>
                                            </div>
                                          </div>
                                     <div class="col-md-2">
                                          <asp:Button runat="server" ID="btnAddNewTempSupplier"   CssClass="btn btn-success" autopostback="true"  
                                                Text="Add"  onclick="btnAddNewTempSupplier_Click"   />
                                       </div>
                                        </div>

                                 <div runat="server" id="dvTempSupplier"  class="col-md-12" visible="false" >
                                     <div class="col-md-6">
                                      <div class="table-responsive">
                                          <asp:GridView ID="gvSupplierTempEmail" runat="server" CssClass="table table-responsive"  
                                                   GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                                                    <asp:BoundField DataField="UserId" HeaderText="Supplier Id" 
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ContactOfficer" HeaderText="Supplier Name"/>
                                                   <asp:BoundField DataField="Email" HeaderText="Email Address" />
                                                     <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Button CssClass="btn btn-danger btn-xs" ID="Delete"
                                                                        runat="server" OnClick="gvSupplierTempEmail_Delete_Click"
                                                                        Text="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>
                                        </div>
                                       </div>

                                      </div>
                                </div>
                                         </div>
                                  <div class="col-sm-12"><hr /></div>
                                 </div>



                                  


                               

             <div class="col-md-12">
                    <div class="box-header with-border no-print">
                        <h4><span>Add one or more contact officer details<span style="color:red"> *</span></span></h4>
                        <label runat="server" id="lblErrorOfficersContact"  style="color:red" visible="false"> Please Add Atleast One Contact</label>
                    </div>
                    <div class="box-body">




                        <div class="row divPrint">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="dvBiddingplanPrint" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;" GridLines="None" OnRowDataBound="dvBiddingplanPrint_RowDataBound" Caption="Procurement Plan details"
                                                    AutoGenerateColumns="false" EmptyDataText="No Bidding Plan Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="BidId" HeaderText="PR Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                        <asp:BoundField DataField="PlanId" HeaderText="Plan Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                        <%--<asp:BoundField DataField="BidCode" HeaderText="Bid Code" />--%>
                                                        <asp:BoundField DataField="Planname" HeaderText="Plan" />
                                                        <asp:BoundField DataField="StartDate"  HeaderText="Start Date" DataFormatString='<%$ appSettings:dateTimePattern %>'   />
                                                        <asp:BoundField DataField="EndDate"  HeaderText="End Date" DataFormatString='<%$ appSettings:dateTimePattern %>'  />
                                                         <asp:BoundField DataField="WithTime"  HeaderText="With Time" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"  />
                                                        <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIscompleted" runat="server" Text='<%# Eval("Iscompleted").ToString() =="1" ? "Completed":"Pending" %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblactualdate" runat="server" Text='<%# Eval("ActualDate").ToString() =="1/1/0001 12:00:00 AM" ? "-":DateTime.Parse(Eval("ActualDate").ToString()).ToString(System.Configuration.ConfigurationSettings.AppSettings["dateTimePatternBackend"]) %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       
                                                    </Columns>
                                                </asp:GridView>
                                                    </div>
                                        </div>
                                      
                                    </div>




                         <div class="row divPrint">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GvItemSpec" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;" GridLines="None" Caption="Item Specifications"
                                                    AutoGenerateColumns="false" EmptyDataText="No Bidding Plan Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                        <asp:BoundField DataField="Meterial" HeaderText="Material"/>
                                                        <asp:BoundField DataField="Description" HeaderText="Description"/>
                                                        
                                                    </Columns>
                                                </asp:GridView>
                                                    </div>
                                        </div>
                                      
                                    </div>



                       <div class="row form-group no-print">
                            <div class="col-md-5" style="padding-left: 0px;">
                                <div class="col-md-12" style="padding-left: 0px;">
                                    <label for="lbl" class="col-sm-7">Contact Officer Name</label><label id="lblCMes1" style="color:red;font-size:12px;display:none;"></label>
                                </div>
                                <div class="col-md-12">
                                    <asp:DropDownList ID="ddlGender"   runat="server" CssClass="form-control" style="width:21%;float:left">
                                    </asp:DropDownList>
                                    <asp:DropDownList CssClass="form-control" runat="server" ID="ddlContactOfficer" AutoPostBack="true" Visible="true" style="width:78%;float:right" OnSelectedIndexChanged="ddlContactOfficer_SelectedIndexChanged" >
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtContactOfficer"  style="width:78%;float:right" runat="server"  Visible="false" CssClass="form-control"></asp:TextBox>
                                    
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="col-md-12" style="padding-left: 0px;">
                                    <label for="lbl" class="col-sm-12" >Contact No</label>  
                                </div>
                                <div class="col-md-6">             
                                    <asp:TextBox ID="txtContactNo" type="number" onkeypress="return isNumberKey(event)" min="0"  ValidationGroup="btnContactInfo" runat="server"  CssClass="form-control"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtContactNo" ValidationGroup="btnContactInfo"
                                ErrorMessage="Maximum 10 Digits" ForeColor="Red" ValidationExpression="[0-9]{10}" Display="Dynamic"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ForeColor="Red" ControlToValidate="txtContactNo" ValidationGroup="btnContactInfo">Fill this Field</asp:RequiredFieldValidator>     
                                </div>
                                <div class="col-md-6">
                                        <div class="col-sm-5">
                                                <asp:Button runat="server" ID="btnContactInfo"   CssClass="btn btn-success" autopostback="true"  
                                                Text="Add" onclick="btnAddContactInfo_Click" OnClientClick="return validateContactUser()" ValidationGroup="btnContactInfo"  />
                                        </div>
                                        <div class="col-sm-4">
                                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-info"  OnClick="btnAddRow_Click" Text="Create New Contact" />
                                        </div>
                                 </div>
                              </div>
                            <div class="col-md-2"></div>
                         </div>




                         <div runat="server" id="divContactInfo"  class="row" visible="false" >
                                     <div class="col-md-6">
                                      <div class="table-responsive">
                                          <asp:GridView ID="gvContactOfficer" runat="server" CssClass="table table-responsive"  caption="Contact Officer Details"
                                                   GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                          
                                                    <asp:BoundField DataField="UserId" HeaderText="UserId" 
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="Title" HeaderText="Title"/>
                                                    <asp:BoundField DataField="ContactOfficer" HeaderText="Contact Name"/>
                                                   <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                                     <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                                                <ItemTemplate>
                                                                    <asp:Button CssClass="btn btn-danger btn-xs" ID="Delete"
                                                                        runat="server" OnClick="btnDeleteContactInfo_Click"
                                                                        Text="Delete" />
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
                               <div class="col-sm-12 no-print">
                                    <div class="box-header with-border ">
                                          <a class="arrowdown"  data-target="#Attachment" data-toggle="collapse" style="cursor: pointer;"  aria-expanded="false">
                                             <h4> <span class="label label-info" >View & Remove any Attachments</span> <span class="expand_caret caret" ></span></h4>
                                          </a>
                                    </div>
                                   <div class="box-body collapse" id="Attachment">
                                    <div class="col-sm-12">                                        
                                        <div class="col-sm-8">
                                        <h4><label class="label label-info">Replacement Images</label></h4>
                                       <asp:GridView ID="gvReplacementImageAttachment" runat="server" CssClass="table table-responsive-sm table-condensed table-bordered" HeaderStyle-BackColor="#67778e" HeaderStyle-ForeColor="White" EmptyDataText="No records Found" 
                                            GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" >
                                           <Columns>
                                    <asp:BoundField DataField="PrdId" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="FileName" HeaderText="Image Name" HeaderStyle-CssClass="text-center" />
                                    <asp:TemplateField HeaderText="Image" HeaderStyle-CssClass="text-center"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                Height="80px" Width="100px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                           <input type="button" class="btn btn-danger btn-xs" 
                                            onclick="btnAttachmentDelete_Click(this)" value="Delete" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                               <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:CheckBox ID="chkreplacementImg" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                       </asp:GridView>
                                            </div>
                                        <div class="col-sm-8">
                                        <h4><label class="label label-info">Standard Images</label></h4>
                                        <asp:GridView ID="gvStandardImageAttachment" runat="server" CssClass="table table-responsive-sm table-condensed table-bordered"  HeaderStyle-BackColor="#67778e" HeaderStyle-ForeColor="White" EmptyDataText="No records Found" 
                                            GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" >
                                            <Columns>
                                                        <asp:BoundField DataField="PrdId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                     <asp:BoundField DataField="FileName" HeaderText="Image Name" HeaderStyle-CssClass="text-center"/>
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" >
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                 <%--<asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                         <input type="button" class="btn btn-danger btn-xs" 
                                                        onclick="btnAttachmentDelete_Click(this)" value="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:CheckBox ID="chkStandardImg" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                                    </Columns>
                                       </asp:GridView>
                                            </div>
                                        <div class="col-sm-8">
                                        <h4><label class="label label-info">Supportive Documents</label></h4>
                                        <asp:GridView ID="gvSupportiveDocumentAttachment" runat="server" CssClass="table table-responsive-sm table-condensed table-bordered" HeaderStyle-BackColor="#67778e" HeaderStyle-ForeColor="White" EmptyDataText="No records Found" 
                                             GridLines="None" AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" >
                                            <Columns>
                                                <asp:BoundField DataField="PrdId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileName" HeaderText="File Name" HeaderStyle-CssClass="text-center" />                                                    
                                                <asp:TemplateField HeaderText="Preview">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" href='<%#Eval("FilePath")%>'>View</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                  <%--<asp:TemplateField HeaderText="Action"  HeaderStyle-CssClass="text-center"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <input type="button" class="btn btn-danger btn-xs" 
                                                        onclick="btnAttachmentDelete_Click(this)" value="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-center"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                          <asp:CheckBox ID="chkSupportingDocs" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                            </Columns>
                                       </asp:GridView>
                                            </div>
                                     </div>
                                    </div>
                                </div> 
                              
                        </div>
                         
                          <div class="box-footer no-print">
                        <div class="form-group">
                               <asp:Button ID="btnSendEmailToSupplier" runat="server" Text="Send Email"   
                                  CssClass="btn btn-primary pull-right margin" onclick="SendEmail_Click" OnClientClick ="disable();"/> 
                            
                             <asp:Button ID="btnPrint" runat="server" Text="Print"   
                                  CssClass="btn btn-success pull-right margin"  OnClientClick="printPage()"/>              

                          </div>
                        </div>
                </div>


                </section>
                <asp:Button ID="btnEmail" runat="server" OnClick="SendEmail_Click" CssClass="hidden" />
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnAdd" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

    <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script src="AdminResources/js/autoCompleter.js"></script>

    <script type="text/javascript">
        Sys.Application.add_load(function () {

            //onload set date value
            var this1 = $("#ContentSection_txtstart");
            if (this1.val() != undefined && this1.val() != "") {
                this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
            }
            this1 = $("#ContentSection_txtend");
            if (this1.val() != undefined && this1.val() != "") {
                this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
            }
            this1 = $("#ContentSection_txtStartDTime");
            if (this1.val() != undefined && this1.val() != "") {
                this1.attr('data-time', moment(this1.val(), 'HH:mm').format(this1.attr('data-time-format')));
            }
            this1 = $("#ContentSection_txtactualDate");
            if (this1.val() != undefined && this1.val() != "") {
                this1.attr('data-date', moment(this1.val(), 'YYYY-MM-DD').format(this1.attr('data-date-format')));
            }
            this1 = $("#ContentSection_txtactualTime");
            if (this1.val() != undefined && this1.val() != "") {
                this1.attr('data-time', moment(this1.val(), 'HH:mm').format(this1.attr('data-time-format')));
            }

            $('.clmdlBiddingPlanDocs').on({
                click: function () {
                    event.preventDefault();
                    $('#mdlBiddingPlanDocs').modal('hide');
                    //$('#mdlQuotations').modal('show');
                    $('#mdlBiddingplan').modal('show');
                    $('div').removeClass('modal-backdrop');
                }

            });
        });

        document.getElementById("clear").addEventListener("click", function () {
            document.getElementById("ContentSection_fileUpload1").value = "";

        }, false);

        function editscroll() {
            document.getElementById('ContentSection_dvUpdateplan').scrollIntoView();
        }

        function actualplanscroll() {
            document.getElementById('ContentSection_dvcomplete').scrollIntoView();
        }

        function SendEmail(obj) {
            var $confirm = $("#modalSendEmail");
            $confirm.modal('show');
            event.preventDefault();
            return this.false;
        }

        function hideSendEmailModal() {
            var $confirm = $("#modalSendEmail");
            $confirm.modal('hide');
            return this.false;
        }

        function validateContactUser(event) {
            //debugger;
            if ($("#ContentSection_ddlContactOfficer")[0] != undefined) {
            } else {
                if ($("#ContentSection_txtContactOfficer").val() == "") {
                    $("#lblCMes1").text("* Fill this field");
                    $("#lblCMes1").css("display", "block");
                    return false;
                    event.preventDefault();
                }
            }
            if ($("#ContentSection_RegularExpressionValidator3").css("display") != "none") {
                return false;
                event.preventDefault();
            }
            return true;
        }

        function SearchSupplier() {

            $("#ContentSection_btnSupplierSearch").click()

        }

       
        function ViewPr(obj) {
            var PrId = <%=ViewState["PrId"].ToString()%>;
            var href = "ViewPRNew.aspx?PrId=" + PrId + ""
            window.open(href, '_blank');
        }

        function ViewMrn(obj) {
            var MrnId = <%=ViewState["MrnId"] != null ? ViewState["MrnId"].ToString() : "0"%>;
            var href = "ViewMRNNew.aspx?MrnId=" + MrnId + ""
            window.open(href, '_blank');
        }


        function btnAttachmentDelete_Click(obj) {
            $(obj).closest("tr").remove();
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function dateChange(obj) {
            if (obj.value) {
                $(obj).attr('data-date', moment(obj.value, 'YYYY-MM-DD').format($(obj).attr('data-date-format')));
            } else {
                $(obj).attr('data-date', '');
            }
        }

        function disable() {
           
            
            

            document.getElementById('<%= btnSendEmailToSupplier.ClientID %>').disabled = true;
                $('#ContentSection_btnEmail').click();
           
           
        }

       
        function printPage() {
            window.print();
        }

        
   

    </script>



</asp:Content>

