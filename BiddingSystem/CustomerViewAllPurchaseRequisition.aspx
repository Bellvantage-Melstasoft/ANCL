<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CustomerViewAllPurchaseRequisition.aspx.cs" Inherits="BiddingSystem.CustomerViewAllPurchaseRequisition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">
  <html>
  <head>
  <link href="AdminResources/css/daterangepicker.css" rel="stylesheet" type="text/css" />
  <link rel="stylesheet" href="../../bower_components/bootstrap-daterangepicker/daterangepicker.css">
  <link href="AdminResources/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
  <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
   
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
    <style type="text/css">
     #myModal .modal-dialog {
       width: 90%;
     }
     #myModal2 .modal-dialog {
       width: 50%;
     }
</style>
    <style type="text/css">
   #myTable {
       font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
       border-collapse: collapse;
       width: 100%;
   }
   
   #myTable td, #customers th {
       border: 1px solid #ddd;
       padding: 8px;
   }
   
   #myTable tr:nth-child(even){background-color: #f2f2f2;}
   
   #myTable tr:hover {background-color: #ddd;}
   
   #myTable th {
       padding-top: 12px;
       padding-bottom: 12px;
       text-align: left;
       background-color: #4CAF50;
       color: white;
   }
</style>
    <style type="text/css">
     #TestTable {
         font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
         border-collapse: collapse;
         width: 100%;
     }
     
     #TestTable td, #TestTable th {
         border: 1px solid #ddd;
         padding: 8px;
     }
     
     #TestTable tr:nth-child(even){background-color: #f2f2f2;}
     
     #TestTable tr:hover {background-color: #ddd;}
     
     #TestTable th {
         padding-top: 12px;
         padding-bottom: 12px;
         text-align: left;
         background-color: #4CAF50;
         color: white;
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
 </head>
<body>
<script src="AdminResources/js/jquery.min.js" type="text/javascript"></script>
<section class="content-header">
    <h1>
       Purchase Requisition
        <small></small>
      </h1>
    <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li>
        <li class="active">View Purchase Requisition</li>
      </ol>
</section>
    <br />
     <form runat="server" id="form1">

     <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1; "><span aria-hidden="true" style="opacity: 1;color: white; ">×</span></button>		
						<h4 class="modal-title">Attachment</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive TestTable" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="PrId" HeaderText="PR Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="FilePath" HeaderText="File Path" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
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

     <div id="myModal2" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="opacity: 1;color:white; "><span aria-hidden="true" style="opacity: 1; ">×</span></button>		
						<h4 class="modal-title">BOM (Bill of Material)</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvBOMDate" runat="server" CssClass="table table-responsive TestTable" style="border-collapse:collapse;color:  black;"
        GridLines="None" AutoGenerateColumns="false">
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

     <div id="myModalUploadedPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Uploaded Item Images</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvUploadedPhotos" runat="server" CssClass="table table-responsive TestTable" style="border-collapse:collapse;color:  black;"
        AutoGenerateColumns="false"  EmptyDataText="No Standard Images Found">
        <Columns>
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

     <div id="myModalReplacementPhotos" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Replacement Item Images</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="table-responsive">
       <asp:GridView ID="gvReplacementPhotos" runat="server" CssClass="table table-responsive TestTable" EmptyDataText="No Replacement Images Found" style="border-collapse:collapse;color:  black;"
        AutoGenerateColumns="false" >
        <Columns>
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

     <div id="SuccessAlert" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header"  style="background-color:#3c8dbc;">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4  class="modal-title" style="color:White;font-weight:bold;">Success</h4>
            </div>
            <div class="modal-body">
                <p style="font-weight:bold; font-size:medium;">PR has been Approved Successfully</p>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnOk" runat="server"  OnClick="btnOK_Click" CssClass="btn btn-info" Text="OK" ></asp:Button>
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
                 <asp:Button ID="btnYesConfirmYesNo" runat="server"  CssClass="btn btn-primary" OnClick="btnApprove_Click" Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo1"  type="button" class="btn btn-danger" >No</button>
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
                 <asp:Button ID="Button1" runat="server"  CssClass="btn btn-primary" OnClick="btnReject_Click"  Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo2"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
</div>

   <section class="content" style="position: relative;background: #fff;border: 1px solid #f4f4f4;">    
   <!-- Main content -->
      <!-- title row -->
      <div class="row">
        <div class="col-xs-12">
          <h2 class="page-header">
            <i class="fa fa-copy"></i> PURCHASE REQUISITION (PR)
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
   <div class="panel-body">
    <div class="co-md-12">
    <div class="table-responsive">
        <asp:GridView ID="gvPRView" runat="server" CssClass="table table-responsive tablegv"
        GridLines="None" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="ItemId" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
            <asp:BoundField DataField="ItemDescription" HeaderText="Item Description" />
            <asp:BoundField DataField="Purpose" HeaderText="Purpose" />
            <asp:BoundField DataField="ItemQuantity" HeaderText="Quantity" />
             <asp:TemplateField HeaderText="Replacement">
              <ItemTemplate>
                  <asp:Label runat="server" Text='<%# Eval("Replacement").ToString() =="1" ? "Yes":"No" %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Replacement Images">
                  <ItemTemplate>
                      <asp:LinkButton ID="btnViewzReplacementPhotos" OnClick="btnViewzReplacementPhotos_Click" runat="server" Text="View"/>
                  </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Standard Images">
                   <ItemTemplate>
                       <asp:LinkButton ID="btnViewUploadPhotos" OnClick="btnViewUploadPhotos_Click" runat="server" Text="View"/>
                   </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField HeaderText="Supportive Documents">
                   <ItemTemplate>
                       <asp:LinkButton ID="btnViewSupportiveDocuments" OnClick="btnViewSupportiveDocuments_Click" runat="server" Text="View"/>
                   </ItemTemplate>
              </asp:TemplateField>
             <asp:TemplateField HeaderText="Item Specifications">
              <ItemTemplate>
                  <asp:LinkButton runat="server" ID="lblViewBom" Text="View" OnClick="btnBOM_Click"></asp:LinkButton>
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
        <asp:Button id="btnEditPRMode"  runat="server" class="btn btn-info pull-right" Visible="false" Text="Edit PR"  OnClick="btnEditPRMode_Click"></asp:Button>
        <asp:Button id="btnRejectPR"  runat="server" class="btn btn-warning pull-right"  Text="Reject PR" onclick="btnRejectPR_Click" style="margin-right: 10px;"></asp:Button>
        <asp:Button id="btnApprove" runat="server"  class="btn btn-primary pull-right"  OnClick="btnApprove_Click" Text="Approve" style="margin-right: 10px;"></asp:Button> 
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
    </form>
   
</body>
    </html>
   <%-- <script language = "javascript" type = "text/javascript">
     function CheckUncheckCheckboxes(CheckBox)
        {
            //get target base & child control.
            var TargetBaseControl = document.getElementById('<%= this.gvPRView.ClientID %>');
            var TargetChildControl = "CheckBox1";

            //get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");  

            for(var n = 0; n < Inputs.length; ++n)
                if(Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl,0) >= 0)
                {
                    Inputs[n].checked = CheckBox.checked;
                }
                else{
                    
                }
        }
   </script> --%>

    <script type="text/javascript">
        $("#btnNoConfirmYesNo1").on('click').click(function () {
            var $confirm = $("#modalApprove");
            $confirm.modal('hide');
            return this.false;
        });

        $("#btnNoConfirmYesNo2").on('click').click(function () {
            var $confirm = $("#modalReject");
            $confirm.modal('hide');
            return this.false;
        });
    </script>
</asp:Content>

        