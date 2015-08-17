<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="regsist.aspx.cs" Inherits="WebApplication4.regsist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">

        function setCenter() {
       
            var windowsWidth = document.documentElement.clientWidth;     //       .clientHeight;//document.body.scrollHeight;
            var windowsHeight = document.documentElement.clientHeight;
            document.getElementById("regist").style.marginLeft = windowsWidth / 2 - 250 + "px";
            document.getElementById("regist").style.marginTop = windowsHeight / 2 - 25 + "px";
            document.getElementById("regist").style.visibility = "visible";

        }
    </script>
</head>
<body  onload="setCenter()">
    <form id="form1" runat="server">
    <div  style=" margin-top:200px; width:100%; align-content:center;" id="logindiv" >
        <table   id="regist" style="background-color: bisque; width: 500px; height: 50px;visibility:hidden ">
            <tr>
                <td style="text-align: right">用户名</td>
                <td style="text-align: left">
                    <asp:TextBox ID="TextBox1" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">密码</td>
                <td style="text-align: left">
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">密码确认</td>
                <td style="text-align: left">
                    <asp:TextBox ID="TextBox3" runat="server" TextMode="Password" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: left">
                    <asp:Button ID="Button1" runat="server" Text="确定" />
                    <asp:Button ID="Button2" runat="server" Text="取消" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
