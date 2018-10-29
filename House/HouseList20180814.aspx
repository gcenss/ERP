<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="HouseList20180814.aspx.cs" Inherits="HouseMIS.Web.House.HouseList20180814" EnableEventValidation='false' ViewStateEncryptionMode='Never' %>

<%@ OutputCache Duration="60" VaryByParam="*" %>

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
    <form rel="pagerForm" runat="server" name="pagerForm" id="pagerForm" class="HList" onsubmit="return navTabSearch(this);" action="House/HouseList.aspx" method="post">
        <div id="houseListdiv" style="position: relative">
            <div class="pageHeader">
                <div class="searchBar" id="HouseListSearchBar">
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
                                <%--<asp:DropDownList ID="mysfrmAreaID" runat="server" Style="width: 73px; display: none;"></asp:DropDownList>--%>
                            </td>
                            <td class="dateRange">
                                <label>装修：</label>
                                <asp:TextBox ID="myffrmFName" runat="server" ReadOnly="true" Width="80px"></asp:TextBox>
                                <asp:TextBox ID="myffrmFitmentID" runat="server" Style="display: none;"></asp:TextBox>

                                <div id="div_fitmentid">
                                    <div style="cursor: pointer; float: right; margin: 10px;">
                                        <img onclick="CloseRoleCheck('div_fitmentid')" src="/img/bar_close.gif">
                                    </div>
                                    <%=GetFitmentID() %>
                                </div>
                            </td>

                            <td class="dateRange">
                                <label>总价：</label>
                                <asp:TextBox ID="myffrmPrice1" runat="server" Width="30px"></asp:TextBox>
                                -
                            <asp:TextBox ID="myffrmPrice2" runat="server" Width="30px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>实价：</label>
                                <asp:TextBox ID="myffrmmin_price_begin" runat="server" Width="30px"></asp:TextBox>
                                -
                            <asp:TextBox ID="myffrmmin_price_end" runat="server" Width="30px"></asp:TextBox>
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
                                        <li><a class="button" href="House/HouseListFrom.aspx?NavTabId=<%=NavTabId %>&type=hl" target="dialog" mask="true" mixable="false" fresh="true" rel='dlg_House2' title="高级检索" width="620" height="450"><span>高级检索</span></a></li>
                                        <li><a class='button' href="House/HouseList.aspx?NavTabId=<%=NavTabId %>" target="navTab" rel="<%=NavTabId %>" title="出售信息"><span>清空</span></a></li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>栋号：</label>
                                <asp:TextBox ID="myffrmbuild_id" runat="server" Width="40px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>房号：</label>
                                <asp:TextBox ID="myffrmbuild_room" runat="server" Width="40px"></asp:TextBox>
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
                                <label title="e房网">e房网</label>
                                <asp:DropDownList ID="myffrmisefw" CssClass="myffrmisefw" runat="server">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="1">待上架</asp:ListItem>
                                    <asp:ListItem Value="2">否</asp:ListItem>
                                    <asp:ListItem Value="3">是</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td class="dateRange">
                                <label title="标签">标签</label>
                                <asp:DropDownList ID="myffrmLabel" CssClass="myffrmLabel" runat="server">
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
                                <label title="委托">委托：</label>
                                <asp:DropDownList ID="ffrmEntrustTypeID" runat="server" CssClass="ffrmEntrustTypeID"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label>首录人：</label>
                                <asp:TextBox ID="myffrmOwnerEmployeeID" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>限时负责人：</label>
                                <asp:TextBox ID="myffrmFast_UserName" runat="server" Width="50px"></asp:TextBox>
                            </td>
                            <td>
                                <input type="checkbox" id="cHasImage" runat="server" value="on" onclick="SetCheckValue(this, 'on', 'myffrmHasImage')" />照片
                                <input type="hidden" id="myffrmHasImage" runat="server" name="myffrmHasImage" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="cHasKey" runat="server" value="on" onclick="SetCheckValue(this, 'on', 'myffrmHasKey')" />钥匙
                                <input type="hidden" id="myffrmHasKey" runat="server" name="myffrmHasKey" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="cHasSuXiao" runat="server" value="on" onclick="SetCheckValue(this, 'on', 'myffrmSuXiao')" />限时
                                <input type="hidden" id="myffrmSuXiao" runat="server" name="myffrmSuXiao" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="cCanKP" runat="server" value="on" onclick="SetCheckValue(this, 'on', 'myffrmCanKP')" />申请开盘
                                <input type="hidden" id="myffrmCanKP" runat="server" name="myffrmCanKP" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="cfinPhoto" runat="server" value="on" onclick="SetCheckValue(this, 'on', 'myffrmfinPhoto')" />完成摄影
                                <input type="hidden" id="myffrmfinPhoto" runat="server" name="myffrmfinPhoto" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="myCollect" runat="server" value="on" onclick="SetCheckValue(this, 'on', 'myffrmmyCollect')" />我的收藏
                                <input type="hidden" id="myffrmmyCollect" runat="server" name="myffrmmyCollect" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="myHouse" runat="server" value="on" onclick="SetCheckValue(this, 'on', 'myffrmmyHouse')" />我的售房
                                <input type="hidden" id="myffrmmyHouse" runat="server" name="myffrmmyHouse" value="" />
                            </td>
                            <td>
                                <input type="checkbox" id="myHouseCheck" runat="server" value="on" onclick="SetCheckValue(this, 'on', 'myffrmmyHouseCheck')" />我的核验
                                <input type="hidden" id="myffrmmyHouseCheck" runat="server" name="myffrmmyHouseCheck" value="" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="pageContent" style="padding: 0px 2px 2px 2px; position: static; display: inherit;">
                    <link href="themes/css/other.css" rel="stylesheet" type="text/css" />
                    <div class='panelBar'>
                        <ul class='toolBar'>
                            <li><a class='add' href="House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=5" target="navTab" rel="<%=NavTabId %>" title="出售信息"><span>我的收藏</span></a></li>
                            <li><a class='add' href="House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=8" target="navTab" rel="<%=NavTabId %>" title="出售信息"><span>今日房源</span></a></li>
                            <li class='line'>line</li>
                            <li><a class="delete" href="House/HouseList.aspx?NavTabId=<%=NavTabId %>&doAjax=true&doType=del" rel="ids" target="selectedTodo" title="确定要删除吗?"><span>删除</span></a></li>
                            <li><a class='add' href='House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&todo=1' target='dialog' rel='dlg_page2' width='660' height='720' maxable='false' mask="true" title="添加出售房源"><span>添加房源</span></a></li>
                            <%=efw() %>
                            <li class='line'>line</li>
                            <%=ExpotA.ToString()%>
                            <%-- <asp:Literal runat="server" ID="ViewSet_gv"></asp:Literal>--%>
                        </ul>
                    </div>
                    <div class='panelBar'>
                        <ul class='toolBar'>
                            <%=z_bottom()%>
                        </ul>
                    </div>
                    <div class='panelBar'>
                        <ul class='toolBar'>
                            <li><a class="iconL" href="House/FollowUpEditor.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="dialog" width='400' height='410' fresh='true' maxable='false' title="增加跟进"><span>增加跟进</span></a></li>
                            <li><a class="iconL" href="House/AddHouseKey.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="dialog" width='600' height='320' fresh='true' maxable='false' title="增加钥匙"><span>增加钥匙</span></a></li>
                            <li><a class="iconL" href="House/HouseTimeLimit.aspx?NavTabId=16_menu0022&doAjax=true&HouseID={HouseID}" width="590" height="380" target="dialog" maxable='false' title="限时协议"><span>限时协议</span></a></li>
                            <li><a class="iconL" href="House/HouseList.aspx?OperTypes=4&NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="ajaxTodo" title="收藏房源"><span>收藏房源</span></a></li>
                            <li><a class="iconL" href="House/HouseList.aspx?OperTypes=5&NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="ajaxTodo" title="取消收藏"><span>取消收藏</span></a></li>
                            <li><a class="iconL" href="House/CheckKeys.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" rel='checkkeys' target='dialog' width='400' height='200' fresh='true' maxable='false' title="查看钥匙"><span>查看钥匙</span></a></li>
                            <li><a class="iconL" href="customer/Customerbring.aspx?NavTabId=16_menu0022&doAjax=true&HouseID={HouseID}" width="590" height="380" target="dialog" maxable='false' title="客户带看"><span>客户带看</span></a></li>
                            <%-- <li><a href="House/HouseProEdit.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target='dialog' width='400' height='470' fresh='true' maxable='false' title="房源质疑"><span>房源质疑</span></a></li>
                            <li><a href="House/HouseList.aspx?OperTypes=3&NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="ajaxTodo" id="tuijian" title="推荐房源"><span>推荐房源</span></a></li>
                            <li><a href="House/HouseList.aspx?OperTypes=6&NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}" target="ajaxTodo" title="取消推荐"><span>取消推荐</span></a></li>--%>
                        </ul>
                    </div>
                    <div style="overflow: auto;" layouth="185">
                        <asp:Repeater runat="server" ID="rtData">
                            <HeaderTemplate>
                                <table class="list" style="width: 1600px;">
                                    <tbody>
                                        <tr>
                                            <th style="width: 20px">
                                                <input type="checkbox" name="house_cb" group="ids" class="checkboxCtrl" title="全部选择/取消选择"></th>
                                            <th style="width: 50px"></th>
                                            <th style="width: 60px">总部认证</th>
                                            <th style="width: 80px">登记日期</th>
                                            <th style="width: 80px">更新日期</th>
                                            <th style="width: 120px">房源编号</th>
                                            <th style="width: 120px">楼盘</th>
                                            <th style="width: 100px">区域</th>
                                            <th style="width: 80px">商圈</th>
                                            <th>楼盘地址</th>
                                            <th style="width: 90px">户型</th>
                                            <th style="width: 40px">楼层</th>
                                            <th style="width: 50px">面积(㎡)</th>
                                            <th style="width: 50px">总价(万)</th>
                                            <th style="width: 50px">单价(元)</th>
                                            <th style="width: 30px">装修</th>
                                            <th style="width: 30px">朝向</th>
                                            <th style="width: 60px">年代</th>
                                            <th style="width: 60px">状态</th>
                                        </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr target="HouseID" rel="<%#Eval("HouseID")%>" ondblclick="OpenHouseEdit_SF('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>','1')">
                                    <td>
                                        <input name="ids" type="checkbox" value="<%#Eval("HouseID")%>" /></td>
                                    <td><%#Eval("ImageInfor")%></td>
                                    <td><%#Eval("state_ZBCheckName")%></td>
                                    <td><%#Convert.ToDateTime(Eval("exe_date")).ToString("yyyy-MM-dd")%></td>
                                    <td><%#Convert.ToDateTime(Eval("update_date")).ToString("yyyy-MM-dd")%></td>
                                    <td><a href="javascript:OpenHouseEdit_SF('<%#Eval("HouseID") %>','<%#Eval("Shi_id") %>','1')">
                                        <%#Eval("shi_id")%></a></td>
                                    <td><%#Eval("HouseDicName")%></td>
                                    <td><%#Eval("AreaName")%></td>
                                    <td><%#Eval("SanjakName")%></td>
                                    <td><%#Eval("HouseDicAddress")%></td>
                                    <td><%#Eval("HouseType")%></td>
                                    <td><%#Eval("FloorAll")%></td>
                                    <td><%#Eval("Build_area")%></td>
                                    <td><%#Eval("sum_price")%></td>
                                    <td><%#Eval("Ohter2ID")%></td>
                                    <td><%#Eval("Renovation")%></td>
                                    <td><%#Eval("Orientation")%></td>
                                    <td><%#Eval("Year")%></td>
                                    <td><%#Eval("SeeHouseType")%></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate></tbody></table></FooterTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="panelBar">
                        <div class="pages">
                            <span>共<% =TotalCount%>条  每页</span>
                            <select class="combox" name="numPerPage" onchange="navTabPageBreak({numPerPage:this.value})">
                                <option value="<%= NumPerPage %>">选择</option>
                                <option value="20">20</option>
                                <option value="30">30</option>
                                <option value="50">50</option>
                                <option value="100">100</option>
                            </select><span>条 当前第<% =PageNum %>页/共<% =TotalCount/NumPerPage+1 %>页</span>
                        </div>
                        <div class="pagination" targettype="navTab" totalcount="<% =TotalCount %>" numperpage="<% =NumPerPage %>"
                            pagenumshown="<%=PageNumShown %>" currentpage="<% =PageNum %>">
                        </div>
                        <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
                        <input type="hidden" name="status" value="status" />
                        <input type="hidden" name="keywords" value="keywords" />
                        <input type="hidden" name="numPerPage" value="<%=NumPerPage %>" />
                        <input type="hidden" name="pageNum" value="1" />
                        <input type="hidden" name="orderField" value="Name" />
                    </div>

                    <%--<asp:GridView ID="gv" runat="server" AutoGenerateColumns="False"
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
                            <asp:BoundField DataField="HouseType" HeaderText="户型" SortExpression="form_foreroom">
                                <ItemStyle Width="90" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FloorAll" HeaderText="楼层" SortExpression="build_floor">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="面积(㎡)" SortExpression="Build_area">
                                <ItemTemplate><%#Eval("Build_area")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="总价(万)" SortExpression="sum_price">
                                <ItemTemplate><%#Eval("sum_price")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="单价(元)" SortExpression="Ohter2ID">
                                <ItemTemplate><%#Eval("Ohter2ID")%></ItemTemplate>
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
                            <asp:BoundField DataField="note" HeaderText="备注">
                                <ItemStyle Width="60" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.H_houseinfor" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                        <SelectParameters>
                            <asp:Parameter Name="whereClause" Type="String" />
                            <asp:Parameter Name="orderClause" Type="String" DefaultValue="istop desc,update_date desc" />
                            <asp:Parameter Name="selects" Type="String" />
                            <asp:Parameter Name="startRowIndex" Type="Int32" />
                            <asp:Parameter Name="maximumRows" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="180" RowTarget="HouseID" PageNumberNav="true" RowRel="HouseID">
                    </TCL:GridViewExtender>--%>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        $(function () {
            $("#myffhx").hide();
            $("#div_fitmentid").hide();
            if ($('#myffrmform_bedroom').children('option:selected').val() == "自定义") { $("#myffhx").show(); }

            $('#myffrmform_bedroom').change(function () {
                var p1 = $(this).children('option:selected').val();//这就是selected的值
                if (p1 == "自定义") {
                    $('#myffrmform_bedroom1').val("");
                    $('#myffrmform_bedroom2').val("");
                    $("#myffhx").show();
                }
                else {
                    if (p1.split("-").length == 2 && p1 != "") {
                        $('#myffrmform_bedroom1').val(p1.split("-")[0]);
                        $('#myffrmform_bedroom2').val(p1.split("-")[1]);
                    }
                    else if (p1.split("-").length == 1 && p1 != "") {
                        $('#myffrmform_bedroom1').val("999");
                        $('#myffrmform_bedroom2').val("999");
                    }
                    else {
                        $('#myffrmform_bedroom1').val("");
                        $('#myffrmform_bedroom2').val("");
                    }
                    $("#myffhx").hide();
                }
            })

        });
        //打开房源编辑页面
        function OpenHouseEdit_SF(houseId, houseNo, atype) {
            // 所有参数都是可选项。
            var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
            var options = { width: 660, height: 700, mixable: false }
            $.pdialog.open(url, "housewiew_" + houseId, houseNo + "-修改出售房源", options);
        }
        function OpenArea(name1, name2) {
            var url = "House/AreaV.aspx?tn1=" + name1 + "&tn2=" + name2 + "&tn3=AreaVillage";
            var options = { width: 580, height: 380, mixable: false }
            $.pdialog.open(url, "AreaVillage", "区域商圈小区选择", options);
        }
        $("#myffrmFName").click(function () {
            var str1 = $("#myffrmFName").val();
            var str2 = $("#myffrmFitmentID").val(), str3;
            if (str1 != "" && str1 != null) {
                for (var i = 0; i < str1.split(',').length - 1; i++) {
                    str3 = str1.split(',')[i] + "_" + str2.split(',')[i + 1];
                    $(":checkbox[value='" + str3 + "']").attr("checked", true)
                }
            }
            if ($("#div_fitmentid").is(":hidden")) {
                $("#div_fitmentid").show();
            }
            else {
                $("#div_fitmentid").hide();
            }
        });
        function zxValue(obj) {
            var nr = $(obj).val().split("_");
            if (!$(obj).attr('checked')) {
                $("#myffrmFName").val($("#myffrmFName").val().replace(nr[0] + ",", ""));
                $("#myffrmFitmentID").val($("#myffrmFitmentID").val().replace("," + nr[1], ""));
            }
            else {
                if ($("#myffrmFName").val().indexOf(nr[0]) < 0) {
                    $("#myffrmFName").val($("#myffrmFName").val() + nr[0] + ",");
                    $("#myffrmFitmentID").val("," + nr[1] + $("#myffrmFitmentID").val());
                }
            }
        }
        function CloseRoleCheck(objId) {
            $("#" + objId).hide();
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

<%--<div id="<%=NavTabId %>" style="display: none;">
    <ul>
        <li key="true" href='House/FollowUpEditor.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' width='400' height='410' fresh='true' maxable='false' title="增加跟进">增加跟进</li>
        <li key="true" href='House/AddHouseKey.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' width='600' height='320' fresh='true' maxable='false' title="增加钥匙">增加钥匙</li>
        <li key="true" href="House/HouseTimeLimit.aspx?NavTabId=16_menu0022&doAjax=true" width="590" height="380" target="dialog" maxable='false' title="限时协议">限时协议</li>
        <li key="true" href="House/HouseList.aspx?OperTypes=3&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" id="tuijian" title="推荐房源">推荐房源</li>
        <li key="true" href="House/HouseList.aspx?OperTypes=6&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="取消推荐">取消推荐</li>
        <li key="true" href="House/HouseList.aspx?OperTypes=4&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="收藏房源">收藏房源</li>
        <li key="true" href="House/HouseList.aspx?OperTypes=5&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="取消收藏">取消收藏</li>
        <li key="true" href="House/CheckKeys.aspx?NavTabId=<%=NavTabId %>&doAjax=true" rel='checkkeys' target='dialog' width='400' height='200' fresh='true' maxable='false' title="查看钥匙">查看钥匙</li>
        <li key="true" href="customer/Customerbring.aspx?NavTabId=16_menu0022&doAjax=true" width="590" height="380" target="dialog" maxable='false' title="客户带看">客户带看</li>
        <li key="true" href='House/HouseProEdit.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' width='400' height='470' fresh='true' maxable='false' title="房源质疑">房源质疑</li>

      <li key="true" href="House/HouseList.aspx?OperType=5&NavTabId=<%=NavTabId %>&doAjax=true" rel="<%=NavTabId %>" target="navTab" title="我的收藏">我的收藏</li>
        <li href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=9&ffrmStateID=<%=ffrmStateID.SelectedValue %>' target="navTab" rel="<%=NavTabId %>" title="我分部售房">我分部售房</li>
        <li href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=4&ffrmStateID=<%=ffrmStateID.SelectedValue %>' target="navTab" rel="<%=NavTabId %>" title="我的售房">我的售房</li>
        <li href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=11&ffrmStateID=<%=ffrmStateID.SelectedValue %>' target="navTab" rel="<%=NavTabId %>" title="我的核验房源">我的核验房源</li>
        <li key="true" href="House/HouseList.aspx?OperType=12&NavTabId=<%=NavTabId %>&7=true" target="ajaxTodo" title="导入到二手房">导入到二手房</li>
        <li key="true" href="House/HouseReporSet.aspx?NavTabId=<%=NavTabId %>&doAjax=true" width="400" height="340" target="dialog" rel='reportset' title="上报房源">上报房源</li>
        <li key="true" href="House/HouseVRPlanRequest.aspx?NavTabId=<%=NavTabId %>&doAjax=true" target='dialog' width="420" height="280" fresh='true' maxable='false' title="申请全景拍摄">申请全景拍摄</li>
        <li key="true" href="House/HouseOutSet.aspx?NavTabId=<%=NavTabId %>&doAjax=true" rel='outsetRel' target='dialog' width='380' height='400' fresh='true' maxable='false' title="添加到走漏名单">添加到走漏名单</li>
        <li key="true" href="House/HouseMessage.aspx?NavTabId=<%=NavTabId %>&doAjax=true" width="400" height="200" target="dialog" rel="housemesga" maxable='false' class="no" mask="true" title="房源公告">房源公告</li>
        <li key="true" rel="dlg_page1" href="House/HouseFormTimeLimit.aspx?NavTabId=16_menu0022&doAjax=true" width="590" height="380" target="dialog" maxable='false' title="限时协议">限时协议</li>
        <li key="true" href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=6' target="navTab" rel="<%=NavTabId %>" title="重复房源">重复房源</li>
        <li href='House/HouseList.aspx?NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="所有房源">所有房源</li>
    </ul>
</div>--%>