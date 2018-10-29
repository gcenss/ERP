<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseListFrom.aspx.cs" Inherits="HouseMIS.Web.House.HouseListFrom" %>

<style>
    .sTab, .sTab2 {
        width: 600px;
        padding-bottom: 10px;
    }

        .sTab input, .pageFormContent .textInput {
            display: inline;
            float: none;
        }

        .sTab2 input {
            display: inline;
        }

        .sTab select {
            padding-right: 3px;
        }

        .sTab th, .sTab2 th {
            text-align: right;
            padding-right: 3px;
        }

        .sTab td, .sTab2 td {
            text-align: left;
            padding-left: 3px;
        }

        .sTab tr, .sTab2 tr {
            height: 30px;
        }

    .datewidth {
        width: 65px;
    }

    .sTab3 input, .sTab3 label {
        width: auto;
        padding: 0;
    }

    .sTab3 td {
        width: 100px;
    }

    .pageFormContent select {
        float: none;
        width: 120px;
    }

    .dateRange label {
        width: auto;
    }

    .dateRange input {
        width: 40px;
    }

    .dateRange {
        padding-right: 5px !important;
    }

    .htab td {
        text-align: left;
        font-size: 12px;
        padding-bottom: 5px;
        padding-left: 4px;
        height: auto;
    }

    .htab select {
        width: 69px;
        float: left;
    }

    .input_h {
        width: 100px;
    }

    .ch_tabls input {
        float: left;
    }

    .ch_tabls label {
        float: left;
        width: auto;
        padding: 0px;
        margin: 0px;
    }
</style>

<body>
    <div class="pageContent">
        <form runat="server" id="pagerForm" method="post" action="House/HouseList.aspx" class="pageForm" onsubmit="return GetVl(this);">
            <div class="pageFormContent" layouth="58">
                <table class="sTab">
                    <tr>
                        <th>状态:</th>
                        <td>
                            <asp:DropDownList ID="sfrmStateID" runat="server"></asp:DropDownList>
                        </td>
                        <th>产权:</th>
                        <td>
                            <asp:DropDownList ID="sfrmPropertyID" runat="server"></asp:DropDownList>
                        </td>
                        <th>朝向:</th>
                        <td>
                            <asp:DropDownList ID="sfrmFacetoID" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>区域:</th>
                        <td>
                            <a href='House/AreaC.aspx?NavTabId=<%=NavTabId %>&doAjax=true&tn1=mysfrmtxtArea&tn2=mysfrmtxtArea2&tn3=AreaVillage' target='dialog' rel='AreaVillage' width='580' height='380' fresh='true' mask="true" maxable='false' title="区域商圈小区选择">
                                <asp:TextBox ID="mysfrmtxtArea" runat="server" Width="115px" ReadOnly="true"></asp:TextBox>
                            </a>
                            <asp:TextBox ID="mysfrmtxtArea2" runat="server" Style="display: none;"></asp:TextBox>
                            <asp:DropDownList ID="mysfrmAreaID" runat="server" Style="display: none;" onchange="ReLoadSanJak(this)"></asp:DropDownList>
                        </td>
                        <th>栋号:</th>
                        <td>
                            <asp:TextBox ID="mysfrmbuild_id" runat="server" Width="40px" onkeyup="value=value.replace(/[^\a-\z\A-\Z0-9\-]/g,'')" onafterpaste="value=value.replace(/[^\a-\z\A-\Z0-9\-]/g,'')"></asp:TextBox>
                            -
                        <asp:TextBox ID="mysfrmbuild_id2" runat="server" Width="40px" onkeyup="value=value.replace(/[^\a-\z\A-\Z0-9\-]/g,'')" onafterpaste="value=value.replace(/[^\a-\z\A-\Z0-9\-]/g,'')"></asp:TextBox>
                        </td>
                        <th>房号:</th>
                        <td>
                            <asp:TextBox ID="mysfrmbuild_room" runat="server" Width="40px" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox>
                            -
                        <asp:TextBox ID="mysfrmbuild_room2" runat="server" Width="40px" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>楼层:</th>
                        <td>
                            <asp:TextBox ID="mysfrmbuild_floor1" runat="server" Style="width: 40px"></asp:TextBox>
                            &nbsp;&nbsp;-&nbsp;&nbsp;

                        <asp:TextBox ID="mysfrmbuild_floor2" runat="server" Style="width: 40px"></asp:TextBox>
                        </td>
                        <th>总价:</th>
                        <td>
                            <asp:TextBox ID="mysfrmprice1" runat="server" Style="width: 40px" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox>
                            -

                        <asp:TextBox ID="mysfrmprice2" runat="server" Style="width: 40px" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox>
                        </td>
                        <th>面积:</th>
                        <td>
                            <asp:TextBox ID="mysfrmarea1" runat="server" Style="width: 40px" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox>
                            -

                        <asp:TextBox ID="mysfrmarea2" runat="server" Style="width: 40px" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>装修:</th>
                        <td>
                            <asp:TextBox ID="mysfrmFName" runat="server" ReadOnly="true" Width="115px"></asp:TextBox>
                            <asp:TextBox ID="mysfrmFitmentID" runat="server" Style="display: none;"></asp:TextBox>
                            <div id="div_mysfrmFitmentID" style="padding-top: 8px; border: 1px solid #ccc; background: #e0dfdf; top: 120px; height: auto; position: absolute; z-index: 9999;">
                                <div style="cursor: pointer; float: right; margin: 10px;">
                                    <img onclick="CloseRoleCheck('div_mysfrmFitmentID')" src="/img/bar_close.gif">
                                </div>
                                <%=GetFitmentID() %>
                            </div>
                        </td>
                        <th>编号:</th>
                        <td>
                            <asp:TextBox ID="mysfrmShi_id" runat="server" Width="115px"></asp:TextBox>
                        </td>
                        <th>电话:</th>
                        <td>
                            <asp:TextBox ID="mysfrmlandlord_tel2" runat="server" CssClass="input_h" MaxLength="11"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>户型:</th>
                        <td>
                            <asp:DropDownList ID="mysfrmform_bedroom" runat="server" Width="70">
                                <asp:ListItem></asp:ListItem>
                                <asp:ListItem>1-2</asp:ListItem>
                                <asp:ListItem>2-3</asp:ListItem>
                                <asp:ListItem>3-4</asp:ListItem>
                                <asp:ListItem>4-5</asp:ListItem>
                                <asp:ListItem>5以上</asp:ListItem>
                                <asp:ListItem>自定义</asp:ListItem>
                            </asp:DropDownList>
                            <span id="mysfhx">
                                <asp:TextBox ID="mysfrmform_bedroom1" runat="server" Width="20px"></asp:TextBox>
                                -
                            <asp:TextBox ID="mysfrmform_bedroom2" runat="server" Width="20px"></asp:TextBox>
                            </span>
                        </td>

                        <th>来源:</th>
                        <td>
                            <asp:DropDownList ID="sfrmVisitID" runat="server"></asp:DropDownList>
                        </td>
                        <th>分部:</th>
                        <td id="PHouseListFrom">
                            <asp:DropDownList ID="mysfrmOrgID" class="mysfrmOrgID" runat="server" AppendDataBoundItems="true">
                                <asp:ListItem></asp:ListItem>
                            </asp:DropDownList>
                            <script type="text/javascript">$("#PHouseListFrom .mysfrmOrgID").combobox({ size: 100 });</script>
                        </td>
                    </tr>
                    <tr>
                        <th>单价:</th>
                        <td>
                            <asp:TextBox ID="mysfrmOhter2ID1" runat="server" Style="width: 40px" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox>
                            -
                        <asp:TextBox ID="mysfrmOhter2ID2" runat="server" Style="width: 40px" onkeyup="value=value.replace(/[^0-9]/g,'')" onafterpaste="value=value.replace(/[^0-9]/g,'')"></asp:TextBox>
                        </td>
                        <th>委托:</th>
                        <td>
                            <asp:DropDownList ID="sfrmEntrustTypeID" runat="server" CssClass="ffrmEntrustTypeID"></asp:DropDownList>
                        </td>
                        <th>首录人：</th>
                        <td>
                            <asp:TextBox ID="mysfrmOwnerEmployeeID" runat="server" CssClass="input_h"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>产权年限：</th>
                        <td>
                            <asp:DropDownList ID="sfrmpropertyYear" runat="server"></asp:DropDownList></td>
                        <th>&nbsp;</th>
                        <td>
                            &nbsp;</td>
                        <th>&nbsp;</th>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
                <table class="sTab2">
                    <tr>
                        <th>登记日期:</th>
                        <td>
                            <asp:TextBox ID="mysfrmexe_date1" runat="server" CssClass="datewidth date" Width="80px"></asp:TextBox>
                            -

                        <asp:TextBox ID="mysfrmexe_date2" runat="server" CssClass="datewidth date" Width="80px"></asp:TextBox>
                        </td>
                        <th>更新日期:</th>
                        <td>
                            <asp:TextBox ID="mysfrmupdate_date1" runat="server" CssClass="datewidth date" Width="80px"></asp:TextBox>
                            -

                        <asp:TextBox ID="mysfrmupdate_date2" runat="server" CssClass="datewidth date" Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>最后更进日期:</th>
                        <td>
                            <asp:TextBox ID="mysfrmFollowUp_Date1" runat="server" CssClass="datewidth date" Width="80px"></asp:TextBox>
                            -

                        <asp:TextBox ID="mysfrmFollowUp_Date2" runat="server" CssClass="datewidth date" Width="80px"></asp:TextBox>
                        </td>
                        <th>房屋用途:</th>
                        <td>
                            <asp:DropDownList ID="sfrmUseID" runat="server" Width="55px"></asp:DropDownList>
                            类型:<asp:DropDownList ID="sfrmTypeCode" runat="server" Width="100px"></asp:DropDownList>
                        </td>
                    </tr>
                </table>

                <table class="sTab3">
                    <tr>
                        <td>
                            <asp:CheckBox ID="mysfrmHasKey" runat="server" Text="钥匙" Width="65px"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="mysfrmHasImage" runat="server" Text="照片" Width="65px"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="mysfrmIsOnlyOne" runat="server" Text="家庭唯一" Width="70px"></asp:CheckBox>
                            <%--<asp:CheckBox ID="mysfrmpastdue" runat="server" Text="满2年" Width="65px"></asp:CheckBox>--%>
                        </td>
                        <td>
                            <asp:CheckBox ID="mysfrmIsBring" runat="server" Text="带看" Width="65px"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="mysfrmHasRecord" runat="server" Text="录音房源" Width="75px"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="mysfrmIsLock" runat="server" Text="锁盘" Width="65px"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="mysfrmShi_addr" runat="server" Text="评估价" Width="65px"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="mysfrmHXImg" runat="server" Text="户型图" Width="65px"></asp:CheckBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="formBar">
                <ul>
                    <li>
                        <div class="buttonActive">
                            <div class="buttonContent">
                                <button type="submit">
                                    开始检索</button>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="button">
                            <div class="buttonContent">
                                <button type="button" onclick="clearform()">
                                    清空重输</button>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>

            <%-- <div style="display: none;">
                <asp:DropDownList ID="sfrmaType" runat="server">
                    <asp:ListItem></asp:ListItem>
                    <asp:ListItem Value="0">出售</asp:ListItem>
                    <asp:ListItem Value="1">出租</asp:ListItem>
                </asp:DropDownList>
                片区:<asp:DropDownList ID="sfrmSanjakID" runat="server" Style="width: 105px;"></asp:DropDownList>
                <script language="javascript">
                    //绑定Suggest事件
                    var aa = IntSuggest("mysfrmHouseDicName", "HouseDicID", "/Ajax/SearchHouseDic.ashx?kw=");
            </script>
                楼盘:<asp:TextBox ID="mysfrmHouseDicName" runat="server" like="true" CssClass="input_h"></asp:TextBox>
                楼盘地址:<asp:TextBox ID="mysfrmHouseDicAddress" runat="server" CssClass="input_h" Style="width: 254px"></asp:TextBox>
                税费:<asp:DropDownList ID="sfrmTaxesID" runat="server" Style="width: 105px;"></asp:DropDownList>
                电话:<asp:TextBox ID="mysfrmLinkTel2" runat="server" Width="115px" MaxLength="9"></asp:TextBox>
                房东:<asp:TextBox ID="mysfrmlandlord_name" runat="server" CssClass="input_h"></asp:TextBox>
                房东类型:<asp:DropDownList ID="sfrmLandlordID" runat="server" Style="width: 105px;"></asp:DropDownList>
                专管员:<asp:TextBox ID="mysfrmOwenEmployeeID" runat="server" CssClass="input_h"></asp:TextBox>
                单价:<asp:textbox id="mysfrmOhter2ID1" runat="server" style="width: 40px"></asp:textbox>
            -<asp:textbox id="mysfrmOhter2ID2" runat="server" style="width: 40px"></asp:textbox>
            租金:<asp:TextBox ID="mysfrmRent_Price1" runat="server" Style="width: 40px"></asp:TextBox>
                -<asp:TextBox ID="mysfrmRent_Price2" runat="server" Style="width: 40px"></asp:TextBox>
                保密说明:<asp:TextBox ID="mysfrmRemarks" runat="server" CssClass="input_h" islike="true"></asp:TextBox>
                评价:<asp:DropDownList ID="sfrmAssessStateID" runat="server" Style="width: 105px;"></asp:DropDownList>
                备案电话:<asp:TextBox ID="mysfrmBackTel" runat="server" CssClass="input_h"></asp:TextBox>
                备注:<asp:TextBox ID="mysfrmnote" runat="server" Style="width: 254px;" islike="true"></asp:TextBox>
登记人:<asp:textbox id="mysfrmOperatorID" runat="server" cssclass="input_h"></asp:textbox>
            年代:<asp:DropDownList ID="sfrmYearID" runat="server" Style="width: 105px;"></asp:DropDownList>
                <asp:DropDownList ID="sfrmform_bedroom" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="sfrmform_foreroom" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="sfrmform_toilet" runat="server">
                    <asp:ListItem Value=""></asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                </asp:DropDownList>
                <asp:CheckBox ID="myffrmAllView" runat="server" Text="3D全景" Width="65px"></asp:CheckBox>
            </div>--%>
        </form>
    </div>

    <script type="text/javascript">
        $(function () {
            $("#mysfhx").hide();
            $("#div_mysfrmFitmentID").hide();
            $('#mysfrmtxtArea').val($('#myffrmtxtArea').val());
            $('#mysfrmtxtArea2').val($('#myffrmtxtArea2').val());

            $('#mysfrmprice1').val($('#myffrmPrice1').val());
            $('#mysfrmprice2').val($('#myffrmPrice2').val());

            $('#mysfrmarea1').val($('#myffrmarea1').val());
            $('#mysfrmarea2').val($('#myffrmarea2').val());

            $('#mysfrmbuild_floor1').val($('#myffrmbuild_floor1').val());
            $('#mysfrmbuild_floor2').val($('#myffrmbuild_floor2').val());
            $('#mysfrmbuild_id').val($('#myffrmbuild_id').val());

            $('#mysfrmFitmentID').val($('#myffrmFitmentID').val());
            $('#mysfrmFName').val($('#myffrmFName').val());

            $('#mysfrmbuild_room').val($('#myffrmbuild_room').val());
            $('#mysfrmlandlord_tel2').val($('#myffrmlandlord_tel2').val());

            $('#sfrmEntrustTypeID').val($('#ffrmEntrustTypeID').val());

            $("#sfrmStateID").val($('#ffrmStateID').val());
            $("#sfrmaType").val($('#ffrmaType').val());

            //首录人
            $("#mysfrmOwnerEmployeeID").val($('#myffrmOwnerEmployeeID').val());
            //房源编号
            $('#mysfrmShi_id').val($('#myffrmShi_id').val());

            if ($("#myffrmHasKey").val() == "on") {
                $("#mysfrmHasKey").attr("checked", true);
            }
            if ($("#myffrmHasImage").val() == "on") {
                $("#mysfrmHasImage").attr("checked", true);
            }

            if ($("#myffrmform_bedroom").val() != "") {
                $("#mysfrmform_bedroom").val($("#myffrmform_bedroom").val());
                $("#mysfrmform_bedroom1").val($("#myffrmform_bedroom1").val());
                $("#mysfrmform_bedroom2").val($("#myffrmform_bedroom2").val());
            }
            if ($("#myffrmform_bedroom").val() == "自定义") { $("#mysfhx").show(); }

            $('#mysfrmform_bedroom').change(function () {
                var p1 = $(this).children('option:selected').val();//这就是selected的值
                if (p1 == "自定义") {
                    $('#mysfrmform_bedroom1').val("");
                    $('#mysfrmform_bedroom2').val("");
                    $("#mysfhx").show();
                }
                else {
                    if (p1.split("-").length == 2 && p1 != "") {
                        $('#mysfrmform_bedroom1').val(p1.split("-")[0]);
                        $('#mysfrmform_bedroom2').val(p1.split("-")[1]);
                    }
                    else if (p1.split("-").length == 1 && p1 != "") {
                        $('#mysfrmform_bedroom1').val("999");
                        $('#mysfrmform_bedroom2').val("999");
                    }
                    else {
                        $('#mysfrmform_bedroom1').val("");
                        $('#mysfrmform_bedroom2').val("");
                    }
                    $("#mysfhx").hide();
                }
            })
        });

        $("#mysfrmFName").click(function () {
            var str1 = $("#mysfrmFName").val();
            var str2 = $("#mysfrmFitmentID").val(), str3;
            if (str1 != "" && str1 != null) {
                for (var i = 0; i < str1.split(',').length - 1; i++) {
                    str3 = str1.split(',')[i] + "_" + str2.split(',')[i + 1];
                    $(":checkbox[value='" + str3 + "']").attr("checked", true)
                }
            }
            if ($("#div_mysfrmFitmentID").is(":hidden")) {
                $("#div_mysfrmFitmentID").show();
            }
            else {
                $("#div_mysfrmFitmentID").hide();
            }
        });

        function zxValue(obj) {
            var nr = $(obj).val().split("_");
            if (!$(obj).attr('checked')) {
                $("#mysfrmFName").val($("#mysfrmFName").val().replace(nr[0] + ",", ""));
                $("#mysfrmFitmentID").val($("#mysfrmFitmentID").val().replace("," + nr[1], ""));
            }
            else {
                if ($("#mysfrmFName").val().indexOf(nr[0]) < 0) {
                    $("#mysfrmFName").val($("#mysfrmFName").val() + nr[0] + ",");
                    $("#mysfrmFitmentID").val("," + nr[1] + $("#mysfrmFitmentID").val());
                }
            }
        }

        function GetVl(obj) {
            try {
                $('#sfrmOperatorID').val($('#OperatorID').val());
                $('#myffrmtxtArea').val($('#mysfrmtxtArea').val());
                $('#myffrmtxtArea2').val($('#mysfrmtxtArea2').val());
                if ($('#mysfrmbuild_id').val() != "" && $('#mysfrmbuild_id2').val() != "") {
                    if (parseInt($('#mysfrmbuild_id').val()) > 0 && parseInt($('#mysfrmbuild_id2').val()) > 0 && parseInt($('#mysfrmbuild_id2').val()) > parseInt($('#mysfrmbuild_id').val())) { }
                    else {
                        alert("楼栋号填写错误");
                        return false;
                    }
                }

                if ($('#mysfrmbuild_room').val() != "" && $('#mysfrmbuild_room2').val() != "") {
                    if (parseInt($('#mysfrmbuild_room').val()) > 0 && parseInt($('#mysfrmbuild_room2').val()) > 0 && parseInt($('#mysfrmbuild_room2').val()) > parseInt($('#mysfrmbuild_room').val())) { }
                    else {
                        alert("门号填写错误");
                        return false;
                    }
                }
                navTabSearch(obj);
                $.pdialog.close('dlg_House2');
                return false;
            }
            catch (e) { }
        }

        function ReLoadSanJak(id) {
            var html = "";
            if ($(id).val()) {
                html = HouseMIS.Web.House.HouseListFrom.GetSanjak($(id).val()).value;
            } else {
                html = HouseMIS.Web.House.HouseListFrom.GetSanjak("0").value;
            }
            var ArryList = html.split(",");
            $("#sfrmSanjakID").html("");
            for (var i = 0; i < ArryList.length; i++) {
                var Arr = ArryList[i].split("|");
                $("#sfrmSanjakID").append("<option value='" + Arr[0] + "'>" + Arr[1] + "</option>")
            }
        }
    </script>
</body>
