<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="WebApplication4.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function setHeight() {
            var windowsHeight = document.documentElement.clientHeight;//document.body.scrollHeight;
            //   document.getElementById("body").style.height = windowsHeight + "px"; //110
            //  document.getElementById("form1").style.height = windowsHeight+ "px"; //110
            document.getElementById("iframe").style.height = windowsHeight+ "px"; //110

        }


    </script>
</head>
<body onload="setHeight()"  style="margin:0;padding:0; background-color:red ;overflow:hidden" >
    <form id="form1" runat="server">
    <div style="overflow:hidden">
<iframe  marginwidth="0" marginheight="0" scrolling="no"  src="_Map.aspx" id="iframe" style=" overflow:hidden; width:100%; border:none; margin:0px;padding:0px;" ></iframe>

    </div>
    </form>
</body>
</html>
