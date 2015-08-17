<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication4.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    	<link rel="stylesheet" type="text/css" href="css/buttonstyle.css" />
    <title></title>
  
  
    <script>

        function setCenter()
        {
            var windowsWidth = document.documentElement.clientWidth;     //       .clientHeight;//document.body.scrollHeight;
            var windowsHeight = document.documentElement.clientHeight;
            document.getElementById("login").style.marginLeft = windowsWidth / 2 - 480 + "px";
            document.getElementById("login").style.marginTop = windowsHeight / 2-240 + "px";
            document.getElementById("login").style.visibility = "visible";
          //  alert("ok");
        }

    </script>
    </head>
<body  onload=" setCenter();"  onresize="setCenter();" style="background-color:#037fac">
    <form id="form1" runat="server">
    <div   id="login" style=" width:961px; height:481px; visibility:hidden; align-content:center; background-image:url(image/login.jpg)" >
       <br/>
       <table id="logintable" style=" margin-top:255px;  margin-left: 404px;width:300px; height:50px ;/*visibility:hidden*/">
            <tr>
                <td  style="text-align:right; width:50px"></td>
                <td>
                    <asp:TextBox ID="tb_un" runat="server" Width="200px">admin</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align:right" ></td>
             
                <td  style="height:40px">
                    <asp:TextBox ID="tb_pw" runat="server" Width="200px" TextMode="Password">admin</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height:50px" ></td>
                <td>
                    <asp:Button ID="btn_login"  class="button orange bigrounded" runat="server" Text="登陆"  OnClick="Button1_Click"/>
          
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
