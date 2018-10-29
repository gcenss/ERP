<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseProEdit.aspx.cs" Inherits="HouseMIS.Web.House.HouseProEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <div class="pageContent">
        <form id="Form1" runat="server" action="House/HouseProEdit.aspx" class="pageForm required-validate" onsubmit="return validateCallback(this, dialogAjaxDone)">
            <div class="pageFormContent" layouth="65">
                <input type="hidden" id="houseid" name="houseid" runat="server" />
                <input type="hidden" id="employeeID_Finish" name="employeeID_Finish" runat="server" />
                <input type="hidden" id="employeeID_Pro" name="employeeID_Pro" runat="server" />
                <fieldset>
                    <legend>房源质疑</legend>
                    <table class="cj">
                        <tr>
                            <td></td>
                            <td width="100px">房源编号:</td>
                            <td>
                                <asp:TextBox ID="frmshi_id" name="frmshi_id" runat="server" Enabled="false" Width="80%" Style="float: left;"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td width="100px">房源核验</td>
                            <td>
                                <asp:CheckBox ID="frmh_Check" runat="server" Text="质疑" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td width="100px">户型图:</td>
                            <td>
                                <asp:CheckBox ID="frmh_hxt" runat="server" Text="质疑" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td width="100px">室内外图:</td>
                            <td>
                                <asp:CheckBox ID="frmh_pic" runat="server" Text="质疑" />
                            </td>
                        </tr>
                        <tr id="tr1" runat="server">
                            <td></td>
                            <td width="100px">处理人:</td>
                            <td>
                                <asp:TextBox ID="frmemployeeID_FinishName" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="tr2" runat="server">
                            <td></td>
                            <td width="100px">处理意见:</td>
                            <td>
                                <asp:TextBox ID="frmremark" runat="server" TextMode="MultiLine" Height="200px" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div class="formBar">
                <ul>
                    <li>
                        <div class="buttonActive">
                            <div class="buttonContent">
                                <button type="submit">保存</button>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="button">
                            <div class="buttonContent">
                                <button type="button" class="close">取消</button>
                            </div>
                        </div>
                    </li>
                </ul>
            </div>
        </form>
    </div>
</body>
</html>
