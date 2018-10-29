<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchoolList.aspx.cs" Inherits="HouseMIS.Web.House.SchoolList" %>
<style>
    .newlista { height:200px;}
.newlista ul li{ float:left; width:100px; }
</style>
<div class="pageContent">
    
    <div class="newlista" id="frmSchools">
        <ul>
            <asp:repeater runat="server" id="rep1">
             <ItemTemplate>

          <li><input type="checkbox" name="frmSchool$<%#Eval("SchoolID") %>" id="frmSchool_<%#Eval("SchoolID") %>" value="<%#Eval("Name") %>"><label for="frmSchool_<%#Eval("SchoolID") %>"><%#Eval("Name") %></label></li>
                       </ItemTemplate>
            </asp:repeater>
        </ul>
    </div>
    <div class="formBar">
        <ul>
            <li>
                <div class="buttonActive">
                    <div class="buttonContent">
                        <button type="button" onclick="GetSchool();">
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
    function GetSchool() {
        var checkList = $("#frmSchools :checkbox");
        var str = "";
        checkList.each(function () {
            var $this = $(this);
           
            if ($this.attr("checked").toString() == "true") {
                str = str + "," + $this.val();
            }
        });
        $.bringBack({ frmSchool: str.substring(1) })
    }
</script>
