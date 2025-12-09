using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using System.Web.UI.WebControls;

/// <summary>
/// AboutNOPI 的摘要描述
/// </summary>
public class AboutNPOI
{
    ClassBasic Basic = new ClassBasic();

    //public void DownLoadExcel(GridView GridView_ID, string FileName)
    //{
    //    AboutNOPI NPOI = new AboutNOPI();
    //    DataTable ta = new DataTable();
    //    ta = NPOI.GetGridDataTable(GridView_ID);

    //    MemoryStream ms = NPOI.RenderDataTableToExcel(ta, "Sheet1") as MemoryStream;
    //    Response.AddHeader("Content-Disposition", string.Format("attachment; filename=" + Server.UrlEncode(FileName) + ".xls"));
    //    Response.BinaryWrite(ms.ToArray());
    //    ms.Close();
    //    ms.Dispose();
    //}

    public AboutNPOI()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//        
	}

    /// <summary>
    /// 將Excel轉成DataTable
    /// </summary>
    /// <param name="ExcelFileStream">Excel內容的Stream</param>
    /// <param name="SheetIndex"></param>
    /// <param name="HeaderRowIndex"></param>
    /// <returns></returns>
    public DataTable RenderDataTableFromExcel(Stream ExcelFileStream, int SheetIndex, int HeaderRowIndex)
    {
        HSSFWorkbook workbook = new HSSFWorkbook(ExcelFileStream);
        HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(SheetIndex);

        DataTable table = new DataTable();

        HSSFRow headerRow = (HSSFRow)sheet.GetRow(HeaderRowIndex);
        int cellCount = headerRow.LastCellNum;

        for (int i = headerRow.FirstCellNum; i < cellCount; i++)
        {
            DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
            table.Columns.Add(column);
        }

        int rowCount = sheet.LastRowNum;

        for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
        {
            HSSFRow row = (HSSFRow)sheet.GetRow(i);
            DataRow dataRow = table.NewRow();

            for (int j = row.FirstCellNum; j < cellCount; j++)
            {
                if (row.GetCell(j) != null)
                    dataRow[j] = row.GetCell(j).ToString();
            }

            table.Rows.Add(dataRow);
        }

        ExcelFileStream.Close();
        workbook = null;
        sheet = null;
        return table;
    }

    /// <summary>
    /// 將DataTable轉成Excel
    /// </summary>
    /// <param name="SourceTable">原始資料DataTable</param>
    /// <param name="CreateSheetName">分頁表的名稱</param>
    /// <returns>Stream</returns>
    public Stream RenderDataTableToExcel(DataTable SourceTable, string CreateSheetName)
    {        
        HSSFWorkbook workbook = new HSSFWorkbook();
        MemoryStream ms = new MemoryStream();
        HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet(CreateSheetName);
        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);

        // handling header.
        foreach (DataColumn column in SourceTable.Columns)
            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

        // handling value.
        int rowIndex = 1;

        foreach (DataRow row in SourceTable.Rows)
        {
            HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);

            foreach (DataColumn column in SourceTable.Columns)
            {
                //因為11.5等複數型態，會被當作11/5號的日期型態，所以要把判斷複數的條件放在前面!!
                if (Basic.IsDouble(row[column].ToString()))
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDouble(row[column].ToString()));
                }
                else if (Basic.IsDate(row[column].ToString()))
                {
                    if (row[column].ToString().Contains("/") && row[column].ToString().Contains(":"))
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDateTime(row[column].ToString()).ToString("yyyy/MM/dd HH:mm:ss"));
                    }
                    else if(row[column].ToString().Contains("/"))
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDateTime(row[column].ToString()).ToString("yyyy/MM/dd"));
                    }
                    else if (row[column].ToString().Contains(":"))
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(Convert.ToDateTime(row[column].ToString()).ToString("HH:mm:ss"));
                    }
                }
                else
                {
                    dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                }
            }

            rowIndex++;
        }

        workbook.Write(ms);
        ms.Flush();
        ms.Position = 0;

        sheet = null;
        headerRow = null;
        workbook = null;

        return ms;
    }      

    public DataTable GetGridDataTable(GridView grid)
    {
        DataTable dt = new DataTable();
        DataColumn dc;//創建列
        DataRow dr;       //創建行
        //構造列
        for (int i = 0; i < grid.Columns.Count; i++)
        {
            dc = new DataColumn();
            dc.ColumnName = grid.Columns[i].HeaderText;
            dt.Columns.Add(dc);
        }
        //構造行
        for (int i = 0; i < grid.Rows.Count; i++)
        {
            dr = dt.NewRow();
            for (int j = 0; j < grid.Columns.Count; j++)
            {
                if (grid.Rows[i].Cells[j].Controls.Count > 0)
                {
                    foreach (System.Web.UI.Control ctrl in grid.Rows[i].Cells[j].Controls)
                    {
                        if (ctrl.GetType() == typeof(System.Web.UI.WebControls.Label))    // Label Control
                        {
                            Label lblTemp = (Label)ctrl;
                            dr[j] = lblTemp.Text;
                            // 取值 lblTemp.Text
                        }
                        else if (ctrl.GetType() == typeof(System.Web.UI.WebControls.HyperLink))
                        {
                            HyperLink hlTemp = (HyperLink)ctrl;
                            dr[j] = hlTemp.Text;
                        }
                    }
                }
                else
                {
                    if (grid.Rows[i].Cells[j].Text == "&nbsp;")
                    {
                        dr[j] = "";
                    }
                    else
                    {
                        dr[j] = grid.Rows[i].Cells[j].Text;
                    }
                }
            }
            dt.Rows.Add(dr);
        }
        return dt;
    }

    public GridView dynamicGenerateColumns(GridView gv, DataTable dt)
    {
        // 把GridView的自動產生列設置為false,否則會出現重復列
        gv.AutoGenerateColumns = false;

        // 清空所有的Columns
        gv.Columns.Clear();

        // 遍歷DataTable 的每個Columns,然後添加到GridView中去
        foreach (DataColumn item in dt.Columns)
        {
            BoundField col = new BoundField();
            col.HeaderText = item.ColumnName;
            col.DataField = item.ColumnName;
            col.Visible = true;
            gv.Columns.Add(col);
        }
        return gv;
    }
}