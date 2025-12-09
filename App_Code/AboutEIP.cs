using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// AboutEIP 的摘要描述
/// </summary>
public class AboutEIP
{
  readonly Operation_MSSQL MSSQL = new Operation_MSSQL();

  //若組織版本有異動，則這邊要修正內容~!!
  string VersionNum = "2";

  //網頁使用的連線字串
  string DBName = "DB_Mis_KCIS_BPM"; //使用資料庫
                                     //程式使用的連線字串


  public AboutEIP()
  {
    //     TODO: 在此加入建構函式的程式碼
  }

  /// <summary>
  /// 回傳人員的職級
  /// </summary>
  /// <param name="AccountID">人員AD帳號</param>
  /// <returns>職級</returns>
  public int GetJobGrade(string AccountID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select Degree");
    str_cmd.AppendLine("from AD_Account a with(nolock)");
    str_cmd.AppendLine("inner join AFS_Title b with(nolock) on a.titleID = b.titleID");
    str_cmd.AppendLine("where AccountID = @AccountID ");
    SqlParameter[] p = { new SqlParameter("@AccountID", AccountID) };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
    {
      if (dr.Read())
      {
        return Convert.ToInt32(dr["Degree"]);
      }
      else
      {
        return 0;
      }
    }
  }

  /// <summary>
  /// 回傳人員職稱
  /// </summary>
  /// <param name="AccountID">員工編號</param>
  /// <returns>職稱</returns>
  public string GetTitle(string AccountID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select TitleCaption");
    str_cmd.AppendLine("from AD_Account a with(nolock)");
    str_cmd.AppendLine("inner join AFS_Title b with(nolock) on a.titleID = b.titleID");
    str_cmd.AppendLine("where AccountID = @AccountID ");
    SqlParameter[] p = {
                           new SqlParameter("@AccountID",AccountID)
                           };

    if (AccountID == "C1010856")
    {
      return "組長";
    }

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
    {
      if (dr.Read())
      {
        return dr["TitleCaption"].ToString();
      }
      else
      {
        return string.Empty;
      }
    }
  }

  /// <summary>
  /// 回傳部門名稱
  /// </summary>
  /// <param name="DeptID">部門代號</param>
  /// <returns>職稱</returns>
  public string GetDeptName(string DeptID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select DeptName");
    str_cmd.AppendLine("from AFS_Dept a with(nolock)");
    str_cmd.AppendLine("where DeptID = @DeptID ");
    SqlParameter[] p = { new SqlParameter("@DeptID", DeptID) };

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
  /// 回傳人員的部門ID
  /// </summary>
  /// <param name="AccountID">人員AD帳號</param>
  /// <returns>部門ID</returns>
  public string GetDeptID(string AccountID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select DeptID ");
    str_cmd.AppendLine("From AD_Account a with(nolock) ");
    str_cmd.AppendLine("where AccountID = @AccountID ");
    SqlParameter[] p = {
                           new SqlParameter("@AccountID",AccountID)
                           };

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
  /// 回傳人員的姓名
  /// </summary>
  /// <param name="AccountID">人員AD帳號</param>
  /// <returns>人員的姓名</returns>
  public string GetMembetName(string AccountID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select FullName ");
    str_cmd.AppendLine("From AD_Account a with(nolock) ");
    str_cmd.AppendLine("where AccountID = @AccountID ");
    SqlParameter[] p = {
                           new SqlParameter("@AccountID",AccountID)
                           };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), p, DBName))
    {
      if (dr.Read())
      {
        return dr["FullName"].ToString().Split('(')[0];
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
  /// <param name="EmpID">人員的員工編號</param>
  /// <returns>人員的AD帳號</returns>
  public string GetMemberAccountID(string EmpID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select AccountID ");
    str_cmd.AppendLine("From AD_Account with(nolock) ");
    str_cmd.AppendLine("where GivenName = @GivenName ");
    SqlParameter[] p = {
                           new SqlParameter("@GivenName",EmpID)
                           };

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
  /// 輸入所在部門以及人員職級，往上找具有簽核權限的人員，並有限制找到的最大職級
  /// </summary>
  /// <param name="DeptID">人員所在的部門ID</param>
  /// <param name="MemberJobGrade">人員職級</param>
  /// <param name="MaxJobGrade">最大職級</param>
  /// <returns>回傳簽核人員的AccountID，並用;分開</returns>
  public string GetRouteByJobGrade(string DeptID, int MemberJobGrade, int MaxJobGrade)
  {
    StringBuilder str_cmd = new StringBuilder();
    string GetRoutList = string.Empty;
    Int32 JobGrade = Int32.MinValue;//紀錄目前該部門找到的最大職級，用來判斷是否要繼續往上層部門走

    str_cmd.Length = 0;
    str_cmd.AppendLine("select AccountID,JobGrade");
    str_cmd.AppendLine("from dbo.FSe7en_Org_MemberStruct  with(nolock)");
    str_cmd.AppendLine("where DeptID = @DeptID and Enabled = 1 and ApproveRight = 1 and JobGrade > @MemberJobGrade and Version = @Version and JobGrade <= 70");
    str_cmd.AppendLine("order by JobGrade");
    SqlParameter[] SP1 = {
                               new SqlParameter("@DeptID",DeptID),
                               new SqlParameter("@MemberJobGrade",MemberJobGrade),
                               new SqlParameter("@Version",VersionNum)
                           };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), SP1, DBName))
    {
      while (dr.Read())
      {
        JobGrade = Convert.ToInt32(dr["JobGrade"].ToString());
        //if (MaxJobGrade > MemberJobGrade)
        //{
        //  GetRoutList = GetRoutList + GetAbsentAgentList(dr["AccountID"].ToString().Trim()) + ";";
        //}
        if (JobGrade <= MaxJobGrade)
        {
          GetRoutList = GetRoutList + GetAbsentAgentList(dr["AccountID"].ToString().Trim()) + ";";
        }

        MemberJobGrade = JobGrade;
      }
    }

    //如果這層部門的最大職級已經超過設定的上限，就結束；否則就繼續往上層部門走
    if (MaxJobGrade <= JobGrade)
    {
      return GetRoutList;
    }
    else
    {
      str_cmd.Length = 0;

      str_cmd.AppendLine("select ParentDept");
      str_cmd.AppendLine("from dbo.FSe7en_Org_DeptStruct with(nolock)");
      str_cmd.AppendLine("where deptID = @DeptID and Enabled = 1 and Version = @Version");
      SqlParameter[] SP2 = {
                               new SqlParameter("@DeptID",DeptID),
                               new SqlParameter("@Version",VersionNum)
                           };

      using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), SP2, DBName))
      {
        if (dr.Read())
        {
          //若上層部門設定為NULL，則結束
          if (Convert.IsDBNull(dr["ParentDept"]))
          {
            return GetRoutList;
          }
          else
          {
            //若上層部門設定為空，則結束
            if (dr["ParentDept"].ToString().Trim() == "")
            {
              return GetRoutList;
            }
            else
            {
              return GetRoutList + GetRouteByJobGrade(dr["ParentDept"].ToString().Trim(), JobGrade, MaxJobGrade);
            }
          }
        }
        else
        {
          //若找不到上層部門，則結束
          return GetRoutList;
        }
      }
    }

  }

  /// <summary>
  /// 輸入所在部門以及人員職級，往上找具有簽核權限的人員，並有限制找到的最大職級(加入可會簽方式)
  /// </summary>
  /// <param name="DeptID">人員所在的部門ID</param>
  /// <param name="MemberJobGrade">人員職級</param>
  /// <param name="MaxJobGrade">最大職級</param>
  /// <param name="Escalate_Search">職級不足時要不要往上找</param>
  /// <param name="Form">從那一個特殊流程來</param>
  /// <returns>回傳簽核人員的AccountID與職等，使用JSON格式</returns>
  public string[,] GetRouteByJobGrade_JSON(string DeptID, int MemberJobGrade, int MaxJobGrade, string Escalate_Search, string Form)
  {
    string JSON = "{\"GetRouteByJobGrade\": [" + GetRouteByJobGrade_JSON_Recursive(DeptID, MemberJobGrade, MaxJobGrade, Escalate_Search).TrimEnd(',') + "]}";

    JSON_GetRouteByJobGrade.Rootobject temp = JsonConvert.DeserializeObject<JSON_GetRouteByJobGrade.Rootobject>(JSON);
    string[,] allow_AD = new string[temp.GetRouteByJobGrade.GetLength(0), 3];

    for (int i = 0; i < temp.GetRouteByJobGrade.GetLength(0); i++)
    {

      //如果是青山幼兒園的加班單，流程不要加入總園長簽核
      if (Form == "OverTime" && (DeptID.Contains("QS1070") || DeptID.Contains("QS107A")))
      {
        if (temp.GetRouteByJobGrade[i].AccountID == "K960891")
        {
          continue;
        }
      }

      allow_AD[i, 0] = temp.GetRouteByJobGrade[i].AccountID;
      allow_AD[i, 1] = temp.GetRouteByJobGrade[i].Grade;
      allow_AD[i, 2] = temp.GetRouteByJobGrade[i].DeptID;
    }

    return allow_AD;
  }

  /// <summary>
  /// 輸入所在部門以及人員職級，往上找具有簽核權限的人員，並有限制找到的最大職級(加入可會簽方式)
  /// </summary>
  /// <param name="DeptID">人員所在的部門ID</param>
  /// <param name="MemberJobGrade">人員職級</param>
  /// <param name="MaxJobGrade">最大職級</param>
  /// <param name="Escalate_Search">是否要往上找</param>
  /// <returns>回傳簽核人員的AccountID與職等，使用JSON格式</returns>
  public string GetRouteByJobGrade_JSON_Recursive(string DeptID, int MemberJobGrade, int MaxJobGrade, string Escalate_Search)
  {
    if (MemberJobGrade == MaxJobGrade)
    {
      MemberJobGrade = MemberJobGrade - 1;
    }

    StringBuilder str_cmd = new StringBuilder();
    string GetRoutList = string.Empty;
    Int32 JobGrade = Int32.MinValue;//紀錄目前該部門找到的最大職級，用來判斷是否要繼續往上層部門走

    str_cmd.Length = 0;
    str_cmd.AppendLine("select AccountID,JobGrade");
    str_cmd.AppendLine("from dbo.FSe7en_Org_MemberStruct  with(nolock)");
    str_cmd.AppendLine("where DeptID = @DeptID and Enabled = 1 and ApproveRight = 1 and JobGrade > @MemberJobGrade and JobGrade<=70 and Version = @Version");
    str_cmd.AppendLine("order by JobGrade");
    SqlParameter[] SP1 = {
                               new SqlParameter("@DeptID",DeptID),
                               new SqlParameter("@MemberJobGrade",MemberJobGrade),
                               new SqlParameter("@MaxJobGrade",MaxJobGrade),
                               new SqlParameter("@Version",VersionNum)
                           };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), SP1, DBName))
    {
      if (dr.HasRows)
      {
        while (dr.Read())
        {
          JobGrade = Convert.ToInt32(dr["JobGrade"].ToString());
          //如果還在最大職等範圍內則加入選單
          if (MaxJobGrade >= JobGrade)
          {
            GetRoutList = GetRoutList + "{\"AccountID\":\"" + GetAbsentAgentList(dr["AccountID"].ToString().Trim()) + "\",\"Grade\":\"" + dr["JobGrade"].ToString() + "\",\"DeptID\":\"" + DeptID + "\"},";
            //GetRoutList = GetRoutList + GetAbsentAgentList(dr["AccountID"].ToString().Trim()) + ";";

            if (MaxJobGrade == JobGrade)//若已經找到符合相同層級的，就不要再找超過職等的
            {
              Escalate_Search = "N";
            }
          }
          //若已經超過最大職等且還沒有找到主管簽核，但Escalate_Search等於Y，可以允許在往上找到大於MaxJobGrade的一位主管簽核
          else if (Escalate_Search == "Y" && MemberJobGrade < JobGrade)
          {
            GetRoutList = GetRoutList + "{\"AccountID\":\"" + GetAbsentAgentList(dr["AccountID"].ToString().Trim()) + "\",\"Grade\":\"" + dr["JobGrade"].ToString() + "\",\"DeptID\":\"" + DeptID + "\"},";

            MaxJobGrade = MemberJobGrade;
            Escalate_Search = "";
          }
          MemberJobGrade = JobGrade;
        }
      }
      else
      {
        JobGrade = MemberJobGrade;
      }
    }

    //如果這層部門的最大職級已經超過設定的上限，就結束；否則就繼續往上層部門走
    if (MaxJobGrade <= JobGrade)
    {
      return GetRoutList;
    }
    else
    {
      str_cmd.Length = 0;

      str_cmd.AppendLine("select ParentDept");
      str_cmd.AppendLine("from dbo.FSe7en_Org_DeptStruct with(nolock)");
      str_cmd.AppendLine("where deptID = @DeptID and Enabled = 1 and Version = @Version");
      SqlParameter[] SP2 = {
                               new SqlParameter("@DeptID",DeptID),
                               new SqlParameter("@Version",VersionNum)
                           };

      using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), SP2, DBName))
      {
        if (dr.Read())
        {
          //若上層部門設定為NULL，則結束
          if (Convert.IsDBNull(dr["ParentDept"]))
          {
            return GetRoutList;
          }
          else
          {
            //若上層部門設定為空或等於董事會，則結束
            if (dr["ParentDept"].ToString().Trim() == "" || dr["ParentDept"].ToString() == "C0100000")
            {
              return GetRoutList;
            }
            else
            {
              return GetRoutList + GetRouteByJobGrade_JSON_Recursive(dr["ParentDept"].ToString().Trim(), JobGrade, MaxJobGrade, Escalate_Search);
            }
          }
        }
        else
        {
          //若找不到上層部門，則結束
          return GetRoutList;
        }
      }
    }

  }

  /// <summary>
  /// 找到該部門的主管
  /// </summary>
  /// <param name="DeptID">部門ID</param>
  /// <returns>回傳人員的AccountID</returns>
  public string GetDeptChief(string DeptID)
  {
    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select TOP(1) AccountID");
    str_cmd.AppendLine("from FSe7en_Org_MemberStruct with(nolock)");
    str_cmd.AppendLine("where DeptID = @DeptID and Version = @Version");
    str_cmd.AppendLine("order by jobGrade desc,IsMainJob desc");

    SqlParameter[] p = {
                           new SqlParameter("@DeptID",DeptID),
                           new SqlParameter("@Version",VersionNum)
                           };

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
  /// 取得於EIP系統內，有請假人員的代理人員內容
  /// 請假人員的員編:LeaveEmployeeID
  /// 代理人員的員編:AgentEmployeeID
  /// </summary>
  /// <returns></returns>
  public string GetAbsentAgentList(string LeaveAccountID)
  {
    string str_Return = LeaveAccountID;

    StringBuilder str_cmd = new StringBuilder();

    str_cmd.AppendLine("select AgentID");
    str_cmd.AppendLine("from dbo.F7Organ_View_AbsentAgent with(nolock)");
    str_cmd.AppendLine("where status = 1 and AccountID = @AccountID");

    SqlParameter[] SP1 = {
            new SqlParameter("@AccountID",LeaveAccountID)
        };

    using (SqlDataReader dr = MSSQL.GetDataRead(str_cmd.ToString(), SP1, DBName))
    {
      if (dr.Read())
      {
        str_Return = dr["AgentID"].ToString();
      }
    }

    return str_Return;
  }
}