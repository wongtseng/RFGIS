<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_setrfinfo.aspx.cs" Inherits="WebApplication4._setinfo1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">

        .center
        {

            text-align:center;
          
        }
        .hidden { display:none;}
        .auto-style11 {
            width: 100%;
        }
        </style>


</head>
<body style="background-color:aliceblue;text-align:center; ">
    <form id="form1" runat="server">
        人防信息录入<br /> 
        <table align="center" >
            <tr>
                <td>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/image/window_app_list_add.png" OnClick="btn_add_Click" ToolTip="添加人防工事信息"/>
                    <br />
                        <asp:Panel ID="Panel_gridview" runat="server" HorizontalAlign="Center">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" Width="1000px" OnRowDataBound="GridView1_RowDataBound">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="ID"  HeaderText="ID" >
                                    <FooterStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NO" HeaderText="编号" />
                                    <asp:BoundField DataField="name" HeaderText="名称" />
                                    <asp:BoundField DataField="lng" HeaderText="经度" />
                                    <asp:BoundField DataField="lat" HeaderText="纬度" />
                                    <asp:BoundField DataField="capacity" HeaderText="初始库容" />
                                    <asp:BoundField DataField="damageinfo" HeaderText="损毁情况" >
                                    <FooterStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle Width="200px" CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="value" HeaderText="状况" />
                                    <asp:BoundField DataField="wsurl" HeaderText="服务地址" >
                                      <FooterStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ip" HeaderText="IP地址">
                                      <FooterStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PortNum" HeaderText="端口号" >
                                       <FooterStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="address" HeaderText="地址" />
                                    <asp:BoundField DataField="DepID" HeaderText="摄像头ID" >
                                        <FooterStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle CssClass="hidden" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="编辑">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_viewinfo" runat="server" ImageUrl="~/image/web_layout.png" OnClick="Button1_Click" ToolTip="查看详情" />
                                            <asp:ImageButton ID="btn_edit" runat="server" ImageUrl="~/image/File_List.png" OnClick="Button2_Click" ToolTip="编辑" Width="28px" />
                                            <asp:ImageButton ID="btn_delete" runat="server" ImageUrl="~/image/newspaper_close.png" OnClick="btn_delete_Click" OnClientClick="return confirm(&quot;确认删除?&quot;)" ToolTip="删除" />
                                            <asp:ImageButton ID="btn_pic" runat="server" ImageUrl="~/image/activity_monitor_add.png" OnClick="btn_pic_Click" ToolTip="上传图片" />
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
                    <br/>     
                    <asp:Panel ID="Panel_maininfo" runat="server" HorizontalAlign="Center"   Visible="false">
                                <table align="center" class="auto-style11">
                        <tr>
                            <td>
                                <asp:Label ID="lb_NO" runat="server" Text="编号"></asp:Label>
                            </td>
                            <td>
                                            <asp:TextBox ID="tb_no" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                            <td>
                                <asp:Label ID="lb_name" runat="server" Text="名称"></asp:Label>
                            </td>
                            <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tb_name" Display="Dynamic" ErrorMessage="名称不能为空" ValidationGroup="info">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tb_name" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                            <td>经度</td>
                            <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tb_lng" Display="Dynamic" ErrorMessage="经度不能为空" ValidationGroup="info">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tb_lng" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                            <td>纬度</td>
                            <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tb_lat" Display="Dynamic" ErrorMessage="纬度不能为空" ValidationGroup="info">*</asp:RequiredFieldValidator>
                                            <asp:TextBox ID="tb_lat" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                        </tr>
                        <tr>
                            <td>初始库容</td>
                            <td>
                                            <asp:TextBox ID="tb_capacity" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                            <td>服务地址</td>
                            <td>
                                            <asp:TextBox ID="tb_wsurl" runat="server"></asp:TextBox>
                                        </td>
                            <td>状况</td>
                            <td>
                                            <asp:DropDownList ID="ddl_state" runat="server" Width="150px">
                                                <asp:ListItem Value="0">可用</asp:ListItem>
                                                <asp:ListItem Value="1">不可用</asp:ListItem>
                                            </asp:DropDownList>
                            </td>
                            <td>DepID</td>
                            <td> <asp:TextBox ID="tb_depID" runat="server" Width="150px"></asp:TextBox></td>
                        </tr>
                                      <tr>
                            <td>IP地址</td>
                            <td> <asp:TextBox ID="TB_IP" runat="server" Width="150px"></asp:TextBox></td>
                            <td>端口号</td>
                            <td><asp:TextBox ID="TB_ProtNum" runat="server"></asp:TextBox></td>
                            <td>位置</td>
                            <td  colspan="3"><asp:TextBox ID="TB_Address" runat="server" Width="100%"></asp:TextBox></td>
                          
                        </tr>
                        <tr>
                            <td>损毁情况</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                                  <tr>
                            <td colspan="8">
                                            <asp:TextBox ID="tb_damageinfo" runat="server" style="margin-left: 0px" Width="100%" Height="80px" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                        </tr>
                          <tr>
                                         <td colspan="8" style="text-align:right">
                                               <asp:Button ID="btn_infoedit_ok" runat="server" Text="确定" Width="80px" OnClick="btn_infoedit_ok_Click" ValidationGroup="info" />&nbsp
                                               <asp:Button ID="btn_infoedit_cancel" runat="server" OnClick="btn_infoedit_cancel_Click" Text="取消" Width="80px" />
                                               <br />
                                               <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="info" />
                                        </td>
                                         </tr>
                    </table>
                            </asp:Panel>
                    <br/>
                     <asp:Panel ID="Panel_picupload" runat="server" HorizontalAlign="Center" Width="1000px" Visible="false" >
                         <asp:Label ID="lb_rfname" runat="server" Text="人防工事名称:"></asp:Label>
                         <asp:FileUpload ID="FileUpload2" runat="server" Width="300px" />
                         &nbsp;图片说明:<asp:TextBox ID="tb_picmessage" runat="server" Width="250px"></asp:TextBox>
                         <asp:Button ID="btn_picupload" runat="server" Text="上传" Width="80px" OnClick="btn_picupload_Click" />
                         <asp:Button ID="btn_picuploadcancel" runat="server" Text="取消" Width="80px" OnClick="btn_picuploadcancel_Click" />
                            </asp:Panel>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate> 
                    <asp:Panel ID="Panel_picselect" runat="server" HorizontalAlign="Center" Width="1000px" >
                          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" Width="1000px" OnRowDataBound="GridView2_RowDataBound">
                              <AlternatingRowStyle BackColor="White" />
                              <Columns>
                                  <asp:BoundField DataField="name" HeaderText="图片名称" >
                                  <ItemStyle Width="80px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="size" HeaderText="图片大小" >
                              
                                  </asp:BoundField>
                                  <asp:BoundField DataField="time" HeaderText="加入时间" />
                                  <asp:BoundField DataField="urlname" HeaderText="urlname">
                                  <HeaderStyle CssClass="hidden" />
                                  <ItemStyle CssClass="hidden" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="message" HeaderText="图片信息" />
                                  <asp:TemplateField HeaderText="操作">
                                       <ItemStyle Width="200px" />
                                      <ItemTemplate>
                                          <asp:ImageButton ImageUrl="~/image/gallery.png"  Width="32px" ID="btn_showpic" runat="server" ToolTip="查看图片" OnClick="btn_showpic_Click" />
                                          <asp:ImageButton ID="btn_deletepic" ImageUrl="~/image/close.png" runat="server" ToolTip="删除图片"  OnClick="btn_deletepic_Click"  OnClientClick="return confirm(&quot;确认删除?&quot;)"/>
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
                          <br />
                        <asp:ImageButton ID="Image1" runat="server" OnClick="Image1_Click" />
                          <asp:ScriptManager ID="ScriptManager1" runat="server">
                          </asp:ScriptManager>
                        </asp:Panel>
                    <br/>
                      
                    </td>
            </tr>
        </table>
    
        
                          </ContentTemplate>
        </asp:UpdatePanel>
                     
    </form>
</body>
</html>
