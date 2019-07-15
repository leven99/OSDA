using OSerialPort.ViewModels;
using System.Windows;

namespace OSerialPort
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowVM mainWindowVM = null;

        public MainWindow()
        {
            InitializeComponent();

            mainWindowVM = new MainWindowVM();
            DataContext = mainWindowVM;
        }

        #region 菜单栏
        /* 菜单栏暂未实现（文件、工具、视图和帮助）*/
        #endregion

        #region 打开/关闭串口
        private void OpenCloseSP(object sender, RoutedEventArgs e)
        {
            mainWindowVM.OpenSP();
        }
        #endregion

        #region 发送
        private void Send(object sender, RoutedEventArgs e)
        {
            mainWindowVM.Send();
        }
        #endregion

        #region 多项发送
        private void Sends(object sender, RoutedEventArgs e)
        {
            /* 多项发送使用新的UI窗口（UI Window），暂未实现*/
        }
        #endregion

        #region 路径选择
        private void SaveRecePath(object sender, RoutedEventArgs e)
        {
            mainWindowVM.SaveRecePath();
        }
        #endregion

        #region 清接收区
        private void ClarReceData(object sender, RoutedEventArgs e)
        {
            mainWindowVM.ClarReceData();
        }
        #endregion

        #region 清发送区
        private void ClearSendData(object sender, RoutedEventArgs e)
        {
            mainWindowVM.ClearSendData();
        }
        #endregion

        #region 清空计数
        private void ClearCount(object sender, RoutedEventArgs e)
        {
            mainWindowVM.ClearCount();
        }
        #endregion
    }
}
