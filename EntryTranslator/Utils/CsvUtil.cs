using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntryTranslator.Utils
{
    internal class CsvUtil
    {
        public async Task<List<T>> LoadDataFromCsvAsync<T>(string filePath)
        {
            var dataStr = File.ReadAllText(filePath);
            if (string.IsNullOrEmpty(dataStr)) throw new ArgumentNullException(nameof(filePath));
            var results = new List<T>();
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            await foreach (var item in csv.GetRecordsAsync<T>())
            {
                results.Add(item);
            }

            return results;
        }

        public async Task<bool> SaveDataToCsvAsync<T>(List<T> dataList, string filePath)
        {
            bool successFlag = true;
            try
            {
                using var writer = new StreamWriter(filePath);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture, false);
                //csv.WriteHeader<T>();
                await csv.WriteRecordsAsync<T>(dataList);
            }
            catch
            {
                successFlag = false;
            }

            return successFlag;
        }

        /// <summary>
        /// DataTable导出到CSV文件
        /// </summary>
        public static bool ExportToCsv(DataTable dt, string filePath, bool includeHeaders = true)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    // 写入表头
                    if (includeHeaders)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            csv.WriteField(column.ColumnName);
                        }
                        csv.NextRecord();
                    }

                    // 写入数据行
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            csv.WriteField(row[i]?.ToString());
                        }
                        csv.NextRecord();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
               return false;
            }
        }

        /// <summary>
        /// 带配置的导出方法
        /// </summary>
        public static void ExportToCsvWithConfig(DataTable dt, string filePath, CsvConfiguration config = null)
        {
            config ??= new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8,
                // 处理字段中的换行符
                ShouldQuote = (field) => true
            };

            using (var writer = new StreamWriter(filePath, false, config.Encoding))
            using (var csv = new CsvWriter(writer, config))
            {
                // 写入表头
                if (config.HasHeaderRecord)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();
                }

                // 写入数据行
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]?.ToString());
                    }
                    csv.NextRecord();
                }
            }
        }

        /// <summary>
        /// 带进度报告的导出方法
        /// </summary>
        public static async Task ExportToCsvAsync(DataTable dt, string filePath,
            IProgress<int> progress = null, CancellationToken cancellationToken = default)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8,
                ShouldQuote = (field) => true
            };

            using (var writer = new StreamWriter(filePath, false, config.Encoding))
            await using (var csv = new CsvWriter(writer, config))
            {
                // 写入表头
                if (config.HasHeaderRecord)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }

                    await csv.NextRecordAsync();
                }

                // 写入数据行
                int totalRows = dt.Rows.Count;
                for (int rowIndex = 0; rowIndex < totalRows; rowIndex++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var row = dt.Rows[rowIndex];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        csv.WriteField(row[i]?.ToString());
                    }

                    await csv.NextRecordAsync();

                    // 报告进度
                    progress?.Report((rowIndex + 1) * 100 / totalRows);
                }
            }
        }

        /// <summary>
        /// 高级导出选项
        /// </summary>
        public class CsvExportOptions
        {
            public string Delimiter { get; set; } = ",";
            public bool IncludeHeaders { get; set; } = true;
            public Encoding Encoding { get; set; } = Encoding.UTF8;
            public bool QuoteAllFields { get; set; } = true;
            public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";
            public string NumberFormat { get; set; } = "0.####";
            public Dictionary<string, Func<object, string>> ColumnFormatters { get; set; }
            public List<string> ExcludeColumns { get; set; }
        }

        /// <summary>
        /// 高级导出方法
        /// </summary>
        public static void ExportToCsvAdvanced(DataTable dt, string filePath, CsvExportOptions options = null)
        {
            options ??= new CsvExportOptions();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = options.Delimiter,
                HasHeaderRecord = options.IncludeHeaders,
                Encoding = options.Encoding,
                ShouldQuote = (field) => options.QuoteAllFields
            };

            using (var writer = new StreamWriter(filePath, false, options.Encoding))
            using (var csv = new CsvWriter(writer, config))
            {
                // 获取要导出的列
                var columns = dt.Columns.Cast<DataColumn>()
                    .Where(c => options.ExcludeColumns == null ||
                               !options.ExcludeColumns.Contains(c.ColumnName))
                    .ToList();

                // 写入表头
                if (options.IncludeHeaders)
                {
                    foreach (var column in columns)
                    {
                        csv.WriteField(column.ColumnName);
                    }
                    csv.NextRecord();
                }

                // 写入数据行
                foreach (DataRow row in dt.Rows)
                {
                    foreach (var column in columns)
                    {
                        var value = row[column];
                        string formattedValue;

                        // 使用自定义格式化器
                        if (options.ColumnFormatters?.ContainsKey(column.ColumnName) == true)
                        {
                            formattedValue = options.ColumnFormatters[column.ColumnName](value);
                        }
                        // 使用默认格式化
                        else
                        {
                            formattedValue = FormatValue(value, column.DataType, options);
                        }

                        csv.WriteField(formattedValue);
                    }
                    csv.NextRecord();
                }
            }
        }

        /// <summary>
        /// 格式化值
        /// </summary>
        private static string FormatValue(object value, Type dataType, CsvExportOptions options)
        {
            if (value == null || value == DBNull.Value)
                return string.Empty;

            if (dataType == typeof(DateTime) || dataType == typeof(DateTime?))
            {
                return ((DateTime)value).ToString(options.DateTimeFormat);
            }

            if (dataType == typeof(decimal) || dataType == typeof(decimal?) ||
                dataType == typeof(double) || dataType == typeof(double?) ||
                dataType == typeof(float) || dataType == typeof(float?))
            {
                return Convert.ToDouble(value).ToString(options.NumberFormat);
            }

            return value.ToString();
        }

        /// <summary>
        /// 从CSV文件导入到DataTable
        /// </summary>
        public static DataTable ImportFromCsv(string filePath)
        {
            var dt = new DataTable();

            using (var reader = new StreamReader(filePath, Encoding.UTF8))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                // 读取标题行
                csv.Read();
                csv.ReadHeader();

                // 创建列
                foreach (string header in csv.HeaderRecord)
                {
                    dt.Columns.Add(header);
                }

                // 读取数据行
                while (csv.Read())
                {
                    var row = dt.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        row[column.ColumnName] = csv.GetField(column.ColumnName);
                    }
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        /// <summary>
        /// 带配置的导入方法
        /// </summary>
        public static DataTable ImportFromCsvWithConfig(string filePath, CsvConfiguration config = null)
        {
            config ??= new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8,
                MissingFieldFound = null, // 忽略缺失字段
                BadDataFound = null // 忽略错误数据
            };

            var dt = new DataTable();

            using (var reader = new StreamReader(filePath, config.Encoding))
            using (var csv = new CsvReader(reader, config))
            {
                // 读取标题行
                csv.Read();
                csv.ReadHeader();

                // 创建列
                foreach (string header in csv.HeaderRecord)
                {
                    dt.Columns.Add(header);
                }

                // 读取数据行
                while (csv.Read())
                {
                    var row = dt.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        row[column.ColumnName] = csv.GetField(column.ColumnName);
                    }
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        /// <summary>
        /// 带进度报告的异步导入方法
        /// </summary>
        public static async Task<DataTable> ImportFromCsvAsync(string filePath,
            IProgress<int> progress = null, CancellationToken cancellationToken = default)
        {
            var dt = new DataTable();
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
                HasHeaderRecord = true,
                Encoding = Encoding.UTF8
            };

            // 首先计算总行数
            int totalLines = File.ReadLines(filePath).Count();
            int currentLine = 0;

            using (var reader = new StreamReader(filePath, config.Encoding))
            using (var csv = new CsvReader(reader, config))
            {
                // 读取标题行
                await csv.ReadAsync();
                csv.ReadHeader();
                currentLine++;

                // 创建列
                foreach (string header in csv.HeaderRecord)
                {
                    dt.Columns.Add(header);
                }

                // 读取数据行
                while (await csv.ReadAsync())
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var row = dt.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        row[column.ColumnName] = csv.GetField(column.ColumnName);
                    }
                    dt.Rows.Add(row);

                    currentLine++;
                    progress?.Report(currentLine * 100 / totalLines);
                }
            }

            return dt;
        }

        /// <summary>
        /// 高级导入选项
        /// </summary>
        public class CsvImportOptions
        {
            public string Delimiter { get; set; } = ",";
            public bool HasHeaderRecord { get; set; } = true;
            public Encoding Encoding { get; set; } = Encoding.UTF8;
            public Dictionary<string, Type> ColumnTypes { get; set; }
            public Dictionary<string, Func<string, object>> ValueParsers { get; set; }
            public bool IgnoreQuotes { get; set; }
            public bool IgnoreBlankLines { get; set; } = true;
            public int BufferSize { get; set; } = 1024;
            public string DateTimeFormat { get; set; } = "yyyy-MM-dd HH:mm:ss";
        }

        /// <summary>
        /// 高级导入方法
        /// </summary>
        public static DataTable ImportFromCsvAdvanced(string filePath, CsvImportOptions options = null)
        {
            options ??= new CsvImportOptions();

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = options.Delimiter,
                HasHeaderRecord = options.HasHeaderRecord,
                Encoding = options.Encoding,
                IgnoreBlankLines = options.IgnoreBlankLines,
                BufferSize = options.BufferSize
            };

            var dt = new DataTable();

            using (var reader = new StreamReader(filePath, options.Encoding))
            using (var csv = new CsvReader(reader, config))
            {
                // 读取标题行
                csv.Read();
                csv.ReadHeader();

                // 创建列并设置数据类型
                foreach (string header in csv.HeaderRecord)
                {
                    Type columnType = typeof(string); // 默认类型
                    if (options.ColumnTypes?.ContainsKey(header) == true)
                    {
                        columnType = options.ColumnTypes[header];
                    }
                    dt.Columns.Add(header, columnType);
                }

                // 读取数据行
                while (csv.Read())
                {
                    var row = dt.NewRow();
                    foreach (DataColumn column in dt.Columns)
                    {
                        string value = csv.GetField(column.ColumnName);

                        if (string.IsNullOrEmpty(value))
                        {
                            row[column.ColumnName] = DBNull.Value;
                            continue;
                        }

                        // 使用自定义解析器
                        if (options.ValueParsers?.ContainsKey(column.ColumnName) == true)
                        {
                            row[column.ColumnName] = options.ValueParsers[column.ColumnName](value);
                        }
                        // 默认类型转换
                        else
                        {
                            row[column.ColumnName] = ParseValue(value, column.DataType, options);
                        }
                    }
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }

        /// <summary>
        /// 解析值
        /// </summary>
        private static object ParseValue(string value, Type targetType, CsvImportOptions options)
        {
            try
            {
                if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                {
                    return DateTime.ParseExact(value, options.DateTimeFormat, CultureInfo.InvariantCulture);
                }

                if (targetType == typeof(decimal) || targetType == typeof(decimal?))
                {
                    return decimal.Parse(value, CultureInfo.InvariantCulture);
                }

                if (targetType == typeof(double) || targetType == typeof(double?))
                {
                    return double.Parse(value, CultureInfo.InvariantCulture);
                }

                if (targetType == typeof(int) || targetType == typeof(int?))
                {
                    return int.Parse(value, CultureInfo.InvariantCulture);
                }

                if (targetType == typeof(bool) || targetType == typeof(bool?))
                {
                    return bool.Parse(value);
                }

                return Convert.ChangeType(value, targetType);
            }
            catch
            {
                return DBNull.Value;
            }
        }

    }
}
