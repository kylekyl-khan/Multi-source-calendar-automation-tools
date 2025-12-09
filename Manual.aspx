<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="Manual.aspx.cs" Inherits="HistoryRecord" %>

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
    &nbsp;<br />
&nbsp;<asp:HyperLink ID="HyperLink1" runat="server" Text ='<%$ Resources:Resource,vManual %>' NavigateUrl="~/訪客入校系統操作手冊.pdf" Font-Bold="True" Font-Size="Medium" Target="_blank"></asp:HyperLink>
    <br />
    <br />
    <br />
    <br />
<br />
<br />
</asp:Content>

