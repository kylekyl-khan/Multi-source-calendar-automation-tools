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
using System.Web.Configuration;

public partial class AdminTest : System.Web.UI.Page
{
    //test2
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
                Session.RemoveAll();
                Session.Clear();
                Response.Redirect("Logon.aspx");
            }
            showGridView1(TextBox1.Text);
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        showGridView1(TextBox1.Text);
    }
    protected void showGridView1(string input)
    {
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("select EmployeeID as 員編,Name as 姓名 from Sys_Interinfo_Person_V");
        str_cmd.AppendLine("where (outdate is null or outdate+1>getdate())");
        str_cmd.AppendLine("and (EmployeeID like '%" + input + "%' or Name like '%" + input + "%')");
        str_cmd.AppendLine("and Campus<>''");
        SqlDataSource1.SelectCommand = str_cmd.ToString();
        GridView1.DataBind();
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Button bt = (Button)sender;
        GridViewRow gv_row = (GridViewRow)bt.NamingContainer;
        string sno = gv_row.Cells[0].Text;
        changeStatus(sno);
    }
    protected void changeStatus(string sno)
    {
        ClassBasic basic = new ClassBasic();
        bool isOK = false;
        string adminLevel = "0";
        string DBname = "DB_MisAdmin";
        string DBname2 = "DB_Tea_VToSchool";
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings
            [DBname].ConnectionString.ToString());
        SqlConnection conn2 = new SqlConnection(ConfigurationManager.ConnectionStrings
            [DBname2].ConnectionString.ToString());
        conn.Open();
        conn2.Open();
        //判斷人員資料有無在hcp系統裡
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("select EmployeeID,Name,AccountID,Campus,DeptName");
        str_cmd.AppendLine("from Sys_Interinfo_Person_V");
        str_cmd.AppendLine("where EmployeeID='" + sno + "' and (outdate is null or outdate + 1 > GETDATE())");
        using (SqlCommand cmd = new SqlCommand(str_cmd.ToString(), conn))
        {
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Session["snameC"] = dr["Name"].ToString();
                Session["AccountID"] = dr["AccountID"].ToString();
                Session["Name"] = dr["Name"].ToString();
                Session["EmployeeID"] = dr["EmployeeID"].ToString();
                Session["Campus"] = dr["Campus"].ToString();
                Session["DeptName"] = dr["DeptName"].ToString();

                isOK = true;
            }
            else
            {
                basic.Script_AlertMsg(this.Page, "無此人員資料!無法變更!");
                return;
            }
            dr.Close();
        }
        //判斷是否為管理者 權限為何
        if (isOK)
        {
            StringBuilder str_cmd2 = new StringBuilder();
            str_cmd2.AppendLine("select EmployeeID,AdminLevel");
            str_cmd2.AppendLine("from Sys_SetAdmin");
            str_cmd2.AppendLine("where EmployeeID='" + Session["EmployeeID"].ToString() + "'");
            using (SqlCommand cmd = new SqlCommand(str_cmd2.ToString(), conn2))
            {
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    adminLevel = dr["AdminLevel"].ToString();
                }
                dr.Close();
            }
        }
        Session["AdminLevel"] = adminLevel;
        conn.Close();
        conn.Dispose();
        conn2.Close();
        conn2.Dispose();
        basic.Script_AlertHref(this.Page,"轉換成功!變更身分為:" + Session["snameC"].ToString(),"PersonalList.aspx");
    }
}