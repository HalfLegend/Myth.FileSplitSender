using System.Windows;
using Microsoft.Practices.Prism.Mvvm;
using Myth.FileSplitSender.View.FileSenderPage;
using Myth.FileSplitSender.View.FileSplitter;

namespace Myth.FileSplitSender
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            InitializeViewModel();
        }

        private static void InitializeViewModel()
        {
            ViewModelLocationProvider.Register(typeof(FileSplitterPage).FullName, () => new FileSplitterViewModel());
            ViewModelLocationProvider.Register(typeof(FileSenderPage).FullName, () =>
            {
                return new FileSenderViewModel();

            });
        }
    }
}
