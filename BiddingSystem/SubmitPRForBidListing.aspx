<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="SubmitPRForBidListing.aspx.cs" Inherits="BiddingSystem.SubmitPRForBidListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

    <script src="AdminResources/js/jquery1.8.min.js"></script>
    <link href="AdminResources/css/select2.min.css" rel="stylesheet" />
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

         .paddingRight {
            margin-right: 5px;
        }
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/moment.min.js"></script>
    <link href="AppResources/css/jquery-ui.css" rel="stylesheet" type="text/css" />
    <link href="AdminResources/css/htmldatecss.css" rel="stylesheet" />
    <section class="content-header">
        <h1>
            Create Bid And Procurement Plan
            <small></small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
            <li class="active">Create Bid And Procurement Plan</li>
        </ol>
    </section>
    <br />


    <form runat="server">
        <asp:ScriptManager runat="server" ID="SM1"></asp:ScriptManager>
        <asp:UpdatePanel ID="Updatepanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="True">
            <ContentTemplate>

                 <div class="modal fade" id="mdlItemSpecs" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">  
                            <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close clmdlItemSpecs" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">ITEM SPECIFICATION</h4>
                                            </div>
                                        </div>
                                        <hr />                         
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                         <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive"
                                             GridLines="None" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White"
                                            AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Specifications Found">
                                            <Columns>
                                                <asp:BoundField DataField="PrdId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="BomId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="Material" HeaderText="Material" />
                                                <asp:BoundField DataField="Description" HeaderText="Description" />
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
    </div>

                  <div class="modal fade" id="mdlCapexDocs" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">CAPEX DOCUMENTS</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                         <asp:GridView ID="gvCapexDocs" runat="server" CssClass="table table-responsive"
                                            GridLines="None"  HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White"
                                            AutoGenerateColumns="false" EmptyDataText="No Documents Found" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:BoundField DataField="PrId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="Preview">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" href='<%#Eval("FilePath")%>'>View</asp:LinkButton>
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
    </div>
                
                  <div class="modal fade" id="mdlPurchaseHistory" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">PURCHASE HISTORY</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                         <asp:GridView ID="gvPurchaseHistory" runat="server" CssClass="table table-responsive"
                                            GridLines="None"  HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White"
                                            AutoGenerateColumns="false" EmptyDataText="No items Found" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:BoundField DataField="PoCode" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                                <asp:BoundField DataField="ShortCode" HeaderText="Unit" />
                                                <asp:BoundField DataField="ItemPrice" HeaderText="Item Price" />
                                               
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
    </div>
              

           <%--     <div id="mdlItemSpecs" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index: 3001" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->

                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Item Specifications</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;" GridLines="None"
                                                    AutoGenerateColumns="false" EmptyDataText="No Specifications Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="Material" HeaderText="Material" />
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
                </div>--%>

             <%--   <div id="mdlStandardImages" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Item Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive TestTable"
                                                    EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
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
                </div>--%>

               
                  <div class="modal fade" id="mdlFileUpload" role="dialog">
                    <div class="modal-dialog" style="width: 63%;">
                        <div class="modal-content">
                            <div class="modal-body">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <button type="button" class="close clmdlFileUpload" data-dismiss="modal">&times;</button>
                                                <h4 class="text-green text-bold">STANDARD IMAGES</h4>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row">
                                                <div class="col-md-12">
                                                <div class="table-responsive">
                                           <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive" GridLines="None" AutoGenerateColumns="false"
                                            EmptyDataText="No Images Found" HeaderStyle-BackColor="#275591" ShowHeader="true" ShowHeaderWhenEmpty="true" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                        <asp:BoundField DataField="PrdId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                     <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
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
                </div>

               <%-- <div id="mdlReplacementImages" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Replacement Photos</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvViewReplacementImages" runat="server" CssClass="table table-responsive TestTable"
                                                    EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                        <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                                    Height="80px" Width="100px" />
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
                </div>--%>

                <div class="modal fade" id="mdlReplacementImages" role="dialog">
        <div class="modal-dialog" style="width: 63%;">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close clmdlReplacementImages" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">REPLACEMENT IMAGES</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                    <div class="col-md-12">
                                    <div class="table-responsive">
                                 <asp:GridView ID="gvViewReplacementImages" runat="server" CssClass="table table-responsive"
                                EmptyDataText="No Images Found" GridLines="None" 
                                AutoGenerateColumns="false" ShowHeader="true" ShowHeaderWhenEmpty="true" HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White">
                                <Columns>
                                    <asp:BoundField DataField="PrdId" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden"
                                        ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                    <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>'
                                                Height="80px" Width="100px" />
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
    </div>


                <%--<div id="mdlSupportiveDocs" class="modal modal-primary fade" tabindex="-1" style="z-index: 3001" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <!-- Modal content-->
                        <div class="modal-content" style="background-color: #a2bdcc;">
                            <div class="modal-header" style="background-color: #7bd47dfa;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">×</span></button>
                                <h4 class="modal-title">Uploaded Supportive Documents</h4>
                            </div>
                            <div class="modal-body">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvSupportiveDocuments" runat="server" CssClass="table table-responsive TestTable"
                                                    Style="border-collapse: collapse; color: black;"
                                                    AutoGenerateColumns="false" EmptyDataText="No Documents Found">
                                                    <Columns>
                                                        <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                        <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Preview">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lbtnViewUploadSupporiveDocument">View</asp:LinkButton>
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
                </div>--%>
                 <div class="modal fade" id="mdlSupportiveDocs" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <button type="button" class="close clmdlSupportiveDocs" data-dismiss="modal">&times;</button>
                                    <h4 class="text-green text-bold">SUPPORTIVE DOCUMENTS</h4>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                         <asp:GridView ID="gvSupportiveDocuments" runat="server" CssClass="table table-responsive"
                                            GridLines="None"  HeaderStyle-BackColor="#275591"  HeaderStyle-ForeColor="White"
                                            AutoGenerateColumns="false" EmptyDataText="No Documents Found" ShowHeader="true" ShowHeaderWhenEmpty="true">
                                            <Columns>
                                                <asp:BoundField DataField="PrdId" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileName" HeaderText="File Name" />
                                                <asp:BoundField DataField="FilePath" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="Preview">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" href='<%#Eval("FilePath")%>'>View</asp:LinkButton>
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
    </div>

                <div id="mdlBidMoreDetails" class="modal modal-primary fade" tabindex="-1" role="dialog" style="z-index: 3000;" aria-hidden="true">
                    <div class="modal-dialog" style="width: 80%">
                        <!-- Modal content-->

                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; color: white;">
                                    <span aria-hidden="true" style="opacity: 1;">×</span></button>
                                <h4 class="modal-title">Bid Details</h4>
                            </div>
                            <div class="modal-body" style="background-color: white;">
                                <div class="login-w3ls">
                                    <div class="row">
                                        <div class="col-md-12">

                                            <div class="table-responsive">

                                                <asp:GridView ID="gvBidMoreDetails" runat="server" CssClass="table table-responsive ChildGrid"
                                                    GridLines="None" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                            HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                                        <asp:BoundField DataField="RequestedQty" HeaderStyle-CssClass="hidden"
                                                            ItemStyle-CssClass="hidden" />

                                                        <asp:TemplateField HeaderText="Quantity">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQty"
                                                                     runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()) + Eval("MeasurementShortName").ToString() %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Replacement">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        

                                                        <asp:TemplateField HeaderText="Replacement Images" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnViewzReplacementPhotosOfBidItem" runat="server" OnClick="btnViewzReplacementPhotosOfBidItem_Click"
                                                                    Text="View" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Standard Images">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnViewUploadPhotosOfBidItem" runat="server" OnClick="btnViewUploadPhotosOfBidItem_Click"
                                                                    Text="View" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Specifications">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="lblViewBomOfBidItem" Text="View" OnClick="lblViewBomOfBidItem_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Supportive Documents">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnViewSupportiveDocumentsOfBidItem" runat="server" OnClick="btnViewSupportiveDocumentsOfBidItem_Click"
                                                                    Text="View" />
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

                <section class="content" style="padding-top: 0px">
                        <div class="box box-info" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Purchase Request Note</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>PR No : </strong>
                                        <%--<asp:LinkButton runat="server" ID="lblPRNo" Text="" OnClick="btnPR_Click" OnClientClick="target ='_blank';" ></asp:LinkButton><br />--%>
                                        <a Title="Click To Go To PR" data-toggle="tooltip" data-placement="bottom" onclick="ViewPr(this)" class="text-navy" style="cursor:pointer">
                                         <asp:Label ID="lblPRNo" runat="server" Text="" CssClass="label label-info"></asp:Label> </a><br />
                                        <strong>Created On : </strong>
                                        <asp:Label ID="lblCreatedOn" runat="server" Text=""></asp:Label><br />
                                        <strong>Created By : </strong>
                                        <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label><br />
                                        <strong>Category : </strong>
                                        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label><br />
                                        <strong>Sub category : </strong>
                                        <asp:Label ID="lblSubcategory" runat="server" Text=""></asp:Label><br />
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
                                        <strong>Approved On : </strong>
                                        <asp:Label ID="lblApprovedOn" runat="server" Text=""></asp:Label><br />
                                        <strong>Approved By : </strong>
                                        <asp:Label ID="lblApprovedBy" runat="server" Text=""></asp:Label><br />
                                    </address>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <address>
                                        <strong>Warehouse : </strong>
                                        <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label><br /> 
                                        <strong>Department : </strong>
                                        <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label><br />  
                                        <strong>PR Expected Date : </strong>
                                        <asp:Label ID="lblPRRequestedDate" runat="server" Text=""></asp:Label><br /> 
                                        <strong>Purchase Type </strong>
                                        <asp:Label ID="lblPurchaseType" runat="server" Text=""></asp:Label><br />
                                        <div  runat="server" Visible="false" id="divMrnReferenceCode">
                                            <strong>MRN No: </strong>
                                            <a Title="Click To Go To MRN" data-toggle="tooltip" data-placement="bottom" onclick="ViewMrn()" class="text-navy" style="cursor:pointer;font-size: 16px;">
                                            <asp:Label runat="server" ID="lblMrnReferenceCode"></asp:Label></a><br />                                            
                                        </div> 
                                    </address>
                                </div>
                            </div>
                            <div class="box box-info" id="divPrDetails" runat="server">

                                <div class="box-header with-border">
                                    <h3 class="box-title" style="margin-left:5px">Purchase Request Items</h3>
                                </div>
                                
                                <div class="box-body">
                                <div class="panel-body">
                                    <div class="co-md-12">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvPRView" runat="server" CssClass="table table-responsive tablegv"
                                                GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="CheckBox2" runat="server" onclick="CheckBoxChecked(this);" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="PrdId" HeaderText="PRDID"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    <asp:BoundField DataField="Remarks" HeaderText="Purpose" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="RequestedQty" HeaderStyle-CssClass="hidden"
                                                        ItemStyle-CssClass="hidden" />

                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtItemQuantity" Width="100" type="number"
                                                                runat="server" step=".01" min='.01' Text='<%# decimal.Parse(Eval("RequestedQty").ToString()) %>'></asp:TextBox>
                                                            <asp:label ID="lblUnit"  type="text" runat="server" Text='<%#Eval("MeasurementShortName")%>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Replacement">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText=" File Sample Provided">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSampleProvided" runat="server" Text='<%# Eval("FileSampleProvided").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="WarehouseStock" HeaderText="Inventory Details" />--%>
                                                   <%-- <asp:TemplateField HeaderText="Inventory Details">
                                                        <ItemTemplate>
                                                             <asp:label ID="lblInventory"  type="text" runat="server" Text='<%# decimal.Parse(Eval("WarehouseStock").ToString()).ToString() + " " + Eval("MeasurementShortName").ToString() %>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                               <%--     <asp:TemplateField HeaderText="Inventory Details">
                                                        <ItemTemplate>
                                                             <asp:label ID="lblInventory"  type="text" runat="server" Text='<%# decimal.Parse(Eval("AvailableQty").ToString()).ToString() + " " + Eval("WarehouseUnit").ToString() %>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                   <asp:TemplateField HeaderText="Inventory Details">
                                                        <ItemTemplate>
                                                            <%-- <asp:label ID="lblInventory"  type="text" runat="server" Text='<%#Eval("AvailableQty")==null ? "0" : decimal.Parse(Eval("AvailableQty").ToString()).ToString() + " " + Eval("WarehouseUnit").ToString() %>'></asp:label>--%>
                                                             <asp:label ID="lblInventory"  type="text" runat="server" Text='<%#Eval("AvailableQty")==null ? "0" : decimal.Parse(Eval("AvailableQty").ToString()).ToString() + " "  %>'></asp:label>
                                                            <asp:Label ID="lblWarehouseUnit" type="text" runat="server" Text='<%# Eval("WarehouseUnit") == null ? " " : Eval("WarehouseUnit").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="More Info" HeaderStyle-CssClass="no-print" ItemStyle-CssClass="no-print">
                                            <ItemTemplate>
                                                 <asp:LinkButton runat="server" ID="lkRepalacementImages" ToolTip="Replacement Images" data-toggle="tooltip" data-placement="top" OnClick="btnViewzReplacementPhotos_Click" CssClass="text-orange "><span class="glyphicon glyphicon-picture"></span></asp:LinkButton>
                                              <asp:LinkButton runat="server" ID="lkStandardImages" ToolTip="Standard Images" data-toggle="tooltip" data-placement="top" OnClick="btnViewUploadPhotos_Click" CssClass="text-green"><span class="glyphicon glyphicon-picture"></span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lkSupportiveDocument_Click" ToolTip="Supportive Documents" data-toggle="tooltip" data-placement="top" OnClick="btnViewSupportiveDocuments_Click" CssClass="text-red"><span class="glyphicon glyphicon-file"></span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lkItemSpecification" ToolTip="Item Specifications" data-toggle="tooltip" data-placement="top" OnClick="lblViewBom_Click" CssClass="text-navy"><span class="glyphicon glyphicon-list"></span></asp:LinkButton>
                                                 </ItemTemplate>
                                        </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Purchase History" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPurchaseHistory" runat="server" OnClick="BtnPurchaseHistory_Click"
                                                                Text="View" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


<%--                                                    <asp:TemplateField HeaderText="Replacement Images" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewzReplacementPhotos" runat="server" OnClick="btnViewzReplacementPhotos_Click"
                                                                Text="View" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Standard Images" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewUploadPhotos" runat="server" OnClick="btnViewUploadPhotos_Click"
                                                                Text="View" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Specifications" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="lblViewBom" Text="View" OnClick="lblViewBom_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Supportive Documents" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewSupportiveDocuments" runat="server" OnClick="btnViewSupportiveDocuments_Click"
                                                                Text="View" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </div>

                            <div class="box box-info"  id="divBidBasicInfo" runat="server">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Bid Basic Info</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                
                                                <label for="ddlBidOpenType">Bid Type</label>
                                                
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1"
                                                    ForeColor="Red" Font-Bold="true" InitialValue="" ControlToValidate="ddlBidOpenType" ValidationGroup="btnSubmitBid">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlBidOpenType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Online" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Manual"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Both Online & Manual"></asp:ListItem>
                                                </asp:DropDownList>
                                            
                                                </div>
                                            <div class="form-group">
                                                <label for="ddlBidType">Submit Selected Items As</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2"
                                                    ForeColor="Red" Font-Bold="true" InitialValue="" ControlToValidate="ddlBidType" ValidationGroup="btnSubmitBid">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlBidType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Individual Bids" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="A Grouped Bid"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <label for="dtStartDate">Open Bid From(Date)</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3"
                                                    ForeColor="Red" Font-Bold="true" ControlToValidate="dtStartDate" ValidationGroup="btnSubmitBid">*</asp:RequiredFieldValidator>
                                                <asp:TextBox ID="dtStartDate" runat="server" type="date" CssClass="form-control customDate"  data-date=""  data-date-format="DD MMM YYYY"
                                                    autocomplete="off"   onchange="dateChange(this)" ></asp:TextBox>
                                            </div>
                                           

                                            </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label for="ddlOpenBidsTo">Open Bid To</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6"
                                                    ForeColor="Red" Font-Bold="true" InitialValue="" ControlToValidate="ddlOpenBidsTo" ValidationGroup="btnSubmitBid">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlOpenBidsTo" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="1" Text="Registered Suppliers" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Every Supplier"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>

                                            <div class="form-group">
                                                <label for="txtTermsAndConditions">Terms & Conditions</label>
                                                <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4"
                                                    ForeColor="Red" Font-Bold="true" InitialValue="" ControlToValidate="txtTermsAndConditions" ValidationGroup="btnSubmitBid">*</asp:RequiredFieldValidator>--%>
                                                <asp:TextBox TextMode="MultiLine" Rows="4" ID="txtTermsAndConditions"
                                                    runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                            <div class="form-group">
                                                <label for="ddlPurchasingOfficer">Assign Purchasing Officer</label><label id="lblQuotationFor" style="color:red;">(*)</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8"
                                                    ForeColor="Red" Font-Bold="true" InitialValue="" ControlToValidate="ddlPurchasingOfficer" ValidationGroup="btnSubmitBid">*</asp:RequiredFieldValidator>
                                                <asp:DropDownList ID="ddlPurchasingOfficer" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <hr />
                                       
                                 </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="box box-info"  id="div1" runat="server">
                                <div class="box-header with-border">
                                    <h3 class="box-title">Bid Info</h3>
                                </div>
                                <!-- /.box-header -->
                                <div class="box-body">
                                  <div class="row">
                                      <div class="col-md-6">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label for="ddlBiddingMethod">Bidding Method</label>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4"
                                                ForeColor="Red" Font-Bold="true" InitialValue="0" ControlToValidate="ddlBiddingMethod" ValidationGroup="btnSubmitBid">*</asp:RequiredFieldValidator>
                                            <asp:DropDownList ID="ddlBiddingMethod"  OnSelectedIndexChanged="ddlBiddingMethod_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                      </div>
                                  
                                 
                                      <div class="col-md-12">
                                        <div class="form-group">
                                          <label for="ddlProcumentPlans">Procument Plan</label>
                                          <asp:DropDownList ID="ddlProcumentPlan" Enabled="false"  AutoPostBack="true" runat="server" CssClass="form-control" onselectedindexchanged="ddlProcumentPlan_SelectedIndexChanged"></asp:DropDownList>
                                       </div>
                                      </div>
                                         </div>
                                       <div class="col-md-6">
                                      <div class="col-md-12">
                                       <div class="form-group">
                                                <label for="txtBidOpenedFor">Bid Opened For(Days)</label>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5"
                                                    ForeColor="Red" Font-Bold="true" ControlToValidate="txtBidOpenedFor" ValidationGroup="btnSubmitBid">*</asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtBidOpenedFor" runat="server" CssClass="form-control"
                                                    type="number" min="1" autocomplete="off"></asp:TextBox>
                                            </div>
                                            </div>
                                            </div>
                                    </div>
                                  
                                         
                                <div id="dvProcumentPlan" class="row" runat="server" visible="false" >
                                    <div class="col-md-12 form-group" style="padding-left: 0px;">
                                         <div class="col-md-4">
                                         <div class="form-group">
                                                <label for="txtBidOpenedFor">Start Date</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" style="font-size: 10px;"
                                                    ForeColor="Red" Font-Bold="true" ControlToValidate="txtStartDate" ValidationGroup="btnAddProcumentPlanTogv">*Please select date</asp:RequiredFieldValidator>
                                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator29" style="font-size: 10px;"
                                                    ForeColor="Red" Font-Bold="true" ControlToValidate="txtStartDTime" ValidationGroup="btnAddProcumentPlanTogv">*Please select time</asp:RequiredFieldValidator>
                                                <asp:TextBox ID="txtStartDate" runat="server" style="width: 50%;float: left;" CssClass="form-control customDate" type="date" 
                                                    data-date=""  data-date-format="DD MMM YYYY" autocomplete="off"   onchange="dateChange(this)" ></asp:TextBox>
                                                  <asp:TextBox ID="txtStartDTime" runat="server" style="width: 40%;float: right" CssClass="form-control" type="time" autocomplete="off"  ></asp:TextBox>
                                      
                                            </div>
                                         </div>
                                         <div class="col-md-4">
                                         <div class="form-group">
                                                <label for="txtBidOpenedFor">End Date</label>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" style="font-size: 10px;"
                                                    ForeColor="Red" Font-Bold="true" ControlToValidate="txtEndDate" ValidationGroup="btnAddProcumentPlanTogv">*Please select date</asp:RequiredFieldValidator>
                                              <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator779"  style="font-size: 10px;"
                                                    ForeColor="Red" Font-Bold="true" ControlToValidate="txtEndDTime" ValidationGroup="btnAddProcumentPlanTogv">*Please select time</asp:RequiredFieldValidator> 
                                             <asp:TextBox ID="txtEndDate" runat="server" style="width: 50%;float: left;" CssClass="form-control customDate" type="date"  data-date=""  data-date-format="DD MMM YYYY"
                                                    autocomplete="off"  onchange="dateChange(this)" ></asp:TextBox>
                                              <asp:TextBox ID="txtEndDTime" runat="server" style="width: 40%;float: right" CssClass="form-control" type="time" autocomplete="off"  ></asp:TextBox>                                    
                                            </div>
                                         </div>
                                         <div class="col-md-4" style=" margin-top: 25px;">
                                         <div class="form-group">
                                                      <asp:Button runat="server" ID="btnAddProcumentPlanTogv"   CssClass="btn btn-info" Text="Add Plan" onclick="btnAddProcumentPlanTogv_Click" ValidationGroup="btnAddProcumentPlanTogv" Enabled="true" />
                                            </div>
                                         </div>
                                    </div>
                                </div>
                                 <div id="dvProcumentPlanGrid" class="row" runat="server" visible="false" >
                                  <div class="row">
                                     <div class="col-md-7" style="margin-left: 15px;">
                                      <div class="table-responsive">
                                   
                                          <asp:GridView ID="gvProcumentPlan" runat="server" CssClass="table table-responsive" 
                                                   GridLines="None" AutoGenerateColumns="false" HeaderStyle-BackColor="#3C8DBC" HeaderStyle-ForeColor="White">
                                                    <Columns>
                          
                                                    <asp:BoundField DataField="PlanId" HeaderText="PlanId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="PlanName" HeaderText="Plan" />
                                                   <asp:BoundField DataField="StartDate" HeaderText="Start Date" />
                                                    <asp:BoundField DataField="EndDate" HeaderText="End Date"  />
                                                     <asp:TemplateField HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:Button CssClass="btn btn-danger btn-xs" ID="Delete"
                                                                        runat="server" OnClick="btnDelete_Click"
                                                                        Text="Delete" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                               </Columns>
                                            </asp:GridView>
                                        </div>
                                       </div>

                                      </div>
                                 </div>

                                   
                                      
               <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                        <label for="procedure">Bid Bond</label>
                            <div class="input-group">
                            <span class="input-group-addon">
                            <asp:RadioButton ID="rdoRequired" runat="server"  AutoPostBack="true" GroupName="bond" oncheckedchanged="rdoRequired_CheckedChanged"></asp:RadioButton>
                            </span>
                            <asp:TextBox ID="txtRequired" Enabled="false"  runat="server"    CssClass="form-control"  Text="Required"></asp:TextBox>
                            </div><br />
                            <label> Enter Amount or Percentage</label>
                        <asp:TextBox ID="txtAmount" placeholder="Amount" runat="server" CssClass="form-control" type="number" autocomplete="off"></asp:TextBox> <br />
                                                    
                        <div class="form-group">
                            <label for="txtPeriodFrom">Period From</label>
                            <asp:TextBox ID="txtPeriodFrom" runat="server" type="date" CssClass="form-control customDate"   data-date=""  data-date-format="DD MMM YYYY" onchange="dateChange(this)"
                            autocomplete="off"  ></asp:TextBox>
                        </div>
                                    
                        </div>
                    </div>
                    <div class="col-md-3" style="margin-top: 25px;">
                        <div class="form-group">
                        
                        <div class="input-group">
                            <span class="input-group-addon">
                            <asp:RadioButton ID="rdoNotRequired" Checked="true" AutoPostBack="true" runat="server"  GroupName="bond" oncheckedchanged="rdoNotRequired_CheckedChanged"  ></asp:RadioButton>
                            </span>
                            <asp:TextBox ID="txtNotRequired" Enabled="false" runat="server"   CssClass="form-control"   Text="Not Required"></asp:TextBox>
                        </div><br /> 
                        <asp:TextBox ID="txtPercentage" style="margin-top: 23px;"  placeholder="%" runat="server" CssClass="form-control" type="number" autocomplete="off"></asp:TextBox>
                       
                        <br />
                       
                        <div class="form-group">
                            <label for="txtPeriodTo">To</label>
                            <asp:TextBox ID="txtPeriodTo" runat="server" type="date" CssClass="form-control customDate" data-date=""  data-date-format="DD MMM YYYY" onchange="dateChange(this)"
                            autocomplete="off"></asp:TextBox>
                        </div>
                        </div>
                </div>
                    <div class="col-md-3">
                        <div class="form-group">

                        <label for="procedure">Performance Bond</label>
                        <div class="input-group">
                            <span class="input-group-addon">
                            <asp:RadioButton ID="rdoRequiredPerformance" AutoPostBack="true" runat="server" GroupName="Performance" oncheckedchanged="rdoRequiredPerformance_CheckedChanged" ></asp:RadioButton>
                            </span>
                                                      
                            <asp:TextBox ID="txtRequiredPerformance" Enabled="false" runat="server"   CssClass="form-control"   Text="Required"></asp:TextBox>
                        </div>        <br />
                        <label> Enter Amount or Percentage</label>
                        <asp:TextBox ID="txtAmountRP"  placeholder="Amount" runat="server" CssClass="form-control" type="number" autocomplete="off"></asp:TextBox>
                        <br />
                                                    
                        <div class="form-group">
                            <label for="txtPeriodfrom1">Period From</label>
                            <asp:TextBox ID="txtPeriodfrom1" runat="server" type="date" CssClass="form-control customDate" data-date=""  data-date-format="DD MMM YYYY" onchange="dateChange(this)"
                            autocomplete="off"  ></asp:TextBox>
                        </div>
                        </div>
                        </div>
                        <div class="col-md-3" style="margin-top: 25px;">
                        <div class="form-group">
              
                        <div class="input-group">
                        <span class="input-group-addon">
                        <asp:RadioButton ID="rdoNotRequiredPerformance" AutoPostBack="true" runat="server" GroupName="Performance" oncheckedchanged="rdoNotRequiredPerformance_CheckedChanged" Checked="true"  ></asp:RadioButton>
                        </span>
                        <asp:TextBox ID="txtNotRequiredPerformance" Enabled="false" runat="server"   CssClass="form-control"   Text="Not Required"></asp:TextBox>
                        </div>  <br />                            
                          
                        <asp:TextBox ID="txtPercentageRP" style="margin-top:23px;"  placeholder="%" runat="server" CssClass="form-control" type="number" autocomplete="off"></asp:TextBox>
                         <br />
                                                    
                                    <div class="form-group">
                                    <label for="txtPeriodfrom1">To</label>
                                    <asp:TextBox ID="txtPeriodTo1" runat="server" type="date" CssClass="form-control customDate" data-date=""  data-date-format="DD MMM YYYY" onchange="dateChange(this)"
                                        autocomplete="off" ></asp:TextBox>
                                    </div>
                                                      
                        </div>
                </div>
               </div>
                                    </div>
                                </div>
                        <div class="box-footer"  id="divBoxFooter" runat="server">
                            <div class="form-group">
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger pull-right"
                                    style="margin-left:4px" OnClick="btnCancel_Click"></asp:Button>
                                <asp:Button ID="btnSubmitBid" runat="server" Text="Submit"
                                    ValidationGroup="btnSubmitBid" CssClass="btn btn-primary pull-right" OnClick="btnSubmitBid_Click"></asp:Button>
                           
                             <asp:Button runat="server" ID="btnCapexDocs" CssClass="btn btn-info pull-right paddingRight" Text="Capex Docs" OnClick="btnCapexDocs_Click"  />
                            
                            </div>
                        </div>
                       
                   
                    <div class="box box-info" runat="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Previously Submitted Bids</h3>
                        </div>
                        <!-- /.box-header -->
                        <div class="box-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView runat="server" ID="gvBids" GridLines="None" CssClass="table table-responsive table-striped "
                                            AutoGenerateColumns="false" DataKeyNames="BidId" OnRowDataBound="gvBids_RowDataBound"
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
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="BidId" HeaderText="BidItemId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="PrdId" HeaderText="PRDId"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="CategoryId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                                                    <asp:BoundField DataField="SubCategoryId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="SubCategoryName" HeaderText="Sub-Category Name" />
                                                    <asp:BoundField DataField="ItemId" HeaderText="Item Id"
                                                        HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                                                    <%--<asp:BoundField DataField="Qty" HeaderText="Quantity"/>--%>
                                                    <asp:TemplateField HeaderText="Inventory Details">
                                                        <ItemTemplate>
                                                             <asp:label ID="lblInventory"  type="text" runat="server" Text='<%# decimal.Parse(Eval("Qty").ToString()).ToString() + " " + Eval("UnitShortName").ToString() %>'></asp:label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EstimatedPrice" HeaderText="Estimated Price"/>

                                                    <asp:TemplateField HeaderText="More Details">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnMoreBidItemDetails" runat="server" Text="View" OnClick="btnMoreBidItemDetails_Click"/>
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
                                                <asp:BoundField DataField="CreateDate" HeaderText="Created Date"
                                                    DataFormatString="{0:dd-MM-yyyy}" />
                                                <asp:BoundField DataField="StartDate" HeaderText="Start Date"
                                                    DataFormatString="{0:dd-MM-yyyy}" />
                                                <asp:BoundField DataField="EndDate" HeaderText="End Date"
                                                    DataFormatString="{0:dd-MM-yyyy}" />
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
                                                <asp:BoundField DataField="ApprovalRemarks" HeaderText="Remarks" />
                                                
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                      
                </section>
                <asp:HiddenField ID="hdnStatus" runat="server"/>
            </ContentTemplate>
            

            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmitBid" />
                <asp:PostBackTrigger ControlID="btnCancel" />
            </Triggers>
        </asp:UpdatePanel>
    </form>

    <script src="AdminResources/js/select2.full.min.js"></script>

    <script type="text/javascript">

        Sys.Application.add_load(function () {
            $(function () {
                $('.select2').select2();
            })

            var customDates = $(".customDate");
            for (x = 0 ; x < customDates.length ; ++x) {
                if ($(customDates[x]).val() != "") {
                    $(customDates[x]).attr('data-date', moment($(customDates[x]).val(), 'YYYY-MM-DD HH:MM').format($(customDates[x]).attr('data-date-format')));
                }
            }
        });


        function CheckBoxChecked(CheckBox) {
            //get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvPRView.ClientID %>');
            var TargetChildControl = "CheckBox1";
            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl, 0) >= 0) {
                    Inputs[n].checked = CheckBox.checked;
                    CheckBox.checked ?Inputs[n].disabled = 'disabled' : Inputs[n].disabled = '';
                }
                else {
                }
        }
       

        function dateChange(obj) {
            if (obj.value) {
                $(obj).attr('data-date', moment(obj.value, 'YYYY-MM-DD').format($(obj).attr('data-date-format')));
            } else {
                $(obj).attr('data-date', '');
            }
        }

         function ViewPr(obj) {
            var PrId = <%=ViewState["PrId"].ToString()%>;
            var href = "ViewPRNew.aspx?PrId="+PrId+""
            window.open(href, '_blank');
         }

        function ViewMrn(obj) {
            var MrnId = <%=ViewState["MrnId"] != null ? ViewState["MrnId"].ToString() : "0"%>;
            var href = "ViewMRNNew.aspx?MrnId="+MrnId+""
            window.open(href, '_blank');
        }

        
            $('.clmdlFileUpload').on({
                click: function () {
                    if ($('#ContentSection_hdnStatus').val() == "2") {
                        event.preventDefault();
                        $('#mdlFileUpload').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlBidMoreDetails').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }
                }

            });

            $('.clmdlSupportiveDocs').on({
                click: function () {
                    event.preventDefault();
                    if ($('#ContentSection_hdnStatus').val() == "2") {
                        $('#mdlSupportiveDocs').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlBidMoreDetails').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }
                }

            });

            $('.clmdlItemSpecs').on({
                click: function () {
                    event.preventDefault();
                    if ($('#ContentSection_hdnStatus').val() == "2") {
                        $('#mdlItemSpecs').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlBidMoreDetails').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }
                }

            });

            $('.clmdlReplacementImages').on({
                click: function () {
                    event.preventDefault();
                    if ($('#ContentSection_hdnStatus').val() == "2") {
                        $('#mdlReplacementImages').modal('hide');
                        //$('#mdlQuotations').modal('show');
                        $('#mdlBidMoreDetails').modal('show');
                        $('div').removeClass('modal-backdrop');
                    }
                }

            });
       
           
    </script>
</asp:Content>
