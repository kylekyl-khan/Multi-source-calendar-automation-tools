using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
public partial class AuthorityManager : System.Web.UI.Page
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
            Panel1.Visible = false;
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Panel1.Visible == false)
        {
            Panel1.Visible = true;
        }
        else
        {
            Panel1.Visible = false;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        ClassBasic basic = new ClassBasic();
        string DBName = "DB_Tea_VToSchool";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBName].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();

        str_cmd.AppendLine("declare @count int;");
        str_cmd.AppendLine("declare @chk nvarchar(1);");
        str_cmd.AppendLine("select @count=count(*) from Sys_SetAdmin");
        str_cmd.AppendLine("where EmployeeID=@EmployeeID");

        str_cmd.AppendLine("if(@count=0)");
        str_cmd.AppendLine("begin");
        str_cmd.AppendLine("insert into Sys_SetAdmin");
        str_cmd.AppendLine("(EmployeeID,Name,AdminLevel)");
        str_cmd.AppendLine("values(@EmployeeID,@Name,'管理')");
        str_cmd.AppendLine("set @chk=1;");
        str_cmd.AppendLine("end");
        str_cmd.AppendLine("else");
        str_cmd.AppendLine("begin");
        str_cmd.AppendLine("set @chk=0;");
        str_cmd.AppendLine("end");
        str_cmd.AppendLine("select @chk as checkValue");
        SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
        cmd.Parameters.AddWithValue("@EmployeeID", TextBox1.Text.Trim());
        cmd.Parameters.AddWithValue("@Name", TextBox2.Text.Trim());
        
        SqlDataReader dr;

        try
        {
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                if (dr["checkValue"].ToString() == "1")
                {
                    basic.Script_AlertMsg(this.Page, "新增成功!");
                    TextBox1.Text = "";
                    TextBox2.Text = "";
                    Panel1.Visible = false;
                }
                else
                {
                    basic.Script_AlertMsg(this.Page, "此員編已存在，請重新輸入!");
                }
            }
            SqlDataSource1.SelectCommand = "SELECT EmployeeID,Name,AdminLevel from Sys_SetAdmin";
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            basic.Script_AlertMsg(this.Page, string.Format("{0}\n{1}", Resources.Resource.ErrorMsg8, ex.Message));
            return;
        }
        finally
        {
            cmd.Cancel();
            cn.Close();
            cn.Dispose();
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        TextBox2.Text = "";
        Panel1.Visible = false;
    }
}