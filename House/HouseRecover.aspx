<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="HouseRecover.aspx.cs" Inherits="HouseMIS.Web.House.HouseRecover" EnableEventValidation='false' ViewStateEncryptionMode='Never' %>

<style>
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

<script type="text/javascript">
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

    function OpenHouseEdit(houseId, houseNo, atype) {
        // 所有参数都是可选项。
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 720, mixable: false }
        $.pdialog.open(url, "houserecover_" + houseId, houseNo + "-查看房源", options);
    }

    $("#HouseListSearchBar").find("input" || "select").each(function () {
        if ($(this).attr("value"))
            $(this).attr("value", "");
        if ($(this).attr("text"))
            $(this).attr("text", "");
    });
    $("#HouseListSearchBar").find("select").each(function () {
        if ($(this).attr("value"))
            $(this).attr("value", "");
        if ($(this).attr("text"))
            $(this).attr("text", "");
    });
</script>

<body>
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" class="HouseRecover" onsubmit="return navTabSearch(this);" action="House/HouseRecover.aspx" method="post">
        <div id="houseListdiv" style="position: relative">
            <div class="pageHeader">
                <script language="javascript">
                    //绑定Suggest事件
                    var aala = IntSuggest("myffrmEstateOrAddress", "HouseDicID", "/Ajax/SearchHouseDic.ashx?kw=");
            </script>
                <div class="searchBar" id="HouseListSearchBar">
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label title="登记日期">日期：</label>
                                <asp:TextBox ID="myffrmexe_date1" runat="server" CssClass="date" Width="66px"></asp:TextBox>
                                -

                            <asp:TextBox ID="myffrmexe_date2" runat="server" CssClass="date" Width="66px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label title="楼盘/地址">楼盘：</label>
                                <asp:TextBox ID="myffrmEstateOrAddress" like="true" runat="server" title="楼盘/地址" Width="154px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>栋号：</label>
                                <asp:TextBox ID="myffrmbuild_id" runat="server" Width="40px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>年代：</label>
                                <asp:DropDownList ID="ffrmYearID" runat="server" Style="width: 69px;"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>装修：</label>
                                <asp:DropDownList ID="myffrmFitmentID" runat="server" Style="width: 73px;"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>状态：</label>
                                <asp:DropDownList ID="ffrmStateID" runat="server" Style="width: 73px;"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>楼层：</label>
                                <asp:TextBox ID="myffrmbuild_floor1" runat="server" Width="36px"></asp:TextBox>
                                -<asp:TextBox ID="myffrmbuild_floor2" runat="server" Width="36px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>面积：</label>
                                <asp:TextBox ID="myffrmarea1" runat="server" Width="40px"></asp:TextBox>
                                -

                            <asp:TextBox ID="myffrmarea2" runat="server" Width="40px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>总价：</label>
                                <asp:TextBox ID="myffrmPrice1" runat="server" Width="40px"></asp:TextBox>
                                -

                            <asp:TextBox ID="myffrmPrice2" runat="server" Width="40px"></asp:TextBox>
                            </td>
                            <td>
                                <input type="checkbox" value="on" onclick="SetCheckValue(this, 'on', 'myffrmHasKey')" />钥匙

                            <input type="hidden" id="myffrmHasKey" name="myffrmHasKey" value="" />
                            </td>
                            <td>
                                <input type="checkbox" value="on" onclick="SetCheckValue(this, 'on', 'myffrmAllView')" />3D全景

                            <input type="hidden" id="myffrmAllView" name="myffrmAllView" value="" />
                            </td>
                            <td class="dateRange">
                                <label>物业：</label>
                                <asp:DropDownList ID="ffrmTypeCode" runat="server" Style="width: 73px;"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label title="小区">小区：</label>
                                <asp:DropDownList ID="mysfrmAreaID" runat="server" Style="width: 73px;"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>户型：</label>
                                <asp:DropDownList ID="ffrmform_bedroom" runat="server" Style="width: 42px;">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                </asp:DropDownList>
                                -<asp:DropDownList ID="ffrmform_foreroom" runat="server" Style="width: 42px;">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem>0</asp:ListItem>
                                    <asp:ListItem>1</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>3</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>5</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label title="房源编号">编号：</label>
                                <asp:TextBox ID="myffrmShi_id" runat="server" title="房源编号" Width="96px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label title="房东电话/联系电话">电话：</label>
                                <asp:TextBox ID="myffrmlandlord_tel2" runat="server" title="房东电话/联系电话"
                                    Width="96px" MaxLength="12"></asp:TextBox>
                            </td>
                            <td>
                                <input type="checkbox" value="on" onclick="SetCheckValue(this, 'on', 'mysfrmHasImage')" />照片

                            <input type="hidden" id="mysfrmHasImage" name="mysfrmHasImage" value="" />
                            </td>
                            <td>
                                <input type="checkbox" value="on" onclick="SetCheckValue(this, 'on', 'mysfrmIsVideo')" />视频

                            <input type="hidden" id="mysfrmIsVideo" name="mysfrmIsVideo" value="" />
                            </td>
                            <td>
                                <input type="checkbox" value="on" onclick="SetCheckValue(this, 'on', 'mysfrmHasRecord')" />录音

                            <input type="hidden" id="mysfrmHasRecord" name="mysfrmHasRecord" value="" />
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
                                        <%--<li ><a class="button" href="House/HouseListFrom.aspx?NavTabId=<%=NavTabId %>&type=hl" target="dialog" mask="true" mixable="false" fresh="true" rel='dlg_House2' title="高级检索" width="580" height="565"><span>高级检索</span></a></li>--%>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="pageContent" style="padding: 0px 2px 2px 2px; position: static; display: inherit;">
                <link href="themes/css/other.css" rel="stylesheet" type="text/css" />
                <div class='panelBar'>
                    <ul class='toolBar'>
                        <asp:Literal ID="LiteralID" runat="server"></asp:Literal>
                        <%--<li><a class='add' id="sell" href='House/HouseRecover.aspx?NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="房源还原"><span>房源还原</span></a></li>--%>
                    </ul>
                </div>
                <%-- <div id="nav" style="top:-6px; left:390px;">
        <ul class="xl">
            <li class="menu2" onmouseover="this.className='menu1'" onmouseout="this.className='menu2'">
                更多操作
                <div class="list">
                    <a href='House/FollowUpEditor.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}' target='dialog' rel='dlg_page2' width='367' height='404' fresh='true' maxable='false'>增加跟进</a><br />
                    <a href='House/AddHouseImage.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' rel='dlg_page2' width='381' height='280' fresh='true' maxable='false'>增加照片</a><br />
                    <a href='House/AddHouseKey.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}' target='dialog' rel='dlg_page2' width='400' height='280' fresh='true' maxable='false'>增加钥匙</a><br />
                    <a href="House/HouseList.aspx?OperType=3&NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="ajaxTodo" id="tuijian">推荐房源</a><br />
                    <a href="#">短信通知</a><br />
                    <a href="House/HouseList.aspx?OperType=4&NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="ajaxTodo">收藏房源</a><br />
                    <a href="House/HouseList.aspx?OperType=5&NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="ajaxTodo">取消收藏</a><br />
                    <a href="#">发布到网站</a><br />
                </div>
            </li>
            <li class="menu2" onmouseover="this.className='menu1'" onmouseout="this.className='menu2'">
                更多过滤
                <div class="list">
                    <a href="House/HouseList.aspx?NavTabId=<%=NavTabId %>">所有房源</a><br />
                    <a href="House/HouseList.aspx?OperType=11">我分部房源</a><br />
                    <a href="House/HouseList.aspx?OperType=12">我组房源</a><br />
                    <a href="House/HouseList.aspx?OperType=13">我的房源</a><br />
                    <a href="House/HouseList.aspx?OperType=14">我收藏房源</a><br />
                    <a href="House/HouseList.aspx?OperType=10">重复房源</a><br />
                </div>
            </li>
        </ul>
    </div> --%>

                <%--<div class="panelBar">
        <ul class="toolBar">
            <li class="line"></li>
            <input type="hidden" id ="frmHosueDicID" name="frmHosueDicID" style="width:113px" />
			<li><span>楼盘</span><input type="text" name="HouseDicName" style="width=100" /></li>
            <script language="javascript">
                //绑定Suggest事件
                IntSuggest("HouseDicName", "HouseDicName", "/Ajax/SearchHouseDic.ashx?kw=");
            </script>
			<li><span>状态</span><select Width="118px"><%=HouseState()%>
            </select></li>
            <li class="line"></li>
			<li><span>面积</span><input type="text" class="number" name="bArea" style="width=40" /></li>
			<li><span>- </span><input type="text" class="number" name="eArea" style="width=40" /></li>
            <li class="line"></li>
			<li><span>总价</span><input type="text" class="number" name="bPrice" style="width=40" /></li>
            <li><span>- </span><input type="text" class="number" name="ePrice" style="width=40" /></li>
            <li class="line"></li>
            <li><span>楼层</span><input type="text" class="number" name="bFloor" style="width=40" /></li>
            <li><span>- </span><input type="text" class="number" name="eFloor" style="width=40" /></li>
            <li class="line"></li>
            <li><input type="submit" value="检索" style="height=23" /></li>
            <li><input type="submit" value="更多" style="height=23" /></li>
            <li><input type="submit" value="高级" style="height=23" /></li>
	</div>--%>

                <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
                <input type="hidden" name="status" value="status" />
                <input type="hidden" name="keywords" value="keywords" />
                <input type="hidden" name="numPerPage" value="20" />
                <input type="hidden" name="orderField" value="" />
                <input type="hidden" name="orderDirection" value="" />
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
                    DataKeyNames="HouseID,shi_id,SeeHouseType,aType" DataSourceID="ods"
                    AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0"
                    GridLines="None" EnableModelValidation="True" EnableViewState="False">
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <HeaderTemplate>
                                <input type="checkbox" name="house_cb" group="ids" class="checkboxCtrl" title="全部选择/取消选择">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input name="ids" type="checkbox" value="<%#Eval("HouseID")%>" />
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                            <ItemStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Eval("ImageInfor")%>
                            </ItemTemplate>
                            <ItemStyle Width="50" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="exe_date" HeaderText="登记日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="exe_date">
                            <ItemStyle Width="80" />
                        </asp:BoundField>
                        <asp:BoundField DataField="update_date" HeaderText="更新日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="update_date">
                            <ItemStyle Width="80" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="分类">
                            <ItemTemplate>
                                <span<%#GetAtype(Eval("AtypeName")) %>><%#Eval("AtypeName")%>
                                </span>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="房源编号">
                            <ItemTemplate>
                                <a href="javascript:OpenHouseEdit('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>','1')">
                                    <span><%#Eval("shi_id")%>
                                    </span></a>
                            </ItemTemplate>
                            <HeaderStyle Width="80px" />
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="build_id" HeaderText="楼栋" SortExpression="build_id">
                            <ItemStyle Width="120" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HouseDicName" HeaderText="楼盘" SortExpression="HouseDicName">
                            <ItemStyle Width="120" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HouseDicAddress" HeaderText="楼盘地址">
                            <HeaderStyle Width="100px" />
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
                        <asp:TemplateField HeaderText="总价(万)" SortExpression="sum_price">
                            <ItemTemplate><%#str(Eval("sum_price"))%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Ohter2ID" HeaderText="单价(元)" SortExpression="Ohter2ID">
                            <ItemStyle Width="60" />
                        </asp:BoundField>
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
                                <span><%#GetState(Eval("SeeHouseType")) %></span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="Rent_Price" HeaderText="租金(元)" SortExpression="Rent_Price">
                            <ItemStyle Width="60" />
                        </asp:BoundField>

                        <%--<asp:BoundField DataField="note" HeaderText="备注">
                    <ItemStyle Width="60" />
                </asp:BoundField>

                <asp:BoundField DataField="FollowUp_Date" HeaderText="最后跟进日期" SortExpression="FollowUp_Date" DataFormatString="{0:d}">
                    <ItemStyle Width="90" />
                </asp:BoundField>      --%>
                        <asp:TemplateField HeaderText="删除人">
                            <ItemTemplate><%#GetEmployeeName(Eval("DelEmployeeID"))%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DelDate" HeaderText="删除日期" DataFormatString="{0:yyyy-MM-dd}">
                            <ItemStyle Width="70" />
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
                <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="168" RowTarget="HouseID" PageNumberNav="false" RowRel="HouseID">
                </TCL:GridViewExtender>
            </div>
        </div>
    </form>
</body>
