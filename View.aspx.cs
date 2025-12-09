using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class View : System.Web.UI.Page
{
  readonly ClassBasic basic = new ClassBasic();
  readonly AboutMail MAIL = new AboutMail();
  readonly VtoSchool VtoSchool = new VtoSchool();

  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["EmployeeID"] == null)
    {
      Response.Redirect("Logon.aspx?" + Request.QueryString.ToString() + "&page=View");
      return;
    }
    if (!IsPostBack)
    {
      Session["FileTable"] = null;
      if (Session["EmployeeID"] == null)
      {
        Response.Redirect("Logon.aspx?" + Request.QueryString.ToString() + "&page=View");
        return;
      }
      if (Request.QueryString["num"] == null)
      {
        Response.Redirect("Logon.aspx");
        return;
      }
      string num = Request.QueryString["num"].ToString();
      ListNum.Text = num;

      //帶出表單基本資料
      showFormInfo();
      //帶出訪客資料
      if (VTypeID.Text.Length > 0)
      {
        showVInfo();
        //帶出通知對象
        showNoticeName();
        //帶出手動新增的通知對象
        showNoticeName_byUser();
        //帶出訪客panel開合
        showInfoPanel();
        //顯示附檔
        showFile();
        //顯示簽核列表
        ShowAllowList();
        //不同的身分(主管,業管,會簽)看到的區塊不一樣
        showPanel();
        ////是否可以編輯入校記錄及新增附件 以及修改入校資料
        if (Session["EmployeeID"].ToString() == sno.Text && state.Text == "已核准")
        {
          Panel7.Visible = true;
          DetailsView1.Rows[5].Visible = true;
          LinkButton1.Visible = true;
          if (intoSchool.Text == "")
          {
            LinkButton2.Visible = true;
          }
          else
          {
            LinkButton2.Visible = false;
          }
        }
        else
        {
          Panel7.Visible = false;
          DetailsView1.Rows[5].Visible = false;
          LinkButton1.Visible = false;
          LinkButton2.Visible = false;
        }
      }
      else
      {
        Panel1.Visible = false;
        Panel2.Visible = false;
        Panel6.Visible = false;
        LinkButton1.Visible = false;
        LinkButton2.Visible = false;
        Label12.Text = "查無資料";
      }
    }
    //上傳檔案
    Button_FileUpload1.Attributes["style"] = "display: none;";
    _myTableBind();
  }
  protected void showFormInfo()
  {
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings
[DBname1].ConnectionString.ToString());
    cn.Open();
    //---1.帶出List_V的資訊 基本資料
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("select a.EmployeeID,a.DeptName,a.DeptID,a.Name,a.Apply_Datetime,a.VTypeID,a.Purpose,a.startTime,a.endTime,a.Reception,a.ReceptionLocation,a.interviewLocation,a.constructionArea,a.SpeRequest,a.Notes,a.C_Level,a.T_Level,a.intoSchoolStatus,a.Campus as Campus2,");
    if (Session["UserCulture"].ToString() == "中文")
    {
      str_cmd.AppendLine("c.VTypeName");
    }
    else
    {
      str_cmd.AppendLine("c.VTypeName");
    }
    str_cmd.AppendLine(",case a.Campus when '秀岡' then '" + Resources.Resource.Xiugang
                                      + "' when '青山' then '" + Resources.Resource.Qingshan
                                      + "' when '林口' then '" + Resources.Resource.Linko
                                      + "' when '新竹' then '" + Resources.Resource.Hsinchu
                                      + "' end as Campus");
    str_cmd.AppendLine(",case a.ListStatus when '審核中' then '" + Resources.Resource.WaitingForApproval
                                     + "' when '已核准' then '" + Resources.Resource.Approved
                                     + "' when '駁回' then '" + Resources.Resource.Rejected
                                     + "' when '管理者撤銷' then '" + Resources.Resource.AdminDelete
                                     + "' end as ListStatus");
    str_cmd.AppendLine("from List_V a");
    str_cmd.AppendLine("inner join DB_Mis_Admin.dbo.Sys_Interinfo_Person_V b");
    str_cmd.AppendLine("on a.EmployeeID = b.EmployeeID");
    str_cmd.AppendLine("inner join Sys_V_Type c");
    str_cmd.AppendLine("on a.VTypeID= c.VTypeID");
    str_cmd.AppendLine("and a.Campus=c.Campus");
    str_cmd.AppendLine("where a.ListNum=@ListNum");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
      sno.Text = dr["EmployeeID"].ToString();
      Campus.Text = dr["Campus"].ToString();
      Campus2.Text = dr["Campus2"].ToString().Substring(0, 2).ToString();
      deptName.Text = dr["DeptName"].ToString();
      deptCode.Text = dr["DeptID"].ToString();
      name.Text = dr["Name"].ToString();
      VTypeName.Text = dr["VTypeName"].ToString();
      VTypeID.Text = dr["VTypeID"].ToString();
      applyTime.Text = dr["Apply_Datetime"].ToString();
      //入校資料
      TextBox8.Text = dr["Purpose"].ToString();
      Label142.Text = Convert.ToDateTime(dr["startTime"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
      Label143.Text = Convert.ToDateTime(dr["endTime"].ToString()).ToString("yyyy/MM/dd HH:mm:ss");
      Label144.Text = dr["Reception"].ToString();
      Label8.Text = dr["ReceptionLocation"].ToString();
      Label145.Text = dr["interviewLocation"].ToString();
      Label146.Text = dr["constructionArea"].ToString();
      Label147.Text = dr["SpeRequest"].ToString();
      TextBox7.Text = dr["Notes"].ToString();
      state.Text = dr["ListStatus"].ToString();
      C_level.Text = dr["C_Level"].ToString();
      T_level.Text = dr["T_Level"].ToString();
      intoSchool.Text = dr["intoSchoolStatus"].ToString();
    }
    dr.Close();
    cmd.Cancel();
    cn.Close();
    cn.Dispose();
    string[] speRequest = Label147.Text.Split(';');
    for (int i = 0; i < CheckBoxList1.Items.Count; i++)
    {
      for (int j = 0; j < speRequest.Length; j++)
      {
        if (CheckBoxList1.Items[i].Value == speRequest[j])
        {
          CheckBoxList1.Items[i].Selected = true;
        }
      }
    }
    CheckBoxList1.Enabled = false;
  }
  protected void showVInfo()
  {
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    switch (VTypeID.Text)
    {
      case "1": //一般訪客
        str_cmd.AppendLine("select * from 一般訪客子表 where ListNum=@ListNum");
        break;
      case "2": //外校生家長
        str_cmd.AppendLine("select * from 外校生家長子表 where ListNum=@ListNum");
        break;
      case "3": //政府機關
        str_cmd.AppendLine("select * from 政府機關子表 where ListNum=@ListNum");
        break;
      case "4": //媒體
        str_cmd.AppendLine("select * from 媒體子表 where ListNum=@ListNum");
        break;
      case "5": //在籍學生家長
        str_cmd.AppendLine("select * from 在籍學生家長子表 where ListNum=@ListNum");
        break;
      case "6": //校友
        str_cmd.AppendLine("select * from 校友子表 where ListNum=@ListNum");
        break;
      case "7": //廠商
        str_cmd.AppendLine("select * from 廠商子表 where ListNum=@ListNum");
        break;
      case "8": //團體訪客
        str_cmd.AppendLine("select * from 團體訪客子表 where ListNum=@ListNum");
        break;
      case "9": //學校活動
        str_cmd.AppendLine("select * from 學校活動子表 where ListNum=@ListNum");
        break;
      case "10": //假日到校
        str_cmd.AppendLine("select * from 假日到校子表 where ListNum=@ListNum");
        break;
    }

    if (str_cmd.Length > 0)
    {
      SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
      cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
      SqlDataReader dr = cmd.ExecuteReader();
      if (dr.Read())
      {
        switch (VTypeID.Text)
        {
          case "1": //一般訪客
            Label101.Text = dr["VName"].ToString();
            Label102.Text = dr["VNum"].ToString();
            Label103.Text = dr["carNum"].ToString();
            Label104.Text = dr["phoneNum"].ToString();
            break;
          case "2": //外校生家長
            Label105.Text = dr["parentName"].ToString();
            Label106.Text = dr["stuName"].ToString();
            Label107.Text = dr["VNum"].ToString();
            Label108.Text = dr["carNum"].ToString();
            Label109.Text = dr["phoneNum"].ToString();
            break;
          case "3": //政府機關
            Label110.Text = dr["governmentName"].ToString();
            Label111.Text = dr["leaderName"].ToString();
            Label112.Text = dr["VNum"].ToString();
            Label113.Text = dr["carNum"].ToString();
            Label114.Text = dr["phoneNum"].ToString();
            break;
          case "4": //媒體
            Label115.Text = dr["mediaName"].ToString();
            Label116.Text = dr["Name"].ToString();
            Label117.Text = dr["VNum"].ToString();
            Label118.Text = dr["carNum"].ToString();
            Label119.Text = dr["phoneNum"].ToString();
            break;
          case "5": //在籍學生家長
            Label120.Text = dr["stuName"].ToString();
            Label121.Text = dr["stuClass"].ToString();
            Label122.Text = dr["stuNum"].ToString();
            Label123.Text = dr["parentName"].ToString();
            Label124.Text = dr["VNum"].ToString();
            Label125.Text = dr["carNum"].ToString();
            Label126.Text = dr["phoneNum"].ToString();
            break;
          case "6": //校友
            Label127.Text = dr["alumni"].ToString();
            Label128.Text = dr["stuNum"].ToString();
            Label129.Text = dr["VNum"].ToString();
            Label130.Text = dr["carNum"].ToString();
            Label131.Text = dr["phoneNum"].ToString();
            break;
          case "7": //廠商
            Label132.Text = dr["companyName"].ToString();
            Label133.Text = dr["VNum"].ToString();
            Label134.Text = dr["carNum"].ToString();
            Label135.Text = dr["phoneNum"].ToString();
            break;
          case "8": //團體訪客
            Label136.Text = dr["groupName"].ToString();
            Label137.Text = dr["leaderName"].ToString();
            Label138.Text = dr["VNum"].ToString();
            Label139.Text = dr["carNum"].ToString();
            Label140.Text = dr["phoneNum"].ToString();
            break;
          case "9": //學校活動
            Label16.Text = dr["VName"].ToString();
            Label22.Text = dr["VNum"].ToString();
            Label24.Text = dr["carNum"].ToString();
            Label26.Text = dr["phoneNum"].ToString();
            break;
          case "10": //假日到校
            Label29.Text = dr["VName"].ToString();
            Label32.Text = dr["VNum"].ToString();
            Label34.Text = dr["carNum"].ToString();
            Label39.Text = dr["phoneNum"].ToString();
            break;
        }
      }
      dr.Close();
      cmd.Cancel();
    }

    cn.Close();
    cn.Dispose();
  }
  //訪客資料 入校資料Panel開合
  protected void showInfoPanel()
  {
    Panel_01.Visible = false;
    Panel_02.Visible = false;
    Panel_03.Visible = false;
    Panel_04.Visible = false;
    Panel_05.Visible = false;
    Panel_06.Visible = false;
    Panel_07.Visible = false;
    Panel_08.Visible = false;
    Panel_09.Visible = false;
    Panel_10.Visible = false;
    Panel_11.Visible = false;
    Panel_12.Visible = false;
    switch (VTypeID.Text)
    {
      case "1": //一般訪客
        Panel_01.Visible = true;
        break;
      case "2": //外校生家長
        Panel_02.Visible = true;
        break;
      case "3": //政府機關
        Panel_03.Visible = true;
        break;
      case "4": //媒體
        Panel_04.Visible = true;
        Panel_09.Visible = true; //採訪地點
        break;
      case "5": //在籍學生家長
        Panel_05.Visible = true;
        break;
      case "6": //校友
        Panel_06.Visible = true;
        break;
      case "7": //廠商
        Panel_07.Visible = true;
        Panel_10.Visible = true; //施工區域
        break;
      case "8": //團體訪客
        Panel_08.Visible = true;
        break;
      case "9": //學校活動
        Panel_11.Visible = true;
        break;
      case "10": //假日到校
        Panel_12.Visible = true;
        break;
    }
  }
  protected void showFile()
  {
    Operation_MSSQL DBOp = new Operation_MSSQL();
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings
[DBname1].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    //附加檔案列表
    str_cmd.AppendLine("SELECT * FROM List_File WITH(NOLOCK) WHERE ListNum='" + ListNum.Text + "'");
    DataTable FileTable = DBOp.GetDataTable(str_cmd.ToString(), DBname1);

    int RowCount = 1;
    PlaceHolderAttachment3.Controls.Clear();
    if (FileTable.Rows.Count > 0)
    {
      string FileName, GuidName;

      foreach (DataRow myRow in FileTable.Rows)
      {
        FileName = myRow["FileName"].ToString().Trim();
        GuidName = myRow["GuidName"].ToString().Trim();

        PlaceHolderAttachment3.Controls.Add(new LiteralControl(RowCount.ToString() + ". <a href=\"UploadFiles/" + GuidName + "\" target=\"_blank\">" + FileName + "</a>"));
        PlaceHolderAttachment3.Controls.Add(new LiteralControl("<br />"));

        RowCount++;
      }
    }
    else
    {
      PlaceHolderAttachment3.Controls.Add(new LiteralControl("<span style=\"font-size:13px; color:#ff00ff;\">&nbsp;&nbsp;&nbsp;&nbsp;" + Resources.Resource.NoProof2 + "</span>"));
    }
  }
  protected void ShowAllowList()
  {
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings
[DBname1].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("select EmployeeID,Name,SignTime,SignContent,Series");
    str_cmd.AppendLine(", case AllowStatus when '0' then '" + Resources.Resource.Already_r
                                      + "' when '1' then '" + Resources.Resource.Already_a
                                      + "' when '2' then '" + Resources.Resource.Already_t
                                      + "' when '3' then '" + Resources.Resource.Already_d
                                      + "' end as AllowStatus");
    str_cmd.AppendLine("from dbo.List_Allow");
    str_cmd.AppendLine("where ListNum = @ListNum");
    str_cmd.AppendLine("order by Series");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    SqlDataReader dr = cmd.ExecuteReader();
    str_cmd.Length = 0;
    StringBuilder str_table = new StringBuilder();
    str_table.AppendLine("<table style='width: 90%; font-family: 微軟正黑體; font-size: small; border-top:1px solid #c3c3c3;border-bottom:1px solid #c3c3c3;' cellpadding='0' cellspacing='0' rules='all'>");
    string String_nowAllow = "";
    string String_nowAllowName = "";
    if (dr.HasRows)
    {
      while (dr.Read())
      {

        //記錄正在審核人員編及姓名
        if (Convert.ToInt32(C_level.Text) + 1 == Convert.ToInt32(dr["Series"].ToString()))
        {
          String_nowAllow += dr["EmployeeID"].ToString() + ";";
          String_nowAllowName += dr["Name"].ToString() + ";";
        }
        //顯示簽核列表左半部籤核者
        str_table.AppendLine("<tr style='border-right:1px solid #c3c3c3;'height='30'>");
        if (Convert.ToInt32(dr["Series"].ToString()) % 2 == 0) //偶數
        {
          str_table.AppendLine("<td width=25%' align='center' bgcolor='#fbe3dc'>" + dr["Name"].ToString());
        }
        else //單數
        {
          str_table.AppendLine("<td width=25%' align='center' bgcolor='#f2ffec'>" + dr["Name"].ToString());
        }
        str_table.AppendLine("</td>");
        str_table.AppendLine("<td width=75%>  ");
        //顯示簽核列表右半部(簽核狀況)
        if (dr["AllowStatus"].ToString() != "")
        {
          str_table.AppendLine("&nbsp &nbsp" + dr["Name"].ToString() + "&nbsp &nbsp");
          str_table.AppendLine(dr["AllowStatus"].ToString() + "&nbsp &nbsp");
          if (dr["SignTime"].ToString() == "")
          {
            str_table.AppendLine("");
          }
          else
          {
            str_table.AppendLine(Convert.ToDateTime(dr["SignTime"].ToString()).ToString("yyyy/MM/dd HH:mm:ss") + "</br>");
          }
          if (dr["SignContent"].ToString() == "")
          {
            str_table.AppendLine("");
          }
          else
          {
            string allow_ext = dr["SignContent"].ToString();
            allow_ext = allow_ext.Replace("\r\n", "</br>");
            str_table.AppendLine("&nbsp &nbsp" + allow_ext);
          }
          str_table.AppendLine("</br>");
        }
        str_table.AppendLine("</td>");
        str_table.AppendLine("</tr>");
      }
    }
    else
    {
      str_table.AppendLine("<tr style='border-right:1px solid #c3c3c3;'height='30'>");
      str_table.AppendLine("<td align='center' bgcolor='#fbe3dc'>無需簽核，直接通過");
      str_table.AppendLine("</td>");
      str_table.AppendLine("</tr>");
    }
    str_table.AppendLine("</table>");
    Literal1.Text = str_table.ToString();
    nowAllow.Text = String_nowAllow.TrimEnd(';');
    nowAllowName.Text = String_nowAllowName.Trim(';');
    cn.Close();
    cn.Dispose();

    //若假單狀態是管理者撤銷，則不能按撤銷按鈕
    if (state.Text.Contains("已核准") || state.Text.Contains("審核中") || state.Text.Contains("Approved") || state.Text.Contains("Waiting"))
    {
      Button1.Enabled = true;
    }
    else
    {
      Button1.Enabled = false;
    }
    //若假單狀態不是審核中，隱藏nowAllowName.Text
    if (state.Text.Contains("審核中") || state.Text.Contains("Waiting"))
    {
      nowAllowName.Visible = true;
      Panel2.Visible = false;
    }
    else
    {
      nowAllowName.Visible = false;
      Panel2.Visible = true;
    }
  }
  protected void showPanel()
  {
    //如果登入者為現階段評核者就開啟panel1
    if (nowAllow.Text.Contains(Session["EmployeeID"].ToString()) && !state.Text.Contains("撤銷"))
    {
      Panel1.Visible = true; //簽核意見區塊
    }
    else
    {
      Panel1.Visible = false;
    }
    if (Session["AdminLevel"].ToString() == "管理")
    {
      Button1.Text = "管理者撤銷";
      Panel6.Visible = true;
    }
    else if (Session["EmployeeID"].ToString() == sno.Text)
    {
      Button1.Text = "撤銷";
      Panel6.Visible = true;
    }
    else
    {
      Panel6.Visible = false;
    }
  }
  protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
  {
    bool isOK = true;
    string DBname1 = "DB_Tea_VToSchool";
    //為避免簽何者同時開兩張頁面做簽核所做的CHECK
    SqlConnection cn2 = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());
    cn2.Open();
    StringBuilder str_cmd2 = new StringBuilder();
    str_cmd2.AppendLine("select C_Level+1 as count from List_V");
    str_cmd2.AppendLine("where ListNum=@ListNum");

    SqlCommand cmd2 = new SqlCommand(str_cmd2.ToString(), cn2);
    cmd2.Parameters.AddWithValue("@ListNum", ListNum.Text);
    SqlDataReader dr2 = cmd2.ExecuteReader();
    if (dr2.Read())
    {
      if (dr2["count"].ToString() != (Convert.ToInt32(C_level.Text) + 1).ToString())
      {
        isOK = false;
      }
    }
    dr2.Close();
    cmd2.Cancel();
    cn2.Close();
    cn2.Dispose();
    if (isOK == false)
    {
      basic.Script_AlertMsg(this.Page, "此份表單已簽過，請勿重複簽核。");
      return;
    }
    string mailSubject = "";
    string mailto = "";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings
[DBname1].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("declare @chk tinyint;");
    str_cmd.AppendLine("set @chk=0;");
    str_cmd.AppendLine("Begin Transaction [Table]");

    //更新List_Allow
    str_cmd.AppendLine("update List_Allow");
    str_cmd.AppendLine("set SignTime=@SignTime,SignContent=@SignContent,");
    str_cmd.AppendLine("AllowStatus='1'");//同意AllowStatus 1
    str_cmd.AppendLine("where ListNum=@ListNum and Series=@C_Level");
    str_cmd.AppendLine("and EmployeeID=@EmployeeID");
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");


    if (Convert.ToInt32(C_level.Text) + 1 == Convert.ToInt32(T_level.Text))
    {
      mailto = "first";

      str_cmd.AppendLine("update List_V");
      str_cmd.AppendLine("set C_Level=@C_Level,ListStatus='已核准'");
      str_cmd.AppendLine("where ListNum=@ListNum");
      str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");
      mailSubject = name.Text + "提出的訪客入校單已核准。" + name.Text + "'s application has been approved." + ListNum.Text;
    }
    else
    {
      mailto = "next";

      str_cmd.AppendLine("update List_V");
      str_cmd.AppendLine("set C_Level=@C_Level");
      str_cmd.AppendLine("where ListNum=@ListNum");
      str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");
      mailSubject = "請簽核" + name.Text + "提出的訪客入校單 Please approve " + name.Text + "'s application." + ListNum.Text;
    }

    if (mailto == "next")
    {
      //找出下一關簽核者mail
      str_cmd.AppendLine("select email");
      str_cmd.AppendLine("from DB_Mis_Admin.dbo.Sys_Interinfo_Person_V");
      str_cmd.AppendLine("where EmployeeID in (");
      str_cmd.AppendLine("    select a.EmployeeID");
      str_cmd.AppendLine("    from List_Allow a");
      str_cmd.AppendLine("    inner join List_V b");
      str_cmd.AppendLine("    on a.ListNum = b.ListNum");
      str_cmd.AppendLine("    and a.Series = b.C_Level + 1");
      str_cmd.AppendLine("    where a.ListNum = @ListNum");
      str_cmd.AppendLine(")");
    }
    else if (mailto == "first")
    {
      str_cmd.AppendLine("select email");
      str_cmd.AppendLine("from DB_Mis_Admin.dbo.Sys_Interinfo_Person_V");
      str_cmd.AppendLine("where EmployeeID=@apply_EmployeeID");
    }

    str_cmd.AppendLine("if @chk<>0 Begin");
    str_cmd.AppendLine("  Rollback Transaction [Table]");
    str_cmd.AppendLine("end");
    str_cmd.AppendLine("else Begin");
    str_cmd.AppendLine("  Commit Transaction [Table]");
    str_cmd.AppendLine("End");

    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    SqlDataReader dr;
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    cmd.Parameters.AddWithValue("@SignTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    cmd.Parameters.AddWithValue("@SignContent", TextBox3.Text);
    cmd.Parameters.AddWithValue("@C_Level", Convert.ToInt32(C_level.Text) + 1);
    cmd.Parameters.AddWithValue("@apply_EmployeeID", sno.Text);
    cmd.Parameters.AddWithValue("@EmployeeID", Session["EmployeeID"].ToString());

    try
    {
      dr = cmd.ExecuteReader();
      str_cmd.Length = 0;
      string email = string.Empty;
      StringBuilder mailBody = new StringBuilder();
      StringBuilder mailInfo = VtoSchool.mailbody(ListNum.Text);
      mailBody.AppendLine(mailInfo.ToString());
      mailBody.AppendLine("請點選下列連結，開啟審核文件:<br>Please Click The Link Below : <br><br>");
      mailBody.AppendLine("<a href='http://w2.kcbs.ntpc.edu.tw/VToSchool/View.aspx?num=" + ListNum.Text + "'>康橋國際學校-訪客入校系統</a><br>");
      if (mailto == "first")
      {
        dr.Read();
        email = dr["email"].ToString();
        try
        {
          //審核過的單要CC給通知對象
          string CCto = VtoSchool.GetNoticeEmail(Campus.Text, VTypeID.Text);
          if (Campus2.Text.Substring(0, 2).ToString() == "秀岡" && CheckBoxList1.SelectedValue.Contains("用餐"))
          {
            CCto += ";verashih@kcis.ntpc.edu.tw";
          }

          string CCto_byUser = VtoSchool.GetNoticeEmail_byUser(ListNum.Text);
          MAIL.Send_Mail(Request.Url.Host, mailBody.ToString(), mailSubject, email, CCto + ";" + CCto_byUser);
          //審核完成！
          basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg14, "NotSignList.aspx");
        }
        catch (Exception ex)
        {
          string getITMail = ConfigurationManager.AppSettings["ErrorMail"];
          MAIL.Send_Mail(Request.Url.Host, mailBody.ToString() + ex.ToString(), "【" + Campus2.Text + "校區】" + mailSubject + " 發信異常", getITMail, ConfigurationManager.AppSettings["AdminMail"]);
          basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg15, "NotSignList.aspx");
        }
      }
      else
      {
        while (dr.Read())
        {
          email = dr["email"].ToString();
          try
          {
            MAIL.Send_Mail(Request.Url.Host, mailBody.ToString(), mailSubject, email);
            //審核完成！
            basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg14, "NotSignList.aspx");
          }
          catch (Exception ex)
          {
            string getITMail = ConfigurationManager.AppSettings["ErrorMail"];
            MAIL.Send_Mail(Request.Url.Host, mailBody.ToString() + ex.ToString(), "【" + Campus2.Text + "校區】" + mailSubject + " 發信異常", getITMail);
            basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg15, "NotSignList.aspx");
          }
        }
      }
      dr.Close();
    }
    catch (Exception ex)
    {
      //執行資料庫異常，請洽資訊組。
      basic.Script_AlertMsg(this.Page, string.Format("{0}\n{1}", Resources.Resource.ErrorMsg8, ex.Message));
    }
    finally
    {
      cmd.Cancel();
      cn.Close();
      cn.Dispose();
    }
  }
  protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
  {
    bool isOK = true;
    string DBname1 = "DB_Tea_VToSchool";
    //為避免簽何者同時開兩張頁面做簽核所做的CHECK
    SqlConnection cn2 = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());
    cn2.Open();
    StringBuilder str_cmd2 = new StringBuilder();
    str_cmd2.AppendLine("select C_Level+1 as count from List_V");
    str_cmd2.AppendLine("where ListNum=@ListNum");

    SqlCommand cmd2 = new SqlCommand(str_cmd2.ToString(), cn2);
    cmd2.Parameters.AddWithValue("@ListNum", ListNum.Text);
    SqlDataReader dr2 = cmd2.ExecuteReader();
    if (dr2.Read())
    {
      if (dr2["count"].ToString() != (Convert.ToInt32(C_level.Text) + 1).ToString())
      {
        isOK = false;
      }
    }
    dr2.Close();
    cmd2.Cancel();
    cn2.Close();
    cn2.Dispose();
    if (isOK == false)
    {
      basic.Script_AlertMsg(this.Page, "此份表單已簽過，請勿重複簽核。");
      return;
    }
    string mailSubject = "";
    string mailto = "";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings
[DBname1].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("declare @chk tinyint;");
    str_cmd.AppendLine("set @chk=0;");
    str_cmd.AppendLine("Begin Transaction [Table]");

    //更新List_Allow
    str_cmd.AppendLine("update List_Allow");
    str_cmd.AppendLine("set SignTime=@SignTime,SignContent=@SignContent,");
    str_cmd.AppendLine("AllowStatus='0'");//駁回AllowStatus 0
    str_cmd.AppendLine("where ListNum=@ListNum and Series=@C_Level");
    str_cmd.AppendLine("and EmployeeID=@EmployeeID");
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");

    //更新List_V
    str_cmd.AppendLine("update List_V");
    str_cmd.AppendLine("set C_Level=@C_Level,");
    str_cmd.AppendLine("ListStatus='駁回'");
    str_cmd.AppendLine("where ListNum=@ListNum");
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");
    mailto = "first";//寄給第一關
    mailSubject = "您提出的訪客入校單已被駁回。 Your application has been rejected." + ListNum.Text;

    if (mailto == "first")
    {
      str_cmd.AppendLine("select email");
      str_cmd.AppendLine("from DB_Mis_Admin.dbo.Sys_Interinfo_Person_V");
      str_cmd.AppendLine("where EmployeeID=@apply_EmployeeID");
    }

    str_cmd.AppendLine("if @chk<>0 Begin");
    str_cmd.AppendLine("  Rollback Transaction [Table]");
    str_cmd.AppendLine("end");
    str_cmd.AppendLine("else Begin");
    str_cmd.AppendLine("  Commit Transaction [Table]");
    str_cmd.AppendLine("End");

    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    SqlDataReader dr;
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    cmd.Parameters.AddWithValue("@SignTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    cmd.Parameters.AddWithValue("@SignContent", TextBox3.Text);
    cmd.Parameters.AddWithValue("@C_Level", Convert.ToInt32(C_level.Text) + 1);
    cmd.Parameters.AddWithValue("@apply_EmployeeID", sno.Text);
    cmd.Parameters.AddWithValue("@EmployeeID", Session["EmployeeID"].ToString());

    try
    {
      dr = cmd.ExecuteReader();
      str_cmd.Length = 0;
      while (dr.Read())
      {
        string email = dr["email"].ToString();
        StringBuilder mailBody = new StringBuilder();
        StringBuilder mailInfo = VtoSchool.mailbody(ListNum.Text);
        mailBody.AppendLine(mailInfo.ToString());
        mailBody.AppendLine("請點選下列連結，開啟審核文件:<br>Please Click The Link Below : <br><br>");
        mailBody.AppendLine("<a href='http://w2.kcbs.ntpc.edu.tw/VToSchool/View.aspx?num=" + ListNum.Text + "'>康橋國際學校-訪客入校系統</a><br>");
        try
        {
          MAIL.Send_Mail(Request.Url.Host, mailBody.ToString(), mailSubject, email);
          //已駁回！
          basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg16, "NotSignList.aspx");
        }
        catch (Exception ex)
        {
          //string getITMail = VtoSchool.get_Sys_SetErrorMail(Campus2.Text);
          string getITMail = ConfigurationManager.AppSettings["ErrorMail"];
          MAIL.Send_Mail(Request.Url.Host, mailBody.ToString() + ex.ToString(), "【" + Campus2.Text + "校區】" + mailSubject + " 發信異常", getITMail, ConfigurationManager.AppSettings["AdminMail"]);
          basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg17, "NotSignList.aspx");
        }
      }
      dr.Close();
    }
    catch (Exception ex)
    {
      //執行資料庫異常，請洽資訊組。
      basic.Script_AlertMsg(this.Page, string.Format("{0}\n{1}", Resources.Resource.ErrorMsg8, ex.Message));
      return;
    }
    finally
    {
      cmd.Cancel();
      cn.Close();
      cn.Dispose();
    }
  }
  //撤銷
  protected void Button1_Click(object sender, EventArgs e)
  {
    string mailSubject;
    string mailto;
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("declare @chk tinyint;");
    str_cmd.AppendLine("set @chk=0;");
    str_cmd.AppendLine("Begin Transaction [Table]");

    //新增 List_Allow
    str_cmd.AppendLine("insert into List_Allow(ListNum,EmployeeID,Name,SignTime,AllowStatus,Series)");
    str_cmd.AppendLine("values(@ListNum,@EmployeeID,@Name,@SignTime,'3',@Series)");
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END"); //撤銷AllowStatus 3

    str_cmd.AppendLine("update List_V");
    str_cmd.AppendLine("set ListStatus='管理者撤銷'");
    str_cmd.AppendLine("where ListNum=@ListNum");
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");

    if (Session["EmployeeID"].ToString() != sno.Text)
      mailto = "first";//寄給第一關
    else
      mailto = "all";

    mailSubject = "訪客入校申請單已撤銷 (Your request has been deleted.)" + ListNum.Text;

    if (mailto == "first")
    {
      str_cmd.AppendLine("SELECT email");
      str_cmd.AppendLine("FROM DB_Mis_Admin.dbo.Sys_Interinfo_Person_V");
      str_cmd.AppendLine("WHERE EmployeeID=@apply_EmployeeID");
    }
    else
    {
      str_cmd.AppendLine("SELECT DISTINCT (SELECT email + ';'");
      str_cmd.AppendLine("FROM DB_Mis_Admin.dbo.Sys_Interinfo_Person_V");
      str_cmd.AppendLine("WHERE EmployeeID=@apply_EmployeeID");
      str_cmd.AppendLine("OR EmployeeID IN (SELECT [EmployeeID] FROM [DB_Tea_VToSchool].[dbo].[List_Allow] where ListNum=@ListNum)");
      str_cmd.AppendLine("FOR XML PATH('')) AS email");
    }

    str_cmd.AppendLine("if @chk<>0 Begin");
    str_cmd.AppendLine("  Rollback Transaction [Table]");
    str_cmd.AppendLine("end");
    str_cmd.AppendLine("else Begin");
    str_cmd.AppendLine("  Commit Transaction [Table]");
    str_cmd.AppendLine("End");

    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    SqlDataReader dr;
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    cmd.Parameters.AddWithValue("@EmployeeID", Session["EmployeeID"].ToString());
    cmd.Parameters.AddWithValue("@Name", Session["Name"].ToString());
    cmd.Parameters.AddWithValue("@SignTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    cmd.Parameters.AddWithValue("@Series", Convert.ToInt32(T_level.Text) + 1);
    cmd.Parameters.AddWithValue("@apply_EmployeeID", sno.Text);

    try
    {
      dr = cmd.ExecuteReader();
      str_cmd.Length = 0;
      while (dr.Read())
      {
        string email = dr["email"].ToString();
        StringBuilder mailBody = new StringBuilder();
        StringBuilder mailInfo = VtoSchool.mailbody(ListNum.Text);
        mailBody.AppendLine(mailInfo.ToString());
        mailBody.AppendLine("請點選下列連結，開啟審核文件:<br>Please Click The Link Below : <br><br>");
        mailBody.AppendLine("<a href='http://w2.kcbs.ntpc.edu.tw/VToSchool/View.aspx?num=" + ListNum.Text + "'>康橋國際學校-訪客入校系統</a><br>");
        try
        {
          MAIL.Send_Mail(Request.Url.Host, mailBody.ToString(), mailSubject, email);
          basic.Script_AlertHref(this.Page, "已撤銷！", "NotSignList.aspx");
        }
        catch
        {
          string getITMail = ConfigurationManager.AppSettings["ErrorMail"];
          MAIL.Send_Mail(Request.Url.Host, mailBody.ToString(), "【" + Campus2.Text + "校區】" + mailSubject, getITMail, ConfigurationManager.AppSettings["AdminMail"]);
          basic.Script_AlertHref(this.Page, "已撤銷！但發信異常，已通知管理者。", "NotSignList.aspx");
        }
      }
      dr.Close();
    }
    catch (Exception ex)
    {
      //執行資料庫異常，請洽資訊組。
      basic.Script_AlertMsg(this.Page, string.Format("{0}\n{1}", Resources.Resource.ErrorMsg8, ex.Message));
    }
    finally
    {
      cmd.Cancel();
      cn.Close();
      cn.Dispose();
    }
  }
  protected void showNoticeName()
  {
    string NoticeName;
    string NoticeEmployeeID;
    VtoSchool.getNotice(Campus.Text, VTypeID.Text, out NoticeName, out NoticeEmployeeID);
    Literal_Notice.Text = "警衛人員、" + NoticeName;
    
    if (Campus2.Text.Substring(0, 2).ToString() == "秀岡" && CheckBoxList1.SelectedValue.Contains("用餐"))
    {
      Literal_Notice.Text += "、石慧玲";
    }

    Literal_Notice.Text = "<span style='color:#1145d2;'>" + Literal_Notice.Text.TrimEnd('、') + "</span>";
  }
  protected void showNoticeName_byUser()
  {
    string NoticeName;
    VtoSchool.getNoticeName_byUser(ListNum.Text, out NoticeName);
    if (NoticeName != string.Empty)
    {
      Literal_Notice_byUser.Text = "<span style='color:#1145d2;'>" + NoticeName.TrimEnd('、') + "</span>";
    }
    else
    {
      Literal_Notice_byUser.Text = "<span style='color:#1145d2;'>無</span>";
    }
  }
  protected void TextBox2_DataBinding(object sender, EventArgs e)
  {
    string id = DetailsView1.Rows[0].Cells[1].Text;
    TextBox startTime = (TextBox)DetailsView1.Rows[2].Cells[1].FindControl("TextBox2");
    TextBox endTime = (TextBox)DetailsView1.Rows[3].Cells[1].FindControl("TextBox3");

    if (startTime.Text == string.Empty)
    {
      startTime.Text = Label142.Text;
    }
    if (endTime.Text == string.Empty)
    {
      endTime.Text = Label143.Text;
    }
  }

  protected void Button_FileUpload1_Click(object sender, EventArgs e)
  {
    string FilePath = Server.MapPath(@"~\UploadFiles") + @"\";
    DataTable FileTable = new DataTable();

    if (Session["FileTable"] != null)
    {
      FileTable = (DataTable)Session["FileTable"];
    }
    else
    {
      FileTable.Columns.Add(new DataColumn("FileName", typeof(string)));
      FileTable.Columns.Add(new DataColumn("GuidName", typeof(string)));
    }

    if (FileUpload1.HasFile)
    {
      string extension = Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
      if (extension != ".doc" && extension != ".docx" && extension != ".xls" && extension != ".xlsx" && extension != ".pdf" && extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".bmp")
      {
        //"僅允許上傳doc, xls, pdf, jpg, png或bmp檔！"
        basic.Script_AlertMsg(this.Page, Resources.Resource.ErrorMsg12);
        return;
      }
      else
      {
        int fileLen = FileUpload1.PostedFile.ContentLength;
        if (fileLen > (4 * 1024 * 1024))
        {
          //單一上傳檔案需小於4MB！
          basic.Script_AlertMsg(this.Page, Resources.Resource.ErrorMsg13);
          return;
        }
        else
        {
          string FileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
          string GuidName = Guid.NewGuid().ToString().Replace("-", "") + extension;
          FileUpload1.SaveAs(FilePath + GuidName);

          DataRow myRow = FileTable.NewRow();
          myRow["FileName"] = FileName.Replace("'", "");
          myRow["GuidName"] = GuidName;

          FileTable.Rows.Add(myRow);

          Session["FileTable"] = FileTable;

          _myTableBind();
        }
      }
    }
    showFile();
  }
  protected void _myTableBind()
  {
    int RowCount = 1;

    PlaceHolderAttachment.Controls.Clear();

    if (Session["FileTable"] != null)
    {
      string FileName, GuidName;

      DataTable FileTable = (DataTable)Session["FileTable"];

      foreach (DataRow myRow in FileTable.Rows)
      {
        FileName = myRow["FileName"].ToString().Trim();
        GuidName = myRow["GuidName"].ToString().Trim();

        LinkButton Btn = new LinkButton();
        Btn.ID = "Btn_" + GuidName;
        Btn.Text = "移除";
        Btn.Click += new EventHandler(ButtonRemove_Click);

        PlaceHolderAttachment.Controls.Add(new LiteralControl("<span style=\"font-size:13px;\">&nbsp;&nbsp;&nbsp;&nbsp;<span style=\"color:#ff00ff;\">"));
        PlaceHolderAttachment.Controls.Add(new LiteralControl(RowCount.ToString() + ". " + FileName + "</span>&nbsp;&nbsp;"));
        PlaceHolderAttachment.Controls.Add(Btn);
        PlaceHolderAttachment.Controls.Add(new LiteralControl("</span>"));
        PlaceHolderAttachment.Controls.Add(new LiteralControl("<br />"));

        RowCount++;
      }
    }
    else
    {
      PlaceHolderAttachment.Controls.Add(new LiteralControl("<span style=\"font-size:13px; color:#ff00ff;\">&nbsp;&nbsp;&nbsp;&nbsp;---" + Resources.Resource.NoProof + "---</span>"));
    }
  }
  protected void ButtonRemove_Click(object sender, EventArgs e)
  {
    string FilePath = Server.MapPath(@"~\UploadFiles") + @"\";
    string GuidName = ((LinkButton)sender).ID.Replace("Btn_", "");
    DataTable FileTable = (DataTable)Session["FileTable"];

    DataRow myRow = FileTable.Select("GuidName='" + GuidName + "'")[0];

    if (File.Exists(FilePath + myRow["GuidName"].ToString().Trim()))
    {
      File.Delete(FilePath + myRow["GuidName"].ToString().Trim());
    }

    FileTable.Rows.Remove(myRow);

    Session["FileTable"] = FileTable.Rows.Count == 0 ? null : FileTable;

    _myTableBind();
  }

  protected void Button2_Click(object sender, EventArgs e)
  {
    string DBName = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBName].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    if (Session["FileTable"] != null)
    {
      string FileName, GuidName;
      DataTable FileTable = (DataTable)Session["FileTable"];
      foreach (DataRow myRow in FileTable.Rows)
      {
        FileName = myRow["FileName"].ToString().Trim();
        GuidName = myRow["GuidName"].ToString().Trim();
        str_cmd.AppendLine("insert into List_File");
        str_cmd.AppendLine("(ListNum,FileName,GuidName,insertTime)");
        str_cmd.AppendLine("values(@ListNum,'" + FileName + "','" + GuidName + "','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "')");
      }
    }

    if (str_cmd.Length > 0)
    {
      SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
      cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);

      try
      {
        cmd.ExecuteNonQuery();
        basic.Script_AlertMsg(this.Page, Resources.Resource.inserted);
        showFile();
        Session["FileTable"] = null;
        PlaceHolderAttachment.Controls.Clear();
      }
      catch (Exception ex)
      {
        //執行資料庫異常，請洽資訊組。
        basic.Script_AlertMsg(this.Page, Resources.Resource.ErrorMsg8);
        MAIL.Send_Mail(Request.Url.Host, "【" + ListNum.Text + "】" + ex.ToString(), "訪客入校系統 新增附件資料庫異常", ConfigurationManager.AppSettings["AdminMail"]);
      }
      finally
      {
        cmd.Cancel();
        cn.Close();
        cn.Dispose();
      }
    }
  }

  protected void LinkButton1_Click(object sender, EventArgs e)
  {
    Response.Redirect("ModifyVInfo.aspx?num=" + ListNum.Text + "&VTypeID=" + VTypeID.Text);
  }

  protected void LinkButton2_Click(object sender, EventArgs e)
  {
    Response.Redirect("ModifyITSInfo.aspx?num=" + ListNum.Text + "&VTypeID=" + VTypeID.Text);
  }

  //protected void LinkButton3_Click(object sender, EventArgs e)
  //{
  //  Response.Redirect("ModifyNotes.aspx?num=" + ListNum.Text + "&VTypeID=" + VTypeID.Text);
  //}
}