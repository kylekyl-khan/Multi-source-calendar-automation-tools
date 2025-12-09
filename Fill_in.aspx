<%@ Page Title="" Language="C#" MasterPageFile="~/Site2.master" AutoEventWireup="true" CodeFile="Fill_in.aspx.cs" Inherits="Fill_in" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .table_big {
            /*border:1px solid #9f9f9f;*/
            border: none;
            border-radius: 3px;
            border-collapse: collapse;
            width: 90%;
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

        .center {
            border-left: 1px solid #e7e7e7;
            background-color: #F2EAED;
            text-align: left;
            width: 98%;
            padding: 1%;
            margin: 1%;
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
            height: 29px;
        }

        .auto-style2 {
            border-left: 1px solid #e7e7e7;
            background-color: #F2EAED;
            text-align: left;
            width: 70%;
            padding: 1%;
            margin: 1%;
            height: 29px;
        }

        .auto-style3 {
            border-right: 1px solid #e7e7e7;
            background-color: #dbe2ea;
            color: black;
            text-align: center;
            width: 30%;
            padding: 1%;
            margin: 1%;
            color: Maroon;
            height: 25px;
        }

        .auto-style4 {
            border-left: 1px solid #e7e7e7;
            background-color: #F2EAED;
            text-align: left;
            width: 70%;
            padding: 1%;
            margin: 1%;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                $("input[id*='TextBox_startTime']").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'yy/mm/dd',
                    showMonthAfterYear: true,
                    minDate: "0",
                    maxDate: "9m"
                });
                $("input[id*='TextBox_endTime']").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    dateFormat: 'yy/mm/dd',
                    showMonthAfterYear: true,
                    minDate: "0",
                    maxDate: "9m"
                });
                var DropDownList1 = $("select[id*='DropDownList1']").val();
                if (DropDownList1 == "請選擇") {
                    $('.c1').hide();
                    $('#Button3').hide();
                }
                else {
                    $('.c1').show();
                    $('#Button3').show();
                }
            });
            $("input[id*='TextBox_startTime']").change(function () {
                $("input[id*='TextBox_endTime']").val($("input[id*='TextBox_startTime']").val());
            });
            $("select[id*='DropDownList1']").change(function () {
                var DropDownList1 = $("select[id*='DropDownList1']").val();
                if (DropDownList1 == "請選擇") {
                    $('.c1').hide();
                    $('#Button3').hide();
                }
                else {
                    $('.c1').show();
                    $('#Button3').show();
                }
            });
            $("input[id*='Button3']").click(function () {
                $.blockUI({
                    message: '<h1>Please wait...</h1>'
                });
            });
            $("#MainContent_FileUpload1").change(function () {
                $("#MainContent_Button_FileUpload1").click();
            });
        });
    </script>
    <div align="left" style="width: 90%; height: 40px;">
        <asp:Label ID="Label31" runat="server" Font-Size="X-Large" ForeColor="#660033"
            Text='<%$Resources:Resource,ApplicationForm2 %>'></asp:Label>
    </div>
    <table class="table_big">
        <tr>
            <td class="left_big">
                <asp:Label ID="Label2" runat="server" Text='<%$ Resources:Resource,EmployeeID%>'></asp:Label>
            </td>
            <td class="right_big1">
                <asp:Label ID="EmployeeID" runat="server"></asp:Label>
                &nbsp;<asp:Label ID="AccountID" runat="server" Visible="False"></asp:Label>
                <br />
            </td>
            <td class="left_big">
                <asp:Label ID="Label30" runat="server" Text='<%$ Resources:Resource,Campus%>'></asp:Label>
            </td>
            <td class="right_big1">
                <asp:Label ID="Campus" runat="server"></asp:Label>
                <asp:Label ID="Campus_save" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="left_big">
                <asp:Label ID="Label3" runat="server" Text='<%$ Resources:Resource,Name %>'></asp:Label>
            </td>
            <td class="right_big1">
                <asp:Label ID="Name"
                    runat="server"></asp:Label>
                <asp:Label ID="degree" runat="server" ForeColor="#669999" Visible="False"></asp:Label>
                <br />
            </td>
            <td class="left_big">
                <asp:Label ID="Label4" runat="server" Text='<%$ Resources:Resource,DeptName %>'></asp:Label>
            </td>
            <td class="right_big1">
                <asp:Label ID="DeptName" runat="server"></asp:Label>
                <asp:Label ID="DeptID" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="color: #660033" class="left_big">＊<asp:Label ID="Label10" runat="server" Text='<%$ Resources:Resource,VisitorType %>'></asp:Label>
            </td>
            <td class="right_big2" colspan="3">
                <asp:DropDownList
                    ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" ForeColor="#1145D2">
                </asp:DropDownList>
                <br />
            </td>
        </tr>
        <tr class="c1">
            <td class="left_big">
                <asp:Label ID="Label17" runat="server" Text='<%$Resources:Resource,VisitorInfo %>'></asp:Label>
                <br />
            </td>
            <td class="right_big2" colspan="3">
                <asp:Panel ID="Panel_01" runat="server">
                    <asp:Label ID="Label91" runat="server" Text="一般訪客" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label61" runat="server"
                                    Text='<%$ Resources:Resource,visitorName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox26" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label65" runat="server"
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox51" runat="server" Width="25%"></asp:TextBox>
                                人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label66" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox30" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label67" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox31" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_02" runat="server" Visible="False">
                    <asp:Label ID="Label92" runat="server" Text="外校生家長" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label68" runat="server"
                                    Text='<%$ Resources:Resource,parentName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox32" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label69" runat="server"
                                    Text='<%$Resources:Resource,stuName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox50" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label99" runat="server" Text="<%$Resources:Resource,visitorNum2 %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox52" runat="server" Width="25%"></asp:TextBox>
                                &nbsp;人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label70" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox33" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label71" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox34" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_03" runat="server" Visible="False">
                    <asp:Label ID="Label93" runat="server" Text="政府機關" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label72" runat="server"
                                    Text='<%$ Resources:Resource,governmentName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox35" runat="server" MaxLength="40" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label73" runat="server"
                                    Text='<%$Resources:Resource,leaderName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox38" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label76" runat="server" Text="<%$Resources:Resource,visitorNum2 %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox53" runat="server" Width="25%"></asp:TextBox>
                                &nbsp;人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label74" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox36" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label75" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox37" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_04" runat="server" Visible="False">
                    <asp:Label ID="Label94" runat="server" Text="媒體" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label77" runat="server"
                                    Text='<%$ Resources:Resource,mediaName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox39" runat="server" MaxLength="40" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label78" runat="server"
                                    Text='<%$Resources:Resource,Name %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox40" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label79" runat="server" Text="<%$Resources:Resource,visitorNum2 %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox54" runat="server" Width="25%"></asp:TextBox>
                                &nbsp;人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label80" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox41" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label81" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox42" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_05" runat="server" Visible="False">
                    <asp:Label ID="Label95" runat="server" Text="在籍學生家長" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label12" runat="server"
                                    Text='<%$ Resources:Resource,stuName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox1" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label41" runat="server" Text="<%$ Resources:Resource,stuClass %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox8" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label13" runat="server"
                                    Text='<%$Resources:Resource,stuNum %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox2" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label14" runat="server"
                                    Text='<%$Resources:Resource,parentName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox9" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label15" runat="server"
                                    Text='<%$Resources:Resource,visitorNum %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox55" runat="server" Width="25%"></asp:TextBox>
                                &nbsp;人</td>
                        </tr>
                        <tr>
                            <td class="auto-style3">
                                <asp:Label ID="Label42" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="auto-style4">
                                <asp:TextBox ID="TextBox10" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label43" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox11" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_06" runat="server" Visible="False">
                    <asp:Label ID="Label96" runat="server" Text="校友" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td colspan="2" class="center">各位師長您好： 
                                <br />
                                近日訪客入校系統上線，開始有許多校友進入校園，基於維護校園安全及班級內的教學秩序，以下事項敬請留意並轉達校友：<br />
                                <br />
                                &lt;校友入校注意事項&gt;
                                <br />
                                <ol>
                                    <li>勿穿學校服裝、勿搭乘校車，以免造成在校同學誤會及不便。</li>
                                    <li>有入班或一同用餐需求者，敬請申請之師長事先提出， 並以手動方式加入當日將受到校友拜訪的其他師長， 如未在事前申請上獲得核准而臨時要求入班、臨時要求一同用餐者， 師長可直接婉拒。</li>
                                    <li>校友如需吃校內餐點，請與餐廳人員聯繫，相關費用將於現場結清。 </li>
                                    <li>校友應攜帶相關身分證件於學校門口與警衛換證。</li>
                                    <li>未帶證件者，因無法識別身分，將不予入校。</li>
                                    <li>校友於校園內應隨時配戴識別證。</li>
                                    <li>於校園內，應遵守校園秩序及在校學生隱私， 不應針對教學現場或個人擅自拍照或錄影。</li>
                                </ol>

                                如有校友未配合以上事項，將依照相關規定辦理。</td>
                        </tr>
                        <tr>
                            <td class="left"><span style="color: red">*</span><asp:Label ID="Label44" runat="server" Text="<%$ Resources:Resource,alumniName %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox12" runat="server" Height="100px" MaxLength="300" TextMode="MultiLine" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label51" runat="server" Text="<%$Resources:Resource,stuNum_inschool %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox18" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label101" runat="server"
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox56" runat="server" Width="25%"></asp:TextBox>
                                &nbsp;人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label49" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox16" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label50" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox17" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_07" runat="server" Visible="False">
                    <asp:Label ID="Label97" runat="server" Text="廠商" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label52" runat="server"
                                    Text='<%$ Resources:Resource,companyName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox19" runat="server" MaxLength="40" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label53" runat="server"
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox57" runat="server" Width="25%"></asp:TextBox>
                                &nbsp;人</td>
                        </tr>
                        <tr>
                            <td class="auto-style1">
                                <asp:Label ID="Label54" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="auto-style2">
                                <asp:TextBox ID="TextBox20" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label55" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox21" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_08" runat="server" Visible="False">
                    <asp:Label ID="Label98" runat="server" Text="團體訪客" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label56" runat="server"
                                    Text='<%$ Resources:Resource,groupName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox22" runat="server" MaxLength="40" Width="80%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left"><span style="color: red">*</span><asp:Label ID="Label60" runat="server" Text="<%$Resources:Resource,leaderName %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox25" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label57" runat="server"
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox58" runat="server" Width="25%"></asp:TextBox>
                                &nbsp;人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label58" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox23" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label59" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox24" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_11" runat="server">
                    <asp:Label ID="Label1" runat="server" Text="學校活動" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label5" runat="server"
                                    Text='<%$ Resources:Resource,visitorName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox3" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label6" runat="server"
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox4" runat="server" Width="25%"></asp:TextBox>
                                人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label7" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox5" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label8" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox6" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
                <asp:Panel ID="Panel_12" runat="server">
                    <asp:Label ID="Label9" runat="server" Text="假日到校" Visible="False"></asp:Label>
                    <table class="table_vInfo">
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label11" runat="server"
                                    Text='<%$Resources:Resource,StaffName %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox13" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <span style="color: red">*</span><asp:Label ID="Label16" runat="server"
                                    Text='<%$Resources:Resource,visitorNum2 %>'></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox14" runat="server" Width="25%"></asp:TextBox>
                                人</td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label22" runat="server" Text="<%$Resources:Resource,carNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox15" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label23" runat="server" Text="<%$Resources:Resource,phoneNum %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox27" runat="server" MaxLength="40"></asp:TextBox>
                            </td>
                        </tr>

                    </table>

                </asp:Panel>
            </td>
        </tr>
        <tr class="c1">
            <td class="left_big">
                <asp:Label ID="Label18" runat="server" Text='<%$Resources:Resource,intoSchoolInfo %>'></asp:Label>
            </td>
            <td class="right_big2" colspan="3">

                <table class="table_vInfo">
                    <tr>
                        <td class="left">
                            <span style="color: red">*</span><asp:Label ID="Label82" runat="server"
                                Text='<%$ Resources:Resource,intoSchoolPurpose %>'></asp:Label>
                        </td>
                        <td class="right">
                            <asp:TextBox ID="TextBox43" runat="server" MaxLength="40" Height="89px" TextMode="MultiLine" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            <span style="color: red">*</span><asp:Label ID="Label83" runat="server"
                                Text='<%$Resources:Resource,intoSchoolBeginTime %>'></asp:Label>
                        </td>
                        <td class="right">
                            <asp:TextBox ID="TextBox_startTime" runat="server" MaxLength="40" Width="40%"></asp:TextBox>
                            &nbsp;<asp:DropDownList ID="DropDownList2" runat="server">
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
                            <asp:DropDownList ID="DropDownList3" runat="server">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            <span style="color: red">*</span><asp:Label ID="Label84" runat="server" Text="<%$Resources:Resource,intoSchoolEndTime %>"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:TextBox ID="TextBox_endTime" runat="server" MaxLength="40" Width="40%"></asp:TextBox>
                            &nbsp;<asp:DropDownList ID="DropDownList4" runat="server">
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem Value="08"></asp:ListItem>
                                <asp:ListItem Value="09"></asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem Selected="True">17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Label ID="Label90" runat="server" Font-Names="微軟正黑體" Font-Size="Small" ForeColor="#006666">：</asp:Label>
                            <asp:DropDownList ID="DropDownList15" runat="server">
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            <span style="color: red">*</span><asp:Label ID="Label85" runat="server" Text="<%$Resources:Resource,receptionDepartment %>"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:TextBox ID="TextBox45" runat="server" MaxLength="40" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="left">
                            <span style="color: red">*</span><asp:Label ID="Label104" runat="server" Text="<%$Resources:Resource,receptionLocation %>"></asp:Label>
                        </td>
                        <td class="right">
                            <asp:TextBox ID="TextBox60" runat="server" MaxLength="40" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <asp:Panel ID="Panel_09" runat="server">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label86" runat="server" Text="<%$Resources:Resource,interviewLocation %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox48" runat="server" MaxLength="40" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="Panel_10" runat="server">
                        <tr>
                            <td class="left">
                                <asp:Label ID="Label87" runat="server" Text="<%$Resources:Resource,constructionArea %>"></asp:Label>
                            </td>
                            <td class="right">
                                <asp:TextBox ID="TextBox49" runat="server" MaxLength="40" Width="95%"></asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>

            </td>
        </tr>
        <tr class="c1">
            <td class="left_big">
                <asp:Label ID="Label88" runat="server" Text='<%$Resources:Resource,specialRequest %>'></asp:Label>
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
                    <asp:ListItem Text='<%$Resources:Resource,Escalator_Text %>' Value="手扶梯開放"></asp:ListItem>
                </asp:CheckBoxList>
            </td>
            <td class="left_big">
                <asp:Label ID="Label19" runat="server" Text='<%$Resources:Resource,Notes %>'></asp:Label>
            </td>
            <td class="right_big1">
                <asp:TextBox ID="TextBox7" runat="server" Rows="6" TextMode="MultiLine"
                    Width="95%" MaxLength="600" Height="151px"></asp:TextBox>
            </td>
        </tr>
        <tr class="c1">
            <td class="left_big">
                <asp:Label ID="Label20" runat="server" Text='<%$Resources:Resource,ProofDocument %>'></asp:Label>
            </td>
            <td class="right_big2" colspan="3">
                <asp:FileUpload ID="FileUpload1" runat="server"
                    Width="207px" />
                <asp:Button ID="Button_FileUpload1" runat="server"
                    OnClick="Button_FileUpload1_Click" Text='<%$Resources:Resource,Upload %>' />
                <br />
                <asp:PlaceHolder ID="PlaceHolderAttachment" runat="server"></asp:PlaceHolder>
                <br />
                <asp:Label ID="Label25" runat="server" Font-Bold="True" ForeColor="Red" Text='<%$Resources:Resource,Ex7 %>'></asp:Label>
            </td>
        </tr>
        <tr class="c1">
            <td class="left_big">
                <asp:Label ID="Label21" runat="server" Text='<%$Resources:Resource,Process %>'></asp:Label>
            </td>
            <td class="right_big2" colspan="3">

                <asp:Literal ID="Literal_Flow" runat="server"></asp:Literal>

            </td>
        </tr>
        <tr class="c1">
            <td class="left_big">
                <asp:Label ID="Label100" runat="server" Text='<%$Resources:Resource,NoticePeople %>'></asp:Label>
            </td>
            <td class="right_big2" colspan="3">

                <asp:Literal ID="Literal_Notice" runat="server"></asp:Literal>

            </td>
        </tr>
        <tr class="c1">
            <td class="left_big">
                <asp:Label ID="Label102" runat="server" Text='<%$Resources:Resource,insertByUser %>'></asp:Label>
            </td>
            <td class="right_big2" colspan="3">

                <asp:Label ID="Label103" runat="server" Text='<%$Resources:Resource,Search1 %>'></asp:Label>
                <br />
                <asp:TextBox ID="TextBox59" runat="server"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text='<%$Resources:Resource,Search %>' />
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" Width="80%" OnRowCommand="GridView1_RowCommand">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="類型" HeaderText="類型" SortExpression="類型" />
                        <asp:BoundField DataField="編號" HeaderText="編號" SortExpression="編號" />
                        <asp:BoundField DataField="名稱" HeaderText="名稱" SortExpression="名稱" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="Button_Insert" runat="server" Text="<%$Resources:Resource,Insert %>" CommandName="myInsert" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                    ConnectionString="<%$ ConnectionStrings:DB_MisAdmin %>"
                    ProviderName="System.Data.SqlClient" SelectCommand="">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="EmployeeID" Name="EmployeeID" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:GridView ID="GridView2" runat="server" BackColor="White"
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                    ForeColor="Black" GridLines="Vertical" DataSourceID="SqlDataSource2" Width="90%"
                    Font-Size="Small" AutoGenerateColumns="False" DataKeyNames="sid">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" />
                        <asp:BoundField DataField="sid" HeaderText="sid" SortExpression="sid" InsertVisible="False" ReadOnly="True" Visible="False" />
                        <asp:BoundField DataField="noticeEmployeeID" HeaderText="員編" SortExpression="noticeEmployeeID" />
                        <asp:BoundField DataField="noticeName" HeaderText="姓名" SortExpression="noticeName" />
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

                <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                    ConnectionString="<%$ ConnectionStrings:DB_Tea_VToSchool %>"
                    ProviderName="System.Data.SqlClient" SelectCommand="select *  from Temp_noticePerson where EmployeeID =@EmployeeID" DeleteCommand="delete from Temp_noticePerson where sid =@sid">
                    <DeleteParameters>
                        <asp:Parameter Name="sid" Type="Int32" />
                    </DeleteParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="EmployeeID" Name="EmployeeID" PropertyName="Text" />
                    </SelectParameters>
                </asp:SqlDataSource>

            </td>
        </tr>
        <tr class="c1">
            <td class="left_big">
                <asp:Label ID="Label24" runat="server" Text='<%$Resources:Resource,SendGuardEmail %>'></asp:Label>
            </td>
            <td class="right_big2" colspan="3">

                <asp:CheckBox ID="CheckBox1" runat="server" Text="表單於簽核狀態時預先通知警衛" Width="90%" Checked="True" />

            </td>
        </tr>
    </table>
    <div align="center" style="width: 90%">
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click"
            Text='<%$Resources:Resource,Submit %>' Width="100px" Height="49px" />
        <br />
    </div>
</asp:Content>

