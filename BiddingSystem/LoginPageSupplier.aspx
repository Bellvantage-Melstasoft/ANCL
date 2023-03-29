<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPageSupplier.aspx.cs"
    Inherits="BiddingSystem.LoginPageSupplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="LoginResources/images/icons/favicon.ico" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/vendor/bootstrap/css/bootstrap.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/vendor/animate/animate.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/vendor/css-hamburgers/hamburgers.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/vendor/animsition/css/animsition.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/vendor/select2/select2.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/vendor/daterangepicker/daterangepicker.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="LoginResources/css/util.css">
    <link rel="stylesheet" type="text/css" href="LoginResources/css/main.css">
    <!--===============================================================================================-->


        <style>
#snackbar,#snackbarMobileNumber, #snackbarFileUpload {
    visibility: hidden;
    min-width: 250px;
    margin-left: -125px;
    background-color: #333;
    color: #fff;
    text-align: center;
    border-radius: 2px;
    padding: 16px;
    position: fixed;
    z-index: 1;
    left: 50%;
    bottom: 30px;
    font-size: 17px;
}

#snackbar.show , #snackbarMobileNumber.show, #snackbarFileUpload.show{
    visibility: visible;
    -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
    animation: fadein 0.5s, fadeout 0.5s 2.5s;
}

@-webkit-keyframes fadein {
    from {bottom: 0; opacity: 0;} 
    to {bottom: 30px; opacity: 1;}
}

@keyframes fadein {
    from {bottom: 0; opacity: 0;}
    to {bottom: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
    from {bottom: 30px; opacity: 1;} 
    to {bottom: 0; opacity: 0;}
}

@keyframes fadeout {
    from {bottom: 30px; opacity: 1;}
    to {bottom: 0; opacity: 0;}
}
</style>








</head>
<body>

    
   

    <div class="limiter">
        <div class="container-login100">
            <div class="wrap-login100" style="height:auto">
                <div style="background-image: url(LoginResources/images/nava3.jpg); padding: 181px 14px 74px 15px ">
                    
                </div>
                <form class="login100-form validate-form" id="form01" runat="server" defaultbutton="btnLogin">
                <div class="modal modal-primary fade" id="myModal">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header" style="text-align: center; background-color: #357ca5; color: white;
                                font-weight: bold;">
                                <h4 class="modal-title" style="text-align: center;">
                                    Supplier Registration</h4>
                            </div>
                            <div class="modal-body">
                                <div class="table-responsive">
                                    <div class="form-group">
                                        <label style="display:inline;">
                                            Supplier Name</label><label id="nameAlert" style="color: red; display:inline; font-size: small;"></label>
                                        <asp:TextBox ID="txtSupplierName" runat="server" AutoComplete="off" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label  style="display:inline;">Email address</label><label id="emailValidaAlert" style="color: red; font-size: small;"></label>
                                        <asp:TextBox ID="txtEmailAddress" runat="server" AutoComplete="off" CssClass="form-control" placeholder="Email address"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label  style="display:inline;">
                                            Contact No</label><label id="contactNoAlert" style="color: red; display:inline; font-size: small;"></label>
                                        <asp:TextBox ID="txtContactNo" runat="server" AutoComplete="off" CssClass="form-control" type="number"
                                            placeholder="Contact No"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label  style="display:inline;">
                                            user name</label><label id="usernameAlert" style="color: red; display:inline; font-size: small;"></label>
                                        <asp:TextBox ID="txtUsernameR" runat="server" AutoComplete="off" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label  style="display:inline;">
                                            Password</label><label id="passwordAlert" style="color: red; display:inline; font-size: small;"></label>
                                        <asp:TextBox ID="txtpasswordR" runat="server" AutoComplete="off" CssClass="form-control" placeholder="Password"
                                            TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label  style="display:inline;">
                                            Confirm Password</label><label id="confirmPasswordAlert" style="color: red; display:inline; font-size: small;"></label>&nbsp;<label
                                                id="PasswordMatchAlert" style="color: red; font-size: small;"></label>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" AutoComplete="off" CssClass="form-control" placeholder="Re-enter password"
                                            TextMode="Password"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label id="lblregisterAlert" style="color: red;">
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer" style="text-align: left; background-color: #357ca5; color: white;
                                font-weight: bold;">
                                <button type="button" class="btn btn-outline pull-left btn-danger" data-dismiss="modal">
                                    Close</button>
                                <asp:Button class="btn btn-outline btn-info" runat="server" ID="btnRegister" Text="Register"
                                    ValidationGroup="btnRegister" OnClientClick="return formValidation();"></asp:Button>
                            </div>
                             <div id="snackbar">
        Phone number should has 10 Digits</div>
    <div id="snackbarMobileNumber">
        Mobile number should has 10 Digits</div>
                        </div>
                        <!-- /.modal-content -->
                    </div>
                    <!-- /.modal-dialog -->
                </div>
                <div class="wrap-input100 validate-input m-b-26" data-validate="Username is required">
                    <span class="label-input100" style="color: Black; font-weight: bold;">Username</span>
                    <asp:TextBox ID="txtUserName" class="input100" placeholder="Enter username" runat="server"></asp:TextBox>
                    <span class="focus-input100"></span>
                </div>
                <div class="wrap-input100 validate-input m-b-18" data-validate="Password is required">
                    <span class="label-input100" style="color: Black; font-weight: bold;">Password</span>
                    <asp:TextBox ID="txtPwd" TextMode="Password" class="input100" placeholder="Enter password"
                        runat="server"></asp:TextBox>
                    <span class="focus-input100"></span>
                </div>
                <div class="flex-sb-m w-full p-b-30">
                   <div class="contact100-form-checkbox">
                           <div class="contact100-form-checkbox">
                            <div style="display:inline;">
                                  <asp:CheckBox ID="chkRemember" runat="server"  name="remember-me"/>
                            </div>
							<label  style="display:inline;">
								Remember me
							</label>
						</div>
						</div>
                  <a href="ResetPassword.aspx" class="" style="color: Black; font-weight: bold;">
                            Forgot Password?
                        </a>
                </div>
                <div class="container-login100-form-btn">
                    <span class="pull-left">
                    <asp:Button ID="btnLogin" runat="server" class="login100-form-btn pull-right" Text="Login"  Style="background-color: #3c8dbc; margin:5px; border-radius: 2px" OnClick="btnLogin_Click" />
                    </span>
                        <span class="pull-right" data-toggle="modal" data-target="#myModal">
                        <button id="btnReg"   class="login100-form-btn pull-right" style="background-color: #a75858;  margin:5px; border-radius: 2px" ><a href="#" style="color:white;" >Register User </a></button>
                            </span>
                </div>
                <div>
                    <asp:Label runat="server" ID="lblLoginmessage" ForeColor="Red"></asp:Label>
                </div>
                </form>
            </div>
        </div>
    </div>


     <script src="adminresources/js/jquery.min.js" type="text/javascript"></script>
   

</body>
<!--===============================================================================================-->
<script src="LoginResources/vendor/jquery/jquery-3.2.1.min.js"></script>
<!--===============================================================================================-->
<script src="LoginResources/vendor/animsition/js/animsition.min.js"></script>
<!--===============================================================================================-->
<script src="LoginResources/vendor/bootstrap/js/popper.js"></script>
<script src="LoginResources/vendor/bootstrap/js/bootstrap.min.js"></script>
<!--===============================================================================================-->
<script src="LoginResources/vendor/select2/select2.min.js"></script>
<!--===============================================================================================-->
<script src="LoginResources/vendor/daterangepicker/moment.min.js"></script>
<script src="LoginResources/vendor/daterangepicker/daterangepicker.js"></script>
<!--===============================================================================================-->
<script src="LoginResources/vendor/countdowntime/countdowntime.js"></script>
<!--===============================================================================================-->
<script src="LoginResources/js/main.js"></script>
<link href="LoginResources/css/bootstrap-multiselect.css" rel="stylesheet" />
<link href="LoginResources/css/main.css" rel="stylesheet" />
<script src="LoginResources/js/bootstrap-multiselect.js"></script>
<script type="text/javascript">
        function ClickRegister() {
            document.getElementById("lblRegister").innerHTML = "Supplier registered successfully."
        }

        $(function () {
        $('[id*=lstCompanyList]').multiselect({
            includeSelectAllOption: true
        });
        });

         function openModal() {
               $('#myModal').modal('show');
        }

      

        function existName()
        {
            var postData = JSON.stringify({
                "name": $("#txtSupplierName").val(),
               
            });
           
                $.ajax({
                    type: "POST",
                    url: "LoginPageSupplier.aspx/checkExistCompanyName",
                    data: postData,
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        response = result.d;
                        if (response == "1") {
                            return false;
                        }
                        else if (response == "0") {
                            return true;
                        }
                    },

                });
            
            event.preventDefault();  
        }



        function validateEmail(email) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(email);
        }

        function formValidation() {
            var Name = $("#txtSupplierName").val();
            var email = $("#txtEmailAddress").val();
            var username = $("#txtUsernameR").val();
            var password = $("#txtpasswordR").val();
            var confirmpassword = $("#txtConfirmPassword").val();
            var contactno = $("#txtContactNo").val();
            
            $("#<%=txtSupplierName.ClientID %>").css('border-color', '#d9d9d9');
            $("#<%=txtEmailAddress.ClientID %>").css('border-color', '#d9d9d9');
            $("#<%=txtUsernameR.ClientID %>").css('border-color', '#d9d9d9');
            $("#<%=txtpasswordR.ClientID %>").css('border-color', '#d9d9d9');
            $("#<%=txtConfirmPassword.ClientID %>").css('border-color', '#d9d9d9');
             $("#<%=txtContactNo.ClientID %>").css('border-color', '#d9d9d9');
         

            document.getElementById("nameAlert").innerHTML = "";
            document.getElementById("contactNoAlert").innerHTML = ""; 
            document.getElementById("usernameAlert").innerHTML = ""; 
            document.getElementById("passwordAlert").innerHTML = ""; 
            document.getElementById("confirmPasswordAlert").innerHTML = ""; 
            document.getElementById("PasswordMatchAlert").innerHTML = ""; 
            document.getElementById("lblregisterAlert").innerHTML = "";
            document.getElementById("emailValidaAlert").innerHTML = "";

            if (Name != "" && email != "" && username != "" && password != "" && confirmpassword != "" && contactno != "")
            {
               
                if (password == confirmpassword && validateEmail(email) && contactno.length == 10 && username.length > 5)
                {
                    var postData = JSON.stringify({
                        "name": $("#txtSupplierName").val(),
                    });

                    $.ajax({
                        type: "POST",
                        url: "LoginPageSupplier.aspx/checkExistCompanyName",
                        data: postData,
                        contentType: "application/json; charset=utf-8",
                        success: function (result) {
                            response = result.d;
                            if (response == "1") {
                                document.getElementById("nameAlert").innerHTML = " This Name already exists.";
                                return false;
                            }
                            else if (response == "0") {
                                document.getElementById("nameAlert").innerHTML = "";


                                var postData = JSON.stringify({
                                    "name": $("#txtSupplierName").val(),
                                    "email": $("#txtEmailAddress").val(),
                                    "contactNo": $("#txtContactNo").val(),
                                    "username": $("#txtUsernameR").val(),
                                    "password": $("#txtpasswordR").val()
                                });
                               
                                $.ajax({
                                    type: "POST",
                                    url: "LoginPageSupplier.aspx/register",
                                    data: postData,
                                    contentType: "application/json; charset=utf-8",
                                    success: function (result) {
                                        response = result.d;
                                        if (response == "1") {
                                            window.location = "SupplierProfile.aspx";
                                        }
                                        else if (response == "0") {
                                            document.getElementById("lblregisterAlert").innerHTML = "Login Problem";
                                        }
                                        else if (response == "-1") {
                                            document.getElementById("lblregisterAlert").innerHTML = "Email Address or Username already exist";
                                        }

                                    },

                                });
                                event.preventDefault();
                            }
                        },

                    });

                    event.preventDefault();



                 
                }
                else {
                    if (password != confirmpassword) { document.getElementById("PasswordMatchAlert").innerHTML = "Password does not match"; }
                    if (!validateEmail(email)) { document.getElementById("emailValidaAlert").innerHTML = "Invalid Email address"; }
                    if (contactno.length != 10) { document.getElementById("contactNoAlert").innerHTML = "contact no should has 10 digits"; }
                    if (username.length < 6) { document.getElementById("usernameAlert").innerHTML = "Username should contains at-least 6 characters"; }
                    return false;
                }
            }
            else {
                if (Name == "") { $("#<%=txtSupplierName.ClientID %>").css('border-color', 'red'); } else{    document.getElementById("nameAlert").innerHTML = ""; $("#<%=txtSupplierName.ClientID %>").css('border-color', '#d9d9d9');}
                if (email == "") { $("#<%=txtEmailAddress.ClientID %>").css('border-color', 'red'); } else if (!validateEmail(email)) { document.getElementById("emailValidaAlert").innerHTML = "Invalid Email address"; } else { document.getElementById("emailValidaAlert").innerHTML = ""; $("#<%=txtEmailAddress.ClientID %>").css('border-color', '#d9d9d9'); }
                if (username == "") {  $("#<%=txtUsernameR.ClientID %>").css('border-color', 'red'); } else {document.getElementById("usernameAlert").innerHTML = "";  $("#<%=txtUsernameR.ClientID %>").css('border-color', '#d9d9d9');}
                if (password == "") {   $("#<%=txtpasswordR.ClientID %>").css('border-color', 'red'); } else {document.getElementById("passwordAlert").innerHTML = "";  $("#<%=txtpasswordR.ClientID %>").css('border-color', '#d9d9d9');}
                if (confirmpassword == "") { $("#<%=txtConfirmPassword.ClientID %>").css('border-color', 'red');} else {document.getElementById("confirmPasswordAlert").innerHTML = "";  $("#<%=txtConfirmPassword.ClientID %>").css('border-color', '#d9d9d9'); }
                if (contactno == "") { $("#<%=txtContactNo.ClientID %>").css('border-color', 'red'); }  else { $("#<%=txtContactNo.ClientID %>").css('border-color', '#d9d9d9'); document.getElementById("contactNoAlert").innerHTML = ""; }
                if (password != confirmpassword) { document.getElementById("PasswordMatchAlert").innerHTML = "Password does not match"; } 
               
                return false;
            }
          
        }
</script>
    <script type="text/javascript">
        $("#<%=txtSupplierName.ClientID %>").keyup(function (e) {
            if ($("#txtSupplierName").val() == "") { $("#<%=txtSupplierName.ClientID %>").css('border-color', 'red'); }
            else
            {
                var postData = JSON.stringify({
                    "name": $("#txtSupplierName").val(),
                });

                $.ajax({
                    type: "POST",
                    url: "LoginPageSupplier.aspx/checkExistCompanyName",
                    data: postData,
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        response = result.d;
                        if (response == "1") {
                            document.getElementById("nameAlert").innerHTML = " This Name already exists.";
                        }
                        else if (response == "0") {
                            document.getElementById("nameAlert").innerHTML = "";
                        }
                    },

                });

                event.preventDefault();
            }
           
            {

                document.getElementById("nameAlert").innerHTML = ""; $("#<%=txtSupplierName.ClientID %>").css('border-color', '#d9d9d9');
            }
        });

         $("#<%=txtEmailAddress.ClientID %>").keyup(function (e) {
             if ($("#txtEmailAddress").val() == "") { $("#<%=txtEmailAddress.ClientID %>").css('border-color', 'red'); } else if (!validateEmail($("#txtEmailAddress").val())) { document.getElementById("emailValidaAlert").innerHTML = "Invalid Email address"; $("#<%=txtEmailAddress.ClientID %>").css('border-color', '#d9d9d9'); } else { document.getElementById("emailValidaAlert").innerHTML = ""; $("#<%=txtEmailAddress.ClientID %>").css('border-color', '#d9d9d9'); }
         });

         $("#<%=txtUsernameR.ClientID %>").keyup(function (e) {
             if ($("#txtUsernameR").val() == "") { $("#<%=txtUsernameR.ClientID %>").css('border-color', 'red'); } else if (username.length < 6) { document.getElementById("usernameAlert").innerHTML = "Username should contains at-least 6 characters"; } else { document.getElementById("nameAlert").innerHTML = ""; $("#<%=txtUsernameR.ClientID %>").css('border-color', '#d9d9d9'); }
         });

         $("#<%=txtpasswordR.ClientID %>").keyup(function (e) {
             if ($("#txtpasswordR").val() == "") { $("#<%=txtpasswordR.ClientID %>").css('border-color', 'red'); } else { document.getElementById("nameAlert").innerHTML = ""; $("#<%=txtpasswordR.ClientID %>").css('border-color', '#d9d9d9'); }
        });

         $("#<%=txtConfirmPassword.ClientID %>").keyup(function (e) {
             if ($("#txtConfirmPassword").val() == "") { $("#<%=txtConfirmPassword.ClientID %>").css('border-color', 'red'); } else { document.getElementById("nameAlert").innerHTML = ""; $("#<%=txtConfirmPassword.ClientID %>").css('border-color', '#d9d9d9'); }
         });

        $("#<%=txtContactNo.ClientID %>").keyup(function (e){if ($("#<%=txtContactNo.ClientID %>").val() == ""){   $("#<%=txtContactNo.ClientID %>").css('border-color', 'red');} else if ($("#<%=txtContactNo.ClientID %>").val().length != 10){document.getElementById("contactNoAlert").innerHTML = " Contact no should has 10 digits";}else{$("#<%=txtContactNo.ClientID %>").css('border-color', '#d9d9d9');document.getElementById("contactNoAlert").innerHTML = ""; }});

        
      


    </script>

    <script>
        $("#<%=txtContactNo.ClientID %>").keypress(function (e) {
           
             if( e.which!=8 && e.which!=0 && (e.which<48 || e.which>57))
             {
              return false;
             }
         });
    </script>
  
</html>
