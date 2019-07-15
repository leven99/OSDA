using System.Windows;

namespace OSerialPort
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModels.MainWindow VMmainWindow = new ViewModels.MainWindow();

        public MainWindow()
        {
            InitializeComponent();
        }

        #region 打开/关闭串口
        private void OpenCloseSP(string empty)
        {
            VMmainWindow.OpenSP();
        }
        #endregion

        #region 发送
        private void Send(string empty)
        {
            VMmainWindow.Send();
        }
        #endregion

        #region 多项发送
        #endregion

        #region 路径选择
        #endregion

        #region 清接收区
        private void ClarReceData(string empty)
        {
            VMmainWindow.ClarReceData();
        }
        #endregion

        #region 清发送区
        private void ClearSendData(string empty)
        {
            VMmainWindow.ClearSendData();
        }
        #endregion

        #region 清空计数
        private void ClearCount(string empty)
        {
            VMmainWindow.ClearCount();
        }
        #endregion
    }
}
