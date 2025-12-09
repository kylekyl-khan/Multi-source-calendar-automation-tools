<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="SignList.aspx.cs" Inherits="HistoryRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type = "text/javascript">
    $(document).ready(function () {
        $(function () {
            $("#MainContent_TextBox3").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'yy/mm/dd',
                yearRange: '2018:+0'
            });
            $("#MainContent_TextBox4").datepicker({
                changeMonth: true,
                changeYear: true,
                dateFormat: 'yy/mm/dd',
                yearRange: '2018:+0'
            });
        });
    }); 
    </script>
    <asp:Label ID="SignList" runat="server" Font-Size="X-Large" ForeColor="#660033" 
        Text='<%$ Resources:Resource,ApprovedRequestRecord2 %>'></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label32" runat="server" Font-Size="Medium" ForeColor="#660033" 
        Text='<%$Resources:Resource,SelectVType %>'></asp:Label>
    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True" 
    onselectedindexchanged="DropDownList3_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <asp:Label ID="Label33" runat="server" Font-Size="Medium" ForeColor="#660033" 
        Text='<%$Resources:Resource,intoSchoolBeginTime %>'></asp:Label>
    <asp:TextBox ID="TextBox3" runat="server" Font-Size="Medium" AutoPostBack="True" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
    ~<asp:TextBox ID="TextBox4" runat="server" Font-Size="Medium" AutoPostBack="True" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
    <br />
    <asp:Label ID="Label36" runat="server" Font-Size="Medium" ForeColor="#660033" 
        Text='<%$Resources:Resource,SearchByApplicant %>'></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text='<%$Resources:Resource,Search %>' />
    <hr />
    <asp:GridView ID="GridView1" runat="server" BackColor="White" 
    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
    ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" 
        DataKeyNames="ListNum" DataSourceID="SqlDataSource1" Width="90%" 
        Font-Size="Small">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText='<%$Resources:Resource,No %>' SortExpression="ListNum">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ListNum") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:HyperLink ID="HyperLink2" runat="server" 
                    NavigateUrl='<%# "view.aspx?num=" + Eval("ListNum") %>' 
                    Text='<%# Eval("ListNum") %>'></asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Campus" HeaderText='<%$Resources:Resource,Campus %>' SortExpression="Campus" />
            <asp:BoundField DataField="Name" HeaderText='<%$Resources:Resource,Applicant %>' SortExpression="Name" />
            <asp:BoundField DataField="VTypeName" HeaderText='<%$Resources:Resource,Type %>' SortExpression="VTypeName" />
            <asp:BoundField DataField="startTime" HeaderText='<%$Resources:Resource,intoSchoolBeginTime %>' SortExpression="startTime" />
            <asp:BoundField DataField="ListStatus" HeaderText='<%$Resources:Resource,Status %>' 
            SortExpression="ListStatus" />
            <asp:BoundField DataField="Apply_Datetime" HeaderText='<%$Resources:Resource,ApplicationDate %>' 
            SortExpression="Apply_Datetime" />
        </Columns>
        <EmptyDataTemplate>
            <asp:Label ID="Label3" runat="server" Font-Names="微軟正黑體" Font-Size="Medium" 
                ForeColor="Red" Text='<%$Resources:Resource,NoDataFound %>' Font-Bold="True"></asp:Label>
        </EmptyDataTemplate>
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
        
        
        SelectCommand="">
    </asp:SqlDataSource>
</asp:Content>

