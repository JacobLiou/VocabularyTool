using EntryTranslator.Models;
using EntryTranslator.Properties;
using EntryTranslator.Utils;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace EntryTranslator.ResourceOperations
{
    public class ResourceHolder
    {
        private readonly object _lockObject = new object();

        private readonly List<string> _deletedKeys;

        private bool _dirty;

        private DataTable _stringsTable;

        private object _columnChangePreviousValue;

        private string[] _lastLanguagesToCheck;

        public event EventHandler DirtyChanged;

        public event EventHandler LanguageChange;

        public string Filename { get; set; }

        public string Id { get; set; }

        public SortedDictionary<string, LanguageHolder> Languages { get; }

        public event EventHandler<ResourceLoadProgressEventArgs> ResourceLoadProgress;

        public event EventHandler ResourcesChanged;

        private string _openedPath;

        public string OpenedPath
        {
            get { return _openedPath; }
            private set
            {
                _openedPath = value;
                OnResourcesChanged();
            }
        }

        public ResourceHolder()
        {
            Languages = new SortedDictionary<string, LanguageHolder>(StringComparer.OrdinalIgnoreCase);
            _deletedKeys = new List<string>();
        }

        public DataTable StringsTable
        {
            get
            {
                lock (_lockObject)
                {
                    if (_stringsTable == null)
                    {
                        LoadResource();
                    }
                    return _stringsTable;
                }
            }
            private set
            {
                lock (_lockObject)
                {
                    _stringsTable = value;
                }
            }
        }

        public bool IsDirty => _stringsTable != null && Dirty;

        public bool Dirty
        {
            get { return _dirty; }
            set
            {
                if (value != _dirty)
                {
                    _dirty = value;
                    DirtyChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Check and prompt for save
        /// </summary>
        /// <returns>True if we can safely close</returns>
        public bool CanClose()
        {
            if (IsDirty)
            {
                var dialogResult = MessageBox.Show("当前有未保存数据", "数据保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                // Return false only if user presses cancel
                if (dialogResult != DialogResult.Yes)
                    return dialogResult == DialogResult.No;

                Save();
            }

            return true;
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

        public void OpenProject(string selectedPath)
        {
            OnResourceLoadProgress(new ResourceLoadProgressEventArgs("加载语言资源..."));

            FindResx(selectedPath);

            LoadResource();

            OpenedPath = selectedPath;

            OnResourceLoadProgress(new ResourceLoadProgressEventArgs(""));
        }

        protected virtual void OnResourceLoadProgress(ResourceLoadProgressEventArgs e)
        {
            ResourceLoadProgress?.Invoke(this, e);
        }

        protected virtual void OnResourcesChanged()
        {
            ResourcesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FindResx(string currentDirectory)
        {
            var files = Directory.GetFiles(currentDirectory, "*.resx");

            foreach (var filename in files)
            {
                var filenameNoExt = Path.GetFileNameWithoutExtension(filename);
                if (string.IsNullOrEmpty(filenameNoExt)) continue;

                // Try to get the language code
                var potentialLanguageCode = Path.GetExtension(filenameNoExt).TrimStart('.');

                var culture = potentialLanguageCode;
                filenameNoExt = Path.GetFileNameWithoutExtension(filenameNoExt);

                var key = ("\\" + filenameNoExt).ToLower();
                var dir = Path.GetDirectoryName(filename);
                Debug.Assert(dir != null, "dir != null");
                Filename = Path.Combine(dir, filenameNoExt + ".resx");

                if (culture != null)
                {
                    if (Languages.ContainsKey(culture.ToLower()))
                        throw new InvalidDataException(filename);

                    Languages.Add(culture.ToLower(), new LanguageHolder(culture, filename));
                }
            }
        }

        private void OnLanguageChange()
        {
            LanguageChange?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateFile(string filename, string valueColumnId, bool skipNontranslatableData, bool saveComments)
        {
            var originalMetadatas = new Dictionary<string, object>();
            var originalResources = new Dictionary<string, ResXDataNode>();

            var fileExists = filename != null && File.Exists(filename);
            if (fileExists)
            {
                using (var reader = new ResXResourceReader(filename,
                    AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetName()).ToArray()))
                {
                    // Set base path so that relative paths work
                    reader.BasePath = Path.GetDirectoryName(filename);

                    // If UseResXDataNodes == true before you call GetMetadataEnumerator, no resource nodes are retrieved
                    var metadataEnumerator = reader.GetMetadataEnumerator();
                    while (metadataEnumerator.MoveNext())
                    {
                        originalMetadatas.Add((string)metadataEnumerator.Key, metadataEnumerator.Value);
                    }
                }

                using (var reader = new ResXResourceReader(filename))
                {
                    reader.UseResXDataNodes = true;
                    var dataEnumerator = reader.GetEnumerator();
                    while (dataEnumerator.MoveNext())
                    {
                        var key = (string)dataEnumerator.Key;
                        if (!originalMetadatas.ContainsKey(key))
                            originalResources.Add(key, (ResXDataNode)dataEnumerator.Value);
                    }
                }

                foreach (var originalResource in originalResources
                    .Where(originalResource => _deletedKeys.Contains(originalResource.Key))
                    .ToList())
                {
                    originalResources.Remove(originalResource.Key);
                }
            }

            var localizableResourceKeys = originalResources.Select(x => x.Key).ToList();

            foreach (DataRow dataRow in _stringsTable.Rows)
            {
                var key = (string)dataRow[Properties.Resources.ColNameKey];

                var valueData = dataRow[valueColumnId] == DBNull.Value ? null : dataRow[valueColumnId];
                var stringValueData = valueData?.ToString() ?? string.Empty;

                if (localizableResourceKeys.Contains(key))
                {
                    if (stringValueData.Equals(originalResources[key].GetValueAsString(), StringComparison.InvariantCulture))
                        continue;

                    originalResources[key] = new ResXDataNode(originalResources[key].Name, stringValueData) { };
                }
                else
                {
                    originalResources.Add(key, new ResXDataNode(key, stringValueData) { });
                    localizableResourceKeys.Add(key);
                }
            }

            using (var writer = new ResXResourceWriter(filename))
            {
                foreach (var originalResource in originalResources)
                {
                    if (!localizableResourceKeys.Contains(originalResource.Key)
                        || !string.IsNullOrWhiteSpace(originalResource.Value.GetValueAsString()))
                    {
                        if (!skipNontranslatableData)
                            writer.AddResource(originalResource.Value);
                    }
                }
                foreach (var originalMetadata in originalMetadatas)
                {
                    writer.AddMetadata(originalMetadata.Key, originalMetadata.Value);
                }

                writer.Generate();
            }
        }

        public void Save()
        {
            if (!IsDirty)
                return;

            try
            {
                foreach (var languageHolder in Languages.Values)
                {
                    UpdateFile(languageHolder.Filename, languageHolder.LanguageId, false, Settings.Default.StoreCommentsInAllFiles);
                }

                Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReadResourceFile(string filename, DataTable stringsTable,
            string valueColumn, bool isTranslated)
        {
            var loadComments = !isTranslated || Settings.Default.StoreCommentsInAllFiles;

            var colNameComment = Properties.Resources.ColNameComment;
            var colNameTranslated = Properties.Resources.ColNameTranslated;

            using (var reader = new ResXResourceReader(filename))
            {
                reader.UseResXDataNodes = true;
                var dataEnumerator = reader.GetEnumerator();
                while (dataEnumerator.MoveNext())
                {
                    var key = (string)dataEnumerator.Key;
                    var dataNode = (ResXDataNode)dataEnumerator.Value;

                    var value = dataNode.GetValueAsString();

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        Dirty = true;
                        continue;
                    }

                    var r = FindByKey(key);
                    if (r == null)
                    {
                        var newRow = stringsTable.NewRow();
                        newRow[Properties.Resources.ColNameKey] = key;

                        newRow[valueColumn] = value;

                        if (loadComments) newRow[colNameComment] = dataNode.Comment;
                        newRow[Properties.Resources.ColNameError] = false;
                        newRow[colNameTranslated] = isTranslated && !string.IsNullOrEmpty(value);
                        stringsTable.Rows.Add(newRow);
                    }
                    else
                    {
                        r[valueColumn] = value;

                        if (loadComments && string.IsNullOrEmpty(r[colNameComment] as string) &&
                            !string.IsNullOrEmpty(dataNode.Comment))
                        {
                            r[colNameComment] = dataNode.Comment;
                        }

                        if (isTranslated && !string.IsNullOrEmpty(value))
                        {
                            r[colNameTranslated] = true;
                        }
                    }
                }
            }
        }

        public void EvaluateRow(DataRow row)
        {
            EvaluateRow(row, _lastLanguagesToCheck);
        }

        public void EvaluateRow(DataRow row, string[] languagesToCheck)
        {
            _lastLanguagesToCheck = languagesToCheck;
            var colNameError = Properties.Resources.ColNameError;
            foreach (var languageHolder in languagesToCheck == null || languagesToCheck.Length < 1 ?
                Languages.Values :
                Languages.Values.Where(x => languagesToCheck.Any(y => x.LanguageId.Equals(y, StringComparison.OrdinalIgnoreCase))))
            {
                if (!RowContainsTranslation(row, languageHolder.LanguageId))
                {
                    row[colNameError] = true;
                    return;
                }
            }

            row[colNameError] = false;
        }

        private static bool RowContainsTranslation(DataRow row, string languageId)
        {
            if (row[languageId] == DBNull.Value)
                return false;

            var value = (string)row[languageId];
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        ///     Read the resource files correspondning with this resource holder
        /// </summary>
        public void LoadResource()
        {
            lock (_lockObject)
            {
                _deletedKeys.Clear();

                _stringsTable = new DataTable("Strings");

                var colNameKey = Properties.Resources.ColNameKey;
                _stringsTable.Columns.Add(colNameKey);
                _stringsTable.PrimaryKey = new[] { _stringsTable.Columns[colNameKey] };

                foreach (var languageHolder in Languages.Values)
                {
                    _stringsTable.Columns.Add(languageHolder.LanguageId);
                }
                _stringsTable.Columns.Add(Properties.Resources.ColNameComment);
                _stringsTable.Columns.Add(Properties.Resources.ColNameTranslated, typeof(bool));
                _stringsTable.Columns.Add(Properties.Resources.ColNameError, typeof(bool));

                foreach (var languageHolder in Languages.Values)
                {
                    ReadResourceFile(languageHolder.Filename, _stringsTable, languageHolder.LanguageId, true);
                }

                EvaluateAllRows();

                _stringsTable.ColumnChanging += stringsTable_ColumnChanging;
                _stringsTable.ColumnChanged += stringsTable_ColumnChanged;
                _stringsTable.RowDeleting += stringsTable_RowDeleting;
                _stringsTable.TableNewRow += stringsTable_RowInserted;
            }
            OnLanguageChange();
        }

        /// <summary>
        ///     Eventhandler for the datatable of strings
        /// </summary>
        private void stringsTable_RowDeleting(object sender, DataRowChangeEventArgs e)
        {
            _deletedKeys.Add((string)e.Row[Properties.Resources.ColNameKey]);
            Dirty = true;
        }

        /// <summary>
        ///     Eventhandler for the datatable of strings
        /// </summary>
        private void stringsTable_RowInserted(object sender, DataTableNewRowEventArgs e)
        {
            Dirty = true;
        }

        /// <summary>
        ///     Eventhandler for the datatable of strings
        /// </summary>
        private void stringsTable_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Column != e.Column.Table.Columns[Properties.Resources.ColNameError])
            {
                var colNameKey = Properties.Resources.ColNameKey;
                if (e.Column == e.Column.Table.Columns[colNameKey])
                {
                    // row key was changed -> treat old as being delete
                    _deletedKeys.Add((string)_columnChangePreviousValue);

                    // maybe we create/renamed a key to a previously deleted one -> remove that
                    _deletedKeys.Remove((string)e.ProposedValue);
                }

                Dirty = true;
                EvaluateRow(e.Row);
            }

            _columnChangePreviousValue = null;
        }

        /// <summary>
        ///     Eventhandler for the datatable of strings
        /// </summary>
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
                Dirty = true;
            }
        }

        /// <summary>
        ///     Add one key
        /// </summary>
        public void AddString(string key, string noXlateValue, string defaultValue)
        {
            if (FindByKey(key) != null)
            {
                throw new DuplicateNameException(key);
            }

            _stringsTable.ColumnChanged -= stringsTable_ColumnChanged;

            var row = _stringsTable.NewRow();
            row[Properties.Resources.ColNameKey] = key;
            foreach (var languageHolder in Languages.Values)
            {
                row[languageHolder.LanguageId] = defaultValue;
            }
            row[Properties.Resources.ColNameComment] = string.Empty;
            row[Properties.Resources.ColNameError] = false;

            _stringsTable.ColumnChanged += stringsTable_ColumnChanged;

            _stringsTable.Rows.Add(row);
        }

        /// <summary>
        ///     Check if such a key exists.
        /// </summary>
        public DataRow FindByKey(string key)
        {
            return _stringsTable.Rows.Find(key);
        }

        /// <summary>
        ///     Add the specified language to this object
        /// </summary>
        public void AddLanguage(string languageCode, bool copyValues)
        {
            if (Languages.ContainsKey(languageCode.ToLower()))
                return;

            // Create the file
            var cleanFilename = Filename.Substring(0, Filename.LastIndexOf('.'));
            var newFilename = $"{cleanFilename}.{languageCode}.resx";
            File.Delete(newFilename);

            using (var writer = new ResXResourceWriter(newFilename))
            {
                if (copyValues)
                {
                    using (var reader = new ResXResourceReader(Filename))
                    {
                        reader.UseResXDataNodes = true;
                        var dataEnumerator = reader.GetEnumerator();
                        while (dataEnumerator.MoveNext())
                        {
                            var key = (string)dataEnumerator.Key;
                            var node = (ResXDataNode)dataEnumerator.Value;

                            var value = node.GetValueAsString();
                            // Skip saving unnecessary items
                            if (!string.IsNullOrWhiteSpace(value))
                                writer.AddResource(key, value);
                        }
                    }
                }
                writer.Generate();
            }

            // Add the created file to this ResourceHolder
            var languageHolder = new LanguageHolder(languageCode, newFilename);
            Languages.Add(languageCode.ToLower(), languageHolder);

            _stringsTable.Columns.Add(languageCode.ToLower());

            ReadResourceFile(languageHolder.Filename, _stringsTable, languageHolder.LanguageId, true);

            EvaluateAllRows();

            Dirty = true;
            OnLanguageChange();
        }

        public void EvaluateAllRows(string[] languagesToCheck = null)
        {
            foreach (DataRow row in _stringsTable.Rows)
            {
                EvaluateRow(row, languagesToCheck);
            }
        }

        /// <summary>
        ///     Delete a language from this object (including its file)
        /// </summary>
        public void DeleteLanguage(string languageCode)
        {
            if (!Languages.ContainsKey(languageCode.ToLower())) return;

            File.Delete(Languages[languageCode.ToLower()].Filename);

            Languages.Remove(languageCode.ToLower());
            _stringsTable.Columns.RemoveAt(_stringsTable.Columns[languageCode].Ordinal);

            OnLanguageChange();
        }

        public List<string> GetTextForTranslating(TranslateAPIConfig translateApiConfig)
        {
            string sl = translateApiConfig.SourceLanguage;
            var result = new List<string>();
            IEnumerable<DataRow> rows = _stringsTable.Rows.Cast<DataRow>();

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
            DataRow[] rows = _stringsTable.Rows.Cast<DataRow>().ToArray();

            for (int i = 0; i < rows.Length; i++)
            {
                rows[i][translateApiConfig.TargetLanguage] = translationResults[i].Result;
            }
        }
    }
}