using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

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
            TextBox3.Text = DateTime.Today.ToString("yyyy/MM/01");
            TextBox4.Text = DateTime.Today.ToString("yyyy/MM/dd");
            //顯示訪客類別
            getVType();
            bind();
        }
    }
    //顯示需求單類別
    protected void getVType()
    {
        DropDownList3.Items.Clear();
        if (Session["UserCulture"].ToString() == "中文")
        {
            DropDownList3.Items.Add(new ListItem("全部類別", "全部類別"));
        }
        else
        {
            DropDownList3.Items.Add(new ListItem("All", "全部類別"));
        }
        string DBName = "DB_Tea_VToSchool";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBName].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();
        if (Session["UserCulture"].ToString() == "中文")
        {
            str_cmd.AppendLine("select VTypeName,VTypeID");
            str_cmd.AppendLine("from Sys_V_Type");
            str_cmd.AppendLine("where Campus=@campus");
        }
        else
        {
            str_cmd.AppendLine("select VTypeName,VTypeID");
            str_cmd.AppendLine("from Sys_V_Type");
            str_cmd.AppendLine("where Campus=@campus");
        }
        SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
        cmd.Parameters.AddWithValue("@campus", Session["Campus"].ToString().Substring(0, 2));

        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            if (Session["UserCulture"].ToString() == "中文")
            {
                DropDownList3.Items.Add(new ListItem(dr["VTypeName"].ToString(), dr["VTypeID"].ToString()));
            }
            else
            {
                DropDownList3.Items.Add(new ListItem(dr["VTypeName"].ToString(), dr["VTypeID"].ToString()));
            }
        }
        dr.Close();
        cmd.Cancel();
        cn.Close();
        cn.Dispose();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind();
    }

    protected void TextBox3_TextChanged(object sender, EventArgs e)
    {
        bind();
    }

    protected void TextBox4_TextChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void bind()
    {
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("SELECT a.ListNum,");
        str_cmd.AppendLine("case a.Campus when '秀岡' then '"+Resources.Resource.Xiugang+                                                     "' when '青山' then '"+Resources.Resource.Qingshan+
            "' when '林口' then '" + Resources.Resource.Linko +
                                       "' when '新竹' then '" +Resources.Resource.Hsinchu+                                                     "' end as Campus,");
        if (Session["UserCulture"].ToString() == "中文")
        {
            str_cmd.AppendLine("b.VTypeName as VTypeName,");
        }
        else
        {
            str_cmd.AppendLine("b.VTypeName as VTypeName,");
        }
        str_cmd.AppendLine("a.Apply_Datetime,");
        str_cmd.AppendLine("case a.ListStatus");
        str_cmd.AppendLine("     when '審核中' then '" + Resources.Resource.WaitingForApproval + 
                              "' when '已核准' then '" + Resources.Resource.Approved +
                              "' when '駁回' then '" + Resources.Resource.Rejected +
                              "' when '管理者撤銷' then '" + Resources.Resource.AdminDelete +
                              "' end as ListStatus");
        str_cmd.AppendLine("FROM List_V a");
        str_cmd.AppendLine("inner join Sys_V_Type b");
        str_cmd.AppendLine("on a.VTypeID = b.VTypeID");
        str_cmd.AppendLine("and a.Campus = b.Campus");
        str_cmd.AppendLine("where (a.Apply_Datetime >= '" + Convert.ToDateTime(TextBox3.Text).ToString("yyyy/MM/dd 00:00:00") + "' and a.Apply_Datetime <= '" + Convert.ToDateTime(TextBox4.Text).ToString("yyyy/MM/dd 23:59:59") + "')");
        str_cmd.AppendLine("and a.EmployeeID='"+Session["EmployeeID"].ToString()+"'");
        if (DropDownList3.SelectedValue != "全部類別")
        {
            str_cmd.AppendLine("and b.VTypeID='" + DropDownList3.SelectedValue + "'");
        }
        str_cmd.AppendLine("order by a.ListNum desc");
        SqlDataSource1.SelectCommand = str_cmd.ToString();
        GridView1.DataBind();
    }
}