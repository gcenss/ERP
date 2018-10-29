<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseDetai.aspx.cs" Inherits="HouseMIS.Web.House.HouseDetai" %>

<script src="/js/jquery.js" type="text/javascript"></script>
<link href="images/all.css" rel="stylesheet" type="text/css" />
<style>
    .you_pos {
        padding: 15px 6px 6px 6px;
        text-align: left;
        clear: both;
    }

    .h_body {
        border-top: 3px #0A68C1 solid;
        height: auto;
        padding: 0px 7px;
        width: 590px;
    }

    .h_body_L {
        float: left;
        width: 590px;
        height: auto;
    }

    .hbl_redt {
        float: left;
        margin-left: 108px;
        width: 122px;
        height: 17px;
        text-align: center;
        background: url(images/housershow_03.gif) no-repeat;
        color: #fff;
        padding-top: 3px;
    }

    .fy_bt {
        float: left;
        width: 360px;
        font-size: 16px;
        font-weight: bold;
        color: #1b3490;
        padding: 15px 0px 5px 100px;
    }

    .hb_top_l {
        clear: both;
        border-top: 1px #f6e096 solid;
    }

    .l_fc_img {
        float: left;
        width: 220px;
        height: 290px;
        padding: 8px 6px 0px 0px;
    }

        .l_fc_img .b_img img {
            padding: 2px;
            border: 1px #cecfce solid;
        }

        .l_fc_img .s_img {
            float: left;
            width: 61px;
            padding-top: 3px;
        }

    .r_list {
        float: left;
        width: 364px;
    }

    .height30 {
        height: 40px;
    }

    .width370 {
        width: 350px;
        clear: both;
        text-align: left;
        font-size: 12px;
        padding-top: 4px;
    }

    .width185 {
        float: left;
        width: 170px;
        text-align: left;
        padding-top: 15px;
        height: 35px;
    }

        .width185 span {
            color: #1b3490;
        }

        .width185 b {
            font-weight: bold;
            font-size: 20px;
            color: #0A68C1;
            font-family: Arial;
        }

        .width185 em, .width370 em {
            font-style: normal;
            color: #9c9a9c;
        }

    .h_money {
        font-size: 36px;
        font-family: Arial;
        color: #0A68C1;
    }

    .f_zj_list {
        float: left;
        width: 332px;
        border-top: 1px #cecfce dashed;
        height: 22px;
        border-bottom: 1px #cecfce dashed;
        padding: 6px;
        margin: 5px 0px;
    }

        .f_zj_list img {
            float: left;
            padding-left: 11px;
        }

        .f_zj_list span a {
            float: left;
            padding: 4px 0px 0px 2px;
            color: #9c9a9c;
        }

    .tel_kj {
        width: 322px;
        border: 1px #0A68C1 solid;
        height: 30px;
        background-color: #EBF0F5;
        float: left;
        margin-top: 5px;
    }

        .tel_kj img {
            float: left;
            padding: 5px 8px 0px 19px;
        }

        .tel_kj span {
            float: left;
            padding-top: 12px;
        }

        .tel_kj b {
            font-size: 24px;
            font-family: Arial;
            color: #de0c00;
            float: left;
            padding-top: 3px;
            overflow: hidden;
            width: 220px;
            height: 36px;
        }

    .h_bodyshow {
        padding-top: 30px;
        clear: both;
    }

        .h_bodyshow .hb_title {
            float: left;
            text-align: center;
            width: 89px;
            height: 23px;
            background: url(images/housershow_42.gif) no-repeat;
            padding-right: 2px;
            line-height: 23px;
            font-weight: bold;
            color: #fff;
        }

            .h_bodyshow .hb_title a {
                color: #fff;
            }

        .h_bodyshow .hb_titles {
            float: left;
            text-align: center;
            width: 63px;
            height: 23px;
            background: url(images/housershow_44.gif) no-repeat;
            padding-right: 2px;
            line-height: 23px;
        }

            .h_bodyshow .hb_titles a {
                color: #000;
            }

    .hb_lista {
        height: auto;
        clear: both;
        font-size: 14px;
        text-align: left;
        border: 1px #0A68C1 solid;
        border-top: 6px #0A68C1 solid;
        padding: 0px 3px;
    }

    .hl_line1 {
        border-top: 1px #0A68C1 solid;
        height: 23px;
        line-height: 23px;
        background-color: #fffbe7;
        text-align: left;
        font-weight: bold;
    }

    .hb_cont {
        text-align: left;
        padding: 20px 0px;
        text-align: left;
        line-height: 22px;
    }
</style>
<form id="hd_from1" runat="server" action="House/HouseDetai.aspx" class="pageForm required-validate"
    onsubmit="return validateCallback(this, dialogAjaxDone)">
    <div class="h_body" oncontextmenu="return false">
        <div class="h_body_L">
            <div class="hbl_redt">
                房源详细介绍
            </div>
            <div class="fy_bt">
            </div>
            <div class="hb_top_l">
                <div class="l_fc_img">
                    <div class="b_img">
                        <img id="showPic" src="images/nohousePic.png" width="210" height="190" />
                    </div>
                </div>
                <div class="r_list">
                    <div class="width370 height30">
                        总&nbsp;&nbsp;价：<b class="h_money"><%=Entity.Sum_price%></b>万元
                    </div>
                    <div class="width185">
                        单&nbsp;&nbsp;价：<span><%=Entity.Ohter2ID.ToString()%>
                        元/平方米</span>
                    </div>
                    <div class="width185">
                        <em>房&nbsp;&nbsp;型：</em><%=Entity.Form_bedroom%>室<%=Entity.Form_foreroom%>厅<%=Entity.Form_toilet%>卫
                    </div>
                    <div class="width185" style="width: 350px;">
                        建筑面积：<b><%=Entity.Build_area%>
                    </b>平方米
                    </div>
                    <div class="width370" style="padding-top: 15px;">
                        <em>建筑年代：</em><%=Entity.Year%>大约年代(仅供参考,实际年代以查档为标准)
                    </div>
                    <div class="width185">
                        <em>所在楼层：</em><%=Entity.Build_floor%>/<%=Entity.Build_levels%>层
                    </div>
                    <div class="width185">
                        <em>房屋朝向：</em><%=Entity.Orientation%>
                    </div>
                    <div class="width185">
                        <em>室内装修：</em><%=Entity.Renovation%>
                    </div>
                    <div class="width185">
                        <em>物业权属：</em><%=Entity.PropertyName%>
                    </div>
                    <div class="width370">
                        <em>楼盘地址：</em><%=Entity.HouseDicAddress%>
                    </div>
                </div>
            </div>
            <div class="h_bodyshow">
                <div class="hb_title">
                    <a href="#hb1">房源介绍</a>
                </div>
                <div class="hb_titles">
                    <a href="#hb2">交通配套</a>
                </div>
                <div class="hb_titles">
                    <a href="#hb3">房源图片</a>
                </div>
                <div class="hb_lista">
                    <div class="hb_cont" id="hb1">
                        <%=Entity.Note%>
                    </div>
                    <div class="hl_line1">
                        <img src="images/newMth41.gif" />
                        交通配套
                    </div>
                    <div class="hb_cont" id="hb2">
                        <asp:label id="hf_label_pt" runat="server"></asp:label>
                    </div>
                    <div class="hl_line1">
                        <img src="images/housershow_49.gif" />
                        房源图片
                    </div>
                    <div class="hb_cont" id="hb3">
                        <asp:repeater id="h_img_list" runat="server"><ItemTemplate>

                                <div>
                                    <img class="picimg_hos" src="<%#GetAllUrl(Eval("PicURL"))%>" onload="javascript:DrawImage(this,560,500)" title="点击保存图片"   /><br />
                                    <%#Eval("BillCode") %><%#Eval("Name") %>-<%#Eval("em_id") %><%#Eval("em_name") %> 日期：<%#Eval("exe_date") %>
                                </div>
                        </ItemTemplate></asp:repeater>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hidden_houset" class="hidden_houset" value="<%=Entity.HouseID %>" />
</form>
<script>
    var hdimgs = $("#hb3 img");
    if (hdimgs.length > 0) {
        document.getElementById("showPic").src = hdimgs[0].src;
    }
    <%--function selTelClick() {
        if ($("#hidden_houset").val() != "0") {
            var result = HouseMIS.Web.House.HouseDetai.CanGetTel(parseInt($("#hidden_houset").val())).value;
            if (result == "True") {
                var house = HouseMIS.Web.House.HouseDetai.ShowTel(parseInt($("#hidden_houset").val()), '<%=NavTabId %>').value;
                if (house != null) {
                    $("#telshows").html(house);
                    GetTelNum();
                }
            } else { alertMsg.error("对不起该房源今天的电话查看次数已满！"); }
        } else { return false; }
    }--%>
</script>