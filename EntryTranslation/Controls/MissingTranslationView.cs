﻿using EntryTranslation.Forms;
using EntryTranslation.Helpers;
using EntryTranslation.ResourceOperations;
using Sunny.UI;
using System.Globalization;

namespace EntryTranslation.Controls
{
    public partial class MissingTranslationView : UserControl
    {
        private ResourceLoader _resourceLoader;

        public MissingTranslationView()
        {
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
        }

        public ResourceLoader ResourceLoader
        {
            get { return _resourceLoader; }
            set
            {
                if (_resourceLoader != null)
                    _resourceLoader.ResourcesChanged -= ResourceLoaderOnResourcesChanged;

                _resourceLoader = value;

                comboBox1.SelectedIndex = 0;

                if (value != null)
                {
                    _resourceLoader.ResourcesChanged += ResourceLoaderOnResourcesChanged;
                    ResourceLoaderOnResourcesChanged(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler<OpenedItemEventArgs> ItemOpened;

        protected virtual void OnItemOpened(OpenedItemEventArgs e)
        {
            ItemOpened?.Invoke(this, e);
        }

        private void RefreshListView(object sender, EventArgs e)
        {
            listView1.BeginUpdate();

            listView1.Items.Clear();
            if (comboBox1.SelectedIndex <= 0)
            {
                listView1.Enabled = false;
            }
            else
            {
                listView1.Enabled = true;

                var selectedCulture = ((ComboBoxWrapper<CultureInfo>)comboBox1.SelectedItem).WrappedObject.Name;

                var missingItems = ResourceLoader.Resources.Where(res => res.HasMissingTranslations(selectedCulture));

                listView1.Items.AddRange(missingItems.OrderBy(x => x.Filename)
                    .Select(x => new ListViewItem(new[] { x.DisplayFolder, x.Id }) { Tag = x, ToolTipText = x.Filename }).ToArray());

                listView1.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.Columns[1].Width = Math.Max(95, listView1.Columns[1].Width);
            }

            listView1.EndUpdate();
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1 ||
                !listView1.RectangleToScreen(listView1.SelectedItems[0].Bounds).Contains(MousePosition))
                return;

            OnItemOpened(new OpenedItemEventArgs(listView1.SelectedItems[0].Tag as ResourceHolder,
                ((ComboBoxWrapper<CultureInfo>)comboBox1.SelectedItem).WrappedObject));
        }

        private void ResourceLoaderOnResourcesChanged(object sender, EventArgs eventArgs)
        {
            comboBox1.SelectedIndex = 0;

            // Remove old items except for the default value
            while (comboBox1.Items.Count >= 2)
                comboBox1.Items.RemoveAt(1);

            comboBox1.Items.AddRange(_resourceLoader.GetUsedLanguages().OrderBy(x => x.Name)
                .Select(x => new ComboBoxWrapper<CultureInfo>(x, info => $"{info.Name} - {info.DisplayName}"))
                .Cast<object>()
                .ToArray());
        }

        public class OpenedItemEventArgs : EventArgs
        {
            public OpenedItemEventArgs(ResourceHolder item, CultureInfo language)
            {
                Item = item;
                Language = language;
            }

            public ResourceHolder Item { get; }

            public CultureInfo Language { get; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var result = LanguageSelectDialog.ShowLanguageSelectDialog(ParentForm);
            if (result == null || comboBox1.Items.OfType<ComboBoxWrapper<CultureInfo>>().Any(x => x.WrappedObject.Equals(result)))
                return;

            var newItem = new ComboBoxWrapper<CultureInfo>(result, info => $"{info.Name} - {info.DisplayName}");

            comboBox1.Items.Add(newItem);
            comboBox1.SelectedItem = newItem;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            RefreshListView(sender, e);
        }
    }
}