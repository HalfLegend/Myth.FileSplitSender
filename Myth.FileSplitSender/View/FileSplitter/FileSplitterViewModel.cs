using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Myth.FileSplitSender.View.FileSplitter
{
    internal class FileSplitterViewModel : BindableBase
    {
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;

        private string _srcFilePath;

        public string SrcFilePath
        {
            get { return _srcFilePath; }
            set
            {
                SetProperty(ref _srcFilePath, value);
                DoSplitCommand.RaiseCanExecuteChanged();
            }
        }

        private double  _processRate;

        public double ProcessRate
        {
            get { return _processRate; }
            set { SetProperty(ref _processRate, value); }
        }

        private string _targetFolderPath;

        public string TargetFolderPath
        {
            get { return _targetFolderPath; }
            set
            {
                SetProperty(ref _targetFolderPath, value);
                DoSplitCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _browseSrcFileCommand;

        public DelegateCommand BrowseSrcFileCommand
        {
            get
            {
                return _browseSrcFileCommand ?? (_browseSrcFileCommand = new DelegateCommand(() =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                    {
                        SrcFilePath = openFileDialog.FileName;
                    }
                }));
            }
        }

        private DelegateCommand _browseTargetDirectoryCommand;

        public DelegateCommand BrowseTargetDirectoryCommand
        {
            get
            {
                return CreateDelegateCommand(ref _browseTargetDirectoryCommand, () =>
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        TargetFolderPath = folderBrowserDialog.SelectedPath;
                    }
                });
            }
        }

        private DelegateCommand _doSplitFile;

        public DelegateCommand DoSplitCommand
        {
            get { return _doSplitFile?? (_doSplitFile = new  DelegateCommand(async () =>
            {
                await Core.FileSplitter.SplitFileAsync(SrcFilePath, TargetFolderPath, FileSplitSize * (long)DataUnit, async (s, d) =>
                {
                    ProcessRate = d;
                    if (_dispatcher == Dispatcher.CurrentDispatcher)
                    {
                        SplitFileOutputList.Add(s);
                    }
                    else
                    {
                        await _dispatcher.InvokeAsync(()=> SplitFileOutputList.Add(s));
                        OnPropertyChanged(()=> SelectedOutputIndex);
                    }
                });
            }, () => !(string.IsNullOrWhiteSpace(SrcFilePath) || string.IsNullOrWhiteSpace(TargetFolderPath)))); }
        }

        private static DelegateCommand CreateDelegateCommand(ref DelegateCommand delegateCommand, Action action)
        {
            return delegateCommand ?? (delegateCommand = new DelegateCommand(action));
        }

        private int _fileSplitSize = 10;

        public int FileSplitSize
        {
            get { return _fileSplitSize; }
            set { SetProperty(ref _fileSplitSize, value); }
        }

        private DataUnitEnum _dataUnit;

        public DataUnitEnum DataUnit
        {
            get { return _dataUnit; }
            set
            {
                SetProperty(ref _dataUnit, value); 
            }
        }

        private ObservableCollection<string> _splitFileOutputList;

        public ObservableCollection<string> SplitFileOutputList => _splitFileOutputList ?? (_splitFileOutputList = new ObservableCollection<string>());

        public int SelectedOutputIndex => SplitFileOutputList.Count - 1;
    }
}