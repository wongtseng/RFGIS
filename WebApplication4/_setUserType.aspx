<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_setUserType.aspx.cs" Inherits="WebApplication4._setUserType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body style="background-color:aliceblue;text-align:center;">
    <form id="form1" runat="server">
    <div>
    
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image/window_app_list_add.png" OnClick="btn_add_Click" ToolTip="添加用户信息" Visible="False"/>
    
                    <br />
                    <asp:DropDownList ID="DropDownList1" runat="server">
                        <asp:ListItem Value="username">用户名</asp:ListItem>
                        <asp:ListItem Value="phone">电话</asp:ListItem>
                    </asp:DropDownList>
                    <asp:TextBox ID="tb_search" runat="server"></asp:TextBox>
                    <asp:Button ID="btn_search" runat="server" Text="查找" OnClick="btn_search_Click" />
    
        <asp:Panel ID="Panel_gridview" runat="server" HorizontalAlign="Center">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnRowDataBound="GridView1_RowDataBound" Width="1000px" AllowPaging="True" OnPageIndexChanging="GridView1_PageIndexChanging">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID">
                    <FooterStyle CssClass="hidden" />
                    <HeaderStyle CssClass="hidden" />
                    <ItemStyle CssClass="hidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="username" HeaderText="用户名称" />
                    <asp:BoundField DataField="phone" HeaderText="注册电话" />
                    <asp:BoundField DataField="userTypeText" HeaderText="用户类型" />
                    <asp:TemplateField HeaderText="编辑">
                        <ItemTemplate>
                            <asp:ImageButton ID="btn_edit" runat="server" ImageUrl="~/image/File_List.png" OnClick="btn_Edit_Click" ToolTip="编辑" Width="28px" />
                            <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/image/newspaper_close.png" OnClick="btn_delete_Click" OnClientClick="return confirm(&quot;确认删除?&quot;)" ToolTip="删除" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
        </asp:Panel>
        <asp:Panel ID="Panel_maininfo" runat="server" HorizontalAlign="Center" Visible="false">
            <table align="center" class="auto-style11">
                <tr>
                    <td>
                        <asp:Label ID="lb_un" runat="server" Text="用户名称"></asp:Label>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_un" Display="Dynamic" ErrorMessage="用户名不能为空" ValidationGroup="info">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="tb_un" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lb_pw" runat="server" Text="注册电话"></asp:Label>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_pw" Display="Dynamic" ErrorMessage="密码不能为空" ValidationGroup="info">*</asp:RequiredFieldValidator>
                        <asp:TextBox ID="tb_pw" runat="server" Width="150px" Enabled="False"></asp:TextBox>
                    </td>
                    <td>用户类型</td>
                    <td>
                        <asp:DropDownList ID="ddl_usertype" runat="server">
                            <asp:ListItem Value="0">普通用户</asp:ListItem>
                            <asp:ListItem Value="1">工作人员</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" style="text-align:right">
                        <asp:Button ID="btn_infoedit_ok" runat="server" OnClick="btn_infoedit_ok_Click" Text="确定" ValidationGroup="info" Width="80px" />
                        &nbsp;<asp:Button ID="btn_infoedit_cancel" runat="server" OnClick="btn_infoedit_cancel_Click" Text="取消" Width="80px" />
                                               <br />
                                               <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="info" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
