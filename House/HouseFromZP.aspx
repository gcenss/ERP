<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseFromZP.aspx.cs" Inherits="HouseMIS.Web.House.HouseFromZP" %>

<body>
    <div style="height: 560px; overflow: auto;" class="tabs" currentindex="0" eventtype="click">
        <input type="hidden" id="PicType" runat="server" value="1" />
        <div class="tabsHeader">
            <div class="tabsHeaderContent">
                <ul>
                    <%--  <li onclick="ChangeHPic('pt0')"><a href="javascript:;"><span>户型图</span></a></li>--%>
                    <asp:Repeater ID="h_zp_list" runat="server">
                        <ItemTemplate>
                            <li onclick="ChangeHPic('pt<%#Eval("PicTypeID") %>')"><a href="javascript:;"><span>
                                <%#Eval("Name") %></span></a></li>
                        </ItemTemplate>
                    </asp:Repeater>
                    <li id="picDel" runat="server" visible="false" onclick="ChangeHPic_Del()"><a href="javascript:;"><span>已删除图片</span></a></li>
                </ul>
            </div>
        </div>

        <div class="tabsContent" layouth="145">
            <asp:Repeater ID="h_img_list" runat="server" OnItemDataBound="h_img_list_ItemDataBound">
                <ItemTemplate>
                    <div id="pt<%#Eval("PicTypeID") %>" class="pt<%#Eval("PicTypeID") %>">
                        <span>上传照片最宽1000最高1200</span>
                        <asp:Repeater ID="hil_imgs" runat="server" OnItemDataBound="hil_imgs_ItemDataBound">
                            <ItemTemplate>
                                <div id="div_<%# Eval("LSH")%>">
                                    <img class="picimg_hos" imgid="<%# Eval("LSH")%>" src="<%#GetUrl(Eval("PicURL"))%><%=HouseMIS.Common.ImageHelper.GetUrlwatermarkPara(Current.MyTopOrgA.watermarkPara)%>"
                                        onload="javascript:DrawImage(this,600,500)" width="600" title="右键点击会弹出窗口可保存照片" />
                                    <br />
                                    <%#Eval("BillCode") %><%#Eval("Name") %> - <%#Eval("em_id") %><%#Eval("em_name") %> - 日期：<%#Eval("exe_date") %><br />
                                    <asp:HyperLink ID="dellink" runat="server" CssClass="edit" NavigateUrl="House/HouseForm.aspx"
                                        Target="ajaxTodo">删除该房源照片</asp:HyperLink>
                                </div>
                                <br />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div>
                <asp:Repeater ID="rptDel" runat="server" Visible="false">
                    <ItemTemplate>
                        <div id="div_<%# Eval("LSH")%>">
                            <img class="picimg_Del" imgid="<%# Eval("LSH")%>" src="<%#GetUrl(Eval("PicURL"))%><%=HouseMIS.Common.ImageHelper.GetUrlwatermarkPara(Current.MyTopOrgA.watermarkPara)%>"
                                onload="javascript:DrawImage(this,600,500)" width="600" title="右键点击会弹出窗口可保存照片" /><br />
                            类型：<%#Eval("Name") %> - 上传人：<%#Eval("上传人门店") %> - <%#Eval("上传人") %> - 日期：<%#Eval("上传时间") %> -- 删除人：<%#Eval("删除人") %> - 日期：<%#Eval("删除时间") %>
                            <br />
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="tabsFooter">
            <div class="tabsFooterContent">
            </div>
        </div>
    </div>

    <div id="HousePicFla<%=HouseID %>" class="HousePicFla<%=HouseID %>" style="display: none;">
        <div id="divPic" runat="server">
            <script type="text/javascript">
                writeFla("HousePicFla<%=HouseID %>", 1, "HouseUpPicComp", "", "house", "5000", "", "1", "100", "1", "1000", "1200", "<%=HouseID %>", "", "", 1, "<%=HouseID %>,house", 0);
            </script>
        </div>
    </div>

    <script type="text/javascript">
        function ChangeHPic_Del() {
            $("#div_<%=HouseID %> .HousePicFla<%=HouseID %>:eq(0)").hide();
        }

        //调用：<img src="图片" onload="javascript:DrawImage(this,100,100)">
        //图片按比例缩放
        function DrawImage(ImgD, w, h) {
            var image = new Image();
            var iwidth = w; //定义允许图片宽度
            var iheight = h; //定义允许图片高度
            var flag = false;
            image.src = ImgD.src;
            if (image.width > 0 && image.height > 0) {
                flag = true;
                if (image.width / image.height >= iwidth / iheight) {
                    if (image.width > iwidth) {
                        ImgD.width = iwidth;
                        ImgD.height = (image.height * iwidth) / image.width;
                    } else {
                        ImgD.width = image.width;
                        ImgD.height = image.height;
                    }
                    //ImgD.alt = image.width + "×" + image.height;
                }
                else {
                    if (image.height > iheight) {
                        ImgD.height = iheight;
                        ImgD.width = (image.width * iheight) / image.height;
                    } else {
                        ImgD.width = image.width;
                        ImgD.height = image.height;
                    }
                    //ImgD.alt = image.width + "×" + image.height;
                }
            }
        }

        $(function () {
            if ($("#PicType").val() == "1")
                $("#div_<%=HouseID %> .HousePicFla<%=HouseID %>:eq(0)").show();
            else if ($("#PicType").val() == "2") {
                $("#div_<%=HouseID %> .HousePicFla<%=HouseID %>:eq(0)").hide();
            }
            else {
                $("#div_<%=HouseID %> .HousePicFla<%=HouseID %>:eq(0)").hide();
                $("#pt1").html(" <b>请先上传5张房源照片!</b>");
            }

            $(".picimg_hos").each(function () {
                this.oncontextmenu = function () {
                    var url = '/HouseImageDown.aspx?LSH=' + $(this).attr("imgID");
                    window.open(url, '_blank', 'height=200,width=300');
                }
            });
            $(".picimg_Del").each(function () {
                this.oncontextmenu = function () {
                    var url = '/HouseImageDown.aspx?Type=del&LSH=' + $(this).attr("imgID");
                    window.open(url, '_blank', 'height=200,width=300');
                }
            });
        })
    </script>
</body>
