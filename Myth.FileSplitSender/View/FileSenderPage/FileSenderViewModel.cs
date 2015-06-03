using System.Windows.Forms;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Myth.FileSplitSender.Core;

namespace Myth.FileSplitSender.View.FileSenderPage
{
    internal class FileSenderViewModel : BindableBase
    {
        private string _srcFolderPath;

        public string SrcFolderPath
        {
            get { return _srcFolderPath; }
            set
            {
                SetProperty(ref _srcFolderPath, value);
                DoSendCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _browseSrcFolderCommand;

        public DelegateCommand BrowseSrcFolderCommand
        {
            get
            {
                return _browseSrcFolderCommand ?? (_browseSrcFolderCommand = new DelegateCommand(() =>
                {
                    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                    if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                    {
                        SrcFolderPath = folderBrowserDialog.SelectedPath;
                    }
                }));
            }
        }

        private string _targetEmailAddress;

        public string TargetEmailAddress
        {
            get { return _targetEmailAddress; }
            set
            {
                SetProperty(ref _targetEmailAddress, value);
                DoSendCommand.RaiseCanExecuteChanged();
            }
        }

        private DelegateCommand _doSendCommand;

        public DelegateCommand DoSendCommand
        {
            get { return _doSendCommand ?? (_doSendCommand = new DelegateCommand(() =>
            {
                FileSender.SendFileByOutlook(SrcFolderPath, TargetEmailAddress);
                
            }, () => !string.IsNullOrWhiteSpace(SrcFolderPath) && !string.IsNullOrWhiteSpace(TargetEmailAddress))); }
        }
    }
}