<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="ViewMRN.aspx.cs" Inherits="BiddingSystem.ViewMRN" %>
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

  <style type="text/css">
    .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%;
            text-align:center;
        }
        .ChildGrid th
        {
            color: White;
            font-size: 10pt;
            line-height:200%;
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #67778e !important;
            color: white;
        }
  </style>
  <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
<section class="content-header">
    <h1>
      View Material Request Notes 
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">Material Request Notes </li>
      </ol>
    </section>
    <br />

    <form runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Updatepanel1" runat="server">
        <ContentTemplate>
            <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelMRN" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Pending Material Requests</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvMRN" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="MrnID" OnRowDataBound="gvMRN_RowDataBound" EmptyDataText="No records Found">
                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                    <asp:Panel ID="pnlMRND" runat="server" Style="display: none">
                        <asp:GridView ID="gvMRND" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="RequestedQty"  HeaderText="Requested Qty"  /> 
                                <asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  /> 
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </ItemTemplate>
                    </asp:TemplateField>

                        <asp:BoundField DataField="MrnID"  HeaderText="MRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created On"  DataFormatString="{0:dd-MM-yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date"  DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Description"  HeaderText="Description" />
                        
                          <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="lbtnView" Text="Edit MRN" OnClick="btnView_Click"></asp:LinkButton>
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
      <!-- /.box -->
    </section>

            
            <section class="content">
      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info" id="panelApprovRejectMRN" runat="server">
        <div class="box-header with-border">
          <h3 class="box-title" >View Approved/Rejected Material Requests</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          <div class="row">
            <div class="col-md-12">
            <div class="table-responsive">
              <asp:GridView runat="server" ID="gvApprovRejectMRN" GridLines="None" CssClass="table table-responsive" AutoGenerateColumns="false" DataKeyNames="MrnID" OnRowDataBound="gvApprovRejectMRN_RowDataBound" EmptyDataText="No records Found">
                    <Columns>
                    <asp:TemplateField>
                    <ItemTemplate>
                    <img alt = "" style="cursor: pointer;margin-top: -6px;" src="images/plus.png" />
                    <asp:Panel ID="pnlApprovRejectMRND" runat="server" Style="display: none">
                        <asp:GridView ID="gvApprovRejectMRND" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid">
                            <Columns>
                                <asp:BoundField ItemStyle-Width="150px" DataField="CategoryName" HeaderText="Category"/>
                                <asp:BoundField ItemStyle-Width="150px" DataField="SubCategoryName"  HeaderText="Sub-Category"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ItemName"  HeaderText="Item"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="RequestedQty"  HeaderText="Requested Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="IssuedQty"  HeaderText="Issued Qty"  />
                                <asp:BoundField ItemStyle-Width="150px" DataField="ReceivedQty"  HeaderText="Received Qty"  />  
                                <asp:BoundField ItemStyle-Width="150px" DataField="Description"  HeaderText="Description"  />  
                                <asp:TemplateField HeaderText="Status" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" ItemStyle-HorizontalAlign="Right" ItemStyle-Width="150px">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNDStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending" :Eval("Status").ToString()=="1"?"Added to PR": Eval("Status").ToString()=="2"?"Partially-Issued": Eval("Status").ToString()=="3"?"Fully-Issued": Eval("Status").ToString()=="4"?"Delivered":"Received" %>'></asp:Label>
                                 </ItemTemplate>
                                </asp:TemplateField> 
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </ItemTemplate>
                    </asp:TemplateField>

                        <asp:BoundField DataField="MrnID"  HeaderText="MRN ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                        <asp:BoundField DataField="CreatedByName"  HeaderText="Created By" />
                        <asp:BoundField DataField="CreatedDate"  HeaderText="Created On" DataFormatString="{0:dd-MM-yyyy hh:mm tt}"/>
                        <asp:BoundField DataField="ExpectedDate"  HeaderText="Expected Date" DataFormatString="{0:dd-MM-yyyy}"/>
                        <asp:BoundField DataField="Description"  HeaderText="Description" />
                        <asp:TemplateField HeaderText="Status">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNStatus" Text='<%#Eval("Status").ToString()=="0"? "Pending":"Completed" %>'></asp:Label>
                                 </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="Approved/Rejected">
                                 <ItemTemplate>
                                     <asp:Label runat="server" ID="txtMRNIsApproved" Text='<%#Eval("IsApproved").ToString()=="1"? "Approved":"Rejected"%>' ForeColor='<%#Eval("IsApproved").ToString()=="1"? System.Drawing.Color.Green:System.Drawing.Color.Red%>'></asp:Label>
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
      <!-- /.box -->
    </section>

        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
   <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    
</asp:Content>

