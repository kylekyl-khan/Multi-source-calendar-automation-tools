<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyITSInfo.aspx.cs" Inherits="detailPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>康橋國際學校-訪客入校系統</title>
    <script src="Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.core.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.position.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.widget.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery-ui.custom.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.datepicker.js" type="text/javascript"></script>
    <script src="Scripts/ui/jquery.ui.menu.js" type="text/javascript"></script>
    <script src="Scripts/jquery.blockUI.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui-1.10.4.custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .table_big {
            /*border:1px solid #9f9f9f;*/
            border: none;
            border-radius: 3px;
            border-collapse: collapse;
            width: 80%;
            font-family: 微軟正黑體;
            font-size: small;
            margin-bottom: 10px;
        }

        .left_big {
            border-top: 1px solid #c3c3c3;
            border-bottom: 1px solid #c3c3c3;
            height: 30px;
            background-color: #eaeaea;
            text-align: center;
            width: 20%;
        }

        .right_big1 {
            border-top: 1px solid #c3c3c3;
            border-bottom: 1px solid #c3c3c3;
            height: 30px;
            padding-left: 5px;
            padding-top: 5px;
            padding-bottom: 5px;
            background-color: white;
            width: 30%;
        }

        .right_big2 {
            border-top: 1px solid #c3c3c3;
            border-bottom: 1px solid #c3c3c3;
            height: 30px;
            padding-left: 5px;
            padding-top: 5px;
            padding-bottom: 5px;
            width: 80%;
            background-color: white;
        }

        .table_vInfo {
            margin: 1%;
            border-collapse: collapse;
            width: 98%;
        }

        .left {
            border-right: 1px solid #e7e7e7;
            background-color: #dbe2ea;
            color: black;
            text-align: center;
            width: 30%;
            padding: 1%;
            margin: 1%;
            color: Maroon;
        }

        .right {
            border-left: 1px solid #e7e7e7;
            background-color: #F2EAED;
            text-align: left;
            width: 70%;
            padding: 1%;
            margin: 1%;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("input[id*='TextBox1']").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'yy/mm/dd',
                    showMonthAfterYear: true,
                    minDate: "0",
                    maxDate: "9m"
                });
                $("input[id*='TextBox2']").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'yy/mm/dd',
                    showMonthAfterYear: true,
                    minDate: "0",
                    maxDate: "9m"
                });
            });
            $("input[id*='TextBox1']").change(function () {
                $("input[id*='TextBox2']").val($("input[id*='TextBox1']").val());
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="background-color: white;">
            <table class="table_big">
                <tr>
                    <td>
                        <table class="table_vInfo">
                            <tr>
                                <td class="left">
                                    <asp:Label ID="Label83" runat="server" ForeColor="Maroon"
                                        Text='<%$Resources:Resource,intoSchoolBeginTime %>'></asp:Label>
                                </td>
                                <td class="right">
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    &nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem Value="08" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="09"></asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label89" runat="server" Font-Names="微軟正黑體" Font-Size="Small" ForeColor="#006666">：</asp:Label>
                                    <asp:DropDownList ID="DropDownList2" runat="server">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Name" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="Campus_save" runat="server" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="left">
                                    <asp:Label ID="Label84" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,intoSchoolEndTime %>"></asp:Label>
                                </td>
                                <td class="right">
                                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                                    &nbsp;<asp:DropDownList ID="DropDownList3" runat="server">
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem Value="08" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="09"></asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="Label90" runat="server" Font-Names="微軟正黑體" Font-Size="Small" ForeColor="#006666">：</asp:Label>
                                    <asp:DropDownList ID="DropDownList4" runat="server">
                                        <asp:ListItem>00</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>45</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>

                        </table>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="Button1" runat="server" CssClass="button_doubleline" Text="<%  $Resources:Resource,Confirm %>" OnClick="Button1_Click" Width="251px" Height="40px" OnClientClick="return confirm('您確定更新嗎?更新後將需重新簽核。');" />
                        <asp:Button ID="Button2" runat="server" Text="<%$Resources:Resource,CancelAndPrePage %>" CssClass="button_doubleline" OnClick="Button2_Click" Width="323px" Height="40px" />
                    </td>
                </tr>
            </table>

        </div>
        <div style="text-align: center">
            &nbsp;<asp:Label ID="ListNum" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="VTypeID" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="SpeRequest" runat="server" Visible="False"></asp:Label>
            <asp:Literal ID="Literal_Script" runat="server"></asp:Literal>
            <br />
        </div>
    </form>
</body>
</html>
