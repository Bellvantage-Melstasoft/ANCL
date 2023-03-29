<%@ Page Title="" Language="C#" MasterPageFile="~/BiddingAdmin.Master" AutoEventWireup="true" CodeBehind="CompanyInitialRequest.aspx.cs" Inherits="BiddingSystem.CompanyInitialRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentSection" runat="server">

   <html>
       <head>
             <script src="AdminResources/js/jquery-1.10.2.min.js"></script>
       </head>
       <body>


    <form runat="server">
<section class="content-header">
      <h1>
       View Registered Requests
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="AdminDashboard.aspx"><i class="fa fa-home"></i> Home</a></li> 
        <li class="active">View Registered Requests</li>
      </ol>
    </section>
    <br />

       

        <div id="myModal" class="modal modal-primary fade" tabindex="-1" role="dialog"  aria-hidden="true">
				  <div class="modal-dialog">
					<!-- Modal content-->
					<div class="modal-content" style="background-color:#a2bdcc;">
					  <div class="modal-header" style="background-color:#7bd47dfa;">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">×</span></button>		
						<h4 class="modal-title">Send E-mail</h4>
					  </div>
					  <div class="modal-body">
						<div class="login-w3ls">
						<div class="row">
                  <div class="col-md-12">
                    <div class="row">
                     
                        
                      <div class="col-md-12">
                        <div class="form-group"> 
                             <label id="lblemail" style="color:black" >Email-Address</label> 
                            <asp:label ID="lblReceiverEmailAddress" class="form-control" runat="server"></asp:label>
                        </div>
                      </div> 

                      <div class="col-md-12">
                        <div class="form-group">
                              <label id="lblSubject" style="color:black" >Subject</label>  <label style="color:red" id="lblSubjectMgs"></label>
                           <asp:TextBox ID="txtMessageSubject" class="form-control" runat="server" name="subject" placeholder="Subject" ></asp:TextBox>
                          <div class="help-block with-errors"></div>
                        </div>
                      </div>  
                        
                         <div class="col-md-12">
                        <div class="form-group">
                             <label id="lblBody" style="color:black">Body</label> <label style="color:red" id="lblBodyMgs"></label>
                              <asp:TextBox ID="txtMessage"  class="form-control" runat="server" placeholder="Message" Rows="6" TextMode="MultiLine" ></asp:TextBox>
                        <%--  <CKEditor:CKEditorControl ID="CKEditor1" BasePath="/ckeditor/" runat="server"></CKEditor:CKEditorControl>
                       --%> </div>
                      </div>  
                                             
                    </div>
                    <asp:Button ID="btnSend" ValidationGroup="btnSubmit" runat="server" Text="Send Message" CssClass="btn btn-primary" OnClientClick="return sendmessage();" ></asp:Button>
                    <asp:Button ID="btnCancelMail" ValidationGroup="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-danger"  ></asp:Button>
                  
                  </div> 
                            <div class="col-md-12">
                                 <label id="lbMailSuccessMessage"  style="margin:3px; color:green; text-align:center;"></label>
                                  <label id="lbMailErrorMessage"  style="margin:3px; color:maroon; text-align:center;"></label>
                            </div>
                </div>
								
						</div>
					  </div>
					</div>
				  </div>
				</div>













    <section class="content">
      
             <div class="row">
        <div class="col-xs-12">
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Registered Requests</h3>

            <%--  <div class="box-tools">
                <div class="input-group input-group-sm" style="width: 150px;">
                  <input type="text" name="table_search" class="form-control pull-right" placeholder="Search">

                  <div class="input-group-btn">
                    <button type="submit" class="btn btn-default"><i class="fa fa-search"></i></button>
                  </div>
                </div>
              </div>--%>
            </div>
            <!-- /.box-header -->
            <div class="box-body table-responsive no-padding">
               <%--  <asp:ScriptManager runat="server"></asp:ScriptManager>

             <asp:UpdatePanel runat="server" ID="udatePanel1" >
                 <ContentTemplate>--%>
              <asp:GridView runat="server" ID="gvApprovalSupplierList" AutoGenerateColumns="false" GridLines="None" CssClass="table table-responsive" EmptyDataText="No Supplier Requests" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvApprovalSupplierList_PageIndexChanging">
                  <Columns>
                      <asp:BoundField  DataField="SupplierId" HeaderText="Supplier ID"/>
                        <asp:BoundField  DataField="_Supplier.SupplierName" HeaderText="Supplier Name"/>
                        <asp:BoundField  DataField="_Supplier.Email" HeaderText="E-mail Address"/>
                        <asp:BoundField  DataField="_Supplier.PhoneNo" HeaderText="Contact No"/>
                        <asp:BoundField  DataField="RequestedDate" HeaderText="Requested Date" DataFormatString="{0:dd/MM/yyyy hh:mm tt}"/>
                       
                      
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button runat="server" Text="Review" CssClass="btn btn-primary" ID="lbtnView" OnClick="lbtnView_Click"></asp:Button>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField>
                          <ItemTemplate>
                              <asp:Button  Text="Get more Info" CssClass="btn btn-default" runat="server" OnClick="lbtnSendMessage_Click"  ID="lbtnSendMessage"></asp:Button>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
                  
              </asp:GridView>
                    <%-- </ContentTemplate>--%>
                 
                 <%--<Triggers>
                     <asp:AsyncPostBackTrigger  ControlID="lbtnSendMessage" EventName="emailSend"/>
                 </Triggers>--%>
                <%-- </asp:UpdatePanel>--%>
              
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
        </div>
      </div>
 
      </section>

        </form>
        
       </body>
   </html>


    <script>
        function sendmessage()
        {
        
            var subject = $("#<%=txtMessageSubject.ClientID%>").val();
            var message = $("#<%=txtMessage.ClientID%>").val();
            if (subject != "" && message != "")
            {

    var postData = JSON.stringify({
        "emailAddress": $("#<%=lblReceiverEmailAddress.ClientID%>").text(),
                "subject": $("#<%=txtMessageSubject.ClientID%>").val(),
                "messageBody": $("#<%=txtMessage.ClientID%>").val()
                
                
            });
            $.ajax({
                type: "POST",
                url: "CompanyInitialRequest.aspx/sendMessage",
                data: postData,
                contentType: "application/json; charset=utf-8",
                success: function(result){
                    response=result.d;
                    if(response == "1")
                    {
                        document.getElementById("lbMailSuccessMessage").innerHTML = "Message has been sent successfully";
                        document.getElementById("lbMailErrorMessage").innerHTML = "";
                    }
                    else
                    {
                        document.getElementById("lbMailErrorMessage").innerHTML = response;
                        document.getElementById("lbMailSuccessMessage").innerHTML = "";
                    }
                  
                },
                //error: function (msg) { alert("Invalid Credentials!!"); }
            }); 

            }
            else
            {
                if (subject == "")
                {
                    $("#lblSubjectMgs").text('*');
                }
                if (message == "") {
                    $("#lblBodyMgs").text('*');
                }

            }
           event.preventDefault();
            

            
        }
    </script>
   
      
       

   

</asp:Content>
