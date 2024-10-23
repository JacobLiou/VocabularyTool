using EntryTranslator.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace EntryTranslator.ResourceOperations
{
    public class ResourceHolder
    {
        private readonly object _lockObject = new object();

        private readonly string Key = "Key";

        private readonly List<string> _deletedKeys;

        private bool _dirty;

        private string _noLanguageLanguage = string.Empty;
        private DataTable _stringsTable;
        private object _columnChangePreviousValue;

        public event EventHandler DirtyChanged;

        public event EventHandler LanguageChange;

        public ResourceHolder()
        {
            Languages = new SortedDictionary<string, LanguageHolder>(StringComparer.OrdinalIgnoreCase);
            _deletedKeys = new List<string>();
        }

        public string Filename { get; set; }
        public string DisplayFolder { get; set; }
        public string Id { get; set; }
        public SortedDictionary<string, LanguageHolder> Languages { get; }

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
        ///     Text shown in the tree view for this resourceholder
        /// </summary>
        public string Caption
        {
            get
            {
                var languages = string.Join(",", Languages.Keys.OrderBy(x => x));

                return $"{Id} ({languages})"; //[{_noLanguageLanguage}]
            }
        }

        /// <summary>
        ///     Trigger LanguageChange event when default language is set
        /// </summary>
        private void OnLanguageChange()
        {
            LanguageChange?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Save one resource file
        /// </summary>
        private void UpdateFile(string filename, string valueColumnId, bool skipNontranslatableData, bool saveComments)
        {
            // Read the entire resource file to a buffer
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
                    // If GetMetadataEnumerator was already called setting the UseResXDataNodes to true will have no effect
                    // Because of this creating a new reader is necessary
                    reader.UseResXDataNodes = true;
                    var dataEnumerator = reader.GetEnumerator();
                    while (dataEnumerator.MoveNext())
                    {
                        var key = (string)dataEnumerator.Key;
                        // GetEnumerator will also get metadata items, filter them out
                        if (!originalMetadatas.ContainsKey(key))
                            originalResources.Add(key, (ResXDataNode)dataEnumerator.Value);
                    }
                }

                // Get rid of keys marked as deleted. If they have been restored they will be re-added later
                // Only support localizable strings to avoid removing other resources by mistake
                // BUG Clear the _deletedKeys?
                foreach (var originalResource in originalResources
                    .Where(originalResource => _deletedKeys.Contains(originalResource.Key))
                    .Where(originalResource => IsLocalizableString(originalResource.Key, originalResource.Value))
                    .ToList())
                {
                    originalResources.Remove(originalResource.Key);
                }
            }

            // Precache the valid keys
            var localizableResourceKeys = originalResources
                .Where(originalResource => IsLocalizableString(originalResource.Key, originalResource.Value))
                .Select(x => x.Key).ToList();

            // Update originalResources with information stored in _stringsTable.
            // Adds keys if they are missing in originalResources
            foreach (DataRow dataRow in _stringsTable.Rows)
            {
                var key = (string)dataRow[Key];

                var valueData = dataRow[valueColumnId] == DBNull.Value ? null : dataRow[valueColumnId];
                var stringValueData = valueData?.ToString() ?? string.Empty;

                if (localizableResourceKeys.Contains(key))
                {
                    // Skip if the original value and comment is the same as the new one
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

            // Write the cached resources to the drive
            using (var writer = new ResXResourceWriter(filename))
            {
                foreach (var originalResource in originalResources)
                {
                    // Write localizable resource only if it is not empty, unless we are saving the default file
                    if (!localizableResourceKeys.Contains(originalResource.Key)
                        || !string.IsNullOrWhiteSpace(originalResource.Value.GetValueAsString()))
                    {
                        if (!skipNontranslatableData || IsLocalizableString(originalResource.Key, originalResource.Value))
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

        /// <summary>
        ///     Save this resource holder's data
        /// </summary>
        public void Save()
        {
            if (!IsDirty)
                return;
            try
            {
                foreach (var languageHolder in Languages.Values)
                {
                    UpdateFile(languageHolder.Filename, languageHolder.LanguageId, false, false);
                }
                Dirty = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Format("保存失败", Id),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///     Read one resource file
        /// </summary>
        private void ReadResourceFile(string filename, DataTable stringsTable,
            string valueColumn, bool isTranslated)
        {
            using (var reader = new ResXResourceReader(filename))
            {
                reader.UseResXDataNodes = true;
                var dataEnumerator = reader.GetEnumerator();
                while (dataEnumerator.MoveNext())
                {
                    var key = (string)dataEnumerator.Key;
                    var dataNode = (ResXDataNode)dataEnumerator.Value;

                    if (!IsLocalizableString(key, dataNode))
                        continue;

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
                        newRow[Key] = key;
                        newRow[valueColumn] = value;
                        stringsTable.Rows.Add(newRow);
                    }
                    else
                    {
                        r[valueColumn] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Check if the entry contains a localizable string
        /// </summary>
        private static bool IsLocalizableString(string key, ResXDataNode dataNode)
        {
            if (key.StartsWith(">>") || (key.StartsWith("$") && key != "$this.Text"))
                return false;

            if (dataNode == null)
                return false;
            if (dataNode.FileRef != null)
                return false;

            var valueType = dataNode.GetValueTypeName((ITypeResolutionService)null);
            return valueType.StartsWith("System.String, ");
        }

        private string[] _lastLanguagesToCheck;

        /// <summary>
        ///     Sets error field on the row depending on missing translations etc
        /// </summary>
        public void EvaluateRow(DataRow row)
        {

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

                var colNameKey = Key;
                _stringsTable.Columns.Add(colNameKey);
                _stringsTable.PrimaryKey = new[] { _stringsTable.Columns[colNameKey] };
                foreach (var languageHolder in Languages.Values)
                {
                    _stringsTable.Columns.Add(languageHolder.LanguageId);
                }

                foreach (var languageHolder in Languages.Values)
                {
                    ReadResourceFile(languageHolder.Filename, _stringsTable, languageHolder.LanguageId, true);
                }

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
            _deletedKeys.Add((string)e.Row[Key]);
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
            var colNameKey = Key;
            if (e.Column == e.Column.Table.Columns[colNameKey])
            {
                // row key was changed -> treat old as being delete
                _deletedKeys.Add((string)_columnChangePreviousValue);

                // maybe we create/renamed a key to a previously deleted one -> remove that
                _deletedKeys.Remove((string)e.ProposedValue);
            }

            Dirty = true;
            EvaluateRow(e.Row);

            _columnChangePreviousValue = null;
        }

        /// <summary>
        ///     Eventhandler for the datatable of strings
        /// </summary>
        private void stringsTable_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {
            _columnChangePreviousValue = e.Row[e.Column];

            var colNameKey = Key;
            if (e.Column == e.Column.Table.Columns[colNameKey])
            {
                var foundRows = e.Column.Table.Select("Key='" + e.ProposedValue + "'");
                if (foundRows.Length > 1
                    || (foundRows.Length == 1 && foundRows[0] != e.Row))
                {
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
            row[Key] = key;
            foreach (var languageHolder in Languages.Values)
            {
                row[languageHolder.LanguageId] = defaultValue;
            }

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
                            if (!IsLocalizableString(key, node))
                                continue;

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

            Dirty = true;
            OnLanguageChange();
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

        /// <summary>
        ///     Revert all non saved changes and reload
        /// </summary>
        public void Revert()
        {
            StringsTable = null;
            LoadResource();
            Dirty = false;
            _deletedKeys.Clear();

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
            //DataRow[] rows = _stringsTable.Rows.Cast<DataRow>().ToArray();

            //foreach (var translationResult in translationResults)
            //{
            //    string sl = translationResult.SpecifiedSourceLanguage == translateApiConfig.DefaultLanguage ? Properties.Resources.ColNameNoLang : translationResult.SpecifiedSourceLanguage;

            //    foreach (DataRow row in rows)
            //    {
            //        string sourceText = row[sl].ToString();

            //        if (sourceText == translationResult.OriginalText)
            //        {
            //            row[translateApiConfig.TargetLanguage] = translationResult.TranslatedText;
            //        }
            //    }
            //}
        }
    }
}