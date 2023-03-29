<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewRecommendedforPO.aspx.cs" Inherits="BiddingSystem.ViewRecommendedforPO"  EnableEventValidation="false" %>
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
      <link href="AdminResources/css/jquery-ui.css" rel="stylesheet" />
 <section class="content-header">
      <h1>
       Tech Committe Recomendedation File Upload
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Tech Commitee Recomendedation File Upload </li>
      </ol>
    </section> 
    <br />
        <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <section class="content">
      <!-- SELECT2 EXAMPLE -->

             <div id="mdladddocs" class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header" style="background-color: #3c8dbc;">
                            <button type="button"
                                class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                            <h4 class="modal-title" style="color: white; font-weight: bold;">Upload Files</h4>
                        </div>
                        <div class="modal-body">
                            
                     <div class="row">
                                           
                                <div class="col-sm-5">
                                     <div class="form-group">
                                    <label for="exampleInputEmail1">Upload Documents</label>
                                     <asp:FileUpload runat="server" style="display:inline;" AllowMultiple="true"    CssClass="form-control" ID="fileUpload1" ></asp:FileUpload>
                                      
                                </div>

                              </div>
                                        <div class="col-sm-2" style="padding-top:25px"> <button class="btn btn-info btn-flat clear"  id="clearDocs" >Clear</button></div>
                                     
                                     
                             

                              </div>
                        </div>
                       
                        <div class="modal-footer">
                            <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary pull-right" OnClick="btnUpload_Click" Style="margin-right: 10px"  />
                        </div>
                           </div> 
                    </div>
                </div>
                <div id="mdlviewdocs" class="modal modal-primary fade" tabindex="-1" style="z-index:3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvbddinplanfiles" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" OnRowDataBound="gvbddinplanfiles_RowDataBound" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="filename" HeaderText="File Name" />
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="sequenceId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Download">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnDownload"  OnClick="lbtnDownload_Click">View</asp:LinkButton>
                                                                <iframe id="downloadFrame" style="display:none"></iframe>
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

            
      <div class="box box-info" id="panelPurchaseRequset" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >Tech Commitee Recomendedation File Upload(For Purchase Order Creation)</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
       
        <!-- /.box-header -->
              <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="gvPurchaseRequest" GridLines="None"  OnRowDataBound="gvPurchaseRequest_RowDataBound"  DataKeyNames="PrId" CssClass="table table-responsive" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White"
                    AutoGenerateColumns="false" EmptyDataText="No PR Found">
                    <Columns>
                        <asp:TemplateField>
                    <ItemTemplate>
                            <img alt="" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                          <asp:Panel ID="pnlqutationdetails" runat="server" Style="display: none">
                                 <asp:GridView ID="gvQuotations" OnRowDataBound="gvQuotations_RowDataBound" runat="server" CssClass="table table-responsive"
                                                    GridLines="None" AutoGenerateColumns="false" 
                                                   Caption="Quotations"
                                                    HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                      <Columns>
                                        <asp:BoundField DataField="QuotationId" HeaderText="QuotationId"
                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="BidId" HeaderText="BidId"
                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="SupplierId" HeaderText="SupplierId"
                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name"
                                        ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="SubTotal" HeaderText="Sub Total"
                                        ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="NbtAmount" HeaderText="NBT Amount"
                                        ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="VatAmount" HeaderText="VAT Amount"
                                        ItemStyle-Font-Bold="true" />
                                    <asp:BoundField DataField="NetTotal" HeaderText="Net Total"
                                        ItemStyle-Font-Bold="true" />
                                          <asp:BoundField DataField="IsUploadeded" HeaderText="IsUploadeded"
                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                     <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtnupload" Visible="false" Text="upload Documents" CssClass="btn btn-sm btn-primary" OnClick="lbtnupload_Click"></asp:LinkButton>

                                                <asp:LinkButton runat="server" ID="lbtnView" Visible="false" Text="View Documents" CssClass="btn btn-sm btn-success" OnClick="btnView_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          </Columns>
                                     </asp:GridView>
                              </asp:Panel>
                        </ItemTemplate>
                            </asp:TemplateField>
                        <asp:BoundField DataField="PrId"  HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrCode"  HeaderText="PR Code" />
                        <asp:BoundField DataField="DepartmentId"  HeaderText="DepartmentId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="DateOfRequest"  HeaderText="Date Of Request"/>
                        <asp:BoundField DataField="QuotationFor"  HeaderText="Quotation For" />
                        <asp:BoundField DataField="OurReference"  HeaderText="OurReference" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="RequestedBy"  HeaderText="RequestedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="CreatedDateTime"  HeaderText="PR Created Date"  />
                        <asp:BoundField DataField="CreatedBy"  HeaderText="CreatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="UpdatedDateTime"  HeaderText="UpdatedDateTime" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="UpdatedBy"  HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="IsActive"  HeaderText="IsActive" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApproved"  HeaderText="PrIsApproved" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApprovedOrRejectedBy"  HeaderText="PrIsApprovedOrRejectedBy" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="PrIsApprovedOeRejectDate"  HeaderText="PrIsApprovedOeRejectDate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                      
                    </Columns>
                </asp:GridView>
                </div>
            </div>         
          </div>
         
        </div>
        <!-- /.box-body -->
      </div>
          </div>
      <!-- /.box -->
    </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload"/>
        </Triggers>
    </asp:UpdatePanel>
    </form>
     <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
     <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        document.getElementById("clear").addEventListener("click", function () {
            document.getElementById("ContentSection_fileUpload1").value = "";

        }, false);
    </script>

</asp:Content>
