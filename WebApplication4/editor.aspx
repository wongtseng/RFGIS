<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="editor.aspx.cs" Inherits="WebApplication4.editor"  validateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
      <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
  <%-- <link rel="stylesheet" href="editor/themes/default/default.css" />--%>
<%--<link rel="stylesheet" href="editor/plugins/code/prettify.css" />--%>
	<script charset="utf-8" src="editor/kindeditor.js"></script>
	<script charset="utf-8" src="editor/lang/zh_CN.js"></script>
	<script charset="utf-8" src="editor/plugins/code/prettify.js"></script>
	<script>
	    KindEditor.ready(function (K) {
	        var editor1 = K.create('#editor_id', {
	            //cssPath: 'editor/plugins/code/prettify.css',
	            uploadJson: 'editor/asp.net/upload_json.ashx',
	            fileManagerJson: 'editor/asp.net/file_manager_json.ashx',
	            allowFileManager: true,
	            afterCreate: function () {
	                var self = this;
	                K.ctrl(document, 13, function () {
	                    self.sync();
	                    K('form[name=form1]')[0].submit();
	                });
	                K.ctrl(self.edit.doc, 13, function () {
	                    self.sync();
	                    K('form[name=form1]')[0].submit();
	                });
	            }
	        });
	        prettyPrint();
	    });
	</script>

    <title></title>
      <style type="text/css">
        .hidden { display:none;}
          .auto-style1 {
              width: 100%;
          }
        </style>
</head>
<body style="background-color:aliceblue;text-align:center; ">
    <form id="form1" runat="server">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image/window_app_list_add.png" OnClick="btn_add_Click" ToolTip="添加人防工事信息"/>
                    <br />
                        <asp:Panel ID="Panel_gridview" runat="server" HorizontalAlign="Center">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" Width="1000px" OnRowDataBound="GridView1_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="no"  HeaderText="NO" >
                                           <ItemStyle Width="25px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="title" HeaderText="标题" />
                                    <asp:TemplateField HeaderText="编辑">
                                           <ItemStyle Width="150px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_viewinfo" runat="server" ImageUrl="~/image/web_layout.png" OnClick="btn_view_Click" ToolTip="查看详情" />
                                                <asp:ImageButton ID="btn_edit" runat="server" ImageUrl="~/image/File_List.png" OnClick="btn_edit_Click" ToolTip="编辑" />
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
                    <br />
        <asp:Panel ID="panel_content_view" runat="server" Height="300px" ScrollBars="Auto" Visible="False" HorizontalAlign="Left">
         <asp:Label ID="lb_thetitle" runat="server" Text="thetitle"></asp:Label>
                    <br />
            <asp:Label ID="lb_thecontent" runat="server" Text="thecontent" ></asp:Label>
                    <br />
                    <br />
            <asp:Button ID="btn_closeview" runat="server" OnClick="btn_closeview_Click" Text="关闭" />

        </asp:Panel>
   
        <br />
            <asp:Panel ID="panel_edit" runat="server"  Visible="false" HorizontalAlign="Center">
              
                    <br/>

                    <table class="auto-style1">
                        <tr>
                            <td colspan="3">
                                <asp:Label ID="lb_title" runat="server" Text="标题"></asp:Label>
                                <asp:TextBox ID="tb_title" runat="server" Width="630px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style=" width:20%">&nbsp;</td>
                            <td>
                                <textarea id="editor_id" runat="server" cols="20" name="content" rows="1" style="width:100%;height:300px;">
</textarea></td>
                            <td style=" width:20%">&nbsp;</td>
                        </tr>
                    </table>
 <asp:Button ID="btn_cancel" runat="server" Text="取消" OnClick="btn_cancel_Click" /> 
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="提交内容" />
                (提交快捷键: Ctrl + Enter)
      </asp:Panel>
    </form>
</body>
</html>
