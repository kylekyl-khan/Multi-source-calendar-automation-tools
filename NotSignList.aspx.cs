using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class NotSignList : System.Web.UI.Page
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
            if (Session["UserCulture"] == null)
            {
                Response.Redirect("Logon.aspx");
                return;
            }
            bind();
        }
    }
    protected void bind()
    {
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("SELECT a.ListNum,");
        str_cmd.AppendLine("case a.Campus when '秀岡' then '" + Resources.Resource.Xiugang +
                                       "' when '青山' then '" + Resources.Resource.Qingshan +
                                       "' when '林口' then '" + Resources.Resource.Linko +
                                       "' when '新竹' then '" + Resources.Resource.Hsinchu +
                                       "' end as Campus,");
        if (Session["UserCulture"].ToString() == "中文")
        {
            str_cmd.AppendLine("c.VTypeName");
        }
        else
        {
            str_cmd.AppendLine("c.VTypeName");
        }
        str_cmd.AppendLine(",a.Name,a.Apply_Datetime,a.startTime,");
        str_cmd.AppendLine("case a.ListStatus when '審核中' then '" + Resources.Resource.WaitingForApproval +
                                         "' when '已核准' then '" + Resources.Resource.Approved +
                                         "' when '駁回' then '" + Resources.Resource.Rejected +
                                         "' when '管理者撤銷' then '" + Resources.Resource.AdminDelete +
                                         "' end as ListStatus,");
        str_cmd.AppendLine("b.Name as nowAllow");
        str_cmd.AppendLine("FROM List_V a");
        str_cmd.AppendLine("inner join List_Allow b");
        str_cmd.AppendLine("on a.ListNum = b.ListNum");
        str_cmd.AppendLine("inner join Sys_V_Type c");
        str_cmd.AppendLine("on a.VTypeID = c.VTypeID");
        str_cmd.AppendLine("and a.Campus = c.Campus");
        str_cmd.AppendLine("where a.C_Level+1=B.Series");
        str_cmd.AppendLine("and b.EmployeeID = '"+Session["EmployeeID"].ToString()+"'");
        str_cmd.AppendLine("and b.SignTime is null");
        str_cmd.AppendLine("and (a.ListStatus='審核中' or a.ListStatus='已核准')");
        str_cmd.AppendLine("order by a.ListNum desc");
        SqlDataSource1.SelectCommand = str_cmd.ToString();
        GridView1.DataBind();
    }
}