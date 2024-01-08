<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmlogin.aspx.vb" Inherits="frmlogin" %>

<%@ Register Src="../sysCtrl/msg_Box.ascx" TagName="msg_Box" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html lang="en">
<head>
    <title>Welcome to LIVEEX Online Money Exchange System</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="images/cclogo.ico" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/bootstrap/css/bootstrap.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="fonts/font-awesome-4.7.0/css/font-awesome.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="fonts/Linearicons-Free-v1.0.0/icon-font.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/animate/animate.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/css-hamburgers/hamburgers.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/animsition/css/animsition.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/select2/select2.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="vendor/daterangepicker/daterangepicker.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="css/util.css">
    <link rel="stylesheet" type="text/css" href="css/main.css">
    <!--===============================================================================================-->
</head>
<body>

    <div class="limiter">
        <div class="container-login100" style="background-image: url('images/bg.jpg');">
            <div class="wrap-login100 p-t-30 p-b-50">
                <img style="width: 300px; padding-left: 100px;" src="images/PngFile.png">
                <span class="login100-form-title p-b-41">Login
                </span>
                <form id="Form1" class="login100-form validate-form p-b-33 p-t-5" runat="server">

                    <div class="wrap-input100 validate-input" data-validate="Enter username">
                        <asp:TextBox ID="txtuserid" runat="server" CssClass="input100" placeholder="User name"></asp:TextBox>

                        <span class="focus-input100" data-placeholder="&#xe82a;"></span>
                    </div>

                    <div class="wrap-input100 validate-input" data-validate="Enter password">

                        <asp:TextBox ID="txtpwd" runat="server" CssClass="input100" placeholder="Password"
                            TextMode="Password">1</asp:TextBox>

                        <span class="focus-input100" data-placeholder="&#xe80f;"></span>

                    </div>

                    <div class="container-login100-form-btn m-t-32">

                        <asp:Button ID="btnsubmit" runat="server" Text="Login" CssClass="login100-form-btn" />


                    </div>
                    <div class="ccs" runat="server" id="dvERR" style="margin-top: 10px; background-color: red; color: white; width: 250px;" visible="false">

                        <asp:Label ID="lblERR" runat="server" Text="Label" Style="margin-left: 10px;"></asp:Label>
                    </div>
                </form>

                <div class="w3-bar w3-green">
                    <center>
                        <div style="font-size: 10px;" class="w3-bar-item">

                            <asp:Label ID="lblVER" runat="server" Text="Label" ForeColor="white"></asp:Label>
                        </div>
                    </center>
                </div>
            </div>
        </div>

    </div>


    <div id="dropDownSelect1"></div>

    <!--===============================================================================================-->
    <script src="vendor/jquery/jquery-3.2.1.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/animsition/js/animsition.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/bootstrap/js/popper.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/select2/select2.min.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/daterangepicker/moment.min.js"></script>
    <script src="vendor/daterangepicker/daterangepicker.js"></script>
    <!--===============================================================================================-->
    <script src="vendor/countdowntime/countdowntime.js"></script>
    <!--===============================================================================================-->
    <script src="js/main.js"></script>

</body>
</html>
