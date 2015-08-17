<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_MapOff.aspx.cs" Inherits="WebApplication4.WebForm_off" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="StyleSheet1.css" />
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link rel="stylesheet" type="text/css" href="css/buttonstyle.css" />
    <script src="Scripts/jquery-1.7.1.js"></script>
    <script type="text/javascript" src="js/MapFunction.js"> </script>

    <%--<script type="text/javascript">
        var open = true;
        //[定义一个控制右侧显示的tap]
        try {
            onoffline("online");
            TabControl.prototype = new BMap.Control();
        }
        catch(err)
        {
            onoffline("offline");
        }

        TabControl.prototype.initialize = function (map) {
            var div = document.createElement("div");
            var img = document.createElement("img");
            img.src = "image/tab.png";
            div.appendChild(img);
            div.style.cursor = "pointer";
            div.style.background = "transparent";
            div.onclick = function (e) {
                var aa = document.getElementById("infoZone");
                if (aa.style.display != "none") {
                    aa.style.display = "none";
                    img.src = "image/tab_.png";
                    jd.setOffset(new BMap.Size(document.body.clientWidth / 2 - 300 + 100, document.body.clientHeight / 2 - 57));
                }
                else {
                    img.src = "image/tab.png";
                    aa.style.display = "";
                    jd.setOffset(new BMap.Size(document.body.clientWidth / 2 - 300, document.body.clientHeight / 2 - 57));
                }
            }
            map.getContainer().appendChild(div);
            return div;
        }
        //[end]
    </script>--%>
    <script type="text/javascript">
        $(function () {
            //菜单隐藏展开
            var tabs_i = 0;
            var open = true;
            $('.vtitle').click(function () {
                var _self = $(this);

                var j = $('.vtitle').index(_self);
                if (tabs_i == j) {
                    if (open == true) {
                        open = false;

                        $('.vcon').slideUp();
                        $('em', _self).removeClass('v v02').addClass('v v01');
                    }
                    else {
                        open = true;

                        $('em', _self).removeClass('v v01').addClass('v v02');
                        $('.vcon').eq(tabs_i).slideDown();
                    }

                    return false;
                }

                tabs_i = j;
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
        * {
            margin: 0;
            padding: 0;
            list-style-type: none;
        }

        a, img {
            border: 0;
        }

        body {
            font: 12px Arial, Helvetica, sans-serif, "新宋体";
        }

        a, a:hover {
            text-decoration: none;
        }

        /*收缩菜单*/
        .v {
            float: right;
            width: 14px;
            height: 12px;
            overflow: hidden;
            background: url(image/vicon.png) no-repeat;
            display: inline-block;
            margin-top: -5px;
            margin-bottom: -5px;
        }

        .v01 {
            background-position: 0 0;
        }

        .v02 {
            background-position: 0 -16px;
           
        }

        .vtitle {
            height: 35px;
            background: #fbede0;
            line-height: 35px;
            border: 1px solid #ccb6a9;
            margin-top: -1px;
            padding-left: 20px;
            font-size: 15px;
            color: #4d4d4d;
            font-family: "\5FAE\8F6F\96C5\9ED1";
            cursor: pointer;
        }

            .vtitle em {
                margin: 10px 10px 0 0;
            }

        .vconlist {
            background: #9cc;
        }

            .vconlist li a {
                height: 30px;
                line-height: 45px;
                padding-left: 30px;
                display: block;
                font-size: 15px;
                color: #866f67;
                font-family: "\5FAE\8F6F\96C5\9ED1";
            }
    </style>
    <style type="text/css">
        #test {
            width: 43px;
        }

        #Text1 {
            width: 52px;
        }

        #Text2 {
            width: 22px;
        }

        #ttb_setgirdSize {
            width: 50px;
        }

        #tb_setgirdSize {
            width: 46px;
        }
    </style>

</head>
<body onload="setHeight()" onresize="setHeight()" style="margin: 0px; padding: 0px; height: 100%; background-color: #0b7fab; font-family: STXihei; font-size: 15px">
    <form id="form1" runat="server">
        <div style="margin: 0px; padding: 0px;">
            <table id="mapzone" style="border: hidden; border-color: red;">
                <tr>
                    <td style="background-color: blue; margin: 0px; border: 0px; padding: 0px;">
                        <div id="allmap" style="width: 100%; height: 100%; margin: 0px; background-color: white; padding: 0px; background-image:url('image/baidu.jpg')" >
                            无法连接地图服务器,请检查互联网状态
                        </div>
                    </td>
                    <td id="infoZone" style="background-color: #0b7fab; vertical-align: top; width: 250px;">
                        <div id="infoDiv" style="background-color: #54b1d2; padding: 5px; height: 100%; overflow: auto;">
                            <div id="div1" style="font-family: STXihei; font-size: 17px; font-weight: bold; text-align: center">人防工事信息(离线) </div>
                            当前用户数:
                             <input id="peoplesum" class="input_text1" type="text" value="" readonly="true" /><br />
                            已经入工事人员:
                             <input id="peoplein" class="input_text1" type="text" value="" readonly="ture" /><br />
                            <div class="vtitle"><em class="v v02"></em>人防信息显示</div>
                            <div class="vcon">
                                <ul class="vconlist clearfix">
                                    <li>
                                        <div id="info" style="height: 130px; overflow: auto; background-color: #fff; margin: 1px;"></div>
                                    </li>
                                    <li>
                                        <a>
                                            <input id="btn444444" type="button" class="button orange small" onclick="getRFList()" value="获取人防工事信息" />
                                        </a></li>
                                    <li>
                                        <a>
                                            <input id="btn3" type="button" class="button orange small" onclick="updateRFinfo()" value="更新人防工事信息" />
                                            <input id="Checkbox_RF" type="checkbox" onclick="checkRFupdate()" />自动更新 </a>
                                    </li>
                                    <li><a></a></li>
                                </ul>
                            </div>
                           
                            <div class="vtitle"><em class="v"></em>用户信息显示</div>
                            <div class="vcon" style="display: none;">
                                <ul class="vconlist clearfix">
                                    <li>
                                        <div id="userinfo" style="height: 130px; overflow: auto; background-color: #fff; margin: 1px;"></div>
                                        <div id="userinfocontrol"style="text-align: center; font-size: 6px; visibility:hidden">
                                            <span id="last" onclick="LastPage()" style="cursor:pointer" ><<</span><input id="nowpageindex" class="input_text3"   style="width:20px; text-align:right" type="text"  value="1" readonly="ture" />/
                                            <input id="peoplenumsum" class="input_text3"   style="width:20px"  type="text" value="" readonly="ture" />页<span id="next"  onclick=" NextPage()"  style="cursor:pointer">>></span>
                                            跳转第<input id="jumptoindex" style="width:20px" type="text" />页<input id="btn_jump" class="button orange small" onclick="addUseronmapbyjump()" type="button" value="确定" />  
                                        </div>
                                    </li>
                                    <li>
                                        <a>
                                            <input id="Radio1" type="radio"  name="identity"  value="全部用户"  checked="checked"   onclick="setUserType(0)"/> 全部用户
                                            <input id="Radio2" type="radio" name ="identity" value="工作人员" onclick="setUserType(1)"/> 工作人员
                                             </a>
                                        <a>
                                            <input id="btn_userloactionf5" class="button orange small" onclick="getUserLocationandrenew();" type="button" value="显示人员位置信息" />
                                             <input id="Checkbox_people" type="checkbox" onclick="checklocationupdate()" />自动更新
                                        </a>
                                    </li>
                                     <li>
                                        <a>
                                            <input id="btn_check_login" class="button orange small" onclick="check_login()" type="button" value="检查用户超时" />
                                       </a>
                                    </li> 
                                    <li>
                                        <a>
                                            <input id="btn44" type="button" class="button orange small" onclick=" getdiction()" value="指派" /> 
                                               <input id="btn_stop" type="button" value="取消" class="button orange small" onclick="set_stop()" />   
                                             <input id="jd" class="input_text3"   style="width:40px"  type="text" value="0" readonly="ture" />
                                        </a>
                                    </li>
                                      <li>
                                        <a>
                                            <input id="btn_sendmessage" type="button" value="发送短信" class="button orange small" onclick="callws()" />
                                        </a>
                                    </li>
                                    <li><a></a></li>
                                </ul>
                            </div>
                             <div class="vtitle"><em class="v"></em>大数据测试</div>
                            <div class="vcon" style="display: none;">
                                <ul class="vconlist clearfix">
                                    <li>
                                         <a>
                                            <input id="btn_creatuser" type="button" class="button orange small" value="自动生成用户" onclick="adduser()" />
                                                </a>
                                    </li>
                                        <li><a></a></li>
                                </ul>
                            </div>
                            <asp:HiddenField ID="wsurl" runat="server" />
                            <asp:HiddenField ID="dataurl" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <div class="wait" id="wait" style="visibility: hidden; height: 80px; width: 200px; background-color: #687085; text-align: center; padding-top: 30px; color: white">
        <img alt="" src="image/loading.gif" /><br />
        Loading.....
    </div>


</body>
</html>







