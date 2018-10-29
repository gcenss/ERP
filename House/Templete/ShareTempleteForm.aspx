<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareTempleteForm.aspx.cs" Inherits="HouseMIS.Web.House.Templete.ShareTempleteForm" validateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body, td, th, p, lable, l, img
        {
            font-family: Arial, Helvetica, sans-serif;
            margin: 0px;
            padding: 0px;
            font-size: 1em;
        }
        p{font-size:16px;color:#b7b7b7; margin:6px 0px 3px 0px;text-shadow: 1px 1px 0 #ffffff;}
    </style>
</head>
<body>
    <form id="form1" runat="server" action="House/Templete/ShareTempleteForm.aspx" class="pageForm required-validate"
    onsubmit="return validateCallback(this, dialogAjaxDone)" >
    <input type="hidden" id="emid" runat="server" />
    <input type="hidden" id="oldadmin" runat="server" />
    <input type="hidden" id="oldbtlx" runat="server" />
    <input type="hidden" id="dstype" runat="server" />
    <asp:HiddenField  runat="server" ID="Type"   />
   <div id="video" style="width:4px;height:4px; display:none " ></div> 
    <div class="pageContent" layouth="50">
        <table width="100%" style="border-spacing: 5px;" >
            <tr>
                <td >模板类型：
                    <asp:DropDownList ID="frmShareType" runat="server" Height="20px" Width="126px">
                         <asp:ListItem Text="好文章" Value="好文章"></asp:ListItem>
                        <asp:ListItem Text="视频" Value="视频"></asp:ListItem>
                        <asp:ListItem Text="游戏" Value="游戏"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >
                    <input type="text" runat="server" id="frmTitle" class="required" style="width: 98%;
                        height: 40px; color: #454545; background-color: #FFFFFF; border: 1px solid #dedede;
                        font-size: 20px; text-align: center; font-weight: bold" />
                </td>
            </tr>
            <tr>
                <td >
                    <p style="color: Red">
                       内容 必须插入5张以上图片，可到百度图片中搜索相关图片
                    </p>
                </td>
            </tr>
            <tr>
                <td >
                    <input type="hidden" id="frmContents" runat="server" />
                    <textarea id="editer1">
            </textarea>
                </td>
            </tr>
        </table>
    </div>
    <div class="formBar">
        <ul>
            <li>
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="submit" id="isok">
                            保存</button></div>
                </div>
            </li>
            <li>
                <div class="button">
                    <div class="buttonContent">
                        <button type="button" class="close">
                            取消</button></div>
                </div>
            </li>
        </ul>
    </div>
    </form>

    <script type="text/javascript" src="http://player.youku.com/jsapi">
        function youku(url) {
            var start = url.indexOf('id_', 0);
            var end = url.indexOf('.html', start);
            var vid = url.substring(start, end).replace('id_', "");
            player = new YKU.Player('video', {
                client_id: 'YOUR YOUKUOPENAPI CLIENT_ID',
                vid: vid,
                autoplay: true
            });
        } 
     </script>


    <script type="text/javascript">
        var editor;
        function makeEditor(id) {
            CKEDITOR.on('dialogDefinition', function (ev) {
                var dialogName = ev.data.name;
                var dialogDefinition = ev.data.definition;
                if (dialogName == 'image') {
                    dialogDefinition.removeContents('info');
                    dialogDefinition.removeContents('Link');

                    dialogDefinition.onOk = function () {
                        var src = $("iframe[title='上传到服务器']").contents().find('pre').html();
                        this.imageElement = editor.document.createElement('img');
                        this.imageElement.setAttribute('src', src);
                        this.imageElement.setAttribute('width', '100%');
                        editor.insertElement(this.imageElement);

                        var d = $('div[role="dialog"]');
                        d.remove();
                        var box = $('div[class="cke_dialog_background_cover"]');
                        box.css("display", "none");
                    }

                }
            });
            editor = CKEDITOR.replace(id);
            CKEDITOR.config.allowedContent = true; 
        }
        makeEditor("editer1");

        function videoinit(msg) {
            var index = msg.indexOf('http://v.youku.com', 0);
            if (index == 0) {
                youku(msg);
                setTimeout('createhtml()', 1000);
            }
            else {
                index = msg.indexOf('http://www.tudou.com', 0);
                if (index == 0) {
                    createhtmltd(msg);
                } else {
                    index = msg.indexOf('http://v.ku6.com', 0);
                    if (index == 0) {
                        createhtmlkl(msg);
                    }
                }
            }
        };


        function createhtmlkl(url) {
            var start = url.indexOf('show/', 0);
            var end = url.indexOf('?', 0);
            var vid = url.substring(start, end).replace("show/", "").replace("html", "").replace(".","");
            var url = 'http://player.ku6.com/refer/' + vid + '/v.swf';
            var gtml='<embed align="middle" allowfullscreen="true" allowscriptaccess="always" flashvars="" height="500" quality="high"';
            gtml+='src="'+url+'"';
            gtml += 'type="application/x-shockwave-flash" width="100%"></embed>'
            CKEDITOR.instances.editer1.insertHtml(gtml);

        }

        function createhtmltd(url) {
            var start = url.indexOf('view/', 0);
            var vid = url.substr(start, url.length).replace("/", "").replace("view", "");
            var gtml='<embed src="http://www.tudou.com/v/';
            gtml=gtml+vid;
            gtml=gtml+' &withRecommendList=false&startSeekPoint=0/v.swf" type="application/x-shockwave-flash"';
            gtml = gtml + 'allowscriptaccess="always" allowfullscreen="true" wmode="opaque" width="100%" height="500"> </embed>';
            CKEDITOR.instances.editer1.insertHtml(gtml);
        }

        function createhtml() {
            var gtml = $("#video").html();
            gtml = '<div style="width:100%;height:500px; ">' + gtml + '</div>';
            CKEDITOR.instances.editer1.insertHtml(gtml);

        }

        $(document).ready(function () {
            editor.setData($("#frmContents").val());
            $("#isok").click(function () {
                $("#frmContents").val(fitdata());
            });
        });


        function fitdata() {
            var content = editor.getData();
            var $box = $("<div></div>");
            $box.html(content);

            var total = 0;
            $("img", $box).each(function () {
                $(this).attr("style", "width:100%");
                $(this).removeAttr("width");
                $(this).removeAttr("height");
                total++;

            });
            //table td div p
            $("table", $box).each(function () {
                $(this).removeAttr("width");
                $(this).removeAttr("height");
                $(this).removeAttr("style");
                $(this).removeClass();
            });

            $("div", $box).each(function () {
                $(this).removeAttr("width");
                $(this).removeAttr("height");
                $(this).removeAttr("style");
                $(this).removeClass();
            });

            $("td", $box).each(function () {
                $(this).removeAttr("width");
                $(this).removeAttr("height");
                $(this).removeAttr("style");
                $(this).removeClass();
            });

            $("p", $box).each(function () {
                $(this).removeAttr("width");
                $(this).removeAttr("height");
                $(this).removeAttr("style");
                $(this).removeClass();
            });
           return "<table style='width:100%'><tr><td>" + $box.html() + "</td></tr></table>";
        }
    </script>
</body>
</html>
