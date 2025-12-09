using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Text.RegularExpressions;
using System.Diagnostics;

/// <summary>
/// ClassBasic 的摘要描述
/// </summary>
public class ClassBasic
{
    public ClassBasic()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    #region  JS相關
    /// <summary>
    /// 導引網頁
    /// </summary>
    /// <param name="page">運行的網頁</param>
    /// <param name="Href">前往的網頁</param>
    public void Script_Href(Page page, string Href)
    {
        string JSCode = "location.href='" + Href + "';";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "Href", JSCode, true);
    }

    /// <summary>
    /// 跳出警告視窗
    /// </summary>
    /// <param name="page">運行的網頁</param>
    /// <param name="Msg">顯示的訊息</param>
    public void Script_AlertMsg(Page page, string Msg)
    {
        string JSCode = "alert('" + Msg + "');";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "AlertMsg", JSCode, true);
    }

    /// <summary>
    /// 跳出警告視窗後導引網頁
    /// </summary>
    /// <param name="page">運行的網頁</param>
    /// <param name="Msg">顯示的訊息</param>
    /// <param name="Href">前往的網頁</param>
    public void Script_AlertHref(Page page, string Msg, string Href)
    {
        string JSCode = "alert('" + Msg + "');location.href='" + Href + "';";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "AlertHref", JSCode, true);
    }

    /// <summary>
    /// 執行JSCode
    /// </summary>
    /// <param name="page">運行的網頁</param>
    /// <param name="JSCode">JS程式碼</param>
    public void Script_JSCode(Page page, string JSCode)
    {
        ScriptManager.RegisterStartupScript(page, page.GetType(), "JSCode", JSCode, true);
    }

    /// <summary>
    /// 跳出警告視窗後關閉網頁
    /// </summary>
    /// <param name="page">運行的網頁</param>
    /// <param name="Msg">顯示的訊息</param>
    public void Script_CloseWindowMsg(Page page, string Msg)
    {
        string JSCode = "alert('" + Msg + "');window.open('', '_self', '');window.close();";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "JSCode", JSCode, true);
    }

    /// <summary>
    /// 關閉網頁
    /// </summary>
    /// <param name="page">運行的網頁</param>
    public void Script_CloseWindow(Page page)
    {
        string JSCode = "window.open('', '_self', '');window.close();";
        ScriptManager.RegisterStartupScript(page, page.GetType(), "JSCode", JSCode, true);
    }
    #endregion

    #region 正則表示式
    /// <summary>
    /// 正整數判斷(包含0)
    /// </summary>
    /// <param name="str">需判斷的內容</param>
    /// <returns>回傳判斷結果</returns>
    public bool IsNonNegativeIntegers(String str)
    {
        Regex NumberPattern = new Regex("^\\d+$");
        return NumberPattern.IsMatch(str);
    }

    /// <summary>
    /// 正浮點數判斷(包含0)
    /// </summary>
    /// <param name="str">需判斷的內容</param>
    /// <returns>回傳判斷結果</returns>
    public bool IsNonNegativeFloat(String str)
    {
        Regex NumberPattern = new Regex("^\\d+(\\.\\d+)?$");
        return NumberPattern.IsMatch(str);
    }

    /// <summary>
    /// 整數判斷
    /// </summary>
    /// <param name="str">需判斷的內容</param>
    /// <returns>回傳判斷結果</returns>
    public bool IsIntegers(String str)
    {
        try
        {
            Convert.ToInt32(str);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 浮點數判斷
    /// </summary>
    /// <param name="str">需判斷的內容</param>
    /// <returns>回傳判斷結果</returns>
    public bool IsDouble(String str)
    {
        try
        {
            Convert.ToDouble(str);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Email判斷
    /// </summary>
    /// <param name="str">需判斷的內容</param>
    /// <returns>回傳判斷結果</returns>
    public bool IsEmail(String str)
    {
        Regex NumberPattern = new Regex("^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$");
        return NumberPattern.IsMatch(str);
    }

    /// <summary>
    /// 日期判斷
    /// </summary>
    /// <param name="str">需判斷的內容</param>
    /// <returns>回傳判斷結果</returns>
    public bool IsDate(String str)
    {
        try
        {
            Convert.ToDateTime(str);
            return true;
        }
        catch
        {
            return false;
        }
    }
    #endregion

    /// <summary>
    /// 回傳學校的學年學期
    /// </summary>
    /// <param name="Date">判斷該時間的學年學期</param>
    /// <returns>回傳 XXX-X</returns>
    public string SchoolYearSeme(DateTime Date)
    {
        int Year = Date.Year;
        int Month = Date.Month;

        int SchoolYear = Year - 1911 - 1;
        int SchoolSeme = 1;
        if (Month >= 2 && Month <= 7)
        {
            SchoolSeme = 2;
        }
        else if (Month >=8)
        {
            SchoolYear = Year - 1911;
        }

        return SchoolYear + "-" + SchoolSeme;

    }

    /// <summary>
    /// 回傳該使用的IP資訊
    /// </summary>
    /// <returns>IP資訊</returns>
    public string GetClientIP()
    {
        string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (null == result || (result != null && String.IsNullOrEmpty(result)))
        {
            result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
        if (null == result || (result != null && String.IsNullOrEmpty(result)))
        {
            result = HttpContext.Current.Request.UserHostAddress;
        }

        return result.Split(new Char[] { ',' })[0];
    }
    public string GetUserCulture()
    {
        var culture = System.Threading.Thread.CurrentThread.CurrentCulture;

        // 有時候client request 會以 zh-Hant回傳, 導致無法吃到zh-TW的情況, 刻意讓他找文字的文化
        culture = System.Globalization.CultureInfo.GetCultureInfo(culture.TextInfo.CultureName);

        return culture.ToString();
    }
}