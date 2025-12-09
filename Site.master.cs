using System;
using System.Web;

public partial class Site : System.Web.UI.MasterPage
{
  protected void Page_Load(object sender, EventArgs e)
  {
    if (HttpContext.Current.Request.IsLocal || HttpContext.Current.Server.MachineName.Contains("TEST"))
    {
      if(!Label2.Text.Contains("(Demo)"))
        Label2.Text += "(Demo)";
    }
  }
}
