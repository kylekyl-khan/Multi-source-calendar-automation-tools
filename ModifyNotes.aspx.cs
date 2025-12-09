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
      showNotes();
    }
  }
  protected void showNotes()
  {
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname1].ConnectionString.ToString());

    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("select Name,Notes,SpeRequest from List_V where ListNum=@ListNum");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
      Name.Text = dr["Name"].ToString();
      TextBox1.Text = dr["Notes"].ToString();
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
    if (Session["EmployeeID"] == null)
    {
      Response.Redirect("Logon.aspx");
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
    str_cmd.AppendLine("set Notes=@Notes");
    str_cmd.AppendLine("where ListNum=@ListNum");
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");

    //--判斷是否產生交易錯誤
    str_cmd.AppendLine("IF @chk <> 0 BEGIN -- 若是新增資料發生錯誤");
    str_cmd.AppendLine("    Rollback Transaction [Trans_Name] -- 復原所有操作所造成的變更");
    str_cmd.AppendLine("END");
    str_cmd.AppendLine("ELSE BEGIN");
    str_cmd.AppendLine("    Commit Transaction [Trans_Name] -- 提交所有操作所造成的變更");
    str_cmd.AppendLine("END");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@Notes", TextBox1.Text);
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);

    try
    {
      cmd.ExecuteNonQuery();

      string email = string.Empty;
      string mailSubject = string.Empty;
      StringBuilder mailBody = new StringBuilder();
      StringBuilder mailInfo = VtoSchool.mailbody(ListNum.Text, true);
      mailBody.AppendLine(mailInfo.ToString());
      mailBody.AppendLine("請點選下列連結，開啟審核文件:<br>Please Click The Link Below : <br><br>");
      mailBody.AppendLine("<a href='http://w2.kcbs.ntpc.edu.tw/VToSchool/View.aspx?num=" + ListNum.Text + "'>康橋國際學校-訪客入校系統</a><br>");
      //審核過的單要CC給通知對象
      string CCto = VtoSchool.GetNoticeEmail(Campus_save.Text.Substring(0, 2).ToString(), VTypeID.Text);

      if (Campus_save.Text.Substring(0, 2).ToString() == "秀岡" && SpeRequest.Text.Contains("用餐"))
      {
        CCto += ";verashih@kcis.ntpc.edu.tw;";
      }

      mailSubject = Name.Text + "提出的訪客入校單注意事項已修改。" + Name.Text + "'s application notes has been modified." + ListNum.Text;

      try
      {
        Mail.Send_Mail(Request.Url.Host, mailBody.ToString(), mailSubject, email, CCto);
        //申請單已送出。
        basic.Script_AlertHref(this.Page, Resources.Resource.ErrorMsg10, "PersonalList.aspx");
      }
      catch (Exception ex)
      {
        //發信異常 

        Mail.Send_Mail(Request.Url.Host, mailBody.ToString() + ex.ToString(), mailSubject + " 發信異常", MailAdmin);
      }
    }
    catch (Exception ex)
    {
      Mail.Send_Mail(Request.Url.Host, "訪客入校系統ModifyNotes.aspx發生錯誤<br>" + ListNum.Text + "<br>" + ex.ToString(), "訪客入校系統ModifyNotes.aspx發生錯誤", ConfigurationManager.AppSettings["AdminMail"]);
    }
    finally
    {
      str_cmd.Length = 0;
      cmd.Cancel();
      cn.Close();
      cn.Dispose();
      basic.Script_AlertHref(this.Page, "已修改!", "view.aspx?num=" + ListNum.Text);
    }
  }

  protected void Button2_Click(object sender, EventArgs e)
  {
    Response.Redirect("view.aspx?num=" + ListNum.Text);
  }
}