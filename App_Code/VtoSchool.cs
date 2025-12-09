using System.Configuration;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// HCP_OverTime 的摘要描述
/// </summary>
public class VtoSchool
{
  public VtoSchool()
  {
    //
    // TODO: 在此加入建構函式的程式碼
    //
  }

  /// <summary>
  /// 取得特出人員或組織流程
  /// </summary>
  /// <param name="DeptID">組織代碼</param>
  /// <param name="MaxJobGrade">最大層級</param>
  /// <param name="Ori_Allow_AD">目前從EIP取得的簽核流程</param>
  /// <param name="EmployeeID">目前使用者的員編</param>
  /// <returns></returns>
  public string[] Get_SpecialSignList(string DeptID, int MaxJobGrade, string[] Ori_Allow_AD, string EmployeeID)
  {
    string[] allow_AD = Ori_Allow_AD;

    Operation_MSSQL MSSQL = new Operation_MSSQL();
    SqlDataReader dr;
    StringBuilder str_cmd = new StringBuilder();

    //撰寫SQL指令
    str_cmd.AppendLine("select SignEmployeeID");
    str_cmd.AppendLine("from Sys_SpecialFlow_V");
    str_cmd.AppendLine("where SpecFlowKey = @SpecFlowKey and MaxJobGrade <= @MaxJobGrade");
    str_cmd.AppendLine("order by Series");

    //建立抓組織特殊流程的參數
    SqlParameter[] SP_Dept = {
            new SqlParameter("@SpecFlowKey",DeptID),
            new SqlParameter("@MaxJobGrade",MaxJobGrade)
        };

    //建立抓個人特殊流程的參數
    SqlParameter[] SP_Employee = {
            new SqlParameter("@SpecFlowKey",EmployeeID),
            new SqlParameter("@MaxJobGrade",MaxJobGrade)
        };

    //先處理組織的特殊流程
    dr = MSSQL.GetDataRead(str_cmd.ToString(), SP_Dept, "DB_Tea_VToSchool");
    if (dr.HasRows)
    {
      string temp_allow_AD = "";
      while (dr.Read())
      {
        temp_allow_AD = temp_allow_AD + dr["SignEmployeeID"] + ";";
      }
      allow_AD = temp_allow_AD.Split(';');
    }
    dr.Close();

    //再處理個人的特殊流程
    dr = MSSQL.GetDataRead(str_cmd.ToString(), SP_Employee, "DB_Tea_VToSchool");
    if (dr.HasRows)
    {
      string temp_allow_AD = "";
      while (dr.Read())
      {
        temp_allow_AD = temp_allow_AD + dr["SignEmployeeID"] + ";";
      }
      allow_AD = temp_allow_AD.Split(';');
    }
    dr.Close();

    #region 處理簽核流程中的個人特例
    //for (int i = 0; i < allow_AD.GetLength(0); i++)
    //{
    //    Temp = Temp + allow_AD[i] + ";";
    //}
    //楊曉暉主任原本要排除於流程之外，於2016/07/14註解此段程式碼
    //留著的原因，是將個人流程的紀錄留下，方便下次程式撰寫
    //allow_AD = Temp.Replace("hsiaohue;", "").Split(';');
    #endregion

    return allow_AD;
  }
  public string[] TransformAccountID2EmployeeID(string[] Allow_AD)
  {
    AboutHCP HCP = new AboutHCP();
    string[] Allow_EmployeeID = new string[Allow_AD.GetLength(0)];

    for (int i = 0; i < Allow_AD.GetLength(0); i++)
    {
      Allow_EmployeeID[i] = HCP.GetMemberEmployeeID(Allow_AD[i]);
    }

    return Allow_EmployeeID;
  }
  /// <summary>
  /// 取得人員的組織代碼
  /// </summary>
  /// <param name="EmployeeID"></param>
  /// <returns></returns>
  public string GetHCPEmployeeDeptID(string EmployeeID)
  {
    Operation_MSSQL MSSQL = new Operation_MSSQL();
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select DeptID");
    str_cmd.AppendLine("from dbo.Sys_Interinfo_Person_V");
    str_cmd.AppendLine("where EmployeeID = @EmployeeID ");

    SqlParameter[] SP1 = { new SqlParameter("@EmployeeID", EmployeeID) };

    return MSSQL.GetScalar(str_cmd.ToString(), SP1, "DB_MisAdmin").ToString();
  }
  /// <summary>
  /// 取得通知人員的Email(並用;分隔)
  /// </summary>
  /// <param name="Campus"></param>
  /// <param name="VTypeID"></param>
  /// <returns></returns>
  public string GetNoticeEmail(string Campus, string VTypeID)
  {
    string Email_String = string.Empty;
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());
    cn.Open();
    //---1.帶出Sys_V_Type的資訊 基本資料
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("select EmployeeID");
    str_cmd.AppendLine("from Sys_V_NoticePeople");
    str_cmd.AppendLine("where Campus=@Campus");
    str_cmd.AppendLine("and VTypeID= @VTypeID");

    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@Campus", Campus);
    cmd.Parameters.AddWithValue("@VTypeID", VTypeID);
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
      Email_String = Email_String + GetHCPMail(dr["EmployeeID"].ToString()) + ";";
    }
    dr.Close();
    cmd.Cancel();
    cn.Close();
    cn.Dispose();
    Email_String = Email_String + GetGuardEmail(Campus + "校區").TrimEnd(';');


    return Email_String;
  }
  /// <summary>
  /// 取得通知人員的Email(使用者自己手動新增的)(並用;分隔)
  /// </summary>
  /// <param name="Campus"></param>
  /// <param name="VTypeID"></param>
  /// <returns></returns>
  public string GetNoticeEmail_byUser(string ListNum)
  {
    string Email_String = string.Empty;
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());
    cn.Open();
    //---1.帶出Sys_V_Type的資訊 基本資料
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("select noticeEmployeeID");
    str_cmd.AppendLine("from List_noticePerson");
    str_cmd.AppendLine("where ListNum=@ListNum");

    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum);
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
      Email_String = Email_String + GetHCPMail(dr["noticeEmployeeID"].ToString()) + ";";
    }
    dr.Close();
    cmd.Cancel();
    cn.Close();
    cn.Dispose();
    Email_String = Email_String.TrimEnd(';');
    return Email_String;
  }
  /// <summary>
  /// 取得各校區警衛人員
  /// </summary>
  /// <param name="Campus"></param>
  /// <returns></returns>
  public string GetGuardEmail(string Campus)
  {
    string Email_String = string.Empty;
    string DBname1 = "DB_MisAdmin";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("select email");
    str_cmd.AppendLine("from Sys_Interinfo_Person_V");
    str_cmd.AppendLine("where Campus=@Campus");
    str_cmd.AppendLine("and Title='警衛'");
    str_cmd.AppendLine("and(outdate is null or outdate + 1 > getdate())");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@Campus", Campus);

    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
      Email_String = Email_String + dr["email"].ToString() + ";";
    }
    dr.Close();
    cmd.Cancel();
    cn.Close();
    cn.Dispose();
    return Email_String;
  }
  /// <summary>
  /// 給員編得到HCP的email
  /// </summary>
  /// <param name="EmployeeID"></param>
  /// <returns></returns>
  public string GetHCPMail(string EmployeeID)
  {
    Operation_MSSQL MSSQL = new Operation_MSSQL();
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select email");
    str_cmd.AppendLine("from dbo.Sys_Interinfo_Person_V");
    str_cmd.AppendLine("where EmployeeID = @EmployeeID ");

    SqlParameter[] SP1 = {
                                 new SqlParameter("@EmployeeID",EmployeeID)
                             };

    return MSSQL.GetScalar(str_cmd.ToString(), SP1, "DB_MisAdmin").ToString();
  }
  /// <summary>
  /// 取得FlowNo和Notice
  /// </summary>
  /// <param name="EmployeeID"></param>
  /// <returns></returns>
  public void getFlowNo(string Campus, string VTypeID, out string FlowNo)
  {
    FlowNo = string.Empty;
    string DBName = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBName].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select FlowNo");
    str_cmd.AppendLine("from Sys_V_Type ");
    str_cmd.AppendLine("where Campus=@Campus");
    str_cmd.AppendLine("and VTypeID=@VTypeID");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@Campus", Campus);
    cmd.Parameters.AddWithValue("@VTypeID", VTypeID);
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
      //流程代碼
      FlowNo = dr["FlowNo"].ToString();
    }
    dr.Close();
    cmd.Cancel();
    cn.Close();
    cn.Dispose();
  }
  public void getNotice(string Campus, string VTypeID, out string NoticeName, out string NoticeEmployeeID)
  {
    NoticeName = string.Empty;
    NoticeEmployeeID = string.Empty;
    string DBName = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBName].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select Name,EmployeeID");
    str_cmd.AppendLine("from Sys_V_NoticePeople ");
    str_cmd.AppendLine("where Campus=@Campus");
    str_cmd.AppendLine("and VTypeID=@VTypeID");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@Campus", Campus);
    cmd.Parameters.AddWithValue("@VTypeID", VTypeID);
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
      //通知對象
      NoticeName = NoticeName + dr["Name"].ToString() + "、";
      NoticeEmployeeID = NoticeEmployeeID + dr["EmployeeID"].ToString() + "、";
    }
    dr.Close();
    cmd.Cancel();
    cn.Close();
    cn.Dispose();
    NoticeName = NoticeName.TrimEnd('、');
    NoticeEmployeeID = NoticeEmployeeID.TrimEnd('、');
  }
  public void getNoticeName_byUser(string ListNum, out string NoticeName)
  {
    NoticeName = string.Empty;
    string DBName = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBName].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select noticeName");
    str_cmd.AppendLine("from List_noticePerson");
    str_cmd.AppendLine("where ListNum=@ListNum");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum);
    SqlDataReader dr = cmd.ExecuteReader();
    while (dr.Read())
    {
      //通知對象
      NoticeName = NoticeName + dr["noticeName"].ToString() + "、";
    }
    dr.Close();
    cmd.Cancel();
    cn.Close();
    cn.Dispose();
    NoticeName = NoticeName.TrimEnd('、');
  }
  //public string get_Sys_SetErrorMail(string Campus)
  //{
  //  string DBname = "DB_Tea_HCP";
  //  string MailString = string.Empty;
  //  SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname].ConnectionString.ToString());
  //  cn.Open();
  //  StringBuilder str_cmd = new StringBuilder();
  //  str_cmd.AppendLine("select b.email from Sys_SetErrorMail a inner join DB_Mis_Admin.dbo.Sys_Interinfo_Person_V b on a.EmployeeID=b.EmployeeID where a.Campus=@Campus");
  //  str_cmd.AppendLine("");
  //  SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
  //  cmd.Parameters.AddWithValue("@Campus", Campus);
  //  SqlDataReader dr = cmd.ExecuteReader();
  //  while (dr.Read())
  //  {
  //    MailString += dr["email"].ToString() + ";";
  //  }
  //  dr.Close();
  //  cmd.Cancel();
  //  cn.Close();
  //  cn.Dispose();
  //  return MailString;
  //}
  public StringBuilder mailbody(string ListNum, bool bNotes = false)
  {
    string VTypeID = string.Empty;
    StringBuilder mailBody = new StringBuilder();
    StringBuilder mailBody2 = new StringBuilder();
    StringBuilder mailBody3 = new StringBuilder();
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());
    cn.Open();
    //---1.帶出List_V的資訊 基本資料
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("SELECT a.ListNum,a.Campus,a.EmployeeID,a.Name,DeptName,DeptName,b.VTypeName,a.VTypeID,Purpose,startTime,endTime,Reception,ReceptionLocation, interviewLocation, constructionArea,SpeRequest,Notes");
    str_cmd.AppendLine("from List_V a");
    str_cmd.AppendLine("inner join Sys_V_Type b");
    str_cmd.AppendLine("on a.Campus = b.Campus");
    str_cmd.AppendLine("and a.VTypeID = b.VTypeID");
    str_cmd.AppendLine("where a.ListNum=@ListNum");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum);
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
      mailBody.AppendLine("<table style='border:3px #0c2d4e solid;padding:3px;width:70%' rules='all' cellpadding='5';>");

      mailBody.AppendLine("<tr>");
      mailBody.AppendLine("<td bgcolor='#e2e4f3'>表單編號</td>");
      mailBody.AppendLine("<td>" + dr["ListNum"].ToString() + "</td>");
      mailBody.AppendLine("<td bgcolor='#e2e4f3'>校區</td>");
      mailBody.AppendLine("<td>" + dr["Campus"].ToString() + "</td>");
      mailBody.AppendLine("</tr>");

      mailBody.AppendLine("<tr>");
      mailBody.AppendLine("<td bgcolor='#e2e4f3'>部門</td>");
      mailBody.AppendLine("<td>" + dr["DeptName"].ToString() + "</td>");
      mailBody.AppendLine("<td bgcolor='#e2e4f3'>員編</td>");
      mailBody.AppendLine("<td>" + dr["EmployeeID"].ToString() + "</td>");
      mailBody.AppendLine("</tr>");

      mailBody.AppendLine("<tr>");
      mailBody.AppendLine("<td bgcolor='#e2e4f3'>申請人</td>");
      mailBody.AppendLine("<td>" + dr["Name"].ToString() + "</td>");
      mailBody.AppendLine("<td bgcolor='#e2e4f3'>訪客類別</td>");
      mailBody.AppendLine("<td style='color:red'>" + dr["VTypeName"].ToString() + "</td>");
      mailBody.AppendLine("</tr>");

      ////
      mailBody3.AppendLine("<tr>");
      mailBody3.AppendLine("<td bgcolor='#e2e4f3'>入校目的</td>");
      mailBody3.AppendLine("<td>" + dr["Purpose"].ToString() + "</td>");
      mailBody3.AppendLine("<td bgcolor='#e2e4f3'>接待單位</td>");
      mailBody3.AppendLine("<td>" + dr["Reception"].ToString() + "</td>");
      mailBody3.AppendLine("</tr>");

      mailBody3.AppendLine("<tr>");
      mailBody3.AppendLine("<td bgcolor='#e2e4f3'>預定入校時間</td>");
      mailBody3.AppendLine("<td style='color:red'>" + dr["startTime"].ToString() + "</td>");
      mailBody3.AppendLine("<td bgcolor='#e2e4f3'>預定離校時間</td>");
      mailBody3.AppendLine("<td style='color:red'>" + dr["endTime"].ToString() + "</td>");
      mailBody3.AppendLine("</tr>");

      mailBody3.AppendLine("<tr>");
      mailBody3.AppendLine("<td bgcolor='#e2e4f3'>接待地點</td>");
      mailBody3.AppendLine("<td>" + dr["ReceptionLocation"].ToString() + "</td>");
      mailBody3.AppendLine("<td bgcolor='#e2e4f3'>採訪地點</td>");
      mailBody3.AppendLine("<td>" + dr["VTypeName"].ToString() + "</td>");
      mailBody3.AppendLine("</tr>");

      mailBody3.AppendLine("<tr>");
      mailBody3.AppendLine("<td bgcolor='#e2e4f3'>特殊需求</td>");
      mailBody3.AppendLine("<td>" + dr["SpeRequest"].ToString() + "</td>");
      mailBody3.AppendLine("<td bgcolor='#e2e4f3'>注意事項</td>");
      if (bNotes)
        mailBody3.AppendLine("<td style='color:red'>" + dr["Notes"].ToString() + "</td>");
      else
        mailBody3.AppendLine("<td>" + dr["Notes"].ToString() + "</td>");
      mailBody3.AppendLine("</tr>");

      VTypeID = dr["VTypeID"].ToString();
    }
    dr.Close();
    cmd.Cancel();

    str_cmd.Length = 0;
    switch (VTypeID)
    {
      case "1": //一般訪客
        str_cmd.AppendLine("select * from 一般訪客子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "2": //外校生家長
        str_cmd.AppendLine("select * from 外校生家長子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "3": //政府機關
        str_cmd.AppendLine("select * from 政府機關子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "4": //媒體
        str_cmd.AppendLine("select * from 媒體子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "5": //在籍學生家長
        str_cmd.AppendLine("select * from 在籍學生家長子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "6": //校友
        str_cmd.AppendLine("select * from 校友子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "7": //廠商
        str_cmd.AppendLine("select * from 廠商子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "8": //團體訪客
        str_cmd.AppendLine("select * from 團體訪客子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "9": //學校活動
        str_cmd.AppendLine("select * from 學校活動子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
      case "10": //假日到校
        str_cmd.AppendLine("select * from 假日到校子表");
        str_cmd.AppendLine("where Listnum=@ListNum");
        break;
    }
    cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum);
    dr = cmd.ExecuteReader();
    if (dr.Read())
    {
      switch (VTypeID)
      {
        case "1": //一般訪客
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>訪客姓名</td>");
          mailBody2.AppendLine("<td>" + dr["VName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "2": //外校生家長
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>家長姓名</td>");
          mailBody2.AppendLine("<td>" + dr["parentName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>學生姓名</td>");
          mailBody2.AppendLine("<td>" + dr["stuName"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'></td>");
          mailBody2.AppendLine("<td></td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "3": //政府機關
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>機關名稱</td>");
          mailBody2.AppendLine("<td>" + dr["governmentName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>領隊姓名</td>");
          mailBody2.AppendLine("<td>" + dr["leaderName"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'></td>");
          mailBody2.AppendLine("<td></td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "4": //媒體
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>媒體名稱</td>");
          mailBody2.AppendLine("<td>" + dr["mediaName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>姓名</td>");
          mailBody2.AppendLine("<td>" + dr["Name"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'></td>");
          mailBody2.AppendLine("<td></td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "5": //在籍學生家長
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>學生姓名</td>");
          mailBody2.AppendLine("<td>" + dr["stuName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>學生班級</td>");
          mailBody2.AppendLine("<td>" + dr["stuClass"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>學生學號</td>");
          mailBody2.AppendLine("<td>" + dr["stuNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>家長姓名</td>");
          mailBody2.AppendLine("<td>" + dr["parentName"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'></td>");
          mailBody2.AppendLine("<td></td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "6": //校友
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>校友姓名</td>");
          mailBody2.AppendLine("<td>" + dr["alumni"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>在學學號</td>");
          mailBody2.AppendLine("<td>" + dr["stuNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'></td>");
          mailBody2.AppendLine("<td></td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "7": //廠商
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>廠商名稱</td>");
          mailBody2.AppendLine("<td>" + dr["companyName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "8": //團體訪客
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>團體名稱</td>");
          mailBody2.AppendLine("<td>" + dr["groupName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>領隊姓名</td>");
          mailBody2.AppendLine("<td>" + dr["leaderName"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'></td>");
          mailBody2.AppendLine("<td></td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "9": //學校活動
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>訪客姓名</td>");
          mailBody2.AppendLine("<td>" + dr["VName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");
          break;
        case "10": //假日到校
          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>教職員姓名</td>");
          mailBody2.AppendLine("<td>" + dr["VName"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>入校人數</td>");
          mailBody2.AppendLine("<td>" + dr["VNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");

          mailBody2.AppendLine("<tr>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>車號</td>");
          mailBody2.AppendLine("<td>" + dr["carNum"].ToString() + "</td>");
          mailBody2.AppendLine("<td bgcolor='#e2e4f3'>聯絡電話</td>");
          mailBody2.AppendLine("<td>" + dr["phoneNum"].ToString() + "</td>");
          mailBody2.AppendLine("</tr>");
          break;
      }
    }

    mailBody.AppendLine(mailBody2.ToString());
    mailBody.AppendLine(mailBody3.ToString());
    mailBody.AppendLine("</table>");

    cn.Close();
    cn.Dispose();
    return mailBody;
  }
}