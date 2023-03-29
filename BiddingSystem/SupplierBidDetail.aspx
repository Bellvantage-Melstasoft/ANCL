<%@ Page Title="" Language="C#" MasterPageFile="~/SupplierLoadingWebUI.Master" AutoEventWireup="true" CodeBehind="SupplierBidDetail.aspx.cs" Inherits="BiddingSystem.SupplierBidDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<head>
        <style type="text/css">
            #customers
            {
                font-family: "Trebuchet MS" , Arial, Helvetica, sans-serif;
                border-collapse: collapse;
                width: 100%;
            }
            
            #customers td, #customers th
            {
                border: 1px solid #ddd;
                padding: 8px;
            }
            
            #customers tr:nth-child(even)
            {
                background-color: #f2f2f2;
            }
            
            #customers tr:hover
            {
                background-color: #ddd;
            }
            
            #customers th
            {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #4CAF50;
                color: white;
            }
        </style>
         <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
             <script src="AppResources/themes/js/jquery.js" type="text/javascript"></script>
    </head>
    <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
       <div class="modal-dialog">
		<!-- Modal content-->
		<div class="modal-content" style="background-color:#a2bdcc;">
		<div class="modal-header" style="background-color:#148690;color:White;">
		<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
		<h4 class="modal-title"><asp:Label ID="lblItemNamePopup" runat="server" Text=""></asp:Label></h4>
	    </div>
	    <div class="modal-body" style=" background-color: white; ">
		<div class="login-w3ls">
        <div class="modal-body">
            <div class="row" style=" margin-left: 10px; ">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvUploadFiles" runat="server" CssClass="table table-responsive tablegv"
                                    Style="border-collapse: collapse; color: black;" GridLines="None" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="DepartmentId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="ItemId" HeaderText="ItemId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                        <asp:BoundField DataField="PrId" HeaderText="PrId" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                                        <asp:TemplateField HeaderText="Image">
                                          <ItemTemplate>
                                              <asp:Image ID="imgPicture" runat="server" ImageUrl='<%# Eval("FilePath") %>' style="width:100px;height:80px"/>
                                          </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="FileName" HeaderText="FileName" />
                                        <asp:BoundField DataField="FilePath" HeaderText="FilePath" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
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
        <div class="modal-footer">
            <button type="button" class="btn btn-danger" data-dismiss="modal">
                Close</button>
        </div>
        </div>
        </div>
    </div>
    </div>
    </div>
    <div id="modalConfirmYesNo" class="modal fade">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" 
                class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <h4 id="lblTitleConfirmYesNo" class="modal-title">Confirmation</h4>
            </div>
            <div class="modal-body">
                <p>Do you want to bid later? Click Ok to proceed.</p>
            </div>
            <div class="modal-footer">
                 <asp:Button ID="btnYesConfirmYesNo" runat="server"  CssClass="btn btn-primary"   onclick="btnLater_Click" Text="Yes" ></asp:Button>
                <button id="btnNoConfirmYesNo"  type="button" class="btn btn-danger" >No</button>
            </div>
        </div>
    </div>
    </div>
    <div class="span9">

         <div class="alert  alert-info  alert-dismissable" id="msg" runat="server" visible="false">
           <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>
           <strong>
               <asp:Label ID="lbMessage" ForeColor="White" runat="server"></asp:Label>
           </strong>
    </div>

        <ul class="breadcrumb">
            <li><a href="SupplierIndex.aspx" style="font-weight: bold;">Home</a> <span class="divider">/</span></li>
            <li class="active"><a href="#">Bid Details</a></li>
        </ul>
        <div class="row">
            <div id="gallery" class="span6">
                <h5>
                    Item : &nbsp;&nbsp;
                    <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label></h5>
                <h5>
                    Description : &nbsp;&nbsp;
                    <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>
                </h5>
            </div>
            <div id="" class="span3">
                <h5>
                    Company : &nbsp;&nbsp;<asp:Label ID="lblCompanyName" runat="server" Text=""></asp:Label>
                </h5>
                <h5 id="demo" style="color: red;">
                </h5>
            </div>
            <div class="span6">
                <form class="form-horizontal qtyFrm">
                <div class="control-group">
                    <br />
                    <div class="controls">
                        <h5 class="span2" style="margin-left: 0px;">
                            BOM (Bill Of Material)</h5>
                        <button onclick="ShowModalPopup()" type="button"  class="btn btn-small btn-primary pull-right" > View Images / Attachments <i class=" icon-copy"></i></button> 
                        <br />
                    </div>
                    <table id="customers" width="100%" align="center" cellpadding="2" cellspacing="2"
                        border="0" bgcolor="#EAEAEA">
                        <tr align="left" style="background-color: #149777; color: Black; font-weight: bold">
                            <td>
                                Seq.No
                            </td>
                            <td>
                                Material
                            </td>
                            <td>
                                Description
                            </td>
                        </tr>
                        <%=getBOMtData()%>
                    </table>
                </div>
                </form>
                <br />
                <hr class="soft clr" />
                <div class="span6" style="margin-left: 0px;">
                    <h5 style="margin-left: 0px;">
                        Bid terms and conditions : &nbsp;&nbsp;
                        <asp:Label ID="lblTermsConditions" runat="server" Text=""></asp:Label></h5>
                </div>
                <br />
                <div class="span6">
                    <button id="btnPaticipate" type="button" class="btn btn-primary" style="padding: 11px;padding-left: 40px;padding-right: 40px;" onclick="return btnPaticipate_onclick()">
                        Participate</></button>
                </div>
                <div class="span6" style=" margin-top: 12px; margin-left: 2px; ">
                    <asp:Label ID="lblSussess" runat="server" Text="" Visible="false" style="color:Green;font-weight:bold;"></asp:Label>
                    <asp:Label ID="lblError" runat="server" Text="" Visible="false" style="color:Red;font-weight:bold;"></asp:Label>
                </div>
            </div>
            <br />
            <br />
            <div class="span3" style=" margin-top: 16px; ">
                <div id="image">
                </div>
            </div>
            <div id="divButtons" class="span6" style="display: none;">
                <asp:Button ID="btnApplyNow" class="btn btn-primary" runat="server" 
                    Text="Apply Now" 
                    style="padding: 11px;padding-left: 40px;padding-right: 40px;margin-left:10px" 
                    onclick="btnApplyNow_Click"/>
                <button type="button" id="applyLater"  data-toggle="modal" data-target="#modalConfirmYesNo"  style="padding: 11px;padding-left: 40px;padding-right: 40px;" class="btn btn-danger">Later</button>
 
                <br />
            </div>
        </div>
    </div>
    <script src="AppResources/themes/js/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
     var Image = <%= getJsonItemImagePath() %>
     LoadImage();
     function LoadImage(){
        var text = "";
        var imagePath = "";
        var BidImagePath =  "";
         imagePath = Image.split('~')
         BidImagePath = imagePath[1];
            var htmlcode =
				  '<div class="thumbnail" style="background-color:#ebf1f7;">' +
				  '<a  href="#"><img src='+BidImagePath+'  alt="" style="height:136px"/></a>' +
				  '</div>' + '' 
        document.getElementById("image").innerHTML = htmlcode;
    }
    </script>
    <script type="text/javascript">
        $("#btnNoConfirmYesNo").on('click').click(function () {
                     var $confirm = $("#modalConfirmYesNo");
                     $confirm.modal('hide');
                     return this.false;
        });
    </script>
    <script type="text/javascript">
    if( getParameterByName('Status') == "P")
    {
       $("#btnPaticipate").hide(); 
       $("#divButtons").show();
       $("#applyLater").hide();
    }

    function getParameterByName( name ){
    var regexS = "[\\?&]"+name+"=([^&#]*)", 
  regex = new RegExp( regexS ),
  results = regex.exec( window.location.search );
  if( results == null ){
    return "";
  } else{
    return decodeURIComponent(results[1].replace(/\+/g, " "));
  }
}

        var EndDate = <%= getJsonEndDateTime() %>;
        var endDate = new Date(EndDate);
        var d = endDate;
        var time01   =  (
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
            document.getElementById("demo").innerHTML = "Expired On : " + days + "d " + hours + "h " + minutes + "m " + seconds + "s ";

            // If the count down is finished, write some text 
            if (distance < 0) {
                clearInterval(x);
                document.getElementById("demo").innerHTML = "EXPIRED";
            }
        }, 1000);

        $("#btnPaticipate").click(function () {
            $("#divButtons").show();
            //alert("Hello");
            $("#btnPaticipate").hide();
        })
    </script>
    <script type="text/javascript">
        function ShowModalPopup() {
            $('#myModal').modal('show');
        }
        function btnPaticipate_onclick() {

        }

    </script>
    <script type="text/javascript">
        //        function GetDownload() {
        //            debugger;
        //            var a = document.createElement('a');
        //            a.href = "/favicon.png";
        //            a.download = "favicon.png";

        //            return false;
        //        }
    </script>
</asp:Content>
