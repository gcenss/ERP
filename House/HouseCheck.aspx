<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseCheck.aspx.cs" Inherits="HouseMIS.Web.House.HouseCheck" %>

<script language="javascript" type="text/javascript">
    //打开房源编辑页面
    function OpenHouseEditC(houseId, houseNo, atype) {
        // 所有参数都是可选项。
        var url = 'House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID=' + houseId + '&EditType=Edit';
        var options = { width: 660, height: 668, mixable: false }
        $.pdialog.open(url, "housewiew2_" + houseId, houseNo + "-修改房源", options);

    }
    function ReloadHouseCheck(B) {
        $.ajax({
            url: 'House/HouseAPI.ashx',
            data: { HouseID: B },
            success: function (result) {

                $("#tbodhc").html(result);
                $("#thedhc tr th:eq(0)").width($("#tbodhc tr td:eq(0)").width());
                $("#thedhc tr th:eq(1)").width($("#tbodhc tr td:eq(1)").width());
                $("#thedhc tr th:eq(2)").width($("#tbodhc tr td:eq(2)").width());
                $("#thedhc tr th:eq(3)").width($("#tbodhc tr td:eq(3)").width());
            }
        });
        var ajaxbg = $("#background,#progressBar");
        ajaxbg.hide();
    }
</script>

<body>
    <form rel="pagerForm" runat="server" name="pagerForm" class="houseCheck" id="pagerForm" onsubmit="return navTabSearch(this);" action="House/HouseCheck.aspx" method="post">
        <div id="houseCheckdiv" style="position: relative">
            <%--<div style="position:absolute;top:7px; left:49px; z-index:9999">

   <asp:textbox id="myffrmloupan" runat="server" style=" display:none;"></asp:textbox>
                    <asp:textbox id="myffrmHouseDicName" runat="server" width="110px"></asp:textbox>
<script language="javascript">
                          //绑定Suggest事件
                          IntSuggest("myffrmHouseDicName", "myffrmloupan", "/Ajax/SearchHouseDic.ashx?kw=");
            </script>
   </div> --%>
            <div class="pageHeader">
                <div class="searchBar" id="HouseCheckSearchBar">
                    <table class="searchContent">
                        <tr>
                            <td class="dateRange">
                                <label title="类型">类型：</label>

                                <asp:DropDownList ID="ffrmaType" CssClass="ffrmaType" runat="server"
                                    Width="73px">
                                    <asp:ListItem></asp:ListItem>
                                    <asp:ListItem Value="0">出售</asp:ListItem>
                                    <asp:ListItem Value="1">出租</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label>状态：</label>
                                <asp:DropDownList ID="ffrmStateID" runat="server"></asp:DropDownList>
                            </td>
                            <td class="dateRange">
                                <label title="楼盘/地址">楼盘：</label>
                                <asp:TextBox ID="myffrmEstateOrAddress" runat="server" title="楼盘/地址" Width="100px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label>楼层：</label>
                                <asp:TextBox ID="myffrmbuild_floor1" runat="server" Style="width: 40px"></asp:TextBox>
                                -

                            <asp:TextBox ID="myffrmbuild_floor2" runat="server" Style="width: 40px"></asp:TextBox>
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
                            <td class="dateRange">
                                <label>栋号：</label>
                                <asp:TextBox ID="myffrmbuild_id" runat="server" Width="40px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table class="searchContent">
                        <tr>
                            <%--<td class="dateRange">
                                <label>分组：</label>
                                <asp:DropDownList ID="myffrmSubGroupID" runat="server"></asp:DropDownList>
                            </td>--%>
                            <td class="dateRange">
                                <label title="房源编号">编号：</label>
                                <asp:TextBox ID="myffrmShi_id" runat="server" title="房源编号" Width="100px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label title="房东电话/联系电话">电话：</label>
                                <asp:TextBox ID="myffrmlandlord_tel2" runat="server" title="房东电话/联系电话" Width="96px"></asp:TextBox>
                            </td>
                            <td class="dateRange">
                                <label title="登记日期">日期：</label>
                                <asp:TextBox ID="myffrmexe_date1" runat="server" CssClass="date" Width="66px"></asp:TextBox>
                                -

                            <asp:TextBox ID="myffrmexe_date2" runat="server" CssClass="date" Width="66px"></asp:TextBox>
                            </td>
                            <td>
                                <input type="checkbox" value="on" onclick="SetCheckValue(this, 'on', 'myffrmIsPrivate')" />私盘

                            <input type="hidden" id="myffrmIsPrivate" name="myffrmIsPrivate" value="" />
                            </td>
                            <td <%=sty %>>
                                <input type="checkbox" value="on" onclick="SetCheckValue(this, 'on', 'mysfrmAssessStateErrors')" />评价不匹配

                            <input type="hidden" id="mysfrmAssessStateErrors" name="mysfrmAssessStateErrors" value="" />
                            </td>
                            <td class="dateRange">
                                <label title="备注">备注：</label>
                                <asp:TextBox ID="myffrmnote" runat="server" title="备注" Width="100px"></asp:TextBox>
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
                                        <li><a class="button" href="House/HouseListFrom.aspx?NavTabId=<%=NavTabId %>&type=hc" target="dialog" mask="true" mixable="false" fresh="true" rel='dlg_House2' title="高级检索" width="580" height="545"><span>高级检索</span></a></li>
                                    </ul>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <script type="text/javascript">
                    $("#HouseCheckSearchBar").find("input" || "select").each(function () {
                        if ($(this).attr("value"))
                            $(this).attr("value", "");
                        else if ($(this).attr("text"))
                            $(this).attr("text", "");

                    });
                </script>
            </div>
            <style>
                .dateRange label {
                    width: auto;
                }

                .dateRange input {
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

                #houseCheckdiv .grid .gridThead TH {
                    border-right-width: 1px;
                    border-bottom-width: 1px;
                }

                #houseCheckdiv .grid .gridTbody TD {
                    border-right-width: 1px;
                    border-bottom-width: 1px;
                    border-right-color: #C3C3C3;
                    border-bottom-color: #C3C3C3;
                    padding-bottom: 3px;
                }

                #houseCheckdiv .grid Table td {
                    border-left-color: #000000;
                    border-top-color: #000000;
                }
            </style>
            <div class="pageContent" style="padding: 0px 2px 2px 2px">
                <link href="themes/css/other.css" rel="stylesheet" type="text/css" />
                <div class='panelBar'>
                    <ul class='toolBar'>
                        <li class='line'>line</li>
                        <li><a class='icon' id="all" href='House/HouseCheck.aspx?NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="所有房源"><span>所有</span></a></li>
                        <li><a class='icon' id="sell" href='House/HouseCheck.aspx?OperType=0&NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="出售房源"><span>出售</span></a></li>
                        <li><a class='icon' id="zu" href='House/HouseCheck.aspx?OperType=1&NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="出租房源"><span>出租</span></a></li>
                        <li><a class="delete" href="House/HouseCheck.aspx?NavTabId=<%=NavTabId %>&doAjax=true&doType=del" rel="hcids" target="selectedTodo" title="确定要删除吗?"><span>删除</span></a></li>
                        <li class='line'>line</li>
                        <li><a class='add' href='House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&todo=1' target='dialog' rel='dlg_page2' width='636' height='668' fresh='true' maxable='false'><span>增加房源</span></a></li>
                        <li><a class='edit' href='House/HouseForm.aspx?NavTabId=<%=NavTabId %>&doAjax=true&HouseID={HouseID}&EditType=Edit' target='dialog' rel='dlg_page2' width='660' height='668' fresh='true' maxable='false'><span>修改房源</span></a></li>
                        <li class='line'>line</li>
                        <asp:Literal runat="server" ID="ViewSet_gv"></asp:Literal>
                    </ul>
                </div>
                <div class='panelBar'>
                    <ul class='toolBar'>
                        <li class='line'>line</li>
                        <%=GetBottom()%>
                    </ul>
                </div>
                <%--<div id="NeedAssess" runat="server" class='panelBar'>
                    <ul class='toolBar'>
                        <li class='line'>line</li>
                        <%=GetBottoms()%>
                    </ul>
                </div>--%>

                <input type="hidden" name="NavTabId" value="<%=NavTabId %>" />
                <input type="hidden" name="status" value="status" />
                <input type="hidden" name="keywords" value="keywords" />
                <input type="hidden" name="numPerPage" value="20" />
                <input type="hidden" name="orderField" value="" />
                <input type="hidden" name="orderDirection" value="" />
                <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="HouseID,shi_id,SeeHouseType,aType" DataSourceID="ods" AllowPaging="True" CssClass="table" PageSize="20" CellPadding="0" GridLines="None" EnableViewState="False">
                    <Columns>
                        <asp:TemplateField HeaderText="选择">
                            <HeaderTemplate>
                                <input type="checkbox" group="hcids" class="checkboxCtrl" title="全部选择/取消选择">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <input name="hcids" type="checkbox" value="<%#Eval("HouseID")%>" />
                            </ItemTemplate>
                            <ItemStyle Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <%#Eval("ImageInfor")%>
                            </ItemTemplate>
                            <ItemStyle Width="50" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="update_date" HeaderText="更新日期" DataFormatString="{0:yyyy-MM-dd}" SortExpression="update_date">
                            <ItemStyle Width="80" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="分类">
                            <ItemTemplate>
                                <span style="color: <%#GetAtype(Eval("AtypeName")) %>"><%#Eval("AtypeName")%></span>
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                            <ItemStyle Width="40px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="shi_id" HeaderText="房源编号" SortExpression="shi_id">
                            <ItemStyle Width="80" />
                        </asp:BoundField>
                        <asp:BoundField DataField="build_id" HeaderText="楼栋" SortExpression="build_id">
                            <ItemStyle Width="120" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HouseDicName" HeaderText="楼盘" SortExpression="HouseDicName">
                            <ItemStyle Width="120" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HouseDicAddress" HeaderText="楼盘地址">
                            <ItemStyle Width="140" />
                        </asp:BoundField>
                        <asp:BoundField DataField="HouseType" HeaderText="户型" SortExpression="form_foreroom" ItemStyle-Width="40px" />
                        <asp:BoundField DataField="FloorAll" HeaderText="楼层" SortExpression="build_floor" ItemStyle-Width="40px" />
                        <asp:BoundField DataField="Renovation" HeaderText="装修" SortExpression="FitmentID" ItemStyle-Width="40px" />
                        <asp:BoundField DataField="Orientation" HeaderText="朝向" SortExpression="FacetoID" ItemStyle-Width="40px" />
                        <%--<asp:TemplateField HeaderText="评价状态">
                            <ItemTemplate>
                                <span<%#GetAStyle(Eval("AssessStateID"),Eval("StateID")) %>><%#Eval("AssessStateName") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="状态" ItemStyle-Width="45px">
                            <ItemTemplate>
                                <span id="check_<%#Eval("HouseID") %>"><%#Eval("SeeHouseType") %></span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Year" HeaderText="年代" SortExpression="YearID" ItemStyle-Width="40px" />
                        <asp:BoundField DataField="Build_area" HeaderText="面积(㎡)" SortExpression="Build_area" />
                        <asp:BoundField DataField="sum_price" HeaderText="总价(万)" SortExpression="sum_price" />
                        <asp:BoundField DataField="Rent_Price" HeaderText="租金(元)" SortExpression="Rent_Price" />
                        <asp:BoundField DataField="Prices" HeaderText="单价(元)" SortExpression="sum_price" />
                        <asp:BoundField DataField="FollowUp_Date" HeaderText="最后跟进日期" SortExpression="FollowUp_Date" DataFormatString="{0:d}">
                            <ItemStyle Width="90" />
                        </asp:BoundField>
                        <asp:BoundField DataField="note" HeaderText="备注">
                            <ItemStyle Width="100px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="ods" TypeName="HouseMIS.EntityUtils.H_houseinfor" runat="server" EnablePaging="True" SelectCountMethod="FindCount" SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                    <SelectParameters>
                        <asp:Parameter Name="whereClause" Type="String" />
                        <asp:Parameter Name="orderClause" Type="String" DefaultValue="HouseID desc" />
                        <asp:Parameter Name="selects" Type="String" />
                        <asp:Parameter Name="startRowIndex" Type="Int32" />
                        <asp:Parameter Name="maximumRows" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="300" RowTarget="HouseID" RowRel="HouseID">
                </TCL:GridViewExtender>
                <table class="table" width="100%" layouth="780">
                    <thead id="thedhc">
                        <tr>
                            <th width="100">
                            跟进类型        
                            <th width="100">跟进类型</th>
                            <th width="250">跟进内容</th>
                            <th width="50">操作员</th>
                            <th width="50">操作时间</th>
                        </tr>
                    </thead>
                    <tbody id="tbodhc">
                    </tbody>
                </table>
            </div>
        </div>
    </form>
    <div id="<%=NavTabId %>" style="display: none;">
        <ul>
            <li key="true" href='House/FollowUpEditor.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' width='367' height='404' fresh='true' maxable='false' title="增加跟进">增加跟进</li>
            <li key="true" href='House/AddHouseKey.aspx?NavTabId=<%=NavTabId %>&doAjax=true' target='dialog' width='600' height='320' fresh='true' maxable='false' title="增加钥匙">增加钥匙</li>
            <li key="true" href="House/HouseList.aspx?OperTypes=3&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" id="tuijian" title="推荐房源">推荐房源</li>
            <li key="true" href="House/HouseList.aspx?OperTypes=4&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="收藏房源">收藏房源</li>
            <li key="true" href="House/HouseList.aspx?OperTypes=5&NavTabId=<%=NavTabId %>&doAjax=true" target="ajaxTodo" title="取消收藏">取消收藏</li>
            <li key="true" href="House/CheckKeys.aspx?NavTabId=<%=NavTabId %>&doAjax=true" rel='checkkeys' target='dialog' width='400' height='200' fresh='true' maxable='false' title="查看钥匙">查看钥匙</li>
            <%--<li key="true" href="House/HouseReporSet.aspx?NavTabId=<%=NavTabId %>&doAjax=true" width="400" height="370" target="dialog" rel='reportset' title="上报房源">上报房源</li>
            <li key="true" href="House/HouseOutSet.aspx?NavTabId=<%=NavTabId %>&doAjax=true" rel='outsetRel' target='dialog' width='380' height='400' fresh='true' maxable='false' title="添加到走漏名单">添加到走漏名单</li>
            <li key="true" href="House/HouseMessageList.aspx?NavTabId=<%=NavTabId %>&doAjax=true" width="400" height="300" target="dialog" maxable='false' mask="true" title="短信通知">短信通知</li>
            <li key="true" href="House/HouseMessage.aspx?NavTabId=<%=NavTabId %>&doAjax=true" width="400" height="200" target="dialog" maxable='false' mask="true" rel="housemesg" title="房源公告">房源公告</li>--%>

            <%--<li target="ajaxTodo" title="发布到网站">发布到网站</li>--%>
            <li href='House/HouseList.aspx?NavTabId=<%=NavTabId %>' target="navTab" rel="<%=NavTabId %>" title="所有房源">所有房源</li>
            <li href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=2' target="navTab" rel="<%=NavTabId %>" title="我分部房源">我分部房源</li>
            <li href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=4' target="navTab" rel="<%=NavTabId %>" title="我的房源">我的房源</li>
            <li href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=5' target="navTab" rel="<%=NavTabId %>" title="我收藏房源">我收藏房源</li>
            <li key="true" href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=10' target="navTab" rel="<%=NavTabId %>" title="相同电话房源">相同电话房源</li>
            <li key="true" href='House/HouseList.aspx?NavTabId=<%=NavTabId %>&OperType=6' target="navTab" rel="<%=NavTabId %>" title="重复房源">重复房源</li>
        </ul>
    </div>
</body>
