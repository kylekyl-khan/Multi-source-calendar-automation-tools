using KCBS_SSO;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
public partial class Logon : System.Web.UI.Page
{
  protected void Page_Load(object sender, EventArgs e)
  {
    Session.Clear();

    KCBS_SSO.SSOOperation SSOp = new SSOOperation();
    SSOp.DefaultLoginHandle(this.Page);

    Session["EmployeeID"] = SSOp.EmpID;

    if (LoadInfo() == true)
    {
      string num = (Request.QueryString["num"] + "").ToString().Trim();
      string page = (Request.QueryString["page"] + "").ToString().Trim();
      if (page != "" && num != "")
      {
        Response.Redirect(page + ".aspx?num=" + num);
        //Response.Write("有轉頁");
      }
      else
      {
        Response.Redirect("Manual.aspx");
        //Response.Write("沒轉頁");
      }
    }
    else
    {
      ClassBasic Basic = new ClassBasic();
      SSOp.Logout(this.Page, SSOp.DefaultLoginURL);

      Basic.Script_AlertHref(this.Page, "沒有您的HCP相關資料，請洽資訊組確認。", "Logon.aspx");
    }
  }

  protected void btnLogon_Click(object sender, EventArgs e)
  {
    // Path to you LDAP directory server.
    // Contact your network administrator to obtain a valid path.
    //Operation_AD AD = new Operation_AD();
    //Session["Account"] = txtUserName.Text;
    //if (txtDomainName.Text == "kcbs")
    //{
    //    try
    //    {
    //        if (AD.checkAccount("KCBS", txtUserName.Text, txtPassword.Text))
    //        {
    //            Operation_MSSQL MSSQL = new Operation_MSSQL();
    //            StringBuilder str_cmd = new StringBuilder();

    //            str_cmd.AppendLine("select [id_no_sz],[name_sz]");
    //            str_cmd.AppendLine("from [HCP_Personnel]");
    //            str_cmd.AppendLine("where [AccountID] = @Account ");

    //            SqlParameter[] SP1 = new SqlParameter[] {
    //                new SqlParameter("@Account",txtUserName.Text)
    //            };

    //            SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), SP1, "DB_MisAdmin");
    //            if (dr.Read())
    //            {
    //                Session["snameC"] = dr["name_sz"].ToString();
    //                Session["EmployeeID"] = dr["id_no_sz"].ToString();
    //                Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserName.Text, false));
    //            }
    //            else
    //            {
    //                lblError.Text = "人員資料表尚未有您的資料，請洽資訊組。";
    //            }
    //        }
    //        else
    //        {
    //            lblError.Text = "認證失敗!請檢查您的帳號,密碼!";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        lblError.Text = "認證失敗! " + ex.Message;
    //    }
    //}
  }
  protected bool LoadInfo()
  {
    //判斷語系
    ClassBasic basic = new ClassBasic();
    string UserCulture = basic.GetUserCulture();
    if (UserCulture.IndexOf("zh") == 0)
    {
      Session["UserCulture"] = "中文";
    }
    else
    {
      Session["UserCulture"] = "英文";
    }
    bool isOK = false;
    string adminLevel = "0";
    string DBname = "DB_MisAdmin";
    string DBname2 = "DB_Tea_VToSchool";
    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings
        [DBname].ConnectionString.ToString());
    SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings
        [DBname2].ConnectionString.ToString());
    conn.Open();
    conn2.Open();
    //判斷人員資料有無在hcp系統裡
    StringBuilder str_cmd = new StringBuilder();
    str_cmd.AppendLine("select EmployeeID,Name,AccountID,Campus,DeptName,email");
    str_cmd.AppendLine("from Sys_Interinfo_Person_V");
    str_cmd.AppendLine("where EmployeeID='" + Session["EmployeeID"].ToString() + "' and (outdate is null or outdate + 1 > GETDATE())");
    using (SqlCommand cmd = new SqlCommand(str_cmd.ToString(), conn))
    {
      SqlDataReader dr = cmd.ExecuteReader();
      if (dr.Read())
      {
        Session["snameC"] = dr["Name"].ToString();

        Session["AccountID"] = dr["AccountID"].ToString();
        Session["Name"] = dr["Name"].ToString();
        Session["Campus"] = dr["Campus"].ToString();
        Session["DeptName"] = dr["DeptName"].ToString();
        Session["EMail"] = dr["email"].ToString();

        isOK = true;
      }
      dr.Close();
    }

    //判斷是否為管理者 權限為何
    if (isOK)
    {
      StringBuilder str_cmd2 = new StringBuilder();
      str_cmd2.AppendLine("select EmployeeID,AdminLevel");
      str_cmd2.AppendLine("from Sys_SetAdmin");
      str_cmd2.AppendLine("where EmployeeID='" + Session["EmployeeID"].ToString() + "'");
      using (SqlCommand cmd = new SqlCommand(str_cmd2.ToString(), conn2))
      {
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
          adminLevel = dr["AdminLevel"].ToString();
        }
        dr.Close();
      }
    }
    Session["AdminLevel"] = adminLevel;
    conn.Close();
    conn.Dispose();
    conn2.Close();
    conn2.Dispose();
    return isOK;
  }
}
