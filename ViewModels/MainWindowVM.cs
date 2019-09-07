using OSerialPort.Models;
using System;
using System.Globalization;
using System.IO.Ports;
using System.Windows.Media;
using System.Windows.Threading;

namespace OSerialPort.ViewModels
{
    class MainWindowViewModel : MainWindowBase, IDisposable
    {
        public SerialPort SPserialPort = new SerialPort();

        public HelpModel HelpModel { get; set; }
        public RecvModel RecvModel { get; set; }
        public SendModel SendModel { get; set; }
        public SerialPortModel SerialPortModel { get; set; }
        public TimerModel TimerModel { get; set; }

        #region 菜单栏

        #region 选项 - 字节编码
        public void ASCIIEnable()
        {
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF16Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.ASCIIEnable = true;

            try
            {
                if(SerialPortModel.ASCIIEnable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.ASCII;
                }
            }
            catch
            {
                DepictInfo = "更改字节编码为ASCII异常";
            }
        }

        public void UTF8Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF16Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.UTF8Enable = true;

            try
            {
                if (SerialPortModel.UTF8Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.UTF8;
                }
            }
            catch
            {
                DepictInfo = "更改字节编码为UTF-8异常";
            }
        }

        public void UTF16Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF32Enable = false;

            SerialPortModel.UTF16Enable = true;

            try
            {
                if (SerialPortModel.UTF16Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.Unicode;
                }
            }
            catch
            {
                DepictInfo = "更改字节编码为Unicode(UTF-16)异常";
            }
        }

        public void UTF32Enable()
        {
            SerialPortModel.ASCIIEnable = false;
            SerialPortModel.UTF8Enable = false;
            SerialPortModel.UTF16Enable = false;

            SerialPortModel.UTF32Enable = true;

            try
            {
                if (SerialPortModel.UTF32Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.UTF32;
                }
            }
            catch
            {
                DepictInfo = "更改字节编码为UTF-32异常";
            }
        }
        #endregion

        public void RtsEnable()
        {
            SerialPortModel.RtsEnable = !(SerialPortModel.RtsEnable);

            try
            {
                if(SerialPortModel.RtsEnable)
                {
                    SPserialPort.RtsEnable = true;
                }
                else
                {
                    SPserialPort.RtsEnable = false;
                }
            }
            catch
            {
                DepictInfo = "信号Rts启用异常";
            }
        }

        public void DtrEnable()
        {
            SerialPortModel.DtrEnable = !(SerialPortModel.DtrEnable);

            try
            {
                if (SerialPortModel.DtrEnable)
                {
                    SPserialPort.DtrEnable = true;
                }
                else
                {
                    SPserialPort.DtrEnable = false;
                }
            }
            catch
            {
                DepictInfo = "信号Dtr启用异常";
            }
        }

        #region 选项 - 流配置
        public void NoneEnable()
        {
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.NoneEnable = true;

            try
            {
                if(SerialPortModel.NoneEnable)
                {
                    SPserialPort.Handshake = Handshake.None;
                }
            }
            catch
            {
                DepictInfo = "更改流控制为None异常";
            }
        }

        public void RequestToSendEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.RequestToSendEnable = true;

            try
            {
                if (SerialPortModel.RequestToSendEnable)
                {
                    SPserialPort.Handshake = Handshake.RequestToSend;
                }
            }
            catch
            {
                DepictInfo = "更改流控制为Hardware异常";
            }
        }

        public void XOnXOffEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.RequestToSendEnable = false;
            SerialPortModel.RequestToSendXOnXOffEnable = false;

            SerialPortModel.XOnXOffEnable = true;

            try
            {
                if (SerialPortModel.XOnXOffEnable)
                {
                    SPserialPort.Handshake = Handshake.XOnXOff;
                }
            }
            catch
            {
                DepictInfo = "更改流控制为XOnXOff异常";
            }
        }

        public void RequestToSendXOnXOffEnable()
        {
            SerialPortModel.NoneEnable = false;
            SerialPortModel.XOnXOffEnable = false;
            SerialPortModel.RequestToSendEnable = false;

            SerialPortModel.RequestToSendXOnXOffEnable = true;

            try
            {
                if (SerialPortModel.RequestToSendXOnXOffEnable)
                {
                    SPserialPort.Handshake = Handshake.RequestToSendXOnXOff;
                }
            }
            catch
            {
                DepictInfo = "更改流控制为Hardware and XOnXOff异常";
            }
        }
        #endregion

        /// <summary>
        /// 帮助 - 检查更新
        /// </summary>
        public void Update()
        {

        }
        #endregion

        #region 打开/关闭串口
        public bool OpenSP()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                return CloseSP();
            }

            try
            {
                SPserialPort.PortName = SerialPortModel.SPPort;
                SPserialPort.BaudRate = SerialPortModel.SPBaudRate;
                SPserialPort.DataBits = SerialPortModel.SPDataBits;
                SPserialPort.StopBits = SerialPortModel.GetStopBits(SerialPortModel.SPStopBits.ToString());
                SPserialPort.Parity = SerialPortModel.GetParity(SerialPortModel.SPParity.ToString());

                SPserialPort.WriteBufferSize = 1048576;   /* 设置串行端口输出缓冲区的大小为1048576字节，即1MB */
                SPserialPort.ReadBufferSize = 2097152;    /* 设置串行端口输入缓冲区的大小为2097152字节，即2MB */

                /* 字节编码方式 */
                if (SerialPortModel.ASCIIEnable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.ASCII;
                }
                else if (SerialPortModel.UTF8Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.UTF8;
                }
                else if (SerialPortModel.UTF16Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.Unicode;
                }
                else if (SerialPortModel.UTF32Enable)
                {
                    SPserialPort.Encoding = System.Text.Encoding.UTF32;
                }

                /* 发送请求（RTS）信号 */
                if (SerialPortModel.RtsEnable)
                {
                    SPserialPort.RtsEnable = true;
                }
                else
                {
                    SPserialPort.RtsEnable = false;
                }

                /* 数据终端就绪（DTR）信号 */
                if (SerialPortModel.DtrEnable)
                {
                    SPserialPort.DtrEnable = true;
                }
                else
                {
                    SPserialPort.DtrEnable = false;
                }

                /* 数据传输的握手协议(或者通信控制协议、或者流控制) */
                if (SerialPortModel.NoneEnable)
                {
                    SPserialPort.Handshake = Handshake.None;
                }
                else if (SerialPortModel.RequestToSendEnable)
                {
                    SPserialPort.Handshake = Handshake.RequestToSend;
                }
                else if (SerialPortModel.XOnXOffEnable)
                {
                    SPserialPort.Handshake = Handshake.XOnXOff;
                }
                else if (SerialPortModel.RequestToSendXOnXOffEnable)
                {
                    SPserialPort.Handshake = Handshake.RequestToSendXOnXOff;
                }

                SPserialPort.DataReceived += new SerialDataReceivedEventHandler(RecvModel.SerialPort_DataReceived);

                SPserialPort.Open();

                if (SPserialPort.IsOpen)
                {
                    SerialPortModel.SPBrush = Brushes.GreenYellow;
                    SerialPortModel.OpenCloseSP = "关闭串口";
                    DepictInfo = string.Format("成功打开串行端口{0}、波特率{1}、数据位{2}、停止位{3}、校验位{4}",
                        SPserialPort.PortName, SPserialPort.BaudRate.ToString(), SPserialPort.DataBits.ToString(),
                        SPserialPort.StopBits.ToString(), SPserialPort.Parity.ToString());

                    SerialPortModel.SPPortEnable = false;
                    SerialPortModel.SPBaudRateEnable = false;
                    SerialPortModel.SPDataBitsEnable = false;
                    SerialPortModel.SPStopBitsEnable = false;
                    SerialPortModel.SPParityEnable = false;

                    return true;
                }
                else
                {
                    DepictInfo = "串行端口打开失败";

                    return false;
                }
            }
            catch
            {
                DepictInfo = "串行端口发生意外，打开失败，请检查线路";

                return false;
            }
        }

        public bool CloseSP()
        {
            try
            {
                if (SPserialPort.IsOpen)
                {
                    SPserialPort.Close();

                    SerialPortModel.SPBrush = Brushes.Red;
                    SerialPortModel.OpenCloseSP = "打开串口";

                    DepictInfo = "串行端口关闭成功";

                    SerialPortModel.SPPortEnable = true;
                    SerialPortModel.SPBaudRateEnable = true;
                    SerialPortModel.SPDataBitsEnable = true;
                    SerialPortModel.SPStopBitsEnable = true;
                    SerialPortModel.SPParityEnable = true;

                    RecvModel.ReceAutoSave = "已停止";
                    RecvModel.ReceHeader = "接收区：已接收" + RecvModel.ReceDataCount +
                            "字节，接收自动保存[" + RecvModel.ReceAutoSave + "]";

                    return SPserialPort.IsOpen;
                }
                else
                {
                    DepictInfo = "串行端口已关闭";

                    return SPserialPort.IsOpen;
                }
            }
            catch
            {
                DepictInfo = "串行端口发生意外，关闭失败，请检查线路";

                return false;
            }
        }
        #endregion

        #region 自动发送
        public DispatcherTimer AutoSendDispatcherTimer = new DispatcherTimer();

        public void InitAutoSendTimer()
        {
            AutoSendDispatcherTimer.IsEnabled = false;
            AutoSendDispatcherTimer.Tick += AutoSendDispatcherTimer_Tick;
        }

        public void AutoSendDispatcherTimer_Tick(object sender, EventArgs e)
        {
            Send();
        }

        public void StartAutoSendTimer(int interval)
        {
            AutoSendDispatcherTimer.IsEnabled = true;
            AutoSendDispatcherTimer.Interval = TimeSpan.FromMilliseconds(interval);
            AutoSendDispatcherTimer.Start();
        }

        public void StopAutoSendTimer()
        {
            AutoSendDispatcherTimer.IsEnabled = false;
            AutoSendDispatcherTimer.Stop();
        }
        #endregion

        #region 发送
        public void Send()
        {
            try
            {
                if (SPserialPort != null && SPserialPort.IsOpen)
                {
                    if (SendModel.HexSend)
                    {
                        int cnt = 0;
                        string[] _sendData = SendModel.SendData.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        char[] sendData = new char[_sendData.Length];

                        foreach (var tmp in _sendData)
                        {
                            sendData[cnt++] = (char)Int16.Parse(tmp, NumberStyles.AllowHexSpecifier);
                        }

                        SendModel.SendDataCount += cnt;
                        SPserialPort.Write(sendData, 0, cnt);

                    }
                    else
                    {
                        SendModel.SendDataCount += SendModel.SendData.Length;
                        SPserialPort.Write(SendModel.SendData.ToCharArray(), 0, SendModel.SendData.Length);
                    }
                }
            }
            catch
            {
                DepictInfo = "发送异常，请检查发送数据";
            }
        }
        #endregion

        #region 多项发送
        public void Sends()
        {
            if (SPserialPort != null && SPserialPort.IsOpen)
            {
                if (SendModel.HexSend)
                {

                }
                else
                {

                }

                SendModel.SendDataCount += SendModel.SendData.Length;
            }
        }
        #endregion

        #region 路径选择
        public void SaveRecePath()
        {
            RecvModel.RecePath();
        }
        #endregion

        #region 辅助区
        public bool _AutoSend;
        public bool AutoSend
        {
            get
            {
                return _AutoSend;
            }
            set
            {
                if (SPserialPort != null && SPserialPort.IsOpen)
                {
                    if (_AutoSend != value)
                    {
                        _AutoSend = value;
                        RaisePropertyChanged("AutoSend");
                    }

                    if (AutoSend == true)
                    {
                        StartAutoSendTimer(SendModel.AutoSendNum);
                    }
                    else
                    {
                        StopAutoSendTimer();
                    }
                }
            }
        }
        #endregion

        #region 清接收区
        public void ClarReceData()
        {
            RecvModel.ReceData.Delete();
        }
        #endregion

        #region 清发送区
        public void ClearSendData()
        {
            SendModel.SendData = string.Empty;
        }
        #endregion

        #region 清空计数
        public void ClearCount()
        {
            RecvModel.ReceDataCount = 0;
            RecvModel.ReceHeader = "接收区：已接收" + RecvModel.ReceDataCount + "字节，接收自动保存[" + RecvModel.ReceAutoSave + "]";

            SendModel.SendDataCount = 0;
        }
        #endregion

        public MainWindowViewModel()
        {
            HelpModel = new HelpModel();
            HelpModel.HelpDataContext();

            RecvModel = new RecvModel();
            RecvModel.RecvDataContext();

            SendModel = new SendModel();
            SendModel.SendDataContext();

            SerialPortModel = new SerialPortModel();
            SerialPortModel.SerialPortDataContext();

            DepictInfo = "串行端口调试助手";

            TimerModel = new TimerModel();
            TimerModel.TimerDataContext();

            AutoSend = false;
            InitAutoSendTimer();
        }

        #region IDisposable 实现
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
                    /* 释放托管资源 */
                    SPserialPort.Dispose();
                }

                /* 释放非托管资源（如果有的话） */

                disposedValue = true;
            }
        }

        /// <summary>
        /// SerialPort 字段 IDisposable 接口的 Dispose 方法实现（无参数）
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
