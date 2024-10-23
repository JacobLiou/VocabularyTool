using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace EntryTranslator.Utils;

internal class ExcelUtil
{
    public static DataTable ImportExcelFile(string filePath, int sheetIndex = 0, bool hasHeaders = true)
    {
        var dt = new DataTable();

        try
        {
            IWorkbook workbook;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // 根据文件扩展名选择适当的工作簿
                string extension = Path.GetExtension(filePath).ToLower();
                if (extension == ".xlsx")
                    workbook = new XSSFWorkbook(fs);
                else if (extension == ".xls")
                    workbook = new HSSFWorkbook(fs);
                else
                    throw new Exception("不支持的文件格式");

                ISheet sheet = workbook.GetSheetAt(sheetIndex);
                IRow headerRow = sheet.GetRow(0);
                int colCount = headerRow.LastCellNum;

                // 处理表头
                if (hasHeaders)
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        ICell cell = headerRow.GetCell(c);
                        string colName = cell?.ToString() ?? $"Column{c + 1}";
                        dt.Columns.Add(colName);
                    }
                }
                else
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        dt.Columns.Add($"Column{c + 1}");
                    }
                }

                // 导入数据
                int startRow = hasHeaders ? 1 : 0;
                for (int r = startRow; r <= sheet.LastRowNum; r++)
                {
                    IRow row = sheet.GetRow(r);
                    if (row == null) continue;

                    DataRow dataRow = dt.NewRow();
                    for (int c = 0; c < colCount; c++)
                    {
                        ICell cell = row.GetCell(c);
                        dataRow[c] = GetCellValue(cell);
                    }
                    dt.Rows.Add(dataRow);
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"导入Excel失败: {ex.Message}");
        }

        return dt;
    }

    /// <summary>
    /// 获取单元格值
    /// </summary>
    private static object GetCellValue(ICell cell)
    {
        if (cell == null) return null;

        switch (cell.CellType)
        {
            case CellType.Numeric:
                if (DateUtil.IsCellDateFormatted(cell))
                    return cell.DateCellValue;
                return cell.NumericCellValue;
            case CellType.String:
                return cell.StringCellValue;
            case CellType.Boolean:
                return cell.BooleanCellValue;
            case CellType.Formula:
                try
                {
                    return cell.NumericCellValue;
                }
                catch
                {
                    return cell.StringCellValue;
                }
            case CellType.Blank:
                return null;
            default:
                return cell.ToString();
        }
    }

    public static bool DataTableToExcel(DataTable dt, string path)
    {
        bool result = false;
        IWorkbook workbook = null;
        FileStream fs = null;
        IRow row = null;
        ISheet sheet = null;
        ICell cell = null;
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                workbook = new HSSFWorkbook();
                sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表
                int rowCount = dt.Rows.Count;//行数
                int columnCount = dt.Columns.Count;//列数

                //设置列头
                row = sheet.CreateRow(0);//excel第一行设为列头
                for (int c = 0; c < columnCount; c++)
                {
                    cell = row.CreateCell(c);
                    cell.SetCellValue(dt.Columns[c].ColumnName);
                }

                //设置每行每列的单元格,
                for (int i = 0; i < rowCount; i++)
                {
                    row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < columnCount; j++)
                    {
                        cell = row.CreateCell(j);//excel第二行开始写入数据
                        double dvalue;
                        if (double.TryParse(dt.Rows[i][j].ToString(), out dvalue))
                        {
                            cell.SetCellValue(dvalue);
                        }
                        else
                        {
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                }
                using (fs = File.OpenWrite(path))
                {
                    workbook.Write(fs);//向打开的这个xls文件中写入数据
                    result = true;
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            if (fs != null)
            {
                fs.Close();
            }
            return false;
        }
    }
}