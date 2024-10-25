using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EntryTranslator.ResourceOperations
{
    public class ResourceLoader
    {
        private static readonly IEnumerable<CultureInfo> SupportedCultureCache
            = CultureInfo.GetCultures(CultureTypes.AllCultures).Where(x => x.Name != string.Empty).ToList();

        private readonly Dictionary<string, ResourceHolder> _resourceStore;

        public event EventHandler<ResourceLoadProgressEventArgs> ResourceLoadProgress;

        public event EventHandler ResourcesChanged;

        private string _openedPath;

        public ResourceLoader()
        {
            _resourceStore = new Dictionary<string, ResourceHolder>();
        }

        public string OpenedPath
        {
            get { return _openedPath; }
            private set
            {
                _openedPath = value;
                OnResourcesChanged();
            }
        }

        public IEnumerable<ResourceHolder> Resources
        {
            get
            {
                var result = _resourceStore.Values.AsEnumerable();
                return result;
            }
        }

        /// <summary>
        /// Check and prompt for save
        /// </summary>
        /// <returns>True if we can safely close</returns>
        public bool CanClose()
        {
            var isDirty = Resources.Any(resource => resource.IsDirty);

            if (isDirty)
            {
                var dialogResult = MessageBox.Show("当前有未保存数据", "数据保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                // Return false only if user presses cancel
                if (dialogResult != DialogResult.Yes)
                    return dialogResult == DialogResult.No;

                SaveAll();
            }
            return true;
        }

        public void Close()
        {
            if (!CanClose())
                throw new InvalidOperationException("��ǰ�޷��ر�");

            _resourceStore.Clear();
            OpenedPath = string.Empty;
        }

        public IEnumerable<CultureInfo> GetUsedLanguages()
        {
            return Resources.Aggregate(Enumerable.Empty<LanguageHolder>(),
                (holder, pair) => holder.Concat(pair.Languages.Values))
                .GroupBy(x => x.LanguageId)
                .Select(holders => holders.First().CultureInfo);
        }

        public void OpenProject(string selectedPath)
        {
            OnResourceLoadProgress(new ResourceLoadProgressEventArgs("加载语言资源..."));

            FindResx(selectedPath);

            // Test for bad files
            foreach (var pair in _resourceStore.ToList())
            {
                try
                {
                    pair.Value.LoadResource();
                }
                catch (SystemException ex)
                {
                    _resourceStore.Remove(pair.Key);
                }
            }

            OpenedPath = selectedPath;

            OnResourceLoadProgress(new ResourceLoadProgressEventArgs(""));
        }

        public void SaveAll()
        {
            foreach (var resource in _resourceStore.Values)
            {
                resource.Save();
            }
        }

        protected virtual void OnResourceLoadProgress(ResourceLoadProgressEventArgs e)
        {
            ResourceLoadProgress?.Invoke(this, e);
        }

        protected virtual void OnResourcesChanged()
        {
            ResourcesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FindResx(string rootDirectory)
        {
            FindResx(rootDirectory, rootDirectory);
        }

        private void FindResx(string rootDirectory, string currentDirectory)
        {
            var displayFolder = string.Empty;
            if (currentDirectory.StartsWith(rootDirectory, StringComparison.InvariantCultureIgnoreCase))
                displayFolder = currentDirectory.Substring(rootDirectory.Length);

            displayFolder = displayFolder.TrimStart('\\', '/');

            var files = Directory.GetFiles(currentDirectory, "*.resx");

            foreach (var filename in files)
            {
                var filenameNoExt = Path.GetFileNameWithoutExtension(filename);
                if (string.IsNullOrEmpty(filenameNoExt)) continue;

                // Try to get the language code
                var potentialLanguageCode = Path.GetExtension(filenameNoExt).TrimStart('.');

                var culture = potentialLanguageCode;
                filenameNoExt = Path.GetFileNameWithoutExtension(filenameNoExt);

                ResourceHolder resourceHolder;
                var key = (displayFolder + "\\" + filenameNoExt).ToLower();
                if (!_resourceStore.TryGetValue(key, out resourceHolder))
                {
                    resourceHolder = new ResourceHolder
                    {
                        Id = filenameNoExt,
                        DisplayFolder = displayFolder
                    };

                    var dir = Path.GetDirectoryName(filename);
                    Debug.Assert(dir != null, "dir != null");
                    resourceHolder.Filename = Path.Combine(dir, filenameNoExt + ".resx");

                    _resourceStore.Add(key, resourceHolder);
                }

                if (culture != null)
                {
                    if (resourceHolder.Languages.ContainsKey(culture.ToLower()))
                        throw new InvalidDataException(filename);

                    resourceHolder.Languages.Add(culture.ToLower(), new LanguageHolder(culture, filename));
                }
            }

            var subfolders = Directory.GetDirectories(currentDirectory);
            foreach (var subfolder in subfolders)
            {
                FindResx(rootDirectory, subfolder);
            }
        }
    }
}