<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="AuthorityManager.aspx.cs" Inherits="AuthorityManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="Label31" runat="server" Font-Size="X-Large" ForeColor="#660033" 
        Text="》權限管理"></asp:Label>
<hr />
<asp:LinkButton ID="LinkButton1" runat="server" Font-Size="Large" 
    onclick="LinkButton1_Click">+新增管理者</asp:LinkButton>
<br />
<asp:Panel ID="Panel1" runat="server">
    員編:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    姓名:<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
    <br />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="新增" />
    &nbsp;<asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="取消" />
</asp:Panel>
<br />
    <asp:Label ID="Label33" runat="server" Font-Size="Medium" ForeColor="#660033" 
        Text="管理者名單："></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
    CellPadding="4" DataKeyNames="EmployeeID" DataSourceID="SqlDataSource1" 
    ForeColor="Black" GridLines="Vertical" Width="40%">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowDeleteButton="True" />
            <asp:BoundField DataField="EmployeeID" HeaderText="員編" ReadOnly="True" 
                SortExpression="EmployeeID" />
            <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#988b98" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F2EAED" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
</asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" 
        ProviderName="System.Data.SqlClient"
        
        SelectCommand="SELECT EmployeeID,Name,AdminLevel
from Sys_SetAdmin"
        DeleteCommand="delete from Sys_SetAdmin where EmployeeID=@EmployeeID">
        <DeleteParameters>
            <asp:Parameter Name="EmployeeID" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <br />
</asp:Content>

