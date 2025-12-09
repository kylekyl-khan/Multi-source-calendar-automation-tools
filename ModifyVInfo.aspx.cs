using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

public partial class detailPage : System.Web.UI.Page
{
  readonly ClassBasic basic = new ClassBasic();
  readonly AboutMail Mail = new AboutMail();
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
      ListNum.Text = Request.QueryString["num"].ToString();
      VTypeID.Text = Request.QueryString["VTypeID"].ToString();

      //帶出訪客資料
      showVInfo();
      //入校資料Panel開合
      showInfoPanel();
    }
  }
  protected void showVInfo()
  {
    string DBname1 = "DB_Tea_VToSchool";
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings
[DBname1].ConnectionString.ToString());
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
    }
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    SqlDataReader dr = cmd.ExecuteReader();
    if (dr.Read())
    {
      switch (VTypeID.Text)
      {
        case "1": //一般訪客
          Label101.Text = dr["VName"].ToString();
          break;
        case "2": //外校生家長
          Label105.Text = dr["parentName"].ToString();
          Label106.Text = dr["stuName"].ToString();
          break;
        case "3": //政府機關
          Label110.Text = dr["governmentName"].ToString();
          Label111.Text = dr["leaderName"].ToString();
          break;
        case "4": //媒體
          Label115.Text = dr["mediaName"].ToString();
          Label116.Text = dr["Name"].ToString();
          break;
        case "5": //在籍學生家長
          Label120.Text = dr["stuName"].ToString();
          Label121.Text = dr["stuClass"].ToString();
          Label122.Text = dr["stuNum"].ToString();
          Label123.Text = dr["parentName"].ToString();
          break;
        case "6": //校友
          Label127.Text = dr["alumni"].ToString();
          Label128.Text = dr["VNum"].ToString();
          break;
        case "7": //廠商
          Label132.Text = dr["companyName"].ToString();
          break;
        case "8": //團體訪客
          Label136.Text = dr["groupName"].ToString();
          Label137.Text = dr["leaderName"].ToString();
          break;
        case "9": //學校活動
          Label5.Text = dr["VName"].ToString();
          break;
      }
      TextBox1.Text = dr["VNum"].ToString();
      TextBox2.Text = dr["carNum"].ToString();
      TextBox3.Text = dr["phoneNum"].ToString();
    }
    dr.Close();
    cmd.Cancel();
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
    Panel_11.Visible = false;
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
        break;
      case "5": //在籍學生家長
        Panel_05.Visible = true;
        break;
      case "6": //校友
        Panel_06.Visible = true;
        break;
      case "7": //廠商
        Panel_07.Visible = true;
        break;
      case "8": //團體訪客
        Panel_08.Visible = true;
        break;
      case "9": //學校活動
        Panel_11.Visible = true;
        break;
    }
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
    SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings
[DBname1].ConnectionString.ToString());
    cn.Open();
    StringBuilder str_cmd = new StringBuilder();
    //--設定交易還原的寫法
    str_cmd.AppendLine("DECLARE @chk tinyint;");
    str_cmd.AppendLine("SET @chk = 0;");
    str_cmd.AppendLine("Begin Transaction [Trans_Name]");
    switch (VTypeID.Text)
    {
      case "1": //一般訪客
        str_cmd.AppendLine("update 一般訪客子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
      case "2": //外校生家長
        str_cmd.AppendLine("update 外校生家長子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
      case "3": //政府機關
        str_cmd.AppendLine("update 政府機關子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
      case "4": //媒體
        str_cmd.AppendLine("update 媒體子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
      case "5": //在籍學生家長
        str_cmd.AppendLine("update 在籍學生家長子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
      case "6": //校友
        str_cmd.AppendLine("update 校友子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
      case "7": //廠商
        str_cmd.AppendLine("update 廠商子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
      case "8": //團體訪客
        str_cmd.AppendLine("update 團體訪客子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
      case "9": //學校活動
        str_cmd.AppendLine("update 學校活動子表");
        str_cmd.AppendLine("set VNum=@VNum,carNum=@carNum,phoneNum=@phoneNum,updateTime=@updateTime");
        str_cmd.AppendLine("where ListNum=@ListNum");
        break;
    }
    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");
    //--判斷是否產生交易錯誤
    str_cmd.AppendLine("IF @chk <> 0 BEGIN -- 若是新增資料發生錯誤");
    str_cmd.AppendLine("    Rollback Transaction [Trans_Name] -- 復原所有操作所造成的變更");
    str_cmd.AppendLine("END");
    str_cmd.AppendLine("ELSE BEGIN");
    str_cmd.AppendLine("    Commit Transaction [Trans_Name] -- 提交所有操作所造成的變更");
    str_cmd.AppendLine("END");
    SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
    cmd.Parameters.AddWithValue("@VNum", TextBox1.Text);
    cmd.Parameters.AddWithValue("@carNum", TextBox2.Text);
    cmd.Parameters.AddWithValue("@phoneNum", TextBox3.Text);
    cmd.Parameters.AddWithValue("@updateTime", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    cmd.Parameters.AddWithValue("@ListNum", ListNum.Text);
    try
    {
      cmd.ExecuteNonQuery();
    }
    catch (Exception ex)
    {
      Mail.Send_Mail(Request.Url.Host, "訪客入校系統ModifyVInfo.aspx發生錯誤<br>" + ListNum.Text + "<br>" + ex.ToString(), "訪客入校系統ModifyVInfo.aspx發生錯誤", ConfigurationManager.AppSettings["AdminMail"]);
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