<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication4._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>title</title>
    <link rel="stylesheet" type="text/css" href="StyleSheet2.css" />
    <script type="text/javascript">

        //判断浏览器版本
        function isBrowser() {
            var Sys = {};
            var ua = navigator.userAgent.toLowerCase();
            var s;
            (s = ua.match(/rv:([\d.]+)\) like gecko/)) ? Sys.ie = s[1] :
            (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
            (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
            (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
            (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
            (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
            if (Sys.ie) return "IE";
            if (Sys.firefox) return "Firefox";
            if (Sys.chrome) return "Chrome";
            if (Sys.opera) return "Opera";
            if (Sys.safari) return "Safari";
        }

        //设定程序界面高度
        function setHeight() {
            
            var windowsHeight = document.documentElement.clientHeight;//document.body.scrollHeight;
            document.getElementById("main").style.height = windowsHeight - 110 + "px"; //110

        }

        //导航
        function change(index) {
            switch (index) {
                case 1:
                    document.getElementById("iframe").src = "_Map.aspx";
                    document.getElementById("map").style.color = "red";
                    document.getElementById("info").style.color = "black";

                    break;
                case 2:
                    document.getElementById("iframe").src = "_Info.aspx";
                    document.getElementById("map").style.color = "black";
                    document.getElementById("info").style.color = "red";
                    break;
            }


        }

        function changeoff() {
 
            if (document.getElementById("checkbox").checked == true) {
                document.getElementById("iframe").src = "_MapOff.aspx";
                document.getElementById("map").style.color = "red";
                document.getElementById("info").style.color = "black";
            }
            else {

                document.getElementById("iframe").src = "_Map.aspx";
                document.getElementById("map").style.color = "red";
                document.getElementById("info").style.color = "black";


            }



                
        }

    </script>
</head>
<body onload="setHeight()" onresize="setHeight()" id="body">
    <form id="form1" runat="server" style="margin: 0; padding: 0;">
        <div style="margin: 0; padding: 0;" id="div">
            <table cellspacing="0px" cellpadding="0px" id="mainview">
                <tr>                                                                                                                                                               
                    <td id="logoZone" style="background-image: url('image/topbanner.jpg')" width="1042"></td>
                    <td id="logoZoner">
                        <a href="javascript:changeoff()"  style="color:white" >离线模式</a>
                      <input type="checkbox" id="checkbox" class="checkbox"  onclick="javascript: changeoff()"/>
<div class="checkbox-wrapper">
    <label for="checkbox" class="checkbox-label"></label>
</div>
                        <br/>	
                        <asp:Image ID="Image1" runat="server" Height="14px" ImageUrl="~/image/user.png" /><asp:Label ID="loginname" runat="server" Text="admin">  </asp:Label>
                        <asp:Image ID="Image2" runat="server" Height="14px" ImageUrl="~/image/logout.png" /><asp:LinkButton ID="LinkButton1" runat="server" Font-Size="11px" ForeColor="White" OnClick="LinkButton1_Click" >安全退出</asp:LinkButton>
                     
                    </td>
                </tr>
                <tr align="right" valign="middle">
                    <td id="menuZone" align="right" colspan="2">|<a href="javascript:change(1)" id="map">地图显示</a>|<a href="javascript:change(2)" id="info">系统设置</a>|
                    </td>
                </tr>
                <tr>
                    <td id="main" colspan="2">
                        <iframe marginwidth="0" marginheight="0" scrolling="no" src="_Map.aspx" id="iframe" ></iframe>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
<script type="text/javascript">
    var windowsHeight = document.documentElement.clientHeight;
    document.getElementById("main").style.height = windowsHeight - 110 + "px"; //110
</script>
</html>
