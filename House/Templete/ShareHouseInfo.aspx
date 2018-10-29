<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShareHouseInfo.aspx.cs" Inherits="HouseMIS.Web.House.Templete.ShareHouseInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Title %><</title>
    <link rel="stylesheet" href="ShareFile/idangerous.swiper.css"/>
    <link href="ShareFile/share.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="js/jquery-1.4.1.min.js"></script>
    <script type="text/javascript" src="ShareFile/jquery-1.10.1.min.js"></script>
    <script type="text/javascript" src="ShareFile/idangerous.swiper-2.1.min.js"></script>
        
</head>
<body>


    <!-- JiaThis Button BEGIN -->
<!-- JiaThis Button BEGIN -->
<script type="text/javascript" >
    var jiathis_config = {
        siteNum: 14,
        sm: "email,qzone,weixin,tqq,renren,kaixin001,douban,yixin,xiaoyou,feixin,tieba,cqq,tianya",//copy
        summary: "",
        showClose: true,
        shortUrl: false,
        hideMore: false
    }
</script>
<script type="text/javascript" src="http://v3.jiathis.com/code/jiathis_r.js?btn=r.gif&move=0" charset="utf-8"></script>
<!-- JiaThis Button END -->



<%--<script type="text/javascript" src="http://v3.jiathis.com/code/jiathis_r.js?uid=1876728&move=0"
        charset="utf-8"></script>--%>
        <script type="text/javascript">
            setTimeout(function () { $("div[class='jiathis_style']").hide(); }, 50);
        </script>
 
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" onsubmit="return dialogSearch(this);"
    action="ShareHouseInfo.aspx" method="post">
    <input type="hidden" id="HouseID" runat="server" value='<%= Request.QueryString["HouseID"] %>' />
    <input type="hidden" id="TempleteID" runat="server" value='<%= Request.QueryString["TempleteID"]%>' />
    <div class="pageFormContent" layouth="56">
        <asp:Literal runat="server" ID="HtmlContent">
        </asp:Literal>
    </div>
    </form>
    <script type="text/javascript">
        var jiathis_config
        $(document).ready(function () {
            $("#buttonok").click(function () {
                postShare();
                $("div[class='jiathis_style']").show();

            });

            var v = GenCan("Type");
            if (v == "0") {
                $("#buttonok").hide();
            }
        });


        function postShare() {
            var House = $("#HouseID").val();
            var Templete = $("#TempleteID").val();

            $.ajax({
                type: "POST",
                dataType: "html",
                url: "SharePost.ashx?HouseID=" + House + "&TempleteID=" + Templete + "",
                data: null,
                success: function (msg) {
                    if (msg == '0') {
                        alert("发生错误！");
                    }
                    else {
                        alert("分享成功");
                        var tit = $(".round title").text();
                        var URL = msg;
                        jiathis_config = { url: URL, title: tit };
                        $("#buttonok").hide();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("保存失败!");
                }
            });
        }


        function GenCan(name) {
            var url = window.location.search;
            if (url.indexOf("?") != -1) {
                var str = url.substr(1)
                strs = str.split("&");
                for (i = 0; i < strs.length; i++) {
                    if ([strs[i].split("=")[0]] == name)
                        return unescape(strs[i].split("=")[1]);
                }
            }
        }
    </script>
    <script type="text/javascript">
        window.onload = function () {
            var picUrl = "<%=PicUrlList %>";
            //房源图片展示
            if (picUrl == ",,,,") {
                $("#mySwiperP").hide();
            } else {
                var sz = picUrl.split(",");
                var len = sz.length;
                for (var i = 0; i < len; i++) { document.getElementById("pic" + i).src = sz[i]; }
            }
            $("#fzf").text(parseInt(Math.random() * (8000000 - 1230000 + 1) + 1230000))
        }
    </script>
    <script>        var mySwiper = new Swiper('.swiper-container', { pagination: '.pagination', paginationClickable: true })</script>
</body>
</html>
