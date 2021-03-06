﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseZPcheck.aspx.cs" Inherits="HouseMIS.Web.House.HouseZPcheck" %>

<script type="text/javascript">
    $(function () {
        $("#myffhx").hide();
        $("#div_fitmentid").hide();
        if ($('#myffrmform_bedroom').children('option:selected').val() == "自定义"){ $("#myffhx").show();}

        if(<%=pHasImage %>=="1"){
            $("#cHasImage").attr("checked", true);
            $("#myffrmHasImage").val("on");
        }
        if(<%=pHasKey %>=="1"){
            $("#cHasKey").attr("checked", true);
            $("#myffrmHasKey").val("on");
        }

        $('#myffrmform_bedroom').change(function () {
            var p1 = $(this).children('option:selected').val();//这就是selected的值
            if (p1 == "自定义") {
                $('#myffrmform_bedroom1').val("");
                $('#myffrmform_bedroom2').val("");
                $("#myffhx").show();
            }
            else {
                if(p1.split("-").length==2&&p1!=""){
                    $('#myffrmform_bedroom1').val(p1.split("-")[0]);
                    $('#myffrmform_bedroom2').val(p1.split("-")[1]);
                }
                else if(p1.split("-").length==1&&p1!=""){
                    $('#myffrmform_bedroom1').val("999");
                    $('#myffrmform_bedroom2').val("999");
                }
                else{
                    $('#myffrmform_bedroom1').val("");
                    $('#myffrmform_bedroom2').val("");
                }
                $("#myffhx").hide();
            }
        })

    });
    //打开房源编辑页面
    function OpenHouseEdit_SF(houseId, houseNo, atype){
        // 所有参数都是可选项。
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 700, mixable: false }
        $.pdialog.open(url, "housewiew_" + houseId, houseNo + "-修改出售房源", options);
    }
    function OpenArea(name1,name2) {
        var url = "House/AreaV.aspx?tn1="+name1+"&tn2="+name2+"&tn3=AreaVillage";
        var options = { width: 580, height: 380, mixable: false }
        $.pdialog.open(url, "AreaVillage", "区域商圈小区选择", options);
    }
    $("#myffrmFName").click(function () {
        var str1=$("#myffrmFName").val();
        var str2=$("#myffrmFitmentID").val(),str3;
        if(str1!=""&&str1!=null){
            for(var i=0;i<str1.split(',').length-1;i++){
                str3=str1.split(',')[i]+"_"+str2.split(',')[i+1];
                $(":checkbox[value='"+str3+"']").attr("checked",true)
            }
        }
        if ($("#div_fitmentid").is(":hidden")) {
            $("#div_fitmentid").show();
        }
        else {
            $("#div_fitmentid").hide();
        }
    });
    function zxValue(obj){
        var nr = $(obj).val().split("_");
        if (!$(obj).attr('checked')) {
            $("#myffrmFName").val($("#myffrmFName").val().replace(nr[0] + ",", ""));
            $("#myffrmFitmentID").val($("#myffrmFitmentID").val().replace(","+nr[1], ""));
        }
        else {
            if ($("#myffrmFName").val().indexOf(nr[0]) < 0){
                $("#myffrmFName").val($("#myffrmFName").val() + nr[0] + ",");
                $("#myffrmFitmentID").val("," + nr[1] + $("#myffrmFitmentID").val());
            }
        }
    }
    function CloseRoleCheck(objId) {
        $("#"+objId).hide();
    }
    $("#Tab_Print" + '<%=NavTabId %>').click(function () {
        $("body").append("<div id=\"tabprint_div\" style=\" position:absolute;top:-2000px; left:-2000px;\" align=\"center\"><table class=\"prin_tab\" heght=\"auto\" width=\"90%\"></table></div>");
        $("#tabprint_div .prin_tab").append($(".grid thead").html());
        $(":checked").each(function () {
            $("#tabprint_div .prin_tab").append("<tr>" + $(this).parents("tr").html() + "</tr>");
            this.checked = false;
        });
        $("#tabprint_div th").attr("style", "");
        $("#tabprint_div td").attr("style", "");
        $("#tabprint_div th div").attr("style", "font-size:18px; text-align:center;");
        $("#tabprint_div td div").attr("style", "font-size:18px; text-align:center;");
        $("#tabprint_div").printArea();
        $("#tabprint_div").remove();
    });
    function ReloadHouseZPcheck(B) {
        $.ajax({
            url: 'House/HouseAPI.ashx',
            data: { HouseID: B },
            success: function (result) {

                $("#thedhl").html(result);
                $("#thodhl tr th:eq(0)").width($("#thedhl tr td:eq(0)").width());
                $("#thodhl tr th:eq(1)").width($("#thedhl tr td:eq(1)").width());
                $("#thodhl tr th:eq(2)").width($("#thedhl tr td:eq(2)").width());
                $("#thodhl tr th:eq(3)").width($("#thedhl tr td:eq(3)").width());
            }
        });
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
    }
</script>

<style>
    #div_fitmentid {
        padding-top: 8px;
        border: 1px solid #ccc;
        background: #fff;
        top: 25px;
        height: 30px;
        position: absolute;
        z-index: 9999;
    }

        #div_fitmentid input, #div_myffrmFitmentID input {
            width: auto;
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
        height: 24px;
        padding-left: 4px;
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
    }

    #HouseZPcheckdiv .grid .gridThead TH {
        border-right-width: 1px;
        border-bottom-width: 1px;
    }

    #HouseZPcheckdiv .grid .gridTbody TD {
        border-right-width: 1px;
        border-bottom-width: 1px;
        border-right-color: #C3C3C3;
        border-bottom-color: #C3C3C3;
        padding-bottom: 3px;
    }

    #HouseZPcheckdiv .grid Table td {
        border-left-color: #000000;
        border-top-color: #000000;
    }
</style>

<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" class="HList" onsubmit="return navTabSearch(this);" action="House/HouseZPcheck.aspx" method="post">
        <div id="HouseZPcheckdiv" style="position: relative">
            <div class="pageHeader">
                <div class="searchBar" id="HouseZPcheckSearchBar">
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>状态：</label>
                                <asp:DropDownList ID="ffrmStateID" runat="server"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label title="区域">区域：</label>
                                <asp:TextBox ID="myffrmtxtArea" runat="server" Width="100px" ReadOnly="true" onclick="OpenArea('myffrmtxtArea','myffrmtxtArea2')"></asp:TextBox>
                                <asp:TextBox ID="myffrmtxtArea2" runat="server" Style="display: none;"></asp:TextBox>
                                <asp:DropDownList ID="mysfrmAreaID" runat="server" Style="width: 73px; display: none;"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>总价：</label>
                                <asp:TextBox ID="myffrmPrice1" runat="server" Width="30px"></asp:TextBox>
                                -
                            <asp:TextBox ID="myffrmPrice2" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>面积：</label>
                                <asp:TextBox ID="myffrmarea1" runat="server" Width="30px"></asp:TextBox>
                                -
                            <asp:TextBox ID="myffrmarea2" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>楼层：</label>
                                <asp:TextBox ID="myffrmbuild_floor1" runat="server" Width="30px"></asp:TextBox>
                                -
                            <asp:TextBox ID="myffrmbuild_floor2" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>首录人：</label>
                                <asp:TextBox ID="myffrmOwnerEmployeeID" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                <div class="subBar">
                                    <ul>
                                        <li>
                                            <div class="buttonActive">
                                                <div class="buttonContent">
                                                    <button type="submit" onclick="return formFind()">检索</button>
                                                </div>
                                            </div>
                                        </li>
                                        <li><a class="button" href="House/HouseZPcheckFrom.aspx?NavTabId=<%=NavTabId %>&type=hl" target="dialog" mask="true" mixable="false" fresh="true" rel='dlg_House2' title="高级检索" width="620" height="450"><span>高级检索</span></a></li>
                                        <li><a class='button' href="House/HouseZPcheck.aspx?NavTabId=<%=NavTabId %>" target="navTab" rel="<%=NavTabId %>" title="出售信息"><span>清空</span></a></li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table class="searchContent"> 
                        <tr>
                            <td class="dateRange">
                                <label>栋号：</label>
                                <asp:TextBox ID="myffrmbuild_id" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>房号：</label>
                                <asp:TextBox ID="myffrmbuild_room" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>户型：</label>
                                <asp:DropDownList ID="myffrmform_bedroom" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>1-2</asp:ListItem>
                                    <asp:ListItem>2-3</asp:ListItem>
                                    <asp:ListItem>3-4</asp:ListItem>
                                    <asp:ListItem>4-5</asp:ListItem>
                                    <asp:ListItem>5以上</asp:ListItem>
                                    <asp:ListItem>自定义</asp:ListItem>
                                </asp:DropDownList>
                                <span id="myffhx">
                                    <asp:TextBox ID="myffrmform_bedroom1" runat="server" Width="20px"></asp:TextBox>
                                    -
                                <asp:TextBox ID="myffrmform_bedroom2" runat="server" Width="20px"></asp:TextBox>
                                </span>
                            </td>
                            <td class="dateRange">
                                <label title="房源编号">编号：</label>
                                <asp:TextBox ID="myffrmShi_id" runat="server" title="房源编号" Width="85px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label title="房东电话/联系电话">电话：</label>
                                <asp:TextBox ID="myffrmlandlord_tel2" runat="server" title="房东电话/联系电话"
                                    Width="85px" MaxLength="12"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label title="标签">标签</label>
                                <asp:DropDownList ID="myffrmLabel" CssClass="frmLabel" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">经济型</asp:ListItem>
                                    <asp:ListItem Value="2">学区房</asp:ListItem>
                                    <asp:ListItem Value="3">地段房</asp:ListItem>
                                    <asp:ListItem Value="4">老城区</asp:ListItem>
                                    <asp:ListItem Value="5">地铁房</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label title="总部认证">总部认证：</label>
                                <asp:DropDownList ID="myffrmstate_ZBCheck" runat="server" CssClass="myffrmstate_ZBCheck"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label title="照片审核">照片审核状态：</label>
                                <asp:DropDownList ID="myffrmIsSh" runat="server" CssClass="ffrmIsSh">
                                    <asp:ListItem Value="2">全部</asp:ListItem>
                                    <asp:ListItem Value="0">未审核</asp:ListItem>
                                    <asp:ListItem Value="1">已审核</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label title="照片待定">照片待定状态：</label>
                                <asp:DropDownList ID="myffrmIsBh" runat="server" CssClass="ffrmIsBh">
                                    <asp:ListItem Value="2">全部</asp:ListItem>
                                    <asp:ListItem Value="0">未待定</asp:ListItem>
                                    <asp:ListItem Value="1">已待定</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <%-- <td>
                                <input type="checkbox" id="cHasImage" value="on" onclick="SetCheckValue(this, 'on', 'myffrmHasImage')" />照片
                                <input type="hidden" id="myffrmHasImage" name="myffrmHasImage" value="on" />
                            </td>--%>
                            <td>
                                <input type="checkbox" id="cHasKey" value="on" onclick="SetCheckValue(this, 'on', 'myffrmHasKey')" />钥匙
                                <input type="hidden" id="myffrmHasKey" name="myffrmHasKey" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="cHasSuXiao" value="on" onclick="SetCheckValue(this, 'on', 'myffrmSuXiao')" />限时
                                <input type="hidden" id="myffrmSuXiao" name="myffrmSuXiao" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="cCanKP" value="on" onclick="SetCheckValue(this, 'on', 'myffrmCanKP')" />申请开盘
                                <input type="hidden" id="myffrmCanKP" name="myffrmCanKP" value="" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="pageContent" style="padding: 0px 2px 2px 2px; position: static; display: inherit;">
                    <link href="themes/css/other.css" rel="stylesheet" type="text/css" />
                    <div class='panelBar'>
                        <ul class='toolBar'>
                            <li class='line'>line</li>
                            <%=z_bottom()%>
                        </ul>
                    </div>

                    <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
                    <input type="hidden" name="status" value="status" />
                    <input type="hidden" name="keywords" value="keywords" />
                    <input type="hidden" name="numPerPage" value="20" />
                    <input type="hidden" name="orderField" value="" />
                    <input type="hidden" name="orderDirection" value="" />
                    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                        DataKeyNames="HouseID,shi_id,SeeHouseType,aType" DataSourceID="ods"
                        AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0"
                        GridLines="None" EnableViewState="False">
                        <Columns>
                            <asp:TemplateField HeaderText="选择">
                                <HeaderTemplate>
                                    <input type="checkbox" name="house_cb" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <input name="ids" type="checkbox" value="<%#Eval("HouseID")%>" />
                                </ItemTemplate>
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <%#Eval("ImageInfor")%>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="外网展示">
                                <ItemTemplate>
                                    <%#Eval("ISefw")%>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="state_ZBCheckName" HeaderText="总部认证" SortExpression="state_ZBCheck">
                                <ItemStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField DataField="exe_date" HeaderText="登记日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="exe_date">
                                <ItemStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField DataField="update_date" HeaderText="更新日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="update_date">
                                <ItemStyle Width="80" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="房源编号">
                                <ItemTemplate>
                                    <a href="javascript:OpenHouseEdit_SF('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>','1')">
                                        <%#Eval("shi_id")%></a>
                                </ItemTemplate>
                                <ItemStyle Width="120px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="HouseDicName" HeaderText="楼盘" SortExpression="HouseDicName">
                                <ItemStyle Width="120" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AreaName" HeaderText="区域">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SanjakName" HeaderText="商圈">
                                <ItemStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField DataField="HouseDicAddress" HeaderText="楼盘地址">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="是否审核">
                                <ItemTemplate>
                                    <%#Eval("IsSh")!= null ? "已审核":""%>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="是否待定">
                                <ItemTemplate>
                                    <%#Eval("IsBh")!= null?"待定":""%>
                                </ItemTemplate>
                                <ItemStyle Width="50" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.H_houseinfor" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                        <SelectParameters>
                            <asp:Parameter Name="whereClause" Type="String" />
                            <asp:Parameter Name="orderClause" Type="String" DefaultValue="update_date desc" />
                            <asp:Parameter Name="selects" Type="String" />
                            <asp:Parameter Name="startRowIndex" Type="Int32" />
                            <asp:Parameter Name="maximumRows" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="180" RowTarget="HouseID" PageNumberNav="true" RowRel="HouseID">
                    </TCL:GridViewExtender>
                </div>
            </div>
        </div>
        <div style="display: none;">
            <asp:DropDownList ID="ffrmaType" CssClass="ffrmaType" runat="server" Width="73px">
                <asp:ListItem></asp:ListItem>
                <asp:ListItem Value="0">出售</asp:ListItem>
                <asp:ListItem Value="1">出租</asp:ListItem>
            </asp:DropDownList>
        </div>
    </form>
</body>

