using System.ComponentModel;
using DbTextEditor.Model;
using DbTextEditor.Shared;
using DbTextEditor.Shared.Exceptions;
using DbTextEditor.ViewModel.Commands;

namespace DbTextEditor.ViewModel
{
    public class EditorViewModel : BaseNotifyPropertyChanged
    {
        internal LocalFileModel Model { get; private set; }
        public bool IsNewFile => Model is null;
        public ICommand<string> TextChangedCommand { get; }

        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                if (_path == value) return;
                _path = value;
                OnPropertyChanged();
            }
        }

        private string _contents;
        public string Contents
        {
            get => _contents;
            set
            {
                if (_contents == value) return;
                _contents = value;
                OnPropertyChanged();
            }
        }

        private bool _isModified;
        public bool IsModified
        {
            get => _isModified;
            set
            {
                if (_isModified == value) return;
                _isModified = value;
                OnPropertyChanged();
            }
        }

        public EditorViewModel()
        {
            TextChangedCommand = new ChangeTextCommand(this);
        }

        public void InitializeModel(string filePath)
        {
            if (Model != null)
            {
                throw new BusinessLogicException("Model is already set for this view model");
            }

            Model = new LocalFileModel();
            Model.PropertyChanged += OnModelPropertyChanged;
            Model.Path = filePath;
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Path":
                    Path = Model.Path;
                    break;
                case "Contents":
                    Contents = Model.Contents;
                    break;
            }
        }
    }
}