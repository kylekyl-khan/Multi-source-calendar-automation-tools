<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifyVInfo.aspx.cs" Inherits="detailPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>康橋國際學校-訪客入校系統</title>
    <script src="Scripts/jquery-1.10.2.js" type="text/javascript"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui-1.10.4.custom.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
       .table_big{
            /*border:1px solid #9f9f9f;*/
            border:none;
            border-radius:3px;
            border-collapse: collapse;
            width:80%;
            font-family: 微軟正黑體; 
            font-size: small; 
            margin-bottom:10px;
        }
        .left_big
        {
            border-top:1px solid #c3c3c3;
            border-bottom:1px solid #c3c3c3;
            height:30px;
            background-color:#eaeaea;
            text-align:center;
            width:20%;
        }
        .right_big1
        {
            border-top:1px solid #c3c3c3;
            border-bottom:1px solid #c3c3c3;
            height:30px;
            padding-left: 5px;
            padding-top: 5px;
            padding-bottom: 5px;
            background-color:white;
            width:30%;
        }
        .right_big2
        {
            border-top:1px solid #c3c3c3;
            border-bottom:1px solid #c3c3c3;
            height:30px;
            padding-left: 5px;
            padding-top: 5px;
            padding-bottom: 5px;
            width:80%;
            background-color:white;
        }
        
        .table_vInfo{
            margin:1%;
            border-collapse: collapse;
            width:98%;
        }
        .left {
            border-right:1px solid #e7e7e7;
            background-color: #dbe2ea;
            color: black;
            text-align: center;
            width: 30%;
            padding: 1%;
            margin:1%;
            color: Maroon;
        }

        .right {
            border-left:1px solid #e7e7e7;
            background-color: #F2EAED;
            text-align: left;
            width: 70%;
            padding: 1%;
            margin:1%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="background-color:white;">
        <table class="table_big">
            <tr >
                <td ">
                    <asp:Panel ID="Panel_01" runat="server">
                    <asp:Label ID="Label91" runat="server" Text="一般訪客" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label61" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,visitorName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label101" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                <asp:Panel ID="Panel_02" runat="server" Visible="False">
                    <asp:Label ID="Label92" runat="server" Text="外校生家長" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label68" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,parentName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label105" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label69" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,stuName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label106" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                <asp:Panel ID="Panel_03" runat="server" Visible="False">
                    <asp:Label ID="Label93" runat="server" Text="政府機關" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label72" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,governmentName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label110" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label73" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,leaderName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label111" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                <asp:Panel ID="Panel_04" runat="server" Visible="False">
                    <asp:Label ID="Label94" runat="server" Text="媒體" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label77" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,mediaName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label115" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label78" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,Name %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label116" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                <asp:Panel ID="Panel_05" runat="server" Visible="False">
                    <asp:Label ID="Label95" runat="server" Text="在籍學生家長" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label1" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,stuName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label120" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label3" runat="server" ForeColor="Maroon" Text="<%$ Resources:Resource,stuClass %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label121" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label13" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,stuNum %>'></asp:Label>
                                    </td>
                            <td class="right">
                                    <asp:Label ID="Label122" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label14" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,parentName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label123" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                <asp:Panel ID="Panel_06" runat="server" Visible="False">
                    <asp:Label ID="Label96" runat="server" Text="校友" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label44" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,alumniName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label127" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label51" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,stuNum_inschool %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label128" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                <asp:Panel ID="Panel_07" runat="server" Visible="False">
                    <asp:Label ID="Label97" runat="server" Text="廠商" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label52" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,companyName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label132" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                <asp:Panel ID="Panel_08" runat="server" Visible="False">
                    <asp:Label ID="Label98" runat="server" Text="團體訪客" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label56" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,groupName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label136" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left"><asp:Label ID="Label60" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,leaderName %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label137" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table> 
                </asp:Panel> 
                    <asp:Panel ID="Panel_11" runat="server">
                    <asp:Label ID="Label2" runat="server" Text="學校活動" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label4" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,visitorName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label5" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                      <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label9" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:TextBox ID="TextBox1" runat="server" Width="25%"></asp:TextBox>
                                    人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label10" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label11" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                </td>
            </tr>
            <tr>
                <td >
            <asp:Button ID="Button1" runat="server" CssClass="button_doubleline" Text="<%  $Resources:Resource,Confirm %>" OnClick="Button1_Click" Width="251px" Height="40px" />
                    <asp:Button ID="Button2" runat="server" Text="<%$Resources:Resource,CancelAndPrePage %>" CssClass="button_doubleline" OnClick="Button2_Click" Width="323px" Height="40px"/>
                    </td>
            </tr>
        </table>
        
    </div>
        <div style="text-align:center">
&nbsp;<asp:Label ID="ListNum" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="VTypeID" runat="server" Visible="False"></asp:Label>
            <asp:Literal ID="Literal_Script" runat="server"></asp:Literal>
                <br />
        </div>
    </form>
</body>
</html>
