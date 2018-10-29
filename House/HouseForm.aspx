<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseForm.aspx.cs" Inherits="HouseMIS.Web.House.HouseForm" %>

<style type="text/css">
    .style6 {
        width: 85px;
    }

    .style8 {
        width: 77px;
    }

    .style9 {
        width: 73px;
    }

    .style12 {
        width: 87px;
    }

    .style13 {
        width: 133px;
    }

    .style14 {
        width: 63px;
    }

    .style15 {
        height: 9px;
    }

    .style16 {
        width: 74px;
    }

    .style17 {
        width: 70px;
    }

    .style18 {
        width: 66px;
    }

    .fbwz2 {
        position: absolute;
        z-index: 500;
        left: 0;
        top: -50px;
    }

        .fbwz2 ul li {
            list-style: none;
            padding: 1px;
            margin: 0;
            background-color: #fff;
            border: 1px solid #999;
            width: 120px;
            height: auto;
        }

        .fbwz2 li a {
            display: block;
            float: left;
            color: #000;
            width: 120px;
            padding: 3px 20px;
            margin: 0;
            border: 1px solid #fff;
            background-color: transparent;
            text-align: left;
            cursor: default;
        }

            .fbwz2 li a:hover {
                border: 1px solid #0a246a;
                background-color: #b6bdd2;
                text-decoration: none;
            }

    #FU td div {
        display: block;
        overflow: auto;
        white-space: normal;
        height: auto;
        line-height: normal;
    }

    .edit {
        color: Red;
    }

    .Hselect {
        display: none;
    }
</style>

<script type="text/javascript" src="js/flaUpfile.js"></script>
<script type="text/javascript">
    $(function () {
        if (parseInt(<%=Entity.HouseID %>) > 0) {
            $("#exit_bxs" +<%=Entity.HouseID %>).hide();
            $("#seeHouseRoom").show();
        }
        else {
            $("#exit_bxs" +<%=Entity.HouseID %>).show();
            $("#seeHouseRoom").hide();
        }

        if ($("#div_<%=Entity.HouseID %> .hidden_houset:eq(0)").val() == "0") {
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(1)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(2)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(3)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(4)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(5)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(6)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(7)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(8)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(9)").hide();
            $("#div_<%=Entity.HouseID %> .tabsHeaderContent ul li:eq(10)").hide();
        }

        if (parseInt(<%=canbut%>) == 1 && parseInt(<%=Entity.HouseID %>) > 0) {
            //alert("您的查看次数超标,无法再次查看,请联系管理员！");
            $("#exit_kj" +<%=Entity.HouseID %>).attr("disabled", true)
            $("#exit_bxs" +<%=Entity.HouseID %>).hide();
        }

        $('#exit_kj' +<%=Entity.HouseID %>).click(function () {
            $.ajax({
                type: "POST",
                url: 'House/HousePicExtS.aspx',
                data: { hid: <%=Entity.HouseID %>, uid: <%=EmployeeID %> },
                success: function (data) {
                    if (data == 1) {
                        alert("您的查看次数超标,无法再次查看,请联系管理员！");
                    }
                    else if (data == 2) {
                        alertMsg.error("您当日的查看次数已满,请勿再执行该操作！");
                    }
                }
            });
            $("#exit_bxs" +<%=Entity.HouseID %>).show();
            $("#seeHouseRoom").hide();
        });

        //清空房源核验
        $("#btnEmpty_Sale").click(function () {
            if (confirm("确认清空房源核验吗？")) {
                //房源描述
                $("#div_" + <%=Entity.HouseID %> + " .frmLinkTel1:eq(0)").val("");
                //看房时间
                $("#div_" + <%=Entity.HouseID %> + " .frmSeeHouseID:eq(0)").val("");
                //房屋情况
                $("#div_" + <%=Entity.HouseID %> + " .frmNowStateID:eq(0)").val("");
                //税费
                $("#div_" + <%=Entity.HouseID %> + " .frmTaxesID:eq(0)").val("");
                //产证情况
                $("#div_" + <%=Entity.HouseID %> + " .frmAssortID:eq(0)").val("");
                //光线情况
                $("#div_" + <%=Entity.HouseID %> + " .frmSaleMotiveID:eq(0)").val("");
                //外墙
                $("#div_" + <%=Entity.HouseID %> + " .frmApplianceID:eq(0)").val("");
                //带看人
                $("#div_" + <%=Entity.HouseID %> + " .frmPayServantID:eq(0)").val("");
            }
        });

        var HDN = $("#div_<%=Entity.HouseID %> .frmHouseDicName:eq(0)").val();
        var HDD = $("#div_<%=Entity.HouseID %> .frmHouseDicID:eq(0)").val();
        //绑定Suggest事件
        IntSuggests("div_<%=Entity.HouseID %> .frmHouseDicName:eq(0)", "div_<%=Entity.HouseID %> .frmHouseDicID:eq(0)", "/Ajax/SearchHouseDic.ashx?HouseID=<%=Entity.HouseID %>&kw=");
        $("#div_<%=Entity.HouseID %> .frmHouseDicName:eq(0)").val(HDN);
        $("#div_<%=Entity.HouseID %> .frmHouseDicID:eq(0)").val(HDD);

        $("#div_<%=Entity.HouseID %> .HousePicFla<%=Entity.HouseID %>:eq(0)").hide();

        $("#div_<%=Entity.HouseID %> .fbwz2 a").click(function () {
            $("#div_<%=Entity.HouseID %> .fbwz2").hide();
        });
    })

    var hpic = "pt1";
    function ChangeHPic(id) {
        if (id == "pt1") {
            if ($("#PicType").val() == "1")
                $("#div_<%=Entity.HouseID %> .HousePicFla<%=Entity.HouseID %>:eq(0)").show();
            else {
                $("#div_<%=Entity.HouseID %> .HousePicFla<%=Entity.HouseID %>:eq(0)").hide();
            }
        }
        else {
            $("#div_<%=Entity.HouseID %> .HousePicFla<%=Entity.HouseID %>:eq(0)").show();
        }
        hpic = id;
    }

    function show_money(hosid) {
        var sm_val = $("#div_" + hosid + " .frmMin_price:eq(0)").val();
        $("#div_" + hosid + " .frmMin_priceb:eq(0)").val(sm_val);
    }
    function TelOpen(TelHouseID, TelEmployeeID) {
        var options = { width: 530, height: 300, mixable: false };
        $.pdialog.open("'NanChangPhone/InternetPhone/Phone.ashx?EmployeeID=" + TelEmployeeID + "&HouseID=" + TelHouseID, "HouseTelGo", "拨打电话", options);
    }

    function ShowJiFenHouse() {
        navTab.openTab("252_menujfrz", "Core/IntegralLogs.aspx?NavTabId=252_menujfrz&HouseID=<%=Entity.HouseID %>", { title: "积分记录", fresh: false, data: {} });
    }

    function selBackTelClick(hs_ids_t) {
        if (parseInt($("#div_" + hs_ids_t + " .hidden_houset:eq(0)").val()) != 0) {
            var result = HouseMIS.Web.House.HouseForm.GetBackTel(parseInt($("#div_" + hs_ids_t + " .hidden_houset:eq(0)").val()), '<%=NavTabId %>').value;

            if (result != null) {
                if (result.indexOf("权限") > 0) {
                    alertMsg.error(result);
                    $("#div_" + hs_ids_t + " .frmBackTel:eq(0)").val(result);
                }
                else {
                    $("#div_" + hs_ids_t + " .frmBackTel:eq(0)").val(result);
                }
            } else {
                $("#div_" + hs_ids_t + " .frmBackTel:eq(0)").val("");
            }
        }
    }
    function FindBackLDD(id, sid) {
        if ($("#" + sid).val() != "") {
            var arrey;
            var href = $(id).attr("href");
            if (href.indexOf("=") + 1 < href.length) {
                href = href.substring(0, href.indexOf("=") + 1);
            }
            if ($("#" + sid).val().indexOf("|") > 0) {
                arrey = $("#" + sid).val().split("|");
                $(id).attr("href", href + arrey[0]);
            } else {
                arrey = $("#" + sid).val()
                $(id).attr("href", href + arrey);
            }
        } else {
            alertMsg.error("请先选择上一级选项");
        }
    }
    function dialogAjaxDoneHouseF(json) {
        //jtaosj 添加自定义Ajax CallBack事件
        if (json && json.userCallBack) {
            userAjaxDone(json);
            return false;
        }
        DWZ.ajaxDone(json);
        if (json.statusCode == DWZ.statusCode.ok) {
            if ("closeCurrent" == json.callbackType) {
                //$.ajax({
                //    url: 'PushExt.ashx',
                //    data: { FollowID: json.rel},
                //    success: function (result)
                //    { }
                //});
                $.pdialog.closeCurrent();
            }
        }
    }
    function GetPrices(div) {
        var Area = $("#div_" + div + " #frmBuild_area").val();
        var SumPrice = $("#div_" + div + " #frmSum_price").val();
        if (Area > 0 && SumPrice > 0) {
            var price = (SumPrice * 10000) / Area;
            $("#div_" + div + " #Prices").val(price.toFixed(0));
        }
        else {
            $("#div_" + div + " #Prices").val(0);
        }
    }
    function OpendailogHouse(houseid) {
        var url = 'House/Templete/HouseTemplete.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseid;
        var options = { width: 860, height: 820, mixable: false };
        window.top.$.pdialog.open(url, "sunfunTemplete_" + houseid, "编辑房源描述", options);
    }
    function MinDalog() {
        var dialoga = $("body").data("housewiew2_<%=Entity.HouseID %>");
        $.pdialog.minimize(dialoga);
        $("div.shadow").remove();
    }

    function ShowRemark(hs_hid_b) {
        var Remark = HouseMIS.Web.House.HouseForm.GetRemark(hs_hid_b).value;
        $("#div_" + hs_hid_b + " .frmRemarks:eq(0)").val(Remark);
    }
    function fbwshow(id) {
        if (document.getElementById("fbwz" + id).style.display == "none") {
            document.getElementById("fbwz" + id).style.display = "block";
        } else {
            document.getElementById("fbwz" + id).style.display = "none";
        }
    }

    function HousePicHX(h_phx_b) {
        ops = { width: 760, height: 630 };
        $.pdialog.open("House/HousePic.aspx?PicTypeID=1&HouseID=" + h_phx_b, "housePicDialog", "户型图", ops);
    }

    function ClickButs(hid_cb_b) {
        if ($("#div_" + hid_cb_b + " .frmHouseDicID:eq(0)").val() == "" || $("#div_" + hid_cb_b + " .frmHouseDicID:eq(0)").val() == "0") {
            alertMsg.error("楼盘请重新输入然后选择提示里您需要的楼盘！");
            $("#div_" + hid_cb_b + " .frmHouseDicName:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;

        }
        if ($("#div_" + hid_cb_b + " .frmSanjakID:eq(0)").val() == "" || $("#div_" + hid_cb_b + " .frmSanjakID:eq(0)").val() == "0") {
            alertMsg.error("楼盘请重新输入然后选择提示里您需要的楼盘！");
            $("#div_" + hid_cb_b + " .frmSanjakID:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmLabel:eq(0)").val() == 2) {
            if ($("#div_" + hid_cb_b + " .frmSchool:eq(0)").val() == "") {
                alertMsg.error("学校不能为空！");
                $("#div_" + hid_cb_b + " .frmSchool:eq(0)").focus();
                $("#AddBtnHouseFrom").show();
                return false;
            }
        }
        else if ($("#div_" + hid_cb_b + " .frmLabel:eq(0)").val() == 5) {
            if ($("#div_" + hid_cb_b + " .frmMetro:eq(0)").val() == "") {
                alertMsg.error("地铁不能为空！");
                $("#div_" + hid_cb_b + " .frmMetro:eq(0)").focus();
                $("#AddBtnHouseFrom").show();
                return false;
            }
        }

        var temp = $("#div_" + hid_cb_b + " .frmEntrustTypeID:eq(0)");
        //速销代卖和十万火急
        if (temp.val() == 4 || temp.val() == 7) {
            if ($("#div_" + hid_cb_b + " .frmFast_UserID:eq(0)").val() == "") {
                alertMsg.error("责任人不能为空！");
                $("#div_" + hid_cb_b + " .frmFast_UserID:eq(0)").focus();
                $("#AddBtnHouseFrom").show();
                return false;
            }
            if ($("#div_" + hid_cb_b + " .frmFast_Date:eq(0)").val() == "") {
                alertMsg.error("到期日不能为空！");
                $("#div_" + hid_cb_b + " .frmFast_Date:eq(0)").focus();
                $("#AddBtnHouseFrom").show();
                return false;
            }
        }

        var str = "（1）该房屋在小区的位置（比如：沿河、沿马路、小区中心位置等）（2）所有房间的朝向（3）客厅属于明厅还是暗厅（4）装修的年数（5）房屋的明显优缺点（如全部是品牌家电，房屋有漏水、暗厕等。）（6）有阁楼、院子等须注明。（7）方便看房时间（8）房屋状态（空关、出租、自住等）";
        //房源描述
        var LinkTel1 = $.trim($("#div_" + hid_cb_b + " .frmLinkTel1:eq(0)").val().replaceAll("\\s*", "").replaceAll(str, ""));

        if (LinkTel1.length > 0 && LinkTel1.length < 30) {
            alertMsg.error("房源描述不能少于30个字！");
            $("#div_" + hid_cb_b + " .frmLinkTel1:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }

        //看房时间
        var temp_SeeHouseID = $.trim($("#div_" + hid_cb_b + " .frmSeeHouseID:eq(0)").val());
        //房屋情况
        var temp_NowStateID = $.trim($("#div_" + hid_cb_b + " .frmNowStateID:eq(0)").val());
        //税费
        var temp_TaxesID = $.trim($("#div_" + hid_cb_b + " .frmTaxesID:eq(0)").val());
        //产证情况
        var temp_AssortID = $.trim($("#div_" + hid_cb_b + " .frmAssortID:eq(0)").val());
        //光线情况
        var temp_SaleMotiveID = $.trim($("#div_" + hid_cb_b + " .frmSaleMotiveID:eq(0)").val());
        //外墙
        var temp_ApplianceID = $.trim($("#div_" + hid_cb_b + " .frmApplianceID:eq(0)").val());
        //带看人
        var temp_PayServantID = $.trim($("#div_" + hid_cb_b + " .frmPayServantID:eq(0)").val());

        var CanSave = HouseMIS.Web.House.HouseForm.CanSave(hid_cb_b, LinkTel1, temp_SeeHouseID, temp_NowStateID, temp_TaxesID, temp_AssortID, temp_SaleMotiveID, temp_ApplianceID, temp_PayServantID).value;

        if (CanSave) {
            //判断已经填写了房源描述，则其他信息也都要填写
            if (LinkTel1.length > 0) {
                if (temp_SeeHouseID.length == 0 || temp_NowStateID.length == 0 || temp_TaxesID.length == 0 || temp_AssortID.length == 0 || temp_SaleMotiveID.length == 0 || temp_ApplianceID.length == 0 || temp_PayServantID.length == 0) {
                    alertMsg.error("房源描述填写后，其他信息的内容必须填写（如看房时间，房屋情况等）！");
                    $("#div_" + hid_cb_b + " .frmSeeHouseID:eq(0)").focus();
                    $("#AddBtnHouseFrom").show();
                    return false;
                }
            }
            //判断已经填写了其他信息，则必须填写房源描述
            else if (temp_SeeHouseID.length > 0 || temp_NowStateID.length > 0 || temp_TaxesID.length > 0 || temp_AssortID.length > 0 || temp_SaleMotiveID.length > 0 || temp_ApplianceID.length > 0 || temp_PayServantID.length > 0) {
                alertMsg.error("其他信息的内容填写后，房源描述必须填写！");
                $("#div_" + hid_cb_b + " .frmLinkTel1:eq(0)").focus();
                $("#AddBtnHouseFrom").show();
                return false;
            }
        }

        if ($("#div_" + hid_cb_b + " .frmHouseDicName:eq(0)").val() == "") {
            alertMsg.error("楼盘名称不能为空！");
            $("#div_" + hid_cb_b + " .frmHouseDicName:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmform_bedroom:eq(0)").val() == "") {
            alertMsg.error("室不能为空！");
            $("#div_" + hid_cb_b + " .frmform_bedroom:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmform_foreroom:eq(0)").val() == "") {
            alertMsg.error("厅不能为空！");
            $("#div_" + hid_cb_b + " .frmform_foreroom:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        var hid = "<%=Entity.HouseID %>";
        if (hid == "0") {
            if ($("#div_" + hid_cb_b + " .frmlandlord_tel2:eq(0)").val() == "") {
                alertMsg.error("业主电话不能为空！");
                $("#div_" + hid_cb_b + " .frmlandlord_tel2:eq(0)").focus();
                $("#AddBtnHouseFrom").show();
                return false;
            }
            else if ($("#div_" + hid_cb_b + " .frmlandlord_tel2:eq(0)").val().length < 10 || $("#div_" + hid_cb_b + " .frmlandlord_tel2:eq(0)").val().length > 12) {
                alertMsg.error("业主电话小于10位数字或大于12位数字，请重新输入！");
                $("#div_" + hid_cb_b + " .frmlandlord_tel2:eq(0)").focus();
                $("#AddBtnHouseFrom").show();
                return false;
            }
            if ($("#div_" + hid_cb_b + " .frmlandlord_name:eq(0)").val() == "") {
                alertMsg.error("房东姓名不能为空！");
                $("#div_" + hid_cb_b + " .frmlandlord_name:eq(0)").focus();
                $("#AddBtnHouseFrom").show();
                return false;
            }
        }
        if ($("#div_" + hid_cb_b + " .frmIsSuburbs:eq(0)").val() == "") {
            alertMsg.error("郊县分类不能为空！");
            $("#div_" + hid_cb_b + " .frmIsSuburbs:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmbuild_levels:eq(0)").val() == "0") {
            alertMsg.error("总楼层不能为0！");
            $("#div_" + hid_cb_b + " .frmbuild_levels:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if (parseInt($("#div_" + hid_cb_b + " .frmbuild_floor:eq(0)").val()) > parseInt($("#div_" + hid_cb_b + " .frmbuild_levels:eq(0)").val())) {
            alertMsg.error("所在楼层不能大于总楼层！");
            $("#div_" + hid_cb_b + " .frmbuild_floor:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmBuild_area:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmBuild_area:eq(0)").val() == "") {
            alertMsg.error("面积不能为0！");
            $("#div_" + hid_cb_b + " .frmBuild_area:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        //总价
        var Sum_price = $("#div_" + hid_cb_b + " .frmSum_price:eq(0)");
        if (Sum_price.val() == "0" || Sum_price.val() == "") {
            alertMsg.error("总价不能为0！");
            Sum_price.focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }

        if ($("#div_" + hid_cb_b + " .frmMin_priceb:eq(0)").val() == "" || $("#div_" + hid_cb_b + " .frmMin_priceb:eq(0)").val() == "0") {
            $("#div_" + hid_cb_b + " .frmMin_priceb:eq(0)").val($("#div_" + hid_cb_b + " .frmSum_price:eq(0)").val());
            //$("#div_" + hid_cb_b + " .frmMin_price:eq(0)").val($("#div_" + hid_cb_b + " .frmSum_price:eq(0)").val());
        }
        if ($("#div_" + hid_cb_b + " .frmMin_priceb:eq(0)").val() != $("#div_" + hid_cb_b + " .frmMin_price:eq(0)").val() && $("#div_" + hid_cb_b + " .frmMin_priceb:eq(0)").val() != "***") {
            $("#div_" + hid_cb_b + " .frmMin_price:eq(0)").val($("#div_" + hid_cb_b + " .frmMin_priceb:eq(0)").val());
        }

        var min_price = $("#div_" + hid_cb_b + " .frmMin_price:eq(0)");
        if ((min_price.val() != "0" && min_price.val() != "") && parseFloat(min_price.val()) > parseFloat(Sum_price.val())) {
            alertMsg.error("实价不能大于总价！");
            Sum_price.focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }

        if ($("#div_" + hid_cb_b + " .frmFitmentID:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmFitmentID:eq(0)").val() == "") {
            alertMsg.error("装修不能为空！");
            $("#div_" + hid_cb_b + " .frmFitmentID:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmYearID:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmYearID:eq(0)").val() == "") {
            alertMsg.error("年代不能为空！");
            $("#div_" + hid_cb_b + " .frmYearID:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmTypeCode:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmTypeCode:eq(0)").val() == "") {
            alertMsg.error("类型不能为空！");
            $("#div_" + hid_cb_b + " .frmTypeCode:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmUseID:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmUseID:eq(0)").val() == "") {
            alertMsg.error("用途不能为空！");
            $("#div_" + hid_cb_b + " .frmUseID:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmPropertyID:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmPropertyID:eq(0)").val() == "") {
            alertMsg.error("产权不能为空！");
            $("#div_" + hid_cb_b + " .frmPropertyID:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmpropertyYear:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmpropertyYear:eq(0)").val() == "") {
            alertMsg.error("产权年限不能为空！");
            $("#div_" + hid_cb_b + " .frmpropertyYear:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmFacetoID:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmFacetoID:eq(0)").val() == "") {
            alertMsg.error("朝向不能为空！");
            $("#div_" + hid_cb_b + " .frmFacetoID:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        if ($("#div_" + hid_cb_b + " .frmEntrustTypeID:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmEntrustTypeID:eq(0)").val() == "") {
            alertMsg.error("委托不能为空！");
            $("#div_" + hid_cb_b + " .frmEntrustTypeID:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }

        if ($("#div_" + hid_cb_b + " .frmLandlordID:eq(0)").val() == "0" || $("#div_" + hid_cb_b + " .frmLandlordID:eq(0)").val() == "") {
            alertMsg.error("委托人不能为空！");
            $("#div_" + hid_cb_b + " .frmLandlordID:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }
        //委托单号,新增时不能为0
        if (parseInt($("#div_" + hid_cb_b + " .frmorderNum:eq(0)").val()) == 0 && parseInt(<%=Entity.HouseID %>) == 0) {
            alertMsg.error("必须填写委托单号，并且不能为0，请重新输入！");
            $("#div_" + hid_cb_b + " .frmorderNum:eq(0)").focus();
            $("#AddBtnHouseFrom").show();
            return false;
        }

        var Repeat = HouseMIS.Web.House.HouseForm.GetRepeat().value;

        if (Repeat.indexOf("form_bedroom") > 0) {
            Repeat = Repeat.replace(/@form_bedroom/, $("#div_" + hid_cb_b + " .frmform_bedroom:eq(0)").val()).replace(/@form_foreroom/, $("#div_" + hid_cb_b + " .frmform_foreroom:eq(0)").val()).replace(/@form_toilet/, $("#div_" + hid_cb_b + " .frmform_toilet:eq(0)").val()).replace(/@form_terrace/, $("#div_" + hid_cb_b + " .frmform_terrace:eq(0)").val());
        }
        if (Repeat.indexOf("build_floor") > 0) {
            Repeat = Repeat.replace(/@build_floor/, $("#div_" + hid_cb_b + " .frmbuild_floor:eq(0)").val()).replace(/@build_levels/, $("#div_" + hid_cb_b + " .frmbuild_levels:eq(0)").val());
        }
        if (Repeat.indexOf("YearID") > 0) {
            Repeat = Repeat.replace(/@YearID/, $("#div_" + hid_cb_b + " .frmYearID:eq(0)").val());
        }
        if (Repeat.indexOf("FacetoID") > 0) {
            Repeat = Repeat.replace(/@FacetoID/, $("#div_" + hid_cb_b + " .frmFacetoID:eq(0)").val());
        }
        if (Repeat.indexOf("EntrustTypeID") > 0) {
            Repeat = Repeat.replace(/@EntrustTypeID/, $("#div_" + hid_cb_b + " .frmEntrustTypeID:eq(0)").val());
        }
        if (Repeat.indexOf("FitmentID") > 0) {
            Repeat = Repeat.replace(/@FitmentID/, $("#div_" + hid_cb_b + " .frmFitmentID:eq(0)").val());
        }
        if (Repeat.indexOf("SanjakID") > 0) {
            Repeat = Repeat.replace(/@SanjakID/, $("#div_" + hid_cb_b + " .frmSanjakID:eq(0)").val());
        }
        if (Repeat.indexOf("UseID") > 0) {
            Repeat = Repeat.replace(/@UseID/, $("#div_" + hid_cb_b + " .frmUseID:eq(0)").val());
        }
        if (Repeat.indexOf("TypeCode") > 0) {
            Repeat = Repeat.replace(/@TypeCode/, $("#div_" + hid_cb_b + " .frmTypeCode:eq(0)").val());
        }
        if (Repeat.indexOf("VisitID") > 0) {
            Repeat = Repeat.replace(/@VisitID/, $("#div_" + hid_cb_b + " .frmVisitID:eq(0)").val());
        }

        //楼栋号
        var b_id = "";
        var b_unit = "";
        //室号
        var b_room = "";
        var landlord_tels = "";
        if ($("#div_" + hid_cb_b + " .frmbuild_room:eq(0)").val() != "***") {
            b_id = $("#div_" + hid_cb_b + " .frmbuild_id:eq(0)").val();
            b_unit = $("#div_" + hid_cb_b + " .frmbuild_unit:eq(0)").val();
            b_room = $("#div_" + hid_cb_b + " .frmbuild_room:eq(0)").val();
            landlord_tels = $("#div_" + hid_cb_b + " .frmlandlord_tel2:eq(0)").val();
        }

        if ($("#div_" + hid_cb_b + " .frmHouseDicID:eq(0)").val().indexOf("|") > 0) {
            var arry = $("#div_" + hid_cb_b + " .frmHouseDicID:eq(0)").val().split("|");
            $("#div_" + hid_cb_b + " .frmHouseDicID:eq(0)").val(arry[0]);
            $("#div_" + hid_cb_b + " .frmSanjakID:eq(0)").val(arry[1]);
        }

        //小区名称
        var HouseDicName = $.trim($("#div_" + hid_cb_b + " .frmHouseDicName:eq(0)").val());

        $.ajax({
            url: 'House/HouseRepeatExt.ashx',
            data: {
                HTML: Repeat,
                Build_area: $("#div_" + hid_cb_b + " .frmBuild_area:eq(0)").val(),
                sum_price: $("#div_" + hid_cb_b + " .frmSum_price:eq(0)").val(),
                HouseDicName: HouseDicName,
                HouseDicAddress: $("#div_" + hid_cb_b + " .frmHouseDicAddress:eq(0)").val(),
                build_id: b_id,
                build_unit: b_unit,
                build_room: b_room,
                landlord_name: $("#div_" + hid_cb_b + " .frmlandlord_name:eq(0)").val(),
                landlord_tel2: landlord_tels,
                HouseID: hid_cb_b,
                aType: 0
            },
            //data: { HTML: "", Build_area: $("#div_" + hid_cb_b + " .frmBuild_area:eq(0)").val(), sum_price: $("#div_" + hid_cb_b + " .frmSum_price:eq(0)").val(), HouseDicName: $("#div_" + hid_cb_b + " .frmHouseDicName:eq(0)").val(), HouseDicAddress: $("#div_" + hid_cb_b + " .frmHouseDicAddress:eq(0)").val(), build_id: b_id, build_unit: b_unit, build_room: b_room, landlord_name: $("#div_" + hid_cb_b + " .frmlandlord_name:eq(0)").val(), landlord_tel2: landlord_tels, HouseID: hid_cb_b,aType:0 },
            success: function (result) { if (result == "1") { alertMsg.error("不能输入重复的房源信息！"); $("#AddBtnHouseFrom").show(); } else { $("#div_" + hid_cb_b + " .hf_from1:eq(0)").submit() } }
        });

        //保存按钮隐藏
        //$("#hf_Buttom").hide();
        //$("#AddBtnHouseFrom").hide();
    }
</script>
<body>
    <div class="pageContent" id="div_<%=Entity.HouseID %>">
        <form id="hf_from1" runat="server" action="House/HouseForm.aspx" class="pageForm required-validate hf_from1"
            onsubmit="return validateCallback(this, dialogAjaxDoneHouseF)">
            <%--<input type="hidden" name="EditType" value="<%=EditType %>" />--%>
            <%--<input type="hidden" name="KeyValue" value="<%=KeyValue %>" />--%>
            <%--<input type="hidden" name="NavTabId" value="<%= NavTabId %>" />--%>
            <input type="hidden" id="orderNum_old" runat="server" value="0" />
            <input type="hidden" name="phoneID" id="phoneID" runat="server" />
            <input type="hidden" id="hid_<%=Entity.HouseID %>" class="hidden_houset" value="<%=Entity.HouseID %>" />
            <div class="tabs" currentindex="0" eventtype="click">
                <div class="tabsHeader">
                    <div class="tabsHeaderContent">
                        <ul>
                            <li><a href="javascript:;"><span>房源信息</span></a></li>
                            <li><a href="House/HouseFromGJ.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>跟进</span></a></li>
                            <li><a href="House/HouseFromDK.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>带看</span></a></li>
                            <li><a href="House/HouseFromDK_JF.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>积分带看</span></a></li>
                            <li><a href="House/HouseFromCD.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>查电</span></a></li>
                            <%--<li><a href="House/HouseFromQJ.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>全景</span></a></li>--%>
                            <li><a href="House/HouseFromZP.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>照片</span></a></li>
                            <li><a href="javascript:;"><span>制度</span></a></li>
                            <li><a href="House/HouseFromEWM.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>二维码</span></a></li>
                            <li><a href="House/HouseFromSP.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>&shi_id=<%=Entity.Shi_id %>" rel="follow_ex" class="j-ajax"><span>视频</span></a></li>
                            <li><a href="House/HouseFromLY.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>录音</span></a></li>
                            <li><a href="House/HouseFromXX.aspx?NavTabId=<%=NavTabId %>&HouseID=<%=Entity.HouseID %>" rel="follow_ex" class="j-ajax"><span>详细</span></a></li>
                        </ul>
                    </div>
                </div>
                <div class="tabsContent" style="height: auto; overflow: auto">
                    <div style="height: 560px;">
                        <table border="0" width="100%" height="100%">
                            <tr>
                                <td>
                                    <fieldset>
                                        <legend><a id="showAHouse" runat="server" width="530" height="300" mixable="false">基本信息 [<%=Entity.SeeHouseType%>]</a>房源编号:<%=shi_ids %><%=Entity.Shi_id %><%--,编号:<%=Entity.HouseID %>--%></legend>
                                        <table width="603" height="306" border="0" cellpadding="2" cellspacing="2" class="test_Tab_House">
                                            <tr>
                                                <td colspan="4">
                                                    <asp:TextBox runat="server" Width="113px" ID="frmshi_id" Style="display: none" CssClass="frmshi_id"></asp:TextBox>
                                                    <div id="seeHouseRoom">
                                                        <input type="button" value="查看门牌" class="close" id="exit_kj<%=Entity.HouseID %>" /><asp:Label ID="seeNum" runat="server"></asp:Label>
                                                    </div>

                                                    <table border="0" cellpadding="0" cellspacing="0" height="100%" style="width: 100%" id="exit_bxs<%=Entity.HouseID %>">
                                                        <tr>
                                                            <td width="70px">楼栋号</td>
                                                            <td width="127">
                                                                <asp:DropDownList ID="frmbID" CssClass="Hselect frmbID" name="frmbID" runat="server" Style="width: 80px; float: left">
                                                                </asp:DropDownList>
                                                                <asp:TextBox runat="server" CssClass="Hinput frmbuild_id" ID="frmbuild_id" name="h.frmbuild_id"
                                                                    Style="width: 72px; float: left" onkeyup="value=value.replace(/[^\a-\z\A-\Z0-9\-]/g,'')" onafterpaste="value=value.replace(/[^\a-\z\A-\Z0-9\-]/g,'')"></asp:TextBox>
                                                            </td>
                                                            <%-- <td width="40" style="display: none;">
                                                                <span class="unit">单元</span>
                                                            </td>
                                                            <td style="display: none;">
                                                            <span class="unit">
                                                            <asp:DropDownList ID="frmUnitID" CssClass="Hselect frmUnitID" name="frmUnitID" runat="server" Style="width: 80px; float: left">
                                                            </asp:DropDownList>
                                                            <asp:TextBox runat="server" CssClass="Hinput frmbuild_unit" ID="frmbuild_unit" name="h.frmbuild_unit" Style="width: 72px; float: left" onkeyup="value=value.replace(/[^\a-\z\A-\Z0-9\u4E00-\u9FA5]/g,'')" onafterpaste="value=value.replace(/[^\a-\z\A-\Z0-9\u4E00-\u9FA5]/g,'')"></asp:TextBox></span>
                                                            </td>--%>
                                                            <td width="40">室号</td>
                                                            <td>
                                                                <asp:DropDownList ID="frmRoomID" CssClass="Hselect frmRoomID" name="frmRoomID" runat="server" Style="width: 80px; float: left">
                                                                </asp:DropDownList>
                                                                <asp:TextBox runat="server" CssClass="Hinput frmbuild_room digits" ID="frmbuild_room" name="h.frmbuild_room"
                                                                    Style="width: 72px; float: left" onkeyup="value=value.replace(/[^\a-\z\A-\Z0-9]/g,'')" onafterpaste="value=value.replace(/[^\a-\z\A-\Z0-9]/g,'')"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td colspan="1">
                                                    <%=yysys%>
                                                </td>
                                                <td colspan="1">
                                                    <%=qxyy%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">楼盘字典<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <asp:TextBox runat="server" ID="frmHouseDicID" CssClass="frmHouseDicID" Style="display: none;"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="frmSanjakID" CssClass="frmSanjakID" Style="display: none;"></asp:TextBox>
                                                    <asp:TextBox runat="server" ID="frmHouseDicName" CssClass="frmHouseDicName" Style="width: 113px; float: left"></asp:TextBox>
                                                </td>
                                                <td width="74">楼盘地址<span style="color: red">*</span>
                                                </td>
                                                <td width="127" colspan="3">
                                                    <asp:TextBox runat="server" ID="frmHouseDicAddress" CssClass="frmHouseDicAddress"
                                                        Width="98%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">区域<span style="color: red">*</span></td>
                                                <td width="127">
                                                    <asp:DropDownList ID="ddlArea" runat="server" CssClass="ddlArea" Width="118px">
                                                    </asp:DropDownList>
                                                </td>
                                                <script type="text/javascript">
                                                    function GetSanjak() {
                                                        var areaid = $("#ddlArea").val();
                                                        $("#ddlSanjakID").html(HouseMIS.Web.House.HouseForm.GetSanjak(areaid).value);
                                                    }
                                                </script>
                                                <td width="74">商圈<span style="color: red">*</span></td>
                                                <td width="127">
                                                    <asp:DropDownList ID="ddlSanjakID" runat="server" CssClass="ddlSanjakID" Width="118px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="74">住宅类型<span style="color: red">*</span></td>
                                                <td>
                                                    <asp:DropDownList ID="frmTypeCode" runat="server" Width="130px" CssClass="frmTypeCode">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td height="24" colspan="4">
                                                    <table border="0" cellpadding="0" cellspacing="0" height="100%" style="width: 100%;">
                                                        <tr>
                                                            <td height="24" width="70">户型<span style="color: red">*</span>
                                                            </td>
                                                            <td class="style8">
                                                                <asp:DropDownList ID="frmform_bedroom" CssClass="frmform_bedroom" runat="server"
                                                                    Width="55px">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>0</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                    <asp:ListItem>6</asp:ListItem>
                                                                    <asp:ListItem>7</asp:ListItem>
                                                                </asp:DropDownList>
                                                                室
                                                        </td>
                                                            <td class="style9">
                                                                <asp:DropDownList ID="frmform_foreroom" CssClass="frmform_foreroom" runat="server"
                                                                    Width="55px">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem>0</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                </asp:DropDownList>
                                                                厅
                                                        </td>
                                                            <td class="style6">
                                                                <asp:DropDownList ID="frmform_toilet" CssClass="frmform_toilet" runat="server" Width="55px">
                                                                    <asp:ListItem>0</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                </asp:DropDownList>
                                                                卫
                                                        </td>
                                                            <td>
                                                                <asp:DropDownList ID="frmform_terrace" CssClass="frmform_terrace" runat="server"
                                                                    Width="55px">
                                                                    <asp:ListItem>0</asp:ListItem>
                                                                    <asp:ListItem>1</asp:ListItem>
                                                                    <asp:ListItem>2</asp:ListItem>
                                                                    <asp:ListItem>3</asp:ListItem>
                                                                    <asp:ListItem>4</asp:ListItem>
                                                                    <asp:ListItem>5</asp:ListItem>
                                                                </asp:DropDownList>
                                                                阳台
                                                        </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td colspan="2">
                                                    <table border="0" cellpadding="0" cellspacing="0" height="100%" style="float: left; width: 100%;">
                                                        <tr>
                                                            <td width="74">楼层<span style="color: red">*</span>
                                                            </td>
                                                            <td width="50">
                                                                <asp:DropDownList ID="frmbuild_floor" CssClass="frmbuild_floor" runat="server" Width="60px"
                                                                    Style="float: left;">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="10">
                                                                <div align="center">
                                                                    /
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="frmbuild_levels" CssClass="frmbuild_levels" runat="server"
                                                                    Width="60px" Style="float: left;">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">面积<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <asp:TextBox runat="server" Width="113px" ID="frmBuild_area" CssClass="frmBuild_area"></asp:TextBox>
                                                </td>
                                                <td width="74">总价<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <asp:TextBox runat="server" CssClass="frmSum_price" Width="100px" ID="frmSum_price"></asp:TextBox>万
                                            </td>
                                                <td width="74">实价<span style="color: red">*</span>
                                                    <a id="btnPrice" runat="server" class="btnLook" style="float: right" href='House/FollowUpPriceList.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=Entity.HouseID %>'
                                                        target='dialog' width='750' height='400' fresh='true' maxable='false' title="压价跟进"
                                                        rel="FollowUpPriceList"></a>
                                                </td>
                                                <td width="127">
                                                    <asp:TextBox runat="server" CssClass="frmMin_priceb" Width="125px" ID="frmMin_priceb" ReadOnly="true"></asp:TextBox>
                                                    <asp:TextBox runat="server" Style="display: none;" CssClass="frmMin_price" Width="125px"
                                                        ID="frmMin_price"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">单价
                                            </td>
                                                <td width="127">
                                                    <asp:TextBox runat="server" Width="113px" ID="Prices" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td width="74">装修<span style="color: red">*</span></td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmFitmentID" runat="server" Width="118px" CssClass="frmFitmentID">
                                                    </asp:DropDownList></td>
                                                <td width="74">年代<span style="color: red">*</span></td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmYearID" runat="server" Width="130px" CssClass="frmYearID">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td width="74">用途<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmUseID" runat="server" Width="118px" CssClass="frmUseID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="74">产权<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmPropertyID" runat="server" Width="118px" CssClass="frmPropertyID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="74">委托人<span style="color: red">*</span></td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmLandlordID" runat="server" Width="130px" CssClass="frmLandlordID">
                                                    </asp:DropDownList>
                                                    <%--onchange="showGZS()"--%>
                                                    <%--<script>
                                                        function showGZS() {
                                                            if ($("#div_<%=Entity.HouseID %> .frmLandlordID:eq(0)").val() == 2) {
                                                                $("#div_<%=Entity.HouseID %> .gzs_i1:eq(0)").show();
                                                                $("#div_<%=Entity.HouseID %> .gzs_i1:eq(1)").show();
                                                                $("#div_<%=Entity.HouseID %> .gzs_i1:eq(2)").show();
                                                                $("#div_<%=Entity.HouseID %> .gzs_i1:eq(3)").show();
                                                            } else {
                                                                $("#div_<%=Entity.HouseID %> .gzs_i1:eq(0)").hide();
                                                                $("#div_<%=Entity.HouseID %> .gzs_i1:eq(1)").hide();
                                                                $("#div_<%=Entity.HouseID %> .gzs_i1:eq(2)").hide();
                                                                $("#div_<%=Entity.HouseID %> .gzs_i1:eq(3)").hide();
                                                            }
                                                        }
                                                    </script>--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">朝向<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmFacetoID" runat="server" Width="118px" CssClass="frmFacetoID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="74">来源</td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmVisitID" runat="server" Width="118px" CssClass="frmVisitID">
                                                    </asp:DropDownList></td>
                                                <td width="74">标签</td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmLabel" CssClass="frmLabel" runat="server" Width="130px">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="1">经济型</asp:ListItem>
                                                        <asp:ListItem Value="2">学区房</asp:ListItem>
                                                        <asp:ListItem Value="3">地段房</asp:ListItem>
                                                        <asp:ListItem Value="4">老城区</asp:ListItem>
                                                        <asp:ListItem Value="5">地铁房</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td width="74">委托<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmEntrustTypeID" runat="server" Width="118px" CssClass="frmEntrustTypeID" onchange="showDM()">
                                                    </asp:DropDownList>
                                                    <script>
                                                        function showDM() {
                                                            var temp = $("#div_<%=Entity.HouseID %> .frmEntrustTypeID:eq(0)");
                                                            //速销代卖和十万火急
                                                            if (temp.val() == 4 || temp.val() == 7) {
                                                                $("#tblFast").show();
                                                            } else {
                                                                $("#tblFast").hide();
                                                            }
                                                        }
                                                    </script>
                                                </td>
                                                <td width="74">委托日期
                                            </td>
                                                <td width="127">
                                                    <asp:TextBox runat="server" Width="113px" ID="frmEntrust_Date" class="date" yearstart="-80"
                                                        yearend="5" ReadOnly="true" CssClass="frmEntrust_Date"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblstate" runat="server" Visible="false">状态<span style="color: red">*</span></asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="frmStateID" runat="server" Width="130px" CssClass="frmStateID" Visible="false">
                                                    </asp:DropDownList></td>
                                            </tr>
                                            <tr>
                                                <td width="74">房东<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <asp:TextBox runat="server" Width="113px" ID="frmlandlord_name" CssClass="frmlandlord_name"></asp:TextBox>
                                                </td>
                                                <td width="74">业主电话<span style="color: red">*</span>
                                                </td>
                                                <td width="127">
                                                    <%--<asp:HiddenField runat="server" ID="GU_IDS"></asp:HiddenField>--%>
                                                    <a id="htlA" runat="server" rel="HouseTelLook">
                                                        <asp:TextBox runat="server" Width="115px" CssClass="frmlandlord_tel2" ID="frmlandlord_tel2" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox></a>
                                                </td>
                                                <td>委托单号<span style="color: red">*</span></td>
                                                <td>
                                                    <asp:TextBox ID="frmorderNum" runat="server" CssClass="frmorderNum required digits"
                                                        Style="width: 125px; float: left" MaxLength="8" onkeyup="value=value.replace(/[^\a-\z\A-\Z0-9]/g,'')" onafterpaste="value=value.replace(/[^\a-\z\A-\Z0-9]/g,'')"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <table id="tblFast" runat="server" class="tblFast" style="display: none" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="74">责任人</td>
                                                            <td width="127">
                                                                <asp:TextBox ID="frmFast_UserID" CssClass="frmFast_UserID" Style="display: none" name="h.frmBeltmanID" runat="server"
                                                                    Width="114px"></asp:TextBox>
                                                                <asp:TextBox ID="frmem_name" name="h.frmem_name" runat="server" ReadOnly="true" Width="90px"></asp:TextBox>
                                                                <a class="btnLook" id="A1" href="Employee/emlookup.aspx?sType=cb" lookupgroup="h"
                                                                    style="float: right"></a></td>
                                                            <td width="74">到期日</td>
                                                            <td width="127">
                                                                <asp:TextBox runat="server" Width="115px" ID="frmFast_Date" class="date" yearstart="-80"
                                                                    yearend="5" ReadOnly="true" CssClass="frmFast_Date"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">备注
                                                </td>
                                                <td colspan="5" valign="top" style="padding: 0px;">
                                                    <table width="98%">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox runat="server" Width="240px" ID="frmnote" Height="36px" TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 30px; vertical-align: text-top; text-align: center;">设施
                                                            </td>
                                                            <td>
                                                                <a class="button" id="aSelFacility" runat="server" href="House/FacilityList.aspx" lookupgroup="F"
                                                                    width="600" height="160"><span>选择</span></a>
                                                                <asp:TextBox runat="server" Width="190px" ID="frmFacility" name="F.frmFacility" Height="36px" ReadOnly="true"
                                                                    TextMode="MultiLine"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">学校
                                                </td>
                                                <td colspan="5" valign="top" style="padding: 0px;">
                                                    <table width="98%">
                                                        <tr>
                                                            <td style="width: 220px;">
                                                                <asp:TextBox runat="server" Width="215px" ID="frmSchool" CssClass="frmSchool" name="SL.frmSchool" Style="float: left;"></asp:TextBox>
                                                            </td>
                                                            <td style="vertical-align: text-top; text-align: center; width: 60px;">钥匙情况
                                                            </td>
                                                            <td>
                                                                <%=z_keys() %>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">地铁</td>
                                                <td colspan="5" valign="top" style="padding: 0px;">
                                                    <table width="98%">
                                                        <tr>
                                                            <td style="width: 220px;">
                                                                <asp:TextBox runat="server" Width="215px" ID="frmMetro" CssClass="frmMetro" Style="float: left;" name="M.frmMetro"></asp:TextBox></td>
                                                            <td style="vertical-align: text-top; text-align: center; width: 60px;">产权年限<span style="color: red">*</span>
                                                                <%--<asp:Label ID="fydj" runat="server" Visible="false">房源等级</asp:Label>--%>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="frmpropertyYear" runat="server" Width="118px" CssClass="frmpropertyYear">
                                                                </asp:DropDownList>
                                                                <%--<asp:DropDownList ID="frmLevelType" CssClass="frmLevelType" runat="server" Width="130px" Visible="false">
                                                                    <asp:ListItem></asp:ListItem>
                                                                    <asp:ListItem Value="1">A</asp:ListItem>
                                                                    <asp:ListItem Value="2">B</asp:ListItem>
                                                                    <asp:ListItem Value="3">C</asp:ListItem>
                                                                    <asp:ListItem Value="4">D</asp:ListItem>
                                                                </asp:DropDownList>--%>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="74">保密说明<br />
                                                    <a class="btnLook" id="LookRemarks" style="cursor: pointer;" onclick="ShowRemark(<%=Entity.HouseID %>)"></a>
                                                </td>
                                                <td colspan="5" valign="top">
                                                    <asp:TextBox runat="server" Width="520px" CssClass="frmRemarks" ID="frmRemarks" Height="36px"
                                                        TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="23" colspan="5">
                                                    <label>
                                                        <%--<asp:CheckBox runat="server" ID="frmpastdue" Text="满2年 "></asp:CheckBox>--%>
                                                        <asp:CheckBox runat="server" ID="frmIsLock" Text="锁盘 "></asp:CheckBox>
                                                        <asp:CheckBox runat="server" ID="frmIsOnlyOne" Text="家庭唯一"></asp:CheckBox>
                                                        <asp:CheckBox runat="server" ID="frmIsElevator" Text="电梯 "></asp:CheckBox>
                                                    </label>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                    <fieldset <%=isDisplay %>>
                                        <legend>房源核验积分项</legend>
                                        <table width="603" height="120" border="0" cellpadding="2" cellspacing="2" class="test_Tab">
                                            <tr>
                                                <td colspan="6">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="70"><span style="color: Red;">描述</span><%--<span style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("填写房源描述") %></span>--%></td>
                                                            <td>
                                                                <asp:TextBox runat="server" Width="516px" CssClass="frmLinkTel1" ID="frmLinkTel1" Height="80px"
                                                                    TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="23" class="style16">
                                                    <%--<span class="HPoint" style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正看房时间") %></span>--%><span style="color: Red;">看房时间</span>
                                                </td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmSeeHouseID" runat="server" Width="93px" CssClass="frmSeeHouseID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="74">
                                                    <%--<span class="HPoint" style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正房屋现状") %></span>--%><span style="color: Red;">房屋情况</span>
                                                </td>
                                                <td width="127">
                                                    <asp:DropDownList ID="frmNowStateID" runat="server" Width="93px" CssClass="frmNowStateID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td height="23"><span style="color: Red;">税费</span>
                                                </td>
                                                <td height="23">
                                                    <asp:DropDownList ID="frmTaxesID" runat="server" Width="118px" CssClass="frmTaxesID">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="21" class="style16">
                                                    <%--<span class="HPoint" style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正房源产证情况") %></span>--%><span style="color: Red;">产证情况</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="frmAssortID" runat="server" Width="93px" CssClass="frmAssortID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <%--<span class="HPoint" style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("光线情况") %></span>--%><span style="color: Red;">光线情况</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="frmSaleMotiveID" runat="server" Width="93px" CssClass="frmSaleMotiveID">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <%--<span class="HPoint" style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("外墙") %></span>--%><span style="color: Red;">外墙</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="frmApplianceID" runat="server" Width="93px" CssClass="frmApplianceID">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="21" class="style16">
                                                    <%--<span class="HPoint" style="color: Red;"><%=HouseMIS.EntityUtils.I_IntegralItem.FindByRemarkTemplate("更正房源带看人") %></span>--%><span style="color: Red;">带看人</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="frmPayServantID" runat="server" Width="93px" CssClass="frmPayServantID">
                                                    </asp:DropDownList>
                                                </td>

                                                <td><a class="delete button" id="btnDel" runat="server" href="House/HouseForm.aspx" target="ajaxTodo" title="确认全部删除吗？" style="display: none"><span>删除全部照片</span></a>
                                                </td>

                                                <td>
                                                    <button id="btnEmpty_Sale" class="btnEmpty_Sale" runat="server" type="button" style="display: none">清空房源核验</button></td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Panel ID="h_gj" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="h_dk" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="h_dk_jf" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="h_cdjl" runat="server" Visible="false">
                    </asp:Panel>
                    <asp:Panel ID="h_zp" runat="server" Visible="false">
                    </asp:Panel>
                    <div style="height: 560px;">
                        <iframe src="/Institution/InsModel.aspx?pClsId=<%=OAID %>" style="width: 100%; height: 100%; overflow: auto"></iframe>
                    </div>
                    <div style="height: 560px;">
                    </div>
                    <div style="height: 560px;">
                    </div>
                    <div style="height: 560px;">
                    </div>
                    <div style="height: 560px;">
                    </div>
                </div>
                <div class="tabsFooter">
                    <div class="tabsFooterContent">
                    </div>
                </div>
            </div>
    </div>
    <div class="formBar" id="formBar_<%=Entity.HouseID %>">
        <div style="float: left; height=30">
            <div style="float: inherit;">
                首录人:<asp:Label runat="server" ID="frmOperName"></asp:Label>,所属分部：<%=BillCs %>
            </div>
            <div>
                登记日期:<asp:Label runat="server" ID="fexe_date"></asp:Label>
            </div>
        </div>
        <ul>
            <%--
            <li style="position: relative;"><a class="button" href="House/Templete/Webshariframe.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=Entity.HouseID %>"
                  rel='shareTemplete' target="navTab" external="true"
                title="房源分享" onclick="MinDalog()"><span>房源分享</span></a>
            </li>--%>

            <li id="likp" runat="server" visible="false">
                <a class="button" href="House/HouseLYList.aspx?navTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=Entity.HouseID %>" rel="HouseLYList"
                    width="550" height="550" target="dialog" title="申请开盘录音">
                    <span>申请开盘</span></a> </li>
            <li style="position: relative" id="ligj" runat="server" visible="false">
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button id="button_follow" runat="server" type="button">
                            写跟进</button>
                    </div>
                </div>
                <div id="fbwz<%=Entity.HouseID %>1" class="fbwz2" style="display: none;">
                    <ul>
                        <%//if (Entity.StateID == 2 && HouseMIS.EntityUtils.Employee.Current.ComID != 2) { %>
                        <%--<a href='House/FollowUpHouse.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=Entity.HouseID %>'
                            target='dialog' width='420' height='216' fresh='true' maxable='false' title="增加房源信息跟进"
                            rel="zj_follwUpEx">增加房源信息跟进</a>--%>
                        <%//}else if (Entity.StateID == 2){%>
                        <%--<a href='House/FollowUpHouse.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=Entity.HouseID %>'
                            target='dialog' width='420' height='266' fresh='true' maxable='false' title="增加房源信息跟进"
                            rel="zj_follwUpEx">增加房源信息跟进</a>--%>
                        <%//} %>
                        <li>
                            <a href='House/FollowUpEditor.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=Entity.HouseID %>'
                                target='dialog' width='380' height='410' fresh='true' maxable='false' title="增加跟进"
                                rel="zj_follwUp">增加跟进</a>
                            <a runat="server" id="PriceFollowUp" href='House/FollowUpPriceEditor.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=<%=Entity.HouseID %>'
                                target='dialog' width='600' height='560' fresh='true' maxable='false' title="增加压价跟进"
                                rel="zj_PricefollwUp">增加压价跟进</a></li>
                    </ul>
                </div>
            </li>
            <li id="lijf" runat="server" visible="false">
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="button" onclick="ShowJiFenHouse()" class="close">
                            积分记录</button>
                    </div>
                </div>
            </li>
            <%=btnSave.ToString() %>
            <li>
                <div class="button">
                    <div class="buttonContent">
                        <button type="button" class="close">
                            取消</button>
                    </div>
                </div>
            </li>
        </ul>
        <div style="display: none;">
            租金<asp:TextBox runat="server" CssClass="frmRent_Price" Width="90px" ID="frmRent_Price" Text="0"></asp:TextBox>元/月
            <asp:DropDownList ID="frmaType" CssClass="frmaType" runat="server" Width="118px">
                <asp:ListItem Value="0">出售</asp:ListItem>
            </asp:DropDownList>
        </div>
        </form>
    </div>

    <script type="text/javascript">
        $(function () {
            ChangeHouseModeLoad(<%=Entity.HouseDicType %>,<%=Entity.HouseID %>,'<%=Entity.Bid%>');
            //房源描述判断是否已经上传照片
            $("#div_<%=Entity.HouseID %> .frmLinkTel1:eq(0)").blur(function () {
                //房源ID
                var HouseID =<%=Entity.HouseID %>;
                //数据库房源描述
                var LinkTel1_Data = $.trim(<%=Entity.LinkTel1 %>);
                //填写的房源描述
                var LinkTel1 = $.trim($("#div_" + HouseID + " .frmLinkTel1:eq(0)").val());
                if (LinkTel1.length > 0 && LinkTel1 != "（1）该房屋在小区的位置（比如：沿河、沿马路、小区中心位置等）（2）所有房间的朝向（3）客厅属于明厅还是暗厅（4）装修的年数（5）房屋的明显优缺点（如全部是品牌家电，房屋有漏水、暗厕等。）（6）有阁楼、院子等须注明。（7）方便看房时间（8）房屋状态（空关、出租、自住等）") {
                    var str = HouseMIS.Web.House.HouseForm.IsUploadPic(HouseID).value;
                    if (str.length > 0) {
                        if (LinkTel1_Data.length == 0) {
                            $("#div_" + HouseID + " .frmLinkTel1:eq(0)").val("（1）该房屋在小区的位置（比如：沿河、沿马路、小区中心位置等）（2）所有房间的朝向（3）客厅属于明厅还是暗厅（4）装修的年数（5）房屋的明显优缺点（如全部是品牌家电，房屋有漏水、暗厕等。）（6）有阁楼、院子等须注明。（7）方便看房时间（8）房屋状态（空关、出租、自住等）");
                        }

                        alertMsg.error(str);
                        $("#AddBtnHouseFrom").show();
                    }
                }
            });

            $("#div_<%=Entity.HouseID %> .frmbuild_id:eq(0),#div_<%=Entity.HouseID %> .frmbuild_room:eq(0),#div_<%=Entity.HouseID %> .frmHouseDicName:eq(0)").blur(function () {
                //房源ID
                var HouseID =<%=Entity.HouseID %>;

                //楼栋号
                var build_id = $.trim($("#div_" + HouseID + " .frmbuild_id:eq(0)").val());
                //室号
                var build_room = $.trim($("#div_" + HouseID + " .frmbuild_room:eq(0)").val());
                //小区名称
                var HouseDicName = $.trim($("#div_" + HouseID + " .frmHouseDicName:eq(0)").val());
                //出售房源类别
                var aType = 0;

                var Repeat = HouseMIS.Web.House.HouseForm.GetRepeat().value;

                if (Repeat.indexOf("form_bedroom") > 0) {
                    Repeat = Repeat.replace(/@form_bedroom/, $("#div_" + HouseID + " .frmform_bedroom:eq(0)").val()).replace(/@form_foreroom/, $("#div_" + HouseID + " .frmform_foreroom:eq(0)").val()).replace(/@form_toilet/, $("#div_" + HouseID + " .frmform_toilet:eq(0)").val()).replace(/@form_terrace/, $("#div_" + HouseID + " .frmform_terrace:eq(0)").val());
                }
                if (Repeat.indexOf("build_floor") > 0) {
                    Repeat = Repeat.replace(/@build_floor/, $("#div_" + HouseID + " .frmbuild_floor:eq(0)").val()).replace(/@build_levels/, $("#div_" + HouseID + " .frmbuild_levels:eq(0)").val());
                }
                if (Repeat.indexOf("YearID") > 0) {
                    Repeat = Repeat.replace(/@YearID/, $("#div_" + HouseID + " .frmYearID:eq(0)").val());
                }
                if (Repeat.indexOf("FacetoID") > 0) {
                    Repeat = Repeat.replace(/@FacetoID/, $("#div_" + HouseID + " .frmFacetoID:eq(0)").val());
                }
                if (Repeat.indexOf("EntrustTypeID") > 0) {
                    Repeat = Repeat.replace(/@EntrustTypeID/, $("#div_" + HouseID + " .frmEntrustTypeID:eq(0)").val());
                }
                if (Repeat.indexOf("FitmentID") > 0) {
                    Repeat = Repeat.replace(/@FitmentID/, $("#div_" + HouseID + " .frmFitmentID:eq(0)").val());
                }
                if (Repeat.indexOf("SanjakID") > 0) {
                    Repeat = Repeat.replace(/@SanjakID/, $("#div_" + HouseID + " .frmSanjakID:eq(0)").val());
                }
                if (Repeat.indexOf("UseID") > 0) {
                    Repeat = Repeat.replace(/@UseID/, $("#div_" + HouseID + " .frmUseID:eq(0)").val());
                }
                if (Repeat.indexOf("TypeCode") > 0) {
                    Repeat = Repeat.replace(/@TypeCode/, $("#div_" + HouseID + " .frmTypeCode:eq(0)").val());
                }
                if (Repeat.indexOf("VisitID") > 0) {
                    Repeat = Repeat.replace(/@VisitID/, $("#div_" + HouseID + " .frmVisitID:eq(0)").val());
                }

                if (build_id.length > 0 &&
                    build_room.length > 0 &&
                    HouseDicName.length > 0) {
                    $.ajax({
                        url: 'House/HouseRepeatExt.ashx',
                        data: { HTML: Repeat, HouseDicName: HouseDicName, build_id: build_id, build_room: build_room, HouseID: HouseID, aType: aType },
                        //data: { HTML: "", HouseDicName: HouseDicName, build_id: build_id, build_room: build_room, HouseID: HouseID,aType:aType },
                        success: function (result) {
                            if (result == "1") {
                                alertMsg.error("不能输入重复的房源信息！");
                                $("#AddBtnHouseFrom").show();
                            }
                        }
                    });
                }
            });
        })
    </script>
</body>
