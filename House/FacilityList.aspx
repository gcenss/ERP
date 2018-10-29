<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FacilityList.aspx.cs" Inherits="HouseMIS.Web.House.FacilityList" %>

<div class="pageContent">
    <table border="0" id="frmFacilitys">
        <tbody>
            <tr>
                <td>
                    <input type="checkbox" name="frmFacilitys$0" id="frmFacilitys_0" value="无"><label for="frmFacilitys_0">无</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$1" id="frmFacilitys_1" value="煤气"><label for="frmFacilitys_1">煤气</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$2" id="frmFacilitys_2" value="灶具"><label for="frmFacilitys_2">灶具</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$3" id="frmFacilitys_3" value="电话"><label for="frmFacilitys_3">电话</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$4" id="frmFacilitys_4" value="有线"><label for="frmFacilitys_4">有线</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$6" id="frmFacilitys_6" value="1个挂空"><label for="frmFacilitys_6">1个挂空</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$7" id="frmFacilitys_7" value="2个挂空"><label for="frmFacilitys_7">2个挂空</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$8" id="frmFacilitys_8" value="3个挂空"><label for="frmFacilitys_8">3个挂空</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$9" id="frmFacilitys_9" value="4个挂空"><label for="frmFacilitys_9">4个挂空</label></td>
            </tr>
            <tr>
                <td>
                    <input type="checkbox" name="frmFacilitys$5" id="frmFacilitys_5" value="淋浴"><label for="frmFacilitys_5">淋浴</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$10" id="frmFacilitys_10" value="窗空"><label for="frmFacilitys_10">窗空</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$11" id="frmFacilitys_11" value="1个柜空"><label for="frmFacilitys_11">1个柜空</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$12" id="frmFacilitys_12" value="2个柜空"><label for="frmFacilitys_12">2个柜空</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$13" id="frmFacilitys_13" value="3个柜空"><label for="frmFacilitys_13">3个柜空</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$14" id="frmFacilitys_14" value="床"><label for="frmFacilitys_14">床</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$15" id="frmFacilitys_15" value="桌"><label for="frmFacilitys_15">桌</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$16" id="frmFacilitys_16" value="橱"><label for="frmFacilitys_16">橱</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$17" id="frmFacilitys_17" value="1台电视"><label for="frmFacilitys_17">1台电视</label>
                </td>
            </tr>
            <tr>
                <td>
                    <input type="checkbox" name="frmFacilitys$18" id="frmFacilitys_18" value="2台电视"><label for="frmFacilitys_18">2台电视</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$19" id="frmFacilitys_19" value="3台电视"><label for="frmFacilitys_19">3台电视</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$20" id="frmFacilitys_20" value="冰箱"><label for="frmFacilitys_20">冰箱</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$21" id="frmFacilitys_21" value="洗衣机"><label for="frmFacilitys_21">洗衣机</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$22" id="frmFacilitys_22" value="电梯"><label for="frmFacilitys_22">电梯</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$23" id="frmFacilitys_23" value="宽带"><label for="frmFacilitys_23">宽带</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$24" id="frmFacilitys_24" value="封阳"><label for="frmFacilitys_24">封阳</label>
                </td>
                <td>
                    <input type="checkbox" name="frmFacilitys$25" id="frmFacilitys_25" value="微波炉"><label for="frmFacilitys_25">微波炉</label>
                </td>
            </tr>
        </tbody>
    </table>
    <div class="formBar">
        <ul>
            <li>
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="button" onclick="GetFacility();">
                            提交</button>
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
</div>
<script>
    function GetFacility() {
        var checkList = $("#frmFacilitys :checkbox");
        var str = "";
        checkList.each(function () {
            var $this = $(this);

            if ($this.attr("checked").toString() == "true") {
                str = str + "," + $this.val();
            }
        });
        $.bringBack({ frmFacility: str.substring(1) })
    }
</script>
