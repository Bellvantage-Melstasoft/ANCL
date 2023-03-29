<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true"
    CodeBehind="CompanyCustomizeBidding.aspx.cs" Inherits="BiddingSystem.CompanyCustomizeBidding" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
    <section class="content-header">
      <h1>
       Customize Bids
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Customize Bids</li>
      </ol>
    </section>
    <br />
    <style type="text/css">
        .customers
        {
            font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }
        
        .customers td, .customers th
        {
            border: 1px solid #ddd;
            padding: 8px;
        }
        
        .customers tr:nth-child(even)
        {
            background-color: #f2f2f2;
        }
        
        .customers tr:hover
        {
            background-color: #ddd;
        }
        
        .customers th
        {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
            background-color: #467394;
            color: white;
        }
        .customers td
        {
            padding-top: 12px;
            padding-bottom: 12px;
            text-align: center;
        }
        input[type=number]::-webkit-inner-spin-button, input[type=number]::-webkit-outer-spin-button
        {
            -webkit-appearance: none;
            margin: 0;
        }
    </style>
    <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content" style="background-color: #a2bdcc;">
                <div class="modal-header" style="background-color: #148690; color: White;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">×</span></button>
                    <h4 class="modal-title">
                        <asp:Label ID="lblItemNamePopup" runat="server" Text=""></asp:Label>Attachments</h4>
                </div>
                <div class="modal-body" style="background-color: white;">
                    <div class="login-w3ls">
                        <div class="modal-body">
                            <div class="row" style="margin-left: 10px;">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive tablegv"
                                            Style="border-collapse: collapse; color: black;" GridLines="None" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="DepartmentId" HeaderText="ItemId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgPicture" runat="server" ImageUrl='<%# Eval("FilePath") %>' Style="width: 100px;
                                                            height: 80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FileName" HeaderText="FileName" />
                                                <asp:BoundField DataField="FilePath" HeaderText="FilePath" HeaderStyle-CssClass="hidden"
                                                    ItemStyle-CssClass="hidden" />
                                                <%-- <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnView" ImageUrl="~/images/view-icon-614x460.png" Style="width: 39px; height: 26px" runat="server" OnClick="btnView_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                                <%-- <asp:TemplateField HeaderText="Download">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDownload" ImageUrl="~/images/dwnload.png" Style="width: 20px; height: 17px; margin-top: 4px;" runat="server" OnClientClick="return GetDownload()" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div>
                                    <label id="lbMailMessage" style="margin: 3px; color: maroon; text-align: center;">
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div   style="background-color:#e8ecef;height:35px;">
                            <span type="button"  class="btn btn-danger pull-right" data-dismiss="modal">
                                Close</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalDeleteYesNo" class="modal fade">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 id="lblTitleDeleteYesNo" class="modal-title">
                        Confirmation</h4>
                </div>
                <div class="modal-body">
                    <p>
                        Are you sure to Delete this Record ?</p>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-primary" Text="Yes">
                    </asp:Button>
                    <button id="btnNoDeleteYesNo" onclick="return hideDeleteModal();" type="button" class="btn btn-danger">
                        No</button>
                </div>
            </div>
        </div>
    </div>
    <asp:ScriptManager runat="server" ID="sm">
                </asp:ScriptManager>
                 <asp:UpdatePanel ID="UpdatePanel1" ChildrenAsTriggers="true" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
    <asp:HiddenField ID="hdnImgPathEdit" runat="server" />
    <section class="content">

        <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" ForeColor="White"  runat="server"></asp:Label>
                            </strong>
                        </div>


      <!-- SELECT2 EXAMPLE -->
      <div class="box box-info">
        <div class="box-header with-border">
          <h3 class="box-title" >Customize Bids</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
          
          <div class="row">
            <div class="col-md-6">

                  <div class="form-group">
                <label for="exampleInputEmail1">Select Supplier</label><label id="lblErrorSupplier" style="color:red"></label>
                <asp:DropDownList ID="ddlSupplier" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged">
                </asp:DropDownList>
                </div>

                <div class="form-group">
                <label for="exampleInputEmail1">Progress PR</label><label id="lblErrorProgressPr" style="color:red"></label>
                <asp:DropDownList ID="ddlProgressPR" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlProgressPR_SelectedIndexChanged"> </asp:DropDownList>
                </div>

                 <div class="form-group">
                <label for="exampleInputEmail1">PR Item</label><label id="lblErrorPRItems" style="color:red"></label>
                <asp:DropDownList ID="ddlPRItems" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPRItems_SelectedIndexChanged">
                </asp:DropDownList>
                </div>

                <div class="form-group">
                 <label for="exampleInputEmail1">Attachments</label><label id="Label1" style="color:red"></label>
            <%--    <h4 class="w3-head" style="color:Black">
                Attachments : &nbsp;&nbsp; </h4>--%>
                <button onclick="ShowModalPopup()" type="button" class="form-control" style=" background-color: #f7b92d; "  CssClass="btn btn-small btn-warning" > View Images / Attachments <i class=" icon-copy"></i></button> 
                
                </div>

                 <div class="form-group">
                 <label for="exampleInputEmail1">Supplier Uploaded Files</label><label id="Label2" style="color:red"></label>
                 </div>
           <div>
               <asp:GridView runat="server" ID="gvUserDocuments" EmptyDataText="No Files Found" OnPageIndexChanging="gvUserDocuments_OnPageIndexChanged" PageIndex="3" AllowPaging="true" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive ">
             <Columns>
                 <asp:BoundField DataField="SupplierId" HeaderText="SupplierId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="QuotationId" HeaderText="QuotationId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="PrId" HeaderText="PrId" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="ItemId" HeaderText="ItemId"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="FileName" HeaderText="FilePath" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden"/>
                 <asp:BoundField DataField="FilePath" HeaderText="FileName"  />
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ForeColor="#0099cc"  OnClick="lbtnview_Click" ID="lbtnview">View</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ForeColor="Red" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete this item?');" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton runat="server" ForeColor="Blue" ID="lbtnDownload" OnClick="lbtnDownload_Click" >Download</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
             </Columns>
          </asp:GridView>
            </div>
    </div>

              <div class="col-md-6"   id="divItemDetails" runat="server" >
         
                 <div class="form-group">
       <div class="row">
        <div class="col-xs-12 table-responsive">
          <table class="table table-striped" style="background-color:#ecfbeb;">

               <tr>
                <td style="color:Black;font-weight:bold;">
                   Quantity:
                </td>
                  <td>
                        <p style="font-weight:bold;font-size:14px" class="form-control" id="lblQuantity"></p>
                  </td>
              </tr>

              <tr>
                <td style="color:Black;font-weight:bold;">
                   Expires On:
                </td>
                  <td>
                        <p style="color:Red;font-weight:bold;font-size:14px" class="form-control" id="demo"></p>
                  </td>
              </tr>
            <tr>
                <td style="color:Black;font-weight:bold;">
                    Unit Price:
                </td>
                <td>
                    <asp:TextBox ID="txtUnitPrice" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </td>
                 <td>
                    <label id="lblErrorUnitPrice" style="color:red"></label>
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="color:Black;font-weight:bold;">
                    Sub Total:
                </td>
                <td>
                    <asp:TextBox ID="txtTotalPrice" runat="server" CssClass="form-control" laceholder=""></asp:TextBox>
                </td>
                <td style="color:Black;font-weight:bold;">
                     VAT/NBT Inclusive
                </td>
                <td>
                    <asp:CheckBox ID="chkVatNbt" runat="server"/>
                </td>
            </tr>
            <tr>
                <td style="color:Black;font-weight:bold;">
                    NBT:
                </td>
                <td>
                    <asp:TextBox ID="txtNBT" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="color:Black;font-weight:bold;">
                    VAT:
                </td>
                <td>
                    <asp:TextBox ID="txtVAT" runat="server" CssClass="form-control" placeholder=""></asp:TextBox>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
             <tr>
                <td style="color:Black;font-weight:bold;">
                    Total Price:
                </td>
                <td>
                    <asp:TextBox ID="txtSubTotal" runat="server" CssClass="form-control"  placeholder=""></asp:TextBox>
                </td>
                <td>
                    <label id="lblError" style="color:red"></label>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
          
        </table>
        </div>
        <!-- /.col -->
      </div>
	</div>
	             <div class="form-group">
	<%--<form class="form-horizontal ">--%>



          

                
                       <label for="exampleInputEmail1">Item Specifications</label><label  style="color:red"></label>
             
    <div class="table-responsive">
       <asp:GridView ID="gvBOM"  runat="server" CssClass="table table-responsive customers" GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Specification Found">
        <Columns>
            <asp:BoundField DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="SeqId" HeaderText="Seq Id" />
            <asp:BoundField DataField="Meterial" HeaderText="Material" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
          <%--  <asp:TemplateField HeaderText="Comply" ItemStyle-Width="500">
              <ItemTemplate>
                  <asp:RadioButton  style="display:inline" ID="RadioButtonYes" runat="server" Text="Yes" GroupName="Comply1" Checked ='<%#Eval("Comply").ToString()=="1"?true:false %>'/>
                  <asp:RadioButton style="display:inline" ID="RadioButtonNo" runat="server" Text="No" GroupName="Comply1" Checked ='<%#Eval("Comply").ToString()=="0"?true:false %>'/> 
              </ItemTemplate>
           </asp:TemplateField>--%>

              <asp:TemplateField HeaderText="Comply"  HeaderStyle-ForeColor="White">
              <ItemTemplate>
                  <asp:RadioButton ID="RadioButtonYes" style="color:Black;"  runat="server" GroupName="Comply" Text="Yes" Checked='<%# Eval("comply").ToString()=="1"?true:false %>' /> &nbsp;&nbsp;&nbsp;&nbsp;
                  <asp:RadioButton ID="RadioButtonNo"  style="color:Black;" runat="server" GroupName="Comply" Text="No" Checked='<%# Eval("comply").ToString()=="0"?true:false %>'/>
              </ItemTemplate>
           </asp:TemplateField>
          <%-- <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
                 <asp:TextBox ID="txtRemarks" Text='<%# Eval("Remarks") %>' runat="server" TextMode="MultiLine"></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
             <asp:BoundField DataField="Comply" HeaderText="Comply"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>

             <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
                 <asp:TextBox ID="txtRemarks"  runat="server" TextMode="MultiLine"></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            <%-- <asp:BoundField DataField="Comply" HeaderText="Comply"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>--%>
        </Columns>
    </asp:GridView>
    </div>
  
                     
             
    <br/>

          <div class="row">
            <div class="col-sm-12">

                  <div class="form-group">
                       <label for="exampleInputEmail1">Supplier terms and conditions</label><label  style="color:red"></label>
                         <asp:TextBox ID="txtTermsConditions" runat="server" CssClass="form-control" Rows="4" Columns="150"  TextMode="MultiLine"></asp:TextBox>
                      </div>
                </div>
              </div>

              <br />
                     <div class="control-group">
          <label for="exampleInputEmail1"  style="font-weight:bold;">Supplier Attachments(One time 10 files can be uploaded)</label>
          <label style="font-weight:bold; color:red;" id="MultipleFileError"></label>
                 <div class="input-group margin">
                 <asp:FileUpload runat="server"  ID="FileUpload1" CssClass="form-control input-large" style="border:solid 1px;border-radius: 5px;padding:2px;border-color:#cccccc;" AllowMultiple="true" onchange="readFilesURL(this);" />
                 <span class="input-group-btn">
                 <button class="btn btn-info btn-flat" id="clearFiles" onclick="return clearImages()">Clear</button>
                 </span>
                 </div>
                <div class="row"  style="overflow-y:auto; overflow-x:hidden; max-height:300px;">
                    <table id="tblUpload" class="table table-hover" style="border:none;margin-left:30px;"  border="1">
                    </table>
                </div>   
          </div>  

        </div>

 <div class="box-footer">
            <span class="pull-right">
                 <asp:Button ID="btnSave" runat="server" Text="Submit Bid" OnClientClick="return validate();" CssClass="btn btn-primary"   ValidationGroup="btnSave" OnClick="btnSave_Click" ></asp:Button>
                 <asp:Button ID="btnClear"  runat="server" Text="Clear"  OnClick="btnClear_Click" CssClass="btn btn-danger" OnClientClick="return clearImages()"></asp:Button>
                </span>
              </div>

              </div>

            </div>
        
          </div>
             
       
     
                 </div>
            
    </section>
    <div class="panel-body">
        <div class="co-md-12">
            <div class="table-responsive">
                <asp:GridView ID="gvAddItems" runat="server" CssClass="table table-responsive tablegv"
                    GridLines="None" AutoGenerateColumns="false" PageSize="200" AllowPaging="true">
                    <Columns>
                        <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                            <ItemTemplate>
                                <asp:Label Text='<%#Eval("ItemId").ToString() %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                            <ItemTemplate>
                                <asp:Label Text='<%#Eval("CategoryId").ToString() %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                            <ItemTemplate>
                                <asp:Label Text='<%#Eval("SubCategoryId").ToString() %>' runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="CategoryId" HeaderText="CategoryId" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                        <asp:BoundField DataField="SubCategoryName" HeaderText="Sub Category Name" />
                        <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                        <asp:BoundField DataField="SubCategoryId" HeaderText="SubCategoryId" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="ReferenceNo" HeaderText="Item Ref.No" />
                        <asp:BoundField DataField="IsActive" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="CreatedDateTime" HeaderText="CreatedDateTime" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="CreatedBy" HeaderText="CreatedBy" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="UpdatedDateTime" HeaderText="UpdatedDateTime" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="UpdatedBy" HeaderText="UpdatedBy" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden" />
                        <asp:BoundField DataField="ImagePath" HeaderText="ImagePath" HeaderStyle-CssClass="hidden"
                            ItemStyle-CssClass="hidden" />
                        <asp:TemplateField HeaderText="Active">
                            <ItemTemplate>
                                <asp:Label Text='<%#Eval("IsActive").ToString()== "1"?"Yes":"No" %>' Font-Bold="true"
                                    ForeColor='<%#Eval("IsActive").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" Style="width: 26px;
                                    height: 20px" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnCancelRequest" CssClass="deleteItem" ImageUrl='<%#Eval("IsActive").ToString()== "1"?"~/images/delete.png":"~/images/dlt.png" %>'
                                    Enabled='<%#Eval("IsActive").ToString()== "1"?true:false %>' ToolTip='<%#Eval("IsActive").ToString()== "1"?"Delete":"Deleted" %>'
                                    Style="width: 26px; height: 20px;" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnItemId" runat="server" />
    <asp:HiddenField ID="hdnCatecoryIdId" runat="server" />
    <asp:HiddenField ID="hdnSubCategoryId" runat="server" />
    <asp:HiddenField ID="hdnEnddateTime" runat="server" />
    <asp:HiddenField ID="hdnItemQuantity" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
     </ContentTemplate>

                      <Triggers>
         <asp:PostBackTrigger ControlID="btnSave" />
         <asp:AsyncPostBackTrigger ControlID="ddlSupplier" />
         <asp:AsyncPostBackTrigger ControlID="ddlProgressPR" />
         <asp:PostBackTrigger ControlID="ddlPRItems" />
      </Triggers>
        </asp:UpdatePanel>
    </form>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">

        function clearImages(){
            document.getElementById('ContentSection_FileUpload1').value="";
            return true;
        }

        $("#<%=btnSave.ClientID %>").click(function () {

            var fileInputDocs = document.getElementById("ContentSection_FileUpload1");

            if (fileInputDocs.files.length <= 10)
            {
                return true;
            }
            else
            {
                $('#MultipleFileError').text('No of Files exceeded than 10');
                return false;
            }             
                          
                        
        });

        
        // LoadImage();
        function LoadImage() {
            var text = "";
            var imagePath = "";
            var BidImagePath = "";
            imagePath = Image.split('~')
            BidImagePath = imagePath[1];
            var htmlcode =
				  '<div class="thumbnail" style="background-color:#ebf1f7;">' +
				  '<a  href="#"><img src=' + BidImagePath + '  alt="" style="height:136px"/></a>' +
				  '</div>' + ''
            document.getElementById("image").innerHTML = htmlcode;
        }
    </script>
    <script>

        var EndDate = $('#ContentSection_hdnEnddateTime').val();
        var Quontity = $('#ContentSection_hdnItemQuantity').val();
        var endDate = new Date(EndDate);
        var d = endDate;
        var time01 = (
                ("00" + (d.getMonth() + 1)).slice(-2) + " " +
                ("00" + d.getDate()).slice(-2) + " ," +
                d.getFullYear() + " " +
                ("00" + d.getHours()).slice(-2) + ":" +
                ("00" + d.getMinutes()).slice(-2) + ":" +
                ("00" + d.getSeconds()).slice(-2)
            );
        // Set the date we're counting down to
        var countDownDate = new Date(time01).getTime();
        // Update the count down every 1 second
        var x = setInterval(function () {
            // Get todays date and time
            var now = new Date().getTime();
            // Find the distance between now an the count down date
            var distance = countDownDate - now;
            // Time calculations for days, hours, minutes and seconds
            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
            var seconds = Math.floor((distance % (1000 * 60)) / 1000);
            // Display the result in the element with id="demo"
            document.getElementById("demo").innerHTML = "Expires On : " + days + "d " + hours + "h " + minutes + "m " + seconds + "s ";
            // If the count down is finished, write some text 
            if (distance < 0) {
                clearInterval(x);
                document.getElementById("demo").innerHTML = "EXPIRED";
            }
        }, 1000);


        document.getElementById("lblQuantity").innerHTML = Quontity;
    </script>
    <script type="text/javascript">
        $("#ContentSection_txtUnitPrice").keyup(function () {
            var Quontity = $('#ContentSection_hdnItemQuantity').val();
            if ($(this).val() != "") {
                debugger;
                var price = parseFloat($(this).val());
                var quontity = parseFloat(Quontity);
                var totalPrice = quontity * price;
                $('#ContentSection_txtTotalPrice').val(totalPrice);
                if ($("#ContentSection_txtTotalPrice").val() != "" && $("#ContentSection_txtUnitPrice").val() != "") {
                    $('#ContentSection_txtSubTotal').val(totalPrice.toFixed(2));
                }
            }
            else {
                $("#ContentSection_txtTotalPrice").val("");
                $("#ContentSection_txtUnitPrice").val("");
                $('#ContentSection_txtNBT').val("");
                $('#ContentSection_txtVAT').val("");
            }
        });

        $("#ContentSection_chkVatNbt").change(function () {
            if (this.checked) {
                var totPrice = parseFloat($('#ContentSection_txtTotalPrice').val());
                var NbtAmount = parseFloat((totPrice * 2) / 98);

                if ($("#ContentSection_txtTotalPrice").val() != "" && $("#ContentSection_txtUnitPrice").val() != "") {
                    $('#ContentSection_txtNBT').val((NbtAmount).toFixed(2));
                    var VatAmount = parseFloat((totPrice + NbtAmount) * 0.15);
                    $('#ContentSection_txtVAT').val((VatAmount).toFixed(2));
                    var SubTotalVal = (totPrice + NbtAmount + VatAmount);
                    $('#ContentSection_txtSubTotal').val(SubTotalVal.toFixed(2));
                }
                if ($("#ContentSection_txtTotalPrice").val() == "" && $("#ContentSection_txtUnitPrice").val() == "") {
                    $('#ContentSection_txtSubTotal').val("");
                    $("#ContentSection_txtTotalPrice").val("");
                    $("#ContentSection_txtUnitPrice").val("");
                    $('#ContentSection_txtNBT').val("");
                    $('#ContentSection_txtVAT').val("");
                }
            }
            else {
                $('#ContentSection_txtNBT').val("");
                $('#ContentSection_txtVAT').val("");
                if ($("#ContentSection_txtTotalPrice").val() == "" && $("#ContentSection_txtUnitPrice").val() == "") {
                    $('#ContentSection_txtSubTotal').val("");
                }
                if ($("#ContentSection_txtTotalPrice").val() != "" && $("#ContentSection_txtUnitPrice").val() != "") {
                    var TotatlPriceWithoutNbtVat = parseFloat($('#ContentSection_txtTotalPrice').val());
                    $('#ContentSection_txtSubTotal').val(TotatlPriceWithoutNbtVat.toFixed(2));
                }
            }
        });



        $("#ContentSection_txtNBT").keyup(function () {
            if ($("#ContentSection_chkVatNbt").prop('checked') == true && $("#ContentSection_txtUnitPrice").val() != "") {

                if ($(this).val() != "") {

                    var price = parseFloat($("#ContentSection_txtUnitPrice").val() == "" ? 0 : $("#ContentSection_txtUnitPrice").val());
                    var nbtAmount = parseFloat($("#ContentSection_txtNBT").val() == "" ? 0 : $("#ContentSection_txtNBT").val());
                    var vatAmount = parseFloat($("#ContentSection_txtVAT").val() == "" ? 0 : $("#ContentSection_txtVAT").val());
                    var quontity = parseFloat(Quontity);
                    var totalPrice = (quontity * price) + nbtAmount + vatAmount;
                    $('#ContentSection_txtSubTotal').val(totalPrice.toFixed(2));
                }
                else {

                    var price = parseFloat($("#ContentSection_txtUnitPrice").val() == "" ? 0 : $("#ContentSection_txtUnitPrice").val());
                    var nbtAmount = parseFloat($("#ContentSection_txtNBT").val() == "" ? 0 : $("#ContentSection_txtNBT").val());
                    var vatAmount = parseFloat($("#ContentSection_txtVAT").val() == "" ? 0 : $("#ContentSection_txtVAT").val());
                    var quontity = parseFloat(Quontity);
                    var totalPrice = (quontity * price) + nbtAmount + vatAmount;
                    $('#ContentSection_txtSubTotal').val(totalPrice.toFixed(2));
                }
            }
            else { $("#ContentSection_txtNBT").val(""); }
        });



        $("#ContentSection_txtVAT").keyup(function () {
            if ($("#ContentSection_chkVatNbt").prop('checked') == true && $("#ContentSection_txtUnitPrice").val() != "") {

                if ($(this).val() != "") {

                    var price = parseFloat($("#ContentSection_txtUnitPrice").val() == "" ? 0 : $("#ContentSection_txtUnitPrice").val());
                    var nbtAmount = parseFloat($("#ContentSection_txtNBT").val() == "" ? 0 : $("#ContentSection_txtNBT").val());
                    var vatAmount = parseFloat($("#ContentSection_txtVAT").val() == "" ? 0 : $("#ContentSection_txtVAT").val());
                    var quontity = parseFloat(Quontity);
                    var totalPrice = (quontity * price) + nbtAmount + vatAmount;
                    $('#ContentSection_txtSubTotal').val(totalPrice.toFixed(2));
                }
                else {

                    var price = parseFloat($("#ContentSection_txtUnitPrice").val() == "" ? 0 : $("#ContentSection_txtUnitPrice").val());
                    var nbtAmount = parseFloat($("#ContentSection_txtNBT").val() == "" ? 0 : $("#ContentSection_txtNBT").val());
                    var vatAmount = parseFloat($("#ContentSection_txtVAT").val() == "" ? 0 : $("#ContentSection_txtVAT").val());
                    var quontity = parseFloat(Quontity);
                    var totalPrice = (quontity * price) + nbtAmount + vatAmount;
                    $('#ContentSection_txtSubTotal').val(totalPrice.toFixed(2));
                }
            }
            else { $("#ContentSection_txtVAT").val(""); }
        });


        $("#ContentSection_txtUnitPrice").keyup(function () {
            if ($(this).val() != "") {
                if ($(this).val() > 0) {

                    var price = parseFloat($(this).val());
                    var quontity = parseFloat(Quontity);
                    var totalPrice = quontity * price;
                    $('#ContentSection_txtTotalPrice').val(totalPrice);
                    if ($("#ContentSection_txtTotalPrice").val() != "" && $("#ContentSection_txtUnitPrice").val() != "") {
                        $('#ContentSection_txtSubTotal').val(totalPrice.toFixed(2));
                    }

                    if ($("#ContentSection_chkVatNbt").prop('checked')) {
                        var totPrice = parseFloat($('#ContentSection_txtTotalPrice').val());
                        var NbtAmount = parseFloat((totPrice * 2) / 98);
                        $('#ContentSection_txtNBT').val((NbtAmount).toFixed(2));
                        var VatAmount = parseFloat((totPrice + NbtAmount) * 0.15);
                        $('#ContentSection_txtVAT').val((VatAmount).toFixed(2));
                        var SubTotalVal = (totPrice + NbtAmount + VatAmount);
                        $('#ContentSection_txtSubTotal').val(SubTotalVal.toFixed(2));
                    }
                    else {
                        $('#ContentSection_txtNBT').val("");
                        $('#ContentSection_txtVAT').val("");
                    }
                }
                else {
                    $("#ContentSection_txtTotalPrice").val("");
                    $('#ContentSection_txtNBT').val("");
                    $('#ContentSection_txtVAT').val("");
                    $('#ContentSection_txtUnitPrice').val("");
                    $('#ContentSection_txtSubTotal').val("");

                }
            }
            else {

                $("#ContentSection_txtTotalPrice").val("");
                $('#ContentSection_txtNBT').val("");
                $('#ContentSection_txtVAT').val("");
                $('#ContentSection_txtSubTotal').val("");
            }
        });

        // ---------------------------------------------Start- Separate (1,000)----------------------------
        function thousandsSeparatorAdd(input) {
            var output = input
            if (parseFloat(input)) {
                input = new String(input);
                var parts = input.split(".");
                parts[0] = parts[0].split("").reverse().join("").replace(/(\d{3})(?!$)/g, "$1,").split("").reverse().join("");
                output = parts.join(".");
            }
            return output;
        }
        function thousandsSeparatorRemove(input) {
            input = input.replace(/,/g, '');
            return input;
        }
        // ---------------------------------------------End--- Separate (1,000)----------------------------
    </script>
    <script type="text/javascript">
         //---------------------------------Validate Form------------------------------------------

        $("#btnNoConfirmYesNo").on('click').click(function () {
                     var $confirm = $("#modalConfirmYesNo");
                     $confirm.modal('hide');
                     return this.false;
        });
    </script>
    <script type="text/javascript">
        
        function validate()
        {
            if ($("#ContentSection_ddlSupplier")[0].selectedIndex <= 0 ) {
                $("#lblErrorSupplier").text("*");
                return false;
            }
            else if($("#ContentSection_ddlProgressPR")[0].selectedIndex <= 0)
            {
                $("#lblErrorProgressPr").text("*");
                return false;
            }
            else if($("#ContentSection_ddlPRItems")[0].selectedIndex <= 0)
            {
                $("#lblErrorPRItems").text("*");
                return false;
            }
            else
            {

                var icount = 0;
                var vatValidate = false;
                var gv = document.getElementById("<%= gvBOM.ClientID %>");  
                if(gv != null)
                {
                    for (var i = 1; i < gv.rows.length; i++) {
                        var row = gv.rows[i];
                        var targetcell = row.cells[5];
                        var inputs = targetcell.getElementsByTagName("input");

                        var radioCheck = 0;
                        for (var j = 0; j < inputs.length; j++)
                            if (inputs[j].checked) {
                                icount++;
                                radioCheck++;
                                break;
                            }
                        if (radioCheck == 0) {
                            gv.rows[i].style.backgroundColor = 'Bisque';
                        }
                        else
                        {
                            gv.rows[i].style.backgroundColor = 'white';
                        }
                    }
                }
                if (<%=chkVatNbt.ClientID%>.checked == true)
                {
                    if ( <%=txtNBT.ClientID%>.value != "" && <%=txtVAT.ClientID%>.value != "" )
                    {
                        <%=txtNBT.ClientID%>.style.backgroundColor = "white";
                        <%=txtVAT.ClientID%>.style.backgroundColor = "white";
                        vatValidate = true;
                    }
                    else
                    {
                        if ( <%=txtNBT.ClientID%>.value == "")
                            <%=txtNBT.ClientID%>.style.backgroundColor = "Bisque";
                        else
                            <%=txtNBT.ClientID%>.style.backgroundColor = "white";

                        if ( <%=txtVAT.ClientID%>.value == "")
                            <%=txtVAT.ClientID%>.style.backgroundColor = "Bisque";
                        else
                            <%=txtVAT.ClientID%>.style.backgroundColor = "white";
                        vatValidate = false;
                    }
                }
               
                if (  $('#ContentSection_txtSubTotal').val() !="" &&  $('#ContentSection_txtUnitPrice').val() !="")
                {
                    if(gv!= null)
                    {
                        if(icount == (gv.rows.length - 1))
                        {
                            var $confirm = $("#modalConfirmYesNo");
                            $confirm.modal('show');
                            $("#lblError").text("");
                            $("#lblErrorUnitPrice").text("");
                            return true;
                        }
                    }
                    else
                    {
                        var $confirm = $("#modalConfirmYesNo");
                        $confirm.modal('show');
                        $("#lblError").text("");
                        $("#lblErrorUnitPrice").text("");
                        return true;
                    }
                   
                   
                }
                else { 
                    $("#lblError").text("");
                    $("#lblErrorUnitPrice").text("");

                    if($('#ContentSection_txtSubTotal').val() =="" )
                    {
                        $("#lblError").text("*");
                      
                    }
                    if($('#ContentSection_txtUnitPrice').val() =="")
                    {
                        $("#lblErrorUnitPrice").text("*");
                       
                    }
                    return false;
                }
            
            }
        }
      //------------------------------End Validate Form----------------------------------
    </script>
    <script type="text/javascript">
        function readFilesURL(input) {
            var output = document.getElementById('tblUpload');
            output.innerHTML = '<tr>';
            output.innerHTML += '<th style="background-color:#e8e8e8;"><b>(' + input.files.length + ') Files has been Selected </b></th>';
            for (var i = 0; i < input.files.length; ++i) {
                output.innerHTML += '<td >' + input.files.item(i).name + '</td>';

            }
            output.innerHTML += '</tr>';
        }
    </script>
    <script type="text/javascript">
        function ShowModalPopup() {
            $('#myModal').modal('show');
        }
    </script>

    <script type="text/javascript">
        function readFilesURL(input) {
            var output = document.getElementById('tblUpload');
            output.innerHTML = '<tr>';
            output.innerHTML += '<th style="background-color:#e8e8e8;"><b>(' + input.files.length + ') Files has been Selected </b></th>';
            for (var i = 0; i < input.files.length; ++i) {
                output.innerHTML += '<td >' + input.files.item(i).name + '</td>';

            }
            output.innerHTML += '</tr>';
        }
   </script>
</asp:Content>
