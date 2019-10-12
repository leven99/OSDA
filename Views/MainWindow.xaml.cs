using OSDA.Interfaces;
using OSDA.ViewModels;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OSDA.Views
{
    public partial class MainWindow : Window, IDisposable
    {
        private MainWindowViewModel mainWindowViewModel = null;

        public MainWindow()
        {
            InitializeComponent();

            Height = 532;
            Width = Height / 0.625;

            mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;
        }

        #region Menu Mouse Support
        /// <summary>
        /// 鼠标移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MouseMove_Click(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        #endregion

        #region 菜单栏

        #region 文件
        /// <summary>
        /// 退出
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
        #endregion

        #region 工具
        /// <summary>
        /// 计算器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("calc.exe");
        }
        #endregion

        #region 选项

        #region 字节编码
        private void ASCIIMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.ASCIIEnable();
        }

        private void UTF8MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.UTF8Enable();
        }

        private void UTF16MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.UTF16Enable();
        }

        private void UTF32MenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.UTF32Enable();
        }
        #endregion

        /// <summary>
        /// RtsEnable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RtsEnableMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.RtsEnable();
        }

        /// <summary>
        /// DtrEnable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtrEnableMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.DtrEnable();
        }

        #region 流控制
        private void NoneMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.NoneEnable();
        }

        private void RequestToSendMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.RequestToSendEnable();
        }

        private void XOnXOffMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.XOnXOffEnable();
        }

        private void RequestToSendXOnXOffMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.RequestToSendXOnXOffEnable();
        }
        #endregion

        #region 发送换行
        private void NonesMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.NonesEnable();
        }

        private void CrMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.CrEnable();
        }

        private void LfMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.LfEnable();
        }

        private void CrLfMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.CrLfEnable();
        }
        #endregion

        #endregion

        #region 视图
        /// <summary>
        /// 精简视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EveryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.Reduced_Enable();
        }
        #endregion

        #region 帮助
        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerUpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.UpdateAsync();
        }

        /// <summary>
        /// Gitee 存储库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RPMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://gitee.com/leven9/OSDA");
        }

        /// <summary>
        /// 报告问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IssueMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://gitee.com/leven9/OSDA/issues");
        }
        #endregion

        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
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

        #region 发送文件
        private void SendFile(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.SendFile();
        }
        #endregion

        #region 路径选择
        private void SaveRecvPath(object sender, RoutedEventArgs e)
        {
            mainWindowViewModel.SaveRecvPath();
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

        #region RecvTextBox Support
        /// <summary>
        /// Mouse Double（鼠标双击）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecvTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mainWindowViewModel.Enable_Recv();
        }

        /// <summary>
        /// ScrollToEnd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RecvTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecvTextBox.ScrollToEnd();
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// 受保护的 Dispose 方法实现
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    /* 释放托管资源（如果需要） */
                }

                mainWindowViewModel.Dispose();
                mainWindowViewModel = null;

                disposedValue = true;
            }
        }

        /// <summary>
        /// mainWindowViewModel 字段 IDisposable 接口的 Dispose 方法实现（无参数）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    #region RecvTextBox Append Text Support
    public static class MvvmTextBox
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

            textBuffer.BufferClearingHandler += (sender, clearingText) =>
            {
                detectChanges = false;
                textBox.Clear();
                detectChanges = true;
            };

            textBox.Text = textBuffer.GetCurrentValue();
            textBuffer.BufferAppendedHandler += (sender, appendedText) =>
            {
                detectChanges = false;
                textBox.AppendText(appendedText.AppendedText);
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
            if(element != null)
            {
                element.SetValue(BufferProperty, value);
            }
        }
        public static ITextBoxAppend GetBuffer(UIElement element)
        {
            if(element == null)
            {
                return (ITextBoxAppend)null;
            }

            return (ITextBoxAppend)element.GetValue(BufferProperty);
        }
    }
    #endregion
}
