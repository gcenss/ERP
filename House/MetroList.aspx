<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MetroList.aspx.cs" Inherits="HouseMIS.Web.House.MetroList" %>
<style>
    .newlista { height:200px;}
.newlista ul li{ float:left; width:100px; }
</style>
<div class="pageContent">
    
    <div class="newlista" id="frmMetros">
        <ul>
            <asp:repeater runat="server" id="rep1">
             <ItemTemplate>

          <li><input type="checkbox" name="frmMetro$<%#Eval("MetroID") %>" id="frmMetro_<%#Eval("MetroID") %>" value="<%#Eval("Name") %>"><label for="frmMetro_<%#Eval("MetroID") %>"><%#Eval("Name") %></label></li>
                       </ItemTemplate>
            </asp:repeater>
        </ul>
    </div>
    <div class="formBar">
        <ul>
            <li>
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="button" onclick="GetMetro();">
                            提交</button></div>
                </div>
            </li>
            <li>
                <div class="button">
                    <div class="buttonContent">
                        <button type="button" class="close">
                            取消</button></div>
                </div>
            </li>
        </ul>
    </div>
</div>
<script>
    function GetMetro() {
        var checkList = $("#frmMetros :checkbox");
        var str = "";
        checkList.each(function () {
            var $this = $(this);

            if ($this.attr("checked").toString() == "true") {
                str = str + "," + $this.val();
            }
        });
        $.bringBack({ frmMetro: str.substring(1) })
    }
</script>