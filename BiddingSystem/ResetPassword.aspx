<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="BiddingSystem.ResetPassword" %>

<html><head>

      </head>

    <body>
        <form runat="server">


 <link href="UserRersourses/assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Font Awesome CSS -->
    <link href="UserRersourses/assets/fonts/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <!-- Line Icons CSS -->
    <link href="UserRersourses/assets/fonts/line-icons/line-icons.css" rel="stylesheet"
        type="text/css" />
    <!-- Main Styles -->
    <link href="UserRersourses/assets/css/main.css" rel="stylesheet" type="text/css" />
    <!-- Animate CSS -->
    <link href="UserRersourses/assets/extras/animate.css" rel="stylesheet" type="text/css" />
    <!-- Owl Carousel -->
    <link href="UserRersourses/assets/extras/owl.carousel.css" rel="stylesheet" type="text/css" />
    <link href="UserRersourses/assets/extras/owl.theme.css" rel="stylesheet" type="text/css" />
    <!-- Rev Slider Css -->
    <link href="UserRersourses/assets/extras/settings.css" rel="stylesheet" type="text/css" />
    <!-- Nivo Lightbox Css -->
    <link href="UserRersourses/assets/extras/nivo-lightbox.css" rel="stylesheet" type="text/css" />
    <!-- Responsive CSS Styles -->
    <link href="UserRersourses/assets/css/responsive.css" rel="stylesheet" type="text/css" />
    <!-- Slicknav Css -->
    <link href="UserRersourses/assets/css/slicknav.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap Select -->
    <link href="UserRersourses/assets/css/bootstrap-select.min.css" rel="stylesheet" type="text/css" />




       <script src="UserRersourses/assets/js/jquery-min.js" type="text/javascript"></script>
      <link href="UserRersourses/test/default.css" rel="stylesheet" /> 
    <link href="UserRersourses/test/fonts.css" rel="stylesheet" /> 
    <link href="UserRersourses/test/light.css" rel="stylesheet" />
    <link href="UserRersourses/test/style.css" rel="stylesheet" />


    <div class="section"  style="background: url(AdminResources/Login/images/resetP.jpg) no-repeat 0px 0px; background-size: cover; min-height: 720px;" >
       
        <div class="container">
            
            <div class="row">
                <div class="wrapper" >
                    <div class="col-sm-3" ></div>
                    <div class="col-sm-6"  style="background-color:#dee1f94a; margin-left:5px; margin-right:5px; padding-bottom:10px; vertical-align:central;;  border-radius:5px;">

                        <div  >

                            <div class="heading main-heading" id="pnlHeading" runat="server">
                                <h3 style="text-align:center;color:white; margin:10px;"><span style="font-weight:bold; ">Reset Password</span></h3>
                              
                            </div>

                            <div class="col-sm-12 contact-form">


                              
                                  <asp:Panel runat="server" ID="pnlEnterEmail" CssClass="col-sm-12 col-md-6 col-lg-12" >
                             
                               
                                <div class="form-input">
                                  <asp:Label runat="server" Font-Bold="true" ForeColor="White">Email address</asp:Label><asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator2" ForeColor="Red" Font-Size="Large"  ValidationGroup="btnSubmit" ControlToValidate="txtEmailAddress">*</asp:RequiredFieldValidator>
                                    <asp:TextBox tabindex="8"  ID="txtEmailAddress" class="form-control shape new-angle" runat="server" required></asp:TextBox>
                                </div>
                                
                                <div class="form-input" >
                                   <span  class="pull-right">
                                       <asp:Button  runat="server"  ID="btnSubmit" ValidationGroup="btnSubmit"  Text="Next" OnClick="btnSubmit_Click" BackColor="#34538c" CssClass="btn btn-md" />
                                   </span>
                                    </div>
                                       <div class="form-input" >
                                   <asp:Label ID="lblError1" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
                                   </div>
                                      
                                      </asp:Panel>


                               <asp:Panel runat="server" ID="pnlVerify" CssClass="col-sm-12 col-md-6 col-lg-12" >
                                   <p style="color:white" >Please verify here using code that hasbeen sent to your email address</p>
                                 <div class="form-group"  >
                                  <asp:TextBox  ID="txtVerification" class="form-control shape new-angle"  runat="server" ></asp:TextBox>
                                     <asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator5" ForeColor="Red" Font-Size="Large"  ValidationGroup="btnVerification" ControlToValidate="txtVerification">*</asp:RequiredFieldValidator>

                                      <div class="form-input" >
                                     <span class="pull-right"> 
                                      <asp:Button Text="verify" runat="server" ID="btnVerification"  ValidationGroup="btnVerification"  OnClick="btnVerification_Click" BackColor="#34538c" class="btn btn-md" />
                                      </span>
                                          </div>
                                     </div>

                                    <div class="form-input" >
                                   <asp:Label ID="lblError2" ForeColor="Red"  Font-Bold="true" runat="server"></asp:Label>
                                   </div>

                               </asp:Panel>
                                   

                                  <asp:Panel runat="server" ID="pnlReset" CssClass="col-sm-12 col-md-6 col-lg-12" >
                             
                                  <div class="form-input">
                                    <asp:Label runat="server" ForeColor="White" Font-Bold="true">New Password</asp:Label><asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator3" ForeColor="Red" Font-Size="Large"  ValidationGroup="btnReset" ControlToValidate="txtPassword">*</asp:RequiredFieldValidator>
                                    <asp:TextBox tabindex="9"  type="password" maxlength="15" id="txtPassword" class="form-control shape new-angle" required runat="server" />       
                                </div>

                                  <div class="form-input">
                                    <asp:Label runat="server" ForeColor="White" Font-Bold="true">Confirm password</asp:Label><asp:RequiredFieldValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator4" ForeColor="Red" Font-Size="Large" ControlToValidate="txtConfirmPassword"  ValidationGroup="btnReset">*</asp:RequiredFieldValidator>
                                     <asp:CompareValidator Display="Dynamic" runat="server" ID="RequiredFieldValidator6" ForeColor="Red" Font-Size="Medium"  ValidationGroup="btnReset" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Password doesn't match"></asp:CompareValidator>
                                       <asp:TextBox tabindex="9"  type="password" maxlength="15"  id="txtConfirmPassword" class="form-control shape new-angle" required runat="server" />       
                             
                                         </div>

                                       <div class="form-input" >
                                   <span class="pull-right">
                                       <asp:Button  runat="server"  ID="btnReset" ValidationGroup="btnReset"  Text="Reset" OnClick="btnReset_Click" BackColor="#34538c" CssClass="btn btn-md" />
                                   </span>
                                    </div>
                                      
                                    <div class="form-input" >
                                   <asp:Label ID="lblError3" ForeColor="Red" runat="server"></asp:Label>
                                   </div>
                                      <p class="w3ls-login" visible="false" id="loginModal" runat="server"> <a href="#" data-toggle="modal" data-target="#myModal">Login</a></p>
                        
                                   </asp:Panel>


                                  <asp:Panel runat="server" ID="pnlLogin" CssClass="col-sm-12 col-md-6 col-lg-12" >
                                   <h4 style="color:white; text-align:center;">Password hasbeen changed Successfully</h4>
                                 <div class="form-group"  >
                                 
                                     <span> 
                                      <button  type="button" style="background-color:#048C57; color:white;" class="btn btn-group-justified" id="btnogin" >Login</button>
                                     
                                       
                                          </span>
                                     </div>


                               </asp:Panel>
                             </div>

                        </div>
                        </div  >
                    <div class="col-sm-3"></div>
                    
                    </div>
                </div>

            

            </div>
        </div>


    </form>

        <script>
            $('#btnogin').click(function () {
                window.open("LoginPageSupplier.aspx");
            });

        </script>
    </body>

</html>