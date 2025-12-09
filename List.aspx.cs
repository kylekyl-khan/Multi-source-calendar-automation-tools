using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

public partial class List : System.Web.UI.Page
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
            TextBox3.Text = DateTime.Today.ToString("yyyy/MM/dd");
            TextBox4.Text = DateTime.Today.ToString("yyyy/MM/dd");
            //顯示訪客類別
            getVType();
            bind();
        }
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        bind();
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
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
        str_cmd.AppendLine("case a.Campus when '秀岡' then '" + Resources.Resource.Xiugang + 
                                       "' when '青山' then '" + Resources.Resource.Qingshan +
                                       "' when '林口' then '" + Resources.Resource.Linko +
                                       "' when '新竹' then '" + Resources.Resource.Hsinchu + 
                                       "' end as Campus,");
        if (Session["UserCulture"].ToString() == "中文")
        {
            str_cmd.AppendLine("b.VTypeName");
        }
        else
        {
            str_cmd.AppendLine("b.VTypeName");
        }
        str_cmd.AppendLine(",a.Name,a.Apply_Datetime,a.startTime,a.endTime,");
        str_cmd.AppendLine("case a.ListStatus when '審核中' then '"+Resources.Resource.WaitingForApproval + 
                                         "' when '已核准' then '" + Resources.Resource.Approved +
                                         "' when '駁回' then '" + Resources.Resource.Rejected +
                                         "' when '管理者撤銷' then '" + Resources.Resource.AdminDelete +
                                         "' end as ListStatus");
        str_cmd.AppendLine("FROM List_V a");
        str_cmd.AppendLine("inner join Sys_V_Type b");
        str_cmd.AppendLine("on a.VTypeID = b.VTypeID");
        str_cmd.AppendLine("and a.Campus = b.Campus");
        str_cmd.AppendLine("where (a.ListNum like'%"+TextBox1.Text+"%' or a.Name like'%"+TextBox1.Text+"%')");
        str_cmd.AppendLine("and ((a.startTime >= '" + Convert.ToDateTime(TextBox3.Text).ToString("yyyy/MM/dd 00:00:01") + "' and a.startTime <= '" + Convert.ToDateTime(TextBox4.Text).ToString("yyyy/MM/dd 23:59:00") + "')");
        str_cmd.AppendLine("or (a.endTime >= '" + Convert.ToDateTime(TextBox3.Text).ToString("yyyy/MM/dd 00:00:01") + "' and a.endTime <= '" + Convert.ToDateTime(TextBox4.Text).ToString("yyyy/MM/dd 23:59:00") + "')");
        str_cmd.AppendLine("or (a.startTime <= '" + Convert.ToDateTime(TextBox3.Text).ToString("yyyy/MM/dd 23:59:00") + "' and a.endTime >= '" + Convert.ToDateTime(TextBox4.Text).ToString("yyyy/MM/dd 00:00:01") + "'))");
        if (DropDownList3.SelectedValue!="所有校區")
        {
            str_cmd.AppendLine("and a.Campus = '" + DropDownList3.SelectedValue + "'");
        }
        if (DropDownList4.SelectedValue != "全部類別")
        {
            str_cmd.AppendLine("and b.VTypeID='" + DropDownList4.SelectedValue + "'");
        }
        str_cmd.AppendLine("order by a.startTime desc");
        SqlDataSource1.SelectCommand = str_cmd.ToString();
        GridView1.DataBind();
    }
    /// <summary> 
    /// 由 ViewState 還原控制項的狀態。 
    /// </summary> 
    /// <param name="savedState">要還原的控制項狀態。</param> 
    protected override void LoadViewState(object savedState)
    {
        if (savedState != null)
        {
            object[] myState = (object[])savedState;
            if (myState[0] != null)
            {
                base.LoadViewState(myState[0]);
            }
            if (myState[1] != null)
            {
                SqlDataSource1.SelectCommand = Convert.ToString(myState[1]);
            }
        }
    }
    /// <summary> 
    /// 控制項的狀態儲存至 ViewState 
    /// </summary> 
    /// <returns>含有控制項之目前檢視狀態的物件。</returns> 
    protected override object SaveViewState()
    {
        object baseSate = base.SaveViewState();
        object[] myState = new object[2];
        myState[0] = baseSate;
        myState[1] = SqlDataSource1.SelectCommand;
        return myState;
    }
    //顯示需求單類別
    protected void getVType()
    {
        DropDownList4.Items.Clear();
        if (Session["UserCulture"].ToString() == "中文")
        {
            DropDownList4.Items.Add(new ListItem("全部類別", "全部類別"));
        }
        else
        {
            DropDownList4.Items.Add(new ListItem("All", "全部類別"));
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
                DropDownList4.Items.Add(new ListItem(dr["VTypeName"].ToString(), dr["VTypeID"].ToString()));
            }
            else
            {
                DropDownList4.Items.Add(new ListItem(dr["VTypeName"].ToString(), dr["VTypeID"].ToString()));
            }
        }
        dr.Close();
        cmd.Cancel();
        cn.Close();
        cn.Dispose();
    }
}