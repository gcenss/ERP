<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddHouseKey.aspx.cs" Inherits="HouseMIS.Web.House.AddHouseKey" %>

<style type="text/css">
    .test_Tab {
        height: 152px;
    }

    .style1 {
        height: 36px;
    }

    .style2 {
        height: 11px;
    }
</style>

<body>
    <form id="form1" runat="server" action="House/AddHouseKey.aspx" class="pageForm required-validate"
        onsubmit="return validateCallback(this, dialogAjaxDone)">
        <fieldset>
            <legend>跟进信息</legend>
            <input type="hidden" id="id" name="id" runat="server" />
            <table width="560" border="0" cellpadding="0" cellspacing="0" class="test_Tab">
                <tr class="style1">
                    <td width="61">房源编号
                </td>
                    <td width="121">
                        <asp:TextBox runat="server" ID="housekeyid1" Style="width: 100px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td width="61">
                        <span id="tdorg" style="display: none">分 部</span>
                    </td>
                    <td width="130" id="PAddHouseKey">
                        <span id="tddord" style="display: none;">
                            <asp:DropDownList ID="frmOrgID" CssClass="frmOrgID" runat="server" AppendDataBoundItems="true"
                                Style="width: 105px;">
                                <asp:ListItem Value="0" Text=""></asp:ListItem>
                            </asp:DropDownList>
                            <script type="text/javascript">$("#PAddHouseKey .frmOrgID").combobox({ size: 98 });  </script>
                        </span>
                    </td>
                    <td rowspan="5">钥匙证明
                        <div style="width: 200px; height: 200px; border: 1px #89a8c5 solid; float: right;"
                            id="HouseKeyImg">
                        </div>
                        <div id="HouseKeyImgFlas">
                            <script type="text/javascript" src="/js/flaUpfile.js"></script>
                            <script type="text/javascript">
                                writeFla("HouseKeyImg", 1, "HouseKeyImgComps", "", "housekey", "1000", "", "0", "100", 0, "500", "500");
                            </script>
                        </div>
                        <asp:TextBox ID="frmimgUrl" runat="server" Style="display: none"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" class="style1">
                        <table>
                            <tr>
                                <td style="width: 80px;">
                                    <span id="qita">
                                        <asp:CheckBox runat="server" ID="frmInOhterCompany" Text="其他中介" onclick="showOrg()"></asp:CheckBox></span>
                                </td>
                                <td style="width: 70px">
                                    <input type="radio" runat="server" id="rdout" name="take" value="拿走" onclick="changeOut()" />拿走
                            </td>
                                <td style="width: 70px">
                                    <input type="radio" runat="server" id="rdin" name="take" value="拿入" onclick="changeOut()" />拿入
                            </td>
                                <td style="width: 70px;">
                                    <span id="landrand">
                                        <asp:CheckBox runat="server" ID="isLandrand" Text="是否房东" onclick="showlandRand()"></asp:CheckBox></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr class="style1">
                    <td>
                        <span id="Span1" style="display: none;">中 介</span>
                    </td>
                    <td><span id="Span2" style="display: none;">
                        <asp:DropDownList ID="frmOhterCompanyNameID" CssClass="frmOrgID" runat="server" Style="width: 105px;">
                        </asp:DropDownList></span>
                    </td>
                    <td><span id="Span3" style="display: none;">电 话
                    </span>
                    </td>
                    <td><span id="Span4" style="display: none;">
                        <asp:TextBox runat="server" ID="frmTel" Style="width: 100px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td class="style2" valign="top">备 注
                </td>
                    <td colspan="3" rowspan="2" valign="top" class="style2">
                        <asp:TextBox runat="server" Width="96%" ID="frmRemarks" TextMode="MultiLine" Height="98px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        <div class="formBar">
            <ul>
                <li>
                    <div class="buttonActive">
                        <div class="buttonContent">
                            <button type="submit">
                                保存</button>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="button">
                        <div class="buttonContent">
                            <button type="button" class="close">
                                取消</button>
                        </div>
                    </div>
                </li>
            </ul>
        </div>
    </form>

    <script type="text/javascript">
        function changeOut() {
            // 点拿入
            if ($("#rdin").attr("checked")) {
                $("#landrand").css("display", "none");
                $("#tdorg").css("display", "block");
                $("#tddord").css("display", "block");
                $("#qita").css("display", "block");

            }
            // 点拿走
            else {
                $("#landrand").css("display", "block");
                $("#tdorg").css("display", "none");
                $("#tddord").css("display", "none");
                $("#qita").css("display", "block");
            }
        }
        function showOrg() {
            if ($("#frmInOhterCompany").attr("checked")) {
                // 拿走
                if ($("#rdout").attr("checked")) {
                    $("#Span1").css("display", "block");
                    $("#Span2").css("display", "block");
                    $("#Span3").css("display", "block");
                    $("#Span4").css("display", "block");

                    $("#landrand").css("display", "none");
                    $("#tdorg").css("display", "none");
                    $("#tddord").css("display", "none");
                }
                else {
                    $("#Span1").css("display", "block");
                    $("#Span2").css("display", "block");
                    $("#Span3").css("display", "block");
                    $("#Span4").css("display", "block");

                    $("#landrand").css("display", "none");
                    $("#tdorg").css("display", "none");
                    $("#tddord").css("display", "none");
                }
            }
            else {
                if ($("#rdout").attr("checked")) {
                    $("#Span1").css("display", "none");
                    $("#Span2").css("display", "none");
                    $("#Span3").css("display", "none");
                    $("#Span4").css("display", "none");

                    $("#tdorg").css("display", "none");
                    $("#tddord").css("display", "none");

                    $("#landrand").css("display", "block");
                }
                else {
                    $("#Span1").css("display", "none");
                    $("#Span2").css("display", "none");
                    $("#Span3").css("display", "none");
                    $("#Span4").css("display", "none");

                    $("#tdorg").css("display", "block");
                    $("#tddord").css("display", "block");

                    $("#landrand").css("display", "none");
                }
            }
        }
        function showlandRand() {
            if ($("#isLandrand").attr("checked")) {
                $("#qita").css("display", "none");
            }
            else {
                $("#qita").css("display", "block");
            }
        }

        function HouseKeyImgComps(nam) {
            $("#HouseKeyImg").html("<img src='" + GetAllUrl("housekey", nam) + "?sj=" + Math.random() + "' width='200' height='200' />");
            $("#frmimgUrl").val(GetUrl("housekey", nam));
        }
    </script>
</body>
