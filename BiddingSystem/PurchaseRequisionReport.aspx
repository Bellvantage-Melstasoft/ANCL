 <%@ Page Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="PurchaseRequisionReport.aspx.cs" Inherits="BiddingSystem.PurchaseRequisionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <section class="content-header">
        <h1>Purchase Requisition Report</h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Purchase Requisition Report </li>
      </ol>
    </section>
    <br />
    <section class="content" id="divPrintPo">
    <div class="container-fluid">
        <form runat="server" id="frm1">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" >
            <ContentTemplate>
            
            <div class="box box-info">
                   <%-- <div class="box-header">
                        <h3 class="text-center"><i class="fa fa-file-invoice"></i>&nbsp;&nbsp;&nbsp;Purchase Requisition Report</h3>
                        <hr>
                    </div>--%>
                    <div class="box-body">
                        <div class="row">
                            <h3 style="text-align:center"><b> MRN Details</b> </h3>
                            </div>
                        <div class="row">
                            <div class="col-xs-4">

                                <strong>MRN CODE: </strong>
                                <asp:Label runat="server" ID="lblMrnCode"></asp:Label><br>
                                 <strong>EXPENSE TYPE: </strong>
                                <asp:Label runat="server" ID="lblExpenseType"></asp:Label><br>
                                <strong>EXPECTED DATE: </strong>
                                <asp:Label runat="server" ID="lblExpectedDate"></asp:Label><br>

                                
                            </div>
                            
                            <div class="col-xs-4">
                                
                                <strong>CATEGORY: </strong>
                                <asp:Label runat="server" ID="lblCategory"></asp:Label><br>
                                 <strong>SUB-CATEGORY: </strong>
                                <asp:Label runat="server" ID="lblSubCategory"></asp:Label><br>
                                <strong>FROM DEPARTMENT: </strong>
                                <asp:Label runat="server" ID="lblDepartmentName"></asp:Label><br>
                                <strong>TO WAREHOUSE: </strong>
                                <asp:Label runat="server" ID="lblWarehouseName"></asp:Label><br>
                                    
                             </div>
                             
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvMRNItems" AutoGenerateColumns="false"
                                        CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                        <Columns>
                                            <asp:BoundField DataField="MrndID" HeaderText="MrndID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />  
                                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                            <asp:BoundField DataField="RequestedQty" HeaderText="Requested QTY" />
                                            <asp:BoundField DataField="MeasurementShortName" HeaderText="Unit" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            <asp:BoundField DataField="ReceivedQty"
                                                HeaderText="Received Quantity" />
                                            <asp:BoundField DataField="IssuedQty"
                                                HeaderText="Issued Quantity" />
                                            
                                                
                                        
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <asp:Panel ID="pnlPR" runat="server" Visible="false">
                        <div class="row">
                            <h3 style="text-align:center"><b> PR Details</b> </h3>
                            </div>
                        <div class="row">
                          <div class="col-xs-4">

                               <strong>PR CODE: </strong>
                                <asp:Label runat="server" ID="lblPrCode"></asp:Label><br>
                                 <strong>EXPENSE TYPE: </strong>
                                <asp:Label runat="server" ID="lblPrExpenseType"></asp:Label><br>
                               <strong>EXPECTED DATE: </strong>
                                <asp:Label runat="server" ID="lblPrExpDate"></asp:Label><br>

                            </div>
                            
                            <div class="col-xs-4">
                               
                                <strong>CATEGORY: </strong>
                                <asp:Label runat="server" ID="lblPRCat"></asp:Label><br>
                                 <strong>SUB-CATEGORY: </strong>
                                <asp:Label runat="server" ID="lblPrSubCat"></asp:Label><br>
                                <strong>FROM DEPARTMENT: </strong>
                                <asp:Label runat="server" ID="lblPrDept"></asp:Label><br>
                                <strong>TO WAREHOUSE: </strong>
                                <asp:Label runat="server" ID="lblPrWarehouse"></asp:Label><br>
                                   
                             </div>
                             
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="table-responsive">
                                    <asp:GridView runat="server" ID="gvPRItems" AutoGenerateColumns="false"
                                        CssClass="table table-responsive" HeaderStyle-BackColor="LightGray" BorderColor="LightGray" EnableViewState="true">
                                        <Columns>
                                            <asp:BoundField DataField="PrdId" HeaderText="PrdId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />  
                                            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden ItemId" />
                                            
                                            
                                             <asp:TemplateField HeaderText="PR CodeItem Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# "PR"+Eval("PrCode").ToString()%>'/>
                                                    </ItemTemplate>
                                            </asp:TemplateField>   
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" CssClass="ItemName" Text='<%# Eval("ItemName").ToString()%>'/>
                                                    <label class="label label-success pull-right lbl-refresh lkPurchaseHistory" style="display:none" onclick="loadPurchaseHistory(this);">Purchase History</label>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                            <asp:BoundField DataField="RequestedQty" HeaderText="Requested QTY" />
                                            <asp:BoundField DataField="MeasurementShortName" HeaderText="Unit" NullDisplayText="Not Found" />
                                            <asp:BoundField DataField="Description" HeaderText="Description" />
                                            
                                         
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <hr />
</asp:Panel>

                    
                </div>
                
                <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvPurchaseRequest"
                                            HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                                            OnRowDataBound="gvPurchaseRequest_RowDataBound" DataKeyNames="PrId"
                                            GridLines="None" CssClass="table table-responsive"
                                            AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                            <PagerStyle HorizontalAlign="Center" CssClass="GridPager" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img alt="" style="cursor: pointer;margin-top: -6px;"
                                                            src="images/plus.png" />
                                                        <asp:Panel ID="pnlPrDetails" runat="server"
                                                            Style="display: none">
                                                        <asp:GridView ID="gvPrDetails" runat="server"
                                                                CssClass="table table-responsive ChildGrid"
                                                                GridLines="None" AutoGenerateColumns="false"  OnRowDataBound="gvPrDetails_RowDataBound"
                                                                DataKeyNames="PrdId" Caption="Items in Purchase Request"
                                                                EmptyDataText="No Item Found">
                                                                <Columns>


                                                                    <asp:BoundField DataField="PrdId" HeaderText="PRDId"
                                                                        HeaderStyle-CssClass="hidden"
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
                                                                   
                                                                    <asp:BoundField DataField="EstimatedAmount"
                                                                        HeaderText="Estimated Price" />
                                                                     
                                                                    <asp:TemplateField HeaderText="Requested Quantity">
							                                        <ItemTemplate>
								                                        <asp:Label runat="server" ID="lblPrCode" Text='<%# Eval("RequestedQty")%>'></asp:Label>
                                                                        <asp:Label runat="server" Text='<%# Eval("ShortCode")%>'></asp:Label>
							                                        </ItemTemplate>
						                                            </asp:TemplateField>
                                                                    
                                                                </Columns>
                                                            </asp:GridView>
                                                        </asp:Panel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PrId" HeaderText="PrId"
                                                    HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                
                                                <asp:TemplateField HeaderText="PR Code">
							                    <ItemTemplate>
								                    <asp:Label runat="server" ID="lblPrCode" Text='<%# "PR"+Eval("PrCode")%>'></asp:Label>
							                    </ItemTemplate>
						                        </asp:TemplateField>
                                              


                                                <asp:BoundField DataField="ExpectedDate" HeaderText="Date Of Request" />
                                                <asp:BoundField DataField="RequiredFor" HeaderText="Required For" />
                                                <asp:BoundField DataField="CreatedDateTime" HeaderText="PR Created Date"/>
                                                <asp:BoundField DataField="CreatedByName" HeaderText="Created By" />
                                               

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>







            </div>  
               
            </ContentTemplate>
        </asp:UpdatePanel>
        </form>
    </div>
    </section>
   
   
</asp:Content>
