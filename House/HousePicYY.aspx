<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HousePicYY.aspx.cs" Inherits="HouseMIS.Web.House.HousePicYY" %>

<style type="text/css">

    .edit {
        color: Red;
    }
</style>
<script type="text/javascript" src="js/flaUpfile.js"></script>
<script type="text/javascript">
    $("#HousePicFla<%=HouseID %>:eq(0)").show()

   function ChangeHPic_Del()
    {
        $("#HousePicFla<%=HouseID %>:eq(0)").hide();
    }
       var hpic = "pt1";
       function ChangeHPic(id) {
            $("#HousePicFla<%=HouseID %>:eq(0)").show();
        hpic = id;
       }
      
</script>

<body>
    <div style="height: 560px; overflow: auto;" class="tabs" currentindex="0" eventtype="click">
        <div class="tabsHeader">
            <div class="tabsHeaderContent">
                <ul>
                   <%-- <li onclick="ChangeHPic('pt0')"><a href="javascript:;"><span>户型图</span></a></li>--%>
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
            <%--<div id="pt0" class="pt0">
                <asp:Repeater ID="hx_imgs" runat="server" OnItemDataBound="hx_imgs_ItemDataBound">
                    <ItemTemplate>
                        <div id="div_<%# Eval("LSH")%>">
                            <img class="picimg_hos" imgid="<%# Eval("LSH")%>" src="<%#GetUrl(Eval("PicURL"))%>?image/auto-orient,1/quality,q_90/watermark,image_c2h1aXlpbi96aG9uZ3NoYW5fc2h1aXlpbi5wbmc_eC1vc3MtcHJvY2Vzcz1pbWFnZS9yZXNpemUscF8xMDAvYnJpZ2h0LDAvY29udHJhc3QsMA,t_25,g_center,y_10,x_10"
                                onload="javascript:DrawImage(this,1024,700)" title="右键点击会弹出窗口可保存照片" /><br />
                            <%#Eval("BillCode") %><%#Eval("Name") %> - <%#Eval("em_id") %><%#Eval("em_name") %> - 日期：<%#Eval("exe_date") %><br />
                            <asp:HyperLink ID="dellinks" runat="server" CssClass="edit" NavigateUrl="House/HouseForm.aspx"
                                Target="ajaxTodo">删除该户型图</asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="subBar" id="divHxt" runat="server">
                    <ul>
                        <li>
                            <a class="button" href="House/HousePic.aspx?PicTypeID=1&HouseID=<%=HouseID %>&EmployeeID=<%=EmployeeID %>" target="dialog" mask="true" mixable="false" fresh="true" rel="Sql_HousePic1" title="户型图" width="1024" height="700"><span>上传户型图</span></a>
                        </li>
                    </ul>
                </div>
                <div class="subBar" id="divHxtContent" runat="server" visible="false">
                    <span>请先上传5张房源照片</span>
                </div>
            </div>--%>
            <asp:Repeater ID="h_img_list" runat="server" OnItemDataBound="h_img_list_ItemDataBound">
                <ItemTemplate>
                    <div id="pt<%#Eval("PicTypeID") %>" class="pt<%#Eval("PicTypeID") %>">
                         <span>上传照片最宽1000最高1200</span>
                        <asp:Repeater ID="hil_imgs" runat="server" OnItemDataBound="hil_imgs_ItemDataBound">
                            <ItemTemplate>
                                <div id="div_<%# Eval("LSH")%>">
                                    <img class="picimg_hos" imgid="<%# Eval("LSH")%>" src="<%#GetUrl(Eval("PicURL"))%><%=HouseMIS.Common.ImageHelper.GetUrlwatermarkPara(Current.MyTopOrgA.watermarkPara)%>"
                                        onload="javascript:DrawImage(this,560,500)" title="右键点击会弹出窗口可保存照片" /><br />
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
                            <img class="picimg_hos" imgid="<%# Eval("LSH")%>" src="<%#GetUrl(Eval("PicURL"))%><%=HouseMIS.Common.ImageHelper.GetUrlwatermarkPara(Current.MyTopOrgA.watermarkPara)%>"
                                onload="javascript:DrawImage(this,560,500)" title="右键点击会弹出窗口可保存照片" /><br />
                            类型：<%#Eval("Name") %> - 上传人：<%#Eval("上传人门店") %> - <%#Eval("上传人") %> - 日期：<%#Eval("上传时间") %> -- 删除人：<%#Eval("删除人") %> - 日期：<%#Eval("删除时间") %>
                            <br />
                        </div>
                        <br />
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
     
    </div>
    <div id="HousePicFla<%=HouseID %>" class="HousePicFla<%=HouseID %>"  style="display: none;">
        <div id="divPic" runat="server">
            <script type="text/javascript">
                writeFla("HousePicFla<%=HouseID %>", 1, "HouseUpPicComp", "", "house", "5000", "", "1", "100", "1", "1000", "1200", "<%=HouseID %>", "", "", 1, "<%=HouseID %>,house", 0);
            </script>
        </div>
    </div>
       <div class="tabsFooter">
            <div class="tabsFooterContent">
                
            </div>
        </div>
    <script type="text/javascript">
        $(".picimg_hos").each(function () {
            this.oncontextmenu = function () {
                var url = '/HouseImageDown.aspx?LSH=' + $(this).attr("imgID");
                window.open(url, '_blank', 'height=200,width=300');
            }
        });
    </script>

<script type="text/javascript">
    var EmployeeID =<%=EmployeeID%>
      function HouseUpPicComp(nam, hID_path) {
        var hp = hID_path.split(',');
       $.ajax({
        type: 'POST',
        url: 'House/HousePicExt.aspx',
        data: { Name: nam, PicTypeID: hpic.substring(2), HouseID: hp[0], Path: hp[1], EmployeeID: EmployeeID },
        success: function (result) {
    
            if (result != "2") {
                var url = result.split('|')[0];
                result = result.split('|')[1];
                if (hpic != "pt0") { 
                    $("#" + hpic).html($("#" + hpic).html() + "<div align='left'><img src='" + url + "?sj=" + Math.random() + "' onload='javascript:DrawImage(this,560,500)' /><br>" + result + "</div>");
                }
                else
                {
                    $("#" + hpic).html("<div><iframe scrolling=no src='House/3DImage.aspx?HouseID=" + hp[0] + "' width='600' height='460' frameborder='0' ></iframe><br>" + result + "</div>");
                }
            } else {
                alertMsg.error("您没有上传全景照片的权限！");
            }
        }
    });
}
</script>
</body>