<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPageAdmin.aspx.cs" Inherits="BiddingSystem.LoginPageAdmin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<title>Login</title>
	<meta charset="UTF-8">
	<meta name="viewport" content="width=device-width, initial-scale=1">
<!--===============================================================================================-->	
	<link rel="icon" type="image/png" href="LoginResources/images/icons/favicon.ico"/>
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
</head>
<body>
  
    <div class="limiter">
		<div class="container-login100">
			<div class="wrap-login100">
				<div class="login100-form-title" style="background-image: url(LoginResources/images/bidLogin.jpg);">
					<span class="login100-form-title-1">
						Super Admin Sign In
					</span>
				</div>

				<form class="login100-form validate-form" id="form01" runat="server">
                   
					<div class="wrap-input100 validate-input m-b-26" data-validate="Username is required">
						<span class="label-input100" style="color:Black;font-weight:bold;">Username</span>
                        <asp:TextBox ID="txtUserName" class="input100" placeholder="Enter username" runat="server"></asp:TextBox>
						<span class="focus-input100"></span>
					</div>

					<div class="wrap-input100 validate-input m-b-18" data-validate = "Password is required">
						<span class="label-input100" style="color:Black;font-weight:bold;">Password</span>
                        <asp:TextBox ID="txtPwd" TextMode="Password" class="input100" placeholder="Enter password" runat="server"></asp:TextBox>
						<span class="focus-input100"></span>
					</div>

					<div class="flex-sb-m w-full p-b-30">
						<div class="contact100-form-checkbox">
                            <div style="display:inline;">
                                  <asp:CheckBox ID="chkRemember" runat="server"  name="remember-me"/>
                            </div>
							<label  style="display:inline;">
								Remember me
							</label>
						</div>

						<div>
							<%--<a href="#" class="txt1" style="color:Black;font-weight:bold;">
								Register User
							</a>--%>
						</div>
					</div>

					<div class="container-login100-form-btn">
                        <asp:Button ID="btnLogin" runat="server" class="login100-form-btn pull-right"  
                            Text="Login" style="background-color:#3c8dbc;border-radius:2px" 
                            onclick="btnLogin_Click"/>
					</div>
                    <div>
                    <asp:Label runat="server" ID="lblLoginAlert" ForeColor="Red"></asp:Label>
                   </div>
				</form>
			</div>
		</div>
	</div>

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
</html>
