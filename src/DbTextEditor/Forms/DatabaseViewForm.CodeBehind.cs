using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using DbTextEditor.Shared.DataBinding;
using DbTextEditor.Shared.DependencyInjection;
using DbTextEditor.ViewModel.Interfaces;

namespace DbTextEditor.Forms
{
    public partial class DatabaseViewForm
    {
        private IDatabaseViewViewModel _viewModel;

        public DatabaseViewForm()
        {
            _viewModel = CompositionRoot.Resolve<IDatabaseViewViewModel>();
            InitializeComponent();
            InitializeRefreshButton();
            Load += OnLoad;
            FilterBox.TextChanged += OnFilterBoxTextChanged;
        }

        private void OnLoad(object sender, EventArgs e)
        {
            MakeBindings();
            CallRefreshCommand();
        }

        private void OnFilterBoxTextChanged(object sender, EventArgs e)
        {
            RefreshFiles();
        }

        private void InitializeRefreshButton()
        {
            RefreshButton.Click += OnRefreshButtonClick;
        }

        private void OnRefreshButtonClick(object sender, EventArgs args)
        {
            CallRefreshCommand();
        }

        private void CallRefreshCommand()
        {
            _viewModel.RefreshCommand.Execute();
        }

        private void MakeBindings()
        {
            Bindings.ForCollection(_viewModel.DbFileNames, OnDbFileNamesChanged);
        }

        private void OnDbFileNamesChanged(NotifyCollectionChangedEventArgs obj)
        {
            RefreshFiles();
        }

        private void RefreshFiles()
        {
            var filterText = FilterBox.Text?.Trim() ?? string.Empty;
            var listViewItems = _viewModel.DbFileNames
                .Where(fileName => string.IsNullOrEmpty(filterText) || fileName.Contains(FilterBox.Text))
                .Select(fileName => new ListViewItem(fileName, 0))
                .ToArray();

            FilesListView.Items.Clear();
            FilesListView.Items.AddRange(listViewItems);
        }
    }
}