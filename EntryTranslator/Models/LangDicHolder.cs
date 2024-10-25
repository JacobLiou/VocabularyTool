using EntryTranslator.Const;
using EntryTranslator.Utils;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EntryTranslator.Models
{
    public class LangDicHolder
    {
        private readonly object _lockObject = new object();

        private readonly List<string> _deletedKeys;

        private bool _dirty;

        public DataTable StringsTable { get; set; }

        private object _columnChangePreviousValue;

        public event EventHandler DirtyChanged;

        public event EventHandler LanguageChange;

        public Dictionary<string, LanguageHolder> Languages { get; }

        public event EventHandler<LoadProgressEventArgs> ResourceLoadProgress;

        public event EventHandler ResourcesChanged;

        public LangDicHolder()
        {
            Languages = new Dictionary<string, LanguageHolder>();
            _deletedKeys = new List<string>();

            if (!File.Exists(GlobalSettings.LangDicPath))
                File.Create(GlobalSettings.LangDicPath);
        }

        public IEnumerable<CultureInfo> GetUsedLanguages()
        {
            var cultureInfos = new List<CultureInfo>();
            Languages.ForEach(item =>
             {
                 cultureInfos.Add(item.Value.CultureInfo);
             });

            return cultureInfos;
        }

        private void OnLanguageChange()
        {
            LanguageChange?.Invoke(this, EventArgs.Empty);
        }

        public void Load()
        {
            lock (_lockObject)
            {
                Languages.Clear();
                _deletedKeys.Clear();

                StringsTable = CsvUtil.CsvToDataTable(GlobalSettings.LangDicPath, Languages);
                StringsTable.ColumnChanging += stringsTable_ColumnChanging;
                StringsTable.ColumnChanged += stringsTable_ColumnChanged;
                StringsTable.RowDeleting += stringsTable_RowDeleting;
                StringsTable.TableNewRow += stringsTable_RowInserted;
            }

            ResourcesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Save()
        {
            try
            {
                CsvUtil.ExportToCsv(StringsTable, GlobalSettings.LangDicPath, GlobalSettings.FilterColNames);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void stringsTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            _deletedKeys.Add((string)e.Row[Properties.Resources.ColNameKey]);
        }

        private void stringsTable_RowInserted(object sender, DataTableNewRowEventArgs e)
        {
        }

        private void stringsTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column != e.Column.Table.Columns[Properties.Resources.ColNameError])
            {
                var colNameKey = Properties.Resources.ColNameKey;
                if (e.Column == e.Column.Table.Columns[colNameKey])
                {
                    _deletedKeys.Add((string)_columnChangePreviousValue);
                    _deletedKeys.Remove((string)e.ProposedValue);
                }
            }

            _columnChangePreviousValue = null;
        }

        private void stringsTable_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            _columnChangePreviousValue = e.Row[e.Column];

            var colNameKey = Properties.Resources.ColNameKey;
            if (e.Column == e.Column.Table.Columns[colNameKey])
            {
                var foundRows = e.Column.Table.Select("Key='" + e.ProposedValue + "'");
                if (foundRows.Length > 1
                    || (foundRows.Length == 1 && foundRows[0] != e.Row))
                {
                    e.Row[Properties.Resources.ColNameError] = true;
                    throw new DuplicateNameException(e.Row[colNameKey].ToString());
                }
            }
        }

        public void AddString(string key, string noXlateValue, string defaultValue)
        {
            if (FindByKey(key) != null)
            {
                throw new DuplicateNameException(key);
            }

            StringsTable.ColumnChanged -= stringsTable_ColumnChanged;

            var row = StringsTable.NewRow();
            row[Properties.Resources.ColNameKey] = key;
            foreach (var languageHolder in Languages.Values)
            {
                row[languageHolder.LanguageId] = defaultValue;
            }

            row[Properties.Resources.ColNameError] = false;

            StringsTable.ColumnChanged += stringsTable_ColumnChanged;

            StringsTable.Rows.Add(row);
        }

        public DataRow FindByKey(string key)
        {
            return StringsTable.Rows.Find(key);
        }

        public void AddLanguage(string languageCode, bool copyValues)
        {
            if (Languages.ContainsKey(languageCode.ToLower()))
                return;

            var languageHolder = new LanguageHolder(languageCode);
            Languages.Add(languageCode.ToLower(), languageHolder);

            StringsTable.Columns.Add(languageCode.ToLower());
            OnLanguageChange();
        }

        public void DeleteLanguage(string languageCode)
        {
            if (!Languages.ContainsKey(languageCode.ToLower())) return;

            Languages.Remove(languageCode.ToLower());
            StringsTable.Columns.RemoveAt(StringsTable.Columns[languageCode].Ordinal);

            OnLanguageChange();
        }

        public List<string> GetTextForTranslating(TranslateAPIConfig translateApiConfig)
        {
            string sl = translateApiConfig.SourceLanguage;
            var result = new List<string>();
            IEnumerable<DataRow> rows = StringsTable.Rows.Cast<DataRow>();

            foreach (DataRow row in rows)
            {
                string sourceText = row[sl].ToString();
                string targetText = row[translateApiConfig.TargetLanguage].ToString();

                if (string.IsNullOrEmpty(targetText))
                {
                    result.Add(sourceText);
                }
                else
                {
                    if (translateApiConfig.Overwrite)
                    {
                        result.Add(sourceText);
                    }
                }
            }

            return result;
        }

        public void SetTranslatedText(TranslateAPIConfig translateApiConfig, IList<StranslationResult> translationResults)
        {
            DataRow[] rows = StringsTable.Rows.Cast<DataRow>().ToArray();

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i][translateApiConfig.TargetLanguage] = translationResults[i].Result;
            }
        }
    }
}