<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_Info.aspx.cs" Inherits="WebApplication4.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>信息管理</title>
    <script type="text/javascript" src="Scripts/jquery-1.7.1.js"></script>
    <script  type="text/javascript">
        function setHeight() {
            var windowsHeight = document.documentElement.clientHeight;
            document.getElementById("mainview").style.height = windowsHeight + "px";
            document.getElementById("div").style.height = windowsHeight + "px";
        }

        //导航
        function change(index) {
            switch (index) {
                case 1:
                    document.getElementById("iframe").src = "_setrfinfo.aspx";
                    document.getElementById("rf").className = "select";
                    document.getElementById("ap").className = "nselect";
                    document.getElementById("bg").className = "nselect";
                    document.getElementById("wsurl").className = "nselect";
                    document.getElementById("en").className = "nselect";
                    document.getElementById("admin").className = "nselect";
                    document.getElementById("user").className = "nselect";
                    break;
                case 2:
                    document.getElementById("iframe").src = "_seteninfo.aspx";
                    document.getElementById("rf").className = "nselect";
                    document.getElementById("ap").className = "nselect";
                    document.getElementById("bg").className = "nselect";
                    document.getElementById("wsurl").className = "nselect";
                    document.getElementById("admin").className = "nselect";
                    document.getElementById("en").className = "select";
                    document.getElementById("admin").className = "nselect";
                    document.getElementById("user").className = "nselect";
                 
                    break;
                case 3:
                    document.getElementById("iframe").src = "_setapinfo.aspx";
                    document.getElementById("rf").className = "nselect";
                    document.getElementById("ap").className = "select";
                    document.getElementById("bg").className = "nselect";
                    document.getElementById("wsurl").className = "nselect";
                    document.getElementById("en").className = "nselect";
                    document.getElementById("admin").className = "nselect";
                    document.getElementById("user").className = "nselect";

                    break;
                case 4:
                    document.getElementById("iframe").src = "_setbginfo.aspx";
                    document.getElementById("rf").className = "nselect";
                    document.getElementById("ap").className = "nselect";
                    document.getElementById("bg").className = "select";
                    document.getElementById("wsurl").className = "nselect";
                    document.getElementById("en").className = "nselect";
                    document.getElementById("admin").className = "nselect";
                    document.getElementById("user").className = "nselect";
                    break;
                case 5:
                    document.getElementById("iframe").src = "_setwsurl.aspx";
                    document.getElementById("rf").className = "nselect";
                    document.getElementById("ap").className = "nselect";
                    document.getElementById("bg").className = "nselect";
                    document.getElementById("wsurl").className = "select";
                    document.getElementById("en").className = "nselect";
                    document.getElementById("admin").className = "nselect";
                    document.getElementById("user").className = "nselect";
                    break;
                case 6:
                    document.getElementById("iframe").src = "_setSysTemUser.aspx";
                    document.getElementById("rf").className = "nselect";
                    document.getElementById("ap").className = "nselect";
                    document.getElementById("bg").className = "nselect";
                    document.getElementById("wsurl").className = "nselect";
                    document.getElementById("en").className = "nselect";
                    document.getElementById("admin").className = "select";
                    document.getElementById("user").className = "nselect";
                    break;
                case 7:
                    document.getElementById("iframe").src = "_setUserType.aspx";
                    document.getElementById("rf").className = "nselect";
                    document.getElementById("ap").className = "nselect";
                    document.getElementById("bg").className = "nselect";
                    document.getElementById("wsurl").className = "nselect";
                    document.getElementById("en").className = "nselect";
                    document.getElementById("admin").className = "nselect";
                    document.getElementById("user").className = "select";
                    break;
         
            }


        }

        $(function () {
            //菜单隐藏展开
            var tabs_i = 0
            $('.vtitle').click(function () {
                var _self = $(this);
                var j = $('.vtitle').index(_self);
                if (tabs_i == j) return false; tabs_i = j;
                $('.vtitle em').each(function (e) {
                    if (e == tabs_i) {
                        $('em', _self).removeClass('v01').addClass('v02');
                    } else {
                        $(this).removeClass('v02').addClass('v01');
                    }
                });
                $('.vcon').slideUp().eq(tabs_i).slideDown();
            });
        })
    </script>
    <style type="text/css">
*{margin:0;padding:0;list-style-type:none;}
a,img{border:0;}
body{font:12px Arial, Helvetica, sans-serif, "新宋体";}
a,a:hover{text-decoration: none;}

/*收缩菜单*/
.v{float:right;width:14px;height:14px;overflow:hidden;background:url(images/vicon.png) no-repeat;display:inline-block;margin-top:-5px;margin-bottom:-5px;}
.v01{background-position:0 0;}
.v02{background-position:0 -16px;;}
.vtitle{height:35px;background:#fbede0;line-height:35px;border:1px solid #ccb6a9;margin-top:-1px;padding-left:20px;font-size:15px;color:#4d4d4d;font-family:"\5FAE\8F6F\96C5\9ED1";cursor:pointer;}
.vtitle em{margin:10px 10px 0 0;}
.vconlist{background:#9cc;}
.vconlist li a{height:30px;line-height:30px;padding-left:30px;display:block;font-size:14px;color:#866f67;font-family:"\5FAE\8F6F\96C5\9ED1";}
.vconlist li.select a,.vconlist li a:hover{color:#ed4948;text-decoration:none;}
</style>
</head>
<body style="margin:0;padding:0;"  onload="setHeight()" onresize="setHeight()">
    <form id="form1" runat="server" style="margin:0; padding:0">
    <div style="margin:0; padding:0;" id="div" >
        <table   cellspacing="0px" cellpadding="0px" id="mainview"  style="width:100%; background-color:white; height:100%"  >
            <tr  style="background-color:AppWorkspace;margin:0; padding:0;">
                <td  style="width:200px; vertical-align: top;">
                   <div style="width:200px;">
	<div class="vtitle"><em class="v v02"></em>信息录入</div>
		<div class="vcon">
		<ul class="vconlist clearfix">
			<li class="select" id="rf"><a href="javascript:change(1);">人防工事信息录入</a></li>
            <li class="nselect" id="en"><a href="javascript:change(2);">工事出入口信息录入</a></li>
			<li class="nselect" id="ap"><a href="javascript:change(3);">无线AP接入点信息录入</a></li>
			<li  class="nselect" id="bg"><a href="javascript:change(4);">桥接设备信息录入</a></li>
		</ul>
	</div>
	<div class="vtitle"><em class="v"></em>系统设置</div>
		<div class="vcon" style="display: none;">
		<ul class="vconlist clearfix">
			<li class="noselect" id="wsurl"><a href="javascript:change(5);">服务地址设置</a></li>
			<li class="noselect" id="admin"><a href="javascript:change(6);">系统用户设置</a></li>
            	<li class="noselect" id="user"><a href="javascript:change(7);">管理员用户设置</a></li>
			<li><a href="javascript:;">content</a></li>
		</ul>
	</div>
	<div class="vtitle"><em class="v"></em>系统预留</div>
		<div class="vcon" style="display: none;">
		<ul class="vconlist clearfix">
			<li><a href="javascript:;">content</a></li>
			<li><a href="javascript:;">content</a></li>
			<li><a href="javascript:;">content</a></li>
		</ul>
	</div>
</div>
                </td>
                <td  style="vertical-align:initial">
                  <iframe  src="_setrfinfo.aspx" id="iframe" style="width:100%; height:100%; border:none" ></iframe>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
