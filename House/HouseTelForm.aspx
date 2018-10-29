<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseTelForm.aspx.cs" Inherits="HouseMIS.Web.House.HouseTelForm" %>

<body>
    <form rel="HTFpagerForm" runat="server" id="HTFpagerForm" onsubmit="return dialogSearch(this);" action="House/HouseTelForm.aspx?doType=Add" method="post">
        <div id="HouseTelFrom<%=House.HouseID %>">
            <asp:HiddenField runat="server" ID="HouseID"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="GU_ID"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="LSH"></asp:HiddenField>
            <asp:HiddenField runat="server" ID="room"></asp:HiddenField>
            <div>
                <div>
                    <label>
                        房东姓名:</label>
                    <asp:TextBox ID="frmName" runat="server" title="房东姓名" Width="60"></asp:TextBox>

                    <label title="类型">类型:</label>
                    <asp:DropDownList ID="TelType" runat="server" Width="55px">
                        <asp:ListItem Value="固话">固话</asp:ListItem>
                        <asp:ListItem Value="手机">手机</asp:ListItem>
                        <asp:ListItem Value="家人">家人</asp:ListItem>
                        <asp:ListItem Value="看房">看房</asp:ListItem>
                    </asp:DropDownList>
                    <label title="电话">电话:</label>
                    <asp:TextBox ID="Tel" runat="server" title="号码" MaxLength="12"></asp:TextBox>
                    <button id="hTelAdd" type="submit" onclick="return VilieFrom()" runat="server">提交</button>
                    <button id="LokTelButa" type="button" runat="server">查看电话</button><br />
                    <br />
                    <div style="float: left; width: 110px;">
                        选择连接电话号码：
                    <asp:RadioButtonList ID="cbTel" runat="server"></asp:RadioButtonList>
                    </div>
                    <%-- <div style="float: left; width: 110px; padding-left: 20px;">
                    是否特殊拨打:<asp:checkbox runat="server" id="cb1"></asp:checkbox>
                </div>--%>
                    <br />
                    <span style="line-height: 22px">
                        <%--  <b style="color:Red;">（注意：红色按钮为外地号码拨打）</b>--%>
                以下是业主的N个电话，可以点击拔出，稍候几秒钟，你已绑定的手机会响铃，你应该接通，由隐号系统自动拔通业主的电话，内网会存储录音，<span style="color: red">公司规定严禁向业主索取号码或主动留下自己的号码给业主</span>，违者请乐捐1000元直至停网除名。
                    <%=OldTel %>
                    </span>
                </div>
                <div class="pageContent">
                    <asp:HiddenField runat="server" ID="ValueBack"></asp:HiddenField>
                    <asp:HiddenField runat="server" ID="TextBack"></asp:HiddenField>
                    <input type="hidden" name="status" value="status" />
                    <input type="hidden" name="keywords" value="keywords" />
                    <input type="hidden" name="numPerPage" value="20" />
                    <input type="hidden" name="orderField" value="" />
                    <input type="hidden" name="orderDirection" value="" />
                    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" DataSourceID="ods"
                        DataKeyNames="LSH" AllowPaging="True" CssClass="table" PageSize="20"
                        CellPadding="0" GridLines="None" EnableModelValidation="True"
                        EnableViewState="False" OnRowDataBound="grid_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="TelType" HeaderText="类型" SortExpression="TelType" />
                            <asp:BoundField DataField="landlord_name" HeaderText="房东姓名" ItemStyle-Width="60px" />
                            <asp:BoundField HeaderText="电话" ItemStyle-Width="120px" />
                            <asp:TemplateField HeaderText="拨打" ItemStyle-Width="60px">
                                <ItemTemplate>
                                    <a class="btnTel" href="javascript:CallTel_mobile('<%#Eval("LSH") %>')" title="本地拨打"></a>
                                    <%--    <a class="btnTels" href="javascript:CallTel('<%#Eval("LSH") %>',2)" title="长途拨打"></a>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="短信"  ItemStyle-Width="30px">
                        <ItemTemplate>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="操作"></asp:TemplateField>
                            <asp:BoundField DataField="exe_date" HeaderText="添加日期" ItemStyle-Width="150px" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="FindCount"
                        SelectMethod="FindAll" SortParameterName="orderClause" EnableViewState="false">
                        <SelectParameters>
                            <asp:Parameter Name="whereClause" Type="String" />
                            <asp:Parameter Name="orderClause" Type="String" />
                            <asp:Parameter Name="selects" Type="String" />
                            <asp:Parameter Name="startRowIndex" Type="Int32" />
                            <asp:Parameter Name="maximumRows" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <TCL:GridViewExtender ID="gvExt" runat="server" LayoutH="90" RowTarget="LSH" TargetType="dialog"
                        RowRel="LSH">
                    </TCL:GridViewExtender>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        $(function () {
            if ($("#room").val() != "") {
                $("#div_" + $("#HouseID").val() + " .frmbuild_room:eq(0)").val($("#room").val());
            }
            document.getElementsByName("cbTel").item(0).checked = true;
        })

        function CallTel_mobile(LSH) {
            var New = document.getElementsByName("cbTel");
            var strNew;
            for (var i = 0; i < New.length; i++) {
                if (New.item(i).checked) {
                    strNew = New.item(i).getAttribute("value");
                    break;
                } else {
                    continue;
                }
            }
            if (strNew == null) {
                alertMsg.error("请选择您要使用的号码！");
                return false;
            }
            var models = "0";
            if ($("#cb1").attr("checked")) {
                models = "1";
            }
            $.ajax({
                url: 'House/CallTel_mobile.ashx',
                data: { LSH: LSH, mytel: strNew },
                success: function (result) {
                    if (result.indexOf(",") > 0) {
                        var arry = result.split(',');
                        if (arry[0] == "1") {
                            var options = { width: 400, height: 145, mixable: false };
                            $.pdialog.open("Employee/EditInternetPhone.aspx?EmployeeID=" + arry[1], "20130930Tel", "注册号码", options);
                        }
                    } else {
                        alertMsg.confirm(result);
                    }
                }
            });
        }

        function DelCallTel(LSH) {
            $.ajax({
                url: 'House/CallTel_mobile.ashx',
                data: { LSH: LSH, aTyp: "del" },
                success: function (result) {
                    if (result == "del") {
                        $("tr[rel='" + LSH + "']").remove();
                        alertMsg.correct("操作成功！");
                    }
                }
            });
        }

        function EditCallTel(LSH, landlord_name, TelDe, TelType, HouseID) {
            $("#HouseTelFrom" + HouseID + " #LSH").val(LSH);
            $("#HouseTelFrom" + HouseID + " #frmName").val(landlord_name);
            $("#HouseTelFrom" + HouseID + " #TelType").val(TelType);
            $("#HouseTelFrom" + HouseID + " #Tel").val(TelDe);
        }

        function VilieFrom() {
            if ($("#Tel").val() == "") {
                alertMsg.error("电话不能为空！");
                $("#Tel").focus();
                return false;
            }
            if (!parseInt($("#Tel").val())) {
                alertMsg.error("电话不能有字符！");
                $("#Tel").focus();
                return false;
            }
        }

        function LookTelClick(HouseID) {
            var options = { width: 530, height: 300, mixable: false, dialogId: "TelLookHouseList" + HouseID };
            $.pdialog.reload("House/HouseTelForm.aspx?GU_ID=<%=GU_ID %>&HouseID=" + HouseID + "&doType=LookTel&NavTabId=<%=NavTabId %>&a=1", options)
        }
    </script>
</body>
