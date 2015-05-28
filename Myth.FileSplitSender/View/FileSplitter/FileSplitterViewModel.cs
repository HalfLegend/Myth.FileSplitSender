using Microsoft.Practices.Prism.Mvvm;

namespace Myth.FileSplitSender.View.FileSplitter
{
    internal class FileSplitterViewModel : BindableBase
    {
        private string _srcFilePath;

        public string SrcFilePath
        {
            get { return _srcFilePath; }
            set
            {
                if (_srcFilePath == value)
                {
                    return;
                }
                _srcFilePath = value;
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged(SrcFilePath);
            }
        }

        private string _targetFolderPath;

        public string TargetFolderPath
        {
            get { return _targetFolderPath; }
            set
            {
                if (_targetFolderPath == value)
                {
                    return;
                }
                _targetFolderPath = value;
                // ReSharper disable once ExplicitCallerInfoArgument
                OnPropertyChanged(TargetFolderPath);
            }
        }
    }
}