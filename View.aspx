<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .table_big{
            /*border:1px solid #9f9f9f;*/
            border:none;
            border-radius:3px;
            border-collapse: collapse;
            width:90%;
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
        .one{
            background-color: #F2EAED;
            text-align: left;
            width: 100%;
            padding: 1%;
            margin:1%;
        }
        .Record{
            border-left:none;
            border-right:none;
            border-top:1px solid #c3c3c3;
            border-bottom:1px solid #c3c3c3;
            background-color:#edeaf2;
        }
        .auto-style1 {
            border-right: 1px solid #e7e7e7;
            background-color: #dbe2ea;
            color: black;
            text-align: center;
            width: 30%;
            padding: 1%;
            margin: 1%;
            color: Maroon;
            height: 21px;
        }
        .auto-style2 {
            border-left: 1px solid #e7e7e7;
            background-color: #F2EAED;
            text-align: left;
            width: 70%;
            padding: 1%;
            margin: 1%;
            height: 21px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                
            });
            $("input[id*='ImageButton1']").click(function () {
                $.blockUI({
                    message: '<h1>Please wait...</h1>'
                });
            });
            $("input[id*='ImageButton2']").click(function () {
                $.blockUI({
                    message: '<h1>Please wait...</h1>'
                });
            });
            $("#MainContent_FileUpload1").change(function () {
                $("#MainContent_Button_FileUpload1").click();
            });
        });
    </script>
    <p>
        <asp:Label ID="Label31" runat="server" Font-Size="X-Large" ForeColor="#660033" 
            Text='<%$Resources:Resource,VForm %>'></asp:Label>
        </p>
    <table class="table_big">
            <tr>
                <td class="left_big">
                    
                    <asp:Label ID="Label41" runat="server" 
                        Text='<%$Resources:Resource,VStatus %>'></asp:Label>
                    
                </td>
                <td class="right_big1">
                    <asp:Label ID="nowAllowName" runat="server" ForeColor="#1145D2"></asp:Label>
                    <asp:Label ID="state" runat="server" ForeColor="#FF3300" Font-Bold="False"></asp:Label>
                    <asp:Label ID="nowAllow" runat="server" Visible="False" ForeColor="#FF9933"></asp:Label>
                </td>
                <td class="left_big">
                    <asp:Label ID="Label42" runat="server" 
                        Text='<%$Resources:Resource,VNo %>'></asp:Label>
                </td>
                <td class="right_big1">
                    <asp:Label ID="ListNum" runat="server" ForeColor="#1145D2"></asp:Label>
                    <asp:Label ID="C_level" runat="server" Visible="False"></asp:Label>
                    <asp:Label ID="T_level" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="left_big" height="30">
                    <asp:Label ID="Label2" runat="server" Text='<%$Resources:Resource,EmployeeID %>'></asp:Label>
                </td>
                <td class="right_big1">
                    <asp:Label ID="sno" runat="server"></asp:Label>
                </td>
                <td class="left_big">
                    <asp:Label ID="Label37" runat="server" Text='<%$Resources:Resource,Campus %>'></asp:Label>
                </td>
                <td class="right_big1">
                    <asp:Label ID="Campus" runat="server"></asp:Label>
                    <asp:Label ID="Campus2" runat="server" Font-Size="Small" ForeColor="#FF9999" 
                        Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="left_big">
                    <asp:Label ID="Label43" runat="server" 
                        Text='<%$Resources:Resource,Applicant %>'></asp:Label>
                </td>
                <td class="right_big1">
                    <asp:Label ID="name" runat="server"></asp:Label>
                </td>
                <td class="left_big">
                    <asp:Label ID="Label38" runat="server" Text='<%$ Resources:Resource,DeptName %>'></asp:Label>
                </td>
                <td class="right_big1">
                    <asp:Label ID="deptName" runat="server"></asp:Label>
                    <asp:Label ID="deptCode" runat="server" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="left_big">
                    <asp:Label ID="Label10" runat="server" Text='<%$Resources:Resource,VisitorType %>'></asp:Label>
                </td>
                <td class="right_big1">
                    <asp:Label ID="VTypeName" runat="server" ForeColor="#1145D2"></asp:Label>
                    <asp:Label ID="VTypeID" runat="server" ForeColor="#1145D2" Visible="False"></asp:Label>
                </td>
                <td class="left_big">
                    <asp:Label ID="Label11" runat="server" Text='<%$Resources:Resource,ApplicationDate %>'></asp:Label>
                </td>
                <td class="right_big1">
                    <asp:Label ID="applyTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="left_big">
                <asp:Label ID="Label17" runat="server" Text='<%$Resources:Resource,VisitorInfo %>'></asp:Label>
                    <br />
                    <asp:LinkButton ID="LinkButton1" runat="server" class='iframe' Text="<%$Resources:Resource,ModifyVisitorInfo %>" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
                </td>
                <td class="right_big2" colspan="3">
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
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label65" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="auto-style2">
                                    <asp:Label ID="Label102" runat="server"></asp:Label>
                                    人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label66" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label103" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label67" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label104" runat="server"></asp:Label>
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
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label99" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,visitorNum2 %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label107" runat="server"></asp:Label>
                                人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label70" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label108" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label71" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label109" runat="server"></asp:Label>
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
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label76" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,visitorNum2 %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label112" runat="server"></asp:Label>
                                人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label74" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label113" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label75" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label114" runat="server"></asp:Label>
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
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label79" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,visitorNum2 %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label117" runat="server"></asp:Label>
                                人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label80" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label118" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label81" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label119" runat="server"></asp:Label>
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
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label165" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label124" runat="server"></asp:Label>
                                    人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label4" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label125" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label5" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label126" runat="server"></asp:Label>
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
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label166" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label129" runat="server"></asp:Label>
                                    人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label49" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label130" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label50" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label131" runat="server"></asp:Label>
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
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label53" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label133" runat="server"></asp:Label>
                                    人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label54" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label134" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label55" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label135" runat="server"></asp:Label>
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
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label57" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label138" runat="server"></asp:Label>
                                    人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label58" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label139" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label59" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label140" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>   
                    <asp:Panel ID="Panel_11" runat="server">
                    <asp:Label ID="Label9" runat="server" Text="學校活動" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label15" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,visitorName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label16" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label21" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="auto-style2">
                                    <asp:Label ID="Label22" runat="server"></asp:Label>
                                    人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label23" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label24" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label25" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label26" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                    <asp:Panel ID="Panel_12" runat="server">
                    <asp:Label ID="Label27" runat="server" Text="假日到校" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label28" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,staffName %>'></asp:Label>
                            </td>
                            <td class="right">
                                    <asp:Label ID="Label29" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label30" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="auto-style2">
                                    <asp:Label ID="Label32" runat="server"></asp:Label>
                                    人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label33" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label34" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label35" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label39" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                           
                </asp:Panel>
                </td>
            </tr>
            <tr>
                <td class="left_big">
                <asp:Label ID="Label18" runat="server" Text='<%$Resources:Resource,intoSchoolInfo %>'></asp:Label>
                    <br />
                    <asp:LinkButton ID="LinkButton2" runat="server" class='iframe' Text="<%$Resources:Resource,ModifyITSInfo %>" OnClick="LinkButton2_Click">LinkButton</asp:LinkButton>
                </td>
                <td class="right_big2" colspan="3">
                
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label82" runat="server" ForeColor="Maroon" 
                                    Text='<%$ Resources:Resource,intoSchoolPurpose %>'></asp:Label>
                            </td>
                            <td class="right">
                    <asp:TextBox ID="TextBox8" runat="server" ReadOnly="True" Rows="6" 
                        TextMode="MultiLine" Width="95%" Height="142px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label83" runat="server" ForeColor="Maroon" 
                                    Text='<%$Resources:Resource,intoSchoolBeginTime %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label142" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label84" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,intoSchoolEndTime %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label143" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label85" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,receptionDepartment %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label144" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label7" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,receptionLocation %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label8" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <asp:Panel ID="Panel_09" runat="server">                 
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label86" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,interviewLocation %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label145" runat="server"></asp:Label>
                            </td>
                        </tr>
                        </asp:Panel> 
                        <asp:Panel ID="Panel_10" runat="server">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label87" runat="server" ForeColor="Maroon" Text="<%$Resources:Resource,constructionArea %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:Label ID="Label146" runat="server"></asp:Label>
                            </td>
                        </tr>
                        </asp:Panel>                                             
                    </table>
      
                </td>
            </tr>
            <tr>
                <td class="left_big">
                <asp:Label ID="Label88" runat="server" Text='<%$Resources:Resource,specialRequest %>'></asp:Label>
                    <br />
                    <asp:Label ID="Label147" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="right_big1">
                <asp:CheckBoxList ID="CheckBoxList1" runat="server" Width="90%" CellPadding="2" CellSpacing="2">
                    <asp:ListItem Text='<%$Resources:Resource,meal %>' Value='用餐'></asp:ListItem>
                    <asp:ListItem Text='<%$Resources:Resource,visitprincipal %>' Value='拜訪校長'></asp:ListItem>
                    <asp:ListItem Text='<%$Resources:Resource,campusTour %>' Value='校園導覽'></asp:ListItem>
                    <asp:ListItem Text='<%$Resources:Resource,interview %>' Value='採訪'></asp:ListItem>
                    <asp:ListItem Text='<%$Resources:Resource,deliveryOfGoods %>' Value='貨物運送'></asp:ListItem>
                    <asp:ListItem Text='<%$Resources:Resource,visitTeacher %>' Value='拜訪師長'></asp:ListItem>
                     <asp:ListItem Text='<%$Resources:Resource,schoolParking %>' Value='校內停車'></asp:ListItem>
                    <asp:ListItem Text='<%$Resources:Resource,Escalator_Value %>' Value='手扶梯開放'></asp:ListItem>
                </asp:CheckBoxList >
                </td>
                <td class="left_big">
                    <asp:Label ID="Label19" runat="server" Text='<%$Resources:Resource,Notes %>'></asp:Label>
                    <br />
                     <%--<asp:LinkButton ID="LinkButton3" runat="server" class='iframe' Text="<%$Resources:Resource,ModifyNotes %>" OnClick="LinkButton3_Click">LinkButton</asp:LinkButton>--%> 
                </td>
                <td class="right_big1">
                    <asp:TextBox ID="TextBox7" runat="server" ReadOnly="True" Rows="6" 
                        TextMode="MultiLine" Width="95%" Height="146px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="left_big">
                    <asp:Label ID="Label20" runat="server" Text='<%$Resources:Resource,ProofDocument %>'></asp:Label>
                </td>
                <td class="right_big2" colspan="3">
                    <asp:PlaceHolder ID="PlaceHolderAttachment3" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td class="left_big">
                <asp:Label ID="Label100" runat="server" Text='<%$Resources:Resource,NoticePeople %>'></asp:Label>
                </td>
                <td class="right_big2" colspan="3">
                
                <asp:Literal ID="Literal_Notice" runat="server"></asp:Literal>
               
                    <br />
                </td>
            </tr>
            <tr>
                <td class="left_big">
                <asp:Label ID="Label167" runat="server" Text='<%$Resources:Resource,insertByUser %>'></asp:Label>
                </td>
                <td class="right_big2" colspan="3">
                
                <asp:Literal ID="Literal_Notice_byUser" runat="server"></asp:Literal>
               
                </td>
            </tr>
        </table>
    <br />
    <asp:Label ID="Label12" runat="server" Font-Names="微軟正黑體" Font-Size="Medium" 
        ForeColor="#660066" Text='<%$Resources:Resource,ListOfOpinions %>'></asp:Label>
    <br />
    <div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </div>
    <br />
    <asp:Panel ID="Panel1" runat="server" Visible="False" Width="100%">
        <table width="90%">
            <tr>
                <td>
                    <asp:Label ID="Label36" runat="server" Font-Names="微軟正黑體" Font-Size="Medium" 
                        ForeColor="#660066" Text="》簽核意見(Opinions)"></asp:Label>
                        <br />
                        <asp:TextBox ID="TextBox3" runat="server" MaxLength="500" Rows="7" 
                        TextMode="MultiLine" Width="100%"></asp:TextBox>
                        <table align="center" style="width: 90%;">
                            <tr>
                                <td style="height:55px">
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/pic/Approved.png" 
                                        OnClick="ImageButton1_Click" style="height: 51px" />
                                </td>
                                <td style="height:55px">
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/pic/Rejected.png" 
                                        OnClick="ImageButton2_Click" />
                                </td>
                            </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel2" runat="server" Width="100%">
        <table width="90%">
            <tr>
                <td >
                    <asp:Label ID="Label6" runat="server" Font-Names="微軟正黑體" Font-Size="Medium"
                        ForeColor="#660066" Text='<%$Resources:Resource,Record %>'></asp:Label>
                        &nbsp;<asp:Label ID="intoSchool" runat="server" Visible="False"></asp:Label>
                    <br />
                    <asp:DetailsView ID="DetailsView1" runat="server" AutoGenerateRows="False" CellPadding="4" DataKeyNames="ListNum" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Height="50px" Width="60%" Font-Size="Small">
                        <AlternatingRowStyle BackColor="White" />
                        <CommandRowStyle BackColor="#dbe2ea" Font-Bold="False" Font-Size="Medium" />
                        <EditRowStyle BackColor="#2461BF" />
                        <FieldHeaderStyle BackColor="#F2EAED" Font-Bold="False" Width="40%" />
                        <Fields>
                            <asp:BoundField DataField="ListNum" HeaderText='<%$Resources:Resource,Campus %>' ReadOnly="True" SortExpression="ListNum" Visible="False" />
                            <asp:TemplateField SortExpression="intoSchoolStatus">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DropDownList1" runat="server" SelectedValue='<%# Bind("intoSchoolStatus") %>'>
                                        <asp:ListItem>未到</asp:ListItem>
                                        <asp:ListItem>遲到</asp:ListItem>
                                        <asp:ListItem>已到</asp:ListItem>
                                        <asp:ListItem></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:Label ID="Label148" runat="server" Text='<%$Resources:Resource,intoSchoolStatus %>'></asp:Label>
                                </HeaderTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("intoSchoolStatus") %>'></asp:TextBox>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("intoSchoolStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="startTime_actual">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("startTime_actual", "{0:yyyy/MM/dd HH:mm:ss}") %>' OnDataBinding="TextBox2_DataBinding"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:Label ID="Label149" runat="server" Text='<%$Resources:Resource,intoSchoolBeginTime_real %>'></asp:Label>
                                </HeaderTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("startTime_actual") %>'></asp:TextBox>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("startTime_actual", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="endTime_actual">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("endTime_actual", "{0:yyyy/MM/dd HH:mm:ss}") %>' OnDataBinding="TextBox2_DataBinding"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:Label ID="Label150" runat="server" Text='<%$Resources:Resource,intoSchoolEndTime_real %>'></asp:Label>
                                </HeaderTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("endTime_actual") %>'></asp:TextBox>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("endTime_actual", "{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="Record">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox9" runat="server" Height="100px" TextMode="MultiLine" Width="100%" Text='<%# Bind("Record") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    <asp:Label ID="Label151" runat="server" Text='<%$Resources:Resource,Record_ %>'></asp:Label>
                                </HeaderTemplate>
                                <InsertItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Record") %>'></asp:TextBox>
                                </InsertItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Record") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                        </Fields>
                        <FooterStyle BackColor="#F2EAED" Font-Bold="False" ForeColor="White" />
                        <HeaderStyle BackColor="#F2EAED" Font-Bold="False" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F2EAED" />
                    </asp:DetailsView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [startTime_actual], [endTime_actual], [Record], [intoSchoolStatus], [ListNum] FROM [List_V]
where ListNum=@ListNum" DeleteCommand="DELETE FROM [List_V] WHERE [ListNum] = @ListNum" InsertCommand="INSERT INTO [List_V] ([startTime_actual], [endTime_actual], [Record], [intoSchoolStatus], [ListNum]) VALUES (@startTime_actual, @endTime_actual, @Record, @intoSchoolStatus, @ListNum)" UpdateCommand="UPDATE [List_V] SET [startTime_actual] = @startTime_actual, [endTime_actual] = @endTime_actual, [Record] = @Record, [intoSchoolStatus] = @intoSchoolStatus WHERE [ListNum] = @ListNum">
                        <DeleteParameters>
                            <asp:Parameter Name="ListNum" Type="String" />
                        </DeleteParameters>
                        <InsertParameters>
                            <asp:Parameter Name="startTime_actual" Type="DateTime" />
                            <asp:Parameter Name="endTime_actual" Type="DateTime" />
                            <asp:Parameter Name="Record" Type="String" />
                            <asp:Parameter Name="intoSchoolStatus" Type="String" />
                            <asp:Parameter Name="ListNum" Type="String" />
                        </InsertParameters>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ListNum" Name="ListNum" PropertyName="Text" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="startTime_actual" Type="DateTime" />
                            <asp:Parameter Name="endTime_actual" Type="DateTime" />
                            <asp:Parameter Name="Record" Type="String" />
                            <asp:Parameter Name="intoSchoolStatus" Type="String" />
                            <asp:Parameter Name="ListNum" Type="String" />
                        </UpdateParameters>
                    </asp:SqlDataSource>
                    <asp:Panel ID="Panel7" runat="server">
                        <table class="table_big">
                            <tr>
                                <td class="left_big">
                                    <asp:Label ID="Label163" runat="server" Text="<%$Resources:Resource,insertProofDocument %>"></asp:Label>
                                </td>
                                <td class="right_big2">
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="207px" />
                                    <asp:Button ID="Button_FileUpload1" runat="server" onclick="Button_FileUpload1_Click" Text="<%$Resources:Resource,Upload %>" />
                                    <br />
                                    <asp:PlaceHolder ID="PlaceHolderAttachment" runat="server"></asp:PlaceHolder>
                                    <br />
                                    <asp:Label ID="Label164" runat="server" Font-Bold="False" ForeColor="Red" Text="<%$Resources:Resource,Ex7 %>"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="one" colspan="2">
                                    <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" text="<%$Resources:Resource,Upload %>" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="Panel6" runat="server">
        <asp:Button ID="Button1" runat="server" Font-Size="Medium" 
        OnClick="Button1_Click" Text="管理者撤銷" style="height: 26px" />
    </asp:Panel>
</asp:Content>

