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
            insertTable_bind();
            Panel3.Visible = false;
            GridView3.Visible = false;
        }
    }
    //校區選擇
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList8.SelectedValue = RadioButtonList1.SelectedValue;
        DropDownList8_SelectedIndexChanged(sender,e);
        DropDownList1.DataBind();
        GridView1.DataBind();
        insertTable_bind();
    }
    //流程選擇
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView1.DataBind();
        insertTable_bind();
    }
    //新增簽認順序的Table
    protected void insertTable_bind()
    {
        if(DropDownList1.Items.Count<1)
        {
            Panel4.Visible = false;
            return;
        }
        Panel4.Visible = true;
        Label6.Text = DropDownList1.SelectedItem.Text;
        Label5.Text = DropDownList1.SelectedValue;
        string DBname = "DB_Tea_VToSchool";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("select max(Series)+1 as nextSeries");
        str_cmd.AppendLine("from Sys_V_Flow2");
        str_cmd.AppendLine("where FlowNo=@FlowNo");
        SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
        cmd.Parameters.AddWithValue("@FlowNo", Label5.Text);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (dr["nextSeries"] == DBNull.Value)
            {
                DropDownList7.SelectedValue = "1";
            }
            else
            {
                DropDownList7.SelectedValue = dr["nextSeries"].ToString();
            }
        }
        dr.Close();
        cmd.Cancel();
        cn.Close();
        cn.Dispose();
    }
    //+新增簽認順序 超連結
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (LinkButton1.Text == "+新增簽認順序")
        {
            LinkButton1.Text = "-新增簽認順序";
            Panel2.Visible = true;
        }
        else
        {
            LinkButton1.Text = "+新增簽認順序";
            Panel2.Visible = false;
        }
    }
    //新增簽認順序 按下新增觸發
    protected void Button5_Click(object sender, EventArgs e)
    {
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
        str_cmd.AppendLine("  insert into Sys_V_Flow2(FlowNo,Series,EmployeeID,Name)");
        str_cmd.AppendLine("  values(@FlowNo,@Series,@EmployeeID,@Name)");
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
        cmd.Parameters.Add(new SqlParameter("@FlowNo", Label5.Text));
        cmd.Parameters.Add(new SqlParameter("@Series", DropDownList7.SelectedValue));
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", TextBox10.Text.ToUpper().TrimEnd()));
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
                    insertTable_bind();
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
    //+新增流程 超連結
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (LinkButton2.Text == "+新增流程")
        {
            LinkButton2.Text = "-新增流程";
            Panel3.Visible = true;
        }
        else
        {
            LinkButton2.Text = "+新增流程";
            Panel3.Visible = false;
            GridView3.Visible = false;
        }
    }
    //新增流程 按下新增觸發
    protected void Button6_Click(object sender, EventArgs e)
    {
        string DBname = "DB_Tea_VToSchool";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();
        //--設定交易還原的寫法
        str_cmd.AppendLine("DECLARE @chk tinyint;");
        str_cmd.AppendLine("SET @chk = 0;");
        str_cmd.AppendLine("Begin Transaction [Trans_Name]");

        str_cmd.AppendLine("declare @num int;");
        str_cmd.AppendLine("declare @outputString nvarchar(4);");

        str_cmd.AppendLine("select @num=count(*)");
        str_cmd.AppendLine("from Sys_V_Flow");
        str_cmd.AppendLine("where FlowNo=@FlowNo");

        str_cmd.AppendLine("if(@num>0)");
        str_cmd.AppendLine("begin");
        str_cmd.AppendLine("  set @outputString='新增失敗'");
        str_cmd.AppendLine("end");
        str_cmd.AppendLine("else");
        str_cmd.AppendLine("begin");
        str_cmd.AppendLine("  set @outputString='新增成功'");
        str_cmd.AppendLine("  insert into Sys_V_Flow(FlowName,FlowNo,Campus)");
        str_cmd.AppendLine("  values(@FlowName,@FlowNo,@Campus)");
        str_cmd.AppendLine("  IF @@Error <> 0 BEGIN SET @chk = 1 END");
        str_cmd.AppendLine("end");

        str_cmd.AppendLine("select @outputString as outputString");
        //--判斷是否產生交易錯誤
        str_cmd.AppendLine("IF @chk <> 0 BEGIN -- 若是新增資料發生錯誤");
        str_cmd.AppendLine("    Rollback Transaction [Trans_Name] -- 復原所有操作所造成的變更");
        str_cmd.AppendLine("END");
        str_cmd.AppendLine("ELSE BEGIN");
        str_cmd.AppendLine("    Commit Transaction [Trans_Name] -- 提交所有操作所造成的變更");
        str_cmd.AppendLine("END");
        SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
        cmd.Parameters.Add(new SqlParameter("@FlowNo", TextBox13.Text.TrimEnd()));
        cmd.Parameters.Add(new SqlParameter("@FlowName", TextBox12.Text.TrimEnd()));
        cmd.Parameters.Add(new SqlParameter("@Campus", DropDownList8.SelectedValue));
        SqlDataReader dr = cmd.ExecuteReader();
        try
        {
            if (dr.Read())
            {
                if (dr["outputString"].ToString() == "新增成功")
                {
                    basic.Script_AlertMsg(this.Page, "新增成功!");
                    RadioButtonList1.SelectedValue = DropDownList8.SelectedValue;
                    DropDownList1.DataBind();
                    DropDownList1.SelectedValue = TextBox13.Text.TrimEnd();
                    GridView1.DataBind();
                    insertTable_bind();
                    GridView3.Visible = false;
                    Panel4.Visible = true;
                    Panel3.Visible = false;
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
   
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        if(GridView3.Visible ==true)
        {
            GridView3.Visible = false;
        }
        else
        {
            GridView3.Visible = true;
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
        Label sid = (Label)GridView1.Rows[GridView1.EditIndex].Cells[3].FindControl("Label8");
        DropDownList Series = (DropDownList)GridView1.Rows[GridView1.EditIndex].Cells[3].FindControl("DropDownList9");
        string DBname = "DB_Tea_VToSchool";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("declare @oldSeries int;");
        str_cmd.AppendLine("select @oldSeries=Series");
        str_cmd.AppendLine("from Sys_V_Flow2");
        str_cmd.AppendLine("where FlowNo=@FlowNo");
        str_cmd.AppendLine("and sid=@sid");
        str_cmd.AppendLine("if(@oldSeries<>@newSeries)"); //若新舊值相同就不用檢查是否重複
        str_cmd.AppendLine("begin");
        str_cmd.AppendLine("  select count(*) as count");
        str_cmd.AppendLine("  from Sys_V_Flow2");
        str_cmd.AppendLine("  where FlowNo=@FlowNo");
        str_cmd.AppendLine("  and Series=@newSeries");
        str_cmd.AppendLine("end");
        SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
        cmd.Parameters.AddWithValue("@FlowNo", FlowNo);
        cmd.Parameters.AddWithValue("@newSeries", Series.SelectedValue); //新值
        cmd.Parameters.AddWithValue("@sid", sid.Text);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            if (Convert.ToInt32(dr["count"].ToString()) > 0)
            {
                e.Cancel = true;
                basic.Script_AlertMsg(this.Page, "簽認順序重複，更新失敗!");
            }
        }
        dr.Close();
        cmd.Cancel();
        cn.Close();
        cn.Dispose();
    }

    protected void DropDownList8_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DropDownList8.SelectedValue=="秀岡")
        {
            TextBox13.Text = "XG_";
        }
        else if(DropDownList8.SelectedValue=="青山")
        {
            TextBox13.Text = "QS_";
        }
        else if(DropDownList8.SelectedValue=="新竹")
        {
            TextBox13.Text = "HS_";
        }
        else if(DropDownList8.SelectedValue=="林口")
        {
            TextBox13.Text = "LK_";
        }
    }
}