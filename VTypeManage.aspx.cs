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
using System.Net.Mail;
using System.Web.Configuration;
using System.IO;

public partial class MemberFlow : System.Web.UI.Page
{
    ClassBasic basic = new ClassBasic();
    protected void Page_Load(object sender, EventArgs e)
    {
        Label3.Visible = false;
        if (!IsPostBack)
        {
            if (Session["EmployeeID"] == null)
            {
                Session.RemoveAll();
                Session.Clear();
                Response.Redirect("Logon.aspx");
                return;
            }

            search();
        }
        //20200921 新竹校區因幼兒園新增特殊流程
        if (RadioButtonList1.SelectedValue == "新竹")
        {
            VtoSchool VtoSchool = new VtoSchool();
            string NoticeName = string.Empty;
            string NoticeEmployeeID = string.Empty;
            VtoSchool.getNotice("新竹", "11", out NoticeName, out NoticeEmployeeID);


            Label3.Visible = true;
            Label3.Text = "<br />" + "新竹校區因幼兒園特殊流程關係，若流程代碼非normal5與normal8 將影響新竹校區幼兒園簽核流程及通知" + "<br />" +
                          "幼兒園部門提出的一般訪客、外校生家長、政府機關、廠商、媒體、在籍學生家長表單時，通知人員：" + "<br />" + NoticeName + "<br />";
            
        }
    }

    protected void search()
    {
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("SELECT a.sid,a.Campus,a.VTypeName,a.VTypeID,b.FlowName,a.FlowNo");
        str_cmd.AppendLine("from Sys_V_Type a");
        str_cmd.AppendLine("left join Sys_V_Flow b");
        str_cmd.AppendLine("on a.FlowNo = b.FlowNo");
        str_cmd.AppendLine("where a.Campus=@Campus");
        str_cmd.AppendLine("order by a.VTypeID");

        SqlDataSource1.SelectCommand = str_cmd.ToString();
        GridView1.DataBind();
    }

    //匯出流程
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        DownLoadExcel(GridView1, "匯出訪客類別管理" + DateTime.Now.ToString("yyyyMMdd"));
    }

    public void DownLoadExcel(GridView GridView_ID, string FileName)
    {
        AboutNPOI NPOI = new AboutNPOI();

        DataTable ta = new DataTable();
        ta = NPOI.GetGridDataTable(GridView_ID);

        MemoryStream ms = NPOI.RenderDataTableToExcel(ta, "Sheet1") as MemoryStream;
        Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + Server.UrlEncode(FileName) + ".xls"));
        Response.BinaryWrite(ms.ToArray());
        ms.Close();
        ms.Dispose();
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
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "search")
        {
            int index = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            GridViewRow row = GridView1.Rows[index];
            Label FlowNo = (Label)row.FindControl("Label2");

            StringBuilder str_cmd = new StringBuilder();
            str_cmd.AppendLine("SELECT a.FlowName,b.sid,a.FlowNo,b.Series,b.EmployeeID,b.Name");
            str_cmd.AppendLine("FROM Sys_V_Flow a");
            str_cmd.AppendLine("inner join Sys_V_Flow2 b");
            str_cmd.AppendLine("on a.FlowNo = b.FlowNo");
            str_cmd.AppendLine("where a.Campus='" + RadioButtonList1.SelectedValue + "'");
            str_cmd.AppendLine("and a.FlowNo='" + FlowNo.Text + "'");
            str_cmd.AppendLine("ORDER BY b.Series");

            SqlDataSource2.SelectCommand = str_cmd.ToString();
            GridView2.DataBind();
        }
    }

    protected void Label10_DataBinding(object sender, EventArgs e)
    {
        Label seminarText = (Label)sender;
        //Button but = (Button)sender;//直接將Button1_Click事件中的sender參數轉換成按鈕
        GridViewRow gv_row = (GridViewRow)seminarText.NamingContainer;//將Button轉換成GridViewRow就是您所點選的某一列
        Label FlowName = (Label)gv_row.Cells[4].FindControl("Label10");
        LinkButton link = (LinkButton)gv_row.Cells[4].FindControl("LinkButton1");
        if (FlowName.Text.Contains("一般流程"))
        {
            FlowName.Visible = true;
            link.Visible = false;
        }
        else
        {
            FlowName.Visible = false;
            link.Visible = true;
        }
    }

    protected void Label11_DataBinding(object sender, EventArgs e)
    {
        Label seminarText = (Label)sender;
        //Button but = (Button)sender;//直接將Button1_Click事件中的sender參數轉換成按鈕
        GridViewRow gv_row = (GridViewRow)seminarText.NamingContainer;//將Button轉換成GridViewRow就是您所點選的某一列
        string Campus = gv_row.Cells[1].Text;
        string VTypeID = gv_row.Cells[3].Text;
        Label Notice = (Label)gv_row.Cells[6].FindControl("Label11");
        string NoticeSring = string.Empty;

        string DBname = "DB_Tea_VToSchool";
        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings[DBname].ConnectionString.ToString());
        cn.Open();
        StringBuilder str_cmd = new StringBuilder();
        str_cmd.AppendLine("select Name,EmployeeID");
        str_cmd.AppendLine("from Sys_V_NoticePeople");
        str_cmd.AppendLine("where Campus=@Campus");
        str_cmd.AppendLine("and VTypeID=@VTypeID");

        SqlCommand cmd = new SqlCommand(str_cmd.ToString(), cn);
        cmd.Parameters.AddWithValue("@Campus", Campus);
        cmd.Parameters.AddWithValue("@VTypeID", VTypeID);
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            NoticeSring = NoticeSring + dr["Name"].ToString() + "、";
        }
        Notice.Text = NoticeSring.TrimEnd('、');
        dr.Close();
        cmd.Cancel();
        cn.Close();
        cn.Dispose();
        str_cmd.Length = 0;
    }
}