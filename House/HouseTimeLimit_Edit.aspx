<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseTimeLimit_Edit.aspx.cs" Inherits="HouseMIS.Web.House.HouseTimeLimit_Edit" %>

<style type="text/css">
    .style1 {
        height: 26px;
        line-height: 26px;
        width: 216px;
    }

    .style2 {
        height: 40px;
        line-height: 40px;
        width: 216px;
    }
</style>

<script type="text/javascript">
    function dialogAjaxDone(json) {
        if (json.notRemindSzTime != null && json.notRemindSzId != null) {
            notRemindSzTime[notRemindSzTime.length] = json.notRemindSzTime;
            notRemindSzId[notRemindSzId.length] = json.notRemindSzId;
        }
        if (json.statusCode == '200') {
            alertMsg.correct(json.message);
        }
        else {
            alertMsg.error(json.message);
        }
        $.pdialog.closeCurrent();
    }
    function RemarksInfoChange(value) {
        if (value != "") {
            if ($("#Remarks").val().length > 0)
                $("#Remarks").val($("#Remarks").val() + "," + value);
            else
                $("#Remarks").val(value);
        }
    }

    var aa = "", bb = "";
    function vv() {
        aa += document.getElementById("frmshi_id").value + ",";
        bb += document.getElementById("fHouseList").value + ",";
        document.getElementById("frmshi_id").value = aa.substr(0, aa.length - 1);
        document.getElementById("fHouseList").value = bb.substr(0, bb.length - 1);
        document.getElementById("frmHouseID").value = aa.substr(0, bb.length - 1);
    }

    function CustomerBringUpPicComps(nam) {
        $("#CustomerBringPics").html("<img src='" + GetAllUrl("customer", nam) + "?sj=" + Math.random() + "' width='136' height='170' />");
        $("#frmImgPic").val(GetUrl("customer", nam));
    }
    </script>

<body>
    <div class="pageContent">
        <form id="form1" method="post" runat="server" action="House/HouseTimeLimit_Edit.aspx"
            class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone)">
            <input type="hidden" runat="server" name="NavTabId" id="NavTabId" />
            <input type="hidden" runat="server" name="Hid" id="Hid" />
            <div class="pageFormContent" layouth="56">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 550px;">
                    <tr >
                        <td style="width: 200px;">定金数额
                        </td>
                        <td style="width: 150px;">
                            <asp:TextBox ID="frmMoney" name="frmMoney" runat="server" Width="310px"></asp:TextBox>
                        </td>

                        <td rowspan="5">定金协议
                            <div style="width: 136px; height: 170px; border: 1px #89a8c5 solid; float: right;"
                                id="CustomerBringPics">
                            </div>
                            <div id="CustomerBringPicFlas">
                                <script type="text/javascript" src="/js/flaUpfile.js"></script>
                                <script type="text/javascript">
                                    writeFla("CustomerBringPicFlas", 1, "CustomerBringUpPicComps", "", "customer", "5000", "", "0", "100", 1, "500", "700");
                                </script>
                            </div>
                            <asp:TextBox ID="frmImgPic" runat="server" Style="display: none"></asp:TextBox>
                       </td>
                    </tr>
                    <tr>
                        <td style="width: 60px;">状态
                         </td>
                        <td style="width: 150px;">
                            <asp:DropDownList ID="ffrmHouseState" runat="server" Height="20px" Width="125px">
                                <asp:ListItem Value="7">十万火急</asp:ListItem>
                                <asp:ListItem Value="4">速销待卖</asp:ListItem>
                                <asp:ListItem Value="6">焕新</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 60px;">责任人</td>
                        <td style="width: 150px;">
                            <asp:TextBox ID="frmEscortEmployeeID1" Style="display: none" name="f.frmEscortEmployeeID1" runat="server"
                                Width="114px"></asp:TextBox>
                            <asp:TextBox ID="frmem_name1" name="f.frmem_name1" runat="server" ReadOnly="true" Width="100px"></asp:TextBox>
                            <a class="btnLook" id="A1" href="Employee/emlookup.aspx?sType=db" lookupgroup="f" style="float: left"></a>
                        </td>
                        <td style="width: 60px;"></td>
                        <td style="width: 150px;"></td>
                    </tr>
                    <tr>
                        <td>备注
                    </td>
                        <td colspan="3">
                            <asp:TextBox ID="frmRemark" runat="server" TextMode="MultiLine" Height="54px" Width="310px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 45px;"></td>
                    </tr>
                </table>
            </div>
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
    </div>
</body>

