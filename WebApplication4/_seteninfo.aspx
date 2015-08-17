<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_seteninfo.aspx.cs" Inherits="WebApplication4._seteninfo" %>

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
        工事出入口信息录入<br /> 
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
                                    <asp:BoundField DataField="rfid" HeaderText="所属工事" />
                                    <asp:BoundField DataField="damage" HeaderText="损毁程度" />
                                    <asp:BoundField DataField="damageinfo" HeaderText="损毁情况" >
                                       <FooterStyle CssClass="hidden" />
                                    <HeaderStyle CssClass="hidden" />
                                    <ItemStyle Width="200px" CssClass="hidden" />
                                          </asp:BoundField>
                                    <asp:TemplateField HeaderText="编辑">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_viewinfo" runat="server" OnClick="Button1_Click"  ToolTip="查看详情"  ImageUrl="~/image/web_layout.png"/>
                                            <asp:ImageButton ID="btn_edit" ImageUrl="~/image/File_List.png" Width="28px" runat="server" OnClick="Button2_Click" ToolTip="编辑" />
                                                <asp:ImageButton  ID="btn_delete" ImageUrl="~/image/newspaper_close.png" runat="server" OnClick="btn_delete_Click"  OnClientClick="return confirm(&quot;确认删除?&quot;)" ToolTip="删除" />
                                               <asp:ImageButton ImageUrl="~/image/activity_monitor_add.png" ID="btn_pic" runat="server" OnClick="btn_pic_Click"   ToolTip="上传图片" />
                                                <asp:ImageButton ImageUrl="~/image/webcam-2.png" ID="ImageButton2" runat="server" OnClick="btn_cam_Click"   ToolTip="添加监控信息" />
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
                
                    <asp:Panel ID="Panel_maininfo" runat="server" HorizontalAlign="Center"   Visible="false">
                        &nbsp;<table align="center" class="auto-style11">
                        <tr>
                            <td>
                                <asp:Label ID="lb_NO" runat="server" Text="编号"></asp:Label>
                            </td>
                            <td>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tb_no" Display="Dynamic" ErrorMessage="编号不能为空" ValidationGroup="info">*</asp:RequiredFieldValidator>
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
                            <td>所属工事</td>
                            <td>
                                            <asp:DropDownList ID="ddl_rf" runat="server" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                            <td>损毁程度</td>
                            <td>
                                            <asp:DropDownList ID="ddl_d" runat="server" Width="150px">
                                                <asp:ListItem Value="0">完好</asp:ListItem>
                                                <asp:ListItem Value="1">轻微受损</asp:ListItem>
                                                <asp:ListItem Value="2">严重受损</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                            <td>&nbsp;</td>
                            <td>
                                            &nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
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
                 
                     <asp:Panel ID="Panel_picupload" runat="server" HorizontalAlign="Center" Width="1000px" Visible="false" >
                         <asp:Label ID="lb_rfname" runat="server" Text="入口名称:"></asp:Label>
                         <asp:FileUpload ID="FileUpload2" runat="server" Width="300px" />
                         &nbsp;图片说明:<asp:TextBox ID="tb_picmessage" runat="server" Width="250px"></asp:TextBox>
                         <asp:Button ID="btn_picupload" runat="server" Text="上传" Width="80px" OnClick="btn_picupload_Click" />
                         <asp:Button ID="btn_picuploadcancel" runat="server" Text="取消" Width="80px" OnClick="btn_picuploadcancel_Click" />
                            </asp:Panel>
                       <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>   
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
               
                    </asp:Panel>
            
                    <asp:Panel ID="Panel_cam" runat="server" HorizontalAlign="Center" Width="1000px" >

                        <asp:Panel runat="server"  ID="Panel_cam_add" Visible="False">
                            <asp:Label ID="lb_enname" runat="server" Text="入口名称:"></asp:Label>
                        监控名称: 
                        <asp:TextBox ID="tb_camname" runat="server" Width="300px"></asp:TextBox>
                         监控编号: 
                        <asp:TextBox ID="tb_camid" runat="server" Width="100px"></asp:TextBox>
                         <br/>  视频地址: 
                        <asp:TextBox ID="tb_camrul" runat="server" Width="400px"></asp:TextBox>
                        <asp:Button ID="btn_cam_ok" runat="server" Text="添加" Width="80px" OnClick="btn_cam_ok_Click" /><asp:Button ID="btn_cam_cancel" runat="server" Text="取消"  Width="80px" OnClick="btn_cam_cancel_Click"/>
                            </asp:Panel>
                  
                        
                        </asp:Panel>
                    </td>
            </tr>
        </table>
                          </ContentTemplate>
        </asp:UpdatePanel>
                        <asp:GridView ID="gv_camera" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" Width="1000px" OnRowDataBound="gv_camera_RowDataBound">
                              <AlternatingRowStyle BackColor="White" />
                              <Columns>
                                  <asp:BoundField DataField="name" HeaderText="监控名称" >
                                  <ItemStyle Width="150px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="no" HeaderText="监控编号" >
                                        <ItemStyle Width="100px" />
                                  </asp:BoundField>
                                  <asp:BoundField DataField="camurl" HeaderText="视频地址" />
                                  <asp:TemplateField HeaderText="操作">
                                       <ItemStyle Width="80px" />
                                      <ItemTemplate>
                                          <asp:ImageButton ImageUrl="~/image/gallery.png"  Width="32px" ID="btn_showcam" runat="server" ToolTip="查看视频" OnClick="btn_showcam_Click" />
                                          <asp:ImageButton ID="btn_deletecam" ImageUrl="~/image/close.png" runat="server" ToolTip="删除s"  OnClick="btn_deletecam_Click"  OnClientClick="return confirm(&quot;确认删除?&quot;)"/>
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
                  
                        
                        &nbsp;</td>
                     </tr>
                    </table>  
                  
    </form>
</body>
</html>
