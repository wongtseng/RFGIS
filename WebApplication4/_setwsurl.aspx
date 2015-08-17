<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_setwsurl.aspx.cs" Inherits="WebApplication4._setwsurl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="background-color:aliceblue;text-align:center;">
    <form id="form1" runat="server">
    <div>
    
        服务地址:<asp:TextBox ID="tb_wsurl" runat="server" Width="413px"></asp:TextBox>
        <asp:Button ID="btn_ok" runat="server" Text="更改" OnClick="btn_ok_Click" />
    
    </div>
    </form>
</body>
</html>
