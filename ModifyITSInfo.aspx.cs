using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

public partial class detailPage : System.Web.UI.Page
{
  readonly ClassBasic basic = new ClassBasic();
  readonly AboutMail Mail = new AboutMail();
  readonly VtoSchool VtoSchool = new VtoSchool();
  readonly string MailAdmin = ConfigurationManager.AppSettings["AdminMail"];
  protected void Page_Load(object sender, EventArgs e)
  {
    if (Session["EmployeeID"] == null)
    {
      Response.Redirect("Logon.aspx");
      return;
    }
    if (!IsPostBack)
    {
      if (Session["EmployeeID"] == null)
      {
        Response.Redirect("Logon.aspx");
        return;
      }
      if (Request.QueryString["num"] == null || Request.QueryString["VTypeID"] == null)
      {
        Response.Redirect("Logon.aspx");
        return;
      }
      Campus_save.Text = Session["Campus"].ToString();
      ListNum.Text = Request.QueryString["num"].ToString();
      VTypeID.Text = Request.QueryString["VTypeID"].ToString();

      //帶出入校資料
      showVInfo();
    }
  }
  protected void showVInfo()
  {
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());

    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("select Name,startTime,endTime,SpeRequest from List_V where ListNum=@ListNum");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
      Name.Text = dr["Name"].ToString();
      TextBox1.Text = Convert.ToDateTime(dr["startTime"].ToString()).ToString("yyyy/MM/dd");
      TextBox2.Text = Convert.ToDateTime(dr["endTime"].ToString()).ToString("yyyy/MM/dd");
      DropDownList1.SelectedValue = Convert.ToDateTime(dr["startTime"].ToString()).Hour.ToString();
      DropDownList2.SelectedValue = Convert.ToDateTime(dr["startTime"].ToString()).Minute.ToString();
      DropDownList3.SelectedValue = Convert.ToDateTime(dr["endTime"].ToString()).Hour.ToString();
      DropDownList4.SelectedValue = Convert.ToDateTime(dr["endTime"].ToString()).Minute.ToString();
      SpeRequest.Text = dr["SpeRequest"].ToString();
    }
    dr.Close();
    cmd.Cancel();
    cn.Close();
    cn.Dispose();
  }
  //送出
  protected void Button1_Click(object sender, EventArgs e)
  {
    DateTime dtStart;
    DateTime dtEnd;

    string sStart = TextBox1.Text;
    string sEnd = TextBox2.Text;

    if (Session["EmployeeID"] == null)
    {
      Response.Redirect("Logon.aspx");
      return;
    }

    if (!DateTime.TryParse(sStart, out dtStart) || dtStart < DateTime.Today)
    {
      //預定入校時間格式有誤，請重新選填!
      basic.Script_AlertMsg(this.Page, Resources.Resource.ErrorMsg5_6);
      TextBox1.Focus();
      return;
    }

    if (!DateTime.TryParse(sEnd, out dtEnd) || dtEnd < DateTime.Today)
    {
      //預定離校時間不得小於今日!
      basic.Script_AlertMsg(this.Page, Resources.Resource.ErrorMsg5_7);
      TextBox2.Focus();
      return;
    }

    DateTime dtLast = dtStart.AddDays(7);
    if (dtEnd < dtStart || dtEnd > dtLast)
    {
      //預定離校時間不得大於一週!
      basic.Script_AlertMsg(this.Page, Resources.Resource.ErrorMsg5_8);
      TextBox2.Focus();
      return;
    }

    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());

    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    //--設定交易還原的寫法
    str_cmd.AppendLine("DECLARE @chk tinyint;");
    str_cmd.AppendLine("SET @chk = 0;");
    str_cmd.AppendLine("Begin Transaction [Trans_Name]");
    //1.更新預計入校時間
    str_cmd.AppendLine("update List_V");
    str_cmd.AppendLine("set startTime=@startTime,endTime=@endTime,C_Level='0',ListStatus='審核中'");
    str_cmd.AppendLine("where ListNum=@ListNum");
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");

    //2.更新List_Allow
    str_cmd.AppendLine("update List_Allow");
    str_cmd.AppendLine("set SignTime=@SignTime,SignContent=@SignContent,AllowStatus=@AllowStatus");
    str_cmd.AppendLine("where ListNum=@ListNum");
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");

    //3.找出有無需簽核的人員
    str_cmd.AppendLine("declare @count int;");
    str_cmd.AppendLine("select @count=count(*)");
    str_cmd.AppendLine("from List_Allow");
    str_cmd.AppendLine("where ListNum=@ListNum");
    str_cmd.AppendLine("if @count>0");
    str_cmd.AppendLine("begin");
    str_cmd.AppendLine("    select email,@ListNum as ListNum");
    str_cmd.AppendLine("    from DB_Mis_Admin.dbo.Sys_Interinfo_Person_V");
    str_cmd.AppendLine("    where EmployeeID in (");
    str_cmd.AppendLine("    select a.EmployeeID");
    str_cmd.AppendLine("    from List_Allow a");
    str_cmd.AppendLine("    inner join List_V b");
    str_cmd.AppendLine("    on a.ListNum = b.ListNum");
    str_cmd.AppendLine("    and a.Series = '1'");
    str_cmd.AppendLine("    where a.ListNum = @ListNum");
    str_cmd.AppendLine(")");
    str_cmd.AppendLine("end");
    str_cmd.AppendLine("else");
    str_cmd.AppendLine("begin");
    str_cmd.AppendLine("    select a.email,b.ListNum");
    str_cmd.AppendLine("    from DB_Mis_Admin.dbo.Sys_Interinfo_Person_V a");
    str_cmd.AppendLine("    inner join List_V b");
    str_cmd.AppendLine("    on a.EmployeeID = b.EmployeeID");
    str_cmd.AppendLine("    where b.ListNum=@ListNum");
    str_cmd.AppendLine("end");

    //--判斷是否產生交易錯誤
    str_cmd.AppendLine("IF @chk <> 0 BEGIN -- 若是新增資料發生錯誤");
    str_cmd.AppendLine("    Rollback Transaction [Trans_Name] -- 復原所有操作所造成的變更");
    str_cmd.AppendLine("END");
    str_cmd.AppendLine("ELSE BEGIN");
    str_cmd.AppendLine("    Commit Transaction [Trans_Name] -- 提交所有操作所造成的變更");
    str_cmd.AppendLine("END");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@startTime", TextBox1.Text + " " + DropDownList1.SelectedValue + ":" + DropDownList2.SelectedValue + ":00");
    cmd.Parameters.AddWithValue("@endTime", TextBox2.Text + " " + DropDownList3.SelectedValue + ":" + DropDownList4.SelectedValue + ":00");
    cmd.Parameters.AddWithValue("@SignTime", DBNull.Value);
    cmd.Parameters.AddWithValue("@SignContent", DBNull.Value);
    cmd.Parameters.AddWithValue("@AllowStatus", DBNull.Value);
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    SqlDataReader dr;
    try
    {
      dr = cmd.ExecuteReader();
      string email = string.Empty;
      string mailSubject = string.Empty;
      StringBuilder mailBody = new StringBuilder();
      StringBuilder mailInfo = VtoSchool.mailbody(ListNum.Text);
      mailBody.AppendLine(mailInfo.ToString());
      mailBody.AppendLine("請點選下列連結，開啟審核文件:<br>Please Click The Link Below : <br><br>");
      mailBody.AppendLine("<a href='http://w2.kcbs.ntpc.edu.tw/VToSchool/View.aspx?num=" + ListNum.Text + "'>康橋國際學校-訪客入校系統</a><br>");

      if (!dr.HasRows)
      {
        dr.Read();
        email = dr["email"].ToString();
        mailSubject = Name.Text + "提出的訪客入校單已核准。" + Name.Text + "'s application has been approved." + ListNum.Text;
        //審核過的單要CC給通知對象
        string CCto = VtoSchool.GetNoticeEmail(Campus_save.Text.Substring(0, 2).ToString(), DropDownList1.SelectedValue);
        if (Campus_save.Text.Substring(0, 2).ToString() == "秀岡" && SpeRequest.Text.Contains("用餐"))
        {
          CCto += ";verashih@kcis.ntpc.edu.tw";
        }

        try
        {
          Mail.Send_Mail(Request.Url.Host, mailBody.ToString(), mailSubject, email, CCto);
          //申請單已送出。
          basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg10_, "PersonalList.aspx");
        }
        catch (Exception ex)
        {
          //發信異常 

          basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg11, "PersonalList.aspx");
          Mail.Send_Mail(Request.Url.Host, mailBody.ToString() + ex.ToString(), mailSubject + " 發信異常", MailAdmin);
        }
        dr.Close();
      }
      else
      {
        while (dr.Read())
        {
          email = dr["email"].ToString();
          mailSubject = "請重新簽核" + Name.Text + "提出的訪客入校單(預計入校時間更改) Please approve " + Name.Text + "'s application." + ListNum.Text;
          try
          {
            Mail.Send_Mail(Request.Url.Host, mailBody.ToString(), mailSubject, email);
            //申請單已送出，請耐心等候審核。
            basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg10, "PersonalList.aspx");
          }
          catch (Exception ex)
          {
            basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg11, "PersonalList.aspx");
            Mail.Send_Mail(Request.Url.Host, mailBody.ToString() + ex.ToString(), mailSubject + " 發信異常", MailAdmin);
          }

        }
        dr.Close();
      }
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

  protected void Button2_Click(object sender, EventArgs e)
  {
    Response.Redirect("view.aspx?num=" + ListNum.Text);
  }
}
