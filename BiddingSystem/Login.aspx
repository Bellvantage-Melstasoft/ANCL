<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BiddingSystem.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Login</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--===============================================================================================-->

   <%-- <script src="https://cdn.jsdelivr.net/npm/sweetalert2@7.29.2/dist/sweetalert2.all.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/promise-polyfill"></script>--%>

    <script src="AdminResources/js/SweetAlert.js"></script>


    <style type="text/css">
        body {
            margin: 0;
            color: #6a6f8c;
            background: #c8c8c8;
            font: 600 16px/18px 'Open Sans',sans-serif;
            background-image: url('LoginResources/images/loginbg.jpg');
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            background-attachment: fixed;
        }

        *, :after, :before {
            box-sizing: border-box;
        }

        .clearfix:after, .clearfix:before {
            content: '';
            display: table;
        }

        .clearfix:after {
            clear: both;
            display: block;
        }

        a {
            color: inherit;
            text-decoration: none;
        }

        .login-wrap {
            width: 100%;
            margin: auto;
            height: 100%;
            position: absolute;
            background: url('AdminResources/images/a.jpg') no-repeat center;
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            background-attachment: fixed;
        }

        .login-html {
            width: 100%;
            height: 100%;
            position: relative;
            padding: 40px;
            background: rgba(40,57,101,.7);
        }


            .login-html .sign-in,
            .login-html .sign-up,
            .login-form .group .check {
                display: none;
            }

            .login-html .tab,
            .login-form .group .label,
            .login-form .group .button {
                text-transform: uppercase;
                cursor: pointer;
            }

            .login-html .tab {
                font-size: 22px;
                margin-right: 15px;
                padding-bottom: 5px;
                margin: 0 15px 10px 0;
                display: inline-block;
                border-bottom: 2px solid transparent;
            }

            .login-html .sign-in:checked + .tab,
            .login-html .sign-up:checked + .tab {
                color: #fff;
                border-color: #1161ee;
            }

        .login-form {
            min-height: 345px;
            position: relative;
            perspective: 1000px;
            transform-style: preserve-3d;
        }

            .login-form .group {
                margin-bottom: 15px;
            }

                .login-form .group .label,
                .login-form .group .input,
                .login-form .group .button {
                    width: 100%;
                    color: #fff;
                    display: block;
                }

                .login-form .group .input,
                .login-form .group .button {
                    border: none;
                    padding: 15px 20px;
                    border-radius: 25px;
                    background: rgba(255,255,255,.1);
                }

                .login-form .group input[data-type="password"] {
                    text-security: circle;
                    -webkit-text-security: circle;
                }

                .login-form .group .label {
                    color: #aaa;
                    font-size: 12px;
                }

                .login-form .group .button {
                    background: #1161ee;
                }

                .login-form .group label .icon {
                    width: 15px;
                    height: 15px;
                    border-radius: 2px;
                    position: relative;
                    display: inline-block;
                    background: rgba(255,255,255,.1);
                }

                    .login-form .group label .icon:before,
                    .login-form .group label .icon:after {
                        content: '';
                        width: 10px;
                        height: 2px;
                        background: #fff;
                        position: absolute;
                        transition: all .2s ease-in-out 0s;
                    }

                    .login-form .group label .icon:before {
                        left: 3px;
                        width: 5px;
                        bottom: 6px;
                        transform: scale(0) rotate(0);
                    }

                    .login-form .group label .icon:after {
                        top: 6px;
                        right: 0;
                        transform: scale(0) rotate(0);
                    }

                .login-form .group .check:checked + label {
                    color: #fff;
                }

                    .login-form .group .check:checked + label .icon {
                        background: #1161ee;
                    }

                        .login-form .group .check:checked + label .icon:before {
                            transform: scale(1) rotate(45deg);
                        }

                        .login-form .group .check:checked + label .icon:after {
                            transform: scale(1) rotate(-45deg);
                        }

        .login-html .sign-in:checked + .tab + .sign-up + .tab + .login-form .sign-in-htm {
            transform: rotate(0);
        }

        .login-html .sign-up:checked + .tab + .login-form .sign-up-htm {
            transform: rotate(0);
        }

        .hr {
            height: 2px;
            margin: 60px 0 50px 0;
            background: rgba(255,255,255,.2);
        }

        .foot-lnk {
            text-align: center;
        }

        input:-webkit-autofill,
        input:-webkit-autofill:hover,
        input:-webkit-autofill:focus,
        input:-webkit-autofill:active {
            -webkit-box-shadow: 0 0 0 30px white inset;
        }

        .sign-in-htm {
            max-width: 520px;
        }
    </style>
</head>
<body>

    <div class="login-wrap">

        <div class="login-html" style="display: flex; justify-content: center; align-items: center;">
            <form id="form01" runat="server">

                <div>
                    <div class="login-form">
                        <small style="color: white; font-size: 50px;"><b>ezbidlanka</b></small>
                        <small style="color: white; font-size: 18px;">Online Procurement System</small>
                        <br />
                        <br />
                        <asp:Label runat="server" ID="lblError" ForeColor="OrangeRed"></asp:Label>
                        <br />
                        <br />
                        <input id="tab-1" type="radio" name="tab" class="sign-in" checked><label for="tab-1" class="tab">Sign In</label>
                        <br />
                        <div class="sign-in-htm">
                            <div class="group">
                                <label for="user" class="label">Username</label>
                                <asp:TextBox ID="txtUserName" autocorrect="off" runat="server" type="text" class="input"></asp:TextBox>
                            </div>
                            <div class="group">
                                <label for="pass" class="label">Password</label>
                                <asp:TextBox ID="txtPwd" autocorrect="off" runat="server" type="password" class="input" data-type="password"></asp:TextBox>
                            </div>
                            <div class="group">
                                <input id="chkRemember" runat="server" type="checkbox" class="check" checked>
                                <label for="check"><span class="icon"></span>&nbsp;&nbsp;Keep me Signed in</label>
                            </div>
                            <div class="group">
                                <asp:Button ID="btnLogin" runat="server" Text="Sign In" class="button" OnClick="btnLogin_Click" />
                            </div>
                        </div>
                        <br />
                        <small style="color: white; font-size: 12px; text-align:center">Powered by Bellvantage </small>
                    </div>
                </div>
            </form>
        </div>
    </div>



</body>
</html>
