<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" MasterPageFile="~/Site.master"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="MainContent">
    <style type="text/css">      
            
        .style1
        {
            height: 38px;
        }
        .style2
        {
            height: 23px;
        }
            
        .style3
        {
            height: 23px;
            width: 77px;
        }
            
    </style>

    <div style="text-align: center; vertical-align: middle;">
        <br />
        <table align="center" style="width: 500px;">
            <tr>
                <td align="right" class="style2">
                </td>
                <td align="left" class="style2">
                </td>
                <td class="style3">
                </td>
            </tr>
            <tr>
                <td align="right" class="style2">
                    &nbsp;</td>
                <td align="left" class="style2">
                    &nbsp;</td>
                <td class="style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="right" class="style1">
        <asp:Label ID="Label1" runat="server" Text="Domain Name:" Font-Size="Small" 
                        ForeColor="#993399"></asp:Label>
                </td>
                <td align="left" colspan="2" class="style1">
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
            <asp:ListItem>kcbs</asp:ListItem>
        </asp:DropDownList>
        <asp:TextBox ID="txtDomainName" runat="server" Enabled="False" Width="80px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
        <asp:Label ID="Label3" runat="server" Text="UserName (帳號):" Font-Size="Small" 
                        ForeColor="#993399"></asp:Label>
                </td>
                <td align="left" colspan="2" class="style1">
        <asp:TextBox ID="txtUserName" runat="server" Width="150px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style1">
        <asp:Label ID="Label2" runat="server" Text="PassWord (密碼):" Font-Size="Small" 
                        ForeColor="#993399"></asp:Label>
                </td>
                <td align="left" colspan="2" class="style1">
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="150px"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="(帳號、密碼為開機帳號密碼！)" Font-Size="Small" 
                        ForeColor="#666666"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" class="style1">
                    </td>
                <td align="left" class="style1" colspan="2">
        <asp:Button ID="btnLogon" runat="server" OnClick="btnLogon_Click" Width="100" Text="登入" />
                    </td>
            </tr>
            <tr>
                <td align="center" class="style2" colspan="3">
        <asp:Label ID="lblError" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
                    </td>
            </tr>
            </table>
        <br />
        </div>
</asp:Content>