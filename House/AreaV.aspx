<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaV.aspx.cs" Inherits="HouseMIS.Web.House.AreaV" %>

<script type="text/javascript">
    var txt1 = $("#txtName1").val(),
        txt2 = $("#txtName2").val(),
        txt3 = $("#txtName3").val();

    $(function () {
        var _xx = $("#" + txt2).val();
        var _cc = "", _vv = "", _bb = "", _nn = "";
        if (_xx != "" || _xx != null) {
            for (var i = 0; i < _xx.split(",").length; i++) {
                _cc = _xx.split(",")[i].split("_")[0];
                _vv = _xx.split(",")[i].split("_")[2];
                if (_cc == "q") {
                    _bb += "<span><input type=\"checkbox\" name=\"var_txt\" value=\"" + _xx.split(",")[i] + "\" checked=\"checked\" />" + _vv + "</span>";
                }
                else if (_cc == "s") {
                    _bb += "<span><input type=\"checkbox\" name=\"var_txt\" value=\"" + _xx.split(",")[i] + "\" checked=\"checked\" />" + _vv + "</span>";
                }
                else if (_cc == "x") {
                    _bb += "<span><input type=\"checkbox\" name=\"var_txt\" value=\"" + _xx.split(",")[i] + "\" checked=\"checked\" />" + _vv + "</span>";
                }
                _nn = _vv + "," + _nn;
            }
            $("#tjContent").html(_bb);
        }
    });
    function hqValue(obj) {
        var nr = $(obj).val(), nr_sc;
        var nr_fz;

        if (nr != "" && nr != null) nr_fz = nr.split("_");
        if (nr_fz[0] == "q") {
            nr_sc = "<span><input name=\"var_txt\" type=\"checkbox\" checked=\"checked\" value=\"" + nr + "\">" + nr_fz[2] + "</span>";
        }
        else if (nr_fz[0] == "s") {
            nr_sc = "<span><input name=\"var_txt\" type=\"checkbox\" checked=\"checked\" value=\"" + nr + "\">" + nr_fz[2] + "</span>";
        }
        else if (nr_fz[0] == "x") {
            nr_sc = "<span><input name=\"var_txt\" type=\"checkbox\" checked=\"checked\" value=\"" + nr + "\">" + nr_fz[2] + "</span>";
        }

        if (!$(obj).attr('checked')) {
            $("#tjContent").html($("#tjContent").html().replace(nr_sc, ""));
        }
        else {
            if ($("#tjContent").html().indexOf(nr_sc) < 0)
                $("#tjContent").html($("#tjContent").html() + nr_sc);
        }
    }
    function openSQ(areaid) {
        //$("#son_" + areaid).attr("className", "iib");
        $("#sqContent").html($("#son_" + areaid).html());
    }
    function openXQ(sqid) {
        var items = "";
        $.ajax({
            url: '/Ajax/getVillage.aspx',
            data: { shangquanid: sqid },
            success: function (result) {
                $.each(eval(result), function (i, item) {
                    if (item.name != "")
                        items += "<span class=\"xqxx\"><input type=\"checkbox\" id=\"cblXQ" + item.id + "\" name=\"cblXQ" + item.id + "\" value=\"x_" + item.id + "_" + item.name + "\" onclick=\"hqValue(this)\" />" + item.name + "</span>";
                });
                $("#xqContent").html(items);
            }
        });
    }
    function closeVal() {
        var _vv = "", _vv2 = "";
        $("#tjName").html("");
        $('input[name="var_txt"]:checked').each(function () {
            _vv = $(this).val().split("_");
            $("#tjName").html(_vv[2] + "," + $("#tjName").html());
            _vv2 = $(this).val() + "," + _vv2;
        });
        $("#" + txt1).val($("#tjName").html());
        $("#" + txt2).val(_vv2);
        $.pdialog.close(txt3);
    }
    function seachXQ() {
        var items = "";
        $.ajax({
            url: '/Ajax/getVillage.aspx',
            data: { name: $("#frmEstateOrAddress").val() },
            success: function (result) {
                $.each(eval(result), function (i, item) {
                    if (item.name != "")
                        items += "<span class=\"xqxx\"><input type=\"checkbox\" id=\"cblXQ" + item.id + "\" name=\"cblXQ" + item.id + "\" value=\"x_" + item.id + "_" + item.name + "\" onclick=\"hqValue(this)\" />" + item.name + "</span>";
                });
                $("#xqContent").html(items);
            }
        });
    }
</script>
<style>
    .iio {
        display: none;
    }

    .xqxx {
        display: block;
        float: left;
        width: 50%;
    }

    #xqContent {
        OVERFLOW-Y: auto;
        OVERFLOW-X: hidden;
        width: 490px;
        height: 145px;
    }

    #qyContent span {
        width: 100px;
        display: block;
        float: left;
    }

    #sqContent span {
        width: 125px;
        display: block;
        float: left;
    }

    .tabX th {
        text-align: right;
    }

    .tabX td, .tabX th {
        padding: 5px 0;
        border-bottom: 1px solid #e1e1e1;
    }
</style>
<script language="javascript">
    //绑定Suggest事件
    var aala = IntSuggest("frmEstateOrAddress", "HouseDicID", "/Ajax/SearchHouseDic.ashx?kw=");
</script>
<body>
    <form id="form1" runat="server">
        <table class="tabX">
            <tr>
                <th width="80"><span style="color: red">已选项：</span></th>
                <td>
                    <div id="tjContent"></div>
                    <div id="tjName"></div>
                </td>
                <td width="60">
                    <button type="button" onclick="closeVal()">确定</button></td>
            </tr>
            <tr style="background: #4274dc">
                <th>小区搜索：</th>
                <td colspan="2">
                    <asp:TextBox ID="frmEstateOrAddress" like="true" runat="server" title="楼盘/地址" Width="154px"></asp:TextBox>
                    <button type="button" onclick="seachXQ()">查小区</button>（选择小区后按回车键，点击查小区按钮）
                </td>
            </tr>
            <tr>
                <th>区域：</th>
                <td colspan="2">
                    <div id="qyContent"><%=pInfo %></div>
                </td>
            </tr>
            <tr>
                <th>商圈：</th>
                <td colspan="2">
                    <div id="sqContent"></div>
                </td>
            </tr>
            <tr>
                <th>小区：</th>
                <td colspan="2">
                    <div id="xqContent"></div>
                </td>
            </tr>
        </table>
        <%=pSon %>
        <asp:TextBox runat="server" ID="txtName1" Style="display: none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtName2" Style="display: none;"></asp:TextBox>
        <asp:TextBox runat="server" ID="txtName3" Style="display: none;"></asp:TextBox>
    </form>
</body>
