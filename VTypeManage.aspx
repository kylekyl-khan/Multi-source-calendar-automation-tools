<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="VTypeManage.aspx.cs" Inherits="MemberFlow" MaintainScrollPositionOnPostback="true"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <span id="MainContent_Label2" 
            style="font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; color: rgb(102, 0, 51); font-family: 微軟正黑體; font-size: 20pt; background-color: white;">
        》訪客類別管理<br />
    </span><hr />
    <asp:Label ID="Label5" runat="server" Font-Size="Medium" Text="校區:" 
        ForeColor="Blue"></asp:Label>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" 
        
        RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Selected="True" Value="秀岡">秀岡校區</asp:ListItem>
        <asp:ListItem Value="青山">青山校區</asp:ListItem>
        <asp:ListItem Value="新竹">新竹校區</asp:ListItem>
        <asp:ListItem Value="林口">林口校區</asp:ListItem>
    </asp:RadioButtonList>
    <br />
    <hr />
      
    &nbsp;
    <asp:Label ID="Label3" runat="server" Text="" Visible="False" Font-Bold="True" ForeColor="Red"></asp:Label>
    
&nbsp;&nbsp;&nbsp;
&nbsp; 
    &nbsp;<asp:GridView ID="GridView1" runat="server" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
        DataSourceID="SqlDataSource1" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" 
        Width="99%" ForeColor="Black" GridLines="Vertical" 
        Font-Size="Small" onrowcommand="GridView1_RowCommand" DataKeyNames="sid" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CommandField ShowEditButton="True" />
            <asp:BoundField DataField="Campus" HeaderText="校區" 
                SortExpression="Campus" ReadOnly="True" />
            <asp:BoundField DataField="VTypeName" HeaderText="訪客類別" SortExpression="VTypeName" ReadOnly="True" />
            <asp:BoundField DataField="VTypeID" HeaderText="訪客類別ID" 
                SortExpression="VTypeID" ReadOnly="True" />
            <asp:TemplateField HeaderText="流程名稱" SortExpression="FlowName">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("FlowName") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("FlowName") %>' OnDataBinding="Label10_DataBinding"></asp:Label>
                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Bind("FlowName") %>' CommandName="search"></asp:LinkButton>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="流程代碼" SortExpression="FlowNo">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource3" DataTextField="FlowNo" DataValueField="FlowNo" SelectedValue='<%# Bind("FlowNo") %>'>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" SelectCommand="SELECT FlowName,FlowNo
from Sys_V_Flow
where (Campus=@Campus or FlowNo in ('normal5','normal8'))
order by FlowNo">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="RadioButtonList1" Name="Campus" PropertyName="SelectedValue" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("FlowNo") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="通知人員">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" OnDataBinding="Label11_DataBinding"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label6" runat="server" Font-Size="Medium" Text="無資料"></asp:Label>
        </EmptyDataTemplate>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#988b98" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F2EAED" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#D8BFD8" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" 
        SelectCommand="SELECT a.sid,a.Campus,a.VTypeName,a.VTypeID,b.FlowName,a.FlowNo
from Sys_V_Type a
left join Sys_V_Flow b
on a.FlowNo = b.FlowNo
where a.Campus=@Campus
order by a.VTypeID"

        ProviderName="System.Data.SqlClient" UpdateCommand="update Sys_V_Type set [FlowNo]=@FlowNo
where [sid]=@sid">
        <SelectParameters>
            <asp:ControlParameter ControlID="RadioButtonList1" Name="Campus" PropertyName="SelectedValue" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="FlowNo" Type="String"/>
            <asp:Parameter Name="sid" Type="Int32"/>
        </UpdateParameters>
    </asp:SqlDataSource>
                    <br />
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="sid" DataSourceID="SqlDataSource2" Font-Size="Small" ForeColor="#333333" GridLines="None"  Width="85%">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
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
                                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("Series") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmployeeID" HeaderText="員編" SortExpression="EmployeeID" />
                            <asp:BoundField DataField="Name" HeaderText="姓名" SortExpression="Name" />
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
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
                    <br />
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" ProviderName="System.Data.SqlClient" SelectCommand="">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="RadioButtonList1" Name="Campus" PropertyName="SelectedValue" />
                            <asp:ControlParameter ControlID="GridView1" Name="FlowNo" PropertyName="SelectedValue" Type="String" DefaultValue="Label2" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    </asp:Content>

