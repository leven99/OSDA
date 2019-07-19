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
        /// <summary>
        /// 文件 - 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (mainWindowVM.SPserialPort != null && mainWindowVM.SPserialPort.IsOpen)
            {
                mainWindowVM.CloseSP();
            }

            Close();
        }

        /// <summary>
        /// 工具 - 计算器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        /// <summary>
        /// 帮助 - 检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerUpMenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 帮助 - Gitee Repository（码云存储库）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RPMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gitee.com/leven9/OSerialPort");
        }

        /// <summary>
        /// 帮助 - Report issue（报告问题）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IssueMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://gitee.com/leven9/OSerialPort/issues");
        }
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
