<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="EditNotice.aspx.cs" Inherits="edit_flow" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 105px;
        }
        .auto-style4 {
            width: 158px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <span id="MainContent_Label2" 
            style="font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; color: rgb(102, 0, 51); font-family: 微軟正黑體; font-size: 20pt; background-color: white;">
        》通知人員管理<br />
    </span><hr />
    &nbsp;<asp:RadioButtonList ID="RadioButtonList1" runat="server" 
         
        RepeatDirection="Horizontal" Width="447px" AutoPostBack="True" onselectedindexchanged="RadioButtonList1_SelectedIndexChanged"  
        >
        <asp:ListItem Selected="True" Value="秀岡">秀岡校區</asp:ListItem>
        <asp:ListItem Value="青山">青山校區</asp:ListItem>
        <asp:ListItem Value="新竹">新竹校區</asp:ListItem>
        <asp:ListItem Value="林口">林口校區</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="Label2" runat="server" Font-Names="微軟正黑體" Font-Size="Medium" 
        ForeColor="Black" Text="訪客類別："></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" 
    DataSourceID="SqlDataSource1" DataTextField="VTypeName" 
    DataValueField="VTypeID" AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged"
        >
</asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" 
                    
    SelectCommand="SELECT VTypeName,VTypeID
FROM Sys_V_Type
where Campus=@Campus" 
    ProviderName="System.Data.SqlClient">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="RadioButtonList1" Name="Campus" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
    </asp:SqlDataSource>
                <br />
                <asp:Panel ID="Panel4" runat="server">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">+新增通知人員</asp:LinkButton>
                    <br />
                    <asp:Panel ID="Panel2" runat="server">
                        <table cellpadding="5px" style="border-collapse: collapse;border:1px solid gray;" width="50%">
                            <tr>
                                <td class="auto-style1" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">訪客類別</td>
                                <td class="auto-style4" style="border: 1px solid #C0C0C0">
                                    <asp:Label ID="Label6" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">員編</td>
                                <td class="auto-style4" style="border: 1px solid #C0C0C0">
                                    <asp:TextBox ID="TextBox10" runat="server" AutoPostBack="True" OnTextChanged="TextBox10_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">姓名</td>
                                <td class="auto-style4" style="border: 1px solid #C0C0C0">
                                    <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="border: 1px solid #C0C0C0">
                                    <asp:Button ID="Button5" runat="server" Height="37px" OnClick="Button5_Click" Text="新增" Width="70px" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="sid" DataSourceID="SqlDataSource2" Font-Size="Small" ForeColor="#333333" GridLines="None" OnRowUpdating="GridView1_RowUpdating" Width="85%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:BoundField DataField="VTypeName" HeaderText="訪客類別" ReadOnly="True" SortExpression="VTypeName" />
                            <asp:BoundField DataField="EmployeeID" HeaderText="員編" SortExpression="EmployeeID" />
                            <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="#CC00CC" Text="無資料"></asp:Label>
                        </EmptyDataTemplate>
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" DeleteCommand="DELETE FROM [Sys_V_NoticePeople] WHERE [sid] = @sid" ProviderName="System.Data.SqlClient" SelectCommand="SELECT a.VTypeName,a.VTypeID,b.sid,b.EmployeeID,b.Name
FROM Sys_V_Type a
inner join Sys_V_NoticePeople b
on a.Campus = b.Campus
and a.VTypeID = b.VTypeID
where a.Campus=@Campus
and a.VTypeID=@VTypeID">
                        <DeleteParameters>
                            <asp:Parameter Name="sid" Type="Int32" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="RadioButtonList1" Name="Campus" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="DropDownList1" Name="VTypeID" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
    </asp:Panel>
                <br />
                    </asp:Content>

