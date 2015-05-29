using System.Windows.Controls;
using Microsoft.Practices.Prism.Mvvm;

namespace Myth.FileSplitSender.View.FileSplitter
{
    /// <summary>
    /// FileSplitterPage.xaml 的交互逻辑
    /// </summary>
    public partial class FileSplitterPage : IView
    {
        public FileSplitterPage()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = (ListBox) sender;
            listBox.ScrollIntoView(listBox.SelectedItem);
        }
    }
}
