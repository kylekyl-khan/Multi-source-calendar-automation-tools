<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="EditFlow.aspx.cs" Inherits="edit_flow" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 105px;
        }
        .auto-style2 {
            width: 105px;
            height: 32px;
        }
        .auto-style3 {
            height: 32px;
        }
        .auto-style4 {
            width: 158px;
        }
        .auto-style5 {
            height: 32px;
            width: 158px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <span id="MainContent_Label2" 
            style="font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; color: rgb(102, 0, 51); font-family: 微軟正黑體; font-size: 20pt; background-color: white;">
        》特殊流程管理<br />
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
        ForeColor="Black" Text="流程："></asp:Label>
    <asp:DropDownList ID="DropDownList1" runat="server" 
    DataSourceID="SqlDataSource1" DataTextField="FlowName" 
    DataValueField="FlowNo" AutoPostBack="True" onselectedindexchanged="DropDownList1_SelectedIndexChanged"
        >
</asp:DropDownList>
                <br />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" 
                    
    SelectCommand="SELECT FlowName,FlowNo
FROM Sys_V_Flow
where Campus=@Campus" 
    ProviderName="System.Data.SqlClient">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="RadioButtonList1" Name="Campus" 
                                PropertyName="SelectedValue" />
                        </SelectParameters>
    </asp:SqlDataSource>
                <asp:Panel ID="Panel4" runat="server">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">+新增簽認順序</asp:LinkButton>
                    <br />
                    <asp:Panel ID="Panel2" runat="server">
                        <table cellpadding="5px" style="border-collapse: collapse;border:1px solid gray;" width="50%">
                            <tr>
                                <td class="auto-style1" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">流程名稱</td>
                                <td class="auto-style4" style="border: 1px solid #C0C0C0">
                                    <asp:Label ID="Label6" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style2" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">流程代碼</td>
                                <td class="auto-style5" style="border: 1px solid #C0C0C0">
                                    <asp:Label ID="Label5" runat="server"></asp:Label>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="auto-style1" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">簽認順序</td>
                                <td class="auto-style4" style="border: 1px solid #C0C0C0">
                                    <asp:DropDownList ID="DropDownList7" runat="server">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                    </asp:DropDownList>
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
                            <asp:CommandField ShowEditButton="True" />
                            <asp:CommandField ShowDeleteButton="True" />
                            <asp:BoundField DataField="FlowNo" HeaderText="流程代碼" ReadOnly="True" SortExpression="FlowNo" />
                            <asp:TemplateField HeaderText="簽認順序" SortExpression="Series">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList9" runat="server" SelectedValue='<%# Bind("Series") %>'>
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>6</asp:ListItem>
                                        <asp:ListItem>7</asp:ListItem>
                                        <asp:ListItem>8</asp:ListItem>
                                        <asp:ListItem>9</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label8" runat="server" Text='<%# Eval("sid") %>' Visible="False"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Series") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmployeeID" HeaderText="員編" SortExpression="EmployeeID" />
                            <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Size="Larger" ForeColor="#CC00CC" Text="請於上方新增簽認順序"></asp:Label>
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
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" DeleteCommand="DELETE FROM [Sys_V_Flow2] WHERE [sid] = @sid" ProviderName="System.Data.SqlClient" SelectCommand="SELECT a.FlowName,b.sid,a.FlowNo,b.Series,b.EmployeeID,b.Name 
FROM Sys_V_Flow a
inner join Sys_V_Flow2 b
on a.FlowNo = b.FlowNo
where a.Campus=@Campus
and a.FlowNo=@FlowNo
ORDER BY b.Series" UpdateCommand="UPDATE [Sys_V_Flow2] SET [Series] = @Series,[EmployeeID] = @EmployeeID, [Name] = @Name WHERE [sid] = @sid">
                        <DeleteParameters>
                            <asp:Parameter Name="sid" Type="Int32" />
                        </DeleteParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="RadioButtonList1" Name="Campus" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="DropDownList1" Name="FlowNo" PropertyName="SelectedValue" Type="String" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="Series" Type="Int32" />
                            <asp:Parameter Name="EmployeeID" Type="String" />
                            <asp:Parameter Name="Name" Type="String" />
                            <asp:Parameter Name="sid" Type="Int32" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
    </asp:Panel>
                <br />
    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">+新增流程</asp:LinkButton>
    <asp:Panel ID="Panel3" runat="server">
        <table style="border-collapse: collapse;border:1px solid gray;" width="50%" cellpadding="5px">
            <tr>
                <td class="auto-style1" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">校區</td>
                <td style="border: 1px solid #C0C0C0">
                    <asp:DropDownList ID="DropDownList8" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList8_SelectedIndexChanged">
                        <asp:ListItem Value="秀岡">秀岡校區</asp:ListItem>
                        <asp:ListItem Value="青山">青山校區</asp:ListItem>
                        <asp:ListItem Value="新竹">新竹校區</asp:ListItem>
                        <asp:ListItem Value="林口">林口校區</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style1" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">流程名稱</td>
                <td style="border: 1px solid #C0C0C0">
                    <asp:TextBox ID="TextBox12" runat="server" Width="149px">特殊流程_</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" style="border: 1px solid #C0C0C0; background-color: #DBDBEE;">流程代碼</td>
                <td class="auto-style3" style="border: 1px solid #C0C0C0">
                    <asp:TextBox ID="TextBox13" runat="server" MaxLength="5" Width="62px"></asp:TextBox>
                    <br />
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">查看已用過的代碼</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="border: 1px solid #C0C0C0">
                    <asp:Button ID="Button6" runat="server" Text="新增" Height="37px" Width="70px" OnClick="Button6_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
                <br />
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black">
        <Columns>
            <asp:BoundField DataField="Campus" HeaderText="校區" SortExpression="Campus" />
            <asp:BoundField DataField="FlowName" HeaderText="流程名稱" SortExpression="FlowName" />
            <asp:BoundField DataField="FlowNo" HeaderText="流程代碼" SortExpression="FlowNo" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" />
        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <RowStyle BackColor="White" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F1F1F1" />
        <SortedAscendingHeaderStyle BackColor="#808080" />
        <SortedDescendingCellStyle BackColor="#CAC9C9" />
        <SortedDescendingHeaderStyle BackColor="#383838" />
    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" 
                    
    SelectCommand="SELECT Campus,FlowName,FlowNo 
FROM Sys_V_Flow
ORDER BY Campus,FlowNo" 
    ProviderName="System.Data.SqlClient">
    </asp:SqlDataSource>
                <br />
                    </asp:Content>

