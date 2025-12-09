using KCBS_SSO;
using System;
using System.Web;

public partial class Site2 : System.Web.UI.MasterPage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (HttpContext.Current.Request.IsLocal || HttpContext.Current.Server.MachineName.Contains("TEST"))
    {
      if (!Label9.Text.Contains("(Demo)"))
        Label9.Text += "(Demo)";
    }

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
      if (Session["AdminLevel"] == null)
      {
        Response.Redirect("Logon.aspx");
        return;
      }
      Panel_User.Visible = true;
      if (Session["AdminLevel"].ToString() == "管理")
      {
        Panel_Admin.Visible = true;
      }
    }

  }
  protected void Button1_Click(object sender, EventArgs e)
  {
    Session.Clear();
    SSOOperation SSOp = new SSOOperation();
    SSOp.Logout(this.Page, SSOp.DefaultLoginURL);
  }
}
