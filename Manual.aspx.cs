using System;

public partial class HistoryRecord : System.Web.UI.Page
{
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
    }
        //20200921 移除新竹校區訪客入校系統操作手冊_HC
        //if (Session["Campus"].ToString() == "新竹校區")
        //{
        //  HyperLink1.NavigateUrl = "~/訪客入校系統操作手冊_HC.pdf";
        //}
        //else
        //{
        //  HyperLink1.NavigateUrl = "~/訪客入校系統操作手冊.pdf";
        //}
    }
}