using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class edit_flow : System.Web.UI.Page
{
    ClassBasic basic = new ClassBasic();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["EmployeeID"] == null)
            {
                Session.RemoveAll();
                Session.Clear();
                Response.Redirect("Logon.aspx");
                return;
            }
            Panel2.Visible = false;
            DropDownList1.DataBind();
            Label6.Text = DropDownList1.SelectedItem.Text;
        }
    }
    //校區選擇
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList1.DataBind();
        GridView1.DataBind();
        Label6.Text = DropDownList1.SelectedItem.Text;
    }
    //流程選擇
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
        Label6.Text = DropDownList1.SelectedItem.Text;
    }
    //+新增簽認順序 超連結
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (LinkButton1.Text == "+新增通知人員")
        {
            LinkButton1.Text = "-新增通知人員";
            Panel2.Visible = true;
        }
        else
        {
            LinkButton1.Text = "+新增通知人員";
            Panel2.Visible = false;
        }
    }
    //新增簽認順序 按下新增觸發
    protected void Button5_Click(object sender, EventArgs e)
    {
        if(TextBox10.Text==string.Empty)
        {
            basic.Script_AlertMsg(this.Page, "請填寫員編!");
            return;
        }
        if (TextBox11.Text == string.Empty)
        {
            basic.Script_AlertMsg(this.Page, "請填寫姓名!");
            return;
        }
        string DBname = "DB_Tea_VToSchool";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();
        //--設定交易還原的寫法
        str_cmd.AppendLine("DECLARE @chk tinyint;");
        str_cmd.AppendLine("SET @chk = 0;");
        str_cmd.AppendLine("Begin Transaction [Trans_Name]");

        str_cmd.AppendLine("declare @outputString nvarchar(4);");
        str_cmd.AppendLine("  set @outputString='新增成功'");
        str_cmd.AppendLine("  insert into Sys_V_NoticePeople(Campus,VTypeID,EmployeeID,Name)");
        str_cmd.AppendLine("  values(@Campus,@VTypeID,@EmployeeID,@Name)");
        str_cmd.AppendLine("  IF @@Error <> 0 BEGIN SET @chk = 1 END");

        str_cmd.AppendLine("select @outputString as outputString");
        //--判斷是否產生交易錯誤
        str_cmd.AppendLine("IF @chk <> 0 BEGIN -- 若是新增資料發生錯誤");
        str_cmd.AppendLine("    Rollback Transaction [Trans_Name] -- 復原所有操作所造成的變更");
        str_cmd.AppendLine("END");
        str_cmd.AppendLine("ELSE BEGIN");
        str_cmd.AppendLine("    Commit Transaction [Trans_Name] -- 提交所有操作所造成的變更");
        str_cmd.AppendLine("END");
        SqlCommand cmd = new SqlCommand(str_cmd.ToString(),cn);
        cmd.Parameters.Add(new SqlParameter("@Campus", RadioButtonList1.SelectedValue));
        cmd.Parameters.Add(new SqlParameter("@VTypeID", DropDownList1.SelectedValue));
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", TextBox10.Text.TrimEnd()));
        cmd.Parameters.Add(new SqlParameter("@Name", TextBox11.Text.TrimEnd()));
        SqlDataReader dr = cmd.ExecuteReader();
        try
        {
            if (dr.Read())
            {
                if (dr["outputString"].ToString() == "新增成功")
                {
                    basic.Script_AlertMsg(this.Page, "新增成功!");
                    GridView1.DataBind();
                    LinkButton1_Click(sender, e);
                    TextBox10.Text = "";
                    TextBox11.Text = "";
                }
                else
                {
                    basic.Script_AlertMsg(this.Page, "新增失敗!有重複的簽認順序。");
                }
            }
        }
        catch
        {
            basic.Script_AlertMsg(this.Page, "資料庫執行異常。請洽 #8297 董光哲(James)!");
        }
        finally
        {
            dr.Close();
            cmd.Cancel();
            cn.Close();
            cn.Dispose();
        }
    }
    protected void TextBox10_TextChanged(object sender, EventArgs e)
    {
        string DBname = "DB_MisAdmin";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("select Name");
        str_cmd.AppendLine("from Sys_Interinfo_Person_V");
        str_cmd.AppendLine("where EmployeeID=@EmployeeID");

        SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
        cmd.Parameters.AddWithValue("@EmployeeID", TextBox10.Text.TrimEnd());
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            TextBox11.Text = dr["Name"].ToString();
        }
        else
        {
            basic.Script_AlertMsg(this.Page, "查無此員編，請確認!");
        }
        dr.Close();
        cmd.Cancel();
        cn.Close();
        cn.Dispose();
        str_cmd.Length = 0;
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        string FlowNo = GridView1.Rows[GridView1.EditIndex].Cells[2].Text;
        string DBname = "DB_Tea_VToSchool";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("@declare @count int;");
        str_cmd.AppendLine("select @count=count(*)");
        str_cmd.AppendLine("from Sys_V_NoticePeople");
        str_cmd.AppendLine("where Campus=@Campus");
        str_cmd.AppendLine("and VTypeID=@VTypeID");
        str_cmd.AppendLine("and EmployeeID=@EmployeeID");

        str_cmd.AppendLine("select @count as count");
        SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
        cmd.Parameters.AddWithValue("@Campus", RadioButtonList1.SelectedValue);
        cmd.Parameters.AddWithValue("@VTypeID", DropDownList1.SelectedValue);
        cmd.Parameters.AddWithValue("@EmployeeID", TextBox10.Text.TrimEnd());

        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (Convert.ToInt32(dr["count"].ToString()) > 0)
            {
                e.Cancel = true;
                basic.Script_AlertMsg(this.Page, "已有重複資料，新增失敗!");
            }
        }
        dr.Close();
        cmd.Cancel();
        cn.Close();
        cn.Dispose();
    }
}