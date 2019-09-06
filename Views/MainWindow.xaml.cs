using OSerialPort.Interface;
using OSerialPort.ViewModels;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace OSerialPort
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel mainWindowViewModel = null;

        public MainWindow()
        {
            InitializeComponent();

            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
        }

        #region 菜单栏
        /// <summary>
        /// 文件 - 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if ((mainWindowViewModel.SPserialPort != null) && mainWindowViewModel.SPserialPort.IsOpen)
            {
                mainWindowViewModel.CloseSP();
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
            Process.Start("calc.exe");
        }

        /// <summary>
        /// 帮助 - 检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerUpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.Update();
        }

        /// <summary>
        /// 帮助 - Gitee Repository（码云存储库）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RPMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://gitee.com/leven9/OSerialPort");
        }

        /// <summary>
        /// 帮助 - Report issue（报告问题）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IssueMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://gitee.com/leven9/OSerialPort/issues");
        }
        #endregion

        #region 打开/关闭串口
        private void OpenCloseSP(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.OpenSP();
        }
        #endregion

        #region 发送
        private void Send(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.Send();
        }
        #endregion

        #region 多项发送
        private void Sends(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.Sends();
        }
        #endregion

        #region 路径选择
        private void SaveRecePath(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.SaveRecePath();
        }
        #endregion

        #region 清接收区
        private void ClarReceData(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.ClarReceData();
        }
        #endregion

        #region 清发送区
        private void ClearSendData(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.ClearSendData();
        }
        #endregion

        #region 清空计数
        private void ClearCount(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.ClearCount();
        }
        #endregion
    }

    #region 用于MVVM模型下TextBox控件的Append Text实现
    public class MvvmTextBox
    {
        public static readonly DependencyProperty BufferProperty =
            DependencyProperty.RegisterAttached(
                "Buffer",
                typeof(ITextBoxAppend),
                typeof(MvvmTextBox),
                new UIPropertyMetadata(null, PropertyChangedCallback)
            );

        private static void PropertyChangedCallback(
            DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs depPropChangedEvArgs)
        {
            var textBox = (TextBox)dependencyObject;
            var textBuffer = (ITextBoxAppend)depPropChangedEvArgs.NewValue;

            var detectChanges = true;

            textBox.Text = textBuffer.GetCurrentValue();
            textBuffer.BufferAppendedHandler += (sender, appendedText) =>
            {
                detectChanges = false;
                textBox.AppendText(appendedText);
                detectChanges = true;
            };

            textBuffer.BufferClearingHandler += (sender, clearingText) =>
            {
                detectChanges = false;
                textBox.Clear();
                detectChanges = true;
            };

            textBox.TextChanged += (sender, args) =>
            {
                if (!detectChanges)
                    return;

                foreach (var change in args.Changes)
                {
                    if (change.AddedLength > 0)
                    {
                        var addedContent = textBox.Text.Substring(
                            change.Offset, change.AddedLength);

                        textBuffer.Append(addedContent, change.Offset);
                    }
                    else
                    {
                        textBuffer.Delete(change.Offset, change.RemovedLength);
                    }
                }

                Debug.WriteLine(textBuffer.GetCurrentValue());
            };
        }

        public static void SetBuffer(UIElement element, bool value)
        {
            element.SetValue(BufferProperty, value);
        }
        public static ITextBoxAppend GetBuffer(UIElement element)
        {
            return (ITextBoxAppend)element.GetValue(BufferProperty);
        }
    }
    #endregion
}
