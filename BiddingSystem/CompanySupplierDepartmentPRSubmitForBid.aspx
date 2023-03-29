<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanySupplierDepartmentPRSubmitForBid.aspx.cs" Inherits="BiddingSystem.CompanySupplierDepartmentPRSubmitForBid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
 
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
     
     .TestTable tr:hover {background-color: #ddd
     
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
     .RejectTable th {
         padding-top: 12px;
         padding-bottom: 12px;
         text-align: left;
         background-color: #f5cccc;
         color: Black;
     }
  </style>
     <style type="text/css">
      input[type="file"] {
         display: block;
       }
       .imageThumb {
         max-height: 75px;
         border: 2px solid;
         padding: 1px;
         cursor: pointer;
       }
       .pip {
         display: inline-block;
         margin: 10px 10px 0 0;
       }
       .remove {
         display: block;
         background: #fff;
         border: 1px solid #fff;
         color: red;
         text-align: center;
         cursor: pointer;
       }
       .remove:hover {
         background: white;
         color: black;
       }
  </style>
   <html>
   <head>
    <script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
          <script src="AppResources/js/jquery-ui.js" type="text/javascript"></script>
   <script src="AdminResources/js/autoCompleter.js"></script>
   </head>
   <body>

       <script>
          


           var files;
           var a = new Array();
           var b = new Array();
           var c = new Array();
           var count = 0;

           var fileReplace;
           var x = new Array();
           var y = new Array();
           var z = new Array();
           $( document ).ready(function() {
               InitClientReplace();
           });


         function InitClientReplace() {
                debugger;
                if (window.File && window.FileList && window.FileReader) {
                    if (fileReplace == null) {
                        $("#fileReplace").trigger("change");
                    }
                    else {
                        for (var k = 0; k < x.length; k++) {
                            var f = x[k];
                            var fileReader = new FileReader();
                            var file = z[k];
                            $("<img></img>", {
                                class: "imageThumb",
                                src: y[k],
                                title: file.name,
                                name: 'test'
                            }).insertAfter("#fileReplace");
                            fileReader.readAsDataURL(f);
                        }
                    }
                } else {
                    alert("Your browser doesn't support to File API")
                }

                $("#fileReplace").on("change", function (u) {
                    fileReplace = u.target.files,
               filesLength = fileReplace.length;
                    for (var k = 0; k < filesLength; k++) {
                        var f = fileReplace[k]
                        x.push(f);
                        var fileReader = new FileReader();
                        fileReader.onload = (function (u) {
                            var file = u.target;
                            z.push(u.target);
                            y.push(u.target.result);
                            $("<span class=\"pip\" style='text-align: center;'>" +
                        "<img class=\"imageThumb\" src=\"" + u.target.result + "\" title=\"" + file.name + "\"/>" +
                        "<br/><span id=\"removeReplace\" class=\"imageThumb\"  file ='" + f.name + "' style='border:none;'><img src='images/delete.png' style='width:16px;' /></span>" +
                        "</span>").insertAfter("#fileReplace");

                            $("#removeReplace").click(function (e) {
                                debugger;
                                e.preventDefault();
                                $(this).parent().remove('');
                                var removeArray = new Array();
                                removeArray = fileReplace;
                                var file = $(this).attr('file');
                                for (var z = 0; z < fileReplace.length; z++) {
                                    if (fileReplace[z].name == file) {
                                        //fileReplace.splice(z, 1);
                                        delete(fileReplace[z]);
                                        // files.remove();
                                        break;
                                    }
                                }

                                var z = Array.indexOf($(this).index);
                                if (z != -1) {
                                    Array.splice(z, 1);
                                }
                                count--;

                                $(this).parent(".pip").remove();
                            });

                        });
                        fileReader.readAsDataURL(f);
                    }
                });
            }

</script>
       


    <form id="form1" runat="server" enctype="multipart/form-data" >

  <asp:ScriptManager runat="server" ID="sm">
     </asp:ScriptManager>
     <asp:UpdatePanel ID="Updatepanel1" runat="server">
     <ContentTemplate>
        
     
         <div id="errorAlert" class="modal fade">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #ff0000;">
                                <button type="button"
                                    class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                                <h4 class="modal-title" style="color: white; font-weight: bold;">ERROR</h4>
                            </div>
                            <div class="modal-body">
                                <p id="errorMessage" style="font-weight: bold; font-size: medium;"></p>
                            </div>
                            <div class="modal-footer">
                                <span class="btn btn-danger" data-dismiss="modal" aria-label="Close">OK</span>
                                <%--<button id="btnOki" class="btn btn-success">OK</button>--%>
                            </div>
                        </div>
                    </div>
                </div>

             <div id="SuccessAlert" class="modal fade in">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header"  style="background-color:lightgreen;">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4  class="modal-title">Success</h4>
            </div>
            <div class="modal-body">
                <p style="font-weight:bold; font-size:medium;">Bid has been Submitted Successfully</p>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnOk" runat="server"  CssClass="btn btn-success" Text="OK"  data-dismiss="modal"></asp:Button>
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
                <p>Are you sure to Submit to Bid list?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnApproveYes" runat="server"  CssClass="btn btn-primary" Text="Yes" onclick="btnSave_Click"></asp:Button>
                <button id="btnApproveNo"  type="button" class="btn btn-danger" >No</button>
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
                <h4>Atleast select one Item for Bid Submission</h4>
            </div>
            <div class="modal-footer" style="background-color:white">
                <button id="btnOkAlert"  type="button" class="btn btn-danger"  data-dismiss="modal">OK</button>
            </div>
        </div>
    </div>
</div>

         <%-- <div id="modalReject" class="modal fade">
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
                <p>Are you sure to Reject Purchase Requisition from Bidding?</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnRejectYes" runat="server"  CssClass="btn btn-primary" OnClick="btnReject_Click"  Text="Yes" ></asp:Button>
                <button id="btnRejectNo"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>
--%>
<div id="myModal2" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->

					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1;color:white; "><span aria-hidden="true" style="opacity: 1; ">×</span></button>		
						<h4 class="modal-title">Item Specifications</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive TestTable" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false" EmptyDataText="No Specifications Found">
        <Columns>
            <asp:BoundField DataField="PrId" HeaderText="PR Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="SeqId" HeaderText="Seq ID"/>
            <asp:BoundField DataField="Meterial" HeaderText="Material" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
        </Columns>
    </asp:GridView>
    </div>               
    </div> 
     <div>
          <label id="Label1"  style="margin:3px; color:maroon; text-align:center;"></label>
     </div>
     </div>	
	 </div>
	 </div>
	 </div>
	 </div>
     </div>

<div id="myModal3" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1;color:white; "><span aria-hidden="true" style="opacity: 1; ">×</span></button>		
						<h4 class="modal-title">Settings</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvSettingsTableModal" runat="server" CssClass="table table-responsive TestTable" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="StartDate"  HeaderText="Start Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
            <asp:BoundField DataField="EndDate" HeaderText="End Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
            <asp:BoundField DataField="BidTermsAndConditions" HeaderText="Terms/Conditions"/>
            <asp:BoundField DataField="BidOpeningPeriod" HeaderText="Bid Opening Period" />
            <asp:BoundField DataField="CanOverride" HeaderText="Can Override"/>
            <asp:BoundField DataField="BidOnlyRegisteredSupplier" HeaderText="Bid Only Registered Supplier"/>
            <asp:BoundField DataField="ViewBidsOnlineUponPrCreation" HeaderText="View Bids Online Upon PR Creation" />
        </Columns>
    </asp:GridView>
    </div>               
    </div> 
     <div>
          <label id="Label2"  style="margin:3px; color:maroon; text-align:center;"></label>
     </div>
     </div>	
	 </div>
	 </div>
	 </div>
	 </div>
     </div>

<div id="myModalUploadedPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Uploaded Item Photos</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Images Found" style="border-collapse:collapse;color:  black;"
        AutoGenerateColumns="false" >
        <Columns>
              <asp:TemplateField HeaderText="Select Default Image" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate >
                   <asp:CheckBox runat="server"  ID="chkDefaultStandardImage" Checked='<%#Eval("isDefaultStandardImage").ToString() == "1" ? true :false %>' OnClick="return GetSelectedRowForSetDefaultStandardImage(this);"/>
               </ItemTemplate>
           </asp:TemplateField>
              <asp:BoundField DataField="PrId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
              <asp:BoundField DataField="ItemId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
              <asp:BoundField DataField="filepath"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
              <asp:BoundField  DataField="FileName" HeaderText="Image Name"/>
           <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate >
                   <asp:Image runat="server" ID="imgUploadImage"  ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
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

        <div id="myModalViewReplacemetPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Uploaded Item Photos</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvViewReplacementImages" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Images Found" style="border-collapse:collapse;color:  black;"
        AutoGenerateColumns="false" >
        <Columns>
              <asp:BoundField DataField="PrId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
              <asp:BoundField DataField="ItemId"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
              <asp:BoundField DataField="filepath"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
              <asp:BoundField  DataField="FileName" HeaderText="Image Name"/>
           <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate >
                   <asp:Image runat="server" ID="imgUploadImage"  ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
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
                        <h4 class="modal-title">Replacement Item Photos</h4>
                    </div>
                    <div class="modal-body">
                        <div class="login-w3ls">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <label style="color: black;">Add More Replacement Image(JPG,JPEG,PNG,GIF)</label><label id="errorReplace" style="color: red"></label>
                                        <div class="input-group input-group-sm">
                                            <input type="file" style="display: inline" class="form-control" id="fileReplace" onchange="readURL(this);" name="fileReplace[]" accept="image/*" data-type='image' multiple />
                                            <span class="input-group-btn">
                                                <asp:Button Style="display: inline" ID="btnReplacementImageUpload" OnClientClick="return ReplaceImagesValiation();" OnClick="btnReplacementImageUpload_Click" runat="server" CssClass="btn btn-primary" Text="Upload"></asp:Button>
                                            </span>
                                        </div>
                                        <br />

                                        <asp:GridView ID="gvReplacementPhotos" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Images Found" Style="border-collapse: collapse; color: black;"
                                            AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select Default Image" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" CssClass="getVal" ID="chkDefaultReplacementImage" Checked='<%#Eval("isDefaultReplaceImage").ToString() == "1" ? true :false %>' OnClick="return GetSelectedRowForSetDefaultReplacementImage(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="filepath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                                                <asp:TemplateField HeaderText="Image" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Image runat="server" ID="imgUploadImage" ImageUrl='<%# Eval("FilePath") %>' Height="80px" Width="100px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lbtnDeleteReplacementImage" Text="Delete" OnClientClick="return GetSelectedRowForDeleteReplacementImage(this);"></asp:LinkButton>
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





        <div id="myModalSupportiveDocuments" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Uploaded Supportive Documents</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvSupportiveDocuments" runat="server" CssClass="table table-responsive TestTable" style="border-collapse:collapse;color:  black;"
        AutoGenerateColumns="false"  EmptyDataText="No Documents Found">
        <Columns>
              <asp:BoundField  DataField="FileName" HeaderText="File Name"/>
              <asp:BoundField  DataField="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
          
            <asp:TemplateField HeaderText="Preview">
                 <ItemTemplate>
                     <asp:LinkButton runat="server" ID="lbtnViewUploadSupporiveDocument" OnClick="lbtnViewUploadSupporiveDocument_Click" >View</asp:LinkButton>
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
        <%--  --%>

    <section class="content-header">
      <h1>
       Submit for Bid Listing
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Submit for Bid Listing</li>
      </ol>
    </section> 
    <br />
         <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Attachments</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive TestTable" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PrId" HeaderText="PR Id" />
            <asp:BoundField DataField="FilePath" HeaderText="File Path"/>
            <asp:BoundField DataField="FileName" HeaderText="File Name" />
         
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

           

    <section class="content" style="background: #fff;border: 1px solid #f4f4f4;">    <!-- Main content -->
          <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
                            <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
                            <strong>
                                <asp:Label ID="lbMessage" ForeColor="White"  runat="server"></asp:Label>
                            </strong>
                        </div>
       <!-- title row -->
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header">
            <i class="fa fa-copy"></i> PURCHASE REQUISITION (PR)
            <small class="pull-right">Date:<asp:Label ID="lblDateNow" runat="server" Text=""></asp:Label></small>
          </h2>
        </div>
        <!-- /.col -->
      </div>
      <!-- info row -->
     <div class="row">
        <div class="col-md-4 " style=" margin-left: 24px; ">
          <address>
            <strong>Department</strong><br />
            User Department : &nbsp;&nbsp;&nbsp;<asp:Label ID="lblDeptName" runat="server" Text=""></asp:Label><br />
            Our Ref.: &nbsp;&nbsp;&nbsp; <asp:Label ID="lblRef" runat="server" Text=""></asp:Label><br />
            PR. No : &nbsp;&nbsp;&nbsp;<asp:Label ID="lblPRCode" runat="server" Text=""></asp:Label><br />
            Date : &nbsp;&nbsp;&nbsp;<asp:Label ID="lblRequestedDate" runat="server" Text=""></asp:Label><br />
          </address>
        </div>
        <!-- /.col -->
        <div class="col-md-4">
          <address>
            <strong>Requester</strong><br />
           Name: <asp:Label ID="lblRequesterName" runat="server" Text=""></asp:Label><br />
          </address>
        </div>
        <!-- /.col -->
        <div class="col-md-4" style="visibility:hidden">
          <b>Requester </b><br />
          <br />
          <b>Requester Name:</b><br />
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
   <div class="panel-body">
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
             <asp:BoundField DataField="ItemQuantity"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
             <asp:TemplateField HeaderText="Quantity">
              <ItemTemplate>
                  <asp:TextBox ID="txtItemQuantity" Width="50" type="number" runat="server" Text='<%# Eval("ItemQuantity") %>'></asp:TextBox>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Replacement" HeaderText="Replacement" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:TemplateField HeaderText="Replacement">
              <ItemTemplate>
                  <asp:Label ID="Label1" runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Replacement Images">
                  <ItemTemplate>
                      <asp:LinkButton ID="btnViewzReplacementPhotos" OnClick="btnViewzReplacementPhotos_Click" OnClientClick="return GetSelectedRow(this)" runat="server" Text="View"/>
                  </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Standard Images">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnViewUploadPhotos" OnClick="btnViewUploadPhotos_Click" runat="server" Text="View"/>
                                        </ItemTemplate>
                                    </asp:TemplateField>

             <asp:TemplateField HeaderText="Item Specifications">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblViewBom" Text="View" OnClick="btnBOM_Click"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Supportive Documents">
                   <ItemTemplate>
                       <asp:LinkButton ID="btnViewSupportiveDocuments" OnClick="btnViewSupportiveDocuments_Click" runat="server" Text="View"/>
                   </ItemTemplate>
              </asp:TemplateField>

            <asp:TemplateField HeaderText="Display Image From">
              <ItemTemplate>
                  <asp:RadioButton  GroupName="imageD" runat="server" Text="Dafault" Checked="true" ID="rdoDefaultImage" OnCheckedChanged="rdoDefaultImage_CheckedChanged" AutoPostBack="true"/><br />
                  <asp:RadioButton  GroupName="imageD" runat="server"  ID="rdoStandardImage" Text="Standard" Enabled='<%# Eval("noOfStanardImages").ToString() =="0" ?false:true %>' OnCheckedChanged="rdoStandardImage_CheckedChanged" AutoPostBack="true"/><br />
                  <asp:RadioButton  GroupName="imageD" runat="server" Text="Replacement"  ID="rdoReplacementImage" Enabled='<%# Eval("noOfRepacementImages").ToString() =="0" ?false:true %>' OnCheckedChanged="rdoReplacementImage_CheckedChanged" AutoPostBack="true"/>
               </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Bidding Type">
              <ItemTemplate>
                  <asp:RadioButton  GroupName="bidTypeR" runat="server" Text="Online" Checked="true" ID="rdoBid" /><br />
                  <asp:RadioButton  GroupName="bidTypeR" runat="server" Text="Manual" ID="rdoManual"  />
               </ItemTemplate>
            </asp:TemplateField>

             
          <%--   <asp:TemplateField HeaderText="Default Image From">
              <ItemTemplate>
              
              </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Default Image From">
              <ItemTemplate>
                 
              </ItemTemplate>
            </asp:TemplateField>--%>


        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>

   <div runat="server" id="divResubmissionForbid">
   <div class="row">
        <div class="col-xs-12">
          <h5 class="page-header">
            <asp:Label ID="lblReSubmitForBid" runat="server" Text="Rejected Items Re-submission"></asp:Label>
          </h5>
        </div>
        <!-- /.col -->
      </div>
   <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvRejectedBidsSubmitAgain" runat="server" CssClass="table table-responsive RejectTable"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="CheckBox3" runat="server" />
                </ItemTemplate>
                <HeaderTemplate>
                    <asp:CheckBox ID="CheckBox4" runat="server" onclick="CheckUncheckCheckboxesRejectBids(this);"/>
                </HeaderTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />
            <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
            <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
            <asp:BoundField DataField="Replacement" HeaderText="Replacement" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="BiddingOrderId" HeaderText="BiddingOrderId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:TemplateField HeaderText="Replacement">
              <ItemTemplate>
                  <asp:Label ID="Label1" runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="Replacement Images">
                  <ItemTemplate>
                      <asp:LinkButton ID="btnVieRejectReplacementPhotos" OnClick="btnVieRejectReplacementPhotos_Click" OnClientClick="return GetSelectedRow(this)" runat="server" Text="View"/>
                  </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Standard Images">
                  <ItemTemplate>
                     <asp:LinkButton ID="btnViewRejectUploadPhotos" OnClick="btnViewRejectUploadPhotos_Click" runat="server" Text="View"/>
                 </ItemTemplate>
              </asp:TemplateField>

             <asp:TemplateField HeaderText="Item Specifications">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblViewBom" Text="View" OnClick="btnBOMRjt_Click"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Settings">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblSettings" Text="View" OnClick="lblSettingsRjt_Click"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Display Image From">
              <ItemTemplate>
                  <asp:RadioButton  GroupName="imageD" runat="server" Text="Dafault" Checked="true" ID="rdoRejectedDefaultImage" OnCheckedChanged="rdoRejectedDefaultImage_CheckedChanged" AutoPostBack="true"/><br />
                  <asp:RadioButton  GroupName="imageD" runat="server"  ID="rdoRejectedStandardImage" Text="Standard" Enabled='<%# Eval("noOfStanardImages").ToString() =="0" ?false:true %>' OnCheckedChanged="rdoRejectedStandardImage_CheckedChanged" AutoPostBack="true"/><br />
                  <asp:RadioButton  GroupName="imageD" runat="server" Text="Replacement"  ID="rdoRejectedReplacementImage" Enabled='<%# Eval("noOfRepacementImages").ToString() =="0" ?false:true %>' OnCheckedChanged="rdoRejectedReplacementImage_CheckedChanged" AutoPostBack="true"/>
               </ItemTemplate>
            </asp:TemplateField>

            <asp:BoundField DataField="PrIsRejectedCount" HeaderText="Rejected Times"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:TemplateField HeaderText="Rejected Times" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="Red">
                <ItemTemplate>
                    <asp:Label ID="txtApproved" Text='<%#Eval("PrIsRejectedCount") %>' ForeColor='<%#Eval("PrIsRejectedCount").ToString() !=""?System.Drawing.Color.Red : System.Drawing.Color.Red%>' runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit Settings">
                <ItemTemplate>
                    <asp:ImageButton ID="btnEdit" ImageUrl="~/images/document.png" OnClick="lnkBtnEditRjt_Click" OnClientClick="return ScrollToEdit();" style="width:26px;height:20px"
                        runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Bidding Type">
              <ItemTemplate>
                  <asp:RadioButton  GroupName="bidTypeRjt" runat="server" Text="Online Bid" Checked="true" ID="rdoBidRjt" /><br />
                  <asp:RadioButton  GroupName="bidTypeRjt" runat="server" Text="Manual Bid" ID="rdoManualRjt"  />
               </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    </div>
    </div>
   </div>
      <div class="box box-info" runat="server" id="panelBidTermCondition">
        <div class="box-header with-border">
          <h3 class="box-title" >Bid Terms / Condition</h3>

          <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
          </div>
        </div>
     

        <!-- /.box-header -->
        <div class="box-body">
           <div class="row">
                <div class="col-md-12">
                <div class="form-group">
                <label for="exampleInputEmail1">Bid Term / Condition</label>
                   
                <asp:TextBox ID="txtCondition" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="Bid Term / Condition"></asp:TextBox>
                </div>
                </div>

                           
           </div>
           <div class="row">
           <div class="col-md-12">
           <div class="table-responsive">
              <table id="example2" class="table table-bordered table-hover" style="border: none;font-size:15px;color:Black">
                <tbody>
                <tr>
                  <td>Bid Starting Day</td>
     
                  <td style="text-align:center;">
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtStartDate" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                   <asp:TextBox ID="txtStartDate" runat="server" Width="50%" CssClass="form-control" type="date" ></asp:TextBox>
                    <%--<div class="controls input-append date form_datetime" data-date="" data-date-format="dd MM yyyy - HH:ii p" data-link-field="dtp_input1">
                    <input size="16" type="text" id="satrtDate" value="" placeholder="Start Date" style="width:180px; border:none;" autocomplete="off">
                    <span class="add-on"><i class="icon-remove" class="form-control"></i></span>
					<span class="add-on"><i class="icon-th"></i></span>
                   </div>--%>
                  </td>
                  <td style="text-align:center;">
                    <asp:Label ID="lblFromTime" runat="server" Text=""></asp:Label>
                    <%--<div class="controls input-append date end_datetime"  data-date="" data-date-format="dd MM yyyy - HH:ii p" data-link-field="dtp_input1">
                    <input size="16" type="text" id="endDate" value="" placeholder="End Date" style="width:180px; border:none;" autocomplete="off">
                    <span class="add-on"><i class="icon-remove"></i></span>
					<span class="add-on"><i class="icon-th"></i></span>
                   </div>--%>
                  </td>
                </tr>

                <tr>
                  <td>Default Bid opening period(days)</td>
     
                  <td>
                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ForeColor="Red" Font-Bold="true"  ControlToValidate="txtNoOfDay" ValidationGroup="btnSave">*</asp:RequiredFieldValidator>
                  <asp:TextBox ID="txtNoOfDay" class="form-control"  style="width:70px;text-align:center;" runat="server"  ></asp:TextBox>
                  </td>
                  <td></td>
                </tr>
                <tr style="display:none;visibility:hidden;">
           
                  <td>Can override</td>
     
                  <td>
                  <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="chkBidOpenYes" runat="server" Text="Yes"  GroupName="chkBid"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                  <td>
                       <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="chkBidOpenNo" runat="server" Text="No"  GroupName="chkBid"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                </tr>
            <tr>
                  <td>Bid only submitted by registered supplier</td>
          
                    <td>
                  <div class="checkbox">
                     <label>
                         <asp:RadioButton ID="chkRegSupYes" runat="server" Text="Yes"  GroupName="chkReg"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                  <td>
                       <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="chkRegSupNo" runat="server" Text="No"  GroupName="chkReg"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                </tr>
                <tr  style="visibility:hidden;">
                  <td>View bids online upon PR creation</td>

                    <td>
                  <div class="checkbox">
                     <label>
                        <asp:RadioButton ID="chkViewBidsYes" runat="server" Text="Yes"  GroupName="chkViewBids"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                  <td>
                       <div class="checkbox">
                     <label>
                       <asp:RadioButton ID="chkViewBidsNo" runat="server" Text="No"  GroupName="chkViewBids"></asp:RadioButton>
                     </label>
                  </div>
                  </td>
                </tr>

                </tbody>
              </table>
           </div>
           </div>
           </div>
        </div>
                <div class="box-footer">
            <span class="pull-right">


               <%--  <input type="file" class="form-control" id="fileReplace" name="fileReplace[]"  accept="image/*" data-type='image' multiple/>
             --%>
                 <asp:Button ID="btnSave" runat="server" Text="Submit for Bid Listing"  
                        ValidationGroup="btnSave" CssClass="btn btn-primary" OnClick="btnSave_Click" OnClientClick = "return validataAlFields();"></asp:Button>

                <%-- <asp:Button ID="btnReject" runat="server" Text="Reject" OnClick="confirmationToReject_Click"
                class="btn btn-danger"  ></asp:Button>--%>
               
                 <asp:Button ID="btnClear"  runat="server" Text="Cancel"  CssClass="btn btn-warning pull-right"  style="margin-left:2px"
                        onclick="btnClear_Click"></asp:Button>
                       
            </span>
                 </div>    
   <br />

   <div class="box box-info">
   <div class="box-header with-border">
   <h3 class="box-title" >Already Submitted For Bid Listing</h3>
     <div class="box-tools pull-right">
       <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
       <button type="button" class="btn btn-box-tool" data-widget="remove"><i class="fa fa-remove"></i></button>
     </div>
   </div>
    <div class="row">
    <div class="panel-body">
    
    <div class="co-md-12" >
     <asp:Label ID="lblMsgSubmitedBids" runat="server" Text="No records found." style="color:Blue;font-weight:bold;"></asp:Label>
    <div class="table-responsive">
        <asp:GridView ID="gvSubmittedBids" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="BiddingOrderId" HeaderText="Bidding Order Id" />
            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />
            <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
            <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
            <asp:BoundField DataField="Replacement" HeaderText="Replacement" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:TemplateField HeaderText="Replacement">
              <ItemTemplate>
                  <asp:Label ID="lblReplacement" runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Replacement Images">
                  <ItemTemplate>
                      <asp:LinkButton ID="btnViewzReplacementPhotosPending" OnClick="btnViewzReplacementPhotosPending_Click" runat="server" Text="View"/>
                  </ItemTemplate>
              </asp:TemplateField>

              <asp:TemplateField HeaderText="Attachments">
                  <ItemTemplate>
                      <asp:LinkButton ID="btnViewUploadPhotosPending" OnClick="btnViewUploadPhotosPending_Click" runat="server" Text="View"/>
                  </ItemTemplate>
              </asp:TemplateField>
         <%--  <asp:TemplateField HeaderText="Attachments">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lbtnView" Text="View Attachments" OnClick="btnViewSub_Click"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="Item Specifications">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblViewBom" Text="View" OnClick="btnBOMSub_Click"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Settings">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblSettings" Text="View" OnClick="lblSettings_Click"></asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField ItemStyle-Width="200px" DataField="BidTypeMaualOrBid" HeaderText="Bid Type Manual/Online Bid" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
             <asp:TemplateField HeaderText="Bid Type Manual/Online Bid">
              <ItemTemplate>
                  <asp:Label ID="lblBidTypemanuaBid" runat="server" Text='<%# Eval("BidTypeMaualOrBid").ToString() =="1" ? "Supplier Online Bid":"Supplier Manual Bid" %>' Font-Bold="true" ForeColor='<%#Eval("BidTypeMaualOrBid").ToString()== "1"?System.Drawing.Color.Green:System.Drawing.Color.Red %>'></asp:Label>
              </ItemTemplate>
             </asp:TemplateField>
            <asp:TemplateField HeaderText="Bid Status">
                <ItemTemplate>
                    <asp:Label ID="txtApproved" Text='<%#Eval("IsApproveToViewInSupplierPortal").ToString()=="1"?"Bid Opened": Eval("IsApproveToViewInSupplierPortal").ToString()=="2"?"Bid Rejected" :"Pending Approval" %>' ForeColor='<%#Eval("IsApproveToViewInSupplierPortal").ToString()=="1"?System.Drawing.Color.Green:Eval("IsApproveToViewInSupplierPortal").ToString()=="2"?System.Drawing.Color.Red:System.Drawing.Color.Orange %>' runat="server"></asp:Label>
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
    
    </section>

         
          </ContentTemplate>
         </asp:UpdatePanel>
         <asp:HiddenField ID="hdnStartDate" runat="server"></asp:HiddenField>
         <asp:HiddenField ID="hdnEndDate" runat="server"></asp:HiddenField>
          <asp:HiddenField runat="server" ID="hdnItemIdForReplacementImage"/>
          <asp:HiddenField runat="server" ID="hdnItemIdForRejectedReplacementImage"/>
   
    </form>

       
    </body>

       
       <script language = "javascript" type = "text/javascript">

           function ScrollToEdit()
           {
               document.getElementById('ContentSection_panelBidTermCondition').scrollIntoView();
           }

           function CheckUncheckCheckboxes(CheckBox) {
               debugger;
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
   <script language = "javascript" type = "text/javascript">
       function CheckUncheckCheckboxesRejectBids(CheckBox) {
           //get target base & child control.
           var TargetBaseControl = document.getElementById('<%= this.gvRejectedBidsSubmitAgain.ClientID %>');
           var TargetChildControl = "CheckBox3";

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
       <script type="text/javascript">
 
           $("#btnApproveNo").on('click').click(function () {
            var $confirm = $("#modalApprove");
            $confirm.modal('hide');
            return this.false;
        });

           $("#btnRejectNo").on('click').click(function () {
            var $confirm = $("#modalReject");
            $confirm.modal('hide');
            return this.false;
        });
    </script>
<%--   <link href="AppResources/Datetime/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
   <script src="AppResources/Datetime/bootstrap-datetimepicker.js" type="text/javascript"></script>
   <link href="AppResources/Datetime/bootstrap-datetimepicker.min.css" rel="stylesheet"type="text/css" />
   <script src="AppResources/Datetime/bootstrap-datetimepicker.min.js" type="text/javascript"></script>--%>

    </html>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/themes/base/jquery-ui.css" type="text/css" media="all">
   <%-- <script src="AppResources/Datetime/bootstrap/js/bootstrap-datetimepicker.sl.js" type="text/javascript"></script>--%>

    <script src="AdminResources/js/datetimepicker/datetimepicker.min.js"></script>
    <script src="AdminResources/js/datetimepicker/datetimepicker.js"></script>
    <link href="AdminResources/css/datetimepicker/datetimepicker.base.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.css" rel="stylesheet" />
    <link href="AdminResources/css/datetimepicker/datetimepicker.themes.css" rel="stylesheet" />

     <script type="text/javascript">

       
         var dtp01 = new DateTimePicker('.date1', { pickerClass: 'datetimepicker-blue', timePicker: true, timePickerFormat: 12, format: 'Y/m/d h:i', allowEmpty: false ,minDate :new Date().setDate(new Date().getDate() - 1) });
         function TimeChange() {
             $('#ContentSection_hdnReceivedDate').val($('.date1').val());
             alert($('#ContentSection_hdnReceivedDate').val());
         }
    </script>

    <script type="text/javascript">
        $(document).ready(function () {

            if ($('#ContentSection_chkBidOpenYes').is(':checked')) {

              //  alert("it's checked");
            }

            if ($('#ContentSection_chkBidOpenNo').is(':checked')) {

            }
        });
    </script>
<%--<script type="text/javascript">
    $('.form_datetime').datetimepicker({
        //language:  'fr',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        showMeridian: 1
    });
    $('.end_datetime').datetimepicker({
        //language:  'fr',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        showMeridian: 1
    });
   
</script>--%>

    <script>
function validataAlFields() {
    var isValid = false;
    var count = 0;
    var countRejected = 0;
    var gridView = document.getElementById('<%= gvPRView.ClientID %>');
    if(gridView!= null)
    {
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
    }


    var gridViewRejected = document.getElementById('<%= gvRejectedBidsSubmitAgain.ClientID %>');
    if(gridViewRejected!= null)
    {
        for (var i = 1; i < gridViewRejected.rows.length; i++) {
            var inputs = gridViewRejected.rows[i].getElementsByTagName('input');
      
            if (inputs != null && inputs[0] != null) {
                if (inputs[0].type == "checkbox") {
                    if (inputs[0].checked) {
                        countRejected++;
                    
                    }
                }
            }
        }
    }

   
  

    if (count > 0 || countRejected > 0) {
        $('#ContentSection_hdnStartDate').val($('#satrtDate').val());
        $('#ContentSection_hdnEndDate').val($('#endDate').val());
        return true;
    }

    else {
       $('#modalSelectCheckBox').modal('show');
        return false;
    }
}

        $("#btnOkAlert").click(function () {
            $('#modalSelectCheckBox').modal('hide');
        });

        $("#btnOk").click(function () {
            $('#SuccessAlert').modal('hide');
        });

    </script>

     <script>
        function GetSelectedRow(lnk) {
            //Reference the GridView Row.
            var row = lnk.parentNode.parentNode;
            var itemid = row.cells[1].innerHTML;
            var prId=<%=PRId%>;
            $("#ContentSection_hdnItemIdForReplacementImage").val(itemid);
            return true;
        }
    </script>

     <script>
        function GetSelectedRowRejected(lnk) {
            //Reference the GridView Row.
            var row = lnk.parentNode.parentNode;
            var itemid = row.cells[1].innerHTML;
            var prId=<%=PRId%>;
            $("#ContentSection_hdnItemIdForRejectedReplacementImage").val(itemid);
            return true;
        }
    </script>


    

      <script>
        function GetSelectedRowForDeleteReplacementImage(lnk) {
            //Reference the GridView Row.
            var row = lnk.parentNode.parentNode;
            
            var prId = row.cells[1].innerHTML;
            var itemid = row.cells[2].innerHTML;
            var fileName = row.cells[3].innerHTML;

            var postJsonData =JSON.stringify({
                "prId":prId,
                "itemId" : itemid,
                "fileName" : fileName
            });

            $.ajax({
                type:"POST",
                url:"CompanySupplierDepartmentPRSubmitForBid.aspx/deleteReplacementImage",
                data:postJsonData,
                contentType : "application/json; charset=utf-8",
                success:function (result)
                {
                    response = result.d;
                    if(response == "1")
                    {
                        location.reload();
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                },

            });

            event.preventDefault();  
            return false;
        }

    </script>
      <script>
 function GetSelectedRowForSetDefaultReplacementImage(lnk) {
     //Reference the GridView Row.

 
            var chkcheckValue;
            var row = lnk.parentNode.parentNode.parentNode;
            var prId = row.cells[1].innerHTML;
            var itemid = row.cells[2].innerHTML;
            var fileName = row.cells[3].innerHTML;
            var chkcheck=row.children[0].children[0].children[0].checked;
            if(chkcheck == true)
                chkcheckValue=1;
            else
                chkcheckValue=0;



            var postJsonData =JSON.stringify({
                "prId":prId,
                "itemId" : itemid,
                "fileName" : fileName,
                "check" :  chkcheckValue
            });

            $.ajax({
                type:"POST",
                url:"CompanySupplierDepartmentPRSubmitForBid.aspx/updateDefaultReplacementImage",
                data:postJsonData,
                contentType : "application/json; charset=utf-8",
                success:function (result)
                {
                    response = result.d;
                    if(response == "1")
                    {
                        location.reload();
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                },

            });

            event.preventDefault();  
            return false;
        }
    </script>



      <script>
 function GetSelectedRowForSetDefaultStandardImage(lnk) {
            //Reference the GridView Row.
     var chkcheckValue;
     var row = lnk.parentNode.parentNode;
     var prId = row.cells[1].innerHTML;
     var itemid = row.cells[2].innerHTML;
     var fileName = row.cells[3].innerHTML;
     var chkcheck=row.children[0].children[0].checked;
     if(chkcheck == true)
         chkcheckValue=1;
     else
         chkcheckValue=0;
            
            var postJsonData =JSON.stringify({
                "prId":prId,
                "itemId" : itemid,
                "fileName" : fileName,
                "check" :  chkcheckValue
            });

            $.ajax({
                type:"POST",
                url:"CompanySupplierDepartmentPRSubmitForBid.aspx/updateDefaultStandardImage",
                data:postJsonData,
                contentType : "application/json; charset=utf-8",
                success:function (result)
                {
                    response = result.d;
                    if(response == "1")
                    {
                        location.reload();
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                },

            });

            event.preventDefault();  
            return false;
        }
    </script>

    <script>
        function ReplaceImagesValiation()
        {
            var _validFileExtensions = ["jpg", "jpeg", "bmp", "gif", "png"]; 
            var replacementfiles = document.getElementById('fileReplace').files;

            if(replacementfiles.length > 0 && replacementfiles.length < 6)
            {

               
                var validFormat=0;
                for (var i = 0; i < replacementfiles.length; i++) {
                    var  count=0;
                    for (var j = 0; j < _validFileExtensions.length; j++) 
                    {
                        var sCurExtension = _validFileExtensions[j];
                        if ((replacementfiles[i].name.split('.')[replacementfiles[i].name.split('.').length-1]).toLowerCase() == sCurExtension.toLowerCase()) 
                        {
                            count++;
                        }
                    }
                    if(count == 0)
                    {
                        $('#errorReplace').text('Invalid format'); 
                        return false;
                    }
                    else
                    {
                        validFormat++;
                    }

                }
                if( replacementfiles.length == validFormat)
                {
                    return true;
                }
                else
                {
                    $('#errorReplace').text('invalid format'); 
                    return false;
                }
               
            }
            else
            { 
                if(replacementfiles.length == 0)
                    $('#errorReplace').text('Atleast select one file');
                else
                   $('#errorReplace').text('Maximum file count is 5');
                return false;
            }
        
        }
    </script>


    <script>
        function readURL(input) {
            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
            if (input.files.length != 0) {
                for (var i = 0; i < input.files.length; i++) {
                   
                    var filePath = input.files[i].name;
                    if(!allowedExtensions.exec(filePath))
                    {
                        $('#errorReplace').text('invalid format');
                    }
                    else
                    {
                  
                    }
                }
               
            }
        }
    </script>
</asp:Content>
