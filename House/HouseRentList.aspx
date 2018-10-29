<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="HouseRentList.aspx.cs" Inherits="HouseMIS.Web.House.HouseRentList" EnableEventValidation='false' ViewStateEncryptionMode='Never' %>

<style type="text/css">
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

    #houseListdiv .grid .gridThead TH {
        border-right-width: 1px;
        border-bottom-width: 1px;
    }

    #houseListdiv .grid .gridTbody TD {
        border-right-width: 1px;
        border-bottom-width: 1px;
        border-right-color: #C3C3C3;
        border-bottom-color: #C3C3C3;
        padding-bottom: 3px;
    }

    #houseListdiv .grid Table td {
        border-left-color: #000000;
        border-top-color: #000000;
    }
</style>

<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" class="HList" onsubmit="return navTabSearch(this);" action="House/HouseRentList.aspx" method="post">
        <div id="houseListdiv" style="position: relative">
            <div class="pageHeader">
                <div class="searchBar" id="HouseListSearchBar">
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>状态：</label>
                                <asp:DropDownList ID="myffrmRentStateID" runat="server"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label title="区域">区域：</label>
                                <asp:TextBox ID="myffrmtxtRentArea" runat="server" Width="100px" ReadOnly="true" onclick="OpenArea('myffrmtxtRentArea','myffrmtxtRentArea_Item')"></asp:TextBox>
                                <asp:TextBox ID="myffrmtxtRentArea_Item" runat="server" Style="display: none;"></asp:TextBox>
                                <%--<asp:DropDownList ID="mysfrmAreaID" runat="server" Style="width: 73px; display: none;"></asp:DropDownList>--%>
                            </td>
                            <td class="dateRange">
                                <label>装修：</label>
                                <asp:TextBox ID="myffrmRentFName" runat="server" ReadOnly="true" Width="80px"></asp:TextBox>
                                <asp:TextBox ID="myffrmRentFitmentID" runat="server" Style="display: none;"></asp:TextBox>
                                <div id="div_fitmentid_rent" style="padding-top: 8px; border: 1px solid #ccc; background: #e0dfdf; top: 20px; height: auto; position: absolute; z-index: 9999;">
                                    <div style="cursor: pointer; float: right; margin: 10px;">
                                        <img onclick="CloseRoleCheck('div_fitmentid_rent')" src="/img/bar_close.gif">
                                    </div>
                                    <%=GetFitmentID() %>
                                </div>
                            </td>

                            <td class="dateRange">
                                <label>租金：</label>
                                <asp:TextBox ID="myffrmRent_Price1" runat="server" Width="30px"></asp:TextBox>
                                -
                            <asp:TextBox ID="myffrmRent_Price2" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>面积：</label>
                                <asp:TextBox ID="myffrmRentarea1" runat="server" Width="30px"></asp:TextBox>
                                -
                            <asp:TextBox ID="myffrmRentarea2" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>楼层：</label>
                                <asp:TextBox ID="myffrmrentbuild_floor1" runat="server" Width="30px"></asp:TextBox>
                                -
                            <asp:TextBox ID="myffrmrentbuild_floor2" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>首录人：</label>
                                <asp:TextBox ID="myffrmOwnerEmployeeID" runat="server" Width="50px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>栋号：</label>
                                <asp:TextBox ID="myffrmrentbuild_id" runat="server" Width="40px"></asp:TextBox>
                                <asp:TextBox ID="myffrmrentbuild_id2" runat="server" Width="40px" Style="display: none"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>房号：</label>
                                <asp:TextBox ID="myffrmrentbuild_room" runat="server" Width="30px"></asp:TextBox>
                                <asp:TextBox ID="myffrmrentbuild_room2" runat="server" Width="30px" Style="display: none"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>户型：</label>
                                <asp:DropDownList ID="myffrmrentform_bedroom" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>1-2</asp:ListItem>
                                    <asp:ListItem>2-3</asp:ListItem>
                                    <asp:ListItem>3-4</asp:ListItem>
                                    <asp:ListItem>4-5</asp:ListItem>
                                    <asp:ListItem>5以上</asp:ListItem>
                                    <asp:ListItem>自定义</asp:ListItem>
                                </asp:DropDownList>
                                <span id="myffrenthx">
                                    <asp:TextBox ID="myffrmrentform_bedroom1" runat="server" Width="20px"></asp:TextBox>
                                    -
                                <asp:TextBox ID="myffrmrentform_bedroom2" runat="server" Width="20px"></asp:TextBox>
                                </span>
                            </td>
                            <td class="dateRange">
                                <label title="房源编号">编号：</label>
                                <asp:TextBox ID="myffrmRentShi_id" runat="server" title="房源编号" Width="85px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label title="房东电话/联系电话">电话：</label>
                                <asp:TextBox ID="myffrmrentlandlord_tel2" runat="server" title="房东电话/联系电话" Width="85px" MaxLength="12"></asp:TextBox>
                            </td>
                            <%--<td class="dateRange">
                                <label title="总部认证">总部认证：</label>
                                <asp:DropDownList ID="myffrmRentstate_ZBCheck" runat="server" CssClass="myffrmRentstate_ZBCheck"></asp:DropDownList>
                            </td>--%>
                            <td>
                                <input type="checkbox" id="cRentHasImage" value="on" onclick="SetCheckValue(this, 'on', 'myffrmRentHasImage')" />照片
                                <input type="hidden" id="myffrmRentHasImage" name="myffrmRentHasImage" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="cRentHasKey" value="on" onclick="SetCheckValue(this, 'on', 'myffrmRentHasKey')" />钥匙
                                <input type="hidden" id="myffrmRentHasKey" name="myffrmRentHasKey" value="" />
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
                                <label>委托单号：</label>
                                <asp:TextBox ID="myffrmorderNum_Rent" runat="server" Width="80px"></asp:TextBox>
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
                                        <li><a class="button" href="House/HouseRentListFrom.aspx?NavTabId=<%=NavTabId %>&type=hl" target="dialog" mask="true" mixable="false" fresh="true" rel='dlg_HouseRent' title="高级检索" width="620" height="450"><span>高级检索</span></a></li>
                                        <li><a class='button' href="House/HouseRentList.aspx?NavTabId=<%=NavTabId %>" target="navTab" rel="<%=NavTabId %>" title="出租信息"><span>清空</span></a></li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="pageContent" style="padding: 0px 2px 2px 2px; position: static; display: inherit;">
                    <link href="themes/css/other.css" rel="stylesheet" type="text/css" />
                    <div class='panelBar'>
                        <ul class='toolBar'>
                            <%--<%=AreaGroupBtn.ToString() %>--%>
                            <li class='line'>line</li>
                            <%--<li><a class='add' id="sell" href='House/HouseRentList.aspx?OperType=0&NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="出租信息"><span>出售</span></a></li>
                    <li><a class='add' id="zu" href='House/HouseRentList.aspx?OperType=1&NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="出租信息"><span>出租</span></a></li>--%>
                            <li><a class='add' href="House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&OperType=5" target="navTab" rel="<%=NavTabId %>" title="出租信息"><span>我的收藏</span></a></li>
                            <li><a class='add' href="House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&OperType=8" target="navTab" rel="<%=NavTabId %>" title="出租信息"><span>今日房源</span></a></li>
                            <li class='line'>line</li>
                            <li><a class="delete" href="House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&doAjax=true&doType=del" rel="ids" target="selectedTodo" title="确定要删除吗?"><span>删除</span></a></li>
                            <li><a class='add' href='House/HouseRentForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&todo=1' target='dialog' rel='dlg_page2' width='660' height='700' maxable='false' mask="true" title="新增出租"><span>添加房源</span></a></li>
                            <li class='line'>line</li>
                            <li><%--<a class='edit' href='House/HousePic.aspx?PicTypeID=1&HouseID={HouseID}' target='dialog' rel='HousePIC' width='760' height='620' fresh='true' maxable='false'><span>户型图</span></a>--%></li>
                            <%=ExpotA.ToString()%>
                            <asp:Literal runat="server" ID="ViewSet_gv"></asp:Literal>
                            <%--<li><a class='edit' id="all" href='House/HouseRentList.aspx?NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="出租信息"><span>所有房源</span></a></li>--%>
                        </ul>
                    </div>
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
                            <%-- <asp:BoundField DataField="state_ZBCheckName" HeaderText="总部认证" SortExpression="state_ZBCheck">
                                <ItemStyle Width="80" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="exe_date" HeaderText="登记日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="exe_date">
                                <ItemStyle Width="80" />
                            </asp:BoundField>
                            <asp:BoundField DataField="update_date" HeaderText="更新日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="update_date">
                                <ItemStyle Width="80" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="房源编号">
                                <ItemTemplate>
                                    <a href="javascript:OpenHouseEdit_Rent('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>','1')">
                                        <%--<span<%#GetShi_idColor(Eval("exe_date"),Eval("FollowUp_Date")) %>><%#Eval("shi_id")%>
                                        </span>--%><%#Eval("shi_id")%></a>
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
                            <asp:BoundField DataField="HouseType" HeaderText="户型" SortExpression="form_foreroom">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FloorAll" HeaderText="楼层" SortExpression="build_floor">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="面积(㎡)" SortExpression="Build_area">
                                <ItemTemplate><%#str(Eval("Build_area"))%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Renovation" HeaderText="装修" SortExpression="FitmentID">
                                <ItemStyle Width="40" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Orientation" HeaderText="朝向" SortExpression="FacetoID">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Year" HeaderText="年代" SortExpression="YearID">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="状态">
                                <ItemTemplate>
                                    <%#Eval("SeeHouseType") %> <%#Eval("IsState").ToString()=="1"?"<img src='/img/z.gif' />":""%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Rent_Price" HeaderText="租金(元)" SortExpression="Rent_Price">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                            <asp:BoundField DataField="note" HeaderText="备注">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FollowUp_Date" HeaderText="最后跟进日期" SortExpression="FollowUp_Date" DataFormatString="{0:yyyy-MM-dd}">
                                <ItemStyle Width="90" />
                            </asp:BoundField>
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
    </form>
    <div id="<%=NavTabId %>" style="display: none;">
        <ul>
            <li key="true" href='House/FollowUpEditor.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' width='375' height='404' fresh='true' maxable='false' title="增加跟进">增加跟进</li>
            <li key="true" href='House/AddHouseKey.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' width='600' height='320' fresh='true' maxable='false' title="增加钥匙">增加钥匙</li>
            <li key="true" href="House/HouseRentList.aspx?OperTypes=3&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" id="tuijian" title="推荐房源">推荐房源</li>
            <li key="true" href="House/HouseRentList.aspx?OperTypes=6&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="取消推荐">取消推荐</li>
            <li key="true" href="House/HouseRentList.aspx?OperTypes=4&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="收藏房源">收藏房源</li>
            <li key="true" href="House/HouseRentList.aspx?OperTypes=5&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="取消收藏">取消收藏</li>
            <li key="true" href="House/HouseRentList.aspx?OperType=5&NavTabId=<%=NavTabId %>&doAjax=true" rel="<%=NavTabId %>" target="navTab" title="我的收藏">我的收藏</li>
            <li key="true" href="House/CheckKeys.aspx?NavTabId=<%=NavTabId %>&doAjax=true" rel='checkkeys' target='dialog' width='400' height='200' fresh='true' maxable='false' title="查看钥匙">查看钥匙</li>
            <%--<li key="true" href="House/HouseReporSet.aspx?NavTabId=<%=NavTabId %>&doAjax=true" width="400" height="340" target="dialog" rel='reportset' title="上报房源">上报房源</li>
            <li key="true" href="House/HouseVRPlanRequest.aspx?NavTabId=<%=NavTabId %>&doAjax=true" target='dialog' width="420" height="280" fresh='true' maxable='false' title="申请全景拍摄">申请全景拍摄</li>
            <li key="true" href="House/HouseOutSet.aspx?NavTabId=<%=NavTabId %>&doAjax=true" rel='outsetRel' target='dialog' width='380' height='400' fresh='true' maxable='false' title="添加到走漏名单">添加到走漏名单</li>
            <li key="true" href="House/HouseMessage.aspx?NavTabId=<%=NavTabId %>&doAjax=true" width="400" height="200" target="dialog" rel="housemesga" maxable='false' class="no" mask="true" title="房源公告">房源公告</li>--%>
            <li key="true" rel="dlg_page1" href="customer/Customerbring.aspx?NavTabId=16_menu0022&doAjax=true&isRent=1" width="590" height="380" target="dialog" maxable='false' title="客户带看">客户带看</li>
            <li href='House/HouseRentList.aspx?NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="出租-所有房源">所有房源</li>
            <li href='House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&OperType=2' target="navTab" rel="<%=NavTabId %>" title="出租-我分部租房">我分部租房</li>
            <%--<li href='House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&OperType=9' target="navTab" rel="<%=NavTabId %>" title="我分部售房">我分部售房</li>--%>
            <li href='House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&OperType=3' target="navTab" rel="<%=NavTabId %>" title="出租-我的租房">我的租房</li>
            <%--<li href='House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&OperType=4' target="navTab" rel="<%=NavTabId %>" title="我的售房">我的售房</li>--%>
            <li key="true" href='House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&OperType=6' target="navTab" rel="<%=NavTabId %>" title="出租-重复房源">重复房源</li>
            <li href='House/HouseRentList.aspx?NavTabId=<%=NavTabId %>&OperType=11' target="navTab" rel="<%=NavTabId %>" title="出租-我的核验房源">我的核验房源</li>
            <li key="true" href='House/HouseProEdit.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' width='400' height='470' fresh='true' maxable='false' title="房源质疑">房源质疑</li>
            <%-- <li key="true" href="House/HouseRentList.aspx?OperType=12&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="导入到二手房">导入到二手房</li>--%>
        </ul>
    </div>

    <script type="text/javascript">
        $(function () {
            $("#myffrenthx").css("display", "none");
            $("#div_fitmentid_rent").css("display", "none");
            if ($('#myffrmrentform_bedroom').children('option:selected').val() == "自定义") { $("#myffrenthx").css("display", "inline"); }

            if (<%=pHasImage %>== "1") {
                $("#cRentHasImage").attr("checked", true);
                $("#myffrmRentHasImage").val("on");
            }
            if (<%=pHasKey %>== "1") {
                $("#cRentHasKey").attr("checked", true);
                $("#myffrmRentHasKey").val("on");
            }
            $('#myffrmrentform_bedroom').change(function () {
                var p1 = $(this).children('option:selected').val();//这就是selected的值
                if (p1 == "自定义") {
                    $('#myffrmrentform_bedroom1').val("");
                    $('#myffrmrentform_bedroom2').val("");
                    $("#myffrenthx").css("display", "inline");
                }
                else {
                    if (p1.split("-").length == 2 && p1 != "") {
                        $('#myffrmrentform_bedroom1').val(p1.split("-")[0]);
                        $('#myffrmrentform_bedroom2').val(p1.split("-")[1]);
                    }
                    else if (p1.split("-").length == 1 && p1 != "") {
                        $('#myffrmrentform_bedroom1').val("999");
                        $('#myffrmrentform_bedroom2').val("999");
                    }
                    else {
                        $('#myffrmrentform_bedroom1').val("");
                        $('#myffrmrentform_bedroom2').val("");
                    }
                    $("#myffrenthx").css("display", "none");
                }
            })
        });
        //打开房源编辑页面
        function OpenHouseEdit_Rent(houseId, houseNo, atype) {
            // 所有参数都是可选项。
            var url = 'House/HouseRentForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
            var options = { width: 660, height: 700, mixable: false }
            $.pdialog.open(url, "housewiew2_" + houseId, houseNo + "-修改出租房源", options);
        }
        function OpenArea(name1, name2) {
            var url = "House/AreaV.aspx?tn1=" + name1 + "&tn2=" + name2 + "&tn3=AreaRentVillage";
            var options = { width: 580, height: 380, mixable: false }
            $.pdialog.open(url, "AreaRentVillage", "区域商圈小区选择", options);
        }
        $("#myffrmRentFName").click(function () {
            var str1 = $("#myffrmRentFName").val();
            var str2 = $("#myffrmRentFitmentID").val(), str3;
            if (str1 != "" && str1 != null) {
                for (var i = 0; i < str1.split(',').length - 1; i++) {
                    str3 = str1.split(',')[i] + "_" + str2.split(',')[i + 1];
                    $(":checkbox[value='" + str3 + "']").attr("checked", true)
                }
            }
            if ($("#div_fitmentid_rent").is(":hidden")) {
                $("#div_fitmentid_rent").show();
            }
            else {
                $("#div_fitmentid_rent").hide();
            }
        });
        function zxValue(obj) {
            var nr = $(obj).val().split("_");
            if (!$(obj).attr('checked')) {
                $("#myffrmRentFName").val($("#myffrmRentFName").val().replace(nr[0] + ",", ""));
                $("#myffrmRentFitmentID").val($("#myffrmRentFitmentID").val().replace("," + nr[1], ""));
            }
            else {
                if ($("#myffrmRentFName").val().indexOf(nr[0]) < 0) {
                    $("#myffrmRentFName").val($("#myffrmRentFName").val() + nr[0] + ",");
                    $("#myffrmRentFitmentID").val("," + nr[1] + $("#myffrmRentFitmentID").val());
                }
            }
        }
        function CloseRoleCheck(objId) {
            $("#" + objId).css("display", "none");
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
        function ReloadHouseList(B) {
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
</body>
