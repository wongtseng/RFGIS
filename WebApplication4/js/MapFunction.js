var _wsurl = "";
var points = [];
var _AllMarker = [];//全局变量,保存当前地图上的所有markers;
var _AllMarkerID = [];//全局变量,保存当前地图上的所有markers的UUID,方便查询
var _AllLocationMarker = [];//全局变量,保存当前地图上的所有用户marker,方便查询
var _AllLocationMarkerID = [];//全局变量,保存当前地图上的所有用户marker的UUID,方便查询
var _AllEnMarker = [];//全局变量,保存当前地图上的所有用户marker,方便查询
var _AllEnMarkerID = [];//全局变量,保存当前地图上的所有用户marker的UUID,方便查询
var circles = [];
var thismarker;
var thistitle;
var kk;
var bool_offline = "false";
var static_point_num = 50;
var map;
var bool_addmarkers = "false";
var wsvalue = null;//全局变量，记录ws返回值
var wsonevalue = null;
var isie = "true";
var mappoint = []; //保存地图上全部marker,用以控制显示范围
var xmlhttp = null;
var show_check_station = "false";//是否显示监测范围？
var show_type = "1";//显示的方式?0:单个，1：全部
var firstload = 0;
var map;  // 创建Map实例
var images = [];
var imagesMessage = [];
var imageindex = 0;
var myZoomCtrl;
var t_RF; //  定时器-更新人防工事
var t_people;  // 定时器-更新人员位置
var t_jd;
var caminfo=null;//入口的视频信息;
var markerClusterer = null;
var showpeople = false;
var User_Page_Num = 0;//目前用户总的分页数
var creatusernum =10000;//500个用户
var creatednum = 0;
var pointCollection;
var heatmapOverlay;
var userType = 0;

function ZoomControl() {
    // 默认停靠位置和偏移量
    this.defaultAnchor = BMAP_ANCHOR_TOP_RIGHT;
    this.defaultOffset = new BMap.Size(5, 45);
}

function ZoomControl2() {
    // 默认停靠位置和偏移量
    this.defaultAnchor = BMAP_ANCHOR_TOP_RIGHT;                                                     //200/80
    this.defaultOffset = new BMap.Size(document.documentElement.clientWidth / 2-200, document.documentElement.clientHeight / 2-40);
}

function setHeight() {
    var windowsHeight = document.documentElement.clientHeight;
    document.getElementById("mapzone").style.height = windowsHeight + "px";
    _wsurl = document.getElementById('wsurl').value;
   // alert(_wsurl);
   // document.getElementById("infoDiv").style.height = windowsHeight + "px";
    if (firstload == 0) {
        firstload = 1;
        try {
            map = new BMap.Map("allmap");            // 创建Map实例
         //   var point = new BMap.Point(116.404, 39.915);    // 创建点坐标
          //  map.centerAndZoom(point, 15);                     // 初始化地图,设置中心点坐标和地图级别。
            map.enableScrollWheelZoom();
            map.centerAndZoom(new BMap.Point(118.76224, 32.078586), 11);//直接定位到南京
            // 定义一个控件类,即function
            // 通过JavaScript的prototype属性继承于BMap.Control
            ZoomControl.prototype = new BMap.Control();
            ZoomControl.prototype.initialize = function (map) {
                // 创建一个DOM元素  
                var div = document.getElementById("div_map_tools");
                // 添加DOM元素到地图中
                map.getContainer().appendChild(div);
                // 将DOM元素返回
                return div;
            }
            var myZoomCtrl = new ZoomControl();
            // 添加到地图当中
            map.addControl(myZoomCtrl);
            ////
            ZoomControl2.prototype = new BMap.Control();
            ZoomControl2.prototype.initialize = function (map) {
                // 创建一个DOM元素  
                var div = document.getElementById("wait");
                // 添加DOM元素到地图中
                map.getContainer().appendChild(div);
                // 将DOM元素返回
                return div;
            }
            myZoomCtrl = new ZoomControl2();
            // 添加到地图当中
            map.addControl(myZoomCtrl);
            ////
            map.addControl(new BMap.NavigationControl());  //添加默认缩放平移控件
            map.addControl(new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT }));
            map.addControl(new BMap.MapTypeControl({ mapTypes: [BMAP_NORMAL_MAP, BMAP_HYBRID_MAP] }));
            map.addControl(new BMap.OverviewMapControl({ isOpen: true }));
            //  GetSumInfo_SOAP();
            //document.getElementById("div_map_tools").style.visibility = "visivle";
            //[soap 调用webservice,获取查询的人防工事简要信息,用以在地图上添加点以及生成列表]
            map.addEventListener("tilesloaded", getRFListLater());
        }
        catch (err) {
            console.log(err.message);
            document.getElementById("div_map_tools").style.visibility = "hidden";
            document.getElementById("allmap").style.backgroundImage = "url('image/baidu.jpg')";
           
            window.location.href = "_Mapoff.aspx";
            //document.getElementById("allmap").style.lineHeight = windowsHeight+"px";
        }
        finally
        {
           
        }
        var mytabCtrl = new TabControl();
        map.addControl(mytabCtrl);
      
    }
    else {
        try {
            if (map == null) {
                document.getElementById("div_map_tools").style.visibility = "hidden";
                document.getElementById("allmap").style.backgroundImage = "url('image/baidu.jpg')";
            }
            map.removeControl(myZoomCtrl);
            ZoomControl2.prototype = new BMap.Control();
            ZoomControl2.prototype.initialize = function (map) {
                // 创建一个DOM元素  
                var div = document.getElementById("wait");
                // 添加DOM元素到地图中
                map.getContainer().appendChild(div);
                // 将DOM元素返回
                return div;
            }
            myZoomCtrl = new ZoomControl2();
            // 添加到地图当中
            map.addControl(myZoomCtrl);

        }
        catch (err)
        {
            if (err.message.indexOf("removeControl") > 0)
            {
                console.log("offline!");

            }

        }


                

    }
  
}

//[根据返回值设定人防信息列表]
function set_list_info(markersinfo) {
    if (markersinfo != "null") {
        var list_marker = markersinfo.split('#');
        var s = [];
        s.push('<div style="font-family: arial,sans-serif; border: 0px; font-size: 12px;">');
        s.push('<ol style="list-style: none outside none; padding: 0pt; margin: 0pt;">');
        //alert("调用了");
        for (var i = 0; i < list_marker.length; i++) {               
            var marker_info = (list_marker[i].split('@')[0]).split('&');    ////检测点数组,保存每一个检测点的信息,格式说明:    0:UUID 1:人防工事名称 2:经度 3:纬度  4:库容; 5剩余库容                
            s.push('<li class="markerslist" id="list' + i + '" style="margin: 2px 0pt; padding: 0pt 5px 0pt 3px; cursor: pointer; overflow: hidden; line-height: 17px;' + 'selected' + '" onclick="setinfobyselectlist(+' + i + ')">');
            s.push('<span style="width:1px;padding-left:0px;margin-right:5px"> <\/span>');
            s.push('<span style="color:#00c;"><b>' + (i + 1) + '<\/b>' + '<\/span>');
            s.push('<span style="color:#666;"> - ' + marker_info[1] + '<\/span><br/>');
            //根据库容要调用服务去报警
            //根据剩余库容情况改变颜色  目前  正常= 绿色 ：#32CD32     接近库容= 红色： #EE2C2C
            if (marker_info[5]>=0)
                s.push('<span style="color:#32CD32";>库容: ' + marker_info[4] + '&nbsp&nbsp 剩余库容:' + marker_info[5] + '<\/span><br/>');
            else
                s.push('<span style="color: #EE2C2C";>库容: ' + marker_info[4] + '&nbsp&nbsp 剩余库容:' + marker_info[5] + '<\/span><br/>');
            s.push('<\/li>');
            s.push('');
        }
        s.push('<\/ol><\/div>');
    }
    else {
        var s = []; 
        s.push('<div style="font-family: arial,sans-serif; border: 1px solid rgb(153, 153, 153); font-size: 12px;overflow:auto;">没有找到相应结果.<\/div>');
    }
    document.getElementById("info").innerHTML = s.join("");
    //  document.getElementById("peopleinfo").innerHTML = s.join("");
    // the element we want to apply the jScrollPane
}

//[根据返回值设定用户信息列表]
function set_list_userinfo(markersinfo) {
    document.getElementById("userinfo").innerHTML = "";
    if (markersinfo != "null") {
        var list_marker = markersinfo.split('@');
      
        var s = [];
        s.push('<div style="font-family: arial,sans-serif; border: 0px; font-size: 12px;">');
        s.push('<ol style="list-style: none outside none; padding: 0pt; margin: 0pt;">');
        //alert("调用了");
        for (var i = 0; i < list_marker.length; i++) {
            var marker_info = list_marker[i].split(',')[0];    ////检测点数组,保存每一个检测点的信息,格式说明:    0:UUID 1:人防工事名称 2:经度 3:纬度  4:库容; 5剩余库容                
            s.push('<li class="markerslist" id="list' + i + '" style="margin: 2px 0pt; padding: 0pt 5px 0pt 3px; cursor: pointer; overflow: hidden; line-height: 17px;' + 'selected' + '" onclick="setuserinfobyselectlist(+' + marker_info.split('&')[3] + ')">');
            s.push('<span style="width:1px;padding-left:0px;margin-right:5px"> <\/span>');
            s.push('<span style="color:#00c;"><b>' + (i + 1) + '<\/b>' + '<\/span>');
            s.push('<span style="color:#666;"> - 用户名：' + marker_info.split('&')[0] + '<\/span><br/>');
            s.push('<\/li>');
            s.push('');
        }
        s.push('<\/ol><\/div>');
    }
    else {
        var s = [];
        s.push('<div style="font-family: arial,sans-serif; border: 1px solid rgb(153, 153, 153); font-size: 12px;overflow:auto;">没有找到相应结果.<\/div>');
    }
    document.getElementById("userinfo").innerHTML = s.join("");
    //document.getElementById("peopleinfo").innerHTML = s.join("");
    // the element we want to apply the jScrollPane

}


//[点击list后显示指定的点]
function setinfobyselectlist(index) {
    get_one_info(_AllMarker[index]);
    // map.panTo(thismarker.getPosition());
}


function setuserinfobyselectlist(id) {

    var num = _AllLocationMarkerID.length;
    for (var i = 0; i < num; i++)
    {
        if (_AllLocationMarkerID[i] == id)
        {
            thismarker = _AllLocationMarker[i];
            break;
        }
    }
    getUserInfo(id);
    
    // map.panTo(thismarker.getPosition());
}
//处理Webservice返回值,获取用户位置信息
function getlocation(wsvalue) {
    set_list_userinfo(wsvalue);
    var markers = [];
    //  _AllMarker = [];
    //  _AllMarkerID = [];
    _AllLocationMarker = [];
    _AllLocationMarkerID = [];
    if (wsvalue != null) {
        if (wsvalue != "null"&&wsvalue!="false") {
            mappoint = [];
            // set_list_info(wsvalue);
            markers = wsvalue.split("@");
            var points = [];//检测点数组,保存每一个检测点的信息,格式说明:    0:UUID 1:人防工事名称 2:经度 3:纬度  4:库容; 5剩余库容
            var vvvv;
                     
            var myIcon = new BMap.Icon("http://api.map.baidu.com/img/markers.png", new BMap.Size(23, 25), {
                offset: new BMap.Size(10, 25), // 指定定位位置  
                imageOffset: new BMap.Size(0, 0 - 10 * 25) // 设置图片偏移  
            });
            for (var i = 0; i < markers.length; i++) {
                points = markers[i].split("&");//        0:用户名称 1:经度 2:纬度 3:ID  
                //新建marker
                // var marker = new BMap.Marker(new BMap.Point(points[0], points[1]));
                var marker = new BMap.Marker(new BMap.Point(points[1], points[2]), { icon: myIcon });
                //设置label
                var label = new BMap.Label(points[0], { offset: new BMap.Size(20, 8) });//设定以什么内容建立label
                label.setStyle({ backgroundColor: "wheat", fontSize: "12px", borderColor: "black" })
                label.setTitle("经度:" + points[1] + "  纬度:" + points[2]);//设定以什么内容建立title,目前是经纬度
                marker.setLabel(label);
                //添加到_AllMarker数组
                    
                _AllLocationMarker.push(marker);
                //记录marker的UUID,方便查询
                //  _AllMarkerID.push(points[0])
            
                _AllLocationMarkerID.push(points[3]);
                mappoint.push(marker.getPosition());
                //为marker指定点击事件
                marker.addEventListener("click", function () { get_one_Location_info(this) });
                //添加marker到地图上
            map.addOverlay(marker);
            }
            //markerClusterer = new BMapLib.MarkerClusterer(map);
            //markerClusterer.setGridSize(60);
            //markerClusterer.addMarkers(_AllLocationMarker);
       
            // map.setViewport(mappoint);
        }
        else {
           // set_list_info(wsvalue);
        }
    }


}
//[end]

function addMarker(point) {
    var myIcon = new BMap.Icon("http://api.map.baidu.com/img/markers.png", new BMap.Size(23, 25), {
        offset: new BMap.Size(10, 25), // 指定定位位置  
        imageOffset: new BMap.Size(0, 0 - 10 * 25) // 设置图片偏移  
    });
    var marker = new BMap.Marker(point, { icon: myIcon });
    _AllLocationMarker.push(marker);
    map.addOverlay(marker);
}
//处理Webservice返回值,获取工事简要信息
function getsuminfo(wsvalue) {
   // alert("getsuminfo" + wsvalue);
    var markers = [];
    _AllMarker = [];
    _AllMarkerID = [];
    // map.removeControl(jd);
    if (wsvalue != null) {
        if (wsvalue != "null") {
            mappoint = [];
           // alert("276");
            set_list_info(wsvalue);
         
            markers = wsvalue.split("#");
            var points = [];//检测点数组,保存每一个检测点的信息,格式说明:    0:UUID 1:人防工事名称 2:经度 3:纬度  4:库容; 5剩余库容;6 状态
            var vvvv;
            for (var i = 0; i < markers.length; i++) {
                points = markers[i].split("&");//        0:UUID 1:监控点名称 2:经度 3:纬度  
        
               
             //   alert(points[6]);     根据不同状态更改标记的颜色
                if (points[6] =='1')
                    var myIcon = new BMap.Icon("image/red.png", new BMap.Size(32, 32));
                else
                    var myIcon = new BMap.Icon("image/green.png", new BMap.Size(32, 32));

               // var marker = new BMap.Marker(pt, { icon: myIcon });
                var marker = new BMap.Marker(new BMap.Point(points[2], points[3]), {icon:myIcon});

                //设置label
                var label = new BMap.Label(points[1], { offset: new BMap.Size(32, 8) });//设定以什么内容建立label
                label.setStyle({ backgroundColor: "wheat", fontSize: "12px", borderColor: "black" })
                label.setTitle("经度:" + points[2] + "  纬度:" + points[3]);//设定以什么内容建立title,目前是经纬度
                marker.setLabel(label);
                //添加到_AllMarker数组
                _AllMarker.push(marker);
                //记录marker的UUID,方便查询
                _AllMarkerID.push(points[0]);
                mappoint.push(marker.getPosition());
                //为marker指定点击事件
                marker.addEventListener("click", function () { get_one_info(this) });
                //添加marker到地图上
                map.addOverlay(marker);
            }
            map.setViewport(mappoint);
        }
        else {
            set_list_info(wsvalue);
       
        }
    }
}
//[end]


//处理Webservice返回值,获取入口简要信息
function getensuminfo(wsvalue) {
    var markers = [];
    _AllEnMarker = [];
    _AllEnMarkerID = [];
    // map.removeControl(jd);
    if (wsvalue != null) {
        if (wsvalue != "none") {
            mappoint = [];
            //set_list_info(wsvalue);
            markers = wsvalue.split("#");
            var points = [];//检测点数组,保存每一个检测点的信息,格式说明:    0: ID 1:人防入口名称 2:经度 3:纬度   4:损毁程度
            var vvvv;
            for (var i = 0; i < markers.length; i++) {
                points = markers[i].split("&");//       0: ID 1:人防入口名称 2:经度 3:纬度  4:损毁程度

                //新建marker
                //var marker = new BMap.Marker(new BMap.Point(points[2], points[3]));      //Gplaces
                //设置不同损毁程度入口对应的图标
                var pt = new BMap.Point(points[2], points[3]);
               // alert(points[4])
                if (points[4]>1)
                var myIcon = new BMap.Icon("image/note.png", new BMap.Size(32, 32));
              else
                var myIcon = new BMap.Icon("image/pin.png", new BMap.Size(32, 32));
                var marker = new BMap.Marker(pt, { icon: myIcon });

                //设置label
                var label = new BMap.Label(points[1], { offset: new BMap.Size(32, 8) });//设定以什么内容建立label
                label.setStyle({ backgroundColor: "wheat", fontSize: "12px", borderColor: "black" })
                label.setTitle("经度:" + points[2] + "  纬度:" + points[3]);//设定以什么内容建立title,目前是经纬度
                marker.setLabel(label);
                //添加到_AllMarker数组
                _AllEnMarker.push(marker);
                //记录marker的UUID,方便查询
                _AllEnMarkerID.push(points[0]);
                mappoint.push(marker.getPosition());
                //为marker指定点击事件
                marker.addEventListener("click", function () { get_one_en_info(this) });
                //添加marker到地图上
                map.addOverlay(marker);
            }
           // map.setViewport(mappoint);
        }
        else {
           // set_list_info(wsvalue);
        }
    }
}
//[end]

//点击一个点查询并显示信息

//[获取特定点marker信息]
function get_one_info(marker) {
    // map.addControl(jd);
    for (var i = 0; i < _AllMarker.length; i++) {
        if (_AllMarker[i] == marker) {
            thismarker = marker;
            getpicmessage(_AllMarkerID[i]);
            getinfo(_AllMarkerID[i]);
    
    
            break;
        }
    }
}
//[end]

//[获取特定入口点marker信息]
function get_one_en_info(marker) {
    // map.addControl(jd);
    for (var i = 0; i < _AllEnMarker.length; i++) {
        if (_AllEnMarker[i] == marker) {
            thismarker = marker;
            //获取该marker的入口信息
           // alert(_AllEnMarkerID[i]);
            getenpicmessage(_AllEnMarkerID[i]);
            geteninfo(_AllEnMarkerID[i]);
            getcaminfo(_AllEnMarkerID[i]);
            break;
        }
    }
}
//[end]

//[获取特定用户信息]
function get_one_Location_info(marker) {
    // map.addControl(jd);
    for (var i = 0; i <_AllLocationMarker.length; i++) {
        if (_AllLocationMarker[i] == marker) {
            thismarker = marker;
            getUserInfo(_AllLocationMarkerID[i]);
            break;
        }
    }
}

function piclast()
        {          
            if (imageindex != 0)
            {
                imageindex = imageindex - 1;
                document.getElementById('pic').src = "pic/" + images[imageindex];
                document.getElementById('pica').href = "pic/" + images[imageindex];
                document.getElementById('td_imagemessage').textContent = imagesMessage[imageindex];
            }        
        }

function picnext() {            
    var i = images.length;
    if (imageindex < i-1)
    {
        imageindex = imageindex + 1;
        document.getElementById('pic').src = "pic/" + images[imageindex];
        document.getElementById('pica').href = "pic/" + images[imageindex];
        document.getElementById('td_imagemessage').textContent = imagesMessage[imageindex];
    }
            
    //  
}

function showwait()
{
    document.getElementById("wait").style.visibility = "visible";
}

function hiddenwait() {
    document.getElementById("wait").style.visibility = "hidden";
}

function TabControl() {
    try {

        this.defaultAnchor = BMAP_ANCHOR_TOP_RIGHT;
        this.defaultOffset = new BMap.Size(0, document.body.clientHeight / 2 - 37);
    }
    catch (err)
    {

    }


 
}


//获取某个人防工事信息后设置信息框
function getoneinfo(wsonevalue) {

    //状态为200说明调用成功，500则说明出错
    //alert(xmlhttp.status);
    // map.removeControl(jd);
    images = null;
    imageindex = 0;
    if (wsonevalue != null) {
        var info = [];
    // alert("509-"+wsonevalue);
        map.setZoom(30);
        info = wsonevalue.split('@')[2].split('&');
        var opts = {
            width: 500, // 信息窗口宽度
            height: 110,// 信息窗口高度
            //title: "测点信息"  // 信息窗口标题
            enableMessage:false     //去掉发送到手机
        }
        //
        infohtml = '<table width="100%" height="60px" align="center" border="0" style="font-size:16px;">';
        infohtml += '<tr>';
        infohtml += '<td width="40px">编号:</td>';
        infohtml += ' <td >' + info[4] + '</td>';
        infohtml += '<td width="40px">名称:</td>';
        infohtml += '<td>' + info[1] + '</td>';
        infohtml += '</tr>';
        infohtml += '<tr>';
        infohtml += '<td>经度:</td>';
        infohtml += '<td>' + info[2] + '</td>';
        infohtml += '<td>纬度:</td>';
        infohtml += '<td>' + info[3] + '</td>';
        infohtml += '</tr>';
        infohtml += '</table>';
        infohtml += '<br/>';
        if (info[11] != "NONE") {
       
            images = info[11].split('$');
          //alert("537-"+info[11]);
        }
        else {
            images = ["no.jpg"];
            imagesMessage[0] = "暂无";
        }

        var infoWindow = new BMap.InfoWindow(infohtml, opts);  // 创建信息窗口对象
        infoWindow.disableAutoPan();
        infoWindow.setTitle("<h4 style='margin:0 0 5px 0;padding:0.2em 0'>人防工事详细信息</h4>");
        var maxinfo = '<table width=100%  border="1" cellspacing="0" style="font-size:13px;">';
        maxinfo += '<tr>';
        maxinfo += '<td width="20px">编号:</td>';
        maxinfo += '<td width="60px">' + info[4] + '</td>';
        maxinfo += '<td width="20px">名称:</td>';
        maxinfo += '<td width="60px">' + info[1] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>经度:</td>';
        maxinfo += '<td>' + info[2] + '</td>';
        maxinfo += '<td>纬度:</td>';
        maxinfo += '<td>' + info[3] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>库容:</td>';
        maxinfo += '<td>' + info[5] + '</td>';
        maxinfo += '<td>进入人数:</td>';
        maxinfo += '<td>' + info[6] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>剩余库容:</td>';
        maxinfo += '<td>' + info[7] + '</td>';
        maxinfo += '<td>损毁情况</td>';
        maxinfo += '<td>' + info[8] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>状态:</td>';
        maxinfo += '<td>' + info[9] + '</td>';
        maxinfo += '<td>扩充信息</td>';
        maxinfo += '<td>' + '   '+ '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>图片描述;</td>';
        maxinfo += '<td colspan="3" id="td_imagemessage">' + imagesMessage[0] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td colspan="4" style="text-align:center;"><a id=pica href="pic/' + images[0] + '" target="_blank" ><img id="pic"style="float:none ;margin:4px" src="pic/' + images[0] + '" width="250px"/></a> </td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td colspan="4" style="text-align:center;"><a  id=pica href="#" onclick="piclast()" title="上一张" >上一张</a>';
        maxinfo += '<a  id=pica href="#" onclick="picnext()" title="下一张" >下一张 </a></td>';
        maxinfo += '</tr>';
        maxinfo += '</table>';
        maxinfo += '</br>';
        maxinfo += '</br>';
        infoWindow.setMaxContent(maxinfo);
        infoWindow.enableMaximize();
        thismarker.openInfoWindow(infoWindow);
     
        map.panTo(infoWindow.getPosition());
      
        infoWindow.addEventListener("maximize", function () {
            map.panBy(0, 200);
           // map.panTo(infoWindow.getPosition());
            document.getElementById('pic').onload = function () {
                infoWindow.redraw();   //防止在网速较慢，图片未加载时，生成的信息框高度比图片的总高度小，导致图片部分被隐藏
               
           
            }
        });


    }

}
//end

function clearalluser()
{
    try
    {
        map.removeOverlay(pointCollection);
    }
    catch(e)
    {
    
    }
  
}


//显示 用户的分布态势
function showalluser()
{
    showwait();
    clearalluser()
    closeHeatmap();




    document.getElementById("userinfo").innerHTML = "人员分布态势图";
    document.getElementById("userinfocontrol").style.visibility = "hidden";
    if (_AllLocationMarker.length > 0) {
        for (var i = 0; i < _AllLocationMarker.length; i++) {
            map.removeOverlay(_AllLocationMarker[i]);
        }
    }
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetAllLoaction",
        data: "{}",
        dataType: 'json',
        success: function (result) {
            addMassPoint(result.d);
        }
    });
      //  document.getElementById("btn_userloactionf5").value = "隐藏人员位置信息";
     //   document.getElementById("btn_userloactionf5").value = "显示人员位置信息";
    //    document.getElementById("userinfocontrol").style.visibility = "hidden";
                                                           
}
//在地图上标出海量数据
function addMassPoint(strXML)
{

    //document.getElementById("btn_show_all_user").disabled = true;
    var struserlaction = strXML.split('@');
    if (document.createElement('canvas').getContext)
    {  // 判断当前浏览器是否支持绘制海量点
        var points = [];  // 添加海量点数据
        for (var i = 0; i < struserlaction.length; i++) 
        {
            var lng=struserlaction[i].split('&')[0];
            var lat=struserlaction[i].split('&')[1];
            points.push(new BMap.Point(lng,lat));
        }
        var options = {
            size: BMAP_POINT_SIZE_SMALLER,      //BMAP_POINT_SIZE_SMALLER       BMAP_POINT_SIZE_TINY
            shape: BMAP_POINT_SHAPE_CIRCLE,// BMAP_POINT_SHAPE_WATERDROP,    、BMAP_POINT_SHAPE_CIRCLE      //BMap_Symbol_SHAPE_STAR
            color: '#B22222'
        }
        pointCollection = new BMap.PointCollection(points, options);  // 初始化PointCollection
        pointCollection.addEventListener('click', function (e) {                   //mouseover                   click
            alert('当前用户的坐标为：' + e.point.lng + ',' + e.point.lat);  // 监听点击事件
        });
    
        hiddenwait();
        map.addOverlay(pointCollection);  // 添加Overlay
      
    }
    else
    {
        alert('请在chrome、safari、IE8+以上浏览器查看本示例');
    }



}



//显示入口详细信息
function getoneeninfo(wsonevalue) {

    //状态为200说明调用成功，500则说明出错
    //alert(xmlhttp.status);
    // map.removeControl(jd);
    images = null;
    imageindex = 0;
   // alert("624-" + wsonevalue);
    if (wsonevalue != null) {
        var info = [];
    
        map.setZoom(30);
        info = wsonevalue.split('@')[2].split('&');
        var opts = {
            width: 500, // 信息窗口宽度
            height: 110,// 信息窗口高度
            //title: "测点信息"  // 信息窗口标题
            enableMessage: false     //去掉发送到手机
        }
        //
        infohtml = '<table width="100%" height="60px" align="center" border="0" style="font-size:16px;">';
        infohtml += '<tr>';
        infohtml += '<td width="40px">编号:</td>';
        infohtml += ' <td >' + info[1] + '</td>';
        infohtml += '<td width="40px">名称:</td>';
        infohtml += '<td>' + info[2] + '</td>';
        infohtml += '</tr>';
        infohtml += '<tr>';
        infohtml += '<td>经度:</td>';
        infohtml += '<td>' + info[3] + '</td>';
        infohtml += '<td>纬度:</td>';
        infohtml += '<td>' + info[4] + '</td>';
        infohtml += '</tr>';
        infohtml += '</table>';
        infohtml += '<br/>';

        if (info[8] != "NONE") {

            images = info[8].split('$');
        }
        else {
            images = ["no.jpg"];
            imagesMessage[0] = "暂无";
        }

        var infoWindow = new BMap.InfoWindow(infohtml, opts);  // 创建信息窗口对象
        infoWindow.disableAutoPan();
        infoWindow.setTitle("<h4 style='margin:0 0 5px 0;padding:0.2em 0'>人防入口详细信息</h4>");
        var maxinfo = '<table width=100%  border="1" cellspacing="0" style="font-size:13px;">';
        maxinfo += '<tr>';
        maxinfo += '<td width="20px">编号:</td>';
        maxinfo += '<td width="60px">' + info[1] + '</td>';
        maxinfo += '<td width="20px">名称:</td>';
        maxinfo += '<td width="60px">' + info[2] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>经度:</td>';
        maxinfo += '<td>' + info[3] + '</td>';
        maxinfo += '<td>纬度:</td>';
        maxinfo += '<td>' + info[4] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>所属工事:</td>';
        maxinfo += '<td>' + info[6] + '</td>';
        maxinfo += '<td>受损程度:</td>';
        maxinfo += '<td>' + info[7] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>损毁情况:</td>';
        maxinfo += '<td colspan="3" >' + info[5] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>图片描述;</td>';
        maxinfo += '<td colspan="3" id="td_imagemessage">' + imagesMessage[0] + '</td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td colspan="4" style="text-align:center;"><a id=pica href="pic/' + images[0] + '" target="_blank" ><img id="pic"style="float:none ;margin:4px" src="pic/' + images[0] + '" width="250px"/></a> </td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td colspan="4" style="text-align:center;"><a  id=pica href="#" onclick="piclast()" title="上一张" >上一张</a>';
        maxinfo += '<a  id=pica href="#" onclick="picnext()" title="下一张" >下一张 </a></td>';
        maxinfo += '</tr>';
        maxinfo += '<tr>';
        maxinfo += '<td>监控视频:</td>';
        maxinfo += '<td colspan="3" id="td_cam">';
       // alert(caminfo);
        if (caminfo != null&&caminfo!="none")
        {
            var caminfos = caminfo.split('#');
            var camnum = caminfos.length;
            for (var i = 0; i < camnum; i++) {
                var cams = caminfos[i].split('&');
                maxinfo += '<a  href=' + cams[1] + '  target="_blank" ><img  src="image/webcam.png"/ title=' + cams[0]+ '></a> ';
            }
        }

       
        maxinfo +=  '</td>';
        maxinfo += '</tr>';
        maxinfo += '</table>';
        maxinfo += '</br>';
        maxinfo += '</br>';
        infoWindow.setMaxContent(maxinfo);
        infoWindow.enableMaximize();
        thismarker.openInfoWindow(infoWindow);

        map.panTo(infoWindow.getPosition());

        infoWindow.addEventListener("maximize", function () {
            map.panBy(0, 200);
            // map.panTo(infoWindow.getPosition());
            document.getElementById('pic').onload = function () {
                infoWindow.redraw();   //防止在网速较慢，图片未加载时，生成的信息框高度比图片的总高度小，导致图片部分被隐藏


            }
        });


    }

}

//显示手机用户位置与信息
function getoneUserinfo(wsonevalue) {

    //状态为200说明调用成功，500则说明出错
    // map.removeControl(jd);

    images = null;
    imageindex = 0;
    if (wsonevalue != null) {
        var info = [];
        info = wsonevalue.split('&');
        var opts = {
            width: 500, // 信息窗口宽度
            height: 110,// 信息窗口高度
            //title: "测点信息"  // 信息窗口标题
            enableMessage:false     //去掉发送到手机
        }
        //
        infohtml = '<table width="100%" height="60px" align="center" border="0" style="font-size:16px;">';
        infohtml += '<tr>';
        infohtml += '<td width="60px">用户名:</td>';
        infohtml += ' <td >' + info[0] + '</td>';
        infohtml += '<td width="60px">手机号:</td>';
        infohtml += '<td>' + info[3] + '</td>';
        infohtml += '</tr>';
        infohtml += '<tr>';
        infohtml += '<td>经度:</td>';
        infohtml += '<td>' + info[1] + '</td>';
        infohtml += '<td>纬度:</td>';
        infohtml += '<td>' + info[2] + '</td>';
        infohtml += '</tr>';
        infohtml += '</table>';
    //    infohtml += '<iframe src="http://auto.mop.com/data/column/type_4.html"/>';
        infohtml += '<br/>';

        var infoWindow = new BMap.InfoWindow(infohtml, opts);  // 创建信息窗口对象
        infoWindow.setTitle("<h4 style='margin:0 0 5px 0;padding:0.2em 0'>用户详细信息</h4>");
        thismarker.openInfoWindow(infoWindow);
        infoWindow.disableAutoPan();
        map.panTo(infoWindow.getPosition());
    }

}
//end

// 随机向地图添加标注
function addtestpoint() {


    if (markerClusterer != null)
        markerClusterer.clearMarkers();
    var num = document.getElementById("test").value;
    var bounds = map.getBounds();
    var sw = bounds.getSouthWest();
    var ne = bounds.getNorthEast();
    var lngSpan = Math.abs(sw.lng - ne.lng);
    var latSpan = Math.abs(ne.lat - sw.lat);
    var rymarkers = [];
    var datasetss = [];
    //  var heatmapOverlay = new BMapLib.HeatmapOverlay({ "radius": 30 });
    //   map.addOverlay(heatmapOverlay);
    for (var i = 0; i < num; i++) {
        var point = new BMap.Point(sw.lng + lngSpan * (Math.random() * 0.7), ne.lat - latSpan * (Math.random() * 0.7));
        rymarkers.push(new BMap.Marker(point));
        //   var data = '{"lng":' + point.lng + ',"lat":' + point.lat + ',"count":95}';
        //  datasetss.push(data);
        //  heatmapOverlay.addDataPoint(point.lng, point.lat, 55);
    }
    // map.addOverlay(rymarkers);
    markerClusterer = new BMapLib.MarkerClusterer(map);
    var gridsize = document.getElementById("tb_setgirdSize").value;
    if (gridsize != "")
        markerClusterer.setGridSize(gridsize);
    else
        markerClusterer.setGridSize(60);


    markerClusterer.addMarkers(rymarkers);

}
//end


// 生成用户
function adduser() {
  
    var bounds = map.getBounds();
    var sw = bounds.getSouthWest();
    var ne = bounds.getNorthEast();
    var lngSpan = Math.abs(sw.lng - ne.lng);
    var latSpan = Math.abs(ne.lat - sw.lat);
        var lng= sw.lng + lngSpan * (Math.random() * 0.7);
        var lat = ne.lat - latSpan * (Math.random() * 0.7);
        var username = "Test_User_Creat" + creatednum;
        var phonenum = "18061666425";
        var pws = "123456";
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: _wsurl + "/CreatUser",
            data: "{username:'" + username + "',psw:'" + pws + "',phonenum:'" + phonenum + "',lng:'" + lng + "',lat:'" + lat + "'}",
            dataType: "json",
            success: function (result) {
             
                if (creatednum < creatusernum)
                {
                    creatednum++;
                    adduser();
                }
               
            }
        });

}
//end


// 清除测试数据
function cleantestpoint() {
    markerClusterer.clearMarkers();
}
//end

function getPicMessage(message) {
    imagesMessage = message.split('&');
  
}

//[soap 调用webservice,刷新人防工事列表]
function updateRFinfo()
{
    //alert("up");
    getdataurl();
}
//[end]


function check_login() {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/check_login_time",    // /getdiction"        pageindex                  data: "{id:'" + id + "' }",
        data: "{}",
        dataType: 'json',
        success: function (result) {
        }
    });
}

//更新数据库里的内容
function updateRF(value) {
  
    value = encodeURIComponent(value);
    if (value.split('@')[0] == false) {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: _wsurl + "/GetAirDefenseInfoSum",
            data: "{}",
            dataType: 'json',
            success: function (result) {
                set_list_info(result.d);
                getPeople_in_num();
               
            }
        });

    }
    else {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: _wsurl + "/updateRFData",
            data: "{info:" + "'" + value + "'" + "}",
            dataType: 'json',
            success: function (result) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json",
                    url: _wsurl + "/GetAirDefenseInfoSum",
                    data: "{}",
                    dataType: 'json',
                    success: function (result) {
                        set_list_info(result.d);
                        getPeople_in_num();
                    }
                });
            }
        });


    }

    
}

//调用WS去更新各个人防工事点的信息
function getdataurl()
{
    console.log("getdataurl....");
    var dataurlstr = document.getElementById("dataurl").value;
    console.log(dataurlstr);
    var dataurl = dataurlstr.split('@');
    var num = dataurl.length;
    getPeople_in_num();
    var a_url = new Array[num];
    var a_id = new Array[num];
    for (var i = 0; i < num; i++)
    {
        var idurl = dataurl[i].split('$');
     //   var id = 12; //idurl[0];
        var wsurl = idurl[1];
        a_url[i] = idur[1];
        a_id[i] = idurl[0];
        if (a_url[i] != "" & a_url[i] != "无")
        {
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: _wsurl + "/getRFData",
                data: "{id:'" + a_id[i] + "'}",
                dataType: 'json',
                success: function (result) {
                    if (result.d.split('@')[0] == "true")
                    {
                        updateRF(result.d);  //更新数据库
                        $.ajax({
                            type: "POST",
                            contentType: "application/json",
                            url: _wsurl + "/Check_capacity",
                            data: "{}",
                            dataType: 'json',
                            success: function (result) {
                           // updateRF(result.d);  //更新数据库
                            }
                        });
                    }
                }
            });
           

        }

    }

      

}

//[soap 调用webservice,获取用户位置信息,用以在地图上添加点]
function checklocationupdate()
{
 
    if (document.getElementById("Checkbox_people").checked == true) {
       t_people = setInterval(function () {
           adduseronmap(userType);
        }, 5000);
    
    }
    else {
        clearInterval(t_people);
    }
}
//[end]

//定时更新人防信息
function checkRFupdate() {
 
    if (document.getElementById("Checkbox_RF").checked == true) {
        t_RF= setInterval(function () {
            getdataurl();
        }, 5000);
  
    }
    else {
     
        clearInterval(t_RF);
    }
}
//[end]

//显示人员位置在地图上
function adduseronmap( userType) {
 
    var pageindex = document.getElementById("nowpageindex").value;
    //$.ajax({
    //    type: "POST",
    //    contentType: "application/json",
    //    url: _wsurl + "/pagenum",
    //    data: "{}",
    //    dataType: 'json',
    //    success: function (result) {
    //        console.log("adduseronmap  "+resul.d);
    //        document.getElementById("peoplenumsum").value = result.d;
    //    }
    //});
    if (showpeople == true) {
        if (_AllLocationMarker.length > 0) {
            for (var i = 0; i < _AllLocationMarker.length; i++) {
                map.removeOverlay(_AllLocationMarker[i]);
            }
        }
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: _wsurl + "/GetLoactionbypageindex",    // /getdiction"        pageindex                  data: "{id:'" + id + "' }",
            data: "{pageindex:'" + pageindex + "', userType:'"+userType+"'}",
            dataType: 'json',
            success: function (result) {
                // alert("1124" + result.d);
                console.log(result.d);
                getlocation(result.d);
            }
        });
    }
    else {
        if (_AllLocationMarker.length > 0) {
            for (var i = 0; i < _AllLocationMarker.length; i++) {
                map.removeOverlay(_AllLocationMarker[i]);
            }
        }



    }
}


function set_stop()
{
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/setstop",    // /getdiction"        pageindex                  data: "{id:'" + id + "' }",
        data: "{torf:'true'}",
        dataType: 'json',
        success: function (result) {
            alert("已取消");
            document.getElementById('jd').value = "0";
          
        }
    });
}

function check_login() {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/check_login_time",    // /getdiction"        pageindex                  data: "{id:'" + id + "' }",
        data: "{}",
        dataType: 'json',
        success: function (result) {
        }
    });
}


//显示人员位置在地图上
function addUseronmapbyjump() {
    User_Page_Num=document.getElementById("peoplenumsum").value 
    var jumptoindex = document.getElementById("jumptoindex").value;
   // alert("1048-" + jumptoindex);
  //  alert("1049-" + User_Page_Num);
    if (jumptoindex != "" && parseInt(jumptoindex) > 0 && parseInt(jumptoindex) <= parseInt(User_Page_Num)) {
        //alert("1051");
        if (showpeople == true) {
            if (_AllLocationMarker.length > 0) {
                for (var i = 0; i < _AllLocationMarker.length; i++) {
                    map.removeOverlay(_AllLocationMarker[i]);
                }
            }
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: _wsurl + "/GetLoactionbypageindex",    // /getdiction"        pageindex                  data: "{id:'" + id + "' }",
                data: "{pageindex:'" + jumptoindex + "', userType:'" + userType + "'}",
                dataType: 'json',
                success: function (result) {
                    //  alert("1074" + result.d);
                    getlocation(result.d);
                    document.getElementById("jumptoindex").value = "";
                    document.getElementById("nowpageindex").value = jumptoindex;
                }
            });
        }
        else {
            if (_AllLocationMarker.length > 0) {
                for (var i = 0; i < _AllLocationMarker.length; i++) {
                    map.removeOverlay(_AllLocationMarker[i]);
                }
            }
        }
    }
    else {
        alert("输入的页面无效");
        document.getElementById("jumptoindex").value = "";
    }
   
}



//[获取用户位置并刷新]
function getUserLocationandrenew() {
 
    console.log("1209:clearalluser");
    clearalluser();
    console.log("1211:clearalluser end");
    var pageindex = document.getElementById("nowpageindex").value;
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/pagenum",
        data: "{userType:'"+userType+"'}",                              //      data: "{id:'" + id + "' }",
        dataType: 'json',
        success: function (result) {
        document.getElementById("peoplenumsum").value = result.d;
        }
    });

    if (showpeople == false) {
        showpeople = true;
       document.getElementById("userinfocontrol").style.visibility = "visible";
       document.getElementById("btn_userloactionf5").value = "隐藏人员位置信息";
    }
    else {
        document.getElementById("btn_userloactionf5").value = "显示人员位置信息";
        document.getElementById("userinfocontrol").style.visibility = "hidden";
        document.getElementById("userinfo").innerHTML = "";
        showpeople = false;
        document.getElementById("nowpageindex").value = "1";
    }
    console.log("userType="+userType);
    adduseronmap(userType);

    }
       
//[end]

function NextPage()
{
    var nowindex = document.getElementById("nowpageindex").value; //当前页数
    var allpage = document.getElementById("peoplenumsum").value;//总页数
   // alert("1127  " + nowindex + "   " + allpage);
    if (parseInt(nowindex) < parseInt(allpage))
    {
        nowindex = parseInt(nowindex);
        var pageindex = ++nowindex;
        document.getElementById("nowpageindex").value = pageindex;
      //  alert("1133  " + pageindex + "   " + document.getElementById("nowpageindex").value);
        if (_AllLocationMarker.length > 0) {
            for (var i = 0; i < _AllLocationMarker.length; i++) {
                map.removeOverlay(_AllLocationMarker[i]);
            }
        }
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: _wsurl + "/GetLoactionbypageindex",    //
            data: "{pageindex:'" + pageindex + "', userType:'" + userType + "'}",
            dataType: 'json',
            success: function (result) {
                console.log(result.d);
                getlocation(result.d);
            }
        });


    }

    
}

function LastPage() {
    var nowindex = document.getElementById("nowpageindex").value;
    nowindex = parseInt(nowindex);
    if (nowindex >= 2)
    {
        var pageindex = --nowindex;
        document.getElementById("nowpageindex").value = pageindex;
       // alert("1085  " + _AllLocationMarker.length);
        if (_AllLocationMarker.length > 0) {
            for (var i = 0; i < _AllLocationMarker.length; i++) {
                map.removeOverlay(_AllLocationMarker[i]);
            }
        }
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: _wsurl + "/GetLoactionbypageindex",    // /getdiction"        pageindex                  data: "{id:'" + id + "' }",
            data: "{pageindex:'" + pageindex + "', userType:'" + userType + "'}",
            dataType: 'json',
            success: function (result) {
                //alert("1074" + result.d);
                getlocation(result.d);
            }
        });
    }


}


function startgetjd()
{
    t_jd = setInterval(function () {
        getjd();
    }, 2000);
}

function getjd() {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/getjd",    // /getdiction"
        data: "{}",
        dataType: 'json',
        success: function (result) {
            document.getElementById("jd").value = result.d;
        }
    });

}

//[soap 调用webservice,获取为用户进行指派]
function getdiction()
{
    clearInterval(t_people);
    startgetjd();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/getAssign",    // /getdiction"
        data: "{}",
        dataType: 'json',
        success: function (result) {
            console.log("getAssign" + result.d);
            clearInterval(t_jd);
        }
    });

}
//[end]

//[soap 调用webservice,获取为用户进行指派]
function getdiction_off() {
    clearInterval(t_people);
    startgetjd();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/getdiction",    // /getdiction"
        data: "{}",
        dataType: 'json',
        success: function (result) {
            console.log("getAssign" + result.d);
            clearInterval(t_jd);
        }
    });

}
//[end]

//获取人防工事list
function getRFListLater()
{
    showwait();

    getPeople_in_num();
    setTimeout("getRFList()",1000);
    setTimeout("geteninfosum()", 1000);
    
}

function getRFList()
{
  
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetAirDefenseInfoSum",
        data: "{}",
        dataType: "json",
        success: function (result) {
            //hiddenwait();
            //alert(result);
            getsuminfo(result.d);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
          //  alert(XMLHttpRequest.status);
          //  alert(XMLHttpRequest.readyState);
           // alert(textStatus);
        }
    });
    //alert("959");
    //[end]

}
//获取 目前用户数和进入工事人数
function getPeople_in_num() {
   // alert("1372");
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/getpeoplenum",
        data: "{}",
        dataType: "json",
        success: function (result) {
            // alert("1401" + result.d);
            console.log(result.d);
            var str=result.d 
            var results = (result.d).split('@');
            var user_num = results[1];
            var in_num = results[2];
            document.getElementById("peoplesum").value = user_num;
            document.getElementById("peoplein").value = in_num;
        }
    });

    //[end]

}

//[通过webservice查询某一个人防点的详细信息]
function getinfo(id) {
    showwait();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetOneAirDefenseInfo",
        data: "{id:'"+id+"'}",
        dataType: "json",
        success: function (result) {
            hiddenwait();
           // alert(result.d);
            getoneinfo(result.d);
       
        }
    });
}
//[end]



//[通过webservice查询某一个人防入口的详细信息]
function geteninfo(id) {
    showwait();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetOneEnInfo",
        data: "{id:'" + id + "'}",
        dataType: "json",
        success: function (result) {
            hiddenwait();
            //alert(result.d);
            getoneeninfo(result.d);
        }
    });
}
//[end]

//[通过webservice查询全部入口的简要信息]
function geteninfosum() {
   
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetenInfoSum",
        data: "{}",
        dataType: "json",
        success: function (result) {
            hiddenwait();
            //alert(result.d);
            getensuminfo(result.d);

        }
    });
}
//[end]

//指派后，发送短消息
function callws()
{

    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/sendMessage2user",    // /getdiction"       
        data: "{}",
        dataType: 'json',
        success: function (result) {
            alert(result.d);
        }
    });


}



//[通过webservice查询某一个用户的详细信息]
function getUserInfo(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetUserinfo",
        data: "{id:" + id + "}",
        dataType: 'json',
        success: function (result) {
          
            getoneUserinfo(result.d);
        }
    });
}
//[end]

//[通过webservice查询人防工事图片的描述信息]
function getpicmessage(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetPicMessage",
        data: "{id:'"+id+"' }",
        dataType: "json",
        success: function (result) {
            imagesMessage = [];
           getPicMessage(result.d);
       
        }
    });

}
//[end]

//[通过webservice查询入口图片的描述信息]
function getenpicmessage(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetEnPicMessage",
        data: "{id:'" + id + "' }",
        dataType: "json",
        success: function (result) {
            imagesMessage = [];
            getPicMessage(result.d);

        }
    });

}
//[end]

//[通过webservice查询视频信息]
function getcaminfo(id) {
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetCamInfo",
        data: "{id:'" + id + "' }",
        dataType: "json",
        success: function (result) {
            caminfo = result.d;
           // alert(caminfo);
        }
    });

}
//[end]


function changeMode(type)
{

    if (type == "DRAW_LINE")   //定位
    {
        map.addEventListener("click", getlatlng);
        map.removeEventListener("click", setCenter);
    }
    //mappoint.push(marker.getPosition());
   // _AllEnMarker.push(marker);

    if (type == "ALL")   //     定位
    {
        var ALL_Type_Marker = [];
        if (_AllEnMarker.length > 0)  //入口点
        {
            for(var i=0;i<_AllEnMarker.length;i++)
            {
                ALL_Type_Marker.push(_AllEnMarker[i].getPosition());
            }
        }
        if (_AllLocationMarker.length > 0) //定位点
        {
            for (var i = 0; i < _AllLocationMarker.length; i++) {
                ALL_Type_Marker.push(_AllLocationMarker[i].getPosition());
            }
        }
        if (_AllMarker.length > 0)//人防点
        {
            for (var i = 0; i < _AllMarker.length; i++) {
                ALL_Type_Marker.push(_AllMarker[i].getPosition());
            }
        }

        map.setViewport(ALL_Type_Marker, { enableAnimation: true });
        map.removeEventListener("click", getlatlng);
        map.removeEventListener("click", setCenter);
        ALL_Type_Marker = [];
   
    }

    if (type == "NORMAL")//移动
    {
     map.removeEventListener("click", getlatlng);
     map.removeEventListener("click", setCenter);
   
 }

    if (type == "CENTER")     //居中
    {
     map.addEventListener("click", setCenter);
     map.removeEventListener("click", getlatlng);
 }
 
}

function getlatlng(e)
{
    alert(e.point.lng + "," + e.point.lat);
}

function setCenter(e)
{
    map.setCenter(e.point);
}

//查询数据，显示人员热力图
function openUserHT()
{
    showwait();
    $.ajax({
        type: "POST",
        contentType: "application/json",
        url: _wsurl + "/GetAllLoaction",
        data: "{}",
        dataType: 'json',
        success: function (result) {
            console.log("success");
            openHeatmap(result.d);
        }
    });
}
//--------end

//显示热力图
function openHeatmap(strXML)
{
    //var points = [
    // { "lng": 116.418261, "lat": 39.921984, "count":80 },
    // { "lng": 116.423332, "lat": 39.916532, "count":80},
    // { "lng": 116.419787, "lat": 39.930658, "count":80 },
    // { "lng": 116.418455, "lat": 39.920921, "count":80 },
    // { "lng": 116.418843, "lat": 39.915516, "count":80 },
    // { "lng": 116.42546, "lat": 39.918503, "count":80 },
    // { "lng": 116.423289, "lat": 39.919989, "count":80 },
    // { "lng": 116.418162, "lat": 39.915051, "count":80},
    // { "lng": 116.422039, "lat": 39.91782, "count":80 },
    // { "lng": 116.41387, "lat": 39.917253, "count":80 },
    // { "lng": 116.41773, "lat": 39.919426, "count":80 },
    // { "lng": 116.421107, "lat": 39.916445, "count":80 },
    // { "lng": 116.417521, "lat": 39.917943, "count":80 },
    // { "lng": 116.419812, "lat": 39.920836, "count":80 },
    // { "lng": 116.420682, "lat": 39.91463, "count":80},
    // { "lng": 116.415424, "lat": 39.924675, "count":80 },
    // { "lng": 116.419242, "lat": 39.914509, "count":80 },
    // { "lng": 116.422766, "lat": 39.921408, "count":80 },
    // { "lng": 116.421674, "lat": 39.924396, "count":80 },
    // { "lng": 116.427268, "lat": 39.92267, "count":80 },
    // { "lng": 116.417721, "lat": 39.920034, "count":80 },
    // { "lng": 116.412456, "lat": 39.92667, "count":80 },
    // { "lng": 116.420432, "lat": 39.919114, "count":80 },
    // { "lng": 116.425013, "lat": 39.921611, "count":80 },
    // { "lng": 116.418733, "lat": 39.931037, "count":80 },
    // { "lng": 116.419336, "lat": 39.931134, "count":80 },
    // { "lng": 116.413557, "lat": 39.923254, "count":80 },
    // { "lng": 116.418367, "lat": 39.92943, "count":80 },
    // { "lng": 116.424312, "lat": 39.919621, "count":80 },
    // { "lng": 116.423874, "lat": 39.919447, "count":80 },
    // { "lng": 116.424225, "lat": 39.923091, "count":80 },
    // { "lng": 116.417801, "lat": 39.921854, "count":80 },
    // { "lng": 116.417129, "lat": 39.928227, "count":80 },
    // { "lng": 116.426426, "lat": 39.922286, "count":80 },
    // { "lng": 116.421597, "lat": 39.91948, "count":80 },
    // { "lng": 116.423895, "lat": 39.920787, "count":80 },
    // { "lng": 116.423563, "lat": 39.921197, "count":80 },
    // { "lng": 116.417982, "lat": 39.922547, "count":80 },
    // { "lng": 116.426126, "lat": 39.921938, "count":80 },
    // { "lng": 116.42326, "lat": 39.915782, "count":80 },
    // { "lng": 116.419239, "lat": 39.916759, "count":80 },
    // { "lng": 116.417185, "lat": 39.929123, "count":80 },
    // { "lng": 116.417237, "lat": 39.927518, "count":80 },
    // { "lng": 116.417784, "lat": 39.915754, "count":80 },
    // { "lng": 116.420193, "lat": 39.917061, "count":80 },
    // { "lng": 116.422735, "lat": 39.915619, "count":80},
    // { "lng": 116.418495, "lat": 39.915958, "count":80 },
    // { "lng": 116.416292, "lat": 39.931166, "count":80},
    // { "lng": 116.419916, "lat": 39.924055, "count":80 },
    // { "lng": 116.42189, "lat": 39.921308, "count":80 },
    // { "lng": 116.413765, "lat": 39.929376, "count":80 },
    // { "lng": 116.418232, "lat": 39.920348, "count":80 },
    // { "lng": 116.417554, "lat": 39.930511, "count":80 },
    // { "lng": 116.418568, "lat": 39.918161, "count":80 },
    // { "lng": 116.413461, "lat": 39.926306, "count":80},
    // { "lng": 116.42232, "lat": 39.92161, "count":80 },
    // { "lng": 116.4174, "lat": 39.928616, "count":80 },
    // { "lng": 116.424679, "lat": 39.915499, "count":80 },
    // { "lng": 116.42171, "lat": 39.915738, "count":80 },
    // { "lng": 116.417836, "lat": 39.916998, "count":80 },
    // { "lng": 116.420755, "lat": 39.928001, "count":80 },
    // { "lng": 116.414077, "lat": 39.930655, "count":80 },
    // { "lng": 116.426092, "lat": 39.922995, "count":80 },
    // { "lng": 116.41535, "lat": 39.931054, "count":80 },
    // { "lng": 116.413022, "lat": 39.921895, "count":80 },
    // { "lng": 116.415551, "lat": 39.913373, "count":80 },
    // { "lng": 116.421191, "lat": 39.926572, "count":80 },
    // { "lng": 116.419612, "lat": 39.917119, "count":80 },
    // { "lng": 116.418237, "lat": 39.921337, "count":80 },
    // { "lng": 116.423776, "lat": 39.921919, "count":80 },
    // { "lng": 116.417694, "lat": 39.92536, "count":80 },
    // { "lng": 116.415377, "lat": 39.914137, "count":80 },
    // { "lng": 116.417434, "lat": 39.914394, "count":80 },
    // { "lng": 116.42588, "lat": 39.922622, "count":80 },
    // { "lng": 116.418345, "lat": 39.919467, "count":80 },
    // { "lng": 116.426883, "lat": 39.917171, "count":80 },
    // { "lng": 116.423877, "lat": 39.916659, "count":80 },
    // { "lng": 116.415712, "lat": 39.915613, "count":80 },
    // { "lng": 116.419869, "lat": 39.931416, "count":80 },
    // { "lng": 116.416956, "lat": 39.925377, "count":80 },
    // { "lng": 116.42066, "lat": 39.925017, "count":80 },
    // { "lng": 116.416244, "lat": 39.920215, "count":80 },
    // { "lng": 116.41929, "lat": 39.915908, "count":80 },
    // { "lng": 116.422116, "lat": 39.919658, "count":80 },
    // { "lng": 116.4183, "lat": 39.925015, "count":80 },
    // { "lng": 116.421969, "lat": 39.913527, "count":80 },
    // { "lng": 116.422936, "lat": 39.921854, "count":80 },
    // { "lng": 116.41905, "lat": 39.929217, "count":80 },
    // { "lng": 116.424579, "lat": 39.914987, "count":80 },
    // { "lng": 116.42076, "lat": 39.915251, "count":80 },
    // { "lng": 116.425867, "lat": 39.918989, "count":80 }];
    //console.log(points[4]);

    if (!isSupportCanvas()) {
        alert('热力图目前只支持有canvas支持的浏览器,您所使用的浏览器不能使用热力图功能~')
    }
    else {
        console.log("支持");
        heatmapOverlay = new BMapLib.HeatmapOverlay({ "radius": 15, "opacity":50 });               //({"radius":10, "visible":true, "opacity":70});
        map.addOverlay(heatmapOverlay);
      
      //  heatmapOverlay.setDataSet({ data: points, max: 100 });
        var struserlaction = strXML.split('@');
        
     
         var points = [];

         for (var i = 0; i < 1000; i++) {
            var lng = struserlaction[i].split('&')[0];
            var lat = struserlaction[i].split('&')[1];
            var jsonStr2 = "{\"lng\":" + lng + "," + "\"lat\":" + lat + ",\"count\":80}";
            var jsonObj2 = eval('(' + jsonStr2 + ')');
            points.push(jsonObj2);
        }
        heatmapOverlay.setDataSet({ data: points, max: 100 });
        heatmapOverlay.show();
        console.log("show");
       hiddenwait();
    }
}
//判断浏览器是否支持“canvas”
function isSupportCanvas() {
    var elem = document.createElement('canvas');
    return !!(elem.getContext && elem.getContext('2d'));
}

//去掉热力图层
function closeHeatmap()
{
    try {
       // heatmapOverlay.hide();
        map.removeOverlay(heatmapOverlay);
    }
    catch (e)
    {
        console.log(e.message);
     
    }

}

function setUserType(type)
{
    userType = type;
 

}

function onoffline(stats)
{
    if (stats == "online")
    {
        bool_offline = "false";
        document.getElementById("Image3").ImageUrl = "~/image/online.png";
        document.getElementById("Label1").Text = "在线";
    }
    
    else
    {
        bool_offline = "true";
        document.getElementById("Image3").ImageUrl = "~/image/offline.png";
        document.getElementById("Label1").Text = "离线";

    }
     



}


