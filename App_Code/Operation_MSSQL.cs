using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Configuration;

/// <summary>
/// MS_SQL連線相關
/// </summary>
public class Operation_MSSQL
{
    #region 資料庫交易失敗可復原的寫法!!!
    //DECLARE @chk tinyint
    //SET @chk = 0
    //Begin Transaction [Trans_Name] -- Trans_Name 交易名稱可自訂或者是不寫

    //    -- 可編寫多個 SQL 指令
    //    INSERT INTO [Table_Name] VALUES( 'Field_Value_1' );
    //    IF @@Error <> 0 BEGIN SET @chk = 1 END

    //    INSERT INTO [Table_Name] VALUES( 'Field_Value_2' );
    //    IF @@Error <> 0 BEGIN SET @chk = 1 END

    //IF @chk <> 0 BEGIN -- 若是新增資料發生錯誤
    //    Rollback Transaction [Trans_Name] -- 復原所有操作所造成的變更
    //END
    //ELSE BEGIN
    //    Commit Transaction [Trans_Name] -- 提交所有操作所造成的變更
    //END

    ////設定交易還原的寫法
    //    str_cmd.AppendLine("DECLARE @chk tinyint;");
    //    str_cmd.AppendLine("SET @chk = 0;");
    //    str_cmd.AppendLine("Begin Transaction [Trans_Name]");


    //    str_cmd.AppendLine("IF @@Error <> 0 BEGIN SET @chk = 1 END");

    ////判斷是否產生交易錯誤
    //    str_cmd.AppendLine("IF @chk <> 0 BEGIN -- 若是新增資料發生錯誤");
    //    str_cmd.AppendLine("    Rollback Transaction [Trans_Name] -- 復原所有操作所造成的變更");
    //    str_cmd.AppendLine("END");
    //    str_cmd.AppendLine("ELSE BEGIN");
    //    str_cmd.AppendLine("    Commit Transaction [Trans_Name] -- 提交所有操作所造成的變更");
    //    str_cmd.AppendLine("END");
    #endregion

    public Operation_MSSQL()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    /// <summary>
    /// 建立網頁資料庫連線
    /// </summary>
    /// <param name="DBName">資料庫名稱</param>
    /// <returns>SqlConnection</returns>
    public SqlConnection GetConn(string DBName)
    {
        try
        {
            SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBName].ToString());
            return Conn;
        }
        catch
        {
            return null;
        }
    }

    ///// <summary>
    ///// 建立應用程式資料庫連線
    ///// </summary>
    ///// <param name="DBName">資料庫名稱</param>
    ///// <returns>SqlConnection</returns>
    //public SqlConnection GetConn(string ConnectionString)
    //{
    //    try
    //    {
    //        SqlConnection Conn = new SqlConnection(ConnectionString);
    //        return Conn;
    //    }
    //    catch
    //    {
    //        return null;
    //    }
    //}

    /// <summary>
    /// 執行SqlNonQuery
    /// </summary>
    /// <param name="sql_Str">SQL指令</param>
    /// <param name="DBName">資料庫名稱</param>
    public void GetCommand(string sql_Str, string DBName)
    {
        using (SqlConnection Conn = this.GetConn(DBName))
        {
            try
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql_Str, Conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                throw;
            }
        }
    }

    /// <summary>
    /// 執行SqlNonQuery，代參數
    /// </summary>
    /// <param name="str_cmd">SQL指令</param>
    /// <param name="myParams">SQL參數</param>
    /// <param name="DBName">資料庫名稱</param>
    public void GetCommand(string str_cmd, SqlParameter[] myParams, string DBName)
    {
        using (SqlConnection Conn = this.GetConn(DBName))
        {
            try
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(str_cmd, Conn))
                {
                    cmd.Parameters.AddRange(myParams);
                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch
            {
                throw;
            }
        }
    }

    /// <summary>
    /// 執行SqlDataReader
    /// </summary>
    /// <param name="str_cmd">SQL指令</param>
    /// <param name="DBName">資料庫名稱</param>
    /// <returns>SqlDataReader</returns>
    public SqlDataReader GetDataRead(string str_cmd, string DBName)
    {

        SqlConnection Conn = this.GetConn(DBName);
        Conn.Open();
        SqlCommand cmd = new SqlCommand(str_cmd, Conn);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        return dr;
    }

    /// <summary>
    /// 執行SqlDataReader，代參數
    /// </summary>
    /// <param name="str_cmd">SQL指令</param>
    /// <param name="myParams">SQL參數</param>
    /// <param name="DBName">資料庫名稱</param>
    /// <returns>SqlDataReader</returns>
    public SqlDataReader GetDataRead(string str_cmd, SqlParameter[] myParams, string DBName)
    {

        SqlConnection Conn = this.GetConn(DBName);
        Conn.Open();
        SqlCommand cmd = new SqlCommand(str_cmd, Conn);
        cmd.Parameters.AddRange(myParams);
        SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        cmd.Parameters.Clear();
        return dr;
    }


    /// <summary>
    /// 執行SQLScalar
    /// </summary>
    /// <param name="str_cmd">SQL指令</param>
    /// <param name="DBName">資料庫名稱</param>
    /// <returns>scalar</returns>
    public object GetScalar(string str_cmd, string DBName)
    {
        using (SqlConnection Conn = this.GetConn(DBName))
        {
            try
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(str_cmd, Conn))
                {
                    Object scalar = cmd.ExecuteScalar();
                    return scalar;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 執行SQLScalar，代參數
    /// </summary>
    /// <param name="str_cmd">SQL指令</param>
    /// <param name="myParams">SQL參數</param>
    /// <param name="DBName">資料庫名稱</param>
    /// <returns>scalar</returns>
    public object GetScalar(string str_cmd, SqlParameter[] myParams, string DBName)
    {
        using (SqlConnection Conn = this.GetConn(DBName))
        {
            try
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(str_cmd, Conn))
                {
                    cmd.Parameters.AddRange(myParams);
                    Object scalar = cmd.ExecuteScalar();
                    cmd.Parameters.Clear();
                    return scalar;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 執行SqlDataReader
    /// </summary>
    /// <param name="str_cmd">SQL指令</param>
    /// <param name="DBName">資料庫名稱</param>
    /// <returns>DataTable</returns>
    public DataTable GetDataTable(string str_cmd, string DBName)
    {
        using (SqlConnection Conn = this.GetConn(DBName))
        {
            try
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(str_cmd, Conn))
                {
                    DataTable ta = new DataTable();
                    ta.Load(cmd.ExecuteReader());
                    return ta;
                }
            }
            catch
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 執行SqlDataReader，代參數
    /// </summary>
    /// <param name="str_cmd">SQL指令</param>
    /// <param name="myParams">SQL參數</param>
    /// <param name="DBName">資料庫名稱</param>
    /// <returns>DataTable</returns>
    public DataTable GetDataTable(string str_cmd, SqlParameter[] myParams, string DBName)
    {
        using (SqlConnection Conn = this.GetConn(DBName))
        {
            try
            {
                Conn.Open();
                using (SqlCommand cmd = new SqlCommand(str_cmd, Conn))
                {
                    cmd.Parameters.AddRange(myParams);

                    DataTable ta = new DataTable();
                    ta.Load(cmd.ExecuteReader());
                    cmd.Parameters.Clear();
                    return ta;
                }
            }
            catch
            {
                return null;
            }
        }
    }

}