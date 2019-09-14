using OSDA.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace OSDA.Views
{
    /// <summary>
    /// WPFUpdate.xaml 的交互逻辑
    /// </summary>
    public partial class WPFUpdate : Window
    {
        private readonly WPFUpdateViewModel wPFUpdateViewModel = null;

        public WPFUpdate()
        {
            InitializeComponent();

            wPFUpdateViewModel = new WPFUpdateViewModel();
            DataContext = wPFUpdateViewModel;
        }

        private void UpdateYesButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://leven9.gitee.io/osdaweb/download.html");

            Close();
        }

        private void UpdateNoButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
