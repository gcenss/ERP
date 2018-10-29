<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFromSP.aspx.cs" Inherits="HouseMIS.Web.House.HouseFromSP" %>

<link href="/xheditor/uploadify/css/uploadify.css" rel="stylesheet" />
<script src="/xheditor/uploadify/scripts/jquery.uploadify.v2.1.0.js"></script>
<script src="/xheditor/uploadify/scripts/swfobject.js"></script>

<body>
    <%--
        <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="400" height="25"
            codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab">
            <param name="movie" value="/Fla/HouseVideoUp.swf" />
            <param name="quality" value="high" />
            <param name="FlashVars" value="HouseID=<%=HouseID %>&EmployeeID=<%=HouseMIS.EntityUtils.Employee.Current.EmployeeID %>" />
            <param name="allowScriptAccess" value="sameDomain" />
            <embed src="/Fla/HouseVideoUp.swf" flashvars="HouseID=<%=HouseID %>&EmployeeID=<%=HouseMIS.EntityUtils.Employee.Current.EmployeeID %>"
                quality="high" width="400" height="25" align="middle" allowfullscreen="true"
                play="true" loop="false" quality="high" allowscriptaccess="sameDomain" type="application/x-shockwave-flash"
                pluginspage="http://www.macromedia.com/go/getflashplayer">
         </embed>
        </object>
    --%>
    <%--<a rel="GJF_House" title="添加积分跟进" style="color: Red;" href="House/FollowUpEditor.aspx?GJAtype=19&HouseID=<%=HouseID %>&NavTabId=<%=NavTabId %>&doAjax=true"
        target="dialog" width="380" height="324">添加积分跟进</a>--%>
    <div id="fileUpload" runat="server" visible="false">
        <div id="fileQueue">
        </div>
        <input type="file" name="uploadify" id="uploadify" />
    </div>

    <%--<p>
      <a href="javascript:$('#uploadify').uploadifyUpload()">上传</a>| 
      <a href="javascript:$('#uploadify').uploadifyClearQueue()">取消上传</a>
    </p>--%>

    <br />
    <br />
    <%=HouseVideo() %>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                'uploader': '/xheditor/uploadify/scripts/uploadify.swf',
                //'script': '/Ajax/upHouseVideo.ashx',
                <%--'scriptData': { 'HouseID':<%=HouseID %>, 'EmployeeID':<%=EmployeeID %>},--%>
                'script': 'http://vod.erp.efw.cn:8010/houseVideoUpLoad.ashx',
                'scriptData': { 'HouseID':<%=HouseID %> },
                'cancelImg': '',
                'method': 'GET',
                'folder': 'UploadFiles',
                'queueID': 'fileQueue',
                'auto': true,
                'multi': true,
                'fileDataName': 'FileData',
                'fileDesc': '请选择MP4文件',
                'fileExt': '*.mp4;',
                'sizeLimit': '52428800',
                'buttonImg': '/xheditor/uploadify/img/uoload.png',
                'width': '100',
                'height': '30',
                'scriptAccess': 'always',
                'onProgress': function (event, queueId, fileObj, data) {
                    if (data.percentage == 100) {
                        $("#fileQueue").append("<span style=\"color: red\">文件正在上传，不要关闭当前页面，请稍候！</span>");
                    }
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    $("#fileQueue").html("");
                    //alert(response);
                    $.ajax({
                        url: "/Ajax/HouseVideoAdd.ashx",
                        data: {
                            houseID:<%=HouseID %>,
                            employeeID:<%=EmployeeID %>,
                            fileName: response
                        },
                        success: function (result) {
                            alert(result);
                        },
                        error: function (result) {
                            alert(result.responseText);
                        }
                    })
                }
            });
        });
    </script>
</body>
