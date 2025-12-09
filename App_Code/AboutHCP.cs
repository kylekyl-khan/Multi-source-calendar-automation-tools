using System.Data.SqlClient;
using System.Text;
/// <summary>
/// AboutHCP 的摘要描述
/// </summary>
public class AboutHCP
{
  readonly Operation_MSSQL MSSQL = new Operation_MSSQL();

  //網頁使用的連線字串
  readonly string DBName = "DB_MisAdmin"; //使用資料庫
                                          //程式使用的連線字串
                                          //string DBName = "Data Source = 172.16.81.248; Initial Catalog = DB_Mis_Admin; user id = CBPSweb_user; password =kcbs@sql2011;Connection Timeout=30;"; 

  public AboutHCP()
  {
    //
    // TODO: 在此加入建構函式的程式碼
    //
  }

  /// <summary>
  /// 回傳人員的部門ID
  /// </summary>
  /// <param name="EmployeeID">員編</param>
  /// <returns>部門ID</returns>
  public string GetMemberDeptID(string EmployeeID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select DeptID ");
    str_cmd.AppendLine("From Sys_Interinfo_Person_V with(nolock) ");
    str_cmd.AppendLine("where EmployeeID = @EmployeeID ");
    SqlParameter[] p = { new SqlParameter("@EmployeeID", EmployeeID) };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
    {
      if (dr.Read())
      {
        return dr["DeptID"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }
  }

  /// <summary>
  /// 回傳人員的部門名稱
  /// </summary>
  /// <param name="EmployeeID">員編</param>
  /// <returns>部門ID</returns>
  public string GetMemberDeptName(string EmployeeID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select DeptName ");
    str_cmd.AppendLine("From Sys_Interinfo_Person_V with(nolock) ");
    str_cmd.AppendLine("where EmployeeID = @EmployeeID ");
    SqlParameter[] p = { new SqlParameter("@EmployeeID", EmployeeID) };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
    {
      if (dr.Read())
      {
        return dr["DeptName"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }
  }

  /// <summary>
  /// 回傳人員的姓名
  /// </summary>
  /// <param name="EmployeeID">員編</param>
  /// <returns>人員的姓名</returns>
  public string GetMemberName(string EmployeeID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select Name ");
    str_cmd.AppendLine("From Sys_Interinfo_Person_V with(nolock) ");
    str_cmd.AppendLine("where EmployeeID = @EmployeeID ");
    SqlParameter[] p = { new SqlParameter("@EmployeeID", EmployeeID) };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
    {
      if (dr.Read())
      {
        return dr["Name"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }
  }

  /// <summary>
  /// 回傳人員的AD帳號
  /// </summary>
  /// <param name="EmployeeID">員編</param>
  /// <returns>人員的AD帳號</returns>
  public string GetMemberAccountID(string EmployeeID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select AccountID ");
    str_cmd.AppendLine("From Sys_Interinfo_Person_V with(nolock) ");
    str_cmd.AppendLine("where EmployeeID = @EmployeeID ");
    SqlParameter[] p = { new SqlParameter("@EmployeeID", EmployeeID) };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
    {
      if (dr.Read())
      {
        return dr["AccountID"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }
  }

  /// <summary>
  /// 回傳人員的員編
  /// </summary>
  /// <param name="AccountID">AD帳號</param>
  /// <returns>人員的員編</returns>
  public string GetMemberEmployeeID(string AccountID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select EmployeeID ");
    str_cmd.AppendLine("From Sys_Interinfo_Person_V with(nolock) ");
    str_cmd.AppendLine("where AccountID = @AccountID ");
    SqlParameter[] p = { new SqlParameter("@AccountID", AccountID) };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
    {
      if (dr.Read())
      {
        return dr["EmployeeID"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }
  }

  /// <summary>
  /// 從員工編號找到HCP系統編號
  /// </summary>
  /// <param name="EmployeeID">員工編號</param>
  /// <returns></returns>
  //public string GetMemberSystemID(string EmployeeID)
  //{
  //    StringBuilder str_cmd = new StringBuilder();

  //    str_cmd.AppendLine("select id ");
  //    str_cmd.AppendLine("From Sys_Interinfo_Person_V with(nolock) ");
  //    str_cmd.AppendLine("where EmployeeID = @EmployeeID ");
  //    SqlParameter[] p = {
  //                       new SqlParameter("@EmployeeID",EmployeeID)
  //                       };

  //    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
  //    {
  //        if (dr.Read())
  //        {
  //            return dr["id"].ToString();
  //        }
  //        else
  //        {
  //            return string.Empty;
  //        }
  //    }
  //}

  /// <summary>
  /// 取得員工於HCP內的公司別
  /// </summary>
  /// <param name="EmployeeID">員工編號</param>
  /// <returns>公司別</returns>
  public string GetCompanyID(string EmployeeID)
  {
    Operation_MSSQL MSSQL = new Operation_MSSQL();
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select CompanyID");
    str_cmd.AppendLine("from dbo.Sys_Interinfo_Person_V");
    str_cmd.AppendLine("where EmployeeID = @EmployeeID ");

    SqlParameter[] SP1 = { new SqlParameter("@EmployeeID", EmployeeID) };

    return MSSQL.GetScalar(str_cmd.ToString(), SP1, "DB_MisAdmin").ToString();
  }

  /// <summary>
  /// 取得人員的校區
  /// </summary>
  /// <param name="EmployeeID">員工編號</param>
  /// <returns>校區</returns>
  public string GetSchoolArea(string EmployeeID)
  {
    Operation_MSSQL MSSQL = new Operation_MSSQL();
    StringBuilder str_cmd = new StringBuilder();
    SqlDataReader dr;
    string Return_str = "";

    str_cmd.AppendLine("select Campus");
    str_cmd.AppendLine("from dbo.Sys_Interinfo_Person_V");
    str_cmd.AppendLine("where EmployeeID = @EmployeeID");

    SqlParameter[] SP1 = {
                             new SqlParameter("@EmployeeID",EmployeeID)
                             };

    dr = MSSQL.GetDataRead(str_cmd.ToString(), SP1, "DB_MisAdmin");

    if (dr.Read())
    {
      Return_str = dr["Campus"].ToString();
    }
    dr.Close();

    return Return_str;
  }
}